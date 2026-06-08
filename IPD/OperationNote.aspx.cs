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
 
public partial class IPD_OperationNote : System.Web.UI.Page
{
    Main Objmain = new Main(ConfigurationSettings.AppSettings["ConnectionString"].ToString());
    OperationNote theaddConsumable = new OperationNote(ConfigurationSettings.AppSettings["ConnectionString"].ToString());
    protected void Page_Load(object sender, EventArgs e)
    {
        Page.Title = "Operation Note";
        if (Session["userName"] == null)
        {
            Response.Redirect("../LoginPage.aspx");
        }
        if (Objmain.checkaccss(Session["CoCode"].ToString(), Session["UserRoleID"].ToString().Trim(), "OPERATION NOTE", checkAccessType.ViewAction) == false)
        {
            Response.Redirect("../AccessDenied.aspx");
        }

        if (Objmain.checkaccss(Session["CoCode"].ToString(), Session["UserRoleID"].ToString().Trim(), "OPERATION NOTE", checkAccessType.InsertAction) == false)
        {
            Button1.Enabled = false;
        }
        
        if (!IsPostBack)
        {
            Tab1Func();

            if (Session["ReqNo"] != null)
            {
                DataSet ds = theaddConsumable.Get_Onlyreg_Sur_Doc_Anes_Emp(Session["ReqNo"].ToString(), Session["CoCode"].ToString().Trim(), Session["YearCode"].ToString().Trim());
                TextBox2.Text = Session["ReqNo"].ToString();
  
                TextBox1.Text = ds.Tables[0].Rows[0][0].ToString();
                DropDownFill(ds);

                GridFill_OperationDetails();
                GridFill_PatientDetails();
            }
            else
            {
                DataSet ds = theaddConsumable.Get_Onlyreg_Sur_Doc_Anes_Emp("", Session["CoCode"].ToString().Trim(), Session["YearCode"].ToString().Trim());
                DropDownFill(ds);
            }
            Session["ReqNo"] = null;
        }
    }

    private void DropDownFill(DataSet ds)
    {
        this.DropDownList1.DataSource = ds.Tables[1];
        this.DropDownList1.DataTextField = "doc_name";
        this.DropDownList1.DataValueField = "doc_id";
        this.DropDownList1.DataBind();
        this.DropDownList1.Items.Insert(0, new ListItem("--Select--", "0"));


        DropDownList10.DataSource = ds.Tables[5];
        DropDownList10.DataTextField = "ServiceTemplateName";
        DropDownList10.DataValueField = "ServiceTemplateName";
        DropDownList10.DataBind();
        DropDownList10.Items.Insert(0, new ListItem("--Select--", "0"));


        DropDownList11.DataSource = ds.Tables[6];
        DropDownList11.DataTextField = "PrescrpTemName";
        DropDownList11.DataValueField = "PrescrpTemName";
        DropDownList11.DataBind();
        DropDownList11.Items.Insert(0, new ListItem("--Select--", "0"));



        for (int i = 2; i <= 4; i++)
        {
            DropDownList d = (DropDownList)Page.FindControl("ctl00$ContentPlaceHolder1$DropDownList" + i.ToString());
            d.DataSource = ds.Tables[2];
            d.DataTextField = "doc_name";
            d.DataValueField = "doc_id";
            d.DataBind();
            d.Items.Insert(0, new ListItem("--Select--", "0"));
        }

        for (int i = 5; i <= 6; i++)
        {
            DropDownList d = (DropDownList)Page.FindControl("ctl00$ContentPlaceHolder1$DropDownList" + i.ToString());
            d.DataSource = ds.Tables[2];
            d.DataTextField = "doc_name";
            d.DataValueField = "doc_id";
            d.DataBind();
            d.Items.Insert(0, new ListItem("--Select--", "0"));
        }

        for (int i = 7; i <= 9; i++)
        {
            DropDownList d = (DropDownList)Page.FindControl("ctl00$ContentPlaceHolder1$DropDownList" + i.ToString());
            d.DataSource = ds.Tables[4];
            d.DataTextField = "EmployeeName";
            d.DataValueField = "EmployeeID";
            d.DataBind();
            d.Items.Insert(0, new ListItem("--Select--", "0"));
        }

    }

