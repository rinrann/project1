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
using System.Collections.Generic;

public partial class Master_MedicineRoute : System.Web.UI.Page
{
    Main Objmain = new Main(ConfigurationSettings.AppSettings["ConnectionString"].ToString());
    MedicineRoute theHelper = new MedicineRoute(ConfigurationSettings.AppSettings["ConnectionString"].ToString());
    protected void Page_Load(object sender, EventArgs e)
    {
        Page.Title = "Medicine Route";
        if (Session["userName"] == null)
        {
            Response.Redirect("../LoginPage.aspx");
        }
        if (Objmain.checkaccss(Session["CoCode"].ToString(), Session["UserRoleID"].ToString().Trim(), "MEDICINE ROUTE", checkAccessType.ViewAction) == false)
        {
            Response.Redirect("../AccessDenied.aspx");
        }

        if (Objmain.checkaccss(Session["CoCode"].ToString(), Session["UserRoleID"].ToString().Trim(), "MEDICINE ROUTE", checkAccessType.InsertAction) == false)
        {
            Button1.Enabled = false;
        }
        if (Objmain.checkaccss(Session["CoCode"].ToString(), Session["UserRoleID"].ToString().Trim(), "MEDICINE ROUTE", checkAccessType.DeleteAction) == false)
        {
            GridView1.Columns[3].Visible = false;
        }
        if (!IsPostBack)
        {
            Tab1Func();
            GridFill();
            txtRouteId.Text = theHelper.GetRouteID(Session["CoCode"].ToString().Trim()).ToString();
        }
    }
    private void GridFill()
    {
        GridView1.DataSource = theHelper.GetAllRoute(Session["CoCode"].ToString().Trim());
        GridView1.DataBind();
    }
    private void ResetAllFields()
    {
        txtRouteId.Text = "";
        txtRouteName.Text = "";
        Button1.Text = "Submit";
        txtRouteId.Text = theHelper.GetRouteID(Session["CoCode"].ToString().Trim()).ToString();

        if (Objmain.checkaccss(Session["CoCode"].ToString(), Session["UserRoleID"].ToString().Trim(), "MEDICINE ROUTE", checkAccessType.InsertAction) == false)
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

            Label lblRouteID = (Label)GridView1.Rows[index].FindControl("RouteID");
            txtRouteId.Text = lblRouteID.Text;
            HiddenField1.Value = lblRouteID.Text;

            Label lblRouteName = (Label)GridView1.Rows[index].FindControl("RouteName");
            txtRouteName.Text = lblRouteName.Text;
            Tab1Func();
            Button1.Text = "Update";
            if (Objmain.checkaccss(Session["CoCode"].ToString(), Session["UserRoleID"].ToString().Trim(), "MEDICINE ROUTE", checkAccessType.UpdateAction) == false)
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
          Label lblRouteID = (Label)GridView1.Rows[e.RowIndex].FindControl("RouteID");
          theHelper.DeleteMedicineRoute(Convert.ToInt32(lblRouteID.Text), Session["CoCode"].ToString().Trim());
        ScriptManager.RegisterStartupScript(this, this.GetType(), "msg", "alert('Deleted  Successfully !');", true);
        GridFill();
        ResetAllFields();
        txtRouteId.Text = theHelper.GetRouteID(Session["CoCode"].ToString().Trim()).ToString();
    }
    protected void Button1_Click(object sender, EventArgs e)
    {
          if (Button1.Text == "Submit")
        {
            theHelper.InsertMedicineRoute(txtRouteName.Text, Session["userName"].ToString(), Session["CoCode"].ToString().Trim());
            ScriptManager.RegisterStartupScript(this, this.GetType(), "msg", "alert('Inserted Successfully !');", true);
        }
        else
        {
            theHelper.UpdateMedicineRoute(HiddenField1.Value, txtRouteName.Text, Session["CoCode"].ToString().Trim());
            ScriptManager.RegisterStartupScript(this, this.GetType(), "msg", "alert('Updated Successfully !');", true);
            Button1.Text = "Submit";
        }

          GridView1.DataSource = theHelper.GetAllRoute(Session["CoCode"].ToString().Trim());
        GridView1.DataBind();
        ResetAllFields();
        txtRouteId.Text = theHelper.GetRouteID(Session["CoCode"].ToString().Trim()).ToString(Session["CoCode"].ToString().Trim());

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

    [System.Web.Script.Services.ScriptMethod()]
    [System.Web.Services.WebMethod]
    public static List<string> Searchroute(string prefixText, int count)
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
                cmd.CommandText = "select distinct RouteName as Name from IPD_MedicineRoute where compcode=@Compcode and RouteName like @SearchText +'%'";
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
            if (Objmain.checkaccss(Session["CoCode"].ToString(), Session["UserRoleID"].ToString().Trim(), "MEDICINE ROUTE", checkAccessType.DeleteAction) == false)
            {
                e.Row.Cells[3].Visible = false;
            }

        }
    }
}
