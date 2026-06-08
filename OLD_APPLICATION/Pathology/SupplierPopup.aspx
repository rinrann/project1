<%@ Page Language="C#" AutoEventWireup="true" CodeFile="SupplierPopup.aspx.cs" Inherits="Pathology_SupplierPopup" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
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
         arg = document.getElementById('HiddenField1').value;
         
         window.returnValue = arg;
         window.close();

     }
 
     function Button1_onclick() {
         window.close();
     }

  
    </script>
</head>
<body onload="GetType()">
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
        <table>
          
       <tr>
                <td>
                    <div class="form-sec-row"> 
             <label class="pname"><strong>Supplier Name :</strong></label> 
                      <asp:TextBox ID="txtname" runat="server" CssClass="textbox-medium1" Width="138px" ></asp:TextBox>  <div class="clear">
            </div>
            
</td>
                <td>
                    <div class="form-sec-row"> 
             <label class="pname"><strong>Address :</strong></label> <asp:TextBox ID="txtaddress" runat="server" CssClass="textbox-medium1" Width="138px" ></asp:TextBox>
            </div>
</td>
                <td>
                             <div class="form-sec-row"> 
             <label class="pname"><strong>Phone No :</strong></label> <asp:TextBox ID="txtph" runat="server" CssClass="textbox-medium1" Width="138px" ></asp:TextBox>
            </div>                  
</td>
                <td>
                             <div class="form-sec-row"> 
           <asp:Button ID="Button2" runat="server" Text="Search"  Height="28px" CssClass="submit-button1" 
                                     onclick="Button2_Click" />
                                 
            </div>                  
</td>             
                      
            </tr>
                
             <tr align="center">                       
                   <td colspan="4">
                      <div style='width:100%; height:250px; overflow:auto;'>
                  <asp:GridView id="GridView_popup" CssClass="grid" PagerStyle-CssClass="pgr" DataKeyNames ="SCode" 
                 runat="server" AutoGenerateColumns="False" AllowPaging="True" PageSize ="100"   SelectedRowStyle-BackColor="GreenYellow"
                 Width="100%" OnRowCommand="GridView_popup_RowCommand" 
                           BackColor="LightGoldenrodYellow" BorderColor="Tan" BorderWidth="1px" 
                           CellPadding="2" ForeColor="Black" GridLines="None" 
                           onpageindexchanging="GridView_popup_PageIndexChanging">
                 
                <Columns>
                    <asp:CommandField SelectText="Select" ShowSelectButton="True" />
                    <asp:TemplateField HeaderText="Supplier Code">
                       
                        <ItemTemplate>
                            <asp:Label ID="lblregno" runat="server" Text='<%# Bind("sCode") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Supplier Name">
                       
                        <ItemTemplate>
                            <asp:Label ID="lblName" runat="server" Text='<%# Bind("sName") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Address">
                       
                        <ItemTemplate>
                            <asp:Label ID="lblfather" runat="server" Text='<%# Bind("Address") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                     <asp:TemplateField HeaderText="Phone-1">
                       
                        <ItemTemplate>
                            <asp:Label ID="lblladd" runat="server" Text='<%# Bind("Ph1") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                      <asp:TemplateField HeaderText="Phone-2">
                       
                        <ItemTemplate>
                            <asp:Label ID="lbllph2" runat="server" Text='<%# Bind("Ph2") %>'></asp:Label>
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
            
                <asp:HiddenField ID="HiddenField1" runat="server" />

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
