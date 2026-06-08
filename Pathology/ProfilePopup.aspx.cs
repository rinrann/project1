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

 
public partial class Pathology_ProfilePopup : System.Web.UI.Page
{
    Main Objmain = new Main(ConfigurationSettings.AppSettings["ConnectionString"].ToString());
    PH_ProfilePopup thedia = new PH_ProfilePopup(ConfigurationSettings.AppSettings["ConnectionString"].ToString());
    PH_ProfileMaster theprofile = new PH_ProfileMaster(ConfigurationSettings.AppSettings["ConnectionString"].ToString());
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Objmain.checkaccss(Session["CoCode"].ToString(), Session["UserRoleID"].ToString().Trim(), "TEST GROUP MASTER", checkAccessType.DeleteAction) == false)
        {
            GridView_popup.Columns[4].Visible = false;
        }
        if (Session["userId"] == null)
        {
            Response.Redirect("../LoginPage.aspx");
        }
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
          GridView_popup.DataSource = thedia.GridFill(txtname.Text);
        GridView_popup.DataBind();

    }
    protected void GridView_popup_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        Label lblregno = (Label)GridView_popup.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("lblregno");
        HiddenField1.Value = lblregno.Text;
      
    }

    protected void GridView_popup_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        Label lblregno = (Label)GridView_popup.Rows[e.RowIndex].FindControl("lblregno");
        theprofile.DeleteMapping(HiddenField1.Value, Session["CoCode"].ToString());
        thedia.Deletetestgrp(lblregno.Text, Session["CoCode"].ToString().Trim());
        ScriptManager.RegisterStartupScript(this, this.GetType(), "msg", "alert('Deleted Successfully !');", true);
        GridFill();

    }
    protected void GridView_popup_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            if (Objmain.checkaccss(Session["CoCode"].ToString(), Session["UserRoleID"].ToString().Trim(), "TEST GROUP MASTER", checkAccessType.DeleteAction) == false)
            {
                
                e.Row.Cells[4].Visible = false;
            }
        }
    }
}