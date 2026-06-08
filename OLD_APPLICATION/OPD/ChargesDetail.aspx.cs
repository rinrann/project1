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

public partial class OPD_ChargesDetail : System.Web.UI.Page
{
    Main Objmain = new Main(ConfigurationSettings.AppSettings["ConnectionString"].ToString());
    OpdCharges thecharge = new OpdCharges(ConfigurationSettings.AppSettings["ConnectionString"].ToString());
    PatientsDetails theopdpatient = new PatientsDetails(ConfigurationSettings.AppSettings["ConnectionString"].ToString());
    AnyTimePayment anypayment = new AnyTimePayment(ConfigurationSettings.AppSettings["ConnectionString"].ToString());

    protected void Page_Load(object sender, EventArgs e)
    {
        Page.Title = "Charge Details";
        if (Session["userId"] == null)
        {
            Response.Redirect("../LoginPage.aspx");
        }
        if (Objmain.checkaccss(Session["CoCode"].ToString(), Session["UserRoleID"].ToString().Trim(), "CHARGE DETAILS", checkAccessType.ViewAction) == false)
        {
            Response.Redirect("../AccessDenied.aspx");
        }

        if (Objmain.checkaccss(Session["CoCode"].ToString(), Session["UserRoleID"].ToString().Trim(), "CHARGE DETAILS", checkAccessType.InsertAction) == false)
        {
            Button1.Enabled = false;
        }
        if (Objmain.checkaccss(Session["CoCode"].ToString(), Session["UserRoleID"].ToString().Trim(), "CHARGE DETAILS", checkAccessType.InsertAction) == false)
        {
            Button5.Enabled = false;
        }

        if (!IsPostBack)
        {
            if (Session["RegnNo"] != null)
            {
                txtreg.Text = Session["RegnNo"].ToString();
                Fill();
            }
            DropdownFill();
            showPaymentButton();
        }
        Session["RegnNo"] = null;
    }
    public void DropdownFill()
    {
        string cocode = Session["CoCode"].ToString().Trim();
        string yearcode = Session["YearCode"].ToString().Trim();
        DropDownList d;
        for (int c = 1; c < 7; c++)
        {
            d = (DropDownList)Page.FindControl("ctl00$ContentPlaceHolder1$ddl" + c.ToString()); ;
            d.DataSource = thecharge.DropdownGetBillType(Session["CoCode"].ToString().Trim());
            d.DataTextField = "BillTypeName";
            d.DataValueField = "BillTypeId";
            d.DataBind();
        }
        DropDownList1.Items.Clear();
        this.DropDownList1.DataSource = anypayment.DropdownPaymentMode(Session["CoCode"].ToString().Trim());
        this.DropDownList1.DataTextField = "Name";
        this.DropDownList1.DataValueField = "ID";
        this.DropDownList1.DataBind();
        this.DropDownList1.Items.Insert(0, new ListItem("--Select--", "0"));
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
                lblbk.Visible = true;
            }
            else
            {
                ddlBook.Items.Clear();
                this.ddlBook.Items.Insert(0, new ListItem("--Select--", "0"));
                ddlBook.Visible = false;
                lblbk.Visible = false;
            }
            HiddenField1.Value = accountStatus.Rows[0]["LinkAccount"].ToString();
        }
        else
        {
            ddlBook.Items.Clear();
            this.ddlBook.Items.Insert(0, new ListItem("--Select--", "0"));
            ddlBook.Visible = false;
            lblbk.Visible = false;
            HiddenField1.Value = "0";
        }
    }
    private void ResetAllFields()
    {
        txtreg.Text = "";
        txtname.Text = "";
        HiddenField1.Value = "";
        Button1.Text = "Submit";
        txtRgnFee.Text = "";txtDocFees.Text = "";txtUsgChrg.Text = "";txtIuiChrg.Text = "";txtInvChrg.Text = "";
        txtOptChrg.Text = ""; txtMedChrg.Text = ""; txtTotal.Text = "";
        TextBox1.Text = ""; TextBox2.Text = ""; TextBox3.Text = ""; TextBox4.Text = ""; TextBox5.Text = ""; TextBox6.Text = "";
        TextBox7.Text = ""; txtpaid.Text = ""; txtdue.Text = ""; txtdate.Text = ""; txtamt.Text = "";

        if (Objmain.checkaccss(Session["CoCode"].ToString(), Session["UserRoleID"].ToString().Trim(), "CHARGE DETAILS", checkAccessType.InsertAction) == false)
        {
            Button1.Enabled = false;
        }
        if (Objmain.checkaccss(Session["CoCode"].ToString(), Session["UserRoleID"].ToString().Trim(), "CHARGE DETAILS", checkAccessType.InsertAction) == false)
        {
            Button5.Enabled = false;
        }

    }
    private void TotalAmt()
    {
        if (txtRgnFee.Text.Trim() == "")
            txtRgnFee.Text = "0.00";
        if (txtDocFees.Text.Trim() == "")
            txtDocFees.Text = "0.00";
        if (txtUsgChrg.Text.Trim() == "")
            txtUsgChrg.Text = "0.00";
        if (txtIuiChrg.Text.Trim() == "")
            txtIuiChrg.Text = "0.00";
        if (txtInvChrg.Text.Trim() == "")
            txtInvChrg.Text = "0.00";
        if (txtOptChrg.Text.Trim() == "")
            txtOptChrg.Text = "0.00";
        if (txtMedChrg.Text.Trim() == "")
            txtMedChrg.Text = "0.00";
        if (TextBox1.Text.Trim() == "")
            TextBox1.Text = "0.00";
        if (TextBox2.Text.Trim() == "")
            TextBox2.Text = "0.00";
        if (TextBox3.Text.Trim() == "")
            TextBox3.Text = "0.00";
        if (TextBox4.Text.Trim() == "")
            TextBox4.Text = "0.00";
        if (TextBox5.Text.Trim() == "")
            TextBox5.Text = "0.00";
        if (TextBox6.Text.Trim() == "")
            TextBox6.Text = "0.00";
        Decimal total = Convert.ToDecimal(txtRgnFee.Text) + Convert.ToDecimal(txtDocFees.Text) + Convert.ToDecimal(txtUsgChrg.Text) +
                        Convert.ToDecimal(txtIuiChrg.Text)+ Convert.ToDecimal(txtInvChrg.Text) + Convert.ToDecimal(txtOptChrg.Text) + 
                        Convert.ToDecimal(txtMedChrg.Text)+ Convert.ToDecimal(TextBox1.Text) + Convert.ToDecimal(TextBox2.Text) + 
                        Convert.ToDecimal(TextBox3.Text) + Convert.ToDecimal(TextBox4.Text)+ Convert.ToDecimal(TextBox5.Text) + Convert.ToDecimal(TextBox6.Text);
        txtTotal.Text = total.ToString("F2");

    }
    protected void Button1_Click(object sender, EventArgs e)
    {
        string compcode = Session["CoCode"].ToString().Trim();
        string yearcode = Session["YearCode"].ToString().Trim();
        System.Globalization.DateTimeFormatInfo dtf = new DateTimeFormatInfo();
        if (txtRgnFee.Text.Trim() == "")
            txtRgnFee.Text = "0.00";
        if (txtDocFees.Text.Trim() == "")
            txtDocFees.Text = "0.00";
        if (txtUsgChrg.Text.Trim() == "")
            txtUsgChrg.Text = "0.00";
        if (txtIuiChrg.Text.Trim() == "")
            txtIuiChrg.Text = "0.00";
        if (txtInvChrg.Text.Trim() == "")
            txtInvChrg.Text = "0.00";
        if (txtOptChrg.Text.Trim() == "")
            txtOptChrg.Text = "0.00";
        if (txtMedChrg.Text.Trim() == "")
            txtMedChrg.Text = "0.00";
        if (txtRgnFee.Text != "0.00" && txtDocFees.Text != "0.00")
        {
            string testdate = DateTime.Now.ToString("yyyy-MM-dd");

            if (thecharge.InsertChargeDetails(compcode, yearcode, txtreg.Text, testdate, txtRgnFee.Text, txtDocFees.Text, txtUsgChrg.Text, txtIuiChrg.Text, txtInvChrg.Text, txtOptChrg.Text, txtMedChrg.Text, testdate, Session["userId"].ToString()) == true)
            {
                DropDownList d; TextBox t;
                for (int c = 1; c < 7; c++)
                {
                    d = (DropDownList)Page.FindControl("ctl00$ContentPlaceHolder1$ddl" + c.ToString());
                    t = (TextBox)Page.FindControl("ctl00$ContentPlaceHolder1$TextBox" + c.ToString());
                    if (d.SelectedValue!="0" && t.Text.Trim()!="")
                    {
                        thecharge.InsertChargeDetailsMapping(compcode, yearcode, txtreg.Text, testdate, d.SelectedValue, t.Text, testdate, Session["userId"].ToString());               
                    }
                }
                ScriptManager.RegisterStartupScript(this, this.GetType(), "msg", "alert('Saved Successfully !');", true);
                //Response.Redirect("../DayCare/PatientDashBoard.aspx");
                PaymentDetails();
            }
            else
                ScriptManager.RegisterStartupScript(this, this.GetType(), "msg", "alert('Error in Insered Data !');", true);
        }
        //ResetAllFields();
    }


    public void Fill()
    {
        DataSet ds = thecharge.GetBillDetails(Session["CoCode"].ToString().Trim(), Session["YearCode"].ToString().Trim(), txtreg.Text);
        DataTable dt = ds.Tables[0];
        DataTable dt2 = ds.Tables[1];
        if (dt.Rows.Count > 0)
        {
            txtname.Text = dt.Rows[0]["PName"].ToString();
            txtappo.Text = dt.Rows[0]["AppoNo"].ToString();
            txtRgnFee.Text = dt.Rows[0]["RegnFees"].ToString();
            txtDocFees.Text = dt.Rows[0]["DoctorFees"].ToString();
            txtUsgChrg.Text = dt.Rows[0]["USGCharge"].ToString();
            txtIuiChrg.Text = dt.Rows[0]["IUICharge"].ToString();
            txtInvChrg.Text = dt.Rows[0]["InvestigationCharge"].ToString();
            txtOptChrg.Text = dt.Rows[0]["OperationCharge"].ToString();
            txtMedChrg.Text = dt.Rows[0]["MedicineCharge"].ToString();
            if (dt2.Rows.Count > 0)
            {
                DropDownList d; TextBox t;
                for (int c = 0; c < 6; c++)
                {
                    if (c<dt2.Rows.Count )
                    {
                        int i = c + 1;
                        d = (DropDownList)Page.FindControl("ctl00$ContentPlaceHolder1$ddl" + i.ToString());
                        t = (TextBox)Page.FindControl("ctl00$ContentPlaceHolder1$TextBox" + i.ToString());
                        d.SelectedValue = dt2.Rows[c]["BillTypeId"].ToString().Trim();
                        t.Text = dt2.Rows[c]["Charge"].ToString();
                    }
                }
            }
            TotalAmt();
            PaymentDetails();
            showPaymentButton();
        }
        else
        {
            DataTable dt1 = thecharge.PatientFill(Session["CoCode"].ToString().Trim(), txtreg.Text);
            txtname.Text = dt1.Rows[0]["pName"].ToString();
            txtappo.Text = dt1.Rows[0]["AppoNo"].ToString();
        }
    }


    protected void Button2_Click(object sender, EventArgs e)
    {
        PaymentDetails(); txtdate.Text = ""; txtamt.Text = "";
    }
    protected void Button4_Click(object sender, EventArgs e)
    {
        Fill();
    }
    protected void txtRgnFee_TextChanged(object sender, EventArgs e)
    {
        TotalAmt();
    }
    protected void txtDocFees_TextChanged(object sender, EventArgs e)
    {
        TotalAmt();
    }
    protected void txtUsgChrg_TextChanged(object sender, EventArgs e)
    {
        TotalAmt();
    }
    protected void txtIuiChrg_TextChanged(object sender, EventArgs e)
    {
        TotalAmt();
    }
    protected void txtInvChrg_TextChanged(object sender, EventArgs e)
    {
        TotalAmt();
    }
    protected void txtOptChrg_TextChanged(object sender, EventArgs e)
    {
        TotalAmt();
    }
    protected void txtMedChrg_TextChanged(object sender, EventArgs e)
    {
        TotalAmt();
    }
    protected void TextBox1_TextChanged(object sender, EventArgs e)
    {
        TotalAmt();
    }
    protected void TextBox2_TextChanged(object sender, EventArgs e)
    {
        TotalAmt();
    }
    protected void TextBox3_TextChanged(object sender, EventArgs e)
    {
        TotalAmt();
    }
    protected void TextBox4_TextChanged(object sender, EventArgs e)
    {
        TotalAmt();
    }
    protected void TextBox5_TextChanged(object sender, EventArgs e)
    {
        TotalAmt();
    }
    protected void TextBox6_TextChanged(object sender, EventArgs e)
    {
        TotalAmt();
    }
    public void PaymentDetails()
    {
        DataTable dt = thecharge.BillDetails(Session["CoCode"].ToString().Trim(), Session["YearCode"].ToString().Trim(), txtreg.Text);
        DataTable dtpay = thecharge.GetPaymentDetails(Session["CoCode"].ToString().Trim(), Session["YearCode"].ToString().Trim(), txtreg.Text);
        if (dt.Rows.Count > 0)
        {
            TextBox7.Text = dt.Rows[0]["Total"].ToString();
        }
        else { TextBox7.Text = "0.00"; }
        if (dtpay.Rows.Count > 0)
        {
            txtpaid.Text = dtpay.Rows[0]["Income"].ToString();
        }
        else { txtpaid.Text = "0.00"; }
        decimal due = Convert.ToDecimal(TextBox7.Text) - Convert.ToDecimal(txtpaid.Text);
        txtdue.Text = due.ToString();
    }
    protected void Button5_Click(object sender, EventArgs e)
    {
        DateTime dt = System.DateTime.Now;
        string due = "";
        string refund = "";
        string cocode = Session["CoCode"].ToString().Trim();
        string yearcode = Session["YearCode"].ToString().Trim();
        System.Globalization.DateTimeFormatInfo dtf = new DateTimeFormatInfo();
        DateTime testdate;
        if (txtdate.Text != "")
            testdate = DateTime.ParseExact(txtdate.Text, "dd/MM/yyyy", dtf);
        else
            testdate = DateTime.Now;
        
        DataTable receiprcode = anypayment.GenerateReceiptCode(cocode);
        HdnReceipNo.Value = receiprcode.Rows[0][0].ToString();
        if (anypayment.InsertPayment(cocode, yearcode, DropDownList1.SelectedValue, txtamt.Text, testdate.ToString("yyyy-MM-dd"), Session["userId"].ToString(), txtreg.Text, "OPD Advance", receiprcode.Rows[0][0].ToString(), due, "", refund, null) == true)
        {
            Button1.Enabled = false;
            lblpayError.ForeColor = System.Drawing.Color.Green;
            lblpayError.Text = "Inserted Successfully";           
            
        }
        else
        {
            lblpayError.ForeColor = System.Drawing.Color.Red;
            lblpayError.Text = "Error in Inserted Data";
        }
    }

    public void showPaymentButton()
    {
        string curDate = DateTime.Now.ToString("yyyy-MM-dd");
        DataTable accountStatus = anypayment.StatusLinkAccount(Session["CoCode"].ToString().Trim(), Session["YearCode"].ToString().Trim());
        if (accountStatus.Rows.Count > 0)
        {
            if (accountStatus.Rows[0]["LinkAccount"].ToString() == "1")
            {
                //passJV.Enabled = true;
                passPayment.Enabled = true;
                //passJV.Visible = true;
                passPayment.Visible = true;
            }
            else
            {
                passJV.Enabled = false;
                passPayment.Enabled = false;
                passJV.Visible = false;
                passPayment.Visible = false;
            }
        }
        else
        {
            passJV.Enabled = false;
            passPayment.Enabled = false;
            passJV.Visible = false;
            passPayment.Visible = false;
        }
    }
    protected void passJV_Click(object sender, EventArgs e)
    {
        string cocode = Session["CoCode"].ToString().Trim();
        string yearcode = Session["YearCode"].ToString().Trim();
        if (anypayment.PassJournal(cocode, yearcode, txtreg.Text, Session["userId"].ToString(), DateTime.Now.ToString("yyyy-MM-dd"), "O") == true)
        {
            passJV.Enabled = false;
            ScriptManager.RegisterStartupScript(this, this.GetType(), "msg", "alert('Journal Passed Successfully!');", true);

        }
        else
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "msg", "alert('Error in Processing!');", true);
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
            if (anypayment.PassPayment(cocode, yearcode, txtreg.Text, ddlBook.SelectedValue, Session["userId"].ToString(), DateTime.Now.ToString("yyyy-MM-dd")) == true)
            {
                passPayment.Enabled = false;
                ScriptManager.RegisterStartupScript(this, this.GetType(), "msg", "alert('Processed Successfully!');", true);
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "msg", "alert('Error in Processing!');", true);
            }
        }
    }
}