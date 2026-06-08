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
 
public partial class Master_RoleWiseAccess : System.Web.UI.Page
{
    Main Objmain = new Main(ConfigurationSettings.AppSettings["ConnectionString"].ToString());
    RoleWiseAccess theHelper = new RoleWiseAccess(ConfigurationSettings.AppSettings["ConnectionString"].ToString());
    protected void Page_Load(object sender, EventArgs e)
    {
        Page.Title = "Role Wise Access";
        if (Session["userName"] == null)
        {
            Response.Redirect("../LoginPage.aspx");
        }

        if (Session["userName"].ToString() != "SUPERVISOR")
        {
            if (Objmain.checkaccss(Session["CoCode"].ToString(), Session["UserRoleID"].ToString().Trim(), "ROLE WISE ACCESS", checkAccessType.ViewAction) == false)
            {
                Response.Redirect("../AccessDenied.aspx");
            }

            if (Objmain.checkaccss(Session["CoCode"].ToString(), Session["UserRoleID"].ToString().Trim(), "ROLE WISE ACCESS", checkAccessType.InsertAction) == false)
            {
                Button1.Enabled = false;
            }
        }
        if (!IsPostBack)
        {
            DropDownFill();
            GridFill();
         
            ViewState["mytable"] = null;
        }
    }

    protected void DynamicTable()
    {
        string duplicateflag = "0";
        DataTable dtprevious=null;

        DataTable dt = new DataTable();
        string view, insert, update, delete;
        dt.Columns.Add("Module", typeof(string));
        dt.Columns.Add("SubModule", typeof(string));
        dt.Columns.Add("Menu", typeof(string));
        dt.Columns.Add("view", typeof(string));
        dt.Columns.Add("insert", typeof(string));
        dt.Columns.Add("update", typeof(string));
        dt.Columns.Add("delete", typeof(string));


        if (ViewState["mytable"] != null)
        {
            dtprevious = (DataTable)ViewState["mytable"];
            dt = dtprevious;
        }

      
        DataRow dtrow = dt.NewRow();    // Create New Row

        //Insert value to datatable   ..
        for (int i = 0; i < GridView1.Rows.Count; i++) 
        {
            Label ModuleID = (Label)GridView1.Rows[i].Cells[2].FindControl("ModuleID");
            Label SubModuleID = (Label)GridView1.Rows[i].Cells[2].FindControl("SubModuleID");
            Label MenuID = (Label)GridView1.Rows[i].Cells[2].FindControl("MenuID");
            CheckBox CheckBox1 = (CheckBox)GridView1.Rows[i].Cells[2].FindControl("CheckBox1");
            CheckBox CheckBox2 = (CheckBox)GridView1.Rows[i].Cells[2].FindControl("CheckBox2");
            CheckBox CheckBox3 = (CheckBox)GridView1.Rows[i].Cells[2].FindControl("CheckBox3");
            CheckBox CheckBox4 = (CheckBox)GridView1.Rows[i].Cells[2].FindControl("CheckBox4");

            //Check Duplicate Menu ..
            if (dtprevious != null)
            {
                for (int j = 0; j < dtprevious.Rows.Count; j++)
                {
                    if (dtprevious.Rows[j]["Menu"].ToString() == MenuID.Text)
                    {
                        duplicateflag = "1";
                    }
                    else
                    {
                        duplicateflag = "0";
                    }
                }
            }

            if (duplicateflag == "0")
            {
                if (CheckBox1.Checked == true || CheckBox2.Checked == true || CheckBox3.Checked == true || CheckBox4.Checked == true)
                {
                    if (CheckBox1.Checked == true)
                        view = "1";
                    else
                        view = "0";

                    if (CheckBox2.Checked == true)
                        insert = "1";
                    else
                        insert = "0";

                    if (CheckBox3.Checked == true)
                        update = "1";
                    else
                        update = "0";

                    if (CheckBox4.Checked == true)
                        delete = "1";
                    else
                        delete = "0";
 
                    //Bind Data to Columns
                    dtrow["Module"] = ModuleID.Text;
                    dtrow["SubModule"] = SubModuleID.Text;
                    dtrow["Menu"] = MenuID.Text;
                    dtrow["view"] = view;            //Bind Data to Columns
                    dtrow["insert"] = insert;
                    dtrow["update"] = update;
                    dtrow["delete"] = delete;
                    dt.Rows.Add(dtrow);

                    dtrow = dt.NewRow();
                } // jfhfdhjfhd 
            }
        }

        ViewState["mytable"] = dt;
    }

