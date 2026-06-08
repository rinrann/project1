using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using System.Data;

/// <summary>
/// Summary description for PatientsDetails12
/// </summary>
public class PatientsDetails
{
	public PatientsDetails(string con)
	{
        conString = con;
	}

    private string conString;
    private SqlConnection theConnection;
    private SqlCommand theCommand;
    private SqlDataAdapter theAdapter;
    private DataTable hospitalTable;

    public DataTable GridFill(string compcode, string reg)
    {
        // Connection.
        theConnection = new SqlConnection();
        theConnection.ConnectionString = conString;

        // Command.
        theCommand = new SqlCommand();
        theCommand.Connection = theConnection;

        theCommand.CommandText = "select *,CONVERT(varchar,Date,103) Date1 from dbo.OPD_PatientHistoryMapping where PatientReg='" + reg + "' and compcode='"+compcode+"'";
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
        

        return hospitalTable;
    }
    
    public DataTable DropdownGroup(string compcode)
    {
        // Connection.
        theConnection = new SqlConnection();
        theConnection.ConnectionString = conString;

        // Command.
        theCommand = new SqlCommand();
        theCommand.Connection = theConnection;
        theCommand.CommandText = "select * from  IPD_MedicineGroup where compcode='"+compcode+"'";
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
        

        return hospitalTable;
    }

    public DataTable DropdownVaccnation(string compcode)
    {
        // Connection.
        theConnection = new SqlConnection();
        theConnection.ConnectionString = conString;

        // Command.
        theCommand = new SqlCommand();
        theCommand.Connection = theConnection;
        theCommand.CommandText = "select * from  OPD_VaccinationMaster where compcode='"+compcode+"'";
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
        

        return hospitalTable;
    }

    public DataTable DropdownComplain(string compcode)
    {
        // Connection.
        theConnection = new SqlConnection();
        theConnection.ConnectionString = conString;

        // Command.
        theCommand = new SqlCommand();
        theCommand.Connection = theConnection;
        theCommand.CommandText = "select * from  OPD_Complain where compcode='"+compcode+"'";
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
        

        return hospitalTable;
    }

    public DataTable DropdownComplain1(string compcode)
    {
        // Connection.
        theConnection = new SqlConnection();
        theConnection.ConnectionString = conString;

        // Command.
        theCommand = new SqlCommand();
        theCommand.Connection = theConnection;
        theCommand.CommandText = "select c.ComplainName from  OPD_Complain c where c.compcode='"+compcode+"'";
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
        

        return hospitalTable;
    }

    public DataTable GetHospitalNote(string compcode, string reg)
    {
        // Connection.
        theConnection = new SqlConnection();
        theConnection.ConnectionString = conString;

        // Command.
        theCommand = new SqlCommand();
        theCommand.Connection = theConnection;

        theCommand.CommandText = "select * from dbo.OPD_PatientNote where PatientReg='" + reg + "' and compcode='"+compcode+"'";
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
        

        return hospitalTable;
    }

    public DataTable GetDiagnosis(string compcode, string regno, string PrsecriptionId)
    {
        // Connection.
        theConnection = new SqlConnection();
        theConnection.ConnectionString = conString;

        // Command.
        theCommand = new SqlCommand();
        theCommand.Connection = theConnection;

        theCommand.CommandText = "select convert(varchar(10),Date,103) Date,PatientReg,AppointmentNo,Diagnosis from dbo.OPD_PatientDiagnosis where PatientReg='" + regno + "' and compcode='" + compcode + "' and PrsecriptionId='" + PrsecriptionId + "'";
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


        return hospitalTable;
    }

    public DataTable DropdownSubGroup(string compcode, string group)
    {
        // Connection.
        theConnection = new SqlConnection();
        theConnection.ConnectionString = conString;

        // Command.
        theCommand = new SqlCommand();
        theCommand.Connection = theConnection;
        if (group == "0")
            theCommand.CommandText = "select * from  IPD_MedicineSubGroup where compcode='"+compcode+"'";
        else
            theCommand.CommandText = "select * from  IPD_MedicineSubGroup where GroupID='" + group + "' and compcode='"+ compcode+"'";
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
        

        return hospitalTable;
    }

    public DataTable FillTemplate(string compcode, string id)
    {
        // Connection.
        theConnection = new SqlConnection();
        theConnection.ConnectionString = conString;

        // Command.
        theCommand = new SqlCommand();
        theCommand.Connection = theConnection;
       theCommand.CommandText = "EXEC sp_MD_FetchTestMedicineTemplate  '"+compcode+"'," + id + "";
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
        

        return hospitalTable;
    }

    public DataTable Dropdowntemplate(string compcode, string gr)
    {
        // Connection.
        theConnection = new SqlConnection();
        theConnection.ConnectionString = conString;

        // Command.
        theCommand = new SqlCommand();
        theCommand.Connection = theConnection;


        theCommand.CommandText = "select * from  IPD_PrescriptionTmplate where PrescriptionGrId='" + gr + "' and compcode='"+compcode+"'";
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
        

        return hospitalTable;
    }

    public DataTable DropdowntemplateGroup(string compcode)
    {
        // Connection.
        theConnection = new SqlConnection();
        theConnection.ConnectionString = conString;

        // Command.
        theCommand = new SqlCommand();
        theCommand.Connection = theConnection;


        theCommand.CommandText = "select RowId , PrescriptionGroupName  from  IPD_PrescriptionTmplateGroup where compcode='" + compcode + "' UNION select 999, 'Other'";
        //        DropDownList34.DataTextField = "PrescriptionGroupName";
        //DropDownList34.DataValueField = "RowId";

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
        

        return hospitalTable;
    }

