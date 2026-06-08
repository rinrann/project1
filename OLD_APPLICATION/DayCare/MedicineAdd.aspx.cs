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

public partial class DayCare_MedicineAdd : System.Web.UI.Page
{
    Main Objmain = new Main(ConfigurationSettings.AppSettings["ConnectionString"].ToString());
    AddMedicine thereagentEntry = new AddMedicine(ConfigurationSettings.AppSettings["ConnectionString"].ToString());
    PatientDoctorVisit thedocvisit = new PatientDoctorVisit(ConfigurationSettings.AppSettings["ConnectionString"].ToString());
    DC_AddMedicine thedcmedicine = new DC_AddMedicine(ConfigurationSettings.AppSettings["ConnectionString"].ToString());
      
    protected void Page_Load(object sender, EventArgs e)
    {

        Page.Title = "Add Medicine";
        if (Session["userName"] == null)
        {
            Response.Redirect("../LoginPage.aspx");
        }
        if (Objmain.checkaccss(Session["CoCode"].ToString(), Session["UserRoleID"].ToString().Trim(), "ADD MEDICINE", checkAccessType.ViewAction) == false)
        {
            Response.Redirect("../AccessDenied.aspx");
        }

        if (Objmain.checkaccss(Session["CoCode"].ToString(), Session["UserRoleID"].ToString().Trim(), "ADD MEDICINE", checkAccessType.InsertAction) == false)
        {
            Button1.Enabled = false;
        }
        if (Objmain.checkaccss(Session["CoCode"].ToString(), Session["UserRoleID"].ToString().Trim(), "ADD MEDICINE", checkAccessType.DeleteAction) == false)
        {
            GridView1.Columns[12].Visible = false;
        }
        if (!IsPostBack)
        {

            DropDownFill();
            Tab1Func();
            //Fill();
            if (Session["RegnNo"] != null)
            {
                TextBox23.Text = Session["RegnNo"].ToString();
                TextBox24.Text = Session["Name"].ToString();
                GridFill();
            }
            Session["RegnNo"] = null;
            Session["Name"] = null;
        }


    }

