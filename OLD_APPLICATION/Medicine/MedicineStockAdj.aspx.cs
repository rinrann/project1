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
using System.Collections.Generic;
 
public partial class Medicine_MedicineStockAdj : System.Web.UI.Page
{
    Main Objmain = new Main(ConfigurationSettings.AppSettings["ConnectionString"].ToString());
    MD_Adjustment theHelper = new MD_Adjustment(ConfigurationSettings.AppSettings["ConnectionString"].ToString());
    MD_PurchaseMedicine thepurHelper = new MD_PurchaseMedicine(ConfigurationSettings.AppSettings["ConnectionString"].ToString());
    protected void Page_Load(object sender, EventArgs e)
    {
        Page.Title = "Medicine Stock Adjustment";
        if (Session["userName"] == null)
        {
            Response.Redirect("../LoginPage.aspx");
        }
        if (Objmain.checkaccss(Session["CoCode"].ToString(), Session["UserRoleID"].ToString().Trim(), "MEDICINE/REAGENT STOCK ADJUSTMENT", checkAccessType.ViewAction) == false)
        {
            Response.Redirect("../AccessDenied.aspx");
        }

        if (Objmain.checkaccss(Session["CoCode"].ToString(), Session["UserRoleID"].ToString().Trim(), "MEDICINE/REAGENT STOCK ADJUSTMENT", checkAccessType.InsertAction) == false)
        {
            Button1.Enabled = false;
        }
        if (!IsPostBack)
        {
            DropDownList2.Items.Insert(0, new ListItem("Medicine", "M"));
            DropDownList2.Items.Insert(1, new ListItem("Reagent", "G"));
            DropDownFill();
            GenerateCode();
        }
    }

    public void GenerateCode()
    {
        DataTable dt = theHelper.GetAdjustmentID(Session["CoCode"].ToString().Trim(), Session["YearCode"].ToString().Trim());
        txtPurchaseMedicineId.Text = dt.Rows[0][0].ToString();
    }
    private void PageDataBind()
    {
        DropDownFill();
        TextBox t1, t2, t3, t4, t5,t6; DropDownList d1, d2, d3,d4,d5,d6;
        DataTable dtPurchaseMedicine = theHelper.GetPurchaseMedicineDetails(Session["CoCode"].ToString().Trim(), Session["YearCode"].ToString().Trim(),txtPurchaseMedicineId.Text);
        if (dtPurchaseMedicine.Rows.Count > 0)
        {
            DropDownList1.SelectedValue = dtPurchaseMedicine.Rows[0]["SLCODE"].ToString().Trim();
            DropDownList2.SelectedValue = dtPurchaseMedicine.Rows[0]["itype"].ToString().Trim();
            DropDownList1.Enabled = false;
            Calendar1.Text = dtPurchaseMedicine.Rows[0]["purdate"].ToString();
            txtReason.Text = dtPurchaseMedicine.Rows[0]["REMARKS"].ToString();
            //TextBox1.Text = dtPurchaseMedicine.Rows[0]["Discount"].ToString();
            //txtlessper.Text = dtPurchaseMedicine.Rows[0]["LessPercent"].ToString();
            //txtTaxper.Text = dtPurchaseMedicine.Rows[0]["TaxPercent"].ToString();
            for (int i = 0, t = 1; i < dtPurchaseMedicine.Rows.Count; i++, t++)
            {
               // t1 = (TextBox)Page.FindControl("ctl00$ContentPlaceHolder1$txtBatch" + t.ToString());
                t1 = (TextBox)Page.FindControl("ctl00$ContentPlaceHolder1$Calendar" + (t + 1).ToString());
                t2= (TextBox)Page.FindControl("ctl00$ContentPlaceHolder1$txtQty" + t.ToString());
                t3 = (TextBox)Page.FindControl("ctl00$ContentPlaceHolder1$txtUnitPrice" + t.ToString());
                t4 = (TextBox)Page.FindControl("ctl00$ContentPlaceHolder1$txtTotalPrice" + t.ToString());
                t5 = (TextBox)Page.FindControl("ctl00$ContentPlaceHolder1$txttrendDisc" + t.ToString());
                t6 = (TextBox)Page.FindControl("ctl00$ContentPlaceHolder1$txtstax" + t.ToString());


                d1 = (DropDownList)Page.FindControl("ctl00$ContentPlaceHolder1$ddlMedi" + t.ToString());
                d2 = (DropDownList)Page.FindControl("ctl00$ContentPlaceHolder1$ddlMfg" + t.ToString());
                d3 = (DropDownList)Page.FindControl("ctl00$ContentPlaceHolder1$ddlMediGrp" + t.ToString());
                
                d4 = (DropDownList)Page.FindControl("ctl00$ContentPlaceHolder1$ddlMediSubGrp" + t.ToString());
                d5 = (DropDownList)Page.FindControl("ctl00$ContentPlaceHolder1$ddlBatch" + t.ToString());
                d6 = (DropDownList)Page.FindControl("ctl00$ContentPlaceHolder1$ddlAddLess" + t.ToString());

               /* d1.SelectedValue = dtPurchaseMedicine.Rows[i]["MCode"].ToString();

                MedicineGroupFill(dtPurchaseMedicine.Rows[i]["MCode"].ToString(), t);
                d2.SelectedValue = dtPurchaseMedicine.Rows[i]["MedicineGroupID"].ToString();

                DdlMedicineSubGroupBind(dtPurchaseMedicine.Rows[i]["MedicineGroupID"].ToString(), d4);
                d4.SelectedValue = dtPurchaseMedicine.Rows[i]["MedicineSubGrp"].ToString();*/


                DdlMedicineIDBind(d1);
                d1.SelectedValue = dtPurchaseMedicine.Rows[i]["MedicineID"].ToString();
                d1.Enabled = true;

                /*if (dtPurchaseMedicine.Rows[i]["MCode"].ToString() != "")
                {
                    DdlIDMfgFill(dtPurchaseMedicine.Rows[i]["MCode"].ToString(), d2);
                    d2.SelectedValue = dtPurchaseMedicine.Rows[i]["MCode"].ToString();
                }
                else
                {*/
                    d2.Items.Clear();
                    d2.Items.Insert(0, new ListItem("--Select--", "0"));
                //}

                /*if (dtPurchaseMedicine.Rows[i]["MedicineGroupID"].ToString() != "")
                {
                    MedicineGroupFill(dtPurchaseMedicine.Rows[i]["MedicineGroupID"].ToString(), d3);
                    d3.SelectedValue = dtPurchaseMedicine.Rows[i]["MedicineGroupID"].ToString();
                }
                else
                {
                    d3.Items.Clear();
                    d3.Items.Insert(0, new ListItem("--Select--", "0"));
                }

                if (dtPurchaseMedicine.Rows[i]["MedicineSubGrp"].ToString() != "")
                {
                    DdlMedicineSubGroupBind(dtPurchaseMedicine.Rows[i]["MedicineSubGrp"].ToString(), d4);
                    d4.SelectedValue = dtPurchaseMedicine.Rows[i]["MedicineSubGrp"].ToString();
                }
                else
                {
                    d4.Items.Clear();
                    d4.Items.Insert(0, new ListItem("--Select--", "0"));
                }
                d4.Enabled = true;*/

                /*DdlMedicineIDBind(dtPurchaseMedicine.Rows[i]["MedicineGroupID"].ToString(),dtPurchaseMedicine.Rows[i]["MedicineSubGrp"].ToString(), d3);*/
               // d3.SelectedValue = dtPurchaseMedicine.Rows[i]["MedicineID"].ToString();

                DdlBatchBind("",dtPurchaseMedicine.Rows[i]["MedicineID"].ToString(), d5);
                d5.SelectedValue = dtPurchaseMedicine.Rows[i]["batchno"].ToString().Trim();
                d6.SelectedValue = dtPurchaseMedicine.Rows[i]["ITRCD"].ToString();
                //t1.Text = dtPurchaseMedicine.Rows[i]["BatchNo"].ToString();
                t1.Text = dtPurchaseMedicine.Rows[i]["exdate"].ToString();
                t2.Text = dtPurchaseMedicine.Rows[i]["IQTY"].ToString();
                t3.Text = dtPurchaseMedicine.Rows[i]["IRATE"].ToString();
                t4.Text = dtPurchaseMedicine.Rows[i]["IAMOUNT"].ToString();
                //t5.Text = dtPurchaseMedicine.Rows[i]["TrendDiscount"].ToString();
                //t6.Text = dtPurchaseMedicine.Rows[i]["Stax"].ToString();


            }
            Button1.Text = "Update";
            if (Objmain.checkaccss(Session["CoCode"].ToString(), Session["UserRoleID"].ToString().Trim(), "MEDICINE STOCK ADJUSTMENT", checkAccessType.UpdateAction) == false)
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
            txtPurchaseMedicineId.Text = theHelper.GetAdjustmentID(Session["CoCode"].ToString().Trim(), Session["YearCode"].ToString().Trim()).ToString();
            Button1.Text = "SUBMIT";
            DropDownList1.Enabled = true;
        }
    }
    private void ResetAllFields()
    {
        hdnMode.Value = "0";
        txtPurchaseMedicineId.Text = theHelper.GetAdjustmentID(Session["CoCode"].ToString().Trim(), Session["YearCode"].ToString().Trim()).ToString();
        Button1.Text = "SUBMIT";
        DropDownList1.Enabled = true;
        DropDownList1.SelectedIndex = 0;
        Calendar1.Text = string.Empty;
        txtReason.Text = string.Empty;
        TextBox1.Text = "";
        for (int i = 1; i <= 12; i++)
        {
            DropDownList ddlMfg = (DropDownList)divContent.FindControl("ddlMfg" + i.ToString());
            ddlMfg.Items.Clear();
            ddlMfg.Items.Insert(0, new ListItem("--Select--", "0"));

            DropDownList ddlMediGrp = (DropDownList)divContent.FindControl("ddlMediGrp" + i.ToString());
            ddlMediGrp.Items.Clear();
            ddlMediGrp.Items.Insert(0, new ListItem("--Select--", "0"));

            DropDownList ddlMediSubGrp = (DropDownList)divContent.FindControl("ddlMediSubGrp" + i.ToString());
            ddlMediSubGrp.Items.Clear();
            ddlMediSubGrp.Items.Insert(0, new ListItem("--Select--", "0"));


            DropDownList ddlMedi = (DropDownList)divContent.FindControl("ddlMedi" + i.ToString());
            ddlMedi.SelectedIndex = 0;
            DropDownList ddlBatch = (DropDownList)divContent.FindControl("ddlBatch" + i.ToString());
            ddlBatch.Items.Clear();
            ddlBatch.Items.Insert(0, new ListItem("--Select--", "0"));
            TextBox Calendar = (TextBox)divContent.FindControl("Calendar" + i.ToString());
            Calendar.Text = string.Empty;
            TextBox txtQty = (TextBox)divContent.FindControl("txtQty" + i.ToString());
            txtQty.Text = string.Empty;
            TextBox txtUnitPrice = (TextBox)divContent.FindControl("txtUnitPrice" + i.ToString());
            txtUnitPrice.Text = string.Empty;
            TextBox txtTotalPrice = (TextBox)divContent.FindControl("txtTotalPrice" + i.ToString());
            txtTotalPrice.Text = string.Empty;
            TextBox txttrendDisc = (TextBox)divContent.FindControl("txttrendDisc" + i.ToString());
            txttrendDisc.Text = string.Empty;
            TextBox txtstax = (TextBox)divContent.FindControl("txtstax" + i.ToString());
            txtstax.Text = string.Empty;
        }
        GenerateCode();
    }
    private void DropDownFill()
    {
        for (int i = 1; i <= 12; i++)
        {
            DropDownList ddlmfg = (DropDownList)Page.FindControl("ctl00$ContentPlaceHolder1$ddlMfg" + i.ToString());
            ddlmfg.Items.Clear();
            ddlmfg.Items.Insert(0, new ListItem("--Select--", "0"));

            DropDownList ddlgrp = (DropDownList)Page.FindControl("ctl00$ContentPlaceHolder1$ddlMediGrp" + i.ToString());
            ddlgrp.Items.Insert(0, new ListItem("--Select--", "0"));

            DropDownList ddlSub = (DropDownList)Page.FindControl("ctl00$ContentPlaceHolder1$ddlMediSubGrp" + i.ToString());
            ddlSub.Items.Insert(0, new ListItem("--Select--", "0"));

            DropDownList ddlAddLess = (DropDownList)Page.FindControl("ctl00$ContentPlaceHolder1$ddlAddLess" + i.ToString());
            //ddlAddLess.Items.Insert(0, new ListItem("--Select--", "0"));
            ddlAddLess.Items.Insert(0, new ListItem("Add", "I"));
            ddlAddLess.Items.Insert(1, new ListItem("Less", "O"));

            string type = DropDownList2.SelectedValue.ToString();
            DropDownList ddlMedicine = (DropDownList)Page.FindControl("ctl00$ContentPlaceHolder1$ddlMedi" + i.ToString());
            ddlMedicine.Items.Clear();
            ddlMedicine.DataSource = theHelper.DropdownMedicine(Session["CoCode"].ToString().Trim(), type);
            ddlMedicine.DataTextField = "MedicineName";
            ddlMedicine.DataValueField = "MedicineID";
            ddlMedicine.DataBind();
            ddlMedicine.Items.Insert(0, new ListItem("--Select--", "0"));

           
        }

        DropDownList1.Items.Clear();
        this.DropDownList1.DataSource = thepurHelper.DropdownID5(Session["CoCode"].ToString().Trim());
        this.DropDownList1.DataTextField = "SName";
        this.DropDownList1.DataValueField = "SCode";
        this.DropDownList1.DataBind();
        this.DropDownList1.Items.Insert(0, new ListItem("--Select--", "0"));

       /* ddlMfg1.Items.Clear();
        this.ddlMfg1.DataSource = theHelper.DropdownID2();
        this.ddlMfg1.DataTextField = "MName";
        this.ddlMfg1.DataValueField = "MCode";
        this.ddlMfg1.DataBind();
        this.ddlMfg1.Items.Insert(0, new ListItem("--Select--", "0"));

        ddlMfg2.Items.Clear();
        this.ddlMfg2.DataSource = theHelper.DropdownID2();
        this.ddlMfg2.DataTextField = "MName";
        this.ddlMfg2.DataValueField = "MCode";
        this.ddlMfg2.DataBind();
        this.ddlMfg2.Items.Insert(0, new ListItem("--Select--", "0"));


        ddlMfg3.Items.Clear();
        this.ddlMfg3.DataSource = theHelper.DropdownID2();
        this.ddlMfg3.DataTextField = "MName";
        this.ddlMfg3.DataValueField = "MCode";
        this.ddlMfg3.DataBind();
        this.ddlMfg3.Items.Insert(0, new ListItem("--Select--", "0"));

        ddlMfg4.Items.Clear();
        this.ddlMfg4.DataSource = theHelper.DropdownID2();
        this.ddlMfg4.DataTextField = "MName";
        this.ddlMfg4.DataValueField = "MCode";
        this.ddlMfg4.DataBind();
        this.ddlMfg4.Items.Insert(0, new ListItem("--Select--", "0"));

        ddlMfg5.Items.Clear();
        this.ddlMfg5.DataSource = theHelper.DropdownID2();
        this.ddlMfg5.DataTextField = "MName";
        this.ddlMfg5.DataValueField = "MCode";
        this.ddlMfg5.DataBind();
        this.ddlMfg5.Items.Insert(0, new ListItem("--Select--", "0"));

        ddlMfg6.Items.Clear();
        this.ddlMfg6.DataSource = theHelper.DropdownID2();
        this.ddlMfg6.DataTextField = "MName";
        this.ddlMfg6.DataValueField = "MCode";
        this.ddlMfg6.DataBind();
        this.ddlMfg6.Items.Insert(0, new ListItem("--Select--", "0"));

        ddlMfg7.Items.Clear();
        this.ddlMfg7.DataSource = theHelper.DropdownID2();
        this.ddlMfg7.DataTextField = "MName";
        this.ddlMfg7.DataValueField = "MCode";
        this.ddlMfg7.DataBind();
        this.ddlMfg7.Items.Insert(0, new ListItem("--Select--", "0"));

        ddlMfg8.Items.Clear();
        this.ddlMfg8.DataSource = theHelper.DropdownID2();
        this.ddlMfg8.DataTextField = "MName";
        this.ddlMfg8.DataValueField = "MCode";
        this.ddlMfg8.DataBind();
        this.ddlMfg8.Items.Insert(0, new ListItem("--Select--", "0"));

        ddlMfg9.Items.Clear();
        this.ddlMfg9.DataSource = theHelper.DropdownID2();
        this.ddlMfg9.DataTextField = "MName";
        this.ddlMfg9.DataValueField = "MCode";
        this.ddlMfg9.DataBind();
        this.ddlMfg9.Items.Insert(0, new ListItem("--Select--", "0"));

        ddlMfg10.Items.Clear();
        this.ddlMfg10.DataSource = theHelper.DropdownID2();
        this.ddlMfg10.DataTextField = "MName";
        this.ddlMfg10.DataValueField = "MCode";
        this.ddlMfg10.DataBind();
        this.ddlMfg10.Items.Insert(0, new ListItem("--Select--", "0"));

        ddlMfg11.Items.Clear();
        this.ddlMfg11.DataSource = theHelper.DropdownID2();
        this.ddlMfg11.DataTextField = "MName";
        this.ddlMfg11.DataValueField = "MCode";
        this.ddlMfg11.DataBind();
        this.ddlMfg11.Items.Insert(0, new ListItem("--Select--", "0"));

        ddlMfg12.Items.Clear();
        this.ddlMfg12.DataSource = theHelper.DropdownID2();
        this.ddlMfg12.DataTextField = "MName";
        this.ddlMfg12.DataValueField = "MCode";
        this.ddlMfg12.DataBind();
        this.ddlMfg12.Items.Insert(0, new ListItem("--Select--", "0"));


        ddlMediGrp1.Items.Clear();
        this.ddlMediGrp1.DataSource = theHelper.DropdownID3();
        this.ddlMediGrp1.DataTextField = "MedicineGroupName";
        this.ddlMediGrp1.DataValueField = "MedicineGroupID";
        this.ddlMediGrp1.DataBind();
        this.ddlMediGrp1.Items.Insert(0, new ListItem("--Select--", "0"));

        ddlMediGrp2.Items.Clear();
        this.ddlMediGrp2.DataSource = theHelper.DropdownID3();
        this.ddlMediGrp2.DataTextField = "MedicineGroupName";
        this.ddlMediGrp2.DataValueField = "MedicineGroupID";
        this.ddlMediGrp2.DataBind();
        this.ddlMediGrp2.Items.Insert(0, new ListItem("--Select--", "0"));

        ddlMediGrp3.Items.Clear();
        this.ddlMediGrp3.DataSource = theHelper.DropdownID3();
        this.ddlMediGrp3.DataTextField = "MedicineGroupName";
        this.ddlMediGrp3.DataValueField = "MedicineGroupID";
        this.ddlMediGrp3.DataBind();
        this.ddlMediGrp3.Items.Insert(0, new ListItem("--Select--", "0"));

        ddlMediGrp4.Items.Clear();
        this.ddlMediGrp4.DataSource = theHelper.DropdownID3();
        this.ddlMediGrp4.DataTextField = "MedicineGroupName";
        this.ddlMediGrp4.DataValueField = "MedicineGroupID";
        this.ddlMediGrp4.DataBind();
        this.ddlMediGrp4.Items.Insert(0, new ListItem("--Select--", "0"));

        ddlMediGrp5.Items.Clear();
        this.ddlMediGrp5.DataSource = theHelper.DropdownID3();
        this.ddlMediGrp5.DataTextField = "MedicineGroupName";
        this.ddlMediGrp5.DataValueField = "MedicineGroupID";
        this.ddlMediGrp5.DataBind();
        this.ddlMediGrp5.Items.Insert(0, new ListItem("--Select--", "0"));

        ddlMediGrp6.Items.Clear();
        this.ddlMediGrp6.DataSource = theHelper.DropdownID3();
        this.ddlMediGrp6.DataTextField = "MedicineGroupName";
        this.ddlMediGrp6.DataValueField = "MedicineGroupID";
        this.ddlMediGrp6.DataBind();
        this.ddlMediGrp6.Items.Insert(0, new ListItem("--Select--", "0"));

        ddlMediGrp7.Items.Clear();
        this.ddlMediGrp7.DataSource = theHelper.DropdownID3();
        this.ddlMediGrp7.DataTextField = "MedicineGroupName";
        this.ddlMediGrp7.DataValueField = "MedicineGroupID";
        this.ddlMediGrp7.DataBind();
        this.ddlMediGrp7.Items.Insert(0, new ListItem("--Select--", "0"));

        ddlMediGrp8.Items.Clear();
        this.ddlMediGrp8.DataSource = theHelper.DropdownID3();
        this.ddlMediGrp8.DataTextField = "MedicineGroupName";
        this.ddlMediGrp8.DataValueField = "MedicineGroupID";
        this.ddlMediGrp8.DataBind();
        this.ddlMediGrp8.Items.Insert(0, new ListItem("--Select--", "0"));

        ddlMediGrp9.Items.Clear();
        this.ddlMediGrp9.DataSource = theHelper.DropdownID3();
        this.ddlMediGrp9.DataTextField = "MedicineGroupName";
        this.ddlMediGrp9.DataValueField = "MedicineGroupID";
        this.ddlMediGrp9.DataBind();
        this.ddlMediGrp9.Items.Insert(0, new ListItem("--Select--", "0"));

        ddlMediGrp10.Items.Clear();
        this.ddlMediGrp10.DataSource = theHelper.DropdownID3();
        this.ddlMediGrp10.DataTextField = "MedicineGroupName";
        this.ddlMediGrp10.DataValueField = "MedicineGroupID";
        this.ddlMediGrp10.DataBind();
        this.ddlMediGrp10.Items.Insert(0, new ListItem("--Select--", "0"));

        ddlMediGrp11.Items.Clear();
        this.ddlMediGrp11.DataSource = theHelper.DropdownID3();
        this.ddlMediGrp11.DataTextField = "MedicineGroupName";
        this.ddlMediGrp11.DataValueField = "MedicineGroupID";
        this.ddlMediGrp11.DataBind();
        this.ddlMediGrp11.Items.Insert(0, new ListItem("--Select--", "0"));

        ddlMediGrp12.Items.Clear();
        this.ddlMediGrp12.DataSource = theHelper.DropdownID3();
        this.ddlMediGrp12.DataTextField = "MedicineGroupName";
        this.ddlMediGrp12.DataValueField = "MedicineGroupID";
        this.ddlMediGrp12.DataBind();
        this.ddlMediGrp12.Items.Insert(0, new ListItem("--Select--", "0"));*/

    }

