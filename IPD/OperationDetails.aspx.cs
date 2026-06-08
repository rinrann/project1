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

public partial class Master_OperationDetails : System.Web.UI.Page
{
    Main Objmain = new Main(ConfigurationSettings.AppSettings["ConnectionString"].ToString());
    OperationDetails theHelper = new OperationDetails(ConfigurationSettings.AppSettings["ConnectionString"].ToString());
    protected void Page_Load(object sender, EventArgs e)
    {
        Page.Title = "Operation Details";
        if (Session["userName"] == null)
        {
            Response.Redirect("../LoginPage.aspx");
        }
        if (Objmain.checkaccss(Session["CoCode"].ToString(), Session["UserRoleID"].ToString().Trim(), "OPERATION DETAILS", checkAccessType.ViewAction) == false)
        {
            Response.Redirect("../AccessDenied.aspx");
        }

        if (Objmain.checkaccss(Session["CoCode"].ToString(), Session["UserRoleID"].ToString().Trim(), "OPERATION DETAILS", checkAccessType.InsertAction) == false)
        {
            Button1.Enabled = false;
        }
        if (!IsPostBack)
        {
            Tab1Func();
            GridFill();
            DropDownFill();
            txtOperationId.Text = theHelper.GetOperationID(Session["CoCode"].ToString().Trim().Trim()).ToString();
        }
    }
    private void GridFill()
    {
        GridView1.DataSource = theHelper.GetAllOperation(Session["CoCode"].ToString().Trim());
        GridView1.DataBind();
    }
    private void ResetAllFields()
    {

        txtOperationId.Text = "";
        txtOperationName.Text = "";
        DropDownList1.SelectedIndex = -1;
        txtOperationCost.Text = "";
        txtOperationSummary.Text = "";
        txtDuration.Text = "";
        Button1.Text = "Submit";
        txtOperationId.Text = theHelper.GetOperationID(Session["CoCode"].ToString().Trim()).ToString();
        if (Objmain.checkaccss(Session["CoCode"].ToString(), Session["UserRoleID"].ToString().Trim(), "OPERATION DETAILS", checkAccessType.InsertAction) == false)
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

            Label lblOperationID = (Label)GridView1.Rows[index].FindControl("OperationID");
            txtOperationId.Text = lblOperationID.Text;
            HiddenField1.Value = lblOperationID.Text;

            Label lblOperationName = (Label)GridView1.Rows[index].FindControl("OperationName");
            txtOperationName.Text = lblOperationName.Text;


             DropDownFill();
            Label lblOperationTypeID = (Label)GridView1.Rows[index].FindControl("OperationTypeID");
            DropDownList1.SelectedValue = lblOperationTypeID.Text;

            Label lblOperationCost = (Label)GridView1.Rows[index].FindControl("OperationCost");
            txtOperationCost.Text = lblOperationCost.Text;

            Label lblOperationSummary = (Label)GridView1.Rows[index].FindControl("OperationSummary");
            txtOperationSummary.Text = lblOperationSummary.Text;

            Label lblDuration = (Label)GridView1.Rows[index].FindControl("Duration");
            txtDuration.Text = lblDuration.Text;

            Label lbldelivery = (Label)GridView1.Rows[index].FindControl("deliverytype");
            string deli = lbldelivery.Text;
            if (deli == "1")
            {
                chkDeli.Checked = true;
            }
            else
            {
                chkDeli.Checked = false;
            }
            Tab1Func();
            Button1.Text = "Update";
            if (Objmain.checkaccss(Session["CoCode"].ToString(), Session["UserRoleID"].ToString().Trim(), "OPERATION DETAILS", checkAccessType.UpdateAction) == false)
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
        this.DropDownList1.DataTextField = "OperationTypeName";
        this.DropDownList1.DataValueField = "OperationTypeID";
        this.DropDownList1.DataBind();
        this.DropDownList1.Items.Insert(0, new ListItem("--Select--", "0"));
    }
    protected void GridView1_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
         Label lblOperationID = (Label)GridView1.Rows[e.RowIndex].FindControl("OperationID");
         theHelper.DeleteOperationDetails(Convert.ToInt32(lblOperationID.Text), Session["CoCode"].ToString().Trim());
        ScriptManager.RegisterStartupScript(this, this.GetType(), "msg", "alert('Deleted Successfully !');", true);
        GridFill();
        ResetAllFields();
        txtOperationId.Text = theHelper.GetOperationID(Session["CoCode"].ToString().Trim()).ToString();
    }
    protected void Button1_Click(object sender, EventArgs e)
    {
        string chkdeli = "";
        if (chkDeli.Checked == true)
        {
            chkdeli = "1";
        }
        else
        {
            chkdeli = "0";
        }
          if (Button1.Text == "Submit")
        {

            theHelper.InsertOperationDetails(txtOperationId.Text,txtOperationName.Text, DropDownList1.SelectedValue, txtOperationCost.Text, txtOperationSummary.Text, txtDuration.Text, Session["userName"].ToString(), Session["CoCode"].ToString().Trim(), chkdeli);
            ScriptManager.RegisterStartupScript(this, this.GetType(), "msg", "alert('Inserted Successfully !');", true);
        }
        else
        {
            theHelper.UpdateOperationDetails(HiddenField1.Value, txtOperationName.Text, DropDownList1.SelectedValue, txtOperationCost.Text, txtOperationSummary.Text, txtDuration.Text, Session["CoCode"].ToString().Trim(),chkdeli);
            ScriptManager.RegisterStartupScript(this, this.GetType(), "msg", "alert('Updated Successfully !');", true);
            Button1.Text = "Submit";
        }

        ResetAllFields();
        GridFill();
        txtOperationId.Text = theHelper.GetOperationID(Session["CoCode"].ToString().Trim()).ToString();
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
    public static List<string> Searchotname(string prefixText, int count)
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
                cmd.CommandText = "select distinct OperationName as Name from IPD_OperationDetails where compcode=@Compcode and OperationName like @SearchText +'%'";
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
            if (Objmain.checkaccss(Session["CoCode"].ToString(), Session["UserRoleID"].ToString().Trim(), "OPERATION DETAILS", checkAccessType.DeleteAction) == false)
            {
                coldel.Visible = false;
                e.Row.Cells[8].Visible = false;
            }

        }
    }
}