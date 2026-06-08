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

public partial class Pathology_RequisitionBillCancel : System.Web.UI.Page
{
    Main Objmain = new Main(ConfigurationSettings.AppSettings["ConnectionString"].ToString());
    PH_PatientRequisition thereq = new PH_PatientRequisition(ConfigurationSettings.AppSettings["ConnectionString"].ToString());
    PH_TestPopupMultiple thedia = new PH_TestPopupMultiple(ConfigurationSettings.AppSettings["ConnectionString"].ToString());
    System.Text.StringBuilder rpt = new System.Text.StringBuilder();
    String Date = DateTime.Now.ToString("dd/MM/yyyy");


    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["userName"] == null)
        {
            Response.Redirect("../LoginPage.aspx");
        }
        if (Objmain.checkaccss(Session["CoCode"].ToString(), Session["UserRoleID"].ToString().Trim(), "BILL CANCELLATION ENTRY", checkAccessType.ViewAction) == false)
        {
            Response.Redirect("../AccessDenied.aspx");
        }
        if (Objmain.checkaccss(Session["CoCode"].ToString(), Session["UserRoleID"].ToString().Trim(), "BILL CANCELLATION ENTRY", checkAccessType.InsertAction) == false)
        {
            //Button1.Enabled = false;
        }
        ScriptManager.RegisterStartupScript(this, this.GetType(), "msg", "GetDatetime();", true);
        Page.Title = "Bill cancellation Entry";
        if (!IsPostBack)
        {
            txtRegDate.Text = DateTime.Now.ToString("yyyy-MM-dd");
            Tab1Func();
            GridFill();
            
        }

    }

    public void Tab1Func()
    {
        MainView.ActiveViewIndex = 0;

    }

    public void GridFill()
    {
        DataTable dt = thereq.GridFillBillDetls(Session["CoCode"].ToString().Trim(), Session["YearCode"].ToString().Trim(), txtregSrch.Text, txtnameSrch.Text, txtphSrch.Text, txtRegDate.Text);
        GridView1.DataSource = dt;
        GridView1.DataBind();
    }

    protected void GridView1_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        GridView1.PageIndex = e.NewPageIndex;
        GridFill();
    }
    protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            TextBox txtReason = (TextBox)e.Row.FindControl("txtReason");
            Label lblcancel = (Label)e.Row.FindControl("lblcancel");
            CheckBox ChkCancel = (CheckBox)e.Row.FindControl("ChkCancel");
            Label lblReqSts = (Label)e.Row.FindControl("lblReqSts");
            Label lblRefsts = (Label)e.Row.FindControl("lblRefsts");
            txtReason.Enabled = false;
            if (lblcancel.Text == "1")
            {
                
                ChkCancel.Checked = true;
                ChkCancel.Enabled = false;
            }
            if (lblReqSts.Text == "1" && lblRefsts.Text=="0")
            {
                e.Row.Cells[12].Enabled = true;
            }
            else
            {
                e.Row.Cells[12].Enabled = false;
            }
        }
    }

    protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "CancelRequest")
        {
            GridViewRow rowSelect = (GridViewRow)(((LinkButton)e.CommandSource).NamingContainer);
            int index = rowSelect.RowIndex;
            CheckBox ChkCancel = (CheckBox)GridView1.Rows[index].FindControl("ChkCancel");
            Label lblVchNo = (Label)GridView1.Rows[index].FindControl("lblVchNo");
            Label lblregno = (Label)GridView1.Rows[index].FindControl("lblregno");
            Label lblreqno = (Label)GridView1.Rows[index].FindControl("lblreqno");
            Label lbltype = (Label)GridView1.Rows[index].FindControl("lbltype");
            TextBox txtReason = (TextBox)GridView1.Rows[index].FindControl("txtReason");
            Label lblamt = (Label)GridView1.Rows[index].FindControl("lblamt");
            if (txtReason.Text == "")
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "msg", "alert('Enter reason for rejection !');", true);
            }
            else
            {
                if (thereq.CancelBillDetails(Session["CoCode"].ToString().Trim(), Session["YearCode"].ToString().Trim(), lblVchNo.Text.Trim(), lblregno.Text.Trim(), lblreqno.Text.Trim(), txtReason.Text.Trim(), lblamt.Text.Trim(), Session["userName"].ToString(), lbltype.Text.Trim()) == true)
                {
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Request Successfully Send!')", true);  
                    GridFill();
                }
            }
        }
        if (e.CommandName == "BillRefund")
        {
            GridViewRow rowSelect = (GridViewRow)(((LinkButton)e.CommandSource).NamingContainer);
            int index = rowSelect.RowIndex;
            CheckBox ChkCancel = (CheckBox)GridView1.Rows[index].FindControl("ChkCancel");
            Label lblVchNo = (Label)GridView1.Rows[index].FindControl("lblVchNo");
            Label lblregno = (Label)GridView1.Rows[index].FindControl("lblregno");
            Label lblreqno = (Label)GridView1.Rows[index].FindControl("lblreqno");
            Label lbltype = (Label)GridView1.Rows[index].FindControl("lbltype");
            Label lblname = (Label)GridView1.Rows[index].FindControl("lblname");
            Label lblamt = (Label)GridView1.Rows[index].FindControl("lblamt");
            TextBox txtReason = (TextBox)GridView1.Rows[index].FindControl("txtReason");
            

            lblPRegno.Text = lblregno.Text;
            lblPname.Text = lblname.Text;
            lblPatientreqno.Text = lblreqno.Text;
            lblTotBillAmt.Text = lblamt.Text;
            lblRefAmt.Text = lblamt.Text;
            lblRefVchNo.Text = lblVchNo.Text;
            lblRefReqNo.Text = lblreqno.Text;
            MainView.ActiveViewIndex = 1;
        }
    }
    protected void ChkCancel_CheckedChanged(object sender, EventArgs e)
    {
        CheckBox chk = (CheckBox)sender;
        GridViewRow gvrow = (GridViewRow)chk.Parent.Parent;
        TextBox txtReason = (TextBox)gvrow.FindControl("txtReason");
        if (chk.Checked == true)
        {
            txtReason.Enabled = true;
        }
        else
        {
            txtReason.Enabled = false;
        }
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

    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        //string chqdt = "";
        //if (txtchqdt.Text != "")
        //{
        //    string[] cdt = txtchqdt.Text.Split('/');
        //    chqdt = cdt[1] + cdt[0];
        //}

        if (thereq.RefundRequisitionBill(Session["CoCode"].ToString().Trim(), Session["YearCode"].ToString().Trim(), lblPRegno.Text.Trim(), lblRefReqNo.Text.Trim(), lblRefVchNo.Text.Trim(), ddlPaymentMode.SelectedValue.Trim(), txtBankName.Text, txtChequeNo.Text, txtchqdt.Text, lblRefAmt.Text.Trim(), DateTime.Now.ToString("yyyy-MM-dd"), Session["userName"].ToString()) == true)
        {
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Successfully Submitted!')", true);
            GridFill();
            Tab1Func();
        }
        else
        {
            lblError.Text = "Error in Saving ";
            lblError.ForeColor = Color.Red;
            lblError.Visible = true;
        }
    }

    protected void btnCancel_Click(object sender, EventArgs e)
    {
        Response.Redirect("../Pathology/RequisitionBillCancel.aspx");
    }

    protected void Button5_Click(object sender, EventArgs e)
    {
        GridFill();
    }
}