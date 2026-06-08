using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using System.Data;

/// <summary>
/// Summary description for doc_master12
/// </summary>
public class doc_master
{
    public doc_master(string con)
    {
        conString = con;
    }


    private string conString;
    private SqlConnection theConnection;
    private SqlCommand theCommand;
    private SqlDataAdapter theAdapter;
    private DataTable hospitalTable;

    public DataTable GetAllDoctor(string cocode)
    {
        // Connection.
        theConnection = new SqlConnection();
        theConnection.ConnectionString = conString;

        // Command.
        theCommand = new SqlCommand();
        theCommand.Connection = theConnection;
        theCommand.CommandText = "select *,'Dr. '+doc_name dname from dbo.GN_DoctorMaster where Status=1 and compcode='" + cocode + "' ";
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

    public DataTable GetDoctorType(string compcode)
    {
        // Connection.
        theConnection = new SqlConnection();
        theConnection.ConnectionString = conString;

        // Command.
        theCommand = new SqlCommand();
        theCommand.Connection = theConnection;
        theCommand.CommandText = "select * from GN_DoctorType where compcode='"+ compcode +"'";
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

    public DataTable GetDoctorSpeciality()
    {
        // Connection.
        theConnection = new SqlConnection();
        theConnection.ConnectionString = conString;

        // Command.
        theCommand = new SqlCommand();
        theCommand.Connection = theConnection;
        theCommand.CommandText = "select * from GN_DoctorSpecialty where Status=1";
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

    public DataTable GetState(string compcode)
    {
        // Connection.
        theConnection = new SqlConnection();
        theConnection.ConnectionString = conString;

        // Command.
        theCommand = new SqlCommand();
        theCommand.Connection = theConnection;
        theCommand.CommandText = "select * from GN_StateMaster where compcode='" + compcode + "'";
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

    public DataTable GetCountryState(string compcode,string country)
    {
        // Connection.
        theConnection = new SqlConnection();
        theConnection.ConnectionString = conString;

        // Command.
        theCommand = new SqlCommand();
        theCommand.Connection = theConnection;
        theCommand.CommandText = "select * from GN_StateMaster where compcode='" + compcode + "' and countryCode='"+ country +"'";
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

    public DataTable GetCountry( string compcode)
    {
        // Connection.
        theConnection = new SqlConnection();
        theConnection.ConnectionString = conString;

        // Command.
        theCommand = new SqlCommand();
        theCommand.Connection = theConnection;
        theCommand.CommandText = "select * from GN_Country  where compcode='" + compcode + "'";
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

    public DataTable GenerateDocID(string compcode)
    {
        // Connection.
        theConnection = new SqlConnection();
        theConnection.ConnectionString = conString;

        // Command.
        theCommand = new SqlCommand();
        theCommand.Connection = theConnection;
        theCommand.CommandText = "exec sp_IPD_GenerateDoctorID '"+compcode+"'";
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

    public DataTable BindGrid(string DoctorID)
    {
        // Connection.
        theConnection = new SqlConnection();
        theConnection.ConnectionString = conString;

        // Command.
        theCommand = new SqlCommand();
        theCommand.Connection = theConnection;
        theCommand.CommandText = "select * from dbo.GN_DoctorVisit where DocId = '" + DoctorID + "'";
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

    public bool InsertDOCMASTER(string visit_charges, string DrRegNo, string DoctorFees, string docid, string doctype, string doc_name, string Address, string Country, string state, string city, string Pin, string qualification, string mono, string resno, string email, string Fax, string Commission_Per, string Commission_Rs, string user, string craeteDate, string cocode, string yearcode, string nightfees, string nightvisit, string DeptCode, string consultant)
    {
        if (Commission_Per == "")
            Commission_Per = "0";
        if (Commission_Rs == "")
            Commission_Rs = "0";
        if (DoctorFees == "")
            DoctorFees = "0";

        if (visit_charges == "")
            visit_charges = "0";

        if (nightfees == "")
            nightfees = "0";

        if (nightvisit == "")
            nightvisit = "0";
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
            theCommand = new SqlCommand();                                                                                                                                                                                                        //,string , string , string ,string ,string city,string Pin,string qualification,string mono,string resno,string email, string Fax,string SpecialistIn1,string SpecialistIn2,string SpecialistIn3,string Commission_Per, string Commission_Rs)
            theCommand.Connection = theConnection;
            using (IDbTransaction tran = theConnection.BeginTransaction())
            {
                try
                {
                    // transactional code...
                    using (theCommand = theConnection.CreateCommand())
                    {
                        theCommand.CommandText = "exec sp_GenerateLedgerId '" + cocode + "','D'";
                        theCommand.Transaction = tran as SqlTransaction;
                        SqlDataReader theReader1 = theCommand.ExecuteReader();
                        theReader1.Read();
                        string ledgerid = theReader1[0].ToString();
                        theReader1.Close();

                        theCommand.CommandText = "insert into AC_Ledger(COMPCODE,LedgerID,LedgerFK,LedgerName,LedgerType,CreatedDate,CreatedBy,ActiveStatus)  values('" + cocode + "','" + ledgerid + "','" + docid + "','" + doc_name + "','D',getdate(),'" + user + "',1)";
                        theCommand.Transaction = tran as SqlTransaction;
                        theCommand.ExecuteNonQuery();

                        theCommand.CommandText = "INSERT INTO GN_DoctorMaster(COMPCODE,visit_charges,DoctorFees,DrRegNo,Status,doc_id,DocTypeId,doc_name,Address,Country,Stateid,City,Pin,Qualification,Phone,doc_ph_res,EmailId,Fax,Commission_Per,Commission_Rs,LedgerID,DoctorFeesNight,visit_night,DeptCode,consultant) VALUES('" + cocode + "','" + visit_charges + "','" + DoctorFees + "','" + DrRegNo + "',1,'" + docid + "','" + doctype + "','" + doc_name + "','" + Address + "','" + Country + "','" + state + "','" + city + "','" + Pin + "', '" + qualification + "','" + mono + "', '" + resno + "', '" + email + "', '" + Fax + "','" + Commission_Per + "', '" + Commission_Rs + "','" + ledgerid + "','" + nightfees + "','" + nightvisit + "','" + DeptCode + "','" + consultant + "')";
                        theCommand.Transaction = tran as SqlTransaction;
                        theCommand.ExecuteNonQuery(); // Execute insert query.

                        theCommand.CommandText = "exec sp_PopulateslmastFrmAC_ledger '" + cocode + "','" + yearcode + "','" + craeteDate + "','" + user + "','" + ledgerid + "'";
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

    public bool UpdateDM(string docid)
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
            theCommand = new SqlCommand();                                                                                                                                                                                                        //,string , string , string ,string ,string city,string Pin,string qualification,string mono,string resno,string email, string Fax,string SpecialistIn1,string SpecialistIn2,string SpecialistIn3,string Commission_Per, string Commission_Rs)
            theCommand.Connection = theConnection;
            using (IDbTransaction tran = theConnection.BeginTransaction())
            {
                try
                {
                    // transactional code...
                    using (theCommand = theConnection.CreateCommand())
                    {
                        theCommand.CommandText = "update GN_DoctorMaster  set Status=0 where docid='" + docid + "'";
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


    public bool InsertDocVisit(string DocId, string DayId, string InTime, string OutTime,string cocode)
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
                        theCommand.CommandText = "INSERT INTO GN_DoctorVisit(compcode,DocId,DayId,InTime,OutTime,Status) VALUES('" + cocode + "','" + DocId + "'," + DayId + ",'" + InTime + "','" + OutTime + "',1)";
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

    public bool DeleteDocVisit(string DocId)
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
                        theCommand.CommandText = "delete from GN_DoctorVisit where DocId='" + DocId + "'";
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

    public bool UpdateDOCMASTER(string visit_charges, string DrRegNo, string DoctorFees, string diocid, string doctype, string doc_name, string Address, string Country, string state, string city, string Pin, string qualification, string mono, string resno, string email, string Fax, string Commission_Per, string Commission_Rs, string cocode, string nightfees, string nightvisit, string DeptCode, string username, string consultant)
    {
        if (DoctorFees == "")
            DoctorFees = "0";

        if (visit_charges == "")
            visit_charges = "0";

        if (Commission_Per == "")
            Commission_Per = "0";
        if (Commission_Rs == "")
            Commission_Rs = "0";

        if (visit_charges == "")
            visit_charges = "0";

        if (nightfees == "")
        {
            nightfees = "0";
        }

        if (nightvisit == "")
        {
            nightvisit = "0";
        }
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
                        theCommand.CommandText = "update GN_DoctorMaster set visit_charges='" + visit_charges + "',DoctorFees='" + DoctorFees + "', DrRegNo='" + DrRegNo + "',DocTypeId='" + doctype + "',doc_name='" + doc_name + "',Address='" + Address + "',Country='" + Country + "',Stateid='" + state + "',City='" + city + "',Qualification='" + qualification + "',Phone='" + mono + "',doc_ph_res='" + resno + "',EmailId='" + email + "',Fax='" + Fax + "',Commission_Per='" + Commission_Per + "',Commission_Rs='" + Commission_Rs + "',DoctorFeesNight='" + nightfees + "',visit_night='" + nightvisit + "',DeptCode='" + DeptCode + "',user02='" + username + "',logdt02=getdate(),consultant='" + consultant + "'  where doc_id='" + diocid + "' and compcode='" + cocode + "'";
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

    public bool DeleteDOC_MASTER(string doc_id,string cocode)
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
                        theCommand.CommandText = "DELETE FROM GN_DoctorMaster WHERE doc_id='" + doc_id + "' and compcode='" + cocode + "'";
                        theCommand.Transaction = tran as SqlTransaction;
                        theCommand.ExecuteNonQuery(); // Execute insert query.

                        theCommand.CommandText = "DELETE FROM AC_Ledger WHERE LedgerFK='" + doc_id + "' and compcode='" + cocode + "' and LedgerType='D'";
                        theCommand.Transaction = tran as SqlTransaction;
                        theCommand.ExecuteNonQuery(); // Execute delete query.
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
}