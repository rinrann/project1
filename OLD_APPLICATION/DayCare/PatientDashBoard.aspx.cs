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

public partial class DayCare_PatientDashBoard : System.Web.UI.Page
{

    Main Objmain = new Main(ConfigurationSettings.AppSettings["ConnectionString"].ToString());
    DC_AppoinmentList thepatientAppo = new DC_AppoinmentList(ConfigurationSettings.AppSettings["ConnectionString"].ToString());
    protected void Page_Load(object sender, EventArgs e)
    {


        Page.Title = "Patient Dashboard"; 
        if (Session["userName"] == null)
        {
            Response.Redirect("../LoginPage.aspx");
        }
        if (Objmain.checkaccss(Session["CoCode"].ToString(), Session["UserRoleID"].ToString().Trim(), "PATIENT DASHBOARD", checkAccessType.ViewAction) == false)
        {
            Response.Redirect("../AccessDenied.aspx");
        }
        if (!IsPostBack)
        {
            Session["Flag"] = null;
            DropDownFill();
            txtdate.Text = DateTime.Now.ToString("dd/MM/yyyy");

            if (Request.QueryString["Date"] != null || Request.QueryString["shift"] != null || Request.QueryString["Title"] != null || Request.QueryString["NoOfPatient"] != null)
            {
                System.Globalization.DateTimeFormatInfo dtf = new DateTimeFormatInfo();
                string testdate = DateTime.ParseExact(Request.QueryString["Date"], "MM/dd/yyyy", null).ToString("dd/MM/yyyy");
                txtdate.Text = testdate;
                DropDownList1.SelectedIndex = SearchIndexById(Request.QueryString["shift"].ToString(), DropDownList1);

            }
            GridFill();
        }

    }

    protected int SearchIndexById(string Value, DropDownList ddl)
    {
        int i;
        for (i = 0; i < ddl.Items.Count; i++)
        {
            if (ddl.Items[i].Value.Trim() == Value.Trim())
                return i;
        }
        return -1;
    }
    private void ResetAllFields()
    {
        DropDownList1.SelectedIndex = 0;
        txtdate.Text = "";
        txtname.Text = "";
        txtreg.Text = "";
        Button1.Text = "Submit";

    }


    private void DropDownFill()
    {
        this.DropDownList1.DataSource = thepatientAppo.DropdownShift(Session["CoCode"].ToString().Trim());
        this.DropDownList1.DataTextField = "ShiftName";
        this.DropDownList1.DataValueField = "ShiftID";
        this.DropDownList1.DataBind();
        this.DropDownList1.Items.Insert(0, new ListItem("--Select--", "0"));
        DropDownList1.SelectedIndex = 0;
    }

    private void GridFill()
    {
        string testdate;
        string tdate;
        System.Globalization.DateTimeFormatInfo dtf = new DateTimeFormatInfo();
        //if (txtdate.Text != "")
        //    testdate = DateTime.ParseExact(txtdate.Text, "dd/MM/yyyy", dtf).ToString();
        //else
        //    testdate = "";
        //GridView1.DataSource = thepatientAppo.PatientDashBoard(DropDownList1.SelectedValue.ToString(), testdate, txtname.Text.Trim(), txtreg.Text.Trim());
        //GridView1.DataBind();
        if (txtdate.Text != "")
        {
            tdate = txtdate.Text.Substring(6, 4) + "/" + txtdate.Text.Substring(3, 2) + "/" + txtdate.Text.Substring(0, 2);
            testdate = DateTime.ParseExact(tdate, "yyyy/MM/dd",dtf).ToString();
        }
        else
        {
            tdate = "";
            testdate = "";
        }
        GridView1.DataSource = thepatientAppo.PatientDashBoard(Session["CoCode"].ToString().Trim(), DropDownList1.SelectedValue.ToString(), tdate, txtname.Text.Trim(), txtreg.Text.Trim());
        GridView1.DataBind();
    }

    //protected Boolean IsFound()
    //{

    //}

