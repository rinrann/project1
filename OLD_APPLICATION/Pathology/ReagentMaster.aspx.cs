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

 
public partial class Pathology_ReagentMaster : System.Web.UI.Page
{
    PH_ReagentMaster thereagent = new PH_ReagentMaster(ConfigurationSettings.AppSettings["ConnectionString"].ToString());
    protected void Page_Load(object sender, EventArgs e)
    {

        Page.Title = "Reagent / Kit Master";
        if (Session["userId"] == null)
        {
            Response.Redirect("../LoginPage.aspx");
        }
        GridFill();

        if (!IsPostBack)
        {
            DropDownFill();
            generateagent();
            Tab1Func();
        }
    }
    private void generateagent()
    {
        DataTable dt = thereagent.GenerateReagentCode(Session["CoCode"].ToString().Trim());
        txtcode.Text = dt.Rows[0][0].ToString();
        
    }

    private void ResetAllFields()
    {
        DropDownList1.SelectedIndex = 0;
        DropDownList2.SelectedIndex = 0;
        txtcode.Text = "";
        txtname.Text = "";
        txtminstock.Text = "";
        txtperunit.Text = "";
        TextBox4.Value = "";
        Button1.Text = "Submit";
        generateagent();

    }
    private void DropDownFill()
    {
        DropDownList1.DataSource = thereagent.DropdownUnit(Session["CoCode"].ToString().Trim());
       DropDownList1.DataTextField = "UnitName";
       DropDownList1.DataValueField = "UnitId";
       DropDownList1.DataBind();
       DropDownList1.Items.Insert(0, new ListItem("--Select--", "0"));

       DropDownList2.DataSource = thereagent.DropdownUnit(Session["CoCode"].ToString().Trim());
       DropDownList2.DataTextField = "UnitName";
       DropDownList2.DataValueField = "UnitId";
       DropDownList2.DataBind();
       DropDownList2.Items.Insert(0, new ListItem("--Select--", "0"));
    }

    private void GridFill()
    {
        GridView1.DataSource = thereagent.GridAgent(Session["CoCode"].ToString().Trim(), txtReagent.Text.Trim());
        GridView1.DataBind();
    }
    protected void Button1_Click(object sender, EventArgs e)
    {
        string id;
        if (TextBox4.Value != "")
        {
            id = TextBox4.Value.ToString();
        }
        else
        {
            id = "null";
        }
        if (Button1.Text == "Submit")
        {

            thereagent.InsertReagent(txtcode.Text, txtname.Text, DropDownList1.SelectedValue, txtperunit.Text, txtminstock.Text, Session["userId"].ToString().Trim(), Session["CoCode"].ToString().Trim(), Session["YearCode"].ToString().Trim(), DropDownList2.SelectedValue);
            ScriptManager.RegisterStartupScript(this, this.GetType(), "msg", "alert('Inserted Successfully !');", true);


        }
        else
        {

            thereagent.UpdateReaent(id, txtname.Text, DropDownList1.SelectedValue, txtperunit.Text, txtminstock.Text, Session["CoCode"].ToString().Trim(), DropDownList2.SelectedValue);
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
            TextBox4.Value = lblcode.Text;

            Label lblname = (Label)GridView1.Rows[index].FindControl("lblname");
            txtname.Text = lblname.Text;

            Label lblunit = (Label)GridView1.Rows[index].FindControl("lblunit");
            DropDownList1.SelectedIndex = DropDownList1.Items.IndexOf(DropDownList1.Items.FindByText(lblunit.Text));


            Label lbltestper = (Label)GridView1.Rows[index].FindControl("lbltestper");
            txtperunit.Text = lbltestper.Text;


            Label lblminstock = (Label)GridView1.Rows[index].FindControl("lblminstock");
            txtminstock.Text = lblminstock.Text;
            Label lbltxtunit2 = (Label)GridView1.Rows[index].FindControl("Label2");
            //DropDownList2.SelectedIndex = DropDownList2.Items.IndexOf(DropDownList2.Items.FindByText(lbltxtunit2.Text));
            DropDownList2.SelectedValue = lbltxtunit2.Text;
            Tab1Func();
            Button1.Text = "Update";
        }
    }
    protected void GridView1_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        Label lblcode = (Label)GridView1.Rows[e.RowIndex].FindControl("lblcode");
        thereagent.DeleteReagent(lblcode.Text, Session["CoCode"].ToString().Trim(), Session["YearCode"].ToString().Trim());
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
    public static List<string> SearchReagent(string prefixText, int count)
    {
        using (SqlConnection conn = new SqlConnection())
        {
            conn.ConnectionString = ConfigurationSettings.AppSettings["ConnectionString"].ToString();
            using (SqlCommand cmd = new SqlCommand())
            {
                cmd.CommandText = "select distinct RName Name from PH_ReagentMaster where RName like @SearchText +'%'";
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

    protected void Button3_Click(object sender, EventArgs e)
    {
        GridFill();
    }
}