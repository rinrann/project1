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

public partial class IPD_RecallPatient : System.Web.UI.Page
{
    Main Objmain = new Main(ConfigurationSettings.AppSettings["ConnectionString"].ToString());
    RecallPatientClass therecallPatient = new RecallPatientClass(ConfigurationSettings.AppSettings["ConnectionString"].ToString());
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["userName"] == null)
        {
            Response.Redirect("../LoginPage.aspx");
        }
        if (Objmain.checkaccss(Session["CoCode"].ToString(), Session["UserRoleID"].ToString().Trim(), "PATIENT RECALL", checkAccessType.ViewAction) == false)
        {
            Response.Redirect("../AccessDenied.aspx");
        }
        if (Objmain.checkaccss(Session["CoCode"].ToString(), Session["UserRoleID"].ToString().Trim(), "PATIENT RECALL", checkAccessType.DeleteAction) == false)
        {
            GridView1.Columns[6].Visible = false;
        }
    }
    protected void Button4_Click(object sender, EventArgs e)
    {
        GridFillAccordingPatient();
    }

    public void GridFillAccordingPatient()
    {
        GridView1.DataSource = therecallPatient.PatientDetails(txtreg.Text, Session["CoCode"].ToString().Trim());
        GridView1.DataBind();
    }
    protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "Select")
        {
            int index = Convert.ToInt32(e.CommandArgument);
            Label lblLedgerId = (Label)GridView1.Rows[index].FindControl("lblLedgerId");
            Label lblPatientReg = (Label)GridView1.Rows[index].FindControl("lblPatientReg");
            if (therecallPatient.RecallPatient(lblPatientReg.Text,lblLedgerId.Text) == true)
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "msg", "Message();", true);
                //ScriptManager.RegisterStartupScript(this, this.GetType(), "msg", "alert('Please do one more click in call to confirm')", true);
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "msg", "alert('Error in Recalled Patient !');", true);
            }
        }
    }
    protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            if (Objmain.checkaccss(Session["CoCode"].ToString(), Session["UserRoleID"].ToString().Trim(), "PATIENT RECALL", checkAccessType.DeleteAction) == false)
            {
                e.Row.Cells[6].Visible = false;
            }
        }
    }
}