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
 
public partial class Master_PurchaseMedicine : System.Web.UI.Page
{
    Main Objmain = new Main(ConfigurationSettings.AppSettings["ConnectionString"].ToString());
    MD_PurchaseMedicine theHelper = new MD_PurchaseMedicine(ConfigurationSettings.AppSettings["ConnectionString"].ToString());
    protected void Page_Load(object sender, EventArgs e)
    {
        Page.Title = "Purchase Medicine";
        if (Session["userName"] == null)
        {
            Response.Redirect("../LoginPage.aspx");
        }
        if (Objmain.checkaccss(Session["CoCode"].ToString(), Session["UserRoleID"].ToString().Trim(), "MEDICINE PURCHASE", checkAccessType.ViewAction) == false)
        {
            Response.Redirect("../AccessDenied.aspx");
        }

        if (Objmain.checkaccss(Session["CoCode"].ToString(), Session["UserRoleID"].ToString().Trim(), "MEDICINE PURCHASE", checkAccessType.InsertAction) == false)
        {
            Button1.Enabled = false;
        }
        if (!IsPostBack)
        {
            DropDownList2.Items.Insert(0, new ListItem("Medicine", "M"));
            DropDownList2.Items.Insert(1, new ListItem("Reagent", "G"));
            DropDownList2.Items.Insert(2, new ListItem("Other", "O"));
            DropDownFill();
            GenerateCode();
            
            Session["back_to_purchase"] = "";
            if (Session["Pur_medid"] != null)
            {
                Fill_meddata();
            }

            if (Session["zm_docno"] != null)
            {
                if (Session["zm_docno"].ToString() != "")
                {
                    txtPurchaseMedicineId.Text = Session["zm_docno"].ToString();
                    HiddenField1.Value = txtPurchaseMedicineId.Text;
                    PageDataBind();
                    Button1.Visible = false; Button2.Visible = false; Button3.Visible = false; Button4.Visible = false; Button5.Visible = false;
                }
            }
        }
    }
    public void Fill_meddata()
    {
        DropDownFill();
        string Pur_medid = Session["Pur_medid"].ToString();
        DataTable meddt = theHelper.getmedicineDetailInfo(Session["CoCode"].ToString().Trim(), Session["YearCode"].ToString().Trim(), Pur_medid);

        //DropDownList1.SelectedValue = meddt.Rows[0]["SCode"].ToString();
        //DropDownList2.SelectedValue = meddt.Rows[0]["itype"].ToString().Trim();

        DdlMedicineIDBind(ddlMedi1);
        ddlMedi1.SelectedValue = meddt.Rows[0]["MedicineID"].ToString();
        Session["Pur_medid"] = null;
        //DdlIDMfgFill(meddt.Rows[0]["MedicineID"].ToString(), ddlMfg1);
        //ddlMfg1.SelectedValue = meddt.Rows[0]["MCode"].ToString();

        //MedicineGroupFill(meddt.Rows[0]["MedicineID"].ToString(), ddlMediGrp1);
        //ddlMediGrp1.SelectedValue = meddt.Rows[0]["MedicineGroupID"].ToString();

        //DdlMedicineSubGroupBind(meddt.Rows[0]["MedicineID"].ToString(), ddlMediSubGrp1);
        //ddlMediSubGrp1.SelectedValue = meddt.Rows[0]["SubGroupid"].ToString();
        
    }
    public void GenerateCode()
    {
        DataTable dt = theHelper.GetPurchaseMedicineID(Session["CoCode"].ToString().Trim(), Session["YearCode"].ToString().Trim());
        txtPurchaseMedicineId.Text = dt.Rows[0][0].ToString();
    }

    [System.Web.Script.Services.ScriptMethod()]
    [System.Web.Services.WebMethod]

    public static List<string> SearchMedicine(string prefixText, int count)
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
                cmd.CommandText = "SELECT mm.MName+' ## '+mg.MedicineGroupName+' ## '+sg.SubGrName+ ' ## '+m.MedicineName Name FROM PH_ManufactureMaster mm,IPD_MedicineGroup mg,IPD_MedicineSubGroup sg,IPD_MedicineMaster m WHERE mg.compcode=m.compcode and sg.compcode=mg.compcode and mm.compcode=m.compcode and mg.MedicineGroupID=sg.GroupID AND sg.ID=m.SubGroupid AND m.MCode=mm.MCode and m.compcode=@Compcode and m.MedicineName like @SearchText + '%'";
                cmd.Parameters.AddWithValue("@SearchText", prefixText);
                cmd.Parameters.AddWithValue("@Compcode", compcode);
                cmd.Parameters.AddWithValue("@Yaercode", yearcode);
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

