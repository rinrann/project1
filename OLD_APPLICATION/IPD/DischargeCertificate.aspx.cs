using System;
using System.Data;
using System.Configuration;
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
using System.Collections.Generic;
//using Spire.Doc;
using iTextSharp.text;
using iTextSharp.text.pdf;
using iTextSharp.text.html;
using iTextSharp.text.html.simpleparser;
using System.IO;  

public partial class IPD_DischargeCertificate : System.Web.UI.Page
{
    Main Objmain = new Main(ConfigurationSettings.AppSettings["ConnectionString"].ToString());
    DischargeCertificate theabortion = new DischargeCertificate(ConfigurationSettings.AppSettings["ConnectionString"].ToString());
    Discharge thepatientbill = new Discharge(ConfigurationSettings.AppSettings["ConnectionString"].ToString());
    System.Text.StringBuilder rpt = new System.Text.StringBuilder();
    String Date = DateTime.Now.ToString("dd/MM/yyyy");
    string compcode="";
    protected void Page_Load(object sender, EventArgs e)
    {
        Page.Title = "Discharge Certificate";
        if (Session["userName"] == null)
        {
            Response.Redirect("../LoginPage.aspx");
            compcode = Session["CoCode"].ToString().Trim();
        }
        if (Objmain.checkaccss(Session["CoCode"].ToString(), Session["UserRoleID"].ToString().Trim(), "DISCHARGE CERTIFICATE", checkAccessType.ViewAction) == false)
        {
            Response.Redirect("../AccessDenied.aspx");
        }
        if (!IsPostBack)
        {
            DropDownList1.SelectedIndex = 2; DropDownList2.SelectedIndex = 2;
            TextBox2.Text=DateTime.Now.ToString("dd/MM/yyyy");
            RadioButtonList1.SelectedValue = "With Header";
            btnBack.Visible = false; btnPDF.Visible = false; cmdPrint.Visible = false;
            Panel2.Visible = false;
            RadioButtonList2.SelectedValue = "Current Report";
            DataSet docTab = thepatientbill.GetDoctors(Session["CoCode"].ToString().Trim());
            //docTab.Tables[0].Columns.Add("SelDoc", typeof(string));
            //docTab.Tables[0].Columns["SelDoc"].DefaultValue = "false";
            GridView1.DataSource = docTab.Tables[0];
            GridView1.DataBind();
        }
    }



    [System.Web.Script.Services.ScriptMethod()]
    [System.Web.Services.WebMethod]
    public static List<string> SearchCondition(string prefixText, int count,string compcode)
    {
        using (SqlConnection conn = new SqlConnection())
        {
            conn.ConnectionString = ConfigurationSettings.AppSettings["ConnectionString"].ToString();
            using (SqlCommand cmd = new SqlCommand())
            {
                
                cmd.CommandText = "select distinct dd.Remarks from GN_DischargeDetail dd  where dd.Remarks like @SearchText + '%' and compcode='"+compcode+"'";
                cmd.Parameters.AddWithValue("@SearchText", prefixText);
                cmd.Connection = conn;
                conn.Open();
                List<string> customers = new List<string>();
                using (SqlDataReader sdr = cmd.ExecuteReader())
                {
                    while (sdr.Read())
                    {
                        customers.Add(sdr["Remarks"].ToString());
                    }
                }
                conn.Close();
                return customers;
            }
        }
    }



    protected void Button4_Click(object sender, EventArgs e)
    {
        TextBox11.Text = "";
        if (RadioButtonList1.SelectedValue == "With Header")
        {
         //   Button1.Enabled = false;
            Report_Header();
            if (DropDownList1.SelectedIndex == 2)
            {
                GetHearder_Detail1(TextBox1.Text);
                ltrReport.Text = rpt.ToString();
            }
            else
            {
                GetHearder_Detail(TextBox1.Text);
                ltrReport.Text = rpt.ToString();
            }
        }
        else
        {
            if (DropDownList1.SelectedIndex == 2)
            {
                GetHearder_Detail1(TextBox1.Text);
                ltrReport.Text = rpt.ToString();
            }
            else
            {
                GetHearder_Detail(TextBox1.Text);
                ltrReport.Text = rpt.ToString();
            }
        }
        ltrReport.Text = rpt.ToString();
        if (ltrReport.Text != "")
        {
            btnBack.Visible = true;
            btnPDF.Visible = true;
            cmdPrint.Visible = true;
        }
        else
        {
            btnBack.Visible = false;
            btnPDF.Visible = false;
            cmdPrint.Visible = false;
        }

    }



