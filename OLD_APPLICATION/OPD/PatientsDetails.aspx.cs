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
 
public partial class OPD_PatientsDetails : System.Web.UI.Page
{
    Main Objmain = new Main(ConfigurationSettings.AppSettings["ConnectionString"].ToString());
    PatientsDetails theopdpatient = new PatientsDetails(ConfigurationSettings.AppSettings["ConnectionString"].ToString());
    PatientAppointment thepatientAppo = new PatientAppointment(ConfigurationSettings.AppSettings["ConnectionString"].ToString());
    System.Text.StringBuilder rpt = new System.Text.StringBuilder();
    protected void Page_Load(object sender, EventArgs e)
    {

        Page.Title = "Patient Details";

        if (Session["userName"] == null)
        {
            Response.Redirect("../LoginPage.aspx");
        }
        if (Objmain.checkaccss(Session["CoCode"].ToString(), Session["UserRoleID"].ToString().Trim(), "PATIENT DETAILS", checkAccessType.ViewAction) == false)
        {
            Response.Redirect("../AccessDenied.aspx");
        }

        if (Objmain.checkaccss(Session["CoCode"].ToString(), Session["UserRoleID"].ToString().Trim(), "PATIENT DETAILS", checkAccessType.InsertAction) == false)
        {
            ButChk.Enabled = false;
            Button5.Enabled = false;
            Button1.Enabled = false;
            Button7.Enabled = false;
            Button9.Enabled = false;
            Button11.Enabled = false;
            Button13.Enabled = false;
            Button15.Enabled = false;
            Button18.Enabled = false;
            Button20.Enabled = false;
            btnSaveDiagnosis.Enabled = false;
        }
        
        if (!IsPostBack)
        {
             Tab1.CssClass = "Clicked";
            MainView.ActiveViewIndex = 0;
            DropDownFill();
            Panel1.Visible = false;
            Panel2.Visible = false;
            if (Session["Prescription"] != null)
            {
                GoToPrescription();
            }
            if (Session["RegNo"] != null)
            {
                txtreg.Text = Session["RegNo"].ToString();
                FillDtls();
            }
        }
        Session["RegNo"] = null;
        Session["Prescription"] = null;
    }

 
    public void GridHistory()
    {
        GridView1.DataSource = theopdpatient.GridFill(Session["CoCode"].ToString().Trim(), txtreg.Text);
        GridView1.DataBind();
    }

    private void ResetHistory()
    {
        for (int t = 21; t <= 45; t++)
        {
            TextBox t1 = (TextBox)Page.FindControl("ctl00$ContentPlaceHolder1$TextBox" + t.ToString());
            t1.Text = "";
        }
    }

    private void ResetTotalPage()
    {
        for (int t = 1; t <= 12; t++)
        {
            TextBox t1 = (TextBox)Page.FindControl("ctl00$ContentPlaceHolder1$TextBox" + t.ToString());
            t1.Text = "";
        }
        lblError.Text = "";
    }
    protected void Tab1_Click(object sender, EventArgs e)
    {
        Tab1.CssClass = "Clicked";
        Tab2.CssClass = "Initial";
        Tab3.CssClass = "Initial";
        Tab4.CssClass = "Initial";
        Tab5.CssClass = "Initial";
        Tab6.CssClass = "Initial";
        Tab7.CssClass = "Initial";
        Tab8.CssClass = "Initial";
        Tab9.CssClass = "Initial";
        MainView.ActiveViewIndex = 0;
    }
    protected void Tab2_Click(object sender, EventArgs e)
    {
        Tab1.CssClass = "Initial";
        Tab2.CssClass = "Clicked";
        Tab3.CssClass = "Initial";
        Tab4.CssClass = "Initial";
        Tab5.CssClass = "Initial";
        Tab6.CssClass = "Initial";
        Tab7.CssClass = "Initial";
        Tab8.CssClass = "Initial";
        Tab9.CssClass = "Initial";
        MainView.ActiveViewIndex = 1;
        GridHistory();
    }

    protected void Tab3_Click(object sender, EventArgs e)
    {
        Tab1.CssClass = "Initial";
        Tab2.CssClass = "Initial";
        Tab3.CssClass = "Clicked";
        Tab4.CssClass = "Initial";
        Tab5.CssClass = "Initial";
        Tab6.CssClass = "Initial";
        Tab7.CssClass = "Initial";
        Tab8.CssClass = "Initial";
        Tab9.CssClass = "Initial";
        MainView.ActiveViewIndex = 2;
        GridFillVaccine();
    }
    protected void Tab4_Click(object sender, EventArgs e)
    {
        Tab1.CssClass = "Initial";
        Tab2.CssClass = "Initial";
        Tab3.CssClass = "Initial";
        Tab4.CssClass = "Clicked";
        Tab5.CssClass = "Initial";
        Tab6.CssClass = "Initial";
        Tab7.CssClass = "Initial";
        Tab8.CssClass = "Initial";
        Tab9.CssClass = "Initial";
        MainView.ActiveViewIndex = 3;
        GridFillInvestigation();
    }
    protected void Tab5_Click(object sender, EventArgs e)
    {
        Tab1.CssClass = "Initial";
        Tab2.CssClass = "Initial";
        Tab3.CssClass = "Initial";
        Tab4.CssClass = "Initial";
        Tab5.CssClass = "Clicked";
        Tab6.CssClass = "Initial";
        Tab7.CssClass = "Initial";
        Tab8.CssClass = "Initial";
        Tab9.CssClass = "Initial";
        MainView.ActiveViewIndex = 4;
        GridFillBody();
    }
    protected void Tab6_Click(object sender, EventArgs e)
    {
        GoToPrescription();
    }

    public void GoToPrescription()
    {
        Tab1.CssClass = "Initial";
        Tab2.CssClass = "Initial";
        Tab3.CssClass = "Initial";
        Tab4.CssClass = "Initial";
        Tab5.CssClass = "Initial";
        Tab6.CssClass = "Clicked";
        Tab7.CssClass = "Initial";
        Tab8.CssClass = "Initial";
        Tab9.CssClass = "Initial";
        MainView.ActiveViewIndex = 5;
        Panel1.Visible = true;
        Panel2.Visible = false;
        Resetprescription();

        TextBox202.Text = DateTime.Now.ToString("dd/MM/yyyy");
        //GeneratePrescritionId();
        GridPrescription();
    }
    protected void Tab7_Click(object sender, EventArgs e)
    {
        Tab1.CssClass = "Initial";
        Tab2.CssClass = "Initial";
        Tab3.CssClass = "Initial";
        Tab4.CssClass = "Initial";
        Tab5.CssClass = "Initial";
        Tab6.CssClass = "Initial";
        Tab7.CssClass = "Clicked";
        Tab8.CssClass = "Initial";
        Tab9.CssClass = "Initial";
        MainView.ActiveViewIndex = 6;
       
    }
    protected void Tab8_Click(object sender, EventArgs e)
    {
        Tab1.CssClass = "Initial";
        Tab2.CssClass = "Initial";
        Tab3.CssClass = "Initial";
        Tab4.CssClass = "Initial";
        Tab5.CssClass = "Initial";
        Tab6.CssClass = "Initial";
        Tab7.CssClass = "Initial";
        Tab8.CssClass = "Clicked";
        Tab9.CssClass = "Initial";
        MainView.ActiveViewIndex =7;
        GetHospitalNote();
    }

    protected void Tab9_Click(object sender, EventArgs e)
    {
        Tab1.CssClass = "Initial";
        Tab2.CssClass = "Initial";
        Tab3.CssClass = "Initial";
        Tab4.CssClass = "Initial";
        Tab5.CssClass = "Initial";
        Tab6.CssClass = "Initial";
        Tab7.CssClass = "Initial";
        Tab8.CssClass = "Initial";
        Tab9.CssClass = "Clicked";
        MainView.ActiveViewIndex = 8;
        GetDiagnosis();
        txtDiagDate.Text = txtCheckedDate.Text;
    }

    public void GetDiagnosis()
    {
        if (txtreg.Text != "")
        {
            DataTable dt = theopdpatient.GetDiagnosis(Session["CoCode"].ToString().Trim(), txtreg.Text,txtprescripId.Text.Trim());
            if (dt.Rows.Count > 0)
            {
                //btnSaveDiagnosis.Text = "Update";
                //TextBox205.Text = dt.Rows[0]["NoteDtls"].ToString();
                GridView6.DataSource = dt;
                GridView6.DataBind();
                //if (Objmain.checkaccss(Session["CoCode"].ToString(), Session["UserRoleID"].ToString().Trim(), "PATIENT DETAILS", checkAccessType.UpdateAction) == false)
                //{
                //    btnSaveDiagnosis.Enabled = false;
                //}
                //else
                //{
                //    btnSaveDiagnosis.Enabled = true;
                //}
            }
        }
    }

