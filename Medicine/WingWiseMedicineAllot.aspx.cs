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

public partial class Medicine_WingWiseMedicineAllot : System.Web.UI.Page
{
    Main Objmain = new Main(ConfigurationSettings.AppSettings["ConnectionString"].ToString());
    ExpiryAlertAndStockReport theHelper = new ExpiryAlertAndStockReport(ConfigurationSettings.AppSettings["ConnectionString"].ToString());
    System.Text.StringBuilder rpt = new System.Text.StringBuilder();

    protected void Page_Load(object sender, EventArgs e)
    {
        Page.Title = "Wing Wise Medicine Allocation";
        if (Session["userName"] == null)
        {
            Response.Redirect("../LoginPage.aspx");
        }
        if (Objmain.checkaccss(Session["CoCode"].ToString(), Session["UserRoleID"].ToString().Trim(), "WING WISE MEDICINE ALLOCATION", checkAccessType.ViewAction) == false)
        {
            Response.Redirect("../AccessDenied.aspx");
        }

        if (!IsPostBack)
        {
            DropDownFill();
            GetReport();
            RadioButtonList1.SelectedValue = "With Header";

        }
    }

    public void DropDownFill()
    {
        ddlWings.Items.Clear();
        ddlWings.Items.Insert(0, new ListItem("--Select--", "0"));

        ddlFloor.Items.Clear();
        ddlFloor.DataSource = theHelper.getFloor(Session["CoCode"].ToString().Trim());
        ddlFloor.DataTextField = "FloorName";
        ddlFloor.DataValueField = "FloorId";
        ddlFloor.DataBind();
        ddlFloor.Items.Insert(0, new ListItem("--Select--", "0"));

        ddlmed.Items.Clear();
        ddlmed.DataSource = theHelper.GetmedicineforAllot(Session["CoCode"].ToString().Trim());
        ddlmed.DataTextField="MedicineName";
        ddlmed.DataValueField = "MedicineID";
        ddlmed.DataBind();
        ddlmed.Items.Insert(0, new ListItem("--Select--", "0"));
    }

    protected void ddlfloor_selectedIndexChange(object sender, EventArgs e)
    {
        ddlWings.Items.Clear();
        if (ddlFloor.SelectedValue != "")
        {
            ddlWings.DataSource = theHelper.getWorkStation(Session["CoCode"].ToString().Trim(),ddlFloor.SelectedValue);
            ddlWings.DataTextField = "WorkStation";
            ddlWings.DataValueField = "WingsID";
            ddlWings.DataBind();
        }
        this.ddlWings.Items.Insert(0, new ListItem("--Select--", "0"));
    }

    protected void ddlmed_SelectedIndexChange(object sender, EventArgs e)
    {
        GetReport();
    }
    protected void ddlwing_SelectedIndexChange(object sender, EventArgs e)
    {
        GetReport();
    }


