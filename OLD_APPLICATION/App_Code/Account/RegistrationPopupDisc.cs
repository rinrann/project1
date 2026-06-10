using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using System.Data;

/// <summary>
/// Summary description for RegistrationPopupDisc
/// </summary>
public class RegistrationPopupDisc
{
    public RegistrationPopupDisc(string con)
	{
        conString = con;
	}

    private string conString;
    private SqlConnection theConnection;
    private SqlCommand theCommand;
    private SqlDataAdapter theAdapter;
    private DataTable hospitalTable;
    private DataTable assoidtable;

    public DataTable GridPopupDisc(string reg, string name, string ph, string address)
    {
        if (reg == "")
            reg = "";
       
        if (name == "")
            name = "";
       

        if (ph == "")
            ph = "";
        

        if (address == "")
            address = "";
      
        // Connection.
        theConnection = new SqlConnection();
        theConnection.ConnectionString = conString;

        // Command.
        theCommand = new SqlCommand();
        theCommand.Connection = theConnection;

        theCommand.CommandText = "Select dd.PatientReg,CONVERT(varchar,dd.DischargeDate,103) DiscDate,pt.patient_name,pt.guardian_name,pt.PhNo1,PhNo2,CONVERT(varchar,pt.AdmissionDate,103) ADate,pt.vill_city,pt.underDoctor,pt.ReferName,pt.Diagnosis from GN_DischargeDetail dd,GN_PatientReg pt where pt.Patientreg=dd.PatientReg and dd.PatientReg like '%" + reg + "%' and pt.patient_name like '%" + name + "%' and pt.PhNo1 like '%" + ph + "%' and pt.vill_city like '%" + address + "%' order by dd.DischargeDate desc";
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