    public void PatientDetails_Bengali(DataSet ds, DataTable dtdischarge)
    {
        rpt.Append("<table width='100%' cellspacing=0 border=0 style='border:1px solid black;border-spacing: 0; padding: 0;font-family:verdana;'>");
        rpt.Append("<tr style='height:20px'>");
        rpt.AppendFormat("<td style='height:20px; font-family:Verdana; font-size:small;text-align:center'><b>রোগী:</b> {0}, {1}, {2},<b> প্রযত্নে:</b>{3}<b>, ঠিকানা :</b>{4},{5},{6}, <b><br/>ফোন নং:</b>{7}</td>", ds.Tables[0].Rows[0]["patient_name"], ds.Tables[0].Rows[0]["age"], ds.Tables[0].Rows[0]["SexName"], ds.Tables[0].Rows[0]["HusbandName"], ds.Tables[0].Rows[0]["vill_city"], ds.Tables[0].Rows[0]["po"], ds.Tables[0].Rows[0]["DistrictName"], ds.Tables[0].Rows[0]["PhNo1"]);
        rpt.Append("</tr >");

        rpt.Append("<tr style='height:20px'>");
        rpt.AppendFormat("<td style='height:20px; font-family:Verdana; font-size:small;text-align:center'><b>ভর্তির কারণ:</b> {0}, <b>ডা:বাবুর নাম:</b> {1}</td>", ds.Tables[0].Rows[0]["DiagnosisName"], ds.Tables[0].Rows[0]["doc_name"]);
        rpt.Append("</tr >");

        rpt.Append("<tr style='height:20px'>");
        rpt.AppendFormat("<td style='height:20px; font-family:Verdana; font-size:small;text-align:center'><b>রেজি: নং:</b> {0}, <b>শয্যা নং:</b> {1}</td>", ds.Tables[0].Rows[0]["PatientReg"], ds.Tables[0].Rows[0]["BedNoText"]);
        rpt.Append("</tr >");

        if (dtdischarge.Rows.Count > 0)
        {
            rpt.Append("<tr style='height:20px'>");
            rpt.AppendFormat("<td style='height:20px; font-family:Verdana; font-size:small;text-align:center'><u><b>ভর্তির তারিখ ও সময় :</b></u> {0},  <u><b>নিষ্কাশন তারিখ  ও সময় :</b></u> {1}</td>", ds.Tables[0].Rows[0]["adate"], dtdischarge.Rows[0]["disdate"] + "," + dtdischarge.Rows[0]["DischargeTime"]);
            rpt.Append("</tr >");
        }
        else
        {
            rpt.Append("<tr style='height:20px'>");
            rpt.AppendFormat("<td style='height:20px; font-family:Verdana; font-size:small;text-align:center'><u><b>ভর্তির তারিখ ও সময় :</b></u> {0},  <u><b>নিষ্কাশন তারিখ :</b></u> {1}</td>", ds.Tables[0].Rows[0]["adate"], TextBox2.Text);
            rpt.Append("</tr >");
        }
        rpt.Append("</table>");
    }

    public void PatientDetails_English(DataSet ds, DataTable dtdischarge)
    {
        rpt.Append("<table width='100%' cellspacing=0 border=0 style='border:1px solid black;border-spacing: 0; padding: 0;font-family:verdana;'>");
        rpt.Append("<tr style='height:20px'>");
        rpt.AppendFormat("<td style='height:20px; font-family:Verdana; font-size:small;text-align:center'><u><b>Patient:</b></u> {0}, {1}, {2},<u><b>C/O:</b></u>{3} <u><b>of</b></u> {4},{5},{6}, <u><b><br/>Ph No:</b></u>{7}</td>", ds.Tables[0].Rows[0]["patient_name"], ds.Tables[0].Rows[0]["age"], ds.Tables[0].Rows[0]["SexName"], ds.Tables[0].Rows[0]["HusbandName"], ds.Tables[0].Rows[0]["vill_city"], ds.Tables[0].Rows[0]["po"], ds.Tables[0].Rows[0]["DistrictName"], ds.Tables[0].Rows[0]["PhNo1"]);
        rpt.Append("</tr >");

        rpt.Append("<tr style='height:20px'>");
        rpt.AppendFormat("<td style='height:20px; font-family:Verdana; font-size:small;text-align:center'><u><b>Diagnosis:</b></u> {0}, <u><b>Under Doctor:</b></u> {1}</td>", ds.Tables[0].Rows[0]["DiagnosisName"], ds.Tables[0].Rows[0]["doc_name"]);
        rpt.Append("</tr >");

        rpt.Append("<tr style='height:20px'>");
        rpt.AppendFormat("<td style='height:20px; font-family:Verdana; font-size:small;text-align:center'><u><b>Regn No:</b></u> {0}, <u><b>Bed No:</b></u> {1}</td>", ds.Tables[0].Rows[0]["PatientReg"], ds.Tables[0].Rows[0]["BedNoText"]);
        rpt.Append("</tr >");

        if (dtdischarge.Rows.Count > 0)
        {
            rpt.Append("<tr style='height:20px'>");
            rpt.AppendFormat("<td style='height:20px; font-family:Verdana; font-size:small;text-align:center'><u><b>Adm Dt :</b></u> {0},  <u><b>Discharge Dt & Time :</b></u> {1}</td>", ds.Tables[0].Rows[0]["adate"], dtdischarge.Rows[0]["disdate"] + ","+ dtdischarge.Rows[0]["DischargeTime"]);
            rpt.Append("</tr >");
        }
        else
        {
            rpt.Append("<tr style='height:20px'>");
            rpt.AppendFormat("<td style='height:20px; font-family:Verdana; font-size:small;text-align:center'><u><b>Adm:</b></u> {0},  <u><b>Discharge Dt:</b></u> {1}</td>", ds.Tables[0].Rows[0]["adate"], TextBox2.Text);
            rpt.Append("</tr >");
        }
        rpt.Append("</table>");
 
    }




