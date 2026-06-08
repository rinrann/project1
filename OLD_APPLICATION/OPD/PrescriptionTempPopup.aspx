<%@ Page Language="C#" AutoEventWireup="true" CodeFile="PrescriptionTempPopup.aspx.cs" Inherits="IPD_PrescriptionTempPopup" %>

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
        function CloseDialog() {
            var arg = new Object();
            arg.NameValue = document.getElementById('HiddenField1').value;
            arg.ProfessionValue = document.getElementById('HiddenField2').value;
            window.returnValue = arg;
            window.opener.document.getElementById("ctl00_ContentPlaceHolder1_HiddenField1").value = arg.NameValue;
            window.opener.document.getElementById("ctl00_ContentPlaceHolder1_txtPrescrpTemName").value = arg.ProfessionValue;
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
       
        <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Always">
        <ContentTemplate>
        <table>
          
       <tr>

                       <td > 
             <label><strong>Template Name :</strong></label> 
         </td>
         <td> <asp:TextBox ID="TextBox1" runat="server" CssClass="textbox-medium1" Width="150px"></asp:TextBox> </td>
         

     
          <td>
                             <div class="form-sec-row"> 
           <asp:Button ID="Button2" runat="server" Text="Search" Height="28px"  CssClass="submit-button" onclick="Button2_Click" 
                                />
            </div>                  
</td>             
                      
            </tr>
                
             <tr align="center">                       
                   <td colspan="3">
                     <div style='width:100%; height:300px; overflow:auto;'>
                  <asp:GridView id="GridView_popup" CssClass="grid" PagerStyle-CssClass="pgr" DataKeyNames ="PrescrpTemID" 
                 runat="server" AutoGenerateColumns="False" AllowPaging="True" 
                 Width="693px" BackColor="LightGoldenrodYellow" BorderColor="Tan" BorderWidth="1px" 
                           CellPadding="2" ForeColor="Black" GridLines="None"   PageSize="100" 
                           SelectedRowStyle-BackColor="GreenYellow" 
                           onpageindexchanging="GridView_popup_PageIndexChanging" onrowcommand="GridView_popup_RowCommand"
                                >
                 
                <Columns>
              
                   <%-- <asp:TemplateField>
                    <ItemTemplate>
                        <asp:CheckBox ID="CheckBox1" runat="server" />
                    </ItemTemplate>
                    </asp:TemplateField>--%>
                     <asp:CommandField SelectText="Select" ShowSelectButton="True" />
                                
                    <asp:TemplateField HeaderText="Template ID" Visible="false">
                       
                        <ItemTemplate>
                            <asp:Label ID="lblid" runat="server" Text='<%# Bind("PrescrpTemID") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Template Name">
                       
                        <ItemTemplate>
                            <asp:Label ID="lblName" runat="server" Text='<%# Bind("PrescrpTemName") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>

                      <asp:TemplateField HeaderText="Created Date">
                       
                        <ItemTemplate>
                            <asp:Label ID="lbldate" runat="server" Text='<%# Bind("cd") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                               
                </Columns>
                <AlternatingRowStyle BackColor="PaleGoldenrod" />
                      <FooterStyle BackColor="Tan" />
             <HeaderStyle BackColor="Tan" Font-Bold="True" />
                      <PagerStyle BackColor="PaleGoldenrod" ForeColor="DarkSlateBlue" 
                          HorizontalAlign="Center" />
                      <SelectedRowStyle   ForeColor="GhostWhite" />
                      <SortedAscendingCellStyle BackColor="#FAFAE7" />
                      <SortedAscendingHeaderStyle BackColor="#DAC09E" />
                      <SortedDescendingCellStyle BackColor="#E1DB9C" />
                      <SortedDescendingHeaderStyle BackColor="#C2A47B" />
        </asp:GridView>
        </div>
        </td>
        </tr>
        </table>
        <table  width="100%">
        <tr>
            <td align="center">
              <asp:HiddenField ID="HiddenField1" runat="server" />
                <asp:HiddenField ID="HiddenField2" runat="server" />
         
               
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
