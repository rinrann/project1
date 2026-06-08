using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using System.Data;

/// <summary>
/// Summary description for PatientAdmission12
/// </summary>
public class PatientAdmission
{
    public PatientAdmission(string con)
    {
        conString = con;
    }

    private string conString;
    private SqlConnection theConnection;
    private SqlCommand theCommand;
    private SqlDataAdapter theAdapter;
    private DataTable hospitalTable;

    public bool Insertpatient_MASTER(string HusbandName, string PUserId, string PPassword, string month, string day, string dob, string Email, string vill_city, string po, string ps, string District, string Pin, string State_Id, string vill_city2, string po2, string GuadianConact, string District2, string RegNo, string patientname, string referby, string diagnosis, string age, string sex, string religion, string maritial, string contact1, string contact2, string guadianname1, string relation1, string guadianname2, string relation2, string guadianname3, string relation3, string under_doctor, string bedallocaton, string admissiondate, string admissiontime, string typeofpayment, string advance_amt, string path, string referid, string refername, string insuranceno, string CreatedDate, string CreatedBy,string cocode,string yearcode)
    {
        if (age == "")
            age = "0";
        if (advance_amt == "")
            advance_amt = "0.00";
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
                        theCommand.CommandText = "exec sp_GenerateLedgerId '"+cocode+"','P'";
                        theCommand.Transaction = tran as SqlTransaction;
                        SqlDataReader theReader1 = theCommand.ExecuteReader();
                        theReader1.Read();
                        string ledgerid = theReader1[0].ToString();
                        theReader1.Close();

                        theCommand.CommandText = "insert into AC_Ledger(compcode,LedgerID,LedgerFK,LedgerName,LedgerType,CreatedDate,CreatedBy,ActiveStatus,user01,logdt01)  values('" + cocode + "','" + ledgerid + "','" + RegNo + "','" + patientname + "','P','" + CreatedDate + "','" + CreatedBy + "',1,'" + CreatedBy + "',GETDATE())";
                        theCommand.Transaction = tran as SqlTransaction;
                        theCommand.ExecuteNonQuery();

                        theCommand.CommandText = "select * from GN_PatientReg where compcode='"+cocode+"' and PatientReg='" + RegNo + "'";
                        theCommand.Transaction = tran as SqlTransaction;
                        SqlDataReader theReader = theCommand.ExecuteReader();
                        theReader.Read();


                        if (theReader.HasRows)
                        {
                            theReader.Close();
                            String cd = "update GN_PatientReg set  CheckStatus=1,PatientType='I',ProbableDischargeDate=NULL,AdvAmount='" + advance_amt + "', LedgerId='" + ledgerid + "', bedallocation='" + bedallocaton + "', HusbandName='" + HusbandName + "',Agemonth='" + month + "',Ageday='" + day + "',DOB='" + dob + "',Email='" + Email + "',vill_city='" + vill_city + "',po='" + po + "',ps='" + ps + "',District='" + District + "',Pin='" + Pin + "',State_Id='" + State_Id + "',vill_city2='" + vill_city2 + "',po2='" + po2 + "',GuadianConact='" + GuadianConact + "',District2='" + District2 + "',path='" + path + "',patient_name='" + patientname + "',referred_by='" + referby + "',Diagnosis='" + diagnosis + "',age='" + age + "',sex='" + sex + "',religion='" + religion + "',marital_status='" + maritial + "',PhNo1='" + contact1 + "',PhNo2='" + contact2 + "',guardian_name='" + guadianname1 + "',relation='" + relation1 + "',guadian_name2='" + guadianname2 + "',relation2='" + relation2 + "',guadian_name3='" + guadianname3 + "',relation3='" + relation3 + "',underDoctor='" + under_doctor + "',AdmissionDate='" + admissiondate + "',AdmissionTime='" + admissiontime + "',ReferID='" + referid + "',ReferName='" + refername + "',InsuranceNo='" + insuranceno + "',user02='"+CreatedBy+"',logdt02=GETDATE() where compcode=-'"+cocode+"' and PatientReg='" + RegNo + "'";
                            theCommand.CommandText = cd;
                            theCommand.Transaction = tran as SqlTransaction;
                            theCommand.ExecuteNonQuery(); // Execute insert query.


                            String cddup = "update GN_PatientReg_History set  CheckStatus=1,PatientType='I', LedgerId='" + ledgerid + "', bedallocation='" + bedallocaton + "', HusbandName='" + HusbandName + "',Agemonth='" + month + "',Ageday='" + day + "',DOB='" + dob + "',Email='" + Email + "',vill_city='" + vill_city + "',po='" + po + "',ps='" + ps + "',District='" + District + "',Pin='" + Pin + "',State_Id='" + State_Id + "',vill_city2='" + vill_city2 + "',po2='" + po2 + "',GuadianConact='" + GuadianConact + "',District2='" + District2 + "',path='" + path + "',patient_name='" + patientname + "',referred_by='" + referby + "',Diagnosis='" + diagnosis + "',age='" + age + "',sex='" + sex + "',religion='" + religion + "',marital_status='" + maritial + "',PhNo1='" + contact1 + "',PhNo2='" + contact2 + "',guardian_name='" + guadianname1 + "',relation='" + relation1 + "',guadian_name2='" + guadianname2 + "',relation2='" + relation2 + "',guadian_name3='" + guadianname3 + "',relation3='" + relation3 + "',underDoctor='" + under_doctor + "',AdmissionDate='" + admissiondate + "',AdmissionTime='" + admissiontime + "',ReferID='" + referid + "',ReferName='" + refername + "',InsuranceNo='" + insuranceno + "',user02='" + CreatedBy + "',logdt02=GETDATE() where compcode='"+cocode+"' and PatientReg='" + RegNo + "'";
                            theCommand.CommandText = cddup;
                            theCommand.Transaction = tran as SqlTransaction;
                            theCommand.ExecuteNonQuery(); // Execute insert query.
                        }
                        else
                        {
                            theReader.Close();
                            // Insert Patient_data
                            String cd = "INSERT INTO GN_PatientReg(compcode,CheckStatus,AdvAmount,LedgerId,bedallocation,HusbandName,PUserId,PPassword,Agemonth,Ageday,DOB,Email,vill_city,po,ps,District,Pin,State_Id,vill_city2,po2,GuadianConact,District2,path,PatientType,PatientReg,patient_name,referred_by,Diagnosis,age,sex,religion,marital_status,PhNo1,PhNo2,guardian_name,relation,guadian_name2,relation2,guadian_name3,relation3,underDoctor,AdmissionDate,AdmissionTime,ReferID,ReferName,InsuranceNo,user01,logdt01) VALUES('"+cocode+"',1,'" + advance_amt + "','" + ledgerid + "','" + bedallocaton + "','" + HusbandName + "','" + PUserId + "','" + PPassword + "','" + month + "','" + day + "','" + dob + "','" + Email + "','" + vill_city + "','" + po + "','" + ps + "','" + District + "','" + Pin + "','" + State_Id + "','" + vill_city2 + "','" + po2 + "','" + GuadianConact + "','" + District2 + "','" + path + "','I','" + RegNo + "','" + patientname + "','" + referby + "','" + diagnosis + "'," + age + ",'" + sex + "','" + religion + "','" + maritial + "','" + contact1 + "','" + contact2 + "','" + guadianname1 + "','" + relation1 + "','" + guadianname2 + "','" + relation2 + "','" + guadianname3 + "','" + relation3 + "','" + under_doctor + "','" +Convert.ToDateTime(admissiondate).ToString("yyyy-MM-dd") + "','" + admissiontime + "','" + referid + "','" + refername + "','" + insuranceno + "','"+CreatedBy+"',GETDATE())";
                            theCommand.CommandText = cd;
                            theCommand.Transaction = tran as SqlTransaction;
                            theCommand.ExecuteNonQuery(); // Execute insert query.

                            String cdhis = "INSERT INTO GN_PatientReg_History(compcode,CheckStatus,LedgerId,bedallocation,HusbandName,PUserId,PPassword,Agemonth,Ageday,DOB,Email,vill_city,po,ps,District,Pin,State_Id,vill_city2,po2,GuadianConact,District2,path,PatientType,PatientReg,patient_name,referred_by,Diagnosis,age,sex,religion,marital_status,PhNo1,PhNo2,guardian_name,relation,guadian_name2,relation2,guadian_name3,relation3,underDoctor,AdmissionDate,AdmissionTime,ReferID,ReferName,InsuranceNo,user01,logdt01) VALUES('" + cocode + "',1,'" + ledgerid + "','" + bedallocaton + "','" + HusbandName + "','" + PUserId + "','" + PPassword + "','" + month + "','" + day + "','" + dob + "','" + Email + "','" + vill_city + "','" + po + "','" + ps + "','" + District + "','" + Pin + "','" + State_Id + "','" + vill_city2 + "','" + po2 + "','" + GuadianConact + "','" + District2 + "','" + path + "','I','" + RegNo + "','" + patientname + "','" + referby + "','" + diagnosis + "'," + age + ",'" + sex + "','" + religion + "','" + maritial + "','" + contact1 + "','" + contact2 + "','" + guadianname1 + "','" + relation1 + "','" + guadianname2 + "','" + relation2 + "','" + guadianname3 + "','" + relation3 + "','" + under_doctor + "','" + Convert.ToDateTime(admissiondate).ToString("yyyy-MM-dd") + "','" + admissiontime + "','" + referid + "','" + refername + "','" + insuranceno + "','" + CreatedBy + "',GETDATE())";
                            theCommand.CommandText = cdhis;
                            theCommand.Transaction = tran as SqlTransaction;
                            theCommand.ExecuteNonQuery(); // Execute insert query.

                            theCommand.CommandText = "exec sp_PopulateslmastFrmAC_ledger '"+cocode+"','"+yearcode+"','" + CreatedDate + "','" + CreatedBy + "','" + ledgerid + "'";
                            theCommand.Transaction = tran as SqlTransaction;
                            theCommand.ExecuteNonQuery(); // Execute insert query.
                        }


                        // Insert BED_ALLOCATION
                        String cd1 = "INSERT INTO IPD_BedAllocation(compcode,LedgerId,PatientReg,BedNo,FromDate,FromTime,user01,logdt01) VALUES('"+cocode+"','" + ledgerid + "','" + RegNo + "','" + bedallocaton + "','" + Convert.ToDateTime(admissiondate).ToString("yyyy-MM-dd") + "','" + admissiontime + "','"+CreatedBy+"',GETDATE())";
                        theCommand.CommandText = cd1;
                        theCommand.Transaction = tran as SqlTransaction;
                        theCommand.ExecuteNonQuery(); // Execute insert query.

                        // Insert current_bed
                        cd1 = "update IPD_BedMaster set Allotted=1 where compcode='"+cocode+"' and BedNo=" + bedallocaton + "";
                        theCommand.CommandText = cd1;
                        theCommand.Transaction = tran as SqlTransaction;
                        theCommand.ExecuteNonQuery();

                        if (advance_amt.ToString() != "" || advance_amt.ToString() != "0" || advance_amt.ToString() != "0.00")
                        {
                            string receiptNo = "";
                            theCommand.CommandText = "EXEC sp_ACCOUNT_GenerateReceiptNo '"+cocode+"'";
                            theCommand.Transaction = tran as SqlTransaction;
                            SqlDataReader theReader2 = theCommand.ExecuteReader();
                            theReader2.Read();
                            receiptNo = theReader2[0].ToString();

                            theReader2.Close();
                            theCommand.CommandText = "INSERT INTO AC_Transaction(compcode,yearcode,receiptNo,paymenttype,Reason,LadgerId, Credit, TrunsactionDate,EntryBy,user01,logdt01) VALUES ('" + cocode + "','" + yearcode + "','" + receiptNo + "',5,'IPD Advance Charge','" + ledgerid + "','" + advance_amt + "', '" + CreatedDate + "','" + CreatedBy + "','" + CreatedBy + "',GETDATE())";
                            theCommand.Transaction = tran as SqlTransaction;
                            theCommand.ExecuteNonQuery();
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


    public DataTable CheckUserId(string compcode,string id)
    {
        // Connection.
        theConnection = new SqlConnection();
        theConnection.ConnectionString = conString;

        // Command.
        theCommand = new SqlCommand();
        theCommand.Connection = theConnection;
        theCommand.CommandText = "select * from GN_PatientReg where PUserId='" + id + "' and compcode='"+compcode+"'";
        theCommand.CommandType = CommandType.Text;

        // Adapter.
        theAdapter = new SqlDataAdapter();
        theAdapter.SelectCommand = theCommand;

        // Datatable.
        DataTable BedTable = new DataTable();
        theAdapter.Fill(BedTable); // Fill data into data table.

        // Clean up.
        theConnection.Dispose();
        theCommand.Dispose();
        theAdapter.Dispose();

        return BedTable;
    }

    public string GetDiagnosisId(string Diagnosis)
    {
        string DiagId = "";
        // Connection.
        theConnection = new SqlConnection();
        theConnection.ConnectionString = conString;

        try
        {
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
                        theCommand.CommandText = "select d.DiagnosisId from GN_Diagnosis d where d.DiagnosisName='" + Diagnosis + "'";
                        theCommand.Transaction = tran as SqlTransaction;
                        SqlDataReader theReader1 = theCommand.ExecuteReader();
                        theReader1.Read();

                        if (theReader1.HasRows)
                        {
                            DiagId = theReader1[0].ToString();
                            theReader1.Close();
                        }
                        else
                        {
                            theReader1.Close();
                            theCommand.CommandText = "insert into GN_Diagnosis(DiagnosisName) VALUES('" + Diagnosis + "')";

                            theCommand.Transaction = tran as SqlTransaction;
                            theCommand.ExecuteNonQuery(); // Execute insert query.

                            theCommand.CommandText = "select d.DiagnosisId from GN_Diagnosis d where d.DiagnosisName='" + Diagnosis + "'";
                            theCommand.Transaction = tran as SqlTransaction;
                            SqlDataReader theReader = theCommand.ExecuteReader();
                            theReader.Read();
                            DiagId = theReader[0].ToString();
                            theReader.Close();
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
            return DiagId;
        }
        finally
        {
            // Clean up.
            theConnection.Dispose();
            theCommand.Dispose();
        }
        return DiagId;
    }



    public bool InsertUserDetails(string UserID, string UserName, string Password, string PhoneNo_1, string PhoneNo_2, string EmailId, string CreateDate, string CreatedBy)
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
                        // Insert Patient_data
                        String cd = "INSERT INTO GN_UserDetails(UserID,UserName,UserRoleID,Password,PhoneNo_1,PhoneNo_2,EmailId,CreateDate,CreatedBy,Status) VALUES('" + UserID + "','" + UserName + "',8,'" + Password + "','" + PhoneNo_1 + "','" + PhoneNo_2 + "','" + EmailId + "','" + CreateDate + "','" + CreatedBy + "',1)";
                        theCommand.CommandText = cd;
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



    public DataTable GetBedPattern(string bed)
    {
        // Connection.
        theConnection = new SqlConnection();
        theConnection.ConnectionString = conString;

        // Command.
        theCommand = new SqlCommand();
        theCommand.Connection = theConnection;
        theCommand.CommandText = "select BedNoText from IPD_BedMaster where BedNo='" + bed + "'";
        theCommand.CommandType = CommandType.Text;

        // Adapter.
        theAdapter = new SqlDataAdapter();
        theAdapter.SelectCommand = theCommand;

        // Datatable.
        DataTable BedTable = new DataTable();
        theAdapter.Fill(BedTable); // Fill data into data table.

        // Clean up.
        theConnection.Dispose();
        theCommand.Dispose();
        theAdapter.Dispose();

        return BedTable;
    }
    public DataTable GenerateRegNo(string compcode, string yearcode)
    {
        // Connection.
        theConnection = new SqlConnection();
        theConnection.ConnectionString = conString;

        // Command.
        theCommand = new SqlCommand();
        theCommand.Connection = theConnection;
        theCommand.CommandText = "exec sp_GenerateRegNO '" + compcode + "','" + yearcode + "'";
        theCommand.CommandType = CommandType.Text;

        // Adapter.
        theAdapter = new SqlDataAdapter();
        theAdapter.SelectCommand = theCommand;

        // Datatable.
        DataTable BedTable = new DataTable();
        theAdapter.Fill(BedTable); // Fill data into data table.

        // Clean up.
        theConnection.Dispose();
        theCommand.Dispose();
        theAdapter.Dispose();

        return BedTable;
    }
    public bool Updatepatient_MASTER(string HusbandName, string month, string day, string dob, string Email, string vill_city, string po, string ps, string District, string Pin, string State_Id, string vill_city2, string po2, string GuadianConact, string District2, string RegNo, string patientname, string referby, string diagnosis, string age, string sex, string religion, string maritial, string contact1, string contact2, string guadianname1, string relation1, string guadianname2, string relation2, string guadianname3, string relation3, string under_doctor, string bedallocaton, string admissiondate, string admissiontime, string typeofpayment, string advance_amt, string path, string docid, string refertext, string insuranceno, string CreatedDate, string CreatedBy,string compcode,string yearcode)
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


            // update Patient_data
            theCommand = new SqlCommand();
            theCommand.Connection = theConnection;
            using (IDbTransaction tran = theConnection.BeginTransaction())
            {
                try
                {
                    // transactional code...
                    using (theCommand = theConnection.CreateCommand())
                    {
                        if (path == "null" || path == "")
                            theCommand.CommandText = "update GN_PatientReg set PatientType='I',HusbandName='" + HusbandName + "',Agemonth='" + month + "',Ageday='" + day + "',DOB='" + dob + "', Email='" + Email + "', GuadianConact='" + GuadianConact + "',po2='" + po2 + "',District2='" + District2 + "', ps='" + ps + "', State_Id='" + State_Id + "',po='" + po + "',District='" + District + "', ReferID='" + docid + "',ReferName='" + refertext + "',InsuranceNo='" + insuranceno + "', patient_name='" + patientname + "',referred_by='" + referby + "',Diagnosis='" + diagnosis + "',age='" + age + "',sex='" + sex + "',religion='" + religion + "',marital_status='" + maritial + "',PhNo1='" + contact1 + "',PhNo2='" + contact2 + "',vill_city='" + vill_city + "',AdmissionDate='" + admissiondate + "',AdmissionTime='" + admissiontime + "',vill_city2='" + vill_city2 + "',Pin='" + Pin + "',guardian_name='" + guadianname1 + "',relation='" + relation1 + "',guadian_name2='" + guadianname2 + "',relation2='" + relation2 + "',guadian_name3='" + guadianname1 + "',relation3='" + relation3 + "',underDoctor='" + under_doctor + "',AdvAmount='" + advance_amt + "',user02='"+CreatedBy+"',logdt02=GETDATE()  where compcode='"+compcode+"' and PatientReg='" + RegNo + "'";
                        else
                            theCommand.CommandText = "update GN_PatientReg set PatientType='I',HusbandName='" + HusbandName + "',GuadianConact='" + GuadianConact + "',po2='" + po2 + "',District2='" + District2 + "', ps='" + ps + "', State_Id='" + State_Id + "',po='" + po + "',District='" + District + "', ReferID='" + docid + "',ReferName='" + refertext + "',InsuranceNo='" + insuranceno + "', patient_name='" + patientname + "',referred_by='" + referby + "',Diagnosis='" + diagnosis + "',age='" + age + "',sex='" + sex + "',religion='" + religion + "',marital_status='" + maritial + "',PhNo1='" + contact1 + "',PhNo2='" + contact2 + "',vill_city='" + vill_city + "',vill_city2='" + vill_city2 + "',Pin='" + Pin + "',guardian_name='" + guadianname1 + "',AdmissionDate='" + admissiondate + "',AdmissionTime='" + admissiontime + "',relation='" + relation1 + "',guadian_name2='" + guadianname2 + "',relation2='" + relation2 + "',guadian_name3='" + guadianname1 + "',relation3='" + relation3 + "',underDoctor='" + under_doctor + "',AdvAmount='" + advance_amt + "',user02='"+CreatedBy+"',logdt02=GETDATE()  where compcode='"+compcode+"' and PatientReg='" + RegNo + "'";
                        theCommand.Transaction = tran as SqlTransaction;
                        theCommand.ExecuteNonQuery();

                        theCommand.CommandText = "select LedgerID from AC_Ledger where compcode='"+compcode+"' and LedgerFK='" + RegNo + "' and LedgerType='P'";
                        theCommand.Transaction = tran as SqlTransaction;
                        SqlDataReader theReader1 = theCommand.ExecuteReader();
                        theReader1.Read();
                        string ledgerid = theReader1[0].ToString();
                        theReader1.Close();

                        theCommand.CommandText = "select LedgerID from AC_Ledger where compcode='"+compcode+"' and LedgerFK='" + RegNo + "' and LedgerType='P'";
                        theCommand.Transaction = tran as SqlTransaction;
                        SqlDataReader theReader = theCommand.ExecuteReader();
                        theReader.Read();

                        if (theReader.HasRows)
                        {
                            theReader.Close();
                            theCommand.CommandText = "update AC_Transaction set credit='" + advance_amt + "',user02='"+CreatedBy+"',logdt02=GETDATE()  where compcode='"+compcode+"' and reason like'IPD Advance Charge%'";
                            theCommand.Transaction = tran as SqlTransaction;
                            theCommand.ExecuteNonQuery();
                        }
                        else
                        {
                            if (Convert.ToDecimal(advance_amt) > 0)
                            {
                                string receiptNo = "";
                                theCommand.CommandText = "EXEC sp_ACCOUNT_GenerateReceiptNo '" + compcode + "'";
                                theCommand.Transaction = tran as SqlTransaction;
                                SqlDataReader theReader2 = theCommand.ExecuteReader();
                                theReader2.Read();
                                receiptNo = theReader2[0].ToString();

                                theCommand.CommandText = "INSERT INTO AC_Transaction(compcode,yearcode,receiptNo,paymenttype,Reason,LadgerId, Credit, TrunsactionDate,EntryBy,user01,logdt01) VALUES ('"+compcode+"','"+yearcode+"','" + receiptNo + "',5,'IPD Advance Charge','" + ledgerid + "','" + advance_amt + "', '" + CreatedDate + "','" + CreatedBy + "','"+CreatedBy+"',GETDATE())";
                                theCommand.Transaction = tran as SqlTransaction;
                                theCommand.ExecuteNonQuery();
                            }
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

    public DataTable GetPatientCheckForDischarge(string compcode,string regno)
    {
        // Connection.
        theConnection = new SqlConnection();
        theConnection.ConnectionString = conString;

        // Command.
        theCommand = new SqlCommand();
        theCommand.Connection = theConnection;
        theCommand.CommandText = "select pr.patient_name,pr.HusbandName,pr.PhNo1,pr.PhNo2,pr.vill_city,pr.po,pr.ps,pr.Email,pr.District,pr.State_Id,pr.Pin,pr.vill_city2,pr.po2,pr.GuadianConact,pr.District2,pr.guardian_name,pr.guadian_name2,pr.guadian_name3 ,pr.relation,pr.relation2,pr.relation3,pr.sex,pr.religion,pr.marital_status,CONVERT(varchar,pr.AdmissionDate,103) ADate,convert(varchar,pr.dob,103)  dob1  from  dbo.GN_PatientReg pr where pr.compcode='"+compcode+"' and  pr.PatientReg='" + regno + "'  and pr.CheckStatus!=1";
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


    public DataTable GetPatientDtls(string compcode,string yearcode,string regno)
    {
        // Connection.
        theConnection = new SqlConnection();
        theConnection.ConnectionString = conString;

        // Command.
        theCommand = new SqlCommand();
        theCommand.Connection = theConnection;
        theCommand.CommandText = "select *,CONVERT(varchar,pr.AdmissionDate,103) ADate,convert(varchar,pr.dob,103)  dob1,(select max(credit) from dbo.AC_Transaction where AC_Transaction.compcode='"+compcode+"' and AC_Transaction.yearcode='"+yearcode+"' and AC_Transaction.LadgerId=pr.LedgerId) AdvAmount from GN_Diagnosis dia, dbo.GN_PatientReg pr,IPD_BedAllocation b,dbo.GN_DoctorMaster d,IPD_BedMaster bm where dia.compcode=pr.compcode and bm.compcode=pr.compcode and b.compcode=pr.compcode and d.compcode=pr.compcode and dia.DiagnosisId=pr.Diagnosis and bm.BedNo=b.BedNo and pr.underDoctor=d.doc_id and pr.PatientReg=b.PatientReg and pr.compcode='"+compcode+"' and pr.PatientReg='" + regno + "'";
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

    public DataTable GetPatientDtlsChamber(string compcode,string regno)
    {
        // Connection.
        theConnection = new SqlConnection();
        theConnection.ConnectionString = conString;

        // Command.
        theCommand = new SqlCommand();
        theCommand.Connection = theConnection;
        theCommand.CommandText = "select * from  dbo.OPD_PatientRegistration pr where pr.compcode='"+compcode+"' and pr.PatientRegNo='" + regno + "'";
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

    public DataTable GetDoctor()
    {
        // Connection.
        theConnection = new SqlConnection();
        theConnection.ConnectionString = conString;

        // Command.
        theCommand = new SqlCommand();
        theCommand.Connection = theConnection;
        theCommand.CommandText = "select * from GN_DoctorMaster  order by doc_name";
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


    public DataTable GetDoctorType(string compcode)
    {
        // Connection.
        theConnection = new SqlConnection();
        theConnection.ConnectionString = conString;

        // Command.
        theCommand = new SqlCommand();
        theCommand.Connection = theConnection;
        theCommand.CommandText = "select * from GN_DoctorType where compcode='"+compcode+"'";
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


    public DataTable GetState(string compcode)
    {
        // Connection.
        theConnection = new SqlConnection();
        theConnection.ConnectionString = conString;

        // Command.
        theCommand = new SqlCommand();
        theCommand.Connection = theConnection;
        theCommand.CommandText = "select * from GN_StateMaster where compcode='"+compcode+"' and status='1'";
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


    public DataTable GetDistrict(string compcode)
    {
        // Connection.
        theConnection = new SqlConnection();
        theConnection.ConnectionString = conString;

        // Command.
        theCommand = new SqlCommand();
        theCommand.Connection = theConnection;
        theCommand.CommandText = "select * from GN_District where compcode='" + compcode + "' and status='1'";
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

    public DataTable GetCity(string id)
    {
        // Connection.
        theConnection = new SqlConnection();
        theConnection.ConnectionString = conString;

        // Command.
        theCommand = new SqlCommand();
        theCommand.Connection = theConnection;
        theCommand.CommandText = "select * from GN_CityMaster where Status=1 and State_ID='" + id + "'";
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
    public DataTable GetReferBy(string compcode)
    {
        // Connection.
        theConnection = new SqlConnection();
        theConnection.ConnectionString = conString;

        // Command.
        theCommand = new SqlCommand();
        theCommand.Connection = theConnection;
        theCommand.CommandText = "select * from GN_ReferBy where compcode='"+compcode+"'";
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

    public DataTable DropDownDistrict(string compcode)
    {
        // Connection.
        theConnection = new SqlConnection();
        theConnection.ConnectionString = conString;

        // Command.
        theCommand = new SqlCommand();
        theCommand.Connection = theConnection;
        theCommand.CommandText = "select * from GN_District where Status=1 and compcode='"+compcode+"'";
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

    public DataTable GetPaymentMode(string compcode)
    {
        // Connection.
        theConnection = new SqlConnection();
        theConnection.ConnectionString = conString;

        // Command.
        theCommand = new SqlCommand();
        theCommand.Connection = theConnection;
        theCommand.CommandText = "select * from GN_PaymentMode where compcode='"+compcode+"' and status='1'";
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
    public DataTable GetMaritialStatus(string compcode)
    {
        // Connection.
        theConnection = new SqlConnection();
        theConnection.ConnectionString = conString;

        // Command.
        theCommand = new SqlCommand();
        theCommand.Connection = theConnection;
        theCommand.CommandText = "select * from GN_MaritialStatus where compcode='"+compcode+"' and status='1'";
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

    public DataTable GetReligion(string compcode)
    {
        // Connection.
        theConnection = new SqlConnection();
        theConnection.ConnectionString = conString;

        // Command.
        theCommand = new SqlCommand();
        theCommand.Connection = theConnection;
        theCommand.CommandText = "select * from GN_Religion where compcode='"+compcode+"' and status='1'";
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

    public DataTable GetGender(string compcode)
    {
        // Connection.
        theConnection = new SqlConnection();
        theConnection.ConnectionString = conString;

        // Command.
        theCommand = new SqlCommand();
        theCommand.Connection = theConnection;
        theCommand.CommandText = "select * from GN_Gender where compcode='"+compcode+"' and status='1'";
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


    public DataTable DropdownDiscipline()
    {
        // Connection.
        theConnection = new SqlConnection();
        theConnection.ConnectionString = conString;

        // Command.
        theCommand = new SqlCommand();
        theCommand.Connection = theConnection;
        theCommand.CommandText = "select * from GN_DisciplineType where Status=1 order by DisciplineName";
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

    public DataTable DropddownDiagnosis(string discipline)
    {
        // Connection.
        theConnection = new SqlConnection();
        theConnection.ConnectionString = conString;

        // Command.
        theCommand = new SqlCommand();
        theCommand.Connection = theConnection;
        if (discipline == "0")
            theCommand.CommandText = "select * from GN_Diagnosis order by DiagnosisName where Status=1";
        else
            theCommand.CommandText = "select * from GN_Diagnosis where  DisciplineId='" + discipline + "' and Status=1 order by DiagnosisName";
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



    public DataTable GridFillDetails(string compcode,string name, string id)
    {
        if (name == "")
            name = "null";
        // Connection.
        theConnection = new SqlConnection();
        theConnection.ConnectionString = conString;

        // Command.
        theCommand = new SqlCommand();
        theCommand.Connection = theConnection;
        theCommand.CommandText = "exec sp_AdmissionReferbydtls " + name + ",'" + id + "','"+compcode+"'";
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



    public string InsertCar(string Name, string Address1, string Address2, string District, string Pin, string PhNo1, string PhNo2, string CarType, string CarNo,string compcode,string user)
    {
        string flag = "UnSuccessfull";
        int effectedRows = 0;
        try
        {
            // Connection.
            theConnection = new SqlConnection();
            if (conString != "")
            {
                theConnection.ConnectionString = conString;
                theConnection.Open();
            }

            string sql11;
            sql11 = "select * from GN_CarMaster where compcode='"+compcode+"' and Name='" + Name + "'  and Address1='" + Address1 + "' and Address2='" + Address2 + "' ";
            SqlDataAdapter da11 = new SqlDataAdapter(sql11, theConnection);
            DataTable dt11 = new DataTable();
            da11.Fill(dt11);


            if (dt11.Rows.Count == 0)
            {

                // Command.
                theCommand = new SqlCommand();
                theCommand.Connection = theConnection;
                String cd = "INSERT INTO GN_CarMaster(compcode,Name,Address1,Address2,District,Pin,PhNo1,PhNo2,CarType,CarNo,user01,logdt01) VALUES('"+compcode+"','" + Name + "','" + Address1 + "','" + Address2 + "','" + District + "','" + Pin + "','" + PhNo1 + "','" + PhNo2 + "','" + CarType + "','" + CarNo + "','"+user+"',GETDATE())";
                theCommand.CommandText = cd;
                theCommand.CommandType = CommandType.Text;
                effectedRows = theCommand.ExecuteNonQuery(); // Execute insert query.
            }
            else
                flag = "Duplicate";
            if (effectedRows > 0)

                flag = "Successfull";
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



    /*public string InsertQuack(string QuackName, string Address1, string Address2, string District, string Pin, string PhNo1, string PhNo2,string compcode,string user)
    {
        int effectedRows = 0;
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

            string sql11;
            sql11 = "select * from GN_QuackMaster where compcode='"+compcode+"' and QuackName='" + QuackName + "'  and Address1='" + Address1 + "' and Address2='" + Address2 + "' ";
            SqlDataAdapter da11 = new SqlDataAdapter(sql11, theConnection);
            DataTable dt11 = new DataTable();
            da11.Fill(dt11);

            if (dt11.Rows.Count == 0)
            {
                String cd = "INSERT INTO GN_QuackMaster(CompCode,QuackName,Address1,Address2,District,Pin,PhNo1,PhNo2,user01,logdt01) VALUES('"+compcode+"','" + QuackName + "','" + Address1 + "','" + Address2 + "','" + District + "','" + Pin + "','" + PhNo1 + "','" + PhNo2 + "','"+user+"',GETDATE())";
                theCommand.CommandText = cd;
                theCommand.CommandType = CommandType.Text;
                effectedRows = theCommand.ExecuteNonQuery(); // Execute insert query.
            }
            else
                flag = "Duplicate";
            if (effectedRows > 0)
                flag = "Successfull";
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
    }*/

    public bool InsertQuack(string id, string QuackName, string Address1, string Address2, string District, string Pin, string PhNo1, string PhNo2, string type, string user,string compcode)
    {
        //int effectedRows = 0;

        string commpercent = "0";
        string Commission_Rs = "0";

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
                    using (theCommand = theConnection.CreateCommand())
                    {

                        theCommand.CommandText = "exec sp_GenerateLedgerId Q,'"+compcode+"'";
                        theCommand.Transaction = tran as SqlTransaction;
                        SqlDataReader theReader1 = theCommand.ExecuteReader();
                        theReader1.Read();
                        string ledgerid = theReader1[0].ToString();
                        theReader1.Close();

                        theCommand.CommandText = "insert into AC_Ledger(compcode,LedgerID,LedgerFK,LedgerName,LedgerType,CreatedDate,CreatedBy,ActiveStatus,user01,logdt01)  values('"+compcode+"','" + ledgerid + "','" + id + "','" + QuackName + "','Q',getdate(),'" + user + "',1,'"+user+"',GETDATE())";
                        theCommand.Transaction = tran as SqlTransaction;
                        theCommand.ExecuteNonQuery();

                        theCommand.CommandText = "INSERT INTO GN_QuackMaster(compcode,QuackId,QuackName,Address1,Address2,District,Pin,PhNo1,PhNo2,type,status,Commission_Per,Commission_Rs,LedgerID,user01,logdt01) VALUES('"+compcode+"','" + id + "','" + QuackName + "','" + Address1 + "','" + Address2 + "','" + District + "','" + Pin + "','" + PhNo1 + "','" + PhNo2 + "','" + type + "','1','" + commpercent + "','" + Commission_Rs + "','" + ledgerid + "','"+user+"',GETDATE())";
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


    public string InsertDoctor(string DocTypeId, string doc_name, string Address, string Address2, string District, string Pin, string PhNo1, string PhNo2, string user, string createDate,string compcode,string yearcode)
    {
        int effectedRows = 0;
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


            using (IDbTransaction tran = theConnection.BeginTransaction())
            {
                try
                {
                    // transactional code...
                    using (theCommand = theConnection.CreateCommand())
                    {

                        theCommand.CommandText = "exec  sp_IPD_GenerateDoctorID '"+compcode+"'";
                        theCommand.Transaction = tran as SqlTransaction;
                        SqlDataReader theReader = theCommand.ExecuteReader();
                        theReader.Read();
                        string docid = theReader[0].ToString();
                        theReader.Close();

                        theCommand.CommandText = "exec sp_GenerateLedgerId '" + compcode + "',D";
                        theCommand.Transaction = tran as SqlTransaction;
                        SqlDataReader theReader2 = theCommand.ExecuteReader();
                        theReader2.Read();
                        string ledgerid = theReader2[0].ToString();
                        theReader2.Close();


                        theCommand.CommandText = "select * from GN_DoctorMaster where compcode='"+compcode+"' and doc_name='" + doc_name + "'  and Address='" + Address + "' and Address2='" + Address2 + "' ";
                        theCommand.Transaction = tran as SqlTransaction;
                        SqlDataReader theReader1 = theCommand.ExecuteReader();
                        theReader1.Read();


                        if (theReader1.HasRows == false)
                        {
                            // Command.
                            theReader1.Close();
                            theCommand = new SqlCommand();
                            theCommand.Connection = theConnection;

                            theCommand.CommandText = "insert into AC_Ledger(CompCode,LedgerID,LedgerFK,LedgerName,LedgerType,CreatedDate,CreatedBy,ActiveStatus,user01,logdt01)  values('"+compcode+"','" + ledgerid + "','" + docid + "','" + doc_name + "','D',getdate(),'" + user + "',1,'"+user+"',GETDATE())";
                            theCommand.Transaction = tran as SqlTransaction;
                            theCommand.ExecuteNonQuery();

                            String cd = "INSERT INTO GN_DoctorMaster(compcode,DocTypeId,doc_id,doc_name,Address,Address2,District,Pin,Phone,Phone2,status,LedgerID,user01,logdt01) VALUES('"+compcode+"','" + DocTypeId + "','" + docid + "','" + doc_name + "','" + Address + "','" + Address2 + "','" + District + "','" + Pin + "','" + PhNo1 + "','" + PhNo2 + "','1','" + ledgerid + "','"+user+"',GETDATE())";
                            theCommand.CommandText = cd;
                            theCommand.Transaction = tran as SqlTransaction;
                            effectedRows = theCommand.ExecuteNonQuery(); // Execute insert query. 

                            theCommand.CommandText = "exec sp_PopulateslmastFrmAC_ledger '"+compcode+"','"+yearcode+"','" + createDate + "','" + user + "','" + ledgerid + "'";
                            theCommand.Transaction = tran as SqlTransaction;
                            effectedRows = theCommand.ExecuteNonQuery(); // Execute insert query.
                        }
                        else
                        {
                            theReader1.Close();
                            flag = "Duplicate";
                        }
                        if (effectedRows > 0)
                            flag = "Successfull";

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
            theConnection.Close();
            return flag;
        }
        finally
        {
            theConnection.Dispose();
            theCommand.Dispose();
        }
        return flag;
    }

    public DataTable GenerateQuackID(string compcode)
    {
        DataTable quacktable;
        // Connection.
        theConnection = new SqlConnection();
        theConnection.ConnectionString = conString;

        // Command.
        theCommand = new SqlCommand();
        theCommand.Connection = theConnection;
        theCommand.CommandText = "exec sp_IPD_GenerateQuackID '"+compcode+"'";
        theCommand.CommandType = CommandType.Text;

        // Adapter.
        theAdapter = new SqlDataAdapter();
        theAdapter.SelectCommand = theCommand;

        // Datatable.
        quacktable = new DataTable();
        theAdapter.Fill(quacktable); // Fill data into data table.

        // Clean up.
        theConnection.Dispose();
        theCommand.Dispose();
        theAdapter.Dispose();

        return quacktable;
    }
}