    public void Report_Header()
    {
        rpt.Append("<table width='100%' cellspacing=0 border=0 style='border:1px solid black;border-spacing: 0; padding: 0;font-family:verdana;'>");
        rpt.Append("<tr cellpadding:'0'  style='height:30px;'>");
        rpt.Append("<td rowspan='3' width='2%' style='font-family:Verdana; font-size:x-large; font-weight:bold;padding-left:100px;text-align:left'><img src='../Images/gfclogo.png'  style='height:77px;width:75px;'/></td>");
        rpt.Append("<td   width='20%'  style='height:20px;font-family:Verdana; font-size:x-large; font-weight:bold;text-align:center'><u>GFC HOSPITAL</u></td>");
        rpt.Append("<td rowspan='3'  width='2%' style='height:20px;font-family:Verdana; font-size:x-large;padding-right:100px; font-weight:bold;text-align:right'><img src='../Images/gfclogo.png'  style='height:77px;width:75px;'/></td>");
        rpt.Append("</tr>");

        rpt.Append("<tr  style='height:20px;'>");
        rpt.AppendFormat("<td   width='20%'  style='height:10px;font-family:Verdana; font-size:small; font-weight:bold;text-align:center'>(Regn. No : NH-315/G-70/2013)</td>");
        rpt.Append("</tr>");

        rpt.Append("<tr  style='height:10px;'>");
        rpt.AppendFormat("<td  width='70%'  style='height:15px;font-family:Verdana; font-size:medium; font-weight:bold;text-align:center'>Kushpata, Ghatal, Paschim Medinipur, WB, 721212</td>");

        rpt.Append("</tr>");
        rpt.Append("</table>");
    }

