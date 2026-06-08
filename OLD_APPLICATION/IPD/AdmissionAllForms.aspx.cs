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

public partial class IPD_AdmissionAllForms : System.Web.UI.Page
{
    AdmissionAllForms thereg = new  AdmissionAllForms(ConfigurationSettings.AppSettings["ConnectionString"].ToString());
    Discharge thepatientbill = new Discharge(ConfigurationSettings.AppSettings["ConnectionString"].ToString());
    System.Text.StringBuilder rpt = new System.Text.StringBuilder();
    String Date = DateTime.Now.ToString("dd/MM/yyyy");
 
    static string floor, roomtype;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["userName"] == null)
        {
            Response.Redirect("../LoginPage.aspx");
        }

        if (!IsPostBack)
        {
            GridFill();
            DropDownFill();
            Panel1.Visible = false;
            RadioButtonList1.SelectedValue = "With Header";
        }
        Page.Title = "Admission All Forms";
    }

    public void DropDownFill()
    {
        this.DropDownList1.DataSource = thereg.DropdownFloor();
        this.DropDownList1.DataTextField = "FloorName";
        this.DropDownList1.DataValueField = "FloorID";
        this.DropDownList1.DataBind();
        this.DropDownList1.Items.Insert(0, new ListItem("--Select--", "0"));

        this.DropDownList4.Items.Insert(0, new ListItem("--Select--", "0"));

        this.DropDownList3.Items.Insert(0, new ListItem("--Select--", "0"));


        this.DropDownList2.DataSource = thereg.Dropdownroomtype();
        this.DropDownList2.DataTextField = "RoomCategoryName";
        this.DropDownList2.DataValueField = "RoomCategoryID";
        this.DropDownList2.DataBind();
        this.DropDownList2.Items.Insert(0, new ListItem("--Select--", "0"));
    }




    protected void DropDownList1_SelectedIndexChanged(object sender, EventArgs e)
    {
        floor = DropDownList1.SelectedValue;
    }

    protected void DropDownList2_SelectedIndexChanged(object sender, EventArgs e)
    {
        roomtype = DropDownList2.SelectedValue;
        this.DropDownList3.DataSource = thereg.Dropdownroom(floor, roomtype);
        this.DropDownList3.DataTextField = "RoomName";
        this.DropDownList3.DataValueField = "RoomID";
        this.DropDownList3.DataBind();
        this.DropDownList3.Items.Insert(0, new ListItem("--Select--", "0"));
    }
    protected void DropDownList3_SelectedIndexChanged(object sender, EventArgs e)
    {
        this.DropDownList4.DataSource = thereg.Dropdownbed(floor, roomtype, DropDownList3.SelectedValue);
        this.DropDownList4.DataTextField = "BedNoText";
        this.DropDownList4.DataValueField = "BedNo";
        this.DropDownList4.DataBind();
        this.DropDownList4.Items.Insert(0, new ListItem("--Select--", "0"));
    }
    public void GridFill()
    {
        GridView1.DataSource = thereg.GridPopup(DropDownList1.SelectedValue, DropDownList2.SelectedValue, DropDownList3.SelectedValue, DropDownList4.SelectedValue, txtname.Text.Trim());
        GridView1.DataBind();
    }
    protected void GridView1_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        GridView1.PageIndex = e.NewPageIndex;
        GridFill();
    }
    protected void Button1_Click(object sender, EventArgs e)
    {
        GridFill();
    }
    protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "Select")
        {
            if (DropDownList5.SelectedIndex == 0)
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "msg", "Calling();", true);
            }
            else
            {
                int index = Convert.ToInt32(e.CommandArgument);
                Label lblregno = (Label)GridView1.Rows[index].FindControl("lblregno");
                CheckBox CheckBox1 = (CheckBox)GridView1.Rows[index].FindControl("CheckBox1");
                CheckBox CheckBox2 = (CheckBox)GridView1.Rows[index].FindControl("CheckBox2");
                CheckBox CheckBox3 = (CheckBox)GridView1.Rows[index].FindControl("CheckBox3");
                CheckBox CheckBox4 = (CheckBox)GridView1.Rows[index].FindControl("CheckBox4");
                CheckBox CheckBox5 = (CheckBox)GridView1.Rows[index].FindControl("CheckBox5");
                DropDownList DropDownList6 = (DropDownList)GridView1.Rows[index].FindControl("DropDownList6");

               

                if (RadioButtonList1.SelectedValue == "With Header")
                {
                    Panel1.Visible = true;
             
                    if (DropDownList5.SelectedIndex == 2)
                    {
                        if (CheckBox5.Checked)
                        {
                            if (rpt.ToString() != "")
                                rpt.Append("<div class='break'></div>");
                            Report_Header();
                            GetHearder_AdmissionSlip(lblregno.Text); 
                        }
                        if (CheckBox1.Checked)
                        {
                            if (rpt.ToString() != "")
                                rpt.Append("<div class='break'></div>");
                            Report_Header();
                            GetHearder_AdmissionFrom1(lblregno.Text);
                        }
                        if (CheckBox2.Checked)
                        {
                            if (rpt.ToString() != "")
                                rpt.Append("<div class='break'></div>");
                            Report_Header();
                            GetHearder_OperationConsent1(lblregno.Text);
                        }
                        if (CheckBox3.Checked)
                        {
                            if (rpt.ToString() != "")
                                rpt.Append("<div class='break'></div>");
                            Report_Header();
                            GetHearder_Aborsion1(lblregno.Text);
                        }
                        if (CheckBox4.Checked)
                        {
                            if (rpt.ToString() != "")
                                rpt.Append("<div class='break'></div>");
                            Report_Header();
                            GetHearder_Ligation1(lblregno.Text);
                        }

                        if (DropDownList6.SelectedValue == "1")
                        {
                            if (rpt.ToString() != "")
                                rpt.Append("<div class='break'></div>");
                            Report_Header();
                            GetHearder_APHReport1(lblregno.Text);
                        }
                        if (DropDownList6.SelectedValue == "2")
                        {
                            if (rpt.ToString() != "")
                                rpt.Append("<div class='break'></div>");
                            Report_Header();
                            GetHearder_BadOrtho1(lblregno.Text);
                        }
                        if (DropDownList6.SelectedValue == "3")
                        {
                            if (rpt.ToString() != "")
                                rpt.Append("<div class='break'></div>");
                            Report_Header();
                            GetHearder_BroChopReport1(lblregno.Text);
                        }
                        if (DropDownList6.SelectedValue == "4")
                        {
                            if (rpt.ToString() != "")
                                rpt.Append("<div class='break'></div>");
                            Report_Header();
                            GetHearder_Eclamsia1(lblregno.Text);
                        }
                        if (DropDownList6.SelectedValue == "5")
                        {
                            if (rpt.ToString() != "")
                                rpt.Append("<div class='break'></div>");
                            Report_Header();
                            GetHearder_EctopicPregnency1(lblregno.Text);
                        }
                        if (DropDownList6.SelectedValue == "6")
                        {
                            if (rpt.ToString() != "")
                                rpt.Append("<div class='break'></div>");
                            Report_Header();
                            GetHearder_HighRisk1(lblregno.Text);
                        }
                        if (DropDownList6.SelectedValue == "7")
                        {
                            if (rpt.ToString() != "")
                                rpt.Append("<div class='break'></div>");
                            Report_Header();
                            GetHearder_NeonatalConvulsion1(lblregno.Text);
                        }
                        if (DropDownList6.SelectedValue == "8")
                        {
                            if (rpt.ToString() != "")
                                rpt.Append("<div class='break'></div>");
                            Report_Header();
                            GetHearder_NeonatalHighFeverReport1(lblregno.Text);
                        }
                        if (DropDownList6.SelectedValue == "9")
                        {
                            if (rpt.ToString() != "")
                                rpt.Append("<div class='break'></div>");
                            Report_Header();
                            GetHearder_NeonatalJaundiceReport1(lblregno.Text);
                        }
                        if (DropDownList6.SelectedValue == "10")
                        {
                            if (rpt.ToString() != "")
                                rpt.Append("<div class='break'></div>");
                            Report_Header();
                            GetHearder_NeoNatalphysia1(lblregno.Text);
                        }
                        if (DropDownList6.SelectedValue == "11")
                        {
                            if (rpt.ToString() != "")
                                rpt.Append("<div class='break'></div>");
                            Report_Header();
                            GetHearder_OperationConsentReport1(lblregno.Text);
                        }
                        if (DropDownList6.SelectedValue == "12")
                        {
                            if (rpt.ToString() != "")
                                rpt.Append("<div class='break'></div>");
                            Report_Header();
                            GetHearder_OperativeriskOldman1(lblregno.Text);
                        }
                        if (DropDownList6.SelectedValue == "13")
                        {
                            if (rpt.ToString() != "")
                                rpt.Append("<div class='break'></div>");
                            Report_Header();
                            GetHearder_PIHReport1(lblregno.Text);
                        }
                        if (DropDownList6.SelectedValue == "14")
                        {
                            if (rpt.ToString() != "")
                                rpt.Append("<div class='break'></div>");
                            Report_Header();
                            GetHearder_PoisionIntake1(lblregno.Text);
                        }
                        if (DropDownList6.SelectedValue == "15")
                        {
                            if (rpt.ToString() != "")
                                rpt.Append("<div class='break'></div>");
                            Report_Header();
                            GetHearder_PostOprative1(lblregno.Text);
                        }
                        if (DropDownList6.SelectedValue == "16")
                        {
                            if (rpt.ToString() != "")
                                rpt.Append("<div class='break'></div>");
                            Report_Header();
                            GetHearder_PPHReport1(lblregno.Text);
                        }
                        if (DropDownList6.SelectedValue == "17")
                        {
                            if (rpt.ToString() != "")
                                rpt.Append("<div class='break'></div>");
                            Report_Header();
                            GetHearder_PrematureBaby1(lblregno.Text);
                        }
                        if (DropDownList6.SelectedValue == "18")
                        {
                            if (rpt.ToString() != "")
                                rpt.Append("<div class='break'></div>");
                            Report_Header();
                            GetHearder_Septiceamia1(lblregno.Text);
                        }
                        if (DropDownList6.SelectedValue == "19")
                        {
                            if (rpt.ToString() != "")
                                rpt.Append("<div class='break'></div>");
                            Report_Header();
                            GetHearder_ProlongAnesthecia1(lblregno.Text);
                        }
                        if (DropDownList6.SelectedValue == "20")
                        {
                            if (rpt.ToString() != "")
                                rpt.Append("<div class='break'></div>");
                            Report_Header();
                            GetHearder_SevererAnemiaREport1(lblregno.Text);
                        }
                        if (DropDownList6.SelectedValue == "21")
                        {
                            if (rpt.ToString() != "")
                                rpt.Append("<div class='break'></div>");
                            Report_Header();
                            GetHearder_StrokeReport1(lblregno.Text);
                        }
         
                       
                    }
                    else
                    {
                        if (CheckBox5.Checked)
                        {
                            if (rpt.ToString() != "")
                                rpt.Append("<div class='break'></div>");
                            Report_Header();
                            GetHearder_AdmissionSlip(lblregno.Text);
                        }
                        if (CheckBox1.Checked)
                        {
                            if (rpt.ToString() != "")
                                rpt.Append("<div class='break'></div>");
                            Report_Header();
                            GetHearder_AdmissionFrom(lblregno.Text);
                        }
                        if (CheckBox2.Checked)
                        {
                            if (rpt.ToString() != "")
                                rpt.Append("<div class='break'></div>");

                            Report_Header();
                            GetHearder_OperationConsent(lblregno.Text);
                        }
                        if (CheckBox3.Checked)
                        {
                            if (rpt.ToString() != "")
                                rpt.Append("<div class='break'></div>");

                            Report_Header();
                            GetHearder_Aborsion(lblregno.Text);
                        }
                        if (CheckBox4.Checked)
                        {
                            if (rpt.ToString() != "")
                                rpt.Append("<div class='break'></div>");
                            Report_Header();
                            GetHearder_Ligation(lblregno.Text);
                        }

                        if (DropDownList6.SelectedValue == "1")
                        {
                            if (rpt.ToString() != "")
                                rpt.Append("<div class='break'></div>");
                            Report_Header();
                            GetHearder_APHReport(lblregno.Text);
                        }
                        if (DropDownList6.SelectedValue == "2")
                        {
                            if (rpt.ToString() != "")
                                rpt.Append("<div class='break'></div>");
                            Report_Header();
                            GetHearder_BadOrtho(lblregno.Text);
                        }
                        if (DropDownList6.SelectedValue == "3")
                        {
                            if (rpt.ToString() != "")
                                rpt.Append("<div class='break'></div>");
                            Report_Header();
                            GetHearder_BroChopReport(lblregno.Text);
                        }
                        if (DropDownList6.SelectedValue == "4")
                        {
                            if (rpt.ToString() != "")
                                rpt.Append("<div class='break'></div>");
                            Report_Header();
                            GetHearder_Eclamsia(lblregno.Text);
                        }
                        if (DropDownList6.SelectedValue == "5")
                        {
                            if (rpt.ToString() != "")
                                rpt.Append("<div class='break'></div>");
                            Report_Header();
                            GetHearder_EctopicPregnency(lblregno.Text);
                        }
                        if (DropDownList6.SelectedValue == "6")
                        {
                            if (rpt.ToString() != "")
                                rpt.Append("<div class='break'></div>");
                            Report_Header();
                            GetHearder_HighRisk(lblregno.Text);
                        }
                        if (DropDownList6.SelectedValue == "7")
                        {
                            if (rpt.ToString() != "")
                                rpt.Append("<div class='break'></div>");
                            Report_Header();
                            GetHearder_NeonatalConvulsion(lblregno.Text);
                        }
                        if (DropDownList6.SelectedValue == "8")
                        {
                            if (rpt.ToString() != "")
                                rpt.Append("<div class='break'></div>");
                            Report_Header();
                            GetHearder_NeonatalHighFeverReport(lblregno.Text);
                        }
                        if (DropDownList6.SelectedValue == "9")
                        {
                            if (rpt.ToString() != "")
                                rpt.Append("<div class='break'></div>");
                            Report_Header();
                            GetHearder_NeonatalJaundiceReport(lblregno.Text);
                        }
                        if (DropDownList6.SelectedValue == "10")
                        {
                            if (rpt.ToString() != "")
                                rpt.Append("<div class='break'></div>");
                            Report_Header();
                            GetHearder_NeoNatalphysia(lblregno.Text);
                        }
                        if (DropDownList6.SelectedValue == "11")
                        {
                            if (rpt.ToString() != "")
                                rpt.Append("<div class='break'></div>");
                            Report_Header();
                            GetHearder_OperationConsentReport(lblregno.Text);
                        }
                        if (DropDownList6.SelectedValue == "12")
                        {
                            if (rpt.ToString() != "")
                                rpt.Append("<div class='break'></div>");
                            Report_Header();
                            GetHearder_OperativeriskOldman(lblregno.Text);
                        }
                        if (DropDownList6.SelectedValue == "13")
                        {
                            if (rpt.ToString() != "")
                                rpt.Append("<div class='break'></div>");
                            Report_Header();
                            GetHearder_PIHReport(lblregno.Text);
                        }
                        if (DropDownList6.SelectedValue == "14")
                        {
                            if (rpt.ToString() != "")
                                rpt.Append("<div class='break'></div>");
                            Report_Header();
                            GetHearder_PoisionIntake(lblregno.Text);
                        }
                        if (DropDownList6.SelectedValue == "15")
                        {
                            if (rpt.ToString() != "")
                                rpt.Append("<div class='break'></div>");
                            Report_Header();
                            GetHearder_PostOprative(lblregno.Text);
                        }
                        if (DropDownList6.SelectedValue == "16")
                        {
                            if (rpt.ToString() != "")
                                rpt.Append("<div class='break'></div>");
                            Report_Header();
                            GetHearder_PPHReport(lblregno.Text);
                        }
                        if (DropDownList6.SelectedValue == "17")
                        {
                            if (rpt.ToString() != "")
                                rpt.Append("<div class='break'></div>");
                            Report_Header();
                            GetHearder_PrematureBaby(lblregno.Text);
                        }
                        if (DropDownList6.SelectedValue == "18")
                        {
                            if (rpt.ToString() != "")
                                rpt.Append("<div class='break'></div>");
                            Report_Header();
                            GetHearder_Septiceamia(lblregno.Text);
                        }
                        if (DropDownList6.SelectedValue == "19")
                        {
                            if (rpt.ToString() != "")
                                rpt.Append("<div class='break'></div>");
                            Report_Header();
                            GetHearder_ProlongAnesthecia(lblregno.Text);
                        }
                        if (DropDownList6.SelectedValue == "20")
                        {
                            if (rpt.ToString() != "")
                                rpt.Append("<div class='break'></div>");
                            Report_Header();
                            GetHearder_SevererAnemiaREport(lblregno.Text);
                        }
                        if (DropDownList6.SelectedValue == "21")
                        {
                            if (rpt.ToString() != "")
                                rpt.Append("<div class='break'></div>");
                            Report_Header();
                            GetHearder_StrokeReport(lblregno.Text);
                        }
           
                    }
                }
                else
                {
                    if (DropDownList5.SelectedIndex == 2)
                    {                       
                        if (CheckBox5.Checked)
                            GetHearder_AdmissionSlip(lblregno.Text);

                        if (CheckBox1.Checked)
                        {
                            if (rpt.ToString() != "")
                                rpt.Append("<div class='break'></div>");
                            GetHearder_AdmissionFrom1(lblregno.Text);
                        }
                        if (CheckBox2.Checked)
                        {
                            if (rpt.ToString() != "")
                                rpt.Append("<div class='break'></div>");
                            GetHearder_OperationConsent1(lblregno.Text);
                        }
                        if (CheckBox3.Checked)
                        {
                            if (rpt.ToString() != "")
                                rpt.Append("<div class='break'></div>");
                            GetHearder_Aborsion1(lblregno.Text);
                        }
                        if (CheckBox4.Checked)
                        {
                            if (rpt.ToString() != "")
                                rpt.Append("<div class='break'></div>");
                            GetHearder_Ligation1(lblregno.Text);
                        }

                        if (DropDownList6.SelectedValue == "1")
                        {
                            if (rpt.ToString() != "")
                                rpt.Append("<div class='break'></div>");
                            GetHearder_APHReport1(lblregno.Text);
                        }
                        if (DropDownList6.SelectedValue == "2")
                        {
                            if (rpt.ToString() != "")
                                rpt.Append("<div class='break'></div>");
                            GetHearder_BadOrtho1(lblregno.Text);
                        }
                        if (DropDownList6.SelectedValue == "3")
                        {
                            if (rpt.ToString() != "")
                                rpt.Append("<div class='break'></div>");
                            GetHearder_BroChopReport1(lblregno.Text);
                        }
                        if (DropDownList6.SelectedValue == "4")
                        {
                            if (rpt.ToString() != "")
                                rpt.Append("<div class='break'></div>");
                            GetHearder_Eclamsia1(lblregno.Text);
                        }
                        if (DropDownList6.SelectedValue == "5")
                        {
                            if (rpt.ToString() != "")
                                rpt.Append("<div class='break'></div>");
                            GetHearder_EctopicPregnency1(lblregno.Text);
                        }
                        if (DropDownList6.SelectedValue == "6")
                        {
                            if (rpt.ToString() != "")
                                rpt.Append("<div class='break'></div>");
                            GetHearder_HighRisk1(lblregno.Text);
                        }
                        if (DropDownList6.SelectedValue == "7")
                        {
                            if (rpt.ToString() != "")
                                rpt.Append("<div class='break'></div>");
                            GetHearder_NeonatalConvulsion1(lblregno.Text);
                        }
                        if (DropDownList6.SelectedValue == "8")
                        {
                            if (rpt.ToString() != "")
                                rpt.Append("<div class='break'></div>");
                            GetHearder_NeonatalHighFeverReport1(lblregno.Text);
                        }
                        if (DropDownList6.SelectedValue == "9")
                        {
                            if (rpt.ToString() != "")
                                rpt.Append("<div class='break'></div>");
                            GetHearder_NeonatalJaundiceReport1(lblregno.Text);
                        }
                        if (DropDownList6.SelectedValue == "10")
                        {
                            if (rpt.ToString() != "")
                                rpt.Append("<div class='break'></div>");
                            GetHearder_NeoNatalphysia1(lblregno.Text);
                        }
                        if (DropDownList6.SelectedValue == "11")
                        {
                            if (rpt.ToString() != "")
                                rpt.Append("<div class='break'></div>");
                            GetHearder_OperationConsentReport1(lblregno.Text);
                        }
                        if (DropDownList6.SelectedValue == "12")
                        {
                            if (rpt.ToString() != "")
                                rpt.Append("<div class='break'></div>");
                            GetHearder_OperativeriskOldman1(lblregno.Text);
                        }
                        if (DropDownList6.SelectedValue == "13")
                        {
                            if (rpt.ToString() != "")
                                rpt.Append("<div class='break'></div>");
                            GetHearder_PIHReport1(lblregno.Text);
                        }
                        if (DropDownList6.SelectedValue == "14")
                        {
                            if (rpt.ToString() != "")
                                rpt.Append("<div class='break'></div>");
                            GetHearder_PoisionIntake1(lblregno.Text);
                        }
                        if (DropDownList6.SelectedValue == "15")
                        {
                            if (rpt.ToString() != "")
                                rpt.Append("<div class='break'></div>");
                            GetHearder_PostOprative1(lblregno.Text);
                        }
                        if (DropDownList6.SelectedValue == "16")
                        {
                            if (rpt.ToString() != "")
                                rpt.Append("<div class='break'></div>");
                            GetHearder_PPHReport1(lblregno.Text);
                        }
                        if (DropDownList6.SelectedValue == "17")
                        {
                            if (rpt.ToString() != "")
                                rpt.Append("<div class='break'></div>");
                            GetHearder_PrematureBaby1(lblregno.Text);
                        }
                        if (DropDownList6.SelectedValue == "18")
                        {
                            if (rpt.ToString() != "")
                                rpt.Append("<div class='break'></div>");
                            GetHearder_Septiceamia1(lblregno.Text);
                        }
                        if (DropDownList6.SelectedValue == "19")
                            GetHearder_ProlongAnesthecia1(lblregno.Text);
                        if (DropDownList6.SelectedValue == "20")
                        {
                            if (rpt.ToString() != "")
                                rpt.Append("<div class='break'></div>");
                            GetHearder_SevererAnemiaREport1(lblregno.Text);
                        }
                        if (DropDownList6.SelectedValue == "21")
                        {
                            if (rpt.ToString() != "")
                                rpt.Append("<div class='break'></div>");
                            GetHearder_StrokeReport1(lblregno.Text);
                        }


                    }
                    else
                    {
                        if (CheckBox5.Checked)
                        {
                            if (rpt.ToString() != "")
                                rpt.Append("<div class='break'></div>");
                            GetHearder_AdmissionSlip(lblregno.Text);
                        }
                        if (CheckBox1.Checked)
                        {
                            if (rpt.ToString() != "")
                                rpt.Append("<div class='break'></div>");
                            GetHearder_AdmissionFrom(lblregno.Text);
                        }
                        if (CheckBox2.Checked)
                        {
                            if (rpt.ToString() != "")
                                rpt.Append("<div class='break'></div>");
                            GetHearder_OperationConsent(lblregno.Text);
                        }
                        if (CheckBox3.Checked)
                        {
                            if (rpt.ToString() != "")
                                rpt.Append("<div class='break'></div>");
                            GetHearder_Aborsion(lblregno.Text);
                        }
                        if (CheckBox4.Checked)
                        {
                            if (rpt.ToString() != "")
                                rpt.Append("<div class='break'></div>");
                            GetHearder_Ligation(lblregno.Text);
                        }



                        if (DropDownList6.SelectedValue == "1")
                        {
                            if (rpt.ToString() != "")
                                rpt.Append("<div class='break'></div>");
                            GetHearder_APHReport(lblregno.Text);
                        }
                        if (DropDownList6.SelectedValue == "2")
                        {
                            if (rpt.ToString() != "")
                                rpt.Append("<div class='break'></div>");
                            GetHearder_BadOrtho(lblregno.Text);
                        }
                        if (DropDownList6.SelectedValue == "3")
                        {
                            if (rpt.ToString() != "")
                                rpt.Append("<div class='break'></div>");
                            GetHearder_BroChopReport(lblregno.Text);
                        }
                        if (DropDownList6.SelectedValue == "4")
                        {
                            if (rpt.ToString() != "")
                                rpt.Append("<div class='break'></div>");
                            GetHearder_Eclamsia(lblregno.Text);
                        }
                        if (DropDownList6.SelectedValue == "5")
                        {
                            if (rpt.ToString() != "")
                                rpt.Append("<div class='break'></div>");
                            GetHearder_EctopicPregnency(lblregno.Text);
                        }
                        if (DropDownList6.SelectedValue == "6")
                        {
                            if (rpt.ToString() != "")
                                rpt.Append("<div class='break'></div>");
                            GetHearder_HighRisk(lblregno.Text);
                        }
                        if (DropDownList6.SelectedValue == "7")
                        {
                            if (rpt.ToString() != "")
                                rpt.Append("<div class='break'></div>");
                            GetHearder_NeonatalConvulsion(lblregno.Text);
                        }
                        if (DropDownList6.SelectedValue == "8")
                        {
                            if (rpt.ToString() != "")
                                rpt.Append("<div class='break'></div>");
                            GetHearder_NeonatalHighFeverReport(lblregno.Text);
                        }
                        if (DropDownList6.SelectedValue == "9")
                        {
                            if (rpt.ToString() != "")
                                rpt.Append("<div class='break'></div>");
                            GetHearder_NeonatalJaundiceReport(lblregno.Text);
                        }
                        if (DropDownList6.SelectedValue == "10")
                        {
                            if (rpt.ToString() != "")
                                rpt.Append("<div class='break'></div>");
                            GetHearder_NeoNatalphysia(lblregno.Text);
                        }
                        if (DropDownList6.SelectedValue == "11")
                        {
                            if (rpt.ToString() != "")
                                rpt.Append("<div class='break'></div>");
                            GetHearder_OperationConsentReport(lblregno.Text);
                        }
                        if (DropDownList6.SelectedValue == "12")
                        {
                            if (rpt.ToString() != "")
                                rpt.Append("<div class='break'></div>");
                            GetHearder_OperativeriskOldman(lblregno.Text);
                        }
                        if (DropDownList6.SelectedValue == "13")
                        {
                            if (rpt.ToString() != "")
                                rpt.Append("<div class='break'></div>");
                            GetHearder_PIHReport(lblregno.Text);
                        }
                        if (DropDownList6.SelectedValue == "14")
                        {
                            if (rpt.ToString() != "")
                                rpt.Append("<div class='break'></div>");
                            GetHearder_PoisionIntake(lblregno.Text);
                        }
                        if (DropDownList6.SelectedValue == "15")
                        {
                            if (rpt.ToString() != "")
                                rpt.Append("<div class='break'></div>");
                            GetHearder_PostOprative(lblregno.Text);
                        }
                        if (DropDownList6.SelectedValue == "16")
                        {
                            if (rpt.ToString() != "")
                                rpt.Append("<div class='break'></div>");
                            GetHearder_PPHReport(lblregno.Text);
                        }
                        if (DropDownList6.SelectedValue == "17")
                        {
                            if (rpt.ToString() != "")
                                rpt.Append("<div class='break'></div>");
                            GetHearder_PrematureBaby(lblregno.Text);
                        }
                        if (DropDownList6.SelectedValue == "18")
                        {
                            if (rpt.ToString() != "")
                                rpt.Append("<div class='break'></div>");
                            GetHearder_Septiceamia(lblregno.Text);
                        }
                        if (DropDownList6.SelectedValue == "19")
                        {
                            if (rpt.ToString() != "")
                                rpt.Append("<div class='break'></div>");
                            GetHearder_ProlongAnesthecia(lblregno.Text);
                        }
                        if (DropDownList6.SelectedValue == "20")
                        {
                            if (rpt.ToString() != "")
                                rpt.Append("<div class='break'></div>");
                            GetHearder_SevererAnemiaREport(lblregno.Text);
                        }
                        if (DropDownList6.SelectedValue == "21")
                        {
                            if (rpt.ToString() != "")
                                rpt.Append("<div class='break'></div>");
                            GetHearder_StrokeReport(lblregno.Text);
                        }
                    }
                }
                ltrReport.Text = rpt.ToString();
            }
        }
    }


    //public void PatientDetails_Bengali(DataSet ds)
    //{
    //    rpt.Append("<table width='100%' cellspacing=0 border=0 style='border:1px solid black;border-spacing: 0; padding: 0;font-family:verdana;'>");
    //    rpt.Append("<tr style='height:20px'>");
    //    rpt.AppendFormat("<td colspan='2' style='height:20px; font-family:Verdana; font-size:small;text-align:center'><b>রোগী:</b> {0}, {1}, {2},<b> প্রযত্নে:</b>{3}<b>, ঠিকানা :</b>{4},{5},{6}, <b><br/>ফোন নং:</b>{7}</td>", ds.Tables[0].Rows[0]["patient_name"], ds.Tables[0].Rows[0]["age"], ds.Tables[0].Rows[0]["SexName"], ds.Tables[0].Rows[0]["HusbandName"], ds.Tables[0].Rows[0]["vill_city"], ds.Tables[0].Rows[0]["po"], ds.Tables[0].Rows[0]["DistrictName"], ds.Tables[0].Rows[0]["PhNo1"]);
    //    rpt.Append("</tr >");

    //    rpt.Append("<tr style='height:20px'>");
    //    rpt.AppendFormat("<td style='height:20px; font-family:Verdana; font-size:small;text-align:center'><b>ভর্তির কারণ:</b> {0}, <b>ডা:বাবুর নাম:</b> {1}</td>", ds.Tables[0].Rows[0]["DiagnosisName"], ds.Tables[0].Rows[0]["doc_name"]);
    //    rpt.AppendFormat("<td rowspan='2' style='height:20px; font-family:Verdana; font-size:small;text-align:right'><img src='{0}' width='60in' height='60in' /></td>", ds.Tables[0].Rows[0]["path"]);
    //    rpt.Append("</tr >");

    //    rpt.Append("<tr style='height:20px'>");
    //    rpt.AppendFormat("<td style='height:20px; font-family:Verdana; font-size:small;text-align:center'><b>রেজি: নং:</b> {0}, <b>শয্যা নং:</b> {1}, <b>ভর্তির তারিখ ও সময়:</b> {2}</td>", ds.Tables[0].Rows[0]["PatientReg"], ds.Tables[0].Rows[0]["BedNoText"], ds.Tables[0].Rows[0]["adate"]);
    //    rpt.Append("</tr >");
    //    rpt.Append("</table>");
    //}


    public void PatientDetails_Bengali(DataSet ds)
    {
        rpt.Append("<table width='100%' cellspacing=0 border=0 style='border:1px solid black;border-spacing: 0; padding: 0;font-family:verdana;'>");
        rpt.Append("<tr style='height:20px'>");
        rpt.AppendFormat("<td style='height:20px; font-family:Verdana; font-size:small;text-align:center'><b>রোগী:</b> {0}, {1}, {2},<b> প্রযত্নে:</b>{3}<b>, ঠিকানা :</b>{4},{5},{6}, <b><br/>ফোন নং:</b>{7}</td>", ds.Tables[0].Rows[0]["patient_name"], ds.Tables[0].Rows[0]["age"], ds.Tables[0].Rows[0]["SexName"], ds.Tables[0].Rows[0]["HusbandName"], ds.Tables[0].Rows[0]["vill_city"], ds.Tables[0].Rows[0]["po"], ds.Tables[0].Rows[0]["DistrictName"], ds.Tables[0].Rows[0]["PhNo1"]);
        rpt.Append("</tr >");

        rpt.Append("<tr style='height:20px'>");
        rpt.AppendFormat("<td style='height:20px; font-family:Verdana; font-size:small;text-align:center'><b>ভর্তির কারণ:</b> {0}, <b>ডা:বাবুর নাম:</b> {1}</td>", ds.Tables[0].Rows[0]["DiagnosisName"], ds.Tables[0].Rows[0]["doc_name"]);
        rpt.Append("</tr >");

        rpt.Append("<tr style='height:20px'>");
        rpt.AppendFormat("<td style='height:20px; font-family:Verdana; font-size:small;text-align:center'><b>রেজি: নং:</b> {0}, <b>শয্যা নং:</b> {1}, <b>ভর্তির তারিখ ও সময়:</b> {2}</td>", ds.Tables[0].Rows[0]["PatientReg"], ds.Tables[0].Rows[0]["BedNoText"], ds.Tables[0].Rows[0]["adate"]);
        rpt.Append("</tr >");
        rpt.Append("</table>");
    }

    public void PatientDetails_English(DataSet ds)
    {
        rpt.Append("<table width='100%' cellspacing=0 border=0 style='border:1px solid black;border-spacing: 0; padding: 0;font-family:verdana;'>");
        rpt.Append("<tr style='height:20px'>");
        rpt.AppendFormat("<td style='height:20px; font-family:Verdana; font-size:small;text-align:center'><u><b>Patient:</b></u> {0}, {1}, {2},<u><b>C/O:</b></u>{3} <u><b>of</b></u> {4},{5},{6}, <u><b><br/>Ph No:</b></u>{7}</td>", ds.Tables[0].Rows[0]["patient_name"], ds.Tables[0].Rows[0]["age"], ds.Tables[0].Rows[0]["SexName"], ds.Tables[0].Rows[0]["HusbandName"], ds.Tables[0].Rows[0]["vill_city"], ds.Tables[0].Rows[0]["po"], ds.Tables[0].Rows[0]["DistrictName"], ds.Tables[0].Rows[0]["PhNo1"]);
        rpt.Append("</tr >");

        rpt.Append("<tr style='height:20px'>");
        rpt.AppendFormat("<td style='height:20px; font-family:Verdana; font-size:small;text-align:center'><u><b>Diagnosis:</b></u> {0}, <u><b>Under Doctor:</b></u> {1}</td>", ds.Tables[0].Rows[0]["DiagnosisName"], ds.Tables[0].Rows[0]["doc_name"]);
        rpt.Append("</tr >");

        rpt.Append("<tr style='height:20px'>");
        rpt.AppendFormat("<td style='height:20px; font-family:Verdana; font-size:small;text-align:center'><u><b>Regn No:</b></u> {0}, <u><b>Bed No:</b></u> {1}, <u><b>Adm:</b></u> {2}</td>", ds.Tables[0].Rows[0]["PatientReg"], ds.Tables[0].Rows[0]["BedNoText"], ds.Tables[0].Rows[0]["adate"]);
        rpt.Append("</tr >");
        rpt.Append("</table>");
    }

    public void GetHearder_AdmissionSlip(string reg)
    {
        ltrReport.Text = "";
        DataSet ds = thepatientbill.PatientForReport(reg, Session["CoCode"].ToString().Trim());
        if (ds.Tables[0].Rows.Count > 0)
        {
            rpt.Append("<table width='100%' cellspacing=0 border=0 style='border:1px solid black;border-spacing: 0; padding: 0;font-family:verdana;'>");
            rpt.Append("<tr style='height:25px;'>");
            rpt.Append("<td style='font-family:Verdana; font-size:x-large;text-align:center'><b>Admission Slip</b>");
            rpt.Append("</tr >");

            rpt.Append("<tr style='height:20px;'>");
            rpt.AppendFormat("<td style='font-family:Verdana; font-size:medium;text-align:center'>(Admitted By :{0}, Relation :{1})</td>", ds.Tables[0].Rows[0]["guardian_name"], ds.Tables[0].Rows[0]["relation"]);
            rpt.Append("</tr >");
            rpt.Append("</table>");



            rpt.Append("<br/>"); rpt.Append("<br/>"); 
            rpt.Append("<table width='100%' cellspacing=0 border=0 style='border:1px solid black;border-spacing: 0; padding: 0;font-family:verdana;'>");
            rpt.Append("<tr style='height:25px'>");
            rpt.AppendFormat("<td style='height:20px; font-family:Verdana;padding-left:250px; font-size:large;text-align:left'><u><b>Patient:&nbsp;&nbsp;{0}, {1}, {2}</b></u></td>", ds.Tables[0].Rows[0]["patient_name"], ds.Tables[0].Rows[0]["age"], ds.Tables[0].Rows[0]["SexName"]);
            rpt.Append("</tr >");


            rpt.Append("<tr style='height:25px'>");
            rpt.AppendFormat("<td style='height:20px; font-family:Verdana;padding-left:250px; font-size:large;text-align:left'><u><b>Bed No:&nbsp;&nbsp;{0}</b></u></td>", ds.Tables[0].Rows[0]["BedNoText"]);
            rpt.Append("</tr >");

            rpt.Append("<tr style='height:25px'>");
            rpt.AppendFormat("<td style='height:20px; font-family:Verdana;padding-left:250px; font-size:small;text-align:left'><u><b>C/O:</b></u> {0}</td>", ds.Tables[0].Rows[0]["HusbandName"]);
            rpt.Append("</tr >");

            rpt.Append("<tr style='height:25px'>");
            rpt.AppendFormat("<td style='height:20px; font-family:Verdana;padding-left:250px; font-size:small;text-align:left'><u><b>Address:</b></u> {0}, {1}, {2},<u><b>Ph No:</b></u>{3}</td>", ds.Tables[0].Rows[0]["vill_city"], ds.Tables[0].Rows[0]["po"], ds.Tables[0].Rows[0]["DistrictName"], ds.Tables[0].Rows[0]["PhNo1"]);
            rpt.Append("</tr >");

            rpt.Append("<tr style='height:25px'>");
            rpt.AppendFormat("<td style='height:20px; font-family:Verdana;padding-left:250px; font-size:small;text-align:left'><u><b>Diagnosis:</b></u> {0}</td>", ds.Tables[0].Rows[0]["DiagnosisName"]);
            rpt.Append("</tr >");

            rpt.Append("<tr style='height:25px'>");
            rpt.AppendFormat("<td style='height:20px; font-family:Verdana;padding-left:250px; font-size:small;text-align:left'><u><b>Under Doctor:</b></u> {0}</td>", ds.Tables[0].Rows[0]["doc_name"]);
            rpt.Append("</tr >");

            rpt.Append("<tr style='height:25px'>");
            rpt.AppendFormat("<td style='height:20px; font-family:Verdana;padding-left:250px; font-size:small;text-align:left'><u><b>Regn No:</b></u> {0}</td>", ds.Tables[0].Rows[0]["PatientReg"]);
            rpt.Append("</tr >");


            rpt.Append("<tr style='height:25px'>");
            rpt.AppendFormat("<td style='height:20px; font-family:Verdana; padding-left:250px;font-size:small;text-align:left'><u><b>Adm Date & Time:</b></u> {0}</td>", ds.Tables[0].Rows[0]["adate"]);
            rpt.Append("</tr >");
           
            rpt.Append("</table>");
        }
        ltrReport.Visible = true;

    }

    public void Report_Header()
    {
        rpt.Append("<table width='100%' cellspacing=0 border=0 style='border:1px solid black;border-spacing: 0; padding: 0;font-family:verdana;'>");
        rpt.Append("<tr cellpadding:'0'  style='height:30px;'>");
        rpt.Append("<td rowspan='3' width='2%' style='font-family:Verdana; font-size:x-large; font-weight:bold;padding-left:100px;text-align:left'><img src='/Images/gfclogo.png'  style='height:77px;width:75px;'/></td>");
        rpt.Append("<td   width='20%'  style='height:20px;font-family:Verdana; font-size:x-large; font-weight:bold;text-align:center'><u>GFC HOSPITAL</u></td>");
        rpt.Append("<td rowspan='3'  width='2%' style='height:20px;font-family:Verdana; font-size:x-large;padding-right:100px; font-weight:bold;text-align:right'><img src='/Images/gfclogo.png'  style='height:77px;width:75px;'/></td>");
        rpt.Append("</tr>");

        rpt.Append("<tr  style='height:20px;'>");
        rpt.AppendFormat("<td   width='20%'  style='height:10px;font-family:Verdana; font-size:small; font-weight:bold;text-align:center'>(Regn. No : NH-315/G-70/2013)</td>");
        rpt.Append("</tr>");

        rpt.Append("<tr  style='height:10px;'>");
        rpt.AppendFormat("<td  width='70%'  style='height:15px;font-family:Verdana; font-size:medium; font-weight:bold;text-align:center'>Kushpata, Ghatal, Paschim Medinipur, WB, 721212</td>");

        rpt.Append("</tr>");
        rpt.Append("</table>");
    }

    public void GetHearder_Aborsion(string regno)
    {
        ltrReport.Text = "";
        DataSet ds = thepatientbill.PatientForReport(regno, Session["CoCode"].ToString().Trim());
        if (ds.Tables[0].Rows.Count > 0)
        {
            rpt.Append("<table width='100%' cellspacing=0 border=0 style='border:1px solid black;border-spacing: 0; padding: 0;font-family:verdana;'>");
            rpt.Append("<tr style='height:25px;'>");
            rpt.Append("<td style='font-family:Verdana; font-size:x-large;text-align:center'><b>গর্ভপাতের সম্মতি পত্র</b>");
            rpt.Append("</tr >");

            rpt.Append("<tr style='height:20px;'>");
            rpt.AppendFormat("<td style='font-family:Verdana; font-size:medium;text-align:center'>(যিনি ভর্তি করাচ্ছেন তার নাম:{0},সম্পর্ক:{1})</td>", ds.Tables[0].Rows[0]["guardian_name"], ds.Tables[0].Rows[0]["relation"]);
            rpt.Append("</tr >");
            rpt.Append("</table>");


            PatientDetails_Bengali(ds);


            rpt.Append("<br/>");
            rpt.Append("<table width='100%'>");
            rpt.Append("<tr style='height:25px'>");
            rpt.Append("<td style='font-family:Verdana;font-size:medium; text-align:justify'>আমার গর্ভে যে সন্তান আছে তা আমি জানি এবং ডাক্তারবাবুও আমাকে পরীক্ষা করে সে কথা আমাকে জানালেন । কিন্তু আমি আমার____________ কারণে আমার এই গর্ভস্থ সন্তান নষ্ট করে দিতে চাই । </td>");
            rpt.Append("</tr >");

            rpt.Append("<tr style='height:25px'>");
            rpt.Append("<td style='font-family:Verdana;font-size:medium; text-align:justify'>ডাক্তারবাবু গর্ভপাত করানোর পদ্ধতি ও তার সম্ভাব্য সমস্ত জটিলতা সম্বন্ধে আমাকে জানিয়েছেন । এমন কি কোনও কোনও ক্ষেত্রে প্রছুর রক্তস্রাব ,পেটের অন্ত্রে ক্ষতি বা জীবাণু সংক্রামনের মতো যাতে জীবণ সংশয় পর্যন্ত হতে পারে বা জরায়ূ-কে আপারেশনের মাধ্যমে বাদ দিতে পর্যন্ত হতে পারে । এসব ভালভাবে জেনে এই গর্ভপাতে অনুমতি দিলাম । আরও জানলাম যে , চিকিৎসা বিজ্ঞানের তথ্যানুযায়ী গর্ভপাতর ফলে ভবিষ্যতে আবার বাচ্চা না-আসার সম্ভাবনা সহ সূদূর প্রসারী আরও অনেক কুফল আছে । এসবের জন্য ডাক্তারবাবু দায়ী থাকবেন না।</td>");
            rpt.Append("</tr >");

            rpt.Append("<tr style='height:25px'>");
            rpt.Append("<td style='font-family:Verdana;font-size:medium; text-align:justify'>এই সম্মতি পত্রে স্বেচ্ছায়, সমস্ত কথার মানে বুঝে সই করলাম । </td>");
            rpt.Append("</tr >");

            rpt.Append("<tr style='height:25px'>");
            rpt.AppendFormat("<td style=' font-family:Verdana;font-size:medium; text-align:right'>ইতি--------- {0}</td>", ds.Tables[0].Rows[0]["patient_name"]);
            rpt.Append("</tr >");
            rpt.Append("</table>");




            rpt.Append("<table width='100%'>");
            rpt.Append("<tr style='height:35px'>");
            rpt.Append("<td style='font-family:Verdana; font-size:medium; text-align:center'> স্বাক্ষর/টিপ </td>");
            rpt.Append("<td style='font-family:Verdana; font-size:medium; text-align:center'> পুরো  নাম  </td>");
            rpt.Append("<td style='font-family:Verdana; font-size:medium; text-align:center'> ঠিকানা </td>");
            rpt.Append("<td style='font-family:Verdana; font-size:medium; text-align:center'> সম্পর্ক  </td>");
            rpt.Append("</tr >");

            rpt.Append("<tr style='height:27px'>");
            rpt.Append("<td style='font-family:Verdana;  font-size:medium; text-align:center'>..................</td>");
            rpt.AppendFormat("<td style='font-family:Verdana;  font-size:medium; text-align:center'>{0}</td>", ds.Tables[0].Rows[0]["patient_name"]);
            rpt.AppendFormat("<td style='font-family:Verdana; font-size:medium; text-align:center'>{0}</td>", ds.Tables[0].Rows[0]["vill_city"]);
            rpt.Append("<td style='font-family:Verdana; font-size:medium; text-align:center'> নিজে </td>");
            rpt.Append("</tr >");

            rpt.Append("<tr style='height:27px'>");
            rpt.Append("<td style='font-family:Verdana;  font-size:medium; text-align:center'>..................</td>");
            rpt.AppendFormat("<td style='font-family:Verdana;  font-size:medium; text-align:center'>{0}</td>", ds.Tables[0].Rows[0]["guardian_name"]);
            rpt.AppendFormat("<td style='font-family:Verdana; font-size:medium; text-align:center'>{0}</td>", ds.Tables[0].Rows[0]["vill_city2"]);
            rpt.AppendFormat("<td style='font-family:Verdana; font-size:medium; text-align:center'> {0} </td>", ds.Tables[0].Rows[0]["relation"]);
            rpt.Append("</tr >");
            rpt.Append("</table>");
        }
        rpt.Append("<div class='break'>");
        rpt.Append("</div>");

        ltrReport.Visible = true;

    }
    
    public void GetHearder_Aborsion1(string reg)
    {
        ltrReport.Text = "";
        DataSet ds = thepatientbill.PatientForReport(reg, Session["CoCode"].ToString().Trim());
        if (ds.Tables[0].Rows.Count > 0)
        { 
            rpt.Append("<table width='100%' cellspacing=0 border=0 style='border:1px solid black;border-spacing: 0; padding: 0;font-family:verdana;'>");
            rpt.Append("<tr style='height:25px;'>");
            rpt.Append("<td style='font-family:Verdana; font-size:x-large;text-align:center'><b>MTP Consent</b>");
            rpt.Append("</tr >");

            rpt.Append("<tr style='height:20px;'>");
            rpt.AppendFormat("<td style='font-family:Verdana; font-size:medium;text-align:center'>(Admitted By :{0}, Relation :{1})</td>", ds.Tables[0].Rows[0]["guardian_name"], ds.Tables[0].Rows[0]["relation"]);
            rpt.Append("</tr >");
            rpt.Append("</table>");

            PatientDetails_English(ds);
            rpt.Append("<br/>");

   
            rpt.Append("<table width='100%'>");
            rpt.Append("<tr style='height:25px'>");
            rpt.Append("<td style='font-family:Verdana;font-size:medium; text-align:justify'>I know that there are children in my womb and told me he would check me daktarababuo.But I lost my ____________ to the fetus due to me like this.</td>");
            rpt.Append("</tr >");

            rpt.Append("<tr style='height:25px'>");
            rpt.Append("<td style='font-family:Verdana;font-size:medium; text-align:justify'>About the abortion procedure and its potential complications daktarababu told me . Prachura even bleeding in some cases , loss of bowel or gut microbe that lives in sankramanera There may be confusion or jarayu - There may be removed through a aparesanera . We well know that these allow the abortion . Also learned that , according to medical science, abortion, the baby is due in the future - including the possibility of reaching sudura have many consequences . Shall not be liable for these daktarababu .</td>");
            rpt.Append("</tr >");

            rpt.Append("<tr style='height:25px'>");
            rpt.Append("<td style='font-family:Verdana;font-size:medium; text-align:justify'>About the abortion procedure and its potential complications daktarababu told me. Even at this Consent Form, signed by all mean I understand.</td>");
            rpt.Append("</tr >");

            rpt.Append("<tr style='height:25px'>");
            rpt.AppendFormat("<td style=' font-family:Verdana;font-size:medium; text-align:right'>Finish--------- {0}</td>", ds.Tables[0].Rows[0]["patient_name"]);
            rpt.Append("</tr >");
            rpt.Append("</table>");


            rpt.Append("<table width='100%'>");
            rpt.Append("<tr style='height:27px'>");
            rpt.Append("<td style='font-family:Verdana; font-size:medium; text-align:center'> Signature </td>");
            rpt.Append("<td style='font-family:Verdana; font-size:medium; text-align:center'> Full Name  </td>");
            rpt.Append("<td style='font-family:Verdana; font-size:medium; text-align:center'> Address </td>");
            rpt.Append("<td style='font-family:Verdana; font-size:medium; text-align:center'> Relation  </td>");
            rpt.Append("</tr >");

            rpt.Append("<tr style='height:27px'>");
            rpt.Append("<td style='font-family:Verdana;  font-size:medium; text-align:center'>..................</td>");
            rpt.AppendFormat("<td style='font-family:Verdana;  font-size:medium; text-align:center'>{0}</td>", ds.Tables[0].Rows[0]["patient_name"]);
            rpt.AppendFormat("<td style='font-family:Verdana; font-size:medium; text-align:center'>{0}</td>", ds.Tables[0].Rows[0]["vill_city"]);
            rpt.Append("<td style='font-family:Verdana; font-size:medium; text-align:center'> Self </td>");
            rpt.Append("</tr >");

            rpt.Append("<tr style='height:27px'>");
            rpt.Append("<td style='font-family:Verdana;  font-size:medium; text-align:center'>..................</td>");
            rpt.AppendFormat("<td style='font-family:Verdana;  font-size:medium; text-align:center'>{0}</td>", ds.Tables[0].Rows[0]["guardian_name"]);
            rpt.AppendFormat("<td style='font-family:Verdana; font-size:medium; text-align:center'>{0}</td>", ds.Tables[0].Rows[0]["vill_city2"]);
            rpt.AppendFormat("<td style='font-family:Verdana; font-size:medium; text-align:center'> {0} </td>", ds.Tables[0].Rows[0]["relation"]);
            rpt.Append("</tr >");
            rpt.Append("</table>");
        }
        ltrReport.Visible = true;
    }

    public void GetHearder_AdmissionFrom(string regno)
    {
        ltrReport.Text = "";
        DataSet ds = thepatientbill.PatientForReport(regno,Session["CoCode"].ToString().Trim()); 

        if (ds.Tables[0].Rows.Count > 0)
        {
            rpt.Append("<table width='100%' cellspacing=0 border=0 style='border:1px solid black;border-spacing: 0; padding: 0;font-family:verdana;'>");
            rpt.Append("<tr style='height:25px;'>");
            rpt.Append("<td style='font-family:Verdana; font-size:x-large;text-align:center'><b>ভর্তির ফর্ম</b>");
            rpt.AppendFormat("<td rowspan='2' style='height:20px; font-family:Verdana; font-size:small;text-align:right'><img src='{0}' width='60in' height='60in' /></td>", ds.Tables[0].Rows[0]["path"]);
            rpt.Append("</tr >");

            rpt.Append("<tr style='height:20px;'>");
            rpt.AppendFormat("<td style='font-family:Verdana; font-size:medium;text-align:center'>(যিনি ভর্তি করাচ্ছেন তার নাম:{0},সম্পর্ক:{1})</td>", ds.Tables[0].Rows[0]["guardian_name"], ds.Tables[0].Rows[0]["relation"]);
            rpt.Append("</tr >");
            rpt.Append("</table>");

            PatientDetails_Bengali(ds);

            rpt.Append("<br/>"); rpt.Append("<br/>"); 
            rpt.Append("<table width='100%'>");
            rpt.Append("<tr style='height:25px'>");
            rpt.Append("<td style=' font-family:Verdana;font-size:medium; text-align:justify'> আমরা সমস্ত কিছু জেনে বুঝে আমাদের রোগী কে এই প্রতিষ্ঠানে ভর্তি করছি। আমরা এই প্রতিষ্ঠানে সমস্ত অসুবিধা, সুবিধা ও চিকিৎসার সীমাবদ্ধতা সম্বন্ধে আমরা খুব ভালো ভাবে জেনেছি।আমাদের আত্মীয়বর্গ ও পরিজনের সংগে দীর্ঘ আলোচনার পরিপ্রেক্ষিতে সম্মিলিত ভাবে আমাদের রোগী ভর্তির এই সিদ্ধান্ত নিয়েছি। আমাদের এখানে রোগী ভর্তি করতে কেউ বাধ্য করেনি । </td>");

            rpt.Append("</tr >");

            rpt.Append("<tr style='height:25px'>");
            rpt.Append("<td style=' font-family:Verdana;font-size:medium; text-align:justify'>কিছু কিছু ওষুধের পার্শ্ব প্রতিক্রিয়ায় রোগী খারাপ হয়ে যাওয়া বা মারা যাওয়ার মতো দূর্ঘটনা ঘটে যেতে পারে-যখন কারো কিছু করার থাকে না। এবং এর আগে থেকে কোন প্রতিষেধক নেওয়া যায় না। </td>");

            rpt.Append("</tr >");

            rpt.Append("<tr style='height:25px'>");
            rpt.Append("<td style=' font-family:Verdana; font-size:medium; text-align:justify'>আমাদের রোগী যদি খুব খারাপ অবস্থা কোন কারনে হয়ে যায় এবং এই প্রতিষ্ঠানের চিকিৎসার সীমাবদ্ধতার বাইরে চলে যায় তখন রোগীর চিকিৎসার স্বার্থে আমরা আমাদের রোগীকে অন্যত্র নিয়ে যাবার প্রতিশ্রুতি দিচ্ছি।</td>");

            rpt.Append("</tr >");

            rpt.Append("<tr style='height:25px'>");
            rpt.Append("<td style=' font-family:Verdana;font-size:medium; text-align:justify'>আমরা রোগীর চিকিৎসার জন্যে জীবনদায়ী ওষুধ, রক্ত এবং যখন যেমন দরকার তার ভিত্তিতে সেই সমস্ত জিনিষ সরবরাহ করে এই প্রতিষ্ঠান কে সবসময় সাহায্য ও সহযোগিতা করব। </td>");
            rpt.Append("</tr >");

            rpt.Append("<tr style='height:25px'>");
            rpt.Append("<td style=' font-family:Verdana;font-size:medium; text-align:justify'>আমরা এই প্রতিষ্ঠানের কর্মকর্তা এবং সংশ্লিষ্ট ডাঃ বাবুর সংগে দীর্ঘ আলোচনা সাপেক্ষে সব কথা শুনে ভালোভাবে বুঝে এবং আমাদের সমস্ত জিজ্ঞাসার সন্তোষজনক উত্তর পেয়ে এখানে রোগী ভর্তি করলাম। </td>");
            rpt.Append("</tr >");
            rpt.Append("</table>");

            rpt.Append("<br/>"); rpt.Append("<br/>");

            rpt.Append("<table width='100%' cellspacing=0 border=0 style='padding: 0;font-family:verdana;'>");
            rpt.Append("<tr style='height:32px'>");
            rpt.Append("<td style=' font-family:Verdana; font-size:medium; text-align:center'> স্বাক্ষর/টিপ </td>");
            rpt.Append("<td style='  font-family:Verdana; font-size:medium; text-align:center'> পুরো  নাম  </td>");
            rpt.Append("<td style=' font-family:Verdana; font-size:medium; text-align:center'> ঠিকানা </td>");
            rpt.Append("<td style=' font-family:Verdana; font-size:medium; text-align:center'> সম্পর্ক  </td>");
            rpt.Append("</tr >");

            rpt.Append("<tr style='height:32px'>");
            rpt.Append("<td style='font-family:Verdana;  font-size:medium; text-align:center'>..................</td>");
            rpt.AppendFormat("<td style='font-family:Verdana;  font-size:medium; text-align:center'>{0}</td>", ds.Tables[0].Rows[0]["patient_name"]);
            rpt.AppendFormat("<td style='font-family:Verdana; font-size:medium; text-align:center'>{0}</td>", ds.Tables[0].Rows[0]["vill_city"]);
            rpt.Append("<td style='font-family:Verdana; font-size:medium; text-align:center'> নিজে </td>");
            rpt.Append("</tr >");

            rpt.Append("<tr style='height:32px'>");
            rpt.Append("<td style='font-family:Verdana;  font-size:medium; text-align:center'>..................</td>");
            rpt.AppendFormat("<td style='font-family:Verdana;  font-size:medium; text-align:center'>{0}</td>", ds.Tables[0].Rows[0]["guardian_name"]);
            rpt.AppendFormat("<td style='font-family:Verdana; font-size:medium; text-align:center'>{0}</td>", ds.Tables[0].Rows[0]["vill_city2"]);
            rpt.AppendFormat("<td style=' font-family:Verdana; font-size:medium; text-align:center'> {0} </td>", ds.Tables[0].Rows[0]["relation"]);
            rpt.Append("</tr >");
            rpt.Append("</table>");


       
            rpt.Append("<table width='100%' cellspacing=0 border=0 style='padding: 0;font-family:verdana;'>");
            rpt.Append("<tr style='height:35px'>");
            rpt.Append("<td style='font-family:Verdana;  font-size:medium; text-align:justify'>-----------------------------------------------------------------------------------------------------------------------</td>");
            rpt.Append("</tr >");
            rpt.Append("</table>");


            Report_Header();
            rpt.Append("<table width='100%' cellspacing=0 border=0 style='border:1px solid black;border-spacing: 0; padding: 0;font-family:verdana;'>");
            rpt.Append("<tr style='height:25px;'>");
            rpt.Append("<td style='font-family:Verdana; font-size:large;text-align:center'><b>রোগীর  ছুটি / অন্যত্র  নিয়ে যাবার সম্মতি পত্র</b>");
            rpt.Append("</tr >");
            rpt.Append("</table>");
            rpt.Append("</br/>"); rpt.Append("</br/>");
            rpt.Append("<tr style='height:30px'>");
            rpt.Append("<td style='font-family:Verdana; font-size:medium; text-align:justify'>১. আমরা রোগী কে সুস্থ  অবস্থায় নিজ দায়িত্বে এখান থেকে নিয়ে যাচ্ছি।<br/> ২. রোগীকে চিকিৎসার জন্য আমাদের নিজেদের দায়িত্বে ও ইচ্ছায় অন্যত্র নিয়ে যাচ্ছি  <br/> ৩. রোগীর চিকিৎসার সমস্ত নথি ও পরীক্ষার কাগজপত্র বুঝে পেলাম। <br/> ৪. ডাঃ বাবুকে নিয়মিত দেখাব ও তাৎক্ষনিক খুব জরুরী সমস্যা হলে সরকারী হাসপাতালে বা অন্যত্র যোগাযোগ করব। </td>");
            rpt.Append("</tr >");
            rpt.Append("</table>");

            rpt.Append("<br/>"); rpt.Append("<br/>"); rpt.Append("<br/>");
            rpt.Append("<table width='100%' cellspacing=0 border=0 style='padding: 0;font-family:verdana;'>");
            rpt.Append("<tr style='height:25px'>");
            rpt.Append("<td style='font-family:Verdana; font-size:medium; text-align:center'> স্বাক্ষর/টিপ </td>");
            rpt.Append("<td style='font-family:Verdana; font-size:medium; text-align:center'> পুরো  নাম  </td>");
            rpt.Append("<td style='font-family:Verdana; font-size:medium; text-align:center'> ঠিকানা </td>");
            rpt.Append("<td style='font-family:Verdana; font-size:medium; text-align:center'> সম্পর্ক  </td>");
            rpt.Append("</tr >");
            rpt.Append("</table >");
        }
        ltrReport.Visible = true;

    }

    public void GetHearder_AdmissionFrom1(string reg)
    {
        ltrReport.Text = "";
        DataSet ds = thepatientbill.PatientForReport(reg,Session["CoCode"].ToString().Trim());
        if (ds.Tables[0].Rows.Count > 0)
        {
            rpt.Append("<table width='100%' cellspacing=0 border=0 style='border:1px solid black;border-spacing: 0; padding: 0;font-family:verdana;'>");
            rpt.Append("<tr style='height:25px;'>");
            rpt.Append("<td style='font-family:Verdana; font-size:x-large;text-align:center'><b>Admission Form</b>");
            rpt.Append("</tr >");

            rpt.Append("<tr style='height:20px;'>");
            rpt.AppendFormat("<td style='font-family:Verdana; font-size:medium;text-align:center'>(Admitted By :{0}, Relation :{1})</td>", ds.Tables[0].Rows[0]["guardian_name"], ds.Tables[0].Rows[0]["relation"]);
            rpt.Append("</tr >");
            rpt.Append("</table>");

            PatientDetails_English(ds);

            rpt.Append("<br/>");        rpt.Append("<br/>");
            rpt.Append("<table width='100%'>");
            rpt.Append("<tr style='height:32px'>");
            rpt.Append("<td style='font-family:Verdana;font-size:small; text-align:justify'> We have all of our patients who have knowingly been admitted to this institution. We all difficulties of this institution, we have a very good way and I learned about the benefits and limitations of treatment. Terms of long discussions with our kinsfolk and family together to make this decision for admission to our patients. None of the patients has forced us to refill. </td>");

            rpt.Append("</tr >");

            rpt.Append("<tr style='height:32px'>");
            rpt.Append("<td style=' font-family:Verdana;font-size:small; text-align:justify'>Some patients fail to respond to drug side died in the accident or may be - when somebody has to do something. And cannot be taken before any antidote. </td>");

            rpt.Append("</tr >");

            rpt.Append("<tr style='height:32px'>");
            rpt.Append("<td style='font-family:Verdana; font-size:small; text-align:justify'> Is due to the very bad condition of our patients, and this goes beyond the limitations of treatment, the patient's medical interests of the company, we promise to give our patients the transplant. </td>");

            rpt.Append("</tr >");

            rpt.Append("<tr style='height:32px'>");
            rpt.Append("<td style='font-family:Verdana;font-size:small; text-align:justify'> We are life-saving drugs for the treatment of the patient, such as blood, and the need to provide the basis for all the things in this organization will always help and cooperation. </td>");
            rpt.Append("</tr >");

            rpt.Append("<tr style='height:32px'>");
            rpt.Append("<td style=' font-family:Verdana;font-size:small; text-align:justify'>We discuss the organization's officers and subject to long associated with Dr. Babu heard and understood well the patients admitted to our getting satisfactory answers to all questions asked. </td>");
            rpt.Append("</tr >");
            rpt.Append("</table>");

            rpt.Append("<table width='100%' cellspacing=0 border=0 style='padding: 0;font-family:verdana;'>");
            rpt.Append("<tr style='height:32px'>");
            rpt.Append("<td style=' font-family:Verdana; font-size:small; text-align:center'> Signature / thumb </td>");
            rpt.Append("<td style=' font-family:Verdana; font-size:small; text-align:center'> Full Name  </td>");
            rpt.Append("<td style=' font-family:Verdana; font-size:small; text-align:center'> Address </td>");
            rpt.Append("<td style=' font-family:Verdana; font-size:small; text-align:center'> Relationship  </td>");
            rpt.Append("</tr >");

            rpt.Append("<tr style='height:32px'>");
            rpt.Append("<td style=' font-family:Verdana;  font-size:small; text-align:center'>..................</td>");
            rpt.AppendFormat("<td style=' font-family:Verdana;  font-size:small; text-align:center'>{0}</td>", ds.Tables[0].Rows[0]["patient_name"]);
            rpt.AppendFormat("<td style='font-family:Verdana; font-size:small; text-align:center'>{0}</td>", ds.Tables[0].Rows[0]["vill_city"]);
            rpt.Append("<td style=' font-family:Verdana; font-size:small; text-align:center'> Self </td>");
            rpt.Append("</tr >");

            rpt.Append("<tr style='height:32px'>");
            rpt.Append("<td style=' font-family:Verdana;  font-size:small; text-align:center'>..................</td>");
            rpt.AppendFormat("<td style=' font-family:Verdana;  font-size:small; text-align:center'>{0}</td>", ds.Tables[0].Rows[0]["guardian_name"]);
            rpt.AppendFormat("<td style=' font-family:Verdana; font-size:small; text-align:center'>{0}</td>", ds.Tables[0].Rows[0]["vill_city2"]);
            rpt.AppendFormat("<td style=' font-family:Verdana; font-size:small; text-align:center'> {0} </td>", ds.Tables[0].Rows[0]["relation"]);
            rpt.Append("</tr >");
            rpt.Append("</table>");

            rpt.Append("<table width='100%' cellspacing=0 border=0 style='padding: 0;font-family:verdana;'>");
            rpt.Append("<tr style='height:32px'>");
            rpt.Append("<td style=' font-family:Verdana;  font-size:medium; text-align:justify'>-----------------------------------------------------------------------------------------------------------------------</td>");
            rpt.Append("</tr >");
            rpt.Append("</table>");

            Report_Header();
            rpt.Append("<table width='100%' cellspacing=0 border=0 style='border:1px solid black;border-spacing: 0; padding: 0;font-family:verdana;'>");
            rpt.Append("<tr style='height:25px;'>");
            rpt.Append("<td style='font-family:Verdana; font-size:large;text-align:center'><b>Discharge / Referral Consent Form</b>");
            rpt.Append("</tr >");
            rpt.Append("</table>");
            rpt.Append("</br/>"); rpt.Append("</br/>");
            rpt.Append("<tr style='height:30px'>");
            rpt.Append("<td style='font-family:Verdana; font-size:medium; text-align:justify'>1. We are taking our physically fit patient at our own responsibility <br/> 2. We are taking our patient elsewhere at our own responsibility for better management and care.<br/> 3. We have received all papers and documents related to treatment and investigation.  <br/> 4. We shall keep contact with the doctor and contact with nearest hospital in case of dire emergency. </td>");
            rpt.Append("</tr >");
            rpt.Append("</table>"); 

            rpt.Append("<br/>"); rpt.Append("<br/>"); 

            rpt.Append("<table width='100%' cellspacing=0 border=0 style='padding: 0;font-family:verdana;'>");
            rpt.Append("<tr style='height:32px'>");
            rpt.Append("<td style='width: 5%;Height:70px;  font-family:Verdana; font-size:small; text-align:center'> Signature / thumb </td>");
            rpt.Append("<td style='width: 5%;Height:70px;  font-family:Verdana; font-size:small; text-align:center'> Full Name  </td>");
            rpt.Append("<td style='width: 5%;Height:70px;  font-family:Verdana; font-size:small; text-align:center'> Address </td>");
            rpt.Append("<td style='width: 5%;Height:70px;  font-family:Verdana; font-size:small; text-align:center'> Relationship  </td>");
            rpt.Append("</tr >");
            rpt.Append("</table >");    
        }
        ltrReport.Visible = true;

    }

    public void GetHearder_OperationConsent(string regno)
    {
        ltrReport.Text = "";
        DataSet ds = thepatientbill.PatientForReport(regno, Session["CoCode"].ToString().Trim());

        if (ds.Tables[0].Rows.Count > 0)
        {
            rpt.Append("<table width='100%' cellspacing=0 border=0 style='border:1px solid black;border-spacing: 0; padding: 0;font-family:verdana;'>");
            rpt.Append("<tr style='height:25px;'>");
            rpt.Append("<td style='font-family:Verdana; font-size:x-large;text-align:center'><b>অপারেশন,অজ্ঞান করার পদ্ধতি,বিশেষ চিকিৎসা বা প্রক্রিয়ার সম্মতি পত্র</b>");
            rpt.Append("</tr >");

            rpt.Append("<tr style='height:20px;'>");
            rpt.AppendFormat("<td style='font-family:Verdana; font-size:medium;text-align:center'>(যিনি ভর্তি করাচ্ছেন তার নাম:{0},সম্পর্ক:{1})</td>", ds.Tables[0].Rows[0]["guardian_name"], ds.Tables[0].Rows[0]["relation"]);
            rpt.Append("</tr >");
            rpt.Append("</table>");

            PatientDetails_Bengali(ds);

            rpt.Append("<br/>");
            rpt.Append("<table width='100%'>");
            rpt.Append("<tr style='height:30px'>");
            rpt.Append("<td style='width: 5%;Height:70px;  font-family:Verdana;font-size:medium; text-align:justify'> আমি / আমার___________________চিকিৎসা, অপারেশন পদ্ধতিগুলি সংক্রান্ত আমার সমস্ত প্রশ্নের যথাযথ এবং সন্তোষজনক উত্তর এই প্রতিষ্ঠানের সংশ্লিষ্ট চিকিৎসকের কাছ থেকে পেয়ে আমার উপসর্গ গুলির যথাযথ চিকিৎসা/অপারেশন/রোগ নিরুপণ সংক্রান্ত পদ্ধতিগুলি সমাপনের নির্দেশ দিলাম । উক্ত চিকিৎসা অপারেশন, অজ্ঞান পদ্ধতি ও প্রক্রিয়াগুলি এবং সম্ভাব্য চিকিৎসা পদ্ধতিগুলির সঙ্গে সংশ্লিষ্ট ঝুঁকি , ঝামেলা জটিলতা ও পরিণামগুলি সম্পর্কে আমি অবহিত হয়েছি এবং জেনেছি এই অপারেশন বা চিকিৎসা চলাকালীন এমন কিছু অদৃষ্টপূর্ব অবস্থা ঘটতে পারে যাতে পূর্ব-নির্দ্দিষ্ট প্রক্রিয়া ছাড়াও অন্যান্য বা মূল পদ্ধতির পরিবর্জনের প্রয়োজন হতে পারে । উক্ত চিকিৎসক বা শল্যচিকিৎসক বা সহকর্মীগণকে যেন তাঁদের পেশাগত বিবেচনার বলে প্রয়োজনীয় চিকিৎসা ও শল্যপদ্ধতি গ্রহণ করেন , সে বিষয়ে আমার সম্মতি ও অনুরোধ রইল । যে কোনও চিকিৎসা বা শল্যচিকিৎসার ক্ষেত্রে প্রচুর রক্তক্ষরণ ,বীজদূষণ , হৃৎপিণ্ড অচলতা ইত্যাদি যে সমস্ত ঝুঁকি থাকে , সেগুলি এক্ষেত্রেও এসে যেতে পারে তাও আমাকে জানানো হয়েছে এবং আমার শারীরিক অসুস্থতা পুরোপুরি নিরাময়ের কোনও গ্যারেন্টি বা প্রতিশ্রুতি আমাকে দেওয়া হয়নি । তাই আমার চিকিৎসা চলাকালীন কোনও জটিলতা হলে আমি অযথা ডাক্তারবাবু ও তাঁর সহকর্মীগণকে কিম্বা নার্সিং হোম/হাসপাতালকে দায়ী করব না ।</td>");

            rpt.Append("</tr >");
            rpt.Append("</table>");



            rpt.Append("<table width='100%' cellspacing=0 border=0 style='padding: 0;font-family:verdana;'>");
            rpt.Append("<tr style='height:30px'>");
            rpt.Append("<td style='width: 5%;Height:70px;  font-family:Verdana;font-size:medium; text-align:justify'>আমি সম্মতি দিচ্ছি যে, এই পরিষেবায় প্রয়োজনীয় ও উপযুক্ত যে কোনও ওষুধের সাহায্যে অজ্ঞানতা প্রয়োগ করা যেতে পারে । আমি মনে করি যে, অজ্ঞানতা প্রয়োগের ফলে জটিলতা দেখা দিতে পারে এবং অজ্ঞান পদ্ধতি ও অপারেশন চলাকালীন মস্তিষ্কের এবং দাঁতের ক্ষতি হতে পারে । </td>");

            rpt.Append("</tr >");
            rpt.Append("</table>");


            rpt.Append("<table width='100%' cellspacing=0 border=0 style='padding: 0;font-family:verdana;'>");
            rpt.Append("<tr style='height:30px'>");
            rpt.Append("<td style='width: 5%;Height:70px;  font-family:Verdana; font-size:medium; text-align:justify'>বিচ্ছিন্ন শারীরিক কোনও অঙ্গ বা যন্ত্রাংশ যাতে নার্সিং হোম/হাসপাতাল কর্তৃপক্ষ তাঁদের প্রচলিত পদ্ধতি অনুসারে অপসরণ করতে পারেন, সে সম্পর্কে আমি সম্মতি দিলাম । </td>");

            rpt.Append("</tr >");
            rpt.Append("</table>");


            rpt.Append("<table width='100%' cellspacing=0 border=0 style='padding: 0;font-family:verdana;'>");
            rpt.Append("<tr style='height:30px'>");
            rpt.Append("<td style='width: 5%;Height:70px;  font-family:Verdana;font-size:medium; text-align:justify'>ল্যাপারোস্কোপি বা হিস্টোরোস্কোপি বা দূরবীন শল্যচিকিৎসা বা মাইক্রো সার্জারি চলা কালীন পদ্ধতিগত জটিলতা দেখা দিলে --প্রথাগত ভাবে শল্যবিদ্যা অনুসরণ করার অর্থাৎ পেট কেটে শল্য চিকিৎসা করার অনুমতি দিলাম । ওই পদ্ধতিতে শল্যচিকিৎসার ক্ষেত্রে হৃৎপিণ্ড অচলাবস্থার মতো মারাত্বক জটিলতা দেখা দিতে পারে তাও জানলাম ।</td>");
            rpt.Append("</tr >");
            rpt.Append("</table>");

            rpt.Append("<table width='100%' cellspacing=0 border=0 style='padding: 0;font-family:verdana;'>");
            rpt.Append("<tr style='height:30px'>");
            rpt.Append("<td style='width: 5%;Height:70px;  font-family:Verdana;font-size:medium; text-align:justify'>আমি OT-তে থাকাকালীন চিকিৎসকের প্রয়োজনে বা নির্দেশে অন্যান্য পর্যবেক্ষকদের OT-তে প্রবেশ করার অনুমতি দেওয়ার পাশাপাশি শিক্ষা কিম্বা গবেষণার উদ্দেশ্যে অপারেশনের প্রক্রিয়া এবং আমার শরীর/দেহাংশের আলোকচিত্র গ্রহণ এবং আমার পরিচয় গোপন রেখে ওই সমস্ত আলোকচিত্র তৎক্ষণাৎ কিম্বা ভবিষ্যতে সম্প্রসারণ করার সম্মতি দিলাম । </td>");
            rpt.Append("</tr >");
            rpt.Append("</table>");

            rpt.Append("<table width='100%' cellspacing=0 border=0 style='padding: 0;font-family:verdana;'>");
            rpt.Append("<tr style='height:30px'>");
            rpt.Append("<td style='width: 5%;Height:70px;  font-family:Verdana;font-size:medium; text-align:justify'>কোনও জটিলতার ক্ষেত্রে অন্য কোনও ডাক্তারবাবুর সাহায্য নেওয়া হতে পারে এবং ওই ডা:বাবুর চিকিৎসায় আমার সম্মতি থাকল। জরুরি পরিস্থিতির খাতিরে,সংশ্লিষ্ট বিশেষ দরকারি সাজসজ্জাযুক্ত রোগী স্থানান্তরের ব্যবস্থা করতে পারেন । ওই ধরনের পরিস্থিতির উদ্ভব হলে আমরা প্রতিষ্ঠান কিম্বা ডাক্তারবাবুকে দায়ী করব না । প্রয়োজনীয় মেডিক্যাল, প্যাথলজিক্যাল, রেডিওলজিক্যাল, সাইটোলজিক্যাল এবং অন্যান্য অনুরূপ রোগ নির্ণয় পদ্ধতির রির্পোটের ভিত্তিতেই চিকিৎসা ও শল্য প্রযুক্তি কৌশল রচনা করেছেন সংশ্লিষ্ট ডাক্তারবাবু পরবর্তীকালে উক্ত রির্পোট জনিত চিকিৎসা ভ্রমের জন্য ডাক্তারবাবু কোনও মতেই দায়বদ্ধ থাকবেন না। আমাকে যে সমস্ত ওষুধপত্র প্রয়োগ করা হবে বা পরে ব্যবহারের জন্য নির্দেশ দেওয়া হবে, সেগুলি স্বপ্লমেয়াদী বা দীর্ঘমেয়াদী প্রতিক্রিয়া থাকতে পারে, কোনও কোনও ক্ষেত্রে এই বিরূপ প্রতিক্রিয়া মারাত্বক হতে পারে --কিন্তু আমরা জেনেছি, একজন ডাক্তারবাবু এর জন্য কোনও মতেই দায়ী থাকতে পারে না । কারোর প্ররোচনা ছাডা আমরা স্বেচ্ছায় ও সুস্থ মনে সমস্থ কিছুর মর্ম বুঝে এই অঙ্গীকার পত্রে সই করলাম। </td>");
            rpt.Append("</tr >");
            rpt.Append("</table>");

            rpt.Append("<table width='100%' cellspacing=0 border=0 style='padding: 0;font-family:verdana;'>");
            rpt.Append("<tr style='height:30px'>");
            rpt.Append("<td style='width: 5%;Height:70px;  font-family:Verdana; font-size:medium; text-align:center'> স্বাক্ষর/টিপ </td>");
            rpt.Append("<td style='width: 5%;Height:70px;  font-family:Verdana; font-size:medium; text-align:center'> পুরো  নাম  </td>");
            rpt.Append("<td style='width: 5%;Height:70px;  font-family:Verdana; font-size:medium; text-align:center'> ঠিকানা </td>");
            rpt.Append("<td style='width: 5%;Height:70px;  font-family:Verdana; font-size:medium; text-align:center'> সম্পর্ক  </td>");
            rpt.Append("</tr >");

            rpt.Append("<tr style='height:30px'>");
            rpt.Append("<td style='width: 5%;Height:70px;  font-family:Verdana;  font-size:medium; text-align:center'>..................</td>");
            rpt.AppendFormat("<td style='width: 5%;Height:70px;  font-family:Verdana;  font-size:medium; text-align:center'>{0}</td>", ds.Tables[0].Rows[0]["patient_name"]);
            rpt.AppendFormat("<td style='width: 5%;Height:70px;  font-family:Verdana; font-size:medium; text-align:center'>{0}</td>", ds.Tables[0].Rows[0]["vill_city"]);
            rpt.Append("<td style='width: 5%;Height:70px;  font-family:Verdana; font-size:medium; text-align:center'> নিজে </td>");
            rpt.Append("</tr >");

            rpt.Append("<tr style='height:30px'>");
            rpt.Append("<td style='width: 5%;Height:70px;  font-family:Verdana;  font-size:medium; text-align:center'>..................</td>");
            rpt.AppendFormat("<td style='width: 5%;Height:70px;  font-family:Verdana;  font-size:medium; text-align:center'>{0}</td>", ds.Tables[0].Rows[0]["guardian_name"]);
            rpt.AppendFormat("<td style='width: 5%;Height:70px;  font-family:Verdana; font-size:medium; text-align:center'>{0}</td>", ds.Tables[0].Rows[0]["vill_city2"]);
            rpt.AppendFormat("<td style='width: 5%;Height:70px;  font-family:Verdana; font-size:medium; text-align:center'> {0} </td>", ds.Tables[0].Rows[0]["relation"]);
            rpt.Append("</tr >");
            rpt.Append("</table>"); 
        }
         rpt.Append("<div class='break'>"); 
        rpt.Append("</div>");
        ltrReport.Visible = true;
    }

    public void GetHearder_OperationConsent1(string reg)
    {
        ltrReport.Text = "";
        DataSet ds = thepatientbill.PatientForReport(reg,Session["CoCode"].ToString().Trim());
        if (ds.Tables[0].Rows.Count > 0)
        { 

            rpt.Append("<table width='100%' cellspacing=0 border=0 style='border:1px solid black;border-spacing: 0; padding: 0;font-family:verdana;'>");
            rpt.Append("<tr style='height:25px;'>");
            rpt.Append("<td style='font-family:Verdana; font-size:x-large;text-align:center'><b>Operation, the method of the senses, in particular medical consent forms and procedures</b>");
            rpt.Append("</tr >");

            rpt.Append("<tr style='height:20px;'>");
            rpt.AppendFormat("<td style='font-family:Verdana; font-size:medium;text-align:center'>(Admitted By :{0}, Relation :{1})</td>", ds.Tables[0].Rows[0]["guardian_name"], ds.Tables[0].Rows[0]["relation"]);
            rpt.Append("</tr >");
            rpt.Append("</table>");

            PatientDetails_English(ds);
            rpt.Append("<br/>");
 
            rpt.Append("<table width='100%'>");
            rpt.Append("<tr style='height:30px'>");
            rpt.Append("<td style='width: 5%;Height:70px;  font-family:Verdana;font-size:small; text-align:justify'>I / my _____________ treatment , appropriate and satisfactory answers to all of my questions regarding the method of operation of this organization to handle my symptoms associated with getting appropriate medical care from a doctor / operations / methods having the disease given point of termination .The medical operations , procedures and processes of the unconscious and the potential risks associated with medical procedures , complications and consequences of the trouble I 've been informed and know some of these operations or unforeseen conditions that may occur during treatment so that the East - in particular the needs of the other , or the procedure to be paribarjanera could . Professional discretion of the physician or surgeon or the necessary medical care they need and salyapaddhati sahakarmiganake took , she proceeded to ask me about the willingness and .In the case of any medical or surgery , bleeding , sepsis , immobility , etc. that are at risk of heart , they also informed me that the case may be , and have been completely cured of my physical illness, no guarantees or promises were given to me . During my treatment without any complications so I daktarababu and his sahakarmiganake or nursing home / hospital will not be responsible .</td>");

            rpt.Append("</tr >");
            rpt.Append("</table>");



            rpt.Append("<table width='100%' cellspacing=0 border=0 style='padding: 0;font-family:verdana;'>");
            rpt.Append("<tr style='height:30px'>");
            rpt.Append("<td style='width: 5%;Height:70px;  font-family:Verdana;font-size:small; text-align:justify'>I would agree that it is necessary and appropriate that these services can be applied to any drug of unconsciousness. I think that, implementation of ignorance and unconscious procedures and operations as a result of complications during a brain and can lead to loss of teeth. </td>");

            rpt.Append("</tr >");
            rpt.Append("</table>");


            rpt.Append("<table width='100%' cellspacing=0 border=0 style='padding: 0;font-family:verdana;'>");
            rpt.Append("<tr style='height:30px'>");
            rpt.Append("<td style='width: 5%;Height:70px;  font-family:Verdana; font-size:small; text-align:justify'>Isolated body parts so that no organ or nursing home / hospital authorities can withdraw them in accordance with conventional methods, she stuck with me and I agreed.</td>");

            rpt.Append("</tr >");
            rpt.Append("</table>");


            rpt.Append("<table width='100%' cellspacing=0 border=0 style='padding: 0;font-family:verdana;'>");
            rpt.Append("<tr style='height:30px'>");
            rpt.Append("<td style='width: 5%;Height:70px;  font-family:Verdana;font-size:small; text-align:justify'>Lyaparoskopi or historoskopi or telescope or micro surgery During surgery systemic complications arise - the traditional way to follow surgery to allow the surgical cut made ​​in the stomach. Stagnating in the heart of the serious complications of the surgery can also do.</td>");
            rpt.Append("</tr >");
            rpt.Append("</table>");

            rpt.Append("<table width='100%' cellspacing=0 border=0 style='padding: 0;font-family:verdana;'>");
            rpt.Append("<tr style='height:30px'>");
            rpt.Append("<td style='width: 5%;Height:70px;  font-family:Verdana;font-size:small; text-align:justify'>While I was on OT-OT-observers on medical necessity or the other educational or research purposes as well as to allow access to the process of operation and my body / part of the body, taking the photograph and the photograph of me revealing the future to expand or accepted immediately.</td>");
            rpt.Append("</tr >");
            rpt.Append("</table>");

            rpt.Append("<table width='100%' cellspacing=0 border=0 style='padding: 0;font-family:verdana;'>");
            rpt.Append("<tr style='height:30px'>");
            rpt.Append("<td style='width: 5%;Height:70px;  font-family:Verdana;font-size:small; text-align:justify'>In case of any problems , and the other can be taken to help daktarababura Dr. Babu treatment began my consent . For emergency situations , associated particularly useful to transfer the patient to the decor . If such a situation that we will not be responsible institutions or daktarababuke . Medically necessary , pyathalajikyala , rediolajikyala , saitolajikyala and other similar methods of diagnosis based on the medical and surgical technology techniques rirpotera Related daktarababu subsequently wrote an account of the rirpota induced daktarababu not be responsible for medical oversight . That would be me after all the medicines should be given to the use of , or the long-term effects they may have sbaplameyadi , in some cases, these adverse reactions may be serious - but as we know , no one stays for a daktarababu can not be held liable . Provoking someone to quit voluntarily and healthy mind , we all understand the meaning of these things, I signed the letter of commitment . </td>");
            rpt.Append("</tr >");
            rpt.Append("</table>");

            rpt.Append("<table width='100%' cellspacing=0 border=0 style='padding: 0;font-family:verdana;'>");
            rpt.Append("<tr style='height:30px'>");
            rpt.Append("<td style='width: 5%;Height:70px;  font-family:Verdana; font-size:medium; text-align:center'> Signature </td>");
            rpt.Append("<td style='width: 5%;Height:70px;  font-family:Verdana; font-size:medium; text-align:center'> Full Name  </td>");
            rpt.Append("<td style='width: 5%;Height:70px;  font-family:Verdana; font-size:medium; text-align:center'> Address </td>");
            rpt.Append("<td style='width: 5%;Height:70px;  font-family:Verdana; font-size:medium; text-align:center'> Relation  </td>");
            rpt.Append("</tr >");

            rpt.Append("<tr style='height:30px'>");
            rpt.Append("<td style='width: 5%;Height:70px;  font-family:Verdana;  font-size:medium; text-align:center'>..................</td>");
            rpt.AppendFormat("<td style='width: 5%;Height:70px;  font-family:Verdana;  font-size:medium; text-align:center'>{0}</td>", ds.Tables[0].Rows[0]["patient_name"]);
            rpt.AppendFormat("<td style='width: 5%;Height:70px;  font-family:Verdana; font-size:medium; text-align:center'>{0}</td>", ds.Tables[0].Rows[0]["vill_city"]);
            rpt.Append("<td style='width: 5%;Height:70px;  font-family:Verdana; font-size:medium; text-align:center'> Self </td>");
            rpt.Append("</tr >");

            rpt.Append("<tr style='height:30px'>");
            rpt.Append("<td style='width: 5%;Height:70px;  font-family:Verdana;  font-size:medium; text-align:center'>..................</td>");
            rpt.AppendFormat("<td style='width: 5%;Height:70px;  font-family:Verdana;  font-size:medium; text-align:center'>{0}</td>", ds.Tables[0].Rows[0]["guardian_name"]);
            rpt.AppendFormat("<td style='width: 5%;Height:70px;  font-family:Verdana; font-size:medium; text-align:center'>{0}</td>", ds.Tables[0].Rows[0]["vill_city2"]);
            rpt.AppendFormat("<td style='width: 5%;Height:70px;  font-family:Verdana; font-size:medium; text-align:center'> {0} </td>", ds.Tables[0].Rows[0]["relation"]);
            rpt.Append("</tr >");
            rpt.Append("</table>"); 
        } 
        ltrReport.Visible = true;

    }

    public void GetHearder_Ligation(string regno)
    {
        ltrReport.Text = "";
        DataSet ds = thepatientbill.PatientForReport(regno, Session["CoCode"].ToString().Trim());
        if (ds.Tables[0].Rows.Count > 0)
        { 
            rpt.Append("<table width='100%' cellspacing=0 border=0 style='border:1px solid black;border-spacing: 0; padding: 0;font-family:verdana;'>");
            rpt.Append("<tr style='height:25px;'>");
            rpt.Append("<td style='font-family:Verdana; font-size:x-large;text-align:center'><b>বন্ধ্যাত্বকরণ বা বাচ্চা বন্ধের জন্য আবেদন</b>");
            rpt.Append("</tr >");

            rpt.Append("<tr style='height:20px;'>");
            rpt.AppendFormat("<td style='font-family:Verdana; font-size:medium;text-align:center'>(যিনি ভর্তি করাচ্ছেন তার নাম:{0},সম্পর্ক:{1})</td>", ds.Tables[0].Rows[0]["guardian_name"], ds.Tables[0].Rows[0]["relation"]);
            rpt.Append("</tr >");
            rpt.Append("</table>");


            PatientDetails_Bengali(ds);

            rpt.Append("<br/>");
            rpt.Append("<table width='100%'>");
            rpt.Append("<tr style='height:35px'>");
            rpt.Append("<td style='font-family:Verdana;font-size:medium; text-align:left'>মহাশয়/মহাশয়া, </td>");
            rpt.Append("</tr >");
            rpt.Append("<tr style='height:25px'>");
            rpt.AppendFormat("<td style='font-family:Verdana;font-size:medium; text-align:justify'>আমি বিবাহিত এবং আমার স্বামী বেঁচে আছেন । আমার বয়স <b> {0} বছর </b> , আমার স্বামীর বয়স _________বছর । বাড়িতে আমাদের _________টি পুত্র সন্তান ________ টি কন্যাসন্তান রয়েছে । সব থেকে কনিষ্ঠ সন্তানের বর্তমান বয়স________ বছর । তাই বন্ধ্যাত্বকরণ তথা বাচ্চা বন্ধের প্রযোজনীয় ব্যবস্থা করার জন্য আপনার কাছে প্রার্থনা করছি । প্রসঙ্গক্রমে আপনাকে জানাই :</td>", ds.Tables[0].Rows[0]["age"]);
            rpt.Append("</tr >");

            rpt.Append("<tr style='height:25px'>");
            rpt.Append("<td style='font-family:Verdana;font-size:medium; text-align:justify'><b>(১)</b> এই বন্ধাত্বকরনের সিদ্ধান্তটি আমার নিজের।আমি স্বতঃস্ফূর্তভাবে এই সিদ্ধান্ত নিয়েছি। এই ব্যাপারে আমাকে কেউ চাপ দেয়নি বা বাধ্য করেনি । </td>");
            rpt.Append("</tr >");

            rpt.Append("<tr style='height:25px'>");
            rpt.Append("<td style='font-family:Verdana;font-size:medium; text-align:justify'><b>(২)</b> বন্ধ্যাত্বকরণ বা বাচ্চা বন্ধের অন্যান্য প্রক্রিয়া সম্বন্ধে আমি অবগত। অন্যান্য প্রক্রিয়া সম্বন্ধে আমাকে ভালভাবে বোঝানো হয়েছে । </td>");
            rpt.Append("</tr >");

            rpt.Append("<tr style='height:25px'>");
            rpt.Append("<td style='font-family:Verdana;font-size:medium; text-align:justify'><b>(৩)</b> বন্ধ্যাত্বকরণ বা বাচ্চা বন্ধ করতে হলে যে যে মানসিক ও শারীরিক সুস্থতার প্রয়োজন তা আমাকে ভালভাবে বোঝানো হয়েছে । ওই সব শোনার পর আমার মনে হয়েছে, এই মুহুর্তে আমি বন্ধ্যাত্বকরণ বা বাচ্চা বন্ধ করানোর পক্ষেই উপযুক্ত । </td>");
            rpt.Append("</tr >");

            rpt.Append("<tr style='height:25px'>");
            rpt.Append("<td style='font-family:Verdana;font-size:medium; text-align:justify'><b>(৪)</b> বন্ধ্যাত্বকরণ বা বাচ্চা বন্ধের উদ্দেশ্য আমি ভালভাবে জানি।এও জানি এই অপারেশনের পর আমি কোনদিন বাচ্চাধারণ করতে পারব না ।  </td>");
            rpt.Append("</tr >");

            rpt.Append("<tr style='height:25px'>");
            rpt.Append("<td style='font-family:Vrinda;font-size:medium; text-align:justify'><b>(৫)</b> আমি জানি এই অপারেশন পুরোপুরি ত্রুটিমুক্ত নয়। এই বন্ধ্যাত্বকরন বা বাচ্চাবন্ধের অপারেশনের পরও কোনও কোনও ক্ষেত্রে বিশেষ কারণে আবার গর্ভবতী হয়ে যেতে পারি। ডাক্তার বাবুর কাছ থেকে জেনেছি যে, এই ব্যতিক্রমি বা বিরল ঘটনার কথা চিকিৎসা বিজ্ঞানে উল্লেখ আছে । তাই তার জন্য আমি, আমার স্বামী, আমার অন্যান্য আত্মীয় কিম্ব্বা পরিচিতরা সংশ্লিষ্ট ডাক্তারবাবু বা এই প্রতিষ্ঠানকে দায়ী করব না বা করবেন না। এই অপারেশনের পর আমার নিয়মিত ঋতুস্রাবের কোনও অনিয়ম ব্যাঘাত ঘটলে আমি পনেরো দিনের মধ্যে এই প্রতিষ্ঠানে বা সংশ্লিষ্ট ডাক্তারবাবুকে জানাব। </td>");
            rpt.Append("</tr >");

            rpt.Append("<tr style='height:25px'>");
            rpt.Append("<td style='font-family:Vrinda;font-size:medium; text-align:justify'><b>(৬)</b> এর আগে আমার স্বামী বাচ্চাবন্ধের অপারেশন করাননি। </td>");
            rpt.Append("</tr >");

            rpt.Append("<tr style='height:25px'>");
            rpt.Append("<td style='font-family:Vrinda;font-size:medium; text-align:justify'><b>(৭)</b> আমি জানি এই অপারেশনে অনেক ঝুঁকি আছে এমন কি প্রাণহানি পর্যন্ত হতে পারে ।  </td>");
            rpt.Append("</tr >");

            rpt.Append("<tr style='height:25px'>");
            rpt.Append("<td style='font-family:Verdana;font-size:medium; text-align:justify'><b>(৮)</b> এই প্রতিষ্ঠানের ডাক্তার বাবুর নির্দেশ মতো অপারেশনের পর নিয়মিত চেক্-আপ করতে বা দেখাতে আসব । যদি না আসতে পারি তাহলে কোনও কিছু অঘটন বা অসুবিধা হলে তার জন্য এই প্রতিষ্ঠান বা ডাক্তারবাবু দায়ী থাকবেন না।  </td>");
            rpt.Append("</tr >");

            rpt.Append("<tr style='height:25px'>");
            rpt.Append("<td style='font-family:Verdana;font-size:medium; text-align:justify'><b>(৯)</b> এই বন্ধ্যাত্বকরণ বা বাচ্চা বন্ধের অপারেশনের জন্য সংশ্লিষ্ট ডাক্তারবাবু আমাকে তাঁর সুবিধা মতো যে কোনও ধরণের অজ্ঞান করতে এবং ওষুধ প্রদান ও প্রয়োগ করতে পারেন , সে ক্ষেত্রে আমার কোনও আপত্তি থাকবে না ।  </td>");
            rpt.Append("</tr >");

            rpt.Append("<tr style='height:25px'>");
            rpt.Append("<td style='font-family:Verdana;font-size:medium; text-align:justify'><b>(১০)</b> আমাকে পুরোপুরি জানানো হয়েছে -- আমাকে যে সমস্ত ওষুধপত্র প্রয়োগ করা হবে বা পরে ব্যবহারের জন্য নির্দেশ দেওয়া হবে , সেগুলি স্বপ্লমেয়াদী বা দীর্ঘমেয়াদী প্রতিক্রিয়া থাকতে পারে, কোনও কোনও ক্ষেত্রে এই বিরূপ প্রতিক্রিয়া মারাত্বক হতে পারে --কিন্তু এক জন ডাক্তারবাবু এর জন্য কোনও মতেই দায়ী হতে পারে না । আশাকরি আমার এই আবেদনের প্রেক্ষিতে আপনার সুবিধে মতো সময়ে আমার বন্ধ্যাত্বকরণের তথা বাচ্চা বন্ধের অপারেশানের জন্য প্রয়োজনীয় পদ্ধতিগত ব্যবস্থা গ্রহণ করবেন। </td>");
            rpt.Append("</tr >");

            rpt.Append("<tr style='height:25px'>");
            rpt.AppendFormat("<td style=' font-family:Verdana;font-size:medium; text-align:right'>ইতি--------- {0}</td>", ds.Tables[0].Rows[0]["patient_name"]);
            rpt.Append("</tr >");
            rpt.Append("</table>");




            rpt.Append("<table width='100%'>");
            rpt.Append("<tr style='height:27px'>");
            rpt.Append("<td style='font-family:Verdana; font-size:medium; text-align:center'> স্বাক্ষর/টিপ </td>");
            rpt.Append("<td style='font-family:Verdana; font-size:medium; text-align:center'> পুরো  নাম  </td>");
            rpt.Append("<td style='font-family:Verdana; font-size:medium; text-align:center'> ঠিকানা </td>");
            rpt.Append("<td style='font-family:Verdana; font-size:medium; text-align:center'> সম্পর্ক  </td>");
            rpt.Append("</tr >");

            rpt.Append("<tr style='height:27px'>");
            rpt.Append("<td style='font-family:Verdana;  font-size:small; text-align:center'>..................</td>");
            rpt.AppendFormat("<td style='font-family:Verdana;  font-size:small; text-align:center'>{0}</td>", ds.Tables[0].Rows[0]["patient_name"]);
            rpt.AppendFormat("<td style='font-family:Verdana; font-size:small; text-align:center'>{0}</td>", ds.Tables[0].Rows[0]["vill_city"]);
            rpt.Append("<td style='font-family:Verdana; font-size:small; text-align:center'> নিজে </td>");
            rpt.Append("</tr >");

            rpt.Append("<tr style='height:27px'>");
            rpt.Append("<td style='font-family:Verdana;  font-size:small; text-align:center'>..................</td>");
            rpt.AppendFormat("<td style='font-family:Verdana;  font-size:small; text-align:center'>{0}</td>", ds.Tables[0].Rows[0]["guardian_name"]);
            rpt.AppendFormat("<td style='font-family:Verdana; font-size:small; text-align:center'>{0}</td>", ds.Tables[0].Rows[0]["vill_city2"]);
            rpt.AppendFormat("<td style='font-family:Verdana; font-size:small; text-align:center'> {0} </td>", ds.Tables[0].Rows[0]["relation"]);
            rpt.Append("</tr >");
            rpt.Append("</table>");
        }
        rpt.Append("</div>");
        ltrReport.Visible = true;
    }

    public void GetHearder_Ligation1(string reg)
    {
        ltrReport.Text = "";
        DataSet ds = thepatientbill.PatientForReport(reg, Session["CoCode"].ToString().Trim());
        if (ds.Tables[0].Rows.Count > 0)
        {
            rpt.Append("<table width='100%' cellspacing=0 border=0 style='border:1px solid black;border-spacing: 0; padding: 0;font-family:verdana;'>");
            rpt.Append("<tr style='height:25px;'>");
            rpt.Append("<td style='font-family:Verdana; font-size:x-large;text-align:center'><b>Ligation Form</b>");
            rpt.Append("</tr >");

            rpt.Append("<tr style='height:20px;'>");
            rpt.AppendFormat("<td style='font-family:Verdana; font-size:medium;text-align:center'>(Admitted By :{0}, Relation :{1})</td>", ds.Tables[0].Rows[0]["guardian_name"], ds.Tables[0].Rows[0]["relation"]);
            rpt.Append("</tr >");
            rpt.Append("</table>");

            PatientDetails_English(ds);
            rpt.Append("<br/>");
 
            rpt.Append("<table width='100%'>");
            rpt.Append("<tr style='height:35px'>");
            rpt.Append("<td style='font-family:Verdana;font-size:medium; text-align:left'>Dear Sir / Madam, </td>");
            rpt.Append("</tr >");
            rpt.Append("<tr style='height:25px'>");
            rpt.AppendFormat("<td style='font-family:Verdana;font-size:small; text-align:justify'>I am married and my husband alive.<b> {0}  </b> of my age, my husband _________ years of age. ________ Son of _________ of us have daughters at home. ________ Years from the current age of the youngest child. So to make the necessary arrangements to stop children of bandhyatbakarana pray. By the way you are:</td>", ds.Tables[0].Rows[0]["age"]);
            rpt.Append("</tr >");

            rpt.Append("<tr style='height:25px'>");
            rpt.Append("<td style='font-family:Verdana;font-size:small; text-align:justify'> <b>(1)</b>  This bandhatbakaranera my own decision., I decided spontaneously to this. This has forced me to or not having one. </td>");
            rpt.Append("</tr >");

            rpt.Append("<tr style='height:25px'>");
            rpt.Append("<td style='font-family:Verdana;font-size:small; text-align:justify'><b>(2)</b>   Bandhyatbakarana or child I knew about the process of closing the other. Other process has meant to me, as well. </td>");
            rpt.Append("</tr >");

            rpt.Append("<tr style='height:25px'>");
            rpt.Append("<td style='font-family:Verdana;font-size:small; text-align:justify'><b>(3)</b>   Bandhyatbakarana to stop or if the child requires that the mental and physical health, it has meant to me, as well. After listening to all of that, I thought, this is the moment I stop or baby into bandhyatbakarana appropriate side.</td>");
            rpt.Append("</tr >");

            rpt.Append("<tr style='height:25px'>");
            rpt.Append("<td style='font-family:Verdana;font-size:small; text-align:justify'><b>(4)</b>   I well know that the purpose of closing bandhyatbakarana or child. Baccadharana also know that someday I will be able to do this after the operation.  </td>");
            rpt.Append("</tr >");

            rpt.Append("<tr style='height:25px'>");
            rpt.Append("<td style='font-family:Verdana;font-size:small; text-align:justify'><b>(5)</b>   I know that this operation is not quite correct . This bandhyatbakarana or have any special reason baccabandhera after the operation can become pregnant again . Babu, learned from doctors that are mentioned in this exceptional or rare cases of medical science . So for me, my husband , my other relatives daktarababu or institution concerned kimbba contacts will not be liable or not . After this operation I have my regular period of fifteen days of the occurrence of an irregularity, interruption or the institution concerned shall daktarababuke . </td>");
            rpt.Append("</tr >");

            rpt.Append("<tr style='height:25px'>");
            rpt.Append("<td style='font-family:Verdana;font-size:small; text-align:justify'><b>(6)</b>   Prior to my husband karanani baccabandhera operations. </td>");
            rpt.Append("</tr >");

            rpt.Append("<tr style='height:25px'>");
            rpt.Append("<td style='font-family:Verdana;font-size:small; text-align:justify'><b>(7)</b>   I know there are many risks to this operation may even be killed.  </td>");
            rpt.Append("</tr >");

            rpt.Append("<tr style='height:25px'>");
            rpt.Append("<td style='font-family:Verdana;font-size:small; text-align:justify'><b>(8)</b>   This bandhyatbakarana or closing operations associated daktarababu child like me any sort of advantage to his senses and medicine as directed pradaei babu business operation after doctors regularly check - will show up or not. If you do not have to come if something strange or difficult for him not to be responsible for this institution or daktarababu.</td>");
            rpt.Append("</tr >");

            rpt.Append("<tr style='height:25px'>");
            rpt.Append("<td style='font-family:Verdana;font-size:small; text-align:justify'><b>(9)</b>   This bandhyatbakarana or closing operations associated daktarababu child like me any sort of advantage to his senses and to apply the medicine and can also, in that case, I do not have any objections.  </td>");
            rpt.Append("</tr >");

            rpt.Append("<tr style='height:25px'>");
            rpt.Append("<td style='font-family:Verdana;font-size:small; text-align:justify'><b>(10)</b>   I have been fully informed - to me that would be applied to all medicines should be given to the use of , or after , the long-term effects they may have sbaplameyadi or , in some cases, these adverse reactions may be serious - but no way for one person to be responsible daktarababu does not . Hopefully, in the context of this application, you can make me cum like a child at the time of closing my bandhyatbakaranera aparesanera necessary procedural steps to take .</td>");
            rpt.Append("</tr >");

            rpt.Append("<tr style='height:25px'>");
            rpt.AppendFormat("<td style=' font-family:Verdana;font-size:small; text-align:right'>Finish--------- {0}</td>", ds.Tables[0].Rows[0]["patient_name"]);
            rpt.Append("</tr >");
            rpt.Append("</table>");




            rpt.Append("<table width='100%'>");
            rpt.Append("<tr style='height:27px'>");
            rpt.Append("<td style='font-family:Verdana; font-size:medium; text-align:center'> Signature </td>");
            rpt.Append("<td style='font-family:Verdana; font-size:medium; text-align:center'> Full Name  </td>");
            rpt.Append("<td style='font-family:Verdana; font-size:medium; text-align:center'> Address </td>");
            rpt.Append("<td style='font-family:Verdana; font-size:medium; text-align:center'> Relation  </td>");
            rpt.Append("</tr >");

            rpt.Append("<tr style='height:27px'>");
            rpt.Append("<td style='font-family:Verdana;  font-size:small; text-align:center'>..................</td>");
            rpt.AppendFormat("<td style='font-family:Verdana;  font-size:small; text-align:center'>{0}</td>", ds.Tables[0].Rows[0]["patient_name"]);
            rpt.AppendFormat("<td style='font-family:Verdana; font-size:small; text-align:center'>{0}</td>", ds.Tables[0].Rows[0]["vill_city"]);
            rpt.Append("<td style='font-family:Verdana; font-size:small; text-align:center'> Self </td>");
            rpt.Append("</tr >");

            rpt.Append("<tr style='height:27px'>");
            rpt.Append("<td style='font-family:Verdana;  font-size:small; text-align:center'>..................</td>");
            rpt.AppendFormat("<td style='font-family:Verdana;  font-size:small; text-align:center'>{0}</td>", ds.Tables[0].Rows[0]["guardian_name"]);
            rpt.AppendFormat("<td style='font-family:Verdana; font-size:small; text-align:center'>{0}</td>", ds.Tables[0].Rows[0]["vill_city2"]);
            rpt.AppendFormat("<td style='font-family:Verdana; font-size:small; text-align:center'> {0} </td>", ds.Tables[0].Rows[0]["relation"]);
            rpt.Append("</tr >");
            rpt.Append("</table>"); 
        }
        ltrReport.Visible = true;
    }

    public void GetHearder_APHReport(string reg)
    {
        ltrReport.Text = "";
        DataSet ds = thepatientbill.PatientForReport(reg, Session["CoCode"].ToString().Trim());
        if (ds.Tables[0].Rows.Count > 0)
        {
            rpt.Append("<table width='100%' cellspacing=0 border=0 style='border:1px solid black;border-spacing: 0; padding: 0;font-family:verdana;'>");
            rpt.Append("<tr style='height:25px;'>");
            rpt.Append("<td style='font-family:Verdana; font-size:x-large;text-align:center'><b>রোগীর বিশেষ কয়েকটি আশঙ্কাজনক পরিস্থিতি ও তার ঝুঁকি সম্পর্কে স্বীকার পত্র</b>");
            rpt.Append("</tr >");

            rpt.Append("<tr style='height:20px;'>");
            rpt.AppendFormat("<td style='font-family:Verdana; font-size:medium;text-align:center'>(যিনি ভর্তি করাচ্ছেন তার নাম:{0},সম্পর্ক:{1})</td>", ds.Tables[0].Rows[0]["guardian_name"], ds.Tables[0].Rows[0]["relation"]);
            rpt.Append("</tr >");
            rpt.Append("</table>");


            PatientDetails_Bengali(ds);
            rpt.Append("<br/>");
            rpt.Append("<table width='100%'>");
            rpt.Append("<tr style='height:25px'>"); rpt.Append("<u>");
            rpt.Append("<td style='font-family:Verdana;font-size:medium;font-weight:bold; text-align:left'>রোগীর সমস্যা বা ঝুঁকির কারণ সমূহ: </td>"); rpt.Append("</u>");
            rpt.Append("</tr >");

            rpt.Append("<tr style='height:35px'>");
            rpt.Append("<td style='font-family:Verdana;font-size:medium; text-align:left'> ফুল নিচের দিকে আছে এবং যে কোন সময়ে প্রচন্ড রক্তস্রাবের সম্ভাবনা, ডেলিভারীর এখনো অনেক দেরী তাই বাচ্চা অপরিনত, যদি ওষুধে ব্লিডিং বন্ধ না হয় বাচ্চা বাঁচবে না জেনে ও সীজার করতে হতে পারে মাকে বাঁচানোর জন্যে,এখুনি প্রচুর রক্ত লাগবে, এই রক্ত জোগাড় করার দায়িত্ব রোগীর বাড়ীর লোককেই নিতে হবে, রক্ত জোগাড় করতে যত দেরি হবে রোগীর বিপদ তত বাড়বে </td>");
            rpt.Append("</tr >");

            rpt.Append("<tr style='height:35px'>"); rpt.Append("<u>");
            rpt.Append("<td style='font-family:Verdana;font-size:medium;font-weight:bold;text-align:left'>ওই সমস্ত রোগীর যে সমস্ত বিপত্তি ঘটতে পারে :</td>"); rpt.Append("</u>");
            rpt.Append("</tr >");

            rpt.Append("<tr style='height:35px'>");
            rpt.Append("<td style='font-family:Verdana;font-size:medium; text-align:left'> প্রচন্ড রক্তস্রাবের জন্য শরীর রক্তশূণ্য হয়ে যেতে পারে, হঠাৎ রোগীর হৃদ স্পন্দন থেমে যেতে পারে, রোগী মারা যেতে পারে, বাচ্চাও মারা যেতে পারে, ফুসফুস কিডনি লিভার বিকল হতে পারে, যে কোন মুহূর্তে রোগীকে অন্যত্র নিয়ে যেতে হতে পারে, বাচ্চা অপরিনত ও দূর্বল হতে পারে এবং মারাও যেতে পারে, খুব বিরল ক্ষেত্রে রোগীর জীবনের স্বার্থে জরায়ূ কেটে বাদ দিতে হতে পারে, এই রোগের চিকিৎসার কোন গ্যারান্টী নেই </td>");
            rpt.Append("</tr >");
            rpt.Append("</table>");
            rpt.Append("<br/>");
            rpt.Append("<table width='100%'>");
            rpt.Append("<tr style='height:30px'>");
            rpt.Append("<td style='font-family:Verdana;font-size:medium; text-align:justify'>এই প্রতিষ্ঠানের পক্ষ থেকে রোগীর এই সমস্ত সমস্যা ও আশঙ্কার কথা জানলাম । এই প্রতিষ্ঠানের পক্ষ থেকে আমাদের বারবার কোন সরকারী মেডিকেল কলেজ কিংবা অন্য কোনও স্বাস্থ্য প্রতিষ্ঠানের নিয়ে যাবার কথা বলা হয়েছে , আমরাও এই প্রতিষ্ঠান যে সমস্ত পরামর্শ দিয়েছে তারসঙ্গে একমত । কিন্তু এই মূহুর্তে রোগীকে অন্যত্র নিয়ে যাবার মতো পরিস্থিতি আমাদের নেই , তাই রোগীর জীবন হানী সহ সমস্ত ঝুঁকির কথা মাথায় রেখেই আমরা আপদকালীন চিকিৎ্সার জন্য রোগীকে এই প্রতিষ্ঠানে ভর্তি করলাম , এখানে রোগীর প্রয়োজনীয় যাবতীয় চিকিত্সা করার জন্য অনুমতি দিলাম । </td>");
            rpt.Append("</tr >");
            rpt.Append("</table>");
            rpt.Append("<br/>");
            rpt.Append("<table width='100%'>");
            rpt.Append("<tr style='height:30px'>");
            rpt.Append("<td style=' font-family:Verdana;font-size:medium; text-align:justify'>আমরা যেহেতু সুস্থ মনে আমাদের আত্মীয় পরিজনদের সঙ্গে আলোচনা করে রোগীর চিকিত্সা চালানোর অনুমতি দিচ্ছি সেক্ষেত্রে চিকিত্সা চলাকালীন কিংবা এই প্রতিষ্ঠান থেকে ছাড়া পাওয়ার পর রোগীর অন্য কোন জটিল সম্যসা হলে কিংবা জীবন সংশয় হলে আমরা এই প্রতিষ্ঠানকে দায়ী করব না ।</td>");
            rpt.Append("</tr >");
            rpt.Append("</table>");




            rpt.Append("<table width='100%'>");
            rpt.Append("<tr style='height:35px'>");
            rpt.Append("<td style='font-family:Verdana; font-size:medium; text-align:center'> স্বাক্ষর/টিপ </td>");
            rpt.Append("<td style='font-family:Verdana; font-size:medium; text-align:center'> পুরো  নাম  </td>");
            rpt.Append("<td style='font-family:Verdana; font-size:medium; text-align:center'> ঠিকানা </td>");
            rpt.Append("<td style='font-family:Verdana; font-size:medium; text-align:center'> সম্পর্ক  </td>");
            rpt.Append("</tr >");

            rpt.Append("<tr style='height:27px'>");
            rpt.Append("<td style='font-family:Verdana;  font-size:small; text-align:center'>..................</td>");
            rpt.AppendFormat("<td style='font-family:Verdana;  font-size:small; text-align:center'>{0}</td>", ds.Tables[0].Rows[0]["patient_name"]);
            rpt.AppendFormat("<td style='font-family:Verdana; font-size:small; text-align:center'>{0}</td>", ds.Tables[0].Rows[0]["vill_city"]);
            rpt.Append("<td style='font-family:Verdana; font-size:small; text-align:center'> নিজে </td>");
            rpt.Append("</tr >");

            rpt.Append("<tr style='height:27px'>");
            rpt.Append("<td style='font-family:Verdana;  font-size:small; text-align:center'>..................</td>");
            rpt.AppendFormat("<td style='font-family:Verdana;  font-size:small; text-align:center'>{0}</td>", ds.Tables[0].Rows[0]["guardian_name"]);
            rpt.AppendFormat("<td style='font-family:Verdana; font-size:small; text-align:center'>{0}</td>", ds.Tables[0].Rows[0]["vill_city2"]);
            rpt.AppendFormat("<td style='font-family:Verdana; font-size:small; text-align:center'> {0} </td>", ds.Tables[0].Rows[0]["relation"]);
            rpt.Append("</tr >");
            rpt.Append("</table>"); 
        }
        ltrReport.Visible = true;
    }

    public void GetHearder_APHReport1(string reg)
    {
        ltrReport.Text = "";
        DataSet ds = thepatientbill.PatientForReport(reg, Session["CoCode"].ToString().Trim());
        if (ds.Tables[0].Rows.Count > 0)
        {
            rpt.Append("<table width='100%' cellspacing=0 border=0 style='border:1px solid black;border-spacing: 0; padding: 0;font-family:verdana;'>");
            rpt.Append("<tr style='height:25px;'>");
            rpt.Append("<td style='font-family:Verdana; font-size:x-large;text-align:center'><b>Some of the critical situation of the patient about the risks and accept the Letter</b>");
            rpt.Append("</tr >");

            rpt.Append("<tr style='height:20px;'>");
            rpt.AppendFormat("<td style='font-family:Verdana; font-size:medium;text-align:center'>(Admitted By :{0}, Relation :{1})</td>", ds.Tables[0].Rows[0]["guardian_name"], ds.Tables[0].Rows[0]["relation"]);
            rpt.Append("</tr >");
            rpt.Append("</table>");

            PatientDetails_English(ds);
            rpt.Append("<br/>");

            rpt.Append("<table width='100%'>");
            rpt.Append("<tr style='height:25px'>"); rpt.Append("<u>");
            rpt.Append("<td style='font-family:Verdana;font-size:medium;font-weight:bold; text-align:left'>Problems or risk factors of the patient  : </td>"); rpt.Append("</u>");
            rpt.Append("</tr >");

            rpt.Append("<tr style='height:25px'>");
            rpt.Append("<td style='font-family:Verdana;font-size:small; text-align:left'>Flowers are at the bottom and at the possibility of severe bleeding, delivery is still so far away, immature child, the medication will not stop bleeding, and Caesar without the child may live to save his mother, would take away a lot of blood, the blood of the patient's responsibility to obtain will take the children home, to get the blood to be delayed as the greater danger to the patient increase </td>");
            rpt.Append("</tr >");

            rpt.Append("<tr style='height:25px'>"); rpt.Append("<u>");
            rpt.Append("<td style='font-family:Verdana;font-size:medium;font-weight:bold;text-align:left'>Hazard that may occur in the patient  :</td>"); rpt.Append("</u>");
            rpt.Append("</tr >");

            rpt.Append("<tr style='height:25px'>");
            rpt.Append("<td style='font-family:Verdana;font-size:small; text-align:left'>Severe bleeding may raktasunya the body , the patient suddenly stopped heart rhythm may be , the patient could die , the baby can die , lungs, kidneys, liver damage may be , may be taken away at any moment the patient , the child may be young and weak and can die for the sake of the very rare cases, a patient may be cut jarayu , treatment of this disease, there is no guarantee</td>");
            rpt.Append("</tr >");
            rpt.Append("</table>");
            rpt.Append("<br/>");
            rpt.Append("<table width='100%'>");
            rpt.Append("<tr style='height:25px'>");
            rpt.Append("<td style='font-family:Verdana;font-size:small; text-align:justify'> The organization learned from the patient about these issues and concerns . This organization has repeatedly us from any government medical college or any other health organizations have been asked to take , we suggest that these institutions have agreed along . But now the situation is like taking away patients who do not have , so we are keeping in mind the patient's life is at risk , including hostile cikitsara emergency admissions for patients who have made this institution , the patient gave permission for the necessary treatment .  </td>");
            rpt.Append("</tr >");
            rpt.Append("</table>");
            rpt.Append("<br/>");
            rpt.Append("<table width='100%'>");
            rpt.Append("<tr style='height:25px'>");
            rpt.Append("<td style=' font-family:Verdana;font-size:small; text-align:justify'> We think it's healthy to talk with our kindred allowed to treat the patient during treatment, or in this case, the organization would be without the complicated error occurred when the patient or any other life-threatening if not, we'll blame the institution. </td>");
            rpt.Append("</tr >");
            rpt.Append("</table>");


            rpt.Append("<table width='100%'>");
            rpt.Append("<tr style='height:27px'>");
            rpt.Append("<td style='font-family:Verdana; font-size:medium; text-align:center'> Signature </td>");
            rpt.Append("<td style='font-family:Verdana; font-size:medium; text-align:center'> Full Name  </td>");
            rpt.Append("<td style='font-family:Verdana; font-size:medium; text-align:center'> Address </td>");
            rpt.Append("<td style='font-family:Verdana; font-size:medium; text-align:center'> Relation  </td>");
            rpt.Append("</tr >");

            rpt.Append("<tr style='height:27px'>");
            rpt.Append("<td style='font-family:Verdana;  font-size:small; text-align:center'>..................</td>");
            rpt.AppendFormat("<td style='font-family:Verdana;  font-size:small; text-align:center'>{0}</td>", ds.Tables[0].Rows[0]["patient_name"]);
            rpt.AppendFormat("<td style='font-family:Verdana; font-size:small; text-align:center'>{0}</td>", ds.Tables[0].Rows[0]["vill_city"]);
            rpt.Append("<td style='font-family:Verdana; font-size:small; text-align:center'> Self </td>");
            rpt.Append("</tr >");

            rpt.Append("<tr style='height:27px'>");
            rpt.Append("<td style='font-family:Verdana;  font-size:small; text-align:center'>..................</td>");
            rpt.AppendFormat("<td style='font-family:Verdana;  font-size:small; text-align:center'>{0}</td>", ds.Tables[0].Rows[0]["guardian_name"]);
            rpt.AppendFormat("<td style='font-family:Verdana; font-size:small; text-align:center'>{0}</td>", ds.Tables[0].Rows[0]["vill_city2"]);
            rpt.AppendFormat("<td style='font-family:Verdana; font-size:small; text-align:center'> {0} </td>", ds.Tables[0].Rows[0]["relation"]);
            rpt.Append("</tr >");
            rpt.Append("</table>"); 
        } 
        ltrReport.Visible = true;
    }

    public void GetHearder_BadOrtho(string reg)
    {
        ltrReport.Text = "";
        DataSet ds = thepatientbill.PatientForReport(reg, Session["CoCode"].ToString().Trim());
        if (ds.Tables[0].Rows.Count > 0)
        {
            rpt.Append("<table width='100%' cellspacing=0 border=0 style='border:1px solid black;border-spacing: 0; padding: 0;font-family:verdana;'>");
            rpt.Append("<tr style='height:25px;'>");
            rpt.Append("<td style='font-family:Verdana; font-size:x-large;text-align:center'><b>রোগীর বিশেষ কয়েকটি আশঙ্কাজনক পরিস্থিতি ও তার ঝুঁকি সম্পর্কে স্বীকার পত্র</b>");
            rpt.Append("</tr >");

            rpt.Append("<tr style='height:20px;'>");
            rpt.AppendFormat("<td style='font-family:Verdana; font-size:medium;text-align:center'>(যিনি ভর্তি করাচ্ছেন তার নাম:{0},সম্পর্ক:{1})</td>", ds.Tables[0].Rows[0]["guardian_name"], ds.Tables[0].Rows[0]["relation"]);
            rpt.Append("</tr >");
            rpt.Append("</table>");

            PatientDetails_Bengali(ds);

            rpt.Append("<br/>");      

            rpt.Append("<table width='100%'>");
            rpt.Append("<tr style='height:25px'>"); rpt.Append("<u>");
            rpt.Append("<td style='font-family:Verdana;font-size:medium;font-weight:bold; text-align:left'>রোগীর সমস্যা বা ঝুঁকির কারণ সমূহ: </td>"); rpt.Append("</u>");
            rpt.Append("</tr >");

            rpt.Append("<tr style='height:35px'>");
            rpt.Append("<td style='font-family:Verdana;font-size:medium; text-align:left'>ইনফেকসন বা সেপশিস হতে পারে, হাড় না জুড়তে পারে, পাত বা রড খুলে দিতে হতে পারে, অজ্ঞান জনিত জটিলতা দেখা দিতে পারে</td>");
            rpt.Append("</tr >");

            rpt.Append("<tr style='height:35px'>"); rpt.Append("<u>");
            rpt.Append("<td style='font-family:Verdana;font-size:medium;font-weight:bold;text-align:left'>ওই সমস্ত রোগীর যে সমস্ত বিপত্তি ঘটতে পারে :</td>"); rpt.Append("</u>");
            rpt.Append("</tr >");

            rpt.Append("<tr style='height:35px'>");
            rpt.Append("<td style='font-family:Verdana;font-size:medium; text-align:left'> হাড় না জুড়তে পারে, পাত বা রড খুলে দিতে হতে পারে, অঙ্গ ছোট হয়ে যেতে পারে, অঙ্গের বিকৃতি ও চলনে খুঁত আসতে পারে, দীর্ঘদিন ক্ষতস্থান দিয়ে পূঁজ পড়তে পারে, একাধিকবার অপারেশন করতে হতে পারে, শ্বাসকষ্ট হতে পারে (Embolism), হঠাৎ হৃদযন্র বিকল হতে পারে,যে কোন মুহূর্তে রোগীকে অন্যত্র নিয়ে যেতে হতে পারে,চিকিৎসার কোন গ্যারান্টী দেওয়া যায় না</td>");
            rpt.Append("</tr >");
            rpt.Append("</table>");
            rpt.Append("<br/>");
            rpt.Append("<table width='100%'>");
            rpt.Append("<tr style='height:30px'>");
            rpt.Append("<td style='font-family:Verdana;font-size:medium; text-align:justify'>এই প্রতিষ্ঠানের পক্ষ থেকে রোগীর এই সমস্ত সমস্যা ও আশঙ্কার কথা জানলাম । এই প্রতিষ্ঠানের পক্ষ থেকে আমাদের বারবার কোন সরকারী মেডিকেল কলেজ কিংবা অন্য কোনও স্বাস্থ্য প্রতিষ্ঠানের নিয়ে যাবার কথা বলা হয়েছে , আমরাও এই প্রতিষ্ঠান যে সমস্ত পরামর্শ দিয়েছে তারসঙ্গে একমত । কিন্তু এই মূহুর্তে রোগীকে অন্যত্র নিয়ে যাবার মতো পরিস্থিতি আমাদের নেই , তাই রোগীর জীবন হানী সহ সমস্ত ঝুঁকির কথা মাথায় রেখেই আমরা আপদকালীন চিকিৎ্সার জন্য রোগীকে এই প্রতিষ্ঠানে ভর্তি করলাম , এখানে রোগীর প্রয়োজনীয় যাবতীয় চিকিত্সা করার জন্য অনুমতি দিলাম । </td>");
            rpt.Append("</tr >");
            rpt.Append("</table>");
            rpt.Append("<br/>");
            rpt.Append("<table width='100%'>");
            rpt.Append("<tr style='height:30px'>");
            rpt.Append("<td style=' font-family:Verdana;font-size:medium; text-align:justify'>আমরা যেহেতু সুস্থ মনে আমাদের আত্মীয় পরিজনদের সঙ্গে আলোচনা করে রোগীর চিকিত্সা চালানোর অনুমতি দিচ্ছি সেক্ষেত্রে চিকিত্সা চলাকালীন কিংবা এই প্রতিষ্ঠান থেকে ছাড়া পাওয়ার পর রোগীর অন্য কোন জটিল সম্যসা হলে কিংবা জীবন সংশয় হলে আমরা এই প্রতিষ্ঠানকে দায়ী করব না ।</td>");
            rpt.Append("</tr >");
            rpt.Append("</table>");




            rpt.Append("<table width='100%'>");
            rpt.Append("<tr style='height:35px'>");
            rpt.Append("<td style='font-family:Verdana; font-size:medium; text-align:center'> স্বাক্ষর/টিপ </td>");
            rpt.Append("<td style='font-family:Verdana; font-size:medium; text-align:center'> পুরো  নাম  </td>");
            rpt.Append("<td style='font-family:Verdana; font-size:medium; text-align:center'> ঠিকানা </td>");
            rpt.Append("<td style='font-family:Verdana; font-size:medium; text-align:center'> সম্পর্ক  </td>");
            rpt.Append("</tr >");

            rpt.Append("<tr style='height:27px'>");
            rpt.Append("<td style='font-family:Verdana;  font-size:small; text-align:center'>..................</td>");
            rpt.AppendFormat("<td style='font-family:Verdana;  font-size:small; text-align:center'>{0}</td>", ds.Tables[0].Rows[0]["patient_name"]);
            rpt.AppendFormat("<td style='font-family:Verdana; font-size:small; text-align:center'>{0}</td>", ds.Tables[0].Rows[0]["vill_city"]);
            rpt.Append("<td style='font-family:Verdana; font-size:small; text-align:center'> নিজে </td>");
            rpt.Append("</tr >");

            rpt.Append("<tr style='height:27px'>");
            rpt.Append("<td style='font-family:Verdana;  font-size:small; text-align:center'>..................</td>");
            rpt.AppendFormat("<td style='font-family:Verdana;  font-size:small; text-align:center'>{0}</td>", ds.Tables[0].Rows[0]["guardian_name"]);
            rpt.AppendFormat("<td style='font-family:Verdana; font-size:small; text-align:center'>{0}</td>", ds.Tables[0].Rows[0]["vill_city2"]);
            rpt.AppendFormat("<td style='font-family:Verdana; font-size:small; text-align:center'> {0} </td>", ds.Tables[0].Rows[0]["relation"]);
            rpt.Append("</tr >");
            rpt.Append("</table>"); 
        } 
        ltrReport.Visible = true;
    }

    public void GetHearder_BadOrtho1(string reg)
    {
        ltrReport.Text = "";
        DataSet ds = thepatientbill.PatientForReport(reg, Session["CoCode"].ToString().Trim());
        if (ds.Tables[0].Rows.Count > 0)
        {
            rpt.Append("<table width='100%' cellspacing=0 border=0 style='border:1px solid black;border-spacing: 0; padding: 0;font-family:verdana;'>");
            rpt.Append("<tr style='height:25px;'>");
            rpt.Append("<td style='font-family:Verdana; font-size:x-large;text-align:center'><b>Some of the critical situation of the patient about the risks and accept the Letter</b>");
            rpt.Append("</tr >");

            rpt.Append("<tr style='height:20px;'>");
            rpt.AppendFormat("<td style='font-family:Verdana; font-size:medium;text-align:center'>(Admitted By :{0}, Relation :{1})</td>", ds.Tables[0].Rows[0]["guardian_name"], ds.Tables[0].Rows[0]["relation"]);
            rpt.Append("</tr >");
            rpt.Append("</table>");

            PatientDetails_English(ds);
            rpt.Append("<br/>");

            rpt.Append("<table width='100%'>");
            rpt.Append("<tr style='height:25px'>"); rpt.Append("<u>");
            rpt.Append("<td style='font-family:Verdana;font-size:medium;font-weight:bold; text-align:left'>Problems or risk factors of the patient  : </td>"); rpt.Append("</u>");
            rpt.Append("</tr >");

            rpt.Append("<tr style='height:25px'>");
            rpt.Append("<td style='font-family:Verdana;font-size:small; text-align:left'>Sepasisa inaphekasana or may not associate bones, metal or rod may be open, senses induced complications</td>");
            rpt.Append("</tr >");

            rpt.Append("<tr style='height:25px'>"); rpt.Append("<u>");
            rpt.Append("<td style='font-family:Verdana;font-size:medium;font-weight:bold;text-align:left'>Hazard that may occur in the patient  :</td>"); rpt.Append("</u>");
            rpt.Append("</tr >");

            rpt.Append("<tr style='height:25px'>");
            rpt.Append("<td style='font-family:Verdana;font-size:small; text-align:left'> Can not add bone, metal or rod may be open, the organ may be small, organ distortion and movement defect may come, may be dirty with prolonged wound, several operations may be shortness of breath (Embolism), suddenly crippled hrdayanra may be, may be taken away at any moment the patient, the treatment can not be given any guarantees  </td>");
            rpt.Append("</tr >");
            rpt.Append("</table>");
            rpt.Append("<br/>");
            rpt.Append("<table width='100%'>");
            rpt.Append("<tr style='height:25px'>");
            rpt.Append("<td style='font-family:Verdana;font-size:small; text-align:justify'> The organization learned from the patient about these issues and concerns . This organization has repeatedly us from any government medical college or any other health organizations have been asked to take , we suggest that these institutions have agreed along . But now the situation is like taking away patients who do not have , so we are keeping in mind the patient's life is at risk , including hostile cikitsara emergency admissions for patients who have made this institution , the patient gave permission for the necessary treatment .  </td>");
            rpt.Append("</tr >");
            rpt.Append("</table>");
            rpt.Append("<br/>");
            rpt.Append("<table width='100%'>");
            rpt.Append("<tr style='height:25px'>");
            rpt.Append("<td style=' font-family:Verdana;font-size:small; text-align:justify'> We think it's healthy to talk with our kindred allowed to treat the patient during treatment, or in this case, the organization would be without the complicated error occurred when the patient or any other life-threatening if not, we'll blame the institution. </td>");
            rpt.Append("</tr >");
            rpt.Append("</table>");


            rpt.Append("<table width='100%'>");
            rpt.Append("<tr style='height:27px'>");
            rpt.Append("<td style='font-family:Verdana; font-size:medium; text-align:center'> Signature </td>");
            rpt.Append("<td style='font-family:Verdana; font-size:medium; text-align:center'> Full Name  </td>");
            rpt.Append("<td style='font-family:Verdana; font-size:medium; text-align:center'> Address </td>");
            rpt.Append("<td style='font-family:Verdana; font-size:medium; text-align:center'> Relation  </td>");
            rpt.Append("</tr >");

            rpt.Append("<tr style='height:27px'>");
            rpt.Append("<td style='font-family:Verdana;  font-size:small; text-align:center'>..................</td>");
            rpt.AppendFormat("<td style='font-family:Verdana;  font-size:small; text-align:center'>{0}</td>", ds.Tables[0].Rows[0]["patient_name"]);
            rpt.AppendFormat("<td style='font-family:Verdana; font-size:small; text-align:center'>{0}</td>", ds.Tables[0].Rows[0]["vill_city"]);
            rpt.Append("<td style='font-family:Verdana; font-size:small; text-align:center'> Self </td>");
            rpt.Append("</tr >");

            rpt.Append("<tr style='height:27px'>");
            rpt.Append("<td style='font-family:Verdana;  font-size:small; text-align:center'>..................</td>");
            rpt.AppendFormat("<td style='font-family:Verdana;  font-size:small; text-align:center'>{0}</td>", ds.Tables[0].Rows[0]["guardian_name"]);
            rpt.AppendFormat("<td style='font-family:Verdana; font-size:small; text-align:center'>{0}</td>", ds.Tables[0].Rows[0]["vill_city2"]);
            rpt.AppendFormat("<td style='font-family:Verdana; font-size:small; text-align:center'> {0} </td>", ds.Tables[0].Rows[0]["relation"]);
            rpt.Append("</tr >");
            rpt.Append("</table>"); 
        } 
        ltrReport.Visible = true;
    }

    public void GetHearder_BroChopReport(string regno)
    {
        ltrReport.Text = "";
        DataSet ds = thepatientbill.PatientForReport(regno, Session["CoCode"].ToString().Trim());
        if (ds.Tables[0].Rows.Count > 0)
        {
            rpt.Append("<table width='100%' cellspacing=0 border=0 style='border:1px solid black;border-spacing: 0; padding: 0;font-family:verdana;'>");
            rpt.Append("<tr style='height:25px;'>");
            rpt.Append("<td style='font-family:Verdana; font-size:x-large;text-align:center'><b>রোগীর বিশেষ কয়েকটি আশঙ্কাজনক পরিস্থিতি ও তার ঝুঁকি সম্পর্কে স্বীকার পত্র</b>");
            rpt.Append("</tr >");

            rpt.Append("<tr style='height:20px;'>");
            rpt.AppendFormat("<td style='font-family:Verdana; font-size:medium;text-align:center'>(যিনি ভর্তি করাচ্ছেন তার নাম:{0},সম্পর্ক:{1})</td>", ds.Tables[0].Rows[0]["guardian_name"], ds.Tables[0].Rows[0]["relation"]);
            rpt.Append("</tr >");
            rpt.Append("</table>");

            PatientDetails_Bengali(ds);

            rpt.Append("<br/>");      

            rpt.Append("<table width='100%'>");
            rpt.Append("<tr style='height:25px'>"); rpt.Append("<u>");
            rpt.Append("<td style='font-family:Verdana;font-size:medium;font-weight:bold; text-align:left'>রোগীর সমস্যা বা ঝুঁকির কারণ সমূহ: </td>"); rpt.Append("</u>");
            rpt.Append("</tr >");

            rpt.Append("<tr style='height:35px'>");
            rpt.Append("<td style='font-family:Verdana;font-size:medium; text-align:left'>বুকে সর্দি বসে গেছে, জ্বর, মুমূর্ষূ বাচ্চা, প্রচন্ড শ্বাসকষ্ট, শুয়ে থাকতে পারছে না খেতে পারছে না,জীবনী শক্তি কম, ফুসফুস কমজোরী, হার্ট দূর্বল</td>");
            rpt.Append("</tr >");

            rpt.Append("<tr style='height:35px'>"); rpt.Append("<u>");
            rpt.Append("<td style='font-family:Verdana;font-size:medium;font-weight:bold;text-align:left'>ওই সমস্ত রোগীর যে সমস্ত বিপত্তি ঘটতে পারে :</td>"); rpt.Append("</u>");
            rpt.Append("</tr >");

            rpt.Append("<tr style='height:35px'>");
            rpt.Append("<td style='font-family:Verdana;font-size:medium; text-align:left'> ইনফেকসন বা সেপশিস হতে পারে, শ্বাসকষ্ট আরও বাড়তে পারে, হঠাৎ হৃদযন্র বিকল হতে পারে, পেট ফুলে যেতে পারে, যখন তখন বাচ্চার অবস্থা আরও খারাপ হতে পারে, এমন কি মারা ও যেতে পারে, যে কোন মুহূর্তে অন্যত্র নিয়ে যেতে হতে পারে  </td>");
            rpt.Append("</tr >");
            rpt.Append("</table>");
            rpt.Append("<br/>");
            rpt.Append("<table width='100%'>");
            rpt.Append("<tr style='height:30px'>");
            rpt.Append("<td style='font-family:Verdana;font-size:medium; text-align:justify'>এই প্রতিষ্ঠানের পক্ষ থেকে রোগীর এই সমস্ত সমস্যা ও আশঙ্কার কথা জানলাম । এই প্রতিষ্ঠানের পক্ষ থেকে আমাদের বারবার কোন সরকারী মেডিকেল কলেজ কিংবা অন্য কোনও স্বাস্থ্য প্রতিষ্ঠানের নিয়ে যাবার কথা বলা হয়েছে , আমরাও এই প্রতিষ্ঠান যে সমস্ত পরামর্শ দিয়েছে তারসঙ্গে একমত । কিন্তু এই মূহুর্তে রোগীকে অন্যত্র নিয়ে যাবার মতো পরিস্থিতি আমাদের নেই , তাই রোগীর জীবন হানী সহ সমস্ত ঝুঁকির কথা মাথায় রেখেই আমরা আপদকালীন চিকিৎ্সার জন্য রোগীকে এই প্রতিষ্ঠানে ভর্তি করলাম , এখানে রোগীর প্রয়োজনীয় যাবতীয় চিকিত্সা করার জন্য অনুমতি দিলাম । </td>");
            rpt.Append("</tr >");
            rpt.Append("</table>");
            rpt.Append("<br/>");
            rpt.Append("<table width='100%'>");
            rpt.Append("<tr style='height:30px'>");
            rpt.Append("<td style=' font-family:Verdana;font-size:medium; text-align:justify'>আমরা যেহেতু সুস্থ মনে আমাদের আত্মীয় পরিজনদের সঙ্গে আলোচনা করে রোগীর চিকিত্সা চালানোর অনুমতি দিচ্ছি সেক্ষেত্রে চিকিত্সা চলাকালীন কিংবা এই প্রতিষ্ঠান থেকে ছাড়া পাওয়ার পর রোগীর অন্য কোন জটিল সম্যসা হলে কিংবা জীবন সংশয় হলে আমরা এই প্রতিষ্ঠানকে দায়ী করব না ।</td>");
            rpt.Append("</tr >");
            rpt.Append("</table>");

            rpt.Append("<table width='100%'>");
            rpt.Append("<tr style='height:35px'>");
            rpt.Append("<td style='font-family:Verdana; font-size:medium; text-align:center'> স্বাক্ষর/টিপ </td>");
            rpt.Append("<td style='font-family:Verdana; font-size:medium; text-align:center'> পুরো  নাম  </td>");
            rpt.Append("<td style='font-family:Verdana; font-size:medium; text-align:center'> ঠিকানা </td>");
            rpt.Append("<td style='font-family:Verdana; font-size:medium; text-align:center'> সম্পর্ক  </td>");
            rpt.Append("</tr >");

            rpt.Append("<tr style='height:27px'>");
            rpt.Append("<td style='font-family:Verdana;  font-size:small; text-align:center'>..................</td>");
            rpt.AppendFormat("<td style='font-family:Verdana;  font-size:small; text-align:center'>{0}</td>", ds.Tables[0].Rows[0]["patient_name"]);
            rpt.AppendFormat("<td style='font-family:Verdana; font-size:small; text-align:center'>{0}</td>", ds.Tables[0].Rows[0]["vill_city"]);
            rpt.Append("<td style='font-family:Verdana; font-size:small; text-align:center'> নিজে </td>");
            rpt.Append("</tr >");

            rpt.Append("<tr style='height:27px'>");
            rpt.Append("<td style='font-family:Verdana;  font-size:small; text-align:center'>..................</td>");
            rpt.AppendFormat("<td style='font-family:Verdana;  font-size:small; text-align:center'>{0}</td>", ds.Tables[0].Rows[0]["guardian_name"]);
            rpt.AppendFormat("<td style='font-family:Verdana; font-size:small; text-align:center'>{0}</td>", ds.Tables[0].Rows[0]["vill_city2"]);
            rpt.AppendFormat("<td style='font-family:Verdana; font-size:small; text-align:center'> {0} </td>", ds.Tables[0].Rows[0]["relation"]);
            rpt.Append("</tr >");
            rpt.Append("</table>"); 
        }
        ltrReport.Visible = true;
    }

    public void GetHearder_BroChopReport1(string reg)
    {
        ltrReport.Text = "";
        DataSet ds = thepatientbill.PatientForReport(reg, Session["CoCode"].ToString().Trim());
        if (ds.Tables[0].Rows.Count > 0)
        {
            rpt.Append("<table width='100%' cellspacing=0 border=0 style='border:1px solid black;border-spacing: 0; padding: 0;font-family:verdana;'>");
            rpt.Append("<tr style='height:25px;'>");
            rpt.Append("<td style='font-family:Verdana; font-size:large;text-align:center'><b>Some of the critical situation of the patient about the risks and accept the Letter</b>");
            rpt.Append("</tr >");

            rpt.Append("<tr style='height:20px;'>");
            rpt.AppendFormat("<td style='font-family:Verdana; font-size:medium;text-align:center'>(Admitted By :{0}, Relation :{1})</td>", ds.Tables[0].Rows[0]["guardian_name"], ds.Tables[0].Rows[0]["relation"]);
            rpt.Append("</tr >");
            rpt.Append("</table>");

            PatientDetails_English(ds);
            rpt.Append("<br/>");

            rpt.Append("<table width='100%'>");
            rpt.Append("<tr style='height:25px'>"); rpt.Append("<u>");
            rpt.Append("<td style='font-family:Verdana;font-size:medium;font-weight:bold; text-align:left'>Problems or risk factors of the patient  : </td>"); rpt.Append("</u>");
            rpt.Append("</tr >");

            rpt.Append("<tr style='height:25px'>");
            rpt.Append("<td style='font-family:Verdana;font-size:small; text-align:left'>Was sitting in the cold, fever, mumursu child, severe shortness of breath, unable to eat, unable to lie down, the biography of the low, deficient lungs, heart weakness</td>");
            rpt.Append("</tr >");

            rpt.Append("<tr style='height:25px'>"); rpt.Append("<u>");
            rpt.Append("<td style='font-family:Verdana;font-size:medium;font-weight:bold;text-align:left'>Hazard that may occur in the patient  :</td>"); rpt.Append("</u>");
            rpt.Append("</tr >");

            rpt.Append("<tr style='height:25px'>");
            rpt.Append("<td style='font-family:Verdana;font-size:small; text-align:left'>Inaphekasana or may sepasisa, shortness of breath may even escalate, hrdayanra can crash suddenly, the stomach may be swollen, may be worse when the child's condition, and may even die, that may go away at any moment</td>");
            rpt.Append("</tr >");
            rpt.Append("</table>");
            rpt.Append("<br/>");
            rpt.Append("<table width='100%'>");
            rpt.Append("<tr style='height:25px'>");
            rpt.Append("<td style='font-family:Verdana;font-size:small; text-align:justify'> The organization learned from the patient about these issues and concerns . This organization has repeatedly us from any government medical college or any other health organizations have been asked to take , we suggest that these institutions have agreed along . But now the situation is like taking away patients who do not have , so we are keeping in mind the patient's life is at risk , including hostile cikitsara emergency admissions for patients who have made this institution , the patient gave permission for the necessary treatment .  </td>");
            rpt.Append("</tr >");
            rpt.Append("</table>");
            rpt.Append("<br/>");
            rpt.Append("<table width='100%'>");
            rpt.Append("<tr style='height:25px'>");
            rpt.Append("<td style=' font-family:Verdana;font-size:small; text-align:justify'> We think it's healthy to talk with our kindred allowed to treat the patient during treatment, or in this case, the organization would be without the complicated error occurred when the patient or any other life-threatening if not, we'll blame the institution. </td>");
            rpt.Append("</tr >");
            rpt.Append("</table>");


            rpt.Append("<table width='100%'>");
            rpt.Append("<tr style='height:27px'>");
            rpt.Append("<td style='font-family:Verdana; font-size:medium; text-align:center'> Signature </td>");
            rpt.Append("<td style='font-family:Verdana; font-size:medium; text-align:center'> Full Name  </td>");
            rpt.Append("<td style='font-family:Verdana; font-size:medium; text-align:center'> Address </td>");
            rpt.Append("<td style='font-family:Verdana; font-size:medium; text-align:center'> Relation  </td>");
            rpt.Append("</tr >");

            rpt.Append("<tr style='height:27px'>");
            rpt.Append("<td style='font-family:Verdana;  font-size:small; text-align:center'>..................</td>");
            rpt.AppendFormat("<td style='font-family:Verdana;  font-size:small; text-align:center'>{0}</td>", ds.Tables[0].Rows[0]["patient_name"]);
            rpt.AppendFormat("<td style='font-family:Verdana; font-size:small; text-align:center'>{0}</td>", ds.Tables[0].Rows[0]["vill_city"]);
            rpt.Append("<td style='font-family:Verdana; font-size:small; text-align:center'> Self </td>");
            rpt.Append("</tr >");

            rpt.Append("<tr style='height:27px'>");
            rpt.Append("<td style='font-family:Verdana;  font-size:small; text-align:center'>..................</td>");
            rpt.AppendFormat("<td style='font-family:Verdana;  font-size:small; text-align:center'>{0}</td>", ds.Tables[0].Rows[0]["guardian_name"]);
            rpt.AppendFormat("<td style='font-family:Verdana; font-size:small; text-align:center'>{0}</td>", ds.Tables[0].Rows[0]["vill_city2"]);
            rpt.AppendFormat("<td style='font-family:Verdana; font-size:small; text-align:center'> {0} </td>", ds.Tables[0].Rows[0]["relation"]);
            rpt.Append("</tr >");
            rpt.Append("</table>"); 
        } 
        ltrReport.Visible = true;
    }

    public void GetHearder_Eclamsia(string reg)
    {
        ltrReport.Text = "";
        DataSet ds = thepatientbill.PatientForReport(reg, Session["CoCode"].ToString().Trim());
        if (ds.Tables[0].Rows.Count > 0)
        {
            rpt.Append("<table width='100%' cellspacing=0 border=0 style='border:1px solid black;border-spacing: 0; padding: 0;font-family:verdana;'>");
            rpt.Append("<tr style='height:25px;'>");
            rpt.Append("<td style='font-family:Verdana; font-size:x-large;text-align:center'><b>রোগীর বিশেষ কয়েকটি আশঙ্কাজনক পরিস্থিতি ও তার ঝুঁকি সম্পর্কে স্বীকার পত্র</b>");
            rpt.Append("</tr >");

            rpt.Append("<tr style='height:20px;'>");
            rpt.AppendFormat("<td style='font-family:Verdana; font-size:medium;text-align:center'>(যিনি ভর্তি করাচ্ছেন তার নাম:{0},সম্পর্ক:{1})</td>", ds.Tables[0].Rows[0]["guardian_name"], ds.Tables[0].Rows[0]["relation"]);
            rpt.Append("</tr >");
            rpt.Append("</table>");

            PatientDetails_Bengali(ds);
            rpt.Append("<br/>");      

            rpt.Append("<table width='100%'>");
            rpt.Append("<tr style='height:25px'>"); rpt.Append("<u>");
            rpt.Append("<td style='font-family:Verdana;font-size:medium;font-weight:bold; text-align:left'>রোগীর সমস্যা বা ঝুঁকির কারণ সমূহ: </td>"); rpt.Append("</u>");
            rpt.Append("</tr >");

            rpt.Append("<tr style='height:35px'>");
            rpt.Append("<td style='font-family:Verdana;font-size:medium; text-align:left'>এক্লামশিয়া বা গর্ভাকালীন খিঁচুনি, অজ্ঞান অবস্থা, বেশী প্রেসার, নাক মুখ দিয়ে গেঁজা বেরুনো, এই অবস্থায় প্রসব করানোর ঝুঁকি, যদি সীজার করতে হয় অপারেশন করা কালীন রোগীর হৃদ স্পন্দন থেমে যেতে পারে, জ্ঞান না ও ফিরতে পারে, ৩০ থেকে ৪০ % ক্ষেত্রে রোগী মারা যেতে পারে, কোন ভাবেই বাচ্চার কি পরিনতী হবে আগে থেকে বলা যাবে না</td>");
            rpt.Append("</tr >");

            rpt.Append("<tr style='height:35px'>"); rpt.Append("<u>");
            rpt.Append("<td style='font-family:Verdana;font-size:medium;font-weight:bold;text-align:left'>ওই সমস্ত রোগীর যে সমস্ত বিপত্তি ঘটতে পারে :</td>"); rpt.Append("</u>");
            rpt.Append("</tr >");

            rpt.Append("<tr style='height:35px'>");
            rpt.Append("<td style='font-family:Verdana;font-size:medium; text-align:left'>হঠাৎ রোগীর হৃদ স্পন্দন থেমে যেতে পারে, রোগী মারা যেতে পারে, বাচ্চাও মারা যেতে পারে, ফুসফুস, কিডনি, লিভার বিকল হতে পারে, যে কোন মুহূর্তে রোগীকে অন্যত্র নিয়ে যেতে হতে পারে, এই রোগের চিকিৎসার কোন গ্যারান্টী নেই</td>");
            rpt.Append("</tr >");
            rpt.Append("</table>");
            rpt.Append("<br/>");
            rpt.Append("<table width='100%'>");
            rpt.Append("<tr style='height:30px'>");
            rpt.Append("<td style='font-family:Verdana;font-size:medium; text-align:justify'>এই প্রতিষ্ঠানের পক্ষ থেকে রোগীর এই সমস্ত সমস্যা ও আশঙ্কার কথা জানলাম । এই প্রতিষ্ঠানের পক্ষ থেকে আমাদের বারবার কোন সরকারী মেডিকেল কলেজ কিংবা অন্য কোনও স্বাস্থ্য প্রতিষ্ঠানের নিয়ে যাবার কথা বলা হয়েছে , আমরাও এই প্রতিষ্ঠান যে সমস্ত পরামর্শ দিয়েছে তারসঙ্গে একমত । কিন্তু এই মূহুর্তে রোগীকে অন্যত্র নিয়ে যাবার মতো পরিস্থিতি আমাদের নেই , তাই রোগীর জীবন হানী সহ সমস্ত ঝুঁকির কথা মাথায় রেখেই আমরা আপদকালীন চিকিৎ্সার জন্য রোগীকে এই প্রতিষ্ঠানে ভর্তি করলাম , এখানে রোগীর প্রয়োজনীয় যাবতীয় চিকিত্সা করার জন্য অনুমতি দিলাম । </td>");
            rpt.Append("</tr >");
            rpt.Append("</table>");
            rpt.Append("<br/>");
            rpt.Append("<table width='100%'>");
            rpt.Append("<tr style='height:30px'>");
            rpt.Append("<td style=' font-family:Verdana;font-size:medium; text-align:justify'>আমরা যেহেতু সুস্থ মনে আমাদের আত্মীয় পরিজনদের সঙ্গে আলোচনা করে রোগীর চিকিত্সা চালানোর অনুমতি দিচ্ছি সেক্ষেত্রে চিকিত্সা চলাকালীন কিংবা এই প্রতিষ্ঠান থেকে ছাড়া পাওয়ার পর রোগীর অন্য কোন জটিল সম্যসা হলে কিংবা জীবন সংশয় হলে আমরা এই প্রতিষ্ঠানকে দায়ী করব না ।</td>");
            rpt.Append("</tr >");
            rpt.Append("</table>");




            rpt.Append("<table width='100%'>");
            rpt.Append("<tr style='height:35px'>");
            rpt.Append("<td style='font-family:Verdana; font-size:medium; text-align:center'> স্বাক্ষর/টিপ </td>");
            rpt.Append("<td style='font-family:Verdana; font-size:medium; text-align:center'> পুরো  নাম  </td>");
            rpt.Append("<td style='font-family:Verdana; font-size:medium; text-align:center'> ঠিকানা </td>");
            rpt.Append("<td style='font-family:Verdana; font-size:medium; text-align:center'> সম্পর্ক  </td>");
            rpt.Append("</tr >");

            rpt.Append("<tr style='height:27px'>");
            rpt.Append("<td style='font-family:Verdana;  font-size:small; text-align:center'>..................</td>");
            rpt.AppendFormat("<td style='font-family:Verdana;  font-size:small; text-align:center'>{0}</td>", ds.Tables[0].Rows[0]["patient_name"]);
            rpt.AppendFormat("<td style='font-family:Verdana; font-size:small; text-align:center'>{0}</td>", ds.Tables[0].Rows[0]["vill_city"]);
            rpt.Append("<td style='font-family:Verdana; font-size:small; text-align:center'> নিজে </td>");
            rpt.Append("</tr >");

            rpt.Append("<tr style='height:27px'>");
            rpt.Append("<td style='font-family:Verdana;  font-size:small; text-align:center'>..................</td>");
            rpt.AppendFormat("<td style='font-family:Verdana;  font-size:small; text-align:center'>{0}</td>", ds.Tables[0].Rows[0]["guardian_name"]);
            rpt.AppendFormat("<td style='font-family:Verdana; font-size:small; text-align:center'>{0}</td>", ds.Tables[0].Rows[0]["vill_city2"]);
            rpt.AppendFormat("<td style='font-family:Verdana; font-size:small; text-align:center'> {0} </td>", ds.Tables[0].Rows[0]["relation"]);
            rpt.Append("</tr >");
            rpt.Append("</table>"); 
        }
        ltrReport.Visible = true;
    }

    public void GetHearder_Eclamsia1(string reg)
    {
        ltrReport.Text = "";
        DataSet ds = thepatientbill.PatientForReport(reg, Session["CoCode"].ToString().Trim());
        if (ds.Tables[0].Rows.Count > 0)
        {
            rpt.Append("<table width='100%' cellspacing=0 border=0 style='border:1px solid black;border-spacing: 0; padding: 0;font-family:verdana;'>");
            rpt.Append("<tr style='height:25px;'>");
            rpt.Append("<td style='font-family:Verdana; font-size:x-large;text-align:center'><b>Some of the critical situation of the patient about the risks and accept the Letter</b>");
            rpt.Append("</tr >");

            rpt.Append("<tr style='height:20px;'>");
            rpt.AppendFormat("<td style='font-family:Verdana; font-size:medium;text-align:center'>(Admitted By :{0}, Relation :{1})</td>", ds.Tables[0].Rows[0]["guardian_name"], ds.Tables[0].Rows[0]["relation"]);
            rpt.Append("</tr >");
            rpt.Append("</table>");

            PatientDetails_English(ds);
            rpt.Append("<br/>");

            rpt.Append("<table width='100%'>");
            rpt.Append("<tr style='height:25px'>"); rpt.Append("<u>");
            rpt.Append("<td style='font-family:Verdana;font-size:medium;font-weight:bold; text-align:left'>Problems or risk factors of the patient  : </td>"); rpt.Append("</u>");
            rpt.Append("</tr >");

            rpt.Append("<tr style='height:25px'>");
            rpt.Append("<td style='font-family:Verdana;font-size:small; text-align:left'>Garbhakalina eklamasiya or convulsions, unconsciousness, high pressure, gemja out on the nose, the mouth, the risk of this condition into labor, you are not Caesar may stop operations during a patient's heart rhythm, and returns to knowledge, 30 to 40% of the patients died may be, in any way, the child can not be called before the parinati</td>");
            rpt.Append("</tr >");

            rpt.Append("<tr style='height:25px'>"); rpt.Append("<u>");
            rpt.Append("<td style='font-family:Verdana;font-size:medium;font-weight:bold;text-align:left'>Hazard that may occur in the patient  :</td>"); rpt.Append("</u>");
            rpt.Append("</tr >");

            rpt.Append("<tr style='height:25px'>");
            rpt.Append("<td style='font-family:Verdana;font-size:small; text-align:left'>Suddenly the patient's heart rhythm can be stopped, the patient could die, the baby can die, lungs, kidneys, liver damage may be, may be taken away at any moment the patient, the treatment of this disease, there is no guarantee</td>");
            rpt.Append("</tr >");
            rpt.Append("</table>");
            rpt.Append("<br/>");
            rpt.Append("<table width='100%'>");
            rpt.Append("<tr style='height:25px'>");
            rpt.Append("<td style='font-family:Verdana;font-size:small; text-align:justify'> The organization learned from the patient about these issues and concerns . This organization has repeatedly us from any government medical college or any other health organizations have been asked to take , we suggest that these institutions have agreed along . But now the situation is like taking away patients who do not have , so we are keeping in mind the patient's life is at risk , including hostile cikitsara emergency admissions for patients who have made this institution , the patient gave permission for the necessary treatment .  </td>");
            rpt.Append("</tr >");
            rpt.Append("</table>");
            rpt.Append("<br/>");
            rpt.Append("<table width='100%'>");
            rpt.Append("<tr style='height:25px'>");
            rpt.Append("<td style=' font-family:Verdana;font-size:small; text-align:justify'> We think it's healthy to talk with our kindred allowed to treat the patient during treatment, or in this case, the organization would be without the complicated error occurred when the patient or any other life-threatening if not, we'll blame the institution. </td>");
            rpt.Append("</tr >");
            rpt.Append("</table>");


            rpt.Append("<table width='100%'>");
            rpt.Append("<tr style='height:27px'>");
            rpt.Append("<td style='font-family:Verdana; font-size:medium; text-align:center'> Signature </td>");
            rpt.Append("<td style='font-family:Verdana; font-size:medium; text-align:center'> Full Name  </td>");
            rpt.Append("<td style='font-family:Verdana; font-size:medium; text-align:center'> Address </td>");
            rpt.Append("<td style='font-family:Verdana; font-size:medium; text-align:center'> Relation  </td>");
            rpt.Append("</tr >");

            rpt.Append("<tr style='height:27px'>");
            rpt.Append("<td style='font-family:Verdana;  font-size:small; text-align:center'>..................</td>");
            rpt.AppendFormat("<td style='font-family:Verdana;  font-size:small; text-align:center'>{0}</td>", ds.Tables[0].Rows[0]["patient_name"]);
            rpt.AppendFormat("<td style='font-family:Verdana; font-size:small; text-align:center'>{0}</td>", ds.Tables[0].Rows[0]["vill_city"]);
            rpt.Append("<td style='font-family:Verdana; font-size:small; text-align:center'> Self </td>");
            rpt.Append("</tr >");

            rpt.Append("<tr style='height:27px'>");
            rpt.Append("<td style='font-family:Verdana;  font-size:small; text-align:center'>..................</td>");
            rpt.AppendFormat("<td style='font-family:Verdana;  font-size:small; text-align:center'>{0}</td>", ds.Tables[0].Rows[0]["guardian_name"]);
            rpt.AppendFormat("<td style='font-family:Verdana; font-size:small; text-align:center'>{0}</td>", ds.Tables[0].Rows[0]["vill_city2"]);
            rpt.AppendFormat("<td style='font-family:Verdana; font-size:small; text-align:center'> {0} </td>", ds.Tables[0].Rows[0]["relation"]);
            rpt.Append("</tr >");
            rpt.Append("</table>"); 
        } 
        ltrReport.Visible = true;
    }

    public void GetHearder_EctopicPregnency(string reg )
    {
        ltrReport.Text = "";
        DataSet ds = thepatientbill.PatientForReport(reg, Session["CoCode"].ToString().Trim());
        if (ds.Tables[0].Rows.Count > 0)
        {
            rpt.Append("<table width='100%' cellspacing=0 border=0 style='border:1px solid black;border-spacing: 0; padding: 0;font-family:verdana;'>");
            rpt.Append("<tr style='height:25px;'>");
            rpt.Append("<td style='font-family:Verdana; font-size:x-large;text-align:center'><b>রোগীর বিশেষ কয়েকটি আশঙ্কাজনক পরিস্থিতি ও তার ঝুঁকি সম্পর্কে স্বীকার পত্র</b>");
            rpt.Append("</tr >");

            rpt.Append("<tr style='height:20px;'>");
            rpt.AppendFormat("<td style='font-family:Verdana; font-size:medium;text-align:center'>(যিনি ভর্তি করাচ্ছেন তার নাম:{0},সম্পর্ক:{1})</td>", ds.Tables[0].Rows[0]["guardian_name"], ds.Tables[0].Rows[0]["relation"]);
            rpt.Append("</tr >");
            rpt.Append("</table>");
            PatientDetails_Bengali(ds);
            rpt.Append("<br/>");      

            rpt.Append("<table width='100%'>");
            rpt.Append("<tr style='height:35px'>"); rpt.Append("<u>");
            rpt.Append("<td style='font-family:Verdana;font-size:medium;font-weight:bold; text-align:left'>রোগীর সমস্যা বা ঝুঁকির কারণ সমূহ: </td>"); rpt.Append("</u>");
            rpt.Append("</tr >");

            rpt.Append("<tr style='height:35px'>");
            rpt.Append("<td style='font-family:Verdana;font-size:medium; text-align:left'>ডিম্বনালি ফেটে গিয়ে পেটে রক্ত ক্ষরন হচ্ছে,দেহের সমস্ত রক্ত পেটে জমা হচ্ছে, রোগী প্রায় রক্ত শূণ্য, এখুনি প্রচুর রক্ত লাগবে, এই রক্ত জোগাড় করার দায়িত্ব রোগীর বাড়ীর লোককেই নিতে হবে, রক্ত জোগাড় করতে যত দেরি হবে রোগীর বিপদ তত বাড়বে, আর রক্ত না পেলে অপারেশন করা প্রায় অসম্ভব, কিন্তু অপারেশন না করলে পেটে রক্ত ক্ষরন হতেই থাকবে </td>");
            rpt.Append("</tr >");

            rpt.Append("<tr style='height:35px'>"); rpt.Append("<u>");
            rpt.Append("<td style='font-family:Verdana;font-size:medium;font-weight:bold;text-align:left'>ওই সমস্ত রোগীর যে সমস্ত বিপত্তি ঘটতে পারে :</td>"); rpt.Append("</u>");
            rpt.Append("</tr >");

            rpt.Append("<tr style='height:35px'>");
            rpt.Append("<td style='font-family:Verdana;font-size:medium; text-align:left'>সমস্ত রক্ত পেটে জমা হয়ে রোগী রক্তশূণ্য হতে পারে, হৃদযন্ত্র বিকল হতে পারে, কিডনি বিকল হতে পারে, অপারেশন করা কালীন হৃদযন্ত্র বিকল হতে পারে, জ্ঞান ফিরতে দেরী বা আদৌ জ্ঞান ফিরতে না পারে, আগে থেকে জানা যাচ্ছে না এমন কোন জটিলতা দেখা দিতে পারে </td>");
            rpt.Append("</tr >");
            rpt.Append("</table>");
            rpt.Append("<br/>");
            rpt.Append("<table width='100%'>");
            rpt.Append("<tr style='height:35px'>");
            rpt.Append("<td style='font-family:Verdana;font-size:medium; text-align:justify'>এই প্রতিষ্ঠানের পক্ষ থেকে রোগীর এই সমস্ত সমস্যা ও আশঙ্কার কথা জানলাম । এই প্রতিষ্ঠানের পক্ষ থেকে আমাদের বারবার কোন সরকারী মেডিকেল কলেজ কিংবা অন্য কোনও স্বাস্থ্য প্রতিষ্ঠানের নিয়ে যাবার কথা বলা হয়েছে , আমরাও এই প্রতিষ্ঠান যে সমস্ত পরামর্শ দিয়েছে তারসঙ্গে একমত । কিন্তু এই মূহুর্তে রোগীকে অন্যত্র নিয়ে যাবার মতো পরিস্থিতি আমাদের নেই , তাই রোগীর জীবন হানী সহ সমস্ত ঝুঁকির কথা মাথায় রেখেই আমরা আপদকালীন চিকিৎ্সার জন্য রোগীকে এই প্রতিষ্ঠানে ভর্তি করলাম , এখানে রোগীর প্রয়োজনীয় যাবতীয় চিকিত্সা করার জন্য অনুমতি দিলাম । </td>");
            rpt.Append("</tr >");
            rpt.Append("</table>");
            rpt.Append("<br/>");
            rpt.Append("<table width='100%'>");
            rpt.Append("<tr style='height:35px'>");
            rpt.Append("<td style=' font-family:Verdana;font-size:medium; text-align:justify'>আমরা যেহেতু সুস্থ মনে আমাদের আত্মীয় পরিজনদের সঙ্গে আলোচনা করে রোগীর চিকিত্সা চালানোর অনুমতি দিচ্ছি সেক্ষেত্রে চিকিত্সা চলাকালীন কিংবা এই প্রতিষ্ঠান থেকে ছাড়া পাওয়ার পর রোগীর অন্য কোন জটিল সম্যসা হলে কিংবা জীবন সংশয় হলে আমরা এই প্রতিষ্ঠানকে দায়ী করব না ।</td>");
            rpt.Append("</tr >");
            rpt.Append("</table>");




            rpt.Append("<table width='100%'>");
            rpt.Append("<tr style='height:35px'>");
            rpt.Append("<td style='font-family:Verdana; font-size:medium; text-align:center'> স্বাক্ষর/টিপ </td>");
            rpt.Append("<td style='font-family:Verdana; font-size:medium; text-align:center'> পুরো  নাম  </td>");
            rpt.Append("<td style='font-family:Verdana; font-size:medium; text-align:center'> ঠিকানা </td>");
            rpt.Append("<td style='font-family:Verdana; font-size:medium; text-align:center'> সম্পর্ক  </td>");
            rpt.Append("</tr >");

            rpt.Append("<tr style='height:27px'>");
            rpt.Append("<td style='font-family:Verdana;  font-size:small; text-align:center'>..................</td>");
            rpt.AppendFormat("<td style='font-family:Verdana;  font-size:small; text-align:center'>{0}</td>", ds.Tables[0].Rows[0]["patient_name"]);
            rpt.AppendFormat("<td style='font-family:Verdana; font-size:small; text-align:center'>{0}</td>", ds.Tables[0].Rows[0]["vill_city"]);
            rpt.Append("<td style='font-family:Verdana; font-size:small; text-align:center'> নিজে </td>");
            rpt.Append("</tr >");

            rpt.Append("<tr style='height:27px'>");
            rpt.Append("<td style='font-family:Verdana;  font-size:small; text-align:center'>..................</td>");
            rpt.AppendFormat("<td style='font-family:Verdana;  font-size:small; text-align:center'>{0}</td>", ds.Tables[0].Rows[0]["guardian_name"]);
            rpt.AppendFormat("<td style='font-family:Verdana; font-size:small; text-align:center'>{0}</td>", ds.Tables[0].Rows[0]["vill_city2"]);
            rpt.AppendFormat("<td style='font-family:Verdana; font-size:small; text-align:center'> {0} </td>", ds.Tables[0].Rows[0]["relation"]);
            rpt.Append("</tr >");
            rpt.Append("</table>"); 
        } 
        ltrReport.Visible = true;
    }


    public void GetHearder_EctopicPregnency1(string reg)
    {
        ltrReport.Text = "";
        DataSet ds = thepatientbill.PatientForReport(reg, Session["CoCode"].ToString().Trim());
        if (ds.Tables[0].Rows.Count > 0)
        {
            rpt.Append("<table width='100%' cellspacing=0 border=0 style='border:1px solid black;border-spacing: 0; padding: 0;font-family:verdana;'>");
            rpt.Append("<tr style='height:25px;'>");
            rpt.Append("<td style='font-family:Verdana; font-size:x-large;text-align:center'><b>Some of the critical situation of the patient about the risks and accept the Letter</b>");
            rpt.Append("</tr >");

            rpt.Append("<tr style='height:20px;'>");
            rpt.AppendFormat("<td style='font-family:Verdana; font-size:medium;text-align:center'>(Admitted By :{0}, Relation :{1})</td>", ds.Tables[0].Rows[0]["guardian_name"], ds.Tables[0].Rows[0]["relation"]);
            rpt.Append("</tr >");
            rpt.Append("</table>");

            PatientDetails_English(ds);
            rpt.Append("<br/>");


            rpt.Append("<table width='100%'>");
            rpt.Append("<tr style='height:25px'>"); rpt.Append("<u>");
            rpt.Append("<td style='font-family:Verdana;font-size:medium;font-weight:bold; text-align:left'>Problems or risk factors of the patient  : </td>"); rpt.Append("</u>");
            rpt.Append("</tr >");

            rpt.Append("<tr style='height:25px'>");
            rpt.Append("<td style='font-family:Verdana;font-size:small; text-align:left'>Oviduct rupture of the stomach, blood dripping, blood in the body is deposited in the stomach, the patient is almost empty of blood, lots of blood, take away, take the children to the house of the patient's responsibility to obtain the blood, the blood will be too late to get the greater danger to the patient increase, the blood does not when the operation is almost impossible, but the operation will continue to ooze blood in the stomach</td>");
            rpt.Append("</tr >");

            rpt.Append("<tr style='height:25px'>"); rpt.Append("<u>");
            rpt.Append("<td style='font-family:Verdana;font-size:medium;font-weight:bold;text-align:left'>Hazard that may occur in the patient  :</td>"); rpt.Append("</u>");
            rpt.Append("</tr >");

            rpt.Append("<tr style='height:25px'>");
            rpt.Append("<td style='font-family:Verdana;font-size:small; text-align:left'>All of the patients submitted to abdominal blood can be raktasunya, heart failure may be crippled kidneys, heart failure while the operation may be, the knowledge could be back or not come back at all the knowledge, before any complications that may not be evident from</td>");
            rpt.Append("</tr >");
            rpt.Append("</table>");
            rpt.Append("<br/>");
            rpt.Append("<table width='100%'>");
            rpt.Append("<tr style='height:25px'>");
            rpt.Append("<td style='font-family:Verdana;font-size:small; text-align:justify'> The organization learned from the patient about these issues and concerns . This organization has repeatedly us from any government medical college or any other health organizations have been asked to take , we suggest that these institutions have agreed along . But now the situation is like taking away patients who do not have , so we are keeping in mind the patient's life is at risk , including hostile cikitsara emergency admissions for patients who have made this institution , the patient gave permission for the necessary treatment .  </td>");
            rpt.Append("</tr >");
            rpt.Append("</table>");
            rpt.Append("<br/>");
            rpt.Append("<table width='100%'>");
            rpt.Append("<tr style='height:25px'>");
            rpt.Append("<td style=' font-family:Verdana;font-size:small; text-align:justify'> We think it's healthy to talk with our kindred allowed to treat the patient during treatment, or in this case, the organization would be without the complicated error occurred when the patient or any other life-threatening if not, we'll blame the institution. </td>");
            rpt.Append("</tr >");
            rpt.Append("</table>");


            rpt.Append("<table width='100%'>");
            rpt.Append("<tr style='height:27px'>");
            rpt.Append("<td style='font-family:Verdana; font-size:medium; text-align:center'> Signature </td>");
            rpt.Append("<td style='font-family:Verdana; font-size:medium; text-align:center'> Full Name  </td>");
            rpt.Append("<td style='font-family:Verdana; font-size:medium; text-align:center'> Address </td>");
            rpt.Append("<td style='font-family:Verdana; font-size:medium; text-align:center'> Relation  </td>");
            rpt.Append("</tr >");

            rpt.Append("<tr style='height:27px'>");
            rpt.Append("<td style='font-family:Verdana;  font-size:small; text-align:center'>..................</td>");
            rpt.AppendFormat("<td style='font-family:Verdana;  font-size:small; text-align:center'>{0}</td>", ds.Tables[0].Rows[0]["patient_name"]);
            rpt.AppendFormat("<td style='font-family:Verdana; font-size:small; text-align:center'>{0}</td>", ds.Tables[0].Rows[0]["vill_city"]);
            rpt.Append("<td style='font-family:Verdana; font-size:small; text-align:center'> Self </td>");
            rpt.Append("</tr >");

            rpt.Append("<tr style='height:27px'>");
            rpt.Append("<td style='font-family:Verdana;  font-size:small; text-align:center'>..................</td>");
            rpt.AppendFormat("<td style='font-family:Verdana;  font-size:small; text-align:center'>{0}</td>", ds.Tables[0].Rows[0]["guardian_name"]);
            rpt.AppendFormat("<td style='font-family:Verdana; font-size:small; text-align:center'>{0}</td>", ds.Tables[0].Rows[0]["vill_city"]);
            rpt.AppendFormat("<td style='font-family:Verdana; font-size:small; text-align:center'> {0} </td>", ds.Tables[0].Rows[0]["relation"]);
            rpt.Append("</tr >");
            rpt.Append("</table>"); 
        } 
        ltrReport.Visible = true;
    }

    public void GetHearder_HighRisk(string reg)
    {
        ltrReport.Text = "";
        DataSet ds = thepatientbill.PatientForReport(reg, Session["CoCode"].ToString().Trim());
        if (ds.Tables[0].Rows.Count > 0)
        {
            rpt.Append("<table width='100%' cellspacing=0 border=0 style='border:1px solid black;border-spacing: 0; padding: 0;font-family:verdana;'>");
            rpt.Append("<tr style='height:25px;'>");
            rpt.Append("<td style='font-family:Verdana; font-size:x-large;text-align:center'><b>রোগীর বিশেষ কয়েকটি আশঙ্কাজনক পরিস্থিতি ও তার ঝুঁকি সম্পর্কে স্বীকার পত্র</b>");
            rpt.Append("</tr >");

            rpt.Append("<tr style='height:20px;'>");
            rpt.AppendFormat("<td style='font-family:Verdana; font-size:medium;text-align:center'>(যিনি ভর্তি করাচ্ছেন তার নাম:{0},সম্পর্ক:{1})</td>", ds.Tables[0].Rows[0]["guardian_name"], ds.Tables[0].Rows[0]["relation"]);
            rpt.Append("</tr >");
            rpt.Append("</table>");
            PatientDetails_Bengali(ds);
            rpt.Append("<br/>");      

            rpt.Append("<table width='100%'>");
            rpt.Append("<tr style='height:25px'>"); rpt.Append("<u>");
            rpt.Append("<td style='font-family:Verdana;font-size:medium;font-weight:bold; text-align:left'>রোগীর সমস্যা বা ঝুঁকির কারণ সমূহ: </td>"); rpt.Append("</u>");
            rpt.Append("</tr >");

            rpt.Append("<tr style='height:35px'>");
            rpt.Append("<td style='font-family:Verdana;font-size:medium; text-align:left'>হার্ট অ্যাটাক, অনিয়মিত হৃদস্পন্দন, অনিয়ন্ত্রিত ব্লাড প্রেসার, সুগারের সমস্যা, বেশী বয়স</td>");
            rpt.Append("</tr >");

            rpt.Append("<tr style='height:35px'>"); rpt.Append("<u>");
            rpt.Append("<td style='font-family:Verdana;font-size:medium;font-weight:bold;text-align:left'>ওই সমস্ত রোগীর যে সমস্ত বিপত্তি ঘটতে পারে :</td>"); rpt.Append("</u>");
            rpt.Append("</tr >");

            rpt.Append("<tr style='height:35px'>");
            rpt.Append("<td style='font-family:Verdana;font-size:medium; text-align:left'>হঠাৎ রোগীর হৃদ স্পন্দন থেমে যেতে পারে, রোগী মারা যেতে পারে, ফুসফুস কিডনি লিভার বিকল হতে পারে, যে কোন মুহূর্তে রোগীকে অন্যত্র নিয়ে যেতে হতে পারে, এই রোগের চিকিৎসার কোন গ্যারান্টী নেই</td>");
            rpt.Append("</tr >");
            rpt.Append("</table>");
            rpt.Append("<br/>");
            rpt.Append("<table width='100%'>");
            rpt.Append("<tr style='height:30px'>");
            rpt.Append("<td style='font-family:Verdana;font-size:medium; text-align:justify'>এই প্রতিষ্ঠানের পক্ষ থেকে রোগীর এই সমস্ত সমস্যা ও আশঙ্কার কথা জানলাম । এই প্রতিষ্ঠানের পক্ষ থেকে আমাদের বারবার কোন সরকারী মেডিকেল কলেজ কিংবা অন্য কোনও স্বাস্থ্য প্রতিষ্ঠানের নিয়ে যাবার কথা বলা হয়েছে , আমরাও এই প্রতিষ্ঠান যে সমস্ত পরামর্শ দিয়েছে তারসঙ্গে একমত । কিন্তু এই মূহুর্তে রোগীকে অন্যত্র নিয়ে যাবার মতো পরিস্থিতি আমাদের নেই , তাই রোগীর জীবন হানী সহ সমস্ত ঝুঁকির কথা মাথায় রেখেই আমরা আপদকালীন চিকিৎ্সার জন্য রোগীকে এই প্রতিষ্ঠানে ভর্তি করলাম , এখানে রোগীর প্রয়োজনীয় যাবতীয় চিকিত্সা করার জন্য অনুমতি দিলাম । </td>");
            rpt.Append("</tr >");
            rpt.Append("</table>");
            rpt.Append("<br/>");
            rpt.Append("<table width='100%'>");
            rpt.Append("<tr style='height:30px'>");
            rpt.Append("<td style=' font-family:Verdana;font-size:medium; text-align:justify'>আমরা যেহেতু সুস্থ মনে আমাদের আত্মীয় পরিজনদের সঙ্গে আলোচনা করে রোগীর চিকিত্সা চালানোর অনুমতি দিচ্ছি সেক্ষেত্রে চিকিত্সা চলাকালীন কিংবা এই প্রতিষ্ঠান থেকে ছাড়া পাওয়ার পর রোগীর অন্য কোন জটিল সম্যসা হলে কিংবা জীবন সংশয় হলে আমরা এই প্রতিষ্ঠানকে দায়ী করব না ।</td>");
            rpt.Append("</tr >");
            rpt.Append("</table>");




            rpt.Append("<table width='100%'>");
            rpt.Append("<tr style='height:35px'>");
            rpt.Append("<td style='font-family:Verdana; font-size:medium; text-align:center'> স্বাক্ষর/টিপ </td>");
            rpt.Append("<td style='font-family:Verdana; font-size:medium; text-align:center'> পুরো  নাম  </td>");
            rpt.Append("<td style='font-family:Verdana; font-size:medium; text-align:center'> ঠিকানা </td>");
            rpt.Append("<td style='font-family:Verdana; font-size:medium; text-align:center'> সম্পর্ক  </td>");
            rpt.Append("</tr >");

            rpt.Append("<tr style='height:27px'>");
            rpt.Append("<td style='font-family:Verdana;  font-size:small; text-align:center'>..................</td>");
            rpt.AppendFormat("<td style='font-family:Verdana;  font-size:small; text-align:center'>{0}</td>", ds.Tables[0].Rows[0]["patient_name"]);
            rpt.AppendFormat("<td style='font-family:Verdana; font-size:small; text-align:center'>{0}</td>", ds.Tables[0].Rows[0]["vill_city"]);
            rpt.Append("<td style='font-family:Verdana; font-size:small; text-align:center'> নিজে </td>");
            rpt.Append("</tr >");

            rpt.Append("<tr style='height:27px'>");
            rpt.Append("<td style='font-family:Verdana;  font-size:small; text-align:center'>..................</td>");
            rpt.AppendFormat("<td style='font-family:Verdana;  font-size:small; text-align:center'>{0}</td>", ds.Tables[0].Rows[0]["guardian_name"]);
            rpt.AppendFormat("<td style='font-family:Verdana; font-size:small; text-align:center'>{0}</td>", ds.Tables[0].Rows[0]["vill_city2"]);
            rpt.AppendFormat("<td style='font-family:Verdana; font-size:small; text-align:center'> {0} </td>", ds.Tables[0].Rows[0]["relation"]);
            rpt.Append("</tr >");
            rpt.Append("</table>"); 
        }
        ltrReport.Visible = true;
    }

    public void GetHearder_HighRisk1(string reg)
    {
        ltrReport.Text = "";
        DataSet ds = thepatientbill.PatientForReport(reg, Session["CoCode"].ToString().Trim());
        if (ds.Tables[0].Rows.Count > 0)
        {
            rpt.Append("<table width='100%' cellspacing=0 border=0 style='border:1px solid black;border-spacing: 0; padding: 0;font-family:verdana;'>");
            rpt.Append("<tr style='height:25px;'>");
            rpt.Append("<td style='font-family:Verdana; font-size:x-large;text-align:center'><b>Some of the critical situation of the patient about the risks and accept the Letter</b>");
            rpt.Append("</tr >");

            rpt.Append("<tr style='height:20px;'>");
            rpt.AppendFormat("<td style='font-family:Verdana; font-size:medium;text-align:center'>(Admitted By :{0}, Relation :{1})</td>", ds.Tables[0].Rows[0]["guardian_name"], ds.Tables[0].Rows[0]["relation"]);
            rpt.Append("</tr >");
            rpt.Append("</table>");

            PatientDetails_English(ds);
            rpt.Append("<br/>");

            rpt.Append("<table width='100%'>");
            rpt.Append("<tr style='height:25px'>"); rpt.Append("<u>");
            rpt.Append("<td style='font-family:Verdana;font-size:medium;font-weight:bold; text-align:left'>Problems or risk factors of the patient  : </td>"); rpt.Append("</u>");
            rpt.Append("</tr >");

            rpt.Append("<tr style='height:25px'>");
            rpt.Append("<td style='font-family:Verdana;font-size:small; text-align:left'>Heart attack, irregular heart rate, uncontrolled blood pressure, sugar problems, higher age</td>");
            rpt.Append("</tr >");

            rpt.Append("<tr style='height:25px'>"); rpt.Append("<u>");
            rpt.Append("<td style='font-family:Verdana;font-size:medium;font-weight:bold;text-align:left'>Hazard that may occur in the patient  :</td>"); rpt.Append("</u>");
            rpt.Append("</tr >");

            rpt.Append("<tr style='height:25px'>");
            rpt.Append("<td style='font-family:Verdana;font-size:small; text-align:left'> Suddenly the patient's heart rhythm can be stopped, the patient may die, lungs, kidneys, liver damage may be, may be taken away at any moment the patient, the treatment of this disease, there is no guarantee </td>");
            rpt.Append("</tr >");
            rpt.Append("</table>");
            rpt.Append("<br/>");
            rpt.Append("<table width='100%'>");
            rpt.Append("<tr style='height:25px'>");
            rpt.Append("<td style='font-family:Verdana;font-size:small; text-align:justify'> The organization learned from the patient about these issues and concerns . This organization has repeatedly us from any government medical college or any other health organizations have been asked to take , we suggest that these institutions have agreed along . But now the situation is like taking away patients who do not have , so we are keeping in mind the patient's life is at risk , including hostile cikitsara emergency admissions for patients who have made this institution , the patient gave permission for the necessary treatment .  </td>");
            rpt.Append("</tr >");
            rpt.Append("</table>");
            rpt.Append("<br/>");
            rpt.Append("<table width='100%'>");
            rpt.Append("<tr style='height:25px'>");
            rpt.Append("<td style=' font-family:Verdana;font-size:small; text-align:justify'> We think it's healthy to talk with our kindred allowed to treat the patient during treatment, or in this case, the organization would be without the complicated error occurred when the patient or any other life-threatening if not, we'll blame the institution. </td>");
            rpt.Append("</tr >");
            rpt.Append("</table>");


            rpt.Append("<table width='100%'>");
            rpt.Append("<tr style='height:27px'>");
            rpt.Append("<td style='font-family:Verdana; font-size:medium; text-align:center'> Signature </td>");
            rpt.Append("<td style='font-family:Verdana; font-size:medium; text-align:center'> Full Name  </td>");
            rpt.Append("<td style='font-family:Verdana; font-size:medium; text-align:center'> Address </td>");
            rpt.Append("<td style='font-family:Verdana; font-size:medium; text-align:center'> Relation  </td>");
            rpt.Append("</tr >");

            rpt.Append("<tr style='height:27px'>");
            rpt.Append("<td style='font-family:Verdana;  font-size:small; text-align:center'>..................</td>");
            rpt.AppendFormat("<td style='font-family:Verdana;  font-size:small; text-align:center'>{0}</td>", ds.Tables[0].Rows[0]["patient_name"]);
            rpt.AppendFormat("<td style='font-family:Verdana; font-size:small; text-align:center'>{0}</td>", ds.Tables[0].Rows[0]["vill_city"]);
            rpt.Append("<td style='font-family:Verdana; font-size:small; text-align:center'> Self </td>");
            rpt.Append("</tr >");

            rpt.Append("<tr style='height:27px'>");
            rpt.Append("<td style='font-family:Verdana;  font-size:small; text-align:center'>..................</td>");
            rpt.AppendFormat("<td style='font-family:Verdana;  font-size:small; text-align:center'>{0}</td>", ds.Tables[0].Rows[0]["guardian_name"]);
            rpt.AppendFormat("<td style='font-family:Verdana; font-size:small; text-align:center'>{0}</td>", ds.Tables[0].Rows[0]["vill_city2"]);
            rpt.AppendFormat("<td style='font-family:Verdana; font-size:small; text-align:center'> {0} </td>", ds.Tables[0].Rows[0]["relation"]);
            rpt.Append("</tr >");
            rpt.Append("</table>"); 
        } 
        ltrReport.Visible = true;
    }

    public void GetHearder_NeonatalConvulsion(string reg)
    {
        ltrReport.Text = "";

        DataSet ds = thepatientbill.PatientForReport(reg, Session["CoCode"].ToString().Trim());
        if (ds.Tables[0].Rows.Count > 0)
        {
            rpt.Append("<table width='100%' cellspacing=0 border=0 style='border:1px solid black;border-spacing: 0; padding: 0;font-family:verdana;'>");
            rpt.Append("<tr style='height:25px;'>");
            rpt.Append("<td style='font-family:Verdana; font-size:x-large;text-align:center'><b>রোগীর বিশেষ কয়েকটি আশঙ্কাজনক পরিস্থিতি ও তার ঝুঁকি সম্পর্কে স্বীকার পত্র</b>");
            rpt.Append("</tr >");

            rpt.Append("<tr style='height:20px;'>");
            rpt.AppendFormat("<td style='font-family:Verdana; font-size:medium;text-align:center'>(যিনি ভর্তি করাচ্ছেন তার নাম:{0},সম্পর্ক:{1})</td>", ds.Tables[0].Rows[0]["guardian_name"], ds.Tables[0].Rows[0]["relation"]);
            rpt.Append("</tr >");
            rpt.Append("</table>");

            PatientDetails_Bengali(ds);
            rpt.Append("<br/>");      

            rpt.Append("<table width='100%'>");
            rpt.Append("<tr style='height:25px'>"); rpt.Append("<u>");
            rpt.Append("<td style='font-family:Verdana;font-size:medium;font-weight:bold; text-align:left'>রোগীর সমস্যা বা ঝুঁকির কারণ সমূহ: </td>"); rpt.Append("</u>");
            rpt.Append("</tr >");

            rpt.Append("<tr style='height:35px'>");
            rpt.Append("<td style='font-family:Verdana;font-size:medium; text-align:left'>অনবরত খিঁচুনি,জীবনী শক্তি কম, ফুসফুস কমজোরী, হার্ট দূর্বল, অন্যান্য অঙ্গ অপরিনত, খাওয়ানোর সমস্যা, যখন তখন বাচ্চার অবস্থা খারাপ হওয়ার প্রনণতা</td>");
            rpt.Append("</tr >");

            rpt.Append("<tr style='height:35px'>"); rpt.Append("<u>");
            rpt.Append("<td style='font-family:Verdana;font-size:medium;font-weight:bold;text-align:left'>ওই সমস্ত রোগীর যে সমস্ত বিপত্তি ঘটতে পারে :</td>"); rpt.Append("</u>");
            rpt.Append("</tr >");

            rpt.Append("<tr style='height:35px'>");
            rpt.Append("<td style='font-family:Verdana;font-size:medium; text-align:left'>ইনফেকসন বা সেপশিস হতে পারে, শ্বাসকষ্ট হতে পারে, ব্রেনে রক্তপাত হতে পারে, হঠাৎ হৃদযন্র বিকল হতে পারে, পেট ফুলে যেতে পারে, যখন তখন বাচ্চার অবস্থা খারাপ হতে পারে, এমন কি মারা ও যেতে পারে, যে কোন মুহূর্তে অন্যত্র নিয়ে যেতে হতে পারে,এই রোগের চিকিৎসার কোন গ্যারান্টী নেই</td>");
            rpt.Append("</tr >");
            rpt.Append("</table>");
            rpt.Append("<br/>");
            rpt.Append("<table width='100%'>");
            rpt.Append("<tr style='height:30px'>");
            rpt.Append("<td style='font-family:Verdana;font-size:medium; text-align:justify'>এই প্রতিষ্ঠানের পক্ষ থেকে রোগীর এই সমস্ত সমস্যা ও আশঙ্কার কথা জানলাম । এই প্রতিষ্ঠানের পক্ষ থেকে আমাদের বারবার কোন সরকারী মেডিকেল কলেজ কিংবা অন্য কোনও স্বাস্থ্য প্রতিষ্ঠানের নিয়ে যাবার কথা বলা হয়েছে , আমরাও এই প্রতিষ্ঠান যে সমস্ত পরামর্শ দিয়েছে তারসঙ্গে একমত । কিন্তু এই মূহুর্তে রোগীকে অন্যত্র নিয়ে যাবার মতো পরিস্থিতি আমাদের নেই , তাই রোগীর জীবন হানী সহ সমস্ত ঝুঁকির কথা মাথায় রেখেই আমরা আপদকালীন চিকিৎ্সার জন্য রোগীকে এই প্রতিষ্ঠানে ভর্তি করলাম , এখানে রোগীর প্রয়োজনীয় যাবতীয় চিকিত্সা করার জন্য অনুমতি দিলাম । </td>");
            rpt.Append("</tr >");
            rpt.Append("</table>");
            rpt.Append("<br/>");
            rpt.Append("<table width='100%'>");
            rpt.Append("<tr style='height:30px'>");
            rpt.Append("<td style=' font-family:Verdana;font-size:medium; text-align:justify'>আমরা যেহেতু সুস্থ মনে আমাদের আত্মীয় পরিজনদের সঙ্গে আলোচনা করে রোগীর চিকিত্সা চালানোর অনুমতি দিচ্ছি সেক্ষেত্রে চিকিত্সা চলাকালীন কিংবা এই প্রতিষ্ঠান থেকে ছাড়া পাওয়ার পর রোগীর অন্য কোন জটিল সম্যসা হলে কিংবা জীবন সংশয় হলে আমরা এই প্রতিষ্ঠানকে দায়ী করব না ।</td>");
            rpt.Append("</tr >");
            rpt.Append("</table>");




            rpt.Append("<table width='100%'>");
            rpt.Append("<tr style='height:35px'>");
            rpt.Append("<td style='font-family:Verdana; font-size:medium; text-align:center'> স্বাক্ষর/টিপ </td>");
            rpt.Append("<td style='font-family:Verdana; font-size:medium; text-align:center'> পুরো  নাম  </td>");
            rpt.Append("<td style='font-family:Verdana; font-size:medium; text-align:center'> ঠিকানা </td>");
            rpt.Append("<td style='font-family:Verdana; font-size:medium; text-align:center'> সম্পর্ক  </td>");
            rpt.Append("</tr >");

            rpt.Append("<tr style='height:27px'>");
            rpt.Append("<td style='font-family:Verdana;  font-size:small; text-align:center'>..................</td>");
            rpt.AppendFormat("<td style='font-family:Verdana;  font-size:small; text-align:center'>{0}</td>", ds.Tables[0].Rows[0]["patient_name"]);
            rpt.AppendFormat("<td style='font-family:Verdana; font-size:small; text-align:center'>{0}</td>", ds.Tables[0].Rows[0]["vill_city"]);
            rpt.Append("<td style='font-family:Verdana; font-size:small; text-align:center'> নিজে </td>");
            rpt.Append("</tr >");

            rpt.Append("<tr style='height:27px'>");
            rpt.Append("<td style='font-family:Verdana;  font-size:small; text-align:center'>..................</td>");
            rpt.AppendFormat("<td style='font-family:Verdana;  font-size:small; text-align:center'>{0}</td>", ds.Tables[0].Rows[0]["guardian_name"]);
            rpt.AppendFormat("<td style='font-family:Verdana; font-size:small; text-align:center'>{0}</td>", ds.Tables[0].Rows[0]["vill_city2"]);
            rpt.AppendFormat("<td style='font-family:Verdana; font-size:small; text-align:center'> {0} </td>", ds.Tables[0].Rows[0]["relation"]);
            rpt.Append("</tr >");
            rpt.Append("</table>"); 
        } 
        ltrReport.Visible = true;
    }

    public void GetHearder_NeonatalConvulsion1(string reg)
    {
        ltrReport.Text = "";
        DataSet ds = thepatientbill.PatientForReport(reg, Session["CoCode"].ToString().Trim());
        if (ds.Tables[0].Rows.Count > 0)
        {
            rpt.Append("<table width='100%' cellspacing=0 border=0 style='border:1px solid black;border-spacing: 0; padding: 0;font-family:verdana;'>");
            rpt.Append("<tr style='height:25px;'>");
            rpt.Append("<td style='font-family:Verdana; font-size:x-large;text-align:center'><b>Some of the critical situation of the patient about the risks and accept the Letter</b>");
            rpt.Append("</tr >");

            rpt.Append("<tr style='height:20px;'>");
            rpt.AppendFormat("<td style='font-family:Verdana; font-size:medium;text-align:center'>(Admitted By :{0}, Relation :{1})</td>", ds.Tables[0].Rows[0]["guardian_name"], ds.Tables[0].Rows[0]["relation"]);
            rpt.Append("</tr >");
            rpt.Append("</table>");

            PatientDetails_English(ds);
            rpt.Append("<br/>");


            rpt.Append("<table width='100%'>");
            rpt.Append("<tr style='height:25px'>"); rpt.Append("<u>");
            rpt.Append("<td style='font-family:Verdana;font-size:medium;font-weight:bold; text-align:left'>Problems or risk factors of the patient  : </td>"); rpt.Append("</u>");
            rpt.Append("</tr >");

            rpt.Append("<tr style='height:25px'>");
            rpt.Append("<td style='font-family:Verdana;font-size:small; text-align:left'>Anabarata spasm biography of the low, deficient lungs, heart weak, other organs, immature, feeding problems, when the child's condition is worse prananata</td>");
            rpt.Append("</tr >");

            rpt.Append("<tr style='height:25px'>"); rpt.Append("<u>");
            rpt.Append("<td style='font-family:Verdana;font-size:medium;font-weight:bold;text-align:left'>Hazard that may occur in the patient  :</td>"); rpt.Append("</u>");
            rpt.Append("</tr >");

            rpt.Append("<tr style='height:25px'>");
            rpt.Append("<td style='font-family:Verdana;font-size:small; text-align:left'>Sepasisa inaphekasana or may be, may be shortness of breath, bleeding may brene, hrdayanra can crash suddenly, stomach swelling can be worse when the child's condition, and may even die, that could be taken away at any moment, there are no guarantees in the treatment of this disease </td>");
            rpt.Append("</tr >");
            rpt.Append("</table>");
            rpt.Append("<br/>");
            rpt.Append("<table width='100%'>");
            rpt.Append("<tr style='height:25px'>");
            rpt.Append("<td style='font-family:Verdana;font-size:small; text-align:justify'> The organization learned from the patient about these issues and concerns . This organization has repeatedly us from any government medical college or any other health organizations have been asked to take , we suggest that these institutions have agreed along . But now the situation is like taking away patients who do not have , so we are keeping in mind the patient's life is at risk , including hostile cikitsara emergency admissions for patients who have made this institution , the patient gave permission for the necessary treatment .  </td>");
            rpt.Append("</tr >");
            rpt.Append("</table>");
            rpt.Append("<br/>");
            rpt.Append("<table width='100%'>");
            rpt.Append("<tr style='height:25px'>");
            rpt.Append("<td style=' font-family:Verdana;font-size:small; text-align:justify'> We think it's healthy to talk with our kindred allowed to treat the patient during treatment, or in this case, the organization would be without the complicated error occurred when the patient or any other life-threatening if not, we'll blame the institution. </td>");
            rpt.Append("</tr >");
            rpt.Append("</table>");


            rpt.Append("<table width='100%'>");
            rpt.Append("<tr style='height:27px'>");
            rpt.Append("<td style='font-family:Verdana; font-size:medium; text-align:center'> Signature </td>");
            rpt.Append("<td style='font-family:Verdana; font-size:medium; text-align:center'> Full Name  </td>");
            rpt.Append("<td style='font-family:Verdana; font-size:medium; text-align:center'> Address </td>");
            rpt.Append("<td style='font-family:Verdana; font-size:medium; text-align:center'> Relation  </td>");
            rpt.Append("</tr >");

            rpt.Append("<tr style='height:27px'>");
            rpt.Append("<td style='font-family:Verdana;  font-size:small; text-align:center'>..................</td>");
            rpt.AppendFormat("<td style='font-family:Verdana;  font-size:small; text-align:center'>{0}</td>", ds.Tables[0].Rows[0]["patient_name"]);
            rpt.AppendFormat("<td style='font-family:Verdana; font-size:small; text-align:center'>{0}</td>", ds.Tables[0].Rows[0]["vill_city"]);
            rpt.Append("<td style='font-family:Verdana; font-size:small; text-align:center'> Self </td>");
            rpt.Append("</tr >");

            rpt.Append("<tr style='height:27px'>");
            rpt.Append("<td style='font-family:Verdana;  font-size:small; text-align:center'>..................</td>");
            rpt.AppendFormat("<td style='font-family:Verdana;  font-size:small; text-align:center'>{0}</td>", ds.Tables[0].Rows[0]["guardian_name"]);
            rpt.AppendFormat("<td style='font-family:Verdana; font-size:small; text-align:center'>{0}</td>", ds.Tables[0].Rows[0]["vill_city2"]);
            rpt.AppendFormat("<td style='font-family:Verdana; font-size:small; text-align:center'> {0} </td>", ds.Tables[0].Rows[0]["relation"]);
            rpt.Append("</tr >");
            rpt.Append("</table>"); 
        }
        ltrReport.Visible = true;
    }

    public void GetHearder_NeonatalHighFeverReport(string reg)
    {
        ltrReport.Text = "";
        DataSet ds = thepatientbill.PatientForReport(reg, Session["CoCode"].ToString().Trim());
        if (ds.Tables[0].Rows.Count > 0)
        {
            rpt.Append("<table width='100%' cellspacing=0 border=0 style='border:1px solid black;border-spacing: 0; padding: 0;font-family:verdana;'>");
            rpt.Append("<tr style='height:25px;'>");
            rpt.Append("<td style='font-family:Verdana; font-size:x-large;text-align:center'><b>রোগীর বিশেষ কয়েকটি আশঙ্কাজনক পরিস্থিতি ও তার ঝুঁকি সম্পর্কে স্বীকার পত্র</b>");
            rpt.Append("</tr >");

            rpt.Append("<tr style='height:20px;'>");
            rpt.AppendFormat("<td style='font-family:Verdana; font-size:medium;text-align:center'>(যিনি ভর্তি করাচ্ছেন তার নাম:{0},সম্পর্ক:{1})</td>", ds.Tables[0].Rows[0]["guardian_name"], ds.Tables[0].Rows[0]["relation"]);
            rpt.Append("</tr >");
            rpt.Append("</table>");

            PatientDetails_Bengali(ds);
            rpt.Append("<br/>"); 


            rpt.Append("<table width='100%'>");
            rpt.Append("<tr style='height:25px'>"); rpt.Append("<u>");
            rpt.Append("<td style='font-family:Verdana;font-size:medium;font-weight:bold; text-align:left'>রোগীর সমস্যা বা ঝুঁকির কারণ সমূহ: </td>"); rpt.Append("</u>");
            rpt.Append("</tr >");

            rpt.Append("<tr style='height:35px'>");
            rpt.Append("<td style='font-family:Verdana;font-size:medium; text-align:left'>প্রচন্ড জ্বর,জীবনী শক্তি কম, ফুসফুস কমজোরী, হার্ট দূর্বল, খাওয়ানোর সমস্যা, যখন তখন বাচ্চার অবস্থা খারাপ হওয়ার প্রনণতা</td>");
            rpt.Append("</tr >");

            rpt.Append("<tr style='height:35px'>"); rpt.Append("<u>");
            rpt.Append("<td style='font-family:Verdana;font-size:medium;font-weight:bold;text-align:left'>ওই সমস্ত রোগীর যে সমস্ত বিপত্তি ঘটতে পারে :</td>"); rpt.Append("</u>");
            rpt.Append("</tr >");

            rpt.Append("<tr style='height:35px'>");
            rpt.Append("<td style='font-family:Verdana;font-size:medium; text-align:left'>ইনফেকসন বা সেপশিস হতে পারে, শ্বাসকষ্ট হতে পারে, হঠাৎ হৃদযন্র বিকল হতে পারে, পেট ফুলে যেতে পারে, ব্রেনে রক্তপাত হতে পারে, যখন তখন বাচ্চার অবস্থা খারাপ হতে পারে, এমন কি মারা ও যেতে পারে, যে কোন মুহূর্তে অন্যত্র নিয়ে যেতে হতে পারে</td>");
            rpt.Append("</tr >");
            rpt.Append("</table>");
            rpt.Append("<br/>");
            rpt.Append("<table width='100%'>");
            rpt.Append("<tr style='height:30px'>");
            rpt.Append("<td style='font-family:Verdana;font-size:medium; text-align:justify'>এই প্রতিষ্ঠানের পক্ষ থেকে রোগীর এই সমস্ত সমস্যা ও আশঙ্কার কথা জানলাম । এই প্রতিষ্ঠানের পক্ষ থেকে আমাদের বারবার কোন সরকারী মেডিকেল কলেজ কিংবা অন্য কোনও স্বাস্থ্য প্রতিষ্ঠানের নিয়ে যাবার কথা বলা হয়েছে , আমরাও এই প্রতিষ্ঠান যে সমস্ত পরামর্শ দিয়েছে তারসঙ্গে একমত । কিন্তু এই মূহুর্তে রোগীকে অন্যত্র নিয়ে যাবার মতো পরিস্থিতি আমাদের নেই , তাই রোগীর জীবন হানী সহ সমস্ত ঝুঁকির কথা মাথায় রেখেই আমরা আপদকালীন চিকিৎ্সার জন্য রোগীকে এই প্রতিষ্ঠানে ভর্তি করলাম , এখানে রোগীর প্রয়োজনীয় যাবতীয় চিকিত্সা করার জন্য অনুমতি দিলাম । </td>");
            rpt.Append("</tr >");
            rpt.Append("</table>");
            rpt.Append("<br/>");
            rpt.Append("<table width='100%'>");
            rpt.Append("<tr style='height:30px'>");
            rpt.Append("<td style=' font-family:Verdana;font-size:medium; text-align:justify'>আমরা যেহেতু সুস্থ মনে আমাদের আত্মীয় পরিজনদের সঙ্গে আলোচনা করে রোগীর চিকিত্সা চালানোর অনুমতি দিচ্ছি সেক্ষেত্রে চিকিত্সা চলাকালীন কিংবা এই প্রতিষ্ঠান থেকে ছাড়া পাওয়ার পর রোগীর অন্য কোন জটিল সম্যসা হলে কিংবা জীবন সংশয় হলে আমরা এই প্রতিষ্ঠানকে দায়ী করব না ।</td>");
            rpt.Append("</tr >");
            rpt.Append("</table>");

            rpt.Append("<table width='100%'>");
            rpt.Append("<tr style='height:35px'>");
            rpt.Append("<td style='font-family:Verdana; font-size:medium; text-align:center'> স্বাক্ষর/টিপ </td>");
            rpt.Append("<td style='font-family:Verdana; font-size:medium; text-align:center'> পুরো  নাম  </td>");
            rpt.Append("<td style='font-family:Verdana; font-size:medium; text-align:center'> ঠিকানা </td>");
            rpt.Append("<td style='font-family:Verdana; font-size:medium; text-align:center'> সম্পর্ক  </td>");
            rpt.Append("</tr >");

            rpt.Append("<tr style='height:27px'>");
            rpt.Append("<td style='font-family:Verdana;  font-size:small; text-align:center'>..................</td>");
            rpt.AppendFormat("<td style='font-family:Verdana;  font-size:small; text-align:center'>{0}</td>", ds.Tables[0].Rows[0]["patient_name"]);
            rpt.AppendFormat("<td style='font-family:Verdana; font-size:small; text-align:center'>{0}</td>", ds.Tables[0].Rows[0]["vill_city"]);
            rpt.Append("<td style='font-family:Verdana; font-size:small; text-align:center'> নিজে </td>");
            rpt.Append("</tr >");

            rpt.Append("<tr style='height:27px'>");
            rpt.Append("<td style='font-family:Verdana;  font-size:small; text-align:center'>..................</td>");
            rpt.AppendFormat("<td style='font-family:Verdana;  font-size:small; text-align:center'>{0}</td>", ds.Tables[0].Rows[0]["guardian_name"]);
            rpt.AppendFormat("<td style='font-family:Verdana; font-size:small; text-align:center'>{0}</td>", ds.Tables[0].Rows[0]["vill_city"]);
            rpt.AppendFormat("<td style='font-family:Verdana; font-size:small; text-align:center'> {0} </td>", ds.Tables[0].Rows[0]["relation"]);
            rpt.Append("</tr >");
            rpt.Append("</table>"); 
        }
        ltrReport.Visible = true;
    }

    public void GetHearder_NeonatalHighFeverReport1(string reg)
    {
        ltrReport.Text = "";
        DataSet ds = thepatientbill.PatientForReport(reg, Session["CoCode"].ToString().Trim());
        if (ds.Tables[0].Rows.Count > 0)
        {
            rpt.Append("<table width='100%' cellspacing=0 border=0 style='border:1px solid black;border-spacing: 0; padding: 0;font-family:verdana;'>");
            rpt.Append("<tr style='height:25px;'>");
            rpt.Append("<td style='font-family:Verdana; font-size:x-large;text-align:center'><b>Some of the critical situation of the patient about the risks and accept the Letter</b>");
            rpt.Append("</tr >");

            rpt.Append("<tr style='height:20px;'>");
            rpt.AppendFormat("<td style='font-family:Verdana; font-size:medium;text-align:center'>(Admitted By :{0}, Relation :{1})</td>", ds.Tables[0].Rows[0]["guardian_name"], ds.Tables[0].Rows[0]["relation"]);
            rpt.Append("</tr >");
            rpt.Append("</table>");

            PatientDetails_English(ds);
            rpt.Append("<br/>");

            rpt.Append("<table width='100%'>");
            rpt.Append("<tr style='height:25px'>"); rpt.Append("<u>");
            rpt.Append("<td style='font-family:Verdana;font-size:medium;font-weight:bold; text-align:left'>Problems or risk factors of the patient  : </td>"); rpt.Append("</u>");
            rpt.Append("</tr >");

            rpt.Append("<tr style='height:25px'>");
            rpt.Append("<td style='font-family:Verdana;font-size:small; text-align:left'>Severe fever, biographies of the low, deficient lungs, heart weakness, feeding difficulties, when the child's condition is worse prananata</td>");
            rpt.Append("</tr >");

            rpt.Append("<tr style='height:25px'>"); rpt.Append("<u>");
            rpt.Append("<td style='font-family:Verdana;font-size:medium;font-weight:bold;text-align:left'>Hazard that may occur in the patient  :</td>"); rpt.Append("</u>");
            rpt.Append("</tr >");

            rpt.Append("<tr style='height:25px'>");
            rpt.Append("<td style='font-family:Verdana;font-size:small; text-align:left'>Sepasisa inaphekasana or may be, may be shortness of breath, sudden hrdayanra could blink, the stomach may be swollen, bleeding may brene, when the child could be worse, and may even die, that may go away at any moment</td>");
            rpt.Append("</tr >");
            rpt.Append("</table>");
            rpt.Append("<br/>");
            rpt.Append("<table width='100%'>");
            rpt.Append("<tr style='height:25px'>");
            rpt.Append("<td style='font-family:Verdana;font-size:small; text-align:justify'> The organization learned from the patient about these issues and concerns . This organization has repeatedly us from any government medical college or any other health organizations have been asked to take , we suggest that these institutions have agreed along . But now the situation is like taking away patients who do not have , so we are keeping in mind the patient's life is at risk , including hostile cikitsara emergency admissions for patients who have made this institution , the patient gave permission for the necessary treatment .  </td>");
            rpt.Append("</tr >");
            rpt.Append("</table>");
            rpt.Append("<br/>");
            rpt.Append("<table width='100%'>");
            rpt.Append("<tr style='height:25px'>");
            rpt.Append("<td style=' font-family:Verdana;font-size:small; text-align:justify'> We think it's healthy to talk with our kindred allowed to treat the patient during treatment, or in this case, the organization would be without the complicated error occurred when the patient or any other life-threatening if not, we'll blame the institution. </td>");
            rpt.Append("</tr >");
            rpt.Append("</table>");


            rpt.Append("<table width='100%'>");
            rpt.Append("<tr style='height:27px'>");
            rpt.Append("<td style='font-family:Verdana; font-size:medium; text-align:center'> Signature </td>");
            rpt.Append("<td style='font-family:Verdana; font-size:medium; text-align:center'> Full Name  </td>");
            rpt.Append("<td style='font-family:Verdana; font-size:medium; text-align:center'> Address </td>");
            rpt.Append("<td style='font-family:Verdana; font-size:medium; text-align:center'> Relation  </td>");
            rpt.Append("</tr >");

            rpt.Append("<tr style='height:27px'>");
            rpt.Append("<td style='font-family:Verdana;  font-size:small; text-align:center'>..................</td>");
            rpt.AppendFormat("<td style='font-family:Verdana;  font-size:small; text-align:center'>{0}</td>", ds.Tables[0].Rows[0]["patient_name"]);
            rpt.AppendFormat("<td style='font-family:Verdana; font-size:small; text-align:center'>{0}</td>", ds.Tables[0].Rows[0]["vill_city"]);
            rpt.Append("<td style='font-family:Verdana; font-size:small; text-align:center'> Self </td>");
            rpt.Append("</tr >");

            rpt.Append("<tr style='height:27px'>");
            rpt.Append("<td style='font-family:Verdana;  font-size:small; text-align:center'>..................</td>");
            rpt.AppendFormat("<td style='font-family:Verdana;  font-size:small; text-align:center'>{0}</td>", ds.Tables[0].Rows[0]["guardian_name"]);
            rpt.AppendFormat("<td style='font-family:Verdana; font-size:small; text-align:center'>{0}</td>", ds.Tables[0].Rows[0]["vill_city2"]);
            rpt.AppendFormat("<td style='font-family:Verdana; font-size:small; text-align:center'> {0} </td>", ds.Tables[0].Rows[0]["relation"]);
            rpt.Append("</tr >");
            rpt.Append("</table>"); 
        } 
        ltrReport.Visible = true;
    }

    public void GetHearder_NeonatalJaundiceReport(string regno)
    {
        ltrReport.Text = "";
        DataSet ds = thepatientbill.PatientForReport(regno, Session["CoCode"].ToString().Trim());
        if (ds.Tables[0].Rows.Count > 0)
        {
            rpt.Append("<table width='100%' cellspacing=0 border=0 style='border:1px solid black;border-spacing: 0; padding: 0;font-family:verdana;'>");
            rpt.Append("<tr style='height:25px;'>");
            rpt.Append("<td style='font-family:Verdana; font-size:x-large;text-align:center'><b>রোগীর বিশেষ কয়েকটি আশঙ্কাজনক পরিস্থিতি ও তার ঝুঁকি সম্পর্কে স্বীকার পত্র</b>");
            rpt.Append("</tr >");

            rpt.Append("<tr style='height:20px;'>");
            rpt.AppendFormat("<td style='font-family:Verdana; font-size:medium;text-align:center'>(যিনি ভর্তি করাচ্ছেন তার নাম:{0},সম্পর্ক:{1})</td>", ds.Tables[0].Rows[0]["guardian_name"], ds.Tables[0].Rows[0]["relation"]);
            rpt.Append("</tr >");
            rpt.Append("</table>");

            PatientDetails_Bengali(ds);
            rpt.Append("<br/>"); 

            rpt.Append("<table width='100%'>");
            rpt.Append("<tr style='height:25px'>"); rpt.Append("<u>");
            rpt.Append("<td style='font-family:Verdana;font-size:medium;font-weight:bold; text-align:left'>রোগীর সমস্যা বা ঝুঁকির কারণ সমূহ: </td>"); rpt.Append("</u>");
            rpt.Append("</tr >");

            rpt.Append("<tr style='height:35px'>");
            rpt.Append("<td style='font-family:Verdana;font-size:medium; text-align:left'>বেশী জন্ডিস, খিঁচুনি,জীবনী শক্তি কম, লিভারের অবস্থা খারাপ, ফুসফুস কমজোরী, হার্ট দূর্বল, অন্যান্য অঙ্গ ভাল কাজ করছে না, খাওয়ানোর সমস্যা, যখন তখন বাচ্চার অবস্থা খারাপ হওয়ার প্রনণতা</td>");
            rpt.Append("</tr >");

            rpt.Append("<tr style='height:35px'>"); rpt.Append("<u>");
            rpt.Append("<td style='font-family:Verdana;font-size:medium;font-weight:bold;text-align:left'>ওই সমস্ত রোগীর যে সমস্ত বিপত্তি ঘটতে পারে :</td>"); rpt.Append("</u>");
            rpt.Append("</tr >");

            rpt.Append("<tr style='height:35px'>");
            rpt.Append("<td style='font-family:Verdana;font-size:medium; text-align:left'>ইনফেকসন বা সেপশিস হতে পারে, শ্বাসকষ্ট হতে পারে, ব্রেনে রক্তপাত হতে পারে, হঠাৎ হৃদযন্র বিকল হতে পারে, পেট ফুলে যেতে পারে, যখন তখন বাচ্চার অবস্থা খারাপ হতে পারে, লিভার অকেজো হতে পারে, এমন কি মারা ও যেতে পারে, যে কোন মুহূর্তে অন্যত্র নিয়ে যেতে হতে পারে</td>");
            rpt.Append("</tr >");
            rpt.Append("</table>");
            rpt.Append("<br/>");
            rpt.Append("<table width='100%'>");
            rpt.Append("<tr style='height:30px'>");
            rpt.Append("<td style='font-family:Verdana;font-size:medium; text-align:justify'>এই প্রতিষ্ঠানের পক্ষ থেকে রোগীর এই সমস্ত সমস্যা ও আশঙ্কার কথা জানলাম । এই প্রতিষ্ঠানের পক্ষ থেকে আমাদের বারবার কোন সরকারী মেডিকেল কলেজ কিংবা অন্য কোনও স্বাস্থ্য প্রতিষ্ঠানের নিয়ে যাবার কথা বলা হয়েছে , আমরাও এই প্রতিষ্ঠান যে সমস্ত পরামর্শ দিয়েছে তারসঙ্গে একমত । কিন্তু এই মূহুর্তে রোগীকে অন্যত্র নিয়ে যাবার মতো পরিস্থিতি আমাদের নেই , তাই রোগীর জীবন হানী সহ সমস্ত ঝুঁকির কথা মাথায় রেখেই আমরা আপদকালীন চিকিৎ্সার জন্য রোগীকে এই প্রতিষ্ঠানে ভর্তি করলাম , এখানে রোগীর প্রয়োজনীয় যাবতীয় চিকিত্সা করার জন্য অনুমতি দিলাম । </td>");
            rpt.Append("</tr >");
            rpt.Append("</table>");
            rpt.Append("<br/>");
            rpt.Append("<table width='100%'>");
            rpt.Append("<tr style='height:30px'>");
            rpt.Append("<td style=' font-family:Verdana;font-size:medium; text-align:justify'>আমরা যেহেতু সুস্থ মনে আমাদের আত্মীয় পরিজনদের সঙ্গে আলোচনা করে রোগীর চিকিত্সা চালানোর অনুমতি দিচ্ছি সেক্ষেত্রে চিকিত্সা চলাকালীন কিংবা এই প্রতিষ্ঠান থেকে ছাড়া পাওয়ার পর রোগীর অন্য কোন জটিল সম্যসা হলে কিংবা জীবন সংশয় হলে আমরা এই প্রতিষ্ঠানকে দায়ী করব না ।</td>");
            rpt.Append("</tr >");
            rpt.Append("</table>");




            rpt.Append("<table width='100%'>");
            rpt.Append("<tr style='height:35px'>");
            rpt.Append("<td style='font-family:Verdana; font-size:medium; text-align:center'> স্বাক্ষর/টিপ </td>");
            rpt.Append("<td style='font-family:Verdana; font-size:medium; text-align:center'> পুরো  নাম  </td>");
            rpt.Append("<td style='font-family:Verdana; font-size:medium; text-align:center'> ঠিকানা </td>");
            rpt.Append("<td style='font-family:Verdana; font-size:medium; text-align:center'> সম্পর্ক  </td>");
            rpt.Append("</tr >");

            rpt.Append("<tr style='height:27px'>");
            rpt.Append("<td style='font-family:Verdana;  font-size:small; text-align:center'>..................</td>");
            rpt.AppendFormat("<td style='font-family:Verdana;  font-size:small; text-align:center'>{0}</td>", ds.Tables[0].Rows[0]["patient_name"]);
            rpt.AppendFormat("<td style='font-family:Verdana; font-size:small; text-align:center'>{0}</td>", ds.Tables[0].Rows[0]["vill_city"]);
            rpt.Append("<td style='font-family:Verdana; font-size:small; text-align:center'> নিজে </td>");
            rpt.Append("</tr >");

            rpt.Append("<tr style='height:27px'>");
            rpt.Append("<td style='font-family:Verdana;  font-size:small; text-align:center'>..................</td>");
            rpt.AppendFormat("<td style='font-family:Verdana;  font-size:small; text-align:center'>{0}</td>", ds.Tables[0].Rows[0]["guardian_name"]);
            rpt.AppendFormat("<td style='font-family:Verdana; font-size:small; text-align:center'>{0}</td>", ds.Tables[0].Rows[0]["vill_city"]);
            rpt.AppendFormat("<td style='font-family:Verdana; font-size:small; text-align:center'> {0} </td>", ds.Tables[0].Rows[0]["relation"]);
            rpt.Append("</tr >");
            rpt.Append("</table>"); 
        } 
        ltrReport.Visible = true;
    }

    public void GetHearder_NeonatalJaundiceReport1(string regno)
    {
        ltrReport.Text = "";
        DataSet ds = thepatientbill.PatientForReport(regno, Session["CoCode"].ToString().Trim());
        if (ds.Tables[0].Rows.Count > 0)
        {
            rpt.Append("<table width='100%' cellspacing=0 border=0 style='border:1px solid black;border-spacing: 0; padding: 0;font-family:verdana;'>");
            rpt.Append("<tr style='height:25px;'>");
            rpt.Append("<td style='font-family:Verdana; font-size:x-large;text-align:center'><b>Some of the critical situation of the patient about the risks and accept the Letter</b>");
            rpt.Append("</tr >");

            rpt.Append("<tr style='height:20px;'>");
            rpt.AppendFormat("<td style='font-family:Verdana; font-size:medium;text-align:center'>(Admitted By :{0}, Relation :{1})</td>", ds.Tables[0].Rows[0]["guardian_name"], ds.Tables[0].Rows[0]["relation"]);
            rpt.Append("</tr >");
            rpt.Append("</table>");

            PatientDetails_English(ds);
            rpt.Append("<br/>");

            rpt.Append("<table width='100%'>");
            rpt.Append("<tr style='height:25px'>"); rpt.Append("<u>");
            rpt.Append("<td style='font-family:Verdana;font-size:medium;font-weight:bold; text-align:left'>Problems or risk factors of the patient  : </td>"); rpt.Append("</u>");
            rpt.Append("</tr >");

            rpt.Append("<tr style='height:25px'>");
            rpt.Append("<td style='font-family:Verdana;font-size:small; text-align:left'> Than jaundice, convulsions, biographies energy less severe liver condition, deficient lungs, heart weak, does not work well in other organs, feeding problems, when the child's condition is worse prananata </td>");
            rpt.Append("</tr >");

            rpt.Append("<tr style='height:25px'>"); rpt.Append("<u>");
            rpt.Append("<td style='font-family:Verdana;font-size:medium;font-weight:bold;text-align:left'>Hazard that may occur in the patient  :</td>"); rpt.Append("</u>");
            rpt.Append("</tr >");

            rpt.Append("<tr style='height:25px'>");
            rpt.Append("<td style='font-family:Verdana;font-size:small; text-align:left'>Sepasisa inaphekasana or may be, may be shortness of breath, bleeding may brene, hrdayanra can crash suddenly, the stomach may be swollen, when the child could be worse, could be liver failure, and even death can be anywhere at any time may be taken</td>");
            rpt.Append("</tr >");
            rpt.Append("</table>");
            rpt.Append("<br/>");
            rpt.Append("<table width='100%'>");
            rpt.Append("<tr style='height:25px'>");
            rpt.Append("<td style='font-family:Verdana;font-size:small; text-align:justify'> The organization learned from the patient about these issues and concerns . This organization has repeatedly us from any government medical college or any other health organizations have been asked to take , we suggest that these institutions have agreed along . But now the situation is like taking away patients who do not have , so we are keeping in mind the patient's life is at risk , including hostile cikitsara emergency admissions for patients who have made this institution , the patient gave permission for the necessary treatment .  </td>");
            rpt.Append("</tr >");
            rpt.Append("</table>");
            rpt.Append("<br/>");
            rpt.Append("<table width='100%'>");
            rpt.Append("<tr style='height:25px'>");
            rpt.Append("<td style=' font-family:Verdana;font-size:small; text-align:justify'> We think it's healthy to talk with our kindred allowed to treat the patient during treatment, or in this case, the organization would be without the complicated error occurred when the patient or any other life-threatening if not, we'll blame the institution. </td>");
            rpt.Append("</tr >");
            rpt.Append("</table>");


            rpt.Append("<table width='100%'>");
            rpt.Append("<tr style='height:27px'>");
            rpt.Append("<td style='font-family:Verdana; font-size:medium; text-align:center'> Signature </td>");
            rpt.Append("<td style='font-family:Verdana; font-size:medium; text-align:center'> Full Name  </td>");
            rpt.Append("<td style='font-family:Verdana; font-size:medium; text-align:center'> Address </td>");
            rpt.Append("<td style='font-family:Verdana; font-size:medium; text-align:center'> Relation  </td>");
            rpt.Append("</tr >");

            rpt.Append("<tr style='height:27px'>");
            rpt.Append("<td style='font-family:Verdana;  font-size:small; text-align:center'>..................</td>");
            rpt.AppendFormat("<td style='font-family:Verdana;  font-size:small; text-align:center'>{0}</td>", ds.Tables[0].Rows[0]["patient_name"]);
            rpt.AppendFormat("<td style='font-family:Verdana; font-size:small; text-align:center'>{0}</td>", ds.Tables[0].Rows[0]["vill_city"]);
            rpt.Append("<td style='font-family:Verdana; font-size:small; text-align:center'> Self </td>");
            rpt.Append("</tr >");

            rpt.Append("<tr style='height:27px'>");
            rpt.Append("<td style='font-family:Verdana;  font-size:small; text-align:center'>..................</td>");
            rpt.AppendFormat("<td style='font-family:Verdana;  font-size:small; text-align:center'>{0}</td>", ds.Tables[0].Rows[0]["guardian_name"]);
            rpt.AppendFormat("<td style='font-family:Verdana; font-size:small; text-align:center'>{0}</td>", ds.Tables[0].Rows[0]["vill_city2"]);
            rpt.AppendFormat("<td style='font-family:Verdana; font-size:small; text-align:center'> {0} </td>", ds.Tables[0].Rows[0]["relation"]);
            rpt.Append("</tr >");
            rpt.Append("</table>");
        } 
        ltrReport.Visible = true;
    }

    public void GetHearder_NeoNatalphysia(string reg)
    {
        ltrReport.Text = "";
        DataSet ds = thepatientbill.PatientForReport(reg, Session["CoCode"].ToString().Trim());
        if (ds.Tables[0].Rows.Count > 0)
        {
            rpt.Append("<table width='100%' cellspacing=0 border=0 style='border:1px solid black;border-spacing: 0; padding: 0;font-family:verdana;'>");
            rpt.Append("<tr style='height:25px;'>");
            rpt.Append("<td style='font-family:Verdana; font-size:x-large;text-align:center'><b>রোগীর বিশেষ কয়েকটি আশঙ্কাজনক পরিস্থিতি ও তার ঝুঁকি সম্পর্কে স্বীকার পত্র</b>");
            rpt.Append("</tr >");

            rpt.Append("<tr style='height:20px;'>");
            rpt.AppendFormat("<td style='font-family:Verdana; font-size:medium;text-align:center'>(যিনি ভর্তি করাচ্ছেন তার নাম:{0},সম্পর্ক:{1})</td>", ds.Tables[0].Rows[0]["guardian_name"], ds.Tables[0].Rows[0]["relation"]);
            rpt.Append("</tr >");
            rpt.Append("</table>");

            PatientDetails_Bengali(ds);
            rpt.Append("<br/>"); 

            rpt.Append("<table width='100%'>");
            rpt.Append("<tr style='height:25px'>"); rpt.Append("<u>");
            rpt.Append("<td style='font-family:Verdana;font-size:medium;font-weight:bold; text-align:left'>রোগীর সমস্যা বা ঝুঁকির কারণ সমূহ: </td>"); rpt.Append("</u>");
            rpt.Append("</tr >");

            rpt.Append("<tr style='height:35px'>");
            rpt.Append("<td style='font-family:Verdana;font-size:medium; text-align:left'>ব্রেনে অক্সিজেন কম হওয়ায় ব্রেন ড্যামেজ, ব্রেনে রক্তপাত , ইনফেকসন বা সেপশিস , প্রচন্ড শ্বাসকষ্ট, খিঁচুনি</td>");
            rpt.Append("</tr >");

            rpt.Append("<tr style='height:35px'>"); rpt.Append("<u>");
            rpt.Append("<td style='font-family:Verdana;font-size:medium;font-weight:bold;text-align:left'>ওই সমস্ত রোগীর যে সমস্ত বিপত্তি ঘটতে পারে :</td>"); rpt.Append("</u>");
            rpt.Append("</tr >");

            rpt.Append("<tr style='height:35px'>");
            rpt.Append("<td style='font-family:Verdana;font-size:medium; text-align:left'>ইনফেকসন বা সেপশিস হতে পারে, জন্ডিস হতে পারে,শ্বাসকষ্ট হতে পারে, ব্রেনে রক্তপাত হতে পারে, হঠাৎ হৃদযন্র বিকল হতে পারে, পেট ফুলে যেতে পারে, যখন তখন বাচ্চার অবস্থা খারাপ হতে পারে, এমন কি মারা ও যেতে পারে, যে কোন মুহূর্তে অন্যত্র নিয়ে যেতে হতে পারে,এই রোগের চিকিৎসার কোন গ্যারান্টী নেই, বেঁচে থাকলে সুদূর প্রসারী ব্রেন ড্যমেজ হয়ে ব্রেন ভাল কাজ না করতে পারে</td>");
            rpt.Append("</tr >");
            rpt.Append("</table>");
            rpt.Append("<br/>");
            rpt.Append("<table width='100%'>");
            rpt.Append("<tr style='height:30px'>");
            rpt.Append("<td style='font-family:Verdana;font-size:medium; text-align:justify'>এই প্রতিষ্ঠানের পক্ষ থেকে রোগীর এই সমস্ত সমস্যা ও আশঙ্কার কথা জানলাম । এই প্রতিষ্ঠানের পক্ষ থেকে আমাদের বারবার কোন সরকারী মেডিকেল কলেজ কিংবা অন্য কোনও স্বাস্থ্য প্রতিষ্ঠানের নিয়ে যাবার কথা বলা হয়েছে , আমরাও এই প্রতিষ্ঠান যে সমস্ত পরামর্শ দিয়েছে তারসঙ্গে একমত । কিন্তু এই মূহুর্তে রোগীকে অন্যত্র নিয়ে যাবার মতো পরিস্থিতি আমাদের নেই , তাই রোগীর জীবন হানী সহ সমস্ত ঝুঁকির কথা মাথায় রেখেই আমরা আপদকালীন চিকিৎ্সার জন্য রোগীকে এই প্রতিষ্ঠানে ভর্তি করলাম , এখানে রোগীর প্রয়োজনীয় যাবতীয় চিকিত্সা করার জন্য অনুমতি দিলাম । </td>");
            rpt.Append("</tr >");
            rpt.Append("</table>");
            rpt.Append("<br/>");
            rpt.Append("<table width='100%'>");
            rpt.Append("<tr style='height:30px'>");
            rpt.Append("<td style=' font-family:Verdana;font-size:medium; text-align:justify'>আমরা যেহেতু সুস্থ মনে আমাদের আত্মীয় পরিজনদের সঙ্গে আলোচনা করে রোগীর চিকিত্সা চালানোর অনুমতি দিচ্ছি সেক্ষেত্রে চিকিত্সা চলাকালীন কিংবা এই প্রতিষ্ঠান থেকে ছাড়া পাওয়ার পর রোগীর অন্য কোন জটিল সম্যসা হলে কিংবা জীবন সংশয় হলে আমরা এই প্রতিষ্ঠানকে দায়ী করব না ।</td>");
            rpt.Append("</tr >");
            rpt.Append("</table>");




            rpt.Append("<table width='100%'>");
            rpt.Append("<tr style='height:35px'>");
            rpt.Append("<td style='font-family:Verdana; font-size:medium; text-align:center'> স্বাক্ষর/টিপ </td>");
            rpt.Append("<td style='font-family:Verdana; font-size:medium; text-align:center'> পুরো  নাম  </td>");
            rpt.Append("<td style='font-family:Verdana; font-size:medium; text-align:center'> ঠিকানা </td>");
            rpt.Append("<td style='font-family:Verdana; font-size:medium; text-align:center'> সম্পর্ক  </td>");
            rpt.Append("</tr >");

            rpt.Append("<tr style='height:27px'>");
            rpt.Append("<td style='font-family:Verdana;  font-size:small; text-align:center'>..................</td>");
            rpt.AppendFormat("<td style='font-family:Verdana;  font-size:small; text-align:center'>{0}</td>", ds.Tables[0].Rows[0]["patient_name"]);
            rpt.AppendFormat("<td style='font-family:Verdana; font-size:small; text-align:center'>{0}</td>", ds.Tables[0].Rows[0]["vill_city"]);
            rpt.Append("<td style='font-family:Verdana; font-size:small; text-align:center'> নিজে </td>");
            rpt.Append("</tr >");

            rpt.Append("<tr style='height:27px'>");
            rpt.Append("<td style='font-family:Verdana;  font-size:small; text-align:center'>..................</td>");
            rpt.AppendFormat("<td style='font-family:Verdana;  font-size:small; text-align:center'>{0}</td>", ds.Tables[0].Rows[0]["guardian_name"]);
            rpt.AppendFormat("<td style='font-family:Verdana; font-size:small; text-align:center'>{0}</td>", ds.Tables[0].Rows[0]["vill_city2"]);
            rpt.AppendFormat("<td style='font-family:Verdana; font-size:small; text-align:center'> {0} </td>", ds.Tables[0].Rows[0]["relation"]);
            rpt.Append("</tr >");
            rpt.Append("</table>"); 
        } 
        ltrReport.Visible = true;
    }

    public void GetHearder_NeoNatalphysia1(string reg)
    {
        ltrReport.Text = "";
        DataSet ds = thepatientbill.PatientForReport(reg, Session["CoCode"].ToString().Trim());
        if (ds.Tables[0].Rows.Count > 0)
        {
            rpt.Append("<table width='100%' cellspacing=0 border=0 style='border:1px solid black;border-spacing: 0; padding: 0;font-family:verdana;'>");
            rpt.Append("<tr style='height:25px;'>");
            rpt.Append("<td style='font-family:Verdana; font-size:x-large;text-align:center'><b>Some of the critical situation of the patient about the risks and accept the Letter</b>");
            rpt.Append("</tr >");

            rpt.Append("<tr style='height:20px;'>");
            rpt.AppendFormat("<td style='font-family:Verdana; font-size:medium;text-align:center'>(Admitted By :{0}, Relation :{1})</td>", ds.Tables[0].Rows[0]["guardian_name"], ds.Tables[0].Rows[0]["relation"]);
            rpt.Append("</tr >");
            rpt.Append("</table>");

            PatientDetails_English(ds);
            rpt.Append("<br/>"); 
            
            rpt.Append("<table width='100%'>");
            rpt.Append("<tr style='height:25px'>"); rpt.Append("<u>");
            rpt.Append("<td style='font-family:Verdana;font-size:medium;font-weight:bold; text-align:left'>Problems or risk factors of the patient  : </td>"); rpt.Append("</u>");
            rpt.Append("</tr >");

            rpt.Append("<tr style='height:25px'>");
            rpt.Append("<td style='font-family:Verdana;font-size:small; text-align:left'>Brene because of lack of oxygen, brain damage, bleeding brene, inaphekasana or sepasisa, severe shortness of breath, convulsions</td>");
            rpt.Append("</tr >");

            rpt.Append("<tr style='height:25px'>"); rpt.Append("<u>");
            rpt.Append("<td style='font-family:Verdana;font-size:medium;font-weight:bold;text-align:left'>Hazard that may occur in the patient  :</td>"); rpt.Append("</u>");
            rpt.Append("</tr >");

            rpt.Append("<tr style='height:25px'>");
            rpt.Append("<td style='font-family:Verdana;font-size:small; text-align:left'> Sepasisa inaphekasana or may be, can cause jaundice, shortness of breath may be brene bleeding, sudden hrdayanra could blink, the stomach may be swollen, may be worse when the child's condition, and may even die, to take away any moment may be, there is no guarantee the treatment of this disease, the long-term survival of brain dyameja brain can not work well </td>");
            rpt.Append("</tr >");
            rpt.Append("</table>");
            rpt.Append("<br/>");
            rpt.Append("<table width='100%'>");
            rpt.Append("<tr style='height:25px'>");
            rpt.Append("<td style='font-family:Verdana;font-size:small; text-align:justify'> The organization learned from the patient about these issues and concerns . This organization has repeatedly us from any government medical college or any other health organizations have been asked to take , we suggest that these institutions have agreed along . But now the situation is like taking away patients who do not have , so we are keeping in mind the patient's life is at risk , including hostile cikitsara emergency admissions for patients who have made this institution , the patient gave permission for the necessary treatment .  </td>");
            rpt.Append("</tr >");
            rpt.Append("</table>");
            rpt.Append("<br/>");
            rpt.Append("<table width='100%'>");
            rpt.Append("<tr style='height:25px'>");
            rpt.Append("<td style=' font-family:Verdana;font-size:small; text-align:justify'> We think it's healthy to talk with our kindred allowed to treat the patient during treatment, or in this case, the organization would be without the complicated error occurred when the patient or any other life-threatening if not, we'll blame the institution. </td>");
            rpt.Append("</tr >");
            rpt.Append("</table>");


            rpt.Append("<table width='100%'>");
            rpt.Append("<tr style='height:27px'>");
            rpt.Append("<td style='font-family:Verdana; font-size:medium; text-align:center'> Signature </td>");
            rpt.Append("<td style='font-family:Verdana; font-size:medium; text-align:center'> Full Name  </td>");
            rpt.Append("<td style='font-family:Verdana; font-size:medium; text-align:center'> Address </td>");
            rpt.Append("<td style='font-family:Verdana; font-size:medium; text-align:center'> Relation  </td>");
            rpt.Append("</tr >");

            rpt.Append("<tr style='height:27px'>");
            rpt.Append("<td style='font-family:Verdana;  font-size:small; text-align:center'>..................</td>");
            rpt.AppendFormat("<td style='font-family:Verdana;  font-size:small; text-align:center'>{0}</td>", ds.Tables[0].Rows[0]["patient_name"]);
            rpt.AppendFormat("<td style='font-family:Verdana; font-size:small; text-align:center'>{0}</td>", ds.Tables[0].Rows[0]["vill_city"]);
            rpt.Append("<td style='font-family:Verdana; font-size:small; text-align:center'> Self </td>");
            rpt.Append("</tr >");

            rpt.Append("<tr style='height:27px'>");
            rpt.Append("<td style='font-family:Verdana;  font-size:small; text-align:center'>..................</td>");
            rpt.AppendFormat("<td style='font-family:Verdana;  font-size:small; text-align:center'>{0}</td>", ds.Tables[0].Rows[0]["guardian_name"]);
            rpt.AppendFormat("<td style='font-family:Verdana; font-size:small; text-align:center'>{0}</td>", ds.Tables[0].Rows[0]["vill_city2"]);
            rpt.AppendFormat("<td style='font-family:Verdana; font-size:small; text-align:center'> {0} </td>", ds.Tables[0].Rows[0]["relation"]);
            rpt.Append("</tr >");
            rpt.Append("</table>"); 
        } 
        ltrReport.Visible = true;
    }

    public void GetHearder_OperationConsentReport(string reg)
    {
        ltrReport.Text = "";
        DataSet ds = thepatientbill.PatientForReport(reg, Session["CoCode"].ToString().Trim());
        if (ds.Tables[0].Rows.Count > 0)
        {

            rpt.Append("<table width='100%' cellspacing=0 border=0 style='border:1px solid black;border-spacing: 0; padding: 0;font-family:verdana;'>");
            rpt.Append("<tr style='height:25px;'>");
            rpt.Append("<td style='font-family:Verdana; font-size:x-large;text-align:center'><b>অপারেশন,অজ্ঞান করার পদ্ধতি,বিশেষ চিকিৎসা বা প্রক্রিয়ার সম্মতি পত্র</b>");
            rpt.Append("</tr >");

            rpt.Append("<tr style='height:20px;'>");
            rpt.AppendFormat("<td style='font-family:Verdana; font-size:medium;text-align:center'>(যিনি ভর্তি করাচ্ছেন তার নাম:{0},সম্পর্ক:{1})</td>", ds.Tables[0].Rows[0]["guardian_name"], ds.Tables[0].Rows[0]["relation"]);
            rpt.Append("</tr >");
            rpt.Append("</table>");

            rpt.Append("<br/>"); 
 
            rpt.Append("<table width='100%'>");
            rpt.Append("<tr style='height:30px'>");
            rpt.Append("<td style='width: 5%;Height:70px;  font-family:Verdana;font-size:medium; text-align:justify'> আমি / আমার___________________চিকিৎসা, অপারেশন পদ্ধতিগুলি সংক্রান্ত আমার সমস্ত প্রশ্নের যথাযথ এবং সন্তোষজনক উত্তর এই প্রতিষ্ঠানের সংশ্লিষ্ট চিকিৎসকের কাছ থেকে পেয়ে আমার উপসর্গ গুলির যথাযথ চিকিৎসা/অপারেশন/রোগ নিরুপণ সংক্রান্ত পদ্ধতিগুলি সমাপনের নির্দেশ দিলাম । উক্ত চিকিৎসা অপারেশন, অজ্ঞান পদ্ধতি ও প্রক্রিয়াগুলি এবং সম্ভাব্য চিকিৎসা পদ্ধতিগুলির সঙ্গে সংশ্লিষ্ট ঝুঁকি , ঝামেলা জটিলতা ও পরিণামগুলি সম্পর্কে আমি অবহিত হয়েছি এবং জেনেছি এই অপারেশন বা চিকিৎসা চলাকালীন এমন কিছু অদৃষ্টপূর্ব অবস্থা ঘটতে পারে যাতে পূর্ব-নির্দ্দিষ্ট প্রক্রিয়া ছাড়াও অন্যান্য বা মূল পদ্ধতির পরিবর্জনের প্রয়োজন হতে পারে । উক্ত চিকিৎসক বা শল্যচিকিৎসক বা সহকর্মীগণকে যেন তাঁদের পেশাগত বিবেচনার বলে প্রয়োজনীয় চিকিৎসা ও শল্যপদ্ধতি গ্রহণ করেন , সে বিষয়ে আমার সম্মতি ও অনুরোধ রইল । যে কোনও চিকিৎসা বা শল্যচিকিৎসার ক্ষেত্রে প্রচুর রক্তক্ষরণ ,বীজদূষণ , হৃৎপিণ্ড অচলতা ইত্যাদি যে সমস্ত ঝুঁকি থাকে , সেগুলি এক্ষেত্রেও এসে যেতে পারে তাও আমাকে জানানো হয়েছে এবং আমার শারীরিক অসুস্থতা পুরোপুরি নিরাময়ের কোনও গ্যারেন্টি বা প্রতিশ্রুতি আমাকে দেওয়া হয়নি । তাই আমার চিকিৎসা চলাকালীন কোনও জটিলতা হলে আমি অযথা ডাক্তারবাবু ও তাঁর সহকর্মীগণকে কিম্বা নার্সিং হোম/হাসপাতালকে দায়ী করব না ।</td>");

            rpt.Append("</tr >");
            rpt.Append("</table>");



            rpt.Append("<table width='100%' cellspacing=0 border=0 style='padding: 0;font-family:verdana;'>");
            rpt.Append("<tr style='height:30px'>");
            rpt.Append("<td style='width: 5%;Height:70px;  font-family:Verdana;font-size:medium; text-align:justify'>আমি সম্মতি দিচ্ছি যে, এই পরিষেবায় প্রয়োজনীয় ও উপযুক্ত যে কোনও ওষুধের সাহায্যে অজ্ঞানতা প্রয়োগ করা যেতে পারে । আমি মনে করি যে, অজ্ঞানতা প্রয়োগের ফলে জটিলতা দেখা দিতে পারে এবং অজ্ঞান পদ্ধতি ও অপারেশন চলাকালীন মস্তিষ্কের এবং দাঁতের ক্ষতি হতে পারে । </td>");

            rpt.Append("</tr >");
            rpt.Append("</table>");


            rpt.Append("<table width='100%' cellspacing=0 border=0 style='padding: 0;font-family:verdana;'>");
            rpt.Append("<tr style='height:30px'>");
            rpt.Append("<td style='width: 5%;Height:70px;  font-family:Verdana; font-size:medium; text-align:justify'>বিচ্ছিন্ন শারীরিক কোনও অঙ্গ বা যন্ত্রাংশ যাতে নার্সিং হোম/হাসপাতাল কর্তৃপক্ষ তাঁদের প্রচলিত পদ্ধতি অনুসারে অপসরণ করতে পারেন, সে সম্পর্কে আমি সম্মতি দিলাম । </td>");

            rpt.Append("</tr >");
            rpt.Append("</table>");


            rpt.Append("<table width='100%' cellspacing=0 border=0 style='padding: 0;font-family:verdana;'>");
            rpt.Append("<tr style='height:30px'>");
            rpt.Append("<td style='width: 5%;Height:70px;  font-family:Verdana;font-size:medium; text-align:justify'>ল্যাপারোস্কোপি বা হিস্টোরোস্কোপি বা দূরবীন শল্যচিকিৎসা বা মাইক্রো সার্জারি চলা কালীন পদ্ধতিগত জটিলতা দেখা দিলে --প্রথাগত ভাবে শল্যবিদ্যা অনুসরণ করার অর্থাৎ পেট কেটে শল্য চিকিৎসা করার অনুমতি দিলাম । ওই পদ্ধতিতে শল্যচিকিৎসার ক্ষেত্রে হৃৎপিণ্ড অচলাবস্থার মতো মারাত্বক জটিলতা দেখা দিতে পারে তাও জানলাম ।</td>");
            rpt.Append("</tr >");
            rpt.Append("</table>");

            rpt.Append("<table width='100%' cellspacing=0 border=0 style='padding: 0;font-family:verdana;'>");
            rpt.Append("<tr style='height:30px'>");
            rpt.Append("<td style='width: 5%;Height:70px;  font-family:Verdana;font-size:medium; text-align:justify'>আমি OT-তে থাকাকালীন চিকিৎসকের প্রয়োজনে বা নির্দেশে অন্যান্য পর্যবেক্ষকদের OT-তে প্রবেশ করার অনুমতি দেওয়ার পাশাপাশি শিক্ষা কিম্বা গবেষণার উদ্দেশ্যে অপারেশনের প্রক্রিয়া এবং আমার শরীর/দেহাংশের আলোকচিত্র গ্রহণ এবং আমার পরিচয় গোপন রেখে ওই সমস্ত আলোকচিত্র তৎক্ষণাৎ কিম্বা ভবিষ্যতে সম্প্রসারণ করার সম্মতি দিলাম । </td>");
            rpt.Append("</tr >");
            rpt.Append("</table>");

            rpt.Append("<table width='100%' cellspacing=0 border=0 style='padding: 0;font-family:verdana;'>");
            rpt.Append("<tr style='height:30px'>");
            rpt.Append("<td style='width: 5%;Height:70px;  font-family:Verdana;font-size:medium; text-align:justify'>কোনও জটিলতার ক্ষেত্রে অন্য কোনও ডাক্তারবাবুর সাহায্য নেওয়া হতে পারে এবং ওই ডা:বাবুর চিকিৎসায় আমার সম্মতি থাকল। জরুরি পরিস্থিতির খাতিরে,সংশ্লিষ্ট বিশেষ দরকারি সাজসজ্জাযুক্ত রোগী স্থানান্তরের ব্যবস্থা করতে পারেন । ওই ধরনের পরিস্থিতির উদ্ভব হলে আমরা প্রতিষ্ঠান কিম্বা ডাক্তারবাবুকে দায়ী করব না । প্রয়োজনীয় মেডিক্যাল, প্যাথলজিক্যাল, রেডিওলজিক্যাল, সাইটোলজিক্যাল এবং অন্যান্য অনুরূপ রোগ নির্ণয় পদ্ধতির রির্পোটের ভিত্তিতেই চিকিৎসা ও শল্য প্রযুক্তি কৌশল রচনা করেছেন সংশ্লিষ্ট ডাক্তারবাবু পরবর্তীকালে উক্ত রির্পোট জনিত চিকিৎসা ভ্রমের জন্য ডাক্তারবাবু কোনও মতেই দায়বদ্ধ থাকবেন না। আমাকে যে সমস্ত ওষুধপত্র প্রয়োগ করা হবে বা পরে ব্যবহারের জন্য নির্দেশ দেওয়া হবে, সেগুলি স্বপ্লমেয়াদী বা দীর্ঘমেয়াদী প্রতিক্রিয়া থাকতে পারে, কোনও কোনও ক্ষেত্রে এই বিরূপ প্রতিক্রিয়া মারাত্বক হতে পারে --কিন্তু আমরা জেনেছি, একজন ডাক্তারবাবু এর জন্য কোনও মতেই দায়ী থাকতে পারে না । কারোর প্ররোচনা ছাডা আমরা স্বেচ্ছায় ও সুস্থ মনে সমস্থ কিছুর মর্ম বুঝে এই অঙ্গীকার পত্রে সই করলাম। </td>");
            rpt.Append("</tr >");
            rpt.Append("</table>");

            rpt.Append("<table width='100%' cellspacing=0 border=0 style='padding: 0;font-family:verdana;'>");
            rpt.Append("<tr style='height:30px'>");
            rpt.Append("<td style='width: 5%;Height:70px;  font-family:Verdana; font-size:medium; text-align:center'> স্বাক্ষর/টিপ </td>");
            rpt.Append("<td style='width: 5%;Height:70px;  font-family:Verdana; font-size:medium; text-align:center'> পুরো  নাম  </td>");
            rpt.Append("<td style='width: 5%;Height:70px;  font-family:Verdana; font-size:medium; text-align:center'> ঠিকানা </td>");
            rpt.Append("<td style='width: 5%;Height:70px;  font-family:Verdana; font-size:medium; text-align:center'> সম্পর্ক  </td>");
            rpt.Append("</tr >");

            rpt.Append("<tr style='height:30px'>");
            rpt.Append("<td style='width: 5%;Height:70px;  font-family:Verdana;  font-size:medium; text-align:center'>..................</td>");
            rpt.AppendFormat("<td style='width: 5%;Height:70px;  font-family:Verdana;  font-size:medium; text-align:center'>{0}</td>", ds.Tables[0].Rows[0]["patient_name"]);
            rpt.AppendFormat("<td style='width: 5%;Height:70px;  font-family:Verdana; font-size:medium; text-align:center'>{0}</td>", ds.Tables[0].Rows[0]["vill_city"]);
            rpt.Append("<td style='width: 5%;Height:70px;  font-family:Verdana; font-size:medium; text-align:center'> নিজে </td>");
            rpt.Append("</tr >");

            rpt.Append("<tr style='height:30px'>");
            rpt.Append("<td style='width: 5%;Height:70px;  font-family:Verdana;  font-size:medium; text-align:center'>..................</td>");
            rpt.AppendFormat("<td style='width: 5%;Height:70px;  font-family:Verdana;  font-size:medium; text-align:center'>{0}</td>", ds.Tables[0].Rows[0]["guardian_name"]);
            rpt.AppendFormat("<td style='width: 5%;Height:70px;  font-family:Verdana; font-size:medium; text-align:center'>{0}</td>", ds.Tables[0].Rows[0]["vill_city2"]);
            rpt.AppendFormat("<td style='width: 5%;Height:70px;  font-family:Verdana; font-size:medium; text-align:center'> {0} </td>", ds.Tables[0].Rows[0]["relation"]);
            rpt.Append("</tr >");
            rpt.Append("</table>"); 
        } 
        ltrReport.Visible = true;
    }

    public void GetHearder_OperationConsentReport1(string reg)
    {
        ltrReport.Text = "";
        DataSet ds = thepatientbill.PatientForReport(reg, Session["CoCode"].ToString().Trim());
        if (ds.Tables[0].Rows.Count > 0)
        {
            rpt.Append("<table width='100%' cellspacing=0 border=0 style='border:1px solid black;border-spacing: 0; padding: 0;font-family:verdana;'>");
            rpt.Append("<tr style='height:25px;'>");
            rpt.Append("<td style='font-family:Verdana; font-size:x-large;text-align:center'><b>Some of the critical situation of the patient about the risks and accept the Letter</b>");
            rpt.Append("</tr >");

            rpt.Append("<tr style='height:20px;'>");
            rpt.AppendFormat("<td style='font-family:Verdana; font-size:medium;text-align:center'>(Admitted By :{0}, Relation :{1})</td>", ds.Tables[0].Rows[0]["guardian_name"], ds.Tables[0].Rows[0]["relation"]);
            rpt.Append("</tr >");
            rpt.Append("</table>");

            PatientDetails_English(ds);
            rpt.Append("<br/>"); 

            rpt.Append("<table width='100%'>");
            rpt.Append("<tr style='height:30px'>");
            rpt.Append("<td style='width: 5%;Height:70px;  font-family:Verdana;font-size:medium; text-align:justify'>I / my _____________ treatment , appropriate and satisfactory answers to all of my questions regarding the method of operation of this organization to handle my symptoms associated with getting appropriate medical care from a doctor / operations / methods having the disease given point of termination .The medical operations , procedures and processes of the unconscious and the potential risks associated with medical procedures , complications and consequences of the trouble I 've been informed and know some of these operations or unforeseen conditions that may occur during treatment so that the East - in particular the needs of the other , or the procedure to be paribarjanera could . Professional discretion of the physician or surgeon or the necessary medical care they need and salyapaddhati sahakarmiganake took , she proceeded to ask me about the willingness and .In the case of any medical or surgery , bleeding , sepsis , immobility , etc. that are at risk of heart , they also informed me that the case may be , and have been completely cured of my physical illness, no guarantees or promises were given to me . During my treatment without any complications so I daktarababu and his sahakarmiganake or nursing home / hospital will not be responsible .</td>");

            rpt.Append("</tr >");
            rpt.Append("</table>");

            rpt.Append("<table width='100%' cellspacing=0 border=0 style='padding: 0;font-family:verdana;'>");
            rpt.Append("<tr style='height:30px'>");
            rpt.Append("<td style='width: 5%;Height:70px;  font-family:Verdana;font-size:medium; text-align:justify'>I would agree that it is necessary and appropriate that these services can be applied to any drug of unconsciousness. I think that, implementation of ignorance and unconscious procedures and operations as a result of complications during a brain and can lead to loss of teeth. </td>");

            rpt.Append("</tr >");
            rpt.Append("</table>");


            rpt.Append("<table width='100%' cellspacing=0 border=0 style='padding: 0;font-family:verdana;'>");
            rpt.Append("<tr style='height:30px'>");
            rpt.Append("<td style='width: 5%;Height:70px;  font-family:Verdana; font-size:medium; text-align:justify'>Isolated body parts so that no organ or nursing home / hospital authorities can withdraw them in accordance with conventional methods, she stuck with me and I agreed.</td>");

            rpt.Append("</tr >");
            rpt.Append("</table>");


            rpt.Append("<table width='100%' cellspacing=0 border=0 style='padding: 0;font-family:verdana;'>");
            rpt.Append("<tr style='height:30px'>");
            rpt.Append("<td style='width: 5%;Height:70px;  font-family:Verdana;font-size:medium; text-align:justify'>Lyaparoskopi or historoskopi or telescope or micro surgery During surgery systemic complications arise - the traditional way to follow surgery to allow the surgical cut made ​​in the stomach. Stagnating in the heart of the serious complications of the surgery can also do.</td>");
            rpt.Append("</tr >");
            rpt.Append("</table>");

            rpt.Append("<table width='100%' cellspacing=0 border=0 style='padding: 0;font-family:verdana;'>");
            rpt.Append("<tr style='height:30px'>");
            rpt.Append("<td style='width: 5%;Height:70px;  font-family:Verdana;font-size:medium; text-align:justify'>While I was on OT-OT-observers on medical necessity or the other educational or research purposes as well as to allow access to the process of operation and my body / part of the body, taking the photograph and the photograph of me revealing the future to expand or accepted immediately.</td>");
            rpt.Append("</tr >");
            rpt.Append("</table>");

            rpt.Append("<table width='100%' cellspacing=0 border=0 style='padding: 0;font-family:verdana;'>");
            rpt.Append("<tr style='height:30px'>");
            rpt.Append("<td style='width: 5%;Height:70px;  font-family:Verdana;font-size:medium; text-align:justify'>In case of any problems , and the other can be taken to help daktarababura Dr. Babu treatment began my consent . For emergency situations , associated particularly useful to transfer the patient to the decor . If such a situation that we will not be responsible institutions or daktarababuke . Medically necessary , pyathalajikyala , rediolajikyala , saitolajikyala and other similar methods of diagnosis based on the medical and surgical technology techniques rirpotera Related daktarababu subsequently wrote an account of the rirpota induced daktarababu not be responsible for medical oversight . That would be me after all the medicines should be given to the use of , or the long-term effects they may have sbaplameyadi , in some cases, these adverse reactions may be serious - but as we know , no one stays for a daktarababu can not be held liable . Provoking someone to quit voluntarily and healthy mind , we all understand the meaning of these things, I signed the letter of commitment . </td>");
            rpt.Append("</tr >");
            rpt.Append("</table>");

            rpt.Append("<table width='100%' cellspacing=0 border=0 style='padding: 0;font-family:verdana;'>");
            rpt.Append("<tr style='height:30px'>");
            rpt.Append("<td style='width: 5%;Height:70px;  font-family:Verdana; font-size:medium; text-align:center'> Signature </td>");
            rpt.Append("<td style='width: 5%;Height:70px;  font-family:Verdana; font-size:medium; text-align:center'> Full Name  </td>");
            rpt.Append("<td style='width: 5%;Height:70px;  font-family:Verdana; font-size:medium; text-align:center'> Address </td>");
            rpt.Append("<td style='width: 5%;Height:70px;  font-family:Verdana; font-size:medium; text-align:center'> Relation  </td>");
            rpt.Append("</tr >");

            rpt.Append("<tr style='height:30px'>");
            rpt.Append("<td style='width: 5%;Height:70px;  font-family:Verdana;  font-size:medium; text-align:center'>..................</td>");
            rpt.AppendFormat("<td style='width: 5%;Height:70px;  font-family:Verdana;  font-size:medium; text-align:center'>{0}</td>", ds.Tables[0].Rows[0]["patient_name"]);
            rpt.AppendFormat("<td style='width: 5%;Height:70px;  font-family:Verdana; font-size:medium; text-align:center'>{0}</td>", ds.Tables[0].Rows[0]["vill_city"]);
            rpt.Append("<td style='width: 5%;Height:70px;  font-family:Verdana; font-size:medium; text-align:center'> Self </td>");
            rpt.Append("</tr >");

            rpt.Append("<tr style='height:30px'>");
            rpt.Append("<td style='width: 5%;Height:70px;  font-family:Verdana;  font-size:medium; text-align:center'>..................</td>");
            rpt.AppendFormat("<td style='width: 5%;Height:70px;  font-family:Verdana;  font-size:medium; text-align:center'>{0}</td>", ds.Tables[0].Rows[0]["guardian_name"]);
            rpt.AppendFormat("<td style='width: 5%;Height:70px;  font-family:Verdana; font-size:medium; text-align:center'>{0}</td>", ds.Tables[0].Rows[0]["vill_city2"]);
            rpt.AppendFormat("<td style='width: 5%;Height:70px;  font-family:Verdana; font-size:medium; text-align:center'> {0} </td>", ds.Tables[0].Rows[0]["relation"]);
            rpt.Append("</tr >");
            rpt.Append("</table>"); 
        } 
        ltrReport.Visible = true;
    }

    public void GetHearder_OperativeriskOldman(string reg)
    {
        ltrReport.Text = "";
        DataSet ds = thepatientbill.PatientForReport(reg, Session["CoCode"].ToString().Trim());
        if (ds.Tables[0].Rows.Count > 0)
        { rpt.Append("<table width='100%' cellspacing=0 border=0 style='border:1px solid black;border-spacing: 0; padding: 0;font-family:verdana;'>");
            rpt.Append("<tr style='height:25px;'>");
            rpt.Append("<td style='font-family:Verdana; font-size:x-large;text-align:center'><b>রোগীর বিশেষ কয়েকটি আশঙ্কাজনক পরিস্থিতি ও তার ঝুঁকি সম্পর্কে স্বীকার পত্র</b>");
            rpt.Append("</tr >");

            rpt.Append("<tr style='height:20px;'>");
            rpt.AppendFormat("<td style='font-family:Verdana; font-size:medium;text-align:center'>(যিনি ভর্তি করাচ্ছেন তার নাম:{0},সম্পর্ক:{1})</td>", ds.Tables[0].Rows[0]["guardian_name"], ds.Tables[0].Rows[0]["relation"]);
            rpt.Append("</tr >");
            rpt.Append("</table>");
            PatientDetails_Bengali(ds);
            rpt.Append("<br/>");  

            rpt.Append("<table width='100%'>");
            rpt.Append("<tr style='height:25px'>"); rpt.Append("<u>");
            rpt.Append("<td style='font-family:Verdana;font-size:medium;font-weight:bold; text-align:left'>রোগীর সমস্যা বা ঝুঁকির কারণ সমূহ: </td>"); rpt.Append("</u>");
            rpt.Append("</tr >");

            rpt.Append("<tr style='height:35px'>");
            rpt.Append("<td style='font-family:Verdana;font-size:medium; text-align:left'>অনিয়মিত হৃদস্পন্দন, অনিয়ন্ত্রিত ব্লাড প্রেসার, সুগারের ও বেশী বয়সের সমস্যা, জরায়ুতে দুরারোগ্য ঘা, মানসিক সমস্যা, অসহিষ্ণু রোগী।</td>");
            rpt.Append("</tr >");

            rpt.Append("<tr style='height:35px'>"); rpt.Append("<u>");
            rpt.Append("<td style='font-family:Verdana;font-size:medium;font-weight:bold;text-align:left'>ওই সমস্ত রোগীর যে সমস্ত বিপত্তি ঘটতে পারে :</td>"); rpt.Append("</u>");
            rpt.Append("</tr >");

            rpt.Append("<tr style='height:35px'>");
            rpt.Append("<td style='font-family:Verdana;font-size:medium; text-align:left'> আর জ্ঞান না ফিরতে পারে, প্যারালেসিস হতে পারে, শ্বাসকষ্ট হতে পারে, ব্রেনে রক্তপাত হতে পারে, হঠাৎ হৃদযন্র বিকল হতে পারে, পেট ফুলে যেতে পারে, ইনফেকসন বা সেপশিস হতে পারে, অপারেশন সম্পুর্ণ করা সম্ভব না হতে পারে, যখন তখন রোগীর অবস্থা আরও খারাপ হতে পারে, এমন কি মারা ও যেতে পারে, যে কোন মুহূর্তে অন্যত্র (যদি সম্ভব হয়) নিয়ে যেতে হতে পারে, এই চিকিৎসার কোন গ্যারান্টী নেই। </td>");
            rpt.Append("</tr >");
            rpt.Append("</table>");
            rpt.Append("<br/>");
            rpt.Append("<table width='100%'>");
            rpt.Append("<tr style='height:30px'>");
            rpt.Append("<td style='font-family:Verdana;font-size:medium; text-align:justify'>এই প্রতিষ্ঠানের পক্ষ থেকে রোগীর এই সমস্ত সমস্যা ও আশঙ্কার কথা জানলাম । এই প্রতিষ্ঠানের পক্ষ থেকে আমাদের বারবার কোন সরকারী মেডিকেল কলেজ কিংবা অন্য কোনও স্বাস্থ্য প্রতিষ্ঠানের নিয়ে যাবার কথা বলা হয়েছে , আমরাও এই প্রতিষ্ঠান যে সমস্ত পরামর্শ দিয়েছে তারসঙ্গে একমত । কিন্তু এই মূহুর্তে রোগীকে অন্যত্র নিয়ে যাবার মতো পরিস্থিতি আমাদের নেই , তাই রোগীর জীবন হানী সহ সমস্ত ঝুঁকির কথা মাথায় রেখেই আমরা আপদকালীন চিকিৎ্সার জন্য রোগীকে এই প্রতিষ্ঠানে ভর্তি করলাম , এখানে রোগীর প্রয়োজনীয় যাবতীয় চিকিত্সা করার জন্য অনুমতি দিলাম । </td>");
            rpt.Append("</tr >");
            rpt.Append("</table>");
            rpt.Append("<br/>");
            rpt.Append("<table width='100%'>");
            rpt.Append("<tr style='height:30px'>");
            rpt.Append("<td style=' font-family:Verdana;font-size:medium; text-align:justify'>আমরা যেহেতু সুস্থ মনে আমাদের আত্মীয় পরিজনদের সঙ্গে আলোচনা করে রোগীর চিকিত্সা চালানোর অনুমতি দিচ্ছি সেক্ষেত্রে চিকিত্সা চলাকালীন কিংবা এই প্রতিষ্ঠান থেকে ছাড়া পাওয়ার পর রোগীর অন্য কোন জটিল সম্যসা হলে কিংবা জীবন সংশয় হলে আমরা এই প্রতিষ্ঠানকে দায়ী করব না ।</td>");
            rpt.Append("</tr >");
            rpt.Append("</table>");

            rpt.Append("<table width='100%'>");
            rpt.Append("<tr style='height:35px'>");
            rpt.Append("<td style='font-family:Verdana; font-size:medium; text-align:center'> স্বাক্ষর/টিপ </td>");
            rpt.Append("<td style='font-family:Verdana; font-size:medium; text-align:center'> পুরো  নাম  </td>");
            rpt.Append("<td style='font-family:Verdana; font-size:medium; text-align:center'> ঠিকানা </td>");
            rpt.Append("<td style='font-family:Verdana; font-size:medium; text-align:center'> সম্পর্ক  </td>");
            rpt.Append("</tr >");

            rpt.Append("<tr style='height:27px'>");
            rpt.Append("<td style='font-family:Verdana;  font-size:small; text-align:center'>..................</td>");
            rpt.AppendFormat("<td style='font-family:Verdana;  font-size:small; text-align:center'>{0}</td>", ds.Tables[0].Rows[0]["patient_name"]);
            rpt.AppendFormat("<td style='font-family:Verdana; font-size:small; text-align:center'>{0}</td>", ds.Tables[0].Rows[0]["vill_city"]);
            rpt.Append("<td style='font-family:Verdana; font-size:small; text-align:center'> নিজে </td>");
            rpt.Append("</tr >");

            rpt.Append("<tr style='height:27px'>");
            rpt.Append("<td style='font-family:Verdana;  font-size:small; text-align:center'>..................</td>");
            rpt.AppendFormat("<td style='font-family:Verdana;  font-size:small; text-align:center'>{0}</td>", ds.Tables[0].Rows[0]["guardian_name"]);
            rpt.AppendFormat("<td style='font-family:Verdana; font-size:small; text-align:center'>{0}</td>", ds.Tables[0].Rows[0]["vill_city2"]);
            rpt.AppendFormat("<td style='font-family:Verdana; font-size:small; text-align:center'> {0} </td>", ds.Tables[0].Rows[0]["relation"]);
            rpt.Append("</tr >");
            rpt.Append("</table>"); 
        }
        rpt.Append("</div>");
        ltrReport.Visible = true;
    }

    public void GetHearder_OperativeriskOldman1(string reg)
    {
        ltrReport.Text = "";
        DataSet ds = thepatientbill.PatientForReport(reg, Session["CoCode"].ToString().Trim());
        if (ds.Tables[0].Rows.Count > 0)
        {
            rpt.Append("<table width='100%' cellspacing=0 border=0 style='border:1px solid black;border-spacing: 0; padding: 0;font-family:verdana;'>");
            rpt.Append("<tr style='height:25px;'>");
            rpt.Append("<td style='font-family:Verdana; font-size:x-large;text-align:center'><b>Some of the critical situation of the patient about the risks and accept the Letter</b>");
            rpt.Append("</tr >");

            rpt.Append("<tr style='height:20px;'>");
            rpt.AppendFormat("<td style='font-family:Verdana; font-size:medium;text-align:center'>(Admitted By :{0}, Relation :{1})</td>", ds.Tables[0].Rows[0]["guardian_name"], ds.Tables[0].Rows[0]["relation"]);
            rpt.Append("</tr >");
            rpt.Append("</table>");

            PatientDetails_English(ds);
            rpt.Append("<br/>"); 


            rpt.Append("<table width='100%'>");
            rpt.Append("<tr style='height:25px'>"); rpt.Append("<u>");
            rpt.Append("<td style='font-family:Verdana;font-size:medium;font-weight:bold; text-align:left'>Problems or risk factors of the patient  : </td>"); rpt.Append("</u>");
            rpt.Append("</tr >");

            rpt.Append("<tr style='height:25px'>");
            rpt.Append("<td style='font-family:Verdana;font-size:small; text-align:left'> Irregular heartbeat, uncontrolled high blood pressure, sugar and more age problems, uterus, chronic sores, psychological problems, patients intolerant. </td>");
            rpt.Append("</tr >");

            rpt.Append("<tr style='height:25px'>"); rpt.Append("<u>");
            rpt.Append("<td style='font-family:Verdana;font-size:medium;font-weight:bold;text-align:left'>Hazard that may occur in the patient  :</td>"); rpt.Append("</u>");
            rpt.Append("</tr >");

            rpt.Append("<tr style='height:25px'>");
            rpt.Append("<td style='font-family:Verdana;font-size:small; text-align:left'>Returns to the knowledge, may be pyaralesisa, shortness of breath may be brene bleeding, sudden hrdayanra can crash, the stomach may be swollen, sepasisa inaphekasana or may not be possible to complete the operation, when the patient's condition worse may be, and may even die, that moment elsewhere (if possible) may be taken, there is no guarantee this treatment. </td>");
            rpt.Append("</tr >");
            rpt.Append("</table>");
            rpt.Append("<br/>");
            rpt.Append("<table width='100%'>");
            rpt.Append("<tr style='height:25px'>");
            rpt.Append("<td style='font-family:Verdana;font-size:small; text-align:justify'> The organization learned from the patient about these issues and concerns . This organization has repeatedly us from any government medical college or any other health organizations have been asked to take , we suggest that these institutions have agreed along . But now the situation is like taking away patients who do not have , so we are keeping in mind the patient's life is at risk , including hostile cikitsara emergency admissions for patients who have made this institution , the patient gave permission for the necessary treatment .  </td>");
            rpt.Append("</tr >");
            rpt.Append("</table>");
            rpt.Append("<br/>");
            rpt.Append("<table width='100%'>");
            rpt.Append("<tr style='height:25px'>");
            rpt.Append("<td style=' font-family:Verdana;font-size:small; text-align:justify'> We think it's healthy to talk with our kindred allowed to treat the patient during treatment, or in this case, the organization would be without the complicated error occurred when the patient or any other life-threatening if not, we'll blame the institution. </td>");
            rpt.Append("</tr >");
            rpt.Append("</table>");


            rpt.Append("<table width='100%'>");
            rpt.Append("<tr style='height:27px'>");
            rpt.Append("<td style='font-family:Verdana; font-size:medium; text-align:center'> Signature </td>");
            rpt.Append("<td style='font-family:Verdana; font-size:medium; text-align:center'> Full Name  </td>");
            rpt.Append("<td style='font-family:Verdana; font-size:medium; text-align:center'> Address </td>");
            rpt.Append("<td style='font-family:Verdana; font-size:medium; text-align:center'> Relation  </td>");
            rpt.Append("</tr >");

            rpt.Append("<tr style='height:27px'>");
            rpt.Append("<td style='font-family:Verdana;  font-size:small; text-align:center'>..................</td>");
            rpt.AppendFormat("<td style='font-family:Verdana;  font-size:small; text-align:center'>{0}</td>", ds.Tables[0].Rows[0]["patient_name"]);
            rpt.AppendFormat("<td style='font-family:Verdana; font-size:small; text-align:center'>{0}</td>", ds.Tables[0].Rows[0]["vill_city"]);
            rpt.Append("<td style='font-family:Verdana; font-size:small; text-align:center'> Self </td>");
            rpt.Append("</tr >");

            rpt.Append("<tr style='height:27px'>");
            rpt.Append("<td style='font-family:Verdana;  font-size:small; text-align:center'>..................</td>");
            rpt.AppendFormat("<td style='font-family:Verdana;  font-size:small; text-align:center'>{0}</td>", ds.Tables[0].Rows[0]["guardian_name"]);
            rpt.AppendFormat("<td style='font-family:Verdana; font-size:small; text-align:center'>{0}</td>", ds.Tables[0].Rows[0]["vill_city2"]);
            rpt.AppendFormat("<td style='font-family:Verdana; font-size:small; text-align:center'> {0} </td>", ds.Tables[0].Rows[0]["relation"]);
            rpt.Append("</tr >");
            rpt.Append("</table>"); 
        } 
        ltrReport.Visible = true;
    }

    public void GetHearder_PIHReport(string reg)
    {
        ltrReport.Text = "";
        DataSet ds = thepatientbill.PatientForReport(reg, Session["CoCode"].ToString().Trim());
        if (ds.Tables[0].Rows.Count > 0)
        {
            rpt.Append("<table width='100%' cellspacing=0 border=0 style='border:1px solid black;border-spacing: 0; padding: 0;font-family:verdana;'>");
            rpt.Append("<tr style='height:25px;'>");
            rpt.Append("<td style='font-family:Verdana; font-size:x-large;text-align:center'><b>রোগীর বিশেষ কয়েকটি আশঙ্কাজনক পরিস্থিতি ও তার ঝুঁকি সম্পর্কে স্বীকার পত্র</b>");
            rpt.Append("</tr >");

            rpt.Append("<tr style='height:20px;'>");
            rpt.AppendFormat("<td style='font-family:Verdana; font-size:medium;text-align:center'>(যিনি ভর্তি করাচ্ছেন তার নাম:{0},সম্পর্ক:{1})</td>", ds.Tables[0].Rows[0]["guardian_name"], ds.Tables[0].Rows[0]["relation"]);
            rpt.Append("</tr >");
            rpt.Append("</table>");
            PatientDetails_Bengali(ds);
            rpt.Append("<br/>");  

            rpt.Append("<table width='100%'>");
            rpt.Append("<tr style='height:25px'>"); rpt.Append("<u>");
            rpt.Append("<td style='font-family:Verdana;font-size:medium;font-weight:bold; text-align:left'>রোগীর সমস্যা বা ঝুঁকির কারণ সমূহ: </td>"); rpt.Append("</u>");
            rpt.Append("</tr >");

            rpt.Append("<tr style='height:35px'>");
            rpt.Append("<td style='font-family:Verdana;font-size:medium; text-align:left'>প্রেসার খুব বেশী, বাচ্চার বৃদ্ধি কম, পা ফোলা,এক্লামশিয়া বা গর্ভাকালীন খিঁচুনি হতে পারে</td>");
            rpt.Append("</tr >");

            rpt.Append("<tr style='height:35px'>"); rpt.Append("<u>");
            rpt.Append("<td style='font-family:Verdana;font-size:medium;font-weight:bold;text-align:left'>ওই সমস্ত রোগীর যে সমস্ত বিপত্তি ঘটতে পারে :</td>"); rpt.Append("</u>");
            rpt.Append("</tr >");

            rpt.Append("<tr style='height:35px'>");
            rpt.Append("<td style='font-family:Verdana;font-size:medium; text-align:left'>এক্লামশিয়া বা গর্ভাকালীন খিঁচুনি হতে পারে, অজ্ঞান অবস্থা, বেশী প্রেসার, নাক মুখ দিয়ে গেঁজা বেরুনো, এই অবস্থায় প্রসব করানোর ঝুঁকি, হঠাৎ রোগীর হৃদ স্পন্দন থেমে যেতে পারে, রোগী মারা যেতে পারে, বাচ্চাও মারা যেতে পারে, ফুসফুস কিডনি লিভার বিকল হতে পারে, যে কোন মুহূর্তে রোগীকে অন্যত্র নিয়ে যেতে হতে পারে, দীর্ঘ মেয়াদী হাই প্রেসার থেকে যেতে পারে, এই রোগের চিকিৎসার কোন গ্যারান্টী নেই</td>");
            rpt.Append("</tr >");
            rpt.Append("</table>");
            rpt.Append("<br/>");
            rpt.Append("<table width='100%'>");
            rpt.Append("<tr style='height:30px'>");
            rpt.Append("<td style='font-family:Verdana;font-size:medium; text-align:justify'>এই প্রতিষ্ঠানের পক্ষ থেকে রোগীর এই সমস্ত সমস্যা ও আশঙ্কার কথা জানলাম । এই প্রতিষ্ঠানের পক্ষ থেকে আমাদের বারবার কোন সরকারী মেডিকেল কলেজ কিংবা অন্য কোনও স্বাস্থ্য প্রতিষ্ঠানের নিয়ে যাবার কথা বলা হয়েছে , আমরাও এই প্রতিষ্ঠান যে সমস্ত পরামর্শ দিয়েছে তারসঙ্গে একমত । কিন্তু এই মূহুর্তে রোগীকে অন্যত্র নিয়ে যাবার মতো পরিস্থিতি আমাদের নেই , তাই রোগীর জীবন হানী সহ সমস্ত ঝুঁকির কথা মাথায় রেখেই আমরা আপদকালীন চিকিৎ্সার জন্য রোগীকে এই প্রতিষ্ঠানে ভর্তি করলাম , এখানে রোগীর প্রয়োজনীয় যাবতীয় চিকিত্সা করার জন্য অনুমতি দিলাম । </td>");
            rpt.Append("</tr >");
            rpt.Append("</table>");
            rpt.Append("<br/>");
            rpt.Append("<table width='100%'>");
            rpt.Append("<tr style='height:30px'>");
            rpt.Append("<td style=' font-family:Verdana;font-size:medium; text-align:justify'>আমরা যেহেতু সুস্থ মনে আমাদের আত্মীয় পরিজনদের সঙ্গে আলোচনা করে রোগীর চিকিত্সা চালানোর অনুমতি দিচ্ছি সেক্ষেত্রে চিকিত্সা চলাকালীন কিংবা এই প্রতিষ্ঠান থেকে ছাড়া পাওয়ার পর রোগীর অন্য কোন জটিল সম্যসা হলে কিংবা জীবন সংশয় হলে আমরা এই প্রতিষ্ঠানকে দায়ী করব না ।</td>");
            rpt.Append("</tr >");
            rpt.Append("</table>");

            rpt.Append("<table width='100%'>");
            rpt.Append("<tr style='height:35px'>");
            rpt.Append("<td style='font-family:Verdana; font-size:medium; text-align:center'> স্বাক্ষর/টিপ </td>");
            rpt.Append("<td style='font-family:Verdana; font-size:medium; text-align:center'> পুরো  নাম  </td>");
            rpt.Append("<td style='font-family:Verdana; font-size:medium; text-align:center'> ঠিকানা </td>");
            rpt.Append("<td style='font-family:Verdana; font-size:medium; text-align:center'> সম্পর্ক  </td>");
            rpt.Append("</tr >");

            rpt.Append("<tr style='height:27px'>");
            rpt.Append("<td style='font-family:Verdana;  font-size:small; text-align:center'>..................</td>");
            rpt.AppendFormat("<td style='font-family:Verdana;  font-size:small; text-align:center'>{0}</td>", ds.Tables[0].Rows[0]["patient_name"]);
            rpt.AppendFormat("<td style='font-family:Verdana; font-size:small; text-align:center'>{0}</td>", ds.Tables[0].Rows[0]["vill_city"]);
            rpt.Append("<td style='font-family:Verdana; font-size:small; text-align:center'> নিজে </td>");
            rpt.Append("</tr >");

            rpt.Append("<tr style='height:27px'>");
            rpt.Append("<td style='font-family:Verdana;  font-size:small; text-align:center'>..................</td>");
            rpt.AppendFormat("<td style='font-family:Verdana;  font-size:small; text-align:center'>{0}</td>", ds.Tables[0].Rows[0]["guardian_name"]);
            rpt.AppendFormat("<td style='font-family:Verdana; font-size:small; text-align:center'>{0}</td>", ds.Tables[0].Rows[0]["vill_city2"]);
            rpt.AppendFormat("<td style='font-family:Verdana; font-size:small; text-align:center'> {0} </td>", ds.Tables[0].Rows[0]["relation"]);
            rpt.Append("</tr >");
            rpt.Append("</table>"); 
        } 
        ltrReport.Visible = true;
    }

    public void GetHearder_PIHReport1(string reg)
    {
        ltrReport.Text = "";
        DataSet ds = thepatientbill.PatientForReport(reg, Session["CoCode"].ToString().Trim());
        if (ds.Tables[0].Rows.Count > 0)
        {
            rpt.Append("<table width='100%' cellspacing=0 border=0 style='border:1px solid black;border-spacing: 0; padding: 0;font-family:verdana;'>");
            rpt.Append("<tr style='height:25px;'>");
            rpt.Append("<td style='font-family:Verdana; font-size:x-large;text-align:center'><b>Some of the critical situation of the patient about the risks and accept the Letter</b>");
            rpt.Append("</tr >");

            rpt.Append("<tr style='height:20px;'>");
            rpt.AppendFormat("<td style='font-family:Verdana; font-size:medium;text-align:center'>(Admitted By :{0}, Relation :{1})</td>", ds.Tables[0].Rows[0]["guardian_name"], ds.Tables[0].Rows[0]["relation"]);
            rpt.Append("</tr >");
            rpt.Append("</table>");

            PatientDetails_English(ds);
            rpt.Append("<br/>"); 

            rpt.Append("<table width='100%'>");
            rpt.Append("<tr style='height:25px'>"); rpt.Append("<u>");
            rpt.Append("<td style='font-family:Verdana;font-size:medium;font-weight:bold; text-align:left'>Problems or risk factors of the patient  : </td>"); rpt.Append("</u>");
            rpt.Append("</tr >");

            rpt.Append("<tr style='height:25px'>");
            rpt.Append("<td style='font-family:Verdana;font-size:small; text-align:left'> Pressure too much, child growth, leg swelling, convulsions garbhakalina eklamasiya or may be </td>");
            rpt.Append("</tr >");

            rpt.Append("<tr style='height:25px'>"); rpt.Append("<u>");
            rpt.Append("<td style='font-family:Verdana;font-size:medium;font-weight:bold;text-align:left'>Hazard that may occur in the patient  :</td>"); rpt.Append("</u>");
            rpt.Append("</tr >");

            rpt.Append("<tr style='height:25px'>");
            rpt.Append("<td style='font-family:Verdana;font-size:small; text-align:left'> Eklamasiya or convulsions may garbhakalina, unconsciousness, high pressure, gemja out on the nose, the mouth, the risk of this condition into labor, suddenly the patient's heart rhythm can be stopped, the patient could die, the baby can die, lungs, kidneys, liver damage may be the patient may be taken away at any moment, could be the long-term high pressure, treatment of this disease, there is no guarantee</td>");
            rpt.Append("</tr >");
            rpt.Append("</table>");
            rpt.Append("<br/>");
            rpt.Append("<table width='100%'>");
            rpt.Append("<tr style='height:25px'>");
            rpt.Append("<td style='font-family:Verdana;font-size:small; text-align:justify'> The organization learned from the patient about these issues and concerns . This organization has repeatedly us from any government medical college or any other health organizations have been asked to take , we suggest that these institutions have agreed along . But now the situation is like taking away patients who do not have , so we are keeping in mind the patient's life is at risk , including hostile cikitsara emergency admissions for patients who have made this institution , the patient gave permission for the necessary treatment .  </td>");
            rpt.Append("</tr >");
            rpt.Append("</table>");
            rpt.Append("<br/>");
            rpt.Append("<table width='100%'>");
            rpt.Append("<tr style='height:25px'>");
            rpt.Append("<td style=' font-family:Verdana;font-size:small; text-align:justify'> We think it's healthy to talk with our kindred allowed to treat the patient during treatment, or in this case, the organization would be without the complicated error occurred when the patient or any other life-threatening if not, we'll blame the institution. </td>");
            rpt.Append("</tr >");
            rpt.Append("</table>");


            rpt.Append("<table width='100%'>");
            rpt.Append("<tr style='height:27px'>");
            rpt.Append("<td style='font-family:Verdana; font-size:medium; text-align:center'> Signature </td>");
            rpt.Append("<td style='font-family:Verdana; font-size:medium; text-align:center'> Full Name  </td>");
            rpt.Append("<td style='font-family:Verdana; font-size:medium; text-align:center'> Address </td>");
            rpt.Append("<td style='font-family:Verdana; font-size:medium; text-align:center'> Relation  </td>");
            rpt.Append("</tr >");

            rpt.Append("<tr style='height:27px'>");
            rpt.Append("<td style='font-family:Verdana;  font-size:small; text-align:center'>..................</td>");
            rpt.AppendFormat("<td style='font-family:Verdana;  font-size:small; text-align:center'>{0}</td>", ds.Tables[0].Rows[0]["patient_name"]);
            rpt.AppendFormat("<td style='font-family:Verdana; font-size:small; text-align:center'>{0}</td>", ds.Tables[0].Rows[0]["vill_city"]);
            rpt.Append("<td style='font-family:Verdana; font-size:small; text-align:center'> Self </td>");
            rpt.Append("</tr >");

            rpt.Append("<tr style='height:27px'>");
            rpt.Append("<td style='font-family:Verdana;  font-size:small; text-align:center'>..................</td>");
            rpt.AppendFormat("<td style='font-family:Verdana;  font-size:small; text-align:center'>{0}</td>", ds.Tables[0].Rows[0]["guardian_name"]);
            rpt.AppendFormat("<td style='font-family:Verdana; font-size:small; text-align:center'>{0}</td>", ds.Tables[0].Rows[0]["vill_city2"]);
            rpt.AppendFormat("<td style='font-family:Verdana; font-size:small; text-align:center'> {0} </td>", ds.Tables[0].Rows[0]["relation"]);
            rpt.Append("</tr >");
            rpt.Append("</table>"); 
        } 
        ltrReport.Visible = true;
    }

    public void GetHearder_PoisionIntake(string reg)
    {
        ltrReport.Text = "";
        DataSet ds = thepatientbill.PatientForReport(reg, Session["CoCode"].ToString().Trim());
        if (ds.Tables[0].Rows.Count > 0)
        {
            rpt.Append("<table width='100%' cellspacing=0 border=0 style='border:1px solid black;border-spacing: 0; padding: 0;font-family:verdana;'>");
            rpt.Append("<tr style='height:25px;'>");
            rpt.Append("<td style='font-family:Verdana; font-size:x-large;text-align:center'><b>রোগীর বিশেষ কয়েকটি আশঙ্কাজনক পরিস্থিতি ও তার ঝুঁকি সম্পর্কে স্বীকার পত্র</b>");
            rpt.Append("</tr >");

            rpt.Append("<tr style='height:20px;'>");
            rpt.AppendFormat("<td style='font-family:Verdana; font-size:medium;text-align:center'>(যিনি ভর্তি করাচ্ছেন তার নাম:{0},সম্পর্ক:{1})</td>", ds.Tables[0].Rows[0]["guardian_name"], ds.Tables[0].Rows[0]["relation"]);
            rpt.Append("</tr >");
            rpt.Append("</table>");
            PatientDetails_Bengali(ds);
            rpt.Append("<br/>");  

            rpt.Append("<table width='100%'>");
            rpt.Append("<tr style='height:25px'>"); rpt.Append("<u>");
            rpt.Append("<td style='font-family:Verdana;font-size:medium;font-weight:bold; text-align:left'>রোগীর সমস্যা বা ঝুঁকির কারণ সমূহ: </td>"); rpt.Append("</u>");
            rpt.Append("</tr >");

            rpt.Append("<tr style='height:35px'>");
            rpt.Append("<td style='font-family:Verdana;font-size:medium; text-align:left'>বিষক্রিয়ায় হার্ট ফুসফুস কিডনি লিভার ও ব্রেন বিকল হতে পারে, ইনফেকসন বা সেপশিস হতে পারে, শ্বাসকষ্ট হতে পারে, ব্রেনে রক্তপাত হতে পারে, হঠাৎ হৃদযন্র বিকল হতে পারে, পেট ফুলে যেতে পারে, যেখানে সেখানে রক্তপাত হতে পারে</td>");
            rpt.Append("</tr >");

            rpt.Append("<tr style='height:35px'>"); rpt.Append("<u>");
            rpt.Append("<td style='font-family:Verdana;font-size:medium;font-weight:bold;text-align:left'>ওই সমস্ত রোগীর যে সমস্ত বিপত্তি ঘটতে পারে :</td>"); rpt.Append("</u>");
            rpt.Append("</tr >");

            rpt.Append("<tr style='height:35px'>");
            rpt.Append("<td style='font-family:Verdana;font-size:medium; text-align:left'> যখন তখন রোগীর অবস্থা আরও খারাপ হতে পারে, এমন কি মারা ও যেতে পারে, যে কোন মুহূর্তে অন্যত্র নিয়ে যেতে হতে পারে, দীর্ঘ মেয়াদী বিষের কুফল শরীরকে পঙ্গু করে দিতে পারে,এই রোগের চিকিৎসার কোন গ্যারান্টী নেই</td>");
            rpt.Append("</tr >");
            rpt.Append("</table>");
            rpt.Append("<br/>");
            rpt.Append("<table width='100%'>");
            rpt.Append("<tr style='height:30px'>");
            rpt.Append("<td style='font-family:Verdana;font-size:medium; text-align:justify'>এই প্রতিষ্ঠানের পক্ষ থেকে রোগীর এই সমস্ত সমস্যা ও আশঙ্কার কথা জানলাম । এই প্রতিষ্ঠানের পক্ষ থেকে আমাদের বারবার কোন সরকারী মেডিকেল কলেজ কিংবা অন্য কোনও স্বাস্থ্য প্রতিষ্ঠানের নিয়ে যাবার কথা বলা হয়েছে , আমরাও এই প্রতিষ্ঠান যে সমস্ত পরামর্শ দিয়েছে তারসঙ্গে একমত । কিন্তু এই মূহুর্তে রোগীকে অন্যত্র নিয়ে যাবার মতো পরিস্থিতি আমাদের নেই , তাই রোগীর জীবন হানী সহ সমস্ত ঝুঁকির কথা মাথায় রেখেই আমরা আপদকালীন চিকিৎ্সার জন্য রোগীকে এই প্রতিষ্ঠানে ভর্তি করলাম , এখানে রোগীর প্রয়োজনীয় যাবতীয় চিকিত্সা করার জন্য অনুমতি দিলাম । </td>");
            rpt.Append("</tr >");
            rpt.Append("</table>");
            rpt.Append("<br/>");
            rpt.Append("<table width='100%'>");
            rpt.Append("<tr style='height:30px'>");
            rpt.Append("<td style=' font-family:Verdana;font-size:medium; text-align:justify'>আমরা যেহেতু সুস্থ মনে আমাদের আত্মীয় পরিজনদের সঙ্গে আলোচনা করে রোগীর চিকিত্সা চালানোর অনুমতি দিচ্ছি সেক্ষেত্রে চিকিত্সা চলাকালীন কিংবা এই প্রতিষ্ঠান থেকে ছাড়া পাওয়ার পর রোগীর অন্য কোন জটিল সম্যসা হলে কিংবা জীবন সংশয় হলে আমরা এই প্রতিষ্ঠানকে দায়ী করব না ।</td>");
            rpt.Append("</tr >");
            rpt.Append("</table>");

            rpt.Append("<table width='100%'>");
            rpt.Append("<tr style='height:35px'>");
            rpt.Append("<td style='font-family:Verdana; font-size:medium; text-align:center'> স্বাক্ষর/টিপ </td>");
            rpt.Append("<td style='font-family:Verdana; font-size:medium; text-align:center'> পুরো  নাম  </td>");
            rpt.Append("<td style='font-family:Verdana; font-size:medium; text-align:center'> ঠিকানা </td>");
            rpt.Append("<td style='font-family:Verdana; font-size:medium; text-align:center'> সম্পর্ক  </td>");
            rpt.Append("</tr >");

            rpt.Append("<tr style='height:27px'>");
            rpt.Append("<td style='font-family:Verdana;  font-size:small; text-align:center'>..................</td>");
            rpt.AppendFormat("<td style='font-family:Verdana;  font-size:small; text-align:center'>{0}</td>", ds.Tables[0].Rows[0]["patient_name"]);
            rpt.AppendFormat("<td style='font-family:Verdana; font-size:small; text-align:center'>{0}</td>", ds.Tables[0].Rows[0]["vill_city"]);
            rpt.Append("<td style='font-family:Verdana; font-size:small; text-align:center'> নিজে </td>");
            rpt.Append("</tr >");

            rpt.Append("<tr style='height:27px'>");
            rpt.Append("<td style='font-family:Verdana;  font-size:small; text-align:center'>..................</td>");
            rpt.AppendFormat("<td style='font-family:Verdana;  font-size:small; text-align:center'>{0}</td>", ds.Tables[0].Rows[0]["guardian_name"]);
            rpt.AppendFormat("<td style='font-family:Verdana; font-size:small; text-align:center'>{0}</td>", ds.Tables[0].Rows[0]["vill_city"]);
            rpt.AppendFormat("<td style='font-family:Verdana; font-size:small; text-align:center'> {0} </td>", ds.Tables[0].Rows[0]["relation"]);
            rpt.Append("</tr >");
            rpt.Append("</table>"); 
        } 
        ltrReport.Visible = true;
    }

    public void GetHearder_PoisionIntake1(string reg)
    {
        ltrReport.Text = "";
        DataSet ds = thepatientbill.PatientForReport(reg, Session["CoCode"].ToString().Trim());
        if (ds.Tables[0].Rows.Count > 0)
        {
            rpt.Append("<table width='100%' cellspacing=0 border=0 style='border:1px solid black;border-spacing: 0; padding: 0;font-family:verdana;'>");
            rpt.Append("<tr style='height:25px;'>");
            rpt.Append("<td style='font-family:Verdana; font-size:x-large;text-align:center'><b>Some of the critical situation of the patient about the risks and accept the Letter</b>");
            rpt.Append("</tr >");

            rpt.Append("<tr style='height:20px;'>");
            rpt.AppendFormat("<td style='font-family:Verdana; font-size:medium;text-align:center'>(Admitted By :{0}, Relation :{1})</td>", ds.Tables[0].Rows[0]["guardian_name"], ds.Tables[0].Rows[0]["relation"]);
            rpt.Append("</tr >");
            rpt.Append("</table>");

            PatientDetails_English(ds);
            rpt.Append("<br/>"); 

            rpt.Append("<table width='100%'>");
            rpt.Append("<tr style='height:25px'>"); rpt.Append("<u>");
            rpt.Append("<td style='font-family:Verdana;font-size:medium;font-weight:bold; text-align:left'>Problems or risk factors of the patient  : </td>"); rpt.Append("</u>");
            rpt.Append("</tr >");

            rpt.Append("<tr style='height:25px'>");
            rpt.Append("<td style='font-family:Verdana;font-size:small; text-align:left'> Heart, lungs, kidneys, liver and brain poisoning may be crippled, inaphekasana or sepasisa may be shortness of breath, bleeding may brene, hrdayanra can crash suddenly, the stomach may be swollen, and there may be bleeding </td>");
            rpt.Append("</tr >");

            rpt.Append("<tr style='height:25px'>"); rpt.Append("<u>");
            rpt.Append("<td style='font-family:Verdana;font-size:medium;font-weight:bold;text-align:left'>Hazard that may occur in the patient  :</td>"); rpt.Append("</u>");
            rpt.Append("</tr >");

            rpt.Append("<tr style='height:25px'>");
            rpt.Append("<td style='font-family:Verdana;font-size:small; text-align:left'> When the patient's condition could be worse, and may even die, that could be taken away at any moment, the long-term effects of the poison could cripple the body, treatment of this disease, there is no guarantee </td>");
            rpt.Append("</tr >");
            rpt.Append("</table>");
            rpt.Append("<br/>");
            rpt.Append("<table width='100%'>");
            rpt.Append("<tr style='height:25px'>");
            rpt.Append("<td style='font-family:Verdana;font-size:small; text-align:justify'> The organization learned from the patient about these issues and concerns . This organization has repeatedly us from any government medical college or any other health organizations have been asked to take , we suggest that these institutions have agreed along . But now the situation is like taking away patients who do not have , so we are keeping in mind the patient's life is at risk , including hostile cikitsara emergency admissions for patients who have made this institution , the patient gave permission for the necessary treatment .  </td>");
            rpt.Append("</tr >");
            rpt.Append("</table>");
            rpt.Append("<br/>");
            rpt.Append("<table width='100%'>");
            rpt.Append("<tr style='height:25px'>");
            rpt.Append("<td style=' font-family:Verdana;font-size:small; text-align:justify'> We think it's healthy to talk with our kindred allowed to treat the patient during treatment, or in this case, the organization would be without the complicated error occurred when the patient or any other life-threatening if not, we'll blame the institution. </td>");
            rpt.Append("</tr >");
            rpt.Append("</table>");


            rpt.Append("<table width='100%'>");
            rpt.Append("<tr style='height:27px'>");
            rpt.Append("<td style='font-family:Verdana; font-size:medium; text-align:center'> Signature </td>");
            rpt.Append("<td style='font-family:Verdana; font-size:medium; text-align:center'> Full Name  </td>");
            rpt.Append("<td style='font-family:Verdana; font-size:medium; text-align:center'> Address </td>");
            rpt.Append("<td style='font-family:Verdana; font-size:medium; text-align:center'> Relation  </td>");
            rpt.Append("</tr >");

            rpt.Append("<tr style='height:27px'>");
            rpt.Append("<td style='font-family:Verdana;  font-size:small; text-align:center'>..................</td>");
            rpt.AppendFormat("<td style='font-family:Verdana;  font-size:small; text-align:center'>{0}</td>", ds.Tables[0].Rows[0]["patient_name"]);
            rpt.AppendFormat("<td style='font-family:Verdana; font-size:small; text-align:center'>{0}</td>", ds.Tables[0].Rows[0]["vill_city"]);
            rpt.Append("<td style='font-family:Verdana; font-size:small; text-align:center'> Self </td>");
            rpt.Append("</tr >");

            rpt.Append("<tr style='height:27px'>");
            rpt.Append("<td style='font-family:Verdana;  font-size:small; text-align:center'>..................</td>");
            rpt.AppendFormat("<td style='font-family:Verdana;  font-size:small; text-align:center'>{0}</td>", ds.Tables[0].Rows[0]["guardian_name"]);
            rpt.AppendFormat("<td style='font-family:Verdana; font-size:small; text-align:center'>{0}</td>", ds.Tables[0].Rows[0]["vill_city"]);
            rpt.AppendFormat("<td style='font-family:Verdana; font-size:small; text-align:center'> {0} </td>", ds.Tables[0].Rows[0]["relation"]);
            rpt.Append("</tr >");
            rpt.Append("</table>"); 
        }
        ltrReport.Visible = true;
    }

    public void GetHearder_PostOprative(string reg)
    {
        ltrReport.Text = "";
        DataSet ds = thepatientbill.PatientForReport(reg, Session["CoCode"].ToString().Trim());
        if (ds.Tables[0].Rows.Count > 0)
        {
            rpt.Append("<table width='100%' cellspacing=0 border=0 style='border:1px solid black;border-spacing: 0; padding: 0;font-family:verdana;'>");
            rpt.Append("<tr style='height:25px;'>");
            rpt.Append("<td style='font-family:Verdana; font-size:x-large;text-align:center'><b>রোগীর বিশেষ কয়েকটি আশঙ্কাজনক পরিস্থিতি ও তার ঝুঁকি সম্পর্কে স্বীকার পত্র</b>");
            rpt.Append("</tr >");

            rpt.Append("<tr style='height:20px;'>");
            rpt.AppendFormat("<td style='font-family:Verdana; font-size:medium;text-align:center'>(যিনি ভর্তি করাচ্ছেন তার নাম:{0},সম্পর্ক:{1})</td>", ds.Tables[0].Rows[0]["guardian_name"], ds.Tables[0].Rows[0]["relation"]);
            rpt.Append("</tr >");
            rpt.Append("</table>");
            PatientDetails_Bengali(ds);
            rpt.Append("<br/>");  

            rpt.Append("<table width='100%'>");
            rpt.Append("<tr style='height:25px'>"); rpt.Append("<u>");
            rpt.Append("<td style='font-family:Verdana;font-size:medium;font-weight:bold; text-align:left'>রোগীর সমস্যা বা ঝুঁকির কারণ সমূহ: </td>"); rpt.Append("</u>");
            rpt.Append("</tr >");

            rpt.Append("<tr style='height:35px'>");
            rpt.Append("<td style='font-family:Verdana;font-size:medium; text-align:left'>  ব্লীডিং হচ্ছে, পেট ফুলে আছে, বায়ূ সরছে না, জ্বর কমছে না, কাটা জায়গায় ঘা, জন্ডিস </td>");
            rpt.Append("</tr >");

            rpt.Append("<tr style='height:35px'>"); rpt.Append("<u>");
            rpt.Append("<td style='font-family:Verdana;font-size:medium;font-weight:bold;text-align:left'>ওই সমস্ত রোগীর যে সমস্ত বিপত্তি ঘটতে পারে :</td>"); rpt.Append("</u>");
            rpt.Append("</tr >");

            rpt.Append("<tr style='height:35px'>");
            rpt.Append("<td style='font-family:Verdana;font-size:medium; text-align:left'> ইনফেকসন বা সেপশিস হতে পারে, শ্বাসকষ্ট হতে পারে, ব্রেনে রক্তপাত হতে পারে, হঠাৎ হৃদযন্র বিকল হতে পারে, পেট ফুলে যেতে পারে, যখন তখন রোগীর অবস্থা খারাপ হতে পারে, আবার অপারেশন করতে হতে পারে , রক্ত দিতে হতে পারে,ফুসফুস কিডনি লিভার বিকল হতে পারে,এমন কি মারা ও যেতে পারে, যে কোন মুহূর্তে অন্যত্র নিয়ে যেতে হতে পারে </td>");
            rpt.Append("</tr >");
            rpt.Append("</table>");
            rpt.Append("<br/>");
            rpt.Append("<table width='100%'>");
            rpt.Append("<tr style='height:30px'>");
            rpt.Append("<td style='font-family:Verdana;font-size:medium; text-align:justify'>এই প্রতিষ্ঠানের পক্ষ থেকে রোগীর এই সমস্ত সমস্যা ও আশঙ্কার কথা জানলাম । এই প্রতিষ্ঠানের পক্ষ থেকে আমাদের বারবার কোন সরকারী মেডিকেল কলেজ কিংবা অন্য কোনও স্বাস্থ্য প্রতিষ্ঠানের নিয়ে যাবার কথা বলা হয়েছে , আমরাও এই প্রতিষ্ঠান যে সমস্ত পরামর্শ দিয়েছে তারসঙ্গে একমত । কিন্তু এই মূহুর্তে রোগীকে অন্যত্র নিয়ে যাবার মতো পরিস্থিতি আমাদের নেই , তাই রোগীর জীবন হানী সহ সমস্ত ঝুঁকির কথা মাথায় রেখেই আমরা আপদকালীন চিকিৎ্সার জন্য রোগীকে এই প্রতিষ্ঠানে ভর্তি করলাম , এখানে রোগীর প্রয়োজনীয় যাবতীয় চিকিত্সা করার জন্য অনুমতি দিলাম । </td>");
            rpt.Append("</tr >");
            rpt.Append("</table>");
            rpt.Append("<br/>");
            rpt.Append("<table width='100%'>");
            rpt.Append("<tr style='height:30px'>");
            rpt.Append("<td style=' font-family:Verdana;font-size:medium; text-align:justify'>আমরা যেহেতু সুস্থ মনে আমাদের আত্মীয় পরিজনদের সঙ্গে আলোচনা করে রোগীর চিকিত্সা চালানোর অনুমতি দিচ্ছি সেক্ষেত্রে চিকিত্সা চলাকালীন কিংবা এই প্রতিষ্ঠান থেকে ছাড়া পাওয়ার পর রোগীর অন্য কোন জটিল সম্যসা হলে কিংবা জীবন সংশয় হলে আমরা এই প্রতিষ্ঠানকে দায়ী করব না ।</td>");
            rpt.Append("</tr >");
            rpt.Append("</table>");

            rpt.Append("<table width='100%'>");
            rpt.Append("<tr style='height:35px'>");
            rpt.Append("<td style='font-family:Verdana; font-size:medium; text-align:center'> স্বাক্ষর/টিপ </td>");
            rpt.Append("<td style='font-family:Verdana; font-size:medium; text-align:center'> পুরো  নাম  </td>");
            rpt.Append("<td style='font-family:Verdana; font-size:medium; text-align:center'> ঠিকানা </td>");
            rpt.Append("<td style='font-family:Verdana; font-size:medium; text-align:center'> সম্পর্ক  </td>");
            rpt.Append("</tr >");

            rpt.Append("<tr style='height:27px'>");
            rpt.Append("<td style='font-family:Verdana;  font-size:small; text-align:center'>..................</td>");
            rpt.AppendFormat("<td style='font-family:Verdana;  font-size:small; text-align:center'>{0}</td>", ds.Tables[0].Rows[0]["patient_name"]);
            rpt.AppendFormat("<td style='font-family:Verdana; font-size:small; text-align:center'>{0}</td>", ds.Tables[0].Rows[0]["vill_city"]);
            rpt.Append("<td style='font-family:Verdana; font-size:small; text-align:center'> নিজে </td>");
            rpt.Append("</tr >");

            rpt.Append("<tr style='height:27px'>");
            rpt.Append("<td style='font-family:Verdana;  font-size:small; text-align:center'>..................</td>");
            rpt.AppendFormat("<td style='font-family:Verdana;  font-size:small; text-align:center'>{0}</td>", ds.Tables[0].Rows[0]["guardian_name"]);
            rpt.AppendFormat("<td style='font-family:Verdana; font-size:small; text-align:center'>{0}</td>", ds.Tables[0].Rows[0]["vill_city"]);
            rpt.AppendFormat("<td style='font-family:Verdana; font-size:small; text-align:center'> {0} </td>", ds.Tables[0].Rows[0]["relation"]);
            rpt.Append("</tr >");
            rpt.Append("</table>"); 
        }
        ltrReport.Visible = true;
    }

    public void GetHearder_PostOprative1(string reg)
    {
        ltrReport.Text = "";
        DataSet ds = thepatientbill.PatientForReport(reg, Session["CoCode"].ToString().Trim());
        if (ds.Tables[0].Rows.Count > 0)
        {
            rpt.Append("<table width='100%' cellspacing=0 border=0 style='border:1px solid black;border-spacing: 0; padding: 0;font-family:verdana;'>");
            rpt.Append("<tr style='height:25px;'>");
            rpt.Append("<td style='font-family:Verdana; font-size:x-large;text-align:center'><b>Some of the critical situation of the patient about the risks and accept the Letter</b>");
            rpt.Append("</tr >");

            rpt.Append("<tr style='height:20px;'>");
            rpt.AppendFormat("<td style='font-family:Verdana; font-size:medium;text-align:center'>(Admitted By :{0}, Relation :{1})</td>", ds.Tables[0].Rows[0]["guardian_name"], ds.Tables[0].Rows[0]["relation"]);
            rpt.Append("</tr >");
            rpt.Append("</table>");

            PatientDetails_English(ds);
            rpt.Append("<br/>"); 


            rpt.Append("<table width='100%'>");
            rpt.Append("<tr style='height:25px'>"); rpt.Append("<u>");
            rpt.Append("<td style='font-family:Verdana;font-size:medium;font-weight:bold; text-align:left'>Problems or risk factors of the patient  : </td>"); rpt.Append("</u>");
            rpt.Append("</tr >");

            rpt.Append("<tr style='height:25px'>");
            rpt.Append("<td style='font-family:Verdana;font-size:small; text-align:left'>Blidim being, has swollen stomach, not sarache bayu fever continues, cut, blow the place, jaundice </td>");
            rpt.Append("</tr >");

            rpt.Append("<tr style='height:25px'>"); rpt.Append("<u>");
            rpt.Append("<td style='font-family:Verdana;font-size:medium;font-weight:bold;text-align:left'>Hazard that may occur in the patient  :</td>"); rpt.Append("</u>");
            rpt.Append("</tr >");

            rpt.Append("<tr style='height:25px'>");
            rpt.Append("<td style='font-family:Verdana;font-size:small; text-align:left'> Sepasisa inaphekasana or may be, may be shortness of breath, bleeding may brene, hrdayanra can crash suddenly, stomach swelling can be worse when the patient's condition, the operation may be, may give blood, lungs, kidneys, liver crash may, and may even die, that may go away at any moment</td>");
            rpt.Append("</tr >");
            rpt.Append("</table>");
            rpt.Append("<br/>");
            rpt.Append("<table width='100%'>");
            rpt.Append("<tr style='height:25px'>");
            rpt.Append("<td style='font-family:Verdana;font-size:small; text-align:justify'> The organization learned from the patient about these issues and concerns . This organization has repeatedly us from any government medical college or any other health organizations have been asked to take , we suggest that these institutions have agreed along . But now the situation is like taking away patients who do not have , so we are keeping in mind the patient's life is at risk , including hostile cikitsara emergency admissions for patients who have made this institution , the patient gave permission for the necessary treatment .  </td>");
            rpt.Append("</tr >");
            rpt.Append("</table>");
            rpt.Append("<br/>");
            rpt.Append("<table width='100%'>");
            rpt.Append("<tr style='height:25px'>");
            rpt.Append("<td style=' font-family:Verdana;font-size:small; text-align:justify'> We think it's healthy to talk with our kindred allowed to treat the patient during treatment, or in this case, the organization would be without the complicated error occurred when the patient or any other life-threatening if not, we'll blame the institution. </td>");
            rpt.Append("</tr >");
            rpt.Append("</table>");


            rpt.Append("<table width='100%'>");
            rpt.Append("<tr style='height:27px'>");
            rpt.Append("<td style='font-family:Verdana; font-size:medium; text-align:center'> Signature </td>");
            rpt.Append("<td style='font-family:Verdana; font-size:medium; text-align:center'> Full Name  </td>");
            rpt.Append("<td style='font-family:Verdana; font-size:medium; text-align:center'> Address </td>");
            rpt.Append("<td style='font-family:Verdana; font-size:medium; text-align:center'> Relation  </td>");
            rpt.Append("</tr >");

            rpt.Append("<tr style='height:27px'>");
            rpt.Append("<td style='font-family:Verdana;  font-size:small; text-align:center'>..................</td>");
            rpt.AppendFormat("<td style='font-family:Verdana;  font-size:small; text-align:center'>{0}</td>", ds.Tables[0].Rows[0]["patient_name"]);
            rpt.AppendFormat("<td style='font-family:Verdana; font-size:small; text-align:center'>{0}</td>", ds.Tables[0].Rows[0]["vill_city"]);
            rpt.Append("<td style='font-family:Verdana; font-size:small; text-align:center'> Self </td>");
            rpt.Append("</tr >");

            rpt.Append("<tr style='height:27px'>");
            rpt.Append("<td style='font-family:Verdana;  font-size:small; text-align:center'>..................</td>");
            rpt.AppendFormat("<td style='font-family:Verdana;  font-size:small; text-align:center'>{0}</td>", ds.Tables[0].Rows[0]["guardian_name"]);
            rpt.AppendFormat("<td style='font-family:Verdana; font-size:small; text-align:center'>{0}</td>", ds.Tables[0].Rows[0]["vill_city2"]);
            rpt.AppendFormat("<td style='font-family:Verdana; font-size:small; text-align:center'> {0} </td>", ds.Tables[0].Rows[0]["relation"]);
            rpt.Append("</tr >");
            rpt.Append("</table>"); 
        } 
        ltrReport.Visible = true;
    }

    public void GetHearder_PPHReport(string reg)
    {
        ltrReport.Text = "";

        DataSet ds = thepatientbill.PatientForReport(reg, Session["CoCode"].ToString().Trim());
        if (ds.Tables[0].Rows.Count > 0)
        {
            rpt.Append("<table width='100%' cellspacing=0 border=0 style='border:1px solid black;border-spacing: 0; padding: 0;font-family:verdana;'>");
            rpt.Append("<tr style='height:25px;'>");
            rpt.Append("<td style='font-family:Verdana; font-size:x-large;text-align:center'><b>রোগীর বিশেষ কয়েকটি আশঙ্কাজনক পরিস্থিতি ও তার ঝুঁকি সম্পর্কে স্বীকার পত্র</b>");
            rpt.Append("</tr >");

            rpt.Append("<tr style='height:20px;'>");
            rpt.AppendFormat("<td style='font-family:Verdana; font-size:medium;text-align:center'>(যিনি ভর্তি করাচ্ছেন তার নাম:{0},সম্পর্ক:{1})</td>", ds.Tables[0].Rows[0]["guardian_name"], ds.Tables[0].Rows[0]["relation"]);
            rpt.Append("</tr >");
            rpt.Append("</table>");
            PatientDetails_Bengali(ds);
            rpt.Append("<br/>");  

            rpt.Append("<table width='100%'>");
            rpt.Append("<tr style='height:25px'>"); rpt.Append("<u>");
            rpt.Append("<td style='font-family:Verdana;font-size:medium;font-weight:bold; text-align:left'>রোগীর সমস্যা বা ঝুঁকির কারণ সমূহ: </td>"); rpt.Append("</u>");
            rpt.Append("</tr >");

            rpt.Append("<tr style='height:35px'>");
            rpt.Append("<td style='font-family:Verdana;font-size:medium; text-align:left'>প্রসবের পর প্রচুর রক্তস্রাব হচ্ছে, কার এই রোগ হবে তা আগে থেকে জানা বা আগে থেকে কোন প্রতিষেধক নেওয়া যায় না, প্রচুর রক্তস্রাবের জন্য রোগী রক্তশূণ্য হতে পারে,হঠাৎ রোগীর হৃদ স্পন্দন থেমে যেতে পারে, রোগী মারা যেতে পারে, ফুসফুস, কিডনি, লিভার বিকল হতে পারে, যে কোন মুহূর্তে রোগীকে অন্যত্র নিয়ে যেতে হতে পারে, এই রোগের চিকিৎসার কোন গ্যারান্টী নেই</td>");
            rpt.Append("</tr >");

            rpt.Append("<tr style='height:35px'>"); rpt.Append("<u>");
            rpt.Append("<td style='font-family:Verdana;font-size:medium;font-weight:bold;text-align:left'>ওই সমস্ত রোগীর যে সমস্ত বিপত্তি ঘটতে পারে :</td>"); rpt.Append("</u>");
            rpt.Append("</tr >");

            rpt.Append("<tr style='height:35px'>");
            rpt.Append("<td style='font-family:Verdana;font-size:medium; text-align:left'>এখুনি প্রচুর রক্ত লাগবে, এই রক্ত জোগাড় করার দায়িত্ব রোগীর বাড়ীর লোককেই নিতে হবে, রক্ত জোগাড় করতে যত দেরি হবে রোগীর বিপদ তত বাড়বে, আর রক্ত না পেলে চিকিৎসা করা প্রায় অসম্ভব, রক্ত পাত বন্ধ না হলে অপারেশন করে জরায়ূ কেটে বাদ দিতে হবে ও এরপর রোগীর আর কোনদিন বাচ্চা হবে না আর মাসিক ও হবে না</td>");
            rpt.Append("</tr >");
            rpt.Append("</table>");
            rpt.Append("<br/>");
            rpt.Append("<table width='100%'>");
            rpt.Append("<tr style='height:30px'>");
            rpt.Append("<td style='font-family:Verdana;font-size:medium; text-align:justify'>এই প্রতিষ্ঠানের পক্ষ থেকে রোগীর এই সমস্ত সমস্যা ও আশঙ্কার কথা জানলাম । এই প্রতিষ্ঠানের পক্ষ থেকে আমাদের বারবার কোন সরকারী মেডিকেল কলেজ কিংবা অন্য কোনও স্বাস্থ্য প্রতিষ্ঠানের নিয়ে যাবার কথা বলা হয়েছে , আমরাও এই প্রতিষ্ঠান যে সমস্ত পরামর্শ দিয়েছে তারসঙ্গে একমত । কিন্তু এই মূহুর্তে রোগীকে অন্যত্র নিয়ে যাবার মতো পরিস্থিতি আমাদের নেই , তাই রোগীর জীবন হানী সহ সমস্ত ঝুঁকির কথা মাথায় রেখেই আমরা আপদকালীন চিকিৎ্সার জন্য রোগীকে এই প্রতিষ্ঠানে ভর্তি করলাম , এখানে রোগীর প্রয়োজনীয় যাবতীয় চিকিত্সা করার জন্য অনুমতি দিলাম । </td>");
            rpt.Append("</tr >");
            rpt.Append("</table>");
            rpt.Append("<br/>");
            rpt.Append("<table width='100%'>");
            rpt.Append("<tr style='height:30px'>");
            rpt.Append("<td style=' font-family:Verdana;font-size:medium; text-align:justify'>আমরা যেহেতু সুস্থ মনে আমাদের আত্মীয় পরিজনদের সঙ্গে আলোচনা করে রোগীর চিকিত্সা চালানোর অনুমতি দিচ্ছি সেক্ষেত্রে চিকিত্সা চলাকালীন কিংবা এই প্রতিষ্ঠান থেকে ছাড়া পাওয়ার পর রোগীর অন্য কোন জটিল সম্যসা হলে কিংবা জীবন সংশয় হলে আমরা এই প্রতিষ্ঠানকে দায়ী করব না ।</td>");
            rpt.Append("</tr >");
            rpt.Append("</table>");




            rpt.Append("<table width='100%'>");
            rpt.Append("<tr style='height:35px'>");
            rpt.Append("<td style='font-family:Verdana; font-size:medium; text-align:center'> স্বাক্ষর/টিপ </td>");
            rpt.Append("<td style='font-family:Verdana; font-size:medium; text-align:center'> পুরো  নাম  </td>");
            rpt.Append("<td style='font-family:Verdana; font-size:medium; text-align:center'> ঠিকানা </td>");
            rpt.Append("<td style='font-family:Verdana; font-size:medium; text-align:center'> সম্পর্ক  </td>");
            rpt.Append("</tr >");

            rpt.Append("<tr style='height:27px'>");
            rpt.Append("<td style='font-family:Verdana;  font-size:small; text-align:center'>..................</td>");
            rpt.AppendFormat("<td style='font-family:Verdana;  font-size:small; text-align:center'>{0}</td>", ds.Tables[0].Rows[0]["patient_name"]);
            rpt.AppendFormat("<td style='font-family:Verdana; font-size:small; text-align:center'>{0}</td>", ds.Tables[0].Rows[0]["vill_city"]);
            rpt.Append("<td style='font-family:Verdana; font-size:small; text-align:center'> নিজে </td>");
            rpt.Append("</tr >");

            rpt.Append("<tr style='height:27px'>");
            rpt.Append("<td style='font-family:Verdana;  font-size:small; text-align:center'>..................</td>");
            rpt.AppendFormat("<td style='font-family:Verdana;  font-size:small; text-align:center'>{0}</td>", ds.Tables[0].Rows[0]["guardian_name"]);
            rpt.AppendFormat("<td style='font-family:Verdana; font-size:small; text-align:center'>{0}</td>", ds.Tables[0].Rows[0]["vill_city2"]);
            rpt.AppendFormat("<td style='font-family:Verdana; font-size:small; text-align:center'> {0} </td>", ds.Tables[0].Rows[0]["relation"]);
            rpt.Append("</tr >");
            rpt.Append("</table>"); 
        } 
        ltrReport.Visible = true;
    }

    public void GetHearder_PPHReport1(string reg)
    {
        ltrReport.Text = "";
        DataSet ds = thepatientbill.PatientForReport(reg, Session["CoCode"].ToString().Trim());
        if (ds.Tables[0].Rows.Count > 0)
        {
            rpt.Append("<table width='100%' cellspacing=0 border=0 style='border:1px solid black;border-spacing: 0; padding: 0;font-family:verdana;'>");
            rpt.Append("<tr style='height:25px;'>");
            rpt.Append("<td style='font-family:Verdana; font-size:x-large;text-align:center'><b>Some of the critical situation of the patient about the risks and accept the Letter</b>");
            rpt.Append("</tr >");

            rpt.Append("<tr style='height:20px;'>");
            rpt.AppendFormat("<td style='font-family:Verdana; font-size:medium;text-align:center'>(Admitted By :{0}, Relation :{1})</td>", ds.Tables[0].Rows[0]["guardian_name"], ds.Tables[0].Rows[0]["relation"]);
            rpt.Append("</tr >");
            rpt.Append("</table>");

            PatientDetails_English(ds);
            rpt.Append("<br/>"); 


            rpt.Append("<table width='100%'>");
            rpt.Append("<tr style='height:25px'>"); rpt.Append("<u>");
            rpt.Append("<td style='font-family:Verdana;font-size:medium;font-weight:bold; text-align:left'>Problems or risk factors of the patient  : </td>"); rpt.Append("</u>");
            rpt.Append("</tr >");

            rpt.Append("<tr style='height:25px'>");
            rpt.Append("<td style='font-family:Verdana;font-size:small; text-align:left'>Bleeding after delivery of the car to the disease before it is known or can be taken before any antidote, lots of blood may be raktasunya the patient, the patient's heart rhythm suddenly stopped could be, patients can die, lungs, kidneys, and liver damage may be, may be taken away at any moment the patient, the treatment of this disease, there is no guarantee</td>");
            rpt.Append("</tr >");

            rpt.Append("<tr style='height:25px'>"); rpt.Append("<u>");
            rpt.Append("<td style='font-family:Verdana;font-size:medium;font-weight:bold;text-align:left'>Hazard that may occur in the patient  :</td>"); rpt.Append("</u>");
            rpt.Append("</tr >");

            rpt.Append("<tr style='height:25px'>");
            rpt.Append("<td style='font-family:Verdana;font-size:small; text-align:left'>Take away a lot of blood, the blood of the patient's home, the children will take the responsibility to obtain, the greater danger to the patient's blood will be too late to raise the other hand, if the blood is almost impossible to treat, lots of blood will not stop the operations and then cut jarayu the child will never be patient and make the monthly</td>");
            rpt.Append("</tr >");
            rpt.Append("</table>");
            rpt.Append("<br/>");
            rpt.Append("<table width='100%'>");
            rpt.Append("<tr style='height:25px'>");
            rpt.Append("<td style='font-family:Verdana;font-size:small; text-align:justify'> The organization learned from the patient about these issues and concerns . This organization has repeatedly us from any government medical college or any other health organizations have been asked to take , we suggest that these institutions have agreed along . But now the situation is like taking away patients who do not have , so we are keeping in mind the patient's life is at risk , including hostile cikitsara emergency admissions for patients who have made this institution , the patient gave permission for the necessary treatment .  </td>");
            rpt.Append("</tr >");
            rpt.Append("</table>");
            rpt.Append("<br/>");
            rpt.Append("<table width='100%'>");
            rpt.Append("<tr style='height:25px'>");
            rpt.Append("<td style=' font-family:Verdana;font-size:small; text-align:justify'> We think it's healthy to talk with our kindred allowed to treat the patient during treatment, or in this case, the organization would be without the complicated error occurred when the patient or any other life-threatening if not, we'll blame the institution. </td>");
            rpt.Append("</tr >");
            rpt.Append("</table>");


            rpt.Append("<table width='100%'>");
            rpt.Append("<tr style='height:27px'>");
            rpt.Append("<td style='font-family:Verdana; font-size:medium; text-align:center'> Signature </td>");
            rpt.Append("<td style='font-family:Verdana; font-size:medium; text-align:center'> Full Name  </td>");
            rpt.Append("<td style='font-family:Verdana; font-size:medium; text-align:center'> Address </td>");
            rpt.Append("<td style='font-family:Verdana; font-size:medium; text-align:center'> Relation  </td>");
            rpt.Append("</tr >");

            rpt.Append("<tr style='height:27px'>");
            rpt.Append("<td style='font-family:Verdana;  font-size:small; text-align:center'>..................</td>");
            rpt.AppendFormat("<td style='font-family:Verdana;  font-size:small; text-align:center'>{0}</td>", ds.Tables[0].Rows[0]["patient_name"]);
            rpt.AppendFormat("<td style='font-family:Verdana; font-size:small; text-align:center'>{0}</td>", ds.Tables[0].Rows[0]["vill_city"]);
            rpt.Append("<td style='font-family:Verdana; font-size:small; text-align:center'> Self </td>");
            rpt.Append("</tr >");

            rpt.Append("<tr style='height:27px'>");
            rpt.Append("<td style='font-family:Verdana;  font-size:small; text-align:center'>..................</td>");
            rpt.AppendFormat("<td style='font-family:Verdana;  font-size:small; text-align:center'>{0}</td>", ds.Tables[0].Rows[0]["guardian_name"]);
            rpt.AppendFormat("<td style='font-family:Verdana; font-size:small; text-align:center'>{0}</td>", ds.Tables[0].Rows[0]["vill_city"]);
            rpt.AppendFormat("<td style='font-family:Verdana; font-size:small; text-align:center'> {0} </td>", ds.Tables[0].Rows[0]["relation"]);
            rpt.Append("</tr >");
            rpt.Append("</table>"); 
        } 
        ltrReport.Visible = true;
    }

    public void GetHearder_PrematureBaby(string reg)
    {
        ltrReport.Text = "";
        DataSet ds = thepatientbill.PatientForReport(reg, Session["CoCode"].ToString().Trim());
        if (ds.Tables[0].Rows.Count > 0)
        {
            rpt.Append("<table width='100%' cellspacing=0 border=0 style='border:1px solid black;border-spacing: 0; padding: 0;font-family:verdana;'>");
            rpt.Append("<tr style='height:25px;'>");
            rpt.Append("<td style='font-family:Verdana; font-size:x-large;text-align:center'><b>রোগীর বিশেষ কয়েকটি আশঙ্কাজনক পরিস্থিতি ও তার ঝুঁকি সম্পর্কে স্বীকার পত্র</b>");
            rpt.Append("</tr >");

            rpt.Append("<tr style='height:20px;'>");
            rpt.AppendFormat("<td style='font-family:Verdana; font-size:medium;text-align:center'>(যিনি ভর্তি করাচ্ছেন তার নাম:{0},সম্পর্ক:{1})</td>", ds.Tables[0].Rows[0]["guardian_name"], ds.Tables[0].Rows[0]["relation"]);
            rpt.Append("</tr >");
            rpt.Append("</table>");
            PatientDetails_Bengali(ds);
            rpt.Append("<br/>");  

            rpt.Append("<table width='100%'>");
            rpt.Append("<tr style='height:25px'>"); rpt.Append("<u>");
            rpt.Append("<td style='font-family:Verdana;font-size:medium;font-weight:bold; text-align:left'>রোগীর সমস্যা বা ঝুঁকির কারণ সমূহ: </td>"); rpt.Append("</u>");
            rpt.Append("</tr >");

            rpt.Append("<tr style='height:35px'>");
            rpt.Append("<td style='font-family:Verdana;font-size:medium; text-align:left'>অপরিনত বাচ্চা, অস্বাভাবিক কম ওজন, জীবনী শক্তি কম, ফুসফুস কমজোরী, হার্ট দূর্বল, অন্যান্য অঙ্গ অপরিনত, খাওয়ানোর সমস্যা, যখন তখন বাচ্চার অবস্থা খারাপ হওয়ার প্রনণতা</td>");
            rpt.Append("</tr >");

            rpt.Append("<tr style='height:35px'>"); rpt.Append("<u>");
            rpt.Append("<td style='font-family:Verdana;font-size:medium;font-weight:bold;text-align:left'>ওই সমস্ত রোগীর যে সমস্ত বিপত্তি ঘটতে পারে :</td>"); rpt.Append("</u>");
            rpt.Append("</tr >");

            rpt.Append("<tr style='height:35px'>");
            rpt.Append("<td style='font-family:Verdana;font-size:medium; text-align:left'>ইনফেকসন বা সেপশিস হতে পারে, শ্বাসকষ্ট হতে পারে, হঠাৎ হৃদযন্র বিকল হতে পারে, পেট ফুলে যেতে পারে, যখন তখন বাচ্চার অবস্থা খারাপ হতে পারে, এমন কি মারা ও যেতে পারে, যে কোন মুহূর্তে অন্যত্র নিয়ে যেতে হতে পারে</td>");
            rpt.Append("</tr >");
            rpt.Append("</table>");
            rpt.Append("<br/>");
            rpt.Append("<table width='100%'>");
            rpt.Append("<tr style='height:30px'>");
            rpt.Append("<td style='font-family:Verdana;font-size:medium; text-align:justify'>এই প্রতিষ্ঠানের পক্ষ থেকে রোগীর এই সমস্ত সমস্যা ও আশঙ্কার কথা জানলাম । এই প্রতিষ্ঠানের পক্ষ থেকে আমাদের বারবার কোন সরকারী মেডিকেল কলেজ কিংবা অন্য কোনও স্বাস্থ্য প্রতিষ্ঠানের নিয়ে যাবার কথা বলা হয়েছে , আমরাও এই প্রতিষ্ঠান যে সমস্ত পরামর্শ দিয়েছে তারসঙ্গে একমত । কিন্তু এই মূহুর্তে রোগীকে অন্যত্র নিয়ে যাবার মতো পরিস্থিতি আমাদের নেই , তাই রোগীর জীবন হানী সহ সমস্ত ঝুঁকির কথা মাথায় রেখেই আমরা আপদকালীন চিকিৎ্সার জন্য রোগীকে এই প্রতিষ্ঠানে ভর্তি করলাম , এখানে রোগীর প্রয়োজনীয় যাবতীয় চিকিত্সা করার জন্য অনুমতি দিলাম । </td>");
            rpt.Append("</tr >");
            rpt.Append("</table>");
            rpt.Append("<br/>");
            rpt.Append("<table width='100%'>");
            rpt.Append("<tr style='height:30px'>");
            rpt.Append("<td style=' font-family:Verdana;font-size:medium; text-align:justify'>আমরা যেহেতু সুস্থ মনে আমাদের আত্মীয় পরিজনদের সঙ্গে আলোচনা করে রোগীর চিকিত্সা চালানোর অনুমতি দিচ্ছি সেক্ষেত্রে চিকিত্সা চলাকালীন কিংবা এই প্রতিষ্ঠান থেকে ছাড়া পাওয়ার পর রোগীর অন্য কোন জটিল সম্যসা হলে কিংবা জীবন সংশয় হলে আমরা এই প্রতিষ্ঠানকে দায়ী করব না ।</td>");
            rpt.Append("</tr >");
            rpt.Append("</table>");




            rpt.Append("<table width='100%'>");
            rpt.Append("<tr style='height:35px'>");
            rpt.Append("<td style='font-family:Verdana; font-size:medium; text-align:center'> স্বাক্ষর/টিপ </td>");
            rpt.Append("<td style='font-family:Verdana; font-size:medium; text-align:center'> পুরো  নাম  </td>");
            rpt.Append("<td style='font-family:Verdana; font-size:medium; text-align:center'> ঠিকানা </td>");
            rpt.Append("<td style='font-family:Verdana; font-size:medium; text-align:center'> সম্পর্ক  </td>");
            rpt.Append("</tr >");

            rpt.Append("<tr style='height:27px'>");
            rpt.Append("<td style='font-family:Verdana;  font-size:small; text-align:center'>..................</td>");
            rpt.AppendFormat("<td style='font-family:Verdana;  font-size:small; text-align:center'>{0}</td>", ds.Tables[0].Rows[0]["patient_name"]);
            rpt.AppendFormat("<td style='font-family:Verdana; font-size:small; text-align:center'>{0}</td>", ds.Tables[0].Rows[0]["vill_city"]);
            rpt.Append("<td style='font-family:Verdana; font-size:small; text-align:center'> নিজে </td>");
            rpt.Append("</tr >");

            rpt.Append("<tr style='height:27px'>");
            rpt.Append("<td style='font-family:Verdana;  font-size:small; text-align:center'>..................</td>");
            rpt.AppendFormat("<td style='font-family:Verdana;  font-size:small; text-align:center'>{0}</td>", ds.Tables[0].Rows[0]["guardian_name"]);
            rpt.AppendFormat("<td style='font-family:Verdana; font-size:small; text-align:center'>{0}</td>", ds.Tables[0].Rows[0]["vill_city2"]);
            rpt.AppendFormat("<td style='font-family:Verdana; font-size:small; text-align:center'> {0} </td>", ds.Tables[0].Rows[0]["relation"]);
            rpt.Append("</tr >");
            rpt.Append("</table>"); 
        } 
        ltrReport.Visible = true;
    }

    public void GetHearder_PrematureBaby1(string reg)
    {
        ltrReport.Text = "";
        DataSet ds = thepatientbill.PatientForReport(reg, Session["CoCode"].ToString().Trim());
        if (ds.Tables[0].Rows.Count > 0)
        {
            rpt.Append("<table width='100%' cellspacing=0 border=0 style='border:1px solid black;border-spacing: 0; padding: 0;font-family:verdana;'>");
            rpt.Append("<tr style='height:25px;'>");
            rpt.Append("<td style='font-family:Verdana; font-size:x-large;text-align:center'><b>Some of the critical situation of the patient about the risks and accept the Letter</b>");
            rpt.Append("</tr >");

            rpt.Append("<tr style='height:20px;'>");
            rpt.AppendFormat("<td style='font-family:Verdana; font-size:medium;text-align:center'>(Admitted By :{0}, Relation :{1})</td>", ds.Tables[0].Rows[0]["guardian_name"], ds.Tables[0].Rows[0]["relation"]);
            rpt.Append("</tr >");
            rpt.Append("</table>");

            PatientDetails_English(ds);
            rpt.Append("<br/>"); 




            rpt.Append("<table width='100%'>");
            rpt.Append("<tr style='height:25px'>"); rpt.Append("<u>");
            rpt.Append("<td style='font-family:Verdana;font-size:medium;font-weight:bold; text-align:left'>Problems or risk factors of the patient  : </td>"); rpt.Append("</u>");
            rpt.Append("</tr >");

            rpt.Append("<tr style='height:25px'>");
            rpt.Append("<td style='font-family:Verdana;font-size:small; text-align:left'>Young children, abnormally low weight, low power biography, deficient lungs, heart weak, other organs, immature, feeding problems, when the child's condition is worse prananata</td>");
            rpt.Append("</tr >");

            rpt.Append("<tr style='height:25px'>"); rpt.Append("<u>");
            rpt.Append("<td style='font-family:Verdana;font-size:medium;font-weight:bold;text-align:left'>Hazard that may occur in the patient  :</td>"); rpt.Append("</u>");
            rpt.Append("</tr >");

            rpt.Append("<tr style='height:25px'>");
            rpt.Append("<td style='font-family:Verdana;font-size:small; text-align:left'>Sepasisa inaphekasana or may be, may be shortness of breath, sudden hrdayanra could blink, the stomach may be swollen, may be worse when the child's condition, and may even die, that may go away at any moment</td>");
            rpt.Append("</tr >");
            rpt.Append("</table>");
            rpt.Append("<br/>");
            rpt.Append("<table width='100%'>");
            rpt.Append("<tr style='height:25px'>");
            rpt.Append("<td style='font-family:Verdana;font-size:small; text-align:justify'> The organization learned from the patient about these issues and concerns . This organization has repeatedly us from any government medical college or any other health organizations have been asked to take , we suggest that these institutions have agreed along . But now the situation is like taking away patients who do not have , so we are keeping in mind the patient's life is at risk , including hostile cikitsara emergency admissions for patients who have made this institution , the patient gave permission for the necessary treatment .  </td>");
            rpt.Append("</tr >");
            rpt.Append("</table>");
            rpt.Append("<br/>");
            rpt.Append("<table width='100%'>");
            rpt.Append("<tr style='height:25px'>");
            rpt.Append("<td style=' font-family:Verdana;font-size:small; text-align:justify'> We think it's healthy to talk with our kindred allowed to treat the patient during treatment, or in this case, the organization would be without the complicated error occurred when the patient or any other life-threatening if not, we'll blame the institution. </td>");
            rpt.Append("</tr >");
            rpt.Append("</table>");


            rpt.Append("<table width='100%'>");
            rpt.Append("<tr style='height:27px'>");
            rpt.Append("<td style='font-family:Verdana; font-size:medium; text-align:center'> Signature </td>");
            rpt.Append("<td style='font-family:Verdana; font-size:medium; text-align:center'> Full Name  </td>");
            rpt.Append("<td style='font-family:Verdana; font-size:medium; text-align:center'> Address </td>");
            rpt.Append("<td style='font-family:Verdana; font-size:medium; text-align:center'> Relation  </td>");
            rpt.Append("</tr >");

            rpt.Append("<tr style='height:27px'>");
            rpt.Append("<td style='font-family:Verdana;  font-size:small; text-align:center'>..................</td>");
            rpt.AppendFormat("<td style='font-family:Verdana;  font-size:small; text-align:center'>{0}</td>", ds.Tables[0].Rows[0]["patient_name"]);
            rpt.AppendFormat("<td style='font-family:Verdana; font-size:small; text-align:center'>{0}</td>", ds.Tables[0].Rows[0]["vill_city"]);
            rpt.Append("<td style='font-family:Verdana; font-size:small; text-align:center'> Self </td>");
            rpt.Append("</tr >");

            rpt.Append("<tr style='height:27px'>");
            rpt.Append("<td style='font-family:Verdana;  font-size:small; text-align:center'>..................</td>");
            rpt.AppendFormat("<td style='font-family:Verdana;  font-size:small; text-align:center'>{0}</td>", ds.Tables[0].Rows[0]["guardian_name"]);
            rpt.AppendFormat("<td style='font-family:Verdana; font-size:small; text-align:center'>{0}</td>", ds.Tables[0].Rows[0]["vill_city2"]);
            rpt.AppendFormat("<td style='font-family:Verdana; font-size:small; text-align:center'> {0} </td>", ds.Tables[0].Rows[0]["relation"]);
            rpt.Append("</tr >");
            rpt.Append("</table>"); 
        } 
        ltrReport.Visible = true;
    }

    public void GetHearder_Septiceamia(string reg)
    {
        ltrReport.Text = "";
        DataSet ds = thepatientbill.PatientForReport(reg, Session["CoCode"].ToString().Trim());
        if (ds.Tables[0].Rows.Count > 0)
        {
            rpt.Append("<table width='100%' cellspacing=0 border=0 style='border:1px solid black;border-spacing: 0; padding: 0;font-family:verdana;'>");
            rpt.Append("<tr style='height:25px;'>");
            rpt.Append("<td style='font-family:Verdana; font-size:x-large;text-align:center'><b>রোগীর বিশেষ কয়েকটি আশঙ্কাজনক পরিস্থিতি ও তার ঝুঁকি সম্পর্কে স্বীকার পত্র</b>");
            rpt.Append("</tr >");

            rpt.Append("<tr style='height:20px;'>");
            rpt.AppendFormat("<td style='font-family:Verdana; font-size:medium;text-align:center'>(যিনি ভর্তি করাচ্ছেন তার নাম:{0},সম্পর্ক:{1})</td>", ds.Tables[0].Rows[0]["guardian_name"], ds.Tables[0].Rows[0]["relation"]);
            rpt.Append("</tr >");
            rpt.Append("</table>");
            PatientDetails_Bengali(ds);
            rpt.Append("<br/>"); 

            rpt.Append("<table width='100%'>");
            rpt.Append("<tr style='height:25px'>"); rpt.Append("<u>");
            rpt.Append("<td style='font-family:Verdana;font-size:medium;font-weight:bold; text-align:left'>রোগীর সমস্যা বা ঝুঁকির কারণ সমূহ: </td>"); rpt.Append("</u>");
            rpt.Append("</tr >");

            rpt.Append("<tr style='height:35px'>");
            rpt.Append("<td style='font-family:Verdana;font-size:medium; text-align:left'>সারা শরীরে ইনফেকশন ছড়িযে গেছে,জীবনী শক্তি কম, ফুসফুস কমজোরী, হার্ট দূর্বল, অন্যান্য অঙ্গ ভাল কাজ করছে না, খাওয়ানোর সমস্যা, যখন তখন বাচ্চার অবস্থা খারাপ হওয়ার প্রনণতা</td>");
            rpt.Append("</tr >");

            rpt.Append("<tr style='height:35px'>"); rpt.Append("<u>");
            rpt.Append("<td style='font-family:Verdana;font-size:medium;font-weight:bold;text-align:left'>ওই সমস্ত রোগীর যে সমস্ত বিপত্তি ঘটতে পারে :</td>"); rpt.Append("</u>");
            rpt.Append("</tr >");

            rpt.Append("<tr style='height:35px'>");
            rpt.Append("<td style='font-family:Verdana;font-size:medium; text-align:left'>শ্বাসকষ্ট হতে পারে, হঠাৎ হৃদযন্র বিকল হতে পারে, পেট ফুলে যেতে পারে, যখন তখন বাচ্চার অবস্থা খারাপ হতে পারে, এমন কি মারা ও যেতে পারে,এই রোগের চিকিৎসার কোন গ্যারান্টী নেই, লিভার, কিডনি, ফুসফুস অকেজো হতে পারে, যে কোন মুহূর্তে অন্যত্র নিয়ে যেতে হতে পারে</td>");
            rpt.Append("</tr >");
            rpt.Append("</table>");
            rpt.Append("<br/>");
            rpt.Append("<table width='100%'>");
            rpt.Append("<tr style='height:30px'>");
            rpt.Append("<td style='font-family:Verdana;font-size:medium; text-align:justify'>এই প্রতিষ্ঠানের পক্ষ থেকে রোগীর এই সমস্ত সমস্যা ও আশঙ্কার কথা জানলাম । এই প্রতিষ্ঠানের পক্ষ থেকে আমাদের বারবার কোন সরকারী মেডিকেল কলেজ কিংবা অন্য কোনও স্বাস্থ্য প্রতিষ্ঠানের নিয়ে যাবার কথা বলা হয়েছে , আমরাও এই প্রতিষ্ঠান যে সমস্ত পরামর্শ দিয়েছে তারসঙ্গে একমত । কিন্তু এই মূহুর্তে রোগীকে অন্যত্র নিয়ে যাবার মতো পরিস্থিতি আমাদের নেই , তাই রোগীর জীবন হানী সহ সমস্ত ঝুঁকির কথা মাথায় রেখেই আমরা আপদকালীন চিকিৎ্সার জন্য রোগীকে এই প্রতিষ্ঠানে ভর্তি করলাম , এখানে রোগীর প্রয়োজনীয় যাবতীয় চিকিত্সা করার জন্য অনুমতি দিলাম । </td>");
            rpt.Append("</tr >");
            rpt.Append("</table>");
            rpt.Append("<br/>");
            rpt.Append("<table width='100%'>");
            rpt.Append("<tr style='height:30px'>");
            rpt.Append("<td style=' font-family:Verdana;font-size:medium; text-align:justify'>আমরা যেহেতু সুস্থ মনে আমাদের আত্মীয় পরিজনদের সঙ্গে আলোচনা করে রোগীর চিকিত্সা চালানোর অনুমতি দিচ্ছি সেক্ষেত্রে চিকিত্সা চলাকালীন কিংবা এই প্রতিষ্ঠান থেকে ছাড়া পাওয়ার পর রোগীর অন্য কোন জটিল সম্যসা হলে কিংবা জীবন সংশয় হলে আমরা এই প্রতিষ্ঠানকে দায়ী করব না ।</td>");
            rpt.Append("</tr >");
            rpt.Append("</table>");

            rpt.Append("<table width='100%'>");
            rpt.Append("<tr style='height:35px'>");
            rpt.Append("<td style='font-family:Verdana; font-size:medium; text-align:center'> স্বাক্ষর/টিপ </td>");
            rpt.Append("<td style='font-family:Verdana; font-size:medium; text-align:center'> পুরো  নাম  </td>");
            rpt.Append("<td style='font-family:Verdana; font-size:medium; text-align:center'> ঠিকানা </td>");
            rpt.Append("<td style='font-family:Verdana; font-size:medium; text-align:center'> সম্পর্ক  </td>");
            rpt.Append("</tr >");

            rpt.Append("<tr style='height:27px'>");
            rpt.Append("<td style='font-family:Verdana;  font-size:small; text-align:center'>..................</td>");
            rpt.AppendFormat("<td style='font-family:Verdana;  font-size:small; text-align:center'>{0}</td>", ds.Tables[0].Rows[0]["patient_name"]);
            rpt.AppendFormat("<td style='font-family:Verdana; font-size:small; text-align:center'>{0}</td>", ds.Tables[0].Rows[0]["vill_city"]);
            rpt.Append("<td style='font-family:Verdana; font-size:small; text-align:center'> নিজে </td>");
            rpt.Append("</tr >");

            rpt.Append("<tr style='height:27px'>");
            rpt.Append("<td style='font-family:Verdana;  font-size:small; text-align:center'>..................</td>");
            rpt.AppendFormat("<td style='font-family:Verdana;  font-size:small; text-align:center'>{0}</td>", ds.Tables[0].Rows[0]["guardian_name"]);
            rpt.AppendFormat("<td style='font-family:Verdana; font-size:small; text-align:center'>{0}</td>", ds.Tables[0].Rows[0]["vill_city2"]);
            rpt.AppendFormat("<td style='font-family:Verdana; font-size:small; text-align:center'> {0} </td>", ds.Tables[0].Rows[0]["relation"]);
            rpt.Append("</tr >");
            rpt.Append("</table>"); 
        } 
        ltrReport.Visible = true;
    }

    public void GetHearder_Septiceamia1(string reg)
    {
        ltrReport.Text = "";
        DataSet ds = thepatientbill.PatientForReport(reg, Session["CoCode"].ToString().Trim());
        if (ds.Tables[0].Rows.Count > 0)
        {
            rpt.Append("<table width='100%' cellspacing=0 border=0 style='border:1px solid black;border-spacing: 0; padding: 0;font-family:verdana;'>");
            rpt.Append("<tr style='height:25px;'>");
            rpt.Append("<td style='font-family:Verdana; font-size:x-large;text-align:center'><b>Some of the critical situation of the patient about the risks and accept the Letter</b>");
            rpt.Append("</tr >");

            rpt.Append("<tr style='height:20px;'>");
            rpt.AppendFormat("<td style='font-family:Verdana; font-size:medium;text-align:center'>(Admitted By :{0}, Relation :{1})</td>", ds.Tables[0].Rows[0]["guardian_name"], ds.Tables[0].Rows[0]["relation"]);
            rpt.Append("</tr >");
            rpt.Append("</table>");
            PatientDetails_English(ds);
            rpt.Append("<br/>"); 


            rpt.Append("<table width='100%'>");
            rpt.Append("<tr style='height:25px'>"); rpt.Append("<u>");
            rpt.Append("<td style='font-family:Verdana;font-size:medium;font-weight:bold; text-align:left'>Problems or risk factors of the patient  : </td>"); rpt.Append("</u>");
            rpt.Append("</tr >");

            rpt.Append("<tr style='height:25px'>");
            rpt.Append("<td style='font-family:Verdana;font-size:small; text-align:left'>infections in the body of bush telegraph, the biography of the low, deficient lungs, heart weak, does not work well in other organs, feeding problems, when the child's condition is worse prananata</td>");
            rpt.Append("</tr >");

            rpt.Append("<tr style='height:25px'>"); rpt.Append("<u>");
            rpt.Append("<td style='font-family:Verdana;font-size:medium;font-weight:bold;text-align:left'>Hazard that may occur in the patient  :</td>"); rpt.Append("</u>");
            rpt.Append("</tr >");

            rpt.Append("<tr style='height:25px'>");
            rpt.Append("<td style='font-family:Verdana;font-size:small; text-align:left'>Shortness of breath may be, may suddenly blink hrdayanra, stomach swelling can be worse when the child's condition, and may even die, there are no guarantees the treatment of this disease, the liver, the kidneys, the lungs may be unhelpful, at any moment Elsewhere may be taken</td>");
            rpt.Append("</tr >");
            rpt.Append("</table>");
            rpt.Append("<br/>");
            rpt.Append("<table width='100%'>");
            rpt.Append("<tr style='height:25px'>");
            rpt.Append("<td style='font-family:Verdana;font-size:small; text-align:justify'> The organization learned from the patient about these issues and concerns . This organization has repeatedly us from any government medical college or any other health organizations have been asked to take , we suggest that these institutions have agreed along . But now the situation is like taking away patients who do not have , so we are keeping in mind the patient's life is at risk , including hostile cikitsara emergency admissions for patients who have made this institution , the patient gave permission for the necessary treatment .  </td>");
            rpt.Append("</tr >");
            rpt.Append("</table>");
            rpt.Append("<br/>");
            rpt.Append("<table width='100%'>");
            rpt.Append("<tr style='height:25px'>");
            rpt.Append("<td style=' font-family:Verdana;font-size:small; text-align:justify'> We think it's healthy to talk with our kindred allowed to treat the patient during treatment, or in this case, the organization would be without the complicated error occurred when the patient or any other life-threatening if not, we'll blame the institution. </td>");
            rpt.Append("</tr >");
            rpt.Append("</table>");


            rpt.Append("<table width='100%'>");
            rpt.Append("<tr style='height:27px'>");
            rpt.Append("<td style='font-family:Verdana; font-size:medium; text-align:center'> Signature </td>");
            rpt.Append("<td style='font-family:Verdana; font-size:medium; text-align:center'> Full Name  </td>");
            rpt.Append("<td style='font-family:Verdana; font-size:medium; text-align:center'> Address </td>");
            rpt.Append("<td style='font-family:Verdana; font-size:medium; text-align:center'> Relation  </td>");
            rpt.Append("</tr >");

            rpt.Append("<tr style='height:27px'>");
            rpt.Append("<td style='font-family:Verdana;  font-size:small; text-align:center'>..................</td>");
            rpt.AppendFormat("<td style='font-family:Verdana;  font-size:small; text-align:center'>{0}</td>", ds.Tables[0].Rows[0]["patient_name"]);
            rpt.AppendFormat("<td style='font-family:Verdana; font-size:small; text-align:center'>{0}</td>", ds.Tables[0].Rows[0]["vill_city"]);
            rpt.Append("<td style='font-family:Verdana; font-size:small; text-align:center'> Self </td>");
            rpt.Append("</tr >");

            rpt.Append("<tr style='height:27px'>");
            rpt.Append("<td style='font-family:Verdana;  font-size:small; text-align:center'>..................</td>");
            rpt.AppendFormat("<td style='font-family:Verdana;  font-size:small; text-align:center'>{0}</td>", ds.Tables[0].Rows[0]["guardian_name"]);
            rpt.AppendFormat("<td style='font-family:Verdana; font-size:small; text-align:center'>{0}</td>", ds.Tables[0].Rows[0]["vill_city"]);
            rpt.AppendFormat("<td style='font-family:Verdana; font-size:small; text-align:center'> {0} </td>", ds.Tables[0].Rows[0]["relation"]);
            rpt.Append("</tr >");
            rpt.Append("</table>"); 
        } 
        ltrReport.Visible = true;
    }

    public void GetHearder_ProlongAnesthecia(string reg)
    {
        ltrReport.Text = "";
        DataSet ds = thepatientbill.PatientForReport(reg, Session["CoCode"].ToString().Trim());
        if (ds.Tables[0].Rows.Count > 0)
        {
            rpt.Append("<table width='100%' cellspacing=0 border=0 style='border:1px solid black;border-spacing: 0; padding: 0;font-family:verdana;'>");
            rpt.Append("<tr style='height:25px;'>");
            rpt.Append("<td style='font-family:Verdana; font-size:x-large;text-align:center'><b>রোগীর বিশেষ কয়েকটি আশঙ্কাজনক পরিস্থিতি ও তার ঝুঁকি সম্পর্কে স্বীকার পত্র</b>");
            rpt.Append("</tr >");

            rpt.Append("<tr style='height:20px;'>");
            rpt.AppendFormat("<td style='font-family:Verdana; font-size:medium;text-align:center'>(যিনি ভর্তি করাচ্ছেন তার নাম:{0},সম্পর্ক:{1})</td>", ds.Tables[0].Rows[0]["guardian_name"], ds.Tables[0].Rows[0]["relation"]);
            rpt.Append("</tr >");
            rpt.Append("</table>");

            PatientDetails_Bengali(ds);
            rpt.Append("<br/>"); 

            rpt.Append("<table width='100%'>");
            rpt.Append("<tr style='height:25px'>"); rpt.Append("<u>");
            rpt.Append("<td style='font-family:Verdana;font-size:medium;font-weight:bold; text-align:left'>রোগীর সমস্যা বা ঝুঁকির কারণ সমূহ: </td>"); rpt.Append("</u>");
            rpt.Append("</tr >");

            rpt.Append("<tr style='height:35px'>");
            rpt.Append("<td style='font-family:Verdana;font-size:medium; text-align:left'> অপারেশনের পর রোগীর জ্ঞান ফিরছে না, কোন রোগীর এই অবস্থা ঘটবে তা আগে থেকে জানা বা বোঝা সম্ভব নয় তাই প্রতিষেধক নেওয়াও অসম্ভব,ডাঃবাবুরা প্রাণপণ চেষ্টা চালাচ্ছেন কিন্তু পরিশেষে কি হবে বলা যাবে না, এখন যন্ত্রের সাহায্যে শ্বাস প্রশ্বাস চালানো হচ্ছে এবং এই মূহূর্তে অন্য জায়গায় স্থানান্তরিত করাও মুশকিল</td>");
            rpt.Append("</tr >");

            rpt.Append("<tr style='height:35px'>"); rpt.Append("<u>");
            rpt.Append("<td style='font-family:Verdana;font-size:medium;font-weight:bold;text-align:left'>ওই সমস্ত রোগীর যে সমস্ত বিপত্তি ঘটতে পারে :</td>"); rpt.Append("</u>");
            rpt.Append("</tr >");

            rpt.Append("<tr style='height:35px'>");
            rpt.Append("<td style='font-family:Verdana;font-size:medium; text-align:left'> আর জ্ঞান না ফিরতে পারে, প্যারালেসিস হতে পারে, শ্বাসকষ্ট হতে পারে, ব্রেনে রক্তপাত হতে পারে, হঠাৎ হৃদযন্র বিকল হতে পারে, পেট ফুলে যেতে পারে,ইনফেকসন বা সেপশিস হতে পারে, যখন তখন রোগীর অবস্থা আরও খারাপ হতে পারে, এমন কি মারা ও যেতে পারে, যে কোন মুহূর্তে অন্যত্র (যদি সম্ভব হয়) নিয়ে যেতে হতে পারে,এই চিকিৎসার কোন গ্যারান্টী নেই </td>");
            rpt.Append("</tr >");
            rpt.Append("</table>");
            rpt.Append("<br/>");
            rpt.Append("<table width='100%'>");
            rpt.Append("<tr style='height:30px'>");
            rpt.Append("<td style='font-family:Verdana;font-size:medium; text-align:justify'>এই প্রতিষ্ঠানের পক্ষ থেকে রোগীর এই সমস্ত সমস্যা ও আশঙ্কার কথা জানলাম । এই প্রতিষ্ঠানের পক্ষ থেকে আমাদের বারবার কোন সরকারী মেডিকেল কলেজ কিংবা অন্য কোনও স্বাস্থ্য প্রতিষ্ঠানের নিয়ে যাবার কথা বলা হয়েছে , আমরাও এই প্রতিষ্ঠান যে সমস্ত পরামর্শ দিয়েছে তারসঙ্গে একমত । কিন্তু এই মূহুর্তে রোগীকে অন্যত্র নিয়ে যাবার মতো পরিস্থিতি আমাদের নেই , তাই রোগীর জীবন হানী সহ সমস্ত ঝুঁকির কথা মাথায় রেখেই আমরা আপদকালীন চিকিৎ্সার জন্য রোগীকে এই প্রতিষ্ঠানে ভর্তি করলাম , এখানে রোগীর প্রয়োজনীয় যাবতীয় চিকিত্সা করার জন্য অনুমতি দিলাম । </td>");
            rpt.Append("</tr >");
            rpt.Append("</table>");
            rpt.Append("<br/>");
            rpt.Append("<table width='100%'>");
            rpt.Append("<tr style='height:30px'>");
            rpt.Append("<td style=' font-family:Verdana;font-size:medium; text-align:justify'>আমরা যেহেতু সুস্থ মনে আমাদের আত্মীয় পরিজনদের সঙ্গে আলোচনা করে রোগীর চিকিত্সা চালানোর অনুমতি দিচ্ছি সেক্ষেত্রে চিকিত্সা চলাকালীন কিংবা এই প্রতিষ্ঠান থেকে ছাড়া পাওয়ার পর রোগীর অন্য কোন জটিল সম্যসা হলে কিংবা জীবন সংশয় হলে আমরা এই প্রতিষ্ঠানকে দায়ী করব না ।</td>");
            rpt.Append("</tr >");
            rpt.Append("</table>");




            rpt.Append("<table width='100%'>");
            rpt.Append("<tr style='height:35px'>");
            rpt.Append("<td style='font-family:Verdana; font-size:medium; text-align:center'> স্বাক্ষর/টিপ </td>");
            rpt.Append("<td style='font-family:Verdana; font-size:medium; text-align:center'> পুরো  নাম  </td>");
            rpt.Append("<td style='font-family:Verdana; font-size:medium; text-align:center'> ঠিকানা </td>");
            rpt.Append("<td style='font-family:Verdana; font-size:medium; text-align:center'> সম্পর্ক  </td>");
            rpt.Append("</tr >");

            rpt.Append("<tr style='height:27px'>");
            rpt.Append("<td style='font-family:Verdana;  font-size:small; text-align:center'>..................</td>");
            rpt.AppendFormat("<td style='font-family:Verdana;  font-size:small; text-align:center'>{0}</td>", ds.Tables[0].Rows[0]["patient_name"]);
            rpt.AppendFormat("<td style='font-family:Verdana; font-size:small; text-align:center'>{0}</td>", ds.Tables[0].Rows[0]["vill_city"]);
            rpt.Append("<td style='font-family:Verdana; font-size:small; text-align:center'> নিজে </td>");
            rpt.Append("</tr >");

            rpt.Append("<tr style='height:27px'>");
            rpt.Append("<td style='font-family:Verdana;  font-size:small; text-align:center'>..................</td>");
            rpt.AppendFormat("<td style='font-family:Verdana;  font-size:small; text-align:center'>{0}</td>", ds.Tables[0].Rows[0]["guardian_name"]);
            rpt.AppendFormat("<td style='font-family:Verdana; font-size:small; text-align:center'>{0}</td>", ds.Tables[0].Rows[0]["vill_city2"]);
            rpt.AppendFormat("<td style='font-family:Verdana; font-size:small; text-align:center'> {0} </td>", ds.Tables[0].Rows[0]["relation"]);
            rpt.Append("</tr >");
            rpt.Append("</table>"); 
        } 
        ltrReport.Visible = true;
    }

    public void GetHearder_ProlongAnesthecia1(string reg)
    {
        ltrReport.Text = "";
        DataSet ds = thepatientbill.PatientForReport(reg, Session["CoCode"].ToString().Trim());
        if (ds.Tables[0].Rows.Count > 0)
        {
            rpt.Append("<table width='100%' cellspacing=0 border=0 style='border:1px solid black;border-spacing: 0; padding: 0;font-family:verdana;'>");
            rpt.Append("<tr style='height:25px;'>");
            rpt.Append("<td style='font-family:Verdana; font-size:x-large;text-align:center'><b>Some of the critical situation of the patient about the risks and accept the Letter</b>");
            rpt.Append("</tr >");

            rpt.Append("<tr style='height:20px;'>");
            rpt.AppendFormat("<td style='font-family:Verdana; font-size:medium;text-align:center'>(Admitted By :{0}, Relation :{1})</td>", ds.Tables[0].Rows[0]["guardian_name"], ds.Tables[0].Rows[0]["relation"]);
            rpt.Append("</tr >");
            rpt.Append("</table>");
            PatientDetails_English(ds);
            rpt.Append("<br/>"); 

            rpt.Append("<table width='100%'>");
            rpt.Append("<tr style='height:25px'>"); rpt.Append("<u>");
            rpt.Append("<td style='font-family:Verdana;font-size:medium;font-weight:bold; text-align:left'>Problems or risk factors of the patient  : </td>"); rpt.Append("</u>");
            rpt.Append("</tr >");

            rpt.Append("<tr style='height:25px'>");
            rpt.Append("<td style='font-family:Verdana;font-size:small; text-align:left'>Coming after the operation the patient's knowledge, no patient in this situation before it happens not to know or understand so it is impossible to preventive, dahbabura assail running but will finally be told what to do, now the machine is running, and this time moved breathe It is also difficult</td>");
            rpt.Append("</tr >");

            rpt.Append("<tr style='height:25px'>"); rpt.Append("<u>");
            rpt.Append("<td style='font-family:Verdana;font-size:medium;font-weight:bold;text-align:left'>Hazard that may occur in the patient  :</td>"); rpt.Append("</u>");
            rpt.Append("</tr >");

            rpt.Append("<tr style='height:25px'>");
            rpt.Append("<td style='font-family:Verdana;font-size:small; text-align:left'> Returns to the knowledge, may be pyaralesisa, shortness of breath may be brene bleeding, sudden hrdayanra can crash, the stomach may be swollen, may sepasisa inaphekasana or when the patient's condition could be worse, and even to die may, at any time elsewhere (if possible) may be taken, there is no guarantee this treatment </td>");
            rpt.Append("</tr >");
            rpt.Append("</table>");
            rpt.Append("<br/>");
            rpt.Append("<table width='100%'>");
            rpt.Append("<tr style='height:25px'>");
            rpt.Append("<td style='font-family:Verdana;font-size:small; text-align:justify'> The organization learned from the patient about these issues and concerns . This organization has repeatedly us from any government medical college or any other health organizations have been asked to take , we suggest that these institutions have agreed along . But now the situation is like taking away patients who do not have , so we are keeping in mind the patient's life is at risk , including hostile cikitsara emergency admissions for patients who have made this institution , the patient gave permission for the necessary treatment .  </td>");
            rpt.Append("</tr >");
            rpt.Append("</table>");
            rpt.Append("<br/>");
            rpt.Append("<table width='100%'>");
            rpt.Append("<tr style='height:25px'>");
            rpt.Append("<td style=' font-family:Verdana;font-size:small; text-align:justify'> We think it's healthy to talk with our kindred allowed to treat the patient during treatment, or in this case, the organization would be without the complicated error occurred when the patient or any other life-threatening if not, we'll blame the institution. </td>");
            rpt.Append("</tr >");
            rpt.Append("</table>");


            rpt.Append("<table width='100%'>");
            rpt.Append("<tr style='height:27px'>");
            rpt.Append("<td style='font-family:Verdana; font-size:medium; text-align:center'> Signature </td>");
            rpt.Append("<td style='font-family:Verdana; font-size:medium; text-align:center'> Full Name  </td>");
            rpt.Append("<td style='font-family:Verdana; font-size:medium; text-align:center'> Address </td>");
            rpt.Append("<td style='font-family:Verdana; font-size:medium; text-align:center'> Relation  </td>");
            rpt.Append("</tr >");

            rpt.Append("<tr style='height:27px'>");
            rpt.Append("<td style='font-family:Verdana;  font-size:small; text-align:center'>..................</td>");
            rpt.AppendFormat("<td style='font-family:Verdana;  font-size:small; text-align:center'>{0}</td>", ds.Tables[0].Rows[0]["patient_name"]);
            rpt.AppendFormat("<td style='font-family:Verdana; font-size:small; text-align:center'>{0}</td>", ds.Tables[0].Rows[0]["vill_city"]);
            rpt.Append("<td style='font-family:Verdana; font-size:small; text-align:center'> Self </td>");
            rpt.Append("</tr >");

            rpt.Append("<tr style='height:27px'>");
            rpt.Append("<td style='font-family:Verdana;  font-size:small; text-align:center'>..................</td>");
            rpt.AppendFormat("<td style='font-family:Verdana;  font-size:small; text-align:center'>{0}</td>", ds.Tables[0].Rows[0]["guardian_name"]);
            rpt.AppendFormat("<td style='font-family:Verdana; font-size:small; text-align:center'>{0}</td>", ds.Tables[0].Rows[0]["vill_city"]);
            rpt.AppendFormat("<td style='font-family:Verdana; font-size:small; text-align:center'> {0} </td>", ds.Tables[0].Rows[0]["relation"]);
            rpt.Append("</tr >");
            rpt.Append("</table>"); 
        }
        rpt.Append("</div>");
        ltrReport.Visible = true;
    }

    public void GetHearder_SevererAnemiaREport(string reg)
    {
        ltrReport.Text = "";
        DataSet ds = thepatientbill.PatientForReport(reg, Session["CoCode"].ToString().Trim());
        if (ds.Tables[0].Rows.Count > 0)
        {
            rpt.Append("<table width='100%' cellspacing=0 border=0 style='border:1px solid black;border-spacing: 0; padding: 0;font-family:verdana;'>");
            rpt.Append("<tr style='height:25px;'>");
            rpt.Append("<td style='font-family:Verdana; font-size:x-large;text-align:center'><b>রোগীর বিশেষ কয়েকটি আশঙ্কাজনক পরিস্থিতি ও তার ঝুঁকি সম্পর্কে স্বীকার পত্র</b>");
            rpt.Append("</tr >");

            rpt.Append("<tr style='height:20px;'>");
            rpt.AppendFormat("<td style='font-family:Verdana; font-size:medium;text-align:center'>(যিনি ভর্তি করাচ্ছেন তার নাম:{0},সম্পর্ক:{1})</td>", ds.Tables[0].Rows[0]["guardian_name"], ds.Tables[0].Rows[0]["relation"]);
            rpt.Append("</tr >");
            rpt.Append("</table>");

            PatientDetails_Bengali(ds);
            rpt.Append("<br/>"); 

            rpt.Append("<table width='100%'>");
            rpt.Append("<tr style='height:25px'>"); rpt.Append("<u>");
            rpt.Append("<td style='font-family:Verdana;font-size:medium;font-weight:bold; text-align:left'>রোগীর সমস্যা বা ঝুঁকির কারণ সমূহ: </td>"); rpt.Append("</u>");
            rpt.Append("</tr >");

            rpt.Append("<tr style='height:35px'>");
            rpt.Append("<td style='font-family:Verdana;font-size:medium; text-align:left'> রোগীর দেহে রক্ত খুব কম,জীবনী শক্তি কম, ফুসফুস কমজোরী, হার্ট দূর্বল , অন্যান্য অঙ্গ ভাল কাজ করছে না, এত রক্ত রোগীকে দেওয়ার সমস্যা ও কিডনির সমস্যা আসতে পারে</td>");
            rpt.Append("</tr >");

            rpt.Append("<tr style='height:35px'>"); rpt.Append("<u>");
            rpt.Append("<td style='font-family:Verdana;font-size:medium;font-weight:bold;text-align:left'>ওই সমস্ত রোগীর যে সমস্ত বিপত্তি ঘটতে পারে :</td>"); rpt.Append("</u>");
            rpt.Append("</tr >");

            rpt.Append("<tr style='height:35px'>");
            rpt.Append("<td style='font-family:Verdana;font-size:medium; text-align:left'> এখুনি প্রচুর রক্ত লাগবে, এই রক্ত জোগাড় করার দায়িত্ব রোগীর বাড়ীর লোককেই নিতে হবে, রক্ত জোগাড় করতে যত দেরি হবে রোগীর বিপদ তত বাড়বে আর রক্ত না পেলে চিকিৎসা চালিয়ে যাওয়া অসম্ভব, রক্ত কম থাকার জন্য কিডনি ও হার্ট ফেল করতে পারে,আর যদি অপারেশনের দরকার হয় রক্ত না পেলে অপারেশন করা প্রায় অসম্ভব</td>");
            rpt.Append("</tr >");
            rpt.Append("</table>");
            rpt.Append("<br/>");
            rpt.Append("<table width='100%'>");
            rpt.Append("<tr style='height:30px'>");
            rpt.Append("<td style='font-family:Verdana;font-size:medium; text-align:justify'>এই প্রতিষ্ঠানের পক্ষ থেকে রোগীর এই সমস্ত সমস্যা ও আশঙ্কার কথা জানলাম । এই প্রতিষ্ঠানের পক্ষ থেকে আমাদের বারবার কোন সরকারী মেডিকেল কলেজ কিংবা অন্য কোনও স্বাস্থ্য প্রতিষ্ঠানের নিয়ে যাবার কথা বলা হয়েছে , আমরাও এই প্রতিষ্ঠান যে সমস্ত পরামর্শ দিয়েছে তারসঙ্গে একমত । কিন্তু এই মূহুর্তে রোগীকে অন্যত্র নিয়ে যাবার মতো পরিস্থিতি আমাদের নেই , তাই রোগীর জীবন হানী সহ সমস্ত ঝুঁকির কথা মাথায় রেখেই আমরা আপদকালীন চিকিৎ্সার জন্য রোগীকে এই প্রতিষ্ঠানে ভর্তি করলাম , এখানে রোগীর প্রয়োজনীয় যাবতীয় চিকিত্সা করার জন্য অনুমতি দিলাম । </td>");
            rpt.Append("</tr >");
            rpt.Append("</table>");
            rpt.Append("<br/>");
            rpt.Append("<table width='100%'>");
            rpt.Append("<tr style='height:30px'>");
            rpt.Append("<td style=' font-family:Verdana;font-size:medium; text-align:justify'>আমরা যেহেতু সুস্থ মনে আমাদের আত্মীয় পরিজনদের সঙ্গে আলোচনা করে রোগীর চিকিত্সা চালানোর অনুমতি দিচ্ছি সেক্ষেত্রে চিকিত্সা চলাকালীন কিংবা এই প্রতিষ্ঠান থেকে ছাড়া পাওয়ার পর রোগীর অন্য কোন জটিল সম্যসা হলে কিংবা জীবন সংশয় হলে আমরা এই প্রতিষ্ঠানকে দায়ী করব না ।</td>");
            rpt.Append("</tr >");
            rpt.Append("</table>");




            rpt.Append("<table width='100%'>");
            rpt.Append("<tr style='height:35px'>");
            rpt.Append("<td style='font-family:Verdana; font-size:medium; text-align:center'> স্বাক্ষর/টিপ </td>");
            rpt.Append("<td style='font-family:Verdana; font-size:medium; text-align:center'> পুরো  নাম  </td>");
            rpt.Append("<td style='font-family:Verdana; font-size:medium; text-align:center'> ঠিকানা </td>");
            rpt.Append("<td style='font-family:Verdana; font-size:medium; text-align:center'> সম্পর্ক  </td>");
            rpt.Append("</tr >");

            rpt.Append("<tr style='height:27px'>");
            rpt.Append("<td style='font-family:Verdana;  font-size:small; text-align:center'>..................</td>");
            rpt.AppendFormat("<td style='font-family:Verdana;  font-size:small; text-align:center'>{0}</td>", ds.Tables[0].Rows[0]["patient_name"]);
            rpt.AppendFormat("<td style='font-family:Verdana; font-size:small; text-align:center'>{0}</td>", ds.Tables[0].Rows[0]["vill_city"]);
            rpt.Append("<td style='font-family:Verdana; font-size:small; text-align:center'> নিজে </td>");
            rpt.Append("</tr >");

            rpt.Append("<tr style='height:27px'>");
            rpt.Append("<td style='font-family:Verdana;  font-size:small; text-align:center'>..................</td>");
            rpt.AppendFormat("<td style='font-family:Verdana;  font-size:small; text-align:center'>{0}</td>", ds.Tables[0].Rows[0]["guardian_name"]);
            rpt.AppendFormat("<td style='font-family:Verdana; font-size:small; text-align:center'>{0}</td>", ds.Tables[0].Rows[0]["vill_city"]);
            rpt.AppendFormat("<td style='font-family:Verdana; font-size:small; text-align:center'> {0} </td>", ds.Tables[0].Rows[0]["relation"]);
            rpt.Append("</tr >");
            rpt.Append("</table>"); 
        }
        rpt.Append("</div>");
        ltrReport.Visible = true;
    }

    public void GetHearder_SevererAnemiaREport1(string reg)
    {
        ltrReport.Text = "";
        DataSet ds = thepatientbill.PatientForReport(reg, Session["CoCode"].ToString().Trim());
        if (ds.Tables[0].Rows.Count > 0)
        {
            rpt.Append("<table width='100%' cellspacing=0 border=0 style='border:1px solid black;border-spacing: 0; padding: 0;font-family:verdana;'>");
            rpt.Append("<tr style='height:25px;'>");
            rpt.Append("<td style='font-family:Verdana; font-size:x-large;text-align:center'><b>Some of the critical situation of the patient about the risks and accept the Letter</b>");
            rpt.Append("</tr >");

            rpt.Append("<tr style='height:20px;'>");
            rpt.AppendFormat("<td style='font-family:Verdana; font-size:medium;text-align:center'>(Admitted By :{0}, Relation :{1})</td>", ds.Tables[0].Rows[0]["guardian_name"], ds.Tables[0].Rows[0]["relation"]);
            rpt.Append("</tr >");
            rpt.Append("</table>");

            PatientDetails_English(ds);
            rpt.Append("<br/>"); 

            rpt.Append("<table width='100%'>");
            rpt.Append("<tr style='height:25px'>"); rpt.Append("<u>");
            rpt.Append("<td style='font-family:Verdana;font-size:medium;font-weight:bold; text-align:left'>Problems or risk factors of the patient  : </td>"); rpt.Append("</u>");
            rpt.Append("</tr >");

            rpt.Append("<tr style='height:25px'>");
            rpt.Append("<td style='font-family:Verdana;font-size:small; text-align:left'>Patients in the blood is too low, the biography of the low, deficient lungs, heart weak, other organs are not working well, so the blood of patient problems and kidney problems may come</td>");
            rpt.Append("</tr >");

            rpt.Append("<tr style='height:25px'>"); rpt.Append("<u>");
            rpt.Append("<td style='font-family:Verdana;font-size:medium;font-weight:bold;text-align:left'>Hazard that may occur in the patient  :</td>"); rpt.Append("</u>");
            rpt.Append("</tr >");

            rpt.Append("<tr style='height:25px'>");
            rpt.Append("<td style='font-family:Verdana;font-size:small; text-align:left'>Take away a lot of blood, the blood of the patient's home, the children will take the responsibility to obtain, to get the blood and blood increase the degree of danger will not be delayed if the patient is physically impossible, for having low blood kidney and heart may fail, and you need operations operation unless the blood is almost impossible</td>");
            rpt.Append("</tr >");
            rpt.Append("</table>");
            rpt.Append("<br/>");
            rpt.Append("<table width='100%'>");
            rpt.Append("<tr style='height:25px'>");
            rpt.Append("<td style='font-family:Verdana;font-size:small; text-align:justify'> The organization learned from the patient about these issues and concerns . This organization has repeatedly us from any government medical college or any other health organizations have been asked to take , we suggest that these institutions have agreed along . But now the situation is like taking away patients who do not have , so we are keeping in mind the patient's life is at risk , including hostile cikitsara emergency admissions for patients who have made this institution , the patient gave permission for the necessary treatment .  </td>");
            rpt.Append("</tr >");
            rpt.Append("</table>");
            rpt.Append("<br/>");
            rpt.Append("<table width='100%'>");
            rpt.Append("<tr style='height:25px'>");
            rpt.Append("<td style=' font-family:Verdana;font-size:small; text-align:justify'> We think it's healthy to talk with our kindred allowed to treat the patient during treatment, or in this case, the organization would be without the complicated error occurred when the patient or any other life-threatening if not, we'll blame the institution. </td>");
            rpt.Append("</tr >");
            rpt.Append("</table>");


            rpt.Append("<table width='100%'>");
            rpt.Append("<tr style='height:27px'>");
            rpt.Append("<td style='font-family:Verdana; font-size:medium; text-align:center'> Signature </td>");
            rpt.Append("<td style='font-family:Verdana; font-size:medium; text-align:center'> Full Name  </td>");
            rpt.Append("<td style='font-family:Verdana; font-size:medium; text-align:center'> Address </td>");
            rpt.Append("<td style='font-family:Verdana; font-size:medium; text-align:center'> Relation  </td>");
            rpt.Append("</tr >");

            rpt.Append("<tr style='height:27px'>");
            rpt.Append("<td style='font-family:Verdana;  font-size:small; text-align:center'>..................</td>");
            rpt.AppendFormat("<td style='font-family:Verdana;  font-size:small; text-align:center'>{0}</td>", ds.Tables[0].Rows[0]["patient_name"]);
            rpt.AppendFormat("<td style='font-family:Verdana; font-size:small; text-align:center'>{0}</td>", ds.Tables[0].Rows[0]["vill_city"]);
            rpt.Append("<td style='font-family:Verdana; font-size:small; text-align:center'> Self </td>");
            rpt.Append("</tr >");

            rpt.Append("<tr style='height:27px'>");
            rpt.Append("<td style='font-family:Verdana;  font-size:small; text-align:center'>..................</td>");
            rpt.AppendFormat("<td style='font-family:Verdana;  font-size:small; text-align:center'>{0}</td>", ds.Tables[0].Rows[0]["guardian_name"]);
            rpt.AppendFormat("<td style='font-family:Verdana; font-size:small; text-align:center'>{0}</td>", ds.Tables[0].Rows[0]["vill_city2"]);
            rpt.AppendFormat("<td style='font-family:Verdana; font-size:small; text-align:center'> {0} </td>", ds.Tables[0].Rows[0]["relation"]);
            rpt.Append("</tr >");
            rpt.Append("</table>"); 
        } 
        ltrReport.Visible = true;
    }

    public void GetHearder_StrokeReport(string reg)
    {
        ltrReport.Text = "";
        DataSet ds = thepatientbill.PatientForReport(reg, Session["CoCode"].ToString().Trim());
        if (ds.Tables[0].Rows.Count > 0)
        {
            rpt.Append("<table width='100%' cellspacing=0 border=0 style='border:1px solid black;border-spacing: 0; padding: 0;font-family:verdana;'>");
            rpt.Append("<tr style='height:25px;'>");
            rpt.Append("<td style='font-family:Verdana; font-size:x-large;text-align:center'><b>রোগীর বিশেষ কয়েকটি আশঙ্কাজনক পরিস্থিতি ও তার ঝুঁকি সম্পর্কে স্বীকার পত্র</b>");
            rpt.Append("</tr >");

            rpt.Append("<tr style='height:20px;'>");
            rpt.AppendFormat("<td style='font-family:Verdana; font-size:medium;text-align:center'>(যিনি ভর্তি করাচ্ছেন তার নাম:{0},সম্পর্ক:{1})</td>", ds.Tables[0].Rows[0]["guardian_name"], ds.Tables[0].Rows[0]["relation"]);
            rpt.Append("</tr >");
            rpt.Append("</table>");

            PatientDetails_Bengali(ds);
            rpt.Append("<br/>"); 

            rpt.Append("<table width='100%'>");
            rpt.Append("<tr style='height:25px'>"); rpt.Append("<u>");
            rpt.Append("<td style='font-family:Verdana;font-size:medium;font-weight:bold; text-align:left'>রোগীর সমস্যা বা ঝুঁকির কারণ সমূহ: </td>"); rpt.Append("</u>");
            rpt.Append("</tr >");

            rpt.Append("<tr style='height:35px'>");
            rpt.Append("<td style='font-family:Verdana;font-size:medium; text-align:left'>ব্রেনে রক্তপাত, অজ্ঞান অবস্থা, প্যারালেসিস, জীবনী শক্তি কম, ফুসফুস কমজোরী, হার্ট দূর্বল এবং অনিয়মিত, অন্যান্য অঙ্গ ভাল কাজ করছে না</td>");
            rpt.Append("</tr >");

            rpt.Append("<tr style='height:35px'>"); rpt.Append("<u>");
            rpt.Append("<td style='font-family:Verdana;font-size:medium;font-weight:bold;text-align:left'>ওই সমস্ত রোগীর যে সমস্ত বিপত্তি ঘটতে পারে :</td>"); rpt.Append("</u>");
            rpt.Append("</tr >");

            rpt.Append("<tr style='height:35px'>");
            rpt.Append("<td style='font-family:Verdana;font-size:medium; text-align:left'>আর জ্ঞান না ফিরতে পারে, প্যারালেসিস হতে পারে, শ্বাসকষ্ট হতে পারে, ব্রেনে আরও রক্তপাত হতে পারে, হঠাৎ হৃদযন্র বিকল হতে পারে, পেট ফুলে যেতে পারে,ইনফেকসন বা সেপশিস হতে পারে, যখন তখন রোগীর অবস্থা আরও খারাপ হতে পারে, এমন কি মারা ও যেতে পারে, যে কোন মুহূর্তে অন্যত্র নিয়ে যেতে হতে পারে,এই রোগের চিকিৎসার কোন গ্যারান্টী নেই</td>");
            rpt.Append("</tr >");
            rpt.Append("</table>");
            rpt.Append("<br/>");
            rpt.Append("<table width='100%'>");
            rpt.Append("<tr style='height:30px'>");
            rpt.Append("<td style='font-family:Verdana;font-size:medium; text-align:justify'>এই প্রতিষ্ঠানের পক্ষ থেকে রোগীর এই সমস্ত সমস্যা ও আশঙ্কার কথা জানলাম । এই প্রতিষ্ঠানের পক্ষ থেকে আমাদের বারবার কোন সরকারী মেডিকেল কলেজ কিংবা অন্য কোনও স্বাস্থ্য প্রতিষ্ঠানের নিয়ে যাবার কথা বলা হয়েছে , আমরাও এই প্রতিষ্ঠান যে সমস্ত পরামর্শ দিয়েছে তারসঙ্গে একমত । কিন্তু এই মূহুর্তে রোগীকে অন্যত্র নিয়ে যাবার মতো পরিস্থিতি আমাদের নেই , তাই রোগীর জীবন হানী সহ সমস্ত ঝুঁকির কথা মাথায় রেখেই আমরা আপদকালীন চিকিৎ্সার জন্য রোগীকে এই প্রতিষ্ঠানে ভর্তি করলাম , এখানে রোগীর প্রয়োজনীয় যাবতীয় চিকিত্সা করার জন্য অনুমতি দিলাম । </td>");
            rpt.Append("</tr >");
            rpt.Append("</table>");
            rpt.Append("<br/>");
            rpt.Append("<table width='100%'>");
            rpt.Append("<tr style='height:30px'>");
            rpt.Append("<td style=' font-family:Verdana;font-size:medium; text-align:justify'>আমরা যেহেতু সুস্থ মনে আমাদের আত্মীয় পরিজনদের সঙ্গে আলোচনা করে রোগীর চিকিত্সা চালানোর অনুমতি দিচ্ছি সেক্ষেত্রে চিকিত্সা চলাকালীন কিংবা এই প্রতিষ্ঠান থেকে ছাড়া পাওয়ার পর রোগীর অন্য কোন জটিল সম্যসা হলে কিংবা জীবন সংশয় হলে আমরা এই প্রতিষ্ঠানকে দায়ী করব না ।</td>");
            rpt.Append("</tr >");
            rpt.Append("</table>");




            rpt.Append("<table width='100%'>");
            rpt.Append("<tr style='height:35px'>");
            rpt.Append("<td style='font-family:Verdana; font-size:medium; text-align:center'> স্বাক্ষর/টিপ </td>");
            rpt.Append("<td style='font-family:Verdana; font-size:medium; text-align:center'> পুরো  নাম  </td>");
            rpt.Append("<td style='font-family:Verdana; font-size:medium; text-align:center'> ঠিকানা </td>");
            rpt.Append("<td style='font-family:Verdana; font-size:medium; text-align:center'> সম্পর্ক  </td>");
            rpt.Append("</tr >");

            rpt.Append("<tr style='height:27px'>");
            rpt.Append("<td style='font-family:Verdana;  font-size:small; text-align:center'>..................</td>");
            rpt.AppendFormat("<td style='font-family:Verdana;  font-size:small; text-align:center'>{0}</td>", ds.Tables[0].Rows[0]["patient_name"]);
            rpt.AppendFormat("<td style='font-family:Verdana; font-size:small; text-align:center'>{0}</td>", ds.Tables[0].Rows[0]["vill_city"]);
            rpt.Append("<td style='font-family:Verdana; font-size:small; text-align:center'> নিজে </td>");
            rpt.Append("</tr >");

            rpt.Append("<tr style='height:27px'>");
            rpt.Append("<td style='font-family:Verdana;  font-size:small; text-align:center'>..................</td>");
            rpt.AppendFormat("<td style='font-family:Verdana;  font-size:small; text-align:center'>{0}</td>", ds.Tables[0].Rows[0]["guardian_name"]);
            rpt.AppendFormat("<td style='font-family:Verdana; font-size:small; text-align:center'>{0}</td>", ds.Tables[0].Rows[0]["vill_city"]);
            rpt.AppendFormat("<td style='font-family:Verdana; font-size:small; text-align:center'> {0} </td>", ds.Tables[0].Rows[0]["relation"]);
            rpt.Append("</tr >");
            rpt.Append("</table>"); 
        } 
        ltrReport.Visible = true;
    }

    public void GetHearder_StrokeReport1(string reg)
    {
        ltrReport.Text = "";
        DataSet ds = thepatientbill.PatientForReport(reg, Session["CoCode"].ToString().Trim());
        if (ds.Tables[0].Rows.Count > 0)
        {
            rpt.Append("<table width='100%' cellspacing=0 border=0 style='border:1px solid black;border-spacing: 0; padding: 0;font-family:verdana;'>");
            rpt.Append("<tr style='height:25px;'>");
            rpt.Append("<td style='font-family:Verdana; font-size:x-large;text-align:center'><b>Some of the critical situation of the patient about the risks and accept the Letter</b>");
            rpt.Append("</tr >");

            rpt.Append("<tr style='height:20px;'>");
            rpt.AppendFormat("<td style='font-family:Verdana; font-size:medium;text-align:center'>(Admitted By :{0}, Relation :{1})</td>", ds.Tables[0].Rows[0]["guardian_name"], ds.Tables[0].Rows[0]["relation"]);
            rpt.Append("</tr >");
            rpt.Append("</table>");

            PatientDetails_English(ds);
            rpt.Append("<br/>"); 

             rpt.Append("<table width='100%'>");
            rpt.Append("<tr style='height:25px'>"); rpt.Append("<u>");
            rpt.Append("<td style='font-family:Verdana;font-size:medium;font-weight:bold; text-align:left'>Problems or risk factors of the patient  : </td>"); rpt.Append("</u>");
            rpt.Append("</tr >");

            rpt.Append("<tr style='height:25px'>");
            rpt.Append("<td style='font-family:Verdana;font-size:small; text-align:left'> Brene bleeding, unconsciousness, pyaralesisa, biographies of the low, deficient lungs, heart weak and irregular, does not work well in other organs </td>");
            rpt.Append("</tr >");

            rpt.Append("<tr style='height:25px'>"); rpt.Append("<u>");
            rpt.Append("<td style='font-family:Verdana;font-size:medium;font-weight:bold;text-align:left'>Hazard that may occur in the patient  :</td>"); rpt.Append("</u>");
            rpt.Append("</tr >");

            rpt.Append("<tr style='height:25px'>");
            rpt.Append("<td style='font-family:Verdana;font-size:small; text-align:left'> Returns to the knowledge, may be pyaralesisa, shortness of breath may be brene further bleeding, sudden hrdayanra can crash, the stomach may be swollen, may sepasisa inaphekasana or when the patient's condition could be worse, and even death may be, may be taken away at any moment, there are no guarantees the treatment of this disease </td>");
            rpt.Append("</tr >");
            rpt.Append("</table>");
            rpt.Append("<br/>");
            rpt.Append("<table width='100%'>");
            rpt.Append("<tr style='height:25px'>");
            rpt.Append("<td style='font-family:Verdana;font-size:small; text-align:justify'> The organization learned from the patient about these issues and concerns . This organization has repeatedly us from any government medical college or any other health organizations have been asked to take , we suggest that these institutions have agreed along . But now the situation is like taking away patients who do not have , so we are keeping in mind the patient's life is at risk , including hostile cikitsara emergency admissions for patients who have made this institution , the patient gave permission for the necessary treatment .  </td>");
            rpt.Append("</tr >");
            rpt.Append("</table>");
            rpt.Append("<br/>");
            rpt.Append("<table width='100%'>");
            rpt.Append("<tr style='height:25px'>");
            rpt.Append("<td style=' font-family:Verdana;font-size:small; text-align:justify'> We think it's healthy to talk with our kindred allowed to treat the patient during treatment, or in this case, the organization would be without the complicated error occurred when the patient or any other life-threatening if not, we'll blame the institution. </td>");
            rpt.Append("</tr >");
            rpt.Append("</table>");


            rpt.Append("<table width='100%'>");
            rpt.Append("<tr style='height:27px'>");
            rpt.Append("<td style='font-family:Verdana; font-size:medium; text-align:center'> Signature </td>");
            rpt.Append("<td style='font-family:Verdana; font-size:medium; text-align:center'> Full Name  </td>");
            rpt.Append("<td style='font-family:Verdana; font-size:medium; text-align:center'> Address </td>");
            rpt.Append("<td style='font-family:Verdana; font-size:medium; text-align:center'> Relation  </td>");
            rpt.Append("</tr >");

            rpt.Append("<tr style='height:27px'>");
            rpt.Append("<td style='font-family:Verdana;  font-size:small; text-align:center'>..................</td>");
            rpt.AppendFormat("<td style='font-family:Verdana;  font-size:small; text-align:center'>{0}</td>", ds.Tables[0].Rows[0]["patient_name"]);
            rpt.AppendFormat("<td style='font-family:Verdana; font-size:small; text-align:center'>{0}</td>", ds.Tables[0].Rows[0]["vill_city"]);
            rpt.Append("<td style='font-family:Verdana; font-size:small; text-align:center'> Self </td>");
            rpt.Append("</tr >");

            rpt.Append("<tr style='height:27px'>");
            rpt.Append("<td style='font-family:Verdana;  font-size:small; text-align:center'>..................</td>");
            rpt.AppendFormat("<td style='font-family:Verdana;  font-size:small; text-align:center'>{0}</td>", ds.Tables[0].Rows[0]["guardian_name"]);
            rpt.AppendFormat("<td style='font-family:Verdana; font-size:small; text-align:center'>{0}</td>", ds.Tables[0].Rows[0]["vill_city2"]);
            rpt.AppendFormat("<td style='font-family:Verdana; font-size:small; text-align:center'> {0} </td>", ds.Tables[0].Rows[0]["relation"]);
            rpt.Append("</tr >");
            rpt.Append("</table>"); 
        } 
        ltrReport.Visible = true;
    }

    protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            // Here you will get the Control you need like:
            DropDownList ddl = (DropDownList)e.Row.FindControl("DropDownList6");

            DataTable dt = thereg.DropdownHighRiskReport();
            ddl.DataSource = dt;
            ddl.DataTextField = "HighRiskReport";
            ddl.DataValueField = "RowId";
            ddl.DataBind();
             ddl.Items.Insert(0, new ListItem("--Select--", "0"));
         }
    }
}