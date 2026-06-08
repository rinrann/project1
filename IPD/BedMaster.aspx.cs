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


public partial class Master_BedMaster : System.Web.UI.Page
{
    Main Objmain = new Main(ConfigurationSettings.AppSettings["ConnectionString"].ToString());
    BedMaster theHelper = new BedMaster(ConfigurationSettings.AppSettings["ConnectionString"].ToString());
    static string a, b, c, d, m, FloorID, wingid;
    
    string dt = DateTime.Now.ToString("MM/dd/yyyy");
    protected void Page_Load(object sender, EventArgs e)
    {

        Page.Title = "Bed Master";
        if (Session["userName"] == null)
        {
            Response.Redirect("../LoginPage.aspx");
        }
        if (Objmain.checkaccss(Session["CoCode"].ToString(), Session["UserRoleID"].ToString().Trim(), "BED MASTER", checkAccessType.ViewAction) == false)
        {
            Response.Redirect("../AccessDenied.aspx");
        }

        if (Objmain.checkaccss(Session["CoCode"].ToString(), Session["UserRoleID"].ToString().Trim(), "BED MASTER", checkAccessType.InsertAction) == false)
        {
            Button1.Enabled = false;
        }
        if (!IsPostBack)
        {
            GridFill();
            DropDownFill();
            Tab1Func();
                     
        }
    }


    private void GridFill()
    {
        GridView1.DataSource = theHelper.GetAllBed(Session["CoCode"].ToString().Trim());
        GridView1.DataBind();
    }
    private void ResetAllFields()
    {
        DropDownList1.SelectedIndex = 0;
       TextBox1.Text = "";
        DropDownList2.SelectedIndex = 0;
        DropDownList3.SelectedIndex = 0;
        DropDownList4.SelectedIndex = 0;
        TextBox2.Text = "";
        txtPatText.Text = "";
        Button1.Text = "Submit";

        if (Objmain.checkaccss(Session["CoCode"].ToString(), Session["UserRoleID"].ToString().Trim(), "BED MASTER", checkAccessType.InsertAction) == false)
        {
            Button1.Enabled = false;
        }
      
    }
 
