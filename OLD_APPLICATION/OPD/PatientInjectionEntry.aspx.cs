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
public partial class OPD_PatientInjectionEntry : System.Web.UI.Page
{
    Main Objmain = new Main(ConfigurationSettings.AppSettings["ConnectionString"].ToString());
    PH_PatientRequisition thereq = new PH_PatientRequisition(ConfigurationSettings.AppSettings["ConnectionString"].ToString());
    PatientAdmission thepd = new PatientAdmission(ConfigurationSettings.AppSettings["ConnectionString"].ToString());
    PH_TestPopupMultiple thedia = new PH_TestPopupMultiple(ConfigurationSettings.AppSettings["ConnectionString"].ToString());
    Discharge thedischarge = new Discharge(ConfigurationSettings.AppSettings["ConnectionString"].ToString());
    PH_PatientRequisition thepatient = new PH_PatientRequisition(ConfigurationSettings.AppSettings["ConnectionString"].ToString());
    MedicineSales theHelper = new MedicineSales(ConfigurationSettings.AppSettings["ConnectionString"].ToString());
    System.Text.StringBuilder rpt = new System.Text.StringBuilder();
    String Date = DateTime.Now.ToString("dd/MM/yyyy");
    static DataTable tempdt = new DataTable();
    protected void Page_Load(object sender, EventArgs e)
    {
        Page.Title = "Patient Injections Entry";
        GridFill();
        if (Session["userId"] == null)
        {
            Response.Redirect("../LoginPage.aspx");
        }
        if (Objmain.checkaccss(Session["CoCode"].ToString(), Session["UserRoleID"].ToString().Trim(), "PATIENT INVOICE LIST", checkAccessType.ViewAction) == false)
        {
            Response.Redirect("../AccessDenied.aspx");
        }

        if (!IsPostBack)
        {

            Tab1Func();
            //addNewItem(ds1)
            BindGrid("");
        }
    }
    private void GridFill()
    {
        GridView1.DataSource = thepatient.GetAllDoc(Session["CoCode"].ToString(), Session["YearCode"].ToString());
        GridView1.DataBind();
    }

    public void Tab1Func()
    {
        Tab1.CssClass = "Clicked";
        Tab2.CssClass = "Initial";
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
        MainView.ActiveViewIndex = 1;
    }

    public void BindGrid(string Regno, string docno = "", string PatientRegNo = "")
    {
        DataTable dt = thereq.getInjectionDtls(Session["CoCode"].ToString().Trim(), Session["YearCode"].ToString().Trim(), docno, PatientRegNo);
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
            DropDownList ddlMEDICINENAME = (DropDownList)e.Row.FindControl("ddlMEDICINENAME");
            ddlMEDICINENAME.Items.Insert(0, new ListItem("--Select--", "0"));

            TextBox txtmedicinegrp = (TextBox)e.Row.FindControl("txtmedicinegrp");
            TextBox txtmedicine = (TextBox)e.Row.FindControl("txtmedicine");
            TextBox txtdose = (TextBox)e.Row.FindControl("txtdose");
            TextBox txtduration = (TextBox)e.Row.FindControl("txtduration");

            ddlMEDICINENAME.Items.Clear();
            ddlMEDICINENAME.DataSource = theHelper.getInjectionsList(Session["CoCode"].ToString().Trim());
            ddlMEDICINENAME.DataTextField = "MedicineName";
            ddlMEDICINENAME.DataValueField = "MedicineID";
            ddlMEDICINENAME.DataBind();
             
            ddlMEDICINENAME.SelectedValue = txtmedicine.Text.Trim(); 
        }
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
         
