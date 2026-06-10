using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Data;
using System.Configuration;
using System.IO;
using ClosedXML.Excel;
using System.Threading;

public partial class Bill_DailyCollection : System.Web.UI.Page
{
    Main Objmain = new Main(ConfigurationSettings.AppSettings["ConnectionString"].ToString());
    PH_Report objreport = new PH_Report(ConfigurationSettings.AppSettings["ConnectionString"].ToString());
    System.Text.StringBuilder rpt = new System.Text.StringBuilder();
    DataTable dt = new DataTable();

    int i = 0;


    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["userName"] == null)
        {
            Response.Redirect("../LoginPage.aspx");
        }
        if (Objmain.checkaccss(Session["CoCode"].ToString(), Session["UserRoleID"].ToString().Trim(), "DAILY COLLECTION", checkAccessType.ViewAction) == false)
        {
            Response.Redirect("../AccessDenied.aspx");
        }
        if (!IsPostBack)
        {
            //bind_report();
            txtfromdt.Text = DateTime.Now.ToString("yyyy-MM-dd");
        }
    }

    protected void btnGenRpt_Click(object sender, EventArgs e)
    {
        bind_report();
        
    }

    public void bind_report()
    {
        //string fromdate = DateTime.Now.ToString("yyyy-MM-dd");
        //string todate = DateTime.Now.ToString("yyyy-MM-dd");
        string fromdate=txtfromdt.Text;
        string todate = txtfromdt.Text;

        string[] frmdt = fromdate.Split('-');
        //string fromdatestr = frmdt[2] + "/" + frmdt[1]+;
        string fromdatestr = fromdate;
        decimal totamtBillCash = 0, totamtBillCard = 0, totamtBillWallet = 0, totamtBillNetBank = 0, gtotamtBill = 0;
        decimal totamtBillCashRef = 0, totamtBillCardRef = 0, totamtBillWalletRef = 0, totamtBillNetBankRef = 0, gtotamtBillRef = 0;
        Decimal netcollection = 0;
        int srl = 1, refsrl=1;

        DataTable dtCash = objreport.GetDateWiseCollection(Session["CoCode"].ToString().Trim(), Session["YearCode"].ToString().Trim(), fromdate, todate, "C", "P", Session["userName"].ToString());
        DataTable dtCard = objreport.GetDateWiseCollection(Session["CoCode"].ToString().Trim(), Session["YearCode"].ToString().Trim(), fromdate, todate, "R", "P", Session["userName"].ToString());
        DataTable dtEWallet = objreport.GetDateWiseCollection(Session["CoCode"].ToString().Trim(), Session["YearCode"].ToString().Trim(), fromdate, todate, "E", "P", Session["userName"].ToString());
        DataTable dtNetBank = objreport.GetDateWiseCollection(Session["CoCode"].ToString().Trim(), Session["YearCode"].ToString().Trim(), fromdate, todate, "N", "P", Session["userName"].ToString());


        DataTable dtCashRef = objreport.GetDateWiseCollection(Session["CoCode"].ToString().Trim(), Session["YearCode"].ToString().Trim(), fromdate, todate, "C", "R", Session["userName"].ToString());
        DataTable dtCardRef = objreport.GetDateWiseCollection(Session["CoCode"].ToString().Trim(), Session["YearCode"].ToString().Trim(), fromdate, todate, "R", "R", Session["userName"].ToString());
        DataTable dtEWalletRef = objreport.GetDateWiseCollection(Session["CoCode"].ToString().Trim(), Session["YearCode"].ToString().Trim(), fromdate, todate, "E", "R", Session["userName"].ToString());
        DataTable dtNetBankRef = objreport.GetDateWiseCollection(Session["CoCode"].ToString().Trim(), Session["YearCode"].ToString().Trim(), fromdate, todate, "N", "R", Session["userName"].ToString());


        rpt.Append("<table width='100%' cellspacing=0 border=0 style=' border-spacing: 0; padding: 0;font-family:Courier New;font-size:small;'>");
        //rpt.Append("<tr>");
        //rpt.AppendFormat("<td colspan='2' style='width: 100%; font-family:Courier New; font-size:x-large; text-align:center'><u>{0}</u></td>", "Daily collection Report");
        //rpt.Append("</tr>");
        rpt.Append("<tr>");
        rpt.AppendFormat("<td style='width: 10%; font-family:Courier New; font-size:large; text-align:left;background-color:burlywood;'>{0}</td>", "From Date");
        rpt.AppendFormat("<td style='width: 90%; font-family:Courier New; font-size:large; text-align:left'>{0}</td>", fromdatestr);
        rpt.Append("</tr>");
        rpt.Append("<tr>");
        rpt.AppendFormat("<td style='width: 10%; font-family:Courier New; font-size:large; text-align:left;background-color:burlywood;'>{0}</td>", "To Date");
        rpt.AppendFormat("<td style='width: 90%; font-family:Courier New; font-size:large; text-align:left'>{0}</td>", fromdatestr);
        rpt.Append("</tr>");
        rpt.Append("<tr>");
        rpt.AppendFormat("<td style='width: 10%; font-family:Courier New; font-size:large; text-align:left;background-color:burlywood;'>{0}</td>", "User Name");
        rpt.AppendFormat("<td style='width: 90%; font-family:Courier New; font-size:large; text-align:left'>{0}</td>", Session["userName"].ToString());
        rpt.Append("</tr>");
        rpt.Append("<tr>");
        rpt.AppendFormat("<td colspan='2' style='width: 100%; font-family:Courier New; font-size:large; text-align:center'><b>{0}</b></td>", "Collections");
        rpt.Append("</tr>");
        rpt.Append("</table>");

        rpt.Append("<table width='100%' cellspacing=0 border=0 style=' border-spacing: 0; padding: 0;font-family:Courier New;font-size:small;'>");
        if (dtCash.Rows.Count > 0)
        {
            rpt.Append("<tr>");
            rpt.AppendFormat("<td colspan='9' style='width: 100%; font-family:Courier New; font-size:large; text-align:left;background-color:burlywood;'>{0}</td>", "Collection By - Cash(Rs.)");
            rpt.Append("</tr>");
            rpt.Append("<tr>");
            rpt.AppendFormat("<td colspan='8' style='width: 90%; font-family:Courier New; font-size:large; text-align:right;background-color:burlywood;'>{0}</td>", "Receipt Count    :");
            rpt.AppendFormat("<td style='width: 10%; font-family:Courier New; font-size:large; text-align:left;background-color:burlywood;'>{0}</td>", dtCash.Rows.Count);
            rpt.Append("</tr>");

            rpt.Append("<tr>");
            rpt.AppendFormat("<td style='width: 5%; font-family:Courier New; font-size:small; text-align:center;background-color:burlywood;'>{0}</td>", "S.No");
            rpt.AppendFormat("<td style='width: 15%; font-family:Courier New; font-size:small; text-align:center;background-color:burlywood;'>{0}</td>", "Reg No.");
            rpt.AppendFormat("<td style='width: 20%; font-family:Courier New; font-size:small; text-align:center;background-color:burlywood;'>{0}</td>", "Patient Name");
            rpt.AppendFormat("<td style='width: 8%; font-family:Courier New; font-size:small; text-align:center;background-color:burlywood;'>{0}</td>", "Visit Type");
            rpt.AppendFormat("<td style='width: 16%; font-family:Courier New; font-size:small; text-align:center;background-color:burlywood;'>{0}</td>", "Date");
            rpt.AppendFormat("<td style='width: 10%; font-family:Courier New; font-size:small; text-align:center;background-color:burlywood;'>{0}</td>", "Invoice No");
            rpt.AppendFormat("<td style='width: 8%; font-family:Courier New; font-size:small; text-align:center;background-color:burlywood;'>{0}</td>", "Receipt Type");
            rpt.AppendFormat("<td style='width: 8%; font-family:Courier New; font-size:small; text-align:center;background-color:burlywood;'>{0}</td>", "Receipt Amount");
            rpt.AppendFormat("<td style='width: 10%; font-family:Courier New; font-size:small; text-align:center;background-color:burlywood;'>{0}</td>", "Paid Amount");
            rpt.Append("</tr>");
            for (int i = 0; i < dtCash.Rows.Count; i++, srl++)
            {
                rpt.Append("<tr>");
                rpt.AppendFormat("<td style='font-family:Courier New; font-size:small; text-align:left;'>{0}</td>", srl);
                rpt.AppendFormat("<td style='font-family:Courier New; font-size:small; text-align:left;'>{0}</td>", dtCash.Rows[i]["RegNo"].ToString());
                rpt.AppendFormat("<td style='font-family:Courier New; font-size:small; text-align:left;'>{0}</td>", dtCash.Rows[i]["PatientName"].ToString());
                rpt.AppendFormat("<td style='font-family:Courier New; font-size:small; text-align:left;'>{0}</td>", dtCash.Rows[i]["ReqType"].ToString());
                rpt.AppendFormat("<td style='font-family:Courier New; font-size:small; text-align:left;'>{0}</td>", dtCash.Rows[i]["Billdate"].ToString() + " " + dtCash.Rows[i]["BillTime"].ToString());
                rpt.AppendFormat("<td style='font-family:Courier New; font-size:small; text-align:center;'>{0}</td>", dtCash.Rows[i]["ReceiptNo"].ToString());
                rpt.AppendFormat("<td style='font-family:Courier New; font-size:small; text-align:center;'>{0}</td>", dtCash.Rows[i]["PayType"].ToString());
                rpt.AppendFormat("<td style='font-family:Courier New; font-size:small; text-align:right;'>{0}</td>", dtCash.Rows[i]["PayableAmt"].ToString());
                rpt.AppendFormat("<td style='font-family:Courier New; font-size:small; text-align:right;'>{0}</td>", dtCash.Rows[i]["BillAmt"].ToString());
                rpt.Append("</tr>");

                totamtBillCash = totamtBillCash + Convert.ToDecimal(dtCash.Rows[i]["BillAmt"].ToString());
                gtotamtBill = gtotamtBill + Convert.ToDecimal(dtCash.Rows[i]["BillAmt"].ToString());
            }
            rpt.Append("<tr>");
            rpt.AppendFormat("<td colspan='8' style='font-family:Courier New; font-size:small; text-align:right;'>{0}</td>", "Total Collection - Cash(Rs.)");
            rpt.AppendFormat("<td style='font-family:Courier New; font-size:small; text-align:right;'>{0}</td>", totamtBillCash);
            rpt.Append("</tr>");
        }

        if (dtCard.Rows.Count > 0)
        {
            rpt.Append("<tr>");
            rpt.AppendFormat("<td colspan='9' style='width: 100%; font-family:Courier New; font-size:large; text-align:left;background-color:burlywood;'>{0}</td>", "Collection By - Card(Rs.)");
            rpt.Append("</tr>");
            rpt.Append("<tr>");
            rpt.AppendFormat("<td colspan='8' style='font-family:Courier New; font-size:large; text-align:right;background-color:burlywood;'>{0}</td>", "Receipt Count    :");
            rpt.AppendFormat("<td style='font-family:Courier New; font-size:x-Medium; text-align:left;background-color:burlywood;'>{0}</td>", dtCard.Rows.Count);
            rpt.Append("</tr>");

            rpt.Append("<tr>");
            rpt.AppendFormat("<td style='font-family:Courier New; font-size:small; text-align:center;background-color:burlywood;'>{0}</td>", "S.No");
            rpt.AppendFormat("<td style='font-family:Courier New; font-size:small; text-align:center;background-color:burlywood;'>{0}</td>", "Reg No.");
            rpt.AppendFormat("<td style='font-family:Courier New; font-size:small; text-align:center;background-color:burlywood;'>{0}</td>", "Patient Name");
            rpt.AppendFormat("<td style='font-family:Courier New; font-size:small; text-align:center;background-color:burlywood;'>{0}</td>", "Visit Type");
            rpt.AppendFormat("<td style='font-family:Courier New; font-size:small; text-align:center;background-color:burlywood;'>{0}</td>", "Date");
            rpt.AppendFormat("<td style='font-family:Courier New; font-size:small; text-align:center;background-color:burlywood;'>{0}</td>", "Invoice No");
            rpt.AppendFormat("<td style='font-family:Courier New; font-size:small; text-align:center;background-color:burlywood;'>{0}</td>", "Receipt Type");
            rpt.AppendFormat("<td style='font-family:Courier New; font-size:small; text-align:center;background-color:burlywood;'>{0}</td>", "Receipt Amount");
            rpt.AppendFormat("<td style='font-family:Courier New; font-size:small; text-align:center;background-color:burlywood;'>{0}</td>", "Paid Amount");
            rpt.Append("</tr>");
            for (int i = 0; i < dtCard.Rows.Count; i++, srl++)
            {
                rpt.Append("<tr>");
                rpt.AppendFormat("<td style='font-family:Courier New; font-size:small; text-align:left;'>{0}</td>", srl);
                rpt.AppendFormat("<td style='font-family:Courier New; font-size:small; text-align:left;'>{0}</td>", dtCard.Rows[i]["RegNo"].ToString());
                rpt.AppendFormat("<td style='font-family:Courier New; font-size:small; text-align:left;'>{0}</td>", dtCard.Rows[i]["PatientName"].ToString());
                rpt.AppendFormat("<td style='font-family:Courier New; font-size:small; text-align:left;'>{0}</td>", dtCard.Rows[i]["ReqType"].ToString());
                rpt.AppendFormat("<td style='font-family:Courier New; font-size:small; text-align:left;'>{0}</td>", dtCard.Rows[i]["Billdate"].ToString() + " " + dtCard.Rows[i]["BillTime"].ToString());
                rpt.AppendFormat("<td style='font-family:Courier New; font-size:small; text-align:center;'>{0}</td>", dtCard.Rows[i]["ReceiptNo"].ToString());
                rpt.AppendFormat("<td style='font-family:Courier New; font-size:small; text-align:center;'>{0}</td>", dtCard.Rows[i]["PayType"].ToString());
                rpt.AppendFormat("<td style='font-family:Courier New; font-size:small; text-align:right;'>{0}</td>", dtCard.Rows[i]["PayableAmt"].ToString());
                rpt.AppendFormat("<td style='font-family:Courier New; font-size:small; text-align:right;'>{0}</td>", dtCard.Rows[i]["BillAmt"].ToString());
                rpt.Append("</tr>");

                totamtBillCard = totamtBillCard + Convert.ToDecimal(dtCard.Rows[i]["BillAmt"].ToString());
                gtotamtBill = gtotamtBill + Convert.ToDecimal(dtCard.Rows[i]["BillAmt"].ToString());
            }
            rpt.Append("<tr>");
            rpt.AppendFormat("<td colspan='8' style='font-family:Courier New; font-size:small; text-align:right;'>{0}</td>", "Total Collection - Card(Rs.)");
            rpt.AppendFormat("<td style='font-family:Courier New; font-size:small; text-align:right;'>{0}</td>", totamtBillCard);
            rpt.Append("</tr>");
        }


        if (dtEWallet.Rows.Count > 0)
        {
            rpt.Append("<tr>");
            rpt.AppendFormat("<td colspan='9' style='font-family:Courier New; font-size:large; text-align:left;background-color:burlywood;'>{0}</td>", "Collection By - e-Wallet(Rs.)");
            rpt.Append("</tr>");
            rpt.Append("<tr>");
            rpt.AppendFormat("<td colspan='8' style='font-family:Courier New; font-size:large; text-align:right;background-color:burlywood;'>{0}</td>", "Receipt Count    :");
            rpt.AppendFormat("<td style='font-family:Courier New; font-size:large; text-align:left;background-color:burlywood;'>{0}</td>", dtEWallet.Rows.Count);
            rpt.Append("</tr>");

            rpt.Append("<tr>");
            rpt.AppendFormat("<td style='font-family:Courier New; font-size:small; text-align:center;background-color:burlywood;'>{0}</td>", "S.No");
            rpt.AppendFormat("<td style='font-family:Courier New; font-size:small; text-align:center;background-color:burlywood;'>{0}</td>", "Reg No.");
            rpt.AppendFormat("<td style='font-family:Courier New; font-size:small; text-align:center;background-color:burlywood;'>{0}</td>", "Patient Name");
            rpt.AppendFormat("<td style='font-family:Courier New; font-size:small; text-align:center;background-color:burlywood;'>{0}</td>", "Visit Type");
            rpt.AppendFormat("<td style='font-family:Courier New; font-size:small; text-align:center;background-color:burlywood;'>{0}</td>", "Date");
            rpt.AppendFormat("<td style='font-family:Courier New; font-size:small; text-align:center;background-color:burlywood;'>{0}</td>", "Invoice No");
            rpt.AppendFormat("<td style='font-family:Courier New; font-size:small; text-align:center;background-color:burlywood;'>{0}</td>", "Receipt Type");
            rpt.AppendFormat("<td style='font-family:Courier New; font-size:small; text-align:center;background-color:burlywood;'>{0}</td>", "Receipt Amount");
            rpt.AppendFormat("<td style='font-family:Courier New; font-size:small; text-align:center;background-color:burlywood;'>{0}</td>", "Paid Amount");
            rpt.Append("</tr>");
            for (int i = 0; i < dtEWallet.Rows.Count; i++, srl++)
            {
                rpt.Append("<tr>");
                rpt.AppendFormat("<td style='font-family:Courier New; font-size:small; text-align:left;'>{0}</td>", srl);
                rpt.AppendFormat("<td style='font-family:Courier New; font-size:small; text-align:left;'>{0}</td>", dtEWallet.Rows[i]["RegNo"].ToString());
                rpt.AppendFormat("<td style='font-family:Courier New; font-size:small; text-align:left;'>{0}</td>", dtEWallet.Rows[i]["PatientName"].ToString());
                rpt.AppendFormat("<td style='font-family:Courier New; font-size:small; text-align:left;'>{0}</td>", dtEWallet.Rows[i]["ReqType"].ToString());
                rpt.AppendFormat("<td style='font-family:Courier New; font-size:small; text-align:left;'>{0}</td>", dtEWallet.Rows[i]["Billdate"].ToString() + " " + dtEWallet.Rows[i]["BillTime"].ToString());
                rpt.AppendFormat("<td style='font-family:Courier New; font-size:small; text-align:center;'>{0}</td>", dtEWallet.Rows[i]["ReceiptNo"].ToString());
                rpt.AppendFormat("<td style='font-family:Courier New; font-size:small; text-align:center;'>{0}</td>", dtEWallet.Rows[i]["PayType"].ToString());
                rpt.AppendFormat("<td style='font-family:Courier New; font-size:small; text-align:right;'>{0}</td>", dtEWallet.Rows[i]["PayableAmt"].ToString());
                rpt.AppendFormat("<td style='font-family:Courier New; font-size:small; text-align:right;'>{0}</td>", dtEWallet.Rows[i]["BillAmt"].ToString());
                rpt.Append("</tr>");

                totamtBillWallet = totamtBillWallet + Convert.ToDecimal(dtEWallet.Rows[i]["BillAmt"].ToString());
                gtotamtBill = gtotamtBill + Convert.ToDecimal(dtEWallet.Rows[i]["BillAmt"].ToString());
            }
            rpt.Append("<tr>");
            rpt.AppendFormat("<td colspan='8' style='font-family:Courier New; font-size:small; text-align:right;'>{0}</td>", "Total Collection - e-Wallet(Rs.)");
            rpt.AppendFormat("<td style='font-family:Courier New; font-size:small; text-align:right;'>{0}</td>", totamtBillWallet);
            rpt.Append("</tr>");

            
        }

        if (dtNetBank.Rows.Count > 0)
        {
            rpt.Append("<tr>");
            rpt.AppendFormat("<td colspan='9' style='font-family:Courier New; font-size:large; text-align:left;background-color:burlywood;'>{0}</td>", "Collection By - Net Banking(Rs.)");
            rpt.Append("</tr>");
            rpt.Append("<tr>");
            rpt.AppendFormat("<td colspan='8' style='font-family:Courier New; font-size:large; text-align:right;background-color:burlywood;'>{0}</td>", "Receipt Count    :");
            rpt.AppendFormat("<td style='font-family:Courier New; font-size:large; text-align:left;background-color:burlywood;'>{0}</td>", dtNetBank.Rows.Count);
            rpt.Append("</tr>");

            rpt.Append("<tr>");
            rpt.AppendFormat("<td style='font-family:Courier New; font-size:small; text-align:center;background-color:burlywood;'>{0}</td>", "S.No");
            rpt.AppendFormat("<td style='font-family:Courier New; font-size:small; text-align:center;background-color:burlywood;'>{0}</td>", "Reg No.");
            rpt.AppendFormat("<td style='font-family:Courier New; font-size:small; text-align:center;background-color:burlywood;'>{0}</td>", "Patient Name");
            rpt.AppendFormat("<td style='font-family:Courier New; font-size:small; text-align:center;background-color:burlywood;'>{0}</td>", "Visit Type");
            rpt.AppendFormat("<td style='font-family:Courier New; font-size:small; text-align:center;background-color:burlywood;'>{0}</td>", "Date");
            rpt.AppendFormat("<td style='font-family:Courier New; font-size:small; text-align:center;background-color:burlywood;'>{0}</td>", "Invoice No");
            rpt.AppendFormat("<td style='font-family:Courier New; font-size:small; text-align:center;background-color:burlywood;'>{0}</td>", "Receipt Type");
            rpt.AppendFormat("<td style='font-family:Courier New; font-size:small; text-align:center;background-color:burlywood;'>{0}</td>", "Receipt Amount");
            rpt.AppendFormat("<td style='font-family:Courier New; font-size:small; text-align:center;background-color:burlywood;'>{0}</td>", "Paid Amount");
            rpt.Append("</tr>");
            for (int i = 0; i < dtNetBank.Rows.Count; i++, srl++)
            {
                rpt.Append("<tr>");
                rpt.AppendFormat("<td style='font-family:Courier New; font-size:small; text-align:left;'>{0}</td>", srl);
                rpt.AppendFormat("<td style='font-family:Courier New; font-size:small; text-align:left;'>{0}</td>", dtNetBank.Rows[i]["RegNo"].ToString());
                rpt.AppendFormat("<td style='font-family:Courier New; font-size:small; text-align:left;'>{0}</td>", dtNetBank.Rows[i]["PatientName"].ToString());
                rpt.AppendFormat("<td style='font-family:Courier New; font-size:small; text-align:left;'>{0}</td>", dtNetBank.Rows[i]["ReqType"].ToString());
                rpt.AppendFormat("<td style='font-family:Courier New; font-size:small; text-align:left;'>{0}</td>", dtNetBank.Rows[i]["Billdate"].ToString() + " " + dtNetBank.Rows[i]["BillTime"].ToString());
                rpt.AppendFormat("<td style='font-family:Courier New; font-size:small; text-align:center;'>{0}</td>", dtNetBank.Rows[i]["ReceiptNo"].ToString());
                rpt.AppendFormat("<td style='font-family:Courier New; font-size:small; text-align:center;'>{0}</td>", dtNetBank.Rows[i]["PayType"].ToString());
                rpt.AppendFormat("<td style='font-family:Courier New; font-size:small; text-align:right;'>{0}</td>", dtNetBank.Rows[i]["PayableAmt"].ToString());
                rpt.AppendFormat("<td style='font-family:Courier New; font-size:small; text-align:right;'>{0}</td>", dtNetBank.Rows[i]["BillAmt"].ToString());
                rpt.Append("</tr>");

                totamtBillNetBank = totamtBillNetBank + Convert.ToDecimal(dtNetBank.Rows[i]["BillAmt"].ToString());
                gtotamtBill = gtotamtBill + Convert.ToDecimal(dtNetBank.Rows[i]["BillAmt"].ToString());
            }
            rpt.Append("<tr>");
            rpt.AppendFormat("<td colspan='8' style='font-family:Courier New; font-size:small; text-align:right;'>{0}</td>", "Total Collection - e-Wallet(Rs.)");
            rpt.AppendFormat("<td style='font-family:Courier New; font-size:small; text-align:right;'>{0}</td>", totamtBillWallet);
            rpt.Append("</tr>");


        }
        rpt.Append("<tr>");
        rpt.AppendFormat("<td colspan='9' style='font-family:Courier New; font-size:x-Medium; text-align:right;height:60px;'>{0}</td>", "");
        rpt.Append("</tr>");

        rpt.Append("<tr>");
        rpt.AppendFormat("<td colspan='8' style='font-family:Courier New; font-size:x-Medium; text-align:right;'>{0}</td>", "Total Collection - Cash(Rs.)");
        rpt.AppendFormat("<td style='font-family:Courier New; font-size:x-Medium; text-align:right;'>{0}</td>", totamtBillCash);
        rpt.Append("</tr>");
        rpt.Append("<tr>");
        rpt.AppendFormat("<td colspan='8' style='font-family:Courier New; font-size:x-Medium; text-align:right;'>{0}</td>", "Total Collection - Card(Rs.)");
        rpt.AppendFormat("<td style='font-family:Courier New; font-size:x-Medium; text-align:right;'>{0}</td>", totamtBillCard);
        rpt.Append("</tr>");
        rpt.Append("<tr>");
        rpt.AppendFormat("<td colspan='8' style='ont-family:Courier New; font-size:x-Medium; text-align:right;'>{0}</td>", "Total Collection - e-Wallet(Rs.)");
        rpt.AppendFormat("<td style='font-family:Courier New; font-size:x-Medium; text-align:right;'>{0}</td>", totamtBillWallet);
        rpt.Append("</tr>");
        rpt.Append("<tr>");
        rpt.AppendFormat("<td colspan='8' style='ont-family:Courier New; font-size:x-Medium; text-align:right;'>{0}</td>", "Total Collection - Net Banking(Rs.)");
        rpt.AppendFormat("<td style='font-family:Courier New; font-size:x-Medium; text-align:right;'>{0}</td>", totamtBillNetBank);
        rpt.Append("</tr>");

        //DataTable dtCashRefund = objreport.GetDateWiseRefund(Session["CoCode"].ToString().Trim(), Session["YearCode"].ToString().Trim(), fromdate, todate, "C");
        //DataTable dtCardRefund = objreport.GetDateWiseRefund(Session["CoCode"].ToString().Trim(), Session["YearCode"].ToString().Trim(), fromdate, todate, "R");
        //Decimal netRefund = 0, netcollection = 0;
        //netRefund = Convert.ToDecimal(dtCashRefund.Rows[0]["RefAmt"].ToString()) + Convert.ToDecimal(dtCardRefund.Rows[0]["RefAmt"].ToString());



        ////////////////////////////Refund

        if (dtCashRef.Rows.Count > 0)
        {
            rpt.Append("<tr>");
            rpt.AppendFormat("<td colspan='9' style='width: 100%; font-family:Courier New; font-size:large; text-align:left;background-color:burlywood;'>{0}</td>", "Refund By - Cash(Rs.)");
            rpt.Append("</tr>");
            rpt.Append("<tr>");
            rpt.AppendFormat("<td colspan='8' style='width: 90%; font-family:Courier New; font-size:large; text-align:right;background-color:burlywood;'>{0}</td>", "Receipt Count    :");
            rpt.AppendFormat("<td style='width: 10%; font-family:Courier New; font-size:large; text-align:left;background-color:burlywood;'>{0}</td>", dtCashRef.Rows.Count);
            rpt.Append("</tr>");

            rpt.Append("<tr>");
            rpt.AppendFormat("<td style='width: 5%; font-family:Courier New; font-size:small; text-align:center;background-color:burlywood;'>{0}</td>", "S.No");
            rpt.AppendFormat("<td style='width: 15%; font-family:Courier New; font-size:small; text-align:center;background-color:burlywood;'>{0}</td>", "Reg No.");
            rpt.AppendFormat("<td style='width: 20%; font-family:Courier New; font-size:small; text-align:center;background-color:burlywood;'>{0}</td>", "Patient Name");
            rpt.AppendFormat("<td style='width: 8%; font-family:Courier New; font-size:small; text-align:center;background-color:burlywood;'>{0}</td>", "Visit Type");
            rpt.AppendFormat("<td style='width: 16%; font-family:Courier New; font-size:small; text-align:center;background-color:burlywood;'>{0}</td>", "Date");
            rpt.AppendFormat("<td style='width: 10%; font-family:Courier New; font-size:small; text-align:center;background-color:burlywood;'>{0}</td>", "Invoice No");
            rpt.AppendFormat("<td style='width: 8%; font-family:Courier New; font-size:small; text-align:center;background-color:burlywood;'>{0}</td>", "Receipt Type");
            rpt.AppendFormat("<td style='width: 8%; font-family:Courier New; font-size:small; text-align:center;background-color:burlywood;'>{0}</td>", "Receipt Amount");
            rpt.AppendFormat("<td style='width: 10%; font-family:Courier New; font-size:small; text-align:center;background-color:burlywood;'>{0}</td>", "Refund Amount");
            rpt.Append("</tr>");
            for (int i = 0; i < dtCashRef.Rows.Count; i++, srl++)
            {
                rpt.Append("<tr>");
                rpt.AppendFormat("<td style='font-family:Courier New; font-size:small; text-align:left;'>{0}</td>", refsrl);
                rpt.AppendFormat("<td style='font-family:Courier New; font-size:small; text-align:left;'>{0}</td>", dtCashRef.Rows[i]["RegNo"].ToString());
                rpt.AppendFormat("<td style='font-family:Courier New; font-size:small; text-align:left;'>{0}</td>", dtCashRef.Rows[i]["PatientName"].ToString());
                rpt.AppendFormat("<td style='font-family:Courier New; font-size:small; text-align:left;'>{0}</td>", dtCashRef.Rows[i]["ReqType"].ToString());
                rpt.AppendFormat("<td style='font-family:Courier New; font-size:small; text-align:left;'>{0}</td>", dtCashRef.Rows[i]["Billdate"].ToString() + " " + dtCashRef.Rows[i]["BillTime"].ToString());
                rpt.AppendFormat("<td style='font-family:Courier New; font-size:small; text-align:center;'>{0}</td>", dtCashRef.Rows[i]["ReceiptNo"].ToString());
                rpt.AppendFormat("<td style='font-family:Courier New; font-size:small; text-align:center;'>{0}</td>", dtCashRef.Rows[i]["PayType"].ToString());
                rpt.AppendFormat("<td style='font-family:Courier New; font-size:small; text-align:right;'>{0}</td>", dtCashRef.Rows[i]["PayableAmt"].ToString());
                rpt.AppendFormat("<td style='font-family:Courier New; font-size:small; text-align:right;'>{0}</td>", dtCashRef.Rows[i]["BillAmt"].ToString());
                rpt.Append("</tr>");

                totamtBillCashRef = totamtBillCashRef + Convert.ToDecimal(dtCashRef.Rows[i]["BillAmt"].ToString());
                gtotamtBillRef = gtotamtBillRef + Convert.ToDecimal(dtCashRef.Rows[i]["BillAmt"].ToString());
            }
            rpt.Append("<tr>");
            rpt.AppendFormat("<td colspan='8' style='font-family:Courier New; font-size:small; text-align:right;'>{0}</td>", "Total Refund - Cash(Rs.)");
            rpt.AppendFormat("<td style='font-family:Courier New; font-size:small; text-align:right;'>{0}</td>", totamtBillCashRef);
            rpt.Append("</tr>");
        }


        if (dtCardRef.Rows.Count > 0)
        {
            rpt.Append("<tr>");
            rpt.AppendFormat("<td colspan='9' style='width: 100%; font-family:Courier New; font-size:large; text-align:left;background-color:burlywood;'>{0}</td>", "Refund By - Card(Rs.)");
            rpt.Append("</tr>");
            rpt.Append("<tr>");
            rpt.AppendFormat("<td colspan='8' style='font-family:Courier New; font-size:large; text-align:right;background-color:burlywood;'>{0}</td>", "Receipt Count    :");
            rpt.AppendFormat("<td style='font-family:Courier New; font-size:x-Medium; text-align:left;background-color:burlywood;'>{0}</td>", dtCardRef.Rows.Count);
            rpt.Append("</tr>");

            rpt.Append("<tr>");
            rpt.AppendFormat("<td style='font-family:Courier New; font-size:small; text-align:center;background-color:burlywood;'>{0}</td>", "S.No");
            rpt.AppendFormat("<td style='font-family:Courier New; font-size:small; text-align:center;background-color:burlywood;'>{0}</td>", "Reg No.");
            rpt.AppendFormat("<td style='font-family:Courier New; font-size:small; text-align:center;background-color:burlywood;'>{0}</td>", "Patient Name");
            rpt.AppendFormat("<td style='font-family:Courier New; font-size:small; text-align:center;background-color:burlywood;'>{0}</td>", "Visit Type");
            rpt.AppendFormat("<td style='font-family:Courier New; font-size:small; text-align:center;background-color:burlywood;'>{0}</td>", "Date");
            rpt.AppendFormat("<td style='font-family:Courier New; font-size:small; text-align:center;background-color:burlywood;'>{0}</td>", "Invoice No");
            rpt.AppendFormat("<td style='font-family:Courier New; font-size:small; text-align:center;background-color:burlywood;'>{0}</td>", "Receipt Type");
            rpt.AppendFormat("<td style='font-family:Courier New; font-size:small; text-align:center;background-color:burlywood;'>{0}</td>", "Receipt Amount");
            rpt.AppendFormat("<td style='font-family:Courier New; font-size:small; text-align:center;background-color:burlywood;'>{0}</td>", "Refund Amount");
            rpt.Append("</tr>");
            for (int i = 0; i < dtCardRef.Rows.Count; i++, srl++)
            {
                rpt.Append("<tr>");
                rpt.AppendFormat("<td style='font-family:Courier New; font-size:small; text-align:left;'>{0}</td>", refsrl);
                rpt.AppendFormat("<td style='font-family:Courier New; font-size:small; text-align:left;'>{0}</td>", dtCardRef.Rows[i]["RegNo"].ToString());
                rpt.AppendFormat("<td style='font-family:Courier New; font-size:small; text-align:left;'>{0}</td>", dtCardRef.Rows[i]["PatientName"].ToString());
                rpt.AppendFormat("<td style='font-family:Courier New; font-size:small; text-align:left;'>{0}</td>", dtCardRef.Rows[i]["ReqType"].ToString());
                rpt.AppendFormat("<td style='font-family:Courier New; font-size:small; text-align:left;'>{0}</td>", dtCardRef.Rows[i]["Billdate"].ToString() + " " + dtCardRef.Rows[i]["BillTime"].ToString());
                rpt.AppendFormat("<td style='font-family:Courier New; font-size:small; text-align:center;'>{0}</td>", dtCardRef.Rows[i]["ReceiptNo"].ToString());
                rpt.AppendFormat("<td style='font-family:Courier New; font-size:small; text-align:center;'>{0}</td>", dtCardRef.Rows[i]["PayType"].ToString());
                rpt.AppendFormat("<td style='font-family:Courier New; font-size:small; text-align:right;'>{0}</td>", dtCardRef.Rows[i]["PayableAmt"].ToString());
                rpt.AppendFormat("<td style='font-family:Courier New; font-size:small; text-align:right;'>{0}</td>", dtCardRef.Rows[i]["BillAmt"].ToString());
                rpt.Append("</tr>");

                totamtBillCardRef = totamtBillCardRef + Convert.ToDecimal(dtCardRef.Rows[i]["BillAmt"].ToString());
                gtotamtBillRef = gtotamtBillRef + Convert.ToDecimal(dtCardRef.Rows[i]["BillAmt"].ToString());
            }
            rpt.Append("<tr>");
            rpt.AppendFormat("<td colspan='8' style='font-family:Courier New; font-size:small; text-align:right;'>{0}</td>", "Total Refund - Card(Rs.)");
            rpt.AppendFormat("<td style='font-family:Courier New; font-size:small; text-align:right;'>{0}</td>", totamtBillCardRef);
            rpt.Append("</tr>");
        }


        if (dtEWalletRef.Rows.Count > 0)
        {
            rpt.Append("<tr>");
            rpt.AppendFormat("<td colspan='9' style='font-family:Courier New; font-size:large; text-align:left;background-color:burlywood;'>{0}</td>", "Refund By - e-Wallet(Rs.)");
            rpt.Append("</tr>");
            rpt.Append("<tr>");
            rpt.AppendFormat("<td colspan='8' style='font-family:Courier New; font-size:large; text-align:right;background-color:burlywood;'>{0}</td>", "Receipt Count    :");
            rpt.AppendFormat("<td style='font-family:Courier New; font-size:large; text-align:left;background-color:burlywood;'>{0}</td>", dtEWalletRef.Rows.Count);
            rpt.Append("</tr>");

            rpt.Append("<tr>");
            rpt.AppendFormat("<td style='font-family:Courier New; font-size:small; text-align:center;background-color:burlywood;'>{0}</td>", "S.No");
            rpt.AppendFormat("<td style='font-family:Courier New; font-size:small; text-align:center;background-color:burlywood;'>{0}</td>", "Reg No.");
            rpt.AppendFormat("<td style='font-family:Courier New; font-size:small; text-align:center;background-color:burlywood;'>{0}</td>", "Patient Name");
            rpt.AppendFormat("<td style='font-family:Courier New; font-size:small; text-align:center;background-color:burlywood;'>{0}</td>", "Visit Type");
            rpt.AppendFormat("<td style='font-family:Courier New; font-size:small; text-align:center;background-color:burlywood;'>{0}</td>", "Date");
            rpt.AppendFormat("<td style='font-family:Courier New; font-size:small; text-align:center;background-color:burlywood;'>{0}</td>", "Invoice No");
            rpt.AppendFormat("<td style='font-family:Courier New; font-size:small; text-align:center;background-color:burlywood;'>{0}</td>", "Receipt Type");
            rpt.AppendFormat("<td style='font-family:Courier New; font-size:small; text-align:center;background-color:burlywood;'>{0}</td>", "Receipt Amount");
            rpt.AppendFormat("<td style='font-family:Courier New; font-size:small; text-align:center;background-color:burlywood;'>{0}</td>", "Refund Amount");
            rpt.Append("</tr>");
            for (int i = 0; i < dtEWalletRef.Rows.Count; i++, srl++)
            {
                rpt.Append("<tr>");
                rpt.AppendFormat("<td style='font-family:Courier New; font-size:small; text-align:left;'>{0}</td>", refsrl);
                rpt.AppendFormat("<td style='font-family:Courier New; font-size:small; text-align:left;'>{0}</td>", dtEWalletRef.Rows[i]["RegNo"].ToString());
                rpt.AppendFormat("<td style='font-family:Courier New; font-size:small; text-align:left;'>{0}</td>", dtEWalletRef.Rows[i]["PatientName"].ToString());
                rpt.AppendFormat("<td style='font-family:Courier New; font-size:small; text-align:left;'>{0}</td>", dtEWalletRef.Rows[i]["ReqType"].ToString());
                rpt.AppendFormat("<td style='font-family:Courier New; font-size:small; text-align:left;'>{0}</td>", dtEWalletRef.Rows[i]["Billdate"].ToString() + " " + dtEWalletRef.Rows[i]["BillTime"].ToString());
                rpt.AppendFormat("<td style='font-family:Courier New; font-size:small; text-align:center;'>{0}</td>", dtEWalletRef.Rows[i]["ReceiptNo"].ToString());
                rpt.AppendFormat("<td style='font-family:Courier New; font-size:small; text-align:center;'>{0}</td>", dtEWalletRef.Rows[i]["PayType"].ToString());
                rpt.AppendFormat("<td style='font-family:Courier New; font-size:small; text-align:right;'>{0}</td>", dtEWalletRef.Rows[i]["PayableAmt"].ToString());
                rpt.AppendFormat("<td style='font-family:Courier New; font-size:small; text-align:right;'>{0}</td>", dtEWalletRef.Rows[i]["BillAmt"].ToString());
                rpt.Append("</tr>");

                totamtBillWalletRef = totamtBillWalletRef + Convert.ToDecimal(dtEWalletRef.Rows[i]["BillAmt"].ToString());
                gtotamtBillRef = gtotamtBillRef + Convert.ToDecimal(dtEWalletRef.Rows[i]["BillAmt"].ToString());
            }
            rpt.Append("<tr>");
            rpt.AppendFormat("<td colspan='8' style='font-family:Courier New; font-size:small; text-align:right;'>{0}</td>", "Total Refund - e-Wallet(Rs.)");
            rpt.AppendFormat("<td style='font-family:Courier New; font-size:small; text-align:right;'>{0}</td>", totamtBillWalletRef);
            rpt.Append("</tr>");


        }


        if (dtNetBankRef.Rows.Count > 0)
        {
            rpt.Append("<tr>");
            rpt.AppendFormat("<td colspan='9' style='font-family:Courier New; font-size:large; text-align:left;background-color:burlywood;'>{0}</td>", "Refund By - Net Banking(Rs.)");
            rpt.Append("</tr>");
            rpt.Append("<tr>");
            rpt.AppendFormat("<td colspan='8' style='font-family:Courier New; font-size:large; text-align:right;background-color:burlywood;'>{0}</td>", "Receipt Count    :");
            rpt.AppendFormat("<td style='font-family:Courier New; font-size:large; text-align:left;background-color:burlywood;'>{0}</td>", dtNetBankRef.Rows.Count);
            rpt.Append("</tr>");

            rpt.Append("<tr>");
            rpt.AppendFormat("<td style='font-family:Courier New; font-size:small; text-align:center;background-color:burlywood;'>{0}</td>", "S.No");
            rpt.AppendFormat("<td style='font-family:Courier New; font-size:small; text-align:center;background-color:burlywood;'>{0}</td>", "Reg No.");
            rpt.AppendFormat("<td style='font-family:Courier New; font-size:small; text-align:center;background-color:burlywood;'>{0}</td>", "Patient Name");
            rpt.AppendFormat("<td style='font-family:Courier New; font-size:small; text-align:center;background-color:burlywood;'>{0}</td>", "Visit Type");
            rpt.AppendFormat("<td style='font-family:Courier New; font-size:small; text-align:center;background-color:burlywood;'>{0}</td>", "Date");
            rpt.AppendFormat("<td style='font-family:Courier New; font-size:small; text-align:center;background-color:burlywood;'>{0}</td>", "Invoice No");
            rpt.AppendFormat("<td style='font-family:Courier New; font-size:small; text-align:center;background-color:burlywood;'>{0}</td>", "Receipt Type");
            rpt.AppendFormat("<td style='font-family:Courier New; font-size:small; text-align:center;background-color:burlywood;'>{0}</td>", "Receipt Amount");
            rpt.AppendFormat("<td style='font-family:Courier New; font-size:small; text-align:center;background-color:burlywood;'>{0}</td>", "Refund Amount");
            rpt.Append("</tr>");
            for (int i = 0; i < dtNetBankRef.Rows.Count; i++, srl++)
            {
                rpt.Append("<tr>");
                rpt.AppendFormat("<td style='font-family:Courier New; font-size:small; text-align:left;'>{0}</td>", refsrl);
                rpt.AppendFormat("<td style='font-family:Courier New; font-size:small; text-align:left;'>{0}</td>", dtNetBankRef.Rows[i]["RegNo"].ToString());
                rpt.AppendFormat("<td style='font-family:Courier New; font-size:small; text-align:left;'>{0}</td>", dtNetBankRef.Rows[i]["PatientName"].ToString());
                rpt.AppendFormat("<td style='font-family:Courier New; font-size:small; text-align:left;'>{0}</td>", dtNetBankRef.Rows[i]["ReqType"].ToString());
                rpt.AppendFormat("<td style='font-family:Courier New; font-size:small; text-align:left;'>{0}</td>", dtNetBankRef.Rows[i]["Billdate"].ToString() + " " + dtNetBankRef.Rows[i]["BillTime"].ToString());
                rpt.AppendFormat("<td style='font-family:Courier New; font-size:small; text-align:center;'>{0}</td>", dtNetBankRef.Rows[i]["ReceiptNo"].ToString());
                rpt.AppendFormat("<td style='font-family:Courier New; font-size:small; text-align:center;'>{0}</td>", dtNetBankRef.Rows[i]["PayType"].ToString());
                rpt.AppendFormat("<td style='font-family:Courier New; font-size:small; text-align:right;'>{0}</td>", dtNetBankRef.Rows[i]["PayableAmt"].ToString());
                rpt.AppendFormat("<td style='font-family:Courier New; font-size:small; text-align:right;'>{0}</td>", dtNetBankRef.Rows[i]["BillAmt"].ToString());
                rpt.Append("</tr>");

                totamtBillNetBankRef = totamtBillNetBankRef + Convert.ToDecimal(dtNetBankRef.Rows[i]["BillAmt"].ToString());
                gtotamtBillRef = gtotamtBillRef + Convert.ToDecimal(dtNetBankRef.Rows[i]["BillAmt"].ToString());
            }
            rpt.Append("<tr>");
            rpt.AppendFormat("<td colspan='8' style='font-family:Courier New; font-size:small; text-align:right;'>{0}</td>", "Total Refund - e-Wallet(Rs.)");
            rpt.AppendFormat("<td style='font-family:Courier New; font-size:small; text-align:right;'>{0}</td>", totamtBillWalletRef);
            rpt.Append("</tr>");


        }


        //rpt.Append("<tr>");
        //rpt.AppendFormat("<td colspan='9' style='font-family:Courier New; font-size:x-Medium; text-align:right;height:30px;'>{0}</td>", "");
        //rpt.Append("</tr>");
        //rpt.Append("<tr>");
        //rpt.AppendFormat("<td colspan='8' style='font-family:Courier New; font-size:x-Medium; text-align:right;'>{0}</td>", "Grand Total(Cash Refunds)");
        //rpt.AppendFormat("<td style='font-family:Courier New; font-size:x-Medium; text-align:right;'>{0}</td>", dtCashRefund.Rows[0]["RefAmt"]);
        //rpt.Append("</tr>");
        //rpt.Append("<tr>");
        //rpt.AppendFormat("<td colspan='8' style='font-family:Courier New; font-size:x-Medium; text-align:right;'>{0}</td>", "Grand Total(Bank Refunds)");
        //rpt.AppendFormat("<td style='font-family:Courier New; font-size:x-Medium; text-align:right;'>{0}</td>", dtCardRefund.Rows[0]["RefAmt"]);
        //rpt.Append("</tr>");

        rpt.Append("<tr>");
        rpt.AppendFormat("<td colspan='9' style='font-family:Courier New; font-size:x-Medium; text-align:right;height:60px;'>{0}</td>", "");
        rpt.Append("</tr>");

        rpt.Append("<tr>");
        rpt.AppendFormat("<td colspan='8' style='font-family:Courier New; font-size:x-Medium; text-align:right;'>{0}</td>", "Total Refund - Cash(Rs.)");
        rpt.AppendFormat("<td style='font-family:Courier New; font-size:x-Medium; text-align:right;'>{0}</td>", totamtBillCashRef);
        rpt.Append("</tr>");
        rpt.Append("<tr>");
        rpt.AppendFormat("<td colspan='8' style='font-family:Courier New; font-size:x-Medium; text-align:right;'>{0}</td>", "Total Refund - Card(Rs.)");
        rpt.AppendFormat("<td style='font-family:Courier New; font-size:x-Medium; text-align:right;'>{0}</td>", totamtBillCardRef);
        rpt.Append("</tr>");
        rpt.Append("<tr>");
        rpt.AppendFormat("<td colspan='8' style='ont-family:Courier New; font-size:x-Medium; text-align:right;'>{0}</td>", "Total Refund - e-Wallet(Rs.)");
        rpt.AppendFormat("<td style='font-family:Courier New; font-size:x-Medium; text-align:right;'>{0}</td>", totamtBillWalletRef);
        rpt.Append("</tr>");
        rpt.Append("<tr>");
        rpt.AppendFormat("<td colspan='8' style='ont-family:Courier New; font-size:x-Medium; text-align:right;'>{0}</td>", "Total Refund - Net Banking(Rs.)");
        rpt.AppendFormat("<td style='font-family:Courier New; font-size:x-Medium; text-align:right;'>{0}</td>", totamtBillNetBankRef);
        rpt.Append("</tr>");

        rpt.Append("<tr>");
        rpt.AppendFormat("<td colspan='9' style='font-family:Courier New; font-size:x-Medium; text-align:right;height:100px;'>{0}</td>", "");
        rpt.Append("</tr>");

        rpt.Append("<tr>");
        rpt.AppendFormat("<td colspan='9' style='font-family:Courier New; font-size:x-Medium; text-align:right;border-top:1px thin;'>{0}</td>", "");
        rpt.Append("</tr>");

        rpt.Append("<tr>");
        rpt.AppendFormat("<td colspan='7' style='font-family:Courier New; font-size:x-Medium; text-align:left;'>{0}</td>", "Total Collection After Refund");
        rpt.AppendFormat("<td style='font-family:Courier New; font-size:x-Medium; text-align:left;'>{0}</td>", "");
        rpt.AppendFormat("<td style='font-family:Courier New; font-size:x-Medium; text-align:right;'>{0}</td>", "");
        rpt.Append("</tr>");

        rpt.Append("<tr>");
        rpt.AppendFormat("<td colspan='8' style='font-family:Courier New; font-size:x-Medium; text-align:right;'>{0}</td>", "Collection - Cash(Rs.)");
        rpt.AppendFormat("<td style='font-family:Courier New; font-size:x-Medium; text-align:right;'>{0}</td>", totamtBillCash-totamtBillCashRef);
        rpt.Append("</tr>");
        rpt.Append("<tr>");
        rpt.AppendFormat("<td colspan='8' style='font-family:Courier New; font-size:x-Medium; text-align:right;'>{0}</td>", "Collection - Card(Rs.)");
        rpt.AppendFormat("<td style='font-family:Courier New; font-size:x-Medium; text-align:right;'>{0}</td>", totamtBillCard-totamtBillCardRef);
        rpt.Append("</tr>");
        rpt.Append("<tr>");
        rpt.AppendFormat("<td colspan='8' style='font-family:Courier New; font-size:x-Medium; text-align:right;'>{0}</td>", "Collection - e-Wallet(Rs.)");
        rpt.AppendFormat("<td style='font-family:Courier New; font-size:x-Medium; text-align:right;'>{0}</td>", totamtBillWallet-totamtBillWalletRef);
        rpt.Append("</tr>");
        rpt.Append("<tr>");
        rpt.AppendFormat("<td colspan='8' style='font-family:Courier New; font-size:x-Medium; text-align:right;'>{0}</td>", "Collection - Net Banking(Rs.)");
        rpt.AppendFormat("<td style='font-family:Courier New; font-size:x-Medium; text-align:right;'>{0}</td>", totamtBillNetBank-totamtBillNetBankRef);
        rpt.Append("</tr>");

        rpt.Append("<tr>");
        rpt.AppendFormat("<td colspan='7' style='font-family:Courier New; font-size:x-Medium; text-align:left;'>{0}</td>", "Debit Memo");
        rpt.AppendFormat("<td style='font-family:Courier New; font-size:x-Medium; text-align:left;'>{0}</td>", "");
        rpt.AppendFormat("<td style='font-family:Courier New; font-size:x-Medium; text-align:right;'>{0}</td>", "");
        rpt.Append("</tr>");

        rpt.Append("<tr>");
        rpt.AppendFormat("<td colspan='9' style='font-family:Courier New; font-size:x-Medium; text-align:right;height:50px;'>{0}</td>", "");
        rpt.Append("</tr>");

        rpt.Append("<tr>");
        rpt.AppendFormat("<td colspan='8' style='font-family:Courier New; font-size:x-Medium; text-align:right;'>{0}</td>", "Net Collection :");
        rpt.AppendFormat("<td style='font-family:Courier New; font-size:x-Medium; text-align:right;'>{0}</td>", gtotamtBill);
        rpt.Append("</tr>");
        //rpt.Append("<tr>");
        //rpt.AppendFormat("<td colspan='8' style='font-family:Courier New; font-size:x-Medium; text-align:right;'>{0}</td>", "Net Debit :");
        //rpt.AppendFormat("<td style='font-family:Courier New; font-size:x-Medium; text-align:right;'>{0}</td>", gtotamtBillRef);
        //rpt.Append("</tr>");
        rpt.Append("<tr>");
        rpt.AppendFormat("<td colspan='8' style='font-family:Courier New; font-size:x-Medium; text-align:right;'>{0}</td>", "Net refund :");
        rpt.AppendFormat("<td style='font-family:Courier New; font-size:x-Medium; text-align:right;'>{0}</td>", gtotamtBillRef);
        rpt.Append("</tr>");
        netcollection = gtotamtBill - gtotamtBillRef;
        rpt.Append("<tr>");
        rpt.AppendFormat("<td colspan='8' style='font-family:Courier New; font-size:x-Medium; text-align:right;'>{0}</td>", "Net Amount :");
        rpt.AppendFormat("<td style='font-family:Courier New; font-size:x-Medium; text-align:right;'>{0}</td>", netcollection);
        rpt.Append("</tr>");
        rpt.Append("<tr>");
        rpt.AppendFormat("<td colspan='7' style='font-family:Courier New; font-size:x-Medium; text-align:right;'>{0}</td>", "Generated On");
        rpt.AppendFormat("<td colspan='2' style='font-family:Courier New; font-size:small; text-align:right;'>{0}</td>", DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss"));
        rpt.Append("</tr>");
        rpt.Append("</table>");

        ltrReport.Text = rpt.ToString();
    }
    protected void Button2_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/HomePage.aspx");
    }

}