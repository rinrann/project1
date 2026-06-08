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

public partial class IPD_AddMedicine : System.Web.UI.Page
{
    Main Objmain = new Main(ConfigurationSettings.AppSettings["ConnectionString"].ToString());
    AddMedicine thereagentEntry = new AddMedicine(ConfigurationSettings.AppSettings["ConnectionString"].ToString());
    PatientDoctorVisit thedocvisit = new PatientDoctorVisit(ConfigurationSettings.AppSettings["ConnectionString"].ToString()); 
    protected void Page_Load(object sender, EventArgs e)
    {
        Page.Title = "Add Medicine";
        ScriptManager.RegisterStartupScript(this, this.GetType(), "msg", "GetDatetime();", true);  
        if (Session["userName"] == null)
        {
            Response.Redirect("../LoginPage.aspx");
        }
        if (Objmain.checkaccss(Session["CoCode"].ToString(), Session["UserRoleID"].ToString().Trim(), "ADD MEDICINE", checkAccessType.ViewAction) == false)
        {
            Response.Redirect("../AccessDenied.aspx");
        }

        if (Objmain.checkaccss(Session["CoCode"].ToString(), Session["UserRoleID"].ToString().Trim(), "ADD MEDICINE", checkAccessType.InsertAction) == false)
        {
            Button1.Enabled = false;
        }

        if (!IsPostBack)
        {

            DropDownFill();
            Tab1Func();
            if (Session["RegNo"] != null)
            {
                TextBox23.Text = Session["RegNo"].ToString();
                GridFill();
            }
            Session["RegNo"] = null;
        }


    }

