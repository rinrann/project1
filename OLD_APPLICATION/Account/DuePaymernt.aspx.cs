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

public partial class Account_DuePaymernt : System.Web.UI.Page
{
    DuePaymentClass theduePaymentClass = new DuePaymentClass(ConfigurationSettings.AppSettings["ConnectionString"].ToString());
    Main Objmain = new Main(ConfigurationSettings.AppSettings["ConnectionString"].ToString());
    AnyTimePayment anypayment = new AnyTimePayment(ConfigurationSettings.AppSettings["ConnectionString"].ToString());
    System.Text.StringBuilder rpt = new System.Text.StringBuilder();
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            Page.Title = "Due Payment";
            if (Session["userName"] == null)
            {
                Response.Redirect("../LoginPage.aspx");
            }
            if (Objmain.checkaccss(Session["CoCode"].ToString(), Session["UserRoleID"].ToString().Trim(), "BILL ADJUSTMENT", checkAccessType.ViewAction) == false)
            {
                Response.Redirect("../AccessDenied.aspx");
            }

            if (Objmain.checkaccss(Session["CoCode"].ToString(), Session["UserRoleID"].ToString().Trim(), "BILL ADJUSTMENT", checkAccessType.InsertAction) == false)
            {
                Button1.Enabled = false;
            }
            if (!IsPostBack)
            {
                GridFill();
                DropDownFill();
            }
        }
        catch (Exception ex)
        {
            lblError.Text = ex.Message;
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
    protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "Select")
        {
            int index = Convert.ToInt32(e.CommandArgument);
            lblError.Text = "";

            Label lblPatientReg = (Label)GridView1.Rows[index].FindControl("lblPatientReg");
            txtreg.Text = lblPatientReg.Text;

            Label lblpatient_name = (Label)GridView1.Rows[index].FindControl("lblpatient_name");
            txtname.Text = lblpatient_name.Text;

            Label lblvill_City = (Label)GridView1.Rows[index].FindControl("lblvill_City");
            txtAddress.Text = lblvill_City.Text;

            Label lblPhNo1 = (Label)GridView1.Rows[index].FindControl("lblPhNo1");
            string[] a = lblPhNo1.Text.Split(' ');
            txtPhNo.Text = a[1];

            Label lblDebit = (Label)GridView1.Rows[index].FindControl("lblDebit");
            txtdueamt.Text = lblDebit.Text;
            HiddenField1.Value = lblDebit.Text;

            Label lblLedgerID = (Label)GridView1.Rows[index].FindControl("lblLedgerID");
            Label lblTransactionId = (Label)GridView1.Rows[index].FindControl("lblTransactionId");

            HiddenField2.Value = lblLedgerID.Text + "#" + lblTransactionId.Text;
        }
    }

    private void GridFill()
    {
        GridView1.DataSource = theduePaymentClass.GridFill(Session["CoCode"].ToString().Trim(), Session["YearCode"].ToString().Trim(), txtreg.Text, txtname.Text, txtAddress.Text, txtPhNo.Text);
        GridView1.DataBind();
    }
    protected void Button3_Click(object sender, EventArgs e)
    {
        GridFill();
    }
    protected void Button1_Click(object sender, EventArgs e)
    {
        string cocode = Session["CoCode"].ToString().Trim();
        string yearcode = Session["YearCode"].ToString().Trim();
        DataTable receiprcode = theduePaymentClass.GenerateReceiptCode(Session["CoCode"].ToString().Trim(), Session["YearCode"].ToString().Trim());
        HiddenField1.Value = receiprcode.Rows[0][0].ToString();
        string[] splitVal = HiddenField2.Value.Split('#');
        if (theduePaymentClass.InsertPayment(cocode, yearcode,splitVal[0], splitVal[1], DateTime.Now.ToString("yyyy-MM-dd"), Session["userName"].ToString(), txtreg.Text, txtreason.Text, receiprcode.Rows[0][0].ToString(), txtamt.Text, txtdueamt.Text, txtdiscount.Text,DropDownList1.SelectedValue,ddlBook.SelectedValue) == true)
        {
            Button1.Enabled = false;
            lblError.ForeColor = System.Drawing.Color.Green;
            lblError.Text = "Inserted Successfully";
            PrintMoneyreceipt();
        }
        else
        {
            lblError.ForeColor = System.Drawing.Color.Red;
            lblError.Text = "Error in Inserted Data";
        } 
    }


    public void PrintMoneyreceipt()
    {
        Panel1.Visible = true;
        Report_Header();
        if (DropDownList1.SelectedIndex != 0)
        {
            GetHearder_Detail1();
        }
        else
        {
            GetHearder_Detail();
        }
        ltrReport.Text = rpt.ToString();
    }


    public void Report_Header()
    {
        rpt.Append("<br/>"); rpt.Append("<br/>"); rpt.Append("<br/>");
        rpt.Append("<table width='100%' cellspacing=0 border=0 style='border:1px solid black;background-color:#9B9C8D;border-spacing: 0; padding: 0;font-family:verdana;'>");
        rpt.Append("<tr cellpadding:'0'>");
        rpt.AppendFormat("<td rowspan='4' style='width: 35%; font-family:Verdana; font-size:x-Medium; font-weight:bold;text-align:left'><img src='/Images/logo.jpg'  style='height:87px;width:205px;border-style:inherit;border-color:#D3CDD4'/></td>");
        rpt.AppendFormat("<td style='width: 35%; font-family:Verdana; font-size:large; font-weight:bold;text-align:center'>{0}</td>", "GFC Hospital");
        rpt.AppendFormat("<td style='width: 35%; font-family:Verdana; font-size:x-Medium; font-weight:bold;text-align:center'>&nbsp;</td>");
        rpt.Append("</tr>");
        rpt.Append("<tr>");
        rpt.AppendFormat("<td style='width: 35%; font-family:Verdana; font-size:x-Medium; font-weight:bold;text-align:center'>{0}</td>", "REG NO : NH-315/G-70/2013");
        rpt.AppendFormat("<td style='width: 35%; font-family:Verdana; font-size:x-Medium; font-weight:bold;text-align:center'>&nbsp;</td>");
        rpt.Append("</tr>");
        rpt.Append("<tr>");
        rpt.AppendFormat("<td style='width: 35%; font-family:Verdana; font-size:x-Medium; font-weight:bold;text-align:center'>{0}</td>", "KUSHPATA,GHATAL,MIDNAPUR(W),WB,721212");
        rpt.AppendFormat("<td style='width: 35%; font-family:Verdana; font-size:x-Medium; font-weight:bold;text-align:center'>&nbsp;</td>");
        rpt.Append("</tr>");
        rpt.Append("<tr>");
        rpt.AppendFormat("<td style='width: 35%; font-family:Verdana; font-size:x-Medium; font-weight:bold;text-align:center'>{0}</td>", "Ph :(03225)244400/244643  M:9434419825");
        rpt.AppendFormat("<td style='width: 35%; font-family:Verdana; font-size:x-Medium; font-weight:bold;text-align:center'>&nbsp;</td>");
        rpt.Append("</tr>");
        rpt.Append("</table>");
    }

    public void GetHearder_Detail()
    {
        ltrReport.Text = "";
        DataTable dt = theduePaymentClass.Get_IPD_PatientDetails(txtreg.Text);
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
            rpt.AppendFormat("<td colspan='6' style='width: 8%; font-family:Arial;font-weight:bold; font-size:larger; text-align:left'> Receipt No :{0} </td>", HiddenField1.Value);
            rpt.Append("</tr'>");



            rpt.Append("<tr style='height:40px'>");
            rpt.AppendFormat("<td colspan='6' style='width: 8%; font-family:Arial;font-weight:bold; font-size:larger; text-align:left'> Date : {0}</td>", DateTime.Now.ToString("dd/MM/yyyy"));
            rpt.Append("</tr'>");

            rpt.Append("</table>");


            rpt.Append("<table width='100%' cellspacing=0 border=0 style=' border-spacing: 0; padding: 0;font-family:verdana;'>");
            rpt.Append("<tr style='height:35px'>");
            rpt.AppendFormat("<td style='font-family:Arial;font-size:small; text-align:justify'>Received from <b>{0} Rs. {1}</b> only for <b>{2}</b></td>", dt.Rows[0]["patient_name"], txtamt.Text, txtreason.Text);
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
        DataTable dt = theduePaymentClass.Get_IPD_PatientDetails(txtreg.Text);
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
            rpt.AppendFormat("<td colspan='6' style='width: 8%; font-family:Arial;font-weight:bold; font-size:larger; text-align:left'> Receipt No :{0} </td>", HiddenField1.Value);
            rpt.Append("</tr'>");



            rpt.Append("<tr style='height:40px'>");
            rpt.AppendFormat("<td colspan='6' style='width: 8%; font-family:Arial;font-weight:bold; font-size:larger; text-align:left'> Date : {0}</td>", DateTime.Now.ToString("dd/MM/yyyy"));
            rpt.Append("</tr'>");

            rpt.Append("</table>");


            rpt.Append("<table width='100%' cellspacing=0 border=0 style=' border-spacing: 0; padding: 0;font-family:verdana;'>");
            rpt.Append("<tr style='height:35px'>");
            rpt.AppendFormat("<td style='font-family:Arial;font-size:small; text-align:justify'>Received from <b>{0} Rs. {1}</b> only for <b>{2}</b></td>", dt.Rows[0]["patient_name"], txtamt.Text, txtreason.Text);
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
        txtreg.Text = "";
        txtname.Text = "";
        txtamt.Text = "";


    }
    protected void Button2_Click(object sender, EventArgs e)
    {
        ResetAllFields();
    }
}