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

public partial class Medicine_MedicineStockDetails : System.Web.UI.Page
{
    Main Objmain = new Main(ConfigurationSettings.AppSettings["ConnectionString"].ToString());
    ExpiryAlertAndStockReport theHelper = new ExpiryAlertAndStockReport(ConfigurationSettings.AppSettings["ConnectionString"].ToString());
    string Expiryalert = ConfigurationSettings.AppSettings["Expirydate"].ToString();

    System.Text.StringBuilder rpt = new System.Text.StringBuilder();

    protected void Page_Load(object sender, EventArgs e)
    {
        Page.Title = "Medicine Stock Details";
        if (Session["userName"] == null)
        {
            Response.Redirect("../LoginPage.aspx");
        }
        if (Objmain.checkaccss(Session["CoCode"].ToString(), Session["UserRoleID"].ToString().Trim(), "MEDICINE/REAGENT STOCK DETAILS", checkAccessType.ViewAction) == false)
        {
            Response.Redirect("../AccessDenied.aspx");
        }
        if (!IsPostBack)
        {
            DropDownList5.Items.Insert(0, new ListItem("Medicine", "M"));
            DropDownList5.Items.Insert(1, new ListItem("Reagent", "G"));
            GetReport();
            RadioButtonList1.SelectedValue = "With Header";

        }
    }


    public void GetReport()
    {
        Report_Header();
        GetHearder_Detail();

        ltrReport.Text = rpt.ToString();

    }


    protected void medicinechange(object sender, System.EventArgs e)
    {

        //DataTable dt = theHelper.getMedicineData(Session["CoCode"].ToString().Trim(), TextBoxicode.Text);
        //if (dt.Rows.Count > 0)
        //{
        //    DropDownList1.SelectedValue = dt.Rows[0]["MCode"].ToString();
        //    DropDownList2.SelectedValue = dt.Rows[0]["MedicineGroupID"].ToString();
        //    DropDownList3.SelectedValue = dt.Rows[0]["SubGroupid"].ToString();
        //    DropDownList4.SelectedValue = dt.Rows[0]["MedicineID"].ToString();
        //}
        GetReport();
    }

    protected void Button1_Click(object sender, EventArgs e)
    {
        //Report_Header();
        //GetHearder_Detail();
        //ltrReport.Text = rpt.ToString();
        GetReport();
    }

    protected void frmdt_textChange(object sender, EventArgs e)
    {
        GetReport();
    }

    protected void todt_textChange(object sender, EventArgs e)
    {
        GetReport();
    }
    public void Report_Header()
    {
        System.Globalization.DateTimeFormatInfo dtf = new DateTimeFormatInfo();
        string date = DateTime.Now.ToString("dd/MM/yyyy");

        rpt.Append("<table width='100%' cellspacing=0 border=0 >");
        rpt.Append("<tr cellpadding:'0'  style='height:30px;'>");
        rpt.Append("<td width='30%'><img src='../Images/logo.jpg'  style=''/></td>");
        rpt.Append("<td   width='40%'  style='height:20px;font-family:Arial; font-size:medium; text-align:center'></td>");
        rpt.Append("<td width='30%' style='text-align:right'>Print Date : " + date + "</td>");
        rpt.Append("</tr>");
        rpt.Append("</table>");
    }


