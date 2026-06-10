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
using System.Web.Services;
using System.Globalization;
using System.Net.Mail;
using System.Net;
using System.IO;
using System.Text;
using System.Collections.Generic;


public partial class OPD_PatientEnrollment : System.Web.UI.Page
{
    static int flag = 0;
    Main Objmain = new Main(ConfigurationSettings.AppSettings["ConnectionString"].ToString());
    PatientAppointment thepatientAppo = new PatientAppointment(ConfigurationSettings.AppSettings["ConnectionString"].ToString());

    static string userId1 = "";

    protected void Page_Load(object sender, EventArgs e)
    {

        Page.Title = "Patient Registration";

        if (Session["userId"] == null)
        {
            Response.Redirect("../LoginPage.aspx");
        }
        if (Objmain.checkaccss(Session["CoCode"].ToString(), Session["UserRoleID"].ToString().Trim(), "PATIENT REGISTRATION", checkAccessType.ViewAction) == false)
        {
            Response.Redirect("../AccessDenied.aspx");
        }
        //if (Objmain.checkaccss(Session["CoCode"].ToString(), Session["UserRoleID"].ToString().Trim(), "PATIENT REGISTRATION", checkAccessType.InsertAction) == false)
        //{
        //    Button1.Enabled = false;
        //}
        //if (Objmain.checkaccss(Session["CoCode"].ToString(), Session["UserRoleID"].ToString().Trim(), "PATIENT REGISTRATION", checkAccessType.DeleteAction) == false)
        //{
        //    GridView1.Columns[19].Visible = false;
        //}

        if (!IsPostBack)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "msg", "GetDatetime();", true);
            DropDownFill();
            GenerateRegCode();
            GenerateAppoCode();
            //txtRegDate.Text = DateTime.Now.ToString("yyyy-MM-dd");
            GridFill();
            DropDownList1.SelectedValue = "2";
            //DropDownList9.SelectedValue = "5";
            Tab1Func();

        }
    }

    private void GenerateAppoCode()
    {
        /*DataTable dt = thepatientAppo.GenerateReqNo(Session["CoCode"].ToString().Trim());
        TextBox1.Text = dt.Rows[0][0].ToString();*/
    }

    private void GenerateRegCode()
    {
        DataTable dt = thepatientAppo.GenerateRegno(Session["CoCode"].ToString().Trim(), Session["YearCode"].ToString().Trim());
        TextBox2.Text = dt.Rows[0][0].ToString();
    }
    private void ResetAllFields()
    {


        for (int t = 1; t <= 15; t++)
        {
            TextBox t1 = (TextBox)Page.FindControl("ctl00$ContentPlaceHolder1$TextBox" + t.ToString());
            if (t == 6 || t == 8 || t == 11)
                continue;
            t1.Text = "";
        }
        TextBox27.Text = "";
        for (int d = 1; d <= 4; d++)
        {
            DropDownList d1 = (DropDownList)Page.FindControl("ctl00$ContentPlaceHolder1$DropDownList" + d.ToString());
            d1.SelectedIndex = 0;
        }

        Button1.Text = "Submit";
        GenerateAppoCode();
        GenerateRegCode();

        if (Objmain.checkaccss(Session["CoCode"].ToString(), Session["UserRoleID"].ToString().Trim(), "PATIENT REGISTRATION", checkAccessType.InsertAction) == false)
        {
            Button1.Enabled = false;
        }

    }
    private void DropDownFill()
    {
        this.DropDownList1.DataSource = thepatientAppo.DropdownSex();
        this.DropDownList1.DataTextField = "SexName";
        this.DropDownList1.DataValueField = "ID";
        this.DropDownList1.DataBind();
        this.DropDownList1.Items.Insert(0, new ListItem("--Select--", "0"));

        this.DropDownList2.DataSource = thepatientAppo.DropdownDocType(Session["CoCode"].ToString().Trim());
        this.DropDownList2.DataTextField = "TypeName";
        this.DropDownList2.DataValueField = "DocTypeId";
        this.DropDownList2.DataBind();
        this.DropDownList2.Items.Insert(0, new ListItem("--Select--", "0"));

        this.DropDownList3.Items.Insert(0, new ListItem("--Select--", "0"));

        this.DropDownList4.DataSource = thepatientAppo.DropPatientType(Session["CoCode"].ToString().Trim());
        this.DropDownList4.DataTextField = "TypeName";
        this.DropDownList4.DataValueField = "TypeId";
        this.DropDownList4.DataBind();
        this.DropDownList4.Items.Insert(0, new ListItem("--Select--", "0"));

        //DropDownList9.Items.Clear();
        //this.DropDownList9.DataSource = thepatientAppo.GetDistrict();
        //this.DropDownList9.DataTextField = "DistrictName";
        //this.DropDownList9.DataValueField = "ID";
        //this.DropDownList9.DataBind();
        //this.DropDownList9.Items.Insert(0, new ListItem("--Select--", "0"));

        ddlRefDoc.Items.Clear();
        ddlRefDoc.DataSource = thepatientAppo.DropdownDoc(Session["CoCode"].ToString().Trim(), "0");
        ddlRefDoc.DataTextField = "doc_name";
        ddlRefDoc.DataValueField = "doc_id";
        ddlRefDoc.DataBind();
        this.ddlRefDoc.Items.Insert(0, new ListItem("Self", "0"));


        ddlCountryPresent.Items.Clear();
        ddlCountryPresent.DataSource = thepatientAppo.getCountry(Session["CoCode"].ToString().Trim());
        ddlCountryPresent.DataTextField = "CountryName";
        ddlCountryPresent.DataValueField = "CountryId";
        ddlCountryPresent.DataBind();
        this.ddlCountryPresent.Items.Insert(0, new ListItem("--Select--", ""));

        ddlCountryPermanent.Items.Clear();
        ddlCountryPermanent.DataSource = thepatientAppo.getCountry(Session["CoCode"].ToString().Trim());
        ddlCountryPermanent.DataTextField = "CountryName";
        ddlCountryPermanent.DataValueField = "CountryId";
        ddlCountryPermanent.DataBind();
        this.ddlCountryPermanent.Items.Insert(0, new ListItem("--Select--", ""));


        ddlStatePresent.Items.Clear();
        ddlStatePresent.DataSource = thepatientAppo.getState(Session["CoCode"].ToString().Trim(), ddlCountryPresent.SelectedValue.Trim());
        ddlStatePresent.DataTextField = "State_Name";
        ddlStatePresent.DataValueField = "State_ID";
        ddlStatePresent.DataBind();
        this.ddlStatePresent.Items.Insert(0, new ListItem("--Select--", ""));

        ddlStatePermanent.Items.Clear();
        ddlStatePermanent.DataSource = thepatientAppo.getState(Session["CoCode"].ToString().Trim(), ddlCountryPermanent.SelectedValue.Trim());
        ddlStatePermanent.DataTextField = "State_Name";
        ddlStatePermanent.DataValueField = "State_ID";
        ddlStatePermanent.DataBind();
        this.ddlStatePermanent.Items.Insert(0, new ListItem("--Select--", ""));


        ddlDistrictPresent.Items.Clear();
        ddlDistrictPresent.DataSource = thepatientAppo.GetSateWiseDistrict(Session["CoCode"].ToString().Trim(), ddlStatePresent.SelectedValue.Trim());
        ddlDistrictPresent.DataTextField = "DistrictName";
        ddlDistrictPresent.DataValueField = "ID";
        ddlDistrictPresent.DataBind();
        this.ddlDistrictPresent.Items.Insert(0, new ListItem("--Select--", ""));


        ddlDistrictPermanent.Items.Clear();
        ddlDistrictPermanent.DataSource = thepatientAppo.GetSateWiseDistrict(Session["CoCode"].ToString().Trim(), ddlStatePermanent.SelectedValue.Trim());
        ddlDistrictPermanent.DataTextField = "DistrictName";
        ddlDistrictPermanent.DataValueField = "ID";
        ddlDistrictPermanent.DataBind();
        this.ddlDistrictPermanent.Items.Insert(0, new ListItem("--Select--", ""));
    }

    private void GridFill()
    {
        string regdate = txtRegDate.Text;
        GridView1.DataSource = thepatientAppo.RegGridFill(Session["CoCode"].ToString().Trim(), Session["YearCode"].ToString().Trim(), txtregSrch.Text, txtnameSrch.Text, txtphSrch.Text, txtRegDate.Text);
        GridView1.DataBind();
    }
    protected void Button2_Click(object sender, EventArgs e)
    {
        ResetAllFields();
    }

    protected void Button1_Click(object sender, EventArgs e)
    {
        string id;
        string[] Ext = null;
        string Extension = null;
        string FilePath = null;
        System.Globalization.DateTimeFormatInfo dtf = new DateTimeFormatInfo();
        //DateTime dob = DateTime.ParseExact(TextBox27.Text, "dd/MM/yyyy", dtf);

        if (TextBox2.Text == "")
        {
            lblError.Text = "Registration No cannot be blank!";
        }
        else if (TextBox3.Text == "")
        {
            lblError.Text = "Patient Name cannot be blank!";
        }
        else if (TextBox7.Text == "")
        {
            lblError.Text = "Contact no cannot be blank!";
        }
        else if (TextBox10.Text == "")
        {
            lblError.Text = "Address cannot be blank!";
        }
        else
        {
            string ph1 = TextBox7.Text;
            string ph2 = TextBox9.Text;
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
                string userID = "";
                string Encriptpassword = "";
                string password = "";
                string first = "";
                string second = "";


                string[] name = TextBox3.Text.ToLower().Split(' ');
                if (name[0].Length < 3)
                    first = name[0].Substring(0, name[0].Length);
                else
                    first = name[0].Substring(0, 3);
                if (name.Length > 1)
                {
                    if (name[1].Length < 3)
                        second = name[1].Substring(0, name[1].Length);
                    else
                        second = name[1].Substring(0, 3);
                }
                else
                {
                    second = "not";
                }

                string pattern = "{0}{1}{2}";
                int dt = DateTime.Now.Day;
                userID = CheckUserId(string.Format(pattern, first, second, dt));

                RandomStringGenerator.RandomStringGenerator re = new RandomStringGenerator.RandomStringGenerator();
                password = re.Generate(8);

                Encriptpassword = EncryptionDecryption.CryptorEngine.EncryptData(password);

                DataTable dtregno = thepatientAppo.GenerateRegno(Session["CoCode"].ToString().Trim(), Session["YearCode"].ToString().Trim());
                TextBox2.Text = dtregno.Rows[0][0].ToString();

                if (FileUpload1.FileName.Length > 0)
                {
                    string FolderName = string.Empty;
                    string DomainPath = "Images/PatientAadhaar/" + Session["CoCode"].ToString().Trim() + "/";
                    string PhysicalPath = Server.MapPath(ResolveUrl(DomainPath));


                    FolderName = System.IO.Path.GetDirectoryName(PhysicalPath);
                    if (System.IO.Directory.Exists(FolderName.Trim()) == false)
                    {
                        System.IO.Directory.CreateDirectory(FolderName.Trim());
                    }



                    //FilePath = PhysicalPath + FileUpload1.FileName.Trim
                    Ext = FileUpload1.FileName.Trim().Split('.');
                    Extension = Ext[Ext.Length - 1];

                    FilePath = PhysicalPath + TextBox2.Text.ToString().Trim() + Extension;

                    if (File.Exists(FilePath))
                    {
                        File.Delete(FilePath);
                    }
                    FileUpload1.PostedFile.SaveAs(FilePath);
                }

                Decimal advamt = (TextBox14.Text == "" ? 0 : Convert.ToDecimal(TextBox14.Text));

                DataTable dtreg = thepatientAppo.GenerateRegno(Session["CoCode"].ToString().Trim(), Session["YearCode"].ToString().Trim());
                TextBox2.Text = dtreg.Rows[0][0].ToString();
                if (thepatientAppo.InsertRegistration("I", Session["CoCode"].ToString().Trim(), Session["YearCode"].ToString().Trim(), TextBox2.Text, TextBox3.Text, TextBox4.Text, TextBox5.Text, DropDownList1.SelectedValue, ph1, ph2, TextBox10.Text, ddlDistrictPresent.SelectedValue.Trim(), TextBox15.Text, TextBox27.Text, Session["userId"].ToString(), userID, Encriptpassword, DateTime.Now.ToString("yyyy-MM-dd"), txtAadhaarNo.Text.Trim(), txtPanNo.Text.Trim(), FilePath, "", ddlRefDoc.SelectedValue.Trim(), txtSpouseName.Text, txtPresentPin.Text, ddlStatePresent.SelectedValue.Trim(), ddlCountryPresent.SelectedValue.Trim(), txtParmAddr.Text, txtParmanentPin.Text, ddlDistrictPermanent.SelectedValue.Trim(), ddlStatePermanent.SelectedValue.Trim(), ddlCountryPermanent.SelectedValue.Trim(), txtEmailId.Text.Trim()) == true)
                {
                    thepatientAppo.UpdateChargeDetails(Session["CoCode"].ToString().Trim(), Session["YearCode"].ToString().Trim(), TextBox2.Text, "R", DateTime.Now.ToString("yyyy-MM-dd"), TextBox14.Text.Trim());
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "msg", "alert('Inserted  Successfully !');", true);
                    Button1.Text = "UPDATE";
                    GridFill();
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "msg", "alert('Error in Inserted Data !');", true);
                }

            }
            else
            {
                if (FileUpload1.FileName.Length > 0)
                {
                    string FolderName = string.Empty;
                    string DomainPath = "Images/PatientAadhaar/" + Session["CoCode"].ToString().Trim() + "/";
                    string PhysicalPath = Server.MapPath(ResolveUrl(DomainPath));


                    FolderName = System.IO.Path.GetDirectoryName(PhysicalPath);
                    if (System.IO.Directory.Exists(FolderName.Trim()) == false)
                    {
                        System.IO.Directory.CreateDirectory(FolderName.Trim());
                    }



                    //FilePath = PhysicalPath + FileUpload1.FileName.Trim
                    Ext = FileUpload1.FileName.Trim().Split('.');
                    Extension = Ext[Ext.Length - 1];

                    FilePath = PhysicalPath + TextBox2.Text.ToString().Trim() + Extension;

                    if (File.Exists(FilePath))
                    {
                        File.Delete(FilePath);
                    }
                    FileUpload1.PostedFile.SaveAs(FilePath);
                }
                Decimal advamt = (TextBox14.Text == "" ? 0 : Convert.ToDecimal(TextBox14.Text));

                if (thepatientAppo.InsertRegistration("U", Session["CoCode"].ToString().Trim(), Session["YearCode"].ToString().Trim(), TextBox2.Text, TextBox3.Text, TextBox4.Text, TextBox5.Text, DropDownList1.SelectedValue, ph1, ph2, TextBox10.Text, ddlDistrictPresent.SelectedValue.Trim(), TextBox15.Text, TextBox27.Text, Session["userId"].ToString(), "", "", DateTime.Now.ToString("yyyy-MM-dd"), txtAadhaarNo.Text.Trim(), txtPanNo.Text.Trim(), FilePath, "", ddlRefDoc.SelectedValue.Trim(), txtSpouseName.Text, txtPresentPin.Text, ddlStatePresent.SelectedValue.Trim(), ddlCountryPresent.SelectedValue.Trim(), txtParmAddr.Text, txtParmanentPin.Text, ddlDistrictPermanent.SelectedValue.Trim(), ddlStatePermanent.SelectedValue.Trim(), ddlCountryPermanent.SelectedValue.Trim(), txtEmailId.Text.Trim()) == true)
                {
                    thepatientAppo.UpdateChargeDetails(Session["CoCode"].ToString().Trim(), Session["YearCode"].ToString().Trim(), TextBox2.Text, "R", DateTime.Now.ToString("yyyy-MM-dd"), TextBox14.Text.Trim());
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "msg", "alert('Updated  Successfully !');", true);
                    Button1.Text = "SUBMIT";
                    GridView1.SelectedRowStyle.BackColor = System.Drawing.Color.Empty;
                    GridFill();
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "msg", "alert('Error in Updated Data !');", true);
                }
            }
        }
    }

    //protected void Button1_Click(object sender, EventArgs e)
    //{
    //    string id;
    //    string[] Ext = null;
    //    string Extension = null;
    //    string FilePath = null;
    //    System.Globalization.DateTimeFormatInfo dtf = new DateTimeFormatInfo();
    //    DateTime testdate = DateTime.ParseExact(TextBox12.Text, "dd/MM/yyyy", dtf);
    //    DateTime dob = DateTime.ParseExact(TextBox27.Text, "dd/MM/yyyy", dtf);
    //    string chqdt = "";
    //    if (TextBox2.Text == "")
    //    {
    //        lblError.Text = "Registration No cannot be blank!";
    //    }
    //    else if (TextBox3.Text == "")
    //    {
    //        lblError.Text = "Patient Name cannot be blank!";
    //    }
    //    //else if (TextBox27.Text == "")
    //    //{
    //    //    lblError.Text = "Date of Birth cannot be blank!";
    //    //}
    //    //else if (DropDownList3.SelectedValue == "" || DropDownList3.SelectedValue == "0")
    //    //{
    //    //    lblError.Text = "Select Doctor!";
    //    //}
    //    //else if (TextBox12.Text=="")
    //    //{
    //    //    lblError.Text = "Appointment Date cannot be blank!";
    //    //}
    //    //else if (TextBox13.Text == "")
    //    //{
    //    //    lblError.Text = "Appointment Time cannot be blank!";
    //    //}
    //    //else if (txtAadhaarNo.Text == "")
    //    //{
    //    //    lblError.Text = "Aadhaar No cannot be blank!";
    //    //}
    //    else if (TextBox7.Text == "")
    //    {
    //        lblError.Text = "Contact no cannot be blank!";
    //    }
    //    else
    //    {
    //        if (txtchqdt.Text != "")
    //        {
    //            string[] cdt = txtchqdt.Text.Split('/');
    //            chqdt = cdt[2] + "-" + cdt[1] + "-" + cdt[0];
    //        }

    //        DataTable OffDay = thepatientAppo.CheckDayoff(Session["CoCode"].ToString().Trim(), DropDownList3.SelectedValue, testdate.ToString("yyyy-MM-dd"));
    //        if (OffDay.Rows.Count > 0)
    //        {
    //            lblError.ForeColor = System.Drawing.Color.Red;
    //            lblError.Text = "Appointment Can not possible due to " + OffDay.Rows[0]["DayoffReason"].ToString();
    //        }
    //        else
    //        {
    //            string ph1 = TextBox6.Text + " " + TextBox7.Text;
    //            string ph2 = TextBox8.Text + " " + TextBox9.Text;
    //            if (HiddenField1.Value != "")
    //            {
    //                id = HiddenField1.Value;
    //            }
    //            else
    //            {
    //                id = "null";
    //            }
    //            if (Button1.Text == "Submit")
    //            {
    //                string userID = "";
    //                string Encriptpassword = "";
    //                string password = "";
    //                string first = "";
    //                string second = "";


    //                string[] name = TextBox3.Text.ToLower().Split(' ');
    //                if (name[0].Length < 3)
    //                    first = name[0].Substring(0, name[0].Length);
    //                else
    //                    first = name[0].Substring(0, 3);
    //                if (name.Length > 1)
    //                {
    //                    if (name[1].Length < 3)
    //                        second = name[1].Substring(0, name[1].Length);
    //                    else
    //                        second = name[1].Substring(0, 3);
    //                }
    //                else
    //                {
    //                    second = "not";
    //                }
    //                string pattern = "{0}{1}{2}";
    //                int dt = DateTime.Now.Day;
    //                userID = CheckUserId(string.Format(pattern, first, second, dt));

    //                RandomStringGenerator.RandomStringGenerator re = new RandomStringGenerator.RandomStringGenerator();
    //                password = re.Generate(8);

    //                Encriptpassword = EncryptionDecryption.CryptorEngine.EncryptData(password);

    //                if (FileUpload1.FileName.Length > 0)
    //                {
    //                    string FolderName = string.Empty;
    //                    string DomainPath = "Images/PatientAadhaar/" + Session["CoCode"].ToString().Trim() + "/";
    //                    string PhysicalPath = Server.MapPath(ResolveUrl(DomainPath));


    //                    FolderName = System.IO.Path.GetDirectoryName(PhysicalPath);
    //                    if (System.IO.Directory.Exists(FolderName.Trim()) == false)
    //                    {
    //                        System.IO.Directory.CreateDirectory(FolderName.Trim());
    //                    }



    //                    //FilePath = PhysicalPath + FileUpload1.FileName.Trim
    //                    Ext = FileUpload1.FileName.Trim().Split('.');
    //                    Extension = Ext[Ext.Length - 1];

    //                    FilePath = PhysicalPath + TextBox2.Text.ToString().Trim() + Extension;

    //                    if (File.Exists(FilePath))
    //                    {
    //                        File.Delete(FilePath);
    //                    }
    //                    FileUpload1.PostedFile.SaveAs(FilePath);
    //                }

    //                Decimal advamt = (TextBox14.Text == "" ? 0 : Convert.ToDecimal(TextBox14.Text));
                       
    //                if (thepatientAppo.InsertRegistration("I", Session["CoCode"].ToString().Trim(), Session["YearCode"].ToString().Trim(), TextBox1.Text, TextBox2.Text, TextBox3.Text, TextBox4.Text, TextBox5.Text, DropDownList1.SelectedValue, ph1, ph2, TextBox10.Text, DropDownList9.SelectedValue, DropDownList2.SelectedValue, DropDownList3.SelectedValue, testdate.ToString("yyyy-MM-dd"), TextBox13.Text, DropDownList4.SelectedValue, advamt, TextBox15.Text, "1", dob.ToString("yyyy-MM-dd"), Session["userName"].ToString(), userID, Encriptpassword, DateTime.Now.ToString("yyyy-MM-dd"), txtAadhaarNo.Text.Trim(), txtPanNo.Text.Trim(), FilePath, ddlPaymentMode.SelectedValue.Trim(), txtBankName.Text.Trim(), txtChequeNo.Text.Trim(), chqdt, "", txt_refred.Text.Trim()) == true)
    //                {
    //                    thepatientAppo.UpdateChargeDetails(Session["CoCode"].ToString().Trim(), Session["YearCode"].ToString().Trim(), TextBox2.Text, "R", testdate.ToString("yyyy-MM-dd"), TextBox14.Text.Trim());
    //                    ScriptManager.RegisterStartupScript(this, this.GetType(), "msg", "alert('Inserted  Successfully !');", true);
    //                }
    //                else
    //                {
    //                    ScriptManager.RegisterStartupScript(this, this.GetType(), "msg", "alert('Error in Inserted Data !');", true);
    //                }



    //            }
    //            else
    //            {

    //                if (FileUpload1.FileName.Length > 0)
    //                {
    //                    string FolderName = string.Empty;
    //                    string DomainPath = "Images/PatientAadhaar/" + Session["CoCode"].ToString().Trim() + "/";
    //                    string PhysicalPath = Server.MapPath(ResolveUrl(DomainPath));


    //                    FolderName = System.IO.Path.GetDirectoryName(PhysicalPath);
    //                    if (System.IO.Directory.Exists(FolderName.Trim()) == false)
    //                    {
    //                        System.IO.Directory.CreateDirectory(FolderName.Trim());
    //                    }



    //                    //FilePath = PhysicalPath + FileUpload1.FileName.Trim
    //                    Ext = FileUpload1.FileName.Trim().Split('.');
    //                    Extension = Ext[Ext.Length - 1];

    //                    FilePath = PhysicalPath + TextBox2.Text.ToString().Trim() + Extension;

    //                    if (File.Exists(FilePath))
    //                    {
    //                        File.Delete(FilePath);
    //                    }
    //                    FileUpload1.PostedFile.SaveAs(FilePath);
    //                }
    //                Decimal advamt = (TextBox14.Text == "" ? 0 : Convert.ToDecimal(TextBox14.Text));
    //                if (thepatientAppo.InsertRegistration("U", Session["CoCode"].ToString().Trim(), Session["YearCode"].ToString().Trim(), TextBox1.Text, TextBox2.Text, TextBox3.Text, TextBox4.Text, TextBox5.Text, DropDownList1.SelectedValue, ph1, ph2, TextBox10.Text, DropDownList9.SelectedValue, DropDownList2.SelectedValue, DropDownList3.SelectedValue, testdate.ToString("yyyy-MM-dd"), TextBox13.Text, DropDownList4.SelectedValue, advamt, TextBox15.Text, "1", dob.ToString("yyyy-MM-dd"), Session["userName"].ToString(), " ", " ", DateTime.Now.ToString("yyyy-MM-dd"), txtAadhaarNo.Text.Trim(), txtPanNo.Text.Trim(), FilePath, ddlPaymentMode.SelectedValue.Trim(), txtBankName.Text.Trim(), txtChequeNo.Text.Trim(), chqdt, hdnvchno.Value, txt_refred.Text.Trim()) == true)
    //                {
    //                    thepatientAppo.UpdateChargeDetails(Session["CoCode"].ToString().Trim(), Session["YearCode"].ToString().Trim(), TextBox2.Text, "R", testdate.ToString("yyyy-MM-dd"), TextBox14.Text.Trim());
    //                    ScriptManager.RegisterStartupScript(this, this.GetType(), "msg", "alert('Updated  Successfully !');", true);
    //                    Button1.Text = "Submit";
    //                    GridView1.SelectedRowStyle.BackColor = System.Drawing.Color.Empty;
    //                }
    //                else
    //                {
    //                    ScriptManager.RegisterStartupScript(this, this.GetType(), "msg", "alert('Error in Updated Data !');", true);
    //                }

    //            }
    //        }
    //    }
    //    GridFill();
    //    ResetAllFields();
    //}

    public string CheckUserId(string userID)
    {
        int keep;

        DataTable checkuserid = thepatientAppo.CheckUserId(Session["CoCode"].ToString().Trim(), userID);
        if (checkuserid.Rows.Count > 0)
        {
            keep = Convert.ToInt32(userID.Substring(6, 2)) + 1;
            userId1 = userID.Substring(0, 6) + keep.ToString();
            CheckUserId(userId1);
        }
        else
        {
            userId1 = userID;
        }


        return (userId1);

    }

    protected void GridView1_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        GridView1.PageIndex = e.NewPageIndex;
        GridFill();
    }

    protected int SearchIndex(string Value, DropDownList ddl)
    {
        int i;
        for (i = 0; i < ddl.Items.Count; i++)
        {
            if (ddl.Items[i].Text.Trim() == Value.Trim())
                return i;
        }
        return -1;
    }
    protected int SearchIndexbyid(string Value, DropDownList ddl)
    {
        int i;
        for (i = 0; i < ddl.Items.Count; i++)
        {
            if (ddl.Items[i].Value.Trim() == Value.Trim())
                return i;
        }
        return -1;
    }

    protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "Select")
        {
            lblError.Text = "";

            int index = Convert.ToInt32(e.CommandArgument);

            Label lblID = (Label)GridView1.Rows[index].FindControl("lblID");
            HiddenField1.Value = lblID.Text;
            TextBox1.Text = lblID.Text;

            Label lblregno = (Label)GridView1.Rows[index].FindControl("lblregno");
            TextBox2.Text = lblregno.Text;

            Label lblvchno = (Label)GridView1.Rows[index].FindControl("lblvchno");
            hdnvchno.Value = lblvchno.Text;

            Label lblname = (Label)GridView1.Rows[index].FindControl("lblname");
            TextBox3.Text = lblname.Text;

            Label lblguadian = (Label)GridView1.Rows[index].FindControl("lblguadian");
            TextBox4.Text = lblguadian.Text;

            Label lblage = (Label)GridView1.Rows[index].FindControl("lblage");
            TextBox5.Text = lblage.Text;

            Label lblsex = (Label)GridView1.Rows[index].FindControl("lblsex");
            DropDownList1.SelectedIndex = SearchIndex(lblsex.Text, DropDownList1);

            Label lblphone1 = (Label)GridView1.Rows[index].FindControl("lblphone1");
            TextBox7.Text = lblphone1.Text;
            

            Label lblphone2 = (Label)GridView1.Rows[index].FindControl("lblphone2");
            TextBox9.Text = lblphone2.Text;
            

            Label lbladdress = (Label)GridView1.Rows[index].FindControl("lbladdress");
            TextBox10.Text = lbladdress.Text;

            

            //Label lbldoctype = (Label)GridView1.Rows[index].FindControl("lbldoctype");
            //DropDownList2.SelectedIndex = SearchIndex(lbldoctype.Text, DropDownList2);

            //Label lbldocname = (Label)GridView1.Rows[index].FindControl("lbldocname");
            //DropDown3Fill("0");
            //DropDownList3.SelectedIndex = SearchIndex(lbldocname.Text, DropDownList3);

            Label lblappodate = (Label)GridView1.Rows[index].FindControl("lblappodate");
            TextBox12.Text = lblappodate.Text;
            //Label lblappotime = (Label)GridView1.Rows[index].FindControl("lblappotime");
            //TextBox13.Text = lblappotime.Text;
            Label lbldob = (Label)GridView1.Rows[index].FindControl("lbldob");
            TextBox27.Text = lbldob.Text;

            //Label lblptype = (Label)GridView1.Rows[index].FindControl("lblptype");
            //DropDownList4.SelectedIndex = SearchIndex(lblptype.Text, DropDownList4);

            //Label lbladvamt = (Label)GridView1.Rows[index].FindControl("lbladvamt");
            //TextBox14.Text = lbladvamt.Text;
            Label lblremarks = (Label)GridView1.Rows[index].FindControl("lblremarks");
            TextBox15.Text = lblremarks.Text;

            Label lblAadhaar = (Label)GridView1.Rows[index].FindControl("lblAadhaar");
            txtAadhaarNo.Text = lblAadhaar.Text;

            Label lblPan = (Label)GridView1.Rows[index].FindControl("lblPan");
            txtPanNo.Text = lblPan.Text;

            Label lblEmailId = (Label)GridView1.Rows[index].FindControl("lblEmailId");
            txtEmailId.Text = lblEmailId.Text;


            Label lblPresentPin = (Label)GridView1.Rows[index].FindControl("lblPresentPin");
            txtPresentPin.Text = lblPresentPin.Text;

            Label lblPresentCountry = (Label)GridView1.Rows[index].FindControl("lblPresentCountry");
            //txtPresentCountry.Text = lblPresentCountry.Text;
            ddlCountryPresent.Items.Clear();
            ddlCountryPresent.DataSource = thepatientAppo.getCountry(Session["CoCode"].ToString().Trim());
            ddlCountryPresent.DataTextField = "CountryName";
            ddlCountryPresent.DataValueField = "CountryId";
            ddlCountryPresent.DataBind();
            this.ddlCountryPresent.Items.Insert(0, new ListItem("--Select--", ""));
            ddlCountryPresent.SelectedValue = lblPresentCountry.Text.Trim();


            Label lblPresentState = (Label)GridView1.Rows[index].FindControl("lblPresentState");
            ddlStatePresent.Items.Clear();
            ddlStatePresent.DataSource = thepatientAppo.getState(Session["CoCode"].ToString().Trim(), ddlCountryPresent.SelectedValue.Trim());
            ddlStatePresent.DataTextField = "State_Name";
            ddlStatePresent.DataValueField = "State_ID";
            ddlStatePresent.DataBind();
            this.ddlStatePresent.Items.Insert(0, new ListItem("--Select--", ""));
            //txtPresentState.Text = lblPresentState.Text;
            ddlStatePresent.SelectedValue = lblPresentState.Text.Trim();
            


            Label lbldist = (Label)GridView1.Rows[index].FindControl("lbldist");
            //txtPresentDist.Text = lbldist.Text;
            ddlDistrictPresent.Items.Clear();
            ddlDistrictPresent.DataSource = thepatientAppo.GetSateWiseDistrict(Session["CoCode"].ToString().Trim(), ddlStatePresent.SelectedValue.Trim());
            ddlDistrictPresent.DataTextField = "DistrictName";
            ddlDistrictPresent.DataValueField = "ID";
            ddlDistrictPresent.DataBind();
            this.ddlDistrictPresent.Items.Insert(0, new ListItem("--Select--", ""));
            ddlDistrictPresent.SelectedValue = lbldist.Text.Trim();
            


            Label lblParmAddr = (Label)GridView1.Rows[index].FindControl("lblParmAddr");
            txtParmAddr.Text = lblParmAddr.Text;
            Label lblParmanentPin = (Label)GridView1.Rows[index].FindControl("lblParmanentPin");
            txtParmanentPin.Text = lblParmanentPin.Text;


            ddlCountryPermanent.DataSource = thepatientAppo.getCountry(Session["CoCode"].ToString().Trim());
            ddlCountryPermanent.DataTextField = "CountryName";
            ddlCountryPermanent.DataValueField = "CountryId";
            ddlCountryPermanent.DataBind();
            this.ddlCountryPermanent.Items.Insert(0, new ListItem("--Select--", ""));

            Label lblparmCountry = (Label)GridView1.Rows[index].FindControl("lblparmCountry");
            //txtParmCountry.Text = lblparmCountry.Text;
            ddlCountryPermanent.SelectedValue = lblparmCountry.Text.Trim();

            Label lblParmState = (Label)GridView1.Rows[index].FindControl("lblParmState");
            //txtParmState.Text = lblParmState.Text;
            ddlStatePermanent.Items.Clear();
            ddlStatePermanent.DataSource = thepatientAppo.getState(Session["CoCode"].ToString().Trim(), ddlCountryPermanent.SelectedValue.Trim());
            ddlStatePermanent.DataTextField = "State_Name";
            ddlStatePermanent.DataValueField = "State_ID";
            ddlStatePermanent.DataBind();
            this.ddlStatePermanent.Items.Insert(0, new ListItem("--Select--", ""));
            ddlStatePermanent.SelectedValue = lblParmState.Text.Trim();
            


            Label lblParmDist = (Label)GridView1.Rows[index].FindControl("lblParmDist");
            //txtParmDist.Text = lblParmDist.Text;
            ddlDistrictPermanent.Items.Clear();
            ddlDistrictPermanent.DataSource = thepatientAppo.GetSateWiseDistrict(Session["CoCode"].ToString().Trim(), ddlStatePermanent.SelectedValue.Trim());
            ddlDistrictPermanent.DataTextField = "DistrictName";
            ddlDistrictPermanent.DataValueField = "ID";
            ddlDistrictPermanent.DataBind();
            this.ddlDistrictPermanent.Items.Insert(0, new ListItem("--Select--", ""));
            ddlDistrictPermanent.SelectedValue = lblParmDist.Text.Trim();
            


            //Label lblPaymode = (Label)GridView1.Rows[index].FindControl("lblPaymode");
            //ddlPaymentMode.SelectedValue = lblPaymode.Text;

            //Label lblChqNo = (Label)GridView1.Rows[index].FindControl("lblChqNo");
            //txtChequeNo.Text = lblChqNo.Text;

            //Label lblChqDt = (Label)GridView1.Rows[index].FindControl("lblChqDt");
            //txtchqdt.Text = lblChqDt.Text;

            //Label lblBankNm = (Label)GridView1.Rows[index].FindControl("lblBankNm");
            //txtBankName.Text = lblBankNm.Text;

            Label lblRefDoc = (Label)GridView1.Rows[index].FindControl("lblRefDoc");
            ddlRefDoc.SelectedValue = lblRefDoc.Text.Trim();
            //if (ddlPaymentMode.SelectedValue == "B")
            //{
            //    lblchqdt.InnerText = "Cheque Date :";
            //    lblchqno.InnerText = "Cheque No :";
            //    lblbankNm.InnerText = "Bank Name :";
            //    divchqdt.Visible = true;
            //    divchqno.Visible = true;
            //    divBank.Visible = true;
            //}
            //else if (ddlPaymentMode.SelectedValue == "R")
            //{
            //    lblchqdt.InnerText = "Expire Date :";
            //    lblchqno.InnerText = "Card No :";
            //    lblbankNm.InnerText = "Card Holder Name :";
            //    divchqdt.Visible = true;
            //    divchqno.Visible = true;
            //    divBank.Visible = true;
            //}
            //else
            //{
            //    divchqdt.Visible = false;
            //    divchqno.Visible = false;
            //    divBank.Visible = false;
            //}
            
            //ddlPaymentMode.Enabled = false;
            Tab1Func();
            Button1.Text = "Update";
            //if (Objmain.checkaccss(Session["CoCode"].ToString(), Session["UserRoleID"].ToString().Trim(), "PATIENT ENROLLMENT", checkAccessType.UpdateAction) == false)
            //{
            //    Button1.Enabled = false;
            //}
            //else
            //{
            //    Button1.Enabled = true;
            //}
        }

        if (e.CommandName == "OPD")
        {
            string id = (e.CommandArgument).ToString();
            Session.Add("RegNo", id);
            Response.Redirect("../Pathology/PatientRequisitionOPD.aspx");
        }
        if (e.CommandName == "Diagnostic")
        {
            string id = (e.CommandArgument).ToString();
            Session.Add("RegNo", id);
            Session.Add("BillType", "DIG");
            Response.Redirect("../Pathology/PatientRequisition.aspx");
        }
        if (e.CommandName == "Procedure")
        {
            string id = (e.CommandArgument).ToString();
            Session.Add("RegNo", id);
            Session.Add("BillType", "PRC");
            Response.Redirect("../Pathology/PatientRequisition.aspx");
        }
        if (e.CommandName == "Infertility")
        {
            string id = (e.CommandArgument).ToString();
            Session.Add("RegNo", id);
            Session.Add("BillType", "INF");
            Response.Redirect("../Pathology/PatientRequisition.aspx");
        }
    }
    protected void GridView1_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        Label lblID = (Label)GridView1.Rows[e.RowIndex].FindControl("lblID");
        thepatientAppo.DeleteAppointment(Session["CoCode"].ToString().Trim(), Session["YearCode"].ToString().Trim(), lblID.Text);
        ScriptManager.RegisterStartupScript(this, this.GetType(), "msg", "alert('Deleted  Successfully !');", true);
        GridFill();
        ResetAllFields();
    }

    public void DropDown3Fill(string val)
    {
        this.DropDownList3.DataSource = thepatientAppo.DropdownDoc(Session["CoCode"].ToString().Trim(), val);
        this.DropDownList3.DataTextField = "doc_name";
        this.DropDownList3.DataValueField = "doc_id";
        this.DropDownList3.DataBind();
        this.DropDownList3.Items.Insert(0, new ListItem("--Select--", "0"));
    }
    protected void DropDownList2_SelectedIndexChanged(object sender, EventArgs e)
    {
        DropDown3Fill(DropDownList2.SelectedValue);
    }
    protected void Button4_Click(object sender, EventArgs e)
    {
        lblError.Text = "";
        DataTable dt = thepatientAppo.GetOpdRegPatientDetails(Session["CoCode"].ToString().Trim(), TextBox1.Text, TextBox2.Text);
        if (TextBox2.Text == "") { GenerateRegCode(); }
        TextBox3.Text = dt.Rows[0]["PName"].ToString();
        TextBox4.Text = dt.Rows[0]["GuadianName"].ToString();
        TextBox5.Text = dt.Rows[0]["age"].ToString();
        DropDownList1.SelectedValue = dt.Rows[0]["sex"].ToString();
        string[] ph1 = dt.Rows[0]["PhNo1"].ToString().Split(' ');
        string[] ph2 = dt.Rows[0]["PhNo2"].ToString().Split(' ');
        TextBox7.Text = ph1[1];
        TextBox9.Text = ph2[1];
        TextBox10.Text = dt.Rows[0]["Address"].ToString();
        ddlDistrictPresent.SelectedValue = dt.Rows[0]["District"].ToString().Trim();
        DropDownList2.SelectedValue = dt.Rows[0]["DocTypeID"].ToString().Trim();
        DropDown3Fill(DropDownList2.SelectedValue.Trim());
        DropDownList3.SelectedValue = dt.Rows[0]["DocId"].ToString().Trim();
        TextBox12.Text = dt.Rows[0]["AppoDate"].ToString().Trim();
        TextBox13.Text = dt.Rows[0]["AppointmentTime"].ToString().Trim();
        DropDownList4.SelectedValue = dt.Rows[0]["OPDPatientType"].ToString().Trim();
        TextBox14.Text = dt.Rows[0]["AdvancedAmount"].ToString().Trim();
    }
    protected void GridView1_SelectedIndexChanged(object sender, EventArgs e)
    {

    }

    public void Tab1Func()
    {
        Tab1.CssClass = "Clicked";
        Tab2.CssClass = "Initial";
        MainView.ActiveViewIndex = 0;
        divchqdt.Visible = false;
        divchqno.Visible = false;
        divBank.Visible = false;
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
    protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        //if (e.Row.RowType == DataControlRowType.DataRow)
        //{
        //    if (Objmain.checkaccss(Session["CoCode"].ToString(), Session["UserRoleID"].ToString().Trim(), "PATIENT ENROLLMENT", checkAccessType.DeleteAction) == false)
        //    {
        //        e.Row.Cells[19].Visible = false;
        //    }

        //}
    }
    protected void TextBox7_TextChanged(object sender, EventArgs e)
    {
        string ph1 = TextBox7.Text;
        DataTable dt = thepatientAppo.GetOpdRegbyPhone(Session["CoCode"].ToString().Trim(), ph1.Trim());
        if (dt.Rows.Count > 0)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "msg", "alert('Phone No already registered !');", true);
            TextBox7.Text = "";
        }
    }

    protected void ddlPaymentMode_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlPaymentMode.SelectedValue == "B")
        {
            lblchqdt.InnerText = "Cheque Date :";
            lblchqno.InnerText = "Cheque No :";
            lblbankNm.InnerText = "Bank Name :";
            divchqdt.Visible = true;
            divchqno.Visible = true;
            divBank.Visible = true;
        }
        else if (ddlPaymentMode.SelectedValue == "R")
        {
            lblchqdt.InnerText = "Expire Date :";
            lblchqno.InnerText = "Card No :";
            lblbankNm.InnerText = "Card Holder Name :";
            divchqdt.Visible = true;
            divchqno.Visible = true;
            divBank.Visible = true;
        }
        else
        {
            divchqdt.Visible = false;
            divchqno.Visible = false;
            divBank.Visible = false;
        }
    }

    protected void Button5_Click(object sender, EventArgs e)
    {
        GridFill();
    }
    protected void chkParmAdd_CheckedChanged(object sender, EventArgs e)
    {
        if (chkParmAdd.Checked == true)
        {
            txtParmAddr.Text = TextBox10.Text;
            txtParmanentPin.Text = txtPresentPin.Text;
            //txtParmDist.Text = txtPresentDist.Text;
            //txtParmState.Text = txtPresentState.Text;
            //txtParmCountry.Text = txtPresentCountry.Text;

            ddlCountryPermanent.SelectedValue = ddlCountryPresent.SelectedValue;

            ddlStatePermanent.Items.Clear();
            ddlStatePermanent.DataSource = thepatientAppo.getState(Session["CoCode"].ToString().Trim(), ddlCountryPermanent.SelectedValue.Trim());
            ddlStatePermanent.DataTextField = "State_Name";
            ddlStatePermanent.DataValueField = "State_ID";
            ddlStatePermanent.DataBind();
            ddlStatePermanent.SelectedValue = ddlStatePresent.SelectedValue;

            ddlDistrictPermanent.Items.Clear();
            ddlDistrictPermanent.DataSource = thepatientAppo.GetSateWiseDistrict(Session["CoCode"].ToString().Trim(), ddlStatePermanent.SelectedValue.Trim());
            ddlDistrictPermanent.DataTextField = "DistrictName";
            ddlDistrictPermanent.DataValueField = "ID";
            ddlDistrictPermanent.DataBind();
            ddlDistrictPermanent.SelectedValue = ddlDistrictPresent.SelectedValue;
        }
        else
        {
            txtParmAddr.Text = "";
            txtParmanentPin.Text = "";
            //txtParmDist.Text = "";
            //txtParmState.Text = "";
            //txtParmCountry.Text = "";
        }
    }
    protected void ddlCountry_SelectedIndexChanged(object sender, EventArgs e)
    {
        ddlStatePresent.Items.Clear();
        ddlStatePresent.DataSource = thepatientAppo.getState(Session["CoCode"].ToString().Trim(), ddlCountryPresent.SelectedValue.Trim());
        ddlStatePresent.DataTextField = "State_Name";
        ddlStatePresent.DataValueField = "State_ID";
        ddlStatePresent.DataBind();
        this.ddlStatePresent.Items.Insert(0, new ListItem("--Select--", ""));
    }
    protected void ddlState_SelectedIndexChanged(object sender, EventArgs e)
    {
        ddlDistrictPresent.Items.Clear();
        ddlDistrictPresent.DataSource = thepatientAppo.GetSateWiseDistrict(Session["CoCode"].ToString().Trim(), ddlStatePresent.SelectedValue.Trim());
        ddlDistrictPresent.DataTextField = "DistrictName";
        ddlDistrictPresent.DataValueField = "ID";
        ddlDistrictPresent.DataBind();
        this.ddlDistrictPresent.Items.Insert(0, new ListItem("--Select--", ""));
    }
    protected void ddlCountryParmanent_SelectedIndexChanged(object sender, EventArgs e)
    {
        ddlStatePermanent.Items.Clear();
        ddlStatePermanent.DataSource = thepatientAppo.getState(Session["CoCode"].ToString().Trim(), ddlCountryPermanent.SelectedValue.Trim());
        ddlStatePermanent.DataTextField = "State_Name";
        ddlStatePermanent.DataValueField = "State_ID";
        ddlStatePermanent.DataBind();
        this.ddlStatePermanent.Items.Insert(0, new ListItem("--Select--", ""));
    }
    protected void ddlStatePermanent_SelectedIndexChanged(object sender, EventArgs e)
    {
        ddlDistrictPermanent.Items.Clear();
        ddlDistrictPermanent.DataSource = thepatientAppo.GetSateWiseDistrict(Session["CoCode"].ToString().Trim(), ddlStatePermanent.SelectedValue.Trim());
        ddlDistrictPermanent.DataTextField = "DistrictName";
        ddlDistrictPermanent.DataValueField = "ID";
        ddlDistrictPermanent.DataBind();
        this.ddlDistrictPermanent.Items.Insert(0, new ListItem("--Select--", ""));
    }
    //string cntryCode

    [System.Web.Script.Services.ScriptMethod()]
    [System.Web.Services.WebMethod]
    public static string GetStateList(string cntryCode)
    {
        string compcode = string.Empty;
        using (SqlConnection conn = new SqlConnection())
        {
            conn.ConnectionString = ConfigurationSettings.AppSettings["ConnectionString"].ToString();
            using (SqlCommand cmd = new SqlCommand())
            {
                compcode = HttpContext.Current.Session["CoCode"].ToString();

                //"select State_ID,State_Name from GN_StateMaster where compcode='" + compcode + "' and countryCode='" + CountryCode + "'";
                cmd.CommandText = "select State_ID,State_Name from GN_StateMaster where compcode=@Compcode and countryCode = @countryCode";
                cmd.Parameters.AddWithValue("@countryCode", cntryCode);
                cmd.Parameters.AddWithValue("@Compcode", compcode);

                cmd.Connection = conn;
                conn.Open();
                //List<ListItem> customers = new List<ListItem>();
                String customers = "";
                using (SqlDataReader sdr = cmd.ExecuteReader())
                {
                    while (sdr.Read())
                    {
                        //customers.Add(new ListItem
                        //{
                        //    Value = sdr["State_ID"].ToString(),
                        //    Text = sdr["State_Name"].ToString()
                        //});
                        customers = customers + sdr["State_ID"].ToString() + "/" + sdr["State_Name"].ToString() + "~";
                    }
                }
                conn.Close();
                return customers;
            }
        }
        
    }

    [System.Web.Script.Services.ScriptMethod()]
    [System.Web.Services.WebMethod]
    public static string GetDistrictList(string stateCode)
    {
        string compcode = string.Empty;
        using (SqlConnection conn = new SqlConnection())
        {
            conn.ConnectionString = ConfigurationSettings.AppSettings["ConnectionString"].ToString();
            using (SqlCommand cmd = new SqlCommand())
            {
                compcode = HttpContext.Current.Session["CoCode"].ToString();


                cmd.CommandText = "select ID,DistrictName from GN_District where compcode=@Compcode and StateCode = @stateCode";
                cmd.Parameters.AddWithValue("@stateCode", stateCode);
                cmd.Parameters.AddWithValue("@Compcode", compcode);

                cmd.Connection = conn;
                conn.Open();
                //List<ListItem> customers = new List<ListItem>();
                String customers = "";
                using (SqlDataReader sdr = cmd.ExecuteReader())
                {
                    while (sdr.Read())
                    {
                        //customers.Add(new ListItem
                        //{
                        //    Value = sdr["State_ID"].ToString(),
                        //    Text = sdr["State_Name"].ToString()
                        //});
                        customers = customers + sdr["ID"].ToString() + "/" + sdr["DistrictName"].ToString() + "~";
                    }
                }
                conn.Close();
                return customers;
            }
        }

    }
}