using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

public partial class IPD_AbortionDOS_RptWhtoutHDR : System.Web.UI.Page
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
        //Print_Header();
        HEADER_Detail();
        //Report();
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
                //Print_Header();


                // ============================== 07/05/14 ================
            }
            rdr.WriteLine(AllowSpace(205) + "গর্ভপাতের সম্মতি পত্র");
            rdr.WriteLine(AllowSpace(208) + "রোগীর বিবরণ   ");
            PrintLine();
            rdr.WriteLine(AllowSpace(0) + " নিবন্ধ সংখ্যা " + AllowSpace(28) + GetFormatedText(RptDt.Rows[0]["PatientReg"].ToString(), 13) + AllowSpace(34) + " রোগীর নাম " + AllowSpace(22) + GetFormatedText(RptDt.Rows[0]["patient_name"].ToString(), 18) + AllowSpace(15) + " বয়স " + AllowSpace(75) + GetRightFormatedText(RptDt.Rows[0]["age"].ToString(), 2));
            PrintLine();
            rdr.WriteLine(AllowSpace(0) + " গার্ডিয়ান নাম  " + AllowSpace(25) + GetFormatedText(RptDt.Rows[0]["guardian_name"].ToString(), 16) + AllowSpace(31) + " ঠিকানা " + AllowSpace(29) + GetFormatedText(RptDt.Rows[0]["vill_city"].ToString(), 13) + AllowSpace(20) + " ফোন নং " + AllowSpace(50) + GetRightFormatedText(RptDt.Rows[0]["PhNo1"].ToString(), 15));
            PrintLine();
            rdr.WriteLine(AllowSpace(0) + " বিছানা সংখ্যা " + AllowSpace(26) + GetRightFormatedText(RptDt.Rows[0]["BedNoText"].ToString(), 9) + AllowSpace(38) + " ভর্তি তারিখ:" + AllowSpace(23) + GetFormatedText(RptDt.Rows[0]["ADate"].ToString(), 10) + AllowSpace(75) + " ভর্তি টাইম" + AllowSpace(58) + GetRightFormatedText(RptDt.Rows[0]["FromTime"].ToString(), 9));
            PrintLine();
            rdr.WriteLine(AllowSpace(0) + " যিনি ভর্তি করছেন" + AllowSpace(21) + GetFormatedText(RptDt.Rows[0]["guardian_name"].ToString(), 16) + AllowSpace(31) + " সম্পর্ক" + AllowSpace(32) + GetFormatedText(RptDt.Rows[0]["relation"].ToString(), 14) + AllowSpace(18) + " ডাক্তার বাবুর নাম" + AllowSpace(8) + GetRightFormatedText(RptDt.Rows[0]["doc_name"].ToString(), 25));
            PrintLine();
            SkipLine(2);
            rdr.WriteLine(AllowSpace(0) + " ভর্তির কারণ  :" + AllowSpace(15) + GetRightFormatedText(RptDt.Rows[0]["Diagnosis"].ToString(), 3));
            SkipLine(2);
            rdr.WriteLine(AllowSpace(0) + "আমার গর্ভে যে সন্তান আছে তা আমি জানি এবং ডাক্তারবাবুও আমাকে পরীক্ষা করে সে কথা আমাকে জানালেন । কিন্তু আমি আমার____________ কারণে আমার এই ");
            rdr.WriteLine(AllowSpace(0) + "গর্ভস্থ সন্তান নষ্ট করে দিতে চাই ।  ");

            rdr.WriteLine(AllowSpace(0) + " ডাক্তারবাবু গর্ভপাত করানোর পদ্ধতি ও তার সম্ভাব্য সমস্ত জটিলতা সম্বন্ধে আমাকে জানিয়েছেন । এমন কি কোনও কোনও ক্ষেত্রে প্রছুর রক্তস্রাব ,পেটের অন্ত্রে ক্ষতি বা ");
            rdr.WriteLine(AllowSpace(0) + " জীবাণু সংক্রামনের মতো যাতে জীবণ সংশয় পর্যন্ত হতে পারে বা জরায়ূ-কে আপারেশনের মাধ্যমে বাদ দিতে পর্যন্ত হতে পারে । এসব ভালভাবে জেনে এই গর্ভপাতে অনুমতি  ");
            rdr.WriteLine(AllowSpace(0) + " দিলাম । আরও জানলাম যে , চিকিৎসা বিজ্ঞানের তথ্যানুযায়ী গর্ভপাতর ফলে ভবিষ্যতে আবার বাচ্চা না-আসার সম্ভাবনা সহ সূদূর প্রসারী আরও অনেক কুফল আছে । ");
            rdr.WriteLine(AllowSpace(0) + " এসবের জন্য ডাক্তারবাবু দায়ী থাকবেন না। ");
            rdr.WriteLine(AllowSpace(0) + " এই সম্মতি পত্রে স্বেচ্ছায়, সমস্ত কথার মানে বুঝে সই করলাম ।  ");
            //rdr.WriteLine(AllowSpace(0) + " About the abortion procedure and its potential complications daktarababu told me. Even at this Consent Form, signed  ");
            //rdr.WriteLine(AllowSpace(0) + " by all mean I understand.");
            rdr.WriteLine(AllowSpace(150) + " ইতি---------  " + AllowSpace(2) + GetFormatedText(RptDt.Rows[0]["patient_name"].ToString(), 18));
            SkipLine(1);
            rdr.WriteLine(AllowSpace(38) + " স্বাক্ষর/টিপ        " + AllowSpace(48) + "পুরো  নাম " + AllowSpace(42) + "ঠিকানা" + AllowSpace(42) + "সম্পর্ক");
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