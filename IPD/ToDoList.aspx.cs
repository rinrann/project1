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

public partial class IPD_ToDoList : System.Web.UI.Page
{
    ToDoTask theHelper = new  ToDoTask(ConfigurationSettings.AppSettings["ConnectionString"].ToString());
    //PatientDoctorVisit thedocvisit = new PatientDoctorVisit(ConfigurationSettings.AppSettings["ConnectionString"].ToString());
  
    protected void Page_Load(object sender, EventArgs e)
    {
        Page.Title = "To Do List";
        //if (Session["userName"] == null)
        //{
        //    Response.Redirect("../LoginPage.aspx");
        //}
        if (!IsPostBack)
        {
           
            DropDownFill();

            if (Session["RegNo"] != null)
            {
                TextBox23.Text = Session["RegNo"].ToString();
                FillDetails();
                GridFill();
            }
           Session["RegNo"]=null;
        }
        Session["RegNo"] = null;
    }
    private void FillDetails()
    {
        DataTable dt = theHelper.Getonlypat(Session["CoCode"].ToString().Trim(),TextBox23.Text);
        if (dt.Rows.Count > 0)
        {
            TextBox24.Text = dt.Rows[0]["patient_name"].ToString();
            TextBox25.Text = dt.Rows[0]["BedNoText"].ToString();
            TextBox26.Text = dt.Rows[0]["adate"].ToString();
        }
    }

    private void GridFill()
    {
        GridView1.DataSource = theHelper.GridFill(Session["CoCode"].ToString().Trim(),TextBox23.Text);
        GridView1.DataBind();
    }
    private void ResetAllFields()
    {

        DropDownList1.SelectedIndex = 0;
        TextBox1.Text = "";
         Button1.Text = "Submit";

    }

   
    //protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
    //{
    //    if (e.CommandName == "Select")
    //    {
    //        int index = Convert.ToInt32(e.CommandArgument);

    //        Label lblMedicineID = (Label)GridView1.Rows[index].FindControl("MedicineID");
    //        HiddenField1.Value = lblMedicineID.Text;

    //        Label lblMedicineName = (Label)GridView1.Rows[index].FindControl("MedicineName");
    //        txtMedicineName.Text = lblMedicineName.Text;

    //        Label lblStockAlert = (Label)GridView1.Rows[index].FindControl("lblStockAlert");
    //        TextBox1.Text = lblStockAlert.Text;

    //        Label lblsub = (Label)GridView1.Rows[index].FindControl("lblsub");
    //        subFill();
    //        DropDownList4.SelectedValue = lblsub.Text;

    //        DropDownFill();
    //        Label lblMCode = (Label)GridView1.Rows[index].FindControl("MCode");
    //        DropDownList1.SelectedValue = lblMCode.Text;

    //        Label lblMedicineGroupID = (Label)GridView1.Rows[index].FindControl("MedicineGroupID");
    //        DropDownList2.SelectedValue = lblMedicineGroupID.Text;

    //        Label UnitID = (Label)GridView1.Rows[index].FindControl("UnitID");
    //        DropDownList3.SelectedValue = UnitID.Text;


    //        //Label lblTradeName = (Label)GridView1.Rows[index].FindControl("TradeName");
    //        //txtTradeName.Text = lblTradeName.Text;

    //        Tab1Func();
    //        Button1.Text = "Update";
    //    }
    //}
    private void DropDownFill()
    {
        DropDownList1.Items.Clear();
        this.DropDownList1.DataSource = theHelper.DropdownTask(Session["CoCode"].ToString().Trim());
        this.DropDownList1.DataTextField = "TaskName";
        this.DropDownList1.DataValueField = "TaskId";
        this.DropDownList1.DataBind();
        this.DropDownList1.Items.Insert(0, new ListItem("--Select--", "0"));

    }


    protected void Button1_Click(object sender, EventArgs e)
    {
        System.Globalization.DateTimeFormatInfo dtf = new DateTimeFormatInfo();
        DateTime testdate = DateTime.ParseExact(TextBox1.Text, "dd/MM/yyyy", dtf);

        if (Button1.Text == "Submit")
        {

            if (theHelper.Patienttask_insert_Update_Delete(1, null, TextBox23.Text, testdate.ToString("yyyy-MM-dd"), DropDownList1.SelectedValue, Session["CoCode"].ToString().Trim(), Session["userName"].ToString().Trim()) == true)
                ScriptManager.RegisterStartupScript(this, this.GetType(), "msg", "alert('Inserted  Successfully !');", true);
            else
                ScriptManager.RegisterStartupScript(this, this.GetType(), "msg", "alert('Error in Inserted Data !');", true);
        }

        ResetAllFields();
        GridFill();

    }


