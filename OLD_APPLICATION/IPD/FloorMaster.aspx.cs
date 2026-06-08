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
 
public partial class Master_FloorMaster : System.Web.UI.Page
{
    Main Objmain = new Main(ConfigurationSettings.AppSettings["ConnectionString"].ToString());
    FloorMaster theHelper = new FloorMaster(ConfigurationSettings.AppSettings["ConnectionString"].ToString());
    protected void Page_Load(object sender, EventArgs e)
    {
        Page.Title = "Floor Master";
        if (Session["userName"] == null)
        {
            Response.Redirect("../LoginPage.aspx");
        }
        if (Objmain.checkaccss(Session["CoCode"].ToString(), Session["UserRoleID"].ToString().Trim(), "FLOOR MASTER", checkAccessType.ViewAction) == false)
        {
            Response.Redirect("../AccessDenied.aspx");
        }

        if (Objmain.checkaccss(Session["CoCode"].ToString(), Session["UserRoleID"].ToString().Trim(), "FLOOR MASTER", checkAccessType.InsertAction) == false)
        {
            Button1.Enabled = false;
        }
        if (Objmain.checkaccss(Session["CoCode"].ToString(), Session["UserRoleID"].ToString().Trim(), "FLOOR MASTER", checkAccessType.DeleteAction) == false)
        {
            GridView1.Columns[4].Visible = false;
        }
        if (!IsPostBack)
        {
            GridFill();
            Tab1Func();
        }
    }
    private void GridFill()
    {
        GridView1.DataSource = theHelper.GetAllFloor(Session["CoCode"].ToString().Trim());
        GridView1.DataBind();
    }
    private void ResetAllFields()
    {
     
        txtFloorName.Text = "";
        txtPattText.Text = "";
        Button1.Text = "Submit";
        if (Objmain.checkaccss(Session["CoCode"].ToString(), Session["UserRoleID"].ToString().Trim(), "FLOOR MASTER", checkAccessType.InsertAction) == false)
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
           
            Label lblFloorID = (Label)GridView1.Rows[index].FindControl("FloorID");
            HiddenField1.Value = lblFloorID.Text;

            Label lblFloorName = (Label)GridView1.Rows[index].FindControl("FloorName");
            txtFloorName.Text = lblFloorName.Text;

            Label lblPattText = (Label)GridView1.Rows[index].FindControl("PatternText");
            txtPattText.Text = lblPattText.Text;
            Tab1Func();
            Button1.Text = "Update";
            if (Objmain.checkaccss(Session["CoCode"].ToString(), Session["UserRoleID"].ToString().Trim(), "FLOOR MASTER", checkAccessType.UpdateAction) == false)
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
         Label lblFloorID = (Label)GridView1.Rows[e.RowIndex].FindControl("FloorID");
         theHelper.DeleteFloorMaster(Convert.ToInt32(lblFloorID.Text), Session["CoCode"].ToString());
        lblError.ForeColor = System.Drawing.Color.Green;
        lblError.Text = "Deleted Successfully";
        GridFill();
        ResetAllFields();
     //   txtFloorId.Text = theHelper.GetFloorID().ToString();
    }
    protected void Button1_Click1(object sender, EventArgs e)
    {
       
        if (Button1.Text == "Submit")
        {
            theHelper.InsertFloorMaster(txtFloorName.Text, txtPattText.Text, Session["userName"].ToString(), Session["CoCode"].ToString());
            ScriptManager.RegisterStartupScript(this, this.GetType(), "msg", "alert('Inserted Successfully !');", true);
        }
        else
        {
            theHelper.UpdateFloorMaster(HiddenField1.Value, txtFloorName.Text, txtPattText.Text, Session["CoCode"].ToString());
            ScriptManager.RegisterStartupScript(this, this.GetType(), "msg", "alert('Updated Successfully !');", true);
            Button1.Text = "Submit";
        }

        GridView1.DataSource = theHelper.GetAllFloor(Session["CoCode"].ToString());
        GridView1.DataBind();
        ResetAllFields();
      
    }

    public string generateID()
    {
        return Guid.NewGuid().ToString("N");
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
    protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            if (Objmain.checkaccss(Session["CoCode"].ToString(), Session["UserRoleID"].ToString().Trim(), "FLOOR MASTER", checkAccessType.DeleteAction) == false)
            {
                e.Row.Cells[4].Visible = false;
            }

        }
    }
}