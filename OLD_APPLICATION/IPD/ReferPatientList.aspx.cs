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

public partial class IPD_ReferPatientList : System.Web.UI.Page
{
    Main Objmain = new Main(ConfigurationSettings.AppSettings["ConnectionString"].ToString());
    ReferPatientList theHelper = new ReferPatientList(ConfigurationSettings.AppSettings["ConnectionString"].ToString());
    System.Text.StringBuilder rpt = new System.Text.StringBuilder();
    String Date = DateTime.Now.ToString("dd/MM/yyyy");

    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["userName"] == null)
        {
            Response.Redirect("../LoginPage.aspx");
        }
        if (Objmain.checkaccss(Session["CoCode"].ToString(), Session["UserRoleID"].ToString().Trim(), "PATIENT LIST", checkAccessType.ViewAction) == false)
        {
            Response.Redirect("../AccessDenied.aspx");
        }
        if (!IsPostBack)
        {
            System.Globalization.DateTimeFormatInfo dtf = new DateTimeFormatInfo();
            RadioButtonList1.SelectedValue = "With Header";
            DropDownList1.Items.Insert(0, new ListItem("--Select--", "0"));
            DropDownList1.Items.Insert(1, new ListItem("Doctor", "D"));
            DropDownList1.Items.Insert(2, new ListItem("Quack ", "Q"));
            DropDownList1.SelectedValue = "D";

            String Date = DateTime.Now.ToString("dd/MM/yyyy");
            string[] aa = Date.Split('/');
            string stday = "01";
            string month = aa[1];
            string year = aa[2];

            if (month.Length == 1)
                month = "0" + month;
            string frmdate = stday + "/" + month + "/" + year;
            //TextBox1.Text = DateTime.ParseExact(frmdate, "dd/MM/yyyy", dtf).ToString();
            TextBox1.Text = frmdate.ToString();

            
            DateTime today = DateTime.Today;
            int daysInMonth = DateTime.DaysInMonth(today.Year, today.Month);

            string todate = daysInMonth + "/" + month + "/" + year;
            //TextBox2.Text = DateTime.ParseExact(todate, "dd/MM/yyyy", dtf).ToString();
            TextBox2.Text = todate.ToString();
            //cmdBack.visible = false;
            //this.cmdPrint.visible = false;
            PopulateDoc("D");
        }

        Page.Title = "Patient List";
    }

    protected void Button1_Click(object sender, EventArgs e)
    {
        
       
            GetReport();
       
    }

    protected void DropDownList1_SelectedIndexChanged(object sender, EventArgs e)
    {

        PopulateDoc(DropDownList1.SelectedValue);
        
    }
   
    private void PopulateDoc( string type)
    {
        DropDownList2.Items.Clear();
        
        
        if (type == "D")
        {
            this.DropDownList2.DataSource = theHelper.getDoc("D",Session["CoCode"].ToString().Trim());
            this.DropDownList2.DataTextField = "doc_name";
            this.DropDownList2.DataValueField = "doc_id";
            this.DropDownList2.DataBind();
            this.DropDownList2.Items.Insert(0, new ListItem("--Select--", "0"));
        }
        else
        {
            if (type == "Q")
            {
                this.DropDownList2.DataSource = theHelper.getDoc("Q", Session["CoCode"].ToString().Trim());
                this.DropDownList2.DataTextField = "QuackName";
                this.DropDownList2.DataValueField = "QuackId";
                this.DropDownList2.DataBind();
                this.DropDownList2.Items.Insert(0, new ListItem("--Select--", "0"));
            }
            else
            {
                DropDownList2.Items.Clear();
            }
            
        }
        
    }

    public void GetReport()
    {
        if (DropDownList1.SelectedValue != "0" && DropDownList2.SelectedValue != "0")
        {
            if (RadioButtonList1.SelectedValue == "With Header")
            {
                Report_Header();
                GetHearder_Detail();
            }
            else
            {
                GetHearder_Detail();
            }
            ltrReport.Text = rpt.ToString();
        }
        else
        {
            ltrReport.Text = "";
        }
    }

    public void Report_Header()
    {

        rpt.Append("<br/>"); rpt.Append("<br/>"); rpt.Append("<br/>");
        rpt.Append("<table width='100%' cellspacing=0 border=0 style='border:1px solid black;background-color:#9B9C8D;border-spacing: 0; padding: 0;font-family:verdana;'>");
        rpt.Append("<tr cellpadding:'0'>");
        rpt.AppendFormat("<td rowspan='7' style='width: 35%; font-family:Verdana; font-size:x-Medium; font-weight:bold;text-align:left'><img src='../Images/logo.jpg'  style='height:87px;width:205px;border-style:inherit;border-color:#D3CDD4'/></td>");
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


    public void GetHearder_Detail()
    {
        System.Globalization.DateTimeFormatInfo dtf = new DateTimeFormatInfo();
        string frmdate="";
        string todate = "";
        if (TextBox1.Text != "")
        {
            DateTime testdate1 = DateTime.ParseExact(TextBox1.Text, "dd/MM/yyyy", dtf);
            frmdate = testdate1.ToString("yyyy-MM-dd");
            
        }
        else
            frmdate ="";

        if (TextBox2.Text != "")
        {
            DateTime testdate2 = DateTime.ParseExact(TextBox2.Text, "dd/MM/yyyy", dtf);
            todate = testdate2.ToString("yyyy-MM-dd");
        }
        else
            todate = "";


        DataTable dt = theHelper.GetPatientDtls(DropDownList2.SelectedValue.ToString(), frmdate, todate, Session["CoCode"].ToString().Trim());
        rpt.Append("<br/>");

        rpt.Append("<table width='100%' cellspacing=0 border=0 style='border:1px solid black;border-spacing: 0; padding: 0;font-family:verdana;'>");

        rpt.Append("<tr style='height:40px'>");
        rpt.Append("<td colspan='7' style='width: 8%;border-bottom: 1px solid black; font-family:Verdana;font-weight:bold;background-color:#9B9C8D;font-size:medium; text-align:Center'> PATIENT DETAILS  </td>");
        rpt.Append("</tr'>");

        rpt.Append("<tr style='height:30px'>");
        rpt.Append("<td style='width: 10%;border-bottom: 1px solid black;border-right: 1px solid black; font-family:Verdana;font-weight:bold;background-color:beige; font-size:small; text-align:center'>Registration No</td>");
        rpt.Append("<td style='width: 20%;border-bottom: 1px solid black;border-right: 1px solid black; font-family:Verdana;font-weight:bold;background-color:beige; font-size:small; text-align:center'>Patient's Name</td>");
        rpt.Append("<td style='width: 20%;border-bottom: 1px solid black;border-right: 1px solid black; font-family:Verdana;font-weight:bold;background-color:beige; font-size:small; text-align:center'>Guardian Name</td>");
        rpt.Append("<td style='width: 10%;border-bottom: 1px solid black;border-right: 1px solid black; font-family:Verdana;font-weight:bold;background-color:beige; font-size:small; text-align:center'>Admission Date</td>");
        rpt.Append("<td style='width: 20%;border-bottom: 1px solid black;border-right: 1px solid black; font-family:Verdana;font-weight:bold;background-color:beige; font-size:small; text-align:center'>Under Doctor</td>");
        rpt.Append("<td style='width: 10%;border-bottom: 1px solid black;border-right: 1px solid black; font-family:Verdana;font-weight:bold;background-color:beige; font-size:small; text-align:center'>Discharged</td>");
        rpt.Append("<td style='width: 10%;border-bottom: 1px solid black;border-right: 1px solid black; font-family:Verdana;font-weight:bold;background-color:beige; font-size:small; text-align:center'>Discharge Date</td>");
        rpt.Append("</tr >");
        string disc = "";
        string discdt = "";
        if (dt.Rows.Count > 0)
        {
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                if (dt.Rows[i]["discdt"].ToString() == "")
                {
                    disc = "";
                    discdt = "";
                }
                else
                {
                    disc = "Discharged";
                    discdt = dt.Rows[i]["discdt"].ToString();
                }
                rpt.Append("<tr style='height:30px'>");
                rpt.AppendFormat("<td style='width: 10%;border-bottom: 1px solid black;border-right: 1px solid black; font-family:Verdana;background-color:beige; font-size:small; text-align:left'>{0}</td>",dt.Rows[i]["PatientReg"]);
                rpt.AppendFormat("<td style='width: 20%;border-bottom: 1px solid black;border-right: 1px solid black; font-family:Verdana;background-color:beige; font-size:small; text-align:left'>{0}</td>",dt.Rows[i]["patient_name"]);
                rpt.AppendFormat("<td style='width: 20%;border-bottom: 1px solid black;border-right: 1px solid black; font-family:Verdana;background-color:beige; font-size:small; text-align:left'>{0}</td>", dt.Rows[i]["guardian_name"]);
                rpt.AppendFormat("<td style='width: 10%;border-bottom: 1px solid black;border-right: 1px solid black; font-family:Verdana;background-color:beige; font-size:small; text-align:center'>{0}</td>", dt.Rows[i]["ADate"]);
                rpt.AppendFormat("<td style='width: 20%;border-bottom: 1px solid black;border-right: 1px solid black; font-family:Verdana;background-color:beige; font-size:small; text-align:left'>{0}</td>", dt.Rows[i]["doctor"]);
                rpt.AppendFormat("<td style='width: 10%;border-bottom: 1px solid black;border-right: 1px solid black; font-family:Verdana;background-color:beige; font-size:small; text-align:left'>{0}</td>", disc);
                rpt.AppendFormat("<td style='width: 10%;border-bottom: 1px solid black;border-right: 1px solid black; font-family:Verdana;background-color:beige; font-size:small; text-align:center'>{0}</td>", discdt);
                rpt.Append("</tr >");
            }
        }
        rpt.Append("</table>");
        ltrReport.Visible = true;
    }
}