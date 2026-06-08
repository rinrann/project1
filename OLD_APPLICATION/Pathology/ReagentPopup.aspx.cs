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
using System.Globalization;
 
public partial class Pathology_ReagentPopup : System.Web.UI.Page
{
    PH_ReagentPopup thereagentpopup = new PH_ReagentPopup(ConfigurationSettings.AppSettings["ConnectionString"].ToString());
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["userId"] == null)
        {
            Response.Redirect("../LoginPage.aspx");
        }
        GridFill();
    }
    public void GridFill()
    {
        string frmdt;
        string todt;
        System.Globalization.DateTimeFormatInfo dtf = new DateTimeFormatInfo();
        if (TextBox5.Text != "")
        {
            frmdt = DateTime.ParseExact(TextBox5.Text, "dd/MM/yyyy", dtf).ToString("yyyy-MM-dd");
        }
        else
        {
            frmdt = "";
        }
        if (TextBox6.Text != "")
        {
            todt = DateTime.ParseExact(TextBox6.Text, "dd/MM/yyyy", dtf).ToString("yyyy-MM-dd");
        }
        else
        {
            todt = "";
        }

        GridView_popup.DataSource = thereagentpopup.supplier(Session["CoCode"].ToString().Trim(), Session["YearCode"].ToString().Trim(), txtname.Text.Trim(), txtcompany.Text.Trim(),frmdt,todt);
        GridView_popup.DataBind();


    }
    protected void GridView_popup_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        Label lblid = (Label)GridView_popup.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("lblid");
        HiddenField1.Value = lblid.Text;
        Label lbldate = (Label)GridView_popup.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("lbldate");
        HiddenField2.Value = lbldate.Text;

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

    [System.Web.Script.Services.ScriptMethod()]
    [System.Web.Services.WebMethod]
    public static List<string> SearchSupplierName(string prefixText, int count)
    {
        using (SqlConnection conn = new SqlConnection())
        {
            conn.ConnectionString = ConfigurationSettings.AppSettings["ConnectionString"].ToString();
            using (SqlCommand cmd = new SqlCommand())
            {
                //+ compcode='"  "' and  Session["CoCode"].ToString().Trim() +  
                cmd.CommandText = "select distinct slname + '-' + slcode as Name from slmast where category='S' and slname like @SearchText + '%'";
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

    [System.Web.Script.Services.ScriptMethod()]
    [System.Web.Services.WebMethod]
    public static List<string> SearchReagentName(string prefixText, int count)
    {
        using (SqlConnection conn = new SqlConnection())
        {
            conn.ConnectionString = ConfigurationSettings.AppSettings["ConnectionString"].ToString();
            using (SqlCommand cmd = new SqlCommand())
            {
                cmd.CommandText = "select distinct iname + '~' + icode as Name from ITEMMAST where itype='G' and iname like @SearchText + '%'";
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
}