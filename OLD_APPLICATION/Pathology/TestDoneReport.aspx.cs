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
 
public partial class Pathology_TestDoneReport : System.Web.UI.Page
{
    Main Objmain = new Main(ConfigurationSettings.AppSettings["ConnectionString"].ToString());
    System.Text.StringBuilder rpt = new System.Text.StringBuilder();
    PH_TestDoneReport thedreport = new PH_TestDoneReport(ConfigurationSettings.AppSettings["ConnectionString"].ToString());
    String Date = DateTime.Now.ToString("dd/MM/yyyy");
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["userName"] == null)
        {
            Response.Redirect("../LoginPage.aspx");
        }
        if (Objmain.checkaccss(Session["CoCode"].ToString(), Session["UserRoleID"].ToString().Trim(), "DEPARTMENT MASTER", checkAccessType.ViewAction) == false)
        {
            Response.Redirect("../AccessDenied.aspx");
        }
        Page.Title = "Test Done";
        if (Session["userName"] == null)
        {
            Response.Redirect("../LoginPage.aspx");
        }
        if (!IsPostBack)
        {
            DropDownFill();
            Panel1.Visible = false; Panel2.Visible = false;
        }
      
    }

    public void DropDownFill()
    {
        DropDownList3.Items.Insert(0, new ListItem("Select", "0"));
           
        for (int i = 2011,j=1; i<2020; i++,j++)
        {
            DropDownList3.Items.Insert(j, new ListItem(i.ToString(), i.ToString()));
        }
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
        rpt.Append("<br/>"); rpt.Append("<br/>"); rpt.Append("<br/>");
        rpt.Append("<table width='100%' cellspacing=0 border=0 style='border:1px solid black;background-color:#9B9C8D;border-spacing: 0; padding: 0;font-family:verdana;'>");
        rpt.Append("<tr cellpadding:'0'>");
        rpt.AppendFormat("<td rowspan='4' style='width: 35%; font-family:Verdana; font-size:x-Medium; font-weight:bold;text-align:left'><img src='../Images/logo.jpg'  style='height:87px;width:205px;border-style:inherit;border-color:#D3CDD4'/></td>");
        rpt.AppendFormat("<td style='width: 35%; font-family:Verdana; font-size:large; font-weight:bold;text-align:center'>{0}</td>", "GFC Hospital");
        rpt.AppendFormat("<td style='width: 35%; font-family:Verdana; font-size:x-Medium; font-weight:bold;text-align:center'>&nbsp;</td>");
        rpt.Append("</tr>");
        rpt.Append("<tr>");
        rpt.AppendFormat("<td style='width: 35%; font-family:Verdana; font-size:x-Medium; font-weight:bold;text-align:center'>{0}</td>", "REG NO : NH-315/G-70/2013");
        rpt.AppendFormat("<td style='width: 35%; font-family:Verdana; font-size:x-Medium; font-weight:bold;text-align:center'>&nbsp;</td>");
        rpt.Append("</tr>");
        rpt.Append("<tr>");
        rpt.AppendFormat("<td style='width: 35%; font-family:Verdana; font-size:x-Medium; font-weight:bold;text-align:center'>{0}</td>", "KUSHPATA,GHATAL,MIDNAPUR(W),WB,721212");
        rpt.AppendFormat("<td style='width: 35%; font-family:Verdana; font-size:x-Medium; font-weight:bold;text-align:center'>&nbsp;</td>");
        rpt.Append("</tr>");
        rpt.Append("<tr>");
        rpt.AppendFormat("<td style='width: 35%; font-family:Verdana; font-size:x-Medium; font-weight:bold;text-align:center'>{0}</td>", "Ph :(03225)244400/244643  M:9434419825");
        rpt.AppendFormat("<td style='width: 35%; font-family:Verdana; font-size:x-Medium; font-weight:bold;text-align:center'>&nbsp;</td>");
        rpt.Append("</tr>");
        rpt.Append("</table>");
    }

    public void   GetHearder_Detail()
    {
         string from,to;
         string prevPatient = string.Empty;
         string curPatient = string.Empty;
         string prevRef = string.Empty;
         string curRef = string.Empty;
        if (TextBox2.Text != "")
            from = DateTime.ParseExact(TextBox2.Text, "dd/MM/yyyy", null).ToString("MM/dd/yyyy");
        else
            from = "";
        if (TextBox3.Text != "")
            to = DateTime.ParseExact(TextBox3.Text, "dd/MM/yyyy", null).ToString("MM/dd/yyyy");
        else
            to = "";
        DataTable dt = thedreport.Gettestdtls(from.ToString(),to.ToString()); 

        rpt.Append("<br/>");
        rpt.Append("<table width='100%' cellspacing=0 border=0 style='border:1px solid black;border-spacing: 0; padding: 0;font-family:verdana;'>");
        rpt.Append("<tr style='height:40px'>");
        rpt.Append("<td colspan='4' style='width: 8%;border-bottom: 1px solid black; font-family:Verdana;font-weight:bold;background-color:beige;font-size:medium; text-align:Center'> TEST DONE ANALYSIS  </td>");
        rpt.Append("</tr'>");
        rpt.Append("</table'>");
        int a = -1, b = -1, c = -1;
        for (int i = 0; i < dt.Rows.Count; i++)
        {
            if (dt.Rows[i]["Department"].ToString() == "PATHOLOGY")
            {
                rpt.Append("<table width='100%' cellspacing=0 border=0 style='border:1px solid black;border-spacing: 0; padding: 0;font-family:verdana;'>");
                if (a == -1)
                {
                    rpt.Append("<tr style='height:30px'>");
                    rpt.AppendFormat("<td colspan=4 style='width: 8%;border-bottom: 1px solid black;border-right: 1px solid black; font-family:Verdana;font-weight:bold;background-color:beige; font-size:x-small; text-align:center'>Department  : PATHOLOGY</td>");
                    rpt.Append("</tr >");
                    rpt.Append("<tr style='height:30px'>");
                    rpt.Append("<td style='width: 8%;border-bottom: 1px solid black;border-right: 1px solid black; font-family:Verdana;font-weight:bold;background-color:beige; font-size:x-small; text-align:center'>Patient Name</td>");
                    rpt.Append("<td style='width: 8%;border-bottom: 1px solid black;border-right: 1px solid black; font-family:Verdana;font-weight:bold;background-color:beige; font-size:x-small; text-align:center'>Consultent Doctor</td>");
                    rpt.Append("<td style='width: 8%;border-bottom: 1px solid black;border-right: 1px solid black; font-family:Verdana;font-weight:bold;background-color:beige; font-size:x-small; text-align:center'>Test Name</td>");
                    rpt.Append("<td style='width: 5%; font-family:Verdana;background-color:beige;border-bottom: 1px solid black;font-weight:bold;font-size:x-small; text-align:center'>No of Test Done</td>");
                    rpt.Append("</tr >");
                    
                }


                for (int j = 0; j < dt.Rows.Count; j++)
                {
                    a++;
                    if (dt.Rows.Count<= a)
                        break;
                    else
                    {
                        if (dt.Rows[a]["Department"].ToString() == "PATHOLOGY")
                        {
                            curPatient=dt.Rows[j]["PatientNmae"].ToString();
                            curRef = dt.Rows[j]["ReferalName"].ToString();
                            rpt.Append("<tr >");
                            if (curPatient == prevPatient)
                            {
                                rpt.AppendFormat("<td style='width: 5%;border-right: 1px solid black; font-family:Verdana; font-size:x-small;text-align:center'></td>");
                            }
                            else
                            {
                                rpt.AppendFormat("<td style='width: 5%;border-top: 1px solid black;border-right: 1px solid black; font-family:Verdana; font-size:x-small;text-align:center'>{0}</td>", curPatient);
                            }
                            if (curRef == prevRef)
                            {
                                rpt.AppendFormat("<td style='width: 5%;border-right: 1px solid black; font-family:Verdana; font-size:x-small;text-align:center'></td>");
                            }
                            else
                            {
                                rpt.AppendFormat("<td style='width: 5%;border-top: 1px solid black;border-right: 1px solid black; font-family:Verdana; font-size:x-small;text-align:center'>{0}</td>", curRef);
                            }
                            rpt.AppendFormat("<td style='width: 5%;border-bottom: 1px solid black;border-right: 1px solid black; font-family:Verdana; font-size:x-small;text-align:center'>{0}</td>", dt.Rows[j]["TestName"]);
                            rpt.AppendFormat("<td style='width: 7%; font-family:Verdana;border-bottom: 1px solid black; font-size:x-small;text-align:center'>{0}</td>", dt.Rows[j]["TotalTest"]);
                            rpt.Append("</tr >");
                            prevPatient = curPatient;
                            prevRef = curRef;

                        }
                    }
                
           
                }
                rpt.Append("</table >");
            }
            curPatient = "";
            curRef = "";
            prevPatient = "";
            prevRef="";

            if (dt.Rows[i]["Department"].ToString() == "RADIOLOGY")
            {
                rpt.Append("<table width='100%' cellspacing=0 border=0 style='border:1px solid black;border-spacing: 0; padding: 0;font-family:verdana;'>");
                if (b == -1)
                {
                    rpt.Append("<tr style='height:30px'>");
                    rpt.AppendFormat("<td colspan=4 style='width: 8%;border-bottom: 1px solid black;border-right: 1px solid black; font-family:Verdana;font-weight:bold;background-color:beige; font-size:x-small; text-align:center'>Department  : RADIOLOGY</td>");
                    rpt.Append("</tr >");
                    rpt.Append("<tr style='height:30px'>");
                    rpt.Append("<td style='width: 8%;border-bottom: 1px solid black;border-right: 1px solid black; font-family:Verdana;font-weight:bold;background-color:beige; font-size:x-small; text-align:center'>Patient Name</td>");
                    rpt.Append("<td style='width: 8%;border-bottom: 1px solid black;border-right: 1px solid black; font-family:Verdana;font-weight:bold;background-color:beige; font-size:x-small; text-align:center'>Consultent Doctor</td>");
                    rpt.Append("<td style='width: 8%;border-bottom: 1px solid black;border-right: 1px solid black; font-family:Verdana;font-weight:bold;background-color:beige; font-size:x-small; text-align:center'>Test Name</td>");
                    rpt.Append("<td style='width: 5%; font-family:Verdana;background-color:beige;border-bottom: 1px solid black;font-weight:bold;font-size:x-small; text-align:center'>No of Test Done</td>");
                    rpt.Append("</tr >");
                   
                }

            

                for (int j = 0; j < dt.Rows.Count; j++)
                {
                    b++;
                    if (dt.Rows.Count <= b)
                        break;
                    else
                    {
                        if (dt.Rows[b]["Department"].ToString() == "RADIOLOGY")
                        {
                            curPatient = dt.Rows[j]["PatientNmae"].ToString();
                            curRef = dt.Rows[j]["ReferalName"].ToString();
                            rpt.Append("<tr >");
                            if (curPatient == prevPatient)
                            {
                                rpt.AppendFormat("<td style='width: 5%;border-right: 1px solid black; font-family:Verdana; font-size:x-small;text-align:center'></td>");
                            }
                            else
                            {
                                rpt.AppendFormat("<td style='width: 5%;border-top: 1px solid black;border-right: 1px solid black; font-family:Verdana; font-size:x-small;text-align:center'>{0}</td>", curPatient);
                            }
                            if (curRef == prevRef)
                            {
                                rpt.AppendFormat("<td style='width: 5%;border-right: 1px solid black; font-family:Verdana; font-size:x-small;text-align:center'></td>");
                            }
                            else
                            {
                                rpt.AppendFormat("<td style='width: 5%;border-top: 1px solid black;border-right: 1px solid black; font-family:Verdana; font-size:x-small;text-align:center'>{0}</td>", curRef);
                            }
                            rpt.AppendFormat("<td style='width: 5%;border-bottom: 1px solid black;border-right: 1px solid black; font-family:Verdana; font-size:x-small;text-align:center'>{0}</td>", dt.Rows[j]["TestName"]);
                            rpt.AppendFormat("<td style='width: 7%; font-family:Verdana;border-bottom: 1px solid black; font-size:x-small;text-align:center'>{0}</td>", dt.Rows[j]["TotalTest"]);
                            rpt.Append("</tr >");
                            prevPatient = curPatient;
                            prevRef = curRef;
                        }
                    }
                  

                }
                rpt.Append("</table >");
            }


            if (dt.Rows[i]["Department"].ToString() == "X-RAY")
            {
                rpt.Append("<table width='100%' cellspacing=0 border=0 style='border:1px solid black;border-spacing: 0; padding: 0;font-family:verdana;'>");
                if (c == -1)
                {
                    rpt.Append("<tr style='height:30px'>");
                    rpt.AppendFormat("<td colspan=3 style='width: 8%;border-bottom: 1px solid black;border-right: 1px solid black; font-family:Verdana;font-weight:bold;background-color:beige; font-size:x-small; text-align:center'>Department  : X-RAY</td>");
                    rpt.Append("</tr >");
                    rpt.Append("<tr style='height:30px'>");
                    rpt.Append("<td style='width: 8%;border-bottom: 1px solid black;border-right: 1px solid black; font-family:Verdana;font-weight:bold;background-color:beige; font-size:x-small; text-align:center'>Patient Name</td>");
                    rpt.Append("<td style='width: 8%;border-bottom: 1px solid black;border-right: 1px solid black; font-family:Verdana;font-weight:bold;background-color:beige; font-size:x-small; text-align:center'>Consultent Doctor</td>");
                    rpt.Append("<td style='width: 8%;border-bottom: 1px solid black;border-right: 1px solid black; font-family:Verdana;font-weight:bold;background-color:beige; font-size:x-small; text-align:center'>Test Name</td>");
                    rpt.Append("<td style='width: 5%; font-family:Verdana;background-color:beige;border-bottom: 1px solid black;font-weight:bold;font-size:x-small; text-align:center'>No of Test Done</td>");
                    rpt.Append("</tr >");

                   
                }

          

                for (int j = 0; j < dt.Rows.Count; j++)
                {
                    c++;
                    if (dt.Rows.Count <= c)
                        break;
                    else
                    {
                        if (dt.Rows[c]["Department"].ToString() == "X-RAY")
                        {
                            curPatient = dt.Rows[j]["PatientNmae"].ToString();
                            curRef = dt.Rows[j]["ReferalName"].ToString();
                            rpt.Append("<tr >");
                            if (curPatient == prevPatient)
                            {
                                rpt.AppendFormat("<td style='width: 5%;border-right: 1px solid black; font-family:Verdana; font-size:x-small;text-align:center'></td>");
                            }
                            else
                            {
                                rpt.AppendFormat("<td style='width: 5%;border-top: 1px solid black;border-right: 1px solid black; font-family:Verdana; font-size:x-small;text-align:center'>{0}</td>", curPatient);
                            }
                            if (curRef == prevRef)
                            {
                                rpt.AppendFormat("<td style='width: 5%;border-right: 1px solid black; font-family:Verdana; font-size:x-small;text-align:center'></td>");
                            }
                            else
                            {
                                rpt.AppendFormat("<td style='width: 5%;border-top: 1px solid black;border-right: 1px solid black; font-family:Verdana; font-size:x-small;text-align:center'>{0}</td>", curRef);
                            }
                            rpt.AppendFormat("<td style='width: 5%;border-bottom: 1px solid black;border-right: 1px solid black; font-family:Verdana; font-size:x-small;text-align:center'>{0}</td>", dt.Rows[j]["TestName"]);
                            rpt.AppendFormat("<td style='width: 7%; font-family:Verdana;border-bottom: 1px solid black; font-size:x-small;text-align:center'>{0}</td>", dt.Rows[j]["TotalTest"]);
                            rpt.Append("</tr >");
                            prevPatient = curPatient;
                            prevRef = curRef;
                        }
                    }

                }
                rpt.Append("</table >");
            }
           
        }
        rpt.Append("<hr>");

    }

    public void GetHearder_Detail1()
    {
         DataTable dt = thedreport.Gettestdtls1(DropDownList2.SelectedValue, DropDownList3.SelectedValue); //ds.Tables[0];
        string[] s1 = new string[12] { "JANUARY", "FEBRUARY", "MARCH","APRIL", "MAY", "JUNE","JULY", "AUGUST", "SEPTEMBER","OCTOBER", "NOVEMBER", "DECEMBER"};
        rpt.Append("<br/>");
        rpt.Append("<table width='100%' cellspacing=0 border=0 style='border:1px solid black;border-spacing: 0; padding: 0;font-family:verdana;'>");
        rpt.Append("<tr style='height:40px'>");
        rpt.Append("<td colspan='6' style='width: 8%;border-bottom: 1px solid black; font-family:Verdana;font-weight:bold;background-color:beige;font-size:medium; text-align:Center'> TEST DONE ANALYSIS  </td>");
        rpt.Append("</tr'>");
        rpt.Append("</table'>");
        int a = -1, b = -1, c = -1,a1=0,b1=0,c1=0;
        string prevPatient = string.Empty;
        string curPatient = string.Empty;
        string prevRef = string.Empty;
        string curRef = string.Empty;
        for (int i = 0; i < dt.Rows.Count; i++)
        {
            if (dt.Rows[i]["Department"].ToString() == "PATHOLOGY")
            {

                rpt.Append("<table width='100%' cellspacing=0 border=0 style='border:1px solid black;border-spacing: 0; padding: 0;font-family:verdana;'>");
                if (a == -1)
                {
                     rpt.Append("<tr style='height:30px'>");
                    rpt.Append("<td colspan='4' style='width: 8%;border-bottom: 1px solid black;border-right: 1px solid black; font-family:Verdana;font-weight:bold;background-color:beige; font-size:x-small; text-align:center'>Department  : PATHOLOGY</td>");
                    rpt.Append("</tr >");
                  
                }
                if (a1 == 0)
                {
                    string amnt = s1[Convert.ToInt32(dt.Rows[i]["month1"]) - 1];
                    rpt.Append("<tr style='height:30px'>");
                    rpt.AppendFormat("<td colspan='4' style='width: 8%;border-bottom: 1px solid black;border-right: 1px solid black; font-family:Verdana;font-weight:bold;background-color:beige; font-size:x-small; text-align:left'>Month  : {0}</td>", amnt);
                    rpt.Append("</tr>");
                    rpt.Append("<tr style='height:30px'>");
                    rpt.Append("<td style='width: 8%;border-bottom: 1px solid black;border-right: 1px solid black; font-family:Verdana;font-weight:bold;background-color:beige; font-size:x-small; text-align:center'>Patient Name</td>");
                    rpt.Append("<td style='width: 8%;border-bottom: 1px solid black;border-right: 1px solid black; font-family:Verdana;font-weight:bold;background-color:beige; font-size:x-small; text-align:center'>Consultent Doctor</td>");
                    rpt.Append("<td style='width: 8%;border-bottom: 1px solid black;border-right: 1px solid black; font-family:Verdana;font-weight:bold;background-color:beige; font-size:x-small; text-align:center'>Test Name</td>");
                    rpt.Append("<td style='width: 5%; font-family:Verdana;background-color:beige;border-bottom: 1px solid black;font-weight:bold;font-size:x-small; text-align:center'>No of Test Done</td>");
                    rpt.Append("</tr >"); 
                    a1++;
                }
                else
                {
                    if (dt.Rows[i]["month1"].ToString() != dt.Rows[i-1]["month1"].ToString())
                    {
                        string amnt = s1[Convert.ToInt32(dt.Rows[i]["month1"]) - 1];
                   
                    rpt.Append("<tr style='height:30px'>");
                    rpt.AppendFormat("<td colspan='4' style='width: 8%;border-bottom: 1px solid black;border-right: 1px solid black; font-family:Verdana;font-weight:bold;background-color:beige; font-size:x-small; text-align:left'>Month  : {0}</td>", amnt);
                    rpt.Append("</tr>");
                    rpt.Append("<tr style='height:30px'>");
                    rpt.Append("<td style='width: 8%;border-bottom: 1px solid black;border-right: 1px solid black; font-family:Verdana;font-weight:bold;background-color:beige; font-size:x-small; text-align:center'>Patient Name</td>");
                    rpt.Append("<td style='width: 8%;border-bottom: 1px solid black;border-right: 1px solid black; font-family:Verdana;font-weight:bold;background-color:beige; font-size:x-small; text-align:center'>Consultent Doctor</td>");
                    rpt.Append("<td style='width: 8%;border-bottom: 1px solid black;border-right: 1px solid black; font-family:Verdana;font-weight:bold;background-color:beige; font-size:x-small; text-align:center'>Test Name</td>");
                    rpt.Append("<td style='width: 5%; font-family:Verdana;background-color:beige;border-bottom: 1px solid black;font-weight:bold;font-size:x-small; text-align:center'>No of Test Done</td>");
                    rpt.Append("</tr >");
                    }

                }


                for (int j = 0; j < dt.Rows.Count; j++)
                {
                    a++;
                    if (dt.Rows.Count <= a)
                        break;
                    else
                    {
                        if (dt.Rows[a]["Department"].ToString() == "PATHOLOGY")
                        {
                            rpt.Append("<tr >");
                            curPatient = dt.Rows[j]["PatientNmae"].ToString();
                            curRef = dt.Rows[j]["ReferalName"].ToString();
                            if (curPatient == prevPatient)
                            {
                                rpt.AppendFormat("<td style='width: 5%;border-right: 1px solid black; font-family:Verdana; font-size:x-small;text-align:center'></td>");
                            }
                            else
                            {
                                rpt.AppendFormat("<td style='width: 5%;border-top: 1px solid black;border-right: 1px solid black; font-family:Verdana; font-size:x-small;text-align:center'>{0}</td>", curPatient);
                            }
                            if (curRef == prevRef)
                            {
                                rpt.AppendFormat("<td style='width: 5%;border-right: 1px solid black; font-family:Verdana; font-size:x-small;text-align:center'></td>");
                            }
                            else
                            {
                                rpt.AppendFormat("<td style='width: 5%;border-top: 1px solid black;border-right: 1px solid black; font-family:Verdana; font-size:x-small;text-align:center'>{0}</td>", curRef);
                            }
                            rpt.AppendFormat("<td style='width: 5%;border-bottom: 1px solid black;border-right: 1px solid black; font-family:Verdana; font-size:x-small;text-align:center'>{0}</td>", dt.Rows[j]["TestName"]);
                            rpt.AppendFormat("<td style='width: 7%; font-family:Verdana;border-bottom: 1px solid black; font-size:x-small;text-align:center'>{0}</td>", dt.Rows[j]["TotalTest"]);
                            rpt.Append("</tr >");
                            prevPatient = curPatient;
                            prevRef = curRef;

                        }
                    }


                }
                rpt.Append("</table >");
            }

            curPatient = "";
            curRef = "";
            prevPatient = "";
            prevRef = "";

            if (dt.Rows[i]["Department"].ToString() == "RADIOLOGY")
            {
                rpt.Append("<table width='100%' cellspacing=0 border=0 style='border:1px solid black;border-spacing: 0; padding: 0;font-family:verdana;'>");
                if (b == -1)
                {
                    string amnt = s1[Convert.ToInt32(dt.Rows[i]["month1"]) - 1];
                    rpt.Append("<tr style='height:30px'>");
                    rpt.AppendFormat("<td colspan='4'  style='width: 8%;border-bottom: 1px solid black;border-right: 1px solid black; font-family:Verdana;font-weight:bold;background-color:beige; font-size:x-small; text-align:center'>Department  : RADIOLOGY</td>");
                    rpt.Append("</tr >");
                   
                }
               
                if (b1 == 0)
                {
                    string amnt = s1[Convert.ToInt32(dt.Rows[i]["month1"]) - 1];
                    rpt.Append("<tr style='height:30px'>");
                    rpt.AppendFormat("<td colspan='2' style='width: 8%;border-bottom: 1px solid black;border-right: 1px solid black; font-family:Verdana;font-weight:bold;background-color:beige; font-size:x-small; text-align:left'>Month  : {0}</td>", amnt);
                    rpt.Append("</tr>");
                    rpt.Append("<tr style='height:30px'>");
                    rpt.Append("<td style='width: 8%;border-bottom: 1px solid black;border-right: 1px solid black; font-family:Verdana;font-weight:bold;background-color:beige; font-size:x-small; text-align:center'>Patient Name</td>");
                    rpt.Append("<td style='width: 8%;border-bottom: 1px solid black;border-right: 1px solid black; font-family:Verdana;font-weight:bold;background-color:beige; font-size:x-small; text-align:center'>Consultent Doctor</td>");
                    rpt.Append("<td style='width: 8%;border-bottom: 1px solid black;border-right: 1px solid black; font-family:Verdana;font-weight:bold;background-color:beige; font-size:x-small; text-align:center'>Test Name</td>");
                    rpt.Append("<td style='width: 5%; font-family:Verdana;background-color:beige;border-bottom: 1px solid black;font-weight:bold;font-size:x-small; text-align:center'>No of Test Done</td>");
                    rpt.Append("</tr >");
                    b1++;
                }
                else
                {
                    if (dt.Rows[i]["month1"].ToString() != dt.Rows[i - 1]["month1"].ToString())
                    {
                        string amnt = s1[Convert.ToInt32(dt.Rows[i]["month1"]) - 1];
                        rpt.Append("<tr style='height:30px'>");
                        rpt.AppendFormat("<td colspan='2' style='width: 8%;border-bottom: 1px solid black;border-right: 1px solid black; font-family:Verdana;font-weight:bold;background-color:beige; font-size:x-small; text-align:left'>Month  : {0}</td>", amnt);
                        rpt.Append("</tr>");
                        rpt.Append("<tr style='height:30px'>");
                        rpt.Append("<td style='width: 8%;border-bottom: 1px solid black;border-right: 1px solid black; font-family:Verdana;font-weight:bold;background-color:beige; font-size:x-small; text-align:center'>Patient Name</td>");
                        rpt.Append("<td style='width: 8%;border-bottom: 1px solid black;border-right: 1px solid black; font-family:Verdana;font-weight:bold;background-color:beige; font-size:x-small; text-align:center'>Consultent Doctor</td>");
                        rpt.Append("<td style='width: 8%;border-bottom: 1px solid black;border-right: 1px solid black; font-family:Verdana;font-weight:bold;background-color:beige; font-size:x-small; text-align:center'>Test Name</td>");
                        rpt.Append("<td style='width: 5%; font-family:Verdana;background-color:beige;border-bottom: 1px solid black;font-weight:bold;font-size:x-small; text-align:center'>No of Test Done</td>");
                        rpt.Append("</tr >");
                    }

                }


                for (int j = 0; j < dt.Rows.Count; j++)
                {
                    b++;
                    if (dt.Rows.Count <= b)
                        break;
                    else
                    {
                        if (dt.Rows[b]["Department"].ToString() == "RADIOLOGY")
                        {
                            rpt.Append("<tr >");
                            curPatient = dt.Rows[j]["PatientNmae"].ToString();
                            curRef = dt.Rows[j]["ReferalName"].ToString();
                            if (curPatient == prevPatient)
                            {
                                rpt.AppendFormat("<td style='width: 5%;border-right: 1px solid black; font-family:Verdana; font-size:x-small;text-align:center'></td>");
                            }
                            else
                            {
                                rpt.AppendFormat("<td style='width: 5%;border-top: 1px solid black;border-right: 1px solid black; font-family:Verdana; font-size:x-small;text-align:center'>{0}</td>", curPatient);
                            }
                            if (curRef == prevRef)
                            {
                                rpt.AppendFormat("<td style='width: 5%;border-right: 1px solid black; font-family:Verdana; font-size:x-small;text-align:center'></td>");
                            }
                            else
                            {
                                rpt.AppendFormat("<td style='width: 5%;border-top: 1px solid black;border-right: 1px solid black; font-family:Verdana; font-size:x-small;text-align:center'>{0}</td>", curRef);
                            }
                            rpt.AppendFormat("<td style='width: 5%;border-bottom: 1px solid black;border-right: 1px solid black; font-family:Verdana; font-size:x-small;text-align:center'>{0}</td>", dt.Rows[j]["TestName"]);
                            rpt.AppendFormat("<td style='width: 7%; font-family:Verdana;border-bottom: 1px solid black; font-size:x-small;text-align:center'>{0}</td>", dt.Rows[j]["TotalTest"]);
                            rpt.Append("</tr >");
                            prevPatient = curPatient;
                            prevRef = curRef;
                        }
                    }


                }
                rpt.Append("</table >");
            }

            curPatient = "";
            curRef = "";
            prevPatient = "";
            prevRef = "";

            if (dt.Rows[i]["Department"].ToString() == "X-RAY")
            {
                rpt.Append("<table width='100%' cellspacing=0 border=0 style='border:1px solid black;border-spacing: 0; padding: 0;font-family:verdana;'>");
                if (c == -1)
                {
                    string amnt = s1[Convert.ToInt32(dt.Rows[i]["month1"]) - 1];
                   
                    rpt.Append("<tr style='height:30px'>");
                    rpt.AppendFormat("<td colspan ='4' style='width: 8%;border-bottom: 1px solid black;border-right: 1px solid black; font-family:Verdana;font-weight:bold;background-color:beige; font-size:x-small; text-align:center'>Department  : X-RAY</td>");
                    rpt.Append("</tr >");
                  

                }

                if (c1 == 0)
                {
                    string amnt = s1[Convert.ToInt32(dt.Rows[i]["month1"]) - 1];
                    rpt.Append("<tr style='height:30px'>");
                    rpt.AppendFormat("<td colspan='4' style='width: 8%;border-bottom: 1px solid black;border-right: 1px solid black; font-family:Verdana;font-weight:bold;background-color:beige; font-size:x-small; text-align:left'>Month  : {0}</td>", amnt);
                    rpt.Append("</tr>");
                    rpt.Append("<tr style='height:30px'>");
                    rpt.Append("<td style='width: 8%;border-bottom: 1px solid black;border-right: 1px solid black; font-family:Verdana;font-weight:bold;background-color:beige; font-size:x-small; text-align:center'>Patient Name</td>");
                    rpt.Append("<td style='width: 8%;border-bottom: 1px solid black;border-right: 1px solid black; font-family:Verdana;font-weight:bold;background-color:beige; font-size:x-small; text-align:center'>Consultent Doctor</td>");
                    rpt.Append("<td style='width: 8%;border-bottom: 1px solid black;border-right: 1px solid black; font-family:Verdana;font-weight:bold;background-color:beige; font-size:x-small; text-align:center'>Test Name</td>");
                    rpt.Append("<td style='width: 5%; font-family:Verdana;background-color:beige;border-bottom: 1px solid black;font-weight:bold;font-size:x-small; text-align:center'>No of Test Done</td>");
                    rpt.Append("</tr >");
                    c1++;
                }
                else
                {
                    if (dt.Rows[i]["month1"].ToString() != dt.Rows[i - 1]["month1"].ToString())
                    {
                        string amnt = s1[Convert.ToInt32(dt.Rows[i]["month1"]) - 1];
                        rpt.Append("<tr style='height:30px'>");
                        rpt.AppendFormat("<td colspan='4' style='width: 8%;border-bottom: 1px solid black;border-right: 1px solid black; font-family:Verdana;font-weight:bold;background-color:beige; font-size:x-small; text-align:left'>Month  : {0}</td>", amnt);
                        rpt.Append("</tr>");
                        rpt.Append("<tr style='height:30px'>");
                        rpt.Append("<td style='width: 8%;border-bottom: 1px solid black;border-right: 1px solid black; font-family:Verdana;font-weight:bold;background-color:beige; font-size:x-small; text-align:center'>Patient Name</td>");
                        rpt.Append("<td style='width: 8%;border-bottom: 1px solid black;border-right: 1px solid black; font-family:Verdana;font-weight:bold;background-color:beige; font-size:x-small; text-align:center'>Consultent Doctor</td>");
                        rpt.Append("<td style='width: 8%;border-bottom: 1px solid black;border-right: 1px solid black; font-family:Verdana;font-weight:bold;background-color:beige; font-size:x-small; text-align:center'>Test Name</td>");
                        rpt.Append("<td style='width: 5%; font-family:Verdana;background-color:beige;border-bottom: 1px solid black;font-weight:bold;font-size:x-small; text-align:center'>No of Test Done</td>");
                        rpt.Append("</tr >");
                    }

                }

                for (int j = 0; j < dt.Rows.Count; j++)
                {
                    c++;
                    if (dt.Rows.Count <= c)
                        break;
                    else
                    {
                        if (dt.Rows[c]["Department"].ToString() == "X-RAY")
                        {
                            rpt.Append("<tr >");
                            curPatient = dt.Rows[j]["PatientNmae"].ToString();
                            curRef = dt.Rows[j]["ReferalName"].ToString();
                            if (curPatient == prevPatient)
                            {
                                rpt.AppendFormat("<td style='width: 5%;border-right: 1px solid black; font-family:Verdana; font-size:x-small;text-align:center'></td>");
                            }
                            else
                            {
                                rpt.AppendFormat("<td style='width: 5%;border-top: 1px solid black;border-right: 1px solid black; font-family:Verdana; font-size:x-small;text-align:center'>{0}</td>", curPatient);
                            }
                            if (curRef == prevRef)
                            {
                                rpt.AppendFormat("<td style='width: 5%;border-right: 1px solid black; font-family:Verdana; font-size:x-small;text-align:center'></td>");
                            }
                            else
                            {
                                rpt.AppendFormat("<td style='width: 5%;border-top: 1px solid black;border-right: 1px solid black; font-family:Verdana; font-size:x-small;text-align:center'>{0}</td>", curRef);
                            }
                            rpt.AppendFormat("<td style='width: 5%;border-bottom: 1px solid black;border-right: 1px solid black; font-family:Verdana; font-size:x-small;text-align:center'>{0}</td>", dt.Rows[j]["TestName"]);
                            rpt.AppendFormat("<td style='width: 7%; font-family:Verdana;border-bottom: 1px solid black; font-size:x-small;text-align:center'>{0}</td>", dt.Rows[j]["TotalTest"]);
                            rpt.Append("</tr >");
                        }
                    }


                }
                rpt.Append("</table >");
            }

        }
        rpt.Append("<hr>");

    }

    protected void Button1_Click(object sender, EventArgs e)
    {
        GetReport();

    }

    protected void Button2_Click(object sender, EventArgs e)
    {
        GetReport1();

    }

    protected void DropDownList1_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (DropDownList1.SelectedIndex == 1)
        {
            Panel1.Visible = true; Panel2.Visible = false;
        }
        else
            if (DropDownList1.SelectedIndex == 2)
            {
                Panel2.Visible = true;
                Panel1.Visible = false;
                //GetReport1();
              
            }
    }
}