<%@ Page Language="C#"  MasterPageFile="~/MasterAll/MasterPageAll.master" AutoEventWireup="true" CodeFile="DailyCollection.aspx.cs" Inherits="Bill_DailyCollection" %>
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
             <asp:Label ID="Label1" runat="server">Daily collection Report</asp:Label>
        </div>
        <table style="width:100%">
            <tr>
                <td style="width:10%">
                    <label><strong>Date :</strong></label> 
                </td>
                <td  style="width:15%">
                    <asp:TextBox ID="txtfromdt" runat="server" TextMode="Date"></asp:TextBox>
                </td>
                <td  style="width:10%;">
                    &nbsp;
                </td>
                <td style="width:10%;">  
                   <asp:Button ID="btnGenRpt" runat="server" Text="Generate Report" CssClass="submit-generate" OnClick="btnGenRpt_Click"/>

               </td>
            </tr>
        </table>
        <table width="100%">
            <tr>        
                <td align="center">  
                   <div id="mydiv" style="text-align: left;width:100%;">
                        <asp:Literal ID="ltrReport" runat="server"></asp:Literal>
                    </div>                 
                </td>
            </tr>
            <tr>
                <td align="center">
                    <asp:Button ID="BtnBack" runat="server" style="width:70px;height:21px; font-size:x-small" Text="Back" OnClick="Button2_Click"  />
                    <%--<asp:Button ID="Button2" runat="server" Text="Back"   CssClass="submit-button" OnClick="Button2_Click" />--%>
                    <input type="button" id="cmdPrint" value="Print" style="width: 70px; font-size: x-small;height:21px;" onclick="javascript: printDiv('mydiv')" />
                </td>
            </tr>
        </table>
        
        <div align="center">
            <%--<asp:Button ID="cmd_excel" runat="server" Text="Export to Excel" OnClick="cmd_excel_Click" />--%>

            
        </div>
    

</asp:Content>