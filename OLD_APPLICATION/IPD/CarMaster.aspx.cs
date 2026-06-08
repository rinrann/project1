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


public partial class IPD_CarMaster : System.Web.UI.Page
{
    Main Objmain = new Main(ConfigurationSettings.AppSettings["ConnectionString"].ToString());
    CarMaster theHelper = new CarMaster(ConfigurationSettings.AppSettings["ConnectionString"].ToString());
    protected void Page_Load(object sender, EventArgs e)
    {
        
        Page.Title = "Car Details";
        if (Session["userName"] == null)
        {
            Response.Redirect("../LoginPage.aspx");
        }
        if (Objmain.checkaccss(Session["CoCode"].ToString(), Session["UserRoleID"].ToString().Trim(), "CAR MASTER", checkAccessType.ViewAction) == false)
        {
            Response.Redirect("../AccessDenied.aspx");
        }

        if (Objmain.checkaccss(Session["CoCode"].ToString(), Session["UserRoleID"].ToString().Trim(), "CAR MASTER", checkAccessType.InsertAction) == false)
        {
            Button1.Enabled = false;
        }
        if (Objmain.checkaccss(Session["CoCode"].ToString(), Session["UserRoleID"].ToString().Trim(), "CAR MASTER", checkAccessType.DeleteAction) == false)
        {
            GridView1.Columns[7].Visible = false;
        }
        if (!IsPostBack)
        {
            GridFill();
            DropDownFill();
            Tab1Func();
        }
       
    }
    public void GridFill()
    {
        GridView1.DataSource = theHelper.GetAllCar(Session["CoCode"].ToString().Trim());
        GridView1.DataBind();
    }
    private void ResetAllFields()
    {
        DropDownList2.SelectedIndex = -1;
        txtName.Text = "";
        txtAddress.Text = "";
        DropDownList1.SelectedIndex = -1;
        txtPinNo.Text = "";
        txtPhNo.Text = "";
        Button1.Text = "Submit";

        if (Objmain.checkaccss(Session["CoCode"].ToString(), Session["UserRoleID"].ToString().Trim(), "CAR MASTER", checkAccessType.InsertAction) == false)
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

            Label lblId = (Label)GridView1.Rows[index].FindControl("lblId");
            HiddenField1.Value = lblId.Text;

           

            Label lblName = (Label)GridView1.Rows[index].FindControl("Name");
            txtName.Text = lblName.Text;

            Label lblAddress = (Label)GridView1.Rows[index].FindControl("Address");
            txtAddress.Text = lblAddress.Text;

            DropDownFill();

            Label lblCarType = (Label)GridView1.Rows[index].FindControl("CarType");
            DropDownList2.SelectedValue = lblCarType.Text;

            Label lblDistrict = (Label)GridView1.Rows[index].FindControl("District");
            //DropDownList1.SelectedIndex = DropDownList1.Items.IndexOf(DropDownList1.Items.FindByText(lblModuleID.Text));
            DropDownList1.SelectedValue = lblDistrict.Text;

            Label lblPattText = (Label)GridView1.Rows[index].FindControl("PinNo");
            txtPinNo.Text = lblPattText.Text;

            Label lblPhNoText = (Label)GridView1.Rows[index].FindControl("PhoneNo");
            txtPhNo.Text = lblPhNoText.Text;
            Tab1Func();
            Button1.Text = "Update";
            if (Objmain.checkaccss(Session["CoCode"].ToString(), Session["UserRoleID"].ToString().Trim(), "CAR MASTER", checkAccessType.UpdateAction) == false)
            {
                Button1.Enabled = false;
            }
            else
            {
                Button1.Enabled = true;
            }
        }
    }
    private void DropDownFill()
    {
        DropDownList1.Items.Clear();
        DropDownList2.Items.Clear();
        this.DropDownList1.DataSource = theHelper.DropdownID(Session["CoCode"].ToString().Trim());
        this.DropDownList1.DataTextField = "DistrictName";
        this.DropDownList1.DataValueField = "ID";
        
        this.DropDownList1.DataBind();
        this.DropDownList1.Items.Insert(0, new ListItem("--Select--", "0"));
        
        this.DropDownList2.Items.Insert(0, new ListItem("--Select--", "0"));
       this.DropDownList2.Items.Insert(1, new ListItem("Car Rented", "R"));
       this.DropDownList2.Items.Insert(2, new ListItem("Car Private", "P"));
       this.DropDownList2.Items.Insert(3, new ListItem("Ambulance", "B"));



       
        //this.DropDownList1.Items.Insert(0, new ListItem("--Select--", "0"));
        //this.DropDownList2.Items.Insert(0, new ListItem("--Select--", "0"));
    }
    protected void GridView1_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        Label lblId = (Label)GridView1.Rows[e.RowIndex].FindControl("lblId");
        //theHelper.DeleteCarMaster(Convert.ToString(lblName.Text),(Convert.ToString(lblCarType.Text) Session["CoCode"].ToString().Trim());
        theHelper.DeleteCarMaster(Convert.ToInt32(lblId.Text), Session["CoCode"].ToString().Trim());
        lblError.ForeColor = System.Drawing.Color.Green;
        //lblError.Text = "Deleted Successfully";
        ScriptManager.RegisterStartupScript(this, this.GetType(), "msg", "alert('Deleted  Successfully !');", true);
        GridFill();
        ResetAllFields();

    }
    protected void Button1_Click(object sender, EventArgs e)
    {

        if (Button1.Text == "Submit")
        {
            theHelper.InsertCarMaster(DropDownList2.SelectedValue.ToString(), txtName.Text, txtAddress.Text, DropDownList1.SelectedValue.ToString(), txtPinNo.Text, Session["CoCode"].ToString().Trim(), txtPhNo.Text);
            //theHelper.InsertCarMaster(DropDownList2.SelectedValue, txtName.Text, txtAddress.Text, DropDownList1.SelectedValue, txtPinNo.Text, Session["CoCode"].ToString().Trim(), txtPhNo.Text);
            ScriptManager.RegisterStartupScript(this, this.GetType(), "msg", "alert('Inserted  Successfully !');", true);
            
        }
        else
        {
                theHelper.UpdateCarMaster(Session["CoCode"].ToString().Trim(), Convert.ToInt32(HiddenField1.Value.ToString().Trim()), DropDownList2.SelectedValue, txtName.Text, txtAddress.Text, DropDownList1.SelectedValue, txtPinNo.Text, txtPhNo.Text);
                ScriptManager.RegisterStartupScript(this, this.GetType(), "msg", "alert('Updated  Successfully !');", true);
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
        ResetAllFields();
    }
    protected void Tab2_Click(object sender, EventArgs e)
    {
        Tab1.CssClass = "Initial";
        Tab2.CssClass = "Clicked";
        MainView.ActiveViewIndex = 1;
    }

    [System.Web.Script.Services.ScriptMethod()]
    [System.Web.Services.WebMethod]
    public static List<string> SearchWing(string prefixText, int count)
    {
        using (SqlConnection conn = new SqlConnection())
        {
            conn.ConnectionString = ConfigurationSettings.AppSettings["ConnectionString"].ToString();
            using (SqlCommand cmd = new SqlCommand())
            {
                cmd.CommandText = "select distinct Name as Name from GN_CarMaster where Name like @SearchText +'%'";
                cmd.Parameters.AddWithValue("@SearchText", prefixText);
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
    protected void GridView1_SelectedIndexChanged(object sender, EventArgs e)
    {

    }
    protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            if (Objmain.checkaccss(Session["CoCode"].ToString(), Session["UserRoleID"].ToString().Trim(), "CAR MASTER", checkAccessType.DeleteAction) == false)
            {
                coldel.Visible = false;
                e.Row.Cells[7].Visible = false;
            }
        }
    }
}
