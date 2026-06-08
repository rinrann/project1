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
 

public partial class IPD_AttendenceFees : System.Web.UI.Page
{
    Main Objmain = new Main(ConfigurationSettings.AppSettings["ConnectionString"].ToString());
    AttendenceFee theotconsumable = new AttendenceFee(ConfigurationSettings.AppSettings["ConnectionString"].ToString());
    TextBox t1,t2,t3,t4,t5,t6,t7,t8,t9,t10;
    protected void Page_Load(object sender, EventArgs e)
    {

        Page.Title = "Attendence Fees";
        if (Session["userName"] == null)
        {
            Response.Redirect("../LoginPage.aspx");
        }
        if (Objmain.checkaccss(Session["CoCode"].ToString(), Session["UserRoleID"].ToString().Trim(), "ATTENDENCE FEES", checkAccessType.ViewAction) == false)
        {
            Response.Redirect("../AccessDenied.aspx");
        }

        if (Objmain.checkaccss(Session["CoCode"].ToString(), Session["UserRoleID"].ToString().Trim(), "ATTENDENCE FEES", checkAccessType.InsertAction) == false)
        {
            Button1.Enabled = false;
        }
        if (!IsPostBack)
        {

            DropDownFill();
            Tab1Func();

            if (Session["ReqNo"] != null)
            {
                  TextBox2.Text = Session["ReqNo"].ToString();
                  DataTable dt = theotconsumable.Getonlyreg(Session["CoCode"].ToString().Trim(), Session["YearCode"].ToString().Trim(), TextBox2.Text);
                TextBox1.Text = dt.Rows[0][0].ToString();
               GridFillSecond();
               GridFill();

            }
            Session["ReqNo"] = null;
        }


    }

