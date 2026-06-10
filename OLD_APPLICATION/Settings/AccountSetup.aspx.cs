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

public partial class Master_AccountSetup : System.Web.UI.Page
{
    Main Objmain = new Main(ConfigurationSettings.AppSettings["ConnectionString"].ToString());
    AccountSetup theHelper = new AccountSetup(ConfigurationSettings.AppSettings["ConnectionString"].ToString());
    protected void Page_Load(object sender, EventArgs e)
    {
        Page.Title = "Account Setup";
        if (Session["userName"] == null)
        {
            Response.Redirect("../LoginPage.aspx");
        }
        if (Objmain.checkaccss(Session["CoCode"].ToString(), Session["UserRoleID"].ToString().Trim(), "ACCOUNTS SETUP", checkAccessType.ViewAction) == false)
        {
            Response.Redirect("../AccessDenied.aspx");
        }
        if (Objmain.checkaccss(Session["CoCode"].ToString(), Session["UserRoleID"].ToString().Trim(), "ACCOUNTS SETUP", checkAccessType.InsertAction) == false)
        {
            btnSubmit.Enabled = false;
        }

        if (!IsPostBack)
        {

            Int32 parmsdata = theHelper.getParmsData(Session["CoCode"].ToString().Trim(), Session["YearCode"].ToString().Trim());
            if (parmsdata == 1)
            {
                chkLink.Checked = true;
            }
            else
            {
                chkLink.Checked = false;
            }
            DropDownFill();
           
        }
    }

    protected int SearchText(string Value, DropDownList ddl)
    {
        int i;
        for (i = 0; i < ddl.Items.Count; i++)
        {
            if (ddl.Items[i].Value.Trim() == Value.Trim())
                return i;
        }
        return -1;
    }

