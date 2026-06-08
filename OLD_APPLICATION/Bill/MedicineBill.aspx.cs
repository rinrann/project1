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
//using Spire.Doc;
using iTextSharp.text;
using iTextSharp.text.pdf;
using iTextSharp.text.html;
using iTextSharp.text.html.simpleparser;
using System.IO;

public partial class IPD_MedicineBill : System.Web.UI.Page
{
    Main Objmain = new Main(ConfigurationSettings.AppSettings["ConnectionString"].ToString());
    BillGeneration thepatientbill = new BillGeneration(ConfigurationSettings.AppSettings["ConnectionString"].ToString());

    System.Text.StringBuilder rpt = new System.Text.StringBuilder();
    String Date = DateTime.Now.ToString("dd/MM/yyyy");

    protected void Page_Load(object sender, EventArgs e)
    {
        Page.Title = "Medicine Bill";
        if (Session["userName"] == null)
        {
            Response.Redirect("../LoginPage.aspx");
        }
        if (Objmain.checkaccss(Session["CoCode"].ToString(), Session["UserRoleID"].ToString().Trim(), "MEDICINE BILL DETAILS", checkAccessType.ViewAction) == false)
        {
            Response.Redirect("../AccessDenied.aspx");
        }
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
        rpt.Append("<br/>"); rpt.Append("<br/>"); rpt.Append("<br/>");
        rpt.Append("<table width='100%' cellspacing=0 border=0 style='border:1px solid black;background-color:#9B9C8D;border-spacing: 0; padding: 0;font-family:verdana;'>");
        rpt.Append("<tr cellpadding:'0'>");
        rpt.AppendFormat("<td rowspan='4' style='width: 35%; font-family:Verdana; font-size:x-Medium; font-weight:bold;text-align:left'><img src='../Images/logo.jpg'  style='height:87px;width:205px;border-style:inherit;border-color:#D3CDD4'/></td>");
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
        string compcode = Session["CoCode"].ToString().Trim();
        DataSet bill = thepatientbill.BedBill(txtreg.Text, compcode);
        DataTable dtbill = bill.Tables[0];
        DataTable dtTotalbill = bill.Tables[1];

        DataTable dt = thepatientbill.PatientDtls(txtreg.Text, compcode);
        rpt.Append("<br/>");
        DataTable billno = thepatientbill.GetBillDtls(txtreg.Text);
        if (dt.Rows.Count > 0)
        {
            rpt.Append("<table width='100%' cellspacing=0 border=0 style='border:1px solid black;border-spacing: 0; padding: 0;font-family:verdana;'>");


            rpt.Append("<tr style='height:30px'>");
            rpt.Append("<td style='width: 8%;border-bottom: 1px solid black;border-right: 1px solid black; font-family:Verdana;font-weight:bold;background-color:beige; font-size:small; text-align:left'>Reg No :</td>");
            rpt.AppendFormat("<td style='width: 5%;border-bottom: 1px solid black;border-right: 1px solid black; font-family:Verdana; font-size:small;text-align:left'>{0}</td>", dt.Rows[0]["PatientReg"]);
            rpt.Append("<td style='width: 8%;border-bottom: 1px solid black;border-right: 1px solid black; font-family:Verdana;font-weight:bold;background-color:beige; font-size:small; text-align:left'>Bed No :</td>");
            rpt.AppendFormat("<td style='width: 5%;border-bottom: 1px solid black;border-right: 1px solid black; font-family:Verdana; font-size:small;text-align:left'>{0}</td>", dt.Rows[0]["BedNoText"]);
            rpt.Append("<td style='width: 8%;border-bottom: 1px solid black;border-right: 1px solid black; font-family:Verdana;font-weight:bold;background-color:beige; font-size:small; text-align:left'>Admission Date :</td>");
            rpt.AppendFormat("<td style='width: 5%;border-bottom: 1px solid black;border-right: 1px solid black; font-family:Verdana; font-size:small;text-align:left'>{0}</td>", dt.Rows[0]["adate"]);


            rpt.Append("</tr >");

            rpt.Append("<tr style='height:30px'>");
            rpt.Append("<td style='width: 8%;border-bottom: 1px solid black;border-right: 1px solid black; font-family:Verdana;font-weight:bold;background-color:beige; font-size:small; text-align:left'>Patient's Name</td>");
            rpt.AppendFormat("<td style='width: 5%;border-bottom: 1px solid black;border-right: 1px solid black; font-family:Verdana; font-size:small;text-align:left'>{0}</td>", dt.Rows[0]["patient_name"]);
            rpt.Append("<td style='width: 8%;border-bottom: 1px solid black;border-right: 1px solid black; font-family:Verdana;font-weight:bold;background-color:beige; font-size:small; text-align:left'>Address</td>");
            rpt.AppendFormat("<td style='width: 5%;border-bottom: 1px solid black;border-right: 1px solid black; font-family:Verdana; font-size:small;text-align:left'>{0}</td>", dt.Rows[0]["vill_city"]);
            rpt.Append("<td style='width: 8%;border-bottom: 1px solid black;border-right: 1px solid black; font-family:Verdana;font-weight:bold;background-color:beige; font-size:small; text-align:left'>Bill No</td>");
            rpt.AppendFormat("<td style='width: 5%;border-bottom: 1px solid black;border-right: 1px solid black; font-family:Verdana; font-size:small;text-align:left'>{0}</td>", "B001");
            rpt.Append("</tr >");



            //DataSet medicine = thepatientbill.MedicineBill(txtreg.Text,DateTime.Now.ToString("yyyy-MM-dd"));
            DataSet medicine = thepatientbill.DischargeMedicineBill(dt.Rows[0]["LedgerId"].ToString(),Session["CoCode"].ToString().Trim());
            DataTable dtmedicine = medicine.Tables[0];
            DataTable dttotalmedicine = medicine.Tables[1];
            if (dtmedicine.Rows.Count > 0)
            {
                rpt.Append("<tr style='height:30px'>");
                rpt.Append("<td colspan='6' style='width: 8%;border-bottom: 1px solid black;border-right: 1px solid black;text-align:center'><br/></td>");
                rpt.Append("</tr >");

                rpt.Append("<tr style='height:30px'>");
                rpt.Append("<td colspan='6' style='width: 8%;border-bottom: 1px solid black;border-right: 1px solid black; font-family:Verdana;font-weight:bold;background-color:#B6BBB4;font-size:small; text-align:center'>MEDICINE CHARGES DETAILS</td>");
                rpt.Append("</tr >");


                rpt.Append("</tr >");

                rpt.Append("<tr style='height:30px'>");
                rpt.Append("<td style='width: 8%;border-bottom: 1px solid black;border-right: 1px solid black; font-family:Verdana;font-weight:bold;background-color:#9B9C8D; font-size:small; text-align:center'>Sl No</td>");
                rpt.Append("<td style='width: 8%;border-bottom: 1px solid black;border-right: 1px solid black; font-family:Verdana;font-weight:bold;background-color:#9B9C8D; font-size:small; text-align:center'>Issue Date</td>");
                rpt.Append("<td colspan='1' style='width: 8%;border-bottom: 1px solid black;border-right: 1px solid black; font-family:Verdana;font-weight:bold;background-color:#9B9C8D; font-size:small; text-align:left'>Medicine Name</td>");
                
                rpt.Append("<td  style='width: 8%;border-bottom: 1px solid black;border-right: 1px solid black; font-family:Verdana;font-weight:bold;background-color:#9B9C8D; font-size:small; text-align:right'>Quantity</td>");
                rpt.Append("<td style='width: 8%;border-bottom: 1px solid black;border-right: 1px solid black; font-family:Verdana;font-weight:bold;background-color:#9B9C8D; font-size:small; text-align:right'>Per Price</td>");
                rpt.Append("<td style='width: 8%;border-bottom: 1px solid black;border-right: 1px solid black; font-family:Verdana;font-weight:bold;background-color:#9B9C8D; font-size:small; text-align:right'>Total Price</td>");

                rpt.Append("</tr >");

                for (int i = 0; i < dtmedicine.Rows.Count; i++)
                {
                    rpt.Append("<tr style='height:30px'>");
                    rpt.AppendFormat("<td style='width: 8%;border-bottom: 1px solid black;border-right: 1px solid black; font-family:Verdana; font-size:small; text-align:center'>{0} </td>",i+1);
                    rpt.AppendFormat("<td style='width: 8%;border-bottom: 1px solid black;border-right: 1px solid black; font-family:Verdana; font-size:small; text-align:center'>{0}</td>", dtmedicine.Rows[i]["IssueDate"]);
                    rpt.AppendFormat("<td colspan='1' style='width: 8%;border-bottom: 1px solid black;border-right: 1px solid black; font-family:Verdana; font-size:small; text-align:left'>{0}</td>", dtmedicine.Rows[i]["MedicineName"]);
                    rpt.AppendFormat("<td style='width: 8%;border-bottom: 1px solid black;border-right: 1px solid black; font-family:Verdana; font-size:small; text-align:right'>{0}</td>", dtmedicine.Rows[i]["BillQty"]);
                    rpt.AppendFormat("<td style='width: 8%;border-bottom: 1px solid black;border-right: 1px solid black; font-family:Verdana; font-size:small; text-align:right'>{0}</td>", dtmedicine.Rows[i]["SellPricePerUnit"]);
                    rpt.AppendFormat("<td  style='width: 8%;border-bottom: 1px solid black;border-right: 1px solid black; font-family:Verdana; font-size:small; text-align:right'>{0}</td>", dtmedicine.Rows[i]["TotalCharge"]);
                    rpt.Append("</tr >");
                }

                rpt.Append("<tr style='height:30px'>");
                rpt.Append("<td colspan='5' style='width: 8%;border-bottom: 1px solid black;border-right: 1px solid black; font-family:Verdana;font-weight:bold;background-color:#F6B2B2; font-size:small; text-align:center'>Total : -</td>");
                rpt.AppendFormat("<td style='width: 8%;border-bottom: 1px solid black;border-right: 1px solid black; font-family:Verdana;font-weight:bold;background-color:#F6B2B2; font-size:small; text-align:center'>{0}.00</td>", dttotalmedicine.Rows[0]["TOTALBILL"]);
                rpt.Append("</tr >");

            }




            rpt.Append("</table >");

        }

        ltrReport.Visible = true;

    }


