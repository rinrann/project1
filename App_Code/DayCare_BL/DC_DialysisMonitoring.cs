using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using System.Data;

/// <summary>
/// Summary description for DC_DialysisMonitoring12
/// </summary>
public class DC_DialysisMonitoring
{
    public DC_DialysisMonitoring(string con)
    {
        conString = con;
    }


    private string conString;
    private SqlConnection theConnection;
    private SqlCommand theCommand;
    private SqlDataAdapter theAdapter;
    private DataTable hospitalTable;

    public DataTable DropdownShift(string compcode)
    {
        // Connection.
        theConnection = new SqlConnection();
        theConnection.ConnectionString = conString;

        // Command.
        theCommand = new SqlCommand();
        theCommand.Connection = theConnection;
        theCommand.CommandText = "select s.ShiftID,s.ShiftName from DC_ShiftDtls s where s.status=1 and compcode='"+compcode+"'";
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

    public DataTable GridDetails(string compcode, string yearcode, string date1, string shift)
    {
        string sh;
        DataTable custTable;
        if (shift == "0")
            sh = "null";
        else
            sh = shift.ToString();


        // Connection.
        theConnection = new SqlConnection();
        theConnection.ConnectionString = conString;

        // Command.
        theCommand = new SqlCommand();
        theCommand.Connection = theConnection;
        if (date1 == "")
        {
            date1 = "null";
            theCommand.CommandText = "exec  sp_DC_PatientMonitor  '"+compcode+"','"+yearcode+"'," + sh + "," + Convert.ToDateTime( date1).ToString("yyyy-MM-dd")  + "";
        }
        else
            theCommand.CommandText = "exec  sp_DC_PatientMonitor  '" + compcode + "','" + yearcode + "'," + sh + ",'" + Convert.ToDateTime(date1).ToString("yyyy-MM-dd") + "'";


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


    public DataTable CountDialysis(string compcode, string ID)
    {


        // Connection.
        theConnection = new SqlConnection();
        theConnection.ConnectionString = conString;

        // Command.
        theCommand = new SqlCommand();
        theCommand.Connection = theConnection;
        theCommand.CommandText = "select COUNT(*) 	from DC_PatientMonitor pm where pm.PatientReg='" + ID + "' and compcode='"+compcode+"'";


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

    public DataTable AllMonitor(string compcode, string yearcode, string ID, string appodate)
    {

        // Connection.
        theConnection = new SqlConnection();
        theConnection.ConnectionString = conString;

        // Command.
        theCommand = new SqlCommand();
        theCommand.Connection = theConnection;
        theCommand.CommandText = "exec sp_DC_PatientDialysis '"+compcode+"','"+yearcode+"'," + ID + ",'" + Convert.ToDateTime(appodate).ToString("yyyy-MM-dd")  + "'";


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


    public DataTable getdiano(string compcode,string reg)
    {
        DataTable custTable;
        // Connection.
        theConnection = new SqlConnection();
        theConnection.ConnectionString = conString;

        // Command.
        theCommand = new SqlCommand();
        theCommand.Connection = theConnection;
        theCommand.CommandText = "select pd.DialyserNo from GN_PatientReg pd where pd.PatientReg='" + reg + "' and pd.compcode='"+compcode+"'";
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

    public DataTable getappodate(string compcode,string yearcode,string reg)
    {
        DataTable custTable;
        // Connection.
        theConnection = new SqlConnection();
        theConnection.ConnectionString = conString;

        // Command.
        theCommand = new SqlCommand();
        theCommand.Connection = theConnection;
        theCommand.CommandText = " select CONVERT(varchar,pa.AppDate,101) as getappodate from DC_PatientAppointment pa where status=1 and PatientReg='" + reg + "' and compcode='"+compcode+"' and yearcode='"+yearcode+"'";
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

    public bool UpdateAll(string compcode, string yearcode, string id, string starttime, string endtime, string regno, string diano, string apppodate, string prena, string prek, string postna, string postk, string shift, string prebp, string preweight, string prehb, string preurea, string precrt, string interbp1, string interbp2, string interbp3, string interbp4, string interbp5, string interbp6, string intert1, string intert2, string intert3, string intert4, string intert5, string intert6, string com1, string com2, string com3, string com4, string com5, string com6, string postbp, string postweight, string posthb, string posturea, string postcrt, string pulse1, string pulse2, string pulse3, string pulse4, string pulse5, string pulse6, string bf1, string bf2, string bf3, string bf4, string bf5, string bf6, string uf1, string uf2, string uf3, string uf4, string uf5, string uf6)
    {
        try
        {
            if (shift == "0")
                shift = "0";

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
                        theCommand.CommandText = "update DC_PatientMonitor set CheckStatus=1,StartTime='" + starttime + "', EndTime='" + endtime + "', DialyserNo='" + diano + "', PreNA='" + prena + "',preK='" + prek + "',PostNA='" + postna + "',PostK='" + postk + "', InterUFGoal1='" + uf1 + "',InterUFGoal2='" + uf2 + "',InterUFGoal3='" + uf3 + "',InterUFGoal4='" + uf4 + "',InterUFGoal5='" + uf5 + "',InterUFGoal6='" + uf6 + "', InterBlood1='" + bf1 + "',InterBlood2='" + bf2 + "',InterBlood3='" + bf3 + "',InterBlood4='" + bf4 + "',InterBlood5='" + bf5 + "',InterBlood6='" + bf6 + "', InterPulse1='" + pulse1 + "',InterPulse2='" + pulse2 + "',InterPulse3='" + pulse3 + "',InterPulse4='" + pulse4 + "',InterPulse5='" + pulse5 + "',InterPulse6='" + pulse6 + "',  Shift='" + shift + "', InterTime1='" + intert1 + "', InterTime2='" + intert2 + "',InterTime3='" + intert3 + "',InterTime4='" + intert4 + "',InterTime5='" + intert5 + "', InterTime6='" + intert6 + "', InterBP1='" + interbp1 + "',InterBP2='" + interbp2 + "',InterBP3='" + interbp3 + "',InterBP4='" + interbp4 + "',InterBP5='" + interbp5 + "',InterBP6='" + interbp6 + "', PreBP='" + prebp + "', PreWeight='" + preweight + "',PreHb='" + prehb + "',Preurea='" + preurea + "',Precritimine='" + precrt + "', PostBP='" + postbp + "', PostWeight='" + postweight + "',PostHb='" + posthb + "',Posturea='" + posturea + "',postcreatinine='" + postcrt + "',Comment1='" + com1 + "',Comment2='" + com2 + "',Comment3='" + com3 + "',Comment4='" + com4 + "',Comment5='" + com5 + "',Comment6='" + com6 + "' where RowID='" + id + "' and compcode='"+compcode+"' and yearcode='"+yearcode+"'";
                        theCommand.Transaction = tran as SqlTransaction;

                        theCommand.ExecuteNonQuery(); // Execute insert query.
                    } tran.Commit();
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