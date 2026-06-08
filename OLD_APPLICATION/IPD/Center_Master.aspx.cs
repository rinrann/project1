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
using System.Collections.Generic;

public partial class Master_Center_Master : System.Web.UI.Page
{
    Main Objmain = new Main(ConfigurationSettings.AppSettings["ConnectionString"].ToString());
    Center_Master theHelper = new Center_Master(ConfigurationSettings.AppSettings["ConnectionString"].ToString());
    string dt = DateTime.Now.ToString("MM/dd/yyyy");

    protected void Page_Load(object sender, EventArgs e)
    {

        if (!IsPostBack)
        {
            Tab1Func();
        }
        Page.Title = "Sister/Aya Center";
        if (Session["userName"] == null)
        {
            Response.Redirect("../LoginPage.aspx");
        }
        if (Objmain.checkaccss(Session["CoCode"].ToString(), Session["UserRoleID"].ToString().Trim(), "SISTER / AYA CENTRE", checkAccessType.ViewAction) == false)
        {
            Response.Redirect("../AccessDenied.aspx");
        }

        if (Objmain.checkaccss(Session["CoCode"].ToString(), Session["UserRoleID"].ToString().Trim(), "SISTER / AYA CENTRE", checkAccessType.InsertAction) == false)
        {
            Button1.Enabled = false;
        }
        if (Objmain.checkaccss(Session["CoCode"].ToString(), Session["UserRoleID"].ToString().Trim(), "SISTER / AYA CENTRE", checkAccessType.DeleteAction) == false)
        {
            GridView1.Columns[8].Visible = false;
        }
        GridFill();
    }
    
    protected void Button1_Click(object sender, EventArgs e)
    {
        int Center_Id;
        if (HiddenField1.Value == "")
        {
            Center_Id = 0;
        }
        else
        {
            Center_Id = Convert.ToInt32(HiddenField1.Value);
        }
        string Center_Name = TextBox1.Text;
        string Center_Address = TextBox2.Text;
        string Contact_Person = TextBox3.Text;
        string Phone_No = TextBox4.Text + " " + TextBox5.Text;
        string alPhone_No = TextBox6.Text + " " + TextBox7.Text;
        string EmailId = TextBox8.Text;

        if (Button1.Text == "Submit")
        {
            if (theHelper.InsertCenter_Master(Center_Name.ToUpper(), Center_Address.ToUpper(), Contact_Person.ToUpper(), Phone_No, alPhone_No, EmailId, dt, Session["CoCode"].ToString().Trim()) == true)
            ScriptManager.RegisterStartupScript(this, this.GetType(), "msg", "alert('Insered Successfully !');", true);
            else
              ScriptManager.RegisterStartupScript(this, this.GetType(), "msg", "alert('Error in Insered Data !');", true);
        }
        else
        {
            if (theHelper.UpdateCenter_Master(Convert.ToInt32(HiddenField1.Value), Center_Name.ToUpper(), Center_Address.ToUpper(), Contact_Person.ToUpper(), Phone_No, alPhone_No, EmailId, dt, Session["CoCode"].ToString().Trim()) == true)
            ScriptManager.RegisterStartupScript(this, this.GetType(), "msg", "alert('Updated Successfully !');", true);
            else
               ScriptManager.RegisterStartupScript(this, this.GetType(), "msg", "alert('Error in Updated Data !');", true);
            Button1.Text = "Submit";
        }

        GridView1.DataSource = theHelper.GetAllhospital(Session["CoCode"].ToString().Trim());
        GridView1.DataBind();
        ResetAllFields();
    }

    private void GridFill()
    {
        GridView1.DataSource = theHelper.GetAllhospital(Session["CoCode"].ToString().Trim());
        GridView1.DataBind();
    }

