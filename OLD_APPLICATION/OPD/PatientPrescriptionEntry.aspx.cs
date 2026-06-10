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
using System.Net.Mail;
using System.Net;
using System.IO;
using System.Text;
using System.Collections.Generic;


public partial class OPD_PatientPrescriptionEntry : System.Web.UI.Page
{
    Main Objmain = new Main(ConfigurationSettings.AppSettings["ConnectionString"].ToString());
    PH_PatientRequisition thereq = new PH_PatientRequisition(ConfigurationSettings.AppSettings["ConnectionString"].ToString());
    PatientAdmission thepd = new PatientAdmission(ConfigurationSettings.AppSettings["ConnectionString"].ToString());
    PH_TestPopupMultiple thedia = new PH_TestPopupMultiple(ConfigurationSettings.AppSettings["ConnectionString"].ToString());
    Discharge thedischarge = new Discharge(ConfigurationSettings.AppSettings["ConnectionString"].ToString());
    PatientAppointment thepatientAppo = new PatientAppointment(ConfigurationSettings.AppSettings["ConnectionString"].ToString());
    MedicineSales theHelper = new MedicineSales(ConfigurationSettings.AppSettings["ConnectionString"].ToString());
    System.Text.StringBuilder rpt = new System.Text.StringBuilder();
    String Date = DateTime.Now.ToString("dd/MM/yyyy");
    static DataTable tempdt = new DataTable();


    protected void Page_Load(object sender, EventArgs e)
    {
        Page.Title = "Patient Prescription Entry";
        if (Session["userId"] == null)
        {
            Response.Redirect("../LoginPage.aspx");
        }
        if (Objmain.checkaccss(Session["CoCode"].ToString(), Session["UserRoleID"].ToString().Trim(), "PATIENT PRESCRIPTION ENTRY", checkAccessType.ViewAction) == false)
        {
            Response.Redirect("../AccessDenied.aspx");
        }

        if (!IsPostBack)
        {
            txtDate.Text = DateTime.Now.ToString("yyyy-MM-dd");
            Tab1Func();
            //addNewItem(ds1)
            BindGrid("");
            //BindPrescriptionGrid();
        }
    }


    //public void BindPrescriptionGrid()
    //{
    //    DataTable dt = theHelper.BindGrid(Session["CoCode"].ToString().Trim(), Session["YearCode"].ToString().Trim());
    //    GridView1.DataSource = dt;
    //    GridView1.DataBind();
    //}

    public void Tab1Func()
    {
        Tab1.CssClass = "Clicked";
        Tab2.CssClass = "Initial";
        MainView.ActiveViewIndex = 0;
        

    }
    public void Tab2Func()
    {
        Tab1.CssClass = "Initial";
        Tab2.CssClass = "Clicked";
        MainView.ActiveViewIndex = 1; 


    }
    protected void Tab1_Click(object sender, EventArgs e)
    {
        Tab1Func();
    }
    protected void Tab2_Click(object sender, EventArgs e)
    {
        Tab2Func();
        
    }

    public void BindGrid(string PrepId="")
    {
        DataTable dt = thereq.getPrescriptionDtls(Session["CoCode"].ToString().Trim(), Session["YearCode"].ToString().Trim(),PrepId);
        ViewState["data"] = dt;
        gvPrepdtl.DataSource = dt;
        gvPrepdtl.DataBind();
        if (dt.Rows.Count <= 0)
        {
            //DataRow dro = dt.NewRow();
            //dt.Rows.Add(dro);
            addnewItem(dt);
        }
        
    }

    private void GenerateRegCode()
    {
        DataTable dt = thepatientAppo.GenerateRegno(Session["CoCode"].ToString().Trim(), Session["YearCode"].ToString().Trim());
        txtreg.Text = dt.Rows[0][0].ToString();
    }


