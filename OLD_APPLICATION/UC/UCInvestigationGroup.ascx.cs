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
public partial class UCInvestigationGroup : System.Web.UI.UserControl
{
    private SqlConnection theConnection;
    private SqlCommand theCommand;
    private SqlDataAdapter theAdapter;
    string conString = ConfigurationSettings.AppSettings["ConnectionString"].ToString();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
            BindGrid();
    }
    public void BindGrid()
    {
        string utp = Convert.ToString(ViewState["usrtp"]);
        string usracctp = Convert.ToString(ViewState["usracctp"]).Trim();
        string agcd = Convert.ToString(ViewState["agrcd"]);
        DataTable dt = new DataTable();
        theConnection = new SqlConnection();
        theConnection.ConnectionString = conString;
        theCommand = new SqlCommand();
        theCommand.Connection = theConnection;
        theCommand.CommandText = "Select ProfileCode,ProfileName From ph_Profilemaster where compcode='" + Session["CoCode"].ToString() + "'";
        theCommand.CommandType = CommandType.Text;
        theAdapter = new SqlDataAdapter();
        theAdapter.SelectCommand = theCommand;
        theAdapter.Fill(dt); 
        theConnection.Dispose();
        theCommand.Dispose();
        theAdapter.Dispose();
        DataGrid1.DataSource = dt;
        DataGrid1.DataBind();
    }

    protected void DataGrid1_PageIndexChanged(object source, DataGridPageChangedEventArgs e)
    {
        DataGrid1.CurrentPageIndex = e.NewPageIndex;
        BindGrid();
        DataGrid1.SelectedIndex = -1;
    }
    protected void cmdSelect_Click(object sender, EventArgs e)
    {
        int i = 0;
        DataGridItem dgi = default(DataGridItem);
        string sno = null;
        CheckBox chk = default(CheckBox);
        DataGrid1.CurrentPageIndex = 0;
        for (i = 0; i <= DataGrid1.Items.Count - 1; i++)
        {
            dgi = DataGrid1.Items[i];
            sno = Convert.ToString(DataGrid1.DataKeys[dgi.ItemIndex]);
            chk = (CheckBox)dgi.FindControl("chkselect");
            if (chk.Checked == false)
            {
                chk.Checked = true;
            }
        }
    }
    protected void cmdDeselect_Click(object sender, EventArgs e)
    {
        int i = 0;
        DataGridItem dgi = default(DataGridItem);
        string sno = null;
        CheckBox chk = default(CheckBox);
        DataGrid1.CurrentPageIndex = 0;
        for (i = 0; i <= DataGrid1.Items.Count - 1; i++)
        {
            dgi = DataGrid1.Items[i];
            sno = Convert.ToString(DataGrid1.DataKeys[dgi.ItemIndex]);
            chk = (CheckBox)dgi.FindControl("chkselect");
            if (chk.Checked == true)
            {
                chk.Checked = false;
            }
        }
    }
    public string GetSelectedCode()
    {
        string sno = string.Empty;
        System.Text.StringBuilder _selcd = new System.Text.StringBuilder();
        // string _selcd = string.Empty;
        int _rowcount = 0;
        for (_rowcount = 0; _rowcount <= DataGrid1.Items.Count - 1; _rowcount++)
        {
            if (((CheckBox)DataGrid1.Items[_rowcount].FindControl("chkselect")).Checked)
            {
                //Label lblCd = (Label)DataGrid1.Items[_rowcount].FindControl("lblcol1");
                //sno = DataGrid1.DataKeys(DataGrid1.Items(_rowcount).ItemIndex)
                // _selcd = Strings(lblCd.Text.Trim()) + "~" + _selcd;
                // _selcd.Append("'" + lblCd.Text.Trim() + "'" + ",");

                string SelectID = DataGrid1.DataKeys[_rowcount].ToString().Trim();
                _selcd.Append("'" + SelectID + "'" + ",");
            }
        }
        char[] period = { ',' };
        String str = _selcd.ToString().TrimEnd(period);
        //Session["Grdn"] = str;
        return str;
    }
    public string GetSelectedCodetild()
    {
        string _selcd = string.Empty;

        for (int _rowcount = 0; _rowcount <= DataGrid1.Items.Count - 1; _rowcount++)
        {
            CheckBox chk = (CheckBox)DataGrid1.Items[_rowcount].FindControl("chkSelect");
            if (chk.Checked == true)
            {
                string SelectID = DataGrid1.DataKeys[_rowcount].ToString().Trim();
                _selcd = SelectID + "~" + _selcd;
            }
        }
        return _selcd;
    }

    public string GetSelectedCode1()
    {
        string _selcd = string.Empty;
        string sno = string.Empty;
        int _rowcount = 0;
        for (_rowcount = 0; _rowcount <= DataGrid1.Items.Count - 1; _rowcount++)
        {
            if (((CheckBox)DataGrid1.Items[_rowcount].FindControl("chkselect")).Checked)
            {
                sno = "'" + Convert.ToString(DataGrid1.DataKeys[_rowcount]).Trim() + "'";
                if (!string.IsNullOrEmpty(_selcd))
                {
                    _selcd = _selcd + ",";
                }
                _selcd = _selcd + sno;
            }
        }
        return _selcd;
    }
    public string DDLItemFilter
    {
        get { return ViewState["P_DDLItemFilter"].ToString(); }
        set { ViewState["P_DDLItemFilter"] = value; }
    }

}