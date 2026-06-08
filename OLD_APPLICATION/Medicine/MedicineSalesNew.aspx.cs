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

public partial class Medicine_MedicineSalesNew : System.Web.UI.Page
{
    Main Objmain = new Main(ConfigurationSettings.AppSettings["ConnectionString"].ToString());
    MedicineSales theHelper = new MedicineSales(ConfigurationSettings.AppSettings["ConnectionString"].ToString());
    BedMaster theHelperBed = new BedMaster(ConfigurationSettings.AppSettings["ConnectionString"].ToString());
    Discharge thedischarge = new Discharge(ConfigurationSettings.AppSettings["ConnectionString"].ToString());
    PH_PatientRequisition thereq = new PH_PatientRequisition(ConfigurationSettings.AppSettings["ConnectionString"].ToString());
    System.Text.StringBuilder rpt = new System.Text.StringBuilder();
    String Date = DateTime.Now.ToString("dd/MM/yyyy");
    double discrt = 0;
    protected void Page_Load(object sender, EventArgs e)
    {
        discrt = 0.2;      //20% discount for staff 
        Page.Title = "Sale/Issue Medicine";
        if (Session["userName"] == null)
        {
            Response.Redirect("../LoginPage.aspx");
        }
        if (Objmain.checkaccss(Session["CoCode"].ToString(), Session["UserRoleID"].ToString().Trim(), "MEDICINE/REAGENT ISSUE", checkAccessType.ViewAction) == false)
        {
            Response.Redirect("../AccessDenied.aspx");
        }

        if (Objmain.checkaccss(Session["CoCode"].ToString(), Session["UserRoleID"].ToString().Trim(), "MEDICINE/REAGENT ISSUE", checkAccessType.InsertAction) == false)
        {
            Button1.Enabled = false;
        }
        //if (theHelper.SalesParameter(Session["CoCode"].ToString().Trim(), Session["YearCode"].ToString().Trim()) == "P")
        //{
        //    lblid2.Text = "<span style='color:red;'>*</span>";
        //}
        //else
        //{
        //    lblid1.Text = "<span style='color:red;'>*</span>";
        //}
        if (!IsPostBack)
        {
            
            DropDownFill();
            GenerateCode();
            TextBox6.Text = DateTime.Now.ToString("yyyy-MM-dd");
            issueby.Text = Session["userName"].ToString();
            txtPushChrg.Text = "0.00";
            txtChrgAmt1.Text = "0.00";
            txtChrgAmt2.Text = "0.00";
            txtCash.Text = "0.00";
            txtCard.Text = "0.00";
            txtewallet.Text = "0.00";
            txtNetBank.Text = "0.00";
        }
        if (Session["zm_docno"] != null)
        {
            if (Session["zm_docno"].ToString() != "")
            {
                TextBox5.Text = Session["zm_docno"].ToString();
                HiddenField1.Value = TextBox5.Text;
                PageDataBind();
                Button1.Visible = false; Button2.Visible = false; Button3.Visible = false; Button4.Visible = false;
            }
        }
    }

    private void ResetRow(int rowno)
    {
        int cal = rowno + 1;
        DropDownList ddlMfg = (DropDownList)divContent.FindControl("ddlMfg" + rowno.ToString());
        ddlMfg.SelectedIndex = -1;
        DropDownList ddlMediGrp = (DropDownList)divContent.FindControl("ddlMediGrp" + rowno.ToString());
        ddlMediGrp.SelectedIndex = -1;

        DropDownList ddlMediSubGrp = (DropDownList)divContent.FindControl("ddlMediSubGrp" + rowno.ToString());
        ddlMediSubGrp.SelectedIndex = -1;
        DropDownList ddlMedi = (DropDownList)divContent.FindControl("ddlMedi" + rowno.ToString());
        ddlMedi.SelectedIndex = -1;
        DropDownList txtBatch = (DropDownList)divContent.FindControl("txtBatch" + rowno.ToString());
        txtBatch.Items.Clear();
        //txtBatch.SelectedIndex = -1;
        TextBox Calendar = (TextBox)divContent.FindControl("Calendar" + cal.ToString());
        Calendar.Text = string.Empty;

        TextBox expdt = (TextBox)divContent.FindControl("expdt" + rowno.ToString());
        expdt.Text = string.Empty;

        TextBox txtStock = (TextBox)divContent.FindControl("txtStock" + rowno.ToString());
        txtStock.Text = string.Empty;

        TextBox txtUnit = (TextBox)divContent.FindControl("txtUnit" + rowno.ToString());
        txtUnit.Text = string.Empty;

        DropDownList ddlSellingUnit = (DropDownList)divContent.FindControl("ddlSellingUnit" + rowno.ToString());
        ddlSellingUnit.Items.Clear();

        TextBox txtQty = (TextBox)divContent.FindControl("txtQty" + rowno.ToString());
        txtQty.Text = string.Empty;

        TextBox txtdiscAmt = (TextBox)divContent.FindControl("txtdiscAmt" + rowno.ToString());
        txtdiscAmt.Text = string.Empty;

        TextBox txtUnitPrice = (TextBox)divContent.FindControl("txtUnitPrice" + rowno.ToString());
        txtUnitPrice.Text = string.Empty;

        TextBox txtTaxableAmt = (TextBox)divContent.FindControl("txtTaxableAmt" + rowno.ToString());
        txtTaxableAmt.Text = string.Empty;

        TextBox txtHsnCode = (TextBox)divContent.FindControl("txtHsnCode" + rowno.ToString());
        txtHsnCode.Text = string.Empty;

        TextBox txtCgstRt = (TextBox)divContent.FindControl("txtCgstRt" + rowno.ToString());
        txtCgstRt.Text = string.Empty;

        TextBox txtCgstAmt = (TextBox)divContent.FindControl("txtCgstAmt" + rowno.ToString());
        txtCgstAmt.Text = string.Empty;

        TextBox txtSgstRt = (TextBox)divContent.FindControl("txtSgstRt" + rowno.ToString());
        txtSgstRt.Text = string.Empty;

        TextBox txtSgstAmt = (TextBox)divContent.FindControl("txtSgstAmt" + rowno.ToString());
        txtSgstAmt.Text = string.Empty;

        TextBox txtTotalPrice = (TextBox)divContent.FindControl("txtTotalPrice" + rowno.ToString());
        txtTotalPrice.Text = string.Empty;
        calculateTotal();
    }


    private void ResetAllFields()
    {
        hdnMode.Value = "0";
        //ddlwing.SelectedIndex = 0; 
        txtreg.Text = ""; TextBox2.Text = ""; TextBox3.Text = ""; TextBox4.Text = ""; TextBox6.Text = ""; TextBox7.Text = "";
        GenerateCode();
        Button1.Text = "SUBMIT";

        for (int i = 1, cal = 2; i <= 12; i++, cal++)
        {
            DropDownList ddlMfg = (DropDownList)divContent.FindControl("ddlMfg" + i.ToString());
            ddlMfg.SelectedIndex = -1;
            DropDownList ddlMediGrp = (DropDownList)divContent.FindControl("ddlMediGrp" + i.ToString());
            ddlMediGrp.SelectedIndex = -1;

            DropDownList ddlMediSubGrp = (DropDownList)divContent.FindControl("ddlMediSubGrp" + i.ToString());
            ddlMediSubGrp.SelectedIndex = -1;
            DropDownList ddlMedi = (DropDownList)divContent.FindControl("ddlMedi" + i.ToString());
            ddlMedi.SelectedIndex = -1;
            DropDownList txtBatch = (DropDownList)divContent.FindControl("txtBatch" + i.ToString());
            txtBatch.Items.Clear();
            //txtBatch.SelectedIndex = -1;
            TextBox Calendar = (TextBox)divContent.FindControl("Calendar" + cal.ToString());
            Calendar.Text = string.Empty;
            TextBox txtQty = (TextBox)divContent.FindControl("txtQty" + i.ToString());
            txtQty.Text = string.Empty;
            TextBox txtUnitPrice = (TextBox)divContent.FindControl("txtUnitPrice" + i.ToString());
            txtUnitPrice.Text = string.Empty;
            TextBox txtTotalPrice = (TextBox)divContent.FindControl("txtTotalPrice" + i.ToString());
            txtTotalPrice.Text = string.Empty;
        }

    }
    private void DropDownFill()
    {
        string compcode = Session["CoCode"].ToString().Trim();
        string yearcode = Session["YearCode"].ToString().Trim();


        //ddlwing.Items.Clear();
        /*this.ddlwing.DataSource = theHelper.TxtBoxList(compcode,"W");
        this.ddlwing.DataTextField = "name";
        this.ddlwing.DataValueField = "code";
        this.ddlwing.DataBind();*/
        //this.ddlwing.Items.Insert(0, new ListItem("--Select--", "0"));

        //ddlFloor.Items.Clear();
        //this.ddlFloor.DataSource = theHelper.getFloor(compcode);
        //this.ddlFloor.DataTextField = "FloorName";
        //this.ddlFloor.DataValueField = "FloorId";
        //this.ddlFloor.DataBind();
        //this.ddlFloor.Items.Insert(0, new ListItem("--Select--", "0"));

        //ddlPatient.Items.Clear();
        //this.ddlPatient.DataSource = theHelper.TxtBoxList(compcode, "P");
        //this.ddlPatient.DataTextField = "name";
        //this.ddlPatient.DataValueField = "slcode";
        //this.ddlPatient.DataBind();
        //this.ddlPatient.Items.Insert(0, new ListItem("--Select--", "0"));

        DataTable dtstaff = theHelper.DropdownStaff(Session["CoCode"].ToString().Trim());
        ddlstaff.DataSource = dtstaff;
        ddlstaff.DataTextField = "EmployeeName";
        ddlstaff.DataValueField = "EmployeeID";
        ddlstaff.DataBind();

        for (int i = 1; i <= 12; i++)
        {
            DropDownList ddlmfg = (DropDownList)Page.FindControl("ctl00$ContentPlaceHolder1$ddlMfg" + i.ToString());
            ddlmfg.Items.Clear();
            ddlmfg.Items.Insert(0, new ListItem("--Select--", "0"));

            DropDownList ddlgrp = (DropDownList)Page.FindControl("ctl00$ContentPlaceHolder1$ddlMediGrp" + i.ToString());
            ddlgrp.Items.Insert(0, new ListItem("--Select--", "0"));


            DropDownList ddlSub = (DropDownList)Page.FindControl("ctl00$ContentPlaceHolder1$ddlMediSubGrp" + i.ToString());
            ddlSub.Items.Insert(0, new ListItem("--Select--", "0"));


            DropDownList ddlMedicine = (DropDownList)Page.FindControl("ctl00$ContentPlaceHolder1$ddlMedi" + i.ToString());
            ddlMedicine.Items.Clear();
            ddlMedicine.DataSource = theHelper.DropdownMedicine(Session["CoCode"].ToString().Trim(), Session["YearCode"].ToString().Trim());
            ddlMedicine.DataTextField = "MedicineName";
            ddlMedicine.DataValueField = "MedicineID";
            ddlMedicine.DataBind();
            ddlMedicine.Items.Insert(0, new ListItem("--Select--", "0"));


        }

    }


