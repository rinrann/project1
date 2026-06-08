using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using Microsoft.VisualBasic;
using System.Text;
using System.Data;
using System.Configuration;

public partial class UC_UCServiceAutoSearch : System.Web.UI.UserControl
{
    private SqlConnection theConnection;
    private SqlCommand theCommand;
    private SqlDataAdapter theAdapter;
    string conString = ConfigurationSettings.AppSettings["ConnectionString"].ToString();
    protected void Page_Load(object sender, EventArgs e)
    {
        initializeDDlDataList();
    }

    private void initializeDDlDataList()
    {
    }
    protected void ImgBut_Click(object sender, ImageClickEventArgs e)
    {
        Bind_Grid();
    }

    private void Bind_Grid()
    {
        DataTable dt = new DataTable();
        theConnection = new SqlConnection();
        theConnection.ConnectionString = conString;
        theCommand = new SqlCommand();
        theCommand.Connection = theConnection;
        theCommand.CommandText = "Select TestId,TestName From ph_TestMaster where compcode='" + Session["CoCode"].ToString() + "'";
        theCommand.CommandType = CommandType.Text;
        theAdapter = new SqlDataAdapter();
        theAdapter.SelectCommand = theCommand;
        theAdapter.Fill(dt);
        theConnection.Dispose();
        theCommand.Dispose();
        theAdapter.Dispose();
    }
}