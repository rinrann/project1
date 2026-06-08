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


public partial class IPD_BedAllocationPopup : System.Web.UI.Page
{
    BedAllocationPopup thereg = new BedAllocationPopup(ConfigurationSettings.AppSettings["ConnectionString"].ToString());
    static string floor, wing;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["userName"] == null)
        {
            Response.Redirect("../LoginPage.aspx");
        }
        Page.Title = "Bed Allocation Popup";

        if (!IsPostBack)
        {
            DropDownFill();
            GridFill();
        }
    }

    public void DropDownFill()
    {
        this.DropDownList1.DataSource = thereg.DropdownFloor();
        this.DropDownList1.DataTextField = "FloorName";
        this.DropDownList1.DataValueField = "FloorID";
        this.DropDownList1.DataBind();
        this.DropDownList1.SelectedValue = "2";

        this.DropDownList2.DataSource = thereg.Dropdownwings(DropDownList1.SelectedValue);
        this.DropDownList2.DataTextField = "WingsName";
        this.DropDownList2.DataValueField = "WingsID";
        this.DropDownList2.DataBind();
        this.DropDownList2.Items.Insert(0, new ListItem("ALL", "0"));


        this.DropDownList3.Items.Insert(0, new ListItem("ALL", "0"));


        this.DropDownList4.DataSource = thereg.Dropdownroomtype();
        this.DropDownList4.DataTextField = "RoomCategoryName";
        this.DropDownList4.DataValueField = "RoomCategoryID";
        this.DropDownList4.DataBind();
        this.DropDownList4.Items.Insert(0, new ListItem("ALL", "0"));
    }
    public void GridFill()
    {
        GridView_popup.DataSource = thereg.GridPopup(DropDownList1.SelectedValue, DropDownList2.SelectedValue, DropDownList3.SelectedValue, DropDownList4.SelectedValue);
        GridView_popup.DataBind();
    }
    protected void Button2_Click(object sender, EventArgs e)
    {
        GridFill();
    }

  
 
    protected void Button3_Click(object sender, EventArgs e)
    {
        int count = 0;
        for (int i = GridView_popup.Rows.Count - 1; i >= 0; i--)
        {
            CheckBox chk = (CheckBox)GridView_popup.Rows[i].Cells[1].FindControl("CheckBox1");
            Label lblbedno = (Label)GridView_popup.Rows[i].Cells[2].FindControl("lblbedno");
         
            if (chk.Checked)
            {
                if (count == 0)
                {
                    HiddenField1.Value = lblbedno.Text;
                     count++;
                }
                else
                {
                    HiddenField1.Value = lblbedno.Text + "," + HiddenField1.Value;
                  }

            }


        }
        ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "MyFun1", "CloseDialog();", true);

    }

    protected void GridView_popup_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {

        GridView_popup.PageIndex = e.NewPageIndex;
        GridFill();
    }
    protected void GridView_popup_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        Label lblbednotext = (Label)GridView_popup.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("lblbednotext");
        Label lblbedno = (Label)GridView_popup.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("lblbedno");
        //Label lblwpattern = (Label)GridView_popup.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("lblwpattern");
        //Label lblcatpattern = (Label)GridView_popup.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("lblcatpattern");
        //Label lblrpattern = (Label)GridView_popup.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("lblrpattern");
        HiddenField1.Value = lblbednotext.Text;
        HiddenField2.Value = lblbedno.Text;
     
    }
    protected void DropDownList1_SelectedIndexChanged(object sender, EventArgs e)
    {
        floor = DropDownList1.SelectedValue;
        this.DropDownList2.DataSource = thereg.Dropdownwings(floor);
        this.DropDownList2.DataTextField = "WingsName";
        this.DropDownList2.DataValueField = "WingsID";
        this.DropDownList2.DataBind();
        this.DropDownList2.Items.Insert(0, new ListItem("ALL", "0"));

       
    }
    protected void DropDownList4_SelectedIndexChanged(object sender, EventArgs e)
    {
        this.DropDownList3.DataSource = thereg.Dropdownroom(DropDownList4.SelectedValue,floor,wing);
        this.DropDownList3.DataTextField = "RoomName";
        this.DropDownList3.DataValueField = "RoomID";
        this.DropDownList3.DataBind();
        this.DropDownList3.Items.Insert(0, new ListItem("ALL", "0"));
    }
    protected void DropDownList2_SelectedIndexChanged(object sender, EventArgs e)
    {
        wing = DropDownList2.SelectedValue;
    }
}