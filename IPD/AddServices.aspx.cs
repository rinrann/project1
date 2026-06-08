using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Data.SqlClient;
using System.Drawing;
using System.Globalization;

public partial class IPD_AddServices : System.Web.UI.Page
{
    Main Objmain = new Main(ConfigurationSettings.AppSettings["ConnectionString"].ToString());
    AddConsumable theaddConsumable = new AddConsumable(ConfigurationSettings.AppSettings["ConnectionString"].ToString());
    AddServices theaddservice = new AddServices(ConfigurationSettings.AppSettings["ConnectionString"].ToString());
    PatientDoctorVisit thedocvisit = new PatientDoctorVisit(ConfigurationSettings.AppSettings["ConnectionString"].ToString());
    ServiceTemplate theshift = new ServiceTemplate(ConfigurationSettings.AppSettings["ConnectionString"].ToString());
    DropDownList d1, d2, d3, d4, d5, d6;
    TextBox t1, t2, t3;
    public static DataTable dt_consumables1;
    protected void Page_Load(object sender, EventArgs e)
    {

        ScriptManager.RegisterStartupScript(this, this.GetType(), "msg", "GetDatetime();", true);

        Page.Title = "Add Services";
        if (Session["userName"] == null)
        {
            Response.Redirect("../LoginPage.aspx");
        }
        if (Objmain.checkaccss(Session["CoCode"].ToString(), Session["UserRoleID"].ToString().Trim(), "ADD SERVICES", checkAccessType.ViewAction) == false)
        {
            Response.Redirect("../AccessDenied.aspx");
        }

        if (Objmain.checkaccss(Session["CoCode"].ToString(), Session["UserRoleID"].ToString().Trim(), "ADD SERVICES", checkAccessType.InsertAction) == false)
        {
            Button1.Enabled = false;
        }
        if (Objmain.checkaccss(Session["CoCode"].ToString(), Session["UserRoleID"].ToString().Trim(), "ADD SERVICES", checkAccessType.UpdateAction) == false)
        {
            GridView7.Columns[7].Visible = false;
        }
        if (Objmain.checkaccss(Session["CoCode"].ToString(), Session["UserRoleID"].ToString().Trim(), "ADD SERVICES", checkAccessType.DeleteAction) == false)
        {
            GridView7.Columns[8].Visible = false;
        }
        if (Objmain.checkaccss(Session["CoCode"].ToString(), Session["UserRoleID"].ToString().Trim(), "ADD SERVICES", checkAccessType.DeleteAction) == false)
        {
            GridView8.Columns[8].Visible = false;
        }
        if (Objmain.checkaccss(Session["CoCode"].ToString(), Session["UserRoleID"].ToString().Trim(), "ADD SERVICES", checkAccessType.UpdateAction) == false)
        {
            GridView1.Columns[10].Visible = false;
        }
        if (Objmain.checkaccss(Session["CoCode"].ToString(), Session["UserRoleID"].ToString().Trim(), "ADD SERVICES", checkAccessType.DeleteAction) == false)
        {
            GridView1.Columns[11].Visible = false;
        }

        if (!IsPostBack)
        {

            DropDownFill();
            txtdate.Text = DateTime.Now.ToString("dd'/'MM'/'yyyy");
            Tab1Func();
            if (Session["RegNo"] != null)
            {
                TextBox2.Text = Session["RegNo"].ToString(); 
                GridFill();
            }
            dt_consumables1 = new DataTable();
            dt_consumables1.Columns.Add("RowID", typeof(string));
            dt_consumables1.Columns.Add("ConsumableGrId", typeof(string));
            dt_consumables1.Columns.Add("ConGroupName", typeof(string));
            dt_consumables1.Columns.Add("ConsumableItemId", typeof(string));
            dt_consumables1.Columns.Add("ConItemName", typeof(string));
            dt_consumables1.Columns.Add("ActualQty", typeof(string));
            dt_consumables1.Columns.Add("BillQty", typeof(string));
            dt_consumables1.Columns.Add("Price", typeof(string));
            Session["CurrentTable"] = null;
        }
    } 

    private void GridFill()
    {
        DataSet ds = theaddservice.GetAllAddServices(Session["CoCode"].ToString().Trim(),TextBox2.Text);

        DropDownList4.SelectedValue = ds.Tables[2].Rows[0][1].ToString();
        ddd3(ds.Tables[2].Rows[0][1].ToString());
        DropDownList3.SelectedValue = ds.Tables[2].Rows[0][0].ToString();

        if (ds.Tables[0].Rows.Count > 0)
        {
            GridView1.DataSource = ds.Tables[0];
            GridView1.DataBind();
            TextBox3.Text = ds.Tables[0].Rows[0]["patient_name"].ToString();
            TextBox4.Text = ds.Tables[0].Rows[0]["BedNoText"].ToString();
            TextBox5.Text = ds.Tables[0].Rows[0]["adate"].ToString();
        }
        else
        {
            if (ds.Tables[1].Rows.Count > 0)
            {
                TextBox3.Text = ds.Tables[1].Rows[0]["patient_name"].ToString();
                TextBox4.Text = ds.Tables[1].Rows[0]["BedNoText"].ToString();
                TextBox5.Text = ds.Tables[1].Rows[0]["adate"].ToString();
            }
           
        }
    
    }


    private void ResetAllFields()
    {
        DropDownList3.SelectedIndex = 0;
        DropDownList4.SelectedIndex = 0; DropDownList5.SelectedIndex = 0; DropDownList6.SelectedIndex = 0;
        TextBox2.Text = "";
        TextBox3.Text = "";
        TextBox4.Text = "";
        TextBox5.Text = "";
        //ddlService1.SelectedIndex = 0; ddlserviceCat1.SelectedIndex = 0; txtTotal1.Text = ""; txtTimeperday1.Text = ""; txtDuration1.Text = "";
        Button1.Text = "Submit";

        if (Objmain.checkaccss(Session["CoCode"].ToString(), Session["UserRoleID"].ToString().Trim(), "ADD SERVICES", checkAccessType.InsertAction) == false)
        {
            Button1.Enabled = false;
        }

    }

  
    //protected void GridView2_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
    //{
    //    GridView2.EditIndex = -1;
    //    DataTable dt = (DataTable)Session["CurrentTable"];
    //    GridView2.DataSource = dt;
    //    GridView2.DataBind();
    //}
    //protected void GridView2_RowDataBound(object sender, GridViewRowEventArgs e)
    //{

    //    if (e.Row.RowType == DataControlRowType.DataRow)
    //    {
    //        Label lblid = (Label)e.Row.FindControl("lblid");
    //        lblid.Text = ((GridView2.PageIndex * GridView2.PageSize) + e.Row.RowIndex + 1).ToString();
    //    }
    //    if (e.Row.RowType == DataControlRowType.DataRow && (e.Row.RowState & DataControlRowState.Edit) == DataControlRowState.Edit)
    //    {
    //        // Here you will get the Control you need like:
    //        DropDownList ddlServiceCategory = (DropDownList)e.Row.FindControl("ddlServiceCategory");
    //        DropDownList ddlService = (DropDownList)e.Row.FindControl("ddlService");

