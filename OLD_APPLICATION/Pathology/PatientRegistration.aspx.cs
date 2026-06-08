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
 
public partial class Pathology_PatientRegistration : System.Web.UI.Page
{
    Main Objmain = new Main(ConfigurationSettings.AppSettings["ConnectionString"].ToString());
    PH_PatientRegistration thereg = new PH_PatientRegistration(ConfigurationSettings.AppSettings["ConnectionString"].ToString());
    protected void Page_Load(object sender, EventArgs e)
    {


        if (Session["userName"] == null)
        {
            Response.Redirect("../LoginPage.aspx");
        }
        if (Objmain.checkaccss(Session["CoCode"].ToString(), Session["UserRoleID"].ToString().Trim(), "PATIENT REGISTRATION", checkAccessType.ViewAction) == false)
        {
            Response.Redirect("../AccessDenied.aspx");
        }

        if (Objmain.checkaccss(Session["CoCode"].ToString(), Session["UserRoleID"].ToString().Trim(), "PATIENT REGISTRATION", checkAccessType.InsertAction) == false)
        {
            Button1.Enabled = false;
        }
        Page.Title = "Patient Registration";
          

        if (!IsPostBack)
        {
            DropDownFill();
            GetRegCode();
            //GridFill();
            if (Request.QueryString["name"] != null && Request.QueryString["address"] != null && Request.QueryString["phone1"] != null && Request.QueryString["phone2"] != null)
            {
                txtPId.Value = Request.QueryString[5].ToString();
                txtname.Text = Request.QueryString[0].ToString();
                txtvillage.Text = Request.QueryString[1].ToString();
                txtcontact1.Text = Request.QueryString[2].ToString();
                txtcontact2.Text = Request.QueryString[3].ToString();
                txtamt.Text = Request.QueryString[4].ToString();
                TextBox1.Text = Request.QueryString[6].ToString();
                GetRegCode();
            }
           
        }
      
    }

    public void GetRegCode()
    {
        DataTable dt = thereg.GenerateRegNo(Session["CoCode"].ToString().Trim(), Session["YearCode"].ToString().Trim());
        txtreg.Text = dt.Rows[0][0].ToString();
    }
    private void DropDownFill()
    {
        this.ddlstate.DataSource = thereg.DropdownState(Session["CoCode"].ToString().Trim());
        this.ddlstate.DataTextField = "State_Name";
        this.ddlstate.DataValueField = "State_ID";
        this.ddlstate.DataBind();
        this.ddlstate.Items.Insert(0, new ListItem("--Select--", "0"));

        this.DropDownList1.Items.Clear();
        this.DropDownList1.DataSource = thereg.Dropdowndistrict(Session["CoCode"].ToString().Trim());
        this.DropDownList1.DataTextField = "DistrictName";
        this.DropDownList1.DataValueField = "ID";
        this.DropDownList1.DataBind();
        this.DropDownList1.Items.Insert(0, new ListItem("--Select--", "0"));


        this.rblsex.Items.Clear();
        this.rblsex.DataSource = thereg.DropdownDender(Session["CoCode"].ToString().Trim());
        this.rblsex.DataTextField = "SexName";
        this.rblsex.DataValueField = "ID";
        this.rblsex.DataBind();
        this.rblsex.Items.Insert(0, new ListItem("--Select--", "0"));
    }
    protected void Button1_Click(object sender, EventArgs e)
    {

        string Appo = Request.QueryString[5];
           if (Button1.Text == "Submit")
        {
            thereg.InsertRegistration(Session["CoCode"].ToString().Trim(), txtreg.Text, txtname.Text.ToUpper(), txtage.Text, rblsex.SelectedValue, txtvillage.Text.ToUpper(), txtvillage2.Text.ToUpper(), txtpo.Text.ToUpper(), txtps.Text.ToUpper(), DropDownList1.SelectedValue, txtpin.Text, ddlstate.SelectedValue, txtcontact1.Text, txtcontact2.Text, Session["userId"].ToString().Trim());
            thereg.InsertRequisition(Session["CoCode"].ToString().Trim(), Session["YearCode"].ToString().Trim(), txtPId.Value, txtreg.Text);
            ScriptManager.RegisterStartupScript(this, this.GetType(), "msg", "alert('Inserted Successfully !');", true);
        }
        else
        {
            thereg.UpdateRegistration(Session["CoCode"].ToString().Trim(),txtreg.Text, txtname.Text.ToUpper(), txtage.Text, rblsex.SelectedValue, txtvillage.Text.ToUpper(),txtvillage2.Text.ToUpper(), txtpo.Text.ToUpper(), txtps.Text.ToUpper(), DropDownList1.SelectedValue, txtpin.Text, ddlstate.SelectedValue, txtcontact1.Text, txtcontact2.Text);
            ScriptManager.RegisterStartupScript(this, this.GetType(), "msg", "alert('Updated Successfully !');", true);
            Button1.Text = "Submit";
        }

         ResetAllFields();
    }
    protected void Button2_Click(object sender, EventArgs e)
    {
        ResetAllFields();
    }
    private void ResetAllFields()
    {
        txtamt.Text = "";
          ddlstate.SelectedIndex = 0;
        txtps.Text = "";
        txtpo.Text = "";
        txtpin.Text = "";
        DropDownList1.SelectedIndex = 0;
        txtvillage.Text = "";
        txtage.Text = "";
        txtcontact1.Text = "";
        txtcontact2.Text = "";
        txtname.Text = "";
        rblsex.SelectedIndex = 0;
        TextBox1.Text = "";
        txtreg.Text = "";
        Button1.Text = "Submit";

    }

