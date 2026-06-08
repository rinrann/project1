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
 

public partial class IPD_SisterCharges : System.Web.UI.Page
{
    Main Objmain = new Main(ConfigurationSettings.AppSettings["ConnectionString"].ToString());
    SisterCharges thecharge = new SisterCharges(ConfigurationSettings.AppSettings["ConnectionString"].ToString());
    string dt = DateTime.Now.ToString("MM/dd/yyyy");
    protected void Page_Load(object sender, EventArgs e)
    {
        Page.Title = "Sister Aya Charges";
        if (Session["userName"] == null)
        {
            Response.Redirect("../LoginPage.aspx");
        }
        if (Objmain.checkaccss(Session["CoCode"].ToString(), Session["UserRoleID"].ToString().Trim(), "SISTER / AYA CHAGES", checkAccessType.ViewAction) == false)
        {
            Response.Redirect("../AccessDenied.aspx");
        }

        if (Objmain.checkaccss(Session["CoCode"].ToString(), Session["UserRoleID"].ToString().Trim(), "SISTER / AYA CHAGES", checkAccessType.InsertAction) == false)
        {
            Button1.Enabled = false;
        }
        if (Objmain.checkaccss(Session["CoCode"].ToString(), Session["UserRoleID"].ToString().Trim(), "SISTER / AYA CHAGES", checkAccessType.DeleteAction) == false)
        {
            GridView1.Columns[8].Visible = false;
        }

        if (!Page.IsPostBack)
        {
            Tab1Func();
            DropDownFill(); GridFill();
        }
    }

    public void DropDownFill()
    {
        CenterNameList.DataSource = thecharge.GetAllCenterName(Session["CoCode"].ToString().Trim());
        this.CenterNameList.DataTextField = "CenterName";
        this.CenterNameList.DataValueField = "CenterCode";
        CenterNameList.DataBind();
        this.CenterNameList.Items.Insert(0, new ListItem("--Select--", "0"));


        DropDownList1.DataSource = thecharge.DropdownType(Session["CoCode"].ToString().Trim());
        this.DropDownList1.DataTextField = "TypeName";
        this.DropDownList1.DataValueField = "ID";
        DropDownList1.DataBind();
        this.DropDownList1.Items.Insert(0, new ListItem("--Select--", "0"));
    }

    private void GridFill()
    {
        GridView1.DataSource = thecharge.GetAllhospital(Session["CoCode"].ToString().Trim());
        GridView1.DataBind();
    }

    private void ResetAllFields()
    {
        HiddenField1.Value = "";
        TextBox2.Text = "";
        TextBox3.Text = "";
        TextBox1.Text = "";
        Button1.Text = "Submit";
        CenterNameList.SelectedIndex = 0;
        DropDownList1.SelectedIndex = 0;
        if (Objmain.checkaccss(Session["CoCode"].ToString(), Session["UserRoleID"].ToString().Trim(), "SISTER / AYA CHAGES", checkAccessType.InsertAction) == false)
        {
            Button1.Enabled = false;
        }
    }

    protected void Button1_Click(object sender, EventArgs e)
    {
        if (Button1.Text == "Submit")
        {

            if (thecharge.InsertSister_Aya_Charges(Convert.ToInt32(CenterNameList.SelectedValue), DropDownList1.SelectedValue, TextBox1.Text, TextBox2.Text, TextBox3.Text, Session["userName"].ToString(), dt, Session["CoCode"].ToString().Trim()) == true)
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "msg", "alert('Inserted  Successfully !');", true);
                ResetAllFields();
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "msg", "alert('Error in Inserted Data !');", true);
            }
        }
        else
        {
            if (thecharge.UpdateSister_Aya_Charges(Convert.ToInt32(HiddenField1.Value), Convert.ToInt32(CenterNameList.SelectedValue), DropDownList1.SelectedValue, TextBox1.Text, TextBox2.Text, TextBox3.Text, dt, Session["CoCode"].ToString().Trim()) == true)
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "msg", "alert('Updated  Successfully !');", true);
                ResetAllFields();
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "msg", "alert('Error in Updated Data !');", true);
            }
            Button1.Text = "Submit";
        }
        GridFill();
      
    }

    protected void Button2_Click(object sender, EventArgs e)
    {
        ResetAllFields();
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

            Label lblId = (Label)GridView1.Rows[index].FindControl("lblId");
            HiddenField1.Value = lblId.Text;

            Label lblcentre = (Label)GridView1.Rows[index].FindControl("lblcentre");
            CenterNameList.SelectedIndex = SearchIndex(lblcentre.Text, CenterNameList);

            Label lblDayShift = (Label)GridView1.Rows[index].FindControl("lblDayShift");
            TextBox1.Text = lblDayShift.Text;

            Label lblNightShift = (Label)GridView1.Rows[index].FindControl("lblNightShift");
            TextBox2.Text = lblNightShift.Text;

            Label lblDayNightShift = (Label)GridView1.Rows[index].FindControl("lblDayNightShift");
            TextBox3.Text = lblDayNightShift.Text;

            Label lbltype = (Label)GridView1.Rows[index].FindControl("lbltype");
            DropDownList1.SelectedIndex = SearchIndex(lbltype.Text, DropDownList1);
            Tab1Func();
            Button1.Text = "Update";
            if (Objmain.checkaccss(Session["CoCode"].ToString(), Session["UserRoleID"].ToString().Trim(), "SISTER / AYA CHAGES", checkAccessType.UpdateAction) == false)
            {
                Button1.Enabled = false;
            }
            else
            {
                Button1.Enabled = true;
            }
        }
    }

    protected int SearchIndex(string Value, DropDownList ddl)
    {
        int i;
        for (i = 0; i < ddl.Items.Count; i++)
        {
            if (ddl.Items[i].Text.Trim() == Value.Trim())
                return i;
        }
        return -1;
    }

    protected void GridView1_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {

     Label lblId = (Label)GridView1.Rows[e.RowIndex].FindControl("lblId");

     if (thecharge.DeleteSister_Aya_Charges(lblId.Text, Session["CoCode"].ToString().Trim()) == true)
     {
         lblError.ForeColor = System.Drawing.Color.Green;
         lblError.Text = "Deleted Successfully";
         GridFill();
         ResetAllFields();
     }
     else
     {


         lblError.Text = "Error Deleting";
     }
   
          
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
            if (Objmain.checkaccss(Session["CoCode"].ToString(), Session["UserRoleID"].ToString().Trim(), "SISTER / AYA CHAGES", checkAccessType.DeleteAction) == false)
            {
                e.Row.Cells[8].Visible = false;
            }
        }
    }
}