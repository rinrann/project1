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


public partial class IPD_AnesthesiaNote : System.Web.UI.Page
{
    Main Objmain = new Main(ConfigurationSettings.AppSettings["ConnectionString"].ToString());
    AnesthesiaNote theaddConsumable = new AnesthesiaNote(ConfigurationSettings.AppSettings["ConnectionString"].ToString());

    protected void Page_Load(object sender, EventArgs e)
    {
        Page.Title = "Anesthesia Note";
        if (Session["userName"] == null)
        {
            Response.Redirect("../LoginPage.aspx");
        }
        if (Objmain.checkaccss(Session["CoCode"].ToString(), Session["UserRoleID"].ToString().Trim(), "ANESTHESIA NOTE", checkAccessType.ViewAction) == false)
        {
            Response.Redirect("../AccessDenied.aspx");
        }

        if (Objmain.checkaccss(Session["CoCode"].ToString(), Session["UserRoleID"].ToString().Trim(), "ANESTHESIA NOTE", checkAccessType.InsertAction) == false)
        {
            Button1.Enabled = false;
        }

        if (!IsPostBack)
        {

            Tab1Func();
            DropDownFill();
            if (Session["ReqNo"] != null)
            {
                TextBox2.Text = Session["ReqNo"].ToString();
                DataTable dt = theaddConsumable.Getonlyreg(Session["CoCode"].ToString().Trim(), Session["YearCode"].ToString().Trim(),TextBox2.Text);
                TextBox1.Text = dt.Rows[0][0].ToString();
                GridFill();
                GridFillSecond();
            }
            Session["ReqNo"] = null;
        }
    }

    private void FillBPRange()
    {
        DataTable dt2 = theaddConsumable.getBPRange();
        if (dt2.Rows.Count > 0)
        {
            HiddenField3.Value = dt2.Rows[0]["LeftValue"].ToString() + "#" + dt2.Rows[0]["RightValue"].ToString();
            HiddenField4.Value = dt2.Rows[1]["LeftValue"].ToString() + "#" + dt2.Rows[1]["RightValue"].ToString();
        }

    }