    public void addnewItem(DataTable ds)
    {
        AddTemprow();
        fillTempTable();
        DataTable tmpdt1 = tempdt;
        DataRow row = tmpdt1.NewRow();
        tempdt.Rows.Add(row);
        gvPrepdtl.DataSource = tempdt;
        gvPrepdtl.DataBind();
    }
    protected void gvPrepdtl_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            DropDownList ddlMEDICINEGROUP = (DropDownList)e.Row.FindControl("ddlMEDICINEGROUP");
            ddlMEDICINEGROUP.Items.Clear();
            ddlMEDICINEGROUP.DataSource = theHelper.DropdownMedicineGrp(Session["CoCode"].ToString().Trim());
            ddlMEDICINEGROUP.DataTextField = "MedicineGroupName";
            ddlMEDICINEGROUP.DataValueField = "MedicineGroupID";
            ddlMEDICINEGROUP.DataBind();


            DropDownList ddlDAILYDOSE = (DropDownList)e.Row.FindControl("ddlDAILYDOSE");
            ddlDAILYDOSE.Items.Clear();
            ddlDAILYDOSE.DataSource = theHelper.DropdownMedicineDose(Session["CoCode"].ToString().Trim());
            ddlDAILYDOSE.DataTextField = "DoseName";
            ddlDAILYDOSE.DataValueField = "ID";
            ddlDAILYDOSE.DataBind();


            DropDownList ddlDURATION = (DropDownList)e.Row.FindControl("ddlDURATION");
            ddlDURATION.Items.Clear();
            ddlDURATION.DataSource = theHelper.DropdownMedicineDuration(Session["CoCode"].ToString().Trim());
            ddlDURATION.DataTextField = "DurationName";
            ddlDURATION.DataValueField = "DurationId";
            ddlDURATION.DataBind();

            DropDownList ddlMEDICINENAME = (DropDownList)e.Row.FindControl("ddlMEDICINENAME");
            ddlMEDICINENAME.Items.Insert(0, new ListItem("--Select--", "0"));

            TextBox txtmedicinegrp = (TextBox)e.Row.FindControl("txtmedicinegrp");
            TextBox txtmedicine = (TextBox)e.Row.FindControl("txtmedicine");
            TextBox txtdose = (TextBox)e.Row.FindControl("txtdose");
            TextBox txtduration = (TextBox)e.Row.FindControl("txtduration");

            Tab1Func();
            Button1.Text = "Update";

