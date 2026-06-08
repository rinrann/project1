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

public partial class Account_AnyTimePayment : System.Web.UI.Page
{
    Main Objmain = new Main(ConfigurationSettings.AppSettings["ConnectionString"].ToString());
    AnyTimePayment anypayment = new AnyTimePayment(ConfigurationSettings.AppSettings["ConnectionString"].ToString());
    BillGeneration thepatientbill = new BillGeneration(ConfigurationSettings.AppSettings["ConnectionString"].ToString());
    Eclamsia12 theabortion = new Eclamsia12(ConfigurationSettings.AppSettings["ConnectionString"].ToString());
    DC_DialysisPayment thepdia = new DC_DialysisPayment(ConfigurationSettings.AppSettings["ConnectionString"].ToString());

    System.Text.StringBuilder rpt = new System.Text.StringBuilder();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["userName"] == null)
        {
            Response.Redirect("../LoginPage.aspx");
        }
        if (Objmain.checkaccss(Session["CoCode"].ToString(), Session["UserRoleID"].ToString().Trim(), "ANYTIME PAYMENT", checkAccessType.ViewAction) == false)
        {
            Response.Redirect("../AccessDenied.aspx");
        }

        if (Objmain.checkaccss(Session["CoCode"].ToString(), Session["UserRoleID"].ToString().Trim(), "ANYTIME PAYMENT", checkAccessType.InsertAction) == false)
        {
            Button1.Enabled = false;
        }
        Page.Title = "Any Time Payment";
        string path = HttpContext.Current.Request.Url.Query;
        if (!IsPostBack)
        {
            Panel1.Visible = false;
            DropDownFill();
            txtdate.Text = DateTime.Now.ToString("dd/MM/yyyy");
            string[] arr = path.Split('?');
            if (arr.Length > 1 && Request.QueryString["regno"] != "")
            {
                txtreg.Text = Request.QueryString["regno"].ToString();
                FillDetails(Request.QueryString["regno"].ToString());
            }
        }
    }

    public void DropDownFill()
    {
        string cocode = Session["CoCode"].ToString().Trim();
        string yearcode = Session["YearCode"].ToString().Trim();
        DropDownList1.Items.Clear();
        this.DropDownList1.DataSource = anypayment.DropdownPaymentMode(cocode);
        this.DropDownList1.DataTextField = "Name";
        this.DropDownList1.DataValueField = "ID";
        this.DropDownList1.DataBind();
        this.DropDownList1.Items.Insert(0, new ListItem("--Select--", "0"));

        txtreason.Items.Clear();
        txtreason.Items.Insert(0, new ListItem("--Select--", "0"));
        txtreason.Items.Insert(1, new ListItem("Discharge Payment", "Discharge Payment"));
        txtreason.Items.Insert(2, new ListItem("Advance Payment", "Advance Payment"));
        txtreason.Items.Insert(3, new ListItem("Other", "Other"));
        string curDate = DateTime.Now.ToString("yyyy-MM-dd");
        DataTable accountStatus = anypayment.StatusLinkAccount(cocode, yearcode);
        if (accountStatus.Rows.Count > 0)
        {
            if (accountStatus.Rows[0]["LinkAccount"].ToString() == "1")
            {
                ddlBook.Items.Clear();
                this.ddlBook.DataSource = anypayment.DropdownBkcodes(cocode);
                this.ddlBook.DataTextField = "bkname";
                this.ddlBook.DataValueField = "bkcode";
                this.ddlBook.DataBind();
                this.ddlBook.Items.Insert(0, new ListItem("--Select--", "0"));
                ddlBook.Visible = true;
            }
            else
            {
                ddlBook.Items.Clear();
                this.ddlBook.Items.Insert(0, new ListItem("--Select--", "0"));
                ddlBook.Visible = false;
            }
            HiddenField1.Value = accountStatus.Rows[0]["LinkAccount"].ToString();
        }
        else
        {
            ddlBook.Items.Clear();
            this.ddlBook.Items.Insert(0, new ListItem("--Select--", "0"));
            ddlBook.Visible = false;
            HiddenField1.Value = "0";
        }
    }
    protected void resetAll()
    {
        txtname.Text = ""; TextBox1.Text = ""; TextBox3.Text = "";
        TextBox2.Text = ""; txtpaid.Text = ""; txtdue.Text = ""; txtamt.Text = "";
        txtdiscount.Text = ""; DropDownList1.SelectedValue = "0"; txtreason.SelectedValue= "0";
        txtdueamt.Text = ""; CheckBox1.Checked = false;
    }
    protected void Button5_Click(object sender, EventArgs e)
    {
        FillDetails(txtreg.Text);
    }
    public void FillDetails(string regno)
    {
        double debit = 0.00;
        double due = 0.00;
        string cocode = Session["CoCode"].ToString().Trim();
        resetAll();
        try
        {

            DataTable DialysisDetails = thepdia.GetDialysisDetails(regno, cocode);
            DataTable DialysisAdvAmt = thepdia.GetAmount(regno, cocode);

            if (DialysisAdvAmt.Rows.Count > 0 && DialysisDetails.Rows.Count > 0)
            {
                HdnPatientType.Value = "D";
                txtname.Text = DialysisDetails.Rows[0]["patient_name"].ToString();
                TextBox2.Text = DialysisDetails.Rows[0]["Debit"].ToString();
                txtpaid.Text = (Convert.ToDouble(DialysisAdvAmt.Rows[0]["Advance"]) - Convert.ToDouble(DialysisAdvAmt.Rows[0]["Refund"])).ToString();
                txtdue.Text = DialysisAdvAmt.Rows[0]["Due"].ToString();
                TextBox1.Text = DialysisDetails.Rows[0]["guardian_name"].ToString();
                TextBox3.Text = DialysisDetails.Rows[0]["vill_city"].ToString();
                due = Convert.ToDouble(DialysisAdvAmt.Rows[0]["Due"]);
                if (due >= 0)
                {
                    Label2.Text = "Due Amount :";
                    Label2.ForeColor = Color.Red;

                    Label3.Text = "Due Amount :";
                    Label3.ForeColor = Color.Red;

                    txtdue.Text = due.ToString();
                    txtdueamt.Text = "";
                    CheckBox1.Enabled = true;
                    txtRefundamt.Value = "0";
                    txtamt.Enabled = true; txtdiscount.Enabled = true; DropDownList1.Enabled = true;
                }
                else
                {
                    CheckBox1.Checked = false;
                    CheckBox1.Enabled = false;

                    Label2.Text = "Amount to Refund :";
                    Label2.ForeColor = Color.Green;

                    Label3.Text = "Amount to Refund :";
                    Label3.ForeColor = Color.Green;
                    txtdue.Text = (-due).ToString();
                    txtdueamt.Text = (-due).ToString();
                    txtRefundamt.Value = (-due).ToString();
                    CheckBox1.Enabled = true;
                    txtamt.Text = ""; txtdiscount.Text = ""; DropDownList1.SelectedIndex = 0;
                    txtamt.Enabled = false; txtdiscount.Enabled = false; DropDownList1.Enabled = false;
                }
            }
            else
            {

                DataTable custTable = anypayment.Get_IPD_PatientDetails(regno, cocode);
                DataTable GetCredit = anypayment.GetCredit(regno, cocode);
                DataSet thetotal = thepatientbill.QuickTotalBillDtls(regno, custTable.Rows[0]["LedgerId"].ToString(), DateTime.Now.ToString("yyyy-MM-dd"), Session["CoCode"].ToString());
                if (custTable.Rows.Count > 0 && GetCredit.Rows.Count > 0)
                {
                    HdnPatientType.Value = "I";
                    debit = Convert.ToDouble(thetotal.Tables[1].Rows[0][0]);
                    due = debit - Convert.ToDouble(GetCredit.Rows[0][0]);
                    TextBox1.Text = custTable.Rows[0]["guardian_name"].ToString();
                    TextBox3.Text = custTable.Rows[0]["vill_city"].ToString();
                    txtname.Text = custTable.Rows[0]["patient_name"].ToString();
                    TextBox2.Text = debit.ToString();
                    txtpaid.Text = GetCredit.Rows[0][0].ToString();
                    if (due >= 0)
                    {
                        Label2.Text = "Due Amount :";
                        Label2.ForeColor = Color.Red;

                        Label3.Text = "Due Amount :";
                        Label3.ForeColor = Color.Red;

                        txtdue.Text = due.ToString();
                        txtdueamt.Text = "";
                        CheckBox1.Enabled = true;
                        txtRefundamt.Value = "0";
                        txtamt.Enabled = true; txtdiscount.Enabled = true; DropDownList1.Enabled = true;
                    }
                    else
                    {
                        CheckBox1.Checked = false;
                        CheckBox1.Enabled = false;

                        Label2.Text = "Amount to Refund :";
                        Label2.ForeColor = Color.Green;

                        Label3.Text = "Amount to Refund :";
                        Label3.ForeColor = Color.Green;
                        txtdue.Text = (-due).ToString();
                        txtdueamt.Text = (-due).ToString();
                        txtRefundamt.Value = (-due).ToString();
                        CheckBox1.Enabled = true;
                        txtamt.Text = ""; txtdiscount.Text = ""; DropDownList1.SelectedIndex = 0;
                        txtamt.Enabled = false; txtdiscount.Enabled = false; DropDownList1.Enabled = false;
                    }
                }
            }
        }
        catch (Exception ex)
        {
            lblError.ForeColor = System.Drawing.Color.Red;
            lblError.Text = ex.Message;
        }
    }

    [System.Web.Script.Services.ScriptMethod()]
    [System.Web.Services.WebMethod]

    public static List<string> SearchCustomers(string prefixText, int count)
    {
        using (SqlConnection conn = new SqlConnection())
        {
            conn.ConnectionString = ConfigurationSettings.AppSettings["ConnectionString"].ToString();
            using (SqlCommand cmd = new SqlCommand())
            {
                cmd.CommandText = "select distinct PatientReg +'-' + patient_name as Name from GN_PatientReg where patient_name like @SearchText + '%'";
                cmd.Parameters.AddWithValue("@SearchText", prefixText);
                cmd.Connection = conn;
                conn.Open();
                List<string> customers = new List<string>();
                using (SqlDataReader sdr = cmd.ExecuteReader())
                {
                    while (sdr.Read())
                    {
                        customers.Add(sdr["Name"].ToString());
                    }
                }
                conn.Close();
                return customers;
            }
        }
    }



    protected void Button1_Click(object sender, EventArgs e)
    {
        DateTime dt = System.DateTime.Now;
        string due = "";
        string refund = "";
        string cocode = Session["CoCode"].ToString().Trim();
        string yearcode = Session["YearCode"].ToString().Trim();
        string strdate="";
        
        System.Globalization.DateTimeFormatInfo dtf = new DateTimeFormatInfo();
        DateTime testdate;
        if (txtdate.Text == "")
        {
            
            ScriptManager.RegisterStartupScript(this, this.GetType(), "msg", "alert('Enter Payment Date!');", true);
            
        }
        else
        {

            testdate = DateTime.ParseExact(txtdate.Text, "dd/MM/yyyy", dtf);
            strdate = DateTime.ParseExact(txtdate.Text, "dd/MM/yyyy", null).ToString("MM/dd/yyyy");
            if (anypayment.checkDate(cocode, yearcode, strdate) == true)
            {
                if (Label2.Text == "Amount to Refund :")
                {
                    refund = txtdueamt.Text;
                }
                else
                {
                    if (CheckBox1.Checked)
                        due = txtdueamt.Text;
                    else
                        due = "";
                }
                DataTable receiprcode = anypayment.GenerateReceiptCode(cocode);
                TextBox4.Value = receiprcode.Rows[0][0].ToString();
                if (anypayment.InsertPayment(cocode, yearcode, DropDownList1.SelectedValue, txtamt.Text, testdate.ToString("yyyy-MM-dd"), Session["userName"].ToString(), txtreg.Text, txtreason.SelectedValue.ToString(), receiprcode.Rows[0][0].ToString(), due, txtdiscount.Text, refund, TextBox5.Text) == true)
                {
                    Button1.Enabled = false;
                    lblError.ForeColor = System.Drawing.Color.Green;
                    lblError.Text = "Inserted Successfully";
                    string curDate = DateTime.Now.ToString("yyyy-MM-dd");
                    DataTable accountStatus = anypayment.StatusLinkAccount(cocode, yearcode);
                    if (accountStatus.Rows.Count > 0 && accountStatus.Rows[0]["LinkAccount"].ToString() == "1")
                    { passJV.Visible = true; passPayment.Visible = true; }
                    else { passJV.Visible = false; passPayment.Visible = false; }
                    PrintMoneyreceipt();
                }
                else
                {
                    lblError.ForeColor = System.Drawing.Color.Red;
                    lblError.Text = "Error in Inserted Data";
                    passJV.Visible = false;
                }
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "msg", "alert('Payment Date must be within financial year!');", true);
            }
            // ResetAllFields();
        }
    }

    public void PrintMoneyreceipt()
    {
        Panel1.Visible = true;
        Report_Header();
        if (DropDownList1.SelectedValue == "5")
        {
            GetHearder_Detail1();
        }
        else
        {
            GetHearder_Detail();
        }
        ltrReport.Text = rpt.ToString();
    }


    public void GetReport()
    {
        Report_Header();
        GetHearder_Detail();
        ltrReport.Text = rpt.ToString();
        showPaymentButton();
    }

    public void GetReport1()
    {
        Report_Header();
        GetHearder_Detail1();
        ltrReport.Text = rpt.ToString();
        showPaymentButton();
    }
    public void showPaymentButton()
    {
        string curDate = DateTime.Now.ToString("yyyy-MM-dd");
        DataTable accountStatus = anypayment.StatusLinkAccount(Session["CoCode"].ToString().Trim(), Session["YearCode"].ToString().Trim());
        if (accountStatus.Rows.Count > 0)
        {
            if (accountStatus.Rows[0]["LinkAccount"].ToString() == "1")
            {
                passJV.Enabled = true;
                passPayment.Enabled = true;
            }
            else
            {
                passJV.Enabled = false;
                passPayment.Enabled = false;
            }
        }
        else
        {
            passJV.Enabled = false;
            passPayment.Enabled = false;
        }
    }

    public void Report_Header()
    {
        rpt.Append("<table width='100%' cellspacing=0 border=0 style='border:1px solid black;border-spacing: 0; padding: 0;font-family:Arial;'>");
        rpt.Append("<tr cellpadding:'0'  style='height:30px;'>");
        rpt.Append("<td rowspan='3' width='2%' style='font-family:Arial; font-size:x-large; font-weight:bold;padding-left:100px;text-align:left'><img src='/Images/gfclogo.png'  style='height:77px;width:75px;'/></td>");
        rpt.Append("<td   width='20%'  style='height:20px;font-family:Arial; font-size:x-large; font-weight:bold;text-align:center'><u>GFC HOSPITAL</u></td>");
        rpt.Append("<td rowspan='3'  width='2%' style='height:20px;font-family:Arial; font-size:x-large;padding-right:100px; font-weight:bold;text-align:right'><img src='/Images/gfclogo.png'  style='height:77px;width:75px;'/></td>");
        rpt.Append("</tr>");

        rpt.Append("<tr  style='height:20px;'>");
        rpt.AppendFormat("<td   width='20%'  style='height:10px;font-family:Arial; font-size:small; font-weight:bold;text-align:center'>(Regn. No : NH-315/G-70/2013)</td>");
        rpt.Append("</tr>");

        rpt.Append("<tr  style='height:10px;'>");
        rpt.AppendFormat("<td  width='70%'  style='height:15px;font-family:Arial; font-size:medium; font-weight:bold;text-align:center'>Kushpata, Ghatal, Paschim Medinipur, WB, 721212</td>");

        rpt.Append("</tr>");
        rpt.Append("</table>");
    }


    public void GetHearder_Detail()
    {
        ltrReport.Text = "";
        string cocode = Session["CoCode"].ToString().Trim();
        DataTable dt = anypayment.Get_IPD_PatientDetails(txtreg.Text, cocode);
        if (dt.Rows.Count > 0)
        {
            rpt.Append("<br/>");
            rpt.Append("<table width='100%' cellspacing=0 border=0 style=' border-spacing: 0; padding: 0;font-family:verdana;'>");

            rpt.Append("<tr style='height:40px'>");
            rpt.Append("<td colspan='6' style='width: 8%; font-family:Arial;font-weight:bold; font-size:larger; text-align:center'><u> Money Receipt </u></td>");
            rpt.Append("</tr'>");
            rpt.Append("<tr style='height:40px'>");
            rpt.Append("<td colspan='6' style='width: 8%; font-family:Arial;font-weight:bold; font-size:larger; text-align:left'><br/><br/> </td>");
            rpt.Append("</tr'>");
            rpt.Append("<tr style='height:40px'>");
            rpt.AppendFormat("<td colspan='6' style='width: 8%; font-family:Arial;font-weight:bold; font-size:larger; text-align:left'> Receipt No :{0} </td>", TextBox4.Value);
            rpt.Append("</tr'>");



            rpt.Append("<tr style='height:40px'>");
            rpt.AppendFormat("<td colspan='6' style='width: 8%; font-family:Arial;font-weight:bold; font-size:larger; text-align:left'> Date : {0}</td>", DateTime.Now.ToString("dd/MM/yyyy"));
            rpt.Append("</tr'>");

            rpt.Append("</table>");


            rpt.Append("<table width='100%' cellspacing=0 border=0 style=' border-spacing: 0; padding: 0;font-family:verdana;'>");
            rpt.Append("<tr style='height:35px'>");
            if (Convert.ToDecimal(txtRefundamt.Value) > 0)
            {
                string refundamt_word = anypayment.Get_AmountinWords(txtRefundamt.Value);
                if (txtreason.Text == "")
                {
                    rpt.AppendFormat("<td style='font-family:Arial;font-size:small; text-align:justify'>Refund To <b>{0} Rs. {1} ({2})</b></td>", dt.Rows[0]["patient_name"], txtRefundamt.Value, refundamt_word);
                }
                else
                {
                    rpt.AppendFormat("<td style='font-family:Arial;font-size:small; text-align:justify'>Refund To <b>{0} Rs. {1} ({2})</b> for <b>{3}</b></td>", dt.Rows[0]["patient_name"], txtRefundamt.Value, refundamt_word, txtreason.Text);

                }
            }
            else
            {

                rpt.AppendFormat("<td style='font-family:Arial;font-size:small; text-align:justify'>Received from <b>{0} Rs. {1} ({2})</b> for <b>{3}</b></td>", dt.Rows[0]["patient_name"], txtamt.Text, anypayment.AmountinWords, txtreason.Text);

            }
            rpt.Append("</tr >");
            rpt.Append("</table>");

            rpt.Append("<table width='100%' cellspacing=0 border=0 style=' border-spacing: 0; padding: 0;font-family:verdana;'>");

            rpt.Append("<tr style='height:35px'>");
            rpt.Append("<td style='font-family:Arial;font-size:small; text-align:left'>Mode Of Payment : <b> Cash</b></td>");
            rpt.Append("</tr >");

            rpt.Append("</table>");

            rpt.Append("<br/>"); rpt.Append("<br/>");
            rpt.Append("<table width='100%' cellspacing=0 border=0 style=' border-spacing: 0; padding: 0;font-family:verdana;'>");
            rpt.Append("<tr>");
            rpt.Append("<td style='font-family:Arial;font-size:medium;font-weight:bold; text-align:left'> Signature of Party </td>");
            rpt.Append("<td style='font-family:Arial;font-size:medium;font-weight:bold; padding-right:100px;text-align:right'> For GFC </td>");
            rpt.Append("</tr>");
            rpt.Append("</table>");
        }
        ltrReport.Visible = true;

    }


    public void GetHearder_Detail1()
    {
        ltrReport.Text = "";
        string cocode = Session["CoCode"].ToString().Trim();
        DataTable dt = anypayment.Get_IPD_PatientDetails(txtreg.Text, cocode);
        if (dt.Rows.Count > 0)
        {
            rpt.Append("<br/>");
            rpt.Append("<table width='100%' cellspacing=0 border=0 style=' border-spacing: 0; padding: 0;font-family:verdana;'>");

            rpt.Append("<tr style='height:40px'>");
            rpt.Append("<td colspan='6' style='width: 8%; font-family:Arial;font-weight:bold; font-size:larger; text-align:center'><u> Money Receipt </u></td>");
            rpt.Append("</tr'>");
            rpt.Append("<tr style='height:40px'>");
            rpt.Append("<td colspan='6' style='width: 8%; font-family:Arial;font-weight:bold; font-size:larger; text-align:left'><br/><br/> </td>");
            rpt.Append("</tr'>");
            rpt.Append("<tr style='height:40px'>");
            rpt.AppendFormat("<td colspan='6' style='width: 8%; font-family:Arial;font-weight:bold; font-size:larger; text-align:left'> Receipt No :{0} </td>", TextBox4.Value);
            rpt.Append("</tr'>");



            rpt.Append("<tr style='height:40px'>");
            rpt.AppendFormat("<td colspan='6' style='width: 8%; font-family:Arial;font-weight:bold; font-size:larger; text-align:left'> Date : {0}</td>", DateTime.Now.ToString("dd/MM/yyyy"));
            rpt.Append("</tr'>");

            rpt.Append("</table>");


            rpt.Append("<table width='100%' cellspacing=0 border=0 style=' border-spacing: 0; padding: 0;font-family:verdana;'>");
            rpt.Append("<tr style='height:35px'>");

            if (Convert.ToDecimal(txtRefundamt.Value) > 0)
            {
                string refundamt_word = anypayment.Get_AmountinWords(txtRefundamt.Value);
                if (txtreason.Text == "")
                {
                    rpt.AppendFormat("<td style='font-family:Arial;font-size:small; text-align:justify'>Refund To <b>{0} Rs. {1} ({2})</b></td>", dt.Rows[0]["patient_name"], txtRefundamt.Value, refundamt_word);
                }
                else
                {
                    rpt.AppendFormat("<td style='font-family:Arial;font-size:small; text-align:justify'>Refund To <b>{0} Rs. {1} ({2})</b> for <b>{3}</b></td>", dt.Rows[0]["patient_name"], txtRefundamt.Value, refundamt_word, txtreason.Text);

                }
            }
            else
            {
                rpt.AppendFormat("<td style='font-family:Arial;font-size:small; text-align:justify'>Received from <b>{0} Rs. {1} ({2})</b> for <b>{3}</b></td>", dt.Rows[0]["patient_name"], txtamt.Text, anypayment.AmountinWords, txtreason.Text);
            }
            rpt.Append("</tr >");
            rpt.Append("</table>");

            rpt.Append("<table width='100%' cellspacing=0 border=0 style=' border-spacing: 0; padding: 0;font-family:verdana;'>");

            rpt.Append("<tr style='height:35px'>");
            rpt.Append("<td style='font-family:Arial;font-size:small; text-align:left'>Mode Of Payment : <b> Card</b></td>");
            rpt.Append("</tr >");

            rpt.Append("</table>");

            rpt.Append("<br/>"); rpt.Append("<br/>");
            rpt.Append("<table width='100%' cellspacing=0 border=0 style=' border-spacing: 0; padding: 0;font-family:verdana;'>");
            rpt.Append("<tr>");
            rpt.Append("<td style='font-family:Arial;font-size:medium;font-weight:bold; text-align:left'> Signature of Party </td>");
            rpt.Append("<td style='font-family:Arial;font-size:medium;font-weight:bold;padding-right:100px; text-align:right'> For GFC </td>");
            rpt.Append("</tr>");
            rpt.Append("</table>");
        }
        ltrReport.Visible = true;


    }
    public void ResetAllFields()
    {
        if (Request.QueryString["regno"] == "") { txtreg.Text = ""; }
        txtname.Text = "";
        txtamt.Text = "";
        TextBox2.Text = ""; TextBox1.Text = ""; TextBox3.Text = "";
        txtpaid.Text = "";
        txtdue.Text = "";

    }
    protected void Button2_Click(object sender, EventArgs e)
    {
        ResetAllFields();
    }
    protected void passJV_Click(object sender, EventArgs e)
    {
        string cocode = Session["CoCode"].ToString().Trim();
        string yearcode = Session["YearCode"].ToString().Trim();
        if (anypayment.CheckPatientGl(cocode, yearcode) == true)
        {
            if (anypayment.PassJournal(cocode, yearcode, txtreg.Text, Session["userName"].ToString(), DateTime.Now.ToString("yyyy-MM-dd"), HdnPatientType.Value.Trim()) == true)
            {
                passJV.Enabled = false;
                ScriptManager.RegisterStartupScript(this, this.GetType(), "msg", "alert('Journal Passed Successfully!');", true);

            }
            else
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "msg", "alert('Error in Processing!');", true);
            }
        }
        else
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "msg", "alert('Please set GlCode for passing journal entry!');", true);
        }
    }
    protected void passPayment_Click(object sender, EventArgs e)
    {
        string cocode = Session["CoCode"].ToString().Trim();
        string yearcode = Session["YearCode"].ToString().Trim();
        if (ddlBook.SelectedValue == "0")
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "msg", "alert('Select Book!');", true);
        }
        else
        {
            if (anypayment.CheckPatientGl(cocode, yearcode) == true)
            {
                if (anypayment.PassPayment(cocode, yearcode, txtreg.Text, ddlBook.SelectedValue, Session["userName"].ToString(), DateTime.Now.ToString("yyyy-MM-dd")) == true)
                {
                    passPayment.Enabled = false;
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "msg", "alert('Processed Successfully!');", true);
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "msg", "alert('Error in Processing!');", true);
                }
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "msg", "alert('Please set GlCode for passing account entry!');", true);
            }
        }
    }

    protected void textreason_change(object sender, EventArgs e)
    {
        if (txtreason.SelectedValue == "Other")
        {
            TextBox5.Visible = true;
        }
        else
        {
            TextBox5.Visible = false;
        }
    }
}