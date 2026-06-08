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

public partial class Pathology_RequisitionBill : System.Web.UI.Page
{
    Main Objmain = new Main(ConfigurationSettings.AppSettings["ConnectionString"].ToString());
    PH_PatientRequisition thereq = new PH_PatientRequisition(ConfigurationSettings.AppSettings["ConnectionString"].ToString());
    PH_TestPopupMultiple thedia = new PH_TestPopupMultiple(ConfigurationSettings.AppSettings["ConnectionString"].ToString());
    AnyTimePayment anypayment = new AnyTimePayment(ConfigurationSettings.AppSettings["ConnectionString"].ToString());
    System.Text.StringBuilder rpt = new System.Text.StringBuilder();
    String Date = DateTime.Now.ToString("dd/MM/yyyy");

    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["userName"] == null)
        {
            Response.Redirect("../LoginPage.aspx");
        }
        if (Objmain.checkaccss(Session["CoCode"].ToString(), Session["UserRoleID"].ToString().Trim(), "REQUISITION BILL ENTRY", checkAccessType.ViewAction) == false)
        {
            Response.Redirect("../AccessDenied.aspx");
        }
        if (Objmain.checkaccss(Session["CoCode"].ToString(), Session["UserRoleID"].ToString().Trim(), "REQUISITION BILL ENTRY", checkAccessType.InsertAction) == false)
        {
            //Button1.Enabled = false;
        }

        ScriptManager.RegisterStartupScript(this, this.GetType(), "msg", "GetDatetime();", true);
        Page.Title = "Requisition Bill Entry";
        string previousPageName = System.IO.Path.GetFileName(Request.UrlReferrer.AbsolutePath);
        string prevUrl = Request.UrlReferrer.ToString().ToString();
        
        if (!IsPostBack)
        {
            if (previousPageName == "HomePage.aspx")
            {
                Session["BillRegNo"] = null;
            }
            if (Session["BillRegNo"] != null)
            {
                getPaymentDetails();
            }
            else
            {
                Tab1Func();
                GridFill();
            }
            if (Request.UrlReferrer != null)
            {
                string previousPageUrl = Request.UrlReferrer.AbsoluteUri;
                previousPageName = System.IO.Path.GetFileName(Request.UrlReferrer.AbsolutePath);
            }
        }
    }

    public void Tab1Func()
    {
        MainView.ActiveViewIndex = 0;

    }

    public void GridFill()
    {
        DataTable dt = thereq.GridFillDue(Session["CoCode"].ToString().Trim(), Session["YearCode"].ToString().Trim(), ddltype.SelectedValue,txtnameSrch.Text);
        GridView1.DataSource = dt;
        GridView1.DataBind();
    }

    protected void GridView1_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        GridView1.PageIndex = e.NewPageIndex;
        GridFill();
    }
    protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "PayNow")
        {
            GridViewRow rowSelect = (GridViewRow)(((LinkButton)e.CommandSource).NamingContainer);
            int index = rowSelect.RowIndex;
            //int index = Convert.ToInt32(row.RowIndex);

            string reqno = (e.CommandArgument).ToString();
            Label lblBillAmt = (Label)GridView1.Rows[index].FindControl("lblBillAmt");
            Label lblPaidAmt = (Label)GridView1.Rows[index].FindControl("lblPaidAmt");
            Label lblDueAmt = (Label)GridView1.Rows[index].FindControl("lblDueAmt");
            Label lblname = (Label)GridView1.Rows[index].FindControl("lblname");
            Label lblregno = (Label)GridView1.Rows[index].FindControl("lblregno");
            Label lblDiscAmt = (Label)GridView1.Rows[index].FindControl("lblDiscAmt");   
            Label lblReqType = (Label)GridView1.Rows[index].FindControl("lblReqType");
            Label lblbilldate = (Label)GridView1.Rows[index].FindControl("lblbilldate");
            DataTable dt = new DataTable();
            if (ddltype.SelectedValue == "1")
                dt = thedia.GetExistTestDetails(Session["CoCode"].ToString().Trim(), Session["YearCode"].ToString().Trim(), reqno, "");
            else dt = thereq.GridtestDtls(Session["CoCode"].ToString().Trim(), Session["YearCode"].ToString().Trim(), reqno);

            GridView2.DataSource = dt;
            GridView2.DataBind();

            DataTable dtBill = anypayment.GetPendingAdvanceDetails(Session["CoCode"].ToString().Trim(), Session["YearCode"].ToString().Trim(), lblregno.Text);
            GridView3.DataSource = dtBill;
            GridView3.DataBind();

            lblPRegno.Text = lblregno.Text;
            lblPatientreqno.Text = reqno;
            lblPname.Text = lblname.Text;
            lblTotBillAmt.Text = lblBillAmt.Text;
            lblPaidBillAmt.Text = lblPaidAmt.Text;
            txtPayableAmt.Text = lblDueAmt.Text;
            lblDueBillAmt.Text = lblDueAmt.Text;
            lblDueBillAmtOrg.Text = lblDueAmt.Text;
            lbldiscountamt.Text = (lblDiscAmt.Text == "" ? "0" : lblDiscAmt.Text);
            txtdiscountamt.Text = (lblDiscAmt.Text==""?"0":lblDiscAmt.Text);
            lblBillType.Text = lblReqType.Text;
            lblReqdate.Text = lblbilldate.Text;
            lblAdvAdjustedAmt.Text = "0.00";
            MainView.ActiveViewIndex = 1;

            
        }
    }

    private void getPaymentDetails()
    {
        string reqno = Session["BillReqNo"].ToString();

        DataTable dt1 = new DataTable();
        dt1 = thedia.GetExistTestDetails(Session["CoCode"].ToString().Trim(), Session["YearCode"].ToString().Trim(), reqno, "");
        GridView2.DataSource = dt1;
        GridView2.DataBind();
        lblPRegno.Text = Session["BillRegNo"].ToString();
        lblPatientreqno.Text = reqno;
        lblPname.Text = Session["BillPtName"].ToString();
        lblTotBillAmt.Text = Session["BillAmt"].ToString();
        lblPaidBillAmt.Text = "0.00";
        txtPayableAmt.Text = Session["BillPayableAmt"].ToString();
        lblDueBillAmt.Text = Session["BillPayableAmt"].ToString();
        lblDueBillAmtOrg.Text = Session["BillPayableAmt"].ToString();
        lbldiscountamt.Text = Session["BillDiscAmt"].ToString();
        if (lbldiscountamt.Text == "")
        {
            lbldiscountamt.Text = "0.00";
        }
        txtdiscountamt.Text = lbldiscountamt.Text;
        lblBillType.Text = Session["BillType"].ToString();
        lblReqdate.Text = Session["BillDate"].ToString();
        lblAdvAdjustedAmt.Text = "0.00";
        MainView.ActiveViewIndex = 1;
        Session["BillRegNo"] = "";
        Session["BillReqNo"] = "";
        DataTable dtBill = anypayment.GetPendingAdvanceDetails(Session["CoCode"].ToString().Trim(), Session["YearCode"].ToString().Trim(), lblPRegno.Text);
        GridView3.DataSource = dtBill;
        GridView3.DataBind();
    }
    protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            Label lblreqno = (Label)e.Row.FindControl("lblreqno");
            Label lblBillAmt = (Label)e.Row.FindControl("lblBillAmt");
            Label lblPaidAmt = (Label)e.Row.FindControl("lblPaidAmt");
            Label lblDueAmt = (Label)e.Row.FindControl("lblDueAmt");
            Label lblvchno = (Label)e.Row.FindControl("lblvchno");
            LinkButton lnk = (LinkButton)e.Row.FindControl("LinkButton9");
            string reqno = lblreqno.Text.Trim();
            decimal totAmt = 0;

            //if (ddltype.SelectedValue == "1")
            //{
            //    DataTable dt = thedia.GetExistTestDetails(Session["CoCode"].ToString().Trim(), Session["YearCode"].ToString().Trim(), reqno, "");
            //    for (int i = 0; i < dt.Rows.Count; i++)
            //    {
            //        totAmt = totAmt + Convert.ToDecimal(dt.Rows[i]["Cost"]);
            //    }
            //    lblBillAmt.Text = totAmt.ToString();
            //}
            if (Convert.ToDecimal(lblDueAmt.Text) == 0)
            {
                lnk.Enabled = false;
                lnk.Text = "Paid";
            }
        }
    }
    protected void GridView2_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        DataTable databound = new DataTable();
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            Label lblSerial = (Label)e.Row.FindControl("lblSerial");
            lblSerial.Text = ((GridView1.PageIndex * GridView1.PageSize) + e.Row.RowIndex + 1).ToString();

            Label lblTestReqNo = (Label)e.Row.FindControl("lblTestReqNo");

        }
    }
    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        Decimal dueamt = Convert.ToDecimal(lblDueBillAmtOrg.Text);
        decimal discount = Convert.ToDecimal(txtdiscountamt.Text);
        Decimal payableAmt = Convert.ToDecimal(txtPayableAmt.Text);
        Decimal PaidAmt = Convert.ToDecimal(lblPaidBillAmt.Text);
        Decimal totbillAmt = Convert.ToDecimal(lblTotBillAmt.Text);
        Decimal adjAmt=(txtAdvBillAmt.Text==""?0:Convert.ToDecimal(txtAdvBillAmt.Text));
        Decimal curDueAmt = 0;
        Decimal totpaidAmt = 0;
        if (adjAmt==0 && payableAmt <= 0)
        {
            lblError.Text = "Payable Amount cannot be equal or less than Zero !!";
            lblError.Visible = true;

        }
        else if (payableAmt > dueamt)
        {
            lblError.Text = "Payable Amount cannot be greater than Due Amount !!";
            lblError.Visible = true;
        }
        //else if (txtAdvBillNo.Text != "" && adjAmt > payableAmt)
        //{
        //    lblError.Text = "Adjusted Amount cannot be greater than Payable Amount !!";
        //    lblError.Visible = true;
        //}
        //else if (txtAdvBillNo.Text != "" && adjAmt < payableAmt)
        //{
        //    lblError.Text = "Adjusted Amount cannot be less than Payable Amount !!";
        //    lblError.Visible = true;
        //}
        else
        {
            if (ddltype.SelectedValue == "2")
            {
                if (payableAmt < dueamt)
                {
                    lblError.Text = "Payable Amount cannot be less than Due Amount !!";
                    lblError.Visible = true;
                    return;
                }
            }
            totpaidAmt = PaidAmt + payableAmt + adjAmt;
            curDueAmt = totbillAmt - discount - totpaidAmt;

            //string chqdt = "";
            //if (txtchqdt.Text != "")
            //{
            //    string[] cdt = txtchqdt.Text.Split('/');
            //    chqdt = cdt[1] + cdt[0];
            //}
            DataTable dt, dt1;
            string invoiceType = "";
            string InvoiceNo = "";
            string updInvNo = "0";
            //dt1 = thereq.GenerateReceiptNo(Session["CoCode"].ToString().Trim(), Session["YearCode"].ToString().Trim(), "RCP");
            //string ReceptNo = dt1.Rows[0][0].ToString();
            //string ReceptNo = "";
            string curmonth = "";
            if (DateTime.Now.Month > 9)
            {
                curmonth = DateTime.Now.Month.ToString();

            }
            else
            {
                curmonth = "0" + DateTime.Now.Month.ToString();
            }
            //DataTable dtinv = thereq.checkInvoice(Session["CoCode"].ToString().Trim(), Session["YearCode"].ToString().Trim(), lblPRegno.Text.Trim(), lblPatientreqno.Text.Trim());
            //if (dtinv.Rows.Count > 0)
            //{
            //    //string cursrl="0000"+(dtinv.Rows.Count+1).ToString();
            //    InvoiceNo = dtinv.Rows[0]["ReceiptNo"].ToString();
            //    updInvNo = "0";
            //    // ReceptNo = ReceptNo + cursrl.Substring(cursrl.Length - 4);
            //}
            //else
            //{
            //    dt = thereq.GenerateReceiptNo(Session["CoCode"].ToString().Trim(), Session["YearCode"].ToString().Trim(), lblBillType.Text.Trim());
            //    InvoiceNo = dt.Rows[0][0].ToString();
            //    updInvNo = "1";
            //    //ReceptNo = ReceptNo + "0001";
            //}


            dt = thereq.GenerateReceiptNo(Session["CoCode"].ToString().Trim(), Session["YearCode"].ToString().Trim(), lblBillType.Text.Trim());
            InvoiceNo = dt.Rows[0][0].ToString();
            string bookcode = thereq.GetBookCode(Session["CoCode"].ToString().Trim(), Session["YearCode"].ToString().Trim(), ddlPaymentMode.SelectedValue.Trim());
            string vchno = thereq.GenerateVoucherNo(Session["CoCode"].ToString().Trim(), Session["YearCode"].ToString().Trim(), bookcode);
            
            //reqtype = lblBillType.Text.Trim();
            if (curDueAmt > 0)
            {
                invoiceType = "M";
            }
            else
            {
                invoiceType = "I";
            }

            if (thereq.UpdateRequisitionBill(Session["CoCode"].ToString().Trim(), Session["YearCode"].ToString().Trim(), lblPRegno.Text.Trim(), lblPatientreqno.Text.Trim(), totpaidAmt.ToString(), curDueAmt.ToString(), ddlPaymentMode.SelectedValue.Trim(), txtBankName.Text, txtChequeNo.Text, txtchqdt.Text, DateTime.Now.ToString("yyyy-MM-dd"), Session["userName"].ToString(), ddltype.SelectedValue, payableAmt.ToString(), ddlPayType.SelectedValue, lblBillType.Text.Trim(), lblReqdate.Text.Trim(), Session["userId"].ToString().Trim(), invoiceType, InvoiceNo, vchno, bookcode, discount.ToString(), updInvNo) == true)
            {
                lblError.Text = "Record Successfully Saved....";
                lblError.ForeColor = Color.Green;
                lblError.Visible = true;
                divOpt.Visible = true;
                //btnrcpt.Visible = true;
                btnInvoice.Visible = true;
                //lblreceitpNo.Text = ReceptNo;
                lblinvoiceNo.Text = InvoiceNo;

                //if (txtAdvBillNo.Text.Length > 0)
                //{
                //    string[] advBills = txtAdvBillNo.Text.Trim(',').Split(',');
                //    for (int i = 0; i<advBills.Length; i++)
                //    {
                //        thereq.UpdateAdvanceBill(Session["CoCode"].ToString().Trim(), Session["YearCode"].ToString().Trim(), vchno,lblPRegno.Text.Trim(), advBills[i]);
                //    }
                //}
                foreach (GridViewRow row in GridView3.Rows)
                {
                    Label lblrcptNo = (Label)row.FindControl("lblrcptNo");
                    Label lblAmt = (Label)row.FindControl("lblAmt");
                    Label lblbalAmt = (Label)row.FindControl("lblbalAmt");
                    CheckBox chkselect = (CheckBox)row.FindControl("chkselect");
                    double adjustAmt = Convert.ToDouble(lblAmt.Text) - Convert.ToDouble(lblbalAmt.Text);
                    int stats = 0;
                    if (Convert.ToDouble(lblbalAmt.Text) == 0)
                    {
                        stats = 1;
                    }
                    if (chkselect.Checked == true)
                    {
                        thereq.UpdateAdvanceBill(Session["CoCode"].ToString().Trim(), Session["YearCode"].ToString().Trim(), vchno, lblPRegno.Text.Trim(), lblrcptNo.Text, adjustAmt, stats, lblPatientreqno.Text.Trim());
                    }
                }
                DataTable testcostdetails = thedia.GetExistTestCostDetails(Session["CoCode"].ToString().Trim(), Session["YearCode"].ToString().Trim(), lblPatientreqno.Text, ddltype.SelectedValue);
                lblPaidBillAmt.Text = testcostdetails.Rows[0]["paid"].ToString();
                lblDueBillAmt.Text = testcostdetails.Rows[0]["due"].ToString();
                txtPayableAmt.Text = testcostdetails.Rows[0]["due"].ToString();
            }
            else
            {
                lblError.Text = "Error in Saving ";
                lblError.ForeColor = Color.Red;
                lblError.Visible = true;
            }


            //if (lblBillType.Text.Trim() == "INF")
            //{
            //    string reqtype = "";
            //    if (curDueAmt > 0)
            //    {
            //        reqtype = "RCP";
            //        dt = thereq.GenerateReceiptNo(Session["CoCode"].ToString().Trim(), Session["YearCode"].ToString().Trim(), "RCP");
            //        invoiceType = "M";
            //    }
            //    else
            //    {
            //        reqtype = lblBillType.Text.Trim();
            //        dt = thereq.GenerateReceiptNo(Session["CoCode"].ToString().Trim(), Session["YearCode"].ToString().Trim(), lblBillType.Text.Trim());
            //        invoiceType = "I";
            //    }

            //    string ReceptNo = dt.Rows[0][0].ToString();
            //    if (thereq.UpdateRequisitionBill(Session["CoCode"].ToString().Trim(), Session["YearCode"].ToString().Trim(), lblPRegno.Text.Trim(), lblPatientreqno.Text.Trim(), totpaidAmt.ToString(), curDueAmt.ToString(), ddlPaymentMode.SelectedValue.Trim(), txtBankName.Text, txtChequeNo.Text, txtchqdt.Text, DateTime.Now.ToString("yyyy-MM-dd"), Session["userName"].ToString(), ddltype.SelectedValue, payableAmt.ToString(), ddlPayType.SelectedValue, ReceptNo, reqtype, lblReqdate.Text.Trim(), Session["userId"].ToString().Trim(), invoiceType) == true)
            //    {
            //        lblError.Text = "Record Successfully Saved....";
            //        lblError.ForeColor = Color.Green;
            //        lblError.Visible = true;
            //        divOpt.Visible = true;
            //        btnrcpt.Visible = true;
            //        lblreceitpNo.Text = ReceptNo;

            //        DataTable testcostdetails = thedia.GetExistTestCostDetails(Session["CoCode"].ToString().Trim(), Session["YearCode"].ToString().Trim(), lblPatientreqno.Text, ddltype.SelectedValue);
            //        lblPaidBillAmt.Text = testcostdetails.Rows[0]["paid"].ToString();
            //        lblDueBillAmt.Text = testcostdetails.Rows[0]["due"].ToString();
            //        txtPayableAmt.Text = testcostdetails.Rows[0]["due"].ToString();
            //    }
            //    else
            //    {
            //        lblError.Text = "Error in Saving ";
            //        lblError.ForeColor = Color.Red;
            //        lblError.Visible = true;
            //    }
            //}
            //else
            //{
            //    dt = thereq.GenerateReceiptNo(Session["CoCode"].ToString().Trim(), Session["YearCode"].ToString().Trim(), lblBillType.Text.Trim());
            //    invoiceType = "I";
            //    string ReceptNo = dt.Rows[0][0].ToString();
            //    if (thereq.UpdateRequisitionBill(Session["CoCode"].ToString().Trim(), Session["YearCode"].ToString().Trim(), lblPRegno.Text.Trim(), lblPatientreqno.Text.Trim(), totpaidAmt.ToString(), curDueAmt.ToString(), ddlPaymentMode.SelectedValue.Trim(), txtBankName.Text, txtChequeNo.Text, txtchqdt.Text, DateTime.Now.ToString("yyyy-MM-dd"), Session["userName"].ToString(), ddltype.SelectedValue, payableAmt.ToString(), ddlPayType.SelectedValue, ReceptNo, lblBillType.Text.Trim(), lblReqdate.Text.Trim(), Session["userId"].ToString().Trim(), invoiceType) == true)
            //    {
            //        lblError.Text = "Record Successfully Saved....";
            //        lblError.ForeColor = Color.Green;
            //        lblError.Visible = true;
            //        divOpt.Visible = true;
            //        btnrcpt.Visible = true;
            //        lblreceitpNo.Text = ReceptNo;

            //        DataTable testcostdetails = thedia.GetExistTestCostDetails(Session["CoCode"].ToString().Trim(), Session["YearCode"].ToString().Trim(), lblPatientreqno.Text, ddltype.SelectedValue);
            //        lblPaidBillAmt.Text = testcostdetails.Rows[0]["paid"].ToString();
            //        lblDueBillAmt.Text = testcostdetails.Rows[0]["due"].ToString();
            //        txtPayableAmt.Text = testcostdetails.Rows[0]["due"].ToString();
            //    }
            //    else
            //    {
            //        lblError.Text = "Error in Saving ";
            //        lblError.ForeColor = Color.Red;
            //        lblError.Visible = true;
            //    }
            //}
        }

    }
    protected void btnCancel_Click(object sender, EventArgs e)
    {
        Response.Redirect("../HomePage.aspx");
    }

    protected void ddlPaymentMode_SelectedIndexChanged(object sender, EventArgs e)
    {

        if (ddlPaymentMode.SelectedValue == "R")
        {
            lblchqdt.InnerText = "Transaction ID(Last 4 Digit) :";
            lblchqno.InnerText = "Card No :";
            lblbankNm.InnerText = "Card Holder Name :";
            divchqdt.Visible = true;
            divchqno.Visible = true;
            divBank.Visible = true;
        }
        else if (ddlPaymentMode.SelectedValue == "N")
        {
            lblchqdt.InnerText = "Transaction ID(Last 4 Digit) :";
            lblchqno.InnerText = "Card No :";
            lblbankNm.InnerText = "Bank Name :";
            divchqdt.Visible = true;
            divchqno.Visible = false;
            divBank.Visible = true;
        }
        else
        {
            divchqdt.Visible = false;
            divchqno.Visible = false;
            divBank.Visible = false;
        }
    }
    protected void btnrcpt_Click(object sender, EventArgs e)
    {
        string someScript = "";
        someScript = "<script language='javascript'> var el = document.getElementById('h1');el.style.display = 'none';</script>";
        Page.ClientScript.RegisterStartupScript(this.GetType(), "onload", someScript);

        GetReport("M");
        Button7.Visible = true;
        Button6.Visible = true;
    }

    protected void btnInvoice_Click(object sender, EventArgs e)
    {
        string someScript = "";
        someScript = "<script language='javascript'> var el = document.getElementById('h1');el.style.display = 'none';</script>";
        Page.ClientScript.RegisterStartupScript(this.GetType(), "onload", someScript);

        GetReport("I");

        Button7.Visible = true;
        Button6.Visible = true;
    }

    public void GetReport(string type)
    {
        if (type == "I")
        {
            BillGetHearder_DetailInvoice("I");
        }
        else
        {
            GetHearder_Detail(type);
        }
        ltrReport.Text = rpt.ToString();
    }

    public void Report_Header()
    {
        System.Globalization.DateTimeFormatInfo dtf = new DateTimeFormatInfo();
        string date = DateTime.Now.ToString("dd/MM/yyyy");

        if (RadioButtonList1.SelectedValue == "Y")
        {
            rpt.Append("<table width='100%' cellspacing=0 border=0 >");
            rpt.Append("<tr cellpadding:'0'  style='height:30px;'>");
            rpt.Append("<td width='30%'><img src='../Images/logo.jpg'  style=''/></td>");
            rpt.Append("<td   width='40%'  style='height:20px;font-family:Arial; font-size:medium; text-align:center'></td>");
            rpt.Append("<td width='30%' style='text-align:right'>Print Date : " + date + "</td>");
            rpt.Append("</tr>");
            rpt.Append("</table>");
        }
        else
        {
            rpt.Append("<table width='100%' cellspacing=0 border=0 >");
            rpt.Append("<tr>");
            rpt.Append("<td width='100%' style='height:70px;'></td>");
            rpt.Append("</tr>");
            rpt.Append("</table>");
        }
    }
    public void GetHearder_Detail(string type)
    {
        DataTable dt = thereq.GetRequisitionForReport(Session["CoCode"].ToString().Trim(), Session["YearCode"].ToString().Trim(), lblPatientreqno.Text); //ds.Tables[0];
        //DataTable SlipSession = (DataTable)Session["SlipSession"];
        DataTable SlipSession = thedia.GetExistTestDetails(Session["CoCode"].ToString().Trim(), Session["YearCode"].ToString().Trim(), lblPatientreqno.Text, "");
        DataTable testcostdetails = thedia.GetExistTestCostDetails(Session["CoCode"].ToString().Trim(), Session["YearCode"].ToString().Trim(), lblPatientreqno.Text);
        
        DataTable billDetails ;
        //if (type == "M")
        //{
            
        //}
        //else
        //{
        //    billDetails = thedia.GetInvoiceDetails(Session["CoCode"].ToString().Trim(), Session["YearCode"].ToString().Trim(), lblinvoiceNo.Text, "1");
        //}
        billDetails = thedia.GetBillDetails(Session["CoCode"].ToString().Trim(), Session["YearCode"].ToString().Trim(), lblreceitpNo.Text, "1");
        if (RadioButtonList1.SelectedValue == "Y")
        {
            rpt.Append("<table width='100%' cellspacing=0 border=0 style='height:30px;'>");
            rpt.Append("<tr cellpadding:'0'  >");
            rpt.Append("<td valign='bottom' rowspan='2' width='30%'><img src='../Images/logo.jpg'  style=''/></td>");
            rpt.Append("<td rowspan='2'  width='40%'  style='height:20px;font-family:Arial; font-size:medium; text-align:center'></td>");
            if (type == "M")
            {
                rpt.Append("<td valign='bottom' width='30%' style='text-align:right'>Receipt Date :" + billDetails.Rows[0]["BillDate"].ToString() + " " + billDetails.Rows[0]["BillTime"].ToString() + "</td>");
            }
            else
            {
                rpt.Append("<td valign='bottom' width='30%' style='text-align:right'>Invoice Date :" + billDetails.Rows[0]["BillDate"].ToString() + " " + billDetails.Rows[0]["BillTime"].ToString() + "</td>");
            }
            rpt.Append("</tr>");
            rpt.Append("<tr>");
            rpt.Append("<td style='font-family:Verdana;font-size:small; text-align:right;'> Print Date :" + DateTime.Now.ToString("dd/MM/yyyy HH:mm tt") + " </td>");
            rpt.Append("</tr>");
            rpt.Append("</table>");
        }
        else
        {
            rpt.Append("<table width='100%' cellspacing=0 border=0 style='height:30px;'>");
            if (type == "M")
            {
                rpt.Append("<td style='width:100%;font-family:Verdana;font-size:small; text-align:right;'> Receipt Date :" + billDetails.Rows[0]["BillDate"].ToString() + " " + billDetails.Rows[0]["BillTime"].ToString() + " </td>");
            }
            else
            {
                rpt.Append("<td style='width:100%;font-family:Verdana;font-size:small; text-align:right;'> Invoice Date :" + billDetails.Rows[0]["BillDate"].ToString() + " " + billDetails.Rows[0]["BillTime"].ToString() + " </td>");
            }
            rpt.Append("<tr>");
            rpt.Append("<td style='width:100%;font-family:Verdana;font-size:small; text-align:right;'> Print Date :" + DateTime.Now.ToString("dd/MM/yyyy HH:mm tt") + " </td>");
            rpt.Append("</tr>");
            rpt.Append("</table>");
        }

        if (SlipSession != null)
        {

            rpt.Append("<table width='100%' cellspacing=0 border=0 style='border-spacing: 0; padding: 0;font-family:verdana;'>");
            //rpt.Append("<tr style='height:20px'>");
            //rpt.Append("<td style='width:25%;font-family:Verdana;font-size:small; text-align:right;background-color:#9B9C8D'>&nbsp;</td>");
            //rpt.Append("<td colspan='2' style='width:50%;font-family:Verdana;font-size:small; text-align:right;background-color:#9B9C8D'>&nbsp;</td>");
            //rpt.Append("</tr'>");

            if (type == "M")
            {
                rpt.Append("<tr>");
                rpt.Append("<td style='width:100%;font-family:Verdana;font-size:small; text-align:Center;background-color:#9B9C8D'> <b><u>Money Receipt</u></b>  </td>");
                rpt.Append("</tr>");
            }
            else
            {
                rpt.Append("<tr>");
                rpt.Append("<td style='width:100%;font-family:Verdana;font-size:small; text-align:Center;background-color:#9B9C8D'> <b><u>Invoice Cum Money Receipt</u></b>  </td>");
                rpt.Append("</tr>");
            }

            rpt.Append("<tr>");
            rpt.Append("<td style='width:100%;font-family:Verdana;font-size:small; text-align:Center;background-color:#9B9C8D'> PATIENT DETAILS  </td>");
            rpt.Append("</tr>");
            rpt.Append("</table>");



            rpt.Append("<table width='100%' cellspacing=0 border=0 style='border-spacing: 0; padding: 0;font-family:verdana;font-size:small;'>");
            if (type == "M")
            {
                rpt.Append("<tr style=''>");
                rpt.Append("<td style='width: 15% ;font-family:Verdana; text-align:left;font-weight:bold;'>Receipt No :</td>");
                rpt.AppendFormat("<td style='width: 35%;font-family:Verdana; text-align:left'>{0}</td>", billDetails.Rows[0]["MoneyReceiptNo"]);
                rpt.Append("<td style='width: 15%;text-align:left;font-weight:bold;'>Patient Id :</td>");
                rpt.AppendFormat("<td style='width: 35%;font-family:Verdana; text-align:left'>{0}</td>", dt.Rows[0]["RegistrationNo"]);
                rpt.Append("</tr>");
            }
            else
            {
                rpt.Append("<tr style=''>");
                rpt.Append("<td style='width: 15% ;font-family:Verdana; text-align:left;font-weight:bold;'>Invoice No :</td>");
                rpt.AppendFormat("<td style='width: 35%;font-family:Verdana; text-align:left'>{0}</td>", billDetails.Rows[0]["ReceiptNo"]);
                rpt.Append("<td style='width: 15%;text-align:left;font-weight:bold;'>Patient Id :</td>");
                rpt.AppendFormat("<td style='width: 35%;font-family:Verdana; text-align:left'>{0}</td>", dt.Rows[0]["RegistrationNo"]);
                rpt.Append("</tr>");
            }

            rpt.Append("<tr style=''>");
            rpt.Append("<td style='font-family:Verdana;  text-align:left;font-weight:bold;'>Name & Age :</td>");
            rpt.AppendFormat("<td style='font-family:Verdana; text-align:left'>{0}</td>", dt.Rows[0]["PatientName"].ToString() + ", " + dt.Rows[0]["age"].ToString());
            rpt.Append("<td style='font-family:Verdana;  text-align:left;font-weight:bold;'>Contact No : </td>");
            rpt.AppendFormat("<td style='font-family:Verdana; text-align:left'>{0}</td>", dt.Rows[0]["Ph1"]);
            rpt.Append("</tr>");

            rpt.Append("<tr style=''>");
            rpt.Append("<td style='font-family:Verdana;  text-align:left;font-weight:bold;'>Address :</td>");
            rpt.AppendFormat("<td  colspan='3' style='font-family:Verdana; text-align:left'>{0}</td>", dt.Rows[0]["Address"]);
            rpt.Append("</tr>");

            if (dt.Rows[0]["UnderDoc"].ToString() != "")
            {
                rpt.Append("<tr style=''>");
                rpt.Append("<td style='font-family:Verdana;  text-align:left;font-weight:bold;'>Doctor Name :</td>");
                rpt.AppendFormat("<td colspan='3' style='font-family:Verdana; text-align:left'>{0}</td>", "Dr. " + dt.Rows[0]["UnderDocName"].ToString() + "( Reg No: " + dt.Rows[0]["DocRegNo"].ToString() + " )");
                rpt.Append("</tr>");
            }
            if (dt.Rows[0]["ReqType"].ToString() == "DIG")
            {
                rpt.Append("<tr style=''>");
                rpt.Append("<td style='font-family:Verdana;  text-align:left;font-weight:bold;'>Referred By:</td>");
                rpt.AppendFormat("<td colspan='3' style='font-family:Verdana; text-align:left'>{0}</td>", "Dr. " + dt.Rows[0]["ReferalName"].ToString());
                rpt.Append("</tr>");
            }
            if (dt.Rows[0]["ReqType"].ToString() == "INF")
            {
                rpt.Append("<tr style=''>");
                rpt.Append("<td style='font-family:Verdana;  text-align:left;font-weight:bold;'>Referred By:</td>");
                rpt.AppendFormat("<td colspan='3' style='font-family:Verdana; text-align:left'>{0}</td>", "Dr. " + dt.Rows[0]["ReferalName"].ToString());
                rpt.Append("</tr>");
            }
            rpt.Append("</table>");



            rpt.Append("<br/>");
            rpt.Append("<center>");



            rpt.Append("<table width='100%' cellspacing=0 border=0 style='border-spacing: 0; padding: 0;font-family:verdana;font-size:small;'>");
            rpt.Append("<tr>");
            rpt.Append("<td style='width: 70%;font-family:Verdana;  text-align:left;font-weight:bold;'>");
            rpt.Append("<table width='100%'  cellspacing=0 border=0 style='border:1px solid black;border-spacing: 0; padding: 0;font-family:verdana;font-size:small;'>");

            rpt.Append("<tr style=''>");
            rpt.Append("<td colspan='2' style='width: 8%;border-left: 1px solid black;border-bottom: 1px solid black;border-top: 1px solid black; font-family:Verdana;font-size:small; text-align:Center;background-color:#9B9C8D;font-weight:bold;'> SERVICE DETAILS  </td>");
            rpt.Append("</tr'>");

            rpt.Append("<tr style=''>");
            //rpt.Append("<td style='width:15%;border-bottom: 1px solid black;border-right: 1px solid black; font-family:Verdana;text-align:left;font-weight:bold;'>Department</td>");
            rpt.Append("<td style='width:70%;border-bottom: 1px solid black;border-right: 1px solid black; font-family:Verdana;  text-align:left;font-weight:bold;'>Service Name</td>");
            rpt.Append("<td style='width:30%;border-bottom: 1px solid black;font-family:Verdana; text-align:right;font-weight:bold;'>Charge</td>");
            double total = 0.0;
            int a = 0, b = 0, c = 0, d = 0;
            rpt.Append("</tr >");

            for (int i = 0; i < SlipSession.Rows.Count; i++)
            {
                string dep = thedia.GetExistTestDetailsDept(Session["CoCode"].ToString().Trim(), SlipSession.Rows[i]["Testid"].ToString());
                if (dep == "")
                {
                    if (SlipSession.Rows[i]["Testid"].ToString().Contains("TX"))
                        dep = "X-Ray";
                    else dep = "USG";
                }
                //rpt.AppendFormat("<td style='border-bottom: 1px solid black;border-right: 1px solid black; font-family:Verdana; text-align:left;font-weight:normal;'>{0}</td>", dep);
                rpt.AppendFormat("<td style='border-bottom: 1px solid black;border-right: 1px solid black; font-family:Verdana;text-align:left;font-weight:normal;'>{0}</td>", SlipSession.Rows[i]["TestName"].ToString());
                rpt.AppendFormat("<td style='border-bottom: 1px solid black;font-family:Verdana; text-align:right;font-weight:normal;'> {0}</td>", SlipSession.Rows[i]["cost"].ToString());
                rpt.Append("</tr >");
                total = total + Convert.ToDouble(SlipSession.Rows[i]["cost"]);
            }
            rpt.Append("</table>");
            rpt.Append("</td>");
            rpt.Append("<td valign='top' style='width: 30%;font-family:Verdana;  text-align:left;font-weight:bold;padding-left:20px;'>");
            rpt.Append("<table width='100%' cellpadding=0 cellspacing=0 >");
            rpt.Append("<tr style=''>");
            rpt.Append("<td colspan='2' style='width:100%; font-family:Verdana; text-align:center;font-weight:bold;'><u>Payment Calculation</u></td>");
            rpt.Append("</tr>");
            rpt.Append("<tr style=''>");
            rpt.Append("<td  style='width:60%; font-family:Verdana; text-align:left;font-weight:bold;'>Total Charges</td>");
            rpt.AppendFormat("<td style='width:40%; font-family:Verdana;text-align:right'>{0}</td>", total.ToString("F"));
            rpt.Append("</tr>");
            if (Convert.ToDecimal(billDetails.Rows[0]["DiscountAmt"]) > 0)
            {
                rpt.Append("<tr style=''>");
                rpt.Append("<td  style=' font-family:Verdana; text-align:left;font-weight:bold;'>Discount Amt</td>");
                rpt.AppendFormat("<td style='font-family:Verdana;text-align:right'>{0}</td>", billDetails.Rows[0]["DiscountAmt"].ToString());
                rpt.Append("</tr>");
            }
            if (type == "M")
            {
                if (Convert.ToDecimal(billDetails.Rows[0]["PrevBillAmt"]) > 0)
                {
                    rpt.Append("<tr style=''>");
                    rpt.Append("<td  style=' font-family:Verdana; text-align:left;font-weight:bold;'>Previous Paid Amt</td>");
                    rpt.AppendFormat("<td style='font-family:Verdana;text-align:right'>{0}</td>", billDetails.Rows[0]["PrevBillAmt"].ToString());
                    rpt.Append("</tr>");
                }
                rpt.Append("<tr style=''>");
                rpt.Append("<td  style=' font-family:Verdana; text-align:left;font-weight:bold;'>Paid Amt</td>");
                rpt.AppendFormat("<td style='font-family:Verdana;text-align:right'>{0}</td>", billDetails.Rows[0]["paid"].ToString());
                rpt.Append("</tr>");
            }
            else
            {
                Decimal totpaidamt = Convert.ToDecimal(billDetails.Rows[0]["paid"]) + Convert.ToDecimal(billDetails.Rows[0]["PrevBillAmt"]);
                rpt.Append("<tr style=''>");
                rpt.Append("<td  style=' font-family:Verdana; text-align:left;font-weight:bold;'>Paid Amt</td>");
                rpt.AppendFormat("<td style='font-family:Verdana;text-align:right'>{0}</td>", billDetails.Rows[0]["totpaidamt"].ToString());
                rpt.Append("</tr>");
            }

            Decimal dueamt = Convert.ToDecimal(total) - Convert.ToDecimal(billDetails.Rows[0]["paid"]) - Convert.ToDecimal(billDetails.Rows[0]["PrevBillAmt"]) - Convert.ToDecimal(billDetails.Rows[0]["DiscountAmt"]);
            rpt.Append("<tr>");
            rpt.Append("<td  style=' font-family:Verdana; text-align:left;font-weight:bold;'>Due Amt</td>");
            rpt.AppendFormat("<td style=' font-family:Verdana;text-align:right'>{0}</td>", dueamt.ToString("F"));
            //rpt.AppendFormat("<td style=' font-family:Verdana;text-align:right'>{0}</td>", billDetails.Rows[0]["DueAmt"].ToString());
            rpt.Append("</tr>");
            rpt.Append("</table>");
            rpt.Append("</td>");
            rpt.Append("</tr>");
            /*rpt.Append("</table>");

            rpt.Append("<table width='100%' cellpadding=0 cellspacing=0 >");*/


            rpt.Append("<tr style=''>");
            rpt.Append("<td colspan='2'></td>");
            rpt.Append("</tr>");

            rpt.Append("<tr style=''>");
            rpt.Append("<td></td>");
            rpt.Append("<td style='font-family:Verdana;  text-align:center'>________________________________</td>");
            rpt.Append("</tr>");

            rpt.Append("<tr>");
            rpt.Append("<td style='font-family:Verdana;  text-align:left'>In Words : " + billDetails.Rows[0]["num2word"].ToString() + "</td>");
            rpt.Append("<td style='font-family:Verdana;  text-align:center;font-weight:bold;'> Username </td>");
            rpt.Append("</tr>");
            rpt.Append("<tr>");
            rpt.Append("<td style='font-family:Verdana;  text-align:left'>Received with thanks </td>");
            rpt.Append("<td style='font-family:Verdana;  text-align:center;font-weight:bold;'> For " + Session["CoName"].ToString() + "</td>");
            rpt.Append("</tr>");


            rpt.Append("<tr style='height:50px'>");
            rpt.Append("<td valign='bottom' colspan='2' valign='bottom' style='width: 100%; font-family:Verdana;  text-align:left;font-size:8pt;color:gray;'><i># Incase you find any unintentional system generated discripency in the bill, kindly bring it to our notice for corrective action.</i></td>");
            rpt.Append("</tr>");

            rpt.Append("</table>");
            rpt.Append("</center>");
        }
        else
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "msg", "alert('No row to Display.. !');", true);
        }

        Session["SlipSession"] = null;
        ltrReport.Visible = true;

    }


    public void BillGetHearder_DetailInvoice(string type)
    {
        DataTable dt = thereq.GetRequisitionForReport(Session["CoCode"].ToString().Trim(), Session["YearCode"].ToString().Trim(), lblPatientreqno.Text); //ds.Tables[0];
        //DataTable SlipSession = (DataTable)Session["SlipSession"];
        DataTable SlipSession = thedia.GetExistTestDetails(Session["CoCode"].ToString().Trim(), Session["YearCode"].ToString().Trim(), lblPatientreqno.Text, "");
        DataTable testcostdetails = thedia.GetExistTestCostDetails(Session["CoCode"].ToString().Trim(), Session["YearCode"].ToString().Trim(), lblPatientreqno.Text);
        DataTable dtPaymentDetails = thedia.GetPaymentDetails(Session["CoCode"].ToString().Trim(), Session["YearCode"].ToString().Trim(), lblPatientreqno.Text);
        DataTable dtAdvanceDetls = thedia.GetAdvanceDetails(Session["CoCode"].ToString().Trim(), Session["YearCode"].ToString().Trim(), dtPaymentDetails.Rows[0]["Vchno"].ToString().Trim(), lblPatientreqno.Text);
        DataTable billDetails;

        string testtype = "";
        for (int n = 0; n < SlipSession.Rows.Count; n++)
        {
            if (SlipSession.Rows[n]["TestType"].ToString().Trim() == "PKG")
            {
                testtype = "PKG";
                break;
            }
        }

        billDetails = thedia.GetBillDetails(Session["CoCode"].ToString().Trim(), Session["YearCode"].ToString().Trim(), lblinvoiceNo.Text, "1");

        if (RadioButtonList1.SelectedValue == "Y")
        {
            rpt.Append("<table width='100%' cellspacing=0 border=0 style='height:25px;'>");
            rpt.Append("<tr cellpadding:'0'  >");
            rpt.Append("<td width='30%'><img src='../Images/logo.jpg'  style=''/></td>");
            rpt.Append("<td   width='40%'  style='height:20px;font-family:Arial; font-size:medium; text-align:center'></td>");
            rpt.Append("<td width='30%' style='text-align:right'>Print Date : " + DateTime.Now.ToString("dd/MM/yyyy HH:mm tt") + "</td>");
            rpt.Append("</tr>");
            rpt.Append("</table>");
        }
        else
        {
            rpt.Append("<table width='100%' cellspacing=0 border=0 style='height:25px;'>");
            rpt.Append("<tr>");
            rpt.Append("<td style='width:100%;font-family:Verdana;font-size:small; text-align:right;'> Invoice Date :" + billDetails.Rows[0]["BillDate"].ToString() + " " + billDetails.Rows[0]["BillTime"].ToString() + " </td>");
            rpt.Append("</tr>");
            rpt.Append("<tr>");
            rpt.Append("<td style='width:100%;font-family:Verdana;font-size:small; text-align:right;'> Print Date :" + DateTime.Now.ToString("dd/MM/yyyy HH:mm tt") + " </td>");
            rpt.Append("</tr>");
            rpt.Append("</table>");
        }

        if (SlipSession != null)
        {
            if (testtype == "PKG")
            {
                rpt.Append("<table width='100%' cellspacing=0 border=0 style='border-spacing: 0; padding: 0;font-family:verdana;'>");
                rpt.Append("<tr>");
                rpt.Append("<td style='width:100%;font-family:Verdana;font-size:small; text-align:Center;background-color:#9B9C8D'> <b><u>Invoice Cum Money Receipt</u></b>  </td>");
                rpt.Append("</tr>");
                rpt.Append("<tr>");
                rpt.Append("<td style='width:100%;font-family:Verdana;font-size:small; text-align:Center;background-color:#9B9C8D'> PATIENT DETAILS  </td>");
                rpt.Append("</tr>");
                rpt.Append("</table>");

                rpt.Append("<table width='100%' cellspacing=0 border=0 style='border-spacing: 0; padding: 0;font-family:verdana;font-size:small;'>");
                rpt.Append("<tr style=''>");
                rpt.Append("<td style='width: 15% ;font-family:Verdana; text-align:left;font-weight:bold;'>Invoice No :</td>");
                rpt.AppendFormat("<td style='width: 35%;font-family:Verdana; text-align:left'>{0}</td>", billDetails.Rows[0]["ReceiptNo"]);
                rpt.Append("<td style='width: 15%;text-align:left;font-weight:bold;'>Patient Id :</td>");
                rpt.AppendFormat("<td style='width: 35%;font-family:Verdana; text-align:left'>{0}</td>", dt.Rows[0]["RegistrationNo"]);
                rpt.Append("</tr>");

                rpt.Append("<tr style=''>");
                rpt.Append("<td style='font-family:Verdana;  text-align:left;font-weight:bold;'>Name & Age :</td>");
                rpt.AppendFormat("<td style='font-family:Verdana; text-align:left'>{0}</td>", dt.Rows[0]["PatientName"].ToString() + ", " + dt.Rows[0]["age"].ToString());
                rpt.Append("<td style='font-family:Verdana;  text-align:left;font-weight:bold;'>Contact No : </td>");
                rpt.AppendFormat("<td style='font-family:Verdana; text-align:left'>{0}</td>", dt.Rows[0]["Ph1"]);
                rpt.Append("</tr>");

                rpt.Append("<tr style=''>");
                rpt.Append("<td style='font-family:Verdana;  text-align:left;font-weight:bold;'>Address :</td>");
                rpt.AppendFormat("<td  colspan='3' style='font-family:Verdana; text-align:left'>{0}</td>", dt.Rows[0]["Address"]);
                rpt.Append("</tr>");

                if (dt.Rows[0]["UnderDoc"].ToString() != "")
                {
                    rpt.Append("<tr style=''>");
                    rpt.Append("<td style='font-family:Verdana;  text-align:left;font-weight:bold;'>Doctor Name :</td>");
                    rpt.AppendFormat("<td colspan='3' style='font-family:Verdana; text-align:left'>{0}</td>", "Dr. " + dt.Rows[0]["UnderDocName"].ToString() + "( Reg No: " + dt.Rows[0]["DocRegNo"].ToString() + " )");
                    rpt.Append("</tr>");
                }
                if (dt.Rows[0]["ReqType"].ToString() == "DIG")
                {
                    rpt.Append("<tr style=''>");
                    rpt.Append("<td style='font-family:Verdana;  text-align:left;font-weight:bold;'>Referred By:</td>");
                    rpt.AppendFormat("<td colspan='3' style='font-family:Verdana; text-align:left'>{0}</td>", "Dr. " + dt.Rows[0]["ReferalName"].ToString());
                    rpt.Append("</tr>");
                }
                if (dt.Rows[0]["ReqType"].ToString() == "INF")
                {
                    rpt.Append("<tr style=''>");
                    rpt.Append("<td style='font-family:Verdana;  text-align:left;font-weight:bold;'>Referred By:</td>");
                    rpt.AppendFormat("<td colspan='3' style='font-family:Verdana; text-align:left'>{0}</td>", "Dr. " + dt.Rows[0]["ReferalName"].ToString());
                    rpt.Append("</tr>");
                }
                rpt.Append("</table>");


                rpt.Append("<br/>");
                rpt.Append("<center>");



                rpt.Append("<table width='100%' cellspacing=0 border=0 style='border-spacing: 0; padding: 0;font-family:verdana;font-size:small;'>");
                rpt.Append("<tr>");
                rpt.Append("<td style='width: 70%;font-family:Verdana;  text-align:left;font-weight:bold;'>");
                rpt.Append("<table width='100%'  cellspacing=0 border=0 style='border:1px solid black;border-spacing: 0; padding: 0;font-family:verdana;font-size:small;'>");

                rpt.Append("<tr style=''>");
                rpt.Append("<td colspan='2' style='width: 8%;border-left: 1px solid black;border-bottom: 1px solid black;border-top: 1px solid black; font-family:Verdana;font-size:small; text-align:Center;background-color:#9B9C8D;font-weight:bold;'> SERVICE DETAILS  </td>");
                rpt.Append("</tr'>");

                rpt.Append("<tr style=''>");
                //rpt.Append("<td style='width:15%;border-bottom: 1px solid black;border-right: 1px solid black; font-family:Verdana;text-align:left;font-weight:bold;'>Department</td>");
                rpt.Append("<td style='width:70%;border-bottom: 1px solid black;border-right: 1px solid black; font-family:Verdana;  text-align:left;font-weight:bold;'>Service Name</td>");
                rpt.Append("<td style='width:30%;border-bottom: 1px solid black;font-family:Verdana; text-align:right;font-weight:bold;'>Charge</td>");
                double total = 0.0;
                int a = 0, b = 0, c = 0, d = 0;
                rpt.Append("</tr >");

                for (int i = 0; i < SlipSession.Rows.Count; i++)
                {
                    string dep = thedia.GetExistTestDetailsDept(Session["CoCode"].ToString().Trim(), SlipSession.Rows[i]["Testid"].ToString());
                    if (dep == "")
                    {
                        if (SlipSession.Rows[i]["Testid"].ToString().Contains("TX"))
                            dep = "X-Ray";
                        else dep = "USG";
                    }
                    rpt.Append("<tr>");
                    //rpt.AppendFormat("<td style='border-bottom: 1px solid black;border-right: 1px solid black; font-family:Verdana; text-align:left;font-weight:normal;'>{0}</td>", dep);
                    rpt.AppendFormat("<td style='border-bottom: 1px solid black;border-right: 1px solid black; font-family:Verdana;text-align:left;font-weight:normal;'>{0}</td>", SlipSession.Rows[i]["TestName"].ToString());
                    rpt.AppendFormat("<td style='border-bottom: 1px solid black;font-family:Verdana; text-align:right;font-weight:normal;'> {0}</td>", SlipSession.Rows[i]["cost"].ToString());
                    rpt.Append("</tr >");
                    total = total + Convert.ToDouble(SlipSession.Rows[i]["cost"]);
                }
                rpt.Append("</table>");
                rpt.Append("</td>");
                rpt.Append("<td valign='top' style='width: 30%;font-family:Verdana;  text-align:left;font-weight:bold;padding-left:20px;'>");
                rpt.Append("<table width='100%' cellpadding=0 cellspacing=0 >");
                rpt.Append("<tr style=''>");
                rpt.Append("<td colspan='3' style='width:100%; font-family:Verdana; text-align:center;font-weight:bold;'><u>Payment Calculation</u></td>");
                rpt.Append("</tr>");
                rpt.Append("<tr style=''>");
                rpt.Append("<td colspan='2' style='width:70%; font-family:Verdana; text-align:left;font-weight:bold;'>Total Charges</td>");
                rpt.AppendFormat("<td style='width:30%; font-family:Verdana;text-align:right'>{0}</td>", total.ToString("F"));
                rpt.Append("</tr>");
                if (Convert.ToDecimal(billDetails.Rows[0]["DiscountAmt"]) > 0)
                {
                    rpt.Append("<tr style=''>");
                    rpt.Append("<td colspan='2'  style=' font-family:Verdana; text-align:left;font-weight:bold;'>Discount Amt</td>");
                    rpt.AppendFormat("<td style='font-family:Verdana;text-align:right'>{0}</td>", billDetails.Rows[0]["DiscountAmt"].ToString());
                    rpt.Append("</tr>");
                }
                /*if (dtPaymentDetails.Rows.Count > 0)
                {
                    for (int j = 0; j < dtPaymentDetails.Rows.Count; j++)
                    {
                        rpt.Append("<tr style=''>");
                        rpt.AppendFormat("<td style='width:50%;font-family:Verdana;text-align:left;font-size:9pt;'>{0}</td>", dtPaymentDetails.Rows[j]["MoneyReceiptNo"].ToString());
                        rpt.AppendFormat("<td style='width:20%;font-family:Verdana;text-align:left;font-size:9pt;'>{0}</td>", "(" + dtPaymentDetails.Rows[j]["PayDate"].ToString() + ")");
                        rpt.AppendFormat("<td style='width:30%;font-family:Verdana;text-align:right;font-size:9pt;'>{0}</td>", dtPaymentDetails.Rows[j]["paidAmt"].ToString());
                        rpt.Append("</tr>");
                    }
                }*/
                if (dtAdvanceDetls.Rows.Count > 0)
                {
                    rpt.Append("<tr style=''>");
                    rpt.Append("<td colspan='2' style=' font-family:Verdana; text-align:left;font-weight:bold;'>Adjusted Advance</td>");
                    rpt.AppendFormat("<td style='font-family:Verdana;text-align:right'>{0}</td>", dtAdvanceDetls.Rows[0]["AdvAmt"].ToString());
                    rpt.Append("</tr>");
                }
                /*Decimal totpaidamt = Convert.ToDecimal(billDetails.Rows[0]["paid"]) + Convert.ToDecimal(billDetails.Rows[0]["PrevBillAmt"])+Convert.ToDecimal(dtAdvanceDetls.Rows[0]["AdvAmt"];
                rpt.Append("<tr style=''>");
                rpt.Append("<td colspan='2' style=' font-family:Verdana; text-align:left;font-weight:bold;'>Total Paid Amt</td>");
                rpt.AppendFormat("<td style='font-family:Verdana;text-align:right'>{0}</td>", billDetails.Rows[0]["totpaidamt"].ToString());
                rpt.Append("</tr>");*/


                Decimal dueamt = Convert.ToDecimal(total) - Convert.ToDecimal(billDetails.Rows[0]["paid"]) - Convert.ToDecimal(billDetails.Rows[0]["PrevBillAmt"]) - Convert.ToDecimal(billDetails.Rows[0]["DiscountAmt"])-Convert.ToDecimal(dtAdvanceDetls.Rows[0]["AdvAmt"]);
                rpt.Append("<tr>");
                rpt.Append("<td colspan='2' style=' font-family:Verdana; text-align:left;font-weight:bold;'>Due Amt</td>");
                rpt.AppendFormat("<td style=' font-family:Verdana;text-align:right'>{0}</td>", dueamt.ToString("F"));
                //rpt.AppendFormat("<td style=' font-family:Verdana;text-align:right'>{0}</td>", billDetails.Rows[0]["DueAmt"].ToString());
                rpt.Append("</tr>");
                rpt.Append("</table>");
                rpt.Append("</td>");
                rpt.Append("</tr>");

                rpt.Append("<tr style=''>");
                rpt.Append("<td colspan='2'></td>");
                rpt.Append("</tr>");

                rpt.Append("<tr style=''>");
                rpt.Append("<td></td>");
                rpt.Append("<td style='font-family:Verdana;  text-align:center'>________________________________</td>");
                rpt.Append("</tr>");

                rpt.Append("<tr>");
                rpt.Append("<td style='font-family:Verdana;  text-align:left'>In Words : " + billDetails.Rows[0]["num2word"].ToString() + "</td>");
                rpt.Append("<td style='font-family:Verdana;  text-align:center;font-weight:bold;'> Username </td>");
                rpt.Append("</tr>");
                rpt.Append("<tr>");
                rpt.Append("<td style='font-family:Verdana;  text-align:left'>Received with thanks </td>");
                rpt.Append("<td style='font-family:Verdana;  text-align:center;font-weight:bold;'> For " + Session["CoName"].ToString() + "</td>");
                rpt.Append("</tr>");


                rpt.Append("<tr style='height:50px'>");
                rpt.Append("<td valign='bottom' colspan='2' valign='bottom' style='width: 100%; font-family:Verdana;  text-align:left;font-size:8pt;color:gray;'><i># Incase you find any unintentional system generated discripency in the bill, kindly bring it to our notice for corrective action.</i></td>");
                rpt.Append("</tr>");

                rpt.Append("</table>");
                rpt.Append("</center>");
            }
            else
            {
                rpt.Append("<table width='100%' cellspacing=0 border=0 style='border-spacing: 0; padding: 0;font-family:verdana;'>");
                //rpt.Append("<tr style='height:20px'>");
                //rpt.Append("<td style='width:25%;font-family:Verdana;font-size:small; text-align:right;background-color:#9B9C8D'>&nbsp;</td>");
                //rpt.Append("<td colspan='2' style='width:50%;font-family:Verdana;font-size:small; text-align:right;background-color:#9B9C8D'>&nbsp;</td>");
                //rpt.Append("</tr'>");

                if (type == "M")
                {
                    rpt.Append("<tr>");
                    rpt.Append("<td style='width:100%;font-family:Verdana;font-size:small; text-align:Center;background-color:#9B9C8D'> <b><u>Money Receipt</u></b>  </td>");
                    rpt.Append("</tr>");
                }
                else
                {
                    rpt.Append("<tr>");
                    rpt.Append("<td style='width:100%;font-family:Verdana;font-size:small; text-align:Center;background-color:#9B9C8D'> <b><u>Invoice Cum Money Receipt</u></b>  </td>");
                    rpt.Append("</tr>");
                }

                rpt.Append("<tr>");
                rpt.Append("<td style='width:100%;font-family:Verdana;font-size:small; text-align:Center;background-color:#9B9C8D'> PATIENT DETAILS  </td>");
                rpt.Append("</tr>");
                rpt.Append("</table>");



                rpt.Append("<table width='100%' cellspacing=0 border=0 style='border-spacing: 0; padding: 0;font-family:verdana;font-size:small;'>");
                if (type == "M")
                {
                    rpt.Append("<tr style=''>");
                    rpt.Append("<td style='width: 15% ;font-family:Verdana; text-align:left;font-weight:bold;'>Receipt No :</td>");
                    rpt.AppendFormat("<td style='width: 35%;font-family:Verdana; text-align:left'>{0}</td>", billDetails.Rows[0]["MoneyReceiptNo"]);
                    rpt.Append("<td style='width: 15%;text-align:left;font-weight:bold;'>Patient Id :</td>");
                    rpt.AppendFormat("<td style='width: 35%;font-family:Verdana; text-align:left'>{0}</td>", dt.Rows[0]["RegistrationNo"]);
                    rpt.Append("</tr>");
                }
                else
                {
                    rpt.Append("<tr style=''>");
                    rpt.Append("<td style='width: 15% ;font-family:Verdana; text-align:left;font-weight:bold;'>Invoice No :</td>");
                    rpt.AppendFormat("<td style='width: 35%;font-family:Verdana; text-align:left'>{0}</td>", billDetails.Rows[0]["ReceiptNo"]);
                    rpt.Append("<td style='width: 15%;text-align:left;font-weight:bold;'>Patient Id :</td>");
                    rpt.AppendFormat("<td style='width: 35%;font-family:Verdana; text-align:left'>{0}</td>", dt.Rows[0]["RegistrationNo"]);
                    rpt.Append("</tr>");
                }

                rpt.Append("<tr style=''>");
                rpt.Append("<td style='font-family:Verdana;  text-align:left;font-weight:bold;'>Name & Age :</td>");
                rpt.AppendFormat("<td style='font-family:Verdana; text-align:left'>{0}</td>", dt.Rows[0]["PatientName"].ToString() + ", " + dt.Rows[0]["age"].ToString());
                rpt.Append("<td style='font-family:Verdana;  text-align:left;font-weight:bold;'>Contact No : </td>");
                rpt.AppendFormat("<td style='font-family:Verdana; text-align:left'>{0}</td>", dt.Rows[0]["Ph1"]);
                rpt.Append("</tr>");

                rpt.Append("<tr style=''>");
                rpt.Append("<td style='font-family:Verdana;  text-align:left;font-weight:bold;'>Address :</td>");
                rpt.AppendFormat("<td  colspan='3' style='font-family:Verdana; text-align:left'>{0}</td>", dt.Rows[0]["Address"]);
                rpt.Append("</tr>");

                if (dt.Rows[0]["UnderDoc"].ToString() != "")
                {
                    rpt.Append("<tr style=''>");
                    rpt.Append("<td style='font-family:Verdana;  text-align:left;font-weight:bold;'>Doctor Name :</td>");
                    rpt.AppendFormat("<td colspan='3' style='font-family:Verdana; text-align:left'>{0}</td>", "Dr. " + dt.Rows[0]["UnderDocName"].ToString() + "( Reg No: " + dt.Rows[0]["DocRegNo"].ToString() + " )");
                    rpt.Append("</tr>");
                }
                if (dt.Rows[0]["ReqType"].ToString() == "DIG")
                {
                    rpt.Append("<tr style=''>");
                    rpt.Append("<td style='font-family:Verdana;  text-align:left;font-weight:bold;'>Referred By:</td>");
                    rpt.AppendFormat("<td colspan='3' style='font-family:Verdana; text-align:left'>{0}</td>", "Dr. " + dt.Rows[0]["ReferalName"].ToString());
                    rpt.Append("</tr>");
                }
                if (dt.Rows[0]["ReqType"].ToString() == "INF")
                {
                    rpt.Append("<tr style=''>");
                    rpt.Append("<td style='font-family:Verdana;  text-align:left;font-weight:bold;'>Referred By:</td>");
                    rpt.AppendFormat("<td colspan='3' style='font-family:Verdana; text-align:left'>{0}</td>", "Dr. " + dt.Rows[0]["ReferalName"].ToString());
                    rpt.Append("</tr>");
                }
                rpt.Append("</table>");



                rpt.Append("<br/>");
                rpt.Append("<center>");



                rpt.Append("<table width='100%' cellspacing=0 border=0 style='border-spacing: 0; padding: 0;font-family:verdana;font-size:small;'>");
                rpt.Append("<tr>");
                rpt.Append("<td style='width: 70%;font-family:Verdana;  text-align:left;font-weight:bold;'>");
                rpt.Append("<table width='100%'  cellspacing=0 border=0 style='border:1px solid black;border-spacing: 0; padding: 0;font-family:verdana;font-size:small;'>");

                rpt.Append("<tr style=''>");
                rpt.Append("<td colspan='2' style='width: 8%;border-left: 1px solid black;border-bottom: 1px solid black;border-top: 1px solid black; font-family:Verdana;font-size:small; text-align:Center;background-color:#9B9C8D;font-weight:bold;'> SERVICE DETAILS  </td>");
                rpt.Append("</tr'>");

                rpt.Append("<tr style=''>");
                //rpt.Append("<td style='width:15%;border-bottom: 1px solid black;border-right: 1px solid black; font-family:Verdana;text-align:left;font-weight:bold;'>Department</td>");
                rpt.Append("<td style='width:70%;border-bottom: 1px solid black;border-right: 1px solid black; font-family:Verdana;  text-align:left;font-weight:bold;'>Service Name</td>");
                rpt.Append("<td style='width:30%;border-bottom: 1px solid black;font-family:Verdana; text-align:right;font-weight:bold;'>Charge</td>");
                double total = 0.0;
                int a = 0, b = 0, c = 0, d = 0;
                rpt.Append("</tr >");

                for (int i = 0; i < SlipSession.Rows.Count; i++)
                {
                    string dep = thedia.GetExistTestDetailsDept(Session["CoCode"].ToString().Trim(), SlipSession.Rows[i]["Testid"].ToString());
                    if (dep == "")
                    {
                        if (SlipSession.Rows[i]["Testid"].ToString().Contains("TX"))
                            dep = "X-Ray";
                        else dep = "USG";
                    }
                    //rpt.AppendFormat("<td style='border-bottom: 1px solid black;border-right: 1px solid black; font-family:Verdana; text-align:left;font-weight:normal;'>{0}</td>", dep);
                    rpt.AppendFormat("<td style='border-bottom: 1px solid black;border-right: 1px solid black; font-family:Verdana;text-align:left;font-weight:normal;'>{0}</td>", SlipSession.Rows[i]["TestName"].ToString());
                    rpt.AppendFormat("<td style='border-bottom: 1px solid black;font-family:Verdana; text-align:right;font-weight:normal;'> {0}</td>", SlipSession.Rows[i]["cost"].ToString());
                    rpt.Append("</tr >");
                    total = total + Convert.ToDouble(SlipSession.Rows[i]["cost"]);
                }
                rpt.Append("</table>");
                rpt.Append("</td>");
                rpt.Append("<td valign='top' style='width: 30%;font-family:Verdana;  text-align:left;font-weight:bold;padding-left:20px;'>");
                rpt.Append("<table width='100%' cellpadding=0 cellspacing=0 >");
                rpt.Append("<tr style=''>");
                rpt.Append("<td colspan='2' style='width:100%; font-family:Verdana; text-align:center;font-weight:bold;'><u>Payment Calculation</u></td>");
                rpt.Append("</tr>");
                rpt.Append("<tr style=''>");
                rpt.Append("<td  style='width:60%; font-family:Verdana; text-align:left;font-weight:bold;'>Total Charges</td>");
                rpt.AppendFormat("<td style='width:40%; font-family:Verdana;text-align:right'>{0}</td>", total.ToString("F"));
                rpt.Append("</tr>");
                if (Convert.ToDecimal(billDetails.Rows[0]["DiscountAmt"]) > 0)
                {
                    rpt.Append("<tr style=''>");
                    rpt.Append("<td  style=' font-family:Verdana; text-align:left;font-weight:bold;'>Discount Amt</td>");
                    rpt.AppendFormat("<td style='font-family:Verdana;text-align:right'>{0}</td>", billDetails.Rows[0]["DiscountAmt"].ToString());
                    rpt.Append("</tr>");
                }
                if (dtAdvanceDetls.Rows.Count > 0)
                {
                    rpt.Append("<tr style=''>");
                    rpt.Append("<td style=' font-family:Verdana; text-align:left;font-weight:bold;'>Adjusted Advance</td>");
                    rpt.AppendFormat("<td style='font-family:Verdana;text-align:right'>{0}</td>", dtAdvanceDetls.Rows[0]["AdvAmt"].ToString());
                    rpt.Append("</tr>");
                }
                if (type == "M")
                {
                    if (Convert.ToDecimal(billDetails.Rows[0]["PrevBillAmt"]) > 0)
                    {
                        rpt.Append("<tr style=''>");
                        rpt.Append("<td  style=' font-family:Verdana; text-align:left;font-weight:bold;'>Previous Paid Amt</td>");
                        rpt.AppendFormat("<td style='font-family:Verdana;text-align:right'>{0}</td>", billDetails.Rows[0]["PrevBillAmt"].ToString());
                        rpt.Append("</tr>");
                    }
                    rpt.Append("<tr style=''>");
                    rpt.Append("<td  style=' font-family:Verdana; text-align:left;font-weight:bold;'>Paid Amt</td>");
                    rpt.AppendFormat("<td style='font-family:Verdana;text-align:right'>{0}</td>", billDetails.Rows[0]["paid"].ToString());
                    rpt.Append("</tr>");
                }
                else
                {
                    
                    Decimal totpaidamt = Convert.ToDecimal(billDetails.Rows[0]["paid"]) + Convert.ToDecimal(billDetails.Rows[0]["PrevBillAmt"]) + Convert.ToDecimal(dtAdvanceDetls.Rows[0]["AdvAmt"]);
                    rpt.Append("<tr style=''>");
                    rpt.Append("<td  style=' font-family:Verdana; text-align:left;font-weight:bold;'>Paid Amt</td>");
                    rpt.AppendFormat("<td style='font-family:Verdana;text-align:right'>{0}</td>", billDetails.Rows[0]["totpaidamt"].ToString());
                    rpt.Append("</tr>");
                }

                Decimal dueamt = Convert.ToDecimal(total) - Convert.ToDecimal(billDetails.Rows[0]["paid"]) - Convert.ToDecimal(billDetails.Rows[0]["PrevBillAmt"]) - Convert.ToDecimal(billDetails.Rows[0]["DiscountAmt"]) - Convert.ToDecimal(dtAdvanceDetls.Rows[0]["AdvAmt"]);
                rpt.Append("<tr>");
                rpt.Append("<td  style=' font-family:Verdana; text-align:left;font-weight:bold;'>Due Amt</td>");
                rpt.AppendFormat("<td style=' font-family:Verdana;text-align:right'>{0}</td>", dueamt.ToString("F"));
                //rpt.AppendFormat("<td style=' font-family:Verdana;text-align:right'>{0}</td>", billDetails.Rows[0]["DueAmt"].ToString());
                rpt.Append("</tr>");
                rpt.Append("</table>");
                rpt.Append("</td>");
                rpt.Append("</tr>");
                /*rpt.Append("</table>");

                rpt.Append("<table width='100%' cellpadding=0 cellspacing=0 >");*/


                rpt.Append("<tr style=''>");
                rpt.Append("<td colspan='2'></td>");
                rpt.Append("</tr>");

                rpt.Append("<tr style=''>");
                rpt.Append("<td></td>");
                rpt.Append("<td style='font-family:Verdana;  text-align:center'>________________________________</td>");
                rpt.Append("</tr>");

                rpt.Append("<tr>");
                rpt.Append("<td style='font-family:Verdana;  text-align:left'>In Words : " + billDetails.Rows[0]["num2word"].ToString() + "</td>");
                rpt.Append("<td style='font-family:Verdana;  text-align:center;font-weight:bold;'> Username </td>");
                rpt.Append("</tr>");
                rpt.Append("<tr>");
                rpt.Append("<td style='font-family:Verdana;  text-align:left'>Received with thanks </td>");
                rpt.Append("<td style='font-family:Verdana;  text-align:center;font-weight:bold;'> For " + Session["CoName"].ToString() + "</td>");
                rpt.Append("</tr>");


                rpt.Append("<tr style='height:50px'>");
                rpt.Append("<td valign='bottom' colspan='2' valign='bottom' style='width: 100%; font-family:Verdana;  text-align:left;font-size:8pt;color:gray;'><i># Incase you find any unintentional system generated discripency in the bill, kindly bring it to our notice for corrective action.</i></td>");
                rpt.Append("</tr>");

                rpt.Append("</table>");
                rpt.Append("</center>");
            }
        }
        else
        {

        }
    }
    protected void Button6_Click(object sender, EventArgs e)
    {
        ltrReport.Visible = false;
        Button7.Visible = false;
        Button6.Visible = false;
    }
    protected void ddltype_SelectedIndexChanged(object sender, EventArgs e)
    {
        GridFill();
    }
    protected void ButtonSrch_Click(object sender, EventArgs e)
    {
        GridFill();
    }
    
    protected void LinkButton10_Click(object sender, EventArgs e)
    {
        advbillDiv.Visible = true;
    }
    protected void chkselect_CheckedChanged(object sender, EventArgs e)
    {
        getTotal();
    }

    private void getTotal()
    {
        double total = 0.00;
        string receiptNos="";
        string exceedflag = "0";
        //double olddisc = Convert.ToDouble(lbldiscountamt.Text);
        double totbill = Convert.ToDouble(lblTotBillAmt.Text);
        double curdisc = Convert.ToDouble(txtdiscountamt.Text);//- Convert.ToDouble(lbldiscountamt.Text);
        double paidamt = Convert.ToDouble(lblPaidBillAmt.Text);
        double dueamt = Convert.ToDouble(lblDueBillAmtOrg.Text);
        double balanceAmt = 0.00;
        double adjbalance = 0.00;
        foreach (GridViewRow row in GridView3.Rows)
        {
            Label lblrcptNo = (Label)row.FindControl("lblrcptNo");
            
            Label lblAmt = (Label)row.FindControl("lblAmt");
            Label lblbalAmt = (Label)row.FindControl("lblbalAmt");
            CheckBox chkselect = (CheckBox)row.FindControl("chkselect");
            if (chkselect.Checked == true)
            {
                if (exceedflag == "1")
                {
                    chkselect.Checked = false;
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "msg", "alert('Bill Amount exceeded.. !');", true);
                    
                }
                else
                {
                    receiptNos = receiptNos + lblrcptNo.Text + ",";
                    if (total + Convert.ToDouble(lblAmt.Text) <= (totbill-paidamt))
                    {
                        total = total + Convert.ToDouble(lblAmt.Text);
                        lblbalAmt.Text = 0.ToString("F");
                    }
                    else
                    {
                        adjbalance = (totbill - (totbill - paidamt));
                        total = total + adjbalance;
                        balanceAmt = Convert.ToDouble(lblAmt.Text) - adjbalance;
                        lblbalAmt.Text = balanceAmt.ToString("F");
                        
                    }
                    if (total == (totbill - paidamt))
                    {
                        exceedflag = "1";
                    }
                    txtAdvBillNo.Text = receiptNos;
                    txtAdvBillAmt.Text = total.ToString();
                }
            }
        }
        //+olddisc
        dueamt = (totbill - paidamt) - (total + curdisc);
        lblAdvAdjustedAmt.Text = total.ToString();
        lblDueBillAmt.Text = dueamt.ToString();
        txtPayableAmt.Text = dueamt.ToString();
    }

    protected void txtdiscountamt_TextChanged(object sender, EventArgs e)
    {
        decimal totbill = Convert.ToDecimal(lblTotBillAmt.Text);
        //decimal olddisc = Convert.ToDecimal(lbldiscountamt.Text);
        decimal curdisc = Convert.ToDecimal(txtdiscountamt.Text);// -Convert.ToDecimal(lbldiscountamt.Text);
        decimal paidamt = Convert.ToDecimal(lblPaidBillAmt.Text);
        decimal adjustamt = Convert.ToDecimal(lblAdvAdjustedAmt.Text);
        decimal dueamt = Convert.ToDecimal(lblDueBillAmtOrg.Text);
        decimal payableamt = Convert.ToDecimal(txtPayableAmt.Text);
        decimal curpayableamt = totbill - (curdisc + paidamt + adjustamt);//olddisc +
        lblDueBillAmt.Text = curpayableamt.ToString();
        txtPayableAmt.Text = curpayableamt.ToString();
    }
}