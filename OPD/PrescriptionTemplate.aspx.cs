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
using System.Collections.Generic;
 
public partial class Master_PrescriptionTemplate : System.Web.UI.Page
{
    Main Objmain = new Main(ConfigurationSettings.AppSettings["ConnectionString"].ToString());
    static string gr,subgr;
    Prescriptemplateopd theHelper = new Prescriptemplateopd(ConfigurationSettings.AppSettings["ConnectionString"].ToString());
    protected void Page_Load(object sender, EventArgs e)
    {
        Page.Title = "Prescription Template";
        if (Session["userId"] == null)
        {
            Response.Redirect("../LoginPage.aspx");
        }

        if (Objmain.checkaccss(Session["CoCode"].ToString(), Session["UserRoleID"].ToString().Trim(), "PRESCRIPTION TEMPLATE", checkAccessType.ViewAction) == false)
        {
            Response.Redirect("../AccessDenied.aspx");
        }

        if (Objmain.checkaccss(Session["CoCode"].ToString(), Session["UserRoleID"].ToString().Trim(), "PRESCRIPTION TEMPLATE", checkAccessType.InsertAction) == false)
        {
            Button1.Enabled = false;
        }

        if (!IsPostBack)
        {
            DropDownFill();
           // PageDataBind();

        }
    }

    [System.Web.Script.Services.ScriptMethod()]
    [System.Web.Services.WebMethod]

    public static List<string> SearchMedicine(string prefixText, int count)
    {
        using (SqlConnection conn = new SqlConnection())
        {
            conn.ConnectionString = ConfigurationSettings.AppSettings["ConnectionString"].ToString();
            using (SqlCommand cmd = new SqlCommand())
            {
                cmd.CommandText = "SELECT mm.MName+' ## '+mg.MedicineGroupName+' ## '+sg.SubGrName+ ' ## '+m.MedicineName Name FROM PH_ManufactureMaster mm,IPD_MedicineGroup mg,IPD_MedicineSubGroup sg,IPD_MedicineMaster m WHERE mg.MedicineGroupID=sg.GroupID AND sg.ID=m.SubGroupid AND m.MCode=mm.MCode and  m.MedicineName like @SearchText + '%'";
                cmd.Parameters.AddWithValue("@SearchText", prefixText);
                cmd.Connection = conn;
                conn.Open();
                List<string> customers = new List<string>();
                using (SqlDataReader sdr = cmd.ExecuteReader())
                {
                    while (sdr.Read())
                    {
                        customers.Add(sdr["Name"].ToString());
                    }
                }
                conn.Close();
                return customers;
            }
        }
    }
    [System.Web.Script.Services.ScriptMethod()]
    [System.Web.Services.WebMethod]

    public static List<string> SearchSubGroup(string prefixText, int count)
    {
        using (SqlConnection conn = new SqlConnection())
        {
            conn.ConnectionString = ConfigurationSettings.AppSettings["ConnectionString"].ToString();
            using (SqlCommand cmd = new SqlCommand())
            {
                cmd.CommandText = "SELECT mm.MName+' ## '+mg.MedicineGroupName+' ## '+sg.SubGrName+ ' ## '+m.MedicineName Name FROM PH_ManufactureMaster mm,IPD_MedicineGroup mg,IPD_MedicineSubGroup sg,IPD_MedicineMaster m WHERE mg.MedicineGroupID=sg.GroupID AND sg.ID=m.SubGroupid AND m.MCode=mm.MCode and  sg.SubGrName like @SearchText + '%'";
                cmd.Parameters.AddWithValue("@SearchText", prefixText);
                cmd.Connection = conn;
                conn.Open();
                List<string> customers = new List<string>();
                using (SqlDataReader sdr = cmd.ExecuteReader())
                {
                    while (sdr.Read())
                    {
                        customers.Add(sdr["Name"].ToString());
                    }
                }
                conn.Close();
                return customers;
            }
        }
    }

