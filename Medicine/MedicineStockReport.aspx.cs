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
using System.Collections.Generic;

public partial class Medicine_MedicineStockReport : System.Web.UI.Page
{
    Main Objmain = new Main(ConfigurationSettings.AppSettings["ConnectionString"].ToString());
    ExpiryAlertAndStockReport theHelper = new ExpiryAlertAndStockReport(ConfigurationSettings.AppSettings["ConnectionString"].ToString());
    string Expiryalert = ConfigurationSettings.AppSettings["Expirydate"].ToString();

    System.Text.StringBuilder rpt = new System.Text.StringBuilder();

    protected void Page_Load(object sender, EventArgs e)
    {
        Page.Title = "Medicine Stock";
        if (Session["userName"] == null)
        {
            Response.Redirect("../LoginPage.aspx");
        }
        if (Objmain.checkaccss(Session["CoCode"].ToString(), Session["UserRoleID"].ToString().Trim(), "MEDICINE STOCK", checkAccessType.ViewAction) == false)
        {
            Response.Redirect("../AccessDenied.aspx");
        }
       
        if (!IsPostBack)
        {
            DropDownList5.Items.Insert(0, new ListItem("Medicine", "M"));
            DropDownList5.Items.Insert(1, new ListItem("Reagent", "G"));
            DropDownFill();
            GetReport();
            RadioButtonList1.SelectedValue = "With Header";

        }
    }

    private void DropDownFill1()
    {

        DropDownList2.Items.Clear();
        this.DropDownList2.DataSource = theHelper.DropdownMedicineGroup(Session["CoCode"].ToString().Trim());
        this.DropDownList2.DataTextField = "MedicineGroupName";
        this.DropDownList2.DataValueField = "MedicineGroupID";
        this.DropDownList2.DataBind();
        this.DropDownList2.Items.Insert(0, new ListItem("--All--", "0"));
    }

    private void DropDownFill()
    {

        DropDownList1.Items.Clear();
        this.DropDownList1.DataSource = theHelper.DropDownManufacturingCompany(Session["CoCode"].ToString().Trim());
        this.DropDownList1.DataTextField = "MName";
        this.DropDownList1.DataValueField = "MCode";
        this.DropDownList1.DataBind();
        this.DropDownList1.Items.Insert(0, new ListItem("--All--", "0"));


        DropDownList2.Items.Clear();
        this.DropDownList2.DataSource = theHelper.DropdownMedicineGroup(Session["CoCode"].ToString().Trim());
        this.DropDownList2.DataTextField = "MedicineGroupName";
        this.DropDownList2.DataValueField = "MedicineGroupID";
        this.DropDownList2.DataBind();
        this.DropDownList2.Items.Insert(0, new ListItem("--All--", "0"));

        this.DropDownList3.Items.Insert(0, new ListItem("--All--", "0"));

        DropDownList4.Items.Insert(0, new ListItem("--All--", "0"));


        DropDownList4.DataSource = theHelper.DropdownMedicineAll(Session["CoCode"].ToString().Trim());
        DropDownList4.DataTextField = "MedicineName";
        DropDownList4.DataValueField = "MedicineID";
        DropDownList4.DataBind();
        DropDownList4.Items.Insert(0, new ListItem("--All--", "0"));

        DropDownList3.DataSource = theHelper.DropdownSubGroupAll(Session["CoCode"].ToString().Trim(), DropDownList2.SelectedValue);
        DropDownList3.DataTextField = "SubGrName";
        DropDownList3.DataValueField = "ID";
        DropDownList3.DataBind();
        DropDownList3.Items.Insert(0, new ListItem("--All--", "0"));
    }

    protected void DropDownList2_SelectedIndexChanged(object sender, EventArgs e)
    {
        DropDownList3.DataSource = theHelper.DropdownSubGroup(Session["CoCode"].ToString().Trim(),DropDownList2.SelectedValue);
        DropDownList3.DataTextField = "SubGrName";
        DropDownList3.DataValueField = "ID";
        DropDownList3.DataBind();
        DropDownList3.Items.Insert(0, new ListItem("--All--", "0"));
        // GetReport();
        CheckRadio();
    }
    protected void DropDownList3_SelectedIndexChanged(object sender, EventArgs e)
    {
        DropDownList4.DataSource = theHelper.DropdownMedicine(Session["CoCode"].ToString().Trim(),DropDownList1.SelectedValue, DropDownList3.SelectedValue);
        DropDownList4.DataTextField = "MedicineName";
        DropDownList4.DataValueField = "MedicineID";
        DropDownList4.DataBind();
        DropDownList4.Items.Insert(0, new ListItem("--All--", "0"));
        //GetReport();
        CheckRadio();
    }

