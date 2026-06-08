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
using System.Web.Security;
 
public partial class IPD_PatientList : System.Web.UI.Page
{
    PatientList thereg = new PatientList(ConfigurationSettings.AppSettings["ConnectionString"].ToString());
    static string floor, roomtype;
    protected void Page_Load(object sender, EventArgs e)
    {
        Page.Title = "Patient Dashboard";
        if (Session["userName"] == null)
        {
            Response.Redirect("../LoginPage.aspx");
        }
  
        if (!IsPostBack)
        {
            DropDownFill(); GridFill();
        }
    }

    public void DropDownFill()
    {
        this.DropDownList1.DataSource = thereg.DropdownFloor(Session["CoCode"].ToString().Trim());
        this.DropDownList1.DataTextField = "FloorName";
        this.DropDownList1.DataValueField = "FloorID";
        this.DropDownList1.DataBind();
        this.DropDownList1.Items.Insert(0, new ListItem("--Select--", "0"));

        this.DropDownList4.Items.Insert(0, new ListItem("--Select--", "0"));

        this.DropDownList3.Items.Insert(0, new ListItem("--Select--", "0"));


        this.DropDownList2.DataSource = thereg.Dropdownroomtype(Session["CoCode"].ToString().Trim());
        this.DropDownList2.DataTextField = "RoomCategoryName";
        this.DropDownList2.DataValueField = "RoomCategoryID";
        this.DropDownList2.DataBind();
        this.DropDownList2.Items.Insert(0, new ListItem("--Select--", "0"));
    }
    public void GridFill()
    {
        GridView1.DataSource = thereg.GridPopup(DropDownList1.SelectedValue, DropDownList2.SelectedValue, DropDownList3.SelectedValue, DropDownList4.SelectedValue, txtname.Text.Trim(), Session["CoCode"].ToString().Trim());
        GridView1.DataBind();
    }
    protected void GridView1_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        GridView1.PageIndex = e.NewPageIndex;
        GridFill();
    }
    protected void Button2_Click(object sender, EventArgs e)
    {
        GridFill();
    }



  
    protected void DropDownList1_SelectedIndexChanged(object sender, EventArgs e)
    {
        floor = DropDownList1.SelectedValue;
    }
 
    protected void DropDownList2_SelectedIndexChanged(object sender, EventArgs e)
    {
          roomtype = DropDownList2.SelectedValue;
          this.DropDownList3.DataSource = thereg.Dropdownroom(floor, roomtype, Session["CoCode"].ToString().Trim());
        this.DropDownList3.DataTextField = "RoomName";
        this.DropDownList3.DataValueField = "RoomID";
        this.DropDownList3.DataBind();
        this.DropDownList3.Items.Insert(0, new ListItem("--Select--", "0"));
    }
    protected void DropDownList3_SelectedIndexChanged(object sender, EventArgs e)
    {
        this.DropDownList4.DataSource = thereg.Dropdownbed(floor, roomtype, DropDownList3.SelectedValue, Session["CoCode"].ToString().Trim());
        this.DropDownList4.DataTextField = "BedNoText";
        this.DropDownList4.DataValueField = "BedNo";
        this.DropDownList4.DataBind();
        this.DropDownList4.Items.Insert(0, new ListItem("--Select--", "0"));
    }
    protected void Button1_Click(object sender, EventArgs e)
    {
        GridFill();
    }


    protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
    {
         if (e.CommandName == "docvisit")
        {

            string id = (e.CommandArgument).ToString();
          
            Session.Add("RegNo", id);
            Response.Redirect("../IPD/PatientDoctorVisit.aspx");
        }

        if (e.CommandName == "addservices")
        {

            string id = (e.CommandArgument).ToString();
          
            Session.Add("RegNo", id);
            Response.Redirect("../IPD/AddServices.aspx");
        }

        if (e.CommandName == "Labrequisition")
        {

            string id = (e.CommandArgument).ToString();
            Session.Add("RegNo", id);
            Response.Redirect("../Pathology/PatientRequisition.aspx");
        }
        if (e.CommandName == "addConsumable")
        {

            string id = (e.CommandArgument).ToString();
            Session.Add("RegNo", id);
            Response.Redirect("../IPD/AddConsumable.aspx");
        }

        if (e.CommandName == "dailycheckup")
        {

            string id = (e.CommandArgument).ToString();
            Session.Add("RegNo", id);
            Response.Redirect("../IPD/DailyCheckupRecord.aspx");
        }

        if (e.CommandName == "addmedicine")
        {

            string id = (e.CommandArgument).ToString();
            Session.Add("RegNo", id);
            Response.Redirect("../IPD/AddMedicine.aspx");
        } 

               if (e.CommandName == "otreq")
        {

            string id = (e.CommandArgument).ToString();
            Session.Add("RegNo", id);
            Response.Redirect("../IPD/OTRequisition.aspx");
        }

        if (e.CommandName == "BedTransfer")
        {

            string id = (e.CommandArgument).ToString();
            Session.Add("RegNo", id);
            Response.Redirect("../IPD/BedTransfer.aspx");
        }


        if (e.CommandName == "sistecharge")
        {

            string id = (e.CommandArgument).ToString();
            Session.Add("RegNo", id);
            Response.Redirect("../IPD/Sister_Aya_Charges.aspx");
        }

        if(e.CommandName=="ToDoTask")
        {
            string id = (e.CommandArgument).ToString();
            Session.Add("RegNo", id);
            Response.Redirect("../IPD/ToDoList.aspx");
        }
            

    }
  
}