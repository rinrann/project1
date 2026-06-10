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
 

public partial class IPD_PatientDoctorVisit : System.Web.UI.Page
{
    PatientDoctorVisit thedocvisit = new PatientDoctorVisit(ConfigurationSettings.AppSettings["ConnectionString"].ToString());
    PatientsDetails thepatientdetails = new PatientsDetails(ConfigurationSettings.AppSettings["ConnectionString"].ToString());
    ServiceTemplate theshift = new ServiceTemplate(ConfigurationSettings.AppSettings["ConnectionString"].ToString());
    AddConsumable theaddConsumable = new AddConsumable(ConfigurationSettings.AppSettings["ConnectionString"].ToString());
    System.Text.StringBuilder rpt = new System.Text.StringBuilder();

    DropDownList d1, d2, d3, d4, d5, d6, d7;
    TextBox t1, t2, t3, t4, t5, t6, t7, t8, t9, t10, t11;
    public static DataTable dt_consumables1;
    protected void Page_Load(object sender, EventArgs e)
    {      
        Page.Title = "Doctor Visit";
        if (Session["userName"] == null)
        {
            Response.Redirect("../LoginPage.aspx");
        }

        if (!IsPostBack)
        {
            DropDownFill();
            HiddenField4.Value = "";
            Session["CurrentTableConsumable"]=null;
            Session["CurrentTable"] = null;
            CheckBox1.Checked = true;
            TextBox202.Text = DateTime.Now.ToString("dd/MM/yyyy");
            TextBox7.Text = DateTime.Now.ToString("dd/MM/yyyy");
            TextBox20.Text = DateTime.Now.ToString("dd/MM/yyyy");
            txtdate.Text = DateTime.Now.Date.ToString("dd/MM/yyyy");
            //TextBox1.Text = DateTime.Now.Date.ToString("HH:mm");
            TextBox1.Text = string.Format("{0:hh:mm tt}", DateTime.Now);
            TextBox9.Text = DateTime.Now.Date.ToString("dd/MM/yyyy");
            TextBox24.Text = DateTime.Now.Date.ToString("HH:mm");

            TextBox136.Text = DateTime.Now.Date.ToString("dd/MM/yyyy");
            //TextBox23.Text = DateTime.Now.Date.ToString("HH:mm");
            TextBox23.Text = string.Format("{0:hh:mm tt}", DateTime.Now);
            TextBox24.Text = string.Format("{0:hh:mm tt}", DateTime.Now);
            if (Session["RegNo"] != null)
            {
                TextBox2.Text = Session["RegNo"].ToString();
                Fill();
                GeneratePrescritionId();
            }

     
            Tab1Func();

            dt_consumables1 = new DataTable();
            dt_consumables1.Columns.Add("RowID", typeof(string));
            dt_consumables1.Columns.Add("ConsumableGrId", typeof(string));
            dt_consumables1.Columns.Add("ConGroupName", typeof(string));
            dt_consumables1.Columns.Add("ConsumableItemId", typeof(string));
            dt_consumables1.Columns.Add("ConItemName", typeof(string));
            dt_consumables1.Columns.Add("ActualQty", typeof(string));
            dt_consumables1.Columns.Add("BillQty", typeof(string));
            dt_consumables1.Columns.Add("Price", typeof(string));
            Session["CurrentTable"] = null;
        }
        Session["RegNo"] = null;


    }

    public void GeneratePrescritionId()
    {
        DataTable dtPrscriptionId = thedocvisit.GetPrescriptionIdAccordingDoctor(Session["CoCode"].ToString().Trim(), Session["YearCode"].ToString().Trim(), TextBox2.Text, DropDownList1.SelectedValue);
        DataTable dt = thedocvisit.GenerateIPDPrescription(Session["CoCode"].ToString().Trim());

        if (dtPrscriptionId.Rows.Count > 0)
        {
            TextBox201.Text = dtPrscriptionId.Rows[0][0].ToString();
            TextBox6.Text = dtPrscriptionId.Rows[0][0].ToString();
            TextBox19.Text = dtPrscriptionId.Rows[0][0].ToString();
        }
        else
        {
            TextBox201.Text = dt.Rows[0][0].ToString();
            TextBox6.Text = dt.Rows[0][0].ToString();
            TextBox19.Text = dt.Rows[0][0].ToString();
        }
    }

    public void SubGroupFill(string  value, DropDownList drop2)
    {
        drop2.Items.Clear();
        drop2.DataSource = thepatientdetails.DropdownSubGroup(Session["CoCode"].ToString().Trim(),value);
        drop2.DataTextField = "SubGrName";
        drop2.DataValueField = "ID";
        drop2.DataBind();
        drop2.Items.Insert(0, new ListItem("--Select--", "0"));
    }

    public void MedicineFill(string sub, DropDownList drop3)
    {
        drop3.Items.Clear();
        drop3.DataSource = thepatientdetails.DropdownMedicine(Session["CoCode"].ToString().Trim(),sub);
        drop3.DataTextField = "MedicineName";
        drop3.DataValueField = "MedicineID";
        drop3.DataBind();
        drop3.Items.Insert(0, new ListItem("--Select--", "0"));
    }



    public void GetDose(string medicine, DropDownList t)
    {
        //DataTable dt = thepatientdetails.GetDose(medicine);
        //if (dt.Rows.Count > 0)
        //    t.SelectedValue = dt.Rows[0][0].ToString();
        //else
        //{
        //    lblError.ForeColor = System.Drawing.Color.Blue;
        //    lblError.Text = "No Dose in This Combination";
        //}
    }
    public void DoctorVisitDetails(DataSet ds)
    {
        // UnderDoctorPopup Doctor Fill

        DropDownList2.SelectedValue = ds.Tables[6].Rows[0]["DocTypeId"].ToString();
        DoctorFill(ds.Tables[6].Rows[0]["DocTypeId"].ToString());
        DropDownList1.SelectedValue = ds.Tables[6].Rows[0]["doc_id"].ToString();
        HiddenField4.Value = "1";
        // For Doctor Visit List
        if (ds.Tables[3].Rows.Count > 0)
        {
            GridView1.DataSource = ds.Tables[3];
            GridView1.DataBind();
            TextBox3.Text = ds.Tables[3].Rows[0]["patient_name"].ToString();
            TextBox4.Text = ds.Tables[3].Rows[0]["BedNoText"].ToString();
            TextBox5.Text = ds.Tables[3].Rows[0]["adate"].ToString();
           // TextBox28.Text = ds.Tables[3].Rows[0]["TreatementNote"].ToString();
            TextBox28.Text = "";
            TextBox8.Text = ds.Tables[3].Rows[0]["ProbableDischargeDate"].ToString();
            DropDownList3.Items.Clear();
            DropDownList3.Items.Insert(0, new ListItem("Day Visit", "D"));
            DropDownList3.Items.Insert(1, new ListItem("Night Visit", "N"));
            string type = "";
            if (ds.Tables[3].Rows[0]["VisitType"] == DBNull.Value)
            {
                type = "D";
            }
            else
            {
                type = ds.Tables[3].Rows[0]["VisitType"].ToString();
            }
            DropDownList3.SelectedValue = type;
        }
        else
        {    //  Without Doctor Visit List
            if (ds.Tables[4].Rows.Count > 0)
            {
                TextBox3.Text = ds.Tables[4].Rows[0]["patient_name"].ToString();
                TextBox4.Text = ds.Tables[4].Rows[0]["BedNoText"].ToString();
                TextBox5.Text = ds.Tables[4].Rows[0]["adate"].ToString();
                TextBox8.Text = ds.Tables[4].Rows[0]["ProbableDischargeDate"].ToString();
                DropDownList3.Items.Clear();
                DropDownList3.Items.Insert(0, new ListItem("Day Visit", "D"));
                DropDownList3.Items.Insert(1, new ListItem("Night Visit", "N"));
            }
            else
            {
                lblError.ForeColor = System.Drawing.Color.Red;
                lblError.Text = "No Record";
            }
        }
    }

    public void PreviousPrescriptionDetails(DataSet ds)
    {
        GridView5.DataSource = ds.Tables[0];
        GridView5.DataBind();
    }

    public void PreviousServiceDetails(DataSet ds)
    {
        GridView2.DataSource = ds.Tables[1];
        GridView2.DataBind();
    }

    public void PreviousConsumableDetails(DataSet ds)
    {
        GridView3.DataSource = ds.Tables[2];
        GridView3.DataBind();
    }

    public void PreviousRunningPrescriptionDetails(DataSet ds)
    {
        GridView9.DataSource = ds.Tables[5];
        GridView9.DataBind();
    }
    private void Fill()
    {
        DataSet ds = thedocvisit.GetDoctorVisitDetails(Session["CoCode"].ToString().Trim(), Session["YearCode"].ToString().Trim(), TextBox2.Text);

        // For Doctor Visit List
        DoctorVisitDetails(ds);

        //For Previous Prescription Details ....
        PreviousPrescriptionDetails(ds);


        // For GridFill Service Details 
        PreviousServiceDetails(ds);

        //For Consumable Details  ...................
        PreviousConsumableDetails(ds);

        GridFillBody();

        // For Running Prescription Details...........................
        PreviousRunningPrescriptionDetails(ds);
    }


    private void ResetAllFields()
    {
        DropDownList1.SelectedIndex = 0; DropDownList2.SelectedIndex = 0; DropDownList35.SelectedIndex = 0;
        TextBox1.Text = "";
        TextBox2.Text = ""; 
        TextBox3.Text = "";
        TextBox4.Text = "";
        TextBox5.Text = ""; TextBox8.Text = "";
        txtdate.Text = "";
        TextBox26.Text = "";
     
        for (int i = 0, d = 4, t = 6; i < 10; i++, d = d + 3, t = t + 2)
        {
            d1 = (DropDownList)Page.FindControl("ctl00$ContentPlaceHolder1$DropDownList" + d.ToString());
            d2 = (DropDownList)Page.FindControl("ctl00$ContentPlaceHolder1$DropDownList" + (d + 1).ToString());
            d3 = (DropDownList)Page.FindControl("ctl00$ContentPlaceHolder1$DropDownList" + (d + 2).ToString());
            t1 = (TextBox)Page.FindControl("ctl00$ContentPlaceHolder1$TextBox" + t.ToString());
            t2 = (TextBox)Page.FindControl("ctl00$ContentPlaceHolder1$TextBox" + (t + 1).ToString());
            d1.SelectedIndex = 0; d2.SelectedIndex = 0; d3.SelectedIndex = 0;
            t1.Text = ""; t2.Text = "";
        }
        Button1.Text = "Submit";

    }
    //protected void InsrtMapTable()
    //{
        
