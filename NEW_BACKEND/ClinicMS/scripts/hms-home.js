/* ============================================================
   ANKURAN HMS — Home dashboard (legacy module-panel layout)
   Replaces P.home with the 8-module launcher from the legacy
   landing screen, re-skinned. Also registers the virtual pages
   that the launcher links to (services, appointment, etc.).
   The left rail + contextual sidebar are unchanged.
   ============================================================ */
(function () {
  "use strict";
  const P = window.HMS_PAGES;
  const esc = ui.esc;
  const field = window.HMS_field, table = window.HMS_table, pageHead = window.HMS_pageHead;
  const master = window.HMS_master, report = window.HMS_report, txn = window.HMS_txn;
  const inr = n => "₹" + Number(n || 0).toLocaleString("en-IN");

  /* ---- virtual item registry (so launcher links route + render) ---- */
  window.HMS_VITEMS = window.HMS_VITEMS || {};
  function V(id, label, page, module, src) { window.HMS_VITEMS[id] = { id, label, page, module, src }; }

  const PATIENTS = [
    { reg: "REG-100231", name: "Priya Sharma", doctor: "Dr. Anjali Mehta" },
    { reg: "REG-100230", name: "Meera Patel", doctor: "Dr. Vikram Sen" },
    { reg: "REG-100229", name: "Imran Sheikh", doctor: "Dr. Rohan Kapoor" },
  ];
  const DEPTS = ["Radiology / USG", "Foetal Medicine", "Reproductive Medicine", "General"];
  const ACTIVE = ["Active", "Inactive"];

  /* ---- service master engine (Imaging / Consultancy / Foetal / IVF) ---- */
  function serviceSpec(label, prefix, rows) {
    return {
      subtitle: label, codePrefix: prefix, codeLabel: "Service Code", formTitle: "New service",
      listTitle: label + " — list",
      fields: [
        { label: "Service Name", name: "name", required: true, placeholder: "e.g. " + (rows[0] ? rows[0].name : "Service") },
        { label: "Department", name: "dept", type: "select", options: DEPTS },
        { label: "Rate (₹)", name: "rate", inputmode: "numeric", placeholder: "0" },
        { label: "Status", name: "status", type: "select", options: ACTIVE },
      ],
      columns: [
        { key: "code", label: "Code", id: true }, { key: "name", label: "Service", main: true },
        { key: "dept", label: "Department" }, { key: "rate", label: "Rate", num: true }, { key: "status", label: "Status" },
      ],
      rows,
    };
  }

  P.imagingService = (h, it) => master(h, it, serviceSpec("Investigation · imaging services", "IMG", [
    { code: "IMG01", name: "TVS (Transvaginal Sonography)", dept: "Radiology / USG", rate: "1200", status: "Active" },
    { code: "IMG02", name: "Follicular Study (per scan)", dept: "Radiology / USG", rate: "900", status: "Active" },
    { code: "IMG03", name: "Anomaly Scan", dept: "Radiology / USG", rate: "2500", status: "Active" },
    { code: "IMG04", name: "Doppler Study", dept: "Radiology / USG", rate: "1800", status: "Active" },
  ]));
  P.consultancyService = (h, it) => master(h, it, serviceSpec("Investigation · consultancy services", "CON", [
    { code: "CON01", name: "New Consultation", dept: "Reproductive Medicine", rate: "800", status: "Active" },
    { code: "CON02", name: "Follow-up Consultation", dept: "Reproductive Medicine", rate: "500", status: "Active" },
    { code: "CON03", name: "Counselling Session", dept: "General", rate: "600", status: "Active" },
  ]));
  P.foetalService = (h, it) => master(h, it, serviceSpec("Investigation · foetal medicine services", "FMS", [
    { code: "FMS01", name: "NT Scan", dept: "Foetal Medicine", rate: "2200", status: "Active" },
    { code: "FMS02", name: "Foetal Echo", dept: "Foetal Medicine", rate: "3000", status: "Active" },
    { code: "FMS03", name: "Amniocentesis", dept: "Foetal Medicine", rate: "5500", status: "Active" },
  ]));
  P.infertilityTreatment = (h, it) => master(h, it, serviceSpec("Procedure & IVF · infertility treatment", "IVF", [
    { code: "IVF01", name: "IUI (Intrauterine Insemination)", dept: "Reproductive Medicine", rate: "12000", status: "Active" },
    { code: "IVF02", name: "IVF — Antagonist Protocol", dept: "Reproductive Medicine", rate: "185000", status: "Active" },
    { code: "IVF03", name: "ICSI", dept: "Reproductive Medicine", rate: "210000", status: "Active" },
    { code: "IVF04", name: "Frozen Embryo Transfer (FET)", dept: "Reproductive Medicine", rate: "65000", status: "Active" },
    { code: "IVF05", name: "Egg Freezing", dept: "Reproductive Medicine", rate: "95000", status: "Active" },
  ]));
  P.miscService = (h, it) => master(h, it, serviceSpec("Procedure & IVF · miscellaneous services", "MSC", [
    { code: "MSC01", name: "Semen Freezing", dept: "Reproductive Medicine", rate: "18000", status: "Active" },
    { code: "MSC02", name: "Embryo Storage (per year)", dept: "Reproductive Medicine", rate: "30000", status: "Active" },
    { code: "MSC03", name: "Day-care Charges", dept: "General", rate: "2500", status: "Active" },
  ]));

  /* ---- Patient Appointment ---- */
  P.appointment = function (host, item) {
    const APPTS = [
      { time: "09:30", reg: "REG-100231", name: "Priya Sharma", doctor: "Dr. Anjali Mehta", type: "Follow-up", status: "Confirmed" },
      { time: "10:00", reg: "REG-100230", name: "Meera Patel", doctor: "Dr. Vikram Sen", type: "New", status: "Confirmed" },
      { time: "10:30", reg: "REG-100229", name: "Imran Sheikh", doctor: "Dr. Rohan Kapoor", type: "Counselling", status: "Waiting" },
      { time: "11:15", reg: "REG-100227", name: "Aisha Khan", doctor: "Dr. Vikram Sen", type: "Follow-up", status: "Confirmed" },
    ];
    host.innerHTML = `
      ${pageHead("Patient Appointment", "Registration · book & manage appointments", `<span class="badge badge--accent" style="padding:7px 12px">${icon("calendar", 14)} Today · ${APPTS.length} booked</span>`)}
      <div class="grid-main" style="grid-template-columns:400px 1fr;align-items:start;gap:24px">
        <form class="card" id="apptForm" novalidate>
          <div class="card-head"><h3>New appointment</h3></div>
          <div class="card-pad"><div class="lform-grid" style="grid-template-columns:1fr">
            ${field({ label: "Registration No", name: "regno", required: true, type: "rowbtn", placeholder: "REG-…", buttons: [{ act: "fetch", label: "Fetch", icon: "download", cls: "btn-soft" }] })}
            ${field({ label: "Patient Name", name: "pname", disabled: true })}
            ${field({ label: "Consultant", name: "doctor", required: true, type: "select", options: ["Dr. Anjali Mehta", "Dr. Rohan Kapoor", "Dr. Vikram Sen", "Dr. Sneha Iyer"] })}
            ${field({ label: "Appointment Date", name: "date", required: true, type: "date" })}
            ${field({ label: "Time Slot", name: "time", required: true, type: "select", options: ["09:00", "09:30", "10:00", "10:30", "11:00", "11:30", "12:00", "16:00", "16:30", "17:00"] })}
            ${field({ label: "Visit Type", name: "type", type: "select", options: ["New", "Follow-up", "Counselling", "Procedure"] })}
            ${field({ label: "Notes", name: "notes", type: "textarea", rows: 2, placeholder: "Optional" })}
          </div></div>
          <div class="modal__foot" style="border-top:1px solid var(--border)"><button type="reset" class="btn btn-ghost">Cancel</button><button type="submit" class="btn btn-primary">${icon("check", 16)} Book appointment</button></div>
        </form>
        <div class="card">
          <div class="card-head"><h3>Today's appointments</h3><div class="sub">${APPTS.length} scheduled</div></div>
          ${table([{ key: "time", label: "Time", id: true }, { key: "reg", label: "Reg No" }, { key: "name", label: "Patient", main: true }, { key: "doctor", label: "Consultant" }, { key: "type", label: "Type" }, { key: "status", label: "Status" }], APPTS)}
        </div>
      </div>`;
    const f = host.querySelector("#apptForm");
    host.querySelector('[data-act="fetch"]').addEventListener("click", () => {
      const v = (f.regno.value || "").trim();
      const p = PATIENTS.find(x => x.reg.toLowerCase() === v.toLowerCase()) || PATIENTS[0];
      f.regno.value = p.reg; f.pname.value = p.name; f.doctor.value = p.doctor;
      ui.toast("Loaded " + p.name, "download");
    });
    f.addEventListener("submit", e => {
      e.preventDefault();
      for (const [n, msg] of [["regno", "Enter Registration No"], ["doctor", "Select a consultant"], ["date", "Select a date"], ["time", "Select a time slot"]]) {
        const el = f.elements[n]; if (!String(el.value).trim()) { el.classList.add("err"); el.focus(); ui.toast(msg, "alert"); el.addEventListener("input", () => el.classList.remove("err"), { once: true }); el.addEventListener("change", () => el.classList.remove("err"), { once: true }); return; }
      }
      ui.toast("Appointment booked · " + f.time.value, "calendar"); f.reset();
    });
  };

  /* ---- Sales Return (transaction) ---- */
  const ITEMS = { "Gonal-F 75 IU": 1150, "Menopur 75 IU": 990, "Folvite 5 mg": 28, "Augmentin 625": 210 };
  function patientHeaderHTML() {
    return `
      ${field({ label: "Registration No", name: "regno", type: "rowbtn", placeholder: "REG-…", buttons: [{ act: "fetch", label: "Fetch", icon: "download", cls: "btn-soft" }] })}
      ${field({ label: "Patient Name", name: "pname", disabled: true })}
      ${field({ label: "Age", name: "page", disabled: true })}
      ${field({ label: "Sex", name: "psex", disabled: true })}
      ${field({ label: "Consultant", name: "pdoctor", disabled: true })}`;
  }
  P.salesReturn = (h, it) => txn(h, it, {
    subtitle: "Pharmacy · Transaction", icon: "refresh", docLabel: "Sales Return", docNo: "SR-" + 89,
    headerTitle: "Patient", header: patientHeaderHTML(),
    lineNoun: "item", gridTitle: "Returned items",
    totals: (lines) => { const s = lines.reduce((a, l) => a + (l._amt || 0), 0); return `<div style="display:flex;justify-content:flex-end"><div style="text-align:right"><div style="color:var(--text-3);font-size:12px">Refund total</div><div style="font-weight:800;color:var(--accent-strong);font-size:18px">${inr(s)}</div></div></div>`; },
    lineFields: [
      { label: "Item", name: "item", required: true, type: "select", options: Object.keys(ITEMS) },
      { label: "Batch", name: "batch", placeholder: "Batch" },
      { label: "Qty", name: "qty", inputmode: "numeric", placeholder: "0" },
      { label: "Reason", name: "reason", placeholder: "Damaged / Wrong item" },
    ],
    mapLine: o => { const qty = +o.qty || 0, rate = ITEMS[o.item] || 0; return { item: o.item, batch: o.batch, qty, rate: inr(rate), reason: o.reason, amount: inr(qty * rate), _amt: qty * rate }; },
    lineCols: [{ key: "item", label: "Item", main: true }, { key: "batch", label: "Batch" }, { key: "qty", label: "Qty", num: true }, { key: "rate", label: "Rate", num: true }, { key: "reason", label: "Reason" }, { key: "amount", label: "Refund", num: true }],
  });

  /* ---- A/c & MIS reports ---- */
  P.dischargeBill = (h, it) => report(h, it, {
    subtitle: "A/c Reports · discharge bill details",
    kpis: [
      { label: "Bills", value: 4, icon: "receipt", color: "#3e72ad", soft: "#e6eef7" },
      { label: "Billed", value: "₹3,68,000", icon: "rupee", color: "#1f9268", soft: "#e2f3ec" },
      { label: "Collected", value: "₹3,21,000", icon: "check", color: "#6a59c2", soft: "#eae7f8" },
      { label: "Due", value: "₹47,000", icon: "clock", color: "#b9772b", soft: "#f8edda" },
    ],
    columns: [{ key: "bill", label: "Bill No", id: true }, { key: "reg", label: "Reg No" }, { key: "name", label: "Patient", main: true }, { key: "discharge", label: "Discharge" }, { key: "total", label: "Total", num: true }, { key: "paid", label: "Paid", num: true }, { key: "status", label: "Status" }],
    actions: false,
    rows: [
      { bill: "DB-2201", reg: "REG-100231", name: "Priya Sharma", discharge: "02 Jun 2026", total: "₹1,85,000", paid: "₹1,85,000", status: "Paid" },
      { bill: "DB-2202", reg: "REG-100230", name: "Meera Patel", discharge: "01 Jun 2026", total: "₹36,000", paid: "₹24,000", status: "Partial" },
      { bill: "DB-2203", reg: "REG-100227", name: "Aisha Khan", discharge: "29 May 2026", total: "₹65,000", paid: "₹65,000", status: "Paid" },
      { bill: "DB-2204", reg: "REG-100229", name: "Imran Sheikh", discharge: "28 May 2026", total: "₹82,000", paid: "₹47,000", status: "Partial" },
    ],
  });
  P.stockValuation = (h, it) => report(h, it, {
    subtitle: "A/c Reports · stock valuation",
    columns: [{ key: "code", label: "Code", id: true }, { key: "name", label: "Item", main: true }, { key: "grp", label: "Group" }, { key: "qty", label: "Qty", num: true }, { key: "rate", label: "Rate", num: true }, { key: "value", label: "Value", num: true }],
    actions: false,
    rows: [
      { code: "MED01", name: "Gonal-F 75 IU", grp: "Hormones", qty: 24, rate: "₹1,150", value: "₹27,600" },
      { code: "MED02", name: "Menopur 75 IU", grp: "Hormones", qty: 8, rate: "₹990", value: "₹7,920" },
      { code: "MED03", name: "Folvite 5 mg", grp: "Supplements", qty: 120, rate: "₹28", value: "₹3,360" },
      { code: "MED04", name: "Augmentin 625", grp: "Antibiotics", qty: 16, rate: "₹210", value: "₹3,360" },
    ],
  });
  P.expiryAlert = (h, it) => report(h, it, {
    subtitle: "MIS Reports · expiry alert",
    kpis: [
      { label: "Expiring ≤30d", value: 1, icon: "alert", color: "#c4453f", soft: "#f9e6e4" },
      { label: "Expiring ≤90d", value: 2, icon: "clock", color: "#b9772b", soft: "#f8edda" },
      { label: "Expired", value: 0, icon: "trash", color: "#6a6f7a", soft: "#eceef1" },
    ],
    columns: [{ key: "code", label: "Code", id: true }, { key: "name", label: "Item", main: true }, { key: "batch", label: "Batch" }, { key: "expiry", label: "Expiry" }, { key: "days", label: "Days Left", num: true }, { key: "stock", label: "Stock", num: true }],
    actions: false,
    rows: [
      { code: "MED04", name: "Augmentin 625", batch: "AG2404", expiry: "Aug 2026", days: 65, stock: 16 },
      { code: "MED03", name: "Folvite 5 mg", batch: "FV2312", expiry: "Dec 2026", days: 178, stock: 120 },
      { code: "MED02", name: "Menopur 75 IU", batch: "MP2405", expiry: "Jan 2027", days: 210, stock: 8 },
    ],
  });

  /* ---- register virtual items (id → page + module context) ---- */
  V("v-appt", "Patient Appointment", "appointment", "opd", "OPD/Appointment.aspx");
  V("v-imaging", "Imaging Service", "imagingService", "investigation", "Investigation/ImagingService.aspx");
  V("v-consult", "Consultancy Service", "consultancyService", "investigation", "Investigation/ConsultancyService.aspx");
  V("v-foetal", "Foetal Medicine Services", "foetalService", "investigation", "Investigation/FoetalService.aspx");
  V("v-infertility", "Infertility Treatment", "infertilityTreatment", "investigation", "IVF/InfertilityTreatment.aspx");
  V("v-misc", "Miscellaneous Service", "miscService", "investigation", "IVF/MiscService.aspx");
  V("v-salesret", "Sales Return", "salesReturn", "pharmacy", "Medicine/SalesReturn.aspx");
  V("v-discharge", "Discharge Bill Details", "dischargeBill", "accounts", "Bill/DischargeBillDetails.aspx");
  V("v-stockval", "Stock Valuation", "stockValuation", "pharmacy", "Medicine/StockValuation.aspx");
  V("v-expiry", "Expiry Alert Report", "expiryAlert", "pharmacy", "Medicine/ExpiryAlert.aspx");

  /* ============================================================
     HOME — 8-module launcher (matches the legacy landing screen)
     ============================================================ */
  const HOME = [
    { title: "Admin/Master", icon: "shield", color: "#9d5c63", soft: "#f3e7e8", items: [
      ["User Role", "sec-role"], ["User Creation", "sec-user"], ["Change Password", "sec-pwd"], ["User Action", "sec-action"], ["Data Consistency", "util-dc"],
    ]},
    { title: "Registration", icon: "userPlus", color: "#4a7aa7", soft: "#e6eef5", items: [
      ["Patient Registration", "opd-reg"], ["Patient Appointment", "v-appt"], ["Patient Details", "opd-list"], ["Patient Enrollment", "opd-reg"], ["Patient Requisition", "inv-reqlist"], ["Investigation Bill Entry", "inv-reqbill"],
    ]},
    { title: "Investigation", icon: "flask", color: "#7a6aa8", soft: "#ece8f4", items: [
      ["Imaging Service", "v-imaging"], ["Consultancy Service", "v-consult"], ["Foetal Medicine Services", "v-foetal"],
    ]},
    { title: "Procedure & IVF", icon: "stethoscope", color: "#2f8f6b", soft: "#e4f1ea", items: [
      ["Infertility Treatment", "v-infertility"], ["Miscellaneous Service", "v-misc"],
    ]},
    { title: "Pathology/Pharma", icon: "pill", color: "#c4843b", soft: "#f8ecdb", items: [
      ["Supplier Entry", "ph-supp"], ["Manufacturer Entry", "ph-manuf"], ["Medicine Master", "ph-master"], ["Medicine Issue Entry", "ph-issue"],
    ]},
    { title: "Stores", icon: "server", color: "#5f8a8b", soft: "#e3eded", items: [
      ["Medicine Purchase", "ph-purch"], ["Purchase Return", "ph-return"], ["Medicine Sales", "ph-issue"], ["Sales Return", "v-salesret"], ["Invoice Report", "ph-sales"], ["Stock Maintain", "ph-stock"],
    ]},
    { title: "A/c Reports", icon: "receipt", color: "#a76b4f", soft: "#f1e7e0", items: [
      ["Discharge Bill Details", "v-discharge"], ["Money Receipt", "ac-advance"], ["Doctor Payment Report", "ac-docmis"], ["Stock Movement", "ph-movement"], ["Stock Ledger Details", "ph-ledger"], ["Stock Valuation", "v-stockval"],
    ]},
    { title: "MIS Reports", icon: "chart", color: "#b5707a", soft: "#f4e8ea", items: [
      ["Investigation Group wise Collection", "inv-grpcoll"], ["Sample wise collection", "inv-coll"], ["Stock End Report", "ph-stock"], ["Expiry Alert Report", "v-expiry"], ["Medicine Stock Details", "ph-stock"],
    ]},
  ];
  window.HMS_HOME = HOME;

  function greeting() {
    const h = new Date().getHours();
    return h < 12 ? "Good morning" : h < 17 ? "Good afternoon" : "Good evening";
  }
  function getSession() {
    try { return JSON.parse(localStorage.getItem("ankuran_session")) || JSON.parse(sessionStorage.getItem("ankuran_session")) || null; } catch (e) { return null; }
  }
  function sessionName() {
    const s = getSession();
    if (s && s.name) return s.name;        // whoever signed in (keeps the Dr. prefix)
    return "there";
  }
  function roleLabel() {
    if (window.HMS_ROLE_LABEL) return HMS_ROLE_LABEL();
    const s = getSession();
    return (s && s.role) || "Team member";
  }
  function userTitle() {
    const s = getSession();
    return (s && s.title) ? s.title : roleLabel();
  }

  P.home = function (host) {
    const yr = (() => { try { return localStorage.getItem("ankuran_year") || ""; } catch (e) { return ""; } })();
    host.innerHTML = `
      <div class="hl-banner">
        <div class="hl-banner__t">
          <h1>${greeting()}, <em>${esc(sessionName())}</em></h1>
          <p>Welcome to Ankuran. Pick a module below to jump straight into any form, entry or report.</p>
          <div class="hl-banner__row">
            <span class="badge badge--accent" style="padding:7px 12px">${icon("shield", 14)} ${esc(userTitle())}</span>
            ${yr ? `<span class="badge badge--neutral" style="padding:7px 12px">${icon("chart", 14)} FY ${esc(yr)}</span>` : ""}
            <span class="badge badge--neutral" style="padding:7px 12px">${icon("calendar", 14)} ${new Date().toLocaleDateString("en-IN", { day: "2-digit", month: "short", year: "numeric" })}</span>
          </div>
        </div>
        <div class="hl-banner__pic clinic-frame" aria-label="Ankuran clinic">
          <svg class="clinic-illo" viewBox="0 0 320 168" preserveAspectRatio="xMidYMid slice" role="img" aria-label="Animated illustration of the Ankuran clinic">
            <defs>
              <linearGradient id="cl-sky" x1="0" y1="0" x2="0" y2="1">
                <stop offset="0" stop-color="#dcebfd"/><stop offset="1" stop-color="#f3f8ff"/>
              </linearGradient>
              <linearGradient id="cl-build" x1="0" y1="0" x2="0" y2="1">
                <stop offset="0" stop-color="#ffffff"/><stop offset="1" stop-color="#eef3fb"/>
              </linearGradient>
              <radialGradient id="cl-sun" cx="0.5" cy="0.5" r="0.5">
                <stop offset="0" stop-color="#ffd874"/><stop offset="1" stop-color="#ffb347"/>
              </radialGradient>
            </defs>

            <rect x="0" y="0" width="320" height="168" fill="url(#cl-sky)"/>
            <circle class="illo-sun" cx="270" cy="40" r="20" fill="url(#cl-sun)"/>

            <g class="illo-cloud illo-cloud1" fill="#ffffff" opacity="0.92">
              <ellipse cx="60" cy="38" rx="20" ry="11"/><ellipse cx="78" cy="34" rx="15" ry="12"/><ellipse cx="44" cy="42" rx="14" ry="9"/>
            </g>
            <g class="illo-cloud illo-cloud2" fill="#ffffff" opacity="0.8">
              <ellipse cx="190" cy="26" rx="16" ry="9"/><ellipse cx="205" cy="23" rx="12" ry="10"/><ellipse cx="178" cy="29" rx="11" ry="7"/>
            </g>

            <!-- ground -->
            <rect x="0" y="132" width="320" height="36" fill="#dfe8d8"/>
            <rect x="0" y="132" width="320" height="5" fill="#cfe0c4"/>

            <!-- trees -->
            <g class="illo-tree" style="--d:0s">
              <rect x="26" y="112" width="5" height="22" rx="2" fill="#9a7b53"/>
              <circle cx="28.5" cy="108" r="13" fill="#3f9c74"/><circle cx="20" cy="114" r="9" fill="#46a87e"/><circle cx="37" cy="114" r="9" fill="#379069"/>
            </g>
            <g class="illo-tree" style="--d:.8s">
              <rect x="291" y="114" width="5" height="20" rx="2" fill="#9a7b53"/>
              <circle cx="293.5" cy="110" r="11" fill="#3f9c74"/><circle cx="286" cy="115" r="8" fill="#46a87e"/>
            </g>

            <!-- building -->
            <g class="illo-building">
              <rect x="96" y="64" width="128" height="70" rx="6" fill="url(#cl-build)" stroke="#d6e2f2" stroke-width="1.5"/>
              <!-- roof band -->
              <rect x="96" y="64" width="128" height="14" rx="6" fill="#1b76e5"/>
              <rect x="96" y="74" width="128" height="5" fill="#155fc0"/>
              <!-- windows -->
              <g fill="#cfe4fb" stroke="#bcd6f4" stroke-width="1">
                <rect class="illo-win" x="108" y="88" width="20" height="16" rx="2" style="--d:0s"/>
                <rect class="illo-win" x="136" y="88" width="20" height="16" rx="2" style="--d:.5s"/>
                <rect class="illo-win" x="192" y="88" width="20" height="16" rx="2" style="--d:1s"/>
              </g>
              <!-- door -->
              <rect x="150" y="106" width="20" height="28" rx="2" fill="#1b76e5"/>
              <rect x="150" y="106" width="20" height="28" rx="2" fill="none" stroke="#155fc0" stroke-width="1"/>
              <circle cx="166" cy="120" r="1.6" fill="#fff"/>
              <!-- awning -->
              <path d="M146 106 h28 l-4 6 h-20 z" fill="#2f8f6b"/>
            </g>

            <!-- cross sign on a pole -->
            <g class="illo-sign">
              <rect x="159" y="40" width="2.5" height="24" fill="#9fb2cb"/>
              <g class="illo-cross">
                <rect x="146" y="24" width="28" height="24" rx="5" fill="#ffffff" stroke="#e7edf6" stroke-width="1.5"/>
                <rect x="158" y="29" width="4" height="14" rx="1.4" fill="#c4453f"/>
                <rect x="153" y="34" width="14" height="4" rx="1.4" fill="#c4453f"/>
              </g>
            </g>

            <!-- ECG heartbeat band -->
            <g>
              <rect x="0" y="120" width="320" height="14" fill="#0c2440" opacity="0.06"/>
              <path class="illo-ecg" d="M0 127 H120 l6 -9 l6 18 l6 -22 l7 26 l5 -13 H320" fill="none" stroke="#1b76e5" stroke-width="2" stroke-linecap="round" stroke-linejoin="round"/>
            </g>

            <!-- floating plus marks -->
            <g fill="#1b76e5" opacity="0.5">
              <g class="illo-plus" style="--d:0s"><rect x="118" y="50" width="3" height="9" rx="1"/><rect x="115" y="53" width="9" height="3" rx="1"/></g>
              <g class="illo-plus" style="--d:1.3s"><rect x="232" y="60" width="3" height="9" rx="1"/><rect x="229" y="63" width="9" height="3" rx="1"/></g>
              <g class="illo-plus" style="--d:2.1s"><rect x="210" y="44" width="2.4" height="7" rx="1"/><rect x="207.5" y="46.5" width="7" height="2.4" rx="1"/></g>
            </g>
          </svg>
        </div>
      </div>

      <div class="hl-sec"><h2>Clinic Modules</h2></div>
      <div class="hl-modstrip">
        ${HOME.map((m, i) => {
          const opts = m.items.map(([label, go]) => ({ label, go, ok: (window.HMS_ITEM_ALLOWED ? HMS_ITEM_ALLOWED(go) : true) }));
          const anyOk = opts.some(o => o.ok);
          return `
          <div class="hl-mod ${anyOk ? "" : "locked"}" data-mod="${i}" style="--mc:${m.color};--mc-sf:${m.soft}">
            <button class="hl-mod__btn" type="button" aria-expanded="false" title="${esc(m.title)}">
              <span class="hl-mod__ic">${icon(m.icon, 22)}</span>
              <span class="hl-mod__t">${esc(m.title)}</span>
              <span class="hl-mod__cx">${anyOk ? icon("chevDown", 14) : icon("lock", 13)}</span>
            </button>
            <div class="hl-mod__panel" role="menu">
              ${opts.map(o => o.ok
                ? `<a class="hl-mod__opt" role="menuitem" data-go="${o.go}"><span class="cv">${icon("chevRight", 14)}</span><span class="lb">${esc(o.label)}</span></a>`
                : `<span class="hl-mod__opt locked" aria-disabled="true" title="Not available for your role"><span class="cv">${icon("lock", 12)}</span><span class="lb">${esc(o.label)}</span></span>`
              ).join("")}
            </div>
          </div>`;
        }).join("")}
      </div>

      <div class="hfooter">Ankuran &nbsp;·&nbsp; Care · Compassion · Cure &nbsp;·&nbsp; Kolkata &nbsp;·&nbsp; Helpline 098765 43210</div>`;

    host.querySelectorAll("[data-go]").forEach(a => a.addEventListener("click", (e) => { e.stopPropagation(); window.HMS_GO(a.dataset.go); }));

    const strip = host.querySelector(".hl-modstrip");
    const closeAll = (except) => strip.querySelectorAll(".hl-mod.open").forEach(t => {
      if (t !== except) { t.classList.remove("open"); const b = t.querySelector(".hl-mod__btn"); if (b) b.setAttribute("aria-expanded", "false"); }
    });
    strip.querySelectorAll(".hl-mod__btn").forEach(btn => btn.addEventListener("click", (e) => {
      e.stopPropagation();
      const tile = btn.closest(".hl-mod");
      if (tile.classList.contains("locked")) { if (window.ui && ui.toast) ui.toast("This module is not available for your role", "alert"); return; }
      const willOpen = !tile.classList.contains("open");
      closeAll(tile);
      tile.classList.toggle("open", willOpen);
      btn.setAttribute("aria-expanded", willOpen ? "true" : "false");
    }));
    if (!window.__hmsHomeMenuBound) {
      document.addEventListener("click", () => document.querySelectorAll(".hl-mod.open").forEach(t => { t.classList.remove("open"); const b = t.querySelector(".hl-mod__btn"); if (b) b.setAttribute("aria-expanded", "false"); }));
      document.addEventListener("keydown", (e) => { if (e.key === "Escape") document.querySelectorAll(".hl-mod.open").forEach(t => t.classList.remove("open")); });
      window.__hmsHomeMenuBound = true;
    }

    // honour a pending "open this tile" request (set by Alice)
    if (window.__hmsOpenTile != null) {
      const want = window.__hmsOpenTile; window.__hmsOpenTile = null;
      const tile = strip.querySelector('.hl-mod[data-mod="' + want + '"]');
      if (tile && !tile.classList.contains("locked")) {
        closeAll(tile); tile.classList.add("open");
        const b = tile.querySelector(".hl-mod__btn"); if (b) b.setAttribute("aria-expanded", "true");
        try { tile.scrollIntoView({ block: "nearest" }); } catch (e) {}
      }
    }
  };

  /* Open the Main Menu and pop a specific clinic-module tile open.
     Accepts a tile title (e.g. "Stores") or its index. Used by Alice. */
  window.HMS_OPEN_TILE = function (which) {
    let idx = -1;
    if (typeof which === "number") idx = which;
    else {
      const t = String(which || "").toLowerCase().trim();
      idx = HOME.findIndex(h => h.title.toLowerCase() === t);
      if (idx === -1) idx = HOME.findIndex(h => h.title.toLowerCase().indexOf(t) !== -1);
    }
    if (idx === -1) return false;
    window.__hmsOpenTile = idx;
    if (window.HMS_GO) window.HMS_GO("home"); else location.hash = "#home";
    // retry a few times in case the home re-render lands slightly later
    let tries = 0;
    const timer = setInterval(() => {
      tries++;
      const strip = document.querySelector(".hl-modstrip");
      const tile = strip && strip.querySelector('.hl-mod[data-mod="' + idx + '"]');
      if (tile) {
        if (!tile.classList.contains("locked") && !tile.classList.contains("open")) {
          document.querySelectorAll(".hl-mod.open").forEach(t => t.classList.remove("open"));
          tile.classList.add("open");
          const b = tile.querySelector(".hl-mod__btn"); if (b) b.setAttribute("aria-expanded", "true");
        }
        window.__hmsOpenTile = null;
        clearInterval(timer);
      }
      if (tries > 12) { window.__hmsOpenTile = null; clearInterval(timer); }
    }, 60);
    return true;
  };

})();
