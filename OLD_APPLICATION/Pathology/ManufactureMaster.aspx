<%@ Page Title="" Language="C#" MasterPageFile="~/MasterAll/MasterPageAll.master" AutoEventWireup="true" CodeFile="ManufactureMaster.aspx.cs" Inherits="Pathology_ManufactureMaster" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
 

 <script language="javascript" type="text/javascript">
     function CheckNumber(evt) {
         var charcode = (evt.which) ? evt.which : event.keycode;
         if (charcode > 31 && (charcode < 48 || charcode > 57)) {
             alert("Only Numbers..");
             return false;
            
         } else {
             return true;
         }
     }


     function ValidateEmail(email) {
         var expr = /^([\w-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([\w-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$/;
         return expr.test(email);
     }

     function Calling() {

         $("input[id$='txtph1']").keydown(function (event) {
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

         $("input[id$='txtph2']").keydown(function (event) {
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


         $("input[id$='Button1']").click(function () {


             if ($("input[id$='txtemail']").val() == '') {
                 $("input[id$='txtemail']").removeClass('textboxerr');
             }
             else
                 if (!ValidateEmail($("input[id$='txtemail']").val())) {
                     alert("Invalid Email Address !");
                     $("input[id$='txtemail']").focus();
                     $("input[id$='txtemail']").addClass('textboxerr');
                     return false;
                 }
                 else {
                     $("input[id$='txtemail']").removeClass('textboxerr');
                 }

             if ($("input[id$='txtname']").val() == '') {
                 alert('Please Enter Manufacturer Name !');
                 $("input[id$='txtname']").focus();
                 $("input[id$='txtname']").addClass('textboxerr');
                 return false;
             }
             else {
                 $("input[id$='txtname']").removeClass('textboxerr');
             }



             if ($("input[id$='txtaddress']").val() == '') {
                 alert('Please Enter Address !');
                 $("input[id$='txtaddress']").focus();
                 $("input[id$='txtaddress']").addClass('textboxerr');
                 return false;
             }
             else {
                 $("input[id$='txtaddress']").removeClass('textboxerr');
             }





             if ($("input[id$='txtph1']").val() == '') {
                 alert('Please Enter Phone 1 !');
                 $("input[id$='txtph1']").focus();
                 $("input[id$='txtph1']").addClass('textboxerr');
                 return false;
             }
             else {
                 $("input[id$='txtph1']").removeClass('textboxerr');
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
         document.getElementById("ctl00_ContentPlaceHolder1_txtname").value = regname[0];
        
         $("#txtname").focus();
         //$("#DropDownList4").val(regname[1]);
     }

     function autoCompleteEx_ItemSelected1(sender, args) {

         var regname = args.get_value().split('~');// alert(regname[0]);
         //document.getElementById("ctl00_ContentPlaceHolder1_TextBoxicode").value = regname[1];
         document.getElementById("ctl00_ContentPlaceHolder1_txtmanf").value = regname[0];

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
         <asp:Label ID="Label1" runat="server">Manufacturer Master</asp:Label>
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
                <label><strong>Manuafacturer Code :</strong></label>
               <asp:TextBox ID="txtcode" runat="server" CssClass="textbox-medium1" Enabled="False" 
                   ></asp:TextBox>
                <div class="clear">  </div>
            </div>
           
            <div class="form-sec-row">
                <label><strong>Manufacturer Name :</strong></label>
                <asp:TextBox ID="txtname" runat="server" CssClass="textbox-medium1" 
                   ></asp:TextBox>
                <cc1:AutoCompleteExtender ServiceMethod="SearchManuafacturer" OnClientItemSelected="autoCompleteEx_ItemSelected" MinimumPrefixLength="1"
                                    CompletionInterval="100" EnableCaching="false" CompletionSetCount="10" TargetControlID="txtname" ID="AutoCompleteExtender1" runat="server" 
                                       FirstRowSelected = "false" ></cc1:AutoCompleteExtender>
                <div class="clear"></div>
            </div>

            <div class="form-sec-row">
                <label><strong>Address:</strong></label>  
                 <asp:TextBox ID="txtaddress" runat="server" CssClass="textbox-medium1" 
                   ></asp:TextBox>
                <div class="clear"></div>
            </div>
            <div class="form-sec-row">
                <label><strong>Phone-1 :</strong></label>
                       <asp:TextBox ID="TextBox1" runat="server" Width="40px" CssClass="textbox-medium1" Text="+91" Enabled="false"
                   ></asp:TextBox>
                  <asp:TextBox ID="txtph1" Width="258px"  runat="server" CssClass="textbox-medium1" MaxLength="10" 
                   ></asp:TextBox>
                <div class="clear"></div>
            </div>
            <div class="form-sec-row">
                <label><strong>Phone-2 :</strong></label>
                       <asp:TextBox ID="TextBox2" runat="server" Width="40px" CssClass="textbox-medium1" Text="+91" Enabled="false"
                   ></asp:TextBox>
                 <asp:TextBox ID="txtph2" Width="258px"  runat="server" CssClass="textbox-medium1"  MaxLength="10" 
                   ></asp:TextBox>
                <div class="clear"></div>
            </div>
             <div class="form-sec-row">
                <label><strong>Email ID :</strong></label>
                 <asp:TextBox ID="txtemail" runat="server" CssClass="textbox-medium1" ></asp:TextBox>
                <div class="clear"></div>
            </div>
            <div class="form-sec-row">
                <label><strong>&nbsp;</strong></label>
                <asp:Button ID="Button1" Height="28px"  runat="server" OnClick="Button1_Click" Text="Submit" CssClass="submit-button" />
                <asp:Button ID="Button2" Height="28px"  runat="server" Text="Cancel" OnClick="Button2_Click" CssClass="submit-button" />
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
                               <td><asp:Label Width="100px" ID="lblComp" runat="server" >Manufacturar Name</asp:Label></td>
                               <td>
                                   <asp:TextBox ID="txtmanf" runat="server"   Width="" AutoPostBack="true" Height="" CssClass="textbox-medium1"></asp:TextBox>
                                   <cc1:AutoCompleteExtender ServiceMethod="SearchManuafacturer" OnClientItemSelected="autoCompleteEx_ItemSelected1" MinimumPrefixLength="1"
                                    CompletionInterval="100" EnableCaching="false" CompletionSetCount="10" TargetControlID="txtmanf" ID="AutoCompleteExtender2" runat="server" 
                                       FirstRowSelected = "false" ></cc1:AutoCompleteExtender>
                               </td>
                               <td>
                                   <asp:Button ID="Button3" runat="server" Text="Search"   Height="28px" CssClass="submit-button" onclick="Button3_Click" />
                               </td>
                            </tr>
                           
                        </table>
                </div>
            </div>
  <table width="100%" style='background-color:#FB7B13; color:#FFF;'>
  <tr style='border:1px solid black;'> 
   <td style='width:80px;' align="center">Code</td>
   <td style='width:110px;'  align="center">Manufacture Name</td>
    <td style='width:110px;' align="center">Manufacture Address</td>
     <td style='width:90px;' align="center">Phone-1</td>
      <td style='width:90px;' align="center"> Phone-2</td>
       <td style='width:90px;' align="center">Email-Id</td>
        <td style='width:70px;' align="center">Edit</td>
          <td style='width:70px;' align="center"  id="codel" runat="server">Delete</td>
          </tr>
  </table> 
  </div> 

     <div class="listing"   style='width:100%; height:500px; overflow:auto;'>
         <asp:GridView id="GridView1"  CssClass="grid" PagerStyle-CssClass="pgr" DataKeyNames ="MCode,MName" runat="server" AutoGenerateColumns="False" AllowPaging="True" 
         PageSize ="100"  ShowHeader="false"  OnRowDataBound="GridView1_RowDataBound"   OnPageIndexChanging="GridView1_PageIndexChanging"   SelectedRowStyle-BackColor="GreenYellow"
                 OnRowCommand="GridView1_RowCommand" 
             OnRowDeleting="GridView1_RowDeleting" Width="100%">
                <RowStyle HorizontalAlign="Center" />
                <Columns>
                    <asp:TemplateField HeaderText="Code" ItemStyle-Width="80px"   ItemStyle-HorizontalAlign="Center"  ><ItemTemplate><asp:Label ID="lblcode" runat="server" Text='<%# Bind("MCode") %>'></asp:Label></ItemTemplate></asp:TemplateField>
                    <asp:TemplateField HeaderText="Manufacture Name" ItemStyle-Width="110px"   ItemStyle-HorizontalAlign="Center"  ><ItemTemplate><asp:Label ID="lblname" runat="server" Text='<%# Bind("MName") %>'></asp:Label></ItemTemplate></asp:TemplateField>
                                       
                    <asp:TemplateField HeaderText="Address" ItemStyle-Width="110px"   ItemStyle-HorizontalAlign="Center"><ItemTemplate><asp:Label ID="lbladdress" runat="server" Text='<%# Bind("Address") %>'></asp:Label></ItemTemplate></asp:TemplateField> 
                       
                    <asp:TemplateField HeaderText="Phone - 1" ItemStyle-Width="90px"   ItemStyle-HorizontalAlign="Center"  ><ItemTemplate><asp:Label ID="lblphone1" runat="server" Text='<%# Bind("Ph1") %>'></asp:Label></ItemTemplate></asp:TemplateField>
                      <asp:TemplateField HeaderText="Phone - 2" ItemStyle-Width="90px"  ItemStyle-HorizontalAlign="Center" ><ItemTemplate><asp:Label ID="lblphone2" runat="server" Text='<%# Bind("Ph2") %>'></asp:Label></ItemTemplate></asp:TemplateField>
                                      <asp:TemplateField HeaderText="Email Id"  ItemStyle-Width="90px"   ItemStyle-HorizontalAlign="Center"  ><ItemTemplate><asp:Label ID="lbalmail" runat="server" Text='<%# Bind("Email") %>'></asp:Label></ItemTemplate></asp:TemplateField>         
                    <asp:CommandField SelectText="Edit" ShowSelectButton="True" ItemStyle-Width="70px"   ItemStyle-HorizontalAlign="Center"   ControlStyle-CssClass="temp" HeaderText="Edit" SelectImageUrl="~/Images/edit.png" ButtonType="Image"><ControlStyle CssClass="temp"></ControlStyle></asp:CommandField>
                    <asp:CommandField ShowDeleteButton="True" ControlStyle-CssClass="temp"  ItemStyle-Width="70px"   ItemStyle-HorizontalAlign="Center"  HeaderText="Delete" DeleteImageUrl="~/Images/delete.png" ButtonType="Image"><ControlStyle CssClass="temp"></ControlStyle></asp:CommandField>
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

