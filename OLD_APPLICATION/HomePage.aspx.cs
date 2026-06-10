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

public partial class HomePage : System.Web.UI.Page
{
    login theHelper = new login(ConfigurationSettings.AppSettings["ConnectionString"].ToString());
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            Page.Title = "Home";
            if (Session["userName"] == null)
            {
                Response.Redirect("LoginPage.aspx");
            }
            DataTable dt = theHelper.CompanyInfo(Session["CoCode"].ToString().Trim(), Session["YearCode"].ToString().Trim());
            Session.Add("STARTDT", dt.Rows[0]["STARTDT"].ToString());
            Session.Add("ENDDT", dt.Rows[0]["ENDDT"].ToString());
            Session.Add("ADDR", dt.Rows[0]["ADDR"].ToString());
            Session.Add("PHONE", dt.Rows[0]["PHONE"].ToString());
            Session.Add("FAX", dt.Rows[0]["FAX"].ToString());
            Session.Add("EMAIL", dt.Rows[0]["EMAIL"].ToString());
            Session.Add("WEB", dt.Rows[0]["WEB"].ToString());
            Session.Add("Logopath", dt.Rows[0]["Logopath"].ToString());
            Session.Add("Logopath2", dt.Rows[0]["Logopath2"].ToString());
            Session.Add("PHONE", dt.Rows[0]["PHONE"].ToString());
            Session.Add("HosRegnNo", dt.Rows[0]["HosRegnNo"].ToString());
            Session["docId"] = "";
            Session["patientLedger"] = "";
            Session["back_to_purchase"] = "";
            Session["zm_docno"] = "";
            Session["zm_icode"] = "";
            Session["zm_itemBatch"] = "";
        }
        catch (Exception ex)
        {
            Label1.Text = ex.Message;
        }
    }
}