    private void ResetAllFields()
    {
        HiddenField1.Value = "";
        TextBox2.Text = "";
        TextBox3.Text = "";
        Button1.Text = "Submit";
        TextBox1.Text = "";
        TextBox5.Text = "";
        TextBox8.Text = ""; TextBox7.Text = "";
        if (Objmain.checkaccss(Session["CoCode"].ToString(), Session["UserRoleID"].ToString().Trim(), "SISTER / AYA CENTRE", checkAccessType.InsertAction) == false)
        {
            Button1.Enabled = false;
        }
    }

    private int cint(string p)
    {
        throw new Exception("The method or operation is not implemented.");
    }
    
    protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "Select")
        {
            int index = Convert.ToInt32(e.CommandArgument);
            Label lblCenter_Id = (Label)GridView1.Rows[index].FindControl("lblCenter_Id");
            HiddenField1.Value = lblCenter_Id.Text;

            Label lblCenter_Name = (Label)GridView1.Rows[index].FindControl("lblCenter_Name");
            TextBox1.Text = lblCenter_Name.Text;

            Label lblCenter_Address = (Label)GridView1.Rows[index].FindControl("lblCenter_Address");
            TextBox2.Text = lblCenter_Address.Text;

            Label lblContact_Person = (Label)GridView1.Rows[index].FindControl("lblContact_Person");
            TextBox3.Text = lblContact_Person.Text;

            Label lblPhone_No = (Label)GridView1.Rows[index].FindControl("lblPhone_No");
            string[] cont = lblPhone_No.Text.Split(' ');
            TextBox4.Text = "+91";
            if(cont.Length>1)
            TextBox5.Text = cont[1];


            Label lblalPhone_No = (Label)GridView1.Rows[index].FindControl("lblalPhone_No");
            string[] cont1 = lblalPhone_No.Text.Split(' ');
            TextBox6.Text = "+91";
            if(cont1.Length>1)
            TextBox7.Text = cont1[1];

            Label lblEmailId = (Label)GridView1.Rows[index].FindControl("lblEmailId");
            TextBox8.Text = lblEmailId.Text;
            Tab1Func();
            Button1.Text = "Update";
            if (Objmain.checkaccss(Session["CoCode"].ToString(), Session["UserRoleID"].ToString().Trim(), "SISTER / AYA CENTRE", checkAccessType.UpdateAction) == false)
            {
                Button1.Enabled = false;
            }
            else
            {
                Button1.Enabled = true;
            }
        }
    }

    protected void Button2_Click(object sender, EventArgs e)
    {
        ResetAllFields();
    }

    protected void GridView1_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        Label lblCenter_Id = (Label)GridView1.Rows[e.RowIndex].FindControl("lblCenter_Id");
        theHelper.DeleteCenter_Master(Convert.ToInt32(lblCenter_Id.Text), Session["CoCode"].ToString().Trim());
        ScriptManager.RegisterStartupScript(this, this.GetType(), "msg", "alert('Deleted Successfully !');", true);
        GridFill();
        ResetAllFields();
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

    [System.Web.Script.Services.ScriptMethod()]
    [System.Web.Services.WebMethod]
    public static List<string> Searchcenter(string prefixText, int count)
    {
        string compcode = string.Empty;
        string yearcode = string.Empty;
        using (SqlConnection conn = new SqlConnection())
        {
            conn.ConnectionString = ConfigurationSettings.AppSettings["ConnectionString"].ToString();
            using (SqlCommand cmd = new SqlCommand())
            {
                compcode = HttpContext.Current.Session["CoCode"].ToString();
                yearcode = HttpContext.Current.Session["YearCode"].ToString();
                cmd.CommandText = "select distinct CenterName as Name from IPD_SisterAyaCenter where compcode=@Compcode and CenterName like @SearchText +'%'";
                cmd.Parameters.AddWithValue("@SearchText", prefixText);
                cmd.Parameters.AddWithValue("@Compcode", compcode);
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
    protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            if (Objmain.checkaccss(Session["CoCode"].ToString(), Session["UserRoleID"].ToString().Trim(), "SISTER / AYA CENTRE", checkAccessType.DeleteAction) == false)
            {
                e.Row.Cells[8].Visible = false;
            }

        }
    }
}
