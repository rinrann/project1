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
 
public partial class Master_ServiceDetails : System.Web.UI.Page
{
    ServiceDetails theHelper = new ServiceDetails(ConfigurationSettings.AppSettings["ConnectionString"].ToString());
    protected void Page_Load(object sender, EventArgs e)
    {
        Page.Title = "Service Details";
        if (Session["userName"] == null)
        {
            Response.Redirect("../LoginPage.aspx");
        }
        if (!IsPostBack)
        {
            GridFill();
            Tab1Func();
            DropDownFill();
            txtServiceId.Text = theHelper.GetServiceID().ToString();
        }
    }
    private void GridFill()
    {
          GridView1.DataSource = theHelper.GetAllService();
        GridView1.DataBind();
    }
    private void ResetAllFields()
    {
        
        txtServiceId.Text = "";
        txtServiceName.Text = "";
        DropDownList1.SelectedIndex = -1;
        DropDownList2.SelectedIndex = -1;
        txtcharges.Text = ""; txtQuantity.Text = "";
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

            Label lblServiceID = (Label)GridView1.Rows[index].FindControl("ServiceID");
            txtServiceId.Text = lblServiceID.Text;
            HiddenField1.Value = lblServiceID.Text;

            Label lblServiceName = (Label)GridView1.Rows[index].FindControl("ServiceName");
            txtServiceName.Text = lblServiceName.Text;


             DropDownFill();
            Label lblServiceCategoryID = (Label)GridView1.Rows[index].FindControl("ServiceCategoryID");
            DropDownList1.SelectedValue = lblServiceCategoryID.Text;

            Label lblServiceProviderID = (Label)GridView1.Rows[index].FindControl("ServiceProviderID");
            DropDownList2.SelectedValue = lblServiceProviderID.Text;

            Label lblCharges = (Label)GridView1.Rows[index].FindControl("Charges");
            txtcharges.Text = lblCharges.Text;

            Label Quantity = (Label)GridView1.Rows[index].FindControl("Quantity");
            txtQuantity.Text = Quantity.Text;

            Tab1Func();
            Button1.Text = "Update";
        }
    }
    private void DropDownFill()
    {
        DropDownList1.Items.Clear();
        this.DropDownList1.DataSource = theHelper.DropdownID1();
        this.DropDownList1.DataTextField = "ServiceCategoryName";
        this.DropDownList1.DataValueField = "ServiceCategoryID";
        this.DropDownList1.DataBind();
        this.DropDownList1.Items.Insert(0, new ListItem("--Select--", "0"));

        DropDownList2.Items.Clear();
        this.DropDownList2.DataSource = theHelper.DropdownID2();
        this.DropDownList2.DataTextField = "ServiceProviderName";
        this.DropDownList2.DataValueField = "ServiceProviderID";
        this.DropDownList2.DataBind();
        this.DropDownList2.Items.Insert(0, new ListItem("--Select--", "0"));

    }
    protected void GridView1_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
          Label lblServiceID = (Label)GridView1.Rows[e.RowIndex].FindControl("ServiceID");
        theHelper.DeleteServiceDetails(Convert.ToInt32(lblServiceID.Text));
        ScriptManager.RegisterStartupScript(this, this.GetType(), "msg", "alert('Deleted  Successfully !');", true);
        GridFill();
        ResetAllFields();
        txtServiceId.Text = theHelper.GetServiceID().ToString();
    }
    protected void Button1_Click(object sender, EventArgs e)
    {
        
        if (Button1.Text == "Submit")
        {

            theHelper.InsertServiceDetails(txtServiceName.Text, DropDownList1.SelectedValue, DropDownList2.SelectedValue,txtQuantity.Text, txtcharges.Text, Session["userName"].ToString());
            ScriptManager.RegisterStartupScript(this, this.GetType(), "msg", "alert('Inserted  Successfully !');", true);
        }
        else
        {
            theHelper.UpdateServiceDetails(HiddenField1.Value, txtServiceName.Text, DropDownList1.SelectedValue, DropDownList2.SelectedValue, txtQuantity.Text, txtcharges.Text);
            ScriptManager.RegisterStartupScript(this, this.GetType(), "msg", "alert('Updated  Successfully !');", true);
            Button1.Text = "Submit";
        }

        ResetAllFields();
        GridFill();
        txtServiceId.Text = theHelper.GetServiceID().ToString();
    }
    protected void Button2_Click(object sender, EventArgs e)
    {
        ResetAllFields();
        Page.Title = "Service Details";
        if (Session["userName"] == null)
        {
            Response.Redirect("/HospitalMgnt/LoginPage.aspx");
        }
        GridFill();
        DropDownFill();
        txtServiceId.Text = theHelper.GetServiceID().ToString();
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
}