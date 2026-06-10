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


public partial class IPD_ServiceTemplate : System.Web.UI.Page
{
    Main Objmain = new Main(ConfigurationSettings.AppSettings["ConnectionString"].ToString());
    ServiceTemplate theshift = new ServiceTemplate(ConfigurationSettings.AppSettings["ConnectionString"].ToString());
    PatientDoctorVisit thedocvisit = new PatientDoctorVisit(ConfigurationSettings.AppSettings["ConnectionString"].ToString());
    TextBox t1, t2;
    DropDownList d1, d2;
    protected void Page_Load(object sender, EventArgs e)
    {
         if (Session["userName"] == null)
        {
            Response.Redirect("../LoginPage.aspx");
        }
         if (Objmain.checkaccss(Session["CoCode"].ToString(), Session["UserRoleID"].ToString().Trim(), "SERVICE TEMPLATE", checkAccessType.ViewAction) == false)
         {
             Response.Redirect("../AccessDenied.aspx");
         }

         if (Objmain.checkaccss(Session["CoCode"].ToString(), Session["UserRoleID"].ToString().Trim(), "SERVICE TEMPLATE", checkAccessType.InsertAction) == false)
         {
             Button1.Enabled = false;
         }
        if (!IsPostBack)
        {
            Tab1Func();
            GridFill();
            DropDownFill();
        }
        Page.Title = "Ser & Cons Template";


    }

    public void FillMap(string id)
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

        DataTable dtfill = theshift.FillMap(id);
        if (dtfill.Rows.Count > 0)
        {
            for (int i = 0; i < dtfill.Rows.Count; i++)
            {
                row["ConsumableGrId"] = dtfill.Rows[i]["ConsumableCategoryId"].ToString();
                row["ConGroupName"] = dtfill.Rows[i]["ConGroupName"].ToString();
                row["ConsumableItemId"] = dtfill.Rows[i]["ConsumableItemId"].ToString();
                row["ConItemName"] = dtfill.Rows[i]["ConItemName"].ToString();
                row["ActualQty"] = dtfill.Rows[i]["ActualQty"].ToString();
                row["BillQty"] = dtfill.Rows[i]["BillQty"].ToString();
                row["Price"] = dtfill.Rows[i]["PriceperUnit"].ToString();
                dt.Rows.Add(row);
                row = dt.NewRow();
     
            }
        }
        GridView8.DataSource = dt;
        GridView8.DataBind();

