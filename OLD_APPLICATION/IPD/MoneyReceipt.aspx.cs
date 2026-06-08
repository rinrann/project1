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
public partial class IPD_MoneyReceipt : System.Web.UI.Page
{
    Main Objmain = new Main(ConfigurationSettings.AppSettings["ConnectionString"].ToString());
    Eclamsia12 theabortion = new Eclamsia12(ConfigurationSettings.AppSettings["ConnectionString"].ToString());
    AnyTimePayment theanytimepayment = new  AnyTimePayment(ConfigurationSettings.AppSettings["ConnectionString"].ToString());
    System.Text.StringBuilder rpt = new System.Text.StringBuilder();
    String Date = DateTime.Now.ToString("dd/MM/yyyy");
    protected void Page_Load(object sender, EventArgs e)
    {
        Page.Title = "Money Receipt";
        if (Session["userName"] == null)
        {
            Response.Redirect("../LoginPage.aspx");
        }
        if (Objmain.checkaccss(Session["CoCode"].ToString(), Session["UserRoleID"].ToString().Trim(), "MONEY RECEIPT", checkAccessType.ViewAction) == false)
        {
            Response.Redirect("../AccessDenied.aspx");
        }

        if (!IsPostBack)
        {
            Panel1.Visible = false;
            Button1.Enabled = false;
        }
    }
    protected void Button4_Click(object sender, EventArgs e)
    {
        DataTable dt = theanytimepayment.MoneyReceiptGridFill(txtreg.Text, Session["CoCode"].ToString().Trim(), Session["YearCode"].ToString().Trim());
        GridView1.DataSource = dt;
        GridView1.DataBind();
        if (dt.Rows.Count > 0)
        {
            Panel1.Visible = true;
            Button1.Enabled = true;
        }
        else
            Panel1.Visible = false;
    }

    public void GetReport()
    {
        int flag = 0;
        for (int i = 0; i < GridView1.Rows.Count; i++)
        {
            CheckBox chk = (CheckBox)GridView1.Rows[i].Cells[1].FindControl("CheckBox1");
            Label lblReceiptNo = (Label)GridView1.Rows[i].Cells[2].FindControl("lblReceiptNo");
            Label lblLedgerName = (Label)GridView1.Rows[i].Cells[2].FindControl("lblLedgerName");
            Label lblCredit = (Label)GridView1.Rows[i].Cells[2].FindControl("lblCredit");
            Label lblDate1 = (Label)GridView1.Rows[i].Cells[2].FindControl("lblDate1");
            Label Mode = (Label)GridView1.Rows[i].Cells[2].FindControl("lblPaymentMode");
            Label lblReason = (Label)GridView1.Rows[i].Cells[2].FindControl("lblReason");
            Label ReceiptNo = (Label)GridView1.Rows[i].Cells[2].FindControl("lblReason");
            Label lblAmountinWords = (Label)GridView1.Rows[i].Cells[2].FindControl("lblAmountinWords");
            if (chk.Checked == true)
            {
                flag = 1;
                Report_Header();
                GetHearder_Detail(lblAmountinWords.Text, lblLedgerName.Text, lblCredit.Text, lblDate1.Text, Mode.Text, lblReason.Text, lblReceiptNo.Text);
            }
        }
        if (flag == 1)
            ltrReport.Text = rpt.ToString();
        else
            ScriptManager.RegisterStartupScript(this, this.GetType(), "msg", "alert('Please Select One Row for Print !');", true);
    }

