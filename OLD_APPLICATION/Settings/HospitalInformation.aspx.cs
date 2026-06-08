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
using System.IO;

public partial class Assignment_HospitalInformation : System.Web.UI.Page
{
    Main Objmain = new Main(ConfigurationSettings.AppSettings["ConnectionString"].ToString());
    HospitalInfoClass objHospital = new HospitalInfoClass(ConfigurationSettings.AppSettings["ConnectionString"].ToString());

    protected void Page_Load(object sender, EventArgs e)
    {
        Page.Title = "Hospital Information";
        if (Session["userName"] == null)
        {
            Response.Redirect("../LoginPage.aspx");
        }
        if (Objmain.checkaccss(Session["CoCode"].ToString(), Session["UserRoleID"].ToString().Trim(), "HOSPITAL INFORMATION", checkAccessType.ViewAction) == false)
        {
            Response.Redirect("../AccessDenied.aspx");
        }

        if (Objmain.checkaccss(Session["CoCode"].ToString(), Session["UserRoleID"].ToString().Trim(), "HOSPITAL INFORMATION", checkAccessType.InsertAction) == false)
        {
            btnSubmit.Enabled = false;
        }
        if (!IsPostBack)
        {
            //GridBind();
            Tab1Func();
            Fill();
        }

    }
    public void Fill()
    {
        DataTable dt = objHospital.FillHospitalDetails(Session["CoCode"].ToString().Trim(), Session["YearCode"].ToString().Trim());
        txtInstituteName.Text = dt.Rows[0]["coname"].ToString();
        txtCatagory.Text = dt.Rows[0]["Catagory"].ToString();
        txtAddress.Text = dt.Rows[0]["Addr"].ToString();
        txtEmail.Text = dt.Rows[0]["Email"].ToString();
        txtPhone1.Text = dt.Rows[0]["Phone"].ToString();
        txtPhone2.Text = dt.Rows[0]["Ph2"].ToString();
        txtFaxNo.Text = dt.Rows[0]["Fax"].ToString();
        txtWebsite.Text = dt.Rows[0]["web"].ToString();
        txticu.Text = dt.Rows[0]["icu"].ToString();
        txtdialysis.Text = dt.Rows[0]["dialysis"].ToString();
        txtNicu.Text = dt.Rows[0]["nicu"].ToString();
        txtGward.Text = dt.Rows[0]["generalward"].ToString();
        txtCabin.Text = dt.Rows[0]["cabin"].ToString();
        txtDelux.Text = dt.Rows[0]["delux"].ToString();
        txtHdu.Text = dt.Rows[0]["hdu"].ToString();
        txtDuplex.Text = dt.Rows[0]["Duplex"].ToString();
        txtLisenseNo.Text = dt.Rows[0]["lisenceno"].ToString();
        txtHosrgno.Text = dt.Rows[0]["HosRegnNo"].ToString();
        txtvalidityDate.Text = dt.Rows[0]["validity"].ToString();
        txtrmo.Text = dt.Rows[0]["Rmo"].ToString();
        txtregnoPrfx.Text = dt.Rows[0]["RegNoPrifix"].ToString();
        ddlMedcineIssue.SelectedValue = dt.Rows[0]["MedicineIssue"].ToString().Trim();
        ddlCommCalRule.SelectedValue = dt.Rows[0]["CommCalcRule"].ToString().Trim();
        ddlReqNo.SelectedValue = dt.Rows[0]["ReqNoRule"].ToString().Trim();
        hdnLogo.Value=dt.Rows[0]["logopath"].ToString().Trim();
        lblLogo.Text = dt.Rows[0]["logopath"].ToString().Trim();
        hdnLogo2.Value = dt.Rows[0]["logopath2"].ToString().Trim();
        lblLogo2.Text = dt.Rows[0]["logopath2"].ToString().Trim();
        btnSubmit.Text = "Update";
    }
    public void GridBind()
    {
        GridView1.DataSource = objHospital.GridFill(Session["CoCode"].ToString().Trim(), Session["YearCode"].ToString().Trim());
        GridView1.DataBind();
    }

