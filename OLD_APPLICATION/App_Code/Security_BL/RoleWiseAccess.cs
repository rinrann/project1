using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using System.Data;

/// <summary>
/// Summary description for RoleWiseAccess123
/// </summary>
public class RoleWiseAccess
{
    public RoleWiseAccess(string con)
    {
        conString = con;
    }


    private string conString;
    private SqlConnection theConnection;
    private SqlCommand theCommand;
    private SqlDataAdapter theAdapter;
    private DataTable RoleWiseTable;
    public DataTable GetAllRole(string module, string submodule)
    {
        if (module == "" || module == "0")
            module = "null";

        if (submodule == "" || submodule == "0")
            submodule = "null";

        // Connection.
        theConnection = new SqlConnection();
        theConnection.ConnectionString = conString;

        // Command.
        theCommand = new SqlCommand();
        theCommand.Connection = theConnection;
        theCommand.CommandText = " exec [sp_RoleWiseAccessDetails]  " + module + "," + submodule + "";
        theCommand.CommandType = CommandType.Text;

        // Adapter.
        theAdapter = new SqlDataAdapter();
        theAdapter.SelectCommand = theCommand;

        // Datatable.
        RoleWiseTable = new DataTable();
        theAdapter.Fill(RoleWiseTable); // Fill data into data table.

        // Clean up.
        theConnection.Dispose();
        theCommand.Dispose();

        return RoleWiseTable;
    }
    public DataTable getacces(string module, string submodule, string userid)
    {
        theConnection = new SqlConnection();
        theConnection.ConnectionString = conString;

        // Command.
        string Compcode = HttpContext.Current.Session["CoCode"].ToString().Trim();
        theCommand = new SqlCommand();
        theCommand.Connection = theConnection;
        theCommand.CommandText = " SELECT T1.ModuleID,T1.ModuleName,T2.SubModuleID,T2.SubModuleName,T3.MenuID,T3.MenuName,ISNULL(T4.ViewAction,0) Views,ISNULL(T4.InsertAction,0) Inserts,ISNULL(T4.UpdateAction,0) Updates,ISNULL(T4.DeleteAction,0) Deletes FROM GN_ModuleDetails T1 left join GN_SubModuleDetails T2 on T1.ModuleID=T2.ModuleID left join GN_MenuDetails T3 on T2.SubModuleID=T3.SubModuleID  left join GN_RoleWisAccess T4 on T3.MenuID=T4.MenuID" +
        " WHERE   T1.ModuleID like  COALESCE(" + module + ", T1.ModuleID)" +
        " AND T2.SubModuleID like  COALESCE(" + submodule + ", T2.SubModuleID) and T4.UserRoleID=" + userid + "";
        theCommand.CommandType = CommandType.Text;

        // Adapter.
        theAdapter = new SqlDataAdapter();
        theAdapter.SelectCommand = theCommand;

        // Datatable.
        RoleWiseTable = new DataTable();
        theAdapter.Fill(RoleWiseTable); // Fill data into data table.

        // Clean up.
        theConnection.Dispose();
        theCommand.Dispose();


        return RoleWiseTable;
    }

    public int GetRoleID()
    {
        //Connection.
        theConnection = new SqlConnection();
        theConnection.ConnectionString = conString;

        //Command.
        theAdapter = new SqlDataAdapter("SELECT max(RoleID) as RoleID FROM GN_RoleWisAccess", theConnection);
        DataSet ds = new DataSet();
        theAdapter.Fill(ds);
        int RoleID = 0;
        if (ds.Tables[0].Rows[0]["RoleID"] == DBNull.Value)
        {
            RoleID = 1;
        }
        else
        {
            RoleID = Convert.ToInt32(ds.Tables[0].Rows[0]["RoleID"]) + 1;
        }
        return RoleID;
    }
    public DataTable DropdownID1()
    {
        // Connection.
        theConnection = new SqlConnection();
        theConnection.ConnectionString = conString;

        // Command.
        string Compcode = HttpContext.Current.Session["CoCode"].ToString().Trim();
        theCommand = new SqlCommand();
        theCommand.Connection = theConnection;
        theCommand.CommandText = "select UserRoleID,UserRoleName from  GN_UserRole where Status=1 and compcode='" + Compcode + "'";
        theCommand.CommandType = CommandType.Text;

        // Adapter.
        theAdapter = new SqlDataAdapter();
        theAdapter.SelectCommand = theCommand;

        // Datatable.
        RoleWiseTable = new DataTable();
        theAdapter.Fill(RoleWiseTable); // Fill data into data table.

        // Clean up.
        theConnection.Dispose();
        theCommand.Dispose();


        return RoleWiseTable;
    }
    public DataTable DropdownModule()
    {
        // Connection.
        theConnection = new SqlConnection();
        theConnection.ConnectionString = conString;

        // Command.
        string Compcode = HttpContext.Current.Session["CoCode"].ToString().Trim();
        theCommand = new SqlCommand();
        theCommand.Connection = theConnection;
        theCommand.CommandText = "select ModuleID,ModuleName from  GN_ModuleDetails where Status=1 and Compcode='" + Compcode + "'";
        theCommand.CommandType = CommandType.Text;

        // Adapter.
        theAdapter = new SqlDataAdapter();
        theAdapter.SelectCommand = theCommand;

        // Datatable.
        RoleWiseTable = new DataTable();
        theAdapter.Fill(RoleWiseTable); // Fill data into data table.

        // Clean up.
        theConnection.Dispose();
        theCommand.Dispose();


        return RoleWiseTable;
    }
    public DataTable DropdownSUBModule(string moduleID)
    {
        // Connection.
        theConnection = new SqlConnection();
        theConnection.ConnectionString = conString;

        // Command.
        string Compcode = HttpContext.Current.Session["CoCode"].ToString().Trim();
        theCommand = new SqlCommand();
        theCommand.Connection = theConnection;
        theCommand.CommandText = "select SubModuleID,SubModuleName from  GN_SubModuleDetails  where Status=1 and ModuleID='" + moduleID + "' and Compcode='" + Compcode + "'";
        theCommand.CommandType = CommandType.Text;

        // Adapter.
        theAdapter = new SqlDataAdapter();
        theAdapter.SelectCommand = theCommand;

        // Datatable.
        RoleWiseTable = new DataTable();
        theAdapter.Fill(RoleWiseTable); // Fill data into data table.

        // Clean up.
        theConnection.Dispose();
        theCommand.Dispose();


        return RoleWiseTable;
    }

