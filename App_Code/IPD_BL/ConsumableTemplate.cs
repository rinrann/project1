using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using System.Data;

/// <summary>
/// Summary description for ConsumableTemplate
/// </summary>
public class ConsumableTemplate
{
    public ConsumableTemplate(string con)
    {
        conString = con;
    }


    private string conString;
    private SqlConnection theConnection;
    private SqlCommand theCommand;
    private SqlDataAdapter theAdapter;
    private DataTable hospitalTable;

    public DataTable GridFill()
    {
        // Connection.
        theConnection = new SqlConnection();
        theConnection.ConnectionString = conString;

        // Command.
        theCommand = new SqlCommand();
        theCommand.Connection = theConnection;
        theCommand.CommandText = "select * from dbo.IPD_ConsumableTemplateCategory";
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

    public DataTable FillMap(string Id)
    {
        // Connection.
        theConnection = new SqlConnection();
        theConnection.ConnectionString = conString;

        // Command.
        theCommand = new SqlCommand();
        theCommand.Connection = theConnection;
        theCommand.CommandText = "select * from dbo.IPD_ConsumableTemplateMapping  where  ConsumableNameId='" + Id + "'";
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

    public DataTable ConsumableGroup()
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


    public DataTable ConsumableItems(string Group)
    {
        // Connection.
        theConnection = new SqlConnection();
        theConnection.ConnectionString = conString;

        // Command.
        theCommand = new SqlCommand();
        theCommand.Connection = theConnection;
        if (Group == "0")
            theCommand.CommandText = "select * from dbo.IPD_ConsumableItems";
        else
            theCommand.CommandText = "select * from dbo.IPD_ConsumableItems  where ConGrId='" + Group + "'";

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



    public DataTable GridFillName()
    {
        // Connection.
        theConnection = new SqlConnection();
        theConnection.ConnectionString = conString;

        // Command.
        theCommand = new SqlCommand();
        theCommand.Connection = theConnection;
        theCommand.CommandText = "select * from dbo.IPD_ConsumableTemplateCategory c,IPD_ConsumableTemplateName n where c.TemplateCategoryId=n.CategoryId";
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

    public bool CategoryFunction(int mode, string TemplateCategoryId, string CategoryName)
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

                            theCommand.CommandText = "INSERT INTO IPD_ConsumableTemplateCategory (CategoryName)  VALUES('" + CategoryName + "')";
                            theCommand.Transaction = tran as SqlTransaction;
                        }

                        if (mode == 2)
                        {
                            theCommand.CommandText = "update IPD_ConsumableTemplateCategory  set CategoryName='" + CategoryName + "' where TemplateCategoryId='" + TemplateCategoryId + "'";
                            theCommand.Transaction = tran as SqlTransaction;
                        }

                        if (mode == 3)
                        {
                            theCommand.CommandText = "delete IPD_ConsumableTemplateCategory  where TemplateCategoryId='" + TemplateCategoryId + "'";
                            theCommand.Transaction = tran as SqlTransaction;
                        }
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
    }

    public bool NameFunction(int mode, string NameID, string CategoryId, string ConsumableTemplateName, string CreatedBy, string CreatedDate)
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

                            theCommand.CommandText = "INSERT INTO IPD_ConsumableTemplateName (CategoryId,ConsumableTemplateName,CreatedBy,CreatedDate,status)  VALUES('" + CategoryId + "','" + ConsumableTemplateName + "','" + CreatedBy + "','" + CreatedDate + "',1)";
                            theCommand.CommandType = CommandType.Text;
                        }

                        if (mode == 2)
                        {
                            theCommand.CommandText = "update IPD_ConsumableTemplateName  set ConsumableTemplateName='" + ConsumableTemplateName + "' where NameID='" + NameID + "'";
                            theCommand.Transaction = tran as SqlTransaction;
                        }

                        if (mode == 3)
                        {
                            theCommand.CommandText = "delete IPD_ConsumableTemplateName  where NameID='" + NameID + "'";
                            theCommand.Transaction = tran as SqlTransaction;
                        }
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
            theCommand.Dispose();
            theConnection.Dispose();
        }
    }



    public bool TemplateFillFunction(int mode, string ConTemplateName, string ConsumableGrId, string ConsumableNameId, string ConsumableItemId, string ActualQty, string BillQty)
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

                            theCommand.CommandText = "INSERT INTO IPD_ConsumableTemplateMapping (ConsumableGrId,ConsumableNameId,ConsumableItemId,ActualQty,BillQty)  VALUES('" + ConsumableGrId + "',(select MAX(NameID) from IPD_ConsumableTemplateName where ConsumableTemplateName='" + ConTemplateName + "'),'" + ConsumableItemId + "','" + ActualQty + "','" + BillQty + "')";
                            theCommand.Transaction = tran as SqlTransaction;
                        }

                        if (mode == 2)
                        {
                            theCommand.CommandText = "delete IPD_ConsumableTemplateMapping  where ConsumableNameId='" + ConsumableNameId + "'";
                            theCommand.Transaction = tran as SqlTransaction;
                        }
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
            theCommand.Dispose();
            theConnection.Dispose();
        }
    }
}