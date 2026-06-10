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

 

public partial class Master_ServiceProvider : System.Web.UI.Page
{
    ServiceProvider theHelper = new ServiceProvider(ConfigurationSettings.AppSettings["ConnectionString"].ToString());
    protected void Page_Load(object sender, EventArgs e)
    {
        Page.Title = "Service Provider";
        if (Session["userName"] == null)
        {
            Response.Redirect("../LoginPage.aspx");
        }
        if (!IsPostBack)
        {
            Tab1Func();
            GridFill();
            txtServiceProviderId.Text = theHelper.GetServiceID().ToString();
        }
    }
    private void GridFill()
    {
         GridView1.DataSource = theHelper.GetAllService();
        GridView1.DataBind();
    }
    private void ResetAllFields()
    {
        txtServiceProviderId.Text = "";
        txtServiceProviderName.Text = "";
        txtAddress.Text = "";
        txtphn13.Text = "";
        txtphn23.Text = "";
        txtemail.Text = "";
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

            Label lblServiceProviderID = (Label)GridView1.Rows[index].FindControl("ServiceProviderID");
            txtServiceProviderId.Text = lblServiceProviderID.Text;
            HiddenField1.Value = lblServiceProviderID.Text;

            Label lblServiceProviderName = (Label)GridView1.Rows[index].FindControl("ServiceProviderName");
            txtServiceProviderName.Text = lblServiceProviderName.Text;

            Label lblAddress = (Label)GridView1.Rows[index].FindControl("Address");
            txtAddress.Text = lblAddress.Text;

            Label lblphn_1 = (Label)GridView1.Rows[index].FindControl("PhoneNo_1");
            string[] Contracts = lblphn_1.Text.Split(' ');

            txtphn11.Text = "+91";
            if(Contracts.Length>1)
            txtphn13.Text = Contracts[1];

            Label lblphn_2 = (Label)GridView1.Rows[index].FindControl("PhoneNo_2");
            string[] Contracts1 = lblphn_2.Text.Split(' ');

            txtphn21.Text = "+91";
            if (Contracts1.Length > 1)
            txtphn23.Text = Contracts1[1];

            Label lblemail = (Label)GridView1.Rows[index].FindControl("Email");
            txtemail.Text = lblemail.Text;
            Tab1Func();
            Button1.Text = "Update";
        }
    }
    protected void GridView1_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
         Label lblServiceProviderID = (Label)GridView1.Rows[e.RowIndex].FindControl("ServiceProviderID");
        theHelper.DeleteServiceProvider(Convert.ToInt32(lblServiceProviderID.Text));
        lblError.ForeColor = System.Drawing.Color.Green;
        lblError.Text = "Deleted Successfully";
        GridFill();
        ResetAllFields();
        txtServiceProviderId.Text = theHelper.GetServiceID().ToString();
    }
    protected void Button1_Click(object sender, EventArgs e)
    {
        //string ServiceProviderID = txtServiceProviderId.Text;
        string ServiceProviderName = txtServiceProviderName.Text;
        string Address = txtAddress.Text;
        string PhoneNo_1 = txtphn11.Text + " " + txtphn13.Text;
        string PhoneNo_2;
        if (txtphn23.Text != "")
        {
            PhoneNo_2 = txtphn21.Text + " " + txtphn23.Text;
        }
        else
            PhoneNo_2 = "";
        string Email = txtemail.Text;

        
             if (Button1.Text == "Submit")
            {
                if (theHelper.InsertServiceProvider(ServiceProviderName.ToUpper(), Address.ToUpper(), PhoneNo_1, PhoneNo_2, Email, Session["userName"].ToString()) == true)
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "msg", "alert('Inserted  Successfully !');", true);
                    ResetAllFields();
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "msg", "alert('Error in Inserted Data  Successfully !');", true);
                }
            }
            else
            {
              if(  theHelper.UpdateServiceProvider(HiddenField1.Value, ServiceProviderName.ToUpper(), Address.ToUpper(), PhoneNo_1, PhoneNo_2, Email)==true)
              {
                  ScriptManager.RegisterStartupScript(this, this.GetType(), "msg", "alert('Updated  Successfully !');", true);
                    ResetAllFields();
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "msg", "alert('Error in Updated Data !');", true);
                }
            }

             GridFill();
           
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
}