    protected void Button1_Click(object sender, EventArgs e)
    {
        string regno = Request.Form[txtreg.UniqueID];
        txtreg.Text = regno;
        TextBox t1, t2, t3;
        string compcode = Session["CoCode"].ToString().Trim();
        string yearcode = Session["YearCode"].ToString().Trim();
        HiddenField1.Value = TextBox5.Text;
        double total = 0.0, totgval = 0.0;
        for (int i = 1; i <= 12; i++)
        {
            t1 = (TextBox)Page.FindControl("ctl00$ContentPlaceHolder1$txtTotalPrice" + i.ToString());
            t2 = (TextBox)Page.FindControl("ctl00$ContentPlaceHolder1$txtUnitPrice" + i.ToString());
            t3 = (TextBox)Page.FindControl("ctl00$ContentPlaceHolder1$txtQty" + i.ToString());
            
            if (t3.Text != "" && Convert.ToDouble(t3.Text)>0)
            {
                //totgval = totgval + (Convert.ToDouble(t2.Text) * Convert.ToDouble(t2.Text));
                totgval = totgval + (Convert.ToDouble(t1.Text));
                total = total + Convert.ToDouble(t1.Text);
            }
        }
        //if (txtPushChrg.Text == "")
        //{
        //    txtPushChrg.Text = "0";
        //}
        //total = total + Convert.ToDouble(txtPushChrg.Text);
        try
        {
            if (Button1.Text.ToUpper() == "SUBMIT")
            {
                //if (theHelper.SalesParameter(Session["CoCode"].ToString().Trim(), Session["YearCode"].ToString().Trim()) == "W" && ddlwing.SelectedValue == "0")
                //{
                //    ScriptManager.RegisterStartupScript(this, this.GetType(), "msg", "alert('Select Work Station / Wings !');", true);
                //}
                //else if (theHelper.SalesParameter(Session["CoCode"].ToString().Trim(), Session["YearCode"].ToString().Trim()) == "P" && ddlPatient.SelectedValue == "0")
                //{
                //    ScriptManager.RegisterStartupScript(this, this.GetType(), "msg", "alert('Select Patient !');", true);
                //}
                if (TextBox6.Text == "")
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "msg", "alert('Bill Date can not be blank !');", true);
                }
                else if (txtreg.Text == "")
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "msg", "alert('Registration No cannot be blank !');", true);
                }
                else
                {
                    InsertPurchaseMedicine(compcode, yearcode, Convert.ToDouble(txtNetAmt.Text), totgval);
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "msg", "alert('Inserted  Successfully !');", true);
                    GetReport();
                }
            }
            else
            {
                //if (theHelper.SalesParameter(Session["CoCode"].ToString().Trim(), Session["YearCode"].ToString().Trim()) == "W" && ddlwing.SelectedValue == "0")
                //{
                //    ScriptManager.RegisterStartupScript(this, this.GetType(), "msg", "alert('Select Work Station / Wings !');", true);
                //}
                //else if (theHelper.SalesParameter(Session["CoCode"].ToString().Trim(), Session["YearCode"].ToString().Trim()) == "P" && ddlPatient.SelectedValue == "0")
                //{
                //    ScriptManager.RegisterStartupScript(this, this.GetType(), "msg", "alert('Select Patient !');", true);
                //}
                if (TextBox6.Text == "")
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "msg", "alert('Bill Date can not be blank !');", true);
                }
                else
                {
                    string patientid = hdnLedgerId.Value;


                    //System.Globalization.DateTimeFormatInfo dtf = new DateTimeFormatInfo();
                    //DateTime testdate = DateTime.ParseExact(TextBox6.Text, "dd/MM/yyyy", dtf);
                    //theHelper.UpdatePurchaseMedicine(compcode, yearcode, TextBox5.Text, ddlPatient.SelectedValue, TextBox2.Text, TextBox4.Text, TextBox3.Text, total.ToString(), testdate.ToString(), TextBox7.Text, ddlwing.SelectedValue);
                    //theHelper.UpdatePurchaseMedicineNew(compcode, yearcode, TextBox5.Text, patientid, TextBox2.Text, TextBox4.Text, TextBox3.Text, total.ToString(), testdate.ToString(), TextBox7.Text);
                    theHelper.DeleteMEdDtls(Session["CoCode"].ToString().Trim(), Session["YearCode"].ToString().Trim(), TextBox5.Text);
                    InsertPurchaseMedicine(compcode, yearcode, Convert.ToDouble(txtNetAmt.Text), totgval);
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "msg", "alert('Updated  Successfully !');", true);
                    GetReport();
                    hdnInsUpd.Value = "I";
                }
            }
            PageDataBind();
        }
        catch (Exception ex)
        {
            lblError.ForeColor = System.Drawing.Color.Red;
            lblError.Text = "Error in saving..";
        }
        ResetAllFields();
    }
    public bool validation(string MedId, string MfgId, string batchno, TextBox txtqty,TextBox packSize)
    {
        DataTable dt = theHelper.AvailableStock(Session["CoCode"].ToString().Trim(), Session["YearCode"].ToString().Trim(), MedId, MfgId, batchno);
        Decimal amt = Convert.ToDecimal(dt.Rows[0]["AvailQty"])*Convert.ToDecimal(packSize.Text);

        int i; Decimal curamt = 0;
        for (i = 1; i <= 12; i++)
        {
            DropDownList d = (DropDownList)Page.FindControl("ctl00$ContentPlaceHolder1$ddlMedi" + i.ToString());
            TextBox txt = (TextBox)Page.FindControl("ctl00$ContentPlaceHolder1$txtQty" + i.ToString());
            string quantity = txt.Text;
            HiddenField qty = (HiddenField)Page.FindControl("ctl00$ContentPlaceHolder1$qty" + i.ToString());

            if (qty.Value.ToString() == "") { qty.Value = "0"; }
            if (quantity == "") { quantity = "0"; }
            if (d.SelectedValue == MedId)
            {
                curamt = curamt + Convert.ToDecimal(quantity);
                if (hdnInsUpd.Value == "U" && quantity == txtqty.Text)
                {
                    curamt = curamt - Convert.ToDecimal(qty.Value);
                }
            }
        }
        if (amt >= curamt)
        {
            return true;
        }
        else { return false; }
    }
    protected void Button2_Click(object sender, EventArgs e)
    {
        ResetAllFields();
        hdnInsUpd.Value = "I";
    }


    protected void Button6_Click(object sender, EventArgs e)
    {
        if (Session["zm_docno"].ToString() != "")
        {
            Response.Redirect("../Query/MedicineTransactionZoom.aspx");
        }
        else
        {
            Response.Redirect("../HomePage.aspx");
        }
    }

    public void GetReport()
    {
        //Report_Header();
        GetHearder_Detail();
        ltrReport.Text = rpt.ToString();
    }


    public void Report_Header()
    {

        rpt.Append("<table width='100%' cellspacing=0 border=0 style='font-family:verdana;font-size:small'>");
        rpt.Append("<tr cellpadding:'0'>");
        rpt.AppendFormat("<td style='width: 35%; font-family:Verdana; font-size:x-Medium; font-weight:bold;text-align:left'><img src='../Images/logo.jpg'  style='height:87px;width:205px;border-style:inherit;border-color:#D3CDD4'/></td>");
        rpt.AppendFormat("<td style='width: 35%; font-family:Verdana; font-size:large; font-weight:bold;text-align:center'>{0}</td>", "&nbsp;");
        rpt.AppendFormat("<td style='width: 35%; font-family:Verdana; font-size:x-Medium; font-weight:bold;text-align:center'>&nbsp;</td>");
        rpt.Append("</tr>");
        rpt.Append("</table>");
    }

    public void GetHearder_Detail()
    {

        DataTable dtPurchaseMedicine = theHelper.gettaxinvoicehead(Session["CoCode"].ToString().Trim(), Session["YearCode"].ToString().Trim(), HiddenField1.Value);

        if (dtPurchaseMedicine.Rows.Count > 0)
        {
            if (dtPurchaseMedicine.Rows[0]["CancelFlag"].ToString().Trim() == "1")
            {
                rpt.Append("<div style='font-family:Arial;font-size:7pt;font-weight:normal;background-image:url(../Images/cancel.png); background-repeat:no-repeat;background-position: center;'>");
            }
            else
            {
                rpt.Append("<div style='font-family:Arial;font-size:7pt;font-weight:normal;'>");                 
            }
            rpt.Append("<table width='100%' cellspacing=0 border=0 style='font-family:Arial;font-size:8pt;'>");
            rpt.Append("<tr><td colspan='5' style='text-align:center;padding-bottom:10px;font-family:Arial;font-size:large;font-weight:normal;height:20px;'>&nbsp;</td></tr>");
            rpt.Append("<tr><td colspan='5' style='text-align:center;padding-bottom:10px;font-family:Algerian;font-size:x-large;font-weight:normal'><u>TAX INVOICE</u></td></tr>");

            rpt.Append("<tr>");
            rpt.Append("<td style='width:17%'>Name of Patient</td>");
            rpt.Append("<td style='width:3%'> :</td>");
            rpt.Append("<td style='width:50%'>" + dtPurchaseMedicine.Rows[0]["patient"].ToString() + "</td>");
            rpt.Append("<td style='width:15%;text-align:right'>Invoice No :</td>");
            rpt.Append("<td style='width:15%;text-align:right'>" + dtPurchaseMedicine.Rows[0]["REFBILLNO"].ToString() + "</td>");
            rpt.Append("</tr>");

            rpt.Append("<tr>");
            rpt.Append("<td>Refered By </td>");
            rpt.Append("<td> :</td>");
            rpt.Append("<td>" + "Dr. "+ dtPurchaseMedicine.Rows[0]["cfiller10"].ToString() + "</td>");
            rpt.Append("<td style='text-align:right'>Date :</td>");
            rpt.Append("<td style='text-align:right'>" + dtPurchaseMedicine.Rows[0]["docdt"].ToString() + "</td>");
            rpt.Append("</tr>");

            rpt.Append("</table>");

            DataTable dtdetl = theHelper.GetSaleMedicineDetails(Session["CoCode"].ToString().Trim(), Session["YearCode"].ToString().Trim(), HiddenField1.Value);
            rpt.Append("<br/><table width='100%' cellspacing=0 border=1 bordercolor=silver style='font-family:Arial;font-size:7pt;font-weight:normal;'>");
            rpt.Append("<tr>");
            rpt.Append("<td style='text-align:center;width:2%' rowspan='2'>Srl</td>");
            rpt.Append("<td style='text-align:center;width:45%' colspan='4'>Description of Goods</td>");
            rpt.Append("<td style='text-align:center;width:5%' rowspan='2'>Qty</td>");
            rpt.Append("<td style='text-align:center;width:5%' rowspan='2'>Disc(%)</td>");
            rpt.Append("<td style='text-align:center;width:5%' rowspan='2'>Disc(Rs.)</td>");
            rpt.Append("<td style='text-align:center;width:7%' rowspan='2'>Taxable Value</td>");
            rpt.Append("<td style='text-align:center;width:5%' rowspan='2'>SGST %</td>");
            rpt.Append("<td style='text-align:center;width:5%' rowspan='2'>CGST %</td>");
            rpt.Append("<td style='text-align:center;width:6%' rowspan='2'>SGST</td>");
            rpt.Append("<td style='text-align:center;width:6%' rowspan='2'>CGST</td>");
            rpt.Append("<td style='text-align:center;width:7%' rowspan='2'>Amount</td>");
            rpt.Append("</tr>");

            rpt.Append("<tr>");
            rpt.Append("<td style='text-align:center;width:26%'>Name of Product</td>");
            rpt.Append("<td style='text-align:center;width:7%'>Batch No</td>");
            rpt.Append("<td style='text-align:center;width:6%'>Expiry Dt</td>");
            rpt.Append("<td style='text-align:center;width:6%'>HSN/SAC</td>");
            rpt.Append("</tr>");

            int slno = 0;
            for (int i = 0; i < dtdetl.Rows.Count; i++)
            {
                slno++;
                rpt.Append("<tr>");
                rpt.Append("<td style=''>" + slno.ToString() + "</td>");
                rpt.Append("<td style=''>" + dtdetl.Rows[i]["MedicineName"].ToString() + "</td>");
                rpt.Append("<td style=''>" + dtdetl.Rows[i]["BATCHNO"].ToString() + "</td>");
                rpt.Append("<td style=''>" + dtdetl.Rows[i]["ExpYearMonth"].ToString() + "</td>");
                rpt.Append("<td style=''>" + dtdetl.Rows[i]["HSNCODE"].ToString() + "</td>");
                rpt.Append("<td style='text-align:center'>" + (Convert.ToDouble(dtdetl.Rows[i]["IQTY"]) * Convert.ToDouble(dtdetl.Rows[i]["PACKQTY"])).ToString() + " " + dtdetl.Rows[i]["sellUnitName"].ToString() + "</td>");
                rpt.Append("<td style=''></td>");
                rpt.Append("<td style='text-align:right'>" + dtdetl.Rows[i]["DISCAMT"].ToString() + "</td>");
                rpt.Append("<td style='text-align:right'>" + dtdetl.Rows[i]["SELLAMOUNT"].ToString() + "</td>");
                rpt.Append("<td style='text-align:right'>" + dtdetl.Rows[i]["SGST_RATE"].ToString() + "</td>");
                rpt.Append("<td style='text-align:right'>" + dtdetl.Rows[i]["CGST_RATE"].ToString() + "</td>");
                rpt.Append("<td style='text-align:right'>" + dtdetl.Rows[i]["SGST_AMT"].ToString() + "</td>");
                rpt.Append("<td style='text-align:right'>" + dtdetl.Rows[i]["CGST_AMT"].ToString() + "</td>");
                rpt.Append("<td style='text-align:right'>" + dtdetl.Rows[i]["IAMOUNTWITHVAT"].ToString() + "</td>");
                rpt.Append("</tr>");
            }

            if (Convert.ToDecimal(dtPurchaseMedicine.Rows[0]["ExtraChargeAmt1"]) > 0)
            {
                slno++;
                rpt.Append("<tr>");
                rpt.Append("<td style=''>" + slno.ToString() + "</td>");
                rpt.Append("<td style=''>" + dtPurchaseMedicine.Rows[0]["ExtraChargeDesc1"] + "</td>");
                rpt.Append("<td style=''></td>");
                rpt.Append("<td style=''></td>");
                rpt.Append("<td style=''></td>");
                rpt.Append("<td style=''></td>");
                rpt.Append("<td style=''></td>");
                rpt.Append("<td style=''></td>");
                rpt.Append("<td style=''></td>");
                rpt.Append("<td style=''></td>");
                rpt.Append("<td style=''></td>");
                rpt.Append("<td style=''></td>");
                rpt.Append("<td style=''></td>");
                rpt.Append("<td style='text-align:right'>" + dtPurchaseMedicine.Rows[0]["ExtraChargeAmt1"].ToString() + "</td>");
                rpt.Append("</tr>");
            }
            if (Convert.ToDecimal(dtPurchaseMedicine.Rows[0]["ExtraChargeAmt2"]) > 0)
            {
                slno++;
                rpt.Append("<tr>");
                rpt.Append("<td style=''>" + slno.ToString() + "</td>");
                rpt.Append("<td style=''>" + dtPurchaseMedicine.Rows[0]["ExtraChargeDesc2"] + "</td>");
                rpt.Append("<td style=''></td>");
                rpt.Append("<td style=''></td>");
                rpt.Append("<td style=''></td>");
                rpt.Append("<td style=''></td>");
                rpt.Append("<td style=''></td>");
                rpt.Append("<td style=''></td>");
                rpt.Append("<td style=''></td>");
                rpt.Append("<td style=''></td>");
                rpt.Append("<td style=''></td>");
                rpt.Append("<td style=''></td>");
                rpt.Append("<td style=''></td>");
                rpt.Append("<td style='text-align:right'>" + dtPurchaseMedicine.Rows[0]["ExtraChargeAmt2"].ToString() + "</td>");
                rpt.Append("</tr>");
            }

            if (Convert.ToDecimal(dtPurchaseMedicine.Rows[0]["pushchrg"]) > 0)
            {
                slno++;
                rpt.Append("<tr>");
                rpt.Append("<td style=''>" + slno.ToString() + "</td>");
                rpt.Append("<td style=''>PUSHING CHARGES</td>");
                rpt.Append("<td style=''></td>");
                rpt.Append("<td style=''></td>");
                rpt.Append("<td style=''></td>");
                rpt.Append("<td style=''></td>");
                rpt.Append("<td style=''></td>");
                rpt.Append("<td style=''></td>");
                rpt.Append("<td style=''></td>");
                rpt.Append("<td style=''></td>");
                rpt.Append("<td style=''></td>");
                rpt.Append("<td style=''></td>");
                rpt.Append("<td style=''></td>");
                rpt.Append("<td style='text-align:right'>" + dtPurchaseMedicine.Rows[0]["pushchrg"].ToString() + "</td>");
                rpt.Append("</tr>");
            }

            rpt.Append("<tr>");
            rpt.Append("<td colspan='13' style='text-align:right;font-size:7pt;font-weight:normal'>Rounded off</td>");
            rpt.Append("<td style='text-align:right'>" + dtPurchaseMedicine.Rows[0]["ROFFAMT"].ToString() + "</td>");
            rpt.Append("</tr>");

            rpt.Append("<tr>");
            rpt.Append("<td colspan='13' style='text-align:right;font-size:7pt;font-weight:normal'>Total</td>");
            rpt.Append("<td style='text-align:right;font-weight:bold'>" + dtPurchaseMedicine.Rows[0]["billvalue"].ToString() + "</td>");
            rpt.Append("</tr>");

            rpt.Append("</table>");

            rpt.Append("<br/><table width='100%' cellspacing=0  style='font-family:Arial;font-size:7pt;font-weight:normal;'>");
            rpt.Append("<tr>");
            rpt.Append("<td>Amount (In words) : " + dtPurchaseMedicine.Rows[0]["amtword"].ToString() + "</td>");
            rpt.Append("</tr>");

            
            //string paymode = "";
            //if (dtPurchaseMedicine.Rows[0]["paymode"].ToString().Trim() == "C")
            //    paymode = "Cash";
            //else if (dtPurchaseMedicine.Rows[0]["paymode"].ToString().Trim() == "R")
            //    paymode = "Card";
            //else if (dtPurchaseMedicine.Rows[0]["paymode"].ToString().Trim() == "E")
            //    paymode = "E-Wallet";
            //else if (dtPurchaseMedicine.Rows[0]["paymode"].ToString().Trim() == "N")
            //    paymode = "Net Banking";
            //else paymode = "";
            if (Convert.ToDouble(dtPurchaseMedicine.Rows[0]["cash_amt"]) > 0)
            {
                rpt.Append("<tr>");
                rpt.Append("<td>Payment Mode : Cash " + dtPurchaseMedicine.Rows[0]["cash_amt"] + "/-</td>");
                rpt.Append("</tr>");
            }
            if (Convert.ToDouble(dtPurchaseMedicine.Rows[0]["card_amt"]) > 0)
            {
                rpt.Append("<tr>");
                rpt.Append("<td>Payment Mode : Card " + dtPurchaseMedicine.Rows[0]["card_amt"] + "/-</td>");
                rpt.Append("</tr>");
            }
            if (Convert.ToDouble(dtPurchaseMedicine.Rows[0]["ewallet_amt"]) > 0)
            {
                rpt.Append("<tr>");
                rpt.Append("<td>Payment Mode : UPI " + dtPurchaseMedicine.Rows[0]["ewallet_amt"] + "/-</td>");
                rpt.Append("</tr>");
            }
            if (Convert.ToDouble(dtPurchaseMedicine.Rows[0]["netbank_amt"]) > 0)
            {
                rpt.Append("<tr>");
                rpt.Append("<td>Payment Mode : Net Banking " + dtPurchaseMedicine.Rows[0]["netbank_amt"] + "/-</td>");
                rpt.Append("</tr>");
            }
            rpt.Append("</table>");

            rpt.Append("<br/><table width='100%' cellspacing=0  style='font-family:Arial;font-size:7pt;font-weight:normal'>");
            rpt.Append("<tr>");
            rpt.Append("<td>Company's GSTIN/UIN : 19AAUCA1636K1Z6</td><td style='text-align:right;font-size:7pt;font-weight:normal'>For Ankuran IVF Center Pvt Ltd</td>");
            rpt.Append("</tr>");
            rpt.Append("<tr>");            
            rpt.Append("<td>D.L. NO : WB/KOL/NBO/R/639122, WB/KOL/BIO/R/639122</td>");
            rpt.Append("<td>&nbsp;</td>");
            rpt.Append("</tr>");
            rpt.Append("<tr>");
            rpt.Append("<td>CIN : U74999WB2020PTC240309</td>");
            rpt.Append("<td style='text-align:right;font-size:7pt;font-weight:normal'>Authorised Signatory</td>");
            rpt.Append("</tr>");
            rpt.Append("</table>");

            rpt.Append("<br/><table width='100%' cellspacing=0  style='font-family:Arial;font-size:7pt;font-weight:normal'>");         
            rpt.Append("<tr>");
            rpt.Append("<td>Declaration<br/>We declare that this invoice shown the actual price of goods described and that all particulars are true & correct.</td>");
            rpt.Append("</tr>");
            rpt.Append("</table>");

            rpt.Append("<br/><table width='100%' cellspacing=0  style='font-family:Arial;font-size:7pt;font-weight:normal'>");
            rpt.Append("<tr>");
            rpt.Append("<td><u>Terms & Conditions</u><br/>All disputes subjects to KOLKATA jurisdiction only.<br/>Goods once sold will not be taken back</td>");
            rpt.Append("</tr>");
            rpt.Append("</table>");
            rpt.Append("</div>");
   
        }
        ltrReport.Visible = true;

    }


    public void GenerateCode()
    {
        DataTable dtsalecode = theHelper.GenerateSaleId(Session["CoCode"].ToString().Trim(), Session["YearCode"].ToString().Trim());
        TextBox5.Text = dtsalecode.Rows[0][0].ToString();
        DataTable dt = thereq.GenerateReceiptNo(Session["CoCode"].ToString().Trim(), Session["YearCode"].ToString().Trim(), "PHR");
        txtBillNo.Text = dt.Rows[0][0].ToString();
    }
    private void InsertPurchaseMedicine(string compcode, string yearcode, double total, double totgval)
    {
        int i = 0;
        string reformattedDate1 = string.Empty;
        System.Globalization.DateTimeFormatInfo dtf = new DateTimeFormatInfo();

        string testdate = TextBox6.Text.Trim();// DateTime.ParseExact(TextBox6.Text, "dd/MM/yyyy", dtf);
        string rcvdate = receivedt.Text.Trim();// DateTime.ParseExact(receivedt.Text, "dd/MM/yyyy", dtf);
        string regno = Request.Form[txtreg.UniqueID];
        string patientid = "";
        if (ddltype.SelectedValue == "P")
            patientid = theHelper.getPatientLedgerId(compcode, yearcode, regno);
        else patientid = ddlstaff.SelectedValue;

        string cancel = "0";
        if (chkCancel.Checked == true)
        {
            cancel = "1";
        }
        else
        {
            cancel = "0";
        }

        if (ddlMedi1.SelectedIndex > 0 && txtQty1.Text.Length > 0 /*&& txtUnitPrice1.Text.Length > 0 && txtTotalPrice1.Text.Length > 0 && txtBatch1.Text.Length > 0 && Calendar2.Text.Length > 0*/)
        {
            i++;
            string expdate1 = Calendar2.Text.Trim();// DateTime.ParseExact(Calendar2.Text, "dd/MM/yyyy", dtf);
            //theHelper.InsertSaleMedicine(i, compcode, yearcode, ddlMediSubGrp1.SelectedValue, ddlPatient.SelectedValue, ddlwing.SelectedValue, TextBox2.Text, TextBox4.Text, TextBox3.Text, TextBox5.Text, testdate.ToString("yyyy-MM-dd"), total.ToString(), TextBox7.Text, ddlMfg1.SelectedValue, ddlMediGrp1.SelectedValue, ddlMedi1.SelectedValue, txtBatch1.Text, testdate1.ToString("yyyy-MM-dd"), txtQty1.Text, txtUnitPrice1.Text, txtTotalPrice1.Text, Session["userName"].ToString(), rcvdate.ToString("yyyy-MM-dd"), receiveby.Text, issueby.Text); //for IPD
            theHelper.InsertSaleMedicineNew(i, compcode, yearcode, ddlMediSubGrp1.SelectedValue, patientid, TextBox2.Text, TextBox4.Text, TextBox3.Text, TextBox5.Text, testdate, total.ToString(), TextBox7.Text, ddlMfg1.SelectedValue, ddlMediGrp1.SelectedValue, ddlMedi1.SelectedValue, txtBatch1.Text, expdate1, txtQty1.Text, txtUnitPrice1.Text, txtTotalPrice1.Text, Session["userName"].ToString(), rcvdate, receiveby.Text, issueby.Text, txtPackSize1.Text, txtdiscAmt1.Text, txtTaxableAmt1.Text, txtHsnCode1.Text, txtCgstRt1.Text, txtCgstAmt1.Text, txtSgstRt1.Text, txtSgstAmt1.Text, totgval.ToString(), txtPushChrg.Text, ddlSellingUnit1.SelectedValue.Trim(), txtBillNo.Text, txtRoundOff.Text.Trim(), txtCash.Text.Trim(), txtCard.Text.Trim(), txtewallet.Text.Trim(), txtNetBank.Text.Trim(), lbliType1.Text, ddltype.SelectedValue, txtDesc1.Text, txtChrgAmt1.Text, txtDesc2.Text, txtChrgAmt2.Text, cancel);
        }
        if (ddlMedi2.SelectedIndex > 0 && txtQty2.Text.Length > 0 /*&& txtUnitPrice2.Text.Length > 0 && txtTotalPrice2.Text.Length > 0 && txtBatch2.Text.Length > 0 && Calendar3.Text.Length > 0*/)
        {
            i++;
            string expdate2 = Calendar3.Text.Trim();//DateTime.ParseExact(Calendar3.Text, "dd/MM/yyyy", dtf);
            //theHelper.InsertSaleMedicine(i, compcode, yearcode, ddlMediSubGrp2.SelectedValue, ddlPatient.SelectedValue, ddlwing.SelectedValue, TextBox2.Text, TextBox4.Text, TextBox3.Text, TextBox5.Text, testdate.ToString("yyyy-MM-dd"), total.ToString(), TextBox7.Text, ddlMfg2.SelectedValue, ddlMediGrp2.SelectedValue, ddlMedi2.SelectedValue, txtBatch2.Text, testdate2.ToString("yyyy-MM-dd"), txtQty2.Text, txtUnitPrice2.Text, txtTotalPrice2.Text, Session["userName"].ToString(), rcvdate.ToString("yyyy-MM-dd"), receiveby.Text, issueby.Text);// for IPD
            theHelper.InsertSaleMedicineNew(i, compcode, yearcode, ddlMediSubGrp2.SelectedValue, patientid, TextBox2.Text, TextBox4.Text, TextBox3.Text, TextBox5.Text, testdate, total.ToString(), TextBox7.Text, ddlMfg2.SelectedValue, ddlMediGrp2.SelectedValue, ddlMedi2.SelectedValue, txtBatch2.Text, expdate2, txtQty2.Text, txtUnitPrice2.Text, txtTotalPrice2.Text, Session["userName"].ToString(), rcvdate, receiveby.Text, issueby.Text, txtPackSize2.Text, txtdiscAmt2.Text, txtTaxableAmt2.Text, txtHsnCode2.Text, txtCgstRt2.Text, txtCgstAmt2.Text, txtSgstRt2.Text, txtSgstAmt2.Text, totgval.ToString(), txtPushChrg.Text, ddlSellingUnit2.SelectedValue.Trim(), txtBillNo.Text, txtRoundOff.Text.Trim(), txtCash.Text.Trim(), txtCard.Text.Trim(), txtewallet.Text.Trim(), txtNetBank.Text.Trim(), lbliType2.Text, ddltype.SelectedValue, txtDesc1.Text, txtChrgAmt1.Text, txtDesc2.Text, txtChrgAmt2.Text, cancel);
        }
        if (ddlMedi3.SelectedIndex > 0 && txtQty3.Text.Length > 0 /*&& txtUnitPrice3.Text.Length > 0 && txtTotalPrice3.Text.Length > 0 && txtBatch3.Text.Length > 0 && Calendar4.Text.Length > 0*/)
        {
            i++;
            string expdate3 = Calendar4.Text.Trim();//DateTime.ParseExact(Calendar4.Text, "dd/MM/yyyy", dtf);
            //theHelper.InsertSaleMedicine(i, compcode, yearcode, ddlMediSubGrp3.SelectedValue, ddlPatient.SelectedValue, ddlwing.SelectedValue, TextBox2.Text, TextBox4.Text, TextBox3.Text, TextBox5.Text, testdate.ToString("yyyy-MM-dd"), total.ToString(), TextBox7.Text, ddlMfg3.SelectedValue, ddlMediGrp3.SelectedValue, ddlMedi3.SelectedValue, txtBatch3.Text, testdate3.ToString("yyyy-MM-dd"), txtQty3.Text, txtUnitPrice3.Text, txtTotalPrice3.Text, Session["userName"].ToString(), rcvdate.ToString("yyyy-MM-dd"), receiveby.Text, issueby.Text);// for IPD
            theHelper.InsertSaleMedicineNew(i, compcode, yearcode, ddlMediSubGrp3.SelectedValue, patientid, TextBox2.Text, TextBox4.Text, TextBox3.Text, TextBox5.Text, testdate, total.ToString(), TextBox7.Text, ddlMfg3.SelectedValue, ddlMediGrp3.SelectedValue, ddlMedi3.SelectedValue, txtBatch3.Text, expdate3, txtQty3.Text, txtUnitPrice3.Text, txtTotalPrice3.Text, Session["userName"].ToString(), rcvdate, receiveby.Text, issueby.Text, txtPackSize3.Text, txtdiscAmt3.Text, txtTaxableAmt3.Text, txtHsnCode3.Text, txtCgstRt3.Text, txtCgstAmt3.Text, txtSgstRt3.Text, txtSgstAmt3.Text, totgval.ToString(), txtPushChrg.Text, ddlSellingUnit3.SelectedValue.Trim(), txtBillNo.Text, txtRoundOff.Text.Trim(), txtCash.Text.Trim(), txtCard.Text.Trim(), txtewallet.Text.Trim(), txtNetBank.Text.Trim(), lbliType3.Text, ddltype.SelectedValue, txtDesc1.Text, txtChrgAmt1.Text, txtDesc2.Text, txtChrgAmt2.Text, cancel);
        }
        if (ddlMedi4.SelectedIndex > 0 && txtQty4.Text.Length > 0 /*&& txtUnitPrice4.Text.Length > 0 && txtTotalPrice4.Text.Length > 0 && txtBatch4.Text.Length > 0 && Calendar5.Text.Length > 0*/)
        {
            i++;
            string expdate4 = Calendar5.Text.Trim();//DateTime.ParseExact(Calendar5.Text, "dd/MM/yyyy", dtf);
            //theHelper.InsertSaleMedicine(i, compcode, yearcode, ddlMediSubGrp4.SelectedValue, ddlPatient.SelectedValue, ddlwing.SelectedValue, TextBox2.Text, TextBox4.Text, TextBox3.Text, TextBox5.Text, testdate.ToString("yyyy-MM-dd"), total.ToString(), TextBox7.Text, ddlMfg4.SelectedValue, ddlMediGrp4.SelectedValue, ddlMedi4.SelectedValue, txtBatch4.Text, testdate4.ToString("yyyy-MM-dd"), txtQty4.Text, txtUnitPrice4.Text, txtTotalPrice4.Text, Session["userName"].ToString(), rcvdate.ToString("yyyy-MM-dd"), receiveby.Text, issueby.Text); //For IPD
            theHelper.InsertSaleMedicineNew(i, compcode, yearcode, ddlMediSubGrp4.SelectedValue, patientid, TextBox2.Text, TextBox4.Text, TextBox3.Text, TextBox5.Text, testdate, total.ToString(), TextBox7.Text, ddlMfg4.SelectedValue, ddlMediGrp4.SelectedValue, ddlMedi4.SelectedValue, txtBatch4.Text, expdate4, txtQty4.Text, txtUnitPrice4.Text, txtTotalPrice4.Text, Session["userName"].ToString(), rcvdate, receiveby.Text, issueby.Text, txtPackSize4.Text, txtdiscAmt4.Text, txtTaxableAmt4.Text, txtHsnCode4.Text, txtCgstRt4.Text, txtCgstAmt4.Text, txtSgstRt4.Text, txtSgstAmt4.Text, totgval.ToString(), txtPushChrg.Text, ddlSellingUnit4.SelectedValue.Trim(), txtBillNo.Text, txtRoundOff.Text.Trim(), txtCash.Text.Trim(), txtCard.Text.Trim(), txtewallet.Text.Trim(), txtNetBank.Text.Trim(), lbliType4.Text, ddltype.SelectedValue, txtDesc1.Text, txtChrgAmt1.Text, txtDesc2.Text, txtChrgAmt2.Text, cancel);
        }
        if (ddlMedi5.SelectedIndex > 0 && txtQty5.Text.Length > 0 /*&& txtUnitPrice5.Text.Length > 0 && txtTotalPrice5.Text.Length > 0 && txtBatch5.Text.Length > 0 && Calendar6.Text.Length > 0*/)
        {
            i++;
            string expdate5 = Calendar6.Text.Trim();//DateTime.ParseExact(Calendar6.Text, "MM/dd/yyyy", dtf);
            //theHelper.InsertSaleMedicine(i, compcode, yearcode, ddlMediSubGrp5.SelectedValue, ddlPatient.SelectedValue, ddlwing.SelectedValue, TextBox2.Text, TextBox4.Text, TextBox3.Text, TextBox5.Text, testdate.ToString("yyyy-MM-dd"), total.ToString(), TextBox7.Text, ddlMfg5.SelectedValue, ddlMediGrp5.SelectedValue, ddlMedi5.SelectedValue, txtBatch5.Text, testdate5.ToString("yyyy-MM-dd"), txtQty5.Text, txtUnitPrice5.Text, txtTotalPrice5.Text, Session["userName"].ToString(), rcvdate.ToString("yyyy-MM-dd"), receiveby.Text, issueby.Text);//For IPD
            theHelper.InsertSaleMedicineNew(i, compcode, yearcode, ddlMediSubGrp5.SelectedValue, patientid, TextBox2.Text, TextBox4.Text, TextBox3.Text, TextBox5.Text, testdate, total.ToString(), TextBox7.Text, ddlMfg5.SelectedValue, ddlMediGrp5.SelectedValue, ddlMedi5.SelectedValue, txtBatch5.Text, expdate5, txtQty5.Text, txtUnitPrice5.Text, txtTotalPrice5.Text, Session["userName"].ToString(), rcvdate, receiveby.Text, issueby.Text, txtPackSize5.Text, txtdiscAmt5.Text, txtTaxableAmt5.Text, txtHsnCode5.Text, txtCgstRt5.Text, txtCgstAmt5.Text, txtSgstRt5.Text, txtSgstAmt5.Text, totgval.ToString(), txtPushChrg.Text, ddlSellingUnit5.SelectedValue.Trim(), txtBillNo.Text, txtRoundOff.Text.Trim(), txtCash.Text.Trim(), txtCard.Text.Trim(), txtewallet.Text.Trim(), txtNetBank.Text.Trim(), lbliType5.Text, ddltype.SelectedValue, txtDesc1.Text, txtChrgAmt1.Text, txtDesc2.Text, txtChrgAmt2.Text, cancel);
        }
        if (ddlMedi6.SelectedIndex > 0 && txtQty6.Text.Length > 0 /*&& txtUnitPrice6.Text.Length > 0 && txtTotalPrice6.Text.Length > 0 && txtBatch6.Text.Length > 0 && Calendar7.Text.Length > 0*/)
        {
            i++;
            string expdate6 = Calendar7.Text.Trim();//DateTime.ParseExact(Calendar7.Text, "dd/MM/yyyy", dtf);
            //theHelper.InsertSaleMedicine(i, compcode, yearcode, ddlMediSubGrp6.SelectedValue, ddlPatient.SelectedValue, ddlwing.SelectedValue, TextBox2.Text, TextBox4.Text, TextBox3.Text, TextBox5.Text, testdate.ToString("yyyy-MM-dd"), total.ToString(), TextBox7.Text, ddlMfg6.SelectedValue, ddlMediGrp6.SelectedValue, ddlMedi6.SelectedValue, txtBatch6.Text, testdate6.ToString("yyyy-MM-dd"), txtQty6.Text, txtUnitPrice6.Text, txtTotalPrice6.Text, Session["userName"].ToString(), rcvdate.ToString("yyyy-MM-dd"), receiveby.Text, issueby.Text);//For IPD
            theHelper.InsertSaleMedicineNew(i, compcode, yearcode, ddlMediSubGrp6.SelectedValue, patientid, TextBox2.Text, TextBox4.Text, TextBox3.Text, TextBox5.Text, testdate, total.ToString(), TextBox7.Text, ddlMfg6.SelectedValue, ddlMediGrp6.SelectedValue, ddlMedi6.SelectedValue, txtBatch6.Text, expdate6, txtQty6.Text, txtUnitPrice6.Text, txtTotalPrice6.Text, Session["userName"].ToString(), rcvdate, receiveby.Text, issueby.Text, txtPackSize6.Text, txtdiscAmt6.Text, txtTaxableAmt6.Text, txtHsnCode6.Text, txtCgstRt6.Text, txtCgstAmt6.Text, txtSgstRt6.Text, txtSgstAmt6.Text, totgval.ToString(), txtPushChrg.Text, ddlSellingUnit6.SelectedValue.Trim(), txtBillNo.Text, txtRoundOff.Text.Trim(), txtCash.Text.Trim(), txtCard.Text.Trim(), txtewallet.Text.Trim(), txtNetBank.Text.Trim(), lbliType6.Text, ddltype.SelectedValue, txtDesc1.Text, txtChrgAmt1.Text, txtDesc2.Text, txtChrgAmt2.Text, cancel);
        }
        if (ddlMedi7.SelectedIndex > 0 && txtQty7.Text.Length > 0 /*&& txtUnitPrice7.Text.Length > 0 && txtTotalPrice7.Text.Length > 0 && txtBatch7.Text.Length > 0 && Calendar8.Text.Length > 0*/)
        {
            i++;
            string expdate7 = Calendar8.Text.Trim();//DateTime.ParseExact(Calendar8.Text, "dd/MM/yyyy", dtf);
            //theHelper.InsertSaleMedicine(i, compcode, yearcode, ddlMediSubGrp7.SelectedValue, ddlPatient.SelectedValue, ddlwing.SelectedValue, TextBox2.Text, TextBox4.Text, TextBox3.Text, TextBox5.Text, testdate.ToString("yyyy-MM-dd"), total.ToString(), TextBox7.Text, ddlMfg7.SelectedValue, ddlMediGrp7.SelectedValue, ddlMedi7.SelectedValue, txtBatch7.Text, testdate7.ToString("yyyy-MM-dd"), txtQty7.Text, txtUnitPrice7.Text, txtTotalPrice7.Text, Session["userName"].ToString(), rcvdate.ToString("yyyy-MM-dd"), receiveby.Text, issueby.Text);//for ipd
            theHelper.InsertSaleMedicineNew(i, compcode, yearcode, ddlMediSubGrp7.SelectedValue, patientid, TextBox2.Text, TextBox4.Text, TextBox3.Text, TextBox5.Text, testdate, total.ToString(), TextBox7.Text, ddlMfg7.SelectedValue, ddlMediGrp7.SelectedValue, ddlMedi7.SelectedValue, txtBatch7.Text, expdate7, txtQty7.Text, txtUnitPrice7.Text, txtTotalPrice7.Text, Session["userName"].ToString(), rcvdate, receiveby.Text, issueby.Text, txtPackSize7.Text, txtdiscAmt7.Text, txtTaxableAmt7.Text, txtHsnCode7.Text, txtCgstRt7.Text, txtCgstAmt7.Text, txtSgstRt7.Text, txtSgstAmt7.Text, totgval.ToString(), txtPushChrg.Text, ddlSellingUnit7.SelectedValue.Trim(), txtBillNo.Text, txtRoundOff.Text.Trim(), txtCash.Text.Trim(), txtCard.Text.Trim(), txtewallet.Text.Trim(), txtNetBank.Text.Trim(), lbliType7.Text, ddltype.SelectedValue, txtDesc1.Text, txtChrgAmt1.Text, txtDesc2.Text, txtChrgAmt2.Text, cancel);
        }
        if (ddlMedi8.SelectedIndex > 0 && txtQty8.Text.Length > 0 /*&& txtUnitPrice8.Text.Length > 0 && txtTotalPrice8.Text.Length > 0 && txtBatch8.Text.Length > 0 && Calendar9.Text.Length > 0*/)
        {
            i++;
            string expdate8 = Calendar9.Text.Trim();//DateTime.ParseExact(Calendar9.Text, "dd/MM/yyyy", dtf);
            //theHelper.InsertSaleMedicine(i, compcode, yearcode, ddlMediSubGrp8.SelectedValue, ddlPatient.SelectedValue, ddlwing.SelectedValue, TextBox2.Text, TextBox4.Text, TextBox3.Text, TextBox5.Text, testdate.ToString("yyyy-MM-dd"), total.ToString(), TextBox7.Text, ddlMfg8.SelectedValue, ddlMediGrp8.SelectedValue, ddlMedi8.SelectedValue, txtBatch8.Text, testdate8.ToString("yyyy-MM-dd"), txtQty8.Text, txtUnitPrice8.Text, txtTotalPrice8.Text, Session["userName"].ToString(), rcvdate.ToString("yyyy-MM-dd"), receiveby.Text, issueby.Text);//for ipd
            theHelper.InsertSaleMedicineNew(i, compcode, yearcode, ddlMediSubGrp8.SelectedValue, patientid, TextBox2.Text, TextBox4.Text, TextBox3.Text, TextBox5.Text, testdate, total.ToString(), TextBox7.Text, ddlMfg8.SelectedValue, ddlMediGrp8.SelectedValue, ddlMedi8.SelectedValue, txtBatch8.Text, expdate8, txtQty8.Text, txtUnitPrice8.Text, txtTotalPrice8.Text, Session["userName"].ToString(), rcvdate, receiveby.Text, issueby.Text, txtPackSize8.Text, txtdiscAmt8.Text, txtTaxableAmt8.Text, txtHsnCode8.Text, txtCgstRt8.Text, txtCgstAmt8.Text, txtSgstRt8.Text, txtSgstAmt8.Text, totgval.ToString(), txtPushChrg.Text, ddlSellingUnit8.SelectedValue.Trim(), txtBillNo.Text, txtRoundOff.Text.Trim(), txtCash.Text.Trim(), txtCard.Text.Trim(), txtewallet.Text.Trim(), txtNetBank.Text.Trim(), lbliType8.Text, ddltype.SelectedValue, txtDesc1.Text, txtChrgAmt1.Text, txtDesc2.Text, txtChrgAmt2.Text, cancel);
        }
        if (ddlMedi9.SelectedIndex > 0 && txtQty9.Text.Length > 0 /*&& txtUnitPrice9.Text.Length > 0 && txtTotalPrice9.Text.Length > 0 && txtBatch9.Text.Length > 0 && Calendar10.Text.Length > 0*/)
        {
            i++;
            string expdate9 = Calendar10.Text.Trim();//DateTime.ParseExact(Calendar10.Text, "dd/MM/yyyy", dtf);
            //theHelper.InsertSaleMedicine(i, compcode, yearcode, ddlMediSubGrp9.SelectedValue, ddlPatient.SelectedValue, ddlwing.SelectedValue, TextBox2.Text, TextBox4.Text, TextBox3.Text, TextBox5.Text, testdate.ToString("yyyy-MM-dd"), total.ToString(), TextBox7.Text, ddlMfg9.SelectedValue, ddlMediGrp9.SelectedValue, ddlMedi9.SelectedValue, txtBatch9.Text, testdate9.ToString("yyyy-MM-dd"), txtQty9.Text, txtUnitPrice9.Text, txtTotalPrice9.Text, Session["userName"].ToString(), rcvdate.ToString("yyyy-MM-dd"), receiveby.Text, issueby.Text);//for ipd
            theHelper.InsertSaleMedicineNew(i, compcode, yearcode, ddlMediSubGrp9.SelectedValue, patientid, TextBox2.Text, TextBox4.Text, TextBox3.Text, TextBox5.Text, testdate, total.ToString(), TextBox7.Text, ddlMfg9.SelectedValue, ddlMediGrp9.SelectedValue, ddlMedi9.SelectedValue, txtBatch9.Text, expdate9, txtQty9.Text, txtUnitPrice9.Text, txtTotalPrice9.Text, Session["userName"].ToString(), rcvdate, receiveby.Text, issueby.Text, txtPackSize9.Text, txtdiscAmt9.Text, txtTaxableAmt9.Text, txtHsnCode9.Text, txtCgstRt9.Text, txtCgstAmt9.Text, txtSgstRt9.Text, txtSgstAmt9.Text, totgval.ToString(), txtPushChrg.Text, ddlSellingUnit9.SelectedValue.Trim(), txtBillNo.Text, txtRoundOff.Text.Trim(), txtCash.Text.Trim(), txtCard.Text.Trim(), txtewallet.Text.Trim(), txtNetBank.Text.Trim(), lbliType9.Text, ddltype.SelectedValue, txtDesc1.Text, txtChrgAmt1.Text, txtDesc2.Text, txtChrgAmt2.Text, cancel);
        }
        if (ddlMedi10.SelectedIndex > 0 && txtQty10.Text.Length > 0 /*&& txtUnitPrice10.Text.Length > 0 && txtTotalPrice10.Text.Length > 0 && txtBatch10.Text.Length > 0 && Calendar11.Text.Length > 0*/)
        {
            i++;
            string expdate10 = Calendar11.Text.Trim();//DateTime.ParseExact(Calendar11.Text, "dd/MM/yyyy", dtf);
            //theHelper.InsertSaleMedicine(i, compcode, yearcode, ddlMediSubGrp10.SelectedValue, ddlPatient.SelectedValue, ddlwing.SelectedValue, TextBox2.Text, TextBox4.Text, TextBox3.Text, TextBox5.Text, testdate.ToString("yyyy-MM-dd"), total.ToString(), TextBox7.Text, ddlMfg10.SelectedValue, ddlMediGrp10.SelectedValue, ddlMedi10.SelectedValue, txtBatch10.Text, testdate10.ToString("yyyy-MM-dd"), txtQty10.Text, txtUnitPrice10.Text, txtTotalPrice10.Text, Session["userName"].ToString(), rcvdate.ToString("yyyy-MM-dd"), receiveby.Text, issueby.Text);//for ipd
            theHelper.InsertSaleMedicineNew(i, compcode, yearcode, ddlMediSubGrp10.SelectedValue, patientid, TextBox2.Text, TextBox4.Text, TextBox3.Text, TextBox5.Text, testdate, total.ToString(), TextBox7.Text, ddlMfg10.SelectedValue, ddlMediGrp10.SelectedValue, ddlMedi10.SelectedValue, txtBatch10.Text, expdate10, txtQty10.Text, txtUnitPrice10.Text, txtTotalPrice10.Text, Session["userName"].ToString(), rcvdate, receiveby.Text, issueby.Text, txtPackSize10.Text, txtdiscAmt10.Text, txtTaxableAmt10.Text, txtHsnCode10.Text, txtCgstRt10.Text, txtCgstAmt10.Text, txtSgstRt10.Text, txtSgstAmt10.Text, totgval.ToString(), txtPushChrg.Text, ddlSellingUnit10.SelectedValue.Trim(), txtBillNo.Text, txtRoundOff.Text.Trim(), txtCash.Text.Trim(), txtCard.Text.Trim(), txtewallet.Text.Trim(), txtNetBank.Text.Trim(), lbliType10.Text, ddltype.SelectedValue, txtDesc1.Text, txtChrgAmt1.Text, txtDesc2.Text, txtChrgAmt2.Text, cancel);
        }
        if (ddlMedi11.SelectedIndex > 0 && txtQty11.Text.Length > 0 /*&& txtUnitPrice11.Text.Length > 0 && txtTotalPrice1.Text.Length > 0 && txtBatch11.Text.Length > 0 && Calendar12.Text.Length > 0*/)
        {
            i++;
            string expdate11 = Calendar12.Text.Trim();//DateTime.ParseExact(Calendar12.Text, "dd/MM/yyyy", dtf);
            //theHelper.InsertSaleMedicine(i, compcode, yearcode, ddlMediSubGrp11.SelectedValue, ddlPatient.SelectedValue, ddlwing.SelectedValue, TextBox2.Text, TextBox4.Text, TextBox3.Text, TextBox5.Text, testdate.ToString("yyyy-MM-dd"), total.ToString(), TextBox7.Text, ddlMfg11.SelectedValue, ddlMediGrp11.SelectedValue, ddlMedi11.SelectedValue, txtBatch11.Text, testdate11.ToString("yyyy-MM-dd"), txtQty11.Text, txtUnitPrice11.Text, txtTotalPrice11.Text, Session["userName"].ToString(), rcvdate.ToString("yyyy-MM-dd"), receiveby.Text, issueby.Text);//for ipd
            theHelper.InsertSaleMedicineNew(i, compcode, yearcode, ddlMediSubGrp11.SelectedValue, patientid, TextBox2.Text, TextBox4.Text, TextBox3.Text, TextBox5.Text, testdate, total.ToString(), TextBox7.Text, ddlMfg11.SelectedValue, ddlMediGrp11.SelectedValue, ddlMedi11.SelectedValue, txtBatch11.Text, expdate11, txtQty11.Text, txtUnitPrice11.Text, txtTotalPrice11.Text, Session["userName"].ToString(), rcvdate, receiveby.Text, issueby.Text, txtPackSize11.Text, txtdiscAmt11.Text, txtTaxableAmt11.Text, txtHsnCode11.Text, txtCgstRt11.Text, txtCgstAmt11.Text, txtSgstRt11.Text, txtSgstAmt11.Text, totgval.ToString(), txtPushChrg.Text, ddlSellingUnit11.SelectedValue.Trim(), txtBillNo.Text, txtRoundOff.Text.Trim(), txtCash.Text.Trim(), txtCard.Text.Trim(), txtewallet.Text.Trim(), txtNetBank.Text.Trim(), lbliType11.Text, ddltype.SelectedValue, txtDesc1.Text, txtChrgAmt1.Text, txtDesc2.Text, txtChrgAmt2.Text, cancel);
        }
        if (ddlMedi12.SelectedIndex > 0 && txtQty12.Text.Length > 0 /*&& txtUnitPrice12.Text.Length > 0 && txtTotalPrice12.Text.Length > 0 && txtBatch12.Text.Length > 0 && Calendar13.Text.Length > 0*/)
        {
            i++;
            string expdate12 = Calendar13.Text.Trim();//DateTime.ParseExact(Calendar13.Text, "dd/MM/yyyy", dtf);
            //theHelper.InsertSaleMedicine(i, compcode, yearcode, ddlMediSubGrp12.SelectedValue, ddlPatient.SelectedValue, ddlwing.SelectedValue, TextBox2.Text, TextBox4.Text, TextBox3.Text, TextBox5.Text, testdate.ToString("yyyy-MM-dd"), total.ToString(), TextBox7.Text, ddlMfg12.SelectedValue, ddlMediGrp12.SelectedValue, ddlMedi12.SelectedValue, txtBatch12.Text, testdate12.ToString("yyyy-MM-dd"), txtQty12.Text, txtUnitPrice12.Text, txtTotalPrice12.Text, Session["userName"].ToString(), rcvdate.ToString("yyyy-MM-dd"), receiveby.Text, issueby.Text);//for ipd
            theHelper.InsertSaleMedicineNew(i, compcode, yearcode, ddlMediSubGrp12.SelectedValue, patientid, TextBox2.Text, TextBox4.Text, TextBox3.Text, TextBox5.Text, testdate, total.ToString(), TextBox7.Text, ddlMfg12.SelectedValue, ddlMediGrp12.SelectedValue, ddlMedi12.SelectedValue, txtBatch12.Text, expdate12, txtQty12.Text, txtUnitPrice12.Text, txtTotalPrice12.Text, Session["userName"].ToString(), rcvdate, receiveby.Text, issueby.Text, txtPackSize12.Text, txtdiscAmt12.Text, txtTaxableAmt12.Text, txtHsnCode12.Text, txtCgstRt12.Text, txtCgstAmt12.Text, txtSgstRt12.Text, txtSgstAmt12.Text, totgval.ToString(), txtPushChrg.Text, ddlSellingUnit12.SelectedValue.Trim(), txtBillNo.Text, txtRoundOff.Text.Trim(), txtCash.Text.Trim(), txtCard.Text.Trim(), txtewallet.Text.Trim(), txtNetBank.Text.Trim(), lbliType12.Text, ddltype.SelectedValue, txtDesc1.Text, txtChrgAmt1.Text, txtDesc2.Text, txtChrgAmt2.Text, cancel);
        }
        theHelper.InsertUpdateSaleMedicineHead(compcode, yearcode, patientid, TextBox2.Text, TextBox4.Text, TextBox3.Text, TextBox5.Text, testdate, total.ToString(), TextBox7.Text, Session["userName"].ToString(), rcvdate, receiveby.Text, issueby.Text, totgval.ToString(), txtPushChrg.Text, txtBillNo.Text, txtRoundOff.Text.Trim(), txtCash.Text.Trim(), txtCard.Text.Trim(), txtewallet.Text.Trim(), txtNetBank.Text.Trim(), lbliType12.Text, ddltype.SelectedValue, txtDesc1.Text, txtChrgAmt1.Text, txtDesc2.Text, txtChrgAmt2.Text, cancel);
        //theHelper.InsUpdInv(compcode, yearcode, TextBox5.Text.Trim(), "I", Session["userName"].ToString());
    }
    protected void ddlMediGrp1_SelectedIndexChanged(object sender, EventArgs e)
    {
        ddlMediSubGrp1.DataSource = theHelper.DropdownSubGroup(Session["CoCode"].ToString().Trim(), ddlMediGrp1.SelectedValue);
        ddlMediSubGrp1.DataTextField = "SubGrName";
        ddlMediSubGrp1.DataValueField = "ID";
        ddlMediSubGrp1.DataBind();
        ddlMediSubGrp1.Items.Insert(0, new ListItem("--Select--", "0"));
    }
    protected void ddlMediGrp2_SelectedIndexChanged(object sender, EventArgs e)
    {
        ddlMediSubGrp2.DataSource = theHelper.DropdownSubGroup(Session["CoCode"].ToString().Trim(), ddlMediGrp2.SelectedValue);
        ddlMediSubGrp2.DataTextField = "SubGrName";
        ddlMediSubGrp2.DataValueField = "ID";
        ddlMediSubGrp2.DataBind();
        ddlMediSubGrp2.Items.Insert(0, new ListItem("--Select--", "0"));
    }
    protected void ddlMediGrp3_SelectedIndexChanged(object sender, EventArgs e)
    {
        ddlMediSubGrp3.DataSource = theHelper.DropdownSubGroup(Session["CoCode"].ToString().Trim(), ddlMediGrp3.SelectedValue);
        ddlMediSubGrp3.DataTextField = "SubGrName";
        ddlMediSubGrp3.DataValueField = "ID";
        ddlMediSubGrp3.DataBind();
        ddlMediSubGrp3.Items.Insert(0, new ListItem("--Select--", "0"));
    }
    protected void ddlMediGrp4_SelectedIndexChanged(object sender, EventArgs e)
    {
        ddlMediSubGrp4.DataSource = theHelper.DropdownSubGroup(Session["CoCode"].ToString().Trim(), ddlMediGrp4.SelectedValue);
        ddlMediSubGrp4.DataTextField = "SubGrName";
        ddlMediSubGrp4.DataValueField = "ID";
        ddlMediSubGrp4.DataBind();
        ddlMediSubGrp4.Items.Insert(0, new ListItem("--Select--", "0"));
    }
    protected void ddlMediGrp5_SelectedIndexChanged(object sender, EventArgs e)
    {
        ddlMediSubGrp5.DataSource = theHelper.DropdownSubGroup(Session["CoCode"].ToString().Trim(), ddlMediGrp5.SelectedValue);
        ddlMediSubGrp5.DataTextField = "SubGrName";
        ddlMediSubGrp5.DataValueField = "ID";
        ddlMediSubGrp5.DataBind();
        ddlMediSubGrp5.Items.Insert(0, new ListItem("--Select--", "0"));
    }
    protected void ddlMediGrp6_SelectedIndexChanged(object sender, EventArgs e)
    {

        ddlMediSubGrp6.DataSource = theHelper.DropdownSubGroup(Session["CoCode"].ToString().Trim(), ddlMediGrp6.SelectedValue);
        ddlMediSubGrp6.DataTextField = "SubGrName";
        ddlMediSubGrp6.DataValueField = "ID";
        ddlMediSubGrp6.DataBind();
        ddlMediSubGrp6.Items.Insert(0, new ListItem("--Select--", "0"));
    }
    protected void ddlMediGrp7_SelectedIndexChanged(object sender, EventArgs e)
    {
        ddlMediSubGrp7.DataSource = theHelper.DropdownSubGroup(Session["CoCode"].ToString().Trim(), ddlMediGrp7.SelectedValue);
        ddlMediSubGrp7.DataTextField = "SubGrName";
        ddlMediSubGrp7.DataValueField = "ID";
        ddlMediSubGrp7.DataBind();
        ddlMediSubGrp7.Items.Insert(0, new ListItem("--Select--", "0"));
    }
    protected void ddlMediGrp8_SelectedIndexChanged(object sender, EventArgs e)
    {
        ddlMediSubGrp8.DataSource = theHelper.DropdownSubGroup(Session["CoCode"].ToString().Trim(), ddlMediGrp8.SelectedValue);
        ddlMediSubGrp8.DataTextField = "SubGrName";
        ddlMediSubGrp8.DataValueField = "ID";
        ddlMediSubGrp8.DataBind();
        ddlMediSubGrp8.Items.Insert(0, new ListItem("--Select--", "0"));
    }
    protected void ddlMediGrp9_SelectedIndexChanged(object sender, EventArgs e)
    {

        ddlMediSubGrp9.DataSource = theHelper.DropdownSubGroup(Session["CoCode"].ToString().Trim(), ddlMediGrp9.SelectedValue);
        ddlMediSubGrp9.DataTextField = "SubGrName";
        ddlMediSubGrp9.DataValueField = "ID";
        ddlMediSubGrp9.DataBind();
        ddlMediSubGrp9.Items.Insert(0, new ListItem("--Select--", "0"));
    }
    protected void ddlMediGrp10_SelectedIndexChanged(object sender, EventArgs e)
    {
        ddlMediSubGrp10.DataSource = theHelper.DropdownSubGroup(Session["CoCode"].ToString().Trim(), ddlMediGrp10.SelectedValue);
        ddlMediSubGrp10.DataTextField = "SubGrName";
        ddlMediSubGrp10.DataValueField = "ID";
        ddlMediSubGrp10.DataBind();
        ddlMediSubGrp10.Items.Insert(0, new ListItem("--Select--", "0"));
    }
    protected void ddlMediGrp11_SelectedIndexChanged(object sender, EventArgs e)
    {
        ddlMediSubGrp11.DataSource = theHelper.DropdownSubGroup(Session["CoCode"].ToString().Trim(), ddlMediGrp11.SelectedValue);
        ddlMediSubGrp11.DataTextField = "SubGrName";
        ddlMediSubGrp11.DataValueField = "ID";
        ddlMediSubGrp11.DataBind();
        ddlMediSubGrp11.Items.Insert(0, new ListItem("--Select--", "0"));
    }
    protected void ddlMediGrp12_SelectedIndexChanged(object sender, EventArgs e)
    {
        ddlMediSubGrp12.DataSource = theHelper.DropdownSubGroup(Session["CoCode"].ToString().Trim(), ddlMediGrp12.SelectedValue);
        ddlMediSubGrp12.DataTextField = "SubGrName";
        ddlMediSubGrp12.DataValueField = "ID";
        ddlMediSubGrp12.DataBind();
        ddlMediSubGrp12.Items.Insert(0, new ListItem("--Select--", "0"));
    }
    public void BatchNoFill(string value, string Mfgvalue, DropDownList ddlBatchNo)
    {

        ddlBatchNo.Items.Clear();
        DataTable dt = theHelper.DropdownBatchNo(Session["CoCode"].ToString().Trim(), Session["YearCode"].ToString().Trim(), value, Mfgvalue, hdnInsUpd.Value, TextBox5.Text);
        ddlBatchNo.DataSource = dt;
        ddlBatchNo.DataTextField = "BatchExDate";
        ddlBatchNo.DataValueField = "BatchNo";
        ddlBatchNo.DataBind();

        //TextBox2.Text = dt.Rows[0]["DoseName"].ToString();
    }
    public void Expirydt(string mid, string mfgid, string Batchno, TextBox txtExpiry, TextBox txtUnitPrice, TextBox expyrmnth)
    {
        txtExpiry.Text = "";
        DataTable dt = theHelper.ExpiryDateBatchWise(Session["CoCode"].ToString().Trim(), Session["YearCode"].ToString().Trim(), mid, mfgid, Batchno);
        if (dt.Rows.Count > 0)
        {
            expyrmnth.Text = dt.Rows[0]["ExpYearMonth"].ToString();
            txtExpiry.Text = dt.Rows[0]["ExDate"].ToString();
            txtUnitPrice.Text = dt.Rows[0]["MRP"].ToString();
        }
        else
        {
            expyrmnth.Text = "";
            txtExpiry.Text = "";
            txtUnitPrice.Text = "";
        }
    }


    private void TaxDetails(string medId, string mgfId, TextBox txtHsnCode, TextBox txtCgstRt, TextBox txtSgstRt, TextBox txtdiscAmt, TextBox txtUnit, Label itype)
    {
        DataTable dt = theHelper.getTaxDetails(Session["CoCode"].ToString().Trim(), Session["YearCode"].ToString().Trim(), medId, mgfId);
        if (dt.Rows.Count > 0)
        {
            txtHsnCode.Text = dt.Rows[0]["HSNCode"].ToString();
            txtCgstRt.Text = dt.Rows[0]["CGSTRate"].ToString();
            txtSgstRt.Text = dt.Rows[0]["SGSTRate"].ToString();
            txtUnit.Text = dt.Rows[0]["UnitName"].ToString();
            itype.Text = dt.Rows[0]["itype"].ToString();
            txtdiscAmt.Text = "0.00";
            
        }
        else
        {
            txtHsnCode.Text = "";
            txtCgstRt.Text = "0.00";
            txtSgstRt.Text = "0.00";
            txtdiscAmt.Text = "0.00";
            itype.Text = "";
            txtUnit.Text = "";
        }
    }
    public void PopulateSellingUnit(string medID,DropDownList ddlSellUnit)
    {
        DataTable dtUnit = theHelper.getMedicineUnits(Session["CoCode"].ToString().Trim(), medID);
        ddlSellUnit.Items.Clear();
        ddlSellUnit.Items.Insert(0, new ListItem(dtUnit.Rows[0]["PurUnitName"].ToString().Trim(), dtUnit.Rows[0]["UnitId"].ToString().Trim()));
        ddlSellUnit.Items.Insert(1, new ListItem(dtUnit.Rows[0]["sellUnitName"].ToString().Trim(), dtUnit.Rows[0]["SellingUnit"].ToString().Trim()));
    }
    protected void ddlMedi1_SelectedIndexChanged(object sender, EventArgs e)
    {
        txtreg.Text = hdnregno.Value;
        if (ddlMedi1.SelectedValue == "0")
        {
            ResetRow(1);
        }
        else
        {
            MfgGrpSgrpFill(ddlMedi1.SelectedValue, 1);
            TaxDetails(ddlMedi1.SelectedValue, ddlMfg1.SelectedValue, txtHsnCode1, txtCgstRt1, txtSgstRt1, txtdiscAmt1, txtUnit1, lbliType1);
            if (lbliType1.Text == "C")
            {
                txtQty1.Text = "1";
                txtUnitPrice1.Text = "0.00";
                txtBatch1.Enabled = false;
                expdt1.Enabled = false;
                txtStock1.Enabled = false;

            }
            else
            {
                txtUnitPrice1.Text = theHelper.GetPurchasePricePerUnit(Session["CoCode"].ToString().Trim(), Session["YearCode"].ToString().Trim(), ddlMedi1.SelectedValue).ToString();
                BatchNoFill(ddlMedi1.SelectedValue, ddlMfg1.SelectedValue, txtBatch1);
                Expirydt(ddlMedi1.SelectedValue, ddlMfg1.SelectedValue, txtBatch1.SelectedValue, Calendar2, txtUnitPrice1, expdt1);

                DataTable dt = theHelper.AvailableStock(Session["CoCode"].ToString().Trim(), Session["YearCode"].ToString().Trim(), ddlMedi1.SelectedValue, ddlMfg1.SelectedValue, txtBatch1.SelectedValue);
                if (dt.Rows.Count > 0)
                {
                        txtStock1.Text = Convert.ToDecimal(dt.Rows[0]["AvailQty"]).ToString();
                }
            }
            PopulateSellingUnit(ddlMedi1.SelectedValue.Trim(), ddlSellingUnit1);
            txtPackSize1.Text = "1";
            txtQty1_TextChanged(sender, e);
        }
        //calculateTotal();
    }
    protected void ddlMedi2_SelectedIndexChanged(object sender, EventArgs e)
    {
        txtreg.Text = hdnregno.Value;
        if (ddlMedi2.SelectedValue == "0")
        {
            ResetRow(2);
        }
        else
        {
            MfgGrpSgrpFill(ddlMedi2.SelectedValue, 2);
            TaxDetails(ddlMedi2.SelectedValue, ddlMfg2.SelectedValue, txtHsnCode2, txtCgstRt2, txtSgstRt2, txtdiscAmt2, txtUnit2, lbliType2);
            if (lbliType2.Text == "C")
            {
                txtQty2.Text = "1";
                txtUnitPrice2.Text = "0.00";
                txtBatch2.Enabled = false;
                expdt2.Enabled = false;
                txtStock2.Enabled = false;

            }
            else
            {
                txtUnitPrice2.Text = theHelper.GetPurchasePricePerUnit(Session["CoCode"].ToString().Trim(), Session["YearCode"].ToString().Trim(), ddlMedi2.SelectedValue).ToString();
                BatchNoFill(ddlMedi2.SelectedValue, ddlMfg2.SelectedValue, txtBatch2);
                Expirydt(ddlMedi2.SelectedValue, ddlMfg2.SelectedValue, txtBatch2.SelectedValue, Calendar3, txtUnitPrice2, expdt2);

                DataTable dt = theHelper.AvailableStock(Session["CoCode"].ToString().Trim(), Session["YearCode"].ToString().Trim(), ddlMedi2.SelectedValue, ddlMfg2.SelectedValue, txtBatch2.SelectedValue);
                if (dt.Rows.Count > 0)
                {
                    txtStock2.Text = Convert.ToDecimal(dt.Rows[0]["AvailQty"]).ToString();
                }
            }
            PopulateSellingUnit(ddlMedi2.SelectedValue.Trim(), ddlSellingUnit2);
            txtPackSize2.Text = "1";
            txtQty2_TextChanged(sender, e);
        }
        
    }
    protected void ddlMedi3_SelectedIndexChanged(object sender, EventArgs e)
    {
        txtreg.Text = hdnregno.Value;
        if (ddlMedi3.SelectedValue == "0")
        {
            ResetRow(3);
        }
        else
        {
            MfgGrpSgrpFill(ddlMedi3.SelectedValue, 3);
            TaxDetails(ddlMedi3.SelectedValue, ddlMfg3.SelectedValue, txtHsnCode3, txtCgstRt3, txtSgstRt3, txtdiscAmt3, txtUnit3, lbliType3);
            if (lbliType3.Text == "C")
            {
                txtQty3.Text = "1";
                txtUnitPrice3.Text = "0.00";
                txtBatch3.Enabled = false;
                expdt3.Enabled = false;
                txtStock3.Enabled = false;

            }
            else
            {
                txtUnitPrice3.Text = theHelper.GetPurchasePricePerUnit(Session["CoCode"].ToString().Trim(), Session["YearCode"].ToString().Trim(), ddlMedi3.SelectedValue).ToString();
                BatchNoFill(ddlMedi3.SelectedValue, ddlMfg3.SelectedValue, txtBatch3);
                Expirydt(ddlMedi3.SelectedValue, ddlMfg3.SelectedValue, txtBatch3.SelectedValue, Calendar4, txtUnitPrice3, expdt3);

                DataTable dt = theHelper.AvailableStock(Session["CoCode"].ToString().Trim(), Session["YearCode"].ToString().Trim(), ddlMedi3.SelectedValue, ddlMfg3.SelectedValue, txtBatch3.SelectedValue);
                if (dt.Rows.Count > 0)
                {
                    txtStock3.Text = Convert.ToDecimal(dt.Rows[0]["AvailQty"]).ToString();
                }
            }
            PopulateSellingUnit(ddlMedi3.SelectedValue.Trim(), ddlSellingUnit3);
            txtPackSize3.Text = "1";
            txtQty3_TextChanged(sender, e);
        }
        
    }
    protected void ddlMedi4_SelectedIndexChanged(object sender, EventArgs e)
    {
        txtreg.Text = hdnregno.Value;
        if (ddlMedi4.SelectedValue == "0")
        {
            ResetRow(4);
        }
        else
        {
            MfgGrpSgrpFill(ddlMedi4.SelectedValue, 4);
            TaxDetails(ddlMedi4.SelectedValue, ddlMfg4.SelectedValue, txtHsnCode4, txtCgstRt4, txtSgstRt4, txtdiscAmt4, txtUnit4, lbliType4);
            if (lbliType4.Text == "C")
            {
                txtQty4.Text = "1";
                txtUnitPrice4.Text = "0.00";
                txtBatch4.Enabled = false;
                expdt4.Enabled = false;
                txtStock4.Enabled = false;

            }
            else
            {
                txtUnitPrice4.Text = theHelper.GetPurchasePricePerUnit(Session["CoCode"].ToString().Trim(), Session["YearCode"].ToString().Trim(), ddlMedi4.SelectedValue).ToString();
                BatchNoFill(ddlMedi4.SelectedValue, ddlMfg4.SelectedValue, txtBatch4);
                Expirydt(ddlMedi4.SelectedValue, ddlMfg4.SelectedValue, txtBatch4.SelectedValue, Calendar5, txtUnitPrice4, expdt4);

                DataTable dt = theHelper.AvailableStock(Session["CoCode"].ToString().Trim(), Session["YearCode"].ToString().Trim(), ddlMedi4.SelectedValue, ddlMfg4.SelectedValue, txtBatch4.SelectedValue);
                if (dt.Rows.Count > 0)
                {
                    txtStock4.Text = Convert.ToDecimal(dt.Rows[0]["AvailQty"]).ToString();
                }
            }
            PopulateSellingUnit(ddlMedi4.SelectedValue.Trim(), ddlSellingUnit4);
            txtPackSize4.Text = "1";
            txtQty4_TextChanged(sender, e);
        }
        
    }
    protected void ddlMedi5_SelectedIndexChanged(object sender, EventArgs e)
    {
        txtreg.Text = hdnregno.Value;
        if (ddlMedi5.SelectedValue == "0")
        {
            ResetRow(5);
        }
        else
        {
            MfgGrpSgrpFill(ddlMedi5.SelectedValue, 5);
            TaxDetails(ddlMedi5.SelectedValue, ddlMfg5.SelectedValue, txtHsnCode5, txtCgstRt5, txtSgstRt5, txtdiscAmt5, txtUnit5, lbliType5);
            if (lbliType5.Text == "C")
            {
                txtQty5.Text = "1";
                txtUnitPrice5.Text = "0.00";
                txtBatch5.Enabled = false;
                expdt5.Enabled = false;
                txtStock5.Enabled = false;

            }
            else
            {
                txtUnitPrice5.Text = theHelper.GetPurchasePricePerUnit(Session["CoCode"].ToString().Trim(), Session["YearCode"].ToString().Trim(), ddlMedi5.SelectedValue).ToString();
                BatchNoFill(ddlMedi5.SelectedValue, ddlMfg5.SelectedValue, txtBatch5);
                Expirydt(ddlMedi5.SelectedValue, ddlMfg5.SelectedValue, txtBatch5.SelectedValue, Calendar6, txtUnitPrice5, expdt5);

                DataTable dt = theHelper.AvailableStock(Session["CoCode"].ToString().Trim(), Session["YearCode"].ToString().Trim(), ddlMedi5.SelectedValue, ddlMfg5.SelectedValue, txtBatch5.SelectedValue);
                if (dt.Rows.Count > 0)
                {
                    txtStock5.Text = Convert.ToDecimal(dt.Rows[0]["AvailQty"]).ToString();
                }
            }
            PopulateSellingUnit(ddlMedi5.SelectedValue.Trim(), ddlSellingUnit5);
            txtPackSize5.Text = "1";
            txtQty5_TextChanged(sender, e);
        }
        
    }
    protected void ddlMedi6_SelectedIndexChanged(object sender, EventArgs e)
    {
        txtreg.Text = hdnregno.Value;
        if (ddlMedi6.SelectedValue == "0")
        {
            ResetRow(6);
        }
        else
        {
            MfgGrpSgrpFill(ddlMedi6.SelectedValue, 6);
            TaxDetails(ddlMedi6.SelectedValue, ddlMfg6.SelectedValue, txtHsnCode6, txtCgstRt6, txtSgstRt6, txtdiscAmt6, txtUnit6, lbliType6);
            if (lbliType6.Text == "C")
            {
                txtQty6.Text = "1";
                txtUnitPrice6.Text = "0.00";
                txtBatch6.Enabled = false;
                expdt6.Enabled = false;
                txtStock6.Enabled = false;

            }
            else
            {
                txtUnitPrice6.Text = theHelper.GetPurchasePricePerUnit(Session["CoCode"].ToString().Trim(), Session["YearCode"].ToString().Trim(), ddlMedi6.SelectedValue).ToString();
                BatchNoFill(ddlMedi6.SelectedValue, ddlMfg6.SelectedValue, txtBatch6);
                Expirydt(ddlMedi6.SelectedValue, ddlMfg6.SelectedValue, txtBatch6.SelectedValue, Calendar7, txtUnitPrice6, expdt6);

                DataTable dt = theHelper.AvailableStock(Session["CoCode"].ToString().Trim(), Session["YearCode"].ToString().Trim(), ddlMedi6.SelectedValue, ddlMfg6.SelectedValue, txtBatch6.SelectedValue);
                if (dt.Rows.Count > 0)
                {
                    txtStock6.Text = Convert.ToDecimal(dt.Rows[0]["AvailQty"]).ToString();
                }
            }
            PopulateSellingUnit(ddlMedi6.SelectedValue.Trim(), ddlSellingUnit6);
            txtPackSize6.Text = "1";
            txtQty6_TextChanged(sender, e);
        }
        
    }
    protected void ddlMedi7_SelectedIndexChanged(object sender, EventArgs e)
    {
        txtreg.Text = hdnregno.Value;
        if (ddlMedi7.SelectedValue == "0")
        {
            ResetRow(7);
        }
        else
        {
            MfgGrpSgrpFill(ddlMedi7.SelectedValue, 7);
            TaxDetails(ddlMedi7.SelectedValue, ddlMfg7.SelectedValue, txtHsnCode7, txtCgstRt7, txtSgstRt7, txtdiscAmt7, txtUnit7, lbliType7);
            if (lbliType7.Text == "C")
            {
                txtQty7.Text = "1";
                txtUnitPrice7.Text = "0.00";
                txtBatch7.Enabled = false;
                expdt7.Enabled = false;
                txtStock7.Enabled = false;

            }
            else
            {
                txtUnitPrice7.Text = theHelper.GetPurchasePricePerUnit(Session["CoCode"].ToString().Trim(), Session["YearCode"].ToString().Trim(), ddlMedi7.SelectedValue).ToString();
                BatchNoFill(ddlMedi7.SelectedValue, ddlMfg7.SelectedValue, txtBatch7);
                Expirydt(ddlMedi7.SelectedValue, ddlMfg7.SelectedValue, txtBatch7.SelectedValue, Calendar8, txtUnitPrice7, expdt7);

                DataTable dt = theHelper.AvailableStock(Session["CoCode"].ToString().Trim(), Session["YearCode"].ToString().Trim(), ddlMedi7.SelectedValue, ddlMfg7.SelectedValue, txtBatch7.SelectedValue);
                if (dt.Rows.Count > 0)
                {
                    txtStock7.Text = Convert.ToDecimal(dt.Rows[0]["AvailQty"]).ToString();
                }
            }
            PopulateSellingUnit(ddlMedi7.SelectedValue.Trim(), ddlSellingUnit7);
            txtPackSize7.Text = "1";
            txtQty7_TextChanged(sender, e);
        }
        
    }
    protected void ddlMedi8_SelectedIndexChanged(object sender, EventArgs e)
    {
        txtreg.Text = hdnregno.Value;
        if (ddlMedi8.SelectedValue == "0")
        {
            ResetRow(8);
        }
        else
        {
            MfgGrpSgrpFill(ddlMedi8.SelectedValue, 8);
            TaxDetails(ddlMedi8.SelectedValue, ddlMfg8.SelectedValue, txtHsnCode8, txtCgstRt8, txtSgstRt8, txtdiscAmt8, txtUnit8, lbliType8);
            if (lbliType8.Text == "C")
            {
                txtQty8.Text = "1";
                txtUnitPrice8.Text = "0.00";
                txtBatch8.Enabled = false;
                expdt8.Enabled = false;
                txtStock8.Enabled = false;

            }
            else
            {
                txtUnitPrice8.Text = theHelper.GetPurchasePricePerUnit(Session["CoCode"].ToString().Trim(), Session["YearCode"].ToString().Trim(), ddlMedi8.SelectedValue).ToString();
                BatchNoFill(ddlMedi8.SelectedValue, ddlMfg8.SelectedValue, txtBatch8);
                Expirydt(ddlMedi8.SelectedValue, ddlMfg8.SelectedValue, txtBatch8.SelectedValue, Calendar9, txtUnitPrice8, expdt8);

                DataTable dt = theHelper.AvailableStock(Session["CoCode"].ToString().Trim(), Session["YearCode"].ToString().Trim(), ddlMedi8.SelectedValue, ddlMfg8.SelectedValue, txtBatch8.SelectedValue);
                if (dt.Rows.Count > 0)
                {
                    txtStock8.Text = Convert.ToDecimal(dt.Rows[0]["AvailQty"]).ToString();
                }
            }
            PopulateSellingUnit(ddlMedi8.SelectedValue.Trim(), ddlSellingUnit8);
            txtPackSize8.Text = "1";
            txtQty8_TextChanged(sender, e);
        }
        
    }
    protected void ddlMedi9_SelectedIndexChanged(object sender, EventArgs e)
    {
        txtreg.Text = hdnregno.Value;
        if (ddlMedi9.SelectedValue == "0")
        {
            ResetRow(9);
        }
        else
        {
            MfgGrpSgrpFill(ddlMedi9.SelectedValue, 9);
            TaxDetails(ddlMedi9.SelectedValue, ddlMfg9.SelectedValue, txtHsnCode9, txtCgstRt9, txtSgstRt9, txtdiscAmt9, txtUnit9, lbliType9);
            if (lbliType9.Text == "C")
            {
                txtQty9.Text = "1";
                txtUnitPrice9.Text = "0.00";
                txtBatch9.Enabled = false;
                expdt9.Enabled = false;
                txtStock9.Enabled = false;

            }
            else
            {
                txtUnitPrice9.Text = theHelper.GetPurchasePricePerUnit(Session["CoCode"].ToString().Trim(), Session["YearCode"].ToString().Trim(), ddlMedi9.SelectedValue).ToString();
                BatchNoFill(ddlMedi9.SelectedValue, ddlMfg9.SelectedValue, txtBatch9);
                Expirydt(ddlMedi9.SelectedValue, ddlMfg9.SelectedValue, txtBatch9.SelectedValue, Calendar10, txtUnitPrice9, expdt9);

                DataTable dt = theHelper.AvailableStock(Session["CoCode"].ToString().Trim(), Session["YearCode"].ToString().Trim(), ddlMedi9.SelectedValue, ddlMfg9.SelectedValue, txtBatch9.SelectedValue);
                if (dt.Rows.Count > 0)
                {
                    txtStock9.Text = Convert.ToDecimal(dt.Rows[0]["AvailQty"]).ToString();
                }
            }
            PopulateSellingUnit(ddlMedi9.SelectedValue.Trim(), ddlSellingUnit9);
            txtPackSize9.Text = "1";
            txtQty9_TextChanged(sender, e);
        }
        
    }
    protected void ddlMedi10_SelectedIndexChanged(object sender, EventArgs e)
    {
        txtreg.Text = hdnregno.Value;
        if (ddlMedi10.SelectedValue == "0")
        {
            ResetRow(10);
        }
        else
        {
            MfgGrpSgrpFill(ddlMedi10.SelectedValue, 10);
            TaxDetails(ddlMedi10.SelectedValue, ddlMfg10.SelectedValue, txtHsnCode10, txtCgstRt10, txtSgstRt10, txtdiscAmt10, txtUnit10, lbliType10);
            if (lbliType10.Text == "C")
            {
                txtQty10.Text = "1";
                txtUnitPrice10.Text = "0.00";
                txtBatch10.Enabled = false;
                expdt10.Enabled = false;
                txtStock10.Enabled = false;

            }
            else
            {
                txtUnitPrice10.Text = theHelper.GetPurchasePricePerUnit(Session["CoCode"].ToString().Trim(), Session["YearCode"].ToString().Trim(), ddlMedi10.SelectedValue).ToString();
                BatchNoFill(ddlMedi10.SelectedValue, ddlMfg10.SelectedValue, txtBatch10);
                Expirydt(ddlMedi10.SelectedValue, ddlMfg10.SelectedValue, txtBatch10.SelectedValue, Calendar11, txtUnitPrice10, expdt10);

                DataTable dt = theHelper.AvailableStock(Session["CoCode"].ToString().Trim(), Session["YearCode"].ToString().Trim(), ddlMedi10.SelectedValue, ddlMfg10.SelectedValue, txtBatch10.SelectedValue);
                if (dt.Rows.Count > 0)
                {
                    txtStock10.Text = Convert.ToDecimal(dt.Rows[0]["AvailQty"]).ToString();
                }
            }
            PopulateSellingUnit(ddlMedi10.SelectedValue.Trim(), ddlSellingUnit10);
            txtPackSize10.Text = "1";
            txtQty10_TextChanged(sender, e);
        }
        
    }
    protected void ddlMedi11_SelectedIndexChanged(object sender, EventArgs e)
    {
        txtreg.Text = hdnregno.Value;
        if (ddlMedi11.SelectedValue == "0")
        {
            ResetRow(11);
        }
        else
        {
            MfgGrpSgrpFill(ddlMedi11.SelectedValue, 11);
            TaxDetails(ddlMedi11.SelectedValue, ddlMfg11.SelectedValue, txtHsnCode11, txtCgstRt11, txtSgstRt11, txtdiscAmt11, txtUnit11, lbliType11);
            if (lbliType11.Text == "C")
            {
                txtQty11.Text = "1";
                txtUnitPrice11.Text = "0.00";
                txtBatch11.Enabled = false;
                expdt11.Enabled = false;
                txtStock11.Enabled = false;

            }
            else
            {
                txtUnitPrice11.Text = theHelper.GetPurchasePricePerUnit(Session["CoCode"].ToString().Trim(), Session["YearCode"].ToString().Trim(), ddlMedi11.SelectedValue).ToString();
                BatchNoFill(ddlMedi11.SelectedValue, ddlMfg11.SelectedValue, txtBatch11);
                Expirydt(ddlMedi11.SelectedValue, ddlMfg11.SelectedValue, txtBatch11.SelectedValue, Calendar12, txtUnitPrice11, expdt11);

                DataTable dt = theHelper.AvailableStock(Session["CoCode"].ToString().Trim(), Session["YearCode"].ToString().Trim(), ddlMedi11.SelectedValue, ddlMfg11.SelectedValue, txtBatch11.SelectedValue);
                if (dt.Rows.Count > 0)
                {
                    txtStock11.Text = Convert.ToDecimal(dt.Rows[0]["AvailQty"]).ToString();
                }
            }
            PopulateSellingUnit(ddlMedi11.SelectedValue.Trim(), ddlSellingUnit11);
            txtPackSize11.Text = "1";
            txtQty11_TextChanged(sender, e);
        }
        
    }
    protected void ddlMedi12_SelectedIndexChanged(object sender, EventArgs e)
    {
        txtreg.Text = hdnregno.Value;
        if (ddlMedi12.SelectedValue == "0")
        {
            ResetRow(12);
        }
        else
        {
            MfgGrpSgrpFill(ddlMedi12.SelectedValue, 12);
            TaxDetails(ddlMedi2.SelectedValue, ddlMfg12.SelectedValue, txtHsnCode12, txtCgstRt12, txtSgstRt12, txtdiscAmt12, txtUnit12, lbliType12);
            if (lbliType12.Text == "C")
            {
                txtQty12.Text = "1";
                txtUnitPrice12.Text = "0.00";
                txtBatch12.Enabled = false;
                expdt12.Enabled = false;
                txtStock12.Enabled = false;

            }
            else
            {
                txtUnitPrice12.Text = theHelper.GetPurchasePricePerUnit(Session["CoCode"].ToString().Trim(), Session["YearCode"].ToString().Trim(), ddlMedi12.SelectedValue).ToString();
                BatchNoFill(ddlMedi12.SelectedValue, ddlMfg12.SelectedValue, txtBatch12);
                Expirydt(ddlMedi12.SelectedValue, ddlMfg12.SelectedValue, txtBatch12.SelectedValue, Calendar13, txtUnitPrice12, expdt12);

                DataTable dt = theHelper.AvailableStock(Session["CoCode"].ToString().Trim(), Session["YearCode"].ToString().Trim(), ddlMedi12.SelectedValue, ddlMfg12.SelectedValue, txtBatch12.SelectedValue);
                if (dt.Rows.Count > 0)
                {
                    txtStock12.Text = Convert.ToDecimal(dt.Rows[0]["AvailQty"]).ToString();
                }
            }
            PopulateSellingUnit(ddlMedi12.SelectedValue.Trim(), ddlSellingUnit12);
            txtPackSize12.Text = "1";
            txtQty12_TextChanged(sender, e);
        }
        
    }



    protected void txtQty1_TextChanged(object sender, EventArgs e)
    {
        if (lbliType1.Text == "C")
        {
        }
        else
        {
            if (validation(ddlMedi1.SelectedValue, ddlMfg1.SelectedValue, txtBatch1.SelectedValue, txtQty1, txtPackSize1) == true)
            {
                if (txtQty1.Text != "")
                {
                    if (ddltype.SelectedValue == "S")
                    {
                        
                        double discamt = (Convert.ToDouble(txtUnitPrice1.Text) * (Convert.ToDouble(txtQty1.Text) / Convert.ToDouble(txtPackSize1.Text)) * discrt);
                        txtdiscAmt1.Text = discamt.ToString();
                    }
                    double totalPrice = (Convert.ToDouble(txtUnitPrice1.Text) * (Convert.ToDouble(txtQty1.Text) / Convert.ToDouble(txtPackSize1.Text)) - Convert.ToDouble(txtdiscAmt1.Text));
                    double taxableamt = totalPrice / (1 + (Convert.ToDouble(txtCgstRt1.Text) + Convert.ToDouble(txtSgstRt1.Text)) / 100);
                    double cgstamt = taxableamt * (Convert.ToDouble(txtCgstRt1.Text) / 100);
                    double sgstamt = taxableamt * (Convert.ToDouble(txtSgstRt1.Text) / 100);
                    txtTaxableAmt1.Text = Math.Round(taxableamt, 2).ToString();
                    txtCgstAmt1.Text = Math.Round(cgstamt, 2).ToString();
                    txtSgstAmt1.Text = Math.Round(sgstamt, 2).ToString();
                    txtTotalPrice1.Text = totalPrice.ToString();
                }
                else
                {
                    txtTaxableAmt1.Text = "0.00";
                    txtCgstAmt1.Text = "0.00";
                    txtSgstAmt1.Text = "0.00";
                    txtTotalPrice1.Text = "0.00";
                }
                txtQty1.BorderColor = System.Drawing.Color.White;
                calculateTotal();
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "msg", "alert('Not Enough Stock Balance!');", true);
                txtQty1.Text = "";
                txtQty1.BorderColor = System.Drawing.Color.Red;
            }
        }
    }
    protected void txtQty2_TextChanged(object sender, EventArgs e)
    {
        if (lbliType2.Text == "C")
        {
        }
        else
        {
            if (validation(ddlMedi2.SelectedValue, ddlMfg2.SelectedValue, txtBatch2.SelectedValue, txtQty2, txtPackSize2) == true)
            {
                if (txtQty2.Text != "")
                {
                    if (ddltype.SelectedValue == "S")
                    {
                        
                        double discamt = (Convert.ToDouble(txtUnitPrice2.Text) * (Convert.ToDouble(txtQty2.Text) / Convert.ToDouble(txtPackSize2.Text)) * discrt);
                        txtdiscAmt2.Text = discamt.ToString();
                    }
                    double totalPrice = (Convert.ToDouble(txtUnitPrice2.Text) * (Convert.ToDouble(txtQty2.Text) / Convert.ToDouble(txtPackSize2.Text)) - Convert.ToDouble(txtdiscAmt2.Text));
                    double taxableamt = totalPrice / (1 + (Convert.ToDouble(txtCgstRt2.Text) + Convert.ToDouble(txtSgstRt2.Text)) / 100);
                    double cgstamt = taxableamt * (Convert.ToDouble(txtCgstRt2.Text) / 100);
                    double sgstamt = taxableamt * (Convert.ToDouble(txtSgstRt2.Text) / 100);
                    txtTaxableAmt2.Text = Math.Round(taxableamt, 2).ToString();
                    txtCgstAmt2.Text = Math.Round(cgstamt, 2).ToString();
                    txtSgstAmt2.Text = Math.Round(sgstamt, 2).ToString();
                    txtTotalPrice2.Text = totalPrice.ToString();
                }
                else
                {
                    txtTotalPrice2.Text = "0.00";//txtCgstRt2.Text = "0.00";
                    txtCgstAmt2.Text = "0.00";
                    txtSgstAmt2.Text = "0.00";
                    txtTotalPrice2.Text = "0.00";
                }
                txtQty2.BorderColor = System.Drawing.Color.White;
                calculateTotal();
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "msg", "alert('Not Enough Stock Balance!');", true);
                txtQty2.Text = "";
                txtQty2.BorderColor = System.Drawing.Color.Red;
            }
        }
    }
    protected void txtQty3_TextChanged(object sender, EventArgs e)
    {
        if (lbliType3.Text == "C")
        {
        }
        else
        {
            if (validation(ddlMedi3.SelectedValue, ddlMfg3.SelectedValue, txtBatch3.SelectedValue, txtQty3, txtPackSize3) == true)
            {
                if (txtQty3.Text != "")
                {
                    if (ddltype.SelectedValue == "S")
                    {
                        
                        double discamt = (Convert.ToDouble(txtUnitPrice3.Text) * (Convert.ToDouble(txtQty3.Text) / Convert.ToDouble(txtPackSize3.Text)) * discrt);
                        txtdiscAmt3.Text = discamt.ToString();
                    }
                    double totalPrice = (Convert.ToDouble(txtUnitPrice3.Text) * (Convert.ToDouble(txtQty3.Text) / Convert.ToDouble(txtPackSize3.Text)) - Convert.ToDouble(txtdiscAmt3.Text));
                    double taxableamt = totalPrice / (1 + (Convert.ToDouble(txtCgstRt3.Text) + Convert.ToDouble(txtSgstRt3.Text)) / 100);
                    double cgstamt = taxableamt * (Convert.ToDouble(txtCgstRt3.Text) / 100);
                    double sgstamt = taxableamt * (Convert.ToDouble(txtSgstRt3.Text) / 100);
                    txtTaxableAmt3.Text = Math.Round(taxableamt, 2).ToString();
                    txtCgstAmt3.Text = Math.Round(cgstamt, 2).ToString();
                    txtSgstAmt3.Text = Math.Round(sgstamt, 2).ToString();
                    txtTotalPrice3.Text = totalPrice.ToString();
                }
                else
                {
                    txtTotalPrice3.Text = "0.00";
                    txtCgstAmt3.Text = "0.00";
                    txtSgstAmt3.Text = "0.00";
                    txtTotalPrice3.Text = "0.00";
                }
                calculateTotal();
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "msg", "alert('Not Enough Stock Balance!');", true);
                txtQty3.Text = "";
                txtQty3.BorderColor = System.Drawing.Color.Red;
            }
        }
    }
    protected void txtQty4_TextChanged(object sender, EventArgs e)
    {
        if (lbliType4.Text == "C")
        {
        }
        else
        {
            if (validation(ddlMedi4.SelectedValue, ddlMfg4.SelectedValue, txtBatch4.SelectedValue, txtQty4, txtPackSize4) == true)
            {
                if (txtQty4.Text != "")
                {
                    if (ddltype.SelectedValue == "S")
                    {
                        
                        double discamt = (Convert.ToDouble(txtUnitPrice4.Text) * (Convert.ToDouble(txtQty4.Text) / Convert.ToDouble(txtPackSize4.Text)) * discrt);
                        txtdiscAmt4.Text = discamt.ToString();
                    }
                    double totalPrice = (Convert.ToDouble(txtUnitPrice4.Text) * (Convert.ToDouble(txtQty4.Text) / Convert.ToDouble(txtPackSize4.Text)) - Convert.ToDouble(txtdiscAmt4.Text));
                    double taxableamt = totalPrice / (1 + (Convert.ToDouble(txtCgstRt4.Text) + Convert.ToDouble(txtSgstRt4.Text)) / 100);
                    double cgstamt = taxableamt * (Convert.ToDouble(txtCgstRt4.Text) / 100);
                    double sgstamt = taxableamt * (Convert.ToDouble(txtSgstRt4.Text) / 100);
                    txtTaxableAmt4.Text = Math.Round(taxableamt, 2).ToString();
                    txtCgstAmt4.Text = Math.Round(cgstamt, 2).ToString();
                    txtSgstAmt4.Text = Math.Round(sgstamt, 2).ToString();
                    txtTotalPrice4.Text = totalPrice.ToString();
                }
                else
                {
                    txtTotalPrice4.Text = "0.00";
                    txtCgstAmt4.Text = "0.00";
                    txtSgstAmt4.Text = "0.00";
                    txtTotalPrice4.Text = "0.00";
                }
                calculateTotal();
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "msg", "alert('Not Enough Stock Balance!');", true);
                txtQty4.Text = "";
                txtQty4.BorderColor = System.Drawing.Color.Red;
            }
        }
    }
    protected void txtQty5_TextChanged(object sender, EventArgs e)
    {
        if (lbliType5.Text == "C")
        {
        }
        else
        {
            if (validation(ddlMedi5.SelectedValue, ddlMfg5.SelectedValue, txtBatch5.SelectedValue, txtQty5, txtPackSize5) == true)
            {
                if (txtQty5.Text != "")
                {
                    if (ddltype.SelectedValue == "S")
                    {
                        
                        double discamt = (Convert.ToDouble(txtUnitPrice5.Text) * (Convert.ToDouble(txtQty5.Text) / Convert.ToDouble(txtPackSize5.Text)) * discrt);
                        txtdiscAmt5.Text = discamt.ToString();
                    }

                    double totalPrice = (Convert.ToDouble(txtUnitPrice5.Text) * (Convert.ToDouble(txtQty5.Text) / Convert.ToDouble(txtPackSize5.Text)) - Convert.ToDouble(txtdiscAmt5.Text));
                    double taxableamt = totalPrice / (1 + (Convert.ToDouble(txtCgstRt5.Text) + Convert.ToDouble(txtSgstRt5.Text)) / 100);
                    double cgstamt = taxableamt * (Convert.ToDouble(txtCgstRt5.Text) / 100);
                    double sgstamt = taxableamt * (Convert.ToDouble(txtSgstRt5.Text) / 100);
                    txtTaxableAmt5.Text = Math.Round(taxableamt, 2).ToString();
                    txtCgstAmt5.Text = Math.Round(cgstamt, 2).ToString();
                    txtSgstAmt5.Text = Math.Round(sgstamt, 2).ToString();
                    txtTotalPrice5.Text = totalPrice.ToString();
                }
                else
                {
                    txtTotalPrice5.Text = "0.00";
                    txtCgstAmt5.Text = "0.00";
                    txtSgstAmt5.Text = "0.00";
                    txtTotalPrice5.Text = "0.00";
                }
                calculateTotal();
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "msg", "alert('Not Enough Stock Balance!');", true);
                txtQty5.Text = "";
                txtQty5.BorderColor = System.Drawing.Color.Red;
            }
        }
    }
    protected void txtQty6_TextChanged(object sender, EventArgs e)
    {
        if (lbliType6.Text == "C")
        {
        }
        else
        {
            if (validation(ddlMedi6.SelectedValue, ddlMfg6.SelectedValue, txtBatch6.SelectedValue, txtQty6, txtPackSize6) == true)
            {
                if (txtQty6.Text != "")
                {
                    if (ddltype.SelectedValue == "S")
                    {
                        
                        double discamt = (Convert.ToDouble(txtUnitPrice6.Text) * (Convert.ToDouble(txtQty6.Text) / Convert.ToDouble(txtPackSize6.Text)) * discrt);
                        txtdiscAmt6.Text = discamt.ToString();
                    }
                    double totalPrice = (Convert.ToDouble(txtUnitPrice6.Text) * (Convert.ToDouble(txtQty6.Text) / Convert.ToDouble(txtPackSize6.Text)) - Convert.ToDouble(txtdiscAmt6.Text));
                    double taxableamt = totalPrice / (1 + (Convert.ToDouble(txtCgstRt6.Text) + Convert.ToDouble(txtSgstRt6.Text)) / 100);
                    double cgstamt = taxableamt * (Convert.ToDouble(txtCgstRt6.Text) / 100);
                    double sgstamt = taxableamt * (Convert.ToDouble(txtSgstRt6.Text) / 100);
                    txtTaxableAmt6.Text = Math.Round(taxableamt, 2).ToString();
                    txtCgstAmt6.Text = Math.Round(cgstamt, 2).ToString();
                    txtSgstAmt6.Text = Math.Round(sgstamt, 2).ToString();
                    txtTotalPrice6.Text = totalPrice.ToString();
                }
                else
                {
                    txtTotalPrice6.Text = "0.00";
                    txtCgstAmt6.Text = "0.00";
                    txtSgstAmt6.Text = "0.00";
                    txtTotalPrice6.Text = "0.00";
                }
                calculateTotal();
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "msg", "alert('Not Enough Stock Balance!');", true);
                txtQty6.Text = "";
                txtQty6.BorderColor = System.Drawing.Color.Red;
            }
        }
    }
    protected void txtQty7_TextChanged(object sender, EventArgs e)
    {
        if (lbliType7.Text == "C")
        {
        }
        else
        {
            if (validation(ddlMedi7.SelectedValue, ddlMfg7.SelectedValue, txtBatch7.SelectedValue, txtQty7, txtPackSize7) == true)
            {
                if (txtQty7.Text != "")
                {
                    if (ddltype.SelectedValue == "S")
                    {
                        
                        double discamt = (Convert.ToDouble(txtUnitPrice7.Text) * (Convert.ToDouble(txtQty7.Text) / Convert.ToDouble(txtPackSize7.Text)) * discrt);
                        txtdiscAmt7.Text = discamt.ToString();
                    }
                    double totalPrice = (Convert.ToDouble(txtUnitPrice7.Text) * (Convert.ToDouble(txtQty7.Text) / Convert.ToDouble(txtPackSize7.Text)) - Convert.ToDouble(txtdiscAmt7.Text));
                    double taxableamt = totalPrice / (1 + (Convert.ToDouble(txtCgstRt7.Text) + Convert.ToDouble(txtSgstRt7.Text)) / 100);
                    double cgstamt = taxableamt * (Convert.ToDouble(txtCgstRt7.Text) / 100);
                    double sgstamt = taxableamt * (Convert.ToDouble(txtSgstRt7.Text) / 100);
                    txtTaxableAmt7.Text = Math.Round(taxableamt, 2).ToString();
                    txtCgstAmt7.Text = Math.Round(cgstamt, 2).ToString();
                    txtSgstAmt7.Text = Math.Round(sgstamt, 2).ToString();
                    txtTotalPrice7.Text = totalPrice.ToString();
                }
                else
                {
                    txtTotalPrice7.Text = "0.00";
                    txtCgstAmt7.Text = "0.00";
                    txtSgstAmt7.Text = "0.00";
                    txtTotalPrice7.Text = "0.00";
                }
                calculateTotal();
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "msg", "alert('Not Enough Stock Balance!');", true);
                txtQty7.Text = "";
                txtQty7.BorderColor = System.Drawing.Color.Red;
            }
        }
    }
    protected void txtQty8_TextChanged(object sender, EventArgs e)
    {
        if (lbliType8.Text == "C")
        {
        }
        else
        {
            if (validation(ddlMedi8.SelectedValue, ddlMfg8.SelectedValue, txtBatch8.SelectedValue, txtQty8, txtPackSize8) == true)
            {
                if (txtQty8.Text != "")
                {
                    if (ddltype.SelectedValue == "S")
                    {
                        
                        double discamt = (Convert.ToDouble(txtUnitPrice8.Text) * (Convert.ToDouble(txtQty8.Text) / Convert.ToDouble(txtPackSize8.Text)) * discrt);
                        txtdiscAmt8.Text = discamt.ToString();
                    }
                    double totalPrice = (Convert.ToDouble(txtUnitPrice8.Text) * (Convert.ToDouble(txtQty8.Text) / Convert.ToDouble(txtPackSize8.Text)) - Convert.ToDouble(txtdiscAmt8.Text));
                    double taxableamt = totalPrice / (1 + (Convert.ToDouble(txtCgstRt8.Text) + Convert.ToDouble(txtSgstRt8.Text)) / 100);
                    double cgstamt = taxableamt * (Convert.ToDouble(txtCgstRt8.Text) / 100);
                    double sgstamt = taxableamt * (Convert.ToDouble(txtSgstRt8.Text) / 100);
                    txtTaxableAmt8.Text = Math.Round(taxableamt, 2).ToString();
                    txtCgstAmt8.Text = Math.Round(cgstamt, 2).ToString();
                    txtSgstAmt8.Text = Math.Round(sgstamt, 2).ToString();
                    txtTotalPrice8.Text = totalPrice.ToString();
                }
                else
                {
                    txtTotalPrice8.Text = "0.00";
                    txtCgstAmt8.Text = "0.00";
                    txtSgstAmt8.Text = "0.00";
                    txtTotalPrice8.Text = "0.00";
                }
                calculateTotal();
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "msg", "alert('Not Enough Stock Balance!');", true);
                txtQty8.Text = "";
                txtQty8.BorderColor = System.Drawing.Color.Red;
            }
        }
    }
    protected void txtQty9_TextChanged(object sender, EventArgs e)
    {
        if (lbliType9.Text == "C")
        {
        }
        else
        {
            if (validation(ddlMedi9.SelectedValue, ddlMfg9.SelectedValue, txtBatch9.SelectedValue, txtQty9, txtPackSize9) == true)
            {
                if (txtQty9.Text != "")
                {
                    if (ddltype.SelectedValue == "S")
                    {
                        
                        double discamt = (Convert.ToDouble(txtUnitPrice9.Text) * (Convert.ToDouble(txtQty9.Text) / Convert.ToDouble(txtPackSize9.Text)) * discrt);
                        txtdiscAmt9.Text = discamt.ToString();
                    }
                    double totalPrice = (Convert.ToDouble(txtUnitPrice9.Text) * (Convert.ToDouble(txtQty9.Text) / Convert.ToDouble(txtPackSize9.Text)) - Convert.ToDouble(txtdiscAmt9.Text));
                    double taxableamt = totalPrice / (1 + (Convert.ToDouble(txtCgstRt9.Text) + Convert.ToDouble(txtSgstRt9.Text)) / 100);
                    double cgstamt = taxableamt * (Convert.ToDouble(txtCgstRt9.Text) / 100);
                    double sgstamt = taxableamt * (Convert.ToDouble(txtSgstRt9.Text) / 100);
                    txtTaxableAmt9.Text = Math.Round(taxableamt, 2).ToString();
                    txtCgstAmt9.Text = Math.Round(cgstamt, 2).ToString();
                    txtSgstAmt9.Text = Math.Round(sgstamt, 2).ToString();
                    txtTotalPrice9.Text = totalPrice.ToString();
                }
                else
                {
                    txtTotalPrice9.Text = "0.00";
                    txtCgstAmt9.Text = "0.00";
                    txtSgstAmt9.Text = "0.00";
                    txtTotalPrice9.Text = "0.00";
                }
                calculateTotal();
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "msg", "alert('Not Enough Stock Balance!');", true);
                txtQty9.Text = "";
                txtQty9.BorderColor = System.Drawing.Color.Red;
            }
        }
    }
    protected void txtQty10_TextChanged(object sender, EventArgs e)
    {
        if (lbliType10.Text == "C")
        {
        }
        else
        {
            if (validation(ddlMedi10.SelectedValue, ddlMfg10.SelectedValue, txtBatch10.SelectedValue, txtQty10, txtPackSize10) == true)
            {
                if (txtQty10.Text != "")
                {
                    if (ddltype.SelectedValue == "S")
                    {
                        
                        double discamt = (Convert.ToDouble(txtUnitPrice10.Text) * (Convert.ToDouble(txtQty10.Text) / Convert.ToDouble(txtPackSize10.Text)) * discrt);
                        txtdiscAmt10.Text = discamt.ToString();
                    }
                    double totalPrice = (Convert.ToDouble(txtUnitPrice10.Text) * (Convert.ToDouble(txtQty10.Text) / Convert.ToDouble(txtPackSize10.Text)) - Convert.ToDouble(txtdiscAmt10.Text));
                    double taxableamt = totalPrice / (1 + (Convert.ToDouble(txtCgstRt10.Text) + Convert.ToDouble(txtSgstRt10.Text)) / 100);
                    double cgstamt = taxableamt * (Convert.ToDouble(txtCgstRt10.Text) / 100);
                    double sgstamt = taxableamt * (Convert.ToDouble(txtSgstRt10.Text) / 100);
                    txtTaxableAmt10.Text = Math.Round(taxableamt, 2).ToString();
                    txtCgstAmt10.Text = Math.Round(cgstamt, 2).ToString();
                    txtSgstAmt10.Text = Math.Round(sgstamt, 2).ToString();
                    txtTotalPrice10.Text = totalPrice.ToString();
                }
                else
                {
                    txtTotalPrice10.Text = "0.00";
                    txtCgstAmt10.Text = "0.00";
                    txtSgstAmt10.Text = "0.00";
                    txtTotalPrice10.Text = "0.00";
                }
                calculateTotal();
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "msg", "alert('Not Enough Stock Balance!');", true);
                txtQty10.Text = "";
                txtQty10.BorderColor = System.Drawing.Color.Red;
            }
        }
    }
    protected void txtQty11_TextChanged(object sender, EventArgs e)
    {
        if (lbliType11.Text == "C")
        {
        }
        else
        {
            if (validation(ddlMedi11.SelectedValue, ddlMfg11.SelectedValue, txtBatch11.SelectedValue, txtQty11, txtPackSize11) == true)
            {
                if (txtQty11.Text != "")
                {
                    if (ddltype.SelectedValue == "S")
                    {
                        
                        double discamt = (Convert.ToDouble(txtUnitPrice11.Text) * (Convert.ToDouble(txtQty11.Text) / Convert.ToDouble(txtPackSize11.Text)) * discrt);
                        txtdiscAmt11.Text = discamt.ToString();
                    }
                    double totalPrice = (Convert.ToDouble(txtUnitPrice11.Text) * (Convert.ToDouble(txtQty11.Text) / Convert.ToDouble(txtPackSize11.Text)) - Convert.ToDouble(txtdiscAmt11.Text));
                    double taxableamt = totalPrice / (1 + (Convert.ToDouble(txtCgstRt11.Text) + Convert.ToDouble(txtSgstRt11.Text)) / 100);
                    double cgstamt = taxableamt * (Convert.ToDouble(txtCgstRt11.Text) / 100);
                    double sgstamt = taxableamt * (Convert.ToDouble(txtSgstRt11.Text) / 100);
                    txtTaxableAmt11.Text = Math.Round(taxableamt, 2).ToString();
                    txtCgstAmt11.Text = Math.Round(cgstamt, 2).ToString();
                    txtSgstAmt11.Text = Math.Round(sgstamt, 2).ToString();
                    txtTotalPrice11.Text = totalPrice.ToString();
                }
                else
                {
                    txtTotalPrice11.Text = "0.00";
                    txtCgstAmt11.Text = "0.00";
                    txtSgstAmt11.Text = "0.00";
                    txtTotalPrice11.Text = "0.00";
                }
                calculateTotal();
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "msg", "alert('Not Enough Stock Balance!');", true);
                txtQty11.Text = "";
                txtQty11.BorderColor = System.Drawing.Color.Red;
            }
        }
    }
    protected void txtQty12_TextChanged(object sender, EventArgs e)
    {
        if (lbliType12.Text == "C")
        {
        }
        else
        {
            if (validation(ddlMedi12.SelectedValue, ddlMfg12.SelectedValue, txtBatch12.SelectedValue, txtQty12, txtPackSize12) == true)
            {
                if (txtQty12.Text != "")
                {
                    if (ddltype.SelectedValue == "S")
                    {
                        
                        double discamt = (Convert.ToDouble(txtUnitPrice12.Text) * (Convert.ToDouble(txtQty12.Text) / Convert.ToDouble(txtPackSize12.Text)) * discrt);
                        txtdiscAmt12.Text = discamt.ToString();
                    }
                    double totalPrice = (Convert.ToDouble(txtUnitPrice12.Text) * (Convert.ToDouble(txtQty12.Text) / Convert.ToDouble(txtPackSize12.Text)) - Convert.ToDouble(txtdiscAmt12.Text));
                    double taxableamt = totalPrice / (1 + (Convert.ToDouble(txtCgstRt12.Text) + Convert.ToDouble(txtSgstRt12.Text)) / 100);
                    double cgstamt = taxableamt * (Convert.ToDouble(txtCgstRt12.Text) / 100);
                    double sgstamt = taxableamt * (Convert.ToDouble(txtSgstRt12.Text) / 100);
                    txtTaxableAmt12.Text = Math.Round(taxableamt, 2).ToString();
                    txtCgstAmt12.Text = Math.Round(cgstamt, 2).ToString();
                    txtSgstAmt12.Text = Math.Round(sgstamt, 2).ToString();
                    txtTotalPrice12.Text = totalPrice.ToString();
                }
                else
                {
                    txtTotalPrice12.Text = "0.00";
                    txtCgstAmt12.Text = "0.00";
                    txtSgstAmt12.Text = "0.00";
                    txtTotalPrice12.Text = "0.00";
                }

                calculateTotal();
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "msg", "alert('Not Enough Stock Balance!');", true);
                txtQty12.Text = "";
                txtQty12.BorderColor = System.Drawing.Color.Red;
            }
        }
    }


    public void calculateTotal()
    {
        string regno = Request.Form[txtreg.UniqueID];
        txtreg.Text = regno;

        TextBox t1, t2, t3;
        double total = 0.0, totgval = 0.0, roundOffAmt = 0.0, netAmt = 0.0,grsamt=0.0,grossval=0.00;
        for (int i = 1; i <= 12; i++)
        {
            t1 = (TextBox)Page.FindControl("ctl00$ContentPlaceHolder1$txtTotalPrice" + i.ToString());
            t2 = (TextBox)Page.FindControl("ctl00$ContentPlaceHolder1$txtUnitPrice" + i.ToString());
            t3 = (TextBox)Page.FindControl("ctl00$ContentPlaceHolder1$txtQty" + i.ToString());
            if (t3.Text != "" && Convert.ToDouble(t3.Text)>0)
            {
                //totgval = totgval + (Convert.ToDouble(t2.Text) * Convert.ToDouble(t3.Text));
                totgval = totgval + (Convert.ToDouble(t1.Text));
                total = total + Convert.ToDouble(t1.Text);
            }
        }
        if (txtPushChrg.Text == "")
        {
            txtPushChrg.Text = "0";
        }

        if (txtChrgAmt1.Text == "")
        {
            txtChrgAmt1.Text = "0";
        }

        if (txtChrgAmt2.Text == "")
        {
            txtChrgAmt2.Text = "0";
        }

        grsamt = total + Convert.ToDouble(txtPushChrg.Text) + Convert.ToDouble(txtChrgAmt1.Text) + Convert.ToDouble(txtChrgAmt2.Text);
        double frac = grsamt - Math.Truncate(grsamt);
        double val = Math.Truncate(grsamt);
        if (frac == 0.50)
        {
            netAmt = Math.Round(grsamt,0,MidpointRounding.AwayFromZero);
            
        }
        else
        {
            netAmt = Math.Round(grsamt);
            
        }


        roundOffAmt = netAmt - grsamt;
        txtgross.Text = total.ToString("F2");
        txtNetAmt.Text = netAmt.ToString("F2");
        txtRoundOff.Text = roundOffAmt.ToString("F2");
    }
    private void PageDataBind()
    {
        try
        {
            DropDownFill();
            TextBox t2, t3, t4, t5,t6,t7,t8,t9,t10,t11,t12,t13,t14,t15,t16,t17; 
            DropDownList t1, d1, d2, d3, d4,d5; HiddenField h1;
            Label l1;
            DataTable dtPurMedicineHead = theHelper.GetSaleMedicineHead(Session["CoCode"].ToString().Trim(), Session["YearCode"].ToString().Trim(), TextBox5.Text);
            DataTable dtPurchaseMedicine = theHelper.GetSaleMedicineDetails(Session["CoCode"].ToString().Trim(), Session["YearCode"].ToString().Trim(), TextBox5.Text);
            if (dtPurMedicineHead.Rows.Count > 0)
            {
                ddltype.SelectedValue = dtPurMedicineHead.Rows[0]["CFILLER09"].ToString().Trim();
                txtBillNo.Text = dtPurMedicineHead.Rows[0]["REFBILLNO"].ToString();
                TextBox6.Text = dtPurMedicineHead.Rows[0]["DOCDATE"].ToString();
                txtPatientName.Text = dtPurMedicineHead.Rows[0]["PatientName"].ToString().Trim();
                txtreg.Text = dtPurMedicineHead.Rows[0]["RegNo"].ToString().Trim();
                hdnregno.Value = dtPurMedicineHead.Rows[0]["RegNo"].ToString().Trim();
                hdnLedgerId.Value = dtPurMedicineHead.Rows[0]["slcode"].ToString().Trim();
                   
                if (ddltype.SelectedValue == "S")
                {
                    ddlstaff.SelectedValue = hdnLedgerId.Value;
                    divs.Visible = true;
                    divp.Visible = false;
                }
                else
                {
                    divs.Visible = false;
                    divp.Visible = true;
                }
                receivedt.Text = dtPurMedicineHead.Rows[0]["ReceiveDate"].ToString();
                TextBox3.Text = dtPurMedicineHead.Rows[0]["DocName"].ToString();

                receiveby.Text = dtPurMedicineHead.Rows[0]["ReceiveBy"].ToString();
                issueby.Text = dtPurMedicineHead.Rows[0]["Issueby"].ToString();
                txtPushChrg.Text = dtPurMedicineHead.Rows[0]["PushingChrg"].ToString();
                txtDesc1.Text = dtPurMedicineHead.Rows[0]["ExtraChargeDesc1"].ToString();
                txtChrgAmt1.Text = dtPurMedicineHead.Rows[0]["ExtraChargeAmt1"].ToString();
                txtDesc2.Text = dtPurMedicineHead.Rows[0]["ExtraChargeDesc2"].ToString();
                txtChrgAmt2.Text = dtPurMedicineHead.Rows[0]["ExtraChargeAmt2"].ToString();
                txtBillNo.Text = dtPurMedicineHead.Rows[0]["REFBILLNO"].ToString();
                //ddlPaymentMode.SelectedValue = dtPurMedicineHead.Rows[0]["PAYMODE"].ToString().Trim();
                txtgross.Text = dtPurMedicineHead.Rows[0]["GVALUE"].ToString().Trim();
                txtNetAmt.Text = dtPurMedicineHead.Rows[0]["BILLVALUE"].ToString().Trim();
                txtRoundOff.Text = dtPurMedicineHead.Rows[0]["ROFFAMT"].ToString().Trim();
                txtCash.Text = dtPurMedicineHead.Rows[0]["cash_amt"].ToString().Trim();
                txtCard.Text = dtPurMedicineHead.Rows[0]["card_amt"].ToString().Trim();
                txtewallet.Text = dtPurMedicineHead.Rows[0]["ewallet_amt"].ToString().Trim();
                txtNetBank.Text = dtPurMedicineHead.Rows[0]["netbank_amt"].ToString().Trim();
                ddltype.SelectedValue = dtPurMedicineHead.Rows[0]["CFILLER09"].ToString().Trim();
                if (dtPurMedicineHead.Rows[0]["CancelFlag"].ToString().Trim() == "1")
                {
                    chkCancel.Checked = true;
                }
                else
                {
                    chkCancel.Checked = false;
                }

                for (int i = 0, t = 1; i < dtPurchaseMedicine.Rows.Count; i++, t++)
                {
                    l1 = (Label)Page.FindControl("ctl00$ContentPlaceHolder1$lbliType" + t.ToString());
                    t1 = (DropDownList)Page.FindControl("ctl00$ContentPlaceHolder1$txtBatch" + t.ToString());
                    t2 = (TextBox)Page.FindControl("ctl00$ContentPlaceHolder1$Calendar" + (t + 1).ToString());
                    t6 = (TextBox)Page.FindControl("ctl00$ContentPlaceHolder1$expdt" + t.ToString());
                    t3 = (TextBox)Page.FindControl("ctl00$ContentPlaceHolder1$txtQty" + t.ToString());
                    h1 = (HiddenField)Page.FindControl("ctl00$ContentPlaceHolder1$qty" + t.ToString());
                    t4 = (TextBox)Page.FindControl("ctl00$ContentPlaceHolder1$txtUnitPrice" + t.ToString());
                    t5 = (TextBox)Page.FindControl("ctl00$ContentPlaceHolder1$txtUnit" + t.ToString());
                    t7 = (TextBox)Page.FindControl("ctl00$ContentPlaceHolder1$txtPackSize" + t.ToString());
                    t8 = (TextBox)Page.FindControl("ctl00$ContentPlaceHolder1$txtdiscAmt" + t.ToString());
                    t9 = (TextBox)Page.FindControl("ctl00$ContentPlaceHolder1$txtTaxableAmt" + t.ToString());
                    t10 = (TextBox)Page.FindControl("ctl00$ContentPlaceHolder1$txtHsnCode" + t.ToString());
                    t11 = (TextBox)Page.FindControl("ctl00$ContentPlaceHolder1$txtCgstRt" + t.ToString());
                    t12 = (TextBox)Page.FindControl("ctl00$ContentPlaceHolder1$txtCgstAmt" + t.ToString());
                    t13 = (TextBox)Page.FindControl("ctl00$ContentPlaceHolder1$txtSgstRt" + t.ToString());
                    t14 = (TextBox)Page.FindControl("ctl00$ContentPlaceHolder1$txtSgstAmt" + t.ToString());
                    t15 = (TextBox)Page.FindControl("ctl00$ContentPlaceHolder1$txtTotalPrice" + t.ToString());
                    t16 = (TextBox)Page.FindControl("ctl00$ContentPlaceHolder1$txtStock" + t.ToString());
                    


                    d1 = (DropDownList)Page.FindControl("ctl00$ContentPlaceHolder1$ddlMfg" + t.ToString());
                    d2 = (DropDownList)Page.FindControl("ctl00$ContentPlaceHolder1$ddlMediGrp" + t.ToString());
                    d3 = (DropDownList)Page.FindControl("ctl00$ContentPlaceHolder1$ddlMedi" + t.ToString());
                    d4 = (DropDownList)Page.FindControl("ctl00$ContentPlaceHolder1$ddlMediSubGrp" + t.ToString());
                    d5 = (DropDownList)Page.FindControl("ctl00$ContentPlaceHolder1$ddlSellingUnit" + t.ToString());
                    //d1.SelectedValue = dtPurchaseMedicine.Rows[i]["MCode"].ToString();
                    //d2.SelectedValue = dtPurchaseMedicine.Rows[i]["MedicineGroupID"].ToString();
                    //DdlMedicineSubGroupBind("0", d4);
                    //d4.SelectedValue = dtPurchaseMedicine.Rows[i]["MedicineSubGrp"].ToString();
                    // DdlMedicineIDBind(d2.SelectedValue,d4.SelectedValue, d3, d1);
                    DdlIssueMedicineIDBind(d3);
                    //d3.SelectedValue = dtPurchaseMedicine.Rows[i]["MedicineID"].ToString();
                    d3.SelectedValue = dtPurchaseMedicine.Rows[i]["icode"].ToString().Trim();


                    // BatchNoFill(d3.SelectedValue, d1.SelectedValue,t1);
                    
                    
                    l1.Text = dtPurchaseMedicine.Rows[i]["itype"].ToString();
                    if (dtPurchaseMedicine.Rows[i]["itype"].ToString() == "C")
                    {
                        t16.Text = "";
                        t16.Enabled = false;
                        t2.Text = "";
                        t2.Enabled = false;
                        t6.Text = "";
                        t6.Enabled=false;
                        t1.Enabled = false;
                        //txtBatch12.Enabled = false;
                        //expdt12.Enabled = false;
                        //txtStock12.Enabled = false;
                    }
                    else
                    {
                        DdlBatchBind("0", dtPurchaseMedicine.Rows[i]["icode"].ToString(), TextBox5.Text, t1);
                        t1.SelectedValue = dtPurchaseMedicine.Rows[i]["BatchNo"].ToString();
                        DataTable dt = theHelper.AvailableStock(Session["CoCode"].ToString().Trim(), Session["YearCode"].ToString().Trim(), dtPurchaseMedicine.Rows[i]["icode"].ToString().Trim(), "", dtPurchaseMedicine.Rows[i]["BatchNo"].ToString());
                        t16.Text = dt.Rows[0]["AvailQty"].ToString();
                        t2.Text = dtPurchaseMedicine.Rows[i]["Expdt"].ToString();
                        t6.Text = dtPurchaseMedicine.Rows[i]["ExpYearMonth"].ToString();
                    }
                    t3.Text = (Convert.ToDecimal(dtPurchaseMedicine.Rows[i]["IQTY"]) * Convert.ToDecimal(dtPurchaseMedicine.Rows[i]["PACKQTY"])).ToString();
                    h1.Value = (Convert.ToDecimal(dtPurchaseMedicine.Rows[i]["IQTY"]) * Convert.ToDecimal(dtPurchaseMedicine.Rows[i]["PACKQTY"])).ToString();
                    t4.Text = dtPurchaseMedicine.Rows[i]["IRATE"].ToString();
                    t5.Text = dtPurchaseMedicine.Rows[i]["Unit1Name"].ToString();
                    t7.Text = dtPurchaseMedicine.Rows[i]["PACKQTY"].ToString();
                    t8.Text = dtPurchaseMedicine.Rows[i]["DISCAMT"].ToString();
                    t9.Text = dtPurchaseMedicine.Rows[i]["SELLAMOUNT"].ToString();
                    t10.Text = dtPurchaseMedicine.Rows[i]["HSNCODE"].ToString();
                    t11.Text = dtPurchaseMedicine.Rows[i]["CGST_RATE"].ToString();
                    t12.Text = dtPurchaseMedicine.Rows[i]["CGST_AMT"].ToString();
                    t13.Text = dtPurchaseMedicine.Rows[i]["SGST_RATE"].ToString();
                    t14.Text = dtPurchaseMedicine.Rows[i]["SGST_AMT"].ToString();
                    t15.Text = dtPurchaseMedicine.Rows[i]["IAMOUNTWITHVAT"].ToString();

                    if (dtPurchaseMedicine.Rows[i]["icode"].ToString() != "")
                    {
                        DdlIssueIDMfgFill(dtPurchaseMedicine.Rows[i]["icode"].ToString(), d1);
                        //d1.SelectedValue = dtPurchaseMedicine.Rows[i]["MCode"].ToString();
                    }
                    else
                    {
                        d1.Items.Clear();
                        d1.Items.Insert(0, new ListItem("--Select--", "0"));
                    }

                    if (dtPurchaseMedicine.Rows[i]["icode"].ToString() != "")
                    {
                        IssueMedicineGroupFill(dtPurchaseMedicine.Rows[i]["icode"].ToString(), d2);
                        //d2.SelectedValue = dtPurchaseMedicine.Rows[i]["MedicineGroupID"].ToString();
                    }
                    else
                    {
                        d2.Items.Clear();
                        d2.Items.Insert(0, new ListItem("--Select--", "0"));
                    }

                    if (dtPurchaseMedicine.Rows[i]["icode"].ToString() != "")
                    {
                        DdlIssueMedicineSubGroupBind(dtPurchaseMedicine.Rows[i]["icode"].ToString(), d4);
                        //d4.SelectedValue = dtPurchaseMedicine.Rows[i]["MedicineSubGrp"].ToString();
                    }
                    else
                    {
                        d4.Items.Clear();
                        d4.Items.Insert(0, new ListItem("--Select--", "0"));
                    }
                    d5.Items.Clear();
                    d5.Items.Insert(0, new ListItem(dtPurchaseMedicine.Rows[i]["Unit1Name"].ToString(), dtPurchaseMedicine.Rows[i]["Unit1"].ToString()));
                    d5.Items.Insert(1, new ListItem(dtPurchaseMedicine.Rows[i]["Unit2Name"].ToString(), dtPurchaseMedicine.Rows[i]["Unit2"].ToString()));
                    d5.SelectedValue = dtPurchaseMedicine.Rows[i]["sellingUnit"].ToString().Trim();
                }
                Button1.Text = "Update";
                
                if (Objmain.checkaccss(Session["CoCode"].ToString(), Session["UserRoleID"].ToString().Trim(), "MEDICINE/REAGENT ISSUE", checkAccessType.UpdateAction) == false)
                {
                    Button1.Enabled = false;
                }
                else
                {
                    Button1.Enabled = true;
                }
            }
            else
            {

                //TextBox5.Text = theHelper.GetPurchaseMedicineID().ToString();
                TextBox5.Text = theHelper.GenerateSaleId(Session["CoCode"].ToString().Trim(), Session["YearCode"].ToString().Trim()).ToString();
                Button1.Text = "SUBMIT";
            }
        }
        catch (Exception ex)
        {
            Label1.ForeColor = System.Drawing.Color.Red;
            Label1.Text = ex.Message;
        }
    }
    public void ClearQtyBoxErr()
    {
        txtQty1.BorderColor = System.Drawing.Color.White;
    }

    /*private void DdlMedicineIDBind(string medicalGrp,string medicalsubgrp, DropDownList ddlMedicineID, DropDownList ddlMfg)
    {
        ddlMedicineID.Items.Clear();
        ddlMedicineID.DataSource = theHelper.DropdownID4(Session["CoCode"].ToString().Trim(), Session["YearCode"].ToString().Trim(), medicalGrp, medicalsubgrp, ddlMfg.SelectedValue);
        ddlMedicineID.DataTextField = "MedicineName";
        ddlMedicineID.DataValueField = "MedicineID";
        ddlMedicineID.DataBind();
        ddlMedicineID.Items.Insert(0, new ListItem("--Select--", "0"));
    }*/


    private void DdlMedicineSubGroupBind(string medicalGrp, DropDownList ddlMedicineSubID)
    {
        ddlMedicineSubID.Items.Clear();
        ddlMedicineSubID.DataSource = theHelper.DropdownSubGroup(Session["CoCode"].ToString().Trim(), medicalGrp);
        ddlMedicineSubID.DataTextField = "SubGrName";
        ddlMedicineSubID.DataValueField = "ID";
        ddlMedicineSubID.DataBind();
        ddlMedicineSubID.Items.Insert(0, new ListItem("--Select--", "0"));
    }


    protected void Button4_Click(object sender, EventArgs e)
    {
        
        HiddenField1.Value = TextBox5.Text;
        hdnInsUpd.Value = "U";
        DropDownFill();
        ResetAllFields();
        TextBox5.Text = HiddenField1.Value;
        PageDataBind();
    }

    protected void txtBatch1_SelectedIndexChanged(object sender, EventArgs e)
    {
        Expirydt(ddlMedi1.SelectedValue, ddlMfg1.SelectedValue, txtBatch1.SelectedValue, Calendar2, txtUnitPrice1, expdt1);
        DataTable dt = theHelper.AvailableStock(Session["CoCode"].ToString().Trim(), Session["YearCode"].ToString().Trim(), ddlMedi1.SelectedValue, ddlMfg1.SelectedValue, txtBatch1.SelectedValue);
        if (dt.Rows.Count > 0)
        {
            txtStock1.Text = Convert.ToDecimal(dt.Rows[0]["AvailQty"]).ToString();
            Calendar2.Text = dt.Rows[0]["EXPDATE"].ToString();
        }
        else
        {
            txtStock1.Text = "0.00";
            Calendar2.Text = "";
        }
        txtQty1_TextChanged(sender, e);
    }
    protected void txtBatch2_SelectedIndexChanged(object sender, EventArgs e)
    {
        Expirydt(ddlMedi2.SelectedValue, ddlMfg2.SelectedValue, txtBatch2.SelectedValue, Calendar3, txtUnitPrice2, expdt2);
        DataTable dt = theHelper.AvailableStock(Session["CoCode"].ToString().Trim(), Session["YearCode"].ToString().Trim(), ddlMedi2.SelectedValue, ddlMfg2.SelectedValue, txtBatch2.SelectedValue);
        if (dt.Rows.Count > 0)
        {
            txtStock2.Text = Convert.ToDecimal(dt.Rows[0]["AvailQty"]).ToString();
            Calendar3.Text = dt.Rows[0]["EXPDATE"].ToString();
        }
        else
        {
            txtStock2.Text = "0.00";
            Calendar3.Text = "";
        }
        txtQty2_TextChanged(sender, e);
    }
    protected void txtBatch3_SelectedIndexChanged(object sender, EventArgs e)
    {
        Expirydt(ddlMedi3.SelectedValue, ddlMfg3.SelectedValue, txtBatch3.SelectedValue, Calendar4, txtUnitPrice3, expdt3);
        DataTable dt = theHelper.AvailableStock(Session["CoCode"].ToString().Trim(), Session["YearCode"].ToString().Trim(), ddlMedi3.SelectedValue, ddlMfg3.SelectedValue, txtBatch3.SelectedValue);
        if (dt.Rows.Count > 0)
        {
            txtStock3.Text = Convert.ToDecimal(dt.Rows[0]["AvailQty"]).ToString();
            Calendar4.Text = dt.Rows[0]["EXPDATE"].ToString();
        }
        else
        {
            txtStock3.Text = "0.00";
            Calendar4.Text = "";
        }
        txtQty3_TextChanged(sender, e);
    }
    protected void txtBatch4_SelectedIndexChanged(object sender, EventArgs e)
    {
        Expirydt(ddlMedi4.SelectedValue, ddlMfg4.SelectedValue, txtBatch4.SelectedValue, Calendar5, txtUnitPrice4, expdt4);
        DataTable dt = theHelper.AvailableStock(Session["CoCode"].ToString().Trim(), Session["YearCode"].ToString().Trim(), ddlMedi4.SelectedValue, ddlMfg4.SelectedValue, txtBatch4.SelectedValue);
        if (dt.Rows.Count > 0)
        {
            txtStock4.Text = Convert.ToDecimal(dt.Rows[0]["AvailQty"]).ToString();
            Calendar5.Text = dt.Rows[0]["EXPDATE"].ToString();
        }
        else
        {
            txtStock4.Text = "0.00";
            Calendar5.Text = "";
        }
        txtQty4_TextChanged(sender, e);
    }
    protected void txtBatch5_SelectedIndexChanged(object sender, EventArgs e)
    {
        Expirydt(ddlMedi5.SelectedValue, ddlMfg5.SelectedValue, txtBatch5.SelectedValue, Calendar6, txtUnitPrice5, expdt5);
        DataTable dt = theHelper.AvailableStock(Session["CoCode"].ToString().Trim(), Session["YearCode"].ToString().Trim(), ddlMedi5.SelectedValue, ddlMfg5.SelectedValue, txtBatch5.SelectedValue);
        if (dt.Rows.Count > 0)
        {
            txtStock5.Text = Convert.ToDecimal(dt.Rows[0]["AvailQty"]).ToString();
            Calendar6.Text = dt.Rows[0]["EXPDATE"].ToString();
        }
        else
        {
            txtStock5.Text = "0.00";
            Calendar6.Text = "";
        }
        txtQty5_TextChanged(sender, e);
    }
    protected void txtBatch6_SelectedIndexChanged(object sender, EventArgs e)
    {
        Expirydt(ddlMedi6.SelectedValue, ddlMfg6.SelectedValue, txtBatch6.SelectedValue, Calendar7, txtUnitPrice6, expdt6);
        DataTable dt = theHelper.AvailableStock(Session["CoCode"].ToString().Trim(), Session["YearCode"].ToString().Trim(), ddlMedi6.SelectedValue, ddlMfg6.SelectedValue, txtBatch6.SelectedValue);
        if (dt.Rows.Count > 0)
        {
            txtStock6.Text = Convert.ToDecimal(dt.Rows[0]["AvailQty"]).ToString();
            Calendar7.Text = dt.Rows[0]["EXPDATE"].ToString();
        }
        else
        {
            txtStock6.Text = "0.00";
            Calendar7.Text = "";
        }
        txtQty6_TextChanged(sender, e);
    }
    protected void txtBatch7_SelectedIndexChanged(object sender, EventArgs e)
    {
        Expirydt(ddlMedi7.SelectedValue, ddlMfg7.SelectedValue, txtBatch7.SelectedValue, Calendar8, txtUnitPrice7, expdt7);
        DataTable dt = theHelper.AvailableStock(Session["CoCode"].ToString().Trim(), Session["YearCode"].ToString().Trim(), ddlMedi7.SelectedValue, ddlMfg7.SelectedValue, txtBatch7.SelectedValue);
        if (dt.Rows.Count > 0)
        {
            txtStock7.Text = Convert.ToDecimal(dt.Rows[0]["AvailQty"]).ToString();
            Calendar8.Text = dt.Rows[0]["EXPDATE"].ToString();
        }
        else
        {
            txtStock7.Text = "0.00";
            Calendar8.Text = "";
        }
        txtQty7_TextChanged(sender, e);
    }
    protected void txtBatch8_SelectedIndexChanged(object sender, EventArgs e)
    {
        Expirydt(ddlMedi8.SelectedValue, ddlMfg8.SelectedValue, txtBatch8.SelectedValue, Calendar9, txtUnitPrice8, expdt8);
        DataTable dt = theHelper.AvailableStock(Session["CoCode"].ToString().Trim(), Session["YearCode"].ToString().Trim(), ddlMedi8.SelectedValue, ddlMfg8.SelectedValue, txtBatch8.SelectedValue);
        if (dt.Rows.Count > 0)
        {
            txtStock8.Text = Convert.ToDecimal(dt.Rows[0]["AvailQty"]).ToString();
            Calendar9.Text = dt.Rows[0]["EXPDATE"].ToString();
        }
        else
        {
            txtStock8.Text = "0.00";
            Calendar9.Text = "";
        }
        txtQty8_TextChanged(sender, e);
    }
    protected void txtBatch9_SelectedIndexChanged(object sender, EventArgs e)
    {
        Expirydt(ddlMedi9.SelectedValue, ddlMfg9.SelectedValue, txtBatch9.SelectedValue, Calendar10, txtUnitPrice9, expdt9);
        DataTable dt = theHelper.AvailableStock(Session["CoCode"].ToString().Trim(), Session["YearCode"].ToString().Trim(), ddlMedi9.SelectedValue, ddlMfg9.SelectedValue, txtBatch9.SelectedValue);
        if (dt.Rows.Count > 0)
        {
            txtStock9.Text = Convert.ToDecimal(dt.Rows[0]["AvailQty"]).ToString();
            Calendar10.Text = dt.Rows[0]["EXPDATE"].ToString();
        }
        else
        {
            txtStock9.Text = "0.00";
            Calendar10.Text = "";
        }
        txtQty9_TextChanged(sender, e);
    }
    protected void txtBatch10_SelectedIndexChanged(object sender, EventArgs e)
    {
        Expirydt(ddlMedi10.SelectedValue, ddlMfg10.SelectedValue, txtBatch10.SelectedValue, Calendar11, txtUnitPrice10, expdt10);
        DataTable dt = theHelper.AvailableStock(Session["CoCode"].ToString().Trim(), Session["YearCode"].ToString().Trim(), ddlMedi10.SelectedValue, ddlMfg10.SelectedValue, txtBatch10.SelectedValue);
        if (dt.Rows.Count > 0)
        {
            txtStock10.Text = Convert.ToDecimal(dt.Rows[0]["AvailQty"]).ToString();
            Calendar11.Text = dt.Rows[0]["EXPDATE"].ToString();
        }
        else
        {
            txtStock10.Text = "0.00";
            Calendar11.Text = "";
        }
        txtQty10_TextChanged(sender, e);
    }
    protected void txtBatch11_SelectedIndexChanged(object sender, EventArgs e)
    {
        Expirydt(ddlMedi11.SelectedValue, ddlMfg11.SelectedValue, txtBatch11.SelectedValue, Calendar12, txtUnitPrice11, expdt11);
        DataTable dt = theHelper.AvailableStock(Session["CoCode"].ToString().Trim(), Session["YearCode"].ToString().Trim(), ddlMedi11.SelectedValue, ddlMfg11.SelectedValue, txtBatch11.SelectedValue);
        if (dt.Rows.Count > 0)
        {
            txtStock11.Text = Convert.ToDecimal(dt.Rows[0]["AvailQty"]).ToString();
            Calendar12.Text = dt.Rows[0]["EXPDATE"].ToString();
        }
        else
        {
            txtStock11.Text = "0.00";
            Calendar12.Text = "";
        }
        txtQty11_TextChanged(sender, e);
    }
    protected void txtBatch12_SelectedIndexChanged(object sender, EventArgs e)
    {
        Expirydt(ddlMedi12.SelectedValue, ddlMfg12.SelectedValue, txtBatch12.SelectedValue, Calendar13, txtUnitPrice12, expdt12);
        DataTable dt = theHelper.AvailableStock(Session["CoCode"].ToString().Trim(), Session["YearCode"].ToString().Trim(), ddlMedi12.SelectedValue, ddlMfg12.SelectedValue, txtBatch12.SelectedValue);
        if (dt.Rows.Count > 0)
        {
            txtStock12.Text = Convert.ToDecimal(dt.Rows[0]["AvailQty"]).ToString();
            Calendar13.Text = dt.Rows[0]["EXPDATE"].ToString();
        }
        else
        {
            txtStock12.Text = "0.00";
            Calendar13.Text = "";
        }
        txtQty12_TextChanged(sender, e);
    }


    private void MfgGrpSgrpFill(string MedCode, int i)
    {
        DropDownList ddlMfg = (DropDownList)Page.FindControl("ctl00$ContentPlaceHolder1$ddlMfg" + i.ToString());
        DropDownList ddlMediGrp = (DropDownList)Page.FindControl("ctl00$ContentPlaceHolder1$ddlMediGrp" + i.ToString());
        DropDownList ddlMediSubGrp = (DropDownList)Page.FindControl("ctl00$ContentPlaceHolder1$ddlMediSubGrp" + i.ToString());


        if (MedCode == "0" || theHelper.MedicineExist(Session["CoCode"].ToString().Trim(), MedCode).ToString() == "0")
        {
            ddlMfg.Items.Clear();
            ddlMfg.Items.Insert(0, new ListItem("--Select--", "0"));
            ddlMfg.SelectedIndex = 0;
            //ddlMfg.Enabled = false;

            ddlMediGrp.Items.Clear();
            ddlMediGrp.Items.Insert(0, new ListItem("--Select--", "0"));
            ddlMediGrp.SelectedIndex = 0;
            //ddlMediGrp.Enabled = false;

            ddlMediSubGrp.Items.Clear();
            ddlMediSubGrp.Items.Insert(0, new ListItem("--Select--", "0"));
            ddlMediSubGrp.SelectedIndex = 0;
            // ddlMediSubGrp.Enabled = false;

        }
        else
        {
            ddlMfg.Items.Clear();
            ddlMfg.DataSource = theHelper.DropdownMfg(Session["CoCode"].ToString().Trim(), Session["YearCode"].ToString().Trim(), MedCode);
            ddlMfg.DataTextField = "MName";
            ddlMfg.DataValueField = "MCode";
            ddlMfg.DataBind();


            ddlMediGrp.Items.Clear();
            ddlMediGrp.DataSource = theHelper.DropdownGrp(Session["CoCode"].ToString().Trim(), Session["YearCode"].ToString().Trim(), MedCode);
            ddlMediGrp.DataTextField = "MedicineGroupName";
            ddlMediGrp.DataValueField = "MedicineGroupID";
            ddlMediGrp.DataBind();
            // ddlMediGrp.Items.Insert(0, new ListItem("--Select--", "0"));

            ddlMediSubGrp.Items.Clear();
            ddlMediSubGrp.DataSource = theHelper.DropdownSubGrp(Session["CoCode"].ToString().Trim(), Session["YearCode"].ToString().Trim(), MedCode);
            ddlMediSubGrp.DataTextField = "SubGrName";
            ddlMediSubGrp.DataValueField = "ID";
            ddlMediSubGrp.DataBind();
            // ddlMediSubGrp.Items.Insert(0, new ListItem("--Select--", "0"));



        }
    }

    private void DdlIssueIDMfgFill(string MedId, DropDownList ddlmfg)
    {
        ddlmfg.Items.Clear();
        //ddlmfg.DataSource = theHelper.GetMedMfgByMCode(Session["CoCode"].ToString().Trim(), MedId);
        ddlmfg.DataSource = theHelper.GetMedGrpSubGrpMfgBymedID(Session["CoCode"].ToString().Trim(), MedId);
        ddlmfg.DataTextField = "MName";
        ddlmfg.DataValueField = "MCode";
        ddlmfg.DataBind();
        //ddlmfg.Items.Insert(0, new ListItem("--Select--", "0"));
    }

    private void DdlIssueMedicineSubGroupBind(string MedId, DropDownList ddlMedicineSubID)
    {
        ddlMedicineSubID.Items.Clear();
        //ddlMedicineSubID.DataSource = theHelper.GetMedSubGrpByID(Session["CoCode"].ToString().Trim(), MedId);
        ddlMedicineSubID.DataSource = theHelper.GetMedGrpSubGrpMfgBymedID(Session["CoCode"].ToString().Trim(), MedId);
        ddlMedicineSubID.DataTextField = "SubGrName";
        ddlMedicineSubID.DataValueField = "SubGroupid";
        ddlMedicineSubID.DataBind();
        //ddlMedicineSubID.Items.Insert(0, new ListItem("--Select--", "0"));
    }

    private void IssueMedicineGroupFill(string MedId, DropDownList ddlMedGrp)
    {
        ddlMedGrp.Items.Clear();
        //ddlMedGrp.DataSource = theHelper.GetMedGrpByID(Session["CoCode"].ToString().Trim(), MedId);
        ddlMedGrp.DataSource = theHelper.GetMedGrpSubGrpMfgBymedID(Session["CoCode"].ToString().Trim(), MedId);
        ddlMedGrp.DataTextField = "MedicineGroupName";
        ddlMedGrp.DataValueField = "MedicineGroupID";
        ddlMedGrp.DataBind();
        //ddlMedGrp.Items.Insert(0, new ListItem("--Select--", "0"));
    }

    private void DdlBatchBind(string mfg, string medicine, string docno, DropDownList ddlBath)
    {
        ddlBath.Items.Clear();
        ddlBath.DataSource = theHelper.getBatchId(Session["CoCode"].ToString().Trim(), Session["YearCode"].ToString().Trim(), mfg, medicine, docno);
        ddlBath.DataTextField = "BatchNo";
        ddlBath.DataValueField = "BatchNo";
        ddlBath.DataBind();
        ddlBath.Items.Insert(0, new ListItem("--Select--", "0"));
    }
    private void DdlIssueMedicineIDBind(DropDownList ddlMedicineID)
    {
        ddlMedicineID.Items.Clear();
        ddlMedicineID.DataSource = theHelper.DropdownMedicine(Session["CoCode"].ToString().Trim());
        ddlMedicineID.DataTextField = "MedicineName";
        ddlMedicineID.DataValueField = "MedicineID";
        ddlMedicineID.DataBind();
        ddlMedicineID.Items.Insert(0, new ListItem("--Select--", "0"));
    }
    protected void btnFetch_Click(object sender, EventArgs e)
    {
        string regno = Request.Form[txtreg.UniqueID];
        txtreg.Text = regno;
        DataTable dt = thedischarge.PatientDetailsForRequisition(regno, Session["CoCode"].ToString().Trim());
        txtPatientName.Text = dt.Rows[0]["PName"].ToString();
        TextBox3.Text = dt.Rows[0]["doc_name"].ToString();
        hdnLedgerId.Value = dt.Rows[0]["LedgerId"].ToString();
    }
    protected void txtdiscAmt1_TextChanged(object sender, EventArgs e)
    {
        double totalPrice = (Convert.ToDouble(txtUnitPrice1.Text) * (Convert.ToDouble(txtQty1.Text) / Convert.ToDouble(txtPackSize1.Text)) - Convert.ToDouble(txtdiscAmt1.Text));
        double taxableamt = totalPrice / (1 + (Convert.ToDouble(txtCgstRt1.Text) + Convert.ToDouble(txtSgstRt1.Text)) / 100);
        double cgstamt = taxableamt * (Convert.ToDouble(txtCgstRt1.Text) / 100);
        double sgstamt = taxableamt * (Convert.ToDouble(txtSgstRt1.Text) / 100);
        txtTaxableAmt1.Text = Math.Round(taxableamt, 2).ToString();
        txtCgstAmt1.Text = Math.Round(cgstamt, 2).ToString();
        txtSgstAmt1.Text = Math.Round(sgstamt, 2).ToString();
        txtTotalPrice1.Text = totalPrice.ToString();
        calculateTotal();
    }
    protected void txtdiscAmt2_TextChanged(object sender, EventArgs e)
    {
        double totalPrice = (Convert.ToDouble(txtUnitPrice2.Text) * (Convert.ToDouble(txtQty2.Text) / Convert.ToDouble(txtPackSize2.Text)) - Convert.ToDouble(txtdiscAmt2.Text));
        double taxableamt = totalPrice / (1 + (Convert.ToDouble(txtCgstRt2.Text) + Convert.ToDouble(txtSgstRt2.Text)) / 100);
        double cgstamt = taxableamt * (Convert.ToDouble(txtCgstRt2.Text) / 100);
        double sgstamt = taxableamt * (Convert.ToDouble(txtSgstRt2.Text) / 100);
        txtTaxableAmt2.Text = Math.Round(taxableamt, 2).ToString();
        txtCgstAmt2.Text = Math.Round(cgstamt, 2).ToString();
        txtSgstAmt2.Text = Math.Round(sgstamt, 2).ToString();
        txtTotalPrice2.Text = totalPrice.ToString();
        calculateTotal();
    }
    protected void txtdiscAmt3_TextChanged(object sender, EventArgs e)
    {
        double totalPrice = (Convert.ToDouble(txtUnitPrice3.Text) * (Convert.ToDouble(txtQty3.Text) / Convert.ToDouble(txtPackSize3.Text)) - Convert.ToDouble(txtdiscAmt3.Text));
        double taxableamt = totalPrice / (1 + (Convert.ToDouble(txtCgstRt3.Text) + Convert.ToDouble(txtSgstRt3.Text)) / 100);
        double cgstamt = taxableamt * (Convert.ToDouble(txtCgstRt3.Text) / 100);
        double sgstamt = taxableamt * (Convert.ToDouble(txtSgstRt3.Text) / 100);
        txtTaxableAmt3.Text = Math.Round(taxableamt, 2).ToString();
        txtCgstAmt3.Text = Math.Round(cgstamt, 2).ToString();
        txtSgstAmt3.Text = Math.Round(sgstamt, 2).ToString();
        txtTotalPrice3.Text = totalPrice.ToString();
    }
    protected void txtdiscAmt4_TextChanged(object sender, EventArgs e)
    {
        double totalPrice = (Convert.ToDouble(txtUnitPrice4.Text) * (Convert.ToDouble(txtQty4.Text) / Convert.ToDouble(txtPackSize4.Text)) - Convert.ToDouble(txtdiscAmt4.Text));
        double taxableamt = totalPrice / (1 + (Convert.ToDouble(txtCgstRt4.Text) + Convert.ToDouble(txtSgstRt4.Text) / 100));
        double cgstamt = taxableamt * (Convert.ToDouble(txtCgstRt4.Text) / 100);
        double sgstamt = taxableamt * (Convert.ToDouble(txtSgstRt4.Text) / 100);
        txtTaxableAmt4.Text = Math.Round(taxableamt, 2).ToString();
        txtCgstAmt4.Text = Math.Round(cgstamt, 2).ToString();
        txtSgstAmt4.Text = Math.Round(sgstamt, 2).ToString();
        txtTotalPrice4.Text = totalPrice.ToString();
        calculateTotal();
    }
    protected void txtdiscAmt5_TextChanged(object sender, EventArgs e)
    {
        double totalPrice = (Convert.ToDouble(txtUnitPrice5.Text) * (Convert.ToDouble(txtQty5.Text) / Convert.ToDouble(txtPackSize5.Text)) - Convert.ToDouble(txtdiscAmt5.Text));
        double taxableamt = totalPrice / (1 + (Convert.ToDouble(txtCgstRt5.Text) + Convert.ToDouble(txtSgstRt5.Text)) / 100);
        double cgstamt = taxableamt * (Convert.ToDouble(txtCgstRt5.Text) / 100);
        double sgstamt = taxableamt * (Convert.ToDouble(txtSgstRt5.Text) / 100);
        txtTaxableAmt5.Text = Math.Round(taxableamt, 2).ToString();
        txtCgstAmt5.Text = Math.Round(cgstamt, 2).ToString();
        txtSgstAmt5.Text = Math.Round(sgstamt, 2).ToString();
        txtTotalPrice5.Text = totalPrice.ToString();
        calculateTotal();
    }
    protected void txtdiscAmt6_TextChanged(object sender, EventArgs e)
    {
        double totalPrice = (Convert.ToDouble(txtUnitPrice6.Text) * (Convert.ToDouble(txtQty6.Text) / Convert.ToDouble(txtPackSize6.Text)) - Convert.ToDouble(txtdiscAmt6.Text));
        double taxableamt = totalPrice / (1 + (Convert.ToDouble(txtCgstRt6.Text) + Convert.ToDouble(txtSgstRt6.Text)) / 100);
        double cgstamt = taxableamt * (Convert.ToDouble(txtCgstRt6.Text) / 100);
        double sgstamt = taxableamt * (Convert.ToDouble(txtSgstRt6.Text) / 100);
        txtTaxableAmt6.Text = Math.Round(taxableamt, 2).ToString();
        txtCgstAmt6.Text = Math.Round(cgstamt, 2).ToString();
        txtSgstAmt6.Text = Math.Round(sgstamt, 2).ToString();
        txtTotalPrice6.Text = totalPrice.ToString();
        calculateTotal();
    }
    protected void txtdiscAmt7_TextChanged(object sender, EventArgs e)
    {
        double totalPrice = (Convert.ToDouble(txtUnitPrice7.Text) * (Convert.ToDouble(txtQty7.Text) / Convert.ToDouble(txtPackSize7.Text)) - Convert.ToDouble(txtdiscAmt7.Text));
        double taxableamt = totalPrice / (1 + (Convert.ToDouble(txtCgstRt7.Text) + Convert.ToDouble(txtSgstRt7.Text)) / 100);
        double cgstamt = taxableamt * (Convert.ToDouble(txtCgstRt7.Text) / 100);
        double sgstamt = taxableamt * (Convert.ToDouble(txtSgstRt7.Text) / 100);
        txtTaxableAmt7.Text = Math.Round(taxableamt, 2).ToString();
        txtCgstAmt7.Text = Math.Round(cgstamt, 2).ToString();
        txtSgstAmt7.Text = Math.Round(sgstamt, 2).ToString();
        txtTotalPrice7.Text = totalPrice.ToString();
        calculateTotal();
    }
    protected void txtdiscAmt8_TextChanged(object sender, EventArgs e)
    {
        double totalPrice = (Convert.ToDouble(txtUnitPrice8.Text) * (Convert.ToDouble(txtQty8.Text) / Convert.ToDouble(txtPackSize8.Text)) - Convert.ToDouble(txtdiscAmt8.Text));
        double taxableamt = totalPrice / (1 + (Convert.ToDouble(txtCgstRt8.Text) + Convert.ToDouble(txtSgstRt8.Text)) / 100);
        double cgstamt = taxableamt * (Convert.ToDouble(txtCgstRt8.Text) / 100);
        double sgstamt = taxableamt * (Convert.ToDouble(txtSgstRt8.Text) / 100);
        txtTaxableAmt8.Text = Math.Round(taxableamt, 2).ToString();
        txtCgstAmt8.Text = Math.Round(cgstamt, 2).ToString();
        txtSgstAmt8.Text = Math.Round(sgstamt, 2).ToString();
        txtTotalPrice8.Text = totalPrice.ToString();
        calculateTotal();
    }
    protected void txtdiscAmt9_TextChanged(object sender, EventArgs e)
    {
        double totalPrice = (Convert.ToDouble(txtUnitPrice9.Text) * (Convert.ToDouble(txtQty9.Text) / Convert.ToDouble(txtPackSize9.Text)) - Convert.ToDouble(txtdiscAmt9.Text));
        double taxableamt = totalPrice / (1 + (Convert.ToDouble(txtCgstRt9.Text) + Convert.ToDouble(txtSgstRt9.Text)) / 100);
        double cgstamt = taxableamt * (Convert.ToDouble(txtCgstRt9.Text) / 100);
        double sgstamt = taxableamt * (Convert.ToDouble(txtSgstRt9.Text) / 100);
        txtTaxableAmt9.Text = Math.Round(taxableamt, 2).ToString();
        txtCgstAmt9.Text = Math.Round(cgstamt, 2).ToString();
        txtSgstAmt9.Text = Math.Round(sgstamt, 2).ToString();
        txtTotalPrice9.Text = totalPrice.ToString();
        calculateTotal();
    }
    protected void txtdiscAmt10_TextChanged(object sender, EventArgs e)
    {
        double totalPrice = (Convert.ToDouble(txtUnitPrice10.Text) * (Convert.ToDouble(txtQty10.Text) / Convert.ToDouble(txtPackSize10.Text)) - Convert.ToDouble(txtdiscAmt10.Text));
        double taxableamt = totalPrice / (1 + (Convert.ToDouble(txtCgstRt10.Text) + Convert.ToDouble(txtSgstRt10.Text)) / 100);
        double cgstamt = taxableamt * (Convert.ToDouble(txtCgstRt10.Text) / 100);
        double sgstamt = taxableamt * (Convert.ToDouble(txtSgstRt10.Text) / 100);
        txtTaxableAmt10.Text = Math.Round(taxableamt, 2).ToString();
        txtCgstAmt10.Text = Math.Round(cgstamt, 2).ToString();
        txtSgstAmt10.Text = Math.Round(sgstamt, 2).ToString();
        txtTotalPrice10.Text = totalPrice.ToString();
        calculateTotal();
    }
    protected void txtdiscAmt11_TextChanged(object sender, EventArgs e)
    {
        double totalPrice = (Convert.ToDouble(txtUnitPrice11.Text) * (Convert.ToDouble(txtQty11.Text) / Convert.ToDouble(txtPackSize11.Text)) - Convert.ToDouble(txtdiscAmt11.Text));
        double taxableamt = totalPrice / (1 + (Convert.ToDouble(txtCgstRt11.Text) + Convert.ToDouble(txtSgstRt11.Text)) / 100);
        double cgstamt = taxableamt * (Convert.ToDouble(txtCgstRt11.Text) / 100);
        double sgstamt = taxableamt * (Convert.ToDouble(txtSgstRt11.Text) / 100);
        txtTaxableAmt11.Text = Math.Round(taxableamt, 2).ToString();
        txtCgstAmt11.Text = Math.Round(cgstamt, 2).ToString();
        txtSgstAmt11.Text = Math.Round(sgstamt, 2).ToString();
        txtTotalPrice11.Text = totalPrice.ToString();
        calculateTotal();
    }
    protected void txtdiscAmt12_DataBinding(object sender, EventArgs e)
    {
        double totalPrice = (Convert.ToDouble(txtUnitPrice12.Text) * (Convert.ToDouble(txtQty12.Text) / Convert.ToDouble(txtPackSize12.Text)) - Convert.ToDouble(txtdiscAmt12.Text));
        double taxableamt = totalPrice / (1 + (Convert.ToDouble(txtCgstRt12.Text) + Convert.ToDouble(txtSgstRt12.Text)) / 100);
        double cgstamt = taxableamt * (Convert.ToDouble(txtCgstRt12.Text) / 100);
        double sgstamt = taxableamt * (Convert.ToDouble(txtSgstRt12.Text) / 100);
        txtTaxableAmt12.Text = Math.Round(taxableamt, 2).ToString();
        txtCgstAmt12.Text = Math.Round(cgstamt, 2).ToString();
        txtSgstAmt12.Text = Math.Round(sgstamt, 2).ToString();
        txtTotalPrice12.Text = totalPrice.ToString();
        calculateTotal();
    }


    [System.Web.Script.Services.ScriptMethod()]
    [System.Web.Services.WebMethod]
    public static List<string> Searchdoc(string prefixText, int count)
    {
        string compcode = string.Empty;
        string yearcode = string.Empty;
        using (SqlConnection conn = new SqlConnection())
        {
            conn.ConnectionString = ConfigurationSettings.AppSettings["ConnectionString"].ToString();
            using (SqlCommand cmd = new SqlCommand())
            {
                compcode = HttpContext.Current.Session["CoCode"].ToString().Trim();
                yearcode = HttpContext.Current.Session["YearCode"].ToString();
                cmd.CommandText = "select doc_name as Name from GN_DoctorMaster where compcode=@Compcode and doc_name like @SearchText +'%'";
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
                cmd.CommandText = "select Pr.PatientRegNo + '~' + Pr.PName+'~'+Al.LedgerID as Name from opd_patientregistration Pr,AC_Ledger Al where Al.compcode=Pr.Compcode and Al.LedgerFK=Pr.PatientRegNo and Pr.compcode=@Compcode and Pr.PName like @SearchText + '%'";
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
    protected void btnInvoice_Click(object sender, EventArgs e)
    {
        GetReport();
    }
    public string getgetUnitDetails(string medId, string unit)
    {
        string convFactor = theHelper.getUnitDetls(Session["CoCode"].ToString().Trim(), medId, unit);
        return convFactor;
    }
    protected void ddlSellingUnit1_SelectedIndexChanged(object sender, EventArgs e)
    {
        string conversionFactor = getgetUnitDetails(ddlMedi1.SelectedValue.Trim(), ddlSellingUnit1.SelectedValue.Trim());
        txtPackSize1.Text = conversionFactor;
    }
    protected void ddlSellingUnit2_SelectedIndexChanged(object sender, EventArgs e)
    {
        string conversionFactor = getgetUnitDetails(ddlMedi2.SelectedValue.Trim(), ddlSellingUnit2.SelectedValue.Trim());
        txtPackSize2.Text = conversionFactor;
    }
    protected void ddlSellingUnit3_SelectedIndexChanged(object sender, EventArgs e)
    {
        string conversionFactor = getgetUnitDetails(ddlMedi3.SelectedValue.Trim(), ddlSellingUnit3.SelectedValue.Trim());
        txtPackSize3.Text = conversionFactor;
    }
    protected void ddlSellingUnit4_SelectedIndexChanged(object sender, EventArgs e)
    {
        string conversionFactor = getgetUnitDetails(ddlMedi4.SelectedValue.Trim(), ddlSellingUnit4.SelectedValue.Trim());
        txtPackSize4.Text = conversionFactor;
    }
    protected void ddlSellingUnit5_SelectedIndexChanged(object sender, EventArgs e)
    {
        string conversionFactor = getgetUnitDetails(ddlMedi5.SelectedValue.Trim(), ddlSellingUnit5.SelectedValue.Trim());
        txtPackSize5.Text = conversionFactor;
    }
    protected void ddlSellingUnit6_SelectedIndexChanged(object sender, EventArgs e)
    {
        string conversionFactor = getgetUnitDetails(ddlMedi6.SelectedValue.Trim(), ddlSellingUnit6.SelectedValue.Trim());
        txtPackSize6.Text = conversionFactor;
    }
    protected void ddlSellingUnit7_SelectedIndexChanged(object sender, EventArgs e)
    {
        string conversionFactor = getgetUnitDetails(ddlMedi7.SelectedValue.Trim(), ddlSellingUnit7.SelectedValue.Trim());
        txtPackSize7.Text = conversionFactor;
    }
    protected void ddlSellingUnit8_SelectedIndexChanged(object sender, EventArgs e)
    {
        string conversionFactor = getgetUnitDetails(ddlMedi8.SelectedValue.Trim(), ddlSellingUnit8.SelectedValue.Trim());
        txtPackSize8.Text = conversionFactor;
    }
    protected void ddlSellingUnit9_SelectedIndexChanged(object sender, EventArgs e)
    {
        string conversionFactor = getgetUnitDetails(ddlMedi9.SelectedValue.Trim(), ddlSellingUnit9.SelectedValue.Trim());
        txtPackSize9.Text = conversionFactor;
    }
    protected void ddlSellingUnit10_SelectedIndexChanged(object sender, EventArgs e)
    {
        string conversionFactor = getgetUnitDetails(ddlMedi10.SelectedValue.Trim(), ddlSellingUnit10.SelectedValue.Trim());
        txtPackSize10.Text = conversionFactor;
    }
    protected void ddlSellingUnit11_SelectedIndexChanged(object sender, EventArgs e)
    {
        string conversionFactor = getgetUnitDetails(ddlMedi11.SelectedValue.Trim(), ddlSellingUnit11.SelectedValue.Trim());
        txtPackSize11.Text = conversionFactor;
    }
    protected void ddlSellingUnit12_SelectedIndexChanged(object sender, EventArgs e)
    {
        string conversionFactor = getgetUnitDetails(ddlMedi12.SelectedValue.Trim(), ddlSellingUnit12.SelectedValue.Trim());
        txtPackSize12.Text = conversionFactor;
    }
    protected void txtPushChrg_TextChanged(object sender, EventArgs e)
    {
        calculateTotal();
    }

    //protected void ddlType1_SelectedIndexChanged(object sender, EventArgs e)
    //{

    //}
    protected void txtUnitPrice1_TextChanged(object sender, EventArgs e)
    {
        if (txtQty1.Text != "")
        {
            if (ddltype.SelectedValue == "S")
            {
                
                double discamt = (Convert.ToDouble(txtUnitPrice1.Text) * (Convert.ToDouble(txtQty1.Text) / Convert.ToDouble(txtPackSize1.Text)) * discrt);
                txtdiscAmt1.Text = discamt.ToString();
            }
            double totalPrice = (Convert.ToDouble(txtUnitPrice1.Text) * (Convert.ToDouble(txtQty1.Text) / Convert.ToDouble(txtPackSize1.Text)) - Convert.ToDouble(txtdiscAmt1.Text));
            double taxableamt = totalPrice / (1 + (Convert.ToDouble(txtCgstRt1.Text) + Convert.ToDouble(txtSgstRt1.Text)) / 100);
            double cgstamt = taxableamt * (Convert.ToDouble(txtCgstRt1.Text) / 100);
            double sgstamt = taxableamt * (Convert.ToDouble(txtSgstRt1.Text) / 100);
            txtTaxableAmt1.Text = Math.Round(taxableamt, 2).ToString();
            txtCgstAmt1.Text = Math.Round(cgstamt, 2).ToString();
            txtSgstAmt1.Text = Math.Round(sgstamt, 2).ToString();
            txtTotalPrice1.Text = totalPrice.ToString();
        }
        else
        {
            txtTaxableAmt1.Text = "0.00";
            txtCgstAmt1.Text = "0.00";
            txtSgstAmt1.Text = "0.00";
            txtTotalPrice1.Text = "0.00";
        }
        txtQty1.BorderColor = System.Drawing.Color.White;
        calculateTotal();
    }
    protected void txtUnitPrice2_TextChanged(object sender, EventArgs e)
    {
        if (txtQty2.Text != "")
        {
            if (ddltype.SelectedValue == "S")
            {
                
                double discamt = (Convert.ToDouble(txtUnitPrice2.Text) * (Convert.ToDouble(txtQty2.Text) / Convert.ToDouble(txtPackSize2.Text)) * discrt);
                txtdiscAmt2.Text = discamt.ToString();
            }
            double totalPrice = (Convert.ToDouble(txtUnitPrice2.Text) * (Convert.ToDouble(txtQty2.Text) / Convert.ToDouble(txtPackSize2.Text)) - Convert.ToDouble(txtdiscAmt2.Text));
            double taxableamt = totalPrice / (1 + (Convert.ToDouble(txtCgstRt2.Text) + Convert.ToDouble(txtSgstRt2.Text)) / 100);
            double cgstamt = taxableamt * (Convert.ToDouble(txtCgstRt2.Text) / 100);
            double sgstamt = taxableamt * (Convert.ToDouble(txtSgstRt2.Text) / 100);
            txtTaxableAmt2.Text = Math.Round(taxableamt, 2).ToString();
            txtCgstAmt2.Text = Math.Round(cgstamt, 2).ToString();
            txtSgstAmt2.Text = Math.Round(sgstamt, 2).ToString();
            txtTotalPrice2.Text = totalPrice.ToString();
        }
        else
        {
            txtTotalPrice2.Text = "0.00";//txtCgstRt2.Text = "0.00";
            txtCgstAmt2.Text = "0.00";
            txtSgstAmt2.Text = "0.00";
            txtTotalPrice2.Text = "0.00";
        }
        txtQty2.BorderColor = System.Drawing.Color.White;
        calculateTotal();
    }
    protected void txtUnitPrice3_TextChanged(object sender, EventArgs e)
    {
        if (txtQty3.Text != "")
        {
            if (ddltype.SelectedValue == "S")
            {
                
                double discamt = (Convert.ToDouble(txtUnitPrice3.Text) * (Convert.ToDouble(txtQty3.Text) / Convert.ToDouble(txtPackSize3.Text)) * discrt);
                txtdiscAmt3.Text = discamt.ToString();
            }
            double totalPrice = (Convert.ToDouble(txtUnitPrice3.Text) * (Convert.ToDouble(txtQty3.Text) / Convert.ToDouble(txtPackSize3.Text)) - Convert.ToDouble(txtdiscAmt3.Text));
            double taxableamt = totalPrice / (1 + (Convert.ToDouble(txtCgstRt3.Text) + Convert.ToDouble(txtSgstRt3.Text)) / 100);
            double cgstamt = taxableamt * (Convert.ToDouble(txtCgstRt3.Text) / 100);
            double sgstamt = taxableamt * (Convert.ToDouble(txtSgstRt3.Text) / 100);
            txtTaxableAmt3.Text = Math.Round(taxableamt, 2).ToString();
            txtCgstAmt3.Text = Math.Round(cgstamt, 2).ToString();
            txtSgstAmt3.Text = Math.Round(sgstamt, 2).ToString();
            txtTotalPrice3.Text = totalPrice.ToString();
        }
        else
        {
            txtTotalPrice3.Text = "0.00";
            txtCgstAmt3.Text = "0.00";
            txtSgstAmt3.Text = "0.00";
            txtTotalPrice3.Text = "0.00";
        }
        calculateTotal();
    }
    protected void txtUnitPrice4_TextChanged(object sender, EventArgs e)
    {
        if (txtQty4.Text != "")
        {
            if (ddltype.SelectedValue == "S")
            {
                
                double discamt = (Convert.ToDouble(txtUnitPrice4.Text) * (Convert.ToDouble(txtQty4.Text) / Convert.ToDouble(txtPackSize4.Text)) * discrt);
                txtdiscAmt4.Text = discamt.ToString();
            }
            double totalPrice = (Convert.ToDouble(txtUnitPrice4.Text) * (Convert.ToDouble(txtQty4.Text) / Convert.ToDouble(txtPackSize4.Text)) - Convert.ToDouble(txtdiscAmt4.Text));
            double taxableamt = totalPrice / (1 + (Convert.ToDouble(txtCgstRt4.Text) + Convert.ToDouble(txtSgstRt4.Text)) / 100);
            double cgstamt = taxableamt * (Convert.ToDouble(txtCgstRt4.Text) / 100);
            double sgstamt = taxableamt * (Convert.ToDouble(txtSgstRt4.Text) / 100);
            txtTaxableAmt4.Text = Math.Round(taxableamt, 2).ToString();
            txtCgstAmt4.Text = Math.Round(cgstamt, 2).ToString();
            txtSgstAmt4.Text = Math.Round(sgstamt, 2).ToString();
            txtTotalPrice4.Text = totalPrice.ToString();
        }
        else
        {
            txtTotalPrice4.Text = "0.00";
            txtCgstAmt4.Text = "0.00";
            txtSgstAmt4.Text = "0.00";
            txtTotalPrice4.Text = "0.00";
        }
        calculateTotal();
    }
    protected void txtUnitPrice5_TextChanged(object sender, EventArgs e)
    {
        if (txtQty5.Text != "")
        {
            if (ddltype.SelectedValue == "S")
            {
                
                double discamt = (Convert.ToDouble(txtUnitPrice5.Text) * (Convert.ToDouble(txtQty5.Text) / Convert.ToDouble(txtPackSize5.Text)) * discrt);
                txtdiscAmt5.Text = discamt.ToString();
            }
            double totalPrice = (Convert.ToDouble(txtUnitPrice5.Text) * (Convert.ToDouble(txtQty5.Text) / Convert.ToDouble(txtPackSize5.Text)) - Convert.ToDouble(txtdiscAmt5.Text));
            double taxableamt = totalPrice / (1 + (Convert.ToDouble(txtCgstRt5.Text) + Convert.ToDouble(txtSgstRt5.Text)) / 100);
            double cgstamt = taxableamt * (Convert.ToDouble(txtCgstRt5.Text) / 100);
            double sgstamt = taxableamt * (Convert.ToDouble(txtSgstRt5.Text) / 100);
            txtTaxableAmt5.Text = Math.Round(taxableamt, 2).ToString();
            txtCgstAmt5.Text = Math.Round(cgstamt, 2).ToString();
            txtSgstAmt5.Text = Math.Round(sgstamt, 2).ToString();
            txtTotalPrice5.Text = totalPrice.ToString();
        }
        else
        {
            txtTotalPrice5.Text = "0.00";
            txtCgstAmt5.Text = "0.00";
            txtSgstAmt5.Text = "0.00";
            txtTotalPrice5.Text = "0.00";
        }
        calculateTotal();
    }
    protected void txtUnitPrice6_TextChanged(object sender, EventArgs e)
    {
        if (txtQty6.Text != "")
        {
            if (ddltype.SelectedValue == "S")
            {
                
                double discamt = (Convert.ToDouble(txtUnitPrice6.Text) * (Convert.ToDouble(txtQty6.Text) / Convert.ToDouble(txtPackSize6.Text)) * discrt);
                txtdiscAmt6.Text = discamt.ToString();
            }
            double totalPrice = (Convert.ToDouble(txtUnitPrice6.Text) * (Convert.ToDouble(txtQty6.Text) / Convert.ToDouble(txtPackSize6.Text)) - Convert.ToDouble(txtdiscAmt6.Text));
            double taxableamt = totalPrice / (1 + (Convert.ToDouble(txtCgstRt6.Text) + Convert.ToDouble(txtSgstRt6.Text)) / 100);
            double cgstamt = taxableamt * (Convert.ToDouble(txtCgstRt6.Text) / 100);
            double sgstamt = taxableamt * (Convert.ToDouble(txtSgstRt6.Text) / 100);
            txtTaxableAmt1.Text = Math.Round(taxableamt, 2).ToString();
            txtCgstAmt6.Text = Math.Round(cgstamt, 2).ToString();
            txtSgstAmt6.Text = Math.Round(sgstamt, 2).ToString();
            txtTotalPrice6.Text = totalPrice.ToString();
        }
        else
        {
            txtTotalPrice6.Text = "0.00";
            txtCgstAmt6.Text = "0.00";
            txtSgstAmt6.Text = "0.00";
            txtTotalPrice6.Text = "0.00";
        }
        calculateTotal();
    }
    protected void txtUnitPrice7_TextChanged(object sender, EventArgs e)
    {
        if (txtQty7.Text != "")
        {
            if (ddltype.SelectedValue == "S")
            {
                
                double discamt = (Convert.ToDouble(txtUnitPrice7.Text) * (Convert.ToDouble(txtQty7.Text) / Convert.ToDouble(txtPackSize7.Text)) * discrt);
                txtdiscAmt7.Text = discamt.ToString();
            }
            double totalPrice = (Convert.ToDouble(txtUnitPrice7.Text) * (Convert.ToDouble(txtQty7.Text) / Convert.ToDouble(txtPackSize7.Text)) - Convert.ToDouble(txtdiscAmt7.Text));
            double taxableamt = totalPrice / (1 + (Convert.ToDouble(txtCgstRt7.Text) + Convert.ToDouble(txtSgstRt7.Text)) / 100);
            double cgstamt = taxableamt * (Convert.ToDouble(txtCgstRt7.Text) / 100);
            double sgstamt = taxableamt * (Convert.ToDouble(txtSgstRt7.Text) / 100);
            txtTaxableAmt7.Text = Math.Round(taxableamt, 2).ToString();
            txtCgstAmt7.Text = Math.Round(cgstamt, 2).ToString();
            txtSgstAmt7.Text = Math.Round(sgstamt, 2).ToString();
            txtTotalPrice7.Text = totalPrice.ToString();
        }
        else
        {
            txtTotalPrice7.Text = "0.00";
            txtCgstAmt7.Text = "0.00";
            txtSgstAmt7.Text = "0.00";
            txtTotalPrice7.Text = "0.00";
        }
        calculateTotal();
    }
    protected void txtUnitPrice8_TextChanged(object sender, EventArgs e)
    {
        if (txtQty8.Text != "")
        {
            if (ddltype.SelectedValue == "S")
            {
                
                double discamt = (Convert.ToDouble(txtUnitPrice8.Text) * (Convert.ToDouble(txtQty8.Text) / Convert.ToDouble(txtPackSize8.Text)) * discrt);
                txtdiscAmt8.Text = discamt.ToString();
            }
            double totalPrice = (Convert.ToDouble(txtUnitPrice8.Text) * (Convert.ToDouble(txtQty8.Text) / Convert.ToDouble(txtPackSize8.Text)) - Convert.ToDouble(txtdiscAmt8.Text));
            double taxableamt = totalPrice / (1 + (Convert.ToDouble(txtCgstRt8.Text) + Convert.ToDouble(txtSgstRt8.Text)) / 100);
            double cgstamt = taxableamt * (Convert.ToDouble(txtCgstRt8.Text) / 100);
            double sgstamt = taxableamt * (Convert.ToDouble(txtSgstRt8.Text) / 100);
            txtTaxableAmt8.Text = Math.Round(taxableamt, 2).ToString();
            txtCgstAmt8.Text = Math.Round(cgstamt, 2).ToString();
            txtSgstAmt8.Text = Math.Round(sgstamt, 2).ToString();
            txtTotalPrice8.Text = totalPrice.ToString();
        }
        else
        {
            txtTotalPrice8.Text = "0.00";
            txtCgstAmt8.Text = "0.00";
            txtSgstAmt8.Text = "0.00";
            txtTotalPrice8.Text = "0.00";
        }
        calculateTotal();
    }
    protected void txtUnitPrice9_TextChanged(object sender, EventArgs e)
    {
        if (txtQty9.Text != "")
        {
            if (ddltype.SelectedValue == "S")
            {
                
                double discamt = (Convert.ToDouble(txtUnitPrice9.Text) * (Convert.ToDouble(txtQty9.Text) / Convert.ToDouble(txtPackSize9.Text)) * discrt);
                txtdiscAmt9.Text = discamt.ToString();
            }
            double totalPrice = (Convert.ToDouble(txtUnitPrice9.Text) * (Convert.ToDouble(txtQty9.Text) / Convert.ToDouble(txtPackSize9.Text)) - Convert.ToDouble(txtdiscAmt9.Text));
            double taxableamt = totalPrice / (1 + (Convert.ToDouble(txtCgstRt9.Text) + Convert.ToDouble(txtSgstRt9.Text)) / 100);
            double cgstamt = taxableamt * (Convert.ToDouble(txtCgstRt9.Text) / 100);
            double sgstamt = taxableamt * (Convert.ToDouble(txtSgstRt9.Text) / 100);
            txtTaxableAmt9.Text = Math.Round(taxableamt, 2).ToString();
            txtCgstAmt9.Text = Math.Round(cgstamt, 2).ToString();
            txtSgstAmt9.Text = Math.Round(sgstamt, 2).ToString();
            txtTotalPrice9.Text = totalPrice.ToString();
        }
        else
        {
            txtTotalPrice9.Text = "0.00";
            txtCgstAmt9.Text = "0.00";
            txtSgstAmt9.Text = "0.00";
            txtTotalPrice9.Text = "0.00";
        }
        calculateTotal();
    }
    protected void txtUnitPrice10_TextChanged(object sender, EventArgs e)
    {
        if (txtQty10.Text != "")
        {
            if (ddltype.SelectedValue == "S")
            {
                
                double discamt = (Convert.ToDouble(txtUnitPrice10.Text) * (Convert.ToDouble(txtQty10.Text) / Convert.ToDouble(txtPackSize10.Text)) * discrt);
                txtdiscAmt10.Text = discamt.ToString();
            }
            double totalPrice = (Convert.ToDouble(txtUnitPrice10.Text) * (Convert.ToDouble(txtQty10.Text) / Convert.ToDouble(txtPackSize10.Text)) - Convert.ToDouble(txtdiscAmt10.Text));
            double taxableamt = totalPrice / (1 + (Convert.ToDouble(txtCgstRt10.Text) + Convert.ToDouble(txtSgstRt10.Text)) / 100);
            double cgstamt = taxableamt * (Convert.ToDouble(txtCgstRt10.Text) / 100);
            double sgstamt = taxableamt * (Convert.ToDouble(txtSgstRt10.Text) / 100);
            txtTaxableAmt10.Text = Math.Round(taxableamt, 2).ToString();
            txtCgstAmt10.Text = Math.Round(cgstamt, 2).ToString();
            txtSgstAmt10.Text = Math.Round(sgstamt, 2).ToString();
            txtTotalPrice10.Text = totalPrice.ToString();
        }
        else
        {
            txtTotalPrice10.Text = "0.00";
            txtCgstAmt10.Text = "0.00";
            txtSgstAmt10.Text = "0.00";
            txtTotalPrice10.Text = "0.00";
        }
        calculateTotal();
    }
    protected void txtUnitPrice11_TextChanged(object sender, EventArgs e)
    {
        if (txtQty11.Text != "")
        {
            if (ddltype.SelectedValue == "S")
            {
                
                double discamt = (Convert.ToDouble(txtUnitPrice11.Text) * (Convert.ToDouble(txtQty11.Text) / Convert.ToDouble(txtPackSize11.Text)) * discrt);
                txtdiscAmt11.Text = discamt.ToString();
            }
            double totalPrice = (Convert.ToDouble(txtUnitPrice11.Text) * (Convert.ToDouble(txtQty11.Text) / Convert.ToDouble(txtPackSize11.Text)) - Convert.ToDouble(txtdiscAmt11.Text));
            double taxableamt = totalPrice / (1 + (Convert.ToDouble(txtCgstRt11.Text) + Convert.ToDouble(txtSgstRt11.Text)) / 100);
            double cgstamt = taxableamt * (Convert.ToDouble(txtCgstRt11.Text) / 100);
            double sgstamt = taxableamt * (Convert.ToDouble(txtSgstRt11.Text) / 100);
            txtTaxableAmt11.Text = Math.Round(taxableamt, 2).ToString();
            txtCgstAmt11.Text = Math.Round(cgstamt, 2).ToString();
            txtSgstAmt11.Text = Math.Round(sgstamt, 2).ToString();
            txtTotalPrice11.Text = totalPrice.ToString();
        }
        else
        {
            txtTotalPrice11.Text = "0.00";
            txtCgstAmt11.Text = "0.00";
            txtSgstAmt11.Text = "0.00";
            txtTotalPrice11.Text = "0.00";
        }
        calculateTotal();
    }
    protected void txtUnitPrice12_TextChanged(object sender, EventArgs e)
    {
        if (txtQty12.Text != "")
        {
            if (ddltype.SelectedValue == "S")
            {
                
                double discamt = (Convert.ToDouble(txtUnitPrice12.Text) * (Convert.ToDouble(txtQty12.Text) / Convert.ToDouble(txtPackSize12.Text)) * discrt);
                txtdiscAmt12.Text = discamt.ToString();
            }
            double totalPrice = (Convert.ToDouble(txtUnitPrice12.Text) * (Convert.ToDouble(txtQty12.Text) / Convert.ToDouble(txtPackSize12.Text)) - Convert.ToDouble(txtdiscAmt12.Text));
            double taxableamt = totalPrice / (1 + (Convert.ToDouble(txtCgstRt12.Text) + Convert.ToDouble(txtSgstRt12.Text)) / 100);
            double cgstamt = taxableamt * (Convert.ToDouble(txtCgstRt12.Text) / 100);
            double sgstamt = taxableamt * (Convert.ToDouble(txtSgstRt12.Text) / 100);
            txtTaxableAmt12.Text = Math.Round(taxableamt, 2).ToString();
            txtCgstAmt12.Text = Math.Round(cgstamt, 2).ToString();
            txtSgstAmt12.Text = Math.Round(sgstamt, 2).ToString();
            txtTotalPrice12.Text = totalPrice.ToString();
        }
        else
        {
            txtTotalPrice12.Text = "0.00";
            txtCgstAmt12.Text = "0.00";
            txtSgstAmt12.Text = "0.00";
            txtTotalPrice12.Text = "0.00";
        }

        calculateTotal();
    }
    protected void ddltype_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddltype.SelectedValue == "S")
        {
            divs.Visible = true;
            divp.Visible = false;
        }
        else
        {
            divs.Visible = false;
            divp.Visible = true;
        }
    }
    protected void ddlstaff_SelectedIndexChanged(object sender, EventArgs e)
    {
        hdnregno.Value = ddlstaff.SelectedValue;
        txtreg.Text = ddlstaff.SelectedValue;
    }
    protected void txtChrgAmt1_TextChanged(object sender, EventArgs e)
    {
        calculateTotal();
    }
    protected void txtChrgAmt2_TextChanged(object sender, EventArgs e)
    {
        calculateTotal();
    }
    protected void Button7_Click(object sender, EventArgs e)
    {
        GetReport();
    }
}