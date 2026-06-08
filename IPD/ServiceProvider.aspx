<%@ Page Language="C#" MasterPageFile="~/MasterAll/MasterPageAll.master" AutoEventWireup="true" CodeFile="ServiceProvider.aspx.cs" Inherits="Master_ServiceProvider" %>
 
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

        $("input[id$='Button1']").click(function () {

            if ($("input[id$='txtServiceProviderName']").val() == '') {
                alert('ServiceProvider Name Can not be Blank!');
                $("input[id$='txtServiceProviderName']").focus();
                $("input[id$='txtServiceProviderName']").addClass('textboxerr');
                return false;
            }
            else {
                $("input[id$='txtServiceProviderName']").removeClass('textboxerr');
            }

            if ($("input[id$='txtAddress']").val() == '') {
                alert('Address Can not be Blank!');
                $("input[id$='txtAddress']").focus();
                $("input[id$='txtAddress']").addClass('textboxerr');
                return false;
            }
            else {
                $("input[id$='txtAddress']").removeClass('textboxerr');
            }



            if ($("input[id$='txtphn13']").val() == '') {
                alert('Please Enter Phone_1 Properly!');
                $("input[id$='txtphn13']").focus();
                $("input[id$='txtphn13']").addClass('textboxerr');
                return false;
            }        
            else {
                $("input[id$='txtphn13']").removeClass('textboxerr');
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
                <asp:Label ID="Label1" runat="server">Service Provider</asp:Label>
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
                        <strong>ServiceProvider ID :</strong></label>
                        <asp:TextBox ID="txtServiceProviderId" runat="server" CssClass="textbox-medium1" 
                            Enabled="False" ></asp:TextBox>
                        <div class="clear">
                        </div>
                    </div>
                    <div class="form-sec-row">
                        <label>
                        <strong>
                        ServiceProvider Name :</strong></label>
                        <asp:TextBox ID="txtServiceProviderName" runat="server" CssClass="textbox-medium1" ></asp:TextBox>
                        <div class="clear">
                        </div>
                    </div>
                     
                    <div class="form-sec-row">
                    <label>
                    <strong>Address</strong>
                    </label>
                    <asp:TextBox ID="txtAddress" runat="server" CssClass="textbox-medium1"></asp:TextBox>
                    <div class="clear">
                    </div>
                    </div>

                <div class="form-sec-row">
                <label>
                <strong>
                Phone No-1:</strong>
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
                Email :</strong></label>
                <asp:TextBox ID="txtemail" runat="server" CssClass="textbox-medium1" 
                    MaxLength="50"></asp:TextBox>
                <div class="clear"></div>
                 </div>
                 
                    <div class="form-sec-row">
                        <div class="clear">
                        </div>
                    </div>
                    <div class="form-sec-row">
                        &nbsp;&nbsp;&nbsp;<label><strong>&nbsp;</strong></label>
                        <asp:Button ID="Button1" runat="server" CssClass="submit-button"   Height="28px"
                            Text="Submit" onclick="Button1_Click"   />
                        <asp:Button ID="Button2" runat="server" CssClass="submit-button"   Height="28px"
                            Text="Cancel" onclick="Button2_Click"  />
                        <div class="clear">
                        </div>
                    </div>
                    <div class="clear">
                    </div>
                </div>
       </asp:View>
        <asp:View ID="View2" runat="server">
             <div style='width:100%;'>
  <table width="100%" style='background-color:#FB7B13; color:#FFF;'>
  <tr style='border:1px solid black;'> 
   <td style='width:125px;' align="center">Service Provider</td>
   <td style='width:125px;'  align="center">Address</td>
    <td style='width:110px;' align="center">Phone-1</td> 
      <td style='width:110px;' align="center">Email-Id</td> 
        <td style='width:70px;' align="center">Edit</td>
          <td style='width:70px;' align="center">Delete</td>
          </tr>
  </table> 
  </div> 
            <div class="listing"  style='width:100%; height:300px; overflow:auto;'>
                <asp:GridView id="GridView1"  Width="100%" CssClass="grid" PagerStyle-CssClass="pgr"
                 DataKeyNames ="ServiceProviderID" runat="server" 
                 AutoGenerateColumns="False" AllowPaging="True"  ShowHeader="false"
                 OnRowCommand="GridView1_RowCommand"   SelectedRowStyle-BackColor="GreenYellow" PageSize="100"
                 OnPageIndexChanging="GridView1_PageIndexChanging" 
                    onrowdeleting="GridView1_RowDeleting" > 
                    <Columns>
                        <asp:TemplateField HeaderText="ServiceProvider ID" Visible="false"> 
                            <ItemTemplate>
                                <asp:Label ID="ServiceProviderID" runat="server" Text='<%# Bind("ServiceProviderID") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="ServiceProvider Name"   ItemStyle-HorizontalAlign="Center"  ItemStyle-Width="125px" >
                            <ItemTemplate>
                                <asp:Label ID="ServiceProviderName" runat="server" Text='<%# Bind("ServiceProviderName") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                      
                           
                        <asp:TemplateField HeaderText="Address"  ItemStyle-HorizontalAlign="Center" ItemStyle-Width="125px"  >
                            <ItemTemplate>
                                <asp:Label ID="Address" runat="server" Text='<%# Bind("Address") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField> 

                        <asp:TemplateField HeaderText="Phone No 1"  ItemStyle-HorizontalAlign="Center"   ItemStyle-Width="110px">
                            <ItemTemplate>
                                <asp:Label ID="PhoneNo_1" runat="server" Text='<%# Bind("PhnNo_1") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Phone No 2"  ItemStyle-HorizontalAlign="Center" Visible="false"  ItemStyle-Width="110px" >
                            <ItemTemplate>
                                <asp:Label ID="PhoneNo_2" runat="server" Text='<%# Bind("PhnNo_2") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Email Id"  ItemStyle-HorizontalAlign="Center"  ItemStyle-Width="110px" >
                            <ItemTemplate>
                                <asp:Label ID="Email" runat="server" Text='<%# Bind("Email") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                                    
                        <asp:CommandField SelectText="Edit" ShowSelectButton="True"  ItemStyle-Width="70px" ItemStyle-HorizontalAlign="Center"   ControlStyle-CssClass="temp" HeaderText="Edit" SelectImageUrl="~/Images/edit.png" ButtonType="Image">
                            <ControlStyle CssClass="temp"></ControlStyle>
                        </asp:CommandField>
                        <asp:CommandField ShowDeleteButton="True" ControlStyle-CssClass="temp" ItemStyle-Width="70px"  ItemStyle-HorizontalAlign="Center"   HeaderText="Delete" DeleteImageUrl="~/Images/delete.png" ButtonType="Image">
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