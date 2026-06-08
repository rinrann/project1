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

public partial class DayCare_PaymentSubmitpopup : System.Web.UI.Page
{
    DC_DialysisPayment thepdia = new DC_DialysisPayment(ConfigurationSettings.AppSettings["ConnectionString"].ToString());
    double due_refund;
    protected void Page_Load(object sender, EventArgs e)
    {
       
        if (!IsPostBack)
        {
            due_refund = 0; 
            txtreg.Text = Session["RegNo"].ToString();
        }
        DataTable custTable = thepdia.GetDialysisDetails(txtreg.Text, Session["CoCode"].ToString().Trim());
        DataTable custTable1 = thepdia.GetAmount(txtreg.Text, Session["CoCode"].ToString().Trim());
        DataTable refund_discount = thepdia.Get_Refund_Discount(Session["CoCode"].ToString().Trim(), Session["YearCode"].ToString().Trim(), txtreg.Text);

            TextBox2.Text = custTable.Rows[0]["Debit"].ToString();
            TextBox1.Text = TextBox2.Text;
            txtpname.Text = custTable.Rows[0]["patient_name"].ToString(); 
            txtpaid.Text = custTable1.Rows[0]["Advance"].ToString();
            TextBox8.Text = refund_discount.Rows[0]["Discount"].ToString();
            if (refund_discount.Rows[0]["Refund"].ToString() != "0.00")
            {
                due_refund = 1;
                TextBox6.ForeColor = System.Drawing.Color.Green;
                Label1.Text = "Amount to Refund :";
                TextBox6.Text = refund_discount.Rows[0]["Refund"].ToString(); 
            }
            else
            {
                due_refund = 0;
                Label1.Text = "Due Amount :";
                TextBox6.Text = custTable1.Rows[0]["DueCF"].ToString();
                TextBox6.ForeColor = System.Drawing.Color.Red; 
            }
            string[] split = TextBox6.Text.Split('(');
        if(due_refund==1)
            TextBox3.Text = (Convert.ToDouble(txtpaid.Text) - Convert.ToDouble(split[0]) + Convert.ToDouble(TextBox8.Text)).ToString();
        else
            TextBox3.Text = (Convert.ToDouble(txtpaid.Text) + Convert.ToDouble(split[0]) + Convert.ToDouble(TextBox8.Text)).ToString();

        if (Session["userName"] == null)
        {
            Response.Redirect("../LoginPage.aspx");
        }

        Session["RegNo"] = null;
    }
    
  
   
}