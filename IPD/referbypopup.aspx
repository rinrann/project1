<%@ Page Language="C#" AutoEventWireup="true" CodeFile="referbypopup.aspx.cs" Inherits="IPD_referbypopup" %>

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
    <script type="text/javascript" language="javascript" >
        function Calling() {
            var date = new Date();
            $("input[id$='txtdate']").datepicker({ dateFormat: 'dd/mm/yy' });



            $("input[id$='Button5']").click(function () {

                if ($("input[id$='TextBox10']").val() == '') {
                    alert('Please Enter Name Properly !');
                    $("input[id$='TextBox10']").focus();
                    $("input[id$='TextBox10']").addClass('textboxerr');
                    return false;
                }
                else {
                    $("input[id$='TextBox10']").removeClass('textboxerr');
                }
            });

            $("input[id$='Button2']").click(function () {

                if ($("select[id$='DropDownList1']").val() == 'D' && $("select[id$='DropDownList4']").val() == '0') {
                    alert('Please Select Doctor Type !');
                    $("select[id$='DropDownList4']").addClass('textboxerr');
                    $("select[id$='DropDownList4']").focus();
                    return false;
                }
                else {
                    $("select[id$='DropDownList4']").removeClass('textboxerr');
                }

                if ($("input[id$='TextBox1']").val() == '') {
                    alert('Please Enter Name Properly !');
                    $("input[id$='TextBox1']").focus();
                    $("input[id$='TextBox1']").addClass('textboxerr');
                    return false;
                }
                else {
                    $("input[id$='TextBox1']").removeClass('textboxerr');
                }

                if ($("input[id$='TextBox2']").val() == '') {
                    alert('Please Enter Address - 1 !');
                    $("input[id$='TextBox2']").focus();
                    $("input[id$='TextBox2']").addClass('textboxerr');
                    return false;
                }
                else {
                    $("input[id$='TextBox2']").removeClass('textboxerr');
                }

                /*if ($("input[id$='TextBox3']").val() == '') {
                    alert('Please Enter Address - 2!');
                    $("input[id$='TextBox3']").focus();
                    $("input[id$='TextBox3']").addClass('textboxerr');
                    return false;
                }
                else {
                    $("input[id$='TextBox3']").removeClass('textboxerr');
                }*/



                if ($("input[id$='TextBox7']").val() == '') {
                    alert('Please Enter Phone -1!');
                    $("input[id$='TextBox7']").focus();
                    $("input[id$='TextBox7']").addClass('textboxerr');
                    return false;
                }
                else {
                    $("input[id$='TextBox7']").removeClass('textboxerr');
                }




                if ($("select[id$='DropDownList2']").val() == '0') {
                    alert('Please Select District !');
                    $("select[id$='DropDownList2']").addClass('textboxerr');
                    $("select[id$='DropDownList2']").focus();
                    return false;
                }
                else {
                    $("select[id$='DropDownList2']").removeClass('textboxerr');
                }


            });

            $("input[id$='TextBox5']").keydown(function (event) {
                if (event.keyCode == 46 || event.keyCode == 8 || event.keyCode == 9 || event.keyCode == 27 || event.keyCode == 13 ||
                        (event.keyCode == 65 && event.ctrlKey === true) || (event.keyCode >= 35 && event.keyCode <= 39)) {
                    return;
                }
                else {
                    if (event.shiftKey || (event.keyCode < 48 || event.keyCode > 57) && (event.keyCode < 96 || event.keyCode > 105)) {
                        event.preventDefault();
                    }
                }
            });

            $("input[id$='TextBox7']").keydown(function (event) {
                if (event.keyCode == 46 || event.keyCode == 8 || event.keyCode == 9 || event.keyCode == 27 || event.keyCode == 13 ||
                        (event.keyCode == 65 && event.ctrlKey === true) || (event.keyCode >= 35 && event.keyCode <= 39)) {
                    return;
                }
                else {
                    if (event.shiftKey || (event.keyCode < 48 || event.keyCode > 57) && (event.keyCode < 96 || event.keyCode > 105)) {
                        event.preventDefault();
                    }
                }
            });




            $("input[id$='TextBox9']").keydown(function (event) {
                if (event.keyCode == 46 || event.keyCode == 8 || event.keyCode == 9 || event.keyCode == 27 || event.keyCode == 13 ||
                        (event.keyCode == 65 && event.ctrlKey === true) || (event.keyCode >= 35 && event.keyCode <= 39)) {
                    return;
                }
                else {
                    if (event.shiftKey || (event.keyCode < 48 || event.keyCode > 57) && (event.keyCode < 96 || event.keyCode > 105)) {
                        event.preventDefault();
                    }
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

        function CloseDialog() {
            var arg = new Object();
            arg.NameValue = document.getElementById('HiddenField1').value;
            arg.ProfessionValue = document.getElementById('HiddenField2').value;
            var a = rtvalue.ProfessionValue.split("#");
            //window.returnValue = arg;
            window.opener.document.getElementById("ctl00_ContentPlaceHolder1_HiddenField3").value = arg.NameValue;
            window.opener.document.getElementById("ctl00_ContentPlaceHolder1_TextBox21").value = a[0];
            window.opener.document.getElementById("ctl00_ContentPlaceHolder1_HiddenField5").value = a[1];
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
                    <div class="form-sec-row"> 
             <label class="pname"><strong>Refer By :</strong></label> 
                        <asp:DropDownList ID="DropDownList1" Width="140px" runat="server" 
                            AutoPostBack="True" 
                            onselectedindexchanged="DropDownList1_SelectedIndexChanged">
                        </asp:DropDownList>
                        <div class="clear">
            </div>
            
</td>
                <td>
                    <div class="form-sec-row"> 
             <label class="pname"><strong>Search Text :</strong></label> 
                      <asp:TextBox ID="txtname" runat="server" CssClass="textbox-medium1" Width="160px" ></asp:TextBox>  <div class="clear">
            </div>
            
</td>
<td><asp:Button ID="Button4" runat="server" Text="Search" CssClass="submit-button" 
        onclick="Button4_Click" />  </td>
 
 
             
                      
            </tr>
                
              <tr>
            <td align="center" >
            
                <asp:HiddenField ID="HiddenField1" runat="server" />
                </td>
                 <td align="center" >
                <asp:HiddenField ID="HiddenField2" runat="server" />

            </td>
            </tr>
        
       </table>


        <div class="formbox">
            <asp:Panel ID="Panel1" runat="server">
              <table width="195px" cellpadding="0" cellspacing="0">
      <%--  <tr>
        <td colspan="6">
        <br /><br />
        </td>
        </tr>--%>
        <tr>
            <td>
                <asp:Button Text="List" BorderStyle="None" ID="Tab1" Width="85px"  Height="40px" 
                    CssClass="Initial" runat="server" onclick="Tab1_Click"
                     /></td>
                     <td>
                <asp:Button Text="New Entry" BorderStyle="None" ID="Tab2" CssClass="Initial"  Width="110px"  Height="40px" 
                    runat="server" onclick="Tab2_Click"
                    /></td>
                    
             
                     </tr>
                     </table>
                    
                <asp:MultiView ID="MainView" runat="server">
                    <asp:View ID="View1" runat="server">
              
      
                        <table style="width:100%; border-width: 1px; border-color: #666; border-style: solid">
                        <tr>
                        <td>
                        <div class="listing" style='width:100%; height:300px; overflow:auto;'>
         <asp:GridView id="GridView1" CssClass="grid" PagerStyle-CssClass="pgr" DataKeyNames ="ID" 
                 runat="server" AutoGenerateColumns="False" AllowPaging="True" PageSize ="100"   SelectedRowStyle-BackColor="GreenYellow"
                 Width="100%" BorderColor="Tan" BorderWidth="1px" 
                           CellPadding="2" ForeColor="Black" GridLines="None" 
                           OnRowCommand="GridView1_RowCommand" 
                           onpageindexchanging="GridView1_PageIndexChanging">
                 
                <Columns>
                    <asp:CommandField SelectText="Select" ShowSelectButton="True" />
                    <asp:TemplateField HeaderText="ID" Visible="false"><ItemTemplate><asp:Label ID="lblregno" runat="server" Text='<%# Bind("ID") %>'></asp:Label></ItemTemplate></asp:TemplateField>
                    <asp:TemplateField HeaderText="NAME"><ItemTemplate><asp:Label ID="lblName" runat="server" Text='<%# Bind("NAME") %>'></asp:Label></ItemTemplate></asp:TemplateField>
                    <asp:TemplateField HeaderText="ADDRESS 1"><ItemTemplate><asp:Label ID="lblADDRESS1" runat="server" Text='<%# Bind("ADDRESS1") %>'></asp:Label></ItemTemplate></asp:TemplateField>
                     <asp:TemplateField HeaderText="ADDRESS 2" ><ItemTemplate><asp:Label ID="lblladd" runat="server" Text='<%# Bind("ADDRESS2") %>'></asp:Label></ItemTemplate></asp:TemplateField>

                        <asp:TemplateField HeaderText="PIN" ><ItemTemplate><asp:Label ID="lblPIN" runat="server" Text='<%# Bind("PIN") %>'></asp:Label></ItemTemplate></asp:TemplateField>
    
                    <asp:TemplateField HeaderText="PHONE - 1"><ItemTemplate><asp:Label ID="lblph1" runat="server" Text='<%# Bind("PHONE1") %>'></asp:Label></ItemTemplate></asp:TemplateField>

                    <asp:TemplateField HeaderText="PHONE - 2"><ItemTemplate><asp:Label ID="lblph2" runat="server" Text='<%# Bind("PHONE2") %>'></asp:Label></ItemTemplate></asp:TemplateField>
       

                </Columns>
                <AlternatingRowStyle BackColor="PaleGoldenrod" />
                      <FooterStyle BackColor="Tan" />
             <HeaderStyle BackColor="Tan" Font-Bold="True" />
                      <PagerStyle BackColor="PaleGoldenrod" ForeColor="DarkSlateBlue" 
                          HorizontalAlign="Center" />
                      <SelectedRowStyle ForeColor="GhostWhite" />
                      <SortedAscendingCellStyle BackColor="#FAFAE7" />
                      <SortedAscendingHeaderStyle BackColor="#DAC09E" />
                      <SortedDescendingCellStyle BackColor="#E1DB9C" />
                      <SortedDescendingHeaderStyle BackColor="#C2A47B" />
        </asp:GridView> 
        </div>
                        </td>
                        </tr>
                                      </table>

                                           <table>
                    <tr>
            <td align="center"> 
            <input id="btnClose" type="button" class="submit-button" value="Ok" onclick="CloseDialog();" />
 
            </td>
            <td align="center">

                
             <input id="Button1" type="button" value="Cancel" class="submit-button" onclick="return Button1_onclick()"" /> 

                </td>
        </tr> 
               </table>

               
                    </asp:View>
                    <asp:View ID="View2" runat="server">
                     <table   cellpadding="0" cellspacing="0" width="100%">

                            <tr>
                   <td style='width:120px;'><label><strong></strong></label></td>
                   <td  align="center"> 
                       <asp:Label ID="Label1" runat="server" ></asp:Label></td>
                            </tr>


                            
                                          <tr> 
                   <td style='width:120px;'><asp:Label ID="lbldoctype" runat="server"><strong> Doctor Type :</strong></asp:Label></td>
                   <td> 
                       <asp:DropDownList ID="DropDownList4" Width="150px"  CssClass="textbox-medium1" runat="server">
                       </asp:DropDownList>
                   </td>
                 
                            </tr>



                            <tr>
                   <td style='width:120px;'><label><strong> Name :</strong></label></td>
                   <td> 
                       <asp:TextBox ID="TextBox1" Width="150px"    CssClass="textbox-medium1" runat="server"></asp:TextBox> </td>
                            </tr>

                                          <tr>
                   <td style='width:120px;'><label><strong> Address<!-- - 1 -->:</strong></label></td>
                   <td> 
                       <asp:TextBox ID="TextBox2" Width="150px"  CssClass="textbox-medium1" runat="server"></asp:TextBox> </td>
                            </tr>

                                          <tr style='display:none;'>
                   <td style='width:120px;'><label><strong> Address - 2 :</strong></label></td>
                   <td> 
                       <asp:TextBox ID="TextBox3" Width="150px"   CssClass="textbox-medium1" runat="server"></asp:TextBox> </td>
                            </tr>


                                          <tr>
                   <td style='width:120px;'><label><strong> District :</strong></label></td>
                   <td> 
                       <asp:DropDownList ID="DropDownList2"   CssClass="textbox-medium1" Width="150px" runat="server">
                       </asp:DropDownList>
                   </td>
                            </tr>
 

                                          <tr>
                   <td style='width:120px;'><label><strong> Pin :</strong></label></td>
                   <td> 
                       <asp:TextBox ID="TextBox5" Width="150px"  MaxLength="6"   CssClass="textbox-medium1" runat="server"></asp:TextBox> </td>
                            </tr>
                                          <tr>
                   <td style='width:120px;'><label><strong> Phone<!-- - 1 -->:</strong></label></td>
                   <td> 
                       <asp:TextBox ID="TextBox6" Width="30px"   CssClass="textbox-medium1"  Text="+91"  Enabled="False"  runat="server"></asp:TextBox>
                        <asp:TextBox ID="TextBox7"    CssClass="textbox-medium1"  MaxLength="10" Width="120px" runat="server"></asp:TextBox> </td>
                            </tr>

                                          <tr style='display:none;'>
                   <td style='width:120px;'><label><strong> Phone - 2 :</strong></label></td>
                   <td> 
                         <asp:TextBox ID="TextBox8" Width="30px" Text="+91"  Enabled="False"   CssClass="textbox-medium1" runat="server"></asp:TextBox> 
                         <asp:TextBox ID="TextBox9"     CssClass="textbox-medium1"  MaxLength="10"  Width="120px" runat="server"></asp:TextBox> </td>
                            </tr>


                                      <tr>
                   <td style='width:120px;'><asp:Label ID="lblvchltype" runat="server"><strong> Type of Vahicle :</strong></asp:Label></td>
                   <td> 
                       <asp:DropDownList ID="DropDownList3" Width="150px"   CssClass="textbox-medium1"  runat="server">
                           <asp:ListItem>--Select--</asp:ListItem>
                           <asp:ListItem>AMBULANCE</asp:ListItem>
                           <asp:ListItem>PRIVATE</asp:ListItem>
                           <asp:ListItem>RENTED</asp:ListItem>
                       </asp:DropDownList>
                   </td>
                            </tr>


                            
                                          <tr>
                   <td style='width:120px;'><asp:Label ID="lblvchlno" runat="server"><strong> Vahicle No :</strong></asp:Label></td>
                   <td> 
                       <asp:TextBox ID="TextBox4" Width="150px"   CssClass="textbox-medium1" runat="server"></asp:TextBox> </td>
                            </tr>




                              <tr>
                   <td style='width:120px;'> </td>
                   <td>    <asp:Button ID="Button2" runat="server" CssClass="submit-button"  Height="28px"
                           Text="Submit" onclick="Button2_Click" />  <asp:Button ID="Button3"  Height="28px"
                           runat="server" CssClass="submit-button" Text="Cancel" onclick="Button3_Click" />
                      </td>
                            </tr>
                </table>
                    </asp:View>
                    
               </asp:MultiView>
            </asp:Panel>
            
            <asp:Panel ID="Panel2" runat="server">
            <table width="100%" cellpadding="0" cellspacing="0">
                                          <tr>
                   <td style='width:120px;'><label><strong> Enter Name :</strong></label></td>
                   <td> 
                       <asp:TextBox ID="TextBox10" Width="150px" runat="server"></asp:TextBox> </td>
                            </tr>
                            <tr>
                            <td align="center"> 
                                <asp:Button ID="Button5" runat="server" CssClass="submit-button1" Text="ok"  
                                    onclick="Button5_Click" /> </td>
                                <td>
                                    <asp:Button ID="Button6" runat="server"  CssClass="submit-button" Text="cancel" /></td>
                            </tr>
                            </table>
            </asp:Panel>
          
          </div>
        </ContentTemplate>
        

        </asp:UpdatePanel>

    
    </div>
    </form>
</body>
</html>
