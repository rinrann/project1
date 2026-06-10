using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Data.SqlClient;
using System.Drawing;
using System.Web.Services;
using System.Globalization;
using System.Collections.Generic;
public partial class IPD_TotalTransactionPopup : System.Web.UI.Page
{
    string LedgerId;
    TotalTransactionPopupClass theHelper = new  TotalTransactionPopupClass(ConfigurationSettings.AppSettings["ConnectionString"].ToString());
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (Session["LedgerId"] != null)
            {
                LedgerId = Session["LedgerId"].ToString();
                DataTable dt = theHelper.GetDetails(LedgerId, Session["CoCode"].ToString().Trim(), Session["YearCode"].ToString().Trim());

                if (dt.Rows.Count > 0)
                {
                    txtBillNo.Text = dt.Rows[0]["BillNo"].ToString();
                    txtRegNo.Text = dt.Rows[0]["PatientReg"].ToString();
                    txtName.Text = dt.Rows[0]["patient_name"].ToString();
                    txtAge.Text = dt.Rows[0]["age"].ToString();
                    txtAddress.Text = dt.Rows[0]["vill_city"].ToString();
                    txtPhNo.Text = dt.Rows[0]["PhNo1"].ToString();
                    txtBedCharge.Text = dt.Rows[0]["BedCharge"].ToString();
                    txtDoctorVisitCharge.Text = dt.Rows[0]["DoctorVisit"].ToString();
                    txtMedicineCharge.Text = dt.Rows[0]["Medicine"].ToString();
                    txtConsumable.Text = dt.Rows[0]["Consumable"].ToString();
                    txtServiceCharge.Text = dt.Rows[0]["SeviceDtls"].ToString();
                    txtpathology.Text = dt.Rows[0]["Pathology"].ToString();
                    txtXRayCharge.Text = dt.Rows[0]["XRay"].ToString();
                    txtusg.Text = dt.Rows[0]["USG"].ToString();
                    txtOTCharge.Text = dt.Rows[0]["OTCharges"].ToString();
                    txtotAttendence.Text = dt.Rows[0]["OTAttendenceCharge"].ToString();
                    txtAmbulance.Text = dt.Rows[0]["Ambulance"].ToString();
                    txtotConsumableCharge.Text = dt.Rows[0]["OTConsumableharge"].ToString();
                    txtSisterAyaCharge.Text = dt.Rows[0]["SisterAya"].ToString();
                    txtInstrument.Text = dt.Rows[0]["Instrument"].ToString();
                    txtAnesthesiaconsumable.Text = dt.Rows[0]["AnesthesiaConsumable"].ToString();
                    txtAnesthesiaMedicine.Text = dt.Rows[0]["AnesthesiaMedicine"].ToString();
                    txtDue.Text = dt.Rows[0]["DueAmount"].ToString();
                    txtDiscount.Text = dt.Rows[0]["Discount"].ToString();
                    txtTotal.Text = dt.Rows[0]["Total"].ToString();
                }
            }

            Session["LedgerId"] = null;
           
        }

    }
}