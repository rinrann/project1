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

public partial class Pathology_TestPopupMultiple : System.Web.UI.Page
{
    PH_TestPopupMultiple thedia = new PH_TestPopupMultiple(ConfigurationSettings.AppSettings["ConnectionString"].ToString());
    PH_PatientRequisition thereq = new PH_PatientRequisition(ConfigurationSettings.AppSettings["ConnectionString"].ToString());
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            DropDownFill();
            GridFill();
            if (Session["ReqNo"] != null)
            {
                string reqno = Session["ReqNo"].ToString();/////
                string test = "";
                DataTable dt = thedia.GetExistTestDetails(Session["CoCode"].ToString().Trim(), Session["YearCode"].ToString().Trim(), reqno, test);
                GridView1.DataSource = dt;
                GridView1.DataBind();
                if (dt.Rows.Count > 0)
                {
                    txttestdate.Text = dt.Rows[0]["TestDt"].ToString();
                    txtdeldate.Text = dt.Rows[0]["DeliveryDt"].ToString();
                }
                else
                {
                    txttestdate.Text = DateTime.Now.ToString("dd/MM/yyyy");
                    txtdeldate.Text = DateTime.Now.AddDays(1).ToString("dd/MM/yyyy");
                }
            }
            else
            {
                txttestdate.Text = DateTime.Now.ToString("dd/MM/yyyy");
                txtdeldate.Text = DateTime.Now.AddDays(1).ToString("dd/MM/yyyy");
            }
        }

    }
    protected void Button2_Click(object sender, EventArgs e)
    {
        GridFill();
        DataTable dt = (DataTable)Session["asaaa"];
    }

    public void DropDownFill()
    {
        DropDownList1.DataSource = thedia.DropDownFill(Session["CoCode"].ToString().Trim());
        DropDownList1.DataTextField = "DeptName";
        DropDownList1.DataValueField = "DeptCode";
        DropDownList1.DataBind();
        DropDownList1.Items.Insert(0, new ListItem("--Select--", "0"));

        string testType = Session["TestType"].ToString();
        DataTable dt = thereq.getTestGroup(Session["CoCode"].ToString().Trim(), testType);
        ddltestGroup.DataSource = dt;
        ddltestGroup.DataTextField = "TestName";
        ddltestGroup.DataValueField = "ProfileCode";
        ddltestGroup.DataBind();

        ddltestGroup.SelectedValue = Session["TestGrp"].ToString().Trim();

    }
    private void GridFill()
    {
        GridView_popup.DataSource = thedia.GridFill(Session["CoCode"].ToString().Trim(), txtname.Text, txtcode.Text, DropDownList1.SelectedValue, ddltestGroup.SelectedValue);
        GridView_popup.DataBind();

    }

    protected void Button4_Click(object sender, EventArgs e)
    {
        DataTable dt = new DataTable();
        DataRow row = dt.NewRow();

        dt.Columns.Add("TestId", typeof(string));
        dt.Columns.Add("TestReqNo", typeof(string));
        dt.Columns.Add("TestName", typeof(string));
        dt.Columns.Add("cost", typeof(decimal));
        dt.Columns.Add("Date", typeof(string));
        dt.Columns.Add("Time", typeof(string));
        dt.Columns.Add("DeliveryDate", typeof(string));
        dt.Columns.Add("Remarks", typeof(string));
        dt.Columns.Add("consultant", typeof(string));
        dt.Columns.Add("consultantname", typeof(string));


        if (GridView1.Rows.Count > 0)
        {
            for (int i = 0; i < GridView1.Rows.Count; i++)
            {
                Label lblName = (Label)GridView1.Rows[i].FindControl("lblName");
                Label lblid = (Label)GridView1.Rows[i].FindControl("lblid");
                Label lblcost = (Label)GridView1.Rows[i].FindControl("lblcost");
                Label lblDate = (Label)GridView1.Rows[i].FindControl("lblDate");
                Label lblTime = (Label)GridView1.Rows[i].FindControl("lblTime");
                Label lblTestReqNo = (Label)GridView1.Rows[i].FindControl("lblTestReqNo");
                Label lblRemarks = (Label)GridView1.Rows[i].FindControl("lblRemarks");
                Label lbldvdate = (Label)GridView1.Rows[i].FindControl("lbldvdate");
                Label lbconsultant = (Label)GridView1.Rows[i].FindControl("lbconsultant");
                Label lbconsultantname = (Label)GridView1.Rows[i].FindControl("lbconsultantname");

                row["TestId"] = lblid.Text;
                row["TestName"] = lblName.Text;
                row["Cost"] = Convert.ToDouble(lblcost.Text);
                row["Date"] = lblDate.Text;
                row["Time"] = lblTime.Text;
                row["DeliveryDate"] = lbldvdate.Text;
                row["TestReqNo"] = lblTestReqNo.Text;
                row["Remarks"] = lblRemarks.Text;
                row["consultant"] = lbconsultant.Text;
                row["consultantname"] = lbconsultantname.Text;
                dt.Rows.Add(row);
                row = dt.NewRow();
            }
        }


        for (int i = 0; i < GridView_popup.Rows.Count; i++)
        {
            CheckBox chk = (CheckBox)GridView_popup.Rows[i].Cells[1].FindControl("CheckBox1");
            Label lblName = (Label)GridView_popup.Rows[i].FindControl("lblName");
            Label lblid = (Label)GridView_popup.Rows[i].FindControl("lblid");
            Label lblcost = (Label)GridView_popup.Rows[i].FindControl("lblcost");
            DropDownList ddlconsult = (DropDownList)GridView_popup.Rows[i].FindControl("ddlconsult");

            if (chk.Checked)
            {
                row["TestId"] = lblid.Text;
                row["TestName"] = lblName.Text;
                row["Cost"] = Convert.ToDouble(lblcost.Text);
                row["Date"] = txttestdate.Text;
                row["Time"] = DateTime.Now.ToShortTimeString();
                row["DeliveryDate"] = txtdeldate.Text;
                row["consultant"] = ddlconsult.SelectedValue;
                row["consultantname"] = ddlconsult.SelectedItem;
                dt.Rows.Add(row);
                row = dt.NewRow();
                chk.Checked = false;
            }
        }

        GridView1.DataSource = dt;
        GridView1.DataBind();
        Session["CurrentTable"] = dt;
    }


    public void sessioninitialize()
    {
        DataTable dt = new DataTable();
        DataRow row1 = dt.NewRow();

        dt.Columns.Add("SerialNo", typeof(string));
        dt.Columns.Add("TestId", typeof(string));
        dt.Columns.Add("TestReqNo", typeof(string));
        dt.Columns.Add("TestName", typeof(string));
        dt.Columns.Add("cost", typeof(decimal));
        dt.Columns.Add("Date", typeof(string));
        dt.Columns.Add("Time", typeof(string));
        dt.Columns.Add("DeliveryDate", typeof(string));
        dt.Columns.Add("Remarks", typeof(string));
        dt.Columns.Add("consultant", typeof(string));
        dt.Columns.Add("consultantname", typeof(string));


        if (GridView1.Rows.Count > 0)
        {
            for (int i = 0; i < GridView1.Rows.Count; i++)
            {
                Label lblSerial = (Label)GridView1.Rows[i].FindControl("lblSerial");
                Label lblid = (Label)GridView1.Rows[i].FindControl("lblid");
                Label lblTestReqNo = (Label)GridView1.Rows[i].FindControl("lblTestReqNo");
                Label lblName = (Label)GridView1.Rows[i].FindControl("lblName");
                Label lblcost = (Label)GridView1.Rows[i].FindControl("lblcost");
                Label lblDate = (Label)GridView1.Rows[i].FindControl("lblDate");
                Label lblTime = (Label)GridView1.Rows[i].FindControl("lblTime");
                Label lbldvdate = (Label)GridView1.Rows[i].FindControl("lbldvdate");
                Label lblRemarks = (Label)GridView1.Rows[i].FindControl("lblRemarks");
                Label lbconsultant = (Label)GridView1.Rows[i].FindControl("lbconsultant");
                Label lbconsultantname = (Label)GridView1.Rows[i].FindControl("lbconsultantname");

                row1["SerialNo"] = lblSerial.Text;
                row1["TestId"] = lblid.Text;
                row1["TestReqNo"] = lblTestReqNo.Text;
                row1["TestName"] = lblName.Text;
                row1["Cost"] = Convert.ToDouble(lblcost.Text);
                row1["Date"] = lblDate.Text;
                row1["Time"] = lblTime.Text;
                row1["DeliveryDate"] = lbldvdate.Text;
                row1["Remarks"] = lblRemarks.Text;
                row1["consultant"] = lbconsultant.Text;
                row1["consultantname"] = lbconsultantname.Text;

                dt.Rows.Add(row1);
                row1 = dt.NewRow();
            }
        }

        Session["CurrentTable"] = dt;
    }
    protected void Button3_Click(object sender, EventArgs e)
    {

        sessioninitialize();
        HiddenField1.Value = ""; HiddenField2.Value = ""; HiddenField4.Value = "";
        double total = 0.00;
        double total1 = 0.00;
        double totdue = 0.00;
        double newcost = 0.00;
        foreach (GridViewRow row in GridView1.Rows)
        {
            Label lblid = (Label)row.FindControl("lblid");
            Label lblName = (Label)row.FindControl("lblName");
            Label lblcost = (Label)row.FindControl("lblcost");
            Label lbconsultant = (Label)row.FindControl("lbconsultant");
            Label lbconsultantname = (Label)row.FindControl("lbconsultantname");
            HiddenField3.Value = total.ToString();
            total = total + Convert.ToDouble(lblcost.Text);

            if (HiddenField1.Value == "")
            {
                HiddenField1.Value = lblid.Text;
                HiddenField2.Value = lblName.Text;
                HiddenField4.Value = lbconsultant.Text;
                HiddenField5.Value = lbconsultantname.Text;
            }
            else
            {
                HiddenField1.Value = HiddenField1.Value + "," + lblid.Text;
                HiddenField2.Value = HiddenField2.Value + "," + lblName.Text;
                HiddenField4.Value = HiddenField4.Value + "," + lbconsultant.Text;
                HiddenField5.Value = HiddenField5.Value + "," + lbconsultantname.Text;
            }

        }

        //total1=
        //string parval = Session["PrvVal"].ToString();
        newcost = (total - (Session["PrvVal"].ToString() == "" ? 0 : double.Parse(Session["PrvVal"].ToString())));
        totdue = (Session["PrvVal"].ToString() == "" ? 0 : double.Parse(Session["PrvVal"].ToString())) + newcost;
        HiddenField3.Value = newcost.ToString();
        HiddenField2.Value = HiddenField2.Value + "#" + total + "#" + totdue;
        ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "MyFun1", "CloseDialog();", true);

    }


    protected void GridView1_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {

        if (Session["CurrentTable"] != null)
        {
            DataTable CurrentTable = (DataTable)Session["CurrentTable"];
            DataRow drCurrentRow = null;
            int rowIndex = Convert.ToInt32(e.RowIndex);
            if (CurrentTable.Rows.Count > 0)
            {
                CurrentTable.Rows.Remove(CurrentTable.Rows[rowIndex]);
                drCurrentRow = CurrentTable.NewRow();
                Session["CurrentTable"] = CurrentTable;
                GridView1.DataSource = CurrentTable;
                GridView1.DataBind();

                for (int i = 0; i < GridView1.Rows.Count - 1; i++)
                {
                    GridView1.Rows[i].Cells[0].Text = Convert.ToString(i + 1);
                }
            }
        }
    }
    protected void GridView1_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
    {
        GridView1.EditIndex = -1;
        DataTable dt = (DataTable)Session["CurrentTable"];
        GridView1.DataSource = dt;
        GridView1.DataBind();
    }
    protected void GridView1_RowEditing(object sender, GridViewEditEventArgs e)
    {
        sessioninitialize();
        GridView1.EditIndex = e.NewEditIndex;
        DataTable dt = (DataTable)Session["CurrentTable"];
        GridView1.DataSource = dt;
        GridView1.DataBind();
    }
    protected void GridView1_RowUpdating(object sender, GridViewUpdateEventArgs e)
    {
        DataTable dt = new DataTable();
        DataRow row = dt.NewRow();

        dt.Columns.Add("SerialNo", typeof(string));
        dt.Columns.Add("TestId", typeof(string));
        dt.Columns.Add("TestReqNo", typeof(string));
        dt.Columns.Add("TestName", typeof(string));
        dt.Columns.Add("cost", typeof(decimal));
        dt.Columns.Add("Date", typeof(string));
        dt.Columns.Add("Time", typeof(string));
        dt.Columns.Add("DeliveryDate", typeof(string));
        dt.Columns.Add("Remarks", typeof(string));
        dt.Columns.Add("consultant", typeof(string));
        dt.Columns.Add("consultantname", typeof(string));


        if (GridView1.Rows.Count > 0)
        {
            for (int i = 0; i < GridView1.Rows.Count; i++)
            {
                Label lblSerial = (Label)GridView1.Rows[i].FindControl("lblSerial");
                Label lblid = (Label)GridView1.Rows[i].FindControl("lblid");
                Label lblName = (Label)GridView1.Rows[i].FindControl("lblName");
                Label lblcost = (Label)GridView1.Rows[i].FindControl("lblcost");
                Label lblDate = (Label)GridView1.Rows[i].FindControl("lblDate");
                Label lblTime = (Label)GridView1.Rows[i].FindControl("lblTime");
                Label lbldvdate = (Label)GridView1.Rows[i].FindControl("lbldvdate");
                Label lblTestReqNo = (Label)GridView1.Rows[i].FindControl("lblTestReqNo");
                Label lblRemarks = (Label)GridView1.Rows[i].FindControl("lblRemarks");
                Label lbconsultant = (Label)GridView1.Rows[i].FindControl("lbconsultant");
                Label lbconsultantname = (Label)GridView1.Rows[i].FindControl("lbconsultantname");
                



                Label EditSerial = (Label)GridView1.Rows[e.RowIndex].FindControl("lblSerial");
                TextBox txtdate = (TextBox)GridView1.Rows[e.RowIndex].FindControl("txtdate");
                TextBox txtTime = (TextBox)GridView1.Rows[e.RowIndex].FindControl("txtTime");
                TextBox txtdvdate = (TextBox)GridView1.Rows[e.RowIndex].FindControl("txtdvdate");
                TextBox txtRemarks = (TextBox)GridView1.Rows[e.RowIndex].FindControl("txtRemarks");
                DropDownList ddlExistconsult = (DropDownList)GridView1.Rows[e.RowIndex].FindControl("ddlExistconsult");

                if (lblSerial.Text == EditSerial.Text)
                {

                    row["SerialNo"] = lblSerial.Text;
                    row["TestId"] = lblid.Text;
                    row["TestReqNo"] = lblTestReqNo.Text;
                    row["TestName"] = lblName.Text;
                    row["Cost"] = Convert.ToDouble(lblcost.Text);
                    row["Date"] = txtdate.Text;
                    row["Time"] = txtTime.Text;
                    row["DeliveryDate"] = txtdvdate.Text;
                    row["Remarks"] = txtRemarks.Text;
                    row["consultant"] = ddlExistconsult.SelectedValue.Trim();
                    row["consultantname"] = ddlExistconsult.SelectedItem.Text.Trim();
                }
                else
                {
                    row["SerialNo"] = lblSerial.Text;
                    row["TestId"] = lblid.Text;
                    row["TestReqNo"] = lblTestReqNo.Text;
                    row["TestName"] = lblName.Text;
                    row["Cost"] = Convert.ToDouble(lblcost.Text);
                    row["Date"] = lblDate.Text;
                    row["Time"] = lblTime.Text;
                    row["DeliveryDate"] = lbldvdate.Text;
                    row["Remarks"] = lblRemarks.Text;
                    row["consultant"] = lbconsultant.Text;
                    row["consultantname"] = lbconsultantname.Text.Trim();
                }
                dt.Rows.Add(row);
                row = dt.NewRow();
            }
        }

        Session["CurrentTable"] = dt;


        GridView1.EditIndex = -1;
        GridView1.DataSource = dt;
        GridView1.DataBind();

    }
    protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        DataTable databound = new DataTable();
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            Label lblSerial = (Label)e.Row.FindControl("lblSerial");
            lblSerial.Text = ((GridView1.PageIndex * GridView1.PageSize) + e.Row.RowIndex + 1).ToString();

            Label lblTestReqNo = (Label)e.Row.FindControl("lblTestReqNo");
            /*if (lblTestReqNo.Text != "")
            {
                e.Row.Cells[10].Enabled = false;
                e.Row.Cells[11].Enabled = false;
            }
            else
            {
                e.Row.Cells[10].Enabled = true;
                e.Row.Cells[11].Enabled = true;
            }*/
        }
        if (((e.Row.RowState == DataControlRowState.Edit) || e.Row.RowState == (DataControlRowState.Alternate | DataControlRowState.Edit)))
        {
            Label lbconsultant = (Label)e.Row.FindControl("lblExistconsultant");
            DropDownList ddlExistconsult = (DropDownList)e.Row.FindControl("ddlExistconsult");

            DataTable dt = thedia.GetconsultantDoc(Session["CoCode"].ToString().Trim(), "");
            ddlExistconsult.DataSource = dt;
            ddlExistconsult.DataValueField = "consullt";
            ddlExistconsult.DataTextField = "docname";
            ddlExistconsult.DataBind();

            ddlExistconsult.Items.Insert(0, new ListItem("None", ""));
            ddlExistconsult.Items.Add(new ListItem("SRL Lab", "SRL Lab"));
            ddlExistconsult.Items.Add(new ListItem("Lilac Lab", "Lilac Lab"));
            ddlExistconsult.SelectedValue = lbconsultant.Text.Trim();
        }
    }
    protected void GridView_popup_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            List<string> str = new List<string>();
            List<string> strname = new List<string>();
            DropDownList ddlconsult = (DropDownList)e.Row.FindControl("ddlconsult");
            Label lblid = (Label)e.Row.FindControl("lblid");
            //DataTable dt = thedia.Getconsultant(Session["CoCode"].ToString().Trim(), lblid.Text.Trim());
            DataTable dt = thedia.GetconsultantDoc(Session["CoCode"].ToString().Trim(), lblid.Text.Trim());
            if (dt.Rows.Count > 0)
            {

                //foreach (DataRow row in dt.Rows)
                //{
                //    foreach (DataColumn Col in dt.Columns)
                //    {
                //        if (row[Col].ToString().Trim() != "")
                //        {
                //            str.Add(row[Col].ToString());
                //        }

                //    }
                //}
                //for (int i = 0; i < dt.Rows.Count; i++)
                //{
                //    if (dt.Rows[i]["consullt"].ToString().Trim() != "")
                //    {
                //        ddlconsult.Items.Add(new ListItem(dt.Rows[i]["docname"].ToString().Trim(), dt.Rows[i]["consullt"].ToString().Trim()));
                //    }
                //}

                ddlconsult.DataSource = dt;
                ddlconsult.DataValueField = "consullt";
                ddlconsult.DataTextField = "docname";
                ddlconsult.DataBind();
                ddlconsult.Items.Insert(0, new ListItem("None", ""));
                ddlconsult.Items.Add(new ListItem("SRL Lab", "SRL Lab"));
                ddlconsult.Items.Add(new ListItem("Lilac Lab", "Lilac Lab"));
            }

            //for (var i = 0; i < str.Count; i++)
            //{
            //    ddlconsult.Items.Add(new ListItem(str[i].ToString(), str[i].ToString()));
            //}
            //ddlconsult.DataBind();
        }
    }
}