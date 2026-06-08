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

public partial class Pathology_PatientRequisitionOPD : System.Web.UI.Page
{
    Main Objmain = new Main(ConfigurationSettings.AppSettings["ConnectionString"].ToString());
    PH_PatientRequisition thereq = new PH_PatientRequisition(ConfigurationSettings.AppSettings["ConnectionString"].ToString());
    PatientAdmission thepd = new PatientAdmission(ConfigurationSettings.AppSettings["ConnectionString"].ToString());
    PH_TestPopupMultiple thedia = new PH_TestPopupMultiple(ConfigurationSettings.AppSettings["ConnectionString"].ToString());
    Discharge thedischarge = new Discharge(ConfigurationSettings.AppSettings["ConnectionString"].ToString());
    PatientAppointment thepatientAppo = new PatientAppointment(ConfigurationSettings.AppSettings["ConnectionString"].ToString());
    static string userId1 = "";
    System.Text.StringBuilder rpt = new System.Text.StringBuilder();
    String Date = DateTime.Now.ToString("dd/MM/yyyy");

    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["userId"] == null)
        {
            Response.Redirect("../LoginPage.aspx");
        }
        if (Objmain.checkaccss(Session["CoCode"].ToString(), Session["UserRoleID"].ToString().Trim(), "INVESTIGATION REQUISITION", checkAccessType.ViewAction) == false)
        {
            Response.Redirect("../AccessDenied.aspx");
        }

        if (Objmain.checkaccss(Session["CoCode"].ToString(), Session["UserRoleID"].ToString().Trim(), "INVESTIGATION REQUISITION", checkAccessType.InsertAction) == false)
        {
            Button1.Enabled = false;
        }
        ScriptManager.RegisterStartupScript(this, this.GetType(), "msg", "GetDatetime();", true);
        Page.Title = "Patient Requisition";
        string previousPageName = "";
        //if (Session["userName"] == null)
        //{
        //    Response.Redirect("../LoginPage.aspx");
        //}


        if (!IsPostBack)
        {
            //if (Request.QueryString != null)
            //{
            //    txtreg.Text = Request.QueryString[0].ToString();
            //}

            Tab1Func();
            txtpreadvamount.Enabled = false;
            //Button3.Enabled = false;
            Button6.Visible = false;
            Button7.Visible = false;
            txtcurtest.Value = "";//////
            cncldiv1.Visible = false;
            cncldiv2.Visible = false;
            
            DropDownFill();
            if (Request.UrlReferrer != null)
            {
                string previousPageUrl = Request.UrlReferrer.AbsoluteUri;
                previousPageName = System.IO.Path.GetFileName(Request.UrlReferrer.AbsolutePath);
            }
            if (previousPageName == "AdmissionPatientList.aspx")
            {
                if (Session["RegNo"] != null)
                {
                    
                    txtreg.Text = Session["RegNo"].ToString();
                }

            }
            else if (previousPageName == "PatientRequisitionList.aspx")
            {
                if (Session["RegNo"] != null)
                {
                    txtreg.Text = Session["RegNo"].ToString();
                }
            }
            else if (previousPageName == "PatientDashBoard.aspx")
            {
                if (Session["RegNo"] != null)
                {
                    txtreg.Text = Session["RegNo"].ToString();
                }
            }
            //else
            //{
            //    Session["RegNo"] = null;
            //}
            if (Session["RegNo"] != null)
            {
                txtreg.Text = Session["RegNo"].ToString();
                //Button4.Visible = false;
                Button5.Visible = false;
                if (Session["ReqNo"] != null)
                {
                    getRequisitionDetails(Session["RegNo"].ToString(), Session["ReqNo"].ToString());
                }
                else
                {
                    FillPatientDetails();
                }
                
            }
            txtdate.Text = DateTime.Now.ToString("yyyy-MM-dd");
            GridFill();
        }

        if (txtdate.Text == "") { txtdate.Text = DateTime.Now.ToString("dd/MM/yyyy"); }
        if (txtdeldate.Text == "") { txtdeldate.Text = DateTime.Now.AddDays(1).ToString("dd/MM/yyyy"); }
        //Session["RegNo"] = null;
    }

    public void DropDownFill()
    {
        DataTable dt = thereq.getTestGroup(Session["CoCode"].ToString().Trim(), "C");
        ddltestGroup.DataSource = dt;
        ddltestGroup.DataTextField = "TestName";
        ddltestGroup.DataValueField = "ProfileCode";
        ddltestGroup.DataBind();

        
        DataTable dtTest = thereq.getTest(Session["CoCode"].ToString().Trim(), ddltestGroup.SelectedValue.Trim());
        ddltestCode.DataSource = dtTest;
        ddltestCode.DataTextField = "TestName";
        ddltestCode.DataValueField = "TestId";
        ddltestCode.DataBind();
        ddltestCode.SelectedValue = dtTest.Rows[0]["TestId"].ToString();
        txttestcode.Text = dtTest.Rows[0]["TestId"].ToString();
        txttestname.Text = dtTest.Rows[0]["TestName"].ToString();
        txtcost.Text = dtTest.Rows[0]["Cost"].ToString();

        //string testcodes = Request.Form[txttestcode.UniqueID];
        //string testnames = Request.Form[txttestname.UniqueID];
        //string testcosts = Request.Form[txtcost.UniqueID];
        string Performingdocscodes = Request.Form[txtconsultant.UniqueID];
        string Performingdocsnamess = Request.Form[txtconsultantname.UniqueID];
        //string PayableAmt = Request.Form[txtPayableAmt.UniqueID];

        //testcodes = dtTest.Rows[0]["TestId"].ToString();
        //testnames = dtTest.Rows[0]["TestName"].ToString();
        //testcosts = dtTest.Rows[0]["Cost"].ToString();
        txtPayableAmt.Text = dtTest.Rows[0]["Cost"].ToString();

        ddlDept.Items.Clear();
        ddlDept.DataSource = thepatientAppo.DropdownDepartment(Session["CoCode"].ToString().Trim());
        ddlDept.DataTextField = "DeptName";
        ddlDept.DataValueField = "DeptCode";
        ddlDept.DataBind();

        ddldocType.Items.Clear();
        ddldocType.DataSource = thepatientAppo.DropdownDocType(Session["CoCode"].ToString().Trim());
        ddldocType.DataTextField = "TypeName";
        ddldocType.DataValueField = "DocTypeId";
        ddldocType.DataBind();

        ddlDoctor.Items.Clear();
        ddlDoctor.DataSource = thepatientAppo.DropdownConsultantDoc(Session["CoCode"].ToString().Trim(), "0");
        ddlDoctor.DataTextField = "doc_name";
        ddlDoctor.DataValueField = "doc_id";
        ddlDoctor.DataBind();
        this.ddlDoctor.Items.Insert(0, new ListItem("Select", "0"));
    }

    public void GenerateCode()
    {
        DataTable dt = thereq.GenerateRequisitionNo(Session["CoCode"].ToString().Trim(), Session["YearCode"].ToString().Trim());
        txtreqno.Text = dt.Rows[0][0].ToString();
    }


    public void GridFill()
    {
        //string regno = Request.Form[txtreg.UniqueID];
        GridView1.DataSource = thereq.GridFill(Session["CoCode"].ToString().Trim(), Session["YearCode"].ToString().Trim(), txtregSrch.Text, "OPD", txtnameSrch.Text, txtphSrch.Text, txtRegDate.Text);
        GridView1.DataBind();
    }

    protected void Button1_Click(object sender, EventArgs e)
    {
        string regno = Request.Form[txtreg.UniqueID];
        string cocode = Session["CoCode"].ToString().Trim();
        string yearcode = Session["YearCode"].ToString().Trim();
        string testcodes = Request.Form[txttestcode.UniqueID];
        txttestcode.Text = testcodes;
        string testcost = Request.Form[txtcost.UniqueID];
        txtcost.Text = testcost;
        
        //string[] test = testcodes.Split(',');
        string consult = Request.Form[txtconsultant.UniqueID];
        string[] consultant = consult.Split(',');
        string ph1 = txtph1.Text;
        string ph2 = txtph2.Text;

        string testnames = Request.Form[txttestname.UniqueID];
        string testcosts = Request.Form[txtcost.UniqueID];
        string Performingdocscodes = Request.Form[txtconsultant.UniqueID];
        string Performingdocsnamess = Request.Form[txtconsultantname.UniqueID];
        string PayableAmt = Request.Form[txtPayableAmt.UniqueID];
        

        txttestcode.Text = testcodes;
        txttestname.Text = testnames;
        txtcost.Text = testcosts;
        txtconsultant.Text = Performingdocscodes;
        txtconsultantname.Text = Performingdocsnamess;
        txtPayableAmt.Text = PayableAmt;


        if (txtadvamount.Text == "")
            txtadvamount.Text = "0";
        if (txtNewCost.Text == "")
            txtNewCost.Text = "0";
        if (txtDiscAmt.Text == "")
        {
            txtDiscAmt.Text = "0";
        }
        txttestname.Text = Request.Form[txttestname.UniqueID];
        txtdueamount.Text = Request.Form[txtdueamount.UniqueID];
        double totval = Convert.ToDouble(testcost) /*+ Convert.ToDouble(txtNewCost.Text)*/;
        double due = totval - Convert.ToDouble(txtadvamount.Text) - Convert.ToDouble(txtDiscAmt.Text);
        System.Globalization.DateTimeFormatInfo dtf = new DateTimeFormatInfo();
        DateTime testdate, deldate;
        //if (txtdate.Text == "") { txtdate.Text = DateTime.Now.ToString("dd/MM/yyyy"); }
        if (txtdeldate.Text == "") { txtdeldate.Text = DateTime.Now.AddDays(1).ToString("dd/MM/yyyy"); }
        if (txtdate.Text == "") { txtdate.Text = DateTime.Now.ToString("yyyy-MM-dd"); }
        //testdate = DateTime.ParseExact(txtdate.Text, "dd/MM/yyyy", dtf);
        deldate = DateTime.ParseExact(txtdeldate.Text, "dd/MM/yyyy", dtf);
        TextBox4.Value = txtreqno.Text;
        string body = "";

        string chqdt = "";
        if (txtchqdt.Text != "")
        {
            string[] cdt = txtchqdt.Text.Split('/');
            chqdt = cdt[2] + "-" + cdt[1] + "-" + cdt[0];
        }

        if (TextBox2.Text != "")
        {

            string mailid = TextBox2.Text;
            string first = ""; string Encriptpassword = ""; string userID = "";

            string second = "";
            string[] name = TextBox2.Text.ToLower().Split(' ');
            if (name[0].Length < 3)
                first = name[0].Substring(0, name[0].Length);
            else
                first = name[0].Substring(0, 3);
            if (name.Length > 1)
            {
                if (name[1].Length < 3)
                    second = name[1].Substring(0, name[1].Length);
                else
                    second = name[1].Substring(0, 3);
            }
            else
            {
                second = "not";
            }
            string pattern = "{0}{1}{2}";
            int dt = DateTime.Now.Day;
            userID = CheckUserId(string.Format(pattern, first, second, dt));

            RandomStringGenerator.RandomStringGenerator re = new RandomStringGenerator.RandomStringGenerator();
            string password = re.Generate(8);
            body = "Hello " + TextBox2.Text + " Your User Name :" + userID + "                          Password :" + password;
            //SendMail(mailid, "gfchospital.ghatal@gmail.com", null, "GFC Login Credential", body);
            Encriptpassword = EncryptionDecryption.CryptorEngine.EncryptData(password);

        }

        //  SendSMS(txtph1.Text, body);
        string reqCancel = "";
        if (chkCancel.Checked == false)
        {
            reqCancel = "0";
        }
        else
        {
            reqCancel = "1";
        }
        string reqType = "OPD";//for OPD

        if (txtreferal.Text == "")
        {
            txtreferal.Text = "Self";
        }
        
        DataTable generatetreq = thereq.GenerateTestReqno(cocode);
        if (Button1.Text == "Submit")
        {
            DataTable dtreqNo = thereq.GenerateRequisitionNo(Session["CoCode"].ToString().Trim(), Session["YearCode"].ToString().Trim());
            string recno = dtreqNo.Rows[0][0].ToString();
            txtreqno.Text = recno;
            if ((thereq.InsertRequisition(cocode, yearcode, txttime.Text, txtage.Text, due.ToString(), regno, recno, txtname.Text, txtreferal.Text, txtaddress.Text, txtaddress2.Text, ph1, ph2, txtdate.Text, deldate.ToString("yyyy-MM-dd"), txtadvamount.Text, Session["userId"].ToString(), DateTime.Now.ToString("yyyy-MM-dd"), ddlPaymentMode.SelectedValue.Trim(), txtBankName.Text.Trim(), txtChequeNo.Text.Trim(), chqdt, ddlDoctor.SelectedValue.Trim(), ddlDept.SelectedValue.Trim(), reqType, txtDiscAmt.Text.Trim(), txtPayableAmt.Text.Trim(), ddldocType.SelectedValue.Trim(), txtReptAmt.Text.Trim()) == true))
            {
                lblError.Text = "Inserted Successfully ....";
                lblError.ForeColor = Color.Green;
                divOpt.Visible = true;
            }
            else
            {
                lblError.Text = "Error in Inserted Data";
                lblError.ForeColor = Color.Red;
                divOpt.Visible = false;
            }
            thereq.DeleteRequisition(Session["CoCode"].ToString().Trim(), Session["YearCode"].ToString().Trim(), txtreqno.Text);

            thereq.InsertRequisitionTestMap(cocode, "P", recno, ddltestCode.SelectedValue.Trim(), generatetreq.Rows[0][0].ToString(), txtdate.Text, deldate.ToString("yyyy-MM-dd"), txttime.Text, ddltestCode.SelectedValue.Trim(), Session["userId"].ToString(), ddlDoctor.SelectedValue.ToString().Trim(), txtreferal.Text.Trim(), totval.ToString(), txttestname.Text);
            //InsertDialysisMap(test, consultant, regno, txtdate.Text);
        }
        else
        {
            if ((thereq.UpdateRequisition(cocode, yearcode, regno, txtage.Text, txtreqno.Text, txtname.Text, txtreferal.Text, txtaddress.Text, txtaddress2.Text, ph1, ph2, txtdate.Text, deldate.ToString("yyyy-MM-dd"), txtadvamount.Text, DateTime.Now.ToString("yyyy-MM-dd"), Session["userId"].ToString(), ddlPaymentMode.SelectedValue.Trim(), txtBankName.Text.Trim(), txtChequeNo.Text.Trim(), chqdt, hdnvchno.Value, reqCancel, txtcancelReason.Text, due.ToString(), ddlDoctor.SelectedValue.Trim(), ddlDept.SelectedValue.Trim(), txttime.Text, txtDiscAmt.Text.Trim(), txtPayableAmt.Text.Trim(), ddldocType.SelectedValue.Trim(), txtReptAmt.Text.Trim()) == true))
            {
                lblError.Text = "Updated Successfully ....";
                lblError.ForeColor = Color.Green;
            }
            else
            {
                lblError.Text = "Error in Updated Data";
                lblError.ForeColor = Color.Red;
            }
            thereq.DeleteRequisition(Session["CoCode"].ToString().Trim(), Session["YearCode"].ToString().Trim(), txtreqno.Text);
            thereq.InsertRequisitionTestMap(cocode, "P", txtreqno.Text, ddltestCode.SelectedValue.Trim(), generatetreq.Rows[0][0].ToString(), txtdate.Text, deldate.ToString("yyyy-MM-dd"), txttime.Text, "", Session["userId"].ToString(), ddlDoctor.SelectedValue.ToString().Trim(), txtreferal.Text, totval.ToString(), txttestname.Text);
            //InsertDialysisMap(test, consultant, regno, txtdate.Text);

        }
        GridFill();



        DataTable dt12 = (DataTable)Session["CurrentTable"];
        Session["SlipSession"] = dt12;
        Session["CurrentTable"] = null;
        Button1.Text = "Submit";
        Button1.Enabled = false;
        Button3.Enabled = true;
        Session["ReqNo"] = null;

        //if(Session["Flag"].ToString()=="1")
        //    Response.Redirect("../DayCare/PatientDashBoard.aspx");


    }

    public void InsertDialysisMap(string test, string consultant, string regno, string testdate)
    {
        string cocode = Session["CoCode"].ToString().Trim();
        DataTable dt = (DataTable)Session["CurrentTable"];
        DataTable generatetreq = thereq.GenerateTestReqno(cocode);
        System.Globalization.DateTimeFormatInfo dtf = new DateTimeFormatInfo();

        if (Session["CurrentTable"] != null)
        {


            thereq.DeleteRequisition(Session["CoCode"].ToString().Trim(), Session["YearCode"].ToString().Trim(), txtreqno.Text);
            for (int i = 0; i < dt.Rows.Count; i++)
            {



                string DeliveryDate = "";
                string testcd = dt.Rows[i]["TestId"].ToString().Substring(0, 2);

                if (dt.Rows[i]["TestReqNo"].ToString() == "")
                {
                    if (txtcurtest.Value == "")
                    {
                        txtcurtest.Value = "'" + generatetreq.Rows[0][0].ToString() + "'";
                    }
                    else
                    {
                        txtcurtest.Value = txtcurtest.Value + ",'" + generatetreq.Rows[0][0].ToString() + "'";
                    }
                }
                //if (dt.Rows[0]["Date"].ToString() != "")
                //    testdate = DateTime.ParseExact(dt.Rows[i]["Date"].ToString(), "dd/MM/yyyy", dtf).ToString("yyyy-MM-dd");
                //else
                //    testdate = "";
                if (dt.Rows[i]["DeliveryDate"].ToString() != "")
                    DeliveryDate = DateTime.ParseExact(dt.Rows[i]["DeliveryDate"].ToString(), "dd/MM/yyyy", dtf).ToString("yyyy-MM-dd");
                else
                    DeliveryDate = "";
                if (testcd == "UG")
                {
                    thereq.InsertRequisitionTestMap(cocode, "U", txtreqno.Text, dt.Rows[i]["TestId"].ToString(), generatetreq.Rows[0][0].ToString(), testdate, DeliveryDate, dt.Rows[i]["Time"].ToString(), dt.Rows[i]["Remarks"].ToString(), Session["userId"].ToString(), consultant[i].ToString().Trim(), txtreferal.Text.Trim(), "", "");
                    //thereq.UpdateChargeDetails(cocode, Session["YearCode"].ToString().Trim(), regno, "U", testdate, dt.Rows[i]["Cost"].ToString());
                }
                else
                {
                    if (testcd == "GC")
                    {
                        thereq.InsertRequisitionTestMap(cocode, "G", txtreqno.Text, dt.Rows[i]["TestId"].ToString(), generatetreq.Rows[0][0].ToString(), testdate, DeliveryDate, dt.Rows[i]["Time"].ToString(), dt.Rows[i]["Remarks"].ToString(), Session["userId"].ToString(), consultant[i].ToString().Trim(), txtreferal.Text.Trim(), "", "");
                        //thereq.UpdateChargeDetails(cocode, Session["YearCode"].ToString().Trim(), regno, "G", testdate, dt.Rows[i]["Cost"].ToString());
                    }
                    else if (testcd == "TC")
                    {
                        thereq.InsertRequisitionTestMap(cocode, "P", txtreqno.Text, dt.Rows[i]["TestId"].ToString(), generatetreq.Rows[0][0].ToString(), testdate, DeliveryDate, dt.Rows[i]["Time"].ToString(), dt.Rows[i]["Remarks"].ToString(), Session["userId"].ToString(), consultant[i].ToString().Trim(), txtreferal.Text.Trim(), "", "");
                        //thereq.UpdateChargeDetails(cocode, Session["YearCode"].ToString().Trim(), regno, "P", testdate, dt.Rows[i]["Cost"].ToString());
                    }
                    else if (testcd == "TX")
                    {
                        thereq.InsertRequisitionTestMap(cocode, "X", txtreqno.Text, dt.Rows[i]["TestId"].ToString(), generatetreq.Rows[0][0].ToString(), testdate, DeliveryDate, dt.Rows[i]["Time"].ToString(), dt.Rows[i]["Remarks"].ToString(), Session["userId"].ToString(), consultant[i].ToString().Trim(), txtreferal.Text.Trim(), "", "");
                        //thereq.UpdateChargeDetails(cocode, Session["YearCode"].ToString().Trim(), regno, "X", testdate, dt.Rows[i]["Cost"].ToString());
                    }
                    else
                    {
                        thereq.InsertRequisitionTestMap(cocode, "P", txtreqno.Text, dt.Rows[i]["TestId"].ToString(), generatetreq.Rows[0][0].ToString(), testdate, DeliveryDate, dt.Rows[i]["Time"].ToString(), dt.Rows[i]["Remarks"].ToString(), Session["userId"].ToString(), consultant[i].ToString().Trim(), txtreferal.Text.Trim(), "", "");
                        //thereq.UpdateChargeDetails(cocode, Session["YearCode"].ToString().Trim(), regno, "P", testdate, dt.Rows[i]["Cost"].ToString());
                    }
                }

            }
        }

        else
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "msg", "alert('Please Add or Update Test First !');", true);
        }

    }

    public string CheckUserId(string userID)
    {
        int keep;

        DataTable checkuserid = thepd.CheckUserId(Session["CoCode"].ToString().Trim(), userID);
        if (checkuserid.Rows.Count > 0)
        {
            keep = Convert.ToInt32(userID.Substring(6, 2)) + 1;
            userId1 = userID.Substring(0, 6) + keep.ToString();
            CheckUserId(userId1);
        }
        else
        {
            userId1 = userID;
        }


        return (userId1);

    }

    public void SendSMS(string No, string body)
    {
        UriBuilder urlBuilder = new UriBuilder();
        urlBuilder.Host = "127.0.0.1";
        urlBuilder.Port = 8800;

        string PhoneNumber = No;
        string message = body;


        urlBuilder.Query = string.Format("PhoneNumber=%2B" + PhoneNumber + "&Text=" + message);

        // HttpWebRequest httpReq = (HttpWebRequest)WebRequest.Create( new Uri(urlBuilder.ToString(), false));

        HttpWebRequest httpReq = (HttpWebRequest)WebRequest.Create("http://www.smsintegra.com/smsweb/desktop_sms/desktopsms.asp?uid=pkroy&pwd=123456&mobile=" + PhoneNumber + "&msg=" + message + "&sid=smsintegra&dtNow=16/02/2013");

        HttpWebResponse httpResponse = (HttpWebResponse)(httpReq.GetResponse());
        lblError.Text = "Message Sent Successfully .. ";
    }

    public string SendMail(string toList, string from, string ccList, string subject, string body)
    {

        MailMessage message = new MailMessage();
        string password = "gfc123456789";
        SmtpClient smtpClient = new SmtpClient();
        string msg = string.Empty;
        try
        {
            MailAddress fromAddress = new MailAddress(from);
            message.From = fromAddress;
            message.To.Add(toList);
            if (ccList != null && ccList != string.Empty)
                message.CC.Add(ccList);
            message.Subject = subject;
            message.IsBodyHtml = true;
            message.Body = body;
            smtpClient.Host = "smtp.gmail.com";   // We use gmail as our smtp client
            smtpClient.Port = 587;
            smtpClient.EnableSsl = true;
            smtpClient.UseDefaultCredentials = true;
            smtpClient.Credentials = new System.Net.NetworkCredential(from, password);
            smtpClient.Send(message);
            msg = "Successful<BR>";

        }
        catch (Exception ex)
        {
            msg = ex.Message;
        }
        return msg;
    }

    public void ResetAllFields()
    {
        txtreg.Text = "";
        txtage.Text = "";
        Button1.Enabled = true;
        Button1.Text = "Submit";
        txtreqno.Text = "";
        txtaddress2.Text = "";
        GenerateCode();
        txtname.Text = ""; txtreferal.Text = ""; txtaddress.Text = ""; txtph1.Text = ""; txtph2.Text = ""; txttestcode.Text = ""; txtdate.Text = "";
        txttestname.Text = "";
        txtdeldate.Text = "";
        txtadvamount.Text = ""; txtcost.Text = "0.00"; txtdueamount.Text = "0.00";
        cncldiv1.Visible = false;
        cncldiv2.Visible = false;
        divOpt.Visible = false;
    }

    protected void Button2_Click(object sender, EventArgs e)
    {
        ResetAllFields();
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

            GridView1.Rows[index].BackColor = Color.GreenYellow;

            Label lblregno = (Label)GridView1.Rows[index].FindControl("lblregno");
            txtreg.Text = lblregno.Text;


            Label lblreqno = (Label)GridView1.Rows[index].FindControl("lblreqno");
            txtreqno.Text = lblreqno.Text;
            TextBox4.Value = lblreqno.Text;
            Session["ReqNo"] = txtreqno.Text;

            //Label lblname = (Label)GridView1.Rows[index].FindControl("lblname");
            //txtname.Text = lblname.Text;

            //Label lblrefname = (Label)GridView1.Rows[index].FindControl("lblrefname");
            //txtreferal.Text = lblrefname.Text;

            //Label lbladdress = (Label)GridView1.Rows[index].FindControl("lbladdress");
            //txtaddress.Text = lbladdress.Text;

            //Label lblphone1 = (Label)GridView1.Rows[index].FindControl("lblphone1");
            //string[] ph1 = lblphone1.Text.Split(' ');
            //TextBox5.Text = ph1[0];
            //txtph1.Text = ph1[1];

            //Label lblphone2 = (Label)GridView1.Rows[index].FindControl("lblphone2");
            //string[] ph2 = lblphone1.Text.Split(' ');
            //TextBox1.Text = ph2[0];
            //txtph2.Text = ph2[1];


            //Label lbltestname = (Label)GridView1.Rows[index].FindControl("lbltestname");
            //Label lbltestdate = (Label)GridView1.Rows[index].FindControl("lbltestdate");
            //txtdate.Text = lbltestdate.Text;
            //Label lbldeldate = (Label)GridView1.Rows[index].FindControl("lbldeldate");
            //txtdeldate.Text = lbldeldate.Text;
            //Label lblamt = (Label)GridView1.Rows[index].FindControl("lblamt");
            ////txtadvamount.Text = lblamt.Text;
            //txtpreadvamount.Text = lblamt.Text;
            string regno = txtreg.Text.Trim();


            //DataTable dtdoc = thereq.GetDocName(Session["CoCode"].ToString().Trim(), regno);

            string testcode = ""; string testname = ""; string consultant = "";
            double testcost = 0.00;
            double dueamt = 0.00;
            string advamt = "";

            txtreg.Text = regno;
            DataTable dt = thedischarge.PatientDetailsForRequisition(regno, Session["CoCode"].ToString().Trim());
            txtname.Text = dt.Rows[0]["PName"].ToString();
            txtage.Text = dt.Rows[0]["Age"].ToString();
            txtaddress.Text = dt.Rows[0]["Address"].ToString();
            txtaddress2.Text = dt.Rows[0]["Address2"].ToString();
            txtadvamount.Text = dt.Rows[0]["AdvancedAmount"].ToString();
            txtGuardian.Text = dt.Rows[0]["GuadianName"].ToString();
            txtSpouse.Text = dt.Rows[0]["SpouseName"].ToString();
            //string[] ph1 = dt.Rows[0]["PhNo1"].ToString();
            //string[] ph2 = dt.Rows[0]["PhNo2"].ToString();
            txtph1.Text = dt.Rows[0]["PhNo1"].ToString();
            txtph2.Text = dt.Rows[0]["PhNo2"].ToString();
            TextBox2.Text = dt.Rows[0]["Address2"].ToString();
            if (dt.Rows.Count > 0)
            {

                DataTable dt1 = thereq.GetRequisitionDtls(Session["CoCode"].ToString().Trim(), Session["YearCode"].ToString().Trim(), txtreqno.Text, dt.Rows[0]["LedgerId"].ToString());
                if (dt1.Rows.Count > 0)
                {
                    for (int i = 0; i < dt1.Rows.Count; i++)
                    {
                        if (testcode == "" && testname == "")
                        {
                            testcode = dt1.Rows[i]["TestId"].ToString();
                            testname = dt1.Rows[i]["TestName"].ToString();

                        }
                        else
                        {
                            testcode = testcode + "," + dt1.Rows[i]["TestId"].ToString();
                            testname = testname + "," + dt1.Rows[i]["TestName"].ToString();

                        }
                        if (consultant == "")
                        {
                            consultant = dt1.Rows[i]["consultant"].ToString();
                        }
                        else
                        {
                            consultant = consultant + "," + dt1.Rows[i]["consultant"].ToString();
                        }
                        testcost = testcost + Convert.ToDouble(dt1.Rows[i]["Cost"]);
                    }
                    if (dt1.Rows[0]["Adamt"].ToString() == "")
                    {
                        advamt = "";
                    }
                    else
                    {
                        advamt = dt1.Rows[0]["Adamt"].ToString();
                    }
                    ddlDept.SelectedValue = dt1.Rows[0]["DeptCode"].ToString();
                    ddldocType.SelectedValue = dt1.Rows[0]["doctype"].ToString();

                    ddlDoctor.Items.Clear();
                    ddlDoctor.DataSource = thepatientAppo.DropdownConsultantDoc(Session["CoCode"].ToString().Trim(), ddldocType.SelectedValue.Trim());
                    ddlDoctor.DataTextField = "doc_name";
                    ddlDoctor.DataValueField = "doc_id";
                    ddlDoctor.DataBind();
                    this.ddlDoctor.Items.Insert(0, new ListItem("Select", "0"));
                    ddlDoctor.SelectedValue = dt1.Rows[0]["DocId"].ToString();

                    dueamt = testcost - (advamt == "" ? Convert.ToDouble("0.00") : Convert.ToDouble(advamt));
                    txttestname.Text = testname;
                    txttestcode.Text = testcode;
                    txtconsultant.Text = consultant;
                    txtcost.Text = testcost.ToString();

                    txtdate.Text = dt1.Rows[0]["TestDate"].ToString();
                    txttime.Text = dt1.Rows[0]["Time"].ToString();
                    txtdeldate.Text = dt1.Rows[0]["delDate"].ToString();
                    //   TextBox3.Text = dt1.Rows[0]["AdAmt"].ToString();
                    txtadvamount.Text = dt1.Rows[0]["AdAmt"].ToString();
                    //txtpreadvamount.Text = dt1.Rows[0]["adv_amt"].ToString();

                    txtreferal.Text = dt1.Rows[0]["ReferalName"].ToString();
                    txtreferalname.Text = dt1.Rows[0]["RefDocName"].ToString();
                    //txtunderdoc.Text = dtdoc.Rows[0]["doc_name"].ToString();


                    txtdueamount.Text = String.Format("{0:0.00}", dueamt);

                    //ddlPaymentMode.SelectedValue = dt1.Rows[0]["PaymentMode"].ToString();
                    //txtBankName.Text = dt1.Rows[0]["Bank_CardHolderName"].ToString();
                    //txtChequeNo.Text = dt1.Rows[0]["Chq_CardNo"].ToString();
                    //txtchqdt.Text = dt1.Rows[0]["CardExpYrMnth"].ToString();

                    if (dt1.Rows[0]["CancelFlag"].ToString().Trim() == "0")
                    {
                        chkCancel.Checked = false;
                    }
                    else
                    {
                        chkCancel.Checked = true;
                    }
                    txtcancelReason.Text = dt1.Rows[0]["CancelReason"].ToString().Trim();

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
                    hdnvchno.Value = dt1.Rows[0]["VchNo"].ToString();
                    ddlPaymentMode.Enabled = false;
                    cncldiv1.Visible = true;
                    cncldiv2.Visible = true;
                    Button1.Text = "update";
                }
            }

            Tab1Func();
            Button1.Text = "Update";
            divOpt.Visible = true;
        }
    }


    public void getRequisitionDetails(string regno, string reqno)
    {
        txtreg.Text = regno;


        txtreqno.Text = reqno;
        TextBox4.Value = reqno;
        Session["ReqNo"] = reqno;

        

        //DataTable dtdoc = thereq.GetDocName(Session["CoCode"].ToString().Trim(), regno);

        string testcode = ""; string testname = ""; string consultant = "";
        double testcost = 0.00;
        double dueamt = 0.00;
        string advamt = "";

        txtreg.Text = regno;
        DataTable dt = thedischarge.PatientDetailsForRequisition(regno, Session["CoCode"].ToString().Trim());
        txtname.Text = dt.Rows[0]["PName"].ToString();
        txtage.Text = dt.Rows[0]["Age"].ToString();
        txtaddress.Text = dt.Rows[0]["Address"].ToString();
        txtaddress2.Text = dt.Rows[0]["Address2"].ToString();
        txtadvamount.Text = dt.Rows[0]["AdvancedAmount"].ToString();
        txtGuardian.Text = dt.Rows[0]["GuadianName"].ToString();
        txtSpouse.Text = dt.Rows[0]["SpouseName"].ToString();
        //string[] ph1 = dt.Rows[0]["PhNo1"].ToString();
        //string[] ph2 = dt.Rows[0]["PhNo2"].ToString();
        txtph1.Text = dt.Rows[0]["PhNo1"].ToString();
        txtph2.Text = dt.Rows[0]["PhNo2"].ToString();
        TextBox2.Text = dt.Rows[0]["Address2"].ToString();
        if (dt.Rows.Count > 0)
        {

            DataTable dt1 = thereq.GetRequisitionDtls(Session["CoCode"].ToString().Trim(), Session["YearCode"].ToString().Trim(), txtreqno.Text, dt.Rows[0]["LedgerId"].ToString());
            if (dt1.Rows.Count > 0)
            {
                for (int i = 0; i < dt1.Rows.Count; i++)
                {
                    if (testcode == "" && testname == "")
                    {
                        testcode = dt1.Rows[i]["TestId"].ToString();
                        testname = dt1.Rows[i]["TestName"].ToString();

                    }
                    else
                    {
                        testcode = testcode + "," + dt1.Rows[i]["TestId"].ToString();
                        testname = testname + "," + dt1.Rows[i]["TestName"].ToString();

                    }
                    if (consultant == "")
                    {
                        consultant = dt1.Rows[i]["consultant"].ToString();
                    }
                    else
                    {
                        consultant = consultant + "," + dt1.Rows[i]["consultant"].ToString();
                    }
                    testcost = testcost + Convert.ToDouble(dt1.Rows[i]["Cost"]);
                }
                if (dt1.Rows[0]["Adamt"].ToString() == "")
                {
                    advamt = "";
                }
                else
                {
                    advamt = dt1.Rows[0]["Adamt"].ToString();
                }
                ddlDept.SelectedValue = dt1.Rows[0]["DeptCode"].ToString();
                ddldocType.SelectedValue = dt1.Rows[0]["doctype"].ToString();

                ddlDoctor.Items.Clear();
                ddlDoctor.DataSource = thepatientAppo.DropdownConsultantDoc(Session["CoCode"].ToString().Trim(), ddldocType.SelectedValue.Trim());
                ddlDoctor.DataTextField = "doc_name";
                ddlDoctor.DataValueField = "doc_id";
                ddlDoctor.DataBind();
                this.ddlDoctor.Items.Insert(0, new ListItem("Select", "0"));
                ddlDoctor.SelectedValue = dt1.Rows[0]["DocId"].ToString();

                dueamt = testcost - (advamt == "" ? Convert.ToDouble("0.00") : Convert.ToDouble(advamt));
                txttestname.Text = testname;
                txttestcode.Text = testcode;
                txtconsultant.Text = consultant;
                txtcost.Text = testcost.ToString();

                txtdate.Text = dt1.Rows[0]["TestDate"].ToString();
                txttime.Text = dt1.Rows[0]["Time"].ToString();
                txtdeldate.Text = dt1.Rows[0]["delDate"].ToString();
                //   TextBox3.Text = dt1.Rows[0]["AdAmt"].ToString();
                txtadvamount.Text = dt1.Rows[0]["AdAmt"].ToString();
                //txtpreadvamount.Text = dt1.Rows[0]["adv_amt"].ToString();

                txtreferal.Text = dt1.Rows[0]["ReferalName"].ToString();
                txtreferalname.Text = dt1.Rows[0]["RefDocName"].ToString();
                //txtunderdoc.Text = dtdoc.Rows[0]["doc_name"].ToString();

                txtPayableAmt.Text = dt1.Rows[0]["PayableAmt"].ToString();
                txtdueamount.Text = String.Format("{0:0.00}", dueamt);

                //ddlPaymentMode.SelectedValue = dt1.Rows[0]["PaymentMode"].ToString();
                //txtBankName.Text = dt1.Rows[0]["Bank_CardHolderName"].ToString();
                //txtChequeNo.Text = dt1.Rows[0]["Chq_CardNo"].ToString();
                //txtchqdt.Text = dt1.Rows[0]["CardExpYrMnth"].ToString();

                if (dt1.Rows[0]["CancelFlag"].ToString().Trim() == "0")
                {
                    chkCancel.Checked = false;
                }
                else
                {
                    chkCancel.Checked = true;
                }
                txtcancelReason.Text = dt1.Rows[0]["CancelReason"].ToString().Trim();

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
                hdnvchno.Value = dt1.Rows[0]["VchNo"].ToString();
                ddlPaymentMode.Enabled = false;
                cncldiv1.Visible = true;
                cncldiv2.Visible = true;
                Button1.Text = "update";
            }
        }

        Tab1Func();
        Button1.Text = "Update";
        divOpt.Visible = true;
    }

    protected void GridView1_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        string cocode = Session["CoCode"].ToString().Trim();
        string yearcode = Session["YearCode"].ToString().Trim();
        Label lblreqno = (Label)GridView1.Rows[e.RowIndex].FindControl("lblreqno");
        Label lblregno = (Label)GridView1.Rows[e.RowIndex].FindControl("lblregno");
        Label lblvchno = (Label)GridView1.Rows[e.RowIndex].FindControl("lblvchno");
        thereq.DeleteReq(cocode, yearcode, lblreqno.Text, lblvchno.Text);
        thereq.DeleteRequisition(cocode, yearcode, lblreqno.Text);
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
        if (RadioButtonList1.SelectedValue == "Y")
        {
            rpt.Append("<table width='100%' cellspacing=0 border=0 >");
            rpt.Append("<tr cellpadding:'0'  style='height:30px;'>");
            rpt.Append("<td width='30%'><img src='../Images/logo.jpg'  style=''/></td>");
            rpt.Append("<td   width='40%'  style='height:20px;font-family:Arial; font-size:medium; text-align:center'></td>");
            rpt.Append("<td width='30%' style='text-align:right'></td>");
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
    public void GetHearder_Detail()
    {
        System.Globalization.DateTimeFormatInfo dtf = new DateTimeFormatInfo();
        string date = DateTime.Now.ToString("dd/MM/yyyy");

        DataTable dt = thereq.GetRequisitionForReport(Session["CoCode"].ToString().Trim(), Session["YearCode"].ToString().Trim(), txtreqno.Text); //ds.Tables[0];
        //DataTable SlipSession = (DataTable)Session["SlipSession"];
        DataTable SlipSession = thedia.GetExistTestDetails(Session["CoCode"].ToString().Trim(), Session["YearCode"].ToString().Trim(), txtreqno.Text, txtcurtest.Value.ToString());
        if (SlipSession != null)
        {

            rpt.Append("<table width='100%' cellspacing=0 border=0 style='border-spacing: 0; padding: 0;font-family:verdana;'>");

            rpt.Append("<tr style='height:20px'>");
            rpt.Append("<td colspan='6' style='width: 8%;font-family:Verdana;font-size:small; text-align:right;'> Print Date : " + date + "</td>");
            rpt.Append("</tr'>");
            rpt.Append("<tr style='height:20px'>");
            rpt.Append("<td colspan='6' style='width: 8%;border-bottom: 1px solid black; font-family:Verdana;font-size:medium; text-align:Center;'> <b><u>Requisition Slip</u></b>  </td>");
            rpt.Append("</tr'>");
            rpt.Append("<tr style='height:20px'>");
            rpt.Append("<td colspan='6' style='width: 8%;border-bottom: 1px solid black;border-left:1px solid;border-right:1px solid; font-family:Verdana;font-size:small; text-align:Center;background-color:#9B9C8D'> PATIENT DETAILS  </td>");
            rpt.Append("</tr'>");


            rpt.Append("<tr style='height:30px'>");
            rpt.Append("<td style='width: 8% ;border-bottom: 1px solid black;border-left:1px solid;border-right: 1px solid black; font-family:Verdana; text-align:left'>Requisition No :</td>");
            rpt.AppendFormat("<td style='width: 5%;border-bottom: 1px solid black; border-right: 1px solid black; font-family:Verdana; text-align:left'>{0}</td>", dt.Rows[0]["RequisitionNo"]);
            rpt.Append("<td style='width: 8%;border-bottom: 1px solid black; border-right: 1px solid black; font-family:Verdana;  text-align:left'>Service Date</td>");
            rpt.AppendFormat("<td style='width: 5%;border-bottom: 1px solid black; border-right: 1px solid black; font-family:Verdana; text-align:left'>{0}</td>", /*dt.Rows[0]["TDate"]*/ SlipSession.Rows[0]["Date"]);
            rpt.Append("<td style='width: 8%;border-bottom: 1px solid black;border-right: 1px solid black; font-family:Verdana;  text-align:left'>Delivery Date</td>");
            rpt.AppendFormat("<td style='width: 5%;border-bottom: 1px solid black;border-right: 1px solid black;font-family:Verdana; text-align:left'>{0}</td>", /*dt.Rows[0]["delDate"]*/ SlipSession.Rows[0]["DeliveryDate"]);
            rpt.Append("</tr>");

            rpt.Append("<tr style='height:30px'>");
            rpt.Append("<td style='width: 8%;border-bottom: 1px solid black;border-left:1px solid;border-right: 1px solid black; font-family:Verdana;  text-align:left'>Reg. No :</td>");
            rpt.AppendFormat("<td style='width: 5%;border-bottom: 1px solid black;border-right: 1px solid black; font-family:Verdana; text-align:left'>{0}</td>", dt.Rows[0]["RegistrationNo"]);
            rpt.Append("<td style='width: 8%;border-bottom: 1px solid black;border-right: 1px solid black; font-family:Verdana;  text-align:left'>Name & Age : </td>");
            rpt.AppendFormat("<td style='width: 5%;border-bottom: 1px solid black;border-right: 1px solid black;font-family:Verdana; text-align:left'>{0}</td>", dt.Rows[0]["PatientName"].ToString() + ", " + dt.Rows[0]["age"].ToString());
            rpt.Append("<td style='width: 8%;border-bottom: 1px solid black;border-right: 1px solid black; font-family:Verdana;  text-align:left'>Contact No : </td>");
            rpt.AppendFormat("<td style='width: 5%;border-bottom: 1px solid black;border-right: 1px solid black;font-family:Verdana; text-align:left'>{0}</td>", dt.Rows[0]["Ph1"]);
            rpt.Append("</tr>");

            rpt.Append("<tr style='height:30px'>");
            rpt.Append("<td style='width: 8%;border-right: 1px solid black;border-left:1px solid;border-bottom: 1px solid black; font-family:Verdana;  text-align:left'>Address :</td>");
            rpt.AppendFormat("<td colspan=3 style='width: 5%;border-bottom: 1px solid black;font-family:Verdana; text-align:left;border-right: 1px solid black; '>{0}</td>", dt.Rows[0]["Address"]);
            rpt.Append("<td style='width: 8%;border-right: 1px solid black;border-bottom: 1px solid black; font-family:Verdana;  text-align:left'>Refer By</td>");
            rpt.AppendFormat("<td  style='width: 5%;border-right: 1px solid black;border-bottom: 1px solid black;font-family:Verdana; text-align:left'>{0}</td>", dt.Rows[0]["ReferalName"]);




            rpt.Append("</tr >");
            rpt.Append("</table>");



            rpt.Append("<br/>");
            rpt.Append("<center>");
            rpt.Append("<table  width='100%' cellspacing=0 border=0 style='border:1px solid black;border-spacing: 0; padding: 0;font-family:verdana;'>");

            rpt.Append("<tr style='height:20px'>");
            rpt.Append("<td colspan='6' style='width: 8%;border-bottom: 1px solid black;border-top: 1px solid black; font-family:Verdana;font-size:small; text-align:Center;background-color:#9B9C8D'> SERVICE DETAILS  </td>");
            rpt.Append("</tr'>");

            rpt.Append("<tr style='height:30px'>");
            rpt.Append("<td style='width:15%;border-bottom: 1px solid black;border-right: 1px solid black; font-family:Verdana;text-align:left'>Service Group</td>");
            rpt.Append("<td style='width:35%;border-bottom: 1px solid black;border-right: 1px solid black; font-family:Verdana;  text-align:left'>Service Name</td>");
            rpt.Append("<td style='width:15%;border-bottom: 1px solid black;border-right: 1px solid black; font-family:Verdana;  text-align:left'>Consultant Doctor</td>");
            rpt.Append("<td style='width:10%;border-bottom: 1px solid black;font-family:Verdana; text-align:right'>Charge</td>");
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
                rpt.AppendFormat("<td style='border-bottom: 1px solid black;border-right: 1px solid black; font-family:Verdana; text-align:left'>{0}</td>", dep);
                rpt.AppendFormat("<td style='border-bottom: 1px solid black;border-right: 1px solid black; font-family:Verdana;text-align:left'>{0}</td>", SlipSession.Rows[i]["TestName"].ToString());
                rpt.AppendFormat("<td style='border-bottom: 1px solid black;border-right: 1px solid black; font-family:Verdana;text-align:left'>{0}</td>", SlipSession.Rows[i]["consultantname"].ToString());
                rpt.AppendFormat("<td style='border-bottom: 1px solid black;font-family:Verdana; text-align:right'> {0}</td>", SlipSession.Rows[i]["cost"].ToString());
                rpt.Append("</tr >");
                total = total + Convert.ToDouble(SlipSession.Rows[i]["cost"]);
            }

            rpt.Append("<tr style='height:30px;'>");
            rpt.Append("<td colspan=3 style='border-right: 1px solid black; font-family:Verdana; text-align:left'>Total Charges</td>");
            rpt.AppendFormat("<td style='font-family:Verdana;text-align:right'>{0}</td>", total.ToString("F"));
            rpt.Append("</tr>");

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

    protected void Button5_Click(object sender, EventArgs e)
    {
        Fill();
    }

    public void FillPatientDetails()
    {
        string regno = txtreg.Text;
        txtregSrch.Text = Session["RegNo"].ToString();
        DataTable CheckReq = thereq.GetRequisitionNo(Session["CoCode"].ToString().Trim(), Session["YearCode"].ToString().Trim(), regno);

        DataTable dtdoc = thereq.GetDocName(Session["CoCode"].ToString().Trim(), regno);

        string testcode = ""; string testname = "";
        double testcost = 0.00;
        double dueamt = 0.00;
        string advamt = "";
        string recno = CheckReq.Rows[0]["RequisitionNo"].ToString();
        txtreg.Text = regno;


        DataTable dt = thedischarge.PatientDetailsForRequisition(regno, Session["CoCode"].ToString().Trim());


        testcost = 0.00;
        //txtcost.Text = testcost.ToString();
        txtdueamount.Text = "0.00";
        GenerateCode();

        txtname.Text = dt.Rows[0]["PName"].ToString();
        //txtunderdoc.Text = dt.Rows[0]["doc_name"].ToString();
        ddlDoctor.SelectedValue = dt.Rows[0]["DocId"].ToString();
        //txtreferal.Text = dt.Rows[0]["doc_name"].ToString();
        txtage.Text = dt.Rows[0]["Age"].ToString();
        txtaddress.Text = dt.Rows[0]["Address"].ToString();
        txtaddress2.Text = dt.Rows[0]["Address2"].ToString();
        txtSpouse.Text = dt.Rows[0]["SpouseName"].ToString();
        txtGuardian.Text = dt.Rows[0]["GuadianName"].ToString();
        txtadvamount.Text = dt.Rows[0]["AdvancedAmount"].ToString();

        txtph1.Text = dt.Rows[0]["PhNo1"].ToString();
        txtph2.Text = dt.Rows[0]["PhNo2"].ToString();
        TextBox2.Text = dt.Rows[0]["Address2"].ToString();
        //txttestcode.Text = "";
        //txttestname.Text = "";

        DataTable dt11 = thereq.GridFillFetch(Session["CoCode"].ToString().Trim(), regno);
        CallNew(dt11);
        Session["ReqNo"] = txtreqno.Text;
    }
    public void Fill()
    {
        string regno = Request.Form[txtreg.UniqueID];
        DataTable CheckReq = thereq.GetRequisitionNo(Session["CoCode"].ToString().Trim(), Session["YearCode"].ToString().Trim(), regno);

        DataTable dtdoc = thereq.GetDocName(Session["CoCode"].ToString().Trim(), regno);

        string testcode = ""; string testname = "";
        double testcost = 0.00;
        double dueamt = 0.00;
        string advamt = "";
        string recno = CheckReq.Rows[0]["RequisitionNo"].ToString();
        //DataSet ds = thedischarge.PatientForReport(regno, Session["CoCode"].ToString().Trim());
        txtreg.Text = regno;

        //ddlDoctor.Items.Clear();
        //ddlDoctor.DataSource = thepatientAppo.DropdownDocDeptWise(Session["CoCode"].ToString().Trim(), "0");
        //ddlDoctor.DataTextField = "doc_name";
        //ddlDoctor.DataValueField = "doc_id";
        //ddlDoctor.DataBind();
        //this.ddlDoctor.Items.Insert(0, new ListItem("Select", "0"));

        DataTable dt = thedischarge.PatientDetailsForRequisition(regno, Session["CoCode"].ToString().Trim());
        //if (CheckReq.Rows.Count > 0 && recno != "0")
        //{
        //    txtreqno.Text = CheckReq.Rows[0][0].ToString();
        //    if (dtdoc.Rows.Count > 0)
        //    {
        //        txtunderdoc.Text = dtdoc.Rows[0]["doc_name"].ToString();
        //    }
        //    else
        //    {
        //        txtunderdoc.Text = "";
        //    }

        //    DataTable dt1 = thereq.GetRequisitionDtls(Session["CoCode"].ToString().Trim(), Session["YearCode"].ToString().Trim(), txtreqno.Text, dt.Rows[0]["LedgerId"].ToString());
        //    if (dt1.Rows.Count > 0)
        //    {
        //        for (int i = 0; i < dt1.Rows.Count; i++)
        //        {
        //            if (testcode == "" && testname == "")
        //            {
        //                testcode = dt1.Rows[i]["TestId"].ToString();
        //                testname = dt1.Rows[i]["TestName"].ToString();
        //            }
        //            else
        //            {
        //                testcode = testcode + "," + dt1.Rows[i]["TestId"].ToString();
        //                testname = testname + "," + dt1.Rows[i]["TestName"].ToString();
        //            }
        //            testcost = testcost + Convert.ToDouble(dt1.Rows[i]["Cost"]);
        //        }
        //        if (dt1.Rows[0]["adv_amt"].ToString() == "")
        //        {
        //            advamt = "";
        //        }
        //        else
        //        {
        //            advamt = dt1.Rows[0]["adv_amt"].ToString();
        //        }
        //        dueamt = testcost - (advamt == "" ? Convert.ToDouble("0.00") : Convert.ToDouble(advamt));
        //        txttestname.Text = testname;
        //        txttestcode.Text = testcode;
        //        txtcost.Text = testcost.ToString();

        //        txtdate.Text = dt1.Rows[0]["TDate"].ToString();
        //        txtdeldate.Text = dt1.Rows[0]["delDate"].ToString();
        //        //   TextBox3.Text = dt1.Rows[0]["AdAmt"].ToString();
        //        //txtadvamount.Text = dt1.Rows[0]["AdAmt"].ToString();
        //        txtpreadvamount.Text = dt1.Rows[0]["adv_amt"].ToString();

        //        txtreferal.Text = dt1.Rows[0]["ReferalName"].ToString();
        //        //txtunderdoc.Text = dtdoc.Rows[0]["doc_name"].ToString();


        //        txtdueamount.Text = String.Format("{0:0.00}", dueamt);
        //        Button1.Text = "update";
        //    }
        //}
        //else
        //{
        //    testcost = 0.00;
        //    txtcost.Text = testcost.ToString();
        //    txtdueamount.Text = "0.00";
        //    GenerateCode();
        //   // DataTable dtdoc = thereq.GetDocName(Session["CoCode"].ToString().Trim(), txtreg.Text);

        //    txtname.Text = dt.Rows[0]["PName"].ToString();
        //    txtunderdoc.Text = dt.Rows[0]["doc_name"].ToString();
        //    txtreferal.Text = dt.Rows[0]["doc_name"].ToString();
        //    txtage.Text = dt.Rows[0]["Age"].ToString();
        //    txtaddress.Text = dt.Rows[0]["Address"].ToString();
        //    txtaddress2.Text = dt.Rows[0]["Address2"].ToString();
        //    txtadvamount.Text = dt.Rows[0]["AdvancedAmount"].ToString();
        //    string[] ph1 = dt.Rows[0]["PhNo1"].ToString().Split(' ');
        //    string[] ph2 = dt.Rows[0]["PhNo2"].ToString().Split(' ');
        //    txtph1.Text = ph1[1];
        //    txtph2.Text = ph2[1];
        //    TextBox2.Text = dt.Rows[0]["Address2"].ToString();
        //    txttestcode.Text = "";
        //    txttestname.Text = "";

        //    //if (dtdoc.Rows.Count > 0)
        //    //{

        //    //}
        //    //else
        //    //{
        //    //    txtunderdoc.Text = "";
        //    //    txtreferal.Text = "";
        //    //}
        //}

        testcost = 0.00;
        txtcost.Text = testcost.ToString();
        txtdueamount.Text = "0.00";
        GenerateCode();
        // DataTable dtdoc = thereq.GetDocName(Session["CoCode"].ToString().Trim(), txtreg.Text);

        txtname.Text = dt.Rows[0]["PName"].ToString();
        //txtunderdoc.Text = dt.Rows[0]["doc_name"].ToString();
        ddlDoctor.SelectedValue = dt.Rows[0]["DocId"].ToString();
        //txtreferal.Text = dt.Rows[0]["doc_name"].ToString();
        txtage.Text = dt.Rows[0]["Age"].ToString();
        txtaddress.Text = dt.Rows[0]["Address"].ToString();
        txtaddress2.Text = dt.Rows[0]["Address2"].ToString();
        txtSpouse.Text = dt.Rows[0]["SpouseName"].ToString();
        txtGuardian.Text = dt.Rows[0]["GuadianName"].ToString();
        txtadvamount.Text = dt.Rows[0]["AdvancedAmount"].ToString();

        txtph1.Text = dt.Rows[0]["PhNo1"].ToString();
        txtph2.Text = dt.Rows[0]["PhNo2"].ToString();
        TextBox2.Text = dt.Rows[0]["Address2"].ToString();
        txttestcode.Text = "";
        txttestname.Text = "";

        DataTable dt11 = thereq.GridFillFetch(Session["CoCode"].ToString().Trim(), regno);
        CallNew(dt11);
        Session["ReqNo"] = txtreqno.Text;
    }

    public void CallNew(DataTable dt)
    {
        // GenerateCode();
        txtname.Text = dt.Rows[0]["PName"].ToString();
        txtaddress.Text = dt.Rows[0]["Address"].ToString();
        txtage.Text = dt.Rows[0]["age"].ToString();
        txtaddress2.Text = dt.Rows[0]["Address2"].ToString();
        //string[] ph1 = dt.Rows[0]["PhNo1"].ToString().Split(' ');
        //TextBox5.Text = ph1[0];
        txtph1.Text = dt.Rows[0]["PhNo1"].ToString();
        //string[] ph2 = dt.Rows[0]["PhNo2"].ToString().Split(' ');
        //TextBox1.Text = ph2[0];
        txtph2.Text = dt.Rows[0]["PhNo2"].ToString();


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
    protected void Button8_Click(object sender, EventArgs e)
    {
        Session["TestType"] = "C";
        Session["PrvVal"] = txtcost.Text;
        Session["PrvDue"] = txtdueamount.Text;
        Session["TestGrp"] = ddltestGroup.SelectedValue.Trim();
        ScriptManager.RegisterStartupScript(this, GetType(), "ShowDialog1", "ShowDialog1();", true);
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
                cmd.CommandText = "select 'Self~Self' as Name Union select distinct doc_id+'~'+doc_name as Name from GN_DoctorMaster where compcode=@Compcode and doc_name like @SearchText +'%'";
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

    protected void ddlPaymentMode_SelectedIndexChanged(object sender, EventArgs e)
    {

        string regno = Request.Form[txtreg.UniqueID];
        string testcode = Request.Form[txttestcode.UniqueID];
        string testname = Request.Form[txttestname.UniqueID];
        string cost = Request.Form[txtcost.UniqueID];

        txttestcode.Text = testcode;
        txttestname.Text = testname;
        txtcost.Text = cost;
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
    protected void ddlDept_SelectedIndexChanged(object sender, EventArgs e)
    {
        ddlDoctor.Items.Clear();
        ddlDoctor.DataSource = thepatientAppo.DropdownDoc(Session["CoCode"].ToString().Trim(), ddlDept.SelectedValue.Trim());
        ddlDoctor.DataTextField = "doc_name";
        ddlDoctor.DataValueField = "doc_id";
        ddlDoctor.DataBind();
        this.ddlDoctor.Items.Insert(0, new ListItem("Select", "0"));
    }
    protected void ddldocType_SelectedIndexChanged(object sender, EventArgs e)
    {
        string testcodes = Request.Form[txttestcode.UniqueID];
        string testnames = Request.Form[txttestname.UniqueID];
        string testcosts = Request.Form[txtcost.UniqueID];
        string Performingdocscodes = Request.Form[txtconsultant.UniqueID];
        string Performingdocsnamess = Request.Form[txtconsultantname.UniqueID];
        string PayableAmt = Request.Form[txtPayableAmt.UniqueID];

        txttestcode.Text = testcodes;
        txttestname.Text = testnames;
        txtcost.Text = testcosts;
        txtconsultant.Text = Performingdocscodes;
        txtconsultantname.Text = Performingdocsnamess;
        txtPayableAmt.Text = PayableAmt;

        ddlDoctor.Items.Clear();
        ddlDoctor.DataSource = thepatientAppo.DropdownDoc(Session["CoCode"].ToString().Trim(), ddldocType.SelectedValue.Trim());
        ddlDoctor.DataTextField = "doc_name";
        ddlDoctor.DataValueField = "doc_id";
        ddlDoctor.DataBind();
        this.ddlDoctor.Items.Insert(0, new ListItem("Select", "0"));
    }
    protected void Button4_Click(object sender, EventArgs e)
    {
        string someScript = "";
        someScript = "<script language='javascript'> var el = document.getElementById('h1');el.style.display = 'none';</script>";
        Page.ClientScript.RegisterStartupScript(this.GetType(), "onload", someScript);

        GenPrescription();
        Button7.Visible = true;
        Button6.Visible = true;
    }

    private void GenPrescription()
    {
        //Report_Header();
        //GetHearder_Detail();
        GenPrescription_Detail();
        ltrReport.Text = rpt.ToString();
    }

    private void GenPrescription_Detail()
    {
        DataTable dtDoc = thepatientAppo.DropdownConsultantDoc(Session["CoCode"].ToString().Trim(), "0");
        string docList = "";
        for (int i = 0; i < dtDoc.Rows.Count; i++)
        {
            if (i == 0)
            {
                docList = "Dr. " + dtDoc.Rows[i]["doc_name"].ToString();
            }
            else
            {
                docList = docList +"<br>"+"Dr. " + dtDoc.Rows[i]["doc_name"].ToString();
            }
        }
        DataTable dt = thereq.GetRequisitionForReport(Session["CoCode"].ToString().Trim(), Session["YearCode"].ToString().Trim(), txtreqno.Text);

        DataTable SlipSession = thedia.GetExistTestDetails(Session["CoCode"].ToString().Trim(), Session["YearCode"].ToString().Trim(), txtreqno.Text, txtcurtest.Value.ToString());

        System.Globalization.DateTimeFormatInfo dtf = new DateTimeFormatInfo();
        string date = DateTime.Now.ToString("dd/MM/yyyy");
        if (RadioButtonList1.SelectedValue == "Y")
        {
            rpt.Append("<table width='100%' cellspacing=0 border=0 >");
            rpt.Append("<tr cellpadding:'0'  style='height:30px;'>");
            rpt.Append("<td width='30%' rowspan=2><img src='../Images/logo.jpg'  style=''/></td>");
            rpt.AppendFormat("<td style='width: 70%;font-family:Verdana;font-size:large; text-align:right;'><i>{0}</i></td>", "Team Ankuran");
            rpt.Append("</tr>");
            rpt.Append("<tr cellpadding:'0'  style='height:30px;'>");
            rpt.AppendFormat("<td style='width: 70%;font-family:Verdana; text-align:right;'>{0}</td>", docList);
            rpt.Append("</tr>");
            rpt.Append("<tr cellpadding:'0'  style='height:30px;'>");
            rpt.Append("<td colspan=2 width='100%' style='text-align:right'>Print Date : " + date + "</td>");
            rpt.Append("</tr>");
            rpt.Append("</table>");
        }
        else
        {
            rpt.Append("<table width='100%' cellspacing=0 border=0 >");
            rpt.Append("<tr>");
            rpt.Append("<td width='100%' style='height:150px;'></td>");
            rpt.Append("</tr>");
            rpt.Append("</table>");
        }

        if (SlipSession != null)
        {
            rpt.Append("<table width='100%' cellspacing=0 border=0 style='border:1px solid black;border-spacing: 0; padding: 0;font-family:verdana;font-size:x-small'>");

            rpt.Append("<tr>");
            rpt.Append("<td style='width: 15%;font-family:Verdana;  text-align:left'>PATIENT ID </td>");
            rpt.AppendFormat("<td style='width: 35%;font-family:Verdana; text-align:left'>{0}</td>", ":  " + dt.Rows[0]["RegistrationNo"]).ToString().ToUpper();
            rpt.Append("<td style='width: 15%;font-family:Verdana;  text-align:left'>CONSULTANT  </td>");
            rpt.AppendFormat("<td style='width: 35%;font-family:Verdana; text-align:left'>{0}</td>", ":  " + SlipSession.Rows[0]["consultantname"].ToString().ToUpper());
            rpt.Append("</tr >");

            rpt.Append("<tr>");
            rpt.Append("<td style='font-family:Verdana;  text-align:left'>PATIENT NAME </td>");
            rpt.AppendFormat("<td style='font-family:Verdana; text-align:left'>{0}</td>", ":  " + dt.Rows[0]["PatientName"].ToString().ToUpper());
            rpt.Append("<td style='font-family:Verdana;  text-align:left'>&nbsp; </td>");
            rpt.AppendFormat("<td style='font-family:Verdana; text-align:left'>{0}</td>", ":  " + SlipSession.Rows[0]["DocQualification"].ToString().ToUpper());
            rpt.Append("</tr >");

            rpt.Append("<tr>");
            rpt.Append("<td style='font-family:Verdana;  text-align:left'>AGE/ GENDER </td>");
            rpt.AppendFormat("<td style='font-family:Verdana; text-align:left'>{0}</td>", ":  " + dt.Rows[0]["age"] + "/" + dt.Rows[0]["Gender"].ToString().ToUpper());
            rpt.Append("<td style='font-family:Verdana;  text-align:left'>DEPARTMENT </td>");
            rpt.AppendFormat("<td style='font-family:Verdana; text-align:left'>{0}</td>", ":  " + SlipSession.Rows[0]["DocType"].ToString().ToUpper());
            rpt.Append("</tr >");

            rpt.Append("<tr>");
            rpt.Append("<td style='font-family:Verdana;  text-align:left'>CONTACT NO </td>");
            rpt.AppendFormat("<td style='font-family:Verdana; text-align:left'>{0}</td>", ":  " + dt.Rows[0]["Ph1"]);
            rpt.Append("<td style='font-family:Verdana;  text-align:left'>REG. NO  </td>");
            rpt.AppendFormat("<td style='font-family:Verdana; text-align:left'>{0}</td>", ":  " + SlipSession.Rows[0]["DocRegNo"].ToString().ToUpper());
            rpt.Append("</tr >");

            rpt.Append("<tr>");
            rpt.Append("<td style='font-family:Verdana;  text-align:left'>FULL ADDRESS </td>");
            rpt.AppendFormat("<td style='font-family:Verdana; text-align:left'>{0}</td>", ":  " + dt.Rows[0]["Address"].ToString().ToUpper());
            rpt.Append("<td style='font-family:Verdana;  text-align:left'>Date  </td>");
            rpt.AppendFormat("<td style='font-family:Verdana; text-align:left'>{0}</td>", ":  " + SlipSession.Rows[0]["Date"].ToString().ToUpper());
            rpt.Append("</tr >");

            rpt.Append("</table>");

            /*rpt.Append("<table width='100%' cellspacing=0 border=0 style='border:1px solid black;border-spacing: 0; padding: 0;font-family:verdana;margin-top:30px;'>");
            rpt.Append("<tr style='height:30px'>");
            rpt.Append("<td style='width: 75%;border-right:1px solid;'></td>");
            rpt.Append("<td style='width: 25%;'></td>");
            rpt.Append("</tr >");
            rpt.Append("</table>");

            rpt.Append("<table width='100%' cellspacing=0 border=0 style='border-left:1px solid black;border-right:1px solid black;border-bottom:1px solid black;border-spacing: 0; padding: 0;font-family:verdana;'>");
            rpt.Append("<tr style='height:40px'>");
            rpt.Append("<td style='width: 75%;border-right:1px solid;' valign='bottom'><span style='font-size:x-large;'>R</span><span style='font-size:xx-small;font-weight:bold;'>X</span></td>");
            rpt.Append("<td style='width: 25%;'></td>");
            rpt.Append("</tr >");
            rpt.Append("</table>");*/
            rpt.Append("</center>");
            Session["SlipSession"] = null;
            ltrReport.Visible = true;
        }
    }
    protected void Button9_Click(object sender, EventArgs e)
    {
        Session["BillRegNo"] = txtreg.Text.Trim();
        Session["BillReqNo"] = txtreqno.Text.Trim();
        Session["BillPtName"] = txtname.Text.Trim();
        Session["BillAmt"] = txtcost.Text.Trim();
        Session["ReportingAmt"] = txtReptAmt.Text.Trim();
        Session["BillDiscAmt"] = txtDiscAmt.Text.Trim();
        Session["BillPayableAmt"] = txtPayableAmt.Text.Trim();
        Session["BillType"] = "OPD";
        Session["BillDate"] = txtdate.Text;
        Response.Redirect("../Pathology/RequisitionBill.aspx");
    }

    protected void Button10_Click(object sender, EventArgs e)
    {
        GridFill();
    }
    protected void ddlDoctor_SelectedIndexChanged(object sender, EventArgs e)
    {
        string testcodes = Request.Form[txttestcode.UniqueID];
        string testnames = Request.Form[txttestname.UniqueID];
        string testcosts = Request.Form[txtcost.UniqueID];
        string Performingdocscodes = Request.Form[txtconsultant.UniqueID];
        string Performingdocsnamess = Request.Form[txtconsultantname.UniqueID];
        string PayableAmt = Request.Form[txtPayableAmt.UniqueID];

        txttestcode.Text = testcodes;
        txttestname.Text = testnames;
        txtcost.Text = testcosts;
        txtconsultant.Text = Performingdocscodes;
        txtconsultantname.Text = Performingdocsnamess;
        txtPayableAmt.Text = PayableAmt;
        DataTable dt = thepatientAppo.getConsultationCharges(Session["CoCode"].ToString().Trim(), ddlDoctor.SelectedValue.Trim());
        txtPayableAmt.Text = dt.Rows[0]["ConsultationChrg"].ToString();
        txtcost.Text = dt.Rows[0]["ConsultationChrg"].ToString();
    }
    protected void ddltestCode_SelectedIndexChanged(object sender, EventArgs e)
    {
        DataTable dtTest = thereq.getTest(Session["CoCode"].ToString().Trim(), ddltestGroup.SelectedValue.Trim(), ddltestCode.SelectedValue.Trim());
        //txttestcode.Text = dtTest.Rows[0]["TestId"].ToString();
        //txttestname.Text = dtTest.Rows[0]["TestName"].ToString();
        //txtcost.Text = dtTest.Rows[0]["Cost"].ToString();
        //txtPayableAmt.Text = dtTest.Rows[0]["Cost"].ToString();
        string regno = Request.Form[txtreg.UniqueID];
        string testcodes = Request.Form[txttestcode.UniqueID];
        string PayableAmt = Request.Form[txtPayableAmt.UniqueID];
        string testcost = Request.Form[txtcost.UniqueID];
        txttestcode.Text = testcodes;
        txtcost.Text = testcost;
        txtPayableAmt.Text = PayableAmt;

        txttestcode.Text = dtTest.Rows[0]["TestId"].ToString();
        txtcost.Text = dtTest.Rows[0]["Cost"].ToString();
        txtPayableAmt.Text = dtTest.Rows[0]["Cost"].ToString();
    }
}