    public void GetHearder_Detail(string reg)
    {
         ltrReport.Text = "";
        string age="";
        string treatedby = string.Empty;
        string treatenote = string.Empty;
        DataSet dsdetails = theabortion.GetVisitedDoctor(reg, Session["CoCode"].ToString().Trim());
        DataSet ds = thepatientbill.PatientForReport(reg, Session["CoCode"].ToString().Trim());

        if (dsdetails.Tables[0].Rows.Count > 0)
        {
            if (dsdetails.Tables[0].Rows[0]["age"].ToString() != "0")
                age = dsdetails.Tables[0].Rows[0]["age"].ToString() + " yrs ";
            if (dsdetails.Tables[0].Rows[0]["Agemonth"].ToString() != "0")
                age = age + dsdetails.Tables[0].Rows[0]["Agemonth"].ToString() + " mnt ";
            if (dsdetails.Tables[0].Rows[0]["Ageday"].ToString() != "0")
                age = age + dsdetails.Tables[0].Rows[0]["Ageday"].ToString() + " day";

            for (int i = 0; i < dsdetails.Tables[1].Rows.Count; i++)
            {
                if (dsdetails.Tables[1].Rows[i][1].ToString() != "")
                {
                    treatenote = treatenote + dsdetails.Tables[1].Rows[i][1].ToString() + " (<i>treated by :" + dsdetails.Tables[1].Rows[i][0].ToString() + "</i>)<br>";
                }
                if (treatedby == "")
                    treatedby = dsdetails.Tables[1].Rows[i][0].ToString();
                else
                    treatedby = treatedby + " , " + dsdetails.Tables[1].Rows[i][0].ToString();
            }

          
            rpt.Append("<br/>");
            DataTable dtdischarge = theabortion.DischargeDtks(reg, Session["CoCode"].ToString().Trim(), Session["YearCode"].ToString().Trim());
            PatientDetails_Bengali(ds, dtdischarge);

            DataTable dtot = theabortion.GetOperation(reg, Session["CoCode"].ToString().Trim(), Session["YearCode"].ToString().Trim());
            string opname = "", opdate = "", opremarks = "", anesthesiatype = "", Surgeon="";
            for (int i = 0; i < dtot.Rows.Count; i++)
            {
                if (opname == "")
                    opname = dtot.Rows[i]["OperationName"].ToString();
                else
                    opname = opname + " , " + dtot.Rows[i]["OperationName"].ToString();

                if (Surgeon == "")
                    Surgeon = dtot.Rows[i]["doc_name"].ToString();
                else
                    Surgeon = Surgeon + " , " + dtot.Rows[i]["doc_name"].ToString();

                if (anesthesiatype == "")
                    anesthesiatype = dtot.Rows[i]["AnesthesiaType"].ToString();
                else
                    anesthesiatype = anesthesiatype + " , " + dtot.Rows[i]["AnesthesiaType"].ToString();

                if (opdate == "")
                    opdate = dtot.Rows[i]["otdate"].ToString();
                else
                    opdate = opdate + " , " + dtot.Rows[i]["otdate"].ToString();


                if (opremarks == "")
                    opremarks = dtot.Rows[i]["Remarks"].ToString();
                else
                    opremarks = opremarks + " , " + dtot.Rows[i]["Remarks"].ToString();
            }

            rpt.Append("<br />");
            rpt.Append("<table width='100%' cellspacing=0 border=0 style='border:1px solid black;border-spacing: 0; padding: 0;font-family:verdana;'>");

            rpt.Append("<tr style='height:30px'>");
            rpt.Append("<td style='width:3%; border-right: 1px solid black; font-family:Verdana;font-weight:bold;background-color:beige; font-size:small; text-align:left'>ক্লিনিকাল ডায়াগনসিস  :</td>");
            rpt.AppendFormat("<td style='width: 15%; padding-left:30px;font-family:Verdana; font-size:small;text-align:left'>{0}</td>", dsdetails.Tables[0].Rows[0]["DiagnosisName"]);
            rpt.Append("</tr >");

           // if (dsdetails.Tables[0].Rows[0]["TreatementNote"].ToString() != "")
           // {
                rpt.Append("<tr style='height:30px'>");
                rpt.Append("<td style='width:3%; border-right: 1px solid black; font-family:Verdana;font-weight:bold;background-color:beige; font-size:small; text-align:left'>ট্রীটমেন্ট  নোট   :</td>");
                rpt.AppendFormat("<td style='width: 15%;padding-left:30px; font-family:Verdana; font-size:small;text-align:left'>{0} </td>",treatenote);
                rpt.Append("</tr >");
           // }

            if (dtot.Rows.Count > 0)
            {
                rpt.Append("<tr style='height:30px'>");
                rpt.Append("<td style='width: 3%; border-right: 1px solid black; font-family:Verdana;font-weight:bold;background-color:beige; font-size:small; text-align:left'>অপারেশন নাম  :</td>");
                rpt.AppendFormat("<td style='width: 15%;padding-left:30px; font-family:Verdana; font-size:small;text-align:left'>{0}</td>", opname);
                rpt.Append("</tr >");

                rpt.Append("<tr style='height:30px'>");
                rpt.Append("<td style='width: 3%; border-right: 1px solid black; font-family:Verdana;font-weight:bold;background-color:beige; font-size:small; text-align:left'>অপারেশন এনেস্থেসিয়া :</td>");
                rpt.AppendFormat("<td style='width: 15%;padding-left:30px; font-family:Verdana; font-size:small;text-align:left'>{0}</td>", anesthesiatype);
                rpt.Append("</tr >");

                rpt.Append("<tr style='height:30px'>");
                rpt.Append("<td style='width: 3%; border-right: 1px solid black; font-family:Verdana;font-weight:bold;background-color:beige; font-size:small; text-align:left'>অপারেশন তারিখ   :</td>");
                rpt.AppendFormat("<td style='width: 15%;padding-left:30px; font-family:Verdana; font-size:small;text-align:left'>{0}</td>", opdate);
                rpt.Append("</tr >");

                rpt.Append("<tr style='height:30px'>");
                rpt.Append("<td style='width:3%; border-right: 1px solid black; font-family:Verdana;font-weight:bold;background-color:beige; font-size:small; text-align:left'>অপারেশন লক্ষ্য   :</td>");
                rpt.AppendFormat("<td style='width: 15%;padding-left:30px; font-family:Verdana; font-size:small;text-align:left'>{0} ( Surgeon :{1} )</td>", opremarks, Surgeon);
                rpt.Append("</tr >");
            }

            if (dtdischarge.Rows.Count > 0)
            {
                rpt.Append("<tr style='height:30px'>");
                rpt.Append("<td style='width: 3%; border-right: 1px solid black; font-family:Verdana;font-weight:bold;background-color:beige; font-size:small; text-align:left'>পরিস্থিতি নিষ্কাশন :</td>");
                rpt.AppendFormat("<td style='width: 15%;padding-left:30px; font-family:Verdana; font-size:small;text-align:left'>{0}</td>", dtdischarge.Rows[0]["Remarks"]);
                rpt.Append("</tr >");
            }
            else
            {
                rpt.Append("<tr style='height:30px'>");
                rpt.Append("<td style='width: 3%; border-right: 1px solid black; font-family:Verdana;font-weight:bold;background-color:beige; font-size:small; text-align:left'>পরিস্থিতি নিষ্কাশন :</td>");
                rpt.AppendFormat("<td style='width: 15%;padding-left:30px; font-family:Verdana; font-size:small;text-align:left'>{0}</td>", TextBox3.Text);
                rpt.Append("</tr >");
            }

            rpt.Append("</table >");

            DataTable dtDoctor = theabortion.GetDoctorDetailsDtls(reg, Session["CoCode"].ToString().Trim());
            if (dtDoctor.Rows.Count > 0)
            {
                rpt.Append("<br />"); rpt.Append("<br />"); rpt.Append("<br />"); rpt.Append("<br />");
                rpt.Append("<table width='100%' cellspacing=0 border=0 style='border-spacing: 0; padding: 0;font-family:verdana;'>");
                rpt.Append("<tr style='height:20px'>");
                rpt.AppendFormat("<td style='font-family:Verdana;font-weight:bold;font-size:small; text-align:left'>Dr. {0}</td>", dtDoctor.Rows[0][0]);
                rpt.Append("</tr >");

                rpt.Append("<tr style='height:20px'>");
                rpt.AppendFormat("<td style='font-family:Verdana;font-weight:bold;font-size:small; text-align:left'>{0}</td>", dtDoctor.Rows[0][1]);
                rpt.Append("</tr >");

                rpt.Append("<tr style='height:20px'>");
                rpt.AppendFormat("<td style='font-family:Verdana;font-weight:bold;font-size:small; text-align:left'>{0}</td>", dtDoctor.Rows[0][2]);
                rpt.Append("</tr >");
                rpt.Append("</table >");
            }
        }
        ltrReport.Visible = true;


    }


