using System;
using System.Collections.Generic;
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
using EncryptionDecryption;
using System.Collections.Generic;
using System.Linq;
using System.Globalization;
using System.Data.OleDb;
using System.IO;
using System.Web;

public partial class Medicine_ImportFromExcel : System.Web.UI.Page
{
    Main Objmain = new Main(ConfigurationSettings.AppSettings["ConnectionString"].ToString());
    ExportToExcel theHelper = new ExportToExcel(ConfigurationSettings.AppSettings["ConnectionString"].ToString());
    ImportFromExcel theimport = new  ImportFromExcel(ConfigurationSettings.AppSettings["ConnectionString"].ToString());

    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["userName"] == null)
        {
            Response.Redirect("../LoginPage.aspx");
        }
        if (Objmain.checkaccss(Session["CoCode"].ToString(), Session["UserRoleID"].ToString().Trim(), "OPENING STOCK UPDATE", checkAccessType.ViewAction) == false)
        {
            Response.Redirect("../AccessDenied.aspx");
        }

        if (Objmain.checkaccss(Session["CoCode"].ToString(), Session["UserRoleID"].ToString().Trim(), "OPENING STOCK UPDATE", checkAccessType.InsertAction) == false)
        {
            Button1.Enabled = false;
        }

        if (!IsPostBack)
        {
            Tab1Func();
            Button3.Visible = false;
            DropDownFill();
            GridFill();
        }
    }

    public void GridFill()
    {
        GridView2.DataSource = theHelper.OpnMedicineList(Session["CoCode"].ToString().Trim(), Session["YearCode"].ToString().Trim());
        GridView2.DataBind();
    }
    public void Tab1Func()
    {
        Tab1.CssClass = "Clicked";
        Tab2.CssClass = "Initial";
        Tab3.CssClass = "Initial";
        MainView.ActiveViewIndex = 0;
    }
    protected void Tab1_Click(object sender, EventArgs e)
    {
        Tab1Func();
    }
    protected void Tab2_Click(object sender, EventArgs e)
    {
        Tab1.CssClass = "Initial";
        Tab2.CssClass = "Clicked";
        Tab3.CssClass = "Initial";
        MainView.ActiveViewIndex = 1;
    }

    protected void Tab3_Click(object sender, System.EventArgs e)
    {
        Tab1.CssClass = "Initial";
        Tab2.CssClass = "Initial";
        Tab3.CssClass = "Clicked";
        MainView.ActiveViewIndex = 2;
    }
    private void DropDownFill1()
    {
         
        DropDownList2.Items.Clear();
        this.DropDownList2.DataSource = theHelper.DropdownGroup(Session["CoCode"].ToString().Trim());
        this.DropDownList2.DataTextField = "MedicineGroupName";
        this.DropDownList2.DataValueField = "MedicineGroupID";
        this.DropDownList2.DataBind();
        this.DropDownList2.Items.Insert(0, new ListItem("--Select--", "0"));
    }

    private void DropDownFill()
    {

        DropDownList1.Items.Clear();
        this.DropDownList1.DataSource = theHelper.DropdownManufacturer(Session["CoCode"].ToString().Trim());
        this.DropDownList1.DataTextField = "MName";
        this.DropDownList1.DataValueField = "MCode";
        this.DropDownList1.DataBind();
        this.DropDownList1.Items.Insert(0, new ListItem("--Select--", "0"));


        DropDownList2.Items.Clear();
        this.DropDownList2.DataSource = theHelper.DropdownGroup(Session["CoCode"].ToString().Trim());
        this.DropDownList2.DataTextField = "MedicineGroupName";
        this.DropDownList2.DataValueField = "MedicineGroupID";
        this.DropDownList2.DataBind();
        this.DropDownList2.Items.Insert(0, new ListItem("--Select--", "0"));

        this.DropDownList3.Items.Insert(0, new ListItem("--Select--", "0"));

        DropDownList4.Items.Clear();
        DropDownList4.DataSource = theHelper.DropdownMedicine(Session["CoCode"].ToString().Trim());
        DropDownList4.DataTextField = "MedicineName";
        DropDownList4.DataValueField = "MedicineID";
        DropDownList4.DataBind();
        DropDownList4.Items.Insert(0, new ListItem("--Select--", "0"));

        ddlmonth.Items.Clear();
        ddlmonth.Items.Insert(0, new ListItem("January", "01"));
        ddlmonth.Items.Insert(1, new ListItem("Febuary", "02"));
        ddlmonth.Items.Insert(2, new ListItem("March", "03"));
        ddlmonth.Items.Insert(3, new ListItem("April", "04"));
        ddlmonth.Items.Insert(4, new ListItem("May", "05"));
        ddlmonth.Items.Insert(5, new ListItem("Jun", "06"));
        ddlmonth.Items.Insert(6, new ListItem("July", "07"));
        ddlmonth.Items.Insert(7, new ListItem("August", "08"));
        ddlmonth.Items.Insert(8, new ListItem("September", "09"));
        ddlmonth.Items.Insert(9, new ListItem("October", "10"));
        ddlmonth.Items.Insert(10, new ListItem("November", "11"));
        ddlmonth.Items.Insert(11, new ListItem("December", "12"));

        Int32 year = DateTime.Now.Year;
        ddlyear.Items.Clear();
        for (int i = 0; i <= 20; i++)
        {
            ddlyear.Items.Insert(i, new ListItem((year + i).ToString(), (year + i).ToString()));
        }
    }



    public void Reset()
    {
        DropDownList1.SelectedIndex = 0; DropDownList2.SelectedIndex = 0; DropDownList3.SelectedIndex = 0; DropDownList4.SelectedIndex = 0;
        TextBox1.Text = ""; TextBox3.Text = ""; ddlmonth.SelectedIndex = 0; ddlyear.SelectedIndex = 0;
    }
    protected void Button1_Click(object sender, EventArgs e)
    {
        System.Globalization.DateTimeFormatInfo dtf = new DateTimeFormatInfo();
        //string date=string.Empty;
        //if (TextBox2.Text.Trim() != "")
        //{
        //    DateTime testdate = DateTime.ParseExact(TextBox2.Text, "dd/MM/yyyy", dtf);
        //    date = testdate.ToString();
        //}
        //else
        //    date = "NULL";
        //DataTable code = theimport.GeneratePurchaseId(Session["CoCode"].ToString().Trim());

        insertOpeningStock(DropDownList4.SelectedValue, TextBox1.Text, ddlmonth.SelectedValue,ddlyear.SelectedValue, TextBox3.Text);              
    }
    protected void DropDownList2_SelectedIndexChanged(object sender, EventArgs e)
    {
        DropDownList3.DataSource = theHelper.DropdownSubGroup(Session["CoCode"].ToString().Trim(),DropDownList2.SelectedValue);
        DropDownList3.DataTextField = "SubGrName";
        DropDownList3.DataValueField = "ID";
        DropDownList3.DataBind();
        DropDownList3.Items.Insert(0, new ListItem("--Select--", "0")); 
    }
    protected void DropDownList3_SelectedIndexChanged(object sender, EventArgs e)
    {
        DropDownList4.DataSource = theHelper.DropdownMedicine(Session["CoCode"].ToString().Trim(),DropDownList1.SelectedValue, DropDownList3.SelectedValue);
        DropDownList4.DataTextField = "MedicineName";
        DropDownList4.DataValueField = "MedicineID";
        DropDownList4.DataBind();
        DropDownList4.Items.Insert(0, new ListItem("--Select--", "0")); 
    }
    protected void Button2_Click(object sender, EventArgs e)
    {
        string ss = Server.MapPath(FileUpload1.FileName);
        string FilePath = "";
        string[] a = new string[1];
        string fileName = "";
        string FullName = "";
        OleDbConnection oledbConn;
        try
        {
            if (FileUpload1.FileName.Length > 0)
            {
                a = FileUpload1.FileName.Split('.');

                fileName = FileUpload1.FileName;
                FilePath = Server.MapPath(@"~\ImportedExcel");
                FileUpload1.SaveAs(FilePath + @"\" + fileName);

                FullName = FilePath + @"\" + fileName;


                // Reading and storing in Dataset

                oledbConn = null;

               // oledbConn = new OleDbConnection("Provider=Microsoft.ACE.OLEDB.12.0;Data Source="+FullName+";Extended Properties="Excel 8.0;HDR=YES");                
                if (Path.GetExtension(FullName) == ".xls")
                {
                  //  oledbConn = new OleDbConnection("Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + FullName + ";Extended Properties=\"Excel 8.0;HDR=Yes;IMEX=2\"");
                    oledbConn = new OleDbConnection(@"Provider=Microsoft.ACE.OLEDB.4.0;Data Source=" + FullName + ";Extended Properties='Excel 8.0;HDR=YES;IMEX=2;';");
                }
                else if (Path.GetExtension(FullName) == ".xlsx")
                {
                   oledbConn = new OleDbConnection(@"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + FullName + ";Extended Properties='Excel 12.0;HDR=YES;IMEX=1;';");
                  //  oledbConn = new OleDbConnection("Provider= Microsoft.Jet.OLEDB.4.0; Data Source = " + FullName + "; Extended Properties =\"Excel 8.0; HDR = Yes; ImportMixedTypes = Text;Imex = 1;\"");
                }

            

                oledbConn.Open();
                OleDbCommand cmd = new OleDbCommand();
                OleDbDataAdapter oleda = new OleDbDataAdapter();
                DataSet ds = new DataSet();
                DataSet ds1 = new DataSet();
                DataSet ds2 = new DataSet();

                // passing list to drop-down list

                // selecting distict list of Slno 
                cmd.Connection = oledbConn;
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = "SELECT  * FROM [" + a[0] + "$]";
                oleda = new OleDbDataAdapter(cmd);




              cmd.CommandText = "SELECT  *   FROM [" + a[0] + "$]";

              //  cmd.CommandText = "SELECT  [ID],[Name],[Designation],[City],[Country]   FROM [" + a[0] + "$]";
                				

                //}
                oleda = new OleDbDataAdapter(cmd);
                oleda.Fill(ds);


                



                // binding form data with grid view
                HttpContext.Current.Session["ImportedData"] = ds.Tables[0];
                GridView1.DataSource = ds.Tables[0].DefaultView;
                GridView1.DataBind();

                Button3.Visible = true;
            }
            else
            {
                // error please select File ..
            }
        }
        catch (Exception ex)
        {
            lblError.ForeColor = System.Drawing.Color.Red;
            lblError.Text = ex.Message;
        }

       
    }

    protected void DropDownList1_SelectedIndexChanged(object sender, System.EventArgs e)
    {
        DropDownList2.Items.Clear();
        DropDownList3.Items.Clear();
        DropDownList4.Items.Clear();
        DropDownList2.Items.Insert(0, new ListItem("--Select--", "0"));
        DropDownList3.Items.Insert(0, new ListItem("--Select--", "0"));
        DropDownList4.Items.Insert(0, new ListItem("--Select--", "0"));
        DropDownFill1();
    }
    protected void Button3_Click(object sender, System.EventArgs e)
    {
        //DataTable dt = theimport.GeneratePurchaseId();

        //DataTable sessiondata = (DataTable)HttpContext.Current.Session["ImportedData"];

        //string day,month,year;
        //day="";
        //month="";
        //year="";

        //if (GridView1.Rows.Count > 0)
        //{
        //    System.Globalization.DateTimeFormatInfo dtf = new DateTimeFormatInfo();
        //    string date = string.Empty;
          
          
        //    for (int i = 0; i < GridView1.Rows.Count; i++)
        //    {
        //        string originaldate = "";
        //        if (sessiondata.Rows[i]["ExpiryDate"].ToString()!= DBNull.Value.ToString() || sessiondata.Rows[i]["ExpiryDate"].ToString() != "NULL")
        //        {
        //            string[] d = GridView1.Rows[i].Cells[5].Text.Split('/');
        //            if (d[0].Length == 1)
        //                day = "0" + d[0];
        //            else
        //                day = d[0];
        //            if (d[1].Length == 1)
        //                month = "0" + d[1];
        //            else
        //                month = d[1];
        //            year = d[2];
        //            originaldate = day + "/" + month + "/" + year;
        //            DateTime testdate = DateTime.ParseExact(originaldate, "dd/MM/yyyy", dtf);
        //            date = testdate.ToString();
        //        }
        //        else
        //            date = "NULL";
        //        DataTable getallid = theimport.GetIDDetails(GridView1.Rows[i].Cells[3].Text.ToString());

        //  insertOpeningStock(getallid.Rows[0][3].ToString(), GridView1.Rows[i].Cells[4].Text.ToString(), date, GridView1.Rows[i].Cells[6].Text.ToString());

        //    }
        //}

    }
    public void insertOpeningStock(string medicine, string batchno, string month,string year,string quantity)
    {
        Int32 lastday1 = DateTime.DaysInMonth(Convert.ToInt32(year), Convert.ToInt32(month));
        string expdt = year + "-" + month + "-" + lastday1.ToString();

        if (theimport.InsertPurchaseMedicine(Session["CoCode"].ToString().Trim(), Session["YearCode"].ToString().Trim(), medicine, batchno, expdt, quantity, TextBox4.Text.Trim(), Session["userName"].ToString()) == true)
        {
            lblError.ForeColor = System.Drawing.Color.Green;
            lblError.Text = "Successfully Saved..";
            ScriptManager.RegisterStartupScript(this, this.GetType(), "msg", "alert('Inserted  Successfully !');", true);
            Reset();
            GridFill();
        }
        else
        {
            lblError.ForeColor = System.Drawing.Color.Red;
            lblError.Text = "Error in Inserted Data..";
            ScriptManager.RegisterStartupScript(this, this.GetType(), "msg", "alert('Error in Saving Data !');", true);
        }

    }
    protected void GridView2_RowEditing(object sender, GridViewEditEventArgs e)
    {

    }
    protected void GridView2_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "Select")
        {
            int i = Convert.ToInt32(e.CommandArgument);
            Label lblMedId = (Label)GridView2.Rows[i].FindControl("lblMedId");
            Label lblMBatchno = (Label)GridView2.Rows[i].FindControl("lblMBatchno");
            Label lblExpdt = (Label)GridView2.Rows[i].FindControl("lblExpdt");
            Label lblOpstock = (Label)GridView2.Rows[i].FindControl("lblOpstock");
            Label lblMrp = (Label)GridView2.Rows[i].FindControl("lblMrp");

            DropDownList4.SelectedValue = lblMedId.Text.Trim();
            TextBox1.Text = lblMBatchno.Text;
            string[] dtarr1 = lblExpdt.Text.Trim().Split('/');
            //TextBox2.Text = lblExpdt.Text;
            ddlmonth.SelectedValue = dtarr1[0].Trim();
            ddlyear.SelectedValue = dtarr1[1].Trim();
            TextBox3.Text = lblOpstock.Text;
            TextBox4.Text = lblMrp.Text;
            DropDownList4.Enabled = false;
            TextBox1.Enabled = false;
            Tab1Func();
        }
    }
    

    protected void GridView2_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        GridView2.PageIndex = e.NewPageIndex;
        GridFill();
    }
}