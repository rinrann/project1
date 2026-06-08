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

 
public partial class IPD_AddConsumable : System.Web.UI.Page
{
    Main Objmain = new Main(ConfigurationSettings.AppSettings["ConnectionString"].ToString());
    AddConsumable theaddConsumable = new AddConsumable(ConfigurationSettings.AppSettings["ConnectionString"].ToString());
    PatientDoctorVisit thedocvisit = new PatientDoctorVisit(ConfigurationSettings.AppSettings["ConnectionString"].ToString());
    DropDownList d1, d2, d3, d4;
    TextBox t1, t2;
    protected void Page_Load(object sender, EventArgs e)
    {
        ScriptManager.RegisterStartupScript(this, this.GetType(), "msg", "GetDatetime();", true);
        Page.Title = "Add Consumable";
        if (Session["userName"] == null)
        {
            Response.Redirect("../LoginPage.aspx");
        }
        if (Objmain.checkaccss(Session["CoCode"].ToString(), Session["UserRoleID"].ToString().Trim(), "ADD CONSUMABLE", checkAccessType.ViewAction) == false)
        {
            Response.Redirect("../AccessDenied.aspx");
        }

        if (Objmain.checkaccss(Session["CoCode"].ToString(), Session["UserRoleID"].ToString().Trim(), "ADD CONSUMABLE", checkAccessType.InsertAction) == false)
        {
            Button1.Enabled = false;
        }
        if (Objmain.checkaccss(Session["CoCode"].ToString(), Session["UserRoleID"].ToString().Trim(), "ADD CONSUMABLE", checkAccessType.DeleteAction) == false)
        {
            GridView1.Columns[15].Visible = false;
        }
        if (Objmain.checkaccss(Session["CoCode"].ToString(), Session["UserRoleID"].ToString().Trim(), "ADD CONSUMABLE", checkAccessType.DeleteAction) == false)
        {
            GridView1.Columns[16].Visible = false;
        }
        if (!IsPostBack)
        {
            txtdate.Text = DateTime.Now.ToString("dd/MM/yyyy");
            DropDownFill();
            Tab1Func();
            if (Session["RegNo"] != null)
            {
                TextBox2.Text = Session["RegNo"].ToString();
                GridFill();
                GridFillFromService();
            }
        }
        Session["RegNo"] = null;
    }

