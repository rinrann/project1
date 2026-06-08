using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Data;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Configuration;
using System.Collections;
using System.Drawing;
using System.Globalization;
 

public partial class Pathology_ReagentEntry : System.Web.UI.Page
{
    PH_ReagentEntry thereagentEntry = new PH_ReagentEntry(ConfigurationSettings.AppSettings["ConnectionString"].ToString());
    MD_ReturnMedicine therPurRet = new MD_ReturnMedicine(ConfigurationSettings.AppSettings["ConnectionString"].ToString());
    TextBox t1, t2, t3, t4, t5, t6;
    DropDownList d1, d2, d3, d4;
    String Date = DateTime.Now.ToString("dd/MM/yyyy");
    protected void Page_Load(object sender, EventArgs e)
    {
        Page.Title = "Reagent / Kit Entry";
        if (Session["userId"] == null)
        {
            Response.Redirect("../LoginPage.aspx");
        }
        if (!IsPostBack)
        {

            DropdownFill();
            //String Date = DateTime.Now.ToString("dd/MM/yyyy");
            txtdate.Text = Date.ToString();
        }
      

    }


    public void DropdownFill()
    {
        string compcode = Session["CoCode"].ToString().Trim();
        string yearcode = Session["YearCode"].ToString().Trim();
        DataTable dt = thereagentEntry.GetDocNo(compcode, yearcode);
        TxtDocno.Text = dt.Rows[0][0].ToString();

        ddlSupplier.DataSource = thereagentEntry.DropdownSupplier(compcode);
        ddlSupplier.DataTextField = "SName";
        ddlSupplier.DataValueField = "SCode";
        ddlSupplier.DataBind();
        ddlSupplier.Items.Insert(0, new ListItem("--Select--", "0"));
        DropDownList d1, d2, d3, d4;
        for (int i = 1,j=2,k=3,l=4; i <=41; i=i+4,j=j+4, k=k+4,l=l+4)
        {
            d1 = (DropDownList)Page.FindControl("ctl00$ContentPlaceHolder1$DropDownList" + i.ToString());
            d1.DataSource = thereagentEntry.Dropdown_Reagent(compcode);
            d1.DataTextField = "iname";
            d1.DataValueField = "icode";
            d1.DataBind();
            d1.Items.Insert(0, new ListItem("--Select--", "0"));

            /*d2 = (DropDownList)Page.FindControl("ctl00$ContentPlaceHolder1$DropDownList" + j.ToString());
            d2.DataSource = thereagentEntry.DropdownCompany();
            d2.DataTextField = "CName";
            d2.DataValueField = "CCode";
            d2.DataBind();
            d2.Items.Insert(0, new ListItem("--Select--", "0"));

            d3 = (DropDownList)Page.FindControl("ctl00$ContentPlaceHolder1$DropDownList" +k.ToString());
            d3.DataSource = thereagentEntry.DropdownSupplier();
            d3.DataTextField = "SName";
            d3.DataValueField = "SCode";
            d3.DataBind();
            d3.Items.Insert(0, new ListItem("--Select--", "0"));

          
            d4 = (DropDownList)Page.FindControl("ctl00$ContentPlaceHolder1$DropDownList" +l.ToString());
            d4.DataSource = thereagentEntry.DropdownManufacturer();
            d4.DataTextField = "MName";
            d4.DataValueField = "MCode";
            d4.DataBind();
            d4.Items.Insert(0, new ListItem("--Select--", "0"));*/
        }

    }
    protected decimal totalAmt()
    {
        decimal amt = 0;
        for (int i = 0, t = 1, d = 1; i < 11; i++, t = t + 6, d = d + 4)
        {
            t5 = (TextBox)Page.FindControl("ctl00$ContentPlaceHolder1$TextBox" + (t + 4).ToString());
            if (t5.Text == "" || t5.Text.Trim() == "0") { }
            else { amt = amt + Convert.ToDecimal(t5.Text.Trim()); }
        }
        return amt;
    }
    protected void Button1_Click(object sender, EventArgs e)
    {
        string id;
        string compcode = Session["CoCode"].ToString().Trim();
        string yearcode = Session["YearCode"].ToString().Trim();
         if (HiddenField1.Value != "")
        {
            id = HiddenField1.Value;
        }
        else
        {
            id = "null";
        }
        Decimal total = totalAmt();
        if (Button1.Text == "Submit")
        {
            DataTable dt = thereagentEntry.GetDocNo(compcode, yearcode);
            for (int i = 0,t=1,d=1; i < 11; i++,t=t+6,d=d+4)
            {
                
                //DataTable dt = thereagentEntry.Generatecode();
                t1 = (TextBox)Page.FindControl("ctl00$ContentPlaceHolder1$TextBox" + t.ToString());
                t2 = (TextBox)Page.FindControl("ctl00$ContentPlaceHolder1$TextBox" + (t+1).ToString());
                t3 = (TextBox)Page.FindControl("ctl00$ContentPlaceHolder1$TextBox" + (t+2).ToString());
                t4 = (TextBox)Page.FindControl("ctl00$ContentPlaceHolder1$TextBox" + (t+3).ToString());
                t5 = (TextBox)Page.FindControl("ctl00$ContentPlaceHolder1$TextBox" + (t+4).ToString());
                t6 = (TextBox)Page.FindControl("ctl00$ContentPlaceHolder1$TextBox" + (t+5).ToString());
                System.Globalization.DateTimeFormatInfo dtf = new DateTimeFormatInfo();
              
                 d1 = (DropDownList)Page.FindControl("ctl00$ContentPlaceHolder1$DropDownList" + d.ToString());
                d2= (DropDownList)Page.FindControl("ctl00$ContentPlaceHolder1$DropDownList" + (d+1).ToString());
                d3 = (DropDownList)Page.FindControl("ctl00$ContentPlaceHolder1$DropDownList" + (d+2).ToString());
               d4 = (DropDownList)Page.FindControl("ctl00$ContentPlaceHolder1$DropDownList" + (d+3).ToString());
               if (t1.Text != "" && t2.Text != "" && t4.Text != "" && t5.Text != "" && t6.Text != "")
               {
                   DateTime testdate = DateTime.ParseExact(t6.Text, "dd/MM/yyyy", dtf);
                   DateTime testdate1 = DateTime.ParseExact(txtdate.Text, "dd/MM/yyyy", dtf);

                   thereagentEntry.InsertReagentEntry(compcode, yearcode, d, dt.Rows[0][0].ToString(), d1.SelectedValue, t1.Text, ddlSupplier.SelectedValue, t2.Text, t3.Text, t4.Text, t5.Text, total.ToString(), testdate.ToString("yyyy-MM-dd"), Session["userId"].ToString(), testdate1.ToString("yyyy-MM-dd"));
                   //thereagentEntry.InsertReagentEntryMap(dt.Rows[0][0].ToString(), Convert.ToString(testdate1));
               }
               else
                   break;
            }
            ScriptManager.RegisterStartupScript(this, this.GetType(), "msg", "alert('Inserted Successfully !');", true);
        }
        else
        {

            System.Globalization.DateTimeFormatInfo dtf = new DateTimeFormatInfo();
            DateTime testdate1 = DateTime.ParseExact(txtdate.Text, "dd/MM/yyyy", dtf);
            thereagentEntry.UpdateReagentEntryMap(compcode, yearcode, TxtDocno.Text, testdate1.ToString("yyyy-MM-dd"), total.ToString());
            thereagentEntry.DeleteDetail(compcode, yearcode, TxtDocno.Text);
            for (int i = 0, t = 1, d = 1; i < 11; i++, t = t + 6, d = d + 4)
            {
               
              

                //DataTable dt = thereagentEntry.Generatecode();
                t1 = (TextBox)Page.FindControl("ctl00$ContentPlaceHolder1$TextBox" + t.ToString());
                t2 = (TextBox)Page.FindControl("ctl00$ContentPlaceHolder1$TextBox" + (t + 1).ToString());
                t3 = (TextBox)Page.FindControl("ctl00$ContentPlaceHolder1$TextBox" + (t + 2).ToString());
                t4 = (TextBox)Page.FindControl("ctl00$ContentPlaceHolder1$TextBox" + (t + 3).ToString());
                t5 = (TextBox)Page.FindControl("ctl00$ContentPlaceHolder1$TextBox" + (t + 4).ToString());
                t6 = (TextBox)Page.FindControl("ctl00$ContentPlaceHolder1$TextBox" + (t + 5).ToString());

                d1 = (DropDownList)Page.FindControl("ctl00$ContentPlaceHolder1$DropDownList" + d.ToString());
                d2 = (DropDownList)Page.FindControl("ctl00$ContentPlaceHolder1$DropDownList" + (d + 1).ToString());
                d3 = (DropDownList)Page.FindControl("ctl00$ContentPlaceHolder1$DropDownList" + (d + 2).ToString());
                d4 = (DropDownList)Page.FindControl("ctl00$ContentPlaceHolder1$DropDownList" + (d + 3).ToString());
                HiddenField h = (HiddenField)Page.FindControl("ctl00$ContentPlaceHolder1$HiddenField" + (i + 1).ToString());
                if (t1.Text != "" && t2.Text != "" /*&& t3.Text != "" */&& t4.Text != "" && t5.Text != "" /*&& t6.Text != ""*/)
                {
                    DateTime testdate = DateTime.ParseExact(t6.Text, "dd/MM/yyyy", dtf);
                    //thereagentEntry.UpdateReagentEntry(h.Value, d1.SelectedValue, t1.Text, d2.SelectedValue, d3.SelectedValue, d4.SelectedValue, t2.Text, t3.Text, t4.Text, t5.Text, Convert.ToString(testdate), Convert.ToString(testdate1));
                    thereagentEntry.InsertReagentEntry(compcode, yearcode, d, TxtDocno.Text, d1.SelectedValue, t1.Text, ddlSupplier.SelectedValue, t2.Text, t3.Text, t4.Text, t5.Text, total.ToString(), testdate.ToString("yyyy-MM-dd"), Session["userId"].ToString(), testdate1.ToString("yyyy-MM-dd"));
                }
            }

            ScriptManager.RegisterStartupScript(this, this.GetType(), "msg", "alert('Updated Successfully !');", true);
            Button1.Text = "Submit";

        }

    ResetAllFields();
    }
    public void ResetAllFields()
    {
        for (int i = 0, t = 1, d = 1; i < 11; i++, t = t + 6, d = d + 4)
        {
            t1 = (TextBox)Page.FindControl("ctl00$ContentPlaceHolder1$TextBox" + t.ToString());
            t2 = (TextBox)Page.FindControl("ctl00$ContentPlaceHolder1$TextBox" + (t + 1).ToString());
            t3 = (TextBox)Page.FindControl("ctl00$ContentPlaceHolder1$TextBox" + (t + 2).ToString());
            t4 = (TextBox)Page.FindControl("ctl00$ContentPlaceHolder1$TextBox" + (t + 3).ToString());
            t5 = (TextBox)Page.FindControl("ctl00$ContentPlaceHolder1$TextBox" + (t + 4).ToString());
            t6 = (TextBox)Page.FindControl("ctl00$ContentPlaceHolder1$TextBox" + (t + 5).ToString());

            d1 = (DropDownList)Page.FindControl("ctl00$ContentPlaceHolder1$DropDownList" + d.ToString());
            d2 = (DropDownList)Page.FindControl("ctl00$ContentPlaceHolder1$DropDownList" + (d + 1).ToString());
            d3 = (DropDownList)Page.FindControl("ctl00$ContentPlaceHolder1$DropDownList" + (d + 2).ToString());
            d4 = (DropDownList)Page.FindControl("ctl00$ContentPlaceHolder1$DropDownList" + (d + 3).ToString());
            t1.Text = ""; t2.Text = ""; t3.Text = ""; t4.Text = ""; t5.Text = ""; t6.Text = "";
            d1.SelectedIndex = 0; d2.SelectedIndex = 0; d3.SelectedIndex = 0; d4.SelectedIndex = 0;
            txtdate.Text = Date.ToString();ddlSupplier.SelectedIndex = 0;
            DataTable dt = thereagentEntry.GetDocNo(Session["CoCode"].ToString().Trim(), Session["YearCode"].ToString().Trim());
            TxtDocno.Text = dt.Rows[0][0].ToString();
        }
    }
    protected void Button3_Click(object sender, EventArgs e)
    {
         System.Globalization.DateTimeFormatInfo dtf = new DateTimeFormatInfo();
        DateTime testdate = DateTime.ParseExact(txtdate.Text, "dd/MM/yyyy", dtf);
        DataTable dt = thereagentEntry.GetReagentDtls( Session["CoCode"].ToString().Trim(), Session["YearCode"].ToString().Trim(),TxtDocno.Text.Trim());
        ddlSupplier.SelectedValue = dt.Rows[0]["SupplierId"].ToString().Trim();
        for (int i = 0, t = 1, d = 1; i < dt.Rows.Count; i++, t = t + 6, d = d + 4)
        {
            t1 = (TextBox)Page.FindControl("ctl00$ContentPlaceHolder1$TextBox" + t.ToString());
            t2 = (TextBox)Page.FindControl("ctl00$ContentPlaceHolder1$TextBox" + (t + 1).ToString());
            t3 = (TextBox)Page.FindControl("ctl00$ContentPlaceHolder1$TextBox" + (t + 2).ToString());
            t4 = (TextBox)Page.FindControl("ctl00$ContentPlaceHolder1$TextBox" + (t + 3).ToString());
            t5 = (TextBox)Page.FindControl("ctl00$ContentPlaceHolder1$TextBox" + (t + 4).ToString());
            t6 = (TextBox)Page.FindControl("ctl00$ContentPlaceHolder1$TextBox" + (t + 5).ToString());

            d1 = (DropDownList)Page.FindControl("ctl00$ContentPlaceHolder1$DropDownList" + d.ToString());
            d2 = (DropDownList)Page.FindControl("ctl00$ContentPlaceHolder1$DropDownList" + (d + 1).ToString());
            d3 = (DropDownList)Page.FindControl("ctl00$ContentPlaceHolder1$DropDownList" + (d + 2).ToString());
            d4 = (DropDownList)Page.FindControl("ctl00$ContentPlaceHolder1$DropDownList" + (d + 3).ToString());
            t1.Text = dt.Rows[i]["BatchNo"].ToString(); t2.Text = dt.Rows[i]["Quantity"].ToString(); //t3.Text = dt.Rows[i]["BonusQty"].ToString(); 
            t4.Text = dt.Rows[i]["Price"].ToString();
            t5.Text = dt.Rows[i]["TotalPrice"].ToString(); t6.Text = dt.Rows[i]["d"].ToString();
            d1.SelectedValue = dt.Rows[i]["ReagentID"].ToString().Trim();  
            
            HiddenField h = (HiddenField)Page.FindControl("ctl00$ContentPlaceHolder1$HiddenField" + (i+1).ToString());
            h.Value = dt.Rows[i]["ID"].ToString();

        }
        Button1.Text = "Update";
    }
    protected void Button4_Click(object sender, EventArgs e)
    {
        ResetAllFields();
    }
}