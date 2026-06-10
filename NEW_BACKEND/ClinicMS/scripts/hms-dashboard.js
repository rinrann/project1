/* ============================================================
   ANKURAN HMS — Dashboard
   ------------------------------------------------------------
   Two experiences, chosen by role:
   • EXECUTIVE (Admin · Super Admin · Accounts · Management):
     global date slicer + financial & business KPIs +
     Revenue/Profit/Loss trend with Day/Week/Month/Quarter/Year
     drill-down. All metrics recompute instantly from the slicer.
   • OPERATIONAL / CLINICAL (Doctor · Embryologist · Receptionist):
     role-relevant operational widgets — NO business financials.
   ============================================================ */
(function () {
  "use strict";
  const P = window.HMS_PAGES;
  const pageHead = window.HMS_pageHead;
  const esc = (window.ui && ui.esc) || (s => String(s == null ? "" : s));
  const PALETTE = ["#3e72ad", "#1f9268", "#b9772b", "#6a59c2", "#c4453f", "#2c8aa6"];
  const A = (h, a) => (window.chartAlpha ? window.chartAlpha(h, a) : h);

  /* roles allowed to see business financial analytics */
  const FIN_ROLES = { "Super Admin": 1, "Admin": 1, "Accounts Team": 1, "Management": 1 };
  function role() { return (window.HMS_ROLE ? HMS_ROLE() : "Admin"); }
  function isFinRole() { return !!FIN_ROLES[role()]; }
  function roleLabel() { return window.HMS_ROLE_LABEL ? HMS_ROLE_LABEL() : role(); }

  /* =================================================================
     1) FINANCE DATA ENGINE  (deterministic synthetic daily ledger)
     ================================================================= */
  const TODAY = new Date(2026, 5, 6); // 06 Jun 2026 (reference "today")
  function iso(d) { return d.getFullYear() + "-" + String(d.getMonth() + 1).padStart(2, "0") + "-" + String(d.getDate()).padStart(2, "0"); }
  function mulberry32(a) { return function () { a |= 0; a = a + 0x6D2B79F5 | 0; let t = Math.imul(a ^ a >>> 15, 1 | a); t = t + Math.imul(t ^ t >>> 7, 61 | t) ^ t; return ((t ^ t >>> 14) >>> 0) / 4294967296; }; }

  let DAILY = null;
  function buildDaily() {
    if (DAILY) return DAILY;
    const out = [];
    const start = new Date(2024, 5, 1);          // 01 Jun 2024
    const end = new Date(2026, 5, 6);
    const rng = mulberry32(987654321);
    let i = 0;
    for (let d = new Date(start); d <= end; d.setDate(d.getDate() + 1)) {
      const dow = d.getDay();
      const weekend = dow === 0 ? 0.68 : dow === 6 ? 0.9 : 1;
      const growth = 1 + i * 0.00050;            // steady upward trend
      const season = 1 + 0.12 * Math.sin((d.getMonth()) / 12 * Math.PI * 2);
      const noise = 0.82 + rng() * 0.40;
      let revenue = 46000 * weekend * growth * season * noise;
      let costRatio = 0.60 + rng() * 0.22;        // 60–82 %
      if (rng() < 0.06) costRatio = 1.02 + rng() * 0.16; // occasional loss day
      const cost = revenue * costRatio;
      const patients = Math.max(1, Math.round(revenue / 2450 * (0.9 + rng() * 0.2)));
      const appts = Math.round(patients * (0.7 + rng() * 0.3));
      const newRegs = Math.round(patients * (0.30 + rng() * 0.18));
      const sales = Math.round(patients * (1.3 + rng() * 0.6));
      const dd = new Date(d);
      out.push({ date: iso(d), ts: +dd, dobj: dd, revenue, cost, patients, appts, newRegs, sales });
      i++;
    }
    // Anchor TODAY to the canonical facts so the dashboard's "Today" headline
    // matches the Accounts · Daily Collection total exactly.
    const f = (window.HMS_FACTS && HMS_FACTS.today) || null;
    if (f && out.length) {
      const last = out[out.length - 1];
      last.revenue = f.revenue;
      last.cost = (f.cost != null) ? f.cost : Math.round(f.revenue * 0.62);
      last.patients = f.patients || last.patients;
      last.appts = f.appointments || last.appts;
      last.newRegs = f.newRegs || last.newRegs;
      last.sales = f.salesTxns || last.sales;
    }
    DAILY = out;
    return out;
  }

  function aggregate(rows) {
    let rev = 0, cost = 0, loss = 0, pat = 0, app = 0, reg = 0, sal = 0;
    rows.forEach(x => {
      rev += x.revenue; cost += x.cost; pat += x.patients; app += x.appts; reg += x.newRegs; sal += x.sales;
      if (x.cost > x.revenue) loss += (x.cost - x.revenue);
    });
    const profit = rev - cost;
    return {
      rev, cost, profit, loss, pat, app, reg, sal,
      margin: rev ? profit / rev * 100 : 0,
      lossPct: rev ? loss / rev * 100 : 0,
      arpp: pat ? rev / pat : 0,
      arpa: app ? rev / app : 0,
      count: rows.length,
    };
  }

  function rowsInRange(ts0, ts1) { return buildDaily().filter(d => d.ts >= ts0 && d.ts <= ts1); }

  function compute(range) {
    const cur = aggregate(rowsInRange(range.ts0, range.ts1));
    const dayMs = 86400000;
    const len = Math.round((range.ts1 - range.ts0) / dayMs) + 1;
    const prevEnd = range.ts0 - dayMs;
    const prevStart = prevEnd - (len - 1) * dayMs;
    const prev = aggregate(rowsInRange(prevStart, prevEnd));
    const growth = (a, b) => (b > 0 ? (a - b) / b * 100 : (a > 0 ? 100 : 0));
    cur.revGrowth = growth(cur.rev, prev.rev);
    cur.profitGrowth = growth(cur.profit, prev.profit);
    cur.prev = prev;
    return cur;
  }

  function bucketKey(d, gran) {
    const y = d.getFullYear(), m = d.getMonth();
    if (gran === "day") return { key: iso(d), label: d.toLocaleDateString("en-IN", { day: "2-digit", month: "short" }), order: +d };
    if (gran === "week") { const mon = new Date(d); const off = (mon.getDay() + 6) % 7; mon.setDate(mon.getDate() - off); return { key: iso(mon), label: mon.toLocaleDateString("en-IN", { day: "2-digit", month: "short" }), order: +mon }; }
    if (gran === "month") return { key: y + "-" + m, label: d.toLocaleDateString("en-IN", { month: "short", year: "2-digit" }), order: y * 12 + m };
    if (gran === "quarter") { const q = Math.floor(m / 3) + 1; return { key: y + "-Q" + q, label: "Q" + q + " '" + String(y).slice(2), order: y * 4 + q }; }
    return { key: "" + y, label: "" + y, order: y };
  }

  function trend(range, gran) {
    const rows = rowsInRange(range.ts0, range.ts1);
    const map = new Map();
    rows.forEach(d => {
      const k = bucketKey(d.dobj, gran);
      let b = map.get(k.key);
      if (!b) { b = { label: k.label, order: k.order, rev: 0, profit: 0, loss: 0 }; map.set(k.key, b); }
      b.rev += d.revenue; b.profit += (d.revenue - d.cost); b.loss += Math.max(0, d.cost - d.revenue);
    });
    return [...map.values()].sort((a, b) => a.order - b.order);
  }

  /* ---- date-slicer presets (relative to TODAY) ---- */
  function R(start, end, label) { return { ts0: +new Date(start.getFullYear(), start.getMonth(), start.getDate()), ts1: +new Date(end.getFullYear(), end.getMonth(), end.getDate()), start: iso(start), end: iso(end), label }; }
  function addDays(d, n) { const x = new Date(d); x.setDate(x.getDate() + n); return x; }

  const PRESETS = {
    today:        () => R(TODAY, TODAY, "Today"),
    yesterday:    () => R(addDays(TODAY, -1), addDays(TODAY, -1), "Yesterday"),
    last7:        () => R(addDays(TODAY, -6), TODAY, "Last 7 Days"),
    last30:       () => R(addDays(TODAY, -29), TODAY, "Last 30 Days"),
    thisWeek:     () => { const off = (TODAY.getDay() + 6) % 7; return R(addDays(TODAY, -off), TODAY, "This Week"); },
    lastWeek:     () => { const off = (TODAY.getDay() + 6) % 7; const s = addDays(TODAY, -off - 7); return R(s, addDays(s, 6), "Last Week"); },
    thisMonth:    () => R(new Date(TODAY.getFullYear(), TODAY.getMonth(), 1), TODAY, "This Month"),
    prevMonth:    () => R(new Date(TODAY.getFullYear(), TODAY.getMonth() - 1, 1), new Date(TODAY.getFullYear(), TODAY.getMonth(), 0), "Previous Month"),
    thisQuarter:  () => { const q = Math.floor(TODAY.getMonth() / 3); return R(new Date(TODAY.getFullYear(), q * 3, 1), TODAY, "This Quarter"); },
    prevQuarter:  () => { const q = Math.floor(TODAY.getMonth() / 3); return R(new Date(TODAY.getFullYear(), q * 3 - 3, 1), new Date(TODAY.getFullYear(), q * 3, 0), "Previous Quarter"); },
    thisYear:     () => R(new Date(TODAY.getFullYear(), 0, 1), TODAY, "This Year"),
    prevYear:     () => R(new Date(TODAY.getFullYear() - 1, 0, 1), new Date(TODAY.getFullYear() - 1, 11, 31), "Previous Year"),
  };
  const PRESET_ORDER = [
    ["today", "Today"], ["yesterday", "Yesterday"], ["thisWeek", "This Week"], ["lastWeek", "Last Week"], ["last7", "Last 7 Days"], ["last30", "Last 30 Days"],
    ["thisMonth", "This Month"], ["prevMonth", "Previous Month"], ["thisQuarter", "This Quarter"], ["prevQuarter", "Previous Quarter"],
    ["thisYear", "This Year"], ["prevYear", "Previous Year"], ["customDate", "Custom Date"], ["customRange", "Custom Date Range"],
  ];

  function autoGran(range) {
    const days = Math.round((range.ts1 - range.ts0) / 86400000) + 1;
    if (days <= 31) return "day";
    if (days <= 120) return "week";
    if (days <= 760) return "month";
    return "quarter";
  }

  /* ---- formatting ---- */
  function inr(n) {
    n = Math.round(n || 0);
    const neg = n < 0; n = Math.abs(n);
    let s;
    if (n >= 1e7) s = "₹" + (n / 1e7).toFixed(2) + " Cr";
    else if (n >= 1e5) s = "₹" + (n / 1e5).toFixed(2) + " L";
    else s = "₹" + n.toLocaleString("en-IN");
    return (neg ? "-" : "") + s;
  }
  function pctStr(n) { return (n >= 0 ? "+" : "") + n.toFixed(1) + "%"; }
  function num(n) { return Math.round(n).toLocaleString("en-IN"); }

  /* =================================================================
     2) EXECUTIVE DASHBOARD
     ================================================================= */
  let state = { preset: "last30", gran: "auto", custStart: iso(addDays(TODAY, -29)), custEnd: iso(TODAY), custDate: iso(TODAY) };
  try { const saved = JSON.parse(localStorage.getItem("ankuran_dash") || "null"); if (saved) state = Object.assign(state, saved); } catch (e) {}
  function saveState() { try { localStorage.setItem("ankuran_dash", JSON.stringify(state)); } catch (e) {} }

  function currentRange() {
    if (state.preset === "customDate") { const d = new Date(state.custDate + "T00:00:00"); return R(d, d, "Custom · " + state.custDate); }
    if (state.preset === "customRange") {
      let a = new Date(state.custStart + "T00:00:00"), b = new Date(state.custEnd + "T00:00:00");
      if (b < a) { const t = a; a = b; b = t; }
      return R(a, b, "Custom range");
    }
    return (PRESETS[state.preset] || PRESETS.last30)();
  }
  function effGran(range) { return state.gran === "auto" ? autoGran(range) : state.gran; }

  function renderExec(host) {
    if (window.destroyCharts) window.destroyCharts();
    host.innerHTML = `
      ${pageHead("Executive Dashboard", "Ankuran · business intelligence & financial monitoring",
        `<span class="badge badge--accent" style="padding:7px 12px">${icon("shield", 14)} ${esc(roleLabel())} view</span>`)}

      <div class="slicer" id="slicer">
        <div class="slicer__l">${icon("calendar", 16)} <span>Date range</span></div>
        <select class="input select slicer__preset" id="slPreset">
          ${PRESET_ORDER.map(([k, lbl]) => `<option value="${k}" ${state.preset === k ? "selected" : ""}>${esc(lbl)}</option>`).join("")}
        </select>
        <div class="slicer__custom" id="slCustomDate" ${state.preset === "customDate" ? "" : "hidden"}>
          <input type="date" class="input" id="slDate" value="${state.custDate}" min="2024-06-01" max="2026-06-06">
        </div>
        <div class="slicer__custom" id="slCustomRange" ${state.preset === "customRange" ? "" : "hidden"}>
          <input type="date" class="input" id="slStart" value="${state.custStart}" min="2024-06-01" max="2026-06-06">
          <span class="slicer__to">to</span>
          <input type="date" class="input" id="slEnd" value="${state.custEnd}" min="2024-06-01" max="2026-06-06">
        </div>
        <div class="slicer__sp"></div>
        <div class="slicer__range" id="slLabel"></div>
      </div>

      <div class="exec-sec"><h2>Financial Performance</h2></div>
      <div class="kpi-grid kpi-grid--exec" id="kpiFin"></div>

      <div class="exec-sec"><h2>Business Analytics</h2></div>
      <div class="kpi-grid kpi-grid--exec" id="kpiBiz"></div>

      <div class="exec-sec exec-sec--trend">
        <h2>Trend Analysis</h2>
        <div class="gran" id="granToggle">
          ${[["day", "Daily"], ["week", "Weekly"], ["month", "Monthly"], ["quarter", "Quarterly"], ["year", "Yearly"], ["auto", "Auto"]].map(([k, lbl]) =>
            `<button class="gran__b ${state.gran === k ? "active" : ""}" data-gran="${k}">${lbl}</button>`).join("")}
        </div>
      </div>
      <div class="dash-grid">
        <section class="dash-card dash-wide">
          <header class="dash-card__h"><div><h3>Revenue · Profit · Loss</h3><p id="trendSub">Trend over the selected period</p></div></header>
          <div class="dash-canvas" style="height:320px"><canvas id="chTrend"></canvas></div>
        </section>
        <section class="dash-card">
          <header class="dash-card__h"><div><h3>Revenue Mix</h3><p>By head · selected period</p></div></header>
          <div class="dash-canvas" style="height:300px"><canvas id="chMix"></canvas></div>
        </section>
        <section class="dash-card">
          <header class="dash-card__h"><div><h3>Revenue by Department</h3><p>Selected period</p></div></header>
          <div class="dash-canvas" style="height:300px"><canvas id="chDept"></canvas></div>
        </section>
        <section class="dash-card dash-wide">
          <header class="dash-card__h"><div><h3>Patient Registrations</h3><p id="regSub">By period</p></div></header>
          <div class="dash-canvas" style="height:280px"><canvas id="chReg"></canvas></div>
        </section>
      </div>`;

    // ---- bind slicer ----
    const sel = host.querySelector("#slPreset");
    sel.addEventListener("change", () => {
      state.preset = sel.value;
      host.querySelector("#slCustomDate").hidden = state.preset !== "customDate";
      host.querySelector("#slCustomRange").hidden = state.preset !== "customRange";
      saveState(); refresh(host);
    });
    const onCust = () => {
      state.custDate = host.querySelector("#slDate").value || state.custDate;
      state.custStart = host.querySelector("#slStart").value || state.custStart;
      state.custEnd = host.querySelector("#slEnd").value || state.custEnd;
      saveState(); refresh(host);
    };
    host.querySelector("#slDate").addEventListener("change", onCust);
    host.querySelector("#slStart").addEventListener("change", onCust);
    host.querySelector("#slEnd").addEventListener("change", onCust);

    host.querySelectorAll("#granToggle .gran__b").forEach(b => b.addEventListener("click", () => {
      state.gran = b.dataset.gran;
      host.querySelectorAll("#granToggle .gran__b").forEach(x => x.classList.toggle("active", x === b));
      saveState(); refresh(host);
    }));

    refresh(host);
  }

  function refresh(host) {
    let range, m;
    try { range = currentRange(); m = compute(range); }
    catch (e) { host.querySelector("#kpiFin").innerHTML = `<div class="card card-pad" style="grid-column:1/-1;color:var(--danger)">Could not compute metrics.</div>`; return; }

    const lbl = host.querySelector("#slLabel");
    if (lbl) lbl.innerHTML = `<b>${esc(range.label)}</b><span>${esc(range.start)} → ${esc(range.end)} · ${m.count} day(s)</span>`;

    // ---- Financial Performance KPIs ----
    const fin = [
      { label: "Total Revenue", value: inr(m.rev), icon: "rupee", color: "#1f9268", soft: "#e2f3ec", delta: pctStr(m.revGrowth), dir: m.revGrowth >= 0 ? "up" : "down", deltaNote: "vs prev period" },
      { label: "Total Profit", value: inr(m.profit), icon: "chart", color: "#3e72ad", soft: "#e6eef7", delta: pctStr(m.profitGrowth), dir: m.profitGrowth >= 0 ? "up" : "down", deltaNote: "vs prev period" },
      { label: "Profit Margin", value: m.margin.toFixed(1), unit: "%", icon: "activity", color: "#6a59c2", soft: "#eae7f8", note: "of total revenue" },
      { label: "Total Loss", value: inr(m.loss), icon: "trash", color: "#c4453f", soft: "#f9e6e4", note: "loss-making days" },
      { label: "Loss Percentage", value: m.lossPct.toFixed(1), unit: "%", icon: "arrowDown", color: "#b9772b", soft: "#f8edda", note: "of total revenue" },
      { label: "Revenue Growth Rate", value: pctStr(m.revGrowth), icon: "chart", color: "#2c8aa6", soft: "#e1f0f4", delta: pctStr(m.revGrowth), dir: m.revGrowth >= 0 ? "up" : "down", deltaNote: "period over period" },
      { label: "Profit Growth Rate", value: pctStr(m.profitGrowth), icon: "chart", color: "#1f9268", soft: "#e2f3ec", delta: pctStr(m.profitGrowth), dir: m.profitGrowth >= 0 ? "up" : "down", deltaNote: "period over period" },
    ];
    host.querySelector("#kpiFin").innerHTML = fin.map(k => ui.kpi(k)).join("");

    // ---- Business Analytics KPIs ----
    const biz = [
      { label: "Total Patients", value: num(m.pat), icon: "users", color: "#3e72ad", soft: "#e6eef7", note: "in period" },
      { label: "New Registrations", value: num(m.reg), icon: "userPlus", color: "#1f9268", soft: "#e2f3ec", note: "in period" },
      { label: "Total Appointments", value: num(m.app), icon: "calendar", color: "#6a59c2", soft: "#eae7f8", note: "in period" },
      { label: "Sales Transactions", value: num(m.sal), icon: "receipt", color: "#b9772b", soft: "#f8edda", note: "billed" },
      { label: "Avg Revenue / Patient", value: inr(m.arpp), icon: "rupee", color: "#2c8aa6", soft: "#e1f0f4", note: "ARPP" },
      { label: "Avg Revenue / Appointment", value: inr(m.arpa), icon: "rupee", color: "#9d5c63", soft: "#f3e7e8", note: "ARPA" },
    ];
    host.querySelector("#kpiBiz").innerHTML = biz.map(k => ui.kpi(k)).join("");

    // accent stripes
    const KC = ["#1f9268", "#3e72ad", "#6a59c2", "#c4453f", "#b9772b", "#2c8aa6", "#1f9268"];
    host.querySelectorAll("#kpiFin > .kpi").forEach((el, i) => el.style.setProperty("--kc", KC[i % KC.length]));
    host.querySelectorAll("#kpiBiz > .kpi").forEach((el, i) => el.style.setProperty("--kc", KC[i % KC.length]));

    drawCharts(host, range, m);
  }

  function killChart(cv) { try { const c = window.Chart && Chart.getChart && Chart.getChart(cv); if (c) c.destroy(); } catch (e) {} }

  function drawCharts(host, range, m) {
    if (!window.Chart || !window.makeChart) {
      host.querySelectorAll(".dash-canvas").forEach(c => { c.innerHTML = `<div class="empty" style="padding:30px">Charts library unavailable.</div>`; });
      return;
    }
    const gran = effGran(range);
    const tr = trend(range, gran);
    const sub = host.querySelector("#trendSub"); if (sub) sub.textContent = ({ day: "Daily", week: "Weekly", month: "Monthly", quarter: "Quarterly", year: "Yearly" }[gran] || "") + " trend · " + range.label;
    const regSub = host.querySelector("#regSub"); if (regSub) regSub.textContent = "By " + gran + " · " + range.label;

    setTimeout(() => {
      const gridYc = () => (window.axisY ? window.axisY({ fmt: v => "₹" + (Math.abs(v) >= 1e5 ? (v / 1e5).toFixed(1) + "L" : (v / 1000).toFixed(0) + "k") }) : {});
      const gridY = () => (window.axisY ? window.axisY() : {});
      const xA = () => (window.axisX ? window.axisX() : {});
      const labels = tr.map(b => b.label);
      const C = id => document.getElementById(id);

      // Trend — Revenue / Profit / Loss
      const cvT = C("chTrend");
      if (cvT) {
        killChart(cvT);
        makeChart(cvT, {
          type: "line",
          data: {
            labels,
            datasets: [
              { label: "Revenue", data: tr.map(b => Math.round(b.rev)), borderColor: PALETTE[0], backgroundColor: A(PALETTE[0], .12), fill: true, tension: .35, borderWidth: 2.4, pointRadius: labels.length > 40 ? 0 : 2.5, pointHoverRadius: 5 },
              { label: "Profit", data: tr.map(b => Math.round(b.profit)), borderColor: PALETTE[1], backgroundColor: A(PALETTE[1], .08), fill: true, tension: .35, borderWidth: 2.4, pointRadius: labels.length > 40 ? 0 : 2.5, pointHoverRadius: 5 },
              { label: "Loss", data: tr.map(b => Math.round(b.loss)), borderColor: PALETTE[4], backgroundColor: A(PALETTE[4], .10), fill: true, tension: .35, borderWidth: 2, pointRadius: labels.length > 40 ? 0 : 2, pointHoverRadius: 5 },
            ],
          },
          options: { interaction: { mode: "index", intersect: false }, plugins: { legend: { display: true, position: "bottom", labels: { usePointStyle: true, pointStyle: "circle", padding: 14 } } }, scales: { x: xA(), y: gridYc() } },
        });
      }

      // Revenue mix (proportional, scaled to period revenue)
      const cvM = C("chMix");
      if (cvM) {
        killChart(cvM);
        const parts = [0.18, 0.24, 0.16, 0.42].map(p => Math.round(m.rev * p));
        makeChart(cvM, {
          type: "doughnut",
          data: { labels: ["Consultation", "Investigation", "Pharmacy", "Procedure / IVF"], datasets: [{ data: parts, backgroundColor: [PALETTE[0], PALETTE[1], PALETTE[3], PALETTE[2]], borderWidth: 0, hoverOffset: 6 }] },
          options: { cutout: "60%", plugins: { legend: { display: true, position: "bottom", labels: { padding: 12, usePointStyle: true, pointStyle: "circle" } } } },
        });
      }

      // Revenue by department
      const cvD = C("chDept");
      if (cvD) {
        killChart(cvD);
        const parts = [0.20, 0.26, 0.14, 0.34, 0.06].map(p => Math.round(m.rev * p));
        makeChart(cvD, {
          type: "bar",
          data: { labels: ["OPD", "Investig.", "Pharmacy", "IVF", "Other"], datasets: [{ data: parts, backgroundColor: PALETTE[1], borderRadius: 6, maxBarThickness: 46 }] },
          options: { scales: { x: xA(), y: gridYc() } },
        });
      }

      // Registrations trend
      const cvR = C("chReg");
      if (cvR) {
        killChart(cvR);
        const rows = rowsInRange(range.ts0, range.ts1);
        const map = new Map();
        rows.forEach(d => { const k = bucketKey(d.dobj, gran); let b = map.get(k.key); if (!b) { b = { label: k.label, order: k.order, v: 0 }; map.set(k.key, b); } b.v += d.newRegs; });
        const rg = [...map.values()].sort((a, b) => a.order - b.order);
        makeChart(cvR, {
          type: "bar",
          data: { labels: rg.map(b => b.label), datasets: [{ data: rg.map(b => b.v), backgroundColor: A(PALETTE[3], .85), borderRadius: 5, maxBarThickness: 34 }] },
          options: { scales: { x: xA(), y: gridY() } },
        });
      }
    }, 40);
  }

  /* =================================================================
     3) OPERATIONAL / CLINICAL DASHBOARD  (non-financial roles)
     ================================================================= */
  const ROLE_CATS = {
    "Receptionist": ["ops", "opsmoney"],
    "Doctor":       ["ops", "clinical"],
    "Embryologist": ["clinical", "lab"],
    "Nurse":        ["ops", "clinical"],
  };
  const SUBTITLE = { "Receptionist": "Front-desk operations", "Doctor": "Clinical & schedule overview", "Embryologist": "Laboratory & embryology overview", "Nurse": "Ward & clinical overview" };

  const OPS_KPIS = [
    { cat: "ops",      a: { label: "Today's Appointments", value: 18, icon: "calendar", color: "#3e72ad", soft: "#e6eef7", note: "4 waiting · 12 done" } },
    { cat: "ops",      a: { label: "New Registrations (Today)", value: 9, icon: "userPlus", color: "#1f9268", soft: "#e2f3ec", note: "front desk" } },
    { cat: "ops",      a: { label: "Patient Check-ins", value: 23, icon: "check", color: "#2c8aa6", soft: "#e1f0f4", note: "6 awaiting vitals" } },
    { cat: "ops",      a: { label: "Patient Queue (Now)", value: 5, icon: "users", color: "#6a59c2", soft: "#eae7f8", note: "avg wait 12 min" } },
    { cat: "opsmoney", a: { label: "Today's Collection", value: "₹56,550", icon: "rupee", color: "#b9772b", soft: "#f8edda", note: "Cash · Card · UPI" } },
    { cat: "opsmoney", a: { label: "Pending Payments / Dues", value: "₹18,200", icon: "clock", color: "#c4453f", soft: "#f9e6e4", note: "7 patients" } },
    { cat: "clinical", a: { label: "Active IVF Cycles", value: 23, icon: "stethoscope", color: "#2c8aa6", soft: "#e1f0f4", note: "8 in stimulation" } },
    { cat: "clinical", a: { label: "Pending Investigations", value: 9, icon: "flask", color: "#c4453f", soft: "#f9e6e4", note: "2 awaiting report" } },
    { cat: "lab",      a: { label: "Samples in Lab", value: 14, icon: "flask", color: "#7a6aa8", soft: "#ece8f4", note: "5 for processing" } },
    { cat: "lab",      a: { label: "Today's Lab Procedures", value: 6, icon: "activity", color: "#2f8f6b", soft: "#e4f1ea", note: "2 OPU · 3 ET · 1 ICSI" } },
  ];
  const OPS_CHARTS = [
    { cat: "ops",      id: "chReg2", title: "Patient Registrations", sub: "Last 12 months", wide: true, h: 280 },
    { cat: "ops",      id: "chDoc2", title: "Appointments by Consultant", sub: "This week", h: 300 },
    { cat: "clinical", id: "chInv2", title: "Investigations by Group", sub: "This month", h: 300 },
    { cat: "lab",      id: "chStage2", title: "Embryology — cases by stage", sub: "Active cycles", h: 300 },
  ];

  function renderOps(host) {
    if (window.destroyCharts) window.destroyCharts();
    const r = role();
    const cats = ROLE_CATS[r] || ["ops", "clinical"];
    const okC = c => cats.indexOf(c) !== -1;
    const kpis = OPS_KPIS.filter(k => okC(k.cat));
    const charts = OPS_CHARTS.filter(c => okC(c.cat));
    const card = c => `<section class="dash-card ${c.wide ? "dash-wide" : ""}"><header class="dash-card__h"><div><h3>${c.title}</h3><p>${c.sub}</p></div></header><div class="dash-canvas" style="height:${c.h}px"><canvas id="${c.id}"></canvas></div></section>`;

    host.innerHTML = `
      ${pageHead("Dashboard", "Ankuran · " + (SUBTITLE[r] || "overview"), `<span class="badge badge--accent" style="padding:7px 12px">${icon("shield", 14)} ${esc(roleLabel())} view</span>`)}
      <div class="kpi-grid kpi-grid--exec" id="opsKpi">${kpis.map(k => ui.kpi(k.a)).join("")}</div>
      ${charts.length ? `<div class="dash-grid">${charts.map(card).join("")}</div>` : ""}`;

    const KC = ["#3e72ad", "#1f9268", "#b9772b", "#6a59c2", "#2c8aa6", "#c4453f"];
    host.querySelectorAll("#opsKpi > .kpi").forEach((el, i) => el.style.setProperty("--kc", KC[i % KC.length]));

    if (!window.Chart || !window.makeChart) return;
    setTimeout(() => {
      const gridY = () => (window.axisY ? window.axisY() : {});
      const xA = () => (window.axisX ? window.axisX() : {});
      const C = id => document.getElementById(id);
      if (C("chReg2")) makeChart(C("chReg2"), { type: "bar", data: { labels: ["Jul", "Aug", "Sep", "Oct", "Nov", "Dec", "Jan", "Feb", "Mar", "Apr", "May", "Jun"], datasets: [{ data: [38, 42, 47, 51, 44, 49, 58, 61, 55, 64, 72, 64], backgroundColor: A(PALETTE[3], .85), borderRadius: 5, maxBarThickness: 34 }] }, options: { scales: { x: xA(), y: gridY() } } });
      if (C("chDoc2")) makeChart(C("chDoc2"), { type: "bar", data: { labels: ["Dr. Anjali Mehta", "Dr. Vikram Sen", "Dr. Rohan Kapoor", "Dr. Sneha Iyer"], datasets: [{ data: [42, 31, 24, 18], backgroundColor: PALETTE[0], borderRadius: 6, maxBarThickness: 30 }] }, options: { indexAxis: "y", scales: { x: gridY(), y: xA() } } });
      if (C("chInv2")) makeChart(C("chInv2"), { type: "bar", data: { labels: ["Hormone", "Haematology", "USG", "Biochem", "Serology"], datasets: [{ data: [42, 31, 26, 19, 12], backgroundColor: PALETTE[5], borderRadius: 6, maxBarThickness: 40 }] }, options: { scales: { x: xA(), y: gridY() } } });
      if (C("chStage2")) makeChart(C("chStage2"), { type: "bar", data: { labels: ["OPU", "Fertilization", "Cleavage", "Blastocyst", "Frozen", "Transfer"], datasets: [{ data: [8, 7, 6, 5, 9, 4], backgroundColor: PALETTE[3], borderRadius: 6, maxBarThickness: 38 }] }, options: { scales: { x: xA(), y: gridY() } } });
    }, 40);
  }

  /* =================================================================
     entry point
     ================================================================= */
  P.dashboard = function (host) {
    try {
      if (isFinRole()) renderExec(host);
      else renderOps(host);
    } catch (e) {
      host.innerHTML = `<div class="card card-pad" style="color:var(--danger)">Dashboard error: ${esc(e.message)}</div>`;
      console.error(e);
    }
  };

  // expose engine (useful for finance reports / future backend swap)
  window.HMS_FIN = { compute, trend, currentRange, presets: PRESETS, daily: buildDaily, inr };

  // Programmatic date-slicer control — lets the assistant apply a preset before navigating.
  window.HMS_DASH_LABEL = function (preset) {
    const item = PRESET_ORDER.find(p => p[0] === preset);
    return item ? item[1] : "";
  };
  window.HMS_DASH_SET = function (preset) {
    if (!preset || !PRESETS[preset]) return false;
    state.preset = preset;
    state.gran = "auto";
    saveState();
    // if the executive dashboard is currently on screen, re-render it live
    try {
      const host = document.getElementById("hcontent");
      if (host && host.querySelector("#slicer") && isFinRole()) renderExec(host);
    } catch (e) {}
    return true;
  };
})();
