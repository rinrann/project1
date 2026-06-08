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


public partial class IPD_DeliveryNote : System.Web.UI.Page
{
    Main Objmain = new Main(ConfigurationSettings.AppSettings["ConnectionString"].ToString());
    DeliveryNote thedeliveryNote = new DeliveryNote(ConfigurationSettings.AppSettings["ConnectionString"].ToString());

    protected void Page_Load(object sender, EventArgs e)
    {


        Page.Title = "Delivery Note";
        if (Session["userName"] == null)
        {
            Response.Redirect("../LoginPage.aspx");
        }
        if (Objmain.checkaccss(Session["CoCode"].ToString(), Session["UserRoleID"].ToString().Trim(), "DELIVERY NOTE", checkAccessType.ViewAction) == false)
        {
            Response.Redirect("../AccessDenied.aspx");
        }

        if (Objmain.checkaccss(Session["CoCode"].ToString(), Session["UserRoleID"].ToString().Trim(), "DELIVERY NOTE", checkAccessType.InsertAction) == false)
        {
            Button1.Enabled = false;
        }
        if (!IsPostBack)
        {
            Tab1Func();
            DropDownFill();
            callfunc(4,21);
            if (Session["ReqNo"] != null)
            {
                TextBox47.Text = Session["ReqNo"].ToString();
                DataTable dt = thedeliveryNote.Getonlyreg(Session["CoCode"].ToString().Trim(),Session["YearCode"].ToString().Trim(),TextBox47.Text);
                TextBox46.Text = dt.Rows[0][0].ToString();
                GridFillSecond();
                GridFill();
            }
            Session["ReqNo"] = null;
        }
    }

    public void fillDtls(DataTable dt)
    {
        for (int i = 0, d = 1,t0=21, t = 1; i < dt.Rows.Count; i++, d=d+4, t = t + 4,t0=t0+2)
        {
            DropDownList d1 = (DropDownList)Page.FindControl("ctl00$ContentPlaceHolder1$DropDownList" + d.ToString());
            DropDownList d2 = (DropDownList)Page.FindControl("ctl00$ContentPlaceHolder1$DropDownList" + (d+1).ToString());
            DropDownList d3 = (DropDownList)Page.FindControl("ctl00$ContentPlaceHolder1$DropDownList" + (d + 2).ToString());
            DropDownList d4 = (DropDownList)Page.FindControl("ctl00$ContentPlaceHolder1$DropDownList" + (d + 3).ToString());
            TextBox t1 = (TextBox)Page.FindControl("ctl00$ContentPlaceHolder1$TextBox" + t.ToString());
            TextBox t2 = (TextBox)Page.FindControl("ctl00$ContentPlaceHolder1$TextBox" + (t + 1).ToString());
            TextBox t3 = (TextBox)Page.FindControl("ctl00$ContentPlaceHolder1$TextBox" + (t + 2).ToString());
            TextBox t4 = (TextBox)Page.FindControl("ctl00$ContentPlaceHolder1$TextBox" + (t + 3).ToString());

            TextBox t5 = (TextBox)Page.FindControl("ctl00$ContentPlaceHolder1$TextBox" + t0.ToString());
            TextBox t6 = (TextBox)Page.FindControl("ctl00$ContentPlaceHolder1$TextBox" + (t0 + 1).ToString());

            d1.SelectedValue = dt.Rows[i]["Sex"].ToString();
            d2.SelectedValue = dt.Rows[i]["Cry"].ToString();
            d3.SelectedValue = dt.Rows[i]["Liquor"].ToString();
            d4.SelectedValue = dt.Rows[i]["Maturity"].ToString();

            t1.Text = dt.Rows[i]["Weight"].ToString();
            t2.Text = dt.Rows[i]["AGScore"].ToString();
            t3.Text = dt.Rows[i]["AGScoreAfter"].ToString();
            t4.Text = dt.Rows[i]["Remarks"].ToString();

            t5.Text = dt.Rows[i]["ddate"].ToString();
            t6.Text = dt.Rows[i]["DeliveryTime"].ToString();
        }

    }

