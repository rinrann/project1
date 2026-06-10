using System;
using System.Data;
using System.Configuration;
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
using iTextSharp.text;
using iTextSharp.text.pdf;
using iTextSharp.text.html;
using iTextSharp.text.html.simpleparser;
using System.IO;

public partial class OPD_OPDBillSlip : System.Web.UI.Page
{
    System.Text.StringBuilder rpt = new System.Text.StringBuilder();
    Main Objmain = new Main(ConfigurationSettings.AppSettings["ConnectionString"].ToString());
    OpdCharges thecharge = new OpdCharges(ConfigurationSettings.AppSettings["ConnectionString"].ToString());
    double due_refund; 
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["userName"] == null)
        {
            Response.Redirect("../LoginPage.aspx");
        }
        if (Objmain.checkaccss(Session["CoCode"].ToString(), Session["UserRoleID"].ToString().Trim(), "PATIENT BILLSLIP", checkAccessType.ViewAction) == false)
        {
            Response.Redirect("../AccessDenied.aspx");
        }

        Page.Title = "OPD Bill Slip";

        if (!IsPostBack)
        {
            RadioButtonList1.SelectedValue = "With Header";
            btnBack.Visible = false; btnPDF.Visible = false; cmdPrint.Visible = false;
            Panel2.Visible = false;
            RadioButtonList2.SelectedValue = "Current Report";
        }

    }

    public void GetReport()
    {
        //Report_Header();
        GetHearder_Detail();
        ltrReport.Text = rpt.ToString();
    }
    public void GetReport_Duplicate()
    {
        //Report_Header();
        GetHearder_Detail_Duplicate();
        ltrReport.Text = rpt.ToString();
    }

    public void Report_Header()
    {
        rpt.Append("<br/>"); rpt.Append("<br/>");
        rpt.Append("<table width='100%' cellspacing=0 border=0 style='border:1px solid black;background-color:#9B9C8D;border-spacing: 0; padding: 0;font-family:verdana;'>");
        rpt.Append("<tr cellpadding:'0'>");
        rpt.AppendFormat("<td style='width: 35%; font-family:Verdana; font-size:x-Medium; font-weight:bold;text-align:left'></td>");
        rpt.AppendFormat("<td style='width: 35%; font-family:Verdana; font-size:large; font-weight:bold;text-align:center'>{0}</td>", "GFC Hospital");
        rpt.AppendFormat("<td style='width: 35%; font-family:Verdana; font-size:x-Medium; font-weight:bold;text-align:center'>&nbsp;</td>");
        rpt.Append("</tr>");
        rpt.Append("<tr>");
        rpt.AppendFormat("<td style='width: 35%; font-family:Verdana; font-size:x-Medium; font-weight:bold;text-align:left'></td>");
        rpt.AppendFormat("<td style='width: 35%; font-family:Verdana; font-size:x-Medium; font-weight:bold;text-align:center'>{0}</td>", "REG NO : NH-315/G-70/2013");
        rpt.AppendFormat("<td style='width: 35%; font-family:Verdana; font-size:x-Medium; font-weight:bold;text-align:center'>&nbsp;</td>");
        rpt.Append("</tr>");
        rpt.Append("<tr>");
        rpt.AppendFormat("<td style='width: 35%; font-family:Verdana; font-size:x-Medium; font-weight:bold;text-align:left'></td>");
        rpt.AppendFormat("<td style='width: 35%; font-family:Verdana; font-size:x-Medium; font-weight:bold;text-align:center'>{0}</td>", "KUSHPATA,GHATAL,MIDNAPUR(W),WB,721212");
        rpt.AppendFormat("<td style='width: 35%; font-family:Verdana; font-size:x-Medium; font-weight:bold;text-align:center'>&nbsp;</td>");
        rpt.Append("</tr>");
        rpt.Append("<tr>");
        rpt.AppendFormat("<td style='width: 35%; font-family:Verdana; font-size:x-Medium; font-weight:bold;text-align:left'></td>");
        rpt.AppendFormat("<td style='width: 35%; font-family:Verdana; font-size:x-Medium; font-weight:bold;text-align:center'>{0}</td>", "Ph :(03225)244400/244643  M:9434419825");
        rpt.AppendFormat("<td style='width: 35%; font-family:Verdana; font-size:x-Medium; font-weight:bold;text-align:center'>&nbsp;</td>");
        rpt.Append("</tr>");
        rpt.Append("</table>");
    }
    public void GetHearder_Detail_Duplicate()
    {
        string compcode = Session["CoCode"].ToString().Trim();
        string yearcode = Session["YearCode"].ToString().Trim();
        DataTable dt = thecharge.PatientFill(compcode, TextBox1.Text.Trim());
        DataTable custTable1 = thecharge.GetPaymentDetails(compcode, yearcode, txtreg.Text);
        int i;

        if (dt.Rows.Count > 0)
        {
            rpt.Append("<table width='100%' cellspacing=0 border=0 style='border:1px solid black;border-spacing: 0; padding: 0;font-family:verdana;'>");
            rpt.Append("<tr style='height:30px'>");
            rpt.Append("<td style='width: 5%;border-bottom: 1px solid black;border-right: 1px solid black; font-family:Verdana;font-weight:bold;background-color:beige; font-size:small; text-align:left'>Registration No :</td>");
            rpt.AppendFormat("<td style='width: 7%;border-bottom: 1px solid black;border-right: 1px solid black; font-family:Verdana; font-size:small;text-align:left'>{0}</td>", dt.Rows[0]["PatientRegNo"]);

            rpt.Append("<td style='width: 5%; font-family:Verdana;background-color:beige;border-bottom: 1px solid black;border-right: 1px solid black;font-weight:bold;font-size:small; text-align:left'>Patient's Name :</td>");
            rpt.AppendFormat("<td style='width: 7%; font-family:Verdana;border-bottom: 1px solid black; font-size:small;text-align:left'>{0}</td>", dt.Rows[0]["PName"]);


            rpt.Append("</tr >");

            rpt.Append("<tr style='height:30px'>");
            rpt.Append("<td style='width: 5%; font-family:Verdana;border-bottom: 1px solid black;border-right: 1px solid black;background-color:beige;font-weight:bold; font-size:small; text-align:left'>Address :</td>");
            rpt.AppendFormat("<td style='width: 7%; font-family:Verdana;border-bottom: 1px solid black;border-right: 1px solid black; font-size:small;text-align:left'>{0}</td>", dt.Rows[0]["Address"]);

            rpt.Append("<td style='width: 5%; font-family:Verdana; font-size:small;border-bottom: 1px solid black;background-color:beige;border-right: 1px solid black;font-weight:bold; text-align:left'>Phone No :</td>");
            rpt.AppendFormat("<td style='width: 7%; font-family:Verdana;border-bottom: 1px solid black; font-size:small;text-align:left'>{0}</td>", dt.Rows[0]["PhNo1"]);


            rpt.Append("</tr>");
            rpt.Append("</table>");
            DataTable Dtledger = thecharge.GetAllLedgerId(compcode, TextBox1.Text);
            //For Item.............................................................
            if(Dtledger.Rows.Count>0)
            {
                for (i = 0; i < Dtledger.Rows.Count; i++)
                {
                    DataSet ds = thecharge.GetBillDetails_Duplicate(compcode, yearcode, TextBox1.Text, Dtledger.Rows[i]["LedgerID"].ToString());
                    DataTable dtfees = ds.Tables[0];
                    DataTable dtotherfees = ds.Tables[1];
                    if (dtfees.Rows.Count > 0)
                    {
                    rpt.Append("<br/>");
                    rpt.Append("<table cellspacing='0'>");

                    rpt.Append("<tr style='height:30px'>");
                    rpt.Append("<td style='width: 30%; font-family:Verdana;border-left: 1px solid black;border-top: 1px solid black;border-right: 1px solid black;background-color:#9B9C8D;font-weight:bold; font-size:small; text-align:center'>* OPD Bill Slip *</td>");
                    rpt.Append("<td style='width: 30%; font-family:Verdana;border-left: 1px solid black;border-top: 1px solid black;border-right: 1px solid black;background-color:#9B9C8D;font-weight:bold; font-size:small; text-align:center'>Appointment Date</td>");
                    rpt.AppendFormat("<td style='width: 30%; font-family:Verdana;border-left: 1px solid black;border-top: 1px solid black;border-right: 1px solid black;background-color:#9B9C8D;font-weight:bold; font-size:small; text-align:center'>{0}</td>", dtfees.Rows[0]["AppointMentDate"]);
                    rpt.Append("</tr>");

                    rpt.Append("<tr  style='height:30px'>");
                    rpt.Append("<td style='width: 1%; font-family:Verdana; font-size:small;border-top: 1px solid black;background-color:beige;border-left: 1px solid black;border-bottom: 1px solid black;font-weight:bold; text-align:center'>Sl. No.</td>");
                    rpt.Append("<td style='width: 40%; font-family:Verdana;border-bottom: 1px solid black;border-left: 1px solid black;border-top: 1px solid black;background-color:beige;font-size:small;font-weight:bold; text-align:center'>Description</td>");
                    rpt.Append("<td style='width: 7%; font-family:Verdana; font-size:small;border-right: 1px solid black; border-left: 1px solid black; border-top: 1px solid black;background-color:beige;border-bottom: 1px solid black;font-weight:bold; text-align:center'>Amount (Rs.)</td>");

                    rpt.Append("</tr>");



                    rpt.Append("<tr style='height:30px'>");
                    rpt.Append("<td style='width: 1%; font-family:Verdana;border-left: 1px solid black;border-bottom: 1px solid black;font-weight:bold; font-size:small; text-align:center'>1.</td>");
                    rpt.Append("<td style='width: 40%; font-family:Verdana;border-left: 1px solid black;border-bottom: 1px solid black;font-weight:bold; font-size:small;padding-left:45px; text-align:left'>Regn Fee :</td>");
                    rpt.AppendFormat("<td style='width: 7%;border-left: 1px solid black;border-right: 1px solid black; font-family:Verdana;border-bottom: 1px solid black;padding-right:100px;font-size:small;text-align:right'>{0}</td>", dtfees.Rows[0]["RegnFees"]);
                    rpt.Append("</tr>");



                    rpt.Append("<tr style='height:30px'>");
                    rpt.Append("<td style='width: 1%; font-family:Verdana;border-bottom: 1px solid black;border-left: 1px solid black;font-weight:bold; font-size:small; text-align:center'>2.</td>");
                    rpt.Append("<td style='width: 40%; font-family:Verdana;border-bottom: 1px solid black;font-weight:bold;border-left: 1px solid black; font-size:small;padding-left:45px; text-align:left'>Doctor Fees :</td>");
                    rpt.AppendFormat("<td style='width: 7%;border-left: 1px solid black;border-right: 1px solid black; font-family:Verdana;border-bottom: 1px solid black;padding-right:100px;font-size:small;text-align:right'>{0}</td>", dtfees.Rows[0]["DoctorFees"]);
                    rpt.Append("</tr>");

                    rpt.Append("<tr style='height:30px'>");
                    rpt.Append("<td style='width: 1%; font-family:Verdana;border-left: 1px solid black;border-bottom: 1px solid black;font-weight:bold; font-size:small; text-align:center'>3.</td>");
                    rpt.Append("<td style='width: 40%; font-family:Verdana;border-left: 1px solid black;border-bottom: 1px solid black;font-weight:bold; font-size:small;padding-left:45px; text-align:left'>USG Charge :</td>");
                    rpt.AppendFormat("<td style='width: 7%;border-left: 1px solid black;border-right: 1px solid black; font-family:Verdana;border-bottom: 1px solid black;padding-right:100px;font-size:small;text-align:right'>{0}</td>", dtfees.Rows[0]["USGCharge"]);
                    rpt.Append("</tr>");

                    rpt.Append("<tr style='height:30px'>");
                    rpt.Append("<td style='width: 1%; font-family:Verdana;border-bottom: 1px solid black;border-left: 1px solid black; font-weight:bold; font-size:small; text-align:center'>4.</td>");
                    rpt.Append("<td style='width: 40%; font-family:Verdana;border-bottom: 1px solid black;font-weight:bold; font-size:small; border-left: 1px solid black;padding-left:45px;text-align:left'>IUI Charge :</td>");
                    rpt.AppendFormat("<td style='width: 7%;border-left: 1px solid black;border-right: 1px solid black; font-family:Verdana;border-bottom: 1px solid black;padding-right:100px;font-size:small;text-align:right'>{0}</td>", dtfees.Rows[0]["IUICharge"]);
                    rpt.Append("</tr>");

                    rpt.Append("<tr style='height:30px'>");
                    rpt.Append("<td style='width: 1%; font-family:Verdana;border-left: 1px solid black;border-bottom: 1px solid black;font-weight:bold; font-size:small; text-align:center'>5.</td>");
                    rpt.Append("<td style='width: 40%; font-family:Verdana;border-left: 1px solid black;border-bottom: 1px solid black;font-weight:bold; font-size:small;padding-left:45px; text-align:left'>Investigation Charge :</td>");
                    rpt.AppendFormat("<td style='width: 7%;border-left: 1px solid black;border-right: 1px solid black; font-family:Verdana;border-bottom: 1px solid black;padding-right:100px;font-size:small;text-align:right'>{0}</td>", dtfees.Rows[0]["InvestigationCharge"]);
                    rpt.Append("</tr>");

                    rpt.Append("<tr style='height:30px'>");
                    rpt.Append("<td style='width: 1%; font-family:Verdana;border-bottom: 1px solid black;border-left: 1px solid black; font-weight:bold; font-size:small; text-align:center'>6.</td>");
                    rpt.Append("<td style='width: 40%; font-family:Verdana;border-bottom: 1px solid black;font-weight:bold; font-size:small;border-left: 1px solid black;padding-left:45px; text-align:left'>Operation Charge :</td>");
                    rpt.AppendFormat("<td style='width: 7%;border-left: 1px solid black;border-right: 1px solid black; font-family:Verdana;border-bottom: 1px solid black;padding-right:100px;font-size:small;text-align:right'>{0}</td>", dtfees.Rows[0]["OperationCharge"]);
                    rpt.Append("</tr>");

                    rpt.Append("<tr style='height:30px'>");
                    rpt.Append("<td style='width: 1%; font-family:Verdana;border-left: 1px solid black;border-bottom: 1px solid black;font-weight:bold; font-size:small; text-align:center'>7.</td>");
                    rpt.Append("<td style='width: 20%; font-family:Verdana;border-left: 1px solid black;border-bottom: 1px solid black;font-weight:bold; font-size:small;padding-left:45px; text-align:left'>Medicine Charge :</td>");
                    rpt.AppendFormat("<td style='width: 7%;border-left: 1px solid black;border-right: 1px solid black; font-family:Verdana;border-bottom: 1px solid black;padding-right:100px;font-size:small;text-align:right'>{0}</td>", dtfees.Rows[0]["MedicineCharge"]);
                    rpt.Append("</tr>");
                    int slno = 7;
                    double othchrgTot = 0;
                    if (dtotherfees.Rows.Count > 0)
                    {
                        slno++;
                        DataTable billtype = thecharge.DropdownGetBillType(compcode, dtotherfees.Rows[0]["BillTypeId"].ToString().Trim());
                        rpt.Append("<tr style='height:30px'>");
                        rpt.AppendFormat("<td style='width: 1%; font-family:Verdana;border-bottom: 1px solid black;border-left: 1px solid black;font-weight:bold; font-size:small; text-align:center'>{0}.</td>", slno);
                        rpt.AppendFormat("<td style='width: 40%; font-family:Verdana;border-left: 1px solid black;border-bottom: 1px solid black;font-weight:bold; font-size:small;padding-left:45px; text-align:left'>{0} :</td>", billtype.Rows[0]["BillTypeName"]);
                        rpt.AppendFormat("<td style='width: 7%;border-left: 1px solid black;border-right: 1px solid black; font-family:Verdana;border-bottom: 1px solid black;padding-right:100px;font-size:small;text-align:right'>{0}</td>", dtotherfees.Rows[0]["Charge"]);
                        rpt.Append("</tr>");
                        othchrgTot = othchrgTot + Convert.ToDouble(dtotherfees.Rows[0]["Charge"]);
                    }

                    /*rpt.Append("<tr style='height:30px'>");
                    rpt.Append("<td style='width: 1%; font-family:Verdana;border-left: 1px solid black;border-bottom: 1px solid black;font-weight:bold; font-size:small; text-align:center'>9.</td>");
                    rpt.Append("<td style='width: 40%; font-family:Verdana;border-left: 1px solid black;border-bottom: 1px solid black;font-weight:bold; font-size:small;padding-left:45px; text-align:left'>Previous Due :</td>");
                    rpt.AppendFormat("<td style='width: 7%;border-left: 1px solid black;border-right: 1px solid black; font-family:Verdana;border-bottom: 1px solid black;padding-right:100px;font-size:small;text-align:right'>{0}</td>", dtfees.Rows[0]["PreviousDue"]);
                    rpt.Append("</tr>");*/

                    double total = Convert.ToDouble(dtfees.Rows[0]["RegnFees"]) + Convert.ToDouble(dtfees.Rows[0]["DoctorFees"]) + Convert.ToDouble(dtfees.Rows[0]["IUICharge"]) + Convert.ToDouble(dtfees.Rows[0]["InvestigationCharge"]) + Convert.ToDouble(dtfees.Rows[0]["MedicineCharge"]) + Convert.ToDouble(dtfees.Rows[0]["OperationCharge"]) + Convert.ToDouble(dtfees.Rows[0]["USGCharge"]) + othchrgTot;
                    rpt.Append("<tr style='height:30px'>");
                    rpt.Append("<td style='width: 1%; font-family:Verdana;border-bottom: 1px solid black;border-left: 1px solid black;font-size:small; text-align:left'></td>");
                    rpt.Append("<td style='width: 40%; font-family:Verdana; border-bottom: 1px solid black;font-size:small;font-weight:bold; text-align:center'>Total Bill Amount :</td>");
                    rpt.AppendFormat("<td style='width: 7%; font-family:Verdana;border-right: 1px solid black;border-bottom: 1px solid black;border-left: 1px solid black;padding-right:100px;font-size:small;text-align:right'>{0}.00</td>", total);

                    rpt.Append("</tr>");
                    DataTable dtpay = thecharge.GetPaymentDetails_LedgerWise(compcode, yearcode,TextBox1.Text, Dtledger.Rows[i]["LedgerID"].ToString());
                    if (dtpay.Rows.Count > 0)
                    {
                        double netpaid = Convert.ToDouble(dtpay.Rows[0]["Income"]) - Convert.ToDouble(dtpay.Rows[0]["Refund"]);
                        if (Convert.ToDouble(dtpay.Rows[0]["Refund"].ToString()) != 0)
                        {
                            rpt.Append("<tr  style='height:30px'>");
                            rpt.AppendFormat("<td style='width: 1%; font-family:Verdana; border-bottom: 1px solid black;border-left: 1px solid black;font-size:small;text-align:left'></td>");
                            rpt.Append("<td style='width: 40%; font-family:Verdana; font-size:small;border-bottom: 1px solid black;border-right: 1px solid black;font-weight:bold; text-align:center'>Refund Amount  :</td>");
                            rpt.AppendFormat("<td style='width: 7%; font-family:Verdana; font-size:small;border-bottom: 1px solid black;border-right: 1px solid black;padding-right:100px;text-align:left'>{0}</td>", dtpay.Rows[0]["Refund"].ToString());
                            rpt.Append("</tr>");
                        }

                        rpt.Append("<tr  style='height:30px'>");
                        rpt.AppendFormat("<td style='width: 1%; font-family:Verdana; border-bottom: 1px solid black;border-left: 1px solid black;font-size:small;text-align:left'></td>");
                        rpt.Append("<td style='width: 40%; font-family:Verdana; font-size:small;border-bottom: 1px solid black;font-weight:bold; text-align:center'>Discount Amount  :</td>");
                        rpt.AppendFormat("<td style='width: 7%; font-family:Verdana; font-size:small;border-right: 1px solid black;border-left: 1px solid black;border-bottom: 1px solid black;padding-right:100px;text-align:right'>{0}</td>", dtpay.Rows[0]["Discount"].ToString());
                        rpt.Append("</tr>");

                        rpt.Append("<tr  style='height:30px'>");
                        rpt.AppendFormat("<td style='width: 1%; font-family:Verdana; border-bottom: 1px solid black;border-left: 1px solid black;font-size:small;text-align:left'></td>");
                        rpt.Append("<td style='width:40%; font-family:Verdana; font-size:small;border-bottom: 1px solid black; font-weight:bold; text-align:center'>Amount Paid  :</td>");
                        rpt.AppendFormat("<td style='width: 7%; font-family:Verdana; font-size:small;border-bottom: 1px solid black;border-left: 1px solid black;padding-right:100px;border-right: 1px solid black;text-align:right'>{0}.00</td>", dtpay.Rows[0]["Income"]);
                        
                        rpt.Append("<tr  style='height:30px'>");
                        rpt.AppendFormat("<td style='width: 1%; font-family:Verdana; border-bottom: 1px solid black;border-left: 1px solid black;font-size:small;text-align:left'></td>");
                        rpt.Append("<td style='width:40%; font-family:Verdana; font-size:small;border-bottom: 1px solid black; font-weight:bold; text-align:center'>Net Amount Paid  :</td>");
                        rpt.AppendFormat("<td style='width: 7%; font-family:Verdana; font-size:small;border-bottom: 1px solid black;border-left: 1px solid black;padding-right:100px;border-right: 1px solid black;text-align:right'>{0}.00</td>", netpaid.ToString());
                        rpt.Append("</tr>");
                        rpt.Append("<tr  style='height:30px'>");
                        rpt.AppendFormat("<td style='width: 1%; font-family:Verdana; border-bottom: 1px solid black;border-left: 1px solid black;font-size:small;text-align:left'></td>");
                        rpt.Append("<td style='width: 40%; font-family:Verdana; font-size:small;border-bottom: 1px solid black;font-weight:bold; text-align:center'>Due Amount  :</td>");
                        rpt.AppendFormat("<td style='width: 7%; font-family:Verdana; font-size:small;border-right: 1px solid black;border-left: 1px solid black;border-bottom: 1px solid black;padding-right:100px;text-align:right'>{0}</td>", dtpay.Rows[0]["Due"].ToString());
                        rpt.Append("</tr>");
                        DataTable amountinwords = thecharge.GetamountinWords(netpaid.ToString());
                        rpt.Append("<tr  style='height:30px'>");
                        rpt.AppendFormat("<td colspan='3' style='font-family:Verdana; font-size:small;border-bottom: 1px solid black;border-left: 1px solid black;border-right: 1px solid black; font-weight:bold; text-align:center'>Pain Amount in Words  :  ({0})</td>", amountinwords.Rows[0][0]);
                        rpt.Append("</tr>");

                    }
                    rpt.Append("</table>");

                    
                    // End for   Items
                    rpt.Append("<br/>");
                    rpt.Append("<br/>");

                }
            }


                rpt.Append("<br/>"); rpt.Append("<br/>"); rpt.Append("<br/>"); rpt.Append("<br/>"); rpt.Append("<br/>");
                rpt.Append("<table>");

                rpt.Append("<tr>");
                rpt.Append("<td style='width: 5%; font-family:Verdana; font-size:small;font-weight:bold; text-align:left'></td>");
                rpt.Append("<td style='width: 5%; font-family:Verdana; font-size:small;font-weight:bold; text-align:left'></td>");
                rpt.Append("<td style='width: 5%; font-family:Verdana; font-size:small;font-weight:bold; text-align:left'></td>");
                rpt.Append("<td style='width: 5%; font-family:Verdana; font-size:small;font-weight:bold; text-align:center'>______________________________________</td>");
                rpt.Append("</tr'>");

                rpt.Append("<tr>");
                rpt.Append("<td style='width: 5%; font-family:Times New Roman; font-size:small;font-weight:bold; text-align:left'></td>");
                rpt.Append("<td style='width: 5%; font-family:Verdana; font-size:small;font-weight:bold; text-align:left'></td>");
                rpt.Append("<td style='width: 5%; font-family:Verdana; font-size:small;font-weight:bold; text-align:left'></td>");
                rpt.Append("<td style='width: 5%; font-family:Verdana; font-size:small;font-weight:bold; text-align:center'>Cashier</td>");

                rpt.Append("</tr'>");
                rpt.Append("</table>");

            }
            rpt.Append("<hr>");

        }
        else
        {
            lblError.ForeColor = System.Drawing.Color.Red;
            lblError.Text = "Please Select Another Patient...";

        }
    }
    public void GetHearder_Detail()
    {
        string compcode = Session["CoCode"].ToString().Trim();
        string yearcode = Session["YearCode"].ToString().Trim();
        DataTable dt = thecharge.PatientFill(compcode, txtreg.Text.Trim());
        DataTable custTable1 = thecharge.GetPaymentDetails(compcode, yearcode, txtreg.Text);

        /*if (custTable.Rows.Count > 0)
            TextBox2.Text = custTable.Rows[0]["Debit"].ToString();
        if (refund_discount.Rows[0]["Refund"].ToString() != "0.00")
        {
            due_refund = 1;
        }
        else
        {
            due_refund = 0;
        }*/

        if (dt.Rows.Count > 0)
        {
            rpt.Append("<table width='100%' cellspacing=0 border=0 style='border:1px solid black;border-spacing: 0; padding: 0;font-family:verdana;'>");
            rpt.Append("<tr style='height:30px'>");
            rpt.Append("<td style='width: 5%;border-bottom: 1px solid black;border-right: 1px solid black; font-family:Verdana;font-weight:bold;background-color:beige; font-size:small; text-align:left'>Registration No :</td>");
            rpt.AppendFormat("<td style='width: 7%;border-bottom: 1px solid black;border-right: 1px solid black; font-family:Verdana; font-size:small;text-align:left'>{0}</td>", dt.Rows[0]["PatientRegNo"]);

            rpt.Append("<td style='width: 5%; font-family:Verdana;background-color:beige;border-bottom: 1px solid black;border-right: 1px solid black;font-weight:bold;font-size:small; text-align:left'>Patient's Name :</td>");
            rpt.AppendFormat("<td style='width: 7%; font-family:Verdana;border-bottom: 1px solid black; font-size:small;text-align:left'>{0}</td>", dt.Rows[0]["PName"]);


            rpt.Append("</tr >");

            rpt.Append("<tr style='height:30px'>");
            rpt.Append("<td style='width: 5%; font-family:Verdana;border-bottom: 1px solid black;border-right: 1px solid black;background-color:beige;font-weight:bold; font-size:small; text-align:left'>Address :</td>");
            rpt.AppendFormat("<td style='width: 7%; font-family:Verdana;border-bottom: 1px solid black;border-right: 1px solid black; font-size:small;text-align:left'>{0}</td>", dt.Rows[0]["Address"]);

            rpt.Append("<td style='width: 5%; font-family:Verdana; font-size:small;border-bottom: 1px solid black;background-color:beige;border-right: 1px solid black;font-weight:bold; text-align:left'>Phone No :</td>");
            rpt.AppendFormat("<td style='width: 7%; font-family:Verdana;border-bottom: 1px solid black; font-size:small;text-align:left'>{0}</td>", dt.Rows[0]["PhNo1"]);


            rpt.Append("</tr>");



            rpt.Append("</table>");

            //For Item.............................................................
            DataSet ds = thecharge.GetBillDetails(compcode, yearcode, txtreg.Text);
            DataTable dtfees = ds.Tables[0];
            DataTable dtotherfees = ds.Tables[1];
            if (dtfees.Rows.Count > 0)
            {
                rpt.Append("<br/>");
                rpt.Append("<table cellspacing='0'>");

                rpt.Append("<tr style='height:30px'>");
                rpt.Append("<td colspan='3' style='width: 5%; font-family:Verdana;border-left: 1px solid black;border-top: 1px solid black;border-right: 1px solid black;background-color:#9B9C8D;font-weight:bold; font-size:small; text-align:center'>* OPD Bill Slip *</td>");
                rpt.Append("</tr>");

                rpt.Append("<tr  style='height:30px'>");
                rpt.Append("<td style='width: 1%; font-family:Verdana; font-size:small;border-top: 1px solid black;background-color:beige;border-left: 1px solid black;border-bottom: 1px solid black;font-weight:bold; text-align:center'>Sl. No.</td>");
                rpt.Append("<td style='width: 40%; font-family:Verdana;border-bottom: 1px solid black;border-left: 1px solid black;border-top: 1px solid black;background-color:beige;font-size:small;font-weight:bold; text-align:center'>Description</td>");
                rpt.Append("<td style='width: 7%; font-family:Verdana; font-size:small;border-right: 1px solid black; border-left: 1px solid black; border-top: 1px solid black;background-color:beige;border-bottom: 1px solid black;font-weight:bold; text-align:center'>Amount (Rs.)</td>");

                rpt.Append("</tr>");



                rpt.Append("<tr style='height:30px'>");
                rpt.Append("<td style='width: 1%; font-family:Verdana;border-left: 1px solid black;border-bottom: 1px solid black;font-weight:bold; font-size:small; text-align:center'>1.</td>");
                rpt.Append("<td style='width: 40%; font-family:Verdana;border-left: 1px solid black;border-bottom: 1px solid black;font-weight:bold; font-size:small;padding-left:45px; text-align:left'>Regn Fee :</td>");
                rpt.AppendFormat("<td style='width: 7%;border-left: 1px solid black;border-right: 1px solid black; font-family:Verdana;border-bottom: 1px solid black;padding-right:100px;font-size:small;text-align:right'>{0}</td>", dtfees.Rows[0]["RegnFees"]);
                rpt.Append("</tr>");



                rpt.Append("<tr style='height:30px'>");
                rpt.Append("<td style='width: 1%; font-family:Verdana;border-bottom: 1px solid black;border-left: 1px solid black;font-weight:bold; font-size:small; text-align:center'>2.</td>");
                rpt.Append("<td style='width: 40%; font-family:Verdana;border-bottom: 1px solid black;font-weight:bold;border-left: 1px solid black; font-size:small;padding-left:45px; text-align:left'>Doctor Fees :</td>");
                rpt.AppendFormat("<td style='width: 7%;border-left: 1px solid black;border-right: 1px solid black; font-family:Verdana;border-bottom: 1px solid black;padding-right:100px;font-size:small;text-align:right'>{0}</td>", dtfees.Rows[0]["DoctorFees"]);
                rpt.Append("</tr>");

                rpt.Append("<tr style='height:30px'>");
                rpt.Append("<td style='width: 1%; font-family:Verdana;border-left: 1px solid black;border-bottom: 1px solid black;font-weight:bold; font-size:small; text-align:center'>3.</td>");
                rpt.Append("<td style='width: 40%; font-family:Verdana;border-left: 1px solid black;border-bottom: 1px solid black;font-weight:bold; font-size:small;padding-left:45px; text-align:left'>USG Charge :</td>");
                rpt.AppendFormat("<td style='width: 7%;border-left: 1px solid black;border-right: 1px solid black; font-family:Verdana;border-bottom: 1px solid black;padding-right:100px;font-size:small;text-align:right'>{0}</td>", dtfees.Rows[0]["USGCharge"]);
                rpt.Append("</tr>");

                rpt.Append("<tr style='height:30px'>");
                rpt.Append("<td style='width: 1%; font-family:Verdana;border-bottom: 1px solid black;border-left: 1px solid black; font-weight:bold; font-size:small; text-align:center'>4.</td>");
                rpt.Append("<td style='width: 40%; font-family:Verdana;border-bottom: 1px solid black;font-weight:bold; font-size:small; border-left: 1px solid black;padding-left:45px;text-align:left'>IUI Charge :</td>");
                rpt.AppendFormat("<td style='width: 7%;border-left: 1px solid black;border-right: 1px solid black; font-family:Verdana;border-bottom: 1px solid black;padding-right:100px;font-size:small;text-align:right'>{0}</td>", dtfees.Rows[0]["IUICharge"]);
                rpt.Append("</tr>");

                rpt.Append("<tr style='height:30px'>");
                rpt.Append("<td style='width: 1%; font-family:Verdana;border-left: 1px solid black;border-bottom: 1px solid black;font-weight:bold; font-size:small; text-align:center'>5.</td>");
                rpt.Append("<td style='width: 40%; font-family:Verdana;border-left: 1px solid black;border-bottom: 1px solid black;font-weight:bold; font-size:small;padding-left:45px; text-align:left'>Investigation Charge :</td>");
                rpt.AppendFormat("<td style='width: 7%;border-left: 1px solid black;border-right: 1px solid black; font-family:Verdana;border-bottom: 1px solid black;padding-right:100px;font-size:small;text-align:right'>{0}</td>", dtfees.Rows[0]["InvestigationCharge"]);
                rpt.Append("</tr>");

                rpt.Append("<tr style='height:30px'>");
                rpt.Append("<td style='width: 1%; font-family:Verdana;border-bottom: 1px solid black;border-left: 1px solid black; font-weight:bold; font-size:small; text-align:center'>6.</td>");
                rpt.Append("<td style='width: 40%; font-family:Verdana;border-bottom: 1px solid black;font-weight:bold; font-size:small;border-left: 1px solid black;padding-left:45px; text-align:left'>Operation Charge :</td>");
                rpt.AppendFormat("<td style='width: 7%;border-left: 1px solid black;border-right: 1px solid black; font-family:Verdana;border-bottom: 1px solid black;padding-right:100px;font-size:small;text-align:right'>{0}</td>", dtfees.Rows[0]["OperationCharge"]);
                rpt.Append("</tr>");

                rpt.Append("<tr style='height:30px'>");
                rpt.Append("<td style='width: 1%; font-family:Verdana;border-left: 1px solid black;border-bottom: 1px solid black;font-weight:bold; font-size:small; text-align:center'>7.</td>");
                rpt.Append("<td style='width: 20%; font-family:Verdana;border-left: 1px solid black;border-bottom: 1px solid black;font-weight:bold; font-size:small;padding-left:45px; text-align:left'>Medicine Charge :</td>");
                rpt.AppendFormat("<td style='width: 7%;border-left: 1px solid black;border-right: 1px solid black; font-family:Verdana;border-bottom: 1px solid black;padding-right:100px;font-size:small;text-align:right'>{0}</td>", dtfees.Rows[0]["MedicineCharge"]);
                rpt.Append("</tr>");
                int slno = 7;
                double othchrgTot = 0;
                if (dtotherfees.Rows.Count > 0)
                {
                    slno++;
                    DataTable billtype = thecharge.DropdownGetBillType(compcode, dtotherfees.Rows[0]["BillTypeId"].ToString().Trim());
                    rpt.Append("<tr style='height:30px'>");
                    rpt.AppendFormat("<td style='width: 1%; font-family:Verdana;border-bottom: 1px solid black;border-left: 1px solid black;font-weight:bold; font-size:small; text-align:center'>{0}.</td>",slno);
                    rpt.AppendFormat("<td style='width: 40%; font-family:Verdana;border-left: 1px solid black;border-bottom: 1px solid black;font-weight:bold; font-size:small;padding-left:45px; text-align:left'>{0} :</td>", billtype.Rows[0]["BillTypeName"]);
                    rpt.AppendFormat("<td style='width: 7%;border-left: 1px solid black;border-right: 1px solid black; font-family:Verdana;border-bottom: 1px solid black;padding-right:100px;font-size:small;text-align:right'>{0}</td>", dtotherfees.Rows[0]["Charge"]);
                    rpt.Append("</tr>");
                    othchrgTot = othchrgTot + Convert.ToDouble(dtotherfees.Rows[0]["Charge"]);
                }
                
                /*rpt.Append("<tr style='height:30px'>");
                rpt.Append("<td style='width: 1%; font-family:Verdana;border-left: 1px solid black;border-bottom: 1px solid black;font-weight:bold; font-size:small; text-align:center'>9.</td>");
                rpt.Append("<td style='width: 40%; font-family:Verdana;border-left: 1px solid black;border-bottom: 1px solid black;font-weight:bold; font-size:small;padding-left:45px; text-align:left'>Previous Due :</td>");
                rpt.AppendFormat("<td style='width: 7%;border-left: 1px solid black;border-right: 1px solid black; font-family:Verdana;border-bottom: 1px solid black;padding-right:100px;font-size:small;text-align:right'>{0}</td>", dtfees.Rows[0]["PreviousDue"]);
                rpt.Append("</tr>");*/

                double total = Convert.ToDouble(dtfees.Rows[0]["RegnFees"]) + Convert.ToDouble(dtfees.Rows[0]["DoctorFees"]) + Convert.ToDouble(dtfees.Rows[0]["IUICharge"]) + Convert.ToDouble(dtfees.Rows[0]["InvestigationCharge"]) + Convert.ToDouble(dtfees.Rows[0]["MedicineCharge"]) + Convert.ToDouble(dtfees.Rows[0]["OperationCharge"]) + Convert.ToDouble(dtfees.Rows[0]["USGCharge"]) + othchrgTot;
                rpt.Append("<tr style='height:30px'>");
                rpt.Append("<td style='width: 1%; font-family:Verdana;border-bottom: 1px solid black;border-left: 1px solid black;font-size:small; text-align:left'></td>");
                rpt.Append("<td style='width: 40%; font-family:Verdana; border-bottom: 1px solid black;font-size:small;font-weight:bold; text-align:center'>Total Bill Amount :</td>");
                rpt.AppendFormat("<td style='width: 7%; font-family:Verdana;border-right: 1px solid black;border-bottom: 1px solid black;border-left: 1px solid black;padding-right:100px;font-size:small;text-align:right'>{0}.00</td>", total);

                rpt.Append("</tr>");
                DataTable dtpay = thecharge.GetPaymentDetails(compcode, yearcode, txtreg.Text);
                if (dtpay.Rows.Count > 0)
                {
                    if (Convert.ToDouble(dtpay.Rows[0]["Refund"].ToString())!= 0)
                    {
                        rpt.Append("<tr  style='height:30px'>");
                        rpt.AppendFormat("<td style='width: 1%; font-family:Verdana; border-bottom: 1px solid black;border-left: 1px solid black;font-size:small;text-align:left'></td>");
                        rpt.Append("<td style='width: 40%; font-family:Verdana; font-size:small;border-bottom: 1px solid black;border-right: 1px solid black;font-weight:bold; text-align:center'>Refund Amount  :</td>");
                        rpt.AppendFormat("<td style='width: 7%; font-family:Verdana; font-size:small;border-bottom: 1px solid black;border-right: 1px solid black;padding-right:100px;text-align:left'>{0}</td>", dtpay.Rows[0]["Refund"].ToString());
                        rpt.Append("</tr>");
                    }

                    rpt.Append("<tr  style='height:30px'>");
                    rpt.AppendFormat("<td style='width: 1%; font-family:Verdana; border-bottom: 1px solid black;border-left: 1px solid black;font-size:small;text-align:left'></td>");
                    rpt.Append("<td style='width: 40%; font-family:Verdana; font-size:small;border-bottom: 1px solid black;font-weight:bold; text-align:center'>Discount Amount  :</td>");
                    rpt.AppendFormat("<td style='width: 7%; font-family:Verdana; font-size:small;border-right: 1px solid black;border-left: 1px solid black;border-bottom: 1px solid black;padding-right:100px;text-align:right'>{0}</td>", dtpay.Rows[0]["Discount"].ToString());
                    rpt.Append("</tr>");

                    rpt.Append("<tr  style='height:30px'>");
                    rpt.AppendFormat("<td style='width: 1%; font-family:Verdana; border-bottom: 1px solid black;border-left: 1px solid black;font-size:small;text-align:left'></td>");
                    rpt.Append("<td style='width:40%; font-family:Verdana; font-size:small;border-bottom: 1px solid black; font-weight:bold; text-align:center'>Net Amount Paid  :</td>");
                    rpt.AppendFormat("<td style='width: 7%; font-family:Verdana; font-size:small;border-bottom: 1px solid black;border-left: 1px solid black;padding-right:100px;border-right: 1px solid black;text-align:right'>{0}.00</td>", dtpay.Rows[0]["Income"]);
                    rpt.Append("</tr>");
                    rpt.Append("<tr  style='height:30px'>");
                    rpt.AppendFormat("<td style='width: 1%; font-family:Verdana; border-bottom: 1px solid black;border-left: 1px solid black;font-size:small;text-align:left'></td>");
                    rpt.Append("<td style='width: 40%; font-family:Verdana; font-size:small;border-bottom: 1px solid black;font-weight:bold; text-align:center'>Due Amount  :</td>");
                    rpt.AppendFormat("<td style='width: 7%; font-family:Verdana; font-size:small;border-right: 1px solid black;border-left: 1px solid black;border-bottom: 1px solid black;padding-right:100px;text-align:right'>{0}</td>", dtpay.Rows[0]["Due"].ToString());
                    rpt.Append("</tr>");
                }
                rpt.Append("</table>");

                DataTable amountinwords = thecharge.GetamountinWords(dtpay.Rows[0]["Income"].ToString());
                rpt.Append("<table cellspacing='0' style='width:100%;'>");
                rpt.Append("<tr  style='height:30px'>");
                rpt.AppendFormat("<td  style='font-family:Verdana; font-size:small;border-bottom: 1px solid black;border-left: 1px solid black;border-right: 1px solid black; font-weight:bold; text-align:center'>Amount in Words  :  ({0})</td>", amountinwords.Rows[0][0]);
                rpt.Append("</tr>");

                rpt.Append("</table>");
                // End for   Items
                rpt.Append("<br/>");
                rpt.Append("<br/>");



                rpt.Append("<br/>"); rpt.Append("<br/>"); rpt.Append("<br/>"); rpt.Append("<br/>"); rpt.Append("<br/>");
                rpt.Append("<table>");

                rpt.Append("<tr>");
                rpt.Append("<td style='width: 5%; font-family:Verdana; font-size:small;font-weight:bold; text-align:left'></td>");
                rpt.Append("<td style='width: 5%; font-family:Verdana; font-size:small;font-weight:bold; text-align:left'></td>");
                rpt.Append("<td style='width: 5%; font-family:Verdana; font-size:small;font-weight:bold; text-align:left'></td>");
                rpt.Append("<td style='width: 5%; font-family:Verdana; font-size:small;font-weight:bold; text-align:center'>______________________________________</td>");
                rpt.Append("</tr'>");

                rpt.Append("<tr>");
                rpt.Append("<td style='width: 5%; font-family:Times New Roman; font-size:small;font-weight:bold; text-align:left'></td>");
                rpt.Append("<td style='width: 5%; font-family:Verdana; font-size:small;font-weight:bold; text-align:left'></td>");
                rpt.Append("<td style='width: 5%; font-family:Verdana; font-size:small;font-weight:bold; text-align:left'></td>");
                rpt.Append("<td style='width: 5%; font-family:Verdana; font-size:small;font-weight:bold; text-align:center'>Cashier</td>");

                rpt.Append("</tr'>");
                rpt.Append("</table>");

            }
            rpt.Append("<hr>");

        }
        else
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "msg", "alert('Please atfirst Do Registration or Confirm !');", true);
        }
    }

    public void PDF(string pdf)
    {
        string filename = "BillSlip" + pdf;
        Response.ContentType = "application/pdf";
        Response.AddHeader("content-disposition", "attachment;filename='" + filename + "'.pdf");
        Response.Cache.SetCacheability(HttpCacheability.NoCache);
        StringWriter sw = new StringWriter();
        HtmlTextWriter hw = new HtmlTextWriter(sw);
        HtmlForm frm = new HtmlForm();
        mydiv.Parent.Controls.Add(frm);
        frm.Attributes["runat"] = "server";
        frm.Controls.Add(mydiv);
        frm.RenderControl(hw);
        StringReader sr = new StringReader(sw.ToString());
        Document pdfDoc = new Document(PageSize.A4, 10f, 10f, 10f, 0f);
        HTMLWorker htmlparser = new HTMLWorker(pdfDoc);
        PdfWriter.GetInstance(pdfDoc, Response.OutputStream);
        pdfDoc.Open();
        htmlparser.Parse(sr);
        pdfDoc.Close();
        Response.Write(pdfDoc);
        Response.End();
    }


    protected void Button4_Click(object sender, System.EventArgs e)
    {

        if (RadioButtonList2.SelectedValue == "Current Report")
        {
            TextBox1.Text = "";
            if (RadioButtonList1.SelectedValue == "With Header")
            {
                Report_Header();
                GetReport();


            }
            else
            {
                GetReport();

            }
            ltrReport.Text = rpt.ToString();
            if (ltrReport.Text != "")
            {
                btnBack.Visible = true;
                btnPDF.Visible = true;
                cmdPrint.Visible = true;
            }
            else
            {
                btnBack.Visible = false;
                btnPDF.Visible = false;
                cmdPrint.Visible = false;

            }
        }
    }

    protected void Button2_Click(object sender, System.EventArgs e)
    {

        if (RadioButtonList2.SelectedValue == "Duplicate Report")
        {
            txtreg.Text = "";
            if (RadioButtonList1.SelectedValue == "With Header")
            {
                Report_Header();
                GetReport_Duplicate();


            }
            else
            {
                GetReport_Duplicate();

            }
            ltrReport.Text = rpt.ToString();
            if (ltrReport.Text != "")
            {
                btnBack.Visible = true;
                btnPDF.Visible = true;
                cmdPrint.Visible = true;
            }
            else
            {
                btnBack.Visible = false;
                btnPDF.Visible = false;
                cmdPrint.Visible = false;

            }
        }
    }
    protected void RadioButtonList2_SelectedIndexChanged(object sender, System.EventArgs e)
    {
        if (RadioButtonList2.SelectedValue == "Current Report")
        {
            Panel1.Visible = true;
            Panel2.Visible = false;
        }
        else
        {
            Panel2.Visible = true;
            Panel1.Visible = false;
        }
    }
    protected void btnPDF_Click(object sender, System.EventArgs e)
    {
        if (RadioButtonList1.SelectedValue == "Current Report" || txtreg.Text != "")
        {
            GetReport();
            ltrReport.Text = rpt.ToString();
            PDF(txtreg.Text);


        }
        else if (RadioButtonList1.SelectedValue == "Duplicate Report" || TextBox1.Text != "")
        {
            GetReport_Duplicate();
            ltrReport.Text = rpt.ToString();
            PDF(TextBox2.Text);


        }
        else
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "msg", "alert('Please Select Proper Report !');", true);
        }
    }
}