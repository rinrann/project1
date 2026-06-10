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
using System.Globalization;

public partial class Medicine_MedicineSalePopup : System.Web.UI.Page
{
    MedicineSales theHelper = new MedicineSales(ConfigurationSettings.AppSettings["ConnectionString"].ToString());
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["userName"] == null)
        {
            Response.Redirect("../LoginPage.aspx");
        }

        if (!IsPostBack)
        {
            GridFill();
        }
    }
    public void GridFill()
    {
        string frmdate;
        string todate;
        System.Globalization.DateTimeFormatInfo dtf = new DateTimeFormatInfo();
        if (TextBox2.Text != "")
        {
            frmdate = DateTime.ParseExact(TextBox2.Text, "dd/MM/yyyy", dtf).ToString("yyyy-MM-dd");
        }
        else
        {
            frmdate = "null";
        }
        if (TextBox3.Text != "")
        {
            todate = DateTime.ParseExact(TextBox3.Text, "dd/MM/yyyy", dtf).ToString("yyyy-MM-dd");
        }
        else
        {
            todate = "null";
        }


        GridView_popup.DataSource = theHelper.GridPopup(Session["CoCode"].ToString().Trim(), Session["YearCode"].ToString().Trim(), TextBox1.Text, frmdate, todate, TextBox4.Text);
        GridView_popup.DataBind();
    }
    protected void Button2_Click(object sender, EventArgs e)
    {
        GridFill();
    }
    protected void GridView_popup_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        GridView_popup.PageIndex = e.NewPageIndex;
        GridFill();
    }
    protected void GridView_popup_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        Label lblid = (Label)GridView_popup.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("lblid");
        Label lblbillno = (Label)GridView_popup.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("iblbillno");
        HiddenField1.Value = lblid.Text;
        HiddenField2.Value = lblbillno.Text;
    }
}