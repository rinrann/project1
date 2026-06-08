<%@ Page Title="" Language="C#" MasterPageFile="~/MasterAll/MasterPageAll.master" AutoEventWireup="true" CodeFile="InvoiceSellPriceDetailsReport.aspx.cs" Inherits="Medicine_InvoiceSellPriceDetailsReport" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <script type="text/javascript" language="javascript">

        function ShowDialog() {
            var rtvalue = window.open("MedicineSalePopup.aspx", "sss", "Width:800px; Height:550px; dialogLeft:250px;");
            //document.getElementById("ctl00_ContentPlaceHolder1_txtissueMedicineId").value = rtvalue.NameValue;

        }

        function printDiv(divID) {
            var divElements = document.getElementById(divID).innerHTML;
            var oldPage = document.body.innerHTML;
            document.body.innerHTML = "<html><head><title></title></head><body>" + divElements + "</body>";
            window.print();
            document.body.innerHTML = oldPage;
        }
    </script>



    
<%-- 
  <asp:UpdateProgress ID="UpdateProgress1" runat="server">
    <ProgressTemplate>
    
       <div id="aaa" class="progressBackgroundFilter">          </div>
        <div id="bbb" class="processMessage"><img alt="Loading" src="../images/pwait.gif"/>
      
              </div>
    </ProgressTemplate>
    </asp:UpdateProgress>--%>


    <div class="pageheader">
            <asp:Label ID="Label1" runat="server">Medicine Sales/Issue Report</asp:Label>
        </div>
            <asp:UpdatePanel ID="UpdatePanel1" runat="server">
    <ContentTemplate>
        <div class="formbox">
    <div class="form-sec-row">
                    <label><strong>
                        Sale/Issue Id:</strong></label>
                    <asp:TextBox ID="TextBox5" runat="server" 
                        CssClass="textbox-medium1" Enabled="False"></asp:TextBox>
                    <asp:Button ID="Button3" runat="server" CssClass="submit-button" Text="SEARCH" Height="28px" OnClientClick="ShowDialog()"/>
                    <asp:Button ID="Button4" runat="server" CssClass="submit-button" Text="FETCH"  Height="28px"
                         onclick="Button4_Click"  />
                    <div class="clear">
                    </div>
                </div>

                    <table width="100%">
                    <tr>        
                        <td>    <div id='mydiv'>              
                            <asp:Literal ID="ltrReport" runat="server"></asp:Literal></div>                
                        </td>
                    </tr>
                    <tr>
                        <td align="center">
                     
                         <input type="button" value="Back" style="width:70px; font-size:x-small" onclick="window.history.back()" />
                         <input type="button" id="cmdPrint" value="Print" style="width:70px; font-size:x-small" onclick="javascript:printDiv('mydiv')"/></td>
                    </tr>
            </table>

                </div>
                </ContentTemplate>
                </asp:UpdatePanel>
</asp:Content>

