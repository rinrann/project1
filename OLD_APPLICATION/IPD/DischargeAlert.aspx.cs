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

public partial class IPD_DischargeAlert : System.Web.UI.Page
{

    Main Objmain = new Main(ConfigurationSettings.AppSettings["ConnectionString"].ToString());
    PatientList thereg = new PatientList(ConfigurationSettings.AppSettings["ConnectionString"].ToString());
    BillGeneration thepatientbill = new BillGeneration(ConfigurationSettings.AppSettings["ConnectionString"].ToString());
    protected void Page_Load(object sender, EventArgs e)
    {
        Page.Title = "Discharge Dashboard";
        try
        {
            if (Session["userName"] == null)
            {
                Response.Redirect("../LoginPage.aspx");
            }
            if (Objmain.checkaccss(Session["CoCode"].ToString(), Session["UserRoleID"].ToString().Trim(), "DISCHARGE DASHBOARD", checkAccessType.ViewAction) == false)
            {
                Response.Redirect("../AccessDenied.aspx");
            }

        if(!IsPostBack)
            GridFill2();
        }
        catch (Exception ex)
        {
            lblError.ForeColor = System.Drawing.Color.Red;
            lblError.Text = ex.Message;
        }
    }

    public void GridFill2()
    {
        GridView2.DataSource = thereg.GridPopupDischargeAlert(DateTime.Now.ToString("yyyy-MM-dd"), Session["CoCode"].ToString().Trim(), Session["YearCode"].ToString().Trim());
        GridView2.DataBind();
    }
    protected void GridView2_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        GridView2.PageIndex = e.NewPageIndex;
        GridFill2();
    }
    protected void GridView2_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "Select")
        {
            lblError.Text = "";
            int index = Convert.ToInt32(e.CommandArgument);

            Label lblReg = (Label)GridView2.Rows[index].FindControl("lblReg");
            Session["RegnNo"] = lblReg.Text;
            Response.Redirect("../IPD/Discharge.aspx");
        }
        else if (e.CommandName == "payment")
        {
            lblError.Text = "";
            int index = Convert.ToInt32(e.CommandArgument);

            Label lblReg = (Label)GridView2.Rows[index].FindControl("lblReg");
            //Session["RegnNo"] = lblReg.Text;
            Response.Redirect("../Account/AnyTimePayment.aspx?regno=" + lblReg.Text);
        }
    }
    protected void GridView2_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        DateTime testdate = new DateTime();

        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            Label DischargeDate = (Label)e.Row.FindControl("DischargeDate");
            Label lblReg = (Label)e.Row.FindControl("lblReg");
            Label ledgerid = (Label)e.Row.FindControl("lblledgerid");
            Label lblpaid = (Label)e.Row.FindControl("lblpaid");
            Label lblTotalBill = (Label)e.Row.FindControl("lblTotalBill");

            DataSet total = thepatientbill.QuickTotalBillDtls(lblReg.Text.ToString(),ledgerid.Text.ToString(),DateTime.Now.ToString("yyyy-MM-dd"),Session["CoCode"].ToString());
            DataTable dttotal = total.Tables[1];
            string totbill = dttotal.Rows[0]["Total"].ToString();
            lblTotalBill.Text = totbill;
        
            Label Due = (Label)e.Row.FindControl("Due");
            Due.Text = (Convert.ToDecimal(totbill) - Convert.ToDecimal(lblpaid.Text)).ToString();
            if (Due.Text != "0" && Due.Text != "0.00")
            {
                e.Row.Cells[12].Enabled = true;
                e.Row.Cells[13].Enabled = false;
            }
            else
            {
                e.Row.Cells[12].Enabled = false;
                e.Row.Cells[13].Enabled = true;
            }



            System.Globalization.DateTimeFormatInfo dtf = new DateTimeFormatInfo();
            if (DischargeDate.Text != "")
                testdate = DateTime.ParseExact(DischargeDate.Text, "dd/MM/yyyy", dtf);
            int Days = Convert.ToInt32(testdate.Subtract(DateTime.Today).TotalDays);

            if (Days <= 2)
                e.Row.Visible = true;
            else
                e.Row.Visible = false;
        }
    }
    protected void GridView2_SelectedIndexChanged(object sender, EventArgs e)
    {

    }
}