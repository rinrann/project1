using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using System.Data;

/// <summary>
/// Summary description for AdmissionRelatedReportClass
/// </summary>
public class AdmissionRelatedReportClass
{
    public AdmissionRelatedReportClass(string con)
	{
        conString = con;
	}

    private string conString;
    private SqlConnection theConnection;
    private SqlCommand theCommand;
    private SqlDataAdapter theAdapter;
    private DataTable hospitalTable;


    public DataTable AdmissionReferenceReport(string FromDate, string ToDate, string vill_city, string Po, string District, string Religion, string UnderDoctor, string ReferDoctor, string Diagonasis,string Discipline,string compcode,string yearcode)
    {


        if (Discipline == "0" || Discipline == "null")
            Discipline = "null";
        else
            Discipline = "'" + Discipline + "'";

         if (FromDate == "" || FromDate == "null")
            FromDate = "null";
        else
            FromDate = "'" + FromDate + "'";

        if (ToDate == "" || ToDate == "null")
            ToDate = "null";
        else
            ToDate = "'" + ToDate + "'";

        if (District == "0")
            District = "null";
        else
            District = "'" + District + "'";

        if (Religion == "0")
            Religion = "null";
        else
            Religion = "'" + Religion + "'";

        if (UnderDoctor == "0" || UnderDoctor == "")
            UnderDoctor = "null";
        else
            UnderDoctor = "'" + UnderDoctor + "'";

        if (ReferDoctor == "0" || ReferDoctor == "")
            ReferDoctor = "null";
        else
            ReferDoctor = "'" + ReferDoctor + "'";

        if (Diagonasis == "0")
            Diagonasis = "null";
        else
            Diagonasis = "'" + Diagonasis + "'";

        if (vill_city == "" || vill_city == "null")
            vill_city = "null";
        else
            vill_city = "'" + vill_city + "'";

        if (Po == "" || Po == "null")
            Po = "null";
        else
            Po = "'" + Po + "'";


        DataTable custTable;

        // Connection.
        theConnection = new SqlConnection();
        theConnection.ConnectionString = conString;

        // Command.
        theCommand = new SqlCommand();
        theCommand.Connection = theConnection;
        theCommand.CommandText = "sp_Report_PatientDetails " + FromDate + "," + ToDate + "," + vill_city + "," + Po + "," + District + "," + Religion + "," + UnderDoctor + "," + ReferDoctor + "," + Diagonasis + "," + Discipline + ",'"+compcode+"','"+yearcode+"'";
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

    public DataTable Religion()
    {
        // Connection.
        theConnection = new SqlConnection();
        theConnection.ConnectionString = conString;

        // Command.
        theCommand = new SqlCommand();
        theCommand.Connection = theConnection;
        theCommand.CommandText = "select * from GN_Religion ";
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


    public DataTable District()
    {
        // Connection.
        theConnection = new SqlConnection();
        theConnection.ConnectionString = conString;

        // Command.
        theCommand = new SqlCommand();
        theCommand.Connection = theConnection;
        theCommand.CommandText = "select * from GN_District ";
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

    public DataTable DisciplineType()
    {
        // Connection.
        theConnection = new SqlConnection();
        theConnection.ConnectionString = conString;

        // Command.
        theCommand = new SqlCommand();
        theCommand.Connection = theConnection;
        theCommand.CommandText = "select * from GN_DisciplineType";
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

    public DataTable DisciplineType(string Type)
    {
        // Connection.
        theConnection = new SqlConnection();
        theConnection.ConnectionString = conString;

        // Command.
        theCommand = new SqlCommand();
        theCommand.Connection = theConnection;
        if(Type=="0")
            theCommand.CommandText = "select * from GN_Diagnosis";
        else
        theCommand.CommandText = "select * from GN_Diagnosis where DisciplineId='" + Type + "'";
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



    public DataTable UnderDoctor()
    {
        // Connection.
        theConnection = new SqlConnection();
        theConnection.ConnectionString = conString;

        // Command.
        theCommand = new SqlCommand();
        theCommand.Connection = theConnection;
        theCommand.CommandText = "select * from GN_DoctorMaster ";
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