    public void DropDownFill()
    {
        DropDownList d1, d2, d3, d4, d5,d6;
        TextBox t1, t2;
        DataTable dtMedicienGroup = thereagentEntry.DropdownMedGroup(Session["CoCode"].ToString().Trim());
        DataTable dtDose = thereagentEntry.DropdownDose(Session["CoCode"].ToString().Trim());
        DataTable dtduration = thereagentEntry.DropdownDuration(Session["CoCode"].ToString().Trim());
        for (int i = 1; i <=5; i++)
        {
            d1 = (DropDownList)Page.FindControl("ctl00$ContentPlaceHolder1$ddlMedGroup" + i.ToString());
            d1.DataSource = dtMedicienGroup;
            d1.DataTextField = "MedicineGroupName";
            d1.DataValueField = "MedicineGroupID";
            d1.DataBind();
            d1.Items.Insert(0, new ListItem("--Select--", "0"));

            d2 = (DropDownList)Page.FindControl("ctl00$ContentPlaceHolder1$ddlSubGroup" + i.ToString());
            d2.Items.Insert(0, new ListItem("--Select--", "0"));

            d3 = (DropDownList)Page.FindControl("ctl00$ContentPlaceHolder1$ddlMedicine" + i.ToString());
            d3.Items.Insert(0, new ListItem("--Select--", "0"));

            d4 = (DropDownList)Page.FindControl("ctl00$ContentPlaceHolder1$ddlBatchNo" + i.ToString());
            d4.Items.Insert(0, new ListItem("--Select--", "0"));

            d5 = (DropDownList)Page.FindControl("ctl00$ContentPlaceHolder1$ddlDose" + i.ToString());
            d5.DataSource = dtDose;
            d5.DataTextField = "DoseName";
            d5.DataValueField = "ID";
            d5.DataBind();
            d5.Items.Insert(0, new ListItem("--Select--", "0"));

            d6 = (DropDownList)Page.FindControl("ctl00$ContentPlaceHolder1$dlDuration" + i.ToString());
            d6.DataSource = dtduration;
            d6.DataTextField = "DurationName";
            d6.DataValueField = "DurationId";
            d6.DataBind();
            d6.Items.Insert(0, new ListItem("--Select--", "0"));
            d6.SelectedIndex = 1;
            d6.Visible = false;
            for (int j = 21; j <= 25; j++)
            {
                d5 = (DropDownList)Page.FindControl("ctl00$ContentPlaceHolder1$ddlDuration" + j.ToString());
                d5.Items.Clear();
                d5.DataSource = thedocvisit.DropdownDuration(Session["CoCode"].ToString().Trim());
                d5.DataTextField = "DurationName";
                d5.DataValueField = "DurationId";
                d5.DataBind();
                d5.Items.Insert(0, new ListItem("--Select--", "0"));
                d5.Visible = false;
            }
        }



        DropDownList3.DataSource = thereagentEntry.DropdownDoctor(Session["CoCode"].ToString().Trim());
            DropDownList3.DataTextField = "doc_name";
            DropDownList3.DataValueField = "doc_id";
            DropDownList3.DataBind();
            DropDownList3.Items.Insert(0, new ListItem("--Select--", "0"));
    } 
    protected void Button1_Click(object sender, EventArgs e)
    {
        string MedicineId = string.Empty;
        string qty = string.Empty;
        string actQty = string.Empty;
        int insertflag = 0; int j;
        System.Globalization.DateTimeFormatInfo dtf = new DateTimeFormatInfo();        
        if (Button1.Text == "Submit")
        { 
            DateTime testdate = DateTime.ParseExact(TextBox1.Text, "dd/MM/yyyy", dtf);
           

             DropDownList d1, d2, d3, d4, d5, d6;
             TextBox t1, t2, txtactQty;
             for (int i = 1; i <= 5; i++)
             {
                 j = i + 20;
                 d1 = (DropDownList)Page.FindControl("ctl00$ContentPlaceHolder1$ddlMedGroup" + i.ToString());
                 d2 = (DropDownList)Page.FindControl("ctl00$ContentPlaceHolder1$ddlSubGroup" + i.ToString());
                 d3 = (DropDownList)Page.FindControl("ctl00$ContentPlaceHolder1$ddlMedicine" + i.ToString());
                 d4 = (DropDownList)Page.FindControl("ctl00$ContentPlaceHolder1$ddlBatchNo" + i.ToString());
                 d5 = (DropDownList)Page.FindControl("ctl00$ContentPlaceHolder1$ddlDose" + i.ToString());
                 //d6 = (DropDownList)Page.FindControl("ctl00$ContentPlaceHolder1$dlDuration" + i.ToString());
                 d6 = (DropDownList)Page.FindControl("ctl00$ContentPlaceHolder1$ddlDuration" + j.ToString());
                 t1 = (TextBox)Page.FindControl("ctl00$ContentPlaceHolder1$txtExpiryDate" + i.ToString());
                 t2 = (TextBox)Page.FindControl("ctl00$ContentPlaceHolder1$txtBillQty" + i.ToString());
                 txtactQty = (TextBox)Page.FindControl("ctl00$ContentPlaceHolder1$txtActualQty" + j.ToString());

          

                 if (d1.SelectedIndex != 0 && d2.SelectedIndex != 0 && d3.SelectedIndex != 0)
                 {
                     if (MedicineId == "")
                         MedicineId = d3.SelectedValue;
                     else
                         MedicineId = MedicineId + "~" + d3.SelectedValue;

                     if (qty == "")
                         qty = t2.Text;
                     else
                         qty = qty + "~" + t2.Text;

                     //if (MedicineId == "")
                     //    MedicineId = "'" + d3.SelectedValue + "'";
                     //else
                     //    MedicineId = MedicineId + ",'" + d3.SelectedValue + "'";
                     if (actQty == "")
                         actQty = txtactQty.Text;
                     else
                         actQty = actQty + "~" + txtactQty.Text;

                     string extdt = "";
                     if (t1.Text != "")
                     {
                         DateTime Exdatedate = DateTime.ParseExact(t1.Text, "dd/MM/yyyy", dtf);
                         extdt = Exdatedate.ToString("yyyy-MM-dd");
                     }
                     else
                     {
                         extdt = "";
                     }


                     if (thereagentEntry.InsertAddedicine(HiddenField2.Value, TextBox23.Text, d1.SelectedValue, d3.SelectedValue, testdate.ToString("yyyy-MM-dd"), DropDownList3.SelectedValue, d2.SelectedValue, t2.Text, txtactQty.Text, d4.SelectedItem.Text, extdt, d5.SelectedValue.ToString(), d6.SelectedValue.ToString(), TextBox25.Text, Session["CoCode"].ToString().Trim(),Session["YearCode"].ToString().Trim(), Session["userName"].ToString().Trim()) == true)
                     {
                         insertflag++;
                     }
                     else
                     {
                         break;
                     }
                 }
                 else
                 {
                     break;
                 }
              
             }
             if (insertflag > 0)
             {
                 thereagentEntry.Insert_Syring(HiddenField2.Value, TextBox23.Text, MedicineId, testdate, DropDownList3.SelectedValue, qty, Session["CoCode"].ToString().Trim(), Session["userName"].ToString().Trim());
                 Response.Redirect("../IPD/AdmissionPatientList.aspx");
             }
             else
             {
                 ScriptManager.RegisterStartupScript(this, this.GetType(), "msg", "alert('Error in inserted Data !');", true);
             }
            
        }
        else
        {
            DateTime testdate = DateTime.ParseExact(TextBox1.Text, "dd/MM/yyyy", dtf);
            DateTime Exdatedate = DateTime.ParseExact(txtExpiryDate1.Text, "dd/MM/yyyy", dtf);
            //if (thereagentEntry.UpdateAddedicine(HiddenField1.Value, TextBox23.Text, ddlMedGroup1.SelectedValue, ddlMedicine1.SelectedValue, testdate.ToString(), DropDownList3.SelectedValue, ddlSubGroup1.SelectedValue, txtBillQty1.Text, ddlBatchNo1.SelectedValue, Exdatedate.ToString(), ddlDose1.SelectedValue, dlDuration1.SelectedValue) == true)
            if (thereagentEntry.UpdateAddedicine(HiddenField1.Value, TextBox23.Text, ddlMedGroup1.SelectedValue, ddlMedicine1.SelectedValue, testdate.ToString("yyyy-MM-dd"), DropDownList3.SelectedValue, ddlSubGroup1.SelectedValue, txtBillQty1.Text, txtActualQty21.Text, ddlBatchNo1.SelectedValue, Exdatedate.ToString("yyyy-MM-dd"), ddlDose1.SelectedValue, ddlDuration21.SelectedValue, TextBox25.Text,Session["CoCode"].ToString().Trim(),Session["userName"].ToString().Trim()) == true)
                ScriptManager.RegisterStartupScript(this, this.GetType(), "msg", "alert('Updated Successfully !');", true);
            else
                ScriptManager.RegisterStartupScript(this, this.GetType(), "msg", "alert('Error in Updated Data !');", true); 
        }
        GridFill();
        ResetAllFields();
    }