    //    for (int i = 0, d = 4, t = 6; i < 10; i++, d = d + 3, t = t + 2)
    //    {
    //        d1 = (DropDownList)Page.FindControl("ctl00$ContentPlaceHolder1$DropDownList" + d.ToString());
    //        d2 = (DropDownList)Page.FindControl("ctl00$ContentPlaceHolder1$DropDownList" + (d + 1).ToString());
    //        d3 = (DropDownList)Page.FindControl("ctl00$ContentPlaceHolder1$DropDownList" + (d + 2).ToString());
    //        t1 = (TextBox)Page.FindControl("ctl00$ContentPlaceHolder1$TextBox" + t.ToString());
    //        t2 = (TextBox)Page.FindControl("ctl00$ContentPlaceHolder1$TextBox" + (t + 1).ToString());
    //        if (d1.SelectedIndex != 0 && d2.SelectedIndex != 0 && d3.SelectedIndex != 0)
    //            thedocvisit.InsertdocvisitPrescription(TextBox2.Text, DropDownList35.SelectedValue, d1.SelectedValue, d2.SelectedValue, d3.SelectedValue, t1.Text, t2.Text);
    //        else
    //            break;
    //    }
    //}
    protected void Button1_Click(object sender, EventArgs e)
    {
        string  probDisDate;
         System.Globalization.DateTimeFormatInfo dtf = new DateTimeFormatInfo();
        DateTime testdate = DateTime.ParseExact(txtdate.Text, "dd/MM/yyyy", dtf);
        if (TextBox8.Text != "")
            probDisDate = DateTime.ParseExact(TextBox8.Text, "dd/MM/yyyy", dtf).ToString("yyyy-MM-dd");
        else
            probDisDate = "";
        string flag = "1";
        if (CheckBox1.Checked == true)
            flag = "1";
        else
            flag ="0";
        if (Button1.Text == "Submit")
        {

            if (thedocvisit.Insertdocvisit(TextBox1.Text, flag, probDisDate, TextBox2.Text, DropDownList1.SelectedValue, testdate.ToString("yyyy-MM-dd"), DropDownList2.SelectedValue, TextBox26.Text, DropDownList3.SelectedValue, Session["CoCode"].ToString().Trim(), Session["YearCode"].ToString().Trim(), Session["userName"].ToString().Trim()) == true)
            { 
                ScriptManager.RegisterStartupScript(this, this.GetType(), "msg", "alert('Inserted  Successfully !');", true);
               // Response.Redirect("../IPD/AdmissionPatientList.aspx");
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "msg", "alert('Error in Inserted Data !');", true);
            }
        }
        else
        {
            if (thedocvisit.UpdateDocVisit(TextBox1.Text, flag, HiddenField1.Value, TextBox2.Text, probDisDate, DropDownList1.SelectedValue, testdate.ToString("yyyy-MM-dd"), DropDownList2.SelectedValue, TextBox26.Text, DropDownList3.SelectedValue, Session["CoCode"].ToString().Trim(), Session["YearCode"].ToString().Trim(), Session["userName"].ToString().Trim()) == true)
            { 
                ScriptManager.RegisterStartupScript(this, this.GetType(), "msg", "alert('Updated  Successfully !');", true);
                Button1.Text = "Submit";
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "msg", "alert('Error in Updated data !');", true);
            }
        }
        Fill();
        Button1.Enabled = false;
      // ResetAllFields();
    }


    protected void Button2_Click(object sender, EventArgs e)
    {
      ResetAllFields();
    }
    private void DropDownFill()
    {
          DropDownList2.Items.Clear();
          this.DropDownList2.DataSource = thedocvisit.DropdownDoctorType(Session["CoCode"].ToString().Trim());
        this.DropDownList2.DataTextField = "TypeName";
        this.DropDownList2.DataValueField = "DocTypeId";
        this.DropDownList2.DataBind();
        this.DropDownList2.Items.Insert(0, new ListItem("--Select--", "0"));

        ddlvisiteddoc.Items.Clear();
        this.ddlvisiteddoc.DataSource = thedocvisit.getVisitedDoctors(Session["CoCode"].ToString().Trim(), Session["YearCode"].ToString().Trim(),Session["RegNo"].ToString());
        this.ddlvisiteddoc.DataTextField = "doc_name";
        this.ddlvisiteddoc.DataValueField = "doc_id";
        this.ddlvisiteddoc.DataBind();
        this.ddlvisiteddoc.Items.Insert(0, new ListItem("--Select--", "0"));

        for (int i = 21; i <= 30;i++)
        { 
            d5 = (DropDownList)Page.FindControl("ctl00$ContentPlaceHolder1$ddlDuration" + i.ToString());
            d5.Items.Clear();
            d5.DataSource = thedocvisit.DropdownDuration(Session["CoCode"].ToString().Trim());
            d5.DataTextField = "DurationName";
            d5.DataValueField = "DurationId";
            d5.DataBind();
            d5.Items.Insert(0, new ListItem("--Select--", "0"));
        }

        for (int d=1; d <=10; d++)
        {
            d1 = (DropDownList)Page.FindControl("ctl00$ContentPlaceHolder1$ddlMedicineGroup" + d.ToString());
            d2 = (DropDownList)Page.FindControl("ctl00$ContentPlaceHolder1$ddlSubGroup" + d.ToString());
            d3 = (DropDownList)Page.FindControl("ctl00$ContentPlaceHolder1$ddlMedicine" + d.ToString());
            d1.DataSource = thepatientdetails.DropdownGroup(Session["CoCode"].ToString().Trim());
            d1.DataTextField = "MedicineGroupName";
            d1.DataValueField = "MedicineGroupID";
            d1.DataBind();
            d1.Items.Insert(0, new ListItem("--Select--", "0"));

            d2.Items.Insert(0, new ListItem("--Select--", "0"));

            d3.Items.Insert(0, new ListItem("--Select--", "0"));

        }


        DropDownList35.Items.Clear();
        this.DropDownList35.DataSource = thedocvisit.DropdownTemplate(Session["CoCode"].ToString().Trim());
        this.DropDownList35.DataTextField = "PrescrpTemName";
        this.DropDownList35.DataValueField = "PrescrpTemID";
        this.DropDownList35.DataBind();
        this.DropDownList35.Items.Insert(0, new ListItem("--Select--", "0"));
        


        DropDownList34.DataSource = thepatientdetails.DropdowntemplateGroup(Session["CoCode"].ToString().Trim());
        DropDownList34.DataTextField = "PrescriptionGroupName";
        DropDownList34.DataValueField = "RowId";
        DropDownList34.DataBind();
        DropDownList34.Items.Insert(0, new ListItem("--Select--", "0"));

        DropDownList35.Items.Insert(0, new ListItem("--Select--", "0"));


        this.DropDownList1.Items.Insert(0, new ListItem("--Select--", "0"));

        DropDownList d11, d22, d33;
        for (int d=1; d <= 10; d++)
        {
            d11 = (DropDownList)Page.FindControl("ctl00$ContentPlaceHolder1$ddlMedicineGroup" + d.ToString());
            d11.Items.Clear();
            d11.DataSource = thedocvisit.DropdownMedicineGrp(Session["CoCode"].ToString().Trim());
            d11.DataTextField = "MedicineGroupName";
            d11.DataValueField = "MedicineGroupID";
            d11.DataBind();
            d11.Items.Insert(0, new ListItem("--Select--", "0"));

            d22 = (DropDownList)Page.FindControl("ctl00$ContentPlaceHolder1$ddlSubGroup" + d.ToString());
            d22.Items.Insert(0, new ListItem("--Select--", "0"));

            d33 = (DropDownList)Page.FindControl("ctl00$ContentPlaceHolder1$ddlMedicine" + d.ToString());
            d33.Items.Insert(0, new ListItem("--Select--", "0"));

            //d33 = (DropDownList)Page.FindControl("ctl00$ContentPlaceHolder1$DropDownList" + d.ToString());
            //d33.Items.Clear();
            //d33.DataSource = thedocvisit.DropdownRoute();
            //d33.DataTextField = "RouteName";
            //d33.DataValueField = "RouteID";
            //d33.DataBind();
            //d33.Items.Insert(0, new ListItem("--Select--", "0"));
        }



        DropDownList36.Items.Clear();
        this.DropDownList36.DataSource = thedocvisit.GetServiceConsumableTemplateeGroup(Session["CoCode"].ToString().Trim());
        this.DropDownList36.DataTextField = "CategoryName";
        this.DropDownList36.DataValueField = "TemplateCategoryId";
        this.DropDownList36.DataBind();
        this.DropDownList36.Items.Insert(0, new ListItem("--Select--", "0"));




        DropDownList65.Items.Clear();
        this.DropDownList65.DataSource = thedocvisit.GetServiceConsumableTemplateeGroup(Session["CoCode"].ToString().Trim());
        this.DropDownList65.DataTextField = "CategoryName";
        this.DropDownList65.DataValueField = "TemplateCategoryId";
        this.DropDownList65.DataBind();
        this.DropDownList65.Items.Insert(0, new ListItem("--Select--", "0"));


        for (int i = 1; i <= 10; i++)
        {
            d2 = (DropDownList)Page.FindControl("ctl00$ContentPlaceHolder1$ddlDose" + i.ToString());
            d2.Items.Clear();
            d2.DataSource = thedocvisit.DropdownDose(Session["CoCode"].ToString().Trim());
            d2.DataTextField = "DoseName";
            d2.DataValueField = "ID";
            d2.DataBind();
            d2.Items.Insert(0, new ListItem("--Select--", "0"));
        }

        for (int i = 1; i < 2; i++)
        {
            
            d1 = (DropDownList)Page.FindControl("ctl00$ContentPlaceHolder1$ddlconsumablegr" + i.ToString());
            d1.Items.Clear();
            d1.DataSource = thedocvisit.GetConsumableGroup(Session["CoCode"].ToString().Trim());
            d1.DataTextField = "ConGroupName";
            d1.DataValueField = "ConGrId";
            d1.DataBind();
            d1.Items.Insert(0, new ListItem("--Select--", "0"));

       


            //d2 = (DropDownList)Page.FindControl("ctl00$ContentPlaceHolder1$ddlserviceCat" + i.ToString());
            //d2.Items.Clear();
            //d2.DataSource = thedocvisit.GetServiceGroup();
            //d2.DataTextField = "ServiceCategoryName";
            //d2.DataValueField = "ServiceCategoryID";
            //d2.DataBind();
            //d2.Items.Insert(0, new ListItem("--Select--", "0"));

            if (i != 11)
            {
                d3 = (DropDownList)Page.FindControl("ctl00$ContentPlaceHolder1$ddlBatchNo" + i.ToString());
                d3.Items.Insert(0, new ListItem("--Select--", "0"));
            }
        }

    }

    public void DropDownMedicineFill(DropDownList drop1, DropDownList drop2)
    {
         drop2.Items.Clear();
        drop2.DataSource = thedocvisit.DropdownMedicine(Session["CoCode"].ToString().Trim(),drop1.SelectedValue);
        drop2.DataTextField = "MedicineName";
        drop2.DataValueField = "MedicineID";
        drop2.DataBind();
        drop2.Items.Insert(0, new ListItem("--Select--", "0"));
    }
    protected void GridView1_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        GridView1.PageIndex = e.NewPageIndex;
        Fill();
    }
    protected int SearchIndex(string Value, DropDownList ddl)
    {
        int i;
        for (i = 0; i < ddl.Items.Count; i++)
        {
            if (ddl.Items[i].Value.Trim() == Value.Trim())
                return i;
        }
        return -1;
    }


    protected int SearchIndexText(string Value, DropDownList ddl)
    {
        int i;
        for (i = 0; i < ddl.Items.Count; i++)
        {
            if (ddl.Items[i].Text.Trim() == Value.Trim())
                return i;
        }
        return -1;
    }

    //public void TableFill()
    //{
  
    //      DataTable dt = thedocvisit.GetTableFill(TextBox2.Text);
    //      if (dt.Rows.Count > 0)
    //      {
    //          DropDownList35.SelectedValue = dt.Rows[0]["DocPresId"].ToString();
    //          for (int i = 0, d = 4, t = 6; i < dt.Rows.Count; i++, d = d + 3, t = t + 2)
    //          {
    //              d1 = (DropDownList)Page.FindControl("ctl00$ContentPlaceHolder1$DropDownList" + d.ToString());
    //              d2 = (DropDownList)Page.FindControl("ctl00$ContentPlaceHolder1$DropDownList" + (d + 1).ToString());
    //              d3 = (DropDownList)Page.FindControl("ctl00$ContentPlaceHolder1$DropDownList" + (d + 2).ToString());
    //              t1 = (TextBox)Page.FindControl("ctl00$ContentPlaceHolder1$TextBox" + t.ToString());
    //              t2 = (TextBox)Page.FindControl("ctl00$ContentPlaceHolder1$TextBox" + (t + 1).ToString());
    //              DDLMedicineFill();
    //              d1.SelectedValue = dt.Rows[i]["MedicineGroupID"].ToString();
    //              d2.SelectedValue = dt.Rows[i]["MedicineID"].ToString();
    //              d3.SelectedValue = dt.Rows[i]["RouteID"].ToString();
    //              t1.Text = dt.Rows[i]["DailyDose"].ToString();
    //              t2.Text = dt.Rows[i]["Duration"].ToString();
    //          }
    //      }
    //}

    protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "Select")
        {
            Button1.Enabled = true;
            int index = Convert.ToInt32(e.CommandArgument);
            lblError.Text = "";
            Label lblid = (Label)GridView1.Rows[index].FindControl("lblid");
            HiddenField1.Value = lblid.Text;


            Label lblRegno = (Label)GridView1.Rows[index].FindControl("lblRegno");
            HiddenField2.Value = lblRegno.Text;
            TextBox2.Text = lblRegno.Text;
            Label lbllname = (Label)GridView1.Rows[index].FindControl("lbllname");
            TextBox3.Text = lbllname.Text;
            Label lbladate = (Label)GridView1.Rows[index].FindControl("lbladate");
            TextBox5.Text = lbladate.Text;
            Label lblbedno = (Label)GridView1.Rows[index].FindControl("lblbedno");
            TextBox4.Text = lblbedno.Text;

            Label lbldoctypeid = (Label)GridView1.Rows[index].FindControl("lbldoctypeid");
            DropDownList2.SelectedIndex = SearchIndex(lbldoctypeid.Text, DropDownList2);

            Label lbldocid = (Label)GridView1.Rows[index].FindControl("lbldocid");
            DDLFill();
            DropDownList1.SelectedIndex = SearchIndexText(lbldocid.Text, DropDownList1);

            Label lblTime = (Label)GridView1.Rows[index].FindControl("lblTime");
            TextBox1.Text = lblTime.Text;

            Label lblvisitdate = (Label)GridView1.Rows[index].FindControl("lblvisitdate");
            txtdate.Text = lblvisitdate.Text;


            Label lblremarks = (Label)GridView1.Rows[index].FindControl("lblremarks");
            TextBox26.Text = lblremarks.Text;
            Label visittype = (Label)GridView1.Rows[index].FindControl("lblvisittype");
            string type = "";
            if (visittype.Text == "")
            {
                type = "D";
            }
            else
            {
                type = visittype.Text;
            }

            DropDownList3.SelectedValue = type;
            
          //  TableFill();

            Button1.Text = "Update";
        }
    }



    public void DDLFill()
    {
         DropDownList1.Items.Clear();
        this.DropDownList1.DataSource = thedocvisit.DropdownDoctor(Session["CoCode"].ToString().Trim(),"0");
        this.DropDownList1.DataTextField = "doc_name";
        this.DropDownList1.DataValueField = "doc_id";
        this.DropDownList1.DataBind();
        this.DropDownList1.Items.Insert(0, new ListItem("--Select--", "0"));
    }

    public void DDLMedicineFill()
    {
         for (int i = 5; i < 33; i=i+3)
        {
            d1 = (DropDownList)Page.FindControl("ctl00$ContentPlaceHolder1$DropDownList" + i.ToString());
            d1.Items.Clear();
            d1.DataSource = thedocvisit.DropdownMedicine(Session["CoCode"].ToString().Trim(), "0");
            d1.DataTextField = "MedicineName";
            d1.DataValueField = "MedicineID";
            d1.DataBind();
            d1.Items.Insert(0, new ListItem("--Select--", "0"));
        }
    }
    protected void Button4_Click(object sender, EventArgs e)
    {
        DataSet ds = thedocvisit.GetDoctorVisitDetails(Session["CoCode"].ToString().Trim(), Session["YearCode"].ToString().Trim(), TextBox2.Text);
        Fill();
        GridFillBody();
    }

    public void DoctorFill(string value)
    {
        DropDownList1.Items.Clear();
        this.DropDownList1.DataSource = thedocvisit.DropdownDoctor(Session["CoCode"].ToString().Trim(), value);
        this.DropDownList1.DataTextField = "doc_name";
        this.DropDownList1.DataValueField = "doc_id";
        this.DropDownList1.DataBind();
        this.DropDownList1.Items.Insert(0, new ListItem("--Select--", "0"));
    }
    protected void DropDownList2_SelectedIndexChanged(object sender, EventArgs e)
    {
        DoctorFill(DropDownList2.SelectedValue);
    }
    
 
    protected void DropDownList3_SelectedIndexChanged(object sender, EventArgs e)
    {
      
         DataTable dt = thedocvisit.GetTemplateMapping(Session["CoCode"].ToString().Trim(),DropDownList35.Text);
        for (int i = 0, d = 4, t =6; i < dt.Rows.Count; i++,d=d+3,t=t+2)
        {
            d1 = (DropDownList)Page.FindControl("ctl00$ContentPlaceHolder1$DropDownList" + d.ToString());
            d2 = (DropDownList)Page.FindControl("ctl00$ContentPlaceHolder1$DropDownList" + (d+1).ToString());
            d3 = (DropDownList)Page.FindControl("ctl00$ContentPlaceHolder1$DropDownList" + (d+2).ToString());
            t1 = (TextBox)Page.FindControl("ctl00$ContentPlaceHolder1$TextBox" + t.ToString());
            t2 = (TextBox)Page.FindControl("ctl00$ContentPlaceHolder1$TextBox" + (t+1).ToString());
            DDLMedicineFill();
            d1.SelectedValue = dt.Rows[i]["MedicineGroupID"].ToString();
            d2.SelectedValue = dt.Rows[i]["MedicineID"].ToString();
            d3.SelectedValue = dt.Rows[i]["RouteID"].ToString();
            t1.Text = dt.Rows[i]["DailyDose"].ToString();
            t2.Text = dt.Rows[i]["Duration"].ToString();          
        }

    }

  
    protected void Button13_Click(object sender, EventArgs e)
    {
        string  testdate;
        string bp = TextBox139.Text + "/" + TextBox25.Text;
        System.Globalization.DateTimeFormatInfo dtf = new DateTimeFormatInfo();
        if (TextBox136.Text != "")
            testdate = DateTime.ParseExact(TextBox136.Text, "dd/MM/yyyy", dtf).ToString("yyyy-MM-dd");
        else
            testdate = DateTime.Now.ToString("yyyy-MM-dd");
       // string date = testdate.ToString("yyyy-MM-dd");
        if (Button13.Text == "Submit")
        {
            if (thedocvisit.ClinicalFinding_Insert_Update(1, TextBox23.Text, null, TextBox2.Text, testdate.ToString(), TextBox138.Text, bp, TextBox142.Text, TextBox143.Text, TextBox144.Text, TextBox146.Text, Session["CoCode"].ToString().Trim(), Session["userName"].ToString().Trim()) == true)
            {
                string[] ComplainId = HiddenField3.Value.Split(',');
                if (ComplainId.Length > 0)
                {
                    for (int i = 0; i < ComplainId.Length; i++)
                    {
                        thedocvisit.ComplainMap_Insert_Delete(1, TextBox2.Text, testdate.ToString(), TextBox23.Text, ComplainId[i], Session["CoCode"].ToString().Trim(), Session["userName"].ToString().Trim());
                    }
                }
                ScriptManager.RegisterStartupScript(this, this.GetType(), "msg", "alert('Patient Clinical Finding Inserted Successfully !');", true);
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "msg", "alert('Error in Inserted Data to Patient Clinical Finding !');", true);
            }
        }
        else
        {
            if (thedocvisit.ClinicalFinding_Insert_Update(2, TextBox23.Text, HiddenField1.Value, TextBox2.Text, testdate.ToString(), TextBox138.Text, bp, TextBox142.Text, TextBox143.Text, TextBox144.Text, TextBox146.Text, Session["CoCode"].ToString().Trim(), Session["userName"].ToString().Trim()) == true)
            {
                thedocvisit.ComplainMap_Insert_Delete(2, TextBox2.Text, testdate.ToString(), TextBox23.Text, null, Session["CoCode"].ToString().Trim(), Session["userName"].ToString().Trim());
                string[] ComplainId = HiddenField3.Value.Split(',');
                if (ComplainId.Length > 0)
                {
                    for (int i = 0; i < ComplainId.Length; i++)
                    {
                        thedocvisit.ComplainMap_Insert_Delete(1, TextBox2.Text, testdate.ToString(), TextBox23.Text, ComplainId[i], Session["CoCode"].ToString().Trim(), Session["userName"].ToString().Trim());
                    }
                }
                ScriptManager.RegisterStartupScript(this, this.GetType(), "msg", "alert('Patient Clinical Finding Inserted Successfully !');", true);
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "msg", "alert('Error in Inserted Data to Patient Clinical Finding !');", true);
            }
        }

        TextBox136.Enabled = true; TextBox23.Enabled = true;
        GridFillBody();
        ResetBody();

    }

    public void ResetBody()
    {
        TextBox136.Text = ""; TextBox21.Text = ""; TextBox139.Text = ""; TextBox138.Text = ""; TextBox142.Text = ""; TextBox143.Text = ""; TextBox144.Text = "";
        TextBox146.Text = ""; TextBox9.Text = ""; TextBox22.Text = ""; TextBox10.Text = "";

        TextBox11.Text = ""; TextBox12.Text = ""; TextBox13.Text = ""; TextBox14.Text = ""; TextBox15.Text = ""; TextBox16.Text = ""; TextBox17.Text = "";
        TextBox18.Text = ""; TextBox23.Text = ""; TextBox24.Text = "";
        Button13.Text = "Submit";
        Button9.Text = "Submit";
        LinkButton2.Text = "Add Complain";
        LinkButton3.Text = "Add Complain";
        HiddenField1.Value = "";

    }

    public void GridFillBody()
    {
        GridView4.DataSource = thedocvisit.GridFillBody(Session["CoCode"].ToString().Trim(),TextBox2.Text);
        GridView4.DataBind();

        GridView6.DataSource = thedocvisit.GridFillBody(Session["CoCode"].ToString().Trim(),TextBox2.Text);
        GridView6.DataBind();
    }

    protected void DropDownList34_SelectedIndexChanged(object sender, EventArgs e)
    {
        DropDownList35.DataSource = thedocvisit.Dropdowntemplate(Session["CoCode"].ToString().Trim(),DropDownList34.SelectedValue);
        DropDownList35.DataTextField = "PrescrpTemName";
        DropDownList35.DataValueField = "PrescrpTemID";
        DropDownList35.DataBind();
        DropDownList35.Items.Insert(0, new ListItem("--Select--", "0"));
    }


    protected void ddlMedicine1_SelectedIndexChanged(object sender, EventArgs e)
    {
        GetDose(ddlMedicine1.SelectedValue, ddlDose1);
        BatchNoFill(ddlMedicine1.SelectedValue, ddlBatchNo1, ddlMedicine1);
        Exdate(ddlBatchNo1, txtExpiryDate1);
    }
    protected void ddlMedicine2_SelectedIndexChanged(object sender, EventArgs e)
    {
        GetDose(ddlMedicine2.SelectedValue, ddlDose2);
        BatchNoFill(ddlMedicine2.SelectedValue, ddlBatchNo2, ddlMedicine2);
        Exdate(ddlBatchNo2, txtExpiryDate2);
    }
    protected void ddlMedicine3_SelectedIndexChanged(object sender, EventArgs e)
    {
        GetDose(ddlMedicine3.SelectedValue, ddlDose3);
        BatchNoFill(ddlMedicine3.SelectedValue, ddlBatchNo3, ddlMedicine3);
        Exdate(ddlBatchNo3, txtExpiryDate3);
    }
    protected void ddlMedicine4_SelectedIndexChanged(object sender, EventArgs e)
    {
        GetDose(ddlMedicine4.SelectedValue, ddlDose4);
        BatchNoFill(ddlMedicine4.SelectedValue, ddlBatchNo4, ddlMedicine4);
        Exdate(ddlBatchNo4, txtExpiryDate4);
    }
    protected void ddlMedicine5_SelectedIndexChanged(object sender, EventArgs e)
    {
        GetDose(ddlMedicine4.SelectedValue, ddlDose5);
        BatchNoFill(ddlMedicine5.SelectedValue, ddlBatchNo5, ddlMedicine5);
        Exdate(ddlBatchNo5, txtExpiryDate5);
    }
    protected void ddlMedicine6_SelectedIndexChanged(object sender, EventArgs e)
    {
        GetDose(ddlMedicine6.SelectedValue, ddlDose6);
        BatchNoFill(ddlMedicine6.SelectedValue, ddlBatchNo6, ddlMedicine6);
        Exdate(ddlBatchNo6, txtExpiryDate6);
    }
    protected void ddlMedicine7_SelectedIndexChanged(object sender, EventArgs e)
    {
        GetDose(ddlMedicine7.SelectedValue, ddlDose7);
        BatchNoFill(ddlMedicine7.SelectedValue, ddlBatchNo7, ddlMedicine7);
        Exdate(ddlBatchNo7, txtExpiryDate7);
    }
    protected void ddlMedicine8_SelectedIndexChanged(object sender, EventArgs e)
    {
        GetDose(ddlMedicine8.SelectedValue, ddlDose8);
        BatchNoFill(ddlMedicine8.SelectedValue, ddlBatchNo8, ddlMedicine8);
        Exdate(ddlBatchNo8, txtExpiryDate8);
    }
    protected void ddlMedicine9_SelectedIndexChanged(object sender, EventArgs e)
    {
        GetDose(ddlMedicine9.SelectedValue, ddlDose9);
        BatchNoFill(ddlMedicine9.SelectedValue, ddlBatchNo9, ddlMedicine9);
        Exdate(ddlBatchNo9, txtExpiryDate9);
    }
    protected void ddlMedicine10_SelectedIndexChanged(object sender, EventArgs e)
    {
        GetDose(ddlMedicine10.SelectedValue, ddlDose10);
        BatchNoFill(ddlMedicine10.SelectedValue, ddlBatchNo10, ddlMedicine10);
        Exdate(ddlBatchNo10, txtExpiryDate10);
    }

    public void Tab1Func()
    {
        Tab1.CssClass = "Initial";
        Tab2.CssClass = "Initial";
        Tab3.CssClass = "Initial";
        Tab4.CssClass = "Initial";
        Tab5.CssClass = "Initial";
        Tab6.CssClass = "Clicked";
        Tab7.CssClass = "Initial";
        MainView.ActiveViewIndex = 5;

    }





    protected void Tab1_Click(object sender, EventArgs e)
    {
        Tab1.CssClass = "Clicked";
        Tab2.CssClass = "Initial";
        Tab3.CssClass = "Initial";
        Tab4.CssClass = "Initial";
        Tab5.CssClass = "Initial";
        Tab6.CssClass = "Initial";
        Tab7.CssClass = "Initial";
        MainView.ActiveViewIndex = 0;
    }

    protected void Tab6_Click(object sender, EventArgs e)
    {
        Tab1Func();
    }

    protected void Tab7_Click(object sender, EventArgs e)
    {
        Tab1.CssClass = "Initial";
        Tab2.CssClass = "Initial";
        Tab3.CssClass = "Initial";
        Tab4.CssClass = "Initial";
        Tab5.CssClass = "Initial";
        Tab6.CssClass = "Initial";
        Tab7.CssClass = "Clicked";
        MainView.ActiveViewIndex = 6;
    }
    protected void Tab5_Click(object sender, EventArgs e)
    {
        Tab1.CssClass = "Initial";
        Tab2.CssClass = "Initial";
        Tab3.CssClass = "Initial";
        Tab4.CssClass = "Initial";
        Tab5.CssClass = "Clicked";
        Tab6.CssClass = "Initial";
        Tab7.CssClass = "Initial";
        MainView.ActiveViewIndex = 1;
    }

    protected void Tab2_Click(object sender, EventArgs e)
    {
        Tab1.CssClass = "Initial";
        Tab2.CssClass = "Clicked";
        Tab3.CssClass = "Initial";
        Tab4.CssClass = "Initial";
        Tab5.CssClass = "Initial";
        Tab6.CssClass = "Initial";
        Tab7.CssClass = "Initial";
        MainView.ActiveViewIndex = 2;
    }
    protected void Tab3_Click(object sender, EventArgs e)
    {
        Tab1.CssClass = "Initial";
        Tab2.CssClass = "Initial";
        Tab3.CssClass = "Clicked";
        Tab4.CssClass = "Initial";
        Tab5.CssClass = "Initial";
        Tab6.CssClass = "Initial";
        Tab7.CssClass = "Initial";
        MainView.ActiveViewIndex = 3;
    }
    protected void Tab4_Click(object sender, EventArgs e)
    {
        Tab1.CssClass = "Initial";
        Tab2.CssClass = "Initial";
        Tab3.CssClass = "Initial";
        Tab5.CssClass = "Initial";
        Tab4.CssClass = "Clicked";
        Tab5.CssClass = "Initial";
        Tab6.CssClass = "Initial";
        Tab7.CssClass = "Initial";
        MainView.ActiveViewIndex = 4;
    }

    protected void Button14_Click(object sender, EventArgs e)
    {

    }

    public void Resetprescription()
    {
        DropDownList d21;
        for (int t = 1; t <= 10; t++)
        {
            d21 = (DropDownList)Page.FindControl("ctl00$ContentPlaceHolder1$ddlDose" + t.ToString());
            d21.SelectedIndex = 0;
        }

        for (int d = 1; d <= 10; d++)
        {
            d1 = (DropDownList)Page.FindControl("ctl00$ContentPlaceHolder1$ddlMedicineGroup" + d.ToString());
            d1.SelectedIndex = 0;

            d1 = (DropDownList)Page.FindControl("ctl00$ContentPlaceHolder1$ddlSubGroup" + d.ToString());
            d1.SelectedIndex = 0;

            d1 = (DropDownList)Page.FindControl("ctl00$ContentPlaceHolder1$ddlMedicine" + d.ToString());
            d1.SelectedIndex = 0;
        }

    }

    public void BatchNoFill(string value, DropDownList ddlBatchNo, DropDownList ddlMedicine=null)
    {  
        ddlBatchNo.Items.Clear();
        ddlBatchNo.DataSource = thedocvisit.DropdownBatchNo(Session["CoCode"].ToString().Trim(),Session["YearCode"].ToString().Trim(), value);
        ddlBatchNo.DataTextField = "BatchNo";
        ddlBatchNo.DataValueField = "BatchNo";
        ddlBatchNo.DataBind();
        DataTable dt = thedocvisit.GetBatchNoinStock(Session["CoCode"].ToString().Trim(), Session["YearCode"].ToString().Trim(), value, TextBox2.Text.Trim());
        if (dt.Rows.Count == 0)
        {
            if (ddlMedicine != null) { ddlMedicine.SelectedValue = "0"; }
            ScriptManager.RegisterStartupScript(this, this.GetType(), "msg", "alert('There is no medicine in stock!');", true);
        }
    }

    protected void DropDownList35_SelectedIndexChanged(object sender, EventArgs e)
    {         
        Resetprescription();
        DataTable dt = thepatientdetails.FillTemplate(Session["CoCode"].ToString().Trim(), DropDownList35.SelectedValue);
        DropDownList d21, du21;
        TextBox tx, tx2;
        for (int i = 0,t =1,txt=21; i < dt.Rows.Count; i++, t++,txt++)
        {
            tx = (TextBox)Page.FindControl("ctl00$ContentPlaceHolder1$txtBillQty" + txt.ToString());
            tx2 = (TextBox)Page.FindControl("ctl00$ContentPlaceHolder1$txtActualQty" + txt.ToString());

            d21 = (DropDownList)Page.FindControl("ctl00$ContentPlaceHolder1$ddlDose" + t.ToString());
            du21 = (DropDownList)Page.FindControl("ctl00$ContentPlaceHolder1$ddlDuration" + txt.ToString());

             d1 = (DropDownList)Page.FindControl("ctl00$ContentPlaceHolder1$ddlMedicineGroup" + t.ToString());
             d1.SelectedValue = dt.Rows[i]["MedicineGroupID"].ToString();


             d2 = (DropDownList)Page.FindControl("ctl00$ContentPlaceHolder1$ddlSubGroup" + t.ToString());
             d2.DataSource = thepatientdetails.DropdownSubGroup(Session["CoCode"].ToString().Trim(), dt.Rows[i]["MedicineGroupID"].ToString());
            d2.DataTextField = "SubGrName";
            d2.DataValueField = "ID";
            d2.DataBind();
            d2.Items.Insert(0, new ListItem("--Select--", "0"));
            d2.SelectedValue = dt.Rows[i]["SubGroupid"].ToString();


            d3 = (DropDownList)Page.FindControl("ctl00$ContentPlaceHolder1$ddlMedicine" + t.ToString());
            d3.DataSource = thepatientdetails.DropdownMedicine(Session["CoCode"].ToString().Trim(), dt.Rows[i]["SubGroupid"].ToString());
            d3.DataTextField = "MedicineName";
            d3.DataValueField = "MedicineID";
            d3.DataBind();
            d3.Items.Insert(0, new ListItem("--Select--", "0"));            
            d3.SelectedValue = dt.Rows[i]["MedicineID"].ToString();
          
         
            d4 = (DropDownList)Page.FindControl("ctl00$ContentPlaceHolder1$ddlBatchNo" + (i + 1).ToString());
            BatchNoFill(dt.Rows[i]["MedicineID"].ToString(), d4);

            d21.SelectedValue = dt.Rows[i]["DoseName"].ToString();
            du21.SelectedValue = dt.Rows[i]["ActualDuration"].ToString();

            getBillQty(dt.Rows[i]["DoseName"].ToString(), tx,dt.Rows[i]["Duration"].ToString(),tx2);

        }
    }


    protected void Button15_Click(object sender, EventArgs e)
    {
        DateTime issuedate;
        DateTime testdate;
        DateTime Exdate;
        System.Globalization.DateTimeFormatInfo dtf = new DateTimeFormatInfo();

        if (TextBox202.Text != "")
            testdate = DateTime.ParseExact(TextBox202.Text, "dd/MM/yyyy", dtf);
        else
            testdate = DateTime.Now;
        if (txtdate.Text != "")
            issuedate = DateTime.ParseExact(txtdate.Text, "dd/MM/yyyy", dtf);
        else
            issuedate = DateTime.Now;

        thedocvisit.InsertPrescription(TextBox201.Text, TextBox2.Text, testdate.ToString("yyyy-MM-dd"), TextBox204.Text, DropDownList1.SelectedValue, Session["CoCode"].ToString().Trim(), Session["userName"].ToString().Trim());
        DropDownList d5, d6;
        for (int i = 1; i <= 10; i++)
        {
            d6 = (DropDownList)Page.FindControl("ctl00$ContentPlaceHolder1$ddlDose" + i.ToString());
            t2 = (TextBox)Page.FindControl("ctl00$ContentPlaceHolder1$txtExpiryDate" + i.ToString());
            d5 = (DropDownList)Page.FindControl("ctl00$ContentPlaceHolder1$ddlDuration" + (i + 20).ToString());
            t5 = (TextBox)Page.FindControl("ctl00$ContentPlaceHolder1$txtActualQty" + (i + 20).ToString());
            t6 = (TextBox)Page.FindControl("ctl00$ContentPlaceHolder1$txtBillQty" + (i + 20).ToString());

            d1 = (DropDownList)Page.FindControl("ctl00$ContentPlaceHolder1$ddlMedicineGroup" + i.ToString());
            d2 = (DropDownList)Page.FindControl("ctl00$ContentPlaceHolder1$ddlSubGroup" + i.ToString());
            d3 = (DropDownList)Page.FindControl("ctl00$ContentPlaceHolder1$ddlMedicine" + i.ToString());
            d4 = (DropDownList)Page.FindControl("ctl00$ContentPlaceHolder1$ddlBatchNo" + i.ToString());


            if (d1.SelectedIndex != 0 && d2.SelectedIndex != 0)
            {
                if (t2.Text != "")
                    Exdate = DateTime.ParseExact(t2.Text, "dd/MM/yyyy", dtf);
                else
                    Exdate = DateTime.Now;
                if (thedocvisit.InsertPrescriptionMap(TextBox201.Text, TextBox2.Text, testdate.ToString(), DropDownList1.SelectedValue, d1.SelectedValue, d2.SelectedValue, d3.SelectedValue, d6.SelectedItem.Text, d4.SelectedValue, Exdate.ToString(), d5.SelectedItem.Text.Split(' ')[0], t5.Text, t6.Text, DateTime.Now.ToString(), Session["CoCode"].ToString().Trim(), Session["userName"].ToString().Trim()) == true)
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "msg", "alert('Patient Prescription Inserted Successfully !');", true);

                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "msg", "alert('Error in Patient Prescription Inserted !');", true);
                }
            }
            else
                break;
        }
        Fill();
        Resetprescription();
    }
    

    protected void Button16_Click(object sender, EventArgs e)
    {
        Resetprescription();
    }

    public void ServiceFill(string value, DropDownList items)
    {
        items.Items.Clear();
        items.DataSource = thedocvisit.GetServiceConsumableTemplate(Session["CoCode"].ToString().Trim(),value);
        items.DataTextField = "ServiceTemplateName";
        items.DataValueField = "NameID";
        items.DataBind();
        items.Items.Insert(0, new ListItem("--Select--", "0"));
    }

    public void ConsumableItemFill(string value, DropDownList items)
    {
        items.Items.Clear();
        items.DataSource = thedocvisit.GetConsumableItem(Session["CoCode"].ToString().Trim(), value);
        items.DataTextField = "ConItemName";
        items.DataValueField = "ConItemID";
        items.DataBind();
        items.Items.Insert(0, new ListItem("--Select--", "0"));
    }


    protected void ddlconsumablegr1_SelectedIndexChanged(object sender, EventArgs e)
    {
        ConsumableItemFill(ddlconsumablegr1.SelectedValue, ddlConsumableItem1);
    } 

    protected void DropDownList36_SelectedIndexChanged(object sender, EventArgs e)
    {
        DropDownList37.Items.Clear();
        this.DropDownList37.DataSource = thedocvisit.GetServiceConsumableTemplate(Session["CoCode"].ToString().Trim(),DropDownList36.SelectedValue);
        this.DropDownList37.DataTextField = "ServiceTemplateName";
        this.DropDownList37.DataValueField = "NameID";
        this.DropDownList37.DataBind();
        this.DropDownList37.Items.Insert(0, new ListItem("--Select--", "0"));
    }
    protected void DropDownList65_SelectedIndexChanged(object sender, EventArgs e)
    {
        DropDownList66.Items.Clear();
        this.DropDownList66.DataSource = thedocvisit.GetServiceConsumableTemplate(Session["CoCode"].ToString().Trim(),DropDownList65.SelectedValue);
        this.DropDownList66.DataTextField = "ServiceTemplateName";
        this.DropDownList66.DataValueField = "NameID";
        this.DropDownList66.DataBind();
        this.DropDownList66.Items.Insert(0, new ListItem("--Select--", "0"));
    }
    protected void DropDownList66_SelectedIndexChanged(object sender, EventArgs e)
    {  
        FillConsumable(DropDownList66.SelectedValue);
    }


    private void FillConsumable(string NameId)
    {
        DataTable dt = new DataTable();
        DataRow row = dt.NewRow();
        dt.Columns.Add("RowID", typeof(string));
        dt.Columns.Add("ConsumableGrId", typeof(string));
        dt.Columns.Add("ConGroupName", typeof(string));
        dt.Columns.Add("ConsumableItemId", typeof(string));
        dt.Columns.Add("ConItemName", typeof(string));
        dt.Columns.Add("ActualQty", typeof(string));
        dt.Columns.Add("BillQty", typeof(string));
        dt.Columns.Add("Price", typeof(string));

        if (GridView8.Rows.Count > 0)
        {
            for (int i = 0; i < GridView8.Rows.Count; i++)
            {
                Label lblid = (Label)GridView8.Rows[i].FindControl("lblid");
                Label lblConGrId = (Label)GridView8.Rows[i].FindControl("lblConGrId");
                Label lblConGroupName = (Label)GridView8.Rows[i].FindControl("lblConGroupName");
                Label lblConItemID = (Label)GridView8.Rows[i].FindControl("lblConItemID");
                Label lblConItemName = (Label)GridView8.Rows[i].FindControl("lblConItemName");
                Label lblActualQty = (Label)GridView8.Rows[i].FindControl("lblActualQty");
                Label lblBillQty = (Label)GridView8.Rows[i].FindControl("lblBillQty");
                Label lblPrice = (Label)GridView8.Rows[i].FindControl("lblPrice");

                row["RowID"] = lblid.Text;
                row["ConsumableGrId"] = lblConGrId.Text;
                row["ConGroupName"] = lblConGroupName.Text;
                row["ConsumableItemId"] = lblConItemID.Text;
                row["ConItemName"] = lblConItemName.Text;
                row["ActualQty"] = lblActualQty.Text;
                row["BillQty"] = lblBillQty.Text;
                row["Price"] = lblPrice.Text;

                dt.Rows.Add(row);
                row = dt.NewRow();
            }
        }

        DataTable dtcon = thedocvisit.GetConsumableTemplateMapping(Session["CoCode"].ToString().Trim(),NameId);
        if (dtcon.Rows.Count > 0)
        {
            for (int i = 0; i < dtcon.Rows.Count; i++)
            {
                row["ConsumableGrId"] = dtcon.Rows[i]["ConGrId"].ToString();
                row["ConGroupName"] = dtcon.Rows[i]["ConGroupName"].ToString();
                row["ConsumableItemId"] = dtcon.Rows[i]["ConItemID"].ToString();
                row["ConItemName"] = dtcon.Rows[i]["ConItemName"].ToString();
                row["ActualQty"] = dtcon.Rows[i]["ActualQty"].ToString();
                row["BillQty"] = dtcon.Rows[i]["BillQty"].ToString();
                row["Price"] = dtcon.Rows[i]["PriceperUnit"].ToString();

                dt.Rows.Add(row);
                row = dt.NewRow();
            }
        }
        GridView8.DataSource = dt;
        GridView8.DataBind();
        Session["CurrentTableConsumable"] = dt;
    }

    protected void DropDownList37_SelectedIndexChanged(object sender, EventArgs e)
    {
        DataTable serviceMap = thedocvisit.GetServiceTemplateCharge(Session["CoCode"].ToString().Trim(),DropDownList37.SelectedValue);
        FillConsumable(DropDownList37.SelectedValue);
        DataTable dttable = (DataTable)Session["CurrentTable"];
            DataTable dt = new DataTable();
            DataRow row = dt.NewRow();

            dt.Columns.Add("RowID", typeof(string));
            dt.Columns.Add("ServiceId", typeof(string));
            dt.Columns.Add("ServiceCategoryName", typeof(string));
            dt.Columns.Add("Quantity", typeof(string));
            dt.Columns.Add("Price", typeof(string));

            if (dttable != null)
            {
                for (int i = 0; i < dttable.Rows.Count; i++)
                {
                    row["ServiceId"] = dttable.Rows[i]["ServiceId"];
                    row["ServiceCategoryName"] = dttable.Rows[i]["ServiceCategoryName"];
                    row["Quantity"] = "1";
                    row["Price"] = dttable.Rows[i]["Price"];
                    dt.Rows.Add(row);
                    row = dt.NewRow();
                }
            }
            row["ServiceId"] = DropDownList37.SelectedValue;
            row["ServiceCategoryName"] = DropDownList37.SelectedItem.Text;
            row["Quantity"] = "1";
            row["Price"] =serviceMap.Rows[0][0].ToString();
            dt.Rows.Add(row);
            row = dt.NewRow();


            GridView7.DataSource = dt;
            GridView7.DataBind();
            Session["CurrentTable"] = dt;
        //}
            FillMap(DropDownList37.SelectedValue);
    }


    public void FillMap(string id)
    {
        DataTable dt_consumables = new DataTable();
        DataRow row_Consumables = dt_consumables.NewRow();

        dt_consumables.Columns.Add("RowID", typeof(string));
        dt_consumables.Columns.Add("ConsumableGrId", typeof(string));
        dt_consumables.Columns.Add("ConGroupName", typeof(string));
        dt_consumables.Columns.Add("ConsumableItemId", typeof(string));
        dt_consumables.Columns.Add("ConItemName", typeof(string));
        dt_consumables.Columns.Add("ActualQty", typeof(string));
        dt_consumables.Columns.Add("BillQty", typeof(string));
        dt_consumables.Columns.Add("Price", typeof(string));

        DataTable dtfill = theshift.FillMap(id);
        if (dtfill.Rows.Count > 0)
        {
            for (int i = 0; i < dtfill.Rows.Count; i++)
            {
                row_Consumables["ConsumableGrId"] = dtfill.Rows[i]["ConsumableCategoryId"].ToString();
                row_Consumables["ConGroupName"] = dtfill.Rows[i]["ConGroupName"].ToString();
                row_Consumables["ConsumableItemId"] = dtfill.Rows[i]["ConsumableItemId"].ToString();
                row_Consumables["ConItemName"] = dtfill.Rows[i]["ConItemName"].ToString();
                row_Consumables["ActualQty"] = dtfill.Rows[i]["ActualQty"].ToString();
                row_Consumables["BillQty"] = dtfill.Rows[i]["BillQty"].ToString();
                row_Consumables["Price"] = dtfill.Rows[i]["PriceperUnit"].ToString();
                dt_consumables.Rows.Add(row_Consumables);
                row_Consumables = dt_consumables.NewRow();

            }
        }
        dt_consumables1.Merge(dt_consumables);
        GridView11.DataSource = dt_consumables1;
        GridView11.DataBind();

        Session["CurrentTable1"] = dt_consumables1;
    }
    protected void Button5_Click(object sender, EventArgs e)
    {
        DateTime testdate;
        System.Globalization.DateTimeFormatInfo dtf = new DateTimeFormatInfo();
        string servcont = "";
        if(txtdate.Text!="")
            testdate = DateTime.ParseExact(txtdate.Text, "dd/MM/yyyy", dtf);
        else
            testdate = DateTime.Now; 

        bool flag = true;
        if (Button1.Text == "Submit")
        {
            for (int i = 0; i < GridView7.Rows.Count; i++)
            {
                Label lblServiceId = (Label)GridView7.Rows[i].FindControl("lblServiceId");
                Label lblQuantity = (Label)GridView7.Rows[i].FindControl("lblQuantity");
                Label lblPrice = (Label)GridView7.Rows[i].FindControl("lblPrice");

                double Total=0.00;
                if (lblQuantity.Text != "" && lblPrice.Text != "")
                    Total = Convert.ToDouble(lblQuantity.Text) * Convert.ToDouble(lblPrice.Text);

                CheckBox servcontinue = (CheckBox)GridView7.Rows[i].FindControl("servcontinue");
                if (servcontinue.Checked == true)
                {
                    servcont = "1";
                }
                else
                {
                    servcont = "0";
                }

                if (thedocvisit.ServicePrescriptionFunction(1, null, TextBox2.Text, DropDownList2.SelectedValue, TextBox6.Text, DropDownList1.SelectedValue, testdate.ToString("yyyy-MM-dd"), lblServiceId.Text, lblQuantity.Text, lblPrice.Text, Total.ToString(), Session["CoCode"].ToString().Trim(), Session["userName"].ToString().Trim(), servcont) == true)
                {
                    flag = true;
                }
                else
                {
                    flag = false;
                    break;
                }
            }

            for (int i = 0; i < GridView11.Rows.Count; i++)
            {
                double Totalcons = 0.00;
                Label lblConGrId = (Label)GridView11.Rows[i].FindControl("lblConGrId");
                Label lblConGroupName = (Label)GridView11.Rows[i].FindControl("lblConGroupName");
                Label lblConItemID = (Label)GridView11.Rows[i].FindControl("lblConItemID");
                Label lblConItemName = (Label)GridView11.Rows[i].FindControl("lblConItemName");
                Label lblActualQty = (Label)GridView11.Rows[i].FindControl("lblActualQty");
                Label lblBillQty = (Label)GridView11.Rows[i].FindControl("lblBillQty");
                Label lblPrice = (Label)GridView8.Rows[i].FindControl("lblPrice");
                if (lblBillQty.Text != "" && lblPrice.Text != "")
                    Totalcons = Convert.ToDouble(lblBillQty.Text) * Convert.ToDouble(lblPrice.Text);
               /* if (theaddConsumable.InsertConsumable(HiddenField2.Value, TextBox2.Text, lblConGrId.Text, lblConItemID.Text, testdate.ToString("yyyy-MM-dd"), lblActualQty.Text, lblBillQty.Text, null, null, null, null, Session["CoCode"].ToString().Trim(), Session["userName"].ToString().Trim()) == true)
                {
                    theaddConsumable.Update_Service_Status(TextBox2.Text);
                    flag = true;
                }
                else
                {
                    flag = false;
                    break;
                }*/

                if (thedocvisit.ConsumablePrescriptionFunction(1, TextBox2.Text, DropDownList2.SelectedValue, null, TextBox19.Text, testdate.ToString("yyyy-MM-dd"), DropDownList1.SelectedValue, lblConGrId.Text, lblConItemID.Text, lblActualQty.Text, lblBillQty.Text, lblPrice.Text, Totalcons.ToString(), Session["CoCode"].ToString().Trim(), Session["userName"].ToString().Trim()) == true)
                {
                    flag = true;
                }
                else
                {
                    flag = false;
                    break;
                }

            }

            if (flag == true)
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "msg", "alert('Insered Successfully !');", true);
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "msg", "alert('Error in inserted data  !');", true);
            }
        }
        //else
        //{
        //    if (theaddservice.UpdateDocVisit(HiddenField1.Value, lblServiceId.Text, lblServiceCategoryId.Text, testdate.ToString(), lblTotalQty.Text, lblDuration.Text, DropDownList3.SelectedValue, DropDownList4.SelectedValue, DropDownList5.SelectedValue, DropDownList6.SelectedValue) == true)
        //    {
        //        ScriptManager.RegisterStartupScript(this, this.GetType(), "msg", "alert('Updated Successfully !');", true);
        //        Button1.Text = "Submit";

        //    }
        //    else
        //    {
        //        ScriptManager.RegisterStartupScript(this, this.GetType(), "msg", "alert('Error in Updated Data !');", true);
        //    }
        //} 

        Fill();
        Session["CurrentTable"] = null;
        GridView7.DataSource = null;
        GridView7.DataBind();
        GridView11.DataSource = null;
        GridView11.DataBind();

    }

     


    public void FillServiceDtls()
    {
        for (int i = 1; i <= 11; i++)
        {
            d1 = (DropDownList)Page.FindControl("ctl00$ContentPlaceHolder1$ddlserviceCat" + i.ToString());
            d2 = (DropDownList)Page.FindControl("ctl00$ContentPlaceHolder1$ddlService" + i.ToString());
            t1 = (TextBox)Page.FindControl("ctl00$ContentPlaceHolder1$txtTimeperday" + i.ToString());
            t2 = (TextBox)Page.FindControl("ctl00$ContentPlaceHolder1$txtDuration" + i.ToString());
            t3 = (TextBox)Page.FindControl("ctl00$ContentPlaceHolder1$txtTotal" + i.ToString());
            d1.SelectedIndex = 0; d2.SelectedIndex = 0;
            t1.Text = ""; t2.Text = ""; t3.Text = "";
        }
    } 
    public void ResetService()
    {
      //  ddlserviceCat1.SelectedIndex = 0; ddlService1.SelectedIndex = 0; txtTimeperday1.Text = ""; txtTotal1.Text = ""; txtDuration1.Text = "";
    }
    

   
    protected void GridView2_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
    {
        GridView2.EditIndex = -1;
        DataSet ds = thedocvisit.GetDoctorVisitDetails(Session["CoCode"].ToString().Trim(), Session["YearCode"].ToString().Trim(), TextBox2.Text);
        PreviousServiceDetails(ds);
    }
    protected void GridView2_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        string servcont = "";

        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            CheckBox ServiceCont = (CheckBox)e.Row.FindControl("chkcont");
            //CheckBox ServiceContedit = (CheckBox)e.Row.FindControl("cont");
            Label lblcont = (Label)e.Row.FindControl("lblcont");
            servcont = lblcont.Text;
            if (ServiceCont != null)
            {
                if (servcont == "1")
                {
                    ServiceCont.Checked = true;
                    // ServiceContedit.Checked = true;
                }
                else
                {
                    ServiceCont.Checked = false;
                    // ServiceContedit.Checked = false;
                }
            }
        }
        if (e.Row.RowType == DataControlRowType.DataRow && (e.Row.RowState & DataControlRowState.Edit) == DataControlRowState.Edit)
        {
            // Here you will get the Control you need like:
            DropDownList ddlServiceCategory = (DropDownList)e.Row.FindControl("ddlServiceCategory");
            DropDownList ddlService = (DropDownList)e.Row.FindControl("ddlService"); 

         
            
            Label lbldoc_id = (Label)e.Row.FindControl("lbldoc_id");

            Label lblServiceID = (Label)e.Row.FindControl("lblServiceID");

            CheckBox ServiceContedit = (CheckBox)e.Row.FindControl("cont");
            Label lblcont = (Label)e.Row.FindControl("lblcont");
            //Label lblServiceCategoryID = (Label)e.Row.FindControl("lblServiceCategoryID");

            //ddlServiceCategory.Items.Clear();
            //ddlServiceCategory.DataSource = thedocvisit.GetServiceConsumableTemplateeGroup();
            //ddlServiceCategory.DataTextField = "ServiceCategoryName";
            //ddlServiceCategory.DataValueField = "ServiceCategoryID";
            //ddlServiceCategory.DataBind();
            //ddlServiceCategory.Items.Insert(0, new ListItem("--Select--", "0"));
            //ddlServiceCategory.SelectedValue = lblServiceCategoryID.Text;


            ServiceFill("0", ddlService);

            ddlService.SelectedValue = lblServiceID.Text;

            //DropDownList ddlDoctor = (DropDownList)e.Row.FindControl("ddlDoctor");
            //ddlDoctor.Items.Clear();
            //ddlDoctor.DataSource = thedocvisit.DropdownDoctor(DropDownList2.SelectedValue);
            //ddlDoctor.DataTextField = "doc_name";
            //ddlDoctor.DataValueField = "doc_id";
            //ddlDoctor.DataBind();
            //ddlDoctor.Items.Insert(0, new ListItem("--Select--", "0"));
            //ddlDoctor.SelectedValue = lbldoc_id.Text;

            if (servcont == "1")
            {
                ServiceContedit.Checked = true;
            }
            else
            {
                ServiceContedit.Checked = false;
            }
        }
    }

    protected void ddlServiceCategory_SelectedIndexChanged(object sender, EventArgs e)
    {

        DropDownList ddlServiceCategory = (DropDownList)GridView2.Rows[GridView2.EditIndex].FindControl("ddlServiceCategory");
        DropDownList ddlService = (DropDownList)GridView2.Rows[GridView2.EditIndex].FindControl("ddlService");
        Label lblServiceID = (Label)GridView2.Rows[GridView2.EditIndex].FindControl("lblServiceID");
        ServiceFill(ddlServiceCategory.SelectedValue, ddlService);
    }

    protected void ddlServiceCategory_SelectedIndexChanged1(object sender, EventArgs e)
    {

        DropDownList ddlServiceCategory = (DropDownList)GridView7.Rows[GridView7.EditIndex].FindControl("ddlServiceCategory");
        DropDownList ddlService = (DropDownList)GridView7.Rows[GridView7.EditIndex].FindControl("ddlService");
        Label lblServiceID = (Label)GridView7.Rows[GridView7.EditIndex].FindControl("lblServiceID");
        ServiceFill(ddlServiceCategory.SelectedValue, ddlService);
    }


 
    protected void GridView2_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        Label lblid = (Label)GridView2.Rows[e.RowIndex].FindControl("lblid");
        System.Globalization.DateTimeFormatInfo dtf = new DateTimeFormatInfo();
        Label lbllblissuedate = (Label)GridView2.Rows[e.RowIndex].FindControl("lbllblissuedate");
        DateTime testdate = DateTime.ParseExact(lbllblissuedate.Text, "dd/MM/yyyy", dtf);
        string issuedate = testdate.ToString("yyyy-MM-dd");

        if (thedocvisit.ServicePrescriptionFunction(3, lblid.Text, null, null, null, null, null, null, null, null, null, Session["CoCode"].ToString().Trim(), Session["userName"].ToString().Trim(),null) == true)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "msg", "alert('Deleted Successfully !');", true);
        }
        else
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "msg", "alert('Error in Deleted Data !');", true);
        }
        DataSet ds = thedocvisit.GetDoctorVisitDetails(Session["CoCode"].ToString().Trim(), Session["YearCode"].ToString().Trim(), TextBox2.Text);
        PreviousServiceDetails(ds);
    }
    protected void GridView2_RowEditing(object sender, GridViewEditEventArgs e)
    {
        GridView2.EditIndex = e.NewEditIndex;
        DataSet ds = thedocvisit.GetDoctorVisitDetails(Session["CoCode"].ToString().Trim(), Session["YearCode"].ToString().Trim(), TextBox2.Text);
        PreviousServiceDetails(ds);
    }
    protected void GridView2_RowUpdating(object sender, GridViewUpdateEventArgs e)
    {
        double Total = 0.00;
        Label lblid = (Label)GridView2.Rows[e.RowIndex].FindControl("lblid");
        Label lblpid = (Label)GridView2.Rows[e.RowIndex].FindControl("lblpid");
        Label lbldoc_id = (Label)GridView2.Rows[e.RowIndex].FindControl("lbldoc_id");
        

        TextBox txtdate = (TextBox)GridView2.Rows[e.RowIndex].FindControl("txtdate");
        //DropDownList ddlDoctor = (DropDownList)GridView2.Rows[e.RowIndex].FindControl("ddlDoctor");
        DropDownList ddlService = (DropDownList)GridView2.Rows[e.RowIndex].FindControl("ddlService");
        TextBox txtQuantity = (TextBox)GridView2.Rows[e.RowIndex].FindControl("txtQuantity");
        TextBox txtPrice = (TextBox)GridView2.Rows[e.RowIndex].FindControl("txtPrice");
        CheckBox ServCont = (CheckBox)GridView1.Rows[e.RowIndex].FindControl("cont");
        string servcont = "";
        if (ServCont.Checked == true)
        {
            servcont = "1";
        }
        else
        {
            servcont = "0";
        }

        if (txtQuantity.Text != "" && txtPrice.Text != "")
            Total = Convert.ToDouble(txtQuantity.Text) * Convert.ToDouble(txtPrice.Text);

        System.Globalization.DateTimeFormatInfo dtf = new DateTimeFormatInfo();
        DateTime testdate = DateTime.ParseExact(txtdate.Text, "dd/MM/yyyy", dtf);
        if (thedocvisit.ServicePrescriptionFunction(2, lblid.Text, TextBox2.Text, null, lblpid.Text, lbldoc_id.Text, testdate.ToString("yyyy-MM-dd"), ddlService.SelectedValue, txtQuantity.Text, txtPrice.Text, Total.ToString(), Session["CoCode"].ToString().Trim(), Session["userName"].ToString().Trim(), servcont) == true)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "msg", "alert('Updated Successfully !');", true);
        }
        else
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "msg", "alert('Error in updated data !');", true);
        }
        GridView2.EditIndex = -1;
        DataSet ds = thedocvisit.GetDoctorVisitDetails(Session["CoCode"].ToString().Trim(), Session["YearCode"].ToString().Trim(), TextBox2.Text);
        PreviousServiceDetails(ds);
    }
    protected void GridView2_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        GridView2.PageIndex = e.NewPageIndex;
        DataSet ds = thedocvisit.GetDoctorVisitDetails(Session["CoCode"].ToString().Trim(), Session["YearCode"].ToString().Trim(), TextBox2.Text);
        PreviousServiceDetails(ds);
    }






    protected void GridView3_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        GridView3.PageIndex = e.NewPageIndex;
        DataSet ds = thedocvisit.GetDoctorVisitDetails(Session["CoCode"].ToString().Trim(), Session["YearCode"].ToString().Trim(), TextBox2.Text);
        PreviousConsumableDetails(ds);
    }



    protected void ddlConGroupName_SelectedIndexChanged(object sender, EventArgs e)
    {

        DropDownList ddlConGroupName = (DropDownList)GridView3.Rows[GridView3.EditIndex].FindControl("ddlConGroupName");
        DropDownList ddlConItemName = (DropDownList)GridView3.Rows[GridView3.EditIndex].FindControl("ddlConItemName");
        ConsumableItemFill(ddlConGroupName.SelectedValue, ddlConItemName);
    }


    protected void GridView3_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
    {
        GridView3.EditIndex = -1;
        DataSet ds = thedocvisit.GetDoctorVisitDetails(Session["CoCode"].ToString().Trim(), Session["YearCode"].ToString().Trim(), TextBox2.Text);
        PreviousConsumableDetails(ds);
    }
    protected void GridView3_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow && (e.Row.RowState & DataControlRowState.Edit) == DataControlRowState.Edit)
        {
            // Here you will get the Control you need like:
            DropDownList ddlConGroupName = (DropDownList)e.Row.FindControl("ddlConGroupName");
            DropDownList ddlConItemName = (DropDownList)e.Row.FindControl("ddlConItemName");

            DropDownList ddlDoctor = (DropDownList)e.Row.FindControl("ddlDoctor");

            Label lbldoc_id = (Label)e.Row.FindControl("lbldoc_id");

            Label lblConItemID = (Label)e.Row.FindControl("lblConItemID");
            Label lblConGrId = (Label)e.Row.FindControl("lblConGrId");

            ddlConGroupName.Items.Clear();
            ddlConGroupName.DataSource = thedocvisit.GetConsumableGroup(Session["CoCode"].ToString().Trim());
            ddlConGroupName.DataTextField = "ConGroupName";
            ddlConGroupName.DataValueField = "ConGrId";
            ddlConGroupName.DataBind();
            ddlConGroupName.Items.Insert(0, new ListItem("--Select--", "0"));
            ddlConGroupName.SelectedValue = lblConGrId.Text;


            ConsumableItemFill(ddlConGroupName.SelectedValue, ddlConItemName);
            ddlConItemName.SelectedValue = lblConItemID.Text;

            ddlDoctor.Items.Clear();
            ddlDoctor.DataSource = thedocvisit.DropdownDoctor(Session["CoCode"].ToString().Trim(),DropDownList2.SelectedValue);
            ddlDoctor.DataTextField = "doc_name";
            ddlDoctor.DataValueField = "doc_id";
            ddlDoctor.DataBind();
            ddlDoctor.Items.Insert(0, new ListItem("--Select--", "0"));
            ddlDoctor.SelectedValue = lbldoc_id.Text;
        }
    }
    protected void GridView3_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        Label lblid = (Label)GridView3.Rows[e.RowIndex].FindControl("lblid");
        Label lblid1 = (Label)GridView3.Rows[e.RowIndex].FindControl("lblid1");
        if (thedocvisit.ConsumablePrescriptionFunction(3, null, null, lblid.Text, null, null, null, null, null, null, null, null, null, Session["CoCode"].ToString().Trim(), Session["userName"].ToString().Trim()) == true)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "msg", "alert(' Deleted Successfully !');", true);
        }
        else
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "msg", "alert(' Error in Deleted Data !');", true);
        }

        DataSet ds = thedocvisit.GetDoctorVisitDetails(Session["CoCode"].ToString().Trim(), Session["YearCode"].ToString().Trim(), TextBox2.Text);
        PreviousConsumableDetails(ds);
    }
    protected void GridView3_RowEditing(object sender, GridViewEditEventArgs e)
    {
        GridView3.EditIndex = e.NewEditIndex;
        DataSet ds = thedocvisit.GetDoctorVisitDetails(Session["CoCode"].ToString().Trim(), Session["YearCode"].ToString().Trim(), TextBox2.Text);
        PreviousConsumableDetails(ds);
    }
    protected void GridView3_RowUpdating(object sender, GridViewUpdateEventArgs e)
    {
        Label lblid = (Label)GridView3.Rows[e.RowIndex].FindControl("lblid"); 
        Label lblpid = (Label)GridView3.Rows[e.RowIndex].FindControl("lblpid");

        TextBox txtdate = (TextBox)GridView3.Rows[e.RowIndex].FindControl("txtdate");
        DropDownList ddlDoctor = (DropDownList)GridView3.Rows[e.RowIndex].FindControl("ddlDoctor");
        DropDownList ddlConGroupName = (DropDownList)GridView3.Rows[e.RowIndex].FindControl("ddlConGroupName");
        DropDownList ddlConItemName = (DropDownList)GridView3.Rows[e.RowIndex].FindControl("ddlConItemName");
        TextBox txtActualQty = (TextBox)GridView3.Rows[e.RowIndex].FindControl("txtActualQty");
        TextBox txtBillQty = (TextBox)GridView3.Rows[e.RowIndex].FindControl("txtBillQty");

        System.Globalization.DateTimeFormatInfo dtf = new DateTimeFormatInfo();
        DateTime testdate = DateTime.ParseExact(txtdate.Text, "dd/MM/yyyy", dtf);
        if (thedocvisit.ConsumablePrescriptionFunction(2, TextBox2.Text, null, lblid.Text, lblpid.Text, testdate.ToString(), ddlDoctor.SelectedValue, ddlConGroupName.SelectedValue, ddlConItemName.SelectedValue, txtActualQty.Text, txtBillQty.Text, null, null, Session["CoCode"].ToString().Trim(), Session["userName"].ToString().Trim()) == true)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "msg", "alert(' Updated Successfully !');", true);
        }
        else
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "msg", "alert(' Error in Updated Data !');", true);
        } 
        GridView3.EditIndex = -1;
        DataSet ds = thedocvisit.GetDoctorVisitDetails(Session["CoCode"].ToString().Trim(), Session["YearCode"].ToString().Trim(), TextBox2.Text);
        PreviousConsumableDetails(ds);
    }


    protected void GridView4_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        GridView4.PageIndex = e.NewPageIndex;
        GridFillBody();
    }
 
    protected void GridView4_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        Label lblid = (Label)GridView4.Rows[e.RowIndex].FindControl("lblid");
        if (thedocvisit.DeleteClinicalFinding(lblid.Text, Session["CoCode"].ToString().Trim()) == true)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "msg", "alert('Deleted Successfully !');", true);
        }
        else
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "msg", "alert('Error in Deleted Data !');", true);
        } 
        GridFillBody();
    }

    protected void GridView4_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "Select")
        {
            TextBox136.Enabled = false; TextBox23.Enabled = false;
            int index = Convert.ToInt32(e.CommandArgument);
            lblError.Text = "";
            HiddenField3.Value = "";
            Label lblid = (Label)GridView4.Rows[index].FindControl("lblid");
            HiddenField1.Value = lblid.Text;

            Label lbldate = (Label)GridView4.Rows[index].FindControl("lbldate");
            TextBox136.Text = lbldate.Text;

            Label lblComplainName = (Label)GridView4.Rows[index].FindControl("lblComplainName");
            TextBox21.Text = lblComplainName.Text;

            if (lblComplainName.Text != "")
            {
                string[] ComplainId = lblComplainName.Text.Split(',');

                for (int i = 0; i < ComplainId.Length; i++)
                {
                    DataTable dt = thedocvisit.GGetComplainId(ComplainId[i]);
                    if (HiddenField3.Value == "")
                    {
                        HiddenField3.Value = dt.Rows[0]["RowId"].ToString();
                    }
                    else
                    {
                        HiddenField3.Value = HiddenField3.Value + "," + dt.Rows[0]["RowId"].ToString();
                    }
                }
            }

            Label lblTime = (Label)GridView4.Rows[index].FindControl("lblTime");
            TextBox23.Text = lblTime.Text;

            Label lblBP = (Label)GridView4.Rows[index].FindControl("lblBP");
            if (lblBP.Text != "")
            {
                string[] bpsplit = lblBP.Text.Split('/');
                if (bpsplit.Length > 1)
                {
                    TextBox139.Text = bpsplit[0];
                    TextBox25.Text = bpsplit[1];
                }
            }

            Label lblPulse = (Label)GridView4.Rows[index].FindControl("lblPulse");
            TextBox138.Text = lblPulse.Text;

            Label lblChest = (Label)GridView4.Rows[index].FindControl("lblChest");
            TextBox142.Text = lblChest.Text;

            Label lblpa = (Label)GridView4.Rows[index].FindControl("lblpa");
            TextBox143.Text = lblpa.Text;

            Label lblpv = (Label)GridView4.Rows[index].FindControl("lblpv");
            TextBox144.Text = lblpv.Text;

            Label lblothers = (Label)GridView4.Rows[index].FindControl("lblothers");
            TextBox146.Text = lblothers.Text; 
            Button13.Text = "Update";
            LinkButton2.Text = "Update Complain";
        }
    }

 
    protected void GridView6_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "Select")
        {

            TextBox9.Enabled = false; TextBox24.Enabled = false;
            int index = Convert.ToInt32(e.CommandArgument);
            lblError.Text = "";
            HiddenField3.Value = "";

            Label lblid = (Label)GridView6.Rows[index].FindControl("lblid");
            HiddenField1.Value = lblid.Text;

            Label lbldate = (Label)GridView6.Rows[index].FindControl("lbldate");
            TextBox9.Text = lbldate.Text;

            Label lblComplainName = (Label)GridView6.Rows[index].FindControl("lblComplainName");
            TextBox22.Text = lblComplainName.Text;



            if (lblComplainName.Text != "")
            {
                string[] ComplainId = lblComplainName.Text.Split(',');

                for (int i = 0; i < ComplainId.Length; i++)
                {
                    DataTable dt = thedocvisit.GGetComplainId(ComplainId[i]);
                    if (HiddenField3.Value == "")
                    {
                        HiddenField3.Value = dt.Rows[0]["RowId"].ToString();
                    }
                    else
                    {
                        HiddenField3.Value = HiddenField3.Value + "," + dt.Rows[0]["RowId"].ToString();
                    }
                }
            }

            Label lblBP = (Label)GridView6.Rows[index].FindControl("lblBP");

            if (lblBP.Text != "")
            {
                string[] bpsplit = lblBP.Text.Split('/');
                if (bpsplit.Length > 1)
                {
                    TextBox10.Text = bpsplit[0];
                    TextBox27.Text = bpsplit[1];
                }
            }

            Label lblTime = (Label)GridView6.Rows[index].FindControl("lblTime");
            TextBox24.Text = lblTime.Text;

            Label lblPulse = (Label)GridView6.Rows[index].FindControl("lblPulse");
            TextBox11.Text = lblPulse.Text;

            Label lblTemp = (Label)GridView6.Rows[index].FindControl("lblTemp");
            TextBox12.Text = lblTemp.Text;

            Label lblSPO2 = (Label)GridView6.Rows[index].FindControl("lblSPO2");
            TextBox13.Text = lblSPO2.Text;

            Label lblUrin = (Label)GridView6.Rows[index].FindControl("lblUrin");
            TextBox14.Text = lblUrin.Text;

            Label lblSunction = (Label)GridView6.Rows[index].FindControl("lblSunction");
            TextBox15.Text = lblSunction.Text;

            Label lblDoppler = (Label)GridView6.Rows[index].FindControl("lblDoppler");
            TextBox16.Text = lblDoppler.Text;

            Label lblBleeding = (Label)GridView6.Rows[index].FindControl("lblBleeding");
            TextBox17.Text = lblBleeding.Text;

            Label lblOthers = (Label)GridView6.Rows[index].FindControl("lblOthers");
            TextBox18.Text = lblOthers.Text;
            Button9.Text = "Update";
            LinkButton3.Text = "Update Complain";
        }
    }
    protected void GridView6_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {

    }

    protected void GridView5_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        GridView5.PageIndex = e.NewPageIndex;
        DataSet ds = thedocvisit.GetDoctorVisitDetails(Session["CoCode"].ToString().Trim(), Session["YearCode"].ToString().Trim(), TextBox2.Text);
        PreviousPrescriptionDetails(ds);
    }
    protected void GridView5_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
    {
        GridView5.EditIndex = -1;
        DataSet ds = thedocvisit.GetDoctorVisitDetails(Session["CoCode"].ToString().Trim(), Session["YearCode"].ToString().Trim(), TextBox2.Text);
        PreviousPrescriptionDetails(ds);
    }
    protected void GridView5_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow && (e.Row.RowState & DataControlRowState.Edit) == DataControlRowState.Edit)
        {
            // Here you will get the Control you need like:
            DropDownList ddl = (DropDownList)e.Row.FindControl("ddlgroup");
            DropDownList ddl1 = (DropDownList)e.Row.FindControl("ddlsub");
            DropDownList ddl2 = (DropDownList)e.Row.FindControl("ddlmed");
            DropDownList ddlDoctor = (DropDownList)e.Row.FindControl("ddlDoctor");
         
            Label lbldoc_id = (Label)e.Row.FindControl("lbldoc_id");

            Label lblMedicineGroupID = (Label)e.Row.FindControl("lblMedicineGroupID");
            Label lblMedicineSubGroupID = (Label)e.Row.FindControl("lblMedicineSubGroupID");
            Label lblMedicineID = (Label)e.Row.FindControl("lblMedicineID");

            DataTable dt = thepatientdetails.DropdownGroup(Session["CoCode"].ToString().Trim());
            ddl.DataSource = dt;
            ddl.DataTextField = "MedicineGroupName";
            ddl.DataValueField = "MedicineGroupID";
            ddl.DataBind();
            ddl.Items.Insert(0, new ListItem("--Select--", "0"));
            ddl.SelectedValue = lblMedicineGroupID.Text;

            SubGroupFill(lblMedicineGroupID.Text, ddl1);
            ddl1.SelectedValue = lblMedicineSubGroupID.Text;

            MedicineFill(lblMedicineSubGroupID.Text, ddl2);    
            ddl2.SelectedValue = lblMedicineID.Text;


            ddlDoctor.Items.Clear();
            ddlDoctor.DataSource = thedocvisit.DropdownDoctor(Session["CoCode"].ToString().Trim(),DropDownList2.SelectedValue);
            ddlDoctor.DataTextField = "doc_name";
            ddlDoctor.DataValueField = "doc_id";
            ddlDoctor.DataBind();
            ddlDoctor.Items.Insert(0, new ListItem("--Select--", "0"));
            ddlDoctor.SelectedValue = lbldoc_id.Text;
        }
    }
    protected void GridView5_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        Label lblid = (Label)GridView5.Rows[e.RowIndex].FindControl("lblid");
        if (thedocvisit.DeletePrescriptionMap(Session["CoCode"].ToString().Trim(),lblid.Text) == true)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "msg", "alert('Updated Successfully !');", true);
        }
        else
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "msg", "alert('Updated Successfully !');", true) ;
        }

        DataSet ds = thedocvisit.GetDoctorVisitDetails(Session["CoCode"].ToString().Trim(), Session["YearCode"].ToString().Trim(), TextBox2.Text);
        PreviousPrescriptionDetails(ds);
    }
    protected void GridView5_RowEditing(object sender, GridViewEditEventArgs e)
    {
        GridView5.EditIndex = e.NewEditIndex;

        DataSet ds = thedocvisit.GetDoctorVisitDetails(Session["CoCode"].ToString().Trim(), Session["YearCode"].ToString().Trim(), TextBox2.Text);
        PreviousPrescriptionDetails(ds);
    }
    protected void GridView5_RowUpdating(object sender, GridViewUpdateEventArgs e)
    {
        Label lblid = (Label)GridView5.Rows[e.RowIndex].FindControl("lblid");
        Label lblpid = (Label)GridView5.Rows[e.RowIndex].FindControl("lblpid");

        TextBox txtdate = (TextBox)GridView5.Rows[e.RowIndex].FindControl("txtdate");
        DropDownList ddlDoctor = (DropDownList)GridView5.Rows[e.RowIndex].FindControl("ddlDoctor");
        DropDownList ddlgroup = (DropDownList)GridView5.Rows[e.RowIndex].FindControl("ddlgroup");
        DropDownList ddlsub = (DropDownList)GridView5.Rows[e.RowIndex].FindControl("ddlsub");
        DropDownList ddlmed = (DropDownList)GridView5.Rows[e.RowIndex].FindControl("ddlmed");
        TextBox txtActQty = (TextBox)GridView5.Rows[e.RowIndex].FindControl("txtActQty");
        TextBox txtBillQty = (TextBox)GridView5.Rows[e.RowIndex].FindControl("txtBillQty");
        TextBox txtDuration = (TextBox)GridView5.Rows[e.RowIndex].FindControl("txtDuration");
     

        System.Globalization.DateTimeFormatInfo dtf = new DateTimeFormatInfo();
        DateTime testdate = DateTime.ParseExact(txtdate.Text, "dd/MM/yyyy", dtf);
        DataTable dtbatchno = thedocvisit.DropdownBatchNo(Session["CoCode"].ToString().Trim(), Session["YearCode"].ToString().Trim(), ddlmed.SelectedValue);
        string batchno = "";
        if (dtbatchno.Rows.Count > 0)
            batchno = dtbatchno.Rows[0][0].ToString();
        else
            batchno = "";
        if (thedocvisit.UpdatePrescriptionMap(lblid.Text, lblpid.Text, testdate.ToString("yyyy-MM-dd"), ddlDoctor.SelectedValue, ddlgroup.SelectedValue, ddlsub.SelectedValue, ddlmed.SelectedValue, txtActQty.Text, txtBillQty.Text, batchno, txtDuration.Text, Session["CoCode"].ToString().Trim(), Session["userName"].ToString().Trim()) == true)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "msg", "alert('Updated Successfully !');", true);
        }
        else
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "msg", "alert('Error in Updated Data !');", true);
        }
        GridView5.EditIndex = -1;

        DataSet ds = thedocvisit.GetDoctorVisitDetails(Session["CoCode"].ToString().Trim(), Session["YearCode"].ToString().Trim(), TextBox2.Text);
        PreviousPrescriptionDetails(ds);
    }

    protected void ddlgroup_SelectedIndexChanged(object sender, EventArgs e)
    {

        DropDownList ddl1 = (DropDownList)GridView5.Rows[GridView5.EditIndex].FindControl("ddlgroup");
        DropDownList ddl2 = (DropDownList)GridView5.Rows[GridView5.EditIndex].FindControl("ddlsub");

        ddl2.DataSource = thepatientdetails.DropdownSubGroup(Session["CoCode"].ToString().Trim(),ddl1.SelectedValue);
        ddl2.DataTextField = "SubGrName";
        ddl2.DataValueField = "ID";
        ddl2.DataBind();
        ddl2.Items.Insert(0, new ListItem("--Select--", "0"));
    }
    
    protected void ddlsub_SelectedIndexChanged(object sender, EventArgs e)
    {
        DropDownList ddl1 = (DropDownList)GridView5.Rows[GridView5.EditIndex].FindControl("ddlgroup");
        DropDownList ddl2 = (DropDownList)GridView5.Rows[GridView5.EditIndex].FindControl("ddlsub");
        DropDownList ddl3 = (DropDownList)GridView5.Rows[GridView5.EditIndex].FindControl("ddlmed");

        ddl3.DataSource = thepatientdetails.DropdownMedicine(Session["CoCode"].ToString().Trim(),ddl2.SelectedValue);
        ddl3.DataTextField = "MedicineName";
        ddl3.DataValueField = "MedicineID";
        ddl3.DataBind();
        ddl3.Items.Insert(0, new ListItem("--Select--", "0"));
    }
    
    protected void ddlmed_SelectedIndexChanged(object sender, EventArgs e)
    {
        DropDownList ddl = (DropDownList)GridView5.Rows[GridView5.EditIndex].FindControl("ddlmed");
        TextBox t1 = (TextBox)GridView5.Rows[GridView5.EditIndex].FindControl("txtdose"); 
        DataTable dt = thepatientdetails.GetDose(Session["CoCode"].ToString().Trim(),ddl.SelectedValue);
    }

    public void Exdate(DropDownList batchno,TextBox exdate)
    {
        DataTable dt = thedocvisit.ExpiryDateFill(Session["CoCode"].ToString().Trim(),batchno.SelectedValue);
        if(dt.Rows.Count>0)
        exdate.Text = dt.Rows[0][0].ToString();
    }

    protected void ddlBatchNo1_SelectedIndexChanged(object sender, EventArgs e)
    {
        DataTable dt=thedocvisit.ExpiryDateFill(Session["CoCode"].ToString().Trim(),ddlBatchNo1.SelectedValue);
        txtExpiryDate1.Text = dt.Rows[0][0].ToString();
    }
    protected void ddlBatchNo2_SelectedIndexChanged(object sender, EventArgs e)
    {
        DataTable dt = thedocvisit.ExpiryDateFill(Session["CoCode"].ToString().Trim(),ddlBatchNo2.SelectedValue);
        txtExpiryDate2.Text = dt.Rows[0][0].ToString();
    }
    protected void ddlBatchNo3_SelectedIndexChanged(object sender, EventArgs e)
    {
        DataTable dt = thedocvisit.ExpiryDateFill(Session["CoCode"].ToString().Trim(),ddlBatchNo3.SelectedValue);
        txtExpiryDate3.Text = dt.Rows[0][0].ToString();
    }
    protected void ddlBatchNo4_SelectedIndexChanged(object sender, EventArgs e)
    {
        DataTable dt = thedocvisit.ExpiryDateFill(Session["CoCode"].ToString().Trim(),ddlBatchNo4.SelectedValue);
        txtExpiryDate4.Text = dt.Rows[0][0].ToString();
    }
    protected void ddlBatchNo5_SelectedIndexChanged(object sender, EventArgs e)
    {
        DataTable dt = thedocvisit.ExpiryDateFill(Session["CoCode"].ToString().Trim(),ddlBatchNo5.SelectedValue);
        txtExpiryDate5.Text = dt.Rows[0][0].ToString();
    }
    protected void ddlBatchNo6_SelectedIndexChanged(object sender, EventArgs e)
    {
        DataTable dt = thedocvisit.ExpiryDateFill(Session["CoCode"].ToString().Trim(),ddlBatchNo6.SelectedValue);
        txtExpiryDate6.Text = dt.Rows[0][0].ToString();
    }
    protected void ddlBatchNo7_SelectedIndexChanged(object sender, EventArgs e)
    {
        DataTable dt = thedocvisit.ExpiryDateFill(Session["CoCode"].ToString().Trim(),ddlBatchNo7.SelectedValue);
        txtExpiryDate7.Text = dt.Rows[0][0].ToString();
    }
    protected void ddlBatchNo8_SelectedIndexChanged(object sender, EventArgs e)
    {
        DataTable dt = thedocvisit.ExpiryDateFill(Session["CoCode"].ToString().Trim(),ddlBatchNo8.SelectedValue);
        txtExpiryDate8.Text = dt.Rows[0][0].ToString();
    }
    protected void ddlBatchNo9_SelectedIndexChanged(object sender, EventArgs e)
    {
        DataTable dt = thedocvisit.ExpiryDateFill(Session["CoCode"].ToString().Trim(),ddlBatchNo9.SelectedValue);
        txtExpiryDate9.Text = dt.Rows[0][0].ToString();
    }
    protected void ddlBatchNo10_SelectedIndexChanged(object sender, EventArgs e)
    {
        DataTable dt = thedocvisit.ExpiryDateFill(Session["CoCode"].ToString().Trim(),ddlBatchNo10.SelectedValue);
        txtExpiryDate10.Text = dt.Rows[0][0].ToString();
    }

    protected void Button9_Click(object sender, EventArgs e)
    {
        string  testdate;
        string bp = TextBox10.Text + "/" + TextBox27.Text;
        System.Globalization.DateTimeFormatInfo dtf = new DateTimeFormatInfo();
        if (TextBox9.Text != "")
            testdate = DateTime.ParseExact(TextBox9.Text, "dd/MM/yyyy", dtf).ToString();
        else
            testdate = DateTime.Now.ToString("MM/dd/yyyy");

        if (Button9.Text == "Submit")
        {
            if (thedocvisit.BHT_Insert_Update(1, TextBox24.Text, null, TextBox2.Text, testdate.ToString(), TextBox11.Text, bp, TextBox12.Text, TextBox13.Text, TextBox14.Text, TextBox18.Text, TextBox15.Text, TextBox16.Text, TextBox17.Text, Session["CoCode"].ToString().Trim(), Session["userName"].ToString().Trim()) == true)
            {
                string[] ComplainId = HiddenField3.Value.Split(',');
                if (ComplainId.Length > 0)
                {
                    for (int i = 0; i < ComplainId.Length; i++)
                    {
                        thedocvisit.ComplainMap_Insert_Delete(1, TextBox2.Text, testdate.ToString(), TextBox24.Text, ComplainId[i], Session["CoCode"].ToString().Trim(), Session["userName"].ToString().Trim());
                    }
                }

                ScriptManager.RegisterStartupScript(this, this.GetType(), "msg", "alert('Inserted Successfully !');", true);
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "msg", "alert('Error in Inserted Data !');", true);
            }
        }
        else
        {
            if (thedocvisit.BHT_Insert_Update(2, TextBox24.Text, HiddenField1.Value, TextBox2.Text, testdate.ToString(), TextBox11.Text, bp, TextBox12.Text, TextBox13.Text, TextBox14.Text, TextBox18.Text, TextBox15.Text, TextBox16.Text, TextBox17.Text, Session["CoCode"].ToString().Trim(), Session["userName"].ToString().Trim()) == true)
            {
                thedocvisit.ComplainMap_Insert_Delete(2, TextBox2.Text, testdate.ToString(), TextBox24.Text, null, Session["CoCode"].ToString().Trim(), Session["userName"].ToString().Trim());

                string[] ComplainId = HiddenField3.Value.Split(',');
                if (ComplainId.Length > 0)
                {
                    for (int i = 0; i < ComplainId.Length; i++)
                    {
                        thedocvisit.ComplainMap_Insert_Delete(1, TextBox2.Text, testdate.ToString(), TextBox24.Text, ComplainId[i], Session["CoCode"].ToString().Trim(), Session["userName"].ToString().Trim());
                    }
                }

                ScriptManager.RegisterStartupScript(this, this.GetType(), "msg", "alert('Updated Successfully !');", true);
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "msg", "alert('Error in Updated Data !');", true);
            }
        }

        TextBox9.Enabled = true; TextBox24.Enabled = true;
        GridFillBody();
        ResetBody();
    }

    protected void Button10_Click(object sender, EventArgs e)
    {
        if (thedocvisit.InsertTreatementNote(Session["CoCode"].ToString().Trim(), Session["YearCode"].ToString().Trim(), TextBox2.Text, TextBox28.Text.Trim(), ddlvisiteddoc.SelectedValue, Session["userName"].ToString().Trim()) == true)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "msg", "alert('Inserted  Successfully !');", true); 
        }
        else
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "msg", "alert('Error in Inserted Data !');", true);
        } 
        Button10.Enabled = false; 
    }
    protected void GridView7_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        GridView7.PageIndex = e.NewPageIndex;
        DataTable dt = (DataTable)Session["CurrentTable"];
        GridView7.DataSource = dt;
        GridView7.DataBind();
    }
    protected void GridView7_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
    {
        GridView7.EditIndex = -1;
        DataTable dt = (DataTable)Session["CurrentTable"];
        GridView7.DataSource = dt;
        GridView7.DataBind();
    }

    protected void GridView7_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        if (Session["CurrentTable"] != null)
        {
            DataTable CurrentTable = (DataTable)Session["CurrentTable"];
            DataRow drCurrentRow = null;
            int rowIndex = Convert.ToInt32(e.RowIndex);
            if (CurrentTable.Rows.Count > 1)
            {
                CurrentTable.Rows.Remove(CurrentTable.Rows[rowIndex]);
                drCurrentRow = CurrentTable.NewRow();
                Session["CurrentTable"] = CurrentTable;
                GridView7.DataSource = CurrentTable;
                GridView7.DataBind();

                for (int i = 0; i < GridView7.Rows.Count - 1; i++)
                {
                    GridView7.Rows[i].Cells[0].Text = Convert.ToString(i + 1);
                }
            }
        } 
    }

    protected void GridView7_RowEditing(object sender, GridViewEditEventArgs e)
    {
        GridView7.EditIndex = e.NewEditIndex;
        DataTable dt = (DataTable)Session["CurrentTable"];
        GridView7.DataSource = dt;
        GridView7.DataBind();
    }

    protected void GridView7_RowUpdating(object sender, GridViewUpdateEventArgs e)
    {
        DataTable dt = new DataTable();
        DataRow row = dt.NewRow();

        dt.Columns.Add("RowID", typeof(string));
        dt.Columns.Add("ServiceId", typeof(string));
        dt.Columns.Add("ServiceCategoryName", typeof(string));
        dt.Columns.Add("Quantity", typeof(string));
        dt.Columns.Add("Price", typeof(string));


        if (GridView7.Rows.Count > 0)
        {
            for (int i = 0; i < GridView7.Rows.Count; i++)
            {
                Label lblid = (Label)GridView7.Rows[i].FindControl("lblid");
                Label lblServiceId = (Label)GridView7.Rows[i].FindControl("lblServiceId");
                Label lblServiceName = (Label)GridView7.Rows[i].FindControl("lblServiceName");
                Label lblQuantity = (Label)GridView7.Rows[i].FindControl("lblQuantity");
                Label lblPrice = (Label)GridView7.Rows[i].FindControl("lblPrice");
                Label EditSerial = (Label)GridView7.Rows[e.RowIndex].FindControl("lblid");

                TextBox txtQuantity = (TextBox)GridView7.Rows[e.RowIndex].FindControl("txtQuantity");
                TextBox txtPrice = (TextBox)GridView7.Rows[e.RowIndex].FindControl("txtPrice");


                row["ServiceId"] = lblServiceId.Text;
                row["ServiceCategoryName"] = lblServiceName.Text; 

                if (lblid.Text == EditSerial.Text)
                {

                    row["RowID"] = EditSerial.Text;
                    row["Quantity"] = txtQuantity.Text;
                    row["Price"] = txtPrice.Text; 
                }
                else
                {
                    row["RowID"] = lblid.Text;
                    row["Quantity"] = lblQuantity.Text;
                    row["Price"] = lblPrice.Text; 
                }
                dt.Rows.Add(row);
                row = dt.NewRow();
            }
        }

        Session["CurrentTable"] = dt;

        GridView7.EditIndex = -1; 
        GridView7.DataSource = dt;
        GridView7.DataBind();
    }
    protected void GridView7_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            Label lblid = (Label)e.Row.FindControl("lblid");
            lblid.Text = ((GridView7.PageIndex * GridView7.PageSize) + e.Row.RowIndex + 1).ToString();
            CheckBox chkcons = (CheckBox)e.Row.FindControl("chkCons");
            chkcons.Checked = true;
        }
        if (e.Row.RowType == DataControlRowType.DataRow && (e.Row.RowState & DataControlRowState.Edit) == DataControlRowState.Edit)
        {
            // Here you will get the Control you need like:
            //DropDownList ddlServiceCategory = (DropDownList)e.Row.FindControl("ddlServiceCategory");
            //DropDownList ddlService = (DropDownList)e.Row.FindControl("ddlService");

            //Label lblServiceID = (Label)e.Row.FindControl("lblServiceID");
            //Label lblServiceCategoryID = (Label)e.Row.FindControl("lblServiceCategoryID");

            //ddlServiceCategory.Items.Clear();
            //ddlServiceCategory.DataSource = thedocvisit.GetServiceConsumableTemplateeGroup();
            //ddlServiceCategory.DataTextField = "CategoryName";
            //ddlServiceCategory.DataValueField = "TemplateCategoryId";
            //ddlServiceCategory.DataBind();
            //ddlServiceCategory.Items.Insert(0, new ListItem("--Select--", "0"));
            //ddlServiceCategory.SelectedValue = lblServiceCategoryID.Text;


            //ServiceFill(ddlServiceCategory.SelectedValue, ddlService);
            //ddlService.SelectedValue = lblServiceID.Text;

        }
    }


    protected void add_remove_cons(object sender, EventArgs e)
    {
        //string servId = "";
        // CheckBox chkcons=(CheckBox)sender;
        // GridViewRow row = (GridViewRow)chkcons.NamingContainer;
        DataTable dt_consumables = new DataTable();
        DataRow row_Consumables = dt_consumables.NewRow();

        dt_consumables.Columns.Add("RowID", typeof(string));
        dt_consumables.Columns.Add("ConsumableGrId", typeof(string));
        dt_consumables.Columns.Add("ConGroupName", typeof(string));
        dt_consumables.Columns.Add("ConsumableItemId", typeof(string));
        dt_consumables.Columns.Add("ConItemName", typeof(string));
        dt_consumables.Columns.Add("ActualQty", typeof(string));
        dt_consumables.Columns.Add("BillQty", typeof(string));
        dt_consumables.Columns.Add("Price", typeof(string));


        for (int i = 0; i < GridView7.Rows.Count; i++)
        {
            Label lblServiceId = (Label)GridView7.Rows[i].FindControl("lblServiceId");
            CheckBox chkcons = (CheckBox)GridView7.Rows[i].FindControl("chkCons");
            string id = lblServiceId.Text;
            if (chkcons.Checked == true)
            {
                DataTable dtfill = theshift.FillMap(id);
                if (dtfill.Rows.Count > 0)
                {
                    for (int j = 0; j < dtfill.Rows.Count; j++)
                    {
                        row_Consumables["ConsumableGrId"] = dtfill.Rows[j]["ConsumableCategoryId"].ToString();
                        row_Consumables["ConGroupName"] = dtfill.Rows[j]["ConGroupName"].ToString();
                        row_Consumables["ConsumableItemId"] = dtfill.Rows[j]["ConsumableItemId"].ToString();
                        row_Consumables["ConItemName"] = dtfill.Rows[j]["ConItemName"].ToString();
                        row_Consumables["ActualQty"] = dtfill.Rows[j]["ActualQty"].ToString();
                        row_Consumables["BillQty"] = dtfill.Rows[j]["BillQty"].ToString();
                        row_Consumables["Price"] = dtfill.Rows[j]["PriceperUnit"].ToString();
                        dt_consumables.Rows.Add(row_Consumables);
                        row_Consumables = dt_consumables.NewRow();

                    }
                }




            }

        }
        GridView8.DataSource = dt_consumables;
        GridView8.DataBind();
        Session["CurrentTable1"] = dt_consumables;


    }

    protected void Button6_Click(object sender, EventArgs e)
    {
        ResetService();
    }
    protected void GridView8_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        GridView8.PageIndex = e.NewPageIndex;
        DataTable dt = (DataTable)Session["CurrentTableConsumable"];
        GridView8.DataSource = dt;
        GridView8.DataBind();
    }
    protected void GridView8_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
    {

        GridView8.EditIndex = -1;
        DataTable dt = (DataTable)Session["CurrentTableConsumable"];
        GridView8.DataSource = dt;
        GridView8.DataBind();
    }
    protected void GridView8_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        if (Session["CurrentTableConsumable"] != null)
        {
            DataTable CurrentTable = (DataTable)Session["CurrentTableConsumable"];
            DataRow drCurrentRow = null;
            int rowIndex = Convert.ToInt32(e.RowIndex);
            if (CurrentTable.Rows.Count > 1)
            {
                CurrentTable.Rows.Remove(CurrentTable.Rows[rowIndex]);
                drCurrentRow = CurrentTable.NewRow();
                Session["CurrentTableConsumable"] = CurrentTable;
                GridView8.DataSource = CurrentTable;
                GridView8.DataBind();

                for (int i = 0; i < GridView8.Rows.Count - 1; i++)
                {
                    GridView8.Rows[i].Cells[0].Text = Convert.ToString(i + 1);
                }
            }
        } 
    }
    protected void GridView8_RowEditing(object sender, GridViewEditEventArgs e)
    {
        GridView8.EditIndex = e.NewEditIndex;
        DataTable dt = (DataTable)Session["CurrentTableConsumable"];
        GridView8.DataSource = dt;
        GridView8.DataBind();
    }
    protected void GridView8_RowUpdating(object sender, GridViewUpdateEventArgs e)
    {

        DataTable dt = new DataTable();
        DataRow row = dt.NewRow();

        dt.Columns.Add("RowID", typeof(string));
        dt.Columns.Add("ConsumableGrId", typeof(string));
        dt.Columns.Add("ConGroupName", typeof(string));
        dt.Columns.Add("ConsumableItemId", typeof(string));
        dt.Columns.Add("ConItemName", typeof(string));
        dt.Columns.Add("ActualQty", typeof(string));
        dt.Columns.Add("BillQty", typeof(string));
        dt.Columns.Add("Price", typeof(string));


        if (GridView8.Rows.Count > 0)
        {
            for (int i = 0; i < GridView8.Rows.Count; i++)
            {
                Label lblid = (Label)GridView8.Rows[i].FindControl("lblid");
                Label lblConGrId = (Label)GridView8.Rows[i].FindControl("lblConGrId");
                Label lblConGroupName = (Label)GridView8.Rows[i].FindControl("lblConGroupName");
                Label lblConItemID = (Label)GridView8.Rows[i].FindControl("lblConItemID");
                Label lblConItemName = (Label)GridView8.Rows[i].FindControl("lblConItemName");
                Label lblActualQty = (Label)GridView8.Rows[i].FindControl("lblActualQty");
                Label lblBillQty = (Label)GridView8.Rows[i].FindControl("lblBillQty");
                Label lblPrice = (Label)GridView8.Rows[i].FindControl("lblPrice");


                Label EditSerial = (Label)GridView8.Rows[e.RowIndex].FindControl("lblid");
                DropDownList ddlConGroupName = (DropDownList)GridView8.Rows[e.RowIndex].FindControl("ddlConGroupName");
                DropDownList ddlConItemNameval = (DropDownList)GridView8.Rows[e.RowIndex].FindControl("ddlConItemName");

                TextBox txtActualQty = (TextBox)GridView8.Rows[e.RowIndex].FindControl("txtActualQty");
                TextBox txtBillQty = (TextBox)GridView8.Rows[e.RowIndex].FindControl("txtBillQty");
                TextBox txtPrice = (TextBox)GridView8.Rows[e.RowIndex].FindControl("txtPrice");

                if (lblid.Text == EditSerial.Text)
                {

                    row["RowID"] = EditSerial.Text;
                    row["ConsumableGrId"] = ddlConGroupName.SelectedValue;
                    row["ConGroupName"] = ddlConGroupName.SelectedItem.Text;
                    row["ConsumableItemId"] = ddlConItemNameval.SelectedValue;
                    row["ConItemName"] = ddlConItemNameval.SelectedItem.Text;
                    row["ActualQty"] = txtActualQty.Text;
                    row["BillQty"] = txtBillQty.Text;
                    row["Price"] = txtPrice.Text;
                }
                else
                {
                    row["RowID"] = EditSerial.Text;
                    row["ConsumableGrId"] = lblConGrId.Text;
                    row["ConGroupName"] = lblConGroupName.Text;
                    row["ConsumableItemId"] = lblConItemID.Text;
                    row["ConItemName"] = lblConItemName.Text;
                    row["ActualQty"] = lblActualQty.Text;
                    row["BillQty"] = lblBillQty.Text;
                    row["Price"] = lblPrice.Text;
                }
                dt.Rows.Add(row);
                row = dt.NewRow();
            }
        }
        Session["CurrentTableConsumable"] = dt; 
        GridView8.EditIndex = -1;

        GridView8.DataSource = dt;
        GridView8.DataBind();
    }
    protected void GridView8_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            Label lblid = (Label)e.Row.FindControl("lblid");
            lblid.Text = ((GridView8.PageIndex * GridView8.PageSize) + e.Row.RowIndex + 1).ToString();
        }

        if (e.Row.RowType == DataControlRowType.DataRow && (e.Row.RowState & DataControlRowState.Edit) == DataControlRowState.Edit)
        {
            // Here you will get the Control you need like:
            DropDownList ddlConGroupName = (DropDownList)e.Row.FindControl("ddlConGroupName");
            DropDownList ddlConItemName = (DropDownList)e.Row.FindControl("ddlConItemName");


            Label lblConItemID = (Label)e.Row.FindControl("lblConItemID");
            Label lblConGrId = (Label)e.Row.FindControl("lblConGrId");

            ddlConGroupName.Items.Clear();
            ddlConGroupName.DataSource = thedocvisit.GetConsumableGroup(Session["CoCode"].ToString().Trim());
            ddlConGroupName.DataTextField = "ConGroupName";
            ddlConGroupName.DataValueField = "ConGrId";
            ddlConGroupName.DataBind();
            ddlConGroupName.Items.Insert(0, new ListItem("--Select--", "0"));
            ddlConGroupName.SelectedValue = lblConGrId.Text;


            ConsumableItemFill(ddlConGroupName.SelectedValue, ddlConItemName);
            ddlConItemName.SelectedValue = lblConItemID.Text;

        }
    }
    protected void Button7_Click1(object sender, EventArgs e)
    {

        DataTable dt = new DataTable();
        DataRow row = dt.NewRow();

        dt.Columns.Add("RowID", typeof(string));
        dt.Columns.Add("ConsumableGrId", typeof(string));
        dt.Columns.Add("ConGroupName", typeof(string));
        dt.Columns.Add("ConsumableItemId", typeof(string));
        dt.Columns.Add("ConItemName", typeof(string));
        dt.Columns.Add("ActualQty", typeof(string));
        dt.Columns.Add("BillQty", typeof(string));
        dt.Columns.Add("Price", typeof(string));

        if (GridView8.Rows.Count > 0)
        {
            for (int i = 0; i < GridView8.Rows.Count; i++)
            {
                Label lblid = (Label)GridView8.Rows[i].FindControl("lblid");
                Label lblConGrId = (Label)GridView8.Rows[i].FindControl("lblConGrId");
                Label lblConGroupName = (Label)GridView8.Rows[i].FindControl("lblConGroupName");
                Label lblConItemID = (Label)GridView8.Rows[i].FindControl("lblConItemID");
                Label lblConItemName = (Label)GridView8.Rows[i].FindControl("lblConItemName");
                Label lblActualQty = (Label)GridView8.Rows[i].FindControl("lblActualQty");
                Label lblBillQty = (Label)GridView8.Rows[i].FindControl("lblBillQty");
                Label lblPrice = (Label)GridView8.Rows[i].FindControl("lblPrice");

                row["RowID"] = lblid.Text;
                row["ConsumableGrId"] = lblConGrId.Text;
                row["ConGroupName"] = lblConGroupName.Text;
                row["ConsumableItemId"] = lblConItemID.Text;
                row["ConItemName"] = lblConItemName.Text;
                row["ActualQty"] = lblActualQty.Text;
                row["BillQty"] = lblBillQty.Text;
                row["Price"] = lblPrice.Text;

                dt.Rows.Add(row);
                row = dt.NewRow();
            }
        }

        row["ConsumableGrId"] = ddlconsumablegr1.SelectedValue;
        row["ConGroupName"] = ddlconsumablegr1.SelectedItem.Text;
        row["ConsumableItemId"] = ddlConsumableItem1.SelectedValue;
        row["ConItemName"] = ddlConsumableItem1.SelectedItem.Text;
        row["ActualQty"] = txtActualQty1.Text;
        row["BillQty"] = txtBillQty1.Text;
        row["Price"] = txtPrice1.Text;

        dt.Rows.Add(row);
        row = dt.NewRow();

        GridView8.DataSource = dt;
        GridView8.DataBind();

        Session["CurrentTableConsumable"] = dt; 
        resetCon();
    }

    protected void Button8_Click(object sender, EventArgs e)
    {
        DateTime testdate;
        System.Globalization.DateTimeFormatInfo dtf = new DateTimeFormatInfo();
        if (txtdate.Text != "")
            testdate = DateTime.ParseExact(txtdate.Text, "dd/MM/yyyy", dtf);
        else
            testdate = DateTime.Now;
      
        bool flag = true;
        double Total = 0.00;
        if (Button1.Text == "Submit")
        {
            for (int i = 0; i < GridView8.Rows.Count; i++)
            {
                Label lblConGrId = (Label)GridView8.Rows[i].FindControl("lblConGrId");
                Label lblConGroupName = (Label)GridView8.Rows[i].FindControl("lblConGroupName");
                Label lblConItemID = (Label)GridView8.Rows[i].FindControl("lblConItemID");
                Label lblConItemName = (Label)GridView8.Rows[i].FindControl("lblConItemName");
                Label lblActualQty = (Label)GridView8.Rows[i].FindControl("lblActualQty");
                Label lblBillQty = (Label)GridView8.Rows[i].FindControl("lblBillQty");
                Label lblPrice = (Label)GridView8.Rows[i].FindControl("lblPrice");
                if (lblBillQty.Text != "" && lblPrice.Text != "")
                    Total = Convert.ToDouble(lblBillQty.Text) * Convert.ToDouble(lblPrice.Text);


                if (thedocvisit.ConsumablePrescriptionFunction(1, TextBox2.Text, DropDownList2.SelectedValue, null, TextBox19.Text, testdate.ToString("yyyy-MM-dd"), DropDownList1.SelectedValue, lblConGrId.Text, lblConItemID.Text, lblActualQty.Text, lblBillQty.Text, lblPrice.Text, Total.ToString(), Session["CoCode"].ToString().Trim(), Session["userName"].ToString().Trim()) == true)
                {
                    flag = true;
                }
                else
                {
                    flag = false;
                    break;
                }
            }

            if (flag == true)
            {
                 ScriptManager.RegisterStartupScript(this, this.GetType(), "msg", "alert('Inserted Successfully !');", true);
            }
            else
                ScriptManager.RegisterStartupScript(this, this.GetType(), "msg", "alert('Error in inserted data !');", true);
        }
        //else
        //{
        //    if (theaddConsumable.UpdateDocVisit(HiddenField1.Value, DropDownList1.SelectedValue, DropDownList2.SelectedValue, testdate.ToString(), TextBox1.Text, TextBox6.Text, DropDownList4.SelectedValue, DropDownList3.SelectedValue, DropDownList5.SelectedValue, DropDownList6.SelectedValue) == true)
        //    {
        //        ScriptManager.RegisterStartupScript(this, this.GetType(), "msg", "alert('Updated Successfully !');", true);
        //        Button1.Text = "Submit";

        //    }
        //    else
        //    {
        //        ScriptManager.RegisterStartupScript(this, this.GetType(), "msg", "alert('Error in Updated Data !');", true);
        //    }
        //}
        Fill();
        ResetConsumable();
        Session["RegNo"] = null;
    }

    protected void Button11_Click(object sender, EventArgs e)
    {
        ResetConsumable();
    }

    protected void ddlConGroupName_SelectedIndexChanged1(object sender, EventArgs e)
    {

        DropDownList ddlConGroupName = (DropDownList)GridView8.Rows[GridView8.EditIndex].FindControl("ddlConGroupName");
        DropDownList ddlConItemName = (DropDownList)GridView8.Rows[GridView8.EditIndex].FindControl("ddlConItemName");
        ConsumableItemFill(ddlConGroupName.SelectedValue, ddlConItemName);
    }

  
    protected void ddlConItemName_SelectedIndexChanged(object sender, EventArgs e)
    {
        //DropDownList ddlConGroupName = (DropDownList)GridView8.Rows[GridView8.EditIndex].FindControl("ddlConItemName");
        //Label lblPrice = (Label)GridView2.Rows[i].FindControl("lblPrice");
        //DataTable dt = thedocvisit.GetConsumableCharge(ddlConGroupName.SelectedValue);
        //if (dt.Rows.Count > 0)
        //    lblPrice.Text = dt.Rows[0][0].ToString();
    }
    

    public void resetCon()
    {
        ddlconsumablegr1.SelectedIndex = 0; ddlConsumableItem1.SelectedIndex = 0; txtActualQty1.Text = ""; txtBillQty1.Text = "";
        txtPrice1.Text = "";
    }

    public void ResetConsumable()
    {
        GridView8.DataSource = null;
        GridView8.DataBind();
    }
    protected void DropDownList1_SelectedIndexChanged(object sender, EventArgs e)
    {
        //Fill();
        GeneratePrescritionId();
        if (DropDownList1.SelectedIndex != 0)
            HiddenField4.Value = "1";
        else
            HiddenField4.Value = "";
    }

    protected void ddlConsumableItem1_SelectedIndexChanged(object sender, EventArgs e)
    {
        DataTable dt = thedocvisit.GetConsumableCharge(Session["CoCode"].ToString().Trim(),ddlConsumableItem1.SelectedValue);
        if (dt.Rows.Count > 0)
            txtPrice1.Text = dt.Rows[0][0].ToString();
    }
    protected void Button12_Click(object sender, EventArgs e)
    {
        TextBox28.Text = "";
        ddlvisiteddoc.SelectedValue = "0";
    }

    public void getBillQty(string value ,TextBox t, string duration,TextBox t2)
    {
        if (value == "0")
        { t.Text = "";t2.Text = ""; }
        else
        {
            DataTable dt = thedocvisit.getBillQty(value, duration);
            t.Text = dt.Rows[0][0].ToString();
            t2.Text = dt.Rows[0][0].ToString();
            //t.Text = (Convert.ToDecimal(dt.Rows[0][0]) * Convert.ToDecimal(duration)).ToString();
        }
    }

    protected void ddlDose1_SelectedIndexChanged(object sender, EventArgs e)
    {
        getBillQty(ddlDose1.SelectedValue, txtBillQty21, ddlDuration21.SelectedValue, txtActualQty21);
    }
    protected void ddlDose2_SelectedIndexChanged(object sender, EventArgs e)
    {
        getBillQty(ddlDose2.SelectedValue, txtBillQty22, ddlDuration22.SelectedValue, txtActualQty22);
    }
    protected void ddlDose3_SelectedIndexChanged(object sender, EventArgs e)
    {
        getBillQty(ddlDose3.SelectedValue, txtBillQty23, ddlDuration23.SelectedValue, txtActualQty23);
    }
    protected void ddlDose4_SelectedIndexChanged(object sender, EventArgs e)
    {
        getBillQty(ddlDose4.SelectedValue, txtBillQty24, ddlDuration24.SelectedValue, txtActualQty24);
    }
    protected void ddlDose5_SelectedIndexChanged(object sender, EventArgs e)
    {
        getBillQty(ddlDose5.SelectedValue, txtBillQty25, ddlDuration25.SelectedValue, txtActualQty25);
    }
    protected void ddlDose6_SelectedIndexChanged(object sender, EventArgs e)
    {
        getBillQty(ddlDose6.SelectedValue, txtBillQty26, ddlDuration26.SelectedValue, txtActualQty26);
    }
    protected void ddlDose7_SelectedIndexChanged(object sender, EventArgs e)
    {
        getBillQty(ddlDose7.SelectedValue, txtBillQty27, ddlDuration27.SelectedValue, txtActualQty27);
    }
    protected void ddlDose8_SelectedIndexChanged(object sender, EventArgs e)
    {
        getBillQty(ddlDose8.SelectedValue, txtBillQty28, ddlDuration28.SelectedValue, txtActualQty28);
    }
    protected void ddlDose9_SelectedIndexChanged(object sender, EventArgs e)
    {
        getBillQty(ddlDose9.SelectedValue, txtBillQty29, ddlDuration29.SelectedValue, txtActualQty29);
    }
    protected void ddlDose10_SelectedIndexChanged(object sender, EventArgs e)
    {
        getBillQty(ddlDose10.SelectedValue, txtBillQty30, ddlDuration30.SelectedValue, txtActualQty30);
    }
    protected void ddlDuration21_SelectedIndexChanged(object sender, EventArgs e)
    {
        getBillQty(ddlDose1.SelectedValue, txtBillQty21, ddlDuration21.SelectedValue, txtActualQty21);
    }
    protected void ddlDuration22_SelectedIndexChanged(object sender, EventArgs e)
    {
        getBillQty(ddlDose2.SelectedValue, txtBillQty22, ddlDuration22.SelectedValue, txtActualQty22);
    }
    protected void ddlDuration23_SelectedIndexChanged(object sender, EventArgs e)
    {
        getBillQty(ddlDose3.SelectedValue, txtBillQty23, ddlDuration23.SelectedValue, txtActualQty23);
    }
    protected void ddlDuration24_SelectedIndexChanged(object sender, EventArgs e)
    {
        getBillQty(ddlDose4.SelectedValue, txtBillQty24, ddlDuration24.SelectedValue, txtActualQty24);
    }
    protected void ddlDuration25_SelectedIndexChanged(object sender, EventArgs e)
    {
        getBillQty(ddlDose5.SelectedValue, txtBillQty25, ddlDuration25.SelectedValue, txtActualQty25);
    }
    protected void ddlDuration26_SelectedIndexChanged(object sender, EventArgs e)
    {
        getBillQty(ddlDose6.SelectedValue, txtBillQty26, ddlDuration26.SelectedValue, txtActualQty26);
    }
    protected void ddlDuration27_SelectedIndexChanged(object sender, EventArgs e)
    {
        getBillQty(ddlDose7.SelectedValue, txtBillQty27, ddlDuration27.SelectedValue, txtActualQty27);
    }
    protected void ddlDuration28_SelectedIndexChanged(object sender, EventArgs e)
    {
        getBillQty(ddlDose8.SelectedValue, txtBillQty28, ddlDuration28.SelectedValue, txtActualQty28);
    }
    protected void ddlDuration29_SelectedIndexChanged(object sender, EventArgs e)
    {
        getBillQty(ddlDose9.SelectedValue, txtBillQty29, ddlDuration29.SelectedValue, txtActualQty29);
    }
    protected void ddlDuration30_SelectedIndexChanged(object sender, EventArgs e)
    {
        getBillQty(ddlDose10.SelectedValue, txtBillQty30, ddlDuration30.SelectedValue, txtActualQty30);
    }
    protected void GridView9_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        GridViewRow row = e.Row;
        string strSort = string.Empty;

        // Make sure we aren't in header/footer rows
        if (row.DataItem == null)
        {
            return;
        }

        //Find Child GridView control
        GridView gv = new GridView();
        gv = (GridView)row.FindControl("GridView10");

        //Check if any additional conditions (Paging, Sorting, Editing, etc) to be applied on child GridView
        //if (gv.UniqueID == gvUniqueID)
        //{
        //    gv.PageIndex = gvNewPageIndex;
        //    gv.EditIndex = gvEditIndex;
        //    //Check if Sorting used
        //    if (gvSortExpr != string.Empty)
        //    {
        //        GetSortDirection();
        //        strSort = " ORDER BY " + string.Format("{0} {1}", gvSortExpr, gvSortDir);
        //    }
        //    //Expand the Child grid
        //    ClientScript.RegisterStartupScript(GetType(), "Expand", "<SCRIPT LANGUAGE='javascript'>expandcollapse('div" + ((DataRowView)e.Row.DataItem)["CustomerID"].ToString() + "','one');</script>");
        //}

        //Prepare the query for Child GridView by passing the Customer ID of the parent row

        Label lblPrescriptionID = (Label)e.Row.FindControl("lblPrescriptionID");
        gv.DataSource = thedocvisit.GridCurrentMedicine(Session["CoCode"].ToString().Trim(),lblPrescriptionID.Text);
        gv.DataBind();
    }
    protected void GridView9_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        //Check if Add button clicked
        if (e.CommandName == "StopPrescription")
        {
            try
            {
                //Get the values stored in the text boxes 
                string lblPrescriptionID = Convert.ToString(e.CommandArgument);  
                //Prepare the Update Command of the DataSource control

                if (thedocvisit.StopMedicneAndPrescription(Session["CoCode"].ToString().Trim(),lblPrescriptionID, "") == true)
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "msg", "alert('Updated Successfully !');", true);
                }

                //Re bind the grid to refresh the data
                Fill();
            }
            catch (Exception ex)
            {
                ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('" + ex.Message.ToString().Replace("'", "") + "');</script>");
            }
        }
    }

    protected void GridView10_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        //Check if Add button clicked
        if (e.CommandName == "StopMedicine")
        {
            try
            {
                //Get the values stored in the text boxes
                 
                string lblMedicineId = Convert.ToString(e.CommandArgument); 

                //Prepare the Insert Command of the DataSource control
                if (thedocvisit.StopMedicneAndPrescription(Session["CoCode"].ToString().Trim(),"", lblMedicineId) == true)
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "msg", "alert('Updated Successfully !');", true);
                }

                //Re bind the grid to refresh the data
                Fill();
            }
            catch (Exception ex)
            {
                ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('" + ex.Message.ToString().Replace("'", "") + "');</script>");
            }
        }
    }
    protected void ddlMedicineGroup1_SelectedIndexChanged(object sender, EventArgs e)
    {
        SubGroupFill(ddlMedicineGroup1.SelectedValue, ddlSubGroup1);
    }
    protected void ddlMedicineGroup2_SelectedIndexChanged(object sender, EventArgs e)
    {
        SubGroupFill(ddlMedicineGroup2.SelectedValue, ddlSubGroup2);
    }
    protected void ddlMedicineGroup3_SelectedIndexChanged(object sender, EventArgs e)
    {
        SubGroupFill(ddlMedicineGroup3.SelectedValue, ddlSubGroup3);
    }
    protected void ddlMedicineGroup4_SelectedIndexChanged(object sender, EventArgs e)
    {
        SubGroupFill(ddlMedicineGroup4.SelectedValue, ddlSubGroup4);
    }
    protected void ddlMedicineGroup5_SelectedIndexChanged(object sender, EventArgs e)
    {
        SubGroupFill(ddlMedicineGroup5.SelectedValue, ddlSubGroup5);
    }
    protected void ddlMedicineGroup6_SelectedIndexChanged(object sender, EventArgs e)
    {
        SubGroupFill(ddlMedicineGroup6.SelectedValue, ddlSubGroup6);
    }
    protected void ddlMedicineGroup7_SelectedIndexChanged(object sender, EventArgs e)
    {
        SubGroupFill(ddlMedicineGroup7.SelectedValue, ddlSubGroup7);
    }
    protected void ddlMedicineGroup8_SelectedIndexChanged(object sender, EventArgs e)
    {
        SubGroupFill(ddlMedicineGroup8.SelectedValue, ddlSubGroup8);
    }
    protected void ddlMedicineGroup9_SelectedIndexChanged(object sender, EventArgs e)
    {
        SubGroupFill(ddlMedicineGroup9.SelectedValue, ddlSubGroup9);
    }
    protected void ddlMedicineGroup10_SelectedIndexChanged(object sender, EventArgs e)
    {
        SubGroupFill(ddlMedicineGroup10.SelectedValue, ddlSubGroup10);
    }

    protected void ddlSubGroup1_SelectedIndexChanged(object sender, EventArgs e)
    {
        MedicineFill(ddlSubGroup1.SelectedValue, ddlMedicine1);
    }
    protected void ddlSubGroup2_SelectedIndexChanged(object sender, EventArgs e)
    {
        MedicineFill(ddlSubGroup2.SelectedValue, ddlMedicine2);
    }
    protected void ddlSubGroup3_SelectedIndexChanged(object sender, EventArgs e)
    {
        MedicineFill(ddlSubGroup3.SelectedValue, ddlMedicine3);
    }
    protected void ddlSubGroup4_SelectedIndexChanged(object sender, EventArgs e)
    {
        MedicineFill(ddlSubGroup4.SelectedValue, ddlMedicine4);
    }
    protected void ddlSubGroup5_SelectedIndexChanged(object sender, EventArgs e)
    {
        MedicineFill(ddlSubGroup5.SelectedValue, ddlMedicine5);
    }
    protected void ddlSubGroup6_SelectedIndexChanged(object sender, EventArgs e)
    {
        MedicineFill(ddlSubGroup6.SelectedValue, ddlMedicine6);
    }
    protected void ddlSubGroup7_SelectedIndexChanged(object sender, EventArgs e)
    {
        MedicineFill(ddlSubGroup7.SelectedValue, ddlMedicine7);
    }
    protected void ddlSubGroup8_SelectedIndexChanged(object sender, EventArgs e)
    {
        MedicineFill(ddlSubGroup8.SelectedValue, ddlMedicine8);
    }
    protected void ddlSubGroup9_SelectedIndexChanged(object sender, EventArgs e)
    {
        MedicineFill(ddlSubGroup9.SelectedValue, ddlMedicine9);
    }
    protected void ddlSubGroup10_SelectedIndexChanged(object sender, EventArgs e)
    {
        MedicineFill(ddlSubGroup10.SelectedValue, ddlMedicine10);
    }

    public void ddlvisiteddoc_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlvisiteddoc.SelectedValue != "0")
        {
            String note = thedocvisit.getDocVisitNote(Session["CoCode"].ToString().Trim(), Session["YearCode"].ToString().Trim(),ddlvisiteddoc.SelectedValue, TextBox2.Text);
            TextBox28.Text = note;
        }
        else
        {
            TextBox28.Text = "";
        }
    }

    protected void GridView11_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        GridView11.PageIndex = e.NewPageIndex;
        DataTable dt = (DataTable)Session["CurrentTable1"];
        GridView11.DataSource = dt;
        GridView11.DataBind();
    }

    protected void GridView11_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
    {
        GridView8.EditIndex = -1;
        DataTable dt = (DataTable)Session["CurrentTable1"];
        GridView11.DataSource = dt;
        GridView11.DataBind();
    }
    protected void GridView11_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {

        if (Session["CurrentTable1"] != null)
        {
            DataTable CurrentTable = (DataTable)Session["CurrentTable1"];
            DataRow drCurrentRow = null;
            int rowIndex = Convert.ToInt32(e.RowIndex);
            if (CurrentTable.Rows.Count > 1)
            {
                CurrentTable.Rows.Remove(CurrentTable.Rows[rowIndex]);
                drCurrentRow = CurrentTable.NewRow();
                Session["CurrentTable1"] = CurrentTable;
                GridView11.DataSource = CurrentTable;
                GridView11.DataBind();

                for (int i = 0; i < GridView8.Rows.Count - 1; i++)
                {
                    GridView8.Rows[i].Cells[0].Text = Convert.ToString(i + 1);
                }
            }
        }
    }

    protected void GridView11_RowEditing(object sender, GridViewEditEventArgs e)
    {
        GridView8.EditIndex = e.NewEditIndex;
        DataTable dt = (DataTable)Session["CurrentTable1"];
        GridView11.DataSource = dt;
        GridView11.DataBind();
    }
    protected void GridView11_RowUpdating(object sender, GridViewUpdateEventArgs e)
    {

        DataTable dt = new DataTable();
        DataRow row = dt.NewRow();

        dt.Columns.Add("RowID", typeof(string));
        dt.Columns.Add("ConsumableGrId", typeof(string));
        dt.Columns.Add("ConGroupName", typeof(string));
        dt.Columns.Add("ConsumableItemId", typeof(string));
        dt.Columns.Add("ConItemName", typeof(string));
        dt.Columns.Add("ActualQty", typeof(string));
        dt.Columns.Add("BillQty", typeof(string));
        dt.Columns.Add("Price", typeof(string));


        if (GridView11.Rows.Count > 0)
        {
            for (int i = 0; i < GridView8.Rows.Count; i++)
            {
                Label lblid = (Label)GridView8.Rows[i].FindControl("lblid");
                Label lblConGrId = (Label)GridView8.Rows[i].FindControl("lblConGrId");
                Label lblConGroupName = (Label)GridView8.Rows[i].FindControl("lblConGroupName");
                Label lblConItemID = (Label)GridView8.Rows[i].FindControl("lblConItemID");
                Label lblConItemName = (Label)GridView8.Rows[i].FindControl("lblConItemName");
                Label lblActualQty = (Label)GridView8.Rows[i].FindControl("lblActualQty");
                Label lblBillQty = (Label)GridView8.Rows[i].FindControl("lblBillQty");
                Label lblPrice = (Label)GridView8.Rows[i].FindControl("lblPrice");


                Label EditSerial = (Label)GridView8.Rows[e.RowIndex].FindControl("lblid");
                DropDownList ddlConGroupName = (DropDownList)GridView8.Rows[e.RowIndex].FindControl("ddlConGroupName");
                DropDownList ddlConItemNameval = (DropDownList)GridView8.Rows[e.RowIndex].FindControl("ddlConItemName");

                TextBox txtActualQty = (TextBox)GridView8.Rows[e.RowIndex].FindControl("txtActualQty");
                TextBox txtBillQty = (TextBox)GridView8.Rows[e.RowIndex].FindControl("txtBillQty");
                TextBox txtPrice = (TextBox)GridView8.Rows[e.RowIndex].FindControl("txtPrice");

                if (lblid.Text == EditSerial.Text)
                {

                    row["RowID"] = EditSerial.Text;
                    row["ConsumableGrId"] = ddlConGroupName.SelectedValue;
                    row["ConGroupName"] = ddlConGroupName.SelectedItem.Text;
                    row["ConsumableItemId"] = ddlConItemNameval.SelectedValue;
                    row["ConItemName"] = ddlConItemNameval.SelectedItem.Text;
                    row["ActualQty"] = txtActualQty.Text;
                    row["BillQty"] = txtBillQty.Text;
                    row["Price"] = txtPrice.Text;
                }
                else
                {
                    row["RowID"] = EditSerial.Text;
                    row["ConsumableGrId"] = lblConGrId.Text;
                    row["ConGroupName"] = lblConGroupName.Text;
                    row["ConsumableItemId"] = lblConItemID.Text;
                    row["ConItemName"] = lblConItemName.Text;
                    row["ActualQty"] = lblActualQty.Text;
                    row["BillQty"] = lblBillQty.Text;
                    row["Price"] = lblPrice.Text;
                }
                dt.Rows.Add(row);
                row = dt.NewRow();
            }
        }
        Session["CurrentTable1"] = dt;
        GridView11.EditIndex = -1;

        GridView11.DataSource = dt;
        GridView11.DataBind();
    }

    protected void GridView11_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            Label lblid = (Label)e.Row.FindControl("lblid");
            lblid.Text = ((GridView8.PageIndex * GridView8.PageSize) + e.Row.RowIndex + 1).ToString();
        }

        if (e.Row.RowType == DataControlRowType.DataRow && (e.Row.RowState & DataControlRowState.Edit) == DataControlRowState.Edit)
        {
            // Here you will get the Control you need like:
            DropDownList ddlConGroupName = (DropDownList)e.Row.FindControl("ddlConGroupName");
            DropDownList ddlConItemName = (DropDownList)e.Row.FindControl("ddlConItemName");


            Label lblConItemID = (Label)e.Row.FindControl("lblConItemID");
            Label lblConGrId = (Label)e.Row.FindControl("lblConGrId");

            ddlConGroupName.Items.Clear();
            ddlConGroupName.DataSource = thedocvisit.GetConsumableGroup(Session["CoCode"].ToString().Trim());
            ddlConGroupName.DataTextField = "ConGroupName";
            ddlConGroupName.DataValueField = "ConGrId";
            ddlConGroupName.DataBind();
            ddlConGroupName.Items.Insert(0, new ListItem("--Select--", "0"));
            ddlConGroupName.SelectedValue = lblConGrId.Text;


            ConsumableItemFill(ddlConGroupName.SelectedValue, ddlConItemName);
            ddlConItemName.SelectedValue = lblConItemID.Text;

        }
    }
}