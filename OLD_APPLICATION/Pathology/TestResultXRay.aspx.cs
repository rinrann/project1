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

 

public partial class Pathology_TestResultXRay : System.Web.UI.Page
{
    Main Objmain = new Main(ConfigurationSettings.AppSettings["ConnectionString"].ToString());
    PH_TestResultXRay theptres = new PH_TestResultXRay(ConfigurationSettings.AppSettings["ConnectionString"].ToString());
    PH_TestResultRadiology theresUSG = new PH_TestResultRadiology(ConfigurationSettings.AppSettings["ConnectionString"].ToString());
    string code, name;
   
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["userName"] == null)
        {
            Response.Redirect("../LoginPage.aspx");
        }
        if (Objmain.checkaccss(Session["CoCode"].ToString(), Session["UserRoleID"].ToString().Trim(), "X-RAY TEST RESULT", checkAccessType.ViewAction) == false)
        {
            Response.Redirect("../AccessDenied.aspx");
        }

        if (Objmain.checkaccss(Session["CoCode"].ToString(), Session["UserRoleID"].ToString().Trim(), "X-RAY TEST RESULT", checkAccessType.InsertAction) == false)
        {
            Button1.Enabled = false;
        }
        Page.Title = " X-RAY Test Result";

        if (Session["userName"] == null)
        {
            Response.Redirect("../LoginPage.aspx");
        }
        if (!IsPostBack)
        {
            DropDownFill();
            String Date = DateTime.Now.ToString("dd/MM/yyyy");
            txtdate.Text = Date.ToString();
            Panel1.Visible = false; Panel2.Visible = false; Panel3.Visible = false; Panel4.Visible = false; Panel5.Visible = false;
         }
    }

    public void DropDownFill()
    {
        string compcode = Session["CoCode"].ToString().Trim();
        for (int i = 11; i <= 15; i++)
        {
            DropDownList d = (DropDownList)Page.FindControl("ctl00$ContentPlaceHolder1$DropDownList" + i.ToString());
            d.DataSource = theptres.DropdownTemplate(compcode);
            d.DataTextField = "TemplateName";
            d.DataValueField = "TemplateID";
            d.DataBind();
            d.Items.Insert(0, new ListItem("--Select--", "0"));
        }


        DropDownList1.DataSource = theptres.DropdownDoc(compcode);
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
  
    protected void Button1_Click(object sender, EventArgs e)
    {
        TextBox t1, t2;
        DropDownList d1;
        int c1, c2,i;
        string compcode=Session["CoCode"].ToString().Trim();
        string yearcode = Session["YearCode"].ToString().Trim();
        DataTable dtxray = theptres.GetXray(compcode,TextBox1.Text);
        string[] a = txtcode.Text.Split(',');
        if (Button1.Text == "Submit")
        {
            for (i = 0,c1=11,c2=21; i < dtxray.Rows.Count; i++,c1++,c2++)
            {
                DataTable dt12 = theresUSG.GenerateRadiologyCode(Session["CoCode"].ToString().Trim(), Session["YearCode"].ToString().Trim());
                t1 = (TextBox)Page.FindControl("ctl00$ContentPlaceHolder1$TextBox" + c1.ToString());
                t2 = (TextBox)Page.FindControl("ctl00$ContentPlaceHolder1$TextBox" + c2.ToString());
                d1 = (DropDownList)Page.FindControl("ctl00$ContentPlaceHolder1$DropDownList" + c1.ToString());
                if(a.Length>1)
                    theptres.InsertXRay(compcode, yearcode, dt12.Rows[0][0].ToString(), a[i], txtregno.Text, TextBox1.Text, t1.Text, DropDownList1.SelectedValue, DropDownList2.SelectedValue, t2.Text, Session["userName"].ToString());
                else
                    theptres.InsertXRay(compcode, yearcode, dt12.Rows[0][0].ToString(), txtcode.Text, txtregno.Text, TextBox1.Text, t1.Text, DropDownList1.SelectedValue, DropDownList2.SelectedValue, t2.Text, Session["userName"].ToString());
            }

            ScriptManager.RegisterStartupScript(this, this.GetType(), "msg", "alert('Inserted Successfully !');", true);
        }
        else
        {
            for (i = 0, c1 = 11, c2 = 21; i < 5; i++, c1++, c2++)
            {
                t1 = (TextBox)Page.FindControl("ctl00$ContentPlaceHolder1$TextBox" + c1.ToString());
                t2 = (TextBox)Page.FindControl("ctl00$ContentPlaceHolder1$TextBox" + c2.ToString());
                d1 = (DropDownList)Page.FindControl("ctl00$ContentPlaceHolder1$DropDownList" + c1.ToString());
                theptres.Updatexray(compcode, yearcode, txtPId.Value, t1.Text, DropDownList1.SelectedValue, DropDownList2.SelectedValue, t2.Text, Session["userName"].ToString());
            }
            ScriptManager.RegisterStartupScript(this, this.GetType(), "msg", "alert('Updated Successfully !');", true);
          
       
        }
        ResetAllFields();
    }
    public void ResetAllFields()
    {
        DropDownList1.SelectedIndex = 0; DropDownList2.SelectedIndex = 0;
        Panel1.Visible = false; Panel2.Visible = false; Panel3.Visible = false; Panel4.Visible = false; Panel5.Visible = false;
        txtcode.Text = ""; txtdate.Text = ""; txtname.Text = ""; txtPId.Value = ""; txtregno.Text = ""; TextBox1.Text = ""; txttestname.Text = ""; txtvillage.Text = "";
        DropDownList11.SelectedIndex = 0; TextBox11.Text = "";
    }

    protected void Button4_Click1(object sender, EventArgs e)
    {
        string compcode = Session["CoCode"].ToString().Trim();
        string yearcode = Session["YearCode"].ToString().Trim();
        DataTable dtxray = theptres.GetXray(compcode, TextBox1.Text);
        DataTable dt = theptres.Populate(compcode, yearcode, txtregno.Text);
        DataTable result = theptres.GetXRayResult(compcode, yearcode, TextBox1.Text);
        if (dtxray.Rows.Count > 0)
        {
            for (int i =0; i < dtxray.Rows.Count; i++)
            {
                TextBox p = (TextBox)Page.FindControl("ctl00$ContentPlaceHolder1$TextBox" + (i+2).ToString());
                p.Text = dtxray.Rows[i]["TestName"].ToString();
            }
        }
        if (result.Rows.Count > 0)
        {
            DropDownList1.SelectedValue = result.Rows[0]["ConsultantDoc"].ToString();
            DropDownList2.SelectedValue = result.Rows[0]["CheckedDoc"].ToString();
            for (int i = 0, c1 = 11, c2 = 21; i < dtxray.Rows.Count; i++, c1++, c2++)
            {
                if (i == 0)
                {
                    code = dtxray.Rows[i]["TestCode"].ToString();
                    name = dtxray.Rows[i]["TestName"].ToString();
                }
                else
                {
                    code = code + "," + dtxray.Rows[i]["TestCode"].ToString();
                    name = name + "," + dtxray.Rows[i]["TestName"].ToString();
                }
                Panel p = (Panel)Page.FindControl("ctl00$ContentPlaceHolder1$Panel" + (i + 1).ToString());
                DropDownList d = (DropDownList)Page.FindControl("ctl00$ContentPlaceHolder1$DropDownList" + c1.ToString());
                TextBox t1 = (TextBox)Page.FindControl("ctl00$ContentPlaceHolder1$TextBox" + c1.ToString());
                TextBox t2 = (TextBox)Page.FindControl("ctl00$ContentPlaceHolder1$TextBox" + c2.ToString());
                p.Visible = true;
                txtcode.Text = code;
                txttestname.Text = name;
                d.SelectedValue = dtxray.Rows[i]["TemplateId"].ToString().Trim();
                //t1.Text = result.Rows[i]["TContent"].ToString();
                t1.Text = result.Rows[i]["HeaderContent"].ToString();
                t2.Text = result.Rows[i]["Remarks"].ToString();
                txtPId.Value = result.Rows[i]["RowID"].ToString();
            }
            Button1.Text = "Update";
        }
        else
        {
            if (dtxray.Rows.Count > 0)
            {

                for (int i = 0, c1 = 11, c2 = 21; i < dtxray.Rows.Count; i++, c1++, c2++)
                {
                    if (i == 0)
                    {
                        code = dtxray.Rows[i]["TestCode"].ToString();
                        name = dtxray.Rows[i]["TestName"].ToString();
                    }
                    else
                    {
                        code = code + "," + dtxray.Rows[i]["TestCode"].ToString();
                        name = name + "," + dtxray.Rows[i]["TestName"].ToString();
                    }
                    Panel p = (Panel)Page.FindControl("ctl00$ContentPlaceHolder1$Panel" + (i + 1).ToString());
                    DropDownList d = (DropDownList)Page.FindControl("ctl00$ContentPlaceHolder1$DropDownList" + c1.ToString());
                    TextBox t1 = (TextBox)Page.FindControl("ctl00$ContentPlaceHolder1$TextBox" + c1.ToString());
                    TextBox t2 = (TextBox)Page.FindControl("ctl00$ContentPlaceHolder1$TextBox" + c2.ToString());
                    p.Visible = true;
                    txtcode.Text = code;
                    txttestname.Text = name;
                    d.SelectedValue = dtxray.Rows[i]["TemplateId"].ToString();
                    t1.Text = dtxray.Rows[i]["Template"].ToString();
                }
            }
        }
            txtname.Text = dt.Rows[0]["patient_name"].ToString();
        txtvillage.Text = dt.Rows[0]["vill_city"].ToString();
        Panel1.Visible = true;
    }
    protected void DropDownList11_SelectedIndexChanged(object sender, EventArgs e)
    {
         DataTable dt = theptres.DropdownTemplate(Session["CoCode"].ToString().Trim(),DropDownList11.SelectedValue);
        TextBox11.Text = dt.Rows[0]["Template"].ToString();
    }
    protected void DropDownList13_SelectedIndexChanged(object sender, EventArgs e)
    {
         DataTable dt = theptres.DropdownTemplate(Session["CoCode"].ToString().Trim(),DropDownList13.SelectedValue);
        TextBox13.Text = dt.Rows[0]["Template"].ToString();
    }
    protected void DropDownList12_SelectedIndexChanged(object sender, EventArgs e)
    {
         DataTable dt = theptres.DropdownTemplate(Session["CoCode"].ToString().Trim(),DropDownList12.SelectedValue);
        TextBox12.Text = dt.Rows[0]["Template"].ToString();
    }
    protected void DropDownList14_SelectedIndexChanged(object sender, EventArgs e)
    {
         DataTable dt = theptres.DropdownTemplate(Session["CoCode"].ToString().Trim(),DropDownList14.SelectedValue);
        TextBox14.Text = dt.Rows[0]["Template"].ToString();
    }
    protected void DropDownList15_SelectedIndexChanged(object sender, EventArgs e)
    {
         DataTable dt = theptres.DropdownTemplate(Session["CoCode"].ToString().Trim(),DropDownList15.SelectedValue);
        TextBox15.Text = dt.Rows[0]["Template"].ToString();
    }
}