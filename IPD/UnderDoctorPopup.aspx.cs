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
 

public partial class IPD_UnderDoctorPopup : System.Web.UI.Page
{
    UnderDoctorPopup thereg = new UnderDoctorPopup(ConfigurationSettings.AppSettings["ConnectionString"].ToString());
    PatientAdmission thepd = new PatientAdmission(ConfigurationManager.AppSettings["ConnectionString"].ToString());
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["userName"] == null)
        {
            Response.Redirect("../LoginPage.aspx");
        }
        GridFill();
        if (!IsPostBack)
        {
            DropDownFill();
            Tab1Func();
        }
    
    }
    public void Tab1Func()
    {
        Tab1.CssClass = "Clicked";
        Tab2.CssClass = "Initial";
        MainView.ActiveViewIndex = 0;
    }
    protected void Tab1_Click(object sender, EventArgs e)
    {
        Tab1Func();
    }
    protected void Tab2_Click(object sender, EventArgs e)
    {
        Tab1.CssClass = "Initial";
        Tab2.CssClass = "Clicked";
        MainView.ActiveViewIndex = 1;
    }
    public void DropDownFill()
    {
        this.DropDownList1.DataSource = thereg.DropdownDocType(Session["CoCode"].ToString().Trim());
        this.DropDownList1.DataTextField = "TypeName";
        this.DropDownList1.DataValueField = "DocTypeId";
        this.DropDownList1.DataBind();
        this.DropDownList1.Items.Insert(0, new ListItem("--All--", "0"));

        this.ddlDoctorType.Items.Clear();
        this.ddlDoctorType.DataSource = thereg.DropdownDocType(Session["CoCode"].ToString().Trim());
        this.ddlDoctorType.DataTextField = "TypeName";
        this.ddlDoctorType.DataValueField = "DocTypeId";
        this.ddlDoctorType.DataBind();
        this.ddlDoctorType.Items.Insert(0, new ListItem("--Select--", "0"));

        this.ddlDistrict.Items.Clear();
        this.ddlDistrict.DataSource = thepd.DropDownDistrict(Session["CoCode"].ToString().Trim());
        this.ddlDistrict.DataTextField = "DistrictName";
        this.ddlDistrict.DataValueField = "ID";
        this.ddlDistrict.DataBind();
        this.ddlDistrict.Items.Insert(0, new ListItem("--Select--", "0"));
    }

    public void GridFill()
    {
         GridView_popup.DataSource = thereg.GridPopup(DropDownList1.SelectedValue,TextBox1.Text);
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
        Label lbldocid = (Label)GridView_popup.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("lbldocid");
        Label lbldocName = (Label)GridView_popup.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("lbldocName");
        HiddenField1.Value = lbldocid.Text + "#" + lbldocName.Text;

    }
    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        string doctorflag = string.Empty;
        string ph1 = string.Empty;
        ph1 = txtPhPrefix1.Text + " " + txtPhNo1.Text;
        string ph2 = string.Empty;
        ph1 = txtPhPrefix2.Text + " " + txtPhNo2.Text;
        doctorflag = thepd.InsertDoctor(ddlDoctorType.SelectedValue, txtDoctorName.Text.Trim().ToUpper(), txtAddress1.Text.Trim().ToUpper(), txtAddress2.Text.Trim().ToUpper(), ddlDistrict.SelectedValue, txtPin.Text, ph1, ph2, Session["userName"].ToString(), DateTime.Now.ToString("yyyy-MM-dd"), Session["CoCode"].ToString().Trim(), Session["YearCode"].ToString().Trim());
        if (doctorflag == "Successfull")
        {
            Label1.ForeColor = System.Drawing.Color.Green;
            Label1.Text = "Inserted Successfully";
           // Reset();
        }
        else
        {
            if (doctorflag == "Duplicate")
            {
                Label1.ForeColor = System.Drawing.Color.Red;
                Label1.Text = "Already Exist !";
            }
            else
            {
                Label1.ForeColor = System.Drawing.Color.Red;
                Label1.Text = "Error in Inserted Data";
            }
        }
    }
    protected void btnClear_Click(object sender, EventArgs e)
    {

    }
}