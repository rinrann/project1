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

public partial class Medicine_MedicineReturnReport : System.Web.UI.Page
{
    Main Objmain = new Main(ConfigurationSettings.AppSettings["ConnectionString"].ToString());
    MD_ReturnMedicine theHelper = new MD_ReturnMedicine(ConfigurationSettings.AppSettings["ConnectionString"].ToString());
    System.Text.StringBuilder rpt = new System.Text.StringBuilder();
    String Date = DateTime.Now.ToString("dd/MM/yyyy");
    protected void Page_Load(object sender, EventArgs e)
    {
        Page.Title = "Medicine Return Report";
        if (Session["userName"] == null)
        {
            Response.Redirect("../LoginPage.aspx");
        }
        if (Objmain.checkaccss(Session["CoCode"].ToString(), Session["UserRoleID"].ToString().Trim(), "MEDICINE RETURN", checkAccessType.ViewAction) == false)
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
        rpt.Append("<table width='1000px' cellspacing=0 border=0 style='border:1px solid black;background-color:#9B9C8D;border-spacing: 0; padding: 0;font-family:verdana;'>");
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
        DataTable dtPurchaseMedicine = theHelper.GetPurchaseMedicineDetails(Session["CoCode"].ToString().Trim(),Session["YearCode"].ToString().Trim(),txtPurchaseMedicineId.Text);

        if (dtPurchaseMedicine.Rows.Count > 0)
        {
            rpt.Append("<br/>");
            rpt.Append("<table width='1000px' cellspacing=0 border=0 style='border-spacing: 0; padding: 0;font-family:verdana;'>");
            rpt.Append("<tr style='height:20px'>");
            rpt.Append("<td colspan='13' style='width: 8%;border-bottom: 1px solid black; font-family:Verdana;font-weight:bold;background-color:#9B9C8D;font-size:larger; text-align:Center'> Purchase Return Invoice  </td>");
            rpt.Append("</tr'>");

            rpt.Append("<tr style='height:20px'>");
            rpt.Append("<td colspan='2' style='width: 100px; font-family:Verdana;font-weight:bold;font-size:small; text-align:left'>Supplier Name :</td>");
            rpt.AppendFormat("<td colspan='2' style='width: 250px; font-family:Verdana; font-size:small;text-align:left'>{0}</td>", dtPurchaseMedicine.Rows[0]["supplier"]);
            rpt.Append("<td style='width: 150px;font-family:Verdana;font-weight:bold;font-size:small; text-align:left'></td>");
            rpt.Append("<td style='width: 250px;font-family:Verdana;font-weight:bold;font-size:small; text-align:left'></td>");
            rpt.Append("<td style='width: 250px;font-family:Verdana;font-weight:bold;font-size:small; text-align:left'></td>");
            rpt.Append("<td style='width: 250px;font-family:Verdana;font-weight:bold;font-size:small; text-align:left'></td>");
            rpt.Append("<td style='width: 250px;font-family:Verdana;font-weight:bold;font-size:small; text-align:left'></td>");

            rpt.Append("<td style='width: 250px;font-family:Verdana;font-weight:bold;font-size:small; text-align:left'></td>");
            rpt.Append("</tr >");

            rpt.Append("<tr style='height:20px'>");
            rpt.Append("<td colspan='2' style='width: 100px;font-family:Verdana;font-weight:bold;font-size:small; text-align:left'>Return Date :</td>");
            rpt.AppendFormat("<td colspan='2' style='width: 250px;font-family:Verdana; font-size:small;text-align:left'>{0}</td>", dtPurchaseMedicine.Rows[0]["retdate"]);
            rpt.Append("<td style='width:150px;font-family:Verdana;font-weight:bold;font-size:small; text-align:left'></td>");
            rpt.Append("<td style='width: 250px;font-family:Verdana;font-weight:bold;font-size:small; text-align:left'></td>");
            rpt.Append("<td style='width: 250px;font-family:Verdana;font-weight:bold;font-size:small; text-align:left'></td>");
            rpt.Append("<td style='width: 250px;font-family:Verdana;font-weight:bold;font-size:small; text-align:left'></td>");
            rpt.Append("<td style='width: 250px;font-family:Verdana;font-weight:bold;font-size:small; text-align:left'></td>");
            rpt.Append("<td style='width: 250px;font-family:Verdana;font-weight:bold;font-size:small; text-align:left'></td>");
            rpt.Append("</tr >");

            rpt.Append("<tr style='height:20px'>");
            rpt.Append("<td colspan='2' style='width: 100px;font-family:Verdana;font-weight:bold;font-size:small; text-align:left'>Invoice No :</td>");
            rpt.AppendFormat("<td colspan='2' style='width: 250px;font-family:Verdana; font-size:small;text-align:left'>{0}</td>", txtPurchaseMedicineId.Text);
            rpt.Append("<td style='width: 250px;font-family:Verdana;font-weight:bold;font-size:small; text-align:left'></td>");
            rpt.Append("<td style='width: 250px;font-family:Verdana;font-weight:bold;font-size:small; text-align:left'></td>");
            rpt.Append("<td style='width: 250px;font-family:Verdana;font-weight:bold;font-size:small; text-align:left'></td>");
            rpt.Append("<td style='width: 250px;font-family:Verdana;font-weight:bold;font-size:small; text-align:left'></td>");
            rpt.Append("<td style='width:150px;font-family:Verdana;font-weight:bold;font-size:small; text-align:right'>Date :</td>");
            rpt.AppendFormat("<td style='width:250px;font-family:Verdana;font-weight:bold;font-size:small; text-align:left'></td>");
            rpt.Append("</tr >");
            rpt.Append("</table >");

            rpt.Append("<table width='1000px' cellspacing=0 border=0 style='border:1px solid black;border-spacing: 0; padding: 0;font-family:verdana;'>");
            rpt.Append("<tr style='height:20px'>");
            rpt.Append("<td colspan='13' style='width: 8%;border-bottom: 1px solid black; font-family:Verdana;font-weight:bold;background-color:#9B9C8D;font-size:larger; text-align:Center'>Details </td>");
            rpt.Append("</tr'>");

            rpt.Append("<tr style='height:25px'>");
            rpt.Append("<td style='font-family:Verdana;font-weight:bold;font-size:small; text-align:center;border-bottom:1px solid;'>Manufacturer</td>");
            rpt.Append("<td style='font-family:Verdana;font-weight:bold;font-size:small; text-align:center;border-bottom:1px solid;'> Group</td>");
            rpt.Append("<td style='font-family:Verdana;font-weight:bold;font-size:small; text-align:center;border-bottom:1px solid;'> Sub Group</td>");
            rpt.Append("<td style='font-family:Verdana;font-weight:bold;font-size:small; text-align:center;border-bottom:1px solid;'>Item Description</td>");
            rpt.Append("<td style='font-family:Verdana;font-weight:bold;font-size:small; text-align:center;border-bottom:1px solid;'>Batch</td>");
            rpt.Append("<td style='font-family:Verdana;font-weight:bold;font-size:small; text-align:center;border-bottom:1px solid;'>Bill No</td>");
            rpt.Append("<td style='font-family:Verdana;font-weight:bold;font-size:small; text-align:center;border-bottom:1px solid;'>Bill Date</td>");
            rpt.Append("<td style='font-family:Verdana;font-weight:bold;font-size:small; text-align:center;border-bottom:1px solid;'>Quantity</td>");
            rpt.Append("<td style='font-family:Verdana;font-weight:bold;font-size:small; text-align:center;border-bottom:1px solid;'>Expiry Date</td>");
            rpt.Append("<td style='font-family:Verdana;font-weight:bold;font-size:small; text-align:center;border-bottom:1px solid;'>Unit Price</td>");
            rpt.Append("<td style='font-family:Verdana;font-weight:bold;font-size:small; text-align:center;border-bottom:1px solid;'>Leess T.Discount</td>");
            rpt.Append("<td style='font-family:Verdana;font-weight:bold;font-size:small; text-align:center;border-bottom:1px solid;'>S.Tax</td>");
            rpt.Append("<td style='font-family:Verdana;font-weight:bold;font-size:small; text-align:center;border-bottom:1px solid;'>Gross Amount</td>");
            rpt.Append("</tr >");

            for (int i = 0; i < dtPurchaseMedicine.Rows.Count; i++)
            {
                rpt.Append("<tr style='height:25px'>");
                rpt.AppendFormat("<td style='font-family:Verdana;font-size:small; text-align:left';padding-left:50px;>{0}</td>", dtPurchaseMedicine.Rows[i]["MName"]);
                rpt.AppendFormat("<td style='font-family:Verdana;font-size:small; text-align:left';padding-left:50px;>{0}</td>", dtPurchaseMedicine.Rows[i]["MedicineGroupName"]);
                rpt.AppendFormat("<td style='font-family:Verdana;font-size:small; text-align:left';padding-left:50px;>{0}</td>", dtPurchaseMedicine.Rows[i]["SubGrName"]);
                rpt.AppendFormat("<td style='font-family:Verdana;font-size:small; text-align:left';padding-left:50px;>{0}</td>", dtPurchaseMedicine.Rows[i]["MedicineName"]);
                rpt.AppendFormat("<td style='font-family:Verdana;font-size:small; text-align:center;'>{0}</td>", dtPurchaseMedicine.Rows[i]["BatchNo"]);
                rpt.AppendFormat("<td style='font-family:Verdana;font-size:small; text-align:center;'>{0}</td>", dtPurchaseMedicine.Rows[i]["chno"]);
                rpt.AppendFormat("<td style='font-family:Verdana;font-size:small; text-align:center;'>{0}</td>", dtPurchaseMedicine.Rows[i]["purdate"]);
                rpt.AppendFormat("<td style='font-family:Verdana;font-size:small; text-align:center;'>{0}</td>", dtPurchaseMedicine.Rows[i]["Qty"]);
                rpt.AppendFormat("<td style='font-family:Verdana;font-size:small; text-align:center;'>{0}</td>", dtPurchaseMedicine.Rows[i]["exdate"]);
                rpt.AppendFormat("<td style='font-family:Verdana;font-size:small; text-align:right;padding-right:15px;'>{0}</td>", dtPurchaseMedicine.Rows[i]["PricePerUnit"]);
                rpt.AppendFormat("<td style='font-family:Verdana;font-size:small; text-align:right;padding-right:15px;'>{0}</td>", dtPurchaseMedicine.Rows[i]["lessper"].ToString()+'%');
                rpt.AppendFormat("<td style='font-family:Verdana;font-size:small; text-align:right;padding-right:15px;'>{0}</td>", dtPurchaseMedicine.Rows[i]["taxper"].ToString()+'%');
                rpt.AppendFormat("<td style='font-family:Verdana;font-size:small;padding-right:50px; text-align:right'>{0}</td>", dtPurchaseMedicine.Rows[i]["TotalPrice"]);
                rpt.Append("</tr >");
            }
            rpt.Append("<tr style='height:20px'>");
            rpt.Append("<td colspan='13' style='width: 8%;border-bottom: 0px solid black; font-family:Verdana;font-weight:bold;font-size:larger; text-align:Center'> </td>");
            rpt.Append("</tr'>");
            rpt.Append("</table >");


            rpt.Append("<table width='1000px' cellspacing=0 border=0 style='border-spacing: 0; padding: 0;font-family:verdana;'>");
            rpt.Append("<tr style='height:25px'>");
            rpt.Append("<td colspan='12' style='width:720px;font-family:Verdana;font-weight:bold;font-size:small; text-align:right'>Total :</td>");
            rpt.AppendFormat("<td style='width:180px;font-family:Verdana;font-size:small;padding-right:50px; text-align:right'>{0}</td>", dtPurchaseMedicine.Rows[0]["TotalPrice"]);
            rpt.Append("</tr >");

            /*rpt.Append("<tr style='height:25px'>");
            rpt.Append("<td  colspan='4' style='width:720px;font-family:Verdana;font-weight:bold;font-size:small; text-align:right'>Discount :</td>");
            rpt.AppendFormat("<td style='width:180px;font-family:Verdana;font-size:small;padding-right:150px; text-align:right'>{0}</td>", dtPurchaseMedicine.Rows[0]["Discount"]);
            rpt.Append("</tr >");*/
            rpt.Append("</table >");

            rpt.Append("<table width='1000px' cellspacing=0 border=0 style='border:0px solid black;border-spacing: 0; padding: 0;font-family:verdana;'>");
            double net = Convert.ToDouble(dtPurchaseMedicine.Rows[0]["BILLVALUE"]);
            rpt.Append("<tr style='height:25px'>");

            rpt.Append("<td colspan='13' style='width:720px;font-family:Verdana;font-weight:bold;font-size:small; text-align:right'>Net Amount :</td>");
            rpt.AppendFormat("<td style='width:180px;font-family:Verdana;font-size:small; padding-right:50px;text-align:right'>{0}</td>", net);
            rpt.Append("</tr >");
            rpt.Append("</table >");
        }
        ltrReport.Visible = true;

    }


}