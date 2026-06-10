<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ReagentPopup.aspx.cs" Inherits="Pathology_ReagentPopup" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>
      <link href="../css/style.css" rel="stylesheet" type="text/css" />
    <link href="../css/MyCss.css" rel="stylesheet" type="text/css" />
    <link href="../css/jquery.ui.timepicker.css" rel="stylesheet" type="text/css" />
    <script src="../css/jquery.timepicker.min.js" type="text/javascript"></script>
    <script src="../css/jquery.timepicker.js" type="text/javascript"></script>
    <link href="../css/jquery.timepicker.css" rel="stylesheet" type="text/css" />
    <link href="../css/jquery-ui.css" rel="stylesheet" type="text/css" />
    <link href="../css/jquery-ui-1.8.19.custom.css" rel="stylesheet" type="text/css" />
    <link href="../css/calendar-blue.css" rel="stylesheet" type="text/css" />
   
   
    <script src="../Script/jquery-1.4.1.min.js" type="text/javascript"></script>
      
   
    <script src="../Script/jquery.dynDateTime.min.js" type="text/javascript"></script>
    <script src="../Script/calendar-en.min.js" type="text/javascript"></script> 
    <script type="text/javascript" src="../Script/jquery-ui-1.8.19.custom.min.js"></script>
    <script type="text/javascript" src="../Script/menu.js"></script>
    <script type="text/javascript" src="../Script/jquery.ui.timepicker.js"></script>
 <script type="text/javascript">


     function CloseDialog() {

         var arg = new Object();

         
         arg.NameValue = document.getElementById('HiddenField1').value;
         arg.ProfessionValue = document.getElementById('HiddenField2').value;
         window.returnValue = arg;
         window.close();

     }
     //     function GetType() {

     //         var obj = window.dialogArguments;
     //         alert(obj);
     //         document.getElementById('HiddenField2')= obj;

     //     } 
     function Button1_onclick() {
         window.close();
     }

     $(document).ready(function () {
         var date = new Date();

         $("input[id$='TextBox5']").datepicker({ dateFormat: 'dd/mm/yy' });
         $("input[id$='TextBox6']").datepicker({ dateFormat: 'dd/mm/yy' });
     });


     function SetContextKey() {
         $find('AutoCompleteExtender1').set_contextKey("GFC");
     }
     function autoCompleteEx_ItemSelected(sender, args) {
         var regname = args.get_value().split('~'); //alert(regname[0]);
         //document.getElementById("ctl00_ContentPlaceHolder1_TextBox1").value = regname[1];
         document.getElementById("txtname").value = regname[0];
         $("#txtname").focus();
     }
     function autoCompleteEx_SupplierSelected(sender, args) {
         var regname = args.get_value().split('-'); //alert(regname[0]);
         //document.getElementById("ctl00_ContentPlaceHolder1_TextBox1").value = regname[1];
         document.getElementById("txtcompany").value = regname[0];
         $("#txtcompany").focus();
     }

    </script>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <asp:ScriptManager ID="ScriptManager1" runat="server">
        </asp:ScriptManager>
<%--For Busy Loader .............................--%>


 
  <asp:UpdateProgress ID="UpdateProgress1" runat="server">
    <ProgressTemplate>
    
       <div id="aaa" class="progressBackgroundFilter">          </div>
        <div id="bbb" class="processMessage"><img alt="Loading" src="../images/pwait.gif"/>
      
              </div>
    </ProgressTemplate>
    </asp:UpdateProgress>


    <%--For Busy Loader End.............................--%>
        <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Always">
        <ContentTemplate>
        <table width="100%">
          
       <tr>
                <td>
                    <div class="form-sec-row"> 
             <label class="pname" style="width:40%;float:left;"><strong>Reagent Name :</strong></label> 
                      <asp:TextBox ID="txtname" runat="server" CssClass="textbox-medium1" Width="50%" style="width:50%;float:left;"></asp:TextBox>  <div class="clear">
                          <cc1:AutoCompleteExtender ServiceMethod="SearchReagentName" OnClientItemSelected="autoCompleteEx_ItemSelected" MinimumPrefixLength="1"
                                    CompletionInterval="100" EnableCaching="false" CompletionSetCount="10" TargetControlID="txtname" ID="AutoCompleteExtender1" runat="server" 
                                       FirstRowSelected = "false" ></cc1:AutoCompleteExtender>
            </div>
            
