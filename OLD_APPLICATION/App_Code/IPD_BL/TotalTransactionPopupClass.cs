using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using System.Data;

/// <summary>
/// Summary description for TotalTransactionPopupClass
/// </summary>
public class TotalTransactionPopupClass
{
    public TotalTransactionPopupClass(string con)
    {
        conString = con;
    }

    private string conString;
    private SqlConnection theConnection;
    private SqlCommand theCommand;
    private SqlDataAdapter theAdapter;
    private DataTable BedTable;


    public DataTable GetDetails(string ledgerId,string compcode,string yearcode)
    {
        // Connection.
        theConnection = new SqlConnection();
        theConnection.ConnectionString = conString;

        // Command.
        theCommand = new SqlCommand();
        theCommand.Connection = theConnection;
        theCommand.CommandText = "SELECT bd.BillNo,pr.PatientReg,pr.patient_name,pr.age,pr.PhNo1,pr.vill_city,bd.BedCharge,bd.DoctorVisit,bd.Medicine, bd.Consumable,bd.SeviceDtls,bd.Pathology,bd.XRay,bd.USG,bd.OTCharges,bd.OTAttendenceCharge,bd.OTConsumableharge, bd.DueAmount,bd.Ambulance,bd.SisterAya,bd.Instrument,bd.Discount,bd.Total,bd.AnesthesiaMedicine,bd.AnesthesiaConsumable  FROM GN_PatientReg pr,IPD_PatientBillDtls bd  WHERE bd.compcode=pr.compcode and pr.PatientReg=bd.PatientRegNo AND bd.LedgerId='" + ledgerId + "' and bd.compcode='"+compcode+"'"; 
        theCommand.CommandType = CommandType.Text;

        // Adapter.
        theAdapter = new SqlDataAdapter();
        theAdapter.SelectCommand = theCommand;

        // Datatable.
        BedTable = new DataTable();
        theAdapter.Fill(BedTable); // Fill data into data table.

        // Clean up.
        theConnection.Dispose();
        theCommand.Dispose();
        theAdapter.Dispose();


        return BedTable;
    }
}