/* ============================================================
   ANKURAN HMS — Exact sitemap
   Transcribed verbatim from the legacy MasterPage.master nav
   (active, non-commented menu items only).
   module → group (submodule) → menu item (leaf page)
   ============================================================ */
(function () {
  "use strict";

  // legacy file path is kept on each leaf for traceability (src)
  const SITEMAP = [
    { id: "home", label: "Home", icon: "grid", home: true },
    { id: "dashboard", label: "Dashboard", icon: "activity", dash: true },

    {
      id: "opd", label: "OPD Unit", icon: "users",
      groups: [
        { label: "OPD Master", items: [
          { id: "opd-symptom",   label: "Symptom Master",            page: "symptomMaster",      src: "OPD/Complain.aspx" },
          { id: "opd-presgrp",   label: "Prescription Group Master", page: "prescriptionGroup",  src: "OPD/PrescriptionGroup.aspx" },
          { id: "opd-prestpl",   label: "Prescription Template",     page: "prescriptionTemplate", src: "OPD/PrescriptionTemplate.aspx" },
          { id: "opd-prestpl2",  label: "Prescription Template II",  page: "prescriptionTemplate2", src: "OPD/PrescriptionTemplate2.aspx" },
          { id: "opd-docspec",   label: "Doctors Speciality",        page: "doctorSpeciality",   src: "IPD/DoctorSpecialty.aspx" },
          { id: "opd-doctype",   label: "Doctor Type",               page: "doctorType",         src: "IPD/DoctorType.aspx" },
          { id: "opd-docmaster", label: "Doctor Master",             page: "doctorMaster",       src: "IPD/doc_master.aspx" },
        ]},
        { label: "Transaction", items: [
          { id: "opd-reg",       label: "Patient Registration",      page: "patientEnrollment",  src: "OPD/PatientEnrollment.aspx" },
          { id: "opd-list",      label: "Patient List",              page: "patientList",        src: "OPD/PatientList.aspx" },
          { id: "opd-presentry", label: "Patient Prescription Entry", page: "prescriptionEntry", src: "OPD/PatientPrescriptionEntry.aspx" },
          { id: "opd-injentry",  label: "Patient Injections Entry",  page: "injectionEntry",     src: "OPD/PatientInjectionEntry.aspx" },
          { id: "opd-testentry", label: "Patient Test Result Entry", page: "testResultEntry",    src: "OPD/PatientTestResultEntry.aspx" },
        ]},
        { label: "Reports", items: [
          { id: "opd-queue",     label: "Patient Queue List",        page: "patientQueue",       src: "OPD/PatientQueue.aspx" },
          { id: "opd-history",   label: "Patient History",           page: "patientHistory",     src: "OPD/PatientHistoryReport.aspx" },
          { id: "opd-invoice",   label: "Patient Invoice List",      page: "patientInvoiceList", src: "OPD/PatientInvoiceList.aspx" },
          { id: "opd-reglist",   label: "Patient Registration List", page: "patientRegList",     src: "OPD/PatientRegistrationList.aspx" },
        ]},
      ]
    },

    {
      id: "investigation", label: "Investigation", icon: "flask",
      groups: [
        { label: "Master", items: [
          { id: "inv-dept",    label: "Department Master",            page: "departmentMaster",   src: "Pathology/DepartmentMaster.aspx" },
          { id: "inv-manuf",   label: "Manufacturer Master",          page: "manufacturerMaster", src: "Pathology/ManufactureMaster.aspx" },
          { id: "inv-supp",    label: "Supplier Master",              page: "supplierMaster",     src: "Pathology/SuppilierMaster.aspx" },
          { id: "inv-grp",     label: "Investigation Group Master",   page: "testGroupMaster",    src: "Pathology/TestGroupMaster.aspx" },
          { id: "inv-test",    label: "Investigation Master",         page: "testMaster",         src: "Pathology/TestMasterNew.aspx" },
        ]},
        { label: "Transaction", items: [
          { id: "inv-reqlist", label: "Requisition List",             page: "requisitionList",    src: "Pathology/PatientRequisitionList.aspx" },
          { id: "inv-reqbill", label: "Requisition Bill Entry",       page: "requisitionBill",    src: "Pathology/RequisitionBill.aspx" },
          { id: "inv-cancel",  label: "Bill Cancellation Entry",      page: "billCancel",         src: "Pathology/RequisitionBillCancel.aspx" },
          { id: "inv-approve", label: "Bill Cancellation Approval",   page: "billCancelApproval", src: "Pathology/ReqCancelBillApproval.aspx" },
          { id: "inv-update",  label: "Bill Update",                  page: "billUpdate",         src: "Pathology/BillUpdate.aspx" },
        ]},
        { label: "Reports", items: [
          { id: "inv-grpcoll", label: "Investigation Group-wise Collection", page: "invGroupCollection", src: "Pathology/Rep_InvestGrpwisecollection.aspx" },
          { id: "inv-coll",    label: "Investigation-wise Collection",       page: "invCollection",      src: "Pathology/Rep_Investwisecollection.aspx" },
        ]},
      ]
    },

    {
      id: "pharmacy", label: "Pharmacy", icon: "pill",
      groups: [
        { label: "Master", items: [
          { id: "ph-route",   label: "Medicine Route",          page: "medicineRoute",     src: "IPD/MedicineRoute.aspx" },
          { id: "ph-dose",    label: "Dose Master",             page: "doseMaster",        src: "OPD/DoseMaster.aspx" },
          { id: "ph-dur",     label: "Duration Master",         page: "durationMaster",    src: "IPD/DurationMaster.aspx" },
          { id: "ph-supp",    label: "Supplier Master",         page: "supplierMaster",    src: "Pathology/SuppilierMaster.aspx" },
          { id: "ph-manuf",   label: "Manufacturer Master",     page: "manufacturerMaster", src: "Pathology/ManufactureMaster.aspx" },
          { id: "ph-grp",     label: "Medicine Group",          page: "medicineGroup",     src: "Medicine/MedicineGroup.aspx" },
          { id: "ph-subgrp",  label: "Medicine Sub Group",      page: "medicineSubGroup",  src: "Medicine/MedicineSubGroup.aspx" },
          { id: "ph-master",  label: "Medicine / Reagent Master", page: "medicineMaster",  src: "Medicine/MedicineMaster.aspx" },
        ]},
        { label: "Transaction", items: [
          { id: "ph-dash",    label: "Medicine / Reagent Dashboard", page: "medicineDashboard", src: "Medicine/MedicineDashboard.aspx" },
          { id: "ph-purch",   label: "Medicine / Reagent Purchase",  page: "medicinePurchase",  src: "Medicine/PurchaseMedicine.aspx" },
          { id: "ph-return",  label: "Purchase Return",              page: "purchaseReturn",    src: "Medicine/MedicineReturn.aspx" },
          { id: "ph-issue",   label: "Medicine / Reagent Issue",     page: "medicineIssue",     src: "Medicine/MedicineSalesNew.aspx" },
          { id: "ph-opening", label: "Opening Stock Update",         page: "openingStock",      src: "Medicine/ImportFromExcel.aspx" },
        ]},
        { label: "Reports", items: [
          { id: "ph-stock",   label: "Medicine / Reagent Stock Details", page: "stockDetails",   src: "Medicine/MedicineStockDetails.aspx" },
          { id: "ph-movement", label: "Stock Movement Register",        page: "stockMovement",   src: "Medicine/Rep_StockMovement.aspx" },
          { id: "ph-ledger",  label: "Stock Ledger",                    page: "stockLedger",     src: "Medicine/Rep_StockLedger.aspx" },
          { id: "ph-sales",   label: "Sales Register",                  page: "salesRegister",   src: "Medicine/SaleRegister.aspx" },
        ]},
      ]
    },

    {
      id: "accounts", label: "Accounts", icon: "rupee",
      groups: [
        { label: "Master", items: [
          { id: "ac-ledger",  label: "Ledger",                page: "ledgerMaster",     src: "Medicine/MedicineGroup.aspx" },
        ]},
        { label: "Transaction", items: [
          { id: "ac-advance", label: "Advance Payment Entry", page: "advancePayment",   src: "Account/AdvancePayment.aspx" },
        ]},
        { label: "Reports", items: [
          { id: "ac-daily",   label: "Daily Collection",            page: "dailyCollection", src: "Bill/DailyCollection.aspx" },
          { id: "ac-docmis",  label: "Performing Doctor-wise MIS",  page: "docWiseMIS",      src: "OPD/PerformingDocWiseMISReport.aspx" },
          { id: "ac-ivfpay",  label: "IVF Payment Status",          page: "ivfPaymentStatus", src: "Bill/IVFPaymentStatus.aspx" },
        ]},
      ]
    },

    {
      id: "settings", label: "Settings", icon: "settings",
      groups: [
        { label: "", items: [
          { id: "set-desig",  label: "Designation Master", page: "designationMaster", src: "Settings/DesignationMaster.aspx" },
          { id: "set-emp",    label: "Employee Master",    page: "employeeMaster",    src: "Settings/EmployeeMaster.aspx" },
        ]},
      ]
    },

    {
      id: "security", label: "Security", icon: "shield",
      groups: [
        { label: "User Related Info", items: [
          { id: "sec-role",   label: "User Role",       page: "userRole",       src: "Security/UserRole.aspx" },
          { id: "sec-user",   label: "User Creation",   page: "userCreation",   src: "Security/UserCreation.aspx" },
          { id: "sec-pwd",    label: "Change Password", page: "changePassword", src: "Security/ChangePassword.aspx" },
          { id: "sec-action", label: "User Actions",    page: "userActions",    src: "Security/UserAction.aspx" },
        ]},
        { label: "Menu Related Info", items: [
          { id: "sec-module", label: "Module Details",     page: "moduleDetails",    src: "Security/ModuleDetails.aspx" },
          { id: "sec-submod", label: "Sub Module Details", page: "subModuleDetails", src: "Security/SubModuleDetails.aspx" },
          { id: "sec-menu",   label: "Menu Details",       page: "menuDetails",      src: "Security/MenuDetails.aspx" },
          { id: "sec-access", label: "Role-wise Access",    page: "roleWiseAccess",   src: "Security/RoleWiseAccess.aspx" },
        ]},
      ]
    },

    {
      id: "utility", label: "Utility", icon: "server",
      groups: [
        { label: "Data Consistency", items: [
          { id: "util-dc",    label: "Data Consistency", page: "dataConsistency", src: "Utility/DataConsistency.aspx" },
        ]},
        { label: "Tools", items: [
          { id: "util-tools", label: "Diagnostic Tools", page: "diagnosticTools", external: "http://pathways.nice.org.uk/" },
        ]},
        { label: "Reference Books", items: [
          { id: "util-ref1",  label: "Drug Reference Book — 1", page: "refBook", external: "http://www.softpedia.com/get/Others/E-Book/Desktop-Drug-Reference.shtml" },
          { id: "util-ref2",  label: "Drug Reference Book — 2", page: "refBook", external: "https://sites.google.com/site/pharmaceuticalpractice/free-download-ebook-pharmaceutical" },
        ]},
      ]
    },
  ];

  // ---- index helpers -------------------------------------------------
  const ITEM_BY_ID = {};
  const MODULE_BY_ITEM = {};
  SITEMAP.forEach(m => {
    if (!m.groups) return;
    m.groups.forEach(g => g.items.forEach(it => { ITEM_BY_ID[it.id] = it; MODULE_BY_ITEM[it.id] = m.id; }));
  });

  window.SITEMAP = SITEMAP;
  window.HMS_VITEMS = window.HMS_VITEMS || {};
  window.HMS_ITEM = (id) => ITEM_BY_ID[id] || window.HMS_VITEMS[id];
  window.HMS_MODULE_OF = (id) => MODULE_BY_ITEM[id] || (window.HMS_VITEMS[id] && window.HMS_VITEMS[id].module);
})();
