/* ============================================================
   ANKURAN HMS — Global interactive wiring
   Makes table Edit / Delete actions work everywhere, plus the
   top-bar notification bell and user menu. Attached once.
   ============================================================ */
(function () {
  "use strict";
  if (window.__hmsActionsBound) return;
  window.__hmsActionsBound = true;

  const esc = (window.ui && ui.esc) || (s => String(s == null ? "" : s));

  function rowCells(tr) {
    // all <td> except the trailing action cell
    const tds = [...tr.children];
    return tds.slice(0, tds.length - 1);
  }
  function headerLabels(table) {
    const ths = [...table.querySelectorAll("thead th")];
    return ths.slice(0, ths.length - 1).map(th => th.textContent.trim());
  }
  function primaryLabel(tr) {
    const main = tr.querySelector(".cell-main") || tr.querySelector(".cell-id") || tr.children[0];
    return main ? main.textContent.trim() : "this record";
  }

  // decrement any "N records" counters inside the same card / page header
  function refreshCounts(scope) {
    const card = scope.closest(".card");
    const heads = [];
    if (card) heads.push(...card.querySelectorAll(".sub, .badge"));
    const ph = document.querySelector(".hcontent .page-head__actions");
    if (ph) heads.push(...ph.querySelectorAll(".badge"));
    heads.forEach(el => {
      el.childNodes.forEach(node => {
        if (node.nodeType === 3 && /\d+\s+records?/i.test(node.nodeValue)) {
          node.nodeValue = node.nodeValue.replace(/(\d+)(\s+records?)/i, (m, n, rest) => (Math.max(0, parseInt(n, 10) - 1)) + rest);
        }
      });
    });
  }

  // ---- DELETE ----
  function onDelete(btn) {
    const tr = btn.closest("tr");
    const table = btn.closest("table");
    if (!tr || !table) return;
    const name = primaryLabel(tr);
    ui.modal({
      title: "Delete record?",
      subtitle: "This action cannot be undone.",
      body: `<p style="margin:0;color:var(--text-2);font-size:14px;line-height:1.55">You are about to delete <b>${esc(name)}</b>. Are you sure you want to remove this record?</p>`,
      footer: `<button class="btn btn-ghost" data-close>Cancel</button><button class="btn btn-danger" id="confirmDel">${icon("trash", 15)} Delete</button>`,
      onMount: (root) => {
        root.querySelector("#confirmDel").addEventListener("click", () => {
          const tbody = tr.parentElement;
          tr.style.transition = "opacity .18s, transform .18s";
          tr.style.opacity = "0"; tr.style.transform = "translateX(8px)";
          refreshCounts(table);
          setTimeout(() => {
            tr.remove();
            // if table now empty, show the empty row
            if (tbody && !tbody.querySelector("tr")) {
              const cols = table.querySelectorAll("thead th").length;
              tbody.innerHTML = `<tr><td colspan="${cols}"><div class="empty" style="padding:26px">No records found.</div></td></tr>`;
            }
          }, 180);
          ui.closeModal();
          ui.toast("Deleted " + name, "trash");
        });
      }
    });
  }

  // ---- EDIT ----
  function onEdit(btn) {
    const tr = btn.closest("tr");
    const table = btn.closest("table");
    if (!tr || !table) return;
    const labels = headerLabels(table);
    const cells = rowCells(tr);
    const name = primaryLabel(tr);

    const fields = labels.map((lab, i) => {
      const cell = cells[i];
      const val = cell ? cell.textContent.trim() : "";
      // if the cell holds a badge/chip, keep it read-only (status etc.)
      const isRich = cell && cell.querySelector(".badge, button, .avatar");
      return { lab, val, idx: i, ro: !!isRich };
    });

    const body = `<div class="lform-grid">${fields.map(f =>
      `<div class="lfield"><label>${esc(f.lab)}</label><input class="input" data-idx="${f.idx}" value="${esc(f.val)}" ${f.ro ? "disabled" : ""}></div>`
    ).join("")}</div>`;

    ui.modal({
      title: "Edit record",
      subtitle: name,
      wide: fields.length > 4,
      body,
      footer: `<button class="btn btn-ghost" data-close>Cancel</button><button class="btn btn-primary" id="saveEdit">${icon("check", 15)} Save changes</button>`,
      onMount: (root) => {
        root.querySelector("#saveEdit").addEventListener("click", () => {
          root.querySelectorAll("input[data-idx]").forEach(inp => {
            if (inp.disabled) return;
            const cell = cells[+inp.dataset.idx];
            if (cell) cell.textContent = inp.value;
          });
          ui.closeModal();
          ui.toast("Saved changes to " + name, "check");
        });
      }
    });
  }

  // ---- delegated listener ----
  document.addEventListener("click", (e) => {
    const del = e.target.closest(".js-row-del");
    if (del) { e.preventDefault(); onDelete(del); return; }
    const edit = e.target.closest(".js-row-edit");
    if (edit) { e.preventDefault(); onEdit(edit); return; }

    // top-bar notification bell
    const bell = e.target.closest("#topBell");
    if (bell) { e.preventDefault(); openNotifications(); return; }

    // top-bar user chip
    const user = e.target.closest("#topUser");
    if (user) { e.preventDefault(); toggleUserMenu(user); return; }
  });

  // ---- notifications ----
  function openNotifications() {
    ui.modal({
      title: "Notifications",
      subtitle: "Recent activity",
      body: `<div class="note-list">
        ${[
          ["calendar", "3 appointments waiting in OPD queue", "5 min ago"],
          ["alert", "Menopur 75 IU is below reorder level", "1 hr ago"],
          ["receipt", "Invoice INV-5003 is overdue", "2 hrs ago"],
          ["flask", "2 investigation reports pending sign-off", "Today"],
        ].map(([ic, t, time]) => `<div class="note-item"><span class="note-ic">${icon(ic, 16)}</span><div><div class="note-t">${esc(t)}</div><div class="note-time">${esc(time)}</div></div></div>`).join("")}
      </div>`,
      footer: `<button class="btn btn-ghost" data-close>Close</button><button class="btn btn-soft" id="markRead">Mark all as read</button>`,
      onMount: (root) => { root.querySelector("#markRead").addEventListener("click", () => { ui.closeModal(); ui.toast("All notifications marked as read", "check"); }); }
    });
  }

  // ---- user menu ----
  function toggleUserMenu(anchor) {
    const existing = document.getElementById("userMenu");
    if (existing) { existing.remove(); return; }
    const menu = document.createElement("div");
    menu.id = "userMenu";
    menu.className = "user-menu";
    menu.innerHTML = `
      <div class="user-menu__h"><b>Dr. Sunita Rao</b><span>Administrator · Ankuran</span></div>
      <a class="user-menu__i" data-go="sec-pwd">${icon("lock", 15)} Change password</a>
      <a class="user-menu__i" data-go="set-emp">${icon("user", 15)} My profile</a>
      <a class="user-menu__i" data-go="dashboard">${icon("activity", 15)} Dashboard</a>
      <div class="user-menu__sep"></div>
      <a class="user-menu__i danger" id="umLogout">${icon("logout", 15)} Sign out</a>`;
    document.body.appendChild(menu);
    const r = anchor.getBoundingClientRect();
    menu.style.top = Math.round(r.bottom + 8) + "px";
    menu.style.right = Math.round(window.innerWidth - r.right) + "px";
    menu.querySelectorAll("[data-go]").forEach(a => a.addEventListener("click", () => { menu.remove(); if (window.HMS_GO) HMS_GO(a.dataset.go); }));
    menu.querySelector("#umLogout").addEventListener("click", () => {
      try { localStorage.removeItem("ankuran_session"); localStorage.removeItem("ankuran_role"); } catch (e) {}
      window.location.href = "login.html";
    });
    setTimeout(() => {
      document.addEventListener("click", function close(ev) {
        if (!ev.target.closest("#userMenu") && !ev.target.closest("#topUser")) { menu.remove(); document.removeEventListener("click", close); }
      });
    }, 0);
  }

})();