    //        Label lblServiceID = (Label)e.Row.FindControl("lblServiceID");
    //        Label lblServiceCategoryID = (Label)e.Row.FindControl("lblServiceCategoryID");

    //        ddlServiceCategory.Items.Clear();
    //        ddlServiceCategory.DataSource = thedocvisit.GetServiceConsumableTemplateeGroup();
    //        ddlServiceCategory.DataTextField = "ServiceCategoryName";
    //        ddlServiceCategory.DataValueField = "ServiceCategoryID";
    //        ddlServiceCategory.DataBind();
    //        ddlServiceCategory.Items.Insert(0, new ListItem("--Select--", "0"));
    //        ddlServiceCategory.SelectedValue = lblServiceCategoryID.Text;


    //        ServiceFill("0", ddlService);
    //        ddlService.SelectedValue = lblServiceID.Text;

    //    }
    //}

    //protected void ddlServiceCategory_SelectedIndexChanged(object sender, EventArgs e)
    //{

    //    DropDownList ddlServiceCategory = (DropDownList)GridView2.Rows[GridView2.EditIndex].FindControl("ddlServiceCategory");
    //    DropDownList ddlService = (DropDownList)GridView2.Rows[GridView2.EditIndex].FindControl("ddlService");
    //    Label lblServiceID = (Label)GridView2.Rows[GridView2.EditIndex].FindControl("lblServiceID");
    //    ServiceFill(ddlServiceCategory.SelectedValue, ddlService);
    //}



    //protected void GridView2_RowDeleting(object sender, GridViewDeleteEventArgs e)
    //{
    //    if (Session["CurrentTable"] != null)
    //    {
    //        DataTable CurrentTable = (DataTable)Session["CurrentTable"];
    //        DataRow drCurrentRow = null;
    //        int rowIndex = Convert.ToInt32(e.RowIndex);
    //        if (CurrentTable.Rows.Count > 1)
    //        {
    //            CurrentTable.Rows.Remove(CurrentTable.Rows[rowIndex]);
    //            drCurrentRow = CurrentTable.NewRow();
    //            Session["CurrentTable"] = CurrentTable;
    //            GridView2.DataSource = CurrentTable;
    //            GridView2.DataBind();

    //            for (int i = 0; i < GridView2.Rows.Count - 1; i++)
    //            {
    //                GridView2.Rows[i].Cells[0].Text = Convert.ToString(i + 1);
    //            }
    //        }
    //    }  
    //}
    //protected void GridView2_RowEditing(object sender, GridViewEditEventArgs e)
    //{
    //    GridView2.EditIndex = e.NewEditIndex;
    //    DataTable dt = (DataTable)Session["CurrentTable"];       
    //    GridView2.DataSource = dt;
    //    GridView2.DataBind();
    //} 

    //protected void GridView2_RowUpdating(object sender, GridViewUpdateEventArgs e)
    //{ 
    //    DataTable dt = new DataTable();
    //    DataRow row = dt.NewRow(); 
    //    dt.Columns.Add("RowID", typeof(string));
    //    dt.Columns.Add("ServiceCategoryId", typeof(string));
    //    dt.Columns.Add("ServiceId", typeof(string));
    //    dt.Columns.Add("ServiceCategoryName", typeof(string));
    //    dt.Columns.Add("ServiceName", typeof(string));
    //    dt.Columns.Add("TimeperDay", typeof(string));
    //    dt.Columns.Add("Duration", typeof(string));
    //    dt.Columns.Add("TotalQty", typeof(string)); 


    //    if (GridView2.Rows.Count > 0)
    //    {
    //        for (int i = 0; i < GridView2.Rows.Count; i++)
    //        {
    //            Label lblid = (Label)GridView2.Rows[i].FindControl("lblid");
    //            Label lblTimeperDay = (Label)GridView2.Rows[i].FindControl("lblTimeperDay");
    //            Label lblDuration = (Label)GridView2.Rows[i].FindControl("lblDuration");
    //            Label lblTotalQty = (Label)GridView2.Rows[i].FindControl("lblTotalQty");
    //            Label lblServiceCategoryName = (Label)GridView2.Rows[i].FindControl("lblServiceCategoryName");
    //            Label lblServiceName = (Label)GridView2.Rows[i].FindControl("lblServiceName");
    //            Label lblServiceCategoryId = (Label)GridView2.Rows[i].FindControl("lblServiceCategoryId");
    //            Label lblServiceId = (Label)GridView2.Rows[i].FindControl("lblServiceId");

    //            Label EditSerial = (Label)GridView2.Rows[e.RowIndex].FindControl("lblid");
    //            DropDownList ddlServiceCategoryVal = (DropDownList)GridView2.Rows[e.RowIndex].FindControl("ddlServiceCategory");
    //            DropDownList ddlServiceVal = (DropDownList)GridView2.Rows[e.RowIndex].FindControl("ddlService");

    //            TextBox txtTimeperDay = (TextBox)GridView2.Rows[e.RowIndex].FindControl("txtTimeperDay");
    //            TextBox txtDuration = (TextBox)GridView2.Rows[e.RowIndex].FindControl("txtDuration");
    //            TextBox txtTotalQuantity = (TextBox)GridView2.Rows[e.RowIndex].FindControl("txtTotalQuantity");

    //            if (lblid.Text == EditSerial.Text)
    //            {

    //                row["RowID"] = EditSerial.Text;
    //                row["ServiceCategoryId"] = lblServiceCategoryId.Text;
    //                row["ServiceId"] = lblServiceId.Text;
    //                row["ServiceCategoryName"] = ddlServiceCategoryVal.SelectedItem.Text;
    //                row["ServiceName"] = ddlServiceVal.SelectedItem.Text;
    //                row["TimeperDay"] = txtTimeperDay.Text;
    //                row["Duration"] = txtDuration.Text;
    //                row["TotalQty"] = txtTotalQuantity.Text;
    //            }
    //            else
    //            {
    //                row["RowID"] = lblid.Text;
    //                row["ServiceCategoryId"] = ddlServiceCategoryVal.SelectedValue;
    //                row["ServiceId"] = ddlServiceVal.SelectedValue;
    //                row["ServiceCategoryName"] = lblServiceCategoryName.Text;
    //                row["ServiceName"] = lblServiceName.Text;
    //                row["TimeperDay"] = txtTimeperDay.Text;
    //                row["Duration"] = lblDuration.Text;
    //                row["TotalQty"] = lblTotalQty.Text;
    //            }
    //            dt.Rows.Add(row);
    //            row = dt.NewRow();
    //        }
    //    }

    //    Session["CurrentTable"] = dt; 

    //    GridView2.EditIndex = -1;

