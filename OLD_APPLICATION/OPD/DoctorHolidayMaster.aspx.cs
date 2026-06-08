using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Data.SqlClient;
using System.Drawing;
using System.Globalization;

public partial class OPD_DoctorHolidayMaster : System.Web.UI.Page
{
    Main Objmain = new Main(ConfigurationSettings.AppSettings["ConnectionString"].ToString());
    PatientDoctorVisit thedocvisit = new PatientDoctorVisit(ConfigurationSettings.AppSettings["ConnectionString"].ToString());
    DoctorAvailablity thedocHoliday = new DoctorAvailablity(ConfigurationSettings.AppSettings["ConnectionString"].ToString());
    protected void Page_Load(object sender, EventArgs e)
    {
        Page.Title = "Doctors On Leave";
        if (Session["userId"] == null)
        {
            Response.Redirect("../LoginPage.aspx");
        }
        if (Objmain.checkaccss(Session["CoCode"].ToString(), Session["UserRoleID"].ToString().Trim(), "DOCTOR HOLIDAY MASTER", checkAccessType.ViewAction) == false)
        {
            Response.Redirect("../AccessDenied.aspx");
        }

        if (Objmain.checkaccss(Session["CoCode"].ToString(), Session["UserRoleID"].ToString().Trim(), "DOCTOR HOLIDAY MASTER", checkAccessType.InsertAction) == false)
        {
            Button1.Enabled = false;
        }

        if (!IsPostBack)
        {
            DropDownFill();
            txtfrmdate.Text = DateTime.Now.ToString("dd/MM/yyyy");
            txttodt.Text = DateTime.Now.ToString("dd/MM/yyyy");
        }
    }
    private void DropDownFill()
    {
        DropDownList2.Items.Clear();
        this.DropDownList2.DataSource = thedocvisit.DropdownDoctorType(Session["CoCode"].ToString().Trim());
        this.DropDownList2.DataTextField = "TypeName";
        this.DropDownList2.DataValueField = "DocTypeId";
        this.DropDownList2.DataBind();
        this.DropDownList2.Items.Insert(0, new ListItem("--Select--", "0"));
    }
    protected void DropDownList2_SelectedIndexChanged(object sender, EventArgs e)
    {
        DoctorFill(DropDownList2.SelectedValue);
    }
    public void DoctorFill(string value)
    {
        DropDownList1.Items.Clear();
        this.DropDownList1.DataSource = thedocvisit.DropdownDoctor(Session["CoCode"].ToString().Trim(), value);
        this.DropDownList1.DataTextField = "doc_name";
        this.DropDownList1.DataValueField = "doc_id";
        this.DropDownList1.DataBind();
        this.DropDownList1.Items.Insert(0, new ListItem("--Select--", "0"));
    }
    protected void DropDownList1_SelectedIndexChanged(object sender, EventArgs e)
    {
        GridFill();
    }
    protected void GridFill()
    {
        GridView1.DataSource = thedocHoliday.GridHoliday(Session["CoCode"].ToString().Trim(), DropDownList1.SelectedValue.Trim());
        GridView1.DataBind();
    }

    protected void GridView1_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        GridView1.PageIndex = e.NewPageIndex;
        GridFill();
    }
    protected void Button1_Click(object sender, EventArgs e)
    {
        System.Globalization.DateTimeFormatInfo dtf = new DateTimeFormatInfo();
        DateTime testfrmdate = DateTime.ParseExact(txtfrmdate.Text, "dd/MM/yyyy", dtf);
        DateTime testtodate = DateTime.ParseExact(txttodt.Text, "dd/MM/yyyy", dtf);
        int mode = 0;
        if (Button1.Text == "Submit")
        {
            mode = 1;
        }
        else
        {
            mode = 2;
        }
        if (thedocHoliday.InsUpdHoliday(Session["CoCode"].ToString().Trim(), Session["YearCode"].ToString().Trim(), mode, HiddenField1.Value, DropDownList1.SelectedValue, DropDownList2.SelectedValue, testfrmdate.ToString("yyyy-MM-dd"), testtodate.ToString("yyyy-MM-dd"), txtReason.Text, Session["userId"].ToString().Trim()) == true)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "msg", "alert('Saved  Successfully !');", true);
            // Response.Redirect("../IPD/AdmissionPatientList.aspx");
        }
        else
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "msg", "alert('Error in Saving !');", true);
        }
        GridFill();
        Button1.Enabled = false;
        ResetAllFields();
    }
    protected void Button2_Click(object sender, EventArgs e)
    {
        ResetAllFields();
    }
    private void ResetAllFields()
    {
        DropDownList1.SelectedIndex = 0; DropDownList2.SelectedIndex = 0; HiddenField1.Value = "0";
        txtfrmdate.Text = DateTime.Now.ToString("dd/MM/yyyy");
        txttodt.Text = DateTime.Now.ToString("dd/MM/yyyy");
        txtReason.Text = "";

        Button1.Text = "Submit";

        if (Objmain.checkaccss(Session["CoCode"].ToString(), Session["UserRoleID"].ToString().Trim(), "DOCTOR HOLIDAY MASTER", checkAccessType.InsertAction) == false)
        {
            Button1.Enabled = false;
        }

    }
    protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "Select")
        {
            ResetAllFields();
            Button1.Enabled = true;
            int index = Convert.ToInt32(e.CommandArgument);
            Label lblid = (Label)GridView1.Rows[index].FindControl("lblid");
            HiddenField1.Value = lblid.Text;

            DropDownFill();
            Label lbldoctypeid = (Label)GridView1.Rows[index].FindControl("lbldoctypeid");
            DropDownList2.SelectedValue = lbldoctypeid.Text.Trim();

            DoctorFill(DropDownList2.SelectedValue);
            Label lbldocid = (Label)GridView1.Rows[index].FindControl("lbldocid");
            DropDownList1.SelectedValue = lbldocid.Text.Trim();

            Label lblFrmdate = (Label)GridView1.Rows[index].FindControl("lblFrmdate");
            txtfrmdate.Text = lblFrmdate.Text;

            Label lblTodate = (Label)GridView1.Rows[index].FindControl("lblTodate");
            txttodt.Text = lblTodate.Text;


            Label lblremarks = (Label)GridView1.Rows[index].FindControl("lblremarks");
            txtReason.Text = lblremarks.Text;
            Label visittype = (Label)GridView1.Rows[index].FindControl("lblvisittype");
            
            Button1.Text = "Update";

            if (Objmain.checkaccss(Session["CoCode"].ToString(), Session["UserRoleID"].ToString().Trim(), "DOCTOR HOLIDAY MASTER", checkAccessType.UpdateAction) == false)
            {
                Button1.Enabled = false;
            }
            else
            {
                Button1.Enabled = true;
            }
        }
    }

}