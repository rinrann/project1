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

public partial class Pathology_ProfileMaster : System.Web.UI.Page
{
    Main Objmain = new Main(ConfigurationSettings.AppSettings["ConnectionString"].ToString());
    PH_ProfileMaster theprofile = new PH_ProfileMaster(ConfigurationSettings.AppSettings["ConnectionString"].ToString());
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["userId"] == null)
        {
            Response.Redirect("../LoginPage.aspx");
        }
        if (Objmain.checkaccss(Session["CoCode"].ToString(), Session["UserRoleID"].ToString().Trim(), "INVESTIGATION GROUP MASTER", checkAccessType.ViewAction) == false)
        {
            Response.Redirect("../AccessDenied.aspx");
        }

        if (Objmain.checkaccss(Session["CoCode"].ToString(), Session["UserRoleID"].ToString().Trim(), "INVESTIGATION GROUP MASTER", checkAccessType.InsertAction) == false)
        {
            Button3.Enabled = false;
        }
        Page.Title = "Test Group Master";


        if (Session["userId"] == null)
        {
            Response.Redirect("../LoginPage.aspx");
        }
        if (!IsPostBack)
        {
            //Button1.Visible = false;
            //Button2.Visible = false;
            GenerateProfileCode();
            DropDownFill();
            GridFill();
        }

    }
    public void initial()
    {
        DataTable dt = new DataTable("MyTable");
        dt.Columns.Add("TestId", typeof(string));
        dt.Columns.Add("TestName", typeof(string));
        dt.Columns.Add("cost", typeof(decimal));

        DataRow dr = dt.NewRow();
        dr["TestId"] = null;
        dr["TestName"] = null;
        dr["cost"] = 0;

        dt.Rows.Add(dr);

        GridView2.DataSource = dt;
        GridView2.DataBind();

        GridView2.Rows[0].Visible = false;
    }
    public void GenerateProfileCode()
    {
         DataTable dt = theprofile.GenerateProfileCode();
        txtcode.Text = dt.Rows[0][0].ToString();
    }
    private void DropDownFill()
    {
        this.DropDownList1.DataSource = theprofile.DropdownDepartment(Session["CoCode"].ToString());
        this.DropDownList1.DataTextField = "DeptName";
        this.DropDownList1.DataValueField = "DeptCode";
        this.DropDownList1.DataBind();
        this.DropDownList1.Items.Insert(0, new ListItem("--Select--", "0"));


        this.DropDownList2.DataSource = theprofile.DropdownDepartment(Session["CoCode"].ToString());
        this.DropDownList2.DataTextField = "DeptName";
        this.DropDownList2.DataValueField = "DeptCode";
        this.DropDownList2.DataBind();
        this.DropDownList2.Items.Insert(0, new ListItem("--Select--", "0"));
    }
    protected void Button3_Click(object sender, EventArgs e)
    {
         HiddenField1.Value = txtcode.Text;
        if (Button3.Text == "Submit")
        {
            theprofile.InsertProfile(txtcode.Text, txtname.Text, txtprice.Text, DropDownList1.SelectedValue, Session["CoCode"].ToString(), Session["CoCode"].ToString());
            for (int i = 1; i < GridView2.Rows.Count; i++)
            {
                Label lblid = (Label)GridView2.Rows[i].Cells[2].FindControl("lblid");

                theprofile.InsertMapping(HiddenField1.Value, lblid.Text,Session["CoCode"].ToString(), Session["CoCode"].ToString());

            }
            ScriptManager.RegisterStartupScript(this, this.GetType(), "msg", "alert('Inserted Successfully !');", true);

        }
        else
        {
            theprofile.DeleteMapping(HiddenField1.Value, Session["CoCode"].ToString());
            theprofile.UpdateProfile(txtcode.Text, txtname.Text, txtprice.Text, DropDownList1.SelectedValue, Session["CoCode"].ToString(), Session["CoCode"].ToString());
            for (int i = 0; i < GridView2.Rows.Count; i++)
            {
                Label lblid = (Label)GridView2.Rows[i].Cells[2].FindControl("lblid");

                theprofile.InsertMapping(HiddenField1.Value, lblid.Text, Session["CoCode"].ToString(), Session["CoCode"].ToString());

            }
            ScriptManager.RegisterStartupScript(this, this.GetType(), "msg", "alert('Updated Successfully !');", true);
        }
      
        ResetAllFields();

        GridFill();
    }
    public void ResetAllFields()
    {
        GenerateProfileCode();
        DropDownList1.SelectedIndex = 0;
        DropDownList2.SelectedIndex = 0;
        txtname.Text = "";
        txtprice.Text = "";

    }
    protected void Button4_Click(object sender, EventArgs e)
    {
        ResetAllFields();
        GridFill();
    }
    private void GridFill1()
    {
        GridView1.DataSource = theprofile.GridChemicalDetails(txtcode.Text, DropDownList2.SelectedValue, TextBox1.Text, Session["CoCode"].ToString());
        GridView1.DataBind();

        GridView2.DataSource = theprofile.GridChemicalRightDetails(txtcode.Text, Session["CoCode"].ToString());
        GridView2.DataBind();
         }
    private void GridFill()
    {
        GridView1.DataSource = theprofile.GridChemicalDetails(txtcode.Text, DropDownList2.SelectedValue, TextBox1.Text, Session["CoCode"].ToString());
        GridView1.DataBind();

        GridView2.DataSource = theprofile.GridChemicalRightDetails(txtcode.Text, Session["CoCode"].ToString());
        GridView2.DataBind();
        initial();
    }
    protected void Button1_Click(object sender, EventArgs e)
    {
        Button3.Visible = true;
        Button4.Visible = true;
        DataTable dt = new DataTable();
        DataRow row = dt.NewRow();
        DataTable dtLeft = new DataTable();
        DataRow rowLeft = dtLeft.NewRow();

        dt.Columns.Add("TestId", typeof(string));
        dt.Columns.Add("TestName", typeof(string));
        dt.Columns.Add("cost", typeof(decimal));


        dtLeft.Columns.Add("TestId", typeof(string));
        dtLeft.Columns.Add("TestName", typeof(string));
        dtLeft.Columns.Add("Cost", typeof(decimal));

        if (GridView2.Rows.Count > 0)
        {
            for (int i = 0; i < GridView2.Rows.Count; i++)
            {
                Label lblname = (Label)GridView2.Rows[i].FindControl("lblname");
                Label lblid = (Label)GridView2.Rows[i].FindControl("lblid");
                Label lblcost = (Label)GridView2.Rows[i].FindControl("lblcost");

                row["TestId"] = lblid.Text;
                row["TestName"] = lblname.Text;
                row["Cost"] = Convert.ToDouble(lblcost.Text);
                dt.Rows.Add(row);
                row = dt.NewRow();
            }
        }

        for (int i = 0; i < GridView1.Rows.Count; i++)
        {
            CheckBox chk = (CheckBox)GridView1.Rows[i].Cells[1].FindControl("CheckBox1");
            Label lblname = (Label)GridView1.Rows[i].Cells[2].FindControl("lblname");
            Label lblid = (Label)GridView1.Rows[i].Cells[2].FindControl("lblid");
            Label lblcost = (Label)GridView1.Rows[i].Cells[2].FindControl("lblcost");

            if (chk.Checked)
            {
                row["TestId"] = lblid.Text;
                row["TestName"] = lblname.Text;
                row["Cost"] = Convert.ToDouble(lblcost.Text);

                dt.Rows.Add(row);
                row = dt.NewRow();
            }
            else
            {
                rowLeft["TestId"] = lblid.Text;
                rowLeft["TestName"] = lblname.Text;
                rowLeft["Cost"] = Convert.ToDouble(lblcost.Text);
                dtLeft.Rows.Add(rowLeft);
                rowLeft = dtLeft.NewRow();
            }

        }
        GridView2.DataSource = dt;
        GridView2.DataBind();

        GridView1.DataSource = dtLeft;
        GridView1.DataBind();
     //   GridView2.Rows[0].Visible = false;
    }
    protected void Button2_Click(object sender, EventArgs e)
    {
        DataTable dt;
        dt = new DataTable();

        DataRow row = dt.NewRow();
        DataTable dtLeft = new DataTable();

        DataRow rowLeft = dtLeft.NewRow();

        dt.Columns.Add("TestId", typeof(string));
        dt.Columns.Add("TestName", typeof(string));
        dt.Columns.Add("Cost", typeof(decimal));


        dtLeft.Columns.Add("TestId", typeof(string));
        dtLeft.Columns.Add("TestName", typeof(string));
        dtLeft.Columns.Add("Cost", typeof(decimal));
        if (GridView1.Rows.Count > 0)
        {
            for (int i = 0; i < GridView1.Rows.Count; i++)
            {
                Label lblname = (Label)GridView1.Rows[i].Cells[2].FindControl("lblname");
                Label lblid = (Label)GridView1.Rows[i].Cells[2].FindControl("lblid");
                Label lblcost = (Label)GridView1.Rows[i].Cells[2].FindControl("lblcost");

                rowLeft["TestId"] = lblid.Text;
                rowLeft["TestName"] = lblname.Text;
                rowLeft["Cost"] = Convert.ToDouble(lblcost.Text);
                dtLeft.Rows.Add(rowLeft);
                rowLeft = dtLeft.NewRow();
            }
        }

        for (int i = 0; i < GridView2.Rows.Count; i++)
        {

            CheckBox chk = (CheckBox)GridView2.Rows[i].Cells[1].FindControl("CheckBox2");
            Label lblname = (Label)GridView2.Rows[i].Cells[2].FindControl("lblname");
            Label lblid = (Label)GridView2.Rows[i].Cells[2].FindControl("lblid");
            Label lblcost = (Label)GridView2.Rows[i].Cells[2].FindControl("lblcost");

            if (!chk.Checked)
            {
                row["TestId"] = lblid.Text;
                row["TestName"] = lblname.Text;
                row["Cost"] = Convert.ToDouble(lblcost.Text);
                dt.Rows.Add(row);
                row = dt.NewRow();
            }
            else
            {
                rowLeft["TestId"] = lblid.Text;
                rowLeft["TestName"] = lblname.Text;
                rowLeft["Cost"] = Convert.ToDouble(lblcost.Text);
                dtLeft.Rows.Add(rowLeft);
                rowLeft = dtLeft.NewRow();
            }

        }
        GridView2.DataSource = dt;
        GridView2.DataBind();

        GridView1.DataSource = dtLeft;
        GridView1.DataBind();
       // GridView2.Rows[0].Visible = false;
    }
    protected void Button5_Click(object sender, EventArgs e)
    {
         DataTable dt = theprofile.GridFill(txtcode.Text);
        txtname.Text = dt.Rows[0]["ProfileName"].ToString();
        txtprice.Text = dt.Rows[0]["Price"].ToString();
        DropDownList1.SelectedValue = dt.Rows[0]["DepartmentID"].ToString();
        GridFill1();
        Button3.Text = "Update";
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
     protected void Button7_Click(object sender, EventArgs e)
     {
         GridFill();
     }

     [System.Web.Script.Services.ScriptMethod()]
     [System.Web.Services.WebMethod]
     public static List<string> SearchTestGroup(string prefixText, int count)
     {
         using (SqlConnection conn = new SqlConnection())
         {
             conn.ConnectionString = ConfigurationSettings.AppSettings["ConnectionString"].ToString();
             using (SqlCommand cmd = new SqlCommand())
             {
                 cmd.CommandText = "select distinct ProfileName as Name from PH_ProfileMaster where ProfileName like @SearchText +'%'";
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