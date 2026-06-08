using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Data;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Configuration;
using System.Collections;
using System.Drawing;
using System.Globalization;


public partial class IPD_Discharge : System.Web.UI.Page
{
    Main Objmain = new Main(ConfigurationSettings.AppSettings["ConnectionString"].ToString());
    DischargeDtls thedischarge = new DischargeDtls(ConfigurationSettings.AppSettings["ConnectionString"].ToString());
    Discharge thepatientbill = new Discharge(ConfigurationSettings.AppSettings["ConnectionString"].ToString()); 
    BillGeneration thepatienttotbill = new BillGeneration(ConfigurationSettings.AppSettings["ConnectionString"].ToString());   
     protected void Page_Load(object sender, EventArgs e)
    {
        Page.Title = "Discharge Details";
        if (Session["userName"] == null)
        {
            Response.Redirect("../LoginPage.aspx");
        }
        if (Objmain.checkaccss(Session["CoCode"].ToString(), Session["UserRoleID"].ToString().Trim(), "DISCHARGE DETAILS", checkAccessType.ViewAction) == false)
        {
            Response.Redirect("../AccessDenied.aspx");
        }

        if (Objmain.checkaccss(Session["CoCode"].ToString(), Session["UserRoleID"].ToString().Trim(), "DISCHARGE DETAILS", checkAccessType.InsertAction) == false)
        {
            Button1.Enabled = false;
        }
        if (!IsPostBack)
        {
            Tab1Func();
            TextBox8.Enabled = false;
            TextBox9.Enabled = false;
            if (Session["RegnNo"] != null)
            {
                txtreg.Text = Session["RegnNo"].ToString();
                Fill();
            }
        }

        Session["RegnNo"] = null;
    }

     [System.Web.Script.Services.ScriptMethod()]
     [System.Web.Services.WebMethod]
     public static List<string> SearchCondition(string prefixText, int count)
     {
         using (SqlConnection conn = new SqlConnection())
         {
             conn.ConnectionString = ConfigurationSettings.AppSettings["ConnectionString"].ToString();
             using (SqlCommand cmd = new SqlCommand())
             {
                 cmd.CommandText = "select distinct dd.Remarks from GN_DischargeDetail dd  where dd.Remarks like @SearchText + '%'";
                 cmd.Parameters.AddWithValue("@SearchText", prefixText);
                 cmd.Connection = conn;
                 conn.Open();
                 List<string> customers = new List<string>();
                 using (SqlDataReader sdr = cmd.ExecuteReader())
                 {
                     while (sdr.Read())
                     {
                         customers.Add(sdr["Remarks"].ToString());
                     }
                 }
                 conn.Close();
                 return customers;
             }
         }
     }