    public DataTable DropdownMedicine(string compcode, string sub)
    {
        // Connection.
        theConnection = new SqlConnection();
        theConnection.ConnectionString = conString;

        // Command.
        theCommand = new SqlCommand();
        theCommand.Connection = theConnection;
        if (sub == "0")
            theCommand.CommandText = "select * from  IPD_MedicineMaster where compcode='"+compcode+"'";
        else
            theCommand.CommandText = "select * from  IPD_MedicineMaster where SubGroupid='" + sub + "' and compcode='"+compcode+"'";
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
        

        return hospitalTable;
    }

    public DataTable GetDose(string compcode, string med)
    {
        // Connection.
        theConnection = new SqlConnection();
        theConnection.ConnectionString = conString;

        // Command.
        theCommand = new SqlCommand();
        theCommand.Connection = theConnection;
        theCommand.CommandText = "select db.DoseName from dbo.IPD_PrescriptionTmplateMapping map,dbo.MD_DoseMaster db where map.compcode=db.compcode and map.DailyDose=db.ID and map.MedicineID='" + med + "' and map.compcode='"+compcode+"'";
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
        

        return hospitalTable;
    }


    public DataTable GetGenericName(string compcode, string med)
    {
        // Connection.
        theConnection = new SqlConnection();
        theConnection.ConnectionString = conString;

        // Command.
        theCommand = new SqlCommand();
        theCommand.Connection = theConnection;
        theCommand.CommandText = "select isNull(GenericName,'') GenericName from IPD_MedicineMaster where MedicineID='" + med + "' and compcode='" + compcode + "'";
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


        return hospitalTable;
    }

    public DataTable GridFillVaccine(string compcode, string reg)
    {
        // Connection.
        theConnection = new SqlConnection();
        theConnection.ConnectionString = conString;

        // Command.
        theCommand = new SqlCommand();
        theCommand.Connection = theConnection;

        theCommand.CommandText = "select map.*,vm.Name,CONVERT(varchar,Date,103) Date1 from dbo.OPD_PatientVaccineMap map,dbo.OPD_VaccinationMaster vm where map.compcode=vm.compcode and vm.ID=map.VacineID and PatientReg='" + reg + "' and map.compcode='"+compcode+"'";
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
        

        return hospitalTable;
    }

    public DataTable GridFillInvestigation(string compcode, string reg)
    {
        // Connection.
        theConnection = new SqlConnection();
        theConnection.ConnectionString = conString;

        // Command.
        theCommand = new SqlCommand();
        theCommand.Connection = theConnection;

        theCommand.CommandText = "select *,CONVERT(varchar,Date,103) Date1 from dbo.OPD_PatientInvestigationMap where PatientReg='" + reg + "' and compcode='"+compcode+"'";
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
        

        return hospitalTable;
    }

    public DataTable GridFillBody(string compcode, string reg)
    {
        // Connection.
        theConnection = new SqlConnection();
        theConnection.ConnectionString = conString;

        // Command.
        theCommand = new SqlCommand();
        theCommand.Connection = theConnection;

        theCommand.CommandText = "select *,CONVERT(varchar,Date,103) Date1 from dbo.OPD_PatientBodyMap map,OPD_Complain c where map.compcode=c.compcode and c.RowId=map.Complain and PatientReg='" + reg + "' and map.compcode='" + compcode + "'";
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
        

        return hospitalTable;
    }

    public DataTable PatientFill(string compcode, string reg,string Appno)
    {
        // Connection.
        theConnection = new SqlConnection();
        theConnection.ConnectionString = conString;

        // Command.
        theCommand = new SqlCommand();
        theCommand.Connection = theConnection;

        //theCommand.CommandText = "select *,CONVERT(varchar,opr.AppointMentDate,103) date1 from OPD_PatientRegistration opr where opr.PatientRegNo='" + reg + "'";
        theCommand.CommandText = "select *,/*isNull(Opa.PrsecriptionId,'')PrsecriptionId,*/isNull(opd.Checked,0) PatientChecked,opd.AppoNo,CONVERT(varchar,opd.AppointMentDate,103) AppDt,dist.DistrictName from OPD_PatientRegistration opd,OPD_PatientRequisition opr/*,OPD_PatientAppointment Opa*/,GN_District dist where dist.compcode=opd.compcode and dist.id=opd.district and/*Opa.compcode=opd.compcode and Opa.PatientRegNo=opd.PatientRegNo and*/ opd.compcode=opr.compcode and opd.PatientRegNo=opr.PatientRegNo and opd.AppoNo=opr.AppoNo and opr.PatientRegNo='" + reg + "' and opd.compcode='" + compcode + "' /*and Opa.AppoNo='" + Appno + "'*/";
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
        

        return hospitalTable;
    }

