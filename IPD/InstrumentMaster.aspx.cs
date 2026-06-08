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
using System.Globalization;
using System.Collections.Generic;

public partial class IPD_InstrumentMaster : System.Web.UI.Page
{
    Main Objmain = new Main(ConfigurationSettings.AppSettings["ConnectionString"].ToString());
    InstrumentMaster theaddConsumable = new InstrumentMaster(ConfigurationSettings.AppSettings["ConnectionString"].ToString());
    protected void Page_Load(object sender, EventArgs e)
    {
        Page.Title = "Instrument Master";
       
        if (Session["userName"] == null)
        {
            Response.Redirect("../LoginPage.aspx");
        }
        if (Objmain.checkaccss(Session["CoCode"].ToString(), Session["UserRoleID"].ToString().Trim(), "OT INSTRUMENT MASTER", checkAccessType.ViewAction) == false)
        {
            Response.Redirect("../AccessDenied.aspx");
        }
        if (Objmain.checkaccss(Session["CoCode"].ToString(), Session["UserRoleID"].ToString().Trim(), "OT INSTRUMENT MASTER", checkAccessType.InsertAction) == false)
        {
            Button1.Enabled = false;
        }
        
        if (!IsPostBack)
        {
            
            Tab1Func();
            DropDownFill();
            GridFill("0", "0", DropDownList6.SelectedValue);
            DropDownList2.Enabled = false;
            DropDownList4.Enabled = false;
            TextBox2.Text = theaddConsumable.GetInstrumentID(Session["CoCode"].ToString().Trim()).ToString();
           
        }
    }
  

