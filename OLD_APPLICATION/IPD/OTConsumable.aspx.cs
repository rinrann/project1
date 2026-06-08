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

public partial class IPD_OTConsumable : System.Web.UI.Page
{
    Main Objmain = new Main(ConfigurationSettings.AppSettings["ConnectionString"].ToString());
    PatientDoctorVisit thedocvisit = new PatientDoctorVisit(ConfigurationSettings.AppSettings["ConnectionString"].ToString());
    OTConsumable theotconsumable = new OTConsumable(ConfigurationSettings.AppSettings["ConnectionString"].ToString());
    ServiceTemplate theshift = new ServiceTemplate(ConfigurationSettings.AppSettings["ConnectionString"].ToString());
    TextBox t1, t2, t3, t4;
    protected void Page_Load(object sender, EventArgs e)
    {

        Page.Title = "OT Consumable";
        if (Session["userName"] == null)
        {
            Response.Redirect("../LoginPage.aspx");
        }
        if (Objmain.checkaccss(Session["CoCode"].ToString(), Session["UserRoleID"].ToString().Trim(), "OT CONSUMABLES", checkAccessType.ViewAction) == false)
        {
            Response.Redirect("../AccessDenied.aspx");
        }

        if (Objmain.checkaccss(Session["CoCode"].ToString(), Session["UserRoleID"].ToString().Trim(), "OT CONSUMABLES", checkAccessType.InsertAction) == false)
        {
            Button1.Enabled = false;
        }
        if (!IsPostBack)
        {

            Tab1Func();
            if (Session["ReqNo"] != null)
            {
                TextBox2.Text = Session["ReqNo"].ToString();
                DataTable dt = theotconsumable.Getonlyreg(Session["CoCode"].ToString().Trim(), Session["YearCode"].ToString().Trim(),TextBox2.Text);
                TextBox1.Text = dt.Rows[0][0].ToString();
                //   Dtls();
                GridFillSecond();
                GridFill();

            }
            Session["ReqNo"] = null;
            DropDownFill();
        }


    }

    private void ConItemFill(string value, DropDownList d2)
    {
        d2.Items.Clear();
        d2.DataSource = theotconsumable.DropDownConsumable(value);
        d2.DataTextField = "ConItemName";
        d2.DataValueField = "ConItemID";
        d2.DataBind();
        d2.Items.Insert(0, new ListItem("--Select--", "0"));
    }