    private void GridFill()
    {
        DataTable dt2 = theaddConsumable.Getonlyreq(Session["CoCode"].ToString().Trim(), Session["YearCode"].ToString().Trim(),TextBox1.Text);
        if (dt2.Rows.Count > 0)
        {
            TextBox2.Text = dt2.Rows[0][0].ToString();
            Dtls("1");
        }
       
    }
    public void Dtls(string a)
    {
        DataTable dt;
          if(a=="1")
      dt = theaddConsumable.GetAllAnesthesis(Session["CoCode"].ToString().Trim(), Session["YearCode"].ToString().Trim(),TextBox1.Text, TextBox2.Text);
        else
         dt = theaddConsumable.FetchFrom2ndGrid(Session["CoCode"].ToString().Trim(), Session["YearCode"].ToString().Trim(),TextBox1.Text, TextBox2.Text);
        if (dt.Rows.Count > 0)
        {
            GridView1.DataSource = dt;
            GridView1.DataBind();
            HiddenField1.Value = dt.Rows[0]["AnesthesiaNoteId"].ToString();
            TextBox3.Text = dt.Rows[0]["patient_name"].ToString();
            TextBox4.Text = dt.Rows[0]["BedNoText"].ToString();
            TextBox5.Text = dt.Rows[0]["adate"].ToString();
            TextBox15.Text = dt.Rows[0]["OperationName"].ToString();


            //string []bpsplit = dt.Rows[0]["BP"].ToString().Split('/');
            //if (bpsplit.Length>1) 
            //    TextBox8.Text = bpsplit[0];
            //else
            //    TextBox16.Text = bpsplit[0];
               
            TextBox6.Text = dt.Rows[0]["dt"].ToString();
            txtPreO2.Text = dt.Rows[0]["PreO2"].ToString();
           // DropDownList4.SelectedValue = dt.Rows[0]["Mode"].ToString();
            txtPreBp.Text = dt.Rows[0]["PreBP"].ToString();
            txtPreOthers.Text = dt.Rows[0]["PreOthers"].ToString();
            txtPrePulse.Text = dt.Rows[0]["PrePulse"].ToString();
            txtPreChest.Text = dt.Rows[0]["PreChest"].ToString();
            txtPreRemarks.Text = dt.Rows[0]["PreRemarks"].ToString();
            txtPreRiskfactor.Text = dt.Rows[0]["PreRiskFactor"].ToString();
            TextBox7.Text = dt.Rows[0]["Time"].ToString();
            txtPostBP.Text = dt.Rows[0]["PostBP"].ToString();
            txtPostPulse.Text = dt.Rows[0]["PostPulse"].ToString();
            txtPostChest.Text = dt.Rows[0]["PostChest"].ToString();
            txtPostO2.Text = dt.Rows[0]["PostO2"].ToString();
            txtPostRiskFactor.Text = dt.Rows[0]["PostRiskFactor"].ToString();
            txtPostOthers.Text = dt.Rows[0]["PostOthers"].ToString();
            txtPostTime.Text = dt.Rows[0]["posttime"].ToString();
            txtPostRemarks.Text = dt.Rows[0]["PostRemarks"].ToString();
            Button1.Text = "Update";

        }
        else
        {
            DataTable dt1 = theaddConsumable.Getonlypat(Session["CoCode"].ToString().Trim(), Session["YearCode"].ToString().Trim(),TextBox1.Text, TextBox2.Text);
            if (dt1.Rows.Count > 0)
            {
                TextBox3.Text = dt1.Rows[0]["patient_name"].ToString();
                TextBox4.Text = dt1.Rows[0]["BedNoText"].ToString();
                TextBox5.Text = dt1.Rows[0]["adate"].ToString();
                TextBox15.Text = dt1.Rows[0]["OperationName"].ToString();
                TextBox2.Text = dt1.Rows[0]["OperationReqID"].ToString();
            }
        }
    }
    private void GridFillSecond()
    {
        //select v.Rowid, pr.PatientReg,pr.,CONVERT(varchar,pr.AdmissionDate,103) adate,bm.BedNoText

        DataTable dt = theaddConsumable.GridFillSecond(Session["CoCode"].ToString().Trim(), Session["YearCode"].ToString().Trim(), TextBox1.Text);
        if (dt.Rows.Count > 0)
        {
            GridView2.DataSource = dt;
            GridView2.DataBind();
        }
    }


