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
 
public partial class DayCare_ChemicalList : System.Web.UI.Page
{
    Main Objmain = new Main(ConfigurationSettings.AppSettings["ConnectionString"].ToString());
    DC_ChemicalList thechemicallist = new DC_ChemicalList(ConfigurationSettings.AppSettings["ConnectionString"].ToString());
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            Page.Title = "Chemical List";
            if (Session["userName"] == null)
            {
                Response.Redirect("../LoginPage.aspx");
            }
            if (Objmain.checkaccss(Session["CoCode"].ToString(), Session["UserRoleID"].ToString().Trim(), "CHEMICAL LIST", checkAccessType.ViewAction) == false)
            {
                Response.Redirect("../AccessDenied.aspx");
            }

            if (Objmain.checkaccss(Session["CoCode"].ToString(), Session["UserRoleID"].ToString().Trim(), "CHEMICAL LIST", checkAccessType.InsertAction) == false)
            {
                Button1.Enabled = false;
            }
           
            GridFill();

            if (!IsPostBack)
            {
                DropDownFill();
                Tab1Func();
            }
        }
        catch (Exception ex)
        {
            lblError.Text = ex.Message;
        }
    }
    private void ResetAllFields()
    {
        DropDownList1.SelectedIndex = 0; 
        ddlunit.SelectedIndex = 0; 
        txtpatientcover.Text = "";
        txtuserfor.Text = "";
        TextBox4.Value = "";
        Button1.Text = "Submit";
        if (Objmain.checkaccss(Session["CoCode"].ToString(), Session["UserRoleID"].ToString().Trim(), "CHEMICAL LIST", checkAccessType.InsertAction) == false)
        {
            Button1.Enabled = false;
        }

    }
    private void DropDownFill()
    {
        this.ddlunit.DataSource = thechemicallist.DropdownUnit();
        this.ddlunit.DataTextField = "UnitName";
        this.ddlunit.DataValueField = "UnitId";
        this.ddlunit.DataBind();
        this.ddlunit.Items.Insert(0, new ListItem("--Select--", "0"));

        this.DropDownList1.DataSource = thechemicallist.DropdownChemical();
        this.DropDownList1.DataTextField = "MedicineName";
        this.DropDownList1.DataValueField = "MedicineID";
        this.DropDownList1.DataBind();
        this.DropDownList1.Items.Insert(0, new ListItem("--Select--", "0"));
    }

    private void GridFill()
    {
        GridView1.SelectedIndex = -1;
        GridView1.DataSource = thechemicallist.GridChemical();
        GridView1.DataBind();
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
            Label lblID = (Label)GridView1.Rows[index].FindControl("lblID");
            TextBox4.Value = lblID.Text;

            Label lbliChemicalName = (Label)GridView1.Rows[index].FindControl("lbliChemicalName");
            DropDownList1.SelectedValue = lbliChemicalName.Text;
        
            Label lbluserfor = (Label)GridView1.Rows[index].FindControl("lbluserfor");
            txtuserfor.Text = lbluserfor.Text;
            
            Label lblunitmas = (Label)GridView1.Rows[index].FindControl("lblunitmas");
            ddlunit.SelectedIndex = ddlunit.Items.IndexOf(ddlunit.Items.FindByText(lblunitmas.Text));
 
        
            Label lblpatientcover = (Label)GridView1.Rows[index].FindControl("lblpatientcover");
            txtpatientcover.Text = lblpatientcover.Text;
 
            Tab1Func();
            Button1.Text = "Update";
            if (Objmain.checkaccss(Session["CoCode"].ToString(), Session["UserRoleID"].ToString().Trim(), "CHEMICAL LIST", checkAccessType.UpdateAction) == false)
            {
                Button1.Enabled = false;
            }
            else
            {
                Button1.Enabled = true;
            }
        }
    }
    protected void GridView1_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        Label lblID = (Label)GridView1.Rows[e.RowIndex].FindControl("lblID");
        thechemicallist.DeleteChemicalList(Convert.ToInt32(lblID.Text), Session["CoCode"].ToString());
        lblError.ForeColor = System.Drawing.Color.Green;
        lblError.Text = "Deleted Successfully";
        GridFill();
        ResetAllFields();
    }
    protected void Button1_Click(object sender, EventArgs e)
    {
        string  id;
        if (TextBox4.Value != "")
        {
            id = TextBox4.Value;
        }
        else
        {
            id = "0";
        }
        if (Button1.Text == "Submit")
        {

            if (thechemicallist.InsertChemicalList(DropDownList1.SelectedValue, txtuserfor.Text, ddlunit.SelectedValue, txtpatientcover.Text, Session["CoCode"].ToString()) == true)

            ScriptManager.RegisterStartupScript(this, this.GetType(), "msg", "alert('Insered Successfully !');", true);
            else
              ScriptManager.RegisterStartupScript(this, this.GetType(), "msg", "alert('Error Insered Data !');", true);
           
          
        }
        else
        {

            if (thechemicallist.UpdateChemicalList(id, DropDownList1.SelectedValue, txtuserfor.Text, ddlunit.SelectedValue, txtpatientcover.Text, Session["CoCode"].ToString()) == true)
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "msg", "alert('Updated Successfully !');", true);
                GridView1.SelectedIndex = -1;
            }
            else
                ScriptManager.RegisterStartupScript(this, this.GetType(), "msg", "alert('Error Updated Data !');", true);
        }

        GridFill();
        ResetAllFields();
    }
    protected void Button2_Click(object sender, EventArgs e)
    {
        ResetAllFields();
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
    protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            if (Objmain.checkaccss(Session["CoCode"].ToString(), Session["UserRoleID"].ToString().Trim(), "CHEMICAL LIST", checkAccessType.DeleteAction) == false)
            {
                coldel.Visible = false;
                e.Row.Cells[7].Visible = false;
            }
        }
    }
}