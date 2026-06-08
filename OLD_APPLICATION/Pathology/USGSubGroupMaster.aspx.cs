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
 

public partial class Pathology_USGSubGroupMaster : System.Web.UI.Page
{
    Main Objmain = new Main(ConfigurationSettings.AppSettings["ConnectionString"].ToString());
    PH_USGSubGroupMaster theSubGroup = new PH_USGSubGroupMaster(ConfigurationSettings.AppSettings["ConnectionString"].ToString());
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["userName"] == null)
        {
            Response.Redirect("../LoginPage.aspx");
        }
        if (Objmain.checkaccss(Session["CoCode"].ToString(), Session["UserRoleID"].ToString().Trim(), "USG SUB GROUP MASTER", checkAccessType.ViewAction) == false)
        {
            Response.Redirect("../AccessDenied.aspx");
        }

        if (Objmain.checkaccss(Session["CoCode"].ToString(), Session["UserRoleID"].ToString().Trim(), "USG SUB GROUP MASTER", checkAccessType.InsertAction) == false)
        {
            Button1.Enabled = false;
        }
        if (Objmain.checkaccss(Session["CoCode"].ToString(), Session["UserRoleID"].ToString().Trim(), "USG SUB GROUP MASTER", checkAccessType.DeleteAction) == false)
        {
            GridView1.Columns[6].Visible = false;
        }
        Page.Title = "USG Sub Group Master";
        if (Session["userName"] == null)
        {
            Response.Redirect("../LoginPage.aspx");
        }
        if (!IsPostBack)
        {
            GenerateCode();
            GridFill();
            Tab1Func();
            DropDownFill();
        }
    }
    private void GridFill()
    {
        GridView1.DataSource = theSubGroup.GridFill(Session["CoCode"].ToString());
        GridView1.DataBind();
    }

    private void DropDownFill()
    {
        this.DropDownList1.DataSource = theSubGroup.DropdownGroup(Session["CoCode"].ToString());        
        this.DropDownList1.DataTextField = "GroupName";
        this.DropDownList1.DataValueField = "GroupCode";
        this.DropDownList1.DataBind();
        this.DropDownList1.Items.Insert(0, new ListItem("--Select--", "0"));
     }
    private void GenerateCode()
    {
        DataTable dt = theSubGroup.GenerateSubUGCCode(Session["CoCode"].ToString());
        TextBox1.Text = dt.Rows[0][0].ToString();
    }
    private void ResetAllFields()
    {

        GenerateCode();
        txttemname.Text = "";
        DropDownList1.SelectedIndex = 0;
        TextBox2.Text = ""; TextBox3.Text = ""; TextBox5.Text = ""; TextBox6.Text = ""; TextBox7.Text = ""; TextBox8.Text = ""; TextBox9.Text = ""; TextBox10.Text = "";
        TextBox11.Text = ""; TextBox12.Text = "";
  
        Button1.Text = "Submit";

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
            int index = Convert.ToInt32(e.CommandArgument);

            Label lblcode = (Label)GridView1.Rows[index].FindControl("lblcode");
            TextBox4.Value = lblcode.Text;
            TextBox1.Text = lblcode.Text;

            Label lblname = (Label)GridView1.Rows[index].FindControl("lblname");
            txttemname.Text = lblname.Text;

            Label lblcharge = (Label)GridView1.Rows[index].FindControl("lblcharge");
            TextBox2.Text = lblcharge.Text;

            Label lblno = (Label)GridView1.Rows[index].FindControl("lblno");
            TextBox3.Text = lblno.Text;

            Label lblsubname = (Label)GridView1.Rows[index].FindControl("lblsubname");
            DropDownList1.SelectedIndex = DropDownList1.Items.IndexOf(DropDownList1.Items.FindByText(lblsubname.Text));
            TextBox t;
            DataTable dt = theSubGroup.GetMappingValue(TextBox1.Text);
            for (int i = 5, j = 1, m = 0; m < dt.Rows.Count; i++, j++, m++)
            {
                t = (TextBox)Page.FindControl("ctl00$ContentPlaceHolder1$TextBox" + i.ToString());
                t.Text = dt.Rows[m]["Parameter"].ToString();
            }
            Button1.Text = "Update";
            if (Objmain.checkaccss(Session["CoCode"].ToString(), Session["UserRoleID"].ToString().Trim(), "USG SUB GROUP MASTER", checkAccessType.UpdateAction) == false)
            {
                Button1.Enabled = false;
            }
            else
            {
                Button1.Enabled = true;
            }

            Tab1Func();
        }
    }
    protected void GridView1_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
         Label lblcode = (Label)GridView1.Rows[e.RowIndex].FindControl("lblcode");
         theSubGroup.DeleteGroup(lblcode.Text, Session["CoCode"].ToString());
        lblError.ForeColor = System.Drawing.Color.Green;
        lblError.Text = "Deleted Successfully";
        GridFill();
        ResetAllFields();
    }
    protected void Button1_Click(object sender, EventArgs e)
    {
        TextBox t;
        string id;
         if (TextBox4.Value != "")
        {
            id = TextBox4.Value;
        }
        else
        {
            id = "null";
        }
        if (Button1.Text == "Submit")
        {

            theSubGroup.InsertGroup(TextBox1.Text, DropDownList1.SelectedValue, txttemname.Text, TextBox2.Text, TextBox3.Text, Session["userName"].ToString(), Session["CoCode"].ToString());
            ScriptManager.RegisterStartupScript(this, this.GetType(), "msg", "alert('Inserted Successfully !');", true);
            for(int i=5;i<=12;i++)
            {
                t = (TextBox)Page.FindControl("ctl00$ContentPlaceHolder1$TextBox" + i.ToString());
                if (t.Text != "")
                    theSubGroup.InsertSubGroupMapping(TextBox1.Text, t.Text, Session["CoCode"].ToString());
                else
                    break;
            }


        }
        else
        {


            theSubGroup.UpdateGroup(TextBox4.Value, DropDownList1.SelectedValue, txttemname.Text, TextBox2.Text, TextBox3.Text, Session["CoCode"].ToString());
            theSubGroup.DeleteMappingGroup(TextBox4.Value, Session["CoCode"].ToString());

            for (int i = 5; i <= 12; i++)
            {
                t = (TextBox)Page.FindControl("ctl00$ContentPlaceHolder1$TextBox" + i.ToString());
                if (t.Text != "")
                    theSubGroup.InsertSubGroupMapping(TextBox1.Text, t.Text, Session["CoCode"].ToString());
                else
                    break;
            }
            ScriptManager.RegisterStartupScript(this, this.GetType(), "msg", "alert('Updated Successfully !');", true);
            Button1.Text = "Submit";

        }

        GridFill();
        ResetAllFields();
    }
    protected void Button2_Click(object sender, EventArgs e)
    {
        ResetAllFields();
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
    public static List<string> SearchUSGSbuGroup(string prefixText, int count)
    {
        using (SqlConnection conn = new SqlConnection())
        {
            conn.ConnectionString = ConfigurationSettings.AppSettings["ConnectionString"].ToString();
            using (SqlCommand cmd = new SqlCommand())
            {
                cmd.CommandText = "select distinct TestName as Name from PH_TestMaster where TestName like @SearchText +'%'";
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
}