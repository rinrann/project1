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
using System.Web.Services;
using System.Globalization;
using System.Net.Mail;
using System.Net;
using System.IO;
using System.Text;
using System.Collections.Generic;

public partial class OPD_PatientQueue : System.Web.UI.Page
{
    Main Objmain = new Main(ConfigurationSettings.AppSettings["ConnectionString"].ToString());
    PatientHistory theabortion = new PatientHistory(ConfigurationSettings.AppSettings["ConnectionString"].ToString());
    System.Text.StringBuilder rpt = new System.Text.StringBuilder();
    String Date = DateTime.Now.ToString("dd/MM/yyyy");

    protected void Page_Load(object sender, EventArgs e)
    {
        Page.Title = "Patient Queue";
        if (Session["userId"] == null)
        {
            Response.Redirect("../LoginPage.aspx");
        }
        if (Objmain.checkaccss(Session["CoCode"].ToString(), Session["UserRoleID"].ToString().Trim(), "PATIENT QUEUE LIST", checkAccessType.ViewAction) == false)
        {
            Response.Redirect("../AccessDenied.aspx");
        }

        if (!IsPostBack)
        {
            TextBox3.Text = DateTime.Now.ToString("yyyy-MM-dd");
            
        }
    }


    protected void Button1_Click(object sender, EventArgs e)
    {
        Report_Header();
        GetHearder_Detail();
        ltrReport.Text = rpt.ToString();
    }


    public void Report_Header()
    {
        System.Globalization.DateTimeFormatInfo dtf = new DateTimeFormatInfo();
        string date = DateTime.Now.ToString("dd/MM/yyyy");
        
        if (RadioButtonList1.SelectedValue == "Y")
        {
            rpt.Append("<table width='100%' cellspacing=0 border=0 style='border:1px solid black;border-spacing: 0; padding: 0;font-family:verdana;'>");
            rpt.Append("<tr cellpadding:'0'>");
            rpt.Append("<td width='30%'><img src='../Images/logo.jpg'  style=''/></td>");
            rpt.Append("<td   width='40%'  style='height:18px;font-family:Arial; font-size:medium; text-align:center'></td>");
            rpt.Append("<td width='30%' style='text-align:right'></td>");
            rpt.Append("</tr>");
            rpt.Append("<tr cellpadding:'0'>");
            rpt.Append("<td width='30%'></td>");
            rpt.Append("<td   width='40%'  style='height:20px;font-family:Arial; font-size:medium; text-align:center'><b><u>PATIENT QUEUE</u></b></td>");
            rpt.Append("<td width='30%' style='text-align:right;'>Print Date : " + date + "</td>");
            rpt.Append("</tr>");
            rpt.Append("</table>");
        }
        else
        {
            rpt.Append("<table width='100%' cellspacing=0 border=0 style='border-spacing: 0; padding: 0;font-family:verdana;'>");
            rpt.Append("<tr cellpadding:'0'>");
            rpt.Append("<td width='30%' style='height:150px;'>&nbsp;</td>");
            rpt.Append("</tr>");
            rpt.Append("</table>");
        }
        
    }
    public void GetHearder_Detail()
    {
        ltrReport.Text = "";
        DataTable dt = theabortion.GetPatientQueue(Session["CoCode"].ToString(), TextBox3.Text,txtDocId.Text.Trim());

        rpt.Append("<table width='100%' cellspacing=0 border=0 style='border:1px solid black;border-spacing: 0; padding: 0;font-family:verdana;'>");
        if (dt.Rows.Count > 0)
        {
            rpt.Append("<tr style='height:40px;'>");
            rpt.Append("<td style='width:7%;font-weight:bold;background-color:beige; font-size:small; text-align:left;border-bottom: 1px solid black;'>SRL No.</td>");
            rpt.Append("<td style='width:15%;font-weight:bold;background-color:beige; font-size:small; text-align:left;border-bottom: 1px solid black;'>DATE</td>");
            rpt.Append("<td style='width:30%;font-weight:bold;background-color:beige; font-size:small; text-align:left;border-bottom: 1px solid black;'>DOCTOR NAME</td>");
            rpt.Append("<td style='width:30%;font-weight:bold;background-color:beige; font-size:small; text-align:left;border-bottom: 1px solid black;'>PATIENT NAME</td>");
            rpt.Append("<td style='width:18%;font-weight:bold;background-color:beige; font-size:small; text-align:left;border-bottom: 1px solid black;'>Patient Id</td>");
            rpt.Append("</tr>");
            int srl=0;
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                srl = srl + 1;
                rpt.Append("<tr>");
                rpt.AppendFormat("<td style='font-family:Verdana; font-size:small;text-align:left'>{0}</td>", srl);
                rpt.AppendFormat("<td style='font-family:Verdana; font-size:small;text-align:left'>{0}</td>", dt.Rows[i]["DispBillDate"] + " " + dt.Rows[i]["DispBillTime"]);
                rpt.AppendFormat("<td style='font-family:Verdana; font-size:small;text-align:left'>{0}</td>", dt.Rows[i]["DocName"]);
                rpt.AppendFormat("<td style='font-family:Verdana; font-size:small;text-align:left'>{0}</td>", dt.Rows[i]["PatientName"]);
                rpt.AppendFormat("<td style='font-family:Verdana; font-size:small;text-align:left'>{0}</td>", dt.Rows[i]["RegNo"]);
                rpt.Append("</tr>");
            }
        }
        else
        {
            rpt.Append("<tr style='height:40px'>");
            rpt.Append("<td colspan='5' width='100%' style='font-family:Verdana;font-weight:bold;font-size:small; text-align:center'>No Data Found</td>");
            rpt.Append("</tr>");
        }
        rpt.Append("</table>");
    }


    [System.Web.Script.Services.ScriptMethod()]
    [System.Web.Services.WebMethod]
    public static List<string> SearchDoctorName(string prefixText, int count)
    {
        string compcode = string.Empty;
        string yearcode = string.Empty;
        using (SqlConnection conn = new SqlConnection())
        {
            conn.ConnectionString = ConfigurationSettings.AppSettings["ConnectionString"].ToString();
            using (SqlCommand cmd = new SqlCommand())
            {
                compcode = HttpContext.Current.Session["CoCode"].ToString();
                yearcode = HttpContext.Current.Session["YearCode"].ToString();
                cmd.CommandText = "select doc_id + '~' + doc_name as Name from GN_DoctorMaster where compcode=@Compcode and doc_name like '%'+@SearchText + '%'";
                cmd.Parameters.AddWithValue("@SearchText", prefixText);
                cmd.Parameters.AddWithValue("@Compcode", compcode);
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