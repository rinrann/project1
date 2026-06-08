<%@ Page Title="" Language="C#" MasterPageFile="~/MasterAll/MasterPageAll.master" AutoEventWireup="true" CodeFile="AdmissionSlip.aspx.cs" Inherits="IPD_AdmissionSlip" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
<script type="text/javascript" language="javascript">
    function printDiv(divID) {
        var divElements = document.getElementById(divID).innerHTML;
        // document.getElementById("ctl00_ContentPlaceHolder1_mydiv").style.width = '900px';
        var oldPage = document.body.innerHTML;
        document.body.innerHTML = "<html><head><title></title></head><body>" + divElements + "</body>";
        window.print();
        document.body.innerHTML = oldPage;
    }
    function ShowDialog() {

        var rtvalue = window.open("IpdpatientPopup.aspx", "sss", "Width:800px; Height:550px; dialogLeft:250px;");
        //document.getElementById("ctl00_ContentPlaceHolder1_txtreg").value = rtvalue.NameValue;

    }

    $(document).ready(function () {
        $("input[id$='Button4']").click(function () {
            if ($("input[id$='TextBox1']").val() == '') {
                alert('Please Select A Patient !');
                $("input[id$='TextBox1']").focus();
                $("input[id$='TextBox1']").addClass('textboxerr');
                return false;
            }
            else {
                $("input[id$='TextBox1']").removeClass('textboxerr');
            }

            if ($("select[id$='DropDownList1']").val() == '0') {
                alert('Please Select Language!');
                $("select[id$='DropDownList1']").addClass('textboxerr');
                $("select[id$='DropDownList1']").focus();
                return false;
            }
            else {
                $("select[id$='DropDownList1']").removeClass('textboxerr');
            }
        });
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
         <asp:Label ID="Label1" runat="server">Admission Slip</asp:Label>
     </div>
     <div class="formbox">
        <div class="form-sec">
            <div class="error">
                <strong><asp:Label ID="lblError" runat="server" Font-Bold="True"></asp:Label></strong>
                <div class="clear"></div>
            </div></div>
           <table width="100%">
           <tr>
   
           <td>  <label><strong>Registration No :</strong></label> </td>
           <td>  <asp:TextBox ID="TextBox1" CssClass="textbox-medium1"  runat="server" 
                   Width="150px" Enabled="False"></asp:TextBox></td>
           <td> <asp:Button ID="Button3" runat="server" Text="Search"  CssClass="submit-button"  OnClientClick="ShowDialog()"/></td>
        
              <td>  <label><strong>Select Language :</strong></label> </td>
                 <td>  
                     <asp:DropDownList ID="DropDownList1" runat="server" CssClass="textbox-medium1" 
                         Width="150px" >
                         <asp:ListItem Value="0">-- Select --</asp:ListItem>
                         <asp:ListItem Value="1">Bengali</asp:ListItem>
                         <asp:ListItem Value="2">English</asp:ListItem>
                     </asp:DropDownList> </td>
                        <td>  <asp:Button ID="Button4" runat="server" Text="Generate Report" CssClass="submit-generate" onclick="Button4_Click" /></td>
  
           </tr>

              <tr>
           <td colspan="6" align="center">
               <asp:RadioButtonList ID="RadioButtonList1" runat="server"  Width="250px"
                   RepeatDirection="Horizontal">
                   <asp:ListItem>With Header</asp:ListItem>
                   <asp:ListItem>Without Header</asp:ListItem>
               </asp:RadioButtonList>
               </td>
           </tr>
           </table>
            
         <asp:HiddenField ID="HiddenField1" runat="server" />
                      </div>
                         <table width="100%">
                    <tr>        
                        <td align="center">    <div id='mydiv'>              
                            <asp:Literal ID="ltrReport" runat="server"></asp:Literal><br />  </div>                  
                        </td>
                    </tr>
                    <tr>
                        <td align="center">
                     
                         <input type="button" value="Back" style="width:70px; font-size:x-small" onclick="window.history.back()" />
                         <input type="button" id="cmdPrint" value="Print" style="width:70px; font-size:x-small" onclick="javascript:printDiv('mydiv')"/></td>
                    </tr>
            </table>
    </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>

