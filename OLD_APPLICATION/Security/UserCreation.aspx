<%@ Page Language="C#" MasterPageFile="~/MasterAll/MasterPageAll.master" AutoEventWireup="true" CodeFile="UserCreation.aspx.cs" Inherits="Master_UserCreation" %>

 <%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
 
       <script language="javascript" type="text/javascript">
        function DisableBackButton() {
        window.history.forward()
        }
        DisableBackButton();
        window.onload = DisableBackButton;
        window.onpageshow = function(evt) { if (evt.persisted) DisableBackButton() }
        window.onunload = function() { void (0) }
        function Calling() {
          
             var date = new Date();
                    $("input[id$='Calendar1']").datepicker({ dateFormat: 'dd/mm/yy', minDate: date });
              
                 $("input[id$='Button1']").click(function () {
                if ($("input[id$='txtUserId']").val() == '' && $("input[id$='txtUserName']").val() == '' && $("input[id$='txtphn12']").val() == '' && $("input[id$='txtphn13']").val() == '') {
                    $("input[id$='txtUserId']").addClass('textboxerr');
                    $("input[id$='txtUserName']").addClass('textboxerr');
                    $("input[id$='txtphn12']").addClass('textboxerr');
                    $("input[id$='txtphn13']").addClass('textboxerr');
                }


                if ($("input[id$='txtUserName']").val() == '') {
                    alert('Please Enter User name Properly!');
                    $("input[id$='txtUserName']").focus();
                    $("input[id$='txtUserName']").addClass('textboxerr');
                    return false;
                }
                else {
                    $("input[id$='txtUserName']").removeClass('textboxerr');
                }

                if ($("input[id$='txtUserId']").val() == '') {
                    alert('Please Enter UserId Properly!');
                    $("input[id$='txtUserId']").focus();
                    $("input[id$='txtUserId']").addClass('textboxerr');
                    return false;
                } else {
                    $("input[id$='txtUserId']").removeClass('textboxerr');
                }

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



                if ($("input[id$='txtphn13']").val() == '') {
                    alert('Please Enter Phone_1 Properly!');
                    $("input[id$='txtphn13']").focus();
                    $("input[id$='txtphn13']").addClass('textboxerr');
                    return false;
                }
                else if ($("input[id$='txtphn13']").val().length < '10') {
                    alert('Invalid Phone_1!');
                    $("input[id$='txtphn13']").focus();
                    $("input[id$='txtphn13']").addClass('textboxerr');
                    return false;
                }
                else {
                    $("input[id$='txtphn13']").removeClass('textboxerr');
                }



                if ($("input[id$='Calendar1']").val() == '') {
                    alert('Please Enter Expiry Date!');
                    $("input[id$='Calendar1']").focus();
                    $("input[id$='Calendar1']").addClass('textboxerr');
                    return false;
                }
             
                else {
                    $("input[id$='Calendar1']").removeClass('textboxerr');
                }





                

                if ($("input[id$='txtemail']").val() != '') {
                    var emailReg = /^([\w-\.]+@([\w-]+\.)+[\w-]{2,4})?$/;
                    if (!emailReg.test($("input[id$='txtemail']").val())) {
                        alert('Invalid Email Address!');
                        $("input[id$='txtemail']").focus();
                        $("input[id$='txtemail']").addClass('textboxerr');
                        return false;
                    }
                }
            });
  
            $("input[id$='txtphn13']").keydown(function (event) {
                if (event.keyCode == 46 || event.keyCode == 8 || event.keyCode == 9 || event.keyCode == 27 || event.keyCode == 13 ||
                    (event.keyCode == 65 && event.ctrlKey === true) || (event.keyCode >= 35 && event.keyCode <= 39)) {
                    return;
                }
                else {
                    if (event.shiftKey || (event.keyCode < 48 || event.keyCode > 57) && (event.keyCode < 96 || event.keyCode > 105)) {
                        event.preventDefault();
                    }
                }
            });
 

            $("input[id$='txtphn23']").keydown(function (event) {
                if (event.keyCode == 46 || event.keyCode == 8 || event.keyCode == 9 || event.keyCode == 27 || event.keyCode == 13 ||
                    (event.keyCode == 65 && event.ctrlKey === true) || (event.keyCode >= 35 && event.keyCode <= 39)) {
                    return;
                }
                else {
                    if (event.shiftKey || (event.keyCode < 48 || event.keyCode > 57) && (event.keyCode < 96 || event.keyCode > 105)) {
                        event.preventDefault();
                    }
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
                <asp:Label ID="Label1" runat="server">User Creation</asp:Label>
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
                        User Name :</strong></label>
                        <asp:TextBox ID="txtUserId" runat="server" CssClass="textbox-medium1"></asp:TextBox>
                        <div class="clear">
                        </div>
                    </div>
                    <div class="form-sec-row">
                        <label>
                        <strong>
                        Name :</strong></label>
                        <asp:TextBox ID="txtUserName" runat="server" CssClass="textbox-medium1" ></asp:TextBox>
                        <div class="clear">
                        </div>
                    </div>
                     <div class="form-sec-row">
                        <label>
                        <strong>
                        User Role :</strong></label>
                         <asp:DropDownList ID="DropDownList1" runat="server" CssClass="textbox-medium1" >
                         </asp:DropDownList>
                        <div class="clear">
                        </div>
                    </div>
                     <div class="form-sec-row">
                        <label>
                        <strong> 
                        Password :</strong></label>
                        <asp:TextBox ID="txtPass" runat="server" CssClass="textbox-medium1" TextMode="Password" ></asp:TextBox>
                        <div class="clear">
                        </div>
                    </div>
                    <div class="form-sec-row">
                    <label>
                    <strong>
                    Confirm Password :</strong>
                    </label>
                    <asp:TextBox ID="txtconfirmPass" runat="server" CssClass="textbox-medium1" TextMode="Password"></asp:TextBox>
                    <div class="clear">
                    </div>
                    </div>
                    <div class="form-sec-row">
                        <label>
                        <strong>
                            Admin:
                        </strong>
                        </label>
                        <asp:CheckBox runat="server" ID="chkAdmin" />
                        <div class="clear">
                        </div>
                    </div>
                    <div class="form-sec-row">
                    <label>
                    <strong>
                    Phone No-1 :</strong>
                    </label>
                    <asp:TextBox ID="txtphn11" runat="server" CssClass="textbox-medium1" Width="50" Enabled="False">+91</asp:TextBox> 
                    <asp:TextBox ID="txtphn13" runat="server" CssClass="textbox-medium1" Width="248px" MaxLength="10"></asp:TextBox>
                    <div class="clear">
                    </div>
                     </div>
                      <div class="form-sec-row">
                    <label>
                    <strong>
                    Phone No-2 :</strong></label>
                    <asp:TextBox ID="txtphn21" runat="server" CssClass="textbox-medium1" Width="50" Enabled="False">+91</asp:TextBox> 
                    <asp:TextBox ID="txtphn23" runat="server" CssClass="textbox-medium1" Width="248px" MaxLength="10"></asp:TextBox>
                    <div class="clear">
                    </div>
                     </div>
                     <div class="form-sec-row">
                    <label>
                    <strong>
                    Email Id :</strong></label>
                    <asp:TextBox ID="txtemail" runat="server" CssClass="textbox-medium1" 
                        MaxLength="50"></asp:TextBox>
                    <div class="clear"></div>
            </div>
            <div class="form-sec-row">
                <label>
                <strong>
                Expiry Date :</strong></label>
                 <asp:TextBox ID="Calendar1" runat="server" CssClass="textbox-medium1"> 
                 </asp:TextBox>
                <cc1:MaskedEditExtender ID="MaskedEditExtender1" runat="server" TargetControlID="Calendar1"
                                            Mask="99/99/9999" MessageValidatorTip="true" MaskType="Date" DisplayMoney="Left"
                                            AcceptNegative="Left" ErrorTooltipEnabled="True" CultureName="en-GB" />
                <asp:Label ID="Label2" runat="server" ForeColor="Green" Text="(DD/MM/YYYY)"></asp:Label>
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
                 DataKeyNames ="UserID" runat="server" 
                 AutoGenerateColumns="False" AllowPaging="True"  PageSize="100"
                 OnRowCommand="GridView1_RowCommand"  OnRowDeleting="GridView1_RowDeleting" SelectedRowStyle-BackColor="GreenYellow"
                 OnPageIndexChanging="GridView1_PageIndexChanging" >
                    <Columns>
                        <asp:TemplateField HeaderText="User ID">
                            <ItemTemplate>
                                <asp:Label ID="UserID" runat="server" Text='<%# Bind("UserID") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="User Name">
                            <ItemTemplate>
                                <asp:Label ID="UserName" runat="server" Text='<%# Bind("UserName") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="UserRole ID" Visible="false">
                            <ItemTemplate>
                                <asp:Label ID="UserRoleID" runat="server" Text='<%# Bind("UserRoleID") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>

                         <asp:TemplateField HeaderText="UserRole Name">
                            <ItemTemplate>
                                <asp:Label ID="UserRoleName" runat="server" Text='<%# Bind("UserRoleName") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>

                       

                        <asp:TemplateField HeaderText="Password" Visible="true">
                            <ItemTemplate>
                                <asp:Label ID="Password" runat="server" Text='<%# Bind("Password") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Phone No-1">
                            <ItemTemplate>
                                <asp:Label ID="PhoneNo_1" runat="server" Text='<%# Bind("PhoneNo_1") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Phone No-2">
                            <ItemTemplate>
                                <asp:Label ID="PhoneNo_2" runat="server" Text='<%# Bind("PhoneNo_2") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Email ID">
                            <ItemTemplate>
                                <asp:Label ID="EmailId" runat="server" Text='<%# Bind("EmailId") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                         <asp:TemplateField HeaderText="Expiry Date">
                            <ItemTemplate>
                                <asp:Label ID="ExpiryDate" runat="server" Text='<%# Bind("ExDate") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Admin">
                            <ItemTemplate>
                                <asp:Label ID="AdminFlag" runat="server" Text='<%# Bind("AdminUser") %>' Visible="false"></asp:Label>
                                <asp:Label ID="AdminFlagText" runat="server" Text='<%# Bind("AdminUserText") %>'></asp:Label>
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