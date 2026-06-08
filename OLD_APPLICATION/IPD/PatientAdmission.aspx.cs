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



public partial class IPD_PatientAdmission : System.Web.UI.Page
{
    Main Objmain = new Main(ConfigurationSettings.AppSettings["ConnectionString"].ToString());
    PatientAdmission thepd = new PatientAdmission(ConfigurationSettings.AppSettings["ConnectionString"].ToString());
    System.Text.StringBuilder rpt = new System.Text.StringBuilder();
    String Date = DateTime.Now.ToString("dd/MM/yyyy");

    //-- SMS variable
    string SmsServerPath = null;
    string SmsUserName = null;
    string SmsPassword = null;
    string Mob = null;
    string mymsg = null;
    string SmsServerAdress = null;
    // string SMSText = null;
    string ToSMS = null;
    //--

    static string userId1 = "";

    protected void Page_Load(object sender, EventArgs e)
    {
        Page.Title = "Patient Admission";
        if (Session["userName"] == null)
        {
            Response.Redirect("../LoginPage.aspx");
        }
        if (Objmain.checkaccss(Session["CoCode"].ToString(), Session["UserRoleID"].ToString().Trim(), "PATIENT ADMISSION", checkAccessType.ViewAction) == false)
        {
            Response.Redirect("../AccessDenied.aspx");
        }

        if (Objmain.checkaccss(Session["CoCode"].ToString(), Session["UserRoleID"].ToString().Trim(), "PATIENT ADMISSION", checkAccessType.InsertAction) == false)
        {
            Button1.Enabled = false;
        }
        if (!IsPostBack)
        {

            Button3.Enabled = false; Button12.Visible = false;
            Button10.Visible = false; Button11.Visible = false;
            Panel3.Visible = false;
            DropDownFill();
            GenerateRegNo();
            filltext();
            DropDownList2.SelectedIndex = 2; DropDownList9.SelectedValue = "5"; DropDownList7.SelectedValue = "1";
            if (Session["BedNo"] != null)
            {
                HiddenField2.Value = Session["BedNo"].ToString();
                DataTable dt = thepd.GetBedPattern(HiddenField2.Value);
                TextBox19.Text = dt.Rows[0][0].ToString();
            }
        }
    }

    [WebMethod]
    public static void SetSession(string value)
    {
        HttpContext.Current.Session["RegnNo"] = value;
    }


    //public void VideoPlay()
    //{
    //    try
    //    {
    //        if (capture.PreviewWindow == null)
    //        {
    //            capture.PreviewWindow = panelVideo;
    //        }
    //        else
    //        {
    //            capture.PreviewWindow = null;
    //        }
    //    }
    //    catch (Exception ex)
    //    {
    //        MessageBox.Show("Unable to enable/disable preview. Please submit a bug report.\n\n" + ex.Message + "\n\n" + ex.ToString());
    //    }
    //    //try
    //    //{
    //    //    MenuItem m = new MenuItem();
    //    //    TunerInputType t = (TunerInputType)m.Index;
    //    //}
    //    //catch (Exception ex)
    //    //{
    //    //    MessageBox.Show("Unable change tuner input type. Please submit a bug report.\n\n" + ex.Message + "\n\n" + ex.ToString());
    //    //}
    //}


    [System.Web.Script.Services.ScriptMethod()]
    [System.Web.Services.WebMethod]
    public static List<string> SearchCustomersName(string prefixText, int count)
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
                cmd.CommandText = "select distinct patient_name + '-' + PatientReg +'-'+ case when husbandname is null then '' else husbandname end +'-'+Vill_city as Name from GN_PatientReg where compcode=@Compcode and patient_name like @SearchText + '%'";
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
    public static List<string> SearchCustomersAddress(string prefixText, int count)
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
                cmd.CommandText = "select distinct vill_city+'-'+vill_city2+'-'+ps+'-'+Pin as Name from GN_PatientReg pr where vill_city like @SearchText + '%'";
                cmd.Parameters.AddWithValue("@SearchText", prefixText);
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

    public static List<string> SearchCustomers2(string prefixText, int count)
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
                cmd.CommandText = "select distinct DiagnosisName as Name from GN_Diagnosis where compcode=@Compcode and DiagnosisName like @SearchText + '%'";
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


    protected void Button2_Click(object sender, EventArgs e)
    {
        ResetAllFields();
        TextBox19.Text = ""; Calendar1.Text = ""; time.Text = "";
    }

    public void SendSMS(string No, string body)
    {
        UriBuilder urlBuilder = new UriBuilder();
        urlBuilder.Host = "127.0.0.1";
        urlBuilder.Port = 8800;
        string PhoneNumber = No;
        string message = body;
        urlBuilder.Query = string.Format("PhoneNumber=%2B" + PhoneNumber + "&Text=" + message);
        HttpWebRequest httpReq = (HttpWebRequest)WebRequest.Create("http://www.smsintegra.com/smsweb/desktop_sms/desktopsms.asp?uid=pkroy&pwd=123456&mobile=" + PhoneNumber + "&msg=" + message + "&sid=smsintegra&dtNow=16/02/2013");
        HttpWebResponse httpResponse = (HttpWebResponse)(httpReq.GetResponse());
        lblError.Text = "Message Sent Successfully .. ";
    }