    private void DdlMedicineIDBind(DropDownList ddlMedicineID)
    {
        string type = DropDownList2.SelectedValue.ToString();
        ddlMedicineID.Items.Clear();
        ddlMedicineID.DataSource = thepurHelper.DropdownMedicine(Session["CoCode"].ToString().Trim(),type);
        ddlMedicineID.DataTextField = "MedicineName";
        ddlMedicineID.DataValueField = "MedicineID";
        ddlMedicineID.DataBind();
        ddlMedicineID.Items.Insert(0, new ListItem("--Select--", "0"));
    }

    private void DdlIDMfgFill(string Mcode, DropDownList ddlmfg)
    {
        ddlmfg.Items.Clear();
        ddlmfg.DataSource = thepurHelper.GetMedMfgByMCode(Session["CoCode"].ToString().Trim(),Mcode);
        ddlmfg.DataTextField = "MName";
        ddlmfg.DataValueField = "MCode";
        ddlmfg.DataBind();
        ddlmfg.Items.Insert(0, new ListItem("--Select--", "0"));
    }

    private void DdlMedicineSubGroupBind(string medSubGrp, DropDownList ddlMedicineSubID)
    {
        ddlMedicineSubID.Items.Clear();
        ddlMedicineSubID.DataSource = thepurHelper.GetMedSubGrpByID(Session["CoCode"].ToString().Trim(),medSubGrp);
        ddlMedicineSubID.DataTextField = "SubGrName";
        ddlMedicineSubID.DataValueField = "ID";
        ddlMedicineSubID.DataBind();
        ddlMedicineSubID.Items.Insert(0, new ListItem("--Select--", "0"));
    }

    private void DdlBatchBind(string mfg,string medicine, DropDownList ddlBath)
    {
        ddlBath.Items.Clear();
        ddlBath.DataSource = theHelper.getBatchId(Session["CoCode"].ToString().Trim(), Session["YearCode"].ToString().Trim(), mfg, medicine, DropDownList1.SelectedValue);
        ddlBath.DataTextField = "BatchNo";
        ddlBath.DataValueField = "BatchNo";
        ddlBath.DataBind();
        ddlBath.Items.Insert(0, new ListItem("--Select--", "0"));
    }
    protected void Button1_Click(object sender, EventArgs e)
    {
        TextBox t1;
        string compcode = Session["CoCode"].ToString().Trim();
        string yearcode = Session["YearCode"].ToString().Trim();
        double total = 0.0;
        for (int i = 1; i <= 12; i++)
        {
            t1 = (TextBox)Page.FindControl("ctl00$ContentPlaceHolder1$txtTotalPrice" + i.ToString());
            if (t1.Text != "")
                total = total + Convert.ToDouble(t1.Text);
        }
        try
        {
            if (Button1.Text == "Submit")
            {

                InsertPurchaseMedicine(compcode, yearcode, Convert.ToDouble(total));
                ScriptManager.RegisterStartupScript(this, this.GetType(), "msg", "alert('Inserted  Successfully !');", true);

            }
            else
            {
                System.Globalization.DateTimeFormatInfo dtf = new DateTimeFormatInfo();
                DateTime testdate = DateTime.ParseExact(Calendar1.Text, "dd/MM/yyyy", dtf);
                theHelper.UpdatePurchaseMedicine(compcode, yearcode, txtPurchaseMedicineId.Text, DropDownList1.SelectedValue, testdate.ToString(), txtReason.Text, total.ToString(), TextBox1.Text, txtlessper.Text, txtTaxper.Text);
                theHelper.DeleteMEdDtls(compcode, yearcode, txtPurchaseMedicineId.Text);
                InsertPurchaseMedicine(compcode, yearcode, Convert.ToDouble(total));
                ScriptManager.RegisterStartupScript(this, this.GetType(), "msg", "alert('Updated  Successfully !');", true);
            }
            //PageDataBind();
        }
        catch (Exception ex)
        {
            lblError.ForeColor = System.Drawing.Color.Red;
            lblError.Text = "Error in saving..";
        }
        ResetAllFields();
    }
    protected void Button2_Click(object sender, EventArgs e)
    {
        ResetAllFields();
    }




