<%@ Page Language="C#" AutoEventWireup="true" CodeFile="TestPopup.aspx.cs" Inherits="Pathology_TestPopup" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
        <link href="../css/style.css" rel="stylesheet" type="text/css" />
    <script src="../Script/timeout-dialog.js" type="text/javascript"></script>
    <script src="../Script/snow.js" type="text/javascript"></script>
    <script src="../Script/menu.js" type="text/javascript"></script>
    <script src="../Script/jquery.ui.timepicker.js" type="text/javascript"></script>
    <script src="../Script/jquery.min.js" type="text/javascript"></script>
    <script src="../Script/jquery.js" type="text/javascript"></script>
    <script src="../Script/jquery.idle-timer.js" type="text/javascript"></script>
    <script src="../Script/jquery.dynDateTime.min.js" type="text/javascript"></script>
    <script src="../Script/Jquery.corner.js" type="text/javascript"></script>
    <script src="../Script/jquery-ui-1.8.19.custom.min.js" type="text/javascript"></script>
    <script src="../Script/jquery-1.8.2.min.js" type="text/javascript"></script>
    <script src="../Script/jquery-1.7.2.min.js" type="text/javascript"></script>
    <script src="../Script/jquery-1.4.1.min.js" type="text/javascript"></script>
    <script src="../Script/jquery-1.4.1.js" type="text/javascript"></script>
    <script src="../Script/jquery-1.4.1-vsdoc.js" type="text/javascript"></script>
    <script src="../Script/jquery-1.3.2.min.js" type="text/javascript"></script>
    <script src="../Script/datetimepicker.js" type="text/javascript"></script>
    <script src="../Script/calendar-en.min.js" type="text/javascript"></script>
    <link href="../css/calendar-blue.css" rel="stylesheet" type="text/css" />
    <link href="../css/jquery-ui-1.8.19.custom.css" rel="stylesheet" type="text/css" />
    <link href="../css/jquery-ui.css" rel="stylesheet" type="text/css" />
    <link href="../css/jquery.timepicker.css" rel="stylesheet" type="text/css" />
    <script src="../css/jquery.timepicker.js" type="text/javascript"></script>
    <script src="../css/jquery.timepicker.min.js" type="text/javascript"></script>
    <link href="../css/jquery.ui.timepicker.css" rel="stylesheet" type="text/css" />
    <link href="../css/MyCss.css" rel="stylesheet" type="text/css" />
    
      <script type="text/javascript">
            function CloseDialog() {
            var arg = new Object();
              arg.NameValue = document.getElementById('HiddenField1').value;
              arg.ProfessionValue = document.getElementById('HiddenField2').value;
              window.returnValue = arg;
              window.opener.document.getElementById("ctl00_ContentPlaceHolder1_txtcode").value = arg.NameValue;
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
        <table>
          
       <tr>

  
                <td>
                             <div class="form-sec-row"> 
             <label class="pname"><strong>Test Code :</strong></label> <asp:TextBox ID="txtcode" runat="server" CssClass="textbox-medium1" Width="138px" ></asp:TextBox>
            </div>                  
</td>
 <td>
                             <div class="form-sec-row"> 
             <label class="pname"><strong>Test Name :</strong></label> <asp:TextBox ID="txtname" runat="server" CssClass="textbox-medium1" Width="138px" ></asp:TextBox>
            </div>                  
</td>
 <td>
                             <div class="form-sec-row"> 
             <label class="pname"><strong>Department :</strong></label> 
                                 <asp:DropDownList ID="DropDownList1" runat="server" CssClass="textbox-mediumpatcover">
                                 </asp:DropDownList>
            </div>                  
</td>

                <td>
                             <div class="form-sec-row"> 
           <asp:Button ID="Button2" runat="server" Text="Search"  Height="28px" CssClass="submit-button1" 
                                onclick="Button2_Click" />
                                 <br />
            </div>                  
</td>             
                      
            </tr>
                
             <tr align="center">                       
                   <td colspan="4">
                      <div style='width:100%; height:250px; overflow:auto;'>
                  <asp:GridView id="GridView_popup" CssClass="grid" PagerStyle-CssClass="pgr" DataKeyNames ="TestId" 
                 runat="server" AutoGenerateColumns="False" AllowPaging="True" PageSize ="100" 
                 Width="100%" BackColor="LightGoldenrodYellow" BorderColor="Tan" BorderWidth="1px" 
                           CellPadding="2" ForeColor="Black" GridLines="None" OnRowDataBound="GridView_popup_RowDataBound"
                           onrowcommand="GridView_popup_RowCommand" OnRowDeleting="GridView_popup_RowDeleting" 
                           onpageindexchanging="GridView_popup_PageIndexChanging">
                 
                <Columns>
                    <asp:CommandField SelectText="Select" ShowSelectButton="True" />
                    <asp:TemplateField HeaderText="Test Code">
                       
                        <ItemTemplate>
                            <asp:Label ID="lblregno" runat="server" Text='<%# Bind("TestId") %>'></asp:Label>
                            <asp:Label ID="lbltype" runat="server" Text='<%# Bind("cat") %>' Visible="false"></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Test Name">
                       
                        <ItemTemplate>
                            <asp:Label ID="lblName" runat="server" Text='<%# Bind("TestName") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    
                     <asp:TemplateField HeaderText="Cost">
                       
                        <ItemTemplate>
                            <asp:Label ID="lblcost" runat="server" Text='<%# Bind("Cost") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Test Type">
                       
                        <ItemTemplate>
                            <asp:Label ID="lbltesttype" runat="server" Text='<%# Bind("TestType") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                   <asp:CommandField ShowDeleteButton="True" ControlStyle-CssClass="temp"  ItemStyle-Width="70px"  ItemStyle-HorizontalAlign="Center"  HeaderText="Delete" DeleteImageUrl="~/Images/delete.png" ButtonType="Image">
                        <ControlStyle CssClass="temp"></ControlStyle>
                    </asp:CommandField>
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
            <td  align="center">
            
                <asp:HiddenField ID="HiddenField1" runat="server" />
                <asp:HiddenField ID="HiddenField2" runat="server" />
                <asp:HiddenField ID="HiddenField3" runat="server" />

            </td>
            <td >

                <input id="btnClose" type="button" class="submit-button" value="Ok"  onclick="CloseDialog();" />
             <input id="Button1" type="button" value="Cancel" class="submit-button" onclick="return Button1_onclick()" />

                </td>
        </tr>  
       </table>
        </ContentTemplate>
        </asp:UpdatePanel>
    </div>
    </form>
</body>
</html>
