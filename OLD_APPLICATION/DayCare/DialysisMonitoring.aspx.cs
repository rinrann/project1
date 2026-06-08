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
 
public partial class DayCare_DialysisMonitoring : System.Web.UI.Page
{

    Main Objmain = new Main(ConfigurationSettings.AppSettings["ConnectionString"].ToString());
    DC_DialysisMonitoring themonitor = new DC_DialysisMonitoring(ConfigurationSettings.AppSettings["ConnectionString"].ToString());
 
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["userName"] == null)
        {
            Response.Redirect("../LoginPage.aspx");
        }
        if (Objmain.checkaccss(Session["CoCode"].ToString(), Session["UserRoleID"].ToString().Trim(), "DIALYSIS MONITORING", checkAccessType.ViewAction) == false)
        {
            Response.Redirect("../AccessDenied.aspx");
        }

        Page.Title = "Dialysis Monitoring";
   
        if (!IsPostBack)
        {
            HiddenField5.Value = "0";
            Panel1.Visible = false;
            Panel2.Visible = false;
           Button1.Enabled = false;
           DateTime dt = DateTime.Now;
           TextBox1.Text = dt.ToString("dd/MM/yyyy");
           dropdownfill();
           if (Session["AppoDate"] != null)
            {
                TextBox1.Text = Session["AppoDate"].ToString();
                gridfill();
            }
        }      
    }

    private void dropdownfill()
    {
         DropDownList2.DataSource = themonitor.DropdownShift(Session["CoCode"].ToString().Trim());
        DropDownList2.DataTextField = "ShiftName";
        DropDownList2.DataValueField = "ShiftID";
        DropDownList2.DataBind();
        DropDownList2.Items.Insert(0, new ListItem("--Select--", "0"));
    }
    public void resetall()
    {
        DropDownList2.SelectedIndex = 0;
        TextBox8.Text = ""; TextBox1.Text = ""; TextBox2.Text = ""; TextBox3.Text = ""; TextBox4.Text = ""; TextBox5.Text = ""; TextBox6.Text = ""; TextBox7.Text = "";
        TextBox9.Text = ""; TextBox10.Text = ""; TextBox22.Text = ""; TextBox23.Text = ""; TextBox24.Text = ""; TextBox25.Text = ""; TextBox26.Text = ""; TextBox11.Text = "";
        TextBox13.Text = ""; TextBox15.Text = ""; TextBox17.Text = ""; TextBox19.Text = ""; TextBox30.Text = ""; TextBox36.Text = ""; TextBox38.Text = ""; TextBox12.Text = "";
        TextBox14.Text = ""; TextBox16.Text = ""; TextBox18.Text = ""; TextBox20.Text = ""; TextBox31.Text = ""; TextBox37.Text = ""; TextBox39.Text = ""; TextBox21.Text = "";
        TextBox27.Text = ""; TextBox28.Text = ""; TextBox29.Text = ""; TextBox40.Text = ""; TextBox41.Text = "";

    }
    protected void Button7_Click(object sender, EventArgs e)
    {

        DataTable dt = themonitor.getappodate(Session["CoCode"].ToString().Trim(), Session["YearCode"].ToString().Trim(), TextBox32.Text);
        DataTable dt1 = themonitor.getdiano(Session["CoCode"].ToString().Trim(), TextBox32.Text);
        if (themonitor.UpdateAll(Session["CoCode"].ToString().Trim(), Session["YearCode"].ToString().Trim(), HiddenField1.Value, TextBox42.Text, TextBox43.Text, TextBox32.Text, dt1.Rows[0]["DialyserNo"].ToString(), dt.Rows[0]["getappodate"].ToString(), TextBox50.Text, TextBox51.Text, TextBox57.Text, TextBox58.Text, HiddenField3.Value, TextBox45.Text, TextBox46.Text, TextBox47.Text, TextBox48.Text, TextBox49.Text, TextBox3.Text, TextBox10.Text, TextBox15.Text, TextBox21.Text, TextBox27.Text, TextBox37.Text, TextBox2.Text, TextBox9.Text, TextBox8.Text, TextBox20.Text, TextBox26.Text, TextBox36.Text, TextBox7.Text, TextBox14.Text, TextBox19.Text, TextBox25.Text, TextBox31.Text, TextBox41.Text, TextBox52.Text, TextBox53.Text, TextBox54.Text, TextBox55.Text, TextBox56.Text, TextBox4.Text, TextBox11.Text, TextBox16.Text, TextBox22.Text, TextBox28.Text, TextBox38.Text, TextBox5.Text, TextBox12.Text, TextBox17.Text, TextBox23.Text, TextBox29.Text, TextBox39.Text, TextBox6.Text, TextBox13.Text, TextBox18.Text, TextBox24.Text, TextBox30.Text, TextBox40.Text) == true)
        {
            HiddenField5.Value = "1";
            ScriptManager.RegisterStartupScript(this, this.GetType(), "msg", "alert('Insered Successfully !');", true);
            resetall();
        }
        else
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "msg", "alert('Error in Insered Data !');", true);
        }
        Response.Redirect("../DayCare/PatientDashBoard.aspx");
    }
    public void gridfill()
    {
        string testdate;
        if (TextBox1.Text != "")
        {
            System.Globalization.DateTimeFormatInfo dtf = new DateTimeFormatInfo();
            testdate = DateTime.ParseExact(TextBox1.Text, "dd/MM/yyyy", dtf).ToString();
        }
        else
            testdate = "";


        GridView1.SelectedIndex = -1;
        GridView1.DataSource = themonitor.GridDetails(Session["CoCode"].ToString().Trim(), Session["YearCode"].ToString().Trim(), testdate, DropDownList2.SelectedValue);
        GridView1.DataBind();
    }
  
    protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        
        if (e.CommandName == "Select")
        {
            HiddenField5.Value = "2";
             if (HiddenField2.Value == "1")
            {
                Button1.Enabled = true;
                 int index = Convert.ToInt32(e.CommandArgument)-1;
               
                Panel1.Visible = true;
                Panel2.Visible = true;

                Label ID = (Label)GridView1.Rows[index].FindControl("ID");
                HiddenField1.Value = ID.Text;

                Label lblID = (Label)GridView1.Rows[index].FindControl("lblID");

                TextBox32.Text = lblID.Text;

                Label lblappodate = (Label)GridView1.Rows[index].FindControl("lblappodate");

                DataTable dt1 = themonitor.CountDialysis(Session["CoCode"].ToString().Trim(), lblID.Text);
                Label lblage = (Label)GridView1.Rows[index].FindControl("lblage");
                TextBox34.Text = lblage.Text;


                TextBox44.Text = dt1.Rows[0][0].ToString();

                Label lblAddress = (Label)GridView1.Rows[index].FindControl("lblAddress");
                TextBox35.Text = lblAddress.Text;

                Label lblname = (Label)GridView1.Rows[index].FindControl("lblname");
                TextBox33.Text = lblname.Text;

                System.Globalization.DateTimeFormatInfo dtf = new DateTimeFormatInfo();
                DateTime testdate = DateTime.ParseExact(lblappodate.Text, "dd/MM/yyyy", dtf);

                Label lblshiftId = (Label)GridView1.Rows[index].FindControl("lblshiftId");
                HiddenField3.Value = lblshiftId.Text;
                DataTable dt = themonitor.AllMonitor(Session["CoCode"].ToString().Trim(), Session["YearCode"].ToString().Trim(), lblID.Text, testdate.ToString());
                if (dt.Rows.Count > 0)
                {
                    TextBox42.Text = dt.Rows[0]["StartTime"].ToString();
                    TextBox43.Text = dt.Rows[0]["EndTime"].ToString();

                    TextBox45.Text = dt.Rows[0]["PreBP"].ToString();
                    TextBox46.Text = dt.Rows[0]["PreWeight"].ToString();
                    TextBox47.Text = dt.Rows[0]["PreHb"].ToString();
                    TextBox48.Text = dt.Rows[0]["Preurea"].ToString();
                    TextBox49.Text = dt.Rows[0]["Precritimine"].ToString();
                    TextBox50.Text = dt.Rows[0]["PreNA"].ToString();
                    TextBox51.Text = dt.Rows[0]["preK"].ToString();
                    TextBox52.Text = dt.Rows[0]["PostBP"].ToString();
                    TextBox53.Text = dt.Rows[0]["PostWeight"].ToString();
                    TextBox54.Text = dt.Rows[0]["PostHb"].ToString();
                    TextBox55.Text = dt.Rows[0]["Posturea"].ToString();
                    TextBox56.Text = dt.Rows[0]["postcreatinine"].ToString();
                    TextBox57.Text = dt.Rows[0]["PostNA"].ToString();
                    TextBox58.Text = dt.Rows[0]["PostK"].ToString();

                    TextBox2.Text = dt.Rows[0]["InterTime1"].ToString();
                    TextBox3.Text = dt.Rows[0]["InterBP1"].ToString();
                    TextBox4.Text = dt.Rows[0]["InterPulse1"].ToString();
                    TextBox5.Text = dt.Rows[0]["InterBlood1"].ToString();
                    TextBox6.Text = dt.Rows[0]["InterUFGoal1"].ToString();
                    TextBox7.Text = dt.Rows[0]["Comment1"].ToString();

                    TextBox9.Text = dt.Rows[0]["InterTime2"].ToString();
                    TextBox10.Text = dt.Rows[0]["InterBP2"].ToString();
                    TextBox11.Text = dt.Rows[0]["InterPulse2"].ToString();
                    TextBox12.Text = dt.Rows[0]["InterBlood2"].ToString();
                    TextBox13.Text = dt.Rows[0]["InterUFGoal2"].ToString();
                    TextBox14.Text = dt.Rows[0]["Comment2"].ToString();

                    TextBox8.Text = dt.Rows[0]["InterTime3"].ToString();
                    TextBox15.Text = dt.Rows[0]["InterBP3"].ToString();
                    TextBox16.Text = dt.Rows[0]["InterPulse3"].ToString();
                    TextBox17.Text = dt.Rows[0]["InterBlood3"].ToString();
                    TextBox18.Text = dt.Rows[0]["InterUFGoal3"].ToString();
                    TextBox19.Text = dt.Rows[0]["Comment3"].ToString();

                    TextBox20.Text = dt.Rows[0]["InterTime4"].ToString();
                    TextBox21.Text = dt.Rows[0]["InterBP4"].ToString();
                    TextBox22.Text = dt.Rows[0]["InterPulse4"].ToString();
                    TextBox23.Text = dt.Rows[0]["InterBlood4"].ToString();
                    TextBox24.Text = dt.Rows[0]["InterUFGoal4"].ToString();
                    TextBox25.Text = dt.Rows[0]["Comment4"].ToString();

                    TextBox26.Text = dt.Rows[0]["InterTime5"].ToString();
                    TextBox27.Text = dt.Rows[0]["InterBP5"].ToString();
                    TextBox28.Text = dt.Rows[0]["InterPulse5"].ToString();
                    TextBox29.Text = dt.Rows[0]["InterBlood5"].ToString();
                    TextBox30.Text = dt.Rows[0]["InterUFGoal5"].ToString();
                    TextBox31.Text = dt.Rows[0]["Comment5"].ToString();

                    TextBox36.Text = dt.Rows[0]["InterTime6"].ToString();
                    TextBox37.Text = dt.Rows[0]["InterBP6"].ToString();
                    TextBox38.Text = dt.Rows[0]["InterPulse6"].ToString();
                    TextBox39.Text = dt.Rows[0]["InterBlood6"].ToString();
                    TextBox40.Text = dt.Rows[0]["InterUFGoal6"].ToString();
                    TextBox41.Text = dt.Rows[0]["Comment6"].ToString();


                }

            }
        }
    }
   

    protected void btnSearch_Click(object sender, EventArgs e)
    {
        gridfill();
    }
    protected void GridView1_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
         GridView1.PageIndex = e.NewPageIndex;
       gridfill();
    }
    protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            Label lblSerial = (Label)e.Row.FindControl("lblSerial");
            lblSerial.Text = ((GridView1.PageIndex * GridView1.PageSize) + e.Row.RowIndex + 1).ToString();
        }
    }
}