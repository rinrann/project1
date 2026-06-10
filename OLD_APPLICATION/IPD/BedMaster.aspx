<%@ Page Language="C#"  MasterPageFile="~/MasterAll/MasterPageAll.master" AutoEventWireup="true" CodeFile="BedMaster.aspx.cs" Inherits="Master_BedMaster" %>
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

             $("input[id$='Button1']").click(function () {

                 if ($("select[id$='DropDownList1']").val() == '0') {
                     alert('Please Select Floor!');
                     $("select[id$='DropDownList1']").focus();
                     $("select[id$='DropDownList1']").addClass('textboxerr');
                     return false;
                 }
                 else {
                     $("select[id$='DropDownList1']").removeClass('textboxerr');
                 }

                 if ($("select[id$='DropDownList2']").val() == '0') {
                     alert('Please Select Wings!');
                     $("select[id$='DropDownList2']").focus();
                     $("select[id$='DropDownList2']").addClass('textboxerr');
                     return false;
                 }
                 else {
                     $("select[id$='DropDownList2']").removeClass('textboxerr');
                 }


                 if ($("select[id$='DropDownList3']").val() == '0') {
                     alert('Please Select Room Category!');
                     $("select[id$='DropDownList3']").focus();
                     $("select[id$='DropDownList3']").addClass('textboxerr');
                     return false;
                 }
                 else {
                     $("select[id$='DropDownList3']").removeClass('textboxerr');
                 }


                 if ($("select[id$='DropDownList4']").val() == '0') {
                     alert('Please Select Room!');
                     $("select[id$='DropDownList4']").focus();
                     $("select[id$='DropDownList4']").addClass('textboxerr');
                     return false;
                 }
                 else {
                     $("select[id$='DropDownList4']").removeClass('textboxerr');
                 }

                 if ($("input[id$='txtPatText']").val() == '') {
                     alert('Please Enter Pattern Type!');
                     $("input[id$='txtPatText']").focus();
                     $("input[id$='txtPatText']").addClass('textboxerr');
                     return false;
                 }
                 else {
                     $("input[id$='txtPatText']").removeClass('textboxerr');
                 }
             });

             //

             $("input[id$='TextBox2']").keydown(function (event) {
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

         function SetContextKey() {
             $find('AutoCompleteExtender1').set_contextKey("GFC");
         }

         function autoCompleteEx_ItemSelected(sender, args) {

             var regname = args.get_value().split('~');// alert(regname[0]);
             //document.getElementById("ctl00_ContentPlaceHolder1_TextBoxicode").value = regname[1];
             document.getElementById("ctl00_ContentPlaceHolder1_TextBox1").value = regname[0];

             $("#TextBox1").focus();
             //$("#DropDownList4").val(regname[1]);
         }
</script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">


 
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
                <asp:Label ID="Label1" runat="server">Bed Master</asp:Label>
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
                    <asp:HiddenField ID="HiddenField1" runat="server" Value="0" /><asp:HiddenField ID="HiddenField2" runat="server" Value="0" />
    
                      <div class="form-sec-row">
                        <label>
                        <strong>
                       Select Floor :</strong></label>
                         <asp:DropDownList ID="DropDownList1" runat="server" CssClass="textbox-medium1" 
                              AutoPostBack="true" 
                              onselectedindexchanged="DropDownList1_SelectedIndexChanged" >
                         </asp:DropDownList>
                        <div class="clear">
                        </div>
                    </div>

                      <div class="form-sec-row">
                        <label>
                        <strong>
                        Select Wings :</strong></label>
                        <asp:DropDownList ID="DropDownList2" runat="server" CssClass="textbox-medium1" 
                              AutoPostBack="True" onselectedindexchanged="DropDownList2_SelectedIndexChanged">                               
                         </asp:DropDownList>
                        <div class="clear">
                        </div>
                    </div>

                      <div class="form-sec-row">
                        <label>
                        <strong>
                        Select RoomCategory :</strong></label>
                        <asp:DropDownList ID="DropDownList3" runat="server" CssClass="textbox-medium1" 
                              AutoPostBack="true" onselectedindexchanged="DropDownList3_SelectedIndexChanged"
                              >
                               
                         </asp:DropDownList>
                        <div class="clear">
                        </div>
                    </div>

                     <div class="form-sec-row">
                        <label>
                        <strong>
                        Select Room :</strong></label>
                         <asp:DropDownList ID="DropDownList4" runat="server" CssClass="textbox-medium1" 
                             AutoPostBack="True" 
                             onselectedindexchanged="DropDownList4_SelectedIndexChanged" >
                         </asp:DropDownList>
                        <div class="clear">
                        </div>
                    </div>
                     
                     <div class="form-sec-row">
                        <label>
                        <strong> 
                        Pattern Prefix:</strong></label>
                        <asp:TextBox ID="txtPatText" runat="server" CssClass="textbox-medium1" 
                             onblur="PatternText()" Enabled="False"></asp:TextBox>
                        <div class="clear">
                        </div>
                    </div>

                            <div class="form-sec-row">
                        <label>
                        <strong> 
                        Bed No Text:</strong></label>
                        <asp:TextBox ID="TextBox1" runat="server" CssClass="textbox-medium1" ></asp:TextBox>
                                <cc1:AutoCompleteExtender ServiceMethod="Searchbedno" OnClientItemSelected="autoCompleteEx_ItemSelected" MinimumPrefixLength="1"
                                    CompletionInterval="100" EnableCaching="false" CompletionSetCount="10" TargetControlID="TextBox1" ID="AutoCompleteExtender2" runat="server" 
                                       FirstRowSelected = "false" ></cc1:AutoCompleteExtender>
                        <div class="clear">
                        </div>
                    </div>

                    
                            <div class="form-sec-row">
                        <label>
                        <strong> 
                        Charges:</strong></label>
                        <asp:TextBox ID="TextBox2"  MaxLength="5"  runat="server" CssClass="textbox-medium1" ></asp:TextBox>
                        <div class="clear">
                        </div>
                    </div>
            
                    <div class="form-sec-row">
                        <div class="clear">
                        </div>
                    </div>
                    <div class="form-sec-row">
                        &nbsp;&nbsp;&nbsp;<label><strong>&nbsp;</strong></label>
                        <asp:Button ID="Button1" runat="server" CssClass="submit-button"  Height="30px"
                            Text="Submit" onclick="Button1_Click" />
                        <asp:Button ID="Button2" runat="server" CssClass="submit-button"  Height="30px"
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
  <table width="960px" style='background-color:#FB7B13; color:#FFF;'>
  <tr style='border:1px solid black;'>
   <td style='width:120px;' align="center">Floor Name</td>
   <td style='width:80px;'  align="center">Wings Name</td>
    <td style='width:120px;' align="center">Room Category</td>
     <td style='width:120px;' align="center">Room</td>
       <td style='width:90px;' align="center" >Pattern Text</td>
      <td style='width:120px;' align="center">Bed No</td>
       <td style='width:120px;' align="center">Charges</td>
        <td style='width:65px;' align="center">Edit</td>
                <td style='width:90px;' align="center" id="coldel" runat="server">Delete</td>
          </tr>
  </table> 
  </div> 
            <div class="listing" style='width:100%; height:500px; overflow:auto;'>
                <asp:GridView id="GridView1"  Width="100%" CssClass="grid" PagerStyle-CssClass="pgr"
                 DataKeyNames ="BedNo" runat="server" 
                 AutoGenerateColumns="False" AllowPaging="True"  PageSize="100" ShowHeader="false"
                 OnRowCommand="GridView1_RowCommand"  OnRowDataBound="GridView1_RowDataBound" OnRowDeleting="GridView1_RowDeleting" SelectedRowStyle-BackColor="GreenYellow"
                 OnPageIndexChanging="GridView1_PageIndexChanging" >
                    <Columns>
                        <asp:TemplateField HeaderText="Bed No" Visible="false">
                            <ItemTemplate>
                                <asp:Label ID="BedNo" runat="server" Text='<%# Bind("BedNo") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
             
                        <asp:TemplateField HeaderText="Floor Name"  ItemStyle-HorizontalAlign="Center" ItemStyle-Width="120px">
                            <ItemTemplate>
                                <asp:Label ID="FloorName" runat="server" Text='<%# Bind("FloorName") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>


                         <asp:TemplateField HeaderText="Wings Name"  ItemStyle-HorizontalAlign="Center"  ItemStyle-Width="80px">
                            <ItemTemplate>
                                <asp:Label ID="WingsName" runat="server" Text='<%# Bind("WingsName") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>

                         <asp:TemplateField HeaderText="RoomCategory Name"  ItemStyle-HorizontalAlign="Center"  ItemStyle-Width="120px">
                            <ItemTemplate>
                                <asp:Label ID="RoomCategoryName" runat="server" Text='<%# Bind("RoomCategoryName") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="Room Name"  ItemStyle-HorizontalAlign="Center" ItemStyle-Width="120px">
                            <ItemTemplate>
                                <asp:Label ID="RoomName" runat="server" Text='<%# Bind("RoomName") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>

                         <asp:TemplateField HeaderText="Pattern Text"  ItemStyle-HorizontalAlign="Center"  ItemStyle-Width="90px">
                            <ItemTemplate>
                                <asp:Label ID="PatternText" runat="server" Text='<%# Bind("PatternText") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                             <asp:TemplateField HeaderText="Bed No"  ItemStyle-HorizontalAlign="Center" ItemStyle-Width="120px">
                            <ItemTemplate>
                                <asp:Label ID="lblbedtextno" runat="server" Text='<%# Bind("BedNoText") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>

                           <asp:TemplateField HeaderText="Charges"  ItemStyle-HorizontalAlign="Center" ItemStyle-Width="120px">
                            <ItemTemplate>
                                <asp:Label ID="lblcharges" runat="server" Text='<%# Bind("Charges") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                                        
                        <asp:CommandField SelectText="Edit" ShowSelectButton="True"  ItemStyle-HorizontalAlign="Center" ItemStyle-Width="65px" ControlStyle-CssClass="temp" HeaderText="Edit" SelectImageUrl="~/Images/edit.png" ButtonType="Image">
                            <ControlStyle CssClass="temp"></ControlStyle>
                        </asp:CommandField>
                        <asp:CommandField ShowDeleteButton="True" ControlStyle-CssClass="temp"  ItemStyle-HorizontalAlign="Center"  ItemStyle-Width="90px" HeaderText="Delete" DeleteImageUrl="~/Images/delete.png" ButtonType="Image">
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