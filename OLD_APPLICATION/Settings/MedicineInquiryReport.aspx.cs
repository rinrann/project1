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
//using iTextSharp.text;
using iTextSharp.text.pdf;
using iTextSharp.text.html;
using iTextSharp.text.html.simpleparser;
using System.IO;

public partial class Assignment_MedicineInquiryReport : System.Web.UI.Page
{
    Main Objmain = new Main(ConfigurationSettings.AppSettings["ConnectionString"].ToString());
    MedicineInquireReportClass theHelper = new MedicineInquireReportClass(ConfigurationSettings.AppSettings["ConnectionString"].ToString());
    System.Text.StringBuilder rpt = new System.Text.StringBuilder();

    protected void Page_Load(object sender, EventArgs e)
    {
        Page.Title = "MEDICINE ENQ REPORT";
        if (Session["userName"] == null)
        {
            Response.Redirect("../LoginPage.aspx");
        }
        if (Objmain.checkaccss(Session["CoCode"].ToString(), Session["UserRoleID"].ToString().Trim(), "MEDICINE ENQ REPORT", checkAccessType.ViewAction) == false)
        {
            Response.Redirect("../AccessDenied.aspx");
        }

        if (!IsPostBack)
        {
            DropDownFill();
            Panel1.Visible = false;
            RadioButtonList1.SelectedValue = "With Header";
            btnBack.Visible = false; btnPDF.Visible = false; cmdPrint.Visible = false;



        }

    }

    public void DropDownFill()
    {

        this.ddlMedicineGroup.Items.Clear();
        this.ddlMedicineGroup.DataSource = theHelper.MedicineGroup();
        this.ddlMedicineGroup.DataTextField = "MedicineGroupName";
        this.ddlMedicineGroup.DataValueField = "MedicineGroupID";
        this.ddlMedicineGroup.DataBind();
        this.ddlMedicineGroup.Items.Insert(0, new ListItem("--Select--", "0"));



        this.ddlMedicineSubGroup.Items.Clear();
        this.ddlMedicineSubGroup.DataSource = theHelper.MedicineSubGroup(ddlMedicineGroup.SelectedValue.ToString());
        this.ddlMedicineSubGroup.DataTextField = "SubGrName";
        this.ddlMedicineSubGroup.DataValueField = "ID";
        this.ddlMedicineSubGroup.DataBind();
        this.ddlMedicineSubGroup.Items.Insert(0, new ListItem("--Select--", "0"));

        this.ddlMedicineName.Items.Insert(0, new ListItem("--Select--", "0"));

    }
    protected void ddlMedicineGroup_SelectedIndexChanged(object sender, EventArgs e)
    {
        this.ddlMedicineSubGroup.Items.Clear();
        this.ddlMedicineSubGroup.DataSource = theHelper.MedicineSubGroup(ddlMedicineGroup.SelectedValue.ToString());
        this.ddlMedicineSubGroup.DataTextField = "SubGrName";
        this.ddlMedicineSubGroup.DataValueField = "ID";
        this.ddlMedicineSubGroup.DataBind();
        this.ddlMedicineSubGroup.Items.Insert(0, new ListItem("--Select--", "0"));
    }

    protected void ddlMedicineSubGroup_SelectedIndexChanged(object sender, EventArgs e)
    {
        this.ddlMedicineName.Items.Clear();
        this.ddlMedicineName.DataSource = theHelper.MedicineName(ddlMedicineGroup.SelectedValue.ToString(), ddlMedicineSubGroup.SelectedValue.ToString());
        this.ddlMedicineName.DataTextField = "MedicineName";
        this.ddlMedicineName.DataValueField = "MedicineID";
        this.ddlMedicineName.DataBind();
        this.ddlMedicineName.Items.Insert(0, new ListItem("--Select--", "0"));
    }


    //public void Report_Header()
    //{
    //    rpt.Append("<table width='100%' cellspacing=0 border=0 style='border:1px solid black;border-spacing: 0; padding: 0;font-family:verdana;'>");
    //    rpt.Append("<tr cellpadding:'0'  style='height:30px;'>");
    //    rpt.Append("<td rowspan='3' width='2%' style='font-family:Verdana; font-size:x-large; font-weight:bold;padding-left:100px;text-align:left'><img src='/Images/gfclogo.png'  style='height:77px;width:75px;'/></td>");
    //    rpt.Append("<td   width='20%'  style='height:20px;font-family:Verdana; font-size:x-large; font-weight:bold;text-align:center'><u>GFC HOSPITAL</u></td>");
    //    rpt.Append("<td rowspan='3'  width='2%' style='height:20px;font-family:Verdana; font-size:x-large;padding-right:100px; font-weight:bold;text-align:right'><img src='/Images/gfclogo.png'  style='height:77px;width:75px;'/></td>");
    //    rpt.Append("</tr>");