    private void ResetAllFields()
    {
        txtInstituteName.Text = "";
        txtCatagory.Text = "";
        txtAddress.Text = "";
        txtEmail.Text = "";
        txtPhone1.Text = "";
        txtPhone2.Text = "";
        txtFaxNo.Text = "";
        txtWebsite.Text = "";
        txticu.Text = "";
        txtdialysis.Text = "";
        txtNicu.Text = "";
        txtGward.Text = "";
        txtCabin.Text = "";
        txtDelux.Text = "";
        txtHdu.Text = "";
        txtDuplex.Text = "";
        txtLisenseNo.Text = "";
        txtvalidityDate.Text = "";
        txtrmo.Text = "";
        txtregnoPrfx.Text = "";
        ddlMedcineIssue.SelectedIndex = 0;
        ddlCommCalRule.SelectedIndex = 0; ddlReqNo.SelectedIndex = 0;
    }
    public void Tab1Func()
    {
        Tab1.CssClass = "Clicked";
        Tab2.CssClass = "Initial";
    }
    protected void Tab1_Click(object sender, EventArgs e)
    {
        Tab1Func();
    }
    protected void Tab2_Click(object sender, EventArgs e)
    {
        Tab1.CssClass = "Initial";
        Tab2.CssClass = "Clicked";
    }

    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        System.Globalization.DateTimeFormatInfo dtf = new DateTimeFormatInfo();
        DateTime valdate = DateTime.ParseExact(txtvalidityDate.Text, "dd/MM/yyyy", dtf);
        string logofile, logofile2;
        int Total;
        Total = Convert.ToInt32(txticu.Text) + Convert.ToInt32(txtdialysis.Text) + Convert.ToInt32(txtNicu.Text) + Convert.ToInt32(txtGward.Text) + Convert.ToInt32(txtCabin.Text) + Convert.ToInt32(txtDelux.Text) + Convert.ToInt32(txtHdu.Text) + Convert.ToInt32(txtDuplex.Text);

