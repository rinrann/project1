<%@ Page Language="C#" MasterPageFile="~/MasterAll/MasterPageAll.master" AutoEventWireup="true" CodeFile="RoleWiseAccess.aspx.cs" Inherits="Master_RoleWiseAccess" %>


<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
     
<script language="javascript" type="text/javascript">

    function DisableBackButton() {
        window.history.forward()
    }
    DisableBackButton();
    window.onload = DisableBackButton;
    window.onpageshow = function (evt) { if (evt.persisted) DisableBackButton() }
    window.onunload = function () { void (0) }
    $(document).ready(function () {
        Calling();

        var prm = Sys.WebForms.PageRequestManager.getInstance();
        if (prm != null) {
            prm.add_endRequest(function (sender, e) {
                if (sender._postBackSettings.panelsToUpdate != null) {

                    Calling();

                }
            });
        };
    });


    function Calling() {
        $("input[id$='Button1']").click(function () {
            var e = document.getElementById("ctl00_ContentPlaceHolder1_DropDownList1");
            var strUser = e.options[e.selectedIndex].text;

            if (strUser == '--Select--') {
                alert('Select User Role');
                $(e).focus();
                $(e).addClass('textboxerr');
                return false;
            }
            else {
                $(e).removeClass('textboxerr');
            }

            var f = document.getElementById("ctl00_ContentPlaceHolder1_DropDownList4");
            var strUser = f.options[f.selectedIndex].text;

            if (strUser == '--Select--') {
                alert('Select Module');
                $(f).focus();
                $(f).addClass('textboxerr');
                return false;
            }
            else {
                $(f).removeClass('textboxerr');
            }


            var g = document.getElementById("ctl00_ContentPlaceHolder1_DropDownList5");
            var strUser = g.options[g.selectedIndex].text;

            if (strUser == '--Select--') {
                alert('Select Sub Module');
                $(g).focus();
                $(g).addClass('textboxerr');
                return false;
            }
            else {
                $(g).removeClass('textboxerr');
            }

            var h = document.getElementById("ctl00_ContentPlaceHolder1_DropDownList2");
            var strUser = h.options[h.selectedIndex].text;

            if (strUser == '--Select--') {
                alert('Select Menu');
                $(h).focus();
                $(h).addClass('textboxerr');
                return false;
            }
            else {
                $(h).removeClass('textboxerr');
            }

            var i = document.getElementById("ctl00_ContentPlaceHolder1_DropDownList3");
            var strUser = i.options[i.selectedIndex].text;

            if (strUser == '--Select--') {
                alert('Select User Action');
                $(i).focus();
                $(i).addClass('textboxerr');
                return false;
            }
            else {
                $(i).removeClass('textboxerr');
            }
        });
    }

</script>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">


<%--For Busy Loader .............................--%>


 <%--
  <asp:UpdateProgress ID="UpdateProgress1" runat="server">
    <ProgressTemplate>
    
       <div id="aaa" class="progressBackgroundFilter">          </div>
        <div id="bbb" class="processMessage"><img alt="Loading" src="../images/pwait.gif"/>
      
              </div>
    </ProgressTemplate>
    </asp:UpdateProgress>
