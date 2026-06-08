using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using System.Data;

/// <summary>
/// Summary description for PatientDoctorVisit12
/// </summary>
public class PatientDoctorVisit
{
    public PatientDoctorVisit(string con)
    {
        conString = con;
    }

    private string conString;
    private SqlConnection theConnection;
    private SqlCommand theCommand;
    private SqlDataAdapter theAdapter;
    private DataTable BedTable;

    //public DataTable GetAllDocVisit(string reg)
    //{
    //    if (reg == "")
    //        reg = "null";
    //    // Connection.
    //    theConnection = new SqlConnection();
    //    theConnection.ConnectionString = conString;

    //    // Command.
    //    theCommand = new SqlCommand();
    //    theCommand.Connection = theConnection;

    //    theCommand.CommandText = "select v.Rowid,v.Time,v.Remarks,v.DocTypeID,pr.PatientReg,pr.patient_name,CONVERT(varchar,pr.AdmissionDate,103) adate, bm.BedNoText,dm.doc_name,CONVERT(varchar,v.Date,103) visitdate,pr.TreatementNote from GN_DoctorMaster dm, dbo.IPD_PatientDoctorVisit v,GN_PatientReg pr,dbo.IPD_BedMaster bm,dbo.IPD_BedAllocation ba where dm.doc_id=v.Docid and pr.PatientReg=v.PatientReg  and bm.BedNo=ba.BedNo and ba.PatientReg=pr.PatientReg and bm.Allotted=1 and pr.PatientReg='" + reg + "' and ba.ToDate  is null and ba.ToTime is null";

    //    theCommand.CommandType = CommandType.Text;

    //    // Adapter.
    //    theAdapter = new SqlDataAdapter();
    //    theAdapter.SelectCommand = theCommand;

    //    // Datatable.
    //    BedTable = new DataTable();
    //    theAdapter.Fill(BedTable); // Fill data into data table.

    //    // Clean up.
    //    theConnection.Dispose();
    //    theCommand.Dispose();
    //    theAdapter.Dispose();

    //    return BedTable;
    //}

    //public DataTable GetPrescriptiondtls(string reg, string DoctorId)
    //{
    //    // Connection.
    //    theConnection = new SqlConnection();
    //    theConnection.ConnectionString = conString;

    //    // Command.
    //    theCommand = new SqlCommand();
    //    theCommand.Connection = theConnection;
    //    if (DoctorId == "0" || DoctorId == "")
    //        theCommand.CommandText = "select map.RowID,op.DoctorId,d.doc_name,CONVERT(varchar,map.IssueDate,103) Date1,map.PrescriptionId,map.Dose,mm.MedicineName,ms.SubGrName,mg.MedicineGroupName,mg.MedicineGroupID,ms.ID,mm.MedicineID  from dbo.IPD_PrescriptionMapping map,dbo.IPD_MedicineGroup mg,dbo.IPD_MedicineMaster mm,dbo.IPD_MedicineSubGroup ms,IPD_Prescription op,GN_DoctorMaster d where d.doc_id=op.DoctorId and op.PrescriptionID=map.PrescriptionId and map.GroupID=mg.MedicineGroupID and map.MedicineId=mm.MedicineID and map.SubGroupId=ms.ID  and op.PatientReg='" + reg + "'  group by map.RowID,op.DoctorId,d.doc_name,map.IssueDate,map.PrescriptionId,map.Dose,mm.MedicineName,ms.SubGrName,mg.MedicineGroupName,mg.MedicineGroupID,ms.ID,mm.MedicineID";
    //    else
    //        theCommand.CommandText = "select map.RowID,op.DoctorId,d.doc_name,CONVERT(varchar,map.IssueDate,103) Date1,map.PrescriptionId,map.Dose,mm.MedicineName,ms.SubGrName,mg.MedicineGroupName,mg.MedicineGroupID,ms.ID,mm.MedicineID  from dbo.IPD_PrescriptionMapping map,dbo.IPD_MedicineGroup mg,dbo.IPD_MedicineMaster mm,dbo.IPD_MedicineSubGroup ms,IPD_Prescription op,GN_DoctorMaster d where op.DoctorId='" + DoctorId + "' and d.doc_id=op.DoctorId and op.PrescriptionID=map.PrescriptionId and map.GroupID=mg.MedicineGroupID and map.MedicineId=mm.MedicineID and   map.SubGroupId=ms.ID  and op.PatientReg='" + reg + "'  group by map.RowID,op.DoctorId,d.doc_name,map.IssueDate,  map.PrescriptionId,map.Dose,mm.MedicineName,ms.SubGrName,mg.MedicineGroupName,mg.MedicineGroupID,ms.ID,  mm.MedicineID";
    //    theCommand.CommandType = CommandType.Text;

    //    // Adapter.
    //    theAdapter = new SqlDataAdapter();
    //    theAdapter.SelectCommand = theCommand;

    //    // Datatable.
    //    DataTable hospitalTable = new DataTable();
    //    theAdapter.Fill(hospitalTable); // Fill data into data table.

    //    // Clean up.
    //    theConnection.Dispose();
    //    theCommand.Dispose();
    //    theAdapter.Dispose();

    //    return hospitalTable;
    //}


    public DataTable GetPrescriptionIdAccordingDoctor(string compcode,string yearcode,string reg, string DoctorId)
    {
        // Connection.
        theConnection = new SqlConnection();
        theConnection.ConnectionString = conString;

        // Command.
        theCommand = new SqlCommand();
        theCommand.Connection = theConnection;
        theCommand.CommandText = "select op.PrescriptionID from  IPD_Prescription op,GN_PatientReg pr where op.compcode=pr.compcode and pr.PatientReg=op.PatientReg and op.DoctorId='" + DoctorId + "' and op.PatientReg='" + reg + "' and op.compcode='"+compcode+"'  and op.Date>=pr.AdmissionDate";
        theCommand.CommandType = CommandType.Text;

        // Adapter.
        theAdapter = new SqlDataAdapter();
        theAdapter.SelectCommand = theCommand;

        // Datatable.
        DataTable hospitalTable = new DataTable();
        theAdapter.Fill(hospitalTable); // Fill data into data table.

        // Clean up.
        theConnection.Dispose();
        theCommand.Dispose();
        theAdapter.Dispose();

        return hospitalTable;
    }

    public DataTable GenerateIPDPrescription(string compcode)
    {
        // Connection.
        theConnection = new SqlConnection();
        theConnection.ConnectionString = conString;

        // Command.
        theCommand = new SqlCommand();
        theCommand.Connection = theConnection;

        theCommand.CommandText = "exec sp_Generate_IPD_PrescriptionNo '" + compcode + "'";
        theCommand.CommandType = CommandType.Text;

        // Adapter.
        theAdapter = new SqlDataAdapter();
        theAdapter.SelectCommand = theCommand;

        // Datatable.
        DataTable hospitalTable = new DataTable();
        theAdapter.Fill(hospitalTable); // Fill data into data table.

        // Clean up.
        theConnection.Dispose();
        theCommand.Dispose();
        theAdapter.Dispose();

        return hospitalTable;
    }

    public DataSet GetDoctorVisitDetails(string compcode,string yearcode,string reg)
    {
        if (reg == "")
            reg = "null";
        // Connection.
        theConnection = new SqlConnection();
        theConnection.ConnectionString = conString;

        // Command.
        theCommand = new SqlCommand();
        theCommand.Connection = theConnection;

        theCommand.CommandText = "exec sp_IPD_PatientDoctorVisitDetails '" + compcode+"','"+yearcode+"','"+reg + "'";

        theCommand.CommandType = CommandType.Text;

        // Adapter.
        theAdapter = new SqlDataAdapter();
        theAdapter.SelectCommand = theCommand;

        // Datatable.
       DataSet BedTable = new  DataSet();
        theAdapter.Fill(BedTable); // Fill data into data table.

        // Clean up.
        theConnection.Dispose();
        theCommand.Dispose();
        theAdapter.Dispose();

        return BedTable;
    }

