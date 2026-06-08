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
using System.Web.Caching;
using System.Globalization;

public partial class MasterPage : System.Web.UI.MasterPage
{
    PatientLogin_Page theHelper = new PatientLogin_Page(ConfigurationSettings.AppSettings["ConnectionString"].ToString());
    System.Text.StringBuilder rpt = new System.Text.StringBuilder();
    protected void Page_Load(object sender, EventArgs e)
    {

       

        //if (Session["userName"] != null)
        //{
        //    lblWelcome.Text = Session["userName"].ToString();

        //    if (Cache["Menu"] == null)
        //        GenerateMenu();
        //    else
        //        CreateMenu(Cache["Menu"].ToString());

        //}
        //else
        //{
        //    Response.Redirect("../LoginPage.aspx");
        //}

        //if (isMobileBrowser())
        //{
        //    //NavigationMenu.Visible = false;
        //    //NavigationTreeView.Visible = true;
        //}

        if (Request.UserAgent.IndexOf("AppleWebKit") > 0)
        {
            Request.Browser.Adapters.Clear();
        }

        if (!IsPostBack)
        {
            //AddEventHandlers();
            //AddPageContent();
        }
    }

    //public void GenerateMenu()
    //{
    //    MenuGenerate thepd = new MenuGenerate(ConfigurationSettings.AppSettings["ConnectionString"].ToString());

    //    DataTable module = thepd.GetModuleDetails();
    //    rpt.Append("<ul id='nav'>");

    //    rpt.Append("<li><a href='../HomePage.aspx'>HOME</a></li>");

    //    for (int i = 0; i < module.Rows.Count; i++)
    //    {
    //        DataTable submodule = thepd.GetSubModuleDetails(module.Rows[i]["ModuleID"].ToString(), Session["userName"].ToString());
    //        //rpt.AppendFormat("<li><a href='#'>{0}</a>", module.Rows[i]["ModuleName"]);
    //        if (submodule.Rows.Count > 0)
    //        {
    //            rpt.AppendFormat("<li><a href='#'>{0}</a>", module.Rows[i]["ModuleName"]);
    //            rpt.Append("<ul>");
    //            for (int j = 0; j < submodule.Rows.Count; j++)
    //            {
    //                DataTable menu = thepd.GetMenuDetails(submodule.Rows[j]["SubModuleID"].ToString(), Session["userName"].ToString());
    //                if (menu.Rows.Count > 0)
    //                {
    //                    rpt.AppendFormat("<li><a href='#'>{0}</a>", submodule.Rows[j]["SubModuleName"]);
    //                    rpt.Append("<ul>");
    //                    for (int k = 0; k < menu.Rows.Count; k++)
    //                    {
    //                        rpt.AppendFormat("<li><a href='{0}'>{1}</a></li>", menu.Rows[k]["URL"], menu.Rows[k]["MenuName"]);
    //                    }

    //                    rpt.Append("</ul>");
    //                    rpt.Append("</li>");
    //                }
    //            }
    //            rpt.Append("</ul>");
    //            rpt.Append("</li>");
    //        }
    //    }
    //    rpt.Append("</li>");
    //    rpt.Append("</ul>");
    //    CreateMenu(rpt.ToString());

    //    //    From here all codes are commented      ;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;



    // //   ltrReport.Text = rpt.ToString();
    //}

    private void CreateMenu(string rpt)
    {
        string folderName = "MyFiles/";
        DataSet ds = null; // Database call by sp.. sp

        // storedprocedure should return three tables 1.Module  2.Submodule  3. Menu
        string menuText;

        if (Cache["Menu"] == null)
        {
            ds = new DataSet();
            //ds.ReadXml(Server.MapPath(menuPath));
            // menu is created
            // DataTable dt1 = ds.Tables[0]; // for module
            //DataTable dt2 = ds.Tables[0]; // for module
            //DataTable dt3 = ds.Tables[0]; // for module



            //    From here all codes are commented      ;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;


           //  ltrReport.Text = rpt;
            // menuText = ltrReport.Text; // create loop of dt1, dt2 and dt3 and then prepare the menutext

            //Cache.Insert("Menu", menuText, new System.Web.Caching.CacheDependency(Server.MapPath(folderName)),DateTime.Now.AddMinutes(60), TimeSpan.Zero,System.Web.Caching.CacheItemPriority.Default,new System.Web.Caching.CacheItemRemovedCallback(CacheItemRemovedCallBack));
            Cache.Insert("Menu", rpt, new System.Web.Caching.CacheDependency(Server.MapPath(folderName)), DateTime.Now.AddMinutes(60), TimeSpan.Zero, System.Web.Caching.CacheItemPriority.Default, new System.Web.Caching.CacheItemRemovedCallback(CacheItemRemovedCallBack));


            // DisplayCacheCreationTime("Object was not in the cache and created at:", DateTime.Now.ToLongTimeString());
        }

        else
        {
            // menu is created from the cache

            // DisplayCacheCreationTime("Object was in the cache", String.Empty);

            //    From here all codes are commented      ;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;



      //      ltrReport.Text = Cache["Menu"].ToString();
            // add menutext to literal control
        }
    }

