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

public partial class IPD_DailyCheckupRecord : System.Web.UI.Page
{
    Main Objmain = new Main(ConfigurationSettings.AppSettings["ConnectionString"].ToString());
    DailyCheckupRecord theaddConsumable = new DailyCheckupRecord(ConfigurationSettings.AppSettings["ConnectionString"].ToString());
    PatientsDetails thepatientdetails = new PatientsDetails(ConfigurationSettings.AppSettings["ConnectionString"].ToString());

    DropDownList d1;
    TextBox t1, t2, t3, t4, t5, t6, t7, t8, t9, t10, t11;

    PatientDoctorVisit thedocvisit = new PatientDoctorVisit(ConfigurationSettings.AppSettings["ConnectionString"].ToString());
  
    protected void Page_Load(object sender, EventArgs e)
    {
        Page.Title = "BHT Record";
        if (Session["userName"] == null)
        {
            Response.Redirect("../LoginPage.aspx");
        }
        if (Objmain.checkaccss(Session["CoCode"].ToString(), Session["UserRoleID"].ToString().Trim(), "DAILY CHECK UP", checkAccessType.ViewAction) == false)
        {
            Response.Redirect("../AccessDenied.aspx");
        }

        if (Objmain.checkaccss(Session["CoCode"].ToString(), Session["UserRoleID"].ToString().Trim(), "DAILY CHECK UP", checkAccessType.InsertAction) == false)
        {
            Button9.Enabled = false;
        }
        //if (Objmain.checkaccss(Session["CoCode"].ToString(), Session["UserRoleID"].ToString().Trim(), "COMPLAIN MASTER", checkAccessType.DeleteAction) == false)
        //{
        //    GridView6.Columns[17].Visible = false;
        //}
        if (!IsPostBack)
        {

            Tab1Func(); 
            if (Session["RegNo"] != null)
            {
                Fill();
            }
        }
    }
    public void Fill()
    {
        TextBox2.Text = Session["RegNo"].ToString(); 
        FillDetails();
    }
 

