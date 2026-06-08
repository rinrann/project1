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
using Enc;

public partial class Master_ChangePassword : System.Web.UI.Page
{
    Main Objmain = new Main(ConfigurationSettings.AppSettings["ConnectionString"].ToString());
    HelperUserCreation theHelper = new HelperUserCreation(ConfigurationManager.AppSettings["ConnectionString"].ToString());
    protected void Page_Load(object sender, EventArgs e)
    {

        Page.Title = "Change Password";

        if (Session["userName"] == null)
        {
            Response.Redirect("../LoginPage.aspx");
        }
        if (Objmain.checkaccss(Session["CoCode"].ToString(), Session["UserRoleID"].ToString().Trim(), "CHANGE PASSWORD", checkAccessType.ViewAction) == false)
        {
            Response.Redirect("../AccessDenied.aspx");
        }
        if (Objmain.checkaccss(Session["CoCode"].ToString(), Session["UserRoleID"].ToString().Trim(), "CHANGE PASSWORD", checkAccessType.UpdateAction) == false)
        {
            Button1.Enabled = false;
        } 
        if (!IsPostBack)
        {
            txtUserId.Text = Session["userId"].ToString();
            txtUserName.Text = Session["userName"].ToString();
            txtCrntPass.Text = "";
        }
    }
    protected void btnsubmit_Click(object sender, EventArgs e)
    {

        //string Pass = CryptorEngine.Encrypt(txtCrntPass.Text,true);
        string Pass = Enc.MAIN.SCrypt(txtCrntPass.Text, true);
        if (txtNewPass.Text.Trim() == txtConfirmPass.Text.Trim())
        {
            string conString = ConfigurationManager.AppSettings["ConnectionString"].ToString();
            SqlConnection theConnection;
 
            theConnection = new SqlConnection();
            theConnection.ConnectionString = conString;
            theConnection.Open();

            string str = "SELECT * FROM GN_UserDetails where UserId ='" + txtUserName.Text + "' And Password='" + Pass + "'";
            
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter();
            da.SelectCommand = new SqlCommand(str, theConnection);
            da.Fill(dt);

            if (dt.Rows.Count > 0)
            {
                HelperUserCreation objname = new HelperUserCreation(txtUserName.Text);
                //objname.ChangePassword(txtUserName.Text, CryptorEngine.Encrypt(txtNewPass.Text, true));
                objname.ChangePassword(txtUserId.Text, Enc.MAIN.SCrypt(txtNewPass.Text, true));
                ScriptManager.RegisterStartupScript(this, this.GetType(), "msg", "alert('Password has been changed successfully !');", true);
           

            }
            else
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "msg", "alert('Invalid existing password !');", true);
      
            }
        }
        else
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "msg", "alert('Please enter valid password !');", true);
 
        }
    }
     protected void Button2Click(object sender, EventArgs e)
    {
        ResetAll();
    }
    private void ResetAll()
    {
        txtNewPass.Text = "";
        txtCrntPass.Text = "";
        txtConfirmPass.Text = "";
    }
}
