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

public partial class ToDoTaskEntry : System.Web.UI.Page
{
    Main Objmain = new Main(ConfigurationSettings.AppSettings["ConnectionString"].ToString());
    ToDoTaskClass objToDoTaskClass = new ToDoTaskClass(ConfigurationSettings.AppSettings["ConnectionString"].ToString());
    static string regno;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["userName"] == null)
        {
            Response.Redirect("../LoginPage.aspx");
        }
        if (Objmain.checkaccss(Session["CoCode"].ToString(), Session["UserRoleID"].ToString().Trim(), "TO DO TASK DONE", checkAccessType.ViewAction) == false)
        {
            Response.Redirect("../AccessDenied.aspx");
        }

        if (!IsPostBack)
        {
            regno = "";
            GridBind();
        }

    }

    public void GridBind()
    {

        GridView1.DataSource = objToDoTaskClass.GridFill(Session["CoCode"].ToString().Trim());
        GridView1.DataBind();
    }

   
    protected void GridView1_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        GridView1.PageIndex = e.NewPageIndex;
        GridBind();

    }
    protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
    {

        if (e.CommandName == "Select")
        {
            int RowIndex =Convert.ToInt32(e.CommandArgument);
            Label lblPatientReg = (Label)GridView1.Rows[RowIndex].FindControl("lblPatientReg");
            regno = lblPatientReg.Text;
            DataTable dt = objToDoTaskClass.GridFill1(lblPatientReg.Text);
            GridTodoTask.DataSource = dt;
            GridTodoTask.DataBind();
        }
    }
    protected void GridTodoTask_RowUpdating(object sender, GridViewUpdateEventArgs e)
    {
        TextBox txtETaskBy = (TextBox)GridTodoTask.Rows[e.RowIndex].FindControl("txtETaskBy");
        TextBox txtTaskTime = (TextBox)GridTodoTask.Rows[e.RowIndex].FindControl("txtTaskTime");
        TextBox txtTaskDate = (TextBox)GridTodoTask.Rows[e.RowIndex].FindControl("txtTaskDate");
        Label lblRowId = (Label)GridTodoTask.Rows[e.RowIndex].FindControl("lblRowId");

         System.Globalization.DateTimeFormatInfo dtf = new DateTimeFormatInfo();
     

         if (txtETaskBy.Text == "")
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "msg", "alert('Please Select TaskBy !');", true);
        }
         else if (txtTaskTime.Text == "")
         {
             ScriptManager.RegisterStartupScript(this, this.GetType(), "msg", "alert('Please Select TaskTime !');", true);
         }
         else if (txtTaskDate.Text == "")
         {
             ScriptManager.RegisterStartupScript(this, this.GetType(), "msg", "alert('Please Select TaskDate !');", true);
         }
         else
         {
             DateTime testdate = DateTime.ParseExact(txtTaskDate.Text, "dd/MM/yyyy", dtf);
             objToDoTaskClass.Patienttask_Entry(lblRowId.Text, txtETaskBy.Text, txtTaskTime.Text, testdate.ToString(), Session["CoCode"].ToString().Trim(), Session["userName"].ToString().Trim());

             GridTodoTask.EditIndex = -1;

             DataTable dt = objToDoTaskClass.GridFill1(regno);
             GridTodoTask.DataSource = dt;
             GridTodoTask.DataBind();

         }
       
    }
    protected void GridTodoTask_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
    {
        GridTodoTask.EditIndex = -1;
 
        DataTable dt = objToDoTaskClass.GridFill1(regno);
        GridTodoTask.DataSource = dt;
        GridTodoTask.DataBind();
    }
    protected void GridTodoTask_RowEditing(object sender, GridViewEditEventArgs e)
    {
        GridTodoTask.EditIndex = e.NewEditIndex;

        DataTable dt = objToDoTaskClass.GridFill1(regno);
        GridTodoTask.DataSource = dt;
        GridTodoTask.DataBind();
    }
}