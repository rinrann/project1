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
 
public partial class Master_UserRole : System.Web.UI.Page
{
    Main Objmain = new Main(ConfigurationSettings.AppSettings["ConnectionString"].ToString());
    UserRole theHelper = new UserRole(ConfigurationSettings.AppSettings["ConnectionString"].ToString());
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["userName"] == null)
        {
            Response.Redirect("../LoginPage.aspx");
        }
        if (Objmain.checkaccss(Session["CoCode"].ToString(), Session["UserRoleID"].ToString().Trim(), "USER ROLE", checkAccessType.ViewAction) == false)
        {
            Response.Redirect("../AccessDenied.aspx");
        }

        if (Objmain.checkaccss(Session["CoCode"].ToString(), Session["UserRoleID"].ToString().Trim(), "USER ROLE", checkAccessType.InsertAction) == false)
        {
            Button1.Enabled = false;
        }
        if (Objmain.checkaccss(Session["CoCode"].ToString(), Session["UserRoleID"].ToString().Trim(), "USER ROLE", checkAccessType.DeleteAction) == false)
        {
            GridView1.Columns[3].Visible = false;
        }
        Page.Title = "User Role";
        
        if (!IsPostBack)
        {
            GridFill();
             Tab1Func();
        }
    }
    private void GridFill()
    {
         GridView1.DataSource = theHelper.GetAllUser();
        GridView1.DataBind();
    }
    private void ResetAllFields()
    {
           TextBox2.Text = "";
        Button1.Text = "Submit";
    }
    protected void Button1_Click(object sender, EventArgs e)
    {

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

            Label lbluserroleID = (Label)GridView1.Rows[index].FindControl("UserRoleID");
             HiddenField1.Value = lbluserroleID.Text;

            Label lbluserroleName = (Label)GridView1.Rows[index].FindControl("UserRoleName");
            TextBox2.Text = lbluserroleName.Text;
            Tab1Func();
            Button1.Text = "Update";

            if (Objmain.checkaccss(Session["CoCode"].ToString(), Session["UserRoleID"].ToString().Trim(), "USER ROLE", checkAccessType.UpdateAction) == false)
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
         Label lbluserroleID = (Label)GridView1.Rows[e.RowIndex].FindControl("UserRoleID");
         theHelper.DeleteUserRole(lbluserroleID.Text, Session["CoCode"].ToString());
        ScriptManager.RegisterStartupScript(this, this.GetType(), "msg", "alert('Deleted Successfully !');", true);
        GridFill();
        ResetAllFields();
      
    }

    protected void Button1_Click1(object sender, EventArgs e)
    {
       
        if (Button1.Text == "Submit")
        {
            theHelper.InsertUserRole(TextBox2.Text, Session["userName"].ToString(), DateTime.Now.ToString("MM/dd/yyyy"), Session["CoCode"].ToString());
            ScriptManager.RegisterStartupScript(this, this.GetType(), "msg", "alert('Inserted Successfully !');", true);
        }
        else
        {
            theHelper.UpdateUserRole(HiddenField1.Value, TextBox2.Text, Session["CoCode"].ToString());
            ScriptManager.RegisterStartupScript(this, this.GetType(), "msg", "alert('Updated Successfully !');", true);
            Button1.Text = "Submit";
        }

        GridView1.DataSource = theHelper.GetAllUser();
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


