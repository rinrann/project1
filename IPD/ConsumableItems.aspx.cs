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

public partial class Master_ConsumableItems : System.Web.UI.Page
{
    Main Objmain = new Main(ConfigurationSettings.AppSettings["ConnectionString"].ToString());
    ConsumableItems theHelper = new ConsumableItems(ConfigurationSettings.AppSettings["ConnectionString"].ToString());
    PatientDoctorVisit thedocvisit = new PatientDoctorVisit(ConfigurationSettings.AppSettings["ConnectionString"].ToString());
    protected void Page_Load(object sender, EventArgs e)
    {
        Page.Title = "Consumable Items Name";
        if (Session["userName"] == null)
        {
            Response.Redirect("../LoginPage.aspx");
        }
        if (Objmain.checkaccss(Session["CoCode"].ToString(), Session["UserRoleID"].ToString().Trim(), "CONSUMABLE ITEMS", checkAccessType.ViewAction) == false)
        {
            Response.Redirect("../AccessDenied.aspx");
        }

        if (Objmain.checkaccss(Session["CoCode"].ToString(), Session["UserRoleID"].ToString().Trim(), "CONSUMABLE ITEMS", checkAccessType.InsertAction) == false)
        {
            Button1.Enabled = false;
        }
        if (!IsPostBack)
        {
            GridFill();
            Tab1Func();
            DropDownFill();
        }
    }
    private void GridFill()
    {
        GridView1.DataSource = theHelper.GetAllConItem(Session["CoCode"].ToString().Trim());
        GridView1.DataBind();
    }
    private void ResetAllFields()
    {

         txtConItemName.Text = "";
         DropDownList1.SelectedIndex = 0; DropDownList2.SelectedIndex = 0;
        txtprice.Text = "";
        txtPricePerUnit.Text = "";
        Button1.Text = "Submit";

        if (Objmain.checkaccss(Session["CoCode"].ToString(), Session["UserRoleID"].ToString().Trim(), "CONSUMABLE ITEMS", checkAccessType.InsertAction) == false)
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

            DropDownFill();
            Label lblConItemID = (Label)GridView1.Rows[index].FindControl("ConItemID");
            HiddenField1.Value = lblConItemID.Text;

            Label lblConItemName = (Label)GridView1.Rows[index].FindControl("ConItemName");
            txtConItemName.Text = lblConItemName.Text;

            

            
            Label lblUnitID = (Label)GridView1.Rows[index].FindControl("UnitId");
            DropDownList1.SelectedValue = lblUnitID.Text;

            Label lblPricePerUnit = (Label)GridView1.Rows[index].FindControl("PricePerUnit");
            txtPricePerUnit.Text = lblPricePerUnit.Text;

            Label lblprice = (Label)GridView1.Rows[index].FindControl("lblprice");
            txtprice.Text = lblprice.Text;
            Tab1Func();
            

            Button1.Text = "Update";
            if (Objmain.checkaccss(Session["CoCode"].ToString(), Session["UserRoleID"].ToString().Trim(), "CONSUMABLE ITEMS", checkAccessType.UpdateAction) == false)
            {
                Button1.Enabled = false;
            }
            else
            {
                Button1.Enabled = true;
            }
        }
    }
    private void DropDownFill()
    {
        DropDownList1.Items.Clear();
        this.DropDownList1.DataSource = theHelper.DropdownID(Session["CoCode"].ToString());
        this.DropDownList1.DataTextField = "UnitName";
        this.DropDownList1.DataValueField = "UnitId";
        this.DropDownList1.DataBind();
        this.DropDownList1.Items.Insert(0, new ListItem("--Select--", "0"));


        DropDownList2.Items.Clear();
        DropDownList2.DataSource = thedocvisit.GetConsumableGroup(Session["CoCode"].ToString().Trim());
        DropDownList2.DataTextField = "ConGroupName";
        DropDownList2.DataValueField = "ConGrId";
        DropDownList2.DataBind();
        DropDownList2.Items.Insert(0, new ListItem("--Select--", "0"));
    }
    protected void GridView1_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
         Label lblConItemID = (Label)GridView1.Rows[e.RowIndex].FindControl("ConItemID");
         theHelper.DeleteConsumableItems(Convert.ToInt32(lblConItemID.Text), Session["CoCode"].ToString().Trim());
        lblError.ForeColor = System.Drawing.Color.Green;
        lblError.Text = "Deleted Successfully";
        GridFill();
        ResetAllFields();
       // txtConItemId.Text = theHelper.GetConItemID().ToString();
    }
    protected void Button1_Click(object sender, EventArgs e)
    {
     
        if (Button1.Text == "Submit")
        {

            if (theHelper.InsertConsumableItems(DropDownList2.SelectedValue, txtConItemName.Text, DropDownList1.SelectedValue, txtPricePerUnit.Text, txtprice.Text, Session["userName"].ToString(), Session["CoCode"].ToString().Trim()) == true)
            ScriptManager.RegisterStartupScript(this, this.GetType(), "msg", "alert('Insered Successfully !');", true);
            else
               ScriptManager.RegisterStartupScript(this, this.GetType(), "msg", "alert('Error in Insered Data !');", true);
        }
        else
        {
            if (theHelper.UpdateConsumableItems(DropDownList2.SelectedValue, HiddenField1.Value, txtConItemName.Text, DropDownList1.SelectedValue, txtPricePerUnit.Text, txtprice.Text, Session["CoCode"].ToString().Trim()) == true)
            ScriptManager.RegisterStartupScript(this, this.GetType(), "msg", "alert('Updated Successfully !');", true);
            else
               ScriptManager.RegisterStartupScript(this, this.GetType(), "msg", "alert('Error in Updated Data  !');", true);
            Button1.Text = "Submit";
        }

        ResetAllFields();
        GridFill();
       // txtConItemId.Text = theHelper.GetConItemID().ToString();
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
    public static List<string> SearchconItem(string prefixText, int count)
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
                cmd.CommandText = "select distinct ConItemName as Name from IPD_ConsumableItems where compcode=@Compcode and ConItemName like @SearchText +'%'";
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
            if (Objmain.checkaccss(Session["CoCode"].ToString(), Session["UserRoleID"].ToString().Trim(), "CONSUMABLE ITEMS", checkAccessType.DeleteAction) == false)
            {
                coldel.Visible = false;
                e.Row.Cells[9].Visible = false;
            }

        }
    }
}