using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using System.Data;

/// <summary>
/// Summary description for AddConsumable
/// </summary>
public class AddConsumable
{
    public AddConsumable(string con)
    {
        conString = con;
    }

    private string conString;
    private SqlConnection theConnection;
    private SqlCommand theCommand;
    private SqlDataAdapter theAdapter;
    private DataTable BedTable;

    public DataSet GetAllPatientConsumable(string compcode,string reg)
    {
        if (reg == "")
            reg = "null";
        // Connection.
        theConnection = new SqlConnection();
        theConnection.ConnectionString = conString;

        // Command.
        theCommand = new SqlCommand();
        theCommand.Connection = theConnection;

        theCommand.CommandText = "exec  sp_IPD_GetAddConsumableDetails   '"+compcode+"','" + reg + "'";
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


    public DataTable GridFillFromService(string compcode,string regno)
    {
        if (regno == "")
            regno = "null";

        // Connection.
        theConnection = new SqlConnection();
        theConnection.ConnectionString = conString;

        // Command.
        theCommand = new SqlCommand();
        theCommand.Connection = theConnection;
        theCommand.CommandText = "EXEC sp_IPD_GetConsumableAccordingService '"+compcode+"','" + regno + "'";
        theCommand.CommandType = CommandType.Text;

        // Adapter.
        theAdapter = new SqlDataAdapter();
        theAdapter.SelectCommand = theCommand;

        // Datatable.
        BedTable = new DataTable();
        theAdapter.Fill(BedTable); // Fill data into data table.

        // Clean up.
        theConnection.Dispose();
        theCommand.Dispose();
        theAdapter.Dispose();

        return BedTable;
    }



    public DataTable DropdownConGroup()
    {
        // Connection.
        theConnection = new SqlConnection();
        theConnection.ConnectionString = conString;

        // Command.
        theCommand = new SqlCommand();
        theCommand.Connection = theConnection;
        theCommand.CommandText = "select * from IPD_ConsumableGroup where status=1";
        theCommand.CommandType = CommandType.Text;

        // Adapter.
        theAdapter = new SqlDataAdapter();
        theAdapter.SelectCommand = theCommand;

        // Datatable.
        BedTable = new DataTable();
        theAdapter.Fill(BedTable); // Fill data into data table.

        // Clean up.
        theConnection.Dispose();
        theCommand.Dispose();
        theAdapter.Dispose();

        return BedTable;
    }

    public DataTable DropdownDOCTORTYPE(string compcode)
    {
        // Connection.
        theConnection = new SqlConnection();
        theConnection.ConnectionString = conString;

        // Command.
        theCommand = new SqlCommand();
        theCommand.Connection = theConnection;
        theCommand.CommandText = "select * from GN_DoctorType where compcode='"+compcode+"' and status=1";
        theCommand.CommandType = CommandType.Text;

        // Adapter.
        theAdapter = new SqlDataAdapter();
        theAdapter.SelectCommand = theCommand;

        // Datatable.
        BedTable = new DataTable();
        theAdapter.Fill(BedTable); // Fill data into data table.

        // Clean up.
        theConnection.Dispose();
        theCommand.Dispose();
        theAdapter.Dispose();

        return BedTable;
    }


    public DataTable DropdownDoctor(string cat)
    {
        // Connection.
        theConnection = new SqlConnection();
        theConnection.ConnectionString = conString;

        // Command.
        theCommand = new SqlCommand();
        theCommand.Connection = theConnection;
        //select dm.doc_id,dm.doc_name from dbo.GN_DoctorMaster dm,dbo.IPD_PatientDoctorVisit pd where dm.doc_id=pd.Docid and pd.PatientReg='"+reg+"' group by dm.doc_id,dm.doc_name
        if (cat == "" || cat == "0")
            theCommand.CommandText = "select dm.doc_id ,dm.doc_name From GN_DoctorMaster dm where Status=1";
        else
            theCommand.CommandText = "select dm.doc_id ,dm.doc_name From GN_DoctorMaster dm where dm.DocTypeId='" + cat + "' and Status=1 ";
        theCommand.CommandType = CommandType.Text;

        // Adapter.
        theAdapter = new SqlDataAdapter();
        theAdapter.SelectCommand = theCommand;

        // Datatable.
        BedTable = new DataTable();
        theAdapter.Fill(BedTable); // Fill data into data table.

        // Clean up.
        theConnection.Dispose();
        theCommand.Dispose();
        theAdapter.Dispose();

        return BedTable;
    }


    public DataTable DropdownConItem(string compcode,string ConGrId)
    {
        // Connection.
        theConnection = new SqlConnection();
        theConnection.ConnectionString = conString;

        // Command.
        theCommand = new SqlCommand();
        theCommand.Connection = theConnection;

        if (ConGrId == "0")
            theCommand.CommandText = "select * from IPD_ConsumableItems where status=1 and compcode='"+compcode+"'";
        else
            theCommand.CommandText = "select * from IPD_ConsumableItems where ConGrId='" + ConGrId + "' and status=1 and compcode='"+compcode+"'";

        theCommand.CommandType = CommandType.Text;

        // Adapter.
        theAdapter = new SqlDataAdapter();
        theAdapter.SelectCommand = theCommand;

        // Datatable.
        BedTable = new DataTable();
        theAdapter.Fill(BedTable); // Fill data into data table.

        // Clean up.
        theConnection.Dispose();
        theCommand.Dispose();
        theAdapter.Dispose();

        return BedTable;
    }


    public bool InsertConsumable(string Time, string PatientReg, string ConsumableGrpId, string ConsumableItemIdId, string IssueDate, string ActualQty, string BillQty, string DoctypeID, string AdviceBy, string AddDoctypeID, string AddAdviceby,string compcode,string user)
    {
        int effectedRows = 0;

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


                        theCommand.CommandText = "INSERT INTO IPD_PatientConsumables(compcode,LedgerId,Time,PatientReg,ConsumableGrpId,ConsumableItemIdId, IssueDate,ActualQty,BillQty,DoctypeID,AdviceBy,AddDoctypeID,AddAdviceby,user01,logdt01) VALUES ('"+compcode+"','" + LedgerId + "','" + Time + "','" + PatientReg + "','" + ConsumableGrpId + "', '" + ConsumableItemIdId + "', '" + IssueDate + "','" + ActualQty + "', '" + BillQty + "','" + DoctypeID + "','" + AdviceBy + "','" + AddDoctypeID + "','" + AddAdviceby + "','"+user+"',GETDATE())";
                        theCommand.Transaction = tran as SqlTransaction;
                        effectedRows = theCommand.ExecuteNonQuery(); // Execute insert query. 

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




    public bool Update_Service_Status(string PatientReg)
    {
        int effectedRows = 0;

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

                        theCommand.CommandText = "SELECT al.LedgerID FROM AC_Ledger al WHERE al.LedgerFK='" + PatientReg + "' AND al.ActiveStatus=1";
                        theCommand.Transaction = tran as SqlTransaction;
                        SqlDataReader theReader = theCommand.ExecuteReader();
                        theReader.Read();
                        LedgerId = theReader[0].ToString();
                        theReader.Close();


                        theCommand.CommandText = "UPDATE  IPD_AddServices  SET Status=0  where LedgerId='" + LedgerId + "'";
                        theCommand.Transaction = tran as SqlTransaction;
                        effectedRows = theCommand.ExecuteNonQuery(); // Execute insert query. 

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

    public bool Update_Delete_DocVisit(int mode, string id, string ConsumableGrpId, string ConsumableItemIdId, string IssueDate, string ActualQty, string BillQty, string DoctypeID, string AdviceBy, string AddDoctypeID, string AddAdviceby,string compcode,string user)
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
                        if(mode==1)
                            theCommand.CommandText = "Update IPD_PatientConsumables set ConsumableGrpId='" + ConsumableGrpId + "',ConsumableItemIdId='" + ConsumableItemIdId + "',IssueDate='" + IssueDate + "', ActualQty='" + ActualQty + "', BillQty='" + BillQty + "',DoctypeID='" + DoctypeID + "',AdviceBy='" + AdviceBy + "',AddDoctypeID='" + AddDoctypeID + "',AddAdviceby='" + AddAdviceby + "',user02='"+user+"',logdt02=GETDATE()  where compcode='"+compcode+"' and RowID = '" + id + "'";
                        else
                            theCommand.CommandText = "delete IPD_PatientConsumables where compcode='"+compcode+"' and RowID = '" + id + "'";
                        theCommand.Transaction = tran as SqlTransaction;
                        theCommand.ExecuteNonQuery(); // Execute update query.

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
}