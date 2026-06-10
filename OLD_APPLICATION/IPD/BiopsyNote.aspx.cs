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

public partial class IPD_BiopsyNote : System.Web.UI.Page
{
    Main Objmain = new Main(ConfigurationSettings.AppSettings["ConnectionString"].ToString());
    BiopsyNote theBiopsy = new BiopsyNote(ConfigurationSettings.AppSettings["ConnectionString"].ToString());
    TextBox t1, t2,t3,t4;
    String Date = DateTime.Now.ToString("dd/MM/yyyy");
    protected void Page_Load(object sender, EventArgs e)
    {
        Page.Title = "Biopsy Note";
        if (Session["userName"] == null)
        {
            Response.Redirect("../LoginPage.aspx");
        }
        if (Objmain.checkaccss(Session["CoCode"].ToString(), Session["UserRoleID"].ToString().Trim(), "BIOPSY NOTE", checkAccessType.ViewAction) == false)
        {
            Response.Redirect("../AccessDenied.aspx");
        }

        if (Objmain.checkaccss(Session["CoCode"].ToString(), Session["UserRoleID"].ToString().Trim(), "BIOPSY NOTE", checkAccessType.InsertAction) == false)
        {
            Button1.Enabled = false;
        }

        if (!IsPostBack)
        {
            Tab1Func();
            if (Session["ReqNo"] != null)
            {
                TextBox2.Text = Session["ReqNo"].ToString();
                DataTable dt = theBiopsy.Getonlyreg(Session["CoCode"].ToString().Trim(),Session["YearCode"].ToString().Trim(),TextBox2.Text);
                TextBox1.Text = dt.Rows[0][0].ToString();
                Dtls();
                GridFillSecond();
            }
            Session["ReqNo"] = null;
        }


    }



    [System.Web.Script.Services.ScriptMethod()]
    [System.Web.Services.WebMethod]