    [System.Web.Script.Services.ScriptMethod()]
    [System.Web.Services.WebMethod]

    public static List<string> SearchCustomers(string prefixText, int count)
    {
        using (SqlConnection conn = new SqlConnection())
        {
            conn.ConnectionString = ConfigurationSettings.AppSettings["ConnectionString"].ToString();
            using (SqlCommand cmd = new SqlCommand())
            {
                cmd.CommandText = "select distinct Remarks as Name from IPD_OTOperationNote where Remarks like @SearchText + '%'";
                cmd.Parameters.AddWithValue("@SearchText", prefixText);
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

    public void Fill_Surgeon_Anesthesis_Asst(DataTable dt)
    {
        DropDownList1.SelectedValue = dt.Rows[0]["SurgeonID"].ToString();
        DropDownList2.SelectedValue = dt.Rows[0]["AdditionalDoctor1"].ToString();
        DropDownList3.SelectedValue = dt.Rows[0]["AdditionalDoctor2"].ToString();
        DropDownList4.SelectedValue = dt.Rows[0]["AdditionalDoctor3"].ToString();
        DropDownList5.SelectedValue = dt.Rows[0]["AnesthetistName1"].ToString();
        DropDownList6.SelectedValue = dt.Rows[0]["AnesthetistName2"].ToString();
        DropDownList7.SelectedValue = dt.Rows[0]["Assistant1"].ToString();
        DropDownList8.SelectedValue = dt.Rows[0]["Assistant2"].ToString();
        DropDownList9.SelectedValue = dt.Rows[0]["Assistant3"].ToString();
        TextBox7.Text = dt.Rows[0]["opdate"].ToString();
        TextBox8.Text = dt.Rows[0]["StartTime"].ToString();
        TextBox9.Text = dt.Rows[0]["EndTime"].ToString();
        TextBox10.Text = dt.Rows[0]["Remarks"].ToString();
    }
  


    private void ResetAllFields()
    {
        for (int i =2 ; i <= 10; i++)
        {
            TextBox t = (TextBox)Page.FindControl("ctl00$ContentPlaceHolder1$TextBox" + i.ToString());
            t.Text = "";
        }
        TextBox1.Text = ""; txtProbableDischargeDate.Text = ""; 
        DropDownList4.SelectedIndex = 0; DropDownList1.SelectedIndex = 0; DropDownList2.SelectedIndex = 0; DropDownList3.SelectedIndex = 0; DropDownList5.SelectedIndex = 0;
        DropDownList6.SelectedIndex = 0; DropDownList7.SelectedIndex = 0; DropDownList8.SelectedIndex = 0; DropDownList9.SelectedIndex = 0;
        Button1.Text = "Submit";
        if (Objmain.checkaccss(Session["CoCode"].ToString(), Session["UserRoleID"].ToString().Trim(), "OPERATION NOTE", checkAccessType.InsertAction) == false)
        {
            Button1.Enabled = false;
        }

    }
    protected void Button1_Click(object sender, EventArgs e)
    {
        string anesthesiatype;
        System.Globalization.DateTimeFormatInfo dtf = new DateTimeFormatInfo();
        DateTime testdate = DateTime.ParseExact(TextBox7.Text, "dd/MM/yyyy", dtf);

        string probDisDate="";
        if (txtProbableDischargeDate.Text != "")
            probDisDate = DateTime.ParseExact(txtProbableDischargeDate.Text, "dd/MM/yyyy", dtf).ToString("yyyy-MM-dd");
        else
            probDisDate = "";

        if(DropDownList10.SelectedIndex!=0)
            anesthesiatype=DropDownList10.SelectedItem.Text;
        else
            anesthesiatype="";
        if (Button1.Text == "Submit")
        {

            if (theaddConsumable.InsertOperation(anesthesiatype, TextBox2.Text, TextBox1.Text, probDisDate, DropDownList1.SelectedValue, DropDownList2.SelectedValue, DropDownList3.SelectedValue, DropDownList4.SelectedValue, DropDownList5.SelectedValue, DropDownList6.SelectedValue, DropDownList7.SelectedValue, DropDownList8.SelectedValue, DropDownList9.SelectedValue, testdate.ToString("yyyy-MM-dd"), TextBox8.Text, TextBox9.Text, TextBox10.Text, Session["CoCode"].ToString().Trim(), Session["YearCode"].ToString().Trim(), Session["userName"].ToString().Trim()) == true)
            {
                theaddConsumable.InsertAnesthesiaType_Medicine_Consumable(TextBox1.Text, DropDownList10.SelectedValue, DropDownList11.SelectedValue, testdate.ToString(), Session["CoCode"].ToString().Trim(), Session["YearCode"].ToString().Trim(), Session["userName"].ToString().Trim());
                //ScriptManager.RegisterStartupScript(this, this.GetType(), "msg", "alert('Inserted  Successfully !');", true);
                Response.Redirect("../IPD/OTList.aspx");
               
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "msg", "alert('Error in Inserted Data !');", true);
            }
        }
        else
        {
            if (theaddConsumable.UpdateOperation(anesthesiatype, HiddenField1.Value, TextBox2.Text, TextBox1.Text, probDisDate, DropDownList1.SelectedValue, DropDownList2.SelectedValue, DropDownList3.SelectedValue, DropDownList4.SelectedValue, DropDownList5.SelectedValue, DropDownList6.SelectedValue, DropDownList7.SelectedValue, DropDownList8.SelectedValue, DropDownList9.SelectedValue, testdate.ToString(), TextBox8.Text, TextBox9.Text, TextBox10.Text, Session["CoCode"].ToString().Trim(), Session["YearCode"].ToString().Trim(), Session["userName"].ToString().Trim()) == true)
            {
                theaddConsumable.InsertAnesthesiaType_Medicine_Consumable(TextBox1.Text, DropDownList10.SelectedValue, DropDownList11.SelectedValue, testdate.ToString(), Session["CoCode"].ToString().Trim(), Session["YearCode"].ToString().Trim(), Session["userName"].ToString().Trim());
                //ScriptManager.RegisterStartupScript(this, this.GetType(), "msg", "alert('Updated   Successfully !');", true);
                Response.Redirect("../IPD/OTList.aspx");
                Button1.Text = "Submit";

            }
            else
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "msg", "alert('Error in Updated data !');", true);
            }
        }
        GridFill_PatientDetails();
        ResetAllFields();
    }