    public DataTable GetPrescriptiondtls(string compcode, string reg, string PrsecriptionId)
    {
        // Connection.
        theConnection = new SqlConnection();
        theConnection.ConnectionString = conString;

        // Command.
        theCommand = new SqlCommand();
        theCommand.Connection = theConnection;

        theCommand.CommandText = "select map.RowID,CONVERT(varchar,op.Date,103) Date1,map.PrescriptionId,map.Dose,mm.MedicineName,isNull(mm.GenericName,'') GenericName,ms.SubGrName,mg.MedicineGroupName " +
            "from dbo.OPD_PrescriptionMapping map,dbo.IPD_MedicineGroup mg,dbo.IPD_MedicineMaster mm,dbo.IPD_MedicineSubGroup ms,OPD_Prescription op "+
            "where op.compcode=map.compcode and op.compcode=mg.compcode and op.compcode=mm.compcode and op.compcode=ms.compcode and op.PrescriptionID=map.PrescriptionId and "+
            "map.GroupID=mg.MedicineGroupID and map.MedicineId=mm.MedicineID and map.SubGroupId=ms.ID and op.Date in "+
            "(select  top 4 op.Date from OPD_Prescription op where  op.PatientReg='" + reg + "' order by op.Date desc) and op.compcode='" + compcode + "' and map.PrescriptionId='" + PrsecriptionId + "'";
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
        

        return hospitalTable;
    }

    public DataTable GeneratePrescription(string compcode)
    {
        // Connection.
        theConnection = new SqlConnection();
        theConnection.ConnectionString = conString;

        // Command.
        theCommand = new SqlCommand();
        theCommand.Connection = theConnection;

        theCommand.CommandText = "exec sp_GeneratePrescriptionNo '"+compcode+"'";
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
        

        return hospitalTable;
    }

    public DataTable GetRadiology(string compcode, string code)
    {
        DataTable custTable;


        // Connection.
        theConnection = new SqlConnection();
        theConnection.ConnectionString = conString;

        // Command.
        theCommand = new SqlCommand();
        theCommand.Connection = theConnection;
        theCommand.CommandText = "select  * from PH_USGHeaderMaster head,PH_USGNameMaster name where name.compcode=head.compcode and name.compcode='"+compcode+"' and name.SubGroupCode=head.SubGrCode and name.ID='" + code + "'";
        theCommand.CommandType = CommandType.Text;

        // Adapter.
        theAdapter = new SqlDataAdapter();
        theAdapter.SelectCommand = theCommand;

        // Datatable.
        custTable = new DataTable();
        theAdapter.Fill(custTable); // Fill data into data table.

        // Clean up.
        theConnection.Dispose();
        theCommand.Dispose();
        

        return custTable;
    }

    public DataTable GetRadiologyParameter(string compcode, string code)
    {
        DataTable custTable;


        // Connection.
        theConnection = new SqlConnection();
        theConnection.ConnectionString = conString;

        // Command.
        theCommand = new SqlCommand();
        theCommand.Connection = theConnection;
        theCommand.CommandText = "select * from PH_USGSubGrMapping submap,PH_USGNameMaster name where submap.compcode=name.compcode and submap.compcode='"+compcode+"' and submap.SubGrId=name.SubGroupCode and name.ID='" + code + "'";
        theCommand.CommandType = CommandType.Text;

        // Adapter.
        theAdapter = new SqlDataAdapter();
        theAdapter.SelectCommand = theCommand;

        // Datatable.
        custTable = new DataTable();
        theAdapter.Fill(custTable); // Fill data into data table.

        // Clean up.
        theConnection.Dispose();
        theCommand.Dispose();
        

        return custTable;
    }

    public DataTable GetUSGResultCode(string compcode, string Reg)
    {
        DataTable custTable;


        // Connection.
        theConnection = new SqlConnection();
        theConnection.ConnectionString = conString;

        // Command.
        theCommand = new SqlCommand();
        theCommand.Connection = theConnection;
        theCommand.CommandText = "select * from PH_USGTestResult ur where ur.Flag='OPD' and ur.RegNo='" + Reg + "' and ur.compcode='"+compcode+"'";
        theCommand.CommandType = CommandType.Text;

        // Adapter.
        theAdapter = new SqlDataAdapter();
        theAdapter.SelectCommand = theCommand;

        // Datatable.
        custTable = new DataTable();
        theAdapter.Fill(custTable); // Fill data into data table.

        // Clean up.
        theConnection.Dispose();
        theCommand.Dispose();
        

        return custTable;
    }

    public DataTable GetUSGheaderResultCode(string compcode, string Reg, string code)
    {
        DataTable custTable;
        // Connection.
        theConnection = new SqlConnection();
        theConnection.ConnectionString = conString;

        // Command.
        theCommand = new SqlCommand();
        theCommand.Connection = theConnection;
        theCommand.CommandText = "select *,hr.HeaderContent hc from PH_USGHeaderResult hr,PH_USGTestResult ur,dbo.PH_USGHeaderMaster hm "+
            "where hr.compcode=ur.compcode and hr.compcode=hm.compcode and hr.yearcode=ur.yearcode and hr.compcode='"+compcode+"' and "+
            "hr.RowId=ur.RowId and hm.ID=hr.HeaderId and ur.USGNameId='" + code + "' and ur.RegNo='" + Reg + "'";
        theCommand.CommandType = CommandType.Text;

        // Adapter.
        theAdapter = new SqlDataAdapter();
        theAdapter.SelectCommand = theCommand;

        // Datatable.
        custTable = new DataTable();
        theAdapter.Fill(custTable); // Fill data into data table.

        // Clean up.
        theConnection.Dispose();
        theCommand.Dispose();
        

        return custTable;
    }

