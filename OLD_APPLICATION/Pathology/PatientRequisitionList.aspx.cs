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
 
public partial class Pathology_PatientRequisitionList : System.Web.UI.Page
{
    Main Objmain = new Main(ConfigurationSettings.AppSettings["ConnectionString"].ToString());
    PH_PatientRequisitionList thepatientAppo = new PH_PatientRequisitionList(ConfigurationSettings.AppSettings["ConnectionString"].ToString());
    Discharge thedischarge = new Discharge(ConfigurationSettings.AppSettings["ConnectionString"].ToString());
    protected void Page_Load(object sender, EventArgs e)
    {

        if (Session["userId"] == null)
        {
            Response.Redirect("../LoginPage.aspx");
        }
        if (Objmain.checkaccss(Session["CoCode"].ToString(), Session["UserRoleID"].ToString().Trim(), "REQUISITION LIST", checkAccessType.ViewAction) == false)
        {
            Response.Redirect("../AccessDenied.aspx");
        }

        Page.Title = "Requisition List";
        if (Session["userId"] == null)
        {
            Response.Redirect("../LoginPage.aspx");
        }
        if (!IsPostBack)
        {
            txtdate.Text=DateTime.Now.ToString("yyyy-MM-dd");
            GridFill();
        }
    }
    //protected int SearchIndexById(string Value, DropDownList ddl)
    //{
    //    int i;
    //    for (i = 0; i < ddl.Items.Count; i++)
    //    {
    //        if (ddl.Items[i].Value.Trim() == Value.Trim())
    //            return i;
    //    }
    //    return -1;
    //}

    [System.Web.Script.Services.ScriptMethod()]
    [System.Web.Services.WebMethod]
    public static List<string> SearchDoctorName(string prefixText, int count)
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
                cmd.CommandText = "select doc_id + '~' + doc_name as Name from GN_DoctorMaster where compcode=@Compcode and doc_name like @SearchText + '%'";
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
    private void ResetAllFields()
    {
        //DropDownList1.SelectedIndex = 0;
        txtdate.Text = "";
        txtDocName.Text = "";
        txtDocId.Text = "";
        Button1.Text = "Submit";

    }


    //private void DropDownFill()
    //{
    //    this.DropDownList1.DataSource = thepatientAppo.DropdownShift(Session["CoCode"].ToString().Trim());
    //    this.DropDownList1.DataTextField = "TestName";
    //    this.DropDownList1.DataValueField = "TestId";
    //    this.DropDownList1.DataBind();
    //    this.DropDownList1.Items.Insert(0, new ListItem("--Select--", "0"));
    //}