    private void DropDownFill()
    {
          DropDownList1.Items.Clear();
          this.DropDownList1.DataSource = theHelper.DropdownID1(Session["CoCode"].ToString().Trim());
        this.DropDownList1.DataTextField = "FloorName";
        this.DropDownList1.DataValueField = "FloorID";
        this.DropDownList1.DataBind();
        this.DropDownList1.Items.Insert(0, new ListItem("--Select--", "0"));

        DropDownList2.Items.Clear();
        this.DropDownList2.DataSource = theHelper.DropdownID2(0, Session["CoCode"].ToString().Trim());
        this.DropDownList2.DataTextField = "WingsName";
        this.DropDownList2.DataValueField = "WingsID";
        this.DropDownList2.DataBind();
        this.DropDownList2.Items.Insert(0, new ListItem("--Select--", "0"));

        DropDownList3.Items.Clear();
        this.DropDownList3.DataSource = theHelper.DropdownID3(Session["CoCode"].ToString().Trim());
        this.DropDownList3.DataTextField = "RoomCategoryName";
        this.DropDownList3.DataValueField = "RoomCategoryID";
        this.DropDownList3.DataBind();
        this.DropDownList3.Items.Insert(0, new ListItem("--Select--", "0"));

        DropDownList4.Items.Clear();
        this.DropDownList4.DataSource = theHelper.DropdownID4("0", "0", "0", Session["CoCode"].ToString().Trim());
        this.DropDownList4.DataTextField = "RoomName";
        this.DropDownList4.DataValueField = "RoomID";
        this.DropDownList4.DataBind();
        this.DropDownList4.Items.Insert(0, new ListItem("--Select--", "0"));
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
            lblError.Text = "";
            Label lblBedNo = (Label)GridView1.Rows[index].FindControl("BedNo");
            HiddenField1.Value = lblBedNo.Text;
            DropDownFill();
            Label FloorName = (Label)GridView1.Rows[index].FindControl("FloorName");
            DropDownList1.SelectedIndex = SearchIndex(FloorName.Text, DropDownList1);


            Label WingsName = (Label)GridView1.Rows[index].FindControl("WingsName");
            DropDownList2.SelectedIndex = SearchIndex(WingsName.Text, DropDownList2);

            Label RoomCategoryName = (Label)GridView1.Rows[index].FindControl("RoomCategoryName");
            DropDownList3.SelectedIndex = SearchIndex(RoomCategoryName.Text, DropDownList3);


            Label RoomName = (Label)GridView1.Rows[index].FindControl("RoomName");
            DropDownList4.SelectedIndex = SearchIndex(RoomName.Text, DropDownList4);


            Label lblPatText = (Label)GridView1.Rows[index].FindControl("PatternText");
            txtPatText.Text = lblPatText.Text;

            Label lblbedtextno = (Label)GridView1.Rows[index].FindControl("lblbedtextno");
            string[] a = lblbedtextno.Text.Split('/');
            TextBox1.Text = a[2];

            Label lblcharges = (Label)GridView1.Rows[index].FindControl("lblcharges");
            TextBox2.Text = lblcharges.Text;

            Tab1Func();

            Button1.Text = "Update";
            if (Objmain.checkaccss(Session["CoCode"].ToString(), Session["UserRoleID"].ToString().Trim(), "BED MASTER", checkAccessType.UpdateAction) == false)
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
          Label lblBedNo = (Label)GridView1.Rows[e.RowIndex].FindControl("BedNo");
          theHelper.DeleteBedMaster(Convert.ToInt32(lblBedNo.Text), Session["CoCode"].ToString().Trim());
        lblError.ForeColor = System.Drawing.Color.Green;
        lblError.Text = "Deleted Successfully";
        GridFill();
        ResetAllFields();
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
    protected void Button1_Click(object sender, EventArgs e)
    {
         string a = txtPatText.Text + "/" + TextBox1.Text;

        if (Button1.Text == "Submit")
        {

            if (theHelper.InsertBedMaster(DropDownList1.SelectedValue, DropDownList2.SelectedValue, DropDownList3.SelectedValue, DropDownList4.SelectedValue, txtPatText.Text, Session["userName"].ToString(), dt, a, TextBox2.Text, Session["CoCode"].ToString().Trim()) == true)
            ScriptManager.RegisterStartupScript(this, this.GetType(), "msg", "alert('Insered Successfully !');", true);
            else
               ScriptManager.RegisterStartupScript(this, this.GetType(), "msg", "alert('Error in Insered Data !');", true);
        }
        else
        {
            if (theHelper.UpdateBedMaster(HiddenField1.Value, DropDownList1.SelectedValue, DropDownList2.SelectedValue, DropDownList3.SelectedValue, DropDownList4.SelectedValue, txtPatText.Text, dt, a, TextBox2.Text, Session["CoCode"].ToString().Trim()) == true)
            ScriptManager.RegisterStartupScript(this, this.GetType(), "msg", "alert('Updated Successfully !');", true);
            else
               ScriptManager.RegisterStartupScript(this, this.GetType(), "msg", "alert('Error in Updated Data !');", true);
            Button1.Text = "Submit";
        }

        ResetAllFields();
        GridFill();
      
    }
    protected void Button2_Click(object sender, EventArgs e)
    {
        ResetAllFields();
           
    }

    public void CallFunc()
    {
        DataTable dt = theHelper.DropdownPattern1(DropDownList1.SelectedValue);
        a = dt.Rows[0]["PatternText"].ToString();
        DataTable dt1 = theHelper.DropdownPattern2(DropDownList2.SelectedValue);
        b = dt1.Rows[0]["PatternText"].ToString();

        DataTable dt2 = theHelper.DropdownPattern3(DropDownList3.SelectedValue);
        c = dt2.Rows[0]["PatternText"].ToString();

        DataTable dt3 = theHelper.DropdownPattern4(DropDownList4.SelectedValue);
        d = dt3.Rows[0]["PatternText"].ToString();
        txtPatText.Text = a + b + "/" + c + d;
    }

    protected void DropDownList1_SelectedIndexChanged(object sender, EventArgs e)
    {
        FloorID = DropDownList1.SelectedValue;

        DropDownList2.DataSource = theHelper.DropdownID2(int.Parse(FloorID), Session["CoCode"].ToString().Trim());
        DropDownList2.DataBind();
        this.DropDownList2.Items.Insert(0, new ListItem("--Select--", "0"));
    }
    protected void DropDownList3_SelectedIndexChanged(object sender, EventArgs e)
    {
        string RoomCategoryID = DropDownList3.SelectedValue;

        DropDownList4.DataSource = theHelper.DropdownID4(RoomCategoryID, FloorID, wingid, Session["CoCode"].ToString().Trim());
        DropDownList4.DataBind();
        this.DropDownList4.Items.Insert(0,new ListItem("--Select--", "0"));

    }

    protected void DropDownList2_SelectedIndexChanged(object sender, EventArgs e)
    {
         wingid = DropDownList2.SelectedValue;
  
    }
    protected void DropDownList4_SelectedIndexChanged(object sender, EventArgs e)
    {
        CallFunc();
    }
    protected void Tab1_Click(object sender, EventArgs e)
    {
        Tab1Func();
    }

    protected void Tab2_Click(object sender, EventArgs e)
    {
        Tab1.CssClass = "Initial";
        Tab2.CssClass = "Clicked";
        MainView.ActiveViewIndex =1;
    }

    public void Tab1Func()
    {
        Tab1.CssClass = "Clicked";
        Tab2.CssClass = "Initial";
        MainView.ActiveViewIndex = 0;
    }

    [System.Web.Script.Services.ScriptMethod()]
    [System.Web.Services.WebMethod]
    public static List<string> Searchbedno(string prefixText, int count)
    {
        using (SqlConnection conn = new SqlConnection())
        {
            conn.ConnectionString = ConfigurationSettings.AppSettings["ConnectionString"].ToString();
            using (SqlCommand cmd = new SqlCommand())
            {
                cmd.CommandText = "select distinct BedNoText as Name from IPD_BedMaster where BedNoText like @SearchText +'%'";
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
    protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            if (Objmain.checkaccss(Session["CoCode"].ToString(), Session["UserRoleID"].ToString().Trim(), "BED MASTER", checkAccessType.DeleteAction) == false)
            {
                coldel.Visible = false;
                e.Row.Cells[9].Visible = false;
            }

        }
    }
}