    private void PageDataBind()
    {
        DropDownList d1, d2, d3, d4, d5,d6;
        DataSet ds = theHelper.GetPrescriptionTemplateDetails(HiddenField1.Value, Session["CoCode"].ToString().Trim());
        if (ds.Tables[0].Rows.Count > 0)
        {
            for (int i = 0, d = 1, d0 = 41, t = 31; i < ds.Tables[0].Rows.Count; i++, t++, d0++, d = d + 3)
            {
                d1 = (DropDownList)Page.FindControl("ctl00$ContentPlaceHolder1$DropDownList" + d.ToString());
                d2 = (DropDownList)Page.FindControl("ctl00$ContentPlaceHolder1$DropDownList" + (d + 1).ToString());
                d3 = (DropDownList)Page.FindControl("ctl00$ContentPlaceHolder1$DropDownList" + (d + 2).ToString());
                d4 = (DropDownList)Page.FindControl("ctl00$ContentPlaceHolder1$DropDownList" + t.ToString());
                d5 = (DropDownList)Page.FindControl("ctl00$ContentPlaceHolder1$DropDownList" + d0.ToString());

                d6 = (DropDownList)Page.FindControl("ctl00$ContentPlaceHolder1$ddlDuration" + (i+1).ToString());

                d1.SelectedValue = ds.Tables[0].Rows[i]["MedicineGroupID"].ToString();
                DropDownSubGroupFill(ds.Tables[0].Rows[i]["MedicineGroupID"].ToString(), d5);

                d5.SelectedValue = ds.Tables[0].Rows[i]["SubGroupid"].ToString();
                DropDownMedicineFill(ds.Tables[0].Rows[i]["SubGroupid"].ToString(), d2);
                d2.SelectedValue = ds.Tables[0].Rows[i]["MedicineID"].ToString();

                d3.SelectedValue = ds.Tables[0].Rows[i]["RouteID"].ToString();
                d4.SelectedValue = ds.Tables[0].Rows[i]["DailyDose"].ToString();
                
                if (ds.Tables[0].Rows[i]["Duration"].ToString() != "")
                {
                    d6.SelectedValue = ds.Tables[0].Rows[i]["Duration"].ToString();
                }
            }
            DropDownList51.SelectedValue = ds.Tables[0].Rows[0]["PrescriptionGrId"].ToString();
        }
        else
            if (ds.Tables[1].Rows.Count > 0)
            {
                DropDownList51.SelectedValue = ds.Tables[1].Rows[0]["PrescriptionGrId"].ToString();
            }

        Button1.Text = "Update";
        if (Objmain.checkaccss(Session["CoCode"].ToString(), Session["UserRoleID"].ToString().Trim(), "PRESCRIPTION TEMPLATE", checkAccessType.UpdateAction) == false)
        {
            Button1.Enabled = false;
        }
        else
        {
            Button1.Enabled = true;
        }
    }

    private void ResetAllFields()
    {
       DropDownList d1;
         for (int d = 1; d <= 50; d++)
        {
            d1 = (DropDownList)Page.FindControl("ctl00$ContentPlaceHolder1$DropDownList" + d.ToString());
            try
            {
                d1.SelectedIndex = 0;
            }
            catch
            {
                d1.SelectedIndex = -1;
            }
        }

         if (Objmain.checkaccss(Session["CoCode"].ToString(), Session["UserRoleID"].ToString().Trim(), "PRESCRIPTION TEMPLATE", checkAccessType.InsertAction) == false)
         {
             Button1.Enabled = false;
         }

    }

