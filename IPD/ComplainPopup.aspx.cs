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
using System.Web.Security;

public partial class IPD_ComplainPopup : System.Web.UI.Page
{
    PatientsDetails thedia = new PatientsDetails(ConfigurationSettings.AppSettings["ConnectionString"].ToString());
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            GridFill();
            Session["SelectedRecords"] = null;
        }
    }
    protected void Button2_Click(object sender, EventArgs e)
    {
        GridFill();
    }
     
    private void GridFill()
    {
        GridView_popup.DataSource = thedia.DropdownComplain(Session["CoCode"].ToString().Trim());
        GridView_popup.DataBind();

    }
 
    protected void Button3_Click(object sender, EventArgs e)
    {
         HiddenField1.Value="";
        HiddenField2.Value="";
        foreach (GridViewRow row in GridView_popup.Rows)
        {
            CheckBox chk = (CheckBox)row.FindControl("CheckBox1");

            Label lblId = (Label)row.FindControl("lblId");
            Label lblName = (Label)row.FindControl("lblName");

            if (chk.Checked)
            {
                if (HiddenField1.Value == "" && HiddenField2.Value == "")
                {
                    HiddenField1.Value = lblId.Text;
                    HiddenField2.Value = lblName.Text;
                }
                else
                {
                    HiddenField1.Value = HiddenField1.Value + "," + lblId.Text;
                    HiddenField2.Value = HiddenField2.Value + "," + lblName.Text;
                }
            }
        }        
        ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "MyFun1", "CloseDialog();", true);

    }
   
}