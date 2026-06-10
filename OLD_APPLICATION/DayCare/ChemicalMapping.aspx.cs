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
 

public partial class DayCare_ChemicalMapping : System.Web.UI.Page
{
    Main Objmain = new Main(ConfigurationSettings.AppSettings["ConnectionString"].ToString());
    DC_ChemicalMapping thechem = new DC_ChemicalMapping(ConfigurationSettings.AppSettings["ConnectionString"].ToString());
    protected void Page_Load(object sender, EventArgs e)
    {
        Page.Title = "Chemical Mapping";
        if (Session["userName"] == null)
        {
            Response.Redirect("../LoginPage.aspx");
        }
        if (Objmain.checkaccss(Session["CoCode"].ToString(), Session["UserRoleID"].ToString().Trim(), "CHEMICAL MAPPING", checkAccessType.ViewAction) == false)
        {
            Response.Redirect("../AccessDenied.aspx");
        }

        if (Objmain.checkaccss(Session["CoCode"].ToString(), Session["UserRoleID"].ToString().Trim(), "CHEMICAL MAPPING", checkAccessType.InsertAction) == false)
        {
            Button3.Enabled = false;
        }
        if (!IsPostBack)
        {
            DropDownFill();
            Button3.Visible = false;
            Button4.Visible = false;
            GridFill();
        }
    }
    protected void ddldialysertype_SelectedIndexChanged(object sender, EventArgs e)
    {
        this.ddldialysername.DataSource = thechem.DropdownDialyserName(Convert.ToInt32(ddldialysertype.SelectedValue));
        this.ddldialysername.DataTextField = "DialysisName";
        this.ddldialysername.DataValueField = "ID";
        this.ddldialysername.DataBind();
        this.ddldialysername.Items.Insert(0, new ListItem("--Select--", "0"));
    }
    protected void ddldialysername_SelectedIndexChanged(object sender, EventArgs e)
    {
        GridFill();
        Button3.Visible = true;
        Button4.Visible = true; 
    }
    private void DropDownFill()
    {
         this.ddldialysertype.DataSource = thechem.DropdownDialysertype();
        this.ddldialysertype.DataTextField = "TypeName";
        this.ddldialysertype.DataValueField = "TypeId";
        this.ddldialysertype.DataBind();
        this.ddldialysertype.Items.Insert(0, new ListItem("--Select--", "0"));
    }

    private void GridFill()
    {
        GridView1.DataSource = thechem.GridChemicalDetails(ddldialysertype.SelectedValue.ToString(), ddldialysername.SelectedValue.ToString());
        GridView1.DataBind();
    }

    public void Reset()
    {
        ddldialysername.SelectedIndex = 0;
        ddldialysertype.SelectedIndex = 0;
        Button3.Visible = false; Button4.Visible = false;
    }
   
    protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
    {

    }

    //public object[] dtrow { get; set; }
    protected void Button3_Click(object sender, EventArgs e)
    {
        thechem.DeleteMapping(ddldialysertype.SelectedValue, ddldialysername.SelectedValue, Session["CoCode"].ToString());

         for (int i = 0; i < GridView1.Rows.Count; i++)
         {
             CheckBox chk = (CheckBox)GridView1.Rows[i].Cells[1].FindControl("CheckBox1");

              if (chk.Checked)
              {
                  Label lblname = (Label)GridView1.Rows[i].Cells[2].FindControl("lblname");
                  Label lblid = (Label)GridView1.Rows[i].Cells[2].FindControl("lblid");
                  if (thechem.InsertMapping(ddldialysertype.SelectedValue.ToString(), ddldialysername.SelectedValue.ToString(), lblid.Text, Session["CoCode"].ToString()) == true)
                  //if (thedisp.InsertMapping(ddldialysertype.SelectedValue.ToString(), ddldialysername.SelectedValue.ToString(), lblid.Text, Session["CoCode"].ToString()) == true)
                      ScriptManager.RegisterStartupScript(this, this.GetType(), "msg", "alert('Insered Successfully !');", true);
                  else
                      ScriptManager.RegisterStartupScript(this, this.GetType(), "msg", "alert('Error Insered Data !');", true);
              }

         }

         Reset();

    

    }
    protected void GridView1_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
         GridView1.PageIndex = e.NewPageIndex;
        GridFill();
    }

    protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            CheckBox CheckBox1 = (CheckBox)e.Row.FindControl("CheckBox1");
            Label lblid = (Label)e.Row.FindControl("lblid");
            DataTable dt = thechem.GridChemicalDatabound(ddldialysertype.SelectedValue, ddldialysername.SelectedValue);
            if (dt.Rows.Count > 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    if (dt.Rows[i][0].ToString() == lblid.Text)
                    {
                        CheckBox1.Checked = true;
                    }
                }
            }
        }
    }
}