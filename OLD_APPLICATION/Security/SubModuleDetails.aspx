<%@ Page Language="C#" MasterPageFile="~/MasterAll/MasterPageAll.master" AutoEventWireup="true" CodeFile="SubModuleDetails.aspx.cs" Inherits="Master_SubModuleDetails" %>



<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
 
<script language="javascript" type="text/javascript">
    function DisableBackButton() {
        window.history.forward()
    }
    DisableBackButton();
    window.onload = DisableBackButton;
    window.onpageshow = function (evt) { if (evt.persisted) DisableBackButton() }
    window.onunload = function () { void (0) }

    function Calling() {
        $("input[id$='txtSubModuleName']").focus(function () {
            $("input[id$='txtSubModuleName']").addClass('textboxborder');
        });
        $("input[id$='txtSubModuleName']").blur(function () {
            $("input[id$='txtSubModuleName']").removeClass('textboxborder');
        });
        $("input[id$='Button1']").click(function () {

            if ($("input[id$='txtSubModuleName']").val() == '') {
                alert('SubModule Name Can not be Blank!');
                $("input[id$='txtSubModuleName']").focus();
                $("input[id$='txtSubModuleName']").addClass('textboxerr');
                return false;
            }
            else {
                $("input[id$='txtSubModuleName']").removeClass('textboxerr');
            }

            var e = document.getElementById("ctl00_ContentPlaceHolder1_DropDownList1");
            var strUser = e.options[e.selectedIndex].text;

            if (strUser == '--Select--') {
                alert('Select Module');
                $(e).focus();
                $(e).addClass('textboxerr');
                return false;
            }
            else {
                $(e).removeClass('textboxerr');
            }

        });
    }
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
                
</script>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">

<%--For Busy Loader .............................--%>


 
  <asp:UpdateProgress ID="UpdateProgress1" runat="server">
    <ProgressTemplate>
    
       <div id="aaa" class="progressBackgroundFilter">          </div>
        <div id="bbb" class="processMessage"><img alt="Loading" src="../images/pwait.gif"/>
      
              </div>
    </ProgressTemplate>
    </asp:UpdateProgress>


    <%--For Busy Loader End.............................--%>


    <!-- Form Section html start -->
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <div class="pageheader">
                <asp:Label ID="Label1" runat="server">Sub Module Details</asp:Label>
            </div>
              <table width="290px" cellpadding="0" cellspacing="0">
         <tr>
            <td>
                <asp:Button Text="Entry" BorderStyle="None" ID="Tab1" Width="145px"  Height="40px" 
                    CssClass="Initial" runat="server" onclick="Tab1_Click"
                     /></td>
                     <td>
                <asp:Button Text="List" BorderStyle="None" ID="Tab2" CssClass="Initial"  Width="145px"  Height="40px" 
                    runat="server" onclick="Tab2_Click"
                    /></td>
           
                     </tr>
                     </table>
            <div class="formbox">
             <asp:MultiView ID="MainView" runat="server">
                    <asp:View ID="View1" runat="server">

                <div class="form-sec">
                    <div class="error">
                        <strong>
                        <asp:Label ID="lblError" runat="server" Font-Bold="True" CssClass="fadeout"></asp:Label>
                        </strong>
                        <div class="clear">
                        </div>
                    </div>
                    <asp:HiddenField ID="HiddenField1" runat="server" Value="0" />
               
                 <div class="form-sec-row">
                        <label>
                        <strong>
                        Module ID :</strong></label>
                         <asp:DropDownList ID="DropDownList1" runat="server" CssClass="textbox-medium1" >
                         </asp:DropDownList>
                        <div class="clear">
                        </div>
                    </div>

                    <div class="form-sec-row">
                        <label>
                        <strong>
                        Sub Module Name :</strong></label>
                        <asp:TextBox ID="txtSubModuleName" runat="server" CssClass="textbox-medium1" ></asp:TextBox>
                        <div class="clear">
                        </div>
                    </div>
                   
                 
                    <div class="form-sec-row">
                        <div class="clear">
                        </div>
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
                    <div class="clear">
                    </div>
                </div>
        
        </asp:View>
          <asp:View ID="View2" runat="server">

                 <div style='width:100%;'>
                <center>
         <table width="40%" style='background-color:#3AA8FC; color:#FFF;'>
         <tr>
         <td><strong>Module Details :</strong></td>
         <td> <asp:DropDownList ID="ddlModuleSearch" Width="180px" AutoPostBack="true"  
                 runat="server" 
                 onselectedindexchanged="ddlModuleSearch_SelectedIndexChanged">
             </asp:DropDownList></td>  
         </tr>
         </table>
       </center>
      </div>

          <div class="listing" style='height:500px; width:100%; overflow:auto;'>
                <asp:GridView id="GridView1"  Width="100%" CssClass="grid" PagerStyle-CssClass="pgr"
                 DataKeyNames ="SubModuleID" runat="server" 
                 AutoGenerateColumns="False" AllowPaging="True"  PageSize="1000" 
                 OnRowCommand="GridView1_RowCommand"  OnRowDeleting="GridView1_RowDeleting" SelectedRowStyle-BackColor="GreenYellow"
                 OnPageIndexChanging="GridView1_PageIndexChanging" >
                 <RowStyle HorizontalAlign="Center" />
                    <Columns>
                        <asp:TemplateField HeaderText="Sub Module ID" Visible="false">
                            <ItemTemplate>
                                <asp:Label ID="SubModuleID" runat="server" Text='<%# Bind("SubModuleID") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Sub Module Name">
                            <ItemTemplate>
                                <asp:Label ID="SubModuleName" runat="server" Text='<%# Bind("SubModuleName") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Module ID" Visible="false">
                            <ItemTemplate>
                                <asp:Label ID="ModuleID" runat="server" Text='<%# Bind("ModuleID") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                         <asp:TemplateField HeaderText="Module Name">
                            <ItemTemplate>
                                <asp:Label ID="ModuleName" runat="server" Text='<%# Bind("ModuleName") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>                
                        <asp:CommandField SelectText="Edit" ShowSelectButton="True" ControlStyle-CssClass="temp" HeaderText="Edit" SelectImageUrl="~/Images/edit.png" ButtonType="Image">
                            <ControlStyle CssClass="temp"></ControlStyle>
                        </asp:CommandField>
                        <asp:CommandField ShowDeleteButton="True" ControlStyle-CssClass="temp" HeaderText="Delete" DeleteImageUrl="~/Images/delete.png" ButtonType="Image">
                            <ControlStyle CssClass="temp"></ControlStyle>
                        </asp:CommandField>
                    </Columns>
                    <PagerStyle CssClass="pgr" />
                    <EditRowStyle BackColor="#CCFF33" />
                    <AlternatingRowStyle BackColor="#FFDB91" />
                </asp:GridView>
            </div>
            </asp:View>
            </asp:MultiView>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>