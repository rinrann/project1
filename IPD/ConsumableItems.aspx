<%@ Page Language="C#" MasterPageFile="~/MasterAll/MasterPageAll.master" AutoEventWireup="true" CodeFile="ConsumableItems.aspx.cs" Inherits="Master_ConsumableItems" %>
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
        $("input[id$='txtConItemName']").focus(function () {
            $("input[id$='txtConItemName']").addClass('textboxborder');
        });
        $("input[id$='txtConItemName']").blur(function () {
            $("input[id$='txtConItemName']").removeClass('textboxborder');
        });
        $("input[id$='Button1']").click(function () {

            if ($("input[id$='txtConItemName']").val() == '') {
                alert('ConsumableItem Name Can not be Blank!');
                $("input[id$='txtConItemName']").focus();
                $("input[id$='txtConItemName']").addClass('textboxerr');
                return false;
            }
            else {
                $("input[id$='txtConItemName']").removeClass('textboxerr');
            }

            var e = document.getElementById("ctl00_ContentPlaceHolder1_DropDownList1");
            var strUser = e.options[e.selectedIndex].text;

            if (strUser == '--Select--') {
                alert('Select Unit');
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
        document.getElementById("ctl00_ContentPlaceHolder1_txtConItemName").value = regname[0];

        $("#txtConItemName").focus();
        //$("#DropDownList4").val(regname[1]);
    }
</script>

</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <!-- Form Section html start -->

    
 
  <asp:UpdateProgress ID="UpdateProgress1" runat="server">
    <ProgressTemplate>
    
       <div id="aaa" class="progressBackgroundFilter">          </div>
        <div id="bbb" class="processMessage"><img alt="Loading" src="../images/pwait.gif"/>
      
              </div>
    </ProgressTemplate>
    </asp:UpdateProgress>


    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <div class="pageheader">
                <asp:Label ID="Label1" runat="server">Consumable Items Name</asp:Label>
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
           <%--         <div class="form-sec-row">
                        <label>
                        <strong>Item ID :</strong></label>
                        <asp:TextBox ID="txtConItemId" runat="server" CssClass="textbox-medium1" 
                            Enabled="False" ></asp:TextBox>
                        <div class="clear">
                        </div>
                    </div>--%>

                       <div class="form-sec-row" style="display:none;">
                        <label>
                        <strong>
                        Consumable Group :</strong></label>
                         <asp:DropDownList ID="DropDownList2" runat="server" CssClass="textbox-medium1" >
                         </asp:DropDownList>
                        <div class="clear">
                        </div>
                    </div>

                    <div class="form-sec-row">
                        <label>
                        <strong>
                        Item Name :</strong></label>
                        <asp:TextBox ID="txtConItemName" runat="server" CssClass="textbox-medium1" ></asp:TextBox>
                        <cc1:AutoCompleteExtender ServiceMethod="SearchconItem" OnClientItemSelected="autoCompleteEx_ItemSelected" MinimumPrefixLength="1"
                                    CompletionInterval="100" EnableCaching="false" CompletionSetCount="10" TargetControlID="txtConItemName" ID="AutoCompleteExtender2" runat="server" 
                                       FirstRowSelected = "false" ></cc1:AutoCompleteExtender>
                        <div class="clear">
                        </div>
                    </div>
                     <div class="form-sec-row">
                        <label>
                        <strong>
                        Unit :</strong></label>
                         <asp:DropDownList ID="DropDownList1" runat="server" CssClass="textbox-medium1" >
                         </asp:DropDownList>
                        <div class="clear">
                        </div>
                    </div>
                    <div class="form-sec-row">
                    <label>
                    <strong>Buying Price/Unit</strong>
                    </label>
                    <asp:TextBox ID="txtPricePerUnit" runat="server" CssClass="textbox-medium1"></asp:TextBox>
                    <div class="clear">
                    </div>
                    </div>

                        <div class="form-sec-row">
                    <label>
                    <strong>Billing Price/Unit</strong>
                    </label>
                    <asp:TextBox ID="txtprice" runat="server" CssClass="textbox-medium1"></asp:TextBox>
                    <div class="clear">
                    </div>
                    </div>
                 
                    <div class="form-sec-row">
                        <div class="clear">
                        </div>
                    </div>
                    <div class="form-sec-row">
                        &nbsp;&nbsp;&nbsp;<label><strong>&nbsp;</strong></label>
                        <asp:Button ID="Button1" runat="server" CssClass="submit-button"   Height="30px"
                            Text="Submit" onclick="Button1_Click"   />
                        <asp:Button ID="Button2" runat="server" CssClass="submit-button"   Height="30px"
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
   <td style='width:120px;'  align="center">Consumable Item</td>
    <td style='width:50px;' align="center">Unit</td>
     <td style='width:90px;' align="center">Buying Price/Unit</td>
      <td style='width:90px;' align="center">Billing Price/Unit</td> 
        <td style='width:70px;' align="center">Edit</td>
          <td style='width:70px;' align="center" id="coldel" runat="server">Delete</td>
          </tr>
  </table> 
  </div> 

            <div class="listing" style='height:500px; width:100%; overflow:auto;'>
                <asp:GridView id="GridView1"  Width="100%" CssClass="grid" PagerStyle-CssClass="pgr"
                 DataKeyNames ="ConItemID" runat="server"  PageSize="1000"
                 AutoGenerateColumns="False" AllowPaging="True"  ShowHeader="false"
                 OnRowCommand="GridView1_RowCommand" OnRowDataBound="GridView1_RowDataBound" SelectedRowStyle-BackColor="GreenYellow"
                 OnPageIndexChanging="GridView1_PageIndexChanging" 
                    onrowdeleting="GridView1_RowDeleting" > 
                    <Columns> 
                       
                         
                        <asp:TemplateField HeaderText="Item ID" Visible="false">
                            <ItemTemplate>
                                <asp:Label ID="ConItemID" runat="server" Text='<%# Bind("ConItemID") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Item Name" ItemStyle-Width="120px"   ItemStyle-HorizontalAlign="Center"  >
                            <ItemTemplate>
                                <asp:Label ID="ConItemName" runat="server" Text='<%# Bind("ConItemName") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Unit ID" Visible="false">
                            <ItemTemplate>
                                <asp:Label ID="UnitID" runat="server" Text='<%# Bind("UnitID") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                         <asp:TemplateField HeaderText="Unit Name" ItemStyle-Width="50px"   ItemStyle-HorizontalAlign="Center"  >
                            <ItemTemplate>
                                <asp:Label ID="UnitName" runat="server" Text='<%# Bind("UnitName") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>    
                        <asp:TemplateField HeaderText="Buying Price/Unit" ItemStyle-Width="90px"   ItemStyle-HorizontalAlign="Center"  >
                            <ItemTemplate>
                                <asp:Label ID="PricePerUnit" runat="server" Text='<%# Bind("BuyingPrice") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField> 

                        <asp:TemplateField HeaderText="Billing Price/Unit" ItemStyle-Width="90px"   ItemStyle-HorizontalAlign="Center"  >
                            <ItemTemplate>
                                <asp:Label ID="lblprice" runat="server" Text='<%# Bind("BillingPrice") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField> 
                                    
                        <asp:CommandField SelectText="Edit" ShowSelectButton="True" ItemStyle-Width="70px"   ItemStyle-HorizontalAlign="Center"   ControlStyle-CssClass="temp" HeaderText="Edit" SelectImageUrl="~/Images/edit.png" ButtonType="Image">
                            <ControlStyle CssClass="temp"></ControlStyle>
                        </asp:CommandField>
                        <asp:CommandField ShowDeleteButton="True" ControlStyle-CssClass="temp" ItemStyle-Width="70px"   ItemStyle-HorizontalAlign="Center"   HeaderText="Delete" DeleteImageUrl="~/Images/delete.png" ButtonType="Image">
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

