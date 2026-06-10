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
 
public partial class OPD_PatientAppointment : System.Web.UI.Page
{
    static int flag = 0;
    Main Objmain = new Main(ConfigurationSettings.AppSettings["ConnectionString"].ToString());
    PatientAppointment thepatientAppo = new PatientAppointment(ConfigurationSettings.AppSettings["ConnectionString"].ToString());
    protected void Page_Load(object sender, EventArgs e)
    {
        Page.Title = "PATIENT APPOINTMENTS";
        
        if (Session["userName"] == null)
        {
            Response.Redirect("../LoginPage.aspx");
        }

        if (Objmain.checkaccss(Session["CoCode"].ToString(), Session["UserRoleID"].ToString().Trim(), "PATIENT APPOINTMENTS", checkAccessType.ViewAction) == false)
        {
            Response.Redirect("../AccessDenied.aspx");
        }

        if (Objmain.checkaccss(Session["CoCode"].ToString(), Session["UserRoleID"].ToString().Trim(), "PATIENT APPOINTMENTS", checkAccessType.InsertAction) == false)
        {
            Button1.Enabled = false;
        }

        if (Objmain.checkaccss(Session["CoCode"].ToString(), Session["UserRoleID"].ToString().Trim(), "PATIENT APPOINTMENTS", checkAccessType.DeleteAction) == false)
        {
            GridView1.Columns[18].Visible = false;
        }

        if (!IsPostBack)
        {
            DropDownFill();
            Tab1Func();
            GenerateRegCode();
            GenerateAppoCode();
            GridFill();
            DropDownList1.SelectedValue = "2";
            DropDownList9.SelectedValue = "5";
            TextBox12.Text = DateTime.Now.ToString("yyyy-MM-dd");
        }
    }

    private void GenerateAppoCode()
    {
        DataTable dt = thepatientAppo.GenerateReqNo(Session["CoCode"].ToString().Trim());
        TextBox1.Text = dt.Rows[0][0].ToString();
    }

    private void GenerateRegCode()
    {
        DataTable dt = thepatientAppo.GenerateRegno(Session["CoCode"].ToString().Trim(), Session["YearCode"].ToString().Trim());
        TextBox2.Text = dt.Rows[0][0].ToString();
    }
    private void ResetAllFields()
    {
        for (int t = 1; t <= 15; t++)
        {
            TextBox t1 = (TextBox)Page.FindControl("ctl00$ContentPlaceHolder1$TextBox" + t.ToString());
            if (t == 6 || t == 8 || t == 11)
                continue;
            t1.Text = "";
        }

        for (int d = 1; d <= 4; d++)
        {
            DropDownList d1 = (DropDownList)Page.FindControl("ctl00$ContentPlaceHolder1$DropDownList" + d.ToString());
            d1.SelectedIndex = 0;
        }
        
        Button1.Text = "Submit";
        GenerateRegCode();
        GenerateAppoCode();

        if (Objmain.checkaccss(Session["CoCode"].ToString(), Session["UserRoleID"].ToString().Trim(), "PATIENT APPOINTMENTS", checkAccessType.InsertAction) == false)
        {
            Button1.Enabled = false;
        }

    }
    private void DropDownFill()
    {
        string compcode = Session["CoCode"].ToString().Trim();

        this.DropDownList1.DataSource = thepatientAppo.DropdownSex();
        this.DropDownList1.DataTextField = "SexName";
        this.DropDownList1.DataValueField = "ID";
        this.DropDownList1.DataBind();
        this.DropDownList1.Items.Insert(0, new ListItem("--Select--", "0"));

        this.DropDownList2.DataSource = thepatientAppo.DropdownDocType(compcode);
        this.DropDownList2.DataTextField = "TypeName";
        this.DropDownList2.DataValueField = "DocTypeId";
        this.DropDownList2.DataBind();
        this.DropDownList2.Items.Insert(0, new ListItem("--Select--", "0"));

        this.DropDownList3.Items.Insert(0, new ListItem("--Select--", "0"));

        this.DropDownList4.DataSource = thepatientAppo.DropPatientType(compcode);
        this.DropDownList4.DataTextField = "TypeName";
        this.DropDownList4.DataValueField = "TypeId";
        this.DropDownList4.DataBind();
        this.DropDownList4.Items.Insert(0, new ListItem("--Select--", "0"));

        DropDownList9.Items.Clear();
        this.DropDownList9.DataSource = thepatientAppo.GetDistrict();
        this.DropDownList9.DataTextField = "DistrictName";
        this.DropDownList9.DataValueField = "ID";
        this.DropDownList9.DataBind();
        this.DropDownList9.Items.Insert(0, new ListItem("--Select--", "0"));
    }