    public static List<string> SearchCustomers(string prefixText, int count)
    {
        using (SqlConnection conn = new SqlConnection())
        {
            conn.ConnectionString = ConfigurationSettings.AppSettings["ConnectionString"].ToString();
            using (SqlCommand cmd = new SqlCommand())
            {
                cmd.CommandText = "select distinct TypeOfTissue as Name from IPD_OTBiopsyNoteMapping where TypeOfTissue like @SearchText + '%'";
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

    public static List<string> SearchCustomers1(string prefixText, int count)
    {
        using (SqlConnection conn = new SqlConnection())
        {
            conn.ConnectionString = ConfigurationSettings.AppSettings["ConnectionString"].ToString();
            using (SqlCommand cmd = new SqlCommand())
            {
                cmd.CommandText = "select distinct ExamRequired as Name from IPD_OTBiopsyNoteMapping where ExamRequired like @SearchText + '%'";
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

 
    private void GridFillSecond()
    {
        DataTable dt = theBiopsy.GridFillSecond(Session["CoCode"].ToString().Trim(), Session["YearCode"].ToString().Trim(), TextBox1.Text);
        if (dt.Rows.Count > 0)
        {
            GridView2.DataSource = dt;
            GridView2.DataBind();
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
            Tab1Func();
            GridFill();
        }
    }
    protected void Button1_Click(object sender, EventArgs e)
    {
        string id;
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
            DataTable dt = theBiopsy.GenerateBiopsycode(Session["CoCode"].ToString().Trim(), Session["YearCode"].ToString().Trim());
            theBiopsy.InsertBiopsyNote(Session["CoCode"].ToString().Trim(), Session["userName"].ToString().Trim(), dt.Rows[0][0].ToString(), TextBox1.Text, TextBox2.Text, Session["YearCode"].ToString().Trim());
            
            for (int t = 11; t <= 36; t = t + 4 )
            {

                t1 = (TextBox)Page.FindControl("ctl00$ContentPlaceHolder1$TextBox" + t.ToString());
                t2 = (TextBox)Page.FindControl("ctl00$ContentPlaceHolder1$TextBox" + (t + 1).ToString());
                t3 = (TextBox)Page.FindControl("ctl00$ContentPlaceHolder1$TextBox" + (t + 2).ToString());
                t4 = (TextBox)Page.FindControl("ctl00$ContentPlaceHolder1$TextBox" + (t + 3).ToString());
                System.Globalization.DateTimeFormatInfo dtf = new DateTimeFormatInfo();

                if (t1.Text != "")
                {
                    DateTime testdate = DateTime.ParseExact(t1.Text, "dd/MM/yyyy", dtf);
                    theBiopsy.InsertBiopsyMapping(dt.Rows[0][0].ToString(), testdate.ToString("yyyy-MM-dd"), t2.Text, t3.Text, t4.Text, Session["CoCode"].ToString().Trim(), Session["userName"].ToString().Trim(), Session["YearCode"].ToString().Trim());
                }
                else
                    break;
            }

         //   ScriptManager.RegisterStartupScript(this, this.GetType(), "msg", "alert('Insered Successfully !');", true);
           
        }
        else
        {
            theBiopsy.DeleteBiopsyMap(HiddenField1.Value, Session["CoCode"].ToString().Trim(), Session["YearCode"].ToString().Trim());

            for (int t = 11; t <= 36; t = t + 4)
            {

                t1 = (TextBox)Page.FindControl("ctl00$ContentPlaceHolder1$TextBox" + t.ToString());
                t2 = (TextBox)Page.FindControl("ctl00$ContentPlaceHolder1$TextBox" + (t + 1).ToString());
                t3 = (TextBox)Page.FindControl("ctl00$ContentPlaceHolder1$TextBox" + (t + 2).ToString());
                t4 = (TextBox)Page.FindControl("ctl00$ContentPlaceHolder1$TextBox" + (t + 3).ToString());
                System.Globalization.DateTimeFormatInfo dtf = new DateTimeFormatInfo();
                if (t1.Text != "")
                {
                    DateTime testdate = DateTime.ParseExact(t1.Text, "dd/MM/yyyy", dtf);
                    theBiopsy.InsertBiopsyMapping(HiddenField1.Value, testdate.ToString("yyyy-MM-dd"), t2.Text, t3.Text, t4.Text, Session["CoCode"].ToString().Trim(), Session["userName"].ToString().Trim(), Session["YearCode"].ToString().Trim());
                }
                else
                    break;
            }
          //  ScriptManager.RegisterStartupScript(this, this.GetType(), "msg", "alert('Insered Successfully !');", true);
             }
        Response.Redirect("../IPD/OTList.aspx");
        GridFill();
        ResetAllFields();
    }
    public void ResetAllFields()
    {
        HiddenField1.Value = "";
        Button1.Text = "Submit";
        for (int t = 11; t <= 38; t++)
        {
            t1 = (TextBox)Page.FindControl("ctl00$ContentPlaceHolder1$TextBox" + t.ToString());
            t1.Text = "";
        }
        TextBox1.Text = ""; TextBox2.Text = ""; TextBox3.Text = ""; TextBox4.Text = ""; TextBox5.Text = ""; TextBox6.Text = "";
        if (Objmain.checkaccss(Session["CoCode"].ToString(), Session["UserRoleID"].ToString().Trim(), "BIOPSY NOTE", checkAccessType.InsertAction) == false)
        {
            Button1.Enabled = false;
        }
    }
    
    protected void Button4_Click(object sender, EventArgs e)
    {
        Dtls();
        GridFillSecond();
    }
    protected void Button2_Click(object sender, EventArgs e)
    {
       ResetAllFields();
    }
    private void GridFill()
    {
        DataTable dt = theBiopsy.FetchFron2ndGrid(Session["CoCode"].ToString().Trim(), Session["YearCode"].ToString().Trim(),TextBox1.Text, TextBox2.Text);
        DataTable dt1 = theBiopsy.Getonlypat(Session["CoCode"].ToString().Trim(), Session["YearCode"].ToString().Trim(), TextBox1.Text, TextBox2.Text);
        if (dt.Rows.Count > 0)
        {
            GridView1.DataSource = dt;
            GridView1.DataBind();
            HiddenField1.Value = dt.Rows[0]["BiopsyNoteId"].ToString();
            TextBox2.Text = dt.Rows[0]["OperationReqID"].ToString();
            TextBox3.Text = dt.Rows[0]["patient_name"].ToString();
            TextBox4.Text = dt.Rows[0]["BedNoText"].ToString();
            TextBox5.Text = dt.Rows[0]["OperationName"].ToString();
            TextBox6.Text = dt.Rows[0]["adate"].ToString();
            FillMedicineDtls(dt);
            Button1.Text = "Update";
            if (Objmain.checkaccss(Session["CoCode"].ToString(), Session["UserRoleID"].ToString().Trim(), "BIOPSY NOTE", checkAccessType.UpdateAction) == false)
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
                TextBox2.Text = dt1.Rows[0]["OperationReqID"].ToString();
                TextBox3.Text = dt1.Rows[0]["patient_name"].ToString();
                TextBox4.Text = dt1.Rows[0]["BedNoText"].ToString();
                TextBox5.Text = dt1.Rows[0]["OperationName"].ToString();
                TextBox6.Text = dt1.Rows[0]["adate"].ToString();

            }

        } 
    }
    private void Dtls()
     {
         DataTable dtreq = theBiopsy.Getonltreq(Session["CoCode"].ToString().Trim(), Session["YearCode"].ToString().Trim(),TextBox1.Text);
        if (dtreq.Rows.Count > 0)
        {
            TextBox2.Text = dtreq.Rows[0][0].ToString();
            GridFill();
        }
        else
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "msg", "alert('no OT is Done !');", true);
        }

    }

    public void FillMedicineDtls(DataTable dt)
    {
        for (int i=0, t = 11; i<dt.Rows.Count;i++, t = t + 4)
        {

            t1 = (TextBox)Page.FindControl("ctl00$ContentPlaceHolder1$TextBox" + t.ToString());
            t2 = (TextBox)Page.FindControl("ctl00$ContentPlaceHolder1$TextBox" + (t + 1).ToString());
            t3 = (TextBox)Page.FindControl("ctl00$ContentPlaceHolder1$TextBox" + (t + 2).ToString());
            t4 = (TextBox)Page.FindControl("ctl00$ContentPlaceHolder1$TextBox" + (t + 3).ToString());
            t1.Text = dt.Rows[i]["coldate"].ToString();
            t2.Text = dt.Rows[i]["TypeOfTissue"].ToString();
            t3.Text = dt.Rows[i]["ExamRequired"].ToString();
            t4.Text = dt.Rows[i]["Remarks"].ToString();
        }
    }
    protected int SearchIndex(string Value, DropDownList ddl)
    {
        int i;
        for (i = 0; i < ddl.Items.Count; i++)
        {
            if (ddl.Items[i].Value.Trim() == Value.Trim())
                return i;
        }
        return -1;
    }

    protected int SearchText(string Value, DropDownList ddl)
    {
        int i;
        for (i = 0; i < ddl.Items.Count; i++)
        {
            if (ddl.Items[i].Text.Trim() == Value.Trim())
                return i;
        }
        return -1;
    }
    protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "Select")
        {
            ResetAllFields();
            int index = Convert.ToInt32(e.CommandArgument);
            lblError.Text = "";
            Label lblid = (Label)GridView1.Rows[index].FindControl("lblid");
            HiddenField1.Value = lblid.Text;

            Label lblRegno = (Label)GridView1.Rows[index].FindControl("lblRegno");
            TextBox1.Text = lblRegno.Text;
            GridFill();
            Tab1Func();
            Button1.Text = "Update";
            if (Objmain.checkaccss(Session["CoCode"].ToString(), Session["UserRoleID"].ToString().Trim(), "BIOPSY NOTE", checkAccessType.UpdateAction) == false)
            {
                Button1.Enabled = false;
            }
            else
            {
                Button1.Enabled = true;
            }
        }
    }
    protected void GridView1_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        GridView1.PageIndex = e.NewPageIndex;
        GridFill();
    }

    protected void GridView2_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        GridView2.PageIndex = e.NewPageIndex;
        GridFill();
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
}