    protected void Button1_Click1(object sender, EventArgs e)
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
        if (e.CommandName == "Confirm")
        {
            //int index = Convert.ToInt32(e.CommandArgument);
            string id = (e.CommandArgument).ToString();
            //Label lblregno = (Label)GridView1.Rows[index].FindControl("lblregno");
            Session["AppNo"] = id;
            //Session["RegnNo"] = lblregno.Text;
            Response.Redirect("../DayCare/PatientReg.aspx");
        }



        if (e.CommandName == "Select")
        {
            int index = Convert.ToInt32(e.CommandArgument); 
            Label lblregno = (Label)GridView1.Rows[index].FindControl("lblregno");
            Label lblname = (Label)GridView1.Rows[index].FindControl("lblname");
            Session["RegnNo"] = lblregno.Text;
            Session["Name"] = lblname.Text;
            Response.Redirect("../DayCare/MedicineAdd.aspx");
        }

           if (e.CommandName == "cancel")
        {
            string id = Convert.ToString(e.CommandArgument);
            thepatientAppo.DeleteAppointment(id);
            lblError.ForeColor = System.Drawing.Color.Green;
            lblError.Text = "Deleted Successfully";
            GridFill();
            ResetAllFields();
        }

        if (e.CommandName == "reschedule")
        {
            string id = Convert.ToString(e.CommandArgument);
            Session["AppNo"] = id;
            Response.Redirect("../DayCare/PatientAppointment.aspx");
        }


        if (e.CommandName == "LabReq")
        {
            string id = Convert.ToString(e.CommandArgument);
            Session["RegNo"] = id;
            Session["Flag"] = "1";
            Response.Redirect("../Pathology/PatientRequisition.aspx");
        }


