<%@ Page  MasterPageFile="~/MasterAll/MasterPageAll.master" Language="C#" AutoEventWireup="true" CodeFile="MonthlyInvoiceReport.aspx.cs" Inherits="OPD_MonthlyInvoiceReport" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <script type="text/javascript">
        function autoCompleteEx_ItemSelected(sender, args) {
            debugger;
            var regname = args.get_value().split('~');
            document.getElementById("ctl00_ContentPlaceHolder1_txtDocId").value = regname[0];
            document.getElementById("ctl00_ContentPlaceHolder1_txtDocName").value = regname[1];

        }

        function printDiv(divID) {
            var divElements = document.getElementById(divID).innerHTML;
            var oldPage = document.body.innerHTML;
            document.body.innerHTML = divElements;
            window.print();
            document.body.innerHTML = oldPage;
        }
    </script>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
    <ContentTemplate>
        </ContentTemplate>
    </asp:UpdatePanel>
    <div class="pageheader">
         <asp:Label ID="Label1" runat="server">MONTHLY INVOICE REPORT</asp:Label>
    </div>
    <div class="formbox">
        <div class="form-sec">
            <div class="error">
                <strong><asp:Label ID="lblError" runat="server" Font-Bold="True"></asp:Label></strong>
                <div class="clear"></div>
            </div>
        </div>
        <table style="width:100%">
            
            <tr>
                <td style="width:10%">
                    <label><strong>From Date :</strong></label> 
                </td>
                <td style="width:15%">
                    <asp:TextBox ID="txtfromdt" runat="server" TextMode="Date"></asp:TextBox>
                                   
                </td>
                <td style="width:10%">
                    <label><strong>To Date :</strong></label> 
                </td>
                <td  style="width:15%">
                        <asp:TextBox ID="txttodt" runat="server" TextMode="Date"></asp:TextBox>
                               
                </td>
                <td style="width:5%">
                    
                </td>
                <td style="width:10%;">  
                   <asp:Button ID="btnGenRpt" runat="server" Text="Generate Report" CssClass="submit-generate" OnClick="btnGenRpt_Click"/>

               </td>
            </tr>
        </table>
        <table width="100%">
            <tr>        
            <td align="center">  
               <h3 id="hd" runat="server" visible="false"> MONTHLY INVOICE REPORT</h3>
                  <div id='mydiv' style="overflow:auto;width:100%">              
                <asp:Literal ID="ltrReport" runat="server"></asp:Literal><br />

                                   </div>                  
            </td>
        </tr>
        <tr>
            <td align="center">
                   <asp:Button ID="BtnBack" runat="server" style="width:70px; font-size:x-small" Text="Back" OnClick="BtnBack_Click"  />
                <asp:Button ID="btn_excel" runat="server" style="width:100px; font-size:x-small" Text="Export to Excel" OnClick="btn_excel_Click" />
                <%--<input type="button" id="cmdPrint" value="Print" style="width:70px; font-size:x-small" onclick="javascript: printDiv('mydiv')"/>--%>

            </td>
        </tr>
        </table>
    </div>
</asp:Content>