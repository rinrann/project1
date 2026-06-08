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

public partial class Reagent_MedicineStockDetails : System.Web.UI.Page
{
    ExpiryAlertAndStockReport theHelper = new ExpiryAlertAndStockReport(ConfigurationSettings.AppSettings["ConnectionString"].ToString());
    string Expiryalert = ConfigurationSettings.AppSettings["Expirydate"].ToString();

    System.Text.StringBuilder rpt = new System.Text.StringBuilder();

    protected void Page_Load(object sender, EventArgs e)
    {
        Page.Title = "Reagent Stock Details";
        if (!IsPostBack)
        {

            GetReport();
            RadioButtonList1.SelectedValue = "With Header";

        }
    }


    public void GetReport()
    {
        Report_Header();
        GetHearder_Detail();

        ltrReport.Text = rpt.ToString();

    }


    protected void reagentchange(object sender, System.EventArgs e)
    {

        
        GetReport();
    }

    protected void frmdt_textChange(object sender, EventArgs e)
    {
        GetReport();
    }

    protected void todt_textChange(object sender, EventArgs e)
    {
        GetReport();
    }
    public void Report_Header()
    {
        rpt.Append("<table width='900px;' cellspacing=0 border=0 style='border:1px solid black;background-color:#9B9C8D;border-spacing: 0; padding: 0;font-family:verdana;'>");
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
        DateTime now = DateTime.Now.AddDays(Convert.ToInt32(Expiryalert));

        string frmdate = "";
        string todate = "";
        if (frmdt.Text != "")
        {
            string[] aa = frmdt.Text.Split('/');
            string fday = aa[0];
            string fmonth = aa[1];
            string fyear = aa[2];
            if (fday.Length == 1)
                fday = "0" + fday;
            if (fmonth.Length == 1)
                fmonth = "0" + fmonth;
            // frmdate = fday + "/" + fmonth + "/" + fyear;
            frmdate = fyear + fmonth + fday;
        }
        else
            frmdate = "";

        if (todt.Text != "")
        {
            string[] aa = todt.Text.Split('/');
            string tday = aa[0];
            string tmonth = aa[1];
            string tyear = aa[2];
            if (tday.Length == 1)
                tday = "0" + tday;
            if (tmonth.Length == 1)
                tmonth = "0" + tmonth;
            //todate = tday + "/" + tmonth + "/" + tyear;
            todate = tyear + tmonth + tday;
        }
        else
            todate = "";


        DataTable dt = theHelper.GetReagentStockDetails(frmdate, todate, Session["CoCode"].ToString().Trim(), Session["YearCode"].ToString().Trim(), TextBoxicode.Text.Trim());
        string suplr;
        DataTable suptbl;
        DataTable rettbl;
        DataTable issuetbl;
        if (dt.Rows.Count > 0)
        {
            rpt.Append("<br/>");
            rpt.Append("<table width='900px' cellspacing=0 border=0 style='border:1px solid black;border-spacing: 0; padding: 0;font-family:verdana;'>");
            rpt.Append("<tr style='height:20px'>");
            rpt.Append("<td colspan='10' style='width: 8%;border-bottom: 1px solid black; font-family:Verdana;font-weight:bold;background-color:#9B9C8D;font-size:larger; text-align:Center'> Reagent Stock Details  </td>");
            rpt.Append("</tr'>");

            rpt.Append("<tr style='height:20px'>");
            rpt.Append("<td style='width: 250px; font-family:Verdana;font-weight:bold;font-size:small;border-right:1px solid black;text-align:center'>Supplier</td>");
            //rpt.Append("<td style='width: 100px; font-family:Verdana;font-weight:bold;font-size:small;border-right:1px solid black;text-align:center'>Manufacturing Company</td>");
            rpt.Append("<td style='width: 250px;font-family:Verdana;font-weight:bold;font-size:small;border-right:1px solid black; text-align:center'>Reagent Name</td>");
            rpt.Append("<td style='width: 100px;font-family:Verdana;font-weight:bold;font-size:small;border-right:1px solid black; text-align:center'>Purchase Date</td>");
            rpt.Append("<td style='width: 100px;font-family:Verdana;font-weight:bold;font-size:small;border-right:1px solid black; text-align:center'>Purchase Quantity</td>");
            rpt.Append("<td style='width: 100px;font-family:Verdana;font-weight:bold;font-size:small;border-right:1px solid black; text-align:center'>Issue/Sale Quantity</td>");
            //rpt.Append("<td style='width: 100px;font-family:Verdana;font-weight:bold;font-size:small;border-right:1px solid black; text-align:center'>Return Quantity</td>");
            rpt.Append("<td style='width: 100px;font-family:Verdana;font-weight:bold;font-size:small;text-align:center'>Remaining Qty</td>");
            rpt.Append("</tr >");
            string icode = "";
            string previcode = "";
            double selqty;
            double retqty;
            double stock;
            for (int i = 0; i < dt.Rows.Count; i++)
            {

                suptbl = theHelper.GetSupplier(dt.Rows[i]["SLCODE"].ToString(), Session["CoCode"].ToString().Trim());
                if (suptbl.Rows.Count > 0)
                {
                    suplr = suptbl.Rows[0]["SName"].ToString();
                }
                else
                {
                    suplr = "";
                }

                rettbl = theHelper.GetRetQty(dt.Rows[i]["ICODE"].ToString(), Session["CoCode"].ToString().Trim(), Session["YearCode"].ToString().Trim(), dt.Rows[i]["BatchNo"].ToString(), dt.Rows[i]["ICODE"].ToString());
                if (rettbl.Rows.Count > 0)
                {
                    retqty = Convert.ToDouble(rettbl.Rows[0]["retqty"]);
                }
                else
                {
                    retqty = 0.00;
                }

                issuetbl = theHelper.GetIssueQty(dt.Rows[i]["ICODE"].ToString(), Session["CoCode"].ToString().Trim(), Session["YearCode"].ToString().Trim(), dt.Rows[i]["BatchNo"].ToString(), dt.Rows[i]["ICODE"].ToString());
                if (issuetbl.Rows.Count > 0)
                {
                    selqty = Convert.ToDouble(issuetbl.Rows[0]["issueqty"]);
                }
                else
                {
                    selqty = 0.00;
                }
                stock = (Convert.ToDouble(dt.Rows[i]["PurchaseQty"]) - (retqty + selqty));
                icode = dt.Rows[i]["ICODE"].ToString();
                rpt.Append("<tr style='height:25px'>");
                rpt.AppendFormat("<td style='font-family:Verdana;font-size:small;border-right:1px solid black;border-top:1px solid black; text-align:left;'>{0}</td>", suplr);
                rpt.AppendFormat("<td style='font-family:Verdana;font-size:small;border-right:1px solid black;border-top:1px solid black; text-align:left;'>{0}</td>", dt.Rows[i]["INAME"]);
                //rpt.AppendFormat("<td style='font-family:Verdana;font-size:small;border-right:1px solid black;border-top:1px solid black; text-align:left;'>{0}</td>", dt.Rows[i]["MedicineName"]);
                rpt.AppendFormat("<td style='font-family:Verdana;font-size:small;border-right:1px solid black;border-top:1px solid black; text-align:right;'>{0}</td>", dt.Rows[i]["purchaseDate"]);
                rpt.AppendFormat("<td style='font-family:Verdana;font-size:small;border-right:1px solid black;border-top:1px solid black; text-align:right;'>{0}</td>", dt.Rows[i]["PurchaseQty"]);
                //if(icode==previcode){selqty="";retqty="";stock="";}else{selqty=dt.Rows[i]["SaleQty"].ToString();retqty=dt.Rows[i]["ReturnQty"].ToString();stock=dt.Rows[i]["curstock"].ToString();}

                rpt.AppendFormat("<td style='font-family:Verdana;font-size:small;border-right:1px solid black;border-top:1px solid black; text-align:right;'>{0}</td>", selqty);
                //rpt.AppendFormat("<td style='font-family:Verdana;font-size:small;border-right:1px solid black;border-top:1px solid black; text-align:right;'>{0}</td>", retqty);
                rpt.AppendFormat("<td style='font-family:Verdana;font-size:small;border-top:1px solid black; text-align:right;'>{0}</td>", stock);
                rpt.Append("</tr >");
                previcode = icode;
            }

            rpt.Append("</table >");
        }
        ltrReport.Visible = true;
    }


    public void CheckRadio()
    {
        if (RadioButtonList1.SelectedValue == "With Header")
        {
            ltrReport.Text = "";
            GetReport();
            ltrReport.Text = rpt.ToString();
        }
        else
        {
            ltrReport.Text = "";
            GetHearder_Detail();
            ltrReport.Text = rpt.ToString();
        }
    }
    protected void RadioButtonList1_SelectedIndexChanged(object sender, System.EventArgs e)
    {
        CheckRadio();
    }

    [System.Web.Script.Services.ScriptMethod()]
    [System.Web.Services.WebMethod]
    public static List<string> SearchReagentName(string prefixText, int count)
    {
        using (SqlConnection conn = new SqlConnection())
        {
            conn.ConnectionString = ConfigurationSettings.AppSettings["ConnectionString"].ToString();
            using (SqlCommand cmd = new SqlCommand())
            {
                cmd.CommandText = "select distinct it.iname + '~' + ltrim(rtrim(it.icode)) as Name from ITEMMAST it,PH_ReagentMaster im where im.rcode=it.icode and it.itype='G' and it.iname like @SearchText +'%'";
                cmd.Parameters.AddWithValue("@SearchText", prefixText);
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
}