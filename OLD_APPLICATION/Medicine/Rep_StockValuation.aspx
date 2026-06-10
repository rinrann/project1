<%@ Page Title="" Language="C#" MasterPageFile="~/MasterAll/MasterPageAll.master" AutoEventWireup="true" CodeFile="Rep_StockValuation.aspx.cs" Inherits="Rep_StockValuation" %>
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
         <asp:Label ID="Label1" runat="server">Stock Valuation</asp:Label>
     </div>
    <div align="center" width="100%">
   <table style="width:80%">
       <tr>
           <td style="width:60%" valign="top">
               <table style="width:100%">
                   <tr>
                       <td>From Date :</td>
                       <td>
                            <asp:TextBox ID="txtfromdt" runat="server" TextMode="Date"></asp:TextBox>
                              
                       </td>
                   </tr>
                   <tr>
                       <td>To Date :</td>
                       <td>
                             <asp:TextBox ID="txttodt" runat="server" TextMode="Date" ></asp:TextBox>
                               
                       </td>
                   </tr>
                   <tr>
                       <td></td>
                       <td>
                           <asp:RadioButtonList ID="rbl1" runat="server"  RepeatDirection="Horizontal" style="float:left">
                               <asp:ListItem Selected="True" Value="1">Item wise</asp:ListItem>
                               <asp:ListItem Value="2">Item & Batch wise</asp:ListItem>
                           </asp:RadioButtonList>
                       </td>
                   </tr>
                   <tr>
                       <td></td>
                       <td>
                           <asp:RadioButtonList ID="rbl2" runat="server" RepeatDirection="Horizontal" style="float:left">
                               <asp:ListItem Selected="True" Value="1">With Value</asp:ListItem>
                               <asp:ListItem Value="2">Without Value</asp:ListItem>
                           </asp:RadioButtonList>
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

           <td style="width:40%"  valign="top">
               <table cellspacing="0" cellpadding="0" border="0">
                                                    <tr id="trDocImgTab" runat="server">
                                                        <td class="pop_tab_line" valign="bottom" style="width: 100%">
                                                            <table cellspacing="0" cellpadding="0" border="0">
                                                                <tr>
                                                                    <td colspan="2" style="height: 10px;">
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td valign="bottom">
                                                                        <table cellspacing="0" cellpadding="0" class="tablink2">
                                                                            <tr>
                                                                               
                                                                                <td class="pop_active_tab" id="td1" runat="server" align="right">
                                                                                    <asp:Button ID="lnktab1" runat="server" Width="69px"
                                                                                        CssClass="submitbutton" OnClick="lnktab1_Click"
                                                                                        Text="Group" />
                                                                                </td>
                                                                            </tr>
                                                                        </table>
                                                                    </td> 
                                                                </tr>
                                                            </table>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td style="width: 100%">
                                                            <table id="trGrp" cellspacing="0" cellpadding="0" border="0" runat="server">
                                                                <tr>
                                                                    <td style="width: 1px" width="1">
                                                                        <ItmGrp:UCItemGroup ID="Itemgroup" runat="server" />
                                                                    </td>
                                                                    <td align="center">
                                                                        &nbsp;
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                             
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

