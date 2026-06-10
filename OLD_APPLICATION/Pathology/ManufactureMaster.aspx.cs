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
 
public partial class Pathology_ManufactureMaster : System.Web.UI.Page
{
    Main Objmain = new Main(ConfigurationSettings.AppSettings["ConnectionString"].ToString());
    PH_ManufactureMaster thepatho = new PH_ManufactureMaster(ConfigurationSettings.AppSettings["ConnectionString"].ToString());
    protected void Page_Load(object sender, EventArgs e)
    {
        Page.Title = "Manufacturer Master";
        if (Session["userName"] == null)
        {
            Response.Redirect("../LoginPage.aspx");
        }
        if (Objmain.checkaccss(Session["CoCode"].ToString(), Session["UserRoleID"].ToString().Trim(), "MANUFACTURER MASTER", checkAccessType.ViewAction) == false)
        {
            Response.Redirect("../AccessDenied.aspx");
        }

        if (Objmain.checkaccss(Session["CoCode"].ToString(), Session["UserRoleID"].ToString().Trim(), "MANUFACTURER MASTER", checkAccessType.InsertAction) == false)
        {
            Button1.Enabled = false;
        }
        
        if (Session["userName"] == null)
        {
            Response.Redirect("../LoginPage.aspx");
        }
        if (!IsPostBack)
        {
            gridfill();
            Tab1Func();
            DataTable dt = thepatho.generatemcode(Session["CoCode"].ToString());
            txtcode.Text = dt.Rows[0][0].ToString();
        }
    }

    private void gridfill()
    {
        GridView1.DataSource = thepatho.gridmanu(Session["CoCode"].ToString(), txtmanf.Text.Trim());
        GridView1.DataBind();
    }

    private void ResetAllFields()
    {
        DataTable dt = thepatho.generatemcode(Session["CoCode"].ToString());
        txtcode.Text = dt.Rows[0][0].ToString();
        txtaddress.Text = "";
        txtname.Text = "";
        txtph1.Text = "";
        TextBox4.Value = "";
        txtph2.Text = "";
        txtemail.Text = "";
        Button1.Text = "Submit";

    }
    protected void Button1_Click(object sender, EventArgs e)
    {
        string id;
         if (TextBox4.Value != "")
        {
            id = TextBox4.Value;
        }
        else
        {
            id = "null";
        }
         
         
             if (Button1.Text == "Submit")
             {

                 string wherecon = "compcode='" + Session["CoCode"].ToString() + "' and MName='" + txtname.Text + "'";
                 DataTable dt = thepatho.checkname("PH_ManufactureMaster", wherecon);
                 if (dt.Rows.Count > 0)
                 {
                    
                     ScriptManager.RegisterStartupScript(this, this.GetType(), "msg", "alert('Manufacturer Already Exist!');", true);
                 }
                 else
                 {
                     thepatho.InsertManufacture(txtemail.Text, txtcode.Text, txtname.Text, txtaddress.Text, txtph1.Text, txtph2.Text, Session["userId"].ToString(), Session["CoCode"].ToString());
                     ScriptManager.RegisterStartupScript(this, this.GetType(), "msg", "alert('Inserted Successfully !');", true);
                 }

             }
             else
             {


                 thepatho.UpdateAppointment(txtemail.Text, id, txtname.Text, txtaddress.Text, txtph1.Text, txtph2.Text, Session["CoCode"].ToString(), Session["userId"].ToString());
                 ScriptManager.RegisterStartupScript(this, this.GetType(), "msg", "alert('Updated Successfully !');", true);
                 Button1.Text = "Submit";

             }
         

        gridfill();
        ResetAllFields();
    }
    protected void Button2_Click(object sender, EventArgs e)
    {
        ResetAllFields();
    }
    protected void GridView1_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        GridView1.PageIndex = e.NewPageIndex;
        gridfill();
    }
    protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "Select")
        {
            int index = Convert.ToInt32(e.CommandArgument);
            Label lblcode = (Label)GridView1.Rows[index].FindControl("lblcode");
            TextBox4.Value = lblcode.Text;
            txtcode.Text = lblcode.Text;

            Label lblname = (Label)GridView1.Rows[index].FindControl("lblname");
            txtname.Text = lblname.Text;

            Label lbladdress = (Label)GridView1.Rows[index].FindControl("lbladdress");
            txtaddress.Text = lbladdress.Text;

            Label lblphone1 = (Label)GridView1.Rows[index].FindControl("lblphone1");
            txtph1.Text = lblphone1.Text;

            Label lblphone2 = (Label)GridView1.Rows[index].FindControl("lblphone2");
            txtph2.Text = lblphone2.Text;
            Label lbalmail = (Label)GridView1.Rows[index].FindControl("lbalmail");
            txtemail.Text = lbalmail.Text;
            Tab1Func();
            Button1.Text = "Update";
            if (Objmain.checkaccss(Session["CoCode"].ToString(), Session["UserRoleID"].ToString().Trim(), "MANUFACTURER MASTER", checkAccessType.UpdateAction) == false)
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
        Label lblcode = (Label)GridView1.Rows[e.RowIndex].FindControl("lblcode");
        thepatho.DeleteAppointment(lblcode.Text, Session["CoCode"].ToString());
        ScriptManager.RegisterStartupScript(this, this.GetType(), "msg", "alert('Deleted Successfully !');", true);
        gridfill();
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

    protected void Button3_Click(object sender, EventArgs e)
    {
        gridfill();
    }

    

    [System.Web.Script.Services.ScriptMethod()]
    [System.Web.Services.WebMethod]
    public static List<string> SearchManuafacturer(string prefixText, int count)
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
                cmd.CommandText = "select distinct MName + '~' + ltrim(rtrim(MCode)) as Name from PH_ManufactureMaster where compcode=@Compcode and MName like @SearchText +'%'";
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
            if (Objmain.checkaccss(Session["CoCode"].ToString(), Session["UserRoleID"].ToString().Trim(), "MANUFACTURER MASTER", checkAccessType.DeleteAction) == false)
            {
                codel.Visible = false;
                e.Row.Cells[7].Visible = false;
            }
        }
    }
}