    private void GridFill()
    {
        //DateTime testdate;
         //System.Globalization.DateTimeFormatInfo dtf = new DateTimeFormatInfo();
        //if (txtdate.Text != "")
        //{
        //    testdate = DateTime.ParseExact(txtdate.Text, "dd/MM/yyyy", dtf);
        //    GridView1.DataSource = thepatientAppo.GridRequisition(Session["CoCode"].ToString().Trim(), Session["YearCode"].ToString().Trim(),DropDownList1.SelectedValue, testdate.ToString(), txtname.Text, txtreg.Text);
        //}
        //else
        //    GridView1.DataSource = thepatientAppo.GridRequisition(Session["CoCode"].ToString().Trim(), Session["YearCode"].ToString().Trim(),DropDownList1.SelectedValue, txtdate.Text, txtname.Text, txtreg.Text);
        if (txtDocName.Text == "")
        {
            txtDocId.Text = "";
        }
        GridView1.DataSource = thepatientAppo.GridRequisition(Session["CoCode"].ToString().Trim(), Session["YearCode"].ToString().Trim(), txtDocId.Text.Trim(), txtdate.Text.ToString().Trim(),txtpname.Text);
        GridView1.DataBind();
    }
   
 
    protected void Button1_Click(object sender, EventArgs e)
    {
        GridFill();
    }
    protected void GridView1_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        GridView1.PageIndex = e.NewPageIndex;
        GridFill();
    }
    protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        
          if (e.CommandName == "select")
        {

            string id = (e.CommandArgument).ToString();
            DataTable thetable = thepatientAppo.rescheduleList(Session["CoCode"].ToString().Trim(), Session["YearCode"].ToString().Trim(), id);
            Response.Redirect("../Pathology/PatientRegistration.aspx?name=" + thetable.Rows[0][2] + "&address=" + thetable.Rows[0][4] + "&phone1=" + thetable.Rows[0][5] + "&phone2=" + thetable.Rows[0][6] + "&advamt=" + thetable.Rows[0][9] + "&req=" + thetable.Rows[0][0] + "&Due=" + thetable.Rows[0][13] + "");
        }

          if (e.CommandName == "cancel")
          {
              string id = Convert.ToString(e.CommandArgument);
              thepatientAppo.DeleteAppointment(Session["CoCode"].ToString().Trim(), id);
              lblError.ForeColor = System.Drawing.Color.Green;
              lblError.Text = "Deleted Successfully";
              GridFill();
              ResetAllFields();
          }
        if (e.CommandName == "reschedule")
        {
            int index = Convert.ToInt32(e.CommandArgument);
            //string id = Convert.ToString(e.CommandArgument);
            Label reqno = (Label)GridView1.Rows[index].FindControl("lblID");
            Label lblregno = (Label)GridView1.Rows[index].FindControl("lblregno");
            Label lblType = (Label)GridView1.Rows[index].FindControl("lblType");
            DataSet ds = thedischarge.PatientForReport(lblregno.Text, Session["CoCode"].ToString().Trim());
            DataTable thetable = thepatientAppo.rescheduleList(Session["CoCode"].ToString().Trim(), Session["YearCode"].ToString().Trim(), reqno.Text);
            DataTable dt1 = thepatientAppo.GetRequisitionDtls(Session["CoCode"].ToString().Trim(), Session["YearCode"].ToString().Trim(), reqno.Text, ds.Tables[0].Rows[0]["LedgerId"].ToString());
            Session.Add("RegNo", lblregno.Text.Trim());
            //Response.Redirect("../Pathology/PatientRequisition.aspx?req=" + thetable.Rows[0][4] + "&reg=" + thetable.Rows[0][5] + "&name=" + thetable.Rows[0][6] + "&ref=" + thetable.Rows[0][7] + "&address=" + thetable.Rows[0][8] + "&phone1=" + thetable.Rows[0][9] + "&phone2=" + thetable.Rows[0][10] + "&testcode=" + dt1.Rows[0][0] + "&testname=" + dt1.Rows[0][5] + "&testcost=" + dt1.Rows[0][6] + "&testdate=" + dt1.Rows[0][8] + "&deldate=" + dt1.Rows[0][9] + "&advamt=" + dt1.Rows[0][10] + "");
            Session["RegNo"] = lblregno.Text.Trim();
            Session["BillType"] = lblType.Text;
            Session["ReqNo"] = reqno.Text;
            if(lblType.Text=="OPD")
            {
                Response.Redirect("../Pathology/PatientRequisitionOPD.aspx");
            }
            else
            {
                Response.Redirect("../Pathology/PatientRequisition.aspx");
            }
        }
        if (e.CommandName == "PerfDoc")
        {
            int index = Convert.ToInt32(e.CommandArgument);
            Label reqno = (Label)GridView1.Rows[index].FindControl("lblID");
            Label lblregno = (Label)GridView1.Rows[index].FindControl("lblregno");
            Label lblname = (Label)GridView1.Rows[index].FindControl("lblname");
            Session["TestReqNo"] = reqno.Text.Trim();
            Session["TestRegNo"] = lblregno.Text.Trim();
            Session["TestPtName"] = lblname.Text;
            ScriptManager.RegisterStartupScript(this, GetType(), "ShowDialog1", "ShowDialog1();", true);
        }
    }

    protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        GridViewRow myRow = e.Row;
        if (myRow.RowType == DataControlRowType.DataRow)
        {
            if (myRow.RowState == DataControlRowState.Normal || myRow.RowState == DataControlRowState.Alternate)
            {
                Button btn = ((Button)e.Row.FindControl("Button2"));
                Label lblregno = (Label)e.Row.FindControl("lblregno");
                Label lblReqType = (Label)e.Row.FindControl("lblReqType");//14
                if (lblregno.Text != "")
                {

                    btn.Enabled = false;
                }
                else
                {
                    btn.Enabled = true;
                }
                if (lblReqType.Text == "OPD")
                {
                    e.Row.Cells[14].Enabled = false;
                }
                else
                {
                    e.Row.Cells[14].Enabled = true;
                }
            }
        }
    }
    protected void btn_save_Click(object sender, EventArgs e)
    {
        if (GridView1.Rows.Count > 0)
        {
            for (int i = 0; i < GridView1.Rows.Count; i++)
            {
                Label lblID = (Label)GridView1.Rows[i].FindControl("lblID");
                CheckBox chkdone = (CheckBox)GridView1.Rows[i].FindControl("chkdone");
                if (chkdone.Checked == true)
                {
                    thepatientAppo.UpdateRequisitionStatus(Session["CoCode"].ToString().Trim(), Session["YearCode"].ToString().Trim(), Session["userId"].ToString(), lblID.Text.Trim());
                }
            }
        }
        GridFill();
    }
    
}