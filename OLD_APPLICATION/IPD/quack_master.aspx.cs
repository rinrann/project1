using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;

public partial class IPD_quack_master : System.Web.UI.Page
{
    Main Objmain = new Main(ConfigurationSettings.AppSettings["ConnectionString"].ToString());
    string connstring=ConfigurationManager.AppSettings["connectionstring"].ToString();

    quack_master thehelper = new quack_master(ConfigurationManager.AppSettings["connectionstring"].ToString());
    protected void Page_Load(object sender, EventArgs e)
    {
        Page.Title = "Quack Master";
        GridFill();
        if (Session["userName"] == null)
        {
            Response.Redirect("../LoginPage.aspx");
        }
        if (Objmain.checkaccss(Session["CoCode"].ToString(), Session["UserRoleID"].ToString().Trim(), "QUACK MASTER", checkAccessType.ViewAction) == false)
        {
            Response.Redirect("../AccessDenied.aspx");
        }

        if (Objmain.checkaccss(Session["CoCode"].ToString(), Session["UserRoleID"].ToString().Trim(), "QUACK MASTER", checkAccessType.InsertAction) == false)
        {
            Button1.Enabled = false;
        }
        if (!IsPostBack)
        {
            Tab1Func();
            DropDownList1.Items.Clear();
            DropDownList1.Items.Insert(0, new ListItem("--Select--", "0"));
            DropDownList1.Items.Insert(1, new ListItem("Asha", "A"));
            DropDownList1.Items.Insert(2, new ListItem("Car Rented", "R"));
            DropDownList1.Items.Insert(3, new ListItem("Car Private", "P"));
            DropDownList1.Items.Insert(4, new ListItem("Ambulance", "B"));
            DropDownList1.Items.Insert(5, new ListItem("Rural Doctor", "Q"));
            DropDownList1.Items.Insert(6, new ListItem("Pathology Technicaian", "T"));
            DropDownList1.Items.Insert(7, new ListItem("Others", "O"));
        }
    }
    protected void Tab1_Click(object sender, EventArgs e)
    {
        Tab1Func();

    }
    public void Tab1Func()
    {
        Tab1.CssClass = "Clicked";
        Tab2.CssClass = "Initial";
        MainView.ActiveViewIndex = 0;
    }
    private void GridFill()
    {
        GridView1.DataSource = thehelper.GetAllData(Session["CoCode"].ToString().Trim());
        GridView1.DataBind();
    }

