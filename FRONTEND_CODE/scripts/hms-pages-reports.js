/* ============================================================
   ANKURAN HMS — Report & list pages + remaining Security forms
   ============================================================ */
(function () {
  "use strict";
  const P = window.HMS_PAGES;
  const table = window.HMS_table, pageHead = window.HMS_pageHead, field = window.HMS_field;
  const esc = ui.esc;
  const col = (key, label, o) => Object.assign({ key, label }, o || {});

  /* ---- report engine: filter bar + (optional) KPIs + grid ---------- */
  function reportPage(host, item, spec) {
    host.innerHTML = `
      ${pageHead(item.label, spec.subtitle || "Report", `<button class="btn btn-ghost" id="rExport">${icon("download", 16)} Export</button>`)}
      ${spec.kpis ? `<div class="kpi-grid" style="margin-bottom:20px">${spec.kpis.map(k => ui.kpi(k)).join("")}</div>` : ""}
      <div class="card">
        <div class="card-head"><h3>${spec.listTitle || item.label}</h3><div class="sub">${spec.rows.length} records</div>
          <div class="actions"><div class="htop__search" style="width:220px">${icon("search", 15, "si")}<input id="rSearch" placeholder="Search…"></div></div>
        </div>
        ${spec.filters ? `<div class="card-pad" style="padding-bottom:8px"><div class="lfilter">${spec.filters.map(field).join("")}<button class="btn btn-primary" id="rGo">${icon("search", 15)} Search</button></div></div>` : ""}
        <div id="rGrid">${table(spec.columns, spec.rows, { actions: spec.actions })}</div>
      </div>`;
    const exp = host.querySelector("#rExport"); if (exp) exp.addEventListener("click", () => exportGridToCSV(host.querySelector("#rGrid table"), item.label));
    const go = host.querySelector("#rGo"); if (go) go.addEventListener("click", () => ui.toast("Filters applied", "filter"));
    const s = host.querySelector("#rSearch");
    if (s) s.addEventListener("input", () => {
      const t = s.value.toLowerCase();
      const rows = spec.rows.filter(r => spec.columns.some(c => String(r[c.key] || "").toLowerCase().includes(t)));
      host.querySelector("#rGrid").innerHTML = table(spec.columns, rows, { actions: spec.actions });
    });
  }
  window.HMS_report = reportPage;
  const rupee = n => "₹" + Number(n).toLocaleString("en-IN");

  /* ---- Real CSV export from a rendered table (works without backend) ---- */
  function exportGridToCSV(table, name) {
    if (!table) { ui.toast("Nothing to export", "alert"); return; }
    const rows = [...table.querySelectorAll("tr")];
    if (!rows.length) { ui.toast("Nothing to export", "alert"); return; }
    // detect a trailing "Action" column to drop it
    const headCells = [...rows[0].children];
    const dropLast = /action/i.test((headCells[headCells.length - 1] || {}).textContent || "");
    const csvOf = (tr) => {
      let cells = [...tr.children];
      if (dropLast) cells = cells.slice(0, cells.length - 1);
      return cells.map(td => {
        const txt = (td.innerText || td.textContent || "").replace(/\s+/g, " ").trim();
        return /[",\n]/.test(txt) ? `"${txt.replace(/"/g, '""')}"` : txt;
      }).join(",");
    };
    // skip an empty-state row
    const body = rows.filter(tr => !tr.querySelector(".empty"));
    if (body.length <= 1) { ui.toast("No records to export", "alert"); return; }
    const csv = "\uFEFF" + body.map(csvOf).join("\r\n"); // BOM so Excel reads ₹/UTF-8
    const blob = new Blob([csv], { type: "text/csv;charset=utf-8;" });
    const url = URL.createObjectURL(blob);
    const a = document.createElement("a");
    const fname = String(name || "export").replace(/[^\w]+/g, "_") + "_" + new Date().toISOString().slice(0, 10) + ".csv";
    a.href = url; a.download = fname;
    document.body.appendChild(a); a.click();
    setTimeout(() => { a.remove(); URL.revokeObjectURL(url); }, 200);
    ui.toast("Downloaded " + fname, "download");
  }
  window.HMS_exportCSV = exportGridToCSV;

  /* ---- OPD reports/lists ---- */
  const PATIENTS = [
    { reg: "REG-100231", name: "Priya Sharma", age: "34", sex: "F", ph: "9830042118", doctor: "Dr. Anjali Mehta", date: "02 Jun 2026", status: "Active" },
    { reg: "REG-100230", name: "Meera Patel", age: "31", sex: "F", ph: "9830042120", doctor: "Dr. Vikram Sen", date: "01 Jun 2026", status: "Active" },
    { reg: "REG-100229", name: "Imran Sheikh", age: "40", sex: "M", ph: "9830042121", doctor: "Dr. Rohan Kapoor", date: "31 May 2026", status: "Active" },
    { reg: "REG-100228", name: "Divya Nair", age: "29", sex: "F", ph: "9830042122", doctor: "Dr. Anjali Mehta", date: "30 May 2026", status: "Discharged" },
    { reg: "REG-100227", name: "Aisha Khan", age: "36", sex: "F", ph: "9830042124", doctor: "Dr. Vikram Sen", date: "29 May 2026", status: "Active" },
  ];

  P.patientList = (h, it) => reportPage(h, it, {
    subtitle: "OPD Unit · Transaction",
    filters: [field({ label: "Reg No", name: "f1", placeholder: "REG-…" }), field({ label: "Name", name: "f2", placeholder: "Name" }), field({ label: "Doctor", name: "f3", type: "select", options: ["Dr. Anjali Mehta", "Dr. Rohan Kapoor", "Dr. Vikram Sen"] })],
    columns: [col("reg", "Reg No", { id: true }), col("name", "Patient Name", { main: true }), col("age", "Age"), col("sex", "Sex"), col("ph", "Phone"), col("doctor", "Consultant"), col("date", "Reg Date"), col("status", "Status")],
    rows: PATIENTS,
  });

  P.patientQueue = (h, it) => reportPage(h, it, {
    subtitle: "OPD Unit · Reports — today's queue",
    kpis: [
      { label: "In queue", value: 6, icon: "users", color: "#3e72ad", soft: "#e6eef7" },
      { label: "Waiting", value: 3, icon: "clock", color: "#b9772b", soft: "#f8edda" },
      { label: "In consultation", value: 1, icon: "stethoscope", color: "#6a59c2", soft: "#eae7f8" },
      { label: "Completed", value: 12, icon: "check", color: "#1f9268", soft: "#e2f3ec" },
    ],
    columns: [col("token", "Token", { id: true }), col("reg", "Reg No"), col("name", "Patient", { main: true }), col("doctor", "Consultant"), col("time", "Time"), col("status", "Status")],
    rows: [
      { token: "T-12", reg: "REG-100231", name: "Priya Sharma", doctor: "Dr. Anjali Mehta", time: "09:10", status: "In consultation" },
      { token: "T-13", reg: "REG-100230", name: "Meera Patel", doctor: "Dr. Vikram Sen", time: "09:25", status: "Waiting" },
      { token: "T-14", reg: "REG-100227", name: "Aisha Khan", doctor: "Dr. Vikram Sen", time: "09:40", status: "Waiting" },
      { token: "T-15", reg: "REG-100229", name: "Imran Sheikh", doctor: "Dr. Rohan Kapoor", time: "10:00", status: "Waiting" },
    ],
  });

  P.patientHistory = (h, it) => reportPage(h, it, {
    subtitle: "OPD Unit · Reports — visit history",
    filters: [field({ label: "Reg No", name: "f1", placeholder: "REG-…" }), field({ label: "From", name: "f2", type: "date" }), field({ label: "To", name: "f3", type: "date" })],
    columns: [col("date", "Visit Date", { id: true }), col("reg", "Reg No"), col("name", "Patient", { main: true }), col("complaint", "Complaint"), col("doctor", "Consultant"), col("rx", "Prescription")],
    rows: [
      { date: "02 Jun 2026", reg: "REG-100231", name: "Priya Sharma", complaint: "Irregular cycle", doctor: "Dr. Anjali Mehta", rx: "IUI Follow-up" },
      { date: "20 May 2026", reg: "REG-100231", name: "Priya Sharma", complaint: "Follow-up", doctor: "Dr. Anjali Mehta", rx: "ANC Routine" },
      { date: "01 Jun 2026", reg: "REG-100230", name: "Meera Patel", complaint: "Abdominal pain", doctor: "Dr. Vikram Sen", rx: "General" },
    ],
  });

  const INVOICES = [
    { inv: "INV-5001", reg: "REG-100231", name: "Priya Sharma", date: "02 Jun 2026", total: 4800, paid: 4800, status: "Paid" },
    { inv: "INV-5002", reg: "REG-100230", name: "Meera Patel", date: "01 Jun 2026", total: 2650, paid: 1000, status: "Partial" },
    { inv: "INV-5003", reg: "REG-100229", name: "Imran Sheikh", date: "31 May 2026", total: 1800, paid: 0, status: "Overdue" },
    { inv: "INV-5004", reg: "REG-100227", name: "Aisha Khan", date: "29 May 2026", total: 3500, paid: 3500, status: "Paid" },
  ];
  P.patientInvoiceList = (h, it) => reportPage(h, it, {
    subtitle: "OPD Unit · Reports — invoices",
    kpis: [
      { label: "Total billed", value: rupee(INVOICES.reduce((s, i) => s + i.total, 0)), icon: "receipt", color: "#3e72ad", soft: "#e6eef7" },
      { label: "Collected", value: rupee(INVOICES.reduce((s, i) => s + i.paid, 0)), icon: "check", color: "#1f9268", soft: "#e2f3ec" },
      { label: "Outstanding", value: rupee(INVOICES.reduce((s, i) => s + (i.total - i.paid), 0)), icon: "clock", color: "#b9772b", soft: "#f8edda" },
      { label: "Overdue", value: INVOICES.filter(i => i.status === "Overdue").length, icon: "alert", color: "#c4453f", soft: "#f9e6e4" },
    ],
    columns: [col("inv", "Invoice No", { id: true }), col("reg", "Reg No"), col("name", "Patient", { main: true }), col("date", "Date"), col("total", "Total", { num: true }), col("paid", "Paid", { num: true }), col("status", "Status")],
    rows: INVOICES.map(i => ({ ...i, total: rupee(i.total), paid: rupee(i.paid) })),
  });

  P.patientRegList = (h, it) => reportPage(h, it, {
    subtitle: "OPD Unit · Reports — registrations",
    filters: [field({ label: "From", name: "f1", type: "date" }), field({ label: "To", name: "f2", type: "date" }), field({ label: "Doctor", name: "f3", type: "select", options: ["Dr. Anjali Mehta", "Dr. Rohan Kapoor", "Dr. Vikram Sen"] })],
    columns: [col("reg", "Reg No", { id: true }), col("name", "Patient", { main: true }), col("age", "Age"), col("sex", "Sex"), col("ph", "Phone"), col("doctor", "Consultant"), col("date", "Reg Date")],
    rows: PATIENTS,
  });

  /* ---- Investigation reports ---- */
  P.requisitionList = (h, it) => reportPage(h, it, {
    subtitle: "Investigation · Transaction — requisitions",
    filters: [field({ label: "Req No", name: "f1", placeholder: "REQ-…" }), field({ label: "Date", name: "f2", type: "date" }), field({ label: "Status", name: "f3", type: "select", options: ["Pending", "Sample Collected", "Reported"] })],
    columns: [col("req", "Req No", { id: true }), col("reg", "Reg No"), col("name", "Patient", { main: true }), col("tests", "Investigations"), col("date", "Date"), col("amount", "Amount", { num: true }), col("status", "Status")],
    rows: [
      { req: "REQ-3001", reg: "REG-100231", name: "Priya Sharma", tests: "AMH, FSH", date: "02 Jun 2026", amount: "₹2,450", status: "Reported" },
      { req: "REQ-3002", reg: "REG-100230", name: "Meera Patel", tests: "CBC", date: "01 Jun 2026", amount: "₹350", status: "Sample Collected" },
      { req: "REQ-3003", reg: "REG-100227", name: "Aisha Khan", tests: "Follicular Study USG", date: "01 Jun 2026", amount: "₹1,200", status: "Pending" },
    ],
  });
  P.invGroupCollection = (h, it) => reportPage(h, it, {
    subtitle: "Investigation · Reports — group-wise collection",
    columns: [col("grp", "Investigation Group", { main: true }), col("count", "Tests", { num: true }), col("amount", "Collection", { num: true })],
    actions: false,
    rows: [
      { grp: "Hormone Profile", count: 42, amount: "₹68,400" }, { grp: "Haematology", count: 31, amount: "₹10,850" },
      { grp: "USG Studies", count: 26, amount: "₹31,200" },
    ],
  });
  P.invCollection = (h, it) => reportPage(h, it, {
    subtitle: "Investigation · Reports — investigation-wise collection",
    columns: [col("test", "Investigation", { main: true }), col("grp", "Group"), col("count", "Count", { num: true }), col("amount", "Collection", { num: true })],
    actions: false,
    rows: [
      { test: "AMH", grp: "Hormone Profile", count: 18, amount: "₹32,400" }, { test: "FSH", grp: "Hormone Profile", count: 24, amount: "₹15,600" },
      { test: "CBC", grp: "Haematology", count: 31, amount: "₹10,850" }, { test: "Follicular Study USG", grp: "USG Studies", count: 26, amount: "₹31,200" },
    ],
  });

  /* ---- Pharmacy reports/dashboard ---- */
  const STOCK = [
    { code: "MED01", name: "Gonal-F 75 IU", grp: "Hormones", batch: "GF2406", stock: 24, reorder: 10, expiry: "Mar 2027", mrp: "₹1,150" },
    { code: "MED02", name: "Menopur 75 IU", grp: "Hormones", batch: "MP2405", stock: 8, reorder: 10, expiry: "Jan 2027", mrp: "₹990" },
    { code: "MED03", name: "Folvite 5 mg", grp: "Supplements", batch: "FV2312", stock: 120, reorder: 30, expiry: "Dec 2026", mrp: "₹28" },
    { code: "MED04", name: "Augmentin 625", grp: "Antibiotics", batch: "AG2404", stock: 16, reorder: 20, expiry: "Aug 2026", mrp: "₹210" },
  ];
  P.medicineDashboard = (h, it) => reportPage(h, it, {
    subtitle: "Pharmacy · Transaction — overview",
    kpis: [
      { label: "SKUs", value: STOCK.length, icon: "pill", color: "#3e72ad", soft: "#e6eef7" },
      { label: "Low stock", value: STOCK.filter(s => s.stock <= s.reorder).length, icon: "alert", color: "#b9772b", soft: "#f8edda" },
      { label: "Stock value", value: "₹38,420", icon: "server", color: "#1f9268", soft: "#e2f3ec" },
      { label: "Expiring ≤3m", value: 1, icon: "clock", color: "#c4453f", soft: "#f9e6e4" },
    ],
    columns: [col("code", "Code", { id: true }), col("name", "Item", { main: true }), col("grp", "Group"), col("stock", "Stock", { num: true }), col("reorder", "Reorder", { num: true }), col("expiry", "Expiry"), col("mrp", "MRP", { num: true })],
    rows: STOCK, actions: false,
  });
  P.stockDetails = (h, it) => reportPage(h, it, {
    subtitle: "Pharmacy · Reports — stock details",
    columns: [col("code", "Code", { id: true }), col("name", "Item", { main: true }), col("grp", "Group"), col("batch", "Batch"), col("stock", "Stock", { num: true }), col("expiry", "Expiry"), col("mrp", "MRP", { num: true })],
    rows: STOCK, actions: false,
  });
  P.stockMovement = (h, it) => reportPage(h, it, {
    subtitle: "Pharmacy · Reports — stock movement",
    filters: [field({ label: "From", name: "f1", type: "date" }), field({ label: "To", name: "f2", type: "date" })],
    columns: [col("ref", "Reference", { id: true }), col("type", "Type"), col("date", "Date"), col("item", "Item", { main: true }), col("qty", "Qty", { num: true })],
    actions: false,
    rows: [
      { ref: "PUR-901", type: "Inward", date: "28 May 2026", item: "Gonal-F 75 IU", qty: "+30" },
      { ref: "ISS-450", type: "Outward", date: "02 Jun 2026", item: "Gonal-F 75 IU", qty: "-6" },
      { ref: "ISS-451", type: "Outward", date: "02 Jun 2026", item: "Folvite 5 mg", qty: "-10" },
    ],
  });
  P.stockLedger = (h, it) => reportPage(h, it, {
    subtitle: "Pharmacy · Reports — stock ledger",
    columns: [col("date", "Date", { id: true }), col("particulars", "Particulars", { main: true }), col("ref", "Ref"), col("inn", "In", { num: true }), col("out", "Out", { num: true }), col("bal", "Balance", { num: true })],
    actions: false,
    rows: [
      { date: "28 May", particulars: "Purchase — MediSupply Co.", ref: "PUR-901", inn: 30, out: "—", bal: 30 },
      { date: "02 Jun", particulars: "Issue — Priya Sharma", ref: "ISS-450", inn: "—", out: 6, bal: 24 },
    ],
  });
  P.salesRegister = (h, it) => reportPage(h, it, {
    subtitle: "Pharmacy · Reports — sales register",
    filters: [field({ label: "From", name: "f1", type: "date" }), field({ label: "To", name: "f2", type: "date" })],
    columns: [col("bill", "Bill No", { id: true }), col("reg", "Reg No"), col("name", "Patient", { main: true }), col("date", "Date"), col("items", "Items", { num: true }), col("amount", "Amount", { num: true })],
    actions: false,
    rows: [
      { bill: "PB-7001", reg: "REG-100231", name: "Priya Sharma", date: "02 Jun 2026", items: 3, amount: "₹2,328" },
      { bill: "PB-7002", reg: "REG-100230", name: "Meera Patel", date: "01 Jun 2026", items: 1, amount: "₹990" },
    ],
  });

  /* ---- Accounts reports ---- */
  P.dailyCollection = (h, it) => {
    const f = (window.HMS_FACTS && HMS_FACTS.today) || { collection: { cash: 18400, cardUpi: 26150, bank: 12000, total: 56550 } };
    const c = f.collection;
    const money = n => "₹" + Number(n).toLocaleString("en-IN");
    return reportPage(h, it, {
      subtitle: "Accounts · Reports — daily collection (" + (f.date || "today") + ")",
      kpis: [
        { label: "Cash", value: money(c.cash), icon: "rupee", color: "#1f9268", soft: "#e2f3ec" },
        { label: "Card / UPI", value: money(c.cardUpi), icon: "receipt", color: "#3e72ad", soft: "#e6eef7" },
        { label: "Bank", value: money(c.bank), icon: "server", color: "#6a59c2", soft: "#eae7f8" },
        { label: "Total Collection", value: money(c.total), icon: "check", color: "#b9772b", soft: "#f8edda" },
      ],
      filters: [field({ label: "Date", name: "f1", type: "date" })],
      columns: [col("rcpt", "Receipt", { id: true }), col("name", "Patient", { main: true }), col("head", "Head"), col("mode", "Mode"), col("amount", "Amount", { num: true })],
      actions: false,
      // receipts below reconcile exactly to the KPI totals (Cash 18,400 · Card/UPI 26,150 · Bank 12,000)
      rows: [
        { rcpt: "MR-9001", name: "Priya Sharma", head: "Consultation", mode: "UPI", amount: "₹800" },
        { rcpt: "MR-9002", name: "Priya Sharma", head: "Investigation", mode: "Card", amount: "₹2,450" },
        { rcpt: "MR-9003", name: "Aisha Khan", head: "Pharmacy", mode: "Cash", amount: "₹990" },
        { rcpt: "MR-9004", name: "Meera Patel", head: "IVF Advance", mode: "Bank", amount: "₹12,000" },
        { rcpt: "MR-9005", name: "Imran Sheikh", head: "Procedure", mode: "Card", amount: "₹22,900" },
        { rcpt: "MR-9006", name: "Divya Nair", head: "Consultation", mode: "Cash", amount: "₹800" },
        { rcpt: "MR-9007", name: "Rahul Verma", head: "IVF Advance", mode: "Cash", amount: "₹16,610" },
      ],
    });
  };
  P.docWiseMIS = (h, it) => reportPage(h, it, {
    subtitle: "Accounts · Reports — performing doctor-wise MIS",
    columns: [col("doctor", "Consultant", { main: true }), col("spec", "Speciality"), col("patients", "Patients", { num: true }), col("revenue", "Revenue", { num: true })],
    actions: false,
    rows: [
      { doctor: "Dr. Anjali Mehta", spec: "Reproductive Medicine", patients: 64, revenue: "₹2,84,000" },
      { doctor: "Dr. Vikram Sen", spec: "Obstetrics & Gynaecology", patients: 41, revenue: "₹1,62,500" },
      { doctor: "Dr. Rohan Kapoor", spec: "Andrology", patients: 28, revenue: "₹98,200" },
    ],
  });
  P.ivfPaymentStatus = (h, it) => reportPage(h, it, {
    subtitle: "Accounts · Reports — IVF payment status",
    columns: [col("reg", "Reg No", { id: true }), col("name", "Patient", { main: true }), col("package", "Package"), col("total", "Package ₹", { num: true }), col("paid", "Paid", { num: true }), col("due", "Due", { num: true }), col("status", "Status")],
    actions: false,
    rows: [
      { reg: "REG-100231", name: "Priya Sharma", package: "IVF — Antagonist", total: "₹1,85,000", paid: "₹1,85,000", due: "₹0", status: "Paid" },
      { reg: "REG-100230", name: "Meera Patel", package: "IUI ×3", total: "₹36,000", paid: "₹24,000", due: "₹12,000", status: "Partial" },
      { reg: "REG-100229", name: "Imran Sheikh", package: "Semen Freezing", total: "₹18,000", paid: "₹0", due: "₹18,000", status: "Due" },
    ],
  });

  /* ============================================================
     Security forms (non-master)
     ============================================================ */
  P.userCreation = function (host, item) {
    host.innerHTML = `
      ${pageHead("User Creation", "Security · create a login", `<span class="badge badge--neutral">Active users · 9</span>`)}
      <div class="grid-main" style="grid-template-columns:420px 1fr;align-items:start;gap:24px">
        <form class="card" id="uForm">
          <div class="card-head"><h3>New user</h3></div>
          <div class="card-pad"><div class="lform-grid" style="grid-template-columns:1fr">
            ${field({ label: "Employee", name: "emp", type: "select", required: true, options: ["Kavita Sen", "Arnab Roy", "Pooja Singh"] })}
            ${field({ label: "Login / Username", name: "uname", required: true, placeholder: "username" })}
            ${field({ label: "Password", name: "pwd", type: "password", required: true })}
            ${field({ label: "Confirm Password", name: "cpwd", type: "password", required: true })}
            ${field({ label: "User Role", name: "role", type: "select", required: true, options: ["Administrator", "Front Desk", "Lab Technician", "Pharmacist", "Accountant"] })}
            ${field({ label: "Status", name: "status", type: "select", options: ["Active", "Inactive"] })}
          </div></div>
          <div class="modal__foot" style="border-top:1px solid var(--border)"><button type="reset" class="btn btn-ghost">Cancel</button><button type="submit" class="btn btn-primary">${icon("check", 16)} Create user</button></div>
        </form>
        <div class="card"><div class="card-head"><h3>Users</h3></div>
          ${table([col("uname", "Username", { id: true }), col("emp", "Employee", { main: true }), col("role", "Role"), col("status", "Status")], [
            { uname: "admin", emp: "Sunita Rao", role: "Administrator", status: "Active" },
            { uname: "kavita.s", emp: "Kavita Sen", role: "Front Desk", status: "Active" },
            { uname: "arnab.r", emp: "Arnab Roy", role: "Lab Technician", status: "Active" },
          ])}
        </div>
      </div>`;
    host.querySelector("#uForm").addEventListener("submit", e => { e.preventDefault(); const f = e.target; if (f.pwd.value !== f.cpwd.value) { ui.toast("Passwords do not match", "alert"); return; } ui.toast("User created", "userPlus"); f.reset(); });
  };

  P.changePassword = function (host, item) {
    host.innerHTML = `
      ${pageHead("Change Password", "Security · update your credentials")}
      <form class="card" id="pForm" style="max-width:460px">
        <div class="card-head"><h3>Change password</h3></div>
        <div class="card-pad"><div class="lform-grid" style="grid-template-columns:1fr">
          ${field({ label: "Current Password", name: "cur", type: "password", required: true })}
          ${field({ label: "New Password", name: "nw", type: "password", required: true })}
          ${field({ label: "Confirm New Password", name: "cf", type: "password", required: true })}
        </div></div>
        <div class="modal__foot" style="border-top:1px solid var(--border)"><button type="reset" class="btn btn-ghost">Clear</button><button type="submit" class="btn btn-primary">${icon("check", 16)} Update</button></div>
      </form>`;
    host.querySelector("#pForm").addEventListener("submit", e => { e.preventDefault(); const f = e.target; if (f.nw.value !== f.cf.value) { ui.toast("New passwords do not match", "alert"); return; } ui.toast("Password updated", "check"); f.reset(); });
  };

  P.userActions = (h, it) => reportPage(h, it, {
    subtitle: "Security · audit log",
    filters: [field({ label: "User", name: "f1", placeholder: "username" }), field({ label: "Date", name: "f2", type: "date" })],
    columns: [col("time", "Date / Time", { id: true }), col("user", "User", { main: true }), col("action", "Action"), col("module", "Module")],
    actions: false,
    rows: [
      { time: "04 Jun 2026 09:12", user: "kavita.s", action: "Created patient REG-100231", module: "OPD" },
      { time: "04 Jun 2026 09:30", user: "arnab.r", action: "Entered result REQ-3001", module: "Investigation" },
      { time: "04 Jun 2026 10:02", user: "admin", action: "Updated role access", module: "Security" },
    ],
  });

  P.roleWiseAccess = function (host, item) {
    const roles = ["Administrator", "Front Desk", "Lab Technician", "Pharmacist", "Accountant"];
    const perms = ["No Access", "View", "Full"];
    const menus = [];
    SITEMAP.filter(m => m.groups).forEach(m => m.groups.forEach(g => g.items.slice(0, 2).forEach(it => menus.push(m.label + " · " + it.label))));
    host.innerHTML = `
      ${pageHead("Role-wise Access", "Security · permission matrix", `<button class="btn btn-primary" id="saveAcc">${icon("check", 16)} Save</button>`)}
      <div class="card">
        <div class="card-head"><h3>Permissions</h3>
          <div class="actions"><select class="input select" id="roleSel" style="width:200px">${roles.map(r => `<option>${esc(r)}</option>`).join("")}</select></div>
        </div>
        <div class="table-wrap"><table class="tbl"><thead><tr><th>Menu</th><th style="width:220px">Permission</th></tr></thead>
          <tbody>${menus.slice(0, 14).map(mn => `<tr><td class="cell-main">${esc(mn)}</td><td><select class="input select">${perms.map(p => `<option ${p === "Full" ? "selected" : ""}>${p}</option>`).join("")}</select></td></tr>`).join("")}</tbody>
        </table></div>
      </div>`;
    host.querySelector("#saveAcc").addEventListener("click", () => ui.toast("Access matrix saved", "check"));
  };

  P.dataConsistency = function (host, item) {
    host.innerHTML = `
      ${pageHead("Data Consistency", "Utility · integrity checks")}
      <div class="card"><div class="card-pad">
        <p style="color:var(--text-2);margin:0 0 16px">Run consistency checks across modules. Results appear below.</p>
        <button class="btn btn-primary" id="runDc">${icon("refresh", 16)} Run checks</button>
        <div id="dcOut" style="margin-top:18px"></div>
      </div></div>`;
    host.querySelector("#runDc").addEventListener("click", () => {
      host.querySelector("#dcOut").innerHTML = table(
        [col("check", "Check", { main: true }), col("result", "Result"), col("status", "Status")],
        [
          { check: "Orphan invoices without registration", result: "0 found", status: "OK" },
          { check: "Stock balance vs. ledger", result: "Matched", status: "OK" },
          { check: "Requisitions without bill", result: "2 found", status: "Review" },
        ], { actions: false });
      ui.toast("Consistency check complete", "check");
    });
  };

})();
