<%@ Page Language="C#" AutoEventWireup="true" CodeFile="IpdpatientPopup.aspx.cs" Inherits="IPD_IpdpatientPopup" %>
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

    <style type="text/css">
      body{
  width: 90%; 
  left: 5%; 
  
  margin-left:auto;
  margin-right:auto; 
}
    </style>
    <script type="text/javascript" >

        function Calling() {
            var date = new Date();
            $("input[id$='txtdate']").datepicker({ dateFormat: 'dd/mm/yy' });
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
        function CloseDialog() {
            var arg = new Object();
            arg.NameValue = document.getElementById('HiddenField1').value;
            arg.ProfessionValue = document.getElementById('HiddenField2').value;
            //window.returnValue = arg;
            window.opener.document.getElementById("ctl00_ContentPlaceHolder1_TextBox1").value = arg.NameValue;
            window.close();
        }

        function Button1_onclick() {
            window.close();
        }

        function SetContextKey() {
            $find('AutoCompleteExtender1').set_contextKey("GFC");
        }

        function autoCompleteEx_ItemSelected(sender, args) {
            var regname = args.get_value().split('-');
            //document.getElementById("ctl00_ContentPlaceHolder1_txtname").value = regname[0];
            document.getElementById("txtname").value = regname[0];
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
                    <div class="form-sec-row"> 
             <label class="pname"><strong>Reg No :</strong></label> 
                      <asp:TextBox ID="txtreg" runat="server" CssClass="textbox-medium1" Width="138px" ></asp:TextBox>  <div class="clear">
            </div>
            
</td>
                <td>
                    <div class="form-sec-row"> 
             <label class="pname"><strong>Name :</strong></label> 
                      <asp:TextBox ID="txtname" runat="server" CssClass="textbox-medium1" Width="138px" ></asp:TextBox>  <div class="clear">

                    <cc1:AutoCompleteExtender ServiceMethod="SearchPatientName"    OnClientItemSelected="autoCompleteEx_ItemSelected"    MinimumPrefixLength="1"
           CompletionInterval="100" EnableCaching="false" 
           CompletionSetCount="10" TargetControlID="txtname"  ID="AutoCompleteExtender1" runat="server" FirstRowSelected = "false" ></cc1:AutoCompleteExtender>
            </div>
            
</td>

<td>
                             <div class="form-sec-row"> 
             <label class="ad"><strong>Address :</strong></label> <asp:TextBox ID="txtaddress" runat="server" CssClass="textbox-medium1" Width="138px" ></asp:TextBox>
            </div>                  
</td>
      
                <td>
                    <div class="form-sec-row"> 
             <label class="pname"><strong>Bed No :</strong></label> <asp:TextBox ID="txtph" runat="server" CssClass="textbox-medium1" Width="138px" ></asp:TextBox>
            </div>
</td>
     
          <td>
                             <div class="form-sec-row"> 
           <asp:Button ID="Button2" runat="server" Text="Search"  Height="28px" CssClass="submit-button1" onclick="Button2_Click" 
                                />
            </div>                  
</td>             
                      
            </tr>
                
             <tr align="center">                       
                   <td colspan="5"  >
                   <div  style='width:100%; height:auto; overflow:auto;'>
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
                    <asp:TemplateField HeaderText="Guadian Name">
                       
                        <ItemTemplate>
                            <asp:Label ID="lblfather" runat="server" Text='<%# Bind("guardian_name") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                     <asp:TemplateField HeaderText="Local Address">
                       
                        <ItemTemplate>
                            <asp:Label ID="lblladd" runat="server" Text='<%# Bind("vill_city") %>'></asp:Label>
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
            
                <asp:HiddenField ID="HiddenField1" runat="server" />
                <asp:HiddenField ID="HiddenField2" runat="server" />

            </td>
            <td align="left" >
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
