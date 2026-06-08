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
using System.Web.Security;
 
public partial class IPD_OTList : System.Web.UI.Page
{
    Main Objmain = new Main(ConfigurationSettings.AppSettings["ConnectionString"].ToString());
    OTList theOTList = new OTList(ConfigurationSettings.AppSettings["ConnectionString"].ToString());
    protected void Page_Load(object sender, EventArgs e)
    {
        Page.Title = "OT Patient List";
        if (Session["userName"] == null)
        {
            Response.Redirect("../LoginPage.aspx");
        }

        if (Objmain.checkaccss(Session["CoCode"].ToString(), Session["UserRoleID"].ToString().Trim(), "OT PATIENT LIST", checkAccessType.ViewAction) == false)
        {
            Response.Redirect("../AccessDenied.aspx");
        }
        if (!IsPostBack)
        {
            DropDownFill();
            GridFill();
        }
    }

    public void DropDownFill()
    {
        this.DropDownList1.DataSource = theOTList.Dropdownottype(Session["CoCode"].ToString().Trim());
        this.DropDownList1.DataTextField = "OperationTypeName";
        this.DropDownList1.DataValueField = "OperationTypeID";
        this.DropDownList1.DataBind();
        this.DropDownList1.Items.Insert(0, new ListItem("--Select--", "0"));
    }
    public void GridFill()
    {
        GridView1.DataSource = theOTList.GridPopup(DropDownList1.SelectedValue, DropDownList2.SelectedValue, TextBox1.Text, TextBox2.Text, Session["CoCode"].ToString().Trim(), Session["YearCode"].ToString().Trim());
        GridView1.DataBind();
    }
    protected void Button2_Click(object sender, EventArgs e)
    {
        GridFill();
    }



     protected void DropDownList1_SelectedIndexChanged(object sender, EventArgs e)
    {
        this.DropDownList2.DataSource = theOTList.DropdownOTDtls(DropDownList1.SelectedValue, Session["CoCode"].ToString().Trim());
        this.DropDownList2.DataTextField = "OperationName";
        this.DropDownList2.DataValueField = "OperationID";
        this.DropDownList2.DataBind();
        this.DropDownList2.Items.Insert(0, new ListItem("--Select--", "0"));
    }

    protected void Button1_Click(object sender, EventArgs e)
    {
        GridFill();
    }


    protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
    {
         if (e.CommandName == "ANESTHESIA")
        {

            string id = (e.CommandArgument).ToString();
            Session.Add("ReqNo", id);
            Response.Redirect("../IPD/AnesthesiaNote.aspx");
        }

        if (e.CommandName == "OPERATION")
        {

            string id = (e.CommandArgument).ToString();

            Session.Add("ReqNo", id);
            Response.Redirect("../IPD/OperationNote.aspx");
        }

        if (e.CommandName == "BIOPSY")
        {

            string id = (e.CommandArgument).ToString();
            Session.Add("ReqNo", id);
            Response.Redirect("../IPD/BiopsyNote.aspx");
        }
        if (e.CommandName == "CONSUMABLE")
        {

            string id = (e.CommandArgument).ToString();
            Session.Add("ReqNo", id);
            Response.Redirect("../IPD/OTConsumable.aspx");
        }

        if (e.CommandName == "ATTENDENCE")
        {

            string id = (e.CommandArgument).ToString();
            Session.Add("ReqNo", id);
            Response.Redirect("../IPD/AttendenceFees.aspx");
        }

        if (e.CommandName == "INSTRUMENT")
        {

            string id = (e.CommandArgument).ToString();
            Session.Add("ReqNo", id);
            Response.Redirect("../IPD/InstrumentCost.aspx?type=T");
        }

        if (e.CommandName == "DELIVERY")
        {

            string id = (e.CommandArgument).ToString();
            Session.Add("ReqNo", id);
            Response.Redirect("../IPD/DeliveryNote.aspx");
        }

    }

    protected void GridView1_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        GridView1.PageIndex = e.NewPageIndex;
        GridFill();
    }
    protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
    {       
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            

            
            Label lblregno = (Label)e.Row.FindControl("lblregno");
            Label lblreqno = (Label)e.Row.FindControl("lblreqno");            
            Label otnote = (Label)e.Row.FindControl("Label20");
            Label deliverynote = (Label)e.Row.FindControl("Label21");
            Label biopsynote = (Label)e.Row.FindControl("Label22");
            Label anesthesisnote = (Label)e.Row.FindControl("Label23");
            Label otconsumable = (Label)e.Row.FindControl("Label24");
            Label attendencefees = (Label)e.Row.FindControl("Label25");
            Label instrumentcost = (Label)e.Row.FindControl("Label26");


            DataSet ds = theOTList.DatabounFill(lblregno.Text, lblreqno.Text, Session["CoCode"].ToString().Trim(), Session["YearCode"].ToString().Trim());

            

            if (ds.Tables[7].Rows[0][0].ToString().Trim() == "0")
                e.Row.Cells[8].Enabled = false;

            if (ds.Tables[0].Rows.Count > 0)
                otnote.Text = "(Done)";

            if (ds.Tables[1].Rows.Count > 0)
                deliverynote.Text = "(Done)";

            if (ds.Tables[2].Rows.Count > 0)
                biopsynote.Text = "(Done)";

            if (ds.Tables[3].Rows.Count > 0)
                anesthesisnote.Text = "(Done)";

            if (ds.Tables[4].Rows.Count > 0)
                otconsumable.Text = "(Done)";

            if (ds.Tables[5].Rows.Count > 0)
                attendencefees.Text = "(Done)";

            if (ds.Tables[6].Rows.Count > 0)
                instrumentcost.Text = "(Done)";
        }
    }
}