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

public partial class Account_AdvancePayment : System.Web.UI.Page
{
    Main Objmain = new Main(ConfigurationSettings.AppSettings["ConnectionString"].ToString());
    AnyTimePayment anypayment = new AnyTimePayment(ConfigurationSettings.AppSettings["ConnectionString"].ToString());
    PH_PatientRequisition thereq = new PH_PatientRequisition(ConfigurationSettings.AppSettings["ConnectionString"].ToString());
    System.Text.StringBuilder rpt = new System.Text.StringBuilder();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["userName"] == null)
        {
            Response.Redirect("../LoginPage.aspx");
        }
        if (Objmain.checkaccss(Session["CoCode"].ToString(), Session["UserRoleID"].ToString().Trim(), "ADVANCE PAYMENT ENTRY", checkAccessType.ViewAction) == false)
        {
            Response.Redirect("../AccessDenied.aspx");
        }

        if (Objmain.checkaccss(Session["CoCode"].ToString(), Session["UserRoleID"].ToString().Trim(), "ADVANCE PAYMENT ENTRY", checkAccessType.InsertAction) == false)
        {
            Button1.Enabled = false;
        }

        if (!IsPostBack)
        {

            DataTable dt1;
            dt1 = anypayment.getServiceList(Session["CoCode"].ToString().Trim());
            ddlService.DataTextField = "TestName";
            ddlService.DataValueField = "TestId";
            ddlService.DataSource = dt1;
            ddlService.DataBind();
            GridFill();
            Tab1Func();
        }
    }

    public void Tab1Func()
    {
        ResetAllFields();
        Tab1.CssClass = "Clicked";
        Tab2.CssClass = "Initial";
        MainView.ActiveViewIndex = 0;
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

    public void GenerateCode()
    {
        DataTable dt1;
        dt1 = thereq.GenerateReceiptNo(Session["CoCode"].ToString().Trim(), Session["YearCode"].ToString().Trim(), "RCP");
        string ReceptNo = dt1.Rows[0][0].ToString();
        txtReceiptNo.Text = ReceptNo;
    }

    protected void Button1_Click(object sender, EventArgs e)
    {
        if (txtPatientName.Text == "")
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "msg", "alert('Patient Name Cannot be blank !');", true);
        }
        else if (txtPayableAmt.Text == "")
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "msg", "alert('Amount Cannot be blank !');", true);
        }
        else if (ddlService.SelectedValue == "")
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "msg", "alert('Select service!');", true);
        }
        else if (Convert.ToDecimal(txtPayableAmt.Text) == 0)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "msg", "alert('Amount must be more than 0 !');", true);
        }
        else
        {
            if (Button1.Text == "Submit")
            {
                string regno = Request.Form[txtreg.UniqueID];
                txtreg.Text = regno;
                DataTable dt1;
                dt1 = thereq.GenerateReceiptNo(Session["CoCode"].ToString().Trim(), Session["YearCode"].ToString().Trim(), "RCP");
                string ReceptNo = dt1.Rows[0][0].ToString();
                txtReceiptNo.Text = ReceptNo;
                
                if (anypayment.InsertUpdateAdvancePayment(Session["CoCode"].ToString().Trim(), Session["YearCode"].ToString().Trim(), txtReceiptNo.Text, txtreg.Text, txtPayableAmt.Text, ddlPaymentMode.SelectedValue, Session["userName"].ToString(),txtDocId.Text,txtBankName.Text,txtChequeNo.Text,txtchqdt.Text,ddlService.SelectedValue.Trim(),lblSrvName.Text,lblSrvCost.Text,"I") == true)
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "msg", "alert('Inserted  Successfully !');", true);
                    //ResetAllFields();
                }
            }
            else
            {
                string regno = Request.Form[txtreg.UniqueID];
                txtreg.Text = regno;
                if (anypayment.InsertUpdateAdvancePayment(Session["CoCode"].ToString().Trim(), Session["YearCode"].ToString().Trim(), txtReceiptNo.Text, txtreg.Text, txtPayableAmt.Text, ddlPaymentMode.SelectedValue, Session["userName"].ToString(), txtDocId.Text, txtBankName.Text, txtChequeNo.Text, txtchqdt.Text, ddlService.SelectedValue.Trim(), lblSrvName.Text, lblSrvCost.Text, "U") == true)
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "msg", "alert('Updated  Successfully !');", true);
                    //ResetAllFields();
                }
            }
            GridFill();
        }
    }


    public void ResetAllFields()
    {
        DataTable dt1;
        dt1 = thereq.GenerateReceiptNo(Session["CoCode"].ToString().Trim(), Session["YearCode"].ToString().Trim(), "RCP");
        string ReceptNo = dt1.Rows[0][0].ToString();
        txtReceiptNo.Text = ReceptNo;
        txtPayableAmt.Text = "0.00";
        txtPatientName.Text = "";
        txtreg.Text = "";
        ddlPaymentMode.SelectedIndex = 0;
    }

    public void GridFill()
    {
        GridView1.DataSource = anypayment.GetAdvanceDetails(Session["CoCode"].ToString().Trim(), Session["YearCode"].ToString().Trim(), txtPnameSrch.Text,txtRegNoSrch.Text,txtPhnoSrch.Text);
        GridView1.DataBind();
    }


    protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "Select")
        {
            
            int index = Convert.ToInt32(e.CommandArgument);
            Label receiptNo = (Label)GridView1.Rows[index].FindControl("receiptNo");
            Label lblRegNo = (Label)GridView1.Rows[index].FindControl("lblRegNo");
            Label lblPatientName = (Label)GridView1.Rows[index].FindControl("lblPatientName");
            Label lblAdvAmt = (Label)GridView1.Rows[index].FindControl("lblAdvAmt");
            Label lblAdvDate = (Label)GridView1.Rows[index].FindControl("lblAdvDate");
            Label lblPaymentMode = (Label)GridView1.Rows[index].FindControl("lblPaymentMode");
            Label lblstatus = (Label)GridView1.Rows[index].FindControl("lblstatus");
            Label lbldocId = (Label)GridView1.Rows[index].FindControl("lbldocId");
            Label lblDocName = (Label)GridView1.Rows[index].FindControl("lblDocName");

            txtReceiptNo.Text = receiptNo.Text;
            txtPatientName.Text = lblPatientName.Text;
            txtreg.Text = lblRegNo.Text;
            hdnregno.Value = lblRegNo.Text;
            txtPayableAmt.Text = lblAdvAmt.Text;
            txtPayableAmt.Enabled = false;
            ddlPaymentMode.SelectedValue = lblPaymentMode.Text;
            txtDocId.Text = lbldocId.Text;
            txtdocname.Text = lblDocName.Text;


            Tab1.CssClass = "Clicked";
            Tab2.CssClass = "Initial";
            MainView.ActiveViewIndex = 0;
            
            Button1.Text = "Update";

            if (lblstatus.Text == "1")
            {
                Button1.Enabled = false;
                ScriptManager.RegisterStartupScript(this, this.GetType(), "msg", "alert('Receipt Already Adjusted, Cannot modify!');", true);
            }
            else
            {
                Button1.Enabled = true;
            }
        }

        if (e.CommandName == "Print")
        {
            int index = Convert.ToInt32(e.CommandArgument);
            Label receiptNo = (Label)GridView1.Rows[index].FindControl("receiptNo");
            Label lblRegNo = (Label)GridView1.Rows[index].FindControl("lblRegNo");

            //BillReport_Header();
            GenerateReceipt(receiptNo.Text, lblRegNo.Text);
            
        }
    }

    protected void Button2_Click(object sender, EventArgs e)
    {
        ResetAllFields();
    }

    protected void Button3_Click(object sender, EventArgs e)
    {
        GridFill();
    }

    protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            //if (Objmain.checkaccss(Session["CoCode"].ToString(), Session["UserRoleID"].ToString().Trim(), "MEDICINE MASTER", checkAccessType.DeleteAction) == false)
            //{
            //    //coldel.Visible = false;
            //    e.Row.Cells[14].Visible = false;
            //}
        }
    }

    protected void GridView1_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        Label receiptNo = (Label)GridView1.Rows[e.RowIndex].FindControl("receiptNo");
        Label lblRegNo = (Label)GridView1.Rows[e.RowIndex].FindControl("lblRegNo");
        Label lblstatus = (Label)GridView1.Rows[e.RowIndex].FindControl("lblstatus");

        if (lblstatus.Text == "1")
        {
            Button1.Enabled = false;
            ScriptManager.RegisterStartupScript(this, this.GetType(), "msg", "alert('Receipt Already Adjusted, Cannot delete!');", true);
        }
        else
        {
            if (anypayment.DeleteAdvancePay(Session["CoCode"].ToString().Trim(), Session["YearCode"].ToString().Trim(), receiptNo.Text, lblRegNo.Text) == true)
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "msg", "alert('Deleted  Successfully !');", true);
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "msg", "alert('Error in Data Deletion !');", true);
            }
            GridFill();
            ResetAllFields();
        }
    }

    protected void GridView1_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        GridView1.PageIndex = e.NewPageIndex;
        GridFill();
    }


    protected void GridView1_SelectedIndexChanged(object sender, EventArgs e)
    {

    }

    [System.Web.Script.Services.ScriptMethod()]
    [System.Web.Services.WebMethod]
    public static List<string> SearchByPatientName(string prefixText)
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
                cmd.CommandText = "select Pr.PatientRegNo + '~' + Pr.PName+'~'+Al.LedgerID as Name from opd_patientregistration Pr,AC_Ledger Al where Al.compcode=Pr.Compcode and Al.LedgerFK=Pr.PatientRegNo and Pr.compcode=@Compcode and Pr.PName like @SearchText + '%'";
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

    [System.Web.Script.Services.ScriptMethod()]
    [System.Web.Services.WebMethod]
    public static List<string> Searchdoc(string prefixText, int count)
    {
        string compcode = string.Empty;
        string yearcode = string.Empty;
        using (SqlConnection conn = new SqlConnection())
        {
            conn.ConnectionString = ConfigurationSettings.AppSettings["ConnectionString"].ToString();
            using (SqlCommand cmd = new SqlCommand())
            {
                compcode = HttpContext.Current.Session["CoCode"].ToString().Trim();
                yearcode = HttpContext.Current.Session["YearCode"].ToString();
                cmd.CommandText = "select doc_id+'~'+doc_name as Name from GN_DoctorMaster where compcode=@Compcode and doc_name like @SearchText +'%'";
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

    protected void btnrcpt_Click(object sender, EventArgs e)
    {
        string someScript = "";
        someScript = "<script language='javascript'> var el = document.getElementById('h1');el.style.display = 'none';</script>";
        Page.ClientScript.RegisterStartupScript(this.GetType(), "onload", someScript);

        
        
        BindReport();
        Button7.Visible = true;
        Button6.Visible = true;
    }


    public void BindReport()
    {
        string receiptNo = txtReceiptNo.Text;
        
        string regno = Request.Form[txtreg.UniqueID];
        DataTable dtPtDtls = anypayment.GetPatientAdvDetails(Session["CoCode"].ToString().Trim(), Session["YearCode"].ToString().Trim(), regno, receiptNo);

        if (dtPtDtls.Rows.Count > 0)
        {
            rpt.Append("<table width='100%' cellspacing=0 border=0 style='height:30px;'>");
            rpt.Append("<td style='width:100%;font-family:Verdana;font-size:small; text-align:right;'> Receipt Date :" + dtPtDtls.Rows[0]["AdvDate"].ToString() + " </td>");
            rpt.Append("<tr>");
            rpt.Append("<td style='width:100%;font-family:Verdana;font-size:small; text-align:right;'> Print Date :" + DateTime.Now.ToString("dd/MM/yyyy HH:mm tt") + " </td>");
            rpt.Append("</tr>");
            rpt.Append("</table>");

            rpt.Append("<table width='100%' cellspacing=0 border=0 style='border-spacing: 0; padding: 0;font-family:verdana;'>");
            rpt.Append("<tr>");
            rpt.Append("<td style='width:100%;font-family:Verdana;font-size:small; text-align:Center;background-color:#9B9C8D'> <b><u>Money Receipt</u></b>  </td>");
            rpt.Append("</tr>");

            rpt.Append("<tr>");
            rpt.Append("<td style='width:100%;font-family:Verdana;font-size:small; text-align:Center;background-color:#9B9C8D'> PATIENT DETAILS  </td>");
            rpt.Append("</tr>");
            rpt.Append("</table>");

            rpt.Append("<table width='100%' cellspacing=0 border=0 style='border-spacing: 0; padding: 0;font-family:verdana;font-size:small;'>");
            rpt.Append("<tr style=''>");
            rpt.Append("<td style='width: 15% ;font-family:Verdana; text-align:left;font-weight:bold;'>Receipt No :</td>");
            rpt.AppendFormat("<td style='width: 35%;font-family:Verdana; text-align:left'>{0}</td>", dtPtDtls.Rows[0]["ReceiptNo"]);
            rpt.Append("<td style='width: 15%;text-align:left;font-weight:bold;'>Patient Id :</td>");
            rpt.AppendFormat("<td style='width: 35%;font-family:Verdana; text-align:left'>{0}</td>", dtPtDtls.Rows[0]["RegNo"]);
            rpt.Append("</tr>");

            rpt.Append("<tr style=''>");
            rpt.Append("<td style='font-family:Verdana;  text-align:left;font-weight:bold;'>Name & Age :</td>");
            rpt.AppendFormat("<td style='font-family:Verdana; text-align:left'>{0}</td>", dtPtDtls.Rows[0]["PName"].ToString() + ", " + dtPtDtls.Rows[0]["Age"].ToString());
            rpt.Append("<td style='font-family:Verdana;  text-align:left;font-weight:bold;'>Contact No : </td>");
            rpt.AppendFormat("<td style='font-family:Verdana; text-align:left'>{0}</td>", dtPtDtls.Rows[0]["PhNo1"]);
            rpt.Append("</tr>");
            rpt.Append("<tr style=''>");
            rpt.Append("<td style='font-family:Verdana;  text-align:left;font-weight:bold;'>Address :</td>");
            rpt.AppendFormat("<td  colspan='3' style='font-family:Verdana; text-align:left'>{0}</td>", dtPtDtls.Rows[0]["Address"]);
            rpt.Append("</tr>");
            rpt.Append("<tr style=''>");
            rpt.Append("<td style='font-family:Verdana;  text-align:left;font-weight:bold;'>Referred By:</td>");
            rpt.AppendFormat("<td colspan='3' style='font-family:Verdana; text-align:left'>{0}</td>", "Dr. " + dtPtDtls.Rows[0]["ReferalName"].ToString());
            rpt.Append("</tr>");
            rpt.Append("</table>");
            rpt.Append("<br/>");
            rpt.Append("<center>");


            rpt.Append("<table width='100%' cellspacing=0 border=0 style='border-spacing: 0; padding: 0;font-family:verdana;font-size:small;'>");
            rpt.Append("<tr>");
            rpt.Append("<td style='width: 80%;font-family:Verdana;  text-align:left;font-weight:bold;'>");

            rpt.Append("<table width='90%'  cellspacing=0 border=0 style='border:1px solid black;border-spacing: 0; padding: 0;font-family:verdana;font-size:small;'>");
            rpt.Append("<tr style=''>");
            rpt.Append("<td colspan='2' style='width: 8%;border-left: 1px solid black;border-bottom: 1px solid black;border-top: 1px solid black; font-family:Verdana;font-size:small; text-align:Center;background-color:#9B9C8D;font-weight:bold;'> SERVICE DETAILS  </td>");
            rpt.Append("</tr'>");
            rpt.Append("<tr style=''>");
            rpt.Append("<td style='width:70%;border-bottom: 1px solid black;border-right: 1px solid black; font-family:Verdana;  text-align:left;font-weight:bold;'>Service Name</td>");
            rpt.Append("<td style='width:30%;border-bottom: 1px solid black;font-family:Verdana; text-align:right;font-weight:bold;'>Charge</td>");
            rpt.Append("</tr >");
            rpt.Append("<tr>");
            rpt.AppendFormat("<td style='border-bottom: 1px solid black;border-right: 1px solid black; font-family:Verdana;text-align:left;font-weight:normal;'>{0}</td>", "Advance Payment for " + dtPtDtls.Rows[0]["testname"].ToString());
            rpt.AppendFormat("<td style='border-bottom: 1px solid black;font-family:Verdana; text-align:right;font-weight:normal;'> {0}</td>", dtPtDtls.Rows[0]["AdvAmount"].ToString());
            rpt.Append("</tr>");
            rpt.Append("</table>");
            rpt.Append("</td>");
            rpt.Append("<td valign='top' style='width: 30%;font-family:Verdana;  text-align:left;font-weight:bold;padding-left:20px;'>");
            rpt.Append("<table width='100%' cellpadding=0 cellspacing=0 >");
            rpt.Append("<tr style=''>");
            rpt.Append("<td colspan='2' style='width:80%; font-family:Verdana; text-align:center;font-weight:bold;'><u>Payment Calculation</u></td>");
            rpt.Append("</tr>");
            rpt.Append("<tr style=''>");
            rpt.Append("<td colspan='2'> &nbsp;</td>");
            rpt.Append("</tr>");
            rpt.Append("<tr style=''>");
            rpt.Append("<td style=' font-family:Verdana; text-align:left;font-weight:bold;'> Paid Amount</td>");
            rpt.AppendFormat("<td style='font-family:Verdana;text-align:right'>{0}</td>", dtPtDtls.Rows[0]["AdvAmount"].ToString());
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
            rpt.Append("<td style='font-family:Verdana;  text-align:left'>In Words : " + dtPtDtls.Rows[0]["num2word"].ToString() + "</td>");
            rpt.Append("<td style='font-family:Verdana;  text-align:center;font-weight:bold;'> " + Session["userName"].ToString() + "  </td>");
            rpt.Append("</tr>");
            rpt.Append("<tr>");
            rpt.Append("<td style='font-family:Verdana;  text-align:left'>Received with thanks </td>");
            rpt.Append("<td colspan='2' style='font-family:Verdana;  text-align:center;font-weight:bold;'> For " + Session["CoName"].ToString() + "</td>");
            rpt.Append("</tr>");


            rpt.Append("<tr style='height:50px'>");
            rpt.Append("<td valign='bottom' colspan='3' valign='bottom' style='width: 100%; font-family:Verdana;  text-align:left;font-size:8pt;color:gray;'><i># Incase you find any unintentional system generated discripency in the bill, kindly bring it to our notice for corrective action.</i></td>");
            rpt.Append("</tr>");

            rpt.Append("</table>");
            rpt.Append("</center>");
        }
        else
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "msg", "alert('No row to Display.. !');", true);
        }

        
        ltrReport.Text = rpt.ToString();
        ltrReport.Visible = true;
    }
    

    protected void Button6_Click(object sender, EventArgs e)
    {
        ltrReport.Visible = false;
        Button7.Visible = false;
        Button6.Visible = false;
    }

    public void GenerateReceipt(string receiptNo, string regno)
    {
        DataTable dtPtDtls = anypayment.GetPatientAdvDetails(Session["CoCode"].ToString().Trim(), Session["YearCode"].ToString().Trim(), regno, receiptNo);

        if (dtPtDtls.Rows.Count > 0)
        {
            rpt.Append("<table width='100%' cellspacing=0 border=0 style='height:30px;'>");
            rpt.Append("<td style='width:100%;font-family:Verdana;font-size:small; text-align:right;'> Receipt Date :" + dtPtDtls.Rows[0]["AdvDate"].ToString() + " </td>");
            rpt.Append("<tr>");
            rpt.Append("<td style='width:100%;font-family:Verdana;font-size:small; text-align:right;'> Print Date :" + DateTime.Now.ToString("dd/MM/yyyy HH:mm tt") + " </td>");
            rpt.Append("</tr>");
            rpt.Append("</table>");

            rpt.Append("<table width='100%' cellspacing=0 border=0 style='border-spacing: 0; padding: 0;font-family:verdana;'>");
            rpt.Append("<tr>");
            rpt.Append("<td style='width:100%;font-family:Verdana;font-size:small; text-align:Center;background-color:#9B9C8D'> <b><u>Money Receipt</u></b>  </td>");
            rpt.Append("</tr>");

            rpt.Append("<tr>");
            rpt.Append("<td style='width:100%;font-family:Verdana;font-size:small; text-align:Center;background-color:#9B9C8D'> PATIENT DETAILS  </td>");
            rpt.Append("</tr>");
            rpt.Append("</table>");

            rpt.Append("<table width='100%' cellspacing=0 border=0 style='border-spacing: 0; padding: 0;font-family:verdana;font-size:small;'>");
            rpt.Append("<tr style=''>");
            rpt.Append("<td style='width: 15% ;font-family:Verdana; text-align:left;font-weight:bold;'>Receipt No :</td>");
            rpt.AppendFormat("<td style='width: 35%;font-family:Verdana; text-align:left'>{0}</td>", dtPtDtls.Rows[0]["ReceiptNo"]);
            rpt.Append("<td style='width: 15%;text-align:left;font-weight:bold;'>Patient Id :</td>");
            rpt.AppendFormat("<td style='width: 35%;font-family:Verdana; text-align:left'>{0}</td>", dtPtDtls.Rows[0]["RegNo"]);
            rpt.Append("</tr>");

            rpt.Append("<tr style=''>");
            rpt.Append("<td style='font-family:Verdana;  text-align:left;font-weight:bold;'>Name & Age :</td>");
            rpt.AppendFormat("<td style='font-family:Verdana; text-align:left'>{0}</td>", dtPtDtls.Rows[0]["PName"].ToString() + ", " + dtPtDtls.Rows[0]["Age"].ToString());
            rpt.Append("<td style='font-family:Verdana;  text-align:left;font-weight:bold;'>Contact No : </td>");
            rpt.AppendFormat("<td style='font-family:Verdana; text-align:left'>{0}</td>", dtPtDtls.Rows[0]["PhNo1"]);
            rpt.Append("</tr>");
            rpt.Append("<tr style=''>");
            rpt.Append("<td style='font-family:Verdana;  text-align:left;font-weight:bold;'>Address :</td>");
            rpt.AppendFormat("<td  colspan='3' style='font-family:Verdana; text-align:left'>{0}</td>", dtPtDtls.Rows[0]["Address"]);
            rpt.Append("</tr>");
            rpt.Append("<tr style=''>");
            rpt.Append("<td style='font-family:Verdana;  text-align:left;font-weight:bold;'>Referred By:</td>");
            rpt.AppendFormat("<td colspan='3' style='font-family:Verdana; text-align:left'>{0}</td>", "Dr. " + dtPtDtls.Rows[0]["ReferalName"].ToString());
            rpt.Append("</tr>");
            rpt.Append("</table>");
            rpt.Append("<br/>");
            rpt.Append("<center>");


            rpt.Append("<table width='100%' cellspacing=0 border=0 style='border-spacing: 0; padding: 0;font-family:verdana;font-size:small;'>");
            rpt.Append("<tr>");
            rpt.Append("<td style='width: 80%;font-family:Verdana;  text-align:left;font-weight:bold;'>");
            
                rpt.Append("<table width='90%'  cellspacing=0 border=0 style='border:1px solid black;border-spacing: 0; padding: 0;font-family:verdana;font-size:small;'>");
                rpt.Append("<tr style=''>");
                rpt.Append("<td colspan='2' style='width: 8%;border-left: 1px solid black;border-bottom: 1px solid black;border-top: 1px solid black; font-family:Verdana;font-size:small; text-align:Center;background-color:#9B9C8D;font-weight:bold;'> SERVICE DETAILS  </td>");
                rpt.Append("</tr'>");
                rpt.Append("<tr style=''>");
                rpt.Append("<td style='width:70%;border-bottom: 1px solid black;border-right: 1px solid black; font-family:Verdana;  text-align:left;font-weight:bold;'>Service Name</td>");
                rpt.Append("<td style='width:30%;border-bottom: 1px solid black;font-family:Verdana; text-align:right;font-weight:bold;'>Charge</td>");
                rpt.Append("</tr >");
                rpt.Append("<tr>");
                rpt.AppendFormat("<td style='border-bottom: 1px solid black;border-right: 1px solid black; font-family:Verdana;text-align:left;font-weight:normal;'>{0}</td>", "Advance Payment for "+dtPtDtls.Rows[0]["testname"].ToString());
                rpt.AppendFormat("<td style='border-bottom: 1px solid black;font-family:Verdana; text-align:right;font-weight:normal;'> {0}</td>", dtPtDtls.Rows[0]["AdvAmount"].ToString());
                rpt.Append("</tr>");
                rpt.Append("</table>");
            rpt.Append("</td>");
            rpt.Append("<td valign='top' style='width: 30%;font-family:Verdana;  text-align:left;font-weight:bold;padding-left:20px;'>");
                rpt.Append("<table width='100%' cellpadding=0 cellspacing=0 >");
                rpt.Append("<tr style=''>");
                rpt.Append("<td colspan='2' style='width:80%; font-family:Verdana; text-align:center;font-weight:bold;'><u>Payment Calculation</u></td>");
                rpt.Append("</tr>");
                rpt.Append("<tr style=''>");
                rpt.Append("<td colspan='2'> &nbsp;</td>");
                rpt.Append("</tr>");
                rpt.Append("<tr style=''>");
                rpt.Append("<td style=' font-family:Verdana; text-align:left;font-weight:bold;'> Paid Amount</td>");
                rpt.AppendFormat("<td style='font-family:Verdana;text-align:right'>{0}</td>", dtPtDtls.Rows[0]["AdvAmount"].ToString());
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
            rpt.Append("<td style='font-family:Verdana;  text-align:left'>In Words : " + dtPtDtls.Rows[0]["num2word"].ToString() + "</td>");
            rpt.Append("<td style='font-family:Verdana;  text-align:center;font-weight:bold;'> " + Session["userName"].ToString() + "  </td>");
            rpt.Append("</tr>");
            rpt.Append("<tr>");
            rpt.Append("<td style='font-family:Verdana;  text-align:left'>Received with thanks </td>");
            rpt.Append("<td colspan='2' style='font-family:Verdana;  text-align:center;font-weight:bold;'> For " + Session["CoName"].ToString() + "</td>");
            rpt.Append("</tr>");


            rpt.Append("<tr style='height:50px'>");
            rpt.Append("<td valign='bottom' colspan='3' valign='bottom' style='width: 100%; font-family:Verdana;  text-align:left;font-size:8pt;color:gray;'><i># Incase you find any unintentional system generated discripency in the bill, kindly bring it to our notice for corrective action.</i></td>");
            rpt.Append("</tr>");

            rpt.Append("</table>");
            rpt.Append("</center>");
        }
        else
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "msg", "alert('No row to Display.. !');", true);
        }


        ltrReceipt.Text = rpt.ToString();
        ltrReceipt.Visible = true;
        Button4.Visible = true;
        Button5.Visible = true;
    }

    protected void Button4_Click(object sender, EventArgs e)
    {
        ltrReceipt.Visible = false;
        Button4.Visible = false;
        Button5.Visible = false;
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
    protected void ddlService_SelectedIndexChanged(object sender, EventArgs e)
    {
        string regno = Request.Form[txtreg.UniqueID];
        txtreg.Text = regno;
        DataTable dt1;
        dt1 = anypayment.getServiceDetails(Session["CoCode"].ToString().Trim(), ddlService.SelectedValue.ToString().Trim());
        lblSrvName.Text = dt1.Rows[0]["TestName"].ToString();
        lblSrvCost.Text = dt1.Rows[0]["Cost"].ToString();
    }

    
}