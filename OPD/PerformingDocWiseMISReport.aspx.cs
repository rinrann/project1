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

public partial class OPD_PerformingDocWiseMISReport : System.Web.UI.Page
{
    Main Objmain = new Main(ConfigurationSettings.AppSettings["ConnectionString"].ToString());
    PatientHistory theabortion = new PatientHistory(ConfigurationSettings.AppSettings["ConnectionString"].ToString());
    System.Text.StringBuilder rpt = new System.Text.StringBuilder();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["userId"] == null)
        {
            Response.Redirect("../LoginPage.aspx");
        }
        if (Objmain.checkaccss(Session["CoCode"].ToString(), Session["UserRoleID"].ToString().Trim(), "MONTHLY MIS REPORT", checkAccessType.ViewAction) == false)
        {
            Response.Redirect("../AccessDenied.aspx");
        }
        if (!IsPostBack)
        {
            string firstdayofmonth = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1).ToString("yyyy-MM-dd");
            string lastdayofmonth = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1).AddMonths(1).AddDays(-1).ToString("yyyy-MM-dd");
            txtfromdt.Text = firstdayofmonth;// Session["YearCode"].ToString().Substring(0, 4) + "-04-01";
            txttodt.Text = lastdayofmonth;
        }
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

    protected void btnGenRpt_Click(object sender, EventArgs e)
    {
        Report_Header();
        GetHearder_Detail();
        ltrReport.Text = rpt.ToString();
    }

    public void Report_Header()
    {
        
        System.Globalization.DateTimeFormatInfo dtf = new DateTimeFormatInfo();
        string date = DateTime.Now.ToString("dd/MM/yyyy");

        hd.Visible = true;


    }

    public void GetHearder_Detail()
    {
        if (txtDocName.Text.Trim() == "")
            txtDocId.Text = "";
        DataTable dtinvestigation = theabortion.GetReportDetails(Session["CoCode"].ToString(), txtDocId.Text.Trim(), txtfromdt.Text.Trim(), txttodt.Text.Trim(), rbloption.SelectedValue);

        rpt.Append("<table width='200%' cellspacing=0 border=1 bordercolor=silver style=' padding: 0;font-family:verdana; font-size:x-small'>");

        rpt.Append("<tr>");
        rpt.Append("<td><strong>SRL</strong></td>");
        rpt.Append("<td><strong>DATE</strong></td>");
        rpt.Append("<td><strong>INVOICE NUMBER</strong></td>");
        rpt.Append("<td><strong>INVOICE TIMING</strong></td>");
        rpt.Append("<td><strong>PERFORMING DOCTOR </strong></td>");
        rpt.Append("<td><strong>PATIENT REG NO </strong></td>");
        rpt.Append("<td><strong>PATIENT NAME</strong></td>");
        rpt.Append("<td><strong>REQUISITION NO </strong></td>");
        rpt.Append("<td><strong>AGE</strong></td>");
        rpt.Append("<td><strong>SEX</strong></td>");
        rpt.Append("<td><strong>ADDRESS</strong></td>");
        rpt.Append("<td><strong>CONSULTANT NAME</strong></td>");
        rpt.Append("<td><strong>REFERRED</strong></td>");
        rpt.Append("<td><strong>DEPARTMENT</strong></td>");
        rpt.Append("<td><strong>SERVICE GROUP</strong></td>");
        rpt.Append("<td><strong>SERVICE NAME</strong></td>");

        rpt.Append("<td><strong>SERVICE COST</strong></td>");
        rpt.Append("<td><strong>DISCOUNT AMT</strong></td>");
        rpt.Append("<td><strong>PAYABLE AMT</strong></td>");
        rpt.Append("<td><strong>ADVANCE AMT</strong></td>");
        rpt.Append("<td><strong>PAYMENT MODE</strong></td>");
        rpt.Append("<td><strong>DUE AMT</strong></td>");
        rpt.Append("<td><strong>REFUND AMT</strong></td>");
        rpt.Append("<td><strong>REFUND MODE</strong></td>");
        rpt.Append("<td><strong>STAFF ID</strong></td>");
        rpt.Append("<td><strong>STAFF NAME</strong></td>");
        rpt.Append("</tr>");

        string recptnocurr = "";
        string recptnonext = "";
        string reqcurr = "";
        string reqprev = "";
        string reqnext = "";
        string olddate = "";
        string newdate = "";
        string currsrv = "";
        string prevsrv = "";

        int srl = 1;
        decimal totalamt = 0;
        decimal totdiscamt = 0;
        decimal totpayableamt = 0;
        decimal totadvamt = 0;
        decimal totdueamt = 0;
        decimal totrefundamt = 0;
        int i = 0;

        for (i = 0; i < dtinvestigation.Rows.Count; i++)
        {
            /*if (rbloption.SelectedValue == "1")
            {
                docnew = dtinvestigation.Rows[i]["DocId"].ToString().Trim();
                reqnew = dtinvestigation.Rows[i]["ReqNo"].ToString().Trim();
                if (reqold != "" && reqold != reqnew)
                {
                    rpt.Append("<tr>");
                    rpt.Append("<td colspan=14 style='font-weight:bold;'></td>");
                    rpt.Append("<td style='font-weight:bold;text-align:right'>" + totalamt.ToString() + "</td>");
                    rpt.Append("<td style='font-weight:bold;text-align:right'>" + dtinvestigation.Rows[i-1]["DiscountAmount"].ToString() + "</td>");
                    rpt.Append("<td style='font-weight:bold;text-align:right'>" + dtinvestigation.Rows[i-1]["PayableAmount"].ToString() + "</td>");
                    rpt.Append("<td style='font-weight:bold;text-align:right'>" + dtinvestigation.Rows[i-1]["AdvanceAmount"].ToString() + "</td>");
                    rpt.Append("<td style='font-weight:bold;text-align:center'>" + dtinvestigation.Rows[i - 1]["paymode"].ToString() + "</td>");
                    rpt.Append("<td style='font-weight:bold;text-align:right'>" + dtinvestigation.Rows[i-1]["DueAmount"].ToString() + "</td>");
                    rpt.Append("<td style='font-weight:bold;text-align:right'>" + dtinvestigation.Rows[i-1]["RefundAmount"].ToString() + "</td>");
                    rpt.Append("<td style='font-weight:bold;text-align:center'>" + dtinvestigation.Rows[i-1]["refmode"].ToString() + "</td>");
                    rpt.Append("<td colspan=2 style='font-weight:bold;'></td>");
                    rpt.Append("</tr>");
                    totalamt = 0;
                }
                if (docold != docnew)
                {
                    rpt.Append("<tr>");
                    rpt.Append("<td colspan=24 style='font-weight:bold;'>" + dtinvestigation.Rows[i]["DocName"].ToString() + "</td>");
                    rpt.Append("</tr>");
                    srl = 1;
                }
            }
            else
            {
                docnew = dtinvestigation.Rows[i]["ReferredById"].ToString().Trim();
                if (reqold != "" && reqold != reqnew)
                {
                    rpt.Append("<tr>");
                    rpt.Append("<td colspan=13 style='font-weight:bold;'></td>");
                    rpt.Append("<td style='font-weight:bold;text-align:right'>" + totalamt.ToString() + "</td>");
                    rpt.Append("<td style='font-weight:bold;text-align:right'>" + dtinvestigation.Rows[i]["DiscountAmount"].ToString() + "</td>");
                    rpt.Append("<td style='font-weight:bold;text-align:right'>" + dtinvestigation.Rows[i]["PayableAmount"].ToString() + "</td>");
                    rpt.Append("<td style='font-weight:bold;text-align:right'>" + dtinvestigation.Rows[i]["AdvanceAmount"].ToString() + "</td>");
                    rpt.Append("<td style='font-weight:bold;text-align:center'>" + dtinvestigation.Rows[i]["paymode"].ToString() + "</td>");
                    rpt.Append("<td style='font-weight:bold;text-align:right'>" + dtinvestigation.Rows[i]["DueAmount"].ToString() + "</td>");
                    rpt.Append("<td style='font-weight:bold;text-align:right'>" + dtinvestigation.Rows[i]["RefundAmount"].ToString() + "</td>");
                    rpt.Append("<td style='font-weight:bold;text-align:center'>" + dtinvestigation.Rows[i]["refmode"].ToString() + "</td>");
                    rpt.Append("<td colspan=2 style='font-weight:bold;'></td>");
                    rpt.Append("</tr>");
                    totalamt = 0;
                }
                if (docold != docnew)
                {
                    rpt.Append("<tr>");
                    rpt.Append("<td colspan=23 style='font-weight:bold;'>" + dtinvestigation.Rows[i]["ReferredBy"].ToString() + "</td>");
                    rpt.Append("</tr>");
                    srl = 1;
                }
            }*/

            newdate = dtinvestigation.Rows[i]["ReqDt"].ToString();

            if (olddate != newdate && olddate != "")
            {
                //rpt.Append("<tr>");
                //rpt.Append("<td colspan=14 style='font-weight:bold;'>" + olddate + " Total :</td>");
                //rpt.Append("<td style='font-weight:bold;text-align:right'>" + totalamt.ToString() + "</td>");
                //rpt.Append("<td style='font-weight:bold;text-align:right'>" + totdiscamt.ToString() + "</td>");
                //rpt.Append("<td style='font-weight:bold;text-align:right'>" + totpayableamt.ToString() + "</td>");
                //rpt.Append("<td style='font-weight:bold;text-align:right'>" + totadvamt.ToString() + "</td>");
                //rpt.Append("<td style='font-weight:bold;text-align:center'></td>");
                //rpt.Append("<td style='font-weight:bold;text-align:right'>" + totdueamt.ToString() + "</td>");
                //rpt.Append("<td style='font-weight:bold;text-align:right'>" + totrefundamt.ToString() + "</td>");
                //rpt.Append("<td style='font-weight:bold;text-align:center'> </td>");
                //rpt.Append("<td colspan=2 style='font-weight:bold;'></td>");
                //rpt.Append("</tr>");

                //totalamt = 0;
                //totdiscamt = 0;
                //totpayableamt = 0;
                //totadvamt = 0;
                //totdueamt = 0;
                //totrefundamt = 0;

            }

            reqcurr = dtinvestigation.Rows[i]["ReqNo"].ToString();
            currsrv = dtinvestigation.Rows[i]["ServiceName"].ToString();
            recptnocurr = dtinvestigation.Rows[i]["ReceiptNo"].ToString();
            if (i + 1 < dtinvestigation.Rows.Count)
            {
                reqnext = dtinvestigation.Rows[i+1]["ReqNo"].ToString();
                recptnonext = dtinvestigation.Rows[i+1]["ReceiptNo"].ToString();
            }
            else
            {
                reqnext = "";
                recptnonext = ""; 
            }
            rpt.Append("<tr>");
            rpt.Append("<td>" + srl.ToString() + "</td>");
            rpt.Append("<td>" + dtinvestigation.Rows[i]["PayDt"].ToString() + "</td>");
            rpt.Append("<td>" + dtinvestigation.Rows[i]["ReceiptNo"].ToString() + "</td>");
            rpt.Append("<td>" + dtinvestigation.Rows[i]["PayTime"].ToString() + "</td>");
            rpt.Append("<td>" + dtinvestigation.Rows[i]["DocName"].ToString() + "</td>");
            rpt.Append("<td>" + dtinvestigation.Rows[i]["RegNo"].ToString() + "</td>");
            rpt.Append("<td>" + dtinvestigation.Rows[i]["PatientName"].ToString() + "</td>");
            rpt.Append("<td>" + dtinvestigation.Rows[i]["ReqNo"].ToString() + "</td>");
            rpt.Append("<td>" + dtinvestigation.Rows[i]["Age"].ToString() + "</td>");
            rpt.Append("<td>" + dtinvestigation.Rows[i]["Sex"].ToString() + "</td>");
            rpt.Append("<td>" + dtinvestigation.Rows[i]["Address"].ToString() + "</td>");
            rpt.Append("<td>" + dtinvestigation.Rows[i]["ConsultantName"].ToString() + "</td>");
            if (rbloption.SelectedValue == "1")
            {
                if (dtinvestigation.Rows[i]["ReferredById"].ToString().Trim() == "Self")
                    rpt.Append("<td>" + dtinvestigation.Rows[i]["ReferredById"].ToString() + "</td>");
                else
                    rpt.Append("<td>" + dtinvestigation.Rows[i]["ReferredBy"].ToString() + "</td>");
            }
            rpt.Append("<td>" + dtinvestigation.Rows[i]["Department"].ToString() + "</td>");
            rpt.Append("<td>" + dtinvestigation.Rows[i]["ServiceGroup"].ToString() + "</td>");
            rpt.Append("<td>" + dtinvestigation.Rows[i]["ServiceName"].ToString() + "</td>");
            if (rbloption.SelectedValue == "2")
            {
                rpt.Append("<td>" + dtinvestigation.Rows[i]["DocName"].ToString() + "</td>");
            }

            rpt.Append("<td style='text-align:right'>" + dtinvestigation.Rows[i]["TotalAmount"].ToString() + "</td>");
            totalamt = totalamt + Convert.ToDecimal(dtinvestigation.Rows[i]["TotalAmount"].ToString());


            //if (reqcurr != reqprev)
            //{
            //    rpt.Append("<td style='text-align:right'>" + dtinvestigation.Rows[i]["TotalAmount"].ToString() + "</td>");
            //    totalamt = totalamt + Convert.ToDecimal(dtinvestigation.Rows[i]["TotalAmount"].ToString());
            //}
            //else
            //{
            //    if (currsrv != prevsrv)
            //    {
            //        rpt.Append("<td style='text-align:right'>" + dtinvestigation.Rows[i]["TotalAmount"].ToString() + "</td>");
            //        totalamt = totalamt + Convert.ToDecimal(dtinvestigation.Rows[i]["TotalAmount"].ToString());
            //    }
            //    else
            //    {
            //        rpt.Append("<td style='text-align:right'></td>");
            //    }
            //}

            if (reqcurr != reqnext)
            {
                rpt.Append("<td style='text-align:right'>" + dtinvestigation.Rows[i]["DiscountAmount"].ToString() + "</td>");
                rpt.Append("<td style='text-align:right'>" + dtinvestigation.Rows[i]["PayableAmount"].ToString() + "</td>");
                totdiscamt = totdiscamt + Convert.ToDecimal(dtinvestigation.Rows[i]["DiscountAmount"].ToString());
                totpayableamt = totpayableamt + Convert.ToDecimal(dtinvestigation.Rows[i]["PayableAmount"].ToString());
            }
            else
            {
                rpt.Append("<td style='text-align:right'></td>");
                rpt.Append("<td style='text-align:right'></td>");
            }

            //rpt.Append("<td style='text-align:right'>" + dtinvestigation.Rows[i]["TotalAmount"].ToString() + "</td>");
            //totpayableamt = totpayableamt + Convert.ToDecimal(dtinvestigation.Rows[i]["TotalAmount"].ToString());


            if (recptnocurr != recptnonext)
            {
                rpt.Append("<td style='text-align:right'>" + dtinvestigation.Rows[i]["AdvanceAmount"].ToString() + "</td>");
                //rpt.Append("<td style='text-align:left'>" + dtinvestigation.Rows[i]["paymode"].ToString() + "</td>");
                totadvamt = totadvamt + Convert.ToDecimal(dtinvestigation.Rows[i]["AdvanceAmount"].ToString());
            }
            else
            {
                rpt.Append("<td style='text-align:right'></td>");
                //rpt.Append("<td style='text-align:left'></td>");
            }
            rpt.Append("<td style='text-align:left'>" + dtinvestigation.Rows[i]["paymode"].ToString() + "</td>");
            if (reqcurr != reqnext)
            {
                rpt.Append("<td style='text-align:right'>" + dtinvestigation.Rows[i]["DueAmount"].ToString() + "</td>");
                totdueamt = totdueamt + Convert.ToDecimal(dtinvestigation.Rows[i]["DueAmount"].ToString());
            }
            else
            {
                rpt.Append("<td style='text-align:right'></td>");
            }
            //rpt.Append("<td style='text-align:right'>" + dtinvestigation.Rows[i]["DueAmount"].ToString() + "</td>");
            //totdueamt = totdueamt + Convert.ToDecimal(dtinvestigation.Rows[i]["DueAmount"].ToString());
            if (recptnocurr != recptnonext)
            {
                rpt.Append("<td style='text-align:right'>" + dtinvestigation.Rows[i]["RefundAmount"].ToString() + "</td>");
                rpt.Append("<td style='text-align:left'>" + dtinvestigation.Rows[i]["refmode"].ToString() + "</td>");

                rpt.Append("<td>" + dtinvestigation.Rows[i]["StaffId"].ToString() + "</td>");
                rpt.Append("<td>" + dtinvestigation.Rows[i]["StaffName"].ToString() + "</td>");
                totrefundamt = totrefundamt + Convert.ToDecimal(dtinvestigation.Rows[i]["RefundAmount"].ToString());
            }
            else
            {
                
                
               
                rpt.Append("<td style='text-align:right'></td>");
                rpt.Append("<td style='text-align:left'></td>");

                rpt.Append("<td></td>");
                rpt.Append("<td></td>");
            }
            rpt.Append("</tr>");

            srl = srl + 1;
            //docold = docnew;
            //reqold = reqnew;
            //olddate = newdate;
            prevsrv = currsrv;
            reqprev = reqcurr;

            
            
            
            
            
            
        }

            //rpt.Append("<tr>");
            //rpt.Append("<td colspan=13 style='font-weight:bold;'></td>");
            //rpt.Append("<td style='font-weight:bold;text-align:right'>" + totalamt.ToString() + "</td>");
            //rpt.Append("<td style='font-weight:bold;text-align:right'>" + dtinvestigation.Rows[i-1]["DiscountAmount"].ToString() + "</td>");
            //rpt.Append("<td style='font-weight:bold;text-align:right'>" + dtinvestigation.Rows[i-1]["PayableAmount"].ToString() + "</td>");
            //rpt.Append("<td style='font-weight:bold;text-align:right'>" + dtinvestigation.Rows[i-1]["AdvanceAmount"].ToString() + "</td>");
            //rpt.Append("<td style='font-weight:bold;text-align:center'>" + dtinvestigation.Rows[i - 1]["paymode"].ToString() + "</td>");
            //rpt.Append("<td style='font-weight:bold;text-align:right'>" + dtinvestigation.Rows[i-1]["DueAmount"].ToString() + "</td>");
            //rpt.Append("<td style='font-weight:bold;text-align:right'>" + dtinvestigation.Rows[i-1]["RefundAmount"].ToString() + "</td>");
            //rpt.Append("<td style='font-weight:bold;text-align:center'>" + dtinvestigation.Rows[i-1]["refmode"].ToString() + "</td>");
            //rpt.Append("<td colspan=2 style='font-weight:bold;'></td>");
            //rpt.Append("</tr>");
            //totalamt = 0;
        rpt.Append("<tr>");
        rpt.Append("<td colspan=16 style='font-weight:bold;'>Total :</td>");
        rpt.Append("<td style='font-weight:bold;text-align:right'>" + totalamt.ToString() + "</td>");
        rpt.Append("<td style='font-weight:bold;text-align:right'>" + totdiscamt.ToString() + "</td>");
        rpt.Append("<td style='font-weight:bold;text-align:right'>" + totpayableamt.ToString() + "</td>");
        rpt.Append("<td style='font-weight:bold;text-align:right'>" + totadvamt.ToString() + "</td>");
        rpt.Append("<td style='font-weight:bold;text-align:center'></td>");
        rpt.Append("<td style='font-weight:bold;text-align:right'>" + totdueamt.ToString() + "</td>");
        rpt.Append("<td style='font-weight:bold;text-align:right'>" + totrefundamt.ToString() + "</td>");
        rpt.Append("<td style='font-weight:bold;text-align:center'> </td>");
        rpt.Append("<td colspan=2 style='font-weight:bold;'></td>");
        rpt.Append("</tr>");   

        rpt.Append("</table>");
    }
    protected void rbloption_SelectedIndexChanged(object sender, EventArgs e)
    {  if (rbloption.SelectedValue == "1")
        {
            td_1.Visible = true;
            td_2.Visible = false;
            td_3.Visible = true;
        }
        else if (rbloption.SelectedValue == "2")
        {
            td_1.Visible = false;
            td_2.Visible = true;
            td_3.Visible = true;
        }
    }
    protected void btn_excel_Click(object sender, EventArgs e)
    {
        Response.Clear();
        Response.Buffer = true;
        Response.ContentType = "application/vnd.ms-excel";
        Response.Charset = "";
        Response.AppendHeader("content-disposition", "attachment; filename=DocWiseMISReport.xls");
        Response.Cache.SetCacheability(HttpCacheability.NoCache);
        this.EnableViewState = false;
        System.IO.StringWriter oStringWriter = new System.IO.StringWriter();
        System.Web.UI.HtmlTextWriter oHtmlTextWriter = new System.Web.UI.HtmlTextWriter(oStringWriter);
        ltrReport.RenderControl(oHtmlTextWriter);
        Response.Write(oStringWriter.ToString());
        Response.End();
    }
    protected void BtnBack_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/HomePage.aspx");
    }
}