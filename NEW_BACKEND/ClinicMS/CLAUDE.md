# Ankuran HMS — Frontend → Backend Handoff (for Claude Code)

This folder is the **complete, working front-end** of **Ankuran**, a Hospital/IVF-Clinic
Management System. It is plain **HTML + CSS + vanilla JavaScript** (no build step, no
framework, no bundler). Everything runs in the browser today against **in-memory mock
data**. Your job is to add a real backend and replace the mock data with live API calls.

---

## 1. How to run the frontend

It is fully static. Serve the folder with any static server, e.g.:

```bash
npx serve .
# or
python3 -m http.server 8080
```

Then open **`login.html`**.

- Demo logins (password is `ankuran` for all). The role is detected **implicitly from the
  Employee-ID prefix**:
  - `OWN-001` → Super Admin (owner, full access)
  - `SUP-001` → Administrator
  - `DOC-007` → Doctor
  - `REC-014` → Receptionist
  - `EMB-003` → Embryologist
  - `ACC-009` → Accounts Team
  - `NUR-021` → Nurse

After login the app redirects to **`Ankuran HMS.html`** (the SPA).

---

## 2. Architecture at a glance

- **`login.html`** — standalone login screen. Calls `AnkuranAuth.authenticate()` and stores
  a session, then redirects to the app.
- **`Ankuran HMS.html`** — the single-page app shell. Loads all scripts in order and renders
  into `<div id="app">`. Routing is **hash-based** (`#opd-reg`, `#dashboard`, etc.).
- **No framework.** Each "page" is a render function that writes HTML into a host element and
  wires its own event listeners.

### Script load order (defined in `Ankuran HMS.html`) and responsibility

| File | Responsibility |
|------|----------------|
| `scripts/icons.js` | Inline SVG icon set (`icon(name,size,cls)`). |
| `scripts/ui.js` | UI helpers: `ui.toast`, `ui.modal`, `ui.kpi`, `ui.esc`, formatting. |
| `scripts/hms-facts.js` | **Central mock "facts" / business numbers** used across dashboard, reports, KB. |
| `scripts/charts.js` | Chart.js theming helpers (`makeChart`, axis helpers). |
| `scripts/hms-sitemap.js` | **The full navigation tree** — modules → sub-modules → menu items. Source of truth for routes. |
| `scripts/hms-pages.js` | Core page renderers + shared form/table builders + Patient Registration. |
| `scripts/hms-pages-masters.js` | All "Master" screens (generic master form + grid engine). |
| `scripts/hms-pages-reports.js` | All "Report" + list screens (filter bar + grid + CSV export). |
| `scripts/hms-pages-trans.js` | All "Transaction" screens (header + line-items + totals). |
| `scripts/hms-home.js` | Main-menu landing (greeting + clinic-module tiles) + a few extra pages. |
| `scripts/hms-dashboard.js` | Role-aware executive dashboard (KPI cards + charts + date slicer). |
| `scripts/hms-actions.js` | Global wiring for table **Edit/Delete**, notifications bell, user menu. |
| `scripts/hms-access.js` | **Role-based access control** (prefix→role, role→allowed modules). |
| `scripts/hms-kb.js` | Knowledge base of plain-language screen descriptions (for the assistant). |
| `scripts/hms-assistant.js` | **Alice** — the in-app NLP navigation + help assistant. |
| `scripts/hms-shell.js` | App shell: rail, sidebar, topbar, hash router, global search. |
| `scripts/auth.js` | (login page) user directory + `authenticate()` + session storage. |

> Note: the project also contains an older `app.html` + `page-*.js` set from an earlier
> design iteration. **Those are NOT part of this deliverable** and are excluded from this
> export folder. The app is `Ankuran HMS.html` only.

---

## 3. Where the data lives today (replace these with APIs)

All data is **hard-coded mock data** in the front-end. The main places:

- **`scripts/auth.js`** — `USERS[]` directory (employee id, email, password, name, role,
  title, department). Replace `AnkuranAuth.authenticate()` with a real login API + JWT/session.
- **`scripts/hms-facts.js`** — central business numbers (collections, revenue, profit, etc.)
  consumed by the dashboard and finance reports.
- **`scripts/hms-pages.js`** — `Patient Registration` sample patients, states/districts.
- **`scripts/hms-pages-masters.js`** — sample rows for every master (symptoms, doctors,
  medicines, suppliers, departments, designations, employees, roles…).
- **`scripts/hms-pages-reports.js`** — sample rows for every report/list (patients, invoices,
  requisitions, stock, collections, MIS…).
- **`scripts/hms-pages-trans.js`** — sample line-item catalogues (tests + rates, medicines +
  rates) for billing/issue/purchase screens.

Search the codebase for these array literals; each is a clean seam for an API call.

---

## 4. The navigation tree / module map

`scripts/hms-sitemap.js` is the **single source of truth** for the app's structure. Each leaf
menu item has:

```js
{ id: "opd-reg", label: "Patient Registration", page: "patientEnrollment",
  src: "OPD/PatientEnrollment.aspx" }   // src = original legacy .aspx, for reference
```

