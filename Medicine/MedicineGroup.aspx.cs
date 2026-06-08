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
using System.Globalization;
 

public partial class Master_MedicineGroup : System.Web.UI.Page
{
    Main Objmain = new Main(ConfigurationSettings.AppSettings["ConnectionString"].ToString());
    MD_MedicineGroup theHelper = new MD_MedicineGroup(ConfigurationSettings.AppSettings["ConnectionString"].ToString());
    protected void Page_Load(object sender, EventArgs e)
    {
        Page.Title = "Medicine Group";
        if (Session["userName"] == null)
        {
            Response.Redirect("../LoginPage.aspx");
        }
        if (Objmain.checkaccss(Session["CoCode"].ToString(), Session["UserRoleID"].ToString().Trim(), "MEDICINE GROUP", checkAccessType.ViewAction) == false)
        {
            Response.Redirect("../AccessDenied.aspx");
        }

        if (Objmain.checkaccss(Session["CoCode"].ToString(), Session["UserRoleID"].ToString().Trim(), "MEDICINE GROUP", checkAccessType.InsertAction) == false)
        {
            Button1.Enabled = false;
        }
        
        if (!IsPostBack)
        {
            GridFill();
            Tab1Func();
        }
    }

    [System.Web.Script.Services.ScriptMethod()]
    [System.Web.Services.WebMethod]

    public static List<string> SearchCustomers(string prefixText, int count)
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
                cmd.CommandText = "select distinct g.MedicineGroupName  as Name from IPD_MedicineGroup  g where g.compcode=@Compcode and g.MedicineGroupName like @SearchText + '%'";
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

    private void GridFill()
    {
        GridView1.SelectedIndex = -1;
        GridView1.DataSource = theHelper.GetAllMedicineGroup(Session["CoCode"].ToString());
        GridView1.DataBind();
    }
    private void ResetAllFields()
    {
      
        txtMedicineGroupName.Text = "";
        Button1.Text = "Submit";
        if (Objmain.checkaccss(Session["CoCode"].ToString(), Session["UserRoleID"].ToString().Trim(), "MEDICINE GROUP", checkAccessType.InsertAction) == false)
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
            GridView1.Rows[index].BackColor = Color.GreenYellow;

            Label lblMedicineGroupID = (Label)GridView1.Rows[index].FindControl("MedicineGroupID");
            HiddenField1.Value = lblMedicineGroupID.Text;

            Label lblMedicineGroupName = (Label)GridView1.Rows[index].FindControl("MedicineGroupName");
            txtMedicineGroupName.Text = lblMedicineGroupName.Text;
            Tab1Func();
            Button1.Text = "Update";
            if (Objmain.checkaccss(Session["CoCode"].ToString(), Session["UserRoleID"].ToString().Trim(), "MEDICINE GROUP", checkAccessType.UpdateAction) == false)
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
        Label lblMedicineGroupID = (Label)GridView1.Rows[e.RowIndex].FindControl("MedicineGroupID");
        if (theHelper.DeleteMedicineGroup(Convert.ToInt32(lblMedicineGroupID.Text), Session["CoCode"].ToString()) == true)
            ScriptManager.RegisterStartupScript(this, this.GetType(), "msg", "alert('Deleted  Successfully !');", true);
        else
            ScriptManager.RegisterStartupScript(this, this.GetType(), "msg", "alert('Error in Deleted Data  !');", true); 

        GridFill();
        ResetAllFields();
       
    }
    protected void Button1_Click(object sender, EventArgs e)
    {
       
        if (Button1.Text == "Submit")
        {
            if (theHelper.InsertMedicineGroup(txtMedicineGroupName.Text, Session["userName"].ToString(), Session["CoCode"].ToString().Trim()) == true)
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "msg", "alert('Inserted  Successfully !');", true);
                ResetAllFields();
            }
            else
                ScriptManager.RegisterStartupScript(this, this.GetType(), "msg", "alert('Duplicate Entry not Possible  !');", true);
        }
        else
        {
            if (theHelper.UpdateMedicineGroup(HiddenField1.Value, txtMedicineGroupName.Text, Session["CoCode"].ToString().Trim()) == true)
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "msg", "alert('Updated  Successfully !');", true);
                ResetAllFields();
            }
            else
                ScriptManager.RegisterStartupScript(this, this.GetType(), "msg", "alert('Duplicate Entry not Possible  !');", true);

            Button1.Text = "Submit";
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


    protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            if (Objmain.checkaccss(Session["CoCode"].ToString(), Session["UserRoleID"].ToString().Trim(), "MEDICINE GROUP", checkAccessType.DeleteAction) == false)
            {
                coldel.Visible = false;
                e.Row.Cells[3].Visible = false;
            }
        }
    }
}