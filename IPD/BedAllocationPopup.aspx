<%@ Page Language="C#" AutoEventWireup="true" CodeFile="BedAllocationPopup.aspx.cs" Inherits="IPD_BedAllocationPopup" %>

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
              //window.returnValue = arg;
            window.opener.document.getElementById("ctl00_ContentPlaceHolder1_TextBox19").value = arg.NameValue;
            window.opener.document.getElementById("ctl00_ContentPlaceHolder1_HiddenField2").value = arg.ProfessionValue;
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
       

       
 
  <asp:UpdateProgress ID="UpdateProgress1" runat="server">
    <ProgressTemplate>
    
       <div id="aaa" class="progressBackgroundFilter">          </div>
        <div id="bbb" class="processMessage"><img alt="Loading" src="../images/pwait.gif"/>
      
              </div>
    </ProgressTemplate>
    </asp:UpdateProgress>


        <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Always">
        <ContentTemplate>
        <table>
          
       <tr>
                <td>
                    <div class="form-sec-row"> 
             <label class="pname"><strong>Floor :</strong></label> 
                        <asp:DropDownList ID="DropDownList1" runat="server" 
                            CssClass="textbox-dropdown1" AutoPostBack="True" 
                            onselectedindexchanged="DropDownList1_SelectedIndexChanged">
                        </asp:DropDownList>    <div class="clear">
            </div>
            
</td>
                <td>
                    <div class="form-sec-row"> 
             <label class="pname"><strong>Wings :</strong></label> 
                      <asp:DropDownList ID="DropDownList2" runat="server" CssClass="textbox-dropdown1" 
                            onselectedindexchanged="DropDownList2_SelectedIndexChanged">
                        </asp:DropDownList>  <div class="clear">
            </div>
            
</td>
        <td>
                    <div class="form-sec-row"> 
             <label class="pname"><strong>Room Type :</strong></label> 
                        <asp:DropDownList ID="DropDownList4" runat="server" 
                            CssClass="textbox-dropdown1" AutoPostBack="True" 
                            onselectedindexchanged="DropDownList4_SelectedIndexChanged">
                        </asp:DropDownList>
            </div>
</td>
<td>
                             <div class="form-sec-row"> 
             <label class="ad"><strong>Room :</strong></label> <asp:DropDownList ID="DropDownList3" runat="server" CssClass="textbox-dropdown1">
                        </asp:DropDownList>
            </div>                  
</td>
      
        
     
          <td>
                             <div class="form-sec-row"> 
           <asp:Button ID="Button2" runat="server" Text="Search"  height="30px"  CssClass="submit-button1" onclick="Button2_Click" 
                                />
            </div>                  
</td>             
                      
            </tr>
                
             <tr align="center">                       
                   <td colspan="5">
                   <div style='width:100%; height:400px; overflow:auto;'>
                  <asp:GridView id="GridView_popup" CssClass="grid" PagerStyle-CssClass="pgr" DataKeyNames ="BedNo" 
                 runat="server" AutoGenerateColumns="False" AllowPaging="True" 
                 Width="100%" BackColor="LightGoldenrodYellow" BorderColor="Tan" BorderWidth="1px" 
                           CellPadding="2" ForeColor="Black" GridLines="None"    PageSize="100"
                           SelectedRowStyle-BackColor="GreenYellow" 
                           onpageindexchanging="GridView_popup_PageIndexChanging" onrowcommand="GridView_popup_RowCommand"
                                >
                 
                <Columns>
        
                     <asp:CommandField SelectText="Select" ShowSelectButton="True" />
                                
                    <asp:TemplateField HeaderText="Bed No" Visible="false">
                       
                        <ItemTemplate>
                            <asp:Label ID="lblbedno" runat="server" Text='<%# Bind("BedNo") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>

                     <asp:TemplateField HeaderText="Bed No">
                       
                        <ItemTemplate>
                            <asp:Label ID="lblbednotext" runat="server" Text='<%# Bind("BedNoText") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>

                       <asp:TemplateField HeaderText="Room Name">
                       
                        <ItemTemplate>
                            <asp:Label ID="lblrName" runat="server" Text='<%# Bind("RoomName") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>

                     
                      <asp:TemplateField HeaderText="Room Type">
                       
                        <ItemTemplate>
                            <asp:Label ID="lblservice" runat="server" Text='<%# Bind("RoomCategoryName") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>


                    
                     <asp:TemplateField HeaderText="Wings Name">
                       
                        <ItemTemplate>
                            <asp:Label ID="lblwname" runat="server" Text='<%# Bind("WingsName") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>


                      <asp:TemplateField HeaderText="Floor Name">
                       
                        <ItemTemplate>
                            <asp:Label ID="lblfname" runat="server" Text='<%# Bind("FloorName") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                 
              
                      <asp:TemplateField HeaderText="Charges">
                       
                        <ItemTemplate>
                            <asp:Label ID="lblCharges" runat="server" Text='<%# Bind("Charges") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                </Columns>
        <EditRowStyle BackColor="#FFDB91" />
            <AlternatingRowStyle BackColor="#FFDB91" />
         <HeaderStyle BackColor="#FFC0C0" />
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
             <td >
                
                 <input id="btnClose" type="button" class="submit-button" value="Ok"  onclick="CloseDialog();" />             
              <input id="Button1" type="button" value="Cancel" class="submit-button" onclick="return Button1_onclick()"" /> 

                </td>
               
        </tr>  
       </table>
        </ContentTemplate>
        

        </asp:UpdatePanel>

    
    </div>
    </form>
</body>
</html>
