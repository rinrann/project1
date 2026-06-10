/* ============================================================
   ANKURAN HMS — Master pages (generic engine specs)
   Every "Master" leaf across all modules. Faithful form + grid.
   ============================================================ */
(function () {
  "use strict";
  const P = window.HMS_PAGES;
  const M = window.HMS_master;

  const col = (key, label, o) => Object.assign({ key, label }, o || {});
  const C_CODE = (p) => col("code", "Code", { id: true });
  const ACTIVE = ["Active", "Inactive"];

  // helper to register a master page
  function reg(page, specFn) { P[page] = (host, item) => M(host, item, specFn()); }

  /* ---------- OPD masters ---------- */
  reg("symptomMaster", () => ({
    subtitle: "OPD Master · symptoms / complaints", codePrefix: "SYM", codeLabel: "Symptom Code",
    fields: [
      { label: "Symptom Name", name: "name", required: true, placeholder: "e.g. Fever" },
      { label: "Description", name: "desc", type: "textarea", rows: 2, placeholder: "Optional" },
      { label: "Status", name: "status", type: "select", options: ACTIVE },
    ],
    columns: [C_CODE(), col("name", "Symptom Name", { main: true }), col("desc", "Description"), col("status", "Status")],
    rows: [
      { code: "SYM001", name: "Fever", desc: "Raised body temperature", status: "Active" },
      { code: "SYM002", name: "Abdominal Pain", desc: "Lower abdomen", status: "Active" },
      { code: "SYM003", name: "Irregular Cycle", desc: "Menstrual irregularity", status: "Active" },
      { code: "SYM004", name: "Nausea", desc: "", status: "Active" },
    ],
  }));

  reg("prescriptionGroup", () => ({
    subtitle: "OPD Master · prescription groups", codePrefix: "PG", codeLabel: "Group Code",
    fields: [
      { label: "Group Name", name: "name", required: true, placeholder: "e.g. Antenatal" },
      { label: "Status", name: "status", type: "select", options: ACTIVE },
    ],
    columns: [C_CODE(), col("name", "Group Name", { main: true }), col("status", "Status")],
    rows: [
      { code: "PG01", name: "Antenatal", status: "Active" }, { code: "PG02", name: "Infertility", status: "Active" },
      { code: "PG03", name: "Post-operative", status: "Active" }, { code: "PG04", name: "General", status: "Active" },
    ],
  }));

  reg("prescriptionTemplate", () => ({
    subtitle: "OPD Master · prescription templates", codePrefix: "PT", codeLabel: "Template Code",
    formTitle: "New template",
    fields: [
      { label: "Template Name", name: "name", required: true, placeholder: "e.g. IUI follow-up" },
      { label: "Prescription Group", name: "grp", type: "select", options: ["Antenatal", "Infertility", "Post-operative", "General"] },
      { label: "Medicines / advice", name: "body", type: "textarea", rows: 4, placeholder: "One per line" },
      { label: "Status", name: "status", type: "select", options: ACTIVE },
    ],
    columns: [C_CODE(), col("name", "Template Name", { main: true }), col("grp", "Group"), col("status", "Status")],
    rows: [
      { code: "PT01", name: "IUI Follow-up", grp: "Infertility", status: "Active" },
      { code: "PT02", name: "ANC Routine", grp: "Antenatal", status: "Active" },
    ],
  }));
  P.prescriptionTemplate2 = P.prescriptionTemplate;

  reg("doctorSpeciality", () => ({
    subtitle: "Master · doctor specialities", codePrefix: "SP", codeLabel: "Speciality Code",
    fields: [
      { label: "Speciality Name", name: "name", required: true, placeholder: "e.g. Reproductive Medicine" },
      { label: "Status", name: "status", type: "select", options: ACTIVE },
    ],
    columns: [C_CODE(), col("name", "Speciality", { main: true }), col("status", "Status")],
    rows: [
      { code: "SP01", name: "Reproductive Medicine", status: "Active" }, { code: "SP02", name: "Obstetrics & Gynaecology", status: "Active" },
      { code: "SP03", name: "Embryology", status: "Active" }, { code: "SP04", name: "Andrology", status: "Active" },
    ],
  }));

  reg("doctorType", () => ({
    subtitle: "Master · doctor types", codePrefix: "DT", codeLabel: "Type Code",
    fields: [
      { label: "Doctor Type", name: "name", required: true, placeholder: "e.g. Consultant" },
      { label: "Status", name: "status", type: "select", options: ACTIVE },
    ],
    columns: [C_CODE(), col("name", "Doctor Type", { main: true }), col("status", "Status")],
    rows: [
      { code: "DT01", name: "Consultant", status: "Active" }, { code: "DT02", name: "Visiting", status: "Active" },
      { code: "DT03", name: "Resident", status: "Active" }, { code: "DT04", name: "Referrer", status: "Active" },
    ],
  }));

  reg("doctorMaster", () => ({
    subtitle: "Master · doctors", codePrefix: "DOC", codeLabel: "Doctor Code", formTitle: "New doctor",
    fields: [
      { label: "Doctor Name", name: "name", required: true, placeholder: "Dr. …" },
      { label: "Speciality", name: "spec", type: "select", options: ["Reproductive Medicine", "Obstetrics & Gynaecology", "Embryology", "Andrology"] },
      { label: "Doctor Type", name: "type", type: "select", options: ["Consultant", "Visiting", "Resident"] },
      { label: "Qualification", name: "qual", placeholder: "MBBS, MD, DNB…" },
      { label: "Reg. No (MCI)", name: "mci", placeholder: "Medical council reg." },
      { label: "Contact No", name: "phone", type: "prefix", prefix: "+91", inputmode: "numeric", maxlength: 10 },
      { label: "Status", name: "status", type: "select", options: ACTIVE },
    ],
    columns: [C_CODE(), col("name", "Doctor Name", { main: true }), col("spec", "Speciality"), col("type", "Type"), col("qual", "Qualification"), col("status", "Status")],
    rows: [
      { code: "DOC01", name: "Dr. Anjali Mehta", spec: "Reproductive Medicine", type: "Consultant", qual: "MBBS, MD, DNB", status: "Active" },
      { code: "DOC02", name: "Dr. Rohan Kapoor", spec: "Andrology", type: "Consultant", qual: "MBBS, MS", status: "Active" },
      { code: "DOC03", name: "Dr. Sneha Iyer", spec: "Embryology", type: "Consultant", qual: "PhD", status: "Active" },
      { code: "DOC04", name: "Dr. Vikram Sen", spec: "Obstetrics & Gynaecology", type: "Visiting", qual: "MBBS, MD", status: "Active" },
    ],
  }));

  /* ---------- Investigation / Pharmacy shared masters ---------- */
  reg("departmentMaster", () => ({
    subtitle: "Investigation · departments", codePrefix: "DEP", codeLabel: "Dept Code",
    fields: [
      { label: "Department Name", name: "name", required: true, placeholder: "e.g. Pathology" },
      { label: "Status", name: "status", type: "select", options: ACTIVE },
    ],
    columns: [C_CODE(), col("name", "Department", { main: true }), col("status", "Status")],
    rows: [
      { code: "DEP01", name: "Pathology", status: "Active" }, { code: "DEP02", name: "Biochemistry", status: "Active" },
      { code: "DEP03", name: "Radiology / USG", status: "Active" }, { code: "DEP04", name: "Hormone Assay", status: "Active" },
    ],
  }));

  reg("manufacturerMaster", () => ({
    subtitle: "Master · manufacturers", codePrefix: "MFR", codeLabel: "Mfr Code",
    fields: [
      { label: "Manufacturer Name", name: "name", required: true, placeholder: "Company name" },
      { label: "Address", name: "addr", type: "textarea", rows: 2 },
      { label: "Contact No", name: "phone", inputmode: "numeric", maxlength: 10 },
      { label: "Status", name: "status", type: "select", options: ACTIVE },
    ],
    columns: [C_CODE(), col("name", "Manufacturer", { main: true }), col("phone", "Contact"), col("status", "Status")],
    rows: [
      { code: "MFR01", name: "Merck Serono", phone: "1800100200", status: "Active" },
      { code: "MFR02", name: "Sun Pharma", phone: "1800100201", status: "Active" },
      { code: "MFR03", name: "Cipla Ltd", phone: "1800100202", status: "Active" },
    ],
  }));

  reg("supplierMaster", () => ({
    subtitle: "Master · suppliers", codePrefix: "SUP", codeLabel: "Supplier Code", formTitle: "New supplier",
    fields: [
      { label: "Supplier Name", name: "name", required: true, placeholder: "Company name" },
      { label: "Contact Person", name: "person", placeholder: "Name" },
      { label: "Address", name: "addr", type: "textarea", rows: 2 },
      { label: "Contact No", name: "phone", inputmode: "numeric", maxlength: 10 },
      { label: "GSTIN", name: "gstin", placeholder: "GST number" },
      { label: "Status", name: "status", type: "select", options: ACTIVE },
    ],
    columns: [C_CODE(), col("name", "Supplier", { main: true }), col("person", "Contact Person"), col("phone", "Contact"), col("gstin", "GSTIN"), col("status", "Status")],
    rows: [
      { code: "SUP01", name: "MediSupply Co.", person: "R. Banerjee", phone: "9830011223", gstin: "19ABCDE1234F1Z5", status: "Active" },
      { code: "SUP02", name: "LabKart Distributors", person: "S. Ghosh", phone: "9830011224", gstin: "19PQRST5678G1Z2", status: "Active" },
      { code: "SUP03", name: "PharmaHub", person: "A. Das", phone: "9830011225", gstin: "19UVWXY9012H1Z8", status: "Active" },
    ],
  }));

  reg("testGroupMaster", () => ({
    subtitle: "Investigation · investigation groups", codePrefix: "IG", codeLabel: "Group Code",
    fields: [
      { label: "Group Name", name: "name", required: true, placeholder: "e.g. Hormone Profile" },
      { label: "Department", name: "dept", type: "select", options: ["Pathology", "Biochemistry", "Radiology / USG", "Hormone Assay"] },
      { label: "Status", name: "status", type: "select", options: ACTIVE },
    ],
    columns: [C_CODE(), col("name", "Group Name", { main: true }), col("dept", "Department"), col("status", "Status")],
    rows: [
      { code: "IG01", name: "Hormone Profile", dept: "Hormone Assay", status: "Active" },
      { code: "IG02", name: "Haematology", dept: "Pathology", status: "Active" },
      { code: "IG03", name: "USG Studies", dept: "Radiology / USG", status: "Active" },
    ],
  }));

  reg("testMaster", () => ({
    subtitle: "Investigation · investigations / tests", codePrefix: "TST", codeLabel: "Test Code", formTitle: "New investigation",
    fields: [
      { label: "Investigation Name", name: "name", required: true, placeholder: "e.g. AMH" },
      { label: "Group", name: "grp", type: "select", options: ["Hormone Profile", "Haematology", "USG Studies"] },
      { label: "Department", name: "dept", type: "select", options: ["Pathology", "Biochemistry", "Radiology / USG", "Hormone Assay"] },
      { label: "Sample / Specimen", name: "sample", placeholder: "Serum / Blood / —" },
      { label: "Unit", name: "unit", placeholder: "ng/mL" },
      { label: "Normal Range", name: "range", placeholder: "1.0 – 4.0" },
      { label: "Rate (₹)", name: "rate", inputmode: "numeric", placeholder: "0" },
      { label: "Status", name: "status", type: "select", options: ACTIVE },
    ],
    columns: [C_CODE(), col("name", "Investigation", { main: true }), col("grp", "Group"), col("sample", "Sample"), col("range", "Normal Range"), col("rate", "Rate", { num: true }), col("status", "Status")],
    rows: [
      { code: "TST01", name: "AMH", grp: "Hormone Profile", sample: "Serum", range: "1.0 – 4.0 ng/mL", rate: "1800", status: "Active" },
      { code: "TST02", name: "FSH", grp: "Hormone Profile", sample: "Serum", range: "3.5 – 12.5 mIU/mL", rate: "650", status: "Active" },
      { code: "TST03", name: "CBC", grp: "Haematology", sample: "Blood", range: "—", rate: "350", status: "Active" },
      { code: "TST04", name: "Follicular Study USG", grp: "USG Studies", sample: "—", range: "—", rate: "1200", status: "Active" },
    ],
  }));

  /* ---------- Pharmacy small masters ---------- */
  reg("medicineRoute", () => ({
    subtitle: "Pharmacy · medicine routes", codePrefix: "RT", codeLabel: "Route Code",
    fields: [{ label: "Route Name", name: "name", required: true, placeholder: "e.g. Oral" }, { label: "Status", name: "status", type: "select", options: ACTIVE }],
    columns: [C_CODE(), col("name", "Route", { main: true }), col("status", "Status")],
    rows: [{ code: "RT01", name: "Oral", status: "Active" }, { code: "RT02", name: "Intramuscular (IM)", status: "Active" }, { code: "RT03", name: "Subcutaneous (SC)", status: "Active" }, { code: "RT04", name: "Intravenous (IV)", status: "Active" }, { code: "RT05", name: "Vaginal", status: "Active" }],
  }));
  reg("doseMaster", () => ({
    subtitle: "Pharmacy · dose master", codePrefix: "DS", codeLabel: "Dose Code",
    fields: [{ label: "Dose", name: "name", required: true, placeholder: "e.g. 1-0-1" }, { label: "Description", name: "desc", placeholder: "Morning-Noon-Night" }, { label: "Status", name: "status", type: "select", options: ACTIVE }],
    columns: [C_CODE(), col("name", "Dose", { main: true }), col("desc", "Description"), col("status", "Status")],
    rows: [{ code: "DS01", name: "1-0-1", desc: "Morning & Night", status: "Active" }, { code: "DS02", name: "1-1-1", desc: "Thrice daily", status: "Active" }, { code: "DS03", name: "0-0-1", desc: "Night only", status: "Active" }, { code: "DS04", name: "SOS", desc: "As needed", status: "Active" }],
  }));
  reg("durationMaster", () => ({
    subtitle: "Pharmacy · duration master", codePrefix: "DU", codeLabel: "Duration Code",
    fields: [{ label: "Duration", name: "name", required: true, placeholder: "e.g. 5 Days" }, { label: "Status", name: "status", type: "select", options: ACTIVE }],
    columns: [C_CODE(), col("name", "Duration", { main: true }), col("status", "Status")],
    rows: [{ code: "DU01", name: "3 Days", status: "Active" }, { code: "DU02", name: "5 Days", status: "Active" }, { code: "DU03", name: "7 Days", status: "Active" }, { code: "DU04", name: "1 Month", status: "Active" }],
  }));
  reg("medicineGroup", () => ({
    subtitle: "Pharmacy · medicine groups", codePrefix: "MG", codeLabel: "Group Code",
    fields: [{ label: "Group Name", name: "name", required: true, placeholder: "e.g. Hormones" }, { label: "Status", name: "status", type: "select", options: ACTIVE }],
    columns: [C_CODE(), col("name", "Group Name", { main: true }), col("status", "Status")],
    rows: [{ code: "MG01", name: "Hormones (Gonadotropins)", status: "Active" }, { code: "MG02", name: "Antibiotics", status: "Active" }, { code: "MG03", name: "Analgesics", status: "Active" }, { code: "MG04", name: "Supplements", status: "Active" }],
  }));
  reg("medicineSubGroup", () => ({
    subtitle: "Pharmacy · medicine sub-groups", codePrefix: "MSG", codeLabel: "Sub-Group Code",
    fields: [{ label: "Sub Group Name", name: "name", required: true, placeholder: "e.g. FSH" }, { label: "Medicine Group", name: "grp", type: "select", options: ["Hormones (Gonadotropins)", "Antibiotics", "Analgesics", "Supplements"] }, { label: "Status", name: "status", type: "select", options: ACTIVE }],
    columns: [C_CODE(), col("name", "Sub Group", { main: true }), col("grp", "Group"), col("status", "Status")],
    rows: [{ code: "MSG01", name: "FSH", grp: "Hormones (Gonadotropins)", status: "Active" }, { code: "MSG02", name: "hMG", grp: "Hormones (Gonadotropins)", status: "Active" }, { code: "MSG03", name: "Folic Acid", grp: "Supplements", status: "Active" }],
  }));
  reg("medicineMaster", () => ({
    subtitle: "Pharmacy · medicine / reagent master", codePrefix: "MED", codeLabel: "Item Code", formTitle: "New medicine / reagent",
    fields: [
      { label: "Item Name", name: "name", required: true, placeholder: "e.g. Gonal-F 75 IU" },
      { label: "Group", name: "grp", type: "select", options: ["Hormones (Gonadotropins)", "Antibiotics", "Analgesics", "Supplements"] },
      { label: "Sub Group", name: "subgrp", type: "select", options: ["FSH", "hMG", "Folic Acid"] },
      { label: "Manufacturer", name: "mfr", type: "select", options: ["Merck Serono", "Sun Pharma", "Cipla Ltd"] },
      { label: "Unit", name: "unit", placeholder: "Vial / Strip / Tab" },
      { label: "HSN Code", name: "hsn", placeholder: "HSN" },
      { label: "MRP (₹)", name: "mrp", inputmode: "numeric", placeholder: "0" },
      { label: "Reorder Level", name: "reorder", inputmode: "numeric", placeholder: "0" },
      { label: "Status", name: "status", type: "select", options: ACTIVE },
    ],
    columns: [C_CODE(), col("name", "Item Name", { main: true }), col("grp", "Group"), col("mfr", "Manufacturer"), col("unit", "Unit"), col("mrp", "MRP", { num: true }), col("status", "Status")],
    rows: [
      { code: "MED01", name: "Gonal-F 75 IU", grp: "Hormones (Gonadotropins)", mfr: "Merck Serono", unit: "Vial", mrp: "1150", status: "Active" },
      { code: "MED02", name: "Menopur 75 IU", grp: "Hormones (Gonadotropins)", mfr: "Merck Serono", unit: "Vial", mrp: "990", status: "Active" },
      { code: "MED03", name: "Folvite 5 mg", grp: "Supplements", mfr: "Cipla Ltd", unit: "Strip", mrp: "28", status: "Active" },
      { code: "MED04", name: "Augmentin 625", grp: "Antibiotics", mfr: "Sun Pharma", unit: "Strip", mrp: "210", status: "Active" },
    ],
  }));
  reg("ledgerMaster", () => ({
    subtitle: "Accounts · ledger heads", codePrefix: "LG", codeLabel: "Ledger Code",
    fields: [{ label: "Ledger Name", name: "name", required: true, placeholder: "e.g. Consultation Income" }, { label: "Group", name: "grp", type: "select", options: ["Income", "Expense", "Asset", "Liability"] }, { label: "Status", name: "status", type: "select", options: ACTIVE }],
    columns: [C_CODE(), col("name", "Ledger", { main: true }), col("grp", "Group"), col("status", "Status")],
    rows: [{ code: "LG01", name: "Consultation Income", grp: "Income", status: "Active" }, { code: "LG02", name: "Pharmacy Sales", grp: "Income", status: "Active" }, { code: "LG03", name: "Procedure Income", grp: "Income", status: "Active" }, { code: "LG04", name: "Salaries", grp: "Expense", status: "Active" }],
  }));

  /* ---------- Settings ---------- */
  reg("designationMaster", () => ({
    subtitle: "Settings · designations", codePrefix: "DG", codeLabel: "Designation Code",
    fields: [{ label: "Designation", name: "name", required: true, placeholder: "e.g. Staff Nurse" }, { label: "Status", name: "status", type: "select", options: ACTIVE }],
    columns: [C_CODE(), col("name", "Designation", { main: true }), col("status", "Status")],
    rows: [{ code: "DG01", name: "Embryologist", status: "Active" }, { code: "DG02", name: "Staff Nurse", status: "Active" }, { code: "DG03", name: "Receptionist", status: "Active" }, { code: "DG04", name: "Accountant", status: "Active" }, { code: "DG05", name: "Pharmacist", status: "Active" }],
  }));
  reg("employeeMaster", () => ({
    subtitle: "Settings · employees", codePrefix: "EMP", codeLabel: "Employee Code", formTitle: "New employee",
    fields: [
      { label: "Employee Name", name: "name", required: true, placeholder: "Full name" },
      { label: "Designation", name: "desig", type: "select", options: ["Embryologist", "Staff Nurse", "Receptionist", "Accountant", "Pharmacist"] },
      { label: "Gender", name: "sex", type: "select", options: ["Female", "Male", "Other"] },
      { label: "Date of Joining", name: "doj", type: "date" },
      { label: "Contact No", name: "phone", type: "prefix", prefix: "+91", inputmode: "numeric", maxlength: 10 },
      { label: "Email Id", name: "email", type: "email" },
      { label: "Address", name: "addr", type: "textarea", rows: 2, full: true },
      { label: "Status", name: "status", type: "select", options: ACTIVE },
    ],
    columns: [C_CODE(), col("name", "Employee", { main: true }), col("desig", "Designation"), col("sex", "Gender"), col("phone", "Contact"), col("status", "Status")],
    rows: [
      { code: "EMP01", name: "Kavita Sen", desig: "Staff Nurse", sex: "Female", phone: "9830055661", status: "Active" },
      { code: "EMP02", name: "Arnab Roy", desig: "Embryologist", sex: "Male", phone: "9830055662", status: "Active" },
      { code: "EMP03", name: "Pooja Singh", desig: "Receptionist", sex: "Female", phone: "9830055663", status: "Active" },
    ],
  }));

  /* ---------- Security ---------- */
  reg("userRole", () => ({
    subtitle: "Security · user roles", codePrefix: "RL", codeLabel: "Role Code",
    fields: [{ label: "Role Name", name: "name", required: true, placeholder: "e.g. Front Desk" }, { label: "Description", name: "desc", placeholder: "Optional" }, { label: "Status", name: "status", type: "select", options: ACTIVE }],
    columns: [C_CODE(), col("name", "Role", { main: true }), col("desc", "Description"), col("status", "Status")],
    rows: [{ code: "RL01", name: "Administrator", desc: "Full access", status: "Active" }, { code: "RL02", name: "Front Desk", desc: "OPD reception", status: "Active" }, { code: "RL03", name: "Lab Technician", desc: "Investigation", status: "Active" }, { code: "RL04", name: "Pharmacist", desc: "Pharmacy", status: "Active" }, { code: "RL05", name: "Accountant", desc: "Accounts", status: "Active" }],
  }));
  reg("moduleDetails", () => ({
    subtitle: "Security · modules", codePrefix: "MOD", codeLabel: "Module Code",
    fields: [{ label: "Module Name", name: "name", required: true }, { label: "Display Order", name: "ord", inputmode: "numeric" }, { label: "Status", name: "status", type: "select", options: ACTIVE }],
    columns: [C_CODE(), col("name", "Module", { main: true }), col("ord", "Order", { num: true }), col("status", "Status")],
    rows: SITEMAP.filter(m => m.groups).map((m, i) => ({ code: "MOD0" + (i + 1), name: m.label, ord: i + 1, status: "Active" })),
  }));
  reg("subModuleDetails", () => ({
    subtitle: "Security · sub-modules", codePrefix: "SUB", codeLabel: "Sub-Module Code",
    fields: [{ label: "Module", name: "mod", type: "select", options: SITEMAP.filter(m => m.groups).map(m => m.label) }, { label: "Sub Module Name", name: "name", required: true }, { label: "Status", name: "status", type: "select", options: ACTIVE }],
    columns: [C_CODE(), col("mod", "Module"), col("name", "Sub Module", { main: true }), col("status", "Status")],
    rows: (() => { const out = []; let i = 1; SITEMAP.filter(m => m.groups).forEach(m => m.groups.forEach(g => out.push({ code: "SUB" + String(i++).padStart(2, "0"), mod: m.label, name: g.label, status: "Active" }))); return out; })(),
  }));
  reg("menuDetails", () => ({
    subtitle: "Security · menus", codePrefix: "MN", codeLabel: "Menu Code",
    fields: [{ label: "Sub Module", name: "sub", placeholder: "e.g. OPD Master" }, { label: "Menu Name", name: "name", required: true }, { label: "Page URL", name: "url", placeholder: "OPD/PatientEnrollment.aspx" }, { label: "Status", name: "status", type: "select", options: ACTIVE }],
    columns: [C_CODE(), col("name", "Menu", { main: true }), col("url", "Page URL"), col("status", "Status")],
    rows: (() => { const out = []; let i = 1; SITEMAP.filter(m => m.groups).forEach(m => m.groups.forEach(g => g.items.forEach(it => out.push({ code: "MN" + String(i++).padStart(3, "0"), name: it.label, url: it.src || "", status: "Active" })))); return out.slice(0, 20); })(),
  }));

  /* ---------- Pharmacy dashboard (master-ish landing) ---------- */
})();