    protected void DropDownList1_SelectedIndexChanged(object sender, System.EventArgs e)
    {
        DropDownList2.Items.Clear();
        DropDownList3.Items.Clear();
        DropDownList4.Items.Clear();
        DropDownList2.Items.Insert(0, new ListItem("--All--", "0"));
        DropDownList3.Items.Insert(0, new ListItem("--All--", "0"));
        DropDownList4.Items.Insert(0, new ListItem("--All--", "0"));
        DropDownFill1();
        // GetReport();
        CheckRadio();
    }

    protected void DropDownList4_SelectedIndexChanged(object sender, System.EventArgs e)
    {
        // GetReport();
        CheckRadio();
    }

    public void GetReport()
    {
        Report_Header();
        GetHearder_Detail();

        ltrReport.Text = rpt.ToString();

    }


    public void Report_Header()
    {
        rpt.Append("<table width='100%' cellspacing=0 border=0 style='border:1px solid black;background-color:#9B9C8D;border-spacing: 0; padding: 0;font-family:verdana;'>");
        rpt.Append("<tr cellpadding:'0'>");
        rpt.AppendFormat("<td rowspan='4' style='width: 35%; font-family:Verdana; font-size:x-Medium; font-weight:bold;text-align:left'><img src='../Images/logo.jpg'  style='height:87px;width:205px;border-style:inherit;border-color:#D3CDD4'/></td>");
        rpt.AppendFormat("<td style='width: 35%; font-family:Verdana; font-size:large; font-weight:bold;text-align:center'>{0}</td>", "GFC Hospital");
        rpt.AppendFormat("<td style='width: 35%; font-family:Verdana; font-size:x-Medium; font-weight:bold;text-align:center'>&nbsp;</td>");
        rpt.Append("</tr>");
        rpt.Append("<tr>");
        rpt.AppendFormat("<td style='width: 35%; font-family:Verdana; font-size:x-Medium; font-weight:bold;text-align:center'>{0}</td>", "REG NO : NH-315/G-70/2013");
        rpt.AppendFormat("<td style='width: 35%; font-family:Verdana; font-size:x-Medium; font-weight:bold;text-align:center'>&nbsp;</td>");
        rpt.Append("</tr>");
        rpt.Append("<tr>");
        rpt.AppendFormat("<td style='width: 35%; font-family:Verdana; font-size:x-Medium; font-weight:bold;text-align:center'>{0}</td>", "KUSHPATA,GHATAL,MIDNAPUR(W),WB,721212");
        rpt.AppendFormat("<td style='width: 35%; font-family:Verdana; font-size:x-Medium; font-weight:bold;text-align:center'>&nbsp;</td>");
        rpt.Append("</tr>");
        rpt.Append("<tr>");
        rpt.AppendFormat("<td style='width: 35%; font-family:Verdana; font-size:x-Medium; font-weight:bold;text-align:center'>{0}</td>", "Ph :(03225)244400/244643  M:9434419825");
        rpt.AppendFormat("<td style='width: 35%; font-family:Verdana; font-size:x-Medium; font-weight:bold;text-align:center'>&nbsp;</td>");
        rpt.Append("</tr>");
        rpt.Append("</table>");
    }

