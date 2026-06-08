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
using System.Globalization;
 
public partial class IPD_OTRequisition : System.Web.UI.Page
{
    Main Objmain = new Main(ConfigurationSettings.AppSettings["ConnectionString"].ToString());
    OTRequisition thereq = new OTRequisition(ConfigurationSettings.AppSettings["ConnectionString"].ToString());
    System.Text.StringBuilder rpt = new System.Text.StringBuilder();
    String Date = DateTime.Now.ToString("dd/MM/yyyy");
    protected void Page_Load(object sender, EventArgs e)
    {
        Page.Title = "OT Requition";
       if (Session["userName"] == null)
        {
            Response.Redirect("../LoginPage.aspx");
        }
       if (Objmain.checkaccss(Session["CoCode"].ToString(), Session["UserRoleID"].ToString().Trim(), "OT REQUISITION", checkAccessType.ViewAction) == false)
       {
           Response.Redirect("../AccessDenied.aspx");
       }

       if (Objmain.checkaccss(Session["CoCode"].ToString(), Session["UserRoleID"].ToString().Trim(), "OT REQUISITION", checkAccessType.InsertAction) == false)
       {
           Button1.Enabled = false;
       }
       if (Objmain.checkaccss(Session["CoCode"].ToString(), Session["UserRoleID"].ToString().Trim(), "OT REQUISITION", checkAccessType.DeleteAction) == false)
       {
           GridView1.Columns[20].Visible = false;
       }

       if (!IsPostBack)
       {
           if (Session["RegNo"] != null)
           {
               txtreg.Text = Session["RegNo"].ToString();
               Fill();
               Tab1Func();
               GenerateCode();
               DropDownFill();
               
           }
           else { GridFill(); DropDownFill(); Tab1Func(); }
         
       }
       Session["RegNo"] = null;
    }
    public void Fill()
    {
        DataTable dt = thereq.pname(Session["CoCode"].ToString().Trim(), txtreg.Text);
        txtName.Text = dt.Rows[0]["patient_name"].ToString();
        txtCurrentBed.Text = dt.Rows[0]["BedNoText"].ToString();
        txtAdmissionDate.Text = dt.Rows[0]["adate"].ToString();
        GridFill();
    }

    public void GenerateCode()
    {
        DataTable dt = thereq.GenerateOTRequisitionNo(Session["CoCode"].ToString().Trim());
        txtreqno.Text = dt.Rows[0][0].ToString();
    }


