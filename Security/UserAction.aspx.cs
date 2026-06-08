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
 
public partial class Master_UserAction : System.Web.UI.Page
{
    Main Objmain = new Main(ConfigurationSettings.AppSettings["ConnectionString"].ToString());
    UserAction theHelper = new UserAction(ConfigurationSettings.AppSettings["ConnectionString"].ToString());
    protected void Page_Load(object sender, EventArgs e)
    {
        Page.Title = "User Action";
        if (Session["userName"] == null)
        {
            Response.Redirect("../LoginPage.aspx");
        }
        if (Objmain.checkaccss(Session["CoCode"].ToString(), Session["UserRoleID"].ToString().Trim(), "USER ACTION", checkAccessType.ViewAction) == false)
        {
            Response.Redirect("../AccessDenied.aspx");
        }

        if (Objmain.checkaccss(Session["CoCode"].ToString(), Session["UserRoleID"].ToString().Trim(), "USER ACTION", checkAccessType.InsertAction) == false)
        {
            Button1.Enabled = false;
        }
        if (Objmain.checkaccss(Session["CoCode"].ToString(), Session["UserRoleID"].ToString().Trim(), "USER ACTION", checkAccessType.DeleteAction) == false)
        {
            GridView1.Columns[3].Visible = false;
        } 
        if (!IsPostBack)
        {
            GridFill();
            Tab1Func();
        }
    }

    private void GridFill()
    {
           GridView1.DataSource = theHelper.GetAllAction();
        GridView1.DataBind();
    }
    private void ResetAllFields()
    {
          txtActionName.Text = "";
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

            Label lblActionId = (Label)GridView1.Rows[index].FindControl("ActionID");
              HiddenField1.Value = lblActionId.Text;

            Label lblActionName = (Label)GridView1.Rows[index].FindControl("ActionName");
            txtActionName.Text = lblActionName.Text;
            Tab1Func();
            Button1.Text = "Update";
            if (Objmain.checkaccss(Session["CoCode"].ToString(), Session["UserRoleID"].ToString().Trim(), "USER ACTION", checkAccessType.UpdateAction) == false)
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
         Label lblActionId = (Label)GridView1.Rows[e.RowIndex].FindControl("ActionID");
        theHelper.DeleteUserAction(Convert.ToInt32(lblActionId.Text));
        ScriptManager.RegisterStartupScript(this, this.GetType(), "msg", "alert('Deleted Successfully !');", true);
        GridFill();
        ResetAllFields();
        
    }
    protected void Button1_Click1(object sender, EventArgs e)
    {
        if (Button1.Text == "Submit")
        {
            theHelper.InsertUserAction(DateTime.Now.ToString("MM/dd/yyyy"), txtActionName.Text.ToUpper(), Session["userName"].ToString());
            ScriptManager.RegisterStartupScript(this, this.GetType(), "msg", "alert('Inserted Successfully !');", true);
        }
        else
        {
            theHelper.UpdateUserAction(HiddenField1.Value, txtActionName.Text.ToUpper());
            ScriptManager.RegisterStartupScript(this, this.GetType(), "msg", "alert('Updated Successfully !');", true);
            Button1.Text = "Submit";
        }

        GridView1.DataSource = theHelper.GetAllAction();
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