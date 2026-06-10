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

public partial class OPD_Complain : System.Web.UI.Page
{
    Main Objmain = new Main(ConfigurationSettings.AppSettings["ConnectionString"].ToString());
    Complain theprescription = new Complain(ConfigurationSettings.AppSettings["ConnectionString"].ToString());
    MD_MedicineMaster theMedicine = new  MD_MedicineMaster(ConfigurationSettings.AppSettings["ConnectionString"].ToString());

    protected void Page_Load(object sender, EventArgs e)
    {
        Page.Title = "Complain Master";
        if (Session["userId"] == null)
        {
            Response.Redirect("../LoginPage.aspx");
        }
        if (Objmain.checkaccss(Session["CoCode"].ToString(), Session["UserRoleID"].ToString().Trim(), "SYMPTOM MASTER", checkAccessType.ViewAction) == false)
        {
            Response.Redirect("../AccessDenied.aspx");
        }

        if (Objmain.checkaccss(Session["CoCode"].ToString(), Session["UserRoleID"].ToString().Trim(), "SYMPTOM MASTER", checkAccessType.InsertAction) == false)
        {
            Button1.Enabled = false;
        }
        if (Objmain.checkaccss(Session["CoCode"].ToString(), Session["UserRoleID"].ToString().Trim(), "SYMPTOM MASTER", checkAccessType.DeleteAction) == false)
        {            
            GridView1.Columns[6].Visible = false;
        }
        GridFill();
        if (!IsPostBack)
        {
            Tab1Func();
            DropDownFill();
        }


    }

    private void DropDownFill()
    {
        DropDownList1.Items.Clear();
        this.DropDownList1.DataSource = theMedicine.DropdownID2(Session["CoCode"].ToString().Trim());
        this.DropDownList1.DataTextField = "MedicineGroupName";
        this.DropDownList1.DataValueField = "MedicineGroupID";
        this.DropDownList1.DataBind();
        this.DropDownList1.Items.Insert(0, new ListItem("--Select--", "0"));

        this.DropDownList2.Items.Insert(0, new ListItem("--Select--", "0"));
    }
    private void ResetAllFields()
    {
        txtchemicalname.Text = "";
        HiddenField1.Value = "";
        Button1.Text = "Submit";
        DropDownList1.SelectedIndex = 0; 
        DropDownList2.SelectedIndex = 0;

        if (Objmain.checkaccss(Session["CoCode"].ToString(), Session["UserRoleID"].ToString().Trim(), "COMPLAIN MASTER", checkAccessType.InsertAction) == false)
        {
            Button1.Enabled = false;
        }
    }

    protected int SearchText(string Value, DropDownList ddl)
    {
        int i;
        for (i = 0; i < ddl.Items.Count; i++)
        {
            if (ddl.Items[i].Text.Trim() == Value.Trim())
                return i;
        }
        return -1;
    }


    private void GridFill()
    {
        GridView1.DataSource = theprescription.GridComplain(Session["CoCode"].ToString().Trim());
        GridView1.DataBind();
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
            lblError.Text = "";
            int index = Convert.ToInt32(e.CommandArgument);
            Label lblID = (Label)GridView1.Rows[index].FindControl("lblID");
            HiddenField1.Value = lblID.Text;

            Label lblname = (Label)GridView1.Rows[index].FindControl("lblname");
            txtchemicalname.Text = lblname.Text;

            Label lblMedicineGroupName = (Label)GridView1.Rows[index].FindControl("lblMedicineGroupName");
            Label lblSubGrName = (Label)GridView1.Rows[index].FindControl("lblSubGrName");
            Label lblMedicineGroupId = (Label)GridView1.Rows[index].FindControl("lblMedicineGroupId");

            DropDownList1.SelectedIndex = SearchText(lblMedicineGroupName.Text, DropDownList1);


            SubGroupFill(DropDownList2, lblMedicineGroupId.Text);
            DropDownList2.SelectedIndex = SearchText(lblSubGrName.Text, DropDownList2);
            Tab1Func();
            Button1.Text = "Update";
            if (Objmain.checkaccss(Session["CoCode"].ToString(), Session["UserRoleID"].ToString().Trim(), "COMPLAIN MASTER", checkAccessType.UpdateAction) == false)
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
        Label lblID = (Label)GridView1.Rows[e.RowIndex].FindControl("lblID");
        theprescription.DeleteComplain(lblID.Text,Session["CoCode"].ToString().Trim());
        lblError.ForeColor = System.Drawing.Color.Green;
        lblError.Text = "Deleted Successfully";
        GridFill();
        ResetAllFields();
    }
    protected void Button1_Click(object sender, EventArgs e)
    {
        if (Button1.Text == "Submit")
        {

            if (theprescription.InsertComplain(txtchemicalname.Text, DropDownList1.SelectedValue, DropDownList2.SelectedValue, Session["CoCode"].ToString().Trim()) == true)
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "msg", "alert('Inserted  Successfully !');", true);
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "msg", "alert('Error in Inderted Data !');", true);
            }
        }
        else
        {
            if (theprescription.UpdateComplain(HiddenField1.Value, txtchemicalname.Text, DropDownList1.SelectedValue, DropDownList2.SelectedValue, Session["CoCode"].ToString().Trim()) == true)
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "msg", "alert('Updated  Successfully !');", true);
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "msg", "alert('Error in Updated Data !');", true);
            }
            Button1.Text = "Submit";
        }
        GridFill();
        ResetAllFields();
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

    public void SubGroupFill(DropDownList ddl,string value)
    {
        ddl.Items.Clear();
        ddl.DataSource = theMedicine.DropdownSubGroup(value, Session["CoCode"].ToString().Trim());
        ddl.DataTextField = "SubGrName";
        ddl.DataValueField = "ID";
        ddl.DataBind();
        ddl.Items.Insert(0, new ListItem("--Select--", "0"));
    }
    protected void DropDownList1_SelectedIndexChanged(object sender, EventArgs e)
    {
        SubGroupFill(DropDownList2, DropDownList1.SelectedValue);
    }

    [System.Web.Script.Services.ScriptMethod()]
    [System.Web.Services.WebMethod]
    public static List<string> SearchComplain(string prefixText, int count)
    {
        using (SqlConnection conn = new SqlConnection())
        {
            conn.ConnectionString = ConfigurationSettings.AppSettings["ConnectionString"].ToString();
            using (SqlCommand cmd = new SqlCommand())
            {
                cmd.CommandText = "select distinct ComplainName as Name from OPD_Complain where ComplainName like @SearchText +'%'";
                cmd.Parameters.AddWithValue("@SearchText", prefixText);
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

    protected void GridView1_RowDataBound1(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {            
            if (Objmain.checkaccss(Session["CoCode"].ToString(), Session["UserRoleID"].ToString().Trim(), "COMPLAIN MASTER", checkAccessType.DeleteAction) == false)
            {                
                e.Row.Cells[6].Visible = false;
            }
        }
    }
}