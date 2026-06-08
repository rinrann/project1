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

public partial class Pathology_SampleCollection : System.Web.UI.Page
{
    Main Objmain = new Main(ConfigurationSettings.AppSettings["ConnectionString"].ToString());
    PH_PatientRequisition thereq = new PH_PatientRequisition(ConfigurationSettings.AppSettings["ConnectionString"].ToString());
    PatientAdmission thepd = new PatientAdmission(ConfigurationSettings.AppSettings["ConnectionString"].ToString());
    PH_TestPopupMultiple thedia = new PH_TestPopupMultiple(ConfigurationSettings.AppSettings["ConnectionString"].ToString());
    Discharge thedischarge = new Discharge(ConfigurationSettings.AppSettings["ConnectionString"].ToString());
    PH_SampleCollection ObjSample = new PH_SampleCollection(ConfigurationSettings.AppSettings["ConnectionString"].ToString());
    static string userId1 = "";
    String Date = DateTime.Now.ToString("dd/MM/yyyy");
    System.Text.StringBuilder rpt = new System.Text.StringBuilder();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["userName"] == null)
        {
            Response.Redirect("../LoginPage.aspx");
        }
        if (Objmain.checkaccss(Session["CoCode"].ToString(), Session["UserRoleID"].ToString().Trim(), "SAMPLE COLLECTION", checkAccessType.ViewAction) == false)
        {
            Response.Redirect("../AccessDenied.aspx");
        }

        if (Objmain.checkaccss(Session["CoCode"].ToString(), Session["UserRoleID"].ToString().Trim(), "SAMPLE COLLECTION", checkAccessType.InsertAction) == false)
        {
            btnSave.Enabled = false;
        }
        ScriptManager.RegisterStartupScript(this, this.GetType(), "msg", "GetDatetime();", true);
        Page.Title = "Sample Collection";

        if (!IsPostBack)
        {
            Tab1Func();
            GridFill();
            Button6.Visible = false;
            Button7.Visible = false;
            DataTable dtSample = ObjSample.getSampleList(Session["CoCode"].ToString().Trim());
            ddlSample.DataSource = dtSample;
            ddlSample.DataTextField = "SName";
            ddlSample.DataValueField = "SCode";
            ddlSample.DataBind();


            DataTable dtAgency = ObjSample.getAgencyList(Session["CoCode"].ToString().Trim(), Session["YearCode"].ToString().Trim());
            ddlAgency.DataSource = dtAgency;
            ddlAgency.DataTextField = "SlName";
            ddlAgency.DataValueField = "SlCode";
            ddlAgency.DataBind();

            cncldiv1.Visible = false;
            cncldiv2.Visible = false;
            btnUpdate.Visible = false;
            btnSave.Visible = true;
        }
    }

    public void GridFill()
    {
        GridView1.DataSource = ObjSample.GridFill(Session["CoCode"].ToString().Trim(), Session["YearCode"].ToString().Trim());
        GridView1.DataBind();
    }


    protected void GridView1_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        GridView1.PageIndex = e.NewPageIndex;
        GridFill();
    }

    protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "Select")
        {
            int index = Convert.ToInt32(e.CommandArgument);
            Label lblSrlno = (Label)GridView1.Rows[index].FindControl("lblSrlno");


            DataTable dt = ObjSample.getSampleCollectionDetls(Session["CoCode"].ToString().Trim(), Session["YearCode"].ToString().Trim(),lblSrlno.Text.Trim());
            if (dt.Rows.Count > 0)
            {
                txtreg.Text = dt.Rows[0]["RegistrationNo"].ToString().Trim();

                DataTable dtreq = ObjSample.GetPatientRequisitionList(Session["CoCode"].ToString().Trim(), txtreg.Text.Trim());
                ddlReqNo.DataSource = dtreq;
                ddlReqNo.DataTextField = "RequisitionNo";
                ddlReqNo.DataValueField = "RequisitionNo";
                ddlReqNo.DataBind();

                hdlSrlNo.Value = dt.Rows[0]["SrlNo"].ToString().Trim();
                ddlReqNo.SelectedValue = dt.Rows[0]["RequisitionNo"].ToString().Trim();
                ddlSample.SelectedValue = dt.Rows[0]["SampleCode"].ToString().Trim();
                txtUnit.Text = dt.Rows[0]["Unit"].ToString().Trim();
                ddlAgency.SelectedValue = dt.Rows[0]["AgencyCode"].ToString().Trim();
                txtCollector.Text = dt.Rows[0]["CollectorName"].ToString().Trim();
                txtdate.Text = dt.Rows[0]["CollectDate"].ToString().Trim();
                txtdeldate.Text = dt.Rows[0]["DeliveryDate"].ToString().Trim();
                txtAgencyAmt.Text = dt.Rows[0]["AgencyBillAmt"].ToString().Trim();
                txtPatientAmt.Text = dt.Rows[0]["PatientBillAmt"].ToString().Trim();

                DataTable dt1 = thedischarge.PatientDetailsForRequisition(txtreg.Text.Trim(), Session["CoCode"].ToString().Trim());
                txtname.Text = dt1.Rows[0]["PName"].ToString();
                txtunderdoc.Text = dt1.Rows[0]["doc_name"].ToString();
                txtreferal.Text = dt1.Rows[0]["doc_name"].ToString();
                txtage.Text = dt1.Rows[0]["Age"].ToString();
                txtaddress.Text = dt1.Rows[0]["Address"].ToString();
                txtaddress2.Text = dt1.Rows[0]["Address2"].ToString();
                //string[] ph1 = dt1.Rows[0]["PhNo1"].ToString().Split(' ');
                //string[] ph2 = dt1.Rows[0]["PhNo2"].ToString().Split(' ');
                txtph1.Text = dt1.Rows[0]["PhNo1"].ToString();
                txtph1.Text = dt1.Rows[0]["PhNo2"].ToString();
                TextBox2.Text = dt1.Rows[0]["Address2"].ToString();


                ddlPaymentMode.SelectedValue = dt.Rows[0]["PaymentMode"].ToString();
                txtBankName.Text = dt.Rows[0]["Bank_CardHolderName"].ToString();
                txtChequeNo.Text = dt.Rows[0]["Chq_CardNo"].ToString();
                txtchqdt.Text = dt.Rows[0]["ChqDt_CardExpDt"].ToString();

                if (dt.Rows[0]["CancelFlag"].ToString().Trim() == "0")
                {
                    chkCancel.Checked = false;
                }
                else
                {
                    chkCancel.Checked = true;
                }
                txtcancelReason.Text = dt.Rows[0]["CancelReason"].ToString().Trim();

                if (ddlPaymentMode.SelectedValue == "B")
                {
                    lblchqdt.InnerText = "Cheque Date :";
                    lblchqno.InnerText = "Cheque No :";
                    lblbankNm.InnerText = "Bank Name :";
                    divchqdt.Visible = true;
                    divchqno.Visible = true;
                    divBank.Visible = true;
                }
                else if (ddlPaymentMode.SelectedValue == "R")
                {
                    lblchqdt.InnerText = "Expire Date :";
                    lblchqno.InnerText = "Card No :";
                    lblbankNm.InnerText = "Card Holder Name :";
                    divchqdt.Visible = true;
                    divchqno.Visible = true;
                    divBank.Visible = true;
                }
                else
                {
                    divchqdt.Visible = false;
                    divchqno.Visible = false;
                    divBank.Visible = false;
                }
                hdnvchno.Value = dt.Rows[0]["VchNo"].ToString();
                ddlPaymentMode.Enabled = false;

                btnFetch.Visible = false;
                Button4.Visible = false;
                btnUpdate.Visible = true;
                btnSave.Visible = false;

                cncldiv1.Visible = true;
                cncldiv2.Visible = true;
                Tab1Func();
            }
        }
    }

    public void Tab1Func()
    {
        Tab1.CssClass = "Clicked";
        Tab2.CssClass = "Initial";
        MainView.ActiveViewIndex = 0;
        divchqdt.Visible = false;
        divchqno.Visible = false;
        divBank.Visible = false;
    }
    protected void btnSave_Click(object sender, EventArgs e)
    {
        string testdate = "";
        string DeliveryDate = "";
        string regno = Request.Form[txtreg.UniqueID];
        string compcode = Session["CoCode"].ToString().Trim();
        string yearcode = Session["YearCode"].ToString().Trim();
        System.Globalization.DateTimeFormatInfo dtf = new DateTimeFormatInfo();

        string chqdt = "";
        if (txtchqdt.Text != "")
        {
            string[] cdt = txtchqdt.Text.Split('/');
            chqdt = cdt[2] + "-" + cdt[1] + "-" + cdt[0];
        }

        if (txtreg.Text == "")
        {
            lblError.Text="Please Select Registration No !";
            lblError.ForeColor = Color.Red;
        }
        else if (ddlReqNo.SelectedValue == "")
        {
            
            lblError.Text = "Please Select Requisition No!";
            lblError.ForeColor = Color.Red;
        }
        else if (ddlSample.SelectedValue == "")
        {
            lblError.Text = "Please Select Sample!";
            lblError.ForeColor = Color.Red;
        }
        else if (txtUnit.Text == "")
        {
            lblError.Text = "Unit collected cannot be blank!";
            lblError.ForeColor = Color.Red;
        }
        else if (ddlAgency.SelectedValue == "")
        {
            lblError.Text = "Please Select Agency!";
            lblError.ForeColor = Color.Red;
        }
        else if (txtdate.Text == "" || txtdate.Text == "__/__/____")
        {
            lblError.Text = "Please enter sample collect date!";
            lblError.ForeColor = Color.Red;
        }
        else if (txtdeldate.Text == "" || txtdeldate.Text == "__/__/____")
        {
            lblError.Text = "Please enter delivery date!";
            lblError.ForeColor = Color.Red;
        }
        else
        {
            if (txtAgencyAmt.Text == "")
            {
                txtAgencyAmt.Text = "0.00";
            }
            if (txtPatientAmt.Text == "")
            {
                txtPatientAmt.Text = "0.00";
            }


            //testdate = DateTime.ParseExact(txtdate.Text.Trim(), "dd/MM/yyyy", dtf).ToString("yyyy-MM-dd");
            //DeliveryDate = DateTime.ParseExact(txtdeldate.Text.Trim(), "dd/MM/yyyy", dtf).ToString("yyyy-MM-dd");
            if (ObjSample.InsertSampleCollection(compcode, yearcode, txtreg.Text.Trim(), ddlReqNo.SelectedValue.Trim(), ddlSample.SelectedValue.Trim(), txtUnit.Text.Trim(), ddlAgency.SelectedValue.Trim(), txtdate.Text.Trim(), txtdeldate.Text.Trim(), txtCollector.Text.Trim(), txtAgencyAmt.Text, txtPatientAmt.Text, Session["userName"].ToString(), ddlPaymentMode.SelectedValue.Trim(), txtBankName.Text.Trim(), txtChequeNo.Text.Trim(), chqdt))
            {
                lblError.Text = "Record Successfully Saved!";
                lblError.ForeColor = Color.Green;
            }
            else
            {
                lblError.Text = "Error Occured during saving!";
                lblError.ForeColor = Color.Red;
            }
        }
    }
    protected void btnCancel_Click(object sender, EventArgs e)
    {
        ResetAllFields();
    }

    public void ResetAllFields()
    {
        txtreg.Text = "";
        txtname.Text = "";
        txtunderdoc.Text = "";
        txtreferal.Text = "";
        txtage.Text = "";
        txtaddress.Text = "";
        txtaddress2.Text = "";
        txtph1.Text = "";
        txtph2.Text = "";
        TextBox2.Text = "";
        ddlReqNo.ClearSelection();
        txtUnit.Text = "";
        txtCollector.Text = "";
        txtdate.Text = "";
        txtdeldate.Text = "";
        txtAgencyAmt.Text = "0.00";
        txtPatientAmt.Text = "0.00";
        btnFetch.Visible = true;
        Button4.Visible = true;
        btnUpdate.Visible = false;
        btnSave.Visible = true;
        cncldiv1.Visible = false;
        cncldiv2.Visible = false;
        
    }

    protected void btnFetch_Click(object sender, EventArgs e)
    {
        Fill();
    }

    private void Fill()
    {
        string regno = Request.Form[txtreg.UniqueID];
        DataTable CheckReq = thereq.GetRequisitionNo(Session["CoCode"].ToString().Trim(), Session["YearCode"].ToString().Trim(), regno);

        DataTable dtdoc = thereq.GetDocName(Session["CoCode"].ToString().Trim(), regno);
        DataTable dt = thedischarge.PatientDetailsForRequisition(regno, Session["CoCode"].ToString().Trim());
        txtreg.Text = regno;
        txtname.Text = dt.Rows[0]["PName"].ToString();
        txtunderdoc.Text = dt.Rows[0]["doc_name"].ToString();
        txtreferal.Text = dt.Rows[0]["doc_name"].ToString();
        txtage.Text = dt.Rows[0]["Age"].ToString();
        txtaddress.Text = dt.Rows[0]["Address"].ToString();
        txtaddress2.Text = dt.Rows[0]["Address2"].ToString();
        //string[] ph1 = dt.Rows[0]["PhNo1"].ToString().Split(' ');
        //string[] ph2 = dt.Rows[0]["PhNo2"].ToString().Split(' ');
        txtph1.Text = dt.Rows[0]["PhNo1"].ToString();
        txtph1.Text = dt.Rows[0]["PhNo2"].ToString();
        TextBox2.Text = dt.Rows[0]["Address2"].ToString();

        DataTable dtreq = ObjSample.GetPatientRequisitionList(Session["CoCode"].ToString().Trim(), regno);
        ddlReqNo.DataSource = dtreq;
        ddlReqNo.DataTextField = "RequisitionNo";
        ddlReqNo.DataValueField = "RequisitionNo";
        ddlReqNo.DataBind();

        

    }

    protected void Tab1_Click(object sender, EventArgs e)
    {
        Tab1Func();
    }
    protected void Tab2_Click(object sender, EventArgs e)
    {
        Tab1.CssClass = "Initial";
        Tab2.CssClass = "Clicked";
        MainView.ActiveViewIndex = 1;
    }
    protected void btnUpdate_Click(object sender, EventArgs e)
    {
        string compcode = Session["CoCode"].ToString().Trim();
        string yearcode = Session["YearCode"].ToString().Trim();
        System.Globalization.DateTimeFormatInfo dtf = new DateTimeFormatInfo();
        string chqdt = "";
        if (txtchqdt.Text != "")
        {
            string[] cdt = txtchqdt.Text.Split('/');
            chqdt = cdt[2] + "-" + cdt[1] + "-" + cdt[0];
        }

        if (txtreg.Text == "")
        {
            lblError.Text = "Please Select Registration No !";
            lblError.ForeColor = Color.Red;
        }
        else if (ddlReqNo.SelectedValue == "")
        {

            lblError.Text = "Please Select Requisition No!";
            lblError.ForeColor = Color.Red;
        }
        else if (ddlSample.SelectedValue == "")
        {
            lblError.Text = "Please Select Sample!";
            lblError.ForeColor = Color.Red;
        }
        else if (txtUnit.Text == "")
        {
            lblError.Text = "Unit collected cannot be blank!";
            lblError.ForeColor = Color.Red;
        }
        else if (ddlAgency.SelectedValue == "")
        {
            lblError.Text = "Please Select Agency!";
            lblError.ForeColor = Color.Red;
        }
        else if (txtdate.Text == "" || txtdate.Text == "__/__/____")
        {
            lblError.Text = "Please enter sample collect date!";
            lblError.ForeColor = Color.Red;
        }
        else if (txtdeldate.Text == "" || txtdeldate.Text == "__/__/____")
        {
            lblError.Text = "Please enter delivery date!";
            lblError.ForeColor = Color.Red;
        }
        else
        {
            if (txtAgencyAmt.Text == "")
            {
                txtAgencyAmt.Text = "0.00";
            }
            if (txtPatientAmt.Text == "")
            {
                txtPatientAmt.Text = "0.00";
            }
            string reqCancel = "";
            if (chkCancel.Checked == false)
            {
                reqCancel = "0";
            }
            else
            {
                reqCancel = "1";
            }
            //testdate = DateTime.ParseExact(txtdate.Text.Trim(), "dd/MM/yyyy", dtf).ToString("yyyy-MM-dd");
            //DeliveryDate = DateTime.ParseExact(txtdeldate.Text.Trim(), "dd/MM/yyyy", dtf).ToString("yyyy-MM-dd");
            if (ObjSample.UpdateSampleCollection(compcode, yearcode, hdlSrlNo.Value.ToString().Trim(), txtreg.Text.Trim(), ddlReqNo.SelectedValue.Trim(), ddlSample.SelectedValue.Trim(), txtUnit.Text.Trim(), ddlAgency.SelectedValue.Trim(), txtdate.Text.Trim(), txtdeldate.Text.Trim(), txtCollector.Text.Trim(), txtAgencyAmt.Text, txtPatientAmt.Text, Session["userName"].ToString(), ddlPaymentMode.SelectedValue.Trim(), txtBankName.Text.Trim(), txtChequeNo.Text.Trim(), chqdt, hdnvchno.Value, reqCancel, txtcancelReason.Text))
            {
                lblError.Text = "Record Successfully Saved!";
                lblError.ForeColor = Color.Green;
            }
            else
            {
                lblError.Text = "Error Occured during updation!";
                lblError.ForeColor = Color.Red;
            }
        }
    }


    protected void ddlPaymentMode_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlPaymentMode.SelectedValue == "B")
        { 
            lblchqdt.InnerText = "Cheque Date :";
            lblchqno.InnerText = "Cheque No :";
            lblbankNm.InnerText = "Bank Name :";
            divchqdt.Visible = true;
            divchqno.Visible = true;
            divBank.Visible = true;
        }
        else if (ddlPaymentMode.SelectedValue == "R")
        {
            lblchqdt.InnerText = "Expire Date :";
            lblchqno.InnerText = "Card No :";
            lblbankNm.InnerText = "Card Holder Name :";
            divchqdt.Visible = true;
            divchqno.Visible = true;
            divBank.Visible = true;
        }
        else
        {
            divchqdt.Visible = false;
            divchqno.Visible = false;
            divBank.Visible = false;
        }
    }
    protected void GridView1_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        string cocode = Session["CoCode"].ToString().Trim();
        string yearcode = Session["YearCode"].ToString().Trim();
        Label lblSrlno = (Label)GridView1.Rows[e.RowIndex].FindControl("lblSrlno");
        Label lblregno = (Label)GridView1.Rows[e.RowIndex].FindControl("lblregno");
        Label lblvchno = (Label)GridView1.Rows[e.RowIndex].FindControl("lblvchno");

        ObjSample.DeleteSampleCollection(cocode, yearcode, lblSrlno.Text, lblvchno.Text);
        //thereq.DeleteReg(cocode, lblregno.Text);
        lblError.ForeColor = System.Drawing.Color.Green;
        lblError.Text = "Deleted Successfully";
        GridFill();
        ResetAllFields();
    }
    protected void Button3_Click(object sender, EventArgs e)
    {
        string someScript = "";
        someScript = "<script language='javascript'> var el = document.getElementById('h1');el.style.display = 'none';</script>";
        Page.ClientScript.RegisterStartupScript(this.GetType(), "onload", someScript);

        GetReport();
        Button7.Visible = true;
        Button6.Visible = true;
    }

    public void GetReport()
    {
        Report_Header();
        GetHearder_Detail();
        ltrReport.Text = rpt.ToString();
    }
    public void Report_Header()
    {
        System.Globalization.DateTimeFormatInfo dtf = new DateTimeFormatInfo();
        string date = DateTime.Now.ToString("dd/MM/yyyy");

        rpt.Append("<table width='100%' cellspacing=0 border=0 >");
        rpt.Append("<tr cellpadding:'0'  style='height:30px;'>");
        rpt.Append("<td width='30%'><img src='../Images/logo.jpg'  style=''/></td>");
        rpt.Append("<td   width='40%'  style='height:20px;font-family:Arial; font-size:medium; text-align:center'><b><u>Sample Requisition Slip</u></b></td>");
        rpt.Append("<td width='30%' style='text-align:right'>Print Date : " + date + "</td>");
        rpt.Append("</tr>");
        rpt.Append("</table>");
    }
    public void GetHearder_Detail()
    {
        DataTable dt = thereq.GetRequisitionForReport(Session["CoCode"].ToString().Trim(), Session["YearCode"].ToString().Trim(), ddlReqNo.SelectedValue); //ds.Tables[0];
        //DataTable SlipSession = (DataTable)Session["SlipSession"];
        DataTable SlipSession = thedia.GetExistSampleDetails(Session["CoCode"].ToString().Trim(), Session["YearCode"].ToString().Trim(), ddlReqNo.SelectedValue, hdlSrlNo.Value);
        if (SlipSession != null)
        {

            rpt.Append("<table width='100%' cellspacing=0 border=0 style='border:1px solid black;border-spacing: 0; padding: 0;font-family:verdana;'>");

            rpt.Append("<tr style='height:20px'>");
            rpt.Append("<td colspan='6' style='width: 8%;border-bottom: 1px solid black; font-family:Verdana;font-size:small; text-align:Center;background-color:#9B9C8D'> PATIENT DETAILS  </td>");
            rpt.Append("</tr'>");


            rpt.Append("<tr style='height:30px'>");
            rpt.Append("<td style='width: 8% ;border-bottom: 1px solid black;border-right: 1px solid black; font-family:Verdana; text-align:left'>Requisition No :</td>");
            rpt.AppendFormat("<td style='width: 5%;border-bottom: 1px solid black; border-right: 1px solid black; font-family:Verdana; text-align:left'>{0}</td>", dt.Rows[0]["RequisitionNo"]);
            rpt.Append("<td style='width: 8%;border-bottom: 1px solid black; border-right: 1px solid black; font-family:Verdana;  text-align:left'>Test Date</td>");
            rpt.AppendFormat("<td style='width: 5%;border-bottom: 1px solid black; border-right: 1px solid black; font-family:Verdana; text-align:left'>{0}</td>", /*dt.Rows[0]["TDate"]*/ SlipSession.Rows[0]["Date"]);
            rpt.Append("<td style='width: 8%;border-bottom: 1px solid black;border-right: 1px solid black; font-family:Verdana;  text-align:left'>Delivery Date</td>");
            rpt.AppendFormat("<td style='width: 5%;border-bottom: 1px solid black;font-family:Verdana; text-align:left'>{0}</td>", /*dt.Rows[0]["delDate"]*/ SlipSession.Rows[0]["DeliveryDate"]);
            rpt.Append("</tr>");

            rpt.Append("<tr style='height:30px'>");
            rpt.Append("<td style='width: 8%;border-bottom: 1px solid black;border-right: 1px solid black; font-family:Verdana;  text-align:left'>Reg. No :</td>");
            rpt.AppendFormat("<td style='width: 5%;border-bottom: 1px solid black;border-right: 1px solid black; font-family:Verdana; text-align:left'>{0}</td>", dt.Rows[0]["RegistrationNo"]);
            rpt.Append("<td style='width: 8%;border-bottom: 1px solid black;border-right: 1px solid black; font-family:Verdana;  text-align:left'>Name & Age : </td>");
            rpt.AppendFormat("<td style='width: 5%;border-bottom: 1px solid black;border-right: 1px solid black;font-family:Verdana; text-align:left'>{0}</td>", dt.Rows[0]["PatientName"].ToString() + ", " + dt.Rows[0]["age"].ToString());
            rpt.Append("<td style='width: 8%;border-bottom: 1px solid black;border-right: 1px solid black; font-family:Verdana;  text-align:left'>Contact No : </td>");
            rpt.AppendFormat("<td style='width: 5%;border-bottom: 1px solid black;font-family:Verdana; text-align:left'>{0}</td>", dt.Rows[0]["Ph1"]);
            rpt.Append("</tr>");

            rpt.Append("<tr style='height:30px'>");
            rpt.Append("<td style='width: 8%;border-right: 1px solid black; font-family:Verdana;  text-align:left'>Address :</td>");
            rpt.AppendFormat("<td colspan=3 style='width: 5%;font-family:Verdana; text-align:left;border-right: 1px solid black; '>{0}</td>", dt.Rows[0]["Address"]);
            rpt.Append("<td style='width: 8%;border-right: 1px solid black; font-family:Verdana;  text-align:left'>Refer By</td>");
            rpt.AppendFormat("<td  style='width: 5%;font-family:Verdana; text-align:left'>{0}</td>", dt.Rows[0]["ReferalName"]);




            rpt.Append("</tr >");
            rpt.Append("</table>");



            rpt.Append("<br/>");
            rpt.Append("<center>");
            rpt.Append("<table width='100%'  cellspacing=0 border=0 style='border:1px solid black;border-spacing: 0; padding: 0;font-family:verdana;'>");

            rpt.Append("<tr style='height:20px'>");
            rpt.Append("<td colspan='6' style='width: 6%;border-bottom: 1px solid black;border-top: 1px solid black; font-family:Verdana;font-size:small; text-align:Center;background-color:#9B9C8D'> SAMPLE DETAILS  </td>");
            rpt.Append("</tr'>");

            rpt.Append("<tr style='height:30px'>");
            rpt.Append("<td style='width:35%;border-bottom: 1px solid black;border-right: 1px solid black; font-family:Verdana;  text-align:left'>Code</td>");
            rpt.Append("<td style='width:35%;border-bottom: 1px solid black;border-right: 1px solid black; font-family:Verdana;  text-align:left'>Sample Name</td>");
            rpt.Append("<td style='width:10%;border-bottom: 1px solid black;font-family:Verdana; text-align:right'>Charge</td>");
            double total = 0.0;
            int a = 0, b = 0, c = 0, d = 0;
            rpt.Append("</tr >");

            for (int i = 0; i < SlipSession.Rows.Count; i++)
            {
                rpt.AppendFormat("<td style='border-bottom: 1px solid black;border-right: 1px solid black; font-family:Verdana;text-align:left'>{0}</td>", SlipSession.Rows[i]["samplecode"].ToString());
                rpt.AppendFormat("<td style='border-bottom: 1px solid black;border-right: 1px solid black; font-family:Verdana;text-align:left'>{0}</td>", SlipSession.Rows[i]["TestName"].ToString());
                rpt.AppendFormat("<td style='border-bottom: 1px solid black;font-family:Verdana; text-align:right'> {0}</td>", SlipSession.Rows[i]["PatientBillAmt"].ToString());
                rpt.Append("</tr >");
                total = total + Convert.ToDouble(SlipSession.Rows[i]["PatientBillAmt"]);
            }
            rpt.Append("</table>");
            rpt.Append("<br/>"); rpt.Append("<br/>");
            rpt.Append("<table>");

            rpt.Append("<tr>");
            rpt.Append("<td style='width: 5%; font-family:Verdana;  text-align:left'></td>");
            rpt.Append("<td style='width: 5%; font-family:Verdana;  text-align:left'></td>");
            rpt.Append("<td style='width: 5%; font-family:Verdana;  text-align:left'></td>");
            rpt.Append("<td style='width: 5%; font-family:Verdana;  text-align:center'>________________________________</td>");
            rpt.Append("</tr'>");

            rpt.Append("<tr>");
            rpt.Append("<td style='width: 5%; font-family:Times New Roman;  text-align:left'></td>");
            rpt.Append("<td style='width: 5%; font-family:Verdana;  text-align:left'></td>");
            rpt.Append("<td style='width: 5%; font-family:Verdana;  text-align:left'></td>");
            rpt.Append("<td style='width: 5%; font-family:Verdana;  text-align:center'> Signature </td>");

            rpt.Append("</tr'>");
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
    protected void Button6_Click(object sender, EventArgs e)
    {
        ltrReport.Visible = false;
        Button7.Visible = false;
        Button6.Visible = false;
    }
}