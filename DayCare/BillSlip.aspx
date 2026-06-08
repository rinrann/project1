<%@ Page Title="" Language="C#" MasterPageFile="~/MasterAll/MasterPageAll.master" AutoEventWireup="true" CodeFile="BillSlip.aspx.cs" Inherits="DayCare_BillSlip" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
   
<script language="javascript" type="text/javascript">
    function printDiv(divID) {
        var divElements = document.getElementById("ctl00_ContentPlaceHolder1_mydiv").innerHTML;
        var oldPage = document.body.innerHTML;
        document.body.innerHTML ="<html><head><title></title></head><body>" + divElements + "</body>";
        window.print();
        document.body.innerHTML = oldPage;
    }

    function ShowDialog() {

        var rtvalue = window.open("RegistrationPopup.aspx", "sss", "Width:800px; Height:550px; dialogLeft:250px;");
        //document.getElementById("ctl00_ContentPlaceHolder1_txtreg").value = rtvalue.NameValue;
    }

    function ShowDialogDuplicate() {

        var rtvalue = window.open("AppointmentPopup.aspx", "sss", "Width:800px; Height:550px; dialogLeft:250px;");
        //document.getElementById("ctl00_ContentPlaceHolder1_TextBox1").value = rtvalue.NameValue;
        window.opener.document.getElementById("ctl00_ContentPlaceHolder1_txtreg").value = arg.NameValue;
    }


    function Calling() {
        $("input[id$='Button4']").click(function () {
            if ($("input[id$='txtreg']").val() == '') {
                alert('Please Select A Patient !');
                $("input[id$='txtreg']").focus();
                $("input[id$='txtreg']").addClass('textboxerr');
                return false;
            }
            else {
                $("input[id$='txtreg']").removeClass('textboxerr');
            }
        });


        $("input[id$='Button2']").click(function () {
            if ($("input[id$='TextBox2']").val() == '') {
                alert('Please Select A Patient !');
                $("input[id$='TextBox2']").focus();
                $("input[id$='TextBox2']").addClass('textboxerr');
                return false;
            }
            else {
                $("input[id$='TextBox2']").removeClass('textboxerr');
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

 <asp:UpdateProgress ID="UpdateProgress1" runat="server">
    <ProgressTemplate>
    
       <div id="aaa" class="progressBackgroundFilter">          </div>
        <div id="bbb" class="processMessage"><img alt="Loading" src="../images/pwait.gif"/>
      
              </div>
    </ProgressTemplate>
    </asp:UpdateProgress>


    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
    <ContentTemplate>
     <div class="pageheader">
         <asp:Label ID="Label1" runat="server">Bill Slip</asp:Label>
     </div>
     <div class="formbox">
        <div class="form-sec">
            <div class="error">
                <strong><asp:Label ID="lblError" runat="server" Font-Bold="True"></asp:Label></strong>
                <div class="clear"></div>
            </div></div>





             <table width="100%">
             <tr>
           <td colspan="6" align="center">
               <asp:RadioButtonList ID="RadioButtonList2" runat="server"  Width="250px"
                   RepeatDirection="Horizontal" AutoPostBack="True" 
                   onselectedindexchanged="RadioButtonList2_SelectedIndexChanged">
                   <asp:ListItem>Current Report</asp:ListItem>
                   <asp:ListItem>Duplicate Report</asp:ListItem>
               </asp:RadioButtonList>
               </td>
           </tr>
           </table>

         <asp:Panel ID="Panel1" runat="server">
         <table width="100%">

           <tr>
   
           <td>  <label><strong>Registration No :</strong></label> </td>
           <td>  
               <asp:TextBox ID="txtreg" CssClass="textbox-medium1"  runat="server" 
                   Width="150px" ></asp:TextBox></td>
           <td> <asp:Button ID="Button3" runat="server" Text="Search"  CssClass="submit-button"  OnClientClick="ShowDialog()"/></td>
        
              <td>  <label><strong>Select Language :</strong></label> </td>
                 <td>  
                     <asp:DropDownList ID="DropDownList1" runat="server" CssClass="textbox-medium1" 
                         Width="150px" >
                         <asp:ListItem Value="0">-- Select --</asp:ListItem>
                         <asp:ListItem Value="1">Bengali</asp:ListItem>
                         <asp:ListItem Value="2">English</asp:ListItem>
                         <asp:ListItem Value="3">Hindi</asp:ListItem>
                     </asp:DropDownList> </td>
                        <td>  
                            <asp:Button ID="Button4" runat="server" Text="Generate Report" 
                                CssClass="submit-generate" onclick="Button4_Click"/></td>
  
           </tr>
           </table>
         </asp:Panel>
         <asp:Panel ID="Panel2" runat="server">
         <table width="100%">

           <tr>
   
           <td>  <label><strong>Registration No :</strong></label> </td>
           <td>  
               <asp:TextBox ID="TextBox1" CssClass="textbox-medium1"  runat="server" 
                   Width="150px" Enabled="False"></asp:TextBox></td>
           <td> <asp:Button ID="Button1" runat="server" Text="Search"  CssClass="submit-button"  OnClientClick="ShowDialogDuplicate()"/></td>
        
              <td>  <label><strong>Select Language :</strong></label> </td>
                 <td>  
                     <asp:DropDownList ID="DropDownList2" runat="server" CssClass="textbox-medium1" 
                         Width="150px" >
                         <asp:ListItem Value="0">-- Select --</asp:ListItem>
                         <asp:ListItem Value="1">Bengali</asp:ListItem>
                         <asp:ListItem Value="2">English</asp:ListItem>
                         <asp:ListItem Value="3">Hindi</asp:ListItem>
                     </asp:DropDownList> </td>
                        <td>  
                            <asp:Button ID="Button2" runat="server" Text="Generate Report" 
                                CssClass="submit-generate" onclick="Button2_Click"/></td>
  
           </tr>
           </table>
         </asp:Panel>
           <table width="100%">

                  <tr>
           <td colspan="6" align="center">
               <asp:TextBox ID="TextBox2" runat="server" CssClass="textbox-medium1" 
                   Enabled="False" Visible="False" 
                   Width="150px"></asp:TextBox>
               <asp:RadioButtonList ID="RadioButtonList1" runat="server"  Width="250px"
                   RepeatDirection="Horizontal">
                   <asp:ListItem>With Header</asp:ListItem>
                   <asp:ListItem>Without Header</asp:ListItem>
               </asp:RadioButtonList>
               </td>
           </tr>
           </table>




           
         
     

         
            
                      </div>
                        <table width="100%">
                    <tr>        
                        <td style="width: 100%" align="center">   
                      <div id="mydiv" runat="server">       
                            <asp:Literal ID="ltrReport" runat="server"></asp:Literal><br />   
                            </div>                 
                        </td>
                    </tr>
                    <tr>
                        <td align="center">
                    
                        <input type="button" id="btnBack" value="Back" runat="server" style="width:70px; font-size:x-small" onclick="window.history.back()" />
                      <input type="button" id="cmdPrint"  runat="server" value="Print" style="width:70px; font-size:x-small" onclick="javascript:printDiv('mydiv')"/>
                           <asp:Button ID="passJV" runat="server" onclick="passJV_Click" Text="Pass Final Bill Journal" Visible="true"  style="width:120px; font-size:x-small"/>
                      <asp:Button ID="btnPDF" runat="server"  style="width:70px; font-size:x-small" 
                                Text="PDF" onclick="btnPDF_Click" />    
 </td>
                    </tr>
            </table>
    </ContentTemplate>
     <Triggers>
  <asp:PostBackTrigger ControlID="btnPDF" />
  </Triggers>
    </asp:UpdatePanel>
</asp:Content>