    private void DropDownFill()
    {
        DataTable dt = theHelper.getRecord(Session["CoCode"].ToString().Trim(), Session["YearCode"].ToString().Trim());
        ddlJournal.Items.Clear();
        this.ddlJournal.DataSource = theHelper.getJournal(Session["CoCode"].ToString().Trim());
        this.ddlJournal.DataTextField = "BkName";
        this.ddlJournal.DataValueField = "BkCode";
        this.ddlJournal.DataBind();
        this.ddlJournal.Items.Insert(0, new ListItem("--Select--", "0"));
        this.ddlJournal.SelectedIndex = SearchText(dt.Rows[0]["CFILLER01"].ToString(), this.ddlJournal);
        this.ddlJournal.Attributes["style"] = "width: 150px;";


        ddlpatient.Items.Clear();
        this.ddlopdfees.DataSource = theHelper.getGL(Session["CoCode"].ToString().Trim());
        this.ddlopdfees.DataTextField = "GLNAME";
        this.ddlopdfees.DataValueField = "GLCODE";
        this.ddlopdfees.DataBind();
        this.ddlopdfees.Items.Insert(0, new ListItem("--Select--", "0"));
        this.ddlopdfees.SelectedIndex = SearchText(dt.Rows[0]["Patient_GL"].ToString(), this.ddlpatient);
        this.ddlopdfees.Attributes["style"] = "width: 150px;";

        ddldoctor.Items.Clear();
        this.ddldoctor.DataSource = theHelper.getGL(Session["CoCode"].ToString().Trim());
        this.ddldoctor.DataTextField = "GLNAME";
        this.ddldoctor.DataValueField = "GLCODE";
        this.ddldoctor.DataBind();
        this.ddldoctor.Items.Insert(0, new ListItem("--Select--", "0"));
        this.ddldoctor.SelectedIndex = SearchText(dt.Rows[0]["Doctor_GL"].ToString(), this.ddldoctor);
        this.ddldoctor.Attributes["style"] = "width: 150px;";

        ddlbedchrg.Items.Clear();
        this.ddlbedchrg.DataSource = theHelper.getGL(Session["CoCode"].ToString().Trim());
        this.ddlbedchrg.DataTextField = "GLNAME";
        this.ddlbedchrg.DataValueField = "GLCODE";
        this.ddlbedchrg.DataBind();
        this.ddlbedchrg.Items.Insert(0, new ListItem("--Select--", "0"));
        this.ddlbedchrg.SelectedIndex = SearchText(dt.Rows[0]["P_BedCharges_GL"].ToString(), this.ddlbedchrg);
        this.ddlbedchrg.Attributes["style"] = "width: 150px;";

        ddlconschrg.Items.Clear();
        this.ddlconschrg.DataSource = theHelper.getGL(Session["CoCode"].ToString().Trim());
        this.ddlconschrg.DataTextField = "GLNAME";
        this.ddlconschrg.DataValueField = "GLCODE";
        this.ddlconschrg.DataBind();
        this.ddlconschrg.Items.Insert(0, new ListItem("--Select--", "0"));
        this.ddlconschrg.SelectedIndex = SearchText(dt.Rows[0]["P_ConsCharges_GL"].ToString(), this.ddlconschrg);
        this.ddlconschrg.Attributes["style"] = "width: 150px;";

        ddldocvisit.Items.Clear();
        this.ddldocvisit.DataSource = theHelper.getGL(Session["CoCode"].ToString().Trim());
        this.ddldocvisit.DataTextField = "GLNAME";
        this.ddldocvisit.DataValueField = "GLCODE";
        this.ddldocvisit.DataBind();
        this.ddldocvisit.Items.Insert(0, new ListItem("--Select--", "0"));
        this.ddldocvisit.SelectedIndex = SearchText(dt.Rows[0]["P_DocCharges_GL"].ToString(), this.ddldocvisit);
        this.ddldocvisit.Attributes["style"] = "width: 150px;";

        ddlmedicin.Items.Clear();
        this.ddlmedicin.DataSource = theHelper.getGL(Session["CoCode"].ToString().Trim());
        this.ddlmedicin.DataTextField = "GLNAME";
        this.ddlmedicin.DataValueField = "GLCODE";
        this.ddlmedicin.DataBind();
        this.ddlmedicin.Items.Insert(0, new ListItem("--Select--", "0"));
        this.ddlmedicin.SelectedIndex = SearchText(dt.Rows[0]["P_MedicineCharges_GL"].ToString(), this.ddlmedicin);
        this.ddlmedicin.Attributes["style"] = "width: 150px;";

        ddlpatho.Items.Clear();
        this.ddlpatho.DataSource = theHelper.getGL(Session["CoCode"].ToString().Trim());
        this.ddlpatho.DataTextField = "GLNAME";
        this.ddlpatho.DataValueField = "GLCODE";
        this.ddlpatho.DataBind();
        this.ddlpatho.Items.Insert(0, new ListItem("--Select--", "0"));
        this.ddlpatho.SelectedIndex = SearchText(dt.Rows[0]["P_PathCharges_GL"].ToString(), this.ddlpatho);
        this.ddlpatho.Attributes["style"] = "width: 150px;";

        ddlservice.Items.Clear();
        this.ddlservice.DataSource = theHelper.getGL(Session["CoCode"].ToString().Trim());
        this.ddlservice.DataTextField = "GLNAME";
        this.ddlservice.DataValueField = "GLCODE";
        this.ddlservice.DataBind();
        this.ddlservice.Items.Insert(0, new ListItem("--Select--", "0"));
        this.ddlservice.SelectedIndex = SearchText(dt.Rows[0]["P_ServicesCharges_GL"].ToString(), this.ddlservice);
        this.ddlservice.Attributes["style"] = "width: 150px;";

        ddlsister.Items.Clear();
        this.ddlsister.DataSource = theHelper.getGL(Session["CoCode"].ToString().Trim());
        this.ddlsister.DataTextField = "GLNAME";
        this.ddlsister.DataValueField = "GLCODE";
        this.ddlsister.DataBind();
        this.ddlsister.Items.Insert(0, new ListItem("--Select--", "0"));
        this.ddlsister.SelectedIndex = SearchText(dt.Rows[0]["P_SisAyaCharges_GL"].ToString(), this.ddlsister);
        this.ddlsister.Attributes["style"] = "width: 150px;";

        ddlusg.Items.Clear();
        this.ddlusg.DataSource = theHelper.getGL(Session["CoCode"].ToString().Trim());
        this.ddlusg.DataTextField = "GLNAME";
        this.ddlusg.DataValueField = "GLCODE";
        this.ddlusg.DataBind();
        this.ddlusg.Items.Insert(0, new ListItem("--Select--", "0"));
        this.ddlusg.SelectedIndex = SearchText(dt.Rows[0]["P_UsgCharges_GL"].ToString(), this.ddlusg);
        this.ddlusg.Attributes["style"] = "width: 150px;";

        ddlxray.Items.Clear();
        this.ddlxray.DataSource = theHelper.getGL(Session["CoCode"].ToString().Trim());
        this.ddlxray.DataTextField = "GLNAME";
        this.ddlxray.DataValueField = "GLCODE";
        this.ddlxray.DataBind();
        this.ddlxray.Items.Insert(0, new ListItem("--Select--", "0"));
        this.ddlxray.SelectedIndex = SearchText(dt.Rows[0]["P_XrayCharges_GL"].ToString(), this.ddlxray);
        this.ddlxray.Attributes["style"] = "width: 150px;";

        ddlambulance.Items.Clear();
        this.ddlambulance.DataSource = theHelper.getGL(Session["CoCode"].ToString().Trim());
        this.ddlambulance.DataTextField = "GLNAME";
        this.ddlambulance.DataValueField = "GLCODE";
        this.ddlambulance.DataBind();
        this.ddlambulance.Items.Insert(0, new ListItem("--Select--", "0"));
        this.ddlambulance.SelectedIndex = SearchText(dt.Rows[0]["P_AmbulanceCharges_GL"].ToString(), this.ddlambulance);
        this.ddlambulance.Attributes["style"] = "width: 150px;";

        ddlotchrg.Items.Clear();
        this.ddlotchrg.DataSource = theHelper.getGL(Session["CoCode"].ToString().Trim());
        this.ddlotchrg.DataTextField = "GLNAME";
        this.ddlotchrg.DataValueField = "GLCODE";
        this.ddlotchrg.DataBind();
        this.ddlotchrg.Items.Insert(0, new ListItem("--Select--", "0"));
        this.ddlotchrg.SelectedIndex = SearchText(dt.Rows[0]["P_OTCharges_GL"].ToString(), this.ddlotchrg);
        this.ddlotchrg.Attributes["style"] = "width: 150px;";

        ddlanesthesia.Items.Clear();
        this.ddlanesthesia.DataSource = theHelper.getGL(Session["CoCode"].ToString().Trim());
        this.ddlanesthesia.DataTextField = "GLNAME";
        this.ddlanesthesia.DataValueField = "GLCODE";
        this.ddlanesthesia.DataBind();
        this.ddlanesthesia.Items.Insert(0, new ListItem("--Select--", "0"));
        this.ddlanesthesia.SelectedIndex = SearchText(dt.Rows[0]["P_AnthesCharges_GL"].ToString(), this.ddlanesthesia);
        this.ddlanesthesia.Attributes["style"] = "width: 150px;";

        ddlothchrg.Items.Clear();
        this.ddlothchrg.DataSource = theHelper.getGL(Session["CoCode"].ToString().Trim());
        this.ddlothchrg.DataTextField = "GLNAME";
        this.ddlothchrg.DataValueField = "GLCODE";
        this.ddlothchrg.DataBind();
        this.ddlothchrg.Items.Insert(0, new ListItem("--Select--", "0"));
        this.ddlothchrg.SelectedIndex = SearchText(dt.Rows[0]["P_OthrCharges_GL"].ToString(), this.ddlothchrg);
        this.ddlothchrg.Attributes["style"] = "width: 150px;";


        this.ddldialysis.DataSource = theHelper.getGL(Session["CoCode"].ToString().Trim());
        this.ddldialysis.DataTextField = "GLNAME";
        this.ddldialysis.DataValueField = "GLCODE";
        this.ddldialysis.DataBind();
        this.ddldialysis.Items.Insert(0, new ListItem("--Select--", "0"));
        this.ddldialysis.SelectedIndex = SearchText(dt.Rows[0]["P_DialysisFees_GL"].ToString(), this.ddlrefconsulchrg);
        this.ddldialysis.Attributes["style"] = "width: 150px;";

        this.ddlothchrg.DataSource = theHelper.getGL(Session["CoCode"].ToString().Trim());
        this.ddlothchrg.DataTextField = "GLNAME";
        this.ddlothchrg.DataValueField = "GLCODE";
        this.ddlothchrg.DataBind();
        this.ddlothchrg.Items.Insert(0, new ListItem("--Select--", "0"));
        this.ddlothchrg.SelectedIndex = SearchText(dt.Rows[0]["P_OPDFees_GL"].ToString(), this.ddlrefconsulchrg);
        this.ddlothchrg.Attributes["style"] = "width: 150px;";

        this.ddldisposable.DataSource = theHelper.getGL(Session["CoCode"].ToString().Trim());
        this.ddldisposable.DataTextField = "GLNAME";
        this.ddldisposable.DataValueField = "GLCODE";
        this.ddldisposable.DataBind();
        this.ddldisposable.Items.Insert(0, new ListItem("--Select--", "0"));
        this.ddldisposable.SelectedIndex = SearchText(dt.Rows[0]["P_DispFees_GL"].ToString(), this.ddlrefconsulchrg);
        this.ddldisposable.Attributes["style"] = "width: 150px;";


        ddlotconschrg.Items.Clear();
        this.ddlotconschrg.DataSource = theHelper.getGL(Session["CoCode"].ToString().Trim());
        this.ddlotconschrg.DataTextField = "GLNAME";
        this.ddlotconschrg.DataValueField = "GLCODE";
        this.ddlotconschrg.DataBind();
        this.ddlotconschrg.Items.Insert(0, new ListItem("--Select--", "0"));
        this.ddlotconschrg.SelectedIndex = SearchText(dt.Rows[0]["P_OTConsCharges_GL"].ToString(), this.ddlotconschrg);
        this.ddlotconschrg.Attributes["style"] = "width: 150px;";

        ddlotinstchrg.Items.Clear();
        this.ddlotinstchrg.DataSource = theHelper.getGL(Session["CoCode"].ToString().Trim());
        this.ddlotinstchrg.DataTextField = "GLNAME";
        this.ddlotinstchrg.DataValueField = "GLCODE";
        this.ddlotinstchrg.DataBind();
        this.ddlotinstchrg.Items.Insert(0, new ListItem("--Select--", "0"));
        this.ddlotinstchrg.SelectedIndex = SearchText(dt.Rows[0]["P_OTInsCharges_GL"].ToString(), this.ddlotinstchrg);
        this.ddlotinstchrg.Attributes["style"] = "width: 150px;";

        ddlotattenchrg.Items.Clear();
        this.ddlotattenchrg.DataSource = theHelper.getGL(Session["CoCode"].ToString().Trim());
        this.ddlotattenchrg.DataTextField = "GLNAME";
        this.ddlotattenchrg.DataValueField = "GLCODE";
        this.ddlotattenchrg.DataBind();
        this.ddlotattenchrg.Items.Insert(0, new ListItem("--Select--", "0"));
        this.ddlotattenchrg.SelectedIndex = SearchText(dt.Rows[0]["P_OTAttCharges_GL"].ToString(), this.ddlotattenchrg);
        this.ddlotattenchrg.Attributes["style"] = "width: 150px;";

        ddlanesconschrg.Items.Clear();
        this.ddlanesconschrg.DataSource = theHelper.getGL(Session["CoCode"].ToString().Trim());
        this.ddlanesconschrg.DataTextField = "GLNAME";
        this.ddlanesconschrg.DataValueField = "GLCODE";
        this.ddlanesconschrg.DataBind();
        this.ddlanesconschrg.Items.Insert(0, new ListItem("--Select--", "0"));
        this.ddlanesconschrg.SelectedIndex = SearchText(dt.Rows[0]["P_OTAnthesConsCharges_GL"].ToString(), this.ddlanesconschrg);
        this.ddlanesconschrg.Attributes["style"] = "width: 150px;";

        ddlsurgeonchrg.Items.Clear();
        this.ddlsurgeonchrg.DataSource = theHelper.getGL(Session["CoCode"].ToString().Trim());
        this.ddlsurgeonchrg.DataTextField = "GLNAME";
        this.ddlsurgeonchrg.DataValueField = "GLCODE";
        this.ddlsurgeonchrg.DataBind();
        this.ddlsurgeonchrg.Items.Insert(0, new ListItem("--Select--", "0"));
        this.ddlsurgeonchrg.SelectedIndex = SearchText(dt.Rows[0]["SurgeonCharge_GL"].ToString(), this.ddlsurgeonchrg);
        this.ddlsurgeonchrg.Attributes["style"] = "width: 150px;";

        ddlvisitchrg.Items.Clear();
        this.ddlvisitchrg.DataSource = theHelper.getGL(Session["CoCode"].ToString().Trim());
        this.ddlvisitchrg.DataTextField = "GLNAME";
        this.ddlvisitchrg.DataValueField = "GLCODE";
        this.ddlvisitchrg.DataBind();
        this.ddlvisitchrg.Items.Insert(0, new ListItem("--Select--", "0"));
        this.ddlvisitchrg.SelectedIndex = SearchText(dt.Rows[0]["DocVisitCharge_GL"].ToString(), this.ddlvisitchrg);
        this.ddlvisitchrg.Attributes["style"] = "width: 150px;";

        ddldocaneschrg.Items.Clear();
        this.ddldocaneschrg.DataSource = theHelper.getGL(Session["CoCode"].ToString().Trim());
        this.ddldocaneschrg.DataTextField = "GLNAME";
        this.ddldocaneschrg.DataValueField = "GLCODE";
        this.ddldocaneschrg.DataBind();
        this.ddldocaneschrg.Items.Insert(0, new ListItem("--Select--", "0"));
        this.ddldocaneschrg.SelectedIndex = SearchText(dt.Rows[0]["DocAnesthesisCharge_GL"].ToString(), this.ddldocaneschrg);
        this.ddldocaneschrg.Attributes["style"] = "width: 150px;";

        ddlrefconsulchrg.Items.Clear();
        this.ddlrefconsulchrg.DataSource = theHelper.getGL(Session["CoCode"].ToString().Trim());
        this.ddlrefconsulchrg.DataTextField = "GLNAME";
        this.ddlrefconsulchrg.DataValueField = "GLCODE";
        this.ddlrefconsulchrg.DataBind();
        this.ddlrefconsulchrg.Items.Insert(0, new ListItem("--Select--", "0"));
        this.ddlrefconsulchrg.SelectedIndex = SearchText(dt.Rows[0]["DocRefCharge_GL"].ToString(), this.ddlrefconsulchrg);
        this.ddlrefconsulchrg.Attributes["style"] = "width: 150px;";
        ddlrefconsulchrg.Items.Clear();


        
    }

    
    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        System.Globalization.DateTimeFormatInfo dtf = new DateTimeFormatInfo();
        string dt = DateTime.Now.ToString("yyyy-MM-dd");
        string flag = "0";
        if (chkLink.Checked == true)
            flag = "1";
        else
            flag = "0";
        if (theHelper.Update(Session["CoCode"].ToString().Trim(), Session["YearCode"].ToString().Trim(), ddlJournal.SelectedValue, ddlpatient.SelectedValue, ddldoctor.SelectedValue, ddlbedchrg.SelectedValue, ddlconschrg.SelectedValue, ddldocvisit.SelectedValue, ddlmedicin.SelectedValue, ddlpatho.SelectedValue, ddlservice.SelectedValue, ddlsister.SelectedValue, ddlusg.SelectedValue, ddlxray.SelectedValue, ddlambulance.SelectedValue, ddlotchrg.SelectedValue, ddlanesthesia.SelectedValue, ddlothchrg.SelectedValue, ddlotconschrg.SelectedValue, ddlotinstchrg.SelectedValue, ddlotattenchrg.SelectedValue, ddlanesconschrg.SelectedValue, flag, ddlsurgeonchrg.SelectedValue, ddlvisitchrg.SelectedValue, ddldocaneschrg.SelectedValue, ddlrefconsulchrg.SelectedValue, ddldialysis.SelectedValue, ddlothchrg.SelectedValue, ddldisposable.SelectedValue, Session["userName"].ToString(), dt) == true)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "msg", "alert('Record Successfully Updated!');", true);
            Response.Redirect("../Settings/AccountSetup.aspx");
        }
        else
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "msg", "alert('Error in Updated Data !');", true);
        }
        
    }
}