    public void GetHospitalNote()
    {
        DataTable dt = theopdpatient.GetHospitalNote(Session["CoCode"].ToString().Trim(), txtreg.Text);
        if (dt.Rows.Count > 0)
        {
            Button18.Text = "Update";
            TextBox205.Text = dt.Rows[0]["NoteDtls"].ToString();
            if (Objmain.checkaccss(Session["CoCode"].ToString(), Session["UserRoleID"].ToString().Trim(), "PATIENT DETAILS", checkAccessType.UpdateAction) == false)
            {
                Button18.Enabled = false;
            }
            else
            {
                Button18.Enabled = true;
            }
        }
    }
   
    //public void patientinfoFill(DataTable dt)
    //{
    //    TextBox13.Text = dt.Rows[0]["GuadianName"].ToString();
    //    string[] ph1 = dt.Rows[0]["PhNo1"].ToString().Split(' ');
    //    if (ph1.Length > 1)
    //        TextBox14.Text = ph1[1];
    //    TextBox19.Text = ph1[0];
    //    TextBox15.Text = dt.Rows[0]["Address"].ToString();
    //    TextBox16.Text = dt.Rows[0]["DistrictName"].ToString();
    //    TextBox17.Text = dt.Rows[0]["AppDt"].ToString();
    //    TextBox18.Text = dt.Rows[0]["AppointmentTime"].ToString();
    //}
    protected void Button4_Click(object sender, EventArgs e)
    {
       
        FillDtls();
        //GeneratePrescritionId();
        GridPrescription();
   
    }

