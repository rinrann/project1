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
using System.Globalization;

public partial class IPD_Allreport : System.Web.UI.Page
{
    Main Objmain = new Main(ConfigurationSettings.AppSettings["ConnectionString"].ToString());
    AdmissionAllForms thereg = new AdmissionAllForms(ConfigurationSettings.AppSettings["ConnectionString"].ToString());
    Discharge thepatientbill = new Discharge(ConfigurationSettings.AppSettings["ConnectionString"].ToString());
    System.Text.StringBuilder rpt = new System.Text.StringBuilder();
 
    static string language;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["userName"] == null)
        {
            Response.Redirect("../LoginPage.aspx");
        }
        if (Objmain.checkaccss(Session["CoCode"].ToString(), Session["UserRoleID"].ToString().Trim(), "ALL FORM", checkAccessType.ViewAction) == false)
        {
            Response.Redirect("../AccessDenied.aspx");
        }

        if (!IsPostBack)
        {
            language = "1";
            //txtDate.Text = DateTime.Now.ToString("dd/MM/yyyy");
            GridFill();
            DropDownFill();
            Panel1.Visible = false;
            //RadioButtonList1.SelectedValue = "With Header";
            RadioButtonList1.SelectedValue = "0";
        }
        Page.Title = "Admission All Forms";
    }

    public void DropDownFill()
    {
        this.DropDownList4.DataSource = thereg.DropdownbedAll(Session["CoCode"].ToString().Trim());
        this.DropDownList4.DataTextField = "BedNoText";
        this.DropDownList4.DataValueField = "BedNo";
        this.DropDownList4.DataBind();
        this.DropDownList4.Items.Insert(0, new ListItem("--Select--", "0"));
    }


    public void GridFill()
    {
        System.Globalization.DateTimeFormatInfo dtf = new DateTimeFormatInfo();
        string  testdate;
        if (txtDate.Text != "")
            testdate = DateTime.ParseExact(txtDate.Text, "dd/MM/yyyy", dtf).ToString("yyyy-MM-dd");
            //testdate = DateTime.ParseExact(txtDate.Text, "MM/dd/yyyy", dtf).ToString();
        else
            testdate = "";

        GridView1.DataSource = thereg.GridForAllReport(txtname.Text, txtCO.Text, txtAddress.Text, DropDownList4.SelectedValue, testdate.ToString(), Session["CoCode"].ToString().Trim());
        GridView1.DataBind();
    }
    protected void GridView1_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        GridView1.PageIndex = e.NewPageIndex;
        GridFill();
    }
    protected void Button1_Click(object sender, EventArgs e)
    {
        GridFill();
    }
    protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "Select")
        {          
            int index = Convert.ToInt32(e.CommandArgument);
            if (DropDownList5.SelectedIndex == 0)
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "msg", "alert('Please Select Language  !');Calling();", true);
                ltrReport.Visible = false;
                Panel1.Visible = false;
            }
            else
            {
                Label lblregno = (Label)GridView1.Rows[index].FindControl("lblregno");
                CheckBoxList CheckBoxList1 = (CheckBoxList)GridView1.Rows[index].FindControl("CheckBoxList1");
                DropDownList ddl = (DropDownList)GridView1.Rows[index].FindControl("DropDownList6");
                DataSet ds = thepatientbill.PatientForReport(lblregno.Text,Session["CoCode"].ToString().Trim());

                foreach (ListItem listItem in CheckBoxList1.Items)
                {
                    if (listItem.Selected)
                    {
                        if (listItem.ToString() == "Admission Slip")
                        {
                            GetHearder_AdmissionSlip(lblregno.Text);
                            //rpt.Append("<div class='break'></div>");
                        }
                        else
                        {
                            DataTable dt = thepatientbill.GetReportDetails(listItem.Value, Session["CoCode"].ToString().Trim());

                            if (rpt.ToString() != "")
                                rpt.Append("<div class='break'></div>");
                            if (RadioButtonList1.SelectedValue == "0") { Report_Header(); }
                            if (DropDownList5.SelectedValue == "1")
                            {
                                PatientDetails_Bengali(ds);

                                rpt.Append("<table width='100%' cellspacing=0 border=0 style='border:1px solid black;border-spacing: 0; padding: 0;font-family:Verdana;'>");
                                rpt.Append("<tr style='height:25px;'>");
                                rpt.AppendFormat("<td style='font-family:Verdana;padding-left:120px; font-size:x-large;text-align:center'><b>{0}</b>", dt.Rows[0]["FormName"]);
                                rpt.AppendFormat("<td rowspan='2' style='height:20px; font-family:Verdana; font-size:small;text-align:right'><img src='{0}' width='60in' height='60in' /></td>", ds.Tables[0].Rows[0]["path"]);
                                rpt.Append("</tr >");

                                rpt.Append("<tr style='height:20px;'>");
                                rpt.AppendFormat("<td style='font-family:Verdana; font-size:medium;padding-left:120px;text-align:center'>(যিনি ভর্তি করাচ্ছেন তার নাম:{0},সম্পর্ক:{1})</td>", ds.Tables[0].Rows[0]["guardian_name"], ds.Tables[0].Rows[0]["relation"]);
                                rpt.Append("</tr >");
                                rpt.Append("</table>");

                                rpt.Append("<br/>");
                                string[] SplitContext = dt.Rows[0]["FormContext"].ToString().Split('#');

                                rpt.Append("<table width='100%'>");
                                for (int m = 0; m < SplitContext.Length; m++)
                                {

                                    rpt.Append("<tr style='height:25px'>");
                                    rpt.AppendFormat("<td style='font-family:Arial;font-size:medium; text-align:justify'>{0}</td>", SplitContext[m]);
                                    rpt.Append("</tr >");

                                }
                                rpt.Append("</table>");


                                rpt.Append("<table width='100%'>");
                                rpt.Append("<tr style='height:35px'>");
                                rpt.Append("<td style='font-family:Arial; font-size:medium; text-align:center'> স্বাক্ষর/টিপ </td>");
                                rpt.Append("<td style='font-family:Arial; font-size:medium; text-align:center'> পুরো  নাম  </td>");
                                rpt.Append("<td style='font-family:Arial; font-size:medium; text-align:center'> ঠিকানা </td>");
                                rpt.Append("<td style='font-family:Arial; font-size:medium; text-align:center'> সম্পর্ক  </td>");
                                rpt.Append("</tr >");

                                rpt.Append("<tr style='height:27px'>");
                                rpt.Append("<td style='font-family:Arial;  font-size:medium; text-align:center'>..................</td>");
                                rpt.AppendFormat("<td style='font-family:Arial;  font-size:medium; text-align:center'>{0}</td>", ds.Tables[0].Rows[0]["patient_name"]);
                                rpt.AppendFormat("<td style='font-family:Arial; font-size:medium; text-align:center'>{0}</td>", ds.Tables[0].Rows[0]["vill_city"]);
                                rpt.Append("<td style='font-family:Arial; font-size:medium; text-align:center'> নিজে </td>");
                                rpt.Append("</tr >");

                                rpt.Append("<tr style='height:27px'>");
                                rpt.Append("<td style='font-family:Arial;  font-size:medium; text-align:center'>..................</td>");
                                rpt.AppendFormat("<td style='font-family:Arial;  font-size:medium; text-align:center'>{0}</td>", ds.Tables[0].Rows[0]["guardian_name"]);
                                rpt.AppendFormat("<td style='font-family:Arial; font-size:medium; text-align:center'>{0}</td>", ds.Tables[0].Rows[0]["vill_city2"]);
                                rpt.AppendFormat("<td style='font-family:Arial; font-size:medium; text-align:center'> {0} </td>", ds.Tables[0].Rows[0]["relation"]);
                                rpt.Append("</tr >");
                                rpt.Append("</table>");


                                if (listItem.ToString() == "ভর্তির ফর্ম ")
                                {

                                    rpt.Append("<table width='100%' cellspacing=0 border=0 style='padding: 0;font-family:Verdana;'>");
                                    rpt.Append("<tr style='height:35px'>");
                                    rpt.Append("<td style='font-family:Arial;  font-size:medium; text-align:justify'>---------------------------------------------------------------------------------------------------------------------------------------------</td>");
                                    rpt.Append("</tr >");
                                    rpt.Append("</table>");


                                    if (RadioButtonList1.SelectedValue == "0") { Report_Header(); }

                                    rpt.Append("<table width='100%' cellspacing=0 border=0 style='border:1px solid black;border-spacing: 0; padding: 0;font-family:Verdana;'>");
                                    rpt.Append("<tr style='height:25px;'>");
                                    rpt.Append("<td style='font-family:Verdana; font-size:large;text-align:center'><b>রোগীর  ছুটি / অন্যত্র  নিয়ে যাবার সম্মতি পত্র</b>");
                                    rpt.Append("</tr >");
                                    rpt.Append("</table>");

                                    rpt.Append("</br/>");
                                    rpt.Append("<table width='100%' cellspacing=0 border=0 style='padding: 0;font-family:Arial;'>");
                                    rpt.Append("<tr style='height:15px'>");
                                    rpt.Append("<td style='font-family:Arial; font-size:medium; text-align:justify'>১. আমরা রোগী কে সুস্থ  অবস্থায় নিজ দায়িত্বে এখান থেকে নিয়ে যাচ্ছি।</td>");
                                    rpt.Append("</tr >");
                                    rpt.Append("<tr style='height:15px'>");
                                    rpt.Append("<td style='font-family:Arial; font-size:medium; text-align:justify'>২. রোগীকে চিকিৎসার জন্য আমাদের নিজেদের দায়িত্বে ও ইচ্ছায় অন্যত্র নিয়ে যাচ্ছি। </td>");
                                    rpt.Append("</tr >");
                                    rpt.Append("<tr style='height:15px'>");
                                    rpt.Append("<td style='font-family:Arial; font-size:medium; text-align:justify'>৩. রোগীর চিকিৎসার সমস্ত নথি ও পরীক্ষার কাগজপত্র বুঝে পেলাম।</td>");
                                    rpt.Append("</tr >");
                                    rpt.Append("<tr style='height:15px'>");
                                    rpt.Append("<td style='font-family:Arial; font-size:medium; text-align:justify'>৪. ডাঃ বাবুকে নিয়মিত দেখাব ও তাৎক্ষনিক খুব জরুরী সমস্যা হলে সরকারী হাসপাতালে বা অন্যত্র যোগাযোগ করব। </td>");
                                    rpt.Append("</tr >");
                                    rpt.Append("</table>");

                                    rpt.Append("<br/>"); rpt.Append("<br/>");
                                    rpt.Append("<table width='100%' cellspacing=0 border=0 style='padding: 0;font-family:Arial;'>");
                                    rpt.Append("<tr style='height:25px'>");
                                    rpt.Append("<td style='font-family:Arial; font-size:medium; text-align:center'> স্বাক্ষর/টিপ </td>");
                                    rpt.Append("<td style='font-family:Arial; font-size:medium; text-align:center'> পুরো  নাম  </td>");
                                    rpt.Append("<td style='font-family:Arial; font-size:medium; text-align:center'> ঠিকানা </td>");
                                    rpt.Append("<td style='font-family:Arial; font-size:medium; text-align:center'> সম্পর্ক  </td>");
                                    rpt.Append("</tr >");
                                    rpt.Append("</table >");
                                }
                            }
                            else
                            {
                                PatientDetails_English(ds);

                                rpt.Append("<table width='100%' cellspacing=0 border=0 style='border:1px solid black;border-spacing: 0; padding: 0;font-family:Verdana;'>");
                                rpt.Append("<tr style='height:25px;'>");
                                rpt.AppendFormat("<td style='font-family:Verdana; font-size:x-large;text-align:center'><b>{0}</b>", dt.Rows[0]["FormName"]);
                                rpt.AppendFormat("<td rowspan='2' style='height:20px;  font-size:small;text-align:right'><img src='{0}' width='60in' height='60in' /></td>", ds.Tables[0].Rows[0]["path"]);
                                rpt.Append("</tr >");

                                rpt.Append("<tr style='height:20px;'>");
                                rpt.AppendFormat("<td style='font-family:Verdana; font-size:medium;text-align:center'>(Admitted By :{0},Relation :{1})</td>", ds.Tables[0].Rows[0]["guardian_name"], ds.Tables[0].Rows[0]["relation"]);
                                rpt.Append("</tr >");
                                rpt.Append("</table>");

                                rpt.Append("<br/>");
                                string[] SplitContext = dt.Rows[0]["FormContext"].ToString().Split('#');

                                rpt.Append("<table width='100%'>");
                                for (int m = 0; m < SplitContext.Length; m++)
                                {

                                    rpt.Append("<tr style='height:25px'>");
                                    rpt.AppendFormat("<td style='font-family:Arial;font-size:medium; text-align:justify'>{0}</td>", SplitContext[m]);
                                    rpt.Append("</tr >");

                                }
                                rpt.Append("</table>");





                                rpt.Append("<table width='100%'>");
                                rpt.Append("<tr style='height:27px'>");
                                rpt.Append("<td style='font-family:Arial; font-size:medium; text-align:center'> Signature </td>");
                                rpt.Append("<td style='font-family:Arial; font-size:medium; text-align:center'> Full Name  </td>");
                                rpt.Append("<td style='font-family:Arial; font-size:medium; text-align:center'> Address </td>");
                                rpt.Append("<td style='font-family:Arial; font-size:medium; text-align:center'> Relation  </td>");
                                rpt.Append("</tr >");

                                rpt.Append("<tr style='height:27px'>");
                                rpt.Append("<td style='font-family:Arial;  font-size:small; text-align:center'>..................</td>");
                                rpt.AppendFormat("<td style='font-family:Arial;  font-size:small; text-align:center'>{0}</td>", ds.Tables[0].Rows[0]["patient_name"]);
                                rpt.AppendFormat("<td style='font-family:Arial; font-size:small; text-align:center'>{0}</td>", ds.Tables[0].Rows[0]["vill_city"]);
                                rpt.Append("<td style='font-family:Arial; font-size:small; text-align:center'> Self </td>");
                                rpt.Append("</tr >");

                                rpt.Append("<tr style='height:27px'>");
                                rpt.Append("<td style='font-family:Arial;  font-size:small; text-align:center'>..................</td>");
                                rpt.AppendFormat("<td style='font-family:Arial;  font-size:small; text-align:center'>{0}</td>", ds.Tables[0].Rows[0]["guardian_name"]);
                                rpt.AppendFormat("<td style='font-family:Arial; font-size:small; text-align:center'>{0}</td>", ds.Tables[0].Rows[0]["vill_city2"]);
                                rpt.AppendFormat("<td style='font-family:Arial; font-size:small; text-align:center'> {0} </td>", ds.Tables[0].Rows[0]["relation"]);
                                rpt.Append("</tr >");
                                rpt.Append("</table>");


                                if (listItem.ToString() == "Admission Form")
                                {
                                    rpt.Append("<table width='100%' cellspacing=0 border=0 style='padding: 0;font-family:Verdana;'>");
                                    rpt.Append("<tr style='height:32px'>");
                                    rpt.Append("<td style=' font-family:Verdana;  font-size:medium; text-align:justify'>-----------------------------------------------------------------------------------------------------------------------</td>");
                                    rpt.Append("</tr >");
                                    rpt.Append("</table>");

                                    if (RadioButtonList1.SelectedValue == "0") { Report_Header(); }
                                    rpt.Append("<table width='100%' cellspacing=0 border=0 style='border:1px solid black;border-spacing: 0; padding: 0;font-family:Verdana;'>");
                                    rpt.Append("<tr style='height:25px;'>");
                                    rpt.Append("<td style='font-family:Verdana; font-size:large;text-align:center'><b>Discharge / Referral Consent Form</b>");
                                    rpt.Append("</tr >");
                                    rpt.Append("</table>");

                                    rpt.Append("</br/>");
                                    rpt.Append("<table width='100%' cellspacing=0 border=0 style='padding: 0;font-family:Arial;'>");
                                    rpt.Append("<tr style='height:15px'>");
                                    rpt.Append("<td style='font-family:Arial; font-size:medium; text-align:justify'>1. We are taking our physically fit patient at our own responsibility. </td>");
                                    rpt.Append("</tr >");

                                    rpt.Append("<tr style='height:15px'>");
                                    rpt.Append("<td style='font-family:Arial; font-size:medium; text-align:justify'>2. We are taking our patient elsewhere at our own responsibility for better management and care.</td>");
                                    rpt.Append("</tr >");

                                    rpt.Append("<tr style='height:15px'>");
                                    rpt.Append("<td style='font-family:Arial; font-size:medium; text-align:justify'>3. We have received all papers and documents related to treatment and investigation.</td>");
                                    rpt.Append("</tr >");

                                    rpt.Append("<tr style='height:15px'>");
                                    rpt.Append("<td style='font-family:Arial; font-size:medium; text-align:justify'>4. We shall keep contact with the doctor and contact with nearest hospital in case of dire emergency. </td>");
                                    rpt.Append("</tr >");
                                    rpt.Append("</table>");

                                    rpt.Append("<br/>"); rpt.Append("<br/>");

                                    rpt.Append("<table width='100%' cellspacing=0 border=0 style='padding: 0;font-family:Arial;'>");
                                    rpt.Append("<tr style='height:32px'>");
                                    rpt.Append("<td style='width: 5%;Height:70px;  font-family:Arial; font-size:small; text-align:center'> Signature / thumb </td>");
                                    rpt.Append("<td style='width: 5%;Height:70px;  font-family:Arial; font-size:small; text-align:center'> Full Name  </td>");
                                    rpt.Append("<td style='width: 5%;Height:70px;  font-family:Arial; font-size:small; text-align:center'> Address </td>");
                                    rpt.Append("<td style='width: 5%;Height:70px;  font-family:Arial; font-size:small; text-align:center'> Relationship  </td>");
                                    rpt.Append("</tr >");
                                    rpt.Append("</table >");
                                }
                            }          
                           
                        }
                    }
                }

                //  For Highrisk report  ...............................................................

                if (ddl.SelectedIndex != 0)
                {
                    string[] SplitContext1;
                    if (rpt.ToString() != "")
                        rpt.Append("<div class='break'></div>");
                    if (RadioButtonList1.SelectedValue == "0") { Report_Header(); }

                    DataTable dtHighrisk = thepatientbill.GetReportDetails(ddl.SelectedValue, Session["CoCode"].ToString().Trim());

                    if (DropDownList5.SelectedValue == "1")
                    {
                        PatientDetails_Bengali(ds);

                        rpt.Append("<table width='100%' cellspacing=0 border=0 style='border:1px solid black;border-spacing: 0; padding: 0;font-family:Verdana;'>");
                        rpt.Append("<tr style='height:25px;'>");
                        rpt.AppendFormat("<td style='font-family:Verdana;padding-left:120px; font-size:x-large;text-align:center'><b>{0}</b>", dtHighrisk.Rows[0]["FormName"]);
                        rpt.AppendFormat("<td rowspan='2' style='height:20px; font-family:Verdana; font-size:small;text-align:right'><img src='{0}' width='60in' height='60in' /></td>", ds.Tables[0].Rows[0]["path"]);
                        rpt.Append("</tr >");

                        rpt.Append("<tr style='height:20px;'>");
                        rpt.AppendFormat("<td style='font-family:Verdana; font-size:medium;padding-left:120px;text-align:center'>(যিনি ভর্তি করাচ্ছেন তার নাম:{0},সম্পর্ক:{1})</td>", ds.Tables[0].Rows[0]["guardian_name"], ds.Tables[0].Rows[0]["relation"]);
                        rpt.Append("</tr>");
                        rpt.Append("</table>");
                    }
                    else
                    {
                        PatientDetails_English(ds);

                        rpt.Append("<table width='100%' cellspacing=0 border=0 style='border:1px solid black;border-spacing: 0; padding: 0;font-family:Verdana;'>");
                        rpt.Append("<tr style='height:25px;'>");
                        rpt.AppendFormat("<td style='font-family:Verdana; font-size:x-large;text-align:center'><b>{0}</b>", dtHighrisk.Rows[0]["FormName"]);
                        rpt.AppendFormat("<td rowspan='2' style='height:20px;  font-size:small;text-align:right'><img src='{0}' width='60in' height='60in' /></td>", ds.Tables[0].Rows[0]["path"]);
                        rpt.Append("</tr >");

                        rpt.Append("<tr style='height:20px;'>");
                        rpt.AppendFormat("<td style='font-family:Verdana; font-size:medium;text-align:center'>(Admitted By :{0},Relation :{1})</td>", ds.Tables[0].Rows[0]["guardian_name"], ds.Tables[0].Rows[0]["relation"]);
                        rpt.Append("</tr >");
                        rpt.Append("</table>");
                    }
                    rpt.Append("<br/>");
                    SplitContext1 = dtHighrisk.Rows[0]["FormContext"].ToString().Split('#');

                    rpt.Append("<table width='100%'>");
                    for (int m = 0; m < SplitContext1.Length; m++)
                    {
                        rpt.Append("<tr style='height:25px'>");
                        rpt.AppendFormat("<td style='font-family:Arial;font-size:medium; text-align:justify'>{0}</td>", SplitContext1[m]);
                        rpt.Append("</tr >");
                    }
                    rpt.Append("</table>");


                    rpt.Append("<table width='100%'>");
                    rpt.Append("<tr style='height:35px'>");
                    rpt.Append("<td style='font-family:Arial; font-size:medium; text-align:center'> স্বাক্ষর/টিপ </td>");
                    rpt.Append("<td style='font-family:Arial; font-size:medium; text-align:center'> পুরো  নাম  </td>");
                    rpt.Append("<td style='font-family:Arial; font-size:medium; text-align:center'> ঠিকানা </td>");
                    rpt.Append("<td style='font-family:Arial; font-size:medium; text-align:center'> সম্পর্ক  </td>");
                    rpt.Append("</tr >");

                    rpt.Append("<tr style='height:27px'>");
                    rpt.Append("<td style='font-family:Arial;  font-size:medium; text-align:center'>..................</td>");
                    rpt.AppendFormat("<td style='font-family:Arial;  font-size:medium; text-align:center'>{0}</td>", ds.Tables[0].Rows[0]["patient_name"]);
                    rpt.AppendFormat("<td style='font-family:Arial; font-size:medium; text-align:center'>{0}</td>", ds.Tables[0].Rows[0]["vill_city"]);
                    rpt.Append("<td style='font-family:Arial; font-size:medium; text-align:center'> নিজে </td>");
                    rpt.Append("</tr >");

                    rpt.Append("<tr style='height:27px'>");
                    rpt.Append("<td style='font-family:Arial;  font-size:medium; text-align:center'>..................</td>");
                    rpt.AppendFormat("<td style='font-family:Arial;  font-size:medium; text-align:center'>{0}</td>", ds.Tables[0].Rows[0]["guardian_name"]);
                    rpt.AppendFormat("<td style='font-family:Arial; font-size:medium; text-align:center'>{0}</td>", ds.Tables[0].Rows[0]["vill_city2"]);
                    rpt.AppendFormat("<td style='font-family:Arial; font-size:medium; text-align:center'> {0} </td>", ds.Tables[0].Rows[0]["relation"]);
                    rpt.Append("</tr >");
                    rpt.Append("</table>");
                }

                ltrReport.Text = rpt.ToString();
                ltrReport.Visible = true;
                Panel1.Visible = true;
            }
        }
    }


    public void PatientDetails_Bengali(DataSet ds)
    {
        rpt.Append("<table width='100%' cellspacing=0 border=0 style='border:1px solid black;border-spacing: 0; padding: 0;font-family:Arial;'>");
        rpt.Append("<tr style='height:20px'>");
        rpt.AppendFormat("<td style='height:20px; font-family:Arial; font-size:small;text-align:center'><b>রোগী:</b> {0}, {1}, {2},<b> প্রযত্নে:</b>{3}<b>, ঠিকানা :</b>{4},{5},{6}, <b><br/>ফোন নং:</b>{7}</td>", ds.Tables[0].Rows[0]["patient_name"], ds.Tables[0].Rows[0]["age"], ds.Tables[0].Rows[0]["SexName"], ds.Tables[0].Rows[0]["HusbandName"], ds.Tables[0].Rows[0]["vill_city"], ds.Tables[0].Rows[0]["po"], ds.Tables[0].Rows[0]["DistrictName"], ds.Tables[0].Rows[0]["PhNo1"]);
        rpt.Append("</tr >");

        rpt.Append("<tr style='height:20px'>");
        rpt.AppendFormat("<td style='height:20px; font-family:Arial; font-size:small;text-align:center'><b>ভর্তির কারণ:</b> {0}, <b>ডা:বাবুর নাম:</b> {1}</td>", ds.Tables[0].Rows[0]["DiagnosisName"], ds.Tables[0].Rows[0]["doc_name"]);
        rpt.Append("</tr >");

        rpt.Append("<tr style='height:20px'>");
        rpt.AppendFormat("<td style='height:20px; font-family:Arial; font-size:small;text-align:center'><b>রেজি: নং:</b> {0}, <b>শয্যা নং:</b> {1}, <b>ভর্তির তারিখ ও সময়:</b> {2}</td>", ds.Tables[0].Rows[0]["PatientReg"], ds.Tables[0].Rows[0]["BedNoText"], ds.Tables[0].Rows[0]["adate"]);
        rpt.Append("</tr >");
        rpt.Append("</table>");
    }

    public void PatientDetails_English(DataSet ds)
    {
        rpt.Append("<table width='100%' cellspacing=0 border=0 style='border:1px solid black;border-spacing: 0; padding: 0;font-family:Arial;'>");
        rpt.Append("<tr style='height:20px'>");
        rpt.AppendFormat("<td style='height:20px; font-family:Arial; font-size:small;text-align:center'><u><b>Patient:</b></u> {0}, {1}, {2},<u><b>C/O:</b></u>{3} <u><b>of</b></u> {4},{5},{6}, <u><b><br/>Ph No:</b></u>{7}</td>", ds.Tables[0].Rows[0]["patient_name"], ds.Tables[0].Rows[0]["age"], ds.Tables[0].Rows[0]["SexName"], ds.Tables[0].Rows[0]["HusbandName"], ds.Tables[0].Rows[0]["vill_city"], ds.Tables[0].Rows[0]["po"], ds.Tables[0].Rows[0]["DistrictName"], ds.Tables[0].Rows[0]["PhNo1"]);
        rpt.Append("</tr >");

        rpt.Append("<tr style='height:20px'>");
        rpt.AppendFormat("<td style='height:20px; font-family:Arial; font-size:small;text-align:center'><u><b>Diagnosis:</b></u> {0}, <u><b>Under Doctor:</b></u> {1}</td>", ds.Tables[0].Rows[0]["DiagnosisName"], ds.Tables[0].Rows[0]["doc_name"]);
        rpt.Append("</tr >");

        rpt.Append("<tr style='height:20px'>");
        rpt.AppendFormat("<td style='height:20px; font-family:Arial; font-size:small;text-align:center'><u><b>Regn No:</b></u> {0}, <u><b>Bed No:</b></u> {1}, <u><b>Adm:</b></u> {2}</td>", ds.Tables[0].Rows[0]["PatientReg"], ds.Tables[0].Rows[0]["BedNoText"], ds.Tables[0].Rows[0]["adate"]);
        rpt.Append("</tr >");
        rpt.Append("</table>");
    }

    public void GetHearder_AdmissionSlip(string reg)
    {
        ltrReport.Text = "";
        DataSet ds = thepatientbill.PatientForReport(reg,Session["CoCode"].ToString().Trim());
        if (RadioButtonList1.SelectedValue == "0") { Report_Header(); }
        if (ds.Tables[0].Rows.Count > 0)
        {
            rpt.Append("<table width='100%' cellspacing=0 border=0 style='border:1px solid black;border-spacing: 0; padding: 0;font-family:Arial;'>");
            rpt.Append("<tr style='height:25px;'>");
            rpt.Append("<td style='font-family:Arial; font-size:x-large;text-align:center'><b>Admission Slip</b>");
            rpt.Append("</tr >");

            rpt.Append("<tr style='height:20px;'>");
            rpt.AppendFormat("<td style='font-family:Arial; font-size:medium;text-align:center'>(Admitted By :{0}, Relation :{1})</td>", ds.Tables[0].Rows[0]["guardian_name"], ds.Tables[0].Rows[0]["relation"]);
            rpt.Append("</tr >");
            rpt.Append("</table>");

            rpt.Append("<table width='100%' cellspacing=0 border=0 style='border:1px solid black;border-spacing: 0; padding: 0;font-family:Arial;'>");
            rpt.Append("<tr style='height:25px'>");
            rpt.AppendFormat("<td style='height:20px; font-family:Arial;padding-left:250px; font-size:large;text-align:left'><u><b>Patient:&nbsp;&nbsp;{0}, {1}, {2}</b></u></td>", ds.Tables[0].Rows[0]["patient_name"], ds.Tables[0].Rows[0]["age"], ds.Tables[0].Rows[0]["SexName"]);
            rpt.Append("</tr >");


            rpt.Append("<tr style='height:25px'>");
            rpt.AppendFormat("<td style='height:20px; font-family:Arial;padding-left:250px; font-size:large;text-align:left'><u><b>Bed No:&nbsp;&nbsp;{0}</b></u></td>", ds.Tables[0].Rows[0]["BedNoText"]);
            rpt.Append("</tr >");

            rpt.Append("<tr style='height:25px'>");
            rpt.AppendFormat("<td style='height:20px; font-family:Arial;padding-left:250px; font-size:small;text-align:left'><u><b>C/O:</b></u> {0}</td>", ds.Tables[0].Rows[0]["HusbandName"]);
            rpt.Append("</tr >");

            rpt.Append("<tr style='height:25px'>");
            rpt.AppendFormat("<td style='height:20px; font-family:Arial;padding-left:250px; font-size:small;text-align:left'><u><b>Address:</b></u> {0}, {1}, {2},<u><b>Ph No:</b></u>{3}</td>", ds.Tables[0].Rows[0]["vill_city"], ds.Tables[0].Rows[0]["po"], ds.Tables[0].Rows[0]["DistrictName"], ds.Tables[0].Rows[0]["PhNo1"]);
            rpt.Append("</tr >");

            rpt.Append("<tr style='height:25px'>");
            rpt.AppendFormat("<td style='height:20px; font-family:Arial;padding-left:250px; font-size:small;text-align:left'><u><b>Diagnosis:</b></u> {0}</td>", ds.Tables[0].Rows[0]["DiagnosisName"]);
            rpt.Append("</tr >");

            rpt.Append("<tr style='height:25px'>");
            rpt.AppendFormat("<td style='height:20px; font-family:Arial;padding-left:250px; font-size:small;text-align:left'><u><b>Under Doctor:</b></u> {0}</td>", ds.Tables[0].Rows[0]["doc_name"]);
            rpt.Append("</tr >");

            rpt.Append("<tr style='height:25px'>");
            rpt.AppendFormat("<td style='height:20px; font-family:Arial;padding-left:250px; font-size:small;text-align:left'><u><b>Regn No:</b></u> {0}</td>", ds.Tables[0].Rows[0]["PatientReg"]);
            rpt.Append("</tr >");


            rpt.Append("<tr style='height:25px'>");
            rpt.AppendFormat("<td style='height:20px; font-family:Arial; padding-left:250px;font-size:small;text-align:left'><u><b>Adm Date & Time:</b></u> {0}</td>", ds.Tables[0].Rows[0]["adate"]);
            rpt.Append("</tr >");

            rpt.Append("</table>");
        }
        ltrReport.Visible = true;
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
     
    protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            // Here you will get the Control you need like:
            DropDownList ddl = (DropDownList)e.Row.FindControl("DropDownList6");
            CheckBoxList ddl1 = (CheckBoxList)e.Row.FindControl("CheckBoxList1");

            DataTable dt = thereg.DropdownReport("2", language);
            ddl.DataSource = dt;
            ddl.DataTextField = "FormName";
            ddl.DataValueField = "ID";
            ddl.DataBind();
            ddl.Items.Insert(0, new ListItem("--Select--", "0"));


            DataTable dt1 = thereg.DropdownReport("1", language);
            ddl1.DataSource = dt1;
            ddl1.DataTextField = "FormName";
            ddl1.DataValueField = "ID";
            ddl1.DataBind();
            //ddl1.Items.Insert(0, new ListItem("--Select--", "0"));
        }
    }
    protected void DropDownList5_SelectedIndexChanged(object sender, EventArgs e)
    {
        language = DropDownList5.SelectedValue;
        GridFill();
    }

    protected void autoCompleteEx_ItemSelected(object sender, EventArgs e)
    {
        TextBox txt = (TextBox)sender;
        
    }

    [System.Web.Script.Services.ScriptMethod()]
    [System.Web.Services.WebMethod]
    public static List<string> SearchPatientName(string prefixText, int count)
    {
        using (SqlConnection conn = new SqlConnection())
        {
            conn.ConnectionString = ConfigurationSettings.AppSettings["ConnectionString"].ToString();
            using (SqlCommand cmd = new SqlCommand())
            {
                cmd.CommandText = "select distinct patient_name + '-' + PatientReg +'-'+ case when husbandname is null then '' else husbandname end +'-'+Vill_city as Name from GN_PatientReg where patient_name like @SearchText + '%'";
                cmd.Parameters.AddWithValue("@SearchText", prefixText);
                cmd.Connection = conn;
                conn.Open();
                List<string> customers = new List<string>();
                using (SqlDataReader sdr = cmd.ExecuteReader())
                {
                    while (sdr.Read())
                    {
                        customers.Add(sdr["Name"].ToString());
                    }
                }
                conn.Close();
                return customers;
            }
        }
    }
}