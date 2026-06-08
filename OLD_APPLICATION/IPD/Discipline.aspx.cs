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
using System.Collections.Generic;

public partial class IPD_Discipline : System.Web.UI.Page
{
    Main Objmain = new Main(ConfigurationSettings.AppSettings["ConnectionString"].ToString());
    Discipline theHelper = new Discipline(ConfigurationSettings.AppSettings["ConnectionString"].ToString());
    protected void Page_Load(object sender, EventArgs e)
    {
        Page.Title = "Discipline Master";
        if (Session["userName"] == null)
        {
            Response.Redirect("../LoginPage.aspx");
        }
        if (Objmain.checkaccss(Session["CoCode"].ToString(), Session["UserRoleID"].ToString().Trim(), "DISCIPLINE MASTER", checkAccessType.ViewAction) == false)
        {
            Response.Redirect("../AccessDenied.aspx");
        }

        if (Objmain.checkaccss(Session["CoCode"].ToString(), Session["UserRoleID"].ToString().Trim(), "DISCIPLINE MASTER", checkAccessType.InsertAction) == false)
        {
            Button1.Enabled = false;
        }
        if (Objmain.checkaccss(Session["CoCode"].ToString(), Session["UserRoleID"].ToString().Trim(), "DISCIPLINE MASTER", checkAccessType.DeleteAction) == false)
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
        GridView1.DataSource = theHelper.GridFill(Session["CoCode"].ToString().Trim());
        GridView1.DataBind();
    }
    private void ResetAllFields()
    {

        txtDiscipline.Text = "";       
        Button1.Text = "Submit";

        if (Objmain.checkaccss(Session["CoCode"].ToString(), Session["UserRoleID"].ToString().Trim(), "DISCIPLINE MASTER", checkAccessType.InsertAction) == false)
        {
            Button1.Enabled = false;
        }

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
            HiddenField1.Value = lblID.Text;

            Label ServiceName = (Label)GridView1.Rows[index].FindControl("ServiceName");
            txtDiscipline.Text = ServiceName.Text;
            Tab1Func();
            Button1.Text = "Update";

            if (Objmain.checkaccss(Session["CoCode"].ToString(), Session["UserRoleID"].ToString().Trim(), "DISCIPLINE MASTER", checkAccessType.UpdateAction) == false)
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
        theHelper.DeleteDiscipline(lblID.Text, Session["CoCode"].ToString().Trim());
        ScriptManager.RegisterStartupScript(this, this.GetType(), "msg", "alert('Deleted Successfully !');", true);
        GridFill();
        ResetAllFields(); 
    }
    protected void Button1_Click(object sender, EventArgs e)
    {

        if (Button1.Text == "Submit")
        {

            if (theHelper.InsertDiscipline(txtDiscipline.Text, Session["CoCode"].ToString().Trim()) == true)
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "msg", "alert('Inserted Successfully !');", true);
            }
        }
        else
        {
            if (theHelper.UpdateDiscipline(HiddenField1.Value, txtDiscipline.Text, Session["CoCode"].ToString().Trim()) == true)
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "msg", "alert('Updated Successfully !');", true);
            }
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

    [System.Web.Script.Services.ScriptMethod()]
    [System.Web.Services.WebMethod]
    public static List<string> Searchdiscipline(string prefixText, int count)
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
                cmd.CommandText = "select distinct DisciplineName as Name from GN_DisciplineType where compcode=@Compcode and DisciplineName like @SearchText +'%'";
                cmd.Parameters.AddWithValue("@SearchText", prefixText);
                cmd.Parameters.AddWithValue("@Compcode", compcode);
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
    protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            if (Objmain.checkaccss(Session["CoCode"].ToString(), Session["UserRoleID"].ToString().Trim(), "DISCIPLINE MASTER", checkAccessType.DeleteAction) == false)
            {
                e.Row.Cells[3].Visible = false;
            }

        }
    }
}