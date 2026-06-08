<%@ Page Language="C#" MasterPageFile="~/MasterAll/MasterPageAll.master" AutoEventWireup="true" CodeFile="OperationDetails.aspx.cs" Inherits="Master_OperationDetails" %>
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
        $("input[id$='txtOperationName']").focus(function () {
            $("input[id$='txtOperationName']").addClass('textboxborder');
        });
        $("input[id$='txtOperationName']").blur(function () {
            $("input[id$='txtOperationName']").removeClass('textboxborder');
        });
        $("input[id$='Button1']").click(function () {

            if ($("input[id$='txtOperationName']").val() == '') {
                alert('Operation Name Can not be Blank!');
                $("input[id$='txtOperationName']").focus();
                $("input[id$='txtOperationName']").addClass('textboxerr');
                return false;
            }
            else {
                $("input[id$='txtOperationName']").removeClass('textboxerr');
            }

            var e = document.getElementById("ctl00_ContentPlaceHolder1_DropDownList1");
            var strUser = e.options[e.selectedIndex].text;

            if (strUser == '--Select--') {
                alert('Select OperationType');
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

    function SetContextKey() {
        $find('AutoCompleteExtender1').set_contextKey("GFC");
    }

    function autoCompleteEx_ItemSelected(sender, args) {

        var regname = args.get_value().split('~');// alert(regname[0]);
        //document.getElementById("ctl00_ContentPlaceHolder1_TextBoxicode").value = regname[1];
        document.getElementById("ctl00_ContentPlaceHolder1_txtOperationName").value = regname[0];

        $("#txtOperationName").focus();
        //$("#DropDownList4").val(regname[1]);
    }
</script>

</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <!-- Form Section html start -->
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <div class="pageheader">
                <asp:Label ID="Label1" runat="server">Operation Details</asp:Label>
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
                        <strong>Operation ID :</strong></label>
                        <asp:TextBox ID="txtOperationId" runat="server" CssClass="textbox-medium1" 
                            Enabled="False" ></asp:TextBox>
                        <div class="clear">
                        </div>
                    </div>
                    <div class="form-sec-row">
                        <label>
                        <strong>
                       Operation Name :</strong></label>
                        <asp:TextBox ID="txtOperationName" runat="server" CssClass="textbox-medium1" ></asp:TextBox>
                        <cc1:AutoCompleteExtender ServiceMethod="Searchotname" OnClientItemSelected="autoCompleteEx_ItemSelected" MinimumPrefixLength="1"
                                    CompletionInterval="100" EnableCaching="false" CompletionSetCount="10" TargetControlID="txtOperationName" ID="AutoCompleteExtender2" runat="server" 
                                       FirstRowSelected = "false" ></cc1:AutoCompleteExtender>
                        <div class="clear">
                        </div>
                    </div>
                     <div class="form-sec-row">
                        <label>
                        <strong>
                        Operation Type :</strong></label>
                         <asp:DropDownList ID="DropDownList1" runat="server" CssClass="textbox-medium1" >
                         </asp:DropDownList>
                        <div class="clear">
                        </div>
                    </div>
                    <div class="form-sec-row">
                    <label>
                    <strong>Operation Cost :</strong>
                    </label>
                    <asp:TextBox ID="txtOperationCost" runat="server" CssClass="textbox-medium1"></asp:TextBox>
                    <div class="clear">
                    </div>
                    </div>

                    <div class="form-sec-row">
                    <label>
                    <strong>Operation Summary :</strong>
                    </label>
                    <asp:TextBox ID="txtOperationSummary" runat="server" CssClass="textbox-medium1"></asp:TextBox>
                    <div class="clear">
                    </div>
                    </div>

                    <div class="form-sec-row">
                    <label>
                    <strong>Duration :</strong>
                    </label>
                    <asp:TextBox ID="txtDuration" runat="server" CssClass="textbox-medium1"></asp:TextBox>
                    <div class="clear">
                    </div>
                    </div>
                    
                    <div class="form-sec-row">
                    <label>
                    <strong>Delivery :</strong>
                    </label>
                    <asp:CheckBox ID="chkDeli" runat="server" />
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
      <div style='width:100%;'>
  <table width="100%" style='background-color:#FB7B13; color:#FFF;'>
  <tr style='border:1px solid black;'> 
   <td style='width:110px;' align="center">Operation Name</td>
   <td style='width:110px;'  align="center">Operation Type</td>
    <td style='width:110px;' align="center">Cost</td>
     <td style='width:110px;' align="center">Summary</td>
      <td style='width:60px;' align="center">Duration</td> 
        <td style='width:70px;' align="center">Edit</td>
          <td style='width:70px;' align="center" id="coldel" runat="server">Delete</td>
          </tr>
  </table> 
  </div> 
            <div class="listing"  style='width:100%; height:300px; overflow:auto;'>
                <asp:GridView id="GridView1"  Width="100%" CssClass="grid" PagerStyle-CssClass="pgr"
                 DataKeyNames ="OperationID" runat="server"  ShowHeader="false"
                 AutoGenerateColumns="False" AllowPaging="True"  PageSize="100"
                 OnRowCommand="GridView1_RowCommand"  OnRowDataBound="GridView1_RowDataBound" SelectedRowStyle-BackColor="GreenYellow"
                 OnPageIndexChanging="GridView1_PageIndexChanging" 
                    onrowdeleting="GridView1_RowDeleting" > 
                    <Columns>
                        <asp:TemplateField HeaderText="Operation ID" Visible="false">
                            <ItemTemplate>
                                <asp:Label ID="OperationID" runat="server" Text='<%# Bind("OperationID") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Operation Name" ItemStyle-Width="110px"  ItemStyle-HorizontalAlign="Center"  >
                            <ItemTemplate>
                                <asp:Label ID="OperationName" runat="server" Text='<%# Bind("OperationName") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="OperationType ID" Visible="false">
                            <ItemTemplate>
                                <asp:Label ID="OperationTypeID" runat="server" Text='<%# Bind("OperationTypeID") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                         <asp:TemplateField HeaderText="OperationType Name" ItemStyle-Width="110px"  ItemStyle-HorizontalAlign="Center"  >
                            <ItemTemplate>
                                <asp:Label ID="OperationTypeName" runat="server" Text='<%# Bind("OperationTypeName") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>    
                        <asp:TemplateField HeaderText="Operation Cost" ItemStyle-Width="110px"  ItemStyle-HorizontalAlign="Center"  >
                            <ItemTemplate>
                                <asp:Label ID="OperationCost" runat="server" Text='<%# Bind("OperationCost") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField> 

                        <asp:TemplateField HeaderText="Operation Summary" ItemStyle-Width="110px"  ItemStyle-HorizontalAlign="Center"  >
                            <ItemTemplate>
                                <asp:Label ID="OperationSummary" runat="server" Text='<%# Bind("OperationSummary") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="Duration" ItemStyle-Width="60px"  ItemStyle-HorizontalAlign="Center"  >
                            <ItemTemplate>
                                <asp:Label ID="Duration" runat="server" Text='<%# Bind("Duration") %>'></asp:Label>
                                <asp:Label ID="deliverytype" runat="server" Text='<%# Bind("Delivery") %>' Visible="false"></asp:Label>
                            </ItemTemplate>

                        </asp:TemplateField>
                                
                        <asp:CommandField SelectText="Edit" ShowSelectButton="True" ItemStyle-Width="70px"  ItemStyle-HorizontalAlign="Center"   ControlStyle-CssClass="temp" HeaderText="Edit" SelectImageUrl="~/Images/edit.png" ButtonType="Image">
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
