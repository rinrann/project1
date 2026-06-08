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
 
public partial class IPD_OthersCharge : System.Web.UI.Page
{
    Main Objmain = new Main(ConfigurationSettings.AppSettings["ConnectionString"].ToString());
    OthersCharge theothers = new OthersCharge(ConfigurationSettings.AppSettings["ConnectionString"].ToString());
    TextBox t1, t2,t3;
    String Date = DateTime.Now.ToString("dd/MM/yyyy");
    protected void Page_Load(object sender, EventArgs e)
    {

        Page.Title = "Other Charges";
        if (Session["userName"] == null)
        {
            Response.Redirect("../LoginPage.aspx");
        }
        if (Objmain.checkaccss(Session["CoCode"].ToString(), Session["UserRoleID"].ToString().Trim(), "OTHER SERVICES", checkAccessType.ViewAction) == false)
        {
            Response.Redirect("../AccessDenied.aspx");
        }

        if (Objmain.checkaccss(Session["CoCode"].ToString(), Session["UserRoleID"].ToString().Trim(), "OTHER SERVICES", checkAccessType.InsertAction) == false)
        {
            Button1.Enabled = false;
        }

        if (!IsPostBack)
        {
            Tab1Func();
        }
  
    }

    public void Fill()
    {

        TextBox23.Text = Session["RegNo"].ToString(); GridFill();
    }


     protected void Button1_Click(object sender, EventArgs e)
    {
      
         System.Globalization.DateTimeFormatInfo dtf = new DateTimeFormatInfo();

         if (Button1.Text == "Submit")
        {

            for (int t = 1; t <= 29; t = t + 3)
            {

                t1 = (TextBox)Page.FindControl("ctl00$ContentPlaceHolder1$TextBox" + t.ToString());
                t2 = (TextBox)Page.FindControl("ctl00$ContentPlaceHolder1$TextBox" + (t + 1).ToString());
                t3 = (TextBox)Page.FindControl("ctl00$ContentPlaceHolder1$TextBox" + (t + 2).ToString());
      

                if (t1.Text != "" && t2.Text != "" && t3.Text != "")
                {
                    DateTime testdate = DateTime.ParseExact(t1.Text, "dd/MM/yyyy", dtf);
                    theothers.InsertOthersCharges(TextBox31.Text, testdate.ToString(), t2.Text, t3.Text, Session["CoCode"].ToString().Trim(), Session["userName"].ToString().Trim());
                }
                else
                    break;
            }
            ScriptManager.RegisterStartupScript(this, this.GetType(), "msg", "alert('Inserted  Successfully !');", true);
        }
        else
        {
            if (HiddenField1.Value == "")
            {
                theothers.DeleteOtherCharges(TextBox31.Text, Session["CoCode"].ToString().Trim());

                for (int t = 1; t <= 29; t = t + 3)
                {

                    t1 = (TextBox)Page.FindControl("ctl00$ContentPlaceHolder1$TextBox" + t.ToString());
                    t2 = (TextBox)Page.FindControl("ctl00$ContentPlaceHolder1$TextBox" + (t + 1).ToString());
                    t3 = (TextBox)Page.FindControl("ctl00$ContentPlaceHolder1$TextBox" + (t + 2).ToString());
                    if (t1.Text != "" && t2.Text != "" && t3.Text != "")
                    {
                        DateTime testdate = DateTime.ParseExact(t1.Text, "dd/MM/yyyy", dtf);
                        theothers.InsertOthersCharges(TextBox31.Text, testdate.ToString(), t2.Text, t3.Text, Session["CoCode"].ToString().Trim(), Session["userName"].ToString().Trim());
                    }
                    else
                        break;
                }
            }
            else
            {
                DateTime testdate = DateTime.ParseExact(TextBox1.Text, "dd/MM/yyyy", dtf);
                theothers.UpdateOthersCharges(HiddenField1.Value, testdate.ToString(), TextBox2.Text, TextBox3.Text, Session["CoCode"].ToString().Trim(), Session["userName"].ToString().Trim());
            }

            ScriptManager.RegisterStartupScript(this, this.GetType(), "msg", "alert('Updated  Successfully !');", true);

        }
        GridFill();
        ResetAllFields();
    }
    public void ResetAllFields()
    {
        HiddenField1.Value = "";
        for (int t = 1; t <= 34; t++)
        {
            t1 = (TextBox)Page.FindControl("ctl00$ContentPlaceHolder1$TextBox" + t.ToString());
            t1.Text = "";
             
        }

        Button1.Text = "Submit";
        if (Objmain.checkaccss(Session["CoCode"].ToString(), Session["UserRoleID"].ToString().Trim(), "OTHER SERVICES", checkAccessType.InsertAction) == false)
        {
            Button1.Enabled = false;
        }
    }

