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
 

public partial class Master_WingsMaster : System.Web.UI.Page
{
    Main Objmain = new Main(ConfigurationSettings.AppSettings["ConnectionString"].ToString());
    WingsMaster theHelper = new WingsMaster(ConfigurationSettings.AppSettings["ConnectionString"].ToString());
    protected void Page_Load(object sender, EventArgs e)
    {
        Page.Title = "Wings Master";
        if (Session["userName"] == null)
        {
            Response.Redirect("../LoginPage.aspx");
        }
        if (Objmain.checkaccss(Session["CoCode"].ToString(), Session["UserRoleID"].ToString().Trim(), "WINGS MASTER", checkAccessType.ViewAction) == false)
        {
            Response.Redirect("../AccessDenied.aspx");
        }

        if (Objmain.checkaccss(Session["CoCode"].ToString(), Session["UserRoleID"].ToString().Trim(), "WINGS MASTER", checkAccessType.InsertAction) == false)
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
        GridView1.DataSource = theHelper.GetAllWings(Session["CoCode"].ToString().Trim());
        GridView1.DataBind();
    }
    private void ResetAllFields()
    {
        txtWingsName.Text = "";
        DropDownList1.SelectedIndex = -1;
        txtPattText.Text = "";
        Button1.Text = "Submit";
        if (Objmain.checkaccss(Session["CoCode"].ToString(), Session["UserRoleID"].ToString().Trim(), "WINGS MASTER", checkAccessType.InsertAction) == false)
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

            Label lblWingsID = (Label)GridView1.Rows[index].FindControl("WingsID");
             HiddenField1.Value = lblWingsID.Text;

            Label lblWingsName = (Label)GridView1.Rows[index].FindControl("WingsName");
            txtWingsName.Text = lblWingsName.Text;

            Label WorkStation = (Label)GridView1.Rows[index].FindControl("WorkStation");
            txtWorkStinnm.Text = WorkStation.Text;

              DropDownFill();
            Label lblFloorID = (Label)GridView1.Rows[index].FindControl("FloorID");
            //DropDownList1.SelectedIndex = DropDownList1.Items.IndexOf(DropDownList1.Items.FindByText(lblModuleID.Text));
            DropDownList1.SelectedValue = lblFloorID.Text;

            Label lblPattText = (Label)GridView1.Rows[index].FindControl("PatternText");
            txtPattText.Text = lblPattText.Text;
            Tab1Func();
            Button1.Text = "Update";

            if (Objmain.checkaccss(Session["CoCode"].ToString(), Session["UserRoleID"].ToString().Trim(), "WINGS MASTER", checkAccessType.UpdateAction) == false)
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
        DropDownList1.Items.Clear();
        this.DropDownList1.DataSource = theHelper.DropdownID(Session["CoCode"].ToString().Trim());
        this.DropDownList1.DataTextField = "FloorName";
        this.DropDownList1.DataValueField = "FloorID";
        this.DropDownList1.DataBind();
        this.DropDownList1.Items.Insert(0, new ListItem("--Select--", "0"));
    }
    protected void GridView1_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
         Label lblWingsID = (Label)GridView1.Rows[e.RowIndex].FindControl("WingsID");
         theHelper.DeleteWingsMaster(Convert.ToInt32(lblWingsID.Text), Session["CoCode"].ToString().Trim());
        lblError.ForeColor = System.Drawing.Color.Green;
        lblError.Text = "Deleted Successfully";
        GridFill();
        ResetAllFields();
        
    }
    protected void Button1_Click(object sender, EventArgs e)
    {
         
        if (Button1.Text == "Submit")
        {

            theHelper.InsertWingsMaster(txtWingsName.Text, DropDownList1.SelectedValue, txtPattText.Text, Session["userName"].ToString(), Session["CoCode"].ToString().Trim(), txtWorkStinnm.Text);
            ScriptManager.RegisterStartupScript(this, this.GetType(), "msg", "alert('Inserted  Successfully !');", true);
        }
        else
        {
            theHelper.UpdateWingsMaster(HiddenField1.Value, txtWingsName.Text, DropDownList1.SelectedValue, txtPattText.Text, Session["CoCode"].ToString().Trim(), txtWorkStinnm.Text);
            ScriptManager.RegisterStartupScript(this, this.GetType(), "msg", "alert('Updated  Successfully !');", true);
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
    public static List<string> SearchWing(string prefixText, int count)
    {
        using (SqlConnection conn = new SqlConnection())
        {
            conn.ConnectionString = ConfigurationSettings.AppSettings["ConnectionString"].ToString();
            using (SqlCommand cmd = new SqlCommand())
            {
                cmd.CommandText = "select distinct WingsName as Name from IPD_WingsMaster where WingsName like @SearchText +'%'";
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
            if (Objmain.checkaccss(Session["CoCode"].ToString(), Session["UserRoleID"].ToString().Trim(), "WINGS MASTER", checkAccessType.DeleteAction) == false)
            {
                coldel.Visible = false;
                e.Row.Cells[7].Visible = false;
            }

        }
    }
}
