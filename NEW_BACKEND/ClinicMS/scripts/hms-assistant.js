/* ============================================================
   ANKURAN HMS — In-app AI Navigation Assistant ("Anka")
   ------------------------------------------------------------
   • Knows the entire application (every module / menu / sub-option
     and the common tasks for each) built live from the SITEMAP.
   • Local NLP intent engine: understands natural language and
     routes the user to the right screen. Fully offline + crash-proof.
   • LLM seam: a backend "brain" (Llama 3 8B via LangChain) can be
     plugged in through AnkuranAssistant.config.llm — when present it
     is used; otherwise the local engine answers. Either way it never
     breaks the app.
   ============================================================ */
(function () {
  "use strict";
  if (window.__ankaBooted) return;
  window.__ankaBooted = true;

  /* -------------------------------------------------------------
     1) Concept dictionary — maps many words → a canonical concept.
     This is the lightweight "NLP" layer (synonyms + intent words).
     ------------------------------------------------------------- */
  const SYN = {
    // actions
    create: ["new", "create", "add", "make", "register", "open", "generate", "entry", "enter", "fill", "start", "book", "raise"],
    view: ["view", "see", "show", "find", "search", "list", "where", "check", "get", "display", "look", "lookup", "go", "goto", "take", "open", "want", "need", "print"],
    edit: ["edit", "update", "modify", "change", "correct"],
    remove: ["delete", "remove", "cancel"],
    // entities / domains
    patient: ["patient", "patients", "client", "clients", "case", "cases"],
    bill: ["bill", "bills", "billing", "invoice", "invoices", "receipt", "receipts"],
    collection: ["collection", "collections", "collected", "cash", "received", "receipt", "receipts"],
    business: ["business", "performance", "biz"],
    revenue: ["revenue", "revenues", "turnover", "topline", "gross", "income", "billed", "earnings", "earning"],
    profit: ["profit", "profits", "profitability", "net", "bottomline", "surplus", "earn"],
    margin: ["margin", "margins"],
    loss: ["loss", "losses", "deficit"],
    growth: ["growth", "trend", "trends", "analytics", "analysis", "performance", "bi", "yoy", "mom", "compare", "comparison", "insight", "insights", "intelligence", "financial", "financials", "metric", "metrics"],
    employee: ["employee", "employees", "staff", "worker", "workers"],
    doctor: ["doctor", "doctors", "consultant", "consultants", "physician", "dr"],
    user: ["user", "users", "login", "logins", "account", "accounts", "credential", "credentials"],
    password: ["password", "passwords", "pwd", "passcode"],
    role: ["role", "roles", "permission", "permissions", "access", "rights"],
    appointment: ["appointment", "appointments", "booking", "bookings", "schedule", "slot", "slots", "visit"],
    registration: ["registration", "register", "enroll", "enrollment", "signup", "admit", "admission", "enrolment"],
    medicine: ["medicine", "medicines", "drug", "drugs", "medication", "reagent", "reagents", "pharmacy", "pharma", "tablet", "vial"],
    issue: ["issue", "dispense", "sell", "give", "hand"],
    sales: ["sales", "sale", "sold", "selling"],
    transaction: ["transaction", "transactions", "txn", "txns", "deals"],
    total: ["total", "totals", "overall", "aggregate", "count", "number", "sum"],
    purchase: ["purchase", "purchases", "buy", "procure", "procurement", "inward"],
    ret: ["return", "returns", "refund"],
    stock: ["stock", "inventory", "store", "stores", "quantity"],
    supplier: ["supplier", "suppliers", "vendor", "vendors"],
    manufacturer: ["manufacturer", "manufacturers", "maker", "makers", "company"],
    investigation: ["investigation", "investigations", "test", "tests", "lab", "laboratory", "pathology", "sample", "samples", "requisition", "diagnostic", "diagnostics"],
    imaging: ["imaging", "scan", "scans", "usg", "ultrasound", "radiology", "sonography"],
    foetal: ["foetal", "fetal", "foetus", "fetus"],
    prescription: ["prescription", "prescriptions", "prescribe", "rx"],
    injection: ["injection", "injections", "inj"],
    result: ["result", "results"],
    report: ["report", "reports", "mis", "statement", "statements", "register"],
    dashboard: ["dashboard", "kpi", "kpis", "overview", "summary", "charts", "graph", "graphs"],
    ledger: ["ledger", "ledgers"],
    accounts: ["account", "accounts", "accounting", "finance"],
    advance: ["advance", "deposit", "pay", "payment", "payments", "moneyreceipt"],
    symptom: ["symptom", "symptoms", "complaint", "complaints"],
    designation: ["designation", "designations", "post", "title"],
    speciality: ["speciality", "specialty", "specialities", "specialties"],
    group: ["group", "groups", "category", "categories"],
    master: ["master", "masters", "setup", "configure", "configuration", "settings"],
    ivf: ["ivf", "infertility", "iui", "icsi", "procedure", "fertility", "treatment"],
    dose: ["dose", "doses", "dosage"],
    route: ["route", "routes"],
    duration: ["duration", "durations"],
    expiry: ["expiry", "expire", "expiring", "expired"],
    movement: ["movement", "movements"],
    valuation: ["valuation", "value"],
    today: ["today", "todays", "daily", "day"],
    queue: ["queue", "waiting", "line"],
    history: ["history", "past", "previous"],
    discharge: ["discharge", "discharged"],
    department: ["department", "departments", "dept"],
    consistency: ["consistency", "integrity", "check"],
    cancel: ["cancellation", "cancel", "void"],
  };

  // build reverse lookup word -> concept
  const W2C = {};
  Object.keys(SYN).forEach(c => SYN[c].forEach(w => { W2C[w] = c; }));

  const STOP = new Set(["i", "a", "an", "the", "to", "for", "of", "my", "me", "is", "do", "did", "can", "could", "would", "please", "want", "need", "how", "in", "on", "this", "that", "with", "and", "or", "go", "page", "section", "screen", "where", "wanna", "gonna", "let", "lets", "us", "we", "you", "your", "it", "be", "am", "are", "was", "here", "there", "now"]);

  /* -------------------------------------------------------------
     2) Destination index (built live from the real SITEMAP).
     ------------------------------------------------------------- */
  function moduleLabel(id) { const m = (window.SITEMAP || []).find(x => x.id === id); return m ? m.label : ""; }

  function buildIndex() {
    const list = [];
    (window.SITEMAP || []).forEach(m => {
      if (!m.groups) return;
      m.groups.forEach(g => g.items.forEach(it => {
        if (it.external) return;
        list.push({ id: it.id, label: it.label, module: m.label, group: g.label || "" });
      }));
    });
    Object.keys(window.HMS_VITEMS || {}).forEach(k => {
      const it = window.HMS_VITEMS[k];
      list.push({ id: it.id, label: it.label, module: moduleLabel(it.module), group: "" });
    });
    // Special rail destinations (not part of the module sitemap)
    list.push({ id: "dashboard", label: "Dashboard", module: "Dashboard", group: "" });
    list.push({ id: "home", label: "Main Menu", module: "Home", group: "" });
    const byId = {};
    list.forEach(d => { byId[d.id] = d; });

    // Home-tile (clinic-module) aliases — so tile-specific names like
    // "Stock Maintain", "Supplier Entry", "Investigation Bill Entry",
    // "Medicine Sales", "Stock Ledger Details" are matchable & navigable.
    const seen = new Set(list.map(d => d.label.toLowerCase()));
    (window.HMS_HOME || []).forEach(tile => {
      (tile.items || []).forEach(pair => {
        const label = pair[0], id = pair[1];
        if (!byId[id]) return;                 // only alias to known screens
        const key = label.toLowerCase();
        if (seen.has(key)) return;             // canonical label already present
        list.push({ id, label, module: tile.title, group: "", alias: true });
        seen.add(key);
      });
    });
    return { list, byId };
  }

  /* -------------------------------------------------------------
     3) Intent rules — precise routing for common natural phrases.
     Each rule: { to, when(C) } where C is the Set of concepts found.
     First match wins; order = priority.
     ------------------------------------------------------------- */
  const has = (C, ...tags) => tags.every(t => C.has(t));
  const any = (C, ...tags) => tags.some(t => C.has(t));

  const RULES = [
    // ---- financial analytics → Executive Dashboard (profit/margin/loss/growth live ONLY here) ----
    { to: "dashboard", when: C => any(C, "profit", "margin", "growth") },
    { to: "dashboard", when: C => has(C, "loss") && !has(C, "stock") },
    // SALES — “total sales / sales transactions / sales growth” = business metric → Dashboard;
    //         “sales register / report” → Pharmacy report; “medicine sales” → pharmacy issue.
    { to: "dashboard", when: C => has(C, "sales") && any(C, "total", "transaction", "growth", "revenue") && !any(C, "medicine", "pharmacy", "register") },
    { to: "dashboard", when: C => has(C, "transaction") && !any(C, "medicine", "stock", "investigation") },
    // revenue: "daily/today/cash collection" → Accounts; otherwise the dashboard's revenue analytics
    { to: "ac-daily", when: C => has(C, "collection") },
    { to: "ac-daily", when: C => has(C, "revenue") && any(C, "today", "daily") && !any(C, "trend", "growth", "report") },
    { to: "dashboard", when: C => has(C, "revenue") },
    // security
    { to: "sec-pwd", when: C => has(C, "password") },
    { to: "sec-user", when: C => has(C, "user") && (any(C, "create", "edit") || !any(C, "role")) },
    { to: "sec-role", when: C => has(C, "role") },
    // settings
    { to: "set-emp", when: C => has(C, "employee") },
    { to: "set-desig", when: C => has(C, "designation") },
    // registration / appointment
    { to: "v-appt", when: C => has(C, "appointment") },
    { to: "opd-reg", when: C => (has(C, "create") && any(C, "patient", "registration")) || has(C, "registration") },
    // patient billing vs collection
    { to: "opd-invoice", when: C => has(C, "bill") && has(C, "patient") },
    { to: "ac-daily", when: C => has(C, "collection") || (has(C, "today") && has(C, "bill")) },
    { to: "v-discharge", when: C => has(C, "discharge") },
    { to: "ac-advance", when: C => has(C, "advance") },
    { to: "opd-invoice", when: C => has(C, "bill") || has(C, "invoice") },
    // prescriptions / injections / results
    { to: "opd-presentry", when: C => has(C, "prescription") },
    { to: "opd-injentry", when: C => has(C, "injection") },
    { to: "opd-testentry", when: C => has(C, "result") && !has(C, "report") },
    // pharmacy / stores
    { to: "ph-issue", when: C => has(C, "issue") && any(C, "medicine", "stock") },
    { to: "ph-purch", when: C => has(C, "purchase") && any(C, "medicine", "stock") },
    { to: "ph-return", when: C => has(C, "ret") && has(C, "purchase") },
    { to: "v-salesret", when: C => has(C, "ret") && any(C, "issue", "medicine") },
    { to: "v-expiry", when: C => has(C, "expiry") },
    { to: "ph-movement", when: C => has(C, "stock") && has(C, "movement") },
    { to: "ph-ledger", when: C => has(C, "stock") && has(C, "ledger") },
    { to: "v-stockval", when: C => has(C, "stock") && has(C, "valuation") },
    { to: "ph-master", when: C => has(C, "medicine") && (any(C, "create", "master") || has(C, "edit")) },
    { to: "ph-grp", when: C => has(C, "medicine") && has(C, "group") },
    { to: "ph-dose", when: C => has(C, "dose") },
    { to: "ph-route", when: C => has(C, "route") },
    { to: "ph-dur", when: C => has(C, "duration") },
    { to: "ph-stock", when: C => has(C, "stock") || (has(C, "medicine") && has(C, "view")) },
    { to: "v-salesret", when: C => has(C, "ret") && has(C, "sales") },
    { to: "ph-sales", when: C => has(C, "sales") && has(C, "report") },
    { to: "ph-issue", when: C => has(C, "sales") && has(C, "medicine") },
    { to: "ph-sales", when: C => has(C, "sales") },
    // suppliers / manufacturers
    { to: "ph-supp", when: C => has(C, "supplier") },
    { to: "ph-manuf", when: C => has(C, "manufacturer") },
    // investigation
    { to: "inv-cancel", when: C => has(C, "investigation") && has(C, "cancel") },
    { to: "inv-reqbill", when: C => has(C, "investigation") && any(C, "bill", "create") },
    { to: "inv-reqlist", when: C => has(C, "investigation") && any(C, "view", "report") },
    { to: "inv-test", when: C => has(C, "investigation") && has(C, "master") },
    { to: "inv-grp", when: C => has(C, "investigation") && has(C, "group") },
    { to: "inv-dept", when: C => has(C, "department") },
    { to: "v-imaging", when: C => has(C, "imaging") },
    { to: "v-foetal", when: C => has(C, "foetal") },
    { to: "inv-grpcoll", when: C => has(C, "investigation") && has(C, "collection") },
    // doctors / opd masters
    { to: "opd-docspec", when: C => has(C, "doctor") && has(C, "speciality") },
    { to: "opd-docmaster", when: C => has(C, "doctor") },
    { to: "opd-symptom", when: C => has(C, "symptom") },
    // ivf
    { to: "v-infertility", when: C => has(C, "ivf") },
    // patient lists / history
    { to: "opd-history", when: C => has(C, "patient") && has(C, "history") },
    { to: "opd-queue", when: C => has(C, "patient") && has(C, "queue") },
    { to: "opd-list", when: C => has(C, "patient") },
    // accounts
    { to: "ac-ledger", when: C => has(C, "ledger") || has(C, "accounts") },
    { to: "ac-docmis", when: C => has(C, "doctor") && has(C, "report") },
    // dashboard
    { to: "dashboard", when: C => has(C, "dashboard") },
  ];

  /* -------------------------------------------------------------
     4) Engine — parse a query into concepts, run rules, then a
     keyword fallback scorer, and produce a friendly reply.
     ------------------------------------------------------------- */
  function tokenize(s) {
    return String(s || "").toLowerCase().replace(/[^a-z0-9\s]/g, " ").split(/\s+/).filter(Boolean);
  }
  function stem(w) { return w.replace(/(ies)$/, "y").replace(/(es|s)$/, "").replace(/(ing|ed)$/, ""); }

  // Levenshtein edit distance (small, for typo tolerance)
  function lev(a, b) {
    const m = a.length, n = b.length;
    if (Math.abs(m - n) > 2) return 3;
    const dp = Array.from({ length: m + 1 }, (_, i) => i);
    for (let j = 1; j <= n; j++) {
      let prev = dp[0]; dp[0] = j;
      for (let i = 1; i <= m; i++) {
        const tmp = dp[i];
        dp[i] = Math.min(dp[i] + 1, dp[i - 1] + 1, prev + (a[i - 1] === b[j - 1] ? 0 : 1));
        prev = tmp;
      }
    }
    return dp[m];
  }
  const VOCAB = Object.keys(W2C);
  // map a single token → concept, tolerating plurals AND typos ("revenu", "appoinment")
  function tokenConcept(t) {
    if (W2C[t]) return W2C[t];
    const st = stem(t);
    if (W2C[st]) return W2C[st];
    if (t.length < 4) return null;
    let best = null, bd = 99;
    for (const w of VOCAB) {
      if (Math.abs(w.length - t.length) > 2) continue;
      const d = lev(t, w);
      if (d < bd) { bd = d; best = w; if (d === 0) break; }
    }
    const tol = t.length >= 7 ? 2 : 1;
    return (best && bd <= tol) ? W2C[best] : null;
  }

  function concepts(tokens) {
    const C = new Set();
    tokens.forEach(t => { const c = tokenConcept(t); if (c) C.add(c); });
    // multi-word phrase hits ("sign up", "money receipt") via joined pairs
    for (let i = 0; i < tokens.length - 1; i++) {
      const joined = tokens[i] + tokens[i + 1];
      if (W2C[joined]) C.add(W2C[joined]);
    }
    return C;
  }

  function scoreDest(tokens, d) {
    const dt = new Set();
    [d.label, d.module, d.group].forEach(s => tokenize(s).forEach(w => { dt.add(w); dt.add(stem(w)); }));
    let s = 0;
    tokens.forEach(qt => {
      if (STOP.has(qt)) return;
      const q = stem(qt);
      if (dt.has(qt) || dt.has(q)) { s += 3; return; }
      for (const w of dt) { if (w.length > 3 && (w.startsWith(q) || q.startsWith(w))) { s += 1; break; } }
    });
    return s;
  }

  const SMALL_PRE = [
    { test: /\b(hi|hello|hey|hii|namaste|hola)\b/, reply: () => "Hello! 👋 I'm **Alice**, your Ankuran assistant & guide. Ask me to take you somewhere (*“open patient registration”*) or ask what a screen does (*“what is daily collection?”*)." },
    { test: /\b(thanks|thank you|thx|great|nice|cool)\b/, reply: () => "You're welcome! 😊 Anything else you'd like to find or understand?" },
    { test: /\b(bye|goodbye|see you)\b/, reply: () => "Goodbye! I'm always here on the left whenever you need directions or help." },
    { test: /(who are you|what are you|your name)/, reply: () => "I'm **Alice** — your in-app guide for Ankuran. I can navigate you to any screen *and* explain what each screen is for." },
  ];
  const SMALL_POST = [
    { test: /(what can you do|^help$|how.*use you|guide me|i am lost|i'm lost|new here|don't know)/, reply: () => "I do two things:\n• **Navigate** — *“open patient registration”*, *“show last month revenue”*\n• **Explain** — *“what is daily collection?”*, *“how do I issue medicine?”*\nJust ask in plain words." },
    { test: /(list|show|what).*(module|menu|section)/, reply: () => "Ankuran has these modules:\n" + (window.SITEMAP || []).filter(m => m.groups).map(m => "• **" + m.label + "**").join("\n") + "\nAsk what any of them does, or tell me a task and I'll open the right screen." },
  ];

  const FIN_CONCEPTS = ["profit", "margin", "loss", "growth", "revenue"];
  function isFinRole() {
    const r = window.HMS_ROLE ? HMS_ROLE() : "Admin";
    return ["Super Admin", "Admin", "Accounts Team", "Management"].indexOf(r) !== -1;
  }

  /* -------------------------------------------------------------
     INTENT LAYER — resolve {intent, dateRange, targetPage}
     before navigating, ChatGPT-style (meaning, not exact names).
     ------------------------------------------------------------- */
  function detectDate(s) {
    const M = [
      [/\bday before yesterday\b/, "yesterday", "Yesterday"],
      [/\byesterday\b/, "yesterday", "Yesterday"],
      [/\btoday'?s?\b|\bso far today\b/, "today", "Today"],
      [/\blast\s*7\s*days?\b|\bpast\s*7\s*days?\b/, "last7", "Last 7 Days"],
      [/\blast\s*30\s*days?\b|\bpast\s*30\s*days?\b/, "last30", "Last 30 Days"],
      [/\b(last|previous|prev|past)\s+week\b/, "lastWeek", "Last Week"],
      [/\b(this|current)\s+week\b|\bweek to date\b|\bwtd\b/, "thisWeek", "This Week"],
      [/\b(last|previous|prev|past)\s+month\b/, "prevMonth", "Previous Month"],
      [/\b(this|current)\s+month\b|\bmonth to date\b|\bmtd\b/, "thisMonth", "This Month"],
      [/\b(last|previous|prev|past)\s+quarter\b/, "prevQuarter", "Previous Quarter"],
      [/\b(this|current)\s+quarter\b|\bqtd\b/, "thisQuarter", "This Quarter"],
      [/\b(last|previous|prev|past)\s+year\b/, "prevYear", "Previous Year"],
      [/\b(this|current)\s+year\b|\byear to date\b|\bytd\b/, "thisYear", "This Year"],
    ];
    for (const [re, p, l] of M) { if (re.test(s)) return { preset: p, label: l }; }
    return null;
  }

  // the screen currently open in the app (for "what is this page?")
  function currentScreenId() {
    try {
      const A = window.HApp;
      if (!A) return null;
      if (A.currentItem) return A.currentItem;
      if (A.currentModule === "dashboard") return "dashboard";
      return "home";
    } catch (e) { return null; }
  }

  // Resolve a query to a target screen WITHOUT producing navigation text.
  // Returns { id, preset, flavor } or { restricted:true } or null.
  function resolveTarget(raw, lower, tokens, C, date) {
    const { byId } = window.__ankaIndex;
    // (A) explicit Sales Register
    if (/\bsales?\s+(register|registers|entry|entries|transaction|transactions|txn|txns|bill|bills|record|records|log|logs|ledger)\b/.test(lower)
      || /\b(register|list|entries|records|ledger)\s+of\s+sales?\b/.test(lower)) {
      return { id: "ph-sales", flavor: "salesreg" };
    }
    // (B) specific Accounts · Daily Collection
    if (/\b(daily|cash|today'?s|day'?s)\s+collection\b/.test(lower) || /\bcollection\s+(register|report|sheet|receipts?)\b/.test(lower) || /\bmoney\s+receipt\b/.test(lower)) {
      return { id: "ac-daily", preset: (date && date.preset) || null, flavor: "collection" };
    }
    // (C) revenue / sales / business analytics → Revenue Dashboard
    const DASH = ["revenue", "sales", "business", "collection", "profit", "margin", "loss", "growth"];
    if (DASH.some(t => C.has(t))) {
      if (!isFinRole()) return { restricted: true };
      return { id: "dashboard", preset: date ? date.preset : null, flavor: "revenue", C };
    }
    // (D) rule engine, then fuzzy scoring
    let hitId = null;
    for (const r of RULES) { try { if (byId[r.to] && r.when(C)) { hitId = r.to; break; } } catch (e) {} }
    const scored = window.__ankaIndex.list.map(d => ({ d, s: scoreDest(tokens, d) })).sort((a, b) => b.s - a.s);
    if (!hitId && scored[0] && scored[0].s >= 3) hitId = scored[0].d.id;
    if (!hitId) return null;
    const alts = scored.filter(x => x.d.id !== hitId && x.s >= 3).slice(0, 2).map(x => x.d.id);
    return { id: hitId, flavor: "generic", alts, C };
  }

  function restrictedReply() {
    return {
      kind: "unsure",
      text: "Business & financial analytics — revenue, sales, profit, growth — are available to **Administrator, Accounts and Management** roles only. I can still take you to clinical and operational screens.",
      chips: ["Patient queue", "Today's appointments", "Issue medicine", "Investigations"],
    };
  }

  // Build the NAVIGATION reply text for a resolved target.
  function navResult(target) {
    const { byId } = window.__ankaIndex;
    const dest = byId[target.id];
    const C = target.C || new Set();
    if (target.flavor === "salesreg") {
      return { kind: "route", id: target.id, dest, text: "Opening the **Sales Register** — every pharmacy sales entry / bill.\n_Pharmacy › Reports_" };
    }
    if (target.flavor === "collection") {
      return { kind: "route", id: target.id, dest, preset: target.preset || null, alts: byId["dashboard"] ? [byId["dashboard"]] : [],
        text: "Opening **Daily Collection** (cash · card/UPI · bank)" + (target.preset ? " for that period" : "") + ".\n_Accounts › Reports_" };
    }
    if (target.flavor === "revenue") {
      const what = C.has("profit") ? "profit, margin & growth" : C.has("margin") ? "profit margin" : C.has("loss") ? "loss & loss %" : C.has("growth") ? "revenue & profit growth" : (C.has("sales") || C.has("business")) ? "sales & business performance" : "total revenue & trends";
      const dtxt = target.preset && window.HMS_DASH_LABEL ? " for **" + HMS_DASH_LABEL(target.preset) + "** — date filter applied" : " — use the date slicer to pick any period";
      return { kind: "route", id: target.id, dest, preset: target.preset || null, alts: byId["ac-daily"] ? [byId["ac-daily"]] : [],
        text: "Here's **" + what + "** on the **Revenue Dashboard**" + dtxt + ".\n_Dashboard_" };
    }
    const isCreate = C.has && C.has("create");
    const verb = isCreate ? "create a new record in" : "open";
    const path = dest.module + (dest.group ? " › " + dest.group : "");
    const alts = (target.alts || []).map(id => byId[id]).filter(Boolean);
    return { kind: "route", id: target.id, dest, alts, text: "Sure — to do that, " + verb + " **" + dest.label + "**.\n_" + path + "_" };
  }

  let lastInfoDest = null, lastInfoPreset = null;

  /* -------------------------------------------------------------
     Lenient name matcher for INFO questions — lets Alice explain
     ANY option / sub-option / module the user names, even when the
     navigation scorer wouldn't be confident enough to jump there.
     ------------------------------------------------------------- */
  const INFO_DROP = new Set([
    "what", "whats", "what's", "whatis", "is", "the", "a", "an", "explain", "explaination", "explanation",
    "describe", "description", "tell", "about", "me", "of", "definition", "define", "meaning", "mean",
    "purpose", "does", "do", "how", "i", "we", "you", "this", "that", "these", "those", "page", "pages",
    "screen", "screens", "menu", "menus", "option", "options", "sub", "suboption", "suboptions", "submenu",
    "for", "used", "use", "uses", "information", "info", "module", "modules", "form", "forms", "section",
    "sections", "tab", "tabs", "know", "want", "need", "and", "or", "to", "in", "on", "work", "working",
    "button", "buttons", "feature", "features", "regarding", "everything", "each", "every", "all", "give",
    // navigation verbs — so "open Stores" / "go to A/c Reports" still match the tile/screen name
    "open", "go", "goto", "show", "navigate", "launch", "take", "bring", "there", "please",
    "see", "find", "get", "view", "visit", "jump", "into", "let", "lets", "wanna",
  ]);
  function infoTokens(tokens) {
    return tokens.filter(t => !INFO_DROP.has(t) && t.length > 1).map(stem);
  }
  function bestScreenMatch(tokens) {
    const words = infoTokens(tokens);
    if (!words.length) return null;
    let best = null, bestScore = 0;
    window.__ankaIndex.list.forEach(d => {
      const labelSet = new Set(tokenize(d.label).map(stem));
      const groupSet = new Set(tokenize(d.group || "").map(stem));
      const modSet = new Set(tokenize(d.module || "").map(stem));
      let labelHits = 0, otherHits = 0;
      words.forEach(w => {
        if (labelSet.has(w)) { labelHits += 1; return; }
        let pref = false;
        for (const lw of labelSet) { if (lw.length > 3 && (lw.startsWith(w) || w.startsWith(lw))) { pref = true; break; } }
        if (pref) { labelHits += 0.7; return; }
        if (groupSet.has(w) || modSet.has(w)) otherHits += 0.5;
      });
      // coverage bonus: reward matching a large share of the screen's own name
      const coverage = labelHits / Math.max(1, tokenize(d.label).length);
      const score = labelHits * 2 + otherHits + coverage;
      if (labelHits >= 1 && score > bestScore) { bestScore = score; best = d; }
    });
    return best ? { id: best.id, dest: best, score: bestScore, words } : null;
  }
  function bestModuleMatch(tokens) {
    const words = infoTokens(tokens);
    if (!words.length) return null;
    let best = null, bs = 0, bestCovered = false;
    (window.SITEMAP || []).filter(m => m.groups).forEach(m => {
      const lw = new Set(tokenize(m.label).map(stem));
      let hits = 0;
      words.forEach(w => {
        if (lw.has(w)) { hits += 1; return; }
        for (const x of lw) { if (x.length > 3 && (x.startsWith(w) || w.startsWith(x))) { hits += 0.7; break; } }
      });
      const covered = words.every(w => lw.has(w) || [...lw].some(x => x.length > 3 && (x.startsWith(w) || w.startsWith(x))));
      if (hits >= 1 && hits > bs) { bs = hits; best = m; bestCovered = covered; }
    });
    return best ? { module: best, score: bs, covered: bestCovered } : null;
  }
  function describeModule(m) {
    const groups = m.groups.map(g => {
      const items = g.items.filter(it => !it.external).map(it => it.label);
      return "**" + g.label + "** — " + items.slice(0, 7).join(", ") + (items.length > 7 ? "…" : "");
    });
    return "**" + m.label + "** is a module in Ankuran, organised into:\n• " + groups.join("\n• ") +
      "\n\nAsk me *“what is <name>?”* about any of these, or say *“open <name>”* to go there.";
  }

  // clinic-module (home-page tile) matcher + describer
  function bestHomeMatch(tokens) {
    const words = infoTokens(tokens);
    if (!words.length) return null;
    let best = null, bs = 0, covered = false;
    (window.HMS_HOME || []).forEach(tile => {
      const lw = new Set(tokenize(tile.title).map(stem));
      let hits = 0;
      words.forEach(w => {
        if (lw.has(w)) { hits += 1; return; }
        for (const x of lw) { if (x.length > 3 && (x.startsWith(w) || w.startsWith(x))) { hits += 0.7; break; } }
      });
      const cov = words.every(w => lw.has(w) || [...lw].some(x => x.length > 3 && (x.startsWith(w) || w.startsWith(x))));
      if (hits >= 1 && hits > bs) { bs = hits; best = tile; covered = cov; }
    });
    return best ? { tile: best, score: bs, covered } : null;
  }
  function describeHome(tile) {
    const uniq = [];
    (tile.items || []).forEach(p => { if (uniq.indexOf(p[0]) === -1) uniq.push(p[0]); });
    return {
      text: "**" + tile.title + "** is a clinic module on the Main Menu. It groups these screens:\n• " + uniq.join("\n• ") +
        "\n\nTap one below to open it, or ask *“what is <name>?”*.",
      chips: uniq.slice(0, 6),
    };
  }

  function understand(query) {
    const raw = String(query || "").trim();
    if (!raw) return { kind: "smalltalk", text: "Tell me what you'd like to do, or ask what a screen is for." };
    const lower = raw.toLowerCase();
    const { byId } = window.__ankaIndex;

    // greetings / identity first
    for (const s of SMALL_PRE) { if (s.test.test(lower)) return { kind: "smalltalk", text: s.reply() }; }

    // pure follow-up after an explanation: "take me there", "open it", "yes"
    if (/^(take me there|open it|open this|open that|open|show me|show it|show this|go there|go|navigate|launch( it)?|yes( please)?|sure|ok(ay)?|do it)[.!]?$/i.test(raw.trim()) && lastInfoDest && byId[lastInfoDest]) {
      return { kind: "route", id: lastInfoDest, dest: byId[lastInfoDest], preset: lastInfoPreset, autoGo: true, text: "Sure — opening **" + byId[lastInfoDest].label + "** now." };
    }

    // exact screen-name match (e.g. a tapped sub-option chip) → navigate directly
    const exact = (window.__ankaIndex.list || []).find(d => d.label.toLowerCase() === raw.trim().toLowerCase());
    if (exact && byId[exact.id]) {
      const allowed = (!window.HMS_ITEM_ALLOWED) || HMS_ITEM_ALLOWED(exact.id) || exact.id === "dashboard" || exact.id === "home";
      if (allowed) { lastInfoDest = exact.id; lastInfoPreset = null; return { kind: "route", id: exact.id, dest: byId[exact.id], autoGo: true, text: "Opening **" + byId[exact.id].label + "**…" }; }
    }

    const tokens = tokenize(raw);
    const C = concepts(tokens);
    const date = detectDate(lower);

    const infoCue = /\b(what|what's|whats|explain|describe|definition|meaning|purpose|tell me about|info|information|about)\b/.test(lower)
      || /\bhow (do|to|can) (i|we|you)\b/.test(lower) || /\bwhere (do|can) (i|we)\b/.test(lower) || /\bwhat.*\b(used for|for|does)\b/.test(lower);
    const thisScreen = /\bthis (page|menu|option|screen|module|form|section|report|tab)\b/.test(lower) || /\b(current)\s+(page|screen|menu)\b/.test(lower);
    const openAlso = /\b(open|take me|show me|navigate|launch|go to|go there|bring me)\b/.test(lower);

    // resolve a target screen (navigation rules)
    let target = null;
    if (thisScreen) { const cur = currentScreenId(); if (cur && byId[cur]) target = { id: cur, flavor: "generic", C }; }
    if (!target) target = resolveTarget(raw, lower, tokens, C, date);

    // synthesize a dest for home/dashboard which aren't in the leaf index
    function destFor(id) {
      if (byId[id]) return byId[id];
      if (id === "dashboard") return { id: "dashboard", label: "Dashboard", module: "Dashboard", group: "" };
      if (id === "home") return { id: "home", label: "Main Menu", module: "Home", group: "" };
      return null;
    }

    // ---------------- INFORMATION REQUEST (help guide) ----------------
    if (infoCue || thisScreen) {
      // "this page" → explain whatever is open
      if (thisScreen) {
        const cur = currentScreenId(); const cd = cur && destFor(cur);
        if (cd) {
          lastInfoDest = cur; lastInfoPreset = null;
          const e = (window.HMS_DESCRIBE ? HMS_DESCRIBE(cd) : ("**" + cd.label + "**"));
          return { kind: "route", id: cur, dest: cd, text: e + "\n\n_Already here. Ask me about another screen any time._" };
        }
      }

      // For INFO questions, trust the literal NAME match first (so
      // "Role-wise Access" isn't hijacked by the word "role", etc.)
      const nameHit = bestScreenMatch(tokens);
      const modHit = bestModuleMatch(tokens);
      const homeHit = bestHomeMatch(tokens);

      // a whole clinic module (home tile) named → describe + list its screens
      if (homeHit && homeHit.covered && (!nameHit || nameHit.score < 3)) {
        lastInfoDest = null;
        const d = describeHome(homeHit.tile);
        // "open Stores" caught as info → still open the tile; "what is Stores" → just describe
        if (openAlso) return { kind: "unsure", text: "Opening **" + homeHit.tile.title + "** on the Main Menu — pick a screen:", chips: d.chips, openTile: homeHit.tile.title };
        return { kind: "unsure", text: d.text, chips: d.chips, openTile: null };
      }

      // a whole sitemap module named (and fully covered) → describe the module
      if (modHit && modHit.covered && (!nameHit || nameHit.score < 3)) {
        lastInfoDest = null;
        return { kind: "smalltalk", text: describeModule(modHit.module) };
      }

      // restricted financial analytics → still explain, note the limit
      if (target && target.restricted && (!nameHit || nameHit.score < 3)) {
        const dd = destFor("dashboard");
        lastInfoDest = null;
        return { kind: "smalltalk", text: (window.HMS_DESCRIBE ? HMS_DESCRIBE(dd) : "The Dashboard shows business analytics.") + "\n\n_Note: revenue & financial analytics are limited to Administrator / Accounts / Management roles._" };
      }

      // pick the screen to explain: strong name match → rule target → weak name → module
      let explainDest = null, explainPreset = null;
      if (nameHit && nameHit.score >= 2) explainDest = nameHit.dest;
      else if (target && destFor(target.id)) { explainDest = destFor(target.id); explainPreset = target.preset || null; }
      else if (nameHit) explainDest = nameHit.dest;
      else if (modHit) { lastInfoDest = null; return { kind: "smalltalk", text: describeModule(modHit.module) }; }

      if (explainDest) {
        lastInfoDest = explainDest.id; lastInfoPreset = explainPreset;
        const expl = (window.HMS_DESCRIBE ? HMS_DESCRIBE(explainDest) : ("**" + explainDest.label + "**"));
        const canOpen = (!window.HMS_ITEM_ALLOWED) || HMS_ITEM_ALLOWED(explainDest.id) || explainDest.id === "dashboard" || explainDest.id === "home";
        if (openAlso && canOpen) {
          return { kind: "route", id: explainDest.id, dest: explainDest, preset: explainPreset, autoGo: true, text: expl + "\n\nOpening it now…" };
        }
        if (canOpen) return { kind: "route", id: explainDest.id, dest: explainDest, preset: explainPreset, text: expl + "\n\n_Want to go there? Tap **Take me there** below._" };
        return { kind: "smalltalk", text: expl + "\n\n_This screen isn't available for your role, so I can't open it for you._" };
      }

      // nothing matched → general help
      for (const s of SMALL_POST) { if (s.test.test(lower)) return { kind: "smalltalk", text: s.reply() }; }
      return {
        kind: "unsure",
        text: "I can explain any screen — tell me its name. For example: *“what is Symptom Master?”*, *“what is Requisition Bill Entry?”*, or *“explain the Pharmacy module”*.",
        chips: ["Explain OPD Unit", "What is Medicine Issue?", "What is Daily Collection?", "List modules"],
      };
    }

    // general help / module list
    for (const s of SMALL_POST) { if (s.test.test(lower)) return { kind: "smalltalk", text: s.reply() }; }

    // ---------------- NAVIGATION REQUEST ----------------
    const navName = bestScreenMatch(tokens);

    // a confident, specific screen-name match wins over loose concept rules
    // (so "Investigation Bill Entry" → Requisition Bill, not Patient Invoice)
    if (navName && navName.score >= 3 && byId[navName.id]) {
      lastInfoDest = navName.id; lastInfoPreset = null;
      const r = navResult({ id: navName.id, flavor: "generic", C });
      if (openAlso && r.kind === "route" && r.dest) r.autoGo = true;
      return r;
    }

    // an explicitly-named clinic module (home tile) → open it on the Main Menu
    const navHome = bestHomeMatch(tokens);
    if (navHome && navHome.covered && (!navName || navName.score < 3)) {
      const d = describeHome(navHome.tile);
      return { kind: "unsure", text: "Opening **" + navHome.tile.title + "** on the Main Menu — pick a screen:", chips: d.chips, openTile: navHome.tile.title };
    }
    // an explicitly-named sitemap module → describe it
    const navMod = bestModuleMatch(tokens);
    if (navMod && navMod.covered && (!navName || navName.score < 3)) {
      return { kind: "smalltalk", text: describeModule(navMod.module) };
    }

    if (target && target.restricted) return restrictedReply();
    if (target && byId[target.id]) {
      lastInfoDest = target.id; lastInfoPreset = target.preset || null;
      const r = navResult(target);
      if (openAlso && r.kind === "route" && r.dest) r.autoGo = true; // explicit "open/go to" → jump now
      return r;
    }

    // lenient name match so a bare screen name still navigates
    if (navName && navName.score >= 2 && byId[navName.id]) {
      lastInfoDest = navName.id; lastInfoPreset = null;
      const r = navResult({ id: navName.id, flavor: "generic", C });
      if (openAlso && r.kind === "route" && r.dest) r.autoGo = true;
      return r;
    }

    return {
      kind: "unsure",
      text: "I'm not sure which screen you mean. You can ask me to **open** something (*“open patient registration”*) or ask **what** a screen does (*“what is daily collection?”*).",
      chips: ["What is the Dashboard?", "Open patient registration", "Today's collection", "How do I issue medicine?"],
    };
  }

  /* -------------------------------------------------------------
     5) LLM seam (optional backend brain: Llama 3 8B via LangChain).
        Configure: AnkuranAssistant.config.llm = async (query, ctx) =>
          ({ reply: "…", route: "<itemId|null>" })   // or null to defer
        Anything thrown here is swallowed — the local engine answers.
     ------------------------------------------------------------- */
  const config = { llm: null, name: "Alice" };

  async function brain(query) {
    // try external LLM brain first, but never let it break the bot
    try {
      if (typeof config.llm === "function") {
        const out = await Promise.race([
          config.llm(query, { sitemap: window.__ankaIndex.list }),
          new Promise(res => setTimeout(() => res(null), 12000)),
        ]);
        if (out && (out.reply || out.route)) {
          const dest = out.route && window.__ankaIndex.byId[out.route];
          return { kind: out.route ? "route" : "smalltalk", id: out.route || null, dest: dest || null, alts: [], text: out.reply || ("Opening **" + (dest ? dest.label : "") + "**.") };
        }
      }
    } catch (e) { /* fall through to local engine */ }
    return understand(query);
  }

  /* -------------------------------------------------------------
     6) Chat UI
     ------------------------------------------------------------- */
  function el(html) { const t = document.createElement("template"); t.innerHTML = html.trim(); return t.content.firstChild; }
  function fmt(s) {
    return ui.esc(s)
      .replace(/\*\*(.+?)\*\*/g, "<b>$1</b>")
      .replace(/_(.+?)_/g, "<i>$1</i>")
      .replace(/\*(.+?)\*/g, "<i>$1</i>")
      .replace(/\n/g, "<br>");
  }

  let panel, msgs, input, openState = false;

  function mount() {
    if (document.getElementById("ankaFab")) return;
    window.__ankaIndex = buildIndex();

    const fab = el(`<button id="ankaFab" class="anka-fab" aria-label="Open assistant">
      ${icon("message", 22)}<span class="anka-fab__dot"></span></button>`);
    document.body.appendChild(fab);

    panel = el(`<div id="ankaPanel" class="anka-panel" hidden>
      <div class="anka-head">
        <div class="anka-head__id"><span class="anka-ava">${icon("sparkles", 16)}</span>
          <div><b>Alice</b><span>Ankuran assistant · online</span></div></div>
        <button class="anka-x" aria-label="Close">${icon("x", 18)}</button>
      </div>
      <div class="anka-msgs" id="ankaMsgs"></div>
      <div class="anka-chips" id="ankaChips"></div>
      <form class="anka-input" id="ankaForm">
        <input id="ankaText" autocomplete="off" placeholder="Ask me where to go…">
        <button type="submit" class="anka-send" aria-label="Send">${icon("arrowRight", 18)}</button>
      </form>
    </div>`);
    document.body.appendChild(panel);

    msgs = panel.querySelector("#ankaMsgs");
    input = panel.querySelector("#ankaText");

    fab.addEventListener("click", toggle);
    panel.querySelector(".anka-x").addEventListener("click", () => toggle(false));
    panel.querySelector("#ankaForm").addEventListener("submit", (e) => { e.preventDefault(); send(input.value); });

    // greeting
    botSay("Hi! I'm **Alice** 👋 your Ankuran guide. I can do two things:\n• **Navigate** — say *“open patient registration”* or *“show last month revenue”*\n• **Explain** — ask *“what is <name>?”* about any module, menu or report\n\n_Try the suggestions below, or just type what you need._");
    setChips(["Explain OPD Unit", "What is Daily Collection?", "Open patient registration", "List modules"]);
  }

  function toggle(force) {
    openState = (typeof force === "boolean") ? force : !openState;
    panel.hidden = !openState;
    document.getElementById("ankaFab").classList.toggle("active", openState);
    if (openState) { setTimeout(() => { try { input.focus(); } catch (e) {} scrollDown(); }, 60); }
  }

  function scrollDown() { try { msgs.scrollTop = msgs.scrollHeight; } catch (e) {} }

  function userSay(text) {
    msgs.appendChild(el(`<div class="anka-msg user"><div class="anka-bub">${fmt(text)}</div></div>`));
    scrollDown();
  }
  function botSay(text, node) {
    const m = el(`<div class="anka-msg bot"><span class="anka-mava">${icon("sparkles", 13)}</span><div class="anka-bub">${fmt(text)}</div></div>`);
    if (node) m.querySelector(".anka-bub").appendChild(node);
    msgs.appendChild(m); scrollDown();
    return m;
  }
  function typing() {
    const t = el(`<div class="anka-msg bot" id="ankaTyping"><span class="anka-mava">${icon("sparkles", 13)}</span><div class="anka-bub anka-typing"><span></span><span></span><span></span></div></div>`);
    msgs.appendChild(t); scrollDown(); return t;
  }

  function setChips(arr) {
    const box = panel.querySelector("#ankaChips");
    box.innerHTML = "";
    (arr || []).forEach(c => {
      const label = (typeof c === "string") ? c : c.label;
      const toSend = (typeof c === "string") ? c : (c.send || c.label);
      const b = el(`<button class="anka-chip">${ui.esc(label)}</button>`);
      b.addEventListener("click", () => send(toSend));
      box.appendChild(b);
    });
  }

  function goButton(dest, preset) {
    const wrap = el(`<div class="anka-actions"></div>`);
    const go = el(`<button class="anka-go">${icon("arrowRight", 15)} Take me there</button>`);
    go.addEventListener("click", () => {
      try { if (preset && window.HMS_DASH_SET) window.HMS_DASH_SET(preset); } catch (e) {}
      try { if (window.HMS_GO) window.HMS_GO(dest.id); } catch (e) {}
      botSay("Opened **" + dest.label + "**" + (preset && window.HMS_DASH_LABEL ? " · " + window.HMS_DASH_LABEL(preset) : "") + " for you. Need anything else?");
    });
    wrap.appendChild(go);
    return wrap;
  }

  async function send(text) {
    text = String(text || "").trim();
    if (!text) return;
    try {
      input.value = "";
      userSay(text);
      setChips([]);
      const t = typing();
      let res;
      try { res = await brain(text); }
      catch (e) { res = { kind: "smalltalk", text: "Sorry, I tripped up for a second. Could you rephrase that?" }; }
      // small human-like delay
      await new Promise(r => setTimeout(r, 380));
      const tn = document.getElementById("ankaTyping"); if (tn) tn.remove();

      if (res.kind === "route" && res.dest) {
        if (res.autoGo) {
          botSay(res.text);
          try { if (res.preset && window.HMS_DASH_SET) window.HMS_DASH_SET(res.preset); } catch (e) {}
          try { if (window.HMS_GO) window.HMS_GO(res.dest.id); } catch (e) {}
          setChips(["Main menu", "Dashboard", "Help"]);
        } else {
          botSay(res.text, goButton(res.dest, res.preset));
          if (res.alts && res.alts.length) setChips(res.alts.map(a => a.label));
          else setChips(["Main menu", "Dashboard", "Help"]);
        }
      } else if (res.kind === "unsure") {
        botSay(res.text);
        if (res.openTile) { try { if (window.HMS_OPEN_TILE) window.HMS_OPEN_TILE(res.openTile); } catch (e) {} }
        setChips(res.chips || ["Help"]);
      } else {
        botSay(res.text);
        setChips(["Register a patient", "Today's collection", "Issue medicine", "Help"]);
      }
    } catch (err) {
      try { const tn = document.getElementById("ankaTyping"); if (tn) tn.remove(); botSay("Hmm, something went wrong on my side — but I'm still here. Try asking again."); } catch (e) {}
    }
  }

  // map a couple of convenience chip phrases
  const ALIAS = { "main menu": "home", "dashboard": "dashboard", "help": "what can you do" };
  const _send = send;
  send = function (text) {
    const k = String(text || "").trim().toLowerCase();
    if (k === "main menu") { try { window.HMS_GO("home"); } catch (e) {} userSay("Main menu"); botSay("Here's the **Main Menu**."); return; }
    if (k === "dashboard") { try { window.HMS_GO("dashboard"); } catch (e) {} userSay("Dashboard"); botSay("Opened the **Dashboard**."); return; }
    return _send(text);
  };

  /* -------------------------------------------------------------
     public API
     ------------------------------------------------------------- */
  window.AnkuranAssistant = {
    config,
    open: () => toggle(true),
    close: () => toggle(false),
    ask: (q) => { toggle(true); send(q); },
    rebuildIndex: () => { window.__ankaIndex = buildIndex(); },
  };

  if (document.readyState === "loading") document.addEventListener("DOMContentLoaded", mount);
  else setTimeout(mount, 0);
})();
