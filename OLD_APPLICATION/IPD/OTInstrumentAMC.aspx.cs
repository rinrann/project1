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

public partial class IPD_OTInstrumentAMC : System.Web.UI.Page
{
    Main Objmain = new Main(ConfigurationSettings.AppSettings["ConnectionString"].ToString());
    IPDOTInstumrntAMC theHelper = new IPDOTInstumrntAMC(ConfigurationSettings.AppSettings["ConnectionString"].ToString());
    protected void Page_Load(object sender, EventArgs e)
    {
        Page.Title = "OT Instrument AMC";
        if (Session["userName"] == null)
        {
            Response.Redirect("../LoginPage.aspx");
        }
        if (Objmain.checkaccss(Session["CoCode"].ToString(), Session["UserRoleID"].ToString().Trim(), "OT INSTRUMENT AMC", checkAccessType.ViewAction) == false)
        {
            Response.Redirect("../AccessDenied.aspx");
        }

        if (Objmain.checkaccss(Session["CoCode"].ToString(), Session["UserRoleID"].ToString().Trim(), "OT INSTRUMENT AMC", checkAccessType.InsertAction) == false)
        {
            Button1.Enabled = false;
        }
        if (Objmain.checkaccss(Session["CoCode"].ToString(), Session["UserRoleID"].ToString().Trim(), "OT INSTRUMENT AMC", checkAccessType.DeleteAction) == false)
        {
            GridView1.Columns[8].Visible = false;
        }
        if (!IsPostBack)
        {
            GridFill();
            DropDownFill();
            Tab1Func();
        }
    }

    public void Tab1Func()
    {
        Tab1.CssClass = "Clicked";
        Tab2.CssClass = "Initial";
        MainView.ActiveViewIndex = 0;
        
    }
    private void GridFill()
    {
        GridView1.DataSource = theHelper.GetAllInstrumentAMC(DropDownList2.SelectedValue, TextBox3.Text.Trim(), Session["CoCode"].ToString().Trim());
        GridView1.DataBind();
    }
    private void ResetAllFields()
    {
        DropDownList1.SelectedIndex = 0; DropDownList3.SelectedIndex = -1;
   
        Calendar1.Text = "";
        Calendar2.Text = "";
        TextBox1.Text = "";
        TextBox2.Text = ""; TextBox4.Text = "";
        Button1.Text = "Submit";
        if (Objmain.checkaccss(Session["CoCode"].ToString(), Session["UserRoleID"].ToString().Trim(), "OT INSTRUMENT AMC", checkAccessType.InsertAction) == false)
        {
            Button1.Enabled = false;
        }
    }


