/* ============================================================
   ANKURAN HMS — Canonical facts (single source of truth)
   ------------------------------------------------------------
   Both the Executive Dashboard and the Accounts · Daily
   Collection report read TODAY's figures from here, so the
   headline numbers can never drift apart.
   ============================================================ */
(function () {
  "use strict";
  const cash = 18400, cardUpi = 26150, bank = 12000;
  const total = cash + cardUpi + bank; // 56,550

  window.HMS_FACTS = {
    today: {
      date: "2026-06-06",
      collection: { cash, cardUpi, bank, total },
      revenue: total,        // headline "Today" revenue == collection (kept identical for consistency)
      cost: Math.round(total * 0.62),
      patients: 23,
      appointments: 18,
      newRegs: 9,
      checkIns: 23,
      salesTxns: 37,
    },
  };
})();