    private void InsertPurchaseMedicine(string compcode,string yearcode,double total)
    {

        string reformattedDate1 = string.Empty;
        System.Globalization.DateTimeFormatInfo dtf = new DateTimeFormatInfo();
        int i = 0;
        DateTime testdate = DateTime.ParseExact(Calendar1.Text, "dd/MM/yyyy", dtf);

        if (ddlMedi1.SelectedIndex > 0 && ddlBatch1.SelectedIndex> 0 && Calendar2.Text.Length > 0 && txtQty1.Text.Length > 0 && txtUnitPrice1.Text.Length > 0 && txtTotalPrice1.Text.Length > 0)
        {
            i++;
            DateTime testdate1 = DateTime.ParseExact(Calendar2.Text, "dd/MM/yyyy", dtf);
            theHelper.InsertPurchaseMedicine(i,compcode, yearcode, ddlMediSubGrp1.SelectedValue, TextBox1.Text, total.ToString(), txtPurchaseMedicineId.Text, DropDownList1.SelectedValue, testdate.ToString("yyyy-MM-dd"), txtReason.Text, ddlMfg1.SelectedValue, ddlMediGrp1.SelectedValue, ddlMedi1.SelectedValue, ddlBatch1.SelectedValue, testdate1.ToString("yyyy-MM-dd"), txtQty1.Text,ddlAddLess1.SelectedValue, txtUnitPrice1.Text, txtTotalPrice1.Text, Session["userName"].ToString(), txttrendDisc1.Text, txtstax1.Text, txtlessper.Text, txtTaxper.Text);
        }
        if (ddlMedi2.SelectedIndex > 0 && ddlBatch2.SelectedIndex > 0 && Calendar3.Text.Length > 0 && txtQty2.Text.Length > 0 && txtUnitPrice2.Text.Length > 0 && txtTotalPrice2.Text.Length > 0)
        {
            i++;
            DateTime testdate2 = DateTime.ParseExact(Calendar3.Text, "dd/MM/yyyy", dtf);
            theHelper.InsertPurchaseMedicine(i, compcode, yearcode, ddlMediSubGrp2.SelectedValue, TextBox1.Text, total.ToString(), txtPurchaseMedicineId.Text, DropDownList1.SelectedValue, testdate.ToString("yyyy-MM-dd"), txtReason.Text, ddlMfg2.SelectedValue, ddlMediGrp2.SelectedValue, ddlMedi2.SelectedValue, ddlBatch2.SelectedValue, testdate2.ToString("yyyy-MM-dd"), txtQty2.Text, ddlAddLess2.SelectedValue, txtUnitPrice2.Text, txtTotalPrice2.Text, Session["userName"].ToString(), txttrendDisc2.Text, txtstax2.Text, txtlessper.Text, txtTaxper.Text);
        }
        if (ddlMedi3.SelectedIndex > 0 && ddlBatch3.SelectedIndex > 0 && Calendar4.Text.Length > 0 && txtQty3.Text.Length > 0 && txtUnitPrice3.Text.Length > 0 && txtTotalPrice3.Text.Length > 0)
        {
            i++;
            DateTime testdate3 = DateTime.ParseExact(Calendar4.Text, "dd/MM/yyyy", dtf);
            theHelper.InsertPurchaseMedicine(i, compcode, yearcode, ddlMediSubGrp3.SelectedValue, TextBox1.Text, total.ToString(), txtPurchaseMedicineId.Text, DropDownList1.SelectedValue, testdate.ToString("yyyy-MM-dd"), txtReason.Text, ddlMfg3.SelectedValue, ddlMediGrp3.SelectedValue, ddlMedi3.SelectedValue, ddlBatch3.SelectedValue, testdate3.ToString("yyyy-MM-dd"), txtQty3.Text, ddlAddLess3.SelectedValue, txtUnitPrice3.Text, txtTotalPrice3.Text, Session["userName"].ToString(), txttrendDisc3.Text, txtstax3.Text, txtlessper.Text, txtTaxper.Text);
        }
        if (ddlMedi4.SelectedIndex > 0 && ddlBatch4.SelectedIndex > 0 && Calendar5.Text.Length > 0 && txtQty4.Text.Length > 0 && txtUnitPrice4.Text.Length > 0 && txtTotalPrice4.Text.Length > 0)
        {
            i++;
            DateTime testdate4 = DateTime.ParseExact(Calendar5.Text, "dd/MM/yyyy", dtf);
            theHelper.InsertPurchaseMedicine(i, compcode, yearcode, ddlMediSubGrp4.SelectedValue, TextBox1.Text, total.ToString(), txtPurchaseMedicineId.Text, DropDownList1.SelectedValue, testdate.ToString("yyyy-MM-dd"), txtReason.Text, ddlMfg4.SelectedValue, ddlMediGrp4.SelectedValue, ddlMedi4.SelectedValue, ddlBatch4.SelectedValue, testdate4.ToString("yyyy-MM-dd"), txtQty4.Text, ddlAddLess4.SelectedValue, txtUnitPrice4.Text, txtTotalPrice4.Text, Session["userName"].ToString(), txttrendDisc4.Text, txtstax4.Text, txtlessper.Text, txtTaxper.Text);
        }
        if (ddlMedi5.SelectedIndex > 0 && ddlBatch5.SelectedIndex > 0 && Calendar6.Text.Length > 0 && txtQty5.Text.Length > 0 && txtUnitPrice5.Text.Length > 0 && txtTotalPrice5.Text.Length > 0)
        {
            i++;
            DateTime testdate5 = DateTime.ParseExact(Calendar6.Text, "MM/dd/yyyy", dtf);
            theHelper.InsertPurchaseMedicine(i, compcode, yearcode, ddlMediSubGrp5.SelectedValue, TextBox1.Text, total.ToString(), txtPurchaseMedicineId.Text, DropDownList1.SelectedValue, testdate.ToString("yyyy-MM-dd"), txtReason.Text, ddlMfg5.SelectedValue, ddlMediGrp5.SelectedValue, ddlMedi5.SelectedValue, ddlBatch5.SelectedValue, testdate5.ToString("yyyy-MM-dd"), txtQty5.Text, ddlAddLess5.SelectedValue, txtUnitPrice5.Text, txtTotalPrice5.Text, Session["userName"].ToString(), txttrendDisc5.Text, txtstax5.Text, txtlessper.Text, txtTaxper.Text);
        }
        if (ddlMedi6.SelectedIndex > 0 && ddlBatch6.SelectedIndex > 0 && Calendar7.Text.Length > 0 && txtQty6.Text.Length > 0 && txtUnitPrice6.Text.Length > 0 && txtTotalPrice6.Text.Length > 0)
        {
            i++;
            DateTime testdate6 = DateTime.ParseExact(Calendar7.Text, "dd/MM/yyyy", dtf);
            theHelper.InsertPurchaseMedicine(i, compcode, yearcode, ddlMediSubGrp6.SelectedValue, TextBox1.Text, total.ToString(), txtPurchaseMedicineId.Text, DropDownList1.SelectedValue, testdate.ToString("yyyy-MM-dd"), txtReason.Text, ddlMfg6.SelectedValue, ddlMediGrp6.SelectedValue, ddlMedi6.SelectedValue, ddlBatch6.SelectedValue, testdate6.ToString("yyyy-MM-dd"), txtQty6.Text, ddlAddLess6.SelectedValue, txtUnitPrice6.Text, txtTotalPrice6.Text, Session["userName"].ToString(), txttrendDisc6.Text, txtstax6.Text, txtlessper.Text, txtTaxper.Text);
        }
        if (ddlMedi7.SelectedIndex > 0 && ddlBatch7.SelectedIndex > 0 && Calendar8.Text.Length > 0 && txtQty7.Text.Length > 0 && txtUnitPrice7.Text.Length > 0 && txtTotalPrice7.Text.Length > 0)
        {
            i++;
            DateTime testdate7 = DateTime.ParseExact(Calendar8.Text, "dd/MM/yyyy", dtf);
            theHelper.InsertPurchaseMedicine(i, compcode, yearcode, ddlMediSubGrp7.SelectedValue, TextBox1.Text, total.ToString(), txtPurchaseMedicineId.Text, DropDownList1.SelectedValue, testdate.ToString("yyyy-MM-dd"), txtReason.Text, ddlMfg7.SelectedValue, ddlMediGrp7.SelectedValue, ddlMedi7.SelectedValue, ddlBatch7.SelectedValue, testdate7.ToString("yyyy-MM-dd"), txtQty7.Text, ddlAddLess7.SelectedValue, txtUnitPrice7.Text, txtTotalPrice7.Text, Session["userName"].ToString(), txttrendDisc7.Text, txtstax7.Text, txtlessper.Text, txtTaxper.Text);
        }
        if (ddlMedi8.SelectedIndex > 0 && ddlBatch8.SelectedIndex > 0 && Calendar9.Text.Length > 0 && txtQty8.Text.Length > 0 && txtUnitPrice8.Text.Length > 0 && txtTotalPrice8.Text.Length > 0)
        {
            i++;
            DateTime testdate8 = DateTime.ParseExact(Calendar9.Text, "dd/MM/yyyy", dtf);
            theHelper.InsertPurchaseMedicine(i, compcode, yearcode, ddlMediSubGrp8.SelectedValue, TextBox1.Text, total.ToString(), txtPurchaseMedicineId.Text, DropDownList1.SelectedValue, testdate.ToString("yyyy-MM-dd"), txtReason.Text, ddlMfg8.SelectedValue, ddlMediGrp8.SelectedValue, ddlMedi8.SelectedValue, ddlBatch8.SelectedValue, testdate8.ToString("yyyy-MM-dd"), txtQty8.Text, ddlAddLess8.SelectedValue, txtUnitPrice8.Text, txtTotalPrice8.Text, Session["userName"].ToString(), txttrendDisc8.Text, txtstax8.Text, txtlessper.Text, txtTaxper.Text);
        }
        if (ddlMedi9.SelectedIndex > 0 && ddlBatch9.SelectedIndex > 0 && Calendar10.Text.Length > 0 && txtQty9.Text.Length > 0 && txtUnitPrice9.Text.Length > 0 && txtTotalPrice9.Text.Length > 0)
        {
            i++;
            DateTime testdate9 = DateTime.ParseExact(Calendar10.Text, "dd/MM/yyyy", dtf);
            theHelper.InsertPurchaseMedicine(i, compcode, yearcode, ddlMediSubGrp9.SelectedValue, TextBox1.Text, total.ToString(), txtPurchaseMedicineId.Text, DropDownList1.SelectedValue, testdate.ToString("yyyy-MM-dd"), txtReason.Text, ddlMfg9.SelectedValue, ddlMediGrp9.SelectedValue, ddlMedi9.SelectedValue, ddlBatch9.SelectedValue, testdate9.ToString("yyyy-MM-dd"), txtQty9.Text, ddlAddLess9.SelectedValue, txtUnitPrice9.Text, txtTotalPrice9.Text, Session["userName"].ToString(), txttrendDisc9.Text, txtstax9.Text, txtlessper.Text, txtTaxper.Text);
        }
        if (ddlMedi10.SelectedIndex > 0 && ddlBatch10.SelectedIndex > 0 && Calendar11.Text.Length > 0 && txtQty10.Text.Length > 0 && txtUnitPrice10.Text.Length > 0 && txtTotalPrice10.Text.Length > 0)
        {
            i++;
            DateTime testdate10 = DateTime.ParseExact(Calendar11.Text, "dd/MM/yyyy", dtf);
            theHelper.InsertPurchaseMedicine(i, compcode, yearcode, ddlMediSubGrp10.SelectedValue, TextBox1.Text, total.ToString(), txtPurchaseMedicineId.Text, DropDownList1.SelectedValue, testdate.ToString("yyyy-MM-dd"), txtReason.Text, ddlMfg10.SelectedValue, ddlMediGrp10.SelectedValue, ddlMedi10.SelectedValue, ddlBatch10.SelectedValue, testdate10.ToString("yyyy-MM-dd"), txtQty10.Text, ddlAddLess10.SelectedValue, txtUnitPrice10.Text, txtTotalPrice10.Text, Session["userName"].ToString(), txttrendDisc10.Text, txtstax10.Text, txtlessper.Text, txtTaxper.Text);
        }
        if (ddlMedi11.SelectedIndex > 0 && ddlBatch11.SelectedIndex > 0 && Calendar12.Text.Length > 0 && txtQty11.Text.Length > 0 && txtUnitPrice11.Text.Length > 0 && txtTotalPrice1.Text.Length > 0)
        {
            i++;
            DateTime testdate11 = DateTime.ParseExact(Calendar12.Text, "dd/MM/yyyy", dtf);
            theHelper.InsertPurchaseMedicine(i, compcode, yearcode, ddlMediSubGrp11.SelectedValue, TextBox1.Text, total.ToString(), txtPurchaseMedicineId.Text, DropDownList1.SelectedValue, testdate.ToString("yyyy-MM-dd"), txtReason.Text, ddlMfg11.SelectedValue, ddlMediGrp11.SelectedValue, ddlMedi11.SelectedValue, ddlBatch11.SelectedValue, testdate11.ToString("yyyy-MM-dd"), txtQty11.Text, ddlAddLess11.SelectedValue, txtUnitPrice11.Text, txtTotalPrice11.Text, Session["userName"].ToString(), txttrendDisc11.Text, txtstax11.Text, txtlessper.Text, txtTaxper.Text);
        }
        if (ddlMedi12.SelectedIndex > 0 && ddlBatch12.SelectedIndex > 0 && Calendar13.Text.Length > 0 && txtQty12.Text.Length > 0 && txtUnitPrice12.Text.Length > 0 && txtTotalPrice12.Text.Length > 0)
        {
            i++;
            DateTime testdate12 = DateTime.ParseExact(Calendar13.Text, "dd/MM/yyyy", dtf);
            theHelper.InsertPurchaseMedicine(i,compcode, yearcode, ddlMediSubGrp12.SelectedValue, TextBox1.Text, total.ToString(), txtPurchaseMedicineId.Text, DropDownList1.SelectedValue, testdate.ToString("yyyy-MM-dd"), txtReason.Text, ddlMfg12.SelectedValue, ddlMediGrp12.SelectedValue, ddlMedi12.SelectedValue, ddlBatch12.SelectedValue, testdate12.ToString("yyyy-MM-dd"), txtQty12.Text, ddlAddLess12.SelectedValue, txtUnitPrice12.Text, txtTotalPrice12.Text, Session["userName"].ToString(), txttrendDisc12.Text, txtstax12.Text, txtlessper.Text, txtTaxper.Text);
        }
        theHelper.InsUpdInv(compcode, yearcode, txtPurchaseMedicineId.Text.Trim(), "I", Session["userName"].ToString());
    }






