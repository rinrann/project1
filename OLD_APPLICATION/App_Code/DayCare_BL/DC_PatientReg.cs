using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using System.Data;

/// <summary>
/// Summary description for DC_PatientReg123
/// </summary>
public class DC_PatientReg
{
	public DC_PatientReg(string con)
	{
        conString = con;
	}

    private string conString;
    private SqlConnection theConnection;
    private SqlCommand theCommand;
    private SqlCommand theCommand1;
    private SqlDataAdapter theAdapter;
    private DataTable hospitalTable;
    private DataTable assoidtable;
    private DataTable diano;
    private DataTable regno;

    public DataTable DropdownState(string compcode)
    {
        // Connection.
        theConnection = new SqlConnection();
        theConnection.ConnectionString = conString;

        // Command.
        theCommand = new SqlCommand();
        theCommand.Connection = theConnection;
        theCommand.CommandText = "select * from GN_StateMaster where Status=1 and compcode='"+compcode+"'";
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
    public DataTable GridFill(string compcode, string reg)
    {
        // Connection.
        theConnection = new SqlConnection();
        theConnection.ConnectionString = conString;

        // Command.
        theCommand = new SqlCommand();
        theCommand.Connection = theConnection;
        theCommand.CommandText = "select pr.PatientReg,pr.patient_name,map.DialysisNo, COUNT(*) Total from dbo.DC_PatientDialysisMap map,GN_PatientReg pr  where pr.compcode=map.compcode and pr.PatientReg=map.PatientReg and map.PatientReg='" + reg + "' and pr.compcode='"+compcode+"' group by pr.PatientReg,pr.patient_name,map.DialysisNo";
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

    public DataTable DropdownCity(string compcode, int id)
    {
        // Connection.
        theConnection = new SqlConnection();
        theConnection.ConnectionString = conString;

        // Command.
        theCommand = new SqlCommand();
        theCommand.Connection = theConnection;
        theCommand.CommandText = "select * from CityMaster where Status=1 and State_ID=" + id + " and compcode='"+compcode+"'";
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
    public DataTable GetGender(string compcode)
    {
        // Connection.
        theConnection = new SqlConnection();
        theConnection.ConnectionString = conString;

        // Command.
        theCommand = new SqlCommand();
        theCommand.Connection = theConnection;
        theCommand.CommandText = "select * from GN_Gender where status=1 and compcode='"+compcode+"'";
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
    public DataTable DropdownDialysertype(string compcode)
    {
        // Connection.
        theConnection = new SqlConnection();
        theConnection.ConnectionString = conString;

        // Command.
        theCommand = new SqlCommand();
        theCommand.Connection = theConnection;
        theCommand.CommandText = "select t.TypeId,t.TypeName from dbo.DC_DialysisType t where t.Status=1 and compcode='"+compcode+"'";
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



    public DataTable DropdownDistrict(string compcode)
    {
        // Connection.
        theConnection = new SqlConnection();
        theConnection.ConnectionString = conString;

        // Command.
        theCommand = new SqlCommand();
        theCommand.Connection = theConnection;
        theCommand.CommandText = "select * from dbo.GN_District where status=1 and compcode='"+compcode+"'";
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


    public DataTable DropdownDialyserName(string compcode, string id)
    {
        // Connection.
        theConnection = new SqlConnection();
        theConnection.ConnectionString = conString;

        // Command.
        theCommand = new SqlCommand();
        theCommand.Connection = theConnection;

        if (id == "0")
            theCommand.CommandText = "select * from dbo.DC_DialysisCharge where Status=1 and compcode='"+compcode+"'";
        else
            theCommand.CommandText = "select * from dbo.DC_DialysisCharge where Status=1 and TypeId='" + id + "' and compcode='"+compcode+"'";
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
    public DataTable DropdownDialyserCharge(string compcode, string id)
    {
        // Connection.
        theConnection = new SqlConnection();
        theConnection.ConnectionString = conString;

        // Command.
        theCommand = new SqlCommand();
        theCommand.Connection = theConnection;
        theCommand.CommandText = "select * from dbo.DC_DialysisCharge where Status=1 and ID='" + id + "' and compcode='"+compcode+"'";
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

    public DataTable GridChemical()
    {
        // Connection.
        theConnection = new SqlConnection();
        theConnection.ConnectionString = conString;

        // Command.
        theCommand = new SqlCommand();
        theCommand.Connection = theConnection;
        theCommand.CommandText = "select pd.*,CONVERT(varchar,pd.DOB,103) as birth,cm.City_Name,sm.State_Name,dc.Charge,dc.DialysisName from DC_DialysisCharge dc, GN_PatientReg pd, GN_StateMaster sm,CityMaster cm where cm.State_ID=sm.State_ID and pd.State_Id=sm.State_ID and pd.City_Id=cm.City_ID and pd.ID=dc.ID";
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

    public string GenerateAppoID()
    {
        // Connection.
        theConnection = new SqlConnection();
        theConnection.ConnectionString = conString;

        // Command.
        theCommand = new SqlCommand();
        theCommand.Connection = theConnection;
        theCommand.CommandText = "exec sp_GenerateAppoId";
        theCommand.CommandType = CommandType.Text;

        // Adapter.
        theAdapter = new SqlDataAdapter();
        theAdapter.SelectCommand = theCommand;

        // Datatable.
        assoidtable = new DataTable();
        theAdapter.Fill(assoidtable); // Fill data into data table.

        // Clean up.
        theConnection.Dispose();
        theCommand.Dispose();
        theAdapter.Dispose();

        return assoidtable.Rows[0][0].ToString();
    }

    public string GenerateRegNo(string compcode, string yearcode)
    {
        // Connection.
        theConnection = new SqlConnection();
        theConnection.ConnectionString = conString;

        // Command.
        theCommand = new SqlCommand();
        theCommand.Connection = theConnection;
        theCommand.CommandText = "exec sp_GenerateRegNO '" + compcode + "','" + yearcode + "'";
        theCommand.CommandType = CommandType.Text;

        // Adapter.
        theAdapter = new SqlDataAdapter();
        theAdapter.SelectCommand = theCommand;

        // Datatable.
        regno = new DataTable();
        theAdapter.Fill(regno); // Fill data into data table.

        // Clean up.
        theConnection.Dispose();
        theCommand.Dispose();
        theAdapter.Dispose();

        return regno.Rows[0][0].ToString();
    }

    public string GenerateDialyserNo(string compcode, string fn, string ln, string dn)
    {
        // Connection.
        theConnection = new SqlConnection();
        theConnection.ConnectionString = conString;

        // Command.
        theCommand = new SqlCommand();
        theCommand.Connection = theConnection;
        theCommand.CommandText = "exec sp_GenerateDialyserNO '" + compcode + "','" + fn + "','" + ln + "','" + dn + "'";
        theCommand.CommandType = CommandType.Text;

        // Adapter.
        theAdapter = new SqlDataAdapter();
        theAdapter.SelectCommand = theCommand;

        // Datatable.
        diano = new DataTable();
        theAdapter.Fill(diano); // Fill data into data table.

        // Clean up.
        theConnection.Dispose();
        theCommand.Dispose();
        theAdapter.Dispose();

        return diano.Rows[0][0].ToString();
    }

    public DataTable checkavailability(int avai, string d)
    {

        DateTime date = Convert.ToDateTime(d);
        theConnection = new SqlConnection();
        theConnection.ConnectionString = conString;

        // Command.
        theCommand = new SqlCommand();
        theCommand.Connection = theConnection;
        theCommand.CommandText = "exec sp_CheckAvail " + avai + ",'" + date + "'";
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


    public DataTable GetAppointmentID(string regno)
    {

         theConnection = new SqlConnection();
        theConnection.ConnectionString = conString;

        // Command.
        theCommand = new SqlCommand();
        theCommand.Connection = theConnection;
        theCommand.CommandText = "select AppNo from DC_PatientAppointment where PatientReg='" + regno + "' and status=1";
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
        theCommand.CommandText = "select * from dbo.GN_PatientReg pr,IPD_BedMaster bm  where bm.BedNo=pr.bedallocation and pr.compcode=bm.compcode and pr.PatientReg in(select dd.PatientReg from GN_DischargeDetail dd" +
                                  "   where dd.PatientReg='" + CustID + "' and dd.compcode='"+compcode+"' and dd.DischargeDate is null) and pr.compcode='"+compcode+"'";
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


    public DataTable GetPreviousDate(string compcode,string CustID)
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
        theCommand.CommandText = "EXEC sp_DC_GetpreviousDialysis  '"+compcode+"'," + CustID + "";
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

    public DataTable GetOnlyPatient(string compcode, string CustID)
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
        theCommand.CommandText = "select * from dbo.GN_PatientReg pr  where  pr.PatientReg='" + CustID + "' and compcode='"+compcode+"'";
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

    public DataTable GetNoofDia(string compcode, string CustID)
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
        theCommand.CommandText = "select  COUNT(*) Total from dbo.DC_PatientDialysisMap map where map.PatientReg ='" + CustID + "' and compcode='"+compcode+"'";
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


    public string InsertAppointment(string compcode, string yearcode, string Startdate, string AppNo, string DiaId, string bedallocation, string time, string charge, string regno, string name, string guadian, string age, string sex, string address, string po, string ps, string dist, string pin, string state, string ph1, string ph2, string dianame, string diano, string createddate, string createdby)
    { 
        string charges="0.00"; 
        string flag = "false";
        string ServiceCharge = "";
        theConnection = new SqlConnection();
        try
        {
            if (conString != "")
            {
                theConnection.ConnectionString = conString;
                theConnection.Open();
            }

            // Command.
            theCommand = new SqlCommand();
            theCommand.Connection = theConnection; 

            //using (IDbTransaction tran = theConnection.BeginTransaction())
            //{
                try
                {
                    // transactional code...
                    using (theCommand = theConnection.CreateCommand())
                    { 
                        theCommand.CommandText = "select * from GN_PatientReg where patient_name='" + name + "'  and guardian_name='" + guadian + "' and PhNo1='" + ph1 + "' and PatientReg!='" + regno + "' and compcode='"+compcode+"'";
                        //theCommand.Transaction = tran as SqlTransaction;
                        SqlDataReader theReader = theCommand.ExecuteReader();
                        theReader.Read(); 

                        if (theReader.HasRows==true)
                        {
                            flag = "duplicate";
                        }
                        else
                        {
                            theReader.Close();
                            theCommand.CommandText = "select * from GN_PatientReg where PatientReg='" + regno + "' and compcode='"+compcode+"' ";
                           // theCommand.Transaction = tran as SqlTransaction;
                            SqlDataReader theReader1 = theCommand.ExecuteReader();
                            theReader1.Read();

                            if (theReader1.HasRows==true)
                            {
                                theReader1.Close();
                                theCommand.CommandText = "update GN_PatientReg set bedallocation='" + bedallocation + "', guardian_name='" + guadian + "', patient_name='" + name + "'," +
                                      "age='" + age + "',sex='" + sex + "',vill_city='" + address + "',po='" + po + "',ps='" + ps + "',District='" + dist + "',Pin='" + pin + "'," +
                                      "State_Id='" + state + "',PhNo1='" + ph1 + "',PhNo2='" + ph2 + "',CheckStatus=1,ID='" + dianame + "',DialyserNo='" + diano + "' where PatientReg = '" + regno + "' and compcode='"+compcode+"'";
                               // theCommand.Transaction = tran as SqlTransaction;
                                theCommand.ExecuteNonQuery(); // Execute insert query.
                               


                                theCommand.CommandText = "update GN_PatientReg_History set bedallocation='" + bedallocation + "', guardian_name='" + guadian + "', patient_name='" + name + "'," +
                                  "age='" + age + "',sex='" + sex + "',vill_city='" + address + "',po='" + po + "',ps='" + ps + "',District='" + dist + "',Pin='" + pin + "'," +
                                  "State_Id='" + state + "',PhNo1='" + ph1 + "',PhNo2='" + ph2 + "',CheckStatus=1,ID='" + dianame + "',DialyserNo='" + diano + "' where PatientReg = '" + regno + "' and compcode='" + compcode + "'";
                               // theCommand.Transaction = tran as SqlTransaction;
                                theCommand.ExecuteNonQuery(); // Execute insert query.
                                //theReader1.Close();
                            }
                            else
                            {
                                theReader1.Close();
                            
                                theCommand.CommandText = "INSERT INTO GN_PatientReg(compcode,Startdate,CheckStatus,bedallocation,PatientType,guardian_name,PatientReg,patient_name,age,sex,vill_city,po,ps,District,Pin,State_Id,PhNo1,PhNo2,ID,DialyserNo) VALUES('"+compcode+"','" + Startdate + "',1,'" + bedallocation + "','D','" + guadian + "','" + regno + "','" + name + "','" + age + "','" + sex + "','" + address + "','" + po + "','" + ps + "','" + dist + "','" + pin + "','" + state + "','" + ph1 + "','" + ph2 + "','" + dianame + "', '" + diano + "')";
                                //theCommand.Transaction = tran as SqlTransaction;
                                theCommand.ExecuteNonQuery(); // Execute insert query.

                                theCommand.CommandText = "INSERT INTO GN_PatientReg_History(compcode,Startdate,CheckStatus,bedallocation,PatientType,guardian_name,PatientReg,patient_name,age,sex,vill_city,po,ps,District,Pin,State_Id,PhNo1,PhNo2,ID,DialyserNo) VALUES('" + compcode + "','" + Startdate + "',1,'" + bedallocation + "','D','" + guadian + "','" + regno + "','" + name + "','" + age + "','" + sex + "','" + address + "','" + po + "','" + ps + "','" + dist + "','" + pin + "','" + state + "','" + ph1 + "','" + ph2 + "','" + dianame + "', '" + diano + "')";
                                //theCommand.Transaction = tran as SqlTransaction;
                                theCommand.ExecuteNonQuery(); // Execute insert query.
                            }

                            theReader1.Close();
                            
                            theCommand.CommandText = "exec sp_GenerateLedgerId '" + compcode + "',P";
                            //theCommand.Transaction = tran as SqlTransaction;
                            SqlDataReader theReader4 = theCommand.ExecuteReader();
                            theReader4.Read();
                            string ledgerid = theReader4[0].ToString();
                            theCommand.CommandText = "select Charge,ServiceCharge from DC_DialysisCharge where ID='" + DiaId + "' and compcode='"+compcode+"'";
                            //theCommand.Transaction = tran as SqlTransaction;
                            theReader4.Close();
                            SqlDataReader theReader2 = theCommand.ExecuteReader();
                            theReader2.Read();
                            charge = theReader2["Charge"].ToString();
                            //-------------------Reader4 close
                            ServiceCharge = theReader2["ServiceCharge"].ToString();
                            theReader2.Close();
                           

                            

                            //Insert into Charge Details 
                            //theReader2.Close();
                            //theReader4.Close();
                            theCommand.CommandText = "insert into DC_ChargeDetails(compcode,yearcode,PatientReg,DialysisCharge,ServiceCharge,Medicine,RequisitionCharge,OtherCharge,DoctorFees,DispsableCharge,PreviousDue,Status,LedgerId)  values('" + compcode + "','" + yearcode + "','" + regno + "','" + charge + "','" + ServiceCharge + "','" + charges + "','" + charges + "','" + charges + "','" + charges + "','" + charges + "','" + charges + "',1,'" + ledgerid + "')";
                            //theCommand.Transaction = tran as SqlTransaction;
                            theCommand.ExecuteNonQuery();
                            
                            theCommand.CommandText = "select *  from  DC_PatientAppointment  where AppNo='" + AppNo + "' and compcode='"+compcode+"'";
                           // theCommand.Transaction = tran as SqlTransaction;
                            SqlDataReader theReader3 = theCommand.ExecuteReader();
                            theReader3.Read();


                            if (theReader3.HasRows)
                            {
                                SqlCommand commd = new SqlCommand();
                                theConnection = null;
                                theConnection = new SqlConnection();
                                SqlConnection con = new SqlConnection(conString);
                                con.Open();
                                commd.Connection = con;
                                commd.CommandText = "INSERT INTO DC_PatientMonitor(YEARCODE, compcode,AppoId,PatientReg,Date1,Patient_Name,Shift,Status) VALUES('" + yearcode +"','" + compcode + "','" + AppNo + "','" + regno + "','" + Convert.ToDateTime(  theReader3["AppDate"]).ToString("MM/dd/yyyy") + "','" + theReader3["PName"] + "','" + theReader3["ShiftId"] + "',1)";
                                //theCommand.Transaction = tran as SqlTransaction;

                                commd.ExecuteNonQuery(); // Execute insert query.
                                con.Close();
                                theReader3.Close();
                            }
                            else { theReader3.Close(); }
                            // Update Patient Appointment  
                            theCommand.CommandText = "update DC_PatientAppointment set PatientReg='" + regno + "' where AppNo='" + AppNo + "' and compcode='" + compcode + "'";
                           // theCommand.Transaction = tran as SqlTransaction;
                            theCommand.ExecuteNonQuery();   // Execute update query.


                            // Insert BED_ALLOCATION 
                            theCommand.CommandText = "INSERT  INTO IPD_BedAllocation(compcode,PatientReg,BedNo,FromDate,FromTime) VALUES('" + compcode + "','" + regno + "','" + bedallocation + "','" + createddate + "','" + time + "')";
                            //theCommand.Transaction = tran as SqlTransaction;
                            theCommand.ExecuteNonQuery(); // Execute insert query.

                            // Insert current_bed
                            theCommand.CommandText = "update IPD_BedMaster set Allotted=1 where BedNo='" + bedallocation + "' and compcode='" + compcode + "'";
                            //theCommand.Transaction = tran as SqlTransaction;
                            theCommand.ExecuteNonQuery();


                            theReader4.Close();
                            theCommand.CommandText = "insert into AC_Ledger (compcode,LedgerID,LedgerFK,LedgerName,LedgerType,CreatedDate,CreatedBy,ActiveStatus)  values('" + compcode + "','" + ledgerid + "','" + regno + "','" + name + "','P','" + createddate + "','" + createdby + "',1)";
                            //theCommand.Transaction = tran as SqlTransaction;
                            theCommand.ExecuteNonQuery();

                            theCommand.CommandText = "exec sp_PopulateslmastFrmAC_ledger '" + compcode + "','" + yearcode + "','" + createddate + "','" + createdby + "','" + ledgerid + "'";
                            //theCommand.Transaction = tran as SqlTransaction;
                            theCommand.ExecuteNonQuery(); // Execute insert query.


                            theCommand.CommandText = "select pa.AdvAmount from  dbo.DC_PatientAppointment pa where  pa.AppNo='" + AppNo + "' and compcode='" + compcode + "'";
                            //theCommand.Transaction = tran as SqlTransaction;
                            SqlDataReader theReader5 = theCommand.ExecuteReader();
                            theReader5.Read();
                             string amount="empty";
                             if (theReader5 != null && theReader5.HasRows==true )
                            {
                                amount = Convert.ToString(theReader5[0]);
                            }
                            else 
                            {
                                 amount ="0";
                            }
                             theReader5.Close(); 
                            if (Convert.ToDouble(amount) != 0.00)
                            {
                                theCommand.CommandText = "exec sp_ACCOUNT_GenerateReceiptNo '" + compcode + "'";
                                //theCommand.Transaction = tran as SqlTransaction;
                                SqlDataReader theReader6 = theCommand.ExecuteReader();
                                theReader6.Read();
                                string ReceiptNo = theReader6[0].ToString();
                                theReader6.Close();

                                theCommand.CommandText = "INSERT INTO AC_Transaction(compcode,yearcode,LadgerId,Debit,Credit, TrunsactionDate,EntryBy,Status,Reason,PaymentType,ReceiptNo) VALUES ('" + compcode + "','" + yearcode + "','" + ledgerid + "',0.00,'" + amount + "', '" + createddate + "','" + createdby + "',1,'Advance Payment',5,'" + ReceiptNo + "')";
                                //theCommand.Transaction = tran as SqlTransaction;
                                theCommand.ExecuteNonQuery();
                            }

                            theCommand.CommandText = "INSERT INTO GN_DischargeDetail(compcode,yearcode,LadgerId,PatientReg,AdmissionDate,AdmissionTime) VALUES('" + compcode + "','" + yearcode + "','" + ledgerid + "','" + regno + "','" + createddate + "','" + time + "')";
                            //theCommand.Transaction = tran as SqlTransaction;
                            theCommand.ExecuteNonQuery(); // Execute insert query.   


                            theCommand.CommandText = "INSERT INTO DC_PatientDialysisMap(compcode,PatientReg,DialysisNo,DateofUse) VALUES('" + compcode + "','" + regno + "','" + diano + "','" + Startdate + "')";
                            //theCommand.Transaction = tran as SqlTransaction;
                            theCommand.ExecuteNonQuery();
                       
                        }
                        //here
                        
                       // theAdapter.Dispose();
                        theReader.Close(); 
                    }

                    flag = "true";
                    //tran.Commit();
                }
                    
                catch 
                {
                    //tran.Rollback();
                    throw;
                }
            }
            //theConnection.Close();
        //}

       
       
        catch 
        {
            return flag;
        }
        finally
        {
            //code by 
            //theAdapter.Dispose();
            //theCommand.Dispose();
            //theCommand1.Dispose();

            theConnection.Dispose();
            theCommand.Dispose();            
        }
           return flag;
    }

    public bool UpdateAppointment(string compcode, string bed, string bedno, string regno, string name, string guadian, string age, string sex, string address, string po, string ps, string dist, string pin, string state, string ph1, string ph2, string dianame, string diano)
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
            theCommand.CommandText = "update GN_PatientReg set bedallocation='" + bedno + "', guardian_name='" + guadian + "', patient_name='" + name + "',"+
                "age='" + age + "',sex='" + sex + "',vill_city='" + address + "',po='" + po + "',ps='" + ps + "',District='" + dist + "',Pin='" + pin + "',"+
                "State_Id='" + state + "',PhNo1='" + ph1 + "',PhNo2='" + ph2 + "',ID='" + dianame + "',DialyserNo='" + diano + "' where PatientReg = '" + regno + "' and compcode='"+compcode+"'";
    
                     theCommand.Transaction = tran as SqlTransaction;
            theCommand.ExecuteNonQuery(); // Execute insert query.

            // previous bed deallocate
           string cd1 = "update IPD_BedMaster set Allotted=0 where BedNo='" + bed + "' and compcode='"+compcode+"'";
            theCommand.CommandText = cd1;
       
                     theCommand.Transaction = tran as SqlTransaction;
            theCommand.ExecuteNonQuery();


            // current bed allocate
            cd1 = "update IPD_BedMaster set Allotted=1 where BedNo='" + bedno + "' and compcode='" + compcode + "'";
            theCommand.CommandText = cd1;
     
                     theCommand.Transaction = tran as SqlTransaction;
           theCommand.ExecuteNonQuery();

                 } 

                        tran.Commit();
                  
                    }
                    catch
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

    public bool DeleteAppointment(string id)
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
                        using (  theCommand = theConnection.CreateCommand())
                        {

            theCommand.CommandText = "update DC_PatientAppointment set status=0 WHERE AppNo='" + id + "'";
            theCommand.Transaction = tran as SqlTransaction;
            theCommand.ExecuteNonQuery(); // Execute insert query.
                        }

                        tran.Commit();

                    }
                    catch
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

    public DataTable GetList(string compcode,string Appono)
    {

        // Connection.
        theConnection = new SqlConnection();
        theConnection.ConnectionString = conString;

        // Command.
        theCommand = new SqlCommand();
        theCommand.Connection = theConnection;

        theCommand.CommandText = "select pa.*,CONVERT(varchar,pa.AppDate,103) AppDate1,s.ShiftName	 from DC_PatientAppointment as pa ,dbo.DC_ShiftDtls s where pa.compcode=s.compcode and pa.status=1 and s.ShiftID=pa.ShiftId and Appno='" + Appono + "' and pa.compcode='"+compcode+"'";

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