    //    GridView2.DataSource = dt;
    //    GridView2.DataBind();
    //}
    //protected void GridView2_PageIndexChanging(object sender, GridViewPageEventArgs e)
    //{
    //    GridView2.PageIndex = e.NewPageIndex;
    //    DataTable dt = (DataTable)Session["CurrentTable"];
    //    GridView2.DataSource = dt;
    //    GridView2.DataBind();
    //}


    protected void Button1_Click(object sender, EventArgs e)
    {
        System.Globalization.DateTimeFormatInfo dtf = new DateTimeFormatInfo();
        DateTime testdate = DateTime.ParseExact(txtdate.Text, "dd/MM/yyyy", dtf);
        string servcont = "";
        bool flag = true;
        if (Button1.Text == "Submit")
        {
            for (int i = 0; i < GridView7.Rows.Count; i++)
            {
                Label lblServiceId = (Label)GridView7.Rows[i].FindControl("lblServiceId");
                Label lblQuantity = (Label)GridView7.Rows[i].FindControl("lblQuantity");
                Label lblPrice = (Label)GridView7.Rows[i].FindControl("lblPrice");

                CheckBox servcontinue = (CheckBox)GridView7.Rows[i].FindControl("servcontinue");
                if (servcontinue.Checked == true)
                {
                    servcont = "1";
                }
                else
                {
                    servcont = "0";
                }

                if (theaddservice.InsertAddservice(DateTime.Now.ToShortTimeString(), TextBox2.Text, lblServiceId.Text, lblQuantity.Text, lblPrice.Text, testdate.ToString("yyyy-MM-dd"), DropDownList4.SelectedValue, DropDownList3.SelectedValue, DropDownList5.SelectedValue, DropDownList6.SelectedValue, Session["CoCode"].ToString().Trim(), Session["userName"].ToString().Trim(), servcont) == true)
                {
                    flag = true;
                }
                else
                {
                    flag = false;
                    break;
                }
            }
            for (int i = 0; i < GridView8.Rows.Count; i++)
            {
                Label lblConGrId = (Label)GridView8.Rows[i].FindControl("lblConGrId");
                Label lblConGroupName = (Label)GridView8.Rows[i].FindControl("lblConGroupName");
                Label lblConItemID = (Label)GridView8.Rows[i].FindControl("lblConItemID");
                Label lblConItemName = (Label)GridView8.Rows[i].FindControl("lblConItemName");
                Label lblActualQty = (Label)GridView8.Rows[i].FindControl("lblActualQty");
                Label lblBillQty = (Label)GridView8.Rows[i].FindControl("lblBillQty");

                if (theaddConsumable.InsertConsumable(HiddenField2.Value, TextBox2.Text, lblConGrId.Text, lblConItemID.Text, testdate.ToString("yyyy-MM-dd"), lblActualQty.Text, lblBillQty.Text, DropDownList4.SelectedValue, DropDownList3.SelectedValue, DropDownList5.SelectedValue, DropDownList6.SelectedValue, Session["CoCode"].ToString().Trim(), Session["userName"].ToString().Trim()) == true)
                {
                    theaddConsumable.Update_Service_Status(TextBox2.Text);
                    flag = true;
                }
                else
                {
                    flag = false;
                    break;
                }
                
            }
            if (flag == true)
            {
                Session["CurrentTable"] = null;
                //ScriptManager.RegisterStartupScript(this, this.GetType(), "msg", "alert('Insered Successfully !');", true);
                Response.Redirect("../IPD/AdmissionPatientList.aspx");
                ResetAllFields();
            }
            else
                ScriptManager.RegisterStartupScript(this, this.GetType(), "msg", "alert('Error in inserted data  !');", true);
            GridFill();
            ResetAllFields();

        }
    }
    
    public void ServiceFill(string value, DropDownList items)
    {
        items.Items.Clear();
        items.DataSource = thedocvisit.GetServiceDetails(value, Session["CoCode"].ToString().Trim());
        items.DataTextField = "ServiceTemplateName";
        items.DataValueField = "NameID";
        items.DataBind();
        items.Items.Insert(0, new ListItem("--Select--", "0"));
    }

    protected void DropDownList37_SelectedIndexChanged(object sender, EventArgs e)
    {
        DataTable serviceMap = thedocvisit.GetServiceTemplateCharge(Session["CoCode"].ToString().Trim(), DropDownList37.SelectedValue); 
        DataTable dttable = (DataTable)Session["CurrentTable"];
        DataTable dt = new DataTable();
        DataRow row = dt.NewRow();

        dt.Columns.Add("RowID", typeof(string));
        dt.Columns.Add("ServiceId", typeof(string));
        dt.Columns.Add("ServiceCategoryName", typeof(string));
        dt.Columns.Add("Quantity", typeof(string));
        dt.Columns.Add("Price", typeof(string));

        if (dttable != null)
        {
            for (int i = 0; i < dttable.Rows.Count; i++)
            {
                row["ServiceId"] = dttable.Rows[i]["ServiceId"];
                row["ServiceCategoryName"] = dttable.Rows[i]["ServiceCategoryName"];
                row["Quantity"] = "1";
                row["Price"] = dttable.Rows[i]["Price"];
                dt.Rows.Add(row);
                row = dt.NewRow();
            }
        }
        row["ServiceId"] = DropDownList37.SelectedValue;
        row["ServiceCategoryName"] = DropDownList37.SelectedItem.Text;
        row["Quantity"] = "1";
        row["Price"] = serviceMap.Rows[0][0].ToString();
        dt.Rows.Add(row);
        row = dt.NewRow();


        GridView7.DataSource = dt;
        GridView7.DataBind();
        Session["CurrentTable"] = dt;

        FillMap(DropDownList37.SelectedValue);
    }

    
    protected void ddlConGroupName_SelectedIndexChanged1(object sender, EventArgs e)
    {

        DropDownList ddlConGroupName = (DropDownList)GridView8.Rows[GridView8.EditIndex].FindControl("ddlConGroupName");
        DropDownList ddlConItemName = (DropDownList)GridView8.Rows[GridView8.EditIndex].FindControl("ddlConItemName");
        ConsumableItemFill(ddlConGroupName.SelectedValue, ddlConItemName);
    }
    public void ConsumableItemFill(string value, DropDownList items)
    {
        items.Items.Clear();
        items.DataSource = thedocvisit.GetConsumableItem(Session["CoCode"].ToString().Trim(), value);
        items.DataTextField = "ConItemName";
        items.DataValueField = "ConItemID";
        items.DataBind();
        items.Items.Insert(0, new ListItem("--Select--", "0"));
    }