    public DataTable DropdownID3()
    {
        // Connection.
        theConnection = new SqlConnection();
        theConnection.ConnectionString = conString;

        // Command.
        string Compcode = HttpContext.Current.Session["CoCode"].ToString().Trim();
        theCommand = new SqlCommand();
        theCommand.Connection = theConnection;
        theCommand.CommandText = "select ActionID,ActionName from  GN_UserAction where Compcode='" + Compcode + "'";
        theCommand.CommandType = CommandType.Text;

        // Adapter.
        theAdapter = new SqlDataAdapter();
        theAdapter.SelectCommand = theCommand;

        // Datatable.
        RoleWiseTable = new DataTable();
        theAdapter.Fill(RoleWiseTable); // Fill data into data table.

        // Clean up.
        theConnection.Dispose();
        theCommand.Dispose();


        return RoleWiseTable;
    }
    public bool InsertRoleWiseAccess(string UserRoleID, string ModuleID, string SubModuleID, string MenuID, string ViewAction, string InsertAction, string UpdateAction, string DeleteAction)
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
            string Compcode = HttpContext.Current.Session["CoCode"].ToString().Trim();
            theCommand = new SqlCommand();
            theCommand.Connection = theConnection;
            string sql;
            sql = "select *  from  GN_RoleWisAccess where Compcode='" + Compcode + "' and UserRoleID='" + UserRoleID + "' and  ModuleID='" + ModuleID + "' and SubModuleID='" + SubModuleID + "' and MenuID='" + MenuID + "'";
            SqlDataAdapter da = new SqlDataAdapter(sql, theConnection);
            DataTable dt = new DataTable();
            da.Fill(dt);
            if (dt.Rows.Count > 0)
            {
                theCommand.CommandText = "delete  GN_RoleWisAccess where Compcode='" + Compcode + "' and UserRoleID='" + UserRoleID + "' and  ModuleID='" + ModuleID + "' and SubModuleID='" + SubModuleID + "' and MenuID='" + MenuID + "' ";
                theCommand.CommandType = CommandType.Text;
                theCommand.ExecuteNonQuery();
            }



            theCommand.CommandText = "INSERT INTO GN_RoleWisAccess(Compcode,UserRoleID, ModuleID, SubModuleID, MenuID, ViewAction,InsertAction,UpdateAction,DeleteAction) VALUES ('" + Compcode + "','" + UserRoleID + "', '" + ModuleID + "', '" + SubModuleID + "', '" + MenuID + "', '" + ViewAction + "', '" + InsertAction + "', '" + UpdateAction + "', '" + DeleteAction + "')";
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
    public bool UpdateRoleWiseAccess(string RoleID, string UserRoleID, string ModuleID, string SubModuleID, string MenuID, string ActionID)
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
            string Compcode = HttpContext.Current.Session["CoCode"].ToString().Trim();
            theCommand = new SqlCommand();
            theCommand.Connection = theConnection;
            theCommand.CommandText = "Update GN_RoleWisAccess set  UserRoleID='" + UserRoleID + "', ModuleID='" + ModuleID + "', SubModuleID='" + SubModuleID + "',MenuID='" + MenuID + "', ActionID='" + ActionID + "' where RoleID = '" + RoleID + "' and Compcode='" + Compcode + "'";
            theCommand.CommandType = CommandType.Text;

            theCommand.ExecuteNonQuery(); // Execute update query.
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
    public bool DeleteRoleWiseAccess(int MenuID)
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
            string Compcode = HttpContext.Current.Session["CoCode"].ToString().Trim();
            theCommand = new SqlCommand();
            theCommand.Connection = theConnection;
            theCommand.CommandText = "DELETE FROM GN_RoleWisAccess WHERE RoleID = " + RoleID + " and Compcode='" + Compcode + "'";
            theCommand.CommandType = CommandType.Text;

            theCommand.ExecuteNonQuery(); // Execute Delete query.
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


    public string RoleID { get; set; }
}