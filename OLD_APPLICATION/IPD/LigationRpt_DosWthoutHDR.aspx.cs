using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

public partial class IPD_LigationRpt_DosWthoutHDR : System.Web.UI.Page
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
                Path = Server.MapPath("Print/LigationRpt_DosWthoutHDR.txt");

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
                Close();
                ScriptManager.RegisterStartupScript(this, this.GetType(), "msg", "alert('Report Generated successfully');", true);

                string command = String.Empty;

                command = "print " + Path;
                Response.ContentType = "application/txt";
                string filePath = "LigationRpt_DosWthoutHDR.txt";
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
    //    Print_Header();
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
            rdr.WriteLine(AllowSpace(0) + " Dear Sir / Madam,  ");
            rdr.WriteLine(AllowSpace(0) + " I am married and my husband alive. 22 years of my age, my husband _________ years of age. ________ Son of _________ of us have daughters  ");

            rdr.WriteLine(AllowSpace(0) + " at home. ________ Years from the current age of the youngest child. So to make the necessary arrangements to stop children of  ");
            rdr.WriteLine(AllowSpace(0) + " bandhyatbakarana pray. By the way you are:");
            SkipLine(1);
            rdr.WriteLine(AllowSpace(0) + " (1) This bandhatbakaranera my own decision., I decided spontaneously to this. This has forced me to or not having one. ");
            SkipLine(1);
            rdr.WriteLine(AllowSpace(0) + " (2) Bandhyatbakarana or child I knew about the process of closing the other. Other process has meant to me, as well. ");
            SkipLine(1);
            rdr.WriteLine(AllowSpace(0) + " (3) Bandhyatbakarana to stop or if the child requires that the mental and physical health, it has meant to me, as well. After listening , ");
            rdr.WriteLine(AllowSpace(0) + "     to all of that I thought, this is the moment I stop or baby into bandhyatbakarana appropriate side. ");
            SkipLine(1);
            rdr.WriteLine(AllowSpace(0) + " (4) I well know that the purpose of closing bandhyatbakarana or child. Baccadharana also know that someday I will be able to do this after ");
            rdr.WriteLine(AllowSpace(0) + "     the operation. ");
            SkipLine(1);
            rdr.WriteLine(AllowSpace(0) + " (5) I know that this operation is not quite correct . This bandhyatbakarana or have any special reason baccabandhera after the operation can ");
            rdr.WriteLine(AllowSpace(0) + "     become pregnant again . Babu, learned from doctors that are mentioned in this exceptional or rare cases of medical science . So for me, my ");
            rdr.WriteLine(AllowSpace(0) + "     husband , my other relatives daktarababu or institution concerned kimbba contacts will not be liable or not . After this operation I have ");
            rdr.WriteLine(AllowSpace(0) + "     my regularperiod of fifteen days of the occurrence of an irregularity, interruption or the institution concerned shall daktarababuke .");
            SkipLine(1);
            rdr.WriteLine(AllowSpace(0) + " (6) Prior to my husband karanani baccabandhera operations. ");
            SkipLine(1);
            rdr.WriteLine(AllowSpace(0) + " (7) I know there are many risks to this operation may even be killed. ");
            SkipLine(1);
            rdr.WriteLine(AllowSpace(0) + " (8) This bandhyatbakarana or closing operations associated daktarababu child like me any sort of advantage to his senses and medicine as directed");
            rdr.WriteLine(AllowSpace(0) + "     pradaei babu business operation after doctors regularly check - will show up or not. If you do not have to come if something strange or  ");
            rdr.WriteLine(AllowSpace(0) + "     difficult forhim not to be responsible for this institution or daktarababu.");
            SkipLine(1);
            rdr.WriteLine(AllowSpace(0) + " (9) This bandhyatbakarana or closing operations associated daktarababu child like me any sort of advantage to his senses and to apply the medicine");
            rdr.WriteLine(AllowSpace(0) + "     and can also, in that case, I do not have any objections. ");
            SkipLine(1);
            rdr.WriteLine(AllowSpace(0) + " (10) I have been fully informed - to me that would be applied to all medicines should be given to the use of , or after , the long-term effects they ");
            rdr.WriteLine(AllowSpace(0) + "      may have sbaplameyadi or , in some cases, these adverse reactions may be serious - but no way for one person to be responsible daktarababu does");
            rdr.WriteLine(AllowSpace(0) + "      not . Hopefully, in the context of this application, you can make me cum like a child at the time of closing my bandhyatbakaranera ");
            rdr.WriteLine(AllowSpace(0) + "      aparesanera necessary procedural steps to take .");
            SkipLine(1);
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