    protected void ddlMediGrp1_SelectedIndexChanged(object sender, EventArgs e)
    {
        ddlMediSubGrp1.DataSource = thepurHelper.DropdownSubGroup(Session["CoCode"].ToString().Trim(),ddlMediGrp1.SelectedValue);
        ddlMediSubGrp1.DataTextField = "SubGrName";
        ddlMediSubGrp1.DataValueField = "ID";
        ddlMediSubGrp1.DataBind();
        ddlMediSubGrp1.Items.Insert(0, new ListItem("--Select--", "0")); 
    }
    protected void ddlMediGrp2_SelectedIndexChanged(object sender, EventArgs e)
    {
        ddlMediSubGrp2.DataSource = thepurHelper.DropdownSubGroup(Session["CoCode"].ToString().Trim(),ddlMediGrp2.SelectedValue);
        ddlMediSubGrp2.DataTextField = "SubGrName";
        ddlMediSubGrp2.DataValueField = "ID";
        ddlMediSubGrp2.DataBind();
        ddlMediSubGrp2.Items.Insert(0, new ListItem("--Select--", "0")); 
    }
    protected void ddlMediGrp3_SelectedIndexChanged(object sender, EventArgs e)
    {
        ddlMediSubGrp3.DataSource = thepurHelper.DropdownSubGroup(Session["CoCode"].ToString().Trim(),ddlMediGrp3.SelectedValue);
        ddlMediSubGrp3.DataTextField = "SubGrName";
        ddlMediSubGrp3.DataValueField = "ID";
        ddlMediSubGrp3.DataBind();
        ddlMediSubGrp3.Items.Insert(0, new ListItem("--Select--", "0")); 
    }
    protected void ddlMediGrp4_SelectedIndexChanged(object sender, EventArgs e)
    {
        ddlMediSubGrp4.DataSource = thepurHelper.DropdownSubGroup(Session["CoCode"].ToString().Trim(),ddlMediGrp4.SelectedValue);
        ddlMediSubGrp4.DataTextField = "SubGrName";
        ddlMediSubGrp4.DataValueField = "ID";
        ddlMediSubGrp4.DataBind();
        ddlMediSubGrp4.Items.Insert(0, new ListItem("--Select--", "0")); 
    }
    protected void ddlMediGrp5_SelectedIndexChanged(object sender, EventArgs e)
    {
        ddlMediSubGrp5.DataSource = thepurHelper.DropdownSubGroup(Session["CoCode"].ToString().Trim(),ddlMediGrp5.SelectedValue);
        ddlMediSubGrp5.DataTextField = "SubGrName";
        ddlMediSubGrp5.DataValueField = "ID";
        ddlMediSubGrp5.DataBind();
        ddlMediSubGrp5.Items.Insert(0, new ListItem("--Select--", "0"));  
    }
    protected void ddlMediGrp6_SelectedIndexChanged(object sender, EventArgs e)
    {
        ddlMediSubGrp6.DataSource = thepurHelper.DropdownSubGroup(Session["CoCode"].ToString().Trim(),ddlMediGrp6.SelectedValue);
        ddlMediSubGrp6.DataTextField = "SubGrName";
        ddlMediSubGrp6.DataValueField = "ID";
        ddlMediSubGrp6.DataBind();
        ddlMediSubGrp6.Items.Insert(0, new ListItem("--Select--", "0")); 
    }
    protected void ddlMediGrp7_SelectedIndexChanged(object sender, EventArgs e)
    {
        ddlMediSubGrp7.DataSource = thepurHelper.DropdownSubGroup(Session["CoCode"].ToString().Trim(),ddlMediGrp7.SelectedValue);
        ddlMediSubGrp7.DataTextField = "SubGrName";
        ddlMediSubGrp7.DataValueField = "ID";
        ddlMediSubGrp7.DataBind();
        ddlMediSubGrp7.Items.Insert(0, new ListItem("--Select--", "0")); 
    }
    protected void ddlMediGrp8_SelectedIndexChanged(object sender, EventArgs e)
    {
        ddlMediSubGrp8.DataSource = thepurHelper.DropdownSubGroup(Session["CoCode"].ToString().Trim(),ddlMediGrp8.SelectedValue);
        ddlMediSubGrp8.DataTextField = "SubGrName";
        ddlMediSubGrp8.DataValueField = "ID";
        ddlMediSubGrp8.DataBind();
        ddlMediSubGrp8.Items.Insert(0, new ListItem("--Select--", "0"));  
    }
    protected void ddlMediGrp9_SelectedIndexChanged(object sender, EventArgs e)
    {
        ddlMediSubGrp9.DataSource = thepurHelper.DropdownSubGroup(Session["CoCode"].ToString().Trim(),ddlMediGrp9.SelectedValue);
        ddlMediSubGrp9.DataTextField = "SubGrName";
        ddlMediSubGrp9.DataValueField = "ID";
        ddlMediSubGrp9.DataBind();
        ddlMediSubGrp9.Items.Insert(0, new ListItem("--Select--", "0")); 
    }
    protected void ddlMediGrp10_SelectedIndexChanged(object sender, EventArgs e)
    {
        ddlMediSubGrp10.DataSource = thepurHelper.DropdownSubGroup(Session["CoCode"].ToString().Trim(),ddlMediGrp10.SelectedValue);
        ddlMediSubGrp10.DataTextField = "SubGrName";
        ddlMediSubGrp10.DataValueField = "ID";
        ddlMediSubGrp10.DataBind();
        ddlMediSubGrp10.Items.Insert(0, new ListItem("--Select--", "0")); 
    }
    protected void ddlMediGrp11_SelectedIndexChanged(object sender, EventArgs e)
    {
        ddlMediSubGrp11.DataSource = thepurHelper.DropdownSubGroup(Session["CoCode"].ToString().Trim(),ddlMediGrp11.SelectedValue);
        ddlMediSubGrp11.DataTextField = "SubGrName";
        ddlMediSubGrp11.DataValueField = "ID";
        ddlMediSubGrp11.DataBind();
        ddlMediSubGrp11.Items.Insert(0, new ListItem("--Select--", "0")); 
    }
    protected void ddlMediGrp12_SelectedIndexChanged(object sender, EventArgs e)
    {
        ddlMediSubGrp12.DataSource = thepurHelper.DropdownSubGroup(Session["CoCode"].ToString().Trim(),ddlMediGrp12.SelectedValue);
        ddlMediSubGrp12.DataTextField = "SubGrName";
        ddlMediSubGrp12.DataValueField = "ID";
        ddlMediSubGrp12.DataBind();
        ddlMediSubGrp12.Items.Insert(0, new ListItem("--Select--", "0"));
    }
    protected void txtQty1_TextChanged(object sender, EventArgs e)
    {
        /*if (txtQty1.Text != "")
        {
            double totalPrice = Convert.ToDouble(txtUnitPrice1.Text) * Convert.ToDouble(txtQty1.Text);
            txtTotalPrice1.Text = totalPrice.ToString();
        }
        else
        {
            txtTotalPrice1.Text = 0.ToString();
        }*/
       
        Double totAmt = calcTotAmt(txtQty1.Text, txtUnitPrice1.Text, txttrendDisc1.Text, txtstax1.Text);
        
        if (totAmt > 0.00)
        {
            txtTotalPrice1.Text = totAmt.ToString();
        }
        else
        {
            txtTotalPrice1.Text = "";
        }
    }
    protected void txtQty2_TextChanged(object sender, EventArgs e)
    {
       /* if (txtQty2.Text != "")
        {
            double totalPrice = Convert.ToDouble(txtUnitPrice2.Text) * Convert.ToDouble(txtQty2.Text);
            txtTotalPrice2.Text = totalPrice.ToString();
        }
        else
        {
            txtTotalPrice2.Text = 0.ToString();
        }*/
        Double totAmt = calcTotAmt(txtQty2.Text, txtUnitPrice2.Text, txttrendDisc2.Text, txtstax2.Text);
        if (totAmt > 0.00)
        {
            txtTotalPrice2.Text = totAmt.ToString();
        }
        else
        {
            txtTotalPrice2.Text = "";
        }
    }
    protected void txtQty3_TextChanged(object sender, EventArgs e)
    {
       /* if (txtQty3.Text != "")
        {
            double totalPrice = Convert.ToDouble(txtUnitPrice3.Text) * Convert.ToDouble(txtQty3.Text);
            txtTotalPrice3.Text = totalPrice.ToString();
        }
        else
        {
            txtTotalPrice3.Text = 0.ToString();
        }*/
        Double totAmt = calcTotAmt(txtQty3.Text, txtUnitPrice3.Text, txttrendDisc3.Text, txtstax3.Text);
        if (totAmt > 0.00)
        {
            txtTotalPrice3.Text = totAmt.ToString();
        }
        else
        {
            txtTotalPrice3.Text = "";
        }
    }
    protected void txtQty4_TextChanged(object sender, EventArgs e)
    {
        /*if (txtQty4.Text != "")
        {
            double totalPrice = Convert.ToDouble(txtUnitPrice4.Text) * Convert.ToDouble(txtQty4.Text);
            txtTotalPrice4.Text = totalPrice.ToString();
        }
        else
        {
            txtTotalPrice4.Text = 0.ToString();
        }*/
        Double totAmt = calcTotAmt(txtQty4.Text, txtUnitPrice4.Text, txttrendDisc4.Text, txtstax4.Text);
        if (totAmt > 0.00)
        {
            txtTotalPrice4.Text = totAmt.ToString();
        }
        else
        {
            txtTotalPrice4.Text = "";
        }
    }
    protected void txtQty5_TextChanged(object sender, EventArgs e)
    {
       /* if (txtQty5.Text != "")
        {
            double totalPrice = Convert.ToDouble(txtUnitPrice5.Text) * Convert.ToDouble(txtQty5.Text);
            txtTotalPrice5.Text = totalPrice.ToString();
        }
        else
        {
            txtTotalPrice5.Text = 0.ToString();
        }*/
        Double totAmt = calcTotAmt(txtQty5.Text, txtUnitPrice5.Text, txttrendDisc5.Text, txtstax5.Text);
        if (totAmt > 0.00)
        {
            txtTotalPrice5.Text = totAmt.ToString();
        }
        else
        {
            txtTotalPrice5.Text = "";
        }
    }
    protected void txtQty6_TextChanged(object sender, EventArgs e)
    {
        /*if (txtQty6.Text != "")
        {
            double totalPrice = Convert.ToDouble(txtUnitPrice6.Text) * Convert.ToDouble(txtQty6.Text);
            txtTotalPrice6.Text = totalPrice.ToString();
        }
        else
        {
            txtTotalPrice6.Text = 0.ToString();
        }*/
        Double totAmt = calcTotAmt(txtQty6.Text, txtUnitPrice6.Text, txttrendDisc6.Text, txtstax6.Text);
        if (totAmt > 0.00)
        {
            txtTotalPrice6.Text = totAmt.ToString();
        }
        else
        {
            txtTotalPrice6.Text = "";
        }
    }
    protected void txtQty7_TextChanged(object sender, EventArgs e)
    {
        /*if (txtQty7.Text != "")
        {
            double totalPrice = Convert.ToDouble(txtUnitPrice7.Text) * Convert.ToDouble(txtQty7.Text);
            txtTotalPrice7.Text = totalPrice.ToString();
        }
        else
        {
            txtTotalPrice7.Text = 0.ToString();
        }*/

        Double totAmt = calcTotAmt(txtQty7.Text, txtUnitPrice7.Text, txttrendDisc7.Text, txtstax7.Text);
        if (totAmt > 0.00)
        {
            txtTotalPrice7.Text = totAmt.ToString();
        }
        else
        {
            txtTotalPrice7.Text = "";
        }
    }
    protected void txtQty8_TextChanged(object sender, EventArgs e)
    {
       /* if (txtQty8.Text != "")
        {
            double totalPrice = Convert.ToDouble(txtUnitPrice8.Text) * Convert.ToDouble(txtQty8.Text);
            txtTotalPrice8.Text = totalPrice.ToString();
        }
        else
        {
            txtTotalPrice8.Text = 0.ToString();
        }*/
        Double totAmt = calcTotAmt(txtQty8.Text, txtUnitPrice8.Text, txttrendDisc8.Text, txtstax8.Text);
        if (totAmt > 0.00)
        {
            txtTotalPrice8.Text = totAmt.ToString();
        }
        else
        {
            txtTotalPrice8.Text = "";
        }
    }
    protected void txtQty9_TextChanged(object sender, EventArgs e)
    {
       /* if (txtQty9.Text != "")
        {
            double totalPrice = Convert.ToDouble(txtUnitPrice9.Text) * Convert.ToDouble(txtQty9.Text);
            txtTotalPrice9.Text = totalPrice.ToString();
        }
        else
        {
            txtTotalPrice9.Text = 0.ToString();
        }*/
        Double totAmt = calcTotAmt(txtQty9.Text, txtUnitPrice9.Text, txttrendDisc9.Text, txtstax9.Text);
        if (totAmt > 0.00)
        {
            txtTotalPrice9.Text = totAmt.ToString();
        }
        else
        {
            txtTotalPrice9.Text = "";
        }
    }
    protected void txtQty10_TextChanged(object sender, EventArgs e)
    {
      /*  if (txtQty10.Text != "")
        {
            double totalPrice = Convert.ToDouble(txtUnitPrice10.Text) * Convert.ToDouble(txtQty10.Text);
            txtTotalPrice10.Text = totalPrice.ToString();
        }
        else
        {
            txtTotalPrice10.Text = 0.ToString();
        }*/

        Double totAmt = calcTotAmt(txtQty10.Text, txtUnitPrice10.Text, txttrendDisc10.Text, txtstax10.Text);
        if (totAmt > 0.00)
        {
            txtTotalPrice10.Text = totAmt.ToString();
        }
        else
        {
            txtTotalPrice10.Text = "";
        }
    }
    protected void txtQty11_TextChanged(object sender, EventArgs e)
    {
        /*if (txtQty11.Text != "")
        {
            double totalPrice = Convert.ToDouble(txtUnitPrice11.Text) * Convert.ToDouble(txtQty11.Text);
            txtTotalPrice11.Text = totalPrice.ToString();
        }
        else
        {
            txtTotalPrice11.Text = 0.ToString();
        }*/

        Double totAmt = calcTotAmt(txtQty11.Text, txtUnitPrice11.Text, txttrendDisc11.Text, txtstax11.Text);
        if (totAmt > 0.00)
        {
            txtTotalPrice11.Text = totAmt.ToString();
        }
        else
        {
            txtTotalPrice11.Text = "";
        }
    }
    protected void txtQty12_TextChanged(object sender, EventArgs e)
    {
        /*if (txtQty12.Text != "")
        {
            double totalPrice = Convert.ToDouble(txtUnitPrice12.Text) * Convert.ToDouble(txtQty12.Text);
            txtTotalPrice12.Text = totalPrice.ToString();
        }
        else
        {
            txtTotalPrice12.Text = 0.ToString();
        }*/

        Double totAmt = calcTotAmt(txtQty12.Text, txtUnitPrice12.Text, txttrendDisc12.Text, txtstax12.Text);
        if (totAmt > 0.00)
        {
            txtTotalPrice12.Text = totAmt.ToString();
        }
        else
        {
            txtTotalPrice12.Text = "";
        }
    }
    protected void ddlMedi1_SelectedIndexChanged(object sender, EventArgs e)
    {
        ddlBatch1.Items.Clear();
        ddlBatch1.DataSource = theHelper.getBatchId(Session["CoCode"].ToString().Trim(), Session["YearCode"].ToString().Trim(), ddlMfg1.SelectedValue, ddlMedi1.SelectedValue, DropDownList1.SelectedValue);
        ddlBatch1.DataTextField = "BatchNo";
        ddlBatch1.DataValueField = "BatchNo";
        ddlBatch1.DataBind();
        this.ddlBatch1.Items.Insert(0, new ListItem("--Select--", "0"));

        MfgGrpSgrpFill(ddlMedi1.SelectedValue, 1);
       // txtUnitPrice1.Text = theHelper.GetPurchasePricePerUnit(ddlMedi1.SelectedValue).ToString();
    }
    protected void ddlMedi2_SelectedIndexChanged(object sender, EventArgs e)
    {

       // txtUnitPrice2.Text = theHelper.GetPurchasePricePerUnit(ddlMedi2.SelectedValue).ToString();
        ddlBatch2.Items.Clear();
        ddlBatch2.DataSource = theHelper.getBatchId(Session["CoCode"].ToString().Trim(), Session["YearCode"].ToString().Trim(), ddlMfg2.SelectedValue, ddlMedi2.SelectedValue, DropDownList1.SelectedValue);
        ddlBatch2.DataTextField = "BatchNo";
        ddlBatch2.DataValueField = "BatchNo";
        ddlBatch2.DataBind();
        this.ddlBatch2.Items.Insert(0, new ListItem("--Select--", "0"));

        MfgGrpSgrpFill(ddlMedi2.SelectedValue, 2);
    }
    protected void ddlMedi3_SelectedIndexChanged(object sender, EventArgs e)
    {
       // txtUnitPrice3.Text = theHelper.GetPurchasePricePerUnit(ddlMedi3.SelectedValue).ToString();

        ddlBatch3.Items.Clear();
        ddlBatch3.DataSource = theHelper.getBatchId(Session["CoCode"].ToString().Trim(), Session["YearCode"].ToString().Trim(), ddlMfg3.SelectedValue, ddlMedi3.SelectedValue, DropDownList1.SelectedValue);
        ddlBatch3.DataTextField = "BatchNo";
        ddlBatch3.DataValueField = "BatchNo";
        ddlBatch3.DataBind();
        this.ddlBatch3.Items.Insert(0, new ListItem("--Select--", "0"));
        MfgGrpSgrpFill(ddlMedi3.SelectedValue, 3);

    }
    protected void ddlMedi4_SelectedIndexChanged(object sender, EventArgs e)
    {
       // txtUnitPrice4.Text = theHelper.GetPurchasePricePerUnit(ddlMedi4.SelectedValue).ToString();
        ddlBatch4.Items.Clear();
        ddlBatch4.DataSource = theHelper.getBatchId(Session["CoCode"].ToString().Trim(), Session["YearCode"].ToString().Trim(), ddlMfg4.SelectedValue, ddlMedi4.SelectedValue, DropDownList1.SelectedValue);
        ddlBatch4.DataTextField = "BatchNo";
        ddlBatch4.DataValueField = "BatchNo";
        ddlBatch4.DataBind();
        this.ddlBatch4.Items.Insert(0, new ListItem("--Select--", "0"));

        MfgGrpSgrpFill(ddlMedi4.SelectedValue, 4);
    }
    protected void ddlMedi5_SelectedIndexChanged(object sender, EventArgs e)
    {
       // txtUnitPrice5.Text = theHelper.GetPurchasePricePerUnit(ddlMedi5.SelectedValue).ToString();

        ddlBatch5.Items.Clear();
        ddlBatch5.DataSource = theHelper.getBatchId(Session["CoCode"].ToString().Trim(), Session["YearCode"].ToString().Trim(), ddlMfg5.SelectedValue, ddlMedi5.SelectedValue, DropDownList1.SelectedValue);
        ddlBatch5.DataTextField = "BatchNo";
        ddlBatch5.DataValueField = "BatchNo";
        ddlBatch5.DataBind();
        this.ddlBatch5.Items.Insert(0, new ListItem("--Select--", "0"));
        MfgGrpSgrpFill(ddlMedi5.SelectedValue, 5);

    }
    protected void ddlMedi6_SelectedIndexChanged(object sender, EventArgs e)
    {
       // txtUnitPrice6.Text = theHelper.GetPurchasePricePerUnit(ddlMedi6.SelectedValue).ToString();
        ddlBatch6.Items.Clear();
        ddlBatch6.DataSource = theHelper.getBatchId(Session["CoCode"].ToString().Trim(), Session["YearCode"].ToString().Trim(), ddlMfg6.SelectedValue, ddlMedi6.SelectedValue, DropDownList1.SelectedValue);
        ddlBatch6.DataTextField = "BatchNo";
        ddlBatch6.DataValueField = "BatchNo";
        ddlBatch6.DataBind();
        this.ddlBatch6.Items.Insert(0, new ListItem("--Select--", "0"));
        MfgGrpSgrpFill(ddlMedi6.SelectedValue, 6);

    }
    protected void ddlMedi7_SelectedIndexChanged(object sender, EventArgs e)
    {
       // txtUnitPrice7.Text = theHelper.GetPurchasePricePerUnit(ddlMedi7.SelectedValue).ToString();
        ddlBatch7.Items.Clear();
        ddlBatch7.DataSource = theHelper.getBatchId(Session["CoCode"].ToString().Trim(), Session["YearCode"].ToString().Trim(), ddlMfg7.SelectedValue, ddlMedi7.SelectedValue, DropDownList1.SelectedValue);
        ddlBatch7.DataTextField = "BatchNo";
        ddlBatch7.DataValueField = "BatchNo";
        ddlBatch7.DataBind();
        this.ddlBatch7.Items.Insert(0, new ListItem("--Select--", "0"));

        MfgGrpSgrpFill(ddlMedi7.SelectedValue, 7);
    }
    protected void ddlMedi8_SelectedIndexChanged(object sender, EventArgs e)
    {
      //  txtUnitPrice8.Text = theHelper.GetPurchasePricePerUnit(ddlMedi8.SelectedValue).ToString();

        ddlBatch8.Items.Clear();
        ddlBatch8.DataSource = theHelper.getBatchId(Session["CoCode"].ToString().Trim(), Session["YearCode"].ToString().Trim(), ddlMfg8.SelectedValue, ddlMedi8.SelectedValue, DropDownList1.SelectedValue);
        ddlBatch8.DataTextField = "BatchNo";
        ddlBatch8.DataValueField = "BatchNo";
        ddlBatch8.DataBind();
        this.ddlBatch8.Items.Insert(0, new ListItem("--Select--", "0"));

        MfgGrpSgrpFill(ddlMedi8.SelectedValue, 8);
    }
    protected void ddlMedi9_SelectedIndexChanged(object sender, EventArgs e)
    {
        // txtUnitPrice9.Text = theHelper.GetPurchasePricePerUnit(ddlMedi9.SelectedValue).ToString();

        ddlBatch9.Items.Clear();
        ddlBatch9.DataSource = theHelper.getBatchId(Session["CoCode"].ToString().Trim(), Session["YearCode"].ToString().Trim(), ddlMfg9.SelectedValue, ddlMedi9.SelectedValue, DropDownList1.SelectedValue);
        ddlBatch9.DataTextField = "BatchNo";
        ddlBatch9.DataValueField = "BatchNo";
        ddlBatch9.DataBind();
        this.ddlBatch9.Items.Insert(0, new ListItem("--Select--", "0"));

        MfgGrpSgrpFill(ddlMedi9.SelectedValue, 9);
    }

