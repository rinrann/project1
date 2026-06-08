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
 
public partial class Master_SubModuleDetails : System.Web.UI.Page
{
    Main Objmain = new Main(ConfigurationSettings.AppSettings["ConnectionString"].ToString());
    SubModuleDetails theHelper = new SubModuleDetails(ConfigurationSettings.AppSettings["ConnectionString"].ToString());
    protected void Page_Load(object sender, EventArgs e)
    {
        Page.Title = "Sub Module Details";
        if (Session["userName"].ToString() != "SUPERVISOR")
        {
            if (Session["userName"] == null)
            {
                Response.Redirect("../LoginPage.aspx");
            }
            if (Objmain.checkaccss(Session["CoCode"].ToString(), Session["UserRoleID"].ToString().Trim(), "SUB-MODULE DETAILS", checkAccessType.ViewAction) == false)
            {
                Response.Redirect("../AccessDenied.aspx");
            }

            if (Objmain.checkaccss(Session["CoCode"].ToString(), Session["UserRoleID"].ToString().Trim(), "SUB-MODULE DETAILS", checkAccessType.InsertAction) == false)
            {
                Button1.Enabled = false;
            }
            if (Objmain.checkaccss(Session["CoCode"].ToString(), Session["UserRoleID"].ToString().Trim(), "SUB-MODULE DETAILS", checkAccessType.DeleteAction) == false)
            {
                GridView1.Columns[5].Visible = false;
            }
        }
       if (!IsPostBack)
        {
            GridFill();
            DropDownFill();
            Tab1Func();
          }
    }
     private void GridFill()
    { 
        GridView1.DataSource = theHelper.GetAllSubModule(Session["CoCode"].ToString(),ddlModuleSearch.SelectedValue);
        GridView1.DataBind();
    }
    private void ResetAllFields()
    {
           txtSubModuleName.Text = "";
        DropDownList1.SelectedIndex = -1;
        Button1.Text = "Submit";
       
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
                       
            Label lblSubModuleID = (Label)GridView1.Rows[index].FindControl("SubModuleID");
             HiddenField1.Value = lblSubModuleID.Text;

            Label lblSubModuleName = (Label)GridView1.Rows[index].FindControl("SubModuleName");
            txtSubModuleName.Text = lblSubModuleName.Text;


             DropDownFill();
            Label lblModuleID = (Label)GridView1.Rows[index].FindControl("ModuleID");
              DropDownList1.SelectedValue = lblModuleID.Text;
              Tab1Func();
            Button1.Text = "Update";
            if (Objmain.checkaccss(Session["CoCode"].ToString(), Session["UserRoleID"].ToString().Trim(), "SUB-MODULE DETAILS", checkAccessType.UpdateAction) == false)
            {
                Button1.Enabled = false;
            }
            else
            {
                Button1.Enabled = true;
            }
        }
    }
    private void DropDownFill()
    {
        DropDownList1.Items.Clear();
        this.DropDownList1.DataSource = theHelper.DropdownID(Session["CoCode"].ToString().Trim());
        this.DropDownList1.DataTextField = "ModuleName";
        this.DropDownList1.DataValueField = "ModuleID";
        this.DropDownList1.DataBind();
        this.DropDownList1.Items.Insert(0, new ListItem("--Select--", "0"));


        ddlModuleSearch.Items.Clear();
        this.ddlModuleSearch.DataSource = theHelper.DropdownID(Session["CoCode"].ToString().Trim());
        this.ddlModuleSearch.DataTextField = "ModuleName";
        this.ddlModuleSearch.DataValueField = "ModuleID";
        this.ddlModuleSearch.DataBind();
        this.ddlModuleSearch.Items.Insert(0, new ListItem("--Select--", "0"));
    }
    protected void GridView1_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
         Label lblSubModuleID = (Label)GridView1.Rows[e.RowIndex].FindControl("SubModuleID");
         theHelper.DeleteSubModuleDetails(lblSubModuleID.Text, Session["CoCode"].ToString().Trim());
        lblError.ForeColor = System.Drawing.Color.Green;
        lblError.Text = "Deleted Successfully";
        GridFill();
        ResetAllFields();
    }


    protected void Button1_Click(object sender, EventArgs e)
    {
         if (Button1.Text == "Submit")
        {
           
            theHelper.InsertSubModuleDtails(Session["CoCode"].ToString().Trim(),txtSubModuleName.Text, DropDownList1.SelectedValue, Session["userName"].ToString());
            ScriptManager.RegisterStartupScript(this, this.GetType(), "msg", "alert('Inserted Successfully !');", true);
        }
        else
        {
            theHelper.UpdateSubModuleDetails(HiddenField1.Value, txtSubModuleName.Text, DropDownList1.SelectedValue, Session["CoCode"].ToString().Trim());
            ScriptManager.RegisterStartupScript(this, this.GetType(), "msg", "alert('Updated Successfully !');", true);
            Button1.Text = "Submit";
        }

        ResetAllFields();
        GridFill();
         
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
    protected void ddlModuleSearch_SelectedIndexChanged(object sender, EventArgs e)
    {
        GridFill();
    }
}