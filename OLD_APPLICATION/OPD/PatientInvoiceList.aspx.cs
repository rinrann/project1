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

public partial class OPD_PatientInvoiceList : System.Web.UI.Page
{
    Main Objmain = new Main(ConfigurationSettings.AppSettings["ConnectionString"].ToString());
    PatientHistory theabortion = new PatientHistory(ConfigurationSettings.AppSettings["ConnectionString"].ToString());
    PH_TestPopupMultiple thedia = new PH_TestPopupMultiple(ConfigurationSettings.AppSettings["ConnectionString"].ToString());
    PH_PatientRequisition thereq = new PH_PatientRequisition(ConfigurationSettings.AppSettings["ConnectionString"].ToString());
    System.Text.StringBuilder rpt = new System.Text.StringBuilder();
    String Date = DateTime.Now.ToString("dd/MM/yyyy");

    protected void Page_Load(object sender, EventArgs e)
    {
        Page.Title = "Patient Queue";
        if (Session["userId"] == null)
        {
            Response.Redirect("../LoginPage.aspx");
        }
        if (Objmain.checkaccss(Session["CoCode"].ToString(), Session["UserRoleID"].ToString().Trim(), "PATIENT INVOICE LIST", checkAccessType.ViewAction) == false)
        {
            Response.Redirect("../AccessDenied.aspx");
        }

        if (!IsPostBack)
        {
            

        }
    }

    protected void Button1_Click(object sender, EventArgs e)
    {
        //Report_Header();
        //GetHearder_Detail();
        //ltrReport.Text = rpt.ToString();
        DataTable dt = theabortion.GetPatientInvoiceList(Session["CoCode"].ToString(), txtRegNo.Text.Trim(), txtPtName.Text.Trim(), txtphSrch.Text.Trim(), txtInvDate.Text.Trim());
        GridView1.DataSource = dt;
        GridView1.DataBind();
    }

    protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "Print")
        {
            int rowIndex = Convert.ToInt32(e.CommandArgument);
            GridViewRow row = GridView1.Rows[rowIndex];
            string reqno = (row.FindControl("lblreqno") as Label).Text.Trim();
            string receiptno = (row.FindControl("lblrecptno") as Label).Text.Trim();
            
            //BillReport_Header();
            BillGetHearder_DetailInvoice(reqno, receiptno,"I");
            ltrReport.Text = rpt.ToString();
            billdiv.Visible = true;
        }
        if (e.CommandName == "Receipt")
        {
            int rowIndex = Convert.ToInt32(e.CommandArgument);
            GridViewRow row = GridView1.Rows[rowIndex];
            string reqno = (row.FindControl("lblreqno") as Label).Text.Trim();
            string receiptno = (row.FindControl("lblrecptno") as Label).Text.Trim();

            //BillReport_Header();
            //BillGetHearder_Detail(reqno, receiptno,"M");
            BillGetHearder_DetailInvoice(reqno, receiptno, "F");
            ltrReport.Text = rpt.ToString();
            billdiv.Visible = true;
        }
    }

    //public void BillReport_Header()
    //{
    //    System.Globalization.DateTimeFormatInfo dtf = new DateTimeFormatInfo();
    //    string date = DateTime.Now.ToString("dd/MM/yyyy");

    //    if (RadioButtonList1.SelectedValue == "Y")
    //    {
    //        rpt.Append("<table width='100%' cellspacing=0 border=0 >");
    //        rpt.Append("<tr cellpadding:'0'  style='height:30px;'>");
    //        rpt.Append("<td width='30%'><img src='../Images/logo.jpg'  style=''/></td>");
    //        rpt.Append("<td   width='40%'  style='height:20px;font-family:Arial; font-size:medium; text-align:center'></td>");
    //        rpt.Append("<td width='30%' style='text-align:right'>Print Date : " + date + "</td>");
    //        rpt.Append("</tr>");
    //        rpt.Append("</table>");
    //    }
    //    else
    //    {
    //        rpt.Append("<table width='100%' cellspacing=0 border=0 >");
    //        rpt.Append("<tr>");
    //        rpt.Append("<td style='width:25%;font-family:Verdana;font-size:small; text-align:right;background-color:#9B9C8D'> Invoice Date :" + billDetails.Rows[0]["BillDate"].ToString() + " " + billDetails.Rows[0]["BillTime"].ToString() + " </td>");
    //        rpt.Append("</tr>");
    //        rpt.Append("</table>");
    //    }
    //}

    public void BillGetHearder_DetailInvoice(string reqno, string receiptno, string type)
    {
        DataTable dt = thereq.GetRequisitionForReport(Session["CoCode"].ToString().Trim(), Session["YearCode"].ToString().Trim(), reqno); //ds.Tables[0];
        //DataTable SlipSession = (DataTable)Session["SlipSession"];
        DataTable SlipSession = thedia.GetExistTestDetails(Session["CoCode"].ToString().Trim(), Session["YearCode"].ToString().Trim(), reqno, "");
        DataTable testcostdetails = thedia.GetExistTestCostDetails(Session["CoCode"].ToString().Trim(), Session["YearCode"].ToString().Trim(), reqno);
        DataTable billDetails = thedia.GetBillDetails(Session["CoCode"].ToString().Trim(), Session["YearCode"].ToString().Trim(), receiptno, "1");
        DataTable dtPaymentDetails = thedia.GetPaymentDetails(Session["CoCode"].ToString().Trim(), Session["YearCode"].ToString().Trim(), reqno);
        DataTable dtAdvanceDetls = thedia.GetAdvanceDetails(Session["CoCode"].ToString().Trim(), Session["YearCode"].ToString().Trim(), dtPaymentDetails.Rows[0]["Vchno"].ToString().Trim(), reqno);
        string testtype = "";
        for (int n = 0; n < SlipSession.Rows.Count; n++)
        {
            if (SlipSession.Rows[n]["TestType"].ToString().Trim() == "PKG")
            {
                testtype = "PKG";
                break;
            }
        }
        if (RadioButtonList1.SelectedValue == "Y")
        {
            rpt.Append("<table width='100%' cellspacing=0 border=0 style='height:25px;'>");
            rpt.Append("<tr cellpadding:'0'  >");
            rpt.Append("<td width='30%'><img src='../Images/logo.jpg'  style=''/></td>");
            rpt.Append("<td   width='40%'  style='height:20px;font-family:Arial; font-size:medium; text-align:center'></td>");
            rpt.Append("<td width='30%' style='text-align:right'>Print Date : " + DateTime.Now.ToString("dd/MM/yyyy HH:mm tt") + "</td>");
            rpt.Append("</tr>");
            rpt.Append("</table>");
        }
        else
        {
            rpt.Append("<table width='100%' cellspacing=0 border=0 style='height:25px;'>");
            rpt.Append("<tr>");
            rpt.Append("<td style='width:100%;font-family:Verdana;font-size:small; text-align:right;'> Invoice Date :" + billDetails.Rows[0]["BillDate"].ToString() + " " + billDetails.Rows[0]["BillTime"].ToString() + " </td>");
            rpt.Append("</tr>");
            rpt.Append("<tr>");
            rpt.Append("<td style='width:100%;font-family:Verdana;font-size:small; text-align:right;'> Print Date :" + DateTime.Now.ToString("dd/MM/yyyy HH:mm tt") + " </td>");
            rpt.Append("</tr>");
            rpt.Append("</table>");
        }

        if (SlipSession != null)
        {
            if (testtype == "PKG")
            {
                rpt.Append("<table width='100%' cellspacing=0 border=0 style='border-spacing: 0; padding: 0;font-family:verdana;'>");
                rpt.Append("<tr>");
                rpt.Append("<td style='width:100%;font-family:Verdana;font-size:small; text-align:Center;background-color:#9B9C8D'> <b><u>Invoice Cum Money Receipt</u></b>  </td>");
                rpt.Append("</tr>");
                rpt.Append("<tr>");
                rpt.Append("<td style='width:100%;font-family:Verdana;font-size:small; text-align:Center;background-color:#9B9C8D'> PATIENT DETAILS  </td>");
                rpt.Append("</tr>");
                rpt.Append("</table>");

                rpt.Append("<table width='100%' cellspacing=0 border=0 style='border-spacing: 0; padding: 0;font-family:verdana;font-size:small;'>");
                rpt.Append("<tr style=''>");
                rpt.Append("<td style='width: 15% ;font-family:Verdana; text-align:left;font-weight:bold;'>Invoice No :</td>");
                rpt.AppendFormat("<td style='width: 35%;font-family:Verdana; text-align:left'>{0}</td>", billDetails.Rows[0]["ReceiptNo"]);
                rpt.Append("<td style='width: 15%;text-align:left;font-weight:bold;'>Patient Id :</td>");
                rpt.AppendFormat("<td style='width: 35%;font-family:Verdana; text-align:left'>{0}</td>", dt.Rows[0]["RegistrationNo"]);
                rpt.Append("</tr>");

                rpt.Append("<tr style=''>");
                rpt.Append("<td style='font-family:Verdana;  text-align:left;font-weight:bold;'>Name & Age :</td>");
                rpt.AppendFormat("<td style='font-family:Verdana; text-align:left'>{0}</td>", dt.Rows[0]["PatientName"].ToString() + ", " + dt.Rows[0]["age"].ToString());
                rpt.Append("<td style='font-family:Verdana;  text-align:left;font-weight:bold;'>Contact No : </td>");
                rpt.AppendFormat("<td style='font-family:Verdana; text-align:left'>{0}</td>", dt.Rows[0]["Ph1"]);
                rpt.Append("</tr>");

                rpt.Append("<tr style=''>");
                rpt.Append("<td style='font-family:Verdana;  text-align:left;font-weight:bold;'>Address :</td>");
                rpt.AppendFormat("<td  colspan='3' style='font-family:Verdana; text-align:left'>{0}</td>", dt.Rows[0]["Address"]);
                rpt.Append("</tr>");

                if (dt.Rows[0]["UnderDoc"].ToString() != "")
                {
                    rpt.Append("<tr style=''>");
                    rpt.Append("<td style='font-family:Verdana;  text-align:left;font-weight:bold;'>Doctor Name :</td>");
                    rpt.AppendFormat("<td colspan='3' style='font-family:Verdana; text-align:left'>{0}</td>", "Dr. " + dt.Rows[0]["UnderDocName"].ToString() + "( Reg No: " + dt.Rows[0]["DocRegNo"].ToString() + " )");
                    rpt.Append("</tr>");
                }
                if (dt.Rows[0]["ReqType"].ToString() == "DIG")
                {
                    rpt.Append("<tr style=''>");
                    rpt.Append("<td style='font-family:Verdana;  text-align:left;font-weight:bold;'>Referred By:</td>");
                    rpt.AppendFormat("<td colspan='3' style='font-family:Verdana; text-align:left'>{0}</td>", "Dr. " + dt.Rows[0]["ReferalName"].ToString());
                    rpt.Append("</tr>");
                }
                if (dt.Rows[0]["ReqType"].ToString() == "INF")
                {
                    rpt.Append("<tr style=''>");
                    rpt.Append("<td style='font-family:Verdana;  text-align:left;font-weight:bold;'>Referred By:</td>");
                    rpt.AppendFormat("<td colspan='3' style='font-family:Verdana; text-align:left'>{0}</td>", "Dr. " + dt.Rows[0]["ReferalName"].ToString());
                    rpt.Append("</tr>");
                }
                rpt.Append("</table>");


                rpt.Append("<br/>");
                rpt.Append("<center>");



                rpt.Append("<table width='100%' cellspacing=0 border=0 style='border-spacing: 0; padding: 0;font-family:verdana;font-size:small;'>");
                rpt.Append("<tr>");
                rpt.Append("<td style='width: 70%;font-family:Verdana;  text-align:left;font-weight:bold;'>");
                rpt.Append("<table width='100%'  cellspacing=0 border=0 style='border:1px solid black;border-spacing: 0; padding: 0;font-family:verdana;font-size:small;'>");

                rpt.Append("<tr style=''>");
                rpt.Append("<td colspan='2' style='width: 8%;border-left: 1px solid black;border-bottom: 1px solid black;border-top: 1px solid black; font-family:Verdana;font-size:small; text-align:Center;background-color:#9B9C8D;font-weight:bold;'> SERVICE DETAILS  </td>");
                rpt.Append("</tr'>");

                rpt.Append("<tr style=''>");
                //rpt.Append("<td style='width:15%;border-bottom: 1px solid black;border-right: 1px solid black; font-family:Verdana;text-align:left;font-weight:bold;'>Department</td>");
                rpt.Append("<td style='width:70%;border-bottom: 1px solid black;border-right: 1px solid black; font-family:Verdana;  text-align:left;font-weight:bold;'>Service Name</td>");
                rpt.Append("<td style='width:30%;border-bottom: 1px solid black;font-family:Verdana; text-align:right;font-weight:bold;'>Charge</td>");
                double total = 0.0;
                int a = 0, b = 0, c = 0, d = 0;
                rpt.Append("</tr >");

                for (int i = 0; i < SlipSession.Rows.Count; i++)
                {
                    string dep = thedia.GetExistTestDetailsDept(Session["CoCode"].ToString().Trim(), SlipSession.Rows[i]["Testid"].ToString());
                    if (dep == "")
                    {
                        if (SlipSession.Rows[i]["Testid"].ToString().Contains("TX"))
                            dep = "X-Ray";
                        else dep = "USG";
                    }
                    //rpt.AppendFormat("<td style='border-bottom: 1px solid black;border-right: 1px solid black; font-family:Verdana; text-align:left;font-weight:normal;'>{0}</td>", dep);
                    rpt.AppendFormat("<td style='border-bottom: 1px solid black;border-right: 1px solid black; font-family:Verdana;text-align:left;font-weight:normal;'>{0}</td>", SlipSession.Rows[i]["TestName"].ToString());
                    rpt.AppendFormat("<td style='border-bottom: 1px solid black;font-family:Verdana; text-align:right;font-weight:normal;'> {0}</td>", SlipSession.Rows[i]["cost"].ToString());
                    rpt.Append("</tr >");
                    total = total + Convert.ToDouble(SlipSession.Rows[i]["cost"]);
                }
                rpt.Append("</table>");
                rpt.Append("</td>");
                rpt.Append("<td valign='top' style='width: 30%;font-family:Verdana;  text-align:left;font-weight:bold;padding-left:20px;'>");
                rpt.Append("<table width='100%' cellpadding=0 cellspacing=0 >");
                rpt.Append("<tr style=''>");
                rpt.Append("<td colspan='2' style='width:100%; font-family:Verdana; text-align:center;font-weight:bold;'><u>Payment Calculation</u></td>");
                rpt.Append("</tr>");
                rpt.Append("<tr style=''>");
                rpt.Append("<td style='width:70%; font-family:Verdana; text-align:left;font-weight:bold;'>Total Charges</td>");
                rpt.AppendFormat("<td style='width:30%; font-family:Verdana;text-align:right'>{0}</td>", total.ToString("F"));
                rpt.Append("</tr>");
                if (Convert.ToDecimal(billDetails.Rows[0]["DiscountAmt"]) > 0)
                {
                    rpt.Append("<tr style=''>");
                    rpt.Append("<td  style=' font-family:Verdana; text-align:left;font-weight:bold;'>Discount Amt</td>");
                    rpt.AppendFormat("<td style='font-family:Verdana;text-align:right'>{0}</td>", billDetails.Rows[0]["DiscountAmt"].ToString());
                    rpt.Append("</tr>");
                }
                /*if (dtPaymentDetails.Rows.Count > 0)
                {
                    for (int j = 0; j < dtPaymentDetails.Rows.Count; j++)
                    {
                        rpt.Append("<tr style=''>");
                        rpt.AppendFormat("<td style='width:50%;font-family:Verdana;text-align:left'>{0}</td>", dtPaymentDetails.Rows[j]["MoneyReceiptNo"].ToString());
                        rpt.AppendFormat("<td style='width:20%;font-family:Verdana;text-align:left'>{0}</td>", "(" + dtPaymentDetails.Rows[j]["PayDate"].ToString() + ")");
                        rpt.AppendFormat("<td style='width:30%;font-family:Verdana;text-align:right'>{0}</td>", dtPaymentDetails.Rows[j]["paidAmt"].ToString());
                        rpt.Append("</tr>");
                    }
                }*/

                if (dtAdvanceDetls.Rows.Count > 0)
                {
                    rpt.Append("<tr style=''>");
                    rpt.Append("<td style=' font-family:Verdana; text-align:left;font-weight:bold;'>Adjusted Advance</td>");
                    rpt.AppendFormat("<td style='font-family:Verdana;text-align:right'>{0}</td>", dtAdvanceDetls.Rows[0]["AdvAmt"].ToString());
                    rpt.Append("</tr>");
                }
                if (type == "I")
                {
                    rpt.Append("<tr style=''>");
                    rpt.Append("<td  style=' font-family:Verdana; text-align:left;font-weight:bold;'>Paid Amount</td>");
                    rpt.AppendFormat("<td style='font-family:Verdana;text-align:right'>{0}</td>", billDetails.Rows[0]["paid"].ToString());
                    rpt.Append("</tr>");
                }
                if (type == "F")
                {
                    Decimal totpaidamt = Convert.ToDecimal(billDetails.Rows[0]["paid"]) + Convert.ToDecimal(billDetails.Rows[0]["PrevBillAmt"])+Convert.ToDecimal(dtAdvanceDetls.Rows[0]["AdvAmt"]);
                    rpt.Append("<tr style=''>");
                    rpt.Append("<td style=' font-family:Verdana; text-align:left;font-weight:bold;'>Total Paid Amt</td>");
                    rpt.AppendFormat("<td style='font-family:Verdana;text-align:right'>{0}</td>", billDetails.Rows[0]["totpaidamt"].ToString());
                    rpt.Append("</tr>");

                }
                Decimal dueamt = Convert.ToDecimal(total) - Convert.ToDecimal(billDetails.Rows[0]["paid"]) - Convert.ToDecimal(billDetails.Rows[0]["PrevBillAmt"]) - Convert.ToDecimal(billDetails.Rows[0]["DiscountAmt"])-Convert.ToDecimal(dtAdvanceDetls.Rows[0]["AdvAmt"]);
                rpt.Append("<tr>");
                rpt.Append("<td style=' font-family:Verdana; text-align:left;font-weight:bold;'>Due Amt</td>");
                rpt.AppendFormat("<td style=' font-family:Verdana;text-align:right'>{0}</td>", dueamt.ToString("F"));
                //rpt.AppendFormat("<td style=' font-family:Verdana;text-align:right'>{0}</td>", billDetails.Rows[0]["DueAmt"].ToString());
                rpt.Append("</tr>");
                rpt.Append("</table>");
                rpt.Append("</td>");
                rpt.Append("</tr>");

                rpt.Append("<tr style=''>");
                rpt.Append("<td colspan='2'></td>");
                rpt.Append("</tr>");

                rpt.Append("<tr style=''>");
                rpt.Append("<td></td>");
                rpt.Append("<td style='font-family:Verdana;  text-align:center'>________________________________</td>");
                rpt.Append("</tr>");

                rpt.Append("<tr>");
                rpt.Append("<td style='font-family:Verdana;  text-align:left'>In Words : " + billDetails.Rows[0]["num2word"].ToString() + "</td>");
                rpt.Append("<td style='font-family:Verdana;  text-align:center;font-weight:bold;'> " + Session["userName"].ToString() + " </td>");
                rpt.Append("</tr>");
                rpt.Append("<tr>");
                rpt.Append("<td style='font-family:Verdana;  text-align:left'>Received with thanks </td>");
                rpt.Append("<td style='font-family:Verdana;  text-align:center;font-weight:bold;'> For " + Session["CoName"].ToString() + "</td>");
                rpt.Append("</tr>");


                rpt.Append("<tr style='height:50px'>");
                rpt.Append("<td valign='bottom' colspan='2' valign='bottom' style='width: 100%; font-family:Verdana;  text-align:left;font-size:8pt;color:gray;'><i># Incase you find any unintentional system generated discripency in the bill, kindly bring it to our notice for corrective action.</i></td>");
                rpt.Append("</tr>");

                rpt.Append("</table>");
                rpt.Append("</center>");
            }
            else
            {
                rpt.Append("<table width='100%' cellspacing=0 border=0 style='border-spacing: 0; padding: 0;font-family:verdana;'>");
                //rpt.Append("<tr style='height:20px'>");
                //rpt.Append("<td style='width:25%;font-family:Verdana;font-size:small; text-align:right;background-color:#9B9C8D'>&nbsp;</td>");
                //rpt.Append("<td colspan='2' style='width:50%;font-family:Verdana;font-size:small; text-align:right;background-color:#9B9C8D'>&nbsp;</td>");
                //rpt.Append("</tr'>");

                if (type == "M")
                {
                    rpt.Append("<tr>");
                    rpt.Append("<td style='width:100%;font-family:Verdana;font-size:small; text-align:Center;background-color:#9B9C8D'> <b><u>Money Receipt</u></b>  </td>");
                    rpt.Append("</tr>");
                }
                else
                {
                    rpt.Append("<tr>");
                    rpt.Append("<td style='width:100%;font-family:Verdana;font-size:small; text-align:Center;background-color:#9B9C8D'> <b><u>Invoice Cum Money Receipt</u></b>  </td>");
                    rpt.Append("</tr>");
                }

                rpt.Append("<tr>");
                rpt.Append("<td style='width:100%;font-family:Verdana;font-size:small; text-align:Center;background-color:#9B9C8D'> PATIENT DETAILS  </td>");
                rpt.Append("</tr>");
                rpt.Append("</table>");



                rpt.Append("<table width='100%' cellspacing=0 border=0 style='border-spacing: 0; padding: 0;font-family:verdana;font-size:small;'>");
                if (type == "M")
                {
                    rpt.Append("<tr style=''>");
                    rpt.Append("<td style='width: 15% ;font-family:Verdana; text-align:left;font-weight:bold;'>Receipt No :</td>");
                    rpt.AppendFormat("<td style='width: 35%;font-family:Verdana; text-align:left'>{0}</td>", billDetails.Rows[0]["MoneyReceiptNo"]);
                    rpt.Append("<td style='width: 15%;text-align:left;font-weight:bold;'>Patient Id :</td>");
                    rpt.AppendFormat("<td style='width: 35%;font-family:Verdana; text-align:left'>{0}</td>", dt.Rows[0]["RegistrationNo"]);
                    rpt.Append("</tr>");
                }
                else
                {
                    rpt.Append("<tr style=''>");
                    rpt.Append("<td style='width: 15% ;font-family:Verdana; text-align:left;font-weight:bold;'>Invoice No :</td>");
                    rpt.AppendFormat("<td style='width: 35%;font-family:Verdana; text-align:left'>{0}</td>", billDetails.Rows[0]["ReceiptNo"]);
                    rpt.Append("<td style='width: 15%;text-align:left;font-weight:bold;'>Patient Id :</td>");
                    rpt.AppendFormat("<td style='width: 35%;font-family:Verdana; text-align:left'>{0}</td>", dt.Rows[0]["RegistrationNo"]);
                    rpt.Append("</tr>");
                }

                rpt.Append("<tr style=''>");
                rpt.Append("<td style='font-family:Verdana;  text-align:left;font-weight:bold;'>Name & Age :</td>");
                rpt.AppendFormat("<td style='font-family:Verdana; text-align:left'>{0}</td>", dt.Rows[0]["PatientName"].ToString() + ", " + dt.Rows[0]["age"].ToString());
                rpt.Append("<td style='font-family:Verdana;  text-align:left;font-weight:bold;'>Contact No : </td>");
                rpt.AppendFormat("<td style='font-family:Verdana; text-align:left'>{0}</td>", dt.Rows[0]["Ph1"]);
                rpt.Append("</tr>");

                rpt.Append("<tr style=''>");
                rpt.Append("<td style='font-family:Verdana;  text-align:left;font-weight:bold;'>Address :</td>");
                rpt.AppendFormat("<td  colspan='3' style='font-family:Verdana; text-align:left'>{0}</td>", dt.Rows[0]["Address"]);
                rpt.Append("</tr>");

                if (dt.Rows[0]["UnderDoc"].ToString() != "")
                {
                    rpt.Append("<tr style=''>");
                    rpt.Append("<td style='font-family:Verdana;  text-align:left;font-weight:bold;'>Doctor Name :</td>");
                    rpt.AppendFormat("<td colspan='3' style='font-family:Verdana; text-align:left'>{0}</td>", "Dr. " + dt.Rows[0]["UnderDocName"].ToString() + "( Reg No: " + dt.Rows[0]["DocRegNo"].ToString() + " )");
                    rpt.Append("</tr>");
                }
                if (dt.Rows[0]["ReqType"].ToString() == "DIG")
                {
                    rpt.Append("<tr style=''>");
                    rpt.Append("<td style='font-family:Verdana;  text-align:left;font-weight:bold;'>Referred By:</td>");
                    rpt.AppendFormat("<td colspan='3' style='font-family:Verdana; text-align:left'>{0}</td>", "Dr. " + dt.Rows[0]["ReferalName"].ToString());
                    rpt.Append("</tr>");
                }
                if (dt.Rows[0]["ReqType"].ToString() == "INF")
                {
                    rpt.Append("<tr style=''>");
                    rpt.Append("<td style='font-family:Verdana;  text-align:left;font-weight:bold;'>Referred By:</td>");
                    rpt.AppendFormat("<td colspan='3' style='font-family:Verdana; text-align:left'>{0}</td>", "Dr. " + dt.Rows[0]["ReferalName"].ToString());
                    rpt.Append("</tr>");
                }
                rpt.Append("</table>");



                rpt.Append("<br/>");
                rpt.Append("<center>");



                rpt.Append("<table width='100%' cellspacing=0 border=0 style='border-spacing: 0; padding: 0;font-family:verdana;font-size:small;'>");
                rpt.Append("<tr>");
                rpt.Append("<td style='width: 70%;font-family:Verdana;  text-align:left;font-weight:bold;'>");
                rpt.Append("<table width='100%'  cellspacing=0 border=0 style='border:1px solid black;border-spacing: 0; padding: 0;font-family:verdana;font-size:small;'>");

                rpt.Append("<tr style=''>");
                rpt.Append("<td colspan='2' style='width: 8%;border-left: 1px solid black;border-bottom: 1px solid black;border-top: 1px solid black; font-family:Verdana;font-size:small; text-align:Center;background-color:#9B9C8D;font-weight:bold;'> SERVICE DETAILS  </td>");
                rpt.Append("</tr'>");

                rpt.Append("<tr style=''>");
                //rpt.Append("<td style='width:15%;border-bottom: 1px solid black;border-right: 1px solid black; font-family:Verdana;text-align:left;font-weight:bold;'>Department</td>");
                rpt.Append("<td style='width:70%;border-bottom: 1px solid black;border-right: 1px solid black; font-family:Verdana;  text-align:left;font-weight:bold;'>Service Name</td>");
                rpt.Append("<td style='width:30%;border-bottom: 1px solid black;font-family:Verdana; text-align:right;font-weight:bold;'>Charge</td>");
                double total = 0.0;
                int a = 0, b = 0, c = 0, d = 0;
                rpt.Append("</tr >");

                for (int i = 0; i < SlipSession.Rows.Count; i++)
                {
                    string dep = thedia.GetExistTestDetailsDept(Session["CoCode"].ToString().Trim(), SlipSession.Rows[i]["Testid"].ToString());
                    if (dep == "")
                    {
                        if (SlipSession.Rows[i]["Testid"].ToString().Contains("TX"))
                            dep = "X-Ray";
                        else dep = "USG";
                    }
                    //rpt.AppendFormat("<td style='border-bottom: 1px solid black;border-right: 1px solid black; font-family:Verdana; text-align:left;font-weight:normal;'>{0}</td>", dep);
                    rpt.AppendFormat("<td style='border-bottom: 1px solid black;border-right: 1px solid black; font-family:Verdana;text-align:left;font-weight:normal;'>{0}</td>", SlipSession.Rows[i]["TestName"].ToString());
                    rpt.AppendFormat("<td style='border-bottom: 1px solid black;font-family:Verdana; text-align:right;font-weight:normal;'> {0}</td>", SlipSession.Rows[i]["cost"].ToString());
                    rpt.Append("</tr >");
                    total = total + Convert.ToDouble(SlipSession.Rows[i]["cost"]);
                }
                rpt.Append("</table>");
                rpt.Append("</td>");
                rpt.Append("<td valign='top' style='width: 30%;font-family:Verdana;  text-align:left;font-weight:bold;padding-left:20px;'>");
                rpt.Append("<table width='100%' cellpadding=0 cellspacing=0 >");
                rpt.Append("<tr style=''>");
                rpt.Append("<td colspan='2' style='width:100%; font-family:Verdana; text-align:center;font-weight:bold;'><u>Payment Calculation</u></td>");
                rpt.Append("</tr>");
                rpt.Append("<tr style=''>");
                rpt.Append("<td  style='width:60%; font-family:Verdana; text-align:left;font-weight:bold;'>Total Charges</td>");
                rpt.AppendFormat("<td style='width:40%; font-family:Verdana;text-align:right'>{0}</td>", total.ToString("F"));
                rpt.Append("</tr>");
                if (Convert.ToDecimal(billDetails.Rows[0]["DiscountAmt"]) > 0)
                {
                    rpt.Append("<tr style=''>");
                    rpt.Append("<td  style=' font-family:Verdana; text-align:left;font-weight:bold;'>Discount Amt</td>");
                    rpt.AppendFormat("<td style='font-family:Verdana;text-align:right'>{0}</td>", billDetails.Rows[0]["DiscountAmt"].ToString());
                    rpt.Append("</tr>");
                }
                /*if (type == "M")
                {
                    if (Convert.ToDecimal(billDetails.Rows[0]["PrevBillAmt"]) > 0)
                    {
                        rpt.Append("<tr style=''>");
                        rpt.Append("<td  style=' font-family:Verdana; text-align:left;font-weight:bold;'>Previous Paid Amt</td>");
                        rpt.AppendFormat("<td style='font-family:Verdana;text-align:right'>{0}</td>", billDetails.Rows[0]["PrevBillAmt"].ToString());
                        rpt.Append("</tr>");
                    }
                    rpt.Append("<tr style=''>");
                    rpt.Append("<td  style=' font-family:Verdana; text-align:left;font-weight:bold;'>Paid Amt</td>");
                    rpt.AppendFormat("<td style='font-family:Verdana;text-align:right'>{0}</td>", billDetails.Rows[0]["paid"].ToString());
                    rpt.Append("</tr>");
                }*/
                if (dtAdvanceDetls.Rows.Count > 0)
                {
                    rpt.Append("<tr style=''>");
                    rpt.Append("<td style=' font-family:Verdana; text-align:left;font-weight:bold;'>Adjusted Advance</td>");
                    rpt.AppendFormat("<td style='font-family:Verdana;text-align:right'>{0}</td>", dtAdvanceDetls.Rows[0]["AdvAmt"].ToString());
                    rpt.Append("</tr>");
                }
                if (type == "F")
                {
                    Decimal totpaidamt = Convert.ToDecimal(billDetails.Rows[0]["paid"]) + Convert.ToDecimal(billDetails.Rows[0]["PrevBillAmt"]) + Convert.ToDecimal(dtAdvanceDetls.Rows[0]["AdvAmt"]);
                    rpt.Append("<tr style=''>");
                    rpt.Append("<td style=' font-family:Verdana; text-align:left;font-weight:bold;'>Total Paid Amt</td>");
                    rpt.AppendFormat("<td style='font-family:Verdana;text-align:right'>{0}</td>", billDetails.Rows[0]["totpaidamt"].ToString());
                    rpt.Append("</tr>");

                }
                else
                {
                    Decimal totpaidamt = Convert.ToDecimal(billDetails.Rows[0]["paid"]) + Convert.ToDecimal(billDetails.Rows[0]["PrevBillAmt"]) + Convert.ToDecimal(dtAdvanceDetls.Rows[0]["AdvAmt"]);
                    rpt.Append("<tr style=''>");
                    rpt.Append("<td  style=' font-family:Verdana; text-align:left;font-weight:bold;'>Paid Amt</td>");
                    rpt.AppendFormat("<td style='font-family:Verdana;text-align:right'>{0}</td>", billDetails.Rows[0]["totpaidamt"].ToString());
                    rpt.Append("</tr>");
                }

                //Decimal dueamt = Convert.ToDecimal(total) - Convert.ToDecimal(billDetails.Rows[0]["paid"]) - Convert.ToDecimal(billDetails.Rows[0]["PrevBillAmt"]) - Convert.ToDecimal(billDetails.Rows[0]["DiscountAmt"]);
                Decimal dueamt = Convert.ToDecimal(total) - Convert.ToDecimal(billDetails.Rows[0]["paid"]) - Convert.ToDecimal(billDetails.Rows[0]["PrevBillAmt"]) - Convert.ToDecimal(billDetails.Rows[0]["DiscountAmt"]) - Convert.ToDecimal(dtAdvanceDetls.Rows[0]["AdvAmt"]);
                rpt.Append("<tr>");
                rpt.Append("<td  style=' font-family:Verdana; text-align:left;font-weight:bold;'>Due Amt</td>");
                rpt.AppendFormat("<td style=' font-family:Verdana;text-align:right'>{0}</td>", dueamt.ToString("F"));
                //rpt.AppendFormat("<td style=' font-family:Verdana;text-align:right'>{0}</td>", billDetails.Rows[0]["DueAmt"].ToString());
                rpt.Append("</tr>");
                rpt.Append("</table>");
                rpt.Append("</td>");
                rpt.Append("</tr>");
                /*rpt.Append("</table>");

                rpt.Append("<table width='100%' cellpadding=0 cellspacing=0 >");*/


                rpt.Append("<tr style=''>");
                rpt.Append("<td colspan='2'></td>");
                rpt.Append("</tr>");

                rpt.Append("<tr style=''>");
                rpt.Append("<td></td>");
                rpt.Append("<td style='font-family:Verdana;  text-align:center'>________________________________</td>");
                rpt.Append("</tr>");

                rpt.Append("<tr>");
                rpt.Append("<td style='font-family:Verdana;  text-align:left'>In Words : " + billDetails.Rows[0]["num2word"].ToString() + "</td>");
                rpt.Append("<td style='font-family:Verdana;  text-align:center;font-weight:bold;'> " + Session["userName"].ToString() + " </td>");
                rpt.Append("</tr>");
                rpt.Append("<tr>");
                rpt.Append("<td style='font-family:Verdana;  text-align:left'>Received with thanks </td>");
                rpt.Append("<td style='font-family:Verdana;  text-align:center;font-weight:bold;'> For " + Session["CoName"].ToString() + "</td>");
                rpt.Append("</tr>");


                rpt.Append("<tr style='height:50px'>");
                rpt.Append("<td valign='bottom' colspan='2' valign='bottom' style='width: 100%; font-family:Verdana;  text-align:left;font-size:8pt;color:gray;'><i># Incase you find any unintentional system generated discripency in the bill, kindly bring it to our notice for corrective action.</i></td>");
                rpt.Append("</tr>");

                rpt.Append("</table>");
                rpt.Append("</center>");
            }
        }
        
    }

    public void BillGetHearder_Detail(string reqno, string receiptno,string type)
    {
        DataTable dt = thereq.GetRequisitionForReport(Session["CoCode"].ToString().Trim(), Session["YearCode"].ToString().Trim(), reqno); //ds.Tables[0];
        //DataTable SlipSession = (DataTable)Session["SlipSession"];
        DataTable SlipSession = thedia.GetExistTestDetails(Session["CoCode"].ToString().Trim(), Session["YearCode"].ToString().Trim(), reqno, "");
        DataTable testcostdetails = thedia.GetExistTestCostDetails(Session["CoCode"].ToString().Trim(), Session["YearCode"].ToString().Trim(), reqno);
        DataTable billDetails = thedia.GetBillDetails(Session["CoCode"].ToString().Trim(), Session["YearCode"].ToString().Trim(), receiptno, "1");

        if (RadioButtonList1.SelectedValue == "Y")
        {
            rpt.Append("<table width='100%' cellspacing=0 border=0 style='height:25px;'>");
            rpt.Append("<tr cellpadding:'0'  >");
            rpt.Append("<td width='30%'><img src='../Images/logo.jpg'  style=''/></td>");
            rpt.Append("<td   width='40%'  style='height:20px;font-family:Arial; font-size:medium; text-align:center'></td>");
            rpt.Append("<td width='30%' style='text-align:right'>Print Date : " + DateTime.Now.ToString("dd/MM/yyyy HH:mm tt") + "</td>");
            rpt.Append("</tr>");
            rpt.Append("</table>");
        }
        else
        {
            rpt.Append("<table width='100%' cellspacing=0 border=0 style='height:25px;'>");
            rpt.Append("<tr>");
            if (type == "M")
            {
                rpt.Append("<td style='width:100%;font-family:Verdana;font-size:small; text-align:right;'> Receipt Date :" + billDetails.Rows[0]["BillDate"].ToString() + " " + billDetails.Rows[0]["BillTime"].ToString() + " </td>");
            }
            else
            {
                rpt.Append("<td style='width:100%;font-family:Verdana;font-size:small; text-align:right;'> Invoice Date :" + billDetails.Rows[0]["BillDate"].ToString() + " " + billDetails.Rows[0]["BillTime"].ToString() + " </td>");
            }
            rpt.Append("</tr>");
            rpt.Append("<tr>");
            rpt.Append("<td style='width:100%;font-family:Verdana;font-size:small; text-align:right;'> Print Date :" + DateTime.Now.ToString("dd/MM/yyyy HH:mm tt") + " </td>");
            rpt.Append("</tr>");
            rpt.Append("</table>");
        }

        if (SlipSession != null)
        {

            rpt.Append("<table width='100%' cellspacing=0 border=0 style='border-spacing: 0; padding: 0;font-family:verdana;'>");
            //rpt.Append("<tr style='height:20px'>");
            //rpt.Append("<td style='width:25%;font-family:Verdana;font-size:small; text-align:right;background-color:#9B9C8D'>&nbsp;</td>");
            //rpt.Append("<td colspan='2' style='width:50%;font-family:Verdana;font-size:small; text-align:right;background-color:#9B9C8D'>&nbsp;</td>");
            //rpt.Append("</tr'>");

            if (type == "M")
            {
                rpt.Append("<tr>");
                rpt.Append("<td style='width:100%;font-family:Verdana;font-size:small; text-align:Center;background-color:#9B9C8D'> <b><u>Money Receipt</u></b>  </td>");
                rpt.Append("</tr>");
            }
            else
            {
                rpt.Append("<tr>");
                rpt.Append("<td style='width:100%;font-family:Verdana;font-size:small; text-align:Center;background-color:#9B9C8D'> <b><u>Invoice Cum Money Receipt</u></b>  </td>");
                rpt.Append("</tr>");
            }

            rpt.Append("<tr>");
            rpt.Append("<td style='width:100%;font-family:Verdana;font-size:small; text-align:Center;background-color:#9B9C8D'> PATIENT DETAILS  </td>");
            rpt.Append("</tr>");
            rpt.Append("</table>");

            

            rpt.Append("<table width='100%' cellspacing=0 border=0 style='border-spacing: 0; padding: 0;font-family:verdana;font-size:small;'>");
            if (type == "M")
            {
                rpt.Append("<tr style=''>");
                rpt.Append("<td style='width: 15% ;font-family:Verdana; text-align:left;font-weight:bold;'>Receipt No :</td>");
                rpt.AppendFormat("<td style='width: 35%;font-family:Verdana; text-align:left'>{0}</td>", billDetails.Rows[0]["MoneyReceiptNo"]);
                rpt.Append("<td style='width: 15%;text-align:left;font-weight:bold;'>Patient Id :</td>");
                rpt.AppendFormat("<td style='width: 35%;font-family:Verdana; text-align:left'>{0}</td>", dt.Rows[0]["RegistrationNo"]);
                rpt.Append("</tr>");
            }
            else
            {
                rpt.Append("<tr style=''>");
                rpt.Append("<td style='width: 15% ;font-family:Verdana; text-align:left;font-weight:bold;'>Invoice No :</td>");
                rpt.AppendFormat("<td style='width: 35%;font-family:Verdana; text-align:left'>{0}</td>", billDetails.Rows[0]["ReceiptNo"]);
                rpt.Append("<td style='width: 15%;text-align:left;font-weight:bold;'>Patient Id :</td>");
                rpt.AppendFormat("<td style='width: 35%;font-family:Verdana; text-align:left'>{0}</td>", dt.Rows[0]["RegistrationNo"]);
                rpt.Append("</tr>");
            }

            rpt.Append("<tr style=''>");
            rpt.Append("<td style='font-family:Verdana;  text-align:left;font-weight:bold;'>Name & Age :</td>");
            rpt.AppendFormat("<td style='font-family:Verdana; text-align:left'>{0}</td>", dt.Rows[0]["PatientName"].ToString() + ", " + dt.Rows[0]["age"].ToString());
            rpt.Append("<td style='font-family:Verdana;  text-align:left;font-weight:bold;'>Contact No : </td>");
            rpt.AppendFormat("<td style='font-family:Verdana; text-align:left'>{0}</td>", dt.Rows[0]["Ph1"]);
            rpt.Append("</tr>");

            rpt.Append("<tr style=''>");
            rpt.Append("<td style='font-family:Verdana;  text-align:left;font-weight:bold;'>Address :</td>");
            rpt.AppendFormat("<td  colspan='3' style='font-family:Verdana; text-align:left'>{0}</td>", dt.Rows[0]["Address"]);
            rpt.Append("</tr>");

            if (dt.Rows[0]["UnderDoc"].ToString() != "")
            {
                rpt.Append("<tr style=''>");
                rpt.Append("<td style='font-family:Verdana;  text-align:left;font-weight:bold;'>Doctor Name :</td>");
                rpt.AppendFormat("<td colspan='3' style='font-family:Verdana; text-align:left'>{0}</td>", "Dr. " + dt.Rows[0]["UnderDocName"].ToString() + "( Reg No: " + dt.Rows[0]["DocRegNo"].ToString() + " )");
                rpt.Append("</tr>");
            }
            if (dt.Rows[0]["ReqType"].ToString() == "DIG")
            {
                rpt.Append("<tr style=''>");
                rpt.Append("<td style='font-family:Verdana;  text-align:left;font-weight:bold;'>Referred By:</td>");
                rpt.AppendFormat("<td colspan='3' style='font-family:Verdana; text-align:left'>{0}</td>", "Dr. " + dt.Rows[0]["ReferalName"].ToString());
                rpt.Append("</tr>");
            }
            if (dt.Rows[0]["ReqType"].ToString() == "INF")
            {
                rpt.Append("<tr style=''>");
                rpt.Append("<td style='font-family:Verdana;  text-align:left;font-weight:bold;'>Referred By:</td>");
                rpt.AppendFormat("<td colspan='3' style='font-family:Verdana; text-align:left'>{0}</td>", "Dr. " + dt.Rows[0]["ReferalName"].ToString());
                rpt.Append("</tr>");
            }
            rpt.Append("</table>");



            rpt.Append("<br/>");
            rpt.Append("<center>");
            


            rpt.Append("<table width='100%' cellspacing=0 border=0 style='border-spacing: 0; padding: 0;font-family:verdana;font-size:small;'>");
            rpt.Append("<tr>");
            rpt.Append("<td style='width: 70%;font-family:Verdana;  text-align:left;font-weight:bold;'>");
            rpt.Append("<table width='100%'  cellspacing=0 border=0 style='border:1px solid black;border-spacing: 0; padding: 0;font-family:verdana;font-size:small;'>");

            rpt.Append("<tr style=''>");
            rpt.Append("<td colspan='2' style='width: 8%;border-left: 1px solid black;border-bottom: 1px solid black;border-top: 1px solid black; font-family:Verdana;font-size:small; text-align:Center;background-color:#9B9C8D;font-weight:bold;'> SERVICE DETAILS  </td>");
            rpt.Append("</tr'>");

            rpt.Append("<tr style=''>");
            //rpt.Append("<td style='width:15%;border-bottom: 1px solid black;border-right: 1px solid black; font-family:Verdana;text-align:left;font-weight:bold;'>Department</td>");
            rpt.Append("<td style='width:70%;border-bottom: 1px solid black;border-right: 1px solid black; font-family:Verdana;  text-align:left;font-weight:bold;'>Service Name</td>");
            rpt.Append("<td style='width:30%;border-bottom: 1px solid black;font-family:Verdana; text-align:right;font-weight:bold;'>Charge</td>");
            double total = 0.0;
            int a = 0, b = 0, c = 0, d = 0;
            rpt.Append("</tr >");

            for (int i = 0; i < SlipSession.Rows.Count; i++)
            {
                string dep = thedia.GetExistTestDetailsDept(Session["CoCode"].ToString().Trim(), SlipSession.Rows[i]["Testid"].ToString());
                if (dep == "")
                {
                    if (SlipSession.Rows[i]["Testid"].ToString().Contains("TX"))
                        dep = "X-Ray";
                    else dep = "USG";
                }
                //rpt.AppendFormat("<td style='border-bottom: 1px solid black;border-right: 1px solid black; font-family:Verdana; text-align:left;font-weight:normal;'>{0}</td>", dep);
                rpt.AppendFormat("<td style='border-bottom: 1px solid black;border-right: 1px solid black; font-family:Verdana;text-align:left;font-weight:normal;'>{0}</td>", SlipSession.Rows[i]["TestName"].ToString());
                rpt.AppendFormat("<td style='border-bottom: 1px solid black;font-family:Verdana; text-align:right;font-weight:normal;'> {0}</td>", SlipSession.Rows[i]["cost"].ToString());
                rpt.Append("</tr >");
                total = total + Convert.ToDouble(SlipSession.Rows[i]["cost"]);
            }
            rpt.Append("</table>");
            rpt.Append("</td>");
            rpt.Append("<td valign='top' style='width: 30%;font-family:Verdana;  text-align:left;font-weight:bold;padding-left:20px;'>");
            rpt.Append("<table width='100%' cellpadding=0 cellspacing=0 >");
            rpt.Append("<tr style=''>");
            rpt.Append("<td colspan='2' style='width:100%; font-family:Verdana; text-align:center;font-weight:bold;'><u>Payment Calculation</u></td>");
            rpt.Append("</tr>");
            rpt.Append("<tr style=''>");
            rpt.Append("<td  style='width:60%; font-family:Verdana; text-align:left;font-weight:bold;'>Total Charges</td>");
            rpt.AppendFormat("<td style='width:40%; font-family:Verdana;text-align:right'>{0}</td>", total.ToString("F"));
            rpt.Append("</tr>");
            if (Convert.ToDecimal(billDetails.Rows[0]["DiscountAmt"]) > 0)
            {
                rpt.Append("<tr style=''>");
                rpt.Append("<td  style=' font-family:Verdana; text-align:left;font-weight:bold;'>Discount Amt</td>");
                rpt.AppendFormat("<td style='font-family:Verdana;text-align:right'>{0}</td>", billDetails.Rows[0]["DiscountAmt"].ToString());
                rpt.Append("</tr>");
            }
            if (type == "M")
            {
                if (Convert.ToDecimal(billDetails.Rows[0]["PrevBillAmt"]) > 0)
                {
                    rpt.Append("<tr style=''>");
                    rpt.Append("<td  style=' font-family:Verdana; text-align:left;font-weight:bold;'>Previous Paid Amt</td>");
                    rpt.AppendFormat("<td style='font-family:Verdana;text-align:right'>{0}</td>", billDetails.Rows[0]["PrevBillAmt"].ToString());
                    rpt.Append("</tr>");
                }
                rpt.Append("<tr style=''>");
                rpt.Append("<td  style=' font-family:Verdana; text-align:left;font-weight:bold;'>Paid Amt</td>");
                rpt.AppendFormat("<td style='font-family:Verdana;text-align:right'>{0}</td>", billDetails.Rows[0]["paid"].ToString());
                rpt.Append("</tr>");
            }
            else
            {
                Decimal totpaidamt = Convert.ToDecimal(billDetails.Rows[0]["paid"]) + Convert.ToDecimal(billDetails.Rows[0]["PrevBillAmt"]);
                rpt.Append("<tr style=''>");
                rpt.Append("<td  style=' font-family:Verdana; text-align:left;font-weight:bold;'>Paid Amt</td>");
                rpt.AppendFormat("<td style='font-family:Verdana;text-align:right'>{0}</td>", billDetails.Rows[0]["totpaidamt"].ToString());
                rpt.Append("</tr>");
            }

            Decimal dueamt = Convert.ToDecimal(total) - Convert.ToDecimal(billDetails.Rows[0]["paid"]) - Convert.ToDecimal(billDetails.Rows[0]["PrevBillAmt"]) - Convert.ToDecimal(billDetails.Rows[0]["DiscountAmt"]);
            rpt.Append("<tr>");
            rpt.Append("<td  style=' font-family:Verdana; text-align:left;font-weight:bold;'>Due Amt</td>");
            rpt.AppendFormat("<td style=' font-family:Verdana;text-align:right'>{0}</td>", dueamt.ToString("F"));
            //rpt.AppendFormat("<td style=' font-family:Verdana;text-align:right'>{0}</td>", billDetails.Rows[0]["DueAmt"].ToString());
            rpt.Append("</tr>");
            rpt.Append("</table>");
            rpt.Append("</td>");
            rpt.Append("</tr>");
            /*rpt.Append("</table>");

            rpt.Append("<table width='100%' cellpadding=0 cellspacing=0 >");*/
            

            rpt.Append("<tr style=''>");
            rpt.Append("<td colspan='2'></td>");
            rpt.Append("</tr>");

            rpt.Append("<tr style=''>");
            rpt.Append("<td></td>");
            rpt.Append("<td style='font-family:Verdana;  text-align:center'>________________________________</td>");
            rpt.Append("</tr>");

            rpt.Append("<tr>");
            rpt.Append("<td style='font-family:Verdana;  text-align:left'>In Words : " + billDetails.Rows[0]["num2word"].ToString() + "</td>");
            rpt.Append("<td style='font-family:Verdana;  text-align:center;font-weight:bold;'> Username </td>");
            rpt.Append("</tr>");
            rpt.Append("<tr>");
            rpt.Append("<td style='font-family:Verdana;  text-align:left'>Received with thanks </td>");
            rpt.Append("<td style='font-family:Verdana;  text-align:center;font-weight:bold;'> For " + Session["CoName"].ToString() + "</td>");
            rpt.Append("</tr>");
            

            rpt.Append("<tr style='height:50px'>");
            rpt.Append("<td valign='bottom' colspan='2' valign='bottom' style='width: 100%; font-family:Verdana;  text-align:left;font-size:8pt;color:gray;'><i># Incase you find any unintentional system generated discripency in the bill, kindly bring it to our notice for corrective action.</i></td>");
            rpt.Append("</tr>");

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

   // public void Report_Header()
    //{
        //System.Globalization.DateTimeFormatInfo dtf = new DateTimeFormatInfo();
        //string date = DateTime.Now.ToString("dd/MM/yyyy");

        //if (RadioButtonList1.SelectedValue == "Y")
        //{
        //    rpt.Append("<table width='100%' cellspacing=0 border=0 style='border:1px solid black;border-spacing: 0; padding: 0;font-family:verdana;'>");
        //    rpt.Append("<tr cellpadding:'0'>");
        //    rpt.Append("<td width='30%'><img src='../Images/logo.jpg'  style=''/></td>");
        //    rpt.Append("<td   width='40%'  style='height:18px;font-family:Arial; font-size:medium; text-align:center'></td>");
        //    rpt.Append("<td width='30%' style='text-align:right'></td>");
        //    rpt.Append("</tr>");
        //    rpt.Append("<tr cellpadding:'0'>");
        //    rpt.Append("<td width='30%'></td>");
        //    rpt.Append("<td   width='40%'  style='height:20px;font-family:Arial; font-size:medium; text-align:center'><b><u>PATIENT INVOICE LIST</u></b></td>");
        //    rpt.Append("<td width='30%' style='text-align:right;'>Print Date : " + date + "</td>");
        //    rpt.Append("</tr>");
        //    rpt.Append("</table>");
        //}
        //else
        //{
        //    rpt.Append("<table width='100%' cellspacing=0 border=0 style='border-spacing: 0; padding: 0;font-family:verdana;'>");
        //    rpt.Append("<tr cellpadding:'0'>");
        //    rpt.Append("<td width='30%'></td>");
        //    rpt.Append("<td   width='40%'  style='height:20px;font-family:Arial; font-size:medium; text-align:center'><b><u>PATIENT INVOICE LIST</u></b></td>");
        //    rpt.Append("<td width='30%' style='text-align:right;'>Print Date : " + date + "</td>");
        //    rpt.Append("</tr>");
        //    rpt.Append("</table>");
        //}

    //}



    //public void GetHearder_Detail()
    //{
        //ltrReport.Text = "";
        //DataTable dt = theabortion.GetPatientInvoiceList(Session["CoCode"].ToString(), txtRegNo.Text);

        //rpt.Append("<table width='100%' cellspacing=0 border=0 style='border:1px solid black;border-spacing: 0; padding: 0;font-family:verdana;'>");
        //if (dt.Rows.Count > 0)
        //{
        //    rpt.Append("<tr style='height:40px;'>");
        //    rpt.Append("<td style='width:5%;font-weight:bold;background-color:beige; font-size:small; text-align:left;border-bottom: 1px solid black;'>SRL No.</td>");
        //    rpt.Append("<td style='width:15%;font-weight:bold;background-color:beige; font-size:small; text-align:left;border-bottom: 1px solid black;'>REG NO</td>");
        //    rpt.Append("<td style='width:15%;font-weight:bold;background-color:beige; font-size:small; text-align:left;border-bottom: 1px solid black;'>NAME</td>");
        //    rpt.Append("<td style='width:15%;font-weight:bold;background-color:beige; font-size:small; text-align:left;border-bottom: 1px solid black;'>INVOICE NO</td>");
        //    rpt.Append("<td style='width:10%;font-weight:bold;background-color:beige; font-size:small; text-align:left;border-bottom: 1px solid black;'>BILL DATE</td>");
        //    rpt.Append("<td style='width:15%;font-weight:bold;background-color:beige; font-size:small; text-align:left;border-bottom: 1px solid black;'>REQUISITION NO</td>");
        //    rpt.Append("<td style='width:10%;font-weight:bold;background-color:beige; font-size:small; text-align:left;border-bottom: 1px solid black;'>BILL TYPE</td>");
        //    rpt.Append("<td style='width:10%;font-weight:bold;background-color:beige; font-size:small; text-align:left;border-bottom: 1px solid black;'>BILL AMOUNT</td>");
        //    rpt.Append("<td style='width:5%;font-weight:bold;background-color:beige; font-size:small; text-align:left;border-bottom: 1px solid black;'></td>");
            
        //    rpt.Append("</tr>");
        //    int srl = 0;
        //    for (int i = 0; i < dt.Rows.Count; i++)
        //    {
        //        srl = srl + 1;
        //        rpt.Append("<tr>");
        //        rpt.AppendFormat("<td style='font-family:Verdana; font-size:small;text-align:left'>{0}</td>", srl);
        //        rpt.AppendFormat("<td style='font-family:Verdana; font-size:small;text-align:left'>{0}</td>", dt.Rows[i]["RegNo"]);
        //        rpt.AppendFormat("<td style='font-family:Verdana; font-size:small;text-align:left'>{0}</td>", dt.Rows[i]["PatientName"]);
        //        rpt.AppendFormat("<td style='font-family:Verdana; font-size:small;text-align:left'>{0}</td>", dt.Rows[i]["ReceiptNo"]);
        //        rpt.AppendFormat("<td style='font-family:Verdana; font-size:small;text-align:left'>{0}</td>", dt.Rows[i]["BillDate"]);
        //        rpt.AppendFormat("<td style='font-family:Verdana; font-size:small;text-align:left'>{0}</td>", dt.Rows[i]["ReqNo"]);
        //        rpt.AppendFormat("<td style='font-family:Verdana; font-size:small;text-align:left'>{0}</td>", dt.Rows[i]["BillTypeName"]);
        //        rpt.AppendFormat("<td style='font-family:Verdana; font-size:small;text-align:right'>{0}</td>", dt.Rows[i]["BillAmt"]);
        //        rpt.AppendFormat("<td style='font-family:Verdana; font-size:small;text-align:right'>{0}</td>", "<a href='billPrint?" + dt.Rows[i]["VchNo"] + ");'>Print</a>");
                
        //        rpt.Append("</tr>");
        //    }
        //}
        //rpt.Append("</table>");
    //}

    [System.Web.Script.Services.ScriptMethod()]
    [System.Web.Services.WebMethod]
    public static List<string> SearchByPatientName(string prefixText, int count)
    {
        string compcode = string.Empty;
        string yearcode = string.Empty;
        using (SqlConnection conn = new SqlConnection())
        {
            conn.ConnectionString = ConfigurationSettings.AppSettings["ConnectionString"].ToString();
            using (SqlCommand cmd = new SqlCommand())
            {
                compcode = HttpContext.Current.Session["CoCode"].ToString();
                yearcode = HttpContext.Current.Session["YearCode"].ToString();
                cmd.CommandText = "select PatientRegNo + '~' + PName as Name from opd_patientregistration where compcode=@Compcode and PName like @SearchText + '%'";
                cmd.Parameters.AddWithValue("@SearchText", prefixText);
                cmd.Parameters.AddWithValue("@Compcode", compcode);
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

    [System.Web.Script.Services.ScriptMethod()]
    [System.Web.Services.WebMethod]
    public static List<string> SearchByRegNo(string prefixText, int count)
    {
        string compcode = string.Empty;
        string yearcode = string.Empty;
        using (SqlConnection conn = new SqlConnection())
        {
            conn.ConnectionString = ConfigurationSettings.AppSettings["ConnectionString"].ToString();
            using (SqlCommand cmd = new SqlCommand())
            {
                compcode = HttpContext.Current.Session["CoCode"].ToString();
                yearcode = HttpContext.Current.Session["YearCode"].ToString();
                cmd.CommandText = "select PatientRegNo + '~' + PName as Name from opd_patientregistration where compcode=@Compcode and PatientRegNo like @SearchText + '%'";
                cmd.Parameters.AddWithValue("@SearchText", prefixText);
                cmd.Parameters.AddWithValue("@Compcode", compcode);
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
    protected void Button3_Click(object sender, EventArgs e)
    {
        billdiv.Visible = false;
    }
    protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            Label lbldueAmt = (Label)e.Row.FindControl("lbldueAmt");
            LinkButton LinkButton5 = (LinkButton)e.Row.FindControl("LinkButton5");
            if (Convert.ToDecimal(lbldueAmt.Text) > 0)
            {
                LinkButton5.Text = "";
                LinkButton5.Enabled = false;
            }

            

        }
    }
}