    public void GridFill()
    {
        DataTable dt = thedocvisit.GridFillBody( Session["CoCode"].ToString().Trim(),TextBox2.Text);
        GridView6.DataSource = dt;
        GridView6.DataBind();
    }
    private void FillDetails()
    {
        DataTable dt1 = theaddConsumable.Getonlypat( Session["CoCode"].ToString().Trim(),TextBox2.Text);
        if (dt1.Rows.Count > 0)
        {
                TextBox3.Text = dt1.Rows[0]["patient_name"].ToString();
                TextBox4.Text = dt1.Rows[0]["BedNoText"].ToString();
                TextBox5.Text = dt1.Rows[0]["adate"].ToString();
         }
         GridFill();
       
         
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
 
    protected void Button4_Click(object sender, EventArgs e)
    {
        FillDetails();
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
        ResetBody();
    }

    protected void Tab2_Click(object sender, EventArgs e)
    {
        Tab1.CssClass = "Initial";
        Tab2.CssClass = "Clicked";
        MainView.ActiveViewIndex = 1;
        
       
    }
    protected void GridView6_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "Select")
        {
            TextBox9.Enabled = false;
            TextBox24.Enabled = false;
            MainView.ActiveViewIndex = 0;

            int index = Convert.ToInt32(e.CommandArgument);
            lblError.Text = "";

            Label lblid = (Label)GridView6.Rows[index].FindControl("lblid");
            HiddenField1.Value = lblid.Text;

            Label lbldate = (Label)GridView6.Rows[index].FindControl("lbldate");
            TextBox9.Text = lbldate.Text;

            Label lblComplainName = (Label)GridView6.Rows[index].FindControl("lblComplainName");
            TextBox22.Text = lblComplainName.Text;



            if (lblComplainName.Text != "")
            {
                string[] ComplainId = lblComplainName.Text.Split(',');

                for (int i = 0; i < ComplainId.Length; i++)
                {
                    DataTable dt = thedocvisit.GGetComplainId(ComplainId[i]);
                    if (HiddenField3.Value == "")
                    {
                        HiddenField3.Value = dt.Rows[0]["RowId"].ToString();
                    }
                    else
                    {
                        HiddenField3.Value = HiddenField3.Value + "," + dt.Rows[0]["RowId"].ToString();
                    }
                }
            }

            Label lblBP = (Label)GridView6.Rows[index].FindControl("lblBP");
            TextBox10.Text = lblBP.Text;

            Label lblTime = (Label)GridView6.Rows[index].FindControl("lblTime");
            TextBox24.Text = lblTime.Text;

            Label lblPulse = (Label)GridView6.Rows[index].FindControl("lblPulse");
            TextBox11.Text = lblPulse.Text;

            Label lblTemp = (Label)GridView6.Rows[index].FindControl("lblTemp");
            TextBox12.Text = lblTemp.Text;

            Label lblSPO2 = (Label)GridView6.Rows[index].FindControl("lblSPO2");
            TextBox13.Text = lblSPO2.Text;

            Label lblUrin = (Label)GridView6.Rows[index].FindControl("lblUrin");
            TextBox14.Text = lblUrin.Text;

            Label lblSunction = (Label)GridView6.Rows[index].FindControl("lblSunction");
            TextBox15.Text = lblSunction.Text;

            Label lblPA = (Label)GridView6.Rows[index].FindControl("lblPA");
            TextBox1.Text = lblPA.Text;

            Label lblPV = (Label)GridView6.Rows[index].FindControl("lblPV");
            TextBox6.Text = lblPV.Text;

            Label lblChest = (Label)GridView6.Rows[index].FindControl("lblChest");
            TextBox7.Text = lblChest.Text;

            
            Label lblDoppler = (Label)GridView6.Rows[index].FindControl("lblDoppler");
            TextBox16.Text = lblDoppler.Text;

            Label lblBleeding = (Label)GridView6.Rows[index].FindControl("lblBleeding");
            TextBox17.Text = lblBleeding.Text;

            Label lblOthers = (Label)GridView6.Rows[index].FindControl("lblOthers");
            TextBox18.Text = lblOthers.Text;
            Button9.Text = "Update";
            LinkButton3.Text = "Update Complain";

            if (Objmain.checkaccss(Session["CoCode"].ToString(), Session["UserRoleID"].ToString().Trim(), "DAILY CHECK UP", checkAccessType.UpdateAction) == false)
            {
                Button9.Enabled = false;
            }
            else
            {
                Button9.Enabled = true;
            }
        }
    }

    protected void Button9_Click(object sender, EventArgs e)
    {
        string testdate;
        System.Globalization.DateTimeFormatInfo dtf = new DateTimeFormatInfo();
        if (TextBox9.Text != "")
            testdate = DateTime.ParseExact(TextBox9.Text, "dd/MM/yyyy", dtf).ToString("yyyy-MM-dd");
        else
            testdate = DateTime.Now.ToString("yyyy-MM-dd");
        if (Button9.Text == "Submit")
        {
            if (thedocvisit.DailyCheckUp_Insert_Update(1, TextBox24.Text, null, TextBox2.Text, testdate.ToString(), TextBox11.Text, TextBox10.Text, TextBox12.Text, TextBox13.Text, TextBox7.Text, TextBox1.Text, TextBox6.Text, TextBox14.Text, TextBox18.Text, TextBox15.Text, TextBox16.Text, TextBox17.Text, Session["CoCode"].ToString().Trim(), Session["userName"].ToString().Trim()) == true)
            {
                TextBox9.Enabled = true;
                TextBox24.Enabled = true;

                string[] ComplainId = HiddenField3.Value.Split(',');
                if (ComplainId.Length > 0)
                {
                    for (int i = 0; i < ComplainId.Length; i++)
                    {
                        thedocvisit.ComplainMap_Insert_Delete(1, TextBox2.Text, testdate.ToString(), TextBox24.Text, ComplainId[i], Session["CoCode"].ToString().Trim(), Session["userName"].ToString().Trim());
                    }
                }

                ScriptManager.RegisterStartupScript(this, this.GetType(), "msg", "alert('Inserted Successfully !');", true);
                Response.Redirect("../IPD/AdmissionPatientList.aspx");
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "msg", "alert('Error in Inserted Data !');", true);
            }
        }
        else
        {
            if (thedocvisit.DailyCheckUp_Insert_Update(2, TextBox24.Text, HiddenField1.Value, TextBox2.Text, testdate.ToString(), TextBox11.Text, TextBox10.Text, TextBox12.Text, TextBox13.Text, TextBox7.Text, TextBox1.Text, TextBox6.Text, TextBox14.Text, TextBox18.Text, TextBox15.Text, TextBox16.Text, TextBox17.Text, Session["CoCode"].ToString().Trim(), Session["userName"].ToString().Trim()) == true)
            {
                TextBox9.Enabled = true;
                TextBox24.Enabled = true;

                thedocvisit.ComplainMap_Insert_Delete(2, TextBox2.Text, testdate.ToString(), TextBox24.Text, null, Session["CoCode"].ToString().Trim(), Session["userName"].ToString().Trim());

                string[] ComplainId = HiddenField3.Value.Split(',');
                if (ComplainId.Length > 0)
                {
                    for (int i = 0; i < ComplainId.Length; i++)
                    {
                        thedocvisit.ComplainMap_Insert_Delete(1, TextBox2.Text, testdate.ToString(), TextBox24.Text, ComplainId[i], Session["CoCode"].ToString().Trim(), Session["userName"].ToString().Trim());
                    }
                }

                ScriptManager.RegisterStartupScript(this, this.GetType(), "msg", "alert('Updated Successfully !');", true);
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "msg", "alert('Error in Updated Data !');", true);
            }
        }

     

        GridFill();
        ResetBody();
    }

    public void ResetBody()
    {
        TextBox9.Text = ""; TextBox22.Text = ""; TextBox10.Text = ""; TextBox1.Text = ""; TextBox6.Text = ""; TextBox7.Text = "";

        TextBox11.Text = ""; TextBox12.Text = ""; TextBox13.Text = ""; TextBox14.Text = ""; TextBox15.Text = ""; TextBox16.Text = ""; TextBox17.Text = "";
        TextBox18.Text = "";   TextBox24.Text = "";
         
        Button9.Text = "Submit";
         
        LinkButton3.Text = "Add Complain";
        HiddenField1.Value = "";
        if (Objmain.checkaccss(Session["CoCode"].ToString(), Session["UserRoleID"].ToString().Trim(), "DAILY CHECK UP", checkAccessType.InsertAction) == false)
        {
            Button9.Enabled = false;
        }

    }
     
    protected void GridView6_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        ScriptManager.RegisterStartupScript(this, this.GetType(), "msg", "alert('You can not delete check up record !');", true);
    }
    protected void GridView6_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        //if (e.Row.RowType == DataControlRowType.DataRow)
        //{
        //    if (Objmain.checkaccss(Session["CoCode"].ToString(), Session["UserRoleID"].ToString().Trim(), "DAILY CHECK UP", checkAccessType.DeleteAction) == false)
        //    {
        //        e.Row.Cells[17].Visible = false;
        //    }
        //}
    }
}