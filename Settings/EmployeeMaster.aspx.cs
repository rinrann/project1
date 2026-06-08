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


public partial class Master_EmployeeMaster : System.Web.UI.Page
{
    Main Objmain = new Main(ConfigurationSettings.AppSettings["ConnectionString"].ToString());
    EmployeeMaster theHelper = new EmployeeMaster(ConfigurationSettings.AppSettings["ConnectionString"].ToString());
    protected void Page_Load(object sender, EventArgs e)
    {
        Page.Title = "Employee Master";
        if (Session["userName"] == null)
        {
            Response.Redirect("../LoginPage.aspx");
        }
        if (Objmain.checkaccss(Session["CoCode"].ToString(), Session["UserRoleID"].ToString().Trim(), "EMPLOYEE MASTER", checkAccessType.ViewAction) == false)
        {
            Response.Redirect("../AccessDenied.aspx");
        }

        if (Objmain.checkaccss(Session["CoCode"].ToString(), Session["UserRoleID"].ToString().Trim(), "EMPLOYEE MASTER", checkAccessType.InsertAction) == false)
        {
            Button1.Enabled = false;
        }
        if (Objmain.checkaccss(Session["CoCode"].ToString(), Session["UserRoleID"].ToString().Trim(), "EMPLOYEE MASTER", checkAccessType.DeleteAction) == false)
        {
            GridView1.Columns[16].Visible = false;
        }
        if (!IsPostBack)
        {
            Tab1Func();
            GridFill();
            DropDownFill();
        }
    }
    private void GridFill()
    {
        GridView1.DataSource = theHelper.GetAllEmployee(Session["CoCode"].ToString());
        GridView1.DataBind();
    }
    private void ResetAllFields()
    {
        txtEmployeeName.Text = "";
        DropDownList1.SelectedIndex = -1;
        DropDownList2.SelectedIndex = -1;
        txtAddress.Text = "";
        txtCity.Text = "";
        DropDownList5.SelectedIndex = 0;
        txtphn13.Text = "";
        txtphn23.Text = "";
        Calendar1.Text = "";
        Calendar2.Text = "";
        txtAge.Text = "";
        DropDownList3.SelectedIndex = 0;
        DropDownList4.SelectedIndex = 0;
        Button1.Text = "Submit";
    }
    protected void GridView1_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        GridView1.PageIndex = e.NewPageIndex;
        GridFill();
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
            int index = Convert.ToInt32(e.CommandArgument);
            Label lblEmployeeID = (Label)GridView1.Rows[index].FindControl("EmployeeID");
            HiddenField1.Value = lblEmployeeID.Text;

            Label lblEmployeeName = (Label)GridView1.Rows[index].FindControl("EmployeeName");
            txtEmployeeName.Text = lblEmployeeName.Text;


            DropDownFill();
            Label lblDesignationID = (Label)GridView1.Rows[index].FindControl("DesignationID");
            DropDownList1.SelectedValue = lblDesignationID.Text;

            Label lblSex = (Label)GridView1.Rows[index].FindControl("Sex");
            DropDownList2.SelectedIndex = SearchText(lblSex.Text, DropDownList2);

            Label lblAddress = (Label)GridView1.Rows[index].FindControl("Address");
            txtAddress.Text = lblAddress.Text;

            Label lblCity = (Label)GridView1.Rows[index].FindControl("City");
            txtCity.Text = lblCity.Text;

            Label lblState = (Label)GridView1.Rows[index].FindControl("State");
            DropDownList5.SelectedValue = lblState.Text;

            Label lblphn_1 = (Label)GridView1.Rows[index].FindControl("PhoneNo_1");
            string[] Contracts = lblphn_1.Text.Split(' ');

            txtphn11.Text = "+91";
            if (Contracts.Length > 1)
                txtphn13.Text = Contracts[1];

            Label lblphn_2 = (Label)GridView1.Rows[index].FindControl("PhoneNo_2");
            string[] Contracts1 = lblphn_2.Text.Split(' ');

            txtphn21.Text = "+91";
            if (Contracts1.Length > 1)
                txtphn23.Text = Contracts1[1];

            Label lblJoiningDate = (Label)GridView1.Rows[index].FindControl("JoiningDate");
            Calendar1.Text = lblJoiningDate.Text.ToString().Substring(0, 10);

            Label lbldob = (Label)GridView1.Rows[index].FindControl("dob");
            Calendar3.Text = lbldob.Text.ToString().Substring(0, 10);

            Label lblAge = (Label)GridView1.Rows[index].FindControl("Age");
            txtAge.Text = lblAge.Text;

            Label lblNationality = (Label)GridView1.Rows[index].FindControl("Nationality");
            DropDownList3.SelectedValue = lblNationality.Text;

            Label lblReligion = (Label)GridView1.Rows[index].FindControl("Religion");
            DropDownList4.SelectedIndex = SearchText(lblReligion.Text, DropDownList4);

            Label lblLeavingDate = (Label)GridView1.Rows[index].FindControl("LeavingDate");
            if (lblLeavingDate.Text.ToString().Substring(0, 10) != "1900-01-01")
                Calendar2.Text = lblLeavingDate.Text.ToString().Substring(0, 10);

