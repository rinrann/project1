using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using System.Data;

/// <summary>
/// Summary description for AdmissionAllForms
/// </summary>
public class AdmissionAllForms
{
    public AdmissionAllForms(string con)
    {
        conString = con;
    }

    private string conString;
    private SqlConnection theConnection;
    private SqlCommand theCommand;
    private SqlDataAdapter theAdapter;
    private DataTable hospitalTable;


    //public DataTable GetAdmissionSlip(string regno)
    //{
    //    // Connection.
    //    theConnection = new SqlConnection();
    //    theConnection.ConnectionString = conString;

    //    // Command.
    //    theCommand = new SqlCommand();
    //    theCommand.Connection = theConnection;
    //    theCommand.CommandText = "select *,CONVERT(varchar,pr.AdmissionDate,103) ADate,convert(varchar,pr.dob,103)  dob1  from GN_Diagnosis dia, dbo.GN_PatientReg pr,IPD_BedAllocation b,dbo.GN_DoctorMaster d,IPD_BedMaster bm where dia.DiagnosisId=pr.Diagnosis and bm.BedNo=b.BedNo and pr.underDoctor=d.doc_id and pr.PatientReg=b.PatientReg and pr.PatientReg='" + regno + "'";
    //    theCommand.CommandType = CommandType.Text;

    //    // Adapter.
    //    theAdapter = new SqlDataAdapter();
    //    theAdapter.SelectCommand = theCommand;

    //    // Datatable.
    //    hospitalTable = new DataTable();
    //    theAdapter.Fill(hospitalTable); // Fill data into data table.

    //    // Clean up.
    //    theConnection.Dispose();
    //    theCommand.Dispose();
    //    theAdapter.Dispose();

    //    return hospitalTable;
    //}

    public DataTable GetPatientAbortion(string regno)
    {
        // Connection.
        theConnection = new SqlConnection();
        theConnection.ConnectionString = conString;

        // Command.
        theCommand = new SqlCommand();
        theCommand.Connection = theConnection;
        theCommand.CommandText = "select *,CONVERT(varchar,pr.AdmissionDate,103) ADate  from  dbo.GN_PatientReg pr,IPD_BedAllocation b,dbo.GN_DoctorMaster d,IPD_BedMaster bm where bm.BedNo=b.BedNo and pr.underDoctor=d.doc_id and pr.PatientReg=b.PatientReg and pr.PatientReg='" + regno + "'";
        theCommand.CommandType = CommandType.Text;

        // Adapter.
        theAdapter = new SqlDataAdapter();
        theAdapter.SelectCommand = theCommand;

        // Datatable.
        hospitalTable = new DataTable();
        theAdapter.Fill(hospitalTable); // Fill data into data table.

        // Clean up.
        theConnection.Dispose();
        theCommand.Dispose();
        theAdapter.Dispose();

        return hospitalTable;
    }


    public DataTable GetPatientdetails(string regno)
    {
        // Connection.
        theConnection = new SqlConnection();
        theConnection.ConnectionString = conString;

        // Command.
        theCommand = new SqlCommand();
        theCommand.Connection = theConnection;
        theCommand.CommandText = "select *,CONVERT(varchar,pr.AdmissionDate,103) ADate  from  dbo.GN_PatientReg pr,IPD_BedAllocation b,dbo.GN_DoctorMaster d,IPD_BedMaster bm,GN_Diagnosis dia where bm.BedNo=b.BedNo and pr.underDoctor=d.doc_id and pr.PatientReg=b.PatientReg and pr.PatientReg='" + regno + "' and pr.Diagnosis=dia.DiagnosisId";
        theCommand.CommandType = CommandType.Text;

        // Adapter.
        theAdapter = new SqlDataAdapter();
        theAdapter.SelectCommand = theCommand;

        // Datatable.
        hospitalTable = new DataTable();
        theAdapter.Fill(hospitalTable); // Fill data into data table.

        // Clean up.
        theConnection.Dispose();
        theCommand.Dispose();
        theAdapter.Dispose();

        return hospitalTable;
    }



