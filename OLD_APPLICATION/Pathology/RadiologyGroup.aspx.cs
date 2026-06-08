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
 
public partial class Pathology_RadiologyGroup : System.Web.UI.Page
{
    PH_RadiologyGroup theRadiology = new PH_RadiologyGroup(ConfigurationSettings.AppSettings["ConnectionString"].ToString());
    protected void Page_Load(object sender, EventArgs e)
    {
        Page.Title = "Radiology Group Master";
        if (Session["userId"] == null)
        {
            Response.Redirect("../LoginPage.aspx");
        }
        if (!IsPostBack)
        {
            //Button1.Visible = false;
            //Button2.Visible = false;
            GenerateProfileCode();
         //   DropDownFill();
            GridFill();
        }

    }
    private void GridFill()
    {
         GridView1.DataSource = theRadiology.GridRadioDetails(txtcode.Text);
        GridView1.DataBind();

        GridView2.DataSource = theRadiology.GridRadioRightDetails(txtcode.Text);
        GridView2.DataBind();
    }
    public void GenerateProfileCode()
    {
         DataTable dt = theRadiology.GenerateRadiologyCode();
        txtcode.Text = dt.Rows[0][0].ToString();
    }
     
    protected void Button3_Click(object sender, EventArgs e)
    {
         HiddenField1.Value = txtcode.Text;
        if (Button3.Text == "Submit")
        {
            theRadiology.InsertRadio(txtcode.Text, txtname.Text, txtprice.Text);
            for (int i = 0; i < GridView2.Rows.Count; i++)
            {
                Label lblid = (Label)GridView2.Rows[i].Cells[2].FindControl("lblid");

                theRadiology.InsertMapping(HiddenField1.Value, lblid.Text);

            }
            ScriptManager.RegisterStartupScript(this, this.GetType(), "msg", "alert('Inserted Successfully !');", true);

        }
        else
        {
            theRadiology.DeleteRadioMapping(HiddenField1.Value);
            theRadiology.UpdateProfile(txtcode.Text, txtname.Text, txtprice.Text);
            for (int i = 0; i < GridView2.Rows.Count; i++)
            {
                Label lblid = (Label)GridView2.Rows[i].Cells[2].FindControl("lblid");

                theRadiology.InsertMapping(HiddenField1.Value, lblid.Text);

            }
            ScriptManager.RegisterStartupScript(this, this.GetType(), "msg", "alert('Updated Successfully !');", true);

        }

        ResetAllFields();

        GridFill();
    }
    protected void Button5_Click(object sender, EventArgs e)
    {
         DataTable dt = theRadiology.GridFill(txtcode.Text);
        txtname.Text = dt.Rows[0]["ProfileName"].ToString();
        txtprice.Text = dt.Rows[0]["Price"].ToString();
        //DropDownList1.SelectedValue = dt.Rows[0]["DepartmentID"].ToString();
        GridFill();
        Button3.Text = "Update";
    }
    protected void Button4_Click(object sender, EventArgs e)
    {
        ResetAllFields();
    
    }
    public void ResetAllFields()
    {
        txtcode.Text = "";
        txtname.Text = ""; txtprice.Text = "";
        GridFill();
    }
    protected void Button1_Click(object sender, EventArgs e)
    {
        Button3.Visible = true;
        Button4.Visible = true;
        DataTable dt = new DataTable();
        DataRow row = dt.NewRow();
        DataTable dtLeft = new DataTable();
        DataRow rowLeft = dtLeft.NewRow();

        dt.Columns.Add("ID", typeof(string));
        dt.Columns.Add("Name", typeof(string));
    //    dt.Columns.Add("cost", typeof(decimal));


        dtLeft.Columns.Add("ID", typeof(string));
        dtLeft.Columns.Add("Name", typeof(string));
      //  dtLeft.Columns.Add("Cost", typeof(decimal));

        if (GridView2.Rows.Count > 0)
        {
            for (int i = 0; i < GridView2.Rows.Count; i++)
            {
                Label lblname = (Label)GridView2.Rows[i].FindControl("lblname");
                Label lblid = (Label)GridView2.Rows[i].FindControl("lblid");
              //  Label lblcost = (Label)GridView2.Rows[i].FindControl("lblcost");

                row["ID"] = lblid.Text;
                row["Name"] = lblname.Text;
             //   row["Cost"] = Convert.ToDouble(lblcost.Text);
                dt.Rows.Add(row);
                row = dt.NewRow();
            }
        }

        for (int i = 0; i < GridView1.Rows.Count; i++)
        {
            CheckBox chk = (CheckBox)GridView1.Rows[i].Cells[1].FindControl("CheckBox1");
            Label lblname = (Label)GridView1.Rows[i].Cells[2].FindControl("lblname");
            Label lblid = (Label)GridView1.Rows[i].Cells[2].FindControl("lblid");
           // Label lblcost = (Label)GridView1.Rows[i].Cells[2].FindControl("lblcost");

            if (chk.Checked)
            {
                row["ID"] = lblid.Text;
                row["Name"] = lblname.Text;
               // row["Cost"] = Convert.ToDouble(lblcost.Text);

                dt.Rows.Add(row);
                row = dt.NewRow();
            }
            else
            {
                rowLeft["ID"] = lblid.Text;
                rowLeft["Name"] = lblname.Text;
             //   rowLeft["Cost"] = Convert.ToDouble(lblcost.Text);
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

        dt.Columns.Add("ID", typeof(string));
        dt.Columns.Add("Name", typeof(string));
       // dt.Columns.Add("Cost", typeof(decimal));


        dtLeft.Columns.Add("ID", typeof(string));
        dtLeft.Columns.Add("Name", typeof(string));
       // dtLeft.Columns.Add("Cost", typeof(decimal));
        if (GridView1.Rows.Count > 0)
        {
            for (int i = 0; i < GridView1.Rows.Count; i++)
            {
                Label lblname = (Label)GridView1.Rows[i].Cells[2].FindControl("lblname");
                Label lblid = (Label)GridView1.Rows[i].Cells[2].FindControl("lblid");
              //  Label lblcost = (Label)GridView1.Rows[i].Cells[2].FindControl("lblcost");

                rowLeft["ID"] = lblid.Text;
                rowLeft["Name"] = lblname.Text;
              //  rowLeft["Cost"] = Convert.ToDouble(lblcost.Text);
                dtLeft.Rows.Add(rowLeft);
                rowLeft = dtLeft.NewRow();
            }
        }

        for (int i = 0; i < GridView2.Rows.Count; i++)
        {

            CheckBox chk = (CheckBox)GridView2.Rows[i].Cells[1].FindControl("CheckBox2");
            Label lblname = (Label)GridView2.Rows[i].Cells[2].FindControl("lblname");
            Label lblid = (Label)GridView2.Rows[i].Cells[2].FindControl("lblid");
          //  Label lblcost = (Label)GridView2.Rows[i].Cells[2].FindControl("lblcost");

            if (!chk.Checked)
            {
                row["ID"] = lblid.Text;
                row["Name"] = lblname.Text;
               // row["Cost"] = Convert.ToDouble(lblcost.Text);
                dt.Rows.Add(row);
                row = dt.NewRow();
            }
            else
            {
                rowLeft["ID"] = lblid.Text;
                rowLeft["Name"] = lblname.Text;
               // rowLeft["Cost"] = Convert.ToDouble(lblcost.Text);
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
}