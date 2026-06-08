using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.Security;
using System.Web.UI.WebControls;
using System.Data.SqlClient;

using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Configuration;
using System.Collections;
using System.Drawing;
using System.Web.Security;
using System.Globalization;
using System.Diagnostics;
using System.Threading;
using System.Web.Services;
 
public partial class OPD_PatientList : System.Web.UI.Page
{
    Main Objmain = new Main(ConfigurationSettings.AppSettings["ConnectionString"].ToString());
    PatientList_OPD theOTList = new PatientList_OPD(ConfigurationSettings.AppSettings["ConnectionString"].ToString());
    static Stopwatch ss;
    protected void Page_Load(object sender, EventArgs e)
    {
        Count();
        Page.Title = "Patient List";
        if (Session["userName"] == null)
        {
            Response.Redirect("../LoginPage.aspx");
        }
        if (Objmain.checkaccss(Session["CoCode"].ToString(), Session["UserRoleID"].ToString().Trim(), "PATIENT LIST", checkAccessType.ViewAction) == false)
        {
            Response.Redirect("../AccessDenied.aspx");
        }

        if (!IsPostBack)
        {
            TextBox3.Text = DateTime.Now.ToString("yyyy-MM-dd");
            DropDownFill(); 
            //TextBox3.Text = DateTime.Now.ToString("dd/MM/yyyy");
            GridFill();
            //ss = new Stopwatch(); 
            //timer1.Enabled = true;
            //ss.Start();
        }
    }
 
    public void Count()
    {
        DataTable dt, dt1;
        DateTime testdate;
        //System.Globalization.DateTimeFormatInfo dtf = new DateTimeFormatInfo();
        if (TextBox3.Text != "")
        {
            //testdate = DateTime.ParseExact(TextBox3.Text, "dd/MM/yyyy", dtf);
            dt = theOTList.TotalPatient(TextBox3.Text);
            dt1 = theOTList.CheckedPatient(TextBox3.Text);
        }
        else {
            dt = theOTList.TotalPatient(TextBox3.Text);
            dt1 = theOTList.CheckedPatient(TextBox3.Text);
        }
        //Label2.Text = dt.Rows[0][0].ToString();
        //Label3.Text = dt1.Rows[0][0].ToString();
        int wait = Convert.ToInt32(dt.Rows[0][0]) - Convert.ToInt32(dt1.Rows[0][0]);
        //Label4.Text = wait.ToString();
    }
    public void DropDownFill()
    {
        //this.DropDownList1.DataSource = theOTList.DropdownDoctor(Session["CoCode"].ToString().Trim());
        //this.DropDownList1.DataTextField = "doc_name";
        //this.DropDownList1.DataValueField = "doc_id";
        //this.DropDownList1.DataBind();
        //this.DropDownList1.Items.Insert(0, new ListItem("ALL", "0"));
    }
   public void GridFill()
    {
        //if (txtPtName.Text == "")
        //{
        //    txtRegNo.Text = "";
        //}

        GridView1.DataSource = theOTList.GridPopupFill(Session["CoCode"].ToString().Trim(), TextBox3.Text, txtRegNo.Text.Trim(),txtPtName.Text.Trim(), txtPhoneNo.Text.Trim());
        
        GridView1.DataBind();
    }
      
    protected void Button1_Click(object sender, EventArgs e)
    {
        //if (rbloption.SelectedValue == "1" && TextBox3.Text=="")
        //{
        //    lblError.Text = "Enter Date!";
        //}
        //else if (rbloption.SelectedValue == "2" && txtRegNo.Text == "")
        //{
        //    lblError.Text = "Enter Registration No!";
        //}
        //else if (rbloption.SelectedValue == "3" && txtPtName.Text == "")
        //{
        //    lblError.Text = "Enter Patient Name!";
        //}
        //else if (rbloption.SelectedValue == "4" && txtPhoneNo.Text == "")
        //{
        //    lblError.Text = "Enter Phone No!";
        //}
        //else
        //{
        //    GridFill();
        //}

        GridFill();
    }