Modules (top level): **OPD Unit, Investigation, Pharmacy, Accounts, Settings, Security,
Utility**, plus **Home** and **Dashboard**. Each module has sub-modules grouped as
**Master / Transaction / Reports**. Use this file to derive your REST resource list.

---

## 5. Suggested backend surface (REST)

A natural mapping (adjust to your stack). All list endpoints should accept
`?from=&to=&q=&page=` for the existing filter bars and CSV export to keep working.

```
POST   /api/auth/login            { empId|email, password } -> { token, user, permissions }
POST   /api/auth/logout
GET    /api/auth/me

# Masters (CRUD)
GET/POST/PUT/DELETE /api/symptoms
GET/POST/PUT/DELETE /api/doctors
GET/POST/PUT/DELETE /api/medicines
GET/POST/PUT/DELETE /api/suppliers
GET/POST/PUT/DELETE /api/manufacturers
GET/POST/PUT/DELETE /api/departments
GET/POST/PUT/DELETE /api/employees
GET/POST/PUT/DELETE /api/designations
GET/POST/PUT/DELETE /api/investigations          # tests + rates
GET/POST/PUT/DELETE /api/roles

# OPD / clinical
GET/POST            /api/patients                 # registration + list
GET/POST            /api/appointments
POST                /api/prescriptions
POST                /api/injections
POST                /api/test-results
GET                 /api/patients/queue
GET                 /api/patients/:id/history

# Investigation / Pharmacy / Accounts (transactions)
POST                /api/requisitions             # investigation bill
POST                /api/pharmacy/purchases
POST                /api/pharmacy/issues          # sales/dispense
POST                /api/pharmacy/returns
POST                /api/accounts/advance-payments

# Reports / analytics (read-only, date-filtered)
GET    /api/reports/invoices
GET    /api/reports/daily-collection?from=&to=
GET    /api/reports/sales-register?from=&to=
GET    /api/reports/stock
GET    /api/dashboard/metrics?preset=thisMonth   # see §6
```

---

## 6. Dashboard date slicer (important contract)

The dashboard has a **single global date slicer** with presets:
`today, yesterday, last7, last30, thisWeek/lastWeek, thisMonth/prevMonth,
thisQuarter/prevQuarter, thisYear/prevYear, custom date, custom range`.

When the slicer changes, **all** KPI cards and charts must refresh. Today that recomputes from
mock data; for the backend, expose a single endpoint such as:

```
GET /api/dashboard/metrics?preset=prevMonth
GET /api/dashboard/metrics?from=2026-05-01&to=2026-05-31
-> { revenue, profit, profitMargin, loss, lossPct, revenueGrowth, profitGrowth,
     totalPatients, newRegistrations, totalAppointments, salesTxns,
     avgRevenuePerPatient, avgRevenuePerAppointment, trends:{ daily, weekly, monthly, ... } }
```

The chatbot ("Alice") can deep-link to the dashboard with a preset already applied via
`HMS_DASH_SET(preset)` — keep that working by feeding the same preset to your API.

---

## 7. Role-Based Access Control (must be enforced server-side too)

`scripts/hms-access.js` defines the rules the UI uses. **The frontend only greys-out / hides
things — real enforcement must happen in the backend.** Mirror this matrix:

- **Super Admin / Admin** — everything.
- **Accounts Team** — Accounts, Pharmacy, and all **financial** dashboard widgets/reports.
- **Receptionist** — OPD, Investigation, Accounts; front-desk dashboard only (today's
  appointments, registrations, check-ins, today's collection, dues). **No** revenue/profit
  analytics.
- **Doctor** — OPD, Investigation, Pharmacy; clinical/operational dashboard only. **No**
  financial metrics.
- **Embryologist** — clinical + lab; **no** financial metrics.

Roles are derived from the **Employee-ID prefix** (`OWN/SA, SUP/ADM, DOC, NUR, REC, EMB, ACC`).
See `PREFIX_ROLE` in `auth.js` and `ROLE_MODULES` in `hms-access.js`.

---

## 8. Integration tips

1. Add a thin `api.js` layer (fetch wrapper with auth header) and route every mock array
   through it. The render functions already expect plain arrays/objects, so returning the
   same shapes from your API keeps the UI unchanged.
2. Keep `hms-sitemap.js` as the menu source; gate items by the **permissions** returned at
   login instead of (or in addition to) the static role map.
3. The table **Edit/Delete** (in `hms-actions.js`) currently mutate the DOM — wire their
   confirm handlers to `PUT`/`DELETE` calls.
4. CSV **Export** is client-side from the rendered table and will keep working; add
   server-side Excel/PDF later if desired.
5. Replace `localStorage` session with an HTTP-only cookie or token; `auth.js` centralises
   this.

---

## 9. What NOT to change (visual layer)

`styles/app.css` + `styles/hms.css` and the `icon()`/`ui.*` helpers are the design system.
The client has approved the look — keep markup classes intact when wiring data so the styling
holds.
