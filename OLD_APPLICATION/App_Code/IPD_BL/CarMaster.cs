using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using System.Data;

/// <summary>
/// Summary description for CarMaster123
/// </summary>
public class CarMaster
{
	public CarMaster(string con)
	{
        conString = con;
	}
    public string conString;
    private SqlConnection theConnection;
    private SqlCommand theCommand;
    private SqlDataAdapter theAdapter;
    private DataTable carTable;
    public DataTable GetAllCar(string cocode)
    {
        //connection
        theConnection = new SqlConnection();
        theConnection.ConnectionString = conString;

        // Command.
        theCommand = new SqlCommand();
        theCommand.Connection = theConnection;
        theCommand.CommandText = "SELECT c.*,d.DistrictName FROM GN_CarMaster c,GN_District d where d.id=c.District and d.compcode=c.compcode and c.Compcode='" + cocode + "' order by Rowid";
        theCommand.CommandType = CommandType.Text;
        // Adapter.
        theAdapter = new SqlDataAdapter();
        theAdapter.SelectCommand = theCommand;

        // Datatable.
        carTable = new DataTable();
        theAdapter.Fill(carTable); // Fill data into data table.
        // Clean up.
        theConnection.Dispose();
        theCommand.Dispose();
        theAdapter.Dispose();
        return carTable;
      }
public DataTable DropdownID(string compcode)
    {
        // Connection.
        theConnection = new SqlConnection();
        theConnection.ConnectionString = conString;

        // Command.
        theCommand = new SqlCommand();
        theCommand.Connection = theConnection;
        theCommand.CommandText = "select * from  GN_District where status=1 and compcode='" + compcode + "'";
        theCommand.CommandType = CommandType.Text;

        // Adapter.
        theAdapter = new SqlDataAdapter();
        theAdapter.SelectCommand = theCommand;

        // Datatable.
        carTable = new DataTable();
        theAdapter.Fill(carTable); // Fill data into data table.

        // Clean up.
        theConnection.Dispose();
        theCommand.Dispose();
        theAdapter.Dispose();

        return carTable;
    }
    public bool InsertCarMaster(string CarType,string Name, string Address, string District, string Pin, string cocode, string PhNo)
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
                        theCommand.CommandText = "INSERT INTO GN_CarMaster(compcode,CarType,Name, Address1, District, Pin, PhNo1) VALUES ('"+cocode+"','" + CarType + "','" + Name + "', '" + Address + "', '" + District + "', '" + Pin + "','" + PhNo + "')";
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
  public bool UpdateCarMaster(string compcode,int rowid,string CarType,string Name, string Address, string District, string Pin,  string PhNo)
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
                        theCommand.CommandText = "Update GN_CarMaster set  CarType='" + CarType + "', Name='" + Name + "', Address1='" + Address + "',District='" + District + "' , Pin = '" + Pin + "',PhNo1='" + PhNo + "' where compcode='"+compcode+"' and rowid="+rowid+"";
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
            theConnection.Dispose();
            theCommand.Dispose();
        }
    }
  public bool DeleteCarMaster(int id,string compcode)
  {
      try
      {
          //Connection.
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
                      theCommand.CommandText = "delete from  GN_CarMaster where compcode='" + compcode + "' and rowid='" + id + "'";
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
          theConnection.Dispose();
          theCommand.Dispose();
      }
  } 
}