    private void GridFill()
    {
      //  DataTable dtprevious = (DataTable)ViewState["mytable"];

        

        DynamicTable();
        GridView1.DataSource = theHelper.GetAllRole(DropDownList2.SelectedValue, DropDownList3.SelectedValue);
        GridView1.DataBind();

        DataTable dtprevious = theHelper.getacces(DropDownList2.SelectedValue, DropDownList3.SelectedValue, DropDownList1.SelectedValue);

        if (dtprevious != null)
        {
            for (int j = 0; j < dtprevious.Rows.Count; j++)
            {
                for (int i = 0; i < GridView1.Rows.Count; i++)
                {
                    Label MenuID = (Label)GridView1.Rows[i].Cells[2].FindControl("MenuID");
                    CheckBox CheckBox1 = (CheckBox)GridView1.Rows[i].Cells[2].FindControl("CheckBox1");
                    CheckBox CheckBox2 = (CheckBox)GridView1.Rows[i].Cells[2].FindControl("CheckBox2");
                    CheckBox CheckBox3 = (CheckBox)GridView1.Rows[i].Cells[2].FindControl("CheckBox3");
                    CheckBox CheckBox4 = (CheckBox)GridView1.Rows[i].Cells[2].FindControl("CheckBox4");

                    if (dtprevious.Rows[j]["Menuid"].ToString() == MenuID.Text)
                    {
                        if (dtprevious.Rows[j]["views"].ToString() == "1")
                            CheckBox1.Checked = true;
                        else
                            CheckBox1.Checked = false;

                        if (dtprevious.Rows[j]["inserts"].ToString() == "1")
                            CheckBox2.Checked = true;
                        else
                            CheckBox2.Checked = false;

                        if (dtprevious.Rows[j]["updates"].ToString() == "1")
                            CheckBox3.Checked = true;
                        else
                            CheckBox3.Checked = false;

                        if (dtprevious.Rows[j]["deletes"].ToString() == "1")
                            CheckBox4.Checked = true;
                        else
                            CheckBox4.Checked = false;
                    }
                }
            }
        }
    }
    private void ResetAllFields()
    {
        DropDownList1.SelectedIndex = -1;
        Button1.Text = "Submit";
        lblError.Text = "";
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
            GridView1.Rows[index].BackColor = Color.GreenYellow;

            Label lblRoleID = (Label)GridView1.Rows[index].FindControl("RoleID");
            HiddenField1.Value = lblRoleID.Text;

                   DropDownFill();
            Label lblUserRoleID = (Label)GridView1.Rows[index].FindControl("UserRoleID");
            //DropDownList1.SelectedIndex = DropDownList1.Items.IndexOf(DropDownList1.Items.FindByText(lblUserRoleID.Text));
            DropDownList1.SelectedValue = lblUserRoleID.Text;

             DropDownFill();
            Label lblModuleID = (Label)GridView1.Rows[index].FindControl("ModuleID");
           
       
            Button1.Text = "Update";
        }
    }
    private void DropDownFill()
    {
        DropDownList1.Items.Clear();
        this.DropDownList1.DataSource = theHelper.DropdownID1();
        this.DropDownList1.DataTextField = "UserRoleName";
        this.DropDownList1.DataValueField = "UserRoleID";
        this.DropDownList1.DataBind();
        this.DropDownList1.Items.Insert(0, new ListItem("--Select--", "0"));



        DropDownList2.Items.Clear();
        this.DropDownList2.DataSource = theHelper.DropdownModule();
        this.DropDownList2.DataTextField = "ModuleName";
        this.DropDownList2.DataValueField = "ModuleID";
        this.DropDownList2.DataBind();
        this.DropDownList2.Items.Insert(0, new ListItem("--ALL--", "0"));

        this.DropDownList3.Items.Insert(0, new ListItem("--ALL--", "0"));

       
    }
    protected void GridView1_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
          Label lblMenuID = (Label)GridView1.Rows[e.RowIndex].FindControl("MenuID");
        theHelper.DeleteRoleWiseAccess(Convert.ToInt32(lblMenuID.Text));
        lblError.ForeColor = System.Drawing.Color.Green;
        lblError.Text = "Deleted Successfully";
        GridFill();
        ResetAllFields(); 
    }


    protected void Button1_Click(object sender, EventArgs e)
    {
        string view, insert, update, delete;
          if (Button1.Text == "Submit")
        {
            foreach (GridViewRow row in GridView1.Rows)
            {
                Label ModuleID = (Label)row.FindControl("ModuleID");
                Label SubModuleID = (Label)row.FindControl("SubModuleID");
                Label MenuID = (Label)row.FindControl("MenuID");


                CheckBox CheckBox1 = (CheckBox)row.FindControl("CheckBox1");
                CheckBox CheckBox2 = (CheckBox)row.FindControl("CheckBox2");
                CheckBox CheckBox3 = (CheckBox)row.FindControl("CheckBox3");
                CheckBox CheckBox4 = (CheckBox)row.FindControl("CheckBox4");
                if (CheckBox1.Checked == true)
                {
                    view = "1";
                }
                else
                {
                    view = "0";
                }

                if (CheckBox2.Checked == true)
                {
                    insert = "1";
                }
                else
                {
                    insert = "0";
                }

                if (CheckBox3.Checked == true)
                {
                    update = "1";
                }
                else
                {
                    update = "0";
                }

                if (CheckBox4.Checked == true)
                {
                    delete = "1";
                }
                else
                {
                    delete = "0";
                }
               // if (view != "0" || insert != "0" || update != "0" || delete != "0")
                {
                    theHelper.InsertRoleWiseAccess(DropDownList1.SelectedValue, ModuleID.Text, SubModuleID.Text, MenuID.Text, view, insert, update, delete);
                    //  ScriptManager.RegisterStartupScript(this, this.GetType(), "msg", "alert('No Insert !');", true);

                }
            }
             ScriptManager.RegisterStartupScript(this, this.GetType(), "msg", "alert('Inserted successfully !');", true);
        }
        
      //  ResetAllFields();
        GridFill(); 
    }
    
    
    protected void Button2_Click(object sender, EventArgs e)
    {
        ResetAllFields();
    
       
    }


    protected void DropDownList2_SelectedIndexChanged(object sender, EventArgs e)
    {
        DropDownList3.Items.Clear();
        this.DropDownList3.DataSource = theHelper.DropdownSUBModule(DropDownList2.SelectedValue);
        this.DropDownList3.DataTextField = "SubModuleName";
        this.DropDownList3.DataValueField = "SubModuleID";
        this.DropDownList3.DataBind();
        this.DropDownList3.Items.Insert(0, new ListItem("--ALL--", "0"));
    }
    protected void Button3_Click(object sender, EventArgs e)
    {
        GridFill();
    }



    protected void CheckBox5_CheckedChanged(object sender, EventArgs e)
    {
        CheckBox ChkBoxHeader = (CheckBox)GridView1.HeaderRow.FindControl("CheckBox5");
        foreach (GridViewRow row in GridView1.Rows)
        {
            CheckBox ChkBoxRows = (CheckBox)row.FindControl("CheckBox1");
            if (ChkBoxHeader.Checked == true)
            {
                ChkBoxRows.Checked = true;
            }
            else
            {
                ChkBoxRows.Checked = false;
            }
        }
    }



    protected void CheckBox6_CheckedChanged(object sender, EventArgs e)
    {
        CheckBox ChkBoxHeader = (CheckBox)GridView1.HeaderRow.FindControl("CheckBox6");
        foreach (GridViewRow row in GridView1.Rows)
        {
            CheckBox ChkBoxRows = (CheckBox)row.FindControl("CheckBox2");
            if (ChkBoxHeader.Checked == true)
            {
                ChkBoxRows.Checked = true;
            }
            else
            {
                ChkBoxRows.Checked = false;
            }
        }
    }



    protected void CheckBox7_CheckedChanged(object sender, EventArgs e)
    {
        CheckBox ChkBoxHeader = (CheckBox)GridView1.HeaderRow.FindControl("CheckBox7");
        foreach (GridViewRow row in GridView1.Rows)
        {
            CheckBox ChkBoxRows = (CheckBox)row.FindControl("CheckBox3");
            if (ChkBoxHeader.Checked == true)
            {
                ChkBoxRows.Checked = true;
            }
            else
            {
                ChkBoxRows.Checked = false;
            }
        }
    }



    protected void CheckBox8_CheckedChanged(object sender, EventArgs e)
    {
        CheckBox ChkBoxHeader = (CheckBox)GridView1.HeaderRow.FindControl("CheckBox8");
        foreach (GridViewRow row in GridView1.Rows)
        {
            CheckBox ChkBoxRows = (CheckBox)row.FindControl("CheckBox4");
            if (ChkBoxHeader.Checked == true)
            {
                ChkBoxRows.Checked = true;
            }
            else
            {
                ChkBoxRows.Checked = false;
            }
        }
    }
}