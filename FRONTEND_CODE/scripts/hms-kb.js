/* ============================================================
   ANKURAN HMS — Alice knowledge base (plain-language guide)
   ------------------------------------------------------------
   describe(dest) → a short, jargon-free explanation of any
   screen: what it's for, when to use it, what you can do there.
   Curated text for key screens; a sensible generator covers
   every other module / menu / report / master automatically.
   ============================================================ */
(function () {
  "use strict";

  // curated explanations, keyed by screen id
  const KB = {
    home: "The **Main Menu** is your starting point. From here you can jump into any module — Registration, Investigation, Pharmacy, Accounts and more — using the cards in the centre or the dark menu on the left.",
    dashboard: "The **Dashboard** gives owners and managers a quick health-check of the clinic. It shows headline numbers (revenue, profit, patients, appointments) and trend charts. Use the date selector at the top to view any period — today, this month, last quarter, and so on.",

    // OPD
    "opd-reg": "**Patient Registration** is used to create a new patient record in the system. Here you enter the patient's name, contact, address and other basic details before any treatment or consultation begins. Use it the first time a patient visits.",
    "opd-list": "**Patient List** shows every registered patient. Use it to search for someone, open their record and review their details.",
    "v-appt": "**Patient Appointment** is where you book and manage consultation appointments — choose the patient, the doctor, and a date and time slot.",
    "opd-presentry": "**Prescription Entry** is where a doctor records the medicines and advice for a patient's visit, so it can be printed or dispensed.",
    "opd-injentry": "**Injection Entry** records injections given to a patient — what was given, the dose, and who administered it.",
    "opd-testentry": "**Test Result Entry** is where lab values for a patient are entered against the tests that were ordered.",
    "opd-queue": "**Patient Queue** shows who is waiting today and where they are in the line, so the front desk and doctors can manage the flow.",
    "opd-history": "**Patient History** lets you look back at a patient's past visits, complaints and prescriptions.",
    "opd-invoice": "**Patient Invoice List** shows patient bills — what was charged, what's paid and what's still due. Use it to review or follow up on billing.",
    "opd-reglist": "**Registration List** is a report of patients registered over a chosen period.",
    "opd-symptom": "**Symptom Master** is a setup screen where you maintain the list of common complaints/symptoms, so they can be picked quickly during consultations.",
    "opd-docmaster": "**Doctor Master** is where you maintain the clinic's doctors — names, specialities, qualifications and contact details — so they appear in appointments and reports.",

    // Investigation
    "inv-reqbill": "**Requisition Bill Entry** is used to bill the investigations/tests ordered for a patient. Pick the tests and it totals the charges.",
    "inv-reqlist": "**Requisition List** shows all investigation requests — their status and amounts — so you can track samples and reports.",
    "inv-test": "**Investigation Master** is the catalogue of all tests the lab offers, with their sample type, normal range and rate. Set them up here so they're available when billing.",
    "inv-grpcoll": "This report shows how much was collected, grouped by investigation category — useful for seeing which test groups bring in the most.",

    // Pharmacy
    "ph-issue": "**Medicine Issue** is where you dispense (sell) medicines to a patient — choose the items and quantities, and it records the sale and reduces stock.",
    "ph-purch": "**Medicine Purchase** records stock bought from a supplier — items, batches, expiry and rates — and adds them to your inventory.",
    "ph-master": "**Medicine / Reagent Master** is the catalogue of all medicines and reagents, with their group, manufacturer, unit and price.",
    "ph-stock": "**Stock Details** shows current stock on hand for each item, with batches and expiry — handy for checking availability.",
    "ph-sales": "**Sales Register** lists every pharmacy sale (each bill/transaction) over a period — use it to review or reconcile sales entries.",
    "v-expiry": "**Expiry Alert** flags medicines that are expired or expiring soon, so you can act before stock is wasted.",

    // Accounts
    "ac-daily": "**Daily Collection** shows the money received today, split by cash, card/UPI and bank, with the list of receipts. Use it to reconcile the day's takings.",
    "ac-advance": "**Advance Payment** records an advance amount a patient pays upfront (for example before an IVF cycle) and issues a receipt.",
    "ac-docmis": "This report shows revenue and patient counts for each performing doctor — useful for management review.",
    "v-discharge": "**Discharge Bill Details** summarises the final bills for patients being discharged — totals, paid and due.",

    // Security & Settings
    "sec-user": "**User Creation** is where an administrator creates login accounts for staff and assigns each one a role.",
    "sec-role": "**User Role** defines the roles (e.g. Doctor, Receptionist) that decide what each user can see and do.",
    "sec-pwd": "**Change Password** lets the signed-in user update their own password.",
    "sec-access": "**Role-wise Access** is where an administrator chooses which screens each role can open.",
    "set-emp": "**Employee Master** is where you maintain staff records — names, designations and contact details.",
    "set-desig": "**Designation Master** maintains the list of job titles/designations used for employees.",
  };

  function nounOf(label) {
    let s = String(label || "").replace(/\b(master|entry|list|report|details|register|new|reagent)\b/ig, "").replace(/[/]/g, " ").replace(/\s+/g, " ").trim();
    return (s || label || "records").toLowerCase();
  }

  function generate(d) {
    const L = d.label, M = d.module || "the system", g = (d.group || "");
    const n = nounOf(L);
    // The sitemap group (Master / Transaction / Reports) is the most reliable signal.
    let kind = "";
    if (/report/i.test(g)) kind = "report";
    else if (/master/i.test(g)) kind = "master";
    else if (/transaction/i.test(g)) kind = "transaction";
    else {
      // vitems / ungrouped → fall back to word heuristics
      if (/\b(report|list|collection|register|ledger|movement|valuation|mis|statement|history|queue|status|alert)\b/i.test(L)) kind = "report";
      else if (/\b(master|group|type|speciality|specialty|designation|route|dose|duration|department|manufacturer|supplier|symptom)\b/i.test(L)) kind = "master";
      else kind = "transaction";
    }
    if (kind === "master") return "**" + L + "** is a setup screen in " + M + ". Here you maintain the standard list of " + n + " the clinic uses, so they're ready to choose when filling other forms. You can add, edit, search and remove entries.";
    if (kind === "report") return "**" + L + "** is a report in " + M + ". It lets you review " + n + " — you can filter by date, search and export the figures. It's for viewing information, not for data entry.";
    return "**" + L + "** is a screen in " + M + " where you record and manage " + n + ". Open it to create a new entry, fill in the details and save.";
  }

  window.HMS_DESCRIBE = function (dest) {
    if (!dest) return "I can explain any screen — just name it.";
    return KB[dest.id] || generate(dest);
  };
})();