    private void DropDownFill()
    {
        ddlTempGroup.Items.Clear();
        this.ddlTempGroup.DataSource = theshift.OperationNames();
        this.ddlTempGroup.DataTextField = "OperationName";
        this.ddlTempGroup.DataValueField = "OperationID";
        this.ddlTempGroup.DataBind();
        this.ddlTempGroup.Items.Insert(0, new ListItem("--Select--", "0"));

        for (int i = 1; i <= 32; i = i + 3)
        {
            DropDownList d1 = (DropDownList)Page.FindControl("ctl00$ContentPlaceHolder1$DropDownList" + i.ToString());
            d1.DataSource = theotconsumable.DropDownConsumablegroup();
            d1.DataTextField = "ConGroupName";
            d1.DataValueField = "ConGrId";
            d1.DataBind();
            d1.Items.Insert(0, new ListItem("--Select--", "0"));

            DropDownList d2 = (DropDownList)Page.FindControl("ctl00$ContentPlaceHolder1$DropDownList" + (i + 1).ToString());
            d2.Items.Insert(0, new ListItem("--Select--", "0"));

            DropDownList d3 = (DropDownList)Page.FindControl("ctl00$ContentPlaceHolder1$DropDownList" + (i + 2).ToString());
            d3.DataSource = theotconsumable.DropDowndoc();
            d3.DataTextField = "doc_name";
            d3.DataValueField = "doc_id";
            d3.DataBind();
            d3.Items.Insert(0, new ListItem("--Select--", "0"));
        }

    }
    public void ResetAllFields()
    {
        HiddenField1.Value = "";
        Button1.Text = "Submit";
        for (int i = 1; i <= 33; i++)
        {
            DropDownList d1 = (DropDownList)Page.FindControl("ctl00$ContentPlaceHolder1$DropDownList" + i.ToString());
            d1.SelectedIndex = 0;
        }
        for (int t = 1; t <= 40; t++)
        {
            t1 = (TextBox)Page.FindControl("ctl00$ContentPlaceHolder1$TextBox" + t.ToString());
            t1.Text = "";
        }
        if (Objmain.checkaccss(Session["CoCode"].ToString(), Session["UserRoleID"].ToString().Trim(), "OT CONSUMABLES", checkAccessType.InsertAction) == false)
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
            TextBox2.Text = lblid.Text;

            Label lblRegno = (Label)GridView2.Rows[index].FindControl("lblRegno");
            TextBox1.Text = lblRegno.Text;
            GridFill();
        }
    }

    public void FillMedicineDtls(DataTable dt)
    {
        DropDownList d1, d2, d3;
        for (int i = 0, t = 8, d = 1; i < dt.Rows.Count; i++, t = t + 3, d = d + 3)
        {
            d1 = (DropDownList)Page.FindControl("ctl00$ContentPlaceHolder1$DropDownList" + d.ToString());
            d2 = (DropDownList)Page.FindControl("ctl00$ContentPlaceHolder1$DropDownList" + (d + 1).ToString());
            d3 = (DropDownList)Page.FindControl("ctl00$ContentPlaceHolder1$DropDownList" + (d + 2).ToString());
            t1 = (TextBox)Page.FindControl("ctl00$ContentPlaceHolder1$TextBox" + t.ToString());
            t2 = (TextBox)Page.FindControl("ctl00$ContentPlaceHolder1$TextBox" + (t + 1).ToString());
            t3 = (TextBox)Page.FindControl("ctl00$ContentPlaceHolder1$TextBox" + (t + 2).ToString());
            t1.Text = dt.Rows[i]["ActualQty"].ToString();
            t2.Text = dt.Rows[i]["BillQty"].ToString();
            t3.Text = dt.Rows[i]["Comment"].ToString();
            d1.SelectedValue = dt.Rows[i]["ConsumableGrID"].ToString();
            ConItemFill(dt.Rows[i]["ConsumableGrID"].ToString(), d2);

            d2.SelectedValue = dt.Rows[i]["ConsumableID"].ToString();
            d3.SelectedValue = dt.Rows[i]["AdviceBy"].ToString();

        }
    }
    private void GridFill()
    {
        DataTable dt = theotconsumable.FetchFron2ndGrid(Session["CoCode"].ToString().Trim(), Session["YearCode"].ToString().Trim(), TextBox1.Text, TextBox2.Text);
        DataTable dt1 = theotconsumable.Getonlypat(Session["CoCode"].ToString().Trim(), Session["YearCode"].ToString().Trim(), TextBox1.Text, TextBox2.Text);
        if (dt.Rows.Count > 0)
        {
            GridView1.DataSource = dt;
            GridView1.DataBind();
            TextBox7.Text = dt.Rows[0]["issudate"].ToString();
            HiddenField1.Value = dt.Rows[0]["RowID"].ToString();
            TextBox2.Text = dt.Rows[0]["OperationReqID"].ToString();
            TextBox3.Text = dt.Rows[0]["patient_name"].ToString();
            TextBox4.Text = dt.Rows[0]["BedNoText"].ToString();
            TextBox5.Text = dt.Rows[0]["OperationName"].ToString();
            TextBox6.Text = dt.Rows[0]["adate"].ToString();
            FillMedicineDtls(dt);
            Button1.Text = "Update";
            if (Objmain.checkaccss(Session["CoCode"].ToString(), Session["UserRoleID"].ToString().Trim(), "OT CONSUMABLES", checkAccessType.UpdateAction) == false)
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
                TextBox2.Text = dt1.Rows[0]["OperationReqID"].ToString();
                TextBox3.Text = dt1.Rows[0]["patient_name"].ToString();
                TextBox4.Text = dt1.Rows[0]["BedNoText"].ToString();
                TextBox5.Text = dt1.Rows[0]["OperationName"].ToString();
                TextBox6.Text = dt1.Rows[0]["adate"].ToString();

            }

        }
    }
    private void GridFillSecond()
    {
        DataTable dt = theotconsumable.GridFillSecond(Session["CoCode"].ToString().Trim(), Session["YearCode"].ToString().Trim(), TextBox1.Text);
        if (dt.Rows.Count > 0)
        {
            GridView2.DataSource = dt;
            GridView2.DataBind();
        }
    }
    private void Dtls()
    {
        DataTable dtreq = theotconsumable.Getonltreq(Session["CoCode"].ToString().Trim(), Session["YearCode"].ToString().Trim(),TextBox1.Text);
        lblError.Text = "";
        if (dtreq.Rows.Count > 0)
        {
            TextBox2.Text = dtreq.Rows[0][0].ToString();
            GridFill();
        }
        else
        {
            lblError.ForeColor = System.Drawing.Color.Red;
            lblError.Text = "No Operation Of This Patient Is Done..";
        }

    }
    protected void Button4_Click(object sender, EventArgs e)
    {
        Dtls();
        GridFillSecond();
    }
    protected void GridView2_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {

    }
    public void Insertdata(string flag)
    {
        System.Globalization.DateTimeFormatInfo dtf = new DateTimeFormatInfo();
        DateTime testdate = DateTime.ParseExact(TextBox7.Text, "dd/MM/yyyy", dtf);

        DropDownList d1, d2, d3;
        for (int t = 8, d = 1; d <= 32; t = t + 3, d = d + 3)
        {
            d1 = (DropDownList)Page.FindControl("ctl00$ContentPlaceHolder1$DropDownList" + d.ToString());
            d2 = (DropDownList)Page.FindControl("ctl00$ContentPlaceHolder1$DropDownList" + (d + 1).ToString());
            d3 = (DropDownList)Page.FindControl("ctl00$ContentPlaceHolder1$DropDownList" + (d + 2).ToString());
            t1 = (TextBox)Page.FindControl("ctl00$ContentPlaceHolder1$TextBox" + t.ToString());
            t2 = (TextBox)Page.FindControl("ctl00$ContentPlaceHolder1$TextBox" + (t + 1).ToString());
            t3 = (TextBox)Page.FindControl("ctl00$ContentPlaceHolder1$TextBox" + (t + 2).ToString());
            if (t1.Text != "" && t2.Text != "")
            {
                if (theotconsumable.InsertOTConsumable(TextBox1.Text, TextBox2.Text, d1.SelectedValue, d2.SelectedValue, testdate.ToString("yyyy-MM-dd"), t1.Text, t2.Text, d3.SelectedValue, t3.Text, Session["CoCode"].ToString().Trim(), Session["userName"].ToString().Trim(), Session["YearCode"].ToString().Trim()) == true)
                {
                    if (flag == "I")
                    {
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "msg", "alert('Inserted  Successfully !');", true);
                    }
                    else
                    {
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "msg", "alert('Updated  Successfully !');", true);
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
                    break;
                }
            }
            else
                break;
        }
    }
    protected void Button1_Click(object sender, EventArgs e)
    {

        if (Button1.Text == "Submit")
        {
            Insertdata("I");
        }
        else
        {
            theotconsumable.DelOTConsumable(Session["CoCode"].ToString().Trim(), Session["YearCode"].ToString().Trim(),TextBox1.Text, TextBox2.Text);
            Insertdata("U");
        }
        Response.Redirect("../IPD/OTList.aspx");
        GridFill();
        ResetAllFields();
    }
    protected void Button2_Click(object sender, EventArgs e)
    {
        ResetAllFields();
    }
    protected void GridView1_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {

    }
    protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        Tab1Func();
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
    protected void DropDownList1_SelectedIndexChanged(object sender, EventArgs e)
    {
        ConItemFill(DropDownList1.SelectedValue, DropDownList2);
    }
    protected void DropDownList4_SelectedIndexChanged(object sender, EventArgs e)
    {
        ConItemFill(DropDownList4.SelectedValue, DropDownList5);
    }
    protected void DropDownList7_SelectedIndexChanged(object sender, EventArgs e)
    {
        ConItemFill(DropDownList7.SelectedValue, DropDownList8);
    }
    protected void DropDownList10_SelectedIndexChanged(object sender, EventArgs e)
    {
        ConItemFill(DropDownList10.SelectedValue, DropDownList11);
    }
    protected void DropDownList13_SelectedIndexChanged(object sender, EventArgs e)
    {
        ConItemFill(DropDownList13.SelectedValue, DropDownList14);
    }
    protected void DropDownList16_SelectedIndexChanged(object sender, EventArgs e)
    {
        ConItemFill(DropDownList16.SelectedValue, DropDownList17);
    }
    protected void DropDownList19_SelectedIndexChanged(object sender, EventArgs e)
    {
        ConItemFill(DropDownList19.SelectedValue, DropDownList20);
    }
    protected void DropDownList22_SelectedIndexChanged(object sender, EventArgs e)
    {
        ConItemFill(DropDownList22.SelectedValue, DropDownList23);
    }
    protected void DropDownList25_SelectedIndexChanged(object sender, EventArgs e)
    {
        ConItemFill(DropDownList25.SelectedValue, DropDownList26);
    }
    protected void DropDownList28_SelectedIndexChanged(object sender, EventArgs e)
    {
        ConItemFill(DropDownList28.SelectedValue, DropDownList29);
    }
    protected void DropDownList31_SelectedIndexChanged(object sender, EventArgs e)
    {
        ConItemFill(DropDownList31.SelectedValue, DropDownList32);
    }
    protected void ddlTempGroup_SelectedIndexChanged(object sender, EventArgs e)
    {
        //ddlTemplate.Items.Clear();
        //this.ddlTemplate.DataSource = thedocvisit.GetOperationConsumableTemplate(ddlTempGroup.SelectedValue);
        //this.ddlTemplate.DataTextField = "ServiceTemplateName";
        //this.ddlTemplate.DataValueField = "NameID";
        //this.ddlTemplate.DataBind();
        //this.ddlTemplate.Items.Insert(0, new ListItem("--Select--", "0"));

        DataTable dtcon = thedocvisit.GetOTConsumableTemplateMapping(ddlTempGroup.SelectedValue);
        if (dtcon.Rows.Count > 0)
        {
            for (int i = 0, d = 1, t = 8; i < dtcon.Rows.Count; i++, d = d + 3, t = t + 3)
            {
                DropDownList d1 = (DropDownList)Page.FindControl("ctl00$ContentPlaceHolder1$DropDownList" + d.ToString());
                DropDownList d2 = (DropDownList)Page.FindControl("ctl00$ContentPlaceHolder1$DropDownList" + (d + 1).ToString());
                t1 = (TextBox)Page.FindControl("ctl00$ContentPlaceHolder1$TextBox" + t.ToString());
                t2 = (TextBox)Page.FindControl("ctl00$ContentPlaceHolder1$TextBox" + (t + 1).ToString());
                d1.SelectedValue = dtcon.Rows[i]["ConGrId"].ToString();
                ConItemFill(dtcon.Rows[i]["ConGrId"].ToString(), d2);
                d2.SelectedValue = dtcon.Rows[i]["ConItemID"].ToString();
                t1.Text = dtcon.Rows[i]["ActualQty"].ToString();
                t2.Text = dtcon.Rows[i]["BillQty"].ToString();
            }
        }
    }
    protected void ddlTemplate_SelectedIndexChanged(object sender, EventArgs e)
    {
        DataTable dtcon = thedocvisit.GetOTConsumableTemplateMapping(ddlTemplate.SelectedValue);
        if (dtcon.Rows.Count > 0)
        {
            for (int i = 0, d = 1, t = 8; i < dtcon.Rows.Count; i++, d = d + 3, t = t + 3)
            {
                DropDownList d1 = (DropDownList)Page.FindControl("ctl00$ContentPlaceHolder1$DropDownList" + d.ToString());
                DropDownList d2 = (DropDownList)Page.FindControl("ctl00$ContentPlaceHolder1$DropDownList" + (d + 1).ToString());
                t1 = (TextBox)Page.FindControl("ctl00$ContentPlaceHolder1$TextBox" + t.ToString());
                t2 = (TextBox)Page.FindControl("ctl00$ContentPlaceHolder1$TextBox" + (t + 1).ToString());
                d1.SelectedValue = dtcon.Rows[i]["ConGrId"].ToString();
                ConItemFill(dtcon.Rows[i]["ConGrId"].ToString(), d2);
                d2.SelectedValue = dtcon.Rows[i]["ConItemID"].ToString();
                t1.Text = dtcon.Rows[i]["ActualQty"].ToString();
                t2.Text = dtcon.Rows[i]["BillQty"].ToString();
            }
        }
    }
}