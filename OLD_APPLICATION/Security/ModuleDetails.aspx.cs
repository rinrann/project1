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
 
public partial class Master_ModuleDetails : System.Web.UI.Page
{
    Main Objmain = new Main(ConfigurationSettings.AppSettings["ConnectionString"].ToString());
    ModuleDetails theHelper = new ModuleDetails(ConfigurationSettings.AppSettings["ConnectionString"].ToString());
    protected void Page_Load(object sender, EventArgs e)
    {
        Page.Title = "Module Details";
        if (Session["userName"].ToString() != "SUPERVISOR")
        {
            if (Session["userName"] == null)
            {
                Response.Redirect("../LoginPage.aspx");
            }
            if (Objmain.checkaccss(Session["CoCode"].ToString(), Session["UserRoleID"].ToString().Trim(), "MODULE DETAILS", checkAccessType.ViewAction) == false)
            {
                Response.Redirect("../AccessDenied.aspx");
            }

            if (Objmain.checkaccss(Session["CoCode"].ToString(), Session["UserRoleID"].ToString().Trim(), "MODULE DETAILS", checkAccessType.InsertAction) == false)
            {
                Button1.Enabled = false;
            }
            if (Objmain.checkaccss(Session["CoCode"].ToString(), Session["UserRoleID"].ToString().Trim(), "MODULE DETAILS", checkAccessType.DeleteAction) == false)
            {
                GridView1.Columns[3].Visible = false;
            }
        }
        if (!IsPostBack)
        {
            GridFill();
            Tab1Func();
          
        }
    }
    private void GridFill()
    {
        GridView1.DataSource = theHelper.GetAllModule(Session["CoCode"].ToString());
        GridView1.DataBind();
    }
    private void ResetAllFields()
    {
        txtModuleName.Text = "";
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
            GridView1.Rows[index].BackColor = Color.GreenYellow;

            Label lblmoduleID = (Label)GridView1.Rows[index].FindControl("ModuleID");
            HiddenField1.Value = lblmoduleID.Text;

            Label lblmoduleName = (Label)GridView1.Rows[index].FindControl("ModuleName");
            txtModuleName.Text = lblmoduleName.Text;
            Button1.Text = "Update";
            Tab1Func();

            if (Objmain.checkaccss(Session["CoCode"].ToString(), Session["UserRoleID"].ToString().Trim(), "MODULE DETAILS", checkAccessType.UpdateAction) == false)
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
         Label lblmoduleID = (Label)GridView1.Rows[e.RowIndex].FindControl("ModuleID");
        theHelper.DeleteModuleDetails(Convert.ToInt32(lblmoduleID.Text));
        lblError.ForeColor = System.Drawing.Color.Green;
        lblError.Text = "Deleted Successfully";
        GridFill();
        ResetAllFields();
       
    }
     protected void Button1_Click1(object sender, EventArgs e)
    {
           if (Button1.Text == "Submit")
        {
            theHelper.InsertModuleDetails(Session["CoCode"].ToString().Trim(),txtModuleName.Text, Session["userName"].ToString(),DateTime.Now.ToString("MM.dd/yyyy"));
            ScriptManager.RegisterStartupScript(this, this.GetType(), "msg", "alert('Inserted successfully !');", true);
        }
        else
        {
            theHelper.UpdateModuleDetails(HiddenField1.Value, txtModuleName.Text, Session["CoCode"].ToString().Trim());
            ScriptManager.RegisterStartupScript(this, this.GetType(), "msg", "alert('Updated successfully !');", true);
            Button1.Text = "Submit";
        }

           GridView1.DataSource = theHelper.GetAllModule(Session["CoCode"].ToString().Trim());
        GridView1.DataBind();
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
}