    protected void GridView1_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        GridView1.PageIndex = e.NewPageIndex;
        GridFill();
    }
    protected void GridView1_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        Label lblid = (Label)GridView1.Rows[e.RowIndex].FindControl("lblid");
        if (theHelper.Patienttask_insert_Update_Delete(3, lblid.Text, TextBox23.Text, null, null, Session["CoCode"].ToString().Trim(), Session["userName"].ToString().Trim()) == true)
          ScriptManager.RegisterStartupScript(this, this.GetType(), "msg", "alert('Deleted  Successfully !');", true);
      else
          ScriptManager.RegisterStartupScript(this, this.GetType(), "msg", "alert('Error in Deleted Data !');", true);
       
        GridFill();
        ResetAllFields();

    }
    protected void GridView1_RowUpdating(object sender, GridViewUpdateEventArgs e)
    {
        Label lblid = (Label)GridView1.Rows[e.RowIndex].FindControl("lblid");
        TextBox txtdate = (TextBox)GridView1.Rows[e.RowIndex].FindControl("txtdate");
        DropDownList ddltask = (DropDownList)GridView1.Rows[e.RowIndex].FindControl("ddltask"); 

        System.Globalization.DateTimeFormatInfo dtf = new DateTimeFormatInfo();
        DateTime testdate = DateTime.ParseExact(txtdate.Text, "dd/MM/yyyy", dtf);
       // if (theHelper.Patienttask_insert_Update_Delete(2, lblid.Text, null, testdate.ToString(), ddltask.SelectedValue) == true)
        if (theHelper.Patienttask_insert_Update_Delete(2, lblid.Text, TextBox23.Text, testdate.ToString(), ddltask.SelectedValue, Session["CoCode"].ToString().Trim(), Session["userName"].ToString().Trim()) == true)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "msg", "alert('Updated Successfully !');", true);
        }
        else
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "msg", "alert('Error in Updated Data !');", true);
        }
        GridView1.EditIndex = -1;
        GridFill();
    }
    protected void GridView1_RowEditing(object sender, GridViewEditEventArgs e)
    {
        GridView1.EditIndex = e.NewEditIndex;
        GridFill();
    }
    protected void GridView1_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
    {
        GridView1.EditIndex = -1;
        GridFill();
    }
    protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow && (e.Row.RowState & DataControlRowState.Edit) == DataControlRowState.Edit)
        {
            DropDownList ddltask = (DropDownList)e.Row.FindControl("ddltask");
            Label lblToDoTaskId = (Label)e.Row.FindControl("lblToDoTaskId");

            ddltask.DataSource = theHelper.DropdownTask(Session["CoCode"].ToString().Trim());
            ddltask.DataTextField = "TaskName";
            ddltask.DataValueField = "TaskId";
            ddltask.DataBind();
            ddltask.Items.Insert(0, new ListItem("--Select--", "0"));
            ddltask.SelectedValue = lblToDoTaskId.Text;
        }
    }
    protected void LinkButton1_Click(object sender, EventArgs e)
    {
        Response.Redirect("../IPD/AdmissionPatientList.aspx");
    }
    protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "UpdateStatus")
        {
            // Retrieve the row index stored in the 
            // CommandArgument property.
            int index = Convert.ToInt32(e.CommandArgument);

            // Retrieve the row that contains the button 
            // from the Rows collection.
            GridViewRow row = GridView1.Rows[index];

            Label lblid = (Label)GridView1.Rows[index].FindControl("lblid");
            TextBox txtdate = (TextBox)GridView1.Rows[index].FindControl("txtdate");
            DropDownList ddltask = (DropDownList)GridView1.Rows[index].FindControl("ddltask");

            System.Globalization.DateTimeFormatInfo dtf = new DateTimeFormatInfo();
            //DateTime testdate = DateTime.ParseExact(txtdate.Text, "dd/MM/yyyy", dtf);
            if (theHelper.Patienttask_insert_Update_Delete(4, lblid.Text, TextBox23.Text, DateTime.Now.Date.ToString(), null, Session["CoCode"].ToString().Trim(), Session["userName"].ToString().Trim()) == true)
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "msg", "alert('Updated Successfully !');", true);
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "msg", "alert('Error in Updated Data !');", true);
            }
            GridView1.EditIndex = -1;
            GridFill();
            // Add code here to add the item to the shopping cart.
        }
    }
}