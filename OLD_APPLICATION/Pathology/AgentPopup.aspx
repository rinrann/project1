<%@ Page Language="C#" AutoEventWireup="true" CodeFile="AgentPopup.aspx.cs" Inherits="Pathology_AgentPopup" %>

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
     //     function GetType() {

     //         var obj = window.dialogArguments;
     //         alert(obj);
     //         document.getElementById('HiddenField2')= obj;

     //     } 
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
       
        <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Always">
        <ContentTemplate>
        <table>
          
       <tr>
                <td>
                    <div class="form-sec-row"> 
             <label class="pname"><strong>Reagent Name :</strong></label> 
                      <asp:TextBox ID="txtname" runat="server" CssClass="textbox-medium1" Width="138px" ></asp:TextBox>  <div class="clear">
            </div>
            
</td>
        
                <td>
                             <div class="form-sec-row"> 
             <label class="pname"><strong>Date :</strong></label> <asp:TextBox ID="txtdate" runat="server" CssClass="textbox-medium1" Width="138px" ></asp:TextBox>
            </div>                  
</td>
                <td>
                             <div class="form-sec-row"> 
           <asp:Button ID="Button2" runat="server" Text="Search"  Height="28px"  CssClass="submit-button1" onclick="Button2_Click" 
                                     />
                                 
            </div>                  
</td>             
                      
            </tr>
                
             <tr align="center">                       
                   <td colspan="4">
                   <div style='width:100%; height:250px; overflow:auto;'>
                  <asp:GridView id="GridView_popup" CssClass="grid" PagerStyle-CssClass="pgr" DataKeyNames ="RCode" 
                 runat="server" AutoGenerateColumns="False" AllowPaging="True" PageSize ="700"   SelectedRowStyle-BackColor="GreenYellow"
                 Width="693px" OnRowCommand="GridView_popup_RowCommand" 
                           BackColor="LightGoldenrodYellow" BorderColor="Tan" BorderWidth="1px" 
                           CellPadding="2" ForeColor="Black" GridLines="None" 
                           onpageindexchanging="GridView_popup_PageIndexChanging">
                 
                <Columns>
                    <asp:CommandField SelectText="Select" ShowSelectButton="True" />
                      <asp:TemplateField HeaderText="Reagent Name">
                       
                        <ItemTemplate>
                            <asp:Label ID="lblrname" runat="server" Text='<%# Bind("RName") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Supplier Name">
                       
                        <ItemTemplate>
                            <asp:Label ID="lblsname" runat="server" Text='<%# Bind("SName") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>

                      <asp:TemplateField HeaderText="Manufacturer Name">
                       
                        <ItemTemplate>
                            <asp:Label ID="lblmname" runat="server" Text='<%# Bind("MName") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>

                      <asp:TemplateField HeaderText="Company Name">
                       
                        <ItemTemplate>
                            <asp:Label ID="lblcname" runat="server" Text='<%# Bind("CName") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                   <%-- <asp:TemplateField HeaderText="Date">
                       
                        <ItemTemplate>
                            <asp:Label ID="lbldate" runat="server" Text='<%# Bind("Date123") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>--%>
                    <asp:TemplateField HeaderText="Quantity">
                       
                        <ItemTemplate>
                            <asp:Label ID="lblqty" runat="server" Text='<%# Bind("Quantity") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                     <asp:TemplateField HeaderText="Bonus Quantity">
                       
                        <ItemTemplate>
                            <asp:Label ID="lblbonusqty" runat="server" Text='<%# Bind("BonusQty") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                      <asp:TemplateField HeaderText="Price Per Unit">
                       
                        <ItemTemplate>
                            <asp:Label ID="lblprice" runat="server" Text='<%# Bind("Price") %>'></asp:Label>
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

                  <input id="btnClose" type="button" class="submit-button" value="Ok" style='width:75px; height:30px;'  onclick="CloseDialog();" /> 
             <input id="Button1" type="button" value="Cancel" class="submit-button" style='width:75px; height:30px;' onclick="return Button1_onclick()"" /> 


                </td>
        </tr>  
       </table>
        </ContentTemplate>
        

        </asp:UpdatePanel>

    
    </div>
    </form>
</body>
</html>