        tempdt.Columns.Add(new DataColumn("MedicineId", typeof(string)));
        tempdt.Columns.Add(new DataColumn("Dates", typeof(string)));
        tempdt.Columns.Add(new DataColumn("NoofDays", typeof(string)));
        tempdt.Columns.Add(new DataColumn("SIG", typeof(string)));
        tempdt.Columns.Add(new DataColumn("ADV", typeof(string))); 
    }
    public void fillTempTable()
    {
        DataRow drow = null;
        foreach (GridViewRow rw in gvPrepdtl.Rows)
        {             
            DropDownList ddlMEDICINENAME = (DropDownList)rw.FindControl("ddlMEDICINENAME"); 
            TextBox txtdates = (TextBox)rw.FindControl("txtdates");
            TextBox txtNoofDays = (TextBox)rw.FindControl("txtNoofDays");
            TextBox txtsig = (TextBox)rw.FindControl("txtsig");
            TextBox txtadv = (TextBox)rw.FindControl("txtadv");

            drow = tempdt.NewRow();

            drow["MedicineId"] = ddlMEDICINENAME.SelectedValue;
            drow["Dates"] = txtdates.Text;
            drow["NoofDays"] = txtNoofDays.Text;
            drow["SIG"] = txtsig.Text;
            drow["ADV"] = txtadv.Text;
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
                cmd.CommandText = "select Pr.PatientRegNo + '~' + Pr.PName+'~'+Al.LedgerID+'~'+convert(varchar, datediff(year,isnull(Pr.dob,'1950-01-01'),getdate())) as Name from opd_patientregistration Pr,AC_Ledger Al where Al.compcode=Pr.Compcode and Al.LedgerFK=Pr.PatientRegNo and Pr.compcode=@Compcode and Pr.PatientRegNo like @SearchText + '%'";
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
    protected void btnsave_Click(object sender, EventArgs e)
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

                txtdocno.Text = "PI/" + thepatient.getInjectionDocno(compcode, yearcode).Rows[0][0].ToString();

                foreach (GridViewRow rw in gvPrepdtl.Rows)
                {
                    TextBox txtdates = (TextBox)rw.FindControl("txtdates");
                    TextBox txtNoofDays = (TextBox)rw.FindControl("txtNoofDays");
                    DropDownList ddlMEDICINENAME = (DropDownList)rw.FindControl("ddlMEDICINENAME");
                    TextBox txtsig = (TextBox)rw.FindControl("txtsig");
                    TextBox txtadv = (TextBox)rw.FindControl("txtadv");
                    thepatient.InsertInjection_1(compcode, yearcode, txtDocDate.Text, txtdocno.Text, txtreg.Text, txtheader.Text, txtNoofDays.Text, txtdates.Text, ddlMEDICINENAME.SelectedValue, txtsig.Text, txtadv.Text, txtfooter.Text, txtAdvtobeFollowed.Text, Session["UserRoleID"].ToString().Trim());
                }


                thepatient.InsertInjection_2(compcode, yearcode, txtdocno.Text, txtreg.Text, txtitem1.Text, chk1.Checked == true ? 1 : 0, txtrem1.Text, txtqty1.Text, Session["UserRoleID"].ToString().Trim());
                thepatient.InsertInjection_2(compcode, yearcode, txtdocno.Text, txtreg.Text, txtitem2.Text, chk2.Checked == true ? 1 : 0, txtrem2.Text, txtqty2.Text, Session["UserRoleID"].ToString().Trim());
                thepatient.InsertInjection_2(compcode, yearcode, txtdocno.Text, txtreg.Text, txtitem3.Text, chk3.Checked == true ? 1 : 0, txtrem3.Text, txtqty3.Text, Session["UserRoleID"].ToString().Trim());
                thepatient.InsertInjection_2(compcode, yearcode, txtdocno.Text, txtreg.Text, txtitem4.Text, chk4.Checked == true ? 1 : 0, txtrem4.Text, txtqty4.Text, Session["UserRoleID"].ToString().Trim());
                thepatient.InsertInjection_2(compcode, yearcode, txtdocno.Text, txtreg.Text, txtitem5.Text, chk5.Checked == true ? 1 : 0, txtrem5.Text, txtqty5.Text, Session["UserRoleID"].ToString().Trim());
                thepatient.InsertInjection_2(compcode, yearcode, txtdocno.Text, txtreg.Text, txtitem6.Text, chk6.Checked == true ? 1 : 0, txtrem6.Text, txtqty6.Text, Session["UserRoleID"].ToString().Trim());
                thepatient.InsertInjection_2(compcode, yearcode, txtdocno.Text, txtreg.Text, txtitem7.Text, chk7.Checked == true ? 1 : 0, txtrem7.Text, txtqty7.Text, Session["UserRoleID"].ToString().Trim());
                thepatient.InsertInjection_2(compcode, yearcode, txtdocno.Text, txtreg.Text, txtitem8.Text, chk8.Checked == true ? 1 : 0, txtrem8.Text, txtqty8.Text, Session["UserRoleID"].ToString().Trim());
                thepatient.InsertInjection_2(compcode, yearcode, txtdocno.Text, txtreg.Text, txtitem9.Text, chk9.Checked == true ? 1 : 0, txtrem9.Text, txtqty9.Text, Session["UserRoleID"].ToString().Trim());
                thepatient.InsertInjection_2(compcode, yearcode, txtdocno.Text, txtreg.Text, txtitem10.Text, chk10.Checked == true ? 1 : 0, txtrem10.Text, txtqty10.Text, Session["UserRoleID"].ToString().Trim());
                thepatient.InsertInjection_2(compcode, yearcode, txtdocno.Text, txtreg.Text, txtitem11.Text, chk11.Checked == true ? 1 : 0, txtrem11.Text, txtqty11.Text, Session["UserRoleID"].ToString().Trim());
                thepatient.InsertInjection_2(compcode, yearcode, txtdocno.Text, txtreg.Text, txtitem12.Text, chk12.Checked == true ? 1 : 0, txtrem12.Text, txtqty12.Text, Session["UserRoleID"].ToString().Trim());
                thepatient.InsertInjection_2(compcode, yearcode, txtdocno.Text, txtreg.Text, txtitem13.Text, chk13.Checked == true ? 1 : 0, txtrem13.Text, txtqty13.Text, Session["UserRoleID"].ToString().Trim());


                lblError.Text = "Record Successfully Saved";
            }
        }
    }


    protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        lblError.Text = "";
        if (e.CommandName == "Select")
        {
            int index = Convert.ToInt32(e.CommandArgument);

            //DataTable dt = thereq.getInjectionDtls(Session["CoCode"].ToString().Trim(), Session["YearCode"].ToString().Trim(), docno, PatientRegNo);
            //ViewState["data"] = dt;
            //gvPrepdtl.DataSource = dt;
            //gvPrepdtl.DataBind();

        }

    }
    protected void GridView1_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
       // Label lblDoctorId = (Label)GridView1.Rows[e.RowIndex].FindControl("lblDoctorId");
       // theHelper.DeleteDOC_MASTER(lblDoctorId.Text, Session["CoCode"].ToString());
        lblError.ForeColor = System.Drawing.Color.Green;
        lblError.Text = "Deleted Successfully";
        GridFill();
       // ResetAllFields();
    }
    protected void GridView1_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        GridView1.PageIndex = e.NewPageIndex;
        GridFill();
    }
    protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
    {

    }
}