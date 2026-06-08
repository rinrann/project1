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

 
public partial class Pathology_SpecimanMaster : System.Web.UI.Page
{
    Main Objmain = new Main(ConfigurationSettings.AppSettings["ConnectionString"].ToString());
    PH_SpecimanMaster thespeciman = new PH_SpecimanMaster(ConfigurationSettings.AppSettings["ConnectionString"].ToString());
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["userName"] == null)
        {
            Response.Redirect("../LoginPage.aspx");
        }
        if (Objmain.checkaccss(Session["CoCode"].ToString(), Session["UserRoleID"].ToString().Trim(), "SAMPLE MASTER", checkAccessType.ViewAction) == false)
        {
            Response.Redirect("../AccessDenied.aspx");
        }

        if (Objmain.checkaccss(Session["CoCode"].ToString(), Session["UserRoleID"].ToString().Trim(), "SAMPLE MASTER", checkAccessType.InsertAction) == false)
        {
            Button1.Enabled = false;
        }
        if (Objmain.checkaccss(Session["CoCode"].ToString(), Session["UserRoleID"].ToString().Trim(), "SAMPLE MASTER", checkAccessType.DeleteAction) == false)
        {
            GridView1.Columns[4].Visible = false;
        }
        Page.Title = "Specimen Master";
        if (Session["userName"] == null)
        {
            Response.Redirect("../LoginPage.aspx");
        }
        GridFill();
        if (!IsPostBack)
        {
            generatedept(); DropDownFill();
            Tab1Func();
        }

    }


    private void generatedept()
    {
         DataTable dt = thespeciman.generateSpeciman();
        txtcode.Text = dt.Rows[0][0].ToString();
    }

    private void ResetAllFields()
    {
        txtname.Text = "";
        generatedept();
        DropDownList1.SelectedIndex = 0;
        Button1.Text = "Submit";

    }

    private void GridFill()
    {
        GridView1.DataSource = thespeciman.GridSpeciman(Session["CoCode"].ToString());
        GridView1.DataBind();
    }
    protected void Button1_Click(object sender, EventArgs e)
    {
        if (Button1.Text == "Submit")
        {

            thespeciman.InsertSpeciman(txtcode.Text, txtname.Text, Session["userName"].ToString(), DropDownList1.SelectedValue, Session["CoCode"].ToString());
            ScriptManager.RegisterStartupScript(this, this.GetType(), "msg", "alert('Inserted Successfully !');", true);


        }
        else
        {

            thespeciman.UpdateSpeciman(txtcode.Text, txtname.Text, DropDownList1.SelectedValue, Session["CoCode"].ToString());
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
            txtcode.Text = lblcode.Text;

            Label lbldeptname = (Label)GridView1.Rows[index].FindControl("lbldeptname");
            DropDownList1.SelectedIndex = DropDownList1.Items.IndexOf(DropDownList1.Items.FindByText(lbldeptname.Text));


            Label lblname = (Label)GridView1.Rows[index].FindControl("lblname");
            txtname.Text = lblname.Text;
            Tab1Func();
            Button1.Text = "Update";

            if (Objmain.checkaccss(Session["CoCode"].ToString(), Session["UserRoleID"].ToString().Trim(), "SPECIMEN MASTER", checkAccessType.UpdateAction) == false)
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
        this.DropDownList1.DataSource = thespeciman.DropdownDepartment();
        this.DropDownList1.DataTextField = "DeptName";
        this.DropDownList1.DataValueField = "DeptCode";
        this.DropDownList1.DataBind();
        this.DropDownList1.Items.Insert(0, new ListItem("--Select--", "0"));
    }
    protected void GridView1_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        Label lblcode = (Label)GridView1.Rows[e.RowIndex].FindControl("lblcode");
        thespeciman.DeleteSpeciman(lblcode.Text, Session["CoCode"].ToString());
        ScriptManager.RegisterStartupScript(this, this.GetType(), "msg", "alert('Deleted Successfully !');", true);
        GridFill();
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
    public static List<string> SearchSpecimen(string prefixText, int count)
    {
        using (SqlConnection conn = new SqlConnection())
        {
            conn.ConnectionString = ConfigurationSettings.AppSettings["ConnectionString"].ToString();
            using (SqlCommand cmd = new SqlCommand())
            {
                cmd.CommandText = "select distinct SName as Name from PH_SpecimanMaster where SName like @SearchText +'%' and status='1'";
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
    protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            if (Objmain.checkaccss(Session["CoCode"].ToString(), Session["UserRoleID"].ToString().Trim(), "SPECIMEN MASTER", checkAccessType.DeleteAction) == false)
            {
                code1.Visible = false;
                e.Row.Cells[4].Visible = false;
            }
        }
    }
}