    protected void sendsms(string smsText)
    {
        string sql = "select SMSServerPath,SMSUserName,SMSPassword,SMSServerAddress from SchdConfig";
        DataTable dt = new DataTable();

        using (SqlConnection conn = new SqlConnection())
        {
            conn.ConnectionString = ConfigurationSettings.AppSettings["ConnectionString"].ToString();
            using (SqlCommand cmd = new SqlCommand())
            {
                cmd.CommandText = "select SMSServerPath,SMSUserName,SMSPassword,SMSServerAddress from SchdConfig";
                cmd.Connection = conn;
                conn.Open();
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(dt);
                conn.Close();
            }
        }
        if (dt.Rows.Count > 0)
        {
            SmsServerPath = Convert.ToString(dt.Rows[0]["SMSServerPath"]);
            SmsUserName = Convert.ToString(dt.Rows[0]["SMSUserName"]);
            SmsPassword = Convert.ToString(dt.Rows[0]["SMSPassword"]);
            SmsServerAdress = Convert.ToString(dt.Rows[0]["SMSServerAddress"]);

        }
        ToSMS = TextBox6.Text;
        SMSSend_RK(ToSMS, smsText);
    }

    public void SMSSend_RK(string Mob, string mymsg)
    {
        try
        {
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(SmsServerPath + "?uid=" + SmsUserName + "&pwd=" + SmsPassword + "&mobile=" + Mob + "&msg=" + mymsg + "&sid=" + SmsServerAdress + "&dtNow=" + DateTime.Now.ToString("dd/MM/yyyy"));
            HttpWebResponse response = (HttpWebResponse)request.GetResponse();
            Stream responseStream = response.GetResponseStream();
            StreamReader readStream = new StreamReader(responseStream, Encoding.UTF8);
            string strSMSResponseString = readStream.ReadToEnd();
        }
        catch (Exception ex)
        {
            string result = ex.Message + "SMS Sending Fail's";
        }

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

    private void filltext()
    {
        //Calendar1.Text = DateTime.Now.ToString("dd/MM/yyyy");

        //DateTime time1 = DateTime.Now.ToLocalTime();
        //string[] a = time1.ToString().Split(' ');
        //time.Text = a[1] + " " + a[2];

        //time.Text = DateTime.Now.ToShortTimeString();

        ScriptManager.RegisterStartupScript(this, this.GetType(), "msg", "GetDatetime();", true);
        TextBox20.Text = "0.00";
    }

    private void GenerateRegNo()
    {
        DataTable dt = thepd.GenerateRegNo(Session["CoCode"].ToString().Trim(), Session["YearCode"].ToString().Trim());
        TextBox1.Text = dt.Rows[0][0].ToString();
    }
    public void DropDownFill()
    {
        //   this.DropDownList1.Items.Clear();
        //this.DropDownList1.DataSource = thepd.GetReferBy();
        //this.DropDownList1.DataTextField = "Name";
        //this.DropDownList1.DataValueField = "ID";
        //this.DropDownList1.DataBind();
        //this.DropDownList1.Items.Insert(0, new ListItem("--Select--", "0"));

        this.DropDownList2.Items.Clear();
        this.DropDownList2.DataSource = thepd.GetGender(Session["CoCode"].ToString().Trim());
        this.DropDownList2.DataTextField = "SexName";
        this.DropDownList2.DataValueField = "ID";
        this.DropDownList2.DataBind();
        this.DropDownList2.Items.Insert(0, new ListItem("--Select--", "0"));

        this.DropDownList3.Items.Clear();
        this.DropDownList3.DataSource = thepd.GetReligion(Session["CoCode"].ToString().Trim());
        this.DropDownList3.DataTextField = "Name";
        this.DropDownList3.DataValueField = "ID";
        this.DropDownList3.DataBind();
        this.DropDownList3.Items.Insert(0, new ListItem("--Select--", "0"));


        this.DropDownList4.Items.Clear();
        this.DropDownList4.DataSource = thepd.GetMaritialStatus(Session["CoCode"].ToString().Trim());
        this.DropDownList4.DataTextField = "Name";
        this.DropDownList4.DataValueField = "ID";
        this.DropDownList4.DataBind();
        this.DropDownList4.Items.Insert(0, new ListItem("--Select--", "0"));

        this.DropDownList5.Items.Clear();
        this.DropDownList5.DataSource = thepd.GetPaymentMode(Session["CoCode"].ToString().Trim());
        this.DropDownList5.DataTextField = "Name";
        this.DropDownList5.DataValueField = "ID";
        this.DropDownList5.DataBind();

        DropDownList7.Items.Clear();
        this.DropDownList7.DataSource = thepd.GetState(Session["CoCode"].ToString().Trim());
        this.DropDownList7.DataTextField = "State_Name";
        this.DropDownList7.DataValueField = "State_ID";
        this.DropDownList7.DataBind();
        this.DropDownList7.Items.Insert(0, new ListItem("--Select--", "0"));




        DropDownList9.Items.Clear();
        this.DropDownList9.DataSource = thepd.GetDistrict(Session["CoCode"].ToString().Trim());
        this.DropDownList9.DataTextField = "DistrictName";
        this.DropDownList9.DataValueField = "ID";
        this.DropDownList9.DataBind();
        this.DropDownList9.Items.Insert(0, new ListItem("--Select--", "0"));


        DropDownList10.Items.Clear();
        this.DropDownList10.DataSource = thepd.GetDistrict(Session["CoCode"].ToString().Trim());
        this.DropDownList10.DataTextField = "DistrictName";
        this.DropDownList10.DataValueField = "ID";
        this.DropDownList10.DataBind();
        this.DropDownList10.Items.Insert(0, new ListItem("--Select--", "0"));


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

    protected void Button1_Click(object sender, EventArgs e)
    {
        Button1.Enabled = false;
        string DiadnosisId = thepd.GetDiagnosisId(TextBox32.Text);
        string testdate1;
        string dob;
        string path;
        string con1 = TextBox5.Text + " " + TextBox6.Text;
        string con2 = TextBox7.Text + " " + TextBox8.Text;
        string guadiancontact = TextBox29.Text + " " + TextBox23.Text;
        //if (FileUpload1.FileName != "")
        //{
        //    string filename = TextBox1.Text + FileUpload1.FileName;
        //    string ext = System.IO.Path.GetExtension(filename);
        //    HiddenField1.Value = TextBox1.Text;
        //    path = Server.MapPath("~") + "photo\\" + filename;
        //    FileUpload1.SaveAs(path);
        //    img1.ImageUrl = "../photo/" + filename;
        //    img1.Visible = true;

        //}
        //else
        path = "null";

        if (TextBox30.Text == "")
            TextBox30.Text = "0";
        if (TextBox31.Text == "")
            TextBox31.Text = "0";
        if (TextBox20.Text == "")
            TextBox20.Text = "0";
        System.Globalization.DateTimeFormatInfo dtf = new DateTimeFormatInfo();
        DateTime testdate = DateTime.ParseExact(Calendar1.Text, "dd/MM/yyyy", dtf);

        if (TextBox27.Text != "")
        {
            string[] aa = TextBox27.Text.Split('/');
            string day = aa[0];
            string month = aa[1];
            string year = aa[2];
            if (day.Length == 1)
                day = "0" + day;
            if (month.Length == 1)
                month = "0" + month;
             dob = day + "/" + month + "/" + year;
             testdate1 = DateTime.ParseExact(dob, "dd/MM/yyyy", dtf).ToString("yyyy-MM-dd");
        }
        else
            testdate1 = "null";
        //if (DropDownList6.SelectedValue == "0")
        //{
        //    refertext = "null";
        //}
        //else
        //{
        //    refertext = DropDownList6.SelectedValue;
        //}

        if (Button1.Text == "Submit")
        {

            string body = "";
            string userID = "";
            string Encriptpassword = "";
            string password = "";
            if (TextBox28.Text != "")
            {

                string mailid = TextBox28.Text;
                string first = "";
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
                password = re.Generate(8);
                body = "Hello " + TextBox2.Text + " Your User Name :" + userID + "                          Password :" + password + "                           Regards, GFC Hospital";
                SendMail(mailid, "gfchospital.ghatal@gmail.com", null, "GFC Login Credential", body);

                Encriptpassword = EncryptionDecryption.CryptorEngine.EncryptData(password);

            }

            if (TextBox6.Text != "")
            {
                sendsms("Hello " + TextBox2.Text + " Your User Name :" + userID + "Password :" + password + " GFC Hospital");
            }
            //  SendSMS(TextBox6.Text,body);



            if (thepd.Insertpatient_MASTER(TextBox3.Text, userID, Encriptpassword, TextBox30.Text, TextBox31.Text, testdate1, TextBox28.Text, TextBox9.Text.ToUpper(), TextBox24.Text.ToUpper(), TextBox25.Text.ToUpper(), DropDownList9.SelectedValue, TextBox11.Text, DropDownList7.SelectedValue, TextBox10.Text.ToUpper(), TextBox26.Text.ToUpper(), guadiancontact, DropDownList10.SelectedValue, TextBox1.Text, TextBox2.Text.ToUpper(), HiddenField5.Value, DiadnosisId, TextBox4.Text, DropDownList2.SelectedValue, DropDownList3.SelectedValue, DropDownList4.SelectedValue, con1, con2, TextBox12.Text.ToUpper(), TextBox13.Text.ToUpper(), TextBox14.Text.ToUpper(), TextBox15.Text.ToUpper(),TextBox16.Text.ToUpper(), TextBox17.Text.ToUpper(), HiddenField4.Value, HiddenField2.Value, testdate.ToString(), time.Text, DropDownList5.SelectedValue, TextBox20.Text, path, HiddenField3.Value, TextBox21.Text, TextBox22.Text, DateTime.Now.ToString("MM/dd/yyyy"), Session["userName"].ToString(), Session["CoCode"].ToString().Trim(), Session["YearCode"].ToString().Trim()) == true)
            {
                Button3.Enabled = true;
                ScriptManager.RegisterStartupScript(this, this.GetType(), "msg", "ConfirmationMessage();", true);
                Response.Redirect("../IPD/Allreport.aspx");
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "msg", "alert('Error in Data Inserting !');", true);
            }
        }
        else
        {
            if (thepd.Updatepatient_MASTER(TextBox3.Text, TextBox30.Text, TextBox31.Text, testdate1, TextBox28.Text, TextBox9.Text.ToUpper(), TextBox24.Text.ToUpper(), TextBox25.Text.ToUpper(), DropDownList9.SelectedValue, TextBox11.Text, DropDownList7.SelectedValue, TextBox10.Text.ToUpper(), TextBox26.Text.ToUpper(), guadiancontact, DropDownList10.SelectedValue, TextBox1.Text, TextBox2.Text.ToUpper(), HiddenField5.Value, DiadnosisId, TextBox4.Text, DropDownList2.SelectedValue, DropDownList3.SelectedValue, DropDownList4.SelectedValue, con1, con2, TextBox12.Text.ToUpper(), TextBox13.Text.ToUpper(), TextBox14.Text.ToUpper(), TextBox15.Text.ToUpper(), TextBox16.Text.ToUpper(), TextBox17.Text.ToUpper(), HiddenField4.Value, HiddenField2.Value, testdate.ToString(), time.Text, DropDownList5.SelectedValue, TextBox20.Text, path, HiddenField3.Value, TextBox21.Text, TextBox22.Text, DateTime.Now.ToString("MM/dd/yyyy"), Session["userName"].ToString(),Session["CoCode"].ToString().Trim(), Session["YearCode"].ToString().Trim()) == true)
            {

                lblError.ForeColor = System.Drawing.Color.Green;
                lblError.Text = " Updated Successfully!";
                Button1.Text = "Submit";
                Button3.Enabled = true;

                ScriptManager.RegisterStartupScript(this, this.GetType(), "msg", "ConfirmationMessage();", true);

                Response.Redirect("../IPD/Allreport.aspx");

            }
            else
            {
                lblError.ForeColor = System.Drawing.Color.Red;
                lblError.Text = " *Error in Data Updating!";
                ScriptManager.RegisterStartupScript(this, this.GetType(), "msg", "alert('* Error in Data Updating!');", true);
            }
        }


        filltext();

    }

    protected void ResetAllFields()
    {

        Button1.Enabled = true;
        DropDownList2.SelectedIndex = 0; DropDownList3.SelectedIndex = 0; DropDownList5.SelectedIndex = 0; DropDownList4.SelectedIndex = 0;
        TextBox1.Text = ""; TextBox2.Text = ""; TextBox23.Text = ""; TextBox32.Text = "";
        TextBox3.Text = ""; TextBox4.Text = ""; TextBox6.Text = ""; TextBox8.Text = ""; TextBox9.Text = ""; TextBox10.Text = ""; TextBox11.Text = ""; TextBox12.Text = "";
        TextBox13.Text = ""; TextBox14.Text = ""; TextBox27.Text = ""; TextBox30.Text = ""; TextBox31.Text = ""; TextBox24.Text = ""; TextBox25.Text = "";
        TextBox28.Text = ""; TextBox26.Text = ""; GenerateRegNo(); TextBox15.Text = ""; TextBox16.Text = ""; TextBox17.Text = ""; TextBox18.Text = "";
        if (Session["BedNo"] == null)
        {
            TextBox19.Text = ""; Calendar1.Text = ""; time.Text = "";

        }
        TextBox20.Text = ""; TextBox21.Text = ""; TextBox22.Text = "";
        Button1.Text = "Submit";
        Panel3.Visible = false;

        if (Objmain.checkaccss(Session["CoCode"].ToString(), Session["UserRoleID"].ToString().Trim(), "PATIENT ADMISSION", checkAccessType.InsertAction) == false)
        {
            Button1.Enabled = false;
        }

    }



    protected int SearchIndex(string Value, DropDownList ddl)
    {
        int i;
        for (i = 0; i < ddl.Items.Count; i++)
        {
            if (ddl.Items[i].Text.Trim() == Value.Trim())
                return i;
        }

        return -1;
    }

    protected int SearchIndexbyid(string Value, DropDownList ddl)
    {
        int i;
        for (i = 0; i < ddl.Items.Count; i++)
        {
            if (ddl.Items[i].Value.Trim() == Value.Trim())
                return i;
        }
        return -1;
    }

    public void patientDetailsFill(DataTable dt)
    {

        TextBox2.Text = dt.Rows[0]["patient_name"].ToString();
        TextBox3.Text = dt.Rows[0]["HusbandName"].ToString();
        if (dt.Rows[0]["dob1"].ToString() != "")
        {
            TextBox27.Text = dt.Rows[0]["dob1"].ToString();
            ScriptManager.RegisterStartupScript(this, this.GetType(), "msg", "displayDate();", true);
        }
        //TextBox4.Text = dt.Rows[0]["age"].ToString();
        //TextBox30.Text = dt.Rows[0]["Agemonth"].ToString();
        //TextBox31.Text = dt.Rows[0]["Ageday"].ToString();
        string[] con1 = dt.Rows[0]["PhNo1"].ToString().Split(' ');
        TextBox5.Text = con1[0];
        TextBox6.Text = con1[1];
        string[] con2 = dt.Rows[0]["PhNo2"].ToString().Split(' ');
        TextBox7.Text = "+91";
        if (con2.Length > 1)
            TextBox8.Text = con2[1];
        TextBox9.Text = dt.Rows[0]["vill_city"].ToString();
        TextBox24.Text = dt.Rows[0]["po"].ToString();
        TextBox25.Text = dt.Rows[0]["ps"].ToString();
        TextBox28.Text = dt.Rows[0]["Email"].ToString();
        DropDownList9.SelectedIndex = SearchIndexbyid(dt.Rows[0]["District"].ToString(), DropDownList9);
        DropDownList7.SelectedIndex = SearchIndexbyid(dt.Rows[0]["State_Id"].ToString(), DropDownList7);
        TextBox11.Text = dt.Rows[0]["Pin"].ToString();
        TextBox10.Text = dt.Rows[0]["vill_city2"].ToString();
        TextBox26.Text = dt.Rows[0]["po2"].ToString();
        string[] a = dt.Rows[0]["GuadianConact"].ToString().Split(' ');
        if (a.Length > 1)
            TextBox23.Text = a[1];
        DropDownList10.SelectedIndex = SearchIndexbyid(dt.Rows[0]["District2"].ToString(), DropDownList10);
        TextBox12.Text = dt.Rows[0]["guardian_name"].ToString();
        TextBox13.Text = dt.Rows[0]["relation"].ToString();
        TextBox14.Text = dt.Rows[0]["guadian_name2"].ToString();
        TextBox15.Text = dt.Rows[0]["relation2"].ToString();
        TextBox16.Text = dt.Rows[0]["guadian_name3"].ToString();
        TextBox17.Text = dt.Rows[0]["relation3"].ToString();
        DropDownList2.SelectedIndex = SearchIndexbyid(dt.Rows[0]["sex"].ToString(), DropDownList2);
        DropDownList3.SelectedIndex = SearchIndexbyid(dt.Rows[0]["religion"].ToString(), DropDownList3);
        DropDownList4.SelectedIndex = SearchIndexbyid(dt.Rows[0]["marital_status"].ToString(), DropDownList4);
    }

    //public void CalculateBirthDate()
    //{
    //    if (TextBox4.Text == "")
    //    {
    //        TextBox4.Text = "0";
    //    }

    //    if (TextBox30.Text == "")
    //    {
    //        TextBox30.Text = "0";
    //    }
    //    if (TextBox31.Text == "")
    //    {
    //        TextBox31.Text = "0";
    //    }

    //    int yr = DateTime.Now.Date.Year - Convert.ToInt32(TextBox4.Text);
    //    int mn = DateTime.Now.Date.Month - Convert.ToInt32(TextBox30.Text);
    //    int dy = (DateTime.Now.Date.Day) - Convert.ToInt32(TextBox31.Text);

    //    if (mn < 0)
    //    {
    //        mn = 12 + mn;
    //        yr--;
    //    }
    //    if (mn == 0)
    //    {
    //        mn = DateTime.Now.Date.Month;
    //    }

    //    if (dy < 0)
    //    {
    //        dy = 30 + dy;

    //    }
    //    string day = "";
    //    string month = "";
    //    if (Convert.ToInt32(dy) < 10)
    //        day = "0" + dy.ToString();
    //    else
    //        day = dy.ToString();

    //    if (Convert.ToInt32(mn) < 10)
    //        month = "0" + mn.ToString();
    //    else
    //        month = mn.ToString();

    //    string dob = day + "/" + month + "/" + yr;
    //    TextBox27.Text = dob;
    //}

    protected void Button5_Click(object sender, EventArgs e)
    {
        Button1.Enabled = true;
        HiddenField1.Value = TextBox1.Text;
        ResetAllFields();
        TextBox1.Text = HiddenField1.Value;
        FetchDetails(TextBox1.Text);

    }
    public void FetchDetails(string regno)
    {
        DataTable dt = thepd.GetPatientDtls(Session["CoCode"].ToString().Trim(), Session["YearCode"].ToString().Trim(),regno);
        DataTable dischargeCheck = thepd.GetPatientCheckForDischarge(Session["CoCode"].ToString().Trim(),regno);
        DataTable dt1 = thepd.GetPatientDtlsChamber(Session["CoCode"].ToString().Trim(),regno);
        if (dischargeCheck.Rows.Count > 0)
        {
            patientDetailsFill(dischargeCheck);
            Button1.Text = "Submit";
            Button9.Enabled = true;
        }
        else
        {
            if (dt.Rows.Count > 0)
            {
                patientDetailsFill(dt);
                TextBox21.Text = dt.Rows[0]["ReferName"].ToString();
                HiddenField4.Value = dt.Rows[0]["underDoctor"].ToString();
                TextBox32.Text = dt.Rows[0]["DiagnosisName"].ToString();
                //DataTable getdiaid = thepd.GetDisplineid(dt.Rows[0]["Diagnosis"].ToString());
                //DropDownList1.SelectedValue = getdiaid.Rows[0][0].ToString();
                HiddenField5.Value = dt.Rows[0]["referred_by"].ToString();
                HiddenField3.Value = dt.Rows[0]["ReferID"].ToString();
                TextBox18.Text = dt.Rows[0]["doc_name"].ToString();
                if (dt.Rows[0]["BedNo"].ToString() != "")
                {
                    TextBox19.Text = dt.Rows[0]["BedNoText"].ToString();
                    HiddenField2.Value = dt.Rows[0]["BedNo"].ToString();
                    Calendar1.Text = dt.Rows[0]["ADate"].ToString();
                    time.Text = dt.Rows[0]["AdmissionTime"].ToString();
                }
                TextBox20.Text = dt.Rows[0]["AdvAmount"].ToString();
                //string[] path = dt.Rows[0]["path"].ToString().Split('\\');
                //img1.ImageUrl = "../photo/" + path[path.Length - 1];

                //img1.Visible = true;
                //DropDownList1.SelectedIndex = SearchIndexbyid(dt.Rows[0]["referred_by"].ToString(), DropDownList1);
                //if (DropDownList1.SelectedItem.Text == "Consultant Doctor")
                //{
                //    Panel1.Visible = true;
                //    DropDownList6.SelectedIndex = SearchIndexbyid(dt.Rows[0]["ReferDocID"].ToString(), DropDownList6);
                //}
                //else
                //{
                //    Panel1.Visible = false;
                //}

                DropDownList5.SelectedIndex = SearchIndexbyid(dt.Rows[0]["TypeofPayment"].ToString(), DropDownList5);
                if (DropDownList5.SelectedItem.Text == "Insurance")
                {
                    Panel3.Visible = true;
                    TextBox22.Text = dt.Rows[0]["InsuranceNo"].ToString();
                }
                else
                {
                    Panel3.Visible = false;
                }
                Button9.Enabled = false;
                Button1.Text = "Update";
                if (Objmain.checkaccss(Session["CoCode"].ToString(), Session["UserRoleID"].ToString().Trim(), "PATIENT ADMISSION", checkAccessType.UpdateAction) == false)
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
                if (dt1.Rows.Count > 0)
                {
                    TextBox2.Text = dt1.Rows[0]["PName"].ToString();
                    TextBox4.Text = dt1.Rows[0]["Age"].ToString();
                    string[] con1 = dt1.Rows[0]["PhNo1"].ToString().Split(' ');
                    TextBox5.Text = con1[0];
                    TextBox6.Text = con1[1];
                    string[] con2 = dt1.Rows[0]["PhNo2"].ToString().Split(' ');
                    TextBox7.Text = "+91";
                    if (con2.Length > 1)
                        TextBox8.Text = con2[1];
                    TextBox9.Text = dt1.Rows[0]["Address"].ToString();
                    TextBox12.Text = dt1.Rows[0]["GuadianName"].ToString();
                    Button9.Enabled = true;
                    Button1.Text = "Submit";
                }
            }
        }
    }
    protected void Button3_Click(object sender, EventArgs e)
    {
        string someScript = "";
        someScript = "<script language='javascript'> var el = document.getElementById('h1');el.style.display = 'none';</script>";
        Page.ClientScript.RegisterStartupScript(this.GetType(), "onload", someScript);

        GetReport();
        Button10.Visible = true;
        Button11.Visible = true;
    }

    public void GetReport()
    {
        Report_Header();
        GetHearder_Detail();
        ltrReport.Text = rpt.ToString();
    }

    public void Report_Header()
    {
        rpt.Append("<br/>"); rpt.Append("<br/>"); rpt.Append("<br/>");
        rpt.Append("<table width='100%' cellspacing=0 border=0 style='border:1px solid black;background-color:#9B9C8D;border-spacing: 0; padding: 0;font-family:verdana;'>");
        rpt.Append("<tr cellpadding:'0'>");
        rpt.AppendFormat("<td rowspan='4' style='width: 35%; font-family:Verdana; font-size:x-Medium; font-weight:bold;text-align:left'><img src='/Images/logo.jpg'  style='height:87px;width:205px;border-style:inherit;border-color:#D3CDD4'/></td>");
        rpt.AppendFormat("<td style='width: 35%; font-family:Verdana; font-size:large; font-weight:bold;text-align:center'>{0}</td>", "GFC Hospital");
        rpt.AppendFormat("<td style='width: 35%; font-family:Verdana; font-size:x-Medium; font-weight:bold;text-align:center'>&nbsp;</td>");
        rpt.Append("</tr>");
        rpt.Append("<tr>");
        rpt.AppendFormat("<td style='width: 35%; font-family:Verdana; font-size:x-Medium; font-weight:bold;text-align:center'>{0}</td>", "REG NO : NH-315/G-70/2013");
        rpt.AppendFormat("<td style='width: 35%; font-family:Verdana; font-size:x-Medium; font-weight:bold;text-align:center'>&nbsp;</td>");
        rpt.Append("</tr>");
        rpt.Append("<tr>");
        rpt.AppendFormat("<td style='width: 35%; font-family:Verdana; font-size:x-Medium; font-weight:bold;text-align:center'>{0}</td>", "KUSHPATA,GHATAL,MIDNAPUR(W),WB,721212");
        rpt.AppendFormat("<td style='width: 35%; font-family:Verdana; font-size:x-Medium; font-weight:bold;text-align:center'>&nbsp;</td>");
        rpt.Append("</tr>");
        rpt.Append("<tr>");
        rpt.AppendFormat("<td style='width: 35%; font-family:Verdana; font-size:x-Medium; font-weight:bold;text-align:center'>{0}</td>", "Ph :(03225)244400/244643  M:9434419825");
        rpt.AppendFormat("<td style='width: 35%; font-family:Verdana; font-size:x-Medium; font-weight:bold;text-align:center'>&nbsp;</td>");
        rpt.Append("</tr>");
        rpt.Append("</table>");
    }

    public void GetHearder_Detail()
    {
        DataTable dt = thepd.GetPatientDtls(Session["CoCode"].ToString().Trim(), Session["YearCode"].ToString().Trim(), TextBox1.Text);
        string age = "";
        if (dt.Rows[0]["age"].ToString() != "" || dt.Rows[0]["age"].ToString() != "0")
            age = dt.Rows[0]["age"].ToString() + " yrs ";

        if (dt.Rows[0]["Agemonth"].ToString() != "" || dt.Rows[0]["Agemonth"].ToString() != "0")
            age = age + dt.Rows[0]["Agemonth"].ToString() + " mnt "; ;

        if (dt.Rows[0]["Ageday"].ToString() != "" || dt.Rows[0]["Ageday"].ToString() != "0")
            age = age + dt.Rows[0]["Ageday"].ToString() + " day"; ;

        rpt.Append("<br/>");
        rpt.Append("<table width='100%' cellspacing=0 border=0 style='border:1px solid black;border-spacing: 0; padding: 0;font-family:verdana;'>");

        rpt.Append("<tr style='height:40px'>");
        rpt.Append("<td colspan='6' style='width: 8%;border-bottom: 1px solid black; font-family:Verdana;font-weight:bold;background-color:#9B9C8D;font-size:medium; text-align:Center'> PATIENT DETAILS  </td>");
        rpt.Append("</tr'>");

        rpt.Append("<tr style='height:30px'>");
        rpt.Append("<td style='border-bottom: 1px solid black;border-right: 1px solid black; font-family:Verdana;font-weight:bold;background-color:beige; font-size:small; text-align:left'>Registration No</td>");
        rpt.AppendFormat("<td style=' border-bottom: 1px solid black;border-right: 1px solid black; font-family:Verdana; font-size:small;text-align:left'>{0}</td>", dt.Rows[0]["PatientReg"]);
        rpt.Append("<td style='width: 8%;border-bottom: 1px solid black;border-right: 1px solid black; font-family:Verdana;font-weight:bold;background-color:beige; font-size:small; text-align:left'>Patient's Name</td>");
        rpt.AppendFormat("<td style='width: 5%;border-bottom: 1px solid black;border-right: 1px solid black; font-family:Verdana; font-size:small;text-align:left'>{0}</td>", dt.Rows[0]["patient_name"]);
        rpt.Append("<td style='width: 8%;border-bottom: 1px solid black;border-right: 1px solid black; font-family:Verdana;font-weight:bold;background-color:beige; font-size:small; text-align:left'>Guadian Name</td>");
        rpt.AppendFormat("<td style='width: 5%;border-bottom: 1px solid black;  font-family:Verdana; font-size:small;text-align:left'>{0}</td>", dt.Rows[0]["guardian_name"]);

        rpt.Append("</tr >");

        rpt.Append("<tr style='height:30px'>");
        rpt.Append("<td style='width: 8%;border-bottom: 1px solid black;border-right: 1px solid black; font-family:Verdana;font-weight:bold;background-color:beige; font-size:small; text-align:left'>Age</td>");
        rpt.AppendFormat("<td style='width: 5%;border-bottom: 1px solid black;border-right: 1px solid black; font-family:Verdana; font-size:small;text-align:left'>{0}</td>", age);
        rpt.Append("<td style='width: 8%;border-bottom: 1px solid black;border-right: 1px solid black; font-family:Verdana;font-weight:bold;background-color:beige; font-size:small; text-align:left'>Address</td>");
        rpt.AppendFormat("<td style='width: 5%;border-bottom: 1px solid black;border-right: 1px solid black; font-family:Verdana; font-size:small;text-align:left'>{0}</td>", dt.Rows[0]["vill_city"]);
        rpt.Append("<td style='width: 8%;border-bottom: 1px solid black;border-right: 1px solid black; font-family:Verdana;font-weight:bold;background-color:beige; font-size:small; text-align:left'>Phone No</td>");
        rpt.AppendFormat("<td style='width: 5%;border-bottom: 1px solid black;font-family:Verdana; font-size:small;text-align:left'>{0}</td>", dt.Rows[0]["PhNo1"]);
        rpt.Append("</tr >");
        rpt.Append("</table >");

        rpt.Append("<br/>"); rpt.Append("<br/>");
        rpt.Append("<table width='100%' cellspacing=0 border=0 style='border:1px solid black;border-spacing: 0; padding: 0;font-family:verdana;'>");
        rpt.Append("<tr style='height:30px'>");
        rpt.Append("<td style='width: 5%;Height:70px;  font-family:Verdana;font-weight:bold;border-bottom: 1px solid black;border-right: 1px solid black; font-size:small; text-align:left'>Date of Admission</td>");
        rpt.AppendFormat("<td style='width: 5%;Height:70px;font-family:Verdana; font-size:small;border-bottom: 1px solid black;border-right: 1px solid black;text-align:left'>{0}</td>", dt.Rows[0]["ADate"]);
        rpt.Append("<td style='width: 5%;Height:70px;font-family:Verdana;font-weight:bold;border-bottom: 1px solid black;border-right: 1px solid black; font-size:small; text-align:left'>Time of Admission</td>");
        rpt.AppendFormat("<td style='width: 5%;Height:70px; font-family:Verdana; font-size:small;border-bottom: 1px solid black;border-right: 1px solid black;text-align:left'>{0}</td>", dt.Rows[0]["FromTime"]);
        rpt.Append("<td style='width: 5%;Height:70px; font-family:Verdana;border-bottom: 1px solid black;border-right: 1px solid black;font-weight:bold;font-size:small; text-align:left'>Bed No</td>");
        rpt.AppendFormat("<td style='width: 5%;Height:70px;border-bottom: 1px solid black; font-family:Verdana; font-size:small;text-align:left'>{0}</td>", dt.Rows[0]["BedNoText"]);
        rpt.Append("</tr >");


        rpt.Append("<tr style='height:30px'>");
        rpt.Append("<td style='width: 5%;Height:70px;border-right: 1px solid black; font-family:Verdana;font-weight:bold;font-size:small; text-align:left'>Doctor's Name :</td>");
        rpt.AppendFormat("<td style='width: 5%;Height:70px; border-right: 1px solid black;font-family:Verdana; font-size:small;text-align:left'>{0}</td>", dt.Rows[0]["doc_name"]);
        rpt.Append("<td style='width: 5%;Height:70px; border-right: 1px solid black;font-family:Verdana;font-weight:bold; font-size:small; text-align:left'>Diagnosis :</td>");
        rpt.AppendFormat("<td style='width: 5%;Height:70px; font-family:Verdana; font-size:small;text-align:left'>{0}</td>", dt.Rows[0]["DiagnosisName"]);
        rpt.Append("<td style='width: 5%;Height:70px;font-family:Verdana;font-weight:bold; font-size:small; text-align:left'></td>");
        rpt.Append("<td style='width: 5%;Height:70px;  font-family:Verdana; font-size:small;text-align:left'></td>");

        rpt.Append("</tr >");
        rpt.Append("</table>");

        ltrReport.Visible = true;

    }
    protected void Button10_Click(object sender, EventArgs e)
    {
        ltrReport.Visible = false;
        Button10.Visible = false;
        Button11.Visible = false;
    }

    protected void DropDownList1_SelectedIndexChanged(object sender, EventArgs e)
    {
        //if (DropDownList1.SelectedItem.Text == "Consultant Doctor")
        //{
        //    Panel1.Visible = true;
        //    Panel2.Visible = false;
        //}
        //else
        //{
        //    Panel2.Visible = true;
        //    Panel1.Visible = false;
        //}
    }
    protected void DropDownList5_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (DropDownList5.SelectedItem.Text == "Insurance" || DropDownList5.SelectedItem.Text == "RSBY")
            Panel3.Visible = true;
        else
            Panel3.Visible = false;
    }

