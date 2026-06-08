<%@ Page Language="C#" AutoEventWireup="true" CodeFile="PurchaseMedicinePopup.aspx.cs" Inherits="Medicine_PurchaseMedicinePopup" %>

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
            arg = document.getElementById('HiddenField1').value;
            window.returnValue = arg;
            window.opener.document.getElementById("ctl00_ContentPlaceHolder1_txtPurchaseMedicineId").value = arg;
            window.close();

        }
           
        function Button1_onclick() {
            window.close();
        }

        function Calling() {
            var date = new Date();
            $("input[id$='TextBox2']").datepicker({ dateFormat: 'dd/mm/yy' });
            $("input[id$='TextBox4']").datepicker({ dateFormat: 'dd/mm/yy' });
            $("input[id$='TextBox5']").datepicker({ dateFormat: 'dd/mm/yy' });
            $("input[id$='TextBox6']").datepicker({ dateFormat: 'dd/mm/yy' });
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
        
        function SetContextKey() {
            $find('AutoCompleteExtender1').set_contextKey("GFC");
        }
        function autoCompleteEx_ItemSelected(sender, args) {
            var regname = args.get_value().split('~'); //alert(regname[0]);
            //document.getElementById("ctl00_ContentPlaceHolder1_TextBox1").value = regname[1];
            document.getElementById("TextBox8").value = regname[0];
            $("#TextBox8").focus();
        }
        function autoCompleteEx_SupplierSelected(sender, args) {
            var regname = args.get_value().split('-'); //alert(regname[0]);
            //document.getElementById("ctl00_ContentPlaceHolder1_TextBox1").value = regname[1];
            document.getElementById("TextBox7").value = regname[0];
            $("#TextBox7").focus();
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
            <div class="formbox" style="width:800px;" id="45">
                <div class="form-sec">
                       <table width="100%">
                           <tr>
                               <td align="left" colspan="3">
                                   <asp:RadioButtonList ID="RadioButtonList1" runat="server" AutoPostBack="true"
                                       RepeatDirection="Horizontal" onselectedindexchanged="RadioButtonList_SelectedIndexChanged">
                                       <asp:ListItem>Supplier Wise</asp:ListItem>
                                       <asp:ListItem>Medicine Wise</asp:ListItem>
                                       <asp:ListItem>Reagent Wise</asp:ListItem>
                                   </asp:RadioButtonList>
                                </td>
                               <td >
                                   <asp:Label Width="100px" ID="lblComp" runat="server" Visible="false"></asp:Label>
                                   <asp:Label Width="100px" ID="lbltext" runat="server"></asp:Label>
                                   <asp:TextBox ID="TextBox7" runat="server"   Width="" OnTextChanged="supplrchange" AutoPostBack="true" Height="" CssClass="textbox-medium1"></asp:TextBox>
                                   <cc1:AutoCompleteExtender ServiceMethod="SearchSupplierName" OnClientItemSelected="autoCompleteEx_SupplierSelected" MinimumPrefixLength="1"
                                    CompletionInterval="100" EnableCaching="false" CompletionSetCount="10" TargetControlID="TextBox7"  ID="AutoCompleteExtender2" runat="server" 
                                       FirstRowSelected = "false" ></cc1:AutoCompleteExtender>
                                   <asp:TextBox ID="TextBox8" runat="server" Width="" OnTextChanged="medicinechange" AutoPostBack="true" CssClass="textbox-medium1" ></asp:TextBox>
                                   <cc1:AutoCompleteExtender ServiceMethod="SearchMedicineName" OnClientItemSelected="autoCompleteEx_ItemSelected" MinimumPrefixLength="1"
                                    CompletionInterval="100" EnableCaching="false" CompletionSetCount="10" TargetControlID="TextBox8" ID="AutoCompleteExtender1" runat="server" 
                                       FirstRowSelected = "false" ></cc1:AutoCompleteExtender>
                               </td>
                            </tr>
                           <tr>
                                <td>
                                    <strong>From Date :</strong>

                                </td>
                                <td>
                                    <asp:TextBox ID="TextBox5" runat="server"   Width="120px" OnTextChanged="frmdatechange" AutoPostBack="true"></asp:TextBox>  
                                </td>
                                <td>
                                    <strong>To Date :</strong>

                                </td>
                                <td>
                                    <asp:TextBox ID="TextBox6" runat="server"   Width="120px" OnTextChanged="todatechange" AutoPostBack="true"></asp:TextBox>  
                                </td>
                            </tr>
                       </table> 
                </div>
            </div>
        <table width="800px">
          <tr>                       
                   <td colspan="8">
                       <div id="supplier" runat="server" style="width:100%">
                     <div class="formbox"  style='width:100%; height:300px; overflow:auto;' id="46">
                  <asp:GridView id="GridView1" CssClass="grid" PagerStyle-CssClass="pgr" DataKeyNames ="SCode" 
                 runat="server" AutoGenerateColumns="False" AllowPaging="True" 
                 Width="100%" BackColor="LightGoldenrodYellow" BorderColor="Tan" BorderWidth="1px" 
                           CellPadding="2" ForeColor="Black" GridLines="None"    PageSize="1000"
                           SelectedRowStyle-BackColor="GreenYellow" onrowcommand="GridView1_RowCommand"  
                                >
                 
                <Columns> 
                     <asp:CommandField SelectText="Select" ShowSelectButton="True" />
                                
                    <asp:TemplateField HeaderText="Supplier Code" >
                       
                        <ItemTemplate>
                            <asp:Label ID="lblSCode" runat="server" Text='<%# Bind("SCode") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
        
                      <asp:TemplateField HeaderText="Supplier Name">
                       
                        <ItemTemplate>
                            <asp:Label ID="lblSName" runat="server" Text='<%# Bind("SName") %>'></asp:Label>
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
                           </div>
        <div id="medicine" runat="server" style="width:100%;">
                            <div class="formbox"  style='width:100%; height:300px; overflow:auto;' id="47" >
                                <asp:GridView id="GridView2" CssClass="grid" PagerStyle-CssClass="pgr" DataKeyNames ="MedicineID" 
                                 runat="server" AutoGenerateColumns="False" AllowPaging="True" 
                                 Width="100%" BackColor="LightGoldenrodYellow" BorderColor="Tan" BorderWidth="1px" 
                                           CellPadding="2" ForeColor="Black" GridLines="None"    PageSize="1000"
                                           SelectedRowStyle-BackColor="GreenYellow" onrowcommand="GridView2_RowCommand"  >
                 
                                <Columns> 
                                    <asp:TemplateField HeaderText="Select" >
                       
                                        <ItemTemplate>
                                                <asp:LinkButton ID="linkButton" CommandName="Select" CommandArgument='<%#Eval("MedicineID") %>' runat="server">
                                                    <asp:Label ID="select" runat="server" Text='Select'></asp:Label>
                                                </asp:LinkButton>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                
                                    <asp:TemplateField HeaderText="Medicine Code" >
                       
                                        <ItemTemplate>
                                            <asp:Label ID="lblmedId" runat="server" Text='<%# Bind("MedicineID") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
        
                                      <asp:TemplateField HeaderText="Medicine Name">
                       
                                        <ItemTemplate>
                                            <asp:Label ID="lblMedName" runat="server" Text='<%# Bind("MedicineName") %>'></asp:Label>
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
                        </div>
        </td>
        </tr>
       <tr>
       
              <td ><strong>Invoice No :</strong>   </td>
              <td>
                  <asp:TextBox ID="TextBox1" runat="server"   Width="120px"></asp:TextBox> 
              </td>
              
               <td><strong>From Date :</strong>   </td>
               <td>
                   <asp:TextBox ID="TextBox2" runat="server"   Width="120px"></asp:TextBox>  
               </td>
          
            <td>
                 <strong>   
         To Date :</strong>   </td>
          <td>
           <asp:TextBox ID="TextBox4" runat="server"   Width="120px"></asp:TextBox>  
   </td>
               <td ><strong>Total Price :</strong>   </td>
               <td>
                    <asp:TextBox ID="TextBox3" runat="server"   Width="120px"></asp:TextBox>  
               </td>
     
               <td>
                    <div class="form-sec-row"> 
                         <asp:Button ID="Button2" runat="server" Text="Search"   Height="28px" CssClass="submit-button" onclick="Button2_Click" />
                    </div>                  
               </td>             
                 
            </tr>
                
             <tr>                       
                   <td colspan="8">
                       <div class="formbox"  style='width:100%; height:300px; overflow:auto;' id="49">
                  <asp:GridView id="GridView_popup" CssClass="grid" PagerStyle-CssClass="pgr" DataKeyNames ="PurchaseMedicineID" 
                 runat="server" AutoGenerateColumns="False" AllowPaging="True" 
                 Width="100%"   BorderColor="Tan" BorderWidth="1px" 
                           CellPadding="2" ForeColor="Black" GridLines="None"    PageSize="100"
                           SelectedRowStyle-BackColor="GreenYellow" 
                           onpageindexchanging="GridView_popup_PageIndexChanging" 
                               onrowcommand="GridView_popup_RowCommand" onrowdatabound="GridView_popup_RowDataBound"
                                >
                 
                <Columns>
              
        
                     <asp:CommandField SelectText="Select" ShowSelectButton="True" />
                                
                    <asp:TemplateField HeaderText="Invoice ID" >
                       
                        <ItemTemplate>
                            <asp:Label ID="lblid" runat="server" Text='<%# Bind("PurchaseMedicineID") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
        
               <%--       <asp:TemplateField HeaderText="Supplier Name">
                       
                        <ItemTemplate>
                            <asp:Label ID="lblsname" runat="server" Text='<%# Bind("SName") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>--%>

                    <asp:TemplateField HeaderText="Purchase Date">
                       
                        <ItemTemplate>
                            <asp:Label ID="lblpdate" runat="server" Text='<%# Bind("pdate") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>

                         <asp:TemplateField HeaderText="Total Purchase">
                       
                        <ItemTemplate>
                            <asp:Label ID="lbltotalpurchase" runat="server" Text='<%# Bind("Total") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>

                      <%--  <asp:TemplateField HeaderText="Flag" Visible="false" >
                       
                        <ItemTemplate>
                            <asp:Label ID="lblInsertFlag" runat="server" Text='<%# Bind("InsertFlag") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>--%>
                               
                </Columns>
          
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
           <table width="100%">
        <tr>
            <td align="center">
            
                <asp:HiddenField ID="HiddenField1" runat="server" />
                <asp:HiddenField ID="HiddenField2" runat="server" />

            </td>          
            <td>
             <input id="btnClose" type="button" class="submit-button"  style='width:80px;' value="Ok" onclick="CloseDialog();" />
         <input id="Button1" type="button" value="Cancel"   class="submit-button" onclick="return Button1_onclick()"" /> 

                </td>
        </tr>  
       </table>
        </ContentTemplate>
        

        </asp:UpdatePanel>

    
    </div>
    </form>
</body>
</html>
