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
 
public partial class Master_MenuDetails : System.Web.UI.Page
{
    Main Objmain = new Main(ConfigurationSettings.AppSettings["ConnectionString"].ToString());
    MenuDetails theHelper = new MenuDetails(ConfigurationSettings.AppSettings["ConnectionString"].ToString());
    protected void Page_Load(object sender, EventArgs e)
    {
        Page.Title = "Menu Details";
        if (Session["userName"].ToString() != "SUPERVISOR")
        {
            if (Session["userName"] == null)
            {
                Response.Redirect("../LoginPage.aspx");
            }
            if (Objmain.checkaccss(Session["CoCode"].ToString(), Session["UserRoleID"].ToString().Trim(), "MENU DETAILS", checkAccessType.ViewAction) == false)
            {
                Response.Redirect("../AccessDenied.aspx");
            }

            if (Objmain.checkaccss(Session["CoCode"].ToString(), Session["UserRoleID"].ToString().Trim(), "MENU DETAILS", checkAccessType.InsertAction) == false)
            {
                Button1.Enabled = false;
            }
            if (Objmain.checkaccss(Session["CoCode"].ToString(), Session["UserRoleID"].ToString().Trim(), "MENU DETAILS", checkAccessType.DeleteAction) == false)
            {
                GridView1.Columns[8].Visible = false;
            }
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
        GridView1.DataSource = theHelper.GetAllMenu(Session["CoCode"].ToString(),ddlModuleSearch.SelectedValue, ddlSubModuleSearch.SelectedValue);
        GridView1.DataBind();
    }
    private void ResetAllFields()
    {
          txtMenuName.Text = "";
        DropDownList1.SelectedIndex = 0;
        DropDownList2.SelectedIndex = 0;
        txtURL.Text = "";
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
            GridView1.Rows[index].BackColor = Color.GreenYellow;

            Label lblMeniID = (Label)GridView1.Rows[index].FindControl("MenuID");
             HiddenField1.Value = lblMeniID.Text;

            Label lblMenuName = (Label)GridView1.Rows[index].FindControl("MenuName");
            txtMenuName.Text = lblMenuName.Text;



              DropDownFill();
            Label lblModuleID = (Label)GridView1.Rows[index].FindControl("ModuleID");
              DropDownList1.SelectedValue = lblModuleID.Text;

              DropDownFill2();
            Label lblSubModuleID = (Label)GridView1.Rows[index].FindControl("SubModuleID");
             DropDownList2.SelectedValue = lblSubModuleID.Text;

            Label lblURL = (Label)GridView1.Rows[index].FindControl("URL");
            txtURL.Text = lblURL.Text;
            Tab1Func();
            Button1.Text = "Update";

            //if (Objmain.checkaccss(Session["CoCode"].ToString(), Session["UserRoleID"].ToString().Trim(), "MENU DETAILS", checkAccessType.UpdateAction) == false)
            //{
            //    Button1.Enabled = false;
            //}
            //else
            //{
            //    Button1.Enabled = true;
            //}
        }
    }
    private void DropDownFill()
    {
        DropDownList1.Items.Clear();
        this.DropDownList1.DataSource = theHelper.DropdownID1();
        this.DropDownList1.DataTextField = "ModuleName";
        this.DropDownList1.DataValueField = "ModuleID";
        this.DropDownList1.DataBind();
        this.DropDownList1.Items.Insert(0, new ListItem("--Select--", "0"));

        DropDownList2.Items.Clear();
        this.DropDownList2.DataSource = theHelper.DropdownID2(0);
        this.DropDownList2.DataTextField = "SubModuleName";
        this.DropDownList2.DataValueField = "SubModuleID";
        this.DropDownList2.DataBind();
        this.DropDownList2.Items.Insert(0, new ListItem("--Select--", "0"));

        ddlModuleSearch.Items.Clear();
        this.ddlModuleSearch.DataSource = theHelper.DropdownID1();
        this.ddlModuleSearch.DataTextField = "ModuleName";
        this.ddlModuleSearch.DataValueField = "ModuleID";
        this.ddlModuleSearch.DataBind();
        this.ddlModuleSearch.Items.Insert(0, new ListItem("--Select--", "0"));

        ddlSubModuleSearch.Items.Clear();
        this.ddlSubModuleSearch.DataSource = theHelper.DropdownID2(0);
        this.ddlSubModuleSearch.DataTextField = "SubModuleName";
        this.ddlSubModuleSearch.DataValueField = "SubModuleID";
        this.ddlSubModuleSearch.DataBind();
        this.ddlSubModuleSearch.Items.Insert(0, new ListItem("--Select--", "0"));
    }
    private void DropDownFill2()
    {
        DropDownList2.Items.Clear();
        this.DropDownList2.DataSource = theHelper.DropdownID2();
        this.DropDownList2.DataTextField = "SubModuleName";
        this.DropDownList2.DataValueField = "SubModuleID";
        this.DropDownList2.DataBind();
        this.DropDownList2.Items.Insert(0, new ListItem("--Select--", "0"));
    }

    protected void GridView1_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
          Label lblMenuID = (Label)GridView1.Rows[e.RowIndex].FindControl("MenuID");
        theHelper.DeleteMenuDetails(Convert.ToInt32(lblMenuID.Text));
        lblError.ForeColor = System.Drawing.Color.Green;
        lblError.Text = "Deleted Successfully";
        GridFill();
        ResetAllFields();
         
    }


    protected void Button1_Click(object sender, EventArgs e)
    {
            if (Button1.Text == "Submit")
        {

            theHelper.InsertMenuDtails(txtMenuName.Text.ToUpper(), DropDownList1.SelectedValue, DropDownList2.SelectedValue, txtURL.Text, Session["userName"].ToString(), DateTime.Now.ToString("MM/dd/yyyy"));
            ScriptManager.RegisterStartupScript(this, this.GetType(), "msg", "alert('Inserted successfully !');", true);
          
        }
        else
        {
            theHelper.UpdateMenuDetails(HiddenField1.Value, txtMenuName.Text.ToUpper(), DropDownList1.SelectedValue, DropDownList2.SelectedValue, txtURL.Text.Trim());
            ScriptManager.RegisterStartupScript(this, this.GetType(), "msg", "alert('Updated successfully !');", true);
            Button1.Text = "Submit";
        }

        ResetAllFields();
        GridFill();
       
    }
    protected void DropDownList1_SelectedIndexChanged(object sender, EventArgs e)
    {
        string moduleID = DropDownList1.SelectedValue;
          DropDownList2.DataSource = theHelper.DropdownID2(int.Parse( moduleID));
        DropDownList2.DataBind();
        this.DropDownList2.Items.Insert(0, new ListItem("--Select--", "0"));
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
    protected void ddlModuleSearch_SelectedIndexChanged(object sender, EventArgs e)
    {
        string moduleID = ddlModuleSearch.SelectedValue;
        ddlSubModuleSearch.DataSource = theHelper.DropdownID2(int.Parse(moduleID));
        ddlSubModuleSearch.DataBind();
        this.ddlSubModuleSearch.Items.Insert(0, new ListItem("--Select--", "0"));

        GridFill();
    }
    protected void ddlSubModuleSearch_SelectedIndexChanged(object sender, EventArgs e)
    {
        GridFill();
    }
}
