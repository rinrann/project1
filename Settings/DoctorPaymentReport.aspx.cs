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
using System.IO; 

public partial class Assignment_DoctorPaymentReport : System.Web.UI.Page
{
    Main Objmain = new Main(ConfigurationSettings.AppSettings["ConnectionString"].ToString());
    DoctorPaymentClass thoperationObject = new DoctorPaymentClass(ConfigurationSettings.AppSettings["ConnectionString"].ToString());
    DoctorPaymentDashBoard theHelper = new DoctorPaymentDashBoard(ConfigurationSettings.AppSettings["ConnectionString"].ToString());
    System.Text.StringBuilder rpt = new System.Text.StringBuilder();

    protected void Page_Load(object sender, EventArgs e)
    {
        Page.Title = "Doctor Payment Report";
        if (Session["userName"] == null)
        {
            Response.Redirect("../LoginPage.aspx");
        }
        if (Objmain.checkaccss(Session["CoCode"].ToString(), Session["UserRoleID"].ToString().Trim(), "DOCTOR PAYMENT REPORT", checkAccessType.ViewAction) == false)
        {
            Response.Redirect("../AccessDenied.aspx");
        }
        if (!IsPostBack)
        {
            DropDownList1.Items.Insert(0, new ListItem("--Select--", "0"));
            DropDownList1.Items.Insert(1, new ListItem("Doctor", "D"));
            DropDownList1.Items.Insert(2, new ListItem("Quack ", "Q"));
            // DropDownList1.SelectedValue = "D";
            ddlType.Items.Insert(0, new ListItem("--Select--", "0"));
            ddlType.Items.Insert(0, new ListItem("--Select--", "0"));
            ddlDoctorName.Items.Insert(0, new ListItem("--Select--", "0"));
            // DropDownFill();

        }
    }

    protected void DropDownList1_SelectedIndexChanged(object sender, EventArgs e)
    {
        string reftype = DropDownList1.SelectedValue.ToString();
        string lbltxt = lbltype.Text;

        if (reftype == "D")
        {
            ddlType.Items.Clear();
            ddlType.DataSource = thoperationObject.DoctorType(Session["CoCode"].ToString().Trim());
            ddlType.DataTextField = "TypeName";
            ddlType.DataValueField = "DocTypeId";
            ddlType.DataBind();
            ddlType.Items.Insert(0, new ListItem("--Select--", "0"));
            lbltype.Text = "Doctor Type";

            ddlDoctorName.Items.Clear();
            ddlDoctorName.Items.Insert(0, new ListItem("--Select--", "0"));
        }
        else if (reftype == "Q")
        {
            ddlType.Items.Clear();
            ddlType.Items.Insert(0, new ListItem("--Select--", ""));
            ddlType.Items.Insert(1, new ListItem("Asha", "A"));
            ddlType.Items.Insert(2, new ListItem("Car Rented", "R"));
            ddlType.Items.Insert(3, new ListItem("Car Private", "P"));
            ddlType.Items.Insert(4, new ListItem("Ambulance", "B"));
            ddlType.Items.Insert(5, new ListItem("Rural Doctor", "Q"));
            ddlType.Items.Insert(6, new ListItem("Others", "O"));
            lbltype.Text = "Quack Type";

            ddlDoctorName.Items.Clear();
            ddlDoctorName.Items.Insert(0, new ListItem("--Select--", "0"));
        }
        else
        {
            ddlType.Items.Clear();
            ddlType.Items.Insert(0, new ListItem("--Select--", "0"));
            lbltype.Text = "Type";

            ddlDoctorName.Items.Clear();
            ddlDoctorName.Items.Insert(0, new ListItem("--Select--", "0"));
        }

        GridFill();

    }

    private void GridFill()
    {
        string reftype = DropDownList1.SelectedValue.ToString();
        string doctype = ddlType.SelectedValue.ToString();
        string docid = ddlDoctorName.SelectedValue.ToString();
        string date1, date2;
        System.Globalization.DateTimeFormatInfo dtf = new DateTimeFormatInfo();
        if (txtFromDate.Text != "")
        {
            DateTime testdate = DateTime.ParseExact(txtFromDate.Text, "dd/MM/yyyy", dtf);
            date1 = testdate.ToString();
        }
        else
        {
            date1 = "null";
        }

        if (txtTodate.Text != "")
        {
            DateTime testdate = DateTime.ParseExact(txtTodate.Text, "dd/MM/yyyy", dtf);
            date2 = testdate.ToString();
        }
        else
        {
            date2 = "null";
        }
        GridView1.DataSource = thoperationObject.GetPayeeName(Session["CoCode"].ToString().Trim(),Session["YearCode"].ToString().Trim(),reftype, doctype, docid, date1, date2);
        GridView1.DataBind();

        //GridView2.Visible = false;
        //GridView3.Visible = false;
        //docdiv.Visible = false;
        //quackdiv.Visible = false;
        //butPay.Visible = false;
        //butSelect.Visible = false;
        //butDeselect.Visible = false;
    }



    protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        System.Globalization.DateTimeFormatInfo dtf = new DateTimeFormatInfo();
        int i;
        if (e.CommandName == "report")
        {
            i = Convert.ToInt32(e.CommandArgument);
            Label lblrecptno = (Label)GridView1.Rows[i].FindControl("lblrecptno");
            Label lblDate = (Label)GridView1.Rows[i].FindControl("lblDate");
            Label lbldocid = (Label)GridView1.Rows[i].FindControl("lbldocid");
            Label lblname = (Label)GridView1.Rows[i].FindControl("lblname");
            receiptno.Value = lblrecptno.Text.Trim();
            receiptdt.Value = lblDate.Text.Trim();
            docid.Value = lbldocid.Text.Trim();
            docname.Value = lblname.Text.Trim();
            PrintPaySlip();
        }
    }

    protected void GridView1_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        GridView1.PageIndex = e.NewPageIndex;
        GridFill();
    }


    protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        System.Globalization.DateTimeFormatInfo dtf = new DateTimeFormatInfo();
        //string date1, date2;

        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            Label lblSelno = (Label)e.Row.FindControl("lblSlno");
            lblSelno.Text = (e.Row.RowIndex + 1).ToString();
            Label lblrecptno = (Label)e.Row.FindControl("lblrecptno");

            string doctype = ddlType.SelectedValue.ToString();
            DataTable Docpayment = thoperationObject.GetDiscPayment(Session["CoCode"].ToString().Trim(), Session["YearCode"].ToString().Trim(), lblrecptno.Text);
            Label lblDisc = (Label)e.Row.FindControl("lblDisc");
            Label lblpay = (Label)e.Row.FindControl("lblpay");
            if (Docpayment.Rows.Count > 0)
            {

            lblDisc.Text = Convert.ToDecimal(Docpayment.Rows[0]["Credit"]).ToString();
            }
            else
            {
                lblDisc.Text = "0.00";
            }
            if (Convert.ToDecimal(lblpay.Text.Trim()) == 0 && Convert.ToDecimal(lblDisc.Text.Trim()) == 0)
            {
                e.Row.Visible = false;
            }
        }
    }

    //public void DropDownFill()
    //{

    //    this.ddlDoctorType.Items.Clear();
    //    this.ddlDoctorType.DataSource = thoperationObject.DoctorType();
    //    this.ddlDoctorType.DataTextField = "TypeName";
    //    this.ddlDoctorType.DataValueField = "DocTypeId";
    //    this.ddlDoctorType.DataBind();
    //    this.ddlDoctorType.Items.Insert(0, new ListItem("--Select--", "0"));

    //    this.ddlDoctorName.Items.Insert(0, new ListItem("--Select--", "0"));

    //}

    public void Report_Header()
    {
        rpt.Append("<table width='100%' cellspacing=0 border=0 style='border:1px solid black;border-spacing: 0; padding: 0;font-family:verdana;'>");
        rpt.Append("<tr cellpadding:'0'  style='height:30px;'>");
        rpt.Append("<td rowspan='3' width='2%' style='font-family:Verdana; font-size:x-large; font-weight:bold;padding-left:100px;text-align:left'><img src='../Images/gfclogo.png'  style='height:77px;width:75px;'/></td>");
        rpt.Append("<td   width='20%'  style='height:20px;font-family:Verdana; font-size:x-large; font-weight:bold;text-align:center'><u>GFC HOSPITAL</u></td>");
        rpt.Append("<td rowspan='3'  width='2%' style='height:20px;font-family:Verdana; font-size:x-large;padding-right:100px; font-weight:bold;text-align:right'><img src='../Images/gfclogo.png'  style='height:77px;width:75px;'/></td>");
        rpt.Append("</tr>");

        rpt.Append("<tr  style='height:20px;'>");
        rpt.AppendFormat("<td   width='20%'  style='height:10px;font-family:Verdana; font-size:small; font-weight:bold;text-align:center'>(Regn. No : NH-315/G-70/2013)</td>");
        rpt.Append("</tr>");

        rpt.Append("<tr  style='height:10px;'>");
        rpt.AppendFormat("<td  width='70%'  style='height:15px;font-family:Verdana; font-size:medium; font-weight:bold;text-align:center'>Kushpata, Ghatal, Paschim Medinipur, WB, 721212</td>");

        rpt.Append("</tr>");
        rpt.Append("</table>");
    }
    protected void ddlType_SelectedIndexChanged(object sender, EventArgs e)
    {
        string reftype = DropDownList1.SelectedValue.ToString();
        string doctype = ddlType.SelectedValue.ToString();

        this.ddlDoctorName.Items.Clear();
        this.ddlDoctorName.DataSource = thoperationObject.GetDocName(Session["CoCode"].ToString().Trim(), reftype, doctype);

        this.ddlDoctorName.DataTextField = "Name";
        this.ddlDoctorName.DataValueField = "Id";
        this.ddlDoctorName.DataBind();
        this.ddlDoctorName.Items.Insert(0, new ListItem("--Select--", "0"));
        GridFill();
    }
    protected void btnSearch_Click(object sender, EventArgs e)
    {
        if (DropDownList1.SelectedIndex == 1)
        {
            Report_Header();
            // GetHearder_SurgeonCharge();
        }

        else if (DropDownList1.SelectedIndex == 2)
        {
            Report_Header();
            // GetHearder_DoctorVisit();
        }
        else if (DropDownList1.SelectedIndex == 3)
        {
            Report_Header();
            // GetHearder_AnesthesisCharge();
        }
        else if (DropDownList1.SelectedIndex == 4)
        {
            Report_Header();
            // GetHearder_ReferConsultantdoctor();
        }
        else if (DropDownList1.SelectedIndex == 5)
        {
            Report_Header();
            // GetHearder_Quackdoctor();
        }
        else
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "msg", "alert('Please Select Payment Type !');", true);
        }



    }

    protected void btnFetch_Click(object sender, EventArgs e)
    {
        /*string docId = ddlDoctorName.SelectedValue.ToString();
        string date1, date2;
        System.Globalization.DateTimeFormatInfo dtf = new DateTimeFormatInfo();
        if (txtFromDate.Text != "")
        {
            DateTime testdate = DateTime.ParseExact(txtFromDate.Text, "dd/MM/yyyy", dtf);
            date1 = testdate.ToString();
        }
        else
        {
            date1 = "null";
        }

        if (txtTodate.Text != "")
        {
            DateTime testdate = DateTime.ParseExact(txtTodate.Text, "dd/MM/yyyy", dtf);
            date2 = testdate.ToString();
        }
        else
        {
            date2 = "null";
        }

        this.ddlrecptno.Items.Clear();
        this.ddlrecptno.DataSource = thoperationObject.GetrecptNo(docId,date1,date2);
        this.ddlrecptno.DataTextField = "ReceiptNo";
        this.ddlrecptno.DataValueField = "ReceiptNo";
        this.ddlrecptno.DataBind();
        this.ddlrecptno.Items.Insert(0, new ListItem("--Select--", "0"));*/
        GridFill();
    }
    public void PrintPaySlip()
    {
        Panel1.Visible = true;
        Report_Header2();
        ltrReport.Text = "";
        DataTable dt;
        if (DropDownList1.SelectedValue == "D")
        {
            Report_Doctor();
        }
        else
        {

            Report_Quack();
        }

        ltrReport.Text = rpt.ToString();
    }
    public void Report_Header2()
    {
        rpt.Append("<table width='100%' cellspacing=0 border=0 style='border:1px solid black;border-spacing: 0; padding: 0;font-family:Arial;'>");
        rpt.Append("<tr cellpadding:'0'  style='height:30px;'>");
        rpt.Append("<td rowspan='3' width='2%' style='font-family:Arial; font-size:x-large; font-weight:bold;padding-left:100px;text-align:left'><img src='../Images/gfclogo.png'  style='height:77px;width:75px;'/></td>");
        rpt.Append("<td   width='20%'  style='height:20px;font-family:Arial; font-size:x-large; font-weight:bold;text-align:center'><u>GFC HOSPITAL</u></td>");
        rpt.Append("<td rowspan='3'  width='2%' style='height:20px;font-family:Arial; font-size:x-large;padding-right:100px; font-weight:bold;text-align:right'><img src='../Images/gfclogo.png'  style='height:77px;width:75px;'/></td>");
        rpt.Append("</tr>");

        rpt.Append("<tr  style='height:20px;'>");
        rpt.AppendFormat("<td   width='20%'  style='height:10px;font-family:Arial; font-size:small; font-weight:bold;text-align:center'>(Regn. No : NH-315/G-70/2013)</td>");
        rpt.Append("</tr>");

        rpt.Append("<tr  style='height:10px;'>");
        rpt.AppendFormat("<td  width='70%'  style='height:15px;font-family:Arial; font-size:medium; font-weight:bold;text-align:center'>Kushpata, Ghatal, Paschim Medinipur, WB, 721212</td>");

        rpt.Append("</tr>");
        rpt.Append("</table>");
    }
    public void Report_Doctor()
    {
        //DataTable dt = theHelper.GetDocdetl(txtdocid.Text.Trim());
        DataTable dt = theHelper.GetDoctorPayslip(Session["CoCode"].ToString().Trim(), Session["YearCode"].ToString().Trim(), docid.Value.Trim(), receiptno.Value.Trim());
        int i;
        if (dt.Rows.Count > 0)
        {
            rpt.Append("<br/>");
            rpt.Append("<table width='100%' cellspacing=0 border=0 style=' border-spacing: 0; padding: 0;font-family:verdana;'>");
            rpt.Append("<tr>");
            rpt.Append("<td style='width: 15%;font-family:Arial;font-weight:bold; font-size:small; text-align:left'><b>Doctor Name</b></td>");
            rpt.AppendFormat("<td style='font-family:Arial;font-weight:bold; font-size:small; text-align:left'><b>{0}</td>", docname.Value);
            rpt.Append("</tr><tr>");
            rpt.Append("<td style='font-family:Arial;font-weight:bold; font-size:small; text-align:left'><b>Receipt No</b></td>");
            rpt.AppendFormat("<td style='font-family:Arial;font-weight:bold; font-size:small; text-align:left'><b>{0}</td>", receiptno.Value.Trim());
            rpt.Append("</tr><tr>");
            rpt.Append("<td style='font-family:Arial;font-weight:bold; font-size:small; text-align:left'><b>Payment Date</b></td>");
            rpt.AppendFormat("<td style='font-family:Arial;font-weight:bold; font-size:small; text-align:left'><b>{0}</td>", receiptdt.Value.Trim());
            rpt.Append("</tr>");
            rpt.Append("</table>");
            rpt.Append("<br/>");
            rpt.Append("<table width='100%' cellspacing=0 border=0 style=' border-spacing: 0; padding: 0;font-family:verdana;'>");

            rpt.Append("<tr style='background-color:gray;'>");
            rpt.Append("<td rowspan='2' style='width: 15%; font-family:Arial;font-weight:bold;border-top:1px;border-bottom:1px;border-right:1px;border-left:1px; font-size:small; text-align:center'>Patient Name</td>");
            rpt.Append("<td rowspan='2' style='width: 8%; font-family:Arial;font-weight:bold;border-bottom:1px;border-right:1px; font-size:small; text-align:center'>Admission Date</td>");
            rpt.Append("<td rowspan='2' style='width: 8%; font-family:Arial;font-weight:bold;border-bottom:1px;border-right:1px; font-size:small; text-align:center'>Discharge Date</td>");
            rpt.Append("<td rowspan='2' style='width: 8%; font-family:Arial;font-weight:bold;border-bottom:1px;border-right:1px; font-size:small; text-align:center'>Diagnosis</td>");
            rpt.Append("<td rowspan='2' style='width: 8%; font-family:Arial;font-weight:bold;border-bottom:1px;border-right:1px; font-size:small; text-align:center'>OT Name</td>");
            rpt.Append("<td colspan='3' style='width: 8%; font-family:Arial;font-weight:bold;border-bottom:1px;border-right:1px; font-size:small; text-align:center'>Pays</td>");
            rpt.Append("</tr>"); rpt.Append("<tr style='background-color:gray;'>");
            rpt.Append("<td style='width: 5%; font-family:Arial;font-weight:bold;border-bottom:1px;border-right:1px; font-size:small; text-align:center'>Fees</td>");
            rpt.Append("<td style='width: 5%; font-family:Arial;font-weight:bold;border-bottom:1px;border-right:1px; font-size:small; text-align:center'>Surgary</td>");
            rpt.Append("<td style='width: 5%; font-family:Arial;font-weight:bold;border-bottom:1px;border-right:1px; font-size:small; text-align:center'>Commissions</td>");
            rpt.Append("</tr>");
            for (i = 0; i < dt.Rows.Count; i++)
            {
                if (dt.Rows[i]["bold"].ToString().Trim() == "0")
                {
                    rpt.Append("<tr style='height:40px'>");
                    rpt.AppendFormat("<td style='width: 15%; font-family:Arial; font-size:x-small;border-bottom:1px;border-left:1px;border-right:1px; text-align:left'>{0}</td>", dt.Rows[i]["patientName"].ToString());
                    rpt.AppendFormat("<td style='width: 8%; font-family:Arial; font-size:x-small;border-bottom:1px;border-right:1px; text-align:center'>{0}</td>", dt.Rows[i]["admissionDate"].ToString());
                    rpt.AppendFormat("<td style='width: 8%; font-family:Arial; font-size:x-small;border-bottom:1px;border-right:1px; text-align:center'>{0}</td>", dt.Rows[i]["DischargeDt"].ToString());
                    rpt.AppendFormat("<td style='width: 8%; font-family:Arial; font-size:x-small;border-bottom:1px;border-right:1px; text-align:left'>{0}</td>", dt.Rows[i]["Diagnosis"].ToString());
                    rpt.AppendFormat("<td style='width: 8%; font-family:Arial; font-size:x-small;border-bottom:1px;border-right:1px; text-align:left'>{0}</td>", dt.Rows[i]["OtName"].ToString());
                    rpt.AppendFormat("<td style='width: 5%; font-family:Arial; font-size:x-small;border-bottom:1px;border-right:1px; text-align:right'>{0}</td>", dt.Rows[i]["VisitCharge"].ToString());
                    rpt.AppendFormat("<td style='width: 5%; font-family:Arial; font-size:x-small;border-bottom:1px;border-right:1px; text-align:right'>{0}</td>", dt.Rows[i]["surgoncharge"].ToString());
                    rpt.AppendFormat("<td style='width: 5%; font-family:Arial; font-size:x-small;border-bottom:1px;border-right:1px; text-align:right'>{0}</td>", dt.Rows[i]["ReferCharge"].ToString());
                    rpt.Append("</tr>");
                }
                else
                {
                    rpt.Append("<tr style='height:40px'>");
                    rpt.AppendFormat("<td style='font-family:Arial;font-weight:bold; font-size:small; text-align:left'><b>{0}:</b></td>", dt.Rows[i]["patientName"].ToString());
                    rpt.AppendFormat("<td style='font-family:Arial;font-weight:bold; font-size:small; text-align:right'><b>{0}</td>", dt.Rows[i]["TotalBill"].ToString());
                    rpt.Append("<td style='font-family:Arial;font-weight:bold; font-size:larger; text-align:center'></td>");
                    rpt.Append("<td style='font-family:Arial;font-weight:bold; font-size:larger; text-align:center'></td>");
                    rpt.Append("<td style='font-family:Arial;font-weight:bold; font-size:larger; text-align:center'></td>");
                    rpt.Append("<td style='font-family:Arial;font-weight:bold; font-size:larger; text-align:center'></td>");
                    rpt.Append("<td style='font-family:Arial;font-weight:bold; font-size:larger; text-align:center'></td>");
                    rpt.Append("<td style='font-family:Arial;font-weight:bold; font-size:larger; text-align:center'></td>");
                    rpt.Append("</tr>");
                }
            }
            rpt.Append("</table>");
        }
    }
    public void Report_Quack()
    {
        //DataTable dt = theHelper.GetQuackdetl(txtdocid.Text.Trim());
        DataTable dt = theHelper.GetDoctorPayslip(Session["CoCode"].ToString().Trim(), Session["YearCode"].ToString().Trim(), docid.Value.Trim(), receiptno.Value.Trim());
        int i;
        if (dt.Rows.Count > 0)
        {
            rpt.Append("<br/>");
            rpt.Append("<table width='100%' cellspacing=0 border=0 style=' border-spacing: 0; padding: 0;font-family:verdana;'>");
            rpt.Append("<tr>");
            rpt.Append("<td style='width: 15%;font-family:Arial;font-weight:bold; font-size:small; text-align:left'><b>Name</b></td>");
            rpt.AppendFormat("<td style='font-family:Arial;font-weight:bold; font-size:small; text-align:left'><b>{0}</td>", docname.Value);
            rpt.Append("</tr><tr>");
            rpt.Append("<td style='font-family:Arial;font-weight:bold; font-size:small; text-align:left'><b>Receipt No</b></td>");
            rpt.AppendFormat("<td style='font-family:Arial;font-weight:bold; font-size:small; text-align:left'><b>{0}</td>", receiptno.Value.Trim());
            rpt.Append("</tr><tr>");
            rpt.Append("<td style='font-family:Arial;font-weight:bold; font-size:small; text-align:left'><b>Payment Date</b></td>");
            rpt.AppendFormat("<td style='font-family:Arial;font-weight:bold; font-size:small; text-align:left'><b>{0}</td>", receiptdt.Value.Trim());
            rpt.Append("</tr>");
            rpt.Append("</table>");
            rpt.Append("<br/>");
            rpt.Append("<table width='100%' cellspacing=0 border=0 style=' border-spacing: 0; padding: 0;font-family:verdana;'>");

            rpt.Append("<tr style='height:40px;background-color:gray;'>");
            rpt.Append("<td style='width: 15%; font-family:Arial;font-weight:bold;border-top:1px;border-bottom:1px;border-right:1px;border-left:1px; font-size:small; text-align:left'>Patient Name</td>");
            rpt.Append("<td style='width: 8%; font-family:Arial;font-weight:bold;border-top:1px;border-bottom:1px;border-right:1px;font-size:small; text-align:center'>Admission Date</td>");
            rpt.Append("<td style='width: 8%; font-family:Arial;font-weight:bold;border-top:1px;border-bottom:1px;border-right:1px;font-size:small; text-align:center'>Discharge Date</td>");
            rpt.Append("<td style='width: 8%; font-family:Arial;font-weight:bold;border-top:1px;border-bottom:1px;border-right:1px;font-size:small; text-align:right'>Commission</td>");
            rpt.Append("</tr>");
            for (i = 0; i < dt.Rows.Count; i++)
            {
                if (dt.Rows[i]["bold"].ToString().Trim() == "0")
                {
                    rpt.Append("<tr style='height:40px'>");
                    rpt.AppendFormat("<td style='width: 15%; font-family:Arial;border-bottom:1px;border-right:1px;border-left:1px; font-size:x-small; text-align:left'>{0}</td>", dt.Rows[i]["patientName"].ToString());
                    rpt.AppendFormat("<td style='width: 8%; font-family:Arial;border-bottom:1px;border-right:1px; font-size:x-small; text-align:center'>{0}</td>", dt.Rows[i]["admissionDate"].ToString());
                    rpt.AppendFormat("<td style='width: 8%; font-family:Arial;border-bottom:1px;border-right:1px; font-size:x-small; text-align:center'>{0}</td>", dt.Rows[i]["DischargeDt"].ToString());
                    rpt.AppendFormat("<td style='width: 8%; font-family:Arial;border-bottom:1px;border-right:1px; font-size:x-small; text-align:right'>{0}</td>", dt.Rows[i]["ReferCharge"].ToString());
                    rpt.Append("</tr>");
                }
                else
                {
                    rpt.Append("<tr style='height:40px'>");
                    rpt.AppendFormat("<td style='font-family:Arial;font-weight:bold; font-size:small; text-align:left'><b>{0}</b></td>", dt.Rows[i]["patientName"].ToString());
                    rpt.AppendFormat("<td style='font-family:Arial;font-weight:bold; font-size:small; text-align:right'><b>{0}</td>", dt.Rows[i]["TotalBill"].ToString());
                    rpt.Append("<td style='font-family:Arial;font-weight:bold; font-size:small; text-align:center'></td>");
                    rpt.Append("<td style='font-family:Arial;font-weight:bold; font-size:small; text-align:center'></td>");
                    rpt.Append("</tr>");
                }
            }
            rpt.Append("</table>");
        }
    }
    protected void btnGenerate_Click(object sender, EventArgs e)
    {

    }
}