    public DataTable GetRadioResultParameter(string compcode, string Reg, string code)
    {
        DataTable custTable;
        // Connection.
        theConnection = new SqlConnection();
        theConnection.ConnectionString = conString;

        // Command.
        theCommand = new SqlCommand();
        theCommand.Connection = theConnection;
        theCommand.CommandText = "select * from PH_USGParameterResult pr,PH_USGTestResult ur,dbo.PH_USGSubGrMapping submap where "+
            "pr.compcode=ur.compcode and pr.compcode=submap.compcode and pr.RowId=ur.RowId and submap.Id=pr.ParameterId and and ur.compcode='"+compcode+"' and ur.RegNo='" + Reg + "'  and  ur.USGNameId='" + code + "'";
        theCommand.CommandType = CommandType.Text;

        // Adapter.
        theAdapter = new SqlDataAdapter();
        theAdapter.SelectCommand = theCommand;

        // Datatable.
        custTable = new DataTable();
        theAdapter.Fill(custTable); // Fill data into data table.

        // Clean up.
        theConnection.Dispose();
        theCommand.Dispose();
        

        return custTable;
    }
   
    public DataTable DropdownUSGGr(string compcode)
    {
        DataTable custTable;


        // Connection.
        theConnection = new SqlConnection();
        theConnection.ConnectionString = conString;

        // Command.
        theCommand = new SqlCommand();
        theCommand.Connection = theConnection;
        theCommand.CommandText = "select * from PH_USGGRMaster where compcode='"+compcode+"'";
        theCommand.CommandType = CommandType.Text;

        // Adapter.
        theAdapter = new SqlDataAdapter();
        theAdapter.SelectCommand = theCommand;

        // Datatable.
        custTable = new DataTable();
        theAdapter.Fill(custTable); // Fill data into data table.

        // Clean up.
        theConnection.Dispose();
        theCommand.Dispose();
        

        return custTable;
    }

    public DataTable DropdownUSGSubgr(string compcode, string gr)
    {
        DataTable custTable;


        // Connection.
        theConnection = new SqlConnection();
        theConnection.ConnectionString = conString;

        // Command.
        theCommand = new SqlCommand();
        theCommand.Connection = theConnection;
        theCommand.CommandText = "select * from PH_USGSubGrMaster where GroupID='" + gr + "' and compcode='"+compcode+"' and status=1";
        theCommand.CommandType = CommandType.Text;

        // Adapter.
        theAdapter = new SqlDataAdapter();
        theAdapter.SelectCommand = theCommand;

        // Datatable.
        custTable = new DataTable();
        theAdapter.Fill(custTable); // Fill data into data table.

        // Clean up.
        theConnection.Dispose();
        theCommand.Dispose();
        

        return custTable;
    }

    public DataTable dropdownusgname(string compcode, string gr, string subgr)
    {
        DataTable custTable;


        // Connection.
        theConnection = new SqlConnection();
        theConnection.ConnectionString = conString;

        // Command.
        theCommand = new SqlCommand();
        theCommand.Connection = theConnection;
        theCommand.CommandText = "select * from PH_USGNameMaster where GroupCode='" + gr + "' and SubGroupCode='" + subgr + "' and compcode='"+compcode+"' and status=1";
        theCommand.CommandType = CommandType.Text;

        // Adapter.
        theAdapter = new SqlDataAdapter();
        theAdapter.SelectCommand = theCommand;

        // Datatable.
        custTable = new DataTable();
        theAdapter.Fill(custTable); // Fill data into data table.

        // Clean up.
        theConnection.Dispose();
        theCommand.Dispose();
        

        return custTable;
    }

    public DataTable GenerateRadiologyCode(string compcode,string yearcode)
    {
        // Connection.
        theConnection = new SqlConnection();
        theConnection.ConnectionString = conString;

        // Command.
        theCommand = new SqlCommand();
        theCommand.Connection = theConnection;
        theCommand.CommandText = "sp_PH_GenerateRadiologyCode '"+compcode+"','"+yearcode+"'";
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
        

        return hospitalTable;
    }

    public DataTable GetDoctorID(string compcode, string reg)
    {
        // Connection.
        theConnection = new SqlConnection();
        theConnection.ConnectionString = conString;

        // Command.
        theCommand = new SqlCommand();
        theCommand.Connection = theConnection;
        theCommand.CommandText = "select DocId from dbo.OPD_PatientRegistration where PatientRegNo='"+reg+"' and compcode='"+compcode+"' order by AppointMentDate desc";
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
        

        return hospitalTable;
    }

    public bool InsertRadiology(string compcode, string yearcode, string RowId, string USGNameId, string RegNo, string ReqNo, string ConsultantDoc, string CheckedDoc, string Remarks, string CreatedBy, string Date)
    {
        try
        {
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
            theCommand.CommandText = "INSERT INTO PH_USGTestResult(compcode,yearcode,RowId,USGNameId,RegNo,ReqNo,ConsultantDoc,CheckedDoc,Remarks,CreatedBy,Status,Flag,Date) VALUES('"+compcode+"','"+yearcode+"','" + RowId + "','" + USGNameId + "','" + RegNo + "','" + ReqNo + "','" + ConsultantDoc + "','" + CheckedDoc + "','" + Remarks + "','" + CreatedBy + "',1,'OPD',"+Date+"')";
            // "INSERT INTO PH_USGTestResult(RowId,USGNameId,RegNo,ReqNo,ConsultantDoc,CheckedDoc,Remarks,CreatedBy,Status) VALUES('" + RowId + "','" + USGNameId + "','" + RegNo + "','" + ReqNo + "','" + ConsultantDoc + "','" + CheckedDoc + "','" + Remarks + "','" + CreatedBy + "',1)";
            theCommand.CommandType = CommandType.Text;
            theCommand.ExecuteNonQuery(); // Execute insert query.
            return true;
        }
        catch
        {
            return false;
        }
        finally
        {
            theConnection.Dispose();
            theCommand.Dispose();
            
        }
    }