    protected void GridView8_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        GridView8.PageIndex = e.NewPageIndex;
        DataTable dt = (DataTable)Session["CurrentTable1"];
        GridView8.DataSource = dt;
        GridView8.DataBind();
    }
    protected void GridView8_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
    {
        GridView8.EditIndex = -1;
        DataTable dt = (DataTable)Session["CurrentTable1"];
        GridView8.DataSource = dt;
        GridView8.DataBind();
    }
    protected void GridView8_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        if (Session["CurrentTable1"] != null)
        {
            DataTable CurrentTable = (DataTable)Session["CurrentTable1"];
            DataRow drCurrentRow = null;
            int rowIndex = Convert.ToInt32(e.RowIndex);
            if (CurrentTable.Rows.Count > 1)
            {
                CurrentTable.Rows.Remove(CurrentTable.Rows[rowIndex]);
                drCurrentRow = CurrentTable.NewRow();
                Session["CurrentTable1"] = CurrentTable;
                GridView8.DataSource = CurrentTable;
                GridView8.DataBind();

                for (int i = 0; i < GridView8.Rows.Count - 1; i++)
                {
                    GridView8.Rows[i].Cells[0].Text = Convert.ToString(i + 1);
                }
            }
        }
    }
    protected void GridView8_RowEditing(object sender, GridViewEditEventArgs e)
    {
        GridView8.EditIndex = e.NewEditIndex;
        DataTable dt = (DataTable)Session["CurrentTable1"];
        GridView8.DataSource = dt;
        GridView8.DataBind();
    }
    protected void GridView8_RowUpdating(object sender, GridViewUpdateEventArgs e)
    {

        DataTable dt = new DataTable();
        DataRow row = dt.NewRow();

        dt.Columns.Add("RowID", typeof(string));
        dt.Columns.Add("ConsumableGrId", typeof(string));
        dt.Columns.Add("ConGroupName", typeof(string));
        dt.Columns.Add("ConsumableItemId", typeof(string));
        dt.Columns.Add("ConItemName", typeof(string));
        dt.Columns.Add("ActualQty", typeof(string));
        dt.Columns.Add("BillQty", typeof(string));
        dt.Columns.Add("Price", typeof(string));


        if (GridView8.Rows.Count > 0)
        {
            for (int i = 0; i < GridView8.Rows.Count; i++)
            {
                Label lblid = (Label)GridView8.Rows[i].FindControl("lblid");
                Label lblConGrId = (Label)GridView8.Rows[i].FindControl("lblConGrId");
                Label lblConGroupName = (Label)GridView8.Rows[i].FindControl("lblConGroupName");
                Label lblConItemID = (Label)GridView8.Rows[i].FindControl("lblConItemID");
                Label lblConItemName = (Label)GridView8.Rows[i].FindControl("lblConItemName");
                Label lblActualQty = (Label)GridView8.Rows[i].FindControl("lblActualQty");
                Label lblBillQty = (Label)GridView8.Rows[i].FindControl("lblBillQty");
                Label lblPrice = (Label)GridView8.Rows[i].FindControl("lblPrice");


                Label EditSerial = (Label)GridView8.Rows[e.RowIndex].FindControl("lblid");
                DropDownList ddlConGroupName = (DropDownList)GridView8.Rows[e.RowIndex].FindControl("ddlConGroupName");
                DropDownList ddlConItemNameval = (DropDownList)GridView8.Rows[e.RowIndex].FindControl("ddlConItemName");

                TextBox txtActualQty = (TextBox)GridView8.Rows[e.RowIndex].FindControl("txtActualQty");
                TextBox txtBillQty = (TextBox)GridView8.Rows[e.RowIndex].FindControl("txtBillQty");
                TextBox txtPrice = (TextBox)GridView8.Rows[e.RowIndex].FindControl("txtPrice");

                if (lblid.Text == EditSerial.Text)
                {

                    row["RowID"] = EditSerial.Text;
                    row["ConsumableGrId"] = ddlConGroupName.SelectedValue;
                    row["ConGroupName"] = ddlConGroupName.SelectedItem.Text;
                    row["ConsumableItemId"] = ddlConItemNameval.SelectedValue;
                    row["ConItemName"] = ddlConItemNameval.SelectedItem.Text;
                    row["ActualQty"] = txtActualQty.Text;
                    row["BillQty"] = txtBillQty.Text;
                    row["Price"] = txtPrice.Text;
                }
                else
                {
                    row["RowID"] = EditSerial.Text;
                    row["ConsumableGrId"] = lblConGrId.Text;
                    row["ConGroupName"] = lblConGroupName.Text;
                    row["ConsumableItemId"] = lblConItemID.Text;
                    row["ConItemName"] = lblConItemName.Text;
                    row["ActualQty"] = lblActualQty.Text;
                    row["BillQty"] = lblBillQty.Text;
                    row["Price"] = lblPrice.Text;
                }
                dt.Rows.Add(row);
                row = dt.NewRow();
            }
        }
        Session["CurrentTable1"] = dt;
        GridView8.EditIndex = -1;

        GridView8.DataSource = dt;
        GridView8.DataBind();
    }
    protected void GridView8_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            if (Objmain.checkaccss(Session["CoCode"].ToString(), Session["UserRoleID"].ToString().Trim(), "ADD SERVICES", checkAccessType.DeleteAction) == false)
            {
                e.Row.Cells[8].Visible = false;
            }
        }

        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            Label lblid = (Label)e.Row.FindControl("lblid");
            lblid.Text = ((GridView8.PageIndex * GridView8.PageSize) + e.Row.RowIndex + 1).ToString();
        }

        if (e.Row.RowType == DataControlRowType.DataRow && (e.Row.RowState & DataControlRowState.Edit) == DataControlRowState.Edit)
        {
            // Here you will get the Control you need like:
            DropDownList ddlConGroupName = (DropDownList)e.Row.FindControl("ddlConGroupName");
            DropDownList ddlConItemName = (DropDownList)e.Row.FindControl("ddlConItemName");


            Label lblConItemID = (Label)e.Row.FindControl("lblConItemID");
            Label lblConGrId = (Label)e.Row.FindControl("lblConGrId");

            ddlConGroupName.Items.Clear();
            ddlConGroupName.DataSource = thedocvisit.GetConsumableGroup(Session["CoCode"].ToString().Trim());
            ddlConGroupName.DataTextField = "ConGroupName";
            ddlConGroupName.DataValueField = "ConGrId";
            ddlConGroupName.DataBind();
            ddlConGroupName.Items.Insert(0, new ListItem("--Select--", "0"));
            ddlConGroupName.SelectedValue = lblConGrId.Text;


            ConsumableItemFill(ddlConGroupName.SelectedValue, ddlConItemName);
            ddlConItemName.SelectedValue = lblConItemID.Text;

        }
    }

    public void FillMap(string id)
    {
        DataTable dt_consumables = new DataTable();
        DataRow row_Consumables = dt_consumables.NewRow();
        
        dt_consumables.Columns.Add("RowID", typeof(string));
        dt_consumables.Columns.Add("ConsumableGrId", typeof(string));
        dt_consumables.Columns.Add("ConGroupName", typeof(string));
        dt_consumables.Columns.Add("ConsumableItemId", typeof(string));
        dt_consumables.Columns.Add("ConItemName", typeof(string));
        dt_consumables.Columns.Add("ActualQty", typeof(string));
        dt_consumables.Columns.Add("BillQty", typeof(string));
        dt_consumables.Columns.Add("Price", typeof(string));

        DataTable dtfill = theshift.FillMap(id);
        if (dtfill.Rows.Count > 0)
        {
            for (int i = 0; i < dtfill.Rows.Count; i++)
            {
                row_Consumables["ConsumableGrId"] = dtfill.Rows[i]["ConsumableCategoryId"].ToString();
                row_Consumables["ConGroupName"] = dtfill.Rows[i]["ConGroupName"].ToString();
                row_Consumables["ConsumableItemId"] = dtfill.Rows[i]["ConsumableItemId"].ToString();
                row_Consumables["ConItemName"] = dtfill.Rows[i]["ConItemName"].ToString();
                row_Consumables["ActualQty"] = dtfill.Rows[i]["ActualQty"].ToString();
                row_Consumables["BillQty"] = dtfill.Rows[i]["BillQty"].ToString();
                row_Consumables["Price"] = dtfill.Rows[i]["PriceperUnit"].ToString();
                dt_consumables.Rows.Add(row_Consumables);
                row_Consumables = dt_consumables.NewRow();

            }
        }
        dt_consumables1.Merge(dt_consumables);
        GridView8.DataSource = dt_consumables1;
        GridView8.DataBind();

        Session["CurrentTable1"] = dt_consumables1;
    }
    protected void DropDownList36_SelectedIndexChanged(object sender, EventArgs e)
    {
        DropDownList37.Items.Clear();
        this.DropDownList37.DataSource = thedocvisit.GetServiceConsumableTemplate(Session["CoCode"].ToString().Trim(),DropDownList36.SelectedValue);
        this.DropDownList37.DataTextField = "ServiceTemplateName";
        this.DropDownList37.DataValueField = "NameID";
        this.DropDownList37.DataBind();
        this.DropDownList37.Items.Insert(0, new ListItem("--Select--", "0"));
    }

    protected void ddlServiceCategory_SelectedIndexChanged(object sender, EventArgs e)
    {
        DropDownList ddlServiceCategory = (DropDownList)GridView1.Rows[GridView1.EditIndex].FindControl("ddlServiceCategory");
        DropDownList ddlService = (DropDownList)GridView1.Rows[GridView1.EditIndex].FindControl("ddlService");

        ddlService.Items.Clear();
        ddlService.DataSource = thedocvisit.GetServiceConsumableTemplate(Session["CoCode"].ToString().Trim(),ddlServiceCategory.SelectedValue);
        ddlService.DataTextField = "ServiceTemplateName";
        ddlService.DataValueField = "NameID";
        ddlService.DataBind();
        ddlService.Items.Insert(0, new ListItem("--Select--", "0"));
    }

    protected void Button2_Click(object sender, EventArgs e)
    {
        ResetAllFields();
    }
    private void DropDownFill()
    {
        DropDownList36.Items.Clear();
        this.DropDownList36.DataSource = thedocvisit.GetServiceConsumableTemplateeGroup(Session["CoCode"].ToString().Trim());
        this.DropDownList36.DataTextField = "CategoryName";
        this.DropDownList36.DataValueField = "TemplateCategoryId";
        this.DropDownList36.DataBind();
        this.DropDownList36.Items.Insert(0, new ListItem("--Select--", "0"));


        this.DropDownList37.Items.Insert(0, new ListItem("--Select--", "0"));


        this.DropDownList4.DataSource = theaddservice.DropdownDOCTORTYPE();
        this.DropDownList4.DataTextField = "TypeName";
        this.DropDownList4.DataValueField = "DocTypeId";
        this.DropDownList4.DataBind();
        this.DropDownList4.Items.Insert(0, new ListItem("--Select--", "0"));

        this.DropDownList5.DataSource = theaddservice.DropdownDOCTORTYPE();
        this.DropDownList5.DataTextField = "TypeName";
        this.DropDownList5.DataValueField = "DocTypeId";
        this.DropDownList5.DataBind();
        this.DropDownList5.Items.Insert(0, new ListItem("--Select--", "0"));

    }

    protected int SearchIndex(string Value, DropDownList ddl)
    {
        int i;
        for (i = 0; i < ddl.Items.Count; i++)
        {
            if (ddl.Items[i].Value.Trim() == Value.Trim())
                return i;
        }
        return -1;
    }

    protected int SearchText(string Value, DropDownList ddl)
    {
        int i;
        for (i = 0; i < ddl.Items.Count; i++)
        {
            if (ddl.Items[i].Text.Trim() == Value.Trim())
                return i;
        }
        return -1;
    }
    protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "Select")
        {
            int index = Convert.ToInt32(e.CommandArgument);
            lblError.Text = "";
            Label lblid = (Label)GridView1.Rows[index].FindControl("lblid");
            HiddenField1.Value = lblid.Text;


            Label lblRegno = (Label)GridView1.Rows[index].FindControl("lblRegno");
            TextBox2.Text = lblRegno.Text;
            Label lbllname = (Label)GridView1.Rows[index].FindControl("lbllname");
            TextBox3.Text = lbllname.Text;
            Label lbladate = (Label)GridView1.Rows[index].FindControl("lbladate");
            TextBox5.Text = lbladate.Text;
            Label lblbedno = (Label)GridView1.Rows[index].FindControl("lblbedno");
            TextBox4.Text = lblbedno.Text;

            //Label lblserivcecat = (Label)GridView1.Rows[index].FindControl("lblserivcecat");
            //DropDownList1.SelectedIndex = SearchText(lblserivcecat.Text, DropDownList1);

            //ddd2();
            //Label lblservice = (Label)GridView1.Rows[index].FindControl("lblservice");
            //DropDownList2.SelectedIndex = SearchText(lblservice.Text, DropDownList2);
           

            Label lbldoctype = (Label)GridView1.Rows[index].FindControl("lbldoctype");
            DropDownList4.SelectedIndex = SearchIndex(lbldoctype.Text, DropDownList4);

            ddd3(lbldoctype.Text);
            Label lbladvicedby = (Label)GridView1.Rows[index].FindControl("lbladvicedby");
            DropDownList3.SelectedIndex = SearchIndex(lbladvicedby.Text, DropDownList3);

            Label lbladditiondoctype = (Label)GridView1.Rows[index].FindControl("lbladditiondoctype");
            DropDownList5.SelectedIndex = SearchIndex(lbladditiondoctype.Text, DropDownList5);

            ddd4();
            Label lbladddoc = (Label)GridView1.Rows[index].FindControl("lbladddoc");
            DropDownList6.SelectedIndex = SearchIndex(lbladddoc.Text, DropDownList6);

            Label lbllblissuedate = (Label)GridView1.Rows[index].FindControl("lbllblissuedate");
           txtdate.Text = lbllblissuedate.Text;

            //Label lblquantity = (Label)GridView1.Rows[index].FindControl("lblquantity");
            //TextBox1.Text = lblquantity.Text;

            //Label lbldurationdays = (Label)GridView1.Rows[index].FindControl("lbldurationdays");
            //TextBox6.Text = lbldurationdays.Text;

            Tab1Func();
            Button1.Text = "Update";
            if (Objmain.checkaccss(Session["CoCode"].ToString(), Session["UserRoleID"].ToString().Trim(), "ADD SERVICES", checkAccessType.UpdateAction) == false)
            {
                Button1.Enabled = false;
            }
            else
            {
                Button1.Enabled = true;
            }
        }
    }
     
    protected void Button4_Click(object sender, EventArgs e)
    {
        GridFill();
    }
    protected void DropDownList4_SelectedIndexChanged(object sender, EventArgs e)
    {
        ddd3(DropDownList4.SelectedValue);       
    }

    public void ddd3(string val)
    {
         this.DropDownList3.Items.Clear();
         this.DropDownList3.DataSource = theaddservice.DropdownDoctor(val);
        this.DropDownList3.DataTextField = "doc_name";
        this.DropDownList3.DataValueField = "doc_id";
        this.DropDownList3.DataBind();
        this.DropDownList3.Items.Insert(0, new ListItem("--Select--", "0"));
    }

    public void ddd4()
    {
         this.DropDownList6.Items.Clear();
        this.DropDownList6.DataSource = theaddservice.DropdownDoctor(DropDownList5.SelectedValue);
        this.DropDownList6.DataTextField = "doc_name";
        this.DropDownList6.DataValueField = "doc_id";
        this.DropDownList6.DataBind();
        this.DropDownList6.Items.Insert(0, new ListItem("--Select--", "0"));
    }
    protected void DropDownList5_SelectedIndexChanged(object sender, EventArgs e)
    {
        ddd4();
    }
    protected void Tab1_Click(object sender, EventArgs e)
    {
        Tab1Func();
    }
    protected void Tab2_Click(object sender, EventArgs e)
    {
        Tab1.CssClass = "Initial";
        Tab2.CssClass = "Clicked";
        MainView.ActiveViewIndex = 1;
    }


    public void Tab1Func()
    {
        Tab1.CssClass = "Clicked";
        Tab2.CssClass = "Initial";
        MainView.ActiveViewIndex = 0;
    }

    //protected void Button5_Click(object sender, EventArgs e)
    //{

    //    DataTable dt = new DataTable();
    //    DataRow row = dt.NewRow();

    //    dt.Columns.Add("RowID", typeof(string));
    //    dt.Columns.Add("ServiceCategoryId", typeof(string));
    //    dt.Columns.Add("ServiceId", typeof(string));
    //    dt.Columns.Add("ServiceCategoryName", typeof(string));
    //    dt.Columns.Add("ServiceName", typeof(string));
    //    dt.Columns.Add("TimeperDay", typeof(string));
    //    dt.Columns.Add("Duration", typeof(string));
    //    dt.Columns.Add("TotalQty", typeof(string));


    //    if (GridView2.Rows.Count > 0)
    //    {
    //        for (int i = 0; i < GridView2.Rows.Count; i++)
    //        {
    //            Label lblid = (Label)GridView2.Rows[i].FindControl("lblid");
    //            Label lblTimeperDay = (Label)GridView2.Rows[i].FindControl("lblTimeperDay");
    //            Label lblDuration = (Label)GridView2.Rows[i].FindControl("lblDuration");
    //            Label lblTotalQty = (Label)GridView2.Rows[i].FindControl("lblTotalQty");
    //            Label lblServiceCategoryName = (Label)GridView2.Rows[i].FindControl("lblServiceCategoryName");
    //            Label lblServiceName = (Label)GridView2.Rows[i].FindControl("lblServiceName");
    //            Label lblServiceCategoryId = (Label)GridView2.Rows[i].FindControl("lblServiceCategoryId");
    //            Label lblServiceId = (Label)GridView2.Rows[i].FindControl("lblServiceId");

    //            row["RowID"] = lblid.Text;
    //            row["ServiceCategoryId"] = lblServiceCategoryId.Text;
    //            row["ServiceId"] = lblServiceId.Text;
    //            row["ServiceCategoryName"] = lblServiceCategoryName.Text;
    //            row["ServiceName"] = lblServiceName.Text;
    //            row["TimeperDay"] = lblTimeperDay.Text;
    //            row["Duration"] = lblDuration.Text;
    //            row["TotalQty"] = lblTotalQty.Text;

    //            dt.Rows.Add(row);
    //            row = dt.NewRow();
    //        }
    //    }
 
    //    row["ServiceCategoryId"] = ddlserviceCat1.SelectedValue;
    //    row["ServiceId"] = ddlService1.SelectedValue;
    //    row["ServiceCategoryName"] = ddlserviceCat1.SelectedItem.Text;
    //    row["ServiceName"] = ddlService1.SelectedItem.Text;
    //    row["TimeperDay"] = txtTimeperday1.Text;
    //    row["Duration"] = txtDuration1.Text;
    //    row["TotalQty"] = txtTotal1.Text;

    //    dt.Rows.Add(row);
    //    row = dt.NewRow();

    //    GridView2.DataSource = dt;
    //    GridView2.DataBind(); 
    //    Session["CurrentTable"] = dt; 

    //    Reset(); 
    //}
    protected void GridView7_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        GridView7.PageIndex = e.NewPageIndex;
        DataTable dt = (DataTable)Session["CurrentTable"];
        GridView7.DataSource = dt;
        GridView7.DataBind();
    }
    protected void GridView7_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
    {
        GridView7.EditIndex = -1;
        DataTable dt = (DataTable)Session["CurrentTable"];
        GridView7.DataSource = dt;
        GridView7.DataBind();
    }
    protected void GridView7_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
       

        if (Session["CurrentTable"] != null)
        {
            DataTable CurrentTable = (DataTable)Session["CurrentTable"];
            DataRow drCurrentRow = null;
            int rowIndex = Convert.ToInt32(e.RowIndex);
            if (CurrentTable.Rows.Count > 1)
            {
                CurrentTable.Rows.Remove(CurrentTable.Rows[rowIndex]);
                drCurrentRow = CurrentTable.NewRow();
                Session["CurrentTable"] = CurrentTable;
                GridView7.DataSource = CurrentTable;
                GridView7.DataBind();

                for (int i = 0; i < GridView7.Rows.Count - 1; i++)
                {
                    GridView7.Rows[i].Cells[0].Text = Convert.ToString(i + 1);
                }
            }
        }
    }
    protected void GridView7_RowEditing(object sender, GridViewEditEventArgs e)
    {
        GridView7.EditIndex = e.NewEditIndex;
        DataTable dt = (DataTable)Session["CurrentTable"];
        GridView7.DataSource = dt;
        GridView7.DataBind();
    }
    protected void GridView7_RowUpdating(object sender, GridViewUpdateEventArgs e)
    {
        DataTable dt = new DataTable();
        DataRow row = dt.NewRow();

        dt.Columns.Add("RowID", typeof(string));
        dt.Columns.Add("ServiceId", typeof(string));
        dt.Columns.Add("ServiceCategoryName", typeof(string));
        dt.Columns.Add("Quantity", typeof(string));
        dt.Columns.Add("Price", typeof(string));


        if (GridView7.Rows.Count > 0)
        {
            for (int i = 0; i < GridView7.Rows.Count; i++)
            {
                Label lblid = (Label)GridView7.Rows[i].FindControl("lblid");
                Label lblServiceId = (Label)GridView7.Rows[i].FindControl("lblServiceId");
                Label lblServiceName = (Label)GridView7.Rows[i].FindControl("lblServiceName");
                Label lblQuantity = (Label)GridView7.Rows[i].FindControl("lblQuantity");
                Label lblPrice = (Label)GridView7.Rows[i].FindControl("lblPrice");
                Label EditSerial = (Label)GridView7.Rows[e.RowIndex].FindControl("lblid");

                TextBox txtQuantity = (TextBox)GridView7.Rows[e.RowIndex].FindControl("txtQuantity");
                TextBox txtPrice = (TextBox)GridView7.Rows[e.RowIndex].FindControl("txtPrice");


                row["ServiceId"] = lblServiceId.Text;
                row["ServiceCategoryName"] = lblServiceName.Text;

                if (lblid.Text == EditSerial.Text)
                {

                    row["RowID"] = EditSerial.Text;
                    row["Quantity"] = txtQuantity.Text;
                    row["Price"] = txtPrice.Text;
                }
                else
                {
                    row["RowID"] = lblid.Text;
                    row["Quantity"] = lblQuantity.Text;
                    row["Price"] = lblPrice.Text;
                }
                dt.Rows.Add(row);
                row = dt.NewRow();
            }
        }

        Session["CurrentTable"] = dt;

        GridView7.EditIndex = -1;
        GridView7.DataSource = dt;
        GridView7.DataBind();
    }
    protected void GridView7_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            if (Objmain.checkaccss(Session["CoCode"].ToString(), Session["UserRoleID"].ToString().Trim(), "ADD SERVICES", checkAccessType.UpdateAction) == false)
            {
                e.Row.Cells[7].Visible = false;                
            }
            if (Objmain.checkaccss(Session["CoCode"].ToString(), Session["UserRoleID"].ToString().Trim(), "ADD SERVICES", checkAccessType.DeleteAction) == false)
            {
                e.Row.Cells[8].Visible = false;                
            }
        }

        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            Label lblid = (Label)e.Row.FindControl("lblid");
            lblid.Text = ((GridView7.PageIndex * GridView7.PageSize) + e.Row.RowIndex + 1).ToString();
            CheckBox chkcons = (CheckBox)e.Row.FindControl("chkCons");
            if (chkcons != null)
            {
                chkcons.Checked = true;
            }
        }
        if (e.Row.RowType == DataControlRowType.DataRow && (e.Row.RowState & DataControlRowState.Edit) == DataControlRowState.Edit)
        {
            // Here you will get the Control you need like:
            //DropDownList ddlServiceCategory = (DropDownList)e.Row.FindControl("ddlServiceCategory");
            //DropDownList ddlService = (DropDownList)e.Row.FindControl("ddlService");

            //Label lblServiceID = (Label)e.Row.FindControl("lblServiceID");
            //Label lblServiceCategoryID = (Label)e.Row.FindControl("lblServiceCategoryID");

            //ddlServiceCategory.Items.Clear();
            //ddlServiceCategory.DataSource = thedocvisit.GetServiceConsumableTemplateeGroup();
            //ddlServiceCategory.DataTextField = "CategoryName";
            //ddlServiceCategory.DataValueField = "TemplateCategoryId";
            //ddlServiceCategory.DataBind();
            //ddlServiceCategory.Items.Insert(0, new ListItem("--Select--", "0"));
            //ddlServiceCategory.SelectedValue = lblServiceCategoryID.Text;


            //ServiceFill(ddlServiceCategory.SelectedValue, ddlService);
            //ddlService.SelectedValue = lblServiceID.Text;

        }
    }

    protected void add_remove_cons(object sender, EventArgs e)
    {
        //string servId = "";
       // CheckBox chkcons=(CheckBox)sender;
       // GridViewRow row = (GridViewRow)chkcons.NamingContainer;
        DataTable dt_consumables = new DataTable();
        DataRow row_Consumables = dt_consumables.NewRow();

        dt_consumables.Columns.Add("RowID", typeof(string));
        dt_consumables.Columns.Add("ConsumableGrId", typeof(string));
        dt_consumables.Columns.Add("ConGroupName", typeof(string));
        dt_consumables.Columns.Add("ConsumableItemId", typeof(string));
        dt_consumables.Columns.Add("ConItemName", typeof(string));
        dt_consumables.Columns.Add("ActualQty", typeof(string));
        dt_consumables.Columns.Add("BillQty", typeof(string));
        dt_consumables.Columns.Add("Price", typeof(string));


        for (int i = 0; i < GridView7.Rows.Count; i++)
        {
            Label lblServiceId = (Label)GridView7.Rows[i].FindControl("lblServiceId");
            CheckBox chkcons = (CheckBox)GridView7.Rows[i].FindControl("chkCons");
            string id=lblServiceId.Text;
            if (chkcons.Checked == true)
            {
                DataTable dtfill = theshift.FillMap(id);
                    if (dtfill.Rows.Count > 0)
                    {
                        for (int j = 0; j < dtfill.Rows.Count; j++)
                        {
                            row_Consumables["ConsumableGrId"] = dtfill.Rows[j]["ConsumableCategoryId"].ToString();
                            row_Consumables["ConGroupName"] = dtfill.Rows[j]["ConGroupName"].ToString();
                            row_Consumables["ConsumableItemId"] = dtfill.Rows[j]["ConsumableItemId"].ToString();
                            row_Consumables["ConItemName"] = dtfill.Rows[j]["ConItemName"].ToString();
                            row_Consumables["ActualQty"] = dtfill.Rows[j]["ActualQty"].ToString();
                            row_Consumables["BillQty"] = dtfill.Rows[j]["BillQty"].ToString();
                            row_Consumables["Price"] = dtfill.Rows[j]["PriceperUnit"].ToString();
                            dt_consumables.Rows.Add(row_Consumables);
                            row_Consumables = dt_consumables.NewRow();

                        }
                    }
                
                

       
            }
            
        }
        GridView8.DataSource = dt_consumables;
        GridView8.DataBind();
        Session["CurrentTable1"] = dt_consumables;

        
    }
    protected void GridView1_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        GridView1.PageIndex = e.NewPageIndex;
        GridFill();
    }

    protected void GridView1_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
    {
        GridView1.EditIndex = -1;
        GridFill();
    }
    protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            if (Objmain.checkaccss(Session["CoCode"].ToString(), Session["UserRoleID"].ToString().Trim(), "ADD SERVICES", checkAccessType.DeleteAction) == false)
            {
                e.Row.Cells[11].Visible = false;
            }
            if (Objmain.checkaccss(Session["CoCode"].ToString(), Session["UserRoleID"].ToString().Trim(), "ADD SERVICES", checkAccessType.UpdateAction) == false)
            {
                e.Row.Cells[10].Visible = false;
            }
        }

        string servcont = "";
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            CheckBox ServiceCont = (CheckBox)e.Row.FindControl("chkcont");
            //CheckBox ServiceContedit = (CheckBox)e.Row.FindControl("cont");
            Label lblcont = (Label)e.Row.FindControl("lblcont");
            servcont = lblcont.Text;
            if (ServiceCont != null)
            {
                if (servcont == "1")
                {
                    ServiceCont.Checked = true;
                    // ServiceContedit.Checked = true;
                }
                else
                {
                    ServiceCont.Checked = false;
                    // ServiceContedit.Checked = false;
                }
            }
        }
        if (e.Row.RowType == DataControlRowType.DataRow && (e.Row.RowState & DataControlRowState.Edit) == DataControlRowState.Edit)
        {
           //  Here you will get the Control you need like:
            DropDownList ddlServiceCategory = (DropDownList)e.Row.FindControl("ddlServiceCategory");
            DropDownList ddlService = (DropDownList)e.Row.FindControl("ddlService");

            Label lblTemplateCategoryId = (Label)e.Row.FindControl("lblTemplateCategoryId");
            Label lblServiceId = (Label)e.Row.FindControl("lblServiceId");
            CheckBox ServiceContedit = (CheckBox)e.Row.FindControl("cont");
            Label lblcont = (Label)e.Row.FindControl("lblcont");

            ddlServiceCategory.Items.Clear();
            ddlServiceCategory.DataSource = thedocvisit.GetServiceConsumableTemplateeGroup(Session["CoCode"].ToString().Trim());
            ddlServiceCategory.DataTextField = "CategoryName";
            ddlServiceCategory.DataValueField = "TemplateCategoryId";
            ddlServiceCategory.DataBind();
            ddlServiceCategory.Items.Insert(0, new ListItem("--Select--", "0"));
            ddlServiceCategory.SelectedValue = lblTemplateCategoryId.Text;


            ServiceFill(lblTemplateCategoryId.Text, ddlService);
            ddlService.SelectedValue = lblServiceId.Text;

            if (servcont == "1")
            {
                ServiceContedit.Checked = true;
            }
            else
            {
                ServiceContedit.Checked = false;
            }

        }
    }
    protected void GridView1_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
       /* LinkButton l = (LinkButton)e.Row.FindControl("LinkButton1");
        l.Attributes.Add("onclick", "javascript:return " +
        "confirm('Are you sure you want to delete this record " +
        DataBinder.Eval(e.Row.DataItem, "CategoryID") + "')"); */

        bool flag = true;
        Label lblid = (Label)GridView1.Rows[e.RowIndex].FindControl("lblid");
        Label lbllblissuedate = (Label)GridView1.Rows[e.RowIndex].FindControl("lbllblissuedate");
        System.Globalization.DateTimeFormatInfo dtf = new DateTimeFormatInfo();
        DateTime testdate = DateTime.ParseExact(lbllblissuedate.Text, "dd/MM/yyyy", dtf);
        string issuedate = testdate.ToString("yyyy-MM-dd");

        if (theaddservice.Update_Delete_Addservice(3, TextBox2.Text, lblid.Text, null, null, null, issuedate,null, Session["CoCode"].ToString().Trim(), Session["userName"].ToString().Trim()) == true)
        {
            flag = true;
            
           // ScriptManager.RegisterStartupScript(this, this.GetType(), "msg", "alert('Deleted Successfully !');", true);
        }
        else
        {
           // ScriptManager.RegisterStartupScript(this, this.GetType(), "msg", "alert('Error in Delete !');", true);
            flag = true;
            
        }
        if (flag == true)
        {
            Label srvcId = (Label)GridView1.Rows[e.RowIndex].FindControl("lblServiceId");
            DataTable dtcons = theshift.FillMap(srvcId.Text);
            if (dtcons.Rows.Count > 0)
            {
                for (int i = 0; i < dtcons.Rows.Count; i++)
                {
                    if (theaddservice.Delete_PatientConsumables(TextBox2.Text, dtcons.Rows[i]["ConsumableCategoryId"].ToString(), dtcons.Rows[i]["ConsumableItemId"].ToString(), issuedate, Session["CoCode"].ToString().Trim()) == true)
                    {
                        flag = true;
                    }
                    else
                    {
                        flag = false;
                        break;
                    }
                }
            }
        }
        if (flag == true)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "msg", "alert('Deleted Successfully !');", true);
        }
        else
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "msg", "alert('Error in Delete !');", true);
        }
        GridFill();
    }
    protected void GridView1_RowEditing(object sender, GridViewEditEventArgs e)
    {
        GridView1.EditIndex = e.NewEditIndex; 
        GridFill();
    }
    protected void GridView1_RowUpdating(object sender, GridViewUpdateEventArgs e)
    {
        Label lblid = (Label)GridView1.Rows[e.RowIndex].FindControl("lblid");

        TextBox txtgDate = (TextBox)GridView1.Rows[e.RowIndex].FindControl("txtgDate");
        DropDownList ddlService = (DropDownList)GridView1.Rows[e.RowIndex].FindControl("ddlService");
        TextBox txtQuantity = (TextBox)GridView1.Rows[e.RowIndex].FindControl("txtQuantity");
        TextBox txtPrice = (TextBox)GridView1.Rows[e.RowIndex].FindControl("txtPrice");
        CheckBox ServCont = (CheckBox)GridView1.Rows[e.RowIndex].FindControl("cont");
        string servcont = "";
        if (ServCont.Checked == true)
        {
            servcont = "1";
        }
        else
        {
            servcont = "0";
        }
      
        System.Globalization.DateTimeFormatInfo dtf = new DateTimeFormatInfo();
        DateTime testdate;
        if (txtdate.Text != "")
            testdate = DateTime.ParseExact(txtgDate.Text, "dd/MM/yyyy", dtf);
        else
            testdate = DateTime.Now;
        if (theaddservice.Update_Delete_Addservice(2, TextBox2.Text, lblid.Text, ddlService.SelectedValue, txtQuantity.Text, txtPrice.Text, testdate.ToString("yyyy-MM-dd"), servcont, Session["CoCode"].ToString().Trim(), Session["userName"].ToString().Trim()) == true)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "msg", "alert('Updated Successfully !');", true);
        }
        else
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "msg", "alert('Error in Updated Data !');", true);
        }
        GridView1.EditIndex = -1;
        GridFill();
    }
}