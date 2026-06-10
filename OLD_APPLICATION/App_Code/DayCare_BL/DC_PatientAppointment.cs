using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using System.Data;

/// <summary>
/// Summary description for DC_PatientAppointment
/// </summary>
public class DC_PatientAppointment
{
	public DC_PatientAppointment(string con)
	{
        conString = con;
	}


    private string conString;
    private SqlConnection theConnection;
    private SqlCommand theCommand;
    private SqlDataAdapter theAdapter;
    private DataTable hospitalTable;
    private DataTable assoidtable;

    public DataTable DropdownShift(string compcode)
    {
        // Connection.
        theConnection = new SqlConnection();
        theConnection.ConnectionString = conString;

        // Command.
        theCommand = new SqlCommand();
        theCommand.Connection = theConnection;
        theCommand.CommandText = "select * from  DC_ShiftDtls where status=1 and compcode='"+compcode+"'";
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

    public DataTable GridChemical(string compcode)
    {
        // Connection.
        theConnection = new SqlConnection();
        theConnection.ConnectionString = conString;

        // Command.
        theCommand = new SqlCommand();
        theCommand.Connection = theConnection;
        theCommand.CommandText = "select CONVERT(varchar,a.AppDate,103) Date1, a.*,s.ShiftName from dbo.DC_PatientAppointment a,dbo.DC_ShiftDtls s where a.compcode=s.compcode and a.ShiftId=s.ShiftID  and a.status=1 and a.compcode='"+compcode+"'";
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

    public string GenerateAppoID(string compcode)
    {
        // Connection.
        theConnection = new SqlConnection();
        theConnection.ConnectionString = conString;

        // Command.
        theCommand = new SqlCommand();
        theCommand.Connection = theConnection;
        theCommand.CommandText = "exec sp_GenerateAppoId '"+compcode+"'";
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

    public DataTable checkavailability(string compcode,int avai, string d)
    {
        //string date = Convert.ToDateTime(d).ToShortDateString();
        theConnection = new SqlConnection();
        theConnection.ConnectionString = conString;

        // Command.
        theCommand = new SqlCommand();
        theCommand.Connection = theConnection;
        theCommand.CommandText = "exec sp_CheckAvail '" + compcode + "'," + avai + ",'" + d + "'";
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

    public DataTable GetPatientDtls(string appono)
    {
        //string date = Convert.ToDateTime(d).ToShortDateString();
        theConnection = new SqlConnection();
        theConnection.ConnectionString = conString;

        // Command.
        theCommand = new SqlCommand();
        theCommand.Connection = theConnection;
        theCommand.CommandText = "select * from dbo.DC_PatientAppointment pa where pa.AppNo='" + appono + "'";
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
    
    public DataTable CheckDayoff(string compcode,string DayoffDay)
    {
        //string date = Convert.ToDateTime(d).ToShortDateString();
        if (DayoffDay == "")
            DayoffDay = "null";
        else
            DayoffDay = "'" + DayoffDay + "'";
        theConnection = new SqlConnection();
        theConnection.ConnectionString = conString;

        // Command.
        theCommand = new SqlCommand();
        theCommand.Connection = theConnection;
        theCommand.CommandText = "exec sp_DC_DayOff '"+compcode+"'," + DayoffDay + "";
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

    public DataTable GetPatientforsession(string CustID)
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
        theCommand.CommandText = "select pr.PatientReg,pr.patient_name PName,pr.vill_city PAddress,pr.PhNo1 ,pr.PhNo2,*  from GN_PatientReg   pr where pr.PatientReg='" + CustID + "'";
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
        theAdapter.Dispose();

        return custTable;
    }

    public string InsertAppointment(string compcode, string yearcode, string appoid, string reg, string name, string address, string ph1, string ph2, string appodate, string shift, string createdby, string amount, string date)
    { 
        string flag = "UnSuccessfull"; 
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
                        if (reg != "")
                        { 

                            theCommand.CommandText = "select CONVERT(VARCHAR,AppDate,103) APPODATE from DC_PatientAppointment where PatientReg='" + reg + "' and Status=1 and compcode='"+compcode+"'";
                            theCommand.Transaction = tran as SqlTransaction;
                            SqlDataReader theReader = theCommand.ExecuteReader();
                            theReader.Read();


                            if (theReader.HasRows==true)
                            {
                                flag = theReader["APPODATE"].ToString();
                                 theReader.Close();
                            }
                            else
                            {
                                theReader.Close();
                                theCommand.CommandText = "INSERT INTO DC_PatientAppointment(compcode,yearcode,AdvAmount,AppNo,PatientReg,PName,PAddress,PhNo1,PhNo2,AppDate,ShiftId,createdBy,CreatedDate,status) VALUES('" + compcode + "','" + yearcode + "','" + amount + "','" + appoid + "','" + reg + "','" + name + "','" + address + "','" + ph1 + "','" + ph2 + "','" + appodate + "','" + shift + "','" + createdby + "','" + date + "', 1)";
                                theCommand.Transaction = tran as SqlTransaction;
                                theCommand.ExecuteNonQuery(); // Execute insert query    
                                flag = "Successfull"; 
                            }
                        }
                        else
                        {
                            theCommand.CommandText = "INSERT INTO DC_PatientAppointment(compcode,yearcode,AdvAmount,AppNo,PatientReg,PName,PAddress,PhNo1,PhNo2,AppDate,ShiftId,createdBy,CreatedDate,status) VALUES('" + compcode + "','" + yearcode + "','" + amount + "','" + appoid + "','" + reg + "','" + name + "','" + address + "','" + ph1 + "','" + ph2 + "','" + appodate + "','" + shift + "','" + createdby + "','" + date + "', 1)";
                            theCommand.Transaction = tran as SqlTransaction;
                            theCommand.ExecuteNonQuery(); // Execute insert query  
                            flag = "Successfull"; 
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

        }
        catch
        {
            return flag;
        }
        finally
        {
            theConnection.Dispose();
            theCommand.Dispose();
            
        }
        return flag;
    }


    public bool UpdateAppointment(string compcode,string yearcode, string appoid, string reg, string name, string address, string ph1, string ph2, string appodate, string shift, string amount)
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
            //theCommand.CommandText = "update DC_PatientAppointment set AdvAmount='" + amount + "', PatientReg='" + reg + "', PName='" + name + "', PAddress='" + address + "',PhNo1='" + ph1 + "',PhNo2='" + ph2 + "',AppDate='" + appodate + "',ShiftId='" + shift + "' where AppNo = '" + appoid + "' and compcode='"+compcode+"'";
                            theCommand.CommandText = "update DC_PatientAppointment set PatientReg='" + reg + "', PName='" + name + "', PAddress='" + address + "',PhNo1='" + ph1 + "',PhNo2='" + ph2 + "',AppDate='" + appodate + "',ShiftId='" + shift + "', AdvAmount='" + amount + "' where AppNo = '" + appoid + "' and compcode='" + compcode + "'";
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

    public bool DeleteAppointment(string AppoId)
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
            theCommand.CommandText = "update DC_PatientAppointment set status=0 WHERE AppNo='" + AppoId + "'";
theCommand.Transaction = tran as SqlTransaction;
            theCommand.ExecuteNonQuery(); // Execute insert query.

            theCommand.CommandText = "update DC_PatientMonitor set status=0 WHERE AppoId='" + AppoId + "'";
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
}