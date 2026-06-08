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

 
public partial class Pathology_ReagentOrder : System.Web.UI.Page
{
    PH_ReagentOrder thereagent = new PH_ReagentOrder(ConfigurationSettings.AppSettings["ConnectionString"].ToString());
    protected void Page_Load(object sender, EventArgs e)
    {
        Page.Title = "Regeant Order";
        if (Session["userId"] == null)
        {
            Response.Redirect("../LoginPage.aspx");
        }
        if (!IsPostBack)
        {
            Dropdownfill();
            Tab1Func();
        }
        GridFill();
    }
    public void GridFill()
    {
        GridView1.DataSource = thereagent.GridChemical();
        GridView1.DataBind();
    }
    public void Dropdownfill()
    {
    //    thereagent = new ReagentOrder();
    //    DropDownList2.DataSource = thereagent.Dropdown_Reagent();
    //    DropDownList2.DataTextField = "RName";
    //    DropDownList2.DataValueField = "RCode";
    //    DropDownList2.DataBind();
    //    DropDownList2.Items.Insert(0, new ListItem("--Select--", "0"));

    }
    protected void Button1_Click(object sender, EventArgs e)
    {
       
         string reformattedDate;
        reformattedDate = DateTime.ParseExact(txtdate.Text, "dd/MM/yyyy", null).ToString("MM/dd/yyyy");
        string a = txttotal.Text.ToString();
        string id;
        if (TextBox4.Value != "")
        {
            id = TextBox4.Value.ToString();
        }
        else
        {
            id = "null";
        }
        if (Button1.Text == "Submit")
        {

            thereagent.InsertAppointment(thereagent.GeneratePurchaseID(), TextBox1.Text, txtid.Text, reformattedDate, txtpurchaseqty.Text, txtbonusqty.Text, txtprice.Text, txttotal.Text, Session["userId"].ToString());
            ScriptManager.RegisterStartupScript(this, this.GetType(), "msg", "alert('Inserted Successfully !');", true);


        }
        else
        {

            thereagent.UpdateAppointment(id, TextBox1.Text, txtid.Text, reformattedDate, txtpurchaseqty.Text, txtbonusqty.Text, txtprice.Text, txttotal.Text);
            ScriptManager.RegisterStartupScript(this, this.GetType(), "msg", "alert('Updated Successfully !');", true);
            Button1.Text = "Submit";
        }

        GridFill();
        ResetAllFields();
    }
    protected void Button2_Click(object sender, EventArgs e)
    {
        ResetAllFields();
    }
    public void ResetAllFields()
    {
         TextBox1.Text="";
        txtid.Text="";
        txtdate.Text="";
        txtpurchaseqty.Text="";
        txtbonusqty.Text="";
        txtprice.Text="";
        txttotal.Text = "";
        DropDownList1.SelectedIndex = 0;
    }
    protected void GridView1_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        GridView1.PageIndex = e.NewPageIndex;
        GridFill();
    }
    protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "Select")
        {
            int index = Convert.ToInt32(e.CommandArgument);

            Label lblcode = (Label)GridView1.Rows[index].FindControl("lblcode");
            TextBox4.Value = lblcode.Text;

            Label lblname = (Label)GridView1.Rows[index].FindControl("lblname");
            TextBox1.Text = lblname.Text;

            Label lbldate = (Label)GridView1.Rows[index].FindControl("lbldate");
            txtdate.Text = lbldate.Text;

            Label lblpqty = (Label)GridView1.Rows[index].FindControl("lblpqty");
            txtpurchaseqty.Text = lblpqty.Text;

            Label lblbqty = (Label)GridView1.Rows[index].FindControl("lblbqty");
            txtbonusqty.Text = lblbqty.Text;

            Label lblprice = (Label)GridView1.Rows[index].FindControl("lblprice");
            txtprice.Text = lblprice.Text;

            Label lbltotal = (Label)GridView1.Rows[index].FindControl("lbltotal");
            txttotal.Text = lbltotal.Text;

            Label lblid = (Label)GridView1.Rows[index].FindControl("lblid");
            txtid.Text = lblid.Text;
            string strModified = txtid.Text.Substring(0, 1);
            if (strModified == "C")
            {
                DropDownList1.SelectedItem.Text = "Company";
            }
            if (strModified == "M")
            {
                DropDownList1.SelectedItem.Text = "Manufacturer";
            }
            if (strModified == "S")
            {
                DropDownList1.SelectedItem.Text = "Supplier";
            }
            //Label lblname = (Label)GridView1.Rows[index].FindControl("lblname");
            //txtname.Text = lblname.Text;
            Tab1Func();
            Button1.Text = "Update";
        }
    }
    protected void GridView1_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        Label lblcode = (Label)GridView1.Rows[e.RowIndex].FindControl("lblcode");
        thereagent.DeleteAppointment(lblcode.Text);
        ScriptManager.RegisterStartupScript(this, this.GetType(), "msg", "alert('Deleted Successfully !');", true);
        GridFill();
        ResetAllFields();
    }
    protected void Button4_Click(object sender, EventArgs e)
    {
        if (DropDownList1.SelectedIndex == 0)
        {
            lblError.Text = "Please Select Type..";
            lblError.ForeColor = System.Drawing.Color.Red;
        }
        else
        {
            if (DropDownList1.SelectedIndex == 1)
            {
                Response.Redirect("../Pathology/SuppilierMaster.aspx");
            }
            else
            {
                if (DropDownList1.SelectedIndex == 2)
                {
                    Response.Redirect("../Pathology/ManufactureMaster.aspx");
                }
                else
                {
                    Response.Redirect("../Pathology/CompanyMaster.aspx");
                }
            }
        }
    }
    public void Tab1Func()
    {
        Tab1.CssClass = "Clicked";
        Tab2.CssClass = "Initial";
        MainView.ActiveViewIndex = 0;
    }
    protected void Tab1_Click(object sender, EventArgs e)
    {
        Tab1Func();
    }
    protected void Tab2_Click(object sender, EventArgs e)
    {
        Tab1.CssClass = "Initial";
        Tab2.CssClass = "Clicked";
        MainView.ActiveViewIndex = 1;
    }
}