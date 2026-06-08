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
using System.Globalization;
 
public partial class Master_MedicineMaster : System.Web.UI.Page
{
    Main Objmain = new Main(ConfigurationSettings.AppSettings["ConnectionString"].ToString());
    MD_MedicineMaster theHelper = new MD_MedicineMaster(ConfigurationSettings.AppSettings["ConnectionString"].ToString());
    protected void Page_Load(object sender, EventArgs e)
    {
        Page.Title = "Medicine Master";
        if (Session["userName"] == null)
        {
            Response.Redirect("../LoginPage.aspx");
        }
        if (Objmain.checkaccss(Session["CoCode"].ToString(), Session["UserRoleID"].ToString().Trim(), "MEDICINE MASTER", checkAccessType.ViewAction) == false)
        {
            Response.Redirect("../AccessDenied.aspx");
        }

        if (Objmain.checkaccss(Session["CoCode"].ToString(), Session["UserRoleID"].ToString().Trim(), "MEDICINE MASTER", checkAccessType.InsertAction) == false)
        {
            Button1.Enabled = false;
        }
        if (Objmain.checkaccss(Session["CoCode"].ToString(), Session["UserRoleID"].ToString().Trim(), "MEDICINE MASTER", checkAccessType.DeleteAction) == false)
        {
            //coldel.Visible = false;
            GridView1.Columns[14].Visible = false;
        }
        if (Session["back_to_purchase"] == "1")
        {
            Button5.Visible = true;
        }
        else
        {
            Button5.Visible = false;
        }
        if (!IsPostBack)
        {
            ddlitemType.Items.Clear();
            this.ddlitemType.Items.Insert(0, new ListItem("--Select--", "0"));
            this.ddlitemType.Items.Insert(1, new ListItem("Medicine", "M"));
            this.ddlitemType.Items.Insert(2, new ListItem("Reagent", "G"));
            this.ddlitemType.Items.Insert(3, new ListItem("Consumables", "C"));
            this.ddlitemType.Items.Insert(3, new ListItem("Injections", "I"));
            this.ddlitemType.Items.Insert(4, new ListItem("Other", "O"));
            


            DropDownList6.Items.Clear();
            this.DropDownList6.DataSource = theHelper.DropdownPotency(Session["CoCode"].ToString().Trim());
            this.DropDownList6.DataTextField = "PotencyName";
            this.DropDownList6.DataValueField = "RowId";
            this.DropDownList6.DataBind();
            this.DropDownList6.Items.Insert(0, new ListItem("--Select--", "0"));

            DropDownList7.Items.Clear();
            this.DropDownList7.DataSource = theHelper.DropdownSyring(Session["CoCode"].ToString().Trim());
            this.DropDownList7.DataTextField = "ConItemName";
            this.DropDownList7.DataValueField = "ConItemID";
            this.DropDownList7.DataBind();
            this.DropDownList7.Items.Insert(0, new ListItem("--Select--", "0"));


            GridFill();
            DropDownFill();
            Tab1Func();
        }
    }


    [System.Web.Script.Services.ScriptMethod()]
    [System.Web.Services.WebMethod]

    public static List<string> SearchCustomers(string prefixText, int count)
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
                cmd.CommandText = "select distinct MedicineName as Name from IPD_MedicineMaster where compcode=@Compcode and MedicineName like @SearchText + '%'";
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