    public void GetHearder_Detail()
    {
        ltrReport.Text = "";
        string itype = DropDownList5.SelectedValue.ToString();
        DateTime now = DateTime.Now.AddDays(Convert.ToInt32(Expiryalert));
        DataTable dt = theHelper.GetStockReport(Session["CoCode"].ToString().Trim(),Session["YearCode"].ToString().Trim(),DropDownList1.SelectedValue, DropDownList2.SelectedValue, DropDownList3.SelectedValue, DropDownList4.SelectedValue,itype);

        if (dt.Rows.Count > 0)
        {
            rpt.Append("<br/>");
            rpt.Append("<table width='900px' cellspacing=0 border=0 style='border:1px solid black;border-spacing: 0; padding: 0;font-family:verdana;'>");
            rpt.Append("<tr style='height:20px'>");
            rpt.Append("<td colspan='6' style='width: 8%;border-bottom: 1px solid black; font-family:Verdana;font-weight:bold;background-color:#9B9C8D;font-size:larger; text-align:Center'> Items Expiry Details  </td>");
            rpt.Append("</tr'>");

            rpt.Append("<tr style='height:20px'>");
            rpt.Append("<td style='width: 100px; font-family:Verdana;font-weight:bold;font-size:small;border-right:1px solid black;border-bottom:1px solid black; text-align:center'>Manufacturing Company</td>");
            rpt.Append("<td style='width: 150px;font-family:Verdana;font-weight:bold;font-size:small;border-right:1px solid black; text-align:center'>Item Group</td>");
            rpt.Append("<td style='width: 150px;font-family:Verdana;font-weight:bold;font-size:small;border-right:1px solid black; text-align:center'>Item Sub Group</td>");
            rpt.Append("<td style='width: 250px;font-family:Verdana;font-weight:bold;font-size:small;border-right:1px solid black; text-align:center'>Medicine Name</td>");
            rpt.Append("<td style='width: 250px;font-family:Verdana;font-weight:bold;font-size:small;text-align:center'>Remaining Qty</td>");
            rpt.Append("</tr >");



            for (int i = 0; i < dt.Rows.Count; i++)
            {
                

                    rpt.Append("<tr style='height:25px'>");
                    rpt.AppendFormat("<td style='font-family:Verdana;font-size:small;border-right:1px solid black;border-top:1px solid black; text-align:left;'>{0}</td>", dt.Rows[i]["MName"]);
                    rpt.AppendFormat("<td style='font-family:Verdana;font-size:small;border-right:1px solid black;border-top:1px solid black; text-align:left;'>{0}</td>", dt.Rows[i]["MedicineGroupName"]);
                    rpt.AppendFormat("<td style='font-family:Verdana;font-size:small;border-right:1px solid black;border-top:1px solid black; text-align:left;'>{0}</td>", dt.Rows[i]["SubGrName"]);
                    rpt.AppendFormat("<td style='font-family:Verdana;font-size:small;border-right:1px solid black;border-top:1px solid black; text-align:left;'>{0}</td>", dt.Rows[i]["MedicineName"]);
                      rpt.AppendFormat("<td style='font-family:Verdana;font-size:small;border-top:1px solid black; text-align:right;'>{0}</td>", dt.Rows[i]["Quantity"]);
                    rpt.Append("</tr >");

 
            }
            rpt.Append("</table >");
        }
        ltrReport.Visible = true;

    }
    public void CheckRadio()
    {
        if (RadioButtonList1.SelectedValue == "With Header")
        {
            ltrReport.Text = "";
            GetReport();
            ltrReport.Text = rpt.ToString();
        }
        else
        {
            ltrReport.Text = "";
            GetHearder_Detail();
            ltrReport.Text = rpt.ToString();
        }
    }
    protected void RadioButtonList1_SelectedIndexChanged(object sender, System.EventArgs e)
    {
        CheckRadio();
    }
    protected void medicinechange(object sender, System.EventArgs e)
    {
        DataTable dt = theHelper.getMedicineData(Session["CoCode"].ToString().Trim(), TextBoxicode.Text);
        if (dt.Rows.Count > 0)
        {
            DropDownList1.SelectedValue = dt.Rows[0]["MCode"].ToString();
            DropDownList2.SelectedValue = dt.Rows[0]["MedicineGroupID"].ToString();
            DropDownList3.SelectedValue = dt.Rows[0]["SubGroupid"].ToString();
            DropDownList4.SelectedValue = dt.Rows[0]["MedicineID"].ToString();
        }
        
        GetReport();
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
                cmd.CommandText = "select distinct it.iname + '~' + ltrim(rtrim(it.icode)) as Name from ITEMMAST it,IPD_MedicineMaster im where im.compcode=it.compcode and im.icode=it.icode and it.itype in('M','G') and it.compcode=@Compcode and it.iname like @SearchText +'%'";
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

    protected void ddl5_selecttedIndexChanged(object sender, EventArgs e)
    {
        DropDownList1.Items.Clear();
        DropDownList2.Items.Clear();
        DropDownList3.Items.Clear();
        DropDownList4.Items.Clear();
        DropDownList2.Items.Insert(0, new ListItem("--All--", "0"));
        DropDownList3.Items.Insert(0, new ListItem("--All--", "0"));
        DropDownList4.Items.Insert(0, new ListItem("--All--", "0"));
        DropDownFill();
        CheckRadio();
    }


}