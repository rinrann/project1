<%@ Page Language="C#"MasterPageFile="~/MasterAll/MasterPageAll.master" AutoEventWireup="true" CodeFile="ChangePassword.aspx.cs" Inherits="Master_ChangePassword" %>


<script runat="server">

   
    
</script>
<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <!-- Form Section html start -->
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <div class="pageheader">
                <asp:Label ID="Label1" runat="server">Change Password</asp:Label>
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
                    <div class="form-sec-row">
                        <label><strong> 
                        User Id :</strong></label>
                        <asp:TextBox ID="txtUserId" runat="server" CssClass="textbox-medium1" 
                    Enabled="False" ></asp:TextBox>
                        <div class="clear">
                        </div>
                    </div>
                    <div class="form-sec-row">
                        <label><strong> 
                        User Name :</strong></label>
                        <asp:TextBox ID="txtUserName" runat="server" CssClass="textbox-medium1" 
                    Enabled="False" ></asp:TextBox>
                        <div class="clear">
                        </div>
                    </div>
                    <div class="form-sec-row">
                        <label>
                        <strong> 
                        Current Password :</strong></label>
                        <asp:TextBox ID="txtCrntPass" runat="server" CssClass="textbox-medium1" TextMode="Password" Text="" autocomplete="Off"></asp:TextBox>
                        <div class="clear">
                        </div>
                    </div>

                    <div class="form-sec-row">
                        <label>
                        <strong> 
                        New Password :</strong></label>
                        <asp:TextBox ID="txtNewPass" runat="server" CssClass="textbox-medium1" TextMode="Password" ></asp:TextBox>
                        <div class="clear">
                        </div>
                    </div>

                    <div class="form-sec-row">
                        <label>
                        <strong> 
                        Confirm Password :</strong></label>
                        <asp:TextBox ID="txtConfirmPass" runat="server" CssClass="textbox-medium1" TextMode="Password" ></asp:TextBox>
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
                            Text="Submit" onclick="btnsubmit_Click" />
                        <asp:Button ID="Button2" runat="server" CssClass="submit-button"   Height="28px"
                            Text="Cancel" />
                        <div class="clear">
                        </div>
                    </div>
                    <div class="clear">
                    </div>
                </div>
            </div>
             </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
