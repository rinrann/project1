<%@ Page Language="C#" AutoEventWireup="true" CodeFile="UnderDoctorPopup.aspx.cs" Inherits="IPD_UnderDoctorPopup" %>

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
            //window.returnValue = arg;
            var a = arg.NameValue.split("#");
            window.opener.document.getElementById("ctl00_ContentPlaceHolder1_HiddenField4").value = a[0];
            window.opener.document.getElementById("ctl00_ContentPlaceHolder1_TextBox18").value = a[1];
            window.close();

        }

        function Button1_onclick() {
            window.close();
        }

    </script>



    <script language="javascript" type="text/javascript">
        function DisableBackButton() {
            window.history.forward()
        }
        DisableBackButton();
        window.onload = DisableBackButton;
        window.onpageshow = function (evt) { if (evt.persisted) DisableBackButton() }
        window.onunload = function () { void (0) }

        function Calling() {
       
                $("input[id$='btnSubmit']").click(function () {
                    if ($("input[id$='txtPhNo1']").val() == '') {
                        alert('Phone No-1 Can not be Blank!');
                        $("input[id$='txtPhNo1']").focus();
                        $("input[id$='txtPhNo1']").addClass('textboxerr');
                        return false;
                    }
                    else {
                        $("input[id$='txtPhNo1']").removeClass('textboxerr');
                    }

                    if ($("input[id$='txtDoctorName']").val() == '') {
                        alert('Doctor Name Can not be Blank!');
                        $("input[id$='txtDoctorName']").focus();
                        $("input[id$='txtDoctorName']").addClass('textboxerr');
                        return false;
                    }
                    else {
                        $("input[id$='txtDoctorName']").removeClass('textboxerr');
                    }

                
                var e = document.getElementById("ddlDoctorType");
                var strUser = e.options[e.selectedIndex].text;

                if (strUser == '--Select--') {
                    alert('Select Doctor Type');
                    $(e).focus();
                    $(e).addClass('textboxerr');
                    return false;
                }
                else {
                    $(e).removeClass('textboxerr');
                }
                var e = document.getElementById("ddlDistrict");
                var strUser = e.options[e.selectedIndex].text;

                if (strUser == '--Select--') {
                    alert('Select District');
                    $(e).focus();
                    $(e).addClass('textboxerr');
                    return false;
                }
                else {
                    $(e).removeClass('textboxerr');
                }

            });
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
       
       
<%--For Busy Loader .............................--%>


<%-- 
  <asp:UpdateProgress ID="UpdateProgress1" runat="server">
    <ProgressTemplate>    
       <div id="aaa" class="progressBackgroundFilter">          </div>
        <div id="bbb" class="processMessage"><img alt="Loading" src="../images/pwait.gif"/></div>
    </ProgressTemplate>
    </asp:UpdateProgress>--%>
    <%--For Busy Loader End.............................--%>

        <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Always">
        <ContentTemplate>
       

            <div class="formbox">
         <table width="290px" cellpadding="0" cellspacing="0">
         <tr>
            <td>
                <asp:Button Text="List" BorderStyle="None" ID="Tab1" Width="145px"  Height="40px" 
                    CssClass="Initial" runat="server" onclick="Tab1_Click"
                     /></td>
                     <td>
                <asp:Button Text="Entry" BorderStyle="None" ID="Tab2" CssClass="Initial"  Width="145px"  Height="40px" 
                    runat="server" onclick="Tab2_Click"
                    /></td>
           
                     </tr>
                     </table>
                     </div>
  <div class="formbox">
         <asp:MultiView ID="MainView" runat="server">
                    <asp:View ID="View1" runat="server">
                     <table>
          
       <tr>
    
              <td > Doctor Type :   </td >  <td >
                        <asp:DropDownList ID="DropDownList1" Width="150px" runat="server" CssClass="textbox-dropdown1">
                        </asp:DropDownList>
                 
</td>
                <td >
                   
            Doctor Name :   </td >  <td >
                        <asp:TextBox ID="TextBox1" runat="server" CssClass="textbox-medium1" Width="150px"></asp:TextBox>  
</td>
         

     
          <td>   
                             
           <asp:Button ID="Button2" runat="server" Text="Search"   Height="28px"  CssClass="submit-button" onclick="Button2_Click" 
                                />
                        
</td>             
                      
            </tr>
                
             <tr align="center">                       
                   <td colspan="5">

                   <div style='width:100%; height:300px; overflow:auto;'>
                  <asp:GridView id="GridView_popup" CssClass="grid" PagerStyle-CssClass="pgr" DataKeyNames ="doc_id" 
                 runat="server" AutoGenerateColumns="False" AllowPaging="True" 
                 Width="693px" BackColor="LightGoldenrodYellow" BorderColor="Tan" BorderWidth="1px" 
                           CellPadding="2" ForeColor="Black" GridLines="None"    PageSize="100"
                           
                           onpageindexchanging="GridView_popup_PageIndexChanging" onrowcommand="GridView_popup_RowCommand"
                                >
                 
                <Columns>
              
                     <asp:CommandField SelectText="Select" ShowSelectButton="True" />
                                
                    <asp:TemplateField HeaderText="Doctor ID" Visible="false">
                       
                        <ItemTemplate>
                            <asp:Label ID="lbldocid" runat="server" Text='<%# Bind("doc_id") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Doctor Name">
                       
                        <ItemTemplate>
                            <asp:Label ID="lbldocName" runat="server" Text='<%# Bind("doc_name") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                               
                </Columns>
                <AlternatingRowStyle BackColor="PaleGoldenrod" />
                      <FooterStyle BackColor="Tan" />
             <HeaderStyle BackColor="Tan" Font-Bold="True" />
                      <PagerStyle BackColor="PaleGoldenrod" ForeColor="DarkSlateBlue" 
                          HorizontalAlign="Center" />
                      <SelectedRowStyle BackColor="GreenYellow" ForeColor="GhostWhite" />
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
              <td  >
             <input id="btnClose" type="button" class="submit-button" value="Ok" onclick="CloseDialog();" />
              <input id="Button1" type="button" value="Cancel" class="submit-button" onclick="return Button1_onclick()"" /> 

                </td>
        </tr>  
       </table>

                    </asp:View>
                     <asp:View ID="View2" runat="server">
                     <table   cellpadding="0" cellspacing="0" width="100%">

                            <tr>
                   <td style='width:120px;'></td>
                   <td  align="center"> 
                       <asp:Label ID="Label1" runat="server" ></asp:Label></td>
                            </tr>


                            
                                          <tr> 
                   <td style='width:180px;'>Doctor Type :</td>
                   <td> 
                       <asp:DropDownList ID="ddlDoctorType" Width="155px" runat="server">
                       </asp:DropDownList>
                   </td>
                 
                            </tr>



                            <tr>
                   <td style='width:120px;'>Name :</td>
                   <td> 
                       <asp:TextBox ID="txtDoctorName" Width="153px"   runat="server"></asp:TextBox> </td>
                            </tr>

                                          <tr>
                   <td style='width:120px;'>Address<!-- - 1--> :</td>
                   <td> 
                       <asp:TextBox ID="txtAddress1" Width="153px"  runat="server"></asp:TextBox> </td>
                            </tr>

                                          <tr style='display:none;'>
                   <td style='width:120px;'>Address - 2 :</td>
                   <td> 
                       <asp:TextBox ID="txtAddress2" Width="153px"   runat="server"></asp:TextBox> </td>
                            </tr>


                                          <tr>
                   <td style='width:120px;'> District :</td>
                   <td> 
                       <asp:DropDownList ID="ddlDistrict" Width="155px" runat="server">
                       </asp:DropDownList>
                   </td>
                            </tr>
 

                                          <tr>
                   <td style='width:120px;'>Pin Code:</td>
                   <td> 
                       <asp:TextBox ID="txtPin" Width="153px"  MaxLength="6"   runat="server"></asp:TextBox> </td>
                            </tr>
                                          <tr>
                   <td style='width:120px;'>Phone<!-- - 1 -->:</td>
                   <td> 
                         <asp:TextBox ID="txtPhPrefix1" Width="25px" Text="+91"  Enabled="False" runat="server"></asp:TextBox> 
                         <asp:TextBox ID="txtPhNo1"     MaxLength="10"  Width="122px" runat="server"></asp:TextBox>  </td>
                            </tr>

                                          <tr style='display:none;'>
                   <td style='width:120px;'>Phone - 2 :</td>
                   <td> 
                         <asp:TextBox ID="txtPhPrefix2" Width="25px" Text="+91"  Enabled="False"  runat="server"></asp:TextBox> 
                         <asp:TextBox ID="txtPhNo2"    MaxLength="10"  Width="122px" runat="server"></asp:TextBox> </td>
                            </tr>

                             <tr>
                   <td style='width:120px;'> </td>
                   <td>    <asp:Button ID="btnSubmit" runat="server" CssClass="submit-button"  Height="28px"
                           Text="Submit" onclick="btnSubmit_Click" /> 
                            <asp:Button ID="btnClear"  Height="28px"
                           runat="server" CssClass="submit-button" Text="Clear" onclick="btnClear_Click" />
                      </td>
                            </tr>
                </table>
                    </asp:View>
                    </asp:MultiView>
        </div>
        </ContentTemplate>
        

        </asp:UpdatePanel>

    
    </div>
    </form>
</body>
</html>
