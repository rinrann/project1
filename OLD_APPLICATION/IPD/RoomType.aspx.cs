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

 

public partial class Master_RoomType : System.Web.UI.Page
{
    Main Objmain = new Main(ConfigurationSettings.AppSettings["ConnectionString"].ToString());
    RoomType theHelper = new RoomType(ConfigurationSettings.AppSettings["ConnectionString"].ToString());
    string dt = DateTime.Now.ToString("MM/dd/yyyy");
    protected void Page_Load(object sender, EventArgs e)
    {
        Page.Title = "Room Type";
        
        if (Session["userName"] == null)
        {
            Response.Redirect("../LoginPage.aspx");
        }
        if (Objmain.checkaccss(Session["CoCode"].ToString(), Session["UserRoleID"].ToString().Trim(), "ROOM TYPE", checkAccessType.ViewAction) == false)
        {
            Response.Redirect("../AccessDenied.aspx");
        }

        if (Objmain.checkaccss(Session["CoCode"].ToString(), Session["UserRoleID"].ToString().Trim(), "ROOM TYPE", checkAccessType.InsertAction) == false)
        {
            Button1.Enabled = false;
        }
        if (!IsPostBack)
        {
            GridFill();
            Tab1Func();           
        }
    }
    private void GridFill()
    {
        GridView1.DataSource = theHelper.GetAllRoom(Session["CoCode"].ToString().Trim());
        GridView1.DataBind();
    }
    private void ResetAllFields()
    {
        txtRoomCatName.Text = "";
        txtPattText.Text = "";
        // lblError.Text = "";
        Button1.Text = "Submit";
        if (Objmain.checkaccss(Session["CoCode"].ToString(), Session["UserRoleID"].ToString().Trim(), "ROOM TYPE", checkAccessType.InsertAction) == false)
        {
            Button1.Enabled = false;
        }
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
            GridView1.Rows[index].BackColor = Color.GreenYellow;

            Label lblRoomCatID = (Label)GridView1.Rows[index].FindControl("RoomCategoryID");
            HiddenField1.Value = lblRoomCatID.Text;

            Label lblRoomCatName = (Label)GridView1.Rows[index].FindControl("RoomCategoryName");
            txtRoomCatName.Text = lblRoomCatName.Text;

            Label lblPattText = (Label)GridView1.Rows[index].FindControl("PatternText");
            txtPattText.Text = lblPattText.Text;
            Tab1Func();
            Button1.Text = "Update";
            if (Objmain.checkaccss(Session["CoCode"].ToString(), Session["UserRoleID"].ToString().Trim(), "ROOM TYPE", checkAccessType.UpdateAction) == false)
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
         Label lblRoomCatID = (Label)GridView1.Rows[e.RowIndex].FindControl("RoomCategoryID");
         theHelper.DeleteRoomType(Convert.ToInt32(lblRoomCatID.Text), Session["CoCode"].ToString().Trim());
        lblError.ForeColor = System.Drawing.Color.Green;
        lblError.Text = "Deleted Successfully";
        GridFill();
        ResetAllFields();
        
    }
    protected void Button1_Click(object sender, EventArgs e)
    {
        
        if (Button1.Text == "Submit")
        {
            theHelper.InsertRoomType(txtRoomCatName.Text, txtPattText.Text, Session["userName"].ToString(), dt, Session["CoCode"].ToString().Trim());
            ScriptManager.RegisterStartupScript(this, this.GetType(), "msg", "alert('Inserted  Successfully !');", true);
        }
        else
        {
            theHelper.UpdateRoomType(HiddenField1.Value, txtRoomCatName.Text, txtPattText.Text, dt, Session["CoCode"].ToString().Trim());
            ScriptManager.RegisterStartupScript(this, this.GetType(), "msg", "alert('Updated  Successfully !');", true);
        }

        GridView1.DataSource = theHelper.GetAllRoom(Session["CoCode"].ToString().Trim());
        GridView1.DataBind();
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
    public static List<string> Searchroomtype(string prefixText, int count)
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
                cmd.CommandText = "select distinct RoomCategoryName as Name from IPD_RoomType where compcode=@Compcode and RoomCategoryName like @SearchText +'%'";
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
            if (Objmain.checkaccss(Session["CoCode"].ToString(), Session["UserRoleID"].ToString().Trim(), "ROOM TYPE", checkAccessType.DeleteAction) == false)
            {
                coldel.Visible = false;
                e.Row.Cells[4].Visible = false;
            }

        }
    }
}