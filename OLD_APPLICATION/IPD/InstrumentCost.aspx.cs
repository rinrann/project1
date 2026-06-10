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

public partial class IPD_InstrumentCost : System.Web.UI.Page
{
    Main Objmain = new Main(ConfigurationSettings.AppSettings["ConnectionString"].ToString());
    InstrumentCost theinstrumentcost = new InstrumentCost(ConfigurationSettings.AppSettings["ConnectionString"].ToString());

    protected void Page_Load(object sender, EventArgs e)
    {

        
        Page.Title = "Instrument Cost";
        if (Session["userName"] == null)
        {
            Response.Redirect("../LoginPage.aspx");
        }
        if (Objmain.checkaccss(Session["CoCode"].ToString(), Session["UserRoleID"].ToString().Trim(), "INSTRUMENT COST", checkAccessType.ViewAction) == false)
        {
            Response.Redirect("../AccessDenied.aspx");
        }

        if (Objmain.checkaccss(Session["CoCode"].ToString(), Session["UserRoleID"].ToString().Trim(), "INSTRUMENT COST", checkAccessType.InsertAction) == false)
        {
            Button1.Enabled = false;
        }
        if (!IsPostBack)
        {
            string type = Request.QueryString[0].ToString();
            ViewState["type"]=type;
            Tab1Func();
            DropDownFill(type);
            DataTable dt;
            if (Session["ReqNo"] != null && type=="T")
            {
                TextBox47.Text = Session["ReqNo"].ToString();
                dt = theinstrumentcost.Getonlyreg(Session["CoCode"].ToString().Trim(), Session["YearCode"].ToString().Trim(), TextBox47.Text);
                TextBox46.Text = dt.Rows[0][0].ToString();
                GridFillSecond(type);
                GridFill();

            }
            if (type == "S" && Session["RegNo"] != null && Session["RegNo"].ToString() != "")
            {
                TextBox46.Text = Session["RegNo"].ToString();
                 dt = theinstrumentcost.GridFillSecond(Session["CoCode"].ToString().Trim(), Session["YearCode"].ToString().Trim(), TextBox46.Text,type);
                 if (dt.Rows.Count > 0)
                 {
                     TextBox47.Text = dt.Rows[0]["ServiceId"].ToString();
                 }
                GridFillSecond(type);
                GridFill();
            }
            if(type=="T")
            {
                lbltype.InnerText = "Operation Name :";
                otdiv.Visible = true;
                srvdiv.Visible=false;
            }
            else
            {
                lbltype.InnerText="Service :";
                otdiv.Visible = false;
                srvdiv.Visible = true;
            }
            Session["ReqNo"] = null;
        }
    }

