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
using System.Net.Mail;
using System.Net;
using System.IO;
using System.Text;
using System.Collections.Generic;

public partial class Pathology_SampleRequisitionBill : System.Web.UI.Page
{
    Main Objmain = new Main(ConfigurationSettings.AppSettings["ConnectionString"].ToString());
    PH_PatientRequisition thereq = new PH_PatientRequisition(ConfigurationSettings.AppSettings["ConnectionString"].ToString());
    PH_TestPopupMultiple thedia = new PH_TestPopupMultiple(ConfigurationSettings.AppSettings["ConnectionString"].ToString());
    System.Text.StringBuilder rpt = new System.Text.StringBuilder();
    String Date = DateTime.Now.ToString("dd/MM/yyyy");

    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["userName"] == null)
        {
            Response.Redirect("../LoginPage.aspx");
        }
        if (Objmain.checkaccss(Session["CoCode"].ToString(), Session["UserRoleID"].ToString().Trim(), "SAMPLE REQUISITION BILL ENTRY", checkAccessType.ViewAction) == false)
        {
            Response.Redirect("../AccessDenied.aspx");
        }
        if (Objmain.checkaccss(Session["CoCode"].ToString(), Session["UserRoleID"].ToString().Trim(), "SAMPLE REQUISITION BILL ENTRY", checkAccessType.InsertAction) == false)
        {
            //Button1.Enabled = false;
        }

        ScriptManager.RegisterStartupScript(this, this.GetType(), "msg", "GetDatetime();", true);
        Page.Title = "Sample Requisition Bill Entry";
        string previousPageName = "";
        if (!IsPostBack)
        {
            Tab1Func();
            GridFill();
            if (Request.UrlReferrer != null)
            {
                string previousPageUrl = Request.UrlReferrer.AbsoluteUri;
                previousPageName = System.IO.Path.GetFileName(Request.UrlReferrer.AbsolutePath);
            }
        }
    }

    public void Tab1Func()
    {
        MainView.ActiveViewIndex = 0;

    }

    public void GridFill()
    {
        DataTable dt = thereq.GridFillDue(Session["CoCode"].ToString().Trim(), Session["YearCode"].ToString().Trim(), ddltype.SelectedValue,"");
        GridView1.DataSource = dt;
        GridView1.DataBind();
    }

    protected void GridView1_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        GridView1.PageIndex = e.NewPageIndex;
        GridFill();
    }
    protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "PayNow")
        {
            GridViewRow rowSelect = (GridViewRow)(((LinkButton)e.CommandSource).NamingContainer);
            int index = rowSelect.RowIndex;
            //int index = Convert.ToInt32(row.RowIndex);

            string reqno = (e.CommandArgument).ToString();
            Label lblBillAmt = (Label)GridView1.Rows[index].FindControl("lblBillAmt");
            Label lblPaidAmt = (Label)GridView1.Rows[index].FindControl("lblPaidAmt");
            Label lblDueAmt = (Label)GridView1.Rows[index].FindControl("lblDueAmt");
            Label lblname = (Label)GridView1.Rows[index].FindControl("lblname");
            Label lblregno = (Label)GridView1.Rows[index].FindControl("lblregno");
            DataTable dt = new DataTable();
            if (ddltype.SelectedValue == "1")
                dt = thedia.GetExistTestDetails(Session["CoCode"].ToString().Trim(), Session["YearCode"].ToString().Trim(), reqno, "");
            else dt = thereq.GridtestDtls(Session["CoCode"].ToString().Trim(), Session["YearCode"].ToString().Trim(), reqno);

            GridView2.DataSource = dt;
            GridView2.DataBind();
            lblPRegno.Text = lblregno.Text;
            lblPatientreqno.Text = reqno;
            lblPname.Text = lblname.Text;
            lblTotBillAmt.Text = lblBillAmt.Text;
            lblPaidBillAmt.Text = lblPaidAmt.Text;
            txtPayableAmt.Text = lblDueAmt.Text;
            lblDueBillAmt.Text = lblDueAmt.Text;
            MainView.ActiveViewIndex = 1;

        }
    }
    protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            Label lblreqno = (Label)e.Row.FindControl("lblreqno");
            Label lblBillAmt = (Label)e.Row.FindControl("lblBillAmt");
            Label lblPaidAmt = (Label)e.Row.FindControl("lblPaidAmt");
            Label lblDueAmt = (Label)e.Row.FindControl("lblDueAmt");
            Label lblvchno = (Label)e.Row.FindControl("lblvchno");
            string reqno = lblreqno.Text.Trim();
            decimal totAmt = 0;

            if (ddltype.SelectedValue == "1")
            {
                DataTable dt = thedia.GetExistTestDetails(Session["CoCode"].ToString().Trim(), Session["YearCode"].ToString().Trim(), reqno, "");
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    totAmt = totAmt + Convert.ToDecimal(dt.Rows[i]["Cost"]);
                }
                lblBillAmt.Text = totAmt.ToString();
            }
        }
    }
    protected void GridView2_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        DataTable databound = new DataTable();
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            Label lblSerial = (Label)e.Row.FindControl("lblSerial");
            lblSerial.Text = ((GridView1.PageIndex * GridView1.PageSize) + e.Row.RowIndex + 1).ToString();

            Label lblTestReqNo = (Label)e.Row.FindControl("lblTestReqNo");

        }
    }
    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        Decimal dueamt = Convert.ToDecimal(lblDueBillAmt.Text);
        Decimal payableAmt = Convert.ToDecimal(txtPayableAmt.Text);
        Decimal PaidAmt = Convert.ToDecimal(lblPaidBillAmt.Text);
        Decimal totbillAmt = Convert.ToDecimal(lblTotBillAmt.Text);
        Decimal curDueAmt = 0;
        Decimal totpaidAmt = 0;
        Decimal discount = 0;
        if (payableAmt <= 0)
        {
            lblError.Text = "Payable Amount cannot be equal or less than Zero !!";
            lblError.Visible = true;

        }
        else if (payableAmt > dueamt)
        {
            lblError.Text = "Payable Amount cannot be greater than Due Amount !!";
            lblError.Visible = true;
        }
        else
        {
            if (ddltype.SelectedValue == "2")
            {
                if (payableAmt < dueamt)
                {
                    lblError.Text = "Payable Amount cannot be less than Due Amount !!";
                    lblError.Visible = true;
                    return;
                }
            }
            totpaidAmt = PaidAmt + payableAmt;
            curDueAmt = totbillAmt - totpaidAmt;

            string chqdt = "";
            if (txtchqdt.Text != "")
            {
                string[] cdt = txtchqdt.Text.Split('/');
                chqdt = cdt[1] + cdt[0];
            }

            DataTable dt = thereq.GenerateReceiptNo(Session["CoCode"].ToString().Trim(), Session["YearCode"].ToString().Trim(),"SAM");
            string ReceptNo = dt.Rows[0][0].ToString();
            if (thereq.UpdateRequisitionBill(Session["CoCode"].ToString().Trim(), Session["YearCode"].ToString().Trim(), lblPRegno.Text.Trim(), lblPatientreqno.Text.Trim(), totpaidAmt.ToString(), curDueAmt.ToString(), ddlPaymentMode.SelectedValue.Trim(), txtBankName.Text, txtChequeNo.Text, chqdt, DateTime.Now.ToString("yyyy-MM-dd"), Session["userName"].ToString(), ddltype.SelectedValue, payableAmt.ToString(), ddlPayType.SelectedValue, "SAM", DateTime.Now.ToString("yyyy-MM-dd"), Session["userId"].ToString().Trim(),ReceptNo,"","",discount.ToString(), "I") == true)
            {
                lblError.Text = "Record Successfully Saved....";
                lblError.ForeColor = Color.Green;
                lblError.Visible = true;
                lblreceitpNo.Text = ReceptNo;

                DataTable testcostdetails = thedia.GetExistTestCostDetails(Session["CoCode"].ToString().Trim(), Session["YearCode"].ToString().Trim(), lblPatientreqno.Text,ddltype.SelectedValue);
                lblPaidBillAmt.Text = testcostdetails.Rows[0]["paid"].ToString();
                lblDueBillAmt.Text = testcostdetails.Rows[0]["due"].ToString();
                txtPayableAmt.Text = testcostdetails.Rows[0]["due"].ToString();
            }
            else
            {
                lblError.Text = "Error in Saving ";
                lblError.ForeColor = Color.Red;
                lblError.Visible = true;
            }
        }

    }
    protected void btnCancel_Click(object sender, EventArgs e)
    {
        Response.Redirect("../Pathology/SampleRequisitionBill.aspx");
    }

    protected void ddlPaymentMode_SelectedIndexChanged(object sender, EventArgs e)
    {

        if (ddlPaymentMode.SelectedValue == "R")
        {
            lblchqdt.InnerText = "Valid Year Month :";
            lblchqno.InnerText = "Card No :";
            lblbankNm.InnerText = "Card Holder Name :";
            divchqdt.Visible = true;
            divchqno.Visible = true;
            divBank.Visible = true;
        }
        else
        {
            divchqdt.Visible = false;
            divchqno.Visible = false;
            divBank.Visible = false;
        }
    }
    protected void btnrcpt_Click(object sender, EventArgs e)
    {
        string someScript = "";
        someScript = "<script language='javascript'> var el = document.getElementById('h1');el.style.display = 'none';</script>";
        Page.ClientScript.RegisterStartupScript(this.GetType(), "onload", someScript);

        GetReport();
        Button7.Visible = true;
        Button6.Visible = true;
    }
    public void GetReport()
    {
        Report_Header();
        GetHearder_Detail();
        ltrReport.Text = rpt.ToString();
    }

    public void Report_Header()
    {
        System.Globalization.DateTimeFormatInfo dtf = new DateTimeFormatInfo();
        string date = DateTime.Now.ToString("dd/MM/yyyy");

        rpt.Append("<table width='100%' cellspacing=0 border=0 >");
        rpt.Append("<tr cellpadding:'0'  style='height:30px;'>");
        rpt.Append("<td width='30%'><img src='../Images/logo.jpg'  style=''/></td>");
        rpt.Append("<td   width='40%'  style='height:20px;font-family:Arial; font-size:medium; text-align:center'><b><u>Money Receipt</u></b></td>");
        rpt.Append("<td width='30%' style='text-align:right'>Print Date : " + date + "</td>");
        rpt.Append("</tr>");
        rpt.Append("</table>");
    }
    public void GetHearder_Detail()
    {
        DataTable dt = thereq.GetRequisitionForReport(Session["CoCode"].ToString().Trim(), Session["YearCode"].ToString().Trim(), lblPatientreqno.Text); //ds.Tables[0];
        //DataTable SlipSession = (DataTable)Session["SlipSession"];
        DataTable SlipSession = thedia.GetExistSampleDetails(Session["CoCode"].ToString().Trim(), Session["YearCode"].ToString().Trim(), lblPatientreqno.Text, "");
        DataTable testcostdetails = thedia.GetExistTestCostDetails(Session["CoCode"].ToString().Trim(), Session["YearCode"].ToString().Trim(), lblPatientreqno.Text, "2");
        DataTable billDetails = thedia.GetBillDetails(Session["CoCode"].ToString().Trim(), Session["YearCode"].ToString().Trim(), lblreceitpNo.Text,"2");
        if (SlipSession != null)
        {

            rpt.Append("<table width='100%' cellspacing=0 border=0 style='border:1px solid black;border-spacing: 0; padding: 0;font-family:verdana;'>");

            rpt.Append("<tr style='height:20px'>");
            rpt.Append("<td colspan='6' style='width: 6%;border-bottom: 1px solid black; font-family:Verdana;font-size:small; text-align:Center;background-color:#9B9C8D'> PATIENT DETAILS  </td>");
            rpt.Append("</tr'>");


            rpt.Append("<tr style='height:30px'>");
            rpt.Append("<td style='width: 6% ;border-bottom: 1px solid black;border-right: 1px solid black; font-family:Verdana; text-align:left'>Requisition No :</td>");
            rpt.AppendFormat("<td style='width: 10%;border-bottom: 1px solid black; border-right: 1px solid black; font-family:Verdana; text-align:left'>{0}</td>", dt.Rows[0]["RequisitionNo"]);
            rpt.Append("<td style='width: 6%;border-bottom: 1px solid black; border-right: 1px solid black; font-family:Verdana;  text-align:left'>Collect Date</td>");
            rpt.AppendFormat("<td style='width: 10%;border-bottom: 1px solid black; border-right: 1px solid black; font-family:Verdana; text-align:left'>{0}</td>", /*dt.Rows[0]["TDate"]*/ SlipSession.Rows[0]["Date"]);
            rpt.Append("<td style='width: 6%;border-bottom: 1px solid black;border-right: 1px solid black; font-family:Verdana;  text-align:left'>Delivery Date</td>");
            rpt.AppendFormat("<td style='width: 10%;border-bottom: 1px solid black;font-family:Verdana; text-align:left'>{0}</td>", /*dt.Rows[0]["delDate"]*/ SlipSession.Rows[0]["DeliveryDate"]);
            rpt.Append("</tr>");

            rpt.Append("<tr style='height:30px'>");
            rpt.Append("<td style='width: 6%;border-bottom: 1px solid black;border-right: 1px solid black; font-family:Verdana;  text-align:left'>Reg. No :</td>");
            rpt.AppendFormat("<td style='width: 10%;border-bottom: 1px solid black;border-right: 1px solid black; font-family:Verdana; text-align:left'>{0}</td>", dt.Rows[0]["RegistrationNo"]);
            rpt.Append("<td style='width: 6%;border-bottom: 1px solid black;border-right: 1px solid black; font-family:Verdana;  text-align:left'>Name & Age : </td>");
            rpt.AppendFormat("<td style='width: 10%;border-bottom: 1px solid black;border-right: 1px solid black;font-family:Verdana; text-align:left'>{0}</td>", dt.Rows[0]["PatientName"].ToString() + ", " + dt.Rows[0]["age"].ToString());
            rpt.Append("<td style='width: 6%;border-bottom: 1px solid black;border-right: 1px solid black; font-family:Verdana;  text-align:left'>Contact No : </td>");
            rpt.AppendFormat("<td style='width: 10%;border-bottom: 1px solid black;font-family:Verdana; text-align:left'>{0}</td>", dt.Rows[0]["Ph1"]);
            rpt.Append("</tr>");

            rpt.Append("<tr style='height:30px'>");
            rpt.Append("<td style='width: 6%;border-right: 1px solid black; font-family:Verdana;  text-align:left'>Address :</td>");
            rpt.AppendFormat("<td colspan=3 style='width: 10%;font-family:Verdana; text-align:left;border-right: 1px solid black; '>{0}</td>", dt.Rows[0]["Address"]);
            rpt.Append("<td style='width: 6%;border-right: 1px solid black; font-family:Verdana;  text-align:left'>Refer By</td>");
            rpt.AppendFormat("<td  style='width: 10%;font-family:Verdana; text-align:left'>{0}</td>", dt.Rows[0]["ReferalName"]);




            rpt.Append("</tr>");
            rpt.Append("</table>");



            rpt.Append("<br/>");
            rpt.Append("<center>");
            rpt.Append("<table width='100%'  cellspacing=0 border=0 style='border:1px solid black;border-spacing: 0; padding: 0;font-family:verdana;'>");

            rpt.Append("<tr style='height:20px'>");
            rpt.Append("<td colspan='6' style='width: 6%;border-bottom: 1px solid black;border-top: 1px solid black; font-family:Verdana;font-size:small; text-align:Center;background-color:#9B9C8D'> SAMPLE DETAILS  </td>");
            rpt.Append("</tr'>");

            rpt.Append("<tr style='height:30px'>");
            rpt.Append("<td style='width:35%;border-bottom: 1px solid black;border-right: 1px solid black; font-family:Verdana;  text-align:left'>Code</td>"); 
            rpt.Append("<td style='width:35%;border-bottom: 1px solid black;border-right: 1px solid black; font-family:Verdana;  text-align:left'>Sample Name</td>"); 
            rpt.Append("<td style='width:10%;border-bottom: 1px solid black;font-family:Verdana; text-align:right'>Charge</td>");
            double total = 0.0;
            int a = 0, b = 0, c = 0, d = 0;
            rpt.Append("</tr >");

            for (int i = 0; i < SlipSession.Rows.Count; i++)
            {
                rpt.AppendFormat("<td style='border-bottom: 1px solid black;border-right: 1px solid black; font-family:Verdana;text-align:left'>{0}</td>", SlipSession.Rows[i]["samplecode"].ToString());
                rpt.AppendFormat("<td style='border-bottom: 1px solid black;border-right: 1px solid black; font-family:Verdana;text-align:left'>{0}</td>", SlipSession.Rows[i]["TestName"].ToString());
                rpt.AppendFormat("<td style='border-bottom: 1px solid black;font-family:Verdana; text-align:right'> {0}</td>", SlipSession.Rows[i]["PatientBillAmt"].ToString());
                rpt.Append("</tr >");
                total = total + Convert.ToDouble(SlipSession.Rows[i]["PatientBillAmt"]);
            }
            rpt.Append("</table>");

            rpt.Append("<table width='100%' cellpadding=0 cellspacing=0 >");
            rpt.Append("<tr style=''>");
            rpt.Append("<td   style='width:60%;'>Mode : By " + billDetails.Rows[0]["mode"].ToString() + "</td>");
            rpt.Append("<td  style='width:10%; font-family:Verdana; text-align:left'>Total Charges</td>");
            rpt.AppendFormat("<td style='width:20%; font-family:Verdana;text-align:right'>{0}</td>", total.ToString("F"));
            rpt.Append("</tr>");

            rpt.Append("<tr style=''>");
            rpt.Append("<td   style='width:60%;'>" + billDetails.Rows[0]["Bank_CardHolderName"].ToString() + " " + billDetails.Rows[0]["Chq_CardNo"].ToString() + " " + billDetails.Rows[0]["ChqDt_CardExpDt"].ToString() + "</td>");
            rpt.Append("<td  style=' font-family:Verdana; text-align:left'>Paid Amt</td>");
            rpt.AppendFormat("<td style='font-family:Verdana;text-align:right'>{0}</td>", billDetails.Rows[0]["paid"].ToString());
            rpt.Append("</tr>");

            rpt.Append("<tr style=''>");
            rpt.Append("<td>In Words : " + testcostdetails.Rows[0]["num2word"].ToString() + "</td>");
            rpt.Append("<td  style=' font-family:Verdana; text-align:left'>Due Amt</td>");
            rpt.AppendFormat("<td style=' font-family:Verdana;text-align:right'>{0}</td>", billDetails.Rows[0]["due"].ToString());
            rpt.Append("</tr>");




            rpt.Append("</table>");

            rpt.Append("<br/>"); rpt.Append("<br/>");
            rpt.Append("<table>");

            rpt.Append("<tr>");
            rpt.Append("<td style='width: 10%; font-family:Verdana;  text-align:left'></td>");
            rpt.Append("<td style='width: 10%; font-family:Verdana;  text-align:left'></td>");
            rpt.Append("<td style='width: 10%; font-family:Verdana;  text-align:left'></td>");
            rpt.Append("<td style='width: 10%; font-family:Verdana;  text-align:center'>________________________________</td>");
            rpt.Append("</tr'>");

            rpt.Append("<tr>");
            rpt.Append("<td style='width: 10%; font-family:Times New Roman;  text-align:left'></td>");
            rpt.Append("<td style='width: 10%; font-family:Verdana;  text-align:left'></td>");
            rpt.Append("<td style='width: 10%; font-family:Verdana;  text-align:left'></td>");
            rpt.Append("<td style='width: 10%; font-family:Verdana;  text-align:center'> Signature </td>");

            rpt.Append("</tr'>");
            rpt.Append("</table>");
            rpt.Append("</center>");
        }
        else
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "msg", "alert('No row to Display.. !');", true);
        }

        Session["SlipSession"] = null;
        ltrReport.Visible = true;

    }
    protected void Button6_Click(object sender, EventArgs e)
    {
        ltrReport.Visible = false;
        Button7.Visible = false;
        Button6.Visible = false;
    }
    protected void ddltype_SelectedIndexChanged(object sender, EventArgs e)
    {
        GridFill();
    }
}