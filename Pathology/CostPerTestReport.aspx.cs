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
 
public partial class Pathology_CostPerTestReport : System.Web.UI.Page
{
    Main Objmain = new Main(ConfigurationSettings.AppSettings["ConnectionString"].ToString());

    System.Text.StringBuilder rpt = new System.Text.StringBuilder();
    PH_CostPerTestReport thedreport = new PH_CostPerTestReport(ConfigurationSettings.AppSettings["ConnectionString"].ToString());
    String Date = DateTime.Now.ToString("dd/MM/yyyy");
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["userName"] == null)
        {
            Response.Redirect("../LoginPage.aspx");
        }
        if (Objmain.checkaccss(Session["CoCode"].ToString(), Session["UserRoleID"].ToString().Trim(), "COST PER TEST", checkAccessType.ViewAction) == false)
        {
            Response.Redirect("../AccessDenied.aspx");
        }
        Page.Title = "Cost Per Test";
        if (Session["userName"] == null)
        {
            Response.Redirect("../LoginPage.aspx");
        }
        if (!IsPostBack)
        {
            DropDownFill();
            GetReport();
        }
        DateTime dt1 = DateTime.Now.AddMonths(4);
       
    }
    public void DropDownFill()
    {
          this.DropDownList1.DataSource = thedreport.DropDownFill();
        this.DropDownList1.DataTextField = "DeptName";
        this.DropDownList1.DataValueField = "DeptCode";
        this.DropDownList1.DataBind();
        this.DropDownList1.Items.Insert(0, new ListItem("All", "0"));

    }

    public void GetReport()
    {
        Report_Header();
        GetHearder_Detail();
        ltrReport.Text = rpt.ToString();



    }
    public void Report_Header()
    {
        rpt.Append("<br/>"); rpt.Append("<br/>"); rpt.Append("<br/>");
        rpt.Append("<table width='100%' cellspacing=0 border=0 style='border-spacing: 0; padding: 0;font-family:verdana;'>");
        rpt.Append("<tr cellpadding:'0'>");
        rpt.AppendFormat("<td rowspan='4' style='width: 35%; font-family:Verdana; font-size:x-Medium; font-weight:bold;text-align:left'><img src='../Images/logo.jpg'  style='height:87px;width:205px;border-style:inherit;border-color:#D3CDD4'/></td>");
        rpt.AppendFormat("<td style='width: 35%; font-family:Verdana; font-size:large; font-weight:bold;text-align:center'>{0}</td>", "");
        rpt.AppendFormat("<td style='width: 35%; font-family:Verdana; font-size:x-Medium; font-weight:bold;text-align:center'>&nbsp;</td>");
        rpt.Append("</tr>");
        //rpt.Append("<tr>");
        //rpt.AppendFormat("<td style='width: 35%; font-family:Verdana; font-size:x-Medium; font-weight:bold;text-align:center'>{0}</td>", "REG NO : NH-315/G-70/2013");
        //rpt.AppendFormat("<td style='width: 35%; font-family:Verdana; font-size:x-Medium; font-weight:bold;text-align:center'>&nbsp;</td>");
        //rpt.Append("</tr>");
        //rpt.Append("<tr>");
        //rpt.AppendFormat("<td style='width: 35%; font-family:Verdana; font-size:x-Medium; font-weight:bold;text-align:center'>{0}</td>", "KUSHPATA,GHATAL,MIDNAPUR(W),WB,721212");
        //rpt.AppendFormat("<td style='width: 35%; font-family:Verdana; font-size:x-Medium; font-weight:bold;text-align:center'>&nbsp;</td>");
        //rpt.Append("</tr>");
        //rpt.Append("<tr>");
        //rpt.AppendFormat("<td style='width: 35%; font-family:Verdana; font-size:x-Medium; font-weight:bold;text-align:center'>{0}</td>", "Ph :(03225)244400/244643  M:9434419825");
        //rpt.AppendFormat("<td style='width: 35%; font-family:Verdana; font-size:x-Medium; font-weight:bold;text-align:center'>&nbsp;</td>");
        //rpt.Append("</tr>");
        rpt.Append("</table>");
    }

    public void GetHearder_Detail()
    {
         DataTable dt1 = thedreport.GetDepartmentCode();
       
            rpt.Append("<br/>");
            rpt.Append("<table width='100%' cellspacing=0 border=0 style='border:1px solid black;border-spacing: 0; padding: 0;font-family:verdana;'>");

            rpt.Append("<tr style='height:40px'>");
            rpt.Append("<td colspan='4' style='width: 8%;border-bottom: 1px solid black; font-family:Verdana;font-weight:bold;background-color:#D2D2CD;font-size:medium; text-align:Center'>  TEST REPORT DETAILS  </td>");
            rpt.Append("</tr'>");
            for (int j = 0; j < dt1.Rows.Count; j++)
            {
                rpt.Append("<tr style='height:40px'>");
                rpt.AppendFormat("<td colspan=2 style='width: 7%;font-family:Verdana;border-bottom: 1px solid black;background-color:#FABC84; font-size:large;text-align:center'>Department  : {0}</td>", dt1.Rows[j]["DeptName"]);
                rpt.Append("</tr>");
                rpt.Append("<tr style='height:30px'>");
                rpt.Append("<td style='width: 8%;border-bottom: 1px solid black;border-right: 1px solid black; font-family:Verdana;font-weight:bold;background-color:beige; font-size:x-small; text-align:center'>Test Name</td>");
                rpt.Append("<td style='width: 5%; font-family:Verdana;background-color:beige;border-bottom: 1px solid black;font-weight:bold;font-size:x-small; text-align:center'>Cost of Test</td>");
                rpt.Append("</tr >");
                 DataTable dt = thedreport.Gettestdtls(dt1.Rows[j]["DepertmentID"].ToString(),TextBox1.Text); //ds.Tables[0];
     
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    rpt.Append("<tr >");
                    rpt.AppendFormat("<td style='width: 5%;border-bottom: 1px solid black;border-right: 1px solid black; font-family:Verdana; font-size:x-small;text-align:center'>{0}</td>", dt.Rows[i]["TestName"]);
                    rpt.AppendFormat("<td style='width: 7%; font-family:Verdana;border-bottom: 1px solid black;font-size:x-small;text-align:center'>{0}</td>", dt.Rows[i]["Cost"]);
                    rpt.Append("</tr >");
                }
                DataTable dtgr = thedreport.GetGroup();
                if (dt1.Rows[j]["DepertmentID"].ToString() != "D0001")
                    continue;
                else
                {
                    for (int k = 0; k < dtgr.Rows.Count; k++)
                    {
                        rpt.Append("<tr >");
                        rpt.AppendFormat("<td style='width: 5%;border-bottom: 1px solid black;border-right: 1px solid black; font-family:Verdana; font-size:x-small;text-align:center'>{0}</td>", dtgr.Rows[k]["ProfileName"]);
                        rpt.AppendFormat("<td style='width: 7%; font-family:Verdana;border-bottom: 1px solid black;font-size:x-small;text-align:center'>{0}</td>", dtgr.Rows[k]["Price"]);
                        rpt.Append("</tr >");
                    }
                }
            }
            GetHearder_Radilogy("1");
           rpt.Append("<hr>");

    }

    public void GetHearder_Detail1(string dept)
    {
         DataTable dt = thedreport.Gettestdtls(dept,TextBox1.Text); //ds.Tables[0];
        if (dt.Rows.Count > 0)
        {
            rpt.Append("<br/>");
            rpt.Append("<table width='100%' cellspacing=0 border=0 style='border:1px solid black;border-spacing: 0; padding: 0;font-family:verdana;'>");
            rpt.Append("<tr style='height:40px'>");
            rpt.Append("<td colspan='4' style='width: 8%;border-bottom: 1px solid black; font-family:Verdana;font-weight:bold;background-color:#D2D2CD;font-size:medium; text-align:Center'>  TEST REPORT DETAILS  </td>");
            rpt.Append("</tr'>");
            rpt.Append("<tr style='height:40px'>");
            rpt.AppendFormat("<td colspan=2 style='width: 7%;font-family:Verdana;border-bottom: 1px solid black;background-color:#FABC84; font-size:large;text-align:center'>Department  : {0}</td>", dt.Rows[0]["DeptName"]);
            rpt.Append("</tr>");
            rpt.Append("<tr style='height:30px'>");
            rpt.Append("<td style='width: 8%;border-bottom: 1px solid black;border-right: 1px solid black; font-family:Verdana;font-weight:bold;background-color:beige; font-size:x-small; text-align:center'>Test Name</td>");
            rpt.Append("<td style='width: 5%; font-family:Verdana;background-color:beige;border-bottom: 1px solid black;font-weight:bold;font-size:x-small; text-align:center'>Cost of Test</td>");
            rpt.Append("</tr >");
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                rpt.Append("<tr >");
                rpt.AppendFormat("<td style='width: 5%;border-bottom: 1px solid black;border-right: 1px solid black; font-family:Verdana; font-size:x-small;text-align:center'>{0}</td>", dt.Rows[i]["TestName"]);
                rpt.AppendFormat("<td style='width: 7%; font-family:Verdana;border-bottom: 1px solid black;font-size:x-small;text-align:center'>{0}</td>", dt.Rows[i]["Cost"]);
                rpt.Append("</tr >");
            }
            DataTable dtgr = thedreport.GetGroup();
            for (int k = 0; k < dtgr.Rows.Count; k++)
                {
                    rpt.Append("<tr >");
                    rpt.AppendFormat("<td style='width: 5%;border-bottom: 1px solid black;border-right: 1px solid black; font-family:Verdana; font-size:x-small;text-align:center'>{0}</td>", dtgr.Rows[k]["ProfileName"]);
                    rpt.AppendFormat("<td style='width: 7%; font-family:Verdana;border-bottom: 1px solid black;font-size:x-small;text-align:center'>{0}</td>", dtgr.Rows[k]["Price"]);
                    rpt.Append("</tr >");
                }
            
        }
      
        rpt.Append("<hr>");

    }

    public void GetHearder_Radilogy(string a)
    {
         DataTable dt = thedreport.GetRadiology();
        rpt.Append("<table width='100%' cellspacing=0 border=0 style='border:1px solid black;border-spacing: 0; padding: 0;font-family:verdana;'>");
        if (a == "2")
        {
            rpt.Append("<tr style='height:40px'>");
            rpt.Append("<td colspan='3' style='width: 8%;border-bottom: 1px solid black; font-family:Verdana;font-weight:bold;background-color:#D2D2CD;font-size:medium; text-align:Center'>  TEST REPORT DETAILS  </td>");
            rpt.Append("</tr'>");
        }
        if (dt.Rows.Count > 0)
        {
            rpt.Append("<br/>");
            rpt.Append("<tr style='height:40px'>");
            rpt.AppendFormat("<td colspan=3 style='width: 7%;font-family:Verdana;border-bottom: 1px solid black;background-color:#FABC84; font-size:large;text-align:center'>Department  : RADIOLOGY</td>");
            rpt.Append("</tr>");
            rpt.Append("<tr style='height:30px'>");
            rpt.Append("<td style='width: 8%;border-bottom: 1px solid black;border-right: 1px solid black; font-family:Verdana;font-weight:bold;background-color:beige; font-size:x-small; text-align:center'>Test Name</td>");
            rpt.Append("<td style='width: 5%; font-family:Verdana;background-color:beige;border-bottom: 1px solid black;font-weight:bold;font-size:x-small; text-align:center'>Cost of Test</td>");
            rpt.Append("</tr >");
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                rpt.Append("<tr >");
                rpt.AppendFormat("<td style='width: 5%;border-bottom: 1px solid black;border-right: 1px solid black; font-family:Verdana; font-size:x-small;text-align:center'>{0}</td>", dt.Rows[i]["Name"]);
                rpt.AppendFormat("<td style='width: 7%; font-family:Verdana;border-bottom: 1px solid black;font-size:x-small;text-align:center'>{0}</td>", dt.Rows[i]["Charges"]);
                rpt.Append("</tr >");
            }
        }

        rpt.Append("<hr>");

    }
    protected void DropDownList1_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (DropDownList1.SelectedIndex == 0)
            GetReport();
        else
            if (DropDownList1.SelectedValue == "D0002")
                GetHearder_Radilogy("2");
        else
        {
            Report_Header();
            GetHearder_Detail1(DropDownList1.SelectedValue);
        }
        ltrReport.Text = rpt.ToString();

    }
    protected void Button1_Click(object sender, EventArgs e)
    {
        GetReport(); 
    }
}