    protected void RadioButtonList1_SelectedIndexChanged(object sender, System.EventArgs e)
    {
        CheckRadio();
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
    public void GetReport()
    {
        Report_Header();
        GetHearder_Detail();

        ltrReport.Text = rpt.ToString();

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
        //DateTime now = DateTime.Now.AddDays(Convert.ToInt32(Expiryalert));

        string frmdate = "";
        string todate = "";
        string issueqty;
        string allotedQty;
        string remainingQty;

        DataTable qtyTbl;

        if (frmdt.Text != "")
        {
            string[] aa = frmdt.Text.Split('/');
            string fday = aa[0];
            string fmonth = aa[1];
            string fyear = aa[2];
            if (fday.Length == 1)
                fday = "0" + fday;
            if (fmonth.Length == 1)
                fmonth = "0" + fmonth;
            // frmdate = fday + "/" + fmonth + "/" + fyear;
            frmdate = fyear + fmonth + fday;
        }
        else
        {
            frmdate = "";
        }
        if (todt.Text != "")
        {
            string[] aa = todt.Text.Split('/');
            string tday = aa[0];
            string tmonth = aa[1];
            string tyear = aa[2];
            if (tday.Length == 1)
                tday = "0" + tday;
            if (tmonth.Length == 1)
                tmonth = "0" + tmonth;
            //todate = tday + "/" + tmonth + "/" + tyear;
            todate = tyear + tmonth + tday;
        }
        else
        {
            todate = "";
        }
        DataTable dt = theHelper.GetWingWiseStockDetails(Session["CoCode"].ToString().Trim(), Session["YearCode"].ToString().Trim(), ddlWings.SelectedValue, ddlmed.SelectedValue, frmdate, todate);
        if (dt.Rows.Count > 0)
        {
            rpt.Append("<br/>");
            rpt.Append("<table width='900px' cellspacing=0 border=0 style='border:1px solid black;border-spacing: 0; padding: 0;font-family:verdana;'>");
            rpt.Append("<tr style='height:20px'>");
            rpt.Append("<td colspan='7' style='width: 8%;border-bottom: 1px solid black; font-family:Verdana;font-weight:bold;background-color:#9B9C8D;font-size:larger; text-align:Center'> Wing Wise Medicine Allocation  </td>");
            rpt.Append("</tr'>");

            rpt.Append("<tr style='height:20px'>");
            rpt.Append("<td style='width: 250px; font-family:Verdana;font-weight:bold;font-size:small;border-right:1px solid black;text-align:center'>Wing Name</td>");
            rpt.Append("<td style='width: 250px; font-family:Verdana;font-weight:bold;font-size:small;border-right:1px solid black;text-align:center'>WorkStation</td>");
            rpt.Append("<td style='width: 100px; font-family:Verdana;font-weight:bold;font-size:small;border-right:1px solid black;text-align:center'>Medicine Name</td>");
            rpt.Append("<td style='width: 150px;font-family:Verdana;font-weight:bold;font-size:small;border-right:1px solid black; text-align:center'>Issue Date</td>");
            rpt.Append("<td style='width: 150px;font-family:Verdana;font-weight:bold;font-size:small;border-right:1px solid black; text-align:right'>Issue Quantity</td>");
            rpt.Append("<td style='width: 250px;font-family:Verdana;font-weight:bold;font-size:small;border-right:1px solid black; text-align:right'>Alloted Quantity</td>");
            rpt.Append("<td style='width: 100px;font-family:Verdana;font-weight:bold;font-size:small; text-align:right'>Remaining Quantity</td>");
           
            rpt.Append("</tr >");

            for (int i = 0; i < dt.Rows.Count; i++)
            {
                qtyTbl = theHelper.getQuantityWingWise(Session["CoCode"].ToString().Trim(), Session["YearCode"].ToString().Trim(),dt.Rows[i]["WingsID"].ToString(), dt.Rows[i]["MedicineID"].ToString());
                rpt.Append("<tr style='height:25px'>");

                rpt.AppendFormat("<td style='font-family:Verdana;font-size:small;border-right:1px solid black;border-top:1px solid black; text-align:left;'>{0}</td>", dt.Rows[i]["WingsName"]);
                rpt.AppendFormat("<td style='font-family:Verdana;font-size:small;border-right:1px solid black;border-top:1px solid black; text-align:left;'>{0}</td>", dt.Rows[i]["WorkStation"]);
                rpt.AppendFormat("<td style='font-family:Verdana;font-size:small;border-right:1px solid black;border-top:1px solid black; text-align:left;'>{0}</td>", dt.Rows[i]["MedicineName"]);
                rpt.AppendFormat("<td style='font-family:Verdana;font-size:small;border-right:1px solid black;border-top:1px solid black; text-align:left;'>{0}</td>", dt.Rows[i]["IssueDate"]);
                rpt.AppendFormat("<td style='font-family:Verdana;font-size:small;border-right:1px solid black;border-top:1px solid black; text-align:left;'>{0}</td>", qtyTbl.Rows[0]["IssueQty"]);
                rpt.AppendFormat("<td style='font-family:Verdana;font-size:small;border-right:1px solid black;border-top:1px solid black; text-align:right;'>{0}</td>", qtyTbl.Rows[0]["AllotedQty"]);
                rpt.AppendFormat("<td style='font-family:Verdana;font-size:small;border-top:1px solid black; text-align:right;'>{0}</td>", qtyTbl.Rows[0]["RemainingQty"]);
                
                
                rpt.Append("</tr >");
            }
            rpt.Append("</table >");
        }
        ltrReport.Visible = true;
    }

}