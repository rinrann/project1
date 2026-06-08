<%@ Page Title="" Language="C#" MasterPageFile="~/MasterAll/MasterPageAll.master" AutoEventWireup="true" CodeFile="Rep_PatientFeesCollection.aspx.cs" Inherits="Rep_PatientFeesCollection" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Src="~/UC/UCItemGroup.ascx" TagName="UCItemGroup" TagPrefix="ItmGrp" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <script type="text/javascript" language="javascript">
        function printDiv(divID) {
            var divElements = document.getElementById(divID).innerHTML;
            var oldPage = document.body.innerHTML;
            document.body.innerHTML = "<html><head><title></title></head><body>" + divElements + "</body>";
            window.print();
            document.body.innerHTML = oldPage;
        }


        function DisableBackButton() {
            window.history.forward()
        }
</script>   <div class="pageheader">
         <asp:Label ID="Label1" runat="server">Patient Registration Collection Register</asp:Label>
     </div>
    <div align="center" width="100%">
   <table style="width:60%">
       <tr>
           <td style="width:48%" valign="top">
               <table style="width:100%">
                   
                   <tr>
                       <td>From Date :</td>
                       <td>
                            <asp:TextBox ID="txtfromdt" runat="server" ></asp:TextBox>
                               <asp:Label ID="Label3" runat="server" ForeColor="Green" Text="(DD/MM/YYYY)" Visible="false"></asp:Label>
                    <cc1:MaskedEditExtender ID="MaskedEditExtender1" runat="server" TargetControlID="txtfromdt"
                                            Mask="99/99/9999" MessageValidatorTip="true" MaskType="Date" DisplayMoney="Left"
                                            AcceptNegative="Left" ErrorTooltipEnabled="True" CultureName="en-GB" />
                       </td>
                   </tr>
                   <tr>
                       <td>To Date :</td>
                       <td>
                             <asp:TextBox ID="txttodt" runat="server"  ></asp:TextBox>
                               <asp:Label ID="Label2" runat="server" ForeColor="Green" Text="(DD/MM/YYYY)" Visible="false"></asp:Label>
                    <cc1:MaskedEditExtender ID="MaskedEditExtender2" runat="server" TargetControlID="txttodt"
                                            Mask="99/99/9999" MessageValidatorTip="true" MaskType="Date" DisplayMoney="Left"
                                            AcceptNegative="Left" ErrorTooltipEnabled="True" CultureName="en-GB" />
                       </td>
                   </tr>
                   <tr><td></td>
                       <td align="center"  ><br />
                                  <asp:Button ID="btnproceed" runat="server"  Text="Proceed" Height="28px" CssClass="submit-button" OnClick="btnproceed_Click" />
                <asp:Button ID="Button2" runat="server" Text="Back"   Height="28px" CssClass="submit-button" OnClick="Button2_Click" />
                       </td>
                   </tr>
               </table> 
           </td>
            
       </tr>
   </table>
    </div>
    <div style="float:right">
        
                                            
    </div>
</asp:Content>

