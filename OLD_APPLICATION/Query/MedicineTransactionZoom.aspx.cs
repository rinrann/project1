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

public partial class Query_MedicineTransactionZoom : System.Web.UI.Page
{
    Main Objmain = new Main(ConfigurationSettings.AppSettings["ConnectionString"].ToString());
    Zoom theQry = new Zoom(ConfigurationSettings.AppSettings["ConnectionString"].ToString());
    protected void Page_Load(object sender, EventArgs e)
    {
        Page.Title = "BatchNo Wise Transaction";
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
        txtbatch.Text = Session["zm_itemBatch"].ToString().Trim();
    }
    public void GridFill()
    {
        DataTable dt = theQry.GetAllMedicineBrkup(Session["CoCode"].ToString().Trim(), Session["YearCode"].ToString().Trim(), Session["zm_icode"].ToString().Trim(), Session["zm_itemBatch"].ToString().Trim());
        GridView2.DataSource = dt;
        GridView2.DataBind();
    }
    protected void GridView2_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "Select")
        {
            int i = Convert.ToInt32(e.CommandArgument);
            Label lbldocno = (Label)GridView2.Rows[i].FindControl("lbldocno");
            Label lbldoctype = (Label)GridView2.Rows[i].FindControl("lbldoctype");
            if (lbldoctype.Text.Trim() == "P")
            {
                Session["zm_docno"] = theQry.InvoiceNo(Session["CoCode"].ToString().Trim(), Session["YearCode"].ToString().Trim(), lbldocno.Text.ToString().Trim());
                Response.Redirect("../Medicine/PurchaseMedicine.aspx");
            } 
            else if (lbldoctype.Text.Trim() == "I")
            {
                Session["zm_docno"] = lbldocno.Text.Trim();
                Response.Redirect("../Medicine/MedicineSales.aspx");
            }
        }
    }
    protected void butBack_Click(object sender, EventArgs e)
    {
        Response.Redirect("BatchwiseZoom.aspx");
    }
}