    public void GetHearder_Detail()
    {
        ltrReport.Text = "";
        DateTime now = DateTime.Now.AddDays(Convert.ToInt32(Expiryalert));

        string frmdate = "";
        string todate = "";
        string itype = DropDownList5.SelectedValue.ToString();
        //if (frmdt.Text != "")
        //{
        //    string[] aa = frmdt.Text.Split('/');
        //    string fday = aa[0];
        //    string fmonth = aa[1];
        //    string fyear = aa[2];
        //    if (fday.Length == 1)
        //        fday = "0" + fday;
        //    if (fmonth.Length == 1)
        //        fmonth = "0" + fmonth;
        //    // frmdate = fday + "/" + fmonth + "/" + fyear;
        //    frmdate = fyear + fmonth + fday;
        //}
        //else
        //    frmdate = "";

        //if (todt.Text != "")
        //{
        //    string[] aa = todt.Text.Split('/');
        //    string tday = aa[0];
        //    string tmonth = aa[1];
        //    string tyear = aa[2];
        //    if (tday.Length == 1)
        //        tday = "0" + tday;
        //    if (tmonth.Length == 1)
        //        tmonth = "0" + tmonth;
        //    //todate = tday + "/" + tmonth + "/" + tyear;
        //    todate = tyear + tmonth + tday;
        //}
        //else
        //    todate = "";


        DataTable dt = theHelper.GetMedicineStockDetails(Session["CoCode"].ToString().Trim(), Session["YearCode"].ToString().Trim(), TextBoxicode.Text.Trim(),itype);
        string suplr;
        DataTable suptbl;
        DataTable rettbl;
        DataTable issuetbl;
        if (dt.Rows.Count > 0)
        {
            rpt.Append("<br/>");
            rpt.Append("<table width='100%' cellspacing=0 border=0 style='border:1px solid black;border-spacing: 0; padding: 0;font-family:verdana;'>");
            rpt.Append("<tr style='height:20px'>");
            rpt.Append("<td colspan='8' style='width: 100%;border-bottom: 1px solid black; font-family:Verdana;font-weight:bold;background-color:#9B9C8D;font-size:larger; text-align:Center'> Items Stock Details  </td>");
            rpt.Append("</tr'>");

            rpt.Append("<tr style='height:20px'>");
            //rpt.Append("<td style='width: 250px; font-family:Verdana;font-weight:bold;font-size:small;border-right:1px solid black;text-align:center'>Supplier</td>");
            //rpt.Append("<td style='width: 100px; font-family:Verdana;font-weight:bold;font-size:small;border-right:1px solid black;text-align:center'>Manufacturing Company</td>");
            //rpt.Append("<td style='width: 150px;font-family:Verdana;font-weight:bold;font-size:small;border-right:1px solid black; text-align:center'> Group</td>");
            //rpt.Append("<td style='width: 150px;font-family:Verdana;font-weight:bold;font-size:small;border-right:1px solid black; text-align:center'> Sub Group</td>");
            rpt.Append("<td style='width: 5%;font-family:Verdana;font-weight:bold;font-size:small;border-right:1px solid black; text-align:center'>Srl No.</td>");
            rpt.Append("<td style='width: 30%;font-family:Verdana;font-weight:bold;font-size:small;border-right:1px solid black; text-align:center'>Medicine Name</td>");
            rpt.Append("<td style='width: 10%;font-family:Verdana;font-weight:bold;font-size:small;border-right:1px solid black; text-align:center'>Batch No</td>");
            rpt.Append("<td style='width: 10%;font-family:Verdana;font-weight:bold;font-size:small;border-right:1px solid black; text-align:center'>Purchase Date</td>");
            rpt.Append("<td style='width: 10%;font-family:Verdana;font-weight:bold;font-size:small;border-right:1px solid black; text-align:center'>Purchase Quantity</td>");
            rpt.Append("<td style='width: 10%;font-family:Verdana;font-weight:bold;font-size:small;border-right:1px solid black; text-align:center'>Issue/Sale Quantity</td>");
            //rpt.Append("<td style='width: 10%;font-family:Verdana;font-weight:bold;font-size:small;border-right:1px solid black; text-align:center'>Return Quantity</td>");
            rpt.Append("<td style='width: 10%;font-family:Verdana;font-weight:bold;font-size:small;text-align:center'>Remaining Qty</td>");
            rpt.Append("</tr >");
            string icode="";
            string previcode="";
            double selqty;
            double retqty;
            double stock;
            int srl = 0;
            for (int i = 0; i < dt.Rows.Count; i++)
            {

                //suptbl = theHelper.GetSupplier(dt.Rows[i]["SLCODE"].ToString(), Session["CoCode"].ToString().Trim());
                //if (suptbl.Rows.Count > 0)
                //{
                //    suplr = suptbl.Rows[0]["SName"].ToString();
                //}
                //else
                //{
                //    suplr = "";
                //}

                //rettbl = theHelper.GetRetQty(dt.Rows[i]["MedicineID"].ToString(), Session["CoCode"].ToString().Trim(), Session["YearCode"].ToString().Trim(), dt.Rows[i]["BatchNo"].ToString(), dt.Rows[i]["ICODE"].ToString());
                //if (rettbl.Rows.Count > 0)
                //{
                //    retqty = Convert.ToDouble(rettbl.Rows[0]["retqty"]);
                //}
                //else
                //{
                //    retqty = 0.00;
                //}
                
                //issuetbl = theHelper.GetIssueQty(dt.Rows[i]["MedicineID"].ToString(), Session["CoCode"].ToString().Trim(), Session["YearCode"].ToString().Trim(), dt.Rows[i]["BatchNo"].ToString(), dt.Rows[i]["ICODE"].ToString());
                //if (issuetbl.Rows.Count > 0)
                //{
                //    selqty = Convert.ToDouble(issuetbl.Rows[0]["issueqty"]);
                //}
                //else
                //{
                //    selqty = 0.00;
                //}
                //stock=(Convert.ToDouble(dt.Rows[i]["PurchaseQty"])-(retqty+selqty));
                icode = dt.Rows[i]["ICODE"].ToString();
                srl = i + 1;
                rpt.Append("<tr style='height:25px'>");
                //rpt.AppendFormat("<td style='font-family:Verdana;font-size:small;border-right:1px solid black;border-top:1px solid black; text-align:left;'>{0}</td>", suplr);
                //rpt.AppendFormat("<td style='font-family:Verdana;font-size:small;border-right:1px solid black;border-top:1px solid black; text-align:left;'>{0}</td>", dt.Rows[i]["MName"]);
                //rpt.AppendFormat("<td style='font-family:Verdana;font-size:small;border-right:1px solid black;border-top:1px solid black; text-align:left;'>{0}</td>", dt.Rows[i]["MedicineGroupName"]);
                //rpt.AppendFormat("<td style='font-family:Verdana;font-size:small;border-right:1px solid black;border-top:1px solid black; text-align:left;'>{0}</td>", dt.Rows[i]["SubGrName"]);
                rpt.AppendFormat("<td style='font-family:Verdana;font-size:small;border-right:1px solid black;border-top:1px solid black; text-align:left;'>{0}</td>", srl);
                rpt.AppendFormat("<td style='font-family:Verdana;font-size:small;border-right:1px solid black;border-top:1px solid black; text-align:left;'>{0}</td>", dt.Rows[i]["MedicineName"]);
                rpt.AppendFormat("<td style='font-family:Verdana;font-size:small;border-right:1px solid black;border-top:1px solid black; text-align:left;'>{0}</td>", dt.Rows[i]["BatchNo"]);
                rpt.AppendFormat("<td style='font-family:Verdana;font-size:small;border-right:1px solid black;border-top:1px solid black; text-align:right;'>{0}</td>", dt.Rows[i]["purchaseDate"]);
                rpt.AppendFormat("<td style='font-family:Verdana;font-size:small;border-right:1px solid black;border-top:1px solid black; text-align:right;'>{0}</td>", dt.Rows[i]["PurchaseQty"]);
                //if(icode==previcode){selqty="";retqty="";stock="";}else{selqty=dt.Rows[i]["SaleQty"].ToString();retqty=dt.Rows[i]["ReturnQty"].ToString();stock=dt.Rows[i]["curstock"].ToString();}

                rpt.AppendFormat("<td style='font-family:Verdana;font-size:small;border-right:1px solid black;border-top:1px solid black; text-align:right;'>{0}</td>", dt.Rows[i]["OUTSTOCK"]);
                //rpt.AppendFormat("<td style='font-family:Verdana;font-size:small;border-right:1px solid black;border-top:1px solid black; text-align:right;'>{0}</td>",retqty );
                rpt.AppendFormat("<td style='font-family:Verdana;font-size:small;border-top:1px solid black; text-align:right;'>{0}</td>", dt.Rows[i]["CURSTOCK"]);
                rpt.Append("</tr >");
                previcode = icode;
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
                cmd.CommandText = "select distinct ltrim(rtrim(icode)) + '~' + ltrim(rtrim(MedicineName)) as Name from IPD_MedicineMaster where compcode=@Compcode and ltrim(rtrim(MedicineName)) like @SearchText +'%'";
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
        
        CheckRadio();
    }
}