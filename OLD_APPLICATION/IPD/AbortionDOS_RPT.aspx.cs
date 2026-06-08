using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

public partial class IPD_AbortionDOS_RPT : System.Web.UI.Page
{

    System.IO.StreamWriter rdr;
    String Path = String.Empty;
    private Int32 LineNo = 0;
    public int pg = 1;
    //String details = String.Empty;
    public string details1 = string.Empty;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            //Session["RptTable"] = dt;
            //DataTable RptDt =(DataTable)Session["RptTable"];
            GenerateReport();
        }
    }
    public void GenerateReport()
    {
        DataTable RptDt = (DataTable)Session["RptTable"];
        if (RptDt != null)
        {
            if (RptDt.Rows.Count > 0)
            {
                string dir = Server.MapPath("Print");
                Path = Server.MapPath("Print/AbortionDos_Rpt.txt");

                if (!System.IO.Directory.Exists(dir))
                {
                    System.IO.Directory.CreateDirectory(dir);
                    ////System.IO.File.Create(Path);
                    rdr = new System.IO.StreamWriter(Path, false);
                }
                else
                {

                    rdr = new System.IO.StreamWriter(Path, false);
                }
                GetReport();
                //COMPNAME = objUserSession.gs_Coname;
                //GARDEN = objUserSession.gs_Gardenname;
                //From = Convert.ToDateTime(ViewState["From"]);
                //To = Convert.ToDateTime(ViewState["To"]);
                //PrintHeader();

                Close();
                ScriptManager.RegisterStartupScript(this, this.GetType(), "msg", "alert('Report Generated successfully');", true);

                string command = String.Empty;

                command = "print " + Path;
                Response.ContentType = "application/txt";
                string filePath = "AbortionDos_Rpt.txt";
                Response.AddHeader("Content-Disposition", "attachment;filename=\"" + filePath + "\"");
                Response.TransmitFile(Path);
                Response.End();
            }
            else
                ScriptManager.RegisterStartupScript(this, this.GetType(), "msg", "alert('No Data found for Division');", true);
        }



    }
    public void GetReport()
    {
        Print_Header();
        HEADER_Detail();
        //Report();
    }
    public void Print_Header()
    {
        LineNo = 0;
        if (pg < 10)
        {
            rdr.WriteLine(AllowSpace(158) + "Pg : " + pg);
            LineNo += 1;
        }
        else
        {
            rdr.WriteLine(AllowSpace(124) + "Pg : " + GetRightFormatedText(pg.ToString(), 3));
            LineNo += 1;
        }
        rdr.WriteLine(AllowSpace(75) + " GFC Hospital ");
        rdr.WriteLine(AllowSpace(70) + "REG NO : NH-315/G-70/2013");
        rdr.WriteLine(AllowSpace(65) + "KUSHPATA,GHATAL,MIDNAPUR(W),WB,721212");
        rdr.WriteLine(AllowSpace(65) + "Ph :(03225)244400/244643  M:9434419825");
        //"Ph :(03225)244400/244643  M:9434419825"
        PrintLine();
    }
    public void HEADER_Detail()
    {
        DataTable RptDt = (DataTable)Session["RptTable"];
        if (RptDt.Rows.Count > 0)
        {
            if (LineNo >= 53)      //if (iLine >= 65)  if (iLine >= 67)
            {
                pg = pg + 1;
                LineNo = 0;
                Print_Header();


                // ============================== 07/05/14 ================
            }
            rdr.WriteLine(AllowSpace(75) + "LIGATION FORM");
            rdr.WriteLine(AllowSpace(75) + "Patient's Details   ");
            PrintLine();
            rdr.WriteLine(AllowSpace(0) + " Registration No " + AllowSpace(8) + GetFormatedText(RptDt.Rows[0]["PatientReg"].ToString(), 13) + AllowSpace(24) + " Patient's Name " + AllowSpace(7) + GetFormatedText(RptDt.Rows[0]["patient_name"].ToString(), 18) + AllowSpace(15) + " Patient's Age " + AllowSpace(24) + GetRightFormatedText(RptDt.Rows[0]["age"].ToString(), 2));
            PrintLine();
            rdr.WriteLine(AllowSpace(0) + " Guadian Name  " + AllowSpace(10) + GetFormatedText(RptDt.Rows[0]["guardian_name"].ToString(), 16) + AllowSpace(21) + " Address " + AllowSpace(14) + GetFormatedText(RptDt.Rows[0]["vill_city"].ToString(), 13) + AllowSpace(20) + " Contact No " + AllowSpace(14) + GetRightFormatedText(RptDt.Rows[0]["PhNo1"].ToString(), 15));
            PrintLine();
            rdr.WriteLine(AllowSpace(0) + " Bed No " + AllowSpace(17) + GetRightFormatedText(RptDt.Rows[0]["BedNoText"].ToString(), 9) + AllowSpace(28) + " Admission Date:" + AllowSpace(7) + GetFormatedText(RptDt.Rows[0]["ADate"].ToString(), 10) + AllowSpace(23) + " Admission Time " + AllowSpace(16) + GetRightFormatedText(RptDt.Rows[0]["FromTime"].ToString(), 9));
            PrintLine();
            rdr.WriteLine(AllowSpace(0) + " Admitted By" + AllowSpace(13) + GetFormatedText(RptDt.Rows[0]["guardian_name"].ToString(), 16) + AllowSpace(21) + " Relation" + AllowSpace(14) + GetFormatedText(RptDt.Rows[0]["relation"].ToString(), 14) + AllowSpace(19) + " Doctor's Name" + AllowSpace(2) + GetRightFormatedText(RptDt.Rows[0]["doc_name"].ToString(), 25));
            PrintLine();
            SkipLine(2);
            rdr.WriteLine(AllowSpace(0) + " Reason of Admission :" + AllowSpace(15) + GetRightFormatedText(RptDt.Rows[0]["Diagnosis"].ToString(), 3));
            SkipLine(2);
            rdr.WriteLine(AllowSpace(0) + " I know that there are children in my womb and told me he would check me daktarababuo.But I lost my ____________  ");
            rdr.WriteLine(AllowSpace(0) + " to the fetus due to me like this. ");

            rdr.WriteLine(AllowSpace(0) + " About the abortion procedure and its potential complications daktarababu told me . Prachura even bleeding in some ");
            rdr.WriteLine(AllowSpace(0) + " cases , loss of bowel or gut microbe that lives in sankramanera There may be confusion or jarayu - There may be ");
            rdr.WriteLine(AllowSpace(0) + " removed through a aparesanera . We well know that these allow the abortion . Also learned that , according to ");
            rdr.WriteLine(AllowSpace(0) + " medical science, abortion, the baby is due in the future - including the possibility of reaching sudura have many ");
            rdr.WriteLine(AllowSpace(0) + " consequences . Shall not be liable for these daktarababu . ");
            rdr.WriteLine(AllowSpace(0) + " About the abortion procedure and its potential complications daktarababu told me. Even at this Consent Form, signed  ");
            rdr.WriteLine(AllowSpace(0) + " by all mean I understand.");
            rdr.WriteLine(AllowSpace(70) + " Finish--------- " + AllowSpace(2) + GetFormatedText(RptDt.Rows[0]["patient_name"].ToString(), 18));
            SkipLine(1);
            rdr.WriteLine(AllowSpace(8) + " Signature        " + AllowSpace(12) + "Full Name" + AllowSpace(12) + "Address" + AllowSpace(10) + "Relationship");
            rdr.WriteLine(AllowSpace(8) + " -----------------" + AllowSpace(12) + GetFormatedText(RptDt.Rows[0]["patient_name"].ToString(), 18) + AllowSpace(3) + GetFormatedText(RptDt.Rows[0]["vill_city"].ToString(), 13) + AllowSpace(8) + "Self");
            rdr.WriteLine(AllowSpace(8) + " -----------------" + AllowSpace(12) + GetFormatedText(RptDt.Rows[0]["guardian_name"].ToString(), 16) + AllowSpace(5) + GetFormatedText(RptDt.Rows[0]["vill_city"].ToString(), 13) + AllowSpace(8) + GetFormatedText(RptDt.Rows[0]["relation"].ToString(), 14));
            //PrintLine();
            //rdr.WriteLine(AllowSpace(0) + "-----------------------------------------------------------------------------------------------------------------");
            //rdr.WriteLine(AllowSpace(0) + " We are going to take from the patient's own account a healthy condition. We found the patient understand all the medical documents and test");
            //rdr.WriteLine(AllowSpace(0) + " papers. Dr. babuke very important issue both regular show and will contact government hospital or elsewhere. ");
            //SkipLine(2);
            //rdr.WriteLine(AllowSpace(8) + " Signature / thumb " + AllowSpace(10) + " Full Name " + AllowSpace(10) + " Address " + AllowSpace(10) + " Relationship ");



        }
    }
    private string GetFormatedText(string Cont, int Length)
    {
        int rLoc = Length - Cont.Length;
        if (rLoc < 0)
        {
            Cont = Cont.Substring(0, Length);
        }
        else
        {
            int nos;
            for (nos = 0; nos < rLoc; nos++)  //Length
                Cont = Cont + " ";
        }
        return (Cont);
    }
    public void SkipLine(int LineNos)
    {
        int i;
        for (i = 1; i <= LineNos; i++)
        {
            rdr.WriteLine("");
        }
    }
    private string AllowSpace(int Length)
    {
        string space = "";

        for (int nos = 0; nos < Length; nos++)
        {
            space += " ";
        }
        return space;
    }
    private string GetRightFormatedText(string Cont, int Length)
    {
        int rLoc = Length - Cont.Length;
        if (rLoc < 0)
        {
            Cont = Cont.Substring(0, Length);
        }
        else
        {
            for (int nos = 1; nos <= rLoc; nos++) // Length
                Cont = " " + Cont;
        }
        return (Cont);
    }
    private string GetCenterdFormatedText(string Cont, int Length)
    {
        int rLoc = Length - Cont.Length;
        if (rLoc < 0)
        {
            Cont = Cont.Substring(0, Length);
        }
        else
        {
            for (int nos = 0; nos < Length; nos++)
                Cont = " " + Cont;
        }
        return (Cont);
    }
    public void Close()
    {
        rdr.Close();
    }
    public void PrintLine()
    {
        int i;
        string Lstr = "";

        for (i = 1; i < 168; i++) //162=>LandScape  81=>Protat
        {
            Lstr = Lstr + "-";
        }

        rdr.WriteLine(Lstr);

    }
}