    public bool InsertHeader(string compcode, string yearcode, string RowId, string HeaderId, string HeaderContent)
    {
        try
        {
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
            theCommand.CommandText = "INSERT INTO PH_USGHeaderResult(compcode,yearcode,RowId,HeaderId,HeaderContent) VALUES('" + compcode + "','" + yearcode + "','" + RowId + "','" + HeaderId + "','" + HeaderContent + "')";
            theCommand.CommandType = CommandType.Text;
            theCommand.ExecuteNonQuery(); // Execute insert query.
            return true;
        }
        catch
        {
            return false;
        }
        finally
        {
            theConnection.Dispose();
            theCommand.Dispose();
            
        }
    }


    public bool SaveDiagnosis(string compcode, string yearcode, string regno, string appno, string diagdate, string diagnosis, string PrsecriptionId)
    {
        try
        {
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
            theCommand.CommandText = "INSERT INTO OPD_PatientDiagnosis(compcode,yearcode,Date,PatientReg,AppointmentNo,Diagnosis,PrsecriptionId) VALUES('" + compcode + "','" + yearcode + "',convert(date,'" + diagdate + "',103),'" + regno + "','" + appno + "','" + diagnosis + "','" + PrsecriptionId + "')";
            theCommand.CommandType = CommandType.Text;
            theCommand.ExecuteNonQuery(); // Execute insert query.
            return true;
        }
        catch
        {
            return false;
        }
        finally
        {
            theConnection.Dispose();
            theCommand.Dispose();

        }
    }

    public bool UpdateDiagnosis(string compcode, string yearcode, string regno, string appno, string diagdate, string diagnosis, string PrsecriptionId)
    {
        try
        {
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
            theCommand.CommandText = "Update OPD_PatientDiagnosis set Diagnosis='" + diagnosis + "' where compcode='" + compcode + "' and yearcode='" + yearcode + "' and PatientReg='" + regno + "' and AppointmentNo='" + appno + "' and PrsecriptionId='" + PrsecriptionId  + "'";
            theCommand.CommandType = CommandType.Text;
            theCommand.ExecuteNonQuery(); // Execute insert query.
            return true;
        }
        catch
        {
            return false;
        }
        finally
        {
            theConnection.Dispose();
            theCommand.Dispose();

        }
    }

    public bool InsertParameter(string compcode, string yearcode, string RowId, string ParameterId, string Value)
    {
        try
        {
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
            theCommand.CommandText = "INSERT INTO PH_USGParameterResult(compcode,yearcode,RowId,ParameterId,Value) VALUES('" + compcode + "','" + yearcode + "','" + RowId + "','" + ParameterId + "','" + Value + "')";
            theCommand.CommandType = CommandType.Text;
            theCommand.ExecuteNonQuery(); // Execute insert query.
            return true;
        }
        catch
        {
            return false;
        }
        finally
        {
            theConnection.Dispose();
            theCommand.Dispose();
            
        }
    }


    public bool InsertHistory(string compcode, string PatientReg, string Date, string Mens, string OperationDtls, string Special, string Others)
    {
        try
        {
            // Connection.
            theConnection = new SqlConnection();
            if (conString != "")
            {
                theConnection.ConnectionString = conString;
                theConnection.Open();
            }

            // Command.
            theCommand = new SqlCommand();
            // string s = Session["userName"];
            theCommand.Connection = theConnection;
            theCommand.CommandText = "INSERT INTO OPD_PatientHistoryMapping(compcode,PatientReg,Date,Mens,OperationDtls,Special,Others) VALUES('"+compcode+"','" + PatientReg + "','" + Date + "','" + Mens + "','" + OperationDtls + "','" + Special + "','" + Others + "')";
            theCommand.CommandType = CommandType.Text;
            theCommand.ExecuteNonQuery(); // Execute insert query.
            return true;
        }
        catch
        {
            return false;
        }
        finally
        {
            theConnection.Dispose();
            theCommand.Dispose();
            
        }
    }

    public bool InsertPrescription(string compcode, string PrescriptionID, string PatientReg, string Date, string Comment)
    {
       try
        {
            // Connection.
            theConnection = new SqlConnection();
            if (conString != "")
            {
                theConnection.ConnectionString = conString;
                theConnection.Open();
            }

            // Command.
            theCommand = new SqlCommand();
            // string s = Session["userName"];
            theCommand.Connection = theConnection;
            theCommand.CommandText = "INSERT INTO OPD_Prescription(compcode,PrescriptionID,PatientReg,Date,Comment) VALUES('"+compcode+"','" + PrescriptionID + "','" + PatientReg + "','" + Date + "','" + Comment + "')";
            theCommand.CommandType = CommandType.Text;

           theCommand.ExecuteNonQuery(); // Execute insert query.
           return true;
        }
        catch
        {
            return false;
        }
       finally
       {
           theConnection.Dispose();
           theCommand.Dispose();
           
       }
    }


