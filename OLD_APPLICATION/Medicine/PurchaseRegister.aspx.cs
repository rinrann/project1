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

public partial class Medicine_PurchaseRegister : System.Web.UI.Page
{
    Main Objmain = new Main(ConfigurationSettings.AppSettings["ConnectionString"].ToString());
    PH_Report objreport = new PH_Report(ConfigurationSettings.AppSettings["ConnectionString"].ToString());
    System.Text.StringBuilder rpt = new System.Text.StringBuilder();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["userName"] == null)
        {
            Response.Redirect("../LoginPage.aspx");
        }
        if (Objmain.checkaccss(Session["CoCode"].ToString(), Session["UserRoleID"].ToString().Trim(), "Purchase Register", checkAccessType.ViewAction) == false)
        {
            Response.Redirect("../AccessDenied.aspx");
        }
        if (!IsPostBack)
        { 
            txtfromdt.Text = DateTime.Now.ToString("yyyy-MM-dd");
            txttodt.Text = DateTime.Now.ToString("yyyy-MM-dd"); 
            ddlsupplier.DataSource = objreport.GetdropdownSuppilier();           
            ddlsupplier.DataValueField = "scode";
            ddlsupplier.DataTextField = "SName";
            ddlsupplier.DataBind();

            ddlitems.DataSource = objreport.GetdropdownItems();
            ddlitems.DataValueField = "MedicineId";
            ddlitems.DataTextField = "MedicineName";
            ddlitems.DataBind();
        }
    }


 
 
    protected void Button1_Click(object sender, EventArgs e)
    {
        System.Globalization.DateTimeFormatInfo dtf = new DateTimeFormatInfo();
        string supplier = "";
        string item = "";
        if (ddlsupplier.SelectedValue.Trim() != "")
            supplier = ddlsupplier.SelectedValue.Trim();
        if (ddlitems.SelectedValue.Trim() != "0")
            item = ddlitems.SelectedValue.Trim();

        Session["dt"] = objreport.GetPurchaseRegister(txtfromdt.Text, txttodt.Text, supplier, item);
 

        bind_report();
        divregister.Visible = true;
        //Response.Redirect("View_SaleRegister.aspx");
    }

    public void bind_report()
    {
        DataTable dt = (DataTable)HttpContext.Current.Session["dt"];
        string oldinv = "";
        string newinv = "";
        rpt.Append("<table width='130%' cellspacing=0 border=0 style=' border-spacing: 0; padding: 0;font-family:verdana;'>");
        rpt.Append("<tr cellpadding:'0'>");
        rpt.AppendFormat("<td rowspan='5' style='width: 35%; font-family:Courier New; font-weight:bold;text-align:left'><img src='../Images/logo.jpg'  style='height:87px;width:205px;border-style:inherit;border-color:#D3CDD4'/></td>");
        rpt.AppendFormat("<td style='width: 35%; font-family:Courier New; font-size:large; font-weight:bold;text-align:center'>{0}</td>", "");
        rpt.AppendFormat("<td style='width: 35%; font-family:Courier New; font-size:x-Medium; font-weight:bold;text-align:center'>&nbsp;</td>");
        rpt.Append("</tr>");


        rpt.Append("<tr style='height:20px'>");
        rpt.Append("<td style='width: 8%;  font-weight:bold; text-align:Center;padding-bottom:10px;font-size:10pt;'><br/> <u>PURCHASE REGISTER</u></td>");
        rpt.Append("</tr'>");
        rpt.Append("</table>");



        rpt.Append("<table  width='130%' cellspacing=0 cellpadding=0 border=0 style='border-top:solid 1px gray;border-right:solid 1px gray;border-left:solid 1px gray;border-bottom:solid 1px gray; padding: 0;font-family:verdana;font-size:8pt;'>");
        rpt.Append("<tr>");
        rpt.Append("<td style='border-bottom:solid 1px gray;text-align:left'>Date</td>");
        rpt.Append("<td style='border-bottom:solid 1px gray;text-align:left'>Party Bill No</td>");
        rpt.Append("<td style='border-bottom:solid 1px gray;text-align:left'>Supplier Name</td>"); 
        rpt.Append("<td style='border-bottom:solid 1px gray;text-align:left'>Products / Items</td>");
        rpt.Append("<td style='border-bottom:solid 1px gray;text-align:left'>Batch No.</td>");
        rpt.Append("<td style='border-bottom:solid 1px gray;text-align:left'>Expiry Dt</td>");
        rpt.Append("<td style='border-bottom:solid 1px gray;text-align:left'>HSN / SAC</td>");
        rpt.Append("<td style='border-bottom:solid 1px gray;text-align:left'>Unit</td>");
        rpt.Append("<td style='border-bottom:solid 1px gray;text-align:right'>Unit Rate</td>");
        rpt.Append("<td style='border-bottom:solid 1px gray;text-align:right'>Item Qty</td>");
        rpt.Append("<td style='border-bottom:solid 1px gray;text-align:right'>Taxable Value</td>");
        rpt.Append("<td style='border-bottom:solid 1px gray;text-align:right'>CGST %</td>");
        rpt.Append("<td style='border-bottom:solid 1px gray;text-align:right'>SGST %</td>");
        rpt.Append("<td style='border-bottom:solid 1px gray;text-align:right'>CGST (Rs.)</td>");
        rpt.Append("<td style='border-bottom:solid 1px gray;text-align:right'>SGST (Rs.)</td>");
        rpt.Append("<td style='border-bottom:solid 1px gray;text-align:right'>Amount</td>");
        rpt.Append("<td style='border-bottom:solid 1px gray;text-align:right'>Rounded Off</td>");
        rpt.Append("<td style='border-bottom:solid 1px gray;text-align:right'>Total Value </td>"); 
        rpt.Append("</tr>");

        for (int i = 0; i < dt.Rows.Count; i++)
        {
            newinv = dt.Rows[i]["PartybillNo"].ToString().Trim();
            if (oldinv != newinv)
            {
                rpt.Append("<tr>");
                rpt.Append("<td style='text-align:left;font-weight:bold'>" + dt.Rows[i]["Datesshow"].ToString() + "</td>");
                rpt.Append("<td style='text-align:left;font-weight:bold'>" + dt.Rows[i]["PartybillNo"].ToString() + "</td>");
                rpt.Append("<td style='text-align:left;font-weight:bold'>" + dt.Rows[i]["SupplierName"].ToString() + "</td>"); 
                rpt.Append("</tr>");
            }


            if (dt.Rows[i]["ICode"].ToString().Trim() == "ZZZZZZZZZZ")
            {
                rpt.Append("<tr>");
                rpt.Append("<td   style='border-top:solid 1px gray;border-bottom:solid 1px gray;'>&nbsp;</td>"); 
                rpt.Append("<td colspan='8' style='border-top:solid 1px gray;border-bottom:solid 1px gray;font-weight:bold;text-align:left'>" + dt.Rows[i]["IName"].ToString() + "</td>");
 
                rpt.Append("<td style='border-top:solid 1px gray;border-bottom:solid 1px gray;font-weight:bold;text-align:right'>" + dt.Rows[i]["MedQty"].ToString() + "</td>");
                rpt.Append("<td style='border-top:solid 1px gray;border-bottom:solid 1px gray;font-weight:bold;text-align:right'>" + dt.Rows[i]["TaxableVal"].ToString() + "</td>");
                rpt.Append("<td style='border-top:solid 1px gray;border-bottom:solid 1px gray;font-weight:bold;text-align:right'>" + dt.Rows[i]["CgstPer"].ToString() + "</td>");
                rpt.Append("<td style='border-top:solid 1px gray;border-bottom:solid 1px gray;font-weight:bold;text-align:right'>" + dt.Rows[i]["SgstPer"].ToString() + "</td>");
                rpt.Append("<td style='border-top:solid 1px gray;border-bottom:solid 1px gray;font-weight:bold;text-align:right'>" + dt.Rows[i]["CgstAmt"].ToString() + "</td>");
                rpt.Append("<td style='border-top:solid 1px gray;border-bottom:solid 1px gray;font-weight:bold;text-align:right'>" + dt.Rows[i]["SgstAmt"].ToString() + "</td>");
                rpt.Append("<td style='border-top:solid 1px gray;border-bottom:solid 1px gray;font-weight:bold;text-align:right'>" + dt.Rows[i]["TotAmt"].ToString() + "</td>");
                rpt.Append("<td style='border-top:solid 1px gray;border-bottom:solid 1px gray;font-weight:bold;text-align:right'>" + dt.Rows[i]["Roundedoff"].ToString() + "</td>");
                rpt.Append("<td style='border-top:solid 1px gray;border-bottom:solid 1px gray;font-weight:bold;text-align:right'>" + dt.Rows[i]["TotInvoiceVal"].ToString() + "</td>"); 
                rpt.Append("</tr>");
            }
            else
            {
                string code = dt.Rows[i]["ICode"].ToString().Trim() == "ZZZZZZ" ? "" : dt.Rows[i]["ICode"].ToString().Trim() + " - ";
                rpt.Append("<tr>");
                rpt.Append("<td colspan='3'>&nbsp;</td>"); 
                rpt.Append("<td style='text-align:left;padding-left:10px'>" + code + dt.Rows[i]["Iname"].ToString().Trim() + "</td>");
                rpt.Append("<td style='text-align:left'>" + dt.Rows[i]["Batchno"].ToString() + "</td>");
                rpt.Append("<td style='text-align:left'>" + dt.Rows[i]["ExpiryDt"].ToString() + "</td>");
                rpt.Append("<td style='text-align:left'>" + dt.Rows[i]["HSNSAC"].ToString() + "</td>");
                rpt.Append("<td style='text-align:left'>" + dt.Rows[i]["Unit"].ToString() + "</td>");
                rpt.Append("<td style='text-align:right'>" + dt.Rows[i]["UnitRate"].ToString() + "</td>");
                rpt.Append("<td style='text-align:right'>" + dt.Rows[i]["MedQty"].ToString() + "</td>");
                rpt.Append("<td style='text-align:right'>" + dt.Rows[i]["TaxableVal"].ToString() + "</td>");
                rpt.Append("<td style='text-align:right'>" + dt.Rows[i]["CgstPer"].ToString() + "</td>");
                rpt.Append("<td style='text-align:right'>" + dt.Rows[i]["SgstPer"].ToString() + "</td>");
                rpt.Append("<td style='text-align:right'>" + dt.Rows[i]["CgstAmt"].ToString() + "</td>");
                rpt.Append("<td style='text-align:right'>" + dt.Rows[i]["SgstAmt"].ToString() + "</td>");
                rpt.Append("<td style='text-align:right'>" + dt.Rows[i]["TotAmt"].ToString() + "</td>");
                rpt.Append("<td style='text-align:right'>&nbsp;</td>");
                rpt.Append("<td style='text-align:right'>&nbsp;</td>"); 
                rpt.Append("</tr>");
            }

            oldinv = newinv;
        }
        rpt.Append("</table>");




        ltrReport.Text = rpt.ToString();

    }
    protected void cmd_excel_Click(object sender, EventArgs e)
    {
        Response.Clear();
        Response.Buffer = true;
        Response.ContentType = "application/vnd.ms-excel";
        Response.Charset = "";
        Response.AppendHeader("content-disposition", "attachment; filename=PurchaseRegister.xls");
        Response.Cache.SetCacheability(HttpCacheability.NoCache);
        this.EnableViewState = false;
        System.IO.StringWriter oStringWriter = new System.IO.StringWriter();
        System.Web.UI.HtmlTextWriter oHtmlTextWriter = new System.Web.UI.HtmlTextWriter(oStringWriter);
        ltrReport.RenderControl(oHtmlTextWriter);
        Response.Write(oStringWriter.ToString());
        Response.End();
    }

    protected void cmdBack_Click(object sender, EventArgs e)
    {
        Response.Redirect("../HomePage.aspx");
    }
}