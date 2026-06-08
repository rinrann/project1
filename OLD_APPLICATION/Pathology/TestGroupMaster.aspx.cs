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

public partial class Pathology_TestGroupMaster : System.Web.UI.Page
{
    Main Objmain = new Main(ConfigurationSettings.AppSettings["ConnectionString"].ToString());
    PH_ProfileMaster theprofile = new PH_ProfileMaster(ConfigurationSettings.AppSettings["ConnectionString"].ToString());

    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["userName"] == null)
        {
            Response.Redirect("../LoginPage.aspx");
        }
        if (Objmain.checkaccss(Session["CoCode"].ToString(), Session["UserRoleID"].ToString().Trim(), "INVESTIGATION GROUP MASTER", checkAccessType.ViewAction) == false)
        {
            Response.Redirect("../AccessDenied.aspx");
        }

        if (Objmain.checkaccss(Session["CoCode"].ToString(), Session["UserRoleID"].ToString().Trim(), "INVESTIGATION GROUP MASTER", checkAccessType.InsertAction) == false)
        {
            Button3.Enabled = false;
        }
        Page.Title = "Test Group Master";


        if (Session["userName"] == null)
        {
            Response.Redirect("../LoginPage.aspx");
        }
        if (!IsPostBack)
        {
            DropDownFill();
        }
        GridFill();
        if (!IsPostBack)
        {
            Tab1Func();
        }
    }

    private void DropDownFill()
    {
        this.DropDownList1.DataSource = theprofile.DropdownDepartment(Session["CoCode"].ToString());
        this.DropDownList1.DataTextField = "DeptName";
        this.DropDownList1.DataValueField = "DeptCode";
        this.DropDownList1.DataBind();
        this.DropDownList1.Items.Insert(0, new ListItem("--Select--", "0"));

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

    private void GridFill()
    {
        DataTable dt = theprofile.getGroupDetails(Session["CoCode"].ToString(), txtgrpname.Text.Trim());
        GridView1.DataSource = dt;
        GridView1.DataBind();
    }

    protected void GridView1_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        Label lblcode = (Label)GridView1.Rows[e.RowIndex].FindControl("lblcode");
        theprofile.DeleteTestGroup(lblcode.Text, Session["CoCode"].ToString());
        ScriptManager.RegisterStartupScript(this, this.GetType(), "msg", "alert('Deleted Successfully !');", true);
        GridFill();
        ResetAllFields();
    }

    protected void GridView1_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        GridView1.PageIndex = e.NewPageIndex;
        GridFill();
    }

    private void ResetAllFields()
    {
        DropDownList1.SelectedValue = "0";
        txtcode.Text = "";
        txtname.Text = "";
        Button3.Text = "Submit";
    }

    protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "Select")
        {
            int index = Convert.ToInt32(e.CommandArgument);
            Label lblcode = (Label)GridView1.Rows[index].FindControl("lblcode");
            txtcode.Text = lblcode.Text;
            txtcode.ReadOnly = true;
            txtcode.Enabled = false;
            Label lblname = (Label)GridView1.Rows[index].FindControl("lblname");
            txtname.Text = lblname.Text;
            Label lbldeptCode = (Label)GridView1.Rows[index].FindControl("lbldeptCode");
            DropDownList1.SelectedValue = lbldeptCode.Text;

            Label lblcons = (Label)GridView1.Rows[index].FindControl("lblcons");
            
            if (lblcons.Text == "1")
            {
                chkcon.Checked = true;
            }
            else
            {
                chkcon.Checked = false;
            }
            Label lblTestType = (Label)GridView1.Rows[index].FindControl("lblTestType");
            ddltesttype.SelectedValue = lblTestType.Text;
            Tab1Func();
            Button3.Text = "Update";
            if (Objmain.checkaccss(Session["CoCode"].ToString(), Session["UserRoleID"].ToString().Trim(), "TEST GROUP MASTER", checkAccessType.UpdateAction) == false)
            {
                Button3.Enabled = false;
            }
            else
            {
                Button3.Enabled = true;
            }
        }
    }

    [System.Web.Script.Services.ScriptMethod()]
    [System.Web.Services.WebMethod]
    public static List<string> SearchTestGroup(string prefixText, int count)
    {
        using (SqlConnection conn = new SqlConnection())
        {
            conn.ConnectionString = ConfigurationSettings.AppSettings["ConnectionString"].ToString();
            using (SqlCommand cmd = new SqlCommand())
            {
                cmd.CommandText = "select distinct ProfileName as Name from PH_ProfileMaster where compcode=@a_CompCode and ProfileName like @SearchText +'%'";
                cmd.Parameters.AddWithValue("@SearchText", prefixText);
                cmd.Parameters.AddWithValue("@a_CompCode", HttpContext.Current.Session["CoCode"].ToString());
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
    protected void Button3_Click(object sender, EventArgs e)
    {
        if (txtcode.Text == "")
        {
            lblError.Text = "Group Code cannot be blank!";
        }
        else if (txtname.Text == "")
        {
            lblError.Text = "Group Name cannot be blank!";
        }
        else if (DropDownList1.SelectedValue == "0")
        {
            lblError.Text = "Select Department!";
        }
        else
        {
            //string cons = "T";
            //if (chkcon.Checked == true)
            //{
            //    cons = "C";
            //}
            //else
            //{
            //    cons = "T";
            //}
            if (Button3.Text == "Submit")
            {
                if (theprofile.InsertInvestigationGroup(txtcode.Text, txtname.Text, DropDownList1.SelectedValue, Session["CoCode"].ToString(), ddltesttype.SelectedValue) == true)
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "msg", "alert('Group Successfully Added!');", true);
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "msg", "alert('Error Occured during saving!');", true);
                }
            }
            else
            {
                if (theprofile.UpdateInvestigationGroup(txtcode.Text, txtname.Text, DropDownList1.SelectedValue, Session["CoCode"].ToString(), ddltesttype.SelectedValue) == true)
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "msg", "alert('Record Successfully Updated!');", true);
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "msg", "alert('Error Occured during saving!');", true);
                }
            }
        }
    }
    protected void Button4_Click(object sender, EventArgs e)
    {
        ResetAllFields();
    }
    protected void Button1_Click(object sender, EventArgs e)
    {
        GridFill();
    }
}