    protected void ddlMedi10_SelectedIndexChanged(object sender, EventArgs e)
    {
        // txtUnitPrice9.Text = theHelper.GetPurchasePricePerUnit(ddlMedi9.SelectedValue).ToString();

        ddlBatch10.Items.Clear();
        ddlBatch10.DataSource = theHelper.getBatchId(Session["CoCode"].ToString().Trim(), Session["YearCode"].ToString().Trim(), ddlMfg10.SelectedValue, ddlMedi10.SelectedValue, DropDownList1.SelectedValue);
        ddlBatch10.DataTextField = "BatchNo";
        ddlBatch10.DataValueField = "BatchNo";
        ddlBatch10.DataBind();
        this.ddlBatch10.Items.Insert(0, new ListItem("--Select--", "0"));

        MfgGrpSgrpFill(ddlMedi10.SelectedValue, 10);
    }

    protected void ddlMedi11_SelectedIndexChanged(object sender, EventArgs e)
    {
        // txtUnitPrice9.Text = theHelper.GetPurchasePricePerUnit(ddlMedi9.SelectedValue).ToString();

        ddlBatch11.Items.Clear();
        ddlBatch11.DataSource = theHelper.getBatchId(Session["CoCode"].ToString().Trim(), Session["YearCode"].ToString().Trim(), ddlMfg11.SelectedValue, ddlMedi11.SelectedValue, DropDownList1.SelectedValue);
        ddlBatch11.DataTextField = "BatchNo";
        ddlBatch11.DataValueField = "BatchNo";
        ddlBatch11.DataBind();
        this.ddlBatch11.Items.Insert(0, new ListItem("--Select--", "0"));

        MfgGrpSgrpFill(ddlMedi11.SelectedValue, 11);
    }

