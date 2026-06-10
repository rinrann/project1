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

public partial class Pathology_ExistTestPopup : System.Web.UI.Page
{
    PH_TestPopupMultiple thedia = new PH_TestPopupMultiple(ConfigurationSettings.AppSettings["ConnectionString"].ToString());
    PH_PatientRequisition thereq = new PH_PatientRequisition(ConfigurationSettings.AppSettings["ConnectionString"].ToString());
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            string reqno = Session["TestReqNo"].ToString();
            lblptname.Text = Session["TestPtName"].ToString();
            string test = "";
            DataTable dt = thedia.GetExistTestDetails(Session["CoCode"].ToString().Trim(), Session["YearCode"].ToString().Trim(), reqno, test);
            GridView1.DataSource = dt;
            GridView1.DataBind();
        }
    }
    protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            Label lblSerial = (Label)e.Row.FindControl("lblSerial");
            lblSerial.Text = ((GridView1.PageIndex * GridView1.PageSize) + e.Row.RowIndex + 1).ToString();
            Label lbconsultant = (Label)e.Row.FindControl("lbconsultant");
            DropDownList ddlExistconsult = (DropDownList)e.Row.FindControl("ddlExistconsult");
            DataTable dt = thedia.GetconsultantDoc(Session["CoCode"].ToString().Trim(), "");
            ddlExistconsult.DataSource = dt;
            ddlExistconsult.DataValueField = "consullt";
            ddlExistconsult.DataTextField = "docname";
            ddlExistconsult.DataBind();

            ddlExistconsult.Items.Insert(0, new ListItem("None", ""));
            ddlExistconsult.Items.Add(new ListItem("SRL Lab", "SRL Lab"));
            ddlExistconsult.Items.Add(new ListItem("Lilac Lab", "Lilac Lab"));
            ddlExistconsult.SelectedValue = lbconsultant.Text.Trim();
        }
    }

    protected void btn_update_Click(object sender, EventArgs e)
    {
        for (int i = 0; i < GridView1.Rows.Count; i++)
        {
            Label lblid = (Label)GridView1.Rows[i].FindControl("lblid");
            Label lblTestReqNo = (Label)GridView1.Rows[i].FindControl("lblTestReqNo");
            TextBox txtRemarks = (TextBox)GridView1.Rows[i].FindControl("txtRemarks");
            DropDownList ddlExistconsult = (DropDownList)GridView1.Rows[i].FindControl("ddlExistconsult");

            thereq.UpdateRequisitionTestMap(Session["CoCode"].ToString().Trim(),Session["TestReqNo"].ToString(), lblTestReqNo.Text.Trim(), lblid.Text.Trim(), txtRemarks.Text.Trim(), ddlExistconsult.SelectedValue.Trim());
        }
        ScriptManager.RegisterStartupScript(this, this.GetType(), "msg", "alert('Record Successfully Updated!');", true);
    }
}