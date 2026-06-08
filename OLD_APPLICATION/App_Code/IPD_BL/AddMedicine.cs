using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using System.Data;

/// <summary>
/// Summary description for AddMedicine
/// </summary>
public class AddMedicine
{
    public AddMedicine(string con)
    {
        conString = con;
    }

    private string conString;
    private SqlConnection theConnection;
    private SqlCommand theCommand;
    private SqlDataAdapter theAdapter;
    private DataTable hospitalTable;


    public DataTable DropdownDoctor(string compcode)
    {
        // Connection.
        theConnection = new SqlConnection();
        theConnection.ConnectionString = conString;

        // Command.
        theCommand = new SqlCommand();
        theCommand.Connection = theConnection;
        theCommand.CommandText = "select * from  GN_DoctorMaster where Status=1 and compcode='"+compcode+"'";
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


    public DataTable DropdownMedGroup(string compcode)
    {
        // Connection.
        theConnection = new SqlConnection();
        theConnection.ConnectionString = conString;

        // Command.
        theCommand = new SqlCommand();
        theCommand.Connection = theConnection;
        theCommand.CommandText = "select * from  IPD_MedicineGroup  where status=1 and compcode='"+compcode+"'";
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


    public DataTable DropdownDose(string compcode)
    {
        // Connection.
        theConnection = new SqlConnection();
        theConnection.ConnectionString = conString;

        // Command.
        theCommand = new SqlCommand();
        theCommand.Connection = theConnection;
        theCommand.CommandText = "select dm.ID,dm.DoseName+' Hourly' DoseName from  MD_DoseMaster dm WHERE dm.status=1 and dm.compcode='"+compcode+"'";
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


    public DataTable DropdownDuration(string compcode)
    {
        // Connection.
        theConnection = new SqlConnection();
        theConnection.ConnectionString = conString;

        // Command.
        theCommand = new SqlCommand();
        theCommand.Connection = theConnection;
        theCommand.CommandText = "select dm.DurationId,dm.DurationName+' Days' DurationName from  IPD_DurationMaster dm WHERE dm.status=1 and compcode='"+compcode+"'";
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

    public DataTable DropDownMedicine(string compcode,string yearcode,string SubGroupid,string PatientReg)
    {
        // Connection.
        theConnection = new SqlConnection();
        theConnection.ConnectionString = conString;

        // Command.
        theCommand = new SqlCommand();
        theCommand.Connection = theConnection;

        //theCommand.CommandText = "exec sp_MD_FetchTestMedicine '"+compcode+"','"+yearcode+"','" + SubGroupid + "','" + PatientReg + "'";
        //theCommand.CommandText = "Select distinct(pm.MedicineID),mm.MedicineName from MD_PurchaseMedicineDetails pm,IPD_MedicineMaster mm where pm.compcode=mm.compcode and pm.MedicineID=mm.MedicineID and mm.MedicineSubGrp='" + SubGroupid + "' and mm.compcode='"+compcode+"' and yearcode='"+yearcode+"'";
        theCommand.CommandText = "Select distinct(mm.MedicineID),mm.MedicineName from BATDETL pm,IPD_MedicineMaster mm where pm.compcode=mm.compcode and pm.icode=dbo.Fnc_GetMedicineIcode(mm.compcode,mm.MedicineID) and mm.SubGroupid='" + SubGroupid + "' and mm.compcode='" + compcode + "' and pm.yearcode='" + yearcode + "'";
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

    public DataTable DropDownMedicineSubgroup(string compcode,string GroupID)
    {
        // Connection.
        theConnection = new SqlConnection();
        theConnection.ConnectionString = conString;

        // Command.
        theCommand = new SqlCommand();
        theCommand.Connection = theConnection;
        if (GroupID == "0")
            theCommand.CommandText = "select * from  IPD_MedicineSubGroup where Status=1 and compcode='"+compcode+"'";
        else
            theCommand.CommandText = "select * from  IPD_MedicineSubGroup where GroupID='" + GroupID + "' and Status=1 and compcode='"+compcode+"'";
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

    public DataSet GetAllPatientMedicine(string reg)
    {
        if (reg == "")
            reg = "null";
        // Connection.
        theConnection = new SqlConnection();
        theConnection.ConnectionString = conString;

        // Command.
        theCommand = new SqlCommand();
        theCommand.Connection = theConnection;

        theCommand.CommandText = "exec  sp_IPD_GetAddMedicineDetails  " + reg + "";

        theCommand.CommandType = CommandType.Text;

        // Adapter.
        theAdapter = new SqlDataAdapter();
        theAdapter.SelectCommand = theCommand;

        // Datatable.
        DataSet BedTable = new DataSet();
        theAdapter.Fill(BedTable); // Fill data into data table.

        // Clean up.
        theConnection.Dispose();
        theCommand.Dispose();
        theAdapter.Dispose();
        return BedTable;
    }


    


    public bool Insert_Syring(string Time, string PatientReg, string MedicineId, DateTime IssueDate, string AdviceBy,string qty,string compcode,string user)
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

            // transactional code...
            using (theCommand = theConnection.CreateCommand())
            {
                theCommand.CommandText = "sp_AddSyringRespectMedicine";
                theCommand.CommandType = CommandType.StoredProcedure;

                theCommand.Parameters.Add("@CompCode", compcode);
                theCommand.Parameters.Add("@PatientReg", PatientReg);
                theCommand.Parameters.Add("@IssueDate", IssueDate);
                theCommand.Parameters.Add("@AdviceBy", AdviceBy);
                theCommand.Parameters.Add("@MedicineId", MedicineId);
                theCommand.Parameters.Add("@Qty", qty);
                theCommand.Parameters.Add("@Time", Time);
                theCommand.ExecuteNonQuery();
            }
            return true;

        }
        catch
        {
            return false;
        }
        finally
        {
            theConnection.Close();
            theCommand.Dispose();
        }
    }


    public bool UpdateAddedicine(string RowID, string PatientReg, string MedicineGrpId, string MedicineId, string IssueDate, string AdviceBy, string MedicineSubGrId, string BillQty, string ActualQty, string BatchNo, string ExpirDate, string Dose, string Duration, string bedno,string compcode,string user)
    {
        string LedgerId = "";
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
                        theCommand.CommandText = "SELECT al.LedgerID FROM AC_Ledger al WHERE al.compcode='"+compcode+"' and al.LedgerFK='" + PatientReg + "' AND al.ActiveStatus=1";
                        theCommand.Transaction = tran as SqlTransaction;
                        SqlDataReader theReader = theCommand.ExecuteReader();
                        theReader.Read();
                        LedgerId = theReader[0].ToString();
                        theReader.Close();


                        theCommand.CommandText = "update IPD_PatientMedicine set LedgerId='" + LedgerId + "', Dose='" + Dose + "',Duration='" + Duration + "', MedicineGrpId='" + MedicineGrpId + "',MedicineId='" + MedicineId + "',IssueDate='" + IssueDate + "',AdviceBy='" + AdviceBy + "',MedicineSubGrId='" + MedicineSubGrId + "',BillQty='" + BillQty + "',Quantity='" + BillQty + "',ActualQty='" + ActualQty + "',BatchNo='" + BatchNo + "',ExpirDate='" + ExpirDate + "',user02='"+user+"',logdt02=GETDATE() where compcode='"+compcode+"' and RowID='" + RowID + "'";
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
            theCommand.Dispose();
            theConnection.Dispose();
        }
    }



    public bool DeleteAddmedicine(string RowID,string compcode)
    {
        int effectedRows = 0;
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
                        theCommand.CommandText = "delete from IPD_PatientMedicine where compcode='"+compcode+"' and RowID='" + RowID + "'";
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
            theCommand.Dispose();
            theConnection.Dispose();
        }
    }

    public bool InsertAddedicine(string Time, string PatientReg, string MedicineGrpId, string MedicineId, string IssueDate, string AdviceBy, string MedicineSubGrId, string BillQty, string actQty, string BatchNo, string ExpirDate, string Dose, string Duration, string bedno, string compcode,string yearcode, string user)
    {
        double RemainMedicine = 0.00;
        double NowAdd = 0.00;
        double Remain = 0.00;
        string wingid;

        string LedgerId = "";
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


                        theCommand.CommandText = "SELECT al.LedgerID FROM AC_Ledger al WHERE al.compcode='" + compcode + "' and al.LedgerFK='" + PatientReg + "' AND al.ActiveStatus=1";
                        theCommand.Transaction = tran as SqlTransaction;
                        SqlDataReader theReader2 = theCommand.ExecuteReader();
                        theReader2.Read();
                        LedgerId = theReader2[0].ToString();
                        theReader2.Close();


                        theCommand.CommandText = "EXEC sp_MD_NoOfMedicine  '" + compcode + "','"+yearcode+"','" + MedicineId + "','" + BatchNo + "'";
                        theCommand.Transaction = tran as SqlTransaction;
                        SqlDataReader theReader = theCommand.ExecuteReader();
                        theReader.Read();
                        RemainMedicine = Convert.ToDouble(theReader[0]);
                        theReader.Close();

                        theCommand.CommandText = "Select WingsID from IPD_BedMaster where compcode='" + compcode + "' and BedNoText='" + bedno + "' and status='1'";
                        theCommand.Transaction = tran as SqlTransaction;
                        SqlDataReader theReader3 = theCommand.ExecuteReader();
                        theReader3.Read();
                        wingid = theReader3[0].ToString();
                        theReader3.Close();


                        //if (RemainMedicine >= Convert.ToDouble(BillQty))
                        if (RemainMedicine >= Convert.ToDouble(actQty))
                        {
                            //theCommand.CommandText = "INSERT INTO IPD_PatientMedicine(LedgerId,Quantity,Time,PatientReg,MedicineGrpId,MedicineId,IssueDate,AdviceBy,MedicineSubGrId,BillQty,ActualQty,BatchNo,ExpirDate,Dose,Duration) VALUES('" + LedgerId + "','" + BillQty + "','" + Time + "','" + PatientReg + "','" + MedicineGrpId + "','" + MedicineId + "','" + IssueDate + "','" + AdviceBy + "','" + MedicineSubGrId + "','" + BillQty + "','" + actQty + "','" + BatchNo + "','" + ExpirDate + "','" + Dose + "','" + Duration + "')";
                            theCommand.CommandText = "INSERT INTO IPD_PatientMedicine(compcode,LedgerId,Quantity,Time,PatientReg,MedicineGrpId,MedicineId,IssueDate,AdviceBy,MedicineSubGrId,BillQty,ActualQty,BatchNo,ExpirDate,Dose,Duration,WingsId,user01,logdt01) VALUES('" + compcode + "','" + LedgerId + "','" + actQty + "','" + Time + "','" + PatientReg + "','" + MedicineGrpId + "','" + MedicineId + "','" + IssueDate + "','" + AdviceBy + "','" + MedicineSubGrId + "','" + BillQty + "','" + actQty + "','" + BatchNo + "','" + ExpirDate + "','" + Dose + "','" + Duration + "','" + wingid + "','" + user + "',GETDATE())";
                            theCommand.Transaction = tran as SqlTransaction;
                            theCommand.ExecuteNonQuery(); // Execute insert query.
                        }
                        else
                        {
                            NowAdd = RemainMedicine;
                            //Remain = Convert.ToDouble(BillQty) - RemainMedicine;
                            Remain = Convert.ToDouble(actQty) - RemainMedicine;

                            theCommand.CommandText = "INSERT INTO IPD_PatientMedicine(Compcode,LedgerId,Quantity,Time,PatientReg,MedicineGrpId,MedicineId,IssueDate,AdviceBy,MedicineSubGrId,BillQty,BatchNo,ExpirDate,Dose,Duration,ActualQty,WingsId,user01,logdt01) VALUES('" + compcode + "','" + LedgerId + "','" + NowAdd + "','" + Time + "','" + PatientReg + "','" + MedicineGrpId + "','" + MedicineId + "','" + IssueDate + "','" + AdviceBy + "','" + MedicineSubGrId + "','" + NowAdd + "','" + BatchNo + "','" + ExpirDate + "','" + Dose + "','" + Duration + "','" + actQty + "','" + wingid + "','" + user + "',GETDATE())";
                            theCommand.Transaction = tran as SqlTransaction;
                            theCommand.ExecuteNonQuery(); // Execute insert query.



                            /*theCommand.CommandText = "EXEC  sp_MD_FetchBatchNo " + MedicineId + "";
                            theCommand.Transaction = tran as SqlTransaction;
                            SqlDataReader theReader1 = theCommand.ExecuteReader();
                            theReader1.Read();*/

                            theCommand.CommandText = "INSERT INTO IPD_PatientMedicine(Compcode,LedgerId,Quantity,Time,PatientReg,MedicineGrpId,MedicineId,IssueDate,AdviceBy,MedicineSubGrId,BillQty,BatchNo,ExpirDate,Dose,Duration,ActualQty,WingsId,user01,logdt01) VALUES('" + compcode + "','" + LedgerId + "','" + Remain + "','" + Time + "','" + PatientReg + "','" + MedicineGrpId + "','" + MedicineId + "','" + IssueDate + "','" + AdviceBy + "','" + MedicineSubGrId + "','" + Remain + "','" + BatchNo + "','" + ExpirDate + "','" + Dose + "','" + Duration + "','" + actQty + "','" + wingid + "','" + user + "',GETDATE())";
                            //theReader1.Close();
                            theCommand.Transaction = tran as SqlTransaction;
                            theCommand.ExecuteNonQuery(); // Execute insert query.
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
            theConnection.Close();
            theCommand.Dispose();
        }
    }
}