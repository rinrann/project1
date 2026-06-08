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

public partial class IPD_BedTransfer : System.Web.UI.Page
{
    Main Objmain = new Main(ConfigurationSettings.AppSettings["ConnectionString"].ToString());
    BedTransfer theHelper = new BedTransfer(ConfigurationSettings.AppSettings["ConnectionString"].ToString());
    static string FloorID, categoryid, wingid;
    protected void Page_Load(object sender, EventArgs e)
    {
        Page.Title = "Bed Transfer";
        if (Session["userName"] == null)
        {
            Response.Redirect("../LoginPage.aspx");
        }
        if (Objmain.checkaccss(Session["CoCode"].ToString(), Session["UserRoleID"].ToString().Trim(), "BED TRANSFER", checkAccessType.ViewAction) == false)
        {
            Response.Redirect("../AccessDenied.aspx");
        }

        if (Objmain.checkaccss(Session["CoCode"].ToString(), Session["UserRoleID"].ToString().Trim(), "BED TRANSFER", checkAccessType.InsertAction) == false)
        {
            Button1.Enabled = false;
        }
        
        if (!IsPostBack)
        {
            Tab1Func();

            ScriptManager.RegisterStartupScript(this, this.GetType(), "msg", "GetDatetime();", true);

            if (Session["RegNo"] != null)
            {
                 TextBox3.Text= Session["RegNo"].ToString();
                 DataTable dt = theHelper.GetPatientDetails(TextBox3.Text, Session["CoCode"].ToString().Trim());
                if (dt.Rows.Count > 0)
                {
                    HiddenField1.Value = dt.Rows[0]["BedNo"].ToString();
                    txtBedNo.Text = dt.Rows[0]["BedNoText"].ToString();
                    TextBox4.Text = dt.Rows[0]["patient_name"].ToString();
                    GridFill();
                }
            }

            Session["RegNo"] = null;
        }
      
      
    }
 
    private void ResetAllFields()
    {

       // txtBedNo.Text = ""; TextBox3.Text = ""; TextBox4.Text = "";
        HiddenField2.Value = "";
        TextBox1.Text = ""; TextBox2.Text = ""; TextBox5.Text = "";
        Button1.Text = "Submit";
        if (Objmain.checkaccss(Session["CoCode"].ToString(), Session["UserRoleID"].ToString().Trim(), "BED TRANSFER", checkAccessType.InsertAction) == false)
        {
            Button1.Enabled = false;
        }
       
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

    public void GridFill()
    {
        GridView1.DataSource = theHelper.GridFill(TextBox3.Text, Session["CoCode"].ToString().Trim());
        GridView1.DataBind();
    }
    protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "Select")
        {
            int index = Convert.ToInt32(e.CommandArgument);
           
            Label lblbedno = (Label)GridView1.Rows[index].FindControl("lblbedno");
            TextBox5.Text = lblbedno.Text;

            Label lblbednoId = (Label)GridView1.Rows[index].FindControl("lblbednoId");
            HiddenField3.Value = lblbednoId.Text;

            Label lblRowId = (Label)GridView1.Rows[index].FindControl("lblRowId");
            HiddenField1.Value = lblRowId.Text;

            TextBox1.Enabled = false;
            TextBox2.Enabled = false;

            Tab1Func(); 
            Button1.Text = "Update";
            if (Objmain.checkaccss(Session["CoCode"].ToString(), Session["UserRoleID"].ToString().Trim(), "BED TRANSFER", checkAccessType.UpdateAction) == false)
            {
                Button1.Enabled = false;
            }
            else
            {
                Button1.Enabled = true;
            }
        }
    }
     
    protected void Button1_Click(object sender, EventArgs e)
    {
      
        if (Button1.Text == "Submit")
        {
            System.Globalization.DateTimeFormatInfo dtf = new DateTimeFormatInfo();
            DateTime testdate = DateTime.ParseExact(TextBox1.Text, "dd/MM/yyyy", dtf);
            if (theHelper.InsertBedMaster(TextBox3.Text, HiddenField1.Value, HiddenField2.Value, testdate.ToString("yyyy-MM-dd"), TextBox2.Text, Session["CoCode"].ToString().Trim(), Session["userName"].ToString().Trim()) == true)
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "msg", "alert('Bed Transfer Successfully !');", true);
                Response.Redirect("../IPD/AdmissionPatientList.aspx");
            }
            else
                ScriptManager.RegisterStartupScript(this, this.GetType(), "msg", "alert('Error in Bed Transfer !');", true);
        }

        else
        {
            if (theHelper.UpdateBedMaster(HiddenField1.Value, TextBox3.Text, HiddenField3.Value, HiddenField2.Value, Session["CoCode"].ToString().Trim(), Session["userName"].ToString().Trim()) == true)
                ScriptManager.RegisterStartupScript(this, this.GetType(), "msg", "alert('Bed Transfer Successfully !');", true);
            else
                ScriptManager.RegisterStartupScript(this, this.GetType(), "msg", "alert('Error in Bed Transfer !');", true);
        }
        GridFill();
        GetDetails();
        ResetAllFields();

        TextBox1.Enabled = true;
        TextBox2.Enabled = true;
      
    }

    public void GetDetails()
    {
        DataTable dt = theHelper.GetPatientDetails(TextBox3.Text, Session["CoCode"].ToString().Trim());
        if (dt.Rows.Count > 0)
        {
            HiddenField1.Value = dt.Rows[0]["BedNo"].ToString();
            txtBedNo.Text = dt.Rows[0]["BedNoText"].ToString();
            TextBox4.Text = dt.Rows[0]["patient_name"].ToString();
            GridFill();
        }
        else
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "msg", "alert('Select IPD Admited Patient !');", true);
        }
    }
     
    protected void Button4_Click(object sender, EventArgs e)
    {

        GetDetails();
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


    protected void Button3_Click(object sender, EventArgs e)
    {

    }
    protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        
    }
}