    public bool InsertOPDHospitalNote(string compcode, string PatientReg, string note)
    {
         try
        {
            // Connection.
            theConnection = new SqlConnection();
            if (conString != "")
            {
                theConnection.ConnectionString = conString;
                theConnection.Open();
            }

            // Command.
            theCommand = new SqlCommand();
            // string s = Session["userName"];
            theCommand.Connection = theConnection;
            theCommand.CommandText = "INSERT INTO OPD_PatientNote(compcode,PatientReg,NoteDtls) VALUES('"+compcode+"','" + PatientReg + "','" + note + "')";
            theCommand.CommandType = CommandType.Text;

            theCommand.ExecuteNonQuery(); // Execute insert query.
            return true;
        }
        catch
        {
            return false;
        }
         finally
         {
             theConnection.Dispose();
             theCommand.Dispose();
             
         }
    }

    public bool UpdateOPDHospitalNote(string compcode, string PatientReg, string note)
    { 
        try
        {
            // Connection.
            theConnection = new SqlConnection();
            if (conString != "")
            {
                theConnection.ConnectionString = conString;
                theConnection.Open();
            }

            // Command.
            theCommand = new SqlCommand();
            // string s = Session["userName"];
            theCommand.Connection = theConnection;
            theCommand.CommandText = "update OPD_PatientNote set  NoteDtls='" + note + "' where PatientReg='" + PatientReg + "' and compcode='"+compcode+"'";
            theCommand.CommandType = CommandType.Text;
            theCommand.ExecuteNonQuery(); // Execute insert query.
            return true;
        }
        catch
        {
            return false;
        }
        finally
        {
            theConnection.Dispose();
            theCommand.Dispose();
            
        }
    }


    public bool InsertPrescriptionMap(string compcode, string PrescriptionId, string GroupID, string SubGroupId, string MedicineId, string Dose)
    { 
        try
        {
            // Connection.
            theConnection = new SqlConnection();
            if (conString != "")
            {
                theConnection.ConnectionString = conString;
                theConnection.Open();
            }

            // Command.
            theCommand = new SqlCommand();
            // string s = Session["userName"];
            theCommand.Connection = theConnection;
            theCommand.CommandText = "INSERT INTO OPD_PrescriptionMapping(compcode,PrescriptionId,GroupID,SubGroupId,MedicineId,Dose) VALUES('"+compcode+"','" + PrescriptionId + "','" + GroupID + "','" + SubGroupId + "','" + MedicineId + "','" + Dose + "')";
            theCommand.CommandType = CommandType.Text;
            theCommand.ExecuteNonQuery(); // Execute insert query.
            return true;
        }
        catch
        {
            return false;
        }
        finally
        {
            theConnection.Dispose();
            theCommand.Dispose();
            
        }
    }


    public bool InsertInvestigation(string compcode, string PatientReg, string Date, string Investigation, string Details)
    { 
        try
        {
            // Connection.
            theConnection = new SqlConnection();
            if (conString != "")
            {
                theConnection.ConnectionString = conString;
                theConnection.Open();
            }

            // Command.
            theCommand = new SqlCommand();
            // string s = Session["userName"];
            theCommand.Connection = theConnection;
            theCommand.CommandText = "INSERT INTO OPD_PatientInvestigationMap(compcode,PatientReg,Date,Investigation,Details) VALUES('"+compcode+"','" + PatientReg + "','" + Date + "','" + Investigation + "','" + Details + "')";
            theCommand.CommandType = CommandType.Text;
            theCommand.ExecuteNonQuery(); // Execute insert query.
            return true;
        }
        catch
        {
            return false;
        }
        finally
        {
            theConnection.Dispose();
            theCommand.Dispose();
            
        }
    }

    public bool InsertBody(string compcode, string PatientReg, string Date, string Complain, string Weight, string BP, string P, string E, string Chest, string PA, string PV, string FH8, string Others,string PrescriptionId,string Appno)
    {
        try
        {
            // Connection.
            theConnection = new SqlConnection();
            if (conString != "")
            {
                theConnection.ConnectionString = conString;
                theConnection.Open();
            }

            // Command.
            theCommand = new SqlCommand();
            // string s = Session["userName"];
            theCommand.Connection = theConnection;
            theCommand.CommandText = "INSERT INTO OPD_PatientBodyMap(compcode,PatientReg,Date,Complain,Weight,BP,P,E,Chest,PA,PV,FH8,Others,PrsecriptionId,AppoNo) VALUES('" + compcode + "','" + PatientReg + "','" + Date + "','" + Complain + "','" + Weight + "','" + BP + "','" + P + "','" + E + "','" + Chest + "','" + PA + "','" + PV + "','" + FH8 + "','" + Others + "','" + PrescriptionId + "','"+ Appno +"')";
            theCommand.CommandType = CommandType.Text;
            theCommand.ExecuteNonQuery(); // Execute insert query.
            return true;

        }
        catch
        {
            return false;
        }
        finally
        {
            theConnection.Dispose();
            theCommand.Dispose();
            
        }
    }

    public bool InsertVaccine(string compcode,string PatientReg, string VacineName, string Date, string Comment)
    {
          try
        {
            // Connection.
            theConnection = new SqlConnection();
            if (conString != "")
            {
                theConnection.ConnectionString = conString;
                theConnection.Open();
            }

            // Command.
            theCommand = new SqlCommand();
            // string s = Session["userName"];
            theCommand.Connection = theConnection;
            theCommand.CommandText = "INSERT INTO OPD_PatientVaccineMap(compcode,PatientReg,VacineID,Date,Comment) VALUES('" + compcode + "','" + PatientReg + "','" + VacineName + "','" + Date + "','" + Comment + "')";
            theCommand.CommandType = CommandType.Text;
            theCommand.ExecuteNonQuery(); // Execute insert query.
            return true;
        }
        catch
        {
            return false;
        }
          finally
          {
              theConnection.Dispose();
              theCommand.Dispose();
              
          }
    }