    public DataTable DropdownDoctor(string compcode,string type)
    {
        // Connection.
        theConnection = new SqlConnection();
        theConnection.ConnectionString = conString;

        // Command.
        theCommand = new SqlCommand();
        theCommand.Connection = theConnection;
        if (type == "0")
            theCommand.CommandText = "select * from GN_DoctorMaster where compcode='"+compcode+"' and Status=1 order by doc_name";
        else
            theCommand.CommandText = "select * from GN_DoctorMaster where compcode='"+compcode+"' and DocTypeId='" + type + "' and Status=1  order by doc_name";
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



    public DataTable getBillQty(string id,string duration)
    {
        // Connection.
        theConnection = new SqlConnection();
        theConnection.ConnectionString = conString;
        decimal du;

        if (duration == "0") { du = 1; }
        else
        {
            theCommand = new SqlCommand();
            theCommand.Connection = theConnection;
            theCommand.CommandText = "select DurationName from IPD_DurationMaster D WHERE D.Durationid='" + duration + "'";
            theCommand.CommandType = CommandType.Text;
            theAdapter = new SqlDataAdapter();
            theAdapter.SelectCommand = theCommand;
            DataTable dt = new DataTable();
            theAdapter.Fill(dt);
            du = Convert.ToDecimal(dt.Rows[0]["DurationName"]);
        }

        // Command.
        theCommand = new SqlCommand();
        theCommand.Connection = theConnection;
        //theCommand.CommandText = "select 24/CONVERT(int,D.DoseName) BillQty from MD_DoseMaster D WHERE D.ID='" + id + "'";
        theCommand.CommandText = "select (24/CONVERT(int,D.DoseName))*(CONVERT(int,'" + du + "')) BillQty from MD_DoseMaster D WHERE D.ID='" + id + "'";
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
    public DataTable DropdownDoctorType(string compcode)
    {
        // Connection.
        theConnection = new SqlConnection();
        theConnection.ConnectionString = conString;

        // Command.
        theCommand = new SqlCommand();
        theCommand.Connection = theConnection;
        theCommand.CommandTimeout = 0;
        theCommand.CommandText = "select * from GN_DoctorType where compcode='"+compcode+"' and Status=1";
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

    public DataTable DropdownDuration(string compcode)
    {
        // Connection.
        theConnection = new SqlConnection();
        theConnection.ConnectionString = conString;

        // Command.
        theCommand = new SqlCommand();
        theCommand.Connection = theConnection;
        theCommand.CommandTimeout = 0;
        theCommand.CommandText = "select d.DurationId,d.DurationName+' Days'  DurationName from  IPD_DurationMaster d where d.status=1 and compcode='"+compcode+"'";
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

    public DataTable Dropdowntemplate(string compcode,string gr)
    {
        // Connection.
        theConnection = new SqlConnection();
        theConnection.ConnectionString = conString;

        // Command.
        theCommand = new SqlCommand();
        theCommand.Connection = theConnection;

        if (gr == "999")
        {
            theCommand.CommandText = "select '999' as PrescrpTemID, 'Others' as PrescrpTemName";
        }
        else
        {
            theCommand.CommandText = "select * from  IPD_PrescriptionTmplate where compcode='" + compcode + "' and PrescriptionGrId='" + gr + "' and status=1";
        }

       // DropDownList35.DataTextField = "PrescrpTemName";
       // DropDownList35.DataValueField = "PrescrpTemID";

        theCommand.CommandType = CommandType.Text;

        // Adapter.
        theAdapter = new SqlDataAdapter();
        theAdapter.SelectCommand = theCommand;

        // Datatable.
        DataTable hospitalTable = new DataTable();
        theAdapter.Fill(hospitalTable); // Fill data into data table.

        // Clean up.
        theConnection.Dispose();
        theCommand.Dispose();
        theAdapter.Dispose();

        return hospitalTable;
    }


    public DataTable GetTemplateMapping(string compcode,string type)
    {
        // Connection.
        theConnection = new SqlConnection();
        theConnection.ConnectionString = conString;

        // Command.
        theCommand = new SqlCommand();
        theCommand.Connection = theConnection;

        theCommand.CommandText = "select * from IPD_PrescriptionTmplateMapping where compcode='"+compcode+"' and PrescrpTemID='" + type + "'";
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


    public DataTable GetTableFill(string regno)
    {
        // Connection.
        theConnection = new SqlConnection();
        theConnection.ConnectionString = conString;

        // Command.
        theCommand = new SqlCommand();
        theCommand.Connection = theConnection;

        theCommand.CommandText = "select * from IPD_DoctorVisitPrescription where PatientReg='" + regno + "'";
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

    public DataTable DropdownTemplate(string compcode)
    {
        // Connection.
        theConnection = new SqlConnection();
        theConnection.ConnectionString = conString;

        // Command.
        theCommand = new SqlCommand();
        theCommand.Connection = theConnection;
        theCommand.CommandText = "select * from IPD_PrescriptionTmplate where compcode='"+compcode+"' and status=1";
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

    public DataTable DropdownBatchNo(string compcode,string yearcode,string MedicineId)
    {
        // Connection.
        theConnection = new SqlConnection();
        theConnection.ConnectionString = conString;

        // Command.
        theCommand = new SqlCommand();
        theCommand.Connection = theConnection;
        //theCommand.CommandText = "EXEC  sp_MD_FetchBatchNo " + MedicineId + "";
        //theCommand.CommandText = "select  md.BatchNo,CONVERT(varchar,md.ExpiryDate,101) ExDate  from MD_PurchaseMedicineDetails md where md.status=1 and md.MedicineID= '" + MedicineId + "'";
        theCommand.CommandText = "select  md.BatchNo,CONVERT(varchar,md.ExpDate,103) ExDate  from BATDETL md where md.compcode='" + compcode + "' and md.yearcode='" + yearcode + "' and ltrim(rtrim(md.icode))= dbo.Fnc_GetMedicineIcode('"+compcode+"','" + MedicineId + "')";
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
    public DataTable GetBatchNoinStock(string compcode,string yearcode,string MedicineId,string PatientReg)
    {
        // Connection.
        theConnection = new SqlConnection();
        theConnection.ConnectionString = conString;

        // Command.
        theCommand = new SqlCommand();
        theCommand.Connection = theConnection;
        //theCommand.CommandText = "EXEC  sp_MD_FetchBatchNo " + MedicineId + "";
        theCommand.CommandText = "EXEC  sp_MD_FetchBatchNoWingWise '" + compcode + "','" + yearcode + "','" + MedicineId + "','" + PatientReg + "'";
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

    public DataTable ExpiryDateFill(string compcode,string BatchNo)
    {
        // Connection.
        theConnection = new SqlConnection();
        theConnection.ConnectionString = conString;

        // Command.
        theCommand = new SqlCommand();
        theCommand.Connection = theConnection;
        //theCommand.CommandText = "select CONVERT(VARCHAR,ExpiryDate,103) Exdate from MD_PurchaseMedicineDetails where compcode='"+compcode+"' and status=1 and BatchNo='" + BatchNo + "'";
        theCommand.CommandText = "select CONVERT(VARCHAR,ExpDate,103) Exdate from BATDETL where compcode='" + compcode + "' and BatchNo='" + BatchNo + "'";
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
    public DataTable medicineQty(string PatientReg, string medid, string qty)
    {
        // Connection.
        theConnection = new SqlConnection();
        theConnection.ConnectionString = conString;

        // Command.
        theCommand = new SqlCommand();
        theCommand.Connection = theConnection;
        theCommand.CommandText = "select dbo.Fnc_MD_AvailableStockDeptWise('" + PatientReg + "','" + medid + "')";
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
    public DataTable medicineQtyBatchwise(string PatientReg,string medid,string batchno,string qty)
    {
        // Connection.
        theConnection = new SqlConnection();
        theConnection.ConnectionString = conString;

        // Command.
        theCommand = new SqlCommand();
        theCommand.Connection = theConnection;
        theCommand.CommandText = "select dbo.Fnc_MD_AvailableStockDeptWiseBatchwise('" + PatientReg + "','" + medid + "','" + batchno + "')";
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
    public DataTable DropdownMedicineGrp(string compcode)
    {
        // Connection.
        theConnection = new SqlConnection();
        theConnection.ConnectionString = conString;

        // Command.
        theCommand = new SqlCommand();
        theCommand.Connection = theConnection;
        theCommand.CommandText = "select * from IPD_MedicineGroup where compcode='" + compcode + "'";
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


    public DataTable DropdownMedicine(string compcode,String Group)
    {
        // Connection.
        theConnection = new SqlConnection();
        theConnection.ConnectionString = conString;

        // Command.
        theCommand = new SqlCommand();
        theCommand.Connection = theConnection;
        if (Group == "0")
            theCommand.CommandText = "select * from IPD_MedicineMaster where compcode='"+compcode+"' and status=1";
        else
            theCommand.CommandText = "select * from IPD_MedicineMaster where compcode='"+compcode+"' and MedicineGroupID='" + Group + "' and status=1 ";
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


    public DataTable DropdownRoute()
    {
        // Connection.
        theConnection = new SqlConnection();
        theConnection.ConnectionString = conString;

        // Command.
        theCommand = new SqlCommand();
        theCommand.Connection = theConnection;
        theCommand.CommandText = "select * from IPD_MedicineRoute where status=1";
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


    public bool Insertdocvisit(string Time, string BillEffectFlag, string ProbableDischargeDate, string regno, string docid, string date, string doctype, string Remarks, string visittype,string compcode,string yearcode,string user)
    {
        if (ProbableDischargeDate == "")
            ProbableDischargeDate = "null";
        else
            ProbableDischargeDate = "'" + ProbableDischargeDate + "'";

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
                    string LedgerId = "";
                    // transactional code...
                    using (theCommand = theConnection.CreateCommand())
                    {
                        theCommand.CommandText = "SELECT al.LedgerID FROM AC_Ledger al WHERE al.compcode='" + compcode + "' and al.LedgerFK='" + regno + "' AND al.ActiveStatus=1";
                        theCommand.Transaction = tran as SqlTransaction;
                        SqlDataReader theReader = theCommand.ExecuteReader();
                        theReader.Read();
                        LedgerId = theReader[0].ToString();
                        theReader.Close();

                        //if (ProbableDischargeDate != "")
                        //{
                        theCommand.CommandText = "update GN_PatientReg set ProbableDischargeDate=" + ProbableDischargeDate + ", User02='" + user + "',logdt02=GETDATE()  where compcode='" + compcode + "' and PatientReg='" + regno + "'";
                            theCommand.Transaction = tran as SqlTransaction;
                            theCommand.ExecuteNonQuery(); // Execute insert query.
                        //}

                            theCommand.CommandText = "update GN_PatientReg_History set ProbableDischargeDate=" + ProbableDischargeDate + ", User02='" + user + "',logdt02=GETDATE()  where compcode='" + compcode + "' and LedgerId='" + LedgerId + "'";
                            theCommand.Transaction = tran as SqlTransaction;
                            theCommand.ExecuteNonQuery(); // Execute insert query.
                       

                        theCommand.Connection = theConnection;
                        theCommand.CommandText = "INSERT INTO IPD_PatientDoctorVisit(compcode,yearcode,LedgerId,BillEffectFlag,Time,DocTypeID,PatientReg,Docid,Date,Remarks,VisitType,User01,logdt01) VALUES ('" + compcode + "','" + yearcode + "','" + LedgerId + "','" + BillEffectFlag + "','" + Time + "','" + doctype + "','" + regno + "','" + docid + "', '" + date + "','" + Remarks + "','" + visittype + "','" + user + "',GETDATE())";
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


    public bool InsertTreatementNote(string compcode,string yearcode,string regno, string TreatementNote, string docid,string user)
    {
        if (TreatementNote == "")
            TreatementNote = "null";
        else
            TreatementNote = "'" + TreatementNote + "'";

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
                        if (TreatementNote != "")
                        {
                            theCommand.CommandText = "update IPD_PatientDoctorVisit set TreatementNote=" + TreatementNote + ", User02='" + user + "',logdt02=GETDATE()  where compcode='" + compcode + "' and yearcode='" + yearcode + "' and PatientReg='" + regno + "' and docid='" + docid + "'";
                            theCommand.Transaction = tran as SqlTransaction;
                            theCommand.ExecuteNonQuery(); // Execute insert query.
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

    public bool InsertdocvisitPrescription(string PatientReg, string DocPresId, string MedicineGroupId, string MedicineId, string RouteId, string DailyDose, string Duration)
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
                        theCommand.CommandText = "INSERT INTO IPD_DoctorVisitPrescription(PatientReg,DocPresId,MedicineGroupId,MedicineId,RouteId, DailyDose,Duration) VALUES ('" + PatientReg + "','" + DocPresId + "','" + MedicineGroupId + "', '" + MedicineId + "', '" + RouteId + "','" + DailyDose + "','" + Duration + "')";
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


    public bool UpdateDocVisit(string Time, string BillEffectFlag, string id, string regno, string ProbableDischargeDate, string docid, string date, string type, string Remarks,string visittype,string compcode,string yearcode,string user)
    {
        if (ProbableDischargeDate == "")
            ProbableDischargeDate = "null";
        else
            ProbableDischargeDate = "'" + ProbableDischargeDate + "'";

        string LedgerId = "";
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
                        theCommand.CommandText = "SELECT al.LedgerID FROM AC_Ledger al WHERE al.compcode='"+compcode+"' and al.LedgerFK='" + regno + "' AND al.ActiveStatus=1";
                        theCommand.Transaction = tran as SqlTransaction;
                        SqlDataReader theReader = theCommand.ExecuteReader();
                        theReader.Read();
                        LedgerId = theReader[0].ToString();
                        theReader.Close();

                        //if (ProbableDischargeDate != "")
                        //{
                        theCommand.CommandText = "update GN_PatientReg set ProbableDischargeDate=" + ProbableDischargeDate + ",user02='"+user+"',logdt02=GETDATE()  where compcode='"+compcode+"' and PatientReg='" + regno + "'";
                        theCommand.Transaction = tran as SqlTransaction;
                        theCommand.ExecuteNonQuery(); // Execute insert query.
                        //}

                        theCommand.CommandText = "update GN_PatientReg_History set ProbableDischargeDate=" + ProbableDischargeDate + ",user02='" + user + "',logdt02=GETDATE()  where compcode='" + compcode + "' and LedgerId='" + LedgerId + "'";
                        theCommand.Transaction = tran as SqlTransaction;
                        theCommand.ExecuteNonQuery(); // Execute insert query.


                        theCommand.Connection = theConnection;
                        theCommand.CommandText = "Update IPD_PatientDoctorVisit set Time='" + Time + "', BillEffectFlag='" + BillEffectFlag + "',Remarks='" + Remarks + "', DocTypeID='" + type + "', Docid='" + docid + "', Date='" + date + "',VisitType='" + visittype + "',user02='" + user + "',logdt02=GETDATE()  where compcode='" + compcode + "' and yearcode='" + yearcode + "' and Rowid = '" + id + "'";
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



    public bool InsertPrescription(string PrescriptionID, string PatientReg, string Date, string Comment, string DoctorId,string compcode,string user)
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
            string LedgerId = "";
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
                        theCommand.CommandText = "SELECT al.LedgerID FROM AC_Ledger al WHERE al.compcode='"+compcode+"' and al.LedgerFK='" + PatientReg + "' AND al.ActiveStatus=1";
                        theCommand.Transaction = tran as SqlTransaction;
                        SqlDataReader theReader1 = theCommand.ExecuteReader();
                        theReader1.Read();
                        LedgerId = theReader1[0].ToString();
                        theReader1.Close();


                        theCommand.CommandText = "select * from IPD_Prescription where compcode='"+compcode+"' and PatientReg='" + PatientReg + "' and DoctorId='" + DoctorId + "'";
                        theCommand.Transaction = tran as SqlTransaction;
                        SqlDataReader theReader = theCommand.ExecuteReader();
                        theReader.Read();

                        if (theReader.HasRows == false)
                        {
                            theReader.Close();
                            theCommand.CommandText = "INSERT INTO IPD_Prescription(compcode,LedgerId,PrescriptionID,PatientReg,Date,Comment,DoctorId,StopFlag,user01,logdt01) VALUES('"+compcode+"','" + LedgerId + "','" + PrescriptionID + "','" + PatientReg + "','" + Date + "','" + Comment + "','" + DoctorId + "',0,'"+user+"',GETDATE())";
                            theCommand.Transaction = tran as SqlTransaction;
                            theCommand.ExecuteNonQuery(); // Execute insert query.
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
            return false;
        }

        finally
        {
            theConnection.Dispose();
            theCommand.Dispose();
        }
        return true;
    }


    public bool InsertPrescriptionMap(string PrescriptionId, string PatientReg, string IssueDate, string AdviceBy, string GroupID, string SubGroupId, string MedicineId, string Dose, string BatchNo, string ExpirtDate,string Duration, string ActualQty, string BillQty, string MapIssueDate,string compcode,string user)
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
            string LedgerId = "";
            theCommand = new SqlCommand();
            theCommand.Connection = theConnection;
            using (IDbTransaction tran = theConnection.BeginTransaction())
            {
                try
                {
                    // transactional code...
                    using (theCommand = theConnection.CreateCommand())
                    {
                        theCommand.CommandText = "SELECT al.LedgerID FROM AC_Ledger al WHERE al.compcode='"+compcode+"' and al.LedgerFK='" + PatientReg + "' AND al.ActiveStatus=1";
                        theCommand.Transaction = tran as SqlTransaction;
                        SqlDataReader theReader1 = theCommand.ExecuteReader();
                        theReader1.Read();
                        LedgerId = theReader1[0].ToString();
                        theReader1.Close();

                        theCommand.CommandText = "INSERT INTO IPD_PrescriptionMapping(compcode,PrescriptionId,GroupID,SubGroupId,MedicineId,Dose, BatchNo,ExpirtDate,Duration,ActualQty,BillQty,IssueDate,StopFlag,user01,logdt01) VALUES('"+compcode+"','" + PrescriptionId + "','" + GroupID + "','" + SubGroupId + "','" + MedicineId + "','" + Dose + "','" + BatchNo + "','" + ExpirtDate + "','" + Duration + "','" + ActualQty + "','" + BillQty + "','" + IssueDate + "',0,'"+user+"',GETDATE())";
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


    public bool UpdatePrescriptionMap(string RowID, string PrescriptionID, string Date, string DoctorId, string GroupID, string SubGroupId, string MedicineId,string ActQty, string BillQty, string batchno, string Duration,string compcode,string user)
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
                        if (DoctorId != "" || DoctorId != "0")
                        {
                            theCommand.CommandText = "update  IPD_Prescription set Date='" + Date + "', DoctorId ='" + DoctorId + "',user02='"+user+"',logdt02=GETDATE() where compcode='"+compcode+"' and PrescriptionID='" + PrescriptionID + "' ";
                            theCommand.Transaction = tran as SqlTransaction;
                            theCommand.ExecuteNonQuery();
                        }

                        theCommand.CommandText = "update  IPD_PrescriptionMapping set GroupID='" + GroupID + "', SubGroupId ='" + SubGroupId + "', MedicineId ='" + MedicineId + "',BatchNo='" + batchno + "',ActualQty='" + ActQty + "',BillQty='" + BillQty + "',Duration='" + Duration + "',IssueDate='" + Date + "',user02='"+user+"',logdt02=GETDATE() where compcode='" + compcode + "' and RowID='" + RowID + "'";
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
        return true;
    }



    public bool DeletePrescriptionMap(string compcode,string RowID)
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
            // string s = Session["userName"];
            theCommand.Connection = theConnection;
            using (IDbTransaction tran = theConnection.BeginTransaction())
            {
                try
                {
                    // transactional code...
                    using (theCommand = theConnection.CreateCommand())
                    {
                        theCommand.CommandText = "delete IPD_PrescriptionMapping where compcode='"+compcode+"' and RowID='" + RowID + "'";
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
        return true;
    }


    //***************************  For Stop Medicine and Prescription   **********************************

    public bool StopMedicneAndPrescription(string compcode,string PrescriptionID, string MedicineId)
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
                        if (PrescriptionID != "")
                        {
                            theCommand.CommandText = "update dbo.IPD_Prescription set StopFlag=1 where compcode='"+compcode+"' and PrescriptionID='" + PrescriptionID + "'";
                            theCommand.Transaction = tran as SqlTransaction;
                            theCommand.ExecuteNonQuery(); // Execute insert query. 

                            theCommand.CommandText = "update dbo.IPD_PrescriptionMapping set StopFlag=1 where compcode='"+compcode+"' and PrescriptionId='" + PrescriptionID + "'";
                            theCommand.CommandType = CommandType.Text;
                            theCommand.ExecuteNonQuery(); // Execute insert query. 
                        }

                        else
                        {
                            theCommand.CommandText = "update dbo.IPD_PrescriptionMapping set StopFlag=1 where compcode='"+compcode+"' and MedicineId='" + MedicineId + "'";
                            theCommand.Transaction = tran as SqlTransaction;
                            theCommand.ExecuteNonQuery(); // Execute insert query. 
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
            return false;
        }
        finally
        {
            theConnection.Dispose();
            theCommand.Dispose();
        }
        return true;
    }



    public bool ServicePrescriptionFunction(int mode, string RowID, string PatientReg, string doctypeid, string PrescriptionId, string DoctorId, string Date, string ServiceId, string Quantity, string Price, string TotalPrice,string compcode,string user,string servcont)
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


                        string LedgerId = "";
                        theCommand.CommandText = "SELECT al.LedgerID FROM AC_Ledger al WHERE al.compcode='"+compcode+"' and al.LedgerFK='" + PatientReg + "' AND al.ActiveStatus=1";
                        theCommand.Transaction = tran as SqlTransaction;
                        SqlDataReader theReader1 = theCommand.ExecuteReader();
                        theReader1.Read();
                        LedgerId = theReader1[0].ToString();
                        theReader1.Close();


                        theCommand.CommandText = "select * from IPD_Prescription where compcode='"+compcode+"' and PatientReg='" + PatientReg + "' and DoctorId='" + DoctorId + "'";
                        theCommand.Transaction = tran as SqlTransaction;
                        SqlDataReader theReader = theCommand.ExecuteReader();
                        theReader.Read();
                        if (theReader.HasRows)
                        {
                            theReader.Close();
                            theCommand.CommandText = "update IPD_Prescription set  Date='" + Date + "',user02='"+user+"',logdt02=GETDATE()  where compcode='"+compcode+"' and PrescriptionID='" + PrescriptionId + "' and DoctorId='" + DoctorId + "'";
                            theCommand.Transaction = tran as SqlTransaction;
                            theCommand.ExecuteNonQuery();
                        }
                        else
                        {
                            theReader.Close();
                            theCommand.CommandText = "insert into IPD_Prescription (compcode,PrescriptionID,PatientReg,Date,DoctorId,LedgerId,user01,logdt01) values('"+compcode+"','" + PrescriptionId + "','" + PatientReg + "','" + Date + "','" + DoctorId + "','" + LedgerId + "','"+user+"',GETDATE())";
                            theCommand.Transaction = tran as SqlTransaction;
                            theCommand.ExecuteNonQuery();
                        }
                        if (mode == 1)
                        {
                            theCommand.CommandText = "INSERT INTO IPD_ServicePrescriptionMap(compcode,IssueDate,LedgerId,PrescriptionId,ServiceId,Quantity,Price,TotalPrice,user01,logdt01,ServCont) VALUES('" + compcode + "','" + Date + "','" + LedgerId + "','" + PrescriptionId + "','" + ServiceId + "','" + Quantity + "','" + Price + "','" + TotalPrice + "','" + user + "',GETDATE(),'"+servcont+"')";
                        }
                        if (mode == 2)
                        {
                            theCommand.CommandText = "update IPD_ServicePrescriptionMap set IssueDate='" + Date + "',  ServiceId='" + ServiceId + "',Quantity='" + Quantity + "',Price='" + Price + "',TotalPrice='" + TotalPrice + "',user02='" + user + "',logdt02=GETDATE(),ServCont='"+servcont+"' where compcode='" + compcode + "' and RowID='" + RowID + "'";
                        }
                        if (mode == 3)
                        {
                            theCommand.CommandText = "delete IPD_ServicePrescriptionMap  where compcode='"+compcode+"' and RowID='" + RowID + "'";
                        }

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
        return true;
    }



    public bool ConsumablePrescriptionFunction(int mode, string PatientReg, string DoctypeID, string RowID, string PrescriptionId, string Date, string DoctorId, string ConsumableGr, string ConsumableItemId, string ActualQty, string BillQty, string Price, string TotalPrice,string compcode,string user)
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

            using (IDbTransaction tran = theConnection.BeginTransaction())
            {
                try
                {
                    // transactional code...
                    using (theCommand = theConnection.CreateCommand())
                    {

                        string LedgerId = "";
                        theCommand.CommandText = "SELECT al.LedgerID FROM AC_Ledger al WHERE al.compcode='"+compcode+"' and al.LedgerFK='" + PatientReg + "' AND al.ActiveStatus=1";
                        theCommand.Transaction = tran as SqlTransaction;
                        SqlDataReader theReader1 = theCommand.ExecuteReader();
                        theReader1.Read();
                        LedgerId = theReader1[0].ToString();
                        theReader1.Close(); 
                        
                        if (mode == 1)
                        {
                            theCommand.CommandText = "select * from IPD_Prescription where compcode='"+compcode+"' and PatientReg='" + PatientReg + "' and DoctorId='" + DoctorId + "'";
                            theCommand.Transaction = tran as SqlTransaction;
                            SqlDataReader theReader = theCommand.ExecuteReader();
                            theReader.Read();
                            theCommand.Connection = theConnection;

                            if (theReader.HasRows)
                            {
                                theReader.Close();
                                theCommand.CommandText = "update IPD_Prescription set  Date='" + Date + "',user02='"+user+"',logdt02=GETDATE()  where compcode='"+compcode+"' and PrescriptionID='" + PrescriptionId + "' and DoctorId='" + DoctorId + "'";
                                theCommand.Transaction = tran as SqlTransaction;
                                theCommand.ExecuteNonQuery();
                            }
                            else
                            {
                                theReader.Close();
                                theCommand.CommandText = "insert into IPD_Prescription (compcode,PrescriptionID,PatientReg,Date,DoctorId,LedgerId,user01,logdt01) values('"+compcode+"','" + PrescriptionId + "','" + PatientReg + "','" + Date + "','" + DoctorId + "','" + LedgerId + "','"+user+"',GETDATE())";
                                theCommand.Transaction = tran as SqlTransaction;
                                theCommand.ExecuteNonQuery();

                            }
                        }

                        if (mode == 1)
                        {
                            theCommand.CommandText = "INSERT INTO IPD_ConsumablePrescriptionMap (compcode,IssueDate,LedgerId,PrescriptionId,ConsumableGr,ConsumableItemId,ActualQty,BillQty,Price,TotalPrice,user01,logdt01) VALUES('" + compcode + "','" + Date + "','" + LedgerId + "','" + PrescriptionId + "','" + ConsumableGr + "','" + ConsumableItemId + "','" + ActualQty + "','" + BillQty + "','" + Price + "','" + TotalPrice + "','" + user + "',GETDATE())";
                        }
                        if (mode == 2)
                        {
                            theCommand.CommandText = "update IPD_ConsumablePrescriptionMap set  IssueDate='" + Date + "', ConsumableGr='" + ConsumableGr + "',ConsumableItemId='" + ConsumableItemId + "',ActualQty='" + ActualQty + "',BillQty='" + BillQty + "',user02='" + user + "',logdt02=GETDATE() where compcode='" + compcode + "' and RowID='" + RowID + "'";
                        }
                        if (mode == 3)
                        {
                            theCommand.CommandText = "delete IPD_ConsumablePrescriptionMap  where compcode='"+compcode+"' and RowID='" + RowID + "'";
                        }
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
        return true;
    }

    public DataTable GetConsumableCharge(string compcode,string ConItemID)
    {
        // Connection.
        theConnection = new SqlConnection();
        theConnection.ConnectionString = conString;

        // Command.
        theCommand = new SqlCommand();
        theCommand.Connection = theConnection;

        theCommand.CommandText = "select i.BillingPrice from dbo.IPD_ConsumableItems i where i.compcode='"+compcode+"' and i.ConItemID='" + ConItemID + "'";
        theCommand.CommandType = CommandType.Text;

        // Adapter.
        theAdapter = new SqlDataAdapter();
        theAdapter.SelectCommand = theCommand;

        // Datatable.
        DataTable hospitalTable = new DataTable();
        theAdapter.Fill(hospitalTable); // Fill data into data table.

        // Clean up.
        theConnection.Dispose();
        theCommand.Dispose();
        theAdapter.Dispose();

        return hospitalTable;
    }


    //public DataTable GridfillServiceDetails(string reg)
    //{
    //    // Connection.
    //    theConnection = new SqlConnection();
    //    theConnection.ConnectionString = conString;

    //    // Command.
    //    theCommand = new SqlCommand();
    //    theCommand.Connection = theConnection;

    //    theCommand.CommandText = "select pres.RowID,pres.PrescriptionId,p.DoctorId,CONVERT(VARCHAR,p.Date,103) Date1,temp.ServiceTemplateName,pres.TotalPrice,pres.Quantity,pres.ServiceId,pres.ServiceId,pres.Price,dm.doc_name from dbo.IPD_ServicePrescriptionMap pres ,IPD_Prescription p,GN_DoctorMaster dm ,IPD_Service_Cons_Template temp where  temp.NameID=pres.ServiceId and pres.PrescriptionId=p.PrescriptionID and p.doctorId=dm.doc_id and p.PatientReg='" + reg + "'";
    //    theCommand.CommandType = CommandType.Text;

    //    // Adapter.
    //    theAdapter = new SqlDataAdapter();
    //    theAdapter.SelectCommand = theCommand;

    //    // Datatable.
    //    DataTable hospitalTable = new DataTable();
    //    theAdapter.Fill(hospitalTable); // Fill data into data table.

    //    // Clean up.
    //    theConnection.Dispose();
    //    theCommand.Dispose();
    //    theAdapter.Dispose();

    //    return hospitalTable;
    //}

    //  ********************************************              Running Medicine and Prescription      *******************************************************************


    //public DataTable GridCurrentPrescription(string reg)
    //{
    //    // Connection.
    //    theConnection = new SqlConnection();
    //    theConnection.ConnectionString = conString;

    //    // Command.
    //    theCommand = new SqlCommand();
    //    theCommand.Connection = theConnection;

    //    theCommand.CommandText = " select p.RowId, p.PrescriptionID,d.doc_name,CONVERT(VARCHAR,p.Date,103) Date1 from IPD_Prescription p,GN_PatientReg pr,GN_DoctorMaster d  where  pr.PatientReg=p.PatientReg and p.DoctorId=d.doc_id  and p.StopFlag=0 and  p.PatientReg='" + reg + "'";
    //    theCommand.CommandType = CommandType.Text;

    //    // Adapter.
    //    theAdapter = new SqlDataAdapter();
    //    theAdapter.SelectCommand = theCommand;

    //    // Datatable.
    //    DataTable hospitalTable = new DataTable();
    //    theAdapter.Fill(hospitalTable); // Fill data into data table.

    //    // Clean up.
    //    theConnection.Dispose();
    //    theCommand.Dispose();
    //    theAdapter.Dispose();

    //    return hospitalTable;
    //}



    public DataTable GridCurrentMedicine(string compcode,string PrescriptionID)
    {
        // Connection.
        theConnection = new SqlConnection();
        theConnection.ConnectionString = conString;

        // Command.
        theCommand = new SqlCommand();
        theCommand.Connection = theConnection;

        theCommand.CommandText = "select distinct map.PrescriptionId,g.MedicineGroupName,sub.SubGrName,mm.MedicineID,mm.MedicineName  from IPD_PrescriptionMapping map,IPD_MedicineGroup g, IPD_MedicineSubGroup sub,IPD_MedicineMaster mm  where g.compcode=map.compcode and sub.compcode=map.compcode and mm.compcode=map.compcode and g.MedicineGroupID=sub.GroupID and sub.ID=mm.SubGroupid and mm.MedicineID=map.MedicineId and map.StopFlag=0 and map.compcode='"+compcode+"' and map.PrescriptionId='" + PrescriptionID + "'";
        theCommand.CommandType = CommandType.Text;

        // Adapter.
        theAdapter = new SqlDataAdapter();
        theAdapter.SelectCommand = theCommand;

        // Datatable.
        DataTable hospitalTable = new DataTable();
        theAdapter.Fill(hospitalTable); // Fill data into data table.

        // Clean up.
        theConnection.Dispose();
        theCommand.Dispose();
        theAdapter.Dispose();

        return hospitalTable;
    }


    public DataTable GridfillConsumableDetails(string compcode,string reg)
    {
        // Connection.
        theConnection = new SqlConnection();
        theConnection.ConnectionString = conString;

        // Command.
        theCommand = new SqlCommand();
        theCommand.Connection = theConnection;

        theCommand.CommandText = "select pres.RowID,CONVERT(VARCHAR,p.Date,103) Date1,p.DoctorId,pres.PrescriptionId,pres.ActualQty,pres.BillQty,pres.ConsumableGr,pres.ConsumableItemId,gr.ConGroupName,dtls.ConItemName,dm.doc_name from dbo.IPD_ConsumablePrescriptionMap pres,IPD_ConsumableGroup gr,IPD_ConsumableItems dtls,IPD_Prescription p,GN_DoctorMaster dm where pres.compcode=p.compcode andgr.compcode=p.compcode and dm.compcode=p.compcode anddtls.compcode=p.compcode and pres.ConsumableGr=gr.ConGrId and pres.ConsumableItemId=dtls.ConItemID and pres.PrescriptionId=p.PrescriptionID and p.doctorId=dm.doc_id and p.compcode='"+compcode+"' and p.PatientReg='" + reg + "'  group by pres.RowID,p.Date,p.DoctorId,pres.PrescriptionId,pres.ActualQty,pres.BillQty,pres.ConsumableGr,pres.ConsumableItemId,gr.ConGroupName,dtls.ConItemName,dm.doc_name";
        theCommand.CommandType = CommandType.Text;

        // Adapter.
        theAdapter = new SqlDataAdapter();
        theAdapter.SelectCommand = theCommand;

        // Datatable.
        DataTable hospitalTable = new DataTable();
        theAdapter.Fill(hospitalTable); // Fill data into data table.

        // Clean up.
        theConnection.Dispose();
        theCommand.Dispose();

        theAdapter.Dispose();
        return hospitalTable;
    }


    public DataTable GridFillBody(string compcode,string reg)
    {
        // Connection.
        theConnection = new SqlConnection();
        theConnection.ConnectionString = conString;

        // Command.
        theCommand = new SqlCommand();
        theCommand.Connection = theConnection;

        theCommand.CommandText = "exec sp_IpdClinicalFindingDetails   '"+compcode+"','" + reg + "'";
        theCommand.CommandType = CommandType.Text;

        // Adapter.
        theAdapter = new SqlDataAdapter();
        theAdapter.SelectCommand = theCommand;

        // Datatable.
        DataTable hospitalTable = new DataTable();
        theAdapter.Fill(hospitalTable); // Fill data into data table.

        // Clean up.
        theConnection.Dispose();
        theCommand.Dispose();
        theAdapter.Dispose();

        return hospitalTable;
    }



    public DataTable GGetComplainId(string ComplainName)
    {
        // Connection.
        theConnection = new SqlConnection();
        theConnection.ConnectionString = conString;

        // Command.
        theCommand = new SqlCommand();
        theCommand.Connection = theConnection;

        theCommand.CommandText = "select RowId from OPD_Complain where ComplainName='" + ComplainName + "'";
        theCommand.CommandType = CommandType.Text;

        // Adapter.
        theAdapter = new SqlDataAdapter();
        theAdapter.SelectCommand = theCommand;

        // Datatable.
        DataTable hospitalTable = new DataTable();
        theAdapter.Fill(hospitalTable); // Fill data into data table.

        // Clean up.
        theConnection.Dispose();
        theCommand.Dispose();
        theAdapter.Dispose();

        return hospitalTable;
    }

    public bool DailyCheckUp_Insert_Update(int Mode, string Time, string RowID, string PatientReg, string Date, string Pulse, string BP, string Temp, string SPO2, string Chest, string PA, string PV, string Urin, string Others, string Sunction, string Doppler, string Bleeding,string compcode,string user)
    {
        string LedgerId = "";
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
                        theCommand.CommandText = "select LedgerID from AC_Ledger al where compcode='"+compcode+"' and LedgerFK='" + PatientReg + "' and ActiveStatus=1";
                        theCommand.Transaction = tran as SqlTransaction;
                        SqlDataReader theReader = theCommand.ExecuteReader();
                        theReader.Read();
                        LedgerId = theReader[0].ToString();
                        theReader.Close();

                        if (Mode == 1)
                            theCommand.CommandText = "INSERT INTO IPD_ClinicalFinding(compcode,LedgerId,Time,PatientReg,Date,Pulse,BP,Temp,SPO2,Chest,PA,PV,Urin,Others,Sunction,Doppler,Bleeding,user01,logdt01) VALUES('"+compcode+"','" + LedgerId + "','" + Time + "','" + PatientReg + "','" + Date + "','" + Pulse + "','" + BP + "','" + Temp + "','" + SPO2 + "','" + Chest + "','" + PA + "','" + PV + "','" + Urin + "','" + Others + "','" + Sunction + "','" + Doppler + "','" + Bleeding + "','"+user+"',GETDATE())";
                        if (Mode == 2)
                            theCommand.CommandText = "update IPD_ClinicalFinding set LedgerId='" + LedgerId + "',Time='" + Time + "',Date='" + Date + "',Pulse='" + Pulse + "',BP='" + BP + "',Temp='" + Temp + "',SPO2='" + SPO2 + "',Chest='" + Chest + "',PA='" + PA + "',PV='" + PV + "',Urin='" + Urin + "',Others='" + Others + "',Sunction='" + Sunction + "',Doppler='" + Doppler + "',Bleeding='" + Bleeding + "',user02='"+user+"',logdt02=GETDATE()  where compcode='"+compcode+"' and RowID='" + RowID + "' ";
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

    

    public bool ClinicalFinding_Insert_Update(int Mode, string Time, string RowID, string PatientReg, string Date, string Pulse, string BP, string Chest, string PA, string PV, string Others,string compcode,string user)
    {
        string LedgerId = "";
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
                        theCommand.CommandText = "SELECT al.LedgerID FROM AC_Ledger al WHERE al.compcode='"+compcode+"' and al.LedgerFK='" + PatientReg + "' AND al.ActiveStatus=1";
                        theCommand.Transaction = tran as SqlTransaction;
                        SqlDataReader theReader = theCommand.ExecuteReader();
                        theReader.Read();
                        LedgerId = theReader[0].ToString();
                        theReader.Close();
                        if (Mode == 1)
                            theCommand.CommandText = "INSERT INTO IPD_ClinicalFinding(compcode,LedgerId,Time,PatientReg,Date,Pulse,BP,Chest,PA,PV,Others,user01,logdt01) VALUES('" + compcode + "','" + LedgerId + "','" + Time + "','" + PatientReg + "','" + Date + "','" + Pulse + "','" + BP + "','" + Chest + "','" + PA + "','" + PV + "','" + Others + "','"+user+"',GETDATE())";
                        if (Mode == 2)
                            theCommand.CommandText = "update IPD_ClinicalFinding set LedgerId='" + LedgerId + "',Time='" + Time + "',Date='" + Date + "',Pulse='" + Pulse + "',BP='" + BP + "',Chest='" + Chest + "',PA='" + PA + "',PV='" + PV + "',Others='" + Others + "',user02='"+user+"',logdt02=GETDATE()  where compcode='"+compcode+"' and RowID='" + RowID + "' ";
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



    public bool BHT_Insert_Update(int Mode, string Time, string RowID, string PatientReg, string Date, string Pulse, string BP, string Temp, string SPO2, string Urin, string Others, string Sunction, string Doppler, string Bleeding,string compcode,string user)
    {
        string LedgerId = "";
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
                        theCommand.CommandText = "SELECT al.LedgerID FROM AC_Ledger al WHERE al.compcode='"+compcode+"' and al.LedgerFK='" + PatientReg + "' AND al.ActiveStatus=1";
                        theCommand.Transaction = tran as SqlTransaction;
                        SqlDataReader theReader = theCommand.ExecuteReader();
                        theReader.Read();
                        LedgerId = theReader[0].ToString();
                        theReader.Close();
                        if (Mode == 1)
                            theCommand.CommandText = "INSERT INTO IPD_ClinicalFinding(compcode,LedgerId,Time,PatientReg,Date,Pulse,BP,Temp,SPO2,Urin,Others,Sunction,Doppler,Bleeding,user01,logdt01) VALUES('"+compcode+"','" + LedgerId + "','" + Time + "','" + PatientReg + "','" + Date + "','" + Pulse + "','" + BP + "','" + Temp + "','" + SPO2 + "','" + Urin + "','" + Others + "','" + Sunction + "','" + Doppler + "','" + Bleeding + "','"+user+"',GETDATE())";
                        if (Mode == 2)
                            theCommand.CommandText = "update IPD_ClinicalFinding set LedgerId='" + LedgerId + "',Time='" + Time + "',Date='" + Date + "',Pulse='" + Pulse + "',BP='" + BP + "',Temp='" + Temp + "',SPO2='" + SPO2 + "',Urin='" + Urin + "',Others='" + Others + "',Sunction='" + Sunction + "',Doppler='" + Doppler + "',Bleeding='" + Bleeding + "',user02='"+user+"',logdt02=GETDATE() where compcode='"+compcode+"' and RowID='" + RowID + "' ";
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


    public bool ComplainMap_Insert_Delete(int Mode, string PatientReg, string Date, string Time, string ComplainId,string compcode,string user)
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

            string LedgerId = "";
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
                        theCommand.CommandText = "SELECT al.LedgerID FROM AC_Ledger al WHERE al.compcode='"+compcode+"' and al.LedgerFK='" + PatientReg + "' AND al.ActiveStatus=1";
                        theCommand.Transaction = tran as SqlTransaction;
                        SqlDataReader theReader = theCommand.ExecuteReader();
                        theReader.Read();
                        LedgerId = theReader[0].ToString();
                        theReader.Close();

                        if (Mode == 1)
                            theCommand.CommandText = "INSERT INTO IPD_ClinicalComplainMap(compcode,LedgerId,PatientReg,Date,Time,ComplainId,user01,logdt01) VALUES('"+compcode+"','" + LedgerId + "','" + PatientReg + "','" + Date + "','" + Time + "','" + ComplainId + "','"+user+"',GETDATE())";
                        if (Mode == 2)
                            theCommand.CommandText = "delete IPD_ClinicalComplainMap  where compcode='"+compcode+"' and PatientReg='" + PatientReg + "' and Date='" + Date + "' and Time='" + Time + "'";

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

    public bool DeleteClinicalFinding(string id,string compcode)
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
                        theCommand.CommandText = "delete from IPD_ClinicalFinding where compcode='"+compcode+"' and RowID='" + id + "'";
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
    public DataTable GetConsumableGroup(string compcode)
    {

        // Connection.
        theConnection = new SqlConnection();
        theConnection.ConnectionString = conString;

        // Command.
        theCommand = new SqlCommand();
        theCommand.Connection = theConnection;

        theCommand.CommandText = "select *  from IPD_ConsumableGroup where compcode='"+compcode+"' and status='1'";

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

    public DataTable DropdownDose(string compcode)
    {

        // Connection.
        theConnection = new SqlConnection();
        theConnection.ConnectionString = conString;

        // Command.
        theCommand = new SqlCommand();
        theCommand.Connection = theConnection;

        theCommand.CommandText = "select d.ID,d.DoseName+' Hourly'  DoseName from  MD_DoseMaster d where d.compcode='"+compcode+"' and d.status=1";

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


    public DataTable GetConsumableItem(string compcode,string group)
    {

        // Connection.
        theConnection = new SqlConnection();
        theConnection.ConnectionString = conString;

        // Command.
        theCommand = new SqlCommand();
        theCommand.Connection = theConnection;

        if (group == "0")
            theCommand.CommandText = "select *  from IPD_ConsumableItems where compcode='"+compcode+"'";
        else
            theCommand.CommandText = "select *  from IPD_ConsumableItems where compcode='"+compcode+"' and ConGrId='" + group + "'";

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

    public DataTable GetServiceConsumableTemplate(string compcode,string TemplateCategoryId)
    {

        // Connection.
        theConnection = new SqlConnection();
        theConnection.ConnectionString = conString;

        // Command.
        theCommand = new SqlCommand();
        theCommand.Connection = theConnection;

        if (TemplateCategoryId == "0")
            theCommand.CommandText = "select *  from  IPD_Service_Cons_Template c where c.compcode='"+compcode+"'";
        else
            theCommand.CommandText = "select *  from  IPD_Service_Cons_Template c where c.compcode='"+compcode+"' and c.TemplateCategoryId='" + TemplateCategoryId + "'";

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

    public DataTable GetOperationConsumableTemplate(string TemplateCategoryId)
    {

        // Connection.
        theConnection = new SqlConnection();
        theConnection.ConnectionString = conString;

        // Command.
        theCommand = new SqlCommand();
        theCommand.Connection = theConnection;

        if (TemplateCategoryId == "0")
            theCommand.CommandText = "select *  from  IPD_Operation_Cons_Template c";
        else
            theCommand.CommandText = "select *  from  IPD_Operation_Cons_Template c where c.TemplateCategoryId='" + TemplateCategoryId + "'";

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

    public DataTable GetServiceDetails(string ServiceCategoryID,string compcode)
    {

        // Connection.
        theConnection = new SqlConnection();
        theConnection.ConnectionString = conString;

        // Command.
        theCommand = new SqlCommand();
        theCommand.Connection = theConnection;
        theCommand.CommandText = "select s.ServiceTemplateName,s.NameID  from IPD_Service_Cons_Template s where s.TemplateCategoryId='" + ServiceCategoryID + "' AND s.status=1 and s.compcode='"+compcode+"'";

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


    public DataTable GetServiceConsumableTemplateeGroup(string compcode)
    {

        // Connection.
        theConnection = new SqlConnection();
        theConnection.ConnectionString = conString;

        // Command.
        theCommand = new SqlCommand();
        theCommand.Connection = theConnection;

        theCommand.CommandText = "select c.TemplateCategoryId,c.CategoryName  from IPD_Service_Cons_TemplateCategory c WHERE c.compcode='"+compcode+"' and c.status=1";

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


    public DataTable GetServiceTemplateCharge(string compcode,string ServiceTemplateId)
    {

        // Connection.
        theConnection = new SqlConnection();
        theConnection.ConnectionString = conString;

        // Command.
        theCommand = new SqlCommand();
        theCommand.Connection = theConnection;

        if (ServiceTemplateId == "0")
            theCommand.CommandText = "select c.ServiceCharge  from  IPD_Service_Cons_Template c where c,compcode='"+compcode+"'";
        else
            theCommand.CommandText = "select c.ServiceCharge  from  IPD_Service_Cons_Template c where c.compcode='"+compcode+"' and c.NameID='" + ServiceTemplateId + "'";

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

    public DataTable GetOTConsumableTemplateMapping(string NameID)
    {

        // Connection.
        theConnection = new SqlConnection();
        theConnection.ConnectionString = conString;

        // Command.
        theCommand = new SqlCommand();
        theCommand.Connection = theConnection;

        theCommand.CommandText = "select map.RowId, gr.ConGrId,gr.ConGroupName,i.ConItemID,i.ConItemName,map.ActualQty,map.BillQty,map.PriceperUnit from IPD_Operation_Cons_TemplateMapping map,IPD_ConsumableGroup gr,IPD_ConsumableItems i where gr.ConGrId=i.ConGrId and map.ConsumableItemId=i.ConItemID and map.NameID='" + NameID + "'";

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

    public DataTable GetConsumableTemplateMapping(string compcode,string NameID)
    {

        // Connection.
        theConnection = new SqlConnection();
        theConnection.ConnectionString = conString;

        // Command.
        theCommand = new SqlCommand();
        theCommand.Connection = theConnection;

        theCommand.CommandText = "select map.RowId, gr.ConGrId,gr.ConGroupName,i.ConItemID,i.ConItemName,map.ActualQty,map.BillQty,map.PriceperUnit from IPD_Service_Cons_TemplateMapping map,IPD_ConsumableGroup gr,IPD_ConsumableItems i where gr.compcode=map.compcode and i.compcode=map.compcode and gr.ConGrId=i.ConGrId and map.ConsumableItemId=i.ConItemID and map.compcode='"+compcode+"' and map.NameID='" + NameID + "'";

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

    public DataTable getVisitedDoctors(string compcode,string yearcode,string patientreg)
    {
        DataTable docTable;
        // Connection.
        theConnection = new SqlConnection();
        theConnection.ConnectionString = conString;

        // Command.
        theCommand = new SqlCommand();
        theCommand.Connection = theConnection;
        theCommand.CommandText = "Select GD.*,PD.Docid,PD.PatientReg from GN_DoctorMaster GD left join IPD_PatientDoctorVisit PD on GD.compcode=PD.compcode and GD.doc_id=PD.docid where PD.PatientReg='" + patientreg + "' and PD.compcode='"+compcode+"' and PD.yearcode='"+yearcode+"' group by PD.docid,PD.PatientReg,GD.doc_id,GD.doc_name,GD.Address,GD.Country,GD.Pin,GD.Phone,GD.Fax,GD.Designation,GD.Qualification,GD.SalesMan,GD.City,GD.doc_ph_res,GD.doc_cell,GD.surgery_type,GD.surgery_charges,GD.visit_charges,GD.SpecialistIn1,GD.EmailId,GD.Commission_Per,GD.Commission_Rs,GD.DocTypeId,GD.Stateid,GD.SpecialistIn2,GD.SpecialistIn3,GD.Status,GD.Address2,GD.Phone2,GD.ps,GD.District,GD.DrRegNo,GD.DoctorFees,GD.DoctorFeesNight,GD.visit_night,GD.Serverflag,GD.LedgerID,GD.slcode,GD.compcode,GD.User01,GD.User02,GD.logdt01,GD.logdt02";
        //PD.Date,PD.Time,PD.DocTypeID,PD.Remarks,PD.BillEffectFlag,PD.LedgerId,PD.Serverflag,PD.TreatementNote";
        //GD.doc_id,GD.doc_name,GD.Address,GD.Country,GD.Pin,GD.Phone,GD.Fax,GD.Designation,GD.Qualification,GD.SalesMan,GD.City,GD.doc_ph_res,GD.doc_cell,GD.surgery_type,GD.surgery_charges,GD.visit_charges,GD.SpecialistIn1,GD.EmailId,GD.Commission_Per,GD.Commission_Rs,GD.DocTypeId,GD.Stateid,GD.SpecialistIn2,GD.SpecialistIn3,GD.Status,GD.Address2,GD.Phone2,GD.ps,GD.District,GD.DrRegNo,GD.DoctorFees,GD.Serverflag,GD.LedgerID,GD.slcode
        theCommand.CommandType = CommandType.Text;
        // Adapter.
        theAdapter = new SqlDataAdapter();
        theAdapter.SelectCommand = theCommand;

        // Datatable.
        docTable = new DataTable();
        theAdapter.Fill(docTable);// Fill data into data table.

        // Clean up.
        theConnection.Dispose();
        theCommand.Dispose();
        theAdapter.Dispose();

        return docTable;
    }

    public String getDocVisitNote(string compcode,string yearcode,string docid, String patientreg)
    {
        String note;
        // Connection.
        theConnection = new SqlConnection();
        theConnection.ConnectionString = conString;

        // Command.
        theCommand = new SqlCommand();
        theCommand.Connection = theConnection;
        theCommand.CommandText = "Select TreatementNote from IPD_PatientDoctorVisit where compcode='"+compcode+"' and yearcode='"+yearcode+"' and PatientReg='" + patientreg + "' and docid='" + docid + "'";

        // Adapter.
        theAdapter = new SqlDataAdapter();
        theAdapter.SelectCommand = theCommand;

        // Datatable.
        DataTable noteTable;
        noteTable = new DataTable();
        theAdapter.Fill(noteTable);// Fill data into data table.

        // Clean up.
        theConnection.Dispose();
        theCommand.Dispose();
        theAdapter.Dispose();
        note = noteTable.Rows[0]["TreatementNote"].ToString();
        return note;

    }
}