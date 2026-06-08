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
public partial class DayCare_PatientReg : System.Web.UI.Page
{
    Main Objmain = new Main(ConfigurationSettings.AppSettings["ConnectionString"].ToString());
    DC_PatientReg thereg = new DC_PatientReg(ConfigurationSettings.AppSettings["ConnectionString"].ToString());
    PatientAdmission thepd = new PatientAdmission(ConfigurationSettings.AppSettings["ConnectionString"].ToString());
    static string userId1 = "";
    static double charge;
    protected void Page_Load(object sender, EventArgs e)
    {
        
        Page.Title = "Patient Registration";
        if (Session["userName"] == null)
        {
            Response.Redirect("../LoginPage.aspx");
        }
        if (Objmain.checkaccss(Session["CoCode"].ToString(), Session["UserRoleID"].ToString().Trim(), "PATIENT REGISTRATION", checkAccessType.ViewAction) == false)
        {
            Response.Redirect("../AccessDenied.aspx");
        }

        if (Objmain.checkaccss(Session["CoCode"].ToString(), Session["UserRoleID"].ToString().Trim(), "PATIENT REGISTRATION", checkAccessType.InsertAction) == false)
        {
            Button1.Enabled = false;
        }
        
        if (!IsPostBack)
        {

            txtreg.Text = thereg.GenerateRegNo(Session["CoCode"].ToString().Trim(), Session["YearCode"].ToString().Trim());
            DropDownFill();
            Button6.Enabled = false;

             if (Session["AppNo"] != null)
                {
                    HiddenField1.Value = Session["AppNo"].ToString();
                    DataTable dt = thereg.GetList(Session["CoCode"].ToString().Trim(), Session["AppNo"].ToString());
                    if (dt.Rows[0]["PatientReg"].ToString() != "")
                    {
                        txtreg.Text = dt.Rows[0]["PatientReg"].ToString();
                        FillDetails();
                    }
                    else
                    {
                        FillDtls(dt);
                    }
                }
            GridFill();
        }

        Page.Title = "Patient Registration";
        Session["AppNo"] = null;
   }

