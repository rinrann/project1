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
using System.Web.Security;

public partial class IPD_AdmissionPatientList : System.Web.UI.Page
{
    Main Objmain = new Main(ConfigurationSettings.AppSettings["ConnectionString"].ToString());
    PatientList thereg = new PatientList(ConfigurationSettings.AppSettings["ConnectionString"].ToString());
    PatientDoctorVisit thedocvisit = new PatientDoctorVisit(ConfigurationSettings.AppSettings["ConnectionString"].ToString());
    static string floor, roomtype;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["userName"] == null)
        {
            Response.Redirect("../LoginPage.aspx");
        }
        if (Objmain.checkaccss(Session["CoCode"].ToString(), Session["UserRoleID"].ToString().Trim(), "PATIENT LIST", checkAccessType.ViewAction) == false)
        {
            Response.Redirect("../AccessDenied.aspx");
        }

        if (!IsPostBack)
        {
            DropDownFill();
            GridFill();
        }
        Page.Title = "Patient Dashboard";
    }

    public void DropDownFill()
    {
        this.DropDownList1.DataSource = thereg.DropdownFloor(Session["CoCode"].ToString().Trim());
        this.DropDownList1.DataTextField = "FloorName";
        this.DropDownList1.DataValueField = "FloorID";
        this.DropDownList1.DataBind();
        this.DropDownList1.Items.Insert(0, new ListItem("--Select--", "0"));

        this.DropDownList4.Items.Insert(0, new ListItem("--Select--", "0"));

        this.DropDownList3.Items.Insert(0, new ListItem("--Select--", "0"));


        this.DropDownList2.DataSource = thereg.Dropdownroomtype(Session["CoCode"].ToString().Trim());
        this.DropDownList2.DataTextField = "RoomCategoryName";
        this.DropDownList2.DataValueField = "RoomCategoryID";
        this.DropDownList2.DataBind();
        this.DropDownList2.Items.Insert(0, new ListItem("--Select--", "0"));
    }

    public void GridFill2(string reg,string ledgerId)
    {
        GridView2.DataSource = thereg.GridPopupDetails(reg, ledgerId, Session["CoCode"].ToString().Trim(),Session["YearCode"].ToString().Trim());
        GridView2.DataBind();
    }
    public void GridFill()
    {
        GridView1.DataSource = thereg.GridPopup(DropDownList1.SelectedValue, DropDownList2.SelectedValue, DropDownList3.SelectedValue, DropDownList4.SelectedValue, txtname.Text.Trim(), Session["CoCode"].ToString().Trim());        
        GridView1.DataBind();
    }
    protected void GridView1_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        GridView1.PageIndex = e.NewPageIndex;
        GridFill();
    }
    protected void Button2_Click(object sender, EventArgs e)
    {
        GridFill();
    }




    protected void DropDownList1_SelectedIndexChanged(object sender, EventArgs e)
    {
        floor = DropDownList1.SelectedValue;
    }

    protected void DropDownList2_SelectedIndexChanged(object sender, EventArgs e)
    {
        roomtype = DropDownList2.SelectedValue;
        this.DropDownList3.DataSource = thereg.Dropdownroom(floor, roomtype, Session["CoCode"].ToString().Trim());
        this.DropDownList3.DataTextField = "RoomName";
        this.DropDownList3.DataValueField = "RoomID";
        this.DropDownList3.DataBind();
        this.DropDownList3.Items.Insert(0, new ListItem("--Select--", "0"));
    }
    protected void DropDownList3_SelectedIndexChanged(object sender, EventArgs e)
    {
        this.DropDownList4.DataSource = thereg.Dropdownbed(floor, roomtype, DropDownList3.SelectedValue, Session["CoCode"].ToString().Trim());
        this.DropDownList4.DataTextField = "BedNoText";
        this.DropDownList4.DataValueField = "BedNo";
        this.DropDownList4.DataBind();
        this.DropDownList4.Items.Insert(0, new ListItem("--Select--", "0"));
    }
    protected void Button1_Click(object sender, EventArgs e)
    {
        GridFill();
    }


    protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "Select")
        {
            string lblregno = (e.CommandArgument).ToString();
            string[] SplitLedgerReg = lblregno.Split('#');
            ScriptManager.RegisterStartupScript(this, this.GetType(), "msg", "showStuff()", true);
            GridFill2(SplitLedgerReg[0], SplitLedgerReg[1]); 
        }
        
        if (e.CommandName == "docvisit")
        {
            string id = (e.CommandArgument).ToString(); 
            Session.Add("RegNo", id);
            Response.Redirect("../IPD/PatientDoctorVisit.aspx");
        }

        if (e.CommandName == "addservices")
        {
            string id = (e.CommandArgument).ToString();
            Session.Add("RegNo", id);
            Response.Redirect("../IPD/AddServices.aspx");
        }

        if (e.CommandName == "Labrequisition")
        {
            string id = (e.CommandArgument).ToString();
            Session.Add("RegNo", id);
            Response.Redirect("../Pathology/PatientRequisition.aspx");
        }

        if (e.CommandName == "addConsumable")
        {
            string id = (e.CommandArgument).ToString();
            Session.Add("RegNo", id);
            Response.Redirect("../IPD/AddConsumable.aspx");
        }

        if (e.CommandName == "dailycheckup")
        {
            string id = (e.CommandArgument).ToString();
            Session.Add("RegNo", id);
            Response.Redirect("../IPD/DailyCheckupRecord.aspx");
        }

        if (e.CommandName == "addmedicine")
        {
            string id = (e.CommandArgument).ToString();
            Session.Add("RegNo", id);
            Response.Redirect("../IPD/AddMedicine.aspx");
        }

        if (e.CommandName == "otreq")
        {
            string id = (e.CommandArgument).ToString();
            Session.Add("RegNo", id);
            Response.Redirect("../IPD/OTRequisition.aspx");
        }

        if (e.CommandName == "BedTransfer")
        {
            string id = (e.CommandArgument).ToString();
            Session.Add("RegNo", id);
            Response.Redirect("../IPD/BedTransfer.aspx");
        }


        if (e.CommandName == "sistecharge")
        {
            string id = (e.CommandArgument).ToString();
            Session.Add("RegNo", id);
            Response.Redirect("../IPD/Sister_Aya_Charges.aspx");
        }

        if (e.CommandName == "ToDoTask")
        {
            string id = (e.CommandArgument).ToString();
            Session["RegNo"]= id;
            Response.Redirect("../IPD/ToDoList.aspx");
        }
        if (e.CommandName == "addinstrument")
        {
            string id = (e.CommandArgument).ToString();
            Session["RegNo"] = id;
            Response.Redirect("../IPD/InstrumentCost.aspx?type=S");
        }

    }

    protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            Label lblregno = (Label)e.Row.FindControl("lblregno");
            Label Bed = (Label)e.Row.FindControl("Label2");
            Label DoctorVisit = (Label)e.Row.FindControl("Label3");
            Label Medicine = (Label)e.Row.FindControl("Label4");
            Label Service = (Label)e.Row.FindControl("Label5");
            Label Consumable = (Label)e.Row.FindControl("Label6");
            Label ClinicalFinding = (Label)e.Row.FindControl("Label7");
            Label SisterAya = (Label)e.Row.FindControl("Label10");
            Label OT = (Label)e.Row.FindControl("Label8");
            Label Lab = (Label)e.Row.FindControl("Label9");
            Label TodoTask = (Label)e.Row.FindControl("Label11");
            Label lblSelno = (Label)e.Row.FindControl("lblSlno");
            Label instrument = (Label)e.Row.FindControl("Label12");
            lblSelno.Text = (e.Row.RowIndex + 1).ToString();

            DataTable dt = thereg.GetPatientBedDtls(lblregno.Text,Session["CoCode"].ToString().Trim());
            if(dt.Rows.Count>0)
            Bed.Text = dt.Rows[0]["Bed"].ToString();

            DataTable dt1 = thereg.GetDocVisitDtls(lblregno.Text,Session["CoCode"].ToString().Trim());
            if (dt1.Rows.Count > 0)
                DoctorVisit.Text = dt1.Rows[0]["Doctorvisit"].ToString();

            DataTable dt2 = thereg.GetMedicineDatetime(lblregno.Text,Session["CoCode"].ToString().Trim());
            if (dt2.Rows.Count > 0)
                Medicine.Text = dt2.Rows[0]["Medicine"].ToString();

            DataTable dt3 = thereg.GetServiceDatetime(lblregno.Text,Session["CoCode"].ToString().Trim());
            DataSet ds = thedocvisit.GetDoctorVisitDetails(Session["CoCode"].ToString().Trim(), Session["YearCode"].ToString().Trim(), lblregno.Text);

            if (dt3.Rows.Count > 0)
            {
                Service.Text = dt3.Rows[0]["Service"].ToString();
            }
            else if (ds.Tables[1].Rows.Count > 0)
            {
                Service.Text = ds.Tables[1].Rows[0]["Date1"].ToString();
            }
            else
            {
                Service.Text = "";
            }

            DataTable dt4 = thereg.GetConsumableDatetime(lblregno.Text,Session["CoCode"].ToString().Trim());
            if (dt4.Rows.Count > 0)
            {
                Consumable.Text = dt4.Rows[0]["Consumable"].ToString();
            }
            else if (ds.Tables[2].Rows.Count > 0)
            {
                Consumable.Text = ds.Tables[2].Rows[0]["Date1"].ToString();
            }
            else
            {
                Consumable.Text = "";
            }

            DataTable dt5 = thereg.GetClinicalFindingDatetime(lblregno.Text,Session["CoCode"].ToString().Trim());
            if (dt5.Rows.Count > 0)
                ClinicalFinding.Text = dt5.Rows[0]["ClinicalFinding"].ToString();

            DataTable dt6 = thereg.GetSisterAyaDatetime(lblregno.Text,Session["CoCode"].ToString().Trim());
            if (dt6.Rows.Count > 0)
                SisterAya.Text = dt6.Rows[0]["SisterAya"].ToString();

            DataTable dt7 = thereg.GetOperationDatetime(lblregno.Text,Session["CoCode"].ToString().Trim(),Session["YearCode"].ToString().Trim());
            if (dt7.Rows.Count > 0)
            {
                if (dt7.Rows[0]["OT"].ToString() == "Rejected")
                {
                    OT.Text = dt7.Rows[0]["OT"].ToString();
                    OT.ForeColor = Color.Red;
                }
                else
                {
                    OT.Text = dt7.Rows[0]["OT"].ToString();
                    OT.ForeColor = Color.Green;
                }
            }

            DataTable dt8 = thereg.GetLabDatetime(lblregno.Text,Session["CoCode"].ToString().Trim());
            if (dt8.Rows.Count > 0)
                Lab.Text = dt8.Rows[0]["Lab"].ToString();


            if (thereg.GetToDoTask(lblregno.Text, Session["CoCode"].ToString().Trim()) > 0)
                TodoTask.Text ="Done";

            DataTable td9 = thereg.GetInsDetls(lblregno.Text, Session["CoCode"].ToString().Trim(), Session["YearCode"].ToString().Trim(),"S");
            if (td9.Rows.Count > 0)
            {
                instrument.Text = "Added";
            }
        }
    }

    [System.Web.Script.Services.ScriptMethod()]
    [System.Web.Services.WebMethod]
    public static List<string> SearchPatientName(string prefixText, int count)
    {
        using (SqlConnection conn = new SqlConnection())
        {
            conn.ConnectionString = ConfigurationSettings.AppSettings["ConnectionString"].ToString();
            using (SqlCommand cmd = new SqlCommand())
            {
                cmd.CommandText = "select distinct patient_name + '-' + PatientReg +'-'+ case when husbandname is null then '' else husbandname end +'-'+Vill_city as Name from GN_PatientReg where patient_name like @SearchText + '%'";
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
    
}