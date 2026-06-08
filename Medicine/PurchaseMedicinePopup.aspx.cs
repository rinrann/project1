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
using System.Globalization;
using System.Web.Services;
 
public partial class Medicine_PurchaseMedicinePopup : System.Web.UI.Page
{
    MD_PurchaseMedicinePopup thereg = new MD_PurchaseMedicinePopup(ConfigurationSettings.AppSettings["ConnectionString"].ToString());
    static string code;

    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["userName"] == null)
        {
            Response.Redirect("../LoginPage.aspx");
        }

        if (!IsPostBack)
        {
            RadioButtonList1.SelectedValue = "Supplier Wise";
            code = "";
            GridFillFirst();
            DropDownFill();
            supplier.Visible = true;
            medicine.Visible = false;
            TextBox7.Visible = true;
            TextBox8.Visible = false;
            lbltext.Text = "Supplier Name";
            lblComp.Text = Session["CoCode"].ToString().Trim();
        }

    }


    public void GridFillFirst()
    {
        string frmdt;
        string todt;
        string opt;
        System.Globalization.DateTimeFormatInfo dtf = new DateTimeFormatInfo();
        if (TextBox5.Text != "")
        {
            frmdt = DateTime.ParseExact(TextBox5.Text, "dd/MM/yyyy", dtf).ToString("yyyy-MM-dd");
        }
        else
        {
            frmdt = "";
        }
        if (TextBox6.Text != "")
        {
            todt = DateTime.ParseExact(TextBox6.Text, "dd/MM/yyyy", dtf).ToString("yyyy-MM-dd");
        }
        else
        {
            todt = "";
        }
        if (RadioButtonList1.SelectedValue == "Reagent Wise")
        {
            opt = "G";
        }
        else
        {
            opt = "M";
        }
        GridView1.DataSource = thereg.GridFillFirst(frmdt, todt, TextBox7.Text,Session["CoCode"].ToString().Trim(), Session["YearCode"].ToString().Trim());
        GridView1.DataBind();
        GridView2.DataSource = thereg.getMedicine(frmdt, todt, TextBox8.Text, Session["CoCode"].ToString().Trim(), Session["YearCode"].ToString().Trim(), opt);
        GridView2.DataBind();
    }


    public void GridFill()
    {
        string testdate;
        System.Globalization.DateTimeFormatInfo dtf = new DateTimeFormatInfo();
        if (TextBox2.Text != "")
        {
            testdate = DateTime.ParseExact(TextBox2.Text, "dd/MM/yyyy", dtf).ToString("yyyy-MM-dd");
        }
        else
        {
            testdate = "null";
        }
        string opt;
        if (RadioButtonList1.SelectedValue == "Supplier Wise")
        {
            opt = "s";
        }
        else
        {
            opt = "m";
           /* if (TextBox2.Text != "")
            {
                string[] aa = TextBox2.Text.Split('/');
                string fday = aa[0];
                string fmonth = aa[1];
                string fyear = aa[2];
                if (fday.Length == 1)
                    fday = "0" + fday;
                if (fmonth.Length == 1)
                    fmonth = "0" + fmonth;
                // frmdate = fday + "/" + fmonth + "/" + fyear;
                testdate = fyear + fmonth + fday;
            }
            else
            {
                testdate = "";
            }*/

        }

        //GridView_popup.DataSource = thereg.GridPopup(Session["CoCode"].ToString().Trim(), Session["YearCode"].ToString().Trim(), TextBox1.Text, code, testdate, TextBox3.Text);
        GridView_popup.DataSource = thereg.GridPopupsupMed(Session["CoCode"].ToString().Trim(), Session["YearCode"].ToString().Trim(),TextBox1.Text, code, testdate.ToString(), TextBox3.Text, opt);
        GridView_popup.DataBind();
    }
    protected void Button2_Click(object sender, EventArgs e)
    {
        GridFill();
    }

    private void DropDownFill()
    {
        //DropDownList1.Items.Clear();
        //DropDownList1.DataSource = thereg.DropdownID4();
        //DropDownList1.DataTextField = "SName";
        //DropDownList1.DataValueField = "SCode";
        //DropDownList1.DataBind();
        //DropDownList1.Items.Insert(0, new ListItem("--Select--", "0"));
    }
 

    protected void GridView_popup_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {

        GridView_popup.PageIndex = e.NewPageIndex;
        GridFill();
    }
    protected void GridView_popup_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        Label lblid = (Label)GridView_popup.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("lblid");
        HiddenField1.Value = lblid.Text;

        ScriptManager.RegisterStartupScript(this, this.GetType(), "msg", "CloseDialog();", true);
    }
    protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        Label lblSCode = (Label)GridView1.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("lblSCode");
        code = lblSCode.Text;
        GridFill();
    }

    protected void GridView2_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "Select")
        {
            string medid = (e.CommandArgument).ToString();
            code = medid;
            GridFill();
        }

    }

    protected void GridView_popup_RowDataBound(object sender, GridViewRowEventArgs e)
    {     
        //if (e.Row.RowType == DataControlRowType.DataRow)
        //{
        //    Label lblInsertFlag = (Label)e.Row.FindControl("lblInsertFlag");
        //    if (lblInsertFlag.Text=="2")
        //    {
        //        e.Row.BackColor = System.Drawing.Color.LightSalmon;
        //    }
        //   else
        //    {
        //        e.Row.BackColor = System.Drawing.Color.YellowGreen;
        //    } 
        //}
    }

    protected void RadioButtonList_SelectedIndexChanged(object sender, System.EventArgs e)
    {
        if (RadioButtonList1.SelectedValue == "Supplier Wise")
        {
            supplier.Visible = true;
            medicine.Visible = false;
            TextBox7.Visible = true;
            TextBox8.Visible = false;
            lbltext.Text = "Supplier Name";
        }
        else if (RadioButtonList1.SelectedValue == "Medicine Wise")
        {
            supplier.Visible = false;
            medicine.Visible = true;
            TextBox7.Visible = false;
            TextBox8.Visible = true;
            lbltext.Text = "Medicine Name";
            GridFillFirst();
        }
        else
        {
            supplier.Visible = false;
            medicine.Visible = true;
            TextBox7.Visible = false;
            TextBox8.Visible = true;
            lbltext.Text = "Reagent Name";
            GridFillFirst();
        }
    }

    protected void supplrchange(object sender, System.EventArgs e)
    {
        GridFillFirst();
    }
    protected void medicinechange(object sender, System.EventArgs e)
    {
        GridFillFirst();
    }

    protected void frmdatechange(object sender, System.EventArgs e)
    {
        GridFillFirst();
    }

    protected void todatechange(object sender, System.EventArgs e)
    {
        GridFillFirst();
    }
    [System.Web.Script.Services.ScriptMethod()]
    [System.Web.Services.WebMethod]
    public static List<string> SearchMedicineName(string prefixText, int count)
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
                cmd.CommandText = "select distinct iname + '~' + icode as Name from ITEMMAST where compcode=@Compcode and itype in('M','G') and iname like @SearchText + '%'";
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
    [System.Web.Script.Services.ScriptMethod()]
    [System.Web.Services.WebMethod]
    public static List<string> SearchSupplierName(string prefixText, int count)
    {
        string compcode = string.Empty;
        string yearcode = string.Empty;
        using (SqlConnection conn = new SqlConnection())
        {
            conn.ConnectionString = ConfigurationSettings.AppSettings["ConnectionString"].ToString();
            using (SqlCommand cmd = new SqlCommand())
            {
                //+ compcode='"  "' and  Session["CoCode"].ToString().Trim() +  
                compcode = HttpContext.Current.Session["CoCode"].ToString();
                yearcode = HttpContext.Current.Session["YearCode"].ToString();
                cmd.CommandText = "select distinct slname + '-' + slcode as Name from slmast where compcode=@Compcode and category='S' and slname like @SearchText + '%'";
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
}