    public void FillDtls()
    {
        string regno = Request.Form[txtreg.UniqueID];
        if (regno != null)
        {
            txtreg.Text = regno;
        }
        DataTable dt = theopdpatient.PatientFill(Session["CoCode"].ToString().Trim(), txtreg.Text,TextBox1.Text);
        if (dt.Rows.Count > 0)
        {
            Button5.Text = "Update"; Button7.Text = "Update";

            if (Objmain.checkaccss(Session["CoCode"].ToString(), Session["UserRoleID"].ToString().Trim(), "PATIENT DETAILS", checkAccessType.UpdateAction) == false)
            {
                Button5.Enabled = false;
                Button7.Enabled = false;
            }
            else
            {
                Button5.Enabled = true;
                Button7.Enabled = true;
            }

            //TextBox1.Text = dt.Rows[0]["AppoNo"].ToString();
            if (dt.Rows[0]["PatientChecked"].ToString() == "1")
            {
                Chk.Checked = true;
            }
            else
            {
                Chk.Checked = false;
            }
            TextBox3.Text = dt.Rows[0]["PName"].ToString();
            TextBox4.Text = dt.Rows[0]["Age"].ToString();
            TextBox5.Text = dt.Rows[0]["G"].ToString();
            TextBox6.Text = dt.Rows[0]["P"].ToString();
            TextBox7.Text = dt.Rows[0]["A"].ToString();
            TextBox8.Text = dt.Rows[0]["Live"].ToString();
            TextBox9.Text = dt.Rows[0]["LMP"].ToString();
            TextBox10.Text = dt.Rows[0]["EDD"].ToString();
            TextBox11.Text = dt.Rows[0]["LCB"].ToString();
            TextBox12.Text = dt.Rows[0]["Comment"].ToString();
            txtCheckedDate.Text = dt.Rows[0]["AppDt"].ToString();
            ///patientinfoFill(dt);
            ///
            TextBox13.Text = dt.Rows[0]["GuadianName"].ToString();
            string[] ph1 = dt.Rows[0]["PhNo1"].ToString().Split(' ');
            if (ph1.Length > 1)
                TextBox14.Text = ph1[1];
            TextBox19.Text = ph1[0];
            TextBox15.Text = dt.Rows[0]["Address"].ToString();
            TextBox16.Text = dt.Rows[0]["DistrictName"].ToString();
            TextBox17.Text = dt.Rows[0]["AppDt"].ToString();
            TextBox18.Text = dt.Rows[0]["AppointmentTime"].ToString();

            TextBox203.Text = dt.Rows[0]["PName"].ToString();
            TextBox202.Text = DateTime.Now.ToString("dd/MM/yyyy");
            GeneratePrescritionId();
            //if (dt.Rows[0]["PrsecriptionId"].ToString() == "")
            //{
            //    GeneratePrescritionId();
            //}
            //else
            //{
            //    txtprescripId.Text = dt.Rows[0]["PrsecriptionId"].ToString();
            //    TextBox201.Text = dt.Rows[0]["PrsecriptionId"].ToString();
            //    TextBox202.Text = dt.Rows[0]["AppDt"].ToString();
            //}

        }
        else
        {
            dt = thepatientAppo.GetOpdRegPatientDetails(Session["CoCode"].ToString().Trim(), "", txtreg.Text);

            if (dt.Rows.Count > 0)
            {
                TextBox3.Text = dt.Rows[0]["PName"].ToString();
                TextBox4.Text = dt.Rows[0]["Age"].ToString();
                TextBox13.Text = dt.Rows[0]["GuadianName"].ToString();
                TextBox14.Text = dt.Rows[0]["PhNo1"].ToString();
                TextBox15.Text = dt.Rows[0]["Address"].ToString();
                TextBox16.Text = dt.Rows[0]["District"].ToString();
            }
        }
    }
    public void GeneratePrescritionId()
    {
         DataTable dt = theopdpatient.GeneratePrescription(Session["CoCode"].ToString().Trim());
        TextBox201.Text = dt.Rows[0][0].ToString();
        txtprescripId.Text = dt.Rows[0][0].ToString();

    }
    protected void Button1_Click(object sender, EventArgs e)
    {
        System.Globalization.DateTimeFormatInfo dtf = new DateTimeFormatInfo();
         for (int t = 21; t <45; t = t + 5)
        {
            TextBox t1 = (TextBox)Page.FindControl("ctl00$ContentPlaceHolder1$TextBox" + t.ToString());
            TextBox t2 = (TextBox)Page.FindControl("ctl00$ContentPlaceHolder1$TextBox" + (t + 1).ToString());
            TextBox t3 = (TextBox)Page.FindControl("ctl00$ContentPlaceHolder1$TextBox" + (t + 2).ToString());
            TextBox t4 = (TextBox)Page.FindControl("ctl00$ContentPlaceHolder1$TextBox" + (t + 3).ToString());
            TextBox t5 = (TextBox)Page.FindControl("ctl00$ContentPlaceHolder1$TextBox" + (t + 4).ToString());
            
            if (t1.Text != "" && t2.Text != "" && t3.Text != "" && t4.Text != "" && t5.Text != "")
            {
                DateTime testdate = DateTime.ParseExact(t1.Text, "dd/MM/yyyy", dtf);
                if (theopdpatient.InsertHistory(Session["CoCode"].ToString().Trim(), txtreg.Text, testdate.ToString("yyyy-MM-dd"), t2.Text, t3.Text, t4.Text, t5.Text) == true)
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "msg", "alert('Patient History Inserted Successfully !');", true);
              
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "msg", "alert('Error in inserted Data to Patient History !');", true);
                }
            }
            else
                break;
        }
        GridHistory();
        ResetHistory();
    }
    protected void Button2_Click(object sender, EventArgs e)
    {

    }
    protected void Button5_Click(object sender, EventArgs e)
    {
        if (theopdpatient.UpdatePatientReg(Session["CoCode"].ToString().Trim(), TextBox1.Text, txtreg.Text, TextBox5.Text, TextBox6.Text, TextBox7.Text, TextBox8.Text, TextBox9.Text, TextBox10.Text, TextBox11.Text, TextBox12.Text) == true)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "msg", "alert('Inserted Successfully !');", true);
        }
        else
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "msg", "alert('Error in Inserted Data !');", true);
        }
    }
    protected void Button6_Click(object sender, EventArgs e)
    {
        ResetTotalPage();
    }
    protected void Button8_Click(object sender, EventArgs e)
    {
        ResetInfo();
    }
    public void ResetInfo()
    {
        for (int t = 13; t <= 18; t++)
        {
            TextBox t1 = (TextBox)Page.FindControl("ctl00$ContentPlaceHolder1$TextBox" + t.ToString());
            t1.Text = "";
        }

    }
    protected void Button7_Click(object sender, EventArgs e)
    {
          System.Globalization.DateTimeFormatInfo dtf = new DateTimeFormatInfo();
           DateTime testdate = DateTime.ParseExact(TextBox17.Text, "dd/MM/yyyy", dtf);
           string ph = "+91 " + TextBox14.Text;
           if (theopdpatient.UpdateAppointment(Session["CoCode"].ToString().Trim(), TextBox1.Text, txtreg.Text, TextBox13.Text, ph, TextBox15.Text, TextBox16.Text, testdate.ToString("yyyy-MM-dd"), TextBox18.Text) == true)
           {
               ScriptManager.RegisterStartupScript(this, this.GetType(), "msg", "alert('Patient Info Inserted Successfully !');", true);
               
           }
           else
           {
               ScriptManager.RegisterStartupScript(this, this.GetType(), "msg", "alert('Error in Inserted Data to Patient Info !');", true);
           }
    }
    protected void Button10_Click(object sender, EventArgs e)
    {
        ResetInvestigation();
    }

    public void ResetVaccine()
    {
        for (int t = 91,d=33; t <= 135; t=t+3,d++)
        {
            TextBox t1 = (TextBox)Page.FindControl("ctl00$ContentPlaceHolder1$TextBox" + (t+1).ToString());
            TextBox t2 = (TextBox)Page.FindControl("ctl00$ContentPlaceHolder1$TextBox" + (t+2).ToString());
            DropDownList d1 = (DropDownList)Page.FindControl("ctl00$ContentPlaceHolder1$DropDownList" + d.ToString());
            d1.SelectedIndex = 0;
            t1.Text = ""; t2.Text = "";
        }
    }

    public void ResetInvestigation()
    {
        for (int t = 46; t <= 90; t++)
        {
            TextBox t1 = (TextBox)Page.FindControl("ctl00$ContentPlaceHolder1$TextBox" + t.ToString());
            t1.Text = "";
        }
    }

    public void GridFillInvestigation()
    {
        GridView2.DataSource = theopdpatient.GridFillInvestigation(Session["CoCode"].ToString().Trim(), txtreg.Text);
        GridView2.DataBind();
    }


    public void GridFillVaccine()
    {
        GridView3.DataSource = theopdpatient.GridFillVaccine(Session["CoCode"].ToString().Trim(), txtreg.Text);
        GridView3.DataBind();
    }
    protected void Button9_Click(object sender, EventArgs e)
    {
        System.Globalization.DateTimeFormatInfo dtf = new DateTimeFormatInfo();
         for (int t = 46; t < 90; t = t + 3)
        {
            TextBox t1 = (TextBox)Page.FindControl("ctl00$ContentPlaceHolder1$TextBox" + t.ToString());
            TextBox t2 = (TextBox)Page.FindControl("ctl00$ContentPlaceHolder1$TextBox" + (t + 1).ToString());
            TextBox t3 = (TextBox)Page.FindControl("ctl00$ContentPlaceHolder1$TextBox" + (t + 2).ToString());
            if (t1.Text != "" && t2.Text != "" && t3.Text != "" )
            {
                DateTime testdate = DateTime.ParseExact(t1.Text, "dd/MM/yyyy", dtf);
                if (theopdpatient.InsertInvestigation(Session["CoCode"].ToString().Trim(), txtreg.Text, testdate.ToString("yyyy-MM-dd"), t2.Text, t3.Text) == true)
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "msg", "alert('Patient Investigation Inserted Successfully !');", true);
                 
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "msg", "alert('Error in Inserted Data in Patient Investigation !');", true);
                }
            }
            else
                break;
        }
        GridFillInvestigation();
        ResetInvestigation();
    }
    protected void Button12_Click(object sender, EventArgs e)
    {
        ResetVaccine();
    }


 
    protected void Button11_Click(object sender, EventArgs e)
    {
        System.Globalization.DateTimeFormatInfo dtf = new DateTimeFormatInfo();
         for (int t = 91,d=33; t < 135; t = t + 3,d++)
        {
            DropDownList d1 = (DropDownList)Page.FindControl("ctl00$ContentPlaceHolder1$DropDownList" + d.ToString());
            TextBox t2 = (TextBox)Page.FindControl("ctl00$ContentPlaceHolder1$TextBox" + (t + 1).ToString());
            TextBox t3 = (TextBox)Page.FindControl("ctl00$ContentPlaceHolder1$TextBox" + (t + 2).ToString());
            if (t2.Text != "" && t3.Text != "")
            {
                DateTime testdate = DateTime.ParseExact(t2.Text, "dd/MM/yyyy", dtf);
                if (theopdpatient.InsertVaccine(Session["CoCode"].ToString().Trim(), txtreg.Text, d1.SelectedValue, testdate.ToString("yyyy-MM-dd"), t3.Text) == true)
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "msg", "alert('Patient Vaccine Inserted Successfully !');", true);
             
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "msg", "alert('Error in Inserted Data to Patient Vaccine !');", true);
                }
            }
            else
                break;
        }
        GridFillVaccine();
        ResetVaccine();
    }
    protected void Button14_Click(object sender, EventArgs e)
    {
        ResetBody();
    }

    public void ResetBody()
    {
        for (int t = 136,t0=138; t < 189; t=t+11)
        {
            TextBox t1 = (TextBox)Page.FindControl("ctl00$ContentPlaceHolder1$TextBox" + t.ToString());
            TextBox t2 = (TextBox)Page.FindControl("ctl00$ContentPlaceHolder1$TextBox" + t0.ToString());
            t1.Text = ""; t2.Text = "";
        }
    }

    protected void Button13_Click(object sender, EventArgs e)
    {
        System.Globalization.DateTimeFormatInfo dtf = new DateTimeFormatInfo();
         for (int t = 136,d=48; t < 190; t = t + 11,d++)
        {
            TextBox t1 = (TextBox)Page.FindControl("ctl00$ContentPlaceHolder1$TextBox" + t.ToString());
            DropDownList d1 = (DropDownList)Page.FindControl("ctl00$ContentPlaceHolder1$DropDownList" + d.ToString());
            TextBox t3 = (TextBox)Page.FindControl("ctl00$ContentPlaceHolder1$TextBox" + (t + 2).ToString());
            TextBox t4 = (TextBox)Page.FindControl("ctl00$ContentPlaceHolder1$TextBox" + (t + 3).ToString());
            TextBox t5 = (TextBox)Page.FindControl("ctl00$ContentPlaceHolder1$TextBox" + (t + 4).ToString());
            TextBox t6 = (TextBox)Page.FindControl("ctl00$ContentPlaceHolder1$TextBox" + (t + 5).ToString());
            TextBox t7 = (TextBox)Page.FindControl("ctl00$ContentPlaceHolder1$TextBox" + (t + 6).ToString());
            TextBox t8= (TextBox)Page.FindControl("ctl00$ContentPlaceHolder1$TextBox" + (t + 7).ToString());
            TextBox t9 = (TextBox)Page.FindControl("ctl00$ContentPlaceHolder1$TextBox" + (t + 8).ToString());
            TextBox t10 = (TextBox)Page.FindControl("ctl00$ContentPlaceHolder1$TextBox" + (t + 9).ToString());
            TextBox t11 = (TextBox)Page.FindControl("ctl00$ContentPlaceHolder1$TextBox" + (t + 10).ToString());
            if (d1.SelectedValue != "0")
            {
               // DateTime testdate = DateTime.ParseExact(txtCheckedDate.Text, "dd/MM/yyyy", dtf);
                DateTime testdate = DateTime.ParseExact(t1.Text, "dd/MM/yyyy", dtf);
                if (theopdpatient.InsertBody(Session["CoCode"].ToString().Trim(), txtreg.Text, testdate.ToString("yyyy-MM-dd"), d1.SelectedValue, t3.Text, t4.Text, t5.Text, t6.Text, t7.Text, t8.Text, t9.Text, t10.Text, t11.Text,txtprescripId.Text.Trim(),TextBox1.Text.Trim()) == true)
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "msg", "alert('Patient Clinical Finding Inserted Successfully !');", true);
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "msg", "alert('Error in Inserted Data to Patient Clinical Finding !');", true);
                }
            }
            else
                break;
        }
        GridFillBody();
        ResetBody();
    }

    public void GridFillBody()
    {
        GridView4.DataSource = theopdpatient.GridFillBody(Session["CoCode"].ToString().Trim(), txtreg.Text);
        GridView4.DataBind();
    }

    public void DropDownFill()
    {
        for (int d = 1; d < 30; d = d + 3)
        {
            DropDownList d1 = (DropDownList)Page.FindControl("ctl00$ContentPlaceHolder1$DropDownList" + d.ToString());
            DropDownList d2 = (DropDownList)Page.FindControl("ctl00$ContentPlaceHolder1$DropDownList" + (d + 1).ToString());
            DropDownList d3 = (DropDownList)Page.FindControl("ctl00$ContentPlaceHolder1$DropDownList" + (d + 2).ToString());
            d1.DataSource = theopdpatient.DropdownGroup(Session["CoCode"].ToString().Trim());
            d1.DataTextField = "MedicineGroupName";
            d1.DataValueField = "MedicineGroupID";
            d1.DataBind();
            d1.Items.Insert(0, new ListItem("--Select--", "0"));

            d2.Items.Insert(0, new ListItem("--Select--", "0"));

            d3.Items.Insert(0, new ListItem("--Select--", "0"));

        }

        DropDownList32.DataSource = theopdpatient.DropdowntemplateGroup(Session["CoCode"].ToString().Trim());
        DropDownList32.DataTextField = "PrescriptionGroupName";
        DropDownList32.DataValueField = "RowId";
        DropDownList32.DataBind();
        DropDownList32.Items.Insert(0, new ListItem("--Select--", "0"));

        DropDownList31.Items.Insert(0, new ListItem("--Select--", "0"));


        for (int d = 33; d <=47; d++)
        {
            DropDownList d1 = (DropDownList)Page.FindControl("ctl00$ContentPlaceHolder1$DropDownList" + d.ToString());
            d1.DataSource = theopdpatient.DropdownVaccnation(Session["CoCode"].ToString().Trim());
            d1.DataTextField = "Name";
            d1.DataValueField = "ID";
            d1.DataBind();
            d1.Items.Insert(0, new ListItem("--Select--", "0"));
        }



        for (int d = 48; d <= 52; d++)
        {
            DropDownList d1 = (DropDownList)Page.FindControl("ctl00$ContentPlaceHolder1$DropDownList" + d.ToString());
            d1.DataSource = theopdpatient.DropdownComplain(Session["CoCode"].ToString().Trim());
            d1.DataTextField = "ComplainName";
            d1.DataValueField = "RowId";
            d1.DataBind();
            d1.Items.Insert(0, new ListItem("--Select--", "0"));
        }



        DropDownList53.DataSource = theopdpatient.DropdownUSGGr(Session["CoCode"].ToString().Trim());
        DropDownList53.DataTextField = "GroupName";
        DropDownList53.DataValueField = "GroupCode";
        DropDownList53.DataBind();
        DropDownList53.Items.Insert(0, new ListItem("--Select--", "0"));

        DropDownList54.Items.Insert(0, new ListItem("--Select--", "0"));
        DropDownList55.Items.Insert(0, new ListItem("--Select--", "0"));



    }

    public void SubGroupFill(DropDownList drop1,DropDownList drop2)
    {
         drop2.Items.Clear();
         drop2.DataSource = theopdpatient.DropdownSubGroup(Session["CoCode"].ToString().Trim(), drop1.SelectedValue);
        drop2.DataTextField = "SubGrName";
        drop2.DataValueField = "ID";
        drop2.DataBind();
        drop2.Items.Insert(0, new ListItem("--Select--", "0"));
    }

    public void MedicineFill(DropDownList drop2,DropDownList drop3)
    {
         drop3.Items.Clear();
         drop3.DataSource = theopdpatient.DropdownMedicine(Session["CoCode"].ToString().Trim(), drop2.SelectedValue);
        drop3.DataTextField = "MedicineName";
        drop3.DataValueField = "MedicineID";
        drop3.DataBind();
        drop3.Items.Insert(0, new ListItem("--Select--", "0"));
    }

    public void GetDose(DropDownList drop3,TextBox t)
    {
        DataTable dt = theopdpatient.GetDose(Session["CoCode"].ToString().Trim(), drop3.SelectedValue);
        if (dt.Rows.Count > 0)
            t.Text = dt.Rows[0][0].ToString();
        else
        {
            lblError.ForeColor = System.Drawing.Color.Blue;
            lblError.Text = "No Dose in This Combination";
        }
    }

    public void GetGenericName(DropDownList drop3, TextBox t)
    {
        DataTable dt = theopdpatient.GetGenericName(Session["CoCode"].ToString().Trim(), drop3.SelectedValue);
        if (dt.Rows.Count > 0)
            t.Text = dt.Rows[0][0].ToString();
        else
        {
            lblError.ForeColor = System.Drawing.Color.Blue;
            lblError.Text = "No Dose in This Combination";
        }
    }

    protected void DropDownList1_SelectedIndexChanged(object sender, EventArgs e)
    {
        SubGroupFill(DropDownList1, DropDownList2);
    }
    protected void DropDownList4_SelectedIndexChanged(object sender, EventArgs e)
    {
        SubGroupFill(DropDownList4, DropDownList5);
    }
    protected void DropDownList7_SelectedIndexChanged(object sender, EventArgs e)
    {
        SubGroupFill(DropDownList7, DropDownList8);
    }
    protected void DropDownList10_SelectedIndexChanged(object sender, EventArgs e)
    {
        SubGroupFill(DropDownList10, DropDownList11);
    }
    protected void DropDownList13_SelectedIndexChanged(object sender, EventArgs e)
    {
        SubGroupFill(DropDownList13, DropDownList14);
    }
    protected void DropDownList16_SelectedIndexChanged(object sender, EventArgs e)
    {
        SubGroupFill(DropDownList16, DropDownList17);
    }
    protected void DropDownList19_SelectedIndexChanged(object sender, EventArgs e)
    {
        SubGroupFill(DropDownList19, DropDownList20);
    }
    protected void DropDownList22_SelectedIndexChanged(object sender, EventArgs e)
    {
        SubGroupFill(DropDownList22, DropDownList23);
    }
    protected void DropDownList25_SelectedIndexChanged(object sender, EventArgs e)
    {
        SubGroupFill(DropDownList25, DropDownList26);
    }
    protected void DropDownList28_SelectedIndexChanged(object sender, EventArgs e)
    {
        SubGroupFill(DropDownList28, DropDownList29);
    }




    protected void DropDownList2_SelectedIndexChanged(object sender, EventArgs e)
    {
        MedicineFill(DropDownList2, DropDownList3);
    }
    protected void DropDownList5_SelectedIndexChanged(object sender, EventArgs e)
    {
        MedicineFill(DropDownList5,DropDownList6);
    }
    protected void DropDownList8_SelectedIndexChanged(object sender, EventArgs e)
    {
        MedicineFill(DropDownList8, DropDownList9);
    }
    protected void DropDownList11_SelectedIndexChanged(object sender, EventArgs e)
    {
        MedicineFill(DropDownList11, DropDownList12);
    }
    protected void DropDownList14_SelectedIndexChanged(object sender, EventArgs e)
    {
        MedicineFill(DropDownList14, DropDownList15);
    }
    protected void DropDownList17_SelectedIndexChanged(object sender, EventArgs e)
    {
        MedicineFill(DropDownList17, DropDownList18);
    }
    protected void DropDownList20_SelectedIndexChanged(object sender, EventArgs e)
    {
        MedicineFill(DropDownList20, DropDownList21);
    }
    protected void DropDownList23_SelectedIndexChanged(object sender, EventArgs e)
    {
        MedicineFill(DropDownList23, DropDownList24);
    }
    protected void DropDownList26_SelectedIndexChanged(object sender, EventArgs e)
    {
        MedicineFill(DropDownList26, DropDownList27);
    }
    protected void DropDownList29_SelectedIndexChanged(object sender, EventArgs e)
    {
        MedicineFill(DropDownList29, DropDownList30);
    }




    protected void DropDownList3_SelectedIndexChanged(object sender, EventArgs e)
    {
        GetDose(DropDownList3, TextBox191);
        GetGenericName(DropDownList3,txtGenname1);
    }
    protected void DropDownList6_SelectedIndexChanged(object sender, EventArgs e)
    {
        GetDose(DropDownList6, TextBox192);
        GetGenericName(DropDownList6, txtGenname2);
    }
    protected void DropDownList9_SelectedIndexChanged(object sender, EventArgs e)
    {
        GetDose(DropDownList9, TextBox193);
        GetGenericName(DropDownList9, txtGenname3);
    }
    protected void DropDownList12_SelectedIndexChanged(object sender, EventArgs e)
    {
        GetDose(DropDownList12, TextBox194);
        GetGenericName(DropDownList12, txtGenname4);
    }
    protected void DropDownList15_SelectedIndexChanged(object sender, EventArgs e)
    {
        GetDose(DropDownList15, TextBox195);
        GetGenericName(DropDownList15, txtGenname5);
    }
    protected void DropDownList18_SelectedIndexChanged(object sender, EventArgs e)
    {
        GetDose(DropDownList18, TextBox196);
        GetGenericName(DropDownList18, txtGenname6);
    }
    protected void DropDownList21_SelectedIndexChanged(object sender, EventArgs e)
    {
        GetDose(DropDownList21, TextBox197);
        GetGenericName(DropDownList21, txtGenname7);
    }
    protected void DropDownList24_SelectedIndexChanged(object sender, EventArgs e)
    {
        GetDose(DropDownList24, TextBox198);
        GetGenericName(DropDownList24, txtGenname8);
    }
    protected void DropDownList27_SelectedIndexChanged(object sender, EventArgs e)
    {
        GetDose(DropDownList27, TextBox199);
        GetGenericName(DropDownList27, txtGenname9);
    }
    protected void DropDownList30_SelectedIndexChanged(object sender, EventArgs e)
    {
        GetDose(DropDownList30, TextBox200);
        GetGenericName(DropDownList30, txtGenname10);
    }
    protected void Button15_Click(object sender, EventArgs e)
    {
        System.Globalization.DateTimeFormatInfo dtf = new DateTimeFormatInfo();
        DateTime testdate = DateTime.ParseExact(TextBox202.Text, "dd/MM/yyyy", dtf);
        theopdpatient.InsertPrescription(Session["CoCode"].ToString().Trim(), TextBox201.Text, txtreg.Text, testdate.ToString("yyyy-MM-dd"), TextBox204.Text);
  
        for (int t = 191,d=1; t <=200; t++,d=d+3)
        {
            TextBox t1 = (TextBox)Page.FindControl("ctl00$ContentPlaceHolder1$TextBox" + t.ToString());
            DropDownList d1 = (DropDownList)Page.FindControl("ctl00$ContentPlaceHolder1$DropDownList" + d.ToString());
            DropDownList d2 = (DropDownList)Page.FindControl("ctl00$ContentPlaceHolder1$DropDownList" + (d + 1).ToString());
            DropDownList d3 = (DropDownList)Page.FindControl("ctl00$ContentPlaceHolder1$DropDownList" + (d+2).ToString());
            if (t1.Text != "" && d1.SelectedIndex != 0 && d2.SelectedIndex != 0)
            {

                if (theopdpatient.InsertPrescriptionMap(Session["CoCode"].ToString().Trim(), TextBox201.Text, d1.SelectedValue, d2.SelectedValue, d3.SelectedValue, t1.Text) == true)
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "msg", "alert('Patient Prescription Inserted Successfully !');", true);
 
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "msg", "alert('Error in Patient Investigation Inserted !');", true);
                }
            }
            else
                break;
        }
        GridPrescription();
      //  Resetprescription();
    }

    public void Resetprescription()
    {
        for (int t = 191; t <= 200; t++)
        {
            TextBox t1 = (TextBox)Page.FindControl("ctl00$ContentPlaceHolder1$TextBox" + t.ToString());
            t1.Text = "";
        }
        for(int d=1;d<=30;d++)
        {
            DropDownList d1 = (DropDownList)Page.FindControl("ctl00$ContentPlaceHolder1$DropDownList" + d.ToString());
            d1.SelectedIndex = 0;
        }

    }
    public void GridPrescription()
    {
        GridView5.DataSource = theopdpatient.GetPrescriptiondtls(Session["CoCode"].ToString().Trim(), txtreg.Text,txtprescripId.Text.Trim());
        GridView5.DataBind();
    }
    protected void GridView5_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        GridView5.PageIndex = e.NewPageIndex;
        GridPrescription();
    }




    protected void Button16_Click(object sender, EventArgs e)
    {
        Resetprescription();
    }
    protected void Button17_Click(object sender, EventArgs e)
    {
        GetHearder_Detail();
        ltrReport.Text = rpt.ToString();
    }
    public void GetHearder_Detail()
    {
        DataTable dt = theopdpatient.PatientFill(Session["CoCode"].ToString().Trim(), txtreg.Text, TextBox1.Text);
        rpt.Append("<br/>");
        rpt.Append("<table width='50%' cellspacing=0 border=0 style='border-top:1px solid black;border-left:1px solid black;border-right:1px solid black;border-spacing: 0; padding: 0;font-family:verdana;'>");

        rpt.Append("<tr>");
        rpt.AppendFormat("<td colspan='4' style=' border-bottom: 1px solid black; font-family:Verdana;font-weight:bold;background-color:#D2D2DF;font-size:x-small; text-align:Center'>  Prescription ( {0} ) </td>",DateTime.Now.ToString("dd/MM/yyyy"));
        rpt.Append("</tr'>");
        rpt.Append("<tr>");
        rpt.Append("<td  style='width:5%; font-family:Verdana; border-bottom: 1px solid black;font-weight:bold;background-color:#D2D2B0; font-size:x-small;text-align:left'>Name :</td>");
        rpt.AppendFormat("<td  style='width:7%; font-family:Verdana; border-bottom: 1px solid black; font-size:x-small;text-align:left'>{0}</td>", TextBox203.Text);
        rpt.Append("<td style='width:5%; font-family:Verdana; border-bottom: 1px solid black;font-weight:bold;background-color:#D2D2B0;font-size:x-small; text-align:left'>Age :</td>");
        rpt.AppendFormat("<td  style='width:7%; font-family:Verdana; border-bottom: 1px solid black; font-size:x-small;text-align:left'>{0}</td>", TextBox4.Text);
        rpt.Append("</tr>");


        rpt.Append("<tr>");
        rpt.Append("<td  style='width:5%; font-family:Verdana; border-bottom: 1px solid black;font-weight:bold;background-color:#D2D2B0; font-size:x-small;text-align:left'>Guadian's Name</td>");
        rpt.AppendFormat("<td  style='width:7%; font-family:Verdana; border-bottom: 1px solid black; font-size:x-small;text-align:left'>{0}</td>", dt.Rows[0]["GuadianName"]);
        rpt.Append("<td style='width:5%; font-family:Verdana; border-bottom: 1px solid black;font-weight:bold;background-color:#D2D2B0;font-size:x-small; text-align:left'>Address :</td>");
        rpt.AppendFormat("<td  style='width:7%; font-family:Verdana; border-bottom: 1px solid black; font-size:x-small;text-align:left'>{0}</td>", dt.Rows[0]["Address"]);
        rpt.Append("</tr>");


        rpt.Append("<tr>");
        rpt.Append("<td  style='width:5%; font-family:Verdana; border-bottom: 1px solid black;font-weight:bold;background-color:#D2D2B0; font-size:x-small;text-align:left'>LMP :</td>");
        rpt.AppendFormat("<td  style='width:7%; font-family:Verdana; border-bottom: 1px solid black; font-size:x-small;text-align:left'>{0}</td>", TextBox9.Text);    
        rpt.Append("<td style='width:5%; font-family:Verdana; border-bottom: 1px solid black;font-weight:bold;background-color:#D2D2B0;font-size:x-small; text-align:left'>EDD :</td>");
        rpt.AppendFormat("<td  style='width:7%; font-family:Verdana; border-bottom: 1px solid black; font-size:x-small;text-align:left'>{0}</td>", TextBox10.Text);
        rpt.Append("</tr>");

        rpt.Append("<tr>");
        rpt.Append("<td colspan='2' style='width:12%; font-family:Verdana; border-bottom: 1px solid black;font-weight:bold;background-color:#D2D2DF; font-size:x-small;text-align:center'>Medicine</td>");
        rpt.Append("<td colspan='2'  style=' width:12%; font-family:Verdana; border-bottom: 1px solid black;font-weight:bold;background-color:#D2D2DF;font-size:x-small; text-align:center'>Dose</td>");
        rpt.Append("</tr>");
        for (int t = 191, d = 3; t <= 200; t++, d = d + 3)
        {
            TextBox t1 = (TextBox)Page.FindControl("ctl00$ContentPlaceHolder1$TextBox" + t.ToString());
            DropDownList d1 = (DropDownList)Page.FindControl("ctl00$ContentPlaceHolder1$DropDownList" + d.ToString());
            if (t1.Text != "" && d1.SelectedValue != "0")
            {
                rpt.Append("<tr>");
                rpt.AppendFormat("<td colspan='2' style='font-family:Verdana; font-size:x-small; text-align:center'>{0}</td>", d1.SelectedItem.Text);
                rpt.AppendFormat("<td colspan='2' style=' font-family:Verdana; font-size:x-small; text-align:center'>{0}</td>", t1.Text);
                rpt.Append("</tr >");
            }
            else
                break;

        }
        
        rpt.Append("</table>");

        rpt.Append("<table width='50%' cellspacing=0 border=0 style='border-spacing: 0;border:1px solid black; padding: 0;font-family:verdana;'>");
        rpt.Append("<tr>");
        rpt.AppendFormat("<td style='width:5%; font-family:Verdana;font-size:x-small;text-align:left'>Weight:{0}</td>",64.2);
        rpt.AppendFormat("<td style='width:7%; font-family:Verdana;font-size:x-small;text-align:left'>BP:{0}</td>","230/80");
        rpt.AppendFormat("<td style='width:5%; font-family:Verdana;font-size:x-small;text-align:left'>P:{0}</td>",'+');
        rpt.AppendFormat("<td style='width:5%; font-family:Verdana;font-size:x-small;text-align:left'>E:{0}</td>",'-');
        rpt.Append("</tr>");

        rpt.Append("<tr>");
        rpt.AppendFormat("<td style='width:5%; font-family:Verdana;font-size:x-small;text-align:left'>P:{0}</td>", '-');
        rpt.AppendFormat("<td style='width:5%; font-family:Verdana;font-size:x-small;text-align:left'>A:{0}</td>", '-');
        rpt.AppendFormat("<td style='width:5%; font-family:Verdana;font-size:x-small;text-align:left'>LCB:{0}</td>", TextBox11.Text);
        rpt.Append("</tr>");
        rpt.Append("</table>");

        rpt.Append("<table width='50%' cellspacing=0 border=0 style='border-spacing: 0;border:1px solid black; padding: 0;font-family:verdana;'>");
        rpt.Append("<tr>");
        rpt.Append("<td style='width:5%; font-family:Verdana;font-size:x-small;text-align:left'>Comment:-</td>");
        rpt.AppendFormat("<td colspan='3' style='width:5%; font-family:Verdana;font-size:x-small;text-align:left'>{0}</td>", TextBox204.Text);
        rpt.Append("</tr>");
     
        rpt.Append("<tr>");
        rpt.Append("<td style='width:5%; font-family:Verdana;font-size:x-small;text-align:left'></td>");
        rpt.Append("<td style='width:7%; font-family:Verdana;font-size:x-small; text-align:left'></td>");
        rpt.Append("<td style='width:5%; font-family:Verdana;font-size:x-small;text-align:left'> </td>");
        rpt.Append("<td style='width:7%; font-family:Verdana;font-size:x-small; text-align:center'>......................</td>");
        rpt.Append("</tr>");

        rpt.Append("<tr>");
        rpt.Append("<td style='width:5%; font-family:Verdana;font-size:x-small;text-align:left'></td>");
        rpt.Append("<td style='width:7%; font-family:Verdana;font-size:x-small; text-align:left'></td>");
        rpt.Append("<td style='width:5%; font-family:Verdana;font-size:x-small;text-align:left'> </td>");
        rpt.Append("<td style='width:7%; font-family:Verdana;font-size:x-small; text-align:center'>T.K.KARMAKAR</td>");
        rpt.Append("</tr>");

       rpt.Append("</table>");

       rpt.Append("<hr>");

    }


    protected void GridView1_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        GridView1.PageIndex = e.NewPageIndex;
        GridHistory();
    }
    protected void GridView1_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
    {
        GridView1.EditIndex = -1;
        GridHistory();
    }
    protected void GridView1_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {

        Label lblId = (Label)GridView1.Rows[e.RowIndex].FindControl("lblId");
        theopdpatient.DeleteHistory(Session["CoCode"].ToString().Trim(),lblId.Text);
        lblError.Text = "Deleted Successfully";
        GridHistory();
    }
    protected void GridView1_RowEditing(object sender, GridViewEditEventArgs e)
    {
        GridView1.EditIndex = e.NewEditIndex;
        GridHistory();
    }
    protected void GridView1_RowUpdating(object sender, GridViewUpdateEventArgs e)
    {
        Label lblId = (Label)GridView1.Rows[e.RowIndex].FindControl("lblId");
        TextBox txtdate = (TextBox)GridView1.Rows[e.RowIndex].FindControl("txtdate");
        TextBox txtmens = (TextBox)GridView1.Rows[e.RowIndex].FindControl("txtmens");
        TextBox txtopdtl = (TextBox)GridView1.Rows[e.RowIndex].FindControl("txtopdtl");
        TextBox txtspecial = (TextBox)GridView1.Rows[e.RowIndex].FindControl("txtspecial");
        TextBox txtothers = (TextBox)GridView1.Rows[e.RowIndex].FindControl("txtothers");
        System.Globalization.DateTimeFormatInfo dtf = new DateTimeFormatInfo();
        DateTime testdate = DateTime.ParseExact(txtdate.Text, "dd/MM/yyyy", dtf);
        theopdpatient.UpdateHistory(Session["CoCode"].ToString().Trim(),lblId.Text, testdate.ToString("yyyy-MM-dd"), txtmens.Text, txtopdtl.Text, txtspecial.Text, txtothers.Text);
        lblError.Text = "Updated Successfully";
        GridView1.EditIndex = -1;
        GridHistory();
    }






    protected void GridView3_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
    {
        GridView3.EditIndex = -1;
        GridFillVaccine();
    }
    protected void GridView3_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
         Label lblid = (Label)GridView3.Rows[e.RowIndex].FindControl("lblid");
         theopdpatient.Deletevaccine(Session["CoCode"].ToString().Trim(), lblid.Text);
        lblError.Text = "Deleted Successfully";
        GridFillVaccine();
    }
    protected void GridView3_RowEditing(object sender, GridViewEditEventArgs e)
    {
        GridView3.EditIndex = e.NewEditIndex;
         GridFillVaccine();
    }
    protected void GridView3_RowUpdating(object sender, GridViewUpdateEventArgs e)
    {
         Label lblId = (Label)GridView3.Rows[e.RowIndex].FindControl("lblId");
        TextBox txtdate = (TextBox)GridView3.Rows[e.RowIndex].FindControl("txtdate");
        DropDownList ddlvaccinationName = (DropDownList)GridView3.Rows[e.RowIndex].FindControl("ddlvaccinationName");
        TextBox txtcommenet = (TextBox)GridView3.Rows[e.RowIndex].FindControl("txtcommenet");
        
        System.Globalization.DateTimeFormatInfo dtf = new DateTimeFormatInfo();
        DateTime testdate = DateTime.ParseExact(txtdate.Text, "dd/MM/yyyy", dtf);
        if (ddlvaccinationName.SelectedIndex == 0)
        {
            lblError.ForeColor = System.Drawing.Color.Red;
            lblError.Text = "plz Select Vacination Name";
        }
        else
        {
            theopdpatient.UpdateVaccine(Session["CoCode"].ToString().Trim(), lblId.Text, ddlvaccinationName.SelectedValue, testdate.ToString("yyyy-MM-dd"), txtcommenet.Text);
            lblError.Text = "Updated Successfully";
        }
        GridView3.EditIndex = -1;
        GridFillVaccine();
    }
    protected void GridView3_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        GridView3.PageIndex = e.NewPageIndex;
        GridFillVaccine();
    }





    protected void GridView2_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        GridView2.PageIndex = e.NewPageIndex;
        GridFillInvestigation();
    }
    protected void GridView2_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
    {
        GridView2.EditIndex = -1;
        GridFillInvestigation();
    }
    protected void GridView2_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
         Label lblid = (Label)GridView2.Rows[e.RowIndex].FindControl("lblid");
         theopdpatient.DeleteInvestigation(Session["CoCode"].ToString().Trim(), lblid.Text);
        lblError.Text = "Deleted Successfully";
        GridFillInvestigation();
    }
    protected void GridView2_RowEditing(object sender, GridViewEditEventArgs e)
    {
        GridView2.EditIndex = e.NewEditIndex;
        GridFillInvestigation();
    }
    protected void GridView2_RowUpdating(object sender, GridViewUpdateEventArgs e)
    {
          Label lblid = (Label)GridView2.Rows[e.RowIndex].FindControl("lblid");
        TextBox txtdate = (TextBox)GridView2.Rows[e.RowIndex].FindControl("txtdate");
        TextBox txtinvs = (TextBox)GridView2.Rows[e.RowIndex].FindControl("txtinvs");
        TextBox txtdtls = (TextBox)GridView2.Rows[e.RowIndex].FindControl("txtdtls");

        System.Globalization.DateTimeFormatInfo dtf = new DateTimeFormatInfo();
        DateTime testdate = DateTime.ParseExact(txtdate.Text, "dd/MM/yyyy", dtf);
        theopdpatient.UpdateInvestigation(Session["CoCode"].ToString().Trim(), lblid.Text, testdate.ToString("yyyy-MM-dd"), txtinvs.Text, txtdtls.Text);
        lblError.Text = "Updated Successfully";
        GridView2.EditIndex = -1;
        GridFillInvestigation();
    }


    protected void GridView4_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        GridView4.PageIndex = e.NewPageIndex;
        GridFillBody();
    }
    protected void GridView4_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
         Label lblid = (Label)GridView4.Rows[e.RowIndex].FindControl("lblid");
         theopdpatient.DeleteBody(Session["CoCode"].ToString().Trim(), lblid.Text);
        lblError.Text = "Deleted Successfully";
        GridFillBody();
    }
    protected void GridView4_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
    {
        GridView4.EditIndex = -1;
        GridFillBody();
    }
    protected void GridView4_RowEditing(object sender, GridViewEditEventArgs e)
    {
        GridView4.EditIndex = e.NewEditIndex;
        GridFillBody();
    }
    protected void GridView4_RowUpdating(object sender, GridViewUpdateEventArgs e)
    {
        Label lblid = (Label)GridView4.Rows[e.RowIndex].FindControl("lblid");
        TextBox txtdate = (TextBox)GridView4.Rows[e.RowIndex].FindControl("txtdate");
        DropDownList ddlcomplain = (DropDownList)GridView4.Rows[e.RowIndex].FindControl("ddlcomplain");
        TextBox txtweight = (TextBox)GridView4.Rows[e.RowIndex].FindControl("txtweight");


        TextBox txtbp = (TextBox)GridView4.Rows[e.RowIndex].FindControl("txtbp");
        TextBox txtp = (TextBox)GridView4.Rows[e.RowIndex].FindControl("txtp");
        TextBox txte = (TextBox)GridView4.Rows[e.RowIndex].FindControl("txte");

        TextBox txtchest = (TextBox)GridView4.Rows[e.RowIndex].FindControl("txtchest");
        TextBox txtpa = (TextBox)GridView4.Rows[e.RowIndex].FindControl("txtpa");
        TextBox txtpv = (TextBox)GridView4.Rows[e.RowIndex].FindControl("txtpv");

        TextBox txtfh8 = (TextBox)GridView4.Rows[e.RowIndex].FindControl("txtfh8");
        TextBox txtothers = (TextBox)GridView4.Rows[e.RowIndex].FindControl("txtothers");

        System.Globalization.DateTimeFormatInfo dtf = new DateTimeFormatInfo();
        DateTime testdate = DateTime.ParseExact(txtdate.Text, "dd/MM/yyyy", dtf);
        theopdpatient.UpdateBody(Session["CoCode"].ToString().Trim(), lblid.Text, testdate.ToString("yyyy-MM-dd"), ddlcomplain.SelectedValue, txtweight.Text, txtbp.Text, txtp.Text, txte.Text, txtchest.Text, txtpa.Text, txtpv.Text, txtfh8.Text, txtothers.Text,TextBox1.Text.Trim());
        lblError.Text = "Updated Successfully";
        GridView4.EditIndex = -1;
        GridFillBody();
    }
      protected void GridView5_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
    {
        GridView5.EditIndex = -1;
        GridPrescription();
    }
    protected void GridView5_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
         Label lblid = (Label)GridView5.Rows[e.RowIndex].FindControl("lblid");
         theopdpatient.DeletePrescription(Session["CoCode"].ToString().Trim(), lblid.Text);
        lblError.Text = "Deleted Successfully";
        GridPrescription();
    }
    protected void GridView5_RowEditing(object sender, GridViewEditEventArgs e)
    {
      
        GridView5.EditIndex = e.NewEditIndex;
        GridPrescription();
    }
    protected void GridView5_RowUpdating(object sender, GridViewUpdateEventArgs e)
    {
         Label lblid = (Label)GridView5.Rows[e.RowIndex].FindControl("lblid");
        TextBox txtdate = (TextBox)GridView5.Rows[e.RowIndex].FindControl("txtdate");
        DropDownList ddlgroup = (DropDownList)GridView5.Rows[e.RowIndex].FindControl("ddlgroup");
        DropDownList ddlsub = (DropDownList)GridView5.Rows[e.RowIndex].FindControl("ddlsub");
        DropDownList ddlmed = (DropDownList)GridView5.Rows[e.RowIndex].FindControl("ddlmed");
        TextBox txtdose = (TextBox)GridView5.Rows[e.RowIndex].FindControl("txtdose");

        //System.Globalization.DateTimeFormatInfo dtf = new DateTimeFormatInfo();
        //DateTime testdate = DateTime.ParseExact(txtdate.Text, "dd/MM/yyyy", dtf);
        theopdpatient.UpdatePrescription(Session["CoCode"].ToString().Trim(), lblid.Text, ddlgroup.SelectedValue, ddlsub.SelectedValue, ddlmed.SelectedValue, txtdose.Text,txtprescripId.Text.Trim());
        lblError.Text = "Updated Successfully";
        GridView5.EditIndex = -1;
        GridPrescription();
    }


    protected void GridView5_RowDataBound(object sender, GridViewRowEventArgs e)
    { 

         if (e.Row.RowType == DataControlRowType.DataRow && (e.Row.RowState & DataControlRowState.Edit) == DataControlRowState.Edit)
        {
            // Here you will get the Control you need like:
            DropDownList ddl = (DropDownList)e.Row.FindControl("ddlgroup");
            DropDownList ddl1 = (DropDownList)e.Row.FindControl("ddlsub");
            DropDownList ddl2 = (DropDownList)e.Row.FindControl("ddlmed");

            DataTable dt = theopdpatient.DropdownGroup(Session["CoCode"].ToString().Trim());
            ddl.DataSource = dt;
            ddl.DataTextField = "MedicineGroupName";
            ddl.DataValueField = "MedicineGroupID";
            ddl.DataBind();
            //ddl.Items.Insert(0, new ListItem("--Select--", "0"));

            ddl1.Items.Insert(0, new ListItem("--Select--", "0"));
            ddl2.Items.Insert(0, new ListItem("--Select--", "0"));
        }
    }


    protected void ddlgroup_SelectedIndexChanged(object sender, EventArgs e)
    {
       
        DropDownList ddl1 = (DropDownList)GridView5.Rows[GridView5.EditIndex ].FindControl("ddlgroup");
        DropDownList ddl2 = (DropDownList)GridView5.Rows[GridView5.EditIndex].FindControl("ddlsub");

        ddl2.DataSource = theopdpatient.DropdownSubGroup(Session["CoCode"].ToString().Trim(), ddl1.SelectedValue);
        ddl2.DataTextField = "SubGrName";
        ddl2.DataValueField = "ID";
        ddl2.DataBind();
        ddl2.Items.Insert(0, new ListItem("--Select--", "0"));
    }


    protected void ddlsub_SelectedIndexChanged(object sender, EventArgs e)
    {

        DropDownList ddl1 = (DropDownList)GridView5.Rows[GridView5.EditIndex].FindControl("ddlgroup");
        DropDownList ddl2 = (DropDownList)GridView5.Rows[GridView5.EditIndex].FindControl("ddlsub");
        DropDownList ddl3 = (DropDownList)GridView5.Rows[GridView5.EditIndex].FindControl("ddlmed");
        //ddl1 = (DropDownList)sender;
        //ddl2 = (DropDownList)sender;
        ////GridViewRow gvr = (GridViewRow)ddl1.NamingContainer;

        ddl3.DataSource = theopdpatient.DropdownMedicine(Session["CoCode"].ToString().Trim(), ddl2.SelectedValue);
        ddl3.DataTextField = "MedicineName";
        ddl3.DataValueField = "MedicineID";
        ddl3.DataBind();
        ddl3.Items.Insert(0, new ListItem("--Select--", "0"));
    }


    protected void ddlmed_SelectedIndexChanged(object sender, EventArgs e)
    {

        DropDownList ddl1 = (DropDownList)GridView5.Rows[GridView5.EditIndex].FindControl("ddlgroup");
        DropDownList ddl2 = (DropDownList)GridView5.Rows[GridView5.EditIndex].FindControl("ddlsub");
        DropDownList ddl3 = (DropDownList)GridView5.Rows[GridView5.EditIndex].FindControl("ddlmed");
        TextBox t1 = (TextBox)GridView5.Rows[GridView5.EditIndex].FindControl("txtdose");
        Label lblGenNameEdit = (Label)GridView5.Rows[GridView5.EditIndex].FindControl("lblGenNameEdit");
        //ddl1 = (DropDownList)sender;
        //ddl2 = (DropDownList)sender;
        ////GridViewRow gvr = (GridViewRow)ddl1.NamingContainer;

        DataTable dt = theopdpatient.GetDose(Session["CoCode"].ToString().Trim(), ddl3.SelectedValue);
        if (dt.Rows.Count > 0)
        {
            t1.Text = dt.Rows[0][0].ToString();
        }

        DataTable dt1 = theopdpatient.GetGenericName(Session["CoCode"].ToString().Trim(), ddl3.SelectedValue);
        if (dt1.Rows.Count > 0)
        {
            lblGenNameEdit.Text = dt.Rows[0][0].ToString();
        }
    }

    public void Getfill(DropDownList d1,DropDownList d2,string selet)
    {

        d2.DataSource = theopdpatient.DropdownSubGroup(Session["CoCode"].ToString().Trim(), selet);
        d2.DataTextField = "SubGrName";
        d2.DataValueField = "ID";
        d2.DataBind();
        d2.Items.Insert(0, new ListItem("--Select--", "0"));
    }
    protected void DropDownList31_SelectedIndexChanged(object sender, EventArgs e)
    {
         Panel2.Visible = true;
        Resetprescription();
        DataTable dt = theopdpatient.FillTemplate(Session["CoCode"].ToString().Trim(), DropDownList31.SelectedValue);
        for (int i = 0, d = 1, t = 191; i < dt.Rows.Count; i++,d=d+3,t++)
        {
            TextBox t1 = (TextBox)Page.FindControl("ctl00$ContentPlaceHolder1$TextBox" + t.ToString());
            DropDownList d1 = (DropDownList)Page.FindControl("ctl00$ContentPlaceHolder1$DropDownList" + d.ToString());
            DropDownList d2 = (DropDownList)Page.FindControl("ctl00$ContentPlaceHolder1$DropDownList" + (d+1).ToString());
            d2.DataSource = theopdpatient.DropdownSubGroup(Session["CoCode"].ToString().Trim(), "0");
            d2.DataTextField = "SubGrName";
            d2.DataValueField = "ID";
            d2.DataBind();
            DropDownList d3 = (DropDownList)Page.FindControl("ctl00$ContentPlaceHolder1$DropDownList" + (d + 2).ToString());
            d3.DataSource = theopdpatient.DropdownMedicine(Session["CoCode"].ToString().Trim(), "0");
            d3.DataTextField = "MedicineName";
            d3.DataValueField = "MedicineID";
            d3.DataBind();
            d1.SelectedValue = dt.Rows[i]["MedicineGroupID"].ToString();
            d2.SelectedValue = dt.Rows[i]["SubGroupid"].ToString();
            d3.SelectedValue = dt.Rows[i]["MedicineID"].ToString();
            d2.Items.Insert(0, new ListItem("--Select--", "0"));
            d3.Items.Insert(0, new ListItem("--Select--", "0"));
            t1.Text = dt.Rows[i]["DoseName"].ToString();

        }

    }


    protected void DropDownList32_SelectedIndexChanged(object sender, EventArgs e)
    {
        DropDownList31.DataSource = theopdpatient.Dropdowntemplate(Session["CoCode"].ToString().Trim(), DropDownList32.SelectedValue);
        DropDownList31.DataTextField = "PrescrpTemName";
        DropDownList31.DataValueField = "PrescrpTemID";
        DropDownList31.DataBind();
        DropDownList31.Items.Insert(0, new ListItem("--Select--", "0"));
    }
    protected void GridView3_RowDataBound(object sender, GridViewRowEventArgs e)
    {

        if (e.Row.RowType == DataControlRowType.DataRow && (e.Row.RowState & DataControlRowState.Edit) == DataControlRowState.Edit)
        {
            DropDownList ddl = (DropDownList)e.Row.FindControl("ddlvaccinationName");
            Label lblvcid = (Label)e.Row.FindControl("lblvcid");
            Label lbldate = (Label)e.Row.FindControl("lbldate");
            
            DataTable dt = theopdpatient.DropdownVaccnation(Session["CoCode"].ToString().Trim());
            ddl.DataSource = dt;
            ddl.DataTextField = "Name";
            ddl.DataValueField = "ID";
            ddl.DataBind();
            ddl.Items.Insert(0, new ListItem("--Select--", "0"));

            ddl.SelectedValue = lblvcid.Text.Trim();
           
        }
    }
    protected void Button19_Click(object sender, EventArgs e)
    {
        TextBox205.Text = "";
    }
    protected void Button18_Click(object sender, EventArgs e)
    {

        if (Button18.Text == "Submit")
        {
            if (theopdpatient.InsertOPDHospitalNote(Session["CoCode"].ToString().Trim(), txtreg.Text, TextBox205.Text) == true)
            {
                lblError.ForeColor = System.Drawing.Color.Green;
                lblError.Text = "Inserted Succesfully..";
                TextBox205.Text = "";
            }
            else
            {
                lblError.ForeColor = System.Drawing.Color.Red;
                lblError.Text = "Error in Inserted Data..";
            }
        }
        else
        {
            if (theopdpatient.UpdateOPDHospitalNote(Session["CoCode"].ToString().Trim(), txtreg.Text, TextBox205.Text) == true)
            {
                lblError.ForeColor = System.Drawing.Color.Green;
                lblError.Text = "Updated Succesfully..";
                TextBox205.Text = "";
            }
            else
            {
                lblError.ForeColor = System.Drawing.Color.Red;
                lblError.Text = "Error in Updated Data..";
            }
        }
    }
    protected void GridView4_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        
         if (e.Row.RowType == DataControlRowType.DataRow && (e.Row.RowState & DataControlRowState.Edit) == DataControlRowState.Edit)
        {
            // Here you will get the Control you need like:
            DropDownList ddl = (DropDownList)e.Row.FindControl("ddlcomplain");
          
            DataTable dt = theopdpatient.DropdownComplain(Session["CoCode"].ToString().Trim());
            ddl.DataSource = dt;
            ddl.DataTextField = "ComplainName";
            ddl.DataValueField = "RowId";
            ddl.DataBind();
            ddl.Items.Insert(0, new ListItem("--Select--", "0"));
        }
    }

    protected void DropDownList55_SelectedIndexChanged(object sender, EventArgs e)
    {
        Gridview11.DataSource = theopdpatient.GetRadiology(Session["CoCode"].ToString().Trim(), DropDownList55.SelectedValue);
        Gridview11.DataBind();


        Gridview12.DataSource = theopdpatient.GetRadiologyParameter(Session["CoCode"].ToString().Trim(), DropDownList55.SelectedValue);
        Gridview12.DataBind();
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
    protected void Button20_Click(object sender, EventArgs e)
    {
                
                System.Globalization.DateTimeFormatInfo dtf = new DateTimeFormatInfo();
                DateTime testdate = DateTime.ParseExact(txtdate.Text, "dd/MM/yyyy", dtf);
                DataTable dt12 = theopdpatient.GenerateRadiologyCode(Session["CoCode"].ToString().Trim(),Session["YearCode"].ToString().Trim());
                DataTable dtdoc = theopdpatient.GetDoctorID(Session["CoCode"].ToString().Trim(), txtreg.Text);
                theopdpatient.InsertRadiology(Session["CoCode"].ToString().Trim(), Session["YearCode"].ToString().Trim(), dt12.Rows[0][0].ToString(), DropDownList55.SelectedValue, txtreg.Text, null, dtdoc.Rows[0][0].ToString(), null, null, Session["userName"].ToString(), testdate.ToString("yyyy-MM-dd"));
                for (int i = 0; i < Gridview11.Rows.Count; i++)
                {
                    Label lblID = (Label)Gridview11.Rows[i].FindControl("lblID");
                    TextBox txtheaderContent = (TextBox)Gridview11.Rows[i].FindControl("txtheaderContent");
                    theopdpatient.InsertHeader(Session["CoCode"].ToString().Trim(), Session["YearCode"].ToString().Trim(), dt12.Rows[0][0].ToString(), lblID.Text, txtheaderContent.Text);

                }

                for (int k = 0; k < Gridview12.Rows.Count; k++)
                {

                    Label lblID = (Label)Gridview12.Rows[k].FindControl("lblID");
                    TextBox txtval = (TextBox)Gridview12.Rows[k].FindControl("txtval");
                    theopdpatient.InsertParameter(Session["CoCode"].ToString().Trim(), Session["YearCode"].ToString().Trim(), dt12.Rows[0][0].ToString(), lblID.Text, txtval.Text);
                }



                ScriptManager.RegisterStartupScript(this, this.GetType(), "msg", "alert('Inserted Successfully !');", true);
        
    }
    protected void Button21_Click(object sender, EventArgs e)
    {

    }
    protected void ButChk_Click(object sender, EventArgs e)
    {
        int check;
        if (Chk.Checked == true)
        {
            check = 1;
        }
        else { check = 0; }
        if (theopdpatient.CheckPatient(Session["CoCode"].ToString().Trim(), Session["YearCode"].ToString().Trim(), check, txtreg.Text.Trim(), TextBox1.Text.Trim()) == true)
        {
            lblError.ForeColor = System.Drawing.Color.Green;
            lblError.Text = "Patient Checked..";
            TextBox205.Text = "";
        }
        else
        {
            lblError.ForeColor = System.Drawing.Color.Red;
            lblError.Text = "Error in Checking..";
        }
    }

    protected void btnSaveDiagnosis_Click(object sender, EventArgs e)
    {
        if (btnSaveDiagnosis.Text == "Submit")
        {
            if (txtreg.Text == "")
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "msg", "alert('Enter Patient Registration No !');", true);
            }
            else if (TextBox1.Text == "")
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "msg", "alert('Enter Patient Appointment No !');", true);
            }
            else if (txtDiagDate.Text == "")
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "msg", "alert('Enter Diagnosis Date !');", true);
            }
            else if (txtDiagnosis.Text == "")
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "msg", "alert('Enter Diagnosis !');", true);
            }
            else
            {
                if (theopdpatient.SaveDiagnosis(Session["CoCode"].ToString().Trim(), Session["YearCode"].ToString().Trim(), txtreg.Text.Trim(), TextBox1.Text.Trim(), txtDiagDate.Text.Trim(), txtDiagnosis.Text.Trim(),txtprescripId.Text.Trim()) == true)
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "msg", "alert('Record Successfully Saved !');", true);
                    GetDiagnosis();
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "msg", "alert('Record cannot be saved!');", true);
                }
            }
        }
        else
        {
            if (txtreg.Text == "")
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "msg", "alert('Enter Patient Registration No !');", true);
            }
            else if (lblAppNoOld.Text == "")
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "msg", "alert('Enter Patient Appointment No !');", true);
            }
            else if (txtDiagDate.Text == "")
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "msg", "alert('Enter Diagnosis Date !');", true);
            }
            else if (txtDiagnosis.Text == "")
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "msg", "alert('Enter Diagnosis !');", true);
            }
            else
            {
                if (theopdpatient.UpdateDiagnosis(Session["CoCode"].ToString().Trim(), Session["YearCode"].ToString().Trim(), txtreg.Text.Trim(), TextBox1.Text.Trim(), txtDiagDate.Text.Trim(), txtDiagnosis.Text.Trim(), txtprescripId.Text.Trim()) == true)
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "msg", "alert('Record Successfully Updated !');", true);
                    lblAppNoOld.Text = "";
                    txtDiagDate.Text = "";
                    txtDiagnosis.Text = "";
                    GetDiagnosis();
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "msg", "alert('Record cannot be updated!');", true);
                }
            }
        }
    }
    protected void btnCancelDiagnosis_Click(object sender, EventArgs e)
    {
        txtDiagnosis.Text = "";
        txtDiagDate.Text = txtCheckedDate.Text;
    }
    protected void GridView6_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        GridView6.PageIndex = e.NewPageIndex;
        GetDiagnosis();
    }
    protected void GridView6_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "Select")
        {
            int index = Convert.ToInt32(e.CommandArgument);
            Label lbldate = (Label)GridView6.Rows[index].FindControl("lbldate");
            Label lblAppNo = (Label)GridView6.Rows[index].FindControl("lblAppNo");
            Label lblDiagnosis = (Label)GridView6.Rows[index].FindControl("lblDiagnosis");

            lblAppNoOld.Text = lblAppNo.Text;
            txtDiagDate.Text = lbldate.Text;
            txtDiagnosis.Text = lblDiagnosis.Text;
            btnSaveDiagnosis.Text = "Update";
        }
    }
}