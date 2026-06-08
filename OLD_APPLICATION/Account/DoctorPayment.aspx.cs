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

public partial class Account_DoctorPayment : System.Web.UI.Page
{
    Main Objmain = new Main(ConfigurationSettings.AppSettings["ConnectionString"].ToString());
    DoctorPayment docpayment = new DoctorPayment(ConfigurationSettings.AppSettings["ConnectionString"].ToString());
    AnyTimePayment anypayment = new AnyTimePayment(ConfigurationSettings.AppSettings["ConnectionString"].ToString());
    DoctorPaymentDashBoard theHelper = new DoctorPaymentDashBoard(ConfigurationSettings.AppSettings["ConnectionString"].ToString());
    System.Text.StringBuilder rpt = new System.Text.StringBuilder();
    
    
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["userName"] == null)
        {
            Response.Redirect("../LoginPage.aspx");
        }
        if (Objmain.checkaccss(Session["CoCode"].ToString(), Session["UserRoleID"].ToString().Trim(), "DOCTOR PAYMENT", checkAccessType.ViewAction) == false)
        {
            Response.Redirect("../AccessDenied.aspx");
        }
        if (Objmain.checkaccss(Session["CoCode"].ToString(), Session["UserRoleID"].ToString().Trim(), "DOCTOR PAYMENT", checkAccessType.InsertAction) == false)
        {
            btnSave.Enabled = false;
        }
        Page.Title = "Doctor/Referrer Payment";
        string path = HttpContext.Current.Request.Url.Query;

        if (!IsPostBack)
        {
            Panel1.Visible = false;
            DropDownFill();
            txtdate.Text = DateTime.Now.ToString("dd'/'MM'/'yyyy");
            lblchq.Visible = false;
            txtchqdtl.Visible = false;

            if (Session["docId"].ToString() != "")
            {
                HiddenField1.Value = Session["payeeType"].ToString();
                txtdocid.Text = Session["docId"].ToString();
                FillDetails(Session["docId"].ToString(), Session["docType"].ToString());
            }
        }
    }


    public void DropDownFill()
    {
        string cocode = Session["CoCode"].ToString().Trim();
        string yearcode = Session["YearCode"].ToString().Trim();
        ddlsplz.Items.Clear();
        this.ddlsplz.DataSource = docpayment.GetDoctorSpeciality(cocode);
        this.ddlsplz.DataTextField = "SpecialtyName";
        this.ddlsplz.DataValueField = "SpecialtyID";
        this.ddlsplz.DataBind();
        this.ddlsplz.Items.Insert(0, new ListItem("--Select--", "0"));

        ddldoctype.Items.Clear();
        this.ddldoctype.DataSource = docpayment.GetDoctorType(cocode);
        this.ddldoctype.DataTextField = "TypeName";
        this.ddldoctype.DataValueField = "DocTypeId";
        this.ddldoctype.DataBind();
        this.ddldoctype.Items.Insert(0, new ListItem("--Select--", "0"));

        ddlpaymode.Items.Clear();
        this.ddlpaymode.DataSource = docpayment.DropdownPaymentMode(cocode);
        this.ddlpaymode.DataTextField = "Name";
        this.ddlpaymode.DataValueField = "ID";
        this.ddlpaymode.DataBind();
        this.ddlpaymode.Items.Insert(0, new ListItem("--Select--", "0"));

        OTanalcode.Value = docpayment.GetAnal(cocode, "TA");
        VisitAnalcode.Value = docpayment.GetAnal(cocode, "D");
        ReferAnalcode.Value = docpayment.GetAnal(cocode, "R");
        OTcostcode.Value = docpayment.GetCostcode(cocode, "T");
        IpdCostcode.Value = docpayment.GetCostcode(cocode, "I");
        OpdCostcode.Value = docpayment.GetCostcode(cocode, "O");

        DataTable accountStatus = anypayment.StatusLinkAccount(cocode,yearcode);
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


    protected void btnSave_Click(object sender, EventArgs e)
    {
        string cocode = Session["CoCode"].ToString().Trim();
        string yearcode = Session["YearCode"].ToString().Trim();
        DateTime dt = System.DateTime.Now;
        string strdate = "";
        System.Globalization.DateTimeFormatInfo dtf = new DateTimeFormatInfo();
        DateTime testdate;

        if (txtdate.Text == "")
        {

            ScriptManager.RegisterStartupScript(this, this.GetType(), "msg", "alert('Enter Payment Date!');", true);

        }
        else
        {
            DataTable receiprcode = anypayment.GenerateReceiptCode(cocode);
            TextBox4.Value = receiprcode.Rows[0][0].ToString();
            string PatientLedger = Session["patientLedger"].ToString();
            DataTable accountStatus = anypayment.StatusLinkAccount(cocode, yearcode);
            if (accountStatus.Rows.Count > 0 && accountStatus.Rows[0]["LinkAccount"].ToString() == "1" && ddlBook.SelectedValue == "0")
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "msg", "alert('Select Book!');", true);
            }
            else
            {
                testdate = DateTime.ParseExact(txtdate.Text, "dd/MM/yyyy", dtf);
                strdate = DateTime.ParseExact(txtdate.Text, "dd/MM/yyyy", null).ToString("MM/dd/yyyy");
                if (anypayment.checkDate(cocode, yearcode, strdate) == true)
                {
                    if (docpayment.Insertdocpayment(txtdocid.Text, OTcostcode.Value, IpdCostcode.Value, OpdCostcode.Value, OTanalcode.Value, VisitAnalcode.Value, ReferAnalcode.Value, txtamtOT.Text, txtdiscountOT.Text, txtamtAn.Text, txtdiscountAn.Text, txtamtVisit.Text, txtdiscountVisit.Text, txtamtRefer.Text, txtdiscountRefer.Text, txtdiscount.Text, testdate.ToString("yyyy-MM-dd"), ddlpaymode.SelectedValue, ddlBook.SelectedValue, Session["userName"].ToString(), cocode, yearcode, txtchqdtl.Text, PatientLedger.Trim(), TextBox4.Value.Trim()) == true)
                    {
                        btnSave.Enabled = false;
                        lblError.ForeColor = System.Drawing.Color.Green;
                        lblError.Text = "Inserted Successfully";
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "msg", "alert('Inserted Successfully !');", true);
                        ResetAllFields();
                    }
                    else
                    {
                        lblError.ForeColor = System.Drawing.Color.Red;
                        lblError.Text = "Error in Inserted Data";
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "msg", "alert('Error in Updated Data !');", true);
                    }
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "msg", "alert('Payment Date must be within financial year!');", true);
                }
            }
        }
    }

    protected void btnBack_Click(object sender, EventArgs e)
    {
        Response.Redirect("../Account/DoctorPaymentDashBoard.aspx");
    }
    public void Fillddldoctype()
    {
        string cocode = Session["CoCode"].ToString().Trim();
        if (HiddenField1.Value == "D")
        {
            ddldoctype.Items.Clear();
            this.ddldoctype.DataSource = docpayment.GetDoctorType(cocode);
            this.ddldoctype.DataTextField = "TypeName";
            this.ddldoctype.DataValueField = "DocTypeId";
            this.ddldoctype.DataBind();
            this.ddldoctype.Items.Insert(0, new ListItem("--Select--", "0"));
        }
        else
        {
            ddldoctype.Items.Clear();
            this.ddldoctype.Items.Insert(0, new ListItem("--Select--", "0"));
            this.ddldoctype.Items.Insert(1, new ListItem("Asha", "A"));
            this.ddldoctype.Items.Insert(2, new ListItem("Rural Doctor", "Q"));
            this.ddldoctype.Items.Insert(3, new ListItem("Ambulance", "B"));
            this.ddldoctype.Items.Insert(4, new ListItem("Car-Private", "P"));
            this.ddldoctype.Items.Insert(5, new ListItem("Car-Rented", "R"));
            this.ddldoctype.Items.Insert(6, new ListItem("Others", "O"));
        }
    }
    public void ResetAllFields()
    {
        if (Request.QueryString["id"] == "") { txtdocid.Text = ""; }
        txtdocname.Text = "";
        txtamt.Text = "";
        txtaddr.Text = "";
        ddldoctype.SelectedIndex = -1;
        ddlsplz.SelectedIndex = -1;
        Fillddldoctype();
    }

    protected void btnCancel_Click(object sender, EventArgs e)
    {
        ResetAllFields();
    }

    protected void btnFetch_Click(object sender, EventArgs e)
    {
        FillDetails(txtdocid.Text, ddldoctype.SelectedValue);
    }
    protected void ddlpaymodeselectedIndexchng(object sender, EventArgs e)
    {
        string paymode = ddlpaymode.SelectedValue.ToString();
        if (paymode == "7")
        {
            lblchq.Visible = true;
            txtchqdtl.Visible = true;
        }
        else
        {
            lblchq.Visible = false;
            txtchqdtl.Visible = false;
        }
    }
    public void FillDetails(string docId, string docType)
    {
        ResetAllFields();
        string cocode = Session["CoCode"].ToString().Trim();
        string yearcode = Session["YearCode"].ToString().Trim();
        try
        {
            string PatientLedger = Session["patientLedger"].ToString();
            if (Session["patientLedger"].ToString() == "")
            {
                DataTable dtdoc = theHelper.GetDocdetl(cocode, yearcode, docId, "null", "null");
            }
            DataTable DocDetails = docpayment.GetDialysisDetails(docId, HiddenField1.Value, cocode);
            DataTable Docpayment = docpayment.GetDoctorPayment(cocode, yearcode, docId, docType, PatientLedger);
            if (DocDetails.Rows.Count > 0)
            {
                if (HiddenField1.Value == "D")
                {
                    txtdocname.Text = DocDetails.Rows[0]["dname"].ToString();
                    txtaddr.Text = DocDetails.Rows[0]["Address"].ToString();
                    ddldoctype.SelectedValue = DocDetails.Rows[0]["DocTypeId"].ToString();
                    ddlsplz.SelectedValue = DocDetails.Rows[0]["SpecialistIn1"].ToString();
                }
                else
                {
                    txtdocname.Text = DocDetails.Rows[0]["QuackName"].ToString();
                    txtaddr.Text = DocDetails.Rows[0]["Address1"].ToString();
                    ddldoctype.SelectedValue = DocDetails.Rows[0]["Type"].ToString().Trim();
                    ddlsplz.Enabled = false;
                }
            }
            if (Docpayment.Rows.Count > 0)
            {
                //TextBox2.Text = (Convert.ToDecimal(Docpayment.Rows[0]["surgoncharge"]) + Convert.ToDecimal(Docpayment.Rows[0]["fees"]) + Convert.ToDecimal(Docpayment.Rows[0]["refercharge"]) + Convert.ToDecimal(Docpayment.Rows[0]["Anesthesischarge"]) - Convert.ToDecimal(Docpayment.Rows[0]["paymentdone"])).ToString();
                TextBox1.Text = (Convert.ToDecimal(Docpayment.Rows[0]["OTcharges"]) + Convert.ToDecimal(Docpayment.Rows[0]["Anesthesischarge"]) + Convert.ToDecimal(Docpayment.Rows[0]["Visitcharges"]) + Convert.ToDecimal(Docpayment.Rows[0]["Refercharges"])).ToString();
                txtamt.Text = TextBox1.Text;
                txtamt.Enabled = false; TextBox1.Enabled = false;
                TextOT.Text = Docpayment.Rows[0]["OTcharges"].ToString();
                TextAnes.Text = Docpayment.Rows[0]["Anesthesischarge"].ToString();
                TextVisit.Text = Docpayment.Rows[0]["Visitcharges"].ToString();
                TextRefer.Text = Docpayment.Rows[0]["Refercharges"].ToString();
                txtamtOT.Text = TextOT.Text; txtamtOT.Enabled = false;
                txtamtAn.Text = TextAnes.Text; txtamtAn.Enabled = false;
                txtamtVisit.Text = TextVisit.Text; txtamtVisit.Enabled = false;
                txtamtRefer.Text = TextRefer.Text; txtamtRefer.Enabled = false;
            }
            else 
            {
                TextBox2.Text = "0.00"; TextBox1.Text = "0.00"; txtamt.Text = "0.00";
                TextOT.Text = "0.00";TextAnes.Text = "0.00";TextVisit.Text = "0.00";TextRefer.Text = "0.00";
                txtamtOT.Text = "0.00";txtamtAn.Text = "0.00";txtamtVisit.Text = "0.00";txtamtRefer.Text = "0.00";
            }
        }
        catch (Exception ex)
        {
            lblError.ForeColor = System.Drawing.Color.Red;
            lblError.Text = ex.Message;
        }
    }

    protected void txtdiscount_TextChanged(object sender, EventArgs e)
    {
        if (txtdiscount.Text != "")
        {
            txtamt.Text = (Convert.ToDecimal(TextBox1.Text) - Convert.ToDecimal(txtdiscount.Text)).ToString();
        }
        else
        {
            txtamt.Text = TextBox1.Text;
        }
    }
}