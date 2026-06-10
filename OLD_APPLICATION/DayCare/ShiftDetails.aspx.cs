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
 
public partial class Master_ShiftDetails : System.Web.UI.Page
{
    Main Objmain = new Main(ConfigurationSettings.AppSettings["ConnectionString"].ToString());
    dc_ShiftDetails theshift = new dc_ShiftDetails(ConfigurationSettings.AppSettings["ConnectionString"].ToString());
    protected void Page_Load(object sender, EventArgs e)
    {
        Page.Title = "Shift Details";
        if (Session["userName"] == null)
        {
            Response.Redirect("../LoginPage.aspx");
        }
        if (Objmain.checkaccss(Session["CoCode"].ToString(), Session["UserRoleID"].ToString().Trim(), "SHIFT DETAILS", checkAccessType.ViewAction) == false)
        {
            Response.Redirect("../AccessDenied.aspx");
        }

        if (Objmain.checkaccss(Session["CoCode"].ToString(), Session["UserRoleID"].ToString().Trim(), "SHIFT DETAILS", checkAccessType.InsertAction) == false)
        {
            Button1.Enabled = false;
        }
        if (Objmain.checkaccss(Session["CoCode"].ToString(), Session["UserRoleID"].ToString().Trim(), "SHIFT DETAILS", checkAccessType.DeleteAction) == false)
        {
            GridView1.Columns[6].Visible = false;
        }
        if(!IsPostBack)
        {
            Tab1Func();
            GridFill();
        }
        Page.Title = "Shift Details";
       
       
    }
    protected void Button1_Click(object sender, EventArgs e)
    {
        int id;
        if (TextBox4.Value != "")
        {
            id = Convert.ToInt32(TextBox4.Value);
        }
        else
        {
            id = 0;
        }
        if (Button1.Text == "Submit")
        {
            //dibynedu
            if (theshift.InsertShift_Master(TextBox1.Text, TextBox2.Text, TextBox3.Text, Convert.ToInt32(TextBox5.Text), Session["CoCode"].ToString()))
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "msg", "alert('Insered Successfully !');", true);
            }
            else
            {
                // dibynedu
                ScriptManager.RegisterStartupScript(this, this.GetType(), "msg", "alert('Error in Insered Data !');", true);
            }
        }
        else
        {
            //dibyendu
            if (theshift.UpdateShift_Master(id, TextBox1.Text, TextBox2.Text, TextBox3.Text, Convert.ToInt32(TextBox5.Text), Session["CoCode"].ToString()))
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "msg", "alert('Updated Successfully !');", true);

                Button1.Text = "Submit";
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "msg", "alert('Error in Updated Data !');", true);
            }
        }

        GridFill();
        ResetAllFields();
    }
    protected void Button2_Click(object sender, EventArgs e)
    {
        ResetAllFields();
    }
    private void GridFill()
    {
        GridView1.DataSource = theshift.GetAllShift(Session["CoCode"].ToString().Trim());
        GridView1.DataBind();
    }
    private void ResetAllFields()
    {
        TextBox1.Text = "";
        TextBox2.Text = "";
        TextBox3.Text = "";
        TextBox4.Value = "";
        Button1.Text = "Submit";
        TextBox5.Text = "";
        //if (Objmain.checkaccss(Session["CoCode"].ToString(), Session["UserRoleID"].ToString().Trim(), "SHIFT DETAILS", checkAccessType.InsertAction) == false)
        //{
        //    Button1.Enabled = false;
        //}
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
            Label lblShiftID = (Label)GridView1.Rows[index].FindControl("lblShiftID");
            TextBox4.Value = lblShiftID.Text;

            Label lblShiftName = (Label)GridView1.Rows[index].FindControl("lblShiftName");
            TextBox1.Text = lblShiftName.Text;

            Label lbltimefrom = (Label)GridView1.Rows[index].FindControl("lbltimefrom");
            TextBox2.Text = lbltimefrom.Text;

            Label lbltotime = (Label)GridView1.Rows[index].FindControl("lbltotime");
            TextBox3.Text = lbltotime.Text;

            Label lblmaxpatient = (Label)GridView1.Rows[index].FindControl("lblmaxpatient");
            TextBox5.Text = lblmaxpatient.Text;
            Tab1Func();
            Button1.Text = "Update";
            if (Objmain.checkaccss(Session["CoCode"].ToString(), Session["UserRoleID"].ToString().Trim(), "SHIFT DETAILS", checkAccessType.UpdateAction) == false)
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
        Label lblShiftID = (Label)GridView1.Rows[e.RowIndex].FindControl("lblShiftID");
        theshift.DeleteShift_Master(Convert.ToInt32(lblShiftID.Text), Session["CoCode"].ToString().Trim());
        ScriptManager.RegisterStartupScript(this, this.GetType(), "msg", "alert('Deleted Successfully !');", true);
        GridFill();
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
    public static List<string> SearchShift(string prefixText, int count)
    {
        using (SqlConnection conn = new SqlConnection())
        {
            conn.ConnectionString = ConfigurationSettings.AppSettings["ConnectionString"].ToString();
            using (SqlCommand cmd = new SqlCommand())
            {
                cmd.CommandText = "select distinct ShiftName as Name from DC_ShiftDtls where ShiftName like @SearchText +'%'";
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
            if (Objmain.checkaccss(Session["CoCode"].ToString(), Session["UserRoleID"].ToString().Trim(), "SHIFT DETAILS", checkAccessType.DeleteAction) == false)
            {
                e.Row.Cells[6].Visible = false;
            }
        }
    }
}