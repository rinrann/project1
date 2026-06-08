<%@ Page Language="C#" MasterPageFile="~/MasterAll/MasterPageAll.master" AutoEventWireup="true" CodeFile="UserAction.aspx.cs" Inherits="Master_UserAction" %>



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
        $("input[id$='txtActionName']").focus(function () {
            $("input[id$='txtActionName']").addClass('textboxborder');
        });
        $("input[id$='txtActionName']").blur(function () {
            $("input[id$='txtActionName']").removeClass('textboxborder');
        });
        $("input[id$='Button1']").click(function () {
            if ($("input[id$='txtActionName']").val() == '') {
                $("input[id$='txtActionName']").addClass('textboxerr');
            }
            if ($("input[id$='txtActionName']").val() == '') {
                alert('Action Name Can not be Blank!');
                $("input[id$='txtActionName']").focus();
                $("input[id$='txtActionName']").addClass('textboxerr');
                return false;
            }
            else {
                $("input[id$='txtActionName']").removeClass('textboxerr');
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
    <!-- Form Section html start -->
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <div class="pageheader">
                <asp:Label ID="Label1" runat="server">User Action</asp:Label>
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
               <%--     <div class="form-sec-row">
                        <label><strong>
                        Action ID :</strong></label>
                        <asp:TextBox ID="txtActionId" runat="server" CssClass="textbox-medium1" 
                    Enabled="False" ></asp:TextBox>
                        <div class="clear">
                        </div>
                    </div>--%>
                    <div class="form-sec-row">
                        <label><strong>
                        Action Name :</strong></label>
                        <asp:TextBox ID="txtActionName" runat="server" CssClass="textbox-medium1" ></asp:TextBox>
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
                            Text="Submit" onclick="Button1_Click1" />
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
<div class="listing" style='height:250px; width:100%; overflow:auto;'>
                <asp:GridView id="GridView1"  Width="100%" CssClass="grid" PagerStyle-CssClass="pgr"
                 DataKeyNames ="ActionID" runat="server"  PageSize="1000"
                 AutoGenerateColumns="False" AllowPaging="True" 
                 OnRowCommand="GridView1_RowCommand"  OnRowDeleting="GridView1_RowDeleting" 
                 OnPageIndexChanging="GridView1_PageIndexChanging" >
                    <Columns>
                        <asp:TemplateField HeaderText="Action ID" Visible="false">
                            <ItemTemplate>
                                <asp:Label ID="ActionID" runat="server" Text='<%# Bind("ActionID") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Action Name">
                            <ItemTemplate>
                                <asp:Label ID="ActionName" runat="server" Text='<%# Bind("ActionName") %>'></asp:Label>
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

