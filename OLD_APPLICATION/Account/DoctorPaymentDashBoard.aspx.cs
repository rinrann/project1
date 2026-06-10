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

public partial class Account_DoctorPaymentDashBoard : System.Web.UI.Page
{
    BillGeneration thepatientbill = new BillGeneration(ConfigurationSettings.AppSettings["ConnectionString"].ToString());
    Main Objmain = new Main(ConfigurationSettings.AppSettings["ConnectionString"].ToString());
    DoctorPaymentDashBoard theHelper = new DoctorPaymentDashBoard(ConfigurationSettings.AppSettings["ConnectionString"].ToString());
    System.Text.StringBuilder rpt = new System.Text.StringBuilder();
    String Date = DateTime.Now.ToString("dd/MM/yyyy");
    int i = 0;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["userName"] == null)
        {
            Response.Redirect("../LoginPage.aspx");
        }
        if (Objmain.checkaccss(Session["CoCode"].ToString(), Session["UserRoleID"].ToString().Trim(), "DOCTOR PAYMENT", checkAccessType.ViewAction) == false)
        {
            Response.Redirect("../AccessDenied.aspx");
        }

         if (Session["userName"] == null)
        {
            Response.Redirect("../LoginPage.aspx");
        }
         if (!IsPostBack)
         {
             DropDownList1.Items.Insert(0, new ListItem("--Select--", "0"));
             DropDownList1.Items.Insert(1, new ListItem("Doctor", "D"));
             DropDownList1.Items.Insert(2, new ListItem("Quack ", "Q"));
            // DropDownList1.SelectedValue = "D";
             ddlType.Items.Insert(0, new ListItem("--Select--", "0"));
             quackdiv.Visible = false;
             docdiv.Visible = false;
             butPay.Visible = false;
             butSelect.Visible = false;
             butDeselect.Visible = false;
         }
         Page.Title = "Doctor Payment DashBoard";
    }


    protected void DropDownList1_SelectedIndexChanged(object sender, EventArgs e)
    {
        string reftype=DropDownList1.SelectedValue.ToString();
        string lbltxt = lbltype.Text;
        
        if (reftype == "D")
        {
            ddlType.Items.Clear();
            ddlType.DataSource = theHelper.GetDoctorType();
            ddlType.DataTextField = "TypeName";
            ddlType.DataValueField = "DocTypeId";
            ddlType.DataBind();
            ddlType.Items.Insert(0, new ListItem("--Select--", ""));
            lbltype.Text = "Doctor Type";
        }
        else if (reftype == "Q")
        {
            ddlType.Items.Clear();
            ddlType.Items.Insert(0, new ListItem("--Select--", ""));
            ddlType.Items.Insert(1, new ListItem("Asha", "A"));
            ddlType.Items.Insert(2, new ListItem("Car Rented", "R"));
            ddlType.Items.Insert(3, new ListItem("Car Private", "P"));
            ddlType.Items.Insert(4, new ListItem("Ambulance", "B"));
            ddlType.Items.Insert(5, new ListItem("Rural Doctor", "Q"));
            ddlType.Items.Insert(6, new ListItem("Others", "O"));
            lbltype.Text = "Quack Type";
        }
        else
        {
            ddlType.Items.Clear();
            ddlType.Items.Insert(0, new ListItem("--Select--", ""));
            lbltype.Text = "Type";
        }
        GridFill();
    }


    protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        string cocode = Session["CoCode"].ToString().Trim();
        string yearcode = Session["YearCode"].ToString().Trim();
        System.Globalization.DateTimeFormatInfo dtf = new DateTimeFormatInfo();
        string date1, date2;
        if (txtFromDate.Text != "")
        {
            DateTime testdate = DateTime.ParseExact(txtFromDate.Text, "dd/MM/yyyy", dtf);
            date1 = testdate.ToString();
        }
        else
        {
            date1 = "null";
        }

        if (txtTodate.Text != "")
        {
            DateTime testdate = DateTime.ParseExact(txtTodate.Text, "dd/MM/yyyy", dtf);
            date2 = testdate.ToString();
        }
        else
        {
            date2 = "null";
        }
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            Label lblSelno = (Label)e.Row.FindControl("lblSlno");
            //lblSelno.Text = (e.Row.RowIndex + 1).ToString();
            Label lblid = (Label)e.Row.FindControl("lblId");
            string doctype = ddlType.SelectedValue.ToString();
            DataTable Docpayment = theHelper.GetDoctorPayment(cocode, yearcode,lblid.Text, doctype, date1, date2);
            Label lblamt = (Label)e.Row.FindControl("lblAmt");
            if (Docpayment.Rows.Count > 0)
            {
                lblamt.Text = (Convert.ToDecimal(Docpayment.Rows[0]["OTcharges"]) + Convert.ToDecimal(Docpayment.Rows[0]["Visitcharges"]) + Convert.ToDecimal(Docpayment.Rows[0]["Refercharges"]) + Convert.ToDecimal(Docpayment.Rows[0]["Anesthesischarge"])/* - Convert.ToDecimal(Docpayment.Rows[0]["paymentdone"])*/).ToString();
                if (Convert.ToDecimal(lblamt.Text) <= 0)
                {
                    e.Row.Visible = false;
                }
                else
                {
                    lblSelno.Text = (i + 1).ToString();
                    i = i + 1;
                }
            }
            else { 
                lblamt.Text = "0.00";
                e.Row.Visible = false;
            }
        }
    }
   

    

    protected void GridView1_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        GridView1.PageIndex = e.NewPageIndex;
        GridFill();
    }

    private void GridFill()
    {
        string reftype = DropDownList1.SelectedValue.ToString();
        string doctype = ddlType.SelectedValue.ToString();

        GridView1.DataSource = theHelper.GetPayeeName(Session["CoCode"].ToString().Trim(), reftype, doctype);
        GridView1.DataBind();

        GridView2.Visible = false;
        GridView3.Visible = false; 
        docdiv.Visible = false;
        quackdiv.Visible = false;
        butPay.Visible = false;
        butSelect.Visible = false;
        butDeselect.Visible = false;
    }


    protected void Button1_Click(object sender, EventArgs e)
    {
        GridFill();
    }

    protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        System.Globalization.DateTimeFormatInfo dtf = new DateTimeFormatInfo();
        string date1, date2;
        if (txtFromDate.Text != "")
        {
            DateTime testdate = DateTime.ParseExact(txtFromDate.Text, "dd/MM/yyyy", dtf);
            date1 = testdate.ToString();
        }
        else
        {
            date1 = "null";
        }

        if (txtTodate.Text != "")
        {
            DateTime testdate = DateTime.ParseExact(txtTodate.Text, "dd/MM/yyyy", dtf);
            date2 = testdate.ToString();
        }
        else
        {
            date2 = "null";
        }
        docIdhid.Value = (e.CommandArgument).ToString();
        string reftype = DropDownList1.SelectedValue.ToString();
        string doctype = ddlType.SelectedValue.ToString();
        if (e.CommandName == "Select")
        {            
            if (reftype == "Q")
            {
                QuackGridFill(date1, date2); 
                GridView2.Visible = true;
                GridView3.Visible = false; 
                docdiv.Visible = false;
                quackdiv.Visible = true;
            }
            else if (reftype == "D")
            {
                DockGridFill(date1, date2);
                GridView2.Visible = false;
                GridView3.Visible = true; 
                docdiv.Visible = true;
                quackdiv.Visible = false;
            }
            else
            {
                docdiv.Visible = false;
                quackdiv.Visible = false;
            }
        }
    }

    private void QuackGridFill(string frmdate,string todate)
    {
        string cocode = Session["CoCode"].ToString().Trim();
        string yearcode = Session["YearCode"].ToString().Trim();
        string reftype = DropDownList1.SelectedValue.ToString();
        string doctype = ddlType.SelectedValue.ToString();
        string id = docIdhid.Value.ToString();
        DataTable dtqck = theHelper.GetQuackdetl(cocode, yearcode,id, frmdate, todate);
        int c=dtqck.Rows.Count - 1;
        GridView2.DataSource = dtqck;
        GridView2.DataBind();
        HiddenField1.Text = id;
        quackdiv.Visible = true;
        docdiv.Visible = false;
        if (dtqck.Rows.Count > 0)
        {
            if (Convert.ToDecimal(dtqck.Rows[c]["TotalBill"]) > 0)
            {
                butPay.Visible = true;
                butSelect.Visible = true;
                butDeselect.Visible = true;
            }
            else { butPay.Visible = false;
            butSelect.Visible = false;
            butDeselect.Visible = false;
            
            }
        }
        else { butPay.Visible = false;
        butSelect.Visible = false;
        butDeselect.Visible = false;
        
        }
    }

    private void DockGridFill(string frmdate, string todate)
    {
        string cocode = Session["CoCode"].ToString().Trim();
        string yearcode = Session["YearCode"].ToString().Trim();
        string reftype = DropDownList1.SelectedValue.ToString();
        string doctype = ddlType.SelectedValue.ToString();
        string id = docIdhid.Value.ToString();
        DataTable dtdoc = theHelper.GetDocdetl(cocode, yearcode,id, frmdate, todate);
        int c = dtdoc.Rows.Count - 1;
        GridView3.DataSource = dtdoc;
        GridView3.DataBind();
        HiddenField1.Text = id;
        docdiv.Visible = true;
        quackdiv.Visible = false;
        if (dtdoc.Rows.Count > 0)
        {
            if (Convert.ToDecimal(dtdoc.Rows[c]["TotalBill"]) > 0)
            {
                butPay.Visible = true;
                butSelect.Visible = true;
                butDeselect.Visible = true;
            }
            else
            {
                butPay.Visible = false;
                butSelect.Visible = false;
                butDeselect.Visible = false;
            }
        }
        else
        {
            butPay.Visible = false;
            butSelect.Visible = false;
            butDeselect.Visible = false;
        }
    }


    protected void GridView2_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            Label lblBold = (Label)e.Row.FindControl("lblBold");
            Label lblQSlno = (Label)e.Row.FindControl("lblQckSlno");
            Label lblPatientLedgerQck = (Label)e.Row.FindControl("lblPatientLedgerQck");
            Label lblTotBillqck = (Label)e.Row.FindControl("lblTotBillqck");
            CheckBox ChkPatientqck = (CheckBox)e.Row.FindControl("ChkPatientqck");
            //LinkButton LinkButton2 = (LinkButton)e.Row.FindControl("LinkButton2");
            if (lblBold.Text == "0")
            {
                lblQSlno.Text = (e.Row.RowIndex + 1).ToString();
                //LinkButton2.Visible = false;
                /*DataSet dsDischarge = thepatientbill.TotalBillDtls(lblPatientLedgerQck.Text.Trim());
                DataSet total = thepatientbill.TotalBillDtls(lblPatientLedgerQck.Text.Trim());
                DataTable dttotal = total.Tables[0];
                lblTotBillqck.Text = dttotal.Rows[0]["Total"].ToString();*/
            }
            else { lblQSlno.Text = ""; /*LinkButton2.Visible = true;*/
            ChkPatientqck.Visible = false;
            }
        }
    }
    protected void GridView2_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "Select")
        {
            string reftype = DropDownList1.SelectedValue.ToString();
            string doctype = ddlType.SelectedValue.ToString();
            Session["payeeType"] = reftype;
           
            Session["docId"] =(e.CommandArgument).ToString();
            Session["docType"] = doctype;
            Response.Redirect("../Account/DoctorPayment.aspx");
        }
    }

    protected void GridView2_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        GridView2.PageIndex = e.NewPageIndex;
        //QuackGridFill();
    }

    protected void GridView3_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            Label lblBoldDoc = (Label)e.Row.FindControl("lblBoldDoc");
            Label lblDSlno = (Label)e.Row.FindControl("lblDocSlno");
            Label lblPatientLedgerDoc = (Label)e.Row.FindControl("lblPatientLedgerDoc");
            Label lblTotBill = (Label)e.Row.FindControl("lblTotBill");
            CheckBox ChkPatient = (CheckBox)e.Row.FindControl("ChkPatient");
            if (lblBoldDoc.Text == "0")
            {
                lblDSlno.Text = (e.Row.RowIndex + 1).ToString();
                DataSet dsDischarge = thepatientbill.TotalBillDtls(lblPatientLedgerDoc.Text.Trim(), Session["CoCode"].ToString().Trim());
                /*DataSet total = thepatientbill.TotalBillDtls(lblPatientLedgerDoc.Text.Trim());
                DataTable dttotal = total.Tables[0];
                lblTotBill.Text = dttotal.Rows[0]["Total"].ToString();*/
            }
            else { lblDSlno.Text = ""; ChkPatient.Visible = false; }
        }
    }



    protected void GridView3_RowCommand(object sender, GridViewCommandEventArgs e)
    {

    }

    protected void GridView3_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        GridView3.PageIndex = e.NewPageIndex;
       // QuackGridFill();
    }
    protected void butPay_Click(object sender, EventArgs e)
    {
        int i;
        string patient = "";
        string reftype=DropDownList1.SelectedValue.ToString();
        if (reftype == "D")
        {
            for (i = 0; i < GridView3.Rows.Count; i++)
            {
                CheckBox ChkPatient = (CheckBox)GridView3.Rows[i].FindControl("ChkPatient");
                if (ChkPatient.Checked == true)
                {
                    patient = GridView3.DataKeys[i].Value.ToString() + "','" + patient;
                }
            }
        }
        else if(reftype=="Q")
        {
            for (i = 0; i < GridView2.Rows.Count-1; i++)
            {
                CheckBox ChkPatientqck = (CheckBox)GridView2.Rows[i].FindControl("ChkPatientqck");
                if (ChkPatientqck.Checked == true)
                {
                    patient = GridView2.DataKeys[i].Value.ToString() + "','" + patient;
                }
            }
        }
        patient = patient.Trim(',');
        Session["patientLedger"] = patient;
        Session["docId"] = HiddenField1.Text.ToString();
        Session["docType"] = ddlType.SelectedValue.ToString();
        Session["payeeType"] = DropDownList1.SelectedValue.ToString();
        Response.Redirect("../Account/DoctorPayment.aspx");
    }
    protected void butSelect_Click(object sender, EventArgs e)
    {
        int i;
        if (DropDownList1.SelectedValue.ToString() == "D")
        {
            for (i = 0; i < GridView3.Rows.Count; i++)
            {
                CheckBox ChkPatient = (CheckBox)GridView3.Rows[i].FindControl("ChkPatient");
                Label lblBoldDoc = (Label)GridView3.Rows[i].FindControl("lblBoldDoc");
                if (lblBoldDoc.Text == "0")
                {
                    ChkPatient.Checked = true;
                }
            }
        }
        else
        {
            for (i = 0; i < GridView2.Rows.Count; i++)
            {
                CheckBox ChkPatientqck = (CheckBox)GridView2.Rows[i].FindControl("ChkPatientqck");

                ChkPatientqck.Checked = true;
                
            }
        }
    }
    protected void butDeselect_Click(object sender, EventArgs e)
    {
        int i;
        if (DropDownList1.SelectedValue.ToString() == "D")
        {
            for (i = 0; i < GridView3.Rows.Count; i++)
            {
                CheckBox ChkPatient = (CheckBox)GridView3.Rows[i].FindControl("ChkPatient");
                ChkPatient.Checked = false;
            }
        }
        else
        {
            for (i = 0; i < GridView2.Rows.Count; i++)
            {
                CheckBox ChkPatientqck = (CheckBox)GridView3.Rows[i].FindControl("ChkPatientqck");
                ChkPatientqck.Checked = false;
            }
        }
    }
}