using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using System.Data;

/// <summary>
/// Summary description for BirthCertificate12
/// </summary>
public class BirthCertificate
{
    public BirthCertificate(string con)
    {
        conString = con;
    }

    private string conString;
    private SqlConnection theConnection;
    private SqlCommand theCommand;
    private SqlDataAdapter theAdapter;
    private DataTable hospitalTable;

    public DataTable GetPatientDtls(string regno,string compcode)
    {
        // Connection.
        theConnection = new SqlConnection();
        theConnection.ConnectionString = conString;

        // Command.
        theCommand = new SqlCommand();
        theCommand.Connection = theConnection;
        theCommand.CommandText = "select *,CONVERT(varchar,pr.AdmissionDate,103) ADate  from GN_District d, dbo.GN_PatientReg pr where d.compcode=pr.compcode and d.ID=pr.District and pr.compcode='"+compcode+"' and pr.PatientReg='" + regno + "'";
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

    public DataTable GetChildDtls(string regno,string compcode)
    {
        // Connection.
        theConnection = new SqlConnection();
        theConnection.ConnectionString = conString;

        // Command.
        theCommand = new SqlCommand();
        theCommand.Connection = theConnection;
        theCommand.CommandText = "select *,CONVERT(varchar,map.DeliveryDate,103) deldt from dbo.GN_ChildSex cs,dbo.OT_DeliveryMode dm,dbo.OT_DeliveryNote dn,dbo.OT_DeliveryNoteMapping map where cs.compcode=dn.compcode and dm.compcode=dn.compcode and map.compcode=dn.compcode and cs.ID=map.Sex and dm.ID=dn.ModeofDelivery and map.DeliveryNoteID=dn.DeliveryNoteID and dn.compcode='"+compcode+"' and dn.PatientReg='" + regno + "'";
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