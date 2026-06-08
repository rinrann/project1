using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Data;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Configuration;
using System.Collections;
using System.Drawing;
using System.Globalization;
 
public partial class DayCare_PatientAppointment : System.Web.UI.Page
{
    Main Objmain = new Main(ConfigurationSettings.AppSettings["ConnectionString"].ToString());
    static int flag = 0, f1 = 0;
  DC_PatientAppointment thepatientAppo = new DC_PatientAppointment(ConfigurationSettings.AppSettings["ConnectionString"].ToString());
    protected void Page_Load(object sender, EventArgs e)
    {
        Page.Title = "Patient Appointment";

       if (Session["userName"] == null)
        {
            Response.Redirect("../LoginPage.aspx");
        }
       if (Objmain.checkaccss(Session["CoCode"].ToString(), Session["UserRoleID"].ToString().Trim(), "PATIENT APPOINTMENT", checkAccessType.ViewAction) == false)
       {
           Response.Redirect("../AccessDenied.aspx");
       }

       if (Objmain.checkaccss(Session["CoCode"].ToString(), Session["UserRoleID"].ToString().Trim(), "PATIENT APPOINTMENT", checkAccessType.InsertAction) == false)
       {
           Button1.Enabled = false;
       }
       if (Objmain.checkaccss(Session["CoCode"].ToString(), Session["UserRoleID"].ToString().Trim(), "PATIENT APPOINTMENT", checkAccessType.DeleteAction) == false)
       {
           GridView1.Columns[10].Visible = false;
       }
        Page.Title = "Patient Appointment";
        GridFill();

        if (!IsPostBack)
        {
            if (Session["RegNo"] != null)
            {
                txtreg.Text = Session["RegNo"].ToString();
                DataTable dt = thepatientAppo.GetPatientforsession(txtreg.Text);
                if (dt.Rows.Count > 0)
                {
                    FillDtls(dt);
                }
            }
            DropDownFill();
            GenerateAppoNo();
            Tab1Func();
       
            if (Session["AppNo"] != null)
            {

                DataTable dt = thepatientAppo.GetPatientDtls(Session["AppNo"].ToString());
                TextBox3.Text = Session["AppNo"].ToString();
                TextBox4.Value= Session["AppNo"].ToString();
                FillDtls(dt);
                Button1.Text = "Update";
            }

            Session["AppNo"] = null;
            Session["RegNo"] = null;
        }
    }


    [System.Web.Script.Services.ScriptMethod()]
    [System.Web.Services.WebMethod]

