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

public partial class IPD_ConsumableTemplate : System.Web.UI.Page
{
    ConsumableTemplate theshift = new  ConsumableTemplate(ConfigurationSettings.AppSettings["ConnectionString"].ToString());

    DropDownList d1, d2;
    TextBox t1, t2;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["userName"] == null)
        {
            Response.Redirect("../LoginPage.aspx");
        }
        if (!IsPostBack)
        {
            Tab1Func();
            GridFill();
            DropDownFill();
        }
        Page.Title = "Consumable Template";


    }

    public void FillMap(string id)
    {
        DataTable fill = theshift.FillMap(id);
        if (fill.Rows.Count > 0)
        {
            for (int i = 0; i < fill.Rows.Count; i++)
            {
                d1 = (DropDownList)Page.FindControl("ctl00$ContentPlaceHolder1$ddlconsumablegr" + (i + 1).ToString());
                d2 = (DropDownList)Page.FindControl("ctl00$ContentPlaceHolder1$ddlConsumableItem" + (i + 1).ToString());
                t1 = (TextBox)Page.FindControl("ctl00$ContentPlaceHolder1$txtActualQty" + (i + 1).ToString());
                t2 = (TextBox)Page.FindControl("ctl00$ContentPlaceHolder1$txtBillQty" + (i + 1).ToString());

                d1.SelectedValue = fill.Rows[i]["ConsumableGrId"].ToString();
                ConsumableItemsFill("0", d2);
                d2.SelectedValue = fill.Rows[i]["ConsumableItemId"].ToString();
                t1.Text = fill.Rows[i]["ActualQty"].ToString();
                t2.Text = fill.Rows[i]["BillQty"].ToString();
            }
        }
    }

    public int InsertMapping()
    {
        int flag = 0;
        for (int i = 1; i <= 11; i++)
        {
            d1 = (DropDownList)Page.FindControl("ctl00$ContentPlaceHolder1$ddlconsumablegr" + i.ToString());
            d2 = (DropDownList)Page.FindControl("ctl00$ContentPlaceHolder1$ddlConsumableItem" + i.ToString());
            t1 = (TextBox)Page.FindControl("ctl00$ContentPlaceHolder1$txtActualQty" + i.ToString());
            t2 = (TextBox)Page.FindControl("ctl00$ContentPlaceHolder1$txtBillQty" + i.ToString());
            if (d1.SelectedIndex != 0 && d2.SelectedIndex != 0)
            {
                if (theshift.TemplateFillFunction(1,TextBox1.Text.Trim(), d1.SelectedValue, null, d2.SelectedValue, t1.Text, t2.Text) == true)
                {
                    flag = 1;
                }
                else
                {
                    flag = 0;
                }
            }
            else
            {
                break;
            }


        }
        return flag;
    }
    protected void Button1_Click(object sender, EventArgs e)
    {
        string id;
        int flag = 0;
        if (TextBox4.Value != "")
        {
            id = TextBox4.Value;
        }
        else
        {
            id = "0";
        }
        if (Button1.Text == "Submit")
        {
            //dibynedu
            if (theshift.NameFunction(1, id, DropDownList1.SelectedValue, TextBox1.Text, Session["userName"].ToString(), DateTime.Now.ToString("MM/dd/yyyy")) == true)
            {
              flag= InsertMapping();              
            }
            if (flag == 1)
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "msg", "alert('Insered Successfully !');", true);
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "msg", "alert('Error in Insered Data !');", true);
            }
          
        }
        else
        {
            //dibyendu
            if (theshift.NameFunction(2, id, DropDownList1.SelectedValue, TextBox1.Text, Session["userName"].ToString(), DateTime.Now.ToString("MM/dd/yyyy")) == true)
            {
                theshift.TemplateFillFunction(2,null, null, id, null, null, null);
                flag = InsertMapping(); 
            }

            if (flag == 1)
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "msg", "alert('updated Successfully !');", true);
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "msg", "alert('Error in updated Data !');", true);
            }

          
        }

        GridFill();
        ResetAllFields();
    }
    protected void Button2_Click(object sender, EventArgs e)
    {
        ResetAllFields();
    }

    private void DropDownFill()
    {
        this.DropDownList1.DataSource = theshift.GridFill();
        this.DropDownList1.DataTextField = "CategoryName";
        this.DropDownList1.DataValueField = "TemplateCategoryId";
        this.DropDownList1.DataBind();
        this.DropDownList1.Items.Insert(0, new ListItem("--Select--", "0"));

        for (int i = 1; i <= 11; i++)
        {
            d1 = (DropDownList)Page.FindControl("ctl00$ContentPlaceHolder1$ddlconsumablegr" + i.ToString());
            d2 = (DropDownList)Page.FindControl("ctl00$ContentPlaceHolder1$ddlConsumableItem" + i.ToString());

            d1.DataSource = theshift.ConsumableGroup();
            d1.DataTextField = "ConGroupName";
            d1.DataValueField = "ConGrId";
            d1.DataBind();
            d1.Items.Insert(0, new ListItem("--Select--", "0"));

            d2.Items.Insert(0, new ListItem("--Select--", "0"));
        }
    }

    private void GridFill()
    {
        GridView1.SelectedIndex = -1;
        GridView1.DataSource = theshift.GridFillName();
        GridView1.DataBind();
    }
    private void ResetAllFields()
    {
        TextBox1.Text = "";
        TextBox4.Value = "";
        DropDownList1.SelectedIndex = 0;
        Button1.Text = "Submit";

        for (int i = 1; i <= 11; i++)
        {
            d1 = (DropDownList)Page.FindControl("ctl00$ContentPlaceHolder1$ddlconsumablegr" + i.ToString());
            d2 = (DropDownList)Page.FindControl("ctl00$ContentPlaceHolder1$ddlConsumableItem" + i.ToString());
            t1 = (TextBox)Page.FindControl("ctl00$ContentPlaceHolder1$txtActualQty" + i.ToString());
            t2 = (TextBox)Page.FindControl("ctl00$ContentPlaceHolder1$txtBillQty" + i.ToString());
            d1.SelectedIndex = 0; d2.SelectedIndex = 0;
            t1.Text = ""; t2.Text = "";
        }
    }
    protected void GridView1_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        GridView1.PageIndex = e.NewPageIndex;
        GridFill();
    }
    protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "Select")
        {
            int index = Convert.ToInt32(e.CommandArgument);
            Label NameID = (Label)GridView1.Rows[index].FindControl("NameID");
            TextBox4.Value = NameID.Text;

            Label TemplateCategoryId = (Label)GridView1.Rows[index].FindControl("TemplateCategoryId");
            DropDownList1.SelectedValue = TemplateCategoryId.Text;

            Label ServiceTemplateName = (Label)GridView1.Rows[index].FindControl("ServiceTemplateName");
            TextBox1.Text = ServiceTemplateName.Text;

            Tab1Func();
            FillMap(NameID.Text);
            Button1.Text = "Update";
        }
    }
    protected void GridView1_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        Label NameID = (Label)GridView1.Rows[e.RowIndex].FindControl("NameID");
        if (theshift.NameFunction(3, NameID.Text, null, null, null, null) == true)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "msg", "alert('Deleted Successfully !');", true);
        }
        else
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "msg", "alert('Error in Deleted Data !');", true);
        }
        GridFill();
        ResetAllFields();
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


    public void ConsumableItemsFill(string value, DropDownList items)
    {
        items.Items.Clear();
        items.DataSource = theshift.ConsumableItems(value);
        items.DataTextField = "ConItemName";
        items.DataValueField = "ConItemID";
        items.DataBind();
        items.Items.Insert(0, new ListItem("--Select--", "0"));
    }
    protected void ddlconsumablegr1_SelectedIndexChanged(object sender, EventArgs e)
    {
        ConsumableItemsFill(ddlconsumablegr1.SelectedValue, ddlConsumableItem1);
    }
    protected void ddlconsumablegr2_SelectedIndexChanged(object sender, EventArgs e)
    {
        ConsumableItemsFill(ddlconsumablegr2.SelectedValue, ddlConsumableItem2);
    }
    protected void ddlconsumablegr3_SelectedIndexChanged(object sender, EventArgs e)
    {
        ConsumableItemsFill(ddlconsumablegr3.SelectedValue, ddlConsumableItem3);
    }
    protected void ddlconsumablegr4_SelectedIndexChanged(object sender, EventArgs e)
    {
        ConsumableItemsFill(ddlconsumablegr4.SelectedValue, ddlConsumableItem4);
    }
    protected void ddlconsumablegr5_SelectedIndexChanged(object sender, EventArgs e)
    {
        ConsumableItemsFill(ddlconsumablegr5.SelectedValue, ddlConsumableItem5);
    }
    protected void ddlconsumablegr6_SelectedIndexChanged(object sender, EventArgs e)
    {
        ConsumableItemsFill(ddlconsumablegr6.SelectedValue, ddlConsumableItem6);
    }
    protected void ddlconsumablegr7_SelectedIndexChanged(object sender, EventArgs e)
    {
        ConsumableItemsFill(ddlconsumablegr7.SelectedValue, ddlConsumableItem7);
    }
    protected void ddlconsumablegr8_SelectedIndexChanged(object sender, EventArgs e)
    {
        ConsumableItemsFill(ddlconsumablegr8.SelectedValue, ddlConsumableItem8);
    }
    protected void ddlconsumablegr9_SelectedIndexChanged(object sender, EventArgs e)
    {
        ConsumableItemsFill(ddlconsumablegr9.SelectedValue, ddlConsumableItem9);
    }
    protected void ddlconsumablegr10_SelectedIndexChanged(object sender, EventArgs e)
    {
        ConsumableItemsFill(ddlconsumablegr10.SelectedValue, ddlConsumableItem10);
    }
    protected void ddlconsumablegr11_SelectedIndexChanged(object sender, EventArgs e)
    {
        ConsumableItemsFill(ddlconsumablegr11.SelectedValue, ddlConsumableItem11);
    }
}