    //    rpt.Append("<tr  style='height:20px;'>");
    //    rpt.AppendFormat("<td   width='20%'  style='height:10px;font-family:Verdana; font-size:small; font-weight:bold;text-align:center'>(Regn. No : NH-315/G-70/2013)</td>");
    //    rpt.Append("</tr>");

    //    rpt.Append("<tr  style='height:10px;'>");
    //    rpt.AppendFormat("<td  width='70%'  style='height:15px;font-family:Verdana; font-size:medium; font-weight:bold;text-align:center'>Kushpata, Ghatal, Paschim Medinipur, WB, 721212</td>");

    //    rpt.Append("</tr>");
    //    rpt.Append("</table>");
    //}

    public void Report_Header()
    {
        rpt.Append("<table width='100%' cellspacing=0 border=0 style='border:1px solid black;border-spacing: 0; padding: 0;font-family:verdana;'>");
        rpt.Append("<tr cellpadding:'0'  style='height:30px;'>");
        rpt.Append("<td rowspan='3' width='2%' style='font-family:Verdana; font-size:x-large; font-weight:bold;padding-left:100px;text-align:left'><img src='../photo/" + Session["Logopath"].ToString().Trim() + "'  style='height:77px;width:75px;'/></td>");
        rpt.Append("<td   width='20%'  style='height:20px;font-family:Verdana; font-size:x-large; font-weight:bold;text-align:center'><u>" + Session["CoName"].ToString().Trim() + "</u></td>");
        rpt.Append("<td rowspan='3'  width='2%' style='height:20px;font-family:Verdana; font-size:x-large;padding-right:100px; font-weight:bold;text-align:right'><img src='../photo/" + Session["Logopath2"].ToString().Trim() + "'  style='height:77px;width:75px;'/></td>");
        rpt.Append("</tr>");

        rpt.Append("<tr  style='height:20px;'>");
        rpt.AppendFormat("<td   width='20%'  style='height:10px;font-family:Verdana; font-size:small; font-weight:bold;text-align:center'>(Regn. No : " + Session["HosRegnNo"].ToString().Trim() + ")</td>");
        rpt.Append("</tr>");

        rpt.Append("<tr  style='height:10px;'>");
        rpt.AppendFormat("<td  width='70%'  style='height:15px;font-family:Verdana; font-size:medium; font-weight:bold;text-align:center'>" + Session["ADDR"].ToString().Trim() + "</td>");

        rpt.Append("</tr>");
        rpt.Append("</table>");
    }

