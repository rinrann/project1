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

public partial class IPD_DiagnosisMaster : System.Web.UI.Page
{
    Main Objmain = new Main(ConfigurationSettings.AppSettings["ConnectionString"].ToString());
    Discipline theHelper = new Discipline(ConfigurationSettings.AppSettings["ConnectionString"].ToString());
    protected void Page_Load(object sender, EventArgs e)
    {
        Page.Title = "Diagnosis Master";
        if (Session["userName"] == null)
        {
            Response.Redirect("../LoginPage.aspx");
        }

        if (Objmain.checkaccss(Session["CoCode"].ToString(), Session["UserRoleID"].ToString().Trim(), "DIAGNOSIS MASTER", checkAccessType.ViewAction) == false)
        {
            Response.Redirect("../AccessDenied.aspx");
        }

        if (Objmain.checkaccss(Session["CoCode"].ToString(), Session["UserRoleID"].ToString().Trim(), "DIAGNOSIS MASTER", checkAccessType.InsertAction) == false)
        {
            Button1.Enabled = false;
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
        GridView1.DataSource = theHelper.GridFillDiagnosis(Session["CoCode"].ToString().Trim());
        GridView1.DataBind();
    }

    private void DropDownFill()
    {
        DropDownList1.Items.Clear();
        this.DropDownList1.DataSource = theHelper.GridFill(Session["CoCode"].ToString().Trim());
        this.DropDownList1.DataTextField = "DisciplineName";
        this.DropDownList1.DataValueField = "DisciplineId";
        this.DropDownList1.DataBind();
        this.DropDownList1.Items.Insert(0, new ListItem("--Select--", "0"));
 

    }
    private void ResetAllFields()
    {

        txtdiagnosis.Text = "";
        DropDownList1.SelectedIndex = 0;
        Button1.Text = "Submit";

        if (Objmain.checkaccss(Session["CoCode"].ToString(), Session["UserRoleID"].ToString().Trim(), "DIAGNOSIS MASTER", checkAccessType.InsertAction) == false)
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

            Label DiagnosisName = (Label)GridView1.Rows[index].FindControl("DiagnosisName");
            txtdiagnosis.Text = DiagnosisName.Text;


            Label Disciplineid = (Label)GridView1.Rows[index].FindControl("Disciplineid");
            DropDownList1.SelectedValue = Disciplineid.Text;
            Button1.Text = "Update";
            Tab1Func();

            if (Objmain.checkaccss(Session["CoCode"].ToString(), Session["UserRoleID"].ToString().Trim(), "DIAGNOSIS MASTER", checkAccessType.UpdateAction) == false)
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
        theHelper.DeleteDiagnosis(lblID.Text, Session["CoCode"].ToString().Trim());
        lblError.ForeColor = System.Drawing.Color.Green;
        lblError.Text = "Deleted Successfully";
        GridFill();
        ResetAllFields();
    }
    protected void Button1_Click(object sender, EventArgs e)
    {

        if (Button1.Text == "Submit")
        {

            if (theHelper.InsertDiagnosis(DropDownList1.SelectedValue, txtdiagnosis.Text, Session["CoCode"].ToString().Trim()) == true)
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "msg", "alert('Inserted Successfully !');", true);
            }
        }
        else
        {
            if (theHelper.UpdateDiagnosis(HiddenField1.Value, DropDownList1.SelectedValue, txtdiagnosis.Text, Session["CoCode"].ToString().Trim()) == true)
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
    public static List<string> Searchdiagno(string prefixText, int count)
    {
        using (SqlConnection conn = new SqlConnection())
        {
            conn.ConnectionString = ConfigurationSettings.AppSettings["ConnectionString"].ToString();
            using (SqlCommand cmd = new SqlCommand())
            {
                cmd.CommandText = "select distinct DiagnosisName as Name from GN_Diagnosis where DiagnosisName like @SearchText +'%'";
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
            if (Objmain.checkaccss(Session["CoCode"].ToString(), Session["UserRoleID"].ToString().Trim(), "DIAGNOSIS MASTER", checkAccessType.DeleteAction) == false)
            {
                coldel.Visible = false;
                e.Row.Cells[5].Visible = false;
            }

        }
    }
}