    private void GridFill()
    {
        DataTable dt = theotconsumable.FetchFron2ndGrid(Session["CoCode"].ToString().Trim(), Session["YearCode"].ToString().Trim(), TextBox1.Text, TextBox2.Text);
        DataTable dt1 = theotconsumable.Getonlypat(Session["CoCode"].ToString().Trim(), Session["YearCode"].ToString().Trim(), TextBox1.Text, TextBox2.Text);
        if (dt.Rows.Count > 0)
        {
            //GridView1.DataSource = dt;
            //GridView1.DataBind();
            TextBox7.Text = dt.Rows[0]["issudate"].ToString();
            HiddenField1.Value = dt.Rows[0]["RowID"].ToString();
            TextBox2.Text = dt.Rows[0]["OperationReqID"].ToString();
            TextBox3.Text = dt.Rows[0]["patient_name"].ToString();
            TextBox4.Text = dt.Rows[0]["BedNoText"].ToString();
            TextBox5.Text = dt.Rows[0]["OperationName"].ToString();
            TextBox6.Text = dt.Rows[0]["adate"].ToString();
            FillMedicineDtls(dt);
            Button1.Text = "Update";
            if (Objmain.checkaccss(Session["CoCode"].ToString(), Session["UserRoleID"].ToString().Trim(), "ATTENDENCE FEES", checkAccessType.UpdateAction) == false)
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
                DropFullFill();
                TextBox2.Text = dt1.Rows[0]["OperationReqID"].ToString();
                TextBox3.Text = dt1.Rows[0]["patient_name"].ToString();
                TextBox4.Text = dt1.Rows[0]["BedNoText"].ToString();
                TextBox5.Text = dt1.Rows[0]["OperationName"].ToString();
                TextBox6.Text = dt1.Rows[0]["adate"].ToString();

            }

        }
    }

    public void DropFullFill()
    {
        DataTable dt = theotconsumable.GetFromOTNote(Session["CoCode"].ToString().Trim(), Session["YearCode"].ToString().Trim(), TextBox1.Text, TextBox2.Text);
       if (dt.Rows.Count > 0)
       {
           DropDownList1.SelectedValue = dt.Rows[0]["SurgeonID"].ToString();
           DropDownList2.SelectedValue = dt.Rows[0]["AdditionalDoctor1"].ToString();
           DropDownList3.SelectedValue = dt.Rows[0]["AdditionalDoctor2"].ToString();
           DropDownList4.SelectedValue = dt.Rows[0]["AdditionalDoctor3"].ToString();
           DropDownList5.SelectedValue = dt.Rows[0]["AnesthetistName1"].ToString();
           DropDownList6.SelectedValue = dt.Rows[0]["AnesthetistName2"].ToString();
           DropDownList7.SelectedValue = dt.Rows[0]["Assistant1"].ToString();
           DropDownList8.SelectedValue = dt.Rows[0]["Assistant2"].ToString();
           DropDownList9.SelectedValue = dt.Rows[0]["Assistant3"].ToString();
       }
    }
    public void FillMedicineDtls(DataTable dt)
    {
        DropFullFill();
        HiddenField h1;
        for (int i = 0,h=1, t =9; i < dt.Rows.Count; i++, t = t + 10,h++)
        {
            h1 = (HiddenField)Page.FindControl("ctl00$ContentPlaceHolder1$HiddenField" + h.ToString());
            t1 = (TextBox)Page.FindControl("ctl00$ContentPlaceHolder1$TextBox" + t.ToString());
            t2 = (TextBox)Page.FindControl("ctl00$ContentPlaceHolder1$TextBox" + (t + 1).ToString());
            t3 = (TextBox)Page.FindControl("ctl00$ContentPlaceHolder1$TextBox" + (t + 2).ToString());
            t4 = (TextBox)Page.FindControl("ctl00$ContentPlaceHolder1$TextBox" + (t + 3).ToString());
            t5= (TextBox)Page.FindControl("ctl00$ContentPlaceHolder1$TextBox" + (t + 4).ToString());
            t6 = (TextBox)Page.FindControl("ctl00$ContentPlaceHolder1$TextBox" + (t + 5).ToString());
            t7 = (TextBox)Page.FindControl("ctl00$ContentPlaceHolder1$TextBox" + (t + 6).ToString());
            t8 = (TextBox)Page.FindControl("ctl00$ContentPlaceHolder1$TextBox" + (t + 7).ToString());
            t9 = (TextBox)Page.FindControl("ctl00$ContentPlaceHolder1$TextBox" + (t + 8).ToString());
            t10 = (TextBox)Page.FindControl("ctl00$ContentPlaceHolder1$TextBox" + (t + 9).ToString());
            h1.Value = dt.Rows[i]["RowID"].ToString();
            t1.Text = dt.Rows[i]["SurgeonCharge"].ToString();
            t2.Text = dt.Rows[i]["Doc1Charge"].ToString();
            t3.Text = dt.Rows[i]["Doc2Charge"].ToString();
            t4.Text = dt.Rows[i]["Doc3Charge"].ToString();
            t5.Text = dt.Rows[i]["AnesthetistCharge1"].ToString();
            t6.Text = dt.Rows[i]["AnesthetistCharge2"].ToString();
            t7.Text = dt.Rows[i]["Asst1Charge"].ToString();
            t8.Text = dt.Rows[i]["Asst2Charge"].ToString();
            t9.Text = dt.Rows[i]["Asst3Charge"].ToString();
            t10.Text = dt.Rows[i]["Remarks"].ToString();
       }
    }
    private void GridFillSecond()
    {
        //select v.Rowid, pr.PatientReg,pr.,CONVERT(varchar,pr.AdmissionDate,103) adate,bm.BedNoText

        DataTable dt = theotconsumable.GridFillSecond(Session["CoCode"].ToString().Trim(), Session["YearCode"].ToString().Trim(), TextBox1.Text);
        if (dt.Rows.Count > 0)
        {
            GridView2.DataSource = dt;
            GridView2.DataBind();
        }
    }
    private void DropDownFill()
    {

        for (int i = 1; i <= 9; i++)
        {
              if (i >= 1 && i <= 6)
            {
                DropDownList d1 = (DropDownList)Page.FindControl("ctl00$ContentPlaceHolder1$DropDownList" + i.ToString());
                d1.DataSource = theotconsumable.DropDownDoctor(Session["CoCode"].ToString().Trim());
                d1.DataTextField = "doc_name";
                d1.DataValueField = "doc_id";
                d1.DataBind();
                d1.Items.Insert(0, new ListItem("--Select--", "0"));
            }
            else
            {

                DropDownList d2 = (DropDownList)Page.FindControl("ctl00$ContentPlaceHolder1$DropDownList" + i.ToString());
                d2.DataSource = theotconsumable.DropDownEmployee(Session["CoCode"].ToString().Trim());
                d2.DataTextField = "EmployeeName";
                d2.DataValueField = "EmployeeID";
                d2.DataBind();
                d2.Items.Insert(0, new ListItem("--Select--", "0"));
            }

        }

    }
    public void ResetAllFields()
    {
        HiddenField1.Value = "";
        Button1.Text = "Submit";
        for (int i = 1; i <= 9; i++)
        {
            DropDownList d1 = (DropDownList)Page.FindControl("ctl00$ContentPlaceHolder1$DropDownList" + i.ToString());
            d1.SelectedIndex = 0;
        }
        for (int t = 1; t <= 28; t++)
        {
            t1 = (TextBox)Page.FindControl("ctl00$ContentPlaceHolder1$TextBox" + t.ToString());
            t1.Text = "";
        }
        if (Objmain.checkaccss(Session["CoCode"].ToString(), Session["UserRoleID"].ToString().Trim(), "ATTENDENCE FEES", checkAccessType.InsertAction) == false)
        {
            Button1.Enabled = false;
        }
    }

    protected void GridView2_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "Select")
        {
            ResetAllFields();
            int index = Convert.ToInt32(e.CommandArgument);
            lblError.Text = "";
            Label lblid = (Label)GridView2.Rows[index].FindControl("lblid");
            TextBox2.Text = lblid.Text;

            Label lblRegno = (Label)GridView2.Rows[index].FindControl("lblRegno");
            TextBox1.Text = lblRegno.Text;
           GridFill();
           Tab1Func();
        }
    }
    protected void GridView2_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {

    } 
    protected void Button2_Click(object sender, EventArgs e)
    {
        ResetAllFields();
    }

    public void Insertdata()
    {
         int count = 0; string type;
        System.Globalization.DateTimeFormatInfo dtf = new DateTimeFormatInfo();
        DateTime testdate = DateTime.ParseExact(TextBox7.Text, "dd/MM/yyyy", dtf);
        for (int t = 9; t<20; t = t + 10)
        {
            if (count == 0)
                type = "1";//actual paid
            else
                type = "2";//Bill Reflection(paid by patient)
            t1 = (TextBox)Page.FindControl("ctl00$ContentPlaceHolder1$TextBox" + t.ToString());
            t2 = (TextBox)Page.FindControl("ctl00$ContentPlaceHolder1$TextBox" + (t + 1).ToString());
            t3 = (TextBox)Page.FindControl("ctl00$ContentPlaceHolder1$TextBox" + (t + 2).ToString());
            t4 = (TextBox)Page.FindControl("ctl00$ContentPlaceHolder1$TextBox" + (t + 3).ToString());
            t5 = (TextBox)Page.FindControl("ctl00$ContentPlaceHolder1$TextBox" + (t + 4).ToString());
            t6 = (TextBox)Page.FindControl("ctl00$ContentPlaceHolder1$TextBox" + (t + 5).ToString());
            t7 = (TextBox)Page.FindControl("ctl00$ContentPlaceHolder1$TextBox" + (t + 6).ToString());
            t8 = (TextBox)Page.FindControl("ctl00$ContentPlaceHolder1$TextBox" + (t + 7).ToString());
            t9 = (TextBox)Page.FindControl("ctl00$ContentPlaceHolder1$TextBox" + (t + 8).ToString());
            t10 = (TextBox)Page.FindControl("ctl00$ContentPlaceHolder1$TextBox" + (t + 9).ToString());
            count++;
            if (t1.Text == "")
                t1.Text = "0";
            if (t2.Text == "")
                t2.Text = "0";
            if (t3.Text == "")
                t3.Text = "0";
            if (t4.Text == "")
                t4.Text = "0";
            if (t5.Text == "")
                t5.Text = "0";
            if (t6.Text == "")
                t6.Text = "0";
            if (t7.Text == "")
                t7.Text = "0";
            if (t8.Text == "")
                t8.Text = "0";
            if (t9.Text == "")
                t9.Text = "0";
            if (t10.Text == "")
                t10.Text = "0";
            if (theotconsumable.InsertAttendencecharge(type, TextBox1.Text, TextBox2.Text, t1.Text, t2.Text, t3.Text, t4.Text, t5.Text, t6.Text, t7.Text, t8.Text, t9.Text, testdate.ToString("yyyy-MM-dd"), t10.Text, Session["CoCode"].ToString().Trim(), Session["userName"].ToString().Trim(), Session["YearCode"].ToString().Trim()) == true)
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "msg", "alert('Insered Successfully !');", true);
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "msg", "alert('Error in Insered Data !');", true);
                break;
            }    
            
        }
    }
    protected void Button1_Click(object sender, EventArgs e)
    {
        System.Globalization.DateTimeFormatInfo dtf = new DateTimeFormatInfo();
        DateTime testdate = DateTime.ParseExact(TextBox7.Text, "dd/MM/yyyy", dtf);
      
        if (Button1.Text == "Submit")
        {
            Insertdata();
        }
        else
        { 
             HiddenField h1;
            for (int t = 9,i=1; t < 20; t = t + 10,i++)
            {
               
                t1 = (TextBox)Page.FindControl("ctl00$ContentPlaceHolder1$TextBox" + t.ToString());
                t2 = (TextBox)Page.FindControl("ctl00$ContentPlaceHolder1$TextBox" + (t + 1).ToString());
                t3 = (TextBox)Page.FindControl("ctl00$ContentPlaceHolder1$TextBox" + (t + 2).ToString());
                t4 = (TextBox)Page.FindControl("ctl00$ContentPlaceHolder1$TextBox" + (t + 3).ToString());
                t5 = (TextBox)Page.FindControl("ctl00$ContentPlaceHolder1$TextBox" + (t + 4).ToString());
                t6 = (TextBox)Page.FindControl("ctl00$ContentPlaceHolder1$TextBox" + (t + 5).ToString());
                t7 = (TextBox)Page.FindControl("ctl00$ContentPlaceHolder1$TextBox" + (t + 6).ToString());
                t8 = (TextBox)Page.FindControl("ctl00$ContentPlaceHolder1$TextBox" + (t + 7).ToString());
                t9 = (TextBox)Page.FindControl("ctl00$ContentPlaceHolder1$TextBox" + (t + 8).ToString());
                t10 = (TextBox)Page.FindControl("ctl00$ContentPlaceHolder1$TextBox" + (t + 9).ToString());
                h1 = (HiddenField)Page.FindControl("ctl00$ContentPlaceHolder1$HiddenField" +i.ToString());

                if (t1.Text == "")
                    t1.Text = "0";
                if (t2.Text == "")
                    t2.Text = "0";
                if (t3.Text == "")
                    t3.Text = "0";
                if (t4.Text == "")
                    t4.Text = "0";
                if (t5.Text == "")
                    t5.Text = "0";
                if (t6.Text == "")
                    t6.Text = "0";
                if (t7.Text == "")
                    t7.Text = "0";
                if (t8.Text == "")
                    t8.Text = "0";
                if (t9.Text == "")
                    t9.Text = "0";
                if (t10.Text == "")
                    t10.Text = "0";

                if (theotconsumable.UpdateAttCharge(h1.Value, t1.Text, t2.Text, t3.Text, t4.Text, t5.Text, t6.Text, t7.Text, t8.Text, t9.Text, testdate.ToString("yyyy-MM-dd"), t10.Text, Session["CoCode"].ToString().Trim(), Session["userName"].ToString().Trim(), Session["YearCode"].ToString().Trim()) == true)
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "msg", "alert('Updated Successfully !');", true);
                }
                else
                {

                    ScriptManager.RegisterStartupScript(this, this.GetType(), "msg", "alert('Error in Updated Data !');", true);
                 
                }
            }
          
        }
        Response.Redirect("../IPD/OTList.aspx");
        ResetAllFields();
    }
    protected void Button4_Click(object sender, EventArgs e)
    {
        Dtls();
        GridFillSecond();
    }
    private void Dtls()
    {
        DataTable dtreq = theotconsumable.Getonltreq(Session["CoCode"].ToString().Trim(), Session["YearCode"].ToString().Trim(), TextBox1.Text);
        lblError.Text = "";
        if (dtreq.Rows.Count > 0)
        {
            TextBox2.Text = dtreq.Rows[0][0].ToString();
            GridFill();
        }
        else
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "msg", "alert('No Operation is Done !');", true);
        }

    }


    public void Tab1Func()
    {
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
}