    private void ResetAllFields()
    {
        for (int i = 1; i <= 15; i++)
        {
            TextBox t = (TextBox)Page.FindControl("ctl00$ContentPlaceHolder1$TextBox" + i.ToString());
            t.Text = "";
        }
        DropDownList4.SelectedIndex = 0;
        Button1.Text = "Submit";
        HiddenField1.Value = "";
        if (Objmain.checkaccss(Session["CoCode"].ToString(), Session["UserRoleID"].ToString().Trim(), "ANESTHESIA NOTE", checkAccessType.InsertAction) == false)
        {
            Button1.Enabled = false;
        }

    }
    protected void Button1_Click(object sender, EventArgs e)
    {
         System.Globalization.DateTimeFormatInfo dtf = new DateTimeFormatInfo();
        DateTime testdate = DateTime.ParseExact(TextBox6.Text, "dd/MM/yyyy", dtf);
        string bp = txtPreBp.Text;
        if (Button1.Text == "Submit")
        {

            if (theaddConsumable.InsertAnesthesis(TextBox1.Text, TextBox2.Text, testdate.ToString("yyyy-MM-dd"), TextBox7.Text, bp, txtPrePulse.Text, txtPreChest.Text, txtPreO2.Text, txtPreRiskfactor.Text, txtPreOthers.Text, txtPreRemarks.Text, txtPostBP.Text, txtPostPulse.Text, txtPostChest.Text, txtPostO2.Text, txtPostRiskFactor.Text, txtPostOthers.Text, txtPostRemarks.Text, txtPostTime.Text, Session["CoCode"].ToString().Trim(), Session["userName"].ToString().Trim(), Session["YearCode"].ToString().Trim()) == true)
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "msg", "alert('Insered Successfully !');", true);
                Response.Redirect("../IPD/OTList.aspx");
         
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "msg", "alert('Error in Insered Data !');", true);
            }
        }
        else 
        {
            if (theaddConsumable.UpdateAnesthesis(HiddenField1.Value, TextBox1.Text, TextBox2.Text, testdate.ToString("yyyy-MM-dd"), TextBox7.Text, bp, txtPrePulse.Text, txtPreChest.Text, txtPreO2.Text, txtPreRiskfactor.Text, txtPreOthers.Text, txtPreRemarks.Text, txtPostBP.Text, txtPostPulse.Text, txtPostChest.Text, txtPostO2.Text, txtPostRiskFactor.Text, txtPostOthers.Text, txtPostRemarks.Text, txtPostTime.Text, Session["CoCode"].ToString().Trim(), Session["userName"].ToString().Trim(), Session["YearCode"].ToString().Trim()) == true)
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "msg", "alert('Updated Successfully !');", true);
                Response.Redirect("../IPD/OTList.aspx");
                Button1.Text = "Submit";

            }
            else
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "msg", "alert('Error in Updated Data !');", true);
            }
        }
        GridFill();
        ResetAllFields();
        Session["RegNo"] = null;


    }
    protected void Button2_Click(object sender, EventArgs e)
    {
        ResetAllFields();
    }
    private void DropDownFill()
    {
        this.DropDownList4.DataSource = theaddConsumable.DropdownMode(Session["CoCode"].ToString().Trim());
        this.DropDownList4.DataTextField = "Mode";
        this.DropDownList4.DataValueField = "ID";
        this.DropDownList4.DataBind();
        this.DropDownList4.Items.Insert(0, new ListItem("--Select--", "0"));

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
            if (ddl.Items[i].Value.Trim() == Value.Trim())
                return i;
        }
        return -1;
    }

 
    protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "Select")
        {
            int index = Convert.ToInt32(e.CommandArgument);
            lblError.Text = "";
            Label lblid = (Label)GridView1.Rows[index].FindControl("lblid");
            HiddenField1.Value = lblid.Text;

            Label lblRegno = (Label)GridView1.Rows[index].FindControl("lblRegno");
            TextBox1.Text = lblRegno.Text;
            Label lbllname = (Label)GridView1.Rows[index].FindControl("lbllname");
            TextBox3.Text = lbllname.Text;
            Label lbladate = (Label)GridView1.Rows[index].FindControl("lbladate");
            TextBox5.Text = lbladate.Text;
            Label lblbedno = (Label)GridView1.Rows[index].FindControl("lblbedno");
            TextBox4.Text = lblbedno.Text;

            //Label lblmode = (Label)GridView1.Rows[index].FindControl("lblmode");
            //DropDownList4.SelectedIndex = SearchIndex(lblmode.Text, DropDownList4);

            Label lbldate = (Label)GridView1.Rows[index].FindControl("lbldate");
            TextBox6.Text = lbldate.Text;

            Label lbltime = (Label)GridView1.Rows[index].FindControl("lbltime");
            TextBox7.Text = lbltime.Text;

            Label lblbp = (Label)GridView1.Rows[index].FindControl("lblbp");
            txtPreBp.Text = lblbp.Text;


            Label lblpulse = (Label)GridView1.Rows[index].FindControl("lblpulse");
            txtPrePulse.Text = lblpulse.Text;

            Label lblchest = (Label)GridView1.Rows[index].FindControl("lblchest");
            txtPreChest.Text = lblchest.Text;

            Label lblo2 = (Label)GridView1.Rows[index].FindControl("lblo2");
            txtPreO2.Text = lblo2.Text;


            Label lblriskfactor = (Label)GridView1.Rows[index].FindControl("lblriskfactor");
            txtPreRiskfactor.Text = lblriskfactor.Text;

            Label lblothers = (Label)GridView1.Rows[index].FindControl("lblothers");
            txtPreOthers.Text = lblothers.Text;

            Label lblremarks = (Label)GridView1.Rows[index].FindControl("lblremarks");
            txtPreRemarks.Text = lblremarks.Text;

            Label lblReq = (Label)GridView1.Rows[index].FindControl("lblReq");
            TextBox2.Text = lblReq.Text;

            Label lblPostBp = (Label)GridView1.Rows[index].FindControl("lblpostBp");
            txtPostBP.Text = lblPostBp.Text;

            Label lblPostPulse = (Label)GridView1.Rows[index].FindControl("lblpulse");
            txtPostPulse.Text = lblPostPulse.Text;

            Label lblPostChest = (Label)GridView1.Rows[index].FindControl("lblpostchest");
            txtPostChest.Text = lblPostChest.Text;

            Label lblPostO2 = (Label)GridView1.Rows[index].FindControl("lblposto2");
            txtPostO2.Text = lblPostO2.Text;

            Label lblPostRiskFactor = (Label)GridView1.Rows[index].FindControl("lblpostriskfactor");
            txtPostRiskFactor.Text = lblPostRiskFactor.Text;

            Label lblPostOther = (Label)GridView1.Rows[index].FindControl("lblpostother");
            txtPostOthers.Text = lblPostOther.Text;

            Label lblPostRemarks = (Label)GridView1.Rows[index].FindControl("lblpostremarks");
            txtPostRemarks.Text = lblPostRemarks.Text;

            Button1.Text = "Update";
            Tab1Func();
            if (Objmain.checkaccss(Session["CoCode"].ToString(), Session["UserRoleID"].ToString().Trim(), "ANESTHESIA NOTE", checkAccessType.UpdateAction) == false)
            {
                Button1.Enabled = false;
            }
            else
            {
                Button1.Enabled = true;
            }
        }
    }

    protected void Button4_Click(object sender, EventArgs e)
    {

        GridFill();
        GridFillSecond();
    }



    protected void GridView2_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {

    }
    protected void GridView2_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "Select")
        {
            int index = Convert.ToInt32(e.CommandArgument);
            lblError.Text = "";
            Label lblid = (Label)GridView2.Rows[index].FindControl("lblid");
            TextBox2.Text = lblid.Text;

            Label lblRegno = (Label)GridView2.Rows[index].FindControl("lblRegno");
            TextBox1.Text = lblRegno.Text;
            Tab1Func();
            Dtls("0");

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
    //protected void TextBox8_TextChanged(object sender, EventArgs e)
    //{
        
    //    FillBPRange(); 
    //    string[] LowBP = HiddenField3.Value.Split('#');
    //    string[] HighBP = HiddenField4.Value.Split('#');
    //    try
    //    {
    //        if (Convert.ToInt32(txtPreBp.Text) < Convert.ToInt32(LowBP[0]) || Convert.ToInt32(txtPreBp.Text) > Convert.ToInt32(HighBP[0]))
    //        {
    //            txtPreBp.ForeColor = System.Drawing.Color.Red;
    //            txtPreBp.BackColor = System.Drawing.Color.Yellow;

    //        }
    //        else
    //        {
    //            txtPreBp.ForeColor = System.Drawing.Color.Black;
    //            txtPreBp.BackColor = System.Drawing.Color.WhiteSmoke;

    //        } 
          
    //    }
    //    catch (Exception ex)
    //    {
    //        txtPreBp.ForeColor = System.Drawing.Color.Red;
    //        txtPreBp.BackColor = System.Drawing.Color.Yellow;
    //    }
    //}
    protected void TextBox16_TextChanged(object sender, EventArgs e)
    {
        //FillBPRange();
        //string[] LowBP = HiddenField3.Value.Split('#');
        //string[] HighBP = HiddenField4.Value.Split('#');

        //try
        //{

        //    if (Convert.ToInt32(TextBox16.Text) < Convert.ToInt32(LowBP[1]) || Convert.ToInt32(TextBox16.Text) > Convert.ToInt32(HighBP[1]))
        //    {

        //        TextBox16.ForeColor = System.Drawing.Color.Red;
        //        TextBox16.BackColor = System.Drawing.Color.Yellow;
        //    }
        //    else
        //    {
        //        TextBox16.ForeColor = System.Drawing.Color.Black;
        //        TextBox16.BackColor = System.Drawing.Color.WhiteSmoke;
        //    }
        //}
        //catch
        //{ 
        //    TextBox16.ForeColor = System.Drawing.Color.Red;
        //    TextBox16.BackColor = System.Drawing.Color.Yellow;
        //}
    }
}