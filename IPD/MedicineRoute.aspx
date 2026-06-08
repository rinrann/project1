<%@ Page Language="C#" MasterPageFile="~/MasterAll/MasterPageAll.master" AutoEventWireup="true" CodeFile="MedicineRoute.aspx.cs" Inherits="Master_MedicineRoute" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

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
        $("input[id$='txtRouteName']").focus(function () {
            $("input[id$='txtRouteName']").addClass('textboxborder');
        });
        $("input[id$='txtRouteName']").blur(function () {
            $("input[id$='txtRouteName']").removeClass('textboxborder');
        });
        $("input[id$='Button1']").click(function () {
            if ($("input[id$='txtRouteName']").val() == '') {
                $("input[id$='txtRouteName']").addClass('textboxerr');
            }
            if ($("input[id$='txtRouteName']").val() == '') {
                alert('Please Enter Route name Properly!');
                $("input[id$='txtRouteName']").focus();
                $("input[id$='txtRouteName']").addClass('textboxerr');
                return false;
            }
            else {
                $("input[id$='txtRouteName']").removeClass('textboxerr');
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
                
             
    function SetContextKey() {
        $find('AutoCompleteExtender1').set_contextKey("GFC");
    }

    function autoCompleteEx_ItemSelected(sender, args) {

        var regname = args.get_value().split('~');// alert(regname[0]);
        //document.getElementById("ctl00_ContentPlaceHolder1_TextBoxicode").value = regname[1];
        document.getElementById("ctl00_ContentPlaceHolder1_txtRouteName").value = regname[0];

        $("#txtRouteName").focus();
        //$("#DropDownList4").val(regname[1]);
    }
</script>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <!-- Form Section html start -->
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <div class="pageheader">
                <asp:Label ID="Label1" runat="server">Medicine Route</asp:Label>
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
                        <label><strong>
                        Route ID :</strong></label>
                        <asp:TextBox ID="txtRouteId" runat="server" CssClass="textbox-medium1" 
                    Enabled="False" ></asp:TextBox>
                        <div class="clear">
                        </div>
                    </div>
                    <div class="form-sec-row">
                        <label><strong>
                        Route Name :</strong></label>
                        <asp:TextBox ID="txtRouteName" runat="server" CssClass="textbox-medium1" ></asp:TextBox>
                        <cc1:AutoCompleteExtender ServiceMethod="Searchroute" OnClientItemSelected="autoCompleteEx_ItemSelected" MinimumPrefixLength="1"
                                    CompletionInterval="100" EnableCaching="false" CompletionSetCount="10" TargetControlID="txtRouteName" ID="AutoCompleteExtender2" runat="server" 
                                       FirstRowSelected = "false" ></cc1:AutoCompleteExtender>
                        <div class="clear">
                        </div>
                    </div>

                    

                    <div class="form-sec-row">
                        <div class="clear">
                        </div>
                    </div>
                    <div class="form-sec-row">
                        &nbsp;&nbsp;&nbsp;<label><strong>&nbsp;</strong></label>
                        <asp:Button ID="Button1" runat="server" CssClass="submit-button"  Height="28px"
                            Text="Submit" onclick="Button1_Click"   />
                        <asp:Button ID="Button2" runat="server" CssClass="submit-button"  Height="28px"
                            Text="Cancel" onclick="Button2_Click"  />
                        <div class="clear">
                        </div>
                    </div>
                    <div class="clear">
                    </div>
                </div>
      
      </asp:View>
                    
                    <asp:View ID="View2" runat="server">
 
            <div class="listing"  style='width:100%; height:300px; overflow:auto;'>
                <asp:GridView id="GridView1"  Width="100%" CssClass="grid" PagerStyle-CssClass="pgr"
                 DataKeyNames ="RouteID" runat="server"  PageSize="100"
                 AutoGenerateColumns="False" AllowPaging="True" 
                 OnRowCommand="GridView1_RowCommand" OnRowDataBound="GridView1_RowDataBound" OnRowDeleting="GridView1_RowDeleting" 
                 OnPageIndexChanging="GridView1_PageIndexChanging" SelectedRowStyle-BackColor="GreenYellow">



                    <Columns>
                        <asp:TemplateField HeaderText="Route ID">
                            <ItemTemplate>
                                <asp:Label ID="RouteID" runat="server" Text='<%# Bind("RouteID") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Route Name">
                            <ItemTemplate>
                                <asp:Label ID="RouteName" runat="server" Text='<%# Bind("RouteName") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                         

                        <asp:CommandField SelectText="Edit" ShowSelectButton="True" ControlStyle-CssClass="temp" ItemStyle-HorizontalAlign="Center" HeaderText="Edit" SelectImageUrl="~/Images/edit.png" ButtonType="Image">
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

