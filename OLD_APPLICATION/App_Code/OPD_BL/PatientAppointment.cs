using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using System.Data;

/// <summary>
/// Summary description for PatientAppointment12
/// </summary>
public class PatientAppointment
{
	public PatientAppointment(string con)
	{
        conString = con;
	}

    private string conString;
    private SqlConnection theConnection;
    private SqlCommand theCommand;
    private SqlDataAdapter theAdapter;
    private DataTable hospitalTable;


    public DataTable DropdownSex()
    {
        // Connection.
        theConnection = new SqlConnection();
        theConnection.ConnectionString = conString;

        // Command.
        theCommand = new SqlCommand();
        theCommand.Connection = theConnection;
        theCommand.CommandText = "select * from GN_Gender where status=1";
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
    public DataTable DropdownDocType(string compcode)
    {
        // Connection.
        theConnection = new SqlConnection();
        theConnection.ConnectionString = conString;

        // Command.
        theCommand = new SqlCommand();
        theCommand.Connection = theConnection;
        theCommand.CommandText = "select * from  GN_DoctorType where compcode='" + compcode + "' and status=1";
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

    public DataTable DropdownConsultantDoc(string compcode, string type)
    {
        // Connection.
        theConnection = new SqlConnection();
        theConnection.ConnectionString = conString;

        // Command.
        theCommand = new SqlCommand();
        theCommand.Connection = theConnection;
        if (type == "0")
            theCommand.CommandText = "select * from GN_DoctorMaster where compcode='" + compcode + "' and status=1 and isNull(consultant,'N')='Y'";
        else
            theCommand.CommandText = "select * from GN_DoctorMaster where  DocTypeId='" + type + "' and compcode='" + compcode + "' and status=1 and isNull(consultant,'N')='Y'";
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

    public DataTable DropdownDoc(string compcode,string type)
    {
        // Connection.
        theConnection = new SqlConnection();
        theConnection.ConnectionString = conString;

        // Command.
        theCommand = new SqlCommand();
        theCommand.Connection = theConnection;
        if (type == "0")
            theCommand.CommandText = "select * from GN_DoctorMaster where compcode='" + compcode + "' and status=1";
        else
            theCommand.CommandText = "select * from GN_DoctorMaster where  DocTypeId='" + type + "' and compcode='" + compcode + "' and status=1";
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

    public DataTable getConsultationCharges(string compcode, string docid)
    {
        // Connection.
        theConnection = new SqlConnection();
        theConnection.ConnectionString = conString;

        // Command.
        theCommand = new SqlCommand();
        theCommand.Connection = theConnection;
        theCommand.CommandText = "select isNull(ConsultationChrg,0)ConsultationChrg from GN_DoctorMaster where  doc_id='" + docid + "' and compcode='" + compcode + "'";
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
    public DataTable DropdownDocDeptWise(string compcode, string DeptCode)
    {
        // Connection.
        theConnection = new SqlConnection();
        theConnection.ConnectionString = conString;

        // Command.
        theCommand = new SqlCommand();
        theCommand.Connection = theConnection;
        theCommand.CommandText = "select * from GN_DoctorMaster where  compcode='" + compcode + "' and DeptCode='" + DeptCode + "' and status=1";
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


    public DataTable DropdownDepartment(string compcode)
    {
        // Connection.
        theConnection = new SqlConnection();
        theConnection.ConnectionString = conString;

        // Command.
        theCommand = new SqlCommand();
        theCommand.Connection = theConnection;
        theCommand.CommandText = "select * from PH_DepartmentMaster where compcode='" + compcode + "' and status=1 order by DeptName";
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
    public DataTable DropPatientType(string compcode)
    {
        // Connection.
        theConnection = new SqlConnection();
        theConnection.ConnectionString = conString;

        // Command.
        theCommand = new SqlCommand();
        theCommand.Connection = theConnection;
        theCommand.CommandText = "select * from  OPD_PatientType where compcode='" + compcode + "' and status=1";
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

    public DataTable GetDistrict()
    {
        // Connection.
        theConnection = new SqlConnection();
        theConnection.ConnectionString = conString;

        // Command.
        theCommand = new SqlCommand();
        theCommand.Connection = theConnection;
        theCommand.CommandText = "select * from GN_District where status='1'";
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

    public DataTable GetSateWiseDistrict(string compcode,string statecode)
    {
        // Connection.
        theConnection = new SqlConnection();
        theConnection.ConnectionString = conString;

        // Command.
        theCommand = new SqlCommand();
        theCommand.Connection = theConnection;
        theCommand.CommandText = "select ID,DistrictName from GN_District where compcode='" + compcode + "' and StateCode='" + statecode + "'";
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

    public DataTable getCountry(string compcode)
    {
        // Connection.
        theConnection = new SqlConnection();
        theConnection.ConnectionString = conString;

        // Command.
        theCommand = new SqlCommand();
        theCommand.Connection = theConnection;
        theCommand.CommandText = "select * from GN_Country where compcode='"+ compcode +"'";
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

    public DataTable getState(string compcode,string CountryCode)
    {
        // Connection.
        theConnection = new SqlConnection();
        theConnection.ConnectionString = conString;

        // Command.
        theCommand = new SqlCommand();
        theCommand.Connection = theConnection;
        theCommand.CommandText = "select State_ID,State_Name from GN_StateMaster where compcode='" + compcode + "' and countryCode='" + CountryCode + "'";
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

    public DataTable GridFill(string compcode,string yearcode)
    {
        // Connection.
        theConnection = new SqlConnection();
        theConnection.ConnectionString = conString;

        // Command.
        theCommand = new SqlCommand();
        theCommand.Connection = theConnection;

        /*theCommand.CommandText = "select pr.*,sex.SexName,ptype.TypeName ptype,dt.TypeName dttype,dm.doc_name,CONVERT(varchar,AppointMentDate,103) apppdate "+
            "from dbo.OPD_PatientRegistration pr,dbo.GN_DoctorMaster dm,GN_DoctorType dt,GN_Gender sex,dbo.OPD_PatientType ptype "+
            "where pr.compcode=dm.compcode and pr.compcode=dt.compcode and pr.compcode=sex.compcode and pr.compcode=ptype.compcode and "+
            "pr.DocId=dm.doc_id and pr.DocTypeID=dt.DocTypeId and pr.Sex=sex.ID and pr.OPDPatientType=ptype.TypeId and pr.AppoType=2 and pr.compcode='" + compcode + "'";*/
        theCommand.CommandText = "select pr.*,sex.SexName,pr.AppoType ptype,dt.TypeName dttype,dm.doc_name,CONVERT(varchar,AppointMentDate,103) apppdate,CONVERT(varchar(10),AppointMentDate,120) strapppdate " +
            "from dbo.OPD_PatientAppointment pr,dbo.GN_DoctorMaster dm,GN_DoctorType dt,GN_Gender sex " +
            "where pr.compcode=dm.compcode and pr.compcode=dt.compcode and pr.compcode=sex.compcode and " +
            "pr.DocId=dm.doc_id and pr.DocTypeID=dt.DocTypeId and pr.Sex=sex.ID and pr.compcode='" + compcode + "' and pr.yearcode='"+yearcode+"' order by AppointMentDate desc";
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
    public DataTable RegGridFill(string compcode, string yearcode,string regno,string name,string phno,string regdate)
    {
        // Connection.
        theConnection = new SqlConnection();
        theConnection.ConnectionString = conString;

        // Command.
        theCommand = new SqlCommand();
        theCommand.Connection = theConnection;
        string lsSql = "";
        //lsSql = "select pr.*,sex.SexName,ptype.TypeName ptype,dt.TypeName dttype,dm.doc_name,CONVERT(varchar,AppointMentDate,103) apppdate,CONVERT(varchar,dob,103) dob1, " +
        //    "case isNull(pr.ChqDt_CardExpDt,'') when '' then '' else convert(varchar(10),pr.ChqDt_CardExpDt,103) end as ChqDt_CardExpDt  " +
        //    "from dbo.OPD_PatientRegistration pr,dbo.GN_DoctorMaster dm,GN_DoctorType dt,GN_Gender sex,dbo.OPD_PatientType ptype " +
        //    "where pr.compcode=dm.compcode and pr.compcode=dt.compcode and pr.compcode=sex.compcode and pr.compcode=ptype.compcode and " +
        //    "pr.DocId=dm.doc_id and pr.DocTypeID=dt.DocTypeId and pr.Sex=sex.ID and pr.OPDPatientType=ptype.TypeId and pr.AppoType=1 and pr.status=1 and pr.compcode='" + compcode + "' and pr.yearcode='" + yearcode + "' ";

        lsSql = "select pr.*,sex.SexName,CONVERT(varchar,AppointMentDate,103) apppdate,CONVERT(varchar(10),dob,120) dob1," +
            "case isNull(pr.ChqDt_CardExpDt,'') when '' then '' else convert(varchar(10),pr.ChqDt_CardExpDt,103) end as ChqDt_CardExpDt " +
            "from dbo.OPD_PatientRegistration pr,GN_Gender sex where pr.compcode=sex.compcode and pr.Sex=sex.ID and pr.status=1 and pr.compcode='" + compcode + "' " +
            "and pr.yearcode='" + yearcode + "' and isNull(OPDPatientType,'0')='0'";

        if (regno != "")
        {
            lsSql = lsSql + " And pr.PatientRegNo like'%" + regno + "%'";
        }

        if (name != "")
        {
            lsSql = lsSql + " And pr.PName like '%" + name + "%'";
        }

        if (phno != "")
        {
            lsSql = lsSql + " And RIGHT(pr.PhNo1,10) ='" + phno + "'";
        }
        if (regdate != "")
        {
            lsSql = lsSql + " And convert(Date,pr.AppointMentDate,103) ='" + regdate + "'";
        }
        lsSql = lsSql + " order by AppointMentDate desc ";
        theCommand.CommandText = lsSql;
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
    public DataTable GenerateRegno(string compcode,string yearcode)
    {
        DataTable custTable;

        // Connection.
        theConnection = new SqlConnection();
        theConnection.ConnectionString = conString;

        // Command.
        theCommand = new SqlCommand();
        theCommand.Connection = theConnection;
        theCommand.CommandText = "exec sp_OPDGenerateRegNo '" + compcode + "','" + yearcode + "'";
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
        theAdapter.Dispose();

        return custTable;
    }
    public DataTable GenerateReqNo(string compcode)
    {
        DataTable custTable;

        // Connection.
        theConnection = new SqlConnection();
        theConnection.ConnectionString = conString;

        // Command.
        theCommand = new SqlCommand();
        theCommand.Connection = theConnection;
        theCommand.CommandText = "exec sp_OPDGenerateReqNo '" + compcode + "'";
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
        theAdapter.Dispose();

        return custTable;
    }
    public DataTable GenerateLedgerID(string compcode)
    {
        DataTable custTable;

        // Connection.
        theConnection = new SqlConnection();
        theConnection.ConnectionString = conString;

        // Command.
        theCommand = new SqlCommand();
        theCommand.Connection = theConnection;
        theCommand.CommandText = "exec sp_GenerateLedgerId '" + compcode + "',P";
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
        theAdapter.Dispose();

        return custTable;
    }
    public DataTable CheckDayoff(string compcode,string Docid, string DayoffDay)
    {
        //string date = Convert.ToDateTime(d).ToShortDateString();
        if (DayoffDay == "")
            DayoffDay = "null";
        else
            DayoffDay = "'" + DayoffDay + "'";

        if (Docid == "")
            Docid = "null";
        theConnection = new SqlConnection();
        theConnection.ConnectionString = conString;

        // Command.
        theCommand = new SqlCommand();
        theCommand.Connection = theConnection;
        theCommand.CommandText = "exec sp_OPD_DayOff '" + compcode + "'," + Docid + "," + DayoffDay + "";
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

        return (hospitalTable);

    }
    public bool InsertAppointment(string compcode, string yearcode, string AppoNo, string PatientRegNo, string PName, string GuadianName, string Age, string Sex, string PhNo1, string PhNo2, string Address, string District, string DocTypeID, string DocId, string AppointMentDate, string AppointmentTime, string OPDPatientType, string AdvancedAmount, string Remarks, string AppoType, string CreatedBy, string CreatedDate)
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
            /*theCommand.CommandText = "INSERT INTO OPD_PatientRegistration(compcode,AppoNo,PatientRegNo,PName,GuadianName,Age,Sex,PhNo1,PhNo2,Address,District,DocTypeID,DocId,AppointMentDate,AppointmentTime,OPDPatientType,AdvancedAmount,Remarks,AppoType) VALUES('" + compcode + "','" + AppoNo + "','" + PatientRegNo + "','" + PName + "','" + GuadianName + "','" + Age + "','" + Sex + "','" + PhNo1 + "','" + PhNo2 + "','" + Address + "','" + District + "','" + DocTypeID + "','" + DocId + "','" + AppointMentDate + "','" + AppointmentTime + "','" + OPDPatientType + "','" + AdvancedAmount + "','" + Remarks + "','" + AppoType + "')";
            theCommand.CommandType = CommandType.Text;
            theCommand.ExecuteNonQuery();*/ // Execute insert query.

            theCommand.CommandText = "INSERT INTO OPD_PatientAppointment(compcode,yearcode,AppoNo,PatientRegNo,PName,GuadianName,Age,Sex,PhNo1,PhNo2,Address,District,DocTypeID,DocId,AppointMentDate,AppointmentTime,OPDPatientType,AdvancedAmount,Remarks,AppoType,Status,CreatedBy,logdt01) VALUES('" + compcode + "','" + yearcode + "','" + AppoNo + "','" + PatientRegNo + "','" + PName + "','" + GuadianName + "','" + Age + "','" + Sex + "','" + PhNo1 + "','" + PhNo2 + "','" + Address + "','" + District + "','" + DocTypeID + "','" + DocId + "','" + AppointMentDate + "','" + AppointmentTime + "','" + OPDPatientType + "','" + AdvancedAmount + "','" + Remarks + "','" + AppoType + "',1,'" + CreatedBy + "','" + CreatedDate + "')";
            theCommand.CommandType = CommandType.Text;
            theCommand.ExecuteNonQuery(); // Execute insert query.

            /*theCommand.CommandText = "INSERT INTO OPD_PatientRequisition(compcode,AppoNo,PatientRegNo,DocTypeID,DocId,AppointMentDate,AppointmentTime,OPDPatientType,AdvancedAmount,Remarks,AppoType) VALUES('" + compcode + "','" + AppoNo + "','" + PatientRegNo + "','" + DocTypeID + "','" + DocId + "','" + AppointMentDate + "','" + AppointmentTime + "','" + OPDPatientType + "','" + AdvancedAmount + "','" + Remarks + "','" + AppoType + "')";
            theCommand.CommandType = CommandType.Text;
            theCommand.ExecuteNonQuery();*/ // Execute insert query.            

            /*string sql;
            sql = "exec sp_GenerateLedgerId '" + compcode + "',P";
            SqlDataAdapter da = new SqlDataAdapter(sql, theConnection);
            DataTable dt = new DataTable();
            da.Fill(dt);
            string ledgerid = dt.Rows[0][0].ToString();


            theCommand.CommandText = "insert into AC_Ledger (compcode,LedgerID,LedgerFK,LedgerName,LedgerType,CreatedDate,CreatedBy,ActiveStatus)  values('" + compcode + "','" + ledgerid + "','" + PatientRegNo + "','" + PName + "','P','" + CreatedDate + "','" + CreatedBy + "',1)";
            theCommand.CommandType = CommandType.Text;
            theCommand.ExecuteNonQuery();

            theCommand.CommandText = "INSERT INTO AC_Transaction(compcode,yearcode,LadgerId, Debit, Credit, TrunsactionDate,EntryBy) VALUES ('" + compcode + "','" + yearcode + "','" + ledgerid + "', 0.00, '" + AdvancedAmount + "', '" + CreatedDate + "','" + CreatedBy + "')";
            theCommand.CommandType = CommandType.Text;
            theCommand.ExecuteNonQuery();*/
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

    public DataTable CheckUserId(string compcode, string id)
    {
        // Connection.
        theConnection = new SqlConnection();
        theConnection.ConnectionString = conString;

        // Command.
        theCommand = new SqlCommand();
        theCommand.Connection = theConnection;
        theCommand.CommandText = "select * from GN_PatientReg where PUserId='" + id + "' and compcode='" + compcode + "'";
        theCommand.CommandType = CommandType.Text;

        // Adapter.
        theAdapter = new SqlDataAdapter();
        theAdapter.SelectCommand = theCommand;

        // Datatable.
        DataTable BedTable = new DataTable();
        theAdapter.Fill(BedTable); // Fill data into data table.

        // Clean up.
        theConnection.Dispose();
        theCommand.Dispose();
        theAdapter.Dispose();

        return BedTable;
    }

    public bool InsertRegistration(string type, string compcode, string yearcode, string PatientRegNo, string PName, string GuadianName, string Age, string Sex, string PhNo1, string PhNo2, string Address, string District, string Remarks, string dob, string CreatedBy, string PUserId, string PPassword, string CreatedDate, string AadhaarNo, string panno, string filepath, string vchnoU, string RefDoc, string SpouseName, string PresentPin, string PresentState, string PresentCountry, string ParmAddr, string ParmanentPin, string ParmDist, string ParmState, string parmCountry,string EmailId)
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

            using (IDbTransaction tran = theConnection.BeginTransaction())
            {
                try
                {
                    // transactional code...
                    using (theCommand = theConnection.CreateCommand())
                    {
                        theCommand.CommandText = "select * from OPD_PatientRegistration where PatientRegNo='" + PatientRegNo + "' and compcode='" + compcode + "' and yearcode='" + yearcode + "'";
                        theCommand.Transaction = tran as SqlTransaction;
                        SqlDataReader theReader = theCommand.ExecuteReader();
                        theReader.Read();

                        if (theReader.HasRows == false)
                        {
                            theReader.Close();

                            theCommand.CommandText = "INSERT INTO OPD_PatientRegistration(compcode,YearCode,DOB,PatientRegNo,AppointMentDate,AppointmentTime,PName,GuadianName,Age,Sex,PhNo1,PhNo2,Address,District,Remarks,Status,CreatedBy,PUserId,PPassword,AADHAARNO,PanNo,FilePath,ReferedBy,SpouseName,PresentPin,PresentState,PresentCountry,ParmAddr,ParmanentPin,ParmDist,ParmState,parmCountry,EmailId) VALUES('" + compcode + "','" + yearcode + "',case '" + dob + "' when '' then Null else '" + dob + "' end,'" + PatientRegNo + "','" + CreatedDate + "',convert(varchar(5), GETDATE(), 108),'" + PName + "','" + GuadianName + "','" + Age + "','" + Sex + "','" + PhNo1 + "','" + PhNo2 + "','" + Address + "','" + District + "','" + Remarks + "',1,'" + CreatedBy + "','" + PUserId + "','" + PPassword + "','" + AadhaarNo + "','" + panno + "','" + filepath + "','" + RefDoc + "','" + SpouseName + "','" + PresentPin + "','" + PresentState + "','" + PresentCountry + "','" + ParmAddr + "','" + ParmanentPin + "','" + ParmDist + "','" + ParmState + "','" + parmCountry + "','"+ EmailId +"')";

                            theCommand.Transaction = tran as SqlTransaction;
                            theCommand.ExecuteNonQuery();


                            theCommand.CommandText = "exec sp_GenerateLedgerId  '" + compcode + "','P'";
                            theCommand.Transaction = tran as SqlTransaction;
                            SqlDataReader theReader1 = theCommand.ExecuteReader();
                            theReader1.Read();
                            string ledgerid = theReader1[0].ToString();
                            theReader1.Close();

                            theCommand.CommandText = "insert into AC_Ledger (compcode,LedgerID,LedgerFK,LedgerName,LedgerType,CreatedDate,ActiveStatus)  values('" + compcode + "','" + ledgerid + "','" + PatientRegNo + "','" + PName + "','P','" + CreatedDate + "',1)";
                            theCommand.Transaction = tran as SqlTransaction;
                            theCommand.ExecuteNonQuery();

                            theCommand.CommandText = "select * from OPD_ChargeDetails cd where cd.PatientReg='" + PatientRegNo + "' and  CONVERT(varchar,cd.IssueDate,103)=CONVERT(varchar,GETDATE(),103) and compcode='" + compcode + "' and yearcode='" + yearcode + "'";
                            theCommand.Transaction = tran as SqlTransaction;
                            SqlDataReader theReader5 = theCommand.ExecuteReader();
                            theReader5.Read();

                            if (theReader5.HasRows == false)
                            {
                                theReader5.Close();
                                theCommand.CommandText = "insert into OPD_ChargeDetails (compcode,yearcode,LedgerId,RegnFees,DoctorFees,Status,PatientReg,IssueDate) values ('" + compcode + "','" + yearcode + "','" + ledgerid + "','0','0',1,'" + PatientRegNo + "','" + CreatedDate + "')";
                                theReader.Close();
                                theCommand.Transaction = tran as SqlTransaction;
                                theCommand.ExecuteNonQuery();
                            }
                            else
                            {
                                theReader5.Close();
                            }

                        }
                        else
                        {
                            theReader.Close();
                            theCommand.CommandText = "update OPD_PatientRegistration set DOB=case '" + dob + "' when '' then Null else '" + dob + "' end,PName='" + PName + "',GuadianName='" + GuadianName + "',Age='" + Age + "',Sex='" + Sex + "',PhNo1='" + PhNo1 + "',PhNo2='" + PhNo2 + "',Address='" + Address + "',District='" + District + "',Remarks='" + Remarks + "',Status=1,Checked=null,AADHAARNO='" + AadhaarNo + "',PanNo='" + panno + "',FilePath='" + filepath + "',ReferedBy='" + RefDoc + "',PresentPin='" + PresentPin + "',PresentState='" + PresentState + "',PresentCountry='" + PresentCountry + "',ParmAddr='" + ParmAddr + "',ParmanentPin='" + ParmanentPin + "',ParmDist='" + ParmDist + "',ParmState='" + ParmState + "',parmCountry='" + parmCountry + "',EmailId='"+ EmailId +"'  where  PatientRegNo='" + PatientRegNo + "' and compcode='" + compcode + "' and yearcode='" + yearcode + "'";
                            theCommand.Transaction = tran as SqlTransaction;
                            theCommand.ExecuteNonQuery();

                            theCommand.CommandText = "select * from ph_patientreq  where RegistrationNo='" + PatientRegNo + "' and   compcode='" + compcode + "'";
                            theCommand.Transaction = tran as SqlTransaction;
                            SqlDataReader theReader8 = theCommand.ExecuteReader();
                            theReader8.Read();
                            if (theReader8.HasRows == true)
                            {
                                theReader8.Close();
                                theCommand.CommandText = "update ph_patientreq set PatientName='" + PName + "',Ph1='" + PhNo1 + "',Ph2='" + PhNo2 + "',Address='" + Address + "',Age='" + Age + "' where  RegistrationNo='" + PatientRegNo + "' and compcode='" + compcode + "'";
                                theCommand.Transaction = tran as SqlTransaction;
                                theCommand.ExecuteNonQuery();
                            }
                        }
                    }
                    tran.Commit();
                    return true;
                }
                catch
                {
                    tran.Rollback();
                    throw;
                }
            }
            
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

    //public bool InsertRegistration(string type, string compcode, string yearcode, string AppoNo, string PatientRegNo, string PName, string GuadianName, string Age, string Sex, string PhNo1, string PhNo2, string Address, string District, string DoctTypeId, string DoctId, string AppointMentDate, string AppointmentTime, string OPDPatientType, Decimal AdvancedAmount, string Remarks, string AppoType, string dob, string CreatedBy, string PUserId, string PPassword, string CreatedDate, string AadhaarNo, string panno, string filepath, string paymentMode, string BankName,string ChequeNo,string chqdt,string vchnoU,string RefDoc)
    //{
    //    try
    //    {
    //        // Connection.
    //        theConnection = new SqlConnection();
    //        if (conString != "")
    //        {
    //            theConnection.ConnectionString = conString;
    //            theConnection.Open();
    //        }

    //        // Command.
    //        theCommand = new SqlCommand();
    //        theCommand.Connection = theConnection;

    //        using (IDbTransaction tran = theConnection.BeginTransaction())
    //        {
    //            try
    //            {
    //                // transactional code...
    //                using (theCommand = theConnection.CreateCommand())
    //                {
    //                    theCommand.CommandText = "select * from OPD_PatientRegistration where PatientRegNo='" + PatientRegNo + "' and compcode='" + compcode + "' and yearcode='" + yearcode + "'";
    //                    theCommand.Transaction = tran as SqlTransaction;
    //                    SqlDataReader theReader = theCommand.ExecuteReader();
    //                    theReader.Read();

    //                    if (theReader.HasRows == false)
    //                    {
    //                        theReader.Close();

    //                        string bookCode = "";
    //                        if (paymentMode == "C")
    //                        {
    //                            theCommand.CommandText = "select bkcode from bkcodes where compcode='" + compcode + "' and bktype='C'";
    //                        }
    //                        else
    //                        {
    //                            theCommand.CommandText = "select bkcode from bkcodes where compcode='" + compcode + "' and bktype='B'";
    //                        }
    //                        theCommand.Transaction = tran as SqlTransaction;
    //                        SqlDataReader theReader2 = theCommand.ExecuteReader();
    //                        theReader2.Read();
    //                        bookCode = theReader2[0].ToString();
    //                        theReader2.Close();


    //                        theCommand.CommandText = "exec Dsp_GenDocNo '" + compcode + "','" + yearcode + "','','" + bookCode + "','I','R','Y',''";
    //                        theCommand.Transaction = tran as SqlTransaction;
    //                        SqlDataReader theReader3 = theCommand.ExecuteReader();
    //                        theReader3.Read();
    //                        string Vchno = theReader3[0].ToString();
    //                        theReader3.Close();


    //                        theCommand.CommandText = "INSERT INTO OPD_PatientRegistration(compcode,YearCode,DocTypeID,DocId,DOB,AppoNo,PatientRegNo,PName,GuadianName,Age,Sex,PhNo1,PhNo2,Address,District,AppointMentDate,AppointmentTime,OPDPatientType,AdvancedAmount,Remarks,AppoType,Status,CreatedBy,PUserId,PPassword,AADHAARNO,PanNo,FilePath,PaymentMode,Chq_CardNo,Bank_CardHolderName,ChqDt_CardExpDt,Vchno,ReferedBy) VALUES('" + compcode + "','" + yearcode + "','" + DoctTypeId + "','" + DoctId + "','" + dob + "','" + AppoNo + "','" + PatientRegNo + "','" + PName + "','" + GuadianName + "','" + Age + "','" + Sex + "','" + PhNo1 + "','" + PhNo2 + "','" + Address + "','" + District + "','" + AppointMentDate + "','" + AppointmentTime + "','" + OPDPatientType + "','" + AdvancedAmount + "','" + Remarks + "','" + AppoType + "',1,'" + CreatedBy + "','" + PUserId + "','" + PPassword + "','" + AadhaarNo + "','" + panno + "','" + filepath + "','" + paymentMode + "','" + ChequeNo + "','" + BankName + "','" + chqdt + "','" + Vchno + "','" + RefDoc + "')";

    //                        theCommand.Transaction = tran as SqlTransaction;
    //                        theCommand.ExecuteNonQuery();

    //                        //theCommand.CommandText = "insert into OPD_PatientRequisition (compcode,AppoNo,PatientRegNo,DocTypeID,DocId,AppointMentDate,AppointmentTime,OPDPatientType,AdvancedAmount,Remarks,Status)  values('" + compcode + "','" + AppoNo + "','" + PatientRegNo + "','" + DoctTypeId + "','" + DoctId + "','" + AppointMentDate + "','" + AppointmentTime + "','" + OPDPatientType + "','" + AdvancedAmount + "','" + Remarks + "',1)";
    //                        //theCommand.Transaction = tran as SqlTransaction;
    //                        //theCommand.ExecuteNonQuery();


    //                        theCommand.CommandText = "exec sp_GenerateLedgerId  '" + compcode + "','P'";
    //                        theCommand.Transaction = tran as SqlTransaction;
    //                        SqlDataReader theReader1 = theCommand.ExecuteReader();
    //                        theReader1.Read();
    //                        string ledgerid = theReader1[0].ToString();
    //                        theReader1.Close();

    //                        theCommand.CommandText = "insert into AC_Ledger (compcode,LedgerID,LedgerFK,LedgerName,LedgerType,CreatedDate,ActiveStatus)  values('" + compcode + "','" + ledgerid + "','" + PatientRegNo + "','" + PName + "','P','" + CreatedDate + "',1)";
    //                        theCommand.Transaction = tran as SqlTransaction;
    //                        theCommand.ExecuteNonQuery();

    //                        // Generate receipt No ..
    //                        theCommand.CommandText = "exec sp_ACCOUNT_GenerateReceiptNo '"+compcode+"'";
    //                        theCommand.Transaction = tran as SqlTransaction;
    //                        SqlDataReader theReader4 = theCommand.ExecuteReader();
    //                        theReader4.Read();
    //                        string ReceiptNo = theReader4[0].ToString();
    //                        theReader4.Close();

    //                        if (AdvancedAmount >0)
    //                        {
    //                            theCommand.CommandText = "INSERT INTO AC_Transaction(compcode,yearcode,LadgerId, Credit, TrunsactionDate,ReceiptNo,PaymentType,Reason) VALUES ('" + compcode + "','" + yearcode + "','" + ledgerid + "','" + AdvancedAmount + "', '" + CreatedDate + "','" + ReceiptNo + "',5,'Advance Payment')";
    //                            theCommand.Transaction = tran as SqlTransaction;
    //                            theCommand.ExecuteNonQuery();
    //                        }

    //                        //Update Appointment 
    //                        theCommand.CommandText = "update OPD_PatientAppointment SET PName='" + PName + "',GuadianName='" + GuadianName + "',Age='" + Age + "',Sex='" + Sex + "',PhNo1='" + PhNo1 + "',PhNo2='" + PhNo2 + "',Address='" + Address + "',District='" + District + "',AppointMentDate='" + AppointMentDate + "',AppointmentTime='" + AppointmentTime + "',OPDPatientType='" + OPDPatientType + "',AdvancedAmount='" + AdvancedAmount + "',Remarks='" + Remarks + "',AppoType='" + AppoType + "',PatientRegNo='" + PatientRegNo + "' where AppoNo='" + AppoNo + "' and compcode='"+compcode+"' and yearcode='"+yearcode+"'";
    //                        theReader.Close();
    //                        theCommand.Transaction = tran as SqlTransaction;
    //                        theCommand.ExecuteNonQuery();

    //                        theCommand.CommandText = "select * from OPD_ChargeDetails cd where cd.PatientReg='" + PatientRegNo + "' and  CONVERT(varchar,cd.IssueDate,103)=CONVERT(varchar,GETDATE(),103) and compcode='"+compcode+"' and yearcode='"+yearcode+"'";
    //                        theCommand.Transaction = tran as SqlTransaction;
    //                        SqlDataReader theReader5 = theCommand.ExecuteReader();
    //                        theReader5.Read();

    //                        if (theReader5.HasRows == false)
    //                        {
    //                            theReader3.Close();
    //                            theCommand.CommandText = "insert into OPD_ChargeDetails (compcode,yearcode,LedgerId,RegnFees,DoctorFees,Status,PatientReg,IssueDate) values ('" + compcode + "','"+ yearcode +"','" + ledgerid + "','0','0',1,'" + PatientRegNo + "','" + AppointMentDate + "')";
    //                            theReader.Close();
    //                            theCommand.Transaction = tran as SqlTransaction;
    //                            theCommand.ExecuteNonQuery();
    //                        }
    //                        else
    //                            theReader3.Close();

    //                        theCommand.CommandText = "exec Dsp_CollectionAccEffect '" + compcode + "','" + yearcode + "','" + CreatedBy + "','" + Vchno + "','" + AppointMentDate + "','" + bookCode + "','" + ledgerid + "','" + AdvancedAmount + "',2";
    //                        theCommand.Transaction = tran as SqlTransaction;
    //                        theCommand.ExecuteNonQuery(); // Execute insert query. 

    //                        theCommand.CommandText = "exec Dsp_GenDocNo '" + compcode + "','" + yearcode + "','','" + bookCode + "','U','R','Y',''";
    //                        theCommand.Transaction = tran as SqlTransaction;
    //                        SqlDataReader theReader6 = theCommand.ExecuteReader();
    //                        theReader6.Read();
    //                        theReader6.Close();
    //                    }

    //                    else
    //                    {
    //                        theReader.Close();
    //                        theCommand.CommandText = "update OPD_PatientRegistration set DocTypeID='" + DoctTypeId + "',DocId='" + DoctId + "', DOB='" + dob + "',PName='" + PName + "',GuadianName='" + GuadianName + "',Age='" + Age + "',Sex='" + Sex + "',PhNo1='" + PhNo1 + "',PhNo2='" + PhNo2 + "',Address='" + Address + "',District='" + District + "',AppointMentDate='" + AppointMentDate + "',AppointmentTime='" + AppointmentTime + "',OPDPatientType='" + OPDPatientType + "',AdvancedAmount='" + AdvancedAmount + "',Remarks='" + Remarks + "',AppoType='" + AppoType + "' ,Status=1,Checked=null,AADHAARNO='" + AadhaarNo + "',PanNo='" + panno + "',FilePath='" + filepath + "',PaymentMode='" + paymentMode + "',Chq_CardNo='" + ChequeNo + "',Bank_CardHolderName='" + BankName + "',ChqDt_CardExpDt='" + chqdt + "',ReferedBy='"+ RefDoc +"'  where  PatientRegNo='" + PatientRegNo + "' and compcode='" + compcode + "' and yearcode='" + yearcode + "'";
    //                        theCommand.Transaction = tran as SqlTransaction;
    //                        theCommand.ExecuteNonQuery();

    //                        theCommand.CommandText = "update OPD_PatientAppointment set DocTypeID='" + DoctTypeId + "',DocId='" + DoctId + "', DOB='" + dob + "',PName='" + PName + "',GuadianName='" + GuadianName + "',Age='" + Age + "',Sex='" + Sex + "',PhNo1='" + PhNo1 + "',PhNo2='" + PhNo2 + "',Address='" + Address + "',District='" + District + "',AppointMentDate='" + AppointMentDate + "',AppointmentTime='" + AppointmentTime + "',OPDPatientType='" + OPDPatientType + "',AdvancedAmount='" + AdvancedAmount + "',Remarks='" + Remarks + "',AppoType='" + AppoType + "' ,Status=1  where  AppoNo='" + AppoNo + "' and compcode='" + compcode + "' and yearcode='" + yearcode + "'";
    //                        theCommand.Transaction = tran as SqlTransaction;
    //                        theCommand.ExecuteNonQuery();

                            


    //                        theCommand.CommandText = "select * from OPD_ChargeDetails cd where cd.PatientReg='" + PatientRegNo + "' and  CONVERT(varchar,cd.IssueDate,103)=CONVERT(varchar,GETDATE(),103) and compcode='" + compcode + "' and yearcode='" + yearcode + "'";
    //                        theCommand.Transaction = tran as SqlTransaction;
    //                        SqlDataReader theReader3 = theCommand.ExecuteReader();
    //                        theReader3.Read();

    //                        if (theReader3.HasRows == false)
    //                        {
    //                            theReader3.Close();
    //                            theCommand.CommandText = "insert into OPD_ChargeDetails (compcode,yearcode,RegnFees,DoctorFees,Status,PatientReg,IssueDate) values ( '" + compcode + "','"+ yearcode +"','0',0,1,'" + PatientRegNo + "','" + AppointMentDate + "')";
    //                            theReader.Close();
    //                            theCommand.Transaction = tran as SqlTransaction;
    //                            theCommand.ExecuteNonQuery();
    //                        }
    //                        else
    //                            theReader3.Close();


    //                        string bookCode = "";
    //                        if (paymentMode == "C")
    //                        {
    //                            theCommand.CommandText = "select bkcode from bkcodes where compcode='" + compcode + "' and bktype='C'";
    //                        }
    //                        else
    //                        {
    //                            theCommand.CommandText = "select bkcode from bkcodes where compcode='" + compcode + "' and bktype='B'";
    //                        }
    //                        //theCommand.Transaction = tran as SqlTransaction;
    //                        SqlDataReader theReader6 = theCommand.ExecuteReader();
    //                        theReader6.Read();
    //                        bookCode = theReader6[0].ToString();
    //                        theReader6.Close();

    //                        theCommand.CommandText = "select LedgerID from AC_Ledger l where l.LedgerFK='" + PatientRegNo + "' /*and CONVERT(varchar,l.CreatedDate,103)=CONVERT(varchar,GETDATE(),103) */and compcode='" + compcode + "' and activestatus=1";
    //                        theCommand.Transaction = tran as SqlTransaction;
    //                        SqlDataReader theReader_Ledger = theCommand.ExecuteReader();
    //                        theReader_Ledger.Read();

    //                        string Ledger_upd = theReader_Ledger[0].ToString().Trim();
    //                        theReader_Ledger.Close();


    //                        theCommand.CommandText = "exec Dsp_CollectionAccEffect '" + compcode + "','" + yearcode + "','" + CreatedBy + "','" + vchnoU + "','" + AppointMentDate + "','" + bookCode + "','" + Ledger_upd + "','" + AdvancedAmount + "',2";
    //                        theCommand.CommandType = CommandType.Text;
    //                        theCommand.ExecuteNonQuery(); // Execute insert query. 

                            
                            
                            
                            
    //                    }
    //                }

    //                tran.Commit();
    //            }
    //            catch
    //            {
    //                tran.Rollback();
    //                throw;
    //            }
    //        }
    //    }
    //    catch
    //    {
    //        return false;
    //    }
    //    return true;

    //}


    public bool UpdateChargeDetails(string compcode, string yearcode, string regno, string type, string date, string amount)
    {
        try
        {
            string lsSql = "";
            // Connection.
            theConnection = new SqlConnection();
            if (conString != "")
            {
                theConnection.ConnectionString = conString;
                theConnection.Open();
            }

            using (IDbTransaction tran = theConnection.BeginTransaction())
            {
                try
                {
                    // transactional code...
                    using (theCommand = theConnection.CreateCommand())
                    {
                        theCommand.CommandText = "select LedgerID from AC_Ledger l where l.LedgerFK='" + regno + "' and compcode='" + compcode + "'";
                        theCommand.Transaction = tran as SqlTransaction;
                        SqlDataReader theReader_Ledger = theCommand.ExecuteReader();
                        theReader_Ledger.Read();

                        string LedgerId = theReader_Ledger[0].ToString().Trim();
                        theReader_Ledger.Close();
                        theCommand.CommandText = "select * from OPD_ChargeDetails cd where cd.PatientReg='" + regno + "' and  cd.IssueDate='" + date + "' and compcode='" + compcode + "' and yearcode='" + yearcode + "'";
                        theCommand.Transaction = tran as SqlTransaction;
                        SqlDataReader theReader5 = theCommand.ExecuteReader();
                        theReader5.Read();

                        if (theReader5.HasRows == false)
                        {

                            theReader5.Close();
                            theCommand.CommandText = "insert into OPD_ChargeDetails (compcode,yearcode,LedgerId,RegnFees,DoctorFees,Status,PatientReg,IssueDate) values ('" + compcode + "','" + yearcode + "','" + LedgerId + "','0','0',1,'" + regno + "',CONVERT(varchar,'" + date + "',103))";
                            theCommand.Transaction = tran as SqlTransaction;
                            theCommand.ExecuteNonQuery();
                        }
                        else
                        {
                            theReader5.Close();
                        }

                        if (type == "R")
                        {
                            lsSql = "update OPD_ChargeDetails set RegnFees='" + amount + "' where compcode='" + compcode + "'  and yearcode='" + yearcode + "'  and IssueDate='" + date + "'";
                        }
                        else
                        {
                            lsSql = "update OPD_ChargeDetails set DoctorFees='" + amount + "' where compcode='" + compcode + "'  and yearcode='" + yearcode + "' and IssueDate='" + date + "'";
                        }
                        theCommand.CommandText = lsSql;
                        theCommand.Transaction = tran as SqlTransaction;
                        theCommand.ExecuteNonQuery();
                    }
                    tran.Commit();
                }
                catch (Exception ex)
                {
                    tran.Rollback();
                    throw;
                }
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

    public bool InsertMonitor(string compcode, string reg, string appodate)
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
            theCommand.CommandText = "INSERT INTO DC_PatientMonitor(compcode,PatientReg,Date1) VALUES('" + compcode + "','" + reg + "','" + appodate + "')";
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
    public bool UpdateAppointment(string compcode, string yearcode, string AppoNo, string PatientRegNo, string PName, string GuadianName, string Age, string Sex, string PhNo1, string PhNo2, string Address, string District, string DocTypeID, string DocId, string AppointMentDate, string AppointmentTime, string OPDPatientType, string AdvancedAmount, string Remarks, string AppoType, string user, string date)
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

            theCommand.CommandText = "update OPD_PatientAppointment set PName='" + PName + "',GuadianName='" + GuadianName + "',Age='" + Age + "',Sex='" + Sex + "',PhNo1='" + PhNo1 + "',PhNo2='" + PhNo2 + "',Address='" + Address + "',District='" + District + "',DocTypeID='" + DocTypeID + "',DocId='" + DocId + "',AppointMentDate='" + AppointMentDate + "',AppointmentTime='" + AppointmentTime + "',OPDPatientType='" + OPDPatientType + "',AdvancedAmount='" + AdvancedAmount + "',Remarks='" + Remarks + "',user02='" + user + "',logdt02='" + date + "' where AppoNo = '" + AppoNo + "' and compcode='" + compcode + "' and yearcode='" + yearcode + "'";
            theCommand.CommandType = CommandType.Text;
            theCommand.ExecuteNonQuery(); // Execute insert query.
            /*theCommand.CommandText = "update OPD_PatientRegistration set PName='" + PName + "',GuadianName='" + GuadianName + "',Age='" + Age + "',Sex='" + Sex + "',PhNo1='" + PhNo1 + "',PhNo2='" + PhNo2 + "',Address='" + Address + "',District='" + District + "',DocTypeID='" + DocTypeID + "',DocId='" + DocId + "',AppointMentDate='" + AppointMentDate + "',AppointmentTime='" + AppointmentTime + "',OPDPatientType='" + OPDPatientType + "',AdvancedAmount='" + AdvancedAmount + "',Remarks='" + Remarks + "' where AppoNo = '" + AppoNo + "' and compcode='"+compcode+"'";
            theCommand.CommandType = CommandType.Text;
            theCommand.ExecuteNonQuery(); // Execute insert query.
            theCommand.CommandText = "update OPD_PatientRequisition set DocTypeID='" + DocTypeID + "',DocId='" + DocId + "',AppointMentDate='" + AppointMentDate + "',AppointmentTime='" + AppointmentTime + "',OPDPatientType='" + OPDPatientType + "',AdvancedAmount='" + AdvancedAmount + "',Remarks='" + Remarks + "' where AppoNo = '" + AppoNo + "' and compcode='" + compcode + "'";
            theCommand.CommandType = CommandType.Text;
            theCommand.ExecuteNonQuery();*/
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
    public DataTable GetPatientDetails(string compcode,string CustID)
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
        theCommand.CommandText = "exec sp_OPD_PatientDetails '" + compcode + "','" + CustID + "',null,null,null";
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
        theAdapter.Dispose();
        return custTable;
    }
    public DataTable GetOpdRegPatientDetails(string compcode, string AppoNo,string RegNo)
    {
        DataTable custTable;
        if (AppoNo == "")
            AppoNo = "null";
        if (RegNo == "")
            RegNo = "null";
        // Connection.
        theConnection = new SqlConnection();
        theConnection.ConnectionString = conString;

        // Command.
        theCommand = new SqlCommand();
        theCommand.Connection = theConnection;
        theCommand.CommandText = "exec sp_OPD_PatientAppointment '" + compcode + "','" + AppoNo + "','" + RegNo + "',null,null,null,null,null,null";
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
        theAdapter.Dispose();
        return custTable;
    }
    public bool DeleteAppointment(string compcode, string yearcode, string id)
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
            theCommand.CommandText = "delete OPD_PatientRegistration WHERE AppoNo='" + id + "' and compcode='" + compcode + "' and yearcode='" + yearcode + "'";
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

    public DataTable GetOpdRegbyPhone(string compcode, string PhoneNo)
    {
        DataTable custTable;
        
        // Connection.
        theConnection = new SqlConnection();
        theConnection.ConnectionString = conString;

        // Command.
        theCommand = new SqlCommand();
        theCommand.Connection = theConnection;
        theCommand.CommandText = "select * from OPD_PatientRegistration where compcode='" + compcode + "' and PhNo1='" + PhoneNo + "'";
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
        theAdapter.Dispose();
        return custTable;
    }

    public bool InsertRegistration(string type, string compcode, string yearcode, string PatientRegNo, string PName, string ledgerid,string User)
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

            using (IDbTransaction tran = theConnection.BeginTransaction())
            {
                try
                {
                    // transactional code...
                    using (theCommand = theConnection.CreateCommand())
                    {
                        theCommand.CommandText = "select * from OPD_PatientRegistration where PatientRegNo='" + PatientRegNo + "' and compcode='" + compcode + "' and yearcode='" + yearcode + "'";
                        theCommand.Transaction = tran as SqlTransaction;
                        SqlDataReader theReader = theCommand.ExecuteReader();
                        theReader.Read();

                        if (theReader.HasRows == false)
                        {
                            theReader.Close();

                            theCommand.CommandText = "INSERT INTO OPD_PatientRegistration(compcode,YearCode,PatientRegNo,AppointMentDate,PName,CreatedBy,OPDPatientType) VALUES('" + compcode + "','" + yearcode + "','" + PatientRegNo + "',getdate(),'" + PName + "','" + User + "','1')";

                            theCommand.Transaction = tran as SqlTransaction;
                            theCommand.ExecuteNonQuery();


                            

                            theCommand.CommandText = "insert into AC_Ledger (compcode,LedgerID,LedgerFK,LedgerName,LedgerType,CreatedDate,ActiveStatus)  values('" + compcode + "','" + ledgerid + "','" + PatientRegNo + "','" + PName + "','P',getdate(),1)";
                            theCommand.Transaction = tran as SqlTransaction;
                            theCommand.ExecuteNonQuery();

                            

                        }
                        else
                        {
                            theReader.Close();
                            theCommand.CommandText = "update OPD_PatientRegistration set PName='" + PName + "'  where  PatientRegNo='" + PatientRegNo + "' and compcode='" + compcode + "' and yearcode='" + yearcode + "'";
                            theCommand.Transaction = tran as SqlTransaction;
                            theCommand.ExecuteNonQuery();

                            
                        }
                    }
                    tran.Commit();
                    return true;
                }
                catch
                {
                    tran.Rollback();
                    throw;
                }
            }

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

    public string GenerateLedgerId(string compcode, string yearcode)
    {
        DataTable custTable;

        // Connection.
        theConnection = new SqlConnection();
        theConnection.ConnectionString = conString;

        // Command.
        theCommand = new SqlCommand();
        theCommand.Connection = theConnection;
        theCommand.CommandText = "exec sp_GenerateLedgerId '" + compcode + "','P'";
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
        theAdapter.Dispose();

        return custTable.Rows[0][0].ToString();
    }
}

