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

public partial class Master_PrescriptionTemplate2 : System.Web.UI.Page
{
    Main Objmain = new Main(ConfigurationSettings.AppSettings["ConnectionString"].ToString());
    static string gr, subgr;
    Prescriptemplateopd theHelper = new Prescriptemplateopd(ConfigurationSettings.AppSettings["ConnectionString"].ToString());
    protected void Page_Load(object sender, EventArgs e)
    {
        Page.Title = "Prescription Template2";
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
            //Button1.Enabled = false;
        }

        if (!IsPostBack)
        {
            //DropDownFill();
            // PageDataBind();
            BindGrid();
        }
    }
    protected void ButtonAdd_Click(object sender, EventArgs e)
    {

    }

    protected void BindGrid()
    {
        DataTable dt = null;
        dg.DataSource = dt;
        dg.DataBind();
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
    private void PageDataBind()
    {
        DropDownList d1, d2, d3, d4, d5, d6;
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

                d6 = (DropDownList)Page.FindControl("ctl00$ContentPlaceHolder1$ddlDuration" + (i + 1).ToString());

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

        //Button1.Text = "Update";
        //if (Objmain.checkaccss(Session["CoCode"].ToString(), Session["UserRoleID"].ToString().Trim(), "PRESCRIPTION TEMPLATE", checkAccessType.UpdateAction) == false)
        //{
        //    Button1.Enabled = false;
        //}
        //else
        //{
        //    Button1.Enabled = true;
        //}
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
    public void DropDownMedicineFill(string value, DropDownList drop3)
    {
        drop3.Items.Clear();
        drop3.DataSource = theHelper.DropdownMedicine(value, Session["CoCode"].ToString().Trim());
        drop3.DataTextField = "MedicineName";
        drop3.DataValueField = "MedicineID";
        drop3.DataBind();
        drop3.Items.Insert(0, new ListItem("--Select--", "0"));
    }

    //protected void Button1_Click(object sender, EventArgs e)
    //{

    //    DropDownList d1, d2, d3, d4, d5, d6;
    //    string id;

    //    if (HiddenField1.Value != "")
    //    {
    //        id = HiddenField1.Value;
    //    }
    //    else
    //    {
    //        id = "null";
    //    }
    //    if (Button1.Text == "Submit")
    //    {
    //        theHelper.Insert_Update_PrescriptionDetails(1, null, txtPrescrpTemName.Text.Trim(), DateTime.Now.ToString("yyyy-MM-dd"), Session["userId"].ToString(), DropDownList51.SelectedValue, Session["CoCode"].ToString());

    //        for (int d = 1, t = 31, d0 = 41, n = 1; t <= 40; t++, d0++, d = d + 3, n = n + 1)
    //        {
    //            d1 = (DropDownList)Page.FindControl("ctl00$ContentPlaceHolder1$DropDownList" + d.ToString());
    //            d2 = (DropDownList)Page.FindControl("ctl00$ContentPlaceHolder1$DropDownList" + (d + 1).ToString());
    //            d3 = (DropDownList)Page.FindControl("ctl00$ContentPlaceHolder1$DropDownList" + (d + 2).ToString());
    //            d4 = (DropDownList)Page.FindControl("ctl00$ContentPlaceHolder1$DropDownList" + t.ToString());
    //            d5 = (DropDownList)Page.FindControl("ctl00$ContentPlaceHolder1$DropDownList" + d0.ToString());

    //            d6 = (DropDownList)Page.FindControl("ctl00$ContentPlaceHolder1$ddlDuration" + n.ToString());

    //            if (d1.SelectedIndex != 0 && d2.SelectedIndex != 0 && d3.SelectedIndex != 0 && d4.SelectedIndex != 0)
    //            {
    //                theHelper.InsertPrescriptionMapping(d5.SelectedValue, "", txtPrescrpTemName.Text.Trim(), d1.SelectedValue, d2.SelectedValue, d3.SelectedValue, d4.SelectedValue, Session["userId"].ToString(), Session["CoCode"].ToString(), d6.SelectedValue.ToString());
    //            }
    //            else
    //                break;
    //        }
    //        ScriptManager.RegisterStartupScript(this, this.GetType(), "msg", "alert('Inserted Successfully !');", true);
    //    }
    //    else
    //    {
    //        theHelper.DeleteMap(HiddenField1.Value, Session["CoCode"].ToString().Trim());

    //        theHelper.Insert_Update_PrescriptionDetails(2, HiddenField1.Value, txtPrescrpTemName.Text.Trim(), DateTime.Now.ToString("MM/dd/yyyy"), Session["userId"].ToString(), DropDownList51.SelectedValue, Session["CoCode"].ToString());

    //        for (int d = 1, d11 = 41, t = 31, n = 1; t <= 40; d11++, t++, d = d + 3, n = n + 1)
    //        {
    //            d1 = (DropDownList)Page.FindControl("ctl00$ContentPlaceHolder1$DropDownList" + d.ToString());
    //            d2 = (DropDownList)Page.FindControl("ctl00$ContentPlaceHolder1$DropDownList" + (d + 1).ToString());
    //            d3 = (DropDownList)Page.FindControl("ctl00$ContentPlaceHolder1$DropDownList" + (d + 2).ToString());
    //            d4 = (DropDownList)Page.FindControl("ctl00$ContentPlaceHolder1$DropDownList" + t.ToString());
    //            d5 = (DropDownList)Page.FindControl("ctl00$ContentPlaceHolder1$DropDownList" + d11.ToString());

    //            d6 = (DropDownList)Page.FindControl("ctl00$ContentPlaceHolder1$ddlDuration" + n.ToString());

    //            if (d1.SelectedIndex != 0 && d2.SelectedIndex != 0)
    //            {
    //                theHelper.InsertPrescriptionMapping(d5.SelectedValue, HiddenField1.Value, txtPrescrpTemName.Text.Trim(), d1.SelectedValue, d2.SelectedValue, d3.SelectedValue, d4.SelectedValue, Session["userId"].ToString(), Session["CoCode"].ToString().Trim(), d6.SelectedValue.ToString());
    //            }
    //            else
    //                break;
    //        }
    //        ScriptManager.RegisterStartupScript(this, this.GetType(), "msg", "alert('Updated Successfully !');", true);

    //    }
    //    //Button1.Text = "Submit";
    //    txtPrescrpTemName.Text = "";
    //    DropDownList51.SelectedIndex = 0;
    //    //ResetAllFields();
    //}

   
}