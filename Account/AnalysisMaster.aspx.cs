using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;

public partial class Account_AnalysisMaster : System.Web.UI.Page
{
    Main Objmain = new Main(ConfigurationSettings.AppSettings["ConnectionString"].ToString());
    AnalysisMaster thehelper = new AnalysisMaster(ConfigurationSettings.AppSettings["ConnectionString"].ToString());
    protected void Page_Load(object sender, EventArgs e)
    {

        if (Session["userName"] == null)
        {
            Response.Redirect("../LoginPage.aspx");
        }
        if (Objmain.checkaccss(Session["CoCode"].ToString(), Session["UserRoleID"].ToString().Trim(), "ANALYSIS MASTER", checkAccessType.ViewAction) == false)
        {
            Response.Redirect("../AccessDenied.aspx");
        }

        if (Objmain.checkaccss(Session["CoCode"].ToString(), Session["UserRoleID"].ToString().Trim(), "ANALYSIS MASTER", checkAccessType.InsertAction) == false)
        {
            Button1.Enabled = false;
        }
        if (Objmain.checkaccss(Session["CoCode"].ToString(), Session["UserRoleID"].ToString().Trim(), "ANALYSIS MASTER", checkAccessType.DeleteAction) == false)
        {
            GridView1.Columns[5].Visible = false;
        }

        Page.Title = "Analysis Master";
        if (Session["userName"] == null)
        {
            Response.Redirect("../LoginPage.aspx");
        }
        GridFill();

        if (!IsPostBack)
        {
            Tab1Func();
            dropdownlisttype.DataSource = thehelper.AnalysisType();
            dropdownlisttype.DataTextField = "SelectText";
            dropdownlisttype.DataValueField = "SelectValue";
            dropdownlisttype.DataBind();
            dropdownlisttype.Items.Insert(0,new ListItem("Select","0"));
        }
     

    }
    private void GridFill()
    {
        GridView1.DataSource = thehelper.GridAnalysis(Session["CoCode"].ToString());
        GridView1.DataBind();
    }
    protected void Tab1_Click(object sender, EventArgs e)
    {
        Tab1Func();
    }

    public void Tab1Func()
    {
        Tab1.CssClass = "Clicked";
        Tab2.CssClass = "Initial";
        MainView.ActiveViewIndex = 0;
    }
    protected void Tab2_Click(object sender, EventArgs e)
    {
        Tab1.CssClass = "Initial";
        Tab2.CssClass = "Clicked";
        MainView.ActiveViewIndex = 1;
    }
    protected void Button1_Click(object sender, EventArgs e)
    {
        if (Button1.Text == "Submit")
        {
           
            if (thehelper.checkanal(txtanalysiscode.Text.Trim(), Session["CoCode"].ToString()) == false)
            {


                if (thehelper.InsertAnal(Session["CoCode"].ToString(), txtanalysiscode.Text.Trim(), txtanalysisname.Text.Trim(), Session["yearcode"].ToString(), Session["userName"].ToString(), dropdownlisttype.SelectedValue) == true)
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "msg", "alert('Inserted  Successfully !');", true);
                    ResetAllFields(); Tab1Func();
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "msg", "alert('Error in Inserted Data !');", true);
                }
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "msg", "alert('Anal Code Already Exists !');", true);
            }
        }

        else
            {

                if (thehelper.UpdateAnal(txtanalysiscode.Text.Trim(), txtanalysisname.Text.Trim(), dropdownlisttype.SelectedValue, Session["CoCode"].ToString()) == true)
                    {
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "msg", "alert('Updated  Successfully !');", true);
                    }
                    else
                    {
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "msg", "alert('Error in Updated Data !');", true);
                    }
                    Button1.Text = "Submit";
                   
                
            }
        }
    
    protected void Button2_Click(object sender, EventArgs e)
    {
        ResetAllFields();
    }
    private void ResetAllFields()
    {
        txtanalysiscode.Text = "";
        txtanalysisname.Text = "";
        dropdownlisttype.SelectedValue = "0";
        HiddenField1.Value = "";
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
            txtanalysiscode.ReadOnly = true;
            lblError.Text = "";
            int index = Convert.ToInt32(e.CommandArgument);
            Label lblID = (Label)GridView1.Rows[index].FindControl("lblcode");
            txtanalysiscode.Text = lblID.Text;

            Label lblname = (Label)GridView1.Rows[index].FindControl("lblname");
            txtanalysisname.Text = lblname.Text;

            Label lbltype = (Label)GridView1.Rows[index].FindControl("lbltype");
            dropdownlisttype.SelectedValue = lbltype.Text;
           // string[] splitname = lblname.Text.Split(' ');
         //   if (splitname.Length > 1)
          //      txtanalysiscode.Text = splitname[0];
           Tab1Func();
            Button1.Text = "Update";
            if (Objmain.checkaccss(Session["CoCode"].ToString(), Session["UserRoleID"].ToString().Trim(), "ANALYSIS MASTER", checkAccessType.UpdateAction) == false)
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
        thehelper.DeleteAnal(lblID.Text, Session["CoCode"].ToString());
        lblError.ForeColor = System.Drawing.Color.Green;
        lblError.Text = "Deleted Successfully";
        GridFill();
        ResetAllFields();
    }
    protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            if (Objmain.checkaccss(Session["CoCode"].ToString(), Session["UserRoleID"].ToString().Trim(), "ANALYSIS MASTER", checkAccessType.DeleteAction) == false)
            {
                code1.Visible = false;
                e.Row.Cells[5].Visible = false;
            }
        }
    }
}