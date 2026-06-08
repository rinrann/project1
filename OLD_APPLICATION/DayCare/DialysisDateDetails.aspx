<%@ Page Language="C#" MasterPageFile="~/MasterAll/MasterPageAll.master" AutoEventWireup="true" CodeFile="DialysisDateDetails.aspx.cs" Inherits="DayCare_DialysisDateDetails" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <style type="text/css">
        .auto-style1 {
            width: 189px;
        }

        .auto-style2 {
            width: 99px;
        }

        .auto-style3 {
            width: 158px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <script type="text/javascript">
        ///
        function ShowDialog() {

            //var rtvalue = window.open("RegistrationPopup.aspx", "sss", "Width:800px; Height:550px; dialogLeft:250px;");
            var rtvalue = window.open("DialysisPopup.aspx", "sss", "Width:800px; Height:550px; dialogLeft:250px;");
            document.getElementById("ctl00_ContentPlaceHolder1_TextBox2").value = rtvalue.NameValue;

            //document.getElementById("ctl00_ContentPlaceHolder1_TextBox1").value=rtvalue.
            //document.getElementById("ctl00_ContentPlaceHolder1_TextBox1").nodeValue=r
            ///
            function Calling() {
                var date = new Date();
                $("input[id$='txtdate']").datepicker({ dateFormat: 'dd/mm/yy' });
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
                arg = document.getElementById('HiddenField1').value;
                window.returnValue = arg;
                window.close();

            }

            function Button1_onclick() {
                window.close();
            }

        }</script>
    <div>
        <table width="100%">

            <tr>
                <td class="auto-style1">Registration No</td>
                <td class="auto-style2">
                    <asp:Label ID="Label1" runat="server" Text="Label" Visible="false"></asp:Label>

                </td>

            </tr>
            <tr>
                <td class="auto-style1">
                    <asp:TextBox ID="TextBox2" CssClass="textbox-medium1" runat="server"
                        Width="150px"></asp:TextBox>
                    <%--<asp:TextBox ID="TextBox2" runat="server"></asp:TextBox>--%>
                </td>
                <td class="auto-style2">
                    <asp:Button ID="Button2" runat="server" Text="Search" CssClass="submit-button" OnClientClick="ShowDialog()" />

                </td>
                <td class="auto-style3">
                    <asp:Button ID="Button5" runat="server" Text="Show Details"
                        CssClass="submit-generate" OnClick="Button5_Click" />
                </td>
                <td>
                    <asp:Button ID="Button1" runat="server" Text="Back"
                        CssClass="submit-generate" OnClick="Button1_Click" />
                </td>
                <asp:Label ID="Label2" runat="server" Text="Label" Visible="false"></asp:Label>
                
            </tr>


            <tr>
                <%-- DataKeyNames="MedicineID,MedicineName"--%>
                <%-- <td>

                    <asp:GridView ID="GridView2" runat="server"></asp:GridView>
                </td>--%>
                <td colspan="4">
                    <div class="formbox">
                        <div style='width: 100%;'>
                            <table width="100%" style='background-color: #FB7B13; color: #FFF;'>
                                <tr style='border: 1px solid black;'>

                                    <td style='width: 110px;' align="center">Dates Of Dialysis</td>
                            </table>
                        </div>
                    </div>
                    <div style='width: 100%; height: 300px; overflow: auto;'>

                        <asp:GridView ID="GridView2" CssClass="grid"
                            PagerStyle-CssClass="pgr" AlternatingRowStyle-CssClass="alt" ShowHeader="false"
                            DataKeyNames="Dates_Of_Dialysis"
                            runat="server" AutoGenerateColumns="False" Width="978px">

                            <Columns>
                                <asp:TemplateField HeaderText="Dialysis Done Date" ItemStyle-Width="100%" ItemStyle-HorizontalAlign="Center">
                                    <ItemTemplate>
                                        <asp:Label ID="lbldialysisdate" runat="server" Text='<%# Bind("Dates_Of_Dialysis") %>'></asp:Label>
                                    </ItemTemplate>

                                </asp:TemplateField>
                            </Columns>
                            <PagerStyle CssClass="pgr"></PagerStyle>
                            <EditRowStyle BackColor="#CCFF33" />
                            <AlternatingRowStyle BackColor="#FFDB91" />
                        </asp:GridView>

                    </div>
                </td>
            </tr>
        </table>
    </div>
</asp:Content>