    public void GetHearder_PriceDetail()
    {
        int i;
        System.Globalization.DateTimeFormatInfo dtf = new DateTimeFormatInfo();
        string date1, date2;
        if (txtFromDate.Text != "")
        {
            DateTime testdate = DateTime.ParseExact(txtFromDate.Text, "dd/MM/yyyy", dtf);
            date1 = testdate.ToString("yyyy-MM-dd");
        }
        else
        {
            date1 = "null";
        }

        if (txtToDate.Text != "")
        {
            DateTime testdate = DateTime.ParseExact(txtToDate.Text, "dd/MM/yyyy", dtf);
            date2 = testdate.ToString("yyyy-MM-dd");
        }
        else
        {
            date2 = "null";
        }

        DataSet ds = theHelper.MedInquireReport(1, date1, date2, ddlMedicineGroup.SelectedValue.ToString(), ddlMedicineSubGroup.SelectedValue.ToString(), ddlMedicineName.SelectedValue.ToString(), Session["CoCode"].ToString().Trim(), Session["YearCode"].ToString().Trim());

        rpt.Append("<table width='100%' cellspacing=0 border=0 style='border:1px solid black;border-spacing: 0; padding: 0;font-family:verdana;'>");
        rpt.Append("<tr style='height:30px'>");
        rpt.Append("<td style='width: 5%;border-bottom: 1px solid black; font-family:Verdana;font-weight:bold;background-color:beige; font-size:small; text-align:center'>SL No</td>");
        rpt.Append("<td style='width: 5%;border-bottom: 1px solid black;font-family:Verdana;font-weight:bold;background-color:beige; font-size:small; text-align:center'>Group Name</td>");
        rpt.Append("<td style='width: 5%;border-bottom: 1px solid black;font-family:Verdana;font-weight:bold;background-color:beige; font-size:small; text-align:center'>Composition</td>");
        rpt.Append("<td style='width: 5%;border-bottom: 1px solid black;font-family:Verdana;font-weight:bold;background-color:beige; font-size:small; text-align:center'>Tread Name</td>");
        rpt.Append("<td style='width: 5%;border-bottom: 1px solid black;font-family:Verdana;font-weight:bold;background-color:beige; font-size:small; text-align:center'>MRP</td>");
        rpt.Append("<td style='width: 5%;border-bottom: 1px solid black;font-family:Verdana;font-weight:bold;background-color:beige; font-size:small; text-align:center'>Purchase Price</td>");
        rpt.Append("</tr >");

        if (ds.Tables[0].Rows.Count > 0)
        {

            for (i = 0; i < ds.Tables[0].Rows.Count; i++)
            {
                rpt.Append("<tr style='height:30px'>");
                rpt.AppendFormat("<td style='width: 7%;font-family:Verdana; font-size:small;text-align:center'>{0}</td>", i + 1);
                rpt.AppendFormat("<td style='width: 7%;font-family:Verdana; font-size:small;text-align:center'>{0}</td>", ds.Tables[0].Rows[i]["MedicineGroupName"]);
                rpt.AppendFormat("<td style='width: 7%;font-family:Verdana; font-size:small;text-align:center'>{0}</td>", ds.Tables[0].Rows[i]["SubGrName"]);
                rpt.AppendFormat("<td style='width: 7%;font-family:Verdana; font-size:small;text-align:center'>{0}</td>", ds.Tables[0].Rows[i]["MedicineName"]);
                rpt.AppendFormat("<td style='width: 7%;font-family:Verdana; font-size:small;text-align:center'>{0}</td>", ds.Tables[0].Rows[i]["SellPricePerUnit"]);
                rpt.AppendFormat("<td style='width: 7%;font-family:Verdana; font-size:small;text-align:center'>{0}</td>", ds.Tables[0].Rows[i]["PricePerUnit"]);
                rpt.Append("</tr >");

            }
        }
        rpt.Append("</table'>");
        ltrReport.Text = rpt.ToString();

    }



