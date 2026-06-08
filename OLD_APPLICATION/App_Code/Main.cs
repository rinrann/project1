using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using System.Data;

/// <summary>
/// Summary description for Main
/// </summary>
/// 
public enum checkAccessType
{
    InsertAction,
    UpdateAction,
    DeleteAction,
    ViewAction
}
public class Main
{
    public string constring;
    public SqlConnection theConnection;
    public SqlCommand theCommand;
    public SqlDataAdapter theAdapter;
    public DataTable useraccessinfo;

    public Main(string con)
	{
		//
		// TODO: Add constructor logic here
		//
        constring = con;
	}


    public bool checkaccss(string compcode,string userRoleId,string menuname, checkAccessType accesstype)
    {
        DataTable dt = new DataTable();
        
        // Connection.
        theConnection = new SqlConnection();
        theConnection.ConnectionString = constring;

        // Command.
        theCommand = new SqlCommand();
        theCommand.Connection = theConnection;
        theCommand.CommandText = "select isNull(ViewAction,'0')ViewAction,isNull(InsertAction,'0')InsertAction,isNull(UpdateAction,'0')UpdateAction,isNull(DeleteAction,'0')DeleteAction from GN_RoleWisAccess,GN_MenuDetails where GN_MenuDetails.compcode=GN_RoleWisAccess.compcode and GN_MenuDetails.MenuID=GN_RoleWisAccess.MenuId and GN_RoleWisAccess.compcode='" + compcode + "' and GN_RoleWisAccess.UserRoleId='" + userRoleId + "' and GN_MenuDetails.MenuName='" + menuname + "' ";
        theCommand.CommandType = CommandType.Text;

        // Adapter.
        theAdapter = new SqlDataAdapter();
        theAdapter.SelectCommand = theCommand;

        // Datatable.
        useraccessinfo = new DataTable();
        theAdapter.Fill(useraccessinfo); // Fill data into data table.

        // Clean up.
        theConnection.Dispose();
        theCommand.Dispose();
        theAdapter.Dispose();
        if (useraccessinfo.Rows.Count <= 0)
        {
            return false;
        }
        else
        {
            if (accesstype == checkAccessType.ViewAction)
            {
                if (useraccessinfo.Rows[0]["ViewAction"].ToString().Trim() == "1")
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            else if (accesstype == checkAccessType.InsertAction)
            {
                if (useraccessinfo.Rows[0]["InsertAction"].ToString().Trim() == "1")
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            else if (accesstype == checkAccessType.UpdateAction)
            {
                if (useraccessinfo.Rows[0]["UpdateAction"].ToString().Trim() == "1")
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            else if (accesstype == checkAccessType.DeleteAction)
            {
                if (useraccessinfo.Rows[0]["DeleteAction"].ToString().Trim() == "1")
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            else
            {
                return false;
            }
        }
    }
}