    private void GridFill(string cat,string subcat,string type)
    {
        GridView1.DataSource = theaddConsumable.GetAllInstrument(cat, subcat, Session["CoCode"].ToString().Trim(),type);
        GridView1.DataBind();
    }
    protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            if (Objmain.checkaccss(Session["CoCode"].ToString(), Session["UserRoleID"].ToString().Trim(), "OT INSTRUMENT MASTER", checkAccessType.DeleteAction) == false)
            {
                coldel.Visible = false;
                e.Row.Cells[10].Visible = false;
            }

        }
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            Label lblTypeNm = (Label)e.Row.FindControl("lblTypeNm");
            Label lblType = (Label)e.Row.FindControl("lblType");
            string type = lblType.Text;
            if (type == "S")
            {
                lblTypeNm.Text = "Service Instrument";
            }
            else
            {
                lblTypeNm.Text = "OT Instrument";
            }
        }
    }

    private void ResetAllFields()
    {
        DropDownList1.SelectedIndex = -1;
        TextBox1.Text = "";
        DropDownList2.SelectedIndex = -1;
        HiddenField1.Value = "";
        TextBox2.Text = theaddConsumable.GetInstrumentID(Session["CoCode"].ToString().Trim()).ToString();
        Button1.Text = "Submit";
        TextBox3.Text = "";

        if (Objmain.checkaccss(Session["CoCode"].ToString(), Session["UserRoleID"].ToString().Trim(), "OT INSTRUMENT MASTER", checkAccessType.InsertAction) == false)
        {
            Button1.Enabled = false;
        }
    }
    protected void Button1_Click(object sender, EventArgs e)
    {
        string cost="0.00";
        if(TextBox3.Text=="")
        {
            cost="0.00";
        }
        else
        {
            cost=TextBox3.Text;
        }

        if (Button1.Text == "Submit")
        {

            if (theaddConsumable.InsertInstrumentMaster(DropDownList5.SelectedValue.Trim(),DropDownList2.SelectedValue, TextBox2.Text, DropDownList1.SelectedValue, TextBox1.Text.ToUpper(), Session["CoCode"].ToString().Trim(), Session["userName"].ToString(),cost) == true)
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "msg", "alert('Inserted Successfully !');", true);
                ResetAllFields();
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "msg", "alert('Error in Inserted Data !');", true);
            }
        }
        else
        {
            if (theaddConsumable.UpdateInstrumentMaster(DropDownList5.SelectedValue.Trim(),DropDownList2.SelectedValue, HiddenField1.Value, DropDownList1.SelectedValue, TextBox1.Text.ToUpper(), Session["CoCode"].ToString().Trim(), Session["userName"].ToString(),cost) == true)
            {
                 ScriptManager.RegisterStartupScript(this, this.GetType(), "msg", "alert('Updated Successfully !');", true);
                Button1.Text = "Submit";

            }
            else
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "msg", "alert(' Error in Updated Data !');", true);
            }
        }
        GridFill("0", "0", DropDownList6.SelectedValue);
        ResetAllFields();
    }
    protected void Button2_Click(object sender, EventArgs e)
    {
        ResetAllFields();
    }
    private void DropDownFill()
    {
        this.DropDownList1.DataSource = theaddConsumable.DropDownType(Session["CoCode"].ToString().Trim());
        this.DropDownList1.DataTextField = "TypeName";
        this.DropDownList1.DataValueField = "TypeId";
        this.DropDownList1.DataBind();
        this.DropDownList1.Items.Insert(0, new ListItem("--Select--", "0"));

        this.DropDownList2.Items.Insert(0, new ListItem("--Select--", "0"));

        this.DropDownList4.Items.Insert(0, new ListItem("--Select--", "0"));

        this.DropDownList3.DataSource = theaddConsumable.DropDownType(Session["CoCode"].ToString().Trim());
        this.DropDownList3.DataTextField = "TypeName";
        this.DropDownList3.DataValueField = "TypeId";
        this.DropDownList3.DataBind();
        this.DropDownList3.Items.Insert(0, new ListItem("--Select--", "0"));

        this.DropDownList5.Items.Insert(0, new ListItem("OT Instrument", "T"));
        this.DropDownList5.Items.Insert(1, new ListItem("Service Instrument", "S"));

        this.DropDownList6.Items.Insert(0, new ListItem("OT Instrument", "T"));
        this.DropDownList6.Items.Insert(1, new ListItem("Service Instrument", "S"));
    }
    protected void GridView1_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        GridView1.PageIndex = e.NewPageIndex;
        GridFill("0","0","0");
    }
    protected int SearchIndex(string Value, DropDownList ddl)
    {
        int i;
        for (i = 0; i < ddl.Items.Count; i++)
        {
            if (ddl.Items[i].Value.Trim() == Value.Trim())
                return i;
        }
        return -1;
    }

    protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "Select")
        {
            int index = Convert.ToInt32(e.CommandArgument);
            lblError.Text = "";
            Label lblid = (Label)GridView1.Rows[index].FindControl("lblid");
            TextBox2.Text = lblid.Text;
            HiddenField1.Value = lblid.Text;

            Label lblInstrumentName = (Label)GridView1.Rows[index].FindControl("lblInstrumentName");
            TextBox1.Text = lblInstrumentName.Text;

            Label lblType = (Label)GridView1.Rows[index].FindControl("lblType");
            DropDownList5.SelectedValue = lblType.Text.Trim();


            FillSubCategory("0",DropDownList2);
            Label SubCategoryId = (Label)GridView1.Rows[index].FindControl("SubCategoryId");
            DropDownList2.SelectedValue = SubCategoryId.Text;

            Label lblTypeid = (Label)GridView1.Rows[index].FindControl("lblTypeid");
            if (lblTypeid.Text == "1")
                DropDownList2.Enabled = false;
            else
            {
                DropDownList2.Enabled = true;
            }
            DropDownList1.SelectedIndex = SearchIndex(lblTypeid.Text, DropDownList1);

            Tab1Func();
            Button1.Text = "Update";

            if (Objmain.checkaccss(Session["CoCode"].ToString(), Session["UserRoleID"].ToString().Trim(), "OT INSTRUMENT MASTER", checkAccessType.UpdateAction) == false)
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
         
        lblError.Text = "";
         Label lblid = (Label)GridView1.Rows[e.RowIndex].FindControl("lblid");
         if (theaddConsumable.DeleteInstrumentMaster(lblid.Text, Session["CoCode"].ToString().Trim()) == true)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "msg", "alert('Deleted Successfully !');", true);
        }
        else
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "msg", "alert('Error in Deleted Data !');", true);
        }
         GridFill("0", "0", DropDownList6.SelectedValue);
    }

    protected void DropDownList1_SelectedIndexChanged(object sender, EventArgs e)
    {
        string a;
        a = DropDownList1.SelectedValue;
        FillSubCategory(a,DropDownList2);
    }

    public void FillSubCategory(string a,DropDownList ddl)
    {
       /* if (a == "1")
            ddl.Enabled = false;
        else
        {*/
            ddl.Enabled = true;
            ddl.DataSource = theaddConsumable.DropDownSubCategory(Session["CoCode"].ToString().Trim(),a);
            ddl.DataTextField = "SubCategoryName";
            ddl.DataValueField = "SubCategoryId";
            ddl.DataBind();
            ddl.Items.Insert(0, new ListItem("--Select--", "0"));
        /*}*/
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
    protected void DropDownList3_SelectedIndexChanged(object sender, EventArgs e)
    {
        string a;
        a = DropDownList3.SelectedValue;
        FillSubCategory(a, DropDownList4);
    }
    protected void Button3_Click(object sender, EventArgs e)
    {
        GridFill(DropDownList3.SelectedValue, DropDownList4.SelectedValue, DropDownList6.SelectedValue);
    }

    [System.Web.Script.Services.ScriptMethod()]
    [System.Web.Services.WebMethod]
    public static List<string> SearchIns(string prefixText, int count)
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
                cmd.CommandText = "select distinct InstrumentName as Name from OT_InstrumentMaster where compcode=@Compcode and InstrumentName like @SearchText +'%'";
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
}