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
using System.Web.Security;

 
public partial class IPD_AdmissionRegPopup : System.Web.UI.Page
{
   
    AdmissionRegPopup thereg = new AdmissionRegPopup(ConfigurationSettings.AppSettings["ConnectionString"].ToString());
    static int flag = 0;
    protected void Page_Load(object sender, EventArgs e)
    {
        
        if (Session["userName"] == null)
        {
            Response.Redirect("../LoginPage.aspx");
           
        }
      if(!IsPostBack)
      {
          Tab1Func();
      }
    }

    [System.Web.Script.Services.ScriptMethod()]
    [System.Web.Services.WebMethod]

    public static List<string> SearchCustomers(string prefixText, int count)
    {
       
        using (SqlConnection conn = new SqlConnection())
        {
            conn.ConnectionString = ConfigurationManager.ConnectionStrings["constr"].ConnectionString;
            using (SqlCommand cmd = new SqlCommand())
            {
                cmd.CommandText = "select distinct patient_name + '-' + PatientReg as patient_name from GN_PatientReg where and patient_name like @SearchText + '%'";
                cmd.Parameters.AddWithValue("@SearchText", prefixText);
                cmd.Connection = conn;
                conn.Open();
                List<string> customers = new List<string>();
                using (SqlDataReader sdr = cmd.ExecuteReader())
                {
                    while (sdr.Read())
                    {
                        customers.Add(sdr["patient_name"].ToString());
                    }
                }

                conn.Close();
                return customers;
            }
        }
    }
  
    protected void Button2_Click(object sender, EventArgs e)
    {
        if (flag == 2)
            GridFill2();
        else
            GridFill();
    }

    protected void Tab1_Click(object sender, EventArgs e)
    {
        Tab1Func();
    }

    public void Tab1Func()
    {
        Tab1.CssClass = "Clicked";
        Tab2.CssClass = "Initial";
        MainView.ActiveViewIndex = 0;
        flag = 1;
        GridFill();
    }

    protected void Tab2_Click(object sender, EventArgs e)
    {
        Tab1.CssClass = "Initial";
        Tab2.CssClass = "Clicked";
        MainView.ActiveViewIndex = 1;
        flag = 2;
        GridFill2();
    }

    public void GridFill()
    {
        GridView1.DataSource = thereg.GridAllPatient(txtreg.Text.Trim(), txtname.Text.Trim(), txtph.Text.Trim(), txtaddress.Text.Trim(),Session["CoCode"].ToString().Trim());
        GridView1.DataBind();
    }
    protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        Label lblregno = (Label)GridView1.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("lblregno");
        HiddenField1.Value = lblregno.Text;
        Label lblName = (Label)GridView1.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("lblName");
        Label lblladd = (Label)GridView1.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("lblladd");
        HiddenField2.Value = lblName.Text + "#" + lblladd.Text;
    }

    protected void GridView1_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        GridView1.PageIndex = e.NewPageIndex;
        GridFill();
    }

    public void GridFill2()
    {
        GridView2.DataSource = thereg.GridPopup2(TextBox1.Text.Trim(), TextBox2.Text.Trim(), TextBox4.Text.Trim(), TextBox3.Text.Trim(), Session["CoCode"].ToString().Trim());
        GridView2.DataBind();
    }
    protected void Button3_Click(object sender, EventArgs e)
    {
        GridFill2();
    }
    protected void GridView2_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        GridView2.PageIndex = e.NewPageIndex;
        GridFill2();
    }
    protected void GridView2_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        Label lblregno = (Label)GridView2.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("lblregno");
        HiddenField1.Value = lblregno.Text;
        Label lblName = (Label)GridView2.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("lblName");
        Label lblladd = (Label)GridView2.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("lblladd");
        HiddenField2.Value = lblName.Text + "#" + lblladd.Text;
    }
}