    public static List<string> SearchSubGroup(string prefixText, int count)
    {
        using (SqlConnection conn = new SqlConnection())
        {
            conn.ConnectionString = ConfigurationSettings.AppSettings["ConnectionString"].ToString();
            using (SqlCommand cmd = new SqlCommand())
            {
                cmd.CommandText = "SELECT mm.MName+' ## '+mg.MedicineGroupName+' ## '+sg.SubGrName+ ' ## '+m.MedicineName Name FROM PH_ManufactureMaster mm,IPD_MedicineGroup mg,IPD_MedicineSubGroup sg,IPD_MedicineMaster m WHERE mg.MedicineGroupID=sg.GroupID AND sg.ID=m.SubGroupid AND m.MCode=mm.MCode and  sg.SubGrName like @SearchText + '%'";
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




    private void PageDataBind()
    {
        try
        {
            DropDownFill();
            TextBox t1, t2, t3, t4, t5,t6,t7,t8,t9,t10,t11,t12,t13,t14,t15,t16,t17,t18,t19,t20,t21; DropDownList d1, d2, d3,d4,d5,d6;
            DataTable dtPurchaseMedicine = theHelper.GetPurchaseMedicineDetails(Session["CoCode"].ToString().Trim(), Session["YearCode"].ToString().Trim(), txtPurchaseMedicineId.Text);
            if (dtPurchaseMedicine.Rows.Count > 0)
            {

                DropDownList2.SelectedValue = dtPurchaseMedicine.Rows[0]["itype"].ToString().Trim();
                DropDownList1.SelectedValue = dtPurchaseMedicine.Rows[0]["SCode"].ToString().Trim();
                Calendar1.Text = dtPurchaseMedicine.Rows[0]["purdate"].ToString();
                txtBillNo.Text = dtPurchaseMedicine.Rows[0]["BillNo"].ToString();
                txtlessper.Text = dtPurchaseMedicine.Rows[0]["LessPercent"].ToString();
                txtTaxper.Text = dtPurchaseMedicine.Rows[0]["TaxPercent"].ToString();
                txtgross.Text = dtPurchaseMedicine.Rows[0]["grossvalue"].ToString();
                txtNetAmt.Text = dtPurchaseMedicine.Rows[0]["Total"].ToString();
                txtRoundOff.Text = dtPurchaseMedicine.Rows[0]["roundOff"].ToString();
                ddlFrom.SelectedValue = dtPurchaseMedicine.Rows[0]["fromloc"].ToString();
                ddlTo.SelectedValue = dtPurchaseMedicine.Rows[0]["toloc"].ToString();
                for (int i = 0, t = 1; i < dtPurchaseMedicine.Rows.Count; i++, t++)
                {
                    t1 = (TextBox)Page.FindControl("ctl00$ContentPlaceHolder1$txtBatch" + t.ToString());
                    //t2 = (TextBox)Page.FindControl("ctl00$ContentPlaceHolder1$Calendar" + (t + 1).ToString());
                    t3 = (TextBox)Page.FindControl("ctl00$ContentPlaceHolder1$txtQty" + t.ToString());
                    t4 = (TextBox)Page.FindControl("ctl00$ContentPlaceHolder1$txtUnitPrice" + t.ToString());
                    t5 = (TextBox)Page.FindControl("ctl00$ContentPlaceHolder1$txtTotalPrice" + t.ToString());
                    t6 = (TextBox)Page.FindControl("ctl00$ContentPlaceHolder1$txtSellPrice" + t.ToString());
                    t7 = (TextBox)Page.FindControl("ctl00$ContentPlaceHolder1$txtfreeqty" + t.ToString());
                    t8 = (TextBox)Page.FindControl("ctl00$ContentPlaceHolder1$txtLess" + t.ToString());
                    t9 = (TextBox)Page.FindControl("ctl00$ContentPlaceHolder1$txtStax" + t.ToString());
                    t10 = (TextBox)Page.FindControl("ctl00$ContentPlaceHolder1$txtTotQty" + t.ToString());
                    t11 = (TextBox)Page.FindControl("ctl00$ContentPlaceHolder1$txtCostPrice" + t.ToString());
                    t12 = (TextBox)Page.FindControl("ctl00$ContentPlaceHolder1$txtLessper" + t.ToString());
                    t13 = (TextBox)Page.FindControl("ctl00$ContentPlaceHolder1$txtStaxper" + t.ToString());
                    t14 = (TextBox)Page.FindControl("ctl00$ContentPlaceHolder1$txtHsnCode" + t.ToString());
                    t15 = (TextBox)Page.FindControl("ctl00$ContentPlaceHolder1$txtCgstRt" + t.ToString());
                    t16 = (TextBox)Page.FindControl("ctl00$ContentPlaceHolder1$txtCgstAmt" + t.ToString());
                    t17 = (TextBox)Page.FindControl("ctl00$ContentPlaceHolder1$txtSgstRt" + t.ToString());
                    t18 = (TextBox)Page.FindControl("ctl00$ContentPlaceHolder1$txtSgstAmt" + t.ToString());
                    t19 = (TextBox)Page.FindControl("ctl00$ContentPlaceHolder1$txtIgstRt" + t.ToString());
                    t20 = (TextBox)Page.FindControl("ctl00$ContentPlaceHolder1$txtIgstAmt" + t.ToString());
                    t21 = (TextBox)Page.FindControl("ctl00$ContentPlaceHolder1$txtTotalPricewithouttax" + t.ToString());

                    d1 = (DropDownList)Page.FindControl("ctl00$ContentPlaceHolder1$ddlMedi" + t.ToString());
                    d2 = (DropDownList)Page.FindControl("ctl00$ContentPlaceHolder1$ddlMfg" + t.ToString());
                    d3 = (DropDownList)Page.FindControl("ctl00$ContentPlaceHolder1$ddlMediGrp" + t.ToString());
                    d4 = (DropDownList)Page.FindControl("ctl00$ContentPlaceHolder1$ddlMediSubGrp" + t.ToString());
                    d5 = (DropDownList)Page.FindControl("ctl00$ContentPlaceHolder1$ddlmonth" + t.ToString());
                    d6 = (DropDownList)Page.FindControl("ctl00$ContentPlaceHolder1$ddlyear" + t.ToString());
                   

                    DdlMedicineIDBind(d1);
                    d1.SelectedValue = dtPurchaseMedicine.Rows[i]["MedicineID"].ToString();
                    d1.Enabled = true;
                    //if (dtPurchaseMedicine.Rows[i]["MCode"].ToString() != "")
                    //{
                    //    DdlIDMfgFill(dtPurchaseMedicine.Rows[i]["MedicineID"].ToString(), d2);
                    //    d2.SelectedValue = dtPurchaseMedicine.Rows[i]["MCode"].ToString();
                    //}
                    //else
                    //{
                    //    d2.Items.Clear();
                    //    d2.Items.Insert(0, new ListItem("--Select--", "0"));
                    //}

                    //if (dtPurchaseMedicine.Rows[i]["MedicineGroupID"].ToString() != "")
                    //{
                    //    MedicineGroupFill(dtPurchaseMedicine.Rows[i]["MedicineID"].ToString(), d3);
                    //    //d3.SelectedValue = dtPurchaseMedicine.Rows[i]["MedicineGroupID"].ToString();
                    //}
                    //else
                    //{
                    //    d3.Items.Clear();
                    //    d3.Items.Insert(0, new ListItem("--Select--", "0"));
                    //}

                    //if (dtPurchaseMedicine.Rows[i]["MedicineSubGrp"].ToString() != "")
                    //{
                    //    DdlMedicineSubGroupBind(dtPurchaseMedicine.Rows[i]["MedicineID"].ToString(), d4);
                    //    d4.SelectedValue = dtPurchaseMedicine.Rows[i]["MedicineSubGrp"].ToString();
                    //}
                    //else
                    //{
                    //    d4.Items.Clear();
                    //    d4.Items.Insert(0, new ListItem("--Select--", "0"));
                    //}

                    d2.Items.Clear();
                    d2.Items.Insert(0, new ListItem("--Select--", "0"));
                    d3.Items.Clear();
                    d3.Items.Insert(0, new ListItem("--Select--", "0"));
                    d4.Enabled = true;

                    
            
                  
                    t1.Text = dtPurchaseMedicine.Rows[i]["BatchNo"].ToString();
                    //t2.Text = dtPurchaseMedicine.Rows[i]["exdate"].ToString();
                    setExpYearMonth(d5, d6, dtPurchaseMedicine.Rows[i]["exdate"].ToString());
                    t3.Text = dtPurchaseMedicine.Rows[i]["Qty"].ToString();
                    t4.Text = dtPurchaseMedicine.Rows[i]["PricePerUnit"].ToString();
                    t5.Text = dtPurchaseMedicine.Rows[i]["TotalPrice"].ToString();
                    t6.Text = dtPurchaseMedicine.Rows[i]["SellPricePerUnit"].ToString();
                    t7.Text = dtPurchaseMedicine.Rows[i]["FQty"].ToString();
                    t8.Text = dtPurchaseMedicine.Rows[i]["TrendDiscount"].ToString();
                    t9.Text = dtPurchaseMedicine.Rows[i]["STax"].ToString();
                    t10.Text = dtPurchaseMedicine.Rows[i]["TotalQty"].ToString();
                    t11.Text = dtPurchaseMedicine.Rows[i]["CostPrice"].ToString();
                    t12.Text = dtPurchaseMedicine.Rows[i]["singleLess"].ToString();
                    t13.Text = dtPurchaseMedicine.Rows[i]["singleTax"].ToString();

                    t14.Text = dtPurchaseMedicine.Rows[i]["HSNCode"].ToString();
                    t15.Text = dtPurchaseMedicine.Rows[i]["CGSTRt"].ToString();
                    t16.Text = dtPurchaseMedicine.Rows[i]["CGSTAmt"].ToString();
                    t17.Text = dtPurchaseMedicine.Rows[i]["SGSTRt"].ToString();
                    t18.Text = dtPurchaseMedicine.Rows[i]["SGSTAmt"].ToString();
                    t19.Text = dtPurchaseMedicine.Rows[i]["IGSTRt"].ToString();
                    t20.Text = dtPurchaseMedicine.Rows[i]["IGSTAmt"].ToString();
                    t21.Text = dtPurchaseMedicine.Rows[i]["Billvalue"].ToString();
                     
                }
                Button1.Text = "Update";
                Button1.Enabled = true;
                //if (Objmain.checkaccss(Session["CoCode"].ToString(), Session["UserRoleID"].ToString().Trim(), "PURCHASE MEDICINE", checkAccessType.UpdateAction) == false)
                //{
                //    Button1.Enabled = false;
                //}
                //else
                //{
                //    Button1.Enabled = true;
                //}
            }
            else
            {

                txtPurchaseMedicineId.Text = theHelper.GetPurchaseMedicineID(Session["CoCode"].ToString().Trim(), Session["YearCode"].ToString().Trim()).ToString();
                Button1.Text = "SUBMIT";
            }
        }
        catch (Exception ex)
        {
            Label1.ForeColor = System.Drawing.Color.Red;
            Label1.Text = ex.Message;
        }
    }

    public void setExpYearMonth(DropDownList ddlExpMonth, DropDownList ddlExpYear, string expdate)
    {
        string[] yearmonth = expdate.Split('/'); 
        ddlExpMonth.Items.Clear();
        ddlExpMonth.Items.Insert(0, new ListItem("Jan", "01"));
        ddlExpMonth.Items.Insert(1, new ListItem("Feb", "02"));
        ddlExpMonth.Items.Insert(2, new ListItem("Mar", "03"));
        ddlExpMonth.Items.Insert(3, new ListItem("Apr", "04"));
        ddlExpMonth.Items.Insert(4, new ListItem("May", "05"));
        ddlExpMonth.Items.Insert(5, new ListItem("Jun", "06"));
        ddlExpMonth.Items.Insert(6, new ListItem("July", "07"));
        ddlExpMonth.Items.Insert(7, new ListItem("Aug", "08"));
        ddlExpMonth.Items.Insert(8, new ListItem("Sep", "09"));
        ddlExpMonth.Items.Insert(9, new ListItem("Oct", "10"));
        ddlExpMonth.Items.Insert(10, new ListItem("Nov", "11"));
        ddlExpMonth.Items.Insert(11, new ListItem("Dec", "12"));
        ddlExpMonth.SelectedValue = yearmonth[1].ToString();
        
        Int32 year = DateTime.Now.Year;
        ddlExpYear.Items.Clear();
        for (int n = 0; n <= 20; n++)
        {
            ddlExpYear.Items.Insert(n, new ListItem((year + n).ToString(), (year + n).ToString()));
        }
        ddlExpYear.SelectedValue = yearmonth[2].ToString();
    }
    private void ResetAllFields()
    {
        hdnMode.Value = "0";
        txtPurchaseMedicineId.Text = theHelper.GetPurchaseMedicineID(Session["CoCode"].ToString().Trim(), Session["YearCode"].ToString().Trim()).ToString();
        Button1.Text = "SUBMIT";
        DropDownList1.SelectedIndex = 0;
        Calendar1.Text = string.Empty;
        txtBillNo.Text = string.Empty;
        txtlessper.Text = string.Empty;
        txtTaxper.Text = string.Empty;
        TextBox1.Text = "";
        for (int i = 1; i <= 12; i++)
        {
            DropDownList ddlMfg = (DropDownList)divContent.FindControl("ddlMfg" + i.ToString());
            ddlMfg.Items.Clear();
            ddlMfg.Items.Insert(0, new ListItem("--Select--", "0"));
            DropDownList ddlMediGrp = (DropDownList)divContent.FindControl("ddlMediGrp" + i.ToString());
            ddlMediGrp.Items.Clear();
            ddlMediGrp.Items.Insert(0, new ListItem("--Select--", "0"));
           // ddlMediGrp.Enabled = false;
            DropDownList ddlMediSubGrp = (DropDownList)divContent.FindControl("ddlMediSubGrp" + i.ToString());
            ddlMediSubGrp.Items.Clear();
            ddlMediSubGrp.Items.Insert(0, new ListItem("--Select--", "0"));
            //ddlMediSubGrp.Enabled = false;
            DropDownList ddlMedi = (DropDownList)divContent.FindControl("ddlMedi" + i.ToString());
            ddlMedi.SelectedIndex = 0;
           // ddlMedi.Enabled = false;
            TextBox txtBatch = (TextBox)divContent.FindControl("txtBatch" + i.ToString());
            txtBatch.Text = string.Empty;
            //TextBox Calendar = (TextBox)divContent.FindControl("Calendar" + i.ToString());
            //Calendar.Text = string.Empty;
            DropDownList ddlmonth = (DropDownList)divContent.FindControl("ddlmonth" + i.ToString());
            ddlmonth.SelectedIndex = 0;
            DropDownList ddlyear = (DropDownList)divContent.FindControl("ddlyear" + i.ToString());
            ddlyear.SelectedIndex = 0;
            TextBox txtQty = (TextBox)divContent.FindControl("txtQty" + i.ToString());
            txtQty.Text = string.Empty;
            TextBox txtUnitPrice = (TextBox)divContent.FindControl("txtUnitPrice" + i.ToString());
            txtUnitPrice.Text = string.Empty;
            TextBox txtTotalPrice = (TextBox)divContent.FindControl("txtTotalPrice" + i.ToString());
            txtTotalPrice.Text = string.Empty;

            TextBox txtSellPrice = (TextBox)divContent.FindControl("txtSellPrice" + i.ToString());
            txtSellPrice.Text = string.Empty;

            TextBox txtfreeqty = (TextBox)divContent.FindControl("txtfreeqty" + i.ToString());
            txtfreeqty.Text = string.Empty;

            TextBox txtLess = (TextBox)divContent.FindControl("txtLess" + i.ToString());
            txtLess.Text = string.Empty;

            TextBox txtStax = (TextBox)divContent.FindControl("txtStax" + i.ToString());
            txtStax.Text = string.Empty;

            TextBox txtTotQty = (TextBox)divContent.FindControl("txtTotQty" + i.ToString());
            txtTotQty.Text = string.Empty;

            TextBox txtCostPrice = (TextBox)divContent.FindControl("txtCostPrice" + i.ToString());
            txtCostPrice.Text = string.Empty;
        }
        GenerateCode();
    }
    private void DropDownFill()
    {

        for (int i = 1; i <= 12; i++)
        {

            DropDownList ddlmfg = (DropDownList)Page.FindControl("ctl00$ContentPlaceHolder1$ddlMfg" + i.ToString());
            ddlmfg.Items.Clear();
            /*ddlmfg.DataSource = theHelper.DropdownID2(Session["CoCode"].ToString().Trim());
            ddlmfg.DataTextField = "MName";
            ddlmfg.DataValueField = "MCode";
            ddlmfg.DataBind();*/
            ddlmfg.Items.Insert(0, new ListItem("--Select--", "0"));
            

            DropDownList ddlgrp = (DropDownList)Page.FindControl("ctl00$ContentPlaceHolder1$ddlMediGrp" + i.ToString());
            ddlgrp.Items.Insert(0, new ListItem("--Select--", "0"));
            //ddlgrp.Enabled = false;

            DropDownList ddlSub = (DropDownList)Page.FindControl("ctl00$ContentPlaceHolder1$ddlMediSubGrp" + i.ToString());
            ddlSub.Items.Insert(0, new ListItem("--Select--", "0"));
           // ddlSub.Enabled = false;

            string type = DropDownList2.SelectedValue.ToString();
            
            DropDownList ddlMedicine = (DropDownList)Page.FindControl("ctl00$ContentPlaceHolder1$ddlMedi" + i.ToString());
            ddlMedicine.Items.Clear();
            /*if (type =="M")
            {*/
                ddlMedicine.DataSource = theHelper.DropdownMedicine(Session["CoCode"].ToString().Trim(),type);
                ddlMedicine.DataTextField = "MedicineName";
                ddlMedicine.DataValueField = "MedicineID";
                ddlMedicine.DataBind();
                ddlMedicine.Items.Insert(0, new ListItem("--Select--", "0"));
            /*}
            else
            {
                ddlMedicine.DataSource = theHelper.Dropdown_Reagent(Session["CoCode"].ToString().Trim());
                ddlMedicine.DataTextField = "iname";
                ddlMedicine.DataValueField = "icode";
                ddlMedicine.DataBind();
                ddlMedicine.Items.Insert(0, new ListItem("--Select--", "0"));

               
            }*/

                DropDownList ddlExpMonth = (DropDownList)Page.FindControl("ctl00$ContentPlaceHolder1$ddlmonth" + i.ToString());
                ddlExpMonth.Items.Clear();
                ddlExpMonth.Items.Insert(0, new ListItem("Jan", "01"));
                ddlExpMonth.Items.Insert(1, new ListItem("Feb", "02"));
                ddlExpMonth.Items.Insert(2, new ListItem("Mar", "03"));
                ddlExpMonth.Items.Insert(3, new ListItem("Apr", "04"));
                ddlExpMonth.Items.Insert(4, new ListItem("May", "05"));
                ddlExpMonth.Items.Insert(5, new ListItem("Jun", "06"));
                ddlExpMonth.Items.Insert(6, new ListItem("July", "07"));
                ddlExpMonth.Items.Insert(7, new ListItem("Aug", "08"));
                ddlExpMonth.Items.Insert(8, new ListItem("Sep", "09"));
                ddlExpMonth.Items.Insert(9, new ListItem("Oct", "10"));
                ddlExpMonth.Items.Insert(10, new ListItem("Nov", "11"));
                ddlExpMonth.Items.Insert(11, new ListItem("Dec", "12"));

                DropDownList ddlExpYear = (DropDownList)Page.FindControl("ctl00$ContentPlaceHolder1$ddlyear" + i.ToString());
                Int32 year = DateTime.Now.Year;
                ddlExpYear.Items.Clear();
                for (int n = 0; n <= 20; n++)
                {
                    ddlExpYear.Items.Insert(n, new ListItem((year + n).ToString(), (year + n).ToString()));
                }

        } 

         DropDownList1.Items.Clear();
         this.DropDownList1.DataSource = theHelper.DropdownID5(Session["CoCode"].ToString().Trim());
        this.DropDownList1.DataTextField = "SName";
        this.DropDownList1.DataValueField = "SCode";
        this.DropDownList1.DataBind();
        this.DropDownList1.Items.Insert(0, new ListItem("--Select--", "0"));


        ddlFrom.DataSource = theHelper.DropdownState();
        ddlFrom.DataTextField = "Names";
        ddlFrom.DataValueField = "code";
        ddlFrom.DataBind();

        ddlTo.DataSource = theHelper.DropdownState();
        ddlTo.DataTextField = "Names";
        ddlTo.DataValueField = "code";
        ddlTo.DataBind();

        if (ddlFrom.SelectedValue == ddlTo.SelectedValue)
            ddlgsttype.SelectedValue = "S";
        else ddlgsttype.SelectedValue = "I";
    }

    private void MedicineGroupFill(string medId,DropDownList ddlMedGrp)
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
            ddlMediGrp.DataSource = theHelper.DropdownID3(MCode);
            ddlMediGrp.DataTextField = "MedicineGroupName";
            ddlMediGrp.DataValueField = "MedicineGroupID";
            ddlMediGrp.DataBind();
            ddlMediGrp.Items.Insert(0, new ListItem("--Select--", "0"));

            ddlMediGrp.Enabled = true;
        }*/

        ddlMedGrp.Items.Clear();
        //ddlMedGrp.DataSource=theHelper.GetMedGrpByID(Session["CoCode"].ToString().Trim(),grp);
        DataTable dt = theHelper.GetMedGrpSubGrpMfgBymedID(Session["CoCode"].ToString().Trim(), medId);
        ddlMedGrp.DataSource = dt;
        ddlMedGrp.DataTextField = "MedicineGroupName";
        ddlMedGrp.DataValueField = "MedicineGroupID";
        ddlMedGrp.DataBind();
        ddlMedGrp.Items.Insert(0, new ListItem("--Select--", "0"));
        ddlMedGrp.SelectedValue = dt.Rows[0]["MedicineGroupID"].ToString();
    }

    private void DdlIDMfgFill(string medId, DropDownList ddlmfg)
    {
        ddlmfg.Items.Clear();
        //ddlmfg.DataSource = theHelper.GetMedMfgByMCode(Session["CoCode"].ToString().Trim(),Mcode);
        DataTable dt = theHelper.GetMedGrpSubGrpMfgBymedID(Session["CoCode"].ToString().Trim(), medId);
        ddlmfg.DataSource = dt;
        ddlmfg.DataTextField = "MName";
        ddlmfg.DataValueField = "MCode";
        ddlmfg.DataBind();
        ddlmfg.Items.Insert(0, new ListItem("--Select--", "0"));
        ddlmfg.SelectedValue = dt.Rows[0]["MCode"].ToString();
    }

    private void DdlMedicineIDBind(DropDownList ddlMedicineID)
    {
        string type = DropDownList2.SelectedValue.ToString();
        ddlMedicineID.Items.Clear();
        ddlMedicineID.DataSource = theHelper.DropdownMedicine(Session["CoCode"].ToString().Trim(),type);
        ddlMedicineID.DataTextField = "MedicineName";
        ddlMedicineID.DataValueField = "MedicineID";
        ddlMedicineID.DataBind();
        ddlMedicineID.Items.Insert(0, new ListItem("--Select--", "0"));
    }


    private void DdlMedicineSubGroupBind(string medId, DropDownList ddlMedicineSubID)
    {
        ddlMedicineSubID.Items.Clear();
        //ddlMedicineSubID.DataSource = theHelper.GetMedSubGrpByID(Session["CoCode"].ToString().Trim(),medSubGrp);
        DataTable dt=theHelper.GetMedGrpSubGrpMfgBymedID(Session["CoCode"].ToString().Trim(), medId);
        ddlMedicineSubID.DataSource = dt;
        ddlMedicineSubID.DataTextField = "SubGrName";
        ddlMedicineSubID.DataValueField = "SubGroupid";
        ddlMedicineSubID.DataBind();
        ddlMedicineSubID.Items.Insert(0, new ListItem("--Select--", "0"));
        ddlMedicineSubID.SelectedValue = dt.Rows[0]["SubGroupid"].ToString();
    }

    protected void Button1_Click(object sender, EventArgs e)
    {
        TextBox t1;
        string compcode = Session["CoCode"].ToString().Trim();
        string yearcode = Session["YearCode"].ToString().Trim();
        double total = 0.0;
        string type = DropDownList2.SelectedValue.ToString();
        if (TextBox1.Text == "")
            TextBox1.Text = "0.00";
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
                string testdate = Calendar1.Text.Trim();// DateTime.ParseExact(Calendar1.Text, "dd/MM/yyyy", dtf);
                theHelper.UpdatePurchaseMedicine(compcode, yearcode, txtPurchaseMedicineId.Text, DropDownList1.SelectedValue, testdate, txtBillNo.Text, total.ToString());
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



    private void InsertPurchaseMedicine(string compcode, string yearcode,double total)
    {
        string reformattedDate1 = string.Empty;
        System.Globalization.DateTimeFormatInfo dtf = new DateTimeFormatInfo();
        string taxamt;
        string lessamt;
        string type = DropDownList2.SelectedValue.ToString();
        
        string testdate = Calendar1.Text.Trim();// DateTime.ParseExact(Calendar1.Text, "dd/MM/yyyy", dtf);

         //if (ddlMfg1.SelectedIndex > 0 && ddlMediGrp1.SelectedIndex > 0 && ddlMedi1.SelectedIndex > 0 && txtBatch1.Text.Length > 0 && Calendar2.Text.Length > 0 && txtQty1.Text.Length > 0 && txtUnitPrice1.Text.Length > 0 && txtTotalPrice1.Text.Length > 0)
        if (ddlMedi1.SelectedIndex > 0  && txtBatch1.Text.Length > 0  && txtQty1.Text.Length > 0 && txtUnitPrice1.Text.Length > 0 && txtTotalPrice1.Text.Length > 0)
        {
            //taxamt = (Convert.ToDouble(txtQty1.Text) * Convert.ToDouble(txtUnitPrice1.Text) * (Convert.ToDouble(txtStaxper1.Text) / 100)).ToString();
            //lessamt = (Convert.ToDouble(txtQty1.Text) * Convert.ToDouble(txtUnitPrice1.Text) * (Convert.ToDouble(txtLessper1.Text) / 100)).ToString();
            taxamt = txtStax1.Text;
            lessamt = txtLess1.Text;
            //string testdate1 = Calendar2.Text.Trim();// DateTime.ParseExact(Calendar2.Text, "dd/MM/yyyy", dtf);
            
            Int32 lastday1 = DateTime.DaysInMonth(Convert.ToInt32(ddlyear1.SelectedValue), Convert.ToInt32(ddlmonth1.SelectedValue));
            string testdate1 = ddlyear1.SelectedValue + "-" + ddlmonth1.SelectedValue + "-" + lastday1.ToString();
            theHelper.InsertPurchaseMedicine(compcode, yearcode, ddlMediSubGrp1.SelectedValue, TextBox1.Text, total.ToString(), txtPurchaseMedicineId.Text, DropDownList1.SelectedValue, testdate, txtBillNo.Text, ddlMfg1.SelectedValue, ddlMediGrp1.SelectedValue, ddlMedi1.SelectedValue, txtBatch1.Text, testdate1, txtQty1.Text, txtUnitPrice1.Text, txtTotalPrice1.Text, Session["userName"].ToString(), txtSellPrice1.Text, txtfreeqty1.Text, lessamt, taxamt, txtTotQty1.Text, txtCostPrice1.Text, txtlessper.Text, txtTaxper.Text, txtLessper1.Text, txtStaxper1.Text, type, ddlFrom.SelectedValue, ddlTo.SelectedValue, ddlgsttype.SelectedValue, txtHsnCode1.Text, txtCgstRt1.Text, txtCgstAmt1.Text, txtSgstRt1.Text, txtSgstAmt1.Text, txtIgstRt1.Text, txtIgstAmt1.Text, txtTotalPricewithouttax1.Text, txtStax1.Text, lblConvrtFactor1.Text,txtNetAmt.Text,txtRoundOff.Text);
        }
        if (ddlMedi2.SelectedIndex > 0  && txtBatch2.Text.Length > 0 && txtQty2.Text.Length > 0 && txtUnitPrice2.Text.Length > 0 && txtTotalPrice2.Text.Length > 0)
        {
            //taxamt = (Convert.ToDouble(txtQty2.Text) * Convert.ToDouble(txtUnitPrice2.Text) * (Convert.ToDouble(txtStaxper2.Text) / 100)).ToString();
            //lessamt = (Convert.ToDouble(txtQty2.Text) * Convert.ToDouble(txtUnitPrice2.Text) * (Convert.ToDouble(txtLessper2.Text) / 100)).ToString();
            taxamt = txtStax2.Text;
            lessamt = txtLess2.Text;
            //string testdate2 = Calendar3.Text.Trim();//DateTime.ParseExact(Calendar3.Text, "dd/MM/yyyy", dtf);
            Int32 lastday2 = DateTime.DaysInMonth(Convert.ToInt32(ddlyear2.SelectedValue), Convert.ToInt32(ddlmonth2.SelectedValue));
            string testdate2 = ddlyear2.SelectedValue + "-" + ddlmonth2.SelectedValue + "-" + lastday2.ToString();
            theHelper.InsertPurchaseMedicine(compcode, yearcode, ddlMediSubGrp2.SelectedValue, TextBox1.Text, total.ToString(), txtPurchaseMedicineId.Text, DropDownList1.SelectedValue, testdate, txtBillNo.Text, ddlMfg2.SelectedValue, ddlMediGrp2.SelectedValue, ddlMedi2.SelectedValue, txtBatch2.Text, testdate2, txtQty2.Text, txtUnitPrice2.Text, txtTotalPrice2.Text, Session["userName"].ToString(), txtSellPrice2.Text, txtfreeqty2.Text, lessamt, taxamt, txtTotQty2.Text, txtCostPrice2.Text, txtlessper.Text, txtTaxper.Text, txtLessper2.Text, txtStaxper2.Text, type, ddlFrom.SelectedValue, ddlTo.SelectedValue, ddlgsttype.SelectedValue, txtHsnCode2.Text, txtCgstRt2.Text, txtCgstAmt2.Text, txtSgstRt2.Text, txtSgstAmt2.Text, txtIgstRt2.Text, txtIgstAmt2.Text, txtTotalPricewithouttax2.Text, txtStax2.Text, lblConvrtFactor2.Text, txtNetAmt.Text, txtRoundOff.Text);
        }
        if (ddlMedi3.SelectedIndex > 0 && txtBatch3.Text.Length > 0 && txtQty3.Text.Length > 0 && txtUnitPrice3.Text.Length > 0 && txtTotalPrice3.Text.Length > 0)
        {
            //taxamt = (Convert.ToDouble(txtQty3.Text) * Convert.ToDouble(txtUnitPrice3.Text) * (Convert.ToDouble(txtStaxper3.Text) / 100)).ToString();
            //lessamt = (Convert.ToDouble(txtQty3.Text) * Convert.ToDouble(txtUnitPrice3.Text) * (Convert.ToDouble(txtLessper3.Text) / 100)).ToString();
            taxamt = txtStax3.Text;
            lessamt = txtLess3.Text;
            //string testdate3 = Calendar4.Text.Trim(); //DateTime.ParseExact(Calendar4.Text, "dd/MM/yyyy", dtf);
            Int32 lastday3 = DateTime.DaysInMonth(Convert.ToInt32(ddlyear3.SelectedValue), Convert.ToInt32(ddlmonth3.SelectedValue));
            string testdate3 = ddlyear3.SelectedValue + "-" + ddlmonth3.SelectedValue + "-" + lastday3.ToString();
            theHelper.InsertPurchaseMedicine(compcode, yearcode, ddlMediSubGrp3.SelectedValue, TextBox1.Text, total.ToString(), txtPurchaseMedicineId.Text, DropDownList1.SelectedValue, testdate, txtBillNo.Text, ddlMfg3.SelectedValue, ddlMediGrp3.SelectedValue, ddlMedi3.SelectedValue, txtBatch3.Text, testdate3, txtQty3.Text, txtUnitPrice3.Text, txtTotalPrice3.Text, Session["userName"].ToString(), txtSellPrice3.Text, txtfreeqty3.Text, lessamt, taxamt, txtTotQty3.Text, txtCostPrice3.Text, txtlessper.Text, txtTaxper.Text, txtLessper3.Text, txtStaxper3.Text, type, ddlFrom.SelectedValue, ddlTo.SelectedValue, ddlgsttype.SelectedValue, txtHsnCode3.Text, txtCgstRt3.Text, txtCgstAmt3.Text, txtSgstRt3.Text, txtSgstAmt3.Text, txtIgstRt3.Text, txtIgstAmt3.Text, txtTotalPricewithouttax3.Text, txtStax3.Text, lblConvrtFactor3.Text, txtNetAmt.Text, txtRoundOff.Text);
        }
        if (ddlMedi4.SelectedIndex > 0 && ddlMfg4.SelectedValue != "" && ddlMediGrp4.SelectedValue != "" && txtBatch4.Text.Length > 0 && txtQty4.Text.Length > 0 && txtUnitPrice4.Text.Length > 0 && txtTotalPrice4.Text.Length > 0)
        {
            //taxamt = (Convert.ToDouble(txtQty4.Text) * Convert.ToDouble(txtUnitPrice4.Text) * (Convert.ToDouble(txtStaxper4.Text) / 100)).ToString();
            //lessamt = (Convert.ToDouble(txtQty4.Text) * Convert.ToDouble(txtUnitPrice4.Text) * (Convert.ToDouble(txtLessper4.Text) / 100)).ToString();
            taxamt = txtStax4.Text;
            lessamt = txtLess4.Text;
            //string testdate4 = Calendar5.Text.Trim(); //DateTime.ParseExact(Calendar5.Text, "dd/MM/yyyy", dtf);
            Int32 lastday4 = DateTime.DaysInMonth(Convert.ToInt32(ddlyear4.SelectedValue), Convert.ToInt32(ddlmonth4.SelectedValue));
            string testdate4 = ddlyear4.SelectedValue + "-" + ddlmonth4.SelectedValue + "-" + lastday4.ToString();
            theHelper.InsertPurchaseMedicine(compcode, yearcode, ddlMediSubGrp4.SelectedValue, TextBox1.Text, total.ToString(), txtPurchaseMedicineId.Text, DropDownList1.SelectedValue, testdate, txtBillNo.Text, ddlMfg4.SelectedValue, ddlMediGrp4.SelectedValue, ddlMedi4.SelectedValue, txtBatch4.Text, testdate4, txtQty4.Text, txtUnitPrice4.Text, txtTotalPrice4.Text, Session["userName"].ToString(), txtSellPrice4.Text, txtfreeqty4.Text, lessamt, taxamt, txtTotQty4.Text, txtCostPrice4.Text, txtlessper.Text, txtTaxper.Text, txtLessper4.Text, txtStaxper4.Text, type, ddlFrom.SelectedValue, ddlTo.SelectedValue, ddlgsttype.SelectedValue, txtHsnCode4.Text, txtCgstRt4.Text, txtCgstAmt4.Text, txtSgstRt4.Text, txtSgstAmt4.Text, txtIgstRt4.Text, txtIgstAmt4.Text, txtTotalPricewithouttax4.Text, txtStax4.Text, lblConvrtFactor4.Text, txtNetAmt.Text, txtRoundOff.Text);
        }
        if (ddlMedi5.SelectedIndex > 0 && txtBatch5.Text.Length > 0 && txtQty5.Text.Length > 0 && txtUnitPrice5.Text.Length > 0 && txtTotalPrice5.Text.Length > 0)
        {
            //taxamt = (Convert.ToDouble(txtQty5.Text) * Convert.ToDouble(txtUnitPrice5.Text) * (Convert.ToDouble(txtStaxper5.Text) / 100)).ToString();
            //lessamt = (Convert.ToDouble(txtQty5.Text) * Convert.ToDouble(txtUnitPrice5.Text) * (Convert.ToDouble(txtLessper5.Text) / 100)).ToString();
            taxamt = txtStax5.Text;
            lessamt = txtLess5.Text;
            //string testdate5 = Calendar6.Text.Trim(); //DateTime.ParseExact(Calendar6.Text, "dd/MM/yyyy", dtf);
            Int32 lastday5 = DateTime.DaysInMonth(Convert.ToInt32(ddlyear5.SelectedValue), Convert.ToInt32(ddlmonth5.SelectedValue));
            string testdate5 = ddlyear5.SelectedValue + "-" + ddlmonth5.SelectedValue + "-" + lastday5.ToString();
            theHelper.InsertPurchaseMedicine(compcode, yearcode, ddlMediSubGrp5.SelectedValue, TextBox1.Text, total.ToString(), txtPurchaseMedicineId.Text, DropDownList1.SelectedValue, testdate, txtBillNo.Text, ddlMfg5.SelectedValue, ddlMediGrp5.SelectedValue, ddlMedi5.SelectedValue, txtBatch5.Text, testdate5, txtQty5.Text, txtUnitPrice5.Text, txtTotalPrice5.Text, Session["userName"].ToString(), txtSellPrice5.Text, txtfreeqty5.Text, lessamt, taxamt, txtTotQty5.Text, txtCostPrice5.Text, txtlessper.Text, txtTaxper.Text, txtLessper5.Text, txtStaxper5.Text, type, ddlFrom.SelectedValue, ddlTo.SelectedValue, ddlgsttype.SelectedValue, txtHsnCode5.Text, txtCgstRt5.Text, txtCgstAmt5.Text, txtSgstRt5.Text, txtSgstAmt5.Text, txtIgstRt5.Text, txtIgstAmt5.Text, txtTotalPricewithouttax5.Text, txtStax5.Text, lblConvrtFactor5.Text, txtNetAmt.Text, txtRoundOff.Text);
        }
        if (ddlMedi6.SelectedIndex > 0 && txtBatch6.Text.Length > 0 && txtQty6.Text.Length > 0 && txtUnitPrice6.Text.Length > 0 && txtTotalPrice6.Text.Length > 0)
        {
            //taxamt = (Convert.ToDouble(txtQty6.Text) * Convert.ToDouble(txtUnitPrice6.Text) * (Convert.ToDouble(txtStaxper6.Text) / 100)).ToString();
            //lessamt = (Convert.ToDouble(txtQty6.Text) * Convert.ToDouble(txtUnitPrice6.Text) * (Convert.ToDouble(txtLessper6.Text) / 100)).ToString();
            taxamt = txtStax6.Text;
            lessamt = txtLess6.Text;
            //string testdate6 = Calendar7.Text.Trim(); //DateTime.ParseExact(Calendar7.Text, "dd/MM/yyyy", dtf);
            Int32 lastday6 = DateTime.DaysInMonth(Convert.ToInt32(ddlyear6.SelectedValue), Convert.ToInt32(ddlmonth6.SelectedValue));
            string testdate6 = ddlyear6.SelectedValue + "-" + ddlmonth6.SelectedValue + "-" + lastday6.ToString();
            theHelper.InsertPurchaseMedicine(compcode, yearcode, ddlMediSubGrp6.SelectedValue, TextBox1.Text, total.ToString(), txtPurchaseMedicineId.Text, DropDownList1.SelectedValue, testdate, txtBillNo.Text, ddlMfg6.SelectedValue, ddlMediGrp6.SelectedValue, ddlMedi6.SelectedValue, txtBatch6.Text, testdate6, txtQty6.Text, txtUnitPrice6.Text, txtTotalPrice6.Text, Session["userName"].ToString(), txtSellPrice6.Text, txtfreeqty6.Text, lessamt, taxamt, txtTotQty6.Text, txtCostPrice6.Text, txtlessper.Text, txtTaxper.Text, txtLessper6.Text, txtStaxper6.Text, type, ddlFrom.SelectedValue, ddlTo.SelectedValue, ddlgsttype.SelectedValue, txtHsnCode6.Text, txtCgstRt6.Text, txtCgstAmt6.Text, txtSgstRt6.Text, txtSgstAmt6.Text, txtIgstRt6.Text, txtIgstAmt6.Text, txtTotalPricewithouttax6.Text, txtStax6.Text, lblConvrtFactor6.Text, txtNetAmt.Text, txtRoundOff.Text);
        }
        if (ddlMedi7.SelectedIndex > 0 && txtBatch7.Text.Length > 0 && txtQty7.Text.Length > 0 && txtUnitPrice7.Text.Length > 0 && txtTotalPrice7.Text.Length > 0)
        {
            //taxamt = (Convert.ToDouble(txtQty7.Text) * Convert.ToDouble(txtUnitPrice7.Text) * (Convert.ToDouble(txtStaxper7.Text) / 100)).ToString();
            //lessamt = (Convert.ToDouble(txtQty7.Text) * Convert.ToDouble(txtUnitPrice7.Text) * (Convert.ToDouble(txtLessper7.Text) / 100)).ToString();
            taxamt = txtStax7.Text;
            lessamt = txtLess7.Text;
            //string testdate7 = Calendar8.Text.Trim(); //DateTime.ParseExact(Calendar8.Text, "dd/MM/yyyy", dtf);
            Int32 lastday7 = DateTime.DaysInMonth(Convert.ToInt32(ddlyear7.SelectedValue), Convert.ToInt32(ddlmonth7.SelectedValue));
            string testdate7 = ddlyear7.SelectedValue + "-" + ddlmonth7.SelectedValue + "-" + lastday7.ToString();
            theHelper.InsertPurchaseMedicine(compcode, yearcode, ddlMediSubGrp7.SelectedValue, TextBox1.Text, total.ToString(), txtPurchaseMedicineId.Text, DropDownList1.SelectedValue, testdate, txtBillNo.Text, ddlMfg7.SelectedValue, ddlMediGrp7.SelectedValue, ddlMedi7.SelectedValue, txtBatch7.Text, testdate7, txtQty7.Text, txtUnitPrice7.Text, txtTotalPrice7.Text, Session["userName"].ToString(), txtSellPrice7.Text, txtfreeqty7.Text, lessamt, taxamt, txtTotQty7.Text, txtCostPrice7.Text, txtlessper.Text, txtTaxper.Text, txtLessper7.Text, txtStaxper7.Text, type, ddlFrom.SelectedValue, ddlTo.SelectedValue, ddlgsttype.SelectedValue, txtHsnCode7.Text, txtCgstRt7.Text, txtCgstAmt7.Text, txtSgstRt7.Text, txtSgstAmt7.Text, txtIgstRt7.Text, txtIgstAmt7.Text, txtTotalPricewithouttax7.Text, txtStax7.Text, lblConvrtFactor7.Text, txtNetAmt.Text, txtRoundOff.Text);
        }
        if (ddlMedi8.SelectedIndex > 0 && txtBatch8.Text.Length > 0 && txtQty8.Text.Length > 0 && txtUnitPrice8.Text.Length > 0 && txtTotalPrice8.Text.Length > 0)
        {
            //taxamt = (Convert.ToDouble(txtQty8.Text) * Convert.ToDouble(txtUnitPrice8.Text) * (Convert.ToDouble(txtStaxper8.Text) / 100)).ToString();
            //lessamt = (Convert.ToDouble(txtQty8.Text) * Convert.ToDouble(txtUnitPrice8.Text) * (Convert.ToDouble(txtLessper8.Text) / 100)).ToString();
            taxamt = txtStax8.Text;
            lessamt = txtLess8.Text;
            //string testdate8 = Calendar9.Text.Trim(); //DateTime.ParseExact(Calendar9.Text, "dd/MM/yyyy", dtf);
            Int32 lastday8 = DateTime.DaysInMonth(Convert.ToInt32(ddlyear8.SelectedValue), Convert.ToInt32(ddlmonth8.SelectedValue));
            string testdate8 = ddlyear8.SelectedValue + "-" + ddlmonth8.SelectedValue + "-" + lastday8.ToString();
            theHelper.InsertPurchaseMedicine(compcode, yearcode, ddlMediSubGrp8.SelectedValue, TextBox1.Text, total.ToString(), txtPurchaseMedicineId.Text, DropDownList1.SelectedValue, testdate, txtBillNo.Text, ddlMfg8.SelectedValue, ddlMediGrp8.SelectedValue, ddlMedi8.SelectedValue, txtBatch8.Text, testdate8, txtQty8.Text, txtUnitPrice8.Text, txtTotalPrice8.Text, Session["userName"].ToString(), txtSellPrice8.Text, txtfreeqty8.Text, lessamt, taxamt, txtTotQty8.Text, txtCostPrice8.Text, txtlessper.Text, txtTaxper.Text, txtLessper8.Text, txtStaxper8.Text, type, ddlFrom.SelectedValue, ddlTo.SelectedValue, ddlgsttype.SelectedValue, txtHsnCode8.Text, txtCgstRt8.Text, txtCgstAmt8.Text, txtSgstRt8.Text, txtSgstAmt8.Text, txtIgstRt8.Text, txtIgstAmt8.Text, txtTotalPricewithouttax8.Text, txtStax8.Text, lblConvrtFactor8.Text, txtNetAmt.Text, txtRoundOff.Text);
        }
        if (ddlMedi9.SelectedIndex > 0 && txtBatch9.Text.Length > 0 && txtQty9.Text.Length > 0 && txtUnitPrice9.Text.Length > 0 && txtTotalPrice9.Text.Length > 0)
        {
           // taxamt = (Convert.ToDouble(txtQty9.Text) * Convert.ToDouble(txtUnitPrice9.Text) * (Convert.ToDouble(txtStaxper9.Text) / 100)).ToString();
           // lessamt = (Convert.ToDouble(txtQty9.Text) * Convert.ToDouble(txtUnitPrice9.Text) * (Convert.ToDouble(txtLessper9.Text) / 100)).ToString();
            taxamt = txtStax9.Text;
            lessamt = txtLess9.Text;
            //string testdate9 = Calendar10.Text.Trim(); //DateTime.ParseExact(Calendar10.Text, "dd/MM/yyyy", dtf);
            Int32 lastday9 = DateTime.DaysInMonth(Convert.ToInt32(ddlyear9.SelectedValue), Convert.ToInt32(ddlmonth9.SelectedValue));
            string testdate9 = ddlyear9.SelectedValue + "-" + ddlmonth9.SelectedValue + "-" + lastday9.ToString();
            theHelper.InsertPurchaseMedicine(compcode, yearcode, ddlMediSubGrp9.SelectedValue, TextBox1.Text, total.ToString(), txtPurchaseMedicineId.Text, DropDownList1.SelectedValue, testdate, txtBillNo.Text, ddlMfg9.SelectedValue, ddlMediGrp9.SelectedValue, ddlMedi9.SelectedValue, txtBatch9.Text, testdate9, txtQty9.Text, txtUnitPrice9.Text, txtTotalPrice9.Text, Session["userName"].ToString(), txtSellPrice9.Text, txtfreeqty9.Text, lessamt, taxamt, txtTotQty9.Text, txtCostPrice9.Text, txtlessper.Text, txtTaxper.Text, txtLessper9.Text, txtStaxper9.Text, type, ddlFrom.SelectedValue, ddlTo.SelectedValue, ddlgsttype.SelectedValue, txtHsnCode9.Text, txtCgstRt9.Text, txtCgstAmt9.Text, txtSgstRt9.Text, txtSgstAmt9.Text, txtIgstRt9.Text, txtIgstAmt9.Text, txtTotalPricewithouttax9.Text, txtStax9.Text, lblConvrtFactor9.Text, txtNetAmt.Text, txtRoundOff.Text);
        }
        if (ddlMedi10.SelectedIndex > 0 && txtBatch10.Text.Length > 0 && txtQty10.Text.Length > 0 && txtUnitPrice10.Text.Length > 0 && txtTotalPrice10.Text.Length > 0)
        {
           // taxamt = (Convert.ToDouble(txtQty10.Text) * Convert.ToDouble(txtUnitPrice10.Text) * (Convert.ToDouble(txtStaxper10.Text) / 100)).ToString();
           // lessamt = (Convert.ToDouble(txtQty10.Text) * Convert.ToDouble(txtUnitPrice10.Text) * (Convert.ToDouble(txtLessper10.Text) / 100)).ToString();
            taxamt = txtStax10.Text;
            lessamt = txtLess10.Text;
            //string testdate10 = Calendar11.Text.Trim(); //DateTime.ParseExact(Calendar11.Text, "dd/MM/yyyy", dtf);
            Int32 lastday10 = DateTime.DaysInMonth(Convert.ToInt32(ddlyear10.SelectedValue), Convert.ToInt32(ddlmonth10.SelectedValue));
            string testdate10 = ddlyear10.SelectedValue + "-" + ddlmonth10.SelectedValue + "-" + lastday10.ToString();
            theHelper.InsertPurchaseMedicine(compcode, yearcode, ddlMediSubGrp10.SelectedValue, TextBox1.Text, total.ToString(), txtPurchaseMedicineId.Text, DropDownList1.SelectedValue, testdate, txtBillNo.Text, ddlMfg10.SelectedValue, ddlMediGrp10.SelectedValue, ddlMedi10.SelectedValue, txtBatch10.Text, testdate10, txtQty10.Text, txtUnitPrice10.Text, txtTotalPrice10.Text, Session["userName"].ToString(), txtSellPrice10.Text, txtfreeqty10.Text, lessamt, taxamt, txtTotQty10.Text, txtCostPrice10.Text, txtlessper.Text, txtTaxper.Text, txtLessper10.Text, txtStaxper10.Text, type, ddlFrom.SelectedValue, ddlTo.SelectedValue, ddlgsttype.SelectedValue, txtHsnCode10.Text, txtCgstRt10.Text, txtCgstAmt10.Text, txtSgstRt10.Text, txtSgstAmt10.Text, txtIgstRt10.Text, txtIgstAmt10.Text, txtTotalPricewithouttax10.Text, txtStax10.Text, lblConvrtFactor10.Text, txtNetAmt.Text, txtRoundOff.Text);
        }
        if (ddlMedi11.SelectedIndex > 0 && txtQty11.Text.Length > 0 && txtUnitPrice11.Text.Length > 0 && txtTotalPrice1.Text.Length > 0)
        {
           // taxamt = (Convert.ToDouble(txtQty11.Text) * Convert.ToDouble(txtUnitPrice11.Text) * (Convert.ToDouble(txtStaxper11.Text) / 100)).ToString();
           // lessamt = (Convert.ToDouble(txtQty11.Text) * Convert.ToDouble(txtUnitPrice11.Text) * (Convert.ToDouble(txtLessper11.Text) / 100)).ToString();
            taxamt = txtStax11.Text;
            lessamt = txtLess11.Text;
            //string testdate11 = Calendar12.Text.Trim(); //DateTime.ParseExact(Calendar12.Text, "dd/MM/yyyy", dtf);
            Int32 lastday11 = DateTime.DaysInMonth(Convert.ToInt32(ddlyear11.SelectedValue), Convert.ToInt32(ddlmonth11.SelectedValue));
            string testdate11 = ddlyear11.SelectedValue + "-" + ddlmonth11.SelectedValue + "-" + lastday11.ToString();
            theHelper.InsertPurchaseMedicine(compcode, yearcode, ddlMediSubGrp11.SelectedValue, TextBox1.Text, total.ToString(), txtPurchaseMedicineId.Text, DropDownList1.SelectedValue, testdate, txtBillNo.Text, ddlMfg11.SelectedValue, ddlMediGrp11.SelectedValue, ddlMedi11.SelectedValue, txtBatch11.Text, testdate11, txtQty11.Text, txtUnitPrice11.Text, txtTotalPrice11.Text, Session["userName"].ToString(), txtSellPrice11.Text, txtfreeqty11.Text, lessamt, taxamt, txtTotQty11.Text, txtCostPrice11.Text, txtlessper.Text, txtTaxper.Text, txtLessper11.Text, txtStaxper11.Text, type, ddlFrom.SelectedValue, ddlTo.SelectedValue, ddlgsttype.SelectedValue, txtHsnCode11.Text, txtCgstRt11.Text, txtCgstAmt11.Text, txtSgstRt11.Text, txtSgstAmt11.Text, txtIgstRt11.Text, txtIgstAmt11.Text, txtTotalPricewithouttax11.Text, txtStax11.Text, lblConvrtFactor11.Text, txtNetAmt.Text, txtRoundOff.Text);
        }
        if (ddlMedi12.SelectedIndex > 0 && txtQty12.Text.Length > 0 && txtUnitPrice12.Text.Length > 0 && txtTotalPrice12.Text.Length > 0)
        {
           // taxamt = (Convert.ToDouble(txtQty12.Text) * Convert.ToDouble(txtUnitPrice12.Text) * (Convert.ToDouble(txtStaxper12.Text) / 100)).ToString();
           // lessamt = (Convert.ToDouble(txtQty12.Text) * Convert.ToDouble(txtUnitPrice12.Text) * (Convert.ToDouble(txtLessper12.Text) / 100)).ToString();
            taxamt = txtStax12.Text;
            lessamt = txtLess12.Text;
            //string testdate12 = Calendar13.Text.Trim(); //DateTime.ParseExact(Calendar13.Text, "dd/MM/yyyy", dtf);
            Int32 lastday12 = DateTime.DaysInMonth(Convert.ToInt32(ddlyear12.SelectedValue), Convert.ToInt32(ddlmonth12.SelectedValue));
            string testdate12 = ddlyear12.SelectedValue + "-" + ddlmonth12.SelectedValue + "-" + lastday12.ToString();
            theHelper.InsertPurchaseMedicine(compcode, yearcode, ddlMediSubGrp12.SelectedValue, TextBox1.Text, total.ToString(), txtPurchaseMedicineId.Text, DropDownList1.SelectedValue, testdate, txtBillNo.Text, ddlMfg12.SelectedValue, ddlMediGrp12.SelectedValue, ddlMedi12.SelectedValue, txtBatch12.Text, testdate12, txtQty12.Text, txtUnitPrice12.Text, txtTotalPrice12.Text, Session["userName"].ToString(), txtSellPrice12.Text, txtfreeqty12.Text, lessamt, taxamt, txtTotQty12.Text, txtCostPrice12.Text, txtlessper.Text, txtTaxper.Text, txtLessper12.Text, txtStaxper12.Text, type, ddlFrom.SelectedValue, ddlTo.SelectedValue, ddlgsttype.SelectedValue, txtHsnCode12.Text, txtCgstRt12.Text, txtCgstAmt12.Text, txtSgstRt12.Text, txtSgstAmt12.Text, txtIgstRt12.Text, txtIgstAmt12.Text, txtTotalPricewithouttax12.Text, txtStax12.Text, lblConvrtFactor12.Text, txtNetAmt.Text, txtRoundOff.Text);
        }
        theHelper.InsUpdInv(compcode, yearcode, txtPurchaseMedicineId.Text.Trim(), "P", Session["userName"].ToString(),DropDownList2.SelectedValue.ToString());
    }




    private void MedicineSubGroupFill(string Group, int i)
    {
        DropDownList ddlMediSubGrp = (DropDownList)Page.FindControl("ctl00$ContentPlaceHolder1$ddlMediSubGrp" + i.ToString());
        DropDownList ddlMedi = (DropDownList)Page.FindControl("ctl00$ContentPlaceHolder1$ddlMedi" + i.ToString());

        if (Group == "0")
        {
            ddlMediSubGrp.SelectedIndex = 0;
            ddlMediSubGrp.Enabled = false;

            ddlMedi.SelectedIndex = 0;
            ddlMedi.Enabled = false;
        }
        else
        {
            ddlMediSubGrp.Items.Clear();
            ddlMediSubGrp.DataSource = theHelper.DropdownSubGroup(Session["CoCode"].ToString().Trim(),Group);
            ddlMediSubGrp.DataTextField = "SubGrName";
            ddlMediSubGrp.DataValueField = "ID";
            ddlMediSubGrp.DataBind();
            ddlMediSubGrp.Items.Insert(0, new ListItem("--Select--", "0"));

            ddlMediSubGrp.Enabled = true;
        }
    }

     

    protected void ddlMediGrp1_SelectedIndexChanged(object sender, EventArgs e)
    {
        MedicineSubGroupFill(ddlMediGrp1.SelectedValue, 1);     
    }
    protected void ddlMediGrp2_SelectedIndexChanged(object sender, EventArgs e)
    {
        MedicineSubGroupFill(ddlMediGrp2.SelectedValue, 2); 
    }
    protected void ddlMediGrp3_SelectedIndexChanged(object sender, EventArgs e)
    {
        MedicineSubGroupFill(ddlMediGrp3.SelectedValue,3); 
    }
    protected void ddlMediGrp4_SelectedIndexChanged(object sender, EventArgs e)
    {
        MedicineSubGroupFill(ddlMediGrp4.SelectedValue,4); 
    }
    protected void ddlMediGrp5_SelectedIndexChanged(object sender, EventArgs e)
    {
        MedicineSubGroupFill(ddlMediGrp5.SelectedValue, 5); 
    }
    protected void ddlMediGrp6_SelectedIndexChanged(object sender, EventArgs e)
    {
        MedicineSubGroupFill(ddlMediGrp6.SelectedValue,6); 
    }
    protected void ddlMediGrp7_SelectedIndexChanged(object sender, EventArgs e)
    {
        MedicineSubGroupFill(ddlMediGrp7.SelectedValue, 7); 
    }
    protected void ddlMediGrp8_SelectedIndexChanged(object sender, EventArgs e)
    {
        MedicineSubGroupFill(ddlMediGrp8.SelectedValue, 8); 
    }
    protected void ddlMediGrp9_SelectedIndexChanged(object sender, EventArgs e)
    {
        MedicineSubGroupFill(ddlMediGrp9.SelectedValue, 9); 
    }
    protected void ddlMediGrp10_SelectedIndexChanged(object sender, EventArgs e)
    {
        MedicineSubGroupFill(ddlMediGrp10.SelectedValue, 10); 
    }
    protected void ddlMediGrp11_SelectedIndexChanged(object sender, EventArgs e)
    {
        MedicineSubGroupFill(ddlMediGrp11.SelectedValue, 11); 
    }
    protected void ddlMediGrp12_SelectedIndexChanged(object sender, EventArgs e)
    {
        MedicineSubGroupFill(ddlMediGrp12.SelectedValue, 12);
    }

    protected void txtQty1_TextChanged(object sender, EventArgs e)
    {
        if (txtQty1.Text!=""  && txtUnitPrice1.Text!="")
        {
            double totalPrice = Convert.ToDouble(txtUnitPrice1.Text) * Convert.ToDouble(txtQty1.Text);
            txtTotalPrice1.Text = totalPrice.ToString();
        }
        else
        {
            txtTotalPrice1.Text = (0.00).ToString();
        }

        int totqty = getTotalQty(txtQty1.Text, txtfreeqty1.Text);
        txtTotQty1.Text = totqty.ToString();

        Double totamt = 0.00;

        totamt = calculateTotAmt(txtUnitPrice1.Text, txtQty1.Text, txtLess1.Text, txtStax1.Text);
        txtTotalPrice1.Text = totamt.ToString();

        Double purcst = calcPurchaseCost(totamt, txtQty1.Text, txtfreeqty1.Text);
        txtCostPrice1.Text = purcst.ToString();
        calculateNetAmt();
    }
    protected void txtQty2_TextChanged(object sender, EventArgs e)
    {
        if (txtQty2.Text != "" && txtUnitPrice2.Text != "")
        {
            double totalPrice = Convert.ToDouble(txtUnitPrice2.Text) * Convert.ToDouble(txtQty2.Text);
            txtTotalPrice2.Text = totalPrice.ToString();
        }
        else
        {
            txtTotalPrice2.Text = (0.00).ToString();
        }

        int totqty = getTotalQty(txtQty2.Text, txtfreeqty2.Text);
        txtTotQty2.Text = totqty.ToString();

        Double totamt = 0.00;

        totamt = calculateTotAmt(txtUnitPrice2.Text, txtQty2.Text, txtLess2.Text, txtStax2.Text);
        txtTotalPrice2.Text = totamt.ToString();

        Double purcst = calcPurchaseCost(totamt, txtQty2.Text, txtfreeqty2.Text);
        txtCostPrice2.Text = purcst.ToString();
        calculateNetAmt();
    }

    
    protected void txtQty3_TextChanged(object sender, EventArgs e)
    {
        if (txtQty3.Text != "" && txtUnitPrice3.Text != "")
        {
            double totalPrice = Convert.ToDouble(txtUnitPrice3.Text) * Convert.ToDouble(txtQty3.Text);
            txtTotalPrice3.Text = totalPrice.ToString();
        }
        else
        {
            txtTotalPrice3.Text = (0.00).ToString();
        }

        int totqty = getTotalQty(txtQty3.Text, txtfreeqty3.Text);
        txtTotQty3.Text = totqty.ToString();

        Double totamt = 0.00;

        totamt = calculateTotAmt(txtUnitPrice3.Text, txtQty3.Text, txtLess3.Text, txtStax3.Text);
        txtTotalPrice3.Text = totamt.ToString();

        Double purcst = calcPurchaseCost(totamt, txtQty3.Text, txtfreeqty3.Text);
        txtCostPrice3.Text = purcst.ToString();
        calculateNetAmt();
    }
    protected void txtQty4_TextChanged(object sender, EventArgs e)
    {
        if (txtQty4.Text != "" && txtUnitPrice4.Text != "")
        {
            double totalPrice = Convert.ToDouble(txtUnitPrice4.Text) * Convert.ToDouble(txtQty4.Text);
            txtTotalPrice4.Text = totalPrice.ToString();
        }
        else
        {
            txtTotalPrice4.Text = (0.00).ToString();
        }

        int totqty = getTotalQty(txtQty4.Text, txtfreeqty4.Text);
        txtTotQty4.Text = totqty.ToString();


        Double totamt = 0.00;

        totamt = calculateTotAmt(txtUnitPrice4.Text, txtQty4.Text, txtLess4.Text, txtStax4.Text);
        txtTotalPrice4.Text = totamt.ToString();

        Double purcst = calcPurchaseCost(totamt, txtQty4.Text, txtfreeqty4.Text);
        txtCostPrice4.Text = purcst.ToString();
        calculateNetAmt();
    }
    protected void txtQty5_TextChanged(object sender, EventArgs e)
    {
        if (txtQty5.Text != "" && txtUnitPrice5.Text != "")
        {
            double totalPrice = Convert.ToDouble(txtUnitPrice5.Text) * Convert.ToDouble(txtQty5.Text);
            txtTotalPrice5.Text = totalPrice.ToString();
        }
        else
        {
            txtTotalPrice5.Text = (0.00).ToString();
        }

        int totqty = getTotalQty(txtQty5.Text, txtfreeqty5.Text);
        txtTotQty5.Text = totqty.ToString();

        Double totamt = 0.00;

        totamt = calculateTotAmt(txtUnitPrice5.Text, txtQty5.Text, txtLess5.Text, txtStax5.Text);
        txtTotalPrice5.Text = totamt.ToString();

        Double purcst = calcPurchaseCost(totamt, txtQty5.Text, txtfreeqty5.Text);
        txtCostPrice5.Text = purcst.ToString();
        calculateNetAmt();
    }
    protected void txtQty6_TextChanged(object sender, EventArgs e)
    {
        if (txtQty6.Text != "" && txtUnitPrice6.Text != "")
        {
            double totalPrice = Convert.ToDouble(txtUnitPrice6.Text) * Convert.ToDouble(txtQty6.Text);
            txtTotalPrice6.Text = totalPrice.ToString();
        }
        else
        {
            txtTotalPrice6.Text = (0.00).ToString();
        }

        int totqty = getTotalQty(txtQty6.Text, txtfreeqty6.Text);
        txtTotQty6.Text = totqty.ToString();

        Double totamt = 0.00;

        totamt = calculateTotAmt(txtUnitPrice6.Text, txtQty6.Text, txtLess6.Text, txtStax6.Text);
        txtTotalPrice6.Text = totamt.ToString();

        Double purcst = calcPurchaseCost(totamt, txtQty6.Text, txtfreeqty6.Text);
        txtCostPrice6.Text = purcst.ToString();
        calculateNetAmt();
    }
    protected void txtQty7_TextChanged(object sender, EventArgs e)
    {
        if (txtQty7.Text != "" && txtUnitPrice7.Text != "")
        {
            double totalPrice = Convert.ToDouble(txtUnitPrice7.Text) * Convert.ToDouble(txtQty7.Text);
            txtTotalPrice7.Text = totalPrice.ToString();
        }
        else
        {
            txtTotalPrice7.Text = (0.00).ToString();
        }

        int totqty = getTotalQty(txtQty7.Text, txtfreeqty7.Text);
        txtTotQty7.Text = totqty.ToString();

        Double totamt = 0.00;

        totamt = calculateTotAmt(txtUnitPrice7.Text, txtQty7.Text, txtLess7.Text, txtStax7.Text);
        txtTotalPrice7.Text = totamt.ToString();

        Double purcst = calcPurchaseCost(totamt, txtQty7.Text, txtfreeqty7.Text);
        txtCostPrice7.Text = purcst.ToString();
        calculateNetAmt();
    }
    protected void txtQty8_TextChanged(object sender, EventArgs e)
    {
        if (txtQty8.Text != "" && txtUnitPrice8.Text != "")
        {
            double totalPrice = Convert.ToDouble(txtUnitPrice8.Text) * Convert.ToDouble(txtQty8.Text);
            txtTotalPrice8.Text = totalPrice.ToString();
        }
        else
        {
            txtTotalPrice8.Text = (0.00).ToString();
        }

        int totqty = getTotalQty(txtQty9.Text, txtfreeqty8.Text);
        txtTotQty8.Text = totqty.ToString();

        Double totamt = 0.00;

        totamt = calculateTotAmt(txtUnitPrice8.Text, txtQty8.Text, txtLess8.Text, txtStax8.Text);
        txtTotalPrice8.Text = totamt.ToString();

        Double purcst = calcPurchaseCost(totamt, txtQty8.Text, txtfreeqty8.Text);
        txtCostPrice8.Text = purcst.ToString();
        calculateNetAmt();
    }
    protected void txtQty9_TextChanged(object sender, EventArgs e)
    {
        if (txtQty9.Text != "" && txtUnitPrice9.Text != "")
        {
            double totalPrice = Convert.ToDouble(txtUnitPrice9.Text) * Convert.ToDouble(txtQty9.Text);
            txtTotalPrice9.Text = totalPrice.ToString();
        }
        else
        {
            txtTotalPrice9.Text = (0.00).ToString();
        }

        int totqty = getTotalQty(txtQty9.Text, txtfreeqty9.Text);
        txtTotQty9.Text = totqty.ToString();

        Double totamt = 0.00;

        totamt = calculateTotAmt(txtUnitPrice9.Text, txtQty9.Text, txtLess9.Text, txtStax9.Text);
        txtTotalPrice9.Text = totamt.ToString();

        Double purcst = calcPurchaseCost(totamt, txtQty9.Text, txtfreeqty9.Text);
        txtCostPrice9.Text = purcst.ToString();
        calculateNetAmt();
    }
    protected void txtQty10_TextChanged(object sender, EventArgs e)
    {
        if (txtQty10.Text != "" && txtUnitPrice10.Text != "")
        {
            double totalPrice = Convert.ToDouble(txtUnitPrice10.Text) * Convert.ToDouble(txtQty10.Text);
            txtTotalPrice10.Text = totalPrice.ToString();
        }
        else
        {
            txtTotalPrice10.Text = (0.00).ToString();
        }

        int totqty = getTotalQty(txtQty10.Text, txtfreeqty10.Text);
        txtTotQty10.Text = totqty.ToString();

        Double totamt = 0.00;

        totamt = calculateTotAmt(txtUnitPrice10.Text, txtQty10.Text, txtLess10.Text, txtStax10.Text);
        txtTotalPrice10.Text = totamt.ToString();

        Double purcst = calcPurchaseCost(totamt, txtQty10.Text, txtfreeqty10.Text);
        txtCostPrice10.Text = purcst.ToString();
        calculateNetAmt();
    }
    protected void txtQty11_TextChanged(object sender, EventArgs e)
    {
        if (txtQty11.Text != "" && txtUnitPrice11.Text != "")
        {
            double totalPrice = Convert.ToDouble(txtUnitPrice11.Text) * Convert.ToDouble(txtQty11.Text);
            txtTotalPrice11.Text = totalPrice.ToString();
        }
        else
        {
            txtTotalPrice11.Text = (0.00).ToString();
        }
        int totqty = getTotalQty(txtQty11.Text, txtfreeqty11.Text);
        txtTotQty11.Text = totqty.ToString();

        Double totamt = 0.00;

        totamt = calculateTotAmt(txtUnitPrice11.Text, txtQty11.Text, txtLess11.Text, txtStax11.Text);
        txtTotalPrice11.Text = totamt.ToString();
        Double purcst = calcPurchaseCost(totamt, txtQty11.Text, txtfreeqty11.Text);
        txtCostPrice11.Text = purcst.ToString();
        calculateNetAmt();
    }
    protected void txtQty12_TextChanged(object sender, EventArgs e)
    {
        if (txtQty12.Text != "" && txtUnitPrice12.Text != "")
        {
            double totalPrice = Convert.ToDouble(txtUnitPrice12.Text) * Convert.ToDouble(txtQty12.Text);
            txtTotalPrice12.Text = totalPrice.ToString();
        }
        else
        {
            txtTotalPrice12.Text = (0.00).ToString();
        }
        int totqty = getTotalQty(txtQty12.Text, txtfreeqty12.Text);
        txtTotQty12.Text = totqty.ToString();

        Double totamt = 0.00;

        totamt = calculateTotAmt(txtUnitPrice12.Text, txtQty12.Text, txtLess12.Text, txtStax12.Text);
        txtTotalPrice12.Text = totamt.ToString();

        Double purcst = calcPurchaseCost(totamt, txtQty12.Text, txtfreeqty12.Text);
        txtCostPrice12.Text = purcst.ToString();
        calculateNetAmt();
    }

    protected void Button4_Click(object sender, EventArgs e)
    {
        HiddenField1.Value = txtPurchaseMedicineId.Text;
     
        ResetAllFields();
        txtPurchaseMedicineId.Text=HiddenField1.Value;
        Button1.Text = "Update";
        DropDownFill();
        PageDataBind();    
    }

    private void MedicineFill(string subgroup, int i)
    {
         DropDownList ddlMedi = (DropDownList)Page.FindControl("ctl00$ContentPlaceHolder1$ddlMedi" + i.ToString());

        if (subgroup == "0")
        {          
            ddlMedi.SelectedIndex = 0;
            ddlMedi.Enabled = false;
        }
        else
        {
            ddlMedi.Items.Clear();
            ddlMedi.DataSource = theHelper.DropdownID4(Session["CoCode"].ToString().Trim(),subgroup);
            ddlMedi.DataTextField = "MedicineName";
            ddlMedi.DataValueField = "MedicineID";
            ddlMedi.DataBind();
            ddlMedi.Items.Insert(0, new ListItem("--Select--", "0"));
            ddlMedi.Enabled = true;
        }
    }

   /* protected void ddlMediSubGrp1_SelectedIndexChanged(object sender, EventArgs e)
    {
        MedicineFill(ddlMediSubGrp1.SelectedValue, 1);
    }
    protected void ddlMediSubGrp2_SelectedIndexChanged(object sender, EventArgs e)
    {
        MedicineFill(ddlMediSubGrp2.SelectedValue, 2);
    }
    protected void ddlMediSubGrp3_SelectedIndexChanged(object sender, EventArgs e)
    {
        MedicineFill(ddlMediSubGrp3.SelectedValue,3);
    }
    protected void ddlMediSubGrp4_SelectedIndexChanged(object sender, EventArgs e)
    {
        MedicineFill(ddlMediSubGrp4.SelectedValue, 4);
    }
    protected void ddlMediSubGrp5_SelectedIndexChanged(object sender, EventArgs e)
    {
        MedicineFill(ddlMediSubGrp5.SelectedValue,5);
    }
    protected void ddlMediSubGrp6_SelectedIndexChanged(object sender, EventArgs e)
    {
        MedicineFill(ddlMediSubGrp6.SelectedValue, 6);
    }
    protected void ddlMediSubGrp7_SelectedIndexChanged(object sender, EventArgs e)
    {
        MedicineFill(ddlMediSubGrp7.SelectedValue,7);
    }
    protected void ddlMediSubGrp8_SelectedIndexChanged(object sender, EventArgs e)
    {
        MedicineFill(ddlMediSubGrp8.SelectedValue, 8);
    }
    protected void ddlMediSubGrp9_SelectedIndexChanged(object sender, EventArgs e)
    {
        MedicineFill(ddlMediSubGrp9.SelectedValue, 9);
    }
    protected void ddlMediSubGrp10_SelectedIndexChanged(object sender, EventArgs e)
    {
        MedicineFill(ddlMediSubGrp10.SelectedValue, 10);
    }
    protected void ddlMediSubGrp11_SelectedIndexChanged(object sender, EventArgs e)
    {
        MedicineFill(ddlMediSubGrp11.SelectedValue, 11);
    }
    protected void ddlMediSubGrp12_SelectedIndexChanged(object sender, EventArgs e)
    {
        MedicineFill(ddlMediSubGrp12.SelectedValue, 12);
    }*/
   /* protected void ddlMfg1_SelectedIndexChanged(object sender, EventArgs e)
    {
            MedicineGroupFill(ddlMfg1.SelectedValue, 1);          
    }
    protected void ddlMfg2_SelectedIndexChanged(object sender, EventArgs e)
    {
        MedicineGroupFill(ddlMfg2.SelectedValue, 2);
    }
    protected void ddlMfg3_SelectedIndexChanged(object sender, EventArgs e)
    {
        MedicineGroupFill(ddlMfg3.SelectedValue, 3);
    }
    protected void ddlMfg4_SelectedIndexChanged(object sender, EventArgs e)
    {
        MedicineGroupFill(ddlMfg4.SelectedValue, 4);
    }
    protected void ddlMfg5_SelectedIndexChanged(object sender, EventArgs e)
    {
        MedicineGroupFill(ddlMfg5.SelectedValue, 5);
    }
    protected void ddlMfg6_SelectedIndexChanged(object sender, EventArgs e)
    {
        MedicineGroupFill(ddlMfg6.SelectedValue, 6);
    }
    protected void ddlMfg7_SelectedIndexChanged(object sender, EventArgs e)
    {
        MedicineGroupFill(ddlMfg7.SelectedValue,7);
    }
    protected void ddlMfg8_SelectedIndexChanged(object sender, EventArgs e)
    {
        MedicineGroupFill(ddlMfg8.SelectedValue,8);
    }
    protected void ddlMfg9_SelectedIndexChanged(object sender, EventArgs e)
    {
        MedicineGroupFill(ddlMfg9.SelectedValue,9);
    }
    protected void ddlMfg10_SelectedIndexChanged(object sender, EventArgs e)
    {
        MedicineGroupFill(ddlMfg10.SelectedValue,10);
    }
    protected void ddlMfg11_SelectedIndexChanged(object sender, EventArgs e)
    {
        MedicineGroupFill(ddlMfg11.SelectedValue,11);
    }
    protected void ddlMfg12_SelectedIndexChanged(object sender, EventArgs e)
    {
        MedicineGroupFill(ddlMfg12.SelectedValue,12);
    }*/


    protected void txtfreeqty1_TextChanged(object sender, EventArgs e)
    {
        int totqty=getTotalQty(txtQty1.Text, txtfreeqty1.Text);
        txtTotQty1.Text = totqty.ToString();

        Double totamt = 0.00;

        totamt = calculateTotAmt(txtUnitPrice1.Text, txtQty1.Text, txtLess1.Text, txtStax1.Text);
        

        Double purcst = calcPurchaseCost(totamt, txtQty1.Text, txtfreeqty1.Text);
        txtCostPrice1.Text = purcst.ToString();
        calculateNetAmt();
    }
    protected void txtfreeqty2_TextChanged(object sender, EventArgs e)
    {
        int totqty=getTotalQty(txtQty2.Text, txtfreeqty2.Text);
        txtTotQty2.Text = totqty.ToString();

        Double totamt = 0.00;

        totamt = calculateTotAmt(txtUnitPrice2.Text, txtQty2.Text, txtLess2.Text, txtStax2.Text);
        

        Double purcst = calcPurchaseCost(totamt, txtQty2.Text, txtfreeqty2.Text);
        txtCostPrice2.Text = purcst.ToString();
        calculateNetAmt();
    }
    protected void txtfreeqty3_TextChanged(object sender, EventArgs e)
    {
        int totqty = getTotalQty(txtQty3.Text, txtfreeqty3.Text);
        txtTotQty3.Text = totqty.ToString();

        Double totamt = 0.00;

        totamt = calculateTotAmt(txtUnitPrice3.Text, txtQty3.Text, txtLess3.Text, txtStax3.Text);


        Double purcst = calcPurchaseCost(totamt, txtQty3.Text, txtfreeqty3.Text);
        txtCostPrice3.Text = purcst.ToString();
        calculateNetAmt();
    }

    protected void txtfreeqty4_TextChanged(object sender, EventArgs e)
    {
        int totqty = getTotalQty(txtQty4.Text, txtfreeqty4.Text);
        txtTotQty4.Text = totqty.ToString();

        Double totamt = 0.00;

        totamt = calculateTotAmt(txtUnitPrice4.Text, txtQty4.Text, txtLess4.Text, txtStax4.Text);


        Double purcst = calcPurchaseCost(totamt, txtQty4.Text, txtfreeqty4.Text);
        txtCostPrice4.Text = purcst.ToString();
        calculateNetAmt();
    }

    protected void txtfreeqty5_TextChanged(object sender, EventArgs e)
    {
        int totqty = getTotalQty(txtQty5.Text, txtfreeqty5.Text);
        txtTotQty5.Text = totqty.ToString();

        Double totamt = 0.00;

        totamt = calculateTotAmt(txtUnitPrice5.Text, txtQty5.Text, txtLess5.Text, txtStax5.Text);


        Double purcst = calcPurchaseCost(totamt, txtQty5.Text, txtfreeqty5.Text);
        txtCostPrice5.Text = purcst.ToString();
        calculateNetAmt();
    }

    protected void txtfreeqty6_TextChanged(object sender, EventArgs e)
    {
        int totqty = getTotalQty(txtQty6.Text, txtfreeqty6.Text);
        txtTotQty6.Text = totqty.ToString();

        Double totamt = 0.00;

        totamt = calculateTotAmt(txtUnitPrice6.Text, txtQty6.Text, txtLess6.Text, txtStax6.Text);


        Double purcst = calcPurchaseCost(totamt, txtQty6.Text, txtfreeqty6.Text);
        txtCostPrice6.Text = purcst.ToString();
        calculateNetAmt();
    }

    protected void txtfreeqty7_TextChanged(object sender, EventArgs e)
    {
        int totqty = getTotalQty(txtQty7.Text, txtfreeqty7.Text);
        txtTotQty7.Text = totqty.ToString();

        Double totamt = 0.00;

        totamt = calculateTotAmt(txtUnitPrice7.Text, txtQty7.Text, txtLess7.Text, txtStax7.Text);


        Double purcst = calcPurchaseCost(totamt, txtQty7.Text, txtfreeqty7.Text);
        txtCostPrice7.Text = purcst.ToString();
        calculateNetAmt();
    }

    protected void txtfreeqty8_TextChanged(object sender, EventArgs e)
    {
        int totqty = getTotalQty(txtQty8.Text, txtfreeqty8.Text);
        txtTotQty8.Text = totqty.ToString();

        Double totamt = 0.00;

        totamt = calculateTotAmt(txtUnitPrice8.Text, txtQty8.Text, txtLess8.Text, txtStax8.Text);


        Double purcst = calcPurchaseCost(totamt, txtQty8.Text, txtfreeqty8.Text);
        txtCostPrice8.Text = purcst.ToString();
        calculateNetAmt();
    }
    protected void txtfreeqty9_TextChanged(object sender, EventArgs e)
    {
        int totqty = getTotalQty(txtQty9.Text, txtfreeqty9.Text);
        txtTotQty9.Text = totqty.ToString();

        Double totamt = 0.00;

        totamt = calculateTotAmt(txtUnitPrice9.Text, txtQty9.Text, txtLess9.Text, txtStax9.Text);


        Double purcst = calcPurchaseCost(totamt, txtQty9.Text, txtfreeqty9.Text);
        txtCostPrice9.Text = purcst.ToString();
        calculateNetAmt();
    }
    protected void txtfreeqty10_TextChanged(object sender, EventArgs e)
    {
        int totqty = getTotalQty(txtQty10.Text, txtfreeqty10.Text);
        txtTotQty10.Text = totqty.ToString();
        Double totamt = 0.00;

        totamt = calculateTotAmt(txtUnitPrice10.Text, txtQty10.Text, txtLess10.Text, txtStax10.Text);


        Double purcst = calcPurchaseCost(totamt, txtQty10.Text, txtfreeqty10.Text);
        txtCostPrice10.Text = purcst.ToString();
        calculateNetAmt();
    }
    protected void txtfreeqty11_TextChanged(object sender, EventArgs e)
    {
        int totqty = getTotalQty(txtQty11.Text, txtfreeqty11.Text);
        txtTotQty11.Text = totqty.ToString();

        Double totamt = 0.00;

        totamt = calculateTotAmt(txtUnitPrice11.Text, txtQty11.Text, txtLess11.Text, txtStax11.Text);


        Double purcst = calcPurchaseCost(totamt, txtQty11.Text, txtfreeqty11.Text);
        txtCostPrice11.Text = purcst.ToString();
        calculateNetAmt();
    }
    protected void txtfreeqty12_TextChanged(object sender, EventArgs e)
    {
        int totqty = getTotalQty(txtQty12.Text, txtfreeqty12.Text);
        txtTotQty12.Text = totqty.ToString();
        Double totamt = 0.00;

        totamt = calculateTotAmt(txtUnitPrice12.Text, txtQty12.Text, txtLess12.Text, txtStax12.Text);


        Double purcst = calcPurchaseCost(totamt, txtQty12.Text, txtfreeqty12.Text);
        txtCostPrice12.Text = purcst.ToString();
        calculateNetAmt();
    }


    protected void txtUnitPrice1_TextChanged(object sender, EventArgs e)
    {
        Double totamt = 0.00;

        Double stax = calStax(txtUnitPrice1.Text);
        txtStax1.Text = stax.ToString();
        Double less = calcLess(txtUnitPrice1.Text);
        txtLess1.Text = less.ToString();

        totamt = calculateTotAmt(txtUnitPrice1.Text, txtQty1.Text, txtLess1.Text, txtStax1.Text);
        txtTotalPrice1.Text = totamt.ToString();
       
        Double purcst = calcPurchaseCost(totamt, txtQty1.Text, txtfreeqty1.Text);
        txtCostPrice1.Text = purcst.ToString();
        calculateNetAmt();
    }

    protected void txtUnitPrice2_TextChanged(object sender, EventArgs e)
    {
        Double totamt = 0.00;

        Double stax = calStax(txtUnitPrice2.Text);
        txtStax2.Text = stax.ToString();
        Double less = calcLess(txtUnitPrice2.Text);
        txtLess2.Text = less.ToString();

        totamt = calculateTotAmt(txtUnitPrice2.Text, txtQty2.Text, txtLess2.Text, txtStax2.Text);
        txtTotalPrice2.Text = totamt.ToString();
        
        Double purcst = calcPurchaseCost(totamt, txtQty2.Text, txtfreeqty2.Text);
        txtCostPrice2.Text = purcst.ToString();
        calculateNetAmt();
    }

    protected void txtUnitPrice3_TextChanged(object sender, EventArgs e)
    {
        Double totamt = 0.00;

        Double stax = calStax(txtUnitPrice3.Text);
        txtStax3.Text = stax.ToString();
        Double less = calcLess(txtUnitPrice3.Text);
        txtLess3.Text = less.ToString();

        totamt = calculateTotAmt(txtUnitPrice3.Text, txtQty3.Text, txtLess3.Text, txtStax3.Text);
        txtTotalPrice3.Text = totamt.ToString();
        
        Double purcst = calcPurchaseCost(totamt, txtQty3.Text, txtfreeqty3.Text);
        txtCostPrice3.Text = purcst.ToString();
        calculateNetAmt();
    }

    protected void txtUnitPrice4_TextChanged(object sender, EventArgs e)
    {
        Double totamt = 0.00;

        Double stax = calStax(txtUnitPrice4.Text);
        txtStax4.Text = stax.ToString();
        Double less = calcLess(txtUnitPrice4.Text);
        txtLess4.Text = less.ToString();

        totamt = calculateTotAmt(txtUnitPrice4.Text, txtQty4.Text, txtLess4.Text, txtStax4.Text);
        txtTotalPrice4.Text = totamt.ToString();
        
        Double purcst = calcPurchaseCost(totamt, txtQty4.Text, txtfreeqty4.Text);
        txtCostPrice4.Text = purcst.ToString();
        calculateNetAmt();
    }

    protected void txtUnitPrice5_TextChanged(object sender, EventArgs e)
    {
        Double totamt = 0.00;

        Double stax = calStax(txtUnitPrice5.Text);
        txtStax5.Text = stax.ToString();
        Double less = calcLess(txtUnitPrice5.Text);
        txtLess5.Text = less.ToString();

        totamt = calculateTotAmt(txtUnitPrice5.Text, txtQty5.Text, txtLess5.Text, txtStax5.Text);
        txtTotalPrice5.Text = totamt.ToString();
        
        Double purcst = calcPurchaseCost(totamt, txtQty5.Text, txtfreeqty5.Text);
        txtCostPrice5.Text = purcst.ToString();
        calculateNetAmt();
    }
    protected void txtUnitPrice6_TextChanged(object sender, EventArgs e)
    {
        Double totamt = 0.00;

        Double stax = calStax(txtUnitPrice6.Text);
        txtStax6.Text = stax.ToString();
        Double less = calcLess(txtUnitPrice6.Text);
        txtLess6.Text = less.ToString();

        totamt = calculateTotAmt(txtUnitPrice6.Text, txtQty6.Text, txtLess6.Text, txtStax6.Text);
        txtTotalPrice6.Text = totamt.ToString();
        
        Double purcst = calcPurchaseCost(totamt, txtQty6.Text, txtfreeqty6.Text);
        txtCostPrice6.Text = purcst.ToString();
        calculateNetAmt();
    }
    protected void txtUnitPrice7_TextChanged(object sender, EventArgs e)
    {
        Double totamt = 0.00;

        Double stax = calStax(txtUnitPrice7.Text);
        txtStax7.Text = stax.ToString();
        Double less = calcLess(txtUnitPrice7.Text);
        txtLess7.Text = less.ToString();

        totamt = calculateTotAmt(txtUnitPrice7.Text, txtQty7.Text, txtLess7.Text, txtStax7.Text);
        txtTotalPrice7.Text = totamt.ToString();
       
        Double purcst = calcPurchaseCost(totamt, txtQty7.Text, txtfreeqty7.Text);
        txtCostPrice7.Text = purcst.ToString();
        calculateNetAmt();
    }

    protected void txtUnitPrice8_TextChanged(object sender, EventArgs e)
    {
        Double totamt = 0.00;

        Double stax = calStax(txtUnitPrice8.Text);
        txtStax8.Text = stax.ToString();
        Double less = calcLess(txtUnitPrice8.Text);
        txtLess8.Text = less.ToString();

        totamt = calculateTotAmt(txtUnitPrice8.Text, txtQty8.Text, txtLess8.Text, txtStax8.Text);
        txtTotalPrice8.Text = totamt.ToString();
        
        Double purcst = calcPurchaseCost(totamt, txtQty8.Text, txtfreeqty8.Text);
        txtCostPrice8.Text = purcst.ToString();
        calculateNetAmt();
    }

    protected void txtUnitPrice9_TextChanged(object sender, EventArgs e)
    {
        Double totamt = 0.00;
        Double stax = calStax(txtUnitPrice9.Text);
        txtStax9.Text = stax.ToString();
        Double less = calcLess(txtUnitPrice9.Text);
        txtLess9.Text = less.ToString();

        totamt = calculateTotAmt(txtUnitPrice9.Text, txtQty9.Text, txtLess9.Text, txtStax9.Text);
        txtTotalPrice9.Text = totamt.ToString();
        
        Double purcst = calcPurchaseCost(totamt, txtQty9.Text, txtfreeqty9.Text);
        txtCostPrice9.Text = purcst.ToString();
        calculateNetAmt();
    }

    protected void txtUnitPrice10_TextChanged(object sender, EventArgs e)
    {
        Double totamt = 0.00;
        Double stax = calStax(txtUnitPrice10.Text);
        txtStax10.Text = stax.ToString();
        Double less = calcLess(txtUnitPrice10.Text);
        txtLess10.Text = less.ToString();

        totamt = calculateTotAmt(txtUnitPrice10.Text, txtQty10.Text, txtLess10.Text, txtStax10.Text);
        txtTotalPrice10.Text = totamt.ToString();
        
        Double purcst = calcPurchaseCost(totamt, txtQty10.Text, txtfreeqty10.Text);
        txtCostPrice10.Text = purcst.ToString();
        calculateNetAmt();
    }

    protected void txtUnitPrice11_TextChanged(object sender, EventArgs e)
    {
        Double totamt = 0.00;
        Double stax = calStax(txtUnitPrice11.Text);
        txtStax11.Text = stax.ToString();
        Double less = calcLess(txtUnitPrice11.Text);
        txtLess11.Text = less.ToString();

        totamt = calculateTotAmt(txtUnitPrice11.Text, txtQty11.Text, txtLess11.Text, txtStax11.Text);
        txtTotalPrice11.Text = totamt.ToString();
        
        Double purcst = calcPurchaseCost(totamt, txtQty11.Text, txtfreeqty11.Text);
        txtCostPrice11.Text = purcst.ToString();
        calculateNetAmt();
    }

    protected void txtUnitPrice12_TextChanged(object sender, EventArgs e)
    {
        Double totamt = 0.00;
        Double stax = calStax(txtUnitPrice12.Text);
        txtStax12.Text = stax.ToString();
        Double less = calcLess(txtUnitPrice12.Text);
        txtLess12.Text = less.ToString();

        totamt = calculateTotAmt(txtUnitPrice12.Text, txtQty12.Text, txtLess12.Text, txtStax12.Text);
        txtTotalPrice12.Text = totamt.ToString();
        
        Double purcst = calcPurchaseCost(totamt, txtQty12.Text, txtfreeqty12.Text);
        txtCostPrice12.Text = purcst.ToString();
        calculateNetAmt();
    }


    protected void txtLess1_textChanged(object sender, EventArgs e)
    {
        Double totamt = 0.00;
        totamt = calculateTotAmt(txtUnitPrice1.Text, txtQty1.Text, txtLess1.Text, txtStax1.Text);
        txtTotalPrice1.Text = totamt.ToString();
        calculateNetAmt();
    }
    protected void txtLess2_textChanged(object sender, EventArgs e)
    {
        Double totamt = 0.00;
        totamt = calculateTotAmt(txtUnitPrice2.Text, txtQty2.Text, txtLess2.Text, txtStax2.Text);
        txtTotalPrice2.Text = totamt.ToString();
        calculateNetAmt();
    }
    protected void txtLess3_textChanged(object sender, EventArgs e)
    {
        Double totamt = 0.00;
        totamt = calculateTotAmt(txtUnitPrice3.Text, txtQty3.Text, txtLess3.Text, txtStax3.Text);
        txtTotalPrice3.Text = totamt.ToString();
        calculateNetAmt();
    }

    protected void txtLess4_textChanged(object sender, EventArgs e)
    {
        Double totamt = 0.00;
        totamt = calculateTotAmt(txtUnitPrice4.Text, txtQty4.Text, txtLess4.Text, txtStax4.Text);
        txtTotalPrice4.Text = totamt.ToString();
        calculateNetAmt();
    }
    protected void txtLess5_textChanged(object sender, EventArgs e)
    {
        Double totamt = 0.00;
        totamt = calculateTotAmt(txtUnitPrice5.Text, txtQty5.Text, txtLess5.Text, txtStax5.Text);
        txtTotalPrice5.Text = totamt.ToString();
        calculateNetAmt();
    }

    protected void txtLess6_textChanged(object sender, EventArgs e)
    {
        Double totamt = 0.00;
        totamt = calculateTotAmt(txtUnitPrice6.Text, txtQty6.Text, txtLess6.Text, txtStax6.Text);
        txtTotalPrice6.Text = totamt.ToString();
        calculateNetAmt();
    }

    protected void txtLess7_textChanged(object sender, EventArgs e)
    {
        Double totamt = 0.00;
        totamt = calculateTotAmt(txtUnitPrice7.Text, txtQty7.Text, txtLess7.Text, txtStax7.Text);
        txtTotalPrice7.Text = totamt.ToString();
        calculateNetAmt();
    }
    protected void txtLess8_textChanged(object sender, EventArgs e)
    {
        Double totamt = 0.00;
        totamt = calculateTotAmt(txtUnitPrice8.Text, txtQty8.Text, txtLess8.Text, txtStax8.Text);
        txtTotalPrice8.Text = totamt.ToString();
        calculateNetAmt();
    }
    protected void txtLess9_textChanged(object sender, EventArgs e)
    {
        Double totamt = 0.00;
        totamt = calculateTotAmt(txtUnitPrice9.Text, txtQty9.Text, txtLess9.Text, txtStax9.Text);
        txtTotalPrice9.Text = totamt.ToString();
        calculateNetAmt();
    }
    protected void txtLess10_textChanged(object sender, EventArgs e)
    {
        Double totamt = 0.00;
        totamt = calculateTotAmt(txtUnitPrice10.Text, txtQty10.Text, txtLess10.Text, txtStax10.Text);
        txtTotalPrice10.Text = totamt.ToString();
        calculateNetAmt();
    }
    protected void txtLess11_textChanged(object sender, EventArgs e)
    {
        Double totamt = 0.00;
        totamt = calculateTotAmt(txtUnitPrice11.Text, txtQty11.Text, txtLess11.Text, txtStax11.Text);
        txtTotalPrice11.Text = totamt.ToString();
        calculateNetAmt();
    }
    protected void txtLess12_textChanged(object sender, EventArgs e)
    {
        Double totamt = 0.00;
        totamt = calculateTotAmt(txtUnitPrice12.Text, txtQty12.Text, txtLess12.Text, txtStax12.Text);
        txtTotalPrice12.Text = totamt.ToString();
        calculateNetAmt();
    }



    protected void txtStax1_textChanged(object sender, EventArgs e)
    {
        Double totamt = 0.00;
        totamt = calculateTotAmt(txtUnitPrice1.Text, txtQty1.Text, txtLess1.Text, txtStax1.Text);
        txtTotalPrice1.Text = totamt.ToString();
        calculateNetAmt();
    }
    protected void txtStax2_textChanged(object sender, EventArgs e)
    {
        Double totamt = 0.00;
        totamt = calculateTotAmt(txtUnitPrice2.Text, txtQty2.Text, txtLess2.Text, txtStax2.Text);
        txtTotalPrice2.Text = totamt.ToString();
        calculateNetAmt();
    }
    protected void txtStax3_textChanged(object sender, EventArgs e)
    {
        Double totamt = 0.00;
        totamt = calculateTotAmt(txtUnitPrice3.Text, txtQty3.Text, txtLess3.Text, txtStax3.Text);
        txtTotalPrice3.Text = totamt.ToString();
        calculateNetAmt();
    }

    protected void txtStax4_textChanged(object sender, EventArgs e)
    {
        Double totamt = 0.00;
        totamt = calculateTotAmt(txtUnitPrice4.Text, txtQty4.Text, txtLess4.Text, txtStax4.Text);
        txtTotalPrice4.Text = totamt.ToString();
        calculateNetAmt();
    }
    protected void txtStax5_textChanged(object sender, EventArgs e)
    {
        Double totamt = 0.00;
        totamt = calculateTotAmt(txtUnitPrice5.Text, txtQty5.Text, txtLess5.Text, txtStax5.Text);
        txtTotalPrice5.Text = totamt.ToString();
        calculateNetAmt();
    }

    protected void txtStax6_textChanged(object sender, EventArgs e)
    {
        Double totamt = 0.00;
        totamt = calculateTotAmt(txtUnitPrice6.Text, txtQty6.Text, txtLess6.Text, txtStax6.Text);
        txtTotalPrice6.Text = totamt.ToString();
        calculateNetAmt();
    }

    protected void txtStax7_textChanged(object sender, EventArgs e)
    {
        Double totamt = 0.00;
        totamt = calculateTotAmt(txtUnitPrice7.Text, txtQty7.Text, txtLess7.Text, txtStax7.Text);
        txtTotalPrice7.Text = totamt.ToString();
        calculateNetAmt();
    }
    protected void txtStax8_textChanged(object sender, EventArgs e)
    {
        Double totamt = 0.00;
        totamt = calculateTotAmt(txtUnitPrice8.Text, txtQty8.Text, txtLess8.Text, txtStax8.Text);
        txtTotalPrice8.Text = totamt.ToString();
        calculateNetAmt();
    }
    protected void txtStax9_textChanged(object sender, EventArgs e)
    {
        Double totamt = 0.00;
        totamt = calculateTotAmt(txtUnitPrice9.Text, txtQty9.Text, txtLess9.Text, txtStax9.Text);
        txtTotalPrice9.Text = totamt.ToString();
        calculateNetAmt();
    }
    protected void txtStax10_textChanged(object sender, EventArgs e)
    {
        Double totamt = 0.00;
        totamt = calculateTotAmt(txtUnitPrice10.Text, txtQty10.Text, txtLess10.Text, txtStax10.Text);
        txtTotalPrice10.Text = totamt.ToString();
        calculateNetAmt();
    }
    protected void txtStax11_textChanged(object sender, EventArgs e)
    {
        Double totamt = 0.00;
        totamt = calculateTotAmt(txtUnitPrice11.Text, txtQty11.Text, txtLess11.Text, txtStax11.Text);
        txtTotalPrice11.Text = totamt.ToString();
        calculateNetAmt();
    }
    protected void txtStax12_textChanged(object sender, EventArgs e)
    {
        Double totamt = 0.00;
        totamt = calculateTotAmt(txtUnitPrice12.Text, txtQty12.Text, txtLess12.Text, txtStax12.Text);
        txtTotalPrice12.Text = totamt.ToString();
        calculateNetAmt();
    }


    protected int getTotalQty(string txtqty, string txtfreeqty)
    {
        int qty = 0;
        int freeqty = 0;
        if (txtqty != "")
        {
            qty = Convert.ToInt16(txtqty);
        }
        else
        {
            qty = 0;
        }
        if (txtfreeqty != "")
        {
            freeqty = Convert.ToInt16(txtfreeqty);
        }
        else
        {
            freeqty = 0;
        }
        return qty + freeqty;
    }

    protected Double calculateTotAmt(string trndprice, string qty,string less,string tax)
    {
        Double trendprice = 0.00;
        Double quantity = 0.00;
        Double amt = 0.00;
       // Double lessper = 0.00;
       // Double tax = 0.00;
        Double lessamt = 0.00;
        Double txamt = 0.00;
        if (trndprice != "")
        {
            trendprice = Convert.ToDouble(trndprice);
        }
        if (qty != "")
        {
            quantity = Convert.ToDouble(qty);
        }
        /*if (txtlessper.Text != "")
        {
            lessper = Convert.ToDouble(txtlessper.Text);
        }
        if (txtTaxper.Text != "")
        {
            tax = Convert.ToDouble(txtTaxper.Text);
        }
        txamt = (tax * trendprice) / 100;
        lessamt = (lessper * trendprice) / 100;*/
        if (less != "")
        {
            lessamt = Convert.ToDouble(less);
        }
        else
        {
            lessamt = 0.00;
        }
        if (tax != "")
        {
            txamt = Convert.ToDouble(tax);
        }
        else
        {
            txamt = 0.00;
        }
        if (qty != "")
        {
            amt = (quantity * trendprice) + txamt - lessamt;
        }
        else
        {
            amt = 0.00;
        }
        return amt;

    }

    Double calStax(string trndprc)
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


    Double calcLess(string trndprc)
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

    protected Double calcPurchaseCost(Double totamt, string txtqty, string txtfreeqty)
    {
        int qty = 0;
        int freeqty = 0;
        Double purcst = 0.00;
        if (txtqty != "")
        {
            qty = Convert.ToInt16(txtqty);
        }
        else
        {
            qty = 0;
        }
        if (txtfreeqty != "")
        {
            freeqty = Convert.ToInt16(txtfreeqty);
        }
        else
        {
            freeqty = 0;
        }
        int totqty = qty + freeqty;
        if (totqty > 0)
        {
            purcst = totamt / totqty;
        }
        else
        {
            purcst = totamt;
        }
        return purcst;
    }

    protected void perc_textchange(object sender, EventArgs e)
    {
        Double lessper = 0.00;
        Double trendprice = 0.00;
        Double lessAmt, taxAmt, totAmt, CostPrice;
        TextBox t1, t2, t3, t4,s1,s2,s3;
        string trndprc, qty, freeqty;
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
            t1 = (TextBox)Page.FindControl("ctl00$ContentPlaceHolder1$txtLess" + t.ToString());
            t2 = (TextBox)Page.FindControl("ctl00$ContentPlaceHolder1$txtStax" + t.ToString());
            t3 = (TextBox)Page.FindControl("ctl00$ContentPlaceHolder1$txtCostPrice" + t.ToString());
            t4 = (TextBox)Page.FindControl("ctl00$ContentPlaceHolder1$txtTotalPrice" + t.ToString());

            s1 = (TextBox)Page.FindControl("ctl00$ContentPlaceHolder1$txtUnitPrice" + t.ToString());
            s2 = (TextBox)Page.FindControl("ctl00$ContentPlaceHolder1$txtQty" + t.ToString());
            s3 = (TextBox)Page.FindControl("ctl00$ContentPlaceHolder1$txtfreeqty" + t.ToString());

            trndprc = s1.Text;
            qty = s2.Text;
            freeqty = s3.Text;

            lessAmt = calcLess(trndprc);
            taxAmt = calStax(trndprc);
            totAmt = calculateTotAmt(trndprc, qty,lessAmt.ToString(),taxAmt.ToString());
            CostPrice = calcPurchaseCost(totAmt, qty, freeqty);
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
            if (CostPrice > 0.00)
            {
                t3.Text = CostPrice.ToString();
            }
            else
            {
                t3.Text = "";
            }
            if (totAmt > 0.00)
            {
                t4.Text = totAmt.ToString();
            }
            else
            {
                t4.Text = "";
            }
        }

    }

    protected void calculateNetAmt()
    {
        TextBox t1;
        double total = 0.0;
        
        for (int i = 1; i <= 12; i++)
        {
            t1 = (TextBox)Page.FindControl("ctl00$ContentPlaceHolder1$txtTotalPrice" + i.ToString());
            if (t1.Text != "")
                total = total + Convert.ToDouble(t1.Text);
        }
        txtNetAmt.Text = total.ToString();
    }
    private void TaxDetails(string medId, string mgfId, TextBox txtHsnCode, TextBox txtCgstRt, TextBox txtSgstRt, TextBox txtIgstRt, TextBox txtLessAmt, Label lblConvrtFactor)
    {
        DataTable dt = theHelper.getTaxDetails(Session["CoCode"].ToString().Trim(), Session["YearCode"].ToString().Trim(), medId, mgfId);
        if (dt.Rows.Count > 0)
        {
            if (ddlgsttype.SelectedValue == "S")
            {
                if (dt.Rows[0]["ApplPurWithoutGst"].ToString().Trim() == "0")
                {
                    txtCgstRt.Text = dt.Rows[0]["CGSTRate"].ToString();
                    txtSgstRt.Text = dt.Rows[0]["SGSTRate"].ToString();
                    txtIgstRt.Text = "0.00";
                }
                else
                {
                    txtCgstRt.Text = "0.00";
                    txtSgstRt.Text = "0.00";
                    txtIgstRt.Text = "0.00";
                }
                
            }
            else
            {
                txtCgstRt.Text = "0.00";
                txtSgstRt.Text = "0.00";
                txtIgstRt.Text = dt.Rows[0]["IGSTRate"].ToString();
            }
            txtLessAmt.Text = "0.00";
            txtHsnCode.Text = dt.Rows[0]["HSNCode"].ToString();
            lblConvrtFactor.Text = dt.Rows[0]["ConversionFactor"].ToString();
        }
        else
        {
            txtHsnCode.Text = "";
            txtCgstRt.Text = "0.00";
            txtSgstRt.Text = "0.00";
            txtIgstRt.Text = "0.00";
            txtLessAmt.Text = "0.00";
        }
    }

    protected void ddlMed1_SelectedIndexChanged(object sender, EventArgs e)
    {
        string type = DropDownList2.SelectedValue.ToString();
        /*if (type == "M")
        {*/
            MfgGrpSgrpFill(ddlMedi1.SelectedValue, 1);
        /*}*/
        txtLess1.Enabled = false;
        txtStax1.Enabled = false;
        TaxDetails(ddlMedi1.SelectedValue, ddlMfg1.SelectedValue, txtHsnCode1, txtCgstRt1, txtSgstRt1, txtIgstRt1, txtLess1, lblConvrtFactor1);
    }

    protected void ddlMed2_SelectedIndexChanged(object sender, EventArgs e)
    {
        string type = DropDownList2.SelectedValue.ToString();
        //if (type == "M")
        //{
            MfgGrpSgrpFill(ddlMedi2.SelectedValue, 2);
        //}
        txtLess2.Enabled = false;
        txtStax2.Enabled = false;
        TaxDetails(ddlMedi2.SelectedValue, ddlMfg2.SelectedValue, txtHsnCode2, txtCgstRt2, txtSgstRt2, txtIgstRt2, txtLess2, lblConvrtFactor2);
    }

    protected void ddlMed3_SelectedIndexChanged(object sender, EventArgs e)
    {
        string type = DropDownList2.SelectedValue.ToString();
        //if (type == "M")
        //{
            MfgGrpSgrpFill(ddlMedi3.SelectedValue, 3);
        //}
        txtLess3.Enabled = false;
        txtStax3.Enabled = false;
        TaxDetails(ddlMedi3.SelectedValue, ddlMfg3.SelectedValue, txtHsnCode3, txtCgstRt3, txtSgstRt3, txtIgstRt3, txtLess3, lblConvrtFactor3);
    }

    protected void ddlMed4_SelectedIndexChanged(object sender, EventArgs e)
    {
        string type = DropDownList2.SelectedValue.ToString();
        //if (type == "M")
        //{
            MfgGrpSgrpFill(ddlMedi4.SelectedValue, 4);
        //}
        txtLess4.Enabled = false;
        txtStax4.Enabled = false;
        TaxDetails(ddlMedi4.SelectedValue, ddlMfg4.SelectedValue, txtHsnCode4, txtCgstRt4, txtSgstRt4, txtIgstRt4, txtLess4, lblConvrtFactor4);
    }

    protected void ddlMed5_SelectedIndexChanged(object sender, EventArgs e)
    {
        string type = DropDownList2.SelectedValue.ToString();
        //if (type == "M")
        //{
            MfgGrpSgrpFill(ddlMedi5.SelectedValue, 5);
        //}
        txtLess5.Enabled = false;
        txtStax5.Enabled = false;
        TaxDetails(ddlMedi5.SelectedValue, ddlMfg5.SelectedValue, txtHsnCode5, txtCgstRt5, txtSgstRt5, txtIgstRt5, txtLess5, lblConvrtFactor5);
    }

    protected void ddlMed6_SelectedIndexChanged(object sender, EventArgs e)
    {
        string type = DropDownList2.SelectedValue.ToString();
        //if (type == "M")
        //{
            MfgGrpSgrpFill(ddlMedi6.SelectedValue, 6);
        //}
        txtLess6.Enabled = false;
        txtStax6.Enabled = false;
        TaxDetails(ddlMedi6.SelectedValue, ddlMfg6.SelectedValue, txtHsnCode6, txtCgstRt6, txtSgstRt6, txtIgstRt6, txtLess6, lblConvrtFactor6);
    }

    protected void ddlMed7_SelectedIndexChanged(object sender, EventArgs e)
    {
        string type = DropDownList2.SelectedValue.ToString();
        //if (type == "M")
        //{
            MfgGrpSgrpFill(ddlMedi7.SelectedValue, 7);
        //}
        txtLess7.Enabled = false;
        txtStax7.Enabled = false;
        TaxDetails(ddlMedi7.SelectedValue, ddlMfg7.SelectedValue, txtHsnCode7, txtCgstRt7, txtSgstRt7, txtIgstRt7, txtLess7, lblConvrtFactor7);
    }

    protected void ddlMed8_SelectedIndexChanged(object sender, EventArgs e)
    {
        string type = DropDownList2.SelectedValue.ToString();
        //if (type == "M")
        //{
            MfgGrpSgrpFill(ddlMedi8.SelectedValue, 8);
        //}
        txtLess8.Enabled = false;
        txtStax8.Enabled = false;
        TaxDetails(ddlMedi8.SelectedValue, ddlMfg8.SelectedValue, txtHsnCode8, txtCgstRt8, txtSgstRt8, txtIgstRt8, txtLess8, lblConvrtFactor8);
    }

    protected void ddlMed9_SelectedIndexChanged(object sender, EventArgs e)
    {
        string type = DropDownList2.SelectedValue.ToString();
        //if (type == "M")
        //{
            MfgGrpSgrpFill(ddlMedi9.SelectedValue, 9);
        //}
        txtLess9.Enabled = false;
        txtStax9.Enabled = false;
        TaxDetails(ddlMedi9.SelectedValue, ddlMfg9.SelectedValue, txtHsnCode9, txtCgstRt9, txtSgstRt9, txtIgstRt9, txtLess9, lblConvrtFactor9);
    }

    protected void ddlMed10_SelectedIndexChanged(object sender, EventArgs e)
    {
        string type = DropDownList2.SelectedValue.ToString();
        //if (type == "M")
        //{
            MfgGrpSgrpFill(ddlMedi10.SelectedValue, 10);
        //}
        txtLess10.Enabled = false;
        txtStax10.Enabled = false;
        TaxDetails(ddlMedi10.SelectedValue, ddlMfg10.SelectedValue, txtHsnCode10, txtCgstRt10, txtSgstRt10, txtIgstRt10, txtLess10, lblConvrtFactor10);
    }

    protected void ddlMed11_SelectedIndexChanged(object sender, EventArgs e)
    {
        string type = DropDownList2.SelectedValue.ToString();
        //if (type == "M")
        //{
            MfgGrpSgrpFill(ddlMedi11.SelectedValue, 11);
        //}
        txtLess11.Enabled = false;
        txtStax11.Enabled = false;
        TaxDetails(ddlMedi11.SelectedValue, ddlMfg11.SelectedValue, txtHsnCode11, txtCgstRt11, txtSgstRt11, txtIgstRt11, txtLess11, lblConvrtFactor11);
    }

    protected void ddlMed12_SelectedIndexChanged(object sender, EventArgs e)
    {
        string type = DropDownList2.SelectedValue.ToString();
        //if (type == "M")
        //{
            MfgGrpSgrpFill(ddlMedi12.SelectedValue, 12);
        //}
        txtLess12.Enabled = false;
        txtStax12.Enabled = false;
        TaxDetails(ddlMedi12.SelectedValue, ddlMfg12.SelectedValue, txtHsnCode12, txtCgstRt12, txtSgstRt12, txtIgstRt12, txtLess12, lblConvrtFactor12);
    }
    protected void Button5_Click(object sender, EventArgs e)
    {

        Session["back_to_purchase"] = "1";
        Response.Redirect("../Medicine/MedicineMaster.aspx");
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
            ddlMfg.DataSource = theHelper.DropdownMfg(Session["CoCode"].ToString().Trim(),MedCode);
            ddlMfg.DataTextField = "MName";
            ddlMfg.DataValueField = "MCode";
            ddlMfg.DataBind();
           

            ddlMediGrp.Items.Clear();
            ddlMediGrp.DataSource = theHelper.DropdownGrp(Session["CoCode"].ToString().Trim(),MedCode);
            ddlMediGrp.DataTextField = "MedicineGroupName";
            ddlMediGrp.DataValueField = "MedicineGroupID";
            ddlMediGrp.DataBind();
           // ddlMediGrp.Items.Insert(0, new ListItem("--Select--", "0"));

            ddlMediSubGrp.Items.Clear();
            ddlMediSubGrp.DataSource = theHelper.DropdownSubGrp(Session["CoCode"].ToString().Trim(),MedCode);
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
    protected void ddlFrom_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlFrom.SelectedValue == ddlTo.SelectedValue)
            ddlgsttype.SelectedValue = "S";
        else ddlgsttype.SelectedValue = "I";
        ddlFrom.Focus();
    }
    protected void ddlTo_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlFrom.SelectedValue == ddlTo.SelectedValue)
            ddlgsttype.SelectedValue = "S";
        else ddlgsttype.SelectedValue = "I";
        ddlTo.Focus();
    }
}