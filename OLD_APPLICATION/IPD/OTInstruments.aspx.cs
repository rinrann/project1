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
 
public partial class Master_OTInstruments : System.Web.UI.Page
{
    Main Objmain = new Main(ConfigurationSettings.AppSettings["ConnectionString"].ToString());
    OTInstruments theHelper = new OTInstruments(ConfigurationSettings.AppSettings["ConnectionString"].ToString());
    protected void Page_Load(object sender, EventArgs e)
    {
        Page.Title = "OT Instruments Purchase";
        if (Session["userName"] == null)
        {
            Response.Redirect("../LoginPage.aspx");
        }
        if (Objmain.checkaccss(Session["CoCode"].ToString(), Session["UserRoleID"].ToString().Trim(), "OT INSTRUMENT PURCHASE", checkAccessType.ViewAction) == false)
        {
            Response.Redirect("../AccessDenied.aspx");
        }

        if (Objmain.checkaccss(Session["CoCode"].ToString(), Session["UserRoleID"].ToString().Trim(), "OT INSTRUMENT PURCHASE", checkAccessType.InsertAction) == false)
        {
            Button1.Enabled = false;
        }
        if (Objmain.checkaccss(Session["CoCode"].ToString(), Session["UserRoleID"].ToString().Trim(), "OT INSTRUMENT PURCHASE", checkAccessType.DeleteAction) == false)
        {
            GridView1.Columns[17].Visible = false;
        }
        if (!IsPostBack)
        {
            Tab1Func();
            GridFill();
            DropDownFill();
            TextFill();
           
        }
    }

    public void TextFill()
    {
        TextBox3.Enabled = false;
        txtAmcPrice.Enabled = false;
        Calendar2.Enabled = false;
        Calendar3.Enabled = false;
    }
    private void GridFill()
    {
        GridView1.DataSource = theHelper.GetAllInstrument(Session["CoCode"].ToString().Trim());
        GridView1.DataBind();
    }
    private void ResetAllFields()
    {

        DropDownList2.SelectedIndex = 0; 
        DropDownList3.SelectedIndex = 0; 
        DropDownList4.SelectedIndex = 0;
        DropDownList1.SelectedIndex = 0;
        txtPurPrice.Text = "";
        txtAmcPrice.Text = ""; 
        Calendar1.Text = "";
        Calendar2.Text = "";
        Calendar3.Text = "";
        TextBox1.Text = ""; TextBox3.Text = "";
        TextBox2.Text = "";
        Button1.Text = "Submit";
        if (Objmain.checkaccss(Session["CoCode"].ToString(), Session["UserRoleID"].ToString().Trim(), "OT INSTRUMENT PURCHASE", checkAccessType.InsertAction) == false)
        {
            Button1.Enabled = false;
        }
    }