    public DataTable GetPatientOperationConsent(string regno)
    {
        // Connection.
        theConnection = new SqlConnection();
        theConnection.ConnectionString = conString;

        // Command.
        theCommand = new SqlCommand();
        theCommand.Connection = theConnection;
        theCommand.CommandText = "select *,CONVERT(varchar,pr.AdmissionDate,103) ADate  from  dbo.GN_PatientReg pr,GN_District dis,IPD_BedAllocation b,dbo.GN_DoctorMaster d,IPD_BedMaster bm where pr.District=dis.ID and bm.BedNo=b.BedNo and pr.underDoctor=d.doc_id and pr.PatientReg=b.PatientReg and pr.PatientReg='" + regno + "'";
        theCommand.CommandType = CommandType.Text;

        // Adapter.
        theAdapter = new SqlDataAdapter();
        theAdapter.SelectCommand = theCommand;

        // Datatable.
        hospitalTable = new DataTable();
        theAdapter.Fill(hospitalTable); // Fill data into data table.

        // Clean up.
        theConnection.Dispose();
        theCommand.Dispose();
        theAdapter.Dispose();

        return hospitalTable;
    }

    public DataTable GetPatientLigation(string regno)
    {
        // Connection.
        theConnection = new SqlConnection();
        theConnection.ConnectionString = conString;

        // Command.
        theCommand = new SqlCommand();
        theCommand.Connection = theConnection;
        theCommand.CommandText = "select *,CONVERT(varchar,pr.AdmissionDate,103) ADate  from  dbo.GN_PatientReg pr,GN_District dis,IPD_BedAllocation b,dbo.GN_DoctorMaster d,IPD_BedMaster bm where pr.District=dis.ID and bm.BedNo=b.BedNo and pr.underDoctor=d.doc_id and pr.PatientReg=b.PatientReg and pr.PatientReg='" + regno + "'";
        theCommand.CommandType = CommandType.Text;

        // Adapter.
        theAdapter = new SqlDataAdapter();
        theAdapter.SelectCommand = theCommand;

        // Datatable.
        hospitalTable = new DataTable();
        theAdapter.Fill(hospitalTable); // Fill data into data table.

        // Clean up.
        theConnection.Dispose();
        theCommand.Dispose();
        theAdapter.Dispose();

        return hospitalTable;
    }


    public DataTable GridPopup(string floor, string roomtype, string room, string bedno, string name)
    {
        if (floor == "" || floor == "0")
            floor = "null";
        if (bedno == "" || bedno == "0")
            bedno = "null";
        if (room == "" || room == "0")
            room = "null";
        if (roomtype == "" || roomtype == "0")
            roomtype = "null";
        if (name == "")
            name = "null";
        else
            name = "'" + name + "'";
        // Connection.
        theConnection = new SqlConnection();
        theConnection.ConnectionString = conString;

        // Command.
        theCommand = new SqlCommand();
        theCommand.Connection = theConnection;

        theCommand.CommandText = "exec sp_Ipdpatietlist  " + floor + "," + room + "," + roomtype + "," + bedno + "," + name + "";
        theCommand.CommandType = CommandType.Text;


        // Adapter.
        theAdapter = new SqlDataAdapter();
        theAdapter.SelectCommand = theCommand;

        // Datatable.
        hospitalTable = new DataTable();
        theAdapter.Fill(hospitalTable); // Fill data into data table.

        // Clean up.
        theConnection.Dispose();
        theCommand.Dispose();
        theAdapter.Dispose();

        return hospitalTable;
    }


