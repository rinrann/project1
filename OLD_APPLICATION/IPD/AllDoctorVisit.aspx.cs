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

public partial class Assignment_AllDoctorVisit : System.Web.UI.Page
{
    Main Objmain = new Main(ConfigurationSettings.AppSettings["ConnectionString"].ToString());
    AllDoctorVisitClass thealldocObject = new AllDoctorVisitClass(ConfigurationSettings.AppSettings["ConnectionString"].ToString());

    int bill;

    protected void Page_Load(object sender, EventArgs e)
    {
        Page.Title = "All Doctor Visit";

        if (Session["userName"] == null)
        {
            Response.Redirect("../LoginPage.aspx");
        }
        if (Objmain.checkaccss(Session["CoCode"].ToString(), Session["UserRoleID"].ToString().Trim(), "ALL DOCTOR VISIT", checkAccessType.ViewAction) == false)
        {
            Response.Redirect("../AccessDenied.aspx");
        }

        if (Objmain.checkaccss(Session["CoCode"].ToString(), Session["UserRoleID"].ToString().Trim(), "ALL DOCTOR VISIT", checkAccessType.InsertAction) == false)
        {
            Button1.Enabled = false;
        }

        if (!IsPostBack)
        {
            GridFill();
            //GridView1.DataSource=thealldocObject.GridShow(Session[""].ToString.trim)
            DropDownFill();
            CheckBox1.Checked = true;
            txtDateOfVisit.Text = DateTime.Now.ToString("dd/MM/yyyy");
            txtTimeofVisit.Text = System.DateTime.Now.ToShortTimeString();
        }

    }
    private void GridFill()
    {
        GridView1.DataSource = thealldocObject.GridShow(Session["CoCode"].ToString().Trim());
        GridView1.DataBind();
    }

    public void DropDownFill()
    {

        this.ddlDoctorType.Items.Clear();
        this.ddlDoctorType.DataSource = thealldocObject.DoctorType(Session["CoCode"].ToString().Trim());
        this.ddlDoctorType.DataTextField = "TypeName";
        this.ddlDoctorType.DataValueField = "DocTypeId";
        this.ddlDoctorType.DataBind();
        this.ddlDoctorType.Items.Insert(0, new ListItem("--Select--", "0"));
        //this.ddlDoctorType.Items.Insert(1, new ListItem("All Doctor Visit", "1"));
        this.ddlDoctor.Items.Insert(0, new ListItem("--Select--", "0"));

    }
    protected void ddlDoctorType_SelectedIndexChanged(object sender, EventArgs e)
    {
        this.ddlDoctor.Items.Clear();
        this.ddlDoctor.DataSource = thealldocObject.DoctorMaster(ddlDoctorType.SelectedValue, Session["CoCode"].ToString().Trim());
        this.ddlDoctor.DataTextField = "doc_name";
        this.ddlDoctor.DataValueField = "doc_id";
        this.ddlDoctor.DataBind();
        this.ddlDoctor.Items.Insert(0, new ListItem("--Select--", "0"));
    }
    protected void btnSearch_Click(object sender, EventArgs e)
    {
        DataTable dt = thealldocObject.DoctorSearch(ddlDoctor.SelectedValue, ddlDoctorType.SelectedItem.Text, Session["CoCode"].ToString().Trim());
        GridView1.DataSource = dt;
        GridView1.DataBind();

    }
    protected void Button1_Click(object sender, EventArgs e)
    {
        System.Globalization.DateTimeFormatInfo dtf = new DateTimeFormatInfo();
        DateTime testdate = DateTime.ParseExact(txtDateOfVisit.Text, "dd/MM/yyyy", dtf);

        bool flag = true;
        if (CheckBox1.Checked == true)
        {

            bill = 1;
        }
        else
        {
            bill = 0;
        }



        for (int i = 0; i < GridView1.Rows.Count; i++)
        {
            CheckBox chk = (CheckBox)GridView1.Rows[i].Cells[0].FindControl("chkSelect");
            Label PReg = (Label)GridView1.Rows[i].Cells[1].FindControl("lblPatientReg");
            Label doc_id = (Label)GridView1.Rows[i].Cells[6].FindControl("lbldoc_id");
            Label DocTypeId = (Label)GridView1.Rows[i].Cells[7].FindControl("lblDocTypeId");
            string doctorid = "";
            if (ddlDoctorType.SelectedItem.Text == "RMO")
            {
                doctorid = ddlDoctor.SelectedValue.ToString();
            }
            else
            {
                doctorid=doc_id.Text;
            }

            if (chk.Checked== true)
            {
                if (thealldocObject.Insert_IPDPatientDoctorVisit(PReg.Text, doctorid, testdate.ToString("yyyy-MM-dd"), txtTimeofVisit.Text, DocTypeId.Text, txtRemarks.Text, bill.ToString(), Session["CoCode"].ToString().Trim(),Session["YearCode"].ToString().Trim(),Session["userName"].ToString().Trim()) == true)
                {
                    flag = true;
                }
                else
                {
                    flag = false;
                    break;
                }

            }
        }
        if (flag == true)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "msg", "alert('Insered Successfully !');", true);
            for (int i = 0; i < GridView1.Rows.Count; i++)
            {
                CheckBox chk = (CheckBox)GridView1.Rows[i].Cells[0].FindControl("chkSelect");
                chk.Checked = false;
            }
            txtRemarks.Text = "";

            GridFill();
            DropDownFill();
            CheckBox1.Checked = true;
            txtDateOfVisit.Text = DateTime.Now.ToString("dd/MM/yyyy");
            txtTimeofVisit.Text = System.DateTime.Now.ToShortTimeString();

            
           
        }
        else
            ScriptManager.RegisterStartupScript(this, this.GetType(), "msg", "alert('Error in inserted data  !');", true);


    }
    protected void GridView1_DataBound(object sender, EventArgs e)
    {
        
    }
    protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            Label lblPatientReg = (Label)e.Row.FindControl("lblPatientReg");
            Label DateTime = (Label)e.Row.FindControl("Label3");
            CheckBox chkSelect = (CheckBox)e.Row.FindControl("chkSelect");
            chkSelect.Checked = true;
            DataTable dt = thealldocObject.DateTimeShow(lblPatientReg.Text);
            if (dt.Rows.Count > 0)
            {
                DateTime.Text = dt.Rows[0]["DateTimeShow"].ToString();
            }

        }
    }
}