    public void fillDtls(DataTable dt)
    {
        for (int i = 0,d=1,t=1; i < dt.Rows.Count; i++,d++,t=t+3)
        {
            DropDownList d1 = (DropDownList)Page.FindControl("ctl00$ContentPlaceHolder1$DropDownList" + d.ToString());
            TextBox t1 = (TextBox)Page.FindControl("ctl00$ContentPlaceHolder1$TextBox" + t.ToString());
            TextBox t2 = (TextBox)Page.FindControl("ctl00$ContentPlaceHolder1$TextBox" + (t+1).ToString());
            TextBox t3 = (TextBox)Page.FindControl("ctl00$ContentPlaceHolder1$TextBox" + (t+2).ToString());
            CheckBox cont = (CheckBox)Page.FindControl("ctl00$ContentPlaceHolder1$chkins" + d.ToString());
            d1.SelectedValue = dt.Rows[i]["InstrumentId"].ToString();
            t1.Text = dt.Rows[i]["Unit"].ToString();
            t2.Text = dt.Rows[i]["UsageCost"].ToString();
            t3.Text = dt.Rows[i]["Remarks"].ToString();
            if (dt.Rows[i]["cont"].ToString() == "1")
            {
                cont.Checked = true;
            }
            else
            {
                cont.Checked = false;
            }
        }
           
    }
    private void GridFill()
    {
        //select v.Rowid, pr.PatientReg,pr.,CONVERT(varchar,pr.AdmissionDate,103) adate,bm.BedNoText
        string type = ViewState["type"].ToString();
        DataTable dt;
        DataTable dt1;
        if (type == "T")
        {
            dt = theinstrumentcost.GetAllInstrumentCost(Session["CoCode"].ToString().Trim(), Session["YearCode"].ToString().Trim(), TextBox46.Text.ToString(), TextBox47.Text.ToString(), type);
            dt1 = theinstrumentcost.Getonlypat(Session["CoCode"].ToString().Trim(), Session["YearCode"].ToString().Trim(), TextBox46.Text, TextBox47.Text, type);
       }
        else
        {
            dt = theinstrumentcost.GetAllServInstrumentCost(Session["CoCode"].ToString().Trim(), Session["YearCode"].ToString().Trim(), TextBox46.Text.ToString(), TextBox47.Text.ToString(), type);
            dt1 = theinstrumentcost.GetonlyServpat(Session["CoCode"].ToString().Trim(), Session["YearCode"].ToString().Trim(), TextBox46.Text, TextBox47.Text, type);
        }
        


        if (dt.Rows.Count > 0)
        {
            GridView1.DataSource = dt;
            GridView1.DataBind();
            GridView1.Visible = true;
            TextBox48.Text = dt.Rows[0]["patient_name"].ToString();
            TextBox49.Text = dt.Rows[0]["BedNoText"].ToString();
            TextBox51.Text = dt.Rows[0]["adate"].ToString();
            TextBox52.Text = dt.Rows[0]["endate"].ToString();
            TextBox50.Text = dt.Rows[0]["OperationName"].ToString();
            //fillDtls(dt);
            Button1.Text = "Update";
            if (Objmain.checkaccss(Session["CoCode"].ToString(), Session["UserRoleID"].ToString().Trim(), "INSTRUMENT COST", checkAccessType.UpdateAction) == false)
            {
                Button1.Enabled = false;
            }
            else
            {
                Button1.Enabled = true;
            }

        }
        else
        {
            if (dt1.Rows.Count > 0)
            {
                GridView1.Visible = false;
                TextBox48.Text = dt1.Rows[0]["patient_name"].ToString();
                TextBox49.Text = dt1.Rows[0]["BedNoText"].ToString();
                TextBox47.Text = dt1.Rows[0]["OperationReqID"].ToString();
                TextBox51.Text = dt1.Rows[0]["adate"].ToString();
                TextBox50.Text = dt1.Rows[0]["OperationName"].ToString();
            }
        }
    }

    protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            Label lblcont = (Label)e.Row.FindControl("lblcont");
            CheckBox cont=(CheckBox)e.Row.FindControl("continue");
            if (lblcont != null)
            {
                if (lblcont.Text == "1")
                {
                    cont.Checked = true;
                }
                else
                {
                    cont.Checked = false;
                }
            }
        }
        if (e.Row.RowType == DataControlRowType.DataRow && (e.Row.RowState & DataControlRowState.Edit) == DataControlRowState.Edit)
        {
            Label lblcont = (Label)e.Row.FindControl("lblcont");
            CheckBox cont = (CheckBox)e.Row.FindControl("cont");
            if (lblcont != null)
            {
                if (lblcont.Text == "1")
                {
                    cont.Checked = true;
                }
                else
                {
                    cont.Checked = false;
                }
            }
        }
    }

    protected void GridView1_RowEditing(object sender, GridViewEditEventArgs e)
    {
        GridView1.EditIndex = e.NewEditIndex;
        GridFill();
    }

    protected void GridView1_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
    }

    protected void GridView1_RowUpdating(object sender, GridViewUpdateEventArgs e)
    {
        Label lblid = (Label)GridView1.Rows[e.RowIndex].FindControl("lblid");
        TextBox txtgDate = (TextBox)GridView1.Rows[e.RowIndex].FindControl("txtgDate");
        TextBox txtgUnit = (TextBox)GridView1.Rows[e.RowIndex].FindControl("txtgUnit");
        TextBox txtusagecost = (TextBox)GridView1.Rows[e.RowIndex].FindControl("txtusagecost");
        CheckBox Cont = (CheckBox)GridView1.Rows[e.RowIndex].FindControl("cont");

        string cont = "";
        if (Cont.Checked == true)
        {
            cont = "1";
        }
        else
        {
            cont = "0";
        }
        System.Globalization.DateTimeFormatInfo dtf = new DateTimeFormatInfo();
        DateTime testdate;
        if (txtgDate.Text != "")
            testdate = DateTime.ParseExact(txtgDate.Text, "dd/MM/yyyy", dtf);
        else
            testdate = DateTime.Now;


        if (theinstrumentcost.Update_Delete_Addservice(2, TextBox46.Text, lblid.Text, txtgUnit.Text, txtusagecost.Text, testdate.ToString("yyyy-MM-dd"), cont, Session["CoCode"].ToString().Trim(), Session["YearCode"].ToString().Trim(), Session["userName"].ToString().Trim()) == true)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "msg", "alert('Updated Successfully !');", true);
        }
        else
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "msg", "alert('Error in Updated Data !');", true);
        }
        GridView1.EditIndex = -1;
        GridFill();

    }

    protected void GridView1_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
    {
        GridView1.EditIndex = -1;
        GridFill();
    }
    private void DropDownFill(string type)
    {
         for (int i = 1; i <= 15; i++)
        {
            DropDownList d = (DropDownList)Page.FindControl("ctl00$ContentPlaceHolder1$DropDownList" + i.ToString());
            d.DataSource = theinstrumentcost.DropdownInstrument(Session["CoCode"].ToString().Trim(),type);
            d.DataTextField = "InstrumentName";
            d.DataValueField = "RowID";
            d.DataBind();
            d.Items.Insert(0, new ListItem("--Select--", "0"));
        }

    }
    public void Insertdata(string flag)
    {
        TextBox t1, t2, t3;
        CheckBox c1;
        DropDownList d1;
           System.Globalization.DateTimeFormatInfo dtf = new DateTimeFormatInfo();
        DateTime testdate = DateTime.ParseExact(TextBox52.Text, "dd/MM/yyyy", dtf);
        string type = ViewState["type"].ToString();
        string cont;
        
        for (int t = 1,d=1; d<=10; t = t + 3,d++)
        {
            t1 = (TextBox)Page.FindControl("ctl00$ContentPlaceHolder1$TextBox" + t.ToString());
            t2 = (TextBox)Page.FindControl("ctl00$ContentPlaceHolder1$TextBox" + (t + 1).ToString());
            t3 = (TextBox)Page.FindControl("ctl00$ContentPlaceHolder1$TextBox" + (t + 2).ToString());
            d1 = (DropDownList)Page.FindControl("ctl00$ContentPlaceHolder1$DropDownList" + d.ToString());
            c1 = (CheckBox)Page.FindControl("ctl00$ContentPlaceHolder1$chkins" + d.ToString());
            if (c1.Checked == true)
            {
                cont = "1";
            }
            else
            {
                cont = "0";
            }
            if (t1.Text != "" && t2.Text != "")
            {
                if (theinstrumentcost.InsertInstrumentCost(TextBox46.Text, TextBox47.Text, testdate.ToString("yyyy-MM-dd"), d1.SelectedValue, t1.Text, t2.Text, t3.Text, Session["CoCode"].ToString().Trim(), Session["userName"].ToString().Trim(), Session["YearCode"].ToString().Trim(),type,cont) == true)
                {
                    if (flag == "I")
                    {
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "msg", "alert('Inserted Successfully !');", true);
                    }
                    else
                    {
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "msg", "alert('Updated Successfully !');", true);

                    }
                }
                else
                {
                    if (flag == "I")
                    {
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "msg", "alert('Error in Inserted Data !');", true);
                    }
                    else
                    {
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "msg", "alert('Error in Updated Data !');", true);
                    }


                }
            }
            else
                break;

        }
    }
    protected void Button1_Click(object sender, EventArgs e)
    {
        System.Globalization.DateTimeFormatInfo dtf = new DateTimeFormatInfo();
        DateTime testdate = DateTime.ParseExact(TextBox52.Text, "dd/MM/yyyy", dtf);
        string type = ViewState["type"].ToString();
        if (Button1.Text == "Submit")
        {
            Insertdata("I");
        }
        else
        {
            theinstrumentcost.DeleteInstrument(Session["CoCode"].ToString().Trim(), Session["YearCode"].ToString().Trim(), TextBox46.Text, TextBox47.Text,type);
            Insertdata("U");
        }
       
        if (type == "T")
        {
            Response.Redirect("../IPD/OTList.aspx");
        }
        else
        {
            Response.Redirect("../IPD/AdmissionPatientList.aspx");
        }
        GridFill();
        ResetAllFields();
    }
    protected void Button4_Click(object sender, EventArgs e)
    {
        ResetAllFields();
    }
    private void ResetAllFields()
    {
        for (int i = 1; i <= 52; i++)
        {
            TextBox t = (TextBox)Page.FindControl("ctl00$ContentPlaceHolder1$TextBox" + i.ToString());
            t.Text = "";
        }
        for (int d = 1; d <= 15; d++)
        {
            DropDownList d1 = (DropDownList)Page.FindControl("ctl00$ContentPlaceHolder1$DropDownList" + d.ToString());
            d1.SelectedIndex = 0;
        }
        Button1.Text = "Submit";
        if (Objmain.checkaccss(Session["CoCode"].ToString(), Session["UserRoleID"].ToString().Trim(), "INSTRUMENT COST", checkAccessType.InsertAction) == false)
        {
            Button1.Enabled = false;
        }

    }
    protected void GridView2_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "Select")
        {
            ResetAllFields();
         
            int index = Convert.ToInt32(e.CommandArgument);
            lblError.Text = "";
            Label lblid = (Label)GridView2.Rows[index].FindControl("lblid");
            TextBox47.Text = lblid.Text;

            Label lblRegno = (Label)GridView2.Rows[index].FindControl("lblRegno");
            TextBox46.Text = lblRegno.Text;
            GridFill();
        }
    }

    protected void GridView3_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "Select")
        {
            ResetAllFields();
            int index = Convert.ToInt32(e.CommandArgument);
            lblError.Text = "";
            Label lblid = (Label)GridView3.Rows[index].FindControl("lblid");
            TextBox47.Text = lblid.Text;
            Label lblRegno = (Label)GridView3.Rows[index].FindControl("lblRegno");
            TextBox46.Text = lblRegno.Text;
            GridFill();
        }
    }
    //private void Dtls()
    //{
    //DataTable dtreq = theinstrumentcost.Getonltreq(TextBox46.Text);
    //    if (dtreq.Rows.Count > 0)
    //    {
    //        TextBox47.Text = dtreq.Rows[0][0].ToString();
    //        GridFill();
    //    }
    //    else
    //    {
    //        lblError.ForeColor = System.Drawing.Color.Red;
    //        lblError.Text = " No OT is done of This patient";
    //    }

    //}

    private void GridFillSecond(string type)
    {
        DataTable dt = theinstrumentcost.GridFillSecond(Session["CoCode"].ToString().Trim(), Session["YearCode"].ToString().Trim(), TextBox46.Text,type);
        
            if (dt.Rows.Count > 0)
            {
                if (type == "T")
                {
                GridView2.DataSource = dt;
                GridView2.DataBind();
                }
                else
                {
                    GridView3.DataSource = dt;
                    GridView3.DataBind();
                    
                 }
            }
        
        
    }
    protected void Button2_Click(object sender, EventArgs e)
    {
        string type = ViewState["type"].ToString();
        GridFill();
        GridFillSecond(type);
    }
    protected void GridView1_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        GridView1.PageIndex = e.NewPageIndex;
        GridFill();
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

    protected void ins1_selIndexChange(object sender, EventArgs e)
    {
        string cost=GetCost(DropDownList1.SelectedValue, 1);
        TextBox1.Text = "1";
        TextBox2.Text = cost;
    }
    protected void ins2_selIndexChange(object sender, EventArgs e)
    {
        string cost = GetCost(DropDownList2.SelectedValue, 2);
        TextBox4.Text = "1";
        TextBox5.Text = cost;
    }

    protected void ins3_selIndexChange(object sender, EventArgs e)
    {
        string cost = GetCost(DropDownList3.SelectedValue, 3);
        TextBox7.Text = "1";
        TextBox8.Text = cost;
    }

    protected void ins4_selIndexChange(object sender, EventArgs e)
    {
        string cost = GetCost(DropDownList4.SelectedValue, 4);
        TextBox10.Text = "1";
        TextBox11.Text = cost;
    }
    protected void ins5_selIndexChange(object sender, EventArgs e)
    {
        string cost = GetCost(DropDownList5.SelectedValue, 5);
        TextBox13.Text = "1";
        TextBox14.Text = cost;
    }

    protected void ins6_selIndexChange(object sender, EventArgs e)
    {
        string cost = GetCost(DropDownList6.SelectedValue, 6);
        TextBox16.Text = "1";
        TextBox17.Text = cost;
    }

    protected void ins7_selIndexChange(object sender, EventArgs e)
    {
        string cost = GetCost(DropDownList7.SelectedValue,7);
        TextBox19.Text = "1";
        TextBox20.Text = cost;
    }

    protected void ins8_selIndexChange(object sender, EventArgs e)
    {
        string cost = GetCost(DropDownList8.SelectedValue, 8);
        TextBox22.Text = "1";
        TextBox23.Text = cost;
    }

    protected void ins9_selIndexChange(object sender, EventArgs e)
    {
        string cost = GetCost(DropDownList9.SelectedValue, 9);
        TextBox25.Text = "1";
        TextBox26.Text = cost;
    }

    protected void ins10_selIndexChange(object sender, EventArgs e)
    {
        string cost = GetCost(DropDownList10.SelectedValue, 10);
        TextBox28.Text = "1";
        TextBox29.Text = cost;
    }

    protected void ins11_selIndexChange(object sender, EventArgs e)
    {
        string cost = GetCost(DropDownList11.SelectedValue, 11);
        TextBox31.Text = "1";
        TextBox32.Text = cost;
    }

    protected void ins12_selIndexChange(object sender, EventArgs e)
    {
        string cost = GetCost(DropDownList12.SelectedValue, 12);
        TextBox34.Text = "1";
        TextBox35.Text = cost;
    }

    protected void ins13_selIndexChange(object sender, EventArgs e)
    {
        string cost = GetCost(DropDownList13.SelectedValue,13);
        TextBox37.Text = "1";
        TextBox38.Text = cost;
    }

    protected void ins14_selIndexChange(object sender, EventArgs e)
    {
        string cost = GetCost(DropDownList14.SelectedValue, 14);
        TextBox40.Text = "1";
        TextBox41.Text = cost;
    }

    protected void ins15_selIndexChange(object sender, EventArgs e)
    {
        string cost = GetCost(DropDownList15.SelectedValue, 15);
        TextBox43.Text = "1";
        TextBox44.Text = cost;
    }
    protected string GetCost(string ins, int i)
    {
        string type = ViewState["type"].ToString();
        DataTable dt = theinstrumentcost.GetCost(Session["CoCode"].ToString().Trim(),ins,type);
        string cost = dt.Rows[0]["UnitCost"].ToString();
        return cost;

    }
}