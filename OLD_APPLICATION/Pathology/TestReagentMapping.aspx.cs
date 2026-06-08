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


 

public partial class Pathology_TestReagentMapping : System.Web.UI.Page
{
    Main Objmain = new Main(ConfigurationSettings.AppSettings["ConnectionString"].ToString());
    PH_TestReagentMapping theprofile = new PH_TestReagentMapping(ConfigurationSettings.AppSettings["ConnectionString"].ToString());
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["userName"] == null)
        {
            Response.Redirect("../LoginPage.aspx");
        }
        if (Objmain.checkaccss(Session["CoCode"].ToString(), Session["UserRoleID"].ToString().Trim(), "TEST REAGENT MAP", checkAccessType.ViewAction) == false)
        {
            Response.Redirect("../AccessDenied.aspx");
        }

        if (Objmain.checkaccss(Session["CoCode"].ToString(), Session["UserRoleID"].ToString().Trim(), "TEST REAGENT MAP", checkAccessType.InsertAction) == false)
        {
            Button3.Enabled = false;
        }
        Page.Title = "Test Reagent Map";
        if (Session["userName"] == null)
        {
            Response.Redirect("../LoginPage.aspx");
        }
        if (!IsPostBack)
        {
            GenerateMapCode();
            GridFill();
           
        }
    }

    public void GenerateMapCode()
    {
        DataTable dt = theprofile.GenerateProfileCode(Session["CoCode"].ToString().Trim());
        TextBox2.Text = dt.Rows[0][0].ToString();
    }

    public void initial()
    {
        DataTable dt = new DataTable("MyTable");
        dt.Columns.Add("RCode", typeof(string));
        dt.Columns.Add("RName", typeof(string));
        dt.Columns.Add("Unit", typeof(string));

        DataRow dr = dt.NewRow();
        dr["RCode"] = null;
        dr["RName"] = null;
        dr["Unit"] = null;

        dt.Rows.Add(dr);

        GridView2.DataSource = dt;
        GridView2.DataBind();

        GridView2.Rows[0].Visible = false;
    }
    protected void Button3_Click(object sender, EventArgs e)
    {
         HiddenField1.Value = txtcode.Text;
        
        if (Button3.Text == "Submit")
        {
            theprofile.InsertProfile(TextBox2.Text, txtcode.Text, Session["CoCode"].ToString().Trim());
             for (int i = 1; i < GridView2.Rows.Count; i++)
            {

                Label lblid = (Label)GridView2.Rows[i].Cells[2].FindControl("lblid");

                theprofile.InsertMapping(HiddenField1.Value, lblid.Text, Session["CoCode"].ToString().Trim());

            }
             ScriptManager.RegisterStartupScript(this, this.GetType(), "msg", "alert('Inserted Successfully !');", true);

        }
        else
        {
            theprofile.DeleteMapping(HiddenField1.Value, Session["CoCode"].ToString().Trim());
            for (int i = 0; i < GridView2.Rows.Count; i++)
            {
                Label lblid = (Label)GridView2.Rows[i].Cells[2].FindControl("lblid");

                theprofile.InsertMapping(HiddenField1.Value, lblid.Text, Session["CoCode"].ToString().Trim());

            }
            ScriptManager.RegisterStartupScript(this, this.GetType(), "msg", "alert('Updated Successfully !');", true);

        }

        ResetAllFields();

        GridFill();
    }
    public void ResetAllFields()
    {
        GenerateMapCode();
       txtname.Text = "";
      // GridFill();
       txtcode.Text = "";
       Button3.Text = "Submit";

    }
    private void GridFill()
    {

        GridView1.DataSource = theprofile.GridChemicalDetails(txtcode.Text, Session["CoCode"].ToString().Trim());
        GridView1.DataBind();

        GridView2.DataSource = theprofile.GridChemicalRightDetails(txtcode.Text, Session["CoCode"].ToString().Trim());
        GridView2.DataBind();
        initial();
     
    }

    private void GridFill1()
    {
         GridView1.DataSource = theprofile.GridChemicalDetails(txtcode.Text,Session["CoCode"].ToString().Trim());
        GridView1.DataBind();

        GridView2.DataSource = theprofile.GridChemicalRightDetails(txtcode.Text,Session["CoCode"].ToString().Trim());
        GridView2.DataBind();
    }
    protected void Button1_Click(object sender, EventArgs e)
    {
        Button3.Visible = true;
        Button4.Visible = true;
        DataTable dt = new DataTable();
        DataRow row = dt.NewRow();
        DataTable dtLeft = new DataTable();
        DataRow rowLeft = dtLeft.NewRow();

        dt.Columns.Add("RCode", typeof(string));
        dt.Columns.Add("RName", typeof(string));
        dt.Columns.Add("Unit", typeof(string));


        dtLeft.Columns.Add("RCode", typeof(string));
        dtLeft.Columns.Add("RName", typeof(string));
        dtLeft.Columns.Add("Unit", typeof(string));

        if (GridView2.Rows.Count > 0)
        {
            for (int i = 0; i < GridView2.Rows.Count; i++)
            {
                Label lblname = (Label)GridView2.Rows[i].FindControl("lblname");
                Label lblid = (Label)GridView2.Rows[i].FindControl("lblid");
                Label lblcost = (Label)GridView2.Rows[i].FindControl("lblcost");

                row["RCode"] = lblid.Text;
                row["RName"] = lblname.Text;
                row["Unit"] = lblcost.Text;
                dt.Rows.Add(row);
                row = dt.NewRow();
            }
        }

        for (int i = 0; i < GridView1.Rows.Count; i++)
        {
            CheckBox chk = (CheckBox)GridView1.Rows[i].Cells[1].FindControl("CheckBox1");
            Label lblname = (Label)GridView1.Rows[i].Cells[2].FindControl("lblname");
            Label lblid = (Label)GridView1.Rows[i].Cells[2].FindControl("lblid");
            Label lblcost = (Label)GridView1.Rows[i].Cells[2].FindControl("lblcost");

            if (chk.Checked)
            {
                row["RCode"] = lblid.Text;
                row["RName"] = lblname.Text;
                row["Unit"] =lblcost.Text;

                dt.Rows.Add(row);
                row = dt.NewRow();
            }
            else
            {
                rowLeft["RCode"] = lblid.Text;
                rowLeft["RName"] = lblname.Text;
                rowLeft["Unit"] = lblcost.Text;
                dtLeft.Rows.Add(rowLeft);
                rowLeft = dtLeft.NewRow();
            }

        }
        GridView2.DataSource = dt;
        GridView2.DataBind();

        GridView1.DataSource = dtLeft;
        GridView1.DataBind();
      
    }
    protected void Button2_Click(object sender, EventArgs e)
    {
        DataTable dt;
        dt = new DataTable();

        DataRow row = dt.NewRow();
        DataTable dtLeft = new DataTable();

        DataRow rowLeft = dtLeft.NewRow();

        dt.Columns.Add("RCode", typeof(string));
        dt.Columns.Add("RName", typeof(string));
        dt.Columns.Add("Unit", typeof(string));


        dtLeft.Columns.Add("RCode", typeof(string));
        dtLeft.Columns.Add("RName", typeof(string));
        dtLeft.Columns.Add("Unit", typeof(string));
        if (GridView1.Rows.Count > 0)
        {
            for (int i = 0; i < GridView1.Rows.Count; i++)
            {
                Label lblname = (Label)GridView1.Rows[i].Cells[2].FindControl("lblname");
                Label lblid = (Label)GridView1.Rows[i].Cells[2].FindControl("lblid");
                Label lblcost = (Label)GridView1.Rows[i].Cells[2].FindControl("lblcost");

                rowLeft["RCode"] = lblid.Text;
                rowLeft["RName"] = lblname.Text;
                rowLeft["Unit"] = lblcost.Text;
                dtLeft.Rows.Add(rowLeft);
                rowLeft = dtLeft.NewRow();
            }
        }

        for (int i = 0; i < GridView2.Rows.Count; i++)
        {

            CheckBox chk = (CheckBox)GridView2.Rows[i].Cells[1].FindControl("CheckBox2");
            Label lblname = (Label)GridView2.Rows[i].Cells[2].FindControl("lblname");
            Label lblid = (Label)GridView2.Rows[i].Cells[2].FindControl("lblid");
            Label lblcost = (Label)GridView2.Rows[i].Cells[2].FindControl("lblcost");

            if (!chk.Checked)
            {
                row["RCode"] = lblid.Text;
                row["RName"] = lblname.Text;
                row["Unit"] =lblcost.Text;
                dt.Rows.Add(row);
                row = dt.NewRow();
            }
            else
            {
                rowLeft["RCode"] = lblid.Text;
                rowLeft["RName"] = lblname.Text;
                rowLeft["Unit"] = lblcost.Text;
                dtLeft.Rows.Add(rowLeft);
                rowLeft = dtLeft.NewRow();
            }

        }
        GridView2.DataSource = dt;
        GridView2.DataBind();

        GridView1.DataSource = dtLeft;
        GridView1.DataBind();
      
    }
    protected void GridView1_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        GridView1.PageIndex = e.NewPageIndex;
        GridFill();
    }
    protected void GridView2_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        GridView2.PageIndex = e.NewPageIndex;
        GridFill();
    }
    protected void Button9_Click(object sender, EventArgs e)
    {
         DataTable dt = theprofile.fetchdata(TextBox2.Text);
        txtcode.Text = dt.Rows[0]["TestId"].ToString();
        txtname.Text = dt.Rows[0]["TestName"].ToString();
        GridFill1();
        Button3.Text = "Update";
    }

    protected void Button4_Click(object sender, EventArgs e)
    {
        ResetAllFields();
    }
}