</td>
                <td>
                    <div class="form-sec-row"> 
             <label class="pname"  style="width:40%;float:left;"><strong>Supplier Name :</strong></label> 
                        <asp:TextBox ID="txtcompany" runat="server" CssClass="textbox-medium1" Width="50%" style="width:50%;float:left;"></asp:TextBox>
                        <cc1:AutoCompleteExtender ServiceMethod="SearchSupplierName" OnClientItemSelected="autoCompleteEx_SupplierSelected" MinimumPrefixLength="1"
                                    CompletionInterval="100" EnableCaching="false" CompletionSetCount="10" TargetControlID="txtcompany"  ID="AutoCompleteExtender2" runat="server" 
                                       FirstRowSelected = "false" ></cc1:AutoCompleteExtender>
            </div>
</td>
                
                <td>
                                
</td>             
                      
            </tr>
               <tr>
                   <td>
                                    
                                     <div class="form-sec-row"> 
                                          <label class="pname"  style="width:40%;float:left;"><strong>From Date :</strong></label> 
                                    <asp:TextBox ID="TextBox5" CssClass="textbox-medium1" runat="server"   Width="120px" ></asp:TextBox>  
                                        </div>
                                </td>
                                <td>
                                    
                                     <div class="form-sec-row"> 
                                          <label class="pname"  style="width:40%;float:left;"><strong>To Date :</strong></label> 
                                   <asp:TextBox ID="TextBox6" CssClass="textbox-medium1" runat="server"   Width="120px" ></asp:TextBox>  
                                        </div>
                                </td>
                                
                   <td>
                       <div class="form-sec-row"> 
           <asp:Button ID="Button2" runat="server" Text="Search"  Height="28px" CssClass="submit-button1" onclick="Button2_Click" 
                                  />
                                 
            </div>               
                   </td>
               </tr>
             <tr align="center">                       
                   <td colspan="3">
                      <div style='width:100%; height:250px; overflow:auto;'>
                  <asp:GridView id="GridView_popup" CssClass="grid" PagerStyle-CssClass="pgr"  
                 runat="server" AutoGenerateColumns="False" AllowPaging="True" PageSize ="100"   SelectedRowStyle-BackColor="GreenYellow"
                 Width="100%" OnRowCommand="GridView_popup_RowCommand" 
                           BackColor="LightGoldenrodYellow" BorderColor="Tan" BorderWidth="1px"
                           CellPadding="2" ForeColor="Black" GridLines="None" 
                           onpageindexchanging="GridView_popup_PageIndexChanging" Style="width:100%;">
                 
                <Columns>
                    <asp:CommandField SelectText="Select" ShowSelectButton="True" />
                    <asp:TemplateField HeaderText="DocNo" Visible="True">
                       
                        <ItemTemplate>
                            <asp:Label ID="lblid" runat="server" Text='<%# Bind("ID") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Supplier Name">
                       
                        <ItemTemplate>
                            <asp:Label ID="lblrname" runat="server" Text='<%# Bind("RName") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                      <asp:TemplateField HeaderText="Date">                       
                        <ItemTemplate>
                            <asp:Label ID="lbldate" runat="server" Text='<%# Bind("Date1") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                  
                    </Columns>
                <AlternatingRowStyle BackColor="PaleGoldenrod" />
                      <FooterStyle BackColor="Tan" />
             <HeaderStyle BackColor="Tan" Font-Bold="True" />
                      <PagerStyle BackColor="PaleGoldenrod" ForeColor="DarkSlateBlue" 
                          HorizontalAlign="Center" />
                      <SelectedRowStyle  ForeColor="GhostWhite" />
                      <SortedAscendingCellStyle BackColor="#FAFAE7" />
                      <SortedAscendingHeaderStyle BackColor="#DAC09E" />
                      <SortedDescendingCellStyle BackColor="#E1DB9C" />
                      <SortedDescendingHeaderStyle BackColor="#C2A47B" />
        </asp:GridView>
        </div>
        </td>
        </tr>
               </table>
        <table width="100%">
        <tr>
            <td align="center">
            
                <asp:HiddenField ID="HiddenField1" runat="server" /><asp:HiddenField ID="HiddenField2" runat="server" />

            </td>
            <td >

                <input id="btnClose" type="button" class="submit-button" value="Ok"  onclick="CloseDialog();" />
             <input id="Button1" type="button" value="Cancel" class="submit-button" onclick="return Button1_onclick()"  /> 

                </td>
        </tr>  
       </table>
        </ContentTemplate>
        

        </asp:UpdatePanel>

    
    </div>
    </form>
</body>
</html>
