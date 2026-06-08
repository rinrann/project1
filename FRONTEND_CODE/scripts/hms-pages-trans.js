/* ============================================================
   ANKURAN HMS — Transaction pages
   Shared engine: header (patient/supplier) + addable line-item grid
   + live totals + save. Configured per legacy screen.
   ============================================================ */
(function () {
  "use strict";
  const P = window.HMS_PAGES;
  const pageHead = window.HMS_pageHead, field = window.HMS_field;
  const esc = ui.esc;
  const inr = n => "₹" + Number(n || 0).toLocaleString("en-IN");

  const PATIENTS = [
    { reg: "REG-100231", name: "Priya Sharma", age: "34", sex: "Female", ph: "9830042118", doctor: "Dr. Anjali Mehta" },
    { reg: "REG-100230", name: "Meera Patel", age: "31", sex: "Female", ph: "9830042120", doctor: "Dr. Vikram Sen" },
    { reg: "REG-100229", name: "Imran Sheikh", age: "40", sex: "Male", ph: "9830042121", doctor: "Dr. Rohan Kapoor" },
    { reg: "REG-100227", name: "Aisha Khan", age: "36", sex: "Female", ph: "9830042124", doctor: "Dr. Vikram Sen" },
  ];
  const SUPPLIERS = ["MediSupply Co.", "LabKart Distributors", "PharmaHub"];

  /* ---- engine ------------------------------------------------------ */
  function txnPage(host, item, spec) {
    const lines = [];
    host.innerHTML = `
      ${pageHead(item.label, spec.subtitle, `<span class="badge badge--accent" style="padding:7px 12px">${icon(spec.icon || "receipt", 14)} ${esc(spec.docLabel || "New")} · ${esc(spec.docNo || "")}</span>`)}
      <div class="card" style="margin-bottom:20px">
        <div class="card-head"><h3>${esc(spec.headerTitle || "Details")}</h3></div>
        <div class="card-pad"><div class="lform-grid">${spec.header}</div></div>
      </div>
      <div class="card" style="margin-bottom:20px">
        <div class="card-head"><h3>Add ${esc(spec.lineNoun || "line")}</h3></div>
        <div class="card-pad" style="padding-bottom:10px">
          <div class="lfilter" id="lineForm">${spec.lineFields.map(field).join("")}
            <button class="btn btn-primary" id="addLine">${icon("plus", 15)} Add</button>
          </div>
        </div>
      </div>
      <div class="card">
        <div class="card-head"><h3>${esc(spec.gridTitle || "Items")}</h3><div class="sub" id="lineCount">0 ${esc(spec.lineNoun || "line")}(s)</div></div>
        <div id="lineGrid"></div>
        ${spec.totals ? `<div class="card-pad" id="totals" style="border-top:1px solid var(--border)"></div>` : ""}
        <div class="modal__foot" style="border-top:1px solid var(--border)"><button class="btn btn-ghost" id="txCancel">Cancel</button><button class="btn btn-primary" id="txSave">${icon("check", 16)} Save ${esc(spec.docLabel || "")}</button></div>
      </div>`;

    // patient fetch (if header has it)
    const fetchBtn = host.querySelector('[data-act="fetch"]');
    if (fetchBtn) fetchBtn.addEventListener("click", () => {
      const v = (host.querySelector('[name="regno"]').value || "").trim();
      const p = PATIENTS.find(x => x.reg.toLowerCase() === v.toLowerCase()) || PATIENTS[0];
      const set = (n, val) => { const el = host.querySelector(`[name="${n}"]`); if (el) el.value = val; };
      set("regno", p.reg); set("pname", p.name); set("page", p.age); set("psex", p.sex); set("pdoctor", p.doctor);
      ui.toast("Loaded " + p.name + " · " + p.reg, "download");
    });

    function renderGrid() {
      const cols = spec.lineCols;
      host.querySelector("#lineGrid").innerHTML = `<div class="table-wrap"><table class="tbl">
        <thead><tr>${cols.map(c => `<th class="${c.num ? "num" : ""}">${esc(c.label)}</th>`).join("")}<th style="width:60px"></th></tr></thead>
        <tbody>${lines.length ? lines.map((l, i) => `<tr>${cols.map(c => `<td class="${c.num ? "num" : ""} ${c.main ? "cell-main" : ""}">${esc(l[c.key] == null ? "—" : l[c.key])}</td>`).join("")}<td><button class="icon-btn" data-del="${i}" title="Remove" style="border:none;background:var(--surface-2);color:var(--danger);width:28px;height:28px;border-radius:7px;cursor:pointer">${icon("trash", 14)}</button></td></tr>`).join("")
          : `<tr><td colspan="${cols.length + 1}"><div class="empty" style="padding:22px">No ${esc(spec.lineNoun || "line")}s added yet.</div></td></tr>`}</tbody>
      </table></div>`;
      host.querySelector("#lineCount").textContent = lines.length + " " + (spec.lineNoun || "line") + "(s)";
      host.querySelectorAll("[data-del]").forEach(b => b.addEventListener("click", () => { lines.splice(+b.dataset.del, 1); renderGrid(); }));
      if (spec.totals) host.querySelector("#totals").innerHTML = spec.totals(lines, inr);
    }

    host.querySelector("#addLine").addEventListener("click", () => {
      const lf = host.querySelector("#lineForm");
      const obj = {}; let firstReq = null, missing = false;
      spec.lineFields.forEach(f => {
        const el = lf.querySelector(`[name="${f.name}"]`);
        obj[f.name] = el ? el.value : "";
        if (f.required && !String(obj[f.name]).trim() && !missing) { missing = true; firstReq = el; }
      });
      if (missing) { firstReq.classList.add("err"); firstReq.focus(); ui.toast("Please complete the line", "alert"); firstReq.addEventListener("input", () => firstReq.classList.remove("err"), { once: true }); return; }
      lines.push(spec.mapLine ? spec.mapLine(obj) : obj);
      spec.lineFields.forEach(f => { const el = lf.querySelector(`[name="${f.name}"]`); if (el && el.tagName !== "SELECT") el.value = ""; });
      renderGrid();
    });
    host.querySelector("#txSave").addEventListener("click", () => {
      if (!lines.length) { ui.toast("Add at least one " + (spec.lineNoun || "line"), "alert"); return; }
      ui.toast((spec.docLabel || "Document") + " saved · " + lines.length + " " + (spec.lineNoun || "line") + "(s)", "check");
    });
    host.querySelector("#txCancel").addEventListener("click", () => { lines.length = 0; renderGrid(); });
    renderGrid();
  }

  // patient header HTML block (reused)
  function patientHeader() {
    return `
      ${field({ label: "Registration No", name: "regno", type: "rowbtn", placeholder: "REG-…", buttons: [{ act: "fetch", label: "Fetch", icon: "download", cls: "btn-soft" }] })}
      ${field({ label: "Patient Name", name: "pname", disabled: true })}
      ${field({ label: "Age", name: "page", disabled: true })}
      ${field({ label: "Sex", name: "psex", disabled: true })}
      ${field({ label: "Consultant", name: "pdoctor", disabled: true })}`;
  }

  /* ---- OPD transactions ---- */
  P.prescriptionEntry = (h, it) => txnPage(h, it, {
    subtitle: "OPD Unit · Transaction", icon: "fileText", docLabel: "Prescription", docNo: "RX-" + 4521,
    headerTitle: "Patient", header: patientHeader(),
    lineNoun: "medicine", gridTitle: "Prescribed medicines",
    lineFields: [
      { label: "Medicine", name: "med", required: true, type: "select", options: ["Gonal-F 75 IU", "Menopur 75 IU", "Folvite 5 mg", "Augmentin 625"] },
      { label: "Dose", name: "dose", type: "select", options: ["1-0-1", "1-1-1", "0-0-1", "SOS"] },
      { label: "Route", name: "route", type: "select", options: ["Oral", "IM", "SC", "IV", "Vaginal"] },
      { label: "Duration", name: "dur", type: "select", options: ["3 Days", "5 Days", "7 Days", "1 Month"] },
      { label: "Remarks", name: "rem", placeholder: "Optional" },
    ],
    lineCols: [{ key: "med", label: "Medicine", main: true }, { key: "dose", label: "Dose" }, { key: "route", label: "Route" }, { key: "dur", label: "Duration" }, { key: "rem", label: "Remarks" }],
  });

  P.injectionEntry = (h, it) => txnPage(h, it, {
    subtitle: "OPD Unit · Transaction", icon: "syringe", docLabel: "Injection sheet", docNo: "INJ-" + 2210,
    headerTitle: "Patient", header: patientHeader(),
    lineNoun: "injection", gridTitle: "Injections given",
    lineFields: [
      { label: "Injection", name: "inj", required: true, type: "select", options: ["Gonal-F 75 IU", "Menopur 75 IU", "Ovitrelle 250 mcg", "Decapeptyl 0.1 mg"] },
      { label: "Dose", name: "dose", placeholder: "e.g. 150 IU" },
      { label: "Route", name: "route", type: "select", options: ["IM", "SC", "IV"] },
      { label: "Date", name: "date", type: "date" },
      { label: "Given By", name: "by", type: "select", options: ["Kavita Sen", "Pooja Singh"] },
    ],
    lineCols: [{ key: "inj", label: "Injection", main: true }, { key: "dose", label: "Dose" }, { key: "route", label: "Route" }, { key: "date", label: "Date" }, { key: "by", label: "Given By" }],
  });

  P.testResultEntry = (h, it) => txnPage(h, it, {
    subtitle: "OPD Unit · Transaction", icon: "flask", docLabel: "Result sheet", docNo: "RES-" + 3301,
    headerTitle: "Patient", header: patientHeader(),
    lineNoun: "result", gridTitle: "Test results",
    lineFields: [
      { label: "Investigation", name: "test", required: true, type: "select", options: ["AMH", "FSH", "LH", "TSH", "CBC", "Beta hCG"] },
      { label: "Result", name: "result", required: true, placeholder: "Value" },
      { label: "Unit", name: "unit", placeholder: "ng/mL" },
      { label: "Normal Range", name: "range", placeholder: "1.0 – 4.0" },
      { label: "Flag", name: "flag", type: "select", options: ["Normal", "High", "Low"] },
    ],
    lineCols: [{ key: "test", label: "Investigation", main: true }, { key: "result", label: "Result", num: true }, { key: "unit", label: "Unit" }, { key: "range", label: "Normal Range" }, { key: "flag", label: "Flag" }],
  });

  /* ---- Investigation transactions ---- */
  const TEST_RATE = { "AMH": 1800, "FSH": 650, "LH": 650, "CBC": 350, "Follicular Study USG": 1200, "Beta hCG": 700 };
  P.requisitionBill = (h, it) => txnPage(h, it, {
    subtitle: "Investigation · Transaction", icon: "receipt", docLabel: "Bill", docNo: "REQ-" + 3004,
    headerTitle: "Patient", header: patientHeader(),
    lineNoun: "investigation", gridTitle: "Billed investigations", totals: billTotals,
    lineFields: [
      { label: "Investigation", name: "test", required: true, type: "select", options: Object.keys(TEST_RATE) },
      { label: "Qty", name: "qty", inputmode: "numeric", placeholder: "1" },
    ],
    mapLine: o => { const rate = TEST_RATE[o.test] || 0; const qty = +o.qty || 1; return { test: o.test, rate: inr(rate), qty, amount: inr(rate * qty), _amt: rate * qty }; },
    lineCols: [{ key: "test", label: "Investigation", main: true }, { key: "rate", label: "Rate", num: true }, { key: "qty", label: "Qty", num: true }, { key: "amount", label: "Amount", num: true }],
  });

  function billTotals(lines, inr) {
    const sub = lines.reduce((s, l) => s + (l._amt || 0), 0);
    const gst = Math.round(sub * 0.0);
    return `<div style="display:flex;justify-content:flex-end;gap:40px;font-size:14px">
      <div style="text-align:right"><div style="color:var(--text-3);font-size:12px">Sub-total</div><div style="font-weight:700">${inr(sub)}</div></div>
      <div style="text-align:right"><div style="color:var(--text-3);font-size:12px">Discount</div><div style="font-weight:700">${inr(0)}</div></div>
      <div style="text-align:right"><div style="color:var(--text-3);font-size:12px">Net payable</div><div style="font-weight:800;color:var(--accent-strong);font-size:18px">${inr(sub - gst)}</div></div>
    </div>`;
  }

  P.billCancel = function (host, item) {
    host.innerHTML = `
      ${pageHead("Bill Cancellation Entry", "Investigation · Transaction")}
      <form class="card" id="bcForm" style="max-width:560px">
        <div class="card-head"><h3>Cancellation request</h3></div>
        <div class="card-pad"><div class="lform-grid" style="grid-template-columns:1fr">
          ${field({ label: "Bill / Requisition No", name: "bill", required: true, type: "rowbtn", placeholder: "REQ-…", buttons: [{ act: "fetch", label: "Fetch", icon: "download", cls: "btn-soft" }] })}
          ${field({ label: "Patient", name: "pname", disabled: true })}
          ${field({ label: "Bill Amount", name: "amt", disabled: true })}
          ${field({ label: "Reason for cancellation", name: "reason", required: true, type: "textarea", rows: 3 })}
        </div></div>
        <div class="modal__foot" style="border-top:1px solid var(--border)"><button type="reset" class="btn btn-ghost">Clear</button><button type="submit" class="btn btn-primary">${icon("check", 16)} Submit for approval</button></div>
      </form>`;
    host.querySelector('[data-act="fetch"]').addEventListener("click", () => { host.querySelector('[name="pname"]').value = "Priya Sharma"; host.querySelector('[name="amt"]').value = "₹2,450"; ui.toast("Bill loaded", "download"); });
    host.querySelector("#bcForm").addEventListener("submit", e => { e.preventDefault(); const r = e.target.reason; if (!r.value.trim()) { r.classList.add("err"); r.focus(); ui.toast("Enter a reason", "alert"); return; } ui.toast("Cancellation submitted for approval", "check"); e.target.reset(); });
  };

  P.billCancelApproval = (h, it) => window.HMS_report(h, it, {
    subtitle: "Investigation · Transaction — pending approvals",
    columns: [col("bill", "Bill No", { id: true }), col("name", "Patient", { main: true }), col("amount", "Amount", { num: true }), col("reason", "Reason"), col("by", "Requested By")],
    actions: false,
    rows: [
      { bill: "REQ-3002", name: "Meera Patel", amount: "₹350", reason: "Duplicate entry", by: "kavita.s" },
      { bill: "REQ-2998", name: "Imran Sheikh", amount: "₹700", reason: "Wrong patient", by: "arnab.r" },
    ],
  });

  P.billUpdate = function (host, item) {
    host.innerHTML = `
      ${pageHead("Bill Update", "Investigation · Transaction")}
      <form class="card" id="buForm" style="max-width:560px">
        <div class="card-head"><h3>Update bill</h3></div>
        <div class="card-pad"><div class="lform-grid" style="grid-template-columns:1fr">
          ${field({ label: "Bill / Requisition No", name: "bill", required: true, type: "rowbtn", placeholder: "REQ-…", buttons: [{ act: "fetch", label: "Fetch", icon: "download", cls: "btn-soft" }] })}
          ${field({ label: "Discount (₹)", name: "disc", inputmode: "numeric", placeholder: "0" })}
          ${field({ label: "Payment Mode", name: "mode", type: "select", options: ["Cash", "Card", "UPI", "Bank"] })}
          ${field({ label: "Remarks", name: "rem", type: "textarea", rows: 2 })}
        </div></div>
        <div class="modal__foot" style="border-top:1px solid var(--border)"><button type="reset" class="btn btn-ghost">Clear</button><button type="submit" class="btn btn-primary">${icon("check", 16)} Update</button></div>
      </form>`;
    host.querySelector('[data-act="fetch"]').addEventListener("click", () => ui.toast("Bill loaded", "download"));
    host.querySelector("#buForm").addEventListener("submit", e => { e.preventDefault(); ui.toast("Bill updated", "check"); });
  };

  /* ---- Pharmacy transactions ---- */
  const ITEM_RATE = { "Gonal-F 75 IU": 1150, "Menopur 75 IU": 990, "Folvite 5 mg": 28, "Augmentin 625": 210 };
  P.medicinePurchase = (h, it) => txnPage(h, it, {
    subtitle: "Pharmacy · Transaction", icon: "download", docLabel: "Purchase", docNo: "PUR-" + 902,
    headerTitle: "Purchase header",
    header: `
      ${field({ label: "Supplier", name: "supplier", required: true, type: "select", options: SUPPLIERS })}
      ${field({ label: "Invoice No", name: "invno", placeholder: "Supplier invoice" })}
      ${field({ label: "Invoice Date", name: "invdt", type: "date" })}`,
    lineNoun: "item", gridTitle: "Purchased items", totals: simpleTotals,
    lineFields: [
      { label: "Item", name: "item", required: true, type: "select", options: Object.keys(ITEM_RATE) },
      { label: "Batch", name: "batch", placeholder: "Batch" },
      { label: "Expiry", name: "expiry", type: "month" },
      { label: "Qty", name: "qty", inputmode: "numeric", placeholder: "0" },
      { label: "Rate", name: "rate", inputmode: "numeric", placeholder: "0" },
    ],
    mapLine: o => { const qty = +o.qty || 0, rate = +o.rate || ITEM_RATE[o.item] || 0; return { item: o.item, batch: o.batch, expiry: o.expiry, qty, rate: inr(rate), amount: inr(qty * rate), _amt: qty * rate }; },
    lineCols: [{ key: "item", label: "Item", main: true }, { key: "batch", label: "Batch" }, { key: "expiry", label: "Expiry" }, { key: "qty", label: "Qty", num: true }, { key: "rate", label: "Rate", num: true }, { key: "amount", label: "Amount", num: true }],
  });

  P.purchaseReturn = (h, it) => txnPage(h, it, {
    subtitle: "Pharmacy · Transaction", icon: "refresh", docLabel: "Return", docNo: "PR-" + 145,
    headerTitle: "Return header",
    header: `
      ${field({ label: "Supplier", name: "supplier", required: true, type: "select", options: SUPPLIERS })}
      ${field({ label: "Against Invoice", name: "invno", placeholder: "PUR-…" })}
      ${field({ label: "Return Date", name: "rdt", type: "date" })}`,
    lineNoun: "item", gridTitle: "Returned items", totals: simpleTotals,
    lineFields: [
      { label: "Item", name: "item", required: true, type: "select", options: Object.keys(ITEM_RATE) },
      { label: "Batch", name: "batch", placeholder: "Batch" },
      { label: "Qty", name: "qty", inputmode: "numeric", placeholder: "0" },
      { label: "Rate", name: "rate", inputmode: "numeric", placeholder: "0" },
      { label: "Reason", name: "reason", placeholder: "Damaged / Expired" },
    ],
    mapLine: o => { const qty = +o.qty || 0, rate = +o.rate || ITEM_RATE[o.item] || 0; return { item: o.item, batch: o.batch, qty, rate: inr(rate), reason: o.reason, amount: inr(qty * rate), _amt: qty * rate }; },
    lineCols: [{ key: "item", label: "Item", main: true }, { key: "batch", label: "Batch" }, { key: "qty", label: "Qty", num: true }, { key: "rate", label: "Rate", num: true }, { key: "reason", label: "Reason" }, { key: "amount", label: "Amount", num: true }],
  });

  P.medicineIssue = (h, it) => txnPage(h, it, {
    subtitle: "Pharmacy · Transaction", icon: "pill", docLabel: "Issue", docNo: "ISS-" + 452,
    headerTitle: "Patient", header: patientHeader(),
    lineNoun: "item", gridTitle: "Issued items", totals: simpleTotals,
    lineFields: [
      { label: "Item", name: "item", required: true, type: "select", options: Object.keys(ITEM_RATE) },
      { label: "Batch", name: "batch", type: "select", options: ["GF2406", "MP2405", "FV2312", "AG2404"] },
      { label: "Qty", name: "qty", inputmode: "numeric", placeholder: "0" },
    ],
    mapLine: o => { const qty = +o.qty || 0, rate = ITEM_RATE[o.item] || 0; return { item: o.item, batch: o.batch, qty, rate: inr(rate), amount: inr(qty * rate), _amt: qty * rate }; },
    lineCols: [{ key: "item", label: "Item", main: true }, { key: "batch", label: "Batch" }, { key: "qty", label: "Qty", num: true }, { key: "rate", label: "Rate", num: true }, { key: "amount", label: "Amount", num: true }],
  });

  function simpleTotals(lines, inr) {
    const sub = lines.reduce((s, l) => s + (l._amt || 0), 0);
    return `<div style="display:flex;justify-content:flex-end"><div style="text-align:right"><div style="color:var(--text-3);font-size:12px">Total</div><div style="font-weight:800;color:var(--accent-strong);font-size:18px">${inr(sub)}</div></div></div>`;
  }

  P.openingStock = function (host, item) {
    host.innerHTML = `
      ${pageHead("Opening Stock Update", "Pharmacy · Transaction — import opening balances")}
      <div class="card" style="max-width:640px"><div class="card-pad">
        <div class="lform-grid" style="grid-template-columns:1fr">
          <div class="lfield"><label>Upload stock file (.xlsx / .csv)</label><input class="input" type="file" accept=".xlsx,.csv"></div>
          ${field({ label: "As-on Date", name: "ason", type: "date" })}
        </div>
        <div style="display:flex;gap:10px;margin-top:18px"><button class="btn btn-soft" id="dl">${icon("download", 15)} Download template</button><button class="btn btn-primary" id="up">${icon("check", 16)} Import</button></div>
      </div></div>`;
    host.querySelector("#dl").addEventListener("click", () => ui.toast("Template downloaded", "download"));
    host.querySelector("#up").addEventListener("click", () => ui.toast("Opening stock imported", "check"));
  };

  /* ---- Accounts transaction ---- */
  P.advancePayment = function (host, item) {
    host.innerHTML = `
      ${pageHead("Advance Payment Entry", "Accounts · Transaction", `<span class="badge badge--accent" style="padding:7px 12px">${icon("rupee", 14)} Receipt · MR-${9004}</span>`)}
      <form class="card" id="apForm" style="max-width:620px">
        <div class="card-head"><h3>Advance receipt</h3></div>
        <div class="card-pad"><div class="lform-grid">
          ${field({ label: "Registration No", name: "regno", required: true, type: "rowbtn", placeholder: "REG-…", buttons: [{ act: "fetch", label: "Fetch", icon: "download", cls: "btn-soft" }], full: true })}
          ${field({ label: "Patient Name", name: "pname", disabled: true })}
          ${field({ label: "Consultant", name: "pdoctor", disabled: true })}
          ${field({ label: "Amount (₹)", name: "amount", required: true, inputmode: "numeric", placeholder: "0" })}
          ${field({ label: "Payment Mode", name: "mode", type: "select", options: ["Cash", "Card", "UPI", "Bank", "Cheque"] })}
          ${field({ label: "Reference / Txn No", name: "ref", placeholder: "Optional" })}
          ${field({ label: "Remarks", name: "rem", type: "textarea", rows: 2 })}
        </div></div>
        <div class="modal__foot" style="border-top:1px solid var(--border)"><button type="reset" class="btn btn-ghost">Cancel</button><button type="submit" class="btn btn-primary">${icon("check", 16)} Receive payment</button></div>
      </form>`;
    host.querySelector('[data-act="fetch"]').addEventListener("click", () => {
      const v = (host.querySelector('[name="regno"]').value || "").trim();
      const p = PATIENTS.find(x => x.reg.toLowerCase() === v.toLowerCase()) || PATIENTS[0];
      host.querySelector('[name="regno"]').value = p.reg; host.querySelector('[name="pname"]').value = p.name; host.querySelector('[name="pdoctor"]').value = p.doctor;
      ui.toast("Loaded " + p.name, "download");
    });
    host.querySelector('[name="amount"]').addEventListener("input", e => { e.target.value = e.target.value.replace(/\D+/g, ""); });
    host.querySelector("#apForm").addEventListener("submit", e => {
      e.preventDefault(); const a = e.target.amount;
      if (!a.value.trim()) { a.classList.add("err"); a.focus(); ui.toast("Enter amount", "alert"); return; }
      ui.toast("Advance ₹" + (+a.value).toLocaleString("en-IN") + " received", "rupee"); e.target.reset();
    });
  };

  const col = (key, label, o) => Object.assign({ key, label }, o || {});
  window.HMS_txn = txnPage;

})();