    public DataTable GridForAllReport(string name, string co, string address, string bedno, string date,string compcode)
    {
        if (name == "")
            name = "null";
        else
            name = "'" + name + "'";

        if (co == "")
            co = "null";
        else
            co = "'" + co + "'";


        if (address == "")
            address = "null";
        else
            address = "'" + address + "'";

        if (bedno == "" || bedno=="0")
            bedno = "null"; 

        if (date == "")
            date = "null";
        else
            date = "'" + date + "'";

        // Connection.
        theConnection = new SqlConnection();
        theConnection.ConnectionString = conString;

        // Command.
        theCommand = new SqlCommand();
        theCommand.Connection = theConnection;

        theCommand.CommandText = "exec [sp_IpdpatietlistAllReport]  " + name + "," + co + "," + address + "," + bedno + "," + date + ",'"+compcode+"'";
        theCommand.CommandType = CommandType.Text;


        // Adapter.
        theAdapter = new SqlDataAdapter();
        theAdapter.SelectCommand = theCommand;

        // Datatable.
        hospitalTable = new DataTable();
        theAdapter.Fill(hospitalTable); // Fill data into data table.

        // Clean up.
        theConnection.Dispose();
        theCommand.Dispose();
        theAdapter.Dispose();

        return hospitalTable;
    }

    public DataTable DropdownFloor()
    {

        // Connection.
        theConnection = new SqlConnection();
        theConnection.ConnectionString = conString;

        // Command.
        theCommand = new SqlCommand();
        theCommand.Connection = theConnection;

        theCommand.CommandText = "select * from dbo.IPD_FloorMaster where status=1";
        theCommand.CommandType = CommandType.Text;


        // Adapter.
        theAdapter = new SqlDataAdapter();
        theAdapter.SelectCommand = theCommand;

        // Datatable.
        hospitalTable = new DataTable();
        theAdapter.Fill(hospitalTable); // Fill data into data table.

        // Clean up.
        theConnection.Dispose();
        theCommand.Dispose();
        theAdapter.Dispose();

        return hospitalTable;
    }

    public DataTable DropdownHighRiskReport()
    {

        // Connection.
        theConnection = new SqlConnection();
        theConnection.ConnectionString = conString;

        // Command.
        theCommand = new SqlCommand();
        theCommand.Connection = theConnection;

        theCommand.CommandText = "select * from dbo.GN_HighRiskReport where Status=1 order by HighRiskReport ";
        theCommand.CommandType = CommandType.Text;


        // Adapter.
        theAdapter = new SqlDataAdapter();
        theAdapter.SelectCommand = theCommand;

        // Datatable.
        hospitalTable = new DataTable();
        theAdapter.Fill(hospitalTable); // Fill data into data table.

        // Clean up.
        theConnection.Dispose();
        theCommand.Dispose();
        theAdapter.Dispose();

        return hospitalTable;
    }



    public DataTable DropdownReport(string type,string language)
    {

        // Connection.
        theConnection = new SqlConnection();
        theConnection.ConnectionString = conString;

        // Command.
        theCommand = new SqlCommand();
        theCommand.Connection = theConnection;
        if(type=="1")
            theCommand.CommandText = "SELECT fm.ID,fm.FormName FROM IPD_FormMaster fm WHERE fm.FormType='" + type + "' AND fm.Status=1 AND fm.Language='" + language + "' union select 0,'Admission Slip'";
        else
            theCommand.CommandText = "SELECT fm.ID,fm.FormName FROM IPD_FormMaster fm WHERE fm.FormType='" + type + "' AND fm.Status=1 AND fm.Language='" + language + "'";
        theCommand.CommandType = CommandType.Text;


        // Adapter.
        theAdapter = new SqlDataAdapter();
        theAdapter.SelectCommand = theCommand;

        // Datatable.
        hospitalTable = new DataTable();
        theAdapter.Fill(hospitalTable); // Fill data into data table.

        // Clean up.
        theConnection.Dispose();
        theCommand.Dispose();
        theAdapter.Dispose();

        return hospitalTable;
    }


    public DataTable Dropdownbed(string floor, string roomtype, string room)
    {

        // Connection.
        theConnection = new SqlConnection();
        theConnection.ConnectionString = conString;

        // Command.
        theCommand = new SqlCommand();
        theCommand.Connection = theConnection;

        theCommand.CommandText = "select * from IPD_BedMaster bm where bm.FloorID='" + floor + "' and bm.RoomCategoryID='" + roomtype + "' and bm.RoomID='" + room + "' and bm.status=1";
        theCommand.CommandType = CommandType.Text;


        // Adapter.
        theAdapter = new SqlDataAdapter();
        theAdapter.SelectCommand = theCommand;

        // Datatable.
        hospitalTable = new DataTable();
        theAdapter.Fill(hospitalTable); // Fill data into data table.

        // Clean up.
        theConnection.Dispose();
        theCommand.Dispose();
        theAdapter.Dispose();

        return hospitalTable;
    }