    private void CacheItemRemovedCallBack(string key, object value, CacheItemRemovedReason reason)
    {
        //Response.Write("Hello world");
    }





   

    
    
    
    
    /*02032010 - Fixed the display problem in Safari and Google Chrome
     http://www.google.com/support/forum/p/Chrome/thread?tid=7506cca26ec06af7&hl=en
     */
    protected override void AddedControl(Control control, int index)
    {
        // This is necessary because Safari and Chrome browsers don't display the Menu control correctly.
        // Add this to the code in your master page.
        if (Request.ServerVariables["http_user_agent"].IndexOf("Safari", StringComparison.CurrentCultureIgnoreCase) != -1)
            this.Page.ClientTarget = "uplevel";

        base.AddedControl(control, index);
    } 

    private void AddPageContent()
    {
        string pageName = HttpContext.Current.Request.Url.AbsolutePath.Substring(
                     HttpContext.Current.Request.Url.AbsolutePath.LastIndexOf("/") + 1);

        ContentPlaceHolder contentPlaceHolder = (ContentPlaceHolder)this.Page.Master.FindControl("ContentPlaceHolder1");
        Label label = new Label();
        //label.Text = " <br /> Content for page: " + pageName;
        contentPlaceHolder.Controls.Add(label);
    }

    private void AddEventHandlers()
    {
        //NavigationMenu.MenuItemDataBound += new MenuEventHandler(NavigationMenu_MenuItemDataBound);
        //SiteMap.SiteMapResolve += new SiteMapResolveEventHandler(SiteMap_SiteMapResolve);
        //SiteMapPath1.ItemCreated +=new SiteMapNodeItemEventHandler(SiteMapPath1_ItemCreated);
    }

    protected void SiteMapPath1_ItemCreated(object sender, SiteMapNodeItemEventArgs e)
    {
        if (e.Item.ItemType == SiteMapNodeItemType.Root)
        {
           // SiteMapPath1.PathSeparator = " ";
        }
        else
        {
            //SiteMapPath1.PathSeparator = " >> ";
        }
    }
  
    //http://msdn.microsoft.com/en-us/library/system.web.sitemap.sitemapresolve.aspx
    //solve > SiteMapNode is readonly, property Title cannot be modified. 

    //SiteMapNode SiteMap_SiteMapResolve(object sender, SiteMapResolveEventArgs e)
    //{
    //    if (SiteMap.CurrentNode != null)
    //    {
    //        SiteMapNode currentNode = SiteMap.CurrentNode.Clone(true);
    //        SiteMapNode tempNode = currentNode;
    //        tempNode = ReplaceNodeText(tempNode);

    //        return currentNode;
    //    }

    //    return null;
    //}

    ////remove <u></u> tag recursively
    //internal SiteMapNode ReplaceNodeText(SiteMapNode smn)
    //{
    //    //current node
    //    if (smn != null && smn.Title.Contains("<u>"))
    //    {
    //        smn.Title = smn.Title.Replace("<u>", "").Replace("</u>", "");
    //    }

    //    //parent nd
    //    if (smn.ParentNode != null)
    //    {
    //        if (smn.ParentNode.Title.Contains("<u>"))
    //        {
    //            SiteMapNode gpn = smn.ParentNode;
    //            smn.ParentNode.Title = smn.ParentNode.Title.Replace("<u>", "").Replace("</u>", "");
    //            smn = ReplaceNodeText(gpn);
    //        }
    //    }
    //    return smn;
    //}