    public void Fill()
    {
        DataTable dt = thedischarge.OnlyName(txtreg.Text);
        TextBox1.Text = dt.Rows[0]["patient_name"].ToString();
        TextBox5.Text = dt.Rows[0]["BedNoText"].ToString();
        TextBox6.Text = dt.Rows[0]["adate"].ToString();
        TextBox7.Text = dt.Rows[0]["AdmissionTime"].ToString();
        HiddenField2.Value = dt.Rows[0]["BedNo"].ToString();
        TextBox2.Text = dt.Rows[0]["ProbDisDate"].ToString();
        txtRefDocid.Text = dt.Rows[0]["ReferID"].ToString();
        txtRefDocnm.Text = dt.Rows[0]["ReferName"].ToString();
        if (dt.Rows[0]["ReferID"].ToString() != "" )//&& dt.Rows[0]["ReferID"].ToString().Length > 3 && dt.Rows[0]["ReferID"].ToString().Substring(0, 3) == "DOC")
        {
            DataSet ds = thepatientbill.PatientForReport(txtreg.Text, Session["CoCode"].ToString().Trim());
            DataTable paydt = thepatientbill.GetPayments(Session["CoCode"].ToString().Trim(), Session["YearCode"].ToString().Trim(),ds.Tables[0].Rows[0]["LedgerId"].ToString().Trim());
            DataSet total = thepatienttotbill.QuickTotalBillDtls(txtreg.Text, ds.Tables[0].Rows[0]["LedgerId"].ToString(), DateTime.Now.ToString("yyyy-MM-dd"), Session["CoCode"].ToString().Trim());
            DataTable dtbill = total.Tables[0];
            DataTable dttotal = total.Tables[1];
            DataTable comm = thepatientbill.doctorCommision(txtRefDocid.Text, Session["CoCode"].ToString().Trim());
            string commrule = thepatientbill.CommisionCalRule(Session["CoCode"].ToString().Trim(), Session["YearCode"].ToString().Trim());
            if (comm != null && comm.Rows.Count > 0)
            {
                if (Convert.ToDecimal(comm.Rows[0]["Commission_Rs"]) > 0)
                {
                    txtDocComm.Text = comm.Rows[0]["Commission_Rs"].ToString();
                }
                else if (Convert.ToDecimal(comm.Rows[0]["Commission_Per"]) > 0)
                {
                    if (commrule.Trim() == "B")
                    {
                        string otAttndCharge; string doccharge;
                        DataSet otattendence = thepatientbill.OTAttendenceBill(ds.Tables[0].Rows[0]["LedgerId"].ToString(), Session["CoCode"].ToString().Trim());
                        DataTable dttotalotattendence = otattendence.Tables[1];
                        DataSet docvisit = thepatientbill.DoctorVisitBill(ds.Tables[0].Rows[0]["LedgerId"].ToString(), Session["CoCode"].ToString().Trim());
                        DataTable dttotaldocvisit = docvisit.Tables[1];
                        if (dttotalotattendence != null && dttotalotattendence.Rows.Count > 0 && dttotalotattendence.Rows[0]["TotalBill"].ToString() != "") { otAttndCharge = dttotalotattendence.Rows[0]["TotalBill"].ToString(); } else { otAttndCharge = "0.00"; }
                        if (dttotaldocvisit != null && dttotaldocvisit.Rows.Count > 0 && dttotaldocvisit.Rows[0]["TotalBill"].ToString() != "") { doccharge = dttotaldocvisit.Rows[0]["TotalBill"].ToString(); } else { doccharge = "0.00"; }
                        txtDocComm.Text = ((Convert.ToDecimal(dttotal.Rows[0]["Total"]) - (Convert.ToDecimal(doccharge) + Convert.ToDecimal(otAttndCharge) + Convert.ToDecimal(paydt.Rows[0]["due_amt"]) + Convert.ToDecimal(paydt.Rows[0]["discount_amt"]))) * Convert.ToDecimal(comm.Rows[0]["Commission_Per"]) / 100).ToString("F2");
                    }
                    else
                    {
                        txtDocComm.Text = ((Convert.ToDecimal(dttotal.Rows[0]["Total"]) - (Convert.ToDecimal(paydt.Rows[0]["due_amt"]) + Convert.ToDecimal(paydt.Rows[0]["discount_amt"]))) * Convert.ToDecimal(comm.Rows[0]["Commission_Per"]) / 100).ToString();
                    }
                }
            }
            else
            {
                txtDocComm.Text = "0.00";
            }
        }
        else
        {
            txtDocComm.Text = "0.00";
        }
    }
    public void GridFill()
    {
        DataTable dt = thedischarge.GridFill(txtreg.Text, Session["CoCode"].ToString().Trim(), Session["YearCode"].ToString().Trim());
        GridView1.DataSource = dt;
        GridView1.DataBind();

    }

    protected void Button5_Click(object sender, EventArgs e)
    {
        Fill();
        GridFill();
    }