    protected void ddlMedi12_SelectedIndexChanged(object sender, EventArgs e)
    {
       // txtUnitPrice12.Text = theHelper.GetPurchasePricePerUnit(ddlMedi12.SelectedValue).ToString();
        ddlBatch12.Items.Clear();
        ddlBatch12.DataSource = theHelper.getBatchId(Session["CoCode"].ToString().Trim(), Session["YearCode"].ToString().Trim(), ddlMfg12.SelectedValue, ddlMedi12.SelectedValue, DropDownList1.SelectedValue);
        ddlBatch12.DataTextField = "MedicineName";
        ddlBatch12.DataValueField = "MedicineID";
        ddlBatch12.DataBind();
        this.ddlBatch12.Items.Insert(0, new ListItem("--Select--", "0"));
        MfgGrpSgrpFill(ddlMedi12.SelectedValue, 12);

    }
    protected void Button4_Click(object sender, EventArgs e)
    {
        HiddenField1.Value = txtPurchaseMedicineId.Text;
        ResetAllFields();
        txtPurchaseMedicineId.Text = HiddenField1.Value;
        DropDownFill();
        PageDataBind();

    }
    protected void ddlMediSubGrp1_SelectedIndexChanged(object sender, EventArgs e)
    {
        ddlMedi1.Items.Clear();
        ddlMedi1.DataSource = theHelper.DropdownID4(Session["CoCode"].ToString().Trim(),ddlMediGrp1.SelectedValue, ddlMediSubGrp1.SelectedValue);
        ddlMedi1.DataTextField = "MedicineName";
        ddlMedi1.DataValueField = "MedicineID";
        ddlMedi1.DataBind();
        this.ddlMedi1.Items.Insert(0, new ListItem("--Select--", "0"));
    }
    protected void ddlMediSubGrp2_SelectedIndexChanged(object sender, EventArgs e)
    {
        ddlMedi2.Items.Clear();
        ddlMedi2.DataSource = theHelper.DropdownID4(Session["CoCode"].ToString().Trim(),ddlMediGrp2.SelectedValue, ddlMediSubGrp2.SelectedValue);
        ddlMedi2.DataTextField = "MedicineName";
        ddlMedi2.DataValueField = "MedicineID";
        ddlMedi2.DataBind();
        this.ddlMedi2.Items.Insert(0, new ListItem("--Select--", "0"));
    }
    protected void ddlMediSubGrp3_SelectedIndexChanged(object sender, EventArgs e)
    {
        ddlMedi3.Items.Clear();
        ddlMedi3.DataSource = theHelper.DropdownID4(Session["CoCode"].ToString().Trim(),ddlMediGrp3.SelectedValue, ddlMediSubGrp3.SelectedValue);
        ddlMedi3.DataTextField = "MedicineName";
        ddlMedi3.DataValueField = "MedicineID";
        ddlMedi3.DataBind();
        this.ddlMedi3.Items.Insert(0, new ListItem("--Select--", "0"));
    }
    protected void ddlMediSubGrp4_SelectedIndexChanged(object sender, EventArgs e)
    {
        ddlMedi4.Items.Clear();
        ddlMedi4.DataSource = theHelper.DropdownID4(Session["CoCode"].ToString().Trim(),ddlMediGrp4.SelectedValue, ddlMediSubGrp4.SelectedValue);
        ddlMedi4.DataTextField = "MedicineName";
        ddlMedi4.DataValueField = "MedicineID";
        ddlMedi4.DataBind();
        this.ddlMedi4.Items.Insert(0, new ListItem("--Select--", "0"));
    }
    protected void ddlMediSubGrp5_SelectedIndexChanged(object sender, EventArgs e)
    {
        ddlMedi5.Items.Clear();
        ddlMedi5.DataSource = theHelper.DropdownID4(Session["CoCode"].ToString().Trim(),ddlMediGrp5.SelectedValue, ddlMediSubGrp5.SelectedValue);
        ddlMedi5.DataTextField = "MedicineName";
        ddlMedi5.DataValueField = "MedicineID";
        ddlMedi5.DataBind();
        this.ddlMedi5.Items.Insert(0, new ListItem("--Select--", "0"));
    }
    protected void ddlMediSubGrp6_SelectedIndexChanged(object sender, EventArgs e)
    {
        ddlMedi6.Items.Clear();
        ddlMedi6.DataSource = theHelper.DropdownID4(Session["CoCode"].ToString().Trim(),ddlMediGrp6.SelectedValue, ddlMediSubGrp6.SelectedValue);
        ddlMedi6.DataTextField = "MedicineName";
        ddlMedi6.DataValueField = "MedicineID";
        ddlMedi6.DataBind();
        this.ddlMedi6.Items.Insert(0, new ListItem("--Select--", "0"));
    }
    protected void ddlMediSubGrp7_SelectedIndexChanged(object sender, EventArgs e)
    {
        ddlMedi7.Items.Clear();
        ddlMedi7.DataSource = theHelper.DropdownID4(Session["CoCode"].ToString().Trim(),ddlMediGrp7.SelectedValue, ddlMediSubGrp7.SelectedValue);
        ddlMedi7.DataTextField = "MedicineName";
        ddlMedi7.DataValueField = "MedicineID";
        ddlMedi7.DataBind();
        this.ddlMedi7.Items.Insert(0, new ListItem("--Select--", "0"));
    }
    protected void ddlMediSubGrp8_SelectedIndexChanged(object sender, EventArgs e)
    {
        ddlMedi8.Items.Clear();
        ddlMedi8.DataSource = theHelper.DropdownID4(Session["CoCode"].ToString().Trim(),ddlMediGrp8.SelectedValue, ddlMediSubGrp8.SelectedValue);
        ddlMedi8.DataTextField = "MedicineName";
        ddlMedi8.DataValueField = "MedicineID";
        ddlMedi8.DataBind();
        this.ddlMedi8.Items.Insert(0, new ListItem("--Select--", "0"));
    }
    protected void ddlMediSubGrp9_SelectedIndexChanged(object sender, EventArgs e)
    {
        ddlMedi9.Items.Clear();
        ddlMedi9.DataSource = theHelper.DropdownID4(Session["CoCode"].ToString().Trim(),ddlMediGrp9.SelectedValue, ddlMediSubGrp9.SelectedValue);
        ddlMedi9.DataTextField = "MedicineName";
        ddlMedi9.DataValueField = "MedicineID";
        ddlMedi9.DataBind();
        this.ddlMedi9.Items.Insert(0, new ListItem("--Select--", "0"));
    }
    protected void ddlMediSubGrp10_SelectedIndexChanged(object sender, EventArgs e)
    {
        ddlMedi10.Items.Clear();
        ddlMedi10.DataSource = theHelper.DropdownID4(Session["CoCode"].ToString().Trim(),ddlMediGrp10.SelectedValue, ddlMediSubGrp10.SelectedValue);
        ddlMedi10.DataTextField = "MedicineName";
        ddlMedi10.DataValueField = "MedicineID";
        ddlMedi10.DataBind();
        this.ddlMedi10.Items.Insert(0, new ListItem("--Select--", "0"));
    }
    protected void ddlMediSubGrp11_SelectedIndexChanged(object sender, EventArgs e)
    {
        ddlMedi11.Items.Clear();
        ddlMedi11.DataSource = theHelper.DropdownID4(Session["CoCode"].ToString().Trim(),ddlMediGrp11.SelectedValue, ddlMediSubGrp11.SelectedValue);
        ddlMedi11.DataTextField = "MedicineName";
        ddlMedi11.DataValueField = "MedicineID";
        ddlMedi11.DataBind();
        this.ddlMedi11.Items.Insert(0, new ListItem("--Select--", "0"));
    }
    protected void ddlMediSubGrp12_SelectedIndexChanged(object sender, EventArgs e)
    {
        ddlMedi12.Items.Clear();
        ddlMedi12.DataSource = theHelper.DropdownID4(Session["CoCode"].ToString().Trim(),ddlMediGrp12.SelectedValue, ddlMediSubGrp12.SelectedValue);
        ddlMedi12.DataTextField = "MedicineName";
        ddlMedi12.DataValueField = "MedicineID";
        ddlMedi12.DataBind();
        this.ddlMedi12.Items.Insert(0, new ListItem("--Select--", "0"));
    }


    protected void ddlBatch1_SelectedIndexChanged(object sender, EventArgs e)
    {
        DataTable meddata = theHelper.getPurMedicineRec(ddlMfg1.SelectedValue, ddlMedi1.SelectedValue, ddlBatch1.SelectedValue);
        if (meddata.Rows.Count > 0)
        {
            Calendar2.Text = meddata.Rows[0]["expiredate"].ToString();
            txtUnitPrice1.Text = meddata.Rows[0]["unitprice"].ToString();
            if (txtlessper.Text != "")
            {
                txttrendDisc1.Text = ((Convert.ToDouble(txtlessper.Text) * Convert.ToDouble(meddata.Rows[0]["unitprice"].ToString())) / 100).ToString();
            }
            else
            {
                txttrendDisc1.Text = "";
            }
            if (txtTaxper.Text != "")
            {
                txtstax1.Text = ((Convert.ToDouble(txtTaxper.Text) * Convert.ToDouble(meddata.Rows[0]["unitprice"].ToString())) / 100).ToString();
            }
            else
            {
                txtstax1.Text = "";
            }
        }
        else
        {
            Calendar2.Text = "";
            txtUnitPrice1.Text = "";
            txttrendDisc1.Text = "";
            txtstax1.Text = "";
        }
    }
    protected void ddlBatch2_SelectedIndexChanged(object sender, EventArgs e)
    {
        DataTable meddata = theHelper.getPurMedicineRec(ddlMfg2.SelectedValue, ddlMedi2.SelectedValue, ddlBatch2.SelectedValue);
        if (meddata.Rows.Count > 0)
        {
            Calendar3.Text = meddata.Rows[0]["expiredate"].ToString();
            txtUnitPrice2.Text = meddata.Rows[0]["unitprice"].ToString();
            if (txtlessper.Text != "")
            {
                txttrendDisc2.Text = ((Convert.ToDouble(txtlessper.Text) * Convert.ToDouble(meddata.Rows[0]["unitprice"].ToString())) / 100).ToString();
            }
            else
            {
                txttrendDisc2.Text = "";
            }
            if (txtTaxper.Text != "")
            {
                txtstax2.Text = ((Convert.ToDouble(txtTaxper.Text) * Convert.ToDouble(meddata.Rows[0]["unitprice"].ToString())) / 100).ToString();
            }
            else
            {
                txtstax2.Text = "";
            }
        }
        else
        {
            Calendar3.Text = "";
            txtUnitPrice2.Text = "";
            txttrendDisc2.Text = "";
            txtstax2.Text = "";
        }
    }
    protected void ddlBatch3_SelectedIndexChanged(object sender, EventArgs e)
    {
        DataTable meddata = theHelper.getPurMedicineRec(ddlMfg3.SelectedValue, ddlMedi3.SelectedValue, ddlBatch3.SelectedValue);
        if (meddata.Rows.Count > 0)
        {
            Calendar4.Text = meddata.Rows[0]["expiredate"].ToString();
            txtUnitPrice3.Text = meddata.Rows[0]["unitprice"].ToString();
            if (txtlessper.Text != "")
            {
                txttrendDisc3.Text = ((Convert.ToDouble(txtlessper.Text) * Convert.ToDouble(meddata.Rows[0]["unitprice"].ToString())) / 100).ToString();
            }
            else
            {
                txttrendDisc3.Text = "";
            }
            if (txtTaxper.Text != "")
            {
                txtstax3.Text = ((Convert.ToDouble(txtTaxper.Text) * Convert.ToDouble(meddata.Rows[0]["unitprice"].ToString())) / 100).ToString();
            }
            else
            {
                txtstax3.Text = "";
            }
        }
        else
        {
            Calendar4.Text = "";
            txtUnitPrice3.Text = "";
            txttrendDisc3.Text = "";
            txtstax3.Text = "";
        }
    }
    protected void ddlBatch4_SelectedIndexChanged(object sender, EventArgs e)
    {
        DataTable meddata = theHelper.getPurMedicineRec(ddlMfg4.SelectedValue, ddlMedi4.SelectedValue, ddlBatch4.SelectedValue);
        if (meddata.Rows.Count > 0)
        {
            Calendar5.Text = meddata.Rows[0]["expiredate"].ToString();
            txtUnitPrice4.Text = meddata.Rows[0]["unitprice"].ToString();
            if (txtlessper.Text != "")
            {
                txttrendDisc4.Text = ((Convert.ToDouble(txtlessper.Text) * Convert.ToDouble(meddata.Rows[0]["unitprice"].ToString())) / 100).ToString();
            }
            else
            {
                txttrendDisc4.Text = "";
            }
            if (txtTaxper.Text != "")
            {
                txtstax4.Text = ((Convert.ToDouble(txtTaxper.Text) * Convert.ToDouble(meddata.Rows[0]["unitprice"].ToString())) / 100).ToString();
            }
            else
            {
                txtstax4.Text = "";
            }
        }
        else
        {
            Calendar5.Text = "";
            txtUnitPrice4.Text = "";
            txttrendDisc4.Text = "";
            txtstax4.Text = "";
        }
    }
    protected void ddlBatch5_SelectedIndexChanged(object sender, EventArgs e)
    {
        DataTable meddata = theHelper.getPurMedicineRec(ddlMfg5.SelectedValue, ddlMedi5.SelectedValue, ddlBatch5.SelectedValue);
        if (meddata.Rows.Count > 0)
        {
            Calendar6.Text = meddata.Rows[0]["expiredate"].ToString();
            txtUnitPrice5.Text = meddata.Rows[0]["unitprice"].ToString();
            if (txtlessper.Text != "")
            {
                txttrendDisc5.Text = ((Convert.ToDouble(txtlessper.Text) * Convert.ToDouble(meddata.Rows[0]["unitprice"].ToString())) / 100).ToString();
            }
            else
            {
                txttrendDisc5.Text = "";
            }
            if (txtTaxper.Text != "")
            {
                txtstax5.Text = ((Convert.ToDouble(txtTaxper.Text) * Convert.ToDouble(meddata.Rows[0]["unitprice"].ToString())) / 100).ToString();
            }
            else
            {
                txtstax5.Text = "";
            }
        }
        else
        {
            Calendar6.Text = "";
            txtUnitPrice5.Text = "";
            txttrendDisc5.Text = "";
            txtstax5.Text = "";
        }
    }
    protected void ddlBatch6_SelectedIndexChanged(object sender, EventArgs e)
    {
        DataTable meddata = theHelper.getPurMedicineRec(ddlMfg6.SelectedValue, ddlMedi6.SelectedValue, ddlBatch6.SelectedValue);
        if (meddata.Rows.Count > 0)
        {
            Calendar7.Text = meddata.Rows[0]["expiredate"].ToString();
            txtUnitPrice6.Text = meddata.Rows[0]["unitprice"].ToString();
            if (txtlessper.Text != "")
            {
                txttrendDisc6.Text = ((Convert.ToDouble(txtlessper.Text) * Convert.ToDouble(meddata.Rows[0]["unitprice"].ToString())) / 100).ToString();
            }
            else
            {
                txttrendDisc6.Text = "";
            }
            if (txtTaxper.Text != "")
            {
                txtstax6.Text = ((Convert.ToDouble(txtTaxper.Text) * Convert.ToDouble(meddata.Rows[0]["unitprice"].ToString())) / 100).ToString();
            }
            else
            {
                txtstax6.Text = "";
            }
        }
        else
        {
            Calendar7.Text = "";
            txtUnitPrice6.Text = "";
            txttrendDisc6.Text = "";
            txtstax6.Text = "";
        }
    }
    protected void ddlBatch7_SelectedIndexChanged(object sender, EventArgs e)
    {
        DataTable meddata = theHelper.getPurMedicineRec(ddlMfg7.SelectedValue, ddlMedi7.SelectedValue, ddlBatch7.SelectedValue);
        if (meddata.Rows.Count > 0)
        {
            Calendar8.Text = meddata.Rows[0]["expiredate"].ToString();
            txtUnitPrice7.Text = meddata.Rows[0]["unitprice"].ToString();
            if (txtlessper.Text != "")
            {
                txttrendDisc7.Text = ((Convert.ToDouble(txtlessper.Text) * Convert.ToDouble(meddata.Rows[0]["unitprice"].ToString())) / 100).ToString();
            }
            else
            {
                txttrendDisc7.Text = "";
            }
            if (txtTaxper.Text != "")
            {
                txtstax7.Text = ((Convert.ToDouble(txtTaxper.Text) * Convert.ToDouble(meddata.Rows[0]["unitprice"].ToString())) / 100).ToString();
            }
            else
            {
                txtstax7.Text = "";
            }
        }
        else
        {
            Calendar8.Text = "";
            txtUnitPrice7.Text = "";
            txttrendDisc7.Text = "";
            txtstax7.Text = "";
        }
    }
    protected void ddlBatch8_SelectedIndexChanged(object sender, EventArgs e)
    {
        DataTable meddata = theHelper.getPurMedicineRec(ddlMfg8.SelectedValue, ddlMedi8.SelectedValue, ddlBatch8.SelectedValue);
        if (meddata.Rows.Count > 0)
        {
            Calendar9.Text = meddata.Rows[0]["expiredate"].ToString();
            txtUnitPrice8.Text = meddata.Rows[0]["unitprice"].ToString();
            if (txtlessper.Text != "")
            {
                txttrendDisc8.Text = ((Convert.ToDouble(txtlessper.Text) * Convert.ToDouble(meddata.Rows[0]["unitprice"].ToString())) / 100).ToString();
            }
            else
            {
                txttrendDisc8.Text = "";
            }
            if (txtTaxper.Text != "")
            {
                txtstax8.Text = ((Convert.ToDouble(txtTaxper.Text) * Convert.ToDouble(meddata.Rows[0]["unitprice"].ToString())) / 100).ToString();
            }
            else
            {
                txtstax8.Text = "";
            }
        }
        else
        {
            Calendar9.Text = "";
            txtUnitPrice8.Text = "";
            txttrendDisc8.Text = "";
            txtstax8.Text = "";
        }
    }
    protected void ddlBatch9_SelectedIndexChanged(object sender, EventArgs e)
    {
        DataTable meddata = theHelper.getPurMedicineRec(ddlMfg9.SelectedValue, ddlMedi9.SelectedValue, ddlBatch9.SelectedValue);
        if (meddata.Rows.Count > 0)
        {
            Calendar10.Text = meddata.Rows[0]["expiredate"].ToString();
            txtUnitPrice9.Text = meddata.Rows[0]["unitprice"].ToString();

            if (txtlessper.Text != "")
            {
                txttrendDisc9.Text = ((Convert.ToDouble(txtlessper.Text) * Convert.ToDouble(meddata.Rows[0]["unitprice"].ToString())) / 100).ToString();
            }
            else
            {
                txttrendDisc9.Text = "";
            }
            if (txtTaxper.Text != "")
            {
                txtstax9.Text = ((Convert.ToDouble(txtTaxper.Text) * Convert.ToDouble(meddata.Rows[0]["unitprice"].ToString())) / 100).ToString();
            }
            else
            {
                txtstax9.Text = "";
            }
        }
        else
        {
            Calendar10.Text = "";
            txtUnitPrice9.Text = "";
            txttrendDisc9.Text = "";
            txtstax9.Text = "";
        }
    }
    protected void ddlBatch10_SelectedIndexChanged(object sender, EventArgs e)
    {
        DataTable meddata = theHelper.getPurMedicineRec(ddlMfg10.SelectedValue, ddlMedi10.SelectedValue, ddlBatch10.SelectedValue);
        if (meddata.Rows.Count > 0)
        {
            Calendar11.Text = meddata.Rows[0]["expiredate"].ToString();
            txtUnitPrice10.Text = meddata.Rows[0]["unitprice"].ToString();
            if (txtlessper.Text != "")
            {
                txttrendDisc10.Text = ((Convert.ToDouble(txtlessper.Text) * Convert.ToDouble(meddata.Rows[0]["unitprice"].ToString())) / 100).ToString();
            }
            else
            {
                txttrendDisc10.Text = "";
            }
            if (txtTaxper.Text != "")
            {
                txtstax10.Text = ((Convert.ToDouble(txtTaxper.Text) * Convert.ToDouble(meddata.Rows[0]["unitprice"].ToString())) / 100).ToString();
            }
            else
            {
                txtstax10.Text = "";
            }
        }
        else
        {
            Calendar11.Text = "";
            txtUnitPrice10.Text = "";
            txttrendDisc10.Text = "";
            txtstax10.Text = "";
        }
    }
    protected void ddlBatch11_SelectedIndexChanged(object sender, EventArgs e)
    {
        DataTable meddata = theHelper.getPurMedicineRec(ddlMfg11.SelectedValue, ddlMedi11.SelectedValue, ddlBatch11.SelectedValue);
        if (meddata.Rows.Count > 0)
        {
            Calendar12.Text = meddata.Rows[0]["expiredate"].ToString();
            txtUnitPrice11.Text = meddata.Rows[0]["unitprice"].ToString();

            if (txtlessper.Text != "")
            {
                txttrendDisc11.Text = ((Convert.ToDouble(txtlessper.Text) * Convert.ToDouble(meddata.Rows[0]["unitprice"].ToString())) / 100).ToString();
            }
            else
            {
                txttrendDisc11.Text = "";
            }
            if (txtTaxper.Text != "")
            {
                txtstax11.Text = ((Convert.ToDouble(txtTaxper.Text) * Convert.ToDouble(meddata.Rows[0]["unitprice"].ToString())) / 100).ToString();
            }
            else
            {
                txtstax11.Text = "";
            }
        }
        else
        {
            Calendar12.Text = "";
            txtUnitPrice11.Text = "";
            txttrendDisc11.Text = "";
            txtstax11.Text = "";
        }
    }
    protected void ddlBatch12_SelectedIndexChanged(object sender, EventArgs e)
    {
        DataTable meddata = theHelper.getPurMedicineRec(ddlMfg12.SelectedValue, ddlMedi12.SelectedValue, ddlBatch12.SelectedValue);
        if (meddata.Rows.Count > 0)
        {
            Calendar13.Text = meddata.Rows[0]["expiredate"].ToString();
            txtUnitPrice12.Text = meddata.Rows[0]["unitprice"].ToString();

            if (txtlessper.Text != "")
            {
                txttrendDisc12.Text = ((Convert.ToDouble(txtlessper.Text) * Convert.ToDouble(meddata.Rows[0]["unitprice"].ToString())) / 100).ToString();
            }
            else
            {
                txttrendDisc12.Text = "";
            }
            if (txtTaxper.Text != "")
            {
                txtstax12.Text = ((Convert.ToDouble(txtTaxper.Text) * Convert.ToDouble(meddata.Rows[0]["unitprice"].ToString())) / 100).ToString();
            }
            else
            {
                txtstax12.Text = "";
            }
        }
        else
        {
            Calendar13.Text = "";
            txtUnitPrice12.Text = "";
            txttrendDisc12.Text = "";
            txtstax12.Text = "";
        }
    }