    public void GetHearder_Detail1(string reg1)
    {
         ltrReport.Text = "";
         string age = "";
         string treatedby = string.Empty;
         string treatenote = string.Empty;
         DataSet dsdetails = theabortion.GetVisitedDoctor(reg1, Session["CoCode"].ToString().Trim());
         if (dsdetails.Tables[0].Rows.Count > 0)
         {
             if (dsdetails.Tables[0].Rows[0]["age"].ToString() != "0")
                 age = dsdetails.Tables[0].Rows[0]["age"].ToString() + " yrs ";
             if (dsdetails.Tables[0].Rows[0]["Agemonth"].ToString() != "0")
                 age = age + dsdetails.Tables[0].Rows[0]["Agemonth"].ToString() + " mnt ";
             if (dsdetails.Tables[0].Rows[0]["Ageday"].ToString() != "0")
                 age = age + dsdetails.Tables[0].Rows[0]["Ageday"].ToString() + " day";

             for (int i = 0; i < dsdetails.Tables[1].Rows.Count; i++)
             {
                 if (dsdetails.Tables[1].Rows[i][1].ToString() != "")
                 {
                     treatenote = treatenote + dsdetails.Tables[1].Rows[i][1].ToString() + " (<i>treated by :" + dsdetails.Tables[1].Rows[i][0].ToString() + "</i>)<br>";
                 }
                 if (treatedby == "")
                     treatedby = dsdetails.Tables[1].Rows[i][0].ToString();
                 else
                     treatedby =treatedby+" , "+ dsdetails.Tables[1].Rows[i][0].ToString();
             }

             rpt.Append("<br/>");
             DataSet ds = thepatientbill.PatientForReport(reg1, Session["CoCode"].ToString().Trim());
             DataTable dtdischarge = theabortion.DischargeDtks(reg1, Session["CoCode"].ToString().Trim(), Session["YearCode"].ToString().Trim());
             PatientDetails_English(ds, dtdischarge);


             DataTable dtot = theabortion.GetOperation(reg1,Session["CoCode"].ToString().Trim(), Session["YearCode"].ToString().Trim());
             string opname = "", opdate = "", opremarks = "", anesthesiatype = "", Surgeon="";
             for (int i = 0; i < dtot.Rows.Count; i++)
             {
                 if (opname == "")
                     opname = dtot.Rows[i]["OperationName"].ToString();
                 else
                     opname = opname + " , " + dtot.Rows[i]["OperationName"].ToString();

                 if (Surgeon == "")
                     Surgeon = dtot.Rows[i]["doc_name"].ToString();
                 else
                     Surgeon = Surgeon + " , " + dtot.Rows[i]["doc_name"].ToString();


                 if (anesthesiatype == "")
                     anesthesiatype = dtot.Rows[i]["AnesthesiaType"].ToString();
                 else
                     anesthesiatype = anesthesiatype + " , " + dtot.Rows[i]["AnesthesiaType"].ToString();



                 if (opdate == "")
                     opdate = dtot.Rows[i]["otdate"].ToString();
                 else
                     opdate = opdate + " , " + dtot.Rows[i]["otdate"].ToString();


                 if (opremarks == "")
                     opremarks = dtot.Rows[i]["Remarks"].ToString();
                 else
                     opremarks = opremarks + " , " + dtot.Rows[i]["Remarks"].ToString();
             }


             rpt.Append("<br />");
             rpt.Append("<table width='100%' cellspacing=0 border=0 style='border:1px solid black;border-spacing: 0; padding: 0;font-family:verdana;'>");

             rpt.Append("<tr style='height:30px'>");
             rpt.Append("<td style='width:3%; border-right: 1px solid black; font-family:Verdana;font-weight:bold;background-color:beige; font-size:small; text-align:left'>Clinical Diagnosis   :</td>");
             rpt.AppendFormat("<td style='width: 15%; padding-left:30px;font-family:Verdana; font-size:small;text-align:left'>{0}</td>", dsdetails.Tables[0].Rows[0]["DiagnosisName"]);
             rpt.Append("</tr >");

            // if (dsdetails.Tables[0].Rows[0]["TreatementNote"].ToString() != "")
            // {
                 rpt.Append("<tr style='height:30px'>");
                 rpt.Append("<td style='width:3%; border-right: 1px solid black; font-family:Verdana;font-weight:bold;background-color:beige; font-size:small; text-align:left' valign='top'>Treatement Note   :</td>");
                 rpt.AppendFormat("<td style='width: 15%;padding-left:30px; font-family:Verdana; font-size:small;text-align:left'>{0}</i></td>",treatenote);
                 rpt.Append("</tr >");
            // } dsdetails.Tables[0].Rows[0]["TreatementNote"], treatedby

             if (dtot.Rows.Count > 0)
             {
                 rpt.Append("<tr style='height:30px'>");
                 rpt.Append("<td style='width: 3%; border-right: 1px solid black; font-family:Verdana;font-weight:bold;background-color:beige; font-size:small; text-align:left'>Operation Name   :</td>");
                 rpt.AppendFormat("<td style='width: 15%;padding-left:30px; font-family:Verdana; font-size:small;text-align:left'>{0}</td>", opname);
                 rpt.Append("</tr >");

                 rpt.Append("<tr style='height:30px'>");
                 rpt.Append("<td style='width: 3%; border-right: 1px solid black; font-family:Verdana;font-weight:bold;background-color:beige; font-size:small; text-align:left'>Anesthesia Type :</td>");
                 rpt.AppendFormat("<td style='width: 15%;padding-left:30px; font-family:Verdana; font-size:small;text-align:left'>{0}</td>", anesthesiatype);
                 rpt.Append("</tr >");

                 rpt.Append("<tr style='height:30px'>");
                 rpt.Append("<td style='width: 3%; border-right: 1px solid black; font-family:Verdana;font-weight:bold;background-color:beige; font-size:small; text-align:left'>Operation Date   :</td>");
                 rpt.AppendFormat("<td style='width: 15%;padding-left:30px; font-family:Verdana; font-size:small;text-align:left'>{0}</td>", opdate);
                 rpt.Append("</tr >");

                 rpt.Append("<tr style='height:30px'>");
                 rpt.Append("<td style='width:3%; border-right: 1px solid black; font-family:Verdana;font-weight:bold;background-color:beige; font-size:small; text-align:left'>Operation Note   :</td>");
                 rpt.AppendFormat("<td style='width: 15%;padding-left:30px; font-family:Verdana; font-size:small;text-align:left'>{0} <i>( Surgeon: {1} )</i></td>", opremarks, Surgeon);
                 rpt.Append("</tr >");

             }


             if (dtdischarge.Rows.Count > 0)
             {
                 rpt.Append("<tr style='height:30px'>");
                 rpt.Append("<td style='width: 3%; border-right: 1px solid black; font-family:Verdana;font-weight:bold;background-color:beige; font-size:small; text-align:left'>Discharge Condition :</td>");
                 rpt.AppendFormat("<td style='width: 15%;padding-left:30px; font-family:Verdana; font-size:small;text-align:left'>{0}</td>", dtdischarge.Rows[0]["Remarks"]);
                 rpt.Append("</tr >");
             }
             else
             {
                 rpt.Append("<tr style='height:30px'>");
                 rpt.Append("<td style='width: 3%; border-right: 1px solid black; font-family:Verdana;font-weight:bold;background-color:beige; font-size:small; text-align:left'>Discharge Condition :</td>");
                 rpt.AppendFormat("<td style='width: 15%;padding-left:30px; font-family:Verdana; font-size:small;text-align:left'>{0}</td>", TextBox3.Text);
                 rpt.Append("</tr >");
             }

             rpt.Append("</table >");


             DataTable dtDoctor = theabortion.GetDoctorDetailsDtls(reg1, Session["CoCode"].ToString().Trim());
             if (dtDoctor.Rows.Count > 0)
             {
                 rpt.Append("<br />"); rpt.Append("<br />"); rpt.Append("<br />"); 
                 rpt.Append("<table width='100%' cellspacing=0 border=0 style='border-spacing: 0; padding: 0;font-family:verdana;'>");
                 rpt.Append("<tr style='height:0px'>");
                 //rpt.AppendFormat("<td style='font-family:Verdana;text-align:left'><div style='font-size:small;height:0px;'>{0}</div> <br/><div style='font-size:x-small;height:0px;'>{1}</div> <br/><div style='font-size:x-small;height:0px;'>{2}</div> </td>", dtDoctor.Rows[0][0], dtDoctor.Rows[0][1], dtDoctor.Rows[0][2]);
                 if (GridView1.Items.Count > 0)
                 {
                     for (int c = 0; c < GridView1.Items.Count; c++)
                     {
                         CheckBox chk = (CheckBox)GridView1.Items[c].FindControl("chkSelect");
                         if (chk.Checked == true)
                         {
                             
                             Label lblname = (Label)GridView1.Items[c].FindControl("lblName");
                             Label lblQuali = (Label)GridView1.Items[c].FindControl("lblQuali");
                             Label lblReg = (Label)GridView1.Items[c].FindControl("lblReg");
                             if (c % 4 != 0 && c != 0)
                             {
                                 rpt.AppendFormat("</td>");
                                 rpt.AppendFormat("<td style='font-family:Verdana;text-align:left'><div style='font-size:small;height:0px;'>{0}</div> <br/><div style='font-size:x-small;height:0px;'>{1}</div> <br/><div style='font-size:x-small;height:0px;'>{2}</div> </td>", lblname.Text, lblQuali.Text, lblReg.Text);
                             }
                             else
                             {
                                 rpt.AppendFormat("</tr>");
                                 rpt.Append("</table >");
                                 rpt.Append("<br />"); rpt.Append("<br />"); rpt.Append("<br />"); rpt.Append("<br />");
                                 rpt.Append("<table width='100%' cellspacing=0 border=0 style='border-spacing: 0; padding: 0;font-family:verdana;'>");
                                 rpt.Append("<tr style='height:0px'>");
                                 rpt.AppendFormat("<td style='font-family:Verdana;text-align:left'><div style='font-size:small;height:0px;'>{0}</div> <br/><div style='font-size:x-small;height:0px;'>{1}</div> <br/><div style='font-size:x-small;height:0px;'>{2}</div> </td>", lblname.Text, lblQuali.Text, lblReg.Text);
                             }
                         }
                     }
                 }
                 rpt.Append("</tr >");

                 //int i = 0;
                 //if (GridView1.Items.Count > 0)
                 //{
                 //    for (int c = 0; c < GridView1.Items.Count; c++)
                 //    {
                 //        CheckBox chk = (CheckBox)GridView1.Items[c].FindControl("chkSelect");
                 //        if (chk.Checked == true)
                 //        {
                 //            Label lblname = (Label)GridView1.Items[c].FindControl("lblName");
                 //            Label lblQuali = (Label)GridView1.Items[c].FindControl("lblQuali");
                 //            Label lblReg = (Label)GridView1.Items[c].FindControl("lblReg");
                 //            //if (i > 3)
                 //            //{
                 //            //    rpt.Append("<br />"); rpt.Append("<br />"); rpt.Append("<br />"); rpt.Append("<br />");
                 //            //    rpt.Append("<table width='100%' cellspacing=0 border=0 style='border-spacing: 0; padding: 0;font-family:verdana;'>");
                 //            //    rpt.Append("<tr style='height:0px'>");
                 //            //}
                 //            //else
                 //            //{
                 //            //    rpt.AppendFormat("<td style='font-family:Verdana;text-align:left'><div style='font-size:small;height:0px;'>{0}</div> <br/><div style='font-size:x-small;height:0px;'>{1}</div> <br/><div style='font-size:x-small;height:0px;'>{2}</div> </td>", dtDoctor.Rows[0][0], dtDoctor.Rows[0][1], dtDoctor.Rows[0][2]);
                 //            //    i = i + 1;
                 //            //}
                 //            //if (i > 3)
                 //            //{
                 //            //    rpt.Append("</tr >");
                 //            //}  

                 //            rpt.Append("<br />"); rpt.Append("<br />"); rpt.Append("<br />"); rpt.Append("<br />");
                 //            rpt.Append("<table width='100%' cellspacing=0 border=0 style='border-spacing: 0; padding: 0;font-family:verdana;'>");
                 //            rpt.Append("<tr style='height:0px'>");
                 //            rpt.AppendFormat("<td style='font-family:Verdana;text-align:left'><div style='font-size:small;height:0px;'>{0}</div> <br/><div style='font-size:x-small;height:0px;'>{1}</div> <br/><div style='font-size:x-small;height:0px;'>{2}</div> </td>", lblname.Text, lblQuali.Text, lblReg.Text);
                 //            rpt.Append("</tr >");     
                 //        }
                 //    }
                 //}
                 //rpt.Append("<tr style='height:0px'>");
                 //rpt.AppendFormat("<td style='font-family:Verdana;font-size:x-small; text-align:left'>{0}</td>", );
                 //rpt.Append("</tr >");

                 //rpt.Append("<tr style='height:0px'>");
                 //rpt.AppendFormat("<td style='font-family:Verdana;font-size:x-small; text-align:left'>{0}</td>", dtDoctor.Rows[0][2]);
                 //rpt.Append("</tr >");
                 rpt.Append("</table >");
             }
         }
        ltrReport.Visible = true;


    }