    protected void Button4_Click(object sender, EventArgs e)
    {
        ResetAllFields();
    }
    protected void Button2_Click(object sender, EventArgs e)
    {
        GridFill();
    }

    private void GridFill()
    {
       
         HiddenField1.Value = "";
         DataTable dt = theothers.GetAllOthersCharge(TextBox31.Text, Session["CoCode"].ToString().Trim());
        DataTable dt1 = theothers.Getonlypat(TextBox31.Text);
        if (dt.Rows.Count > 0)
        {
            GridView1.DataSource = dt;
            GridView1.DataBind();
            TextBox32.Text = dt.Rows[0]["patient_name"].ToString();
            TextBox33.Text = dt.Rows[0]["BedNoText"].ToString();
            TextBox34.Text = dt.Rows[0]["adate"].ToString();
            FillOthersCharge(dt);
            Button1.Text = "Update";
        }
        else
        {
            if (dt1.Rows.Count > 0)
            {
                TextBox32.Text = dt1.Rows[0]["patient_name"].ToString();
                TextBox33.Text = dt1.Rows[0]["BedNoText"].ToString();
                TextBox34.Text = dt1.Rows[0]["adate"].ToString();

            }

        }

    }

    public void FillOthersCharge(DataTable dt)
    {
        for (int i = 0, t = 1; i < dt.Rows.Count; i++, t = t + 3)
        {
            t1 = (TextBox)Page.FindControl("ctl00$ContentPlaceHolder1$TextBox" + t.ToString());
            t2 = (TextBox)Page.FindControl("ctl00$ContentPlaceHolder1$TextBox" + (t + 1).ToString());
            t3= (TextBox)Page.FindControl("ctl00$ContentPlaceHolder1$TextBox" + (t + 2).ToString());
            t1.Text = dt.Rows[i]["IssueDate"].ToString();
            t2.Text = dt.Rows[i]["OthersDetails"].ToString();
            t3.Text = dt.Rows[i]["Amount"].ToString();
        }
    }
 
    protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "Select")
        {
            Tab1Func();
            ResetAllFields();
            int index = Convert.ToInt32(e.CommandArgument);
            lblError.Text = "";
            Label lblid = (Label)GridView1.Rows[index].FindControl("lblid");
            HiddenField1.Value = lblid.Text;

            Label lblRegno = (Label)GridView1.Rows[index].FindControl("lblRegno");
            TextBox31.Text = lblRegno.Text;

            Label lbllname = (Label)GridView1.Rows[index].FindControl("lbllname");
            TextBox32.Text = lbllname.Text;

            Label lbladate = (Label)GridView1.Rows[index].FindControl("lbladate");
            TextBox34.Text = lbladate.Text;

            Label lblbedno = (Label)GridView1.Rows[index].FindControl("lblbedno");
            TextBox33.Text = lblbedno.Text;

            Label lbldatee = (Label)GridView1.Rows[index].FindControl("lbldatee");
            TextBox1.Text = lbldatee.Text;

            Label lblothers = (Label)GridView1.Rows[index].FindControl("lblothers");
            TextBox2.Text = lblothers.Text;

            Label lblamount = (Label)GridView1.Rows[index].FindControl("lblamount");
            TextBox3.Text = lblamount.Text;

            Button1.Text = "Update";

            if (Objmain.checkaccss(Session["CoCode"].ToString(), Session["UserRoleID"].ToString().Trim(), "OTHER SERVICES", checkAccessType.UpdateAction) == false)
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