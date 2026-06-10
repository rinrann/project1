using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Data;
using System.Web.UI.HtmlControls;
using System.Configuration;
using System.Collections;
using System.Drawing;
using System.Globalization;

public partial class Assignment_LaprosopicNote : System.Web.UI.Page
{
    LaprosopicNoteClass objLapNote = new LaprosopicNoteClass(ConfigurationSettings.AppSettings["ConnectionString"].ToString());
    int flag = 1;
    string a;
    protected void Page_Load(object sender, EventArgs e)
    {
        Page.Title = "Laprosopic Note";
        if (!IsPostBack)
        {
            DropDownFill();
        }

    }

    public void DropDownFill()
    {

        ddlOTType.Items.Clear();
        this.ddlOTType.DataSource = objLapNote.DropdownOprationType();
        this.ddlOTType.DataTextField = "OperationTypeName";
        this.ddlOTType.DataValueField = "OperationTypeID";
        this.ddlOTType.DataBind();
        this.ddlOTType.Items.Insert(0, new ListItem("-Select-", "0"));
    }

    public void TypeNameFill(string value)
    {
        ddlOTName.Items.Clear();
        ddlOTName.DataSource = objLapNote.DropdownOperationName(value);
        ddlOTName.DataTextField = "OperationName";
        ddlOTName.DataValueField = "OperationID";
        ddlOTName.DataBind();
        ddlOTName.Items.Insert(0, new ListItem("-Select-", "0"));
    }
    protected void ddlOTType_SelectedIndexChanged(object sender, EventArgs e)
    {
        TypeNameFill(ddlOTType.SelectedValue);

       
    }
    protected void btnSubmit_Click(object sender, EventArgs e)
    {



        //if (objLapNote.Insert_Update_LaprosopicNote(2, null, Session["OTPReg"].ToString(), txtLapNote.Text, null, null) == true)
        //{

        //}

        //DataTable dt = objLapNote.LapNote();
        //if (dt.Rows.Count > 0)
        //{

        //    for (int i = 0; i < dt.Rows.Count; i++)
        //    {
        //        if (dt.Rows[i]["OperationTypeId"].ToString() == ddlOTType.SelectedValue && dt.Rows[i]["OperationId"].ToString() == ddlOTName.SelectedValue)
        //        {
        //            flag = 1;
        //            break;
        //        }
        //        else
        //        {
        //            flag = 0;

        //        }
        //    }

        //    if (flag == 0)
        //    {
        //        objLapNote.Insert_Update_LaprosopicNote(1, null, null, txtLapNote.Text, ddlOTType.SelectedValue, ddlOTName.SelectedValue);
        //    }

        //}
        //else
        //{
        //    objLapNote.Insert_Update_LaprosopicNote(1, null, null, txtLapNote.Text, ddlOTType.SelectedValue, ddlOTName.SelectedValue);
        //}





        objLapNote.Insert_Update_LaprosopicNote(txtLapNote.Text, ddlOTType.SelectedValue, ddlOTName.SelectedValue, Session["OTPReg"].ToString());


        ddlOTType.SelectedIndex = 0;
        ddlOTName.SelectedIndex = 0;
        txtLapNote.Text = "";
        

        ScriptManager.RegisterStartupScript(this, this.GetType(), "msg", "alert('Inserted  Successfully !');", true);




    }
    protected void ddlOTName_SelectedIndexChanged(object sender, EventArgs e)
    {
        DataTable dt = objLapNote.LapNote(ddlOTType.SelectedValue,ddlOTName.SelectedValue);
        if (dt.Rows.Count > 0)
        {
            txtLapNote.Text = dt.Rows[0]["LaproscopicNote"].ToString();
        }
        else
        {
            txtLapNote.Text = "";
        }

    }
}