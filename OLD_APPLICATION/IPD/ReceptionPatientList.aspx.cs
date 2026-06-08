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
using System.Globalization;

public partial class IPD_ReceptionPatientList : System.Web.UI.Page
{
    Main Objmain = new Main(ConfigurationSettings.AppSettings["ConnectionString"].ToString());
    PatientList thereg = new PatientList(ConfigurationSettings.AppSettings["ConnectionString"].ToString());
    protected void Page_Load(object sender, EventArgs e)
    {
        Page.Title = "Patient Summary";
        //try
        //{
            if (Session["userName"] == null)
            {
                Response.Redirect("../LoginPage.aspx");
            }
            if (Objmain.checkaccss(Session["CoCode"].ToString(), Session["UserRoleID"].ToString().Trim(), "PATIENT SUMMARY", checkAccessType.ViewAction) == false)
            {
                Response.Redirect("../AccessDenied.aspx");
            }
          if(!IsPostBack)
              GridFill2();
        //}
        //catch (Exception ex)
        //{
        //    ScriptManager.RegisterStartupScript(this, this.GetType(), "msg", "alert('" + ex.Message + "');", true);
        //}
    }

    public void GridFill2()
    {
        DataSet ds = thereg.GridPopupDetails1(DateTime.Now.ToString("yyyy-MM-dd"),Session["CoCode"].ToString().Trim());
        GridView2.DataSource = ds.Tables[0];
        GridView2.DataBind();

        lbltotalpatient.Text ="Total No of Patient :"+ ds.Tables[1].Rows[0][0].ToString();
        lbltotalpatient.ForeColor = Color.Blue;
    }
    protected void GridView2_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        GridView2.PageIndex = e.NewPageIndex;
        GridFill2();
    }
    protected void GridView2_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "Select")
        {
            int index = Convert.ToInt32(e.CommandArgument);

            Label lblReg = (Label)GridView2.Rows[index].FindControl("lblReg");
            Session["RegnNo"] = lblReg.Text;
            Response.Redirect("../IPD/Discharge.aspx");
        }
    }

    protected void GridView2_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {    
                e.Row.Cells[11].ForeColor = System.Drawing.Color.Red;
                e.Row.Cells[11].Font.Bold = true;

                Label Due = (Label)e.Row.FindControl("Due"); 
                if (Due.Text != "0" && Due.Text != "0.00")
                {
                    e.Row.Cells[12].Enabled = false;
                }
                else
                {
                    e.Row.Cells[12].Enabled = true;
                }
        } 
    }
}