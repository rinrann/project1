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

using iTextSharp.text;
using iTextSharp.text.pdf;
using iTextSharp.text.html;
using iTextSharp.text.html.simpleparser;
using System.IO;
using System.Web.UI.HtmlControls;
using System.Data;
using System.Data.SqlClient;  

public partial class DayCare_DCDischarge : System.Web.UI.Page
{
    Main Objmain = new Main(ConfigurationSettings.AppSettings["ConnectionString"].ToString());
    DCDischargedtls thechemicallist = new DCDischargedtls(ConfigurationSettings.AppSettings["ConnectionString"].ToString());
    DC_DialysisPayment thepdia = new DC_DialysisPayment(ConfigurationSettings.AppSettings["ConnectionString"].ToString());
    DC_AppoinmentList thepatientAppo = new DC_AppoinmentList(ConfigurationSettings.AppSettings["ConnectionString"].ToString());
    System.Text.StringBuilder rpt = new System.Text.StringBuilder();
   
    protected void Page_Load(object sender, EventArgs e)
    {
        Page.Title = "Check out";
        if (Session["userName"] == null)
        {
            Response.Redirect("../LoginPage.aspx");
        }
        if (Objmain.checkaccss(Session["CoCode"].ToString(), Session["UserRoleID"].ToString().Trim(), "DISCHARGE DETAILS", checkAccessType.ViewAction) == false)
        {
            Response.Redirect("../AccessDenied.aspx");
        }

        if (Objmain.checkaccss(Session["CoCode"].ToString(), Session["UserRoleID"].ToString().Trim(), "DISCHARGE DETAILS", checkAccessType.InsertAction) == false)
        {
            Button1.Enabled = false;
        }
        if (!IsPostBack)
        {
            TextBox8.Enabled = false;
            TextBox9.Enabled = false;
            Button5.Enabled = false;
            Button6.Enabled = false;
           // Button7.Enabled = false;
            Panel1.Visible = false;
          // Panel2.Visible = false;
            Tab1Func();
            string aa = txtreg.ClientID;
            if (Session["RegnNo"] != null)
            {
                txtreg.Text = Session["RegnNo"].ToString();
                CallFunc();               
            }
           
        }
        Session["RegnNo"] = null;


    }
    private void ResetAllFields()
    {
        TextBox1.Text = ""; txtreg.Text = "";
        TextBox2.Text = ""; TextBox3.Text = "";
        TextBox4.Text = ""; TextBox5.Text = "";
        HiddenField1.Value = "";
        Button1.Text = "Submit";
        if (Objmain.checkaccss(Session["CoCode"].ToString(), Session["UserRoleID"].ToString().Trim(), "DISCHARGE DETAILS", checkAccessType.InsertAction) == false)
        {
            Button1.Enabled = false;
        }

    }
    private void ResetFieldAfterSubmit()
    {
        TextBox1.Text = "";
        TextBox2.Text = "";
        TextBox3.Text = "";
        TextBox8.Text = "";
        TextBox9.Text = "";
    }
    private void GridFill()
    {
        DataTable dt = thechemicallist.GridFill(Session["CoCode"].ToString().Trim(), txtreg.Text);
        if (dt.Rows.Count > 0)
        {
            TextBox6.Text = dt.Rows[0]["PatientReg"].ToString();
            TextBox7.Text = dt.Rows[0]["patient_name"].ToString();
            GridView1.DataSource = dt;
            GridView1.DataBind();
        }

        GridView1.SelectedIndex = -1;
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

            Label PatientName = (Label)GridView1.Rows[index].FindControl("PatientName");
            TextBox4.Text = PatientName.Text;

            Label DDate = (Label)GridView1.Rows[index].FindControl("DDate");
            TextBox1.Text = DDate.Text;

            Label DischargeTime = (Label)GridView1.Rows[index].FindControl("DischargeTime");
            TextBox2.Text = DischargeTime.Text;


            Label Comment = (Label)GridView1.Rows[index].FindControl("Comment");
            TextBox3.Text = Comment.Text;

            Tab1Func();
            Button1.Text = "Update";
            if (Objmain.checkaccss(Session["CoCode"].ToString(), Session["UserRoleID"].ToString().Trim(), "DISCHARGE DETAILS", checkAccessType.UpdateAction) == false)
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
        if (thechemicallist.DeleteDayOff(Session["CoCode"].ToString().Trim(), lblID.Text) == true)
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
        string  testdate1;
        string flag="0";

        int dueflag = 0;
        // check is  due amount

        DataTable dtcheck = thepatientAppo.PatientDashBoard(Session["CoCode"].ToString().Trim(), "", "", "", txtreg.Text.Trim());
        if (dtcheck.Rows.Count > 0)
        {
            if (Convert.ToDouble(dtcheck.Rows[0]["Due"]) == 0)
                dueflag = 1;
            else
                dueflag = 0;
        }
        else
        {
            dueflag = 0;
        }
        if (CheckBox1.Checked == true)
            flag = "1";
        else
            flag = "0";
        System.Globalization.DateTimeFormatInfo dtf = new DateTimeFormatInfo();
        DateTime testdate = DateTime.ParseExact(TextBox1.Text, "dd/MM/yyyy", dtf);

        if (TextBox8.Text != "")
            testdate1 = DateTime.ParseExact(TextBox8.Text, "dd/MM/yyyy", dtf).ToString("yyyy-MM-dd");
        else
            testdate1 = "";
        if (dueflag==1)
        {
            if (Button1.Text == "Submit")
            {

                if (thechemicallist.InsertDischarge(Session["CoCode"].ToString().Trim(), HiddenField2.Value, txtreg.Text, TextBox4.Text, testdate.ToString("yyyy-MM-dd"), TextBox2.Text, TextBox3.Text, flag, testdate1, TextBox9.Text) == true)
                {
                    Button1.Enabled = false;
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "msg", "alert('Insered Successfully !');", true);
                    Button5.Enabled = true;
                    Button6.Enabled = true;
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "msg", "alert('Error Insered Data !');", true);
                    Button5.Enabled = false;
                    Button6.Enabled = false;
                } 
            }
            else
            {

                if (thechemicallist.UpdateDayOff(Session["CoCode"].ToString().Trim(), HiddenField1.Value, testdate.ToString(), TextBox2.Text, TextBox3.Text) == true)
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "msg", "alert('Updated Successfully !');", true);
                    Button5.Enabled = true;
                    Button6.Enabled = true;
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "msg", "alert('Error Updated Data !');", true);
                    Button5.Enabled = false;
                    Button6.Enabled = false;
                }
                Button1.Text = "Submit";
            }
        }
        else
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "msg", "alert('Please Balance your Due Amount !');", true);
        }

        GridFill();
      //  ResetAllFields();
        ResetFieldAfterSubmit();
      

    }
    protected void Button2_Click(object sender, EventArgs e)
    {
        ResetAllFields();
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
    public void Tab1Func()
    {
        Tab1.CssClass = "Clicked";
        Tab2.CssClass = "Initial";
        MainView.ActiveViewIndex = 0;
    }
    public void CallFunc()
    {

        DataTable dt = thechemicallist.GetPatient(Session["CoCode"].ToString().Trim(), txtreg.Text);
        if (dt.Rows.Count > 0)
        {
            TextBox4.Text = dt.Rows[0]["patient_name"].ToString();
            TextBox5.Text = dt.Rows[0]["BedNoText"].ToString();
            HiddenField2.Value = dt.Rows[0]["bedallocation"].ToString();
        }
    }
    protected void Button4_Click(object sender, EventArgs e)
    {
        CallFunc();
       
        GridFill();
    }
    protected void CheckBox1_CheckedChanged(object sender, EventArgs e)
    {
        if (CheckBox1.Checked == true)
        {
            TextBox8.Enabled = true;
            TextBox9.Enabled = true;
        }
        else
        {
            TextBox8.Enabled = false;
            TextBox9.Enabled = false;
        }
    }
    protected void Button2_Click1(object sender, EventArgs e)
    {
        ResetAllFields();
    }
    protected void Button6_Click(object sender, EventArgs e)
    {
        Session["RegNo"] = txtreg.Text;
        Response.Redirect("PatientAppointment.aspx");
    }


    public void GetReport()
    {
        Report_Header();
        GetHearder_Detail();
        ltrReport.Text = rpt.ToString();
    }

    public void GetReport1()
    {
        Report_Header();
        GetHearder_Detail1();
        ltrReport.Text = rpt.ToString();
    }


    public void Report_Header()
    {
        rpt.Append("<table width='100%' cellspacing=0 border=0 style='border:1px solid black;border-spacing: 0; padding: 0;font-family:Arial;'>");
        rpt.Append("<tr cellpadding:'0'  style='height:30px;'>");
        rpt.Append("<td rowspan='3' width='2%' style='font-family:Arial; font-size:x-large; font-weight:bold;padding-left:100px;text-align:left'><img src='/Images/gfclogo.png'  style='height:77px;width:75px;'/></td>");
        rpt.Append("<td   width='20%'  style='height:20px;font-family:Arial; font-size:x-large; font-weight:bold;text-align:center'><u>GFC HOSPITAL</u></td>");
        rpt.Append("<td rowspan='3'  width='2%' style='height:20px;font-family:Arial; font-size:x-large;padding-right:100px; font-weight:bold;text-align:right'><img src='/Images/gfclogo.png'  style='height:77px;width:75px;'/></td>");
        rpt.Append("</tr>");

        rpt.Append("<tr  style='height:20px;'>");
        rpt.AppendFormat("<td   width='20%'  style='height:10px;font-family:Arial; font-size:small; font-weight:bold;text-align:center'>(Regn. No : NH-315/G-70/2013)</td>");
        rpt.Append("</tr>");

        rpt.Append("<tr  style='height:10px;'>");
        rpt.AppendFormat("<td  width='70%'  style='height:15px;font-family:Arial; font-size:medium; font-weight:bold;text-align:center'>Kushpata, Ghatal, Paschim Medinipur, WB, 721212</td>");

        rpt.Append("</tr>");
        rpt.Append("</table>");
    }


    public void GetHearder_Detail()
    {
        ltrReport.Text = "";
        DataTable dt = thechemicallist.GetPatientDtls(Session["CoCode"].ToString().Trim(), txtreg.Text);
        rpt.Append("<br/>");
        rpt.Append("<table width='100%' cellspacing=0 border=0 style='border:1px solid black;border-spacing: 0; padding: 0;font-family:verdana;'>");
        rpt.Append("<tr style='height:40px'>");
        rpt.Append("<td colspan='6' style='width: 8%;border-bottom: 1px solid black; font-family:Verdana;font-weight:bold;background-color:#9B9C8D;font-size:larger; text-align:Center'> ডিসচার্জ সার্টিফিকেট  </td>");
        rpt.Append("</tr'>");
        rpt.Append("<tr style='height:40px'>");
        rpt.Append("<td colspan='6' style='width: 8%;border-bottom: 1px solid black; font-family:Verdana;font-weight:bold;background-color:#9B9C8D;font-size:medium; text-align:Center'>রোগীর বিবরণ </td>");
        rpt.Append("</tr'>");

        rpt.Append("<tr style='height:30px'>");
        rpt.Append("<td style='width: 8%;border-bottom: 1px solid black;border-right: 1px solid black; font-family:Verdana;font-weight:bold;background-color:beige; font-size:small; text-align:left'>নিবন্ধ সংখ্যা :</td>");
        rpt.AppendFormat("<td style='width: 5%;border-bottom: 1px solid black;border-right: 1px solid black; font-family:Verdana; font-size:small;text-align:left'>{0}</td>", dt.Rows[0]["PatientReg"]);
        rpt.Append("<td style='width: 8%;border-bottom: 1px solid black;border-right: 1px solid black; font-family:Verdana;font-weight:bold;background-color:beige; font-size:small; text-align:left'>রোগীর নাম :</td>");
        rpt.AppendFormat("<td style='width: 5%;border-bottom: 1px solid black;border-right: 1px solid black; font-family:Verdana; font-size:small;text-align:left'>{0}</td>", dt.Rows[0]["patient_name"]);
        rpt.Append("<td style='width: 8%;border-bottom: 1px solid black;border-right: 1px solid black; font-family:Verdana;font-weight:bold;background-color:beige; font-size:small; text-align:left'>রোগীর বয়স :</td>");
        rpt.AppendFormat("<td style='width: 5%;border-bottom: 1px solid black;font-family:Verdana; font-size:small;text-align:left'>{0} yrs.</td>", dt.Rows[0]["age"]);

        rpt.Append("</tr >");

        rpt.Append("<tr style='height:30px'>");
        rpt.Append("<td style='width: 8%;border-bottom: 1px solid black;border-right: 1px solid black; font-family:Verdana;font-weight:bold;background-color:beige; font-size:small; text-align:left'>অভিভাবক নাম :</td>");
        rpt.AppendFormat("<td style='width: 5%;border-bottom: 1px solid black;border-right: 1px solid black; font-family:Verdana; font-size:small;text-align:left'>{0}</td>", dt.Rows[0]["guardian_name"]);
        rpt.Append("<td style='width: 8%;border-bottom: 1px solid black;border-right: 1px solid black; font-family:Verdana;font-weight:bold;background-color:beige; font-size:small; text-align:left'>রোগীর ঠিকানা :</td>");
        rpt.AppendFormat("<td style='width: 5%;border-bottom: 1px solid black;border-right: 1px solid black; font-family:Verdana; font-size:small;text-align:left'>{0}</td>", dt.Rows[0]["vill_city"]);
        rpt.Append("<td style='width: 8%;border-bottom: 1px solid black;border-right: 1px solid black; font-family:Verdana;font-weight:bold;background-color:beige; font-size:small; text-align:left'>যোগাযোগ নম্বর :</td>");
        rpt.AppendFormat("<td style='width: 5%;border-bottom: 1px solid black;font-family:Verdana; font-size:small;text-align:left'>{0}</td>", dt.Rows[0]["PhNo1"]);
        rpt.Append("</tr >");


        rpt.Append("<tr style='height:30px'>");
        rpt.Append("<td style='width: 8%; border-right: 1px solid black; font-family:Verdana;font-weight:bold;background-color:beige; font-size:small; text-align:left'>ডাকঘর :</td>");
        rpt.AppendFormat("<td style='width: 5%; border-right: 1px solid black; font-family:Verdana; font-size:small;text-align:left'>{0}</td>", dt.Rows[0]["po"]);
        rpt.Append("<td style='width: 8%; border-right: 1px solid black; font-family:Verdana;font-weight:bold;background-color:beige; font-size:small; text-align:left'>থানা :</td>");
        rpt.AppendFormat("<td style='width: 5%; border-right: 1px solid black; font-family:Verdana; font-size:small;text-align:left'>{0}</td>", dt.Rows[0]["ps"]);
        rpt.Append("<td style='width: 8%; border-right: 1px solid black; font-family:Verdana;font-weight:bold;background-color:beige; font-size:small; text-align:left'>জেলা :</td>");
        rpt.AppendFormat("<td style='width: 5%;font-family:Verdana; font-size:small;text-align:left'>{0}</td>", dt.Rows[0]["DistrictName"]);
        rpt.Append("</tr >");
        rpt.Append("</table>");


        rpt.Append("<br />");
        rpt.Append("<table width='100%' cellspacing=0 border=0 style='border:1px solid black;border-spacing: 0; padding: 0;font-family:verdana;'>");
 
 
            rpt.Append("<tr style='height:30px'>");
            rpt.Append("<td style='width: 8%; border-right: 1px solid black; font-family:Verdana;font-weight:bold;background-color:beige; font-size:small; text-align:left'>নিষ্কাশন তারিখ :</td>");
            rpt.AppendFormat("<td style='width: 5%; border-right: 1px solid black; font-family:Verdana; font-size:small;text-align:left'>{0}</td>", dt.Rows[0]["DDate"]);
            rpt.Append("<td style='width: 8%; border-right: 1px solid black; font-family:Verdana;font-weight:bold;background-color:beige; font-size:small; text-align:left'>নিষ্কাশন টাইম :</td>");
            rpt.AppendFormat("<td style='width: 5%; font-family:Verdana; font-size:small;text-align:left'>{0}</td>", dt.Rows[0]["DischargeTime"]);
            rpt.Append("</tr >");
     

        rpt.Append("</table >");

         rpt.Append("<br />");
        rpt.Append("<table width='100%' cellspacing=0 border=0 style='border:1px solid black;border-spacing: 0; padding: 0;font-family:verdana;'>");

        rpt.Append("<tr style='height:30px'>");
        rpt.Append("<td style='width:3%; border-right: 1px solid black; font-family:Verdana;font-weight:bold;background-color:beige; font-size:small; text-align:left'>ক্লিনিক ডায়াগনস্টিক   :</td>");
        rpt.AppendFormat("<td style='width: 15%; padding-left:30px;font-family:Verdana; font-size:small;text-align:left'>{0}</td>","Hemo Dialysis");
        rpt.Append("</tr >");
      
            rpt.Append("<tr style='height:30px'>");
            rpt.Append("<td style='width: 3%; border-right: 1px solid black; font-family:Verdana;font-weight:bold;background-color:beige; font-size:small; text-align:left'>পরিস্থিতি নিষ্কাশন :</td>");
            rpt.AppendFormat("<td style='width: 15%;padding-left:30px; font-family:Verdana; font-size:small;text-align:left'>{0}</td>", dt.Rows[0]["Remarks"]);
            rpt.Append("</tr >");
       

        rpt.Append("</table >");



        rpt.Append("<br />"); rpt.Append("<br />");
        rpt.Append("<table width='100%' cellspacing=0 border=0 style='border-spacing: 0; padding: 0;font-family:verdana;'>");
        rpt.Append("<tr style='height:30px'>");
        rpt.Append("<td style='font-family:Verdana;font-weight:bold;font-size:small; text-align:left'>ডঃ টি কে কর্মকার</td>");
        rpt.Append("</tr >");

        rpt.Append("<tr style='height:30px'>");
        rpt.Append("<td style='font-family:Verdana;font-weight:bold;font-size:small; text-align:left'>MBBS,DGO,MD,DNB,MNAMS,FICMCH,FICOG,MBA,Ph.D</td>");
        rpt.Append("</tr >");

        rpt.Append("<tr style='height:30px'>");
        rpt.Append("<td style='font-family:Verdana;font-weight:bold;font-size:small; text-align:left'>Regn. WNMC 46599</td>");
        rpt.Append("</tr >");
        rpt.Append("</table >");
        ltrReport.Visible = true;


    }


    public void GetHearder_Detail1()
    {
        ltrReport.Text = "";
        DataTable dt = thechemicallist.GetPatientDtls(Session["CoCode"].ToString().Trim(), txtreg.Text);
        rpt.Append("<br/>");
        rpt.Append("<table width='100%' cellspacing=0 border=0 style='border:1px solid black;border-spacing: 0; padding: 0;font-family:verdana;'>");
        rpt.Append("<tr style='height:40px'>");
        rpt.Append("<td colspan='6' style='width: 8%;border-bottom: 1px solid black; font-family:Verdana;font-weight:bold;background-color:#9B9C8D;font-size:larger; text-align:Center'> DISCHARGE CERTIFICATE  </td>");
        rpt.Append("</tr'>");
        rpt.Append("<tr style='height:40px'>");
        rpt.Append("<td colspan='6' style='width: 8%;border-bottom: 1px solid black; font-family:Verdana;font-weight:bold;background-color:#9B9C8D;font-size:medium; text-align:Center'> Patient's Details </td>");
        rpt.Append("</tr'>");

        rpt.Append("<tr style='height:30px'>");
        rpt.Append("<td style='width: 8%;border-bottom: 1px solid black;border-right: 1px solid black; font-family:Verdana;font-weight:bold;background-color:beige; font-size:small; text-align:left'>Registration No :</td>");
        rpt.AppendFormat("<td style='width: 5%;border-bottom: 1px solid black;border-right: 1px solid black; font-family:Verdana; font-size:small;text-align:left'>{0}</td>", dt.Rows[0]["PatientReg"]);
        rpt.Append("<td style='width: 8%;border-bottom: 1px solid black;border-right: 1px solid black; font-family:Verdana;font-weight:bold;background-color:beige; font-size:small; text-align:left'>Patient's Name :</td>");
        rpt.AppendFormat("<td style='width: 5%;border-bottom: 1px solid black;border-right: 1px solid black; font-family:Verdana; font-size:small;text-align:left'>{0}</td>", dt.Rows[0]["patient_name"]);
        rpt.Append("<td style='width: 8%;border-bottom: 1px solid black;border-right: 1px solid black; font-family:Verdana;font-weight:bold;background-color:beige; font-size:small; text-align:left'>Patient's Age :</td>");
        rpt.AppendFormat("<td style='width: 5%;border-bottom: 1px solid black;font-family:Verdana; font-size:small;text-align:left'>{0} yrs.</td>", dt.Rows[0]["age"]);

        rpt.Append("</tr >");

        rpt.Append("<tr style='height:30px'>");
        rpt.Append("<td style='width: 8%;border-bottom: 1px solid black;border-right: 1px solid black; font-family:Verdana;font-weight:bold;background-color:beige; font-size:small; text-align:left'>Guadian's Name :</td>");
        rpt.AppendFormat("<td style='width: 5%;border-bottom: 1px solid black;border-right: 1px solid black; font-family:Verdana; font-size:small;text-align:left'>{0}</td>", dt.Rows[0]["guardian_name"]);
        rpt.Append("<td style='width: 8%;border-bottom: 1px solid black;border-right: 1px solid black; font-family:Verdana;font-weight:bold;background-color:beige; font-size:small; text-align:left'>Address :</td>");
        rpt.AppendFormat("<td style='width: 5%;border-bottom: 1px solid black;border-right: 1px solid black; font-family:Verdana; font-size:small;text-align:left'>{0}</td>", dt.Rows[0]["vill_city"]);
        rpt.Append("<td style='width: 8%;border-bottom: 1px solid black;border-right: 1px solid black; font-family:Verdana;font-weight:bold;background-color:beige; font-size:small; text-align:left'>Contact No :</td>");
        rpt.AppendFormat("<td style='width: 5%;border-bottom: 1px solid black;font-family:Verdana; font-size:small;text-align:left'>{0}</td>", dt.Rows[0]["PhNo1"]);
        rpt.Append("</tr >");


        rpt.Append("<tr style='height:30px'>");
        rpt.Append("<td style='width: 8%; border-right: 1px solid black; font-family:Verdana;font-weight:bold;background-color:beige; font-size:small; text-align:left'>Post :</td>");
        rpt.AppendFormat("<td style='width: 5%; border-right: 1px solid black; font-family:Verdana; font-size:small;text-align:left'>{0}</td>", dt.Rows[0]["po"]);
        rpt.Append("<td style='width: 8%; border-right: 1px solid black; font-family:Verdana;font-weight:bold;background-color:beige; font-size:small; text-align:left'>Police Station :</td>");
        rpt.AppendFormat("<td style='width: 5%; border-right: 1px solid black; font-family:Verdana; font-size:small;text-align:left'>{0}</td>", dt.Rows[0]["ps"]);
        rpt.Append("<td style='width: 8%; border-right: 1px solid black; font-family:Verdana;font-weight:bold;background-color:beige; font-size:small; text-align:left'>District :</td>");
        rpt.AppendFormat("<td style='width: 5%;font-family:Verdana; font-size:small;text-align:left'>{0}</td>", dt.Rows[0]["DistrictName"]);
        rpt.Append("</tr >");
        rpt.Append("</table>");


        rpt.Append("<br />");
        rpt.Append("<table width='100%' cellspacing=0 border=0 style='border:1px solid black;border-spacing: 0; padding: 0;font-family:verdana;'>");

        rpt.Append("<tr style='height:30px'>");
        rpt.Append("<td style='width:5%; border-right: 1px solid black; font-family:Verdana;font-weight:bold;background-color:beige; font-size:small; text-align:left'>Discharge Date :</td>");
        rpt.AppendFormat("<td style='width: 15%; border-right: 1px solid black; font-family:Verdana; font-size:small;text-align:left'>{0}</td>", dt.Rows[0]["DDate"]);
        rpt.Append("<td style='width: 5%; border-right: 1px solid black; font-family:Verdana;font-weight:bold;background-color:beige; font-size:small; text-align:left'>Discharge Time :</td>");
        rpt.AppendFormat("<td style='width: 15%;  font-family:Verdana; font-size:small;text-align:left'>{0}</td>", dt.Rows[0]["DischargeTime"]);
        rpt.Append("</tr >");
 

        rpt.Append("</table >");

       
        rpt.Append("<br />");
        rpt.Append("<table width='100%' cellspacing=0 border=0 style='border:1px solid black;border-spacing: 0; padding: 0;font-family:verdana;'>");

        rpt.Append("<tr style='height:30px'>");
        rpt.Append("<td style='width:3%; border-right: 1px solid black; font-family:Verdana;font-weight:bold;background-color:beige; font-size:small; text-align:left'>Clinic Diagnostic   :</td>");
        rpt.AppendFormat("<td style='width: 15%; padding-left:30px;font-family:Verdana; font-size:small;text-align:left'>{0}</td>", "Hemo Dialysis");
        rpt.Append("</tr >");
      
        rpt.Append("<tr style='height:30px'>");
        rpt.Append("<td style='width: 3%; border-right: 1px solid black; font-family:Verdana;font-weight:bold;background-color:beige; font-size:small; text-align:left'>Discharge Condition :</td>");
            rpt.AppendFormat("<td style='width: 15%;padding-left:30px; font-family:Verdana; font-size:small;text-align:left'>{0}</td>", dt.Rows[0]["Remarks"]);
            rpt.Append("</tr >");
       

        rpt.Append("</table >");



        rpt.Append("<br />"); rpt.Append("<br />");
        rpt.Append("<table width='100%' cellspacing=0 border=0 style='border-spacing: 0; padding: 0;font-family:verdana;'>");
        rpt.Append("<tr style='height:30px'>");
        rpt.Append("<td style='font-family:Verdana;font-weight:bold;font-size:small; text-align:left'>Dr. T. K. Karmakar</td>");
        rpt.Append("</tr >");

        rpt.Append("<tr style='height:30px'>");
        rpt.Append("<td style='font-family:Verdana;font-weight:bold;font-size:small; text-align:left'>MBBS,DGO,MD,DNB,MNAMS,FICMCH,FICOG,MBA,Ph.D</td>");
        rpt.Append("</tr >");

        rpt.Append("<tr style='height:30px'>");
        rpt.Append("<td style='font-family:Verdana;font-weight:bold;font-size:small; text-align:left'>Regn. WNMC 46599</td>");
        rpt.Append("</tr >");
        rpt.Append("</table >");
        ltrReport.Visible = true;


    }
    protected void Button5_Click(object sender, EventArgs e)
    {
        Panel1.Visible = true;
    }
    protected void Button8_Click(object sender, EventArgs e)
    {
        Panel2.Visible = true;
        if (RadioButtonList1.SelectedValue == "With Header")
        {
          //  Button7.Enabled = false;
            Report_Header();
            if (DropDownList1.SelectedIndex == 2)
            {
                GetHearder_Detail1();
            }
            else
            {
                GetHearder_Detail();
            }
        }
        else
        {
          //  Button7.Enabled = true;
            if (DropDownList1.SelectedIndex == 2)
            {
                GetHearder_Detail1();
            }
            else
            {
                GetHearder_Detail();
            }
        }
        ltrReport.Text = rpt.ToString();
    }
    protected void Button7_Click(object sender, EventArgs e)
    {
        try
        {
            if (RadioButtonList1.SelectedValue == "Without Header")
            {
                Response.ContentType = "application/pdf";
                Response.AddHeader("content-disposition", "attachment;filename=MyReport.pdf");
                Response.Cache.SetCacheability(HttpCacheability.NoCache);
                StringWriter sw = new StringWriter();
                HtmlTextWriter hw = new HtmlTextWriter(sw);
                HtmlForm frm = new HtmlForm();
                mydiv.Parent.Controls.Add(frm);
                frm.Attributes["runat"] = "server";
                frm.Controls.Add(mydiv);
                frm.RenderControl(hw);
                StringReader sr = new StringReader(sw.ToString());
                Document pdfDoc = new Document(PageSize.A4, 10f, 10f, 10f, 0f);
                HTMLWorker htmlparser = new HTMLWorker(pdfDoc);
                PdfWriter.GetInstance(pdfDoc, Response.OutputStream);
                pdfDoc.Open();
                htmlparser.Parse(sr);
                pdfDoc.Close();
                Response.Write(pdfDoc);
                Response.End();
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "msg", "alert('With Header PDF can not possible Please select Without header !');", true);
            }
        }
        catch 
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "msg", "alert('Atfirst Generate Report !');", true);
        }
    }
    protected void RadioButtonList1_SelectedIndexChanged(object sender, EventArgs e)
    {
        Panel2.Visible = true;
        if (RadioButtonList1.SelectedValue == "With Header")
        {
            //  Button7.Enabled = false;
            Report_Header();
            if (DropDownList1.SelectedIndex == 2)
            {
                GetHearder_Detail1();
            }
            else
            {
                GetHearder_Detail();
            }
        }
        else
        {
            //  Button7.Enabled = true;
            if (DropDownList1.SelectedIndex == 2)
            {
                GetHearder_Detail1();
            }
            else
            {
                GetHearder_Detail();
            }
        }
        ltrReport.Text = rpt.ToString();
    }
}