--%>

    <%--For Busy Loader End.............................--%>


    <!-- Form Section html start -->
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <div class="pageheader">
                <asp:Label ID="Label1" runat="server">Role Wise Access</asp:Label>
            </div>
           
            <div class="formbox">
         
                <div class="form-sec">
                    <div class="error">
                        <strong>
                        <asp:Label ID="lblError" runat="server" Font-Bold="True" CssClass="fadeout"></asp:Label>
                        </strong>
                        <div class="clear">
                        </div>
                    </div>
                    <asp:HiddenField ID="HiddenField1" runat="server" Value="0" />
                <%--    <div class="form-sec-row">
                        <label>
                        <strong>Role ID :</strong></label>
                        <asp:TextBox ID="txtRoleId" runat="server" CssClass="textbox-medium1" 
                            Enabled="False" ></asp:TextBox>
                        <div class="clear">
                        </div>
                    </div>--%>
                      <div class="form-sec-row">
                        <label>
                        <strong>
                        User Role Name:</strong></label>
                         <asp:DropDownList ID="DropDownList1" runat="server" CssClass="textbox-medium1" >
                         </asp:DropDownList>
                        <div class="clear">
                        </div>
                    </div>

                <%--      <div class="form-sec-row">
                        <label>
                        <strong>
                        Module Name :</strong></label>
                        <asp:DropDownList ID="DropDownList4" runat="server" CssClass="textbox-medium1" AutoPostBack="true"
                              onselectedindexchanged="DropDownList4_SelectedIndexChanged">
                               
                         </asp:DropDownList>
                        <div class="clear">
                        </div>
                    </div>--%>

                  <%--    <div class="form-sec-row">
                        <label>
                        <strong>
                        Sub-Module Name :</strong></label>
                        <asp:DropDownList ID="DropDownList5" runat="server" CssClass="textbox-medium1" AutoPostBack="true"
                              onselectedindexchanged="DropDownList5_SelectedIndexChanged" >
                               
                         </asp:DropDownList>
                        <div class="clear">
                        </div>
                    </div>--%>

                 <%--    <div class="form-sec-row">
                        <label>
                        <strong>
                        Menu Name :</strong></label>
                         <asp:DropDownList ID="DropDownList2" runat="server" CssClass="textbox-medium1" >
                         </asp:DropDownList>
                        <div class="clear">
                        </div>
                    </div>--%>
                <%--     <div class="form-sec-row">
                        <label>
                        <strong> 
                        Action ID :</strong></label>
                       <asp:DropDownList ID="DropDownList3" runat="server" CssClass="textbox-medium1" >
                         </asp:DropDownList>
                        </div>--%>
                    </div>
                 
                    <div class="form-sec-row">
                        <div class="clear">
                        </div>
                    </div>
                  
                    <div class="clear">
                    </div>
 
 <table width="100%" style='background-color:#F86E31; color:#fff;'>
 <tr>
 <td><label><strong>Module :</strong></label></td>
  <td> 
      <asp:DropDownList ID="DropDownList2" Width="200px" runat="server" 
          AutoPostBack="true" onselectedindexchanged="DropDownList2_SelectedIndexChanged">
      </asp:DropDownList> </td>
   <td><label><strong>Sub-Module :</strong></label></td>
    <td>    <asp:DropDownList ID="DropDownList3" Width="200px" runat="server">
      </asp:DropDownList> </td>
      <td>    
          <asp:Button ID="Button3" runat="server" CssClass="submit-button"    Height="28px"
                            Text="Search" onclick="Button3_Click"  /></td>
 </tr>
 </table>
        <div class="listing" style='height:500px; width:100%; overflow:auto;'>
                <asp:GridView id="GridView1"  CssClass="grid1" PagerStyle-CssClass="pgr"
                 DataKeyNames ="ModuleID" runat="server"  PageSize="1000"
                 AutoGenerateColumns="False" AllowPaging="True" Width="100%" SelectedRowStyle-BackColor="GreenYellow"
                 OnRowCommand="GridView1_RowCommand"  OnRowDeleting="GridView1_RowDeleting" 
                 OnPageIndexChanging="GridView1_PageIndexChanging" >
                    <Columns>
                       
                
                        <asp:TemplateField HeaderText="Module Id" Visible="false">
                            <ItemTemplate>
                                <asp:Label ID="ModuleID" runat="server" Text='<%# Bind("ModuleID") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>

                         <asp:TemplateField HeaderText="Module Name" ItemStyle-HorizontalAlign="Left">
                            <ItemTemplate>
                                <asp:Label ID="ModuleName" runat="server" Text='<%# Bind("ModuleName") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="SubModule ID" Visible="false">
                            <ItemTemplate>
                                <asp:Label ID="SubModuleID" runat="server" Text='<%# Bind("SubModuleID") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>

                         <asp:TemplateField HeaderText="Sub-Module Name"  ItemStyle-HorizontalAlign="Left">
                            <ItemTemplate>
                                <asp:Label ID="SubModuleName" runat="server" Text='<%# Bind("SubModuleName") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="Menu ID" Visible="false">
                            <ItemTemplate>
                                <asp:Label ID="MenuID" runat="server" Text='<%# Bind("MenuID") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="Menu Name"  ItemStyle-HorizontalAlign="Left">
                            <ItemTemplate>
                                <asp:Label ID="MenuName" runat="server" Text='<%# Bind("MenuName") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>

                     <asp:TemplateField  ItemStyle-HorizontalAlign="Center">
            <HeaderTemplate> 
                <asp:CheckBox ID="CheckBox5"  Text="Select All"  runat="server" AutoPostBack="true"  OnCheckedChanged="CheckBox5_CheckedChanged" />
            </HeaderTemplate>
                            <ItemTemplate>
                                <asp:CheckBox ID="CheckBox1"   Text="View"  runat="server" />
                            </ItemTemplate>
                        </asp:TemplateField>

                           <asp:TemplateField   ItemStyle-HorizontalAlign="Center">

                            <HeaderTemplate> 
                <asp:CheckBox ID="CheckBox6"  Text="Select All"  runat="server"  AutoPostBack="true"  OnCheckedChanged="CheckBox6_CheckedChanged" />
            </HeaderTemplate>

                            <ItemTemplate>
                                <asp:CheckBox ID="CheckBox2" Text="Insert"  runat="server" />
                            </ItemTemplate>
                        </asp:TemplateField>

                           <asp:TemplateField   ItemStyle-HorizontalAlign="Center">

                             <HeaderTemplate>
               
                <asp:CheckBox ID="CheckBox7" Text="Select All"  runat="server" AutoPostBack="true"  OnCheckedChanged="CheckBox7_CheckedChanged" />
            </HeaderTemplate>

                            <ItemTemplate>
                                <asp:CheckBox ID="CheckBox3" Text="Update" runat="server" />
                            </ItemTemplate>
                        </asp:TemplateField>

                           <asp:TemplateField HeaderText="Delete"   ItemStyle-HorizontalAlign="Center">

                             <HeaderTemplate> 
                <asp:CheckBox ID="CheckBox8" runat="server"  Text="Select All"  AutoPostBack="true"  OnCheckedChanged="CheckBox8_CheckedChanged" />
            </HeaderTemplate>

                            <ItemTemplate>
                                <asp:CheckBox ID="CheckBox4" Text="Delete"  runat="server" />
                            </ItemTemplate>
                        </asp:TemplateField>
 
                    </Columns>
                    <PagerStyle CssClass="pgr" />
                    <EditRowStyle BackColor="#CCFF33" />
                    <AlternatingRowStyle BackColor="#FFDB91" />
                </asp:GridView>
            </div>


  <div class="form-sec-row">
                        &nbsp;&nbsp;&nbsp;<label><strong>&nbsp;</strong></label>
                        <asp:Button ID="Button1" runat="server" CssClass="submit-button"    Height="28px"
                            Text="Submit" onclick="Button1_Click" />
                        <asp:Button ID="Button2" runat="server" CssClass="submit-button"    Height="28px"
                            Text="Cancel" onclick="Button2_Click" />
                        <div class="clear">
                        </div>
                    </div>

          
            </div>
       </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>