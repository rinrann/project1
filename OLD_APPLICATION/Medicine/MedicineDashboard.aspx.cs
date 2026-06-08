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

public partial class Medicine_MedicineDashboard : System.Web.UI.Page
{
    Main Objmain = new Main(ConfigurationSettings.AppSettings["ConnectionString"].ToString());
    MedicineDashboard thehlpr = new MedicineDashboard(ConfigurationSettings.AppSettings["ConnectionString"].ToString());
    //System.Text.StringBuilder rpt = new System.Text.StringBuilder();
    String Date = DateTime.Now.ToString("dd/MM/yyyy");

    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["userName"] == null)
        {
            Response.Redirect("../LoginPage.aspx");
        }
        if (Objmain.checkaccss(Session["CoCode"].ToString(), Session["UserRoleID"].ToString().Trim(), "MEDICINE DASHBOARD", checkAccessType.ViewAction) == false)
        {
            Response.Redirect("../AccessDenied.aspx");
        }
        if (!IsPostBack)
        {
            DropDownFill();
            
            GridFill();
        }
        Page.Title = "Medicine DashBoard";
        Session["RegNo"] = null;
    }

    private void DropDownFill()
    {
        //ddlMfg.Items.Clear();
        //ddlMfg.DataSource = thehlpr.getMfg(Session["CoCode"].ToString().Trim());
        //ddlMfg.DataTextField = "MName";
        //ddlMfg.DataValueField = "MCode";
        //ddlMfg.DataBind();
        //ddlMfg.Items.Insert(0, new ListItem("--Select--", "0"));

        //ddlMediGrp.Items.Clear();
        //ddlMediGrp.DataSource = thehlpr.getMedGrp(Session["CoCode"].ToString().Trim());
        //ddlMediGrp.DataTextField = "MedicineGroupName";
        //ddlMediGrp.DataValueField = "MedicineGroupID";
        //ddlMediGrp.DataBind();
        //ddlMediGrp.Items.Insert(0, new ListItem("--Select--", "0"));

        //ddlMediSubGrp.Items.Clear();
        //ddlMediSubGrp.Items.Insert(0, new ListItem("--Select--", "0"));

        //ddlMedi.Items.Clear();
        //ddlMedi.Items.Insert(0, new ListItem("--Select--", "0"));
    }

    private void GridFill()
    {
        string frmdate = "";
        string todate = "";
        //if (frmdt.Text != "")
        //{
        //    string[] aa = frmdt.Text.Split('/');
        //    string fday = aa[0];
        //    string fmonth = aa[1];
        //    string fyear = aa[2];
        //    if (fday.Length == 1)
        //        fday = "0" + fday;
        //    if (fmonth.Length == 1)
        //        fmonth = "0" + fmonth;
        //    // frmdate = fday + "/" + fmonth + "/" + fyear;
        //    frmdate = fyear + fmonth + fday;
        //}
        //else
        //    frmdate = "";

        //if (todt.Text != "")
        //{
        //    string[] aa = todt.Text.Split('/');
        //    string tday = aa[0];
        //    string tmonth = aa[1];
        //    string tyear = aa[2];
        //    if (tday.Length == 1)
        //        tday = "0" + tday;
        //    if (tmonth.Length == 1)
        //        tmonth = "0" + tmonth;
        //    //todate = tday + "/" + tmonth + "/" + tyear;
        //    todate = tyear + tmonth + tday;
        //}
        //else
        //    todate = "";
        //ddlMedi.SelectedValue.ToString()
        GridView1.DataSource = thehlpr.MedicineDetails(Session["CoCode"].ToString().Trim(), Session["YearCode"].ToString().Trim(),"");
        GridView1.DataBind();
    }

    //protected void ddlMediGrp_SelectedIndexChanged(object sender, EventArgs e)
    //{
    //    if (ddlMediGrp.SelectedValue.ToString() == "0")
    //    {
    //        ddlMediSubGrp.Items.Clear();
    //        ddlMediGrp.Items.Insert(0, new ListItem("--Select--", "0"));

            
    //    }
    //    else
    //    {
    //        ddlMediSubGrp.Items.Clear();
    //        ddlMediSubGrp.DataSource = thehlpr.DropdownSubGroup(Session["CoCode"].ToString().Trim(), ddlMediGrp.SelectedValue.ToString());
    //        ddlMediSubGrp.DataTextField = "SubGrName";
    //        ddlMediSubGrp.DataValueField = "ID";
    //        ddlMediSubGrp.DataBind();
    //        ddlMediSubGrp.Items.Insert(0, new ListItem("--Select--", "0"));

            
    //    }

    //    ddlMedi.Items.Clear();
    //    ddlMedi.Items.Insert(0, new ListItem("--Select--", "0"));

    //    GridFill();
    //}

    //protected void ddlMediSubGrp_SelectedIndexChanged(object sender, EventArgs e)
    //{
    //    if (ddlMediSubGrp.SelectedValue.ToString() == "0")
    //    {
    //        ddlMedi.SelectedIndex = 0;
    //        ddlMedi.Enabled = false;
    //    }
    //    else
    //    {
    //        ddlMedi.Items.Clear();
    //        ddlMedi.DataSource = thehlpr.DropdownMedicine(Session["CoCode"].ToString().Trim(), ddlMediSubGrp.SelectedValue.ToString());
    //        ddlMedi.DataTextField = "MedicineName";
    //        ddlMedi.DataValueField = "MedicineID";
    //        ddlMedi.DataBind();
    //        ddlMedi.Items.Insert(0, new ListItem("--Select--", "0"));
    //        ddlMedi.Enabled = true;
    //    }

    //    GridFill();
    //}

    protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            Label lblmedid = (Label)e.Row.FindControl("lblmedid");
            Label lblSelno = (Label)e.Row.FindControl("lblSlno");
            lblSelno.Text = (e.Row.RowIndex + 1).ToString();
            
            //Label lblsuplrno = (Label)e.Row.FindControl("lblsuplrno");
            //Label lblsup = (Label)e.Row.FindControl("lblsup");
            //if (lblsuplrno.Text != "")
            //{
            //    DataTable suplr = thehlpr.getSuplrinfo(Session["CoCode"].ToString().Trim(), lblsuplrno.Text);
            //    lblsup.Text = suplr.Rows[0]["SName"].ToString();
            //}
            //else
            //{
            //    lblsup.Text = "";
            //}
            //lblsup.Text = "";

        }

    }

    protected void ddlMed_SelectedIndexChanged(object sender,EventArgs e)
    {
        GridFill();
    }

    protected void ddlMfg_SelectedIndexChanged(object sender, EventArgs e)
    {
        GridFill();
    }
    protected void frmdt_textChange(object sender, EventArgs e)
    {
        GridFill();
    }

    protected void todt_textChange(object sender, EventArgs e)
    {
        GridFill();
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
            string medid = (e.CommandArgument).ToString();
            Session.Add("Pur_medid", medid);
            Response.Redirect("../Medicine/PurchaseMedicine.aspx");
        }
    }
}