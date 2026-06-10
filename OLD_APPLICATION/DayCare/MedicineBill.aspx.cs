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

public partial class DayCare_MedicineBill : System.Web.UI.Page
{

    Main Objmain = new Main(ConfigurationSettings.AppSettings["ConnectionString"].ToString());
    BillGeneration thepatientbill = new BillGeneration(ConfigurationSettings.AppSettings["ConnectionString"].ToString());
    DC_AddMedicine theMedicine = new DC_AddMedicine(ConfigurationSettings.AppSettings["ConnectionString"].ToString());
    System.Text.StringBuilder rpt = new System.Text.StringBuilder(); 
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

    }
    protected void Button4_Click(object sender, EventArgs e)
    {
        GetReport();
    }

    public void GetReport()
    {
        Report_Header();
        GetHearder_Detail();
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

        DataTable dt = thepatientbill.PatientDtls(txtreg.Text, Session["CoCode"].ToString().Trim());
        rpt.Append("<br/>");
        DataTable billno = thepatientbill.GetBillDtls(txtreg.Text);
        if (dt.Rows.Count > 0)
        {
            rpt.Append("<table width='100%' cellspacing=0 border=0 style='border:1px solid black;border-spacing: 0; padding: 0;font-family:verdana;'>");

            rpt.Append("<tr style='height:30px'>");
            rpt.Append("<td style='width: 8%;border-bottom: 1px solid black;border-right: 1px solid black; font-family:Verdana;font-weight:bold;background-color:beige; font-size:small; text-align:left'>Reg No :</td>");
            rpt.AppendFormat("<td style='width: 5%;border-bottom: 1px solid black;border-right: 1px solid black; font-family:Verdana; font-size:small;text-align:left'>{0}</td>", dt.Rows[0]["PatientReg"]);
            rpt.Append("<td style='width: 8%;border-bottom: 1px solid black;border-right: 1px solid black; font-family:Verdana;font-weight:bold;background-color:beige; font-size:small; text-align:left'>Patient's Name :</td>");
            rpt.AppendFormat("<td style='width: 5%;border-bottom: 1px solid black;border-right: 1px solid black; font-family:Verdana; font-size:small;text-align:left'>{0}</td>", dt.Rows[0]["patient_name"]);
            rpt.Append("<td style='width: 8%;border-bottom: 1px solid black;border-right: 1px solid black; font-family:Verdana;font-weight:bold;background-color:beige; font-size:small; text-align:left'>Age :</td>");
            rpt.AppendFormat("<td style='width: 5%;border-bottom: 1px solid black;border-right: 1px solid black; font-family:Verdana; font-size:small;text-align:left'>{0}</td>", dt.Rows[0]["age"]);
            rpt.Append("</tr >");

            rpt.Append("<tr style='height:30px'>");
            rpt.Append("<td style='width: 8%;border-bottom: 1px solid black;border-right: 1px solid black; font-family:Verdana;font-weight:bold;background-color:beige; font-size:small; text-align:left'>Address :</td>");
            rpt.AppendFormat("<td style='width: 5%;border-bottom: 1px solid black;border-right: 1px solid black; font-family:Verdana; font-size:small;text-align:left'>{0}</td>", dt.Rows[0]["vill_city"]);
            rpt.Append("<td style='width: 8%;border-bottom: 1px solid black;border-right: 1px solid black; font-family:Verdana;font-weight:bold;background-color:beige; font-size:small; text-align:left'>Bed No :</td>");
            rpt.AppendFormat("<td style='width: 5%;border-bottom: 1px solid black;border-right: 1px solid black; font-family:Verdana; font-size:small;text-align:left'>{0}</td>", dt.Rows[0]["BedNoText"]);
            rpt.Append("<td style='width: 8%;border-bottom: 1px solid black;border-right: 1px solid black; font-family:Verdana;font-weight:bold;background-color:beige; font-size:small; text-align:left'>Bill No :</td>");
            rpt.AppendFormat("<td style='width: 5%;border-bottom: 1px solid black;border-right: 1px solid black; font-family:Verdana; font-size:small;text-align:left'>{0}</td>", "B001");
            rpt.Append("</tr >");



            DataSet medicine = theMedicine.GetMedicine_Bill(Session["CoCode"].ToString().Trim(), Session["YearCode"].ToString().Trim(), txtreg.Text);
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
                rpt.Append("<td colspan='2' style='width: 8%;border-bottom: 1px solid black;border-right: 1px solid black; font-family:Verdana;font-weight:bold;background-color:#9B9C8D; font-size:small; text-align:center'>Medicine Name</td>");
                rpt.Append("<td style='width: 8%;border-bottom: 1px solid black;border-right: 1px solid black; font-family:Verdana;font-weight:bold;background-color:#9B9C8D; font-size:small; text-align:center'>Issue Date</td>");
                rpt.Append("<td  style='width: 8%;border-bottom: 1px solid black;border-right: 1px solid black; font-family:Verdana;font-weight:bold;background-color:#9B9C8D; font-size:small; text-align:center'>Quantity</td>");
                rpt.Append("<td style='width: 8%;border-bottom: 1px solid black;border-right: 1px solid black; font-family:Verdana;font-weight:bold;background-color:#9B9C8D; font-size:small; text-align:center'>Per Price</td>");
                rpt.Append("<td style='width: 8%;border-bottom: 1px solid black;border-right: 1px solid black; font-family:Verdana;font-weight:bold;background-color:#9B9C8D; font-size:small; text-align:center'>Total Price</td>");

                rpt.Append("</tr >");

                for (int i = 0; i < dtmedicine.Rows.Count; i++)
                {
                    rpt.Append("<tr style='height:30px'>");
                    rpt.AppendFormat("<td colspan='2' style='width: 8%;border-bottom: 1px solid black;border-right: 1px solid black; font-family:Verdana; font-size:small; text-align:center'>{0}</td>", dtmedicine.Rows[i]["MedicineName"]);
                    rpt.AppendFormat("<td style='width: 8%;border-bottom: 1px solid black;border-right: 1px solid black; font-family:Verdana; font-size:small; text-align:center'>{0}</td>", dtmedicine.Rows[i]["IssueDate"]);
                    rpt.AppendFormat("<td style='width: 8%;border-bottom: 1px solid black;border-right: 1px solid black; font-family:Verdana; font-size:small; text-align:center'>{0}</td>", dtmedicine.Rows[i]["BillQty"]);
                    rpt.AppendFormat("<td style='width: 8%;border-bottom: 1px solid black;border-right: 1px solid black; font-family:Verdana; font-size:small; text-align:center'>{0}</td>", dtmedicine.Rows[i]["SellPricePerUnit"]);
                    rpt.AppendFormat("<td  style='width: 8%;border-bottom: 1px solid black;border-right: 1px solid black; font-family:Verdana; font-size:small; text-align:center'>{0}</td>", dtmedicine.Rows[i]["TotalCharge"]);
                    rpt.Append("</tr >");
                }

                rpt.Append("<tr style='height:30px'>");
                rpt.Append("<td colspan='5' style='width: 8%;border-bottom: 1px solid black;border-right: 1px solid black; font-family:Verdana;font-weight:bold;background-color:#F6B2B2; font-size:small; text-align:center'>Total : -</td>");
                rpt.AppendFormat("<td style='width: 8%;border-bottom: 1px solid black;border-right: 1px solid black; font-family:Verdana;font-weight:bold;background-color:#F6B2B2; font-size:small; text-align:center'>{0}.00</td>", dttotalmedicine.Rows[0]["TotalBill"]);
                rpt.Append("</tr >");

            }




            rpt.Append("</table >");

        }

        ltrReport.Visible = true;

    } 
}