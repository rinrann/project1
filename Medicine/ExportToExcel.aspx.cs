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
using System.Reflection;
using System.Collections.Generic;
using RKLib.ExportData;
 

public partial class Medicine_ExportToExcel : System.Web.UI.Page
{
    Main Objmain = new Main(ConfigurationSettings.AppSettings["ConnectionString"].ToString());
    ExportToExcel theHelper = new ExportToExcel(ConfigurationSettings.AppSettings["ConnectionString"].ToString());

    private DataTable _dt;

    
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            Page.Title = "Export To Excel";
            if (Session["userName"] == null)
            {
                Response.Redirect("../LoginPage.aspx");
            }
            if (Objmain.checkaccss(Session["CoCode"].ToString(), Session["UserRoleID"].ToString().Trim(), "EXPORT TO EXCEL", checkAccessType.ViewAction) == false)
            {
                Response.Redirect("../AccessDenied.aspx");
            }


            if (Session["userName"] == null)
            {
                Response.Redirect("../LoginPage.aspx");
            }
            if (!IsPostBack)
            {
                DropDownFill();
                GridFill();

            }
        }
        catch (Exception ex)
        {
            lblError.ForeColor = System.Drawing.Color.Red;
            lblError.Text = ex.Message;
        }
    }

 

     
    private void DropDownFill()
    {     

        DropDownList1.Items.Clear();
        this.DropDownList1.DataSource = theHelper.DropdownManufacturer(Session["CoCode"].ToString().Trim());
        this.DropDownList1.DataTextField = "MName";
        this.DropDownList1.DataValueField = "MCode";
        this.DropDownList1.DataBind();
        this.DropDownList1.Items.Insert(0, new ListItem("--ALL--", "0"));
   

        DropDownList2.Items.Clear();
        this.DropDownList2.DataSource = theHelper.DropdownGroup(Session["CoCode"].ToString().Trim());
        this.DropDownList2.DataTextField = "MedicineGroupName";
        this.DropDownList2.DataValueField = "MedicineGroupID";
        this.DropDownList2.DataBind();
        this.DropDownList2.Items.Insert(0, new ListItem("--ALL--", "0"));

        this.DropDownList3.Items.Insert(0, new ListItem("--ALL--", "0"));
        

    }


    private void ExporttoExcel(DataTable table)
    {
        HttpContext.Current.Response.Clear();
        HttpContext.Current.Response.ClearContent();
        HttpContext.Current.Response.ClearHeaders();
        HttpContext.Current.Response.Buffer = true;
        HttpContext.Current.Response.ContentType = "application/ms-excel";
        HttpContext.Current.Response.Write(@"<!DOCTYPE HTML PUBLIC ""-//W3C//DTD HTML 4.0 Transitional//EN"">");
        HttpContext.Current.Response.AddHeader("Content-Disposition", "attachment;filename=Reports.xls");
        HttpContext.Current.Response.Charset = "utf-8";
        HttpContext.Current.Response.ContentEncoding = System.Text.Encoding.GetEncoding("windows-1250");
        HttpContext.Current.Response.Write("<font style='font-size:10.0pt; font-family:Calibri;'>");
        HttpContext.Current.Response.Write("<BR><BR><BR>");
        HttpContext.Current.Response.Write("<Table border='1' bgColor='#ffffff' borderColor='#000000' cellSpacing='0' cellPadding='0' style='font-size:10.0pt; font-family:Calibri; background:white;'> <TR>");
        int columnscount = GridView1.Columns.Count;

        for (int j = 0; j < columnscount; j++)
        {
            HttpContext.Current.Response.Write("<Td>");
            HttpContext.Current.Response.Write("<B>");
            HttpContext.Current.Response.Write(GridView1.Columns[j].HeaderText.ToString());
            HttpContext.Current.Response.Write("</B>");
            HttpContext.Current.Response.Write("</Td>");
        }
        HttpContext.Current.Response.Write("</TR>");
        foreach (DataRow row in table.Rows)
        {
            HttpContext.Current.Response.Write("<TR>");
            for (int i = 0; i < table.Columns.Count; i++)
            {
                HttpContext.Current.Response.Write("<Td>");
                HttpContext.Current.Response.Write(row[i].ToString());
                HttpContext.Current.Response.Write("</Td>");
            }

            HttpContext.Current.Response.Write("</TR>");
        }
        HttpContext.Current.Response.Write("</Table>");
        HttpContext.Current.Response.Write("</font>");
        HttpContext.Current.Response.Flush();
        HttpContext.Current.Response.End();

         
    }


    protected void Button1_Click(object sender, EventArgs e)
    {
        DataTable dtcaste = theHelper.GetAllMedicine(Session["CoCode"].ToString().Trim(), DropDownList2.SelectedValue, DropDownList3.SelectedValue);
       // ExporttoExcel(dtcaste);


        int[] iColumns = new int[7];
        string[] sHeaders = new string[7];
        for (int i = 0; i < GridView1.Columns.Count; i++)
        {
            iColumns[i] = i;
            sHeaders[i] = GridView1.Columns[i].HeaderText.ToString();
        }



     //   int[] iColumns = { 0, 1, 2, 3, 4, 5, 6, 7 };

     //   string aa = GridView1.Columns[0].HeaderText.ToString();
     //   string[] sHeaders = {        
     //               "ManufacturingCompany"
     //               ,"Group"
     //               ,"SubGroup"
     //               ,"ItemName"
     //               ,"BatchNo"   
     //               ,"ExpiryDate"
     //               ,"Quantity"
     //               ,"OpeningStock"
     //              };



        // Export the details of specified columns with specified headers to CSV


        RKLib.ExportData.Export objExport = new RKLib.ExportData.Export();
        objExport.ExportDetails(dtcaste, iColumns, sHeaders, Export.ExportFormat.Excel, "file.xls");  


      

    }
    protected void DropDownList2_SelectedIndexChanged(object sender, EventArgs e)
    {
        DropDownList3.DataSource = theHelper.DropdownSubGroup(Session["CoCode"].ToString().Trim(),DropDownList2.SelectedValue);
        DropDownList3.DataTextField = "SubGrName";
        DropDownList3.DataValueField = "ID";
        DropDownList3.DataBind();
        DropDownList3.Items.Insert(0, new ListItem("--Select--", "0")); 
    }

    public void GridFill()
    {
        DataTable dt = theHelper.GetAllMedicine(Session["CoCode"].ToString().Trim(), DropDownList2.SelectedValue, DropDownList3.SelectedValue);
        GridView1.DataSource = dt;
        GridView1.DataBind();
    }
    protected void DropDownList3_SelectedIndexChanged(object sender, System.EventArgs e)
    {
        GridFill();  
    }
}