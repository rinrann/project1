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

public partial class Pathology_TestPopupMapping : System.Web.UI.Page
{
    PH_TestPopup thedia = new PH_TestPopup(ConfigurationSettings.AppSettings["ConnectionString"].ToString());
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            DropDownFill();
            GridFill();
        }
    }
    public void DropDownFill()
    {
        DropDownList1.DataSource = thedia.DropDownFill();
        DropDownList1.DataTextField = "DeptName";
        DropDownList1.DataValueField = "DeptCode";
        DropDownList1.DataBind();
        DropDownList1.Items.Insert(0, new ListItem("--Select--", "0"));

    }
    protected void Button2_Click(object sender, EventArgs e)
    {
        GridFill();
    }
    private void GridFill()
    {
        GridView_popup.DataSource = thedia.GridFill(txtname.Text, txtcode.Text, DropDownList1.SelectedValue);
        GridView_popup.DataBind();

    }


    protected void GridView_popup_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        Label lblregno = (Label)GridView_popup.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("lblregno");
        HiddenField1.Value = lblregno.Text;
        Label lblName = (Label)GridView_popup.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("lblName");
        Label lblcost = (Label)GridView_popup.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("lblcost");
        HiddenField2.Value = lblName.Text + "#" + lblcost.Text;
        GridView_popup.Rows[Convert.ToInt32(e.CommandArgument)].BackColor = System.Drawing.Color.Yellow;

    }
    protected void GridView_popup_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        GridView_popup.PageIndex = e.NewPageIndex;
        GridFill();
    }

    protected void GridView_popup_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        Label lblregno = (Label)GridView_popup.Rows[e.RowIndex].FindControl("lblregno");
        Label lbltype = (Label)GridView_popup.Rows[e.RowIndex].FindControl("lbltype");
        DataTable dt = thedia.gettestrec(lblregno.Text, Session["CoCode"].ToString().Trim());
        DataTable dt1 = thedia.getSugTestRec(lblregno.Text, Session["CoCode"].ToString().Trim());
        DataTable dt2 = thedia.getGrptest(lblregno.Text, Session["CoCode"].ToString().Trim());
        if (dt1.Rows.Count > 0 || dt.Rows.Count > 0 || dt2.Rows.Count > 0)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "msg", "alert('Test cannot be deleted. Currently it is being Used!');", true);
        }
        else
        {
            if (thedia.Deletetest(lblregno.Text, Session["CoCode"].ToString().Trim(), lbltype.Text) == true)
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "msg", "alert('Deleted Successfully !');", true);
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "msg", "alert('Test cannot be deleted. Currently it is being Used!');", true);
            }
        }
        GridFill();

    }
}