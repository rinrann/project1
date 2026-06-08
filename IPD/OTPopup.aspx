<%@ Page Language="C#" AutoEventWireup="true" CodeFile="OTPopup.aspx.cs" Inherits="IPD_OTPopup" %>

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

    <style type="text/css">
      body{
  width: 90%; 
  left: 5%; 
  
  margin-left:auto;
  margin-right:auto; 
}
    </style>
    <script type="text/javascript" >

        function Calling() {
            var date = new Date();
            $("input[id$='txtdate']").datepicker({ dateFormat: 'dd/mm/yy' });


            $("input[id$='btnSubmit']").click(function () {

                if ($("input[id$='txtOperationName']").val() == '') {
                    alert('Operation Name Can not be Blank!');
                    $("input[id$='txtOperationName']").focus();
                    $("input[id$='txtOperationName']").addClass('textboxerr');
                    return false;
                }
                else {
                    $("input[id$='txtOperationName']").removeClass('textboxerr');
                }

                var e = document.getElementById("ctl00_ContentPlaceHolder1_ddlOTTYpe");
                var strUser = e.options[e.selectedIndex].text;

                if (strUser == '--Select--') {
                    alert('Select Operation Type');
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

        function CloseDialog() {
            var arg = new Object();
            arg.NameValue = document.getElementById('HiddenField1').value;
            arg.ProfessionValue = document.getElementById('HiddenField2').value;
            window.returnValue = arg;
            window.close();

        }

        function Button1_onclick() {
            window.close();
        }

    </script>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <asp:ScriptManager ID="ScriptManager1" runat="server">
        </asp:ScriptManager>
       
<%--For Busy Loader .............................--%> 
<%--  <asp:UpdateProgress ID="UpdateProgress1" runat="server">
    <ProgressTemplate>
    
       <div id="aaa" class="progressBackgroundFilter">          </div>
        <div id="bbb" class="processMessage"><img alt="Loading" src="../images/pwait.gif"/>
      
              </div>
    </ProgressTemplate>
    </asp:UpdateProgress>--%>
    <%--For Busy Loader End.............................--%>
        <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Always">
        <ContentTemplate>

        <div class="formbox">
         <table width="290px" cellpadding="0" cellspacing="0">
         <tr>
            <td>
                <asp:Button Text="List" BorderStyle="None" ID="Tab1" Width="145px"  Height="40px" 
                    CssClass="Initial" runat="server" onclick="Tab1_Click"
                     /></td>
                     <td>
                <asp:Button Text="Entry" BorderStyle="None" ID="Tab2" CssClass="Initial"  Width="145px"  Height="40px" 
                    runat="server" onclick="Tab2_Click"
                    /></td>
           
                     </tr>
                     </table>
                     </div>
  <div class="formbox">
         <asp:MultiView ID="MainView" runat="server">
                    <asp:View ID="View1" runat="server">
                    <table width="700px">
          
       <tr>
                <td>
                   
           OT Type :  </td>
             <td><asp:DropDownList ID="DropDownList1" runat="server" CssClass ="textbox-medium2" 
                            AutoPostBack="True" onselectedindexchanged="DropDownList1_SelectedIndexChanged">
                        </asp:DropDownList>   
</td>
                <td>
                   
            OT Name :
              </td>
                <td>
                       <asp:DropDownList ID="DropDownList2" runat="server" CssClass ="textbox-medium2">
                        </asp:DropDownList> 
</td>

    
          <td align="left"> <asp:Button ID="Button2" runat="server" Text="Search"  Height="28px" CssClass="submit-button1" onclick="Button2_Click" 
                                />
                    
</td>             
                      
            </tr>
                
             <tr>                       
                   <td colspan="5">
                     <div style='width:100%; height:300px; overflow:auto;'>
                  <asp:GridView id="GridView1" CssClass="grid" PagerStyle-CssClass="pgr"  
                 runat="server" AutoGenerateColumns="False" AllowPaging="True" PageSize ="1000"   SelectedRowStyle-BackColor="GreenYellow"
                 Width="693px" BorderColor="Tan" BorderWidth="1px" 
                           CellPadding="2" ForeColor="Black" GridLines="None" 
                           OnRowCommand="GridView1_RowCommand" 
                           onpageindexchanging="GridView1_PageIndexChanging">
                 
                <Columns>
                    <asp:CommandField SelectText="Select" ShowSelectButton="True" />
                       <asp:TemplateField HeaderText="OT NAME ID" Visible="false">
                       
                        <ItemTemplate>
                            <asp:Label ID="lblotid" runat="server" Text='<%# Bind("OperationID") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="OT NAME">
                       
                        <ItemTemplate>
                            <asp:Label ID="lblotname" runat="server" Text='<%# Bind("OperationName") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField> 
                     <asp:TemplateField HeaderText="OT TYPE ID" Visible="false">
                       
                        <ItemTemplate>
                            <asp:Label ID="lblOTTYPEID" runat="server" Text='<%# Bind("OperationTypeID") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="OT TYPE">
                       
                        <ItemTemplate>
                            <asp:Label ID="lblottypename" runat="server" Text='<%# Bind("OperationTypeName") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>   

                          <asp:TemplateField HeaderText="OPERATION COST">
                       
                        <ItemTemplate>
                            <asp:Label ID="lblotcost" runat="server" Text='<%# Bind("OperationCost") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField> 
                  

                      <asp:TemplateField HeaderText="DURATION">
                       
                        <ItemTemplate>
                            <asp:Label ID="lblduration" runat="server" Text='<%# Bind("Duration") %>'></asp:Label>
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
        <tr>
            <td align="center">
            
                <asp:HiddenField ID="HiddenField1" runat="server" />
                <asp:HiddenField ID="HiddenField2" runat="server" />

            </td>
            <td align="center">

                <input id="btnClose" type="button" class="submit-button" value="Ok" style='width:75px; height:30px;'  onclick="CloseDialog();" />
             <input id="Button1" type="button" value="Cancel" class="submit-button" style='width:75px; height:30px;'  onclick="return Button1_onclick()"" /> 

                </td>
        </tr>  
       </table>
                    </asp:View>
                     <asp:View ID="View2" runat="server">
                     <table width="100%">
                     <tr>
                     <td style='width:200px;'>Operation Type :</td>
                         <td>
                             <asp:DropDownList ID="ddlOTTYpe" Width="155px" runat="server">
                             </asp:DropDownList>
                         </td></tr>

                                    <tr>
                     <td style='width:200px;'>Operation Name :</td>
                         <td>
                             <asp:TextBox ID="txtOperationName" Width="150px"  runat="server"></asp:TextBox>
                         </td></tr>
                         
                                    <tr>
                     <td style='width:200px;'>Operation Cost :</td>
                         <td>
                              <asp:TextBox ID="txtCost" Width="150px"  runat="server"></asp:TextBox>
                         </td></tr>

                         
                                    <tr>
                     <td style='width:200px;'>Operation Summary :</td>
                         <td>
                              <asp:TextBox ID="txtSummary" Width="150px"  runat="server"></asp:TextBox>
                         </td></tr>

                         
                                    <tr>
                     <td style='width:200px;'>Duration :</td>
                         <td>
                              <asp:TextBox ID="txtDuration" Width="150px"  runat="server"></asp:TextBox>
                         </td></tr>

                         <tr>
                         <td></td>
                         <td>
                             <asp:Button ID="btnSubmit" CssClass="submit-button" runat="server" 
                                 Text="Submit" onclick="btnSubmit_Click" />
                             <asp:Button ID="btnClear" CssClass="submit-button" runat="server" Text="clear" /></td></tr>
                         
                         </table>
                    </asp:View>
                    </asp:MultiView>
        </div>
        </ContentTemplate>
        

        </asp:UpdatePanel>

    
    </div>
    </form>
</body>
</html>
