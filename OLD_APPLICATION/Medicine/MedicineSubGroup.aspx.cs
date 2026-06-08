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
 
public partial class Medicine_MedicineSubGroup : System.Web.UI.Page
{
    Main Objmain = new Main(ConfigurationSettings.AppSettings["ConnectionString"].ToString());
    MD_MedicineSubGroup theHelper = new MD_MedicineSubGroup(ConfigurationSettings.AppSettings["ConnectionString"].ToString());
    protected void Page_Load(object sender, EventArgs e)
    {
        Page.Title = "Medicine Sub Group";
        if (Session["userName"] == null)
        {
            Response.Redirect("../LoginPage.aspx");
        }
        if (Objmain.checkaccss(Session["CoCode"].ToString(), Session["UserRoleID"].ToString().Trim(), "MEDICINE SUB GROUP", checkAccessType.ViewAction) == false)
        {
            Response.Redirect("../AccessDenied.aspx");
        }

        if (Objmain.checkaccss(Session["CoCode"].ToString(), Session["UserRoleID"].ToString().Trim(), "MEDICINE SUB GROUP", checkAccessType.InsertAction) == false)
        {
            Button1.Enabled = false;
        }
        if (!IsPostBack)
        {
            GridFill();
            DropDownFill();
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
                cmd.CommandText = "select distinct s.SubGrName  as Name from IPD_MedicineSubGroup  s where s.compcode=@Compcode and s.SubGrName like @SearchText + '%'";
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


    private void DropDownFill()
    {
        DropDownList1.Items.Clear();
        DropDownList1.DataSource = theHelper.DropdownMedicineGroup();
        DropDownList1.DataTextField = "MedicineGroupName";
        DropDownList1.DataValueField = "MedicineGroupID";
        DropDownList1.DataBind();
        DropDownList1.Items.Insert(0, new ListItem("--Select--", "0")); 

        DropDownList8.Items.Clear();
        DropDownList8.DataSource = theHelper.DropdownMedicineGroup();
        DropDownList8.DataTextField = "MedicineGroupName";
        DropDownList8.DataValueField = "MedicineGroupID";
        DropDownList8.DataBind();
        DropDownList8.Items.Insert(0, new ListItem("--Select--", "0"));
    }

    private void GridFill()
    {
        GridView1.SelectedIndex = -1;
        GridView1.DataSource = theHelper.GridFill(DropDownList8.SelectedValue, DropDownList9.SelectedValue);
        GridView1.DataBind();
    }
    private void ResetAllFields()
    {
        DropDownList1.SelectedIndex = 0;
        txtMedicineGroupName.Text = "";
        Button1.Text = "Submit";
        if (Objmain.checkaccss(Session["CoCode"].ToString(), Session["UserRoleID"].ToString().Trim(), "MEDICINE SUB GROUP", checkAccessType.InsertAction) == false)
        {
            Button1.Enabled = false;
        }
    }

    protected void GridView1_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        GridView1.PageIndex = e.NewPageIndex;
        GridFill();
    }
    protected int SearchIndexById(string Value, DropDownList ddl)
    {
        int i;
        for (i = 0; i < ddl.Items.Count; i++)
        {
            if (ddl.Items[i].Text.Trim() == Value.Trim())
                return i;
        }
        return -1;
    }
    protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "Select")
        {
            lblError.Text = "";
            int index = Convert.ToInt32(e.CommandArgument);
             Label lblid = (Label)GridView1.Rows[index].FindControl("lblid");
            HiddenField1.Value = lblid.Text;


            Label lblgroup = (Label)GridView1.Rows[index].FindControl("lblgroup");
            DropDownList1.SelectedIndex = SearchIndexById(lblgroup.Text, DropDownList1);

            Label lblsubgr = (Label)GridView1.Rows[index].FindControl("lblsubgr");
            txtMedicineGroupName.Text = lblsubgr.Text;
            Tab1Func();
            Button1.Text = "Update";
            if (Objmain.checkaccss(Session["CoCode"].ToString(), Session["UserRoleID"].ToString().Trim(), "MEDICINE SUB GROUP", checkAccessType.UpdateAction) == false)
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
         Label lblid = (Label)GridView1.Rows[e.RowIndex].FindControl("lblid");
         if (theHelper.DeleteMedicineGroup(lblid.Text, Session["CoCode"].ToString()) == true)
         {
             ScriptManager.RegisterStartupScript(this, this.GetType(), "msg", "alert('Deleted  Successfully !');", true);
         }
         else
         {
             ScriptManager.RegisterStartupScript(this, this.GetType(), "msg", "alert('Error in Deleted Data  !');", true);
         } 
        GridFill();
        ResetAllFields();
    }
    protected void Button1_Click(object sender, EventArgs e)
    {
           if (Button1.Text == "Submit")
        {
            if (theHelper.InsertMedicineSubGroup(txtMedicineGroupName.Text, DropDownList1.SelectedValue, Session["userName"].ToString(), Session["CoCode"].ToString()) == true)
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "msg", "alert('Inserted  Successfully !');", true);
                ResetAllFields();

            }
            else
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "msg", "alert('Duplicate Entry cannot Possible  !');", true);
            }
        }
        else
        {
            if (theHelper.UpdateMedicineGroup(HiddenField1.Value, txtMedicineGroupName.Text, DropDownList1.SelectedValue, Session["CoCode"].ToString()) == true)
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "msg", "alert('Updated  Successfully !');", true);
                ResetAllFields();
                Button1.Text = "Submit";
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "msg", "alert('Duplicate Entry cannot Possible  !');", true);
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
    protected void DropDownList8_SelectedIndexChanged(object sender, EventArgs e)
    {
        subFill(DropDownList8.SelectedValue, DropDownList9);
    }

    public void subFill(string value, DropDownList d1)
    {
        d1.Items.Clear();
        d1.DataSource = theHelper.DropdownSubGroup(value);
        d1.DataTextField = "SubGrName";
        d1.DataValueField = "ID";
        d1.DataBind();
        d1.Items.Insert(0, new ListItem("--Select--", "0"));
    }
    protected void Button3_Click(object sender, EventArgs e)
    {
        GridFill();
    }
    protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            if (Objmain.checkaccss(Session["CoCode"].ToString(), Session["UserRoleID"].ToString().Trim(), "MEDICINE SUB GROUP", checkAccessType.DeleteAction) == false)
            {
                coldel.Visible = false;
                e.Row.Cells[4].Visible = false;
            }
        }
    }
}