    public void GetHearder_Detail_Duplicate()
    {
        DataTable dt = thepatientbill.GetPatientDetails_Duplicate(txtreg.Text.Trim());
        //DataTable custTable1 = thepatientbill.GetAmount(txtreg.Text);
        //DataTable refund_discount = thepatientbill.Get_Refund_Discount(txtreg.Text);


        if (dt.Rows.Count > 0)
        {
            rpt.Append("<table width='100%' cellspacing=0 border=0 style='border:1px solid black;border-spacing: 0; padding: 0;font-family:verdana;'>");
            rpt.Append("<tr style='height:30px'>");
            rpt.Append("<td style='width: 5%;border-bottom: 1px solid black;border-right: 1px solid black; font-family:Verdana;font-weight:bold;background-color:beige; font-size:small; text-align:left'>Registration No :</td>");
            rpt.AppendFormat("<td style='width: 7%;border-bottom: 1px solid black;border-right: 1px solid black; font-family:Verdana; font-size:small;text-align:left'>{0}</td>", dt.Rows[0]["PatientReg"]);

            rpt.Append("<td style='width: 5%; font-family:Verdana;background-color:beige;border-bottom: 1px solid black;border-right: 1px solid black;font-weight:bold;font-size:small; text-align:left'>Patient's Name :</td>");
            rpt.AppendFormat("<td style='width: 7%; font-family:Verdana;border-bottom: 1px solid black; font-size:small;text-align:left'>{0}</td>", dt.Rows[0]["patient_name"]);


            rpt.Append("</tr >");

            rpt.Append("<tr style='height:30px'>");
            rpt.Append("<td style='width: 5%; font-family:Verdana;border-bottom: 1px solid black;border-right: 1px solid black;background-color:beige;font-weight:bold; font-size:small; text-align:left'>Address :</td>");
            rpt.AppendFormat("<td style='width: 7%; font-family:Verdana;border-bottom: 1px solid black;border-right: 1px solid black; font-size:small;text-align:left'>{0}</td>", dt.Rows[0]["vill_city"]);

            rpt.Append("<td style='width: 5%; font-family:Verdana; font-size:small;border-bottom: 1px solid black;background-color:beige;border-right: 1px solid black;font-weight:bold; text-align:left'>Phone No :</td>");
            rpt.AppendFormat("<td style='width: 7%; font-family:Verdana;border-bottom: 1px solid black; font-size:small;text-align:left'>{0}</td>", dt.Rows[0]["PhNo1"]);


            rpt.Append("</tr>");
            rpt.Append("</table>");

            //For Item.............................................................

            DataTable dtfees = thepatientbill.GetPatientDetails_Duplicate(TextBox1.Text);
            if (dtfees.Rows.Count > 0)
            {
                rpt.Append("<br/>");
                rpt.Append("<table cellspacing='0'>");

                rpt.Append("<tr style='height:30px'>");
                rpt.Append("<td colspan='3' style='width: 5%; font-family:Verdana;border-left: 1px solid black;border-top: 1px solid black;border-right: 1px solid black;background-color:#9B9C8D;font-weight:bold; font-size:small; text-align:center'>* Dialysis Bill Slip *</td>");
                rpt.Append("</tr>");

                rpt.Append("<tr  style='height:30px'>");
                rpt.Append("<td style='width: 7%; font-family:Verdana; font-size:small;border-top: 1px solid black;background-color:beige;border-left: 1px solid black;border-bottom: 1px solid black;font-weight:bold; text-align:left'>Item</td>");
                rpt.Append("<td style='width: 7%; font-family:Verdana;border-right: 1px solid black; border-bottom: 1px solid black;border-left: 1px solid black;border-top: 1px solid black;background-color:beige;font-size:small;font-weight:bold; text-align:left'>Cost</td>");
                rpt.Append("<td style='width: 7%; font-family:Verdana; font-size:small;border-top: 1px solid black;background-color:beige;border-bottom: 1px solid black;font-weight:bold; text-align:left'>Item</td>");
                rpt.Append("<td style='width: 7%; font-family:Verdana;border-right: 1px solid black; border-bottom: 1px solid black;border-left: 1px solid black;border-top: 1px solid black;background-color:beige;font-size:small;font-weight:bold; text-align:left'>Cost</td>");

                rpt.Append("</tr>");



                rpt.Append("<tr style='height:30px'>");
                rpt.Append("<td style='width: 7%; font-family:Verdana;border-left: 1px solid black;border-bottom: 1px solid black;font-weight:bold; font-size:small; text-align:left'>Doctor's Fees :</td>");
                rpt.AppendFormat("<td style='width: 7%;border-left: 1px solid black;border-right: 1px solid black; font-family:Verdana;border-bottom: 1px solid black;font-size:small;text-align:left'>{0}</td>", dtfees.Rows[0]["DoctorFees"]);
                rpt.Append("<td style='width: 7%; font-family:Verdana;border-bottom: 1px solid black;font-weight:bold; font-size:small; text-align:left'>Bed Rent :</td>");
                rpt.AppendFormat("<td style='width: 7%;border-left: 1px solid black;border-right: 1px solid black; font-family:Verdana;border-bottom: 1px solid black;font-size:small;text-align:left'>{0}</td>", dt.Rows[0]["Charges"]);
                rpt.Append("</tr>");

                rpt.Append("<tr style='height:30px'>");
                rpt.Append("<td style='width: 7%; font-family:Verdana;border-left: 1px solid black;border-bottom: 1px solid black;font-weight:bold; font-size:small; text-align:left'>Dialysis :</td>");
                rpt.AppendFormat("<td style='width: 7%;border-left: 1px solid black;border-right: 1px solid black; font-family:Verdana;border-bottom: 1px solid black;font-size:small;text-align:left'>{0}</td>", dtfees.Rows[0]["DialysisCharge"]);
                rpt.Append("<td style='width: 7%; font-family:Verdana;border-bottom: 1px solid black;font-weight:bold; font-size:small; text-align:left'>Medicines :</td>");
                rpt.AppendFormat("<td style='width: 7%;border-left: 1px solid black;border-right: 1px solid black; font-family:Verdana;border-bottom: 1px solid black;font-size:small;text-align:left'>{0}</td>", dtfees.Rows[0]["Medicine"]);
                rpt.Append("</tr>");

                rpt.Append("<tr style='height:30px'>");
                rpt.Append("<td style='width: 7%; font-family:Verdana;border-left: 1px solid black;border-bottom: 1px solid black;font-weight:bold; font-size:small; text-align:left'>Disposals :</td>");
                rpt.AppendFormat("<td style='width: 7%;border-left: 1px solid black;border-right: 1px solid black; font-family:Verdana;border-bottom: 1px solid black;font-size:small;text-align:left'>{0}</td>", dtfees.Rows[0]["DispsableCharge"]);
                rpt.Append("<td style='width: 7%; font-family:Verdana;border-bottom: 1px solid black;font-weight:bold; font-size:small; text-align:left'>Others :</td>");
                rpt.AppendFormat("<td style='width: 7%;border-left: 1px solid black;border-right: 1px solid black; font-family:Verdana;border-bottom: 1px solid black;font-size:small;text-align:left'>{0}</td>", dtfees.Rows[0]["OtherCharge"]);
                rpt.Append("</tr>");

                double total = Convert.ToDouble(dtfees.Rows[0]["DoctorFees"]) + Convert.ToDouble(dt.Rows[0]["Charges"]) + Convert.ToDouble(dtfees.Rows[0]["DialysisCharge"]) + Convert.ToDouble(dtfees.Rows[0]["Medicine"]) + Convert.ToDouble(dtfees.Rows[0]["DispsableCharge"]) + Convert.ToDouble(dtfees.Rows[0]["OtherCharge"]);
                rpt.Append("<tr style='height:30px'>");
                rpt.Append("<td style='width: 7%; font-family:Verdana;border-bottom: 1px solid black;border-left: 1px solid black; font-size:small; text-align:left'></td>");
                rpt.Append("<td style='width: 7%; font-family:Verdana;border-bottom: 1px solid black;font-size:small; text-align:left'></td>");
                rpt.Append("<td style='width: 7%; font-family:Verdana; border-bottom: 1px solid black;font-size:small;font-weight:bold; text-align:center'>Total :</td>");
                rpt.AppendFormat("<td style='width: 7%; font-family:Verdana;border-right: 1px solid black;border-bottom: 1px solid black;border-left: 1px solid black;font-size:small;text-align:left'>{0}</td>", total);

                rpt.Append("</tr>");




                rpt.Append("<tr  style='height:30px'>");

                rpt.Append("<td style='width: 5%; font-family:Verdana; border-bottom: 1px solid black;border-left: 1px solid black;background-color:beige;border-right: 1px solid black;font-size:small;font-weight:bold; text-align:left'>Advanced Date :</td>");
                rpt.AppendFormat("<td style='width: 7%; font-family:Verdana; border-bottom: 1px solid black;border-right: 1px solid black;font-size:small;text-align:left'>{0}</td>", dt.Rows[0]["AppDate1"]);
                rpt.Append("<td style='width: 5%; font-family:Verdana; font-size:small;background-color:beige;border-bottom: 1px solid black;border-right: 1px solid black;font-weight:bold; text-align:center'>Advanced  :</td>");
                rpt.AppendFormat("<td style='width: 7%; font-family:Verdana; font-size:small;border-right: 1px solid black;text-align:left'>{0}</td>", dt.Rows[0]["AdvAmount"]);



                rpt.Append("</tr>");

                double due = total - Convert.ToDouble(dt.Rows[0]["AdvAmount"]);
                rpt.Append("<tr  style='height:30px'>");

                rpt.Append("<td style='width: 5%; font-family:Verdana; border-bottom: 1px solid black; border-left: 1px solid black;font-size:small;font-weight:bold; text-align:left'></td>");
                rpt.AppendFormat("<td style='width: 7%; font-family:Verdana; border-bottom: 1px solid black;font-size:small;text-align:left'></td>");
                rpt.Append("<td style='width: 5%; font-family:Verdana; font-size:small; border-bottom: 1px solid black;border-right: 1px solid black;font-weight:bold; text-align:center'>Due Amount  :</td>");
                rpt.AppendFormat("<td style='width: 7%; font-family:Verdana; font-size:small;border-top: 1px solid black;border-bottom: 1px solid black;border-right: 1px solid black;text-align:left'>{0}.00</td>", due);

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
            lblError.ForeColor = System.Drawing.Color.Red;
            lblError.Text = "Please Select Another Patient...";

        }
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
        else
        {
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

    public void PDF(string pdf)
    {
        string filename = "MedicineBill" + pdf;
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
            PDF(TextBox1.Text);


        }
        else
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "msg", "alert('Please Select Proper Report !');", true);
        }
    }
    protected void RadioButtonList2_SelectedIndexChanged(object sender, System.EventArgs e)
    {
        //if (RadioButtonList2.SelectedValue == "Current Report")
        //{
        //    Panel1.Visible = true;
        //    Panel2.Visible = false;
        //}
        //else
        //{
        //    Panel2.Visible = true;
        //    Panel1.Visible = false;
        //}
    }
}