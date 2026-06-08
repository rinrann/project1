/* ============================================================
   ANKURAN — Authentication & Session module
   ------------------------------------------------------------
   Single source of truth for:
     • the credential directory (employee ID / email -> profile)
     • role resolution after a successful login
     • the permission matrix (module -> role -> level)
     • session creation, persistence, expiry and teardown
     • a route guard used by every protected page

   No role is ever chosen by the user. The role is read from the
   matched user record at login time; permissions are derived
   from that role and frozen into the session.
   ============================================================ */
(function () {
  "use strict";

  /* ----------------------------------------------------------
     1 · USER DIRECTORY  (stand-in for the server `users` table)
     Each record is keyed by a unique Employee ID. `aliases`
     lets a person sign in with either their ID or work email.
     In production these rows live in the DB and the password
     is a salted hash verified server-side — never shipped to
     the browser. Demo passwords here are illustrative only.
  ---------------------------------------------------------- */
  const USERS = [
    { empId: "OWN-001", email: "owner@ankuran.in",     password: "ankuran", name: "Dr. Rajat Anand",   role: "Super Admin",   title: "Founder & Medical Director",   color: "#1b76e5", dept: "Management" },
    { empId: "SUP-001", email: "s.rao@ankuran.in",      password: "ankuran", name: "Sunita Rao",        role: "Admin",         title: "Clinic Supervisor",            color: "#9d5c63", dept: "Administration" },
    { empId: "DOC-007", email: "a.menon@ankuran.in",    password: "ankuran", name: "Dr. Aparna Menon",  role: "Doctor",        title: "Reproductive Endocrinologist", color: "#9d5c63", dept: "Clinical", doctorId: "DR-01" },
    { empId: "REC-014", email: "r.shah@ankuran.in",     password: "ankuran", name: "Ritika Shah",       role: "Receptionist",  title: "Front Office Executive",       color: "#4a7aa7", dept: "Front Office" },
    { empId: "EMB-003", email: "s.kulkarni@ankuran.in", password: "ankuran", name: "Sneha Kulkarni",    role: "Embryologist",  title: "Senior Embryologist",          color: "#7a6aa8", dept: "Laboratory" },
    { empId: "ACC-009", email: "d.reddy@ankuran.in",    password: "ankuran", name: "Deepa Reddy",       role: "Accounts Team", title: "Finance & Accounts",           color: "#2f8f6b", dept: "Accounts" },
    { empId: "NUR-021", email: "f.qureshi@ankuran.in",  password: "ankuran", name: "Farhan Qureshi",    role: "Nurse",         title: "Clinical Nurse",               color: "#c4843b", dept: "Nursing" },
  ];

  /* Employee-ID prefix → role.  This is the IMPLICIT role detection:
     the role is inferred from the ID a person signs in with, so
     OWN-… is always a Super Admin, DOC-… a Doctor, and so on. */
  const PREFIX_ROLE = {
    OWN: "Super Admin", SA: "Super Admin", SADMIN: "Super Admin",
    SUP: "Admin", ADM: "Admin", ADMIN: "Admin",
    DOC: "Doctor", DR: "Doctor",
    NUR: "Nurse",
    REC: "Receptionist", FO: "Receptionist",
    EMB: "Embryologist",
    ACC: "Accounts Team", FIN: "Accounts Team",
  };
  function roleFromPrefix(empId) {
    const p = String(empId || "").split(/[-_ ]/)[0].toUpperCase();
    return PREFIX_ROLE[p] || null;
  }

  /* ----------------------------------------------------------
     2 · PERMISSION MATRIX  (module -> role -> access level)
     Levels: None | View | Edit | Full. Mirrors DB.PERMS used
     inside the app so login and app agree on one rulebook.
  ---------------------------------------------------------- */
  const PERMS = {
    "Dashboard":      { Admin:"Full", Receptionist:"View", Doctor:"View", Embryologist:"View", Nurse:"View", "Accounts Team":"View" },
    "Patients":       { Admin:"Full", Receptionist:"Edit", Doctor:"Edit", Embryologist:"View", Nurse:"Edit", "Accounts Team":"View" },
    "Appointments":   { Admin:"Full", Receptionist:"Full", Doctor:"Edit", Embryologist:"None", Nurse:"View", "Accounts Team":"None" },
    "Doctors":        { Admin:"Full", Receptionist:"View", Doctor:"View", Embryologist:"View", Nurse:"View", "Accounts Team":"None" },
    "Investigation":  { Admin:"Full", Receptionist:"Edit", Doctor:"Full", Embryologist:"View", Nurse:"Edit", "Accounts Team":"None" },
    "IVF Cycles":     { Admin:"Full", Receptionist:"None", Doctor:"Full", Embryologist:"Edit", Nurse:"View", "Accounts Team":"None" },
    "Billing":        { Admin:"Full", Receptionist:"Edit", Doctor:"None", Embryologist:"None", Nurse:"None", "Accounts Team":"Full" },
    "Pharmacy":       { Admin:"Full", Receptionist:"View", Doctor:"View", Embryologist:"View", Nurse:"Edit", "Accounts Team":"View" },
    "Employees":      { Admin:"Full", Receptionist:"None", Doctor:"None", Embryologist:"None", Nurse:"None", "Accounts Team":"None" },
    "Administration": { Admin:"Full", Receptionist:"None", Doctor:"None", Embryologist:"None", Nurse:"None", "Accounts Team":"None" },
    "Reports":        { Admin:"Full", Receptionist:"View", Doctor:"View", Embryologist:"View", Nurse:"View", "Accounts Team":"Full" },
  };

  // Build the list of modules a role may at least view.
  function permissionsForRole(role) {
    const out = {};
    Object.keys(PERMS).forEach(mod => {
      // Super Admin (the owner) has unrestricted access to everything.
      out[mod] = (role === "Super Admin") ? "Full" : ((PERMS[mod] && PERMS[mod][role]) || "None");
    });
    return out;
  }

  /* ----------------------------------------------------------
     3 · SESSION STORAGE
     "Remember me" => localStorage (survives browser restart,
     30-day sliding window). Otherwise sessionStorage (cleared
     when the tab closes, 8-hour cap).
  ---------------------------------------------------------- */
  const KEY = "ankuran_session";
  const LEGACY_KEY = "ankuran_role"; // kept in sync for older scripts
  const REMEMBER_MS = 30 * 24 * 60 * 60 * 1000; // 30 days
  const SHIFT_MS     = 8  * 60 * 60 * 1000;      // 8 hours

  function store(remember) { return remember ? window.localStorage : window.sessionStorage; }

  function readRaw() {
    try {
      return JSON.parse(localStorage.getItem(KEY)) ||
             JSON.parse(sessionStorage.getItem(KEY)) || null;
    } catch (e) { return null; }
  }

  /* ----------------------------------------------------------
     4 · PUBLIC API
  ---------------------------------------------------------- */
  const Auth = {
    USERS, PERMS,
    permissionsForRole,

    /* Validate credentials. Returns { ok, session } or { ok:false, error }.
       `identifier` may be an Employee ID or a work email (case-insensitive). */
    authenticate(identifier, password, remember) {
      const id = String(identifier || "").trim().toLowerCase();
      if (!id || !password) {
        return { ok: false, error: "Enter your Employee ID and password." };
      }
      const user = USERS.find(u =>
        u.empId.toLowerCase() === id || u.email.toLowerCase() === id
      );
      // Generic message — never reveal which half was wrong.
      if (!user || user.password !== password) {
        return { ok: false, error: "Those credentials don't match an active account." };
      }

      // Role is detected IMPLICITLY from the Employee-ID prefix first,
      // then falls back to the directory record.
      const resolvedRole = roleFromPrefix(user.empId) || user.role;
      const now = Date.now();
      const session = {
        userId:    user.empId,
        name:      user.name,
        email:     user.email,
        role:      resolvedRole,        // <- inferred from ID prefix, never chosen
        title:     user.title,
        dept:      user.dept,
        color:     user.color,
        doctorId:  user.doctorId || null,
        permissions: permissionsForRole(resolvedRole),
        remember:  !!remember,
        issuedAt:  now,
        expiresAt: now + (remember ? REMEMBER_MS : SHIFT_MS),
      };

      // Persist to exactly one store; clear the other to avoid stale copies.
      (remember ? sessionStorage : localStorage).removeItem(KEY);
      store(remember).setItem(KEY, JSON.stringify(session));
      localStorage.setItem(LEGACY_KEY, resolvedRole); // back-compat for existing pages
      return { ok: true, session };
    },

    /* Current session if present AND unexpired; otherwise null (and cleans up). */
    session() {
      const s = readRaw();
      if (!s) return null;
      if (Date.now() > s.expiresAt) { this.logout(); return null; }
      return s;
    },

    isAuthenticated() { return !!this.session(); },

    /* Slide the expiry forward on activity (called by the route guard). */
    touch() {
      const s = this.session();
      if (!s) return;
      s.expiresAt = Date.now() + (s.remember ? REMEMBER_MS : SHIFT_MS);
      store(s.remember).setItem(KEY, JSON.stringify(s));
    },

    /* Permission helpers ------------------------------------- */
    can(module, action) {
      const s = this.session();
      const lvl = (s && s.permissions[module]) || "None";
      if (action === "view") return lvl !== "None";
      if (action === "edit") return lvl === "Edit" || lvl === "Full";
      if (action === "full") return lvl === "Full";
      return false;
    },
    canAccess(module) { return this.can(module, "view"); },
    levelFor(module) { const s = this.session(); return (s && s.permissions[module]) || "None"; },

    /* ----------------------------------------------------------
       Working date — the clinic "as-of" date chosen at sign-in.
       Defaults to today; changeable in-app from the top bar.
       Stored in localStorage and stamped onto the live session.
    ---------------------------------------------------------- */
    workingDate() {
      return localStorage.getItem("ankuran_workdate") || new Date().toISOString().slice(0, 10);
    },
    setWorkingDate(d) {
      if (!d) return;
      localStorage.setItem("ankuran_workdate", d);
      const s = readRaw();
      if (s) { s.workingDate = d; store(s.remember).setItem(KEY, JSON.stringify(s)); }
    },

    /* Tear down everything and (optionally) bounce to login. */
    logout(redirect) {
      localStorage.removeItem(KEY);
      sessionStorage.removeItem(KEY);
      localStorage.removeItem(LEGACY_KEY);
      if (redirect) window.location.href = "login.html";
    },

    /* Guard a protected page: no/expired session -> login.html.
       Returns the live session when access is granted. */
    requireAuth() {
      const s = this.session();
      if (!s) { window.location.replace("login.html"); return null; }
      this.touch();
      return s;
    },
  };

  Auth.roleFromPrefix = roleFromPrefix;
  window.AnkuranAuth = Auth;
})();
