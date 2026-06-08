using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Data;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Configuration;
using System.Collections;
using System.Drawing;

public partial class DayCare_DayCareCalender : System.Web.UI.Page
{
    Main Objmain = new Main(ConfigurationSettings.AppSettings["ConnectionString"].ToString());

    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["userName"] == null)
        {
            Response.Redirect("../LoginPage.aspx");
        }
        if (Objmain.checkaccss(Session["CoCode"].ToString(), Session["UserRoleID"].ToString().Trim(), "DAYCARE CALENDER", checkAccessType.ViewAction) == false)
        {
            Response.Redirect("../AccessDenied.aspx");
        }
        Page.Title = "DayCare Calender";
        cal1.DataSource = GetEventData(Session["CoCode"].ToString().Trim());
        cal1.DayField = "MeetingDate";
        cal1.VisibleDate = System.DateTime.Now;
       //cal1.VisibleDate =(System.DateTime.Now).ToString("MM/dd/yyyy");
    }

    public DataTable GetEventData(string compcode)
    {

        string conString = ConfigurationManager.AppSettings["ConnectionString"].ToString();
        SqlConnection theConnection;
        SqlCommand theCommand;
        SqlDataAdapter theAdapter;
        DataTable hospitalTable;

        // Connection.
        theConnection = new SqlConnection();
        theConnection.ConnectionString = conString;

        // Command.
        theCommand = new SqlCommand();
        theCommand.Connection = theConnection;
        theCommand.CommandText = "exec dbo.sp_DC_CalenderAppointment '"+compcode+"'";
        theCommand.CommandType = CommandType.Text;

        // Adapter.
        theAdapter = new SqlDataAdapter();
        theAdapter.SelectCommand = theCommand;

        // Datatable.
        hospitalTable = new DataTable();
        theAdapter.Fill(hospitalTable); // Fill data into data table.

        // Clean up.
        theConnection.Dispose();   theCommand.Dispose();    theAdapter.Dispose();
        theCommand.Dispose();
        

        return hospitalTable;
    }
    
}