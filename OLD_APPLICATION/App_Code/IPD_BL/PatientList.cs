using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using System.Data;

/// <summary>
/// Summary description for PatientList123
/// </summary>
public class PatientList
{
    public PatientList(string con)
    {
        conString = con;
    }

    private string conString;
    private SqlConnection theConnection;
    private SqlCommand theCommand;
    private SqlDataAdapter theAdapter;
    private DataTable hospitalTable;

    public DataTable GridPopup(string floor, string roomtype, string room, string bedno, string name,string compcode)
    {
        if (floor == "" || floor == "0")
        {
            floor = "null";
        }
        else
        {
            floor = "'"+floor+"'";
        }
        if (bedno == "" || bedno == "0")
        {
            bedno = "null";
        }
        else
        {
            bedno="'"+bedno+"'";
        }
        if (room == "" || room == "0")
        {
            room = "null";
        }
        else
        {
            room="'"+room+"'";
        }
        if (roomtype == "" || roomtype == "0")
        {
            roomtype = "null";
        }
        else
        {
            roomtype="'"+roomtype+"'";
        }
        if (name == "")
        {
            name = "null";
        }
        else
        {
            name="'"+name+"'";
        }
        // Connection.
        theConnection = new SqlConnection();
        theConnection.ConnectionString = conString;

        // Command.
        theCommand = new SqlCommand();
        theCommand.Connection = theConnection;

        theCommand.CommandText = "exec sp_Ipdpatietlist  " + floor + "," + room + "," + roomtype + "," + bedno + "," + name + ",'"+compcode+"'";
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


    public DataTable GetPatientBedDtls(string regno,string compcode)
    {
        // Connection.
        theConnection = new SqlConnection();
        theConnection.ConnectionString = conString;

        // Command.
        theCommand = new SqlCommand();
        theCommand.Connection = theConnection;

        theCommand.CommandText = "select CONVERT(varchar,ba.FromDate,103) +'  '+ba.FromTime Bed from IPD_BedAllocation ba,GN_PatientReg pr  where ba.compcode=pr.compcode and pr.PatientReg=ba.PatientReg and pr.LedgerId=ba.LedgerId and  ba.ToDate is null and pr.PatientType='I' and pr.PatientReg='" + regno + "' and pr.compcode='"+compcode+"'";
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

    public DataTable GetDocVisitDtls(string regno,string compcode)
    {
        // Connection.
        theConnection = new SqlConnection();
        theConnection.ConnectionString = conString;

        // Command.
        theCommand = new SqlCommand();
        theCommand.Connection = theConnection;

        theCommand.CommandText = "select CONVERT(VARCHAR,dv.Date,103) +'  '+dv.Time Doctorvisit from IPD_PatientDoctorVisit dv,GN_PatientReg pr where dv.compcode=pr.compcode and pr.PatientReg=dv.PatientReg and dv.Date>=pr.AdmissionDate and pr.compcode='"+compcode+"' and  pr.PatientReg='" + regno + "' and    dv.Time=(select MAX(CONVERT(time,dv.Time))   from IPD_PatientDoctorVisit dv where dv.compcode='"+compcode+"' and  dv.PatientReg='" + regno + "' and dv.Date =(select MAX(dv.Date)   from dbo.IPD_PatientDoctorVisit dv where dv.compcode='"+compcode+"' and  dv.PatientReg='" + regno + "')) ";   
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

    public DataTable GetMedicineDatetime(string regno,string compcode)
    {
        // Connection.
        theConnection = new SqlConnection();
        theConnection.ConnectionString = conString;

        // Command.
        theCommand = new SqlCommand();
        theCommand.Connection = theConnection;

        theCommand.CommandText = "select CONVERT(VARCHAR,pm.IssueDate,103) +'  '+pm.Time Medicine from IPD_PatientMedicine pm,GN_PatientReg pr where pm.compcode=pr.compcode and  pr.PatientReg=pm.PatientReg and pm.IssueDate>=pr.AdmissionDate and pr.compcode='"+compcode+"'  and pr.PatientReg='" + regno + "' and  pm.Time=(select MAX(CONVERT(time,pm.Time))  from IPD_PatientMedicine pm where pm.compcode='"+compcode+"' and pm.PatientReg='" + regno + "'  and pm.IssueDate =(select MAX(pm.IssueDate) from  dbo.IPD_PatientMedicine pm where pm.compcode='"+compcode+"' and pm.PatientReg='" + regno + "'))";
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

    public DataTable GetServiceDatetime(string regno,string compcode)
    {
        // Connection.
        theConnection = new SqlConnection();
        theConnection.ConnectionString = conString;

        // Command.
        theCommand = new SqlCommand();
        theCommand.Connection = theConnection;

        theCommand.CommandText = "select CONVERT(VARCHAR,sa.IssueDate,103) +'  '+sa.Time Service from IPD_AddServices sa,GN_PatientReg pr where sa.compcode=pr.compcode and pr.PatientReg=sa.PatientReg and pr.compcode='"+compcode+"' and sa.IssueDate>=pr.AdmissionDate and sa.Time=(select MAX(CONVERT(time,sa.Time))  from IPD_AddServices  sa where sa.compcode='"+compcode+"' and sa.PatientReg='" + regno + "' and sa.IssueDate =(select MAX(sa.IssueDate) from dbo.IPD_AddServices sa  where sa.compcode='"+compcode+"' and sa.PatientReg='" + regno + "')) and sa.PatientReg='" + regno + "'";
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

    public DataTable GetServicePresDatetime(string regno, string compcode)
    {
        // Connection.
        theConnection = new SqlConnection();
        theConnection.ConnectionString = conString;

        // Command.
        theCommand = new SqlCommand();
        theCommand.Connection = theConnection;

        theCommand.CommandText = "select CONVERT(VARCHAR,sa.IssueDate,103) +'  '+sa.Time Service from IPD_AddServices sa,GN_PatientReg pr where sa.compcode=pr.compcode and pr.PatientReg=sa.PatientReg and pr.compcode='" + compcode + "' and sa.IssueDate>=pr.AdmissionDate and sa.Time=(select MAX(CONVERT(time,sa.Time))  from IPD_AddServices  sa where sa.compcode='" + compcode + "' and sa.PatientReg='" + regno + "' and sa.IssueDate =(select MAX(sa.IssueDate) from dbo.IPD_AddServices sa  where sa.compcode='" + compcode + "' and sa.PatientReg='" + regno + "')) and sa.PatientReg='" + regno + "'";
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

    public DataTable GetConsumableDatetime(string regno,string compcode)
    {
        // Connection.
        theConnection = new SqlConnection();
        theConnection.ConnectionString = conString;

        // Command.
        theCommand = new SqlCommand();
        theCommand.Connection = theConnection;

        theCommand.CommandText = "select CONVERT(VARCHAR,c.IssueDate,103) +'  '+c.Time Consumable from IPD_PatientConsumables c,GN_PatientReg pr  where c.compcode=pr.compcode and pr.compcode='"+compcode+"' and c.PatientReg=pr.PatientReg and c.IssueDate>=pr.AdmissionDate and c.Time=(select MAX(CONVERT(time,c.Time))  from IPD_PatientConsumables  c where c.compcode='"+compcode+"' and c.PatientReg='" + regno + "'  and c.IssueDate =(select MAX(c.IssueDate) from dbo.IPD_PatientConsumables c where c.compcode='"+compcode+"' and c.PatientReg='" + regno + "'))  and pr.PatientReg='" + regno + "'"; 
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

    public DataTable GetClinicalFindingDatetime(string regno,string compcode)
    {
        // Connection.
        theConnection = new SqlConnection();
        theConnection.ConnectionString = conString;

        // Command.
        theCommand = new SqlCommand();
        theCommand.Connection = theConnection;

        theCommand.CommandText = "select CONVERT(VARCHAR,c.Date,103) +'  '+c.Time ClinicalFinding from IPD_ClinicalFinding c,GN_PatientReg pr where c.compcode=pr.compcode and pr.compcode='"+compcode+"' and pr.PatientReg=c.PatientReg and c.Date>=pr.AdmissionDate and c.Time=(select MAX(CONVERT(time,c.Time))  from IPD_ClinicalFinding  c where c.compcode='"+compcode+"' and c.PatientReg='" + regno + "'  and c.Date =(select MAX(c.Date) from dbo.IPD_ClinicalFinding c where c.compcode='"+compcode+"' and c.PatientReg='" + regno + "')) and pr.PatientReg='" + regno + "'";  
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

    public DataTable GetSisterAyaDatetime(string regno,string compcode)
    {
        // Connection.
        theConnection = new SqlConnection();
        theConnection.ConnectionString = conString;

        // Command.
        theCommand = new SqlCommand();
        theCommand.Connection = theConnection;

        theCommand.CommandText = "  SELECT Substring(CONVERT(VARCHAR,MAX(c.Date),103)+' '+CONVERT(VARCHAR,MAX(Convert(time,c.Time))),1,16) SisterAya  from    IPD_PatientSisterAyaCharges c,GN_PatientReg pr where c.compcode=pr.compcode and pr.compcode='"+compcode+"' and c.PatientReg=pr.PatientReg and c.Date>=pr.AdmissionDate      and c.PatientReg='" + regno + "' ";
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


    public DataTable GetOperationDatetime(string regno,string compcode,string yearcode)
    {
        // Connection.
        theConnection = new SqlConnection();
        theConnection.ConnectionString = conString;

        // Command.
        theCommand = new SqlCommand();
        theCommand.Connection = theConnection;

        theCommand.CommandText = "select CONVERT(VARCHAR,c.OperationDate,103) +'  '+c.StartTime OT from IPD_OTRequisition c where c.compcode='"+compcode+"' and c.yearcode='"+yearcode+"' and c.StartTime=(select MAX(CONVERT(time,c.startTime))  from IPD_OTRequisition  c,GN_PatientReg pr  where c.compcode=pr.compcode and  c.compcode='"+compcode+"' and c.yearcode='"+yearcode+"' and pr.PatientReg=c.PatientRegId and c.OperationDate>=pr.AdmissionDate and  c.PatientRegId='" + regno + "' and c.OperationDate =(select MAX(c.OperationDate) from dbo.IPD_OTRequisition c  where c.compcode='"+compcode+"' and c.yearcode='"+yearcode+"' and c.PatientRegId='" + regno + "')) AND  c.PatientRegId='" + regno + "'  union select 'Rejected' OT   from IPD_OTRequisition c where c.StartTime=(select MAX(CONVERT(time,c.startTime))   from IPD_OTRequisition  c ,GN_PatientReg pr  where pr.PatientReg=c.PatientRegId and c.OperationDate>=pr.AdmissionDate and  c.PatientRegId='" + regno + "' and    c.OperationDate =(select MAX(c.OperationDate) from dbo.IPD_OTRequisition c where c.PatientRegId='" + regno + "')) AND c.PatientRegId='" + regno + "' and c.Status='Cancel'"; 
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

    public DataTable GetLabDatetime(string regno,string compcode)
    {
        // Connection.
        theConnection = new SqlConnection();
        theConnection.ConnectionString = conString;

        // Command.
        theCommand = new SqlCommand();
        theCommand.Connection = theConnection;

        theCommand.CommandText = "select CONVERT(VARCHAR,c.TestDate,103) +'  '+c.Time Lab from PH_PatientReq c,GN_patientReg pr where c.compcode=pr.compcode and pr.compcode='"+compcode+"' and pr.PatientReg=c.RegistrationNo and c.TestDate>=pr.AdmissionDate and c.Time=(select MAX(CONVERT(time,c.Time))  from PH_PatientReq  c where c.compcode='"+compcode+"' and c.RegistrationNo='" + regno + "' and  c.TestDate =(select MAX(c.TestDate) from dbo.PH_PatientReq c where c.compcode='"+compcode+"' and c.RegistrationNo='" + regno + "'))  and pr.PatientReg='" + regno + "'";
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

    public DataTable GetInsDetls(string regno, string compcode,string yearcode,string type)
    {
        // Connection.
        theConnection = new SqlConnection();
        theConnection.ConnectionString = conString;

        // Command.
        theCommand = new SqlCommand();
        theCommand.Connection = theConnection;

        
        theCommand.CommandText = "Select CONVERT(VARCHAR,EntryDate,103),InstrumentId,OperationReqID from OT_InstrumentCost  where compcode='" + compcode + "' and yearcode='" + yearcode + "' and PatientReg='"+regno+"' and Ins_type='"+type+"'";
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

    public int  GetToDoTask(string regno,string compcode)
    {
        string LedgerId = "";
        int CheckFlag = 0;
        // Connection.
        theConnection = new SqlConnection();
        if (conString != "")
        {
            theConnection.ConnectionString = conString;
            theConnection.Open();
        }

        // Command.
        theCommand = new SqlCommand();
        theCommand.Connection = theConnection;


        using (IDbTransaction tran = theConnection.BeginTransaction())
        {
            try
            {
                // transactional code...
                using (theCommand = theConnection.CreateCommand())
                {
                    theCommand.CommandText = "SELECT al.LedgerID FROM AC_Ledger al WHERE al.LedgerFK='" + regno + "' AND al.ActiveStatus=1 and al.compcode='"+compcode+"'";
                    theCommand.Transaction = tran as SqlTransaction;
                    SqlDataReader theReader = theCommand.ExecuteReader();
                    theReader.Read();
                    if(theReader.HasRows)
                    LedgerId = theReader[0].ToString();
                    theReader.Close();


                    theCommand.CommandText = "SELECT COUNT(t.RowId) FROM IPD_ToDoTask t WHERE t.LedgerId='" + LedgerId + "' and t.compcode='"+compcode+"'";
                    theCommand.Transaction = tran as SqlTransaction;
                    SqlDataReader theReader1 = theCommand.ExecuteReader();
                    theReader1.Read();
                    if (theReader1.HasRows)
                    CheckFlag = Convert.ToInt32(theReader1[0]);
                    theReader1.Close();

                }

                tran.Commit();

            }
            catch
            {
                tran.Rollback();
                throw;
            }
        }
    
        // Clean up.
        theConnection.Dispose();
        theCommand.Dispose();

        return CheckFlag;
    }

    public DataTable GridPopupDetails(string reg,string LedgerId,string compcode,string yearcode)
    {
        if (reg == "" || reg == "0")
            reg = "null";

        // Connection.
        theConnection = new SqlConnection();
        theConnection.ConnectionString = conString;

        // Command.
        theCommand = new SqlCommand();
        theCommand.Connection = theConnection;

        theCommand.CommandText = "exec sp_IpdpatietlistDetails  " + reg + "," + LedgerId + ",'"+compcode+"','"+yearcode+"'";
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

    public DataTable GridPopupDischargeAlert(string curdate,string compcode,string yearcode)
    {

        // Connection.
        theConnection = new SqlConnection();
        theConnection.ConnectionString = conString;

        // Command.
        theCommand = new SqlCommand();
        theCommand.Connection = theConnection;

        theCommand.CommandText = "exec  sp_IpdpatietlistDischargeDetails '" + curdate + "','"+compcode+"','"+yearcode+"'";
        //theCommand.CommandText = "exec  sp_IpdpatietlistDetails1 1";
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


    public DataSet GridPopupDetails1(string curdate,string cocode)
    {

        // Connection.
        theConnection = new SqlConnection();
        theConnection.ConnectionString = conString;

        // Command.
        theCommand = new SqlCommand();
        theCommand.Connection = theConnection;

        theCommand.CommandText = "exec sp_IpdpatietlistDetails1 '" + cocode + "','" + curdate + "'";
        theCommand.CommandType = CommandType.Text;


        // Adapter.
        theAdapter = new SqlDataAdapter();
        theAdapter.SelectCommand = theCommand;

        // Datatable.
        DataSet hospitalTable = new DataSet();
        theAdapter.Fill(hospitalTable); // Fill data into data table.

        // Clean up.
        theConnection.Dispose();
        theCommand.Dispose();
        theAdapter.Dispose();

        return hospitalTable;
    }


    public DataTable DropdownFloor(string compcode)
    {

        // Connection.
        theConnection = new SqlConnection();
        theConnection.ConnectionString = conString;

        // Command.
        theCommand = new SqlCommand();
        theCommand.Connection = theConnection;

        theCommand.CommandText = "select * from dbo.IPD_FloorMaster where status=1 and compcode='"+compcode+"'";
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


    public DataTable Dropdownbed(string floor, string roomtype, string room,string compcode)
    {

        // Connection.
        theConnection = new SqlConnection();
        theConnection.ConnectionString = conString;

        // Command.
        theCommand = new SqlCommand();
        theCommand.Connection = theConnection;

        theCommand.CommandText = "select * from IPD_BedMaster bm where bm.FloorID='" + floor + "' and bm.RoomCategoryID='" + roomtype + "' and bm.RoomID='" + room + "' and bm.status=1 and bm.compcode='"+compcode+"'";
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


    public DataTable Dropdownroom(string floor, string roomcatid,string compcode)
    {

        // Connection.
        theConnection = new SqlConnection();
        theConnection.ConnectionString = conString;

        // Command.
        theCommand = new SqlCommand();
        theCommand.Connection = theConnection;

        theCommand.CommandText = "select * from  IPD_RoomMaster rm where rm.status=1 AND rm.FloorID= '" + floor + "' and  rm.RoomCategoryID='" + roomcatid + "' and rm.compcode='"+compcode+"'";
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

    public DataTable Dropdownroomtype(string compcode)
    {

        // Connection.
        theConnection = new SqlConnection();
        theConnection.ConnectionString = conString;

        // Command.
        theCommand = new SqlCommand();
        theCommand.Connection = theConnection;

        theCommand.CommandText = "select * from dbo.IPD_RoomType where status=1 and compcode='"+compcode+"'";
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