    public void Timer12()
    {
        //Timer tmr = new Timer();
        //tmr.Interval = 1000;//ticks every 1 second
        //tmr.Tick += new EventHandler<ontick>
        //tmr.Start();
    }
    protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "select")
        {

            string id = (e.CommandArgument).ToString();
            Session.Add("RegNo", id);
            Response.Redirect("../OPD/PatientsDetails.aspx");
        }
        if (e.CommandName == "OPD")
        {

            string id = (e.CommandArgument).ToString();
            Session.Add("RegNo", id);
            Session["ReqNo"] = null;
            Response.Redirect("../Pathology/PatientRequisitionOPD.aspx");
        }
        if (e.CommandName == "Diagnostic")
        {

            string id = (e.CommandArgument).ToString();
            Session.Add("RegNo", id);
            Session.Add("BillType", "DIG");
            Session["ReqNo"] = null;
            Response.Redirect("../Pathology/PatientRequisition.aspx");
        }
        if (e.CommandName == "Procedure")
        {

            string id = (e.CommandArgument).ToString();
            Session.Add("RegNo", id);
            Session.Add("BillType", "PRC");
            Session["ReqNo"] = null;
            Response.Redirect("../Pathology/PatientRequisition.aspx");
        }
        if (e.CommandName == "Infertility")
        {

            string id = (e.CommandArgument).ToString();
            Session.Add("RegNo", id);
            Session["ReqNo"] = null;
            Session.Add("BillType", "INF");
            Response.Redirect("../Pathology/PatientRequisition.aspx");
        }
    }

    protected void GridView1_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        GridView1.PageIndex = e.NewPageIndex;
        GridFill();
    }
    protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        GridViewRow myRow = e.Row;
      //  string dt = DateTime.Now.ToString("dd/MM/yyyy");
        if (myRow.RowType == DataControlRowType.DataRow)
        {
            Label lblSelno = (Label)e.Row.FindControl("lblSlno");
            lblSelno.Text = (e.Row.RowIndex + 1).ToString();
            if (myRow.RowState == DataControlRowState.Normal || myRow.RowState == DataControlRowState.Alternate)
            {
                Label lblappotype = (Label)e.Row.FindControl("lblappotype");
                if (lblappotype.Text == "2")
                {

                    e.Row.BackColor = Color.Gold;
                }
                else
                {
                    GridView1.RowStyle.BackColor = System.Drawing.Color.Empty;
                }
            }
        }
    }

    //protected void timer1_tick(object sender, EventArgs e)
    //{
    //    //System.Threading.Thread.Sleep(100);
    //    //string currenttime = DateTime.Now.ToLongTimeString();
    //    //Label5.Text = currenttime;

    //    int hrs = ss.Elapsed.Hours;
    //    int mins = ss.Elapsed.Minutes;
    //    int secs = ss.Elapsed.Seconds;
     
    //    Label5.Text = hrs + ":";
    //    if (mins < 10)
    //        Label5.Text += "0" + mins + ":";
    //    else
    //        Label5.Text += mins + ":";
    //    if (secs < 10)
    //        Label5.Text += "0" + secs;
    //    else
    //        Label5.Text += secs;

        
    //}
    protected void GridView1_SelectedIndexChanged(object sender, EventArgs e)
    {

    }

    [System.Web.Script.Services.ScriptMethod()]
    [System.Web.Services.WebMethod]
    public static List<string> SearchByPatientName(string prefixText, int count)
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
                cmd.CommandText = "select PatientRegNo + '~' + PName as Name from opd_patientregistration where compcode=@Compcode and PName like @SearchText + '%'";
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
    public static List<string> SearchByPhoneNo(string prefixText, int count)
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
                cmd.CommandText = "select PatientRegNo + '~' + PName+'~'+PhNo1 as Name from opd_patientregistration where compcode=@Compcode and PhNo1 like @SearchText + '%'";
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
    public static List<string> SearchByRegNo(string prefixText, int count)
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
                cmd.CommandText = "select PatientRegNo + '~' + PName as Name from opd_patientregistration where compcode=@Compcode and PatientRegNo like @SearchText + '%'";
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
}
