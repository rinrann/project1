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

public partial class Query_BatchwiseZoom : System.Web.UI.Page
{
    Main Objmain = new Main(ConfigurationSettings.AppSettings["ConnectionString"].ToString());
    Zoom theQry = new Zoom(ConfigurationSettings.AppSettings["ConnectionString"].ToString());
    protected void Page_Load(object sender, EventArgs e)
    {
        Page.Title = "BatchNo Wise Medicine Zoom";
        if (Session["userName"] == null)
        {
            Response.Redirect("../LoginPage.aspx");
        }
        if (Objmain.checkaccss(Session["CoCode"].ToString(), Session["UserRoleID"].ToString().Trim(), "MEDICINE ZOOM", checkAccessType.ViewAction) == false)
        {
            Response.Redirect("../AccessDenied.aspx");
        }
        if (Session["zm_icode"].ToString() == "")
        {
            Response.Redirect("../HomePage.aspx");
        }
        if (!IsPostBack)
        {
            GridFill();
        }
        txtname.Text = theQry.ItemName(Session["CoCode"].ToString().Trim(), Session["zm_icode"].ToString().Trim());
    }
    public void GridFill()
    {
        DataTable dt = theQry.GetAllBatchMedicinewise(Session["CoCode"].ToString().Trim(), Session["YearCode"].ToString().Trim(), Session["zm_icode"].ToString().Trim());
        GridView2.DataSource = dt;
        GridView2.DataBind();
    }
    protected void GridView2_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "Select")
        {
            int i = Convert.ToInt32(e.CommandArgument);
            Label lblbatchno = (Label)GridView2.Rows[i].FindControl("lblbatchno");
            Session["zm_itemBatch"] = lblbatchno.Text.ToString().Trim();
            Response.Redirect("../Query/MedicineTransactionZoom.aspx");
        }
    }
    protected void GridView2_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            int i = Convert.ToInt32(e.Row.RowIndex);
            Label lblbatchno = (Label)e.Row.FindControl("lblbatchno");
            Label lblpdate = (Label)e.Row.FindControl("lblpdate");
            Label lblsup = (Label)e.Row.FindControl("lblsup");
            Label lblBillno = (Label)e.Row.FindControl("lblBillno");
            string batch=lblbatchno.Text.Trim();
            DataTable dt = theQry.getMedicineData(Session["CoCode"].ToString().Trim(), Session["YearCode"].ToString().Trim(), Session["zm_icode"].ToString().Trim(), batch);
            if (dt.Rows.Count > 0)
            {
                lblsup.Text=dt.Rows[0]["SName"].ToString();
                lblBillno.Text = dt.Rows[0]["BillNo"].ToString();
            }
        }
    }

    protected void butBack_Click(object sender, EventArgs e)
    {
        Response.Redirect("MedicineZoom.aspx");
    }
}