    private void GridFill()
    {
        GridView1.DataSource = theHelper.GetAllMedicine( Session["CoCode"].ToString().Trim(),"", "", TextBox3.Text);
        GridView1.DataBind();
    }
    private void ResetAllFields()
    {

         txtMedicineName.Text = "";
        DropDownList1.SelectedIndex = 0;
        DropDownList2.SelectedIndex = 0;
        DropDownList7.SelectedIndex = 0;
          TextBox1.Text = "";
        DropDownList3.SelectedIndex = 0;
        TextBox2.Text = "";
        DropDownList5.SelectedIndex = 0;
        DropDownList6.SelectedIndex = 0;
        DropDownList4.SelectedIndex = 0;
        HdnItemcode.Value = "";
        txtHsnNo.Text = "";
        txtGenericName.Text = "";
        txtIGstRate.Text = "0.00";
        txtCGstRate.Text = "0.00";
        txtSGstRate.Text = "0.00";
        chkPurGst.Checked = false;
         Button1.Text = "Submit";

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

    protected void GridView1_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        GridView1.PageIndex = e.NewPageIndex;
        GridFill();
    }
    protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "Select")
        {
            int retval;
            int index = Convert.ToInt32(e.CommandArgument);
            Label lblMedicineID = (Label)GridView1.Rows[index].FindControl("MedicineID");
             HiddenField1.Value = lblMedicineID.Text;

             Label Icode = (Label)GridView1.Rows[index].FindControl("Icode");
             HdnItemcode.Value = Icode.Text;

            Label lblMedicineName = (Label)GridView1.Rows[index].FindControl("MedicineName");
            string[] mednamesplit = lblMedicineName.Text.Split(' ');
            retval = -1;
            if (mednamesplit.Length > 1)
            {
                retval = SearchText(mednamesplit[1], DropDownList6);
            }

            DropDownList6.SelectedIndex = retval;
            
            
            if(retval<0)
            {
                txtMedicineName.Text = lblMedicineName.Text;
            }
            else
            {

                txtMedicineName.Text = mednamesplit[0];
            }
            Label lblStockAlert = (Label)GridView1.Rows[index].FindControl("lblStockAlert");
            TextBox1.Text = lblStockAlert.Text;

            Label lblGenericName = (Label)GridView1.Rows[index].FindControl("lblGenericName");
            txtGenericName.Text = lblGenericName.Text;

            Label lblMedicineGroupID = (Label)GridView1.Rows[index].FindControl("MedicineGroupID");
            DropDownList2.SelectedValue = lblMedicineGroupID.Text;

            //Label lblsub = (Label)GridView1.Rows[index].FindControl("lblsub");
            //subFill(lblMedicineGroupID.Text,DropDownList4);
            //DropDownList4.SelectedValue = lblsub.Text;

             DropDownFill();
             Label lblMCode = (Label)GridView1.Rows[index].FindControl("MCode");
             DropDownList1.SelectedValue = lblMCode.Text;

 

            Label UnitID = (Label)GridView1.Rows[index].FindControl("UnitID");
            DropDownList3.SelectedValue = UnitID.Text;


            //Label lblDose = (Label)GridView1.Rows[index].FindControl("lblDose");
            //string[] split;
            //if (lblDose.Text != "")
            //{
            //    split = lblDose.Text.Split(' ');
            //    DropDownList7.SelectedValue = split[0];
            //}
            Label lblConversionFactor = (Label)GridView1.Rows[index].FindControl("lblConversionFactor");
            TextBox2.Text = lblConversionFactor.Text;

            Label lblSellingUnit = (Label)GridView1.Rows[index].FindControl("lblSellingUnit");
            DropDownList5.SelectedValue = lblSellingUnit.Text;

            Label lblitype = (Label)GridView1.Rows[index].FindControl("lblitype");
            ddlitemType.SelectedValue = lblitype.Text;

            Label lblHsnCode = (Label)GridView1.Rows[index].FindControl("lblHsnCode");
            txtHsnNo.Text = lblHsnCode.Text;

            Label lblIgstrate = (Label)GridView1.Rows[index].FindControl("lblIgstrate");
            txtIGstRate.Text = lblIgstrate.Text;

            Label lblCgstrate = (Label)GridView1.Rows[index].FindControl("lblCgstrate");
            txtCGstRate.Text = lblCgstrate.Text;

            Label lblSgstrate = (Label)GridView1.Rows[index].FindControl("lblSgstrate");
            txtSGstRate.Text = lblSgstrate.Text;

            Label lblPurGst = (Label)GridView1.Rows[index].FindControl("lblPurGst");
            if (lblPurGst.Text == "0")
            {
                chkPurGst.Checked = false;
            }
            else if (lblPurGst.Text == "")
            {
                chkPurGst.Checked = false;
            }
            else
            {
                chkPurGst.Checked = true;
            }
            Tab1Func();
            Button1.Text = "Update";
            if (Objmain.checkaccss(Session["CoCode"].ToString(), Session["UserRoleID"].ToString().Trim(), "MEDICINE MASTER", checkAccessType.UpdateAction) == false)
            {
                Button1.Enabled = false;
            }
            else
            {
                Button1.Enabled = true;
            }
        }
    }
    private void DropDownFill()
    {
        string compcode = Session["CoCode"].ToString().Trim();
        
        DropDownList1.Items.Clear();
        this.DropDownList1.DataSource = theHelper.DropdownID1(compcode);
        this.DropDownList1.DataTextField = "MName";
        this.DropDownList1.DataValueField = "MCode";
        this.DropDownList1.DataBind();
        this.DropDownList1.Items.Insert(0, new ListItem("--Select--", "0"));

        DropDownList2.Items.Clear();
        this.DropDownList2.DataSource = theHelper.DropdownID2(compcode);
        this.DropDownList2.DataTextField = "MedicineGroupName";
        this.DropDownList2.DataValueField = "MedicineGroupID";
        this.DropDownList2.DataBind();
        this.DropDownList2.Items.Insert(0, new ListItem("--Select--", "0"));


        //DropDownList8.Items.Clear();
        //this.DropDownList8.DataSource = theHelper.DropdownID2(compcode);
        //this.DropDownList8.DataTextField = "MedicineGroupName";
        //this.DropDownList8.DataValueField = "MedicineGroupID";
        //this.DropDownList8.DataBind();
        //this.DropDownList8.Items.Insert(0, new ListItem("--Select--", "0"));

        DropDownList3.Items.Clear();
        this.DropDownList3.DataSource = theHelper.DropdownID3(compcode);
        this.DropDownList3.DataTextField = "UnitName";
        this.DropDownList3.DataValueField = "UnitId";
        this.DropDownList3.DataBind();
        this.DropDownList3.Items.Insert(0, new ListItem("--Select--", "0"));

        DropDownList5.Items.Clear();
        this.DropDownList5.DataSource = theHelper.DropdownID3(compcode);
        this.DropDownList5.DataTextField = "UnitName";
        this.DropDownList5.DataValueField = "UnitId";
        this.DropDownList5.DataBind();
        this.DropDownList5.Items.Insert(0, new ListItem("--Select--", "0"));

        this.DropDownList4.Items.Insert(0, new ListItem("--Select--", "0"));
    }
    protected void GridView1_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        Label lblMedicineID = (Label)GridView1.Rows[e.RowIndex].FindControl("MedicineID");
        Label Icode = (Label)GridView1.Rows[e.RowIndex].FindControl("Icode");
        if (theHelper.DeleteMedicineMaster(Convert.ToInt32(lblMedicineID.Text), Session["CoCode"].ToString().Trim(), Session["YearCode"].ToString().Trim(), Icode.Text) == true)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "msg", "alert('Deleted  Successfully !');", true);
        }
        else
            ScriptManager.RegisterStartupScript(this, this.GetType(), "msg", "alert('Error in Deleted Data  !');", true);
        GridFill();
        ResetAllFields();
    }

    protected void Button1_Click(object sender, EventArgs e)
    {
        string medicinename="";
        string injection="";
        string purgst = "";
        string itemtype = ddlitemType.SelectedValue.ToString();
        if (DropDownList6.SelectedIndex != 0)
            medicinename = txtMedicineName.Text + " " + DropDownList6.SelectedItem.Text;
        else
            medicinename = txtMedicineName.Text;

        if(DropDownList7.SelectedIndex==0)
            injection="";
        else
            injection=DropDownList7.SelectedValue;

        if (chkPurGst.Checked == true)
        {
            purgst = "1";
        }
        else
        {
            purgst="0";
        }
       if (Button1.Text == "Submit")
        {
            if (theHelper.InsertMedicineMaster(injection, DropDownList5.SelectedValue, TextBox2.Text, TextBox1.Text, DropDownList4.SelectedValue, medicinename, DropDownList1.SelectedValue, DropDownList2.SelectedValue, DropDownList3.SelectedValue, Session["userName"].ToString(), Session["CoCode"].ToString().Trim(), Session["YearCode"].ToString().Trim(), itemtype, txtGenericName.Text.Trim(), txtHsnNo.Text.Trim(), txtIGstRate.Text, txtCGstRate.Text, txtSGstRate.Text, purgst) == true)
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "msg", "alert('Inserted  Successfully !');", true);
                ResetAllFields();
            }
            else
                ScriptManager.RegisterStartupScript(this, this.GetType(), "msg", "alert('Duplicate Entry Cannot Possible  !');", true);
        }
        else
        {
            if (theHelper.UpdateMedicineMaster(injection, DropDownList5.SelectedValue, TextBox2.Text, TextBox1.Text, DropDownList4.SelectedValue, HiddenField1.Value, medicinename, DropDownList1.SelectedValue, DropDownList2.SelectedValue, DropDownList3.SelectedValue, Session["CoCode"].ToString().Trim(), Session["YearCode"].ToString().Trim(), HdnItemcode.Value, itemtype, txtGenericName.Text.Trim(), txtHsnNo.Text.Trim(), txtIGstRate.Text, txtCGstRate.Text, txtSGstRate.Text, purgst) == true)
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "msg", "alert('Updated  Successfully !');", true);
                ResetAllFields();
                Button1.Text = "Submit";
            }
            else
                ScriptManager.RegisterStartupScript(this, this.GetType(), "msg", "alert('Duplicate Entry Cannot Possible  !');", true);
      
        }

 
        GridFill();      
    }

    protected void Button2_Click(object sender, EventArgs e)
    {
        ResetAllFields();  
    }

    protected void DropDownList2_SelectedIndexChanged(object sender, EventArgs e)
    {
        subFill(DropDownList2.SelectedValue,DropDownList4);    
    }

    public void subFill(string value,DropDownList d1)
    {
        d1.Items.Clear();
        d1.DataSource = theHelper.DropdownSubGroup(value, Session["CoCode"].ToString().Trim());
         d1.DataTextField = "SubGrName";
        d1.DataValueField = "ID";
        d1.DataBind();
        d1.Items.Insert(0, new ListItem("--Select--", "0"));
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
    protected void GridView1_SelectedIndexChanged(object sender, EventArgs e)
    {

    }
    protected void Button3_Click(object sender, EventArgs e)
    { 
        GridFill();
    }
    //protected void DropDownList8_SelectedIndexChanged(object sender, EventArgs e)
    //{
    //    subFill(DropDownList8.SelectedValue, DropDownList9);
    //}
    protected void Button5_Click(object sender, EventArgs e)
    {
        Session["back_to_purchase"] = "";
        Response.Redirect("../Medicine/PurchaseMedicine.aspx");
    }
    protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            //if (Objmain.checkaccss(Session["CoCode"].ToString(), Session["UserRoleID"].ToString().Trim(), "MEDICINE MASTER", checkAccessType.DeleteAction) == false)
            //{
            //    //coldel.Visible = false;
            //    e.Row.Cells[14].Visible = false;
            //}
        }
    }
}