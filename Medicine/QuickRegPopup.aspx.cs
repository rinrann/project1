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

public partial class Medicine_QuickRegPopup : System.Web.UI.Page
{
    Main Objmain = new Main(ConfigurationSettings.AppSettings["ConnectionString"].ToString());
    PatientAppointment thepatientAppo = new PatientAppointment(ConfigurationSettings.AppSettings["ConnectionString"].ToString());

    protected void Page_Load(object sender, EventArgs e)
    {
        Page.Title = "Patient Registration";
        if (Session["userId"] == null)
        {
            Response.Redirect("../LoginPage.aspx");
        }

        if (!IsPostBack)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "msg", "GetDatetime();", true);
            GenerateRegCode();
            Button1.Text = "Submit";
        }
    }

    private void GenerateRegCode()
    {
        DataTable dt = thepatientAppo.GenerateRegno(Session["CoCode"].ToString().Trim(), Session["YearCode"].ToString().Trim());
        TextBox2.Text = dt.Rows[0][0].ToString();
    }

    protected void Button1_Click(object sender, EventArgs e)
    {
        
        System.Globalization.DateTimeFormatInfo dtf = new DateTimeFormatInfo();
        //DateTime dob = DateTime.ParseExact(TextBox27.Text, "dd/MM/yyyy", dtf);

        //if (TextBox2.Text == "")
        //{
        //    lblError.Text = "Registration No cannot be blank!";
        //}
        if (TextBox3.Text == "")
        {
            lblError.Text = "Patient Name cannot be blank!";
        }
        else
        {
            DataTable dt = thepatientAppo.GenerateRegno(Session["CoCode"].ToString().Trim(), Session["YearCode"].ToString().Trim());
            string LedgerId = thepatientAppo.GenerateLedgerId(Session["CoCode"].ToString().Trim(), Session["YearCode"].ToString().Trim());
            TextBox2.Text = dt.Rows[0][0].ToString();
            HiddenField1.Value = TextBox3.Text;
            HiddenField2.Value = dt.Rows[0][0].ToString();
            HiddenField3.Value = LedgerId;
            if (thepatientAppo.InsertRegistration("I", Session["CoCode"].ToString().Trim(), Session["YearCode"].ToString().Trim(), TextBox2.Text, TextBox3.Text, LedgerId, Session["userId"].ToString()) == true)
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "msg", "alert('Inserted  Successfully !');", true);
                
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "msg", "alert('Error in Inserted Data !');", true);
            }
        }
    }
}