    public void Fill()
    {
        if (Session["RegnNo"] != null)
        {
            
        }
        GridFill();
    }
    public void DropDownFill()
    {

        DropDownList1.DataSource = thereagentEntry.DropdownMedGroup(Session["CoCode"].ToString().Trim());
        DropDownList1.DataTextField = "MedicineGroupName";
        DropDownList1.DataValueField = "MedicineGroupID";
            DropDownList1.DataBind();
            DropDownList1.Items.Insert(0, new ListItem("--Select--", "0"));

            ddlSubGroup1.Items.Insert(0, new ListItem("--Select--", "0"));

            ddlBatchNo1.Items.Insert(0, new ListItem("--Select--", "0"));

            DropDownList2.Items.Insert(0, new ListItem("--Select--", "0"));

            DropDownList3.DataSource = thereagentEntry.DropdownDoctor(Session["CoCode"].ToString().Trim());
            DropDownList3.DataTextField = "doc_name";
            DropDownList3.DataValueField = "doc_id";
            DropDownList3.DataBind();
            DropDownList3.Items.Insert(0, new ListItem("--Select--", "0")); 

    }
    protected void Button1_Click(object sender, EventArgs e)
    { 
        System.Globalization.DateTimeFormatInfo dtf = new DateTimeFormatInfo();
 
        if (Button1.Text == "Submit")
        {
            DateTime testdate = DateTime.ParseExact(TextBox1.Text, "dd/MM/yyyy", dtf);
            DateTime Exdatedate = DateTime.ParseExact(txtExpiryDate1.Text, "dd/MM/yyyy", dtf);

            if (thedcmedicine.InsertAddedicine(Session["CoCode"].ToString().Trim(), Session["YearCode"].ToString().Trim(), TextBox23.Text, DropDownList1.SelectedValue, DropDownList2.SelectedValue, testdate.ToString("yyyy-MM-dd"), DropDownList3.SelectedValue, ddlSubGroup1.SelectedValue, txtBillQty1.Text, ddlBatchNo1.SelectedValue, Exdatedate.ToString("yyyy-MM-dd")) == true)
                ScriptManager.RegisterStartupScript(this, this.GetType(), "msg", "alert('Insered Successfully !');", true);
            else
                ScriptManager.RegisterStartupScript(this, this.GetType(), "msg", "alert('Error in Insered Data !');", true);
        }
        else
        {
            DateTime testdate = DateTime.ParseExact(TextBox1.Text, "dd/MM/yyyy", dtf);
            DateTime Exdatedate = DateTime.ParseExact(txtExpiryDate1.Text, "dd/MM/yyyy", dtf);
            if (thedcmedicine.UpdateAddedicine(Session["CoCode"].ToString().Trim(), Session["YearCode"].ToString().Trim(), HiddenField1.Value, DropDownList1.SelectedValue, DropDownList2.SelectedValue, testdate.ToString("yyyy-MM-dd"), DropDownList3.SelectedValue, ddlSubGroup1.SelectedValue, txtBillQty1.Text, ddlBatchNo1.SelectedValue, Exdatedate.ToString("yyyy-MM-dd")) == true)
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "msg", "alert('Updated Successfully !');", true);
                lblError.ForeColor = Color.Green;
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "msg", "alert('Error in Updated Data !');", true);
                lblError.ForeColor = Color.Red;
            }


        }
        GridFill();
        ResetAllFields();
    }
    public void ResetAllFields()
    {
        HiddenField1.Value = "";
        TextBox1.Text = "";
        txtBillQty1.Text = "";
        txtExpiryDate1.Text = "";
        DropDownList1.SelectedIndex = 0;
        DropDownList2.SelectedIndex = 0;
        DropDownList3.SelectedIndex = 0;
        ddlBatchNo1.SelectedIndex = 0;
        ddlSubGroup1.SelectedIndex = 0;

        if (Objmain.checkaccss(Session["CoCode"].ToString(), Session["UserRoleID"].ToString().Trim(), "ADD MEDICINE", checkAccessType.InsertAction) == false)
        {
            Button1.Enabled = false;
        }
    }

    protected void Button4_Click(object sender, EventArgs e)
    {
        ResetAllFields();
    }
    protected void Button2_Click(object sender, EventArgs e)
    {
        GridFill();
    }

    private void GridFill()
    {

        DataTable dt = thedcmedicine.GetAllPatientMedicine(Session["CoCode"].ToString().Trim(), Session["YearCode"].ToString().Trim(), TextBox23.Text);
        GridView1.DataSource = dt;
        GridView1.DataBind();

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

    protected int SearchText(string Value, DropDownList ddl)
    {
        int i;
        for (i = 0; i < ddl.Items.Count; i++)
        {
            if (ddl.Items[i].Text.Trim() == Value.Trim())
                return i;
        }
        return -1;
    }
    protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "Select")
        {
            ResetAllFields();
            int index = Convert.ToInt32(e.CommandArgument);
            lblError.Text = "";
            Label lblid = (Label)GridView1.Rows[index].FindControl("lblid");
            HiddenField1.Value = lblid.Text;

            Label lblRegno = (Label)GridView1.Rows[index].FindControl("lblRegno");
            TextBox23.Text = lblRegno.Text;

            Label lbllname = (Label)GridView1.Rows[index].FindControl("lbllname");
            TextBox24.Text = lbllname.Text;

            Label lblMedicineGroupName = (Label)GridView1.Rows[index].FindControl("lblMedicineGroupName");
            DropDownList1.SelectedIndex = SearchText(lblMedicineGroupName.Text, DropDownList1);

            DropDownSubGroupFill(DropDownList1.SelectedValue, ddlSubGroup1);
            Label lblMedicineSubGrId = (Label)GridView1.Rows[index].FindControl("lblMedicineSubGrId");
            ddlSubGroup1.SelectedIndex = SearchIndex(lblMedicineSubGrId.Text, ddlSubGroup1);

            DropDownMedicineFill(ddlSubGroup1.SelectedValue, DropDownList2);
            Label lblMedicineName = (Label)GridView1.Rows[index].FindControl("lblMedicineName");
            DropDownList2.SelectedIndex = SearchText(lblMedicineName.Text, DropDownList2);

            Label lblisdate = (Label)GridView1.Rows[index].FindControl("lblisdate");
            TextBox1.Text = lblisdate.Text;

            Label lblAdviceBy = (Label)GridView1.Rows[index].FindControl("lblAdviceBy");
            DropDownList3.SelectedIndex = SearchText(lblAdviceBy.Text, DropDownList3);
            DropDownList3.SelectedValue = lblAdviceBy.Text.Trim();

            Label lblBillQty = (Label)GridView1.Rows[index].FindControl("lblBillQty");
            txtBillQty1.Text = lblBillQty.Text;

            BatchNoFill(DropDownList2.SelectedValue, ddlBatchNo1);
            Label lblBatchNo = (Label)GridView1.Rows[index].FindControl("lblBatchNo");
            ddlBatchNo1.SelectedIndex = SearchText(lblBatchNo.Text, ddlBatchNo1);

            Label lblExpirDate = (Label)GridView1.Rows[index].FindControl("lblExpirDate");
            txtExpiryDate1.Text = lblExpirDate.Text;

            Tab1Func();
            Button1.Text = "Update";
            if (Objmain.checkaccss(Session["CoCode"].ToString(), Session["UserRoleID"].ToString().Trim(), "ADD MEDICINE", checkAccessType.UpdateAction) == false)
            {
                Button1.Enabled = false;
            }
            else
            {
                Button1.Enabled = true;
            }
        }
    }
    protected void GridView1_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        GridView1.PageIndex = e.NewPageIndex;
        GridFill();
    }


    public void DropDownSubGroupFill(string value, DropDownList drop2)
    {
        drop2.Items.Clear();
        drop2.DataSource = thereagentEntry.DropDownMedicineSubgroup(Session["CoCode"].ToString().Trim(),value);
        drop2.DataTextField = "SubGrName";
        drop2.DataValueField = "ID";
        drop2.DataBind();
        drop2.Items.Insert(0, new ListItem("--Select--", "0"));
    }
    public void DropDownMedicineFill(string value, DropDownList drop2)
    {
        drop2.Items.Clear();
        drop2.DataSource = thereagentEntry.DropDownMedicine(Session["CoCode"].ToString().Trim(), Session["YearCode"].ToString().Trim(), value, TextBox23.Text.Trim());
        drop2.DataTextField = "MedicineName";
        drop2.DataValueField = "MedicineID";
        drop2.DataBind();
        drop2.Items.Insert(0, new ListItem("--Select--", "0"));
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
        GridFill();
    }
    public void Tab1Func()
    {
        Tab1.CssClass = "Clicked";
        Tab2.CssClass = "Initial";
        MainView.ActiveViewIndex = 0;
    }


    public void BatchNoFill(string value, DropDownList ddlBatchNo)
    {

        ddlBatchNo.Items.Clear();
        ddlBatchNo.DataSource = thedocvisit.DropdownBatchNo(Session["CoCode"].ToString().Trim(), Session["YearCode"].ToString().Trim(), value);
        ddlBatchNo.DataTextField = "BatchNo";
        ddlBatchNo.DataValueField = "BatchNo";
        ddlBatchNo.DataBind();
        ddlBatchNo.Items.Insert(0, new ListItem("--Select--", "0"));

    }

    protected void DropDownList1_SelectedIndexChanged(object sender, EventArgs e)
    {
        DropDownSubGroupFill(DropDownList1.SelectedValue, ddlSubGroup1);
    }

    protected void ddlSubGroup1_SelectedIndexChanged(object sender, EventArgs e)
    {
        DropDownMedicineFill(ddlSubGroup1.SelectedValue, DropDownList2);
    }
    protected void DropDownList2_SelectedIndexChanged(object sender, EventArgs e)
    {
        BatchNoFill(DropDownList2.SelectedValue, ddlBatchNo1);
    }
    protected void ddlBatchNo1_SelectedIndexChanged(object sender, EventArgs e)
    {
        DataTable dt = thedocvisit.ExpiryDateFill(Session["CoCode"].ToString().Trim(), ddlBatchNo1.SelectedValue);
        txtExpiryDate1.Text = dt.Rows[0][0].ToString();
    }
    protected void GridView1_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {

        Label lblid = (Label)GridView1.Rows[e.RowIndex].FindControl("lblid");
        if (thedcmedicine.DeleteAddmedicine(lblid.Text) == true)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "msg", "alert('Deleted Successfully !');", true);
        }
        else
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "msg", "alert('Error in Deleted Data !');", true);
        }
        GridFill();
        ResetAllFields();
    }
    protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            if (Objmain.checkaccss(Session["CoCode"].ToString(), Session["UserRoleID"].ToString().Trim(), "ADD MEDICINE", checkAccessType.DeleteAction) == false)
            {
                e.Row.Cells[12].Visible = false;
            }
        }
    }
}