    protected void CheckBox1_CheckedChanged(object sender, EventArgs e)
    {
        if (CheckBox1.Checked)
        {
            TextBox10.Text = TextBox9.Text;
            TextBox26.Text = TextBox24.Text;
            TextBox12.Text = TextBox3.Text;
            DropDownList10.SelectedValue = DropDownList9.SelectedValue;

        }
        else
        {
            TextBox10.Text = "";
            TextBox26.Text = "";
            TextBox23.Text = "";
            DropDownList10.SelectedIndex = 0;

        }
    }

    protected void TextBox27_TextChanged(object sender, EventArgs e)
    {

        DateTime now = DateTime.Now;
        System.Globalization.DateTimeFormatInfo dtf = new DateTimeFormatInfo();
        DateTime dob = DateTime.ParseExact(TextBox27.Text, "dd/MM/yyyy", dtf);

        TimeSpan ts = now - dob;
        DateTime Age1 = DateTime.MinValue.AddDays(ts.Days);
        int y = Age1.Year - 1;
        int m = Age1.Month - 1;
        int d = Age1.Day - 1;
        TextBox4.Text = y.ToString(); TextBox30.Text = m.ToString(); TextBox31.Text = d.ToString();

    }
    protected void Button12_Click(object sender, EventArgs e)
    {

    }
    protected void Button_fetchcopy_Click(object sender, EventArgs e)
    {
        //ResetAllFields();
        FetchDetails(TextBox1.Text);
        GenerateRegNo();
        HiddenField1.Value = TextBox1.Text;
        TextBox19.Text = "";//Intialise BedNo.
        TextBox20.Text = "0.00";//Advance Amount
        Button1.Text = "Submit";
        Button9.Enabled = true;
    }
}