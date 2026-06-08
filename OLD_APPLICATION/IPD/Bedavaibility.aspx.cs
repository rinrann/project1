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
using System.Web.Services;
using System.Globalization;
 

public partial class IPD_Bedavaibility : System.Web.UI.Page
{
    Main Objmain = new Main(ConfigurationSettings.AppSettings["ConnectionString"].ToString());
    Bedavaibility thebedavail = new Bedavaibility(ConfigurationSettings.AppSettings["ConnectionString"].ToString());
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["userName"] == null)
        {
            Response.Redirect("../LoginPage.aspx");
        }
        if (Objmain.checkaccss(Session["CoCode"].ToString(), Session["UserRoleID"].ToString().Trim(), "BED AVAILABILITY", checkAccessType.ViewAction) == false)
        {
            Response.Redirect("../AccessDenied.aspx");
        }
        Page.Title = "Bed Avaibility";
        if (!IsPostBack)
        {
            DropDownFill();
            GridFill();
        }
    }
    protected void Button2_Click(object sender, EventArgs e)
    {
        GridFill();
    }
    private void GridFill()
    {
        GridView1.DataSource = thebedavail.GetBedAvailDtls(TextBox1.Text, TextBox2.Text, DropDownList1.SelectedValue, DropDownList2.SelectedValue, DropDownList3.SelectedValue, DropDownList4.SelectedValue, Session["CoCode"].ToString().Trim());
        GridView1.DataBind();
    }
    private void DropDownFill()
    {
        this.DropDownList1.DataSource = thebedavail.DropDownFloor(Session["CoCode"].ToString().Trim());
        this.DropDownList1.DataTextField = "FloorName";
        this.DropDownList1.DataValueField = "FloorID";
        this.DropDownList1.DataBind();
        this.DropDownList1.Items.Insert(0, new ListItem("ALL", "0"));



        this.DropDownList3.DataSource = thebedavail.DropDowncategory(Session["CoCode"].ToString().Trim());
        this.DropDownList3.DataTextField = "RoomCategoryName";
        this.DropDownList3.DataValueField = "RoomCategoryID";
        this.DropDownList3.DataBind();
        this.DropDownList3.Items.Insert(0, new ListItem("ALL", "0"));

        this.DropDownList2.Items.Insert(0, new ListItem("ALL", "0"));

        this.DropDownList4.Items.Insert(0, new ListItem("ALL", "0"));


    }
    protected void GridView1_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {

    }
    protected void DropDownList1_SelectedIndexChanged(object sender, EventArgs e)
    {
         DropDownList2.Items.Clear();
         this.DropDownList2.DataSource = thebedavail.DropDownWings(DropDownList1.SelectedValue, Session["CoCode"].ToString().Trim());
        this.DropDownList2.DataTextField = "WingsName";
        this.DropDownList2.DataValueField = "WingsID";
        this.DropDownList2.DataBind();
        this.DropDownList2.Items.Insert(0, new ListItem("ALL", "0"));

    }
    protected void DropDownList3_SelectedIndexChanged(object sender, EventArgs e)
    {
          DropDownList4.Items.Clear();
          this.DropDownList4.DataSource = thebedavail.DropDownroom(DropDownList1.SelectedValue, DropDownList2.SelectedValue, DropDownList3.SelectedValue, Session["CoCode"].ToString().Trim());
        this.DropDownList4.DataTextField = "RoomName";
        this.DropDownList4.DataValueField = "RoomID";
        this.DropDownList4.DataBind();
        this.DropDownList4.Items.Insert(0, new ListItem("ALL", "0"));
    }
    protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "bedavail")
        {

            string id = (e.CommandArgument).ToString();
            Session.Add("BedNo", id);
            Response.Redirect("../IPD/PatientAdmission.aspx");
        }
    }
    protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            if (e.Row.RowIndex == 0)
                e.Row.Style.Add("height", "50px");


            Label lblSelno = (Label)e.Row.FindControl("lblSlno");
            lblSelno.Text = (e.Row.RowIndex + 1).ToString();
        }
    }
}