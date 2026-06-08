using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Linq;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Data.SqlClient;
using System.Drawing;
using EncryptionDecryption;
using System.Globalization;
using Enc;


public partial class Master_UserCreation : System.Web.UI.Page
{
    Main Objmain = new Main(ConfigurationSettings.AppSettings["ConnectionString"].ToString());
    HelperUserCreation theHelper = new HelperUserCreation(ConfigurationManager.AppSettings["ConnectionString"].ToString());
    protected void Page_Load(object sender, EventArgs e)
    {
        Page.Title = "User Creation";
        if (Session["userName"] == null)
        {
            Response.Redirect("../LoginPage.aspx");
        }
        if (Objmain.checkaccss(Session["CoCode"].ToString(), Session["UserRoleID"].ToString().Trim(), "USER CREATION", checkAccessType.ViewAction) == false)
        {
            Response.Redirect("../AccessDenied.aspx");
        }

        if (Objmain.checkaccss(Session["CoCode"].ToString(), Session["UserRoleID"].ToString().Trim(), "USER CREATION", checkAccessType.InsertAction) == false)
        {
            Button1.Enabled = false;
        }
        if (Objmain.checkaccss(Session["CoCode"].ToString(), Session["UserRoleID"].ToString().Trim(), "USER CREATION", checkAccessType.DeleteAction) == false)
        {
            GridView1.Columns[10].Visible = false;
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

        GridView1.DataSource = theHelper.GetAllUser(Session["CoCode"].ToString());
        GridView1.DataBind();

    }
    private void ResetAllFields()
    {
         txtUserId.Text = "";
        txtUserName.Text = "";
        DropDownList1.SelectedIndex = -1;
        txtPass.Text = "";
        txtconfirmPass.Text = "";
        txtphn13.Text = "";
        txtphn23.Text = "";
        txtemail.Text = "";
        Calendar1.Text = "";
        Button1.Text = "Submit";
        lblError.Text = "";
        chkAdmin.Checked = false;
     
    }
    private void DropDownFill()
    {
        DropDownList1.Items.Clear();
        this.DropDownList1.DataSource = theHelper.DropdownID();
        this.DropDownList1.DataTextField = "UserRoleName";
        this.DropDownList1.DataValueField = "UserRoleID";
        this.DropDownList1.DataBind();
        this.DropDownList1.Items.Insert(0, new ListItem("--Select--", "0"));
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

            Label lbluserID = (Label)GridView1.Rows[index].FindControl("UserID");
            txtUserId.Text = lbluserID.Text;
            HiddenField1.Value = lbluserID.Text;

            Label lbluserName = (Label)GridView1.Rows[index].FindControl("UserName");
            txtUserName.Text = lbluserName.Text;

            theHelper = new HelperUserCreation(ConfigurationManager.AppSettings["ConnectionString"].ToString());
            DropDownFill();
            Label lbluserrole = (Label)GridView1.Rows[index].FindControl("UserRoleID");
             DropDownList1.SelectedValue = lbluserrole.Text;

            Label lblpass = (Label)GridView1.Rows[index].FindControl("Password");
            txtPass.Text = lblpass.Text;

            Label lblphn_1 = (Label)GridView1.Rows[index].FindControl("PhoneNo_1");
            string[] Contracts = lblphn_1.Text.Split(' ');

            txtphn11.Text = "+91";
            if (Contracts.Length > 1)
            txtphn13.Text = Contracts[1];

            Label lblphn_2 = (Label)GridView1.Rows[index].FindControl("PhoneNo_2");
            string[] Contracts1 = lblphn_2.Text.Split(' ');

            txtphn21.Text = "+91";
            if(Contracts1.Length>1)
            txtphn23.Text = Contracts1[1];

            Label lblemailID = (Label)GridView1.Rows[index].FindControl("EmailId");
            txtemail.Text = lblemailID.Text;

            Label AdminFlag = (Label)GridView1.Rows[index].FindControl("AdminFlag");
            if (AdminFlag.Text == "1")
            {
                chkAdmin.Checked = true;
            }
            else
            {
                chkAdmin.Checked = false;
            }

            Label h = (Label)GridView1.Rows[index].FindControl("ExpiryDate");
            if (h.Text.Trim() != "")
                Calendar1.Text = h.Text.ToString().Substring(0, 10);
            else Calendar1.Text = "";
            Tab1Func();
            Button1.Text = "Update";

            if (Objmain.checkaccss(Session["CoCode"].ToString(), Session["UserRoleID"].ToString().Trim(), "USER CREATION", checkAccessType.UpdateAction) == false)
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
        Label a = (Label)GridView1.Rows[e.RowIndex].FindControl("UserID");
        theHelper.DeleteUserCreation(a.Text, Session["CoCode"].ToString());
        lblError.ForeColor = System.Drawing.Color.Green;
        lblError.Text = "Deleted Successfully";
        GridFill();
        ResetAllFields();
    }

    

    protected void Button1_Click1(object sender, EventArgs e)
    {

        string reformattedDate;

        System.Globalization.DateTimeFormatInfo dtf = new DateTimeFormatInfo();
        if (Calendar1.Text != "")
        {
            DateTime testdate = DateTime.ParseExact(Calendar1.Text, "dd/MM/yyyy", dtf);
            reformattedDate = testdate.ToString();
        }
        else
        {
            reformattedDate = "";
        }
        string PhoneNo_2 = "";
        string UserID = txtUserId.Text.Trim();
        string UserName = txtUserName.Text;
        string UserRoleID = DropDownList1.SelectedValue;
        string adminflag = "";
        //string password = EncryptionDecryption.CryptorEngine.EncryptData(txtPass.Text);
        string password = Enc.MAIN.SCrypt(txtPass.Text, true);
        //string password = txtPass.Text;
        string PhoneNo_1 = txtphn11.Text + " " + txtphn13.Text;
        if (txtphn23.Text != "")
            PhoneNo_2 = txtphn21.Text + " " + txtphn23.Text;
        else
            PhoneNo_2 = "";
        string EmailId = txtemail.Text;
        theHelper = new HelperUserCreation(ConfigurationManager.AppSettings["ConnectionString"].ToString());


        if (chkAdmin.Checked == true)
        {
            adminflag = "1";
        }
        else
        {
            adminflag = "0";
        }

        if (txtPass.Text.Trim() == txtconfirmPass.Text.Trim())
        {

            if (Button1.Text == "Submit")
            {
                if (!theHelper.CheckUser(UserID))
                {
                    theHelper.InsertUserCreation(UserID, UserName, UserRoleID, password, PhoneNo_1, PhoneNo_2, EmailId, Calendar1.Text, Session["userName"].ToString(), Session["CoCode"].ToString(), adminflag);
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "msg", "alert('Inserted Successfully !');", true);
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "msg", "alert('User Name Already Exist !');", true);
                }
            }
            else
            {
                theHelper.UpdateUserCreation(UserID, UserName, UserRoleID, password, PhoneNo_1, PhoneNo_2, EmailId, Calendar1.Text, Session["CoCode"].ToString(), adminflag);
                ScriptManager.RegisterStartupScript(this, this.GetType(), "msg", "alert('Updated Successfully !');", true);
                Button1.Text = "Submit";
            }

            ResetAllFields();
        }
        else
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "msg", "alert('Password And Confirm Password does not Matched !');", true);
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
