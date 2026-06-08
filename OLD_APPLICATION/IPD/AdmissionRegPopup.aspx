<%@ Page Language="C#" AutoEventWireup="true" CodeFile="AdmissionRegPopup.aspx.cs" Inherits="IPD_AdmissionRegPopup" %>

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
   
   
    <script src="../Script/jquery-1.4.1.min.js" type="text/javascript"></script>
      
   
    <script src="../Script/jquery.dynDateTime.min.js" type="text/javascript"></script>
    <script src="../Script/calendar-en.min.js" type="text/javascript"></script>
 
    <script type="text/javascript" src="../Script/jquery-ui-1.8.19.custom.min.js"></script>
    <script type="text/javascript" src="../Script/menu.js"></script>
    <script type="text/javascript" src="../Script/jquery.ui.timepicker.js"></script>
 
    <script type="text/javascript" >
        function CloseDialog() {
            //debugger;
            var arg = new Object();
            var val = document.getElementById('HiddenField1').value;
            //alert(val);
            arg.NameValue = document.getElementById('HiddenField1').value;
            arg.ProfessionValue = document.getElementById('HiddenField2').value;
            window.returnValue = arg;
            //alert();
            window.opener.document.getElementById("ctl00_ContentPlaceHolder1_TextBox1").value = arg.NameValue;
            window.close();

        }

        function CloseDialog1() {
            var arg = new Object();
            arg.NameValue = document.getElementById('HiddenField1').value;
            arg.ProfessionValue = document.getElementById('HiddenField2').value;
            window.returnValue = arg;
            window.close();

        }

        function Button1_onclick() {
            window.close();
        }

        function Button5_onclick() {
            window.close();
        }

    </script>
</head>
<body>

    <form id="form1" runat="server">
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
                <asp:Button Text="Existing Patient" BorderStyle="None" ID="Tab1" Width="145px"  Height="40px" 
                    CssClass="Initial" runat="server" onclick="Tab1_Click"
                     /></td>
                     <td>
                <asp:Button Text="From Chamber" BorderStyle="None" ID="Tab2" CssClass="Initial"  Width="145px"  Height="40px" 
                    runat="server" onclick="Tab2_Click"
                    /></td>
           
                     </tr>
                     </table>
                     </div>
                        <div class="formbox">
                     <asp:MultiView ID="MainView" runat="server">
                    <asp:View ID="View1" runat="server">
   
        <table width="100%">
          
       <tr>
                <td>
                    <div class="form-sec-row"> 
             <label class="pname"><strong>Reg No :</strong></label> 
                      <asp:TextBox ID="txtreg" runat="server" CssClass="textbox-medium1" Width="138px" ></asp:TextBox>  <div class="clear">
            </div>
            
</td>
                <td>
                    <div class="form-sec-row"> 
             <label class="pname"><strong>Name :</strong></label> 
                      <asp:TextBox ID="txtname" runat="server" CssClass="textbox-medium1" Width="138px" ></asp:TextBox> 
                      
                      
                <cc1:AutoCompleteExtender ServiceMethod="SearchCustomers"  
                    MinimumPrefixLength="1" CompletionInterval="100" EnableCaching="false" CompletionSetCount="10" 
      TargetControlID="txtname"   ID="AutoCompleteExtender1" runat="server" 
                    FirstRowSelected = "false"  OnClientItemSelected="autoCompleteEx_ItemSelected" 
                    UseContextKey="True">
</cc1:AutoCompleteExtender>
 <div class="clear">
            </div>
            
</td>

<td>
                             <div class="form-sec-row"> 
             <label class="ad"><strong>Address :</strong></label> <asp:TextBox ID="txtaddress" runat="server" CssClass="textbox-medium1" Width="138px" ></asp:TextBox>
            </div>                  
</td>
      
                <td>
                    <div class="form-sec-row"> 
             <label class="pname"><strong>Ph No :</strong></label> <asp:TextBox ID="txtph" runat="server" CssClass="textbox-medium1" Width="138px" ></asp:TextBox>
            </div>
</td>
     
          <td>
                             
           <asp:Button ID="Button2" runat="server" Text="Search"  CssClass="submit-button1" onclick="Button2_Click" 
                                />
                        