    private void babyfunction(DataTable dt)
    {
        DropDownList25.SelectedValue = dt.Rows[0]["NoofBaby"].ToString();

        if (dt.Rows[0]["NoofBaby"].ToString() == "Single")
        {
            callfunc(4, 21);
        }

        if (dt.Rows[0]["NoofBaby"].ToString() == "Twin")
        {
            callfunc(8, 23);
        }
        if (dt.Rows[0]["NoofBaby"].ToString() == "Triplet")
        {
            callfunc(12, 25);
        }
        if (dt.Rows[0]["NoofBaby"].ToString() == "Quadruplet")
        {
            callfunc(16, 27);
        }

        if (dt.Rows[0]["NoofBaby"].ToString() == "Quintuplet")
        {
            callfunc(20, 29);
        }

    }
    private void GridFill()
    {
        DataTable dt = thedeliveryNote.GetAllDelivery(TextBox46.Text, TextBox47.Text,Session["CoCode"].ToString().Trim(), Session["YearCode"].ToString().Trim());
        DataTable dt1 = thedeliveryNote.Getonlypat(TextBox46.Text, TextBox47.Text, Session["CoCode"].ToString().Trim(), Session["YearCode"].ToString().Trim());
        if (dt.Rows.Count > 0)
        {
            GridView1.DataSource = dt;
            GridView1.DataBind();
            GridView1.Visible = true;
            HiddenField1.Value = dt.Rows[0]["DeliveryNoteID"].ToString();
            TextBox48.Text = dt.Rows[0]["patient_name"].ToString();
            TextBox49.Text = dt.Rows[0]["BedNoText"].ToString();
            TextBox51.Text = dt.Rows[0]["adate"].ToString();
            babyfunction(dt);
            DropDownList24.SelectedValue = dt.Rows[0]["ModeofDelivery"].ToString();
            TextBox50.Text = dt.Rows[0]["OperationName"].ToString();
            fillDtls(dt);
            Button1.Text = "Update";

            if (Objmain.checkaccss(Session["CoCode"].ToString(), Session["UserRoleID"].ToString().Trim(), "DELIVERY NOTE", checkAccessType.UpdateAction) == false)
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
    private void DropDownFill()
    {
        DropDownList24.DataSource = thedeliveryNote.DropdownDeliveryMode(Session["CoCode"].ToString().Trim());
         DropDownList24.DataTextField = "DeliveryMode";
         DropDownList24.DataValueField = "ID";
         DropDownList24.DataBind();
         DropDownList24.Items.Insert(0, new ListItem("--Select--", "0"));
        for (int i = 1; i <=17; i=i+4)
        {
            DropDownList d1 = (DropDownList)Page.FindControl("ctl00$ContentPlaceHolder1$DropDownList" + i.ToString());
            d1.DataSource = thedeliveryNote.DropdownChildSex(Session["CoCode"].ToString().Trim());
            d1.DataTextField = "SexName";
            d1.DataValueField = "ID";
            d1.DataBind();
            d1.Items.Insert(0, new ListItem("--Select--", "0"));


            DropDownList d2 = (DropDownList)Page.FindControl("ctl00$ContentPlaceHolder1$DropDownList" + (i+1).ToString());
            d2.DataSource = thedeliveryNote.DropdownCry(Session["CoCode"].ToString().Trim());
            d2.DataTextField = "Name";
            d2.DataValueField = "ID";
            d2.DataBind();
            d2.Items.Insert(0, new ListItem("--Select--", "0"));


            DropDownList d3 = (DropDownList)Page.FindControl("ctl00$ContentPlaceHolder1$DropDownList" + (i + 2).ToString());
            d3.DataSource = thedeliveryNote.DropdownLiquor(Session["CoCode"].ToString().Trim());
            d3.DataTextField = "LiquorName";
            d3.DataValueField = "ID";
            d3.DataBind();
            d3.Items.Insert(0, new ListItem("--Select--", "0"));


            DropDownList d4 = (DropDownList)Page.FindControl("ctl00$ContentPlaceHolder1$DropDownList" + (i + 3).ToString());
            d4.DataSource = thedeliveryNote.DropdownMaturity(Session["CoCode"].ToString().Trim());
            d4.DataTextField = "MaturityName";
            d4.DataValueField = "ID";
            d4.DataBind();
            d4.Items.Insert(0, new ListItem("--Select--", "0"));
        }

    }

    protected void GridView1_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        GridView1.PageIndex = e.NewPageIndex;
        GridFill();
    }
    public void Insertdata(string flag)
    {
        TextBox t1, t2, t3,t4,t5,t6;
        DropDownList d1,d2,d3,d4 ;
        System.Globalization.DateTimeFormatInfo dtf = new DateTimeFormatInfo();
       

         for (int t = 1,td=21, d = 1; d <= 17; t = t + 4, d=d+4,td=td+2)
        {
            t1 = (TextBox)Page.FindControl("ctl00$ContentPlaceHolder1$TextBox" + t.ToString());
            t2 = (TextBox)Page.FindControl("ctl00$ContentPlaceHolder1$TextBox" + (t + 1).ToString());
            t3 = (TextBox)Page.FindControl("ctl00$ContentPlaceHolder1$TextBox" + (t + 2).ToString());
            t4 = (TextBox)Page.FindControl("ctl00$ContentPlaceHolder1$TextBox" + (t + 3).ToString());
            t5 = (TextBox)Page.FindControl("ctl00$ContentPlaceHolder1$TextBox" + td.ToString());
            t6 = (TextBox)Page.FindControl("ctl00$ContentPlaceHolder1$TextBox" + (td + 1).ToString());
            d1 = (DropDownList)Page.FindControl("ctl00$ContentPlaceHolder1$DropDownList" + d.ToString());
            d2 = (DropDownList)Page.FindControl("ctl00$ContentPlaceHolder1$DropDownList" + (d+1).ToString());
            d3 = (DropDownList)Page.FindControl("ctl00$ContentPlaceHolder1$DropDownList" + (d+2).ToString());
            d4 = (DropDownList)Page.FindControl("ctl00$ContentPlaceHolder1$DropDownList" + (d+3).ToString());
            if (t1.Text != "" &&  t5.Text != "" && t6.Text != "")
            {
                DateTime testdate = DateTime.ParseExact(t5.Text, "dd/MM/yyyy", dtf);

                if (thedeliveryNote.InsertDelNoteMap(testdate.ToString("yyyy-MM-dd"), t6.Text, TextBox46.Text, TextBox47.Text, d1.SelectedValue, t1.Text, d4.SelectedValue, d2.SelectedValue, d3.SelectedValue, t2.Text, t3.Text, t4.Text, Session["CoCode"].ToString().Trim(), Session["userName"].ToString().Trim(), Session["YearCode"].ToString().Trim()) == true)
                {
                    if (flag == "I")
                    {
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "msg", "alert('Insered Successfully !');", true);
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
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "msg", "alert('Error in Insered Data !');", true);
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

    private void ResetAllFields()
    {
        for (int i = 1; i <= 30; i++)
        {
            TextBox t = (TextBox)Page.FindControl("ctl00$ContentPlaceHolder1$TextBox" + i.ToString());
            t.Text = "";
        }

        DropDownList24.SelectedIndex = 0; DropDownList25.SelectedIndex = 0;
        for (int i = 46; i <= 51; i++)
        {
            TextBox t = (TextBox)Page.FindControl("ctl00$ContentPlaceHolder1$TextBox" + i.ToString());
            t.Text = "";
        }
        for (int d = 1; d <= 20; d++)
        {
            DropDownList d1 = (DropDownList)Page.FindControl("ctl00$ContentPlaceHolder1$DropDownList" + d.ToString());
           
            d1.SelectedIndex = 0;
        }
        Button1.Text = "Submit";
        if (Objmain.checkaccss(Session["CoCode"].ToString(), Session["UserRoleID"].ToString().Trim(), "DELIVERY NOTE", checkAccessType.InsertAction) == false)
        {
            Button1.Enabled = false;
        }

    }
    private void GridFillSecond()
    {
        DataTable dt = thedeliveryNote.GridFillSecond(TextBox46.Text, Session["CoCode"].ToString().Trim(), Session["YearCode"].ToString().Trim());
        if (dt.Rows.Count > 0)
        {
            GridView2.DataSource = dt;
            GridView2.DataBind();
        }
    }

    private void Dtls()
    {
         DataTable dtreq = thedeliveryNote.Getonltreq(TextBox46.Text);
        if (dtreq.Rows.Count > 0)
        {
            TextBox47.Text = dtreq.Rows[0][0].ToString();
            GridFill();
        }
        else
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "msg", "alert('No OT of this Patient is done !');", true);
        }

    }
    protected void Button2_Click(object sender, EventArgs e)
    {
        Dtls();
        GridFillSecond();
    }
    protected void Button4_Click(object sender, EventArgs e)
    {
        ResetAllFields();
    }
    protected void Button1_Click(object sender, EventArgs e)
    {
        if (DropDownList25.SelectedIndex == 0)
        {
             
                if (Button1.Text == "Submit")
                {
                    thedeliveryNote.InsertDeliveryNote(TextBox46.Text, TextBox47.Text, DropDownList24.SelectedValue, DropDownList25.SelectedValue, Session["CoCode"].ToString().Trim(), Session["userName"].ToString().Trim(), Session["YearCode"].ToString().Trim());
                    Insertdata("I");
                }
                else
                {
                    thedeliveryNote.DeleteDeliveryNote(HiddenField1.Value, Session["CoCode"].ToString().Trim(), Session["YearCode"].ToString().Trim());
                    thedeliveryNote.UpdateDeliveryNote(TextBox46.Text, TextBox47.Text, DropDownList24.SelectedValue, DropDownList25.SelectedValue, Session["CoCode"].ToString().Trim(), Session["userName"].ToString().Trim(), Session["YearCode"].ToString().Trim());
                    Insertdata("U");
                } 
                GridFill();
                ResetAllFields();     
 
        }

        if (DropDownList25.SelectedIndex == 1)
        {
            if (TextBox5.Text != "" && DropDownList5.SelectedIndex != 0 && TextBox23.Text != "" && TextBox24.Text != "")
            {
                if (Button1.Text == "Submit")
                {
                    thedeliveryNote.InsertDeliveryNote(TextBox46.Text, TextBox47.Text, DropDownList24.SelectedValue, DropDownList25.SelectedValue, Session["CoCode"].ToString().Trim(), Session["userName"].ToString().Trim(), Session["YearCode"].ToString().Trim());
                    Insertdata("I");
                }
                else
                {
                    thedeliveryNote.DeleteDeliveryNote(HiddenField1.Value, Session["CoCode"].ToString().Trim(), Session["YearCode"].ToString().Trim());
                    thedeliveryNote.UpdateDeliveryNote(TextBox46.Text, TextBox47.Text, DropDownList24.SelectedValue, DropDownList25.SelectedValue, Session["CoCode"].ToString().Trim(), Session["userName"].ToString().Trim(), Session["YearCode"].ToString().Trim());
                    Insertdata("U");
                }
                GridFill();
                ResetAllFields();
            }
            else
            {
                lblError.ForeColor = System.Drawing.Color.Red;
                lblError.Text = "You don't enter either Date/Time/Sex/Weight..";
            }
        }


        if (DropDownList25.SelectedIndex == 2)
        {
            if (TextBox5.Text != "" && DropDownList5.SelectedIndex != 0 && TextBox23.Text != "" && TextBox24.Text != "" && TextBox9.Text != "" && DropDownList9.SelectedIndex != 0 && TextBox25.Text != "" && TextBox26.Text != "")
            {
                if (Button1.Text == "Submit")
                {
                    thedeliveryNote.InsertDeliveryNote(TextBox46.Text, TextBox47.Text, DropDownList24.SelectedValue, DropDownList25.SelectedValue, Session["CoCode"].ToString().Trim(), Session["userName"].ToString().Trim(), Session["YearCode"].ToString().Trim());
                    Insertdata("I");
                }
                else
                {
                    thedeliveryNote.DeleteDeliveryNote(HiddenField1.Value, Session["CoCode"].ToString().Trim(), Session["YearCode"].ToString().Trim());
                    thedeliveryNote.UpdateDeliveryNote(TextBox46.Text, TextBox47.Text, DropDownList24.SelectedValue, DropDownList25.SelectedValue, Session["CoCode"].ToString().Trim(), Session["userName"].ToString().Trim(), Session["YearCode"].ToString().Trim());
                    Insertdata("U");
                }
                GridFill();
                ResetAllFields();
            }
            else
            {
                lblError.ForeColor = System.Drawing.Color.Red;
                lblError.Text = "You don't enter either Date/Time/Sex/Weight..";
            }
        }



        if (DropDownList25.SelectedIndex == 3)
        {
            if (TextBox5.Text != "" && DropDownList5.SelectedIndex != 0 && TextBox23.Text != "" && TextBox24.Text != "" && TextBox9.Text != "" && DropDownList9.SelectedIndex != 0 && TextBox25.Text != "" && TextBox26.Text != "" && TextBox13.Text != "" && DropDownList13.SelectedIndex != 0 && TextBox27.Text != "" && TextBox28.Text != "")
            {
                if (Button1.Text == "Submit")
                {
                    thedeliveryNote.InsertDeliveryNote(TextBox46.Text, TextBox47.Text, DropDownList24.SelectedValue, DropDownList25.SelectedValue, Session["CoCode"].ToString().Trim(), Session["userName"].ToString().Trim(), Session["YearCode"].ToString().Trim());
                    Insertdata("I");
                }
                else
                {
                    thedeliveryNote.DeleteDeliveryNote(HiddenField1.Value, Session["CoCode"].ToString().Trim(), Session["YearCode"].ToString().Trim());
                    thedeliveryNote.UpdateDeliveryNote(TextBox46.Text, TextBox47.Text, DropDownList24.SelectedValue, DropDownList25.SelectedValue, Session["CoCode"].ToString().Trim(), Session["userName"].ToString().Trim(), Session["YearCode"].ToString().Trim());
                    Insertdata("U");
                }
                GridFill();
                ResetAllFields();
            }
            else
            {
                lblError.ForeColor = System.Drawing.Color.Red;
                lblError.Text = "You don't enter either Date/Time/Sex/Weight..";
            }
        }



        if (DropDownList25.SelectedIndex == 4)
        {
            if (TextBox5.Text != "" && DropDownList5.SelectedIndex != 0 && TextBox23.Text != "" && TextBox24.Text != "" && TextBox9.Text != "" && DropDownList9.SelectedIndex != 0 && TextBox25.Text != "" && TextBox26.Text != "" && TextBox13.Text != "" && DropDownList13.SelectedIndex != 0 && TextBox27.Text != "" && TextBox28.Text != "" && TextBox17.Text != "" && DropDownList17.SelectedIndex != 0 && TextBox29.Text != "" && TextBox30.Text != "")
            {
                if (Button1.Text == "Submit")
                {
                    thedeliveryNote.InsertDeliveryNote(TextBox46.Text, TextBox47.Text, DropDownList24.SelectedValue, DropDownList25.SelectedValue, Session["CoCode"].ToString().Trim(), Session["userName"].ToString().Trim(), Session["YearCode"].ToString().Trim());
                    Insertdata("I");
                }
                else
                {
                    thedeliveryNote.DeleteDeliveryNote(HiddenField1.Value, Session["CoCode"].ToString().Trim(), Session["YearCode"].ToString().Trim());
                    thedeliveryNote.UpdateDeliveryNote(TextBox46.Text, TextBox47.Text, DropDownList24.SelectedValue, DropDownList25.SelectedValue, Session["CoCode"].ToString().Trim(), Session["userName"].ToString().Trim(), Session["YearCode"].ToString().Trim());
                    Insertdata("U");
                }
                GridFill();
                ResetAllFields();
            }
            else
            {
                lblError.ForeColor = System.Drawing.Color.Red;
                lblError.Text = "You don't enter either Date/Time/Sex/Weight..";
            }
        }

        Response.Redirect("../IPD/OTList.aspx");

    }

    public void callfunc(int from,int next)
    {
        for (int i = from + 1; i <= 20; i = i + 4)
        {
            TextBox tb1 = (TextBox)Page.FindControl("ctl00$ContentPlaceHolder1$TextBox" + i.ToString());
            TextBox tb2 = (TextBox)Page.FindControl("ctl00$ContentPlaceHolder1$TextBox" + (i + 1).ToString());
            TextBox tb3 = (TextBox)Page.FindControl("ctl00$ContentPlaceHolder1$TextBox" + (i + 2).ToString());
            TextBox tb4 = (TextBox)Page.FindControl("ctl00$ContentPlaceHolder1$TextBox" + (i + 3).ToString());
               DropDownList d = (DropDownList)Page.FindControl("ctl00$ContentPlaceHolder1$DropDownList" + i.ToString());
            tb1.Enabled = false; tb2.Enabled = false; tb3.Enabled = false; tb4.Enabled = false; 
            d.Enabled = false;
        }

        for (int   i = next+2; i <= 30; i++)
        {
            TextBox tb1 = (TextBox)Page.FindControl("ctl00$ContentPlaceHolder1$TextBox" + i.ToString());
          
            tb1.Enabled = false; 
          
        }

        for (int i = 21; i < next+2; i++)
        {
            TextBox tb1 = (TextBox)Page.FindControl("ctl00$ContentPlaceHolder1$TextBox" + i.ToString());

            tb1.Enabled = true;

        }

        for (int i = 1; i <= from; i++)
        {
            TextBox t = (TextBox)Page.FindControl("ctl00$ContentPlaceHolder1$TextBox" + i.ToString());
            DropDownList d = (DropDownList)Page.FindControl("ctl00$ContentPlaceHolder1$DropDownList" + i.ToString());
            t.Enabled = true;
            d.Enabled = true;
        }
    }
    protected void DropDownList25_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (DropDownList25.SelectedIndex == 0)
        {
            callfunc(4,21);
        }

        if (DropDownList25.SelectedIndex == 1)
        {
            callfunc(8,23);
        }

        if (DropDownList25.SelectedIndex == 2)
        {
            callfunc(12,25);
        }

        if (DropDownList25.SelectedIndex == 3)
        {

            callfunc(16,27);
        }

        if (DropDownList25.SelectedIndex == 4)
        {

            callfunc(20,29);
        }

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
}