    public void ResetAllFields()
    {
        HiddenField1.Value = "";
        Button1.Text = "Submit";
        DropDownList d1;
        TextBox t1; int j;
        for (int i = 1; i <= 5; i++)
        {
            j = i + 20;
            d1 = (DropDownList)Page.FindControl("ctl00$ContentPlaceHolder1$ddlMedGroup" + i.ToString());
            d1.SelectedIndex = 0;

            d1 = (DropDownList)Page.FindControl("ctl00$ContentPlaceHolder1$ddlSubGroup" + i.ToString());
            d1.SelectedIndex = 0;

            d1 = (DropDownList)Page.FindControl("ctl00$ContentPlaceHolder1$ddlMedicine" + i.ToString());
            d1.SelectedIndex = 0;

            d1 = (DropDownList)Page.FindControl("ctl00$ContentPlaceHolder1$ddlBatchNo" + i.ToString());
            d1.SelectedIndex = 0;

            d1 = (DropDownList)Page.FindControl("ctl00$ContentPlaceHolder1$ddlDose" + i.ToString());
            d1.SelectedIndex = 0;

            d1 = (DropDownList)Page.FindControl("ctl00$ContentPlaceHolder1$dlDuration" + i.ToString());
            d1.SelectedIndex = 0;

            d1 = (DropDownList)Page.FindControl("ctl00$ContentPlaceHolder1$ddlDuration" + j.ToString());
            d1.SelectedIndex = 0;

            t1 = (TextBox)Page.FindControl("ctl00$ContentPlaceHolder1$txtExpiryDate" + i.ToString());
            t1.Text = "";

            t1 = (TextBox)Page.FindControl("ctl00$ContentPlaceHolder1$txtBillQty" + i.ToString());
            t1.Text = "";

            t1 = (TextBox)Page.FindControl("ctl00$ContentPlaceHolder1$txtActualQty" + j.ToString());
            t1.Text = "";
        }
        if (Objmain.checkaccss(Session["CoCode"].ToString(), Session["UserRoleID"].ToString().Trim(), "ADD MEDICINE", checkAccessType.InsertAction) == false)
        {
            Button1.Enabled = false;
        }
    }


 
    
