<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ComplainPopup.aspx.cs" Inherits="IPD_ComplainPopup" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
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
        <table style='width:100%;'>
          
       <tr>
 
 <td style='width:200px;'>
                             
             <label class="pname"><strong>Complain Name :</strong></label> 
             </td>
        <td style='width:200px;'>      <asp:TextBox ID="txtname" runat="server" CssClass="textbox-medium1" Width="138px" ></asp:TextBox>
                         
</td> 

                <td>
                             <div class="form-sec-row"> 
           <asp:Button ID="Button2" runat="server" Text="Search"  CssClass="submit-button1"  Height="28px"
                                onclick="Button2_Click" />
                                 <br />
            </div>                  
</td>             
                      
            </tr>
                
             <tr align="center">                       
                   <td colspan="3">
                      <div style='width:100%; height:200px; overflow:auto;'>
                  <asp:GridView id="GridView_popup" CssClass="grid" PagerStyle-CssClass="pgr" DataKeyNames ="RowId"
                 runat="server" AutoGenerateColumns="False" AllowPaging="True"  PageSize="1000"
                 Width="100%" BackColor="LightGoldenrodYellow" BorderColor="Tan" BorderWidth="1px" 
                           CellPadding="2" ForeColor="Black" GridLines="None"    >
                 
                <Columns>
                    <asp:TemplateField HeaderText="Select">
                        <ItemTemplate>
                            <asp:CheckBox ID="CheckBox1" runat="server" />
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="RowId" Visible="false">
                       
                        <ItemTemplate>
                            <asp:Label ID="lblId" runat="server" Text='<%# Bind("RowId") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Complain Name">
                       
                        <ItemTemplate>
                            <asp:Label ID="lblName" runat="server" Text='<%# Bind("ComplainName") %>'></asp:Label>
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
            <td  align="center">
            
                <asp:HiddenField ID="HiddenField1" runat="server" /> <asp:HiddenField ID="HiddenField2" runat="server" />             

            </td>
            <td>
                <asp:Button ID="Button3" runat="server" Text="OK" CssClass="submit-button" 
                    onclick="Button3_Click" /> 
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
