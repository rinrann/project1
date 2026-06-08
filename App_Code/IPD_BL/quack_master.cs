using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;

/// <summary>
/// Summary description for quack_master
/// </summary>
public class quack_master
{
        //
		// TODO: Add constructor logic here
		//

        public quack_master ( string con)
        {

            conString = con;
        }

        private string conString;
        private SqlConnection theConnection;
        private SqlCommand theCommand;
        private SqlDataAdapter theAdapter;
        private DataTable quacktable;

        public DataTable GetAllData(string cocode)
        {
            // Connection.
            theConnection = new SqlConnection();
            theConnection.ConnectionString = conString;

            // Command.
            theCommand = new SqlCommand();
            theCommand.Connection = theConnection;
            theCommand.CommandText = "select * from GN_QuackMaster where status='1' and Compcode='" + cocode + "'";
            theCommand.CommandType = CommandType.Text;

            // Adapter.
            theAdapter = new SqlDataAdapter();
            theAdapter.SelectCommand = theCommand;

            // Datatable.
            quacktable = new DataTable();
            theAdapter.Fill(quacktable); // Fill data into data table.

            // Clean up.
            theConnection.Dispose();
            theCommand.Dispose();
            theAdapter.Dispose();

            return quacktable;
        }
        public DataTable GenerateQuackID(string cocode)
        {
            // Connection.
            theConnection = new SqlConnection();
            theConnection.ConnectionString = conString;

            // Command.
            theCommand = new SqlCommand();
            theCommand.Connection = theConnection;
            theCommand.CommandText = "exec sp_IPD_GenerateQuackID "+cocode+"";
            theCommand.CommandType = CommandType.Text;

            // Adapter.
            theAdapter = new SqlDataAdapter();
            theAdapter.SelectCommand = theCommand;

            // Datatable.
            quacktable = new DataTable();
            theAdapter.Fill(quacktable); // Fill data into data table.

            // Clean up.
            theConnection.Dispose();
            theCommand.Dispose();
            theAdapter.Dispose();

            return quacktable;
        }
        public bool InsertQuack(string id, string quack_type, string name, string Address, string Country, string state, string city, string Pin, string mobile, string ph, string EmailId, string Fax, string commpercent, string Commission_Rs, string user, string craeteDate, string cocode, string yearcode)
        {
            if (commpercent == "")
                commpercent = "0";
            if (Commission_Rs == "")
                Commission_Rs = "0";
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
                         theCommand.CommandText = "exec sp_GenerateLedgerId '" + cocode + "','Q'";
                         theCommand.Transaction = tran as SqlTransaction;
                         SqlDataReader theReader1 = theCommand.ExecuteReader();
                         theReader1.Read();
                         string ledgerid = theReader1[0].ToString();
                         theReader1.Close();

                         theCommand.CommandText = "insert into AC_Ledger(compcode,LedgerID,LedgerFK,LedgerName,LedgerType,CreatedDate,CreatedBy,ActiveStatus)  values('" + cocode + "','" + ledgerid + "','" + id + "','" + name + "','Q',getdate(),'" + user + "',1)";
                         theCommand.Transaction = tran as SqlTransaction;
                         theCommand.ExecuteNonQuery();

                         theCommand.CommandText = "insert into dbo.GN_QuackMaster(compcode,QuackId,QuackName,Address1,Address2,StateId,pin,PhNo1,PhNo2,EmailId,Type,Fax,Commission_Per,Commission_Rs,LedgerID,status)values('" + cocode + "','" + id + "','" + name + "','" + Address + "','" + city + "','" + state + "','" + Pin + "','" + mobile + "','" + ph + "','" + EmailId + "','" + quack_type + "','" + Fax + "','" + commpercent + "','" + Commission_Rs + "','" + ledgerid + "','1')";
                         theCommand.Transaction = tran as SqlTransaction;
                         theCommand.ExecuteNonQuery();

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
        public bool UpdateQuack(string id, string quack_type, string name, string Address, string Country, string state, string city, string Pin, string mobile, string ph, string EmailId, string Fax, string commpercent, string Commission_Rs, string user, string craeteDate, string cocode)
        {
            if (commpercent == "")
                commpercent = "0";
            if (Commission_Rs == "")
                Commission_Rs = "0";
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
                            theCommand.CommandText = "update dbo.GN_QuackMaster set QuackName='" + name + "',Address1='" + Address + "',Address2='" + city + "',StateId='" + state + "',pin='" + Pin + "',PhNo1='" + mobile + "',PhNo2='" + ph + "',EmailId='" + EmailId + "',Type='" + quack_type + "',Fax='" + Fax + "',Commission_Per='" + commpercent + "',Commission_Rs='" + Commission_Rs + "' where QuackId='" + id + "' and compcode='" + cocode + "'";
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
        public bool DeleteQuack(string id, string cocode)
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
                            theCommand.CommandText = "DELETE FROM GN_QuackMaster WHERE QuackId='" + id + "' and compcode='" + cocode + "'";
                            theCommand.Transaction = tran as SqlTransaction;
                            theCommand.ExecuteNonQuery(); // Execute delete query.

                            theCommand.CommandText = "DELETE FROM AC_Ledger WHERE LedgerFK='" + id + "' and compcode='" + cocode + "' and LedgerType='Q'";
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