</td>             
                      
            </tr>
                
             <tr align="center">                       
                   <td colspan="5">
                   <div style='width:100%; height:300px; overflow:auto;'>
                  <asp:GridView id="GridView1" CssClass="grid" PagerStyle-CssClass="pgr" DataKeyNames ="PatientReg" 
                 runat="server" AutoGenerateColumns="False" AllowPaging="True" PageSize ="100"   SelectedRowStyle-BackColor="GreenYellow"
                 Width="100%" BorderColor="Tan" BorderWidth="1px" 
                           CellPadding="2" ForeColor="Black" GridLines="None" 
                           OnRowCommand="GridView1_RowCommand" 
                           onpageindexchanging="GridView1_PageIndexChanging">
                 
                <Columns>
                    <asp:CommandField SelectText="Select" ShowSelectButton="True" />
                    <asp:TemplateField HeaderText="Registration NO">
                       
                        <ItemTemplate>
                            <asp:Label ID="lblregno" runat="server" Text='<%# Bind("PatientReg") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Patient Name">
                       
                        <ItemTemplate>
                            <asp:Label ID="lblName" runat="server" Text='<%# Bind("patient_name") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Husband Name / C/O">
                       
                        <ItemTemplate>
                            <asp:Label ID="lblfather" runat="server" Text='<%# Bind("HusbandName") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                     <asp:TemplateField HeaderText="Local Address" ControlStyle-Width="200px">
                       
                        <ItemTemplate>
                            <asp:Label ID="lblladd" runat="server" Text='<%# Bind("vill_city") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
    
                    <asp:TemplateField HeaderText="Phone - 1">
                       
                        <ItemTemplate>
                            <asp:Label ID="lblph1" runat="server" Text='<%# Bind("PhNo1") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="Phone - 2">
                       
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
      
               
                    </asp:View>


                    
                    <asp:View ID="View2" runat="server">
                    
        <table width="100%">
          
       <tr>
                <td>
                    <div class="form-sec-row"> 
             <label class="pname"><strong>Reg No :</strong></label> 
                      <asp:TextBox ID="TextBox1" runat="server" CssClass="textbox-medium1" Width="138px" ></asp:TextBox>  <div class="clear">
            </div>
            
</td>
                <td>
                    <div class="form-sec-row"> 
             <label class="pname"><strong>Name :</strong></label> 
                      <asp:TextBox ID="TextBox2" runat="server" CssClass="textbox-medium1" Width="138px" ></asp:TextBox>  <div class="clear">
            </div>
            
</td>

<td>
                             <div class="form-sec-row"> 
             <label class="ad"><strong>Address :</strong></label> <asp:TextBox ID="TextBox3" runat="server" CssClass="textbox-medium1" Width="138px" ></asp:TextBox>
            </div>                  
</td>
      
                <td>
                    <div class="form-sec-row"> 
             <label class="pname"><strong>Ph No :</strong></label> <asp:TextBox ID="TextBox4" runat="server" CssClass="textbox-medium1" Width="138px" ></asp:TextBox>
            </div>
</td>
     
          <td>
                         
           <asp:Button ID="Button3" runat="server" Text="Search"  CssClass="submit-button1" onclick="Button3_Click"  
                                />
                        
</td>             
                      
            </tr>
                
             <tr align="center">                       
                   <td colspan="5">
                   <div style='width:100%; height:300px; overflow:auto;'>
                  <asp:GridView id="GridView2" CssClass="grid" PagerStyle-CssClass="pgr" DataKeyNames ="PatientRegNo" 
                 runat="server" AutoGenerateColumns="False" AllowPaging="True" PageSize ="100"   SelectedRowStyle-BackColor="GreenYellow"
                 Width="100%" BorderColor="Tan" BorderWidth="1px" 
                           CellPadding="2" ForeColor="Black" GridLines="None" 
                           OnRowCommand="GridView2_RowCommand" 
                           onpageindexchanging="GridView2_PageIndexChanging">
                 
                <Columns>
                    <asp:CommandField SelectText="Select" ShowSelectButton="True" />
                    <asp:TemplateField HeaderText="Registration NO">
                       
                        <ItemTemplate>
                            <asp:Label ID="lblregno" runat="server" Text='<%# Bind("PatientRegNo") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Patient Name">
                       
                        <ItemTemplate>
                            <asp:Label ID="lblName" runat="server" Text='<%# Bind("PName") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Guadian Name">
                       
                        <ItemTemplate>
                            <asp:Label ID="lblfather" runat="server" Text='<%# Bind("GuadianName") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                     <asp:TemplateField HeaderText="Local Address" ControlStyle-Width="200px">
                       
                        <ItemTemplate>
                            <asp:Label ID="lblladd" runat="server" Text='<%# Bind("Address") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
          
                    <asp:TemplateField HeaderText="Phone - 1">
                       
                        <ItemTemplate>
                            <asp:Label ID="lblph1" runat="server" Text='<%# Bind("PhNo1") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="Phone - 2">
                       
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
      
                    </asp:View>
  
                </asp:MultiView>

                <table width="100%">
                 <tr>
            <td   align="center">
            
                <asp:HiddenField ID="HiddenField1" runat="server" />
                <asp:HiddenField ID="HiddenField2" runat="server" />

            </td>
            <td ><input id="btnClose" type="button" class="submit-button" value="Ok" style='width:75px; height:30px;'  onclick="CloseDialog();" /> 
         

                
             <input id="Button1" type="button" value="Cancel" class="submit-button" style='width:75px; height:30px;' onclick="return Button1_onclick()" /> 

                </td>
        </tr> 
                </table>
                </div>
                  </ContentTemplate>
               </asp:UpdatePanel>
    </form>
</body>
</html>
