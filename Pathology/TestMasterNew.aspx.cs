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

public partial class Pathology_TestMasterNew : System.Web.UI.Page
{
    Main Objmain = new Main(ConfigurationSettings.AppSettings["ConnectionString"].ToString());
    PH_TestMaster thetest = new PH_TestMaster(ConfigurationSettings.AppSettings["ConnectionString"].ToString());
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["userId"] == null)
        {
            Response.Redirect("../LoginPage.aspx");
        }
        if (Objmain.checkaccss(Session["CoCode"].ToString(), Session["UserRoleID"].ToString().Trim(), "INVESTIGATION MASTER", checkAccessType.ViewAction) == false)
        {
            Response.Redirect("../AccessDenied.aspx");
        }

        if (Objmain.checkaccss(Session["CoCode"].ToString(), Session["UserRoleID"].ToString().Trim(), "INVESTIGATION MASTER", checkAccessType.InsertAction) == false)
        {
           // Button1.Enabled = false;
        }

        Page.Title = "Test Master";
        if (Session["userId"] == null)
        {
            Response.Redirect("../LoginPage.aspx");
        }
        if (!IsPostBack)
        {
            DropDownFill();
            string adminflag = "0";
            DataTable dt = thetest.getUserDetails(Session["CoCode"].ToString().Trim(), Session["userId"].ToString().Trim());
            if (dt.Rows.Count > 0)
            {
                adminflag = dt.Rows[0]["AdminUser"].ToString();
                ViewState["adminflag"] = adminflag;
            }
            if (adminflag == "1")
            {
                
                GridView1.Columns[14].Visible = true;
                GridView1.Columns[15].Visible = true;
                GridView1.Columns[16].Visible = true;
            }
        }
    }

    private void DropDownFill()
    {
        this.ddlgroup.DataSource = thetest.DropdownTestGroup(Session["CoCode"].ToString().Trim());
        this.ddlgroup.DataTextField = "ProfileName";
        this.ddlgroup.DataValueField = "ProfileCode";
        this.ddlgroup.DataBind();
        this.ddlgroup.Items.Insert(0, new ListItem("--Select--", "0"));

    }
    
    protected void ddlgroup_SelectedIndexChanged(object sender, EventArgs e)
    {
        DataTable dt = thetest.getDeptCode(Session["CoCode"].ToString().Trim(), ddlgroup.SelectedValue.ToString().Trim());
        txtDeptCode.Text = dt.Rows[0]["DepartmentID"].ToString();
        gridBind();
    }

    private void gridBind()
    {
        string grpCode = ddlgroup.SelectedValue.ToString().Trim();
        DataTable dt = thetest.getTestDetails(Session["CoCode"].ToString().Trim(), grpCode);
        GridView1.DataSource = dt;
        if (dt.Rows.Count == 0)
        {
            DataRow DR = dt.NewRow();
            dt.Rows.Add(DR);
        }
        GridView1.DataBind();
    }

    protected void GridView1_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        GridView1.PageIndex = e.NewPageIndex;
        gridBind();
    }

    protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "Select")
        {
            //int index = Convert.ToInt32(e.CommandArgument);
            //Label lblcode = (Label)GridView1.Rows[index].FindControl("lblcode");
            //txtcode.Text = lblcode.Text;
            //txtcode.ReadOnly = true;
            //txtcode.Enabled = false;
            //Label lblname = (Label)GridView1.Rows[index].FindControl("lblname");
            //txtname.Text = lblname.Text;
            //Label lbldeptCode = (Label)GridView1.Rows[index].FindControl("lbldeptCode");
            //DropDownList1.SelectedValue = lbldeptCode.Text;
            //Tab1Func();
            //Button3.Text = "Update";
            //if (Objmain.checkaccss(Session["CoCode"].ToString(), Session["UserRoleID"].ToString().Trim(), "TEST GROUP MASTER", checkAccessType.UpdateAction) == false)
            //{
            //    Button3.Enabled = false;
            //}
            //else
            //{
            //    Button3.Enabled = true;
            //}
        }
        if (e.CommandName == "Insert")
        {
            string testcode = "";
            int reccnt=0;
            TextBox txtNameNew = (TextBox)GridView1.FooterRow.FindControl("txtNameNew");
            TextBox txtInrNew = (TextBox)GridView1.FooterRow.FindControl("txtInrNew");
            TextBox txtConsNew = (TextBox)GridView1.FooterRow.FindControl("txtConsNew");
            TextBox txtCompNew = (TextBox)GridView1.FooterRow.FindControl("txtCompNew");
            TextBox txtSingleNew = (TextBox)GridView1.FooterRow.FindControl("txtSingleNew");
            TextBox txtTwinsNew = (TextBox)GridView1.FooterRow.FindControl("txtTwinsNew");
            TextBox txtLabNew = (TextBox)GridView1.FooterRow.FindControl("txtLabNew");
            TextBox txtOtNew = (TextBox)GridView1.FooterRow.FindControl("txtOtNew");
            TextBox txtMedNew = (TextBox)GridView1.FooterRow.FindControl("txtMedNew");
            TextBox txtBioNew = (TextBox)GridView1.FooterRow.FindControl("txtBioNew");
            TextBox txtIvfNew = (TextBox)GridView1.FooterRow.FindControl("txtIvfNew");
            TextBox txtOfrRt1New = (TextBox)GridView1.FooterRow.FindControl("txtOfrRt1New");
            TextBox txtOfrRt2New = (TextBox)GridView1.FooterRow.FindControl("txtOfrRt2New");
            TextBox txtOfrRt3New = (TextBox)GridView1.FooterRow.FindControl("txtOfrRt3New");
            DropDownList ddlConsult1_New = (DropDownList)GridView1.FooterRow.FindControl("ddlConsult1_New");
            DropDownList ddlConsult2_New = (DropDownList)GridView1.FooterRow.FindControl("ddlConsult2_New");
            DropDownList ddlConsult3_New = (DropDownList)GridView1.FooterRow.FindControl("ddlConsult3_New");

            TextBox txtNormalRangeNew = (TextBox)GridView1.FooterRow.FindControl("txtNormalRangeNew");
            TextBox txtUnitNew = (TextBox)GridView1.FooterRow.FindControl("txtUnitNew");
            TextBox txtMethodNew = (TextBox)GridView1.FooterRow.FindControl("txtMethodNew");
            TextBox txtDetailsNew = (TextBox)GridView1.FooterRow.FindControl("txtDetailsNew");


            if (txtInrNew.Text == "")
            {
                txtInrNew.Text = "0";
            }
            if (txtConsNew.Text == "")
            {
                txtConsNew.Text = "0";
            }
            if (txtCompNew.Text == "")
            {
                txtCompNew.Text = "0";
            }
            if (txtSingleNew.Text == "")
            {
                txtSingleNew.Text = "0";
            }
            if (txtTwinsNew.Text == "")
            {
                txtTwinsNew.Text = "0";
            }
            if (txtLabNew.Text == "")
            {
                txtLabNew.Text = "0";
            }
            if (txtOtNew.Text == "")
            {
                txtOtNew.Text = "0";
            }
            if (txtMedNew.Text == "")
            {
                txtMedNew.Text = "0";
            }
            if (txtBioNew.Text == "")
            {
                txtBioNew.Text = "0";
            }
            if (txtIvfNew.Text == "")
            {
                txtIvfNew.Text = "0";
            }
            if (txtOfrRt1New.Text == "")
            {
                txtOfrRt1New.Text = "0";
            }
            if (txtOfrRt2New.Text == "")
            {
                txtOfrRt2New.Text = "0";
            }
            if (txtOfrRt3New.Text == "")
            {
                txtOfrRt3New.Text = "0";
            }


            DataTable dt = thetest.getTestDetails(Session["CoCode"].ToString().Trim(), ddlgroup.SelectedValue.Trim());
            reccnt = dt.Rows.Count;
            string newno="0000"+(reccnt+1).ToString();
            testcode = ddlgroup.SelectedValue.Trim() + newno.Substring(newno.Length - 3);

            if (txtNameNew.Text == "")
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "msg", "alert('Investigation Name cannot be blank!');", true);
            }
            else
            {
                if (thetest.InsertInvestigation(Session["CoCode"].ToString().Trim(), txtDeptCode.Text.Trim(), ddlgroup.SelectedValue.Trim(), testcode, txtNameNew.Text.Trim(), txtInrNew.Text.Trim(), txtConsNew.Text.Trim(), txtCompNew.Text.Trim(), txtSingleNew.Text.Trim(), txtTwinsNew.Text.Trim(), txtLabNew.Text.Trim(), txtOtNew.Text.Trim(), txtMedNew.Text.Trim(), txtBioNew.Text.Trim(), txtIvfNew.Text.Trim(), Session["userId"].ToString().Trim(), txtOfrRt1New.Text.Trim(), txtOfrRt2New.Text.Trim(), txtOfrRt3New.Text.Trim(), ddlConsult1_New.SelectedValue.Trim(), ddlConsult2_New.SelectedValue.Trim(), ddlConsult3_New.SelectedValue.Trim(), txtNormalRangeNew.Text, txtUnitNew.Text, txtMethodNew.Text,txtDetailsNew.Text) == true)
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "msg", "alert('Investigation Successfully saved!');", true);
                    gridBind();
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "msg", "alert('Error occured during saved!');", true);
                }
            }
        }
    }

    protected void GridView1_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
    {
        GridView1.EditIndex = -1;
        gridBind();
    }
    
    protected void GridView1_RowEditing(object sender, GridViewEditEventArgs e)
    {
        GridView1.EditIndex = e.NewEditIndex;
        gridBind();
    }
    protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            DataRowView drv = (DataRowView)e.Row.DataItem;

            if (drv["TestId"].ToString() == String.Empty)
            {
                e.Row.Visible = false;//ViewState["adminflag"]
            }
        }
        if (e.Row.RowType == DataControlRowType.Footer)
        {
            //TextBox txtconsullt_name1New = (TextBox)e.Row.FindControl("txtconsullt_name1New");
            //TextBox txtconsullt_name2New = (TextBox)e.Row.FindControl("txtconsullt_name2New");
            //TextBox txtconsullt_name3New = (TextBox)e.Row.FindControl("txtconsullt_name3New");
            DropDownList ddlConsult1_New = (DropDownList)e.Row.FindControl("ddlConsult1_New");
            DropDownList ddlConsult2_New = (DropDownList)e.Row.FindControl("ddlConsult2_New");
            DropDownList ddlConsult3_New = (DropDownList)e.Row.FindControl("ddlConsult3_New");
            DataTable dt = thetest.DropdownDoctor(Session["CoCode"].ToString().Trim());

            ddlConsult1_New.DataSource = dt;
            ddlConsult1_New.DataTextField = "doc_name";
            ddlConsult1_New.DataValueField = "doc_id";
            ddlConsult1_New.DataBind();
            ddlConsult1_New.Items.Insert(0, new ListItem("--Select--", ""));

            ddlConsult2_New.DataSource = dt;
            ddlConsult2_New.DataTextField = "doc_name";
            ddlConsult2_New.DataValueField = "doc_id";
            ddlConsult2_New.DataBind();
            ddlConsult2_New.Items.Insert(0, new ListItem("--Select--", ""));

            ddlConsult3_New.DataSource = dt;
            ddlConsult3_New.DataTextField = "doc_name";
            ddlConsult3_New.DataValueField = "doc_id";
            ddlConsult3_New.DataBind();
            ddlConsult3_New.Items.Insert(0, new ListItem("--Select--", ""));

            if (ViewState["adminflag"].ToString() == "1")
            {
                ddlConsult1_New.Enabled = true;
                ddlConsult2_New.Enabled = true;
                ddlConsult3_New.Enabled = true;
            }
            else
            {
                ddlConsult1_New.Enabled = false;
                ddlConsult2_New.Enabled = false;
                ddlConsult3_New.Enabled = false;
            }
        }
        if (((e.Row.RowState == DataControlRowState.Edit) || e.Row.RowState == (DataControlRowState.Alternate | DataControlRowState.Edit)))
        {
            //TextBox txtconsullt_name1Old = (TextBox)e.Row.FindControl("txtconsullt_name1Old");
            //TextBox txtconsullt_name2Old = (TextBox)e.Row.FindControl("txtconsullt_name2Old");
            //TextBox txtconsullt_name3Old = (TextBox)e.Row.FindControl("txtconsullt_name3Old");

            Label lblconsullt1_Old = (Label)e.Row.FindControl("lblconsullt1_Old");
            Label lblconsullt2_Old = (Label)e.Row.FindControl("lblconsullt2_Old");
            Label lblconsullt3_Old = (Label)e.Row.FindControl("lblconsullt3_Old");

            DropDownList ddlConsult1_Old = (DropDownList)e.Row.FindControl("ddlConsult1_Old");
            DropDownList ddlConsult2_Old = (DropDownList)e.Row.FindControl("ddlConsult2_Old");
            DropDownList ddlConsult3_Old = (DropDownList)e.Row.FindControl("ddlConsult3_Old");
            DataTable dt = thetest.DropdownDoctor(Session["CoCode"].ToString().Trim());

            ddlConsult1_Old.DataSource = dt;
            ddlConsult1_Old.DataTextField = "doc_name";
            ddlConsult1_Old.DataValueField = "doc_id";
            ddlConsult1_Old.DataBind();
            ddlConsult1_Old.Items.Insert(0, new ListItem("--Select--", ""));
            ddlConsult1_Old.SelectedValue = lblconsullt1_Old.Text.Trim();

            ddlConsult2_Old.DataSource = dt;
            ddlConsult2_Old.DataTextField = "doc_name";
            ddlConsult2_Old.DataValueField = "doc_id";
            ddlConsult2_Old.DataBind();
            ddlConsult2_Old.Items.Insert(0, new ListItem("--Select--", ""));
            ddlConsult2_Old.SelectedValue = lblconsullt2_Old.Text.Trim();

            ddlConsult3_Old.DataSource = dt;
            ddlConsult3_Old.DataTextField = "doc_name";
            ddlConsult3_Old.DataValueField = "doc_id";
            ddlConsult3_Old.DataBind();
            ddlConsult3_Old.Items.Insert(0, new ListItem("--Select--", ""));
            ddlConsult3_Old.SelectedValue = lblconsullt3_Old.Text.Trim();

            if (ViewState["adminflag"].ToString() == "1")
            {
                ddlConsult1_Old.Enabled = true;
                ddlConsult2_Old.Enabled = true;
                ddlConsult3_Old.Enabled = true;
            }
            else
            {
                ddlConsult1_Old.Enabled = false;
                ddlConsult2_Old.Enabled = false;
                ddlConsult3_Old.Enabled = false;
            }
        }
    }
    protected void GridView1_RowUpdating(object sender, GridViewUpdateEventArgs e)
    {
        Label lblCodeOld = (Label)GridView1.Rows[e.RowIndex].FindControl("lblCodeOld");
        TextBox txtNameOld = (TextBox)GridView1.Rows[e.RowIndex].FindControl("txtNameOld");
        TextBox txtInrOld = (TextBox)GridView1.Rows[e.RowIndex].FindControl("txtInrOld");
        TextBox txtConsOld = (TextBox)GridView1.Rows[e.RowIndex].FindControl("txtConsOld");
        TextBox txtCompOld = (TextBox)GridView1.Rows[e.RowIndex].FindControl("txtCompOld");
        TextBox txtSingleOld = (TextBox)GridView1.Rows[e.RowIndex].FindControl("txtSingleOld");
        TextBox txtTwinsOld = (TextBox)GridView1.Rows[e.RowIndex].FindControl("txtTwinsOld");
        TextBox txtLabOld = (TextBox)GridView1.Rows[e.RowIndex].FindControl("txtLabOld");
        TextBox txtOtOld = (TextBox)GridView1.Rows[e.RowIndex].FindControl("txtOtOld");
        TextBox txtMedOld = (TextBox)GridView1.Rows[e.RowIndex].FindControl("txtMedOld");
        TextBox txtBioOld = (TextBox)GridView1.Rows[e.RowIndex].FindControl("txtBioOld");
        TextBox txtIvfOld = (TextBox)GridView1.Rows[e.RowIndex].FindControl("txtIvfOld");
        TextBox txtOfrRt1Old = (TextBox)GridView1.Rows[e.RowIndex].FindControl("txtOfrRt1Old");
        TextBox txtOfrRt2Old = (TextBox)GridView1.Rows[e.RowIndex].FindControl("txtOfrRt2Old");
        TextBox txtOfrRt3Old = (TextBox)GridView1.Rows[e.RowIndex].FindControl("txtOfrRt3Old");
        DropDownList ddlConsult1_Old = (DropDownList)GridView1.Rows[e.RowIndex].FindControl("ddlConsult1_Old");
        DropDownList ddlConsult2_Old = (DropDownList)GridView1.Rows[e.RowIndex].FindControl("ddlConsult2_Old");
        DropDownList ddlConsult3_Old = (DropDownList)GridView1.Rows[e.RowIndex].FindControl("ddlConsult3_Old");

        TextBox txtNormalRangeOld = (TextBox)GridView1.Rows[e.RowIndex].FindControl("txtNormalRangeOld");
        TextBox txtUnitOld = (TextBox)GridView1.Rows[e.RowIndex].FindControl("txtUnitOld");
        TextBox txtMethodOld = (TextBox)GridView1.Rows[e.RowIndex].FindControl("txtMethodOld");
        TextBox txtDetailsOld = (TextBox)GridView1.Rows[e.RowIndex].FindControl("txtDetailsOld"); 

        if (txtInrOld.Text == "")
        {
            txtInrOld.Text = "0";
        }
        if (txtConsOld.Text == "")
        {
            txtConsOld.Text = "0";
        }
        if (txtCompOld.Text == "")
        {
            txtCompOld.Text = "0";
        }
        if (txtSingleOld.Text == "")
        {
            txtSingleOld.Text = "0";
        }
        if (txtTwinsOld.Text == "")
        {
            txtTwinsOld.Text = "0";
        }
        if (txtLabOld.Text == "")
        {
            txtLabOld.Text = "0";
        }
        if (txtOtOld.Text == "")
        {
            txtOtOld.Text = "0";
        }
        if (txtMedOld.Text == "")
        {
            txtMedOld.Text = "0";
        }
        if (txtBioOld.Text == "")
        {
            txtBioOld.Text = "0";
        }
        if (txtIvfOld.Text == "")
        {
            txtIvfOld.Text = "0";
        }

        if (txtOfrRt1Old.Text == "")
        {
            txtOfrRt1Old.Text = "0";
        }
        if (txtOfrRt2Old.Text == "")
        {
            txtOfrRt2Old.Text = "0";
        }
        if (txtOfrRt3Old.Text == "")
        {
            txtOfrRt3Old.Text = "0";
        }

        string testcode = lblCodeOld.Text.Trim();

        if (txtNameOld.Text == "")
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "msg", "alert('Investigation Name cannot be blank!');", true);
        }
        else
        {
            if (thetest.UpdateInvestigation(Session["CoCode"].ToString().Trim(), txtDeptCode.Text.Trim(), ddlgroup.SelectedValue.Trim(), testcode, txtNameOld.Text.Trim(), txtInrOld.Text.Trim(), txtConsOld.Text.Trim(), txtCompOld.Text.Trim(), txtSingleOld.Text.Trim(), txtTwinsOld.Text.Trim(), txtLabOld.Text.Trim(), txtOtOld.Text.Trim(), txtMedOld.Text.Trim(), txtBioOld.Text.Trim(), txtIvfOld.Text.Trim(), Session["userId"].ToString().Trim(), txtOfrRt1Old.Text.Trim(), txtOfrRt2Old.Text.Trim(), txtOfrRt2Old.Text.Trim(), ddlConsult1_Old.SelectedValue.Trim(), ddlConsult2_Old.SelectedValue.Trim(), ddlConsult3_Old.SelectedValue.Trim(), txtNormalRangeOld.Text, txtUnitOld.Text, txtMethodOld.Text,txtDetailsOld.Text) == true)
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "msg", "alert('Record Successfully Updated!');", true);
                GridView1.EditIndex = -1;
                gridBind();
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "msg", "alert('Error occured during updating!');", true);
            }
        }
    }
    protected void btnlist_Click(object sender, EventArgs e)
    {

        Response.Redirect("view_investigationlist.aspx");
    }
}