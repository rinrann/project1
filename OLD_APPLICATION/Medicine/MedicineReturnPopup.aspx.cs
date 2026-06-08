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
using System.Web.Security;
using System.Globalization;

public partial class Medicine_MedicineReturnPopup : System.Web.UI.Page
{

    MedicineReturnPopup thereg = new MedicineReturnPopup(ConfigurationSettings.AppSettings["ConnectionString"].ToString());
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["userName"] == null)
        {
            Response.Redirect("../LoginPage.aspx");
        }

        if (!IsPostBack)
        {
            GridFill();
            DropDownFill();
        }

    }


    public void GridFill()
    {
        string fromdate;
        string todate;
        System.Globalization.DateTimeFormatInfo dtf = new DateTimeFormatInfo();
        if (TextBox2.Text != "")
        {
            fromdate = DateTime.ParseExact(TextBox2.Text, "dd/MM/yyyy", dtf).ToString("yyyy-MM-dd");
        }
        else
        {
            fromdate = "";
        }
        if (TextBox4.Text != "")
        {
            todate = DateTime.ParseExact(TextBox4.Text, "dd/MM/yyyy", dtf).ToString("yyyy-MM-dd");
        }
        else
        {
            todate = "";
        }


        GridView_popup.DataSource = thereg.GridPopup(Session["CoCode"].ToString().Trim(), Session["YearCode"].ToString().Trim(), TextBox1.Text, DropDownList1.SelectedValue, fromdate, TextBox3.Text, todate);
        GridView_popup.DataBind();
    }
    protected void Button2_Click(object sender, EventArgs e)
    {
        GridFill();
    }

    private void DropDownFill()
    {
        DropDownList1.Items.Clear();
        DropDownList1.DataSource = thereg.DropdownID4(Session["CoCode"].ToString().Trim());
        DropDownList1.DataTextField = "SName";
        DropDownList1.DataValueField = "SCode";
        DropDownList1.DataBind();
        DropDownList1.Items.Insert(0, new ListItem("--Select--", "0"));
    }


    protected void GridView_popup_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {

        GridView_popup.PageIndex = e.NewPageIndex;
        GridFill();
    }
    protected void GridView_popup_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        Label lblid = (Label)GridView_popup.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("lblid");
        HiddenField1.Value = lblid.Text;
    }
}