            if (txtmedicinegrp.Text.Trim() != "0" && txtmedicinegrp.Text.Trim() != "")
            {
                ddlMEDICINENAME.Items.Clear();
                ddlMEDICINENAME.DataSource = theHelper.getMedicineList(Session["CoCode"].ToString().Trim(), txtmedicinegrp.Text.Trim());
                ddlMEDICINENAME.DataTextField = "MedicineName";
                ddlMEDICINENAME.DataValueField = "MedicineID";
                ddlMEDICINENAME.DataBind();
            }
            ddlMEDICINEGROUP.SelectedValue = txtmedicinegrp.Text.Trim();
            ddlMEDICINENAME.SelectedValue = txtmedicine.Text.Trim();
            ddlDAILYDOSE.SelectedValue = txtdose.Text.Trim();
            ddlDURATION.SelectedValue = txtduration.Text.Trim();
        }
    }
    
    protected void ddlMEDICINEGROUP_SelectedIndexChanged(object sender, EventArgs e)
    {
        DropDownList txt = (DropDownList)sender;
        GridViewRow gvrow = (GridViewRow)txt.Parent.Parent;
        DropDownList ddlMEDICINEGROUP = (DropDownList)gvrow.FindControl("ddlMEDICINEGROUP");
        DropDownList ddlMEDICINENAME = (DropDownList)gvrow.FindControl("ddlMEDICINENAME");

        ddlMEDICINENAME.Items.Clear();
        ddlMEDICINENAME.DataSource = theHelper.getMedicineList(Session["CoCode"].ToString().Trim(), ddlMEDICINEGROUP.SelectedValue.Trim());
        ddlMEDICINENAME.DataTextField = "MedicineName";
        ddlMEDICINENAME.DataValueField = "MedicineID";
        ddlMEDICINENAME.DataBind();

    }
    protected void btnAddRow_Click(object sender, EventArgs e)
    {
        AddTemprow();
        fillTempTable();
        DataTable tmpdt1 = tempdt;
        DataRow row = tmpdt1.NewRow();
        tempdt.Rows.Add(row);
        gvPrepdtl.DataSource = tempdt;
        gvPrepdtl.DataBind();
    }

    public void AddTemprow()
    {
        //DataSet ds2 = refreshDataSet(ds);
        tempdt = new DataTable();

        DataRow dr = tempdt.NewRow();

        tempdt.Columns.Add(new DataColumn("GroupID", typeof(string)));
        tempdt.Columns.Add(new DataColumn("MedicineId", typeof(string)));
        tempdt.Columns.Add(new DataColumn("Dose", typeof(string)));
        tempdt.Columns.Add(new DataColumn("Duration", typeof(string)));
    }
    public void fillTempTable()
    {
        DataRow drow = null;
        foreach (GridViewRow rw in gvPrepdtl.Rows)
        {
            DropDownList ddlMEDICINEGROUP = (DropDownList)rw.FindControl("ddlMEDICINEGROUP");
            DropDownList ddlMEDICINENAME = (DropDownList)rw.FindControl("ddlMEDICINENAME");
            DropDownList ddlDAILYDOSE = (DropDownList)rw.FindControl("ddlDAILYDOSE");
            DropDownList ddlDURATION = (DropDownList)rw.FindControl("ddlDURATION");

            drow = tempdt.NewRow();

            drow["GroupID"] = ddlMEDICINEGROUP.SelectedValue;
            drow["MedicineId"] = ddlMEDICINENAME.SelectedValue;
            drow["Dose"] = ddlDAILYDOSE.SelectedValue;
            drow["Duration"] = ddlDURATION.SelectedValue;
            tempdt.Rows.Add(drow);
            
        }
    }

    [System.Web.Script.Services.ScriptMethod()]
    [System.Web.Services.WebMethod]
    public static List<string> SearchByPatientName(string prefixText)
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
                cmd.CommandText = "select Pr.PatientRegNo + '~' + Pr.PName+'~'+Al.LedgerID as Name from opd_patientregistration Pr,AC_Ledger Al where Al.compcode=Pr.Compcode and Al.LedgerFK=Pr.PatientRegNo and Pr.compcode=@Compcode and Pr.PatientRegNo like @SearchText + '%'";
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
    public static List<string> Searchdoc(string prefixText, int count)
    {
        string compcode = string.Empty;
        string yearcode = string.Empty;
        using (SqlConnection conn = new SqlConnection())
        {
            conn.ConnectionString = ConfigurationSettings.AppSettings["ConnectionString"].ToString();
            using (SqlCommand cmd = new SqlCommand())
            {
                compcode = HttpContext.Current.Session["CoCode"].ToString().Trim();
                yearcode = HttpContext.Current.Session["YearCode"].ToString();
                cmd.CommandText = "select doc_id+'~'+doc_name as Name from GN_DoctorMaster where compcode=@Compcode and doc_name like @SearchText +'%'";
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


    protected void Button1_Click(object sender, EventArgs e)
    {
       string flag;
        System.Globalization.DateTimeFormatInfo dtf = new DateTimeFormatInfo();
        //DateTime dob = DateTime.ParseExact(TextBox27.Text, "dd/MM/yyyy", dtf);
        bool status = false;
        if (txtreg.Text == "")
        {
            lblError.Text = "Registration No cannot be blank!";
        }
        else if (txtDate.Text == "")
        {
            lblError.Text = "Prescription Date cannot be blank!";
        }
        else
        {
            if (Button1.Text.ToLower() == "update") 
            {
                flag = "U";
                theHelper.DeletePrescriptionDetl(Session["CoCode"].ToString().Trim(), Session["YearCode"].ToString().Trim(), txtPrepId.Text.Trim());
            }
            else
            {
                flag = "I";
            }
            bool sts = theHelper.InsertUpdatePerscriptionHead(Session["CoCode"].ToString().Trim(), Session["YearCode"].ToString().Trim(), txtPrepId.Text.Trim(), txtreg.Text.Trim(), txtDate.Text.Trim(), txtDocId.Text.Trim(), Session["userName"].ToString(), txtRemarks.Text.Trim(), flag);
            if (sts == true)
            {
                
                foreach(GridViewRow rw in gvPrepdtl.Rows)
                {
                    DropDownList ddlMEDICINEGROUP = (DropDownList)rw.FindControl("ddlMEDICINEGROUP");
                    DropDownList ddlMEDICINENAME = (DropDownList)rw.FindControl("ddlMEDICINENAME");
                    DropDownList ddlDAILYDOSE = (DropDownList)rw.FindControl("ddlDAILYDOSE");
                    DropDownList ddlDURATION = (DropDownList)rw.FindControl("ddlDURATION");

                    status=theHelper.InsertperscriptionMaping(Session["CoCode"].ToString().Trim(), Session["YearCode"].ToString().Trim(), txtPrepId.Text.Trim(), ddlMEDICINEGROUP.SelectedValue.Trim(), ddlMEDICINENAME.SelectedValue.Trim(), ddlDAILYDOSE.SelectedValue.Trim(), ddlDURATION.SelectedValue.Trim());
                    
                }
            }
            if (status == true)
            {
                lblError.Text = "Record Successfully Saved";

            }
            else
            {
                lblError.Text = "Record cannot be Saved";
            }

            
        }

        
        
    }

    protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        lblError.Text = "";
        if (e.CommandName == "Select")
        {
            int index = Convert.ToInt32(e.CommandArgument);


            Label lblPrescriptionId = (Label)GridView1.Rows[index].FindControl("lblPrescriptionId");
            txtPrepId.Text = lblPrescriptionId.Text;

            Label lblPatientname = (Label)GridView1.Rows[index].FindControl("lblPatientname");
            txtPatientName.Text = lblPatientname.Text;

            Label lblregno = (Label)GridView1.Rows[index].FindControl("lblregno");
            txtreg.Text = lblregno.Text;
            txtreg.Enabled = false;

            Label lblDoctorname = (Label)GridView1.Rows[index].FindControl("lblDoctorname");
            Label lblDocId = (Label)GridView1.Rows[index].FindControl("lblDocId");
            txtdocname.Text = lblDoctorname.Text;
            txtDocId.Text = lblDocId.Text;

            Label lblPrescriptiondate = (Label)GridView1.Rows[index].FindControl("lblPrescriptiondate");
            txtDate.Text = lblPrescriptiondate.Text;
            Label lblRemarks = (Label)GridView1.Rows[index].FindControl("lblRemarks");
            txtRemarks.Text = lblRemarks.Text;
           
            //if (ViewState["CurrentTable"] != null)
            //{
            //    DataTable dt = theHelper.BindGrid(lblPrescriptionId.Text);
            //    if (dt.Rows.Count > 0)
            //    {

            //    }
                
            //}
            BindGrid(lblPrescriptionId.Text.Trim());
            Button1.Text = "Update";
            if (Objmain.checkaccss(Session["CoCode"].ToString(), Session["UserRoleID"].ToString().Trim(), "PATIENT PRESCRIPTION ENTRY", checkAccessType.UpdateAction) == false)
            {
                Button1.Enabled = false;
            }
            else
            {
                Button1.Enabled = true;
            }
            Tab1Func();
        }
    }


    //For Deleting 

    protected void GridView1_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        Label lblPrescriptionId = (Label)GridView1.Rows[e.RowIndex].FindControl("lblPrescriptionId");
        bool status = theHelper.DeletePrescription(Session["CoCode"].ToString().Trim(), Session["YearCode"].ToString().Trim(), lblPrescriptionId.Text.Trim());
        if (status == true)
        {
            //lblError.ForeColor = System.Drawing.Color.Green;
            //lblError.Text = "Deleted Successfully";
            BindPrescriptionGrid();
            Tab2Func();
        }
        else
        {
           // lblError.ForeColor = System.Drawing.Color.Green;
            //lblError.Text = "Cannot Deleted !";
            BindPrescriptionGrid();
            Tab2Func();
        }
    }
    private void GridFill()
    {

        //GridView1.DataSource = thepatientAppo.RegGridFill(Session["CoCode"].ToString().Trim(), Session["YearCode"].ToString().Trim(), txtPrepId.Text, txtreg.Text, txtPatientName.Text, txtDate.Text);
        //GridView1.DataBind();
    }
    protected void GridView1_Sorting(object sender, GridViewSortEventArgs e)
    { 

    }

    private void ResetAllFields()
    {

        txtPrepId.Text = ""; txtreg.Text = ""; txtPatientName.Text = ""; txtDate.Text = ""; txtreferal.Text = ""; txtDate.Text = "";
        Button1.Text = "Submit";

        if (Objmain.checkaccss(Session["CoCode"].ToString(), Session["UserRoleID"].ToString().Trim(), "PATIENT PRESCRIPTION ENTRY", checkAccessType.InsertAction) == false)
        {
            Button1.Enabled = false;
        }
    }



    protected void GridView1_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        GridView1.PageIndex = e.NewPageIndex;
        GridFill();
    }

    protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            if (Objmain.checkaccss(Session["CoCode"].ToString(), Session["UserRoleID"].ToString().Trim(), "PATIENT PRESCRIPTION ENTRY", checkAccessType.DeleteAction) == false)
            {
                coldel.Visible = false;
                e.Row.Cells[6].Visible = false;
            }

        }
    }

    protected void LinkButton1_Click(object sender, EventArgs e)
    {
        LinkButton lb = (LinkButton)sender;
        GridViewRow gvRow = (GridViewRow)lb.NamingContainer;
        int rowID = gvRow.RowIndex + 1;
        if (ViewState["CurrentTable"] != null)
        {
            DataTable dt = (DataTable)ViewState["CurrentTable"];
            if (dt.Rows.Count > 1)
            {
                if (gvRow.RowIndex < dt.Rows.Count - 1)
                {
                    //Remove the Selected Row data
                    dt.Rows.Remove(dt.Rows[rowID]);
                }
            }
            //Store the current data in ViewState for future reference
            ViewState["CurrentTable"] = dt;
            //Re bind the GridView for the updated data
            GridView1.DataSource = dt;
            GridView1.DataBind();
        }
        //Set Previous Data on Postbacks
        SetPreviousData();
    }

    private void SetPreviousData()
    {
        int rowIndex = 0;
        if (ViewState["CurrentTable"] != null)
        {
            DataTable dt = (DataTable)ViewState["CurrentTable"];
            if (dt.Rows.Count > 0)
            {
                for (int i = 1; i < dt.Rows.Count; i++)
                {

                    TextBox box2 = (TextBox)GridView1.Rows[rowIndex].Cells[2].FindControl("txtPrepId");
                    

                    
                    box2.Text = dt.Rows[i]["Column2"].ToString();
                   

                    rowIndex++;
                }
            }
        }
    }







    //For Cancel Button

    protected void btnCancel_Click(object sender, EventArgs e)
    {
        txtRemarks.Text = "";
        txtreg.Text = "";
        txtreg.Enabled = true;
        txtPatientName.Text = "";
        txtDocId.Text = "";
        txtdocname.Text = "";
        txtDate.Text = DateTime.Now.ToString("yyyy-MM-dd");
        Button1.Text = "Save";
        BindGrid("");
    }
}