    public bool UpdatePatientReg(string compcode, string AppoNo, string PatientReg, string G, string P, string A, string Live, string LMP, string EDD, string LCB, string Comment)
    {
        try
        {
            // Connection.
            theConnection = new SqlConnection();
            if (conString != "")
            {
                theConnection.ConnectionString = conString;
                theConnection.Open();
            }

            // Command.
            theCommand = new SqlCommand();
            // string s = Session["userName"];
            theCommand.Connection = theConnection;
            theCommand.CommandText = "update OPD_PatientRequisition set G='" + G + "',P='" + P + "',A='" + A + "',Live='" + Live + "',LCB='" + LCB + "',LMP='" + LMP + "',EDD='" + EDD + "',Comment='" + Comment + "' where PatientRegNo='" + PatientReg + "' and compcode='" + compcode + "'";
            theCommand.CommandType = CommandType.Text;
            theCommand.ExecuteNonQuery(); // Execute insert query.
            return true;
        }
        catch
        {
            return false;
        }
        finally
        {
            theConnection.Dispose();
            theCommand.Dispose();
            
        }
    }

    public bool UpdateAppointment(string compcode, string AppoNo, string PatientRegNo, string GuadianName, string PhNo1, string Address, string District, string AppointMentDate, string AppointmentTime)
    {
        try
        {
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

            theCommand.CommandText = "update OPD_PatientRegistration set  GuadianName='" + GuadianName + "',PhNo1='" + PhNo1 + "',Address='" + Address + "',District='" + District + "',AppointMentDate='" + AppointMentDate + "',AppointmentTime='" + AppointmentTime + "' where AppoNo = '" + AppoNo + "' and compcode='" + compcode + "'";
            theCommand.CommandType = CommandType.Text;
            theCommand.ExecuteNonQuery(); // Execute insert query.
            theCommand.CommandText = "update OPD_PatientAppointment set  GuadianName='" + GuadianName + "',PhNo1='" + PhNo1 + "',Address='" + Address + "',District='" + District + "',AppointMentDate='" + AppointMentDate + "',AppointmentTime='" + AppointmentTime + "' where AppoNo = '" + AppoNo + "' and compcode='" + compcode + "'";
            theCommand.CommandType = CommandType.Text;
            theCommand.ExecuteNonQuery(); // Execute insert query.
            return true;
        }
        catch
        {
            return false;
        }
        finally
        {
            theConnection.Dispose();
            theCommand.Dispose();
            
        }

    }
    public DataTable GetPatientDetails(string compcode, string CustID)
    {
        DataTable custTable;
        if (CustID == "")
            CustID = "null";
        // Connection.
        theConnection = new SqlConnection();
        theConnection.ConnectionString = conString;

        // Command.
        theCommand = new SqlCommand();
        theCommand.Connection = theConnection;
        theCommand.CommandText = "exec sp_PatientDetails '"+compcode+"','" + CustID + "',null,null,null";
        theCommand.CommandType = CommandType.Text;

        // Adapter.
        theAdapter = new SqlDataAdapter();
        theAdapter.SelectCommand = theCommand;

        // Datatable.
        custTable = new DataTable();
        theAdapter.Fill(custTable); // Fill data into data table.

        // Clean up.
        theConnection.Dispose();
        theCommand.Dispose();
        

        return custTable;
    }
    public bool DeleteHistory(string compcode,string id)
    {
        try
        {
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
            theCommand.CommandText = "delete OPD_PatientHistoryMapping WHERE RowID='" + id + "' and compcode='"+compcode+"'";
            theCommand.CommandType = CommandType.Text;
            theCommand.ExecuteNonQuery(); // Execute insert query.
            return true;
        }
        catch
        {
            return false;
        }
        finally
        {
            theConnection.Dispose();
            theCommand.Dispose();
            
        }

    }

    public bool UpdateHistory(string compcode, string id, string Date, string Mens, string OperationDtls, string Special, string Others)
    { 
        try
        {
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
            theCommand.CommandText = "update OPD_PatientHistoryMapping set Date='" + Date + "',Mens='" + Mens + "',OperationDtls='" + OperationDtls + "',Special='" + Special + "',Others='" + Others + "' where RowID='" + id + "' and compcode='"+compcode+"'";
            theCommand.CommandType = CommandType.Text;

            theCommand.ExecuteNonQuery(); // Execute insert query.
            return true;
        }
        catch
        {
            return false;
        }
        finally
        {
            theConnection.Dispose();
            theCommand.Dispose();
            
        }
    }



    public bool Deletevaccine(string compcode, string id)
    {
        try
        {
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
            theCommand.CommandText = "delete OPD_PatientVaccineMap WHERE RowId='" + id + "' and compcode='"+compcode+"'";
            theCommand.CommandType = CommandType.Text;

            theCommand.ExecuteNonQuery(); // Execute insert query.
            return true;
        }
        catch
        {
            return false;
        }
        finally
        {
            theConnection.Dispose();
            theCommand.Dispose();
            
        }

    }

    public bool UpdateVaccine(string compcode, string id, string VacineName, string Date, string Comment)
    { 
        try
        {
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
            theCommand.CommandText = "update OPD_PatientVaccineMap set VacineID='" + VacineName + "',Date='" + Date + "',Comment='" + Comment + "'   where RowId='" + id + "' and compcode='"+compcode+"'";
            theCommand.CommandType = CommandType.Text;
            return true;
        }
        catch
        {
            return false;
        }
        finally
        {
            theConnection.Dispose();
            theCommand.Dispose();
            
        }

    }