    private void DropDownFill()
    {
         DropDownList d1, d2, d3,d4,d5;
        for (int d = 1,d0=31; d <= 29; d = d + 3,d0++)
        {
            d1 = (DropDownList)Page.FindControl("ctl00$ContentPlaceHolder1$DropDownList" + d.ToString());
            d1.DataSource = theHelper.DropdownID2(Session["CoCode"].ToString().Trim());
            d1.DataTextField = "MedicineGroupName";
            d1.DataValueField = "MedicineGroupID";
            d1.DataBind();
            d1.Items.Insert(0, new ListItem("--Select--", "0"));

            d2 = (DropDownList)Page.FindControl("ctl00$ContentPlaceHolder1$DropDownList" + (d + 1).ToString());
            d2.Items.Insert(0, new ListItem("--Select--", "0"));

            d3 = (DropDownList)Page.FindControl("ctl00$ContentPlaceHolder1$DropDownList" + (d + 2).ToString());
            d3.DataSource = theHelper.DropdownID4(Session["CoCode"].ToString().Trim());
            d3.DataTextField = "RouteName";
            d3.DataValueField = "RouteID";
            d3.DataBind();
            d3.Items.Insert(0, new ListItem("--Select--", "0"));


            d4 = (DropDownList)Page.FindControl("ctl00$ContentPlaceHolder1$DropDownList" + d0.ToString());
            d4.DataSource = theHelper.DropdownDose(Session["CoCode"].ToString().Trim());
            d4.DataTextField = "DoseName";
            d4.DataValueField = "ID";
            d4.DataBind();
            d4.Items.Insert(0, new ListItem("--Select--", "0"));

            for (int i = 1; i <= 10; i++)
            {
                d5 = (DropDownList)Page.FindControl("ctl00$ContentPlaceHolder1$ddlDuration" + i.ToString());
                d5.Items.Clear();
                d5.DataSource = theHelper.DropdownDuration(Session["CoCode"].ToString().Trim());
                d5.DataTextField = "DurationName";
                d5.DataValueField = "DurationId";
                d5.DataBind();
                d5.Items.Insert(0, new ListItem("--Select--", "0"));
            }
        }

        DropDownList51.DataSource = theHelper.DropdownTempalteGroup(Session["CoCode"].ToString().Trim());
        DropDownList51.DataTextField = "PrescriptionGroupName";
        DropDownList51.DataValueField = "RowId";
        DropDownList51.DataBind();
        DropDownList51.Items.Insert(0, new ListItem("--Select--", "0"));

    }

    protected void Button1_Click(object sender, EventArgs e)
    {
 
        DropDownList d1, d2, d3,d4,d5,d6;
        string id;
        
         if (HiddenField1.Value != "")
        {
            id = HiddenField1.Value;
        }
        else
        {
            id = "null";
        }
        if (Button1.Text == "Submit")
        {
            theHelper.Insert_Update_PrescriptionDetails(1, null, txtPrescrpTemName.Text.Trim(), DateTime.Now.ToString("yyyy-MM-dd"), Session["userId"].ToString(), DropDownList51.SelectedValue, Session["CoCode"].ToString());

            for (int d = 1, t = 31,d0=41,n=1; t <= 40; t++,d0++, d = d + 3,n=n+1)
            {
                d1 = (DropDownList)Page.FindControl("ctl00$ContentPlaceHolder1$DropDownList" + d.ToString());
                d2 = (DropDownList)Page.FindControl("ctl00$ContentPlaceHolder1$DropDownList" + (d + 1).ToString());
                d3 = (DropDownList)Page.FindControl("ctl00$ContentPlaceHolder1$DropDownList" + (d + 2).ToString());
                d4 = (DropDownList)Page.FindControl("ctl00$ContentPlaceHolder1$DropDownList" + t.ToString());
                d5 = (DropDownList)Page.FindControl("ctl00$ContentPlaceHolder1$DropDownList" + d0.ToString());

                d6 = (DropDownList)Page.FindControl("ctl00$ContentPlaceHolder1$ddlDuration" + n.ToString());

                if (d1.SelectedIndex != 0 && d2.SelectedIndex != 0 && d3.SelectedIndex != 0 && d4.SelectedIndex != 0)
                {
                    theHelper.InsertPrescriptionMapping(d5.SelectedValue, "", txtPrescrpTemName.Text.Trim(), d1.SelectedValue, d2.SelectedValue, d3.SelectedValue, d4.SelectedValue, Session["userId"].ToString(), Session["CoCode"].ToString(), d6.SelectedValue.ToString());
                }
                else
                    break;
            }
            ScriptManager.RegisterStartupScript(this, this.GetType(), "msg", "alert('Inserted Successfully !');", true);
        }
        else
        {
            theHelper.DeleteMap(HiddenField1.Value, Session["CoCode"].ToString().Trim());

            theHelper.Insert_Update_PrescriptionDetails(2, HiddenField1.Value, txtPrescrpTemName.Text.Trim(), DateTime.Now.ToString("MM/dd/yyyy"), Session["userId"].ToString(), DropDownList51.SelectedValue, Session["CoCode"].ToString());

            for (int d = 1,d11=41, t = 31,n=1; t <= 40;d11++, t++, d = d + 3,n=n+1)
            {
                d1 = (DropDownList)Page.FindControl("ctl00$ContentPlaceHolder1$DropDownList" + d.ToString());
                d2 = (DropDownList)Page.FindControl("ctl00$ContentPlaceHolder1$DropDownList" + (d + 1).ToString());
                d3 = (DropDownList)Page.FindControl("ctl00$ContentPlaceHolder1$DropDownList" + (d + 2).ToString());
                d4 = (DropDownList)Page.FindControl("ctl00$ContentPlaceHolder1$DropDownList" + t.ToString());
                d5 = (DropDownList)Page.FindControl("ctl00$ContentPlaceHolder1$DropDownList" + d11.ToString());

                d6 = (DropDownList)Page.FindControl("ctl00$ContentPlaceHolder1$ddlDuration" + n.ToString());

                if (d1.SelectedIndex != 0 && d2.SelectedIndex != 0)
                {
                    theHelper.InsertPrescriptionMapping(d5.SelectedValue, HiddenField1.Value, txtPrescrpTemName.Text.Trim(), d1.SelectedValue, d2.SelectedValue, d3.SelectedValue, d4.SelectedValue, Session["userId"].ToString(), Session["CoCode"].ToString().Trim(), d6.SelectedValue.ToString());
                }
                else
                    break;
            }
            ScriptManager.RegisterStartupScript(this, this.GetType(), "msg", "alert('Updated Successfully !');", true);

        }
         Button1.Text = "Submit";
        txtPrescrpTemName.Text = "";
        DropDownList51.SelectedIndex = 0;
        ResetAllFields();
    }

