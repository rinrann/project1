/* ============================================================
   ANKURAN HMS — Role-based access (implicit, prefix-driven)
   ------------------------------------------------------------
   • Role is detected IMPLICITLY from the signed-in user's
     Employee-ID prefix (DOC-… → Doctor, NUR-… → Nurse, …),
     falling back to the role stored in the session.
   • Each role unlocks a set of modules; everything else is
     greyed-out across the rail, sidebar, flyout, home tiles
     and is blocked at the router. Super Admin = no limits.
   • No session (e.g. opening the file directly) ⇒ full access
     so the build never looks broken in preview.
   ============================================================ */
(function () {
  "use strict";

  function readSession() {
    try {
      return JSON.parse(localStorage.getItem("ankuran_session")) ||
             JSON.parse(sessionStorage.getItem("ankuran_session")) || null;
    } catch (e) { return null; }
  }
  window.HMS_SESSION = readSession;

  /* Employee-ID prefix → role (the "implicit role detection") */
  const PREFIX_ROLE = {
    OWN: "Super Admin", SA: "Super Admin", SADMIN: "Super Admin",
    SUP: "Admin", ADM: "Admin", ADMIN: "Admin",
    DOC: "Doctor", DR: "Doctor",
    NUR: "Nurse",
    REC: "Receptionist", FO: "Receptionist",
    EMB: "Embryologist",
    ACC: "Accounts Team", FIN: "Accounts Team",
    MGT: "Management", MGR: "Management",
  };
  window.HMS_ROLE_FROM_ID = function (id) {
    const p = String(id || "").split(/[-_ ]/)[0].toUpperCase();
    return PREFIX_ROLE[p] || null;
  };

  window.HMS_ROLE = function () {
    const s = readSession();
    if (!s) return "Super Admin"; // no login → preview with full access
    return HMS_ROLE_FROM_ID(s.userId || s.empId) || s.role || "Super Admin";
  };
  window.HMS_ROLE_LABEL = function () {
    const r = HMS_ROLE();
    return r === "Admin" ? "Administrator" : r;
  };

  /* Role → allowed HMS module ids. "*" = everything.
     home & dashboard are always allowed for every role. */
  const ROLE_MODULES = {
    "Super Admin":   "*",
    "Admin":         "*",
    "Doctor":        ["opd", "investigation", "pharmacy"],
    "Nurse":         ["opd", "investigation", "pharmacy"],
    "Receptionist":  ["opd", "accounts", "investigation"],
    "Embryologist":  ["opd", "investigation"],
    "Accounts Team": ["accounts", "pharmacy"],
    "Management":    ["accounts", "pharmacy", "investigation", "opd"],
  };
  window.HMS_ROLE_MODULES = ROLE_MODULES;

  window.HMS_MODULE_ALLOWED = function (modId) {
    if (modId === "home" || modId === "dashboard") return true;
    const allow = ROLE_MODULES[HMS_ROLE()];
    if (!allow || allow === "*") return true;
    return allow.indexOf(modId) !== -1;
  };

  window.HMS_ITEM_ALLOWED = function (itemId) {
    let mod = (window.HMS_MODULE_OF && HMS_MODULE_OF(itemId)) || null;
    if (!mod && window.HMS_VITEMS && HMS_VITEMS[itemId]) mod = HMS_VITEMS[itemId].module;
    if (!mod) return true;
    return HMS_MODULE_ALLOWED(mod);
  };
})();