    public void GetHearder_PurchaseDetail()
    {
        int i;
        System.Globalization.DateTimeFormatInfo dtf = new DateTimeFormatInfo();
        string date1, date2;
        if (txtFromDate.Text != "")
        {
            DateTime testdate = DateTime.ParseExact(txtFromDate.Text, "dd/MM/yyyy", dtf);
            date1 = testdate.ToString("yyyy-MM-dd");
        }
        else
        {
            date1 = "null";
        }

        if (txtToDate.Text != "")
        {
            DateTime testdate = DateTime.ParseExact(txtToDate.Text, "dd/MM/yyyy", dtf);
            date2 = testdate.ToString("yyyy-MM-dd");
        }
        else
        {
            date2 = "null";
        }

        DataSet ds = theHelper.MedInquireReport(2, date1, date2, ddlMedicineGroup.SelectedValue.ToString(), ddlMedicineSubGroup.SelectedValue.ToString(), ddlMedicineName.SelectedValue.ToString(), Session["CoCode"].ToString().Trim(), Session["YearCode"].ToString().Trim());

        rpt.Append("<table width='100%' cellspacing=0 border=0 style='border:1px solid black;border-spacing: 0; padding: 0;font-family:verdana;'>");
        rpt.Append("<tr style='height:30px'>");
        rpt.Append("<td style='width: 5%;border-bottom: 1px solid black; font-family:Verdana;font-weight:bold;background-color:beige; font-size:small; text-align:center'>SL No</td>");
        rpt.Append("<td style='width: 5%;border-bottom: 1px solid black;font-family:Verdana;font-weight:bold;background-color:beige; font-size:small; text-align:center'>Group Name</td>");
        rpt.Append("<td style='width: 5%;border-bottom: 1px solid black;font-family:Verdana;font-weight:bold;background-color:beige; font-size:small; text-align:center'>Composition</td>");
        rpt.Append("<td style='width: 5%;border-bottom: 1px solid black;font-family:Verdana;font-weight:bold;background-color:beige; font-size:small; text-align:center'>Tread Name</td>");
        rpt.Append("<td style='width: 5%;border-bottom: 1px solid black;font-family:Verdana;font-weight:bold;background-color:beige; font-size:small; text-align:center'>Sup.Name</td>");
        rpt.Append("<td style='width: 5%;border-bottom: 1px solid black;font-family:Verdana;font-weight:bold;background-color:beige; font-size:small; text-align:center'>Bill No</td>");
        rpt.Append("<td style='width: 5%;border-bottom: 1px solid black;font-family:Verdana;font-weight:bold;background-color:beige; font-size:small; text-align:center'>Bill Dt.</td>");
        rpt.Append("<td style='width: 5%;border-bottom: 1px solid black;font-family:Verdana;font-weight:bold;background-color:beige; font-size:small; text-align:center'>Qty</td>");
        rpt.Append("<td style='width: 5%;border-bottom: 1px solid black;font-family:Verdana;font-weight:bold;background-color:beige; font-size:small; text-align:center'>Purc.Price</td>");
        rpt.Append("<td style='width: 5%;border-bottom: 1px solid black;font-family:Verdana;font-weight:bold;background-color:beige; font-size:small; text-align:center'>MRP</td>");
        rpt.Append("</tr >");

        if (ds.Tables[0].Rows.Count > 0)
        {

            for (i = 0; i < ds.Tables[0].Rows.Count; i++)
            {
                rpt.Append("<tr style='height:30px'>");
                rpt.AppendFormat("<td style='width: 7%;font-family:Verdana; font-size:small;text-align:center'>{0}</td>", i + 1);
                rpt.AppendFormat("<td style='width: 7%;font-family:Verdana; font-size:small;text-align:center'>{0}</td>", ds.Tables[0].Rows[i]["MedicineGroupName"]);
                rpt.AppendFormat("<td style='width: 7%;font-family:Verdana; font-size:small;text-align:center'>{0}</td>", ds.Tables[0].Rows[i]["SubGrName"]);
                rpt.AppendFormat("<td style='width: 7%;font-family:Verdana; font-size:small;text-align:center'>{0}</td>", ds.Tables[0].Rows[i]["MedicineName"]);
                rpt.AppendFormat("<td style='width: 7%;font-family:Verdana; font-size:small;text-align:center'>{0}</td>", ds.Tables[0].Rows[i]["SName"]);
                rpt.AppendFormat("<td style='width: 7%;font-family:Verdana; font-size:small;text-align:center'>{0}</td>", ds.Tables[0].Rows[i]["BillNo"]);
                rpt.AppendFormat("<td style='width: 7%;font-family:Verdana; font-size:small;text-align:center'>{0}</td>", ds.Tables[0].Rows[i]["PurchaseDate"]);
                rpt.AppendFormat("<td style='width: 7%;font-family:Verdana; font-size:small;text-align:center'>{0}</td>", ds.Tables[0].Rows[i]["Qty"]);
                rpt.AppendFormat("<td style='width: 7%;font-family:Verdana; font-size:small;text-align:center'>{0}</td>", ds.Tables[0].Rows[i]["PricePerUnit"]);
                rpt.AppendFormat("<td style='width: 7%;font-family:Verdana; font-size:small;text-align:center'>{0}</td>", ds.Tables[0].Rows[i]["SellPricePerUnit"]);
                rpt.Append("</tr >");

            }
        }
        rpt.Append("</table'>");
        ltrReport.Text = rpt.ToString();

    }



