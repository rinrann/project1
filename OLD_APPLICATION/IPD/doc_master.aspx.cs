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
using System.Collections.Generic;

public partial class Master_doc_master : System.Web.UI.Page
{
    Main Objmain = new Main(ConfigurationSettings.AppSettings["ConnectionString"].ToString());
    doc_master theHelper = new doc_master(ConfigurationSettings.AppSettings["ConnectionString"].ToString());
    PatientAppointment thepatientAppo = new PatientAppointment(ConfigurationSettings.AppSettings["ConnectionString"].ToString());
    string am;
     private void SetInitialRow()
    {
        DataTable dt = new DataTable();
        DataRow dr = null;

        ddlDept.Items.Clear();
        ddlDept.DataSource = thepatientAppo.DropdownDepartment(Session["CoCode"].ToString().Trim());
        ddlDept.DataTextField = "DeptName";
        ddlDept.DataValueField = "DeptCode";
        ddlDept.DataBind();

        dt.Columns.Add(new DataColumn("RowNumber", typeof(string)));
        dt.Columns.Add(new DataColumn("Column1", typeof(string)));
        dt.Columns.Add(new DataColumn("Column2", typeof(string)));
        dt.Columns.Add(new DataColumn("Column3", typeof(string)));

        dr = dt.NewRow();
        dr["RowNumber"] = 1;
        dr["Column1"] = string.Empty;
        dr["Column2"] = string.Empty;
        dr["Column3"] = string.Empty;
        dt.Rows.Add(dr);

        //Store the DataTable in ViewState
        ViewState["CurrentTable"] = dt;

        Gridview3.DataSource = dt;
        Gridview3.DataBind();
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
                    DropDownList ddl = (DropDownList)Gridview3.Rows[rowIndex].Cells[1].FindControl("ddlVisitingDays");
                    TextBox box2 = (TextBox)Gridview3.Rows[rowIndex].Cells[2].FindControl("txtInTime");
                    TextBox box3 = (TextBox)Gridview3.Rows[rowIndex].Cells[3].FindControl("txtOutTime");

                    ddl.SelectedValue = dt.Rows[i]["Column1"].ToString();
                    box2.Text = dt.Rows[i]["Column2"].ToString();
                    box3.Text = dt.Rows[i]["Column3"].ToString();

                    rowIndex++;
                }
            }
        }
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        Page.Title = "Doctor Master";
        GridFill();
        if (Session["userName"] == null)
        {
            Response.Redirect("../LoginPage.aspx");
        }
        if (Objmain.checkaccss(Session["CoCode"].ToString(), Session["UserRoleID"].ToString().Trim(), "DOCTOR MASTER", checkAccessType.ViewAction) == false)
        {
            Response.Redirect("../AccessDenied.aspx");
        }

        if (Objmain.checkaccss(Session["CoCode"].ToString(), Session["UserRoleID"].ToString().Trim(), "DOCTOR MASTER", checkAccessType.InsertAction) == false)
        {
            Button1.Enabled = false;
        }
        if (!IsPostBack)
        {
            Tab1Func();
            DropDownFill();
            SetInitialRow();
        }
      
    }

    private void DropDownFill()
    {
        this.DropDownList1.DataSource = theHelper.GetDoctorType(Session["CoCode"].ToString());
        this.DropDownList1.DataTextField = "TypeName";
        this.DropDownList1.DataValueField = "DocTypeId";
        this.DropDownList1.DataBind();
        this.DropDownList1.Items.Insert(0, new ListItem("--Select--", "0"));

        this.DropDownList2.DataSource = theHelper.GetCountry(Session["CoCode"].ToString());
        this.DropDownList2.DataTextField = "CountryName";
        this.DropDownList2.DataValueField = "CountryId";
        this.DropDownList2.DataBind();
        this.DropDownList2.Items.Insert(0, new ListItem("--Select--", "0"));

        //this.DropDownList3.DataSource = theHelper.GetState(Session["CoCode"].ToString());
        //this.DropDownList3.DataTextField = "State_Name";
        //this.DropDownList3.DataValueField = "State_ID";
        //this.DropDownList3.DataBind();
        //this.DropDownList3.Items.Insert(0, new ListItem("--Select--", "0"));

        getstateList();

        for (int i = 4; i <= 6; i++)
        {
            DropDownList d = (DropDownList)Page.FindControl("ctl00$ContentPlaceHolder1$DropDownList" +i.ToString());
           d.DataSource = theHelper.GetDoctorSpeciality();
           d.DataTextField = "SpecialtyName";
           d.DataValueField = "SpecialtyID";
           d.DataBind();
           d.Items.Insert(0, new ListItem("--Select--", "0"));
        }
       
    }

    private void GridFill()
    {
        GridView1.DataSource = theHelper.GetAllDoctor(Session["CoCode"].ToString());
        GridView1.DataBind();
    }

    private void ResetAllFields()
    {
        SetInitialRow();
        txtDoctorVisitCharge.Text = "";
        for (int i = 1; i <= 6; i++)
        {
            DropDownList d = (DropDownList)Page.FindControl("ctl00$ContentPlaceHolder1$DropDownList" + i.ToString());
            d.SelectedIndex = 0;
        }
        TextBox1.Text = ""; TextBox2.Text = ""; TextBox3.Text = ""; TextBox4.Text = ""; TextBox5.Text = ""; TextBox7.Text = ""; TextBox9.Text = ""; TextBox10.Text = "";
        TextBox14.Text = "";
        TextBox11.Text = "";
        TextBox12.Text = "";
        TextBox13.Text = "";
        TextBox15.Text = ""; TextBox16.Text = "";
        Button1.Text = "Submit";
        DropDownList3.SelectedIndex = 0;
        if (Objmain.checkaccss(Session["CoCode"].ToString(), Session["UserRoleID"].ToString().Trim(), "DOCTOR MASTER", checkAccessType.InsertAction) == false)
        {
            Button1.Enabled = false;
        }
    }

    private int cint(string p)
    {
        throw new Exception("The method or operation is not implemented.");
    }

    protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
     {
         lblError.Text = "";
        if (e.CommandName == "Select")
        {
            int index = Convert.ToInt32(e.CommandArgument);


            Label lblDoctorId = (Label)GridView1.Rows[index].FindControl("lblDoctorId");
            HiddenField1.Value = lblDoctorId.Text;



            Label lbldrregno = (Label)GridView1.Rows[index].FindControl("lbldrregno");
            TextBox15.Text = lbldrregno.Text;

            Label lbltype = (Label)GridView1.Rows[index].FindControl("lbltype");
            DropDownList1.SelectedIndex = SearchIndex(lbltype.Text, DropDownList1);

            Label lblDoctorName = (Label)GridView1.Rows[index].FindControl("lblDoctorName");
            string doc_name = lblDoctorName.Text;
            string[] doc_name1 = doc_name.Split(' ');

            TextBox20.Text = doc_name1[0]; 
            for(int i=1;i<doc_name1.Length;i++)
              am =am+" " +doc_name1[i];
            TextBox1.Text = am;

            Label lblAddress = (Label)GridView1.Rows[index].FindControl("lblAddress");
            TextBox2.Text = lblAddress.Text;

            Label lblCountry = (Label)GridView1.Rows[index].FindControl("lblCountry");
            DropDownList2.SelectedValue = lblCountry.Text;

            Label lblvisit_charges = (Label)GridView1.Rows[index].FindControl("lblvisit_charges");
            txtDoctorVisitCharge.Text = lblvisit_charges.Text;

            getstateList();
            Label lblstate = (Label)GridView1.Rows[index].FindControl("lblstate");
            DropDownList3.SelectedIndex = SearchIndex(lblstate.Text, DropDownList3);

            Label lblCity = (Label)GridView1.Rows[index].FindControl("lblCity");
            TextBox3.Text = lblCity.Text;

            Label lblPin = (Label)GridView1.Rows[index].FindControl("lblPin");
            TextBox4.Text = lblPin.Text;

            Label lblPhone = (Label)GridView1.Rows[index].FindControl("lblPhone");
            //string[] ph = lblPhone.Text.Trim().Split(' ');
            //TextBox6.Text ="+91";
            //if(ph.Length>1)
            TextBox7.Text = lblPhone.Text;


            Label lbldoc_ph_res = (Label)GridView1.Rows[index].FindControl("lbldoc_ph_res");
            string[] Contracts1 = lbldoc_ph_res.Text.Split(' ');

            //TextBox8.Text = Contracts1[0];
            if (Contracts1.Length > 1) { TextBox9.Text = Contracts1[0]; }
            if (Contracts1.Length > 2) { TextBox10.Text = Contracts1[1]; }


            Label lblEmailId = (Label)GridView1.Rows[index].FindControl("lblEmailId");
            TextBox11.Text = lblEmailId.Text;

            Label lblFax = (Label)GridView1.Rows[index].FindControl("lblFax");
            TextBox12.Text = lblFax.Text;

            Label lblDeptCode = (Label)GridView1.Rows[index].FindControl("lblDeptCode");
            ddlDept.SelectedValue = lblDeptCode.Text.Trim();

            Label lblSpeciality1 = (Label)GridView1.Rows[index].FindControl("lblSpeciality1");
            DropDownList4.SelectedIndex = SearchIndex(lblSpeciality1.Text, DropDownList4);


            Label lblSpeciality2 = (Label)GridView1.Rows[index].FindControl("lblSpeciality2");
            DropDownList5.SelectedIndex = SearchIndex(lblSpeciality2.Text, DropDownList5);


            Label lblSpeciality3 = (Label)GridView1.Rows[index].FindControl("lblSpeciality3");
            DropDownList6.SelectedIndex = SearchIndex(lblSpeciality3.Text, DropDownList6);

            Label lblqualification = (Label)GridView1.Rows[index].FindControl("lblqualification");
            TextBox5.Text = lblqualification.Text;

            
            Label lblCommission_Per = (Label)GridView1.Rows[index].FindControl("lblCommission_Per");
            TextBox13.Text = lblCommission_Per.Text;

            Label lblCommission_Rs = (Label)GridView1.Rows[index].FindControl("lblCommission_Rs");
            TextBox14.Text = lblCommission_Rs.Text;

            Label lblDocFees = (Label)GridView1.Rows[index].FindControl("lblDocFees");
            TextBox16.Text = lblDocFees.Text;

            Label lblDocFeesNight = (Label)GridView1.Rows[index].FindControl("lblDocFeesNight");
            TextBox17.Text = lblDocFeesNight.Text;

            Label lblNightVisit = (Label)GridView1.Rows[index].FindControl("lblNightVisit");
            txtNightVisitCharge.Text = lblNightVisit.Text;

            Label lblConsultant = (Label)GridView1.Rows[index].FindControl("lblConsultant");
            if (lblConsultant.Text == "Y")
            {
                chkCon.Checked = true;
            }
            else
            {
                chkCon.Checked = false;
            }
            Tab1Func();

            int rowIndex = 0;
            if (ViewState["CurrentTable"] != null)
            {
                DataTable dt = theHelper.BindGrid(lblDoctorId.Text);
                if (dt.Rows.Count > 0)
                {
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        DropDownList ddl = (DropDownList)Gridview3.Rows[rowIndex].Cells[1].FindControl("ddlVisitingDays");
                        TextBox box2 = (TextBox)Gridview3.Rows[rowIndex].Cells[2].FindControl("txtInTime");
                        TextBox box3 = (TextBox)Gridview3.Rows[rowIndex].Cells[3].FindControl("txtOutTime");

                        ddl.SelectedValue = dt.Rows[i]["DayId"].ToString();
                        box2.Text = dt.Rows[i]["InTime"].ToString();
                        box3.Text = dt.Rows[i]["OutTime"].ToString();
                        if ((dt.Rows.Count - 1) != rowIndex)
                        {
                            AddNewRowToGrid();
                        }
                        rowIndex++;
                    }
                }
            }
              
            Button1.Text = "Update";
            if (Objmain.checkaccss(Session["CoCode"].ToString(), Session["UserRoleID"].ToString().Trim(), "DOCTOR MASTER", checkAccessType.UpdateAction) == false)
            {
                Button1.Enabled = false;
            }
            else
            {
                Button1.Enabled = true;
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

    protected void GridView1_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        Label lblDoctorId = (Label)GridView1.Rows[e.RowIndex].FindControl("lblDoctorId");
        theHelper.DeleteDOC_MASTER(lblDoctorId.Text, Session["CoCode"].ToString());
        lblError.ForeColor = System.Drawing.Color.Green;
        lblError.Text = "Deleted Successfully";
        GridFill();
        ResetAllFields();
    }

    protected void GridView1_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        GridView1.PageIndex = e.NewPageIndex;
        GridFill();
    }

    protected void Button2_Click1(object sender, EventArgs e)
    {
        ResetAllFields();
    }
    protected void ButtonAdd_Click(object sender, EventArgs e)
    {
        AddNewRowToGrid();
    }

    private void AddNewRowToGrid()
    {
        int rowIndex = 0;
        if (ViewState["CurrentTable"] != null)
        {
            DataTable dtCurrentTable = (DataTable)ViewState["CurrentTable"];
            DataRow drCurrentRow = null;
            if (dtCurrentTable.Rows.Count > 0)
            {
                for (int i = 1; i <= dtCurrentTable.Rows.Count; i++)
                {
                    //extract the TextBox values
                    DropDownList ddl = (DropDownList)Gridview3.Rows[rowIndex].Cells[1].FindControl("ddlVisitingDays");
                    string box1 = ddl.SelectedValue.ToString();
                    TextBox box2 = (TextBox)Gridview3.Rows[rowIndex].Cells[2].FindControl("txtInTime");
                    TextBox box3 = (TextBox)Gridview3.Rows[rowIndex].Cells[3].FindControl("txtOutTime");

                    drCurrentRow = dtCurrentTable.NewRow();
                    drCurrentRow["RowNumber"] = i + 1;
                    drCurrentRow["Column1"] = box1;
                    drCurrentRow["Column2"] = box2.Text;
                    drCurrentRow["Column3"] = box3.Text;
                    rowIndex++;
                }

                //add new row to DataTable
                dtCurrentTable.Rows.Add(drCurrentRow);
                //Store the current data to ViewState
                ViewState["CurrentTable"] = dtCurrentTable;

                //Rebind the Grid with the current data
                Gridview3.DataSource = dtCurrentTable;
                Gridview3.DataBind();
            }
        }
        else
        {
            Response.Write("ViewState is null");
        }
        //Set Previous Data on Postbacks
        SetPreviousData();
    }
    protected void Button1_Click(object sender, EventArgs e)
    {
        int count = Gridview3.Rows.Count;
        string doctype = DropDownList1.SelectedValue;
        string doc_name = TextBox1.Text.Trim();
        string Address = TextBox2.Text.ToUpper();
        string Country = DropDownList2.SelectedValue;
        string state = DropDownList3.SelectedValue;
        string city = TextBox3.Text.ToUpper();
        string Pin=TextBox4.Text;
        string qualification = TextBox5.Text;
        string mobile = TextBox7.Text;
        string ph = TextBox9.Text+ " " + TextBox10.Text;
        string EmailId = TextBox11.Text;
        string Fax = TextBox12.Text;
        //string Specialist1 = DropDownList4.SelectedValue;
        //string Specialist2 = DropDownList5.SelectedValue;
        //string Specialist3 = DropDownList6.SelectedValue;
        string comm =  TextBox13.Text;
        string Commission_Rs = TextBox14.Text;
        string consultant = "N";
        if (chkCon.Checked == true)
        {
            consultant = "Y";
        }
        else
        {
            consultant = "N";
        }
        DataTable dt = theHelper.GenerateDocID(Session["CoCode"].ToString());
        if (Button1.Text == "Submit")
        {
            if (theHelper.InsertDOCMASTER(txtDoctorVisitCharge.Text, TextBox15.Text, TextBox16.Text, dt.Rows[0][0].ToString(), doctype, doc_name, Address, Country, state, city, Pin, qualification, mobile, ph, EmailId, Fax, comm, Commission_Rs, Session["userId"].ToString(), DateTime.Now.ToString("yyyy-MM-dd"), Session["CoCode"].ToString(), Session["YearCode"].ToString(), TextBox17.Text, txtNightVisitCharge.Text, ddlDept.SelectedValue.Trim(), consultant) == true)
            {
                //for (int i = 0; i < count; i++)
                //{
                //    DropDownList ddl = (DropDownList)Gridview3.Rows[i].FindControl("ddlVisitingDays");
                //    string selecteditem = ddl.SelectedValue.ToString();
                //    TextBox txtInTime = (TextBox)Gridview3.Rows[i].FindControl("txtInTime");
                //    TextBox txtOutTime = (TextBox)Gridview3.Rows[i].FindControl("txtOutTime");
                //    theHelper.InsertDocVisit(dt.Rows[0][0].ToString(), ddl.SelectedValue, txtInTime.Text, txtOutTime.Text, Session["CoCode"].ToString());
                //}
                ScriptManager.RegisterStartupScript(this, this.GetType(), "msg", "alert('Inserted Successfully !');", true);
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "msg", "alert('Error in Inserted Data !');", true);
            }
        }
        else
        {
            if (theHelper.UpdateDOCMASTER(txtDoctorVisitCharge.Text, TextBox15.Text, TextBox16.Text, HiddenField1.Value, doctype, doc_name, Address, Country, state, city, Pin, qualification, mobile, ph, EmailId, Fax, comm, Commission_Rs, Session["CoCode"].ToString(), TextBox17.Text, txtNightVisitCharge.Text, ddlDept.SelectedValue.Trim(), Session["userId"].ToString(), consultant) == true)
            {
                theHelper.DeleteDocVisit(HiddenField1.Value);
                //for (int i = 0; i < count; i++)
                //{
                //    DropDownList ddl = (DropDownList)Gridview3.Rows[i].FindControl("ddlVisitingDays");
                //    string selecteditem = ddl.SelectedValue.ToString();
                //    TextBox txtInTime = (TextBox)Gridview3.Rows[i].FindControl("txtInTime");
                //    TextBox txtOutTime = (TextBox)Gridview3.Rows[i].FindControl("txtOutTime");
                //    theHelper.InsertDocVisit(HiddenField1.Value, ddl.SelectedValue, txtInTime.Text, txtOutTime.Text, Session["CoCode"].ToString());
                //}
                ScriptManager.RegisterStartupScript(this, this.GetType(), "msg", "alert('Updated Successfully !');", true);
                Button1.Text = "Submit";
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "msg", "alert('Error in Updated Data !');", true);
            }
        }

        GridFill();
        ResetAllFields();
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
            Gridview3.DataSource = dt;
            Gridview3.DataBind();
        }
        //Set Previous Data on Postbacks
        SetPreviousData();
    }
    protected void GridView1_Sorting(object sender, GridViewSortEventArgs e)
    {
        //if (e.SortExpression.Trim() == this.SortField)
        //    this.SortDirection = (this.SortDirection == "A" ? "D" : "A");
        //else
        //    this.SortDirection = "D";
        //this.SortField = e.SortExpression;
        //return hospitalTable();
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
                cmd.CommandText = "select distinct doc_name as Name from GN_DoctorMaster where compcode=@Compcode and doc_name like @SearchText +'%'";
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
    protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            if (Objmain.checkaccss(Session["CoCode"].ToString(), Session["UserRoleID"].ToString().Trim(), "DOCTOR MASTER", checkAccessType.DeleteAction) == false)
            {
                coldel.Visible = false;
                e.Row.Cells[24].Visible = false;
            }

        }
    }
    protected void DropDownList2_SelectedIndexChanged(object sender, EventArgs e)
    {
        getstateList();
    }
    private void getstateList()
    {
        this.DropDownList3.DataSource = theHelper.GetCountryState(Session["CoCode"].ToString(), DropDownList2.SelectedValue.Trim());
        this.DropDownList3.DataTextField = "State_Name";
        this.DropDownList3.DataValueField = "State_ID";
        this.DropDownList3.DataBind();
        this.DropDownList3.Items.Insert(0, new ListItem("--Select--", "0"));
    }
}
