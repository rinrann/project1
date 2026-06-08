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

public partial class Account_CostCenter : System.Web.UI.Page
{
    Main Objmain = new Main(ConfigurationSettings.AppSettings["ConnectionString"].ToString());
    CostCentre theHelper = new CostCentre(ConfigurationSettings.AppSettings["ConnectionString"].ToString());
    string dt = DateTime.Now.ToString("MM/dd/yyyy");
   
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["userName"] == null)
        {
            Response.Redirect("../LoginPage.aspx");
        }
        if (Objmain.checkaccss(Session["CoCode"].ToString(), Session["UserRoleID"].ToString().Trim(), "COST CENTER", checkAccessType.ViewAction) == false)
        {
            Response.Redirect("../AccessDenied.aspx");
        }

        if (Objmain.checkaccss(Session["CoCode"].ToString(), Session["UserRoleID"].ToString().Trim(), "COST CENTER", checkAccessType.InsertAction) == false)
        {
            Button1.Enabled = false;
        }
        if (Objmain.checkaccss(Session["CoCode"].ToString(), Session["UserRoleID"].ToString().Trim(), "COST CENTER", checkAccessType.DeleteAction) == false)
        {
            GridView1.Columns[5].Visible = false;
        }
        Page.Title = "Cost Centre Master";
        if (Session["userName"] == null)
        {
            Response.Redirect("../LoginPage.aspx");
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
        GridView1.DataSource = theHelper.GetCostCentre(Session["CoCode"].ToString(), Session["YearCode"].ToString());
        GridView1.DataBind();
    }

    private void DropDownFill()
    {
        DropDownList1.Items.Clear();
        DropDownList1.Items.Insert(0, new ListItem("--Select--", "0"));
        DropDownList1.Items.Insert(1, new ListItem("IPD", "I"));
        DropDownList1.Items.Insert(2, new ListItem("OPD ", "O"));
        DropDownList1.Items.Insert(3, new ListItem("OT ", "T"));
        DropDownList1.Items.Insert(4, new ListItem("PATHOLOGY ", "P"));
        DropDownList1.Items.Insert(5, new ListItem("USG ", "U"));
        DropDownList1.Items.Insert(6, new ListItem("XRAY ", "X"));
        DropDownList1.Items.Insert(7, new ListItem("DIALYSIS ", "D"));
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

    public void Tab1Func()
    {
        Tab1.CssClass = "Clicked";
        Tab2.CssClass = "Initial";
        MainView.ActiveViewIndex = 0;
    }

    protected void Button1_Click(object sender, EventArgs e)
    {
        if (Button1.Text == "Submit")
        {
            if(theHelper.CheckCostCode(TextBox1.Text,Session["CoCode"].ToString(), Session["YearCode"].ToString())>0)
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "msg", "alert('Code Already Exist! Try Another.');", true);
            }
            else
            {
                if (theHelper.InsertCostCentre(TextBox1.Text, TextBox2.Text, DropDownList1.SelectedValue.ToString(), Session["CoCode"].ToString(), Session["YearCode"].ToString(),Session["userName"].ToString(), dt) == true)
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "msg", "alert('Insered Successfully !');", true);
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "msg", "alert('Error in Insered Data !');", true);
                }
            }
        }
        else
        {
            if (theHelper.UpdateCostCentre(TextBox1.Text, TextBox2.Text, DropDownList1.SelectedValue.ToString(), Session["CoCode"].ToString(), Session["YearCode"].ToString(), Session["userName"].ToString(), dt) == true)
                ScriptManager.RegisterStartupScript(this, this.GetType(), "msg", "alert('Updated Successfully !');", true);
            else
                ScriptManager.RegisterStartupScript(this, this.GetType(), "msg", "alert('Error in Updated Data !');", true);
            Button1.Text = "Submit";
        }

        ResetAllFields();
        GridFill();
    }

    protected void Button2_Click(object sender, EventArgs e)
    {
        ResetAllFields();

    }

    private void ResetAllFields()
    {
        DropDownList1.SelectedIndex = 0;
        TextBox1.Text = "";
        
        TextBox2.Text = "";
        
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
            lblError.Text = "";
            Label lblCostCode = (Label)GridView1.Rows[index].FindControl("CostCode");
            HiddenField1.Value = lblCostCode.Text;
            TextBox1.Text = lblCostCode.Text;
            Label lblCostName = (Label)GridView1.Rows[index].FindControl("CostName");
            TextBox2.Text = lblCostName.Text;


            Label lbltype = (Label)GridView1.Rows[index].FindControl("CostType");
            DropDownList1.SelectedValue = lbltype.Text;
            Tab1Func();
            TextBox1.ReadOnly = true;
            Button1.Text = "Update";
            if (Objmain.checkaccss(Session["CoCode"].ToString(), Session["UserRoleID"].ToString().Trim(), "COST CENTER", checkAccessType.UpdateAction) == false)
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
        Label lblCostCode = (Label)GridView1.Rows[e.RowIndex].FindControl("CostCode");
        if(theHelper.DeleteCostCentre(lblCostCode.Text, Session["CoCode"].ToString(),Session["YearCode"].ToString())==true)
        {
        lblError.ForeColor = System.Drawing.Color.Green;
        lblError.Text = "Deleted Successfully";
        }
        else
        {
            lblError.ForeColor = System.Drawing.Color.Green;
            lblError.Text = "Cannot Be Deleted ";
        }
        GridFill();
        ResetAllFields();
    }
    protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            if (Objmain.checkaccss(Session["CoCode"].ToString(), Session["UserRoleID"].ToString().Trim(), "COST CENTER", checkAccessType.DeleteAction) == false)
            {
                code1.Visible = false;
                e.Row.Cells[5].Visible = false;
            }
        }
    }
}