    protected void Button2_Click(object sender, EventArgs e)
    {
        ResetAllFields();
        Button1.Text = "Submit";
        txtPrescrpTemName.Text = "";
        DropDownList51.SelectedIndex = 0;
    }

    public void DropDownSubGroupFill(string value, DropDownList drop2)
    {
         drop2.Items.Clear();
         drop2.DataSource = theHelper.DropdownsUB(value, Session["CoCode"].ToString().Trim());
        drop2.DataTextField = "SubGrName";
        drop2.DataValueField = "ID";
        drop2.DataBind();
        drop2.Items.Insert(0, new ListItem("--Select--", "0"));
    }

    public void DropDownMedicineFill(string value,DropDownList drop3)
    {
        drop3.Items.Clear();
        drop3.DataSource = theHelper.DropdownMedicine(value, Session["CoCode"].ToString().Trim());
        drop3.DataTextField = "MedicineName";
        drop3.DataValueField = "MedicineID";
        drop3.DataBind();
        drop3.Items.Insert(0, new ListItem("--Select--", "0"));
    }

    protected void DropDownList1_SelectedIndexChanged(object sender, EventArgs e)
    { 
        //DropDownSubGroupFill(DropDownList1.SelectedValue, DropDownList41);
        DropDownMedicineFill(DropDownList1.SelectedValue, DropDownList2);
    }

    protected void DropDownList4_SelectedIndexChanged(object sender, EventArgs e)
    {
        // gr = DropDownList4.SelectedValue;
        DropDownSubGroupFill(DropDownList4.SelectedValue, DropDownList42);
    }

    protected void DropDownList7_SelectedIndexChanged(object sender, EventArgs e)
    {
      //  gr = DropDownList7.SelectedValue;
        DropDownSubGroupFill(DropDownList7.SelectedValue, DropDownList43); 
    }

    protected void DropDownList10_SelectedIndexChanged(object sender, EventArgs e)
    {
       // gr = DropDownList10.SelectedValue;
        DropDownSubGroupFill(DropDownList10.SelectedValue, DropDownList44); 
    }