    //void NavigationMenu_MenuItemDataBound(object sender, MenuEventArgs e)
    //{
    //    SiteMapNode node = (SiteMapNode)e.Item.DataItem;
       
    //    //set the target of the navigation menu item (blank, self, etc...)
    //    if (node["target"] != null)
    //    {
    //        e.Item.Target = node["target"];
    //    }
    //    //create access key button
    //    if (node["accesskey"] != null)
    //    {
    //        CreateAccessKeyButton(node["accesskey"] as string, node.Url);
    //    }
    //}

    //create access key button
    //void CreateAccessKeyButton(string ak, string url)
    //{
    //    HtmlButton inputBtn = new HtmlButton();
    //    inputBtn.Style.Add("width", "1px");
    //    inputBtn.Style.Add("height", "1px");
    //    inputBtn.Style.Add("position", "absolute");
    //    inputBtn.Style.Add("left", "-2555px");
    //    inputBtn.Style.Add("z-index", "-1");
    //    inputBtn.Attributes.Add("type", "button");
    //    inputBtn.Attributes.Add("value", "");
    //    inputBtn.Attributes.Add("accesskey", ak);
    //    inputBtn.Attributes.Add("onclick", "navigateTo('" + url + "');");

    //    AccessKeyPanel.Controls.Add(inputBtn);
    //}

    /*
     * 02032010, Detecting a mobile browser
     * http://www.codeproject.com/KB/aspnet/mobiledetect.aspx
     Place this on a seperate class
     */
    //public static readonly string[] mobiles =          new[]
    //            {
    //                "midp", "j2me", "avant", "docomo", 
    //                "novarra", "palmos", "palmsource", 
    //                "240x320", "opwv", "chtml",
    //                "pda", "windows ce", "mmp/", 
    //                "blackberry", "mib/", "symbian", 
    //                "wireless", "nokia", "hand", "mobi",
    //                "phone", "cdm", "up.b", "audio", 
    //                "SIE-", "SEC-", "samsung", "HTC", 
    //                "mot-", "mitsu", "sagem", "sony"
    //                , "alcatel", "lg", "eric", "vx", 
    //                "philips", "mmm", "xx", 
    //                "panasonic", "sharp", "wap", "sch",
    //                "rover", "pocket", "benq", "java", 
    //                "pg", "vox", "amoi", 
    //                "bird", "compal", "kg", "voda",
    //                "sany", "kdd", "dbt", "sendo", 
    //                "sgh", "gradi", "jb", "dddi", 
    //                "moto", "iphone"
    //            };

    public static bool isMobileBrowser()
    {
        ////GETS THE CURRENT USER CONTEXT
        //HttpContext context = HttpContext.Current;

        ////FIRST TRY BUILT IN ASP.NT CHECK
        //if (context.Request.Browser.IsMobileDevice)
        //{
        //    return true;
        //}
        ////THEN TRY CHECKING FOR THE HTTP_X_WAP_PROFILE HEADER
        //if (context.Request.ServerVariables["HTTP_X_WAP_PROFILE"] != null)
        //{
        //    return true;
        //}
        ////THEN TRY CHECKING THAT HTTP_ACCEPT EXISTS AND CONTAINS WAP
        //if (context.Request.ServerVariables["HTTP_ACCEPT"] != null &&
        //    context.Request.ServerVariables["HTTP_ACCEPT"].ToLower().Contains("wap"))
        //{
        //    return true;
        //}
        ////AND FINALLY CHECK THE HTTP_USER_AGENT 
        ////HEADER VARIABLE FOR ANY ONE OF THE FOLLOWING
        //if (context.Request.ServerVariables["HTTP_USER_AGENT"] != null)
        //{
        //    for (int i = 0; i < mobiles.Length; i++)
        //    {
        //        if (context.Request.ServerVariables["HTTP_USER_AGENT"].
        //                                            ToLower().Contains(mobiles[i].ToLower()))
        //        {
        //            return true;
        //        }
        //    }
        //}

        return false;
    }



    protected void LinkButton1_Click(object sender, EventArgs e)
    {
        theHelper.updateLoginHistory(Session["userName"].ToString());
        Session.Abandon();
        Session.Clear();
        Cache.Remove("Menu");
        Response.Redirect("LoginPage.aspx");
    }
}
