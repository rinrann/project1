<%@ Page Title="" Language="C#" MasterPageFile="~/MasterAll/MasterPageAll.master" AutoEventWireup="true" CodeFile="PurchaseRegister.aspx.cs" Inherits="Medicine_PurchaseRegister" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <script type="text/javascript">
        function autoCompleteEx_ItemSelected(sender, args) {
            debugger;
            var regname = args.get_value().split('~');
            document.getElementById("ctl00_ContentPlaceHolder1_txtRegNo").value = regname[0];
            document.getElementById("ctl00_ContentPlaceHolder1_txtPtName").value = regname[1];

        }

        function autoCompleteEx_PhoneSelected(sender, args) {
            debugger;
            var regname = args.get_value().split('~');
            document.getElementById("ctl00_ContentPlaceHolder1_txtRegNo").value = regname[0];
            document.getElementById("ctl00_ContentPlaceHolder1_txtPtName").value = regname[1];
            document.getElementById("ctl00_ContentPlaceHolder1_txtPhoneNo").value = regname[2];
        }
        function autoCompleteEx_RegSelected(sender, args) {
            debugger;
            var regname = args.get_value().split('~');
            document.getElementById("ctl00_ContentPlaceHolder1_txtRegNo").value = regname[0];
            document.getElementById("ctl00_ContentPlaceHolder1_txtPtName").value = regname[1];

        }

        function printDiv(divID) {
            var divElements = document.getElementById(divID).innerHTML;
            // document.getElementById("ctl00_ContentPlaceHolder1_mydiv").style.width = '900px';
            var oldPage = document.body.innerHTML;
            document.body.innerHTML = "<html><head><title></title></head><body>" + divElements + "</body>";
            window.print();
            document.body.innerHTML = oldPage;
        }

    
    </script>

         

    <div class="pageheader">
        <asp:Label ID="Label1" runat="server">Purchase Register</asp:Label>
    </div>
    <div class="formbox">
        <div class="form-sec">
            <div align="center">
                <table cellpadding="0" cellspacing="0" title="Search" width="60%">
                    <tr>
                        <td>
                            <label class="ipdList" style='width: 75px;'><strong>From Date :</strong></label>
                        </td>
                        <td>
                            <asp:TextBox ID="txtfromdt" runat="server" TextMode="Date" CssClass="textbox-medium1"></asp:TextBox>
                        </td>
                        <td>
                            <label class="ipdList" style='width: 75px;'><strong>To Date :</strong></label></td>
                        <td>
                            <asp:TextBox ID="txttodt" runat="server" CssClass="textbox-medium1" TextMode="Date"></asp:TextBox>

                        </td>
                    </tr>
                    <tr>
                        <td style="">
                            <label class="ipdList" style='width: 75px;'><strong>Suppliers :</strong></label>
                        </td>
                        <td style="">
                            <asp:DropDownList ID="ddlsupplier" runat="server" ></asp:DropDownList>

                        </td>
                        <td style="">
                            <label class="ipdList" style='width: 75px;'><strong>&nbsp;&nbsp;Items :</strong></label>
                        </td>
                        <td style="">

                          <asp:DropDownList ID="ddlitems" runat="server" ></asp:DropDownList>

                        </td>



                    </tr>
                    <tr>
                        <td colspan="4" align="center" style="text-align: center; padding-top: 10px">

                            <asp:Button ID="Button1" runat="server" Text="Submit" CssClass="submit-button" Height="28px" OnClick="Button1_Click" />
                            <asp:Button ID="cmdBack" runat="server" Text="Back"  CssClass="submit-button"  OnClick="cmdBack_Click"  /> 
                        </td>
                    </tr>
                </table>

                <br />

                <div id="divregister" runat="server" visible="false" width="100%">
                    <div id="mydiv" style="text-align: left;width:1300px;height:400px;overflow:auto" align="center">
                        <asp:Literal ID="ltrReport" runat="server"></asp:Literal>
                    </div>
                    <br />
                    <div align="center">
                        <asp:Button ID="cmd_excel" runat="server" Text="Export to Excel" style="width: 120px; font-size: small" OnClick="cmd_excel_Click" />
                        
                    </div>
                </div>

            </div>
        </div>
    </div>
</asp:Content>