        if (btnSubmit.Text == "Save")
        {
            if (FileUpload1.HasFile)
            {
                //create the path to save the file to
                string fileName = Path.Combine(Server.MapPath("~/photo"), FileUpload1.FileName);
                //save the file to our local path
                FileUpload1.SaveAs(fileName);
                logofile = FileUpload1.FileName;
            }
            else
            {
                logofile = "";
            }
            if (FileUpload2.HasFile)
            {
                //create the path to save the file to
                string fileName2 = Path.Combine(Server.MapPath("~/photo"), FileUpload2.FileName);
                //save the file to our local path
                FileUpload2.SaveAs(fileName2);
                logofile2 = FileUpload2.FileName;
            }
            else
            {
                logofile2 = "";
            }
            if (objHospital.HospitalInfo_Insert_Update_Delete("1", null, txtInstituteName.Text, txtCatagory.Text, txtAddress.Text, txtEmail.Text, txtPhone1.Text, txtPhone2.Text, txtFaxNo.Text, txtWebsite.Text, txticu.Text, txtdialysis.Text, txtNicu.Text, txtGward.Text, txtCabin.Text, txtDelux.Text, txtHdu.Text, txtDuplex.Text, Total.ToString(), txtLisenseNo.Text, txtHosrgno.Text, valdate.ToString("yyyy-MM-dd"), txtrmo.Text, ddlMedcineIssue.SelectedValue, txtregnoPrfx.Text, ddlCommCalRule.SelectedValue.Trim(), ddlReqNo.SelectedValue.Trim(), logofile, logofile2, Session["CoCode"].ToString().Trim(), Session["YearCode"].ToString().Trim(), Session["userName"].ToString().Trim()) == true)
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "msg", "alert('Inserted Successfully  !');", true);
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "msg", "alert('Error in Inserted Data !');", true);
            }
        }
        else
        {
            if (FileUpload1.HasFile)
            {
                //create the path to save the file to
                string fileName = Path.Combine(Server.MapPath("~/photo"), FileUpload1.FileName);
                //save the file to our local path
                FileUpload1.SaveAs(fileName);
                logofile = FileUpload1.FileName;
            }
            else
            {
                logofile = hdnLogo.Value;
            } 
            if (FileUpload2.HasFile)
            {
                //create the path to save the file to
                string fileName2 = Path.Combine(Server.MapPath("~/photo"), FileUpload2.FileName);
                //save the file to our local path
                FileUpload2.SaveAs(fileName2);
                logofile2 = FileUpload2.FileName;
            }
            else
            {
                logofile2 = hdnLogo2.Value;
            }  
            if (objHospital.HospitalInfo_Insert_Update_Delete("2", HiddenField1.Value, txtInstituteName.Text, txtCatagory.Text, txtAddress.Text, txtEmail.Text, txtPhone1.Text, txtPhone2.Text, txtFaxNo.Text, txtWebsite.Text, txticu.Text, txtdialysis.Text, txtNicu.Text, txtGward.Text, txtCabin.Text, txtDelux.Text, txtHdu.Text, txtDuplex.Text, Total.ToString(), txtLisenseNo.Text, txtHosrgno.Text, valdate.ToString("yyyy-MM-dd"), txtrmo.Text, ddlMedcineIssue.SelectedValue, txtregnoPrfx.Text, ddlCommCalRule.SelectedValue.Trim(), ddlReqNo.SelectedValue.Trim(),logofile,logofile2, Session["CoCode"].ToString().Trim(), Session["YearCode"].ToString().Trim(), Session["userName"].ToString().Trim()) == true)
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "msg", "alert('Updated Successfully !');", true);
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "msg", "alert('Error in Updated Data !');", true);
            }
            //btnSubmit.Text = "Save";
        }
        GridBind();
        //ResetAllFields();
    }

    protected void GridView1_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        Label lblHid = (Label)GridView1.Rows[e.RowIndex].FindControl("lblHid");
        if (objHospital.HospitalInfo_Insert_Update_Delete("3", lblHid.Text, null,null,null,null, null,null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null,null, Session["CoCode"].ToString().Trim(), Session["YearCode"].ToString().Trim(), null) == true)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "msg", "alert('Deleted Successfully !');", true);
        }
        else
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "msg", "alert('Error in Deleted Data !');", true);
        }
        GridBind();
        ResetAllFields();
        btnSubmit.Text = "Save";
    }
    protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "Select")
        {
            int index = Convert.ToInt32(e.CommandArgument);

            Tab1Func();

            Label lblHid = (Label)GridView1.Rows[index].FindControl("lblHid");
            HiddenField1.Value = lblHid.Text;

            Label lblInstitutionName = (Label)GridView1.Rows[index].FindControl("lblInstitutionName");
            txtInstituteName.Text = lblInstitutionName.Text;

            Label lblCatagory = (Label)GridView1.Rows[index].FindControl("lblCatagory");
            txtCatagory.Text = lblCatagory.Text;

            Label lblAddress = (Label)GridView1.Rows[index].FindControl("lblAddress");
            txtAddress.Text = lblAddress.Text;

            Label lblEmail = (Label)GridView1.Rows[index].FindControl("lblEmail");
            txtEmail.Text = lblEmail.Text;

            Label lblPh1 = (Label)GridView1.Rows[index].FindControl("lblPh1");
            txtPhone1.Text = lblPh1.Text;

            Label lblPh2 = (Label)GridView1.Rows[index].FindControl("lblPh2");
            txtPhone2.Text = lblPh2.Text;

            Label lblFax = (Label)GridView1.Rows[index].FindControl("lblFax");
            txtFaxNo.Text = lblFax.Text;

            Label lblwebsite = (Label)GridView1.Rows[index].FindControl("lblwebsite");
            txtWebsite.Text = lblwebsite.Text;

            Label lblicu = (Label)GridView1.Rows[index].FindControl("lblicu");
            txticu.Text = lblicu.Text;

            Label lbldialysis = (Label)GridView1.Rows[index].FindControl("lbldialysis");
            txtdialysis.Text = lbldialysis.Text;

            Label lblnicu = (Label)GridView1.Rows[index].FindControl("lblnicu");
            txtNicu.Text = lblnicu.Text;

            Label lblgeneralward = (Label)GridView1.Rows[index].FindControl("lblgeneralward");
            txtGward.Text = lblgeneralward.Text;

            Label lblcabin = (Label)GridView1.Rows[index].FindControl("lblcabin");
            txtCabin.Text = lblcabin.Text;

            Label lbldelux = (Label)GridView1.Rows[index].FindControl("lbldelux");
            txtDelux.Text = lbldelux.Text;

            Label lblhdu = (Label)GridView1.Rows[index].FindControl("lblhdu");
            txtHdu.Text = lblhdu.Text;

            Label lblDuplex = (Label)GridView1.Rows[index].FindControl("lblDuplex");
            txtDuplex.Text = lblDuplex.Text;

            Label lbllisenceno = (Label)GridView1.Rows[index].FindControl("lbllisenceno");
            txtLisenseNo.Text = lbllisenceno.Text;

            Label lblvalidity = (Label)GridView1.Rows[index].FindControl("lblvalidity");
            txtvalidityDate.Text = lblvalidity.Text;

            Label lblRmo = (Label)GridView1.Rows[index].FindControl("lblRmo");
            txtrmo.Text = lblRmo.Text;

            Label lblMedicineIssue = (Label)GridView1.Rows[index].FindControl("lblMedicineIssue");
            ddlMedcineIssue.SelectedValue = lblMedicineIssue.Text.Trim();

            Label lblPrefix = (Label)GridView1.Rows[index].FindControl("lblPre");
            txtregnoPrfx.Text = lblPrefix.Text;

            Label lblCommCalcRule = (Label)GridView1.Rows[index].FindControl("lblCommCalcRule");
            ddlCommCalRule.SelectedValue = lblCommCalcRule.Text.Trim();

            Label lblReqNoRule = (Label)GridView1.Rows[index].FindControl("lblReqNoRule");
            ddlReqNo.SelectedValue = lblReqNoRule.Text.Trim();
        }


        btnSubmit.Text = "Update";
        if (Objmain.checkaccss(Session["CoCode"].ToString(), Session["UserRoleID"].ToString().Trim(), "HOSPITAL INFORMATION", checkAccessType.UpdateAction) == false)
        {
            btnSubmit.Enabled = false;
        }
        else
        {
            btnSubmit.Enabled = true;
        }
    }
}