    protected void Button1_Click(object sender, EventArgs e)
    {
        string quack_type = DropDownList1.SelectedValue;
        string name = TextBox1.Text;
        string Address = TextBox2.Text.ToUpper();
        string Country = DropDownList2.SelectedValue;
        string state = DropDownList3.SelectedValue;
        string city = TextBox3.Text.ToUpper();
        string Pin = TextBox4.Text;
        string mobile = TextBox6.Text + " " + TextBox7.Text;
        string ph = TextBox8.Text + " " + TextBox9.Text + " " + TextBox10.Text;
        string EmailId = TextBox11.Text;
        string Fax = TextBox12.Text;
        string commpercent = TextBox13.Text;
        string Commission_Rs = TextBox14.Text;
        if (Button1.Text == "Submit")
        {
            DataTable quacktable = thehelper.GenerateQuackID(Session["CoCode"].ToString().Trim());
            if (thehelper.InsertQuack(quacktable.Rows[0][0].ToString(), quack_type, name, Address, Country, state, city, Pin, mobile, ph, EmailId, Fax, commpercent, Commission_Rs, Session["userName"].ToString(), DateTime.Now.ToString("yyyy-MM-dd"), Session["CoCode"].ToString().Trim(), Session["YearCode"].ToString().Trim()) == true)
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "msg", "alert('Inserted Successfully !');", true);
                Button1.Text = "Update";
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "msg", "alert('Error in Inserted Data !');", true);
            }
        }
        else
        {
            if (thehelper.UpdateQuack(HiddenField1.Value, quack_type, name, Address, Country, state, city, Pin, mobile, ph, EmailId, Fax, commpercent, Commission_Rs, Session["userName"].ToString(), DateTime.Now.ToString("yyyy-MM-dd"), Session["CoCode"].ToString().Trim()) == true)
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "msg", "alert('Updated Successfully !');", true);
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "msg", "alert('Error in Updation !');", true);
            }
        }
    }
    protected void Button2_Click(object sender, EventArgs e)
    {
        ResetAllFields();
    }
    protected void Tab2_Click(object sender, EventArgs e)
    {
        Tab2.CssClass = "Clicked";
        Tab1.CssClass = "Initial";
        MainView.ActiveViewIndex = 1;
    }
    protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        lblError.Text = "";
        if (e.CommandName == "Select")
        {
            int index = Convert.ToInt32(e.CommandArgument);


            Label lblDoctorId = (Label)GridView1.Rows[index].FindControl("lblDoctorId");
            HiddenField1.Value = lblDoctorId.Text;

            Label lbltype = (Label)GridView1.Rows[index].FindControl("lbltype");
            DropDownList1.SelectedIndex = SearchIndex(lbltype.Text, DropDownList1);

            Label lblDoctorName = (Label)GridView1.Rows[index].FindControl("lblDoctorName");
            TextBox1.Text = lblDoctorName.Text;

            Label lblAddress = (Label)GridView1.Rows[index].FindControl("lblAddress");
            TextBox2.Text = lblAddress.Text;

            Label lblCountry = (Label)GridView1.Rows[index].FindControl("lblCountry");
            DropDownList2.SelectedIndex = SearchIndex(lblCountry.Text, DropDownList2);


            Label lblstate = (Label)GridView1.Rows[index].FindControl("lblstate");
            DropDownList3.SelectedIndex = SearchIndex(lblstate.Text, DropDownList3);

            Label lblCity = (Label)GridView1.Rows[index].FindControl("lblCity");
            TextBox3.Text = lblCity.Text;

            Label lblPin = (Label)GridView1.Rows[index].FindControl("lblPin");
            TextBox4.Text = lblPin.Text;

            Label lblPhone = (Label)GridView1.Rows[index].FindControl("lblPhone");
            string[] ph = lblPhone.Text.Trim().Split(' ');
            TextBox6.Text = "+91";
            if (ph.Length > 1)
                TextBox7.Text = ph[1];


            Label lbldoc_ph_res = (Label)GridView1.Rows[index].FindControl("lbldoc_ph_res");
            string[] Contracts1 = lbldoc_ph_res.Text.Split(' ');

            TextBox8.Text = Contracts1[0];
            if (Contracts1.Length > 1) { TextBox9.Text = Contracts1[1]; }
            if (Contracts1.Length > 2) { TextBox10.Text = Contracts1[2]; }


            Label lblEmailId = (Label)GridView1.Rows[index].FindControl("lblEmailId");
            TextBox11.Text = lblEmailId.Text;

            Label lblFax = (Label)GridView1.Rows[index].FindControl("lblFax");
            TextBox12.Text = lblFax.Text;
            
            Label lblCommission_Per = (Label)GridView1.Rows[index].FindControl("lblCommission_Per");
            TextBox13.Text = lblCommission_Per.Text;

            Label lblCommission_Rs = (Label)GridView1.Rows[index].FindControl("lblCommission_Rs");
            TextBox14.Text = lblCommission_Rs.Text;


            Tab1Func();
            Button1.Text = "Update";
            if (Objmain.checkaccss(Session["CoCode"].ToString(), Session["UserRoleID"].ToString().Trim(), "QUACK MASTER", checkAccessType.UpdateAction) == false)
            {
                Button1.Enabled = false;
            }
            else
            {
                Button1.Enabled = true;
            }
        }
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
    protected void GridView1_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        Label lblDoctorId = (Label)GridView1.Rows[e.RowIndex].FindControl("lblDoctorId");
        if(thehelper.DeleteQuack(lblDoctorId.Text, Session["CoCode"].ToString().Trim())==true)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "msg", "alert('Deleted Successfully !');", true);
        }
        else
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "msg", "alert('Error in Deletion !');", true);
        }
        //lblError.ForeColor = System.Drawing.Color.Green;
        //lblError.Text = "Deleted Successfully";
        GridFill();
        ResetAllFields();
        Tab1Func();
        Button1.Text = "Submit";
    }
    protected void ResetAllFields()
    {
        TextBox1.Text = ""; TextBox2.Text = ""; TextBox3.Text = ""; TextBox4.Text = "";  TextBox7.Text = ""; TextBox9.Text = ""; TextBox10.Text = "";
        TextBox14.Text = "";
        TextBox11.Text = "";
        TextBox12.Text = "";
        TextBox13.Text = "";
        Button1.Text = "Submit";
        DropDownList1.SelectedIndex = 0;
        DropDownList3.SelectedIndex = 0;
        if (Objmain.checkaccss(Session["CoCode"].ToString(), Session["UserRoleID"].ToString().Trim(), "QUACK MASTER", checkAccessType.InsertAction) == false)
        {
            Button1.Enabled = false;
        }
    }
    protected void GridView1_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        GridView1.PageIndex = e.NewPageIndex;
        GridFill();
    }

    [System.Web.Script.Services.ScriptMethod()]
    [System.Web.Services.WebMethod]
    public static List<string> Searchquacktype(string prefixText, int count)
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
                cmd.CommandText = "select distinct QuackName as Name from GN_QuackMaster where compcode=@Compcode and QuackName like @SearchText +'%'";
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
    protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            if (Objmain.checkaccss(Session["CoCode"].ToString(), Session["UserRoleID"].ToString().Trim(), "QUACK MASTER", checkAccessType.DeleteAction) == false)
            {
                coldel.Visible = false;
                e.Row.Cells[15].Visible = false;
            }

        }
    }
}