    public bool DeleteInvestigation(string compcode, string id)
    { 
        try
        {
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
            theCommand.CommandText = "delete OPD_PatientInvestigationMap WHERE RowId='" + id + "' and compcode='"+compcode+"'";
            theCommand.CommandType = CommandType.Text;
            theCommand.ExecuteNonQuery(); // Execute insert query.
            return true;
        }
        catch
        {
            return false;
        }
        finally
        {
            theConnection.Dispose();
            theCommand.Dispose();
            
        }

    }

    public bool UpdateInvestigation(string compcode, string id, string Date, string Investigation, string Details)
    { 
        try
        {
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
            theCommand.CommandText = "update OPD_PatientInvestigationMap set Date='" + Date + "',Investigation='" + Investigation + "',Details='" + Details + "'   where RowId='" + id + "' and compcode='"+compcode+"'";
            theCommand.CommandType = CommandType.Text;
            theCommand.ExecuteNonQuery(); // Execute insert query.
            return true;
        }
        catch
        {
            return false;
        }
        finally
        {
            theConnection.Dispose();
            theCommand.Dispose();
            
        }

    }


    public bool DeleteBody(string compcode, string id)
    { 
        try
        {
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
            theCommand.CommandText = "delete OPD_PatientBodyMap WHERE RowId='" + id + "' and compcode='"+compcode+"'";
            theCommand.CommandType = CommandType.Text;
           theCommand.ExecuteNonQuery(); // Execute insert query.
            return true;
        }
        catch
        {
            return false;
        }
        finally
        {
            theConnection.Dispose();
            theCommand.Dispose();
            
        }

    }

    public bool UpdateBody(string compcode, string id, string Date, string Complain, string Weight, string BP, string P, string E, string Chest, string PA, string PV, string FH8, string Others,string AppNo)
    { 
        try
        {
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
            theCommand.CommandText = "update OPD_PatientBodyMap set Date='" + Date + "',Complain='" + Complain + "',Weight='" + Weight + "',BP='" + BP + "',P='" + P + "',E='" + E + "' ,Chest='" + Chest + "',PA='" + PA + "',PV='" + PV + "',FH8='" + FH8 + "',Others='" + Others + "'  where RowID='" + id + "' and compcode='" + compcode + "' and AppoNo='"+ AppNo +"'";
            theCommand.CommandType = CommandType.Text;
            theCommand.ExecuteNonQuery(); // Execute insert query.
            return true;
        }
        catch
        {
            return false;
        }
        finally
        {
            theConnection.Dispose();
            theCommand.Dispose();
            
        }

    }


    public bool DeletePrescription(string compcode, string id)
    {
        try
        {
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
            theCommand.CommandText = "delete OPD_PrescriptionMapping WHERE RowID='" + id + "' and compcode='"+compcode+"'";
            theCommand.CommandType = CommandType.Text;
            theCommand.ExecuteNonQuery(); // Execute insert query.
            return true;
        }
        catch
        {
            return false;
        }
        finally
        {
            theConnection.Dispose();
            theCommand.Dispose();
            
        }

    }

    public bool UpdatePrescription(string compcode, string id, string GroupID, string SubGroupId, string MedicineId, string Dose, string PrsecriptionId)
    { 
        try
        {
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
            theCommand.CommandText = "update OPD_PrescriptionMapping set GroupID='" + GroupID + "',SubGroupId='" + SubGroupId + "',MedicineId='" + MedicineId + "',Dose='" + Dose + "'  where RowID='" + id + "' and compcode='" + compcode + "' and PrsecriptionId='" + PrsecriptionId + "'";
            theCommand.CommandType = CommandType.Text;
            theCommand.ExecuteNonQuery(); // Execute insert query.
            return true;
        }
        catch
        {
            return false;
        }
        finally
        {
            theConnection.Dispose();
            theCommand.Dispose();
            
        }

    }
    public bool CheckPatient(string compcode, string yearcode, int chk,string RegNo,string AppoNo)
    {
        try
        {
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
            if (chk == 1)
            {
                theCommand.CommandText = "update OPD_PatientRegistration set Checked=1,Status=0 where PatientRegNo='" + RegNo + "' and compcode='" + compcode + "'";
                theCommand.CommandType = CommandType.Text;
                theCommand.ExecuteNonQuery(); // Execute insert query.
                theCommand.CommandText = "update OPD_PatientAppointment set Status=0,Checked=1 where AppoNo='" + AppoNo + "' and compcode='" + compcode + "'";
                theCommand.CommandType = CommandType.Text;
                theCommand.ExecuteNonQuery(); // Execute insert query.

                //theCommand.CommandText = "update AC_Ledger set LedgerID=0 where LedgerFK='" + RegNo + "' and compcode='" + compcode + "'";
                //theCommand.CommandType = CommandType.Text;
                //theCommand.ExecuteNonQuery(); // Execute insert query.
            }
            else
            {
                theCommand.CommandText = "update OPD_PatientRegistration set Checked=NULL,Status=1 where PatientRegNo='" + RegNo + "' and compcode='" + compcode + "'";
                theCommand.CommandType = CommandType.Text;
                theCommand.ExecuteNonQuery(); // Execute insert query.
            }
            return true;
        }
        catch
        {
            return false;
        }
        finally
        {
            theConnection.Dispose();
            theCommand.Dispose();

        }

    }
}