    private void GridFillFromService()
    {
        DataTable dt = theaddConsumable.GridFillFromService(Session["CoCode"].ToString().Trim(),TextBox2.Text);
        GridView3.DataSource = dt;
        GridView3.DataBind();
        Session["CurrentTableConsumable"] = dt;
    }
    private void GridFill()
    {
        DataSet ds = theaddConsumable.GetAllPatientConsumable(Session["CoCode"].ToString().Trim(),TextBox2.Text);
        DropDownList4.SelectedValue = ds.Tables[2].Rows[0][1].ToString();
        ddd3(ds.Tables[2].Rows[0][1].ToString(), DropDownList3);
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
        ddlconsumablegr1.SelectedIndex = 0; ddlConsumableItem1.SelectedIndex = 0; txtActualQty1.Text = ""; txtBillQty1.Text = "";
        Button1.Text = "Submit";
        Button5.Enabled = true;
        if (Objmain.checkaccss(Session["CoCode"].ToString(), Session["UserRoleID"].ToString().Trim(), "ADD CONSUMABLE", checkAccessType.InsertAction) == false)
        {
            Button1.Enabled = false;
        }
    }
    protected void Button1_Click(object sender, EventArgs e)
    {
        System.Globalization.DateTimeFormatInfo dtf = new DateTimeFormatInfo();
        DateTime testdate = DateTime.ParseExact(txtdate.Text, "dd/MM/yyyy", dtf);

        bool flag=true;
        
        if (Button1.Text == "Submit")
        {
            for (int i = 0; i < GridView3.Rows.Count; i++)
            {
                Label lblConGrId = (Label)GridView3.Rows[i].FindControl("lblConGrId");
                Label lblConGroupName = (Label)GridView3.Rows[i].FindControl("lblConGroupName");
                Label lblConItemID = (Label)GridView3.Rows[i].FindControl("lblConItemID");
                Label lblConItemName = (Label)GridView3.Rows[i].FindControl("lblConItemName");
                Label lblActualQty = (Label)GridView3.Rows[i].FindControl("lblActualQty");
                Label lblBillQty = (Label)GridView3.Rows[i].FindControl("lblBillQty");

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
                Session["CurrentTableConsumable"] = null;

                //ResetAllFields();
                //ScriptManager.RegisterStartupScript(this, this.GetType(), "msg", "alert('Inserted Successfully !');", true);
                Response.Redirect("../IPD/AdmissionPatientList.aspx");
            }
            else
                ScriptManager.RegisterStartupScript(this, this.GetType(), "msg", "alert('Error in inserted data !');", true);
        }
        else
        {
            if (theaddConsumable.Update_Delete_DocVisit(1, HiddenField1.Value, ddlconsumablegr1.SelectedValue, ddlConsumableItem1.SelectedValue, testdate.ToString("yyyy-MM-dd"), txtActualQty1.Text, txtBillQty1.Text, DropDownList4.SelectedValue, DropDownList3.SelectedValue, DropDownList5.SelectedValue, DropDownList6.SelectedValue, Session["CoCode"].ToString().Trim(), Session["userName"].ToString().Trim()) == true)
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "msg", "alert('Updated Successfully !');", true);
                Button1.Text = "Submit";

            }
            else
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "msg", "alert('Error in Updated Data !');", true);
            }
        }
        GridFill();
        ResetAllFields();
        Session["RegNo"] = null; 
     }
    protected void Button2_Click(object sender, EventArgs e)
    {
        ResetAllFields();
    }
    private void DropDownFill()
    {

        ddlconsumablegr1.Items.Clear();
        ddlconsumablegr1.DataSource = thedocvisit.GetConsumableGroup(Session["CoCode"].ToString().Trim());
        ddlconsumablegr1.DataTextField = "ConGroupName";
        ddlconsumablegr1.DataValueField = "ConGrId";
        ddlconsumablegr1.DataBind();
        ddlconsumablegr1.Items.Insert(0, new ListItem("--Select--", "0"));

        ddlConsumableItem1.Items.Insert(0, new ListItem("--Select--", "0"));

        DropDownList65.Items.Clear();
        this.DropDownList65.DataSource = thedocvisit.GetServiceConsumableTemplateeGroup(Session["CoCode"].ToString().Trim());
        this.DropDownList65.DataTextField = "CategoryName";
        this.DropDownList65.DataValueField = "TemplateCategoryId";
        this.DropDownList65.DataBind();
        this.DropDownList65.Items.Insert(0, new ListItem("--Select--", "0"));


        this.DropDownList66.Items.Insert(0, new ListItem("--Select--", "0"));

        this.DropDownList4.DataSource = theaddConsumable.DropdownDOCTORTYPE(Session["CoCode"].ToString().Trim());
        this.DropDownList4.DataTextField = "TypeName";
        this.DropDownList4.DataValueField = "DocTypeId";
        this.DropDownList4.DataBind();
        this.DropDownList4.Items.Insert(0, new ListItem("--Select--", "0"));

        this.DropDownList5.DataSource = theaddConsumable.DropdownDOCTORTYPE(Session["CoCode"].ToString().Trim());
        this.DropDownList5.DataTextField = "TypeName";
        this.DropDownList5.DataValueField = "DocTypeId";
        this.DropDownList5.DataBind();
        this.DropDownList5.Items.Insert(0, new ListItem("--Select--", "0"));

    }
    protected void GridView1_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        GridView1.PageIndex = e.NewPageIndex;
        GridFill();
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

    public void GridFillConsumable()
    {
        GridView3.DataSource = thedocvisit.GridfillConsumableDetails(Session["CoCode"].ToString().Trim(),TextBox2.Text);
        GridView3.DataBind();
    }

    protected void GridView3_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        GridView3.PageIndex = e.NewPageIndex;
        DataTable dt = (DataTable)Session["CurrentTableConsumable"];
        GridView3.DataSource = dt;
        GridView3.DataBind();
    }

    protected void ddlConGroupName_SelectedIndexChanged(object sender, EventArgs e)
    {

        DropDownList ddlConGroupName = (DropDownList)GridView3.Rows[GridView3.EditIndex].FindControl("ddlConGroupName");
        DropDownList ddlConItemName = (DropDownList)GridView3.Rows[GridView3.EditIndex].FindControl("ddlConItemName");
        ConsumableItemFill(ddlConGroupName.SelectedValue, ddlConItemName);
    }


    protected void GridView3_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
    {

        GridView3.EditIndex = -1;
        DataTable dt = (DataTable)Session["CurrentTableConsumable"];
        GridView3.DataSource = dt;
        GridView3.DataBind();
    }
    protected void GridView3_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            Label lblid = (Label)e.Row.FindControl("lblid");
            lblid.Text = ((GridView3.PageIndex * GridView3.PageSize) + e.Row.RowIndex + 1).ToString();
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


            ConsumableItemFill("0", ddlConItemName);
            ddlConItemName.SelectedValue = lblConItemID.Text;
             
        }
    }
    protected void GridView3_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        if (Session["CurrentTableConsumable"] != null)
        {
            DataTable CurrentTable = (DataTable)Session["CurrentTableConsumable"];
            DataRow drCurrentRow = null;
            int rowIndex = Convert.ToInt32(e.RowIndex);
            if (CurrentTable.Rows.Count > 1)
            {
                CurrentTable.Rows.Remove(CurrentTable.Rows[rowIndex]);
                drCurrentRow = CurrentTable.NewRow();
                Session["CurrentTable"] = CurrentTable;
                GridView3.DataSource = CurrentTable;
                GridView3.DataBind();

                for (int i = 0; i < GridView3.Rows.Count - 1; i++)
                {
                    GridView3.Rows[i].Cells[0].Text = Convert.ToString(i + 1);
                }
            }
        } 
    }
    protected void GridView3_RowEditing(object sender, GridViewEditEventArgs e)
    {
        GridView3.EditIndex = e.NewEditIndex;
        DataTable dt = (DataTable)Session["CurrentTableConsumable"];
        GridView3.DataSource = dt;
        GridView3.DataBind();
    }
    protected void GridView3_RowUpdating(object sender, GridViewUpdateEventArgs e)
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


        if (GridView3.Rows.Count > 0)
        {
            for (int i = 0; i < GridView3.Rows.Count; i++)
            {
                Label lblid = (Label)GridView3.Rows[i].FindControl("lblid");
                Label lblConGrId = (Label)GridView3.Rows[i].FindControl("lblConGrId");
                Label lblConGroupName = (Label)GridView3.Rows[i].FindControl("lblConGroupName");
                Label lblConItemID = (Label)GridView3.Rows[i].FindControl("lblConItemID");
                Label lblConItemName = (Label)GridView3.Rows[i].FindControl("lblConItemName");
                Label lblActualQty = (Label)GridView3.Rows[i].FindControl("lblActualQty");
                Label lblBillQty = (Label)GridView3.Rows[i].FindControl("lblBillQty");
                Label lblPrice = (Label)GridView3.Rows[i].FindControl("lblPrice"); 


                Label EditSerial = (Label)GridView3.Rows[e.RowIndex].FindControl("lblid");
                DropDownList ddlConGroupName = (DropDownList)GridView3.Rows[e.RowIndex].FindControl("ddlConGroupName");
                DropDownList ddlConItemNameval = (DropDownList)GridView3.Rows[e.RowIndex].FindControl("ddlConItemName");
                TextBox txtActualQty = (TextBox)GridView3.Rows[e.RowIndex].FindControl("txtActualQty");
                TextBox txtBillQty = (TextBox)GridView3.Rows[e.RowIndex].FindControl("txtBillQty");
                TextBox txtPrice = (TextBox)GridView3.Rows[e.RowIndex].FindControl("txtPrice");
               
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

        Session["CurrentTableConsumable"] = dt;
         
        GridView3.EditIndex = -1;

        GridView3.DataSource = dt;
        GridView3.DataBind();
    }


    protected void DropDownList65_SelectedIndexChanged(object sender, EventArgs e)
    {
        DropDownList66.Items.Clear();
        this.DropDownList66.DataSource = thedocvisit.GetServiceConsumableTemplate(Session["CoCode"].ToString().Trim(),DropDownList65.SelectedValue);
        this.DropDownList66.DataTextField = "ServiceTemplateName";
        this.DropDownList66.DataValueField = "NameID";
        this.DropDownList66.DataBind();
        this.DropDownList66.Items.Insert(0, new ListItem("--Select--", "0"));
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


    protected void ddlconsumablegr1_SelectedIndexChanged(object sender, EventArgs e)
    {
        ConsumableItemFill(ddlconsumablegr1.SelectedValue, ddlConsumableItem1);
    }

    private void FillConsumable(string NameId)
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

        if (GridView3.Rows.Count > 0)
        {
            for (int i = 0; i < GridView3.Rows.Count; i++)
            {
                Label lblid = (Label)GridView3.Rows[i].FindControl("lblid");
                Label lblConGrId = (Label)GridView3.Rows[i].FindControl("lblConGrId");
                Label lblConGroupName = (Label)GridView3.Rows[i].FindControl("lblConGroupName");
                Label lblConItemID = (Label)GridView3.Rows[i].FindControl("lblConItemID");
                Label lblConItemName = (Label)GridView3.Rows[i].FindControl("lblConItemName");
                Label lblActualQty = (Label)GridView3.Rows[i].FindControl("lblActualQty");
                Label lblBillQty = (Label)GridView3.Rows[i].FindControl("lblBillQty");
                Label lblPrice = (Label)GridView3.Rows[i].FindControl("lblPrice");

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

        DataTable dtcon = thedocvisit.GetConsumableTemplateMapping(Session["CoCode"].ToString().Trim(),NameId);
        if (dtcon.Rows.Count > 0)
        {
            for (int i = 0; i < dtcon.Rows.Count; i++)
            {
                row["ConsumableGrId"] = dtcon.Rows[i]["ConGrId"].ToString();
                row["ConGroupName"] = dtcon.Rows[i]["ConGroupName"].ToString();
                row["ConsumableItemId"] = dtcon.Rows[i]["ConItemID"].ToString();
                row["ConItemName"] = dtcon.Rows[i]["ConItemName"].ToString();
                row["ActualQty"] = dtcon.Rows[i]["ActualQty"].ToString();
                row["BillQty"] = dtcon.Rows[i]["BillQty"].ToString();
                row["Price"] = dtcon.Rows[i]["PriceperUnit"].ToString();

                dt.Rows.Add(row);
                row = dt.NewRow();
            }
        }
        GridView3.DataSource = dt;
        GridView3.DataBind();
        Session["CurrentTableConsumable"] = dt;
    }

    protected void DropDownList66_SelectedIndexChanged(object sender, EventArgs e)
    {
        FillConsumable(DropDownList66.SelectedValue);
    }
    protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "Select")
        {
            int index = Convert.ToInt32(e.CommandArgument);
            lblError.Text = "";
            Label lblid = (Label)GridView1.Rows[index].FindControl("lblid");
            HiddenField1.Value = lblid.Text;

            //Label lblRegno = (Label)GridView1.Rows[index].FindControl("lblRegno");
            //TextBox2.Text = lblRegno.Text;
            //Label lbllname = (Label)GridView1.Rows[index].FindControl("lbllname");
            //TextBox3.Text = lbllname.Text;
            //Label lbladate = (Label)GridView1.Rows[index].FindControl("lbladate");
            //TextBox5.Text = lbladate.Text;
            //Label lblbedno = (Label)GridView1.Rows[index].FindControl("lblbedno");
            //TextBox4.Text = lblbedno.Text;

            Label lblConGroupName = (Label)GridView1.Rows[index].FindControl("lblConGroupName");
            ddlconsumablegr1.SelectedIndex = SearchText(lblConGroupName.Text, ddlconsumablegr1);

            Label lblConsumableGrpId = (Label)GridView1.Rows[index].FindControl("lblConsumableGrpId");

            ConItemFill(lblConsumableGrpId.Text, ddlConsumableItem1);
            Label lblConItemName = (Label)GridView1.Rows[index].FindControl("lblConItemName");
            ddlConsumableItem1.SelectedIndex = SearchText(lblConItemName.Text, ddlConsumableItem1);

            Label lbldoctype = (Label)GridView1.Rows[index].FindControl("lbldoctype");
            DropDownList4.SelectedIndex = SearchIndex(lbldoctype.Text, DropDownList4);

            ddd3(lbldoctype.Text,DropDownList3);
            Label lbladvicedby = (Label)GridView1.Rows[index].FindControl("lbladvicedby");
            DropDownList3.SelectedIndex = SearchIndex(lbladvicedby.Text, DropDownList3);

        

            Label lbladditiondoctype = (Label)GridView1.Rows[index].FindControl("lbladditiondoctype");
            DropDownList5.SelectedIndex = SearchIndex(lbladditiondoctype.Text, DropDownList5);

            ddd3(lbladditiondoctype.Text, DropDownList6);
            Label lbladddoc = (Label)GridView1.Rows[index].FindControl("lbladddoc");
            DropDownList6.SelectedIndex = SearchIndex(lbladddoc.Text, DropDownList6);

            Label lblisdate = (Label)GridView1.Rows[index].FindControl("lblisdate");
            txtdate.Text = lblisdate.Text;

            Label lblActualQty = (Label)GridView1.Rows[index].FindControl("lblActualQty");
            txtActualQty1.Text = lblActualQty.Text;

            Label lblBillQty = (Label)GridView1.Rows[index].FindControl("lblBillQty");
            txtBillQty1.Text = lblBillQty.Text;
            Tab1Func();
            Button1.Text = "Update";

            Button5.Enabled = false;

            if (Objmain.checkaccss(Session["CoCode"].ToString(), Session["UserRoleID"].ToString().Trim(), "ADD CONSUMABLE", checkAccessType.UpdateAction) == false)
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

        ddd3(DropDownList4.SelectedValue,DropDownList3);

    }

    public void ddd3(string val,DropDownList ddl)
    {
        ddl.Items.Clear();
        ddl.DataSource = theaddConsumable.DropdownDoctor(val);
        ddl.DataTextField = "doc_name";
        ddl.DataValueField = "doc_id";
        ddl.DataBind();
        ddl.Items.Insert(0, new ListItem("--Select--", "0"));
    } 
  
    protected void DropDownList5_SelectedIndexChanged(object sender, EventArgs e)
    {
        ddd3(DropDownList5.SelectedValue,DropDownList6);
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

    private void ConItemFill(string value,DropDownList ddl)
    {
        ddl.DataSource = theaddConsumable.DropdownConItem(Session["CoCode"].ToString().Trim(),value);
        ddl.DataTextField = "ConItemName";
        ddl.DataValueField = "ConItemID";
        ddl.DataBind();
        ddl.Items.Insert(0, new ListItem("--Select--", "0"));
    }
    //protected void DropDownList1_SelectedIndexChanged(object sender, EventArgs e)
    //{
    //    ConItemFill(DropDownList1.SelectedValue);       
    //}
    protected void Button5_Click(object sender, EventArgs e)
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

        if (GridView3.Rows.Count > 0)
        {
            for (int i = 0; i < GridView3.Rows.Count; i++)
            {
                Label lblid = (Label)GridView3.Rows[i].FindControl("lblid");
                Label lblConGrId = (Label)GridView3.Rows[i].FindControl("lblConGrId");
                Label lblConGroupName = (Label)GridView3.Rows[i].FindControl("lblConGroupName");
                Label lblConItemID = (Label)GridView3.Rows[i].FindControl("lblConItemID");
                Label lblConItemName = (Label)GridView3.Rows[i].FindControl("lblConItemName");
                Label lblActualQty = (Label)GridView3.Rows[i].FindControl("lblActualQty");
                Label lblBillQty = (Label)GridView3.Rows[i].FindControl("lblBillQty");
                Label lblPrice = (Label)GridView3.Rows[i].FindControl("lblPrice");

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


        dt.Rows.Add(row);
        row = dt.NewRow();

        GridView3.DataSource = dt;
        GridView3.DataBind();

        Session["CurrentTableConsumable"] = dt;
        ResetAllFields();
    }


    protected void GridView1_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        Label lblid = (Label)GridView1.Rows[e.RowIndex].FindControl("lblid");
        if (theaddConsumable.Update_Delete_DocVisit(2, lblid.Text, null, null, null, null, null, null, null, null, null, Session["CoCode"].ToString().Trim(), Session["userName"].ToString().Trim()) == true)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "msg", "alert('Deleted Successfully !');", true);
        }
        else
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "msg", "alert(' Error in Deleted Data !');", true);
        }
        GridFill();
    }
    protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            if (Objmain.checkaccss(Session["CoCode"].ToString(), Session["UserRoleID"].ToString().Trim(), "ADD CONSUMABLE", checkAccessType.UpdateAction) == false)
            {
                e.Row.Cells[15].Visible = false;
            }
            if (Objmain.checkaccss(Session["CoCode"].ToString(), Session["UserRoleID"].ToString().Trim(), "ADD CONSUMABLE", checkAccessType.DeleteAction) == false)
            {
                e.Row.Cells[16].Visible = false;
            }
        }
    }
}