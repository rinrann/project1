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

public partial class Medicine_InvoiceSellPriceDetailsReport : System.Web.UI.Page
{
    Main Objmain = new Main(ConfigurationSettings.AppSettings["ConnectionString"].ToString());
    MedicineSellInsert theHelper = new MedicineSellInsert(ConfigurationSettings.AppSettings["ConnectionString"].ToString());
    System.Text.StringBuilder rpt = new System.Text.StringBuilder();
    String Date = DateTime.Now.ToString("dd/MM/yyyy");
    protected void Page_Load(object sender, EventArgs e)
    {
        Page.Title = "Sales/Issue Report";
        if (Session["userName"] == null)
        {
            Response.Redirect("../LoginPage.aspx");
        }
        if (Objmain.checkaccss(Session["CoCode"].ToString(), Session["UserRoleID"].ToString().Trim(), "INVOICE SELL PRICE", checkAccessType.ViewAction) == false)
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
       // DataSet dtPurchaseMedicine = theHelper.FillTable(txtissueMedicineId.Text);

        /*if ((dtPurchaseMedicine.Tables[0].Rows.Count > 0) &&  (dtPurchaseMedicine.Tables[1].Rows.Count > 0))
        {
            rpt.Append("<br/>");
            rpt.Append("<table width='900px' cellspacing=0 border=0 style='border-spacing: 0; padding: 0;font-family:verdana;'>");
            rpt.Append("<tr style='height:20px'>");
            rpt.Append("<td colspan='4' style='width: 8%;border-bottom: 1px solid black; font-family:Verdana;font-weight:bold;background-color:#9B9C8D;font-size:larger; text-align:Center'> Purchase Sell Price Details  </td>");
            rpt.Append("</tr'>");

            rpt.Append("<tr style='height:20px'>");
            rpt.Append("<td style='width: 100px; font-family:Verdana;font-weight:bold;font-size:small; text-align:left'>Supplier Name :</td>");
            rpt.AppendFormat("<td style='width: 250px; font-family:Verdana; font-size:small;text-align:left'>{0}</td>", dtPurchaseMedicine.Tables[1].Rows[0]["SCode"]);
            rpt.Append("<td style='width: 150px;font-family:Verdana;font-weight:bold;font-size:small; text-align:left'></td>");
            rpt.Append("<td style='width: 250px;font-family:Verdana;font-weight:bold;font-size:small; text-align:left'></td>");
            rpt.Append("</tr >");

            rpt.Append("<tr style='height:20px'>");
            rpt.Append("<td style='width: 100px;font-family:Verdana;font-weight:bold;font-size:small; text-align:left'>Bill Date :</td>");
            rpt.AppendFormat("<td style='width: 250px;font-family:Verdana; font-size:small;text-align:left'>{0}</td>", dtPurchaseMedicine.Tables[1].Rows[0]["purdate"]);
            rpt.Append("<td style='width:150px;font-family:Verdana;font-weight:bold;font-size:small; text-align:left'></td>");
            rpt.Append("<td style='width: 250px;font-family:Verdana;font-weight:bold;font-size:small; text-align:left'></td>");
            rpt.Append("</tr >");

            rpt.Append("<tr style='height:20px'>");
            rpt.Append("<td style='width: 100px;font-family:Verdana;font-weight:bold;font-size:small; text-align:left'>Invoice No :</td>");
            rpt.AppendFormat("<td style='width: 250px;font-family:Verdana; font-size:small;text-align:left'>{0}</td>", txtPurchaseMedicineId.Text);
            rpt.Append("<td style='width:150px;font-family:Verdana;font-weight:bold;font-size:small; text-align:right'>Date :</td>");
            rpt.AppendFormat("<td style='width:250px;font-family:Verdana;font-weight:bold;font-size:small; text-align:left'>{0}</td>", DateTime.Now.ToString("dd/MM/yyyy"));
            rpt.Append("</tr >");
            rpt.Append("</table >");

            rpt.Append("<table width='900px' cellspacing=0 border=0 style='border:1px solid black;border-spacing: 0; padding: 0;font-family:verdana;'>");
            rpt.Append("<tr style='height:20px'>");
            rpt.Append("<td colspan='5' style='width: 8%;border-bottom: 1px solid black; font-family:Verdana;font-weight:bold;background-color:#9B9C8D;font-size:larger; text-align:Center'>Details </td>");
            rpt.Append("</tr'>");

            rpt.Append("<tr style='height:25px'>");
            rpt.Append("<td style='font-family:Verdana;font-weight:bold;font-size:small; text-align:center'><u>Item Description</u></td>");
            rpt.Append("<td style='font-family:Verdana;font-weight:bold;font-size:small; text-align:center'><u>Quantity</u></td>");
            rpt.Append("<td style='font-family:Verdana;font-weight:bold;font-size:small; text-align:center'><u>Expiry Date</u></td>");
            rpt.Append("<td style='font-family:Verdana;font-weight:bold;font-size:small; text-align:center'><u>Unit Price</u></td>");
            rpt.Append("<td style='font-family:Verdana;font-weight:bold;font-size:small; text-align:center'><u>Unit Sell Price</u></td>");
           rpt.Append("</tr >");

           for (int i = 0; i < dtPurchaseMedicine.Tables[1].Rows.Count; i++)
            {
                rpt.Append("<tr style='height:25px'>");
                rpt.AppendFormat("<td style='font-family:Verdana;font-size:small; text-align:center'>{0}</td>", dtPurchaseMedicine.Tables[1].Rows[i]["MedicineName"]);
                rpt.AppendFormat("<td style='font-family:Verdana;font-size:small; text-align:center'>{0}</td>", dtPurchaseMedicine.Tables[1].Rows[i]["Qty"]);
                rpt.AppendFormat("<td style='font-family:Verdana;font-size:small; text-align:center'>{0}</td>", dtPurchaseMedicine.Tables[1].Rows[i]["ExDate"]);
                rpt.AppendFormat("<td style='font-family:Verdana;font-size:small; text-align:center'>{0}</td>", dtPurchaseMedicine.Tables[1].Rows[i]["PricePerUnit"]);
                rpt.AppendFormat("<td style='font-family:Verdana;font-size:small; text-align:center'>{0}</td>", dtPurchaseMedicine.Tables[0].Rows[i]["SellingPrice"]);
              rpt.Append("</tr >");
            }
            rpt.Append("</table >");


            //rpt.Append("<table width='900px' cellspacing=0 border=0 style='border-spacing: 0; padding: 0;font-family:verdana;'>");
            //rpt.Append("<tr style='height:25px'>");
            //rpt.Append("<td colspan='4' style='width:720px;font-family:Verdana;font-weight:bold;font-size:small; text-align:right'>Total :</td>");
            //rpt.AppendFormat("<td style='width:180px;font-family:Verdana;font-size:small;padding-right:150px; text-align:right'>{0}</td>", dtPurchaseMedicine.Rows[0]["Total"]);
            //rpt.Append("</tr >");
            //rpt.Append("</table >");

        }*/

        DataTable dtPurchaseMedicine = theHelper.IssueFillTable(TextBox5.Text, Session["CoCode"].ToString().Trim(), Session["YearCode"].ToString().Trim());

        if (dtPurchaseMedicine.Rows.Count > 0)
        {
            rpt.Append("<br/>");
            rpt.Append("<table width='900px' cellspacing=0 border=0 style='border-spacing: 0; padding: 0;font-family:verdana;'>");
            rpt.Append("<tr style='height:20px'>");
            rpt.Append("<td colspan='4' style='width: 8%;border-bottom: 1px solid black; font-family:Verdana;font-weight:bold;background-color:#9B9C8D;font-size:larger; text-align:Center'> Medicine Sale Invoice  </td>");
            rpt.Append("</tr'>");

            rpt.Append("<tr style='height:20px'>");
            rpt.Append("<td style='width: 190px; font-family:Verdana;font-weight:bold;font-size:small; text-align:left'>Invoice No :</td>");
            rpt.AppendFormat("<td style='width: 200px; font-family:Verdana; font-size:small;text-align:left'>{0}</td>", TextBox5.Text);
            rpt.Append("<td style='width: 150px;font-family:Verdana;font-weight:bold;font-size:small; text-align:left'></td>");
            rpt.Append("<td style='width: 250px;font-family:Verdana;font-weight:bold;font-size:small; text-align:left'></td>");
            rpt.Append("</tr >");

            /*rpt.Append("<tr style='height:20px'>");
            rpt.Append("<td style='width: 190px;font-family:Verdana;font-weight:bold;font-size:small; text-align:left'>Issue Date :</td>");
            rpt.AppendFormat("<td style='width: 200px;font-family:Verdana; font-size:small;text-align:left'>{0}</td>", dtPurchaseMedicine.Rows[0]["purdate"]);
            rpt.Append("<td style='width:150px;font-family:Verdana;font-weight:bold;font-size:small; text-align:left'></td>");
            rpt.Append("<td style='width: 250px;font-family:Verdana;font-weight:bold;font-size:small; text-align:left'></td>");
            rpt.Append("</tr >");*/

            rpt.Append("<tr style='height:20px'>");
            rpt.Append("<td style='width: 190px;font-family:Verdana;font-weight:bold;font-size:small; text-align:left'>Work Station / Wings : </td>");
            rpt.AppendFormat("<td style='width: 200px;font-family:Verdana; font-size:small;text-align:left'>{0}</td>", dtPurchaseMedicine.Rows[0]["WingName"]);
            rpt.Append("<td style='width:150px;font-family:Verdana;font-weight:bold;font-size:small; text-align:left'></td>");
            rpt.Append("<td style='width: 250px;font-family:Verdana;font-weight:bold;font-size:small; text-align:left'></td>");
            rpt.Append("</tr >");

            rpt.Append("<tr style='height:20px'>");
            rpt.Append("<td style='width: 190px;font-family:Verdana;font-weight:bold;font-size:small; text-align:left'>Patient's / Customer Name :</td>");
            rpt.AppendFormat("<td style='width: 200px;font-family:Verdana; font-size:small;text-align:left'>{0}</td>", dtPurchaseMedicine.Rows[0]["PatientFullname"]);
            rpt.Append("<td style='width:150px;font-family:Verdana;font-weight:bold;font-size:small; text-align:left'></td>");
            rpt.Append("<td style='width: 250px;font-family:Verdana;font-weight:bold;font-size:small; text-align:left'></td>");
            rpt.Append("</tr >");


            rpt.Append("<tr style='height:20px'>");
            rpt.Append("<td style='width: 190px;font-family:Verdana;font-weight:bold;font-size:small; text-align:left'>Issue By :</td>");
            rpt.AppendFormat("<td style='width: 200px;font-family:Verdana; font-size:small;text-align:left'>{0}</td>", dtPurchaseMedicine.Rows[0]["Issueby"]);
            rpt.Append("<td style='width:150px;font-family:Verdana;font-weight:bold;font-size:small; text-align:left'>Issue Date :</td>");
            rpt.AppendFormat("<td style='width: 250px;font-family:Verdana;font-weight:bold;font-size:small; text-align:right'>{0}</td>", dtPurchaseMedicine.Rows[0]["purdate"]);
            rpt.Append("</tr >");

            rpt.Append("<tr style='height:20px'>");
            rpt.Append("<td style='width: 190px;font-family:Verdana;font-weight:bold;font-size:small; text-align:left'>Received By :</td>");
            rpt.AppendFormat("<td style='width: 200px;font-family:Verdana; font-size:small;text-align:left'>{0}</td>", dtPurchaseMedicine.Rows[0]["ReceiveBy"]);
            rpt.Append("<td style='width:150px;font-family:Verdana;font-weight:bold;font-size:small; text-align:left'>Received Date :</td>");
            rpt.AppendFormat("<td style='width: 250px;font-family:Verdana;font-weight:bold;font-size:small; text-align:right'>{0}</td>", dtPurchaseMedicine.Rows[0]["rcvdt"]);
            rpt.Append("</tr >");

            rpt.Append("</table >");

            rpt.Append("<table width='900px' cellspacing=0 border=0 style='border:1px solid black;border-spacing: 0; padding: 0;font-family:verdana;'>");
            rpt.Append("<tr style='height:20px'>");
            rpt.Append("<td colspan='7' style='width: 8%;border-bottom: 1px solid black; font-family:Verdana;font-weight:bold;background-color:#9B9C8D;font-size:larger; text-align:Center'>** Details **</td>");
            rpt.Append("</tr'>");

            /*rpt.Append("</table >");*/

            

            rpt.Append("<tr style='height:25px'>");
            rpt.Append("<td style='font-family:Verdana;font-weight:bold;font-size:small; text-align:center'><u>Manufacturer</u></td>");
            rpt.Append("<td style='font-family:Verdana;font-weight:bold;font-size:small; text-align:center'><u> Group</u></td>");
            rpt.Append("<td style='font-family:Verdana;font-weight:bold;font-size:small; text-align:center'><u> Sub Group</u></td>");
            rpt.Append("<td style='font-family:Verdana;font-weight:bold;font-size:small; text-align:center'><u>Item Description</u></td>");
            rpt.Append("<td style='font-family:Verdana;font-weight:bold;font-size:small; text-align:center'><u>Quantity</u></td>");
            rpt.Append("<td style='font-family:Verdana;font-weight:bold;font-size:small; text-align:center'><u>Batch No</u></td>");
            rpt.Append("<td style='font-family:Verdana;font-weight:bold;font-size:small; text-align:center'><u>Expiry Date</u></td>");
            //rpt.Append("<td style='font-family:Verdana;font-weight:bold;font-size:small; text-align:center'><u>Unit Price</u></td>");
            //rpt.Append("<td style='font-family:Verdana;font-weight:bold;font-size:small; text-align:center'><u>Gross Amount</u></td>");
            rpt.Append("</tr >");

            for (int i = 0; i < dtPurchaseMedicine.Rows.Count; i++)
            {
                rpt.Append("<tr style='height:25px'>");
                rpt.AppendFormat("<td style='font-family:Verdana;font-size:small; text-align:center'>{0}</td>", dtPurchaseMedicine.Rows[i]["MName"]);
                rpt.AppendFormat("<td style='font-family:Verdana;font-size:small; text-align:center'>{0}</td>", dtPurchaseMedicine.Rows[i]["MedicineGroupName"]);
                rpt.AppendFormat("<td style='font-family:Verdana;font-size:small; text-align:center'>{0}</td>", dtPurchaseMedicine.Rows[i]["SubGrName"]);
                rpt.AppendFormat("<td style='font-family:Verdana;font-size:small; text-align:center'>{0}</td>", dtPurchaseMedicine.Rows[i]["MedicineName"]);
                rpt.AppendFormat("<td style='font-family:Verdana;font-size:small; text-align:center'>{0}</td>", dtPurchaseMedicine.Rows[i]["IQTY"]);
                rpt.AppendFormat("<td style='font-family:Verdana;font-size:small; text-align:center'>{0}</td>", dtPurchaseMedicine.Rows[i]["BATCHNO"]);
                rpt.AppendFormat("<td style='font-family:Verdana;font-size:small; text-align:center'>{0}</td>", dtPurchaseMedicine.Rows[i]["exdate"]);
                //rpt.AppendFormat("<td style='font-family:Verdana;font-size:small; text-align:center'>{0}</td>", dtPurchaseMedicine.Rows[i]["PricePerUnit"]);
                //rpt.AppendFormat("<td style='font-family:Verdana;font-size:small;padding-right:150px; text-align:right'>{0}.00</td>", dtPurchaseMedicine.Rows[i]["TotalPrice"]);
                rpt.Append("</tr >");
            }
           // rpt.Append("</table >");
            rpt.Append("</table >");


            /*rpt.Append("<table width='900px' cellspacing=0 border=0 style='border-spacing: 0; padding: 0;font-family:verdana;'>");
            rpt.Append("<tr style='height:25px'>");
            rpt.Append("<td colspan='5' style='width:720px;font-family:Verdana;font-weight:bold;font-size:small; text-align:right'>Total :</td>");
            rpt.AppendFormat("<td style='width:180px;font-family:Verdana;font-size:small;padding-right:150px; text-align:right'>{0}.00</td>", dtPurchaseMedicine.Rows[0]["Total"]);
            rpt.Append("</tr >");

            rpt.Append("<tr style='height:25px'>");
            rpt.Append("<td  colspan='5' style='width:720px;font-family:Verdana;font-weight:bold;font-size:small; text-align:right'>Discount :</td>");
            rpt.AppendFormat("<td style='width:180px;font-family:Verdana;font-size:small;padding-right:150px; text-align:right'>{0}.00</td>", dtPurchaseMedicine.Rows[0]["Discount"]);
            rpt.Append("</tr >");
            rpt.Append("</table >");

            rpt.Append("<table width='900px' cellspacing=0 border=0 style='border:1px solid black;border-spacing: 0; padding: 0;font-family:verdana;'>");
            double net = Convert.ToDouble(dtPurchaseMedicine.Rows[0]["Total"]); //- Convert.ToDouble(dtPurchaseMedicine.Rows[0]["Discount"]);
            rpt.Append("<tr style='height:25px'>");

            rpt.Append("<td colspan='5' style='width:720px;font-family:Verdana;font-weight:bold;font-size:small; text-align:right'>Net Amount :</td>");
            rpt.AppendFormat("<td style='width:180px;font-family:Verdana;font-size:small; padding-right:150px;text-align:right'>{0}.00</td>", net);
            rpt.Append("</tr >");
            rpt.Append("</table >");*/
        }
        ltrReport.Visible = true;

    }
}