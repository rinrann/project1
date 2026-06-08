using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using System.Data;

/// <summary>
/// Summary description for ServiceTemplate
/// </summary>
public class ServiceTemplate
{
    public ServiceTemplate(string con)
    {
        conString = con;
    }


    private string conString;
    private SqlConnection theConnection;
    private SqlCommand theCommand;
    private SqlDataAdapter theAdapter;
    private DataTable hospitalTable;

    public DataTable GridFill(string cocode)
    {
        // Connection.
        theConnection = new SqlConnection();
        theConnection.ConnectionString = conString;

        // Command.
        theCommand = new SqlCommand();
        theCommand.Connection = theConnection;
        theCommand.CommandText = "select * from dbo.IPD_Service_Cons_TemplateCategory where Compcode='" + cocode + "'";
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

   
    public DataTable GridFillName(string cocode)
    {
        // Connection.
        theConnection = new SqlConnection();
        theConnection.ConnectionString = conString;

        // Command.
        theCommand = new SqlCommand();
        theCommand.Connection = theConnection;
        theCommand.CommandText = "select * from dbo.IPD_Service_Cons_TemplateCategory c,IPD_Service_Cons_Template n where c.TemplateCategoryId=n.TemplateCategoryId";
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


    public DataTable OperationGridFillName()
    {
        // Connection.
        theConnection = new SqlConnection();
        theConnection.ConnectionString = conString;

        // Command.
        theCommand = new SqlCommand();
        theCommand.Connection = theConnection;
        //theCommand.CommandText = "select * from dbo.IPD_Operation_Cons_TemplateCategory c,IPD_Operation_Cons_Template n where c.TemplateCategoryId=n.TemplateCategoryId";
        //select C.OPERATIONID AS CATEGOTRYID,C.OPERATIONNAME AS CATEGORYNAME, N.SERVICETEMPLATENAME ,N.SERVICECHARGE from dbo.IPD_OperationDetails c,IPD_Operation_Cons_Template n where c.OperationID=n.TemplateCategoryId
        theCommand.CommandText = "select C.OPERATIONID AS TemplateCategoryId,C.OPERATIONNAME AS CATEGORYNAME, N.SERVICETEMPLATENAME ,N.SERVICECHARGE,C.OPERATIONID AS NAMEID from dbo.IPD_OperationDetails c,IPD_Operation_Cons_Template n where c.OperationID=n.TemplateCategoryId";
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

    public bool TemplateFillFunction(int mode, string NameID, string ServiceTemplateName, string ConsumableCategoryId, string ConsumableItemId, string ActualQty, string BillQty, string PriceperUnit,string compcode)
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
                        if (mode == 1)
                        {

                            theCommand.CommandText = "INSERT INTO IPD_Service_Cons_TemplateMapping (compcode,NameID,ConsumableCategoryId,ConsumableItemId,ActualQty,BillQty,PriceperUnit)  VALUES('"+compcode+"',(select NameID from IPD_Service_Cons_Template where ServiceTemplateName='" + ServiceTemplateName + "' and Status=1),'" + ConsumableCategoryId + "','" + ConsumableItemId + "','" + ActualQty + "','" + BillQty + "','" + PriceperUnit + "')";

                        }

                        if (mode == 2)
                        {
                            theCommand.CommandText = "delete IPD_Service_Cons_TemplateMapping  where NameID='" + NameID + "' and compcode='"+compcode+"'";

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

    public bool OperationTemplateFillFunction(int mode, string NameID, string ServiceTemplateName, string ConsumableCategoryId, string ConsumableItemId, string ActualQty, string BillQty, string PriceperUnit)
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
                        if (mode == 1)
                        {

                            theCommand.CommandText = "INSERT INTO IPD_Operation_Cons_TemplateMapping (NameID,ConsumableCategoryId,ConsumableItemId,ActualQty,BillQty,PriceperUnit)  VALUES('"+NameID+"','" + ConsumableCategoryId + "','" + ConsumableItemId + "','" + ActualQty + "','" + BillQty + "','" + PriceperUnit + "')";

                        }

                        if (mode == 2)
                        {
                            theCommand.CommandText = "delete IPD_Operation_Cons_TemplateMapping  where NameID='" + NameID + "'";

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

    public DataTable FillMap(string Id)
    {
        // Connection.
        theConnection = new SqlConnection();
        theConnection.ConnectionString = conString;

        // Command.
        theCommand = new SqlCommand();
        theCommand.Connection = theConnection;
        theCommand.CommandText = "select m.RowId,m.NameID,m.ConsumableCategoryId,g.ConGroupName,m.ConsumableItemId,i.ConItemName,m.ActualQty,m.BillQty,m.PriceperUnit from IPD_Service_Cons_TemplateMapping m,IPD_ConsumableGroup g,IPD_ConsumableItems i where  g.ConGrId=i.ConGrId and m.ConsumableItemId=i.ConItemID and m.NameID='" + Id + "'";
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

    public DataTable OperationFillMap(string Id)
    {
        // Connection.
        theConnection = new SqlConnection();
        theConnection.ConnectionString = conString;

        // Command.
        theCommand = new SqlCommand();
        theCommand.Connection = theConnection;
        theCommand.CommandText = "select m.RowId,m.NameID,m.ConsumableCategoryId,g.ConGroupName,m.ConsumableItemId,i.ConItemName,m.ActualQty,m.BillQty,m.PriceperUnit from IPD_operation_Cons_TemplateMapping m,IPD_ConsumableGroup g,IPD_ConsumableItems i where  g.ConGrId=i.ConGrId and m.ConsumableItemId=i.ConItemID and m.NameID='" + Id + "'";
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

    public DataTable ServiceCate()
    {
        // Connection.
        theConnection = new SqlConnection();
        theConnection.ConnectionString = conString;

        // Command.
        theCommand = new SqlCommand();
        theCommand.Connection = theConnection;
        theCommand.CommandText = "select * from dbo.IPD_ConsumableGroup";
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

    public DataTable OperationNames()
    {
        // Connection.
        theConnection = new SqlConnection();
        theConnection.ConnectionString = conString;

        // Command.
        theCommand = new SqlCommand();
        theCommand.Connection = theConnection;
        theCommand.CommandText = "select distinct OperationID,OperationName from dbo.IPD_OperationDetails";
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

    public DataTable ServiceName(string ConGrId)
    {
        // Connection.
        theConnection = new SqlConnection();
        theConnection.ConnectionString = conString;

        // Command.
        theCommand = new SqlCommand();
        theCommand.Connection = theConnection;
        if (ConGrId == "0")
            theCommand.CommandText = "select * from dbo.IPD_ConsumableItems";
        else
            theCommand.CommandText = "select * from dbo.IPD_ConsumableItems  where ConGrId='" + ConGrId + "'";

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

    public bool CategoryFunction(int mode, string TemplateCategoryId, string CategoryName,string cocode)
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
                        if (mode == 1)
                        {

                            theCommand.CommandText = "INSERT INTO IPD_Service_Cons_TemplateCategory (compcode,CategoryName)  VALUES('"+cocode+"','" + CategoryName + "')";
                        }

                        if (mode == 2)
                        {
                            theCommand.CommandText = "update IPD_Service_Cons_TemplateCategory  set CategoryName='" + CategoryName + "' where TemplateCategoryId='" + TemplateCategoryId + "'and compcode='"+cocode+"'";
                        }

                        if (mode == 3)
                        {
                            theCommand.CommandText = "delete IPD_Service_Cons_TemplateCategory  where TemplateCategoryId='" + TemplateCategoryId + "'and compcode='"+cocode+"'";

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

    public bool NameFunction(int mode, string NameID, string TemplateCategoryId, string ServiceTemplateName, string CreatedBy, string CreatedDate, string ServiceCharge,string cocode)
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
                        if (mode == 1)
                        {

                            theCommand.CommandText = "INSERT INTO IPD_Service_Cons_Template(compcode,TemplateCategoryId,ServiceTemplateName,CreatedBy,CreatedDate,status,ServiceCharge)  VALUES('"+cocode+"','" + TemplateCategoryId + "','" + ServiceTemplateName + "','" + CreatedBy + "','" + CreatedDate + "',1,'" + ServiceCharge + "')";

                        }

                        if (mode == 2)
                        {
                            theCommand.CommandText = "update IPD_Service_Cons_Template  set  ServiceTemplateName='" + ServiceTemplateName + "',ServiceCharge='" + ServiceCharge + "' where NameID='" + NameID + "'and compcode='"+cocode+"'";

                        }

                        if (mode == 3)
                        {
                            theCommand.CommandText = "delete IPD_Service_Cons_Template  where NameID='" + NameID + "'and compcode='"+cocode+"'";

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

    public bool OperationTempSave(int mode, string NameID, string TemplateCategoryId, string ServiceTemplateName, string CreatedBy, string CreatedDate, string ServiceCharge,string cocode)
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
                        if (mode == 1)
                        {

                            theCommand.CommandText = "INSERT INTO IPD_Operation_Cons_Template(compcode,TemplateCategoryId,ServiceTemplateName,CreatedBy,CreatedDate,status,ServiceCharge)  VALUES('"+cocode+"','" + TemplateCategoryId + "','" + ServiceTemplateName + "','" + CreatedBy + "','" + CreatedDate + "',1,'" + 0 + "')";

                        }

                        if (mode == 2)
                        {
                            theCommand.CommandText = "update IPD_Operation_Cons_Template  set  ServiceTemplateName='" + ServiceTemplateName + "',ServiceCharge='" + 0 + "' where NameID='" + NameID + "'and compcode='"+cocode+"'";

                        }

                        if (mode == 3)
                        {
                            theCommand.CommandText = "delete IPD_Operation_Cons_Template  where TemplateCategoryId='" + NameID + "' delete from IPD_operation_Cons_TemplateMapping where nameid='" + NameID + "'and compcode='"+cocode+"'";

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