    protected void Button2_Click(object sender, EventArgs e)
    {
        ResetAllFields();
    }

    protected void GridView1_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        GridView1.PageIndex = e.NewPageIndex;
        GridFill_PatientDetails();
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


    protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "Select")
        {
            int index = Convert.ToInt32(e.CommandArgument);
            lblError.Text = "";
            Label lblid = (Label)GridView1.Rows[index].FindControl("lblid");
            HiddenField1.Value = lblid.Text;

            Label lblRegno = (Label)GridView1.Rows[index].FindControl("lblRegno");
            TextBox1.Text = lblRegno.Text;
            Label lbllname = (Label)GridView1.Rows[index].FindControl("lbllname");
            TextBox3.Text = lbllname.Text;
            Label lbladate = (Label)GridView1.Rows[index].FindControl("lbladate");
            TextBox5.Text = lbladate.Text;
            Label lblbedno = (Label)GridView1.Rows[index].FindControl("lblbedno");
            TextBox4.Text = lblbedno.Text;

            Label lblsurgeon = (Label)GridView1.Rows[index].FindControl("lblsurgeon");
            DropDownList1.SelectedIndex = SearchIndex(lblsurgeon.Text, DropDownList1);

            Label lbldoc1 = (Label)GridView1.Rows[index].FindControl("lbldoc1");
            DropDownList2.SelectedIndex = SearchIndex(lbldoc1.Text, DropDownList2);

            Label lbldoc2 = (Label)GridView1.Rows[index].FindControl("lbldoc2");
            DropDownList3.SelectedIndex = SearchIndex(lbldoc2.Text, DropDownList3);

            Label lbldoc3 = (Label)GridView1.Rows[index].FindControl("lbldoc3");
            DropDownList4.SelectedIndex = SearchIndex(lbldoc3.Text, DropDownList4);

            Label lblanesthesis1 = (Label)GridView1.Rows[index].FindControl("lblanesthesis1");
            DropDownList5.SelectedIndex = SearchIndex(lblanesthesis1.Text, DropDownList5);

            Label lblanesthesis2 = (Label)GridView1.Rows[index].FindControl("lblanesthesis2");
            DropDownList6.SelectedIndex = SearchIndex(lblanesthesis2.Text, DropDownList6);

            Label lblassistant1 = (Label)GridView1.Rows[index].FindControl("lblassistant1");
            DropDownList7.SelectedIndex = SearchIndex(lblassistant1.Text, DropDownList7);

            Label lblassistant2 = (Label)GridView1.Rows[index].FindControl("lblassistant2");
            DropDownList8.SelectedIndex = SearchIndex(lblassistant2.Text, DropDownList8);

            Label lblassistant3 = (Label)GridView1.Rows[index].FindControl("lblassistant3");
            DropDownList9.SelectedIndex = SearchIndex(lblassistant3.Text, DropDownList9);

            Label lbldate = (Label)GridView1.Rows[index].FindControl("lbldate");
            TextBox7.Text = lbldate.Text;

            Label lblstarttime = (Label)GridView1.Rows[index].FindControl("lblstarttime");
            TextBox8.Text = lblstarttime.Text;

            Label lblendtime = (Label)GridView1.Rows[index].FindControl("lblendtime");
            TextBox9.Text = lblendtime.Text;


            Label lblremarks = (Label)GridView1.Rows[index].FindControl("lblremarks");
            TextBox10.Text = lblremarks.Text;
            Tab1Func();
             Button1.Text = "Update";
             if (Objmain.checkaccss(Session["CoCode"].ToString(), Session["UserRoleID"].ToString().Trim(), "OPERATION NOTE", checkAccessType.UpdateAction) == false)
             {
                 Button1.Enabled = false;
             }
             else
             {
                 Button1.Enabled = true;
             }
        }
    }

    protected void Button4_Click(object sender, EventArgs e)
    {
        GetRequisitionNo_Details();
        GridFill_OperationDetails();
    }
    private void GridFill_PatientDetails()
    {
        DataTable dt = theaddConsumable.GetAllOperationNote(TextBox1.Text, TextBox2.Text, Session["CoCode"].ToString().Trim(), Session["YearCode"].ToString().Trim());
        DataTable dt2 = theaddConsumable.GetAllOperationreq(TextBox1.Text, TextBox2.Text, Session["CoCode"].ToString().Trim(), Session["YearCode"].ToString().Trim());
        DataTable dt1 = theaddConsumable.Getonlypat(TextBox1.Text, TextBox2.Text, Session["CoCode"].ToString().Trim(), Session["YearCode"].ToString().Trim());
        if (dt.Rows.Count > 0)
        {
            GridView1.DataSource = dt;
            GridView1.DataBind();
            HiddenField1.Value = dt.Rows[0]["OperationNoteId"].ToString();
            TextBox3.Text = dt.Rows[0]["patient_name"].ToString();
            TextBox4.Text = dt.Rows[0]["BedNoText"].ToString();
            TextBox6.Text = dt.Rows[0]["adate"].ToString();
            TextBox5.Text = dt.Rows[0]["OperationName"].ToString();
            txtProbableDischargeDate.Text = dt.Rows[0]["ProbableDischargeDate"].ToString();

            // for Anesthesis Type
            if (dt.Rows[0]["AnesthesiaType"].ToString() != "")
            {
                DropDownList10.SelectedValue = dt.Rows[0]["AnesthesiaType"].ToString();
                //DropDownList11.Text = dt.Rows[0]["AnesthesiaType"].ToString(); // vivek to be update
            }

            // For details doctor and anesthesia and assistant ...........
            Fill_Surgeon_Anesthesis_Asst(dt);
            Button1.Text = "Update";
            if (Objmain.checkaccss(Session["CoCode"].ToString(), Session["UserRoleID"].ToString().Trim(), "OPERATION NOTE", checkAccessType.UpdateAction) == false)
            {
                Button1.Enabled = false;
            }
            else
            {
                Button1.Enabled = true;
            }

        }
        
        else
        {
            if (dt1.Rows.Count > 0)
            {
                txtProbableDischargeDate.Text = dt1.Rows[0]["ProbableDischargeDate"].ToString();
                TextBox3.Text = dt1.Rows[0]["patient_name"].ToString();
                TextBox4.Text = dt1.Rows[0]["BedNoText"].ToString();
                TextBox2.Text = dt1.Rows[0]["OperationReqID"].ToString();
                TextBox6.Text = dt1.Rows[0]["adate"].ToString();
                TextBox5.Text = dt1.Rows[0]["OperationName"].ToString();

                DropDownList1.SelectedValue = dt1.Rows[0]["SurgeonID"].ToString();
                DropDownList5.SelectedValue = dt1.Rows[0]["Anesthetist1"].ToString();
                TextBox7.Text = dt1.Rows[0]["otdate"].ToString();
                TextBox8.Text = dt1.Rows[0]["StartTime"].ToString();
                TextBox9.Text = dt1.Rows[0]["EndTime"].ToString();
            }
            else if (dt2.Rows.Count > 0)
            {
                txtProbableDischargeDate.Text = dt2.Rows[0]["ProbableDischargeDate"].ToString();
                TextBox3.Text = dt2.Rows[0]["patient_name"].ToString();
                TextBox4.Text = dt2.Rows[0]["BedNoText"].ToString();
                TextBox2.Text = dt2.Rows[0]["OperationReqID"].ToString();
                TextBox6.Text = dt2.Rows[0]["adate"].ToString();
                TextBox5.Text = dt2.Rows[0]["OperationName"].ToString();

                DropDownList1.SelectedValue = dt2.Rows[0]["SurgeonID"].ToString();
                DropDownList5.SelectedValue = dt2.Rows[0]["Anesthetist1"].ToString();
                TextBox7.Text = dt2.Rows[0]["otdate"].ToString();
                TextBox8.Text = dt2.Rows[0]["StartTime"].ToString();
            }
        }
    }
    private void GridFill_OperationDetails()
    {
        DataTable dt = theaddConsumable.GridFillSecond(TextBox1.Text, Session["CoCode"].ToString().Trim(), Session["YearCode"].ToString().Trim());
        if (dt.Rows.Count > 0)
        {
            GridView2.DataSource = dt;
            GridView2.DataBind();
        }
    }

    private void GetRequisitionNo_Details()
    {
        DataTable dtreq = theaddConsumable.Getonltreq(TextBox1.Text, Session["CoCode"].ToString().Trim(), Session["YearCode"].ToString().Trim());
        if (dtreq.Rows.Count > 0)
        {
            TextBox2.Text = dtreq.Rows[0][0].ToString();
            GridFill_PatientDetails();
        }
        else
        {
            lblError.ForeColor = System.Drawing.Color.Red;
            lblError.Text = " No OT is done of This patient";
        }

    }
    protected void GridView2_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {

    }
    protected void GridView2_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "Select")
        {
            ResetAllFields();
            int index = Convert.ToInt32(e.CommandArgument);
            lblError.Text = "";
            Label lblid = (Label)GridView2.Rows[index].FindControl("lblid");
            TextBox2.Text = lblid.Text;

            Label lblRegno = (Label)GridView2.Rows[index].FindControl("lblRegno");
            TextBox1.Text = lblRegno.Text;
            Tab1Func();
            GridFill_PatientDetails();
        }
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
    protected void DropDownList10_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (DropDownList10.SelectedIndex ==1)
            DropDownList11.SelectedIndex = 1;
        else
            if (DropDownList10.SelectedIndex == 2)
                DropDownList11.SelectedIndex = 2;
            else
                if (DropDownList10.SelectedIndex == 3)
                    DropDownList11.SelectedIndex = 3;
        else
                        DropDownList11.SelectedIndex = 0;
    }
    protected void Button8_Click(object sender, EventArgs e)
    {
        Session["OTPReg"] = TextBox1.Text;
        Response.Redirect("../IPD/LaprosopicNote.aspx");
    }
}