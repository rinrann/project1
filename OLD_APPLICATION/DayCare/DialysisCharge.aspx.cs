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
using System.Drawing;
 
public partial class DayCare_DialysisCharge : System.Web.UI.Page
{
    Main Objmain = new Main(ConfigurationSettings.AppSettings["ConnectionString"].ToString());
    DC_DialysisCharge thecharge = new DC_DialysisCharge(ConfigurationSettings.AppSettings["ConnectionString"].ToString());
    protected void Page_Load(object sender, EventArgs e)
    {
        Page.Title = "Charge Details";
        if (Session["userName"] == null)
        {
            Response.Redirect("../LoginPage.aspx");
        }
        if (Objmain.checkaccss(Session["CoCode"].ToString(), Session["UserRoleID"].ToString().Trim(), "DIALYSER NAME", checkAccessType.ViewAction) == false)
        {
            Response.Redirect("../AccessDenied.aspx");
        }

        if (Objmain.checkaccss(Session["CoCode"].ToString(), Session["UserRoleID"].ToString().Trim(), "DIALYSER NAME", checkAccessType.InsertAction) == false)
        {
            Button1.Enabled = false;
        }
        if (Objmain.checkaccss(Session["CoCode"].ToString(), Session["UserRoleID"].ToString().Trim(), "DIALYSER NAME", checkAccessType.DeleteAction) == false)
        {
            GridView1.Columns[6].Visible = false;
        }
        GridFill();

        if (!IsPostBack)
        {
            DropDownFill();
            Tab1Func();
        }
    }
    private void ResetAllFields()
    {
       DropDownList1.SelectedIndex=0;
        TextBox2.Text = "";
        TextBox3.Text = "";
        TextBox1.Text = "";
        TextBox4.Value = "";
        Button1.Text = "Submit";
        if (Objmain.checkaccss(Session["CoCode"].ToString(), Session["UserRoleID"].ToString().Trim(), "DIALYSER NAME", checkAccessType.InsertAction) == false)
        {
            Button1.Enabled = false;
        }
        
    }
 
    protected void Button1_Click(object sender, EventArgs e)
    {
        int id;
        if (TextBox4.Value != "")
        {
            id = Convert.ToInt32(TextBox4.Value); 
        }
        else
        {
            id = 0;
        }
        if (Button1.Text == "Submit")
        {
            if (thecharge.InsertDialysisCharge(Convert.ToInt32(DropDownList1.SelectedValue), TextBox2.Text, Convert.ToDouble(TextBox3.Text), TextBox1.Text, Session["CoCode"].ToString().Trim(), Session["YearCode"].ToString().Trim()) == true)
                ScriptManager.RegisterStartupScript(this, this.GetType(), "msg", "alert('Insered Successfully !');", true);
            else
                ScriptManager.RegisterStartupScript(this, this.GetType(), "msg", "alert('Error In Inserted Data !');", true);
        }
        else
        {
            if (thecharge.UpdateDialysisCharge(id, Convert.ToInt32(DropDownList1.SelectedValue), TextBox2.Text, Convert.ToDouble(TextBox3.Text), TextBox1.Text, Session["CoCode"].ToString(), Session["YearCode"].ToString().Trim()) == true)
            ScriptManager.RegisterStartupScript(this, this.GetType(), "msg", "alert('Updated Successfully !');", true);
            else
               ScriptManager.RegisterStartupScript(this, this.GetType(), "msg", "alert('Error in Updated Data !');", true);

            Button1.Text = "Submit";
        }

        GridFill();
        ResetAllFields();
    }
    private void DropDownFill()
    {
        this.DropDownList1.DataSource = thecharge.DropdownDialysisType(Session["CoCode"].ToString().Trim());
        this.DropDownList1.DataTextField = "TypeName";
        this.DropDownList1.DataValueField = "TypeId";
        this.DropDownList1.DataBind();
        this.DropDownList1.Items.Insert(0, new ListItem("--Select--", "0"));
    }

    private void GridFill()
    {

        GridView1.SelectedIndex = -1;
        GridView1.DataSource = thecharge.GridDialysisCharge(Session["CoCode"].ToString().Trim(), Session["YearCode"].ToString().Trim());
        GridView1.DataBind();
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
     Label lblID = (Label)GridView1.Rows[index].FindControl("lblID");
            TextBox4.Value = lblID.Text;

            Label lbltype = (Label)GridView1.Rows[index].FindControl("lbltype");
            DropDownList1.SelectedIndex = DropDownList1.Items.IndexOf(DropDownList1.Items.FindByText(lbltype.Text));

            Label lblname = (Label)GridView1.Rows[index].FindControl("lblname");
            TextBox2.Text = lblname.Text;

            Label lblcharge = (Label)GridView1.Rows[index].FindControl("lblcharge");
            TextBox3.Text = lblcharge.Text;


            Label lblsercharge = (Label)GridView1.Rows[index].FindControl("lblsercharge");
            TextBox1.Text = lblsercharge.Text;

            Tab1Func();
            Button1.Text = "Update";
            if (Objmain.checkaccss(Session["CoCode"].ToString(), Session["UserRoleID"].ToString().Trim(), "DIALYSER NAME", checkAccessType.UpdateAction) == false)
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
        thecharge.DeleteDialysisCharge(Convert.ToInt32(lblID.Text), Session["CoCode"].ToString().Trim(), Session["YearCode"].ToString().Trim());
        lblError.ForeColor = System.Drawing.Color.Green;
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
        MainView.ActiveViewIndex =1;
    }

    [System.Web.Script.Services.ScriptMethod()]
    [System.Web.Services.WebMethod]
    public static List<string> SearchName(string prefixText, int count)
    {
        using (SqlConnection conn = new SqlConnection())
        {
            conn.ConnectionString = ConfigurationSettings.AppSettings["ConnectionString"].ToString();
            using (SqlCommand cmd = new SqlCommand())
            {
                cmd.CommandText = "select distinct DialysisName as Name from DC_DialysisCharge where DialysisName like @SearchText +'%'";
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
            if (Objmain.checkaccss(Session["CoCode"].ToString(), Session["UserRoleID"].ToString().Trim(), "DIALYSER NAME", checkAccessType.DeleteAction) == false)
            {
                e.Row.Cells[6].Visible = false;
            }
        }
    }
}