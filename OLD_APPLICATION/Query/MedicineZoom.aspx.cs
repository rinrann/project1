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
using System.Collections.Generic;

public partial class Query_MedicineZoom : System.Web.UI.Page
{
    Main Objmain = new Main(ConfigurationSettings.AppSettings["ConnectionString"].ToString());
    Zoom theQry = new Zoom(ConfigurationSettings.AppSettings["ConnectionString"].ToString());
    protected void Page_Load(object sender, EventArgs e)
    {
        Page.Title = "Medicine Zoom";
        if (Session["userName"] == null)
        {
            Response.Redirect("../LoginPage.aspx");
        }
        if (Objmain.checkaccss(Session["CoCode"].ToString(), Session["UserRoleID"].ToString().Trim(), "MEDICINE ZOOM", checkAccessType.ViewAction) == false)
        {
            Response.Redirect("../AccessDenied.aspx");
        }
        if (!IsPostBack)
        {
            GridFill();
        }
    }
    public void GridFill()
    {
        DataTable dt = theQry.GetAllMedicine(Session["CoCode"].ToString().Trim(), Session["YearCode"].ToString().Trim(), TextBoxicode.Text.Trim());
        GridView2.DataSource = dt;
        GridView2.DataBind();
    }
    protected void GridView2_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "Select")
        {
            int i = Convert.ToInt32(e.CommandArgument);
            Label lblcd = (Label)GridView2.Rows[i].FindControl("lblcd");
            Session["zm_icode"] = lblcd.Text.ToString().Trim();
            Response.Redirect("../Query/BatchwiseZoom.aspx");
        }
    }
    protected void butBack_Click(object sender, EventArgs e)
    {
        Response.Redirect("../HomePage.aspx");
    }

    protected void GridView2_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        GridView2.PageIndex = e.NewPageIndex;
        GridFill();
    }

    [System.Web.Script.Services.ScriptMethod()]
    [System.Web.Services.WebMethod]
    public static List<string> SearchMedicineName(string prefixText, int count)
    {
        using (SqlConnection conn = new SqlConnection())
        {
            conn.ConnectionString = ConfigurationSettings.AppSettings["ConnectionString"].ToString();
            using (SqlCommand cmd = new SqlCommand())
            {
                cmd.CommandText = "select distinct it.iname + '~' + ltrim(rtrim(it.icode)) as Name from ITEMMAST it,IPD_MedicineMaster im where im.icode=it.icode and it.itype='M' and it.iname like @SearchText +'%'";
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

    protected void medicinechange(object sender, System.EventArgs e)
    {
        GridFill();
        
    }
}