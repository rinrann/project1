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

public class HelperRoomMaster
{
  
}

public partial class Master_RoomMaster : System.Web.UI.Page
{
    Main Objmain = new Main(ConfigurationSettings.AppSettings["ConnectionString"].ToString());
    RoomMaster theHelper = new RoomMaster(ConfigurationSettings.AppSettings["ConnectionString"].ToString());
    string dt = DateTime.Now.ToString("MM/dd/yyyy");
    protected void Page_Load(object sender, EventArgs e)
    {
        Page.Title = "Room Master";
        if (Session["userName"] == null)
        {
            Response.Redirect("../LoginPage.aspx");
        }
        if (Objmain.checkaccss(Session["CoCode"].ToString(), Session["UserRoleID"].ToString().Trim(), "ROOM MASTER", checkAccessType.ViewAction) == false)
        {
            Response.Redirect("../AccessDenied.aspx");
        }

        if (Objmain.checkaccss(Session["CoCode"].ToString(), Session["UserRoleID"].ToString().Trim(), "ROOM MASTER", checkAccessType.InsertAction) == false)
        {
            Button1.Enabled = false;
        }
        if (!IsPostBack)
        {
            Tab1Func();
            GridFill();
            DropDownFill();
           
        }
    }
    private void GridFill()
    {
        GridView1.DataSource = theHelper.GetAllRoom(Session["CoCode"].ToString().Trim());
        GridView1.DataBind();
    }
    private void ResetAllFields()
    {
           
        txtRoomName.Text = "";
        DropDownList1.SelectedIndex = 0;
        DropDownList2.SelectedIndex = 0;
        DropDownList3.SelectedIndex = -1;
        txtPatText.Text = "";
        Button1.Text = "Submit";
        if (Objmain.checkaccss(Session["CoCode"].ToString(), Session["UserRoleID"].ToString().Trim(), "ROOM MASTER", checkAccessType.InsertAction) == false)
        {
            Button1.Enabled = false;
        }
    
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
            if (ddl.Items[i].Text.Trim() == Value.Trim())
                return i;
        }
        return -1;
    }



    protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "Select")
        {
            lblError.Text = "";
            int index = Convert.ToInt32(e.CommandArgument);
            Label lblRoomID = (Label)GridView1.Rows[index].FindControl("RoomID");
            HiddenField1.Value = lblRoomID.Text;

            Label lblRoomName = (Label)GridView1.Rows[index].FindControl("RoomName");
            txtRoomName.Text = lblRoomName.Text;

            DropDownFill();
            Label FloorName = (Label)GridView1.Rows[index].FindControl("FloorName");
            DropDownList1.SelectedIndex = SearchIndex(FloorName.Text, DropDownList1);

            Label WingsName = (Label)GridView1.Rows[index].FindControl("WingsName");
             DropDownList2.SelectedIndex = SearchIndex(WingsName.Text, DropDownList2);

             Label RoomCategoryName = (Label)GridView1.Rows[index].FindControl("RoomCategoryName");
             DropDownList3.SelectedIndex = SearchIndex(RoomCategoryName.Text, DropDownList3);

            Label lblPatText = (Label)GridView1.Rows[index].FindControl("PatternText");
            txtPatText.Text = lblPatText.Text;
            Tab1Func();
            Button1.Text = "Update";
            if (Objmain.checkaccss(Session["CoCode"].ToString(), Session["UserRoleID"].ToString().Trim(), "ROOM MASTER", checkAccessType.UpdateAction) == false)
            {
                Button1.Enabled = false;
            }
            else
            {
                Button1.Enabled = true;
            }
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
    }

    protected void GridView1_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
         Label lblRoomID = (Label)GridView1.Rows[e.RowIndex].FindControl("RoomID");
         theHelper.DeleteRoomMaster(Convert.ToInt32(lblRoomID.Text), Session["CoCode"].ToString().Trim());
        lblError.ForeColor = System.Drawing.Color.Green;
        lblError.Text = "Deleted Successfully";
        GridFill();
        ResetAllFields();
       
    }
    protected void Button1_Click(object sender, EventArgs e)
    {
          if (Button1.Text == "Submit")
        {

            theHelper.InsertRoomMaster(txtRoomName.Text, DropDownList1.SelectedValue, DropDownList2.SelectedValue, DropDownList3.SelectedValue, txtPatText.Text, Session["userName"].ToString(), dt, Session["CoCode"].ToString().Trim());
            ScriptManager.RegisterStartupScript(this, this.GetType(), "msg", "alert('Inserted  Successfully !');", true);
        }
        else
        {
            theHelper.UpdateRoomMaster(HiddenField1.Value, txtRoomName.Text, DropDownList1.SelectedValue, DropDownList2.SelectedValue, DropDownList3.SelectedValue, txtPatText.Text, dt, Session["CoCode"].ToString().Trim());
            ScriptManager.RegisterStartupScript(this, this.GetType(), "msg", "alert('Updated  Successfully !');", true);
            Button1.Text = "Submit";
        }

        ResetAllFields();
        GridFill();
       
    }
    protected void Button2_Click(object sender, EventArgs e)
    {
        ResetAllFields();
    }
    protected void DropDownList1_SelectedIndexChanged(object sender, EventArgs e)
    {
        string FloorID = DropDownList1.SelectedValue;
        DropDownList2.DataSource = theHelper.DropdownID2(int.Parse(FloorID), Session["CoCode"].ToString().Trim());
        DropDownList2.DataBind();
        this.DropDownList2.Items.Insert(0, new ListItem("--Select--", "0"));
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
        using (SqlConnection conn = new SqlConnection())
        {
            conn.ConnectionString = ConfigurationSettings.AppSettings["ConnectionString"].ToString();
            using (SqlCommand cmd = new SqlCommand())
            {
                cmd.CommandText = "select distinct RoomName as Name from IPD_RoomMaster where RoomName like @SearchText +'%'";
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
            if (Objmain.checkaccss(Session["CoCode"].ToString(), Session["UserRoleID"].ToString().Trim(), "ROOM MASTER", checkAccessType.DeleteAction) == false)
            {
                coldel.Visible = false;
                e.Row.Cells[7].Visible = false;
            }

        }
    }
}