    public void GetHearder_Detail(string AmountinWords, string name, string amount, string date, string mode, string reason, string ReceiptNo)
    {
        ltrReport.Text = "";

        rpt.Append("<br/>");
        rpt.Append("<table width='100%' cellspacing=0 border=0 style=' border-spacing: 0; padding: 0;font-family:verdana;'>");

        rpt.Append("<tr style='height:40px'>");
        rpt.Append("<td colspan='6' style='width: 8%; font-family:Arial;font-weight:bold; font-size:larger; text-align:center'><u> Money Receipt </u> </td>");
        rpt.Append("</tr'>");
        rpt.Append("<tr style='height:40px'>");
        rpt.Append("<td colspan='6' style='width: 8%; font-family:Arial;font-weight:bold; font-size:larger; text-align:left'><br/><br/> </td>");
        rpt.Append("</tr'>");
        rpt.Append("<tr style='height:40px'>");
        rpt.AppendFormat("<td colspan='6' style='width: 8%; font-family:Arial;font-weight:bold; font-size:larger; text-align:left'> Receipt No :{0} </td>", ReceiptNo);
        rpt.Append("</tr'>");

        rpt.Append("<tr style='height:40px'>");
        rpt.AppendFormat("<td colspan='6' style='width: 8%; font-family:Arial;font-weight:bold; font-size:larger; text-align:left'> Registration No :{0} </td>", txtreg.Text);
        rpt.Append("</tr'>");

        rpt.Append("<tr style='height:40px'>");
        rpt.AppendFormat("<td colspan='6' style='width: 8%; font-family:Arial;font-weight:bold; font-size:larger; text-align:left'> Date : {0}</td>", DateTime.Now.ToString("dd/MM/yyyy"));
        rpt.Append("</tr'>");
        rpt.Append("</table>");


        rpt.Append("<table width='100%' cellspacing=0 border=0 style=' border-spacing: 0; padding: 0;font-family:verdana;'>");
        rpt.Append("<tr style='height:35px'>");
        rpt.AppendFormat("<td style='font-family:Arial;font-size:small; text-align:justify'>Received from <b>{0} Rs. {1} ( {2} )</b> for <b>{3}</b> on <b>{4} .</b></td>", name, amount,AmountinWords, reason,date);
        rpt.Append("</tr >");
        rpt.Append("</table>");

        rpt.Append("<table width='100%' cellspacing=0 border=0 style=' border-spacing: 0; padding: 0;font-family:verdana;'>");

        rpt.Append("<tr style='height:35px'>");
        rpt.AppendFormat("<td style='font-family:Arial;font-size:small; text-align:left'>Mode Of Payment : <b>{0}</b></td>",mode);
        rpt.Append("</tr >");

        rpt.Append("</table>");

        rpt.Append("<br/>"); rpt.Append("<br/>");
        rpt.Append("<table width='100%' cellspacing=0 border=0 style=' border-spacing: 0; padding: 0;font-family:verdana;'>");
        rpt.Append("<tr>");
        rpt.Append("<td style='font-family:Arial;font-size:medium;font-weight:bold; text-align:left'> Signature of Party </td>");
        rpt.Append("<td style='font-family:Arial;font-size:medium;font-weight:bold; padding-right:100px;text-align:right'> For GFC </td>");
        rpt.Append("</tr>");
        rpt.Append("</table>");
        rpt.Append("<br/>");

        ltrReport.Visible = true;


    }



    public void Report_Header()
    {
        rpt.Append("<table width='100%' cellspacing=0 border=0 style='border:1px solid black;border-spacing: 0; padding: 0;font-family:Arial;'>");
        rpt.Append("<tr cellpadding:'0'  style='height:30px;'>");
        rpt.Append("<td rowspan='3' width='2%' style='font-family:Arial; font-size:x-large; font-weight:bold;padding-left:100px;text-align:left'><img src='../Images/gfclogo.png'  style='height:77px;width:75px;'/></td>");
        rpt.Append("<td   width='20%'  style='height:20px;font-family:Arial; font-size:x-large; font-weight:bold;text-align:center'><u>GFC HOSPITAL</u></td>");
        rpt.Append("<td rowspan='3'  width='2%' style='height:20px;font-family:Arial; font-size:x-large;padding-right:100px; font-weight:bold;text-align:right'><img src='../Images/gfclogo.png'  style='height:77px;width:75px;'/></td>");
        rpt.Append("</tr>");

        rpt.Append("<tr  style='height:20px;'>");
        rpt.AppendFormat("<td   width='20%'  style='height:10px;font-family:Arial; font-size:small; font-weight:bold;text-align:center'>(Regn. No : NH-315/G-70/2013)</td>");
        rpt.Append("</tr>");

        rpt.Append("<tr  style='height:10px;'>");
        rpt.AppendFormat("<td  width='70%'  style='height:15px;font-family:Arial; font-size:medium; font-weight:bold;text-align:center'>Kushpata, Ghatal, Paschim Medinipur, WB, 721212</td>");

        rpt.Append("</tr>");
        rpt.Append("</table>");
    }
     
  
     
    protected void Button1_Click(object sender, EventArgs e)
    {
        GetReport();
        ltrReport.Text = rpt.ToString();
    }
}