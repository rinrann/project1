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
using System.Net;
using EncryptionDecryption;
 
public partial class LoginPage : System.Web.UI.Page
{
    PatientLogin_Page theHelper = new PatientLogin_Page(ConfigurationSettings.AppSettings["ConnectionString"].ToString());
  
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            Page.Title = "Login Page";
        }
        catch (Exception ex)
        {
            lblError.Text = ex.Message;
        }

        if (Request.QueryString != null)
        {
           // txtreg.Text = Request.QueryString.ToString();
        }
        if (!IsPostBack)
        {
            getCompanies();
        }
        //txtUserId.Text = "admin";
        //txtPassword.Text = "54321";
        //btnLogin.Click += this.btnLogin_Click;
        /*IPAddress[] localIPs = Dns.GetHostAddresses(Dns.GetHostName());
        string allip="";
        foreach (var ip in localIPs)
        {
            allip = allip +"~~"+ ip.ToString();
        }
        lblError.Text = Request.UserHostAddress;*/
    }
    private void getCompanies()
    {        
        ddlCompany.DataValueField = "COMPCODE";
        ddlCompany.DataTextField = "CONAME";
        ddlCompany.DataSource = theHelper.getCompanies();
        ddlCompany.DataBind();
        ddlCompany.Items.Insert(0, new ListItem("Code : Name : YearCode", "Code"));
    }

    //select 'Code' as COMPCODE,'Code' + ' : '+ 'Name' + ' : '+'YearCode' as CONAME union 
    protected void btnLogin_Click(object sender, EventArgs e)
    {
        try
        {
            if (txtUserId.Text != "" && txtPassword.Text != "")
            {
                string localComputerIp = Request.UserHostAddress;
                //string localComputerName = Dns.GetHostName();
                //string localComputerName = Server.MapPath;
                /*IPAddress[] localIPs = Dns.GetHostAddresses(Dns.GetHostName());
                string localComputerIp = localIPs[0].ToString();*/
                string asf = ddlCompany.SelectedValue.Trim();
                string[] words = asf.Split('~');
                if (theHelper.Login(txtUserId.Text, txtPassword.Text, words[0].Trim(), localComputerIp) == true)
                {
                    if (ddlCompany.SelectedIndex != 0)
                    {
                        //if (btnLogin.Text == "Login" && theHelper.UserLogged(txtUserId.Text, words[0].Trim()) == true)
                        //{
                        //    lblError.Text = "User is already logged in,If you still want to login again Please try Force Login.";
                        //    btnLogin.Text = "Force Login";
                        //}
                        //else
                        //{
                        //    if (btnLogin.Text == "Force Login")
                        //    {
                        //        theHelper.ForceLogout(txtUserId.Text, words[0].Trim());
                        //    }
                        //    theHelper.insertLoginHistory(txtUserId.Text, localComputerIp);
                        //    DataTable dt = theHelper.getUserDetails(txtUserId.Text, words[0].Trim().Trim());
                            
                        //    Session.Add("userId", txtUserId.Text);
                        //    Session.Add("userName", dt.Rows[0]["UserName"].ToString().Trim());
                        //    Session.Add("UserRoleID", dt.Rows[0]["UserRoleID"].ToString().Trim());
                        //    Session.Add("CoCode", words[0]);
                        //    Session.Add("CoName", words[2]);
                        //    Session.Add("YearCode", words[1]);
                            
                        //    //theHelper.insertAutoMedicine(ddlCompany.SelectedValue.ToString());
                        //    //theHelper.insertAutoServices(ddlCompany.SelectedValue.ToString());
                        //    //theHelper.insertAutoServicesPrescription(ddlCompany.SelectedValue.ToString());
                        //    //theHelper.insertAutoInstrument(ddlCompany.SelectedValue.ToString(), words[1].ToString().Trim());//
                        //    //theHelper.insertAutoInsertSysterAya(ddlCompany.SelectedValue.ToString());
                        //    Response.Redirect("HomePage.aspx",false);
                        //}

                        theHelper.insertLoginHistory(txtUserId.Text, localComputerIp);
                        DataTable dt = theHelper.getUserDetails(txtUserId.Text, words[0].Trim().Trim());

                        Session.Add("userId", txtUserId.Text);
                        Session.Add("userName", dt.Rows[0]["UserName"].ToString().Trim());
                        Session.Add("UserRoleID", dt.Rows[0]["UserRoleID"].ToString().Trim());
                        Session.Add("CoCode", words[0]);
                        Session.Add("CoName", words[2]);
                        Session.Add("YearCode", words[1]);
                        Response.Redirect("HomePage.aspx", false);
                    }
                    else
                    {
                        lblError.Text = "Please select a Company.";
                    }
                }
                else
                {
                    lblError.Text = "Invalid User!";
                }
            }
            
        }
        catch (Exception ex)
        {
            lblError.ForeColor = System.Drawing.Color.Red;
            lblError.Text = ex.Message;
        }

      
    }
   
}