    public void GridFill()
    {
        GridView1.DataSource = thereq.GridFill(Session["CoCode"].ToString().Trim(), Session["YearCode"].ToString().Trim(), txtreg.Text);
        GridView1.DataBind();
    }
    protected void Button1_Click(object sender, EventArgs e)
    {        
        Button1.Enabled = false;
          string a = HiddenField1.Value;
        string b = HiddenField2.Value;
        System.Globalization.DateTimeFormatInfo dtf = new DateTimeFormatInfo();
        DateTime testdate = DateTime.ParseExact(txtOTDate.Text, "dd/MM/yyyy", dtf);
        if (Button1.Text == "Submit")
        {
            if (thereq.Insert_Update_Delete_OTRequisition(ddlImplant.SelectedValue, 1, ddlOTRoom.SelectedValue, txtreg.Text, txtreqno.Text, /*HiddenField1.Value*/ddlOperationType.SelectedValue, HiddenField2.Value, ddlSurgeonName.SelectedValue, ddlAddDoc1.SelectedValue, ddlAddDoc2.SelectedValue, ddlAddDoc3.SelectedValue, ddlAnesthesia1.SelectedValue, ddlAnesthesia2.SelectedValue, testdate.ToString("yyyy-MM-dd"), txtStartTime.Text, txtendtime.Text, Session["CoCode"].ToString().Trim(), Session["YearCode"].ToString().Trim(), Session["userName"].ToString().Trim()) == true)
            {
                //ScriptManager.RegisterStartupScript(this, this.GetType(), "msg", "alert('Inserted  Successfully !');", true);
                Response.Redirect("../IPD/OTList.aspx");
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "msg", "alert('Error in inserted Data !');", true);
            }
        }
        else
        {
            if (thereq.Insert_Update_Delete_OTRequisition(ddlImplant.SelectedValue, 2, ddlOTRoom.SelectedValue, txtreg.Text, txtreqno.Text, /*HiddenField1.Value*/ddlOperationType.SelectedValue, HiddenField2.Value, ddlSurgeonName.SelectedValue, ddlAddDoc1.SelectedValue, ddlAddDoc2.SelectedValue, ddlAddDoc3.SelectedValue, ddlAnesthesia1.SelectedValue, ddlAnesthesia2.SelectedValue, testdate.ToString("yyyy-MM-dd"), txtStartTime.Text, txtendtime.Text, Session["CoCode"].ToString().Trim(), Session["YearCode"].ToString().Trim(), Session["userName"].ToString().Trim()) == true)
            {
                //ScriptManager.RegisterStartupScript(this, this.GetType(), "msg", "alert('Inserted  Successfully !');", true);
                Response.Redirect("../IPD/OTList.aspx");
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "msg", "alert('Error in Updated Data !');", true);
            }
        }
        Button1.Text = "Submit";
        GridFill();
    }
    public void ResetAllFields()
    {
        txtreg.Text = "";
        GenerateCode();
        HiddenField1.Value="";
        HiddenField2.Value="";
        ddlSurgeonName.SelectedIndex=0;
        ddlAddDoc1.SelectedIndex=0;ddlAddDoc2.SelectedIndex=0;ddlAddDoc3.SelectedIndex=0;ddlAnesthesia1.SelectedIndex=0;ddlAnesthesia2.SelectedIndex=0;
        txtOTDate.Text="";txtStartTime.Text="";
        txtendtime.Text="";

        if (Objmain.checkaccss(Session["CoCode"].ToString(), Session["UserRoleID"].ToString().Trim(), "OT REQUISITION", checkAccessType.InsertAction) == false)
        {
            Button1.Enabled = false;
        }

    }
    protected void Button2_Click(object sender, EventArgs e)
    {
        ResetAllFields();
        Button1.Enabled = true;
    }
    private void DropDownFill()
    {
        this.ddlSurgeonName.DataSource = thereq.GetSurgeon(Session["CoCode"].ToString().Trim());
        this.ddlSurgeonName.DataTextField = "doc_name";
        this.ddlSurgeonName.DataValueField = "doc_id";
        this.ddlSurgeonName.DataBind();
        this.ddlSurgeonName.Items.Insert(0, new ListItem("--Select--", "0"));


        this.ddlOTRoom.DataSource = thereq.GetOTRoom(Session["CoCode"].ToString().Trim());
        this.ddlOTRoom.DataTextField = "RoomName";
        this.ddlOTRoom.DataValueField = "RoomId";
        this.ddlOTRoom.DataBind();
        this.ddlOTRoom.Items.Insert(0, new ListItem("--Select--", "0"));

        this.ddlOperationType.DataSource = thereq.GetOperationType(Session["CoCode"].ToString().Trim());
        this.ddlOperationType.DataTextField = "OperationTypeName";
        this.ddlOperationType.DataValueField = "OperationTypeID";
        this.ddlOperationType.DataBind();
        this.ddlOperationType.Items.Insert(0, new ListItem("--Select--", "0"));

        this.ddlImplant.DataSource = thereq.GetImplant(Session["CoCode"].ToString().Trim());
        this.ddlImplant.DataTextField = "Description";
        this.ddlImplant.DataValueField = "ImplantId";
        this.ddlImplant.DataBind();
        this.ddlImplant.Items.Insert(0, new ListItem("--Select--", "0"));

        for (int i = 1; i <=3; i++)
        {
            DropDownList d = (DropDownList)Page.FindControl("ctl00$ContentPlaceHolder1$ddlAddDoc" + i.ToString());
            d.DataSource = thereq.GetDoctor(Session["CoCode"].ToString().Trim());
            d.DataTextField = "doc_name";
            d.DataValueField = "doc_id";
            d.DataBind();
            d.Items.Insert(0, new ListItem("--Select--", "0"));
        }

        for (int i = 1; i <= 2; i++)
        {
            DropDownList d = (DropDownList)Page.FindControl("ctl00$ContentPlaceHolder1$ddlAnesthesia" + i.ToString());
            d.DataSource = thereq.GetDoctor(Session["CoCode"].ToString().Trim());
            d.DataTextField = "doc_name";
            d.DataValueField = "doc_id";
            d.DataBind();
            d.Items.Insert(0, new ListItem("--Select--", "0"));
        }

    }

    protected void GridView1_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        GridView1.PageIndex = e.NewPageIndex;
        GridFill();
    }
     
    protected void Button5_Click(object sender, EventArgs e)
    { 
        Fill();
        Tab1Func();
        GenerateCode();
        DropDownFill();
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
    protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "Select")
        {
            int index = Convert.ToInt32(e.CommandArgument);


            Label lblreqno = (Label)GridView1.Rows[index].FindControl("lblreqno");
            txtreqno.Text = lblreqno.Text;

            Label lblOperationTypeID = (Label)GridView1.Rows[index].FindControl("lblOperationTypeID");
            HiddenField1.Value = lblOperationTypeID.Text;

            Label lblOperationID = (Label)GridView1.Rows[index].FindControl("lblOperationID");
            HiddenField2.Value = lblOperationID.Text;


            /*Label lblottype = (Label)GridView1.Rows[index].FindControl("lblottype");
            txtOperationType.Text = lblottype.Text;*/
            ddlOperationType.SelectedValue = lblOperationTypeID.Text;

            Label lblotname = (Label)GridView1.Rows[index].FindControl("lblotname");
            txtOperationName.Text = lblotname.Text;

            Label lblSurgeonID = (Label)GridView1.Rows[index].FindControl("lblSurgeonID");
            ddlSurgeonName.SelectedValue = lblSurgeonID.Text;

            Label lblAddDocID1 = (Label)GridView1.Rows[index].FindControl("lblAddDocID1");
            ddlAddDoc1.SelectedValue = lblAddDocID1.Text;

            Label lblAddDocID2 = (Label)GridView1.Rows[index].FindControl("lblAddDocID2");
            ddlAddDoc2.SelectedValue = lblAddDocID2.Text;

            Label lblAddDocID3 = (Label)GridView1.Rows[index].FindControl("lblAddDocID3");
            ddlAddDoc3.SelectedValue = lblAddDocID3.Text;

            Label lblAnesthetist1 = (Label)GridView1.Rows[index].FindControl("lblAnesthetist1");
            ddlAnesthesia1.SelectedValue = lblAnesthetist1.Text;

            Label lblAnesthetist2 = (Label)GridView1.Rows[index].FindControl("lblAnesthetist2");
            ddlAnesthesia2.SelectedValue = lblAnesthetist2.Text;

            Label lblOTRoomNo = (Label)GridView1.Rows[index].FindControl("lblOTRoomNo");
            ddlOTRoom.SelectedValue = lblOTRoomNo.Text;

            Label lblImplant = (Label)GridView1.Rows[index].FindControl("lblImplant");
            ddlImplant.SelectedValue = lblImplant.Text;

            Label lblotdate = (Label)GridView1.Rows[index].FindControl("lblotdate");
            txtOTDate.Text = lblotdate.Text;

            Label lblstarttime = (Label)GridView1.Rows[index].FindControl("lblstarttime");
            txtStartTime.Text = lblstarttime.Text;

            

            Tab1Func();
            Button1.Text = "Update";
            if (Objmain.checkaccss(Session["CoCode"].ToString(), Session["UserRoleID"].ToString().Trim(), "OT REQUISITION", checkAccessType.UpdateAction) == false)
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
        Label lblreqno = (Label)GridView1.Rows[e.RowIndex].FindControl("lblreqno");

        if (thereq.Insert_Update_Delete_OTRequisition(null, 3, null, null, lblreqno.Text, null, null, null, null, null, null, null, null, null, null, null, Session["CoCode"].ToString().Trim(), Session["YearCode"].ToString().Trim(), Session["userName"].ToString().Trim()) == true)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "msg", "alert('Deleted  Successfully !');", true);
            //Response.Redirect("../IPD/OTList.aspx");
        }
        else
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "msg", "alert('Error in Deleted Data !');", true);
        }

        GridFill();
        ResetAllFields();
    }
    protected void Button8_Click(object sender, EventArgs e)
    {

    }
    protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            if (Objmain.checkaccss(Session["CoCode"].ToString(), Session["UserRoleID"].ToString().Trim(), "OT REQUISITION", checkAccessType.DeleteAction) == false)
            {
                e.Row.Cells[20].Visible = false;
            }
        }
    }
}