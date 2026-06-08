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

public partial class IPD_OTInstrumentRepair : System.Web.UI.Page
{
    Main Objmain = new Main(ConfigurationSettings.AppSettings["ConnectionString"].ToString());
    OTInstrumentRepair theHelper = new OTInstrumentRepair(ConfigurationSettings.AppSettings["ConnectionString"].ToString());
    protected void Page_Load(object sender, EventArgs e)
    {
        Page.Title = "OT Instrument Repair";
        if (Session["userName"] == null)
        {
            Response.Redirect("../LoginPage.aspx");
        }
        if (Objmain.checkaccss(Session["CoCode"].ToString(), Session["UserRoleID"].ToString().Trim(), "OT INSTRUMENT REPAIR", checkAccessType.ViewAction) == false)
        {
            Response.Redirect("../AccessDenied.aspx");
        }

        if (Objmain.checkaccss(Session["CoCode"].ToString(), Session["UserRoleID"].ToString().Trim(), "OT INSTRUMENT REPAIR", checkAccessType.InsertAction) == false)
        {
            Button1.Enabled = false;
        }
        if (Objmain.checkaccss(Session["CoCode"].ToString(), Session["UserRoleID"].ToString().Trim(), "OT INSTRUMENT REPAIR", checkAccessType.DeleteAction) == false)
        {
            GridView1.Columns[9].Visible = false;
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
        GridView1.DataSource = theHelper.GetAllInstrumentRepair(DropDownList5.SelectedValue, DropDownList2.SelectedValue, TextBox3.Text.Trim(), Session["CoCode"].ToString().Trim());
        GridView1.DataBind();
    }
    private void ResetAllFields()
    {
        DropDownList1.SelectedIndex = 0; DropDownList3.SelectedIndex = -1;

        Calendar1.Text = "";
        Calendar2.Text = "";
        TextBox1.Text = "";
        TextBox2.Text = "";
        Button1.Text = "Submit";
        if (Objmain.checkaccss(Session["CoCode"].ToString(), Session["UserRoleID"].ToString().Trim(), "OT INSTRUMENT REPAIR", checkAccessType.InsertAction) == false)
        {
            Button1.Enabled = false;
        }
    }


    private void DropDownFill()
    {

        this.DropDownList1.Items.Insert(0, new ListItem("--Select--", "0"));
        this.DropDownList3.Items.Insert(0, new ListItem("--Select--", "0"));

        this.DropDownList2.Items.Insert(0, new ListItem("--All--", "0"));



        this.DropDownList5.DataSource = theHelper.DropdownInstrumentTye();
        this.DropDownList5.DataTextField = "TypeName";
        this.DropDownList5.DataValueField = "TypeId";
        this.DropDownList5.DataBind();
        this.DropDownList5.Items.Insert(0, new ListItem("--Select--", "0"));

        this.DropDownList4.DataSource = theHelper.DropdownInstrumentTye();
        this.DropDownList4.DataTextField = "TypeName";
        this.DropDownList4.DataValueField = "TypeId";
        this.DropDownList4.DataBind();
        this.DropDownList4.Items.Insert(0, new ListItem("--Select--", "0"));

   
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

            Label rdate = (Label)GridView1.Rows[index].FindControl("rdate");
            Calendar2.Text = rdate.Text;


            Label RepairCost = (Label)GridView1.Rows[index].FindControl("RepairCost");
            TextBox4.Text = RepairCost.Text;

            Label TypeID = (Label)GridView1.Rows[index].FindControl("TypeID");
            DropDownList4.SelectedValue = TypeID.Text;

            Label InstrumentId = (Label)GridView1.Rows[index].FindControl("NameId");
            FillInstrument("0");
            DropDownList1.SelectedValue = InstrumentId.Text;

            if (TypeID.Text == "1")
            {
                FillModel("0");
                Label ModelNo = (Label)GridView1.Rows[index].FindControl("ModelNo");
                DropDownList3.SelectedValue = ModelNo.Text;
                CallFunction(ModelNo.Text);
            }
            else
            {
                CallFunction(InstrumentId.Text);
            }
            Tab1Func();
            Tab1Func();
            Button1.Text = "Update";
            if (Objmain.checkaccss(Session["CoCode"].ToString(), Session["UserRoleID"].ToString().Trim(), "OT INSTRUMENT REPAIR", checkAccessType.UpdateAction) == false)
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
        theHelper.DeleteOTInstrumentsRepair(lblInstrumentID.Text);
        ScriptManager.RegisterStartupScript(this, this.GetType(), "msg", "alert('Deleted  Successfully !');", true);
        GridFill();
        ResetAllFields();

    }
    protected void Button1_Click(object sender, EventArgs e)
    {
        string reformattedDate1 = string.Empty;
        System.Globalization.DateTimeFormatInfo dtf = new DateTimeFormatInfo();
        DateTime testdate2 = DateTime.ParseExact(Calendar2.Text, "dd/MM/yyyy", dtf);

        if (Button1.Text == "Submit")
        {

            if (theHelper.InsertOTInstrumentsRepair(DropDownList1.SelectedValue, DropDownList3.SelectedValue, testdate2.ToString(),TextBox4.Text,Session["userName"].ToString()) == true)
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
            if (theHelper.UpdateOTInstrumentsRepair(HiddenField1.Value, DropDownList1.SelectedValue, DropDownList3.SelectedValue, testdate2.ToString(), TextBox4.Text) == true)
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "msg", "alert('Updated  Successfully !');", true);
                ResetAllFields();
                Button1.Text = "Submit";
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "msg", "alert('Error in Update Data !');", true);
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
        this.DropDownList3.DataSource = theHelper.GetModel(insid);
        this.DropDownList3.DataTextField = "ModelNo";
        this.DropDownList3.DataValueField = "ModelNo";
        this.DropDownList3.DataBind();
        this.DropDownList3.Items.Insert(0, new ListItem("--Select--", "0"));
    }
    protected void DropDownList1_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (DropDownList4.SelectedValue == "1")
        {
            FillModel(DropDownList1.SelectedValue);
        }
        else
        {
            CallFunction(DropDownList1.SelectedValue);
        }
    }

    public void CallFunction(string id)
    {
        DataTable dt = theHelper.GetAllInstrument(id, DropDownList4.SelectedValue);
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


    public void FillInstrument(string type)
    {
        this.DropDownList1.DataSource = theHelper.DropdownInstrument(type);
        this.DropDownList1.DataTextField = "InstrumentName";
        this.DropDownList1.DataValueField = "RowID";
        this.DropDownList1.DataBind();
        this.DropDownList1.Items.Insert(0, new ListItem("--Select--", "0"));
    }

    protected void DropDownList4_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (DropDownList4.SelectedValue == "1")
        {
            DropDownList3.Enabled = true;
            FillInstrument(DropDownList4.SelectedValue);
        }
        else
        {
            DropDownList3.Enabled = false;
            FillInstrument(DropDownList4.SelectedValue);

        }
    }
    protected void DropDownList5_SelectedIndexChanged(object sender, EventArgs e)
    {
        this.DropDownList2.DataSource = theHelper.DropdownInstrument(DropDownList5.SelectedValue);
        this.DropDownList2.DataTextField = "InstrumentName";
        this.DropDownList2.DataValueField = "RowID";
        this.DropDownList2.DataBind();
        this.DropDownList2.Items.Insert(0, new ListItem("--All--", "0"));
    }
    protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            if (Objmain.checkaccss(Session["CoCode"].ToString(), Session["UserRoleID"].ToString().Trim(), "OT INSTRUMENT REPAIR", checkAccessType.DeleteAction) == false)
            {
                e.Row.Cells[9].Visible = false;
            }
        }
    }
}