<%@ Page Language="C#" AutoEventWireup="true" CodeFile="View_StockLedger.aspx.cs" Inherits="View_StockLedger" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <script>
        function printDiv(divID) {
            var divElements = document.getElementById(divID).innerHTML;
            var oldPage = document.body.innerHTML;
            document.body.innerHTML = divElements;
            window.print();
            document.body.innerHTML = oldPage;
        }
    </script>
</head>
<body>
    <form id="form1" runat="server">
        <div id="mydiv" style="text-align: left">
            <asp:Literal ID="ltrReport" runat="server"></asp:Literal>
        </div>
        <div align="center">
            <asp:Button ID="cmd_excel" runat="server" Text="Export to Excel" OnClick="cmd_excel_Click" />

            <asp:Button ID="cmdBack" runat="server" Text="Back" OnClick="cmdBack_Click" />
            <input type="button" id="cmdPrint" value="Print" style="width: 70px; font-size: x-small" onclick="javascript: printDiv('mydiv')" />
        </div>
    </form>
</body>
</html>