        if (e.CommandName == "Discharge")
        {
            string id = Convert.ToString(e.CommandArgument);
            Session["RegnNo"] = id;
            Response.Redirect("../Daycare/DCDischarge.aspx");

        }

        
        if (e.CommandName == "Monitoring")
        {
            string id = Convert.ToString(e.CommandArgument);
            Session["AppoDate"] = id;
            Response.Redirect("../Daycare/DialysisMonitoring.aspx");

        }

        
        if (e.CommandName == "Payment")
        {
            string id = Convert.ToString(e.CommandArgument);
            Session["RegnNo"] = id;
            Response.Redirect("../Daycare/DialysisPayment.aspx");

        }

        
        if (e.CommandName == "ChargesDtls")
        {
            string id = Convert.ToString(e.CommandArgument);
            Session["RegnNo"] = id;
            Response.Redirect("../Daycare/ChargeSetails.aspx");
        }



    }
    protected void GridView1_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {

    }
    protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        GridViewRow myRow = e.Row;


        if (myRow.RowType == DataControlRowType.DataRow)
        {
            if (myRow.RowState == DataControlRowState.Normal || myRow.RowState == DataControlRowState.Alternate)
            {
                Label lblregno = (Label)e.Row.FindControl("lblregno");
                Label lblDue = (Label)e.Row.FindControl("lblDue");

                LinkButton LinkButton1 = ((LinkButton)e.Row.FindControl("LinkButton1"));
                LinkButton LinkButton2 = ((LinkButton)e.Row.FindControl("LinkButton2"));
                LinkButton LinkButton3 = ((LinkButton)e.Row.FindControl("LinkButton3"));  
               LinkButton LinkButton4 = ((LinkButton)e.Row.FindControl("LinkButton4"));
                LinkButton LinkButton5 = ((LinkButton)e.Row.FindControl("LinkButton5"));
                LinkButton LinkButton6 = ((LinkButton)e.Row.FindControl("LinkButton6"));
                LinkButton LinkButton7 = ((LinkButton)e.Row.FindControl("LinkButton7")); 
                LinkButton LinkButton8 = ((LinkButton)e.Row.FindControl("LinkButton8"));

                DataTable charge = thepatientAppo.CheckChargeDetails(Session["CoCode"].ToString().Trim(), lblregno.Text);
                DataTable Registration = thepatientAppo.CheckRegistration(Session["CoCode"].ToString().Trim(), lblregno.Text);
                DataTable Monitor = thepatientAppo.CheckMonitor(Session["CoCode"].ToString().Trim(), lblregno.Text);
                DataTable Lab = thepatientAppo.CheckLabDetails(Session["CoCode"].ToString().Trim(), Session["YearCode"].ToString().Trim(), lblregno.Text);

                Label lblSelno = (Label)e.Row.FindControl("lblSlno");
                lblSelno.Text = (e.Row.RowIndex + 1).ToString();

                if (lblregno.Text != "")
                {
                    LinkButton1.Text = "Confirm"; 
                    LinkButton3.Enabled = true;
                    LinkButton2.Enabled = true; 
                    LinkButton4.Enabled = false;
                    LinkButton5.Enabled = false;
                    LinkButton6.Enabled = false; 
                    LinkButton7.Enabled = false;
                    LinkButton8.Enabled = false;
                    e.Row.Cells[12].Enabled = false;

                    if (Lab.Rows.Count > 0)
                    {
                        if (Lab.Rows[0][0].ToString() == "1")
                        {
                            LinkButton5.ForeColor = System.Drawing.Color.Green;
                            LinkButton5.Text = "Done";
                            LinkButton5.Font.Bold = true;
                        }
                        else
                            LinkButton5.ForeColor = System.Drawing.Color.Blue;

                    }

                    
                    


                    if (Registration.Rows.Count > 0)
                    {
                        if (Registration.Rows[0][0].ToString() == "1")
                        {
                            LinkButton1.ForeColor = System.Drawing.Color.Green;
                            LinkButton1.Font.Bold = true;
                            LinkButton1.Text = "Confirmed";
                            LinkButton2.Enabled = false;
                            LinkButton3.Enabled = false;
                            LinkButton4.Enabled = true;
                            LinkButton5.Enabled = true;
                            LinkButton8.Enabled = true;
                            LinkButton7.Enabled = true;
                            e.Row.Cells[12].Enabled = true;
                        }
                        else
                            LinkButton1.ForeColor = System.Drawing.Color.Blue;
                    }


                    if (lblDue.Text == "0")
                    {
                        if (Registration.Rows[0][0].ToString() == "1")
                        {
                            LinkButton8.ForeColor = System.Drawing.Color.Green;
                            LinkButton8.Font.Bold = true;
                            LinkButton6.Enabled = true;
                            LinkButton8.Enabled = false;
                            LinkButton8.Text = "Paid";
                        }
                        else
                        {
                            LinkButton6.Enabled = false;
                            LinkButton8.Enabled = true;
                            LinkButton8.ForeColor = System.Drawing.Color.Blue;
                        }
                    }




                    if (Monitor.Rows.Count > 0)
                    {

                        if (Monitor.Rows[0][0].ToString() == "1")
                        {
                            LinkButton7.ForeColor = System.Drawing.Color.Green;
                            LinkButton7.Font.Bold = true;
                            LinkButton7.Text = "Done";
                        }
                        else
                            LinkButton7.ForeColor = System.Drawing.Color.Blue;

                    }



                    if (charge.Rows.Count > 0)
                    {

                        if (charge.Rows[0][0].ToString() == "1")
                        {
                            LinkButton4.ForeColor = System.Drawing.Color.Green;
                            LinkButton4.Font.Bold = true;
                            LinkButton4.Text = "Done";
                        }
                        else
                            LinkButton4.ForeColor = System.Drawing.Color.Blue;
                    }
                }
                else
                {
                    lblregno.Text = "NEW PATIENT";
                    LinkButton3.Enabled = true;
                    LinkButton2.Enabled = true;
                    LinkButton4.Enabled = false;
                    LinkButton5.Enabled = false;
                    LinkButton6.Enabled = false; 
                    LinkButton7.Enabled = false;
                    LinkButton8.Enabled = false;
                    e.Row.Cells[12].Enabled = false;

                }
            }
        }




        if (e.Row.RowType == DataControlRowType.DataRow)
        { 
            Label lblDueCF = (Label)e.Row.FindControl("lblDueCF");
            string[] duecfsplit = lblDueCF.Text.Split('(');
            if (duecfsplit.Length > 1)
            {
                lblDueCF.Font.Bold = true;
                lblDueCF.ForeColor = System.Drawing.Color.Red;
            } 
        }
    }
}