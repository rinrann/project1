<%@ Page Language="C#" AutoEventWireup="true" CodeFile="MedicineReturnPopup.aspx.cs" Inherits="Medicine_MedicineReturnPopup" %>

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
   
   

     
    <script src="../js/menu.js" type="text/javascript"></script>
    <script src="../js/jquery.js" type="text/javascript"></script>
    <script src="../js/jquery-ui.js" type="text/javascript"></script>
    <link href="../js/jquery-ui.css" rel="stylesheet" type="text/css" />
    <script src="../js/jquery-1.9.1.js" type="text/javascript"></script>


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
            window.returnValue = arg;
            window.opener.document.getElementById("ctl00_ContentPlaceHolder1_txtPurchaseMedicineId").value = arg.NameValue;
            window.close();

        }




        function Button1_onclick() {
            window.close();
        }

        function Calling() {
            var date = new Date();
            $("input[id$='TextBox2']").datepicker({ dateFormat: 'dd/mm/yy' });
            $("input[id$='TextBox4']").datepicker({ dateFormat: 'dd/mm/yy' });
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

    </script>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <asp:ScriptManager ID="ScriptManager1" runat="server">
        </asp:ScriptManager>
       
        <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Always">
        <ContentTemplate>
        <table width="990px">
          
       <tr>

                       <td>
                 <strong>   
          Invoice No :</strong>   </td>
          <td>
           <asp:TextBox ID="TextBox1" runat="server"   Width="120px"></asp:TextBox> 
   </td>
               <td >
              <strong>
          Supplier Name:</strong>   </td>
          <td>
              <asp:DropDownList ID="DropDownList1" runat="server"  Width="120px">
              </asp:DropDownList>
   </td>
               <td >
                 <strong>   
         From Date :</strong>   </td>
          <td>
           <asp:TextBox ID="TextBox2" runat="server"   Width="120px"></asp:TextBox>  
   </td>
          <td >
                 <strong>   
         To Date :</strong>   </td>
          <td>
           <asp:TextBox ID="TextBox4" runat="server"   Width="120px"></asp:TextBox>  
   </td>
               <td>
                 <strong>   
         Total Price :</strong>   </td>
          <td>
           <asp:TextBox ID="TextBox3" runat="server"   Width="120px"></asp:TextBox>  
   </td>
     
          <td>
                             <div class="form-sec-row"> 
           <asp:Button ID="Button2" runat="server" Text="Search"  CssClass="submit-button1" onclick="Button2_Click"  Height="28px"
                                />
            </div>                  
</td>             
                      
            </tr>
                
             <tr align="center">                       
                   <td colspan="10">
                     <div style='width:100%; height:300px; overflow:auto;'>
                  <asp:GridView id="GridView_popup" CssClass="grid" PagerStyle-CssClass="pgr" DataKeyNames ="ReturnMedicineID" 
                 runat="server" AutoGenerateColumns="False" AllowPaging="True" 
                 Width="100%" BackColor="LightGoldenrodYellow" BorderColor="Tan" BorderWidth="1px" 
                           CellPadding="2" ForeColor="Black" GridLines="None"   
                           SelectedRowStyle-BackColor="GreenYellow"  PageSize="100"
                           onpageindexchanging="GridView_popup_PageIndexChanging" onrowcommand="GridView_popup_RowCommand"
                                >
                 
                <Columns>
              
        
                     <asp:CommandField SelectText="Select" ShowSelectButton="True" />
                                
                    <asp:TemplateField HeaderText="Return ID" >
                       
                        <ItemTemplate>
                            <asp:Label ID="lblid" runat="server" Text='<%# Bind("ReturnMedicineID") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
   

                      <asp:TemplateField HeaderText="Supplier Name">
                       
                        <ItemTemplate>
                            <asp:Label ID="lblsname" runat="server" Text='<%# Bind("SName") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="Return Date">
                       
                        <ItemTemplate>
                            <asp:Label ID="lblpdate" runat="server" Text='<%# Bind("pdate") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>

                         <asp:TemplateField HeaderText="Total Purchase">
                       
                        <ItemTemplate>
                            <asp:Label ID="lbltotalpurchase" runat="server" Text='<%# Bind("Total") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                               
                </Columns>
                <AlternatingRowStyle BackColor="PaleGoldenrod" />
                      <FooterStyle BackColor="Tan" />
             <HeaderStyle BackColor="Tan" Font-Bold="True" />
                      <PagerStyle BackColor="PaleGoldenrod" ForeColor="DarkSlateBlue" 
                          HorizontalAlign="Center" />
                      <SelectedRowStyle BackColor="GreenYellow"   ForeColor="GhostWhite" />
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
            <td align="center" >
        <asp:HiddenField ID="HiddenField1" runat="server" Value="" />
                <asp:HiddenField ID="HiddenField2" runat="server" />
                </td>
                 <td>
             <input id="btnClose" type="button" class="submit-button"   value="Ok" onclick="CloseDialog();" />
              <input id="Button1" type="button" value="Cancel"  class="submit-button" onclick="return Button1_onclick()"" /> 

                </td>
        </tr>  
       </table>
        </ContentTemplate>
        

        </asp:UpdatePanel>

    
    </div>
    </form>
</body>
</html>
