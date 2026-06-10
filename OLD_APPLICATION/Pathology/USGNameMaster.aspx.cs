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
 
public partial class Pathology_USGNameMaster : System.Web.UI.Page
{
    Main Objmain = new Main(ConfigurationSettings.AppSettings["ConnectionString"].ToString());

    PH_USGNameMaster theNameMaster=new PH_USGNameMaster(ConfigurationSettings.AppSettings["ConnectionString"].ToString());
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["userName"] == null)
        {
            Response.Redirect("../LoginPage.aspx");
        }
        if (Objmain.checkaccss(Session["CoCode"].ToString(), Session["UserRoleID"].ToString().Trim(), "USG NAME MASTER", checkAccessType.ViewAction) == false)
        {
            Response.Redirect("../AccessDenied.aspx");
        }

        if (Objmain.checkaccss(Session["CoCode"].ToString(), Session["UserRoleID"].ToString().Trim(), "USG NAME MASTER", checkAccessType.InsertAction) == false)
        {
            Button1.Enabled = false;
        }
        if (Objmain.checkaccss(Session["CoCode"].ToString(), Session["UserRoleID"].ToString().Trim(), "USG NAME MASTER", checkAccessType.DeleteAction) == false)
        {
            GridView1.Columns[5].Visible = false;
        }

        Page.Title = "USG Name Master";
        if (Session["userName"] == null)
        {
            Response.Redirect("../LoginPage.aspx");
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
        GridView1.DataSource = theNameMaster.GridFill(Session["CoCode"].ToString().Trim());
        GridView1.DataBind();
    }

    private void DropDownFill()
    {
        this.DropDownList1.DataSource = theNameMaster.DropdownGroup(Session["CoCode"].ToString().Trim());
        this.DropDownList1.DataTextField = "GroupName";
        this.DropDownList1.DataValueField = "GroupCode";
        this.DropDownList1.DataBind();
        this.DropDownList1.Items.Insert(0, new ListItem("--Select--", "0"));
    }

    private void DropDownSubFill(string sub)
    {
        this.DropDownList2.DataSource = theNameMaster.DropdownSubGroup(sub, Session["CoCode"].ToString().Trim());
        this.DropDownList2.DataTextField = "SubGrName";
        this.DropDownList2.DataValueField = "SubGrID";
        this.DropDownList2.DataBind();
        this.DropDownList2.Items.Insert(0, new ListItem("--Select--", "0"));
    }
    private void ResetAllFields()
    {

       txtname.Text = "";
       DropDownList1.SelectedIndex = 0; DropDownList2.SelectedIndex = 0;
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
            txtname.Text = lblname.Text;

            Label lblgrname = (Label)GridView1.Rows[index].FindControl("lblgrname");
            DropDownList1.SelectedIndex = DropDownList1.Items.IndexOf(DropDownList1.Items.FindByText(lblgrname.Text));

            DropDownSubFill(DropDownList1.SelectedValue);
            Label lblsubname = (Label)GridView1.Rows[index].FindControl("lblsubname");
            DropDownList2.SelectedIndex = DropDownList2.Items.IndexOf(DropDownList2.Items.FindByText(lblsubname.Text));
            Tab1Func();
            Button1.Text = "Update";
            if (Objmain.checkaccss(Session["CoCode"].ToString(), Session["UserRoleID"].ToString().Trim(), "USG NAME MASTER", checkAccessType.UpdateAction) == false)
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
         theNameMaster.DeleteGroup(lblcode.Text, Session["CoCode"].ToString().Trim());
        ScriptManager.RegisterStartupScript(this, this.GetType(), "msg", "alert('Deleted Successfully !');", true);
        GridFill();
        ResetAllFields();
    }
    protected void Button1_Click(object sender, EventArgs e)
    {
        TextBox t;
        string id;
          if (TextBox4.Value != "")
        {
            id = TextBox4.Value;
        }
        else
        {
            id = "null";
        }

        if (DropDownList2.SelectedIndex == 0 || txtname.Text == "")
        {
            lblError.ForeColor = System.Drawing.Color.Red;
            lblError.Text = ("Plz Select Subgroup or enter Template Name ");
        }
        else
        {
            if (Button1.Text == "Submit")
            {
                DataTable dt = theNameMaster.GenerateCode();
                theNameMaster.InsertGroup(dt.Rows[0][0].ToString(), DropDownList1.SelectedValue, DropDownList2.SelectedValue, txtname.Text, Session["userName"].ToString(), Session["CoCode"].ToString().Trim());
                ScriptManager.RegisterStartupScript(this, this.GetType(), "msg", "alert('Inserted Successfully !');", true);
            }
            else
            {


                theNameMaster.UpdateGroup(TextBox4.Value, DropDownList1.SelectedValue, DropDownList2.SelectedValue, txtname.Text, Session["CoCode"].ToString().Trim());
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
    protected void DropDownList1_SelectedIndexChanged(object sender, EventArgs e)
    {
        DropDownSubFill(DropDownList1.SelectedValue);
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
    public static List<string> SearchUSGName(string prefixText, int count)
    {
        using (SqlConnection conn = new SqlConnection())
        {
            conn.ConnectionString = ConfigurationSettings.AppSettings["ConnectionString"].ToString();
            using (SqlCommand cmd = new SqlCommand())
            {
                cmd.CommandText = "select distinct Name as Name from PH_USGNameMaster where Name like @SearchText +'%'";
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