    public static List<string> SearchCustomers(string compcode,string prefixText, int count)
    {
        using (SqlConnection conn = new SqlConnection())
        {
            conn.ConnectionString = ConfigurationSettings.AppSettings["ConnectionString"].ToString();
            using (SqlCommand cmd = new SqlCommand())
            {
                cmd.CommandText = "select distinct patient_name + '-' + PatientReg as Name from GN_PatientReg where patient_name like @SearchText + '%' and compcode='"+compcode+"'";
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
    public static List<string> SearchCustomersAddress(string compcode,string prefixText, int count)
    {
        using (SqlConnection conn = new SqlConnection())
        {
            conn.ConnectionString = ConfigurationSettings.AppSettings["ConnectionString"].ToString();
            using (SqlCommand cmd = new SqlCommand())
            {
                cmd.CommandText = "select distinct vill_city as Name from GN_PatientReg  where vill_city like @SearchText + '%' and compcode='"+compcode+"'";
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

    public void FillDtls(DataTable dt)
    {
        txtreg.Text = dt.Rows[0]["PatientReg"].ToString();
        txtname.Text = dt.Rows[0]["PName"].ToString();
        txtaddress.Text = dt.Rows[0]["PAddress"].ToString();
        string[] ph1 = dt.Rows[0]["PhNo1"].ToString().Split(' ');
        TextBox1.Text = "+91";
        txtph1.Text = ph1[1];
        string[] ph2 = dt.Rows[0]["PhNo2"].ToString().Split(' ');
        if (ph2.Length > 1)
            txtph2.Text = ph2[1];
        TextBox2.Text = "+91";
    }
    public void GenerateAppoNo()
    {
        TextBox3.Text = thepatientAppo.GenerateAppoID(Session["CoCode"].ToString().Trim());
    }

    private void ResetAllFields()
    {
        GenerateAppoNo();
        DropDownList1.SelectedIndex = 0;
        txtaddress.Text = "";
        Calendar1.Text = "";
        txtname.Text = "";
        txtph1.Text = "";
        TextBox4.Value = "";
        txtph2.Text = "";
        txtreg.Text = "";
        txtamt.Text = "";
        Button1.Text = "Submit";
      //  lblError.Text = "";
        Label2.Text = "";
        if (Objmain.checkaccss(Session["CoCode"].ToString(), Session["UserRoleID"].ToString().Trim(), "PATIENT APPOINTMENT", checkAccessType.InsertAction) == false)
        {
            Button1.Enabled = false;
        }

    }
    private void DropDownFill()
    {
        this.DropDownList1.DataSource = thepatientAppo.DropdownShift(Session["CoCode"].ToString().Trim());
        this.DropDownList1.DataTextField = "ShiftName";
        this.DropDownList1.DataValueField = "ShiftID";
        this.DropDownList1.DataBind();
        this.DropDownList1.Items.Insert(0, new ListItem("--Select--", "0"));
    }

    private void GridFill()
    {

        GridView1.SelectedIndex = -1;
        GridView1.DataSource = thepatientAppo.GridChemical(Session["CoCode"].ToString().Trim());
        GridView1.DataBind();
    }
    protected void Button2_Click(object sender, EventArgs e)
    {
        ResetAllFields();
    }
    protected void Button1_Click(object sender, EventArgs e)
    {
        string possible="";
          System.Globalization.DateTimeFormatInfo dtf = new DateTimeFormatInfo();
          string testdate = DateTime.ParseExact(Calendar1.Text, "dd/MM/yyyy", dtf).ToString("yyyy-MM-dd");
          DataTable dayOff = thepatientAppo.CheckDayoff(Session["CoCode"].ToString().Trim(),testdate.ToString());
        if(dayOff.Rows.Count>0)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "msg", "alert('Selectd Day is the off Day !');", true);
        }
        else
        {
            if (f1 == 0)
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "msg", "alert('Please Check Availability !');", true);
            }
            else
            {
                
                string ph1 = TextBox1.Text + " " + txtph1.Text;
                string ph2 = TextBox2.Text + " " + txtph2.Text;
                string amount;
                string tdate;
                tdate = Calendar1.Text.Substring(6, 4) + "/" + Calendar1.Text.Substring(3, 2) + "/" + Calendar1.Text.Substring(0, 2);
                testdate = DateTime.ParseExact(Calendar1.Text, "dd/MM/yyyy", dtf).ToString("yyyy-MM-dd");
                 
                if (txtamt.Text == "")
                    amount = "0";
                else
                    amount = txtamt.Text;
                string id;
                if (TextBox4.Value != "")
                {
                    id = TextBox4.Value;
                }
                else
                {
                    id = "null";
                }
                if (Button1.Text == "Submit")
                {

                    if (flag == 1)
                    {
                        string appo = thepatientAppo.GenerateAppoID(Session["CoCode"].ToString().Trim());
                        possible = thepatientAppo.InsertAppointment(Session["CoCode"].ToString().Trim(),Session["YearCode"].ToString().Trim(),appo, txtreg.Text, txtname.Text.ToUpper(), txtaddress.Text.ToUpper(), ph1, ph2, tdate, DropDownList1.SelectedValue, Session["userName"].ToString(), amount, DateTime.Now.ToString("MM/dd/yyyy"));
                        if (possible == "Successfull")
                        {
                            // thepatientAppo.InsertMonitor(appo,txtreg.Text, testdate,txtname.Text.ToUpper(),DropDownList1.SelectedValue);
                            //ScriptManager.RegisterStartupScript(this, this.GetType(), "msg", "alert('Insered Successfully !');", true);
                            Response.Redirect("../DayCare/PatientDashBoard.aspx");
                            ResetAllFields();
                        }
                        else
                        {
                            if (possible == "UnSuccessfull")
                            {
                                ScriptManager.RegisterStartupScript(this, this.GetType(), "msg", "alert('Error in Insered Data !');", true);
                            }
                            else
                            {
                                HiddenField2.Value = possible;
                                ScriptManager.RegisterStartupScript(this, this.GetType(), "msg", "CheckExistappointment();", true); 
                            }
                        }
                    
                    }
                    else
                    {
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "msg", "alert('Maximum Patient Limit Cross !');", true);
                    }


                }
                else
                {

                    if (flag == 1)
                    {
                        ///if (thepatientAppo.UpdateAppointment(Session["CoCode"].ToString().Trim(), Session["YearCode"].ToString().Trim(), id, txtreg.Text, txtname.Text.ToUpper(), txtaddress.Text.ToUpper(), ph1, ph2, testdate, DropDownList1.SelectedValue, amount, DateTime.Now.ToString("MM/dd/yyyy")) == true)
                          
                        if (thepatientAppo.UpdateAppointment(Session["CoCode"].ToString().Trim(), Session["YearCode"].ToString().Trim(), id, txtreg.Text, txtname.Text.ToUpper(), txtaddress.Text.ToUpper(), ph1, ph2, testdate, DropDownList1.SelectedValue, amount)== true)
                       //, DateTime.Now.ToString("MM/dd/yyyy"
                        {
                            ScriptManager.RegisterStartupScript(this, this.GetType(), "msg", "alert('Updated Successfully !');", true);
                            Button1.Text = "Submit";
                            ResetAllFields();
                            Response.Redirect("../DayCare/PatientDashBoard.aspx");
                         
                        }
                        else
                        {
                            ScriptManager.RegisterStartupScript(this, this.GetType(), "msg", "alert('Error in Updated Data !');", true);

                        }
                    }
                    else
                    {
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "msg", "alert('Maximum Patient Limit Cross !');", true);
                    }
                }

                GridFill();
            }
    }

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
            ResetAllFields();
            int index = Convert.ToInt32(e.CommandArgument);

            GridView1.Rows[index].BackColor = Color.GreenYellow;

            Label lblID = (Label)GridView1.Rows[index].FindControl("lblID");
            TextBox4.Value = lblID.Text;

            Label lblregno = (Label)GridView1.Rows[index].FindControl("lblregno");
            txtreg.Text = lblregno.Text;
      
            Label lblname = (Label)GridView1.Rows[index].FindControl("lblname");
            txtname.Text = lblname.Text;

            Label lbladdress = (Label)GridView1.Rows[index].FindControl("lbladdress");
            txtaddress.Text = lbladdress.Text;

            Label lblphone1 = (Label)GridView1.Rows[index].FindControl("lblphone1");

            string[] ph1 = lblphone1.Text.Split(' ');
            TextBox1.Text = "+91";
            txtph1.Text = ph1[1];
           

            Label lblphone2 = (Label)GridView1.Rows[index].FindControl("lblphone2");
            string[] ph2 = lblphone2.Text.Split(' ');
            if (ph2.Length > 1)
            {
                TextBox2.Text = "+91";
                txtph2.Text = ph2[1];
            }
            else
            {
                TextBox2.Text = "+91";
            }

            Label lblshift = (Label)GridView1.Rows[index].FindControl("lblshift");
            DropDownList1.SelectedIndex = DropDownList1.Items.IndexOf(DropDownList1.Items.FindByText(lblshift.Text));


            Label lblAppDate = (Label)GridView1.Rows[index].FindControl("lblAppDate");
            Calendar1.Text = lblAppDate.Text;
            Label lblamt = (Label)GridView1.Rows[index].FindControl("lblamt");
            txtamt.Text = lblamt.Text;

            Tab1Func();
            Button1.Text = "Update";
            if (Objmain.checkaccss(Session["CoCode"].ToString(), Session["UserRoleID"].ToString().Trim(), "PATIENT APPOINTMENT", checkAccessType.UpdateAction) == false)
            {
                Button1.Enabled = false;
            }
            else
            {
                Button1.Enabled = true;
            }
        }
    }
    protected void GridView1_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        
        Label lblID = (Label)GridView1.Rows[e.RowIndex].FindControl("lblID");
        thepatientAppo.DeleteAppointment(lblID.Text);
        ScriptManager.RegisterStartupScript(this, this.GetType(), "msg", "alert('Deleted Successfully !');", true);
        GridFill();
        ResetAllFields();
    }
    protected void Button5_Click(object sender, EventArgs e)
    {
        HiddenField1.Value = txtreg.Text;
        ResetAllFields();
        txtreg.Text = HiddenField1.Value;
        try
        {
            if (txtreg.Text.Trim() != "")
            {
                DataTable custTable = thepatientAppo.GetPatientDetails(Session["CoCode"].ToString().Trim(), txtreg.Text.Trim());

                if (custTable.Rows.Count > 0)
                {
                    txtname.Text = custTable.Rows[0]["patient_name"].ToString();
                    txtaddress.Text = custTable.Rows[0]["vill_city"].ToString();
                    string[] ph1 = custTable.Rows[0]["PhNo1"].ToString().Split(' ');
                    TextBox1.Text = "+91";
                    txtph1.Text = ph1[1];
                    string[] ph2 = custTable.Rows[0]["PhNo2"].ToString().Split(' ');
                    if (ph2.Length > 1)
                        txtph2.Text = ph2[1];
                    TextBox2.Text = "+91";
              
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "msg", "alert('Choose Daycare Patient !');", true);
                }
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "msg", "alert('Please Choose Customer !');", true);
            }
        }
        catch (Exception ex)
        {
            lblError.ForeColor = System.Drawing.Color.Red;
            lblError.Text = ex.Message;
        }
    }
    protected void Button3_Click(object sender, EventArgs e)
    {
        if (DropDownList1.SelectedIndex == 0)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "msg", "alert('Please Choose Shift !');", true);
        }
        else
        {
            if (Calendar1.Text == "")
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "msg", "alert('Please Choose Appointment Date !');", true);
            }


            else
            {
                System.Globalization.DateTimeFormatInfo dtf = new DateTimeFormatInfo();
                DateTime testdate = DateTime.ParseExact(Calendar1.Text, "dd/MM/yyyy", dtf);
                DataTable availibility = thepatientAppo.checkavailability(Session["CoCode"].ToString().Trim(), Convert.ToInt32(DropDownList1.SelectedValue), testdate.ToString("yyyy-MM-dd"));
                if (Convert.ToInt32(availibility.Rows[0]["Maximum"]) > Convert.ToInt32(availibility.Rows[0]["Used"]))
                {

                    Label2.ForeColor = System.Drawing.Color.Green;
                    Label2.Text = "Available";
                    flag = 1; f1 = 1;
                }
                else
                {
                    Label2.ForeColor = System.Drawing.Color.Red;
                    Label2.Text = "Unavailable";
                    flag = 0;
                }
            }
        }
    }
    protected void Button4_Click(object sender, EventArgs e)
    {

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


    public void Tab1Func()
    {
        Tab1.CssClass = "Clicked";
        Tab2.CssClass = "Initial";
        MainView.ActiveViewIndex = 0;
    }
    protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            if (Objmain.checkaccss(Session["CoCode"].ToString(), Session["UserRoleID"].ToString().Trim(), "PATIENT APPOINTMENT", checkAccessType.DeleteAction) == false)
            {
                e.Row.Cells[10].Visible = false;
            }
        }
    }
}