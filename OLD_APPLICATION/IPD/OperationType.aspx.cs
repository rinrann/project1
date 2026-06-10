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
 
public partial class Master_OperationType : System.Web.UI.Page
{
    Main Objmain = new Main(ConfigurationSettings.AppSettings["ConnectionString"].ToString());
    OperationType theHelper = new OperationType(ConfigurationSettings.AppSettings["ConnectionString"].ToString());
    protected void Page_Load(object sender, EventArgs e)
    {
        Page.Title = "Operation Type";
        if (Session["userName"] == null)
        {
            Response.Redirect("../LoginPage.aspx");
        }
        if (Objmain.checkaccss(Session["CoCode"].ToString(), Session["UserRoleID"].ToString().Trim(), "OPERATION TYPE", checkAccessType.ViewAction) == false)
        {
            Response.Redirect("../AccessDenied.aspx");
        }

        if (Objmain.checkaccss(Session["CoCode"].ToString(), Session["UserRoleID"].ToString().Trim(), "OPERATION TYPE", checkAccessType.InsertAction) == false)
        {
            Button1.Enabled = false;
        }
        if (!IsPostBack)
        {
            GridFill();
            Tab1Func();
            txtOpeTypeId.Text = theHelper.GetOperationTypeID(Session["CoCode"].ToString().Trim()).ToString();
        }
    }
    private void GridFill()
    {
        GridView1.DataSource = theHelper.GetAllOperationType(Session["CoCode"].ToString().Trim());
        GridView1.DataBind();
    }
    private void ResetAllFields()
    {
        txtOpeTypeId.Text = "";
        txtOpeTypeName.Text = "";
        Button1.Text = "Submit";
        txtOpeTypeId.Text = theHelper.GetOperationTypeID(Session["CoCode"].ToString().Trim()).ToString();
        if (Objmain.checkaccss(Session["CoCode"].ToString(), Session["UserRoleID"].ToString().Trim(), "OPERATION TYPE", checkAccessType.InsertAction) == false)
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

            Label lblOperationTypeID = (Label)GridView1.Rows[index].FindControl("OperationTypeID");
            txtOpeTypeId.Text = lblOperationTypeID.Text;
            HiddenField1.Value = lblOperationTypeID.Text;

            Label lblOperationTypeName = (Label)GridView1.Rows[index].FindControl("OperationTypeName");
            txtOpeTypeName.Text = lblOperationTypeName.Text;
            Tab1Func();
            Button1.Text = "Update";
            if (Objmain.checkaccss(Session["CoCode"].ToString(), Session["UserRoleID"].ToString().Trim(), "OPERATION TYPE", checkAccessType.UpdateAction) == false)
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
         Label lblOperationTypeID = (Label)GridView1.Rows[e.RowIndex].FindControl("OperationTypeID");
         theHelper.DeleteOperationType(Convert.ToInt32(lblOperationTypeID.Text), Session["CoCode"].ToString());
        ScriptManager.RegisterStartupScript(this, this.GetType(), "msg", "alert('Deleted  Successfully !');", true);
        GridFill();
        ResetAllFields();
        txtOpeTypeId.Text = theHelper.GetOperationTypeID(Session["CoCode"].ToString().Trim()).ToString();
    }
    protected void Button1_Click(object sender, EventArgs e)
    {
           if (Button1.Text == "Submit")
        {
            theHelper.InsertOperationType(txtOpeTypeName.Text, Session["userName"].ToString(), Session["CoCode"].ToString().Trim());
            ScriptManager.RegisterStartupScript(this, this.GetType(), "msg", "alert('Inserted  Successfully !');", true);
        }
        else
        {
            theHelper.UpdateOperationType(HiddenField1.Value, txtOpeTypeName.Text, Session["CoCode"].ToString().Trim());
            ScriptManager.RegisterStartupScript(this, this.GetType(), "msg", "alert('Updated  Successfully !');", true);
            Button1.Text = "Submit";
        }

           GridView1.DataSource = theHelper.GetAllOperationType(Session["CoCode"].ToString().Trim());
        GridView1.DataBind();
        ResetAllFields();
        txtOpeTypeId.Text = theHelper.GetOperationTypeID(Session["CoCode"].ToString().Trim()).ToString();
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
    public static List<string> Searchottype(string prefixText, int count)
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
                cmd.CommandText = "select distinct OperationTypeName as Name from IPD_OperationType where compcode=@Compcode and OperationTypeName like @SearchText +'%'";
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
            if (Objmain.checkaccss(Session["CoCode"].ToString(), Session["UserRoleID"].ToString().Trim(), "OPERATION TYPE", checkAccessType.DeleteAction) == false)
            {
                coldel.Visible = false;
                e.Row.Cells[3].Visible = false;
            }

        }
    }
}