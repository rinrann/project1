using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using System.Data;

/// <summary>
/// Summary description for AddServices
/// </summary>
public class AddServices
{
    public AddServices(string con)
    {
        conString = con;
    }

    private string conString;
    private SqlConnection theConnection;
    private SqlCommand theCommand;
    private SqlDataAdapter theAdapter;
    private DataTable BedTable;

    public DataSet GetAllAddServices(string compcode,string reg)
    {
        if (reg == "")
            reg = "null";
        // Connection.
        theConnection = new SqlConnection();
        theConnection.ConnectionString = conString;

        // Command.
        theCommand = new SqlCommand();
        theCommand.Connection = theConnection;

        theCommand.CommandText = "exec  sp_IPD_GetAddServiceDetails  '"+compcode+"','" + reg + "'"; 

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


   
    public DataTable DropdownServiceCategory()
    {
        // Connection.
        theConnection = new SqlConnection();
        theConnection.ConnectionString = conString;

        // Command.
        theCommand = new SqlCommand();
        theCommand.Connection = theConnection;
        theCommand.CommandText = "select * from IPD_ServiceCategory where status=1";
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

    public DataTable DropdownDOCTORTYPE()
    {
        // Connection.
        theConnection = new SqlConnection();
        theConnection.ConnectionString = conString;

        // Command.
        theCommand = new SqlCommand();
        theCommand.Connection = theConnection;
        theCommand.CommandText = "select * from GN_DoctorType where Status=1";
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
            theCommand.CommandText = "select dm.doc_id ,dm.doc_name From GN_DoctorMaster dm where dm.DocTypeId='" + cat + "'and dm.Status=1";
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

    public bool InsertAddservice(string Time, string PatientReg, string ServiceId, string Quantity, string Price, string IssueDate, string DoctorType, string DoctorId, string AddDoctorType, string AddDoctorId, string compcode, string user, string servcont)
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

                        theCommand.CommandText = "INSERT INTO IPD_AddServices(compcode,Status,LedgerId,Time,PatientReg,ServiceId,Quantity,Price,IssueDate,DoctorType,DoctorId,AddDoctorType,AddDoctorId,ServCont,user01,logdt01) VALUES ('" + compcode + "',1,'" + LedgerId + "','" + Time + "','" + PatientReg + "','" + ServiceId + "','" + Quantity + "','" + Price + "', '" + IssueDate + "', '" + DoctorType + "','" + DoctorId + "', '" + AddDoctorType + "','" + AddDoctorId + "','" + servcont + "','" + user + "',GETDATE())";
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
            theConnection.Close();
            theCommand.Dispose();
        }
    }



    public bool Update_Delete_Addservice(int mode,string patientreg,string RowId, string ServiceId, string Quantity, string Price, string IssueDate,string servcont,string compcode,string user)
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
                        if (mode == 2)
                        {
                            theCommand.CommandText = "update  IPD_AddServices set ServiceId='" + ServiceId + "',Quantity='" + Quantity + "',Price='" + Price + "',IssueDate='" + IssueDate + "',ServCont='" + servcont + "',user02='" + user + "',logdt02=GETDATE()  where compcode='" + compcode + "' and RowId='" + RowId + "'";
                            theCommand.Transaction = tran as SqlTransaction;
                            theCommand.ExecuteNonQuery(); // Execute insert query.
                        }
                        else
                        {

                            theCommand.CommandText = "delete  IPD_AddServices    where compcode='" + compcode + "' and RowId='" + RowId + "' and IssueDate='" + IssueDate + "'";

                            theCommand.Transaction = tran as SqlTransaction;
                            theCommand.ExecuteNonQuery(); 

                        }
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
            theConnection.Close();
            theCommand.Dispose();
        }
    }

    public bool Delete_PatientConsumables(string patientreg, string conscatId,string consitemId,string compcode,string issuedate)
    {
        try
        {
            theConnection = new SqlConnection();
            if (conString != "")
            {
                theConnection.ConnectionString = conString;
                theConnection.Open();
            }

            theCommand=new SqlCommand();
            theCommand.Connection=theConnection;

            using (IDbTransaction tran = theConnection.BeginTransaction())
            {
                try
                {
                    using (theCommand = theConnection.CreateCommand())
                    {
                        theCommand.CommandText = "Delete from IPD_PatientConsumables where compcode='" + compcode + "' and patientreg='" + patientreg + "' and ConsumableGrpId='" + conscatId + "' and ConsumableItemIdId='" + consitemId + "' and IssueDate='" + issuedate + "'";
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
            theConnection.Close();
            theCommand.Dispose();
        }
    }
}