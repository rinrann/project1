/* ============================================================
   ANKURAN HMS — Shell controller (rail · sidebar · topbar · routing)
   ============================================================ */
(function () {
  "use strict";
  window.DB = window.DB || { TODAY: "2026-06-04" };
  window.HMS_PAGES = window.HMS_PAGES || {};

  const App = {
    root: null,
    currentItem: null,   // leaf id
    currentModule: "home",
  };
  window.HApp = App;

  const esc = (window.ui && ui.esc) || (s => String(s == null ? "" : s));

  function moduleById(id) { return SITEMAP.find(m => m.id === id); }
  function firstItemOf(mod) {
    if (!mod.groups) return null;
    for (const g of mod.groups) if (g.items && g.items.length) return g.items[0];
    return null;
  }

  // ---- routing -------------------------------------------------------
  function parseHash() { return (location.hash || "").replace(/^#/, "").trim(); }

  function go(itemId) {
    if (location.hash === "#" + itemId) route();
    else location.hash = "#" + itemId;
  }
  window.HMS_GO = go;

  function route() {
    const id = parseHash();
    if (!id || id === "home") { App.currentItem = null; App.currentModule = "home"; render(); return; }
    if (id === "dashboard") { App.currentItem = null; App.currentModule = "dashboard"; render(); return; }
    const item = HMS_ITEM(id);
    if (!item) { // maybe a module id → jump to its first item
      const mod = moduleById(id);
      if (mod && mod.home) { App.currentItem = null; App.currentModule = "home"; render(); return; }
      if (mod) {
        if (window.HMS_MODULE_ALLOWED && !HMS_MODULE_ALLOWED(mod.id)) { denyAccess(mod.label); return; }
        const fi = firstItemOf(mod); if (fi) { go(fi.id); return; }
      }
      App.currentItem = null; App.currentModule = "home"; render(); return;
    }
    if (item.external) { window.open(item.external, "_blank"); history.back(); return; }
    if (window.HMS_ITEM_ALLOWED && !HMS_ITEM_ALLOWED(id)) {
      const modId = HMS_MODULE_OF(id); const mod = moduleById(modId);
      denyAccess(mod ? mod.label : "That section"); return;
    }
    App.currentItem = id;
    App.currentModule = HMS_MODULE_OF(id);
    render();
  }

  function denyAccess(label) {
    if (window.ui && ui.toast) ui.toast((label || "That section") + " is not available for your role", "alert");
    App.currentItem = null; App.currentModule = "home";
    if (location.hash && location.hash !== "#home") location.hash = "#home";
    else render();
  }

  // ---- render --------------------------------------------------------
  function render() {
    if (window.destroyCharts) window.destroyCharts();
    const mod = moduleById(App.currentModule) || moduleById("home");
    const noSide = !!(mod.home || mod.dash);
    App.root.innerHTML = `
      <div class="hms ${noSide ? "hms--full" : ""}">
        ${rail()}
        ${noSide ? "" : sidebar(mod)}
        <div class="hmain">
          ${topbar(mod)}
          <div class="hcontent" id="hcontent"></div>
        </div>
        <div class="hflyout" id="railFlyout" hidden></div>
      </div>`;
    bindChrome();
    renderContent(mod);
    const sc = document.querySelector(".hcontent"); if (sc) sc.scrollTop = 0;
  }

  function rail() {
    return `<nav class="hrail">
      <div class="hrail__logo" title="Ankuran">A</div>
      <div class="hrail__nav">
        ${SITEMAP.map(m => {
          const ok = (window.HMS_MODULE_ALLOWED ? HMS_MODULE_ALLOWED(m.id) : true);
          return `<button class="hrail__btn ${m.id === App.currentModule ? "active" : ""} ${ok ? "" : "locked"}" data-mod="${m.id}" ${ok ? "" : 'data-locked="1"'}>
            ${icon(m.icon, 21)}<span>${esc(m.label)}</span>${ok ? "" : `<span class="hrail__lock">${icon("lock", 10)}</span>`}
          </button>`;
        }).join("")}
      </div>
      <div class="hrail__spacer"></div>
      <button class="hrail__btn hrail__btn--alice" id="railAlice" title="Ask Alice">${icon("message", 21)}<span>Alice</span></button>
      <button class="hrail__btn" id="railLogout" title="Sign out">${icon("logout", 21)}<span>Logout</span></button>
    </nav>`;
  }

  function sidebar(mod) {
    if (mod.home) {
      return `<aside class="hside">
        <div class="hside__head"><div class="ic">${icon("grid", 20)}</div><div><h2>Ankuran</h2><p>Hospital Management</p></div></div>
        <div class="hside__scroll">
          <div class="hgroup"><div class="hgroup__t">Modules</div>
            ${SITEMAP.filter(m => m.groups).map(m => {
              const ok = (window.HMS_MODULE_ALLOWED ? HMS_MODULE_ALLOWED(m.id) : true);
              return `<a class="hitem ${ok ? "" : "locked"}" data-mod="${m.id}" ${ok ? "" : 'data-locked="1"'}><span class="dot"></span>${esc(m.label)}${ok ? "" : `<span class="ext">${icon("lock", 12)}</span>`}</a>`;
            }).join("")}
          </div>
        </div>
      </aside>`;
    }
    return `<aside class="hside">
      <div class="hside__head"><div class="ic">${icon(mod.icon, 20)}</div><div><h2>${esc(mod.label)}</h2><p>${mod.groups.length} sub-modules</p></div></div>
      <div class="hside__scroll">
        ${mod.groups.map(g => `
          <div class="hgroup">
            ${g.label ? `<div class="hgroup__t">${esc(g.label)}</div>` : ""}
            ${g.items.map(it => `<a class="hitem ${it.id === App.currentItem ? "active" : ""}" data-item="${it.id}">
              <span class="dot"></span>${esc(it.label)}${it.external ? `<span class="ext">${icon("arrowRight", 13)}</span>` : ""}
            </a>`).join("")}
          </div>`).join("")}
      </div>
    </aside>`;
  }

  function topbar(mod) {
    const item = App.currentItem ? HMS_ITEM(App.currentItem) : null;
    let crumb = `<b>Home</b>`;
    if (item) {
      const grp = mod.groups && mod.groups.find(g => g.items.some(i => i.id === item.id));
      crumb = (grp && grp.label)
        ? `${esc(mod.label)} <span class="sep">${icon("chevRight", 13)}</span> ${esc(grp.label)} <span class="sep">${icon("chevRight", 13)}</span> <b>${esc(item.label)}</b>`
        : `${esc(mod.label)} <span class="sep">${icon("chevRight", 13)}</span> <b>${esc(item.label)}</b>`;
    } else if (!mod.home) {
      crumb = `<b>${esc(mod.label)}</b>`;
    }
    const sess = (window.HMS_SESSION && HMS_SESSION()) || null;
    const uname = (sess && sess.name) ? sess.name : "Guest User";
    const urole = (window.HMS_ROLE_LABEL ? HMS_ROLE_LABEL() : (sess && sess.role)) || "—";
    const initials = uname.replace(/^Dr\.?\s*/i, "").split(/\s+/).map(w => w[0]).filter(Boolean).slice(0, 2).join("").toUpperCase() || "U";
    const uColor = (sess && sess.color) ? sess.color : "var(--accent)";
    return `<header class="htop">
      <div class="hbread">${crumb}</div>
      <div class="htop__sp"></div>
      <div class="htop__search" id="searchWrap">${icon("search", 16, "si")}<input id="globalSearch" placeholder="Search pages, patients, doctors, bills…" autocomplete="off"><div class="hsearch-pop" id="searchPop" hidden></div></div>
      <button class="icon-btn" id="topBell" title="Notifications" style="background:transparent;border:none;color:var(--text-3);cursor:pointer">${icon("bell", 19)}</button>
      <button class="htop__user" id="topUser" type="button" style="border:none;background:transparent;cursor:pointer">
        <div class="nm"><b>${esc(uname)}</b><span>${esc(urole)}</span></div>
        <div class="avatar" style="width:36px;height:36px;background:${uColor};font-size:13px">${esc(initials)}</div>
      </button>
    </header>`;
  }

  function bindChrome() {
    document.querySelectorAll("[data-mod]").forEach(b => b.addEventListener("click", () => {
      const mod = moduleById(b.dataset.mod);
      if (!mod) return;
      if (b.dataset.locked) { if (window.ui && ui.toast) ui.toast(esc(mod.label) + " is not available for your role", "alert"); return; }
      if (mod.home) { go("home"); return; }
      if (mod.dash) { go("dashboard"); return; }
      const fi = firstItemOf(mod);
      if (fi) go(fi.id); else { App.currentModule = mod.id; App.currentItem = null; render(); }
    }));
    document.querySelectorAll("[data-item]").forEach(a => a.addEventListener("click", () => go(a.dataset.item)));
    setupFlyout();
    setupSearch();
    const al = document.getElementById("railAlice");
    if (al) al.addEventListener("click", () => { try { window.AnkuranAssistant && AnkuranAssistant.open(); } catch (e) {} });
    const lo = document.getElementById("railLogout");
    if (lo) lo.addEventListener("click", () => {
      try { localStorage.removeItem("ankuran_session"); localStorage.removeItem("ankuran_role"); } catch (e) {}
      window.location.href = "login.html";
    });
  }

  function setupFlyout() {
    const fly = document.getElementById("railFlyout");
    if (!fly) return;
    let hideT = null;
    const scheduleHide = () => { clearTimeout(hideT); hideT = setTimeout(() => { fly.hidden = true; }, 170); };
    const cancelHide = () => clearTimeout(hideT);

    function open(btn, mod) {
      cancelHide();
      fly.innerHTML = `
        <div class="hfly__head">${esc(mod.label)}</div>
        <div class="hfly__cols">
          ${mod.groups.map(g => `
            <div class="hfly__col">
              ${g.label ? `<div class="hfly__gh" data-item="${g.items[0].id}">${esc(g.label)}</div>` : ""}
              ${g.items.map(it => `<a class="hfly__item ${it.id === App.currentItem ? "active" : ""}" data-item="${it.id}">${esc(it.label)}${it.external ? " ↗" : ""}</a>`).join("")}
            </div>`).join("")}
        </div>`;
      fly.querySelectorAll("[data-item]").forEach(el => el.addEventListener("click", () => { fly.hidden = true; go(el.dataset.item); }));
      fly.hidden = false;
      // position: to the right of the rail, vertically aligned to the button, clamped to viewport
      const r = btn.getBoundingClientRect();
      fly.style.left = Math.round(r.right - 2) + "px";
      fly.style.top = "0px";
      const fh = fly.offsetHeight, vh = window.innerHeight;
      let top = r.top;
      if (top + fh > vh - 10) top = vh - 10 - fh;
      if (top < 10) top = 10;
      fly.style.top = Math.round(top) + "px";
    }

    document.querySelectorAll(".hrail__btn[data-mod]").forEach(btn => {
      const mod = moduleById(btn.dataset.mod);
      if (!mod || !mod.groups) return; // Home / Dashboard have no sub-menu
      if (window.HMS_MODULE_ALLOWED && !HMS_MODULE_ALLOWED(mod.id)) return; // locked → no flyout
      btn.addEventListener("mouseenter", () => open(btn, mod));
      btn.addEventListener("mouseleave", scheduleHide);
    });
    fly.addEventListener("mouseenter", cancelHide);
    fly.addEventListener("mouseleave", scheduleHide);
  }

  function buildSearchIndex() {
    const out = [];
    const common = new Set(["opd-reg", "opd-list", "v-appt", "inv-reqbill", "ph-issue", "ac-advance"]);
    SITEMAP.forEach(m => {
      if (!m.groups) return;
      m.groups.forEach(g => g.items.forEach(it => {
        if (it.external) return;
        out.push({ id: it.id, label: it.label, sub: m.label + " · " + g.label, hay: (it.label + " " + m.label + " " + g.label).toLowerCase(), common: common.has(it.id) });
      }));
    });
    Object.keys(window.HMS_VITEMS || {}).forEach(k => {
      const it = window.HMS_VITEMS[k];
      const mod = moduleById(it.module);
      out.push({ id: it.id, label: it.label, sub: (mod ? mod.label : "") + " · Shortcut", hay: (it.label + " " + (mod ? mod.label : "")).toLowerCase(), common: common.has(it.id) });
    });

    // ---- Record-level entries (sample data; replace with live results post-backend) ----
    // Patients → Patient List
    [["REG-100231", "Priya Sharma"], ["REG-100230", "Meera Patel"], ["REG-100229", "Imran Sheikh"],
     ["REG-100228", "Divya Nair"], ["REG-100227", "Aisha Khan"]].forEach(([reg, nm]) => {
      out.push({ id: "opd-list", label: nm, sub: "Patient · " + reg, kind: "Patient", hay: (nm + " " + reg + " patient").toLowerCase() });
    });
    // Doctors → Doctor Master
    ["Dr. Anjali Mehta", "Dr. Rohan Kapoor", "Dr. Sneha Iyer", "Dr. Vikram Sen"].forEach(nm => {
      out.push({ id: "opd-docmaster", label: nm, sub: "Consultant · Doctor Master", kind: "Doctor", hay: (nm + " doctor consultant").toLowerCase() });
    });
    // Invoices → Patient Invoice List
    [["INV-5001", "Priya Sharma"], ["INV-5002", "Meera Patel"], ["INV-5003", "Imran Sheikh"], ["INV-5004", "Aisha Khan"]].forEach(([inv, nm]) => {
      out.push({ id: "opd-invoice", label: inv, sub: "Invoice · " + nm, kind: "Invoice", hay: (inv + " " + nm + " invoice bill").toLowerCase() });
    });
    // Requisition / investigation bills → Requisition List
    [["REQ-3001", "Priya Sharma"], ["REQ-3002", "Meera Patel"], ["REQ-3003", "Aisha Khan"]].forEach(([req, nm]) => {
      out.push({ id: "inv-reqlist", label: req, sub: "Bill · " + nm, kind: "Bill", hay: (req + " " + nm + " bill requisition").toLowerCase() });
    });
    return out;
  }

  function setupSearch() {
    const input = document.getElementById("globalSearch");
    const pop = document.getElementById("searchPop");
    if (!input || !pop) return;
    const idx = buildSearchIndex();

    function draw(q) {
      q = (q || "").trim().toLowerCase();
      let pool = idx;
      if (window.HMS_ITEM_ALLOWED) pool = idx.filter(x => HMS_ITEM_ALLOWED(x.id));
      const list = q ? pool.filter(x => x.hay.includes(q)).slice(0, 9) : pool.filter(x => x.common).slice(0, 6);
      if (!list.length) {
        pop.innerHTML = `<div class="hsearch-empty">No match for “<b>${esc(q)}</b>”. Try a page (“registration”, “stock”), a patient or doctor name, or a bill / invoice no.</div>`;
        pop.hidden = false; return;
      }
      pop.innerHTML = `<div class="hsearch-h">${q ? "Results" : "Quick access — search pages, patients, doctors, bills…"}</div>` +
        list.map(x => `<a class="hsearch-item" data-item="${x.id}"><span class="t">${esc(x.label)}</span><span class="s">${esc(x.sub)}</span>${x.kind ? `<span class="hsearch-tag">${esc(x.kind)}</span>` : ""}</a>`).join("");
      pop.querySelectorAll("[data-item]").forEach(a => a.addEventListener("mousedown", e => { e.preventDefault(); pop.hidden = true; input.value = ""; go(a.dataset.item); input.blur(); }));
      pop.hidden = false;
    }

    input.addEventListener("focus", () => draw(input.value));
    input.addEventListener("input", () => draw(input.value));
    input.addEventListener("blur", () => setTimeout(() => { pop.hidden = true; }, 130));
    input.addEventListener("keydown", e => {
      if (e.key === "Escape") { input.blur(); return; }
      if (e.key === "Enter") { const first = pop.querySelector("[data-item]"); if (first) { pop.hidden = true; const id = first.dataset.item; input.value = ""; input.blur(); go(id); } }
    });
  }

  function renderContent(mod) {
    const host = document.getElementById("hcontent");
    if (!App.currentItem) {
      if (mod && mod.dash) { HMS_PAGES.dashboard ? HMS_PAGES.dashboard(host) : (host.innerHTML = ""); return; }
      HMS_PAGES.home ? HMS_PAGES.home(host) : (host.innerHTML = ""); return;
    }
    const item = HMS_ITEM(App.currentItem);
    const fn = HMS_PAGES[item.page];
    if (fn) { try { fn(host, item); } catch (e) { host.innerHTML = errBox(item, e); console.error(e); } }
    else host.innerHTML = stub(item);
  }

  function stub(item) {
    return `
      <div class="page-head"><div class="page-head__t"><h1>${esc(item.label)}</h1><p>Legacy screen — faithful rebuild in progress</p></div></div>
      <div class="card"><div class="lstub">
        <div class="ic">${icon("fileText", 22)}</div>
        <div>
          <h3>${esc(item.label)}</h3>
          <p>This screen is being reproduced field-for-field from the original ASP.NET source — every input, button and grid column carried across into the new theme. It will appear here once built.</p>
          <span class="src">Source · ${esc(item.src || "")}</span>
        </div>
      </div></div>`;
  }
  function errBox(item, e) {
    return `<div class="page-head"><div class="page-head__t"><h1>${esc(item.label)}</h1></div></div>
      <div class="card card-pad" style="color:var(--danger)">Render error: ${esc(e.message)}</div>`;
  }
  window.HMS_STUB = stub;

  // ---- boot ----------------------------------------------------------
  function boot() {
    App.root = document.getElementById("app");
    window.addEventListener("hashchange", route);
    route();
  }
  if (document.readyState === "loading") document.addEventListener("DOMContentLoaded", boot);
  else boot();
})();