    private void DropDownFill()
    {
        this.DropDownList1.DataSource = theHelper.DropdownInstrument(Session["CoCode"].ToString().Trim());
        this.DropDownList1.DataTextField = "InstrumentName";
        this.DropDownList1.DataValueField = "RowID";
        this.DropDownList1.DataBind();
        this.DropDownList1.Items.Insert(0, new ListItem("--Select--", "0"));

        this.DropDownList2.DataSource = theHelper.DropdownInstrument(Session["CoCode"].ToString().Trim());
        this.DropDownList2.DataTextField = "InstrumentName";
        this.DropDownList2.DataValueField = "RowID";
        this.DropDownList2.DataBind();
        this.DropDownList2.Items.Insert(0, new ListItem("--All--", "0"));
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
            Label RowId = (Label)GridView1.Rows[index].FindControl("RowId");
            HiddenField1.Value = RowId.Text;

             Label AMCpaidDate = (Label)GridView1.Rows[index].FindControl("AMCpaidDate");
             Calendar2.Text = AMCpaidDate.Text;     

             Label InstrumentId = (Label)GridView1.Rows[index].FindControl("InstrumentId");
             DropDownList1.SelectedValue = InstrumentId.Text;
             FillModel(InstrumentId.Text);


             Label AMCPrice = (Label)GridView1.Rows[index].FindControl("AMCPrice");
             TextBox2.Text = AMCPrice.Text;

             Label ModelNo = (Label)GridView1.Rows[index].FindControl("ModelNo");
             DropDownList3.SelectedValue = ModelNo.Text;

             Label Comment = (Label)GridView1.Rows[index].FindControl("Comment");
             TextBox4.Text = Comment.Text;
            
             CallFunction(ModelNo.Text);
             Tab1Func();
            Button1.Text = "Update";
            if (Objmain.checkaccss(Session["CoCode"].ToString(), Session["UserRoleID"].ToString().Trim(), "OT INSTRUMENT AMC", checkAccessType.UpdateAction) == false)
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
        Label RowId = (Label)GridView1.Rows[e.RowIndex].FindControl("RowId");
        theHelper.DeleteOTInstruments(RowId.Text);
        ScriptManager.RegisterStartupScript(this, this.GetType(), "msg", "alert('Deleted  Successfully !');", true);
        GridFill();
        ResetAllFields();

    }
    protected void Button1_Click(object sender, EventArgs e)
    {
        string reformattedDate1 = string.Empty;
        System.Globalization.DateTimeFormatInfo dtf = new DateTimeFormatInfo();
        DateTime testdate2 = DateTime.ParseExact(Calendar2.Text, "dd/MM/yyyy", dtf);
        if(TextBox2.Text=="")
        TextBox2.Text="0.00";


        if (Button1.Text == "Submit")
        {

            if (theHelper.InsertOTInstruments(TextBox2.Text, TextBox4.Text, DropDownList3.SelectedValue, DropDownList1.SelectedValue, testdate2.ToString("yyyy-MM-dd"), Session["CoCode"].ToString().Trim(), Session["userName"].ToString()) == true)
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
            if (theHelper.UpdateOTInstruments(TextBox2.Text, TextBox4.Text, HiddenField1.Value, DropDownList3.SelectedValue, DropDownList1.SelectedValue, testdate2.ToString("yyyy-MM-dd"), Session["CoCode"].ToString().Trim(), Session["userName"].ToString()) == true)
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "msg", "alert('Updated  Successfully !');", true);
                ResetAllFields();
                Button1.Text = "Submit";
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "msg", "alert('Error in Updated Data !');", true);
            }
        }


        GridFill();

    }
    protected void Button2_Click(object sender, EventArgs e)
    {
        ResetAllFields();

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

    public void FillModel(string insid)
    {
        this.DropDownList3.DataSource = theHelper.GetModel(insid, Session["CoCode"].ToString().Trim()); 
        this.DropDownList3.DataTextField = "ModelNo";
        this.DropDownList3.DataValueField = "ModelNo";
        this.DropDownList3.DataBind();
        this.DropDownList3.Items.Insert(0, new ListItem("--Select--", "0"));
    }
    protected void DropDownList1_SelectedIndexChanged(object sender, EventArgs e)
    {
       
        FillModel(DropDownList1.SelectedValue);
    }

    public void CallFunction(string id)
    {
        DataTable dt = theHelper.GetAllInstrument(id);
        if (dt.Rows.Count > 0)
        {
            Calendar1.Text = dt.Rows[0]["pdate"].ToString();
            TextBox1.Text = dt.Rows[0]["MName"].ToString();
            TextBox2.Text = dt.Rows[0]["PurchasePrice"].ToString();
        }
    }
    protected void Button3_Click(object sender, EventArgs e)
    {
        GridFill();
    }
    protected void DropDownList3_SelectedIndexChanged(object sender, EventArgs e)
    {
        CallFunction(DropDownList3.SelectedValue);
    }
    protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            if (Objmain.checkaccss(Session["CoCode"].ToString(), Session["UserRoleID"].ToString().Trim(), "OT INSTRUMENT AMC", checkAccessType.DeleteAction) == false)
            {
                e.Row.Cells[8].Visible = false;
            }
        }
    }
}