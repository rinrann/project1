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

 

public partial class IPD_PrescriptionTempPopup : System.Web.UI.Page
{
    Prescriptemplateopd thereg = new Prescriptemplateopd(ConfigurationSettings.AppSettings["ConnectionString"].ToString());
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
         GridView_popup.DataSource = thereg.GridPopup(TextBox1.Text);
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
        HiddenField1.Value = lblid.Text;
        Label lblName = (Label)GridView_popup.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("lblName");
        HiddenField2.Value = lblName.Text;

    }
}