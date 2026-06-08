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
using System.Web.Services;
using System.Globalization;

public partial class IPD_DischargePatientList : System.Web.UI.Page
{
    Main Objmain = new Main(ConfigurationSettings.AppSettings["ConnectionString"].ToString());
    DischargePatientList thehlpr = new DischargePatientList(ConfigurationSettings.AppSettings["ConnectionString"].ToString());
    //System.Text.StringBuilder rpt = new System.Text.StringBuilder();
    String Date = DateTime.Now.ToString("dd/MM/yyyy");

    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["userName"] == null)
        {
            Response.Redirect("../LoginPage.aspx");
        }
        if (Objmain.checkaccss(Session["CoCode"].ToString(), Session["UserRoleID"].ToString().Trim(), "DISCHARGE PATIENT LIST", checkAccessType.ViewAction) == false)
        {
            Response.Redirect("../AccessDenied.aspx");
        }
        if (!IsPostBack)
        {
            GridFill();
        }
        Page.Title = "Discharge Patient List";
        Session["RegNo"] = null;
    }

    public void GridFill()
    {
         string frmdate="";
        string todate = "";
        System.Globalization.DateTimeFormatInfo dtf = new DateTimeFormatInfo();
        if (TextBox1.Text != "")
        {
            DateTime testdate1 = DateTime.ParseExact(TextBox1.Text, "dd/MM/yyyy", dtf);
            frmdate = testdate1.ToString("yyyy-MM-dd");
        }
        else
            frmdate ="";

        if (TextBox2.Text != "")
        {
            DateTime testdate2 = DateTime.ParseExact(TextBox2.Text, "dd/MM/yyyy", dtf);
            todate = testdate2.ToString("yyyy-MM-dd");
        }
        else
            todate = "";

        GridView1.DataSource = thehlpr.DiscPatientList(txtname.Text.Trim(), frmdate, todate, Session["CoCode"].ToString().Trim(),Session["YearCode"].ToString().Trim());
        GridView1.DataBind();
    }


    protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            Label lblregno = (Label)e.Row.FindControl("lblregno");
            Label lblSelno = (Label)e.Row.FindControl("lblSlno");
            lblSelno.Text = (e.Row.RowIndex + 1).ToString();
            Label lbldiagot = (Label)e.Row.FindControl("lbldiagot");
            DataTable ot = thehlpr.getOtdeta(lblregno.Text);
            if (ot != null && ot.Rows.Count > 0)
            {
                lbldiagot.Text=ot.Rows[0]["operationname"].ToString();
            }
            else
            {
                lbldiagot.Text="";
            }
        }
        
    }

    protected void Button1_Click(object sender, EventArgs e)
    {
        GridFill();
    }
    protected void GridView1_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        GridView1.PageIndex = e.NewPageIndex;
        GridFill();
    }
}