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
using System.Globalization;
 
public partial class Documents_Sister_Aya_Charges : System.Web.UI.Page
{
    Main Objmain = new Main(ConfigurationSettings.AppSettings["ConnectionString"].ToString());
    Sister_Aya_Charges thecharge = new Sister_Aya_Charges(ConfigurationSettings.AppSettings["ConnectionString"].ToString());
    string dt = DateTime.Now.ToString("MM/dd/yyyy");
    protected void Page_Load(object sender, EventArgs e)
    {
        ScriptManager.RegisterStartupScript(this, this.GetType(), "msg", "GetDatetime();", true);
        Page.Title = "Sister Aya Charges";
        if (Session["userName"] == null)
        {
            Response.Redirect("../LoginPage.aspx");
        }
        if (Objmain.checkaccss(Session["CoCode"].ToString(), Session["UserRoleID"].ToString().Trim(), "SISTER / AYA SERVICES", checkAccessType.ViewAction) == false)
        {
            Response.Redirect("../AccessDenied.aspx");
        }

        if (Objmain.checkaccss(Session["CoCode"].ToString(), Session["UserRoleID"].ToString().Trim(), "SISTER / AYA SERVICES", checkAccessType.InsertAction) == false)
        {
            Button1.Enabled = false;
        }
        if (Objmain.checkaccss(Session["CoCode"].ToString(), Session["UserRoleID"].ToString().Trim(), "SISTER / AYA SERVICES", checkAccessType.DeleteAction) == false)
        {
            GridView1.Columns[12].Visible = false;
        }
        if (!IsPostBack)
        {
            Tab1Func();
            DropDownFill(); GridFill();
            if (Session["RegNo"] != null)
            {
                TextBox1.Text = Session["RegNo"].ToString();
                GridFill();
            }
        }
        Session["RegNo"] = null;
    }

    public void DropDownFill()
    {
        for (int d = 1; d <= 20; d = d + 2)
        {
           
            DropDownList d1 = (DropDownList)Page.FindControl("ctl00$ContentPlaceHolder1$DropDownList" + d.ToString());
            DropDownList d2 = (DropDownList)Page.FindControl("ctl00$ContentPlaceHolder1$DropDownList" + (d+1).ToString());
            d1.DataSource = thecharge.GetAllType();
            d1.DataTextField = "TypeName";
            d1.DataValueField = "ID";
            d1.DataBind();
            d1.Items.Insert(0, new ListItem("--Select--", "0"));


            d2.DataSource = thecharge.GetAllShift();
            d2.DataTextField = "ShiftName";
            d2.DataValueField = "ID";
            d2.DataBind();
            d2.Items.Insert(0, new ListItem("--Select--", "0"));
        }
    }
    public void FillDtls(DataTable dt)
    {
        for (int i=0,d = 1, t = 5; i<dt.Rows.Count;i++, d = d + 2, t = t + 3)
        {
            System.Globalization.DateTimeFormatInfo dtf = new DateTimeFormatInfo();

            DropDownList d1 = (DropDownList)Page.FindControl("ctl00$ContentPlaceHolder1$DropDownList" + d.ToString());
            DropDownList d2 = (DropDownList)Page.FindControl("ctl00$ContentPlaceHolder1$DropDownList" + (d + 1).ToString());
            TextBox t1 = (TextBox)Page.FindControl("ctl00$ContentPlaceHolder1$TextBox" + t.ToString());
            TextBox t2 = (TextBox)Page.FindControl("ctl00$ContentPlaceHolder1$TextBox" + (t + 1).ToString());
            TextBox t3 = (TextBox)Page.FindControl("ctl00$ContentPlaceHolder1$TextBox" + (t + 2).ToString());
            CheckBox c = (CheckBox)Page.FindControl("ctl00$ContentPlaceHolder1$chksys" + (i +1).ToString());
            d1.SelectedValue = dt.Rows[i]["Type"].ToString();
            d2.SelectedValue = dt.Rows[i]["Shift"].ToString();
            t1.Text = dt.Rows[i]["Charges1"].ToString();
            t2.Text = dt.Rows[i]["dt"].ToString();
            t3.Text = dt.Rows[i]["Remarks"].ToString();
            if (dt.Rows[i]["cont"].ToString() == "1")
            {
                c.Checked = true;
            }
            else
            {
                c.Checked = false;
            }
        }
    }
    private void GridFill()
    {
        DataTable dt = thecharge.GetAllhospital(Session["CoCode"].ToString().Trim(),TextBox1.Text);
        DataTable dt1 = thecharge.GetonlyPatient(Session["CoCode"].ToString().Trim(),TextBox1.Text);
        if (dt.Rows.Count > 0)
        {

            TextBox1.Text = dt1.Rows[0]["PatientReg"].ToString();
            TextBox2.Text = dt1.Rows[0]["patient_name"].ToString();
            TextBox3.Text = dt1.Rows[0]["BedNoText"].ToString();
            TextBox4.Text = dt1.Rows[0]["adate"].ToString();
            GridView1.DataSource = dt;
            GridView1.DataBind();
            //FillDtls(dt);
            //Button1.Text = "Update";
        }
        if (dt1.Rows.Count > 0)
        {
            TextBox1.Text = dt1.Rows[0]["PatientReg"].ToString();
            TextBox2.Text = dt1.Rows[0]["patient_name"].ToString();
            TextBox3.Text = dt1.Rows[0]["BedNoText"].ToString();
            TextBox4.Text = dt1.Rows[0]["adate"].ToString();
        }
        

    }

    protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            if (Objmain.checkaccss(Session["CoCode"].ToString(), Session["UserRoleID"].ToString().Trim(), "SISTER / AYA SERVICES", checkAccessType.DeleteAction) == false)
            {
                e.Row.Cells[12].Visible = false;
            }
        }

        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            Label lblcont = (Label)e.Row.FindControl("lblcont");
            CheckBox cont = (CheckBox)e.Row.FindControl("chksysaya");
            if (lblcont.Text == "1")
            {
                cont.Checked = true;
            }
            else
            {
                cont.Checked = false;
            }
        }
    }
    private void ResetAllFields()
    {
        HiddenField1.Value = "";
        Button1.Text = "Submit";
        for (int i = 1; i <= 34; i++)
        {
          TextBox  t1 = (TextBox)Page.FindControl("ctl00$ContentPlaceHolder1$TextBox" + i.ToString());
          t1.Text = "";          
        }
        for (int i = 1; i <= 20; i++)
        {
            DropDownList d1 = (DropDownList)Page.FindControl("ctl00$ContentPlaceHolder1$DropDownList" + i.ToString());
            d1.SelectedIndex = 0;
        }
        if (Objmain.checkaccss(Session["CoCode"].ToString(), Session["UserRoleID"].ToString().Trim(), "SISTER / AYA SERVICES", checkAccessType.InsertAction) == false)
        {
            Button1.Enabled = false;
        }
       
    }
    public void Insert()
    {
        string cont="";
        for (int d = 1, t = 5,i=1; d <= 20; d = d + 2, t = t + 3,i++)
        {
            System.Globalization.DateTimeFormatInfo dtf = new DateTimeFormatInfo();

            DropDownList d1 = (DropDownList)Page.FindControl("ctl00$ContentPlaceHolder1$DropDownList" + d.ToString());
            DropDownList d2 = (DropDownList)Page.FindControl("ctl00$ContentPlaceHolder1$DropDownList" + (d + 1).ToString());
            TextBox t1 = (TextBox)Page.FindControl("ctl00$ContentPlaceHolder1$TextBox" + t.ToString());
            TextBox t2 = (TextBox)Page.FindControl("ctl00$ContentPlaceHolder1$TextBox" + (t + 1).ToString());
            TextBox t3 = (TextBox)Page.FindControl("ctl00$ContentPlaceHolder1$TextBox" + (t + 2).ToString());
            CheckBox c = (CheckBox)Page.FindControl("ctl00$ContentPlaceHolder1$chksys" + i.ToString());
            if(c.Checked==true)
            {
                cont="1";
            }
            else
            {
                cont="0";
            }
            if (t1.Text != "" && t2.Text != "")
            {
                DateTime testdate = DateTime.ParseExact(t2.Text, "dd/MM/yyyy", dtf);
                if (thecharge.InsertSister_Aya_Charges(HiddenField2.Value, TextBox1.Text, d1.SelectedValue, d2.SelectedValue, t1.Text, testdate.ToString("yyyy-MM-dd"), t3.Text, Session["CoCode"].ToString(),cont) == true)
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "msg", "alert('Inserted  Successfully !');", true);
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "msg", "alert('Error in Inserted Data !');", true);
                }
            }
            else
                break;
        }
    }
    protected void Button1_Click(object sender, EventArgs e)
    {
        if (Button1.Text == "Submit")
        {
            Insert();
            Response.Redirect("../IPD/AdmissionPatientList.aspx");
        }
        else
        {
            if (HiddenField1.Value == "")
            {
                thecharge.DeleteSister_Aya_Charges("0", TextBox1.Text, Session["CoCode"].ToString());
                Insert();
                Response.Redirect("../IPD/AdmissionPatientList.aspx");
            }
            else
            {
                System.Globalization.DateTimeFormatInfo dtf = new DateTimeFormatInfo();
                DateTime testdate = DateTime.ParseExact(TextBox6.Text, "dd/MM/yyyy", dtf);
                thecharge.UpdateSister_Aya_Charges(HiddenField1.Value, TextBox1.Text, DropDownList1.SelectedValue, DropDownList2.SelectedValue, TextBox5.Text, testdate.ToString("yyyy-MM-dd"), TextBox7.Text, Session["CoCode"].ToString());
            }
            ScriptManager.RegisterStartupScript(this, this.GetType(), "msg", "alert('Updated  Successfully !');", true);
            Button1.Text = "Submit";
        }
        GridFill();
        ResetAllFields();
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
            ResetAllFields();
            int index = Convert.ToInt32(e.CommandArgument);
            Button1.Text = "Update";

            Label lblId = (Label)GridView1.Rows[index].FindControl("lblId");
            HiddenField1.Value = lblId.Text;

            Label lblreg = (Label)GridView1.Rows[index].FindControl("lblreg");
           TextBox1.Text = lblreg.Text;

           Label lblname = (Label)GridView1.Rows[index].FindControl("lblname");
           TextBox2.Text = lblname.Text;

           Label lblbedno = (Label)GridView1.Rows[index].FindControl("lblbedno");
           TextBox3.Text = lblbedno.Text;

           Label lbladate = (Label)GridView1.Rows[index].FindControl("lbladate");
           TextBox4.Text = lbladate.Text;

           Label lbltype = (Label)GridView1.Rows[index].FindControl("lbltype");
            DropDownList1.SelectedIndex = SearchIndex(lbltype.Text, DropDownList1);

            Label lblDayShift = (Label)GridView1.Rows[index].FindControl("lblDayShift");
            DropDownList2.SelectedIndex = SearchIndex(lblDayShift.Text, DropDownList2);

            Label lblCharges = (Label)GridView1.Rows[index].FindControl("lblCharges");
            TextBox5.Text = lblCharges.Text;

            Label lblDate = (Label)GridView1.Rows[index].FindControl("lblDate");
            TextBox6.Text = lblDate.Text;

            Label lblRemarks = (Label)GridView1.Rows[index].FindControl("lblRemarks");
            TextBox7.Text = lblRemarks.Text;
            Tab1Func();
            Button1.Text = "Update";
            if (Objmain.checkaccss(Session["CoCode"].ToString(), Session["UserRoleID"].ToString().Trim(), "SISTER / AYA SERVICES", checkAccessType.UpdateAction) == false)
            {
                Button1.Enabled = false;
            }
            else
            {
                Button1.Enabled = true;
            }
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

    protected void GridView1_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
         Label lblId = (Label)GridView1.Rows[e.RowIndex].FindControl("lblId");
         thecharge.DeleteSister_Aya_Charges(lblId.Text, TextBox1.Text, Session["CoCode"].ToString());
        lblError.ForeColor = System.Drawing.Color.Green;
        lblError.Text = "Deleted Successfully";
        GridFill();
        ResetAllFields();
    }


    protected void Button4_Click(object sender, EventArgs e)
    {
        GridFill();
    }
    protected void DropDownList2_SelectedIndexChanged(object sender, EventArgs e)
    {
        FillCharge(DropDownList1,DropDownList2,TextBox5);
    }

    public void FillCharge(DropDownList d1,DropDownList d2,TextBox t)
    {
        lblError.Text = "";
        t.Text = "";
        if (d1.SelectedIndex != 0)
        {

            DataTable dt = thecharge.GetCharges(d1.SelectedValue);
            if (d2.SelectedValue == "0")
            {
                lblError.ForeColor = System.Drawing.Color.Red;
                lblError.Text = "Select Shift  !!";
            }
            if (d2.SelectedValue == "1")
                t.Text = dt.Rows[0]["DayCharge"].ToString();
            if (d2.SelectedValue == "2")
                t.Text = dt.Rows[0]["NightCharge"].ToString();
            if (d2.SelectedValue == "3")
                t.Text = dt.Rows[0]["DayNightCharge"].ToString();
        }
        else
        {
            lblError.ForeColor = System.Drawing.Color.Red;
            lblError.Text = "Select Type  !!";
        }
    }
    protected void DropDownList4_SelectedIndexChanged(object sender, EventArgs e)
    {
        FillCharge(DropDownList3, DropDownList4, TextBox8);
    }
    protected void DropDownList8_SelectedIndexChanged(object sender, EventArgs e)
    {
      FillCharge(DropDownList7,DropDownList8,TextBox14);
    }
    protected void DropDownList6_SelectedIndexChanged(object sender, EventArgs e)
    {
        FillCharge(DropDownList5, DropDownList6, TextBox11);
    }
    protected void DropDownList10_SelectedIndexChanged(object sender, EventArgs e)
    {
        FillCharge(DropDownList9, DropDownList10, TextBox17);
    }
    protected void DropDownList12_SelectedIndexChanged(object sender, EventArgs e)
    {
        FillCharge(DropDownList11, DropDownList12, TextBox20);
    }
    protected void DropDownList14_SelectedIndexChanged(object sender, EventArgs e)
    {
        FillCharge(DropDownList13, DropDownList14, TextBox23);
    }
    protected void DropDownList16_SelectedIndexChanged(object sender, EventArgs e)
    {
        FillCharge(DropDownList15, DropDownList16, TextBox26);
    }
    protected void DropDownList18_SelectedIndexChanged(object sender, EventArgs e)
    {
        FillCharge(DropDownList17, DropDownList18, TextBox29);
    }
    protected void DropDownList20_SelectedIndexChanged(object sender, EventArgs e)
    {
        FillCharge(DropDownList19, DropDownList20, TextBox32);
    }
    protected void MainView_ActiveViewChanged(object sender, EventArgs e)
    {

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
