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
 

public partial class OPD_PrescriptionGroup : System.Web.UI.Page
{
    Main Objmain = new Main(ConfigurationSettings.AppSettings["ConnectionString"].ToString());
    TemplateGroup theprescription = new TemplateGroup(ConfigurationSettings.AppSettings["ConnectionString"].ToString());
    protected void Page_Load(object sender, EventArgs e)
    {
        Page.Title = "Prescription Group Master";
        if (Session["userName"] == null)
        {
            Response.Redirect("../LoginPage.aspx");
        }
        if (Objmain.checkaccss(Session["CoCode"].ToString(), Session["UserRoleID"].ToString().Trim(), "PRESCRIPTION GROUP MASTER", checkAccessType.ViewAction) == false)
        {
            Response.Redirect("../AccessDenied.aspx");
        }

        if (Objmain.checkaccss(Session["CoCode"].ToString(), Session["UserRoleID"].ToString().Trim(), "PRESCRIPTION GROUP MASTER", checkAccessType.InsertAction) == false)
        {
            Button1.Enabled = false;
        }

        if (Objmain.checkaccss(Session["CoCode"].ToString(), Session["UserRoleID"].ToString().Trim(), "PRESCRIPTION GROUP MASTER", checkAccessType.DeleteAction) == false)
        {
            GridView1.Columns[3].Visible = false;
        }

        GridFill();
        if (!IsPostBack)
        {
            Tab1Func();
        }


    }
    private void ResetAllFields()
    {
        txtchemicalname.Text = "";
        HiddenField1.Value = "";
        Button1.Text = "Submit";

        if (Objmain.checkaccss(Session["CoCode"].ToString(), Session["UserRoleID"].ToString().Trim(), "PRESCRIPTION GROUP MASTER", checkAccessType.InsertAction) == false)
        {
            Button1.Enabled = false;
        }

    }
    private void GridFill()
    {
        GridView1.DataSource = theprescription.GridtemplateGroup(Session["CoCode"].ToString().Trim());
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
            lblError.Text = "";
            int index = Convert.ToInt32(e.CommandArgument);
            Label lblID = (Label)GridView1.Rows[index].FindControl("lblID");
            HiddenField1.Value = lblID.Text;

            Label lblname = (Label)GridView1.Rows[index].FindControl("lblname");
            txtchemicalname.Text = lblname.Text;
            Tab1Func();
            Button1.Text = "Update";

            if (Objmain.checkaccss(Session["CoCode"].ToString(), Session["UserRoleID"].ToString().Trim(), "PRESCRIPTION GROUP MASTER", checkAccessType.UpdateAction) == false)
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
        theprescription.DeletePrescription(lblID.Text, Session["CoCode"].ToString().Trim());
        ScriptManager.RegisterStartupScript(this, this.GetType(), "msg", "alert('Deleted Successfully !');", true);
        GridFill();
        ResetAllFields();
    }
    protected void Button1_Click(object sender, EventArgs e)
    {
        if (Button1.Text == "Submit")
        {

            if (theprescription.InsertPrescriptionGroup(txtchemicalname.Text, Session["CoCode"].ToString().Trim()) == true)
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "msg", "alert('Inserted Successfully !');", true);
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "msg", "alert('Error in Inserted Data !');", true);
            }


        }
        else
        {

            if (theprescription.UpdatePrescription(HiddenField1.Value, txtchemicalname.Text, Session["CoCode"].ToString().Trim()) == true)
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "msg", "alert('Updated Successfully !');", true);
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "msg", "alert('Error in Updated data !');", true);
            }
            Button1.Text = "Submit";
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

    [System.Web.Script.Services.ScriptMethod()]
    [System.Web.Services.WebMethod]
    public static List<string> SearchPresGroup(string prefixText, int count)
    {
        using (SqlConnection conn = new SqlConnection())
        {
            conn.ConnectionString = ConfigurationSettings.AppSettings["ConnectionString"].ToString();
            using (SqlCommand cmd = new SqlCommand())
            {
                cmd.CommandText = "select distinct PrescriptionGroupName as Name from IPD_PrescriptionTmplateGroup where PrescriptionGroupName like @SearchText +'%'";
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
            if (Objmain.checkaccss(Session["CoCode"].ToString(), Session["UserRoleID"].ToString().Trim(), "PRESCRIPTION GROUP MASTER", checkAccessType.DeleteAction) == false)
            {
                e.Row.Cells[3].Visible = false;
            }

        }
    }
}