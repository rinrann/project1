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
 
public partial class Pathology_TestReagentPopup : System.Web.UI.Page
{
    PH_TestReagentPopup thedia = new PH_TestReagentPopup(ConfigurationSettings.AppSettings["ConnectionString"].ToString());
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
          
            GridFill();
        }
    }
    protected void Button2_Click(object sender, EventArgs e)
    {
        GridFill();
    }
    private void GridFill()
    {
         GridView_popup.DataSource = thedia.GridFill(txtname.Text, txtcode.Text);
        GridView_popup.DataBind();

    }

    protected void GridView_popup_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        GridView_popup.PageIndex = e.NewPageIndex;
        GridFill();
    }
    protected void GridView_popup_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        Label lblregno = (Label)GridView_popup.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("lblregno");
        HiddenField1.Value = lblregno.Text;
        
    }
}