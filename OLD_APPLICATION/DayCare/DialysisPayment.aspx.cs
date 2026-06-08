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
 
public partial class DayCare_DialysisPayment : System.Web.UI.Page
{
    Main Objmain = new Main(ConfigurationSettings.AppSettings["ConnectionString"].ToString());
    DC_DialysisPayment thepdia = new DC_DialysisPayment(ConfigurationSettings.AppSettings["ConnectionString"].ToString());
    AnyTimePayment anypayment = new AnyTimePayment(ConfigurationSettings.AppSettings["ConnectionString"].ToString());
    static int flag; 
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["userName"] == null)
        {
            Response.Redirect("../LoginPage.aspx");
        }
        if (Objmain.checkaccss(Session["CoCode"].ToString(), Session["UserRoleID"].ToString().Trim(), "DIALYSIS PAYMENT", checkAccessType.ViewAction) == false)
        {
            Response.Redirect("../AccessDenied.aspx");
        }

        if (Objmain.checkaccss(Session["CoCode"].ToString(), Session["UserRoleID"].ToString().Trim(), "DIALYSIS PAYMENT", checkAccessType.InsertAction) == false)
        {
            Button1.Enabled = false;
        }
        Page.Title = "Dialysis Payment";
        if (!IsPostBack)
        {

            ScriptManager.RegisterStartupScript(this, this.GetType(), "msg", "GetDatetime();", true);
            flag = 0; 
            FillDrop();
            DropDownList2.Enabled = false; 
            if (Session["RegnNo"]!=null)
            {
                txtreg.Text = Session["RegnNo"].ToString();
                Fill();
            }
        }

        Session["RegnNo"] = null;
    }


    [System.Web.Services.WebMethod]
    public static string SessionFill(string name)
    {
        return "Hello " + name + Environment.NewLine + "The Current Time is     :-   " + DateTime.Now.ToString("dd/MM/yyyy");
    }

    public void FillDrop()
    {
       this.DropDownList1.DataSource = thepdia.DropdownShift();
       this.DropDownList1.DataTextField = "ShiftName";
       this.DropDownList1.DataValueField = "ShiftID";
       this.DropDownList1.DataBind();
       this.DropDownList1.Items.Insert(0, new ListItem("--Select--", "0"));

        //Code by sankar
       string cocode = Session["CoCode"].ToString().Trim();
       string yearcode = Session["YearCode"].ToString().Trim();
       DropDownList3.Items.Clear();
       this.DropDownList3.DataSource = anypayment.DropdownPaymentMode(cocode);
       this.DropDownList3.DataTextField = "Name";
       this.DropDownList3.DataValueField = "ID";
       this.DropDownList3.DataBind();
       this.DropDownList3.Items.Insert(0, new ListItem("--Select--", "0"));


       txtreason.Items.Clear();
       txtreason.Items.Insert(0, new ListItem("--Select--", "0"));
       txtreason.Items.Insert(1, new ListItem("Discharge Payment", "Discharge Payment"));
       txtreason.Items.Insert(2, new ListItem("Advance Payment", "Advance Payment"));
       txtreason.Items.Insert(3, new ListItem("Other", "Other"));

    }

    public void Fill()
    {
        Button1.Enabled = true;
        double advanceamt;   
        try
        {
            if (txtreg.Text.Trim() != "")
            {
                DataTable custTable = thepdia.GetDialysisDetails(txtreg.Text, Session["CoCode"].ToString().Trim());
                DataTable custTable1 = thepdia.GetAmount(txtreg.Text, Session["CoCode"].ToString().Trim());

                advanceamt = Convert.ToDouble(custTable1.Rows[0]["Advance"]);

                double due = Convert.ToDouble(custTable1.Rows[0]["Due"]);
                TextBox4.Value = custTable1.Rows[0]["Due"].ToString();
                txtpaid.Text = advanceamt.ToString();
                if (due >= 0)
                {
                    txtamt.Enabled = true;
                    Label3.Text = "Due Amount :";
                    txtdue.Text = due.ToString();

                    Label5.Text = "Due Amount :";
                    TextBox3.Text = due.ToString();
                    TextBox3.ForeColor = System.Drawing.Color.Red;
                    txtdue.ForeColor = System.Drawing.Color.Red; 
                }
                else
                {
                    TextBox3.ForeColor = System.Drawing.Color.Green;
                    txtdue.ForeColor = System.Drawing.Color.Green;
                    txtamt.Enabled = false;
                    Label5.Text = "Amount to Refund :";
                    TextBox3.Text = (-due).ToString();
                    Label3.Text = "Amount to Refund :";
                    txtdue.Text = (-due).ToString(); 
                }

                if (custTable.Rows.Count > 0)
                {
                    txtname.Text = custTable.Rows[0]["DialysisName"].ToString();
                    txtpname.Text = custTable.Rows[0]["patient_name"].ToString();
                    DropDownList1.SelectedValue = custTable.Rows[0]["ShiftId"].ToString();
                    TextBox2.Text = custTable.Rows[0]["Debit"].ToString();
                }
                else
                {
                    lblError.ForeColor = System.Drawing.Color.Red;
                    lblError.Text = "Please choose valid Patient"; 
                }
            }
            else
            {
                lblError.ForeColor = System.Drawing.Color.Red;
                lblError.Text = "Please choose Customer";
            }
 
        }
        catch (Exception ex)
        {
            lblError.ForeColor = System.Drawing.Color.Red;
            lblError.Text = ex.Message;
        }
    }
    protected void Button5_Click(object sender, EventArgs e)
    {
        Fill();

    }
 
    public void ResetAllFields()
    {
        txtreg.Text = ""; 
        txtname.Text = "";
        txtamt.Text = "";
        TextBox2.Text = "";
        TextBox3.Text = ""; 
        //TextBox8.Text = "";
        TextBox5.Text = ""; 
        TextBox1.Text = ""; 
        txtpaid.Text = "";
        txtpname.Text = "";
        txtdue.Text = "";
        CheckBox1.Checked = false;
        DropDownList1.SelectedIndex = 0;
        DropDownList1.Enabled = false;
        if (Objmain.checkaccss(Session["CoCode"].ToString(), Session["UserRoleID"].ToString().Trim(), "DIALYSIS PAYMENT", checkAccessType.InsertAction) == false)
        {
            Button1.Enabled = false;
        }
    }
    protected void Button2_Click(object sender, EventArgs e)
    {
        ResetAllFields();
    }
  
    protected void txtamt_TextChanged(object sender, EventArgs e)
    {
        double currentPayment = 0;
        double discount = 0;
        double amount = 0;
        if (txtamt.Text != "")
            amount = Convert.ToDouble(txtamt.Text);
        else
            amount = 0;
        if (TextBox5.Text != "")
           discount= Convert.ToDouble(TextBox5.Text);
        else
            discount = 0;
        if (TextBox4.Value != "")
        {
            currentPayment = Convert.ToDouble(TextBox4.Value) - amount - discount;
        }
        txtdue.Text = currentPayment.ToString();
        TextBox3.Text = currentPayment.ToString();
    }
    protected void TextBox5_TextChanged(object sender, EventArgs e)
    {
        double currentPayment = 0;
        double current = 0;
        double discount = 0;
        if (TextBox5.Text != "")
            discount = Convert.ToDouble(TextBox5.Text);
        else
            discount = 0;


        if (txtamt.Text != "")
            current = Convert.ToDouble(txtamt.Text);
        else
            current = 0;
        if (TextBox4.Value != "")
        {
            if (Label3.Text == "Amount to Refund :")
                currentPayment = -(Convert.ToDouble(TextBox4.Value) - discount - current);
            else
                currentPayment = Convert.ToDouble(TextBox4.Value) -discount - current;
        }
        txtdue.Text = currentPayment.ToString();
        TextBox3.Text = currentPayment.ToString();
    }
    protected void Button1_Click(object sender, EventArgs e)
    {
        Button1.Enabled = false;
        string currentpayment = "0";
        string dueamount = "0";
        string discount = "0";
        string refund = "0";
        if (txtamt.Text == "")
        {
            currentpayment = "0";
        }
        else
            currentpayment = txtamt.Text;


        if (TextBox5.Text == "")
        {
            discount = "0";
        }
        else
            discount = TextBox5.Text;


        if (Label3.Text == "Due Amount :")
        {
            if (txtdue.Text != "" && CheckBox1.Checked == true)
            {

                dueamount = txtdue.Text;
            }
            else
                dueamount = "0";
        }
        else
        {
            if (txtdue.Text == "")
            {
                refund = "0";
            }
            else
                refund = txtdue.Text;
        }

        if (Convert.ToDouble(txtdue.Text) >= 0)
        {
            if (TextBox4.Value != "")
            {

                DataTable receiprcode = anypayment.GenerateReceiptCode(Session["CoCode"].ToString().Trim());
                if (thepdia.InsertDialysisPayment(Session["CoCode"].ToString().Trim(), Session["yearcode"].ToString().Trim(), TextBox8.Text, receiprcode.Rows[0][0].ToString(), Convert.ToInt16( currentpayment), dueamount, discount, refund, txtreg.Text, txtpname.Text, HiddenField1.Value, Session["userName"].ToString(),DropDownList3.SelectedValue,txtreason.SelectedValue.ToString()) == true)
                {
                    Session["RegNo"] = txtreg.Text;
                    ResetAllFields();
                    //ScriptManager.RegisterStartupScript(this, this.GetType(), "msg", "ShowDialog1();", true);
                    Response.Redirect("../DayCare/PatientDashBoard.aspx");
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "msg", "alert('Error in Inserted Data !');", true);
                }
            }

        }
        else
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "msg", "alert('Due amount not to be Negative !');", true);
        }

       // Response.Redirect("../DayCare/PatientDashBoard.aspx");
    }

    protected void txtreason_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (txtreason.SelectedValue == "Other")
        {
            TextBox8.Visible = true;

        }
        else
        {
            TextBox8.Visible = false;
        }
    }
}