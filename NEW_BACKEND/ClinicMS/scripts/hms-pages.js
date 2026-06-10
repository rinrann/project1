/* ============================================================
   ANKURAN HMS — Page renderers (core)
   Home · generic Master engine · Patient Registration (Enrollment)
   ============================================================ */
(function () {
  "use strict";
  const P = window.HMS_PAGES = window.HMS_PAGES || {};
  const esc = ui.esc;

  /* ---- shared field builders --------------------------------------- */
  function field(f) {
    const req = f.required ? `<span class="req">*</span>` : "";
    const cls = f.full ? "lfield col-2" : "lfield";
    let ctl;
    if (f.type === "select") {
      ctl = `<select class="input select" name="${f.name}" ${f.disabled ? "disabled" : ""}>
        <option value="">${esc(f.placeholder || "— Select —")}</option>
        ${(f.options || []).map(o => `<option>${esc(o)}</option>`).join("")}</select>`;
    } else if (f.type === "textarea") {
      ctl = `<textarea class="input" name="${f.name}" rows="${f.rows || 3}" placeholder="${esc(f.placeholder || "")}" ${f.disabled ? "disabled" : ""}></textarea>`;
    } else if (f.type === "prefix") {
      ctl = `<div class="lrow"><span class="lprefix">${esc(f.prefix)}</span><input class="input" name="${f.name}" inputmode="${f.inputmode || "text"}" maxlength="${f.maxlength || 524288}" placeholder="${esc(f.placeholder || "")}" ${f.disabled ? "disabled" : ""}></div>`;
    } else if (f.type === "rowbtn") {
      ctl = `<div class="lrow"><input class="input" name="${f.name}" placeholder="${esc(f.placeholder || "")}" ${f.disabled ? "disabled" : ""}>${(f.buttons || []).map(b => `<button type="button" class="btn ${b.cls || "btn-soft"}" data-act="${b.act}">${b.icon ? icon(b.icon, 15) : ""} ${esc(b.label)}</button>`).join("")}</div>`;
    } else {
      ctl = `<input class="input" type="${f.type || "text"}" name="${f.name}" inputmode="${f.inputmode || "text"}" maxlength="${f.maxlength || 524288}" placeholder="${esc(f.placeholder || "")}" ${f.disabled ? "disabled" : ""}>`;
    }
    return `<div class="${cls}"><label>${esc(f.label)}${req}</label>${ctl}${f.hint ? `<span class="hint">${esc(f.hint)}</span>` : ""}</div>`;
  }

  function table(columns, rows, opts) {
    opts = opts || {};
    const actions = opts.actions !== false;
    return `<div class="table-wrap"><table class="tbl ${actions ? "tbl--act" : ""}">
      <thead><tr>${columns.map(c => `<th class="${c.num ? "num" : ""}">${esc(c.label)}</th>`).join("")}${actions ? `<th style="width:90px">Action</th>` : ""}</tr></thead>
      <tbody>
        ${rows.length ? rows.map(r => `<tr>${columns.map(c => `<td class="${c.num ? "num" : ""} ${c.id ? "cell-id" : (c.main ? "cell-main" : "")}">${r[c.key] == null ? "—" : esc(r[c.key])}</td>`).join("")}${actions ? `<td><div style="display:flex;gap:6px"><button type="button" class="icon-btn js-row-edit" title="Edit" style="border:none;background:var(--surface-2);color:var(--accent-strong);width:28px;height:28px;border-radius:7px;cursor:pointer">${icon("edit", 14)}</button><button type="button" class="icon-btn js-row-del" title="Delete" style="border:none;background:var(--surface-2);color:var(--danger);width:28px;height:28px;border-radius:7px;cursor:pointer">${icon("trash", 14)}</button></div></td>` : ""}</tr>`).join("")
          : `<tr><td colspan="${columns.length + (actions ? 1 : 0)}"><div class="empty" style="padding:26px">No records found.</div></td></tr>`}
      </tbody>
    </table></div>`;
  }

  function pageHead(title, sub, actions) {
    return `<div class="page-head"><div class="page-head__t"><h1>${esc(title)}</h1>${sub ? `<p>${esc(sub)}</p>` : ""}</div>${actions ? `<div class="page-head__actions">${actions}</div>` : ""}</div>`;
  }

  /* =================================================================
     GENERIC MASTER ENGINE
     Reproduces the legacy "form + listing grid" master pattern.
     ================================================================= */
  function masterPage(host, item, spec) {
    const nextCode = spec.codePrefix ? spec.codePrefix + String(spec.rows.length + 1).padStart(3, "0") : null;
    host.innerHTML = `
      ${pageHead(item.label, spec.subtitle || "Master entry", `<span class="badge badge--neutral">${spec.rows.length} records</span>`)}
      <div class="grid-main" style="grid-template-columns: 380px 1fr; align-items:start; gap:24px">
        <form class="card" id="mForm">
          <div class="card-head"><h3>${spec.formTitle || "New entry"}</h3>${spec.formSub ? `<div class="sub">${esc(spec.formSub)}</div>` : ""}</div>
          <div class="card-pad">
            <div class="lform-grid" style="grid-template-columns:1fr">
              ${nextCode ? field({ label: spec.codeLabel || "Code", name: "code", placeholder: nextCode, disabled: true }) : ""}
              ${spec.fields.map(field).join("")}
            </div>
          </div>
          <div class="modal__foot" style="border-top:1px solid var(--border)">
            <button type="reset" class="btn btn-ghost">Cancel</button>
            <button type="submit" class="btn btn-primary">${icon("check", 16)} Submit</button>
          </div>
        </form>
        <div class="card">
          <div class="card-head"><h3>${spec.listTitle || (item.label + " — list")}</h3>
            <div class="actions"><div class="htop__search" style="width:210px">${icon("search", 15, "si")}<input id="mSearch" placeholder="Search…"></div></div>
          </div>
          <div id="mGrid">${table(spec.columns, spec.rows)}</div>
        </div>
      </div>`;

    const form = host.querySelector("#mForm");
    form.addEventListener("submit", e => {
      e.preventDefault();
      const reqd = spec.fields.filter(f => f.required);
      for (const f of reqd) {
        const el = form.elements[f.name];
        if (el && !String(el.value).trim()) { el.classList.add("err"); el.focus(); ui.toast("Please enter " + f.label, "alert"); el.addEventListener("input", () => el.classList.remove("err"), { once: true }); return; }
      }
      ui.toast(item.label + " saved" + (nextCode ? " · " + nextCode : ""), "check");
      form.reset();
    });
    const search = host.querySelector("#mSearch");
    if (search) search.addEventListener("input", () => {
      const t = search.value.toLowerCase();
      const rows = spec.rows.filter(r => spec.columns.some(c => String(r[c.key] || "").toLowerCase().includes(t)));
      host.querySelector("#mGrid").innerHTML = table(spec.columns, rows);
    });
  }
  window.HMS_master = masterPage;
  window.HMS_field = field;
  window.HMS_table = table;
  window.HMS_pageHead = pageHead;

  /* =================================================================
     HOME
     ================================================================= */
  P.home = function (host) {
    const mods = SITEMAP.filter(m => m.groups);
    const totalScreens = mods.reduce((s, m) => s + m.groups.reduce((a, g) => a + g.items.length, 0), 0);
    host.innerHTML = `
      ${pageHead("Welcome to Ankuran", "Hospital Management System · Kolkata", `<span class="badge badge--accent" style="padding:7px 12px">${icon("activity", 14)} ${totalScreens} screens · ${mods.length} modules</span>`)}
      <div class="kpi-grid" style="margin-bottom:22px">
        ${ui.kpi({ label: "Modules", value: mods.length, icon: "grid", color: "#3e72ad", soft: "#e6eef7", note: "OPD, Investigation, Pharmacy…" })}
        ${ui.kpi({ label: "Sub-modules", value: mods.reduce((s, m) => s + m.groups.length, 0), icon: "server", color: "#6a59c2", soft: "#eae7f8", note: "Master · Transaction · Reports" })}
        ${ui.kpi({ label: "Total screens", value: totalScreens, icon: "fileText", color: "#1f9268", soft: "#e2f3ec", note: "carried from legacy system" })}
        ${ui.kpi({ label: "Today", value: "04 Jun", unit: "2026", icon: "calendar", color: "#b9772b", soft: "#f8edda", note: "Thursday" })}
      </div>
      <div class="grid" style="display:grid;grid-template-columns:repeat(auto-fill,minmax(280px,1fr));gap:18px">
        ${mods.map(m => `
          <div class="card mod-card" data-mod="${m.id}" style="cursor:pointer">
            <div class="card-pad">
              <div style="display:flex;align-items:center;gap:12px;margin-bottom:12px">
                <div class="ic" style="width:42px;height:42px;border-radius:11px;background:var(--accent-soft);color:var(--accent-strong);display:grid;place-items:center">${icon(m.icon, 21)}</div>
                <div><div class="cell-main" style="font-size:15px;font-weight:700">${esc(m.label)}</div><div class="cell-sub">${m.groups.reduce((a, g) => a + g.items.length, 0)} screens</div></div>
              </div>
              <div style="display:flex;flex-direction:column;gap:5px">
                ${m.groups.map(g => `<div style="display:flex;justify-content:space-between;font-size:12.5px;color:var(--text-2)"><span>${esc(g.label)}</span><span style="color:var(--text-3)">${g.items.length}</span></div>`).join("")}
              </div>
            </div>
          </div>`).join("")}
      </div>`;
    host.querySelectorAll(".mod-card").forEach(c => c.addEventListener("click", () => {
      const m = SITEMAP.find(x => x.id === c.dataset.mod);
      const fi = m.groups[0].items[0];
      HMS_GO(fi.id);
    }));
  };

  /* =================================================================
     OPD · PATIENT REGISTRATION  (PatientEnrollment.aspx)
     Faithful: Entry tab (full demographic + present/permanent address
     + KYC + remarks) and List tab (search + grid).
     ================================================================= */
  const COUNTRIES = ["India", "Bangladesh", "Nepal", "Bhutan"];
  const STATES = ["West Bengal", "Bihar", "Jharkhand", "Odisha", "Assam", "Delhi", "Maharashtra"];
  const DISTRICTS = {
    "West Bengal": ["Kolkata", "Howrah", "North 24 Parganas", "South 24 Parganas", "Hooghly", "Nadia"],
    "Bihar": ["Patna", "Gaya", "Bhagalpur"], "Jharkhand": ["Ranchi", "Dhanbad", "Jamshedpur"],
    "Odisha": ["Bhubaneswar", "Cuttack", "Puri"], "Assam": ["Guwahati", "Dibrugarh"],
    "Delhi": ["New Delhi", "South Delhi"], "Maharashtra": ["Mumbai", "Pune", "Nagpur"],
  };
  const REG_ROWS = [
    { reg: "REG-100231", name: "Priya Sharma", age: "34 Y", sex: "Female", ph1: "9830042118", ph2: "9830042119", addr: "12 Lake Town, Kolkata", email: "priya.s@gmail.com", date: "02 Jun 2026", dist: "Kolkata" },
    { reg: "REG-100230", name: "Meera Patel", age: "31 Y", sex: "Female", ph1: "9830042120", ph2: "", addr: "5 Salt Lake Sec-2", email: "meera.p@gmail.com", date: "01 Jun 2026", dist: "North 24 Parganas" },
    { reg: "REG-100229", name: "Imran Sheikh", age: "40 Y", sex: "Male", ph1: "9830042121", ph2: "", addr: "88 Park Circus", email: "imran.s@gmail.com", date: "31 May 2026", dist: "Kolkata" },
    { reg: "REG-100228", name: "Divya Nair", age: "29 Y", sex: "Female", ph1: "9830042122", ph2: "9830042123", addr: "21 Behala", email: "divya.n@gmail.com", date: "30 May 2026", dist: "South 24 Parganas" },
    { reg: "REG-100227", name: "Aisha Khan", age: "36 Y", sex: "Female", ph1: "9830042124", ph2: "", addr: "3 Howrah Maidan", email: "aisha.k@gmail.com", date: "29 May 2026", dist: "Howrah" },
  ];
  const REG_COLS = [
    { key: "reg", label: "Registration No", id: true }, { key: "name", label: "Patient Name", main: true },
    { key: "age", label: "Age" }, { key: "sex", label: "Sex" }, { key: "ph1", label: "Phone - 1" },
    { key: "ph2", label: "Phone - 2" }, { key: "addr", label: "Address" }, { key: "email", label: "Email Id" },
    { key: "date", label: "Registration Date" }, { key: "dist", label: "District" },
  ];

  P.patientEnrollment = function (host, item) {
    const nextReg = "REG-" + (100232);
    let tab = "entry";

    function draw() {
      host.innerHTML = `
        ${pageHead("Patient Registration", "OPD Unit · Transaction", `<span class="badge badge--accent" style="padding:7px 12px">${icon("idCard", 14)} New Reg No · ${nextReg}</span>`)}
        <div class="ltabs">
          <button class="ltab ${tab === "entry" ? "active" : ""}" data-tab="entry">Entry</button>
          <button class="ltab ${tab === "list" ? "active" : ""}" data-tab="list">List</button>
        </div>
        <div id="tabBody">${tab === "entry" ? entryView() : listView()}</div>`;
      host.querySelectorAll("[data-tab]").forEach(b => b.addEventListener("click", () => { tab = b.dataset.tab; draw(); }));
      if (tab === "entry") wireEntry(); else wireList();
    }

    function entryView() {
      return `
        <form class="card" id="regForm" novalidate>
          <div class="card-head"><h3>Patient details</h3><div class="sub">Fields marked <span style="color:var(--danger)">*</span> are required</div></div>
          <div class="card-pad">
            <div class="reg-section">Identity</div>
            <div class="lform-grid">
              ${field({ label: "Registration No", name: "regno", required: true, type: "rowbtn", placeholder: nextReg, buttons: [{ act: "search", label: "Search", icon: "search", cls: "btn-soft" }, { act: "fetch", label: "Fetch", icon: "download", cls: "btn-ghost" }], full: true })}
              ${field({ label: "Patient Name", name: "pname", required: true, placeholder: "Full name" })}
              ${field({ label: "Guardian Name", name: "guardian", placeholder: "Father / guardian" })}
              ${field({ label: "Spouse Name", name: "spouse", placeholder: "Spouse name" })}
              ${field({ label: "Date of Birth", name: "dob", type: "date" })}
            </div>
            <div class="reg-section">Age &amp; sex</div>
            <div class="lform-grid" style="grid-template-columns:repeat(4,1fr)">
              ${field({ label: "Age — Year", name: "ageY", inputmode: "numeric", maxlength: 3, placeholder: "34" })}
              ${field({ label: "Month", name: "ageM", inputmode: "numeric", maxlength: 2, placeholder: "0" })}
              ${field({ label: "Day", name: "ageD", inputmode: "numeric", maxlength: 2, placeholder: "0" })}
              ${field({ label: "Sex", name: "sex", required: true, type: "select", options: ["Female", "Male", "Other"] })}
            </div>
            <div class="reg-section">Contact</div>
            <div class="lform-grid">
              ${field({ label: "Contact No (Primary)", name: "ph1", required: true, type: "prefix", prefix: "+91", inputmode: "numeric", maxlength: 10, placeholder: "99300 00000" })}
              ${field({ label: "Alternative Contact No.", name: "ph2", type: "prefix", prefix: "+91", inputmode: "numeric", maxlength: 10, placeholder: "Optional" })}
              ${field({ label: "Email Id", name: "email", type: "email", placeholder: "name@email.com", full: true })}
            </div>
            <div class="reg-section">Present address</div>
            <div class="lform-grid">
              ${field({ label: "Present Address", name: "addr1", type: "textarea", placeholder: "House / street / locality", full: true })}
              ${field({ label: "Country", name: "country1", type: "select", options: COUNTRIES })}
              ${field({ label: "State", name: "state1", type: "select", options: STATES })}
              ${field({ label: "District", name: "dist1", type: "select", options: [] })}
              ${field({ label: "Pin", name: "pin1", inputmode: "numeric", maxlength: 6, placeholder: "700001" })}
            </div>
            <div class="reg-section">Permanent address &nbsp;<label style="font-weight:500;font-size:12px;color:var(--text-3)"><input type="checkbox" id="sameAddr"> Same as present</label></div>
            <div class="lform-grid">
              ${field({ label: "Permanent Address", name: "addr2", type: "textarea", placeholder: "House / street / locality", full: true })}
              ${field({ label: "Country", name: "country2", type: "select", options: COUNTRIES })}
              ${field({ label: "State", name: "state2", type: "select", options: STATES })}
              ${field({ label: "District", name: "dist2", type: "select", options: [] })}
              ${field({ label: "Pin", name: "pin2", inputmode: "numeric", maxlength: 6, placeholder: "700001" })}
            </div>
            <div class="reg-section">KYC &amp; remarks</div>
            <div class="lform-grid">
              ${field({ label: "Aadhaar No", name: "aadhaar", inputmode: "numeric", maxlength: 12, placeholder: "XXXX XXXX XXXX" })}
              ${field({ label: "Passport Id", name: "passport", placeholder: "Passport / PAN" })}
              <div class="lfield col-2"><label>Attach Aadhaar Card</label><input class="input" type="file"></div>
              ${field({ label: "Remarks", name: "remarks", type: "textarea", rows: 2, placeholder: "Notes", full: true })}
            </div>
          </div>
          <div class="modal__foot" style="border-top:1px solid var(--border)">
            <button type="reset" class="btn btn-ghost">Cancel</button>
            <button type="submit" class="btn btn-primary">${icon("check", 16)} Submit</button>
          </div>
        </form>`;
    }

    function listView() {
      return `
        <div class="card">
          <div class="card-head"><h3>Registered patients</h3><div class="sub">${REG_ROWS.length} records</div></div>
          <div class="card-pad" style="padding-bottom:6px">
            <div class="lfilter">
              ${field({ label: "Reg No", name: "fReg", placeholder: "REG-…" })}
              ${field({ label: "Name", name: "fName", placeholder: "Patient name" })}
              ${field({ label: "Ph No", name: "fPh", placeholder: "Phone" })}
              ${field({ label: "Reg Date", name: "fDate", type: "date" })}
              <button class="btn btn-primary" id="listSearch">${icon("search", 15)} Search</button>
            </div>
          </div>
          <div id="listGrid">${table(REG_COLS, REG_ROWS)}</div>
        </div>`;
    }

    function numeric(el) { if (el) el.addEventListener("input", () => { el.value = el.value.replace(/\D+/g, "").slice(0, +el.maxLength || 99); }); }

    function wireEntry() {
      const form = host.querySelector("#regForm");
      ["ageY", "ageM", "ageD", "ph1", "ph2", "pin1", "pin2", "aadhaar"].forEach(n => numeric(form.elements[n]));
      // cascade districts
      function cascade(stateName, distName) {
        const s = form.elements[stateName], d = form.elements[distName];
        s.addEventListener("change", () => { const list = DISTRICTS[s.value] || []; d.innerHTML = `<option value="">— Select —</option>` + list.map(x => `<option>${esc(x)}</option>`).join(""); });
      }
      cascade("state1", "dist1"); cascade("state2", "dist2");
      // same as present
      const same = host.querySelector("#sameAddr");
      same.addEventListener("change", () => {
        if (!same.checked) return;
        form.elements.addr2.value = form.elements.addr1.value;
        form.elements.country2.value = form.elements.country1.value;
        form.elements.state2.value = form.elements.state1.value;
        form.elements.state2.dispatchEvent(new Event("change"));
        setTimeout(() => { form.elements.dist2.value = form.elements.dist1.value; }, 0);
        form.elements.pin2.value = form.elements.pin1.value;
      });
      // search/fetch buttons
      host.querySelectorAll("[data-act]").forEach(b => b.addEventListener("click", () => {
        if (b.dataset.act === "search") openSearch(form);
        else { const v = (form.elements.regno.value || "").trim(); const hit = REG_ROWS.find(r => r.reg.toLowerCase() === v.toLowerCase()); if (hit) fill(form, hit); else ui.toast("No record for " + (v || "—"), "alert"); }
      }));
      // validation (faithful required set)
      form.addEventListener("submit", e => {
        e.preventDefault();
        const checks = [["sex", "Please select Sex"], ["regno", "Please enter Registration No"], ["pname", "Please enter Patient Name"], ["ageY", "Please enter Age"], ["ph1", "Please enter Phone - 1"], ["addr1", "Please enter Address"]];
        for (const [n, msg] of checks) { const el = form.elements[n]; if (el && !String(el.value).trim()) { el.classList.add("err"); el.focus(); ui.toast(msg, "alert"); el.addEventListener("input", () => el.classList.remove("err"), { once: true }); el.addEventListener("change", () => el.classList.remove("err"), { once: true }); return; } }
        ui.toast("Registered " + form.elements.pname.value + " · " + nextReg, "userPlus"); form.reset();
      });
    }

    function fill(form, r) {
      form.elements.regno.value = r.reg; form.elements.pname.value = r.name;
      form.elements.ageY.value = (r.age || "").replace(/\D+/g, ""); form.elements.sex.value = r.sex;
      form.elements.ph1.value = r.ph1; form.elements.ph2.value = r.ph2 || ""; form.elements.email.value = r.email; form.elements.addr1.value = r.addr;
      ui.toast("Loaded " + r.name + " · " + r.reg, "download");
    }

    function openSearch(form) {
      ui.modal({
        title: "Patient quick search", subtitle: "Search by name or registration no.",
        body: `<div class="lfield" style="margin-bottom:12px"><input class="input" id="qsq" placeholder="Type a name or Reg No…"></div>
          <div class="table-wrap" style="max-height:320px;overflow:auto"><table class="tbl"><thead><tr><th>Reg No</th><th>Name</th><th>Age</th><th>Phone</th></tr></thead><tbody id="qsr"></tbody></table></div>`,
        onMount: (root) => {
          const q = root.querySelector("#qsq"), body = root.querySelector("#qsr");
          const draw = (t) => {
            t = (t || "").toLowerCase();
            const rows = REG_ROWS.filter(r => !t || r.name.toLowerCase().includes(t) || r.reg.toLowerCase().includes(t));
            body.innerHTML = rows.map(r => `<tr class="clickable" data-pick="${r.reg}"><td class="cell-id">${r.reg}</td><td class="cell-main">${esc(r.name)}</td><td>${r.age}</td><td>${r.ph1}</td></tr>`).join("") || `<tr><td colspan="4"><div class="empty">No matches.</div></td></tr>`;
            body.querySelectorAll("[data-pick]").forEach(tr => tr.addEventListener("click", () => { fill(form, REG_ROWS.find(r => r.reg === tr.dataset.pick)); ui.closeModal(); }));
          };
          draw(""); q.focus(); q.addEventListener("input", () => draw(q.value));
        }
      });
    }

    function wireList() {
      const btn = host.querySelector("#listSearch");
      btn.addEventListener("click", () => {
        const get = n => (host.querySelector(`[name="${n}"]`).value || "").toLowerCase();
        const reg = get("fReg"), nm = get("fName"), ph = get("fPh");
        const rows = REG_ROWS.filter(r => (!reg || r.reg.toLowerCase().includes(reg)) && (!nm || r.name.toLowerCase().includes(nm)) && (!ph || (r.ph1 + r.ph2).includes(ph)));
        host.querySelector("#listGrid").innerHTML = table(REG_COLS, rows);
      });
    }

    draw();
  };

})();