    protected void txtUnitPrice1_TextChanged(object sender, EventArgs e)
    {
        Double totAmt = calculateTotAmt(txtQty1.Text, txtUnitPrice1.Text);
        if (totAmt > 0.00)
        {
            txtTotalPrice1.Text = totAmt.ToString();
        }
        else
        {
            txtTotalPrice1.Text = "";
        }
    }
    protected void txtUnitPrice2_TextChanged(object sender, EventArgs e)
    {
        Double totAmt = calculateTotAmt(txtQty2.Text, txtUnitPrice2.Text);
        if (totAmt > 0.00)
        {
            txtTotalPrice2.Text = totAmt.ToString();
        }
        else
        {
            txtTotalPrice2.Text = "";
        }
    }

    protected void txtUnitPrice3_TextChanged(object sender, EventArgs e)
    {
        Double totAmt = calculateTotAmt(txtQty3.Text, txtUnitPrice3.Text);
        if (totAmt > 0.00)
        {
            txtTotalPrice3.Text = totAmt.ToString();
        }
        else
        {
            txtTotalPrice3.Text = "";
        }
    }

    protected void txtUnitPrice4_TextChanged(object sender, EventArgs e)
    {
        Double totAmt = calculateTotAmt(txtQty4.Text, txtUnitPrice4.Text);
        if (totAmt > 0.00)
        {
            txtTotalPrice4.Text = totAmt.ToString();
        }
        else
        {
            txtTotalPrice4.Text = "";
        }
    }
    protected void txtUnitPrice5_TextChanged(object sender, EventArgs e)
    {
        Double totAmt = calculateTotAmt(txtQty5.Text, txtUnitPrice5.Text);
        if (totAmt > 0.00)
        {
            txtTotalPrice5.Text = totAmt.ToString();
        }
        else
        {
            txtTotalPrice5.Text = "";
        }
    }
    protected void txtUnitPrice6_TextChanged(object sender, EventArgs e)
    {
        Double totAmt = calculateTotAmt(txtQty6.Text, txtUnitPrice6.Text);
        if (totAmt > 0.00)
        {
            txtTotalPrice6.Text = totAmt.ToString();
        }
        else
        {
            txtTotalPrice6.Text = "";
        }
    }

    protected void txtUnitPrice7_TextChanged(object sender, EventArgs e)
    {
        Double totAmt = calculateTotAmt(txtQty7.Text, txtUnitPrice7.Text);
        if (totAmt > 0.00)
        {
            txtTotalPrice7.Text = totAmt.ToString();
        }
        else
        {
            txtTotalPrice7.Text = "";
        }
    }

    protected void txtUnitPrice8_TextChanged(object sender, EventArgs e)
    {
        Double totAmt = calculateTotAmt(txtQty8.Text, txtUnitPrice8.Text);
        if (totAmt > 0.00)
        {
            txtTotalPrice8.Text = totAmt.ToString();
        }
        else
        {
            txtTotalPrice8.Text = "";
        }
    }

    protected void txtUnitPrice9_TextChanged(object sender, EventArgs e)
    {
        Double totAmt = calculateTotAmt(txtQty9.Text, txtUnitPrice9.Text);
        if (totAmt > 0.00)
        {
            txtTotalPrice9.Text = totAmt.ToString();
        }
        else
        {
            txtTotalPrice9.Text = "";
        }
    }

    protected void txtUnitPrice10_TextChanged(object sender, EventArgs e)
    {
        Double totAmt = calculateTotAmt(txtQty10.Text, txtUnitPrice10.Text);
        if (totAmt > 0.00)
        {
            txtTotalPrice10.Text = totAmt.ToString();
        }
        else
        {
            txtTotalPrice10.Text = "";
        }
    }

    protected void txtUnitPrice11_TextChanged(object sender, EventArgs e)
    {
        Double totAmt = calculateTotAmt(txtQty11.Text, txtUnitPrice11.Text);
        if (totAmt > 0.00)
        {
            txtTotalPrice11.Text = totAmt.ToString();
        }
        else
        {
            txtTotalPrice11.Text = "";
        }
    }

    protected void txtUnitPrice12_TextChanged(object sender, EventArgs e)
    {
        Double totAmt = calculateTotAmt(txtQty12.Text, txtUnitPrice12.Text);
        if (totAmt > 0.00)
        {
            txtTotalPrice12.Text = totAmt.ToString();
        }
        else
        {
            txtTotalPrice12.Text = "";
        }
    }



    protected void txttrendDisc1_TextChanged(object sender, EventArgs e)
    {
        Double totAmt = calculateTotAmt(txtQty1.Text, txtUnitPrice1.Text);
        if (totAmt > 0.00)
        {
            txtTotalPrice1.Text = totAmt.ToString();
        }
        else
        {
            txtTotalPrice1.Text = "";
        }
    }
    protected void txttrendDisc2_TextChanged(object sender, EventArgs e)
    {
        Double totAmt = calculateTotAmt(txtQty2.Text, txtUnitPrice2.Text);
        if (totAmt > 0.00)
        {
            txtTotalPrice2.Text = totAmt.ToString();
        }
        else
        {
            txtTotalPrice2.Text = "";
        }
    }

    protected void txttrendDisc3_TextChanged(object sender, EventArgs e)
    {
        Double totAmt = calculateTotAmt(txtQty3.Text, txtUnitPrice3.Text);
        if (totAmt > 0.00)
        {
            txtTotalPrice3.Text = totAmt.ToString();
        }
        else
        {
            txtTotalPrice3.Text = "";
        }
    }

    protected void txttrendDisc4_TextChanged(object sender, EventArgs e)
    {
        Double totAmt = calculateTotAmt(txtQty4.Text, txtUnitPrice4.Text);
        if (totAmt > 0.00)
        {
            txtTotalPrice4.Text = totAmt.ToString();
        }
        else
        {
            txtTotalPrice4.Text = "";
        }
    }
    protected void txttrendDisc5_TextChanged(object sender, EventArgs e)
    {
        Double totAmt = calculateTotAmt(txtQty5.Text, txtUnitPrice5.Text);
        if (totAmt > 0.00)
        {
            txtTotalPrice5.Text = totAmt.ToString();
        }
        else
        {
            txtTotalPrice5.Text = "";
        }
    }
    protected void txttrendDisc6_TextChanged(object sender, EventArgs e)
    {
        Double totAmt = calculateTotAmt(txtQty6.Text, txtUnitPrice6.Text);
        if (totAmt > 0.00)
        {
            txtTotalPrice6.Text = totAmt.ToString();
        }
        else
        {
            txtTotalPrice6.Text = "";
        }
    }

    protected void txttrendDisc7_TextChanged(object sender, EventArgs e)
    {
        Double totAmt = calculateTotAmt(txtQty7.Text, txtUnitPrice7.Text);
        if (totAmt > 0.00)
        {
            txtTotalPrice7.Text = totAmt.ToString();
        }
        else
        {
            txtTotalPrice7.Text = "";
        }
    }