    protected void DropDownList13_SelectedIndexChanged(object sender, EventArgs e)
    {
       // gr = DropDownList13.SelectedValue;
        DropDownSubGroupFill(DropDownList13.SelectedValue, DropDownList45); 
    }

    protected void DropDownList16_SelectedIndexChanged(object sender, EventArgs e)
    {
       // gr = DropDownList1.SelectedValue;
        DropDownSubGroupFill(DropDownList16.SelectedValue, DropDownList46); 
    }

    protected void DropDownList19_SelectedIndexChanged(object sender, EventArgs e)
    {
        DropDownSubGroupFill(DropDownList19.SelectedValue, DropDownList47); 
    }

    protected void DropDownList22_SelectedIndexChanged(object sender, EventArgs e)
    {
        DropDownSubGroupFill(DropDownList22.SelectedValue, DropDownList48); 
    }

    protected void DropDownList25_SelectedIndexChanged(object sender, EventArgs e)
    {
        DropDownSubGroupFill(DropDownList25.SelectedValue, DropDownList49); 
    }

    protected void DropDownList28_SelectedIndexChanged(object sender, EventArgs e)
    {
        DropDownSubGroupFill(DropDownList28.SelectedValue, DropDownList50); 
    }
    protected void Button4_Click(object sender, EventArgs e)
    {
        ResetAllFields();
        PageDataBind();
    }
    protected void DropDownList41_SelectedIndexChanged(object sender, EventArgs e)
    {
        DropDownMedicineFill(DropDownList41.SelectedValue, DropDownList2);
    }

    protected void DropDownList42_SelectedIndexChanged(object sender, EventArgs e)
    {
        DropDownMedicineFill(DropDownList42.SelectedValue, DropDownList5);
    }

    protected void DropDownList43_SelectedIndexChanged(object sender, EventArgs e)
    {
        DropDownMedicineFill(DropDownList43.SelectedValue, DropDownList8);
    }

    protected void DropDownList44_SelectedIndexChanged(object sender, EventArgs e)
    {
        DropDownMedicineFill(  DropDownList44.SelectedValue, DropDownList11);
    }

    protected void DropDownList45_SelectedIndexChanged(object sender, EventArgs e)
    {
        DropDownMedicineFill( DropDownList45.SelectedValue, DropDownList14);
    }

    protected void DropDownList46_SelectedIndexChanged(object sender, EventArgs e)
    {
        DropDownMedicineFill(DropDownList46.SelectedValue, DropDownList17);
    }
    protected void DropDownList47_SelectedIndexChanged(object sender, EventArgs e)
    {
        DropDownMedicineFill(DropDownList47.SelectedValue, DropDownList20);
    }
    protected void DropDownList48_SelectedIndexChanged(object sender, EventArgs e)
    {
        DropDownMedicineFill(DropDownList48.SelectedValue, DropDownList23);
    }
    protected void DropDownList49_SelectedIndexChanged(object sender, EventArgs e)
    {
        DropDownMedicineFill(DropDownList49.SelectedValue, DropDownList26);
    }
    protected void DropDownList50_SelectedIndexChanged(object sender, EventArgs e)
    {
        DropDownMedicineFill(DropDownList50.SelectedValue, DropDownList29);
    }

    [System.Web.Script.Services.ScriptMethod()]
    [System.Web.Services.WebMethod]
    public static List<string> SearchTemplate(string prefixText, int count)
    {
        using (SqlConnection conn = new SqlConnection())
        {
            conn.ConnectionString = ConfigurationSettings.AppSettings["ConnectionString"].ToString();
            using (SqlCommand cmd = new SqlCommand())
            {
                cmd.CommandText = "select distinct PrescrpTemName as Name from IPD_PrescriptionTmplate where PrescrpTemName like @SearchText +'%'";
                cmd.Parameters.AddWithValue("@SearchText", prefixText);
                cmd.Connection = conn;
                conn.Open();
                List<string> customers = new List<string>();
                using (SqlDataReader sdr = cmd.ExecuteReader())
                {
                    while (sdr.Read())
                    {
                        customers.Add(sdr["Name"].ToString());
                    }
                }
                conn.Close();
                return customers;
            }
        }
    }
}