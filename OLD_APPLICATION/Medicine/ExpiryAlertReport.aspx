<%@ Page Title="" Language="C#" MasterPageFile="~/MasterAll/MasterPageAll.master" AutoEventWireup="true" CodeFile="ExpiryAlertReport.aspx.cs" Inherits="Medicine_ExpiryAlertReport" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <script type="text/javascript" language="javascript">


        function printDiv(divID) {
            var divElements = document.getElementById(divID).innerHTML;
            var oldPage = document.body.innerHTML;
            document.body.innerHTML = "<html><head><title></title></head><body>" + divElements + "</body>";
            window.print();
            document.body.innerHTML = oldPage;
        }




        function Calling() {

            $("input[id$='TextBox1']").keydown(function (event) {
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

        function SetContextKey() {
            $find('AutoCompleteExtender1').set_contextKey("GFC");
        }

        function autoCompleteEx_ItemSelected(sender, args) {

            var regname = args.get_value().split('~');// alert(regname[0]);
            document.getElementById("ctl00_ContentPlaceHolder1_TextBoxicode").value = regname[1];
            document.getElementById("ctl00_ContentPlaceHolder1_TextBox8").value = regname[0];
            // document.getElementById("TextBox8").value = regname[0];
            //document.getElementById("TextBox1").value = regname[0];
            $("#TextBox8").focus();
            //$("#DropDownList4").val(regname[1]);
        }
    </script>
    <div class="pageheader">
        <asp:Label ID="Label1" runat="server" ForeColor="#996600" Font-Size="X-Large"><strong>EXPIRY ALERT REPORT</strong></asp:Label>
    </div>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <table style="width: 100%">
                <tr>
                    <td>
                        <asp:RadioButtonList ID="rbl_option" runat="server" RepeatDirection="Horizontal" AutoPostBack="true" OnSelectedIndexChanged="chk_option_SelectedIndexChanged">
                            <asp:ListItem Selected="True" Value="1">Going to be Expired</asp:ListItem>
                            <asp:ListItem Value="2">As on Date basis</asp:ListItem>
                        </asp:RadioButtonList>
                    </td>
                </tr>
            </table>
            <div class="formbox">
                <center>
                    <table width="100%" style='background-color: #42C25A; color: White;' runat="server" id="tbl1">
                        <tr>

                            <td>
                                <label>
                                    <strong>Day:</strong></label></td>
                            <td>
                                <asp:TextBox ID="TextBox1" Width="30px" runat="server" MaxLength="5" AutoPostBack="True"
                                    OnTextChanged="TextBox1_TextChanged"></asp:TextBox>
                            </td>

                            <%--<td>
                                <label>
                                    <strong>Mfg Company:</strong></label></td>
                            <td>
                                <asp:DropDownList ID="DropDownList1" runat="server" Width="140px"
                                    AutoPostBack="True" OnSelectedIndexChanged="DropDownList1_SelectedIndexChanged">
                                </asp:DropDownList>
                            </td>

                            <td>
                                <label>
                                    <strong>Group :</strong></label></td>
                            <td>
                                <asp:DropDownList ID="DropDownList2" runat="server" Width="140px"
                                    AutoPostBack="True" OnSelectedIndexChanged="DropDownList2_SelectedIndexChanged">
                                </asp:DropDownList>
                            </td>

                            <td>
                                <label>
                                    <strong>Sub Group :</strong></label></td>
                            <td>
                                <asp:DropDownList ID="DropDownList3" runat="server" Width="140px"
                                    AutoPostBack="True" OnSelectedIndexChanged="DropDownList3_SelectedIndexChanged">
                                </asp:DropDownList>
                            </td>--%>

                            <td>
                                <label>
                                    <strong>Medincine :</strong></label></td>
                            <td>
                                <asp:DropDownList ID="DropDownList4" runat="server" Width="140px"
                                    AutoPostBack="True" OnSelectedIndexChanged="DropDownList4_SelectedIndexChanged">
                                </asp:DropDownList>
                                <%--<asp:TextBox ID="TextBoxicode" runat="server" Width="150px" CssClass="textbox-medium1"></asp:TextBox>--%>
                            </td>
                            <%--<td>

                                <asp:TextBox ID="TextBox8" runat="server" OnTextChanged="medicinechange" AutoPostBack="true" Width="150px" CssClass="textbox-medium1"></asp:TextBox>
                                <cc1:AutoCompleteExtender ServiceMethod="SearchMedicineName" OnClientItemSelected="autoCompleteEx_ItemSelected" MinimumPrefixLength="1"
                                    CompletionInterval="100" EnableCaching="false" CompletionSetCount="10" TargetControlID="TextBox8" ID="AutoCompleteExtender1" runat="server"
                                    FirstRowSelected="false">
                                </cc1:AutoCompleteExtender>
                            </td>--%>
                        </tr>
                    </table>

                    <table style=' ' runat="server" id="tbl2">
                        <tr>
                            <td>As on Date</td>
                            <td>
                                                <asp:TextBox ID="txtasondt" runat="server" ></asp:TextBox>
                               <asp:Label ID="Label3" runat="server" ForeColor="Green" Text="(DD/MM/YYYY)" Visible="false"></asp:Label>
                    <cc1:MaskedEditExtender ID="MaskedEditExtender1" runat="server" TargetControlID="txtasondt"
                                            Mask="99/99/9999" MessageValidatorTip="true" MaskType="Date" DisplayMoney="Left"
                                            AcceptNegative="Left" ErrorTooltipEnabled="True" CultureName="en-GB" />
                            </td>
                            <td>
                                <asp:Button ID="btnsubmit" runat="server" Text="Proceed" OnClick="btnsubmit_Click" Width="120px" />
                            </td>
                        </tr>
                    </table>

                </center>
            </div>
            <div class="formbox">
                <table width="100%">
                    <tr>
                        <td align="center">

                            <asp:RadioButtonList ID="RadioButtonList1" runat="server" Width="350px"
                                AutoPostBack="True"
                                OnSelectedIndexChanged="RadioButtonList1_SelectedIndexChanged"
                                RepeatDirection="Horizontal">
                                <asp:ListItem>With Header</asp:ListItem>
                                <asp:ListItem>Without Header</asp:ListItem>
                            </asp:RadioButtonList>
                        </td>
                    </tr>
                </table>
            </div>
            <div class="formbox">
                <div class="form-sec">
                    <table width="100%">
                        <tr>
                            <td>
                                <div id='mydiv'>
                                    <asp:Literal ID="ltrReport" runat="server"></asp:Literal>
                                </div>
                            </td>
                        </tr>
                        <tr>
                            <td align="center">

                                <input type="button" value="Back" style="width: 70px; font-size: x-small" onclick="window.history.back()" />
                                <input type="button" id="cmdPrint" value="Print" style="width: 70px; font-size: x-small" onclick="javascript: printDiv('mydiv')" /></td>
                        </tr>
                    </table>
                </div>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>