    protected void txttrendDisc8_TextChanged(object sender, EventArgs e)
    {
        Double totAmt = calculateTotAmt(txtQty8.Text, txtUnitPrice8.Text);
        if (totAmt > 0.00)
        {
            txtTotalPrice8.Text = totAmt.ToString();
        }
        else
        {
            txtTotalPrice8.Text = "";
        }
    }

    protected void txttrendDisc9_TextChanged(object sender, EventArgs e)
    {
        Double totAmt = calculateTotAmt(txtQty9.Text, txtUnitPrice9.Text);
        if (totAmt > 0.00)
        {
            txtTotalPrice9.Text = totAmt.ToString();
        }
        else
        {
            txtTotalPrice9.Text = "";
        }
    }

    protected void txttrendDisc10_TextChanged(object sender, EventArgs e)
    {
        Double totAmt = calculateTotAmt(txtQty10.Text, txtUnitPrice10.Text);
        if (totAmt > 0.00)
        {
            txtTotalPrice10.Text = totAmt.ToString();
        }
        else
        {
            txtTotalPrice10.Text = "";
        }
    }

    protected void txttrendDisc11_TextChanged(object sender, EventArgs e)
    {
        Double totAmt = calculateTotAmt(txtQty11.Text, txtUnitPrice11.Text);
        if (totAmt > 0.00)
        {
            txtTotalPrice11.Text = totAmt.ToString();
        }
        else
        {
            txtTotalPrice11.Text = "";
        }
    }

    protected void txttrendDisc12_TextChanged(object sender, EventArgs e)
    {
        Double totAmt = calculateTotAmt(txtQty12.Text, txtUnitPrice12.Text);
        if (totAmt > 0.00)
        {
            txtTotalPrice12.Text = totAmt.ToString();
        }
        else
        {
            txtTotalPrice12.Text = "";
        }
    }


    protected void txtstax1_TextChanged(object sender, EventArgs e)
    {
        Double totAmt = calculateTotAmt(txtQty1.Text, txtUnitPrice1.Text);
        if (totAmt > 0.00)
        {
            txtTotalPrice1.Text = totAmt.ToString();
        }
        else
        {
            txtTotalPrice1.Text = "";
        }
    }
    protected void txtstax2_TextChanged(object sender, EventArgs e)
    {
        Double totAmt = calculateTotAmt(txtQty2.Text, txtUnitPrice2.Text);
        if (totAmt > 0.00)
        {
            txtTotalPrice2.Text = totAmt.ToString();
        }
        else
        {
            txtTotalPrice2.Text = "";
        }
    }

    protected void txtstax3_TextChanged(object sender, EventArgs e)
    {
        Double totAmt = calculateTotAmt(txtQty3.Text, txtUnitPrice3.Text);
        if (totAmt > 0.00)
        {
            txtTotalPrice3.Text = totAmt.ToString();
        }
        else
        {
            txtTotalPrice3.Text = "";
        }
    }

    protected void txtstax4_TextChanged(object sender, EventArgs e)
    {
        Double totAmt = calculateTotAmt(txtQty4.Text, txtUnitPrice4.Text);
        if (totAmt > 0.00)
        {
            txtTotalPrice4.Text = totAmt.ToString();
        }
        else
        {
            txtTotalPrice4.Text = "";
        }
    }
    protected void txtstax5_TextChanged(object sender, EventArgs e)
    {
        Double totAmt = calculateTotAmt(txtQty5.Text, txtUnitPrice5.Text);
        if (totAmt > 0.00)
        {
            txtTotalPrice5.Text = totAmt.ToString();
        }
        else
        {
            txtTotalPrice5.Text = "";
        }
    }
    protected void txtstax6_TextChanged(object sender, EventArgs e)
    {
        Double totAmt = calculateTotAmt(txtQty6.Text, txtUnitPrice6.Text);
        if (totAmt > 0.00)
        {
            txtTotalPrice6.Text = totAmt.ToString();
        }
        else
        {
            txtTotalPrice6.Text = "";
        }
    }

    protected void txtstax7_TextChanged(object sender, EventArgs e)
    {
        Double totAmt = calculateTotAmt(txtQty7.Text, txtUnitPrice7.Text);
        if (totAmt > 0.00)
        {
            txtTotalPrice7.Text = totAmt.ToString();
        }
        else
        {
            txtTotalPrice7.Text = "";
        }
    }

    protected void txtstax8_TextChanged(object sender, EventArgs e)
    {
        Double totAmt = calculateTotAmt(txtQty8.Text, txtUnitPrice8.Text);
        if (totAmt > 0.00)
        {
            txtTotalPrice8.Text = totAmt.ToString();
        }
        else
        {
            txtTotalPrice8.Text = "";
        }
    }

    protected void txtstax9_TextChanged(object sender, EventArgs e)
    {
        Double totAmt = calculateTotAmt(txtQty9.Text, txtUnitPrice9.Text);
        if (totAmt > 0.00)
        {
            txtTotalPrice9.Text = totAmt.ToString();
        }
        else
        {
            txtTotalPrice9.Text = "";
        }
    }

    protected void txtstax10_TextChanged(object sender, EventArgs e)
    {
        Double totAmt = calculateTotAmt(txtQty10.Text, txtUnitPrice10.Text);
        if (totAmt > 0.00)
        {
            txtTotalPrice10.Text = totAmt.ToString();
        }
        else
        {
            txtTotalPrice10.Text = "";
        }
    }

    protected void txtstax11_TextChanged(object sender, EventArgs e)
    {
        Double totAmt = calculateTotAmt(txtQty11.Text, txtUnitPrice11.Text);
        if (totAmt > 0.00)
        {
            txtTotalPrice11.Text = totAmt.ToString();
        }
        else
        {
            txtTotalPrice11.Text = "";
        }
    }

    protected void txtstax12_TextChanged(object sender, EventArgs e)
    {
        Double totAmt = calculateTotAmt(txtQty12.Text, txtUnitPrice12.Text);
        if (totAmt > 0.00)
        {
            txtTotalPrice12.Text = totAmt.ToString();
        }
        else
        {
            txtTotalPrice12.Text = "";
        }
    }
    protected Double calcTotAmt(string quantity, string trendprice, string discount, string sertax)
    {
        int qty = 0;
        Double trndprc = 0.00;
        Double disc = 0.00;
        Double tax = 0.00;
        Double totamt=0.00;
        if(quantity!="")
        {
            qty = Convert.ToInt32(quantity);
        }
        if (trendprice != "")
        {
            trndprc = Convert.ToDouble(trendprice);
        }
        else
        {
            trndprc = 0.00;
        }
        if (discount != "")
        {
            disc = Convert.ToDouble(discount);

        }
        else
        {
            disc = 0.00;
        }
        if (sertax != "")
        {
            tax = Convert.ToDouble(sertax);
        }
        else
        {
            tax = 0.00;
        }

        totamt = (trndprc * qty) + tax - disc;
        return totamt;
    }


    private void MedicineGroupFill(string grp, DropDownList ddlMedGrp)
    {
        /* DropDownList ddlMediGrp = (DropDownList)Page.FindControl("ctl00$ContentPlaceHolder1$ddlMediGrp" + i.ToString());
        DropDownList ddlMediSubGrp = (DropDownList)Page.FindControl("ctl00$ContentPlaceHolder1$ddlMediSubGrp" + i.ToString());
        DropDownList ddlMedi = (DropDownList)Page.FindControl("ctl00$ContentPlaceHolder1$ddlMedi" + i.ToString());

       if (MCode == "0")
        {
            ddlMediGrp.SelectedIndex = 0;
            ddlMediGrp.Enabled = false;

            ddlMediSubGrp.SelectedIndex = 0;
            ddlMediSubGrp.Enabled = false;

            ddlMedi.SelectedIndex = 0;
            ddlMedi.Enabled = false;
        }
        else
        {
            ddlMediGrp.Items.Clear();
            ddlMediGrp.DataSource = theHelper.DropdownID3();
            ddlMediGrp.DataTextField = "MedicineGroupName";
            ddlMediGrp.DataValueField = "MedicineGroupID";
            ddlMediGrp.DataBind();
            ddlMediGrp.Items.Insert(0, new ListItem("--Select--", "0"));

            ddlMediGrp.Enabled = true;
        }*/

        ddlMedGrp.Items.Clear();
        ddlMedGrp.DataSource = theHelper.GetMedGrpByID(grp);
        ddlMedGrp.DataTextField = "MedicineGroupName";
        ddlMedGrp.DataValueField = "MedicineGroupID";
        ddlMedGrp.DataBind();
        ddlMedGrp.Items.Insert(0, new ListItem("--Select--", "0"));
    }

    protected void perc_textchange(object sender, EventArgs e)
    {
        Double lessper = 0.00;
        Double trendprice = 0.00;
        Double lessAmt, taxAmt, totAmt;
        TextBox t1, t2,t3,s1, s2, s3;
        string trndprc, qty;
        if (txtlessper.Text != "")
        {
            lessper = Convert.ToDouble(txtlessper.Text);
        }
        if (txtTaxper.Text != "")
        {
            trendprice = Convert.ToDouble(txtTaxper.Text);
        }

        for (int i = 0, t = 1; i < 12; i++, t++)
        {
            t1 = (TextBox)Page.FindControl("ctl00$ContentPlaceHolder1$txttrendDisc" + t.ToString());
            t2 = (TextBox)Page.FindControl("ctl00$ContentPlaceHolder1$txtstax" + t.ToString());
            t3 = (TextBox)Page.FindControl("ctl00$ContentPlaceHolder1$txtTotalPrice" + t.ToString());


            s1 = (TextBox)Page.FindControl("ctl00$ContentPlaceHolder1$txtUnitPrice" + t.ToString());
            s2 = (TextBox)Page.FindControl("ctl00$ContentPlaceHolder1$txtQty" + t.ToString());
           

            trndprc = s1.Text;
            qty = s2.Text;
            

            lessAmt = calcLess(trndprc);
            taxAmt = calStax(trndprc);
            totAmt = calculateTotAmt(trndprc, qty);
           
            if (lessAmt > 0.00)
            {
                t1.Text = lessAmt.ToString();
            }
            else
            {
                t1.Text = "";
            }
            if (taxAmt > 0.00)
            {
                t2.Text = taxAmt.ToString();
            }
            else
            {
                t2.Text = "";
            }
            
            if (totAmt > 0.00)
            {
                t3.Text = totAmt.ToString();
            }
            else
            {
                t3.Text = "";
            }
        }

    }
    private Double calculateTotAmt(string trndprice, string qty)
    {
        Double trendprice = 0.00;
        Double quantity = 0.00;
        Double amt = 0.00;
        Double lessper = 0.00;
        Double tax = 0.00;
        Double less = 0.00;
        Double txamt = 0.00;
        if (trndprice != "")
        {
            trendprice = Convert.ToDouble(trndprice);
        }
        if (qty != "")
        {
            quantity = Convert.ToDouble(qty);
        }
        if (txtlessper.Text != "")
        {
            lessper = Convert.ToDouble(txtlessper.Text);
        }
        if (txtTaxper.Text != "")
        {
            tax = Convert.ToDouble(txtTaxper.Text);
        }
        txamt = (tax * trendprice) / 100;
        less = (lessper * trendprice) / 100;
        if (qty != "")
        {
            amt = (quantity * trendprice) + txamt - less;
        }
        else
        {
            amt = 0.00;
        }
        return amt;

    }

    private Double calStax(string trndprc)
    {
        Double trendprice = 0.00;
        Double tax = 0.00;
        if (trndprc != "")
        {
            trendprice = Convert.ToDouble(trndprc);
        }
        if (txtTaxper.Text != "")
        {
            tax = Convert.ToDouble(txtTaxper.Text);
        }
        return (tax * trendprice) / 100;
    }


    private Double calcLess(string trndprc)
    {
        Double lessper = 0.00;
        Double trendprice = 0.00;

        if (txtlessper.Text != "")
        {
            lessper = Convert.ToDouble(txtlessper.Text);
        }
        if (trndprc != "")
        {
            trendprice = Convert.ToDouble(trndprc);
        }
        return (lessper * trendprice) / 100;
    }

    private void MfgGrpSgrpFill(string MedCode, int i)
    {
        DropDownList ddlMfg = (DropDownList)Page.FindControl("ctl00$ContentPlaceHolder1$ddlMfg" + i.ToString());
        DropDownList ddlMediGrp = (DropDownList)Page.FindControl("ctl00$ContentPlaceHolder1$ddlMediGrp" + i.ToString());
        DropDownList ddlMediSubGrp = (DropDownList)Page.FindControl("ctl00$ContentPlaceHolder1$ddlMediSubGrp" + i.ToString());

        if (MedCode == "0")
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
            ddlMfg.DataSource = theHelper.DropdownMfg(MedCode);
            ddlMfg.DataTextField = "MName";
            ddlMfg.DataValueField = "MCode";
            ddlMfg.DataBind();


            ddlMediGrp.Items.Clear();
            ddlMediGrp.DataSource = theHelper.DropdownGrp(MedCode);
            ddlMediGrp.DataTextField = "MedicineGroupName";
            ddlMediGrp.DataValueField = "MedicineGroupID";
            ddlMediGrp.DataBind();
            // ddlMediGrp.Items.Insert(0, new ListItem("--Select--", "0"));

            ddlMediSubGrp.Items.Clear();
            ddlMediSubGrp.DataSource = theHelper.DropdownSubGrp(MedCode);
            ddlMediSubGrp.DataTextField = "SubGrName";
            ddlMediSubGrp.DataValueField = "ID";
            ddlMediSubGrp.DataBind();
            // ddlMediSubGrp.Items.Insert(0, new ListItem("--Select--", "0"));

        }
    }

    protected void ddl2_selecttedIndexChanged(object sender, EventArgs e)
    {
        DropDownFill();
    }

}