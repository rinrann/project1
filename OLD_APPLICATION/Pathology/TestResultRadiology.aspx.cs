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
 
public partial class Pathology_TestResultRadiology : System.Web.UI.Page
{
    Main Objmain = new Main(ConfigurationSettings.AppSettings["ConnectionString"].ToString());
    PH_TestResultRadiology theptres = new PH_TestResultRadiology(ConfigurationSettings.AppSettings["ConnectionString"].ToString());
    string ucode, uname;
   
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["userName"] == null)
        {
            Response.Redirect("../LoginPage.aspx");
        }
        if (Objmain.checkaccss(Session["CoCode"].ToString(), Session["UserRoleID"].ToString().Trim(), "USG TEST RESULT", checkAccessType.ViewAction) == false)
        {
            Response.Redirect("../AccessDenied.aspx");
        }

        if (Objmain.checkaccss(Session["CoCode"].ToString(), Session["UserRoleID"].ToString().Trim(), "USG TEST RESULT", checkAccessType.InsertAction) == false)
        {
            Button5.Enabled = false;
        }
        Page.Title = "USG Test Result";
        if (Session["userName"] == null)
        {
            Response.Redirect("../LoginPage.aspx");
        }
        if (!IsPostBack)
        {
            DropDownFill();
            String Date = DateTime.Now.ToString("dd/MM/yyyy");
            txtdate.Text = Date.ToString(); 
            Panel1.Visible = false;
            Panel2.Visible = false; Panel3.Visible = false;
           
        }
    }

    protected void Button4_Click(object sender, EventArgs e)
    {          
        GridFill();      
    }
    public void GridFill()
    {
         DataTable dt1 = theptres.GetPatientDetails(Session["CoCode"].ToString().Trim(), Session["YearCode"].ToString().Trim(),txtregno.Text);
        if (dt1.Rows.Count > 0)
        {
            txtname.Text = dt1.Rows[0]["PatientName"].ToString();
            txtvillage.Text = dt1.Rows[0]["Address"].ToString();
            for (int i = 0; i < dt1.Rows.Count; i++)
            {
                if (i == 0)
                {
                    ucode = dt1.Rows[i]["TestCode"].ToString();
                    uname = dt1.Rows[i]["TestName"].ToString();
                }
                else
                {
                    ucode = ucode + "," + dt1.Rows[i]["TestCode"].ToString();
                    uname = uname + "," + dt1.Rows[i]["TestName"].ToString();
                }
            }
            GridView g1, g2;
            txtcode.Text = ucode;
            txttestname.Text = uname;
            DataTable dtusgresult = theptres.GetUSGResultCode(Session["CoCode"].ToString().Trim(), Session["YearCode"].ToString().Trim(), txtregno.Text);
            if (dtusgresult.Rows.Count > 0)
            {
                DropDownList1.SelectedValue = dtusgresult.Rows[0]["ConsultantDoc"].ToString();
                DropDownList2.SelectedValue = dtusgresult.Rows[0]["CheckedDoc"].ToString();
                Button5.Text = "Update";
                for (int i = 0, n = 11; i < dtusgresult.Rows.Count; i++, n = n + 2)
                {
                    Panel p = (Panel)Page.FindControl("ctl00$ContentPlaceHolder1$Panel" + (i + 1).ToString());
                    p.Visible = true;
                    HiddenField h = (HiddenField)Page.FindControl("ctl00$ContentPlaceHolder1$HiddenField" + (i + 1).ToString());
                    h.Value = dtusgresult.Rows[i]["RowId"].ToString();
                    TextBox t = (TextBox)Page.FindControl("ctl00$ContentPlaceHolder1$TextBox" + (i + 2).ToString());
                    t.Text = dtusgresult.Rows[i]["Remarks"].ToString();
                    DataTable dt = theptres.GetUSGheaderResultCode(Session["CoCode"].ToString().Trim(), Session["YearCode"].ToString().Trim(), txtregno.Text, dtusgresult.Rows[i]["USGNameId"].ToString());
                    g1 = (GridView)Page.FindControl("ctl00$ContentPlaceHolder1$GridView" + n.ToString());
                    g2 = (GridView)Page.FindControl("ctl00$ContentPlaceHolder1$GridView" + (n + 1).ToString());
                    g1.DataSource = dt;
                    g1.DataBind();
                    g2.DataSource = theptres.GetRadioResultParameter(Session["CoCode"].ToString().Trim(), Session["YearCode"].ToString().Trim(), txtregno.Text, dtusgresult.Rows[i]["USGNameId"].ToString());
                    g2.DataBind();

                }
            }
            else
            {
                DataTable dtusgcode = theptres.GetUSGCode(Session["CoCode"].ToString().Trim(), Session["YearCode"].ToString().Trim(), txtregno.Text);

                for (int i = 0, n = 11; i < dtusgcode.Rows.Count; i++, n = n + 2)
                {
                    Panel p = (Panel)Page.FindControl("ctl00$ContentPlaceHolder1$Panel" + (i + 1).ToString());
                    p.Visible = true;
                    DataTable dt = theptres.GetRadiology(Session["CoCode"].ToString().Trim(), Session["YearCode"].ToString().Trim(), txtregno.Text, dtusgcode.Rows[i][0].ToString());
                    g1 = (GridView)Page.FindControl("ctl00$ContentPlaceHolder1$GridView" + n.ToString());
                    g2 = (GridView)Page.FindControl("ctl00$ContentPlaceHolder1$GridView" + (n + 1).ToString());
                    g1.DataSource = dt;
                    g1.DataBind();
                    g2.DataSource = theptres.GetRadiologyParameter(Session["CoCode"].ToString().Trim(), Session["YearCode"].ToString().Trim(), txtregno.Text, dtusgcode.Rows[i][0].ToString());
                    g2.DataBind();

                }
            }
           
        }
    }

    public void DropDownFill()
    {
        DropDownList1.DataSource = theptres.DropdownDoc(Session["CoCode"].ToString().Trim());
        DropDownList1.DataTextField = "doc_name";
        DropDownList1.DataValueField = "doc_id";
        DropDownList1.DataBind();
        DropDownList1.Items.Insert(0, new ListItem("--Select--", "0"));



        DropDownList2.DataSource = theptres.DropdownTech(Session["CoCode"].ToString().Trim());
        DropDownList2.DataTextField = "QuackName";
        DropDownList2.DataValueField = "QuackId";
        DropDownList2.DataBind();
        DropDownList2.Items.Insert(0, new ListItem("--Select--", "0"));
    }

    protected void Button5_Click(object sender, EventArgs e)
    {
        string[] words = txtcode.Text.Split(',');
       GridView g1, g2;
       if (Button5.Text == "Submit")
       {
           for (int j = 0, n = 11; j < words.Length; j++, n = n + 2)
           {
               DataTable dt12 = theptres.GenerateRadiologyCode(Session["CoCode"].ToString().Trim(), Session["YearCode"].ToString().Trim());
               g1 = (GridView)Page.FindControl("ctl00$ContentPlaceHolder1$GridView" + n.ToString());
               g2 = (GridView)Page.FindControl("ctl00$ContentPlaceHolder1$GridView" + (n + 1).ToString());
               TextBox t = (TextBox)Page.FindControl("ctl00$ContentPlaceHolder1$TextBox" + (j + 2).ToString());
               theptres.InsertRadiology(Session["CoCode"].ToString().Trim(), Session["YearCode"].ToString().Trim(), dt12.Rows[0][0].ToString(), words[j], txtregno.Text, TextBox1.Text, DropDownList1.SelectedValue, DropDownList2.SelectedValue, t.Text, Session["userName"].ToString());
               for (int i = 0; i < g1.Rows.Count; i++)
               {
                   Label lblID = (Label)g1.Rows[i].FindControl("lblID");
                   TextBox txtheaderContent = (TextBox)g1.Rows[i].FindControl("txtheaderContent");
                   theptres.InsertHeader(Session["CoCode"].ToString().Trim(), Session["YearCode"].ToString().Trim(), dt12.Rows[0][0].ToString(), lblID.Text, txtheaderContent.Text, Session["userName"].ToString());

               }

               for (int k = 0; k < g2.Rows.Count; k++)
               {

                   Label lblID = (Label)g2.Rows[k].FindControl("lblID");
                   TextBox txtval = (TextBox)g2.Rows[k].FindControl("txtval");
                   theptres.InsertParameter(Session["CoCode"].ToString().Trim(), Session["YearCode"].ToString().Trim(), dt12.Rows[0][0].ToString(), lblID.Text, txtval.Text, Session["userName"].ToString());
               }
           }


           ScriptManager.RegisterStartupScript(this, this.GetType(), "msg", "alert('Inserted Successfully !');", true);
       }
       else
       {
           for (int j = 0, n = 11; j < words.Length; j++, n = n + 2)
           {
             
               g1 = (GridView)Page.FindControl("ctl00$ContentPlaceHolder1$GridView" + n.ToString());
               g2 = (GridView)Page.FindControl("ctl00$ContentPlaceHolder1$GridView" + (n + 1).ToString());
               HiddenField h = (HiddenField)Page.FindControl("ctl00$ContentPlaceHolder1$HiddenField" + (j + 1).ToString());
                 TextBox t = (TextBox)Page.FindControl("ctl00$ContentPlaceHolder1$TextBox" + (j + 2).ToString());
                 theptres.UpdateRadiology(Session["CoCode"].ToString().Trim(), Session["YearCode"].ToString().Trim(), h.Value, DropDownList1.SelectedValue, DropDownList2.SelectedValue, t.Text, Session["userName"].ToString());
                 
               for (int i = 0; i < g1.Rows.Count; i++)
               {
                   Label lblID = (Label)g1.Rows[i].FindControl("lblID");
                   TextBox txtheaderContent = (TextBox)g1.Rows[i].FindControl("txtheaderContent");
                   theptres.UpdateHeader(Session["CoCode"].ToString().Trim(), Session["YearCode"].ToString().Trim(), h.Value, lblID.Text, txtheaderContent.Text, Session["userName"].ToString());

               }

               for (int k = 0; k < g2.Rows.Count; k++)
               {

                   Label lblID = (Label)g2.Rows[k].FindControl("lblID");
                   TextBox txtval = (TextBox)g2.Rows[k].FindControl("txtval");
                   theptres.UpdateParameter(Session["CoCode"].ToString().Trim(), Session["YearCode"].ToString().Trim(), h.Value, lblID.Text, txtval.Text, Session["userName"].ToString());
               }
           }


           ScriptManager.RegisterStartupScript(this, this.GetType(), "msg", "alert('Updated Successfully !');", true);
       }
        ResetAllFields();
      
    }
    public void ResetAllFields()
    {


        Panel1.Visible = false; Panel2.Visible = false; Panel3.Visible = false;
        DropDownList1.SelectedIndex = 0; DropDownList2.SelectedIndex = 0;
        txtcode.Text = ""; txtdate.Text = ""; txtname.Text = ""; txtregno.Text = ""; TextBox1.Text = ""; txttestname.Text = ""; txtvillage.Text = "";
        HiddenField1.Value="";

      
    }

}