            Tab1Func();
            Button1.Text = "Update";
            lblError.Text = "";
            //if (Objmain.checkaccss(Session["CoCode"].ToString(), Session["UserRoleID"].ToString().Trim(), "EMPLOYEE MASTER", checkAccessType.UpdateAction) == false)
            //{
            //    Button1.Enabled = false;
            //}
            //else
            //{
            //    Button1.Enabled = true;
            //}
        }
    }
    private void DropDownFill()
    {
        DropDownList1.Items.Clear();
        this.DropDownList1.DataSource = theHelper.DropdownID();
        this.DropDownList1.DataTextField = "DesignationName";
        this.DropDownList1.DataValueField = "DesignationID";
        this.DropDownList1.DataBind();
        this.DropDownList1.Items.Insert(0, new ListItem("--Select--", "0"));


        DropDownList2.Items.Clear();
        this.DropDownList2.DataSource = theHelper.DropdownGender();
        this.DropDownList2.DataTextField = "SexName";
        this.DropDownList2.DataValueField = "ID";
        this.DropDownList2.DataBind();
        this.DropDownList2.Items.Insert(0, new ListItem("--Select--", "0"));




        DropDownList4.Items.Clear();
        this.DropDownList4.DataSource = theHelper.DropdownReligion();
        this.DropDownList4.DataTextField = "Name";
        this.DropDownList4.DataValueField = "ID";
        this.DropDownList4.DataBind();

        DropDownList5.Items.Clear();
        this.DropDownList5.DataSource = theHelper.DropdownState();
        this.DropDownList5.DataTextField = "State_Name";
        this.DropDownList5.DataValueField = "State_ID";
        this.DropDownList5.DataBind();

    }
    protected void GridView1_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        Label lblEmployeeID = (Label)GridView1.Rows[e.RowIndex].FindControl("EmployeeID");
        theHelper.DeleteEmployeeMaster(Convert.ToInt32(lblEmployeeID.Text));
        lblError.ForeColor = System.Drawing.Color.Green;
        lblError.Text = "Deleted Successfully";
        GridFill();
        ResetAllFields();

    }
    protected void Button1_Click(object sender, EventArgs e)
    {
        if (txtEmployeeName.Text.Trim() == "")
        {
            lblError.ForeColor = System.Drawing.Color.Red;
            lblError.Text = "Employee Name cannot Blank!";
            return;
        }
        if (DropDownList1.SelectedValue.Trim() == "0")
        {
            lblError.ForeColor = System.Drawing.Color.Red;
            lblError.Text = "Designation cannot Blank!";
            return;
        }
        if (DropDownList2.SelectedValue.Trim() == "0")
        {
            lblError.ForeColor = System.Drawing.Color.Red;
            lblError.Text = "Sex cannot Blank!";
            return;
        }
        if (txtphn13.Text.Trim() == "")
        {
            lblError.ForeColor = System.Drawing.Color.Red;
            lblError.Text = "Primary Phone No cannot Blank!";
            return;
        }
        if (Calendar1.Text.Trim() == "")
        {
            lblError.ForeColor = System.Drawing.Color.Red;
            lblError.Text = "Join Date cannot Blank!";
            return;
        }
        if (Calendar3.Text.Trim() == "")
        {
            lblError.ForeColor = System.Drawing.Color.Red;
            lblError.Text = "Birth Date cannot Blank!";
            return;
        }


        string reformattedDate1 = string.Empty;
        System.Globalization.DateTimeFormatInfo dtf = new DateTimeFormatInfo();
        //DateTime testdate = DateTime.ParseExact(Calendar1.Text, "dd/MM/yyyy", dtf);
        // DateTime testdate1 = DateTime.ParseExact(Calendar2.Text, "dd/MM/yyyy", dtf);

        string PhoneNo_1 = txtphn11.Text + " " + txtphn13.Text;
        string PhoneNo_2 = txtphn21.Text + " " + txtphn23.Text;

        if (Button1.Text == "Submit")
        {
            string Empno = theHelper.GenerateStaffID(Session["CoCode"].ToString()).Rows[0][0].ToString();
            theHelper.InsertEmployeeMaster(Session["CoCode"].ToString(), txtEmployeeName.Text.ToUpper(), DropDownList1.SelectedValue, DropDownList2.SelectedValue, txtAddress.Text.ToUpper(), txtCity.Text.ToUpper(), DropDownList5.SelectedValue, PhoneNo_1, PhoneNo_2, Calendar1.Text.ToString(), txtAge.Text, DropDownList3.SelectedValue, DropDownList4.SelectedValue, Calendar2.Text.ToString(), Calendar3.Text, Session["userName"].ToString(), Empno);
            lblError.ForeColor = System.Drawing.Color.Green;
            lblError.Text = "Inserted Successfully!";
        }
        else
        {
            theHelper.UpdateEmployeeMaster(Session["CoCode"].ToString(), HiddenField1.Value, txtEmployeeName.Text.ToUpper(), DropDownList1.SelectedValue, DropDownList2.SelectedValue, txtAddress.Text.ToUpper(), txtCity.Text.ToUpper(), DropDownList5.SelectedValue, PhoneNo_1, PhoneNo_2, Calendar1.Text.ToString(), txtAge.Text, DropDownList3.SelectedValue, DropDownList4.SelectedValue, Calendar2.Text, Calendar3.Text.ToString());
            lblError.ForeColor = System.Drawing.Color.Green;
            lblError.Text = "Updated Successfully!";
            Button1.Text = "Submit";
        }

        ResetAllFields();
        GridFill();

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
    protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            if (Objmain.checkaccss(Session["CoCode"].ToString(), Session["UserRoleID"].ToString().Trim(), "EMPLOYEE MASTER", checkAccessType.DeleteAction) == false)
            {
                e.Row.Cells[16].Visible = false;
            }
        }
    }
}