    public void GetHearder_SupplierDetails()
    {
        int i;
        System.Globalization.DateTimeFormatInfo dtf = new DateTimeFormatInfo();
        string date1, date2;
        if (txtFromDate.Text != "")
        {
            DateTime testdate = DateTime.ParseExact(txtFromDate.Text, "dd/MM/yyyy", dtf);
            date1 = testdate.ToString("yyyy-MM-dd");
        }
        else
        {
            date1 = "null";
        }

        if (txtToDate.Text != "")
        {
            DateTime testdate = DateTime.ParseExact(txtToDate.Text, "dd/MM/yyyy", dtf);
            date2 = testdate.ToString("yyyy-MM-dd");
        }
        else
        {
            date2 = "null";
        }

        DataSet ds = theHelper.MedInquireReport(3, date1, date2, ddlMedicineGroup.SelectedValue.ToString(), ddlMedicineSubGroup.SelectedValue.ToString(), ddlMedicineName.SelectedValue.ToString(), Session["CoCode"].ToString().Trim(), Session["YearCode"].ToString().Trim());

        rpt.Append("<table width='100%' cellspacing=0 border=0 style='border:1px solid black;border-spacing: 0; padding: 0;font-family:verdana;'>");
        rpt.Append("<tr style='height:30px'>");
        rpt.Append("<td style='width: 5%;border-bottom: 1px solid black; font-family:Verdana;font-weight:bold;background-color:beige; font-size:small; text-align:center'>SL No</td>");
        rpt.Append("<td style='width: 5%;border-bottom: 1px solid black;font-family:Verdana;font-weight:bold;background-color:beige; font-size:small; text-align:center'>Group Name</td>");
        rpt.Append("<td style='width: 5%;border-bottom: 1px solid black;font-family:Verdana;font-weight:bold;background-color:beige; font-size:small; text-align:center'>Composition</td>");
        rpt.Append("<td style='width: 5%;border-bottom: 1px solid black;font-family:Verdana;font-weight:bold;background-color:beige; font-size:small; text-align:center'>Tread Name</td>");
        rpt.Append("<td style='width: 5%;border-bottom: 1px solid black;font-family:Verdana;font-weight:bold;background-color:beige; font-size:small; text-align:center'>Sup.Name</td>");
        rpt.Append("<td style='width: 5%;border-bottom: 1px solid black;font-family:Verdana;font-weight:bold;background-color:beige; font-size:small; text-align:center'>Address</td>");
        rpt.Append("<td style='width: 5%;border-bottom: 1px solid black;font-family:Verdana;font-weight:bold;background-color:beige; font-size:small; text-align:center'>Phone No</td>");
        rpt.Append("</tr >");

        if (ds.Tables[0].Rows.Count > 0)
        {

            for (i = 0; i < ds.Tables[0].Rows.Count; i++)
            {
                rpt.Append("<tr style='height:30px'>");
                rpt.AppendFormat("<td style='width: 7%;font-family:Verdana; font-size:small;text-align:center'>{0}</td>", i + 1);
                rpt.AppendFormat("<td style='width: 7%;font-family:Verdana; font-size:small;text-align:center'>{0}</td>", ds.Tables[0].Rows[i]["MedicineGroupName"]);
                rpt.AppendFormat("<td style='width: 7%;font-family:Verdana; font-size:small;text-align:center'>{0}</td>", ds.Tables[0].Rows[i]["SubGrName"]);
                rpt.AppendFormat("<td style='width: 7%;font-family:Verdana; font-size:small;text-align:center'>{0}</td>", ds.Tables[0].Rows[i]["MedicineName"]);
                rpt.AppendFormat("<td style='width: 7%;font-family:Verdana; font-size:small;text-align:center'>{0}</td>", ds.Tables[0].Rows[i]["SName"]);
                rpt.AppendFormat("<td style='width: 7%;font-family:Verdana; font-size:small;text-align:center'>{0}</td>", ds.Tables[0].Rows[i]["Address"]);
                rpt.AppendFormat("<td style='width: 7%;font-family:Verdana; font-size:small;text-align:center'>{0}</td>", ds.Tables[0].Rows[i]["Ph1"]);

                rpt.Append("</tr >");

            }
        }
        rpt.Append("</table'>");
        ltrReport.Text = rpt.ToString();

    }


