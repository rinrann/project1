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

public partial class Master_DailyRecordMaster : System.Web.UI.Page
{
    Main Objmain = new Main(ConfigurationSettings.AppSettings["ConnectionString"].ToString());
    DailyRecordMaster theHelper = new DailyRecordMaster(ConfigurationSettings.AppSettings["ConnectionString"].ToString());
    protected void Page_Load(object sender, EventArgs e)
    {
        Page.Title = "Daily Record Master";
        if (Session["userName"] == null)
        {
            Response.Redirect("../LoginPage.aspx");
        }
        if (Objmain.checkaccss(Session["CoCode"].ToString(), Session["UserRoleID"].ToString().Trim(), "DAILY RECORD MASTER", checkAccessType.ViewAction) == false)
        {
            Response.Redirect("../AccessDenied.aspx");
        }

        if (Objmain.checkaccss(Session["CoCode"].ToString(), Session["UserRoleID"].ToString().Trim(), "DAILY RECORD MASTER", checkAccessType.InsertAction) == false)
        {
            Button1.Enabled = false;
        }
        if (Objmain.checkaccss(Session["CoCode"].ToString(), Session["UserRoleID"].ToString().Trim(), "DAILY RECORD MASTER", checkAccessType.DeleteAction) == false)
        {
            GridView1.Columns[6].Visible = false;
        }
        if (!IsPostBack)
        {
            GridFill();
            DropDownFill();
            Tab1Func();
            txtRecordId.Text = theHelper.GetRecordID(Session["CoCode"].ToString().Trim()).ToString();
        }
    }
    private void GridFill()
    {
        GridView1.DataSource = theHelper.GetAllRecord(Session["CoCode"].ToString().Trim());
        GridView1.DataBind();
    }
    private void ResetAllFields()
    {

        txtRecordId.Text = "";
        txtRecordName.Text = "";
        DropDownList1.SelectedIndex = -1;
        DropDownList2.SelectedIndex = -1;
        Button1.Text = "Submit";
        txtRecordId.Text = theHelper.GetRecordID(Session["CoCode"].ToString().Trim()).ToString();

        if (Objmain.checkaccss(Session["CoCode"].ToString(), Session["UserRoleID"].ToString().Trim(), "DAILY RECORD MASTER", checkAccessType.InsertAction) == false)
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

            Label lblRecordID = (Label)GridView1.Rows[index].FindControl("RecordID");
            txtRecordId.Text = lblRecordID.Text;
            HiddenField1.Value = lblRecordID.Text;

            Label lblRecordName = (Label)GridView1.Rows[index].FindControl("RecordName");
            txtRecordName.Text = lblRecordName.Text;


             DropDownFill();
            Label lblUnitID = (Label)GridView1.Rows[index].FindControl("UnitId");
            DropDownList1.SelectedValue = lblUnitID.Text;

            Label lblActive = (Label)GridView1.Rows[index].FindControl("Active");
            DropDownList2.SelectedValue = lblActive.Text;
            Tab1Func();
            Button1.Text = "Update";

            if (Objmain.checkaccss(Session["CoCode"].ToString(), Session["UserRoleID"].ToString().Trim(), "DAILY RECORD MASTER", checkAccessType.UpdateAction) == false)
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
        this.DropDownList1.DataTextField = "UnitName";
        this.DropDownList1.DataValueField = "UnitId";
        this.DropDownList1.DataBind();
        this.DropDownList1.Items.Insert(0, new ListItem("--Select--", "0"));
    }
    protected void GridView1_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
          Label lblConItemID = (Label)GridView1.Rows[e.RowIndex].FindControl("RecordID");
          theHelper.DeleteDailyRecordMaster(Convert.ToInt32(lblConItemID.Text), Session["CoCode"].ToString().Trim());
        lblError.ForeColor = System.Drawing.Color.Green;
        lblError.Text = "Deleted Successfully";
        GridFill();
        ResetAllFields();
        txtRecordId.Text = theHelper.GetRecordID(Session["CoCode"].ToString().Trim()).ToString();
    }
    protected void Button1_Click(object sender, EventArgs e)
    {
          if (Button1.Text == "Submit")
        {

            theHelper.InsertDailyRecordMaster(txtRecordName.Text, DropDownList1.SelectedValue, DropDownList2.SelectedValue, Session["userName"].ToString(), Session["CoCode"].ToString().Trim());
            ScriptManager.RegisterStartupScript(this, this.GetType(), "msg", "alert('Insered Successfully !');", true);
        }
        else
        {
            theHelper.UpdateDailyRecordMaster(HiddenField1.Value, txtRecordName.Text, DropDownList1.SelectedValue, DropDownList2.SelectedValue, Session["CoCode"].ToString().Trim());
            ScriptManager.RegisterStartupScript(this, this.GetType(), "msg", "alert('Updated Successfully !');", true);
            Button1.Text = "Submit";
        }

        ResetAllFields();
        GridFill();
        txtRecordId.Text = theHelper.GetRecordID(Session["CoCode"].ToString().Trim()).ToString();
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
    public static List<string> Searchbedno(string prefixText, int count)
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
                cmd.CommandText = "select distinct RecordName as Name from IPD_DailyRecordMaster where compcode=@Compcode and RecordName like @SearchText +'%'";
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
            if (Objmain.checkaccss(Session["CoCode"].ToString(), Session["UserRoleID"].ToString().Trim(), "DAILY RECORD MASTER", checkAccessType.DeleteAction) == false)
            {
                e.Row.Cells[6].Visible = false;
            }

        }
    }
}