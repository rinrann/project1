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
 

public partial class DayCare_DialysisPopup : System.Web.UI.Page
{
    DC_DialysisPopup thedia = new DC_DialysisPopup(ConfigurationSettings.AppSettings["ConnectionString"].ToString());
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["userName"] == null)
        {
            Response.Redirect("../LoginPage.aspx");
        }
        Page.Title = "Dialysis Popup";
        if (!IsPostBack)
        {
            DropdownFill();
            GridFill();
        }
    }
    protected void Button2_Click(object sender, EventArgs e)
    {
        GridFill();
    }
    private void DropdownFill()
    {
        DropDownList1.DataSource = thedia.DropdownShift();
        DropDownList1.DataTextField = "ShiftName";
        DropDownList1.DataValueField = "ShiftID";
        DropDownList1.DataBind();
        DropDownList1.Items.Insert(0, new ListItem("--Select--", "0"));
    }

    private void GridFill()
    {
         string reformattedDate;
        if (txtdate.Text != "")
        {
            reformattedDate = DateTime.ParseExact(txtdate.Text, "dd/MM/yyyy", null).ToString("MM/dd/yyyy");
        }
        else
        {
            reformattedDate = "null";
        }
        GridView_popup.DataSource = thedia.GridPopup(DropDownList1.SelectedValue, reformattedDate, txtname.Text, txtaddress.Text);
        GridView_popup.DataBind();

    }
    protected void GridView_popup_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        Label lblregno = (Label)GridView_popup.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("lblregno");
        HiddenField1.Value = lblregno.Text;
         }
    protected void GridView_popup_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
         GridView_popup.PageIndex = e.NewPageIndex;
        GridFill();

    }
}