    public void GetHearder_StockDetails()
    {
        int i;
        System.Globalization.DateTimeFormatInfo dtf = new DateTimeFormatInfo();
        string date1, date2;
        if (txtFromDate.Text != "")
        {
            DateTime testdate = DateTime.ParseExact(txtFromDate.Text, "dd/MM/yyyy", dtf);
            date1 = testdate.ToString("yyyy-MM-dd");
        }
        else
        {
            date1 = "null";
        }

        if (txtToDate.Text != "")
        {
            DateTime testdate = DateTime.ParseExact(txtToDate.Text, "dd/MM/yyyy", dtf);
            date2 = testdate.ToString("yyyy-MM-dd");
        }
        else
        {
            date2 = "null";
        }

        DataSet ds = theHelper.MedInquireReport(4, date1, date2, ddlMedicineGroup.SelectedValue.ToString(), ddlMedicineSubGroup.SelectedValue.ToString(), ddlMedicineName.SelectedValue.ToString(),Session["CoCode"].ToString().Trim(), Session["YearCode"].ToString().Trim());

        rpt.Append("<table width='100%' cellspacing=0 border=0 style='border:1px solid black;border-spacing: 0; padding: 0;font-family:verdana;'>");
        rpt.Append("<tr style='height:30px'>");
        rpt.Append("<td style='width: 5%;border-bottom: 1px solid black; font-family:Verdana;font-weight:bold;background-color:beige; font-size:small; text-align:center'>SL No</td>");
        rpt.Append("<td style='width: 5%;border-bottom: 1px solid black;font-family:Verdana;font-weight:bold;background-color:beige; font-size:small; text-align:center'>Group Name</td>");
        rpt.Append("<td style='width: 5%;border-bottom: 1px solid black;font-family:Verdana;font-weight:bold;background-color:beige; font-size:small; text-align:center'>Composition</td>");
        rpt.Append("<td style='width: 5%;border-bottom: 1px solid black;font-family:Verdana;font-weight:bold;background-color:beige; font-size:small; text-align:center'>Tread Name</td>");
        rpt.Append("<td style='width: 5%;border-bottom: 1px solid black;font-family:Verdana;font-weight:bold;background-color:beige; font-size:small; text-align:center'>ExpiryDate</td>");
        rpt.Append("<td style='width: 5%;border-bottom: 1px solid black;font-family:Verdana;font-weight:bold;background-color:beige; font-size:small; text-align:center'>Qty</td>");

        rpt.Append("</tr >");

        if (ds.Tables[0].Rows.Count > 0)
        {

            for (i = 0; i < ds.Tables[0].Rows.Count; i++)
            {
                rpt.Append("<tr style='height:30px'>");
                rpt.AppendFormat("<td style='width: 7%;font-family:Verdana; font-size:small;text-align:center'>{0}</td>", i + 1);
                rpt.AppendFormat("<td style='width: 7%;font-family:Verdana; font-size:small;text-align:center'>{0}</td>", ds.Tables[0].Rows[i]["MedicineGroupName"]);
                rpt.AppendFormat("<td style='width: 7%;font-family:Verdana; font-size:small;text-align:center'>{0}</td>", ds.Tables[0].Rows[i]["SubGrName"]);
                rpt.AppendFormat("<td style='width: 7%;font-family:Verdana; font-size:small;text-align:center'>{0}</td>", ds.Tables[0].Rows[i]["MedicineName"]);
                rpt.AppendFormat("<td style='width: 7%;font-family:Verdana; font-size:small;text-align:center'>{0}</td>", ds.Tables[0].Rows[i]["ExpiryDate"]);
                rpt.AppendFormat("<td style='width: 7%;font-family:Verdana; font-size:small;text-align:center'>{0}</td>", ds.Tables[0].Rows[i]["Qty"]);


                rpt.Append("</tr >");

            }
        }
        rpt.Append("</table'>");
        ltrReport.Text = rpt.ToString();

    }




    protected void Button3_Click(object sender, EventArgs e)
    {
        if (ddlSearchDetails.SelectedIndex == 0)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "msg", "alert('Please Select Search Item !');", true);
            ltrReport.Text = "";

        }
        else
        {


            if (ddlSearchDetails.SelectedIndex == 1)
            {

                if (RadioButtonList1.SelectedValue == "With Header")
                {

                    Report_Header();
                    GetHearder_PriceDetail();
                }
                else
                {
                    GetHearder_PriceDetail();
                }
            }
            else if (ddlSearchDetails.SelectedIndex == 2)
            {
                if (RadioButtonList1.SelectedValue == "With Header")
                {

                    Report_Header();
                    GetHearder_PurchaseDetail();
                }
                else
                {
                    GetHearder_PurchaseDetail();
                }
            }
            else if (ddlSearchDetails.SelectedIndex == 3)
            {
                if (RadioButtonList1.SelectedValue == "With Header")
                {

                    Report_Header();
                    GetHearder_SupplierDetails();
                }
                else
                {
                    GetHearder_SupplierDetails();
                }
            }
            else if (ddlSearchDetails.SelectedIndex == 4)
            {
                if (RadioButtonList1.SelectedValue == "With Header")
                {

                    Report_Header();
                    GetHearder_StockDetails();
                }
                else
                {
                    GetHearder_StockDetails();
                }
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
    protected void ddlReport_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlReport.SelectedIndex == 1)
        {
            Panel1.Visible = true;
        }
        else
        {
            Panel1.Visible = false;
        }
    }
}

   

