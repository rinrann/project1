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

 
public partial class Pathology_TestResultReport : System.Web.UI.Page
{
    Main Objmain = new Main(ConfigurationSettings.AppSettings["ConnectionString"].ToString());
    PH_TestResultReport theTestResult = new PH_TestResultReport(ConfigurationSettings.AppSettings["ConnectionString"].ToString());
    String Date = DateTime.Now.ToString("dd/MM/yyyy");
    int a = -1;
    System.Text.StringBuilder rpt = new System.Text.StringBuilder();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["userName"] == null)
        {
            Response.Redirect("../LoginPage.aspx");
        }
        if (Objmain.checkaccss(Session["CoCode"].ToString(), Session["UserRoleID"].ToString().Trim(), "TEST RESULT REPORT", checkAccessType.ViewAction) == false)
        {
            Response.Redirect("../AccessDenied.aspx");
        }
        if (Session["userName"] == null)
        {
            Response.Redirect("../LoginPage.aspx");
          
        }
        Page.Title = "Test Result Report";
        if (!IsPostBack)
        {

            cmdPrint.Visible = false;
            btnBack.Visible = false;
            
        }
    }

    public void DropDownFill()
    {
        //this.DropDownList1.DataSource = theTestResult.DropDownfill(HiddenField1.Value);
        this.DropDownList1.DataSource = theTestResult.DropDownfill(Session["CoCode"].ToString().Trim(), Session["YearCode"].ToString().Trim(), txtreg.Text);
        this.DropDownList1.DataTextField = "TestName";
        this.DropDownList1.DataValueField = "TestId";
        this.DropDownList1.DataBind();
        this.DropDownList1.Items.Insert(0, new ListItem("--All--", "0"));
    }
    protected void Button4_Click(object sender, EventArgs e)
    {
         GetReport();
         if (ltrReport.Text != "")
         {
             btnBack.Visible = true;
            
             cmdPrint.Visible = true;
         }
         else
         {
             btnBack.Visible = false;
             
             cmdPrint.Visible = false;

         } 
    }

    public void GetReport()
    {
        GetHearder_Detail();
        ltrReport.Text = rpt.ToString();
    }

    public void Report_Header()
    {
        rpt.Append("<br/>");
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

    public void DocDtls(DataTable dtdoc)
    {
        if (dtdoc.Rows.Count > 0)
        {
            rpt.Append("<br/>");
            rpt.Append("<table width='100%' cellspacing=0 border=0 style=' padding: 0;font-family:verdana;'>");

            rpt.Append("<tr>");
            rpt.Append("<td width='350px' style='font-family:Verdana; font-size:x-Medium;font-weight:bold;color:Blue; text-align:center'>Consultant Pathologist</td>");
            rpt.Append("<td width='300px' style= 'font-family:Verdana; font-size:medium;font-weight:bold; text-align:left'></td>");
            rpt.Append("<td width='350px' style='font-family:Verdana; font-size:x-Medium;font-weight:bold;color:Blue; text-align:center'>Checked By</td>");
            rpt.Append("</tr'>");
            rpt.Append("<br/>");
            rpt.Append("<tr>");
            rpt.AppendFormat("<td width='350px' style='font-family:Verdana; font-size:medium;font-weight:bold; text-align:center'>Dr. {0}</td>", dtdoc.Rows[0]["pathologist"]);
            rpt.Append("<td width='300px' style= 'font-family:Verdana; font-size:medium;font-weight:bold; text-align:left'></td>");
            rpt.AppendFormat("<td width='350px' style='font-family:Verdana; font-size:medium;font-weight:bold; text-align:center'>{0}</td>", dtdoc.Rows[0]["CheckedByDoc"]);
            rpt.Append("</tr'>");
            rpt.Append("<br/>");
            rpt.Append("<tr>");
            rpt.AppendFormat("<td width='350px' style='font-family:Verdana; font-size:medium;font-weight:bold; text-align:center'>{0}</td>", dtdoc.Rows[0]["PathDesignation"]);
            rpt.Append("<td width='300px' style= 'font-family:Verdana; font-size:medium;font-weight:bold; text-align:left'></td>");
            rpt.AppendFormat("<td  width='350px' style='font-family:Verdana; font-size:medium;font-weight:bold; text-align:center'></td>"/*, dtdoc.Rows[0]["ChkDesignation"]*/);
            rpt.Append("</tr'>");

            rpt.Append("</tr'>");
            rpt.Append("</table>");
        }
    }
    public void pathology(string compcode, string yearcode, string code)
    {
        
        DataTable dt1 = theTestResult.GetTestmaster(compcode, yearcode,txtreg.Text.Trim(),code);
        if (dt1.Rows.Count > 0)
        {
            for (int d = 0; d < dt1.Rows.Count; d++)
            {
                rpt.Append("<table width='100%' cellspacing=0 border=0 style='border:1px solid black;border-spacing: 0; padding: 0;font-family:verdana;'>");
                rpt.Append("<tr style='height:30px'>");
                rpt.AppendFormat("<td colspan='3' style='width: 5%;border-bottom: 1px solid black;font-family:Verdana;font-weight:bold;background-color:#57B1F6;color:#fff; font-size:small; text-align:center'>Test Name :     {0}</td>", dt1.Rows[d]["TestName"]);
                rpt.Append("</tr >");
                DataTable dt2 = theTestResult.GetTestmasterMul( compcode,dt1.Rows[d]["TestId"].ToString());
                DataTable dt5 = theTestResult.GetTestResult(compcode, yearcode,txtreg.Text,code);
                rpt.Append("<tr style='height:30px'>");
                rpt.Append("<td style='width: 5%;border-right: 1px solid black; font-family:Verdana;font-weight:bold;background-color:beige; font-size:small; text-align:center'>Parameter Name</td>");
                rpt.Append("<td style='width: 5%;border-right: 1px solid black; font-family:Verdana;font-weight:bold;background-color:beige; font-size:small; text-align:center'>Value</td>");
                rpt.Append("<td style='width: 5%;font-family:Verdana;font-weight:bold;background-color:beige; font-size:small; text-align:center'>Normal Range</td>");
                rpt.Append("</tr >");
                if (dt1.Rows[d]["TestType"].ToString() == "1")
                {
                    rpt.Append("<tr style='height:30px'>");
                    rpt.AppendFormat("<td style='width: 5%;border-top: 1px solid black;border-right: 1px solid black; font-family:Verdana;font-size:small; text-align:center'>No Parameter</td>");
                    if (dt5.Rows.Count > 0 && d < dt5.Rows.Count)
                    {
                        rpt.AppendFormat("<td style='width: 5%;border-top: 1px solid black;border-right: 1px solid black; font-family:Verdana;font-size:small; text-align:center'>{0}</td>", dt5.Rows[d]["Value"]);
                    }
                    else
                    {
                        rpt.AppendFormat("<td style='width: 5%;border-top: 1px solid black;border-right: 1px solid black; font-family:Verdana;font-size:small; text-align:center'></td>");
                    }
                    rpt.AppendFormat("<td style='width: 5%;border-top: 1px solid black;font-family:Verdana;font-size:small; text-align:center'>{0}</td>", dt1.Rows[d]["NormalRange"]);
                    rpt.Append("</tr >");
                }
                else
                {
                    if (dt5.Rows.Count > d)
                    {
                        if (dt5.Rows[d]["SpecimenId"].ToString() != "")
                        {
                            DataTable dt6 = theTestResult.GetTestResultMul(compcode,  yearcode,dt5.Rows[d]["ID"].ToString());
                            if(dt6.Rows.Count>0)
                            {
                          for (int i1 = 0; i1 < dt2.Rows.Count; i1++)
                            {
                                rpt.Append("<tr style='height:30px'>");
                                rpt.AppendFormat("<td style='width: 5%;border-bottom: 1px solid black;border-right: 1px solid black; font-family:Verdana;font-weight:bold; font-size:x-small; text-align:center'>{0}</td>", dt2.Rows[i1]["SubHeading"]);
                                if (dt6.Rows.Count > 0 && i1 < dt6.Rows.Count)
                                {
                                    rpt.AppendFormat("<td style='width: 5%;border-bottom: 1px solid black;border-right: 1px solid black; font-family:Verdana;font-weight:bold;font-size:x-small; text-align:center'>{0}</td>", dt6.Rows[i1]["Value"]);
                                }
                                else
                                {
                                    rpt.AppendFormat("<td style='width: 5%;border-bottom: 1px solid black;border-right: 1px solid black; font-family:Verdana;font-weight:bold; font-size:x-small; text-align:center'></td>");
                                }
                                rpt.AppendFormat("<td style='width: 5%;border-bottom: 1px solid black;border-right: 1px solid black; font-family:Verdana;font-weight:bold; font-size:x-small; text-align:center'>{0}</td>", dt2.Rows[i1]["NormalRange"]);
                                rpt.Append("</tr >");
                                DataTable dt3 = theTestResult.GetTestmasterComplex( compcode,dt2.Rows[i1]["MultipleId"].ToString());
                                if (dt3.Rows.Count == 0)
                                    continue;
                                DataTable dt7 = theTestResult.GetTestResultComplex(compcode, yearcode,dt6.Rows[i1]["RMultipleId"].ToString());
                                if (dt3.Rows.Count > 0)
                                {
                                    for (int j = 0; j < dt3.Rows.Count; j++)
                                    {
                                        rpt.Append("<tr style='height:30px'>");
                                        rpt.AppendFormat("<td style='width: 5%;border-bottom: 1px solid black;border-right: 1px solid black; font-family:Verdana;font-weight:bold; font-size:x-small; text-align:center'>{0}</td>", dt3.Rows[j]["SubHeading"]);
                                        if (dt7.Rows.Count > 0 && j < dt7.Rows.Count)
                                            rpt.AppendFormat("<td style='width: 5%;border-bottom: 1px solid black;border-right: 1px solid black; font-family:Verdana;font-weight:bold; font-size:x-small; text-align:center'>{0}</td>", dt7.Rows[j]["Value"]);
                                        else
                                        {
                                            rpt.AppendFormat("<td style='width: 5%;border-bottom: 1px solid black;border-right: 1px solid black; font-family:Verdana;font-weight:bold; font-size:x-small; text-align:center'></td>");
                                        }
                                        rpt.AppendFormat("<td style='width: 5%;border-bottom: 1px solid black;border-right: 1px solid black; font-family:Verdana;font-weight:bold; font-size:x-small; text-align:center'>{0}</td>", dt3.Rows[j]["NormalRange"]);
                                        rpt.Append("</tr >");
                                        DataTable dt4 = theTestResult.GetTestmasterDuplex( compcode,dt3.Rows[j]["ComplexId"].ToString());
                                        if (dt4.Rows.Count == 0)
                                            continue;
                                        DataTable dt8 = theTestResult.GetTestResultDuplex( compcode,  yearcode,dt7.Rows[j]["RComplexId"].ToString());
                                        if (dt4.Rows.Count > 0)
                                        {
                                            for (int k = 0; k < dt4.Rows.Count; k++)
                                            {
                                                rpt.Append("<tr style='height:30px'>");
                                                rpt.AppendFormat("<td style='width: 5%;border-bottom: 1px solid black;border-right: 1px solid black; font-family:Verdana;font-weight:bold; font-size:x-small; text-align:center'>{0}</td>", dt4.Rows[k]["SubHeading"]);
                                                if (dt8.Rows.Count > 0 && k < dt8.Rows.Count)
                                                    rpt.AppendFormat("<td style='width: 5%;border-bottom: 1px solid black;border-right: 1px solid black; font-family:Verdana;font-weight:bold; font-size:x-small; text-align:center'>{0}</td>", dt8.Rows[k]["Value"]);
                                                else
                                                    rpt.AppendFormat("<td style='width: 5%;border-bottom: 1px solid black;border-right: 1px solid black; font-family:Verdana;font-weight:bold; font-size:x-small; text-align:center'></td>");
                                                rpt.AppendFormat("<td style='width: 5%;border-bottom: 1px solid black;border-right: 1px solid black; font-family:Verdana;font-weight:bold; font-size:x-small; text-align:center'>{0}</td>", dt4.Rows[k]["NormalRange"]);
                                                rpt.Append("</tr >");
                                            }
                                        }
                                    }
                                }

                            }
                        }

                        }
                    }
                }

                rpt.Append("</table>");
                rpt.Append("<br/>");
                //rpt.Append("<table width='100%' cellspacing=0 style=' padding: 0;font-family:verdana;'>");
                //rpt.Append("<tr >");
                //rpt.Append("<td style='width: 5%;font-family:Verdana;font-weight:bold; font-size:x-medium; text-align:left'>Remark / Impression  : :</td>");
                //rpt.AppendFormat("<td style='width: 5%;font-family:Verdana;font-weight:bold; font-size:x-small; text-align:left'>{0}</td>", dt5.Rows[d]["Remarks"]);
                //rpt.Append("</tr >");
                //rpt.Append("</table>");
            }

           /* DataTable dtdoc = theTestResult.GetDoctors(compcode, yearcode, txtreg.Text, "P");
            DocDtls(dtdoc); */
        } 
    }


    public void Group(string compcode, string yearcode)
    {

        DataTable dt1 = theTestResult.GetTestmaster1(txtreg.Text.Trim());
        if (dt1.Rows.Count > 0)
        { 
            for (int d = 0; d < dt1.Rows.Count; d++)
            {
                rpt.Append("<br />"); 
                rpt.Append("<table width='100%' cellspacing=0 border=0 style='border:1px solid black;border-spacing: 0; padding: 0;font-family:verdana;'>");
                rpt.Append("<tr style='height:30px'>");
                rpt.AppendFormat("<td colspan='3' style='width: 5%;border-bottom: 1px solid black;font-family:Verdana;font-weight:bold;background-color:#57B1F6;color:#fff; font-size:small; text-align:center'>Test Name :     {0}</td>", dt1.Rows[d]["TestName"]);
                rpt.Append("</tr >");
                DataTable dt2 = theTestResult.GetTestmasterMul(compcode, dt1.Rows[d]["TestId"].ToString());
                DataTable dt5 = theTestResult.GetTestResult1(txtreg.Text, Session["CoCode"].ToString().Trim());
                rpt.Append("<tr style='height:30px'>");
                rpt.Append("<td style='width: 5%;border-bottom: 1px solid black;border-right: 1px solid black; font-family:Verdana;font-weight:bold;background-color:beige; font-size:x-small; text-align:center'>Parameter Name</td>");
                rpt.Append("<td style='width: 5%;border-bottom: 1px solid black;border-right: 1px solid black; font-family:Verdana;font-weight:bold;background-color:beige; font-size:x-small; text-align:center'>Value</td>");
                rpt.Append("<td style='width: 5%;border-bottom: 1px solid black;border-right: 1px solid black; font-family:Verdana;font-weight:bold;background-color:beige; font-size:x-small; text-align:center'>Normal Range</td>");
                rpt.Append("</tr >");
                if (dt1.Rows[d]["TestType"].ToString() == "1")
                {
                    rpt.Append("<tr style='height:30px'>");
                    rpt.AppendFormat("<td style='width: 5%;border-bottom: 1px solid black;border-right: 1px solid black; font-family:Verdana;font-weight:bold; font-size:x-small; text-align:center'>{0}</td>",dt1.Rows[d]["TestName"]);
                    rpt.AppendFormat("<td style='width: 5%;border-bottom: 1px solid black;border-right: 1px solid black; font-family:Verdana;font-weight:bold; font-size:x-small; text-align:center'>{0}</td>", dt5.Rows[d]["Value"]);
                    rpt.AppendFormat("<td style='width: 5%;border-bottom: 1px solid black;border-right: 1px solid black; font-family:Verdana;font-weight:bold; font-size:x-small; text-align:center'>{0}</td>", dt1.Rows[d]["NormalRange"]);
                    rpt.Append("</tr >");
                }
                else
                {
                    if (dt5.Rows.Count > d)
                    {
                        if (dt5.Rows[d]["SpecimenId"].ToString() != "")
                        {
                            DataTable dt6 = theTestResult.GetTestResultMul(compcode, yearcode, dt5.Rows[d]["ID"].ToString());
                            if (dt6.Rows.Count > 0)
                            {
                                for (int i1 = 0; i1 < dt2.Rows.Count; i1++)
                                {
                                    rpt.Append("<tr style='height:30px'>");
                                    rpt.AppendFormat("<td style='width: 5%;border-bottom: 1px solid black;border-right: 1px solid black; font-family:Verdana;font-weight:bold; font-size:x-small; text-align:center'>{0}</td>", dt2.Rows[i1]["SubHeading"]);
                                    if (dt6.Rows.Count > 0)
                                    {
                                        rpt.AppendFormat("<td style='width: 5%;border-bottom: 1px solid black;border-right: 1px solid black; font-family:Verdana;font-weight:bold;font-size:x-small; text-align:center'>{0}</td>", dt6.Rows[i1]["Value"]);
                                    }
                                    else
                                    {
                                        rpt.AppendFormat("<td style='width: 5%;border-bottom: 1px solid black;border-right: 1px solid black; font-family:Verdana;font-weight:bold; font-size:x-small; text-align:center'></td>");
                                    }
                                    rpt.AppendFormat("<td style='width: 5%;border-bottom: 1px solid black;border-right: 1px solid black; font-family:Verdana;font-weight:bold; font-size:x-small; text-align:center'>{0}</td>", dt2.Rows[i1]["NormalRange"]);
                                    rpt.Append("</tr >");
                                    DataTable dt3 = theTestResult.GetTestmasterComplex(compcode, dt2.Rows[i1]["MultipleId"].ToString());
                                    if (dt3.Rows.Count == 0)
                                        continue;
                                    DataTable dt7 = theTestResult.GetTestResultComplex(compcode, yearcode, dt6.Rows[i1]["RMultipleId"].ToString());
                                    if (dt3.Rows.Count > 0)
                                    {
                                        for (int j = 0; j < dt3.Rows.Count; j++)
                                        {
                                            rpt.Append("<tr style='height:30px'>");
                                            rpt.AppendFormat("<td style='width: 5%;border-bottom: 1px solid black;border-right: 1px solid black; font-family:Verdana;font-weight:bold; font-size:x-small; text-align:center'>{0}</td>", dt3.Rows[j]["SubHeading"]);
                                            if (dt7.Rows.Count > 0 && j < dt7.Rows.Count)
                                            {
                                                rpt.AppendFormat("<td style='width: 5%;border-bottom: 1px solid black;border-right: 1px solid black; font-family:Verdana;font-weight:bold; font-size:x-small; text-align:center'>{0}</td>", dt7.Rows[j]["Value"]);
                                               //rpt.AppendFormat("<td style='width: 5%;border-bottom: 1px solid black;border-right: 1px solid black; font-family:Verdana;font-weight:bold; font-size:x-small; text-align:center'></td>");
                                            }
                                            else
                                            {
                                                rpt.AppendFormat("<td style='width: 5%;border-bottom: 1px solid black;border-right: 1px solid black; font-family:Verdana;font-weight:bold; font-size:x-small; text-align:center'></td>");
                                            }
                                            rpt.AppendFormat("<td style='width: 5%;border-bottom: 1px solid black;border-right: 1px solid black; font-family:Verdana;font-weight:bold; font-size:x-small; text-align:center'>{0}</td>", dt3.Rows[j]["NormalRange"]);
                                            rpt.Append("</tr >");
                                            DataTable dt4 = theTestResult.GetTestmasterDuplex(compcode, dt3.Rows[j]["ComplexId"].ToString());
                                            if (dt4.Rows.Count == 0)
                                                continue;
                                            DataTable dt8 = theTestResult.GetTestResultDuplex(compcode, yearcode, dt7.Rows[j]["RComplexId"].ToString());
                                            if (dt4.Rows.Count > 0)
                                            {
                                                for (int k = 0; k < dt4.Rows.Count; k++)
                                                {
                                                    rpt.Append("<tr style='height:30px'>");
                                                    rpt.AppendFormat("<td style='width: 5%;border-bottom: 1px solid black;border-right: 1px solid black; font-family:Verdana;font-weight:bold; font-size:x-small; text-align:center'>{0}</td>", dt4.Rows[k]["SubHeading"]);
                                                    if (dt8.Rows.Count > 0 && k < dt8.Rows.Count)
                                                        rpt.AppendFormat("<td style='width: 5%;border-bottom: 1px solid black;border-right: 1px solid black; font-family:Verdana;font-weight:bold; font-size:x-small; text-align:center'>{0}</td>", dt8.Rows[k]["value"]);
                                                    else
                                                        rpt.AppendFormat("<td style='width: 5%;border-bottom: 1px solid black;border-right: 1px solid black; font-family:Verdana;font-weight:bold; font-size:x-small; text-align:center'></td>");
                                                    rpt.AppendFormat("<td style='width: 5%;border-bottom: 1px solid black;border-right: 1px solid black; font-family:Verdana;font-weight:bold; font-size:x-small; text-align:center'>{0}</td>", dt4.Rows[k]["NormalRange"]);
                                                    rpt.Append("</tr >");
                                                }
                                            }
                                        }
                                    }

                                }
                            }

                        }
                    }
                }

                rpt.Append("</table>");
                rpt.Append("<br/>"); rpt.Append("<br/>"); rpt.Append("<br/>");
                rpt.Append("<table width='100%' cellspacing=0 style=' padding: 0;font-family:verdana;'>");
                rpt.Append("<tr >");
                rpt.Append("<td style='width: 5%;font-family:Verdana;font-weight:bold; font-size:x-medium; text-align:left'>Remark / Impression  : :</td>");
                rpt.AppendFormat("<td style='width: 5%;font-family:Verdana;font-weight:bold; font-size:x-small; text-align:left'>{0}</td>", dt5.Rows[d]["Remarks"]);
                rpt.Append("</tr >");
                rpt.Append("</table>");
            }

            

        } 
    }


    public void XRay(string compcode,string yearcode,int a,string code)
    {
        DataTable dtxray = theTestResult.Getxrayreport(compcode,yearcode,txtreg.Text,code);
        if (dtxray.Rows.Count > 0)
        {
            rpt.Append("<table width='100%' cellspacing=0 border=0 style='border:1px solid black;border-spacing: 0; padding: 0;font-family:verdana;'>");
            rpt.Append("<tr>");
            rpt.AppendFormat("<td colspan='2' style='font-family:Verdana;border-bottom: 1px solid black; font-size:x-Medium;font-weight:bold;color:Blue; text-align:center'>Department :{0}</td>", dtxray.Rows[a]["DeptName"]);
            rpt.Append("</tr'>");
            rpt.Append("<tr>");
            rpt.AppendFormat("<td colspan='2' style='font-family:Verdana; border-bottom: 1px solid black;font-size:x-Medium;font-weight:bold; text-align:left'>Test Name :{0}</td>", dtxray.Rows[a]["TestName"]);
            rpt.Append("</tr'>");
            rpt.Append("<tr>");
            rpt.Append("<td style='font-family:Verdana;border-bottom: 1px solid black; font-size:x-Medium;font-weight:bold;text-align:left'>Result</td>");
            rpt.AppendFormat("<td style='font-family:Verdana;height:200px; border-bottom: 1px solid black;font-size:x-small;font-weight:bold; text-align:center'>{0}</td>", dtxray.Rows[a]["HeaderContent"]);

            rpt.Append("</tr'>");
            rpt.Append("</table'>");
            a++;

            rpt.Append("<br/>"); rpt.Append("<br/>"); rpt.Append("<br/>");
            rpt.Append("<table width='100%' cellspacing=0 border=0 style='padding: 0;font-family:verdana;'>");
            rpt.Append("<tr>");
            rpt.Append("<td style='font-family:Verdana;font-size:x-Medium;font-weight:bold; text-align:left'>Remarks / Impression: </td>");
            rpt.AppendFormat("<td style='font-family:Verdana;font-size:x-Medium;font-weight:bold; text-align:left'>{0}</td>", dtxray.Rows[a - 1]["Remarks"]);
            rpt.Append("</tr'>");
            rpt.Append("</table>");
            rpt.Append("<br/>"); rpt.Append("<br/>");
            a++;


             
        }
    }

    public void Radiology(string compcode, string yearcode, string code)
    {

        DataTable dtusg = theTestResult.GetUsgResult(compcode, yearcode, txtreg.Text, code);
        if (dtusg.Rows.Count > 0)
        {
            rpt.Append("<table width='100%' cellspacing=0 border=0 style='border:1px solid black;border-spacing: 0; padding: 0;font-family:verdana;'>");
            rpt.Append("<tr>");
            rpt.AppendFormat("<td style='font-family:Verdana;background-color:#9B9C8D;border-bottom: 1px solid black; font-size:x-Medium;font-weight:bold;text-align:center'>Department : RADIOLOGY</td>");
            rpt.AppendFormat("<td style='font-family:Verdana;background-color:#9B9C8D; border-bottom: 1px solid black;font-size:x-Medium;font-weight:bold; text-align:center'>Test Name :{0}</td>", dtusg.Rows[0]["Name"]);
            rpt.Append("</tr'>");
            rpt.Append("<tr>");
            rpt.Append("<td colspan='2' style='font-family:Verdana; border-bottom: 1px solid black;font-size:x-Medium;font-weight:bold; text-align:center'>HEADER DETAILS</td>");
            rpt.Append("</tr'>");
            rpt.Append("<tr>");
            rpt.Append("<td style='font-family:Verdana;border-bottom: 1px solid black; font-size:x-Medium;font-weight:bold;text-align:left'>Header Name</td>");
            rpt.Append("<td style='font-family:Verdana;border-bottom: 1px solid black; font-size:x-Medium;font-weight:bold;text-align:left'>Header Content</td>");
            rpt.Append("</tr'>");
        
            for (int p = 0; p < dtusg.Rows.Count; p++)
            {
                rpt.Append("<tr>");
                rpt.AppendFormat("<td style='font-family:Verdana; font-size:x-small;font-weight:bold; text-align:left'>{0}</td>", dtusg.Rows[p]["HeaderName"]);
                rpt.AppendFormat("<td style='font-family:Verdana;height:100px; font-size:x-small;font-weight:bold; text-align:left'>{0}</td>", dtusg.Rows[p]["hc"]);
                rpt.Append("</tr'>");
            }

            DataTable dtpara = theTestResult.GetUGCparameter(compcode, yearcode, txtreg.Text, code);
            if (dtpara.Rows.Count > 0)
            {
                rpt.Append("<tr>");
                rpt.Append("<td colspan='2' style='font-family:Verdana; border-bottom: 1px solid black; border-top: 1px solid black;font-size:x-Medium;font-weight:bold; text-align:center'>PARAMETER DETAILS</td>");
                rpt.Append("</tr'>");
                rpt.Append("<tr>");
                rpt.Append("<td style='font-family:Verdana;border-bottom: 1px solid black; font-size:x-Medium;font-weight:bold;text-align:left'>Parameter Name</td>");
                rpt.Append("<td style='font-family:Verdana;border-bottom: 1px solid black; font-size:x-Medium;font-weight:bold;text-align:left'>Value</td>");
                rpt.Append("</tr'>");

                for (int p = 0; p < dtpara.Rows.Count; p++)
                {
                    rpt.Append("<tr>");
                    rpt.AppendFormat("<td style='font-family:Verdana;  font-size:x-small;font-weight:bold; text-align:left'>{0}</td>", dtpara.Rows[p]["Parameter"]);
                    rpt.AppendFormat("<td style='font-family:Verdana; font-size:x-small;font-weight:bold;height:50px; text-align:left'>{0}</td>", dtpara.Rows[p]["value"]);
                    rpt.Append("</tr'>");
                }
            }
            rpt.Append("</table>");
            rpt.Append("<br/>"); rpt.Append("<br/>"); rpt.Append("<br/>");
            rpt.Append("<table width='100%' cellspacing=0 border=0 style='padding: 0;font-family:verdana;'>");
            rpt.Append("<tr>");
            rpt.Append("<td style='font-family:Verdana;font-size:x-Medium;font-weight:bold; text-align:left'>Remarks / Impression: </td>");
            rpt.AppendFormat("<td style='font-family:Verdana;font-size:x-Medium;font-weight:bold; text-align:left'>{0}</td>", dtusg.Rows[0]["Remarks"]);
            rpt.Append("</tr'>");
            rpt.Append("</table>");

            
        }
        
    }

    public void patientDts()
    {
         DataTable dt = theTestResult.GetPatientDetails(Session["CoCode"].ToString().Trim(), Session["YearCode"].ToString().Trim(),txtreg.Text.Trim()); //ds.Tables[0];
        
         rpt.Append("<br/>");
        rpt.Append("<table width='100%' cellspacing=0 border=0 style='border:1px solid black;border-spacing: 0; padding: 0;font-family:verdana;'>");
        rpt.Append("<tr style='height:30px'>");
        rpt.AppendFormat("<td colspan='4' style='width: 35%;border-bottom: 1px solid black; font-family:Verdana; font-size:x-Medium;background-color:#9B9C8D; font-weight:bold;text-align:center'>PATIENT DETAILS</td>");
        rpt.Append("</tr>");
        rpt.Append("<tr style='height:30px'>");
        rpt.Append("<td style='width: 5%;border-bottom: 1px solid black;border-right: 1px solid black; font-family:Verdana;font-weight:bold;background-color:beige; font-size:small; text-align:left'>Reg. No :</td>");
        rpt.AppendFormat("<td style='width: 7%;border-bottom: 1px solid black;border-right: 1px solid black; font-family:Verdana; font-size:small;text-align:left'>{0}</td>", dt.Rows[0]["PatientReg"]);
        rpt.Append("<td style='width: 5%; font-family:Verdana;background-color:beige;border-bottom: 1px solid black;border-right: 1px solid black;font-weight:bold;font-size:small; text-align:left'>Patient's Name :</td>");
        rpt.AppendFormat("<td style='width: 7%; font-family:Verdana;border-bottom: 1px solid black; font-size:small;text-align:left'>{0}</td>", dt.Rows[0]["patient_name"]);
        rpt.Append("</tr >");

        rpt.Append("<tr style='height:30px'>");
        rpt.Append("<td style='width: 5%;border-bottom: 1px solid black;border-right: 1px solid black; font-family:Verdana;font-weight:bold;background-color:beige; font-size:small; text-align:left'>C/O :</td>");
        rpt.AppendFormat("<td style='width: 7%;border-bottom: 1px solid black;border-right: 1px solid black; font-family:Verdana; font-size:small;text-align:left'>{0}</td>", dt.Rows[0]["guardian_name"]);
        rpt.Append("<td style='width: 5%; font-family:Verdana;background-color:beige;border-bottom: 1px solid black;border-right: 1px solid black;font-weight:bold;font-size:small; text-align:left'>Address :</td>");
        rpt.AppendFormat("<td style='width: 7%; font-family:Verdana;border-bottom: 1px solid black; font-size:small;text-align:left'>{0}</td>", dt.Rows[0]["vill_city"]);
        rpt.Append("</tr >");

        rpt.Append("<tr style='height:30px'>");
        rpt.Append("<td style='width: 5%; font-family:Verdana;border-bottom: 1px solid black;border-right: 1px solid black;background-color:beige;font-weight:bold; font-size:small; text-align:left'>Phone No :</td>");
        rpt.AppendFormat("<td  style='width: 7%; font-family:Verdana;border-bottom: 1px solid black;border-right: 1px solid black; font-size:small;text-align:left'>{0}</td>", dt.Rows[0]["PhNo1"]);
        rpt.Append("<td style='width: 5%; font-family:Verdana; font-size:small;border-bottom: 1px solid black;background-color:beige;border-right: 1px solid black;font-weight:bold; text-align:left'>Refer By :</td>");
        rpt.AppendFormat("<td style='width: 7%; font-family:Verdana;border-bottom: 1px solid black; font-size:small;text-align:left'>{0}</td>", dt.Rows[0]["ReferalName"]);
        rpt.Append("</tr>");
        rpt.Append("</table>");
    }


    public void GetHearder_Detail()
    {
            string compcode = Session["CoCode"].ToString().Trim();
            string yearcode = Session["YearCode"].ToString().Trim();
            DataTable dt = theTestResult.GetPatientDetails(compcode, yearcode,txtreg.Text.Trim()); //ds.Tables[0];
            int a = 0;
            Report_Header();
            patientDts();

            rpt.Append("<table width='100%' style='border-spacing: 0; padding: 0;font-family:verdana;'>");
            rpt.Append("<tr style='height:30px'>");
            rpt.AppendFormat("<td colspan='4' style='width: 35%;font-family:Verdana; font-size:x-Medium; font-weight:bold;text-align:center'>TEST RESULT DETAILS</td>");
            rpt.Append("</tr>");
            rpt.Append("</table>");

            if (DropDownList1.SelectedIndex == 0)
            {

                DataTable dmap = theTestResult.GetTestMap(HiddenField1.Value);
                for (int i = 0; i < dmap.Rows.Count; i++)
                {
                    
                    if (dmap.Rows[i]["Type"].ToString() == "P")
                    {
                        pathology(compcode, yearcode, dmap.Rows[i]["TestCode"].ToString());
                    }
                    if (dmap.Rows[i]["Type"].ToString() == "X")
                    {
                        Report_Header();
                        XRay(compcode, yearcode,a, dmap.Rows[i]["TestCode"].ToString());

                    }

                    if (dmap.Rows[i]["Type"].ToString() == "U")
                    {
                        Report_Header();
                        patientDts();
                        Radiology(compcode, yearcode, dmap.Rows[i]["TestCode"].ToString());
                    }


                    if (dmap.Rows[i]["Type"].ToString() == "G")
                    {
                        Group(compcode, yearcode);
                    }
                    rpt.Append("<style='page-break-after:always'>");

                }
            }
            else
            {
                string val = DropDownList1.SelectedValue.Substring(0, 2);
                if (val == "UG")
                {
                    //Report_Header();
                    //patientDts();
                    Radiology(compcode, yearcode, DropDownList1.SelectedValue);
                }
                if (val == "GC")
                {
                    //Report_Header();
                    //patientDts();
                    Group(compcode, yearcode);
                }
                if (val == "TC")
                {
                    //Report_Header();
                    //patientDts();
                    pathology(compcode, yearcode, DropDownList1.SelectedValue);
                }
                if (val == "TX")
                {
                    //Report_Header();
                    //patientDts();
                    XRay(compcode, yearcode, a, DropDownList1.SelectedValue);
                }
                
            }

            DataTable dtdoc = theTestResult.GetDoctors(compcode, yearcode, txtreg.Text, "P");
            DataTable dtdoc1 = theTestResult.GetDoctors(compcode, yearcode, txtreg.Text, "R");
            DataTable dtdoc2 = theTestResult.GetDoctors(compcode, yearcode, txtreg.Text, "X");
           
            //DocDtls(dtdoc);
            if (dtdoc.Rows.Count > 0)
            {
                DocDtls(dtdoc);
            }
            else if(dtdoc1.Rows.Count>0)
            {
                DocDtls(dtdoc1);
            }
            else if(dtdoc2.Rows.Count>0)
            {
                 DocDtls(dtdoc2);
            }
           /* DataTable dtdoc = theTestResult.GetDoctorusg(compcode, yearcode, txtreg.Text, DropDownList1.SelectedValue.ToString());
            DocDtls(dtdoc); */
            rpt.Append("<hr>"); 
    }


    protected void Button1_Click(object sender, EventArgs e)
    {
        DropDownFill();
        ltrReport.Text = "";
    }
}