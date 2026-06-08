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
 
public partial class Pathology_PatientSearchPopup : System.Web.UI.Page
{
    PH_PatientSearchPopup thepopup = new PH_PatientSearchPopup(ConfigurationSettings.AppSettings["ConnectionString"].ToString());
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["userName"] == null)
        {
            Response.Redirect("../LoginPage.aspx");
        }
        Page.Title = "Customer Registration Popup";
        GridFill();
    }

    private void ResetAllFields()
    {
        txtname.Text = "";
        txtreg.Text = "";
        txtname.Focus();
        txtaddress.Text = "";
        txtphone.Text = "";
    }
    private void GridFill()
    {
         string reformattedDate;


        if (txtname.Text != "")
        {
            reformattedDate = DateTime.ParseExact(txtname.Text, "dd/MM/yyyy", null).ToString("MM/dd/yyyy");
        }
        else
        {
            reformattedDate = "null";
        }
        GridView_popup.DataSource = thepopup.GridPopup(txtreg.Text, reformattedDate, txtphone.Text, txtaddress.Text);
        GridView_popup.DataBind();
    }
    protected void GridView_popup_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        Label lblregno = (Label)GridView_popup.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("lblregno");
        HiddenField1.Value = lblregno.Text;

         GridView_popup.Rows[Convert.ToInt32(e.CommandArgument)].BackColor = System.Drawing.Color.Yellow;
        
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
}