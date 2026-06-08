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
 
public partial class Pathology_ReportTemplateMaster : System.Web.UI.Page
{
    Main Objmain = new Main(ConfigurationSettings.AppSettings["ConnectionString"].ToString());
    PH_ReportTemplateMaster thetest = new PH_ReportTemplateMaster(ConfigurationSettings.AppSettings["ConnectionString"].ToString());
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["userName"] == null)
        {
            Response.Redirect("../LoginPage.aspx");
        }
        if (Objmain.checkaccss(Session["CoCode"].ToString(), Session["UserRoleID"].ToString().Trim(), "REPORT TEMPLATE MASTER", checkAccessType.ViewAction) == false)
        {
            Response.Redirect("../AccessDenied.aspx");
        }

        if (Objmain.checkaccss(Session["CoCode"].ToString(), Session["UserRoleID"].ToString().Trim(), "REPORT TEMPLATE MASTER", checkAccessType.InsertAction) == false)
        {
            Button1.Enabled = false;
        }
        if (Objmain.checkaccss(Session["CoCode"].ToString(), Session["UserRoleID"].ToString().Trim(), "REPORT TEMPLATE MASTER", checkAccessType.DeleteAction) == false)
        {
            GridView1.Columns[5].Visible = false;
        }
        Page.Title = "Report Template Master";
        if (Session["userName"] == null)
        {
            Response.Redirect("../LoginPage.aspx");
        }
        if (!IsPostBack)
        {
            DropDownFill();
            Tab1Func();
            GridFill();
        }
    }

    private void GridFill()
    {
        GridView1.DataSource = thetest.GridFill(Session["CoCode"].ToString().Trim());
        GridView1.DataBind();
    }

    private void ResetAllFields()
    {
        txttemname.Text = "";
        txttemplate.Text = "";
        DropDownList1.SelectedIndex = 0;
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

            Label lblcode = (Label)GridView1.Rows[index].FindControl("lblcode");
            TextBox4.Value = lblcode.Text;
          
            Label lblname = (Label)GridView1.Rows[index].FindControl("lblname");
           txttemname.Text = lblname.Text;

           Label lbltemplate = (Label)GridView1.Rows[index].FindControl("lbltemplate");
           txttemplate.Text = lbltemplate.Text;

           Label lbldept = (Label)GridView1.Rows[index].FindControl("lbldept");
           DropDownList1.SelectedIndex = DropDownList1.Items.IndexOf(DropDownList1.Items.FindByText(lbldept.Text));
           Tab1Func();
            Button1.Text = "Update";
            if (Objmain.checkaccss(Session["CoCode"].ToString(), Session["UserRoleID"].ToString().Trim(), "REPORT TEMPLATE MASTER", checkAccessType.UpdateAction) == false)
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
         Label lblcode = (Label)GridView1.Rows[e.RowIndex].FindControl("lblcode");
         thetest.DeleteTemplate(lblcode.Text, Session["CoCode"].ToString());
        lblError.ForeColor = System.Drawing.Color.Green;
        lblError.Text = "Deleted Successfully";
        GridFill();
        ResetAllFields();
    }

    private void DropDownFill()
    {
        this.DropDownList1.DataSource = thetest.DropdownDepartment(Session["CoCode"].ToString().Trim());
        this.DropDownList1.DataTextField = "DeptName";
        this.DropDownList1.DataValueField = "DeptCode";
        this.DropDownList1.DataBind();
        this.DropDownList1.Items.Insert(0, new ListItem("--Select--", "0"));
    }
    protected void Button1_Click(object sender, EventArgs e)
    {
        string id;
         if (TextBox4.Value != "")
        {
            id =TextBox4.Value;
        }
        else
        {
            id = "null";
        }
        if (txttemplate.Text == "")
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "msg", "alert('Select Template Content !');", true);
        }
        else
        {
            if (Button1.Text == "Submit")
            {

                thetest.InsertTemplate(txttemname.Text, txttemplate.Text, DropDownList1.SelectedValue, Session["CoCode"].ToString());
                ScriptManager.RegisterStartupScript(this, this.GetType(), "msg", "alert('Inserted Successfully !');", true);


            }
            else
            {


                thetest.UpdateTemplate(id, txttemname.Text, txttemplate.Text, DropDownList1.SelectedValue, Session["CoCode"].ToString());
                ScriptManager.RegisterStartupScript(this, this.GetType(), "msg", "alert('Updated Successfully !');", true);
                Button1.Text = "Submit";

            }

            GridFill();
            ResetAllFields();
        }
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
    public static List<string> SearchRptTemplate(string prefixText, int count)
    {
        using (SqlConnection conn = new SqlConnection())
        {
            conn.ConnectionString = ConfigurationSettings.AppSettings["ConnectionString"].ToString();
            using (SqlCommand cmd = new SqlCommand())
            {
                cmd.CommandText = "select distinct TemplateName as Name from PH_ReportTemplate where TemplateName like @SearchText +'%' and status='1'";
                cmd.Parameters.AddWithValue("@SearchText", prefixText);
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
}