        Session["CurrentTable"] = dt;
    }

    public void ServiceFill(string value, DropDownList items)
    {
        items.Items.Clear();
        items.DataSource = theshift.ServiceName(value);
        items.DataTextField = "ConItemName";
        items.DataValueField = "ConItemID";
        items.DataBind();
        items.Items.Insert(0, new ListItem("--Select--", "0"));
    }

    public int InsertMapping()
    {
        int flag = 0;
        for (int i = 0; i < GridView8.Rows.Count; i++)
        {
            Label lblConGrId = (Label)GridView8.Rows[i].FindControl("lblConGrId");
            Label lblConItemID = (Label)GridView8.Rows[i].FindControl("lblConItemID");
            Label lblActualQty = (Label)GridView8.Rows[i].FindControl("lblActualQty");
            Label lblBillQty = (Label)GridView8.Rows[i].FindControl("lblBillQty");
            Label lblPrice = (Label)GridView8.Rows[i].FindControl("lblPrice");

            if (theshift.TemplateFillFunction(1, null, txtTemplateName.Text.Trim(), lblConGrId.Text, lblConItemID.Text, lblActualQty.Text, lblBillQty.Text, lblPrice.Text, Session["CoCode"].ToString().Trim()) == true)
            {
                flag = 1;
            }
            else
            {
                flag = 0;
                break;
            }
        }
        return flag;
    }
             
    protected void Button1_Click(object sender, EventArgs e)
    {
        string id;
        int flag = 0;
        if (TextBox4.Value != "")
        {
            id = TextBox4.Value;
        }
        else
        {
            id = "0";
        }
        if (Button1.Text == "Submit")
        {
            //dibynedu
            if (theshift.NameFunction(1, id, DropDownList1.SelectedValue, txtTemplateName.Text, Session["userName"].ToString(), DateTime.Now.ToString("MM/dd/yyyy"), txtserviceCharge.Text, Session["CoCode"].ToString().Trim()) == true)
            {
                flag = InsertMapping();
            }
            if (flag == 1)
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "msg", "alert('Insered Successfully !');", true);
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "msg", "alert('Error in Insered Data !');", true);
            }
        }
        else
        {
            //dibyendu
            if (theshift.NameFunction(2, id, DropDownList1.SelectedValue, txtTemplateName.Text, Session["userName"].ToString(), DateTime.Now.ToString("MM/dd/yyyy"), txtserviceCharge.Text, Session["CoCode"].ToString().Trim()) == true)
            {
                theshift.TemplateFillFunction(2, id, null, null, null, null, null, null, Session["CoCode"].ToString().Trim());
                flag = InsertMapping();
            }

            if (flag == 1)
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "msg", "alert('updated Successfully !');", true);
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "msg", "alert('Error in updated Data !');", true);
            }
        }

        GridFill();
        ResetAllFields(1);
   
    }
    protected void Button2_Click(object sender, EventArgs e)
    {
        ResetAllFields(1);
    }

    private void DropDownFill()
    {
        this.DropDownList1.DataSource = theshift.GridFill(Session["CoCode"].ToString().Trim());
        this.DropDownList1.DataTextField = "CategoryName";
        this.DropDownList1.DataValueField = "TemplateCategoryId";
        this.DropDownList1.DataBind();
        this.DropDownList1.Items.Insert(0, new ListItem("--Select--", "0"));


        for (int i = 1; i <= 1; i++)
        {
            d1 = (DropDownList)Page.FindControl("ctl00$ContentPlaceHolder1$ddlconsumablegr" + i.ToString());
            d2 = (DropDownList)Page.FindControl("ctl00$ContentPlaceHolder1$ddlConsumableItem" + i.ToString());

            d1.DataSource = theshift.ServiceCate();
            d1.DataTextField = "ConGroupName";
            d1.DataValueField = "ConGrId";
            d1.DataBind();
            d1.Items.Insert(0, new ListItem("--Select--", "0"));

            d2.Items.Insert(0, new ListItem("--Select--", "0"));
        }

    }

    private void GridFill()
    {
        GridView1.SelectedIndex = -1;
        GridView1.DataSource = theshift.GridFillName(Session["CoCode"].ToString().Trim());
        GridView1.DataBind();
    }
    private void ResetAllFields(int mode)
    {
        if (mode == 1)
        {
            txtTemplateName.Text = "";
            TextBox4.Value = "";
            txtserviceCharge.Text = "";
            DropDownList1.SelectedIndex = 0;
            GridView8.DataSource = null;
            GridView8.DataBind();
            Session["CurrentTable"] = null;
            Button1.Text = "Submit";
        }

        TextBox t3;
        for (int i = 1; i <= 1; i++)
        {
            d1 = (DropDownList)Page.FindControl("ctl00$ContentPlaceHolder1$ddlconsumablegr" + i.ToString());
            d2 = (DropDownList)Page.FindControl("ctl00$ContentPlaceHolder1$ddlConsumableItem" + i.ToString());
            t1 = (TextBox)Page.FindControl("ctl00$ContentPlaceHolder1$txtActualQty" + i.ToString());
            t2 = (TextBox)Page.FindControl("ctl00$ContentPlaceHolder1$txtBillQty" + i.ToString());
            t3 = (TextBox)Page.FindControl("ctl00$ContentPlaceHolder1$txtPrice" + i.ToString());
            d1.SelectedIndex = 0; d2.SelectedIndex = 0;
            t1.Text = ""; t2.Text = ""; t3.Text = "";
        }
        if (Objmain.checkaccss(Session["CoCode"].ToString(), Session["UserRoleID"].ToString().Trim(), "SERVICE TEMPLATE", checkAccessType.InsertAction) == false)
        {
            Button1.Enabled = false;
        }

    }
    protected void GridView1_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        GridView1.PageIndex = e.NewPageIndex;
        GridFill();
    }
    protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "Select")
        {
            int index = Convert.ToInt32(e.CommandArgument);
            Label NameID = (Label)GridView1.Rows[index].FindControl("NameID");
            TextBox4.Value = NameID.Text;

            Label TemplateCategoryId = (Label)GridView1.Rows[index].FindControl("TemplateCategoryId");
            DropDownList1.SelectedValue = TemplateCategoryId.Text;

            Label ServiceTemplateName = (Label)GridView1.Rows[index].FindControl("ServiceTemplateName");
            txtTemplateName.Text = ServiceTemplateName.Text;

            Label ServiceCharge = (Label)GridView1.Rows[index].FindControl("ServiceCharge");
            txtserviceCharge.Text = ServiceCharge.Text;

            
            FillMap(NameID.Text);
            Tab1Func();
            Button1.Text = "Update";
            if (Objmain.checkaccss(Session["CoCode"].ToString(), Session["UserRoleID"].ToString().Trim(), "SERVICE TEMPLATE", checkAccessType.UpdateAction) == false)
            {
                Button1.Enabled = false;
            }
            else
            {
                Button1.Enabled = true;
            }
        }
    }
    protected void GridView1_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        Label NameID = (Label)GridView1.Rows[e.RowIndex].FindControl("NameID");
        if (theshift.NameFunction(3, NameID.Text, null, null, null, null, null, Session["CoCode"].ToString().Trim()) == true)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "msg", "alert('Deleted Successfully !');", true);
        }
        else
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "msg", "alert('Error in Deleted Data !');", true);
        }
        GridFill();
        ResetAllFields(1);
    }

    public void Tab1Func()
    {
        Tab1.CssClass = "Clicked";
        Tab2.CssClass = "Initial";
        MainView.ActiveViewIndex = 0;
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

    protected void ddlconsumablegr1_SelectedIndexChanged(object sender, EventArgs e)
    {
        ServiceFill(ddlconsumablegr1.SelectedValue, ddlConsumableItem1);
    }
   
    protected void Button7_Click(object sender, EventArgs e)
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

                row["RowID"] = lblid.Text;
                row["ConsumableGrId"] = lblConGrId.Text;
                row["ConGroupName"] = lblConGroupName.Text;
                row["ConsumableItemId"] = lblConItemID.Text;
                row["ConItemName"] = lblConItemName.Text;
                row["ActualQty"] = lblActualQty.Text;
                row["BillQty"] = lblBillQty.Text;
                row["Price"] = lblPrice.Text;

                dt.Rows.Add(row);
                row = dt.NewRow();
            }
        }
        row["ConsumableGrId"] = ddlconsumablegr1.SelectedValue;
        row["ConGroupName"] = ddlconsumablegr1.SelectedItem.Text;
        row["ConsumableItemId"] = ddlConsumableItem1.SelectedValue;
        row["ConItemName"] = ddlConsumableItem1.SelectedItem.Text;
        row["ActualQty"] = txtActualQty1.Text;
        row["BillQty"] = txtBillQty1.Text;
        row["Price"] = txtPrice1.Text;

        dt.Rows.Add(row);
        row = dt.NewRow();

        GridView8.DataSource = dt;
        GridView8.DataBind();

        Session["CurrentTable"] = dt;
        ResetAllFields(0);
    }

    protected void GridView8_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        GridView8.PageIndex = e.NewPageIndex;
        DataTable dt = (DataTable)Session["CurrentTable"];
        GridView8.DataSource = dt;
        GridView8.DataBind();
    }
    protected void GridView8_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
    {
        GridView8.EditIndex = -1;
        DataTable dt = (DataTable)Session["CurrentTable"];
        GridView8.DataSource = dt;
        GridView8.DataBind();
    }
    protected void GridView8_RowDeleting(object sender, GridViewDeleteEventArgs e)
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
        DataTable dt = (DataTable)Session["CurrentTable"];
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
        Session["CurrentTable"] = dt;
        GridView8.EditIndex = -1;

        GridView8.DataSource = dt;
        GridView8.DataBind();
    }
    protected void GridView8_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            if (Objmain.checkaccss(Session["CoCode"].ToString(), Session["UserRoleID"].ToString().Trim(), "SERVICE TEMPLATE", checkAccessType.UpdateAction) == false)
            {
                GridView8.Columns[8].Visible = false;
                e.Row.Cells[8].Visible = false;                
            }

            if (Objmain.checkaccss(Session["CoCode"].ToString(), Session["UserRoleID"].ToString().Trim(), "SERVICE TEMPLATE", checkAccessType.DeleteAction) == false)
            {
                GridView8.Columns[9].Visible = false;
                e.Row.Cells[9].Visible = false;
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

    protected void ddlConGroupName_SelectedIndexChanged1(object sender, EventArgs e)
    {

        DropDownList ddlConGroupName = (DropDownList)GridView8.Rows[GridView8.EditIndex].FindControl("ddlConGroupName");
        DropDownList ddlConItemName = (DropDownList)GridView8.Rows[GridView8.EditIndex].FindControl("ddlConItemName");
        ConsumableItemFill(ddlConGroupName.SelectedValue, ddlConItemName);
    }
    public void ConsumableItemFill(string value, DropDownList items)
    {
        items.Items.Clear();
        items.DataSource = thedocvisit.GetConsumableItem(Session["CoCode"].ToString().Trim(),value);
        items.DataTextField = "ConItemName";
        items.DataValueField = "ConItemID";
        items.DataBind();
        items.Items.Insert(0, new ListItem("--Select--", "0"));
    }
    protected void ddlConsumableItem1_SelectedIndexChanged(object sender, EventArgs e)
    {
        DataTable dt = thedocvisit.GetConsumableCharge(Session["CoCode"].ToString().Trim(),ddlConsumableItem1.SelectedValue);
        if (dt.Rows.Count > 0)
            txtPrice1.Text = dt.Rows[0][0].ToString();
    }

    [System.Web.Script.Services.ScriptMethod()]
    [System.Web.Services.WebMethod]
    public static List<string> Searchtemplate(string prefixText, int count)
    {
        string compcode = string.Empty;
        string yearcode = string.Empty;
        using (SqlConnection conn = new SqlConnection())
        {
            conn.ConnectionString = ConfigurationSettings.AppSettings["ConnectionString"].ToString();
            using (SqlCommand cmd = new SqlCommand())
            {
                compcode = HttpContext.Current.Session["CoCode"].ToString();
                yearcode = HttpContext.Current.Session["YearCode"].ToString();
                cmd.CommandText = "select distinct ServiceTemplateName as Name from IPD_Service_Cons_Template where compcode=@Compcode and ServiceTemplateName like @SearchText +'%'";
                cmd.Parameters.AddWithValue("@SearchText", prefixText);
                cmd.Parameters.AddWithValue("@Compcode", compcode);
                cmd.Connection = conn;
                conn.Open();
                List<string> customers = new List<string>();
                using (SqlDataReader sdr = cmd.ExecuteReader())
                {
                    while (sdr.Read())
                    {
                        customers.Add(sdr["Name"].ToString());
                    }
                }
                conn.Close();
                return customers;
            }
        }
    }
    protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            if (Objmain.checkaccss(Session["CoCode"].ToString(), Session["UserRoleID"].ToString().Trim(), "SERVICE TEMPLATE", checkAccessType.DeleteAction) == false)
            {
                coldel.Visible = false;
                e.Row.Cells[6].Visible = false;
            }
        }
    }
}