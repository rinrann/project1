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

public partial class Medicine_MedicineSellInsert : System.Web.UI.Page
{
    MedicineSellInsert theHelper = new MedicineSellInsert(ConfigurationSettings.AppSettings["ConnectionString"].ToString());
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            DropDownFill();
        }
    }


    public void subgroup(int j,string value)
    {
        DropDownList d1 = (DropDownList)Page.FindControl("ctl00$ContentPlaceHolder1$ddlMediSubGrp" + j.ToString());
        d1.DataSource = theHelper.DropdownSubGroup(value);
        d1.DataTextField = "SubGrName";
        d1.DataValueField = "ID";
        d1.DataBind();
        d1.Items.Insert(0, new ListItem("--Select--", "0"));
    }
  
    protected void ddlMediGrp1_SelectedIndexChanged(object sender, EventArgs e)
    {
        ddlMediSubGrp1.DataSource = theHelper.DropdownSubGroup(ddlMediGrp1.SelectedValue);
        ddlMediSubGrp1.DataTextField = "SubGrName";
        ddlMediSubGrp1.DataValueField = "ID";
        ddlMediSubGrp1.DataBind();
        ddlMediSubGrp1.Items.Insert(0, new ListItem("--Select--", "0"));
    }
    protected void ddlMediGrp2_SelectedIndexChanged(object sender, EventArgs e)
    {
        ddlMediSubGrp2.DataSource = theHelper.DropdownSubGroup(ddlMediGrp2.SelectedValue);
        ddlMediSubGrp2.DataTextField = "SubGrName";
        ddlMediSubGrp2.DataValueField = "ID";
        ddlMediSubGrp2.DataBind();
        ddlMediSubGrp2.Items.Insert(0, new ListItem("--Select--", "0"));
    }
    protected void ddlMediGrp3_SelectedIndexChanged(object sender, EventArgs e)
    {
        ddlMediSubGrp3.DataSource = theHelper.DropdownSubGroup(ddlMediGrp3.SelectedValue);
        ddlMediSubGrp3.DataTextField = "SubGrName";
        ddlMediSubGrp3.DataValueField = "ID";
        ddlMediSubGrp3.DataBind();
        ddlMediSubGrp3.Items.Insert(0, new ListItem("--Select--", "0"));
    }
    protected void ddlMediGrp4_SelectedIndexChanged(object sender, EventArgs e)
    {
        ddlMediSubGrp4.DataSource = theHelper.DropdownSubGroup(ddlMediGrp4.SelectedValue);
        ddlMediSubGrp4.DataTextField = "SubGrName";
        ddlMediSubGrp4.DataValueField = "ID";
        ddlMediSubGrp4.DataBind();
        ddlMediSubGrp4.Items.Insert(0, new ListItem("--Select--", "0"));
    }
    protected void ddlMediGrp5_SelectedIndexChanged(object sender, EventArgs e)
    {
        ddlMediSubGrp5.DataSource = theHelper.DropdownSubGroup(ddlMediGrp5.SelectedValue);
        ddlMediSubGrp5.DataTextField = "SubGrName";
        ddlMediSubGrp5.DataValueField = "ID";
        ddlMediSubGrp5.DataBind();
        ddlMediSubGrp5.Items.Insert(0, new ListItem("--Select--", "0"));
    }
    protected void ddlMediGrp6_SelectedIndexChanged(object sender, EventArgs e)
    {

        ddlMediSubGrp6.DataSource = theHelper.DropdownSubGroup(ddlMediGrp6.SelectedValue);
        ddlMediSubGrp6.DataTextField = "SubGrName";
        ddlMediSubGrp6.DataValueField = "ID";
        ddlMediSubGrp6.DataBind();
        ddlMediSubGrp6.Items.Insert(0, new ListItem("--Select--", "0"));
    }
    protected void ddlMediGrp7_SelectedIndexChanged(object sender, EventArgs e)
    {
        ddlMediSubGrp7.DataSource = theHelper.DropdownSubGroup(ddlMediGrp7.SelectedValue);
        ddlMediSubGrp7.DataTextField = "SubGrName";
        ddlMediSubGrp7.DataValueField = "ID";
        ddlMediSubGrp7.DataBind();
        ddlMediSubGrp7.Items.Insert(0, new ListItem("--Select--", "0"));
    }
    protected void ddlMediGrp8_SelectedIndexChanged(object sender, EventArgs e)
    {
        ddlMediSubGrp8.DataSource = theHelper.DropdownSubGroup(ddlMediGrp8.SelectedValue);
        ddlMediSubGrp8.DataTextField = "SubGrName";
        ddlMediSubGrp8.DataValueField = "ID";
        ddlMediSubGrp8.DataBind();
        ddlMediSubGrp8.Items.Insert(0, new ListItem("--Select--", "0"));
    }
    protected void ddlMediGrp9_SelectedIndexChanged(object sender, EventArgs e)
    {

        ddlMediSubGrp9.DataSource = theHelper.DropdownSubGroup(ddlMediGrp9.SelectedValue);
        ddlMediSubGrp9.DataTextField = "SubGrName";
        ddlMediSubGrp9.DataValueField = "ID";
        ddlMediSubGrp9.DataBind();
        ddlMediSubGrp9.Items.Insert(0, new ListItem("--Select--", "0"));
    }
    protected void ddlMediGrp10_SelectedIndexChanged(object sender, EventArgs e)
    {
        ddlMediSubGrp10.DataSource = theHelper.DropdownSubGroup(ddlMediGrp10.SelectedValue);
        ddlMediSubGrp10.DataTextField = "SubGrName";
        ddlMediSubGrp10.DataValueField = "ID";
        ddlMediSubGrp10.DataBind();
        ddlMediSubGrp10.Items.Insert(0, new ListItem("--Select--", "0"));
    }
    protected void ddlMediGrp11_SelectedIndexChanged(object sender, EventArgs e)
    {
        ddlMediSubGrp11.DataSource = theHelper.DropdownSubGroup(ddlMediGrp11.SelectedValue);
        ddlMediSubGrp11.DataTextField = "SubGrName";
        ddlMediSubGrp11.DataValueField = "ID";
        ddlMediSubGrp11.DataBind();
        ddlMediSubGrp11.Items.Insert(0, new ListItem("--Select--", "0"));
    }
    protected void ddlMediGrp12_SelectedIndexChanged(object sender, EventArgs e)
    {
        ddlMediSubGrp12.DataSource = theHelper.DropdownSubGroup(ddlMediGrp12.SelectedValue);
        ddlMediSubGrp12.DataTextField = "SubGrName";
        ddlMediSubGrp12.DataValueField = "ID";
        ddlMediSubGrp12.DataBind();
        ddlMediSubGrp12.Items.Insert(0, new ListItem("--Select--", "0"));
    }

    public void BatchNo(DropDownList ddl, DropDownList ddl1, DataTable dt)
    {
        ddl.DataSource = theHelper.GetPurchasePricePerUnit(ddl1.SelectedValue);
        ddl.DataTextField = "BatchNo";
        ddl.DataValueField = "BatchNo";      
        ddl.DataBind();
        ddl.Items.Insert(0, new ListItem("--Select--", "0"));
    }

    public void GetSellingUnit(TextBox t1,string value)
    {
        DataTable dt = theHelper.GetSellUnit(value);
        if (dt.Rows.Count > 0)
        {
            t1.Text = dt.Rows[0]["UnitName"].ToString();
        }
    }
    protected void ddlMedi1_SelectedIndexChanged(object sender, EventArgs e)
    {
        DataTable dt = theHelper.GetPurchasePricePerUnit(ddlMedi1.SelectedValue);
        if (dt.Rows.Count > 0)
        {
            dropdown(DropDownList1);
            BatchNo(DropDownList1, ddlMedi1, dt);
        }
        GetSellingUnit(TextBox1, ddlMedi1.SelectedValue);       
    }
    protected void ddlMedi2_SelectedIndexChanged(object sender, EventArgs e)
    {
        DataTable dt = theHelper.GetPurchasePricePerUnit(ddlMedi2.SelectedValue);
        if (dt.Rows.Count > 0)
        {
            dropdown(DropDownList2);
            BatchNo(DropDownList2, ddlMedi2, dt);
        }
        GetSellingUnit(TextBox2, ddlMedi2.SelectedValue);    
    }
    protected void ddlMedi3_SelectedIndexChanged(object sender, EventArgs e)
    {
        DataTable dt = theHelper.GetPurchasePricePerUnit(ddlMedi3.SelectedValue);
        if (dt.Rows.Count > 0)
        {
            dropdown(DropDownList3);
            BatchNo(DropDownList3, ddlMedi3, dt);
        }
        GetSellingUnit(TextBox3, ddlMedi3.SelectedValue);    
    }
    protected void ddlMedi4_SelectedIndexChanged(object sender, EventArgs e)
    {
        DataTable dt = theHelper.GetPurchasePricePerUnit(ddlMedi4.SelectedValue);
        if (dt.Rows.Count > 0)
        {
            dropdown(DropDownList4);
            BatchNo(DropDownList4, ddlMedi4, dt);
        }
        GetSellingUnit(TextBox4, ddlMedi4.SelectedValue);    
    }
    protected void ddlMedi5_SelectedIndexChanged(object sender, EventArgs e)
    {
        DataTable dt = theHelper.GetPurchasePricePerUnit(ddlMedi5.SelectedValue);
        if (dt.Rows.Count > 0)
        {
            dropdown(DropDownList5);
            BatchNo(DropDownList5, ddlMedi5, dt);
        }
        GetSellingUnit(TextBox5, ddlMedi5.SelectedValue);    
    }
    protected void ddlMedi6_SelectedIndexChanged(object sender, EventArgs e)
    {
        DataTable dt = theHelper.GetPurchasePricePerUnit(ddlMedi6.SelectedValue);
        if (dt.Rows.Count > 0)
        {
            dropdown(DropDownList6);
            BatchNo(DropDownList6, ddlMedi6, dt);
        }
        GetSellingUnit(TextBox6, ddlMedi6.SelectedValue);    
    }
    protected void ddlMedi7_SelectedIndexChanged(object sender, EventArgs e)
    {
        DataTable dt = theHelper.GetPurchasePricePerUnit(ddlMedi7.SelectedValue);
        if (dt.Rows.Count > 0)
        {
            dropdown(DropDownList7);
            BatchNo(DropDownList7, ddlMedi7, dt);
        }
        GetSellingUnit(TextBox7, ddlMedi7.SelectedValue);    
    }
    protected void ddlMedi8_SelectedIndexChanged(object sender, EventArgs e)
    {
        DataTable dt = theHelper.GetPurchasePricePerUnit(ddlMedi8.SelectedValue);
        if (dt.Rows.Count > 0)
        {
            dropdown(DropDownList8);
            BatchNo(DropDownList8, ddlMedi8, dt);
        }
        GetSellingUnit(TextBox8, ddlMedi8.SelectedValue);    
    }
    protected void ddlMedi9_SelectedIndexChanged(object sender, EventArgs e)
    {
        DataTable dt = theHelper.GetPurchasePricePerUnit(ddlMedi9.SelectedValue);
        if (dt.Rows.Count > 0)
        {
            dropdown(DropDownList9);
            BatchNo(DropDownList9, ddlMedi9, dt);
        }
        GetSellingUnit(TextBox9, ddlMedi9.SelectedValue);    
    }
    protected void ddlMedi10_SelectedIndexChanged(object sender, EventArgs e)
    {
        DataTable dt = theHelper.GetPurchasePricePerUnit(ddlMedi10.SelectedValue);
        if (dt.Rows.Count > 0)
        {
            dropdown(DropDownList10);
            BatchNo(DropDownList10, ddlMedi10, dt);
        }
        GetSellingUnit(TextBox10, ddlMedi10.SelectedValue);    
    }
    protected void ddlMedi11_SelectedIndexChanged(object sender, EventArgs e)
    {
        DataTable dt = theHelper.GetPurchasePricePerUnit(ddlMedi11.SelectedValue);
        if (dt.Rows.Count > 0)
        {
            dropdown(DropDownList11);
            BatchNo(DropDownList11, ddlMedi11, dt);
        }
        GetSellingUnit(TextBox11, ddlMedi11.SelectedValue);    
    }
    protected void ddlMedi12_SelectedIndexChanged(object sender, EventArgs e)
    {
        DataTable dt = theHelper.GetPurchasePricePerUnit(ddlMedi12.SelectedValue);
        if (dt.Rows.Count > 0)
        {
            dropdown(DropDownList12);
            BatchNo(DropDownList12, ddlMedi12, dt);
        }
        GetSellingUnit(TextBox12, ddlMedi12.SelectedValue);    
    }



    private void ResetAllFields()
    {
        TextBox t1, t2,t3; 
        DropDownList d1, d2, d3, d4;
        for (int i = 1, j = 2; i < 13; i++, j++)
        {
            t1 = (TextBox)Page.FindControl("ctl00$ContentPlaceHolder1$Calendar" + j.ToString());
            
            t2 = (TextBox)Page.FindControl("ctl00$ContentPlaceHolder1$txtTotalPrice" + i.ToString());
            t3 = (TextBox)Page.FindControl("ctl00$ContentPlaceHolder1$txtUnitPrice" + i.ToString());  
            d1 = (DropDownList)Page.FindControl("ctl00$ContentPlaceHolder1$ddlMediGrp" + i.ToString());
            d2 = (DropDownList)Page.FindControl("ctl00$ContentPlaceHolder1$ddlMediSubGrp" + i.ToString());
            d3 = (DropDownList)Page.FindControl("ctl00$ContentPlaceHolder1$ddlMedi" + i.ToString());
            d4 = (DropDownList)Page.FindControl("ctl00$ContentPlaceHolder1$DropDownList" + i.ToString());
            t3.Text = "";
            t1.Text = ""; t2.Text = ""; d1.SelectedIndex = -1; d2.SelectedIndex = -1; d3.SelectedIndex = -1; d4.SelectedIndex = -1;
        }
    }

  
    private void DropDownFill()
    {

        for (int i = 1; i <= 12; i++)
        {
            DropDownList d1 = (DropDownList)Page.FindControl("ctl00$ContentPlaceHolder1$ddlMediSubGrp" + i.ToString());
            DropDownList d2 = (DropDownList)Page.FindControl("ctl00$ContentPlaceHolder1$ddlMedi" + i.ToString());
            DropDownList d3 = (DropDownList)Page.FindControl("ctl00$ContentPlaceHolder1$DropDownList" + i.ToString());
            d1.Items.Insert(0, new ListItem("--Select--", "0"));
            d2.Items.Insert(0, new ListItem("--Select--", "0"));
            d3.Items.Insert(0, new ListItem("--Select--", "0"));               
        }

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
        this.ddlMediGrp12.Items.Insert(0, new ListItem("--Select--", "0"));

    }


    public void dropdown(DropDownList ddl)
    {
        ddl.DataSource = theHelper.GetPurchasePricePerUnit("0");
        ddl.DataTextField = "BatchNo";
        ddl.DataValueField = "BatchNo";
        ddl.DataBind();
        ddl.Items.Insert(0, new ListItem("--Select--", "0"));
    }

    protected void Button1_Click(object sender, EventArgs e)
    {
        TextBox t1,t2;
        DropDownList d1, d2, d3, d4;
        System.Globalization.DateTimeFormatInfo dtf = new DateTimeFormatInfo();
       
        try
        {
            if (Button1.Text == "Submit")
            {
                for (int i = 1, j = 2; i < 13; i++, j++)
                {
                    t1 = (TextBox)Page.FindControl("ctl00$ContentPlaceHolder1$Calendar" + j.ToString());
                    t2 = (TextBox)Page.FindControl("ctl00$ContentPlaceHolder1$txtTotalPrice" + i.ToString());
                    d1 = (DropDownList)Page.FindControl("ctl00$ContentPlaceHolder1$ddlMediGrp" + i.ToString());
                    d2 = (DropDownList)Page.FindControl("ctl00$ContentPlaceHolder1$ddlMediSubGrp" + i.ToString());
                    d3 = (DropDownList)Page.FindControl("ctl00$ContentPlaceHolder1$ddlMedi" + i.ToString());
                    d4 = (DropDownList)Page.FindControl("ctl00$ContentPlaceHolder1$DropDownList" + i.ToString());
                    if (t1.Text != "" && d1.SelectedIndex != 0 && d2.SelectedIndex != 0)
                    {
                        DateTime testdate = DateTime.ParseExact(t1.Text, "dd/MM/yyyy", dtf);
                        if (theHelper.InsertSaleMedicine(txtPurchaseMedicineId.Text, d1.SelectedValue, d2.SelectedValue, d3.SelectedValue, d4.SelectedValue, testdate.ToString(), t2.Text) == true)
                        {
                            ScriptManager.RegisterStartupScript(this, this.GetType(), "msg", "alert('Inserted  Successfully !');", true);
                            // ResetAllFields();
                        }
                        else
                        {
                            ScriptManager.RegisterStartupScript(this, this.GetType(), "msg", "alert('Error in Inserted Data !');", true);
                        }
                    }
                    else
                        break;
                }
            }
            else
            {
                theHelper.DeleteSaleMedicine(txtPurchaseMedicineId.Text);

                for (int i = 1, j = 2; i < 13; i++, j++)
                {
                    t1 = (TextBox)Page.FindControl("ctl00$ContentPlaceHolder1$Calendar" + j.ToString());
                    t2 = (TextBox)Page.FindControl("ctl00$ContentPlaceHolder1$txtTotalPrice" + i.ToString());
                    d1 = (DropDownList)Page.FindControl("ctl00$ContentPlaceHolder1$ddlMediGrp" + i.ToString());
                    d2 = (DropDownList)Page.FindControl("ctl00$ContentPlaceHolder1$ddlMediSubGrp" + i.ToString());
                    d3 = (DropDownList)Page.FindControl("ctl00$ContentPlaceHolder1$ddlMedi" + i.ToString());
                    d4 = (DropDownList)Page.FindControl("ctl00$ContentPlaceHolder1$DropDownList" + i.ToString());
                    if (t1.Text != "" && d1.SelectedIndex != 0 && d2.SelectedIndex != 0)
                    {
                        DateTime testdate = DateTime.ParseExact(t1.Text, "dd/MM/yyyy", dtf);
                        if (theHelper.InsertSaleMedicine(txtPurchaseMedicineId.Text, d1.SelectedValue, d2.SelectedValue, d3.SelectedValue, d4.SelectedValue, testdate.ToString(), t2.Text) == true)
                        {
                            ScriptManager.RegisterStartupScript(this, this.GetType(), "msg", "alert('Updated  Successfully !');", true);
                        }
                        else
                        {
                            ScriptManager.RegisterStartupScript(this, this.GetType(), "msg", "alert('Error in Updated Data !');", true);
                        }
                    }
                    else
                        break;
                }
            }
           
        }
        catch (Exception ex)
        {
            lblError.ForeColor = System.Drawing.Color.Red;
            //lblError.Text = "Error in saving..";
            lblError.Text = ex.Message;
        }
        ResetAllFields();
    }
    protected void Button2_Click(object sender, EventArgs e)
    {
        ResetAllFields();
    }

    public void Medicine(int j,string Sub)
    {
        DropDownList d2 = (DropDownList)Page.FindControl("ctl00$ContentPlaceHolder1$ddlMedi" + j.ToString());
        d2.Items.Clear();
        d2.DataSource = theHelper.DropdownID4(Sub);
        d2.DataTextField = "MedicineName";
        d2.DataValueField = "MedicineID";
        d2.DataBind();
        d2.Items.Insert(0, new ListItem("--Select--", "0"));
    }

    protected void ddlMediSubGrp1_SelectedIndexChanged(object sender, EventArgs e)
    {
        ddlMedi1.Items.Clear();
        ddlMedi1.DataSource = theHelper.DropdownID4(ddlMediSubGrp1.SelectedValue);
        ddlMedi1.DataTextField = "MedicineName";
        ddlMedi1.DataValueField = "MedicineID";
        ddlMedi1.DataBind();
        this.ddlMedi1.Items.Insert(0, new ListItem("--Select--", "0"));
    }
    protected void ddlMediSubGrp2_SelectedIndexChanged(object sender, EventArgs e)
    {
        ddlMedi2.Items.Clear();
        ddlMedi2.DataSource = theHelper.DropdownID4(ddlMediSubGrp2.SelectedValue);
        ddlMedi2.DataTextField = "MedicineName";
        ddlMedi2.DataValueField = "MedicineID";
        ddlMedi2.DataBind();
        this.ddlMedi2.Items.Insert(0, new ListItem("--Select--", "0"));
    }
    protected void ddlMediSubGrp3_SelectedIndexChanged(object sender, EventArgs e)
    {
        ddlMedi3.Items.Clear();
        ddlMedi3.DataSource = theHelper.DropdownID4(ddlMediSubGrp3.SelectedValue);
        ddlMedi3.DataTextField = "MedicineName";
        ddlMedi3.DataValueField = "MedicineID";
        ddlMedi3.DataBind();
        this.ddlMedi3.Items.Insert(0, new ListItem("--Select--", "0"));
    }
    protected void ddlMediSubGrp4_SelectedIndexChanged(object sender, EventArgs e)
    {
        ddlMedi4.Items.Clear();
        ddlMedi4.DataSource = theHelper.DropdownID4(ddlMediSubGrp4.SelectedValue);
        ddlMedi4.DataTextField = "MedicineName";
        ddlMedi4.DataValueField = "MedicineID";
        ddlMedi4.DataBind();
        this.ddlMedi4.Items.Insert(0, new ListItem("--Select--", "0"));
    }
    protected void ddlMediSubGrp5_SelectedIndexChanged(object sender, EventArgs e)
    {
        ddlMedi5.Items.Clear();
        ddlMedi5.DataSource = theHelper.DropdownID4(ddlMediSubGrp5.SelectedValue);
        ddlMedi5.DataTextField = "MedicineName";
        ddlMedi5.DataValueField = "MedicineID";
        ddlMedi5.DataBind();
        this.ddlMedi5.Items.Insert(0, new ListItem("--Select--", "0"));
    }
    protected void ddlMediSubGrp6_SelectedIndexChanged(object sender, EventArgs e)
    {
        ddlMedi6.Items.Clear();
        ddlMedi6.DataSource = theHelper.DropdownID4(ddlMediSubGrp6.SelectedValue);
        ddlMedi6.DataTextField = "MedicineName";
        ddlMedi6.DataValueField = "MedicineID";
        ddlMedi6.DataBind();
        this.ddlMedi6.Items.Insert(0, new ListItem("--Select--", "0"));
    }
    protected void ddlMediSubGrp7_SelectedIndexChanged(object sender, EventArgs e)
    {
        ddlMedi7.Items.Clear();
        ddlMedi7.DataSource = theHelper.DropdownID4(ddlMediSubGrp7.SelectedValue);
        ddlMedi7.DataTextField = "MedicineName";
        ddlMedi7.DataValueField = "MedicineID";
        ddlMedi7.DataBind();
        this.ddlMedi7.Items.Insert(0, new ListItem("--Select--", "0"));
    }
    protected void ddlMediSubGrp8_SelectedIndexChanged(object sender, EventArgs e)
    {
        ddlMedi8.Items.Clear();
        ddlMedi8.DataSource = theHelper.DropdownID4(ddlMediSubGrp8.SelectedValue);
        ddlMedi8.DataTextField = "MedicineName";
        ddlMedi8.DataValueField = "MedicineID";
        ddlMedi8.DataBind();
        this.ddlMedi8.Items.Insert(0, new ListItem("--Select--", "0"));
    }
    protected void ddlMediSubGrp9_SelectedIndexChanged(object sender, EventArgs e)
    {
        ddlMedi9.Items.Clear();
        ddlMedi9.DataSource = theHelper.DropdownID4(ddlMediSubGrp9.SelectedValue);
        ddlMedi9.DataTextField = "MedicineName";
        ddlMedi9.DataValueField = "MedicineID";
        ddlMedi9.DataBind();
        this.ddlMedi9.Items.Insert(0, new ListItem("--Select--", "0"));
    }
    protected void ddlMediSubGrp10_SelectedIndexChanged(object sender, EventArgs e)
    {
        ddlMedi10.Items.Clear();
        ddlMedi10.DataSource = theHelper.DropdownID4(ddlMediSubGrp10.SelectedValue);
        ddlMedi10.DataTextField = "MedicineName";
        ddlMedi10.DataValueField = "MedicineID";
        ddlMedi10.DataBind();
        this.ddlMedi10.Items.Insert(0, new ListItem("--Select--", "0"));
    }
    protected void ddlMediSubGrp11_SelectedIndexChanged(object sender, EventArgs e)
    {
        ddlMedi11.Items.Clear();
        ddlMedi11.DataSource = theHelper.DropdownID4(ddlMediSubGrp11.SelectedValue);
        ddlMedi11.DataTextField = "MedicineName";
        ddlMedi11.DataValueField = "MedicineID";
        ddlMedi11.DataBind();
        this.ddlMedi11.Items.Insert(0, new ListItem("--Select--", "0"));
    }
    protected void ddlMediSubGrp12_SelectedIndexChanged(object sender, EventArgs e)
    {
        ddlMedi12.Items.Clear();
        ddlMedi12.DataSource = theHelper.DropdownID4(ddlMediSubGrp12.SelectedValue);
        ddlMedi12.DataTextField = "MedicineName";
        ddlMedi12.DataValueField = "MedicineID";
        ddlMedi12.DataBind();
        this.ddlMedi12.Items.Insert(0, new ListItem("--Select--", "0"));
    }
    protected void DropDownList1_SelectedIndexChanged(object sender, EventArgs e)
    {
        DataTable dt = theHelper.GetAlldetails(DropDownList1.SelectedValue);
        if (dt.Rows.Count > 0)
        {
            txtUnitPrice1.Text = dt.Rows[0]["PricePerUnit"].ToString();
            Calendar2.Text = dt.Rows[0]["ExDate"].ToString();
        }
    }
    protected void DropDownList2_SelectedIndexChanged(object sender, EventArgs e)
    {
        DataTable dt = theHelper.GetAlldetails(DropDownList2.SelectedValue);
        if (dt.Rows.Count > 0)
        {
            txtUnitPrice2.Text = dt.Rows[0]["PricePerUnit"].ToString();
            Calendar3.Text = dt.Rows[0]["ExDate"].ToString();
        }
    }
    protected void DropDownList3_SelectedIndexChanged(object sender, EventArgs e)
    {
        DataTable dt = theHelper.GetAlldetails(DropDownList3.SelectedValue);
        if (dt.Rows.Count > 0)
        {
            txtUnitPrice3.Text = dt.Rows[0]["PricePerUnit"].ToString();
            Calendar4.Text = dt.Rows[0]["ExDate"].ToString();
        }
    }
    protected void DropDownList4_SelectedIndexChanged(object sender, EventArgs e)
    {
        DataTable dt = theHelper.GetAlldetails(DropDownList4.SelectedValue);
        if (dt.Rows.Count > 0)
        {
            txtUnitPrice4.Text = dt.Rows[0]["PricePerUnit"].ToString();
            Calendar5.Text = dt.Rows[0]["ExDate"].ToString();
        }
    }
    protected void DropDownList5_SelectedIndexChanged(object sender, EventArgs e)
    {
        DataTable dt = theHelper.GetAlldetails(DropDownList5.SelectedValue);
        if (dt.Rows.Count > 0)
        {
            txtUnitPrice5.Text = dt.Rows[0]["PricePerUnit"].ToString();
            Calendar6.Text = dt.Rows[0]["ExDate"].ToString();
        }
    }
    protected void DropDownList6_SelectedIndexChanged(object sender, EventArgs e)
    {
        DataTable dt = theHelper.GetAlldetails(DropDownList6.SelectedValue);
        if (dt.Rows.Count > 0)
        {
            txtUnitPrice6.Text = dt.Rows[0]["PricePerUnit"].ToString();
            Calendar7.Text = dt.Rows[0]["ExDate"].ToString();
        }
    }
    protected void DropDownList7_SelectedIndexChanged(object sender, EventArgs e)
    {
        DataTable dt = theHelper.GetAlldetails(DropDownList7.SelectedValue);
        if (dt.Rows.Count > 0)
        {
            txtUnitPrice7.Text = dt.Rows[0]["PricePerUnit"].ToString();
            Calendar8.Text = dt.Rows[0]["ExDate"].ToString();
        }
    }
    protected void DropDownList8_SelectedIndexChanged(object sender, EventArgs e)
    {
        DataTable dt = theHelper.GetAlldetails(DropDownList8.SelectedValue);
        if (dt.Rows.Count > 0)
        {
            txtUnitPrice8.Text = dt.Rows[0]["PricePerUnit"].ToString();
            Calendar9.Text = dt.Rows[0]["ExDate"].ToString();
        }
    }
    protected void DropDownList9_SelectedIndexChanged(object sender, EventArgs e)
    {
        DataTable dt = theHelper.GetAlldetails(DropDownList9.SelectedValue);
        if (dt.Rows.Count > 0)
        {
            txtUnitPrice9.Text = dt.Rows[0]["PricePerUnit"].ToString();
            Calendar10.Text = dt.Rows[0]["ExDate"].ToString();
        }
    }
    protected void DropDownList10_SelectedIndexChanged(object sender, EventArgs e)
    {
        DataTable dt = theHelper.GetAlldetails(DropDownList10.SelectedValue);
        if (dt.Rows.Count > 0)
        {
            txtUnitPrice10.Text = dt.Rows[0]["PricePerUnit"].ToString();
            Calendar11.Text = dt.Rows[0]["ExDate"].ToString();
        }
    }
    protected void DropDownList11_SelectedIndexChanged(object sender, EventArgs e)
    {
        DataTable dt = theHelper.GetAlldetails(DropDownList11.SelectedValue);
        if (dt.Rows.Count > 0)
        {
            txtUnitPrice11.Text = dt.Rows[0]["PricePerUnit"].ToString();
            Calendar12.Text = dt.Rows[0]["ExDate"].ToString();
        }
    }
    protected void DropDownList12_SelectedIndexChanged(object sender, EventArgs e)
    {
        DataTable dt = theHelper.GetAlldetails(DropDownList12.SelectedValue);
        if (dt.Rows.Count > 0)
        {
            txtUnitPrice12.Text = dt.Rows[0]["PricePerUnit"].ToString();
            Calendar13.Text = dt.Rows[0]["ExDate"].ToString();
        }
    }

    public void FillDetails(DataTable dt)
    {
        DropDownList d1, d2, d3, d4;
        TextBox t1, t2, t3;
        for (int i = 0, j = 1; i < dt.Rows.Count; i++, j++)
        {
            t1 = (TextBox)Page.FindControl("ctl00$ContentPlaceHolder1$Calendar" + (j + 1).ToString());
            t2 = (TextBox)Page.FindControl("ctl00$ContentPlaceHolder1$txtUnitPrice" + j.ToString());
            t3 = (TextBox)Page.FindControl("ctl00$ContentPlaceHolder1$TextBox" + j.ToString());
            d1 = (DropDownList)Page.FindControl("ctl00$ContentPlaceHolder1$ddlMediGrp" + j.ToString());
            d2 = (DropDownList)Page.FindControl("ctl00$ContentPlaceHolder1$ddlMediSubGrp" + j.ToString());
            d3 = (DropDownList)Page.FindControl("ctl00$ContentPlaceHolder1$ddlMedi" + j.ToString());
            d4 = (DropDownList)Page.FindControl("ctl00$ContentPlaceHolder1$DropDownList" + j.ToString());
            d1.SelectedValue = dt.Rows[i]["MedicineGroupID"].ToString();

            subgroup(j, dt.Rows[i]["MedicineGroupID"].ToString());
            d2.SelectedValue = dt.Rows[i]["MedicineSubGrp"].ToString();

            Medicine(j, dt.Rows[i]["MedicineSubGrp"].ToString());
            d3.SelectedValue = dt.Rows[i]["MedicineID"].ToString();

            GetSellingUnit(t3, dt.Rows[i]["MedicineID"].ToString());
            BatchNo(d4, d3, dt);
            d4.SelectedValue = dt.Rows[i]["BatchNo"].ToString();
            t1.Text = dt.Rows[i]["ExDate"].ToString();
            t2.Text = dt.Rows[i]["PricePerUnit"].ToString();
        }
    }
    protected void Button4_Click(object sender, EventArgs e)
    {
        DataSet ds = theHelper.FillTable(txtPurchaseMedicineId.Text);
        ResetAllFields();

        if (ds.Tables[0].Rows.Count > 0)
        {
            TextBox t1;
            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
            {
                t1 = (TextBox)Page.FindControl("ctl00$ContentPlaceHolder1$txtTotalPrice" + (i + 1).ToString());
                t1.Text = ds.Tables[0].Rows[i]["SellingPrice"].ToString();
            }
            Button1.Text = "Update";
        }
        else
        {
            Button1.Text = "Submit";
        }
        if (ds.Tables[1].Rows.Count > 0)
        {
            FillDetails(ds.Tables[1]);
        } 
    }
}