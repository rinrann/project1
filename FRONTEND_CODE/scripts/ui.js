/* ============================================================
   ANKURAN — UI helpers
   ============================================================ */
(function () {
  "use strict";

  const ui = {};

  ui.esc = (s) => String(s == null ? "" : s).replace(/[&<>"']/g, c => ({ "&":"&amp;","<":"&lt;",">":"&gt;",'"':"&quot;","'":"&#39;" }[c]));

  // ₹ formatting (Indian grouping)
  ui.inr = function (n, opts) {
    opts = opts || {};
    const v = Math.round(Number(n) || 0);
    const s = v.toLocaleString("en-IN");
    return (opts.bare ? "" : "₹") + s;
  };
  ui.inrShort = function (n) {
    const v = Number(n) || 0;
    if (v >= 1e7) return "₹" + (v / 1e7).toFixed(2).replace(/\.00$/,"") + " Cr";
    if (v >= 1e5) return "₹" + (v / 1e5).toFixed(2).replace(/\.00$/,"") + " L";
    if (v >= 1e3) return "₹" + (v / 1e3).toFixed(0) + "K";
    return "₹" + v;
  };

  ui.dateLong = function (iso) {
    if (!iso) return "—";
    const d = new Date(iso + "T00:00:00");
    return d.toLocaleDateString("en-IN", { day: "numeric", month: "short", year: "numeric" });
  };
  ui.dateShort = function (iso) {
    if (!iso) return "—";
    const d = new Date(iso + "T00:00:00");
    return d.toLocaleDateString("en-IN", { day: "numeric", month: "short" });
  };
  ui.dow = function (iso) {
    const d = new Date(iso + "T00:00:00");
    return d.toLocaleDateString("en-IN", { weekday: "short" });
  };
  ui.relDay = function (iso) {
    const today = DB.TODAY;
    if (iso === today) return "Today";
    const a = new Date(iso + "T00:00:00"), b = new Date(today + "T00:00:00");
    const diff = Math.round((a - b) / 86400000);
    if (diff === 1) return "Tomorrow";
    if (diff === -1) return "Yesterday";
    if (diff > 1 && diff < 7) return "In " + diff + " days";
    return ui.dateShort(iso);
  };

  // initials
  ui.initials = function (name) {
    const parts = String(name).replace(/^Dr\.?\s+/i, "").trim().split(/[\s&]+/).filter(Boolean);
    if (!parts.length) return "?";
    if (parts.length === 1) return parts[0].slice(0, 2).toUpperCase();
    return (parts[0][0] + parts[parts.length - 1][0]).toUpperCase();
  };

  // deterministic warm color from string
  const PALETTE = ["#9d5c63","#4a7aa7","#7a6aa8","#2f8f6b","#c4843b","#b5707a","#5f8a8b","#a76b4f"];
  ui.colorFor = function (key) {
    let h = 0; const s = String(key);
    for (let i = 0; i < s.length; i++) h = (h * 31 + s.charCodeAt(i)) >>> 0;
    return PALETTE[h % PALETTE.length];
  };

  ui.avatar = function (name, size, color) {
    const sz = size || 36;
    const c = color || ui.colorFor(name);
    return `<div class="avatar" style="width:${sz}px;height:${sz}px;background:${c};font-size:${Math.round(sz*0.36)}px">${ui.esc(ui.initials(name))}</div>`;
  };

  ui.person = function (name, sub, size, color) {
    return `<div class="cell-person">${ui.avatar(name, size, color)}<div><div class="cell-main">${ui.esc(name)}</div>${sub ? `<div class="cell-sub">${ui.esc(sub)}</div>` : ""}</div></div>`;
  };

  // status badge mapping
  const STATUS_MAP = {
    // patient
    "In Treatment": "violet", "New": "info", "Completed": "neutral",
    // cycle
    "Active": "accent", "Successful": "ok", "Unsuccessful": "danger", "Cancelled": "neutral",
    // appt
    "Confirmed": "ok", "Waiting": "warn", "Pending": "warn",
    // invoice
    "Paid": "ok", "Partial": "warn", "Overdue": "danger",
    // employee
    "On Leave": "warn",
    // embryo
    "In culture": "info", "Frozen": "violet", "Transferred": "ok", "Arrested": "danger",
  };
  ui.badge = function (text, variant) {
    const v = variant || STATUS_MAP[text] || "neutral";
    return `<span class="badge badge--${v}"><span class="bdot"></span>${ui.esc(text)}</span>`;
  };
  ui.badgePlain = function (text, variant) {
    return `<span class="badge badge--${variant||'neutral'}">${ui.esc(text)}</span>`;
  };

  // ---- toast ----
  ui.toast = function (msg, ic) {
    let wrap = document.querySelector(".toast-wrap");
    if (!wrap) { wrap = document.createElement("div"); wrap.className = "toast-wrap"; document.body.appendChild(wrap); }
    const t = document.createElement("div");
    t.className = "toast";
    t.innerHTML = `<span class="ic">${icon(ic || "check", 16)}</span>${ui.esc(msg)}`;
    wrap.appendChild(t);
    setTimeout(() => { t.style.transition = "opacity .3s, transform .3s"; t.style.opacity = "0"; t.style.transform = "translateY(8px)"; setTimeout(() => t.remove(), 300); }, 2600);
  };

  // ---- modal ----
  ui.modal = function (opts) {
    closeModal();
    const root = document.createElement("div");
    root.className = "modal-root open";
    root.id = "modalRoot";
    root.innerHTML = `
      <div class="modal-overlay" data-close></div>
      <div class="modal ${opts.wide ? "wide" : ""}" role="dialog" aria-modal="true">
        <div class="modal__head">
          <div>
            <h3>${ui.esc(opts.title)}</h3>
            ${opts.subtitle ? `<p>${ui.esc(opts.subtitle)}</p>` : ""}
          </div>
          <button class="icon-btn x" data-close aria-label="Close">${icon("x", 18)}</button>
        </div>
        <div class="modal__body">${opts.body || ""}</div>
        ${opts.footer ? `<div class="modal__foot">${opts.footer}</div>` : ""}
      </div>`;
    document.body.appendChild(root);
    root.addEventListener("click", (e) => { if (e.target.closest("[data-close]")) closeModal(); });
    if (opts.onMount) opts.onMount(root);
    return root;
  };
  function closeModal() { const m = document.getElementById("modalRoot"); if (m) m.remove(); }
  ui.closeModal = closeModal;
  document.addEventListener("keydown", (e) => { if (e.key === "Escape") closeModal(); });

  // empty state
  ui.empty = function (msg, ic) {
    return `<div class="empty"><div class="ic">${icon(ic || "search", 44)}</div><div>${ui.esc(msg)}</div></div>`;
  };

  // KPI card
  ui.kpi = function (o) {
    const deltaCls = o.dir === "down" ? "down" : "up";
    const arrow = o.dir === "down" ? "arrowDown" : "arrowUp";
    return `<div class="kpi">
      <div class="kpi__top">
        <span class="kpi__label">${ui.esc(o.label)}</span>
        <span class="kpi__icon" style="background:${o.soft};color:${o.color}">${icon(o.icon, 20)}</span>
      </div>
      <div class="kpi__value">${o.value}${o.unit ? `<small> ${o.unit}</small>` : ""}</div>
      ${o.delta ? `<div class="kpi__delta ${deltaCls}">${icon(arrow,14)} ${ui.esc(o.delta)} <span class="muted">${ui.esc(o.deltaNote||"")}</span></div>`
                : (o.note ? `<div class="kpi__delta"><span class="muted">${ui.esc(o.note)}</span></div>` : "")}
    </div>`;
  };

  window.ui = ui;
})();
