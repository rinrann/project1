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
using System.Web.Services;
using System.Globalization;

public partial class IPD_referbypopup : System.Web.UI.Page
{
    PatientAdmission thepd = new PatientAdmission(ConfigurationSettings.AppSettings["ConnectionString"].ToString());
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            Panel1.Visible = false; Panel2.Visible = false;
            DropDownList3.Enabled = false;
            TextBox4.Enabled = false;
            DropDownFill();
            DropDownList4.Visible = false;
        }
    }
    protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        Label lblregno = (Label)GridView1.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("lblregno");
        HiddenField1.Value = lblregno.Text;
        Label lblName = (Label)GridView1.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("lblName");
        HiddenField2.Value = lblName.Text + "#" + DropDownList1.SelectedValue;
    }
    protected void GridView1_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {

    }

    public void DropDownFill()
    {
        this.DropDownList1.Items.Clear();
        // this.DropDownList1.DataSource = thepd.GetReferBy();
        // this.DropDownList1.DataTextField = "Name";
        //this.DropDownList1.DataValueField = "ID";
        // this.DropDownList1.DataBind();
        // this.DropDownList1.Items.Insert(0, new ListItem("--Select--", "0"));

        this.DropDownList1.Items.Insert(0, new ListItem("--Select--", "0"));
        this.DropDownList1.Items.Insert(1, new ListItem("GFC", "G"));
        this.DropDownList1.Items.Insert(2, new ListItem("Self", "S"));
        this.DropDownList1.Items.Insert(3, new ListItem("Consultant Doctor", "D"));
        this.DropDownList1.Items.Insert(4, new ListItem("Asha", "A"));
        this.DropDownList1.Items.Insert(5, new ListItem("Car Rented", "R"));
        this.DropDownList1.Items.Insert(6, new ListItem("Car Private", "P"));
        this.DropDownList1.Items.Insert(7, new ListItem("Ambulance", "B"));
        this.DropDownList1.Items.Insert(8, new ListItem("Rural Doctor", "Q"));
        this.DropDownList1.Items.Insert(9, new ListItem("Others", "O"));



        this.DropDownList2.Items.Clear();
        this.DropDownList2.DataSource = thepd.DropDownDistrict(Session["CoCode"].ToString().Trim());
        this.DropDownList2.DataTextField = "DistrictName";
        this.DropDownList2.DataValueField = "ID";
        this.DropDownList2.DataBind();
        this.DropDownList2.Items.Insert(0, new ListItem("--Select--", "0"));




        this.DropDownList4.Items.Clear();
        this.DropDownList4.DataSource = thepd.GetDoctorType(Session["CoCode"].ToString().Trim());
        this.DropDownList4.DataTextField = "TypeName";
        this.DropDownList4.DataValueField = "DocTypeId";
        this.DropDownList4.DataBind();
        this.DropDownList4.Items.Insert(0, new ListItem("--Select--", "0"));


    }
    protected void Tab1_Click(object sender, EventArgs e)
    {
        CallTab1();
    }

    public void CallTab1()
    {
        Tab1.CssClass = "Clicked";
        Tab2.CssClass = "Initial";
        MainView.ActiveViewIndex = 0;
    }

    protected void Tab2_Click(object sender, EventArgs e)
    {
        Tab1.CssClass = "Initial";
        Tab2.CssClass = "Clicked";
        MainView.ActiveViewIndex = 1;

    }
    protected void DropDownList1_SelectedIndexChanged(object sender, EventArgs e)
    {
        CallTab1();
        if (DropDownList1.SelectedValue == "D")
        {
            Reset();
            Panel1.Visible = true;
            Panel2.Visible = false;
            Button4.Enabled = true;
            txtname.Enabled = true;
            DropDownList3.Enabled = false;
            DropDownList3.Visible = false;
            DropDownList4.Enabled = true;
            DropDownList4.Visible = true;
            TextBox4.Enabled = false;
            TextBox4.Visible = false;
            lblvchltype.Visible = false;
            lbldoctype.Visible = true;
            lblvchlno.Visible = false;
            gridFill("D");
        }
        else if (DropDownList1.SelectedValue == "A")
        {
            Reset();
            Panel1.Visible = true;
            Button4.Enabled = true;
            Panel2.Visible = false;
            txtname.Enabled = true;
            DropDownList3.Enabled = false;
            DropDownList3.Visible = false;
            DropDownList4.Enabled = false;
            DropDownList4.Visible = false;
            TextBox4.Enabled = false;
            TextBox4.Visible = false;
            lblvchltype.Visible = false;
            lbldoctype.Visible = false;
            lblvchlno.Visible = false;
            gridFill("A");
        }
        else if (DropDownList1.SelectedValue == "R")
        {
            Reset();
            Panel1.Visible = true;
            Button4.Enabled = true;
            Panel2.Visible = false;
            txtname.Enabled = true;
            DropDownList3.Enabled = false;
            DropDownList3.Visible = false;
            DropDownList4.Enabled = false;
            DropDownList4.Visible = false;
            TextBox4.Enabled = true;
            TextBox4.Visible = false;
            lblvchltype.Visible = false;
            lbldoctype.Visible = false;
            lblvchlno.Visible = false;
            gridFill("R");
        }
        else if (DropDownList1.SelectedValue == "P")
        {
            Reset();
            Panel1.Visible = true;
            Button4.Enabled = true;
            Panel2.Visible = false;
            txtname.Enabled = true;
            DropDownList3.Enabled = false;
            DropDownList3.Visible = false;
            DropDownList4.Enabled = false;
            DropDownList4.Visible = false;
            TextBox4.Enabled = true;
            TextBox4.Visible = false;
            lblvchltype.Visible = false;
            lbldoctype.Visible = false;
            lblvchlno.Visible = false;
            gridFill("P");
        }
        else if (DropDownList1.SelectedValue == "B")
        {
            Reset();
            Panel1.Visible = true;
            Button4.Enabled = true;
            Panel2.Visible = false;
            txtname.Enabled = true;
            DropDownList3.Enabled = false;
            DropDownList3.Visible = false;
            DropDownList4.Enabled = false;
            DropDownList4.Visible = false;
            TextBox4.Enabled = true;
            TextBox4.Visible = false;
            lblvchltype.Visible = false;
            lbldoctype.Visible = false;
            lblvchlno.Visible = false;
            gridFill("B");
        }
        else if (DropDownList1.SelectedValue == "Q")
        {
            Reset();
            Panel1.Visible = true;
            Button4.Enabled = true;
            Panel2.Visible = false;
            txtname.Enabled = true;
            DropDownList3.Enabled = false;
            DropDownList3.Visible = false;
            DropDownList4.Enabled = false;
            DropDownList4.Visible = false;
            TextBox4.Enabled = false;
            TextBox4.Visible = false;
            lblvchltype.Visible = false;
            lbldoctype.Visible = false;
            lblvchlno.Visible = false;
            gridFill("Q");
        }
        else if (DropDownList1.SelectedValue == "O")
        {
            Reset();
            Panel1.Visible = true;
            Button4.Enabled = true;
            Panel2.Visible = false;
            txtname.Enabled = true;
            DropDownList3.Enabled = false;
            DropDownList3.Visible = false;
            DropDownList4.Enabled = false;
            DropDownList4.Visible = false;
            TextBox4.Enabled = false;
            TextBox4.Visible = false;
            lblvchltype.Visible = false;
            lbldoctype.Visible = false;
            lblvchlno.Visible = false;
            gridFill("O");
        }
        else
        {
            Panel2.Visible = true;
            Panel1.Visible = false;
        }


    }

    public void gridFill(string id)
    {
        GridView1.DataSource = thepd.GridFillDetails(Session["CoCode"].ToString().Trim(),txtname.Text, id);
        GridView1.DataBind();
    }

    public void Reset()
    {
        DropDownList4.SelectedIndex = 0; DropDownList2.SelectedIndex = 0; DropDownList3.SelectedIndex = 0;
        TextBox1.Text = ""; TextBox2.Text = ""; TextBox3.Text = ""; TextBox5.Text = ""; TextBox7.Text = "";
        TextBox9.Text = "";
        TextBox4.Text = "";
    }


    protected void Button4_Click(object sender, EventArgs e)
    {
        gridFill(DropDownList1.SelectedValue);
    }

    protected void Button2_Click(object sender, EventArgs e)
    {
        string ph1 = "+91" + " " + TextBox7.Text;
        string ph2 = "+91" + " " + TextBox9.Text;
        string doctorflag = "";
        bool quackflag;
        string carnoflag = "";
        string type = DropDownList1.SelectedValue.ToString();
        if (type == "D")
        {
            if (DropDownList4.SelectedIndex == 0)
            {
                Label1.ForeColor = System.Drawing.Color.Green;
                Label1.Text = "Please Enter Doctor Type ! Understand ?";
            }
            else
            {
                doctorflag = thepd.InsertDoctor(DropDownList4.SelectedValue, TextBox1.Text.Trim().ToUpper(), TextBox2.Text.Trim().ToUpper(), TextBox3.Text.Trim().ToUpper(), DropDownList2.SelectedValue, TextBox5.Text, ph1, ph2, Session["userName"].ToString(), DateTime.Now.ToString("yyyy-MM-dd"), Session["CoCode"].ToString().Trim(), Session["YearCode"].ToString().Trim());
                if (doctorflag == "Successfull")
                {
                    Label1.ForeColor = System.Drawing.Color.Green;
                    Label1.Text = "Inserted Successfully";
                    Reset();
                }
                else
                {
                    if (doctorflag == "Duplicate")
                    {
                        Label1.ForeColor = System.Drawing.Color.Red;
                        Label1.Text = "Already Exist !";
                    }
                    else
                    {
                        Label1.ForeColor = System.Drawing.Color.Red;
                        Label1.Text = "Error in Inserted Data";
                    }
                }

                gridFill("3");
            }
        }

        if (type == "A" || type == "R" || type == "P" || type == "B" || type == "Q" || type == "O")
        {
            DataTable quacktable = thepd.GenerateQuackID(Session["CoCode"].ToString().Trim());
            string qckid = quacktable.Rows[0][0].ToString();
            quackflag = thepd.InsertQuack(quacktable.Rows[0][0].ToString(), TextBox1.Text.Trim().ToUpper(), TextBox2.Text.Trim().ToUpper(), TextBox3.Text.Trim().ToUpper(), DropDownList2.SelectedValue.ToString(), TextBox5.Text, ph1, ph2, type, Session["userName"].ToString(), Session["CoCode"].ToString().Trim());
            if (quackflag == true)
            {
                Label1.ForeColor = System.Drawing.Color.Green;
                Label1.Text = "Inserted Successfully";
                Reset();
            }
            else
            {

                Label1.ForeColor = System.Drawing.Color.Red;
                Label1.Text = "Error in Inserted Data";

            }

            gridFill(type);
        }

        if (DropDownList1.SelectedValue == "5")
        {
            carnoflag = thepd.InsertCar(TextBox1.Text.Trim().ToUpper(), TextBox2.Text.Trim().ToUpper(), TextBox3.Text.Trim().ToUpper(), DropDownList2.SelectedValue, TextBox5.Text, ph1, ph2, DropDownList3.SelectedValue, TextBox4.Text, Session["CoCode"].ToString().Trim(), Session["userName"].ToString());
            if (carnoflag == "Successfull")
            {
                Label1.ForeColor = System.Drawing.Color.Green;
                Label1.Text = "Inserted Successfully";
                Reset();
            }
            else
            {
                if (carnoflag == "Duplicate")
                {
                    Label1.ForeColor = System.Drawing.Color.Red;
                    Label1.Text = "Already Exist !";
                }
                else
                {
                    Label1.ForeColor = System.Drawing.Color.Red;
                    Label1.Text = "Error in Inserted Data";
                }
            }

            gridFill("5");
        }

    }
    protected void Button3_Click(object sender, EventArgs e)
    {
        Reset();
    }
    protected void Button5_Click(object sender, EventArgs e)
    {
        HiddenField1.Value = "";
        HiddenField2.Value = TextBox10.Text + "#" + DropDownList1.SelectedValue;
        ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "MyFun1", "CloseDialog();", true);
    }
}