    private void GridFill()
    {
        GridView1.DataSource = thepatientAppo.GridFill(Session["CoCode"].ToString().Trim(), Session["YearCode"].ToString().Trim());
        GridView1.DataBind();
    }
    protected void Button2_Click(object sender, EventArgs e)
    {
        ResetAllFields();
    }
    protected void Button1_Click(object sender, EventArgs e)
    {
        string id;
        System.Globalization.DateTimeFormatInfo dtf = new DateTimeFormatInfo();
        //DateTime testdate = DateTime.ParseExact(TextBox12.Text, "dd/MM/yyyy", dtf);
        DataTable OffDay = thepatientAppo.CheckDayoff(Session["CoCode"].ToString().Trim(), DropDownList3.SelectedValue, TextBox12.Text);
        if (OffDay.Rows.Count > 0)
        {
            lblError.ForeColor = System.Drawing.Color.Red;
            lblError.Text = "Appointment Can not possible due to " + OffDay.Rows[0]["DayoffReason"].ToString();
        }
        else
        {
            string ph1 = TextBox6.Text + " " + TextBox7.Text;
            string ph2 = TextBox8.Text + " " + TextBox9.Text;
            if (HiddenField1.Value != "")
            {
                id = HiddenField1.Value;
            }
            else
            {
                id = "null";
            }
            if (Button1.Text == "Submit")
            {

                if (thepatientAppo.InsertAppointment(Session["CoCode"].ToString().Trim(), Session["YearCode"].ToString().Trim(), TextBox1.Text, TextBox2.Text, TextBox3.Text, TextBox4.Text, TextBox5.Text, DropDownList1.SelectedValue, ph1, ph2, TextBox10.Text, DropDownList9.SelectedValue, DropDownList2.SelectedValue, DropDownList3.SelectedValue, TextBox12.Text, TextBox13.Text, DropDownList4.SelectedValue, TextBox14.Text, TextBox15.Text, "2", Session["userName"].ToString(), DateTime.Now.ToString("yyyy-MM-dd")) == true)
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "msg", "alert('Inserted  Successfully !');", true);
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "msg", "alert('Error in Inserted Data !');", true);
                }



            }
            else
            {

                if (thepatientAppo.UpdateAppointment(Session["CoCode"].ToString().Trim(), Session["YearCode"].ToString().Trim(), HiddenField1.Value, TextBox2.Text, TextBox3.Text, TextBox4.Text, TextBox5.Text, DropDownList1.SelectedValue, ph1, ph2, TextBox10.Text, DropDownList9.SelectedValue, DropDownList2.SelectedValue, DropDownList3.SelectedValue, TextBox12.Text, TextBox13.Text, DropDownList4.SelectedValue, TextBox14.Text, TextBox15.Text, "2", Session["userName"].ToString(), DateTime.Now.ToString("yyyy-MM-dd")) == true)
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "msg", "alert('Updated  Successfully !');", true);
                    Button1.Text = "Submit";
                    GridView1.SelectedRowStyle.BackColor = System.Drawing.Color.Empty;
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "msg", "alert('Error in Updated Data !');", true);
                }

            }
        }

        GridFill();
        ResetAllFields();
    }
    protected void GridView1_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        GridView1.PageIndex = e.NewPageIndex;
        GridFill();
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
    protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "Select")
        {
            lblError.Text = "";

            int index = Convert.ToInt32(e.CommandArgument);

            Label lblID = (Label)GridView1.Rows[index].FindControl("lblID");
            HiddenField1.Value = lblID.Text;
            TextBox1.Text = lblID.Text;

            Label lblregno = (Label)GridView1.Rows[index].FindControl("lblregno");
            TextBox2.Text = lblregno.Text;

            Label lblname = (Label)GridView1.Rows[index].FindControl("lblname");
            TextBox3.Text = lblname.Text;

            Label lblguadian = (Label)GridView1.Rows[index].FindControl("lblguadian");
            TextBox4.Text = lblguadian.Text;

            Label lblage = (Label)GridView1.Rows[index].FindControl("lblage");
            TextBox5.Text = lblage.Text;

            Label lblsex = (Label)GridView1.Rows[index].FindControl("lblsex");
            DropDownList1.SelectedIndex = SearchIndex(lblsex.Text, DropDownList1);

            Label lblphone1 = (Label)GridView1.Rows[index].FindControl("lblphone1");
            //string[] ph1 = lblphone1.Text.Split(' ');
            TextBox7.Text = lblphone1.Text;

            Label lblphone2 = (Label)GridView1.Rows[index].FindControl("lblphone2");
            //string[] ph2 = lblphone2.Text.Split(' ');
            TextBox9.Text = lblphone2.Text;

            Label lbladdress = (Label)GridView1.Rows[index].FindControl("lbladdress");
            TextBox10.Text = lbladdress.Text;

            Label lbldist = (Label)GridView1.Rows[index].FindControl("lbldist");
            DropDownList9.SelectedIndex = SearchIndexbyid(lbldist.Text, DropDownList9);

            Label lbldoctype = (Label)GridView1.Rows[index].FindControl("lbldoctype");
            DropDownList2.SelectedIndex = SearchIndex(lbldoctype.Text, DropDownList2);

            Label lbldocname = (Label)GridView1.Rows[index].FindControl("lbldocname");
            DropDown3Fill("0");
            DropDownList3.SelectedIndex = SearchIndex(lbldocname.Text, DropDownList3);

            Label lblappodate1 = (Label)GridView1.Rows[index].FindControl("lblappodate1");
            TextBox12.Text = lblappodate1.Text;
            Label lblappotime = (Label)GridView1.Rows[index].FindControl("lblappotime");
            TextBox13.Text = lblappotime.Text;

            Label lblptype = (Label)GridView1.Rows[index].FindControl("lblptype");
            DropDownList4.SelectedIndex = SearchIndex(lblptype.Text, DropDownList4);

            Label lbladvamt = (Label)GridView1.Rows[index].FindControl("lbladvamt");
            TextBox14.Text = lbladvamt.Text;
            Label lblremarks = (Label)GridView1.Rows[index].FindControl("lblremarks");
            TextBox15.Text = lblremarks.Text;
            Tab1Func();
            Button1.Text = "Update";

            if (Objmain.checkaccss(Session["CoCode"].ToString(), Session["UserRoleID"].ToString().Trim(), "PATIENT APPOINTMENTS", checkAccessType.UpdateAction) == false)
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
        thepatientAppo.DeleteAppointment(Session["CoCode"].ToString().Trim(),Session["YearCode"].ToString().Trim(),lblID.Text);
        ScriptManager.RegisterStartupScript(this, this.GetType(), "msg", "alert('Deleted  Successfully !');", true);
        GridFill();
        ResetAllFields();
    }
 
    public void DropDown3Fill(string val)
    {
         this.DropDownList3.DataSource = thepatientAppo.DropdownDoc(Session["CoCode"].ToString().Trim(),val);
        this.DropDownList3.DataTextField = "doc_name";
        this.DropDownList3.DataValueField = "doc_id";
        this.DropDownList3.DataBind();
        this.DropDownList3.Items.Insert(0, new ListItem("--Select--", "0"));
    }
    protected void DropDownList2_SelectedIndexChanged(object sender, EventArgs e)
    {
        DropDown3Fill(DropDownList2.SelectedValue);
    }
    protected void Button4_Click(object sender, EventArgs e)
    {
        lblError.Text = "";
       DataTable dt=  thepatientAppo.GetPatientDetails(Session["CoCode"].ToString().Trim(),TextBox2.Text);
       TextBox3.Text = dt.Rows[0]["PName"].ToString();
       TextBox4.Text = dt.Rows[0]["GuadianName"].ToString();
       TextBox5.Text = dt.Rows[0]["Age"].ToString();
       DropDownList1.SelectedValue = dt.Rows[0]["Sex"].ToString();
      //string[] ph1 = dt.Rows[0]["PhNo1"].ToString().Split(' ');
      //string[] ph2 = dt.Rows[0]["PhNo2"].ToString().Split(' ');
       TextBox7.Text = dt.Rows[0]["PhNo1"].ToString();
        //if(ph2.Length>1)
       TextBox9.Text = dt.Rows[0]["PhNo2"].ToString();
        TextBox10.Text = dt.Rows[0]["Address"].ToString();
        DropDownList9.SelectedValue = dt.Rows[0]["District"].ToString();
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
    protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {            
            if (Objmain.checkaccss(Session["CoCode"].ToString(), Session["UserRoleID"].ToString().Trim(), "PATIENT APPOINTMENTS", checkAccessType.DeleteAction) == false)
            {
                e.Row.Cells[18].Visible = false;
            }

        }
    }
}