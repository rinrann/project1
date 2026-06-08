<%@ Page Title="" Language="C#" MasterPageFile="~/MasterAll/MasterPageAll.master" AutoEventWireup="true" CodeFile="ReagentMaster.aspx.cs" Inherits="Pathology_ReagentMaster" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
 
<script type="text/javascript" language="javascript">
    function DisableBackButton() {
        window.history.forward()
    }
    DisableBackButton();
    window.onload = DisableBackButton;
    window.onpageshow = function (evt) { if (evt.persisted) DisableBackButton() }
    window.onunload = function () { void (0) }

    function Calling() {
        $("input[id$='Button1']").click(function () {
            if ($("input[id$='txtname']").val() == '') {
                $("input[id$='txtname']").addClass('textboxerr');
            }

            if ($("input[id$='txtname']").val() == '') {
                alert('Please Enter Reagent Name !');
                $("input[id$='txtname']").focus();
                $("input[id$='txtname']").addClass('textboxerr');
                return false;
            }
            else {
                $("input[id$='txtname']").removeClass('textboxerr');
            }
            if ($("select[id$='DropDownList1']").val() == '0') {
                alert('Select Unit!');
                $("select[id$='DropDownList1']").focus();
                $("select[id$='DropDownList1']").addClass('textboxerr');
                return false;
            }
            else {
                $("select[id$='DropDownList1']").removeClass('textboxerr');
            }

            if ($("input[id$='txtperunit']").val() == '') {
                $("input[id$='txtperunit']").addClass('textboxerr');
            }

            if ($("input[id$='txtperunit']").val() == '') {
                alert('Please Enter Test Per Unit !');
                $("input[id$='txtperunit']").focus();
                $("input[id$='txtperunit']").addClass('textboxerr');
                return false;
            }
            else {
                $("input[id$='txtperunit']").removeClass('textboxerr');
            }



            if ($("input[id$='txtminstock']").val() == '') {
                $("input[id$='txtminstock']").addClass('textboxerr');
            }

            if ($("input[id$='txtminstock']").val() == '') {
                alert('Please Enter Minimum Stock !');
                $("input[id$='txtminstock']").focus();
                $("input[id$='txtminstock']").addClass('textboxerr');
                return false;
            }
            else {
                $("input[id$='txtminstock']").removeClass('textboxerr');
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
        document.getElementById("ctl00_ContentPlaceHolder1_txtReagent").value = regname[0];

        $("#txtReagent").focus();
        //$("#DropDownList4").val(regname[1]);
    }

    function autoCompleteEx_ItemSelected1(sender, args) {

        var regname = args.get_value().split('~');// alert(regname[0]);
        //document.getElementById("ctl00_ContentPlaceHolder1_TextBoxicode").value = regname[1];
        document.getElementById("ctl00_ContentPlaceHolder1_txtname").value = regname[0];

        $("#txtname").focus();
        //$("#DropDownList4").val(regname[1]);
    }
</script>


<%--For Busy Loader .............................--%>


 
  <asp:UpdateProgress ID="UpdateProgress1" runat="server">
    <ProgressTemplate>
    
       <div id="aaa" class="progressBackgroundFilter">          </div>
        <div id="bbb" class="processMessage"><img alt="Loading" src="../images/pwait.gif"/>
      
              </div>
    </ProgressTemplate>
    </asp:UpdateProgress>


    <%--For Busy Loader End.............................--%>

    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
    <ContentTemplate>
    <div class="pageheader">
         <asp:Label ID="Label1" runat="server">Reagent / Kit Master</asp:Label>
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
                <strong><asp:Label ID="lblError" runat="server" Font-Bold="True"></asp:Label>
                </strong>
                <div class="clear"></div>
            </div>
            <asp:HiddenField ID="TextBox4" runat="server" />
			<div class="form-sec-row">
                <label><strong>Reagent/Kit Code :</strong></label>
               <asp:TextBox ID="txtcode" runat="server" CssClass="textbox-medium1" Enabled="False" 
                   ></asp:TextBox>
                <div class="clear">  </div>
            </div>
           
            <div class="form-sec-row">
                <label><strong>Reagent/Kit Name :</strong></label>
                <asp:TextBox ID="txtname" runat="server" CssClass="textbox-medium1" 
                   ></asp:TextBox>
                 <cc1:AutoCompleteExtender ServiceMethod="SearchReagent" OnClientItemSelected="autoCompleteEx_ItemSelected1" MinimumPrefixLength="1"
                                    CompletionInterval="100" EnableCaching="false" CompletionSetCount="10" TargetControlID="txtname" ID="AutoCompleteExtender2" runat="server" 
                                       FirstRowSelected = "false" ></cc1:AutoCompleteExtender>
                
                <div class="clear"></div>
            </div>

            <div class="form-sec-row">
                <label><strong>Unit :</strong></label>
                <asp:DropDownList ID="DropDownList1" runat="server" CssClass="textbox-medium1">
                </asp:DropDownList>
                 <div class="clear"></div>
            </div>
            <div class="form-sec-row">
                <label><strong>Test Per Unit :</strong></label>
                  <asp:TextBox ID="txtperunit" runat="server" CssClass="textbox-medium1" MaxLength="10" Width="200px" onkeypress="return CheckNumber(event)"
                   ></asp:TextBox>
                <asp:DropDownList ID="DropDownList2" runat="server" Width="100" CssClass="textbox-medium1">
                </asp:DropDownList>
                <div class="clear"></div>
            </div>
            <div class="form-sec-row">
                <label><strong>Minimum Stock Level :</strong></label>
                 <asp:TextBox ID="txtminstock" runat="server" CssClass="textbox-medium1"  MaxLength="10" onkeypress="return CheckNumber(event)"
                   ></asp:TextBox>
                <div class="clear"></div>
            </div>
            <div class="form-sec-row">
                <label><strong>&nbsp;</strong></label>
                <asp:Button ID="Button1" runat="server"  Height="28px" OnClick="Button1_Click" Text="Submit" CssClass="submit-button" />
                <asp:Button ID="Button2" runat="server"  Height="28px"  Text="Cancel" OnClick="Button2_Click" CssClass="submit-button" />
                <div class="clear"></div>
            </div>  
   
     </div>
     </asp:View>

    <asp:View ID="View2" runat="server">

         <div style='width:100%;'>
            <div class="formbox" style="width:800px;" id="45">
                <div class="form-sec">
                       <table width="100%">
                           <tr>
                               <td><asp:Label Width="100px" ID="lblComp" runat="server" >Department Name</asp:Label></td>
                               <td>
                                   <asp:TextBox ID="txtReagent" runat="server"   Width=""  Height="" CssClass="textbox-medium1"></asp:TextBox>
                                    <cc1:AutoCompleteExtender ServiceMethod="SearchReagent" OnClientItemSelected="autoCompleteEx_ItemSelected" MinimumPrefixLength="1"
                                    CompletionInterval="100" EnableCaching="false" CompletionSetCount="10" TargetControlID="txtReagent" ID="AutoCompleteExtender1" runat="server" 
                                       FirstRowSelected = "false" ></cc1:AutoCompleteExtender>
                <div class="clear"></div>
                               </td>
                               <td>
                                   <asp:Button ID="Button3" runat="server" Text="Search"   Height="28px" CssClass="submit-button" onclick="Button3_Click" />
                               </td>
                            </tr>
                           
                        </table>
                </div>
            </div>
                           <div style='width:100%;'>
  <table width="100%" style='background-color:#FB7B13; color:#FFF;'>
  <tr style='border:1px solid black;'> 
   <td style='width:90px;'  align="center">Code</td>
      <td style='width:110px;'  align="center">Reagent/Kit Name</td>
     <td style='width:90px;' align="center">Unit</td>
      <td style='width:90px;' align="center">Test Per Unit</td>
       <td style='width:60px;' align="center">Minimum Stock</td>
        <td style='width:70px;' align="center">Edit</td>
          <td style='width:70px;' align="center">Delete</td>
          </tr>
  </table> 
  </div> 

     <div class="listing"    style='width:100%; height:450px; overflow:auto;'>
         <asp:GridView id="GridView1"  CssClass="grid" PagerStyle-CssClass="pgr" 
             DataKeyNames ="RCode,RName" runat="server" AutoGenerateColumns="False"   SelectedRowStyle-BackColor="GreenYellow"
             AllowPaging="True" PageSize ="100" Width="100%"  ShowHeader="false"
             onpageindexchanging="GridView1_PageIndexChanging" 
             onrowcommand="GridView1_RowCommand" onrowdeleting="GridView1_RowDeleting">
                <RowStyle HorizontalAlign="Center" />
                <Columns>
                    <asp:TemplateField HeaderText="Code" ItemStyle-Width="90px"   ItemStyle-HorizontalAlign="Center"  >
                        <ItemTemplate>
                            <asp:Label ID="lblcode" runat="server" Text='<%# Bind("RCode") %>'></asp:Label>
                            <asp:Label ID="Label2" runat="server" Text='<%# Bind("TestPerUnitText") %>' Visible="false"></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Reagent/Kit Name" ItemStyle-Width="110px"   ItemStyle-HorizontalAlign="Center"  >
                       
                        <ItemTemplate>
                            <asp:Label ID="lblname" runat="server" Text='<%# Bind("RName") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                                       
                    <asp:TemplateField HeaderText="Unit" ItemStyle-Width="90px"   ItemStyle-HorizontalAlign="Center"  >
                        <ItemTemplate>                        
                            <asp:Label ID="lblunit" runat="server" Text='<%# Bind("UnitName") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField> 
                       
                    <asp:TemplateField HeaderText="Test Per Unit" ItemStyle-Width="90px"   ItemStyle-HorizontalAlign="Center"  >
                        <ItemTemplate>                        
                            <asp:Label ID="lbltestper" runat="server" Text='<%# Bind("TestPerUnit") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                      <asp:TemplateField HeaderText="Minimum Stock"  ItemStyle-Width="60px"   ItemStyle-HorizontalAlign="Center"  >
                        <ItemTemplate>                        
                            <asp:Label ID="lblminstock" runat="server" Text='<%# Bind("MinStock") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                                          
                    <asp:CommandField SelectText="Edit" ShowSelectButton="True"  ItemStyle-Width="70px"   ItemStyle-HorizontalAlign="Center"  ControlStyle-CssClass="temp" HeaderText="Edit" SelectImageUrl="~/Images/edit.png" ButtonType="Image">
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

