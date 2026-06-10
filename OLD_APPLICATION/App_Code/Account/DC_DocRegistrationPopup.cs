using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using System.Data;

/// <summary>
/// Summary description for DC_DocRegistrationPopup
/// </summary>
public class DC_DocRegistrationPopup
{
	public DC_DocRegistrationPopup(string con)
	{
        conString = con;
	}

    private string conString;
    private SqlConnection theConnection;
    private SqlCommand theCommand;
    private SqlDataAdapter theAdapter;
    private DataTable hospitalTable;
    private DataTable assoidtable;

    public DataTable GridPopup(string name, string type)
    {
        string whr="Where status='1'";

        if (name == "")
            name = "null";
        else
        {
            if (type == "D")
            {
                whr = whr + "and doc_name like '%" + name + "%'";
            }
            else
            {
                whr = whr + "and quackname like '%" + name + "%'";
            }
        }
        // Connection.
        theConnection = new SqlConnection();
        theConnection.ConnectionString = conString;

        // Command.
        theCommand = new SqlCommand();
        theCommand.Connection = theConnection;
        if (type == "D")
        {
            theCommand.CommandText = "select doc_id,DrRegNo,Address,City,Phone,'Dr. '+doc_name dname from dbo.GN_DoctorMaster " + whr;
        }
        else
        {
            theCommand.CommandText = "select QuackId doc_id,QuackId DrRegNo,Address1 Address,Address2 City,PhNo1 Phone,QuackName dname from dbo.GN_QuackMaster " + whr;
        }
        theCommand.CommandType = CommandType.Text;


        // Adapter.
        theAdapter = new SqlDataAdapter();
        theAdapter.SelectCommand = theCommand;

        // Datatable.
        DataTable docTable;
        docTable = new DataTable();
        theAdapter.Fill(docTable); // Fill data into data table.

        // Clean up.
        theConnection.Dispose();
        theCommand.Dispose();
        theAdapter.Dispose();

        return docTable;
    }
}