    protected void Button4_Click(object sender, EventArgs e)
    {
        ResetAllFields();
    }
    protected void Button2_Click(object sender, EventArgs e)
    {
        GridFill();
    }

    private void GridFill()
    {
        DataSet ds = thereagentEntry.GetAllPatientMedicine(TextBox23.Text);

         DropDownList3.SelectedValue = ds.Tables[2].Rows[0][0].ToString();

        if (ds.Tables[0].Rows.Count > 0)
        {
            GridView1.DataSource = ds.Tables[0];
            GridView1.DataBind();
            TextBox24.Text = ds.Tables[0].Rows[0]["patient_name"].ToString();
            TextBox25.Text = ds.Tables[0].Rows[0]["BedNoText"].ToString();
            TextBox26.Text = ds.Tables[0].Rows[0]["adate"].ToString();
        }
        else
        {
            if (ds.Tables[1].Rows.Count > 0)
            {
                TextBox24.Text = ds.Tables[1].Rows[0]["patient_name"].ToString();
                TextBox25.Text = ds.Tables[1].Rows[0]["BedNoText"].ToString();
                TextBox26.Text = ds.Tables[1].Rows[0]["adate"].ToString();
            }

        }
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

    protected int SearchText(string Value, DropDownList ddl)
    {
        int i;
        for (i = 0; i < ddl.Items.Count; i++)
        {
            if (ddl.Items[i].Text.Trim() == Value.Trim())
                return i;
        }
        return -1;
    }
    protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "Select")
        {
            lblError.Text = "";     
            int index = Convert.ToInt32(e.CommandArgument);
            lblError.Text = "";
            Label lblid = (Label)GridView1.Rows[index].FindControl("lblid");
            HiddenField1.Value = lblid.Text;

            Label lblRegno = (Label)GridView1.Rows[index].FindControl("lblRegno");
            TextBox23.Text = lblRegno.Text;

            Label lbllname = (Label)GridView1.Rows[index].FindControl("lbllname");
            TextBox24.Text = lbllname.Text;

            Label lblbedno = (Label)GridView1.Rows[index].FindControl("lblbedno");
            TextBox25.Text = lblbedno.Text;

            Label lbladate = (Label)GridView1.Rows[index].FindControl("lbladate");
            TextBox26.Text = lbladate.Text;

            Label lblMedicineGroupName = (Label)GridView1.Rows[index].FindControl("lblMedicineGroupName");
            ddlMedGroup1.SelectedIndex = SearchText(lblMedicineGroupName.Text, ddlMedGroup1);

            DropDownSubGroupFill(ddlMedGroup1.SelectedValue, ddlSubGroup1);
            Label lblMedicineSubGrId = (Label)GridView1.Rows[index].FindControl("lblMedicineSubGrId");
            ddlSubGroup1.SelectedIndex = SearchIndex(lblMedicineSubGrId.Text, ddlSubGroup1);

            DropDownMedicineFill(ddlSubGroup1.SelectedValue, ddlMedicine1);
            Label lblMedicineName = (Label)GridView1.Rows[index].FindControl("lblMedicineName");
            ddlMedicine1.SelectedIndex = SearchText(lblMedicineName.Text, ddlMedicine1);

            Label lblisdate = (Label)GridView1.Rows[index].FindControl("lblisdate");
            TextBox1.Text = lblisdate.Text;

            Label lbladvicedby = (Label)GridView1.Rows[index].FindControl("lbladvicedby");
            DropDownList3.SelectedIndex = SearchIndex(lbladvicedby.Text, DropDownList3);

            Label lblBillQty = (Label)GridView1.Rows[index].FindControl("lblBillQty");
            txtBillQty1.Text = lblBillQty.Text;

            Label lblActualQty = (Label)GridView1.Rows[index].FindControl("lblActualQty");
            txtActualQty21.Text = lblActualQty.Text;

            BatchNoFill(ddlMedicine1.SelectedValue, ddlBatchNo1);
            Label lblBatchNo = (Label)GridView1.Rows[index].FindControl("lblBatchNo");
            ddlBatchNo1.SelectedIndex = SearchText(lblBatchNo.Text, ddlBatchNo1);

            Label lblExpirDate = (Label)GridView1.Rows[index].FindControl("lblExpirDate");
            txtExpiryDate1.Text = lblExpirDate.Text;


            Label lblDose = (Label)GridView1.Rows[index].FindControl("lblDose");
            ddlDose1.SelectedIndex = SearchIndex(lblDose.Text, ddlDose1);

            Label lblDuration = (Label)GridView1.Rows[index].FindControl("lblDuration");
            //dlDuration1.SelectedValue = lblDuration.Text;
            ddlDuration21.SelectedValue = lblDuration.Text;

            Tab1Func();
            Button1.Text = "Update";
            if (Objmain.checkaccss(Session["CoCode"].ToString(), Session["UserRoleID"].ToString().Trim(), "ADD MEDICINE", checkAccessType.UpdateAction) == false)
            {
                Button1.Enabled = false;
            }
            else
            {
                Button1.Enabled = true;
            }
        }
    }
    protected void GridView1_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        GridView1.PageIndex = e.NewPageIndex;
        GridFill();
    }


    public void DropDownSubGroupFill(string value, DropDownList drop2)
    {
        drop2.Items.Clear();
        drop2.DataSource = thereagentEntry.DropDownMedicineSubgroup(Session["CoCode"].ToString().Trim(), value);
        drop2.DataTextField = "SubGrName";
        drop2.DataValueField = "ID";
        drop2.DataBind();
        drop2.Items.Insert(0, new ListItem("--Select--", "0"));
    }
    public void DropDownMedicineFill(string value,DropDownList drop2)
    {
        drop2.Items.Clear();
        drop2.DataSource = thereagentEntry.DropDownMedicine(Session["CoCode"].ToString().Trim(), Session["YearCode"].ToString().Trim(), value, TextBox23.Text);
        drop2.DataTextField = "MedicineName";
        drop2.DataValueField = "MedicineID";
        drop2.DataBind();
        drop2.Items.Insert(0, new ListItem("--Select--", "0"));
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
    public void Tab1Func()
    {
        Tab1.CssClass = "Clicked";
        Tab2.CssClass = "Initial";
        MainView.ActiveViewIndex = 0;
    }


    public void BatchNoFill(string value, DropDownList ddlBatchNo)
    {

        ddlBatchNo.Items.Clear();
        DataTable dt = thedocvisit.DropdownBatchNo(Session["CoCode"].ToString().Trim(), Session["YearCode"].ToString().Trim(), value);
        ddlBatchNo.DataSource = dt;
        ddlBatchNo.DataTextField = "BatchNo";
        ddlBatchNo.DataValueField = "BatchNo";
        ddlBatchNo.DataBind();

        //TextBox2.Text = dt.Rows[0]["DoseName"].ToString();
    }

    protected void ddlMedGroup1_SelectedIndexChanged(object sender, EventArgs e)
    {
        DropDownSubGroupFill(ddlMedGroup1.SelectedValue, ddlSubGroup1);
    } 

    protected void ddlSubGroup1_SelectedIndexChanged(object sender, EventArgs e)
    {
        DropDownMedicineFill(ddlSubGroup1.SelectedValue, ddlMedicine1);
    }

    protected void ddlMedicine1_SelectedIndexChanged(object sender, EventArgs e)
    {
        BatchNoFill(ddlMedicine1.SelectedValue, ddlBatchNo1);
        Exdate(ddlBatchNo1, txtExpiryDate1);
    }

    public void Exdate(DropDownList batchno, TextBox exdate)
    {
        DataTable dt = thedocvisit.ExpiryDateFill(Session["CoCode"].ToString().Trim(), batchno.SelectedValue);
        if(dt.Rows.Count>0)
        exdate.Text = dt.Rows[0][0].ToString();
    }
    protected void ddlBatchNo1_SelectedIndexChanged(object sender, EventArgs e)
    {
        DataTable dt = thedocvisit.ExpiryDateFill(Session["CoCode"].ToString().Trim(), ddlBatchNo1.SelectedValue);
        txtExpiryDate1.Text = dt.Rows[0][0].ToString();
    }
    protected void GridView1_RowDeleting(object sender, GridViewDeleteEventArgs e)
    { 
        Label lblid = (Label)GridView1.Rows[e.RowIndex].FindControl("lblid");
        if (thereagentEntry.DeleteAddmedicine(lblid.Text,Session["CoCode"].ToString().Trim()) == true)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "msg", "alert('Deleted Successfully !');", true);
        }
        else
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "msg", "alert('Error in Deleted Data !');", true);
        }
        GridFill();
        ResetAllFields();
    }

    public void getBillQty(string value, TextBox t, string duration, TextBox t2)
    {
        if (value == "0")
        { t.Text = ""; t2.Text = ""; }
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
        getBillQty(ddlDose1.SelectedValue, txtBillQty1, ddlDuration21.SelectedValue, txtActualQty21);
    }
    protected void ddlDose2_SelectedIndexChanged(object sender, EventArgs e)
    {
        getBillQty(ddlDose2.SelectedValue, txtBillQty2, ddlDuration22.SelectedValue, txtActualQty22);
    }
    protected void ddlDose3_SelectedIndexChanged(object sender, EventArgs e)
    {
        getBillQty(ddlDose3.SelectedValue, txtBillQty3, ddlDuration23.SelectedValue, txtActualQty23);
    }
    protected void ddlDose4_SelectedIndexChanged(object sender, EventArgs e)
    {
        getBillQty(ddlDose4.SelectedValue, txtBillQty4, ddlDuration24.SelectedValue, txtActualQty24);
    }
    protected void ddlDose5_SelectedIndexChanged(object sender, EventArgs e)
    {
        getBillQty(ddlDose5.SelectedValue, txtBillQty5, ddlDuration25.SelectedValue, txtActualQty25);
    }
    protected void ddlDuration21_SelectedIndexChanged(object sender, EventArgs e)
    {
        getBillQty(ddlDose1.SelectedValue, txtBillQty1, ddlDuration21.SelectedValue, txtActualQty21);
    }
    protected void ddlDuration22_SelectedIndexChanged(object sender, EventArgs e)
    {
        getBillQty(ddlDose2.SelectedValue, txtBillQty2, ddlDuration22.SelectedValue, txtActualQty22);
    }
    protected void ddlDuration23_SelectedIndexChanged(object sender, EventArgs e)
    {
        getBillQty(ddlDose3.SelectedValue, txtBillQty3, ddlDuration23.SelectedValue, txtActualQty23);
    }
    protected void ddlDuration24_SelectedIndexChanged(object sender, EventArgs e)
    {
        getBillQty(ddlDose4.SelectedValue, txtBillQty4, ddlDuration24.SelectedValue, txtActualQty24);
    }
    protected void ddlDuration25_SelectedIndexChanged(object sender, EventArgs e)
    {
        getBillQty(ddlDose5.SelectedValue, txtBillQty5, ddlDuration25.SelectedValue, txtActualQty25);
    }
    protected void ddlMedGroup2_SelectedIndexChanged(object sender, EventArgs e)
    {
        DropDownSubGroupFill(ddlMedGroup2.SelectedValue, ddlSubGroup2);
    }
    protected void ddlMedGroup3_SelectedIndexChanged(object sender, EventArgs e)
    {
        DropDownSubGroupFill(ddlMedGroup3.SelectedValue, ddlSubGroup3);
    }
    protected void ddlMedGroup4_SelectedIndexChanged(object sender, EventArgs e)
    {
        DropDownSubGroupFill(ddlMedGroup4.SelectedValue, ddlSubGroup4);
    }
    protected void ddlMedGroup5_SelectedIndexChanged(object sender, EventArgs e)
    {
        DropDownSubGroupFill(ddlMedGroup5.SelectedValue, ddlSubGroup5);
    }


    protected void ddlSubGroup2_SelectedIndexChanged(object sender, EventArgs e)
    {
        DropDownMedicineFill(ddlSubGroup2.SelectedValue, ddlMedicine2);
    }
    protected void ddlSubGroup3_SelectedIndexChanged(object sender, EventArgs e)
    {
        DropDownMedicineFill(ddlSubGroup3.SelectedValue, ddlMedicine3);
    }
    protected void ddlSubGroup4_SelectedIndexChanged(object sender, EventArgs e)
    {
        DropDownMedicineFill(ddlSubGroup4.SelectedValue, ddlMedicine4);
    }
    protected void ddlSubGroup5_SelectedIndexChanged(object sender, EventArgs e)
    {
        DropDownMedicineFill(ddlSubGroup5.SelectedValue, ddlMedicine5);
    }
    protected void ddlMedicine2_SelectedIndexChanged(object sender, EventArgs e)
    {
        BatchNoFill(ddlMedicine2.SelectedValue, ddlBatchNo2);
        Exdate(ddlBatchNo2, txtExpiryDate2);
    }
    protected void ddlMedicine3_SelectedIndexChanged(object sender, EventArgs e)
    {
        BatchNoFill(ddlMedicine3.SelectedValue, ddlBatchNo3);
        Exdate(ddlBatchNo3, txtExpiryDate3);
    }
    protected void ddlMedicine4_SelectedIndexChanged(object sender, EventArgs e)
    {
        BatchNoFill(ddlMedicine4.SelectedValue, ddlBatchNo4);
        Exdate(ddlBatchNo4, txtExpiryDate4);
    }
    protected void ddlMedicine5_SelectedIndexChanged(object sender, EventArgs e)
    {
        BatchNoFill(ddlMedicine5.SelectedValue, ddlBatchNo5);
        Exdate(ddlBatchNo5, txtExpiryDate1);
    }
    protected void ddlBatchNo2_SelectedIndexChanged(object sender, EventArgs e)
    {
        DataTable dt = thedocvisit.ExpiryDateFill(Session["CoCode"].ToString().Trim(), ddlBatchNo2.SelectedValue);
        txtExpiryDate2.Text = dt.Rows[0][0].ToString();
    }
    protected void ddlBatchNo3_SelectedIndexChanged(object sender, EventArgs e)
    {
        DataTable dt = thedocvisit.ExpiryDateFill(Session["CoCode"].ToString().Trim(), ddlBatchNo3.SelectedValue);
        txtExpiryDate3.Text = dt.Rows[0][0].ToString();
    }
    protected void ddlBatchNo4_SelectedIndexChanged(object sender, EventArgs e)
    {
        DataTable dt = thedocvisit.ExpiryDateFill(Session["CoCode"].ToString().Trim(), ddlBatchNo4.SelectedValue);
        txtExpiryDate4.Text = dt.Rows[0][0].ToString();
    }
    protected void ddlBatchNo5_SelectedIndexChanged(object sender, EventArgs e)
    {
        DataTable dt = thedocvisit.ExpiryDateFill(Session["CoCode"].ToString().Trim(), ddlBatchNo5.SelectedValue);
        txtExpiryDate5.Text = dt.Rows[0][0].ToString();
    }
}