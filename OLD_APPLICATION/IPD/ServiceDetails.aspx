<%@ Page Language="C#" MasterPageFile="~/MasterAll/MasterPageAll.master" AutoEventWireup="true" CodeFile="ServiceDetails.aspx.cs" Inherits="Master_ServiceDetails" %>


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
        $("input[id$='txtServiceName']").focus(function () {
            $("input[id$='txtServiceName']").addClass('textboxborder');
        });
        $("input[id$='txtServiceName']").blur(function () {
            $("input[id$='txtServiceName']").removeClass('textboxborder');
        });
        $("input[id$='Button1']").click(function () {

            if ($("input[id$='txtServiceName']").val() == '') {
                alert('Service Name Can not be Blank!');
                $("input[id$='txtServiceName']").focus();
                $("input[id$='txtServiceName']").addClass('textboxerr');
                return false;
            }
            else {
                $("input[id$='txtServiceName']").removeClass('textboxerr');
            }

            var e = document.getElementById("ctl00_ContentPlaceHolder1_DropDownList1");
            var strUser = e.options[e.selectedIndex].text;

            if (strUser == '--Select--') {
                alert('Select ServiceCategory');
                $(e).focus();
                $(e).addClass('textboxerr');
                return false;
            }
            else {
                $(e).removeClass('textboxerr');
            }
            var e = document.getElementById("ctl00_ContentPlaceHolder1_DropDownList2");
            var strUser = e.options[e.selectedIndex].text;

            if (strUser == '--Select--') {
                alert('Select ServiceProvider');
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
<%--  <asp:UpdateProgress ID="UpdateProgress1" runat="server">
    <ProgressTemplate>
    
       <div id="aaa" class="progressBackgroundFilter">          </div>
        <div id="bbb" class="processMessage"><img alt="Loading" src="../images/pwait.gif"/>
      
              </div>
    </ProgressTemplate>
    </asp:UpdateProgress>--%>
    <%--For Busy Loader End.............................--%>

    <!-- Form Section html start -->
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <div class="pageheader">
                <asp:Label ID="Label1" runat="server">Service Details</asp:Label>
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
                    <div class="form-sec-row"  style='display:none;'>
                        <label>
                        <strong>Service ID :</strong></label>
                        <asp:TextBox ID="txtServiceId" runat="server" CssClass="textbox-medium1" 
                            Enabled="False" ></asp:TextBox>
                        <div class="clear">
                        </div>
                    </div>
                    <div class="form-sec-row">
                        <label>
                        <strong>
                        Service Name :</strong></label>
                        <asp:TextBox ID="txtServiceName" runat="server" CssClass="textbox-medium1" ></asp:TextBox>
                        <div class="clear">
                        </div>
                    </div>
                     <div class="form-sec-row" >
                        <label>
                        <strong>
                        Service Category :</strong></label>
                         <asp:DropDownList ID="DropDownList1" runat="server" CssClass="textbox-medium1" >
                         </asp:DropDownList>
                        <div class="clear">
                        </div>
                    </div>

                     <div class="form-sec-row">
                        <label>
                        <strong>
                        Service Provider :</strong></label>
                         <asp:DropDownList ID="DropDownList2" runat="server" CssClass="textbox-medium1" >
                         </asp:DropDownList>
                        <div class="clear">
                        </div>
                    </div>

                    
                         <div class="form-sec-row">
                        <label>
                        <strong>
                        Quantity :</strong></label>
                        <asp:TextBox ID="txtQuantity" runat="server" CssClass="textbox-medium1" ></asp:TextBox>
                        <div class="clear">
                        </div>
                    </div>

                    <div class="form-sec-row">
                        <label>
                        <strong>
                        Charges / Quantity :</strong></label>
                        <asp:TextBox ID="txtcharges" runat="server" CssClass="textbox-medium1" ></asp:TextBox>
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
                            Text="Submit" onclick="Button1_Click"  />
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
   <td style='width:90px;' align="center">Ser. Name</td>
   <td style='width:90px;'  align="center">Ser. Category</td>
    <td style='width:110px;' align="center">Service Provider</td>
     <td style='width:45px;' align="center">Quantity</td>
       <td style='width:45px;' align="center">Charges</td>     
        <td style='width:40px;' align="center">Edit</td>
          <td style='width:40px;' align="center">Delete</td>
          </tr>
  </table> 
  </div> 

            <div class="listing"   style='width:100%; height:300px; overflow:auto;'>
                <asp:GridView id="GridView1"  Width="100%" CssClass="grid" PagerStyle-CssClass="pgr"
                 DataKeyNames ="ServiceID" runat="server" 
                 AutoGenerateColumns="False" AllowPaging="True"  PageSize="100" ShowHeader="false"
                 OnRowCommand="GridView1_RowCommand"  OnRowDeleting="GridView1_RowDeleting" SelectedRowStyle-BackColor="GreenYellow"
                 OnPageIndexChanging="GridView1_PageIndexChanging" >
                    <Columns>
                        <asp:TemplateField HeaderText="Service ID" Visible="false">
                            <ItemTemplate>
                                <asp:Label ID="ServiceID" runat="server" Text='<%# Bind("ServiceID") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Service Name"  ItemStyle-Width="90px"  ItemStyle-HorizontalAlign="Center"  >
                            <ItemTemplate>
                                <asp:Label ID="ServiceName" runat="server" Text='<%# Bind("ServiceName") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="ServiceCategory ID" Visible="false">
                            <ItemTemplate>
                                <asp:Label ID="ServiceCategoryID" runat="server" Text='<%# Bind("ServiceCategoryID") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                         <asp:TemplateField HeaderText="Service Category Name"  ItemStyle-Width="90px"  ItemStyle-HorizontalAlign="Center"  >
                            <ItemTemplate>
                                <asp:Label ID="ServiceCategoryName" runat="server" Text='<%# Bind("ServiceCategoryName") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>         
                         <asp:TemplateField HeaderText="ServiceProvider ID" Visible="false">
                            <ItemTemplate>
                                <asp:Label ID="ServiceProviderID" runat="server" Text='<%# Bind("ServiceProviderID") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                         <asp:TemplateField HeaderText="Service Provider Name"  ItemStyle-Width="110px"  ItemStyle-HorizontalAlign="Center"  >
                            <ItemTemplate>
                                <asp:Label ID="ServiceProviderName" runat="server" Text='<%# Bind("ServiceProviderName") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>    

                        <asp:TemplateField HeaderText="Quantity"  ItemStyle-Width="45px"  ItemStyle-HorizontalAlign="Center"  >
                            <ItemTemplate>
                                <asp:Label ID="Quantity" runat="server" Text='<%# Bind("Quantity") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField> 

                              <asp:TemplateField HeaderText="Charges"  ItemStyle-Width="45px"  ItemStyle-HorizontalAlign="Center"  >
                            <ItemTemplate>
                                <asp:Label ID="Charges" runat="server" Text='<%# Bind("Charges") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField> 
                               
                        <asp:CommandField SelectText="Edit" ShowSelectButton="True"  ItemStyle-Width="40px"  ItemStyle-HorizontalAlign="Center"    ControlStyle-CssClass="temp" HeaderText="Edit" SelectImageUrl="~/Images/edit.png" ButtonType="Image">
                            <ControlStyle CssClass="temp"></ControlStyle>
                        </asp:CommandField>
                        <asp:CommandField ShowDeleteButton="True" ControlStyle-CssClass="temp"   ItemStyle-HorizontalAlign="Center"   ItemStyle-Width="40px" HeaderText="Delete" DeleteImageUrl="~/Images/delete.png" ButtonType="Image">
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