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

public partial class DayCare_PatientListSearch : System.Web.UI.Page
{
    Main Objmain = new Main(ConfigurationSettings.AppSettings["ConnectionString"].ToString());
    System.Text.StringBuilder rpt = new System.Text.StringBuilder();
    String Date = DateTime.Now.ToString("dd/MM/yyyy");
    DC_PatientListSearch thepana = new DC_PatientListSearch(ConfigurationSettings.AppSettings["ConnectionString"].ToString());


    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["userName"] == null)
        {
            Response.Redirect("../LoginPage.aspx");
        }
        if (Objmain.checkaccss(Session["CoCode"].ToString(), Session["UserRoleID"].ToString().Trim(), "REPORT ANALYSIS", checkAccessType.ViewAction) == false)
        {
            Response.Redirect("../AccessDenied.aspx");
        }
        Page.Title = "Report Analysis";
        if (!IsPostBack)
        {
            Panel1.Visible = false; Panel2.Visible = false; Panel3.Visible = false; Panel5.Visible = false;
        }
    }


    protected void Button1_Click(object sender, EventArgs e)
    {
        GetReport();
        // ResetAllFields();
    }

    public void GetReport()
    {
        Report_Header();
        GetHearder_Detail();
        ltrReport.Text = rpt.ToString();
    }

    public void Report_Header()
    {
        rpt.Append("<table width='100%' cellspacing=0 border=0 style='border:1px solid black;border-spacing: 0; padding: 0;font-family:verdana;'>");
        rpt.Append("<tr cellpadding:'0'  style='height:30px;'>");
        rpt.Append("<td rowspan='3' width='2%' style='font-family:Verdana; font-size:x-large; font-weight:bold;padding-left:100px;text-align:left'><img src='../Images/gfclogo.png'  style='height:77px;width:75px;'/></td>");
        rpt.Append("<td   width='20%'  style='height:20px;font-family:Verdana; font-size:x-large; font-weight:bold;text-align:center'><u>GFC HOSPITAL</u></td>");
        rpt.Append("<td rowspan='3'  width='2%' style='height:20px;font-family:Verdana; font-size:x-large;padding-right:100px; font-weight:bold;text-align:right'><img src='../Images/gfclogo.png'  style='height:77px;width:75px;'/></td>");
        rpt.Append("</tr>");

        rpt.Append("<tr  style='height:20px;'>");
        rpt.AppendFormat("<td   width='20%'  style='height:10px;font-family:Verdana; font-size:small; font-weight:bold;text-align:center'>(Regn. No : NH-315/G-70/2013)</td>");
        rpt.Append("</tr>");

        rpt.Append("<tr  style='height:10px;'>");
        rpt.AppendFormat("<td  width='70%'  style='height:15px;font-family:Verdana; font-size:medium; font-weight:bold;text-align:center'>Kushpata, Ghatal, Paschim Medinipur, WB, 721212</td>");
        rpt.Append("</tr>");
        rpt.Append("</table>");
    }


    public void GetHearder_Detail()
    {
        int i;
        System.Globalization.DateTimeFormatInfo dtf = new DateTimeFormatInfo();
        string date1, date2;
        if (TextBox2.Text != "")
        {
            DateTime testdate = DateTime.ParseExact(TextBox2.Text, "dd/MM/yyyy", dtf);
            date1 = testdate.ToString("yyyy-MM-dd");
        }
        else
        {
            date1 = "null";
        }

        if (TextBox1.Text != "")
        {
            DateTime testdate = DateTime.ParseExact(TextBox1.Text, "dd/MM/yyyy", dtf);
            date2 = testdate.ToString("yyyy-MM-dd");
        }
        else
        {
            date2 = "null";
        }

        DataTable dt = thepana.GetPatientfordtls(date1, date2, Session["CoCode"].ToString().Trim()); //ds.Tables[0];
        rpt.Append("<table width='100%' cellspacing=0 border=0 style='border:1px solid black;border-spacing: 0; padding: 0;font-family:verdana;'>");
        rpt.Append("<tr style='height:30px'>");
        rpt.Append("<td style='width: 5%;border-bottom: 1px solid black; font-family:Verdana;font-weight:bold;background-color:beige; font-size:small; text-align:center'>Registration No</td>");
        rpt.Append("<td style='width: 5%;border-bottom: 1px solid black;font-family:Verdana;font-weight:bold;background-color:beige; font-size:small; text-align:center'>Name</td>");
        rpt.Append("<td style='width: 5%;border-bottom: 1px solid black;font-family:Verdana;font-weight:bold;background-color:beige; font-size:small; text-align:center'>Address</td>");
        rpt.Append("<td style='width: 5%;border-bottom: 1px solid black;font-family:Verdana;font-weight:bold;background-color:beige; font-size:small; text-align:center'>Phone No</td>");
        rpt.Append("<td style='width: 5%;border-bottom: 1px solid black;font-family:Verdana;font-weight:bold;background-color:beige; font-size:small; text-align:center'>StartDate</td>");
        rpt.Append("</tr >");

        for (i = 0; i < dt.Rows.Count; i++)
        {
            rpt.Append("<tr style='height:30px'>");
            rpt.AppendFormat("<td style='width: 7%;font-family:Verdana; font-size:small;text-align:center'>{0}</td>", dt.Rows[i]["PatientReg"]);
            rpt.AppendFormat("<td style='width: 7%;font-family:Verdana; font-size:small;text-align:center'>{0}</td>", dt.Rows[i]["patient_name"].ToString().ToUpper());
            rpt.AppendFormat("<td style='width: 7%;font-family:Verdana; font-size:small;text-align:center'>{0}</td>", dt.Rows[i]["vill_city"]);
            rpt.AppendFormat("<td style='width: 7%;font-family:Verdana; font-size:small;text-align:center'>{0}</td>", dt.Rows[i]["PhNo1"]);
            rpt.AppendFormat("<td style='width: 7%;font-family:Verdana; font-size:small;text-align:center'>{0}</td>", dt.Rows[i]["StartDate"]);
            rpt.Append("</tr >");
        }
        rpt.Append("</table'>");
        ltrReport.Text = rpt.ToString();

    }
    protected void Button2_Click(object sender, EventArgs e)
    {
        Report_Header();
        PatientDetails();
    }
    public void ResetAllFields()
    {
        //TextBox1.Text = "";
        //TextBox2.Text = "";
        DropDownList1.SelectedIndex = 0;
    }
    protected void Button3_Click(object sender, EventArgs e)
    {
        Report_Header();
        ReportGenerate();
    }
    protected void Button4_Click(object sender, EventArgs e)
    {
        Report_Header();
        ReportGenerate();
    }

    public void PatientDetails()
    {
        System.Globalization.DateTimeFormatInfo dtf = new DateTimeFormatInfo();
        string date1, date2;
        if (TextBox2.Text != "")
        {
            DateTime testdate = DateTime.ParseExact(TextBox2.Text, "dd/MM/yyyy", dtf);
            date1 = testdate.ToString("yyyy-MM-dd");
        }
        else
        {
            date1 = "null";
        }

        if (TextBox1.Text != "")
        {
            DateTime testdate = DateTime.ParseExact(TextBox1.Text, "dd/MM/yyyy", dtf);
            date2 = testdate.ToString("yyyy-MM-dd");
        }
        else
        {
            date2 = "null";
        }

        DataTable dt1 = thepana.GetPatientDtls(TextBox6.Text, TextBox3.Text, TextBox4.Text, TextBox5.Text, date1, date2, Session["CoCode"].ToString().Trim());
        rpt.Append("<table width='100%' cellspacing=0 border=0 style='border:1px solid black;border-spacing: 0; padding: 0;font-family:verdana;'>");
        rpt.Append("<tr style='height:30px'>");
        rpt.Append("<td style='width: 5%;border-bottom: 1px solid black;font-family:Verdana;font-weight:bold;background-color:beige; font-size:small; text-align:center'>Registration No</td>");
        rpt.Append("<td style='width: 5%;border-bottom: 1px solid black;font-family:Verdana;font-weight:bold;background-color:beige; font-size:small; text-align:center'>Name</td>");
        rpt.Append("<td style='width: 5%;border-bottom: 1px solid black;font-family:Verdana;font-weight:bold;background-color:beige; font-size:small; text-align:center'>Address</td>");
        rpt.Append("<td style='width: 5%;border-bottom: 1px solid black;font-family:Verdana;font-weight:bold;background-color:beige; font-size:small; text-align:center'>Phone No</td>");
        rpt.Append("<td style='width: 5%;border-bottom: 1px solid black;font-family:Verdana;font-weight:bold;background-color:beige; font-size:small; text-align:center'>No of Dialysis</td>");
        rpt.Append("<td style='width: 5%;border-bottom: 1px solid black;font-family:Verdana;font-weight:bold;background-color:beige; font-size:small; text-align:center'>StartDate</td>");
        rpt.Append("</tr >");
        for (int i = 0; i < dt1.Rows.Count; i++)
        {
            rpt.Append("<tr style='height:30px'>");
            rpt.AppendFormat("<td style='width: 7%; font-family:Verdana; font-size:small;text-align:center'>{0}</td>", dt1.Rows[i]["PatientReg"]);
            rpt.AppendFormat("<td style='width: 7%; font-family:Verdana; font-size:small;text-align:center'>{0}</td>", dt1.Rows[i]["patient_name"]);
            rpt.AppendFormat("<td style='width: 7%; font-family:Verdana; font-size:small;text-align:center'>{0}</td>", dt1.Rows[i]["vill_city"]);
            rpt.AppendFormat("<td style='width: 7%; font-family:Verdana; font-size:small;text-align:center'>{0}</td>", dt1.Rows[i]["PhNo1"]);
            rpt.AppendFormat("<td style='width: 7%  font-family:Verdana;color:Red;font-weight:bold; font-size:small;text-align:center'>( {0} )</td>", dt1.Rows[i]["NoOfDialysis"]);
            rpt.AppendFormat("<td style='width: 7%; font-family:Verdana; font-size:small;text-align:center'>{0}</td>", dt1.Rows[i]["StartDate"]);
            rpt.Append("</tr >");
            ltrReport.Text = rpt.ToString();
        }
        rpt.Append("</table'>");
       // ResetAllFields();
    }

    public void ReportGenerate()
    {
        System.Globalization.DateTimeFormatInfo dtf = new DateTimeFormatInfo();

        string date1, date2;
        if (TextBox2.Text != "")
        {
            DateTime testdate = DateTime.ParseExact(TextBox2.Text, "dd/MM/yyyy", dtf);
            date1 = testdate.ToString("yyyy-MM-dd");
        }
        else
        {
            date1 = "null";
        }

        if (TextBox1.Text != "")
        {
            DateTime testdate = DateTime.ParseExact(TextBox1.Text, "dd/MM/yyyy", dtf);
            date2 = testdate.ToString("yyyy-MM-dd");
        }
        else
        {
            date2 = "null";
        }


        //DataTable dt1 = thepana.GetPatientDtls(TextBox6.Text, TextBox3.Text, TextBox4.Text, TextBox5.Text, date1, date2);
        DataTable dt1 = thepana.GetPatientDtls(TextBox6.Text, TextBox3.Text, TextBox4.Text, TextBox5.Text, date1, date2, Session["CoCode"].ToString().Trim());

        rpt.Append("<table width='100%' cellspacing=0 border=0 style='border:1px solid black;border-spacing: 0; padding: 0;font-family:verdana;'>");
        rpt.Append("<tr style='height:30px'>");
        rpt.Append("<td style='width: 5%;border-bottom: 1px solid black; font-family:Verdana;font-weight:bold;background-color:beige; font-size:small; text-align:center'>Sl. No.</td>");
        rpt.Append("<td style='width: 5%;border-bottom: 1px solid black; font-family:Verdana;font-weight:bold;background-color:beige; font-size:small; text-align:center'>Registration No</td>");
        rpt.Append("<td style='width: 5%;border-bottom: 1px solid black; font-family:Verdana;font-weight:bold;background-color:beige; font-size:small; text-align:center'>Name</td>");
        rpt.Append("<td style='width: 5%;border-bottom: 1px solid black; font-family:Verdana;font-weight:bold;background-color:beige; font-size:small; text-align:center'>Address</td>");
        rpt.Append("<td style='width: 5%;border-bottom: 1px solid black; font-family:Verdana;font-weight:bold;background-color:beige; font-size:small; text-align:center'>Phone No</td>");
        rpt.Append("<td style='width: 5%;border-bottom: 1px solid black; font-family:Verdana;font-weight:bold;background-color:beige; font-size:small; text-align:center'>StartDate</td>");
        rpt.Append("<td style='width: 5%;border-bottom: 1px solid black; font-family:Verdana;font-weight:bold;background-color:beige; font-size:small; text-align:center'>No of Dialysis</td>");
        
        rpt.Append("</tr >");


        for (int i = 0; i < dt1.Rows.Count; i++)
        {
            rpt.Append("<tr style='height:30px'>");
            rpt.AppendFormat("<td style='width: 7%; font-family:Verdana; font-size:small;text-align:center'>{0}</td>", i + 1);
            rpt.AppendFormat("<td style='width: 7%; font-family:Verdana; font-size:small;text-align:center'>{0}</td>", dt1.Rows[i]["PatientReg"]);
            rpt.AppendFormat("<td style='width: 7%; font-family:Verdana; font-size:small;text-align:center'>{0}</td>", dt1.Rows[i]["patient_name"]);
            rpt.AppendFormat("<td style='width: 7%; font-family:Verdana; font-size:small;text-align:center'>{0}</td>", dt1.Rows[i]["vill_city"]);
            rpt.AppendFormat("<td style='width: 7%; font-family:Verdana; font-size:small;text-align:center'>{0}</td>", dt1.Rows[i]["PhNo1"]);
            rpt.AppendFormat("<td style='width: 7%; font-family:Verdana; font-size:small;text-align:center'>{0}</td>", dt1.Rows[i]["StartDate"]);
            //rpt.AppendFormat("<td style='width: 7%; font-family:Verdana;color:Red;font-weight:bold; font-size:small;text-align:center'><a href='javascript:ShowDialog(" + dt1.Rows[i]["NoOfDialysis"].ToString().Trim() + ")'> (" + dt1.Rows[i]["NoOfDialysis"] + " )</a> </td>");
             rpt.AppendFormat("<td style='width: 7%; font-family:Verdana;color:Red;font-weight:bold; font-size:small;text-align:center'><a href='DialysisDateDetails.aspx'> (" + dt1.Rows[i]["NoOfDialysis"] + " )</a> </td>");
            //rpt.AppendFormat("<td style='width: 7%; font-family:Verdana;color:Red;font-weight:bold; font-size:small;text-align:center'><a href='javascript:ShowDialog(" + dt1.Rows[i]["NoOfDialysis"] + ")'> (" + dt1.Rows[i]["NoOfDialysis"] + " )</a> </td>");
            //<a href='javascript:ShowDialog()'>My link</a>

            rpt.Append("</tr >");
            ltrReport.Text = rpt.ToString();
        }
        rpt.Append("</table'>");
       // ResetAllFields();
    }
    protected void Button6_Click(object sender, EventArgs e)
    {
        System.Globalization.DateTimeFormatInfo dtf = new DateTimeFormatInfo();
        string fromdate = "";
        string todate = "";
        if (TextBox2.Text != "")
        {
            fromdate = DateTime.ParseExact(TextBox2.Text, "dd/MM/yyyy", dtf).ToString("yyyy-MM-dd");
        }
        else
        {
            fromdate = "";
        }


        if (TextBox1.Text != "")
        {
            todate = DateTime.ParseExact(TextBox1.Text, "dd/MM/yyyy", dtf).ToString("yyyy-MM-dd");
        }
        else
        {
            todate = "";
        }
        ltrReport.Text = "";

        if (DropDownList1.SelectedIndex == 1)
        {
            Panel1.Visible = false;
            Panel2.Visible = false;
            Panel3.Visible = false;
            Panel5.Visible = true;

        }
        else if (DropDownList1.SelectedIndex == 2)
        {
            Panel1.Visible = true;
            Panel2.Visible = false;
            Panel3.Visible = false;
            Panel5.Visible = false;
            DataSet ds = thepana.GetPatientfortotal(fromdate, todate, Session["CoCode"].ToString().Trim());
            Label1.ForeColor = System.Drawing.Color.Blue;
            Label1.Text = "( " + ds.Tables[0].Rows[0]["Total"] + " )";
        }
        else
            if (DropDownList1.SelectedIndex == 3)
            {
                Panel1.Visible = false;
                Panel2.Visible = true;
                Panel3.Visible = false;
                Panel5.Visible = false;

                DataSet ds1 = thepana.GetPatientfortotal(fromdate, todate, Session["CoCode"].ToString().Trim());
                Label2.ForeColor = System.Drawing.Color.Blue;
                Label2.Text = "( " + ds1.Tables[1].Rows[0]["NoOfDialysis"] + " )";



            }
            else
                if (DropDownList1.SelectedIndex == 4)
                {
                    //DataTable dt4 = thepana.GetTotalDia(fromdate, todate);
                    DataSet ds2 = thepana.GetPatientfortotal(fromdate, todate, Session["CoCode"].ToString().Trim());
                    Label3.ForeColor = System.Drawing.Color.Blue;
                    double avg = Convert.ToDouble(ds2.Tables[1].Rows[0]["NoOfDialysis"]) / Convert.ToDouble(ds2.Tables[0].Rows[0]["Total"]);

                    Label3.Text = "( " + Math.Round(avg, 2).ToString() + " )";

                    Panel1.Visible = false;
                    Panel2.Visible = false;
                    Panel3.Visible = true;
                    Panel5.Visible = false;

                }
                else
                {

                    lblError.ForeColor = System.Drawing.Color.Green;
                    lblError.Text = "Please select Searching Type.....";
                }
    }
    
}