    public DataTable DropdownbedAll(string compcode)
    {

        // Connection.
        theConnection = new SqlConnection();
        theConnection.ConnectionString = conString;

        // Command.
        theCommand = new SqlCommand();
        theCommand.Connection = theConnection;

        theCommand.CommandText = "select bm.BedNo,bm.BedNoText from IPD_BedMaster bm where  bm.Allotted=1 and bm.status=1 and bm.compcode='"+compcode+"'";
        theCommand.CommandType = CommandType.Text;


        // Adapter.
        theAdapter = new SqlDataAdapter();
        theAdapter.SelectCommand = theCommand;

        // Datatable.
        hospitalTable = new DataTable();
        theAdapter.Fill(hospitalTable); // Fill data into data table.

        // Clean up.
        theConnection.Dispose();
        theCommand.Dispose();
        theAdapter.Dispose();

        return hospitalTable;
    }

    public DataTable Dropdownroom(string floor, string roomcatid)
    {

        // Connection.
        theConnection = new SqlConnection();
        theConnection.ConnectionString = conString;

        // Command.
        theCommand = new SqlCommand();
        theCommand.Connection = theConnection;

        theCommand.CommandText = "select * from  IPD_RoomMaster rm where rm.status=1 AND rm.FloorID= '" + floor + "' and  rm.RoomCategoryID='" + roomcatid + "'";
        theCommand.CommandType = CommandType.Text;


        // Adapter.
        theAdapter = new SqlDataAdapter();
        theAdapter.SelectCommand = theCommand;

        // Datatable.
        hospitalTable = new DataTable();
        theAdapter.Fill(hospitalTable); // Fill data into data table.

        // Clean up.
        theConnection.Dispose();
        theCommand.Dispose();
        theAdapter.Dispose();

        return hospitalTable;
    }

    public DataTable Dropdownroomtype()
    {

        // Connection.
        theConnection = new SqlConnection();
        theConnection.ConnectionString = conString;

        // Command.
        theCommand = new SqlCommand();
        theCommand.Connection = theConnection;

        theCommand.CommandText = "select * from dbo.IPD_RoomType where status=1";
        theCommand.CommandType = CommandType.Text;


        // Adapter.
        theAdapter = new SqlDataAdapter();
        theAdapter.SelectCommand = theCommand;

        // Datatable.
        hospitalTable = new DataTable();
        theAdapter.Fill(hospitalTable); // Fill data into data table.

        // Clean up.
        theConnection.Dispose();
        theCommand.Dispose();
        theAdapter.Dispose();

        return hospitalTable;
    }

    public DataTable GetPatient(string reg)
    {

        // Connection.
        theConnection = new SqlConnection();
        theConnection.ConnectionString = conString;

        // Command.
        theCommand = new SqlCommand();
        theCommand.Connection = theConnection;

        theCommand.CommandText = "select pr.patient_name,pr.PatientReg,CONVERT(varchar,pr.AdmissionDate,103) ADate,bm.BedNoText from IPD_BedMaster bm,IPD_BedAllocation ba,GN_PatientReg pr where pr.PatientReg=ba.PatientReg and ba.BedNo=bm.BedNo and bm.Allotted =1 and  pr.PatientReg='" + reg + "'";
        theCommand.CommandType = CommandType.Text;


        // Adapter.
        theAdapter = new SqlDataAdapter();
        theAdapter.SelectCommand = theCommand;

        // Datatable.
        hospitalTable = new DataTable();
        theAdapter.Fill(hospitalTable); // Fill data into data table.

        // Clean up.
        theConnection.Dispose();
        theCommand.Dispose();
        theAdapter.Dispose();

        return hospitalTable;
    }
}