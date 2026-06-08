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

public partial class OPD_USGPopup : System.Web.UI.Page
{
    PatientsDetails theopdpatient = new PatientsDetails(ConfigurationSettings.AppSettings["ConnectionString"].ToString());

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            DropDownFill();
            HiddenField1.Value = Request.QueryString["Regno"].ToString();
            Fill();
        }
    }

   
    public void Fill()
    {
        DataTable dtusgresult = theopdpatient.GetUSGResultCode(Session["CoCode"].ToString().Trim(), HiddenField1.Value);
        if (dtusgresult.Rows.Count > 0)
        {
            DataTable dt = theopdpatient.GetUSGheaderResultCode(Session["CoCode"].ToString().Trim(), HiddenField1.Value, dtusgresult.Rows[0]["USGNameId"].ToString());
                Gridview11.DataSource = dt;
                Gridview11.DataBind();
                Gridview12.DataSource = theopdpatient.GetRadioResultParameter(Session["CoCode"].ToString().Trim(), HiddenField1.Value, dtusgresult.Rows[0]["USGNameId"].ToString());
                Gridview12.DataBind();

            
        }
    }
    public void DropDownFill()
    {
        DropDownList53.DataSource = theopdpatient.DropdownUSGGr(Session["CoCode"].ToString().Trim());
        DropDownList53.DataTextField = "GroupName";
        DropDownList53.DataValueField = "GroupCode";
        DropDownList53.DataBind();
        DropDownList53.Items.Insert(0, new ListItem("--Select--", "0"));

        DropDownList54.Items.Insert(0, new ListItem("--Select--", "0"));
        DropDownList55.Items.Insert(0, new ListItem("--Select--", "0"));
    }
    protected void Gridview11_SelectedIndexChanged(object sender, EventArgs e)
    {

    }
    protected void DropDownList53_SelectedIndexChanged(object sender, EventArgs e)
    {
        DropDownList54.DataSource = theopdpatient.DropdownUSGSubgr(Session["CoCode"].ToString().Trim(), DropDownList53.SelectedValue);
        DropDownList54.DataTextField = "SubGrName";
        DropDownList54.DataValueField = "SubGrID";
        DropDownList54.DataBind();
        DropDownList54.Items.Insert(0, new ListItem("--Select--", "0"));
    }
    protected void DropDownList54_SelectedIndexChanged(object sender, EventArgs e)
    {
        DropDownList55.DataSource = theopdpatient.dropdownusgname(Session["CoCode"].ToString().Trim(), DropDownList53.SelectedValue, DropDownList54.SelectedValue);
        DropDownList55.DataTextField = "Name";
        DropDownList55.DataValueField = "ID";
        DropDownList55.DataBind();
        DropDownList55.Items.Insert(0, new ListItem("--Select--", "0"));
    }
    protected void DropDownList55_SelectedIndexChanged(object sender, EventArgs e)
    {
        Gridview11.DataSource = theopdpatient.GetRadiology(Session["CoCode"].ToString().Trim(), DropDownList55.SelectedValue);
        Gridview11.DataBind();


        Gridview12.DataSource = theopdpatient.GetRadiologyParameter(Session["CoCode"].ToString().Trim(), DropDownList55.SelectedValue);
        Gridview12.DataBind();
    }
}