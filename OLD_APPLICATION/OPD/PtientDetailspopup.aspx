<%@ Page Language="C#" AutoEventWireup="true" CodeFile="PtientDetailspopup.aspx.cs" Inherits="OPD_PtientDetailspopup" %>

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
        $(document).ready(function () {
            var date = new Date();
            $("input[id$='txtdate']").datepicker({ dateFormat: 'dd/mm/yy' });
        });

        function CloseDialog() {
            debugger
            var arg = new Object();
            arg.NameValue = document.getElementById('HiddenField1').value;
            arg.ProfessionValue = document.getElementById('HiddenField2').value;
            //alert(arg.ProfessionValue);
            window.returnValue = arg;
            //window.opener.document.getElementById("ctl00_ContentPlaceHolder1_txtreg").value = arg.NameValue;
            window.opener.document.getElementById("txtreg").value = arg.NameValue;
            //window.opener.document.getElementById("ctl00_ContentPlaceHolder1_txtAppo").value = arg.ProfessionValue;
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
        <table width="100%">
          
       <tr>
                <td>
                 
             <label class="pname"><strong>Reg No :</strong></label> 
            </td>
            <td>
                      <asp:TextBox ID="txtreg" runat="server" CssClass="textbox-medium1" Width="138px" ></asp:TextBox>  <div class="clear">
          
            
</td>
                <td>
                 
             <label class="pname"><strong>Name :</strong></label> 
             </td>
             <td>
                      <asp:TextBox ID="txtname" runat="server" CssClass="textbox-medium1" Width="138px" ></asp:TextBox>  
        
            
</td>

<td>
                      
             <label class="ad"><strong>Address :</strong></label></td>
             
             <td> <asp:TextBox ID="txtaddress" runat="server" CssClass="textbox-medium1" Width="138px" ></asp:TextBox>
                            
</td>
      
                <td>
            
             <label class="pname"><strong>Ph No :</strong></label></td>
             <td> <asp:TextBox ID="txtph" runat="server" CssClass="textbox-medium1" Width="138px" ></asp:TextBox>
   
</td>
     
          <td> 
           <asp:Button ID="Button2" runat="server" Text="Search" Height="28px"  CssClass="submit-button" onclick="Button2_Click" />
                          
</td>             
                      
            </tr>
                
             <tr align="center">                       
                   <td colspan="9">
                     <div style='width:100%; height:500px; overflow:auto;'>
                  <asp:GridView id="GridView1" CssClass="grid" PagerStyle-CssClass="pgr" DataKeyNames ="PatientRegNo" 
                 runat="server" AutoGenerateColumns="False" AllowPaging="True" PageSize ="100"   SelectedRowStyle-BackColor="GreenYellow"
                 Width="100%" BorderColor="Tan" BorderWidth="1px" 
                           CellPadding="2" ForeColor="Black" GridLines="None" 
                           OnRowCommand="GridView1_RowCommand" 
                           onpageindexchanging="GridView1_PageIndexChanging">
                 
                <Columns>
                    <asp:CommandField SelectText="Select" ShowSelectButton="True" />
                    <asp:TemplateField HeaderText="Appointment NO" ItemStyle-HorizontalAlign="Left">
                       
                        <ItemTemplate>
                            <asp:Label ID="lblAppno" runat="server" Text='<%# Bind("AppoNo") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Appointment Date" ItemStyle-HorizontalAlign="center">
                       
                        <ItemTemplate>
                            <asp:Label ID="lblAppdate" runat="server" Text='<%# Bind("AppointMentDate") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Appointment Time" ItemStyle-HorizontalAlign="center">
                       
                        <ItemTemplate>
                            <asp:Label ID="lblAppTime" runat="server" Text='<%# Bind("AppointmentTime") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Registration NO" ItemStyle-HorizontalAlign="Left">
                       
                        <ItemTemplate>
                            <asp:Label ID="lblregno" runat="server" Text='<%# Bind("PatientRegNo") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Patient Name" ItemStyle-HorizontalAlign="Left">
                       
                        <ItemTemplate>
                            <asp:Label ID="lblName" runat="server" Text='<%# Bind("PName") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Guadian Name" ItemStyle-HorizontalAlign="Left">
                       
                        <ItemTemplate>
                            <asp:Label ID="lblfather" runat="server" Text='<%# Bind("GuadianName") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                     <asp:TemplateField HeaderText="Local Address" ControlStyle-Width="200px" ItemStyle-HorizontalAlign="Left">
                       
                        <ItemTemplate>
                            <asp:Label ID="lblladd" runat="server" Text='<%# Bind("Address") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
    
                    <asp:TemplateField HeaderText="Phone - 1" ItemStyle-HorizontalAlign="Left">
                       
                        <ItemTemplate>
                            <asp:Label ID="lblph1" runat="server" Text='<%# Bind("PhNo1") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="Phone - 2" ItemStyle-HorizontalAlign="Left">
                       
                        <ItemTemplate>
                            <asp:Label ID="lblph2" runat="server" Text='<%# Bind("PhNo2") %>'></asp:Label>
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
                <asp:HiddenField ID="HiddenField2" runat="server" />

                </td>
            <td>
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