    private void DropDownFill()
    {
        this.DropDownList1.DataSource = theHelper.DropdownCompany(Session["CoCode"].ToString().Trim());
        this.DropDownList1.DataTextField = "MName";
        this.DropDownList1.DataValueField = "MCode";
        this.DropDownList1.DataBind();
        this.DropDownList1.Items.Insert(0, new ListItem("--Select--", "0"));



        this.DropDownList2.DataSource = theHelper.DropdownInstrumentTye(Session["CoCode"].ToString().Trim());
        this.DropDownList2.DataTextField = "TypeName";
        this.DropDownList2.DataValueField = "TypeId";
        this.DropDownList2.DataBind();
        this.DropDownList2.Items.Insert(0, new ListItem("--Select--", "0"));
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
         
            Label lblInstrumentID = (Label)GridView1.Rows[index].FindControl("InstrumentID");
            HiddenField1.Value = lblInstrumentID.Text;


            Label InstrumentTypeId = (Label)GridView1.Rows[index].FindControl("InstrumentTypeId");
            DropDownList2.SelectedValue = InstrumentTypeId.Text;
            callType(InstrumentTypeId.Text);


            Label InstrumentNameid = (Label)GridView1.Rows[index].FindControl("InstrumentNameid");
            DropDownList3.SelectedValue = InstrumentNameid.Text;

            callFunction("0");
            Label ManufacturingCompanyid = (Label)GridView1.Rows[index].FindControl("ManufacturingCompanyid");
            DropDownList1.SelectedValue = ManufacturingCompanyid.Text;

            Label lblPurchasePrice = (Label)GridView1.Rows[index].FindControl("PurchasePrice");
            txtPurPrice.Text = lblPurchasePrice.Text;


            Label ModelNo = (Label)GridView1.Rows[index].FindControl("ModelNo");
            TextBox3.Text = ModelNo.Text;

            Label lblAMCPrice = (Label)GridView1.Rows[index].FindControl("AMCPrice");
            txtAmcPrice.Text = lblAMCPrice.Text;

            //Label lblCurrentStatus = (Label)GridView1.Rows[index].FindControl("CurrentStatus");
            //txtCrntSts.Text = lblCurrentStatus.Text;

            Label lblPurchaseDate = (Label)GridView1.Rows[index].FindControl("PurchaseDate");
            Calendar1.Text = lblPurchaseDate.Text;

            Label lblAMCCommencementDate = (Label)GridView1.Rows[index].FindControl("AMCCommencementDate");
            Calendar2.Text = lblAMCCommencementDate.Text;

            Label lblAMCCompletionDate = (Label)GridView1.Rows[index].FindControl("AMCCompletionDate");
            Calendar3.Text = lblAMCCompletionDate.Text;


            Label BillNo = (Label)GridView1.Rows[index].FindControl("BillNo");
            TextBox2.Text = BillNo.Text;

            Label Quantity = (Label)GridView1.Rows[index].FindControl("Quantity");
            TextBox1.Text = Quantity.Text;
            Tab1Func();
            Button1.Text = "Update";
            if (Objmain.checkaccss(Session["CoCode"].ToString(), Session["UserRoleID"].ToString().Trim(), "OT INSTRUMENT PURCHASE", checkAccessType.UpdateAction) == false)
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
          Label lblInstrumentID = (Label)GridView1.Rows[e.RowIndex].FindControl("InstrumentID");
          theHelper.DeleteOTInstruments(lblInstrumentID.Text, Session["CoCode"].ToString().Trim());
        ScriptManager.RegisterStartupScript(this, this.GetType(), "msg", "alert('Deleted  Successfully !');", true);
        GridFill();
        ResetAllFields();
       
    }
    protected void Button1_Click(object sender, EventArgs e)
    {
        string reformattedDate1 = string.Empty;
        string purchasedate = "";
        string commencementdate = "";
        string completiondate = "";
        if (txtAmcPrice.Text == "")
            txtAmcPrice.Text = "0.00";
        System.Globalization.DateTimeFormatInfo dtf = new DateTimeFormatInfo();
        if (Calendar1.Text != "")
        {
            DateTime testdate = DateTime.ParseExact(Calendar1.Text, "dd/MM/yyyy", dtf);
            purchasedate = testdate.ToString("yyyy-MM-dd");
        }

        if (Calendar2.Text != "")
        {
            DateTime testdate2 = DateTime.ParseExact(Calendar2.Text, "dd/MM/yyyy", dtf);
            commencementdate = testdate2.ToString("yyyy-MM-dd");
        }

        if (Calendar3.Text != "")
        {
            DateTime testdate3 = DateTime.ParseExact(Calendar3.Text, "dd/MM/yyyy", dtf);
            completiondate = testdate3.ToString("yyyy-MM-dd");
        }
        
        if (Button1.Text == "Submit")
        {

            if (theHelper.InsertOTInstruments(DropDownList2.SelectedValue, TextBox3.Text, TextBox2.Text, TextBox1.Text, DropDownList3.SelectedValue, DropDownList1.SelectedValue, txtPurPrice.Text, txtAmcPrice.Text, purchasedate, commencementdate, completiondate, Session["userName"].ToString(), Session["CoCode"].ToString().Trim()) == true)
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "msg", "alert('Inserted  Successfully !');", true);
                ResetAllFields();
            }
            else
            {

                ScriptManager.RegisterStartupScript(this, this.GetType(), "msg", "alert('Error in inserted Data !');", true);
            }
        }
        else
        {
            if (theHelper.UpdateOTInstruments(DropDownList2.SelectedValue, TextBox3.Text, TextBox2.Text, TextBox1.Text, HiddenField1.Value, DropDownList3.SelectedValue, DropDownList1.SelectedValue, txtPurPrice.Text, txtAmcPrice.Text, purchasedate, commencementdate, completiondate, Session["CoCode"].ToString().Trim(), Session["userName"].ToString()) == true)
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

 public void callType(string id)
    {
        if (id == "1")
        {
            TextBox1.Text = "1";
            TextBox3.Enabled = true;
            txtAmcPrice.Enabled = true;
            Calendar2.Enabled = true;
            Calendar3.Enabled = true;
        }
        else
        {
            TextBox1.Text = "";
            TextBox3.Text = "";
            txtAmcPrice.Text = "";
            Calendar2.Text = "";
            Calendar3.Text = "";

            TextBox3.Enabled = false;
            txtAmcPrice.Enabled = false;
            Calendar2.Enabled = false;
            Calendar3.Enabled = false;
        }
    }
    protected void DropDownList2_SelectedIndexChanged(object sender, EventArgs e)
    {
        callType(DropDownList2.SelectedValue);
        callFunction(DropDownList2.SelectedValue);
    }

    public void callFunction(string id)
    {
        this.DropDownList3.DataSource = theHelper.DropdownInstrumentName(Session["CoCode"].ToString().Trim(),id);
        this.DropDownList3.DataTextField = "InstrumentName";
        this.DropDownList3.DataValueField = "RowID";
        this.DropDownList3.DataBind();
        this.DropDownList3.Items.Insert(0, new ListItem("--Select--", "0"));
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
            if (Objmain.checkaccss(Session["CoCode"].ToString(), Session["UserRoleID"].ToString().Trim(), "OT INSTRUMENT PURCHASE", checkAccessType.DeleteAction) == false)
            {
                e.Row.Cells[17].Visible = false;
            }
        }
    }
}