    public void FillDtls(DataTable dt)
    {
        string fullName = dt.Rows[0]["PName"].ToString();
        string[] names = fullName.Split(' ');
        string firstName = names[0];
        string lastName = names[1];
        txtname.Text = firstName;
        txtlname.Text = lastName;
        this.txtvillage.Text = dt.Rows[0]["PAddress"].ToString();
        string[] ph1 = dt.Rows[0]["PhNo1"].ToString().Split(' ');
        TextBox9.Text = "+91";
        TextBox17.Text = "+91";
        this.txtcontact1.Text = ph1[1];
        string []ph2=dt.Rows[0]["PhNo2"].ToString().Split(' ');
        if (ph2.Length > 1)
            this.txtcontact2.Text = ph2[1];
    }
    private void DropDownFill()
    {
        this.ddldialysertype.DataSource = thereg.DropdownDialysertype(Session["CoCode"].ToString().Trim());
        this.ddldialysertype.DataTextField = "TypeName";
        this.ddldialysertype.DataValueField = "TypeId";
        this.ddldialysertype.DataBind();
        this.ddldialysertype.Items.Insert(0, new ListItem("--Select--", "0"));


        this.DropDownList1.DataSource = thereg.DropdownDistrict(Session["CoCode"].ToString().Trim());
        this.DropDownList1.DataTextField = "DistrictName";
        this.DropDownList1.DataValueField = "ID";
        this.DropDownList1.DataBind();
        this.DropDownList1.Items.Insert(0, new ListItem("--Select--", "0"));


        this.ddlstate.DataSource = thereg.DropdownState(Session["CoCode"].ToString().Trim());
        this.ddlstate.DataTextField = "State_Name";
        this.ddlstate.DataValueField = "State_ID";
        this.ddlstate.DataBind();
        this.ddlstate.Items.Insert(0, new ListItem("--Select--", "0"));

        this.rblsex.Items.Clear();
        this.rblsex.DataSource = thereg.GetGender(Session["CoCode"].ToString().Trim());
        this.rblsex.DataTextField = "SexName";
        this.rblsex.DataValueField = "ID";
        this.rblsex.DataBind();
        this.rblsex.Items.Insert(0, new ListItem("--Select--", "0"));
    }
    public void GridFill()
    {
        GridView1.DataSource = thereg.GridFill(Session["CoCode"].ToString().Trim(), txtreg.Text);
        GridView1.DataBind();
    }
    protected void Button1_Click(object sender, EventArgs e)
    {
        string DiaId="";
         string userID = "";
        string body = "";
        string Encriptpassword = "";
        
        string name = txtname.Text + " " + txtlname.Text;
      
        string ph1 = TextBox9.Text + " " + txtcontact1.Text;
        string ph2;
        if (txtcontact2.Text != "")
            ph2 = TextBox17.Text + " " + txtcontact2.Text;
        else
            ph2 = "";
        DiaId = HiddenField3.Value;
        if (DiaId == "" || DiaId == "0")
            DiaId = ddldialysername.SelectedValue;
        if (Button1.Text == "Submit")
        {
            
            
            if (TextBox1.Text != "")
            {

                string mailid = TextBox1.Text;

                string first = "";
                string second = "";
                int indexOfSpace = TextBox2.Text.IndexOf(' ');
                string[] name1;
                if (indexOfSpace > 0)
                {
                    name1 = TextBox2.Text.ToLower().Split(' ');


                    if (name1[0].Length < 3)
                        first = name1[0].Substring(0, name1[0].Length);
                    else
                        first = name1[0].Substring(0, 3);
                    if (name.Length > 1)
                    {
                        if (name1[1].Length < 3)
                            second = name1[1].Substring(0, name1[1].Length);
                        else
                            second = name1[1].Substring(0, 3);
                    }
                    else
                    {
                        second = "not";
                    }

                }
                else
                {
                    first = TextBox2.Text;
                }
                string pattern = "{0}{1}{2}";
                int dt = DateTime.Now.Day;
                userID = CheckUserId(string.Format(pattern, first, second, dt));

                RandomStringGenerator.RandomStringGenerator re = new RandomStringGenerator.RandomStringGenerator();
                string password = re.Generate(8);
                body = "Hello " + TextBox2.Text + " Your User Name :" + userID + "                          Password :" + password;
                SendMail(mailid, "gfchospital.ghatal@gmail.com", null, "GFC Login Credential", body);
                Encriptpassword = EncryptionDecryption.CryptorEngine.EncryptData(password);
            }

           // SendSMS(txtcontact1.Text, body);
            string Flag = thereg.InsertAppointment(Session["CoCode"].ToString().Trim(), Session["YearCode"].ToString().Trim(), DateTime.Now.ToString("MM/dd/yyyy"), HiddenField1.Value, DiaId, HiddenField2.Value, DateTime.Now.ToShortTimeString(), txtPId.Value, txtreg.Text, name.ToUpper(), TextBox2.Text.ToUpper(), txtage.Text, rblsex.SelectedValue, txtvillage.Text.ToUpper(), txtpo.Text.ToUpper(), txtps.Text.ToUpper(), DropDownList1.SelectedValue, txtpin.Text, ddlstate.SelectedValue, ph1, ph2, ddldialysername.SelectedValue, txtdialyserno.Text, DateTime.Now.ToString("MM/dd/yyyy"), Session["userName"].ToString());
            if (Flag == "true")
            {  
                ScriptManager.RegisterStartupScript(this, this.GetType(), "msg", "alert('Inserted Successfully !');", true); 
                Response.Redirect("../DayCare/PatientDashBoard.aspx");
            }
            else
            {
                if (Flag == "duplicate")
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "msg", "alert('Duplicate Entry Can not Possible !');", true);
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "msg", "alert('Error in Inserted Data !');", true);
                }
            }

        }
        else
        {
            thereg.UpdateAppointment(Session["CoCode"].ToString().Trim(), HiddenField4.Value, HiddenField2.Value, txtreg.Text, name.ToUpper(), TextBox2.Text, txtage.Text, rblsex.SelectedValue, txtvillage.Text, txtpo.Text, txtps.Text, DropDownList1.SelectedValue, txtpin.Text, ddlstate.SelectedValue, ph1, ph2, ddldialysername.SelectedValue, txtdialyserno.Text);
          //  thereg.InsertDialysismap(txtreg.Text, txtdialyserno.Text, DateTime.Now.ToString("MM/dd/yyyy"));
            ScriptManager.RegisterStartupScript(this, this.GetType(), "msg", "alert('Updated Successfully !');", true);
            Button1.Text = "Submit";
        }

        //if (dt.Rows.Count > 0)
        //{
        //    string dat = dt.Rows[0]["AppDate"].ToString();
        //    string shift = dt.Rows[0]["ShiftId"].ToString();
        //    thereg.InsertMonitor(txtreg.Text, name, dat, shift);
        //}

        GridFill();
        ResetAllFields();
       
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


    public string CheckUserId(string userID)
    {
        int keep;

        DataTable checkuserid = thepd.CheckUserId(Session["CoCode"].ToString().Trim(),userID);
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

    protected void Button2_Click(object sender, EventArgs e)
    {
        ResetAllFields();
    }

     protected void ddldialysername_SelectedIndexChanged(object sender, EventArgs e)
    {
         if (txtname.Text == "" && txtlname.Text == "")
        {
            lbltxt.ForeColor = System.Drawing.Color.Red;
            lbltxt.Text = "Put First OR Last Name";
            ddldialysername.SelectedIndex = 0;
        }
        else
        {
            DataTable dt = thereg.GetList(Session["CoCode"].ToString().Trim(), HiddenField1.Value);


            DataTable mm = thereg.DropdownDialyserCharge(Session["CoCode"].ToString().Trim(), ddldialysername.SelectedValue);
            txtPId.Value = mm.Rows[0][3].ToString();


            string fn = txtname.Text.Substring(0, 1).ToUpper();
            string ln = txtlname.Text.Substring(0, 1).ToUpper();
            txtdialyserno.Text = thereg.GenerateDialyserNo(Session["CoCode"].ToString().Trim(), fn, ln, ddldialysername.SelectedItem.Text);
            //if (dt.Rows.Count > 0)
            //{
            //    double due = Convert.ToDouble(dt.Rows[0]["AdvAmount"]);
            //    double du = Convert.ToDouble(ddlamount.Text) - due;
            //    TextBox1.Text = du.ToString();
            //}
        }
       
    }

     public void FillDrop()
     {
         DataTable dt = thereg.DropdownDialyserName(Session["CoCode"].ToString().Trim(), "0");
         this.ddldialysername.DataSource = dt;
         this.ddldialysername.DataTextField = "DialysisName";
         this.ddldialysername.DataValueField = "ID";
         this.ddldialysername.DataBind();
         this.ddldialysername.Items.Insert(0, new ListItem("--Select--", "0"));
     
     }
    protected void ddldialysertype_SelectedIndexChanged(object sender, EventArgs e)
    {
        this.ddldialysername.DataSource = thereg.DropdownDialyserName(Session["CoCode"].ToString().Trim(), ddldialysertype.SelectedValue);
        this.ddldialysername.DataTextField = "DialysisName";
        this.ddldialysername.DataValueField = "ID";
        this.ddldialysername.DataBind();
        this.ddldialysername.Items.Insert(0, new ListItem("--Select--", "0"));
    }
    private void ResetAllFields()
    {
        //ddlamount.Text="";
        ddldialysername.SelectedIndex = 0;
        ddldialysertype.SelectedIndex = 0;
        ddlstate.SelectedIndex = 0;
        txtps.Text = "";
        txtpo.Text = "";
        TextBox2.Text = ""; TextBox19.Text = "";
        txtpin.Text = "";
        DropDownList1.SelectedIndex = 0;
        txtvillage.Text = "";
        txtage.Text = "";
        txtcontact1.Text = "";
        //TextBox1.Text = "";
        rblsex.SelectedIndex = 0;
        txtcontact2.Text = "";
        txtdialyserno.Text = "";
        txtlname.Text = "";
        txtname.Text = "";
        txtreg.Text = "";
        Button1.Text = "Submit";
        txtreg.Text = thereg.GenerateRegNo(Session["CoCode"].ToString().Trim(), Session["YearCode"].ToString().Trim());

        if (Objmain.checkaccss(Session["CoCode"].ToString(), Session["UserRoleID"].ToString().Trim(), "PATIENT REGISTRATION", checkAccessType.InsertAction) == false)
        {
            Button1.Enabled = false;
        }
        
    }

    protected int  SearchIndexByName(string Value,DropDownList ddl)
    {
        int i;
        for (i = 0; i < ddl.Items.Count; i++)
        {
            if (ddl.Items[i].Text.Trim() == Value.Trim())
                return i;
        }
        return -1;
    }

    protected int SearchIndexById(string Value,DropDownList ddl)
    {
        int i;
        for (i = 0; i < ddl.Items.Count; i++)
        {
            if (ddl.Items[i].Value.Trim() == Value.Trim())
                return i;
        }
        return -1;
    }
  //  select pm.DialyserNo, COUNT(pm.DialyserNo) as DiaNo from DC_PatientMonitor pm where pm.PatientReg='GFC2109130002'  group by pm.DialyserNo
    public void CallFill(DataTable custTable)
    {
        DataTable prev = thereg.GetPreviousDate(Session["CoCode"].ToString().Trim(), txtreg.Text);
        if (prev.Rows.Count > 0)
        {
            TextBox3.Text = prev.Rows[0]["PrevDate"].ToString();
            TextBox4.Text = prev.Rows[0]["nowdate"].ToString();
        }
        string fullName = custTable.Rows[0]["patient_name"].ToString();
        var names = fullName.Split(' ');
        string firstName = names[0];
        string lastName = names[1];
        txtname.Text = firstName.ToString();
        txtlname.Text = lastName.ToString();
        txtvillage.Text = custTable.Rows[0]["vill_city"].ToString();
        TextBox2.Text = custTable.Rows[0]["guardian_name"].ToString();
        txtage.Text = custTable.Rows[0]["age"].ToString();
        txtpo.Text = custTable.Rows[0]["po"].ToString();
        txtps.Text = custTable.Rows[0]["ps"].ToString();
        HiddenField3.Value = custTable.Rows[0]["ID"].ToString()  ;
        rblsex.SelectedValue = custTable.Rows[0]["sex"].ToString();
        DropDownList1.SelectedValue = custTable.Rows[0]["District"].ToString();
        txtpin.Text = custTable.Rows[0]["Pin"].ToString();
        string[] ph1 = custTable.Rows[0]["PhNo1"].ToString().Split(' ');
        TextBox9.Text = "+91";
        TextBox17.Text = "+91";
        txtcontact1.Text = ph1[1];
        string[] ph2 = custTable.Rows[0]["PhNo2"].ToString().Split(' ');
        if (ph2.Length > 1)
            txtcontact2.Text = ph2[1];

        ddlstate.SelectedIndex = SearchIndexById(custTable.Rows[0]["State_Id"].ToString(), ddlstate);
        FillDrop();
        ddldialysername.SelectedIndex = SearchIndexById(custTable.Rows[0]["ID"].ToString(), ddldialysername);
        ddldialysertype.SelectedIndex = SearchIndexById(ddldialysername.SelectedValue, ddldialysertype);
        txtdialyserno.Text = custTable.Rows[0]["DialyserNo"].ToString();

        DataTable dt = thereg.GetAppointmentID(custTable.Rows[0]["PatientReg"].ToString());
        HiddenField1.Value = dt.Rows[0][0].ToString();
    }

    public void FillDetails()
    {
        try
        {
            if (txtreg.Text.Trim() != "")
            {
                DataTable custTable = thereg.GetPatientDetails(Session["CoCode"].ToString().Trim(), txtreg.Text.Trim());
                DataTable custTable1 = thereg.GetOnlyPatient(Session["CoCode"].ToString().Trim(), txtreg.Text.Trim());

                if (custTable.Rows.Count > 0)
                {
                    CallFill(custTable);
                    HiddenField4.Value = custTable.Rows[0]["bedallocation"].ToString();
                    TextBox19.Text = custTable.Rows[0]["BedNoText"].ToString();
                    Button1.Text = "Update";
                    if (Objmain.checkaccss(Session["CoCode"].ToString(), Session["UserRoleID"].ToString().Trim(), "PATIENT REGISTRATION", checkAccessType.UpdateAction) == false)
                    {
                        Button1.Enabled = false;
                    }
                    else
                    {
                        Button1.Enabled = true;
                    }
                }
                else
                {
                    CallFill(custTable1);

                }
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "msg", "alert('Please choose a Proper Patient !');", true);
            }


            DataTable dt = thereg.GetNoofDia(Session["CoCode"].ToString().Trim(), txtreg.Text.Trim());
            if (dt.Rows.Count > 0)
            {
                Button6.Enabled = true;
                lbldia.ForeColor = System.Drawing.Color.Green;
                lbldia.Text = "       ( " + dt.Rows[0][0].ToString() + " )";
            }
            else
            {
                lbldia.ForeColor = System.Drawing.Color.Red;
                lbldia.Text = "       ( 0 )";
            }
        }
        catch (Exception ex)
        {

        }
    }
    protected void Button4_Click(object sender, EventArgs e)
    {
        string a = DateTime.Now.ToLongTimeString(); 
        GridFill();
        try
        {
            FillDetails();             
        }
        catch (Exception ex)
        {
            lblError.ForeColor = System.Drawing.Color.Red;
            lblError.Text = ex.Message;
        }

       
    }
  
    protected void Button3_Click(object sender, EventArgs e)
    {

    }

    protected void Button5_Click(object sender, EventArgs e)
    {
          
        Response.Redirect("DialiserTypeMaster.aspx");

    }
    protected void Button6_Click(object sender, EventArgs e)
    {
        string dia=txtdialyserno.Text;
        string val;
        int len = dia.Length;
        string dia1 = dia.Substring(0, dia.Length-6);
        int a= Convert.ToInt32(dia.Substring(dia.Length - 6, 6))+1;
        if (a.ToString().Length < 6)
            val = "0" + a.ToString();
        else
            val = a.ToString();
        txtdialyserno.Text = dia1 + val;
    }
}