    protected int SearchIndexByName(string Value, DropDownList ddl)
    {
        int i;
        for (i = 0; i < ddl.Items.Count; i++)
        {
            if (ddl.Items[i].Text.Trim() == Value.Trim())
                return i;
        }
        return -1;
    }

    protected int SearchIndexById(string Value, DropDownList ddl)
    {
        int i;
        for (i = 0; i < ddl.Items.Count; i++)
        {
            if (ddl.Items[i].Value.Trim() == Value.Trim())
                return i;
        }
        return -1;
    }
    protected void Button4_Click(object sender, EventArgs e)
    {
        DataTable dt = thereg.GetNoofDia(txtreg.Text.Trim());
        try
        {
            if (txtreg.Text.Trim() != "")
            {
                DataTable custTable = thereg.GetPatientDetails(Session["CoCode"].ToString().Trim(), txtreg.Text.Trim());
                if (custTable.Rows.Count > 0)
                {
                    txtname.Text = custTable.Rows[0]["patient_name"].ToString();
                    txtvillage.Text = custTable.Rows[0]["vill_city"].ToString();
                    txtvillage2.Text = custTable.Rows[0]["vill_city2"].ToString();
                    txtage.Text = custTable.Rows[0]["age"].ToString();
                    txtpo.Text = custTable.Rows[0]["po"].ToString();
                    txtps.Text = custTable.Rows[0]["ps"].ToString();
                    DropDownList1.SelectedValue = custTable.Rows[0]["District"].ToString();
                    txtpin.Text = custTable.Rows[0]["Pin"].ToString();
                    txtcontact1.Text = custTable.Rows[0]["PhNo1"].ToString();
                    txtcontact2.Text = custTable.Rows[0]["PhNo2"].ToString();
                    ddlstate.SelectedIndex = SearchIndexById(custTable.Rows[0]["State_Id"].ToString(), ddlstate);
                    rblsex.SelectedIndex = SearchIndexByName(custTable.Rows[0]["sex"].ToString(), rblsex);
                   
                    Button1.Text = "Update";

                }
                else
                {
                    lblError.ForeColor = System.Drawing.Color.Red;
                    lblError.Text = "Please choose valid Patient";

                }
            }
            else
            {
                lblError.ForeColor = System.Drawing.Color.Red;
                lblError.Text = "Please choose Customer";
            }
        }
        catch (Exception ex)
        {
            lblError.ForeColor = System.Drawing.Color.Red;
            lblError.Text = ex.Message;
        }
    }
}