    public void BillDetailsInsert()
    {
        string compcode=Session["CoCode"].ToString().Trim();
        string yearcode = Session["YearCode"].ToString().Trim();

        DataSet ambulance = thepatientbill.AmbulanceBill(thedischarge.LedgerId, compcode);
        DataTable dtambulance = ambulance.Tables[1];


        DataSet OTInstrument = thepatientbill.OTInstrumentBill(thedischarge.LedgerId, compcode);
        DataTable dtOTInstrument = OTInstrument.Tables[1];



        DataSet otattendence = thepatientbill.OTAttendenceBill(thedischarge.LedgerId, Session["CoCode"].ToString().Trim());
        DataTable dtotattendence = otattendence.Tables[1];


        DataSet OtConsumable = thepatientbill.OTConsumableBill(thedischarge.LedgerId, compcode);
        DataTable dtOtConsumable = OtConsumable.Tables[1];




        DataSet sisteraya = thepatientbill.SisterAyaBill(thedischarge.LedgerId, compcode);
        DataTable dtsisteraya = sisteraya.Tables[1];

        DataSet bed = thepatientbill.BedBill(thedischarge.LedgerId, compcode);
        DataTable dtbed = bed.Tables[1];

        DataSet doctorvisit = thepatientbill.DoctorVisitBill(thedischarge.LedgerId, compcode);
        DataTable dtdoctorvisit = doctorvisit.Tables[1];

        DataSet medicine = thepatientbill.MedicineBill(thedischarge.LedgerId, compcode);
        DataTable dtmedicine = medicine.Tables[1];

        DataSet service = thepatientbill.ServiceBill(thedischarge.LedgerId, compcode);
        DataTable dtservice = service.Tables[1];

        DataSet consumable = thepatientbill.ConsumableBill(thedischarge.LedgerId, compcode);
        DataTable dtconsumable = consumable.Tables[1];

        DataSet ot = thepatientbill.OTRequisitionBill(thedischarge.LedgerId, compcode);
        DataTable dtot = ot.Tables[1];

        DataSet pathology = thepatientbill.pathologybill(thedischarge.LedgerId, compcode);
        DataTable dtpathology = pathology.Tables[1];

        DataSet xray = thepatientbill.xraybill(thedischarge.LedgerId, compcode);
        DataTable dtxray = xray.Tables[1];

        DataSet usg = thepatientbill.USGBill(thedischarge.LedgerId, compcode);
        DataTable dtusg = usg.Tables[1];

        DataSet AnesthesiaMedicine = thepatientbill.AnesthesiaMed(thedischarge.LedgerId, compcode);
        DataTable dtAnesthesiaMedicine = AnesthesiaMedicine.Tables[1];

        DataSet AnesthesiaConsumable = thepatientbill.AnesthesiaCons(thedischarge.LedgerId, compcode);
        DataTable dtAnesthesiaConsumable = AnesthesiaConsumable.Tables[1];

        //DataSet totalbill = thepatientbill.Discharge1(txtreg.Text);
        //DataTable dtTotalbill = totalbill.Tables[1];

        DataTable genbill = thepatientbill.GenerateBillNo();

        DataTable dtcheck = thepatientbill.PatientChecking(thedischarge.LedgerId, compcode);
        //if (dtcheck.Rows.Count == 0)
        //{
        if (thepatientbill.InsertPatientBilldtls(compcode,thedischarge.LedgerId, dtotattendence.Rows[0][0].ToString(), dtOtConsumable.Rows[0][0].ToString(), dtAnesthesiaConsumable.Rows[0][0].ToString(), dtAnesthesiaMedicine.Rows[0][0].ToString(), genbill.Rows[0][0].ToString(), txtreg.Text, DateTime.Now.ToString("MM/dd/yyyy"), dtbed.Rows[0][0].ToString(), dtdoctorvisit.Rows[0][0].ToString(), dtmedicine.Rows[0][0].ToString(), dtconsumable.Rows[0][0].ToString(), dtservice.Rows[0][0].ToString(), dtpathology.Rows[0][0].ToString(), dtxray.Rows[0][0].ToString(), dtusg.Rows[0][0].ToString(), dtot.Rows[0][0].ToString(), dtsisteraya.Rows[0][0].ToString(), dtambulance.Rows[0][0].ToString(), dtOTInstrument.Rows[0][0].ToString()) == true)
        {
            lblError.ForeColor = System.Drawing.Color.Green;
            lblError.Text = "Discharged Successfully";

        }
        else
        {
            lblError.ForeColor = System.Drawing.Color.Red;
            lblError.Text = "Error in Discharge Patient";
        }
    }
    protected void Button1_Click(object sender, EventArgs e)
    {
        System.Globalization.DateTimeFormatInfo dtf = new DateTimeFormatInfo();
        int isdeath=0;
        string death = "";
        string doccomm;
        if (CheckBox1.Checked == true)
            isdeath = 1;
        else
            isdeath = 0;
        DateTime testdate = DateTime.ParseExact(TextBox2.Text, "dd/MM/yyyy", dtf);
        DateTime testdate1 = DateTime.ParseExact(TextBox6.Text, "dd/MM/yyyy", dtf);
        if (TextBox8.Text != "")
            death = DateTime.ParseExact(TextBox8.Text, "dd/MM/yyyy", dtf).ToString("yyyy-MM-dd");
        else
            death = "";
        if (txtDocComm.Text == "")
        {
            doccomm = "0.00";
        }
        else
        {
            doccomm = txtDocComm.Text;
        }
       // BillDetailsInsert();
        if (Button1.Text == "Submit")
        {
            if (thedischarge.InsertDischarge(isdeath.ToString(), death, TextBox9.Text, HiddenField2.Value, txtreg.Text, testdate.ToString("yyyy-MM-dd"), TextBox3.Text, TextBox4.Text, testdate1.ToString("yyyy-MM-dd"), TextBox7.Text, txtRefDocid.Text, doccomm.ToString(), Session["userName"].ToString(), Session["CoCode"].ToString().Trim(), Session["YearCode"].ToString().Trim(), Session["userName"].ToString().Trim()) == true)
            {
                BillDetailsInsert();
                Response.Redirect("../IPD/AdmissionPatientList.aspx");
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "msg", "alert('Error in Inserted Data !');", true);
            }
        }
        else
        {
            if (thedischarge.updateDischarge(isdeath.ToString(), death, TextBox9.Text, HiddenField1.Value, txtreg.Text, testdate.ToString(), TextBox3.Text, TextBox4.Text, txtRefDocid.Text, txtDocComm.Text, Session["CoCode"].ToString().Trim(), Session["YearCode"].ToString().Trim()) == true)
            {
                //ScriptManager.RegisterStartupScript(this, this.GetType(), "msg", "alert('Updated Successfully !');", true);
                Response.Redirect("../IPD/AdmissionPatientList.aspx");
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "msg", "alert('Error in Updated Data !');", true);
            }
        }
        //GridFill();
        //ResetAllFields();
    }

    public void ResetAllFields()
    {
        txtreg.Text = "";
       
        HiddenField1.Value = "";
        HiddenField2.Value = "";
        TextBox1.Text = ""; TextBox2.Text = ""; TextBox3.Text = ""; TextBox4.Text = ""; TextBox5.Text = "";
        if (Objmain.checkaccss(Session["CoCode"].ToString(), Session["UserRoleID"].ToString().Trim(), "DISCHARGE DETAILS", checkAccessType.InsertAction) == false)
        {
            Button1.Enabled = false;
        }

    }
    protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "Select")
        {
            lblError.Text = "";
            int index = Convert.ToInt32(e.CommandArgument);

            Label lblid = (Label)GridView1.Rows[index].FindControl("lblid");
            HiddenField1.Value = lblid.Text;


            Label lblregno = (Label)GridView1.Rows[index].FindControl("lblregno");
            txtreg.Text = lblregno.Text;


            Label lblname = (Label)GridView1.Rows[index].FindControl("lblname");
            TextBox1.Text = lblname.Text;

            Label lbldate = (Label)GridView1.Rows[index].FindControl("lbldate");
            TextBox2.Text = lbldate.Text;

            Label lbltime = (Label)GridView1.Rows[index].FindControl("lbltime");
            TextBox3.Text = lbltime.Text;


            Tab1Func();

            Label lblcon = (Label)GridView1.Rows[index].FindControl("lblcon");
            TextBox4.Text = lblcon.Text;
            Button1.Text = "Update";
            if (Objmain.checkaccss(Session["CoCode"].ToString(), Session["UserRoleID"].ToString().Trim(), "DISCHARGE DETAILS", checkAccessType.UpdateAction) == false)
            {
                Button1.Enabled = false;
            }
            else
            {
                Button1.Enabled = true;
            }
        }
    }

    protected void Button2_Click(object sender, EventArgs e)
    {
        ResetAllFields();
    }

    public void Tab1Func()
    {
        Tab1.CssClass = "Clicked";
        Tab2.CssClass = "Initial";
        MainView.ActiveViewIndex = 0;
    }
    protected void Tab1_Click(object sender, EventArgs e)
    {
        Tab1Func();
    }
    protected void Tab2_Click(object sender, EventArgs e)
    {
        Tab1.CssClass = "Initial";
        Tab2.CssClass = "Clicked";
        MainView.ActiveViewIndex = 1;
    }
    protected void CheckBox1_CheckedChanged(object sender, EventArgs e)
    {
        if (CheckBox1.Checked==true)
        {
            TextBox8.Enabled = true;
            TextBox9.Enabled = true;
        }
        else
        {
            TextBox8.Text = ""; TextBox9.Text = "";
            TextBox8.Enabled = false;
            TextBox9.Enabled = false;
        }
    }
}