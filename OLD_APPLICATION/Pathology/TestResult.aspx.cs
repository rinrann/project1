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
 
public partial class Pathology_TestResult : System.Web.UI.Page
{
    Main Objmain = new Main(ConfigurationSettings.AppSettings["ConnectionString"].ToString());
    PH_TestResult theptres = new PH_TestResult(ConfigurationSettings.AppSettings["ConnectionString"].ToString());
    string code, name;
    double cost;

    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["userName"] == null)
        {
            Response.Redirect("../LoginPage.aspx");
        }
        if (Objmain.checkaccss(Session["CoCode"].ToString(), Session["UserRoleID"].ToString().Trim(), "PATHOLOGY TEST RESULT", checkAccessType.ViewAction) == false)
        {
            Response.Redirect("../AccessDenied.aspx");
        }

        if (Objmain.checkaccss(Session["CoCode"].ToString(), Session["UserRoleID"].ToString().Trim(), "PATHOLOGY TEST RESULT", checkAccessType.InsertAction) == false)
        {
            Button1.Enabled = false;
        }
        Page.Title = "Pathology Test Result";
        if (Session["userName"] == null)
        {
            Response.Redirect("../LoginPage.aspx");
        }
        if (!IsPostBack)
        {
            DropDownFill();
            String Date = DateTime.Now.ToString("dd/MM/yyyy");
            txtdate.Text = Date.ToString();
            for (int i = 1; i <= 11; i++)
            {
               Panel p = (Panel)Page.FindControl("ctl00$ContentPlaceHolder1$Panel" + i.ToString());
               p.Visible = false;
           
                  
            }
                     
        }
    }


    public void DropDownFill()
    {
        ddlSpeciman.DataSource = theptres.DropdownSpecimen(Session["CoCode"].ToString().Trim());
        ddlSpeciman.DataTextField = "SName";
        ddlSpeciman.DataValueField = "SCode";
        ddlSpeciman.DataBind();
        ddlSpeciman.Items.Insert(0, new ListItem("--Select--", "0"));

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
    protected void Button1_Click(object sender, EventArgs e)
    {
         DataTable dt123 = theptres.GetTestmasert(Session["CoCode"].ToString().Trim(),txtregno.Text);
           
        GridView gd;
        DataTable dt = theptres.Populate(Session["CoCode"].ToString().Trim(), Session["YearCode"].ToString().Trim(), txtregno.Text);
                for (int c1 = 1; c1 <= Convert.ToInt32(txtPId.Value); c1++)
                {

                    gd = (GridView)Page.FindControl("ctl00$ContentPlaceHolder1$GridView" + c1.ToString());
                    int i = 0;
                    int com = -1;
                    int dup = -1;
                    Label lblID = (Label)gd.Rows[i].FindControl("lblID");
                    DataTable dmul = theptres.noofmul(Session["CoCode"].ToString().Trim(), lblID.Text);
                    DataTable dcom = theptres.noofcomplex(Session["CoCode"].ToString().Trim(), lblID.Text);
                    DataTable ddup = theptres.noofduplex(Session["CoCode"].ToString().Trim(), lblID.Text);
                    TextBox txtValue = (TextBox)gd.Rows[i].FindControl("txtValue");
                    Label lblRange = (Label)gd.Rows[i].FindControl("lblRange");
                    DataTable dt1 = theptres.GenerateTestResult(Session["CoCode"].ToString().Trim(), Session["YearCode"].ToString().Trim());

                    Label lblResultID = (Label)gd.Rows[i].FindControl("lblResultID");

                    TextBox t = (TextBox)Page.FindControl("ctl00$ContentPlaceHolder1$TextBox" + c1.ToString());
                    theptres.InsertTestResult(Session["CoCode"].ToString().Trim(), Session["YearCode"].ToString().Trim(), lblResultID.Text, t.Text, TextBox50.Text, dt1.Rows[0][0].ToString(), lblID.Text, txtregno.Text, txtValue.Text, ddlSpeciman.SelectedValue, DropDownList1.SelectedValue, DropDownList2.SelectedValue, Session["userName"].ToString().Trim());
                    i++;
                    if (txtValue.Text == "" || lblRange.Text == "")
                    {
                        for (int count = 0; count < dmul.Rows.Count; count++)
                        {
                            DataTable dt5 = theptres.GenerateTestResultMul(Session["CoCode"].ToString().Trim(), Session["YearCode"].ToString().Trim());
                            TextBox txtValue1 = (TextBox)gd.Rows[i].FindControl("txtValue");
                            Label lblRange1 = (Label)gd.Rows[i].FindControl("lblRange");
                            Label lblResultIDMultiple = (Label)gd.Rows[i].FindControl("lblResultID");
                            theptres.InsertResultMultipleTest(Session["CoCode"].ToString().Trim(), Session["YearCode"].ToString().Trim(), lblResultIDMultiple.Text, dt5.Rows[0][0].ToString(), dt1.Rows[0][0].ToString(), txtValue1.Text, dmul.Rows[count]["MultipleId"].ToString(), Session["userName"].ToString().Trim());
                            i++;
                            if ((txtValue1.Text == "" || lblRange1.Text == "") && dcom.Rows.Count > 0 && dcom.Rows.Count > com+1)
                            {
                                com++;
                                for (int m = 0; m < Convert.ToInt32(dcom.Rows[com]["totalMul"]); m++)
                                {
                                    //DataTable dt3 = theptres.GenerateTestResultDup();
                                    DataTable dt2 = theptres.GenerateTestResultCom(Session["CoCode"].ToString().Trim(), Session["YearCode"].ToString().Trim());
                                    TextBox txtValue2 = (TextBox)gd.Rows[i].FindControl("txtValue");
                                    Label lblID1 = (Label)gd.Rows[i].FindControl("lblID");
                                    Label lblRange2 = (Label)gd.Rows[i].FindControl("lblRange");
                                    Label lblResultIDComplex = (Label)gd.Rows[i].FindControl("lblResultID");
                                    theptres.InsertResultcomplexTest(Session["CoCode"].ToString().Trim(), Session["YearCode"].ToString().Trim(), lblResultIDComplex.Text, dt2.Rows[0][0].ToString(), dt1.Rows[0][0].ToString(), dt5.Rows[0][0].ToString(), txtValue2.Text, lblID1.Text, Session["userName"].ToString().Trim());
                                    i++;
                                    if ((txtValue2.Text == "" || lblRange2.Text == "") && ddup.Rows.Count > 0 && ddup.Rows.Count > dup+1)
                                    {
                                        dup++;
                                        for (int n = 0; n < Convert.ToInt32(ddup.Rows[dup]["totalMul"]); n++)
                                        {
                                            DataTable dt3 = theptres.GenerateTestResultDup(Session["CoCode"].ToString().Trim(), Session["YearCode"].ToString().Trim());
                                            TextBox txtValue3 = (TextBox)gd.Rows[i].FindControl("txtValue");
                                            Label lblID2 = (Label)gd.Rows[i].FindControl("lblID");
                                            Label lblResultIDDuplex = (Label)gd.Rows[i].FindControl("lblResultID");
                                            theptres.InsertResultduplexTest(Session["CoCode"].ToString().Trim(), Session["YearCode"].ToString().Trim(), lblResultIDDuplex.Text, dt3.Rows[0][0].ToString(), dt1.Rows[0][0].ToString(), dt5.Rows[0][0].ToString(), dt2.Rows[0][0].ToString(), txtValue3.Text, lblID2.Text, Session["userName"].ToString().Trim());
                                            i++;
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
                ScriptManager.RegisterStartupScript(this, this.GetType(), "msg", "alert('Inserted Successfully !');", true);
            ResetAllFields();
        }

    public void ResetAllFields()
    {
        for (int c1 = 1; c1 <= 11; c1++)
        {
          GridView  gd = (GridView)Page.FindControl("ctl00$ContentPlaceHolder1$GridView" + c1.ToString());
          Panel p = (Panel)Page.FindControl("ctl00$ContentPlaceHolder1$Panel" + c1.ToString());
          p.Visible = false;
          gd.Visible = false;
        }
        ddlSpeciman.SelectedIndex = 0; DropDownList1.SelectedIndex = 0; DropDownList2.SelectedIndex = 0;
        txtcode.Text = "";txtdate.Text = "";txtname.Text = ""; txtPId.Value = ""; txtregno.Text = ""; TextBox50.Text = ""; txttestname.Text = ""; txtvillage.Text = "";
         }

    public void BindGrid()
    {
        DataSet testDataset = theptres.BindGrid(Session["CoCode"].ToString().Trim(), Session["YearCode"].ToString().Trim(), TextBox50.Text);
        txtPId.Value = testDataset.Tables.Count.ToString();
        int i = 0;
        foreach (DataTable dt in testDataset.Tables)
        {
            GridView grd = (GridView)Page.FindControl("ctl00$ContentPlaceHolder1$GridView" + (i + 1).ToString());
            grd.DataSource = testDataset.Tables[i];
            grd.DataBind();
            grd.Visible = true;
            i = i + 1;
        }
        for (int j = 1; j <= i; j++)
        {
            Panel p = (Panel)Page.FindControl("ctl00$ContentPlaceHolder1$Panel" + j.ToString());
            p.Visible = true;
        }
      }
    protected void Button4_Click1(object sender, EventArgs e)
    {
        for (int c1 = 1; c1 <= 11; c1++)
        {
            GridView gd = (GridView)Page.FindControl("ctl00$ContentPlaceHolder1$GridView" + c1.ToString());
            Panel p = (Panel)Page.FindControl("ctl00$ContentPlaceHolder1$Panel" + c1.ToString());
            p.Visible = false;
            gd.Visible = false;
        } 
        BindGrid();

         DataTable dt = theptres.Populate(Session["CoCode"].ToString().Trim(), Session["YearCode"].ToString().Trim(), txtregno.Text);
        DataTable dt1 = theptres.GetTestmasert(Session["CoCode"].ToString().Trim(),txtregno.Text);
    
      
    
            for (int i = 0; i <dt1.Rows.Count; i++)
            {
                if (i == 0)
                {
                    code = dt1.Rows[i]["TestId"].ToString();
                    name = dt1.Rows[i]["TestName"].ToString();
                }
                else
                {
                    code = code + "," + dt1.Rows[i]["TestId"].ToString();
                    name = name + "," + dt1.Rows[i]["TestName"].ToString();
                }
            }
            txtcode.Text = code.ToString();
            txttestname.Text = name.ToString();
            DataTable dttest = theptres.GetTestResult(Session["CoCode"].ToString().Trim(), Session["YearCode"].ToString().Trim(), txtregno.Text);
            if (dttest.Rows.Count > 0)
            {
                ddlSpeciman.SelectedValue = dttest.Rows[0]["SCode"].ToString();
                DropDownList1.SelectedValue = dttest.Rows[0]["doc_id"].ToString();
                DropDownList2.SelectedValue = dttest.Rows[0]["CheckedBy"].ToString();
                TextBox t;
                for (int i = 1;i<=dttest.Rows.Count; i++)
                {
                    t = (TextBox)Page.FindControl("ctl00$ContentPlaceHolder1$TextBox" + i.ToString());
                    t.Text = dttest.Rows[i - 1]["Remarks"].ToString();
                }
                Button1.Text = "Update";
            }
       
        txtname.Text = dt.Rows[0]["patient_name"].ToString();
        txtvillage.Text = dt.Rows[0]["vill_city"].ToString();
  
    }

    protected void Gridview3_RowDataBound(object sender, GridViewRowEventArgs e)
    {
       
            GridViewRow myRow = e.Row;
            if (myRow.RowType == DataControlRowType.DataRow)
            {
                if (myRow.RowState == DataControlRowState.Normal || myRow.RowState == DataControlRowState.Alternate)
                {
                    Label lblRange = ((Label)e.Row.FindControl("lblRange"));
                    TextBox txtValue = (TextBox)e.Row.FindControl("txtValue");
                    if (lblRange.Text == "")
                    {

                        txtValue.Enabled = true;
                    }
                    else
                    {
                        txtValue.Enabled = true;
                    }
                }
         
        }
    }
    protected void Gridview1_RowDataBound(object sender, GridViewRowEventArgs e)
    {
             GridViewRow myRow = e.Row;
             if (myRow.RowType == DataControlRowType.DataRow)
             {
                 if (myRow.RowState == DataControlRowState.Normal || myRow.RowState == DataControlRowState.Alternate)
                 {
                     Label lblRange = ((Label)e.Row.FindControl("lblRange"));
                     TextBox txtValue = (TextBox)e.Row.FindControl("txtValue");
                     if (lblRange.Text == "")
                     {

                         txtValue.Enabled = true;
                     }
                     else
                     {
                         txtValue.Enabled = true;
                     }
                 }
             }
        
    }
    protected void Gridview2_RowDataBound(object sender, GridViewRowEventArgs e)
    {
             GridViewRow myRow = e.Row;
             if (myRow.RowType == DataControlRowType.DataRow)
             {
                 if (myRow.RowState == DataControlRowState.Normal || myRow.RowState == DataControlRowState.Alternate)
                 {
                     Label lblRange = ((Label)e.Row.FindControl("lblRange"));
                     TextBox txtValue = (TextBox)e.Row.FindControl("txtValue");
                     if (lblRange.Text == "")
                     {

                         txtValue.Enabled = true;
                     }
                     else
                     {
                         txtValue.Enabled = true;
                     }
                 }
             
         }
    }
    protected void Gridview4_RowDataBound(object sender, GridViewRowEventArgs e)
    {
             GridViewRow myRow = e.Row;
             if (myRow.RowType == DataControlRowType.DataRow)
             {
                 if (myRow.RowState == DataControlRowState.Normal || myRow.RowState == DataControlRowState.Alternate)
                 {
                     Label lblRange = ((Label)e.Row.FindControl("lblRange"));
                     TextBox txtValue = (TextBox)e.Row.FindControl("txtValue");
                     if (lblRange.Text == "")
                     {

                         txtValue.Enabled = true;
                     }
                     else
                     {
                         txtValue.Enabled = true;
                     }
              
             }
         }
    }
    protected void Gridview5_RowDataBound(object sender, GridViewRowEventArgs e)
    {
             GridViewRow myRow = e.Row;
             if (myRow.RowType == DataControlRowType.DataRow)
             {
                 if (myRow.RowState == DataControlRowState.Normal || myRow.RowState == DataControlRowState.Alternate)
                 {
                     Label lblRange = ((Label)e.Row.FindControl("lblRange"));
                     TextBox txtValue = (TextBox)e.Row.FindControl("txtValue");
                     if (lblRange.Text == "")
                     {

                         txtValue.Enabled = true;
                     }
                     else
                     {
                         txtValue.Enabled = true;
                     }
               
             }
         }
    }
    protected void Gridview6_RowDataBound(object sender, GridViewRowEventArgs e)
    {
             GridViewRow myRow = e.Row;
             if (myRow.RowType == DataControlRowType.DataRow)
             {
                 if (myRow.RowState == DataControlRowState.Normal || myRow.RowState == DataControlRowState.Alternate)
                 {
                     Label lblRange = ((Label)e.Row.FindControl("lblRange"));
                     TextBox txtValue = (TextBox)e.Row.FindControl("txtValue");
                     if (lblRange.Text == "")
                     {

                         txtValue.Enabled = true;
                     }
                     else
                     {
                         txtValue.Enabled = true;
                     }
             
             }
         }
    }
    protected void Gridview7_RowDataBound(object sender, GridViewRowEventArgs e)
    {
             GridViewRow myRow = e.Row;
             if (myRow.RowType == DataControlRowType.DataRow)
             {
                 if (myRow.RowState == DataControlRowState.Normal || myRow.RowState == DataControlRowState.Alternate)
                 {
                     Label lblRange = ((Label)e.Row.FindControl("lblRange"));
                     TextBox txtValue = (TextBox)e.Row.FindControl("txtValue");
                     if (lblRange.Text == "")
                     {

                         txtValue.Enabled = true;
                     }
                     else
                     {
                         txtValue.Enabled = true;
                     }

             }
         }
    }
    protected void Gridview8_RowDataBound(object sender, GridViewRowEventArgs e)
    {
            GridViewRow myRow = e.Row;
             if (myRow.RowType == DataControlRowType.DataRow)
             {
                 if (myRow.RowState == DataControlRowState.Normal || myRow.RowState == DataControlRowState.Alternate)
                 {
                     Label lblRange = ((Label)e.Row.FindControl("lblRange"));
                     TextBox txtValue = (TextBox)e.Row.FindControl("txtValue");
                     if (lblRange.Text == "")
                     {

                         txtValue.Enabled = true;
                     }
                     else
                     {
                         txtValue.Enabled = true;
                     }
             
             }
         }
    }
    protected void Gridview9_RowDataBound(object sender, GridViewRowEventArgs e)
    {
             GridViewRow myRow = e.Row;
             if (myRow.RowType == DataControlRowType.DataRow)
             {
                 if (myRow.RowState == DataControlRowState.Normal || myRow.RowState == DataControlRowState.Alternate)
                 {
                     Label lblRange = ((Label)e.Row.FindControl("lblRange"));
                     TextBox txtValue = (TextBox)e.Row.FindControl("txtValue");
                     if (lblRange.Text == "")
                     {

                         txtValue.Enabled = true;
                     }
                     else
                     {
                         txtValue.Enabled = true;
                     }
         
             }
         }
    }
    protected void Gridview10_RowDataBound(object sender, GridViewRowEventArgs e)
    { 
        GridViewRow myRow = e.Row;
        if (myRow.RowType == DataControlRowType.DataRow)
        {
            if (myRow.RowState == DataControlRowState.Normal || myRow.RowState == DataControlRowState.Alternate)
            {
                Label lblRange = ((Label)e.Row.FindControl("lblRange"));
                TextBox txtValue = (TextBox)e.Row.FindControl("txtValue");
                if (lblRange.Text == "")
                {

                    txtValue.Enabled = true;
                }
                else
                {
                    txtValue.Enabled = true;
                }
          
        }
    }
    }

    protected void Gridview11_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        GridViewRow myRow = e.Row;
        if (myRow.RowType == DataControlRowType.DataRow)
        {
            if (myRow.RowState == DataControlRowState.Normal || myRow.RowState == DataControlRowState.Alternate)
            {
                Label lblRange = ((Label)e.Row.FindControl("lblRange"));
                TextBox txtValue = (TextBox)e.Row.FindControl("txtValue");
                if (lblRange.Text == "")
                {

                    txtValue.Enabled = true;
                }
                else
                {
                    txtValue.Enabled = true;
                }

            }
        }
    }
}
