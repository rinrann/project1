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
using System.Globalization;

public partial class Medicine_MedicineSales : System.Web.UI.Page
{
    Main Objmain = new Main(ConfigurationSettings.AppSettings["ConnectionString"].ToString());
    MedicineSales theHelper = new MedicineSales(ConfigurationSettings.AppSettings["ConnectionString"].ToString());
    BedMaster theHelperBed = new BedMaster(ConfigurationSettings.AppSettings["ConnectionString"].ToString());
    System.Text.StringBuilder rpt = new System.Text.StringBuilder();
    String Date = DateTime.Now.ToString("dd/MM/yyyy");
    protected void Page_Load(object sender, EventArgs e)
    {
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
        if (theHelper.SalesParameter(Session["CoCode"].ToString().Trim(), Session["YearCode"].ToString().Trim()) == "P")
        {
            lblid2.Text = "<span style='color:red;'>*</span>";
        }
        else
        {
            lblid1.Text = "<span style='color:red;'>*</span>";
        }
        if (!IsPostBack)
        {
            DropDownFill();
            GenerateCode();
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

 
  
    private void ResetAllFields()
    {
        hdnMode.Value = "0";
        ddlwing.SelectedIndex = 0; ddlPatient.SelectedIndex = 0; TextBox2.Text = ""; TextBox3.Text = ""; TextBox4.Text = ""; TextBox6.Text = ""; TextBox7.Text = "";
        GenerateCode();
        Button1.Text = "SUBMIT";

        for (int i = 1,cal=2; i <= 12; i++,cal++)
        {
            DropDownList ddlMfg = (DropDownList)divContent.FindControl("ddlMfg" + i.ToString());
            ddlMfg.SelectedIndex = -1;
            DropDownList ddlMediGrp = (DropDownList)divContent.FindControl("ddlMediGrp" + i.ToString());
            ddlMediGrp.SelectedIndex = -1;

            DropDownList ddlMediSubGrp = (DropDownList)divContent.FindControl("ddlMediSubGrp" + i.ToString());
            ddlMediSubGrp.SelectedIndex =-1;
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
        ddlwing.Items.Clear();
        /*this.ddlwing.DataSource = theHelper.TxtBoxList(compcode,"W");
        this.ddlwing.DataTextField = "name";
        this.ddlwing.DataValueField = "code";
        this.ddlwing.DataBind();*/
        this.ddlwing.Items.Insert(0, new ListItem("--Select--", "0"));

        ddlFloor.Items.Clear();
        this.ddlFloor.DataSource = theHelper.getFloor(compcode);
        this.ddlFloor.DataTextField = "FloorName";
        this.ddlFloor.DataValueField = "FloorId";
        this.ddlFloor.DataBind();
        this.ddlFloor.Items.Insert(0, new ListItem("--Select--", "0"));

        ddlPatient.Items.Clear();
        this.ddlPatient.DataSource = theHelper.TxtBoxList(compcode,"P");
        this.ddlPatient.DataTextField = "name";
        this.ddlPatient.DataValueField = "slcode";
        this.ddlPatient.DataBind();
        this.ddlPatient.Items.Insert(0, new ListItem("--Select--", "0"));
        
        /*ddlMfg1.Items.Clear();
        this.ddlMfg1.DataSource = theHelper.DropdownID2(compcode);
        this.ddlMfg1.DataTextField = "MName";
        this.ddlMfg1.DataValueField = "MCode";
        this.ddlMfg1.DataBind();
        this.ddlMfg1.Items.Insert(0, new ListItem("--Select--", "0"));

        ddlMfg2.Items.Clear();
        this.ddlMfg2.DataSource = theHelper.DropdownID2(compcode);
        this.ddlMfg2.DataTextField = "MName";
        this.ddlMfg2.DataValueField = "MCode";
        this.ddlMfg2.DataBind();
        this.ddlMfg2.Items.Insert(0, new ListItem("--Select--", "0"));


        ddlMfg3.Items.Clear();
        this.ddlMfg3.DataSource = theHelper.DropdownID2(compcode);
        this.ddlMfg3.DataTextField = "MName";
        this.ddlMfg3.DataValueField = "MCode";
        this.ddlMfg3.DataBind();
        this.ddlMfg3.Items.Insert(0, new ListItem("--Select--", "0"));

        ddlMfg4.Items.Clear();
        this.ddlMfg4.DataSource = theHelper.DropdownID2(compcode);
        this.ddlMfg4.DataTextField = "MName";
        this.ddlMfg4.DataValueField = "MCode";
        this.ddlMfg4.DataBind();
        this.ddlMfg4.Items.Insert(0, new ListItem("--Select--", "0"));

        ddlMfg5.Items.Clear();
        this.ddlMfg5.DataSource = theHelper.DropdownID2(compcode);
        this.ddlMfg5.DataTextField = "MName";
        this.ddlMfg5.DataValueField = "MCode";
        this.ddlMfg5.DataBind();
        this.ddlMfg5.Items.Insert(0, new ListItem("--Select--", "0"));

        ddlMfg6.Items.Clear();
        this.ddlMfg6.DataSource = theHelper.DropdownID2(compcode);
        this.ddlMfg6.DataTextField = "MName";
        this.ddlMfg6.DataValueField = "MCode";
        this.ddlMfg6.DataBind();
        this.ddlMfg6.Items.Insert(0, new ListItem("--Select--", "0"));

        ddlMfg7.Items.Clear();
        this.ddlMfg7.DataSource = theHelper.DropdownID2(compcode);
        this.ddlMfg7.DataTextField = "MName";
        this.ddlMfg7.DataValueField = "MCode";
        this.ddlMfg7.DataBind();
        this.ddlMfg7.Items.Insert(0, new ListItem("--Select--", "0"));

        ddlMfg8.Items.Clear();
        this.ddlMfg8.DataSource = theHelper.DropdownID2(compcode);
        this.ddlMfg8.DataTextField = "MName";
        this.ddlMfg8.DataValueField = "MCode";
        this.ddlMfg8.DataBind();
        this.ddlMfg8.Items.Insert(0, new ListItem("--Select--", "0"));

        ddlMfg9.Items.Clear();
        this.ddlMfg9.DataSource = theHelper.DropdownID2(compcode);
        this.ddlMfg9.DataTextField = "MName";
        this.ddlMfg9.DataValueField = "MCode";
        this.ddlMfg9.DataBind();
        this.ddlMfg9.Items.Insert(0, new ListItem("--Select--", "0"));

        ddlMfg10.Items.Clear();
        this.ddlMfg10.DataSource = theHelper.DropdownID2(compcode);
        this.ddlMfg10.DataTextField = "MName";
        this.ddlMfg10.DataValueField = "MCode";
        this.ddlMfg10.DataBind();
        this.ddlMfg10.Items.Insert(0, new ListItem("--Select--", "0"));

        ddlMfg11.Items.Clear();
        this.ddlMfg11.DataSource = theHelper.DropdownID2(compcode);
        this.ddlMfg11.DataTextField = "MName";
        this.ddlMfg11.DataValueField = "MCode";
        this.ddlMfg11.DataBind();
        this.ddlMfg11.Items.Insert(0, new ListItem("--Select--", "0"));

        ddlMfg12.Items.Clear();
        this.ddlMfg12.DataSource = theHelper.DropdownID2(compcode);
        this.ddlMfg12.DataTextField = "MName";
        this.ddlMfg12.DataValueField = "MCode";
        this.ddlMfg12.DataBind();
        this.ddlMfg12.Items.Insert(0, new ListItem("--Select--", "0"));


        ddlMediGrp1.Items.Clear();
        this.ddlMediGrp1.DataSource = theHelper.DropdownID3(compcode);
        this.ddlMediGrp1.DataTextField = "MedicineGroupName";
        this.ddlMediGrp1.DataValueField = "MedicineGroupID";
        this.ddlMediGrp1.DataBind();
        this.ddlMediGrp1.Items.Insert(0, new ListItem("--Select--", "0"));

        ddlMediGrp2.Items.Clear();
        this.ddlMediGrp2.DataSource = theHelper.DropdownID3(compcode);
        this.ddlMediGrp2.DataTextField = "MedicineGroupName";
        this.ddlMediGrp2.DataValueField = "MedicineGroupID";
        this.ddlMediGrp2.DataBind();
        this.ddlMediGrp2.Items.Insert(0, new ListItem("--Select--", "0"));

        ddlMediGrp3.Items.Clear();
        this.ddlMediGrp3.DataSource = theHelper.DropdownID3(compcode);
        this.ddlMediGrp3.DataTextField = "MedicineGroupName";
        this.ddlMediGrp3.DataValueField = "MedicineGroupID";
        this.ddlMediGrp3.DataBind();
        this.ddlMediGrp3.Items.Insert(0, new ListItem("--Select--", "0"));

        ddlMediGrp4.Items.Clear();
        this.ddlMediGrp4.DataSource = theHelper.DropdownID3(compcode);
        this.ddlMediGrp4.DataTextField = "MedicineGroupName";
        this.ddlMediGrp4.DataValueField = "MedicineGroupID";
        this.ddlMediGrp4.DataBind();
        this.ddlMediGrp4.Items.Insert(0, new ListItem("--Select--", "0"));

        ddlMediGrp5.Items.Clear();
        this.ddlMediGrp5.DataSource = theHelper.DropdownID3(compcode);
        this.ddlMediGrp5.DataTextField = "MedicineGroupName";
        this.ddlMediGrp5.DataValueField = "MedicineGroupID";
        this.ddlMediGrp5.DataBind();
        this.ddlMediGrp5.Items.Insert(0, new ListItem("--Select--", "0"));

        ddlMediGrp6.Items.Clear();
        this.ddlMediGrp6.DataSource = theHelper.DropdownID3(compcode);
        this.ddlMediGrp6.DataTextField = "MedicineGroupName";
        this.ddlMediGrp6.DataValueField = "MedicineGroupID";
        this.ddlMediGrp6.DataBind();
        this.ddlMediGrp6.Items.Insert(0, new ListItem("--Select--", "0"));

        ddlMediGrp7.Items.Clear();
        this.ddlMediGrp7.DataSource = theHelper.DropdownID3(compcode);
        this.ddlMediGrp7.DataTextField = "MedicineGroupName";
        this.ddlMediGrp7.DataValueField = "MedicineGroupID";
        this.ddlMediGrp7.DataBind();
        this.ddlMediGrp7.Items.Insert(0, new ListItem("--Select--", "0"));

        ddlMediGrp8.Items.Clear();
        this.ddlMediGrp8.DataSource = theHelper.DropdownID3(compcode);
        this.ddlMediGrp8.DataTextField = "MedicineGroupName";
        this.ddlMediGrp8.DataValueField = "MedicineGroupID";
        this.ddlMediGrp8.DataBind();
        this.ddlMediGrp8.Items.Insert(0, new ListItem("--Select--", "0"));

        ddlMediGrp9.Items.Clear();
        this.ddlMediGrp9.DataSource = theHelper.DropdownID3(compcode);
        this.ddlMediGrp9.DataTextField = "MedicineGroupName";
        this.ddlMediGrp9.DataValueField = "MedicineGroupID";
        this.ddlMediGrp9.DataBind();
        this.ddlMediGrp9.Items.Insert(0, new ListItem("--Select--", "0"));

        ddlMediGrp10.Items.Clear();
        this.ddlMediGrp10.DataSource = theHelper.DropdownID3(compcode);
        this.ddlMediGrp10.DataTextField = "MedicineGroupName";
        this.ddlMediGrp10.DataValueField = "MedicineGroupID";
        this.ddlMediGrp10.DataBind();
        this.ddlMediGrp10.Items.Insert(0, new ListItem("--Select--", "0"));

        ddlMediGrp11.Items.Clear();
        this.ddlMediGrp11.DataSource = theHelper.DropdownID3(compcode);
        this.ddlMediGrp11.DataTextField = "MedicineGroupName";
        this.ddlMediGrp11.DataValueField = "MedicineGroupID";
        this.ddlMediGrp11.DataBind();
        this.ddlMediGrp11.Items.Insert(0, new ListItem("--Select--", "0"));

        ddlMediGrp12.Items.Clear();
        this.ddlMediGrp12.DataSource = theHelper.DropdownID3(compcode);
        this.ddlMediGrp12.DataTextField = "MedicineGroupName";
        this.ddlMediGrp12.DataValueField = "MedicineGroupID";
        this.ddlMediGrp12.DataBind();
        this.ddlMediGrp12.Items.Insert(0, new ListItem("--Select--", "0"));*/

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
        TextBox t1;
        string compcode = Session["CoCode"].ToString().Trim();
        string yearcode = Session["YearCode"].ToString().Trim();
        HiddenField1.Value = TextBox5.Text;
        double total = 0.0;
        for (int i = 1; i <= 12; i++)
        {
            t1 = (TextBox)Page.FindControl("ctl00$ContentPlaceHolder1$txtTotalPrice" + i.ToString());
            if (t1.Text != "")
                total = total + Convert.ToDouble(t1.Text);
        }
        try
        {
            if (Button1.Text.ToUpper() == "SUBMIT")
            {
                if (theHelper.SalesParameter(Session["CoCode"].ToString().Trim(), Session["YearCode"].ToString().Trim()) == "W" && ddlwing.SelectedValue == "0")
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "msg", "alert('Select Work Station / Wings !');", true);                    
                }
                else if (theHelper.SalesParameter(Session["CoCode"].ToString().Trim(), Session["YearCode"].ToString().Trim()) == "P" && ddlPatient.SelectedValue == "0")
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "msg", "alert('Select Patient !');", true);
                }
                else if (TextBox6.Text=="")
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "msg", "alert('Bill Date can not be blank !');", true);
                }
                else
                {
                    InsertPurchaseMedicine(compcode, yearcode, Convert.ToDouble(total));
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "msg", "alert('Inserted  Successfully !');", true);
                    //GetReport();
                }
            }
            else
            {
                if (theHelper.SalesParameter(Session["CoCode"].ToString().Trim(), Session["YearCode"].ToString().Trim()) == "W" && ddlwing.SelectedValue == "0")
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "msg", "alert('Select Work Station / Wings !');", true);
                }
                else if (theHelper.SalesParameter(Session["CoCode"].ToString().Trim(), Session["YearCode"].ToString().Trim()) == "P" && ddlPatient.SelectedValue == "0")
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "msg", "alert('Select Patient !');", true);
                }
                else if (TextBox6.Text == "")
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "msg", "alert('Bill Date can not be blank !');", true);
                }
                else
                {
                    System.Globalization.DateTimeFormatInfo dtf = new DateTimeFormatInfo();
                    DateTime testdate = DateTime.ParseExact(TextBox6.Text, "dd/MM/yyyy", dtf);
                    theHelper.UpdatePurchaseMedicine(compcode, yearcode, TextBox5.Text, ddlPatient.SelectedValue, TextBox2.Text, TextBox4.Text, TextBox3.Text, total.ToString(), testdate.ToString(), TextBox7.Text, ddlwing.SelectedValue);
                    theHelper.DeleteMEdDtls(Session["CoCode"].ToString().Trim(), Session["YearCode"].ToString().Trim(),TextBox5.Text);
                    InsertPurchaseMedicine(compcode, yearcode, Convert.ToDouble(total));
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "msg", "alert('Updated  Successfully !');", true);
                    //GetReport();
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
    public bool validation(string MedId, string MfgId, string batchno,TextBox txtqty)
    {
        DataTable dt = theHelper.AvailableStock(Session["CoCode"].ToString().Trim(), Session["YearCode"].ToString().Trim(),MedId, MfgId, batchno);
        Decimal amt = Convert.ToDecimal(dt.Rows[0]["AvailQty"]);
        int i;Decimal curamt=0;
        for (i = 1; i <= 12; i++)
        {
            DropDownList d = (DropDownList)Page.FindControl("ctl00$ContentPlaceHolder1$ddlMedi" + i.ToString());
            TextBox txt = (TextBox)Page.FindControl("ctl00$ContentPlaceHolder1$txtQty" + i.ToString());
            HiddenField qty = (HiddenField)Page.FindControl("ctl00$ContentPlaceHolder1$qty" + i.ToString());
            if (qty.Value.ToString()== "") { qty.Value = "0"; }
            if (d.SelectedValue == MedId)
            {
                curamt = curamt + Convert.ToDecimal(txt.Text);
                if (hdnInsUpd.Value == "U" && txt == txtqty)
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
        Report_Header();
        GetHearder_Detail();
        ltrReport.Text = rpt.ToString();
    }


    public void Report_Header()
    {
        rpt.Append("<br/>"); rpt.Append("<br/>"); rpt.Append("<br/>");
        rpt.Append("<table width='100%' cellspacing=0 border=0 style='border:1px solid black;background-color:#9B9C8D;border-spacing: 0; padding: 0;font-family:verdana;'>");
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
        DataTable dtPurchaseMedicine = theHelper.GetSaleMedicineDetails(Session["CoCode"].ToString().Trim(), Session["YearCode"].ToString().Trim(), HiddenField1.Value);

        if (dtPurchaseMedicine.Rows.Count > 0)
        {
            rpt.Append("<br/>");
            rpt.Append("<table width='900px' cellspacing=0 border=0 style='border-spacing: 0; padding: 0;font-family:verdana;'>");
            rpt.Append("<tr style='height:20px'>");
            rpt.Append("<td colspan='4' style='width: 8%;border-bottom: 1px solid black; font-family:Verdana;font-weight:bold;background-color:#9B9C8D;font-size:larger; text-align:Center'> Medicine Sale Invoice  </td>");
            rpt.Append("</tr'>");

            rpt.Append("<tr style='height:20px'>");
            rpt.Append("<td style='width: 190px; font-family:Verdana;font-weight:bold;font-size:small; text-align:left'>Invoice No :</td>");
            rpt.AppendFormat("<td style='width: 200px; font-family:Verdana; font-size:small;text-align:left'>{0}</td>", HiddenField1.Value);
            rpt.Append("<td style='width: 150px;font-family:Verdana;font-weight:bold;font-size:small; text-align:left'></td>");
            rpt.Append("<td style='width: 250px;font-family:Verdana;font-weight:bold;font-size:small; text-align:left'></td>");
            rpt.Append("</tr >");

            rpt.Append("<tr style='height:20px'>");
            rpt.Append("<td style='width: 190px;font-family:Verdana;font-weight:bold;font-size:small; text-align:left'>Issue Date :</td>");
            rpt.AppendFormat("<td style='width: 200px;font-family:Verdana; font-size:small;text-align:left'>{0}</td>", dtPurchaseMedicine.Rows[0]["purdate"]);
            rpt.Append("<td style='width:150px;font-family:Verdana;font-weight:bold;font-size:small; text-align:left'></td>");
            rpt.Append("<td style='width: 250px;font-family:Verdana;font-weight:bold;font-size:small; text-align:left'></td>");
            rpt.Append("</tr >");

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


            /*rpt.Append("<tr style='height:20px'>");
            rpt.Append("<td style='width: 190px;font-family:Verdana;font-weight:bold;font-size:small; text-align:left'>Doctor Name :</td>");
            rpt.AppendFormat("<td style='width: 200px;font-family:Verdana; font-size:small;text-align:left'>{0}</td>", dtPurchaseMedicine.Rows[0]["DoctorName"]);
            rpt.Append("<td style='width:150px;font-family:Verdana;font-weight:bold;font-size:small; text-align:right'>Date :</td>");
            rpt.AppendFormat("<td style='width: 250px;font-family:Verdana;font-weight:bold;font-size:small; text-align:left'>{0}</td>",DateTime.Now.ToString("dd/MM/yyyy"));
            rpt.Append("</tr >");*/

     
            rpt.Append("</table >");

            rpt.Append("<table width='900px' cellspacing=0 border=0 style='border:1px solid black;border-spacing: 0; padding: 0;font-family:verdana;'>");
            rpt.Append("<tr style='height:20px'>");
            rpt.Append("<td colspan='6' style='width: 8%;border-bottom: 1px solid black; font-family:Verdana;font-weight:bold;background-color:#9B9C8D;font-size:larger; text-align:Center'>** Details **</td>");
            rpt.Append("</tr'>");

            rpt.Append("<tr style='height:25px'>");
            rpt.Append("<td style='font-family:Verdana;font-weight:bold;font-size:small; text-align:center'><u>Quantity</u></td>");
            rpt.Append("<td style='font-family:Verdana;font-weight:bold;font-size:small; text-align:center'><u>Item Description</u></td>");
            rpt.Append("<td style='font-family:Verdana;font-weight:bold;font-size:small; text-align:center'><u>Batch No</u></td>");
            rpt.Append("<td style='font-family:Verdana;font-weight:bold;font-size:small; text-align:center'><u>Expiry Date</u></td>");
            //rpt.Append("<td style='font-family:Verdana;font-weight:bold;font-size:small; text-align:center'><u>Unit Price</u></td>");
            //rpt.Append("<td style='font-family:Verdana;font-weight:bold;font-size:small; text-align:center'><u>Gross Amount</u></td>");
            rpt.Append("</tr >");

            for (int i = 0; i < dtPurchaseMedicine.Rows.Count; i++)
            {
                rpt.Append("<tr style='height:25px'>");
                rpt.AppendFormat("<td style='font-family:Verdana;font-size:small; text-align:center'>{0}</td>", dtPurchaseMedicine.Rows[i]["Qty"]);
                rpt.AppendFormat("<td style='font-family:Verdana;font-size:small; text-align:center'>{0}</td>", dtPurchaseMedicine.Rows[i]["MedicineName"]);
                rpt.AppendFormat("<td style='font-family:Verdana;font-size:small; text-align:center'>{0}</td>", dtPurchaseMedicine.Rows[i]["BatchNo"]);
                rpt.AppendFormat("<td style='font-family:Verdana;font-size:small; text-align:center'>{0}</td>", dtPurchaseMedicine.Rows[i]["exdate"]);
                //rpt.AppendFormat("<td style='font-family:Verdana;font-size:small; text-align:center'>{0}</td>", dtPurchaseMedicine.Rows[i]["PricePerUnit"]);
                //rpt.AppendFormat("<td style='font-family:Verdana;font-size:small;padding-right:150px; text-align:right'>{0}.00</td>", dtPurchaseMedicine.Rows[i]["TotalPrice"]);
                rpt.Append("</tr >");
            }
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


    public void GenerateCode()
    {
        DataTable dtsalecode = theHelper.GenerateSaleId(Session["CoCode"].ToString().Trim(), Session["YearCode"].ToString().Trim());
        TextBox5.Text = dtsalecode.Rows[0][0].ToString();
    }
    private void InsertPurchaseMedicine(string compcode, string yearcode, double total)
    
    {
        int i = 0;
        string reformattedDate1 = string.Empty;
        System.Globalization.DateTimeFormatInfo dtf = new DateTimeFormatInfo();
     
        DateTime testdate = DateTime.ParseExact(TextBox6.Text, "dd/MM/yyyy", dtf);
        DateTime rcvdate = DateTime.ParseExact(receivedt.Text, "dd/MM/yyyy", dtf);
        if (ddlMedi1.SelectedIndex > 0 && txtBatch1.Text.Length > 0 && Calendar2.Text.Length > 0 && txtQty1.Text.Length > 0 /*&& txtUnitPrice1.Text.Length > 0 && txtTotalPrice1.Text.Length > 0*/)
        {
            i++;
            DateTime testdate1 = DateTime.ParseExact(Calendar2.Text, "dd/MM/yyyy", dtf);
            theHelper.InsertSaleMedicine(i, compcode, yearcode, ddlMediSubGrp1.SelectedValue, ddlPatient.SelectedValue, ddlwing.SelectedValue, TextBox2.Text, TextBox4.Text, TextBox3.Text, TextBox5.Text, testdate.ToString("yyyy-MM-dd"), total.ToString(), TextBox7.Text, ddlMfg1.SelectedValue, ddlMediGrp1.SelectedValue, ddlMedi1.SelectedValue, txtBatch1.Text, testdate1.ToString("yyyy-MM-dd"), txtQty1.Text, txtUnitPrice1.Text, txtTotalPrice1.Text, Session["userName"].ToString(), rcvdate.ToString("yyyy-MM-dd"), receiveby.Text,issueby.Text);
        }
        if (ddlMedi2.SelectedIndex > 0 && txtBatch2.Text.Length > 0 && Calendar3.Text.Length > 0 && txtQty2.Text.Length > 0 /*&& txtUnitPrice2.Text.Length > 0 && txtTotalPrice2.Text.Length > 0*/)
        {
            i++;
            DateTime testdate2 = DateTime.ParseExact(Calendar3.Text, "dd/MM/yyyy", dtf);
            theHelper.InsertSaleMedicine(i, compcode, yearcode, ddlMediSubGrp2.SelectedValue, ddlPatient.SelectedValue, ddlwing.SelectedValue, TextBox2.Text, TextBox4.Text, TextBox3.Text, TextBox5.Text, testdate.ToString("yyyy-MM-dd"), total.ToString(), TextBox7.Text, ddlMfg2.SelectedValue, ddlMediGrp2.SelectedValue, ddlMedi2.SelectedValue, txtBatch2.Text, testdate2.ToString("yyyy-MM-dd"), txtQty2.Text, txtUnitPrice2.Text, txtTotalPrice2.Text, Session["userName"].ToString(), rcvdate.ToString("yyyy-MM-dd"), receiveby.Text, issueby.Text);
        }
        if (ddlMedi3.SelectedIndex > 0 && txtBatch3.Text.Length > 0 && Calendar4.Text.Length > 0 && txtQty3.Text.Length > 0 /*&& txtUnitPrice3.Text.Length > 0 && txtTotalPrice3.Text.Length > 0*/)
        {
            i++;
            DateTime testdate3 = DateTime.ParseExact(Calendar4.Text, "dd/MM/yyyy", dtf);
            theHelper.InsertSaleMedicine(i, compcode, yearcode, ddlMediSubGrp3.SelectedValue, ddlPatient.SelectedValue, ddlwing.SelectedValue, TextBox2.Text, TextBox4.Text, TextBox3.Text, TextBox5.Text, testdate.ToString("yyyy-MM-dd"), total.ToString(), TextBox7.Text, ddlMfg3.SelectedValue, ddlMediGrp3.SelectedValue, ddlMedi3.SelectedValue, txtBatch3.Text, testdate3.ToString("yyyy-MM-dd"), txtQty3.Text, txtUnitPrice3.Text, txtTotalPrice3.Text, Session["userName"].ToString(), rcvdate.ToString("yyyy-MM-dd"), receiveby.Text, issueby.Text);
        }
        if (ddlMedi4.SelectedIndex > 0 && txtBatch4.Text.Length > 0 && Calendar5.Text.Length > 0 && txtQty4.Text.Length > 0 /*&& txtUnitPrice4.Text.Length > 0 && txtTotalPrice4.Text.Length > 0*/)
        {
            i++;
            DateTime testdate4 = DateTime.ParseExact(Calendar5.Text, "dd/MM/yyyy", dtf);
            theHelper.InsertSaleMedicine(i, compcode, yearcode, ddlMediSubGrp4.SelectedValue, ddlPatient.SelectedValue, ddlwing.SelectedValue, TextBox2.Text, TextBox4.Text, TextBox3.Text, TextBox5.Text, testdate.ToString("yyyy-MM-dd"), total.ToString(), TextBox7.Text, ddlMfg4.SelectedValue, ddlMediGrp4.SelectedValue, ddlMedi4.SelectedValue, txtBatch4.Text, testdate4.ToString("yyyy-MM-dd"), txtQty4.Text, txtUnitPrice4.Text, txtTotalPrice4.Text, Session["userName"].ToString(), rcvdate.ToString("yyyy-MM-dd"), receiveby.Text, issueby.Text);
        }
        if (ddlMedi5.SelectedIndex > 0 && txtBatch5.Text.Length > 0 && Calendar6.Text.Length > 0 && txtQty5.Text.Length > 0 /*&& txtUnitPrice5.Text.Length > 0 && txtTotalPrice5.Text.Length > 0*/)
        {
            i++;
            DateTime testdate5 = DateTime.ParseExact(Calendar6.Text, "MM/dd/yyyy", dtf);
            theHelper.InsertSaleMedicine(i, compcode, yearcode, ddlMediSubGrp5.SelectedValue, ddlPatient.SelectedValue, ddlwing.SelectedValue, TextBox2.Text, TextBox4.Text, TextBox3.Text, TextBox5.Text, testdate.ToString("yyyy-MM-dd"), total.ToString(), TextBox7.Text, ddlMfg5.SelectedValue, ddlMediGrp5.SelectedValue, ddlMedi5.SelectedValue, txtBatch5.Text, testdate5.ToString("yyyy-MM-dd"), txtQty5.Text, txtUnitPrice5.Text, txtTotalPrice5.Text, Session["userName"].ToString(), rcvdate.ToString("yyyy-MM-dd"), receiveby.Text, issueby.Text);
        }
        if (ddlMedi6.SelectedIndex > 0 && txtBatch6.Text.Length > 0 && Calendar7.Text.Length > 0 && txtQty6.Text.Length > 0 /*&& txtUnitPrice6.Text.Length > 0 && txtTotalPrice6.Text.Length > 0*/)
        {
            i++;
            DateTime testdate6 = DateTime.ParseExact(Calendar7.Text, "dd/MM/yyyy", dtf);
            theHelper.InsertSaleMedicine(i, compcode, yearcode, ddlMediSubGrp6.SelectedValue, ddlPatient.SelectedValue, ddlwing.SelectedValue, TextBox2.Text, TextBox4.Text, TextBox3.Text, TextBox5.Text, testdate.ToString("yyyy-MM-dd"), total.ToString(), TextBox7.Text, ddlMfg6.SelectedValue, ddlMediGrp6.SelectedValue, ddlMedi6.SelectedValue, txtBatch6.Text, testdate6.ToString("yyyy-MM-dd"), txtQty6.Text, txtUnitPrice6.Text, txtTotalPrice6.Text, Session["userName"].ToString(), rcvdate.ToString("yyyy-MM-dd"), receiveby.Text, issueby.Text);
        }
        if (ddlMedi7.SelectedIndex > 0 && txtBatch7.Text.Length > 0 && Calendar8.Text.Length > 0 && txtQty7.Text.Length > 0 /*&& txtUnitPrice7.Text.Length > 0 && txtTotalPrice7.Text.Length > 0*/)
        {
            i++;
            DateTime testdate7 = DateTime.ParseExact(Calendar8.Text, "dd/MM/yyyy", dtf);
            theHelper.InsertSaleMedicine(i, compcode, yearcode, ddlMediSubGrp7.SelectedValue, ddlPatient.SelectedValue, ddlwing.SelectedValue, TextBox2.Text, TextBox4.Text, TextBox3.Text, TextBox5.Text, testdate.ToString("yyyy-MM-dd"), total.ToString(), TextBox7.Text, ddlMfg7.SelectedValue, ddlMediGrp7.SelectedValue, ddlMedi7.SelectedValue, txtBatch7.Text, testdate7.ToString("yyyy-MM-dd"), txtQty7.Text, txtUnitPrice7.Text, txtTotalPrice7.Text, Session["userName"].ToString(), rcvdate.ToString("yyyy-MM-dd"), receiveby.Text, issueby.Text);
        }
        if (ddlMedi8.SelectedIndex > 0 && txtBatch8.Text.Length > 0 && Calendar9.Text.Length > 0 && txtQty8.Text.Length > 0 /*&& txtUnitPrice8.Text.Length > 0 && txtTotalPrice8.Text.Length > 0*/)
        {
            i++;
            DateTime testdate8 = DateTime.ParseExact(Calendar9.Text, "dd/MM/yyyy", dtf);
            theHelper.InsertSaleMedicine(i, compcode, yearcode, ddlMediSubGrp8.SelectedValue, ddlPatient.SelectedValue, ddlwing.SelectedValue, TextBox2.Text, TextBox4.Text, TextBox3.Text, TextBox5.Text, testdate.ToString("yyyy-MM-dd"), total.ToString(), TextBox7.Text, ddlMfg8.SelectedValue, ddlMediGrp8.SelectedValue, ddlMedi8.SelectedValue, txtBatch8.Text, testdate8.ToString("yyyy-MM-dd"), txtQty8.Text, txtUnitPrice8.Text, txtTotalPrice8.Text, Session["userName"].ToString(), rcvdate.ToString("yyyy-MM-dd"), receiveby.Text, issueby.Text);
        }
        if (ddlMedi9.SelectedIndex > 0 && txtBatch9.Text.Length > 0 && Calendar10.Text.Length > 0 && txtQty9.Text.Length > 0 /*&& txtUnitPrice9.Text.Length > 0 && txtTotalPrice9.Text.Length > 0*/)
        {
            i++;
            DateTime testdate9 = DateTime.ParseExact(Calendar10.Text, "dd/MM/yyyy", dtf);
            theHelper.InsertSaleMedicine(i, compcode, yearcode, ddlMediSubGrp9.SelectedValue, ddlPatient.SelectedValue, ddlwing.SelectedValue, TextBox2.Text, TextBox4.Text, TextBox3.Text, TextBox5.Text, testdate.ToString("yyyy-MM-dd"), total.ToString(), TextBox7.Text, ddlMfg9.SelectedValue, ddlMediGrp9.SelectedValue, ddlMedi9.SelectedValue, txtBatch9.Text, testdate9.ToString("yyyy-MM-dd"), txtQty9.Text, txtUnitPrice9.Text, txtTotalPrice9.Text, Session["userName"].ToString(), rcvdate.ToString("yyyy-MM-dd"), receiveby.Text, issueby.Text);
        }
        if (ddlMedi10.SelectedIndex > 0 && txtBatch10.Text.Length > 0 && Calendar11.Text.Length > 0 && txtQty10.Text.Length > 0 /*&& txtUnitPrice10.Text.Length > 0 && txtTotalPrice10.Text.Length > 0*/)
        {
            i++;
            DateTime testdate10 = DateTime.ParseExact(Calendar11.Text, "dd/MM/yyyy", dtf);
            theHelper.InsertSaleMedicine(i, compcode, yearcode, ddlMediSubGrp10.SelectedValue, ddlPatient.SelectedValue, ddlwing.SelectedValue, TextBox2.Text, TextBox4.Text, TextBox3.Text, TextBox5.Text, testdate.ToString("yyyy-MM-dd"), total.ToString(), TextBox7.Text, ddlMfg10.SelectedValue, ddlMediGrp10.SelectedValue, ddlMedi10.SelectedValue, txtBatch10.Text, testdate10.ToString("yyyy-MM-dd"), txtQty10.Text, txtUnitPrice10.Text, txtTotalPrice10.Text, Session["userName"].ToString(), rcvdate.ToString("yyyy-MM-dd"), receiveby.Text, issueby.Text);
        }
        if (ddlMedi11.SelectedIndex > 0 && txtBatch11.Text.Length > 0 && Calendar12.Text.Length > 0 && txtQty11.Text.Length > 0 /*&& txtUnitPrice11.Text.Length > 0 && txtTotalPrice1.Text.Length > 0*/)
        {
            i++;
            DateTime testdate11 = DateTime.ParseExact(Calendar12.Text, "dd/MM/yyyy", dtf);
            theHelper.InsertSaleMedicine(i, compcode, yearcode, ddlMediSubGrp11.SelectedValue, ddlPatient.SelectedValue, ddlwing.SelectedValue, TextBox2.Text, TextBox4.Text, TextBox3.Text, TextBox5.Text, testdate.ToString("yyyy-MM-dd"), total.ToString(), TextBox7.Text, ddlMfg11.SelectedValue, ddlMediGrp11.SelectedValue, ddlMedi11.SelectedValue, txtBatch11.Text, testdate11.ToString("yyyy-MM-dd"), txtQty11.Text, txtUnitPrice11.Text, txtTotalPrice11.Text, Session["userName"].ToString(), rcvdate.ToString("yyyy-MM-dd"), receiveby.Text, issueby.Text);
        }
        if (ddlMedi12.SelectedIndex > 0 && txtBatch12.Text.Length > 0 && Calendar13.Text.Length > 0 && txtQty12.Text.Length > 0 /*&& txtUnitPrice12.Text.Length > 0 && txtTotalPrice12.Text.Length > 0*/)
        {
            i++;
            DateTime testdate12 = DateTime.ParseExact(Calendar13.Text, "dd/MM/yyyy", dtf);
            theHelper.InsertSaleMedicine(i, compcode, yearcode, ddlMediSubGrp12.SelectedValue, ddlPatient.SelectedValue, ddlwing.SelectedValue, TextBox2.Text, TextBox4.Text, TextBox3.Text, TextBox5.Text, testdate.ToString("yyyy-MM-dd"), total.ToString(), TextBox7.Text, ddlMfg12.SelectedValue, ddlMediGrp12.SelectedValue, ddlMedi12.SelectedValue, txtBatch12.Text, testdate12.ToString("yyyy-MM-dd"), txtQty12.Text, txtUnitPrice12.Text, txtTotalPrice12.Text, Session["userName"].ToString(), rcvdate.ToString("yyyy-MM-dd"), receiveby.Text, issueby.Text);
        }
        //theHelper.InsUpdInv(compcode, yearcode, TextBox5.Text.Trim(), "I", Session["userName"].ToString());
    }
    protected void ddlMediGrp1_SelectedIndexChanged(object sender, EventArgs e)
    {
        ddlMediSubGrp1.DataSource = theHelper.DropdownSubGroup(Session["CoCode"].ToString().Trim(),ddlMediGrp1.SelectedValue);
        ddlMediSubGrp1.DataTextField = "SubGrName";
        ddlMediSubGrp1.DataValueField = "ID";
        ddlMediSubGrp1.DataBind();
        ddlMediSubGrp1.Items.Insert(0, new ListItem("--Select--", "0")); 
    }
    protected void ddlMediGrp2_SelectedIndexChanged(object sender, EventArgs e)
    {
        ddlMediSubGrp2.DataSource = theHelper.DropdownSubGroup(Session["CoCode"].ToString().Trim(),ddlMediGrp2.SelectedValue);
        ddlMediSubGrp2.DataTextField = "SubGrName";
        ddlMediSubGrp2.DataValueField = "ID";
        ddlMediSubGrp2.DataBind();
        ddlMediSubGrp2.Items.Insert(0, new ListItem("--Select--", "0")); 
    }
    protected void ddlMediGrp3_SelectedIndexChanged(object sender, EventArgs e)
    {
        ddlMediSubGrp3.DataSource = theHelper.DropdownSubGroup(Session["CoCode"].ToString().Trim(),ddlMediGrp3.SelectedValue);
        ddlMediSubGrp3.DataTextField = "SubGrName";
        ddlMediSubGrp3.DataValueField = "ID";
        ddlMediSubGrp3.DataBind();
        ddlMediSubGrp3.Items.Insert(0, new ListItem("--Select--", "0"));
    }
    protected void ddlMediGrp4_SelectedIndexChanged(object sender, EventArgs e)
    {
        ddlMediSubGrp4.DataSource = theHelper.DropdownSubGroup(Session["CoCode"].ToString().Trim(),ddlMediGrp4.SelectedValue);
        ddlMediSubGrp4.DataTextField = "SubGrName";
        ddlMediSubGrp4.DataValueField = "ID";
        ddlMediSubGrp4.DataBind();
        ddlMediSubGrp4.Items.Insert(0, new ListItem("--Select--", "0"));  
    }
    protected void ddlMediGrp5_SelectedIndexChanged(object sender, EventArgs e)
    {
        ddlMediSubGrp5.DataSource = theHelper.DropdownSubGroup(Session["CoCode"].ToString().Trim(),ddlMediGrp5.SelectedValue);
        ddlMediSubGrp5.DataTextField = "SubGrName";
        ddlMediSubGrp5.DataValueField = "ID";
        ddlMediSubGrp5.DataBind();
        ddlMediSubGrp5.Items.Insert(0, new ListItem("--Select--", "0"));  
    }
    protected void ddlMediGrp6_SelectedIndexChanged(object sender, EventArgs e)
    {

        ddlMediSubGrp6.DataSource = theHelper.DropdownSubGroup(Session["CoCode"].ToString().Trim(),ddlMediGrp6.SelectedValue);
        ddlMediSubGrp6.DataTextField = "SubGrName";
        ddlMediSubGrp6.DataValueField = "ID";
        ddlMediSubGrp6.DataBind();
        ddlMediSubGrp6.Items.Insert(0, new ListItem("--Select--", "0"));  
    }
    protected void ddlMediGrp7_SelectedIndexChanged(object sender, EventArgs e)
    {
        ddlMediSubGrp7.DataSource = theHelper.DropdownSubGroup(Session["CoCode"].ToString().Trim(),ddlMediGrp7.SelectedValue);
        ddlMediSubGrp7.DataTextField = "SubGrName";
        ddlMediSubGrp7.DataValueField = "ID";
        ddlMediSubGrp7.DataBind();
        ddlMediSubGrp7.Items.Insert(0, new ListItem("--Select--", "0"));  
    }
    protected void ddlMediGrp8_SelectedIndexChanged(object sender, EventArgs e)
    {
        ddlMediSubGrp8.DataSource = theHelper.DropdownSubGroup(Session["CoCode"].ToString().Trim(),ddlMediGrp8.SelectedValue);
        ddlMediSubGrp8.DataTextField = "SubGrName";
        ddlMediSubGrp8.DataValueField = "ID";
        ddlMediSubGrp8.DataBind();
        ddlMediSubGrp8.Items.Insert(0, new ListItem("--Select--", "0")); 
    }
    protected void ddlMediGrp9_SelectedIndexChanged(object sender, EventArgs e)
    {

        ddlMediSubGrp9.DataSource = theHelper.DropdownSubGroup(Session["CoCode"].ToString().Trim(),ddlMediGrp9.SelectedValue);
        ddlMediSubGrp9.DataTextField = "SubGrName";
        ddlMediSubGrp9.DataValueField = "ID";
        ddlMediSubGrp9.DataBind();
        ddlMediSubGrp9.Items.Insert(0, new ListItem("--Select--", "0"));    
    }
    protected void ddlMediGrp10_SelectedIndexChanged(object sender, EventArgs e)
    {
        ddlMediSubGrp10.DataSource = theHelper.DropdownSubGroup(Session["CoCode"].ToString().Trim(),ddlMediGrp10.SelectedValue);
        ddlMediSubGrp10.DataTextField = "SubGrName";
        ddlMediSubGrp10.DataValueField = "ID";
        ddlMediSubGrp10.DataBind();
        ddlMediSubGrp10.Items.Insert(0, new ListItem("--Select--", "0"));  
    }
    protected void ddlMediGrp11_SelectedIndexChanged(object sender, EventArgs e)
    {
        ddlMediSubGrp11.DataSource = theHelper.DropdownSubGroup(Session["CoCode"].ToString().Trim(),ddlMediGrp11.SelectedValue);
        ddlMediSubGrp11.DataTextField = "SubGrName";
        ddlMediSubGrp11.DataValueField = "ID";
        ddlMediSubGrp11.DataBind();
        ddlMediSubGrp11.Items.Insert(0, new ListItem("--Select--", "0"));   
    }
    protected void ddlMediGrp12_SelectedIndexChanged(object sender, EventArgs e)
    {
        ddlMediSubGrp12.DataSource = theHelper.DropdownSubGroup(Session["CoCode"].ToString().Trim(),ddlMediGrp12.SelectedValue);
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
        ddlBatchNo.DataTextField = "BatchNo";
        ddlBatchNo.DataValueField = "BatchNo";
        ddlBatchNo.DataBind();

        //TextBox2.Text = dt.Rows[0]["DoseName"].ToString();
    }
    public void Expirydt(string mid, string mfgid,string Batchno, TextBox txtExpiry)
    {
        txtExpiry.Text = "";
        DataTable dt = theHelper.ExpiryDateBatchWise(Session["CoCode"].ToString().Trim(), Session["YearCode"].ToString().Trim(), mid, mfgid, Batchno);
        if (dt.Rows.Count > 0)
        {
            txtExpiry.Text = dt.Rows[0]["ExDate"].ToString();
        }
        else
        {
            txtExpiry.Text = "";
        }
    }
    protected void ddlMedi1_SelectedIndexChanged(object sender, EventArgs e)
    {
        MfgGrpSgrpFill(ddlMedi1.SelectedValue, 1);
        txtUnitPrice1.Text = theHelper.GetPurchasePricePerUnit(Session["CoCode"].ToString().Trim(), Session["YearCode"].ToString().Trim(),ddlMedi1.SelectedValue).ToString();
        BatchNoFill(ddlMedi1.SelectedValue, ddlMfg1.SelectedValue, txtBatch1);
        Expirydt(ddlMedi1.SelectedValue, ddlMfg1.SelectedValue, txtBatch1.SelectedValue, Calendar2);
        DataTable dt = theHelper.AvailableStock(Session["CoCode"].ToString().Trim(), Session["YearCode"].ToString().Trim(), ddlMedi1.SelectedValue, ddlMfg1.SelectedValue, txtBatch1.SelectedValue);
        if (dt.Rows.Count > 0)
        {
            txtStock1.Text = Convert.ToDecimal(dt.Rows[0]["AvailQty"]).ToString();
        }
    }
    protected void ddlMedi2_SelectedIndexChanged(object sender, EventArgs e)
    {
        MfgGrpSgrpFill(ddlMedi2.SelectedValue, 2);
        txtUnitPrice2.Text = theHelper.GetPurchasePricePerUnit(Session["CoCode"].ToString().Trim(), Session["YearCode"].ToString().Trim(),ddlMedi2.SelectedValue).ToString();
        BatchNoFill(ddlMedi2.SelectedValue, ddlMfg2.SelectedValue, txtBatch2);
        Expirydt(ddlMedi2.SelectedValue, ddlMfg2.SelectedValue, txtBatch2.SelectedValue, Calendar3);
        DataTable dt = theHelper.AvailableStock(Session["CoCode"].ToString().Trim(), Session["YearCode"].ToString().Trim(), ddlMedi2.SelectedValue, ddlMfg2.SelectedValue, txtBatch2.SelectedValue);
        if (dt.Rows.Count > 0)
        {
            txtStock2.Text = Convert.ToDecimal(dt.Rows[0]["AvailQty"]).ToString();
        }
    }
    protected void ddlMedi3_SelectedIndexChanged(object sender, EventArgs e)
    {
        MfgGrpSgrpFill(ddlMedi3.SelectedValue, 3);
        txtUnitPrice3.Text = theHelper.GetPurchasePricePerUnit(Session["CoCode"].ToString().Trim(), Session["YearCode"].ToString().Trim(),ddlMedi3.SelectedValue).ToString();
        BatchNoFill(ddlMedi3.SelectedValue, ddlMfg3.SelectedValue, txtBatch3);
        Expirydt(ddlMedi3.SelectedValue, ddlMfg3.SelectedValue, txtBatch3.SelectedValue, Calendar4);
        DataTable dt = theHelper.AvailableStock(Session["CoCode"].ToString().Trim(), Session["YearCode"].ToString().Trim(), ddlMedi3.SelectedValue, ddlMfg3.SelectedValue, txtBatch3.SelectedValue);
        if (dt.Rows.Count > 0)
        {
            txtStock3.Text = Convert.ToDecimal(dt.Rows[0]["AvailQty"]).ToString();
        }
    }
    protected void ddlMedi4_SelectedIndexChanged(object sender, EventArgs e)
    {
        MfgGrpSgrpFill(ddlMedi4.SelectedValue, 4);
        txtUnitPrice4.Text = theHelper.GetPurchasePricePerUnit(Session["CoCode"].ToString().Trim(), Session["YearCode"].ToString().Trim(), ddlMedi4.SelectedValue).ToString();
        BatchNoFill(ddlMedi4.SelectedValue, ddlMfg4.SelectedValue, txtBatch4);
        Expirydt(ddlMedi4.SelectedValue, ddlMfg4.SelectedValue, txtBatch4.SelectedValue, Calendar5);
        DataTable dt = theHelper.AvailableStock(Session["CoCode"].ToString().Trim(), Session["YearCode"].ToString().Trim(), ddlMedi4.SelectedValue, ddlMfg4.SelectedValue, txtBatch4.SelectedValue);
        if (dt.Rows.Count > 0)
        {
            txtStock4.Text = Convert.ToDecimal(dt.Rows[0]["AvailQty"]).ToString();
        }
    }
    protected void ddlMedi5_SelectedIndexChanged(object sender, EventArgs e)
    {
        MfgGrpSgrpFill(ddlMedi5.SelectedValue, 5);
        txtUnitPrice5.Text = theHelper.GetPurchasePricePerUnit(Session["CoCode"].ToString().Trim(), Session["YearCode"].ToString().Trim(),ddlMedi5.SelectedValue).ToString();
        BatchNoFill(ddlMedi5.SelectedValue, ddlMfg5.SelectedValue, txtBatch5);
        Expirydt(ddlMedi5.SelectedValue, ddlMfg5.SelectedValue, txtBatch5.SelectedValue, Calendar6);
        DataTable dt = theHelper.AvailableStock(Session["CoCode"].ToString().Trim(), Session["YearCode"].ToString().Trim(), ddlMedi5.SelectedValue, ddlMfg5.SelectedValue, txtBatch5.SelectedValue);
        if (dt.Rows.Count > 0)
        {
            txtStock5.Text = Convert.ToDecimal(dt.Rows[0]["AvailQty"]).ToString();
        }
    }
    protected void ddlMedi6_SelectedIndexChanged(object sender, EventArgs e)
    {
        MfgGrpSgrpFill(ddlMedi6.SelectedValue, 6);
        txtUnitPrice6.Text = theHelper.GetPurchasePricePerUnit(Session["CoCode"].ToString().Trim(), Session["YearCode"].ToString().Trim(),ddlMedi6.SelectedValue).ToString();
        BatchNoFill(ddlMedi6.SelectedValue, ddlMfg6.SelectedValue, txtBatch6);
        Expirydt(ddlMedi6.SelectedValue, ddlMfg6.SelectedValue, txtBatch6.SelectedValue, Calendar7);
        DataTable dt = theHelper.AvailableStock(Session["CoCode"].ToString().Trim(), Session["YearCode"].ToString().Trim(), ddlMedi6.SelectedValue, ddlMfg6.SelectedValue, txtBatch6.SelectedValue);
        if (dt.Rows.Count > 0)
        {
            txtStock6.Text = Convert.ToDecimal(dt.Rows[0]["AvailQty"]).ToString();
        }
    }
    protected void ddlMedi7_SelectedIndexChanged(object sender, EventArgs e)
    {
        MfgGrpSgrpFill(ddlMedi7.SelectedValue, 7);
        txtUnitPrice7.Text = theHelper.GetPurchasePricePerUnit(Session["CoCode"].ToString().Trim(), Session["YearCode"].ToString().Trim(),ddlMedi7.SelectedValue).ToString();
        BatchNoFill(ddlMedi7.SelectedValue, ddlMfg7.SelectedValue, txtBatch7);
        Expirydt(ddlMedi7.SelectedValue, ddlMfg7.SelectedValue, txtBatch7.SelectedValue, Calendar8);
        DataTable dt = theHelper.AvailableStock(Session["CoCode"].ToString().Trim(), Session["YearCode"].ToString().Trim(), ddlMedi7.SelectedValue, ddlMfg7.SelectedValue, txtBatch7.SelectedValue);
        if (dt.Rows.Count > 0)
        {
            txtStock7.Text = Convert.ToDecimal(dt.Rows[0]["AvailQty"]).ToString();
        }
    }
    protected void ddlMedi8_SelectedIndexChanged(object sender, EventArgs e)
    {
        MfgGrpSgrpFill(ddlMedi8.SelectedValue, 8);
        txtUnitPrice8.Text = theHelper.GetPurchasePricePerUnit(Session["CoCode"].ToString().Trim(), Session["YearCode"].ToString().Trim(),ddlMedi8.SelectedValue).ToString();
        BatchNoFill(ddlMedi8.SelectedValue, ddlMfg8.SelectedValue, txtBatch8);
        Expirydt(ddlMedi8.SelectedValue, ddlMfg8.SelectedValue, txtBatch8.SelectedValue, Calendar9);
        DataTable dt = theHelper.AvailableStock(Session["CoCode"].ToString().Trim(), Session["YearCode"].ToString().Trim(), ddlMedi8.SelectedValue, ddlMfg8.SelectedValue, txtBatch8.SelectedValue);
        if (dt.Rows.Count > 0)
        {
            txtStock8.Text = Convert.ToDecimal(dt.Rows[0]["AvailQty"]).ToString();
        }
    }
    protected void ddlMedi9_SelectedIndexChanged(object sender, EventArgs e)
    {
        MfgGrpSgrpFill(ddlMedi9.SelectedValue, 9);
        txtUnitPrice9.Text = theHelper.GetPurchasePricePerUnit(Session["CoCode"].ToString().Trim(), Session["YearCode"].ToString().Trim(),ddlMedi9.SelectedValue).ToString();
        BatchNoFill(ddlMedi9.SelectedValue, ddlMfg9.SelectedValue, txtBatch9);
        Expirydt(ddlMedi9.SelectedValue, ddlMfg9.SelectedValue, txtBatch9.SelectedValue, Calendar10);
        DataTable dt = theHelper.AvailableStock(Session["CoCode"].ToString().Trim(), Session["YearCode"].ToString().Trim(), ddlMedi9.SelectedValue, ddlMfg9.SelectedValue, txtBatch9.SelectedValue);
        if (dt.Rows.Count > 0)
        {
            txtStock9.Text = Convert.ToDecimal(dt.Rows[0]["AvailQty"]).ToString();
        }
    }
    protected void ddlMedi10_SelectedIndexChanged(object sender, EventArgs e)
    {
        MfgGrpSgrpFill(ddlMedi10.SelectedValue, 10);
        txtUnitPrice10.Text = theHelper.GetPurchasePricePerUnit(Session["CoCode"].ToString().Trim(), Session["YearCode"].ToString().Trim(),ddlMedi10.SelectedValue).ToString();
        BatchNoFill(ddlMedi10.SelectedValue, ddlMfg10.SelectedValue, txtBatch10);
        Expirydt(ddlMedi10.SelectedValue, ddlMfg10.SelectedValue, txtBatch10.SelectedValue, Calendar11);
        DataTable dt = theHelper.AvailableStock(Session["CoCode"].ToString().Trim(), Session["YearCode"].ToString().Trim(), ddlMedi10.SelectedValue, ddlMfg10.SelectedValue, txtBatch10.SelectedValue);
        if (dt.Rows.Count > 0)
        {
            txtStock10.Text = Convert.ToDecimal(dt.Rows[0]["AvailQty"]).ToString();
        }
    }
    protected void ddlMedi11_SelectedIndexChanged(object sender, EventArgs e)
    {
        MfgGrpSgrpFill(ddlMedi11.SelectedValue, 11);
        txtUnitPrice11.Text = theHelper.GetPurchasePricePerUnit(Session["CoCode"].ToString().Trim(), Session["YearCode"].ToString().Trim(),ddlMedi11.SelectedValue).ToString();
        BatchNoFill(ddlMedi11.SelectedValue, ddlMfg11.SelectedValue, txtBatch11);
        Expirydt(ddlMedi11.SelectedValue, ddlMfg11.SelectedValue, txtBatch11.SelectedValue, Calendar12);
        DataTable dt = theHelper.AvailableStock(Session["CoCode"].ToString().Trim(), Session["YearCode"].ToString().Trim(), ddlMedi11.SelectedValue, ddlMfg11.SelectedValue, txtBatch11.SelectedValue);
        if (dt.Rows.Count > 0)
        {
            txtStock11.Text = Convert.ToDecimal(dt.Rows[0]["AvailQty"]).ToString();
        }
    }
    protected void ddlMedi12_SelectedIndexChanged(object sender, EventArgs e)
    {
        MfgGrpSgrpFill(ddlMedi12.SelectedValue, 12);
        txtUnitPrice12.Text = theHelper.GetPurchasePricePerUnit(Session["CoCode"].ToString().Trim(), Session["YearCode"].ToString().Trim(),ddlMedi12.SelectedValue).ToString();
        BatchNoFill(ddlMedi12.SelectedValue, ddlMfg12.SelectedValue, txtBatch12);
        Expirydt(ddlMedi12.SelectedValue, ddlMfg12.SelectedValue, txtBatch12.SelectedValue, Calendar13);
        DataTable dt = theHelper.AvailableStock(Session["CoCode"].ToString().Trim(), Session["YearCode"].ToString().Trim(), ddlMedi12.SelectedValue, ddlMfg12.SelectedValue, txtBatch12.SelectedValue);
        if (dt.Rows.Count > 0)
        {
            txtStock12.Text = Convert.ToDecimal(dt.Rows[0]["AvailQty"]).ToString();
        }
    }



    protected void txtQty1_TextChanged(object sender, EventArgs e)
    {
        if (validation(ddlMedi1.SelectedValue, ddlMfg1.SelectedValue, txtBatch1.SelectedValue, txtQty1) == true)
        {
            if (txtQty1.Text != "")
            {
                double totalPrice = Convert.ToDouble(txtUnitPrice1.Text) * Convert.ToDouble(txtQty1.Text);
                txtTotalPrice1.Text = totalPrice.ToString();
            }
            else
            {
                txtTotalPrice1.Text = 0.ToString();
            }
            txtQty1.BorderColor = System.Drawing.Color.White;
        }
        else
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "msg", "alert('Not Enough Stock Balance!');", true);
            txtQty1.Text = "";
            txtQty1.BorderColor = System.Drawing.Color.Red;
        }
    }
    protected void txtQty2_TextChanged(object sender, EventArgs e)
    {
        if (validation(ddlMedi2.SelectedValue, ddlMfg2.SelectedValue, txtBatch2.SelectedValue, txtQty2) == true)
        {
            if (txtQty2.Text != "")
            {
                double totalPrice = Convert.ToDouble(txtUnitPrice2.Text) * Convert.ToDouble(txtQty2.Text);
                txtTotalPrice2.Text = totalPrice.ToString();
            }
            else
            {
                txtTotalPrice2.Text = 0.ToString();
            }
            txtQty2.BorderColor = System.Drawing.Color.White;
        }
        else
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "msg", "alert('Not Enough Stock Balance!');", true);
            txtQty2.Text = "";
            txtQty2.BorderColor = System.Drawing.Color.Red;
        }
    }
    protected void txtQty3_TextChanged(object sender, EventArgs e)
    {
        if (txtQty3.Text != "")
        {
            double totalPrice = Convert.ToDouble(txtUnitPrice3.Text) * Convert.ToDouble(txtQty3.Text);
            txtTotalPrice3.Text = totalPrice.ToString();
        }
        else
        {
            txtTotalPrice3.Text = 0.ToString();
        }
    }
    protected void txtQty4_TextChanged(object sender, EventArgs e)
    {
        if (txtQty4.Text != "")
        {
            double totalPrice = Convert.ToDouble(txtUnitPrice4.Text) * Convert.ToDouble(txtQty4.Text);
            txtTotalPrice4.Text = totalPrice.ToString();
        }
        else
        {
            txtTotalPrice4.Text = 0.ToString();
        }
    }
    protected void txtQty5_TextChanged(object sender, EventArgs e)
    {
        if (txtQty5.Text != "")
        {
            double totalPrice = Convert.ToDouble(txtUnitPrice5.Text) * Convert.ToDouble(txtQty5.Text);
            txtTotalPrice5.Text = totalPrice.ToString();
        }
        else
        {
            txtTotalPrice5.Text = 0.ToString();
        }
    }
    protected void txtQty6_TextChanged(object sender, EventArgs e)
    {
        if (txtQty6.Text != "")
        {
            double totalPrice = Convert.ToDouble(txtUnitPrice6.Text) * Convert.ToDouble(txtQty6.Text);
            txtTotalPrice6.Text = totalPrice.ToString();
        }
        else
        {
            txtTotalPrice6.Text = 0.ToString();
        }
    }
    protected void txtQty7_TextChanged(object sender, EventArgs e)
    {
        if (txtQty7.Text != "")
        {
            double totalPrice = Convert.ToDouble(txtUnitPrice7.Text) * Convert.ToDouble(txtQty7.Text);
            txtTotalPrice7.Text = totalPrice.ToString();
        }
        else
        {
            txtTotalPrice7.Text = 0.ToString();
        }
    }
    protected void txtQty8_TextChanged(object sender, EventArgs e)
    {
        if (txtQty8.Text != "")
        {
            double totalPrice = Convert.ToDouble(txtUnitPrice8.Text) * Convert.ToDouble(txtQty8.Text);
            txtTotalPrice8.Text = totalPrice.ToString();
        }
        else
        {
            txtTotalPrice8.Text = 0.ToString();
        }
    }
    protected void txtQty9_TextChanged(object sender, EventArgs e)
    {
        if (txtQty9.Text != "")
        {
            double totalPrice = Convert.ToDouble(txtUnitPrice9.Text) * Convert.ToDouble(txtQty9.Text);
            txtTotalPrice9.Text = totalPrice.ToString();
        }
        else
        {
            txtTotalPrice9.Text = 0.ToString();
        }
    }
    protected void txtQty10_TextChanged(object sender, EventArgs e)
    {
        if (txtQty10.Text != "")
        {
            double totalPrice = Convert.ToDouble(txtUnitPrice10.Text) * Convert.ToDouble(txtQty10.Text);
            txtTotalPrice10.Text = totalPrice.ToString();
        }
        else
        {
            txtTotalPrice10.Text = 0.ToString();
        }
    }
    protected void txtQty11_TextChanged(object sender, EventArgs e)
    {
        if (txtQty11.Text != "")
        {
            double totalPrice = Convert.ToDouble(txtUnitPrice11.Text) * Convert.ToDouble(txtQty11.Text);
            txtTotalPrice11.Text = totalPrice.ToString();
        }
        else
        {
            txtTotalPrice11.Text = 0.ToString();
        }
    }
    protected void txtQty12_TextChanged(object sender, EventArgs e)
    {
        if (txtQty12.Text != "")
        {
            double totalPrice = Convert.ToDouble(txtUnitPrice12.Text) * Convert.ToDouble(txtQty12.Text);
            txtTotalPrice12.Text = totalPrice.ToString();
        }
        else
        {
            txtTotalPrice12.Text = 0.ToString();
        }
    }


    private void PageDataBind()
    {
        try
        {
            DropDownFill();
            TextBox t2, t3, t4, t5; DropDownList t1, d1, d2, d3, d4; HiddenField h1;
            DataTable dtPurchaseMedicine = theHelper.GetSaleMedicineDetails(Session["CoCode"].ToString().Trim(), Session["YearCode"].ToString().Trim(), TextBox5.Text);
            if (dtPurchaseMedicine.Rows.Count > 0)
            {
               
                TextBox6.Text = dtPurchaseMedicine.Rows[0]["purdate"].ToString();
                ddlPatient.SelectedValue = dtPurchaseMedicine.Rows[0]["Patient_Name"].ToString().Trim();
                ddlFloor.Items.Clear();
                this.ddlFloor.DataSource = theHelper.getFloor(Session["CoCode"].ToString().Trim());
                this.ddlFloor.DataTextField = "FloorName";
                this.ddlFloor.DataValueField = "FloorId";
                this.ddlFloor.DataBind();
                this.ddlFloor.Items.Insert(0, new ListItem("--Select--", "0"));
                ddlFloor.SelectedValue = dtPurchaseMedicine.Rows[0]["FloorId"].ToString().Trim();
                ddlwing.Items.Clear();
                if (dtPurchaseMedicine.Rows[0]["FloorId"].ToString() != "")
                {
                    ddlwing.DataSource = theHelper.getWorkStation(Session["CoCode"].ToString().Trim(),dtPurchaseMedicine.Rows[0]["FloorId"].ToString());
                    ddlwing.DataTextField = "WorkStation";
                    ddlwing.DataValueField = "WingsID";
                    ddlwing.DataBind();

                    this.ddlwing.Items.Insert(0, new ListItem("--Select--", "0"));
                    ddlwing.SelectedValue = dtPurchaseMedicine.Rows[0]["Wings_ID"].ToString();
                }
                receivedt.Text = dtPurchaseMedicine.Rows[0]["Rcvdate"].ToString();
                
                receiveby.Text = dtPurchaseMedicine.Rows[0]["ReceiveBy"].ToString();
                issueby.Text = dtPurchaseMedicine.Rows[0]["Issueby"].ToString();
                //TextBox4.Text = dtPurchaseMedicine.Rows[0]["CardNo"].ToString();
                for (int i = 0, t = 1; i < dtPurchaseMedicine.Rows.Count; i++, t++)
                {
                    t1 = (DropDownList)Page.FindControl("ctl00$ContentPlaceHolder1$txtBatch" + t.ToString());
                    t2 = (TextBox)Page.FindControl("ctl00$ContentPlaceHolder1$Calendar" + (t + 1).ToString());
                    t3 = (TextBox)Page.FindControl("ctl00$ContentPlaceHolder1$txtQty" + t.ToString());
                    h1 = (HiddenField)Page.FindControl("ctl00$ContentPlaceHolder1$qty" + t.ToString());
                    t4 = (TextBox)Page.FindControl("ctl00$ContentPlaceHolder1$txtUnitPrice" + t.ToString());
                    t5 = (TextBox)Page.FindControl("ctl00$ContentPlaceHolder1$txtTotalPrice" + t.ToString());
                    d1 = (DropDownList)Page.FindControl("ctl00$ContentPlaceHolder1$ddlMfg" + t.ToString());
                    d2 = (DropDownList)Page.FindControl("ctl00$ContentPlaceHolder1$ddlMediGrp" + t.ToString());
                    d3 = (DropDownList)Page.FindControl("ctl00$ContentPlaceHolder1$ddlMedi" + t.ToString());
                    d4 = (DropDownList)Page.FindControl("ctl00$ContentPlaceHolder1$ddlMediSubGrp" + t.ToString());
                    //d1.SelectedValue = dtPurchaseMedicine.Rows[i]["MCode"].ToString();
                    //d2.SelectedValue = dtPurchaseMedicine.Rows[i]["MedicineGroupID"].ToString();
                    //DdlMedicineSubGroupBind("0", d4);
                    //d4.SelectedValue = dtPurchaseMedicine.Rows[i]["MedicineSubGrp"].ToString();
                   // DdlMedicineIDBind(d2.SelectedValue,d4.SelectedValue, d3, d1);
                    DdlIssueMedicineIDBind(d3);
                    //d3.SelectedValue = dtPurchaseMedicine.Rows[i]["MedicineID"].ToString();
                    d3.SelectedValue = dtPurchaseMedicine.Rows[i]["icode"].ToString().Trim();


                    // BatchNoFill(d3.SelectedValue, d1.SelectedValue,t1);
                    DdlBatchBind("0", dtPurchaseMedicine.Rows[i]["icode"].ToString(), TextBox5.Text, t1);

                    t1.SelectedValue = dtPurchaseMedicine.Rows[i]["BatchNo"].ToString();
                    t2.Text = dtPurchaseMedicine.Rows[i]["exdate"].ToString();
                    t3.Text = dtPurchaseMedicine.Rows[i]["Qty"].ToString();
                    h1.Value = dtPurchaseMedicine.Rows[i]["Qty"].ToString();
                    t4.Text = dtPurchaseMedicine.Rows[i]["PricePerUnit"].ToString();
                    t5.Text = dtPurchaseMedicine.Rows[i]["TotalPrice"].ToString();


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
                }
                Button1.Text = "Update";
                if (Objmain.checkaccss(Session["CoCode"].ToString(), Session["UserRoleID"].ToString().Trim(), "MEDICINE SALES/ISSUE", checkAccessType.UpdateAction) == false)
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
        ddlMedicineSubID.DataSource = theHelper.DropdownSubGroup(Session["CoCode"].ToString().Trim(),medicalGrp);
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
    /*protected void ddlMediSubGrp1_SelectedIndexChanged(object sender, EventArgs e)
    {
        ddlMedi1.Items.Clear();
        ddlMedi1.DataSource = theHelper.DropdownID4(Session["CoCode"].ToString().Trim(), Session["YearCode"].ToString().Trim(),ddlMediGrp1.SelectedValue, ddlMediSubGrp1.SelectedValue, ddlMfg1.SelectedValue);
        ddlMedi1.DataTextField = "MedicineName";
        ddlMedi1.DataValueField = "MedicineID";
        ddlMedi1.DataBind();
        this.ddlMedi1.Items.Insert(0, new ListItem("--Select--", "0"));
    }
    protected void ddlMediSubGrp2_SelectedIndexChanged(object sender, EventArgs e)
    {
        ddlMedi2.Items.Clear();
        ddlMedi2.DataSource = theHelper.DropdownID4(Session["CoCode"].ToString().Trim(), Session["YearCode"].ToString().Trim(),ddlMediGrp2.SelectedValue, ddlMediSubGrp2.SelectedValue, ddlMfg2.SelectedValue);
        ddlMedi2.DataTextField = "MedicineName";
        ddlMedi2.DataValueField = "MedicineID";
        ddlMedi2.DataBind();
        this.ddlMedi2.Items.Insert(0, new ListItem("--Select--", "0"));
    }
    protected void ddlMediSubGrp3_SelectedIndexChanged(object sender, EventArgs e)
    {
        ddlMedi3.Items.Clear();
        ddlMedi3.DataSource = theHelper.DropdownID4(Session["CoCode"].ToString().Trim(), Session["YearCode"].ToString().Trim(),ddlMediGrp3.SelectedValue, ddlMediSubGrp3.SelectedValue, ddlMfg3.SelectedValue);
        ddlMedi3.DataTextField = "MedicineName";
        ddlMedi3.DataValueField = "MedicineID";
        ddlMedi3.DataBind();
        this.ddlMedi3.Items.Insert(0, new ListItem("--Select--", "0"));
    }
    protected void ddlMediSubGrp4_SelectedIndexChanged(object sender, EventArgs e)
    {
        ddlMedi4.Items.Clear();
        ddlMedi4.DataSource = theHelper.DropdownID4(Session["CoCode"].ToString().Trim(), Session["YearCode"].ToString().Trim(),ddlMediGrp4.SelectedValue, ddlMediSubGrp4.SelectedValue, ddlMfg4.SelectedValue);
        ddlMedi4.DataTextField = "MedicineName";
        ddlMedi4.DataValueField = "MedicineID";
        ddlMedi4.DataBind();
        this.ddlMedi4.Items.Insert(0, new ListItem("--Select--", "0"));
    }
    protected void ddlMediSubGrp5_SelectedIndexChanged(object sender, EventArgs e)
    {
        ddlMedi5.Items.Clear();
        ddlMedi5.DataSource = theHelper.DropdownID4(Session["CoCode"].ToString().Trim(), Session["YearCode"].ToString().Trim(),ddlMediGrp5.SelectedValue, ddlMediSubGrp5.SelectedValue, ddlMfg5.SelectedValue);
        ddlMedi5.DataTextField = "MedicineName";
        ddlMedi5.DataValueField = "MedicineID";
        ddlMedi5.DataBind();
        this.ddlMedi5.Items.Insert(0, new ListItem("--Select--", "0"));
    }
    protected void ddlMediSubGrp6_SelectedIndexChanged(object sender, EventArgs e)
    {
        ddlMedi6.Items.Clear();
        ddlMedi6.DataSource = theHelper.DropdownID4(Session["CoCode"].ToString().Trim(), Session["YearCode"].ToString().Trim(),ddlMediGrp6.SelectedValue, ddlMediSubGrp6.SelectedValue, ddlMfg6.SelectedValue);
        ddlMedi6.DataTextField = "MedicineName";
        ddlMedi6.DataValueField = "MedicineID";
        ddlMedi6.DataBind();
        this.ddlMedi6.Items.Insert(0, new ListItem("--Select--", "0"));
    }
    protected void ddlMediSubGrp7_SelectedIndexChanged(object sender, EventArgs e)
    {
        ddlMedi7.Items.Clear();
        ddlMedi7.DataSource = theHelper.DropdownID4(Session["CoCode"].ToString().Trim(), Session["YearCode"].ToString().Trim(),ddlMediGrp7.SelectedValue, ddlMediSubGrp7.SelectedValue, ddlMfg7.SelectedValue);
        ddlMedi7.DataTextField = "MedicineName";
        ddlMedi7.DataValueField = "MedicineID";
        ddlMedi7.DataBind();
        this.ddlMedi7.Items.Insert(0, new ListItem("--Select--", "0"));
    }
    protected void ddlMediSubGrp8_SelectedIndexChanged(object sender, EventArgs e)
    {
        ddlMedi8.Items.Clear();
        ddlMedi8.DataSource = theHelper.DropdownID4(Session["CoCode"].ToString().Trim(), Session["YearCode"].ToString().Trim(),ddlMediGrp8.SelectedValue, ddlMediSubGrp8.SelectedValue, ddlMfg8.SelectedValue);
        ddlMedi8.DataTextField = "MedicineName";
        ddlMedi8.DataValueField = "MedicineID";
        ddlMedi8.DataBind();
        this.ddlMedi8.Items.Insert(0, new ListItem("--Select--", "0"));
    }
    protected void ddlMediSubGrp9_SelectedIndexChanged(object sender, EventArgs e)
    {
        ddlMedi9.Items.Clear();
        ddlMedi9.DataSource = theHelper.DropdownID4(Session["CoCode"].ToString().Trim(), Session["YearCode"].ToString().Trim(),ddlMediGrp9.SelectedValue, ddlMediSubGrp9.SelectedValue, ddlMfg9.SelectedValue);
        ddlMedi9.DataTextField = "MedicineName";
        ddlMedi9.DataValueField = "MedicineID";
        ddlMedi9.DataBind();
        this.ddlMedi9.Items.Insert(0, new ListItem("--Select--", "0"));
    }
    protected void ddlMediSubGrp10_SelectedIndexChanged(object sender, EventArgs e)
    {
        ddlMedi10.Items.Clear();
        ddlMedi10.DataSource = theHelper.DropdownID4(Session["CoCode"].ToString().Trim(), Session["YearCode"].ToString().Trim(),ddlMediGrp10.SelectedValue, ddlMediSubGrp10.SelectedValue, ddlMfg10.SelectedValue);
        ddlMedi10.DataTextField = "MedicineName";
        ddlMedi10.DataValueField = "MedicineID";
        ddlMedi10.DataBind();
        this.ddlMedi10.Items.Insert(0, new ListItem("--Select--", "0"));

    }
    protected void ddlMediSubGrp11_SelectedIndexChanged(object sender, EventArgs e)
    {
        ddlMedi11.Items.Clear();
        ddlMedi11.DataSource = theHelper.DropdownID4(Session["CoCode"].ToString().Trim(), Session["YearCode"].ToString().Trim(),ddlMediGrp11.SelectedValue, ddlMediSubGrp11.SelectedValue, ddlMfg12.SelectedValue);
        ddlMedi11.DataTextField = "MedicineName";
        ddlMedi11.DataValueField = "MedicineID";
        ddlMedi11.DataBind();
        this.ddlMedi11.Items.Insert(0, new ListItem("--Select--", "0"));
    }
    protected void ddlMediSubGrp12_SelectedIndexChanged(object sender, EventArgs e)
    {
        ddlMedi12.Items.Clear();
        ddlMedi12.DataSource = theHelper.DropdownID4(Session["CoCode"].ToString().Trim(), Session["YearCode"].ToString().Trim(),ddlMediGrp12.SelectedValue, ddlMediSubGrp12.SelectedValue, ddlMfg12.SelectedValue);
        ddlMedi12.DataTextField = "MedicineName";
        ddlMedi12.DataValueField = "MedicineID";
        ddlMedi12.DataBind();
        this.ddlMedi12.Items.Insert(0, new ListItem("--Select--", "0"));
    }*/
    protected void txtBatch1_SelectedIndexChanged(object sender, EventArgs e)
    {
        Expirydt(ddlMedi1.SelectedValue, ddlMfg1.SelectedValue, txtBatch1.SelectedValue, Calendar2);
        DataTable dt = theHelper.AvailableStock(Session["CoCode"].ToString().Trim(), Session["YearCode"].ToString().Trim(),ddlMedi1.SelectedValue, ddlMfg1.SelectedValue, txtBatch1.SelectedValue);
        txtStock1.Text = Convert.ToDecimal(dt.Rows[0]["AvailQty"]).ToString();
    }
    protected void txtBatch2_SelectedIndexChanged(object sender, EventArgs e)
    {
        Expirydt(ddlMedi2.SelectedValue, ddlMfg2.SelectedValue, txtBatch2.SelectedValue, Calendar3);
        DataTable dt = theHelper.AvailableStock(Session["CoCode"].ToString().Trim(), Session["YearCode"].ToString().Trim(), ddlMedi2.SelectedValue, ddlMfg2.SelectedValue, txtBatch2.SelectedValue);
        txtStock2.Text = Convert.ToDecimal(dt.Rows[0]["AvailQty"]).ToString();
    }
    protected void txtBatch3_SelectedIndexChanged(object sender, EventArgs e)
    {
        Expirydt(ddlMedi3.SelectedValue, ddlMfg3.SelectedValue, txtBatch3.SelectedValue, Calendar4);
        DataTable dt = theHelper.AvailableStock(Session["CoCode"].ToString().Trim(), Session["YearCode"].ToString().Trim(), ddlMedi3.SelectedValue, ddlMfg3.SelectedValue, txtBatch3.SelectedValue);
        txtStock3.Text = Convert.ToDecimal(dt.Rows[0]["AvailQty"]).ToString();
    }
    protected void txtBatch4_SelectedIndexChanged(object sender, EventArgs e)
    {
        Expirydt(ddlMedi4.SelectedValue, ddlMfg4.SelectedValue, txtBatch4.SelectedValue, Calendar5);
        DataTable dt = theHelper.AvailableStock(Session["CoCode"].ToString().Trim(), Session["YearCode"].ToString().Trim(), ddlMedi4.SelectedValue, ddlMfg4.SelectedValue, txtBatch4.SelectedValue);
        txtStock4.Text = Convert.ToDecimal(dt.Rows[0]["AvailQty"]).ToString();
    }
    protected void txtBatch5_SelectedIndexChanged(object sender, EventArgs e)
    {
        Expirydt(ddlMedi5.SelectedValue, ddlMfg5.SelectedValue, txtBatch5.SelectedValue, Calendar6);
        DataTable dt = theHelper.AvailableStock(Session["CoCode"].ToString().Trim(), Session["YearCode"].ToString().Trim(), ddlMedi5.SelectedValue, ddlMfg5.SelectedValue, txtBatch5.SelectedValue);
        txtStock5.Text = Convert.ToDecimal(dt.Rows[0]["AvailQty"]).ToString();
    }
    protected void txtBatch6_SelectedIndexChanged(object sender, EventArgs e)
    {
        Expirydt(ddlMedi6.SelectedValue, ddlMfg6.SelectedValue, txtBatch6.SelectedValue, Calendar7);
        DataTable dt = theHelper.AvailableStock(Session["CoCode"].ToString().Trim(), Session["YearCode"].ToString().Trim(), ddlMedi6.SelectedValue, ddlMfg6.SelectedValue, txtBatch6.SelectedValue);
        txtStock6.Text = Convert.ToDecimal(dt.Rows[0]["AvailQty"]).ToString();
    }
    protected void txtBatch7_SelectedIndexChanged(object sender, EventArgs e)
    {
        Expirydt(ddlMedi7.SelectedValue, ddlMfg7.SelectedValue, txtBatch7.SelectedValue, Calendar8);
        DataTable dt = theHelper.AvailableStock(Session["CoCode"].ToString().Trim(), Session["YearCode"].ToString().Trim(), ddlMedi7.SelectedValue, ddlMfg7.SelectedValue, txtBatch7.SelectedValue);
        txtStock7.Text = Convert.ToDecimal(dt.Rows[0]["AvailQty"]).ToString();
    }
    protected void txtBatch8_SelectedIndexChanged(object sender, EventArgs e)
    {
        Expirydt(ddlMedi8.SelectedValue, ddlMfg8.SelectedValue, txtBatch8.SelectedValue, Calendar9);
        DataTable dt = theHelper.AvailableStock(Session["CoCode"].ToString().Trim(), Session["YearCode"].ToString().Trim(), ddlMedi8.SelectedValue, ddlMfg8.SelectedValue, txtBatch8.SelectedValue);
        txtStock8.Text = Convert.ToDecimal(dt.Rows[0]["AvailQty"]).ToString();
    }
    protected void txtBatch9_SelectedIndexChanged(object sender, EventArgs e)
    {
        Expirydt(ddlMedi9.SelectedValue, ddlMfg9.SelectedValue, txtBatch9.SelectedValue, Calendar10);
        DataTable dt = theHelper.AvailableStock(Session["CoCode"].ToString().Trim(), Session["YearCode"].ToString().Trim(), ddlMedi9.SelectedValue, ddlMfg9.SelectedValue, txtBatch9.SelectedValue);
        txtStock9.Text = Convert.ToDecimal(dt.Rows[0]["AvailQty"]).ToString();
    }
    protected void txtBatch10_SelectedIndexChanged(object sender, EventArgs e)
    {
        Expirydt(ddlMedi10.SelectedValue, ddlMfg10.SelectedValue, txtBatch10.SelectedValue, Calendar11);
        DataTable dt = theHelper.AvailableStock(Session["CoCode"].ToString().Trim(), Session["YearCode"].ToString().Trim(), ddlMedi10.SelectedValue, ddlMfg10.SelectedValue, txtBatch10.SelectedValue);
        txtStock10.Text = Convert.ToDecimal(dt.Rows[0]["AvailQty"]).ToString();
    }
    protected void txtBatch11_SelectedIndexChanged(object sender, EventArgs e)
    {
        Expirydt(ddlMedi11.SelectedValue, ddlMfg11.SelectedValue, txtBatch11.SelectedValue, Calendar12);
        DataTable dt = theHelper.AvailableStock(Session["CoCode"].ToString().Trim(), Session["YearCode"].ToString().Trim(), ddlMedi11.SelectedValue, ddlMfg11.SelectedValue, txtBatch11.SelectedValue);
        txtStock11.Text = Convert.ToDecimal(dt.Rows[0]["AvailQty"]).ToString();
    }
    protected void txtBatch12_SelectedIndexChanged(object sender, EventArgs e)
    {
        Expirydt(ddlMedi12.SelectedValue, ddlMfg12.SelectedValue, txtBatch12.SelectedValue, Calendar13);
        DataTable dt = theHelper.AvailableStock(Session["CoCode"].ToString().Trim(), Session["YearCode"].ToString().Trim(), ddlMedi12.SelectedValue, ddlMfg12.SelectedValue, txtBatch12.SelectedValue);
        txtStock12.Text = Convert.ToDecimal(dt.Rows[0]["AvailQty"]).ToString();
    }
    protected void ddlfloor_selectedIndexChange(object sender, EventArgs e)
    {
        ddlwing.Items.Clear();
        if (ddlFloor.SelectedValue != "")
        {
            this.ddlwing.DataSource = theHelper.getWorkStation(Session["CoCode"].ToString().Trim(), ddlFloor.SelectedValue);
            this.ddlwing.DataTextField = "WorkStation";
            this.ddlwing.DataValueField = "WingsID";
            this.ddlwing.DataBind();
        }
        this.ddlwing.Items.Insert(0, new ListItem("--Select--", "0"));
    }

    private void MfgGrpSgrpFill(string MedCode, int i)
    {
        DropDownList ddlMfg = (DropDownList)Page.FindControl("ctl00$ContentPlaceHolder1$ddlMfg" + i.ToString());
        DropDownList ddlMediGrp = (DropDownList)Page.FindControl("ctl00$ContentPlaceHolder1$ddlMediGrp" + i.ToString());
        DropDownList ddlMediSubGrp = (DropDownList)Page.FindControl("ctl00$ContentPlaceHolder1$ddlMediSubGrp" + i.ToString());

        if (MedCode == "0" || theHelper.MedicineExist(Session["CoCode"].ToString().Trim(),  MedCode).ToString() == "0")
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
}