    public void PDF(string pdf)
    {
        string filename = "DischargeCertificate" +pdf;
        Response.ContentType = "application/pdf";
        Response.AddHeader("content-disposition", "attachment;filename='" + filename + "'.pdf");
        Response.Cache.SetCacheability(HttpCacheability.NoCache);
        StringWriter sw = new StringWriter();
        HtmlTextWriter hw = new HtmlTextWriter(sw);
        HtmlForm frm = new HtmlForm();
        mydiv.Parent.Controls.Add(frm);
        frm.Attributes["runat"] = "server";
        frm.Controls.Add(mydiv);
        frm.RenderControl(hw);
        StringReader sr = new StringReader(sw.ToString());
        Document pdfDoc = new Document(PageSize.A4, 10f, 10f, 10f, 0f);
        HTMLWorker htmlparser = new HTMLWorker(pdfDoc);
        PdfWriter.GetInstance(pdfDoc, Response.OutputStream);
        pdfDoc.Open();
        htmlparser.Parse(sr);
        pdfDoc.Close();
        Response.Write(pdfDoc);
        Response.End();
    }

  
    protected void RadioButtonList2_SelectedIndexChanged(object sender, System.EventArgs e)
    {
        //if (RadioButtonList2.SelectedValue == "Current Report")
        //{
        //    Panel1.Visible = true;
        //    Panel2.Visible = false;
        //}
        //else
        //{
        //    Panel2.Visible = true;
        //    Panel1.Visible = false;
        //}
    }
   
    protected void Button2_Click(object sender, System.EventArgs e)
    {
        TextBox1.Text = "";

        if (RadioButtonList1.SelectedValue == "With Header")
        {
            //   Button1.Enabled = false;
            Report_Header();
            if (DropDownList2.SelectedIndex == 2)
            {
                GetHearder_Detail1(TextBox11.Text);
            }
            else
            {
                GetHearder_Detail(TextBox11.Text);
            }
        }
        else
        {
            if (DropDownList2.SelectedIndex == 2)
            {
                GetHearder_Detail1(TextBox11.Text);
            }
            else
            {
                GetHearder_Detail(TextBox11.Text);
            }
        }
        ltrReport.Text = rpt.ToString();
        if (ltrReport.Text != "")
        {
            btnBack.Visible = true;
            btnPDF.Visible = true;
            cmdPrint.Visible = true;
        }
        else
        {
            btnBack.Visible = false;
            btnPDF.Visible = false;
            cmdPrint.Visible = false;

        }
    }
    protected void btnPDF_Click1(object sender, System.EventArgs e)
    {
        if (TextBox11.Text == "")
        {
            if (DropDownList1.SelectedIndex == 2)
            {

                GetHearder_Detail1(TextBox1.Text);
            }
            else
            {
                GetHearder_Detail(TextBox1.Text);

            }

            ltrReport.Text = rpt.ToString();
            PDF(TextBox1.Text);
        }
        else
        {
            if (DropDownList2.SelectedIndex == 2)
            {
                GetHearder_Detail1(TextBox11.Text);
            }
            else
            {
                GetHearder_Detail(TextBox11.Text);
            }
            ltrReport.Text = rpt.ToString();
            PDF(TextBox11.Text);
        }
    }
    protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            if (e.Row.RowIndex == 0)
                e.Row.Style.Add("height", "50px");
        }
    }
}