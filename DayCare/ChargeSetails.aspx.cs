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

public partial class DayCare_ChargeSetails : System.Web.UI.Page
{
    Main Objmain = new Main(ConfigurationSettings.AppSettings["ConnectionString"].ToString());
    ChargeDetails thechemicallist = new ChargeDetails(ConfigurationSettings.AppSettings["ConnectionString"].ToString());
    DC_PatientReg thereg = new DC_PatientReg(ConfigurationSettings.AppSettings["ConnectionString"].ToString());
    DC_AddMedicine theMedicine = new DC_AddMedicine(ConfigurationSettings.AppSettings["ConnectionString"].ToString());
  
    protected void Page_Load(object sender, EventArgs e)
    {
        Page.Title = "Charge Details";
        if (Session["userName"] == null)
        {
            Response.Redirect("../LoginPage.aspx");
        }
        if (Objmain.checkaccss(Session["CoCode"].ToString(), Session["UserRoleID"].ToString().Trim(), "CHARGE DETAILS", checkAccessType.ViewAction) == false)
        {
            Response.Redirect("../AccessDenied.aspx");
        }

        if (Objmain.checkaccss(Session["CoCode"].ToString(), Session["UserRoleID"].ToString().Trim(), "CHARGE DETAILS", checkAccessType.InsertAction) == false)
        {
            Button1.Enabled = false;
        }
        if (!IsPostBack)
        {
             if (Session["RegnNo"] != null)
            {
                txtreg.Text = Session["RegnNo"].ToString();
                CallFunc();
                GridFill();
            }

        }
        Session["RegnNo"] = null;
   


    }
    private void ResetAllFields()
    {
        txtbedno.Text = ""; txtreg.Text = "";
        txtname.Text = "";  
        HiddenField1.Value = "";
        Button1.Text = "Submit";
        TextBox1.Text = ""; TextBox2.Text = ""; TextBox3.Text = ""; TextBox4.Text = ""; TextBox5.Text = ""; TextBox6.Text = ""; TextBox7.Text = ""; TextBox8.Text = "";
        if (Objmain.checkaccss(Session["CoCode"].ToString(), Session["UserRoleID"].ToString().Trim(), "CHARGE DETAILS", checkAccessType.InsertAction) == false)
        {
            Button1.Enabled = false;
        }
    }
    private void GridFill()
    {
      
        GridView1.DataSource = thechemicallist.GridFill(Session["CoCode"].ToString().Trim(),Session["YearCode"].ToString().Trim(),txtreg.Text);
        GridView1.DataBind();
       
    }
    protected void GridView1_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        GridView1.PageIndex = e.NewPageIndex;
        GridFill();
    }
    protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "Select")
        {

            int index = Convert.ToInt32(e.CommandArgument);
            Label lblID = (Label)GridView1.Rows[index].FindControl("lblID");
            HiddenField1.Value = lblID.Text;

            Label PatientReg = (Label)GridView1.Rows[index].FindControl("PatientReg");
            txtreg.Text = PatientReg.Text;

            Label DialysisCharge = (Label)GridView1.Rows[index].FindControl("DialysisCharge");
            TextBox2.Text = DialysisCharge.Text;

            Label Date = (Label)GridView1.Rows[index].FindControl("Date");
            TextBox1.Text = Date.Text;

            Label ServiceCharge = (Label)GridView1.Rows[index].FindControl("ServiceCharge");
            TextBox3.Text = ServiceCharge.Text;

            Label Medicine = (Label)GridView1.Rows[index].FindControl("Medicine");
            TextBox4.Text = Medicine.Text;


            Label RequisitionCharge = (Label)GridView1.Rows[index].FindControl("RequisitionCharge");
            TextBox5.Text = RequisitionCharge.Text;

            Label DoctorFees = (Label)GridView1.Rows[index].FindControl("DoctorFees");
            TextBox6.Text = DoctorFees.Text;

            Label DispsableCharge = (Label)GridView1.Rows[index].FindControl("DispsableCharge");
            TextBox7.Text = DispsableCharge.Text;

            Label OtherCharge = (Label)GridView1.Rows[index].FindControl("OtherCharge");
            TextBox8.Text = OtherCharge.Text;
            CallFunc();
            Button1.Text = "Update";
            if (Objmain.checkaccss(Session["CoCode"].ToString(), Session["UserRoleID"].ToString().Trim(), "CHARGE DETAILS", checkAccessType.UpdateAction) == false)
            {
                Button1.Enabled = false;
            }
            else
            {
                Button1.Enabled = true;
            }
        }
    }
    protected void GridView1_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        Label lblID = (Label)GridView1.Rows[e.RowIndex].FindControl("lblID");
        if (thechemicallist.Deletecharge(Session["CoCode"].ToString().Trim(), Session["YearCode"].ToString().Trim(), lblID.Text) == true)
        {
            lblError.ForeColor = System.Drawing.Color.Green;
            lblError.Text = "Deleted Successfully";
        }
        else
        {
            lblError.ForeColor = System.Drawing.Color.Red;
            lblError.Text = "Error Deleted Data";
        }
        GridFill();
        ResetAllFields();
    }
    protected void Button1_Click(object sender, EventArgs e)
    {
        System.Globalization.DateTimeFormatInfo dtf = new DateTimeFormatInfo();
        if (TextBox2.Text.Trim() == "")
            TextBox2.Text = "0.00";
        if (TextBox3.Text.Trim() == "")
            TextBox3.Text = "0.00";
        if (TextBox4.Text.Trim() == "")
            TextBox4.Text = "0.00";
        if (TextBox5.Text.Trim() == "")
            TextBox5.Text = "0.00";
        if (TextBox6.Text.Trim() == "")
            TextBox6.Text = "0.00";
        if (TextBox7.Text.Trim() == "")
            TextBox7.Text = "0.00";
        if (TextBox8.Text.Trim() == "")
            TextBox8.Text = "0.00";
        if (TextBox9.Text.Trim() == "")
            TextBox9.Text = "0.00";
           if (TextBox1.Text != "" )
                {
                    DateTime testdate = DateTime.ParseExact(TextBox1.Text, "dd/MM/yyyy", dtf);

                    if (thechemicallist.InsertDischarge(Session["CoCode"].ToString().Trim(), Session["YearCode"].ToString().Trim(), testdate.ToString("yyyy-MM-dd"), txtreg.Text, TextBox2.Text, TextBox3.Text, TextBox4.Text, TextBox5.Text, TextBox8.Text, TextBox6.Text, TextBox7.Text, TextBox9.Text) == true)
                    {
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "msg", "alert('Insered Successfully !');", true);
                        Response.Redirect("../DayCare/PatientDashBoard.aspx");
                    }
                    else
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "msg", "alert('Error in Insered Data !');", true);
                } 

        GridFill();
        ResetAllFields();

    }
 
  
    public void CallFunc()
    {
        DataTable dt = thechemicallist.GetPatient(Session["CoCode"].ToString().Trim(), Session["YearCode"].ToString().Trim(), txtreg.Text);
        DataTable LabCharge = thechemicallist.GetLabChargeDetais(Session["CoCode"].ToString().Trim(), Session["YearCode"].ToString().Trim(), txtreg.Text);


        DataSet medicine = theMedicine.GetMedicine_Bill(Session["CoCode"].ToString().Trim(), Session["YearCode"].ToString().Trim(), txtreg.Text); 
        DataTable dttotalmedicine = medicine.Tables[1];

        DataTable prev = thereg.GetPreviousDate(Session["CoCode"].ToString().Trim(), txtreg.Text);
        if (prev.Rows.Count > 0)
        {
            TextBox10.Text = prev.Rows[0]["PrevDate"].ToString();
            TextBox11.Text = prev.Rows[0]["nowdate"].ToString();
        }
     
        if (dt.Rows.Count > 0)
        {
            txtname.Text = dt.Rows[0]["patient_name"].ToString();
            txtbedno.Text = dt.Rows[0]["BedNoText"].ToString();
            TextBox1.Text = dt.Rows[0]["APPO"].ToString();
            TextBox8.Text = dt.Rows[0]["DueAmount"].ToString();

            DataTable dt1 = thechemicallist.GetChargeDetais(Session["CoCode"].ToString().Trim(), Session["YearCode"].ToString().Trim(), txtreg.Text);
            DataTable dtgetdue = thechemicallist.GetPreviousDue(Session["CoCode"].ToString().Trim(), Session["YearCode"].ToString().Trim(), txtreg.Text);
            if (dt1.Rows.Count > 0)
            {
                TextBox2.Text = dt1.Rows[0]["DialysisCharge"].ToString();
                TextBox3.Text = dt1.Rows[0]["ServiceCharge"].ToString();
                TextBox4.Text = dttotalmedicine.Rows[0][0].ToString();
            //    TextBox5.Text = dt1.Rows[0]["RequisitionCharge"].ToString();
                TextBox6.Text = dt1.Rows[0]["DoctorFees"].ToString();
                TextBox7.Text = dt1.Rows[0]["DispsableCharge"].ToString();
                TextBox8.Text = dt1.Rows[0]["OtherCharge"].ToString();
                if (dtgetdue.Rows.Count > 0)
                {
                    TextBox9.Text = dtgetdue.Rows[0]["Debit"].ToString();
                }               
            }

            if (LabCharge.Rows.Count > 0)
            {
                TextBox5.Text = LabCharge.Rows[0]["Cost"].ToString();
            }
        }
    }
 

    protected void Button2_Click(object sender, EventArgs e)
    {
        ResetAllFields();
    }
    protected void Button4_Click(object sender, EventArgs e)
    {
        CallFunc();
        GridFill();
    }
}