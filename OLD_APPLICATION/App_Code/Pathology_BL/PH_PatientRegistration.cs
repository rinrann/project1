using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using System.Data;

/// <summary>
/// Summary description for PH_PatientRegistration123
/// </summary>
public class PH_PatientRegistration
{
	public PH_PatientRegistration(string con)
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

    public DataTable GenerateRegNo(String compcode,string yearcode)
    {
        DataTable custTable;
        //if (fromdate == "")
        //    fromdate = "null";
        //if (todate == "")
        //    todate = "null";
        // Connection.
        theConnection = new SqlConnection();
        theConnection.ConnectionString = conString;

        // Command.
        theCommand = new SqlCommand();
        theCommand.Connection = theConnection;
        theCommand.CommandText = "exec sp_GenerateRegNO '"+compcode+"','"+yearcode+"'";
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
        

        return custTable;
    }
    public DataTable DropdownState(String compcode)
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
        

        return hospitalTable;
    }

    public DataTable DropdownDender(string compcode)
    {
        // Connection.
        theConnection = new SqlConnection();
        theConnection.ConnectionString = conString;

        // Command.
        theCommand = new SqlCommand();
        theCommand.Connection = theConnection;
        theCommand.CommandText = "select * from GN_Gender where compcode='"+compcode+"'";
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
        

        return hospitalTable;
    }

    public DataTable DropdownCity(int id)
    {
        // Connection.
        theConnection = new SqlConnection();
        theConnection.ConnectionString = conString;

        // Command.
        theCommand = new SqlCommand();
        theCommand.Connection = theConnection;
        theCommand.CommandText = "select * from CityMaster where Status=1 and State_ID=" + id + "";
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
        

        return hospitalTable;
    }

    public DataTable GetAdv(string id)
    {
        // Connection.
        theConnection = new SqlConnection();
        theConnection.ConnectionString = conString;

        // Command.
        theCommand = new SqlCommand();
        theCommand.Connection = theConnection;
        theCommand.CommandText = "select AdAmt from dbo.PH_PatientReq where RequisitionNo='" + id + "'";
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
        

        return hospitalTable;
    }

    public DataTable GetCost(string id)
    {
        // Connection.
        theConnection = new SqlConnection();
        theConnection.ConnectionString = conString;

        // Command.
        theCommand = new SqlCommand();
        theCommand.Connection = theConnection;
        theCommand.CommandText = "select sum(Cost) from PH_TestMaster tm,PH_PatientReq pr,dbo.PH_RequisitionTestMap map where tm.TestId=map.TestCode and pr.RequisitionNo=map.RequisitionID and pr.RequisitionNo='" + id + "' ";
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
        

        return hospitalTable;
    }
    public DataTable DropdownDialyserCharge(int id)
    {
        // Connection.
        theConnection = new SqlConnection();
        theConnection.ConnectionString = conString;

        // Command.
        theCommand = new SqlCommand();
        theCommand.Connection = theConnection;
        theCommand.CommandText = "select * from dbo.DC_DialysisCharge where Status=1 and ID=" + id + " ";
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
        

        return assoidtable.Rows[0][0].ToString();
    }

    public string GenerateDialyserNo(string fn, string ln, string dn)
    {
        // Connection.
        theConnection = new SqlConnection();
        theConnection.ConnectionString = conString;

        // Command.
        theCommand = new SqlCommand();
        theCommand.Connection = theConnection;
        theCommand.CommandText = "exec sp_GenerateDialyserNO " + fn + "," + ln + "," + dn + "";
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
        

        return (hospitalTable);

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
        theCommand.CommandText = "exec sp_PatientDetails '" + compcode + "','" + CustID + "',null,null,null";
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
        

        return custTable;
    }
    public DataTable GetNoofDia(string CustID)
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
        theCommand.CommandText = "select pm.DialyserNo, COUNT(pm.DialyserNo) as DiaNo from DC_PatientMonitor pm where pm.PatientReg='" + CustID + "'  group by pm.DialyserNo";
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
        

        return custTable;
    }

    public DataTable Dropdowndistrict(string compcode)
    {
        DataTable custTable;


        // Connection.
        theConnection = new SqlConnection();
        theConnection.ConnectionString = conString;

        // Command.
        theCommand = new SqlCommand();
        theCommand.Connection = theConnection;
        theCommand.CommandText = "select * from GN_District where compcode='"+compcode+"'";
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
        

        return custTable;
    }

    public bool InsertRegistration(string compcode,string regno, string name, string age, string sex, string address, string address2, string po, string ps, string dist, string pin, string state, string ph1, string ph2,string user)
    {
        try
        {
            theConnection = new SqlConnection();
            if (conString != "")
            {
                theConnection.ConnectionString = conString;
                theConnection.Open();
            }

            // Command.
            theCommand = new SqlCommand();
            theCommand1 = new SqlCommand();
            // string s = Session["userName"];
            theCommand.Connection = theConnection;

            theCommand.CommandText = "exec sp_GenerateLedgerId '" + compcode + "','P'";
            //theCommand.Transaction = tran as SqlTransaction;
            SqlDataReader theReader1 = theCommand.ExecuteReader();
            theReader1.Read();
            string ledgerid = theReader1[0].ToString();
            theReader1.Close();

            theCommand.CommandText = "insert into AC_Ledger(compcode,LedgerID,LedgerFK,LedgerName,LedgerType,CreatedDate,CreatedBy,ActiveStatus)  values('" + compcode + "','" + ledgerid + "','" + regno + "','" + name + "','P',getdate(),'" + user + "',1)";
            //theCommand.Transaction = tran as SqlTransaction;
            theCommand.CommandType = CommandType.Text;
            theCommand.Connection = theConnection;
            theCommand.ExecuteNonQuery();

            theCommand.CommandText = "INSERT INTO GN_PatientReg(compcode,PatientReg,patient_name,age,sex,vill_city,vill_city2,po,ps,District,Pin,State_Id,PhNo1,PhNo2,PatientType) VALUES('"+compcode+"','" + regno + "','" + name + "','" + age + "','" + sex + "','" + address + "','" + address2 + "','" + po + "','" + ps + "','" + dist + "','" + pin + "','" + state + "','" + ph1 + "','" + ph2 + "','P')";
            theCommand.CommandType = CommandType.Text;
            theCommand.Connection = theConnection;
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


    public bool InsertRequisition(string compcode,string yearcode,string reqno, string regno)
    {
        try
        {
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
            theCommand.CommandText = "update PH_PatientReq set RegistrationNo='" + regno + "' where RequisitionNo='" + reqno + "' and compcode='"+compcode+"' and yearcode='"+yearcode+"'";
            theCommand.CommandType = CommandType.Text;
            theCommand.Connection = theConnection;
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


    //public bool UpdateAppo(string appo, string regno)
    //{


    //    int effectedRows = 0;
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

    //        theCommand.CommandText = "update DC_PatientAppointment set PatientReg='" + regno + "' where AppNo='" + appo + "'";
    //        theCommand.CommandType = CommandType.Text;

    //        effectedRows = theCommand.ExecuteNonQuery(); // Execute insert query.
    //        if (effectedRows > 0)
    //        {
    //            return true;
    //        }
    //        else
    //        {
    //            return false;
    //        }

    //        theConnection.Close();

    //    }
    //    catch
    //    {
    //        return false;
    //    }
    //}
    public bool UpdateRegistration(string compcode,string regno, string name, string age, string sex, string address, string address2, string po, string ps, string dist, string pin, string state, string ph1, string ph2)
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

            theCommand.CommandText = "update GN_PatientReg set address2='" + address2 + "',patient_name='" + name + "', age='" + age + "',sex='" + sex + "',vill_city='" + address + "',po='" + po + "',ps='" + ps + "',District='" + dist + "',Pin='" + pin + "',State_Id=" + state + ",PhNo1='" + ph1 + "',PhNo2='" + ph2 + "'  where PatientReg = '" + regno + "' and compcode='"+compcode+"'";
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
            theCommand.CommandText = "update DC_PatientAppointment set status=0 WHERE AppNo='" + id + "'";
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
}