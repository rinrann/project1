<%@ Page Language="C#" MasterPageFile="~/MasterAll/MasterPageAll.master" AutoEventWireup="true" CodeFile="MedicineSalesNew.aspx.cs" Inherits="Medicine_MedicineSalesNew" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">

    <script type="text/javascript" language="javascript">
        function ShowDialogPatient() {
            //var rtvalue = window.open("RegistrationPopup.aspx", "sss", "Width:800px; Height:550px; dialogLeft:250px;");
            var rtvalue = window.open("../OPD/PtientDetailspopup.aspx", "sss", "Width:500px; Height:350px; dialogLeft:250px;");
            //document.getElementById("ctl00_ContentPlaceHolder1_txtreg").value = rtvalue.NameValue;
        }

        function ShowDialog() {
            var rtvalue = window.open("MedicineSalePopup.aspx", "sss", "Width:800px; Height:550px; dialogLeft:250px;");
            //document.getElementById("ctl00_ContentPlaceHolder1_TextBox5").value = rtvalue.NameValue;

        }

        function printDiv(divID) {
            if (document.getElementById('ctl00_ContentPlaceHolder1_chkCancel').checked) {
                document.getElementById('divcancel').style.visibility = 'visible';
            }
            var divElements = document.getElementById(divID).innerHTML;
            var oldPage = document.body.innerHTML;
            document.body.innerHTML = "<html><head><title></title></head><body>" + divElements + "</body>";            
           
            window.print();
            document.getElementById('divcancel').style.visibility = 'hidden';
            document.body.innerHTML = oldPage;
        }


        function ShowDialogReg() {
            var rtvalue = window.open("QuickRegPopup.aspx", "sss", "Width:500px; Height:350px; dialogLeft:250px;");
        }

        function Calling() {
            //var date = new Date();
            //$('.DatepickerCall1').datepicker({ dateFormat: 'dd/mm/yy' });

            //var date = new Date();
            //$('.DatepickerCall').datepicker({ dateFormat: 'dd/mm/yy' });

            $("input[id$='Button1']").click(function () {

                //if ($("select[id$='DropDownList1']").val() == '0') {
                //    alert('Please Select Supplier Name!');
                //    $("select[id$='DropDownList1']").addClass('textboxerr');
                //    $("select[id$='DropDownList1']").focus();
                //    return false;
                //}
                //else {
                //    $("select[id$='DropDownList1']").removeClass('textboxerr');
                //}

                if ($("input[id$='txtPurchaseMedicineId']").val() == '') {
                    alert('Please Enter Medicine ID!');
                    $("input[id$='txtPurchaseMedicineId']").focus();
                    $("input[id$='txtPurchaseMedicineId']").addClass('textboxerr');
                    return false;
                }
                else {
                    $("input[id$='txtPurchaseMedicineId']").removeClass('textboxerr');
                }

                if ($("input[id$='txtPurchaseMedicineName']").val() == '') {
                    alert('Please Enter Medicine Name!');
                    $("input[id$='txtPurchaseMedicineName']").focus();
                    $("input[id$='txtPurchaseMedicineName']").addClass('textboxerr');
                    return false;
                }
                else {
                    $("input[id$='txtPurchaseMedicineName']").removeClass('textboxerr');
                }

                if ($("input[id$='Calendar1']").val() == '') {
                    alert('Please Enter purchase Date!');
                    $("input[id$='Calendar1']").focus();
                    $("input[id$='Calendar1']").addClass('textboxerr');
                    return false;
                }
                else {
                    $("input[id$='Calendar1']").removeClass('textboxerr');
                }

                if ($("input[id$='txtBillNo']").val() == '') {
                    alert('Please Enter Bill No!');
                    $("input[id$='txtBillNo']").focus();
                    $("input[id$='txtBillNo']").addClass('textboxerr');
                    return false;
                }
                else {
                    $("input[id$='txtBillNo']").removeClass('textboxerr');
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
    </script>
    <script type="text/javascript">
        function autoCompleteEx_ItemSelected(sender, args) {

            var regname = args.get_value();// alert(regname[0]);
            document.getElementById("ctl00_ContentPlaceHolder1_TextBox3").value = regname;

            $("#TextBox3").focus();
            //$("#DropDownList4").val(regname[1]);
        }

        function autoCompleteEx_PatientSelected(sender, args) {
            debugger;
            var regname = args.get_value().split('~');
            document.getElementById("ctl00_ContentPlaceHolder1_txtreg").value = regname[0];
            document.getElementById("ctl00_ContentPlaceHolder1_hdnregno").value = regname[0];
            document.getElementById("ctl00_ContentPlaceHolder1_txtPatientName").value = regname[1];
            document.getElementById("ctl00_ContentPlaceHolder1_hdnLedgerId").value = regname[2];
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <div id="divContent" runat="server">
                <div class="pageheader">
                    <asp:Label ID="Label1" runat="server">Medicine Sales/Issue</asp:Label>
                </div>
                <div class="formbox">
                    <div class="form-sec">
                        <div class="error">
                            <strong>
                                <asp:Label ID="lblError" runat="server" Font-Bold="True" CssClass="fadeout"></asp:Label>
                            </strong>
                            <div class="clear">
                            </div>
                        </div>
                        <asp:HiddenField ID="hdnMode" runat="server" Value="0" />
                        <asp:HiddenField ID="HiddenField1" runat="server" />

                        <div class="form-sec-row">
                            <label>
                                <strong>Sales/Issue Bill No :</strong></label>
                            <div style="display: none;">
                                <asp:TextBox ID="TextBox5" runat="server" Enabled="false" CssClass="textbox-medium1"></asp:TextBox>
                            </div>
                            <asp:TextBox ID="txtBillNo" runat="server" Enabled="false" CssClass="textbox-medium1"></asp:TextBox>
                            <asp:Button ID="Button3" runat="server" CssClass="submit-search" Text="SEARCH" Width="74px" OnClientClick="ShowDialog()" />
                            <asp:Button ID="Button4" runat="server" CssClass="submit-button" Text="FETCH" Width="74px" OnClick="Button4_Click" />
                            <div class="clear">
                            </div>
                        </div>

                        <div class="form-sec-row">
                            <label>
                                <strong>Type :</strong></label>

                            <asp:DropDownList ID="ddltype" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddltype_SelectedIndexChanged" Width="298px">
                                <asp:ListItem Selected="True" Value="P">Patient</asp:ListItem>
                                <asp:ListItem Value="S">Staff</asp:ListItem>
                            </asp:DropDownList>
                            <div class="clear">
                            </div>
                        </div>

                        <div class="form-sec-row"  runat="server" id="divp" >
                            <label>
                                <strong><strong>Patient Name :
                                </strong>
                            </label>
                            <asp:TextBox ID="txtPatientName" runat="server" CssClass="textbox-medium1"></asp:TextBox>
                            <cc1:AutoCompleteExtender ServiceMethod="SearchByPatientName" OnClientItemSelected="autoCompleteEx_PatientSelected" MinimumPrefixLength="1"
                                CompletionInterval="100" EnableCaching="false" CompletionSetCount="10" TargetControlID="txtPatientName" ID="AutoCompleteExtender1" runat="server"
                                FirstRowSelected="false">
                            </cc1:AutoCompleteExtender>
                            <asp:Button ID="Button6" runat="server" CssClass="submit-search" Height="28px" Text="New Register" OnClientClick="ShowDialogReg()" />
                            <div class="clear">
                            </div>
                        </div>

                        <div class="form-sec-row" runat="server" id="divs" visible="false">
                            <label>
                                <strong><strong>Staff Name :
                                </strong>
                            </label>
                            <asp:DropDownList ID="ddlstaff" runat="server"  Width="298px" AutoPostBack="True" OnSelectedIndexChanged="ddlstaff_SelectedIndexChanged"></asp:DropDownList>
                            <div class="clear">
                            </div>
                        </div>

                        <div class="form-sec-row">
                            <label><strong>Registration No :</strong></label>
                            <asp:TextBox ID="txtreg" runat="server" CssClass="textbox-medium1" ReadOnly="true"></asp:TextBox>
                            <asp:HiddenField ID="hdnLedgerId" runat="server" />
                            <asp:HiddenField ID="hdnregno" runat="server" />
                            <asp:Button ID="Button5" runat="server" CssClass="submit-search" Height="28px" Text="Quick Search" OnClientClick="ShowDialogPatient()" Visible="false" />
                            <asp:Button ID="btnFetch" runat="server" CssClass="submit-button" Height="28px" Text="Fetch" OnClick="btnFetch_Click" Visible="false" />
                            <div class="clear"></div>
                        </div>

                        <div class="form-sec-row" style="display: none;">
                            <label>
                                <strong>Patient's Address :</strong></label>
                            <asp:TextBox ID="TextBox2" runat="server" CssClass="textbox-medium1">
                            </asp:TextBox>
                            <div class="clear">
                            </div>
                        </div>

                        <div class="form-sec-row">
                            <label>
                                <strong>Doctor Name :</strong></label>
                            <asp:TextBox ID="TextBox3" runat="server" CssClass="textbox-medium1"></asp:TextBox>
                            <cc1:AutoCompleteExtender ServiceMethod="Searchdoc" OnClientItemSelected="autoCompleteEx_ItemSelected" MinimumPrefixLength="1"
                                CompletionInterval="100" EnableCaching="false" CompletionSetCount="10" TargetControlID="TextBox3" ID="AutoCompleteExtender2" runat="server"
                                FirstRowSelected="false">
                            </cc1:AutoCompleteExtender>
                            <div class="clear">
                            </div>
                        </div>

                        <div class="form-sec-row" style="display: none;">
                            <label>
                                <strong>Card No :</strong></label>
                            <asp:TextBox ID="TextBox4" runat="server" CssClass="textbox-medium1"></asp:TextBox>
                            <div class="clear">
                            </div>
                        </div>
                        <div class="form-sec-row">
                            <label>
                                <strong>Issued By:</strong></label>
                            <asp:TextBox ID="issueby" runat="server" CssClass="textbox-medium1"></asp:TextBox>
                            <div class="clear">
                            </div>
                        </div>


                        <div class="form-sec-row">
                            <label>
                                <strong>Sales/Issue Bill Date :<span style="color: red;">*</span></strong></label>
                            <asp:TextBox ID="TextBox6" runat="server" CssClass="DatepickerCall" TextMode="Date"></asp:TextBox>
                            <%--<asp:Label ID="Label3" runat="server" ForeColor="Green" Text="(DD/MM/YYYY)"></asp:Label>
                    <cc1:MaskedEditExtender ID="MaskedEditExtender1" runat="server" TargetControlID="TextBox6"
                                            Mask="99/99/9999" MessageValidatorTip="true" MaskType="Date" DisplayMoney="Left"
                                            AcceptNegative="Left" ErrorTooltipEnabled="True" CultureName="en-GB" />--%>
                            <div class="clear">
                            </div>
                        </div>
                        <div class="form-sec-row" style="display: none;">
                            <label>
                                <strong>Received By :</strong></label>
                            <asp:TextBox ID="receiveby" runat="server" CssClass="textbox-medium1"></asp:TextBox>
                            <div class="clear">
                            </div>
                        </div>
                        <div class="form-sec-row" style="display: none;">
                            <label>
                                <strong>Receive Date :</strong></label>
                            <asp:TextBox ID="receivedt" runat="server" CssClass="DatepickerCall" TextMode="Date"></asp:TextBox>
                            <%--<asp:Label ID="Label2" runat="server" ForeColor="Green" Text="(DD/MM/YYYY)"></asp:Label>
                    <cc1:MaskedEditExtender ID="MaskedEditExtender2" runat="server" TargetControlID="receivedt"
                                            Mask="99/99/9999" MessageValidatorTip="true" MaskType="Date" DisplayMoney="Left"
                                            AcceptNegative="Left" ErrorTooltipEnabled="True" CultureName="en-GB" />--%>
                            <div class="clear">
                            </div>
                        </div>

                        <div class="form-sec-row">
                            <label>
                                <strong>Cancel :</strong></label>
                            <asp:CheckBox ID="chkCancel" runat="server" />
                        </div>
                        <div class="form-sec-row">
                        </div>
                        <div class="form-sec-row" style="width: 100%; overflow: auto;">
                            <table style="text-align: left;">

                                <tr style='background-color: #FF9300;'>
                                    <td align="center" style="width: 15%;">
                                        <asp:Label ID="lblMedi" runat="server" Font-Bold="True" Text="Medicine/Consumables" Style="width: 100%;"></asp:Label>
                                    </td>
                                    <td align="center" style="width: 15%; display: none;">
                                        <asp:Label ID="lblMfg" runat="server" Font-Bold="True" Text="Manufacture " Style="width: 100%;"></asp:Label>
                                    </td>

                                    <td align="center" style="display: none;">
                                        <asp:Label ID="lblMediGrp" runat="server" Font-Bold="True" Text="Medicine Group" Style="width: 100%;"></asp:Label>
                                    </td>

                                    <td align="center" style="display: none;">
                                        <asp:Label ID="lblMediSubGrp" runat="server" Font-Bold="True" Text="Med. Sub Group" Width=""></asp:Label>
                                    </td>

                                    <td align="center" style="width: 10%;">
                                        <asp:Label ID="lblBatch" runat="server" Font-Bold="True" Text="Batch" Style="width: 100%;"></asp:Label>
                                    </td>
                                    <td align="center" style="width: 10%;">
                                        <asp:Label ID="lblExpiry" runat="server" Font-Bold="True" Text="Expiry" Style="width: 100%;"></asp:Label>
                                    </td>
                                    <td align="center" style="width: 10%;">
                                        <asp:Label ID="lblStock" runat="server" Font-Bold="True" Text="Cur Stock" Style="width: 100%;"></asp:Label>
                                    </td>
                                    <td align="center" style="width: 5%;">
                                        <asp:Label ID="lblUnit" runat="server" Font-Bold="True" Text="UOM Unit" Style="width: 100%;"></asp:Label>
                                    </td>
                                    <td align="center" style="width: 5%;">
                                        <asp:Label ID="lblSellingUnit" runat="server" Font-Bold="True" Text="Selling Unit" Style="width: 100%;"></asp:Label>
                                        <asp:Label ID="lblPackSize" runat="server" Font-Bold="True" Text="Pack Size" Style="width: 100%;" Visible="false"></asp:Label>
                                    </td>
                                    <td align="center" style="width: 7%;">
                                        <asp:Label ID="lblQty" runat="server" Font-Bold="True" Text="Quantity" Style="width: 100%;"></asp:Label>
                                    </td>
                                    <td align="center" style="width: 10%;">
                                        <asp:Label ID="lblUnitPrice" runat="server" Font-Bold="True" Text="MRP" Style="width: 100%;"></asp:Label>
                                    </td>
                                    <td align="center" style="width: 10%;">
                                        <asp:Label ID="lblDiscAmt" runat="server" Font-Bold="True" Text="Disc Amt" Style="width: 100%;"></asp:Label>
                                    </td>
                                    <td align="center" style="width: 10%;">
                                        <asp:Label ID="lblTaxableAmt" runat="server" Font-Bold="True" Text="Taxable Amt" Style="width: 100%;"></asp:Label>
                                    </td>
                                    <td align="center" style="width: 10%;">
                                        <asp:Label ID="lblHsn" runat="server" Font-Bold="True" Text="HSN Code" Style="width: 100%;"></asp:Label>
                                    </td>
                                    <td align="center" style="width: 10%;">
                                        <asp:Label ID="lblCgstRt" runat="server" Font-Bold="True" Text="Cgst Rate" Style="width: 100%;"></asp:Label>
                                    </td>
                                    <td align="center" style="width: 10%;">
                                        <asp:Label ID="lblCgstAmt" runat="server" Font-Bold="True" Text="Cgst Amt" Style="width: 100%;"></asp:Label>
                                    </td>
                                    <td align="center" style="width: 10%;">
                                        <asp:Label ID="lblSgstRt" runat="server" Font-Bold="True" Text="Sgst Rate" Style="width: 100%;"></asp:Label>
                                    </td>
                                    <td align="center" style="width: 10%;">
                                        <asp:Label ID="lblSgstAmt" runat="server" Font-Bold="True" Text="Sgst Amt" Style="width: 100%;"></asp:Label>
                                    </td>
                                    <td align="center" style="width: 10%;">
                                        <asp:Label ID="lblTotalPrice" runat="server" Font-Bold="True" Text="Total Amt" Style="width: 100%;"></asp:Label>
                                    </td>
                                </tr>
                                <tr>

                                    <td class="style1">
                                        <asp:DropDownList ID="ddlMedi1" runat="server" Width="150px" AutoPostBack="True" OnSelectedIndexChanged="ddlMedi1_SelectedIndexChanged">
                                        </asp:DropDownList>
                                        <asp:Label ID="lbliType1" runat="server" Visible="false"></asp:Label>
                                    </td>
                                    <td class="style1" style="display: none;">
                                        <asp:DropDownList ID="ddlMfg1" runat="server" Width="150px">
                                        </asp:DropDownList>
                                    </td>
                                    <td class="style1" style="display: none;">
                                        <asp:DropDownList ID="ddlMediGrp1" runat="server" AutoPostBack="true" Height="16px">
                                        </asp:DropDownList>
                                    </td>
                                    <td class="style1" style="display: none;" >
                                        
                                        <asp:DropDownList ID="ddlMediSubGrp1" runat="server" AutoPostBack="True">
                                        </asp:DropDownList>
                                    </td>


                                    <td class="style1">
                                        <asp:DropDownList ID="txtBatch1" runat="server" Width="100px" OnSelectedIndexChanged="txtBatch1_SelectedIndexChanged" AutoPostBack="true"></asp:DropDownList>
                                    </td>
                                    <td class="style1">
                                        <asp:TextBox ID="expdt1" runat="server" Width="100px" ReadOnly="true"> </asp:TextBox>
                                        <asp:TextBox ID="Calendar2" runat="server" Width="100px" TextMode="Date" Visible="false"> </asp:TextBox>
                                    </td>
                                    <td class="style1">
                                        <asp:TextBox ID="txtStock1" runat="server" Width="100px" ReadOnly="true" Style="text-align: right;"></asp:TextBox>
                                    </td>
                                    <td class="style1">
                                        <asp:TextBox ID="txtUnit1" runat="server" Width="100px" ReadOnly="true"></asp:TextBox>
                                    </td>
                                    <td class="style1">
                                        <asp:DropDownList ID="ddlSellingUnit1" runat="server" Width="100px" OnSelectedIndexChanged="ddlSellingUnit1_SelectedIndexChanged" AutoPostBack="true"></asp:DropDownList>
                                        <asp:TextBox ID="txtPackSize1" runat="server" Width="100px" Visible="false"></asp:TextBox>
                                    </td>
                                    <td class="style1">
                                        <asp:TextBox ID="txtQty1" runat="server" Width="100px" AutoPostBack="True" OnTextChanged="txtQty1_TextChanged" Style="text-align: right;"></asp:TextBox>
                                        <asp:HiddenField ID="qty1" runat="server" Value="" />
                                    </td>
                                    <td class="style1">
                                        <asp:TextBox ID="txtUnitPrice1" runat="server" Width="100px" Style="text-align: right;" OnTextChanged="txtUnitPrice1_TextChanged" AutoPostBack="true"></asp:TextBox>
                                    </td>
                                    <td class="style1">
                                        <asp:TextBox ID="txtdiscAmt1" runat="server" Width="100px" Style="text-align: right;" OnTextChanged="txtdiscAmt1_TextChanged" AutoPostBack="true"></asp:TextBox>
                                    </td>
                                    <td class="style1">
                                        <asp:TextBox ID="txtTaxableAmt1" runat="server" Width="100px" Style="text-align: right;"></asp:TextBox>
                                    </td>
                                    <td class="style1">
                                        <asp:TextBox ID="txtHsnCode1" runat="server" Width="100px" ReadOnly="true"></asp:TextBox>
                                    </td>
                                    <td class="style1">
                                        <asp:TextBox ID="txtCgstRt1" runat="server" Width="100px" Style="text-align: right;" ReadOnly="true"></asp:TextBox>
                                    </td>
                                    <td class="style1">
                                        <asp:TextBox ID="txtCgstAmt1" runat="server" Width="100px" Style="text-align: right;" ReadOnly="true"></asp:TextBox>
                                    </td>
                                    <td class="style1">
                                        <asp:TextBox ID="txtSgstRt1" runat="server" Width="100px" Style="text-align: right;" ReadOnly="true"></asp:TextBox>
                                    </td>
                                    <td class="style1">
                                        <asp:TextBox ID="txtSgstAmt1" runat="server" Width="100px" Style="text-align: right;" ReadOnly="true"></asp:TextBox>
                                    </td>
                                    <td class="style1">
                                        <asp:TextBox ID="txtTotalPrice1" runat="server" Width="100px" Style="text-align: right;" ReadOnly="true"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="style1">
                                        <asp:DropDownList ID="ddlMedi2" runat="server" Width="150px" AutoPostBack="True" OnSelectedIndexChanged="ddlMedi2_SelectedIndexChanged">
                                        </asp:DropDownList>
                                        <asp:Label ID="lbliType2" runat="server" Visible="false"></asp:Label>
                                    </td>
                                    <td class="style1" style="display: none;">
                                        <asp:DropDownList ID="ddlMfg2" runat="server" Width="150px">
                                        </asp:DropDownList>
                                    </td>
                                    <td class="style1" style="display: none;">
                                        <asp:DropDownList ID="ddlMediGrp2" runat="server"></asp:DropDownList>
                                    </td>

                                    <td class="style1" style="display: none;">
                                        <asp:DropDownList ID="ddlMediSubGrp2" runat="server"></asp:DropDownList>
                                    </td>

                                    <td class="style1">
                                        <asp:DropDownList ID="txtBatch2" runat="server" Width="100px" OnSelectedIndexChanged="txtBatch2_SelectedIndexChanged" AutoPostBack="true"></asp:DropDownList>
                                    </td>
                                    <td class="style1">
                                        <asp:TextBox ID="expdt2" runat="server" Width="100px" ReadOnly="true"> </asp:TextBox>
                                        <asp:TextBox ID="Calendar3" runat="server" Width="100px" TextMode="Date" Visible="false"></asp:TextBox>
                                    </td>
                                    <td class="style1">
                                        <asp:TextBox ID="txtStock2" runat="server" Width="100px" ReadOnly="true" Style="text-align: right;"></asp:TextBox>
                                    </td>
                                    <td class="style1">
                                        <asp:TextBox ID="txtUnit2" runat="server" Width="100px" ReadOnly="true"></asp:TextBox>
                                    </td>
                                    <td class="style1">
                                        <asp:DropDownList ID="ddlSellingUnit2" runat="server" Width="100px" OnSelectedIndexChanged="ddlSellingUnit2_SelectedIndexChanged" AutoPostBack="true"></asp:DropDownList>
                                        <asp:TextBox ID="txtPackSize2" runat="server" Width="100px" Visible="false"></asp:TextBox>
                                    </td>
                                    <td class="style1">
                                        <asp:TextBox ID="txtQty2" runat="server" Width="100px" AutoPostBack="True" OnTextChanged="txtQty2_TextChanged" Style="text-align: right;"></asp:TextBox>
                                        <asp:HiddenField ID="qty2" runat="server" Value="" />
                                    </td>
                                    <td class="style1">
                                        <asp:TextBox ID="txtUnitPrice2" runat="server" Width="100px" Style="text-align: right;" OnTextChanged="txtUnitPrice2_TextChanged" AutoPostBack="true"></asp:TextBox>
                                    </td>
                                    <td class="style1">
                                        <asp:TextBox ID="txtdiscAmt2" runat="server" Width="100px" Style="text-align: right;" OnTextChanged="txtdiscAmt2_TextChanged" AutoPostBack="true"></asp:TextBox>
                                    </td>
                                    <td class="style1">
                                        <asp:TextBox ID="txtTaxableAmt2" runat="server" Width="100px" Style="text-align: right;"></asp:TextBox>
                                    </td>
                                    <td class="style1">
                                        <asp:TextBox ID="txtHsnCode2" runat="server" Width="100px" ReadOnly="true"></asp:TextBox>
                                    </td>
                                    <td class="style1">
                                        <asp:TextBox ID="txtCgstRt2" runat="server" Width="100px" Style="text-align: right;" ReadOnly="true"></asp:TextBox>
                                    </td>
                                    <td class="style1">
                                        <asp:TextBox ID="txtCgstAmt2" runat="server" Width="100px" Style="text-align: right;" ReadOnly="true"></asp:TextBox>
                                    </td>
                                    <td class="style1">
                                        <asp:TextBox ID="txtSgstRt2" runat="server" Width="100px" Style="text-align: right;" ReadOnly="true"></asp:TextBox>
                                    </td>
                                    <td class="style1">
                                        <asp:TextBox ID="txtSgstAmt2" runat="server" Width="100px" Style="text-align: right;" ReadOnly="true"></asp:TextBox>
                                    </td>
                                    <td class="style1">
                                        <asp:TextBox ID="txtTotalPrice2" runat="server" Width="100px" Style="text-align: right;" ReadOnly="true"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="style1">
                                        <asp:DropDownList ID="ddlMedi3" runat="server" Width="150px" AutoPostBack="True" OnSelectedIndexChanged="ddlMedi3_SelectedIndexChanged">
                                        </asp:DropDownList>
                                        <asp:Label ID="lbliType3" runat="server" Visible="false"></asp:Label>
                                    </td>
                                    <td class="style1" style="display: none;">
                                        <asp:DropDownList ID="ddlMfg3" runat="server" Width="150px">
                                        </asp:DropDownList>
                                    </td>
                                    <td class="style1" style="display: none;">
                                        <asp:DropDownList ID="ddlMediGrp3" runat="server" Width="100px"
                                            AutoPostBack="true">
                                        </asp:DropDownList>
                                    </td>
                                    <td class="style1" style="display: none;">
                                        <asp:DropDownList ID="ddlMediSubGrp3" runat="server" Width="100px"
                                            AutoPostBack="True">
                                        </asp:DropDownList>
                                    </td>


                                    <td class="style1">
                                        <asp:DropDownList ID="txtBatch3" runat="server" Width="100px" OnSelectedIndexChanged="txtBatch3_SelectedIndexChanged" AutoPostBack="true"></asp:DropDownList>
                                    </td>
                                    <td class="style1">
                                        <asp:TextBox ID="expdt3" runat="server" Width="100px" ReadOnly="true"> </asp:TextBox>
                                        <asp:TextBox ID="Calendar4" runat="server" Width="100px" TextMode="Date" Visible="false"></asp:TextBox>
                                    </td>
                                    <td class="style1">
                                        <asp:TextBox ID="txtStock3" runat="server" Width="100px" ReadOnly="true" Style="text-align: right;"></asp:TextBox>
                                    </td>
                                    <td class="style1">
                                        <asp:TextBox ID="txtUnit3" runat="server" Width="100px" ReadOnly="true"></asp:TextBox>
                                    </td>
                                    <td class="style1">
                                        <asp:DropDownList ID="ddlSellingUnit3" runat="server" Width="100px" OnSelectedIndexChanged="ddlSellingUnit3_SelectedIndexChanged" AutoPostBack="true"></asp:DropDownList>
                                        <asp:TextBox ID="txtPackSize3" runat="server" Width="100px" Visible="false"></asp:TextBox>
                                    </td>
                                    <td class="style1">
                                        <asp:TextBox ID="txtQty3" runat="server" Width="100px" AutoPostBack="True" OnTextChanged="txtQty3_TextChanged" Style="text-align: right;"></asp:TextBox>
                                        <asp:HiddenField ID="qty3" runat="server" Value="" />
                                    </td>
                                    <td class="style1">
                                        <asp:TextBox ID="txtUnitPrice3" runat="server" Width="100px" Style="text-align: right;" OnTextChanged="txtUnitPrice3_TextChanged" AutoPostBack="true"></asp:TextBox>
                                    </td>
                                    <td class="style1">
                                        <asp:TextBox ID="txtdiscAmt3" runat="server" Width="100px" Style="text-align: right;" OnTextChanged="txtdiscAmt3_TextChanged" AutoPostBack="true"></asp:TextBox>
                                    </td>
                                    <td class="style1">
                                        <asp:TextBox ID="txtTaxableAmt3" runat="server" Width="100px" Style="text-align: right;"></asp:TextBox>
                                    </td>
                                    <td class="style1">
                                        <asp:TextBox ID="txtHsnCode3" runat="server" Width="100px" ReadOnly="true"></asp:TextBox>
                                    </td>
                                    <td class="style1">
                                        <asp:TextBox ID="txtCgstRt3" runat="server" Width="100px" Style="text-align: right;" ReadOnly="true"></asp:TextBox>
                                    </td>
                                    <td class="style1">
                                        <asp:TextBox ID="txtCgstAmt3" runat="server" Width="100px" Style="text-align: right;" ReadOnly="true"></asp:TextBox>
                                    </td>
                                    <td class="style1">
                                        <asp:TextBox ID="txtSgstRt3" runat="server" Width="100px" Style="text-align: right;" ReadOnly="true"></asp:TextBox>
                                    </td>
                                    <td class="style1">
                                        <asp:TextBox ID="txtSgstAmt3" runat="server" Width="100px" Style="text-align: right;" ReadOnly="true"></asp:TextBox>
                                    </td>
                                    <td class="style1">
                                        <asp:TextBox ID="txtTotalPrice3" runat="server" Width="100px" Style="text-align: right;" ReadOnly="true"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="style1">
                                        <asp:DropDownList ID="ddlMedi4" runat="server" Width="150px" AutoPostBack="True" OnSelectedIndexChanged="ddlMedi4_SelectedIndexChanged">
                                        </asp:DropDownList>
                                        <asp:Label ID="lbliType4" runat="server" Visible="false"></asp:Label>
                                    </td>
                                    <td class="style1" style="display: none;">
                                        <asp:DropDownList ID="ddlMfg4" runat="server" Width="150px">
                                        </asp:DropDownList>
                                    </td>
                                    <td class="style1" style="display: none;">
                                        <asp:DropDownList ID="ddlMediGrp4" runat="server" Width="100px"
                                            AutoPostBack="true">
                                        </asp:DropDownList>
                                    </td>

                                    <td class="style1" style="display: none;">
                                        <asp:DropDownList ID="ddlMediSubGrp4" runat="server" Width="100px"
                                            AutoPostBack="True">
                                        </asp:DropDownList>
                                    </td>


                                    <td class="style1">
                                        <asp:DropDownList ID="txtBatch4" runat="server" Width="100px" OnSelectedIndexChanged="txtBatch4_SelectedIndexChanged" AutoPostBack="true"></asp:DropDownList>
                                    </td>
                                    <td class="style1">
                                        <asp:TextBox ID="expdt4" runat="server" Width="100px" ReadOnly="true"> </asp:TextBox>
                                        <asp:TextBox ID="Calendar5" runat="server" Width="100px" TextMode="Date" Visible="false"></asp:TextBox>
                                    </td>
                                    <td class="style1">
                                        <asp:TextBox ID="txtStock4" runat="server" Width="100px" ReadOnly="true" Style="text-align: right;"></asp:TextBox>
                                    </td>
                                    <td class="style1">
                                        <asp:TextBox ID="txtUnit4" runat="server" Width="100px" ReadOnly="true"></asp:TextBox>
                                    </td>
                                    <td class="style1">
                                        <asp:DropDownList ID="ddlSellingUnit4" runat="server" Width="100px" OnSelectedIndexChanged="ddlSellingUnit4_SelectedIndexChanged" AutoPostBack="true"></asp:DropDownList>
                                        <asp:TextBox ID="txtPackSize4" runat="server" Width="100px" Visible="false"></asp:TextBox>
                                    </td>
                                    <td class="style1">
                                        <asp:TextBox ID="txtQty4" runat="server" Width="100px" AutoPostBack="True" OnTextChanged="txtQty4_TextChanged" Style="text-align: right;"></asp:TextBox>
                                        <asp:HiddenField ID="qty4" runat="server" Value="" />
                                    </td>
                                    <td class="style1">
                                        <asp:TextBox ID="txtUnitPrice4" runat="server" Width="100px" Style="text-align: right;" OnTextChanged="txtUnitPrice4_TextChanged" AutoPostBack="true"></asp:TextBox>
                                    </td>
                                    <td class="style1">
                                        <asp:TextBox ID="txtdiscAmt4" runat="server" Width="100px" Style="text-align: right;" OnTextChanged="txtdiscAmt4_TextChanged" AutoPostBack="true"></asp:TextBox>
                                    </td>
                                    <td class="style1">
                                        <asp:TextBox ID="txtTaxableAmt4" runat="server" Width="100px" Style="text-align: right;"></asp:TextBox>
                                    </td>
                                    <td class="style1">
                                        <asp:TextBox ID="txtHsnCode4" runat="server" Width="100px" ReadOnly="true"></asp:TextBox>
                                    </td>
                                    <td class="style1">
                                        <asp:TextBox ID="txtCgstRt4" runat="server" Width="100px" Style="text-align: right;" ReadOnly="true"></asp:TextBox>
                                    </td>
                                    <td class="style1">
                                        <asp:TextBox ID="txtCgstAmt4" runat="server" Width="100px" Style="text-align: right;" ReadOnly="true"></asp:TextBox>
                                    </td>
                                    <td class="style1">
                                        <asp:TextBox ID="txtSgstRt4" runat="server" Width="100px" Style="text-align: right;" ReadOnly="true"></asp:TextBox>
                                    </td>
                                    <td class="style1">
                                        <asp:TextBox ID="txtSgstAmt4" runat="server" Width="100px" Style="text-align: right;" ReadOnly="true"></asp:TextBox>
                                    </td>
                                    <td class="style1">
                                        <asp:TextBox ID="txtTotalPrice4" runat="server" Width="100px" Style="text-align: right;" ReadOnly="true"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="style1">
                                        <asp:DropDownList ID="ddlMedi5" runat="server" Width="150px"
                                            AutoPostBack="True" OnSelectedIndexChanged="ddlMedi5_SelectedIndexChanged">
                                        </asp:DropDownList>
                                        <asp:Label ID="lbliType5" runat="server" Visible="false"></asp:Label>
                                    </td>
                                    <td class="style1" style="display: none;">
                                        <asp:DropDownList ID="ddlMfg5" runat="server" Width="150px">
                                        </asp:DropDownList>
                                    </td>
                                    <td class="style1" style="display: none;">
                                        <asp:DropDownList ID="ddlMediGrp5" runat="server" Width="100px"
                                            AutoPostBack="true">
                                        </asp:DropDownList>
                                    </td>

                                    <td class="style1" style="display: none;">
                                        <asp:DropDownList ID="ddlMediSubGrp5" runat="server" Width="100px"
                                            AutoPostBack="True">
                                        </asp:DropDownList>
                                    </td>


                                    <td class="style1">
                                        <asp:DropDownList ID="txtBatch5" runat="server" Width="100px" OnSelectedIndexChanged="txtBatch5_SelectedIndexChanged" AutoPostBack="true"></asp:DropDownList>
                                    </td>
                                    <td class="style1">
                                        <asp:TextBox ID="expdt5" runat="server" Width="100px" ReadOnly="true"> </asp:TextBox>
                                        <asp:TextBox ID="Calendar6" runat="server" Width="100px" TextMode="Date" Visible="false"></asp:TextBox>
                                    </td>
                                    <td class="style1">
                                        <asp:TextBox ID="txtStock5" runat="server" Width="100px" ReadOnly="true" Style="text-align: right;"></asp:TextBox>
                                    </td>
                                    <td class="style1">
                                        <asp:TextBox ID="txtUnit5" runat="server" Width="100px" ReadOnly="true"></asp:TextBox>
                                    </td>
                                    <td class="style1">
                                        <asp:DropDownList ID="ddlSellingUnit5" runat="server" Width="100px" OnSelectedIndexChanged="ddlSellingUnit5_SelectedIndexChanged" AutoPostBack="true"></asp:DropDownList>
                                        <asp:TextBox ID="txtPackSize5" runat="server" Width="100px" Visible="false"></asp:TextBox>
                                    </td>
                                    <td class="style1">
                                        <asp:TextBox ID="txtQty5" runat="server" Width="100px" AutoPostBack="True" OnTextChanged="txtQty5_TextChanged" Style="text-align: right;"></asp:TextBox>
                                        <asp:HiddenField ID="qty5" runat="server" Value="" />
                                    </td>
                                    <td class="style1">
                                        <asp:TextBox ID="txtUnitPrice5" runat="server" Width="100px" Style="text-align: right;" OnTextChanged="txtUnitPrice5_TextChanged" AutoPostBack="true"></asp:TextBox>
                                    </td>
                                    <td class="style1">
                                        <asp:TextBox ID="txtdiscAmt5" runat="server" Width="100px" Style="text-align: right;" OnTextChanged="txtdiscAmt5_TextChanged" AutoPostBack="true"></asp:TextBox>
                                    </td>
                                    <td class="style1">
                                        <asp:TextBox ID="txtTaxableAmt5" runat="server" Width="100px" Style="text-align: right;"></asp:TextBox>
                                    </td>
                                    <td class="style1">
                                        <asp:TextBox ID="txtHsnCode5" runat="server" Width="100px" ReadOnly="true"></asp:TextBox>
                                    </td>
                                    <td class="style1">
                                        <asp:TextBox ID="txtCgstRt5" runat="server" Width="100px" Style="text-align: right;" ReadOnly="true"></asp:TextBox>
                                    </td>
                                    <td class="style1">
                                        <asp:TextBox ID="txtCgstAmt5" runat="server" Width="100px" Style="text-align: right;" ReadOnly="true"></asp:TextBox>
                                    </td>
                                    <td class="style1">
                                        <asp:TextBox ID="txtSgstRt5" runat="server" Width="100px" Style="text-align: right;" ReadOnly="true"></asp:TextBox>
                                    </td>
                                    <td class="style1">
                                        <asp:TextBox ID="txtSgstAmt5" runat="server" Width="100px" Style="text-align: right;" ReadOnly="true"></asp:TextBox>
                                    </td>
                                    <td class="style1">
                                        <asp:TextBox ID="txtTotalPrice5" runat="server" Width="100px" Style="text-align: right;" ReadOnly="true"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="style1">
                                        <asp:DropDownList ID="ddlMedi6" runat="server" Width="150px" AutoPostBack="True" OnSelectedIndexChanged="ddlMedi6_SelectedIndexChanged">
                                        </asp:DropDownList>
                                        <asp:Label ID="lbliType6" runat="server" Visible="false"></asp:Label>
                                    </td>
                                    <td class="style1" style="display: none;">
                                        <asp:DropDownList ID="ddlMfg6" runat="server" Width="150px">
                                        </asp:DropDownList>
                                    </td>
                                    <td class="style1" style="display: none;">
                                        <asp:DropDownList ID="ddlMediGrp6" runat="server" Width="100px"
                                            AutoPostBack="true">
                                        </asp:DropDownList>
                                    </td>

                                    <td class="style1" style="display: none;">
                                        <asp:DropDownList ID="ddlMediSubGrp6" runat="server" Width="100px"
                                            AutoPostBack="True">
                                        </asp:DropDownList>
                                    </td>


                                    <td class="style1">
                                        <asp:DropDownList ID="txtBatch6" runat="server" Width="100px" OnSelectedIndexChanged="txtBatch6_SelectedIndexChanged" AutoPostBack="true"></asp:DropDownList>
                                    </td>
                                    <td class="style1">
                                        <asp:TextBox ID="expdt6" runat="server" Width="100px" ReadOnly="true"> </asp:TextBox>
                                        <asp:TextBox ID="Calendar7" runat="server" Width="100px" TextMode="Date" Visible="false"></asp:TextBox>
                                    </td>
                                    <td class="style1">
                                        <asp:TextBox ID="txtStock6" runat="server" Width="100px" ReadOnly="true" Style="text-align: right;"></asp:TextBox>
                                    </td>

                                    <td class="style1">
                                        <asp:TextBox ID="txtUnit6" runat="server" Width="100px" ReadOnly="true"></asp:TextBox>
                                    </td>
                                    <td class="style1">
                                        <asp:DropDownList ID="ddlSellingUnit6" runat="server" Width="100px" OnSelectedIndexChanged="ddlSellingUnit6_SelectedIndexChanged" AutoPostBack="true"></asp:DropDownList>
                                        <asp:TextBox ID="txtPackSize6" runat="server" Width="100px" Visible="false"></asp:TextBox>
                                    </td>

                                    <td class="style1">
                                        <asp:TextBox ID="txtQty6" runat="server" Width="100px" AutoPostBack="True" OnTextChanged="txtQty6_TextChanged" Style="text-align: right;"></asp:TextBox>
                                        <asp:HiddenField ID="qty6" runat="server" Value="" />
                                    </td>
                                    <td class="style1">
                                        <asp:TextBox ID="txtUnitPrice6" runat="server" Width="100px" Style="text-align: right;" OnTextChanged="txtUnitPrice6_TextChanged" AutoPostBack="true"></asp:TextBox>
                                    </td>
                                    <td class="style1">
                                        <asp:TextBox ID="txtdiscAmt6" runat="server" Width="100px" Style="text-align: right;" OnTextChanged="txtdiscAmt6_TextChanged" AutoPostBack="true"></asp:TextBox>
                                    </td>
                                    <td class="style1">
                                        <asp:TextBox ID="txtTaxableAmt6" runat="server" Width="100px" Style="text-align: right;"></asp:TextBox>
                                    </td>
                                    <td class="style1">
                                        <asp:TextBox ID="txtHsnCode6" runat="server" Width="100px" ReadOnly="true"></asp:TextBox>
                                    </td>
                                    <td class="style1">
                                        <asp:TextBox ID="txtCgstRt6" runat="server" Width="100px" Style="text-align: right;" ReadOnly="true"></asp:TextBox>
                                    </td>
                                    <td class="style1">
                                        <asp:TextBox ID="txtCgstAmt6" runat="server" Width="100px" Style="text-align: right;" ReadOnly="true"></asp:TextBox>
                                    </td>
                                    <td class="style1">
                                        <asp:TextBox ID="txtSgstRt6" runat="server" Width="100px" Style="text-align: right;" ReadOnly="true"></asp:TextBox>
                                    </td>
                                    <td class="style1">
                                        <asp:TextBox ID="txtSgstAmt6" runat="server" Width="100px" Style="text-align: right;" ReadOnly="true"></asp:TextBox>
                                    </td>
                                    <td class="style1">
                                        <asp:TextBox ID="txtTotalPrice6" runat="server" Width="100px" Style="text-align: right;" ReadOnly="true"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="style1">
                                        <asp:DropDownList ID="ddlMedi7" runat="server" Width="150px" AutoPostBack="True" OnSelectedIndexChanged="ddlMedi7_SelectedIndexChanged">
                                        </asp:DropDownList>
                                        <asp:Label ID="lbliType7" runat="server" Visible="false"></asp:Label>
                                    </td>
                                    <td class="style1" style="display: none;">
                                        <asp:DropDownList ID="ddlMfg7" runat="server" Width="150px">
                                        </asp:DropDownList>
                                    </td>
                                    <td class="style1" style="display: none;">
                                        <asp:DropDownList ID="ddlMediGrp7" runat="server" Width="100px" AutoPostBack="true">
                                        </asp:DropDownList>
                                    </td>

                                    <td class="style1" style="display: none;">
                                        <asp:DropDownList ID="ddlMediSubGrp7" runat="server" Width="100px" AutoPostBack="True">
                                        </asp:DropDownList>
                                    </td>


                                    <td class="style1">
                                        <asp:DropDownList ID="txtBatch7" runat="server" Width="100px" OnSelectedIndexChanged="txtBatch7_SelectedIndexChanged" AutoPostBack="true"></asp:DropDownList>
                                    </td>
                                    <td class="style1">
                                        <asp:TextBox ID="expdt7" runat="server" Width="100px" ReadOnly="true"> </asp:TextBox>
                                        <asp:TextBox ID="Calendar8" runat="server" Width="100px" TextMode="Date" Visible="false"></asp:TextBox>
                                    </td>
                                    <td class="style1">
                                        <asp:TextBox ID="txtStock7" runat="server" Width="100px" ReadOnly="true" Style="text-align: right;"></asp:TextBox>
                                    </td>
                                    <td class="style1">
                                        <asp:TextBox ID="txtUnit7" runat="server" Width="100px" ReadOnly="true"></asp:TextBox>
                                    </td>
                                    <td class="style1">
                                        <asp:DropDownList ID="ddlSellingUnit7" runat="server" Width="100px" OnSelectedIndexChanged="ddlSellingUnit7_SelectedIndexChanged" AutoPostBack="true"></asp:DropDownList>
                                        <asp:TextBox ID="txtPackSize7" runat="server" Width="100px" Visible="false"></asp:TextBox>
                                    </td>
                                    <td class="style1">
                                        <asp:TextBox ID="txtQty7" runat="server" Width="100px" AutoPostBack="True" OnTextChanged="txtQty7_TextChanged" Style="text-align: right;"></asp:TextBox>
                                        <asp:HiddenField ID="qty7" runat="server" Value="" />
                                    </td>
                                    <td class="style1">
                                        <asp:TextBox ID="txtUnitPrice7" runat="server" Width="100px" Style="text-align: right;" OnTextChanged="txtUnitPrice7_TextChanged" AutoPostBack="true"></asp:TextBox>
                                    </td>
                                    <td class="style1">
                                        <asp:TextBox ID="txtdiscAmt7" runat="server" Width="100px" Style="text-align: right;" OnTextChanged="txtdiscAmt7_TextChanged" AutoPostBack="true"></asp:TextBox>
                                    </td>
                                    <td class="style1">
                                        <asp:TextBox ID="txtTaxableAmt7" runat="server" Width="100px" Style="text-align: right;"></asp:TextBox>
                                    </td>
                                    <td class="style1">
                                        <asp:TextBox ID="txtHsnCode7" runat="server" Width="100px" ReadOnly="true"></asp:TextBox>
                                    </td>
                                    <td class="style1">
                                        <asp:TextBox ID="txtCgstRt7" runat="server" Width="100px" Style="text-align: right;" ReadOnly="true"></asp:TextBox>
                                    </td>
                                    <td class="style1">
                                        <asp:TextBox ID="txtCgstAmt7" runat="server" Width="100px" Style="text-align: right;" ReadOnly="true"></asp:TextBox>
                                    </td>
                                    <td class="style1">
                                        <asp:TextBox ID="txtSgstRt7" runat="server" Width="100px" Style="text-align: right;" ReadOnly="true"></asp:TextBox>
                                    </td>
                                    <td class="style1">
                                        <asp:TextBox ID="txtSgstAmt7" runat="server" Width="100px" Style="text-align: right;" ReadOnly="true"></asp:TextBox>
                                    </td>
                                    <td class="style1">
                                        <asp:TextBox ID="txtTotalPrice7" runat="server" Width="100px" Style="text-align: right;" ReadOnly="true"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="style1">
                                        <asp:DropDownList ID="ddlMedi8" runat="server" Width="150px" AutoPostBack="True" OnSelectedIndexChanged="ddlMedi8_SelectedIndexChanged">
                                        </asp:DropDownList>
                                        <asp:Label ID="lbliType8" runat="server" Visible="false"></asp:Label>
                                    </td>
                                    <td class="style1" style="display: none;">
                                        <asp:DropDownList ID="ddlMfg8" runat="server" Width="150px">
                                        </asp:DropDownList>
                                    </td>
                                    <td class="style1" style="display: none;">
                                        <asp:DropDownList ID="ddlMediGrp8" runat="server" Width="100px"
                                            AutoPostBack="true">
                                        </asp:DropDownList>
                                    </td>

                                    <td class="style1" style="display: none;">
                                        <asp:DropDownList ID="ddlMediSubGrp8" runat="server" Width="100px" AutoPostBack="True">
                                        </asp:DropDownList>
                                    </td>


                                    <td class="style1">
                                        <asp:DropDownList ID="txtBatch8" runat="server" Width="100px" OnSelectedIndexChanged="txtBatch8_SelectedIndexChanged" AutoPostBack="true"></asp:DropDownList>
                                    </td>
                                    <td class="style1">
                                        <asp:TextBox ID="expdt8" runat="server" Width="100px" ReadOnly="true"> </asp:TextBox>
                                        <asp:TextBox ID="Calendar9" runat="server" Width="100px" TextMode="Date" Visible="false"></asp:TextBox>
                                    </td>
                                    <td class="style1">
                                        <asp:TextBox ID="txtStock8" runat="server" Width="100px" ReadOnly="true" Style="text-align: right;"></asp:TextBox>
                                    </td>
                                    <td class="style1">
                                        <asp:TextBox ID="txtUnit8" runat="server" Width="100px" ReadOnly="true"></asp:TextBox>
                                    </td>
                                    <td class="style1">
                                        <asp:DropDownList ID="ddlSellingUnit8" runat="server" Width="100px" OnSelectedIndexChanged="ddlSellingUnit8_SelectedIndexChanged" AutoPostBack="true"></asp:DropDownList>
                                        <asp:TextBox ID="txtPackSize8" runat="server" Width="100px" Visible="false"></asp:TextBox>
                                    </td>
                                    <td class="style1">
                                        <asp:TextBox ID="txtQty8" runat="server" Width="100px" AutoPostBack="True" OnTextChanged="txtQty8_TextChanged" Style="text-align: right;"></asp:TextBox>
                                        <asp:HiddenField ID="qty8" runat="server" Value="" />
                                    </td>
                                    <td class="style1">
                                        <asp:TextBox ID="txtUnitPrice8" runat="server" Width="100px" Style="text-align: right;" OnTextChanged="txtUnitPrice8_TextChanged" AutoPostBack="true"></asp:TextBox>
                                    </td>
                                    <td class="style1">
                                        <asp:TextBox ID="txtdiscAmt8" runat="server" Width="100px" Style="text-align: right;" OnTextChanged="txtdiscAmt8_TextChanged" AutoPostBack="true"></asp:TextBox>
                                    </td>
                                    <td class="style1">
                                        <asp:TextBox ID="txtTaxableAmt8" runat="server" Width="100px" Style="text-align: right;"></asp:TextBox>
                                    </td>
                                    <td class="style1">
                                        <asp:TextBox ID="txtHsnCode8" runat="server" Width="100px" ReadOnly="true"></asp:TextBox>
                                    </td>
                                    <td class="style1">
                                        <asp:TextBox ID="txtCgstRt8" runat="server" Width="100px" Style="text-align: right;" ReadOnly="true"></asp:TextBox>
                                    </td>
                                    <td class="style1">
                                        <asp:TextBox ID="txtCgstAmt8" runat="server" Width="100px" Style="text-align: right;" ReadOnly="true"></asp:TextBox>
                                    </td>
                                    <td class="style1">
                                        <asp:TextBox ID="txtSgstRt8" runat="server" Width="100px" Style="text-align: right;" ReadOnly="true"></asp:TextBox>
                                    </td>
                                    <td class="style1">
                                        <asp:TextBox ID="txtSgstAmt8" runat="server" Width="100px" Style="text-align: right;" ReadOnly="true"></asp:TextBox>
                                    </td>
                                    <td class="style1">
                                        <asp:TextBox ID="txtTotalPrice8" runat="server" Width="100px" Style="text-align: right;" ReadOnly="true"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="style1">
                                        <asp:DropDownList ID="ddlMedi9" runat="server" Width="150px" AutoPostBack="True" OnSelectedIndexChanged="ddlMedi9_SelectedIndexChanged">
                                        </asp:DropDownList>
                                        <asp:Label ID="lbliType9" runat="server" Visible="false"></asp:Label>
                                    </td>
                                    <td class="style1" style="display: none;">
                                        <asp:DropDownList ID="ddlMfg9" runat="server" Width="150px">
                                        </asp:DropDownList>
                                    </td>
                                    <td class="style1" style="display: none;">
                                        <asp:DropDownList ID="ddlMediGrp9" runat="server" Width="100px"
                                            AutoPostBack="true">
                                        </asp:DropDownList>
                                    </td>

                                    <td class="style1" style="display: none;">
                                        <asp:DropDownList ID="ddlMediSubGrp9" runat="server" Width="100px"
                                            AutoPostBack="True">
                                        </asp:DropDownList>
                                    </td>


                                    <td class="style1">
                                        <asp:DropDownList ID="txtBatch9" runat="server" Width="100px" OnSelectedIndexChanged="txtBatch9_SelectedIndexChanged" AutoPostBack="true"></asp:DropDownList>
                                    </td>
                                    <td class="style1">
                                        <asp:TextBox ID="expdt9" runat="server" Width="100px" ReadOnly="true"> </asp:TextBox>
                                        <asp:TextBox ID="Calendar10" runat="server" Width="100px" TextMode="Date" Visible="false"></asp:TextBox>
                                    </td>
                                    <td class="style1">
                                        <asp:TextBox ID="txtStock9" runat="server" Width="100px" ReadOnly="true" Style="text-align: right;"></asp:TextBox>
                                    </td>
                                    <td class="style1">
                                        <asp:TextBox ID="txtUnit9" runat="server" Width="100px" ReadOnly="true"></asp:TextBox>
                                    </td>
                                    <td class="style1">
                                        <asp:DropDownList ID="ddlSellingUnit9" runat="server" Width="100px" OnSelectedIndexChanged="ddlSellingUnit9_SelectedIndexChanged" AutoPostBack="true"></asp:DropDownList>
                                        <asp:TextBox ID="txtPackSize9" runat="server" Width="100px" Visible="false"></asp:TextBox>
                                    </td>
                                    <td class="style1">
                                        <asp:TextBox ID="txtQty9" runat="server" Width="100px" AutoPostBack="True" OnTextChanged="txtQty9_TextChanged" Style="text-align: right;"></asp:TextBox>
                                        <asp:HiddenField ID="qty9" runat="server" Value="" />
                                    </td>
                                    <td class="style1">
                                        <asp:TextBox ID="txtUnitPrice9" runat="server" Width="100px" Style="text-align: right;" OnTextChanged="txtUnitPrice9_TextChanged" AutoPostBack="true"></asp:TextBox>
                                    </td>
                                    <td class="style1">
                                        <asp:TextBox ID="txtdiscAmt9" runat="server" Width="100px" Style="text-align: right;" OnTextChanged="txtdiscAmt9_TextChanged" AutoPostBack="true"></asp:TextBox>
                                    </td>
                                    <td class="style1">
                                        <asp:TextBox ID="txtTaxableAmt9" runat="server" Width="100px" Style="text-align: right;"></asp:TextBox>
                                    </td>
                                    <td class="style1">
                                        <asp:TextBox ID="txtHsnCode9" runat="server" Width="100px" ReadOnly="true"></asp:TextBox>
                                    </td>
                                    <td class="style1">
                                        <asp:TextBox ID="txtCgstRt9" runat="server" Width="100px" Style="text-align: right;" ReadOnly="true"></asp:TextBox>
                                    </td>
                                    <td class="style1">
                                        <asp:TextBox ID="txtCgstAmt9" runat="server" Width="100px" Style="text-align: right;" ReadOnly="true"></asp:TextBox>
                                    </td>
                                    <td class="style1">
                                        <asp:TextBox ID="txtSgstRt9" runat="server" Width="100px" Style="text-align: right;" ReadOnly="true"></asp:TextBox>
                                    </td>
                                    <td class="style1">
                                        <asp:TextBox ID="txtSgstAmt9" runat="server" Width="100px" Style="text-align: right;" ReadOnly="true"></asp:TextBox>
                                    </td>
                                    <td class="style1">
                                        <asp:TextBox ID="txtTotalPrice9" runat="server" Width="100px" Style="text-align: right;" ReadOnly="true"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="style1">
                                        <asp:DropDownList ID="ddlMedi10" runat="server" Width="150px" AutoPostBack="True" OnSelectedIndexChanged="ddlMedi10_SelectedIndexChanged">
                                        </asp:DropDownList>
                                        <asp:Label ID="lbliType10" runat="server" Visible="false"></asp:Label>
                                    </td>
                                    <td class="style1" style="display: none;">
                                        <asp:DropDownList ID="ddlMfg10" runat="server" Width="150px">
                                        </asp:DropDownList>
                                    </td>
                                    <td class="style1" style="display: none;">
                                        <asp:DropDownList ID="ddlMediGrp10" runat="server" Width="100px"
                                            AutoPostBack="true">
                                        </asp:DropDownList>
                                    </td>

                                    <td class="style1" style="display: none;">
                                        <asp:DropDownList ID="ddlMediSubGrp10" runat="server" Width="100px"
                                            AutoPostBack="True">
                                        </asp:DropDownList>
                                    </td>


                                    <td class="style1">
                                        <asp:DropDownList ID="txtBatch10" runat="server" Width="100px" OnSelectedIndexChanged="txtBatch10_SelectedIndexChanged" AutoPostBack="true"></asp:DropDownList>
                                    </td>
                                    <td class="style1">
                                        <asp:TextBox ID="expdt10" runat="server" Width="100px" ReadOnly="true"> </asp:TextBox>
                                        <asp:TextBox ID="Calendar11" runat="server" Width="100px" TextMode="Date" Visible="false"> </asp:TextBox>
                                    </td>
                                    <td class="style1">
                                        <asp:TextBox ID="txtStock10" runat="server" Width="100px" ReadOnly="true" Style="text-align: right;"></asp:TextBox>
                                    </td>
                                    <td class="style1">
                                        <asp:TextBox ID="txtUnit10" runat="server" Width="100px" ReadOnly="true"></asp:TextBox>
                                    </td>
                                    <td class="style1">
                                        <asp:DropDownList ID="ddlSellingUnit10" runat="server" Width="100px" OnSelectedIndexChanged="ddlSellingUnit10_SelectedIndexChanged" AutoPostBack="true"></asp:DropDownList>
                                        <asp:TextBox ID="txtPackSize10" runat="server" Width="100px" Visible="false"></asp:TextBox>
                                    </td>
                                    <td class="style1">
                                        <asp:TextBox ID="txtQty10" runat="server" Width="100px" AutoPostBack="True" OnTextChanged="txtQty10_TextChanged" Style="text-align: right;"></asp:TextBox>
                                        <asp:HiddenField ID="qty10" runat="server" Value="" />
                                    </td>
                                    <td class="style1">
                                        <asp:TextBox ID="txtUnitPrice10" runat="server" Width="100px" Style="text-align: right;" OnTextChanged="txtUnitPrice10_TextChanged" AutoPostBack="true"></asp:TextBox>
                                    </td>
                                    <td class="style1">
                                        <asp:TextBox ID="txtdiscAmt10" runat="server" Width="100px" Style="text-align: right;" OnTextChanged="txtdiscAmt10_TextChanged" AutoPostBack="true"></asp:TextBox>
                                    </td>
                                    <td class="style1">
                                        <asp:TextBox ID="txtTaxableAmt10" runat="server" Width="100px" Style="text-align: right;"></asp:TextBox>
                                    </td>
                                    <td class="style1">
                                        <asp:TextBox ID="txtHsnCode10" runat="server" Width="100px"></asp:TextBox>
                                    </td>
                                    <td class="style1">
                                        <asp:TextBox ID="txtCgstRt10" runat="server" Width="100px" Style="text-align: right;" ReadOnly="true"></asp:TextBox>
                                    </td>
                                    <td class="style1">
                                        <asp:TextBox ID="txtCgstAmt10" runat="server" Width="100px" Style="text-align: right;" ReadOnly="true"></asp:TextBox>
                                    </td>
                                    <td class="style1">
                                        <asp:TextBox ID="txtSgstRt10" runat="server" Width="100px" Style="text-align: right;" ReadOnly="true"></asp:TextBox>
                                    </td>
                                    <td class="style1">
                                        <asp:TextBox ID="txtSgstAmt10" runat="server" Width="100px" Style="text-align: right;" ReadOnly="true"></asp:TextBox>
                                    </td>
                                    <td class="style1">
                                        <asp:TextBox ID="txtTotalPrice10" runat="server" Width="100px" Style="text-align: right;" ReadOnly="true"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="style1">
                                        <asp:DropDownList ID="ddlMedi11" runat="server" Width="150px" AutoPostBack="True" OnSelectedIndexChanged="ddlMedi11_SelectedIndexChanged">
                                        </asp:DropDownList>
                                        <asp:Label ID="lbliType11" runat="server" Visible="false"></asp:Label>
                                    </td>
                                    <td class="style1" style="display: none;">
                                        <asp:DropDownList ID="ddlMfg11" runat="server" Width="150px">
                                        </asp:DropDownList>
                                    </td>
                                    <td class="style1" style="display: none;">
                                        <asp:DropDownList ID="ddlMediGrp11" runat="server" Width="100px"
                                            AutoPostBack="true">
                                        </asp:DropDownList>
                                    </td>

                                    <td class="style1" style="display: none;">
                                        <asp:DropDownList ID="ddlMediSubGrp11" runat="server" Width="100px"
                                            AutoPostBack="True">
                                        </asp:DropDownList>
                                    </td>


                                    <td class="style1">
                                        <asp:DropDownList ID="txtBatch11" runat="server" Width="100px" OnSelectedIndexChanged="txtBatch11_SelectedIndexChanged" AutoPostBack="true"></asp:DropDownList>
                                    </td>
                                    <td class="style1">
                                        <asp:TextBox ID="expdt11" runat="server" Width="100px" ReadOnly="true"> </asp:TextBox>
                                        <asp:TextBox ID="Calendar12" runat="server" Width="100px" TextMode="Date" Visible="false"> </asp:TextBox>
                                    </td>
                                    <td class="style1">
                                        <asp:TextBox ID="txtStock11" runat="server" Width="100px" ReadOnly="true" Style="text-align: right;"></asp:TextBox>
                                    </td>
                                    <td class="style1">
                                        <asp:TextBox ID="txtUnit11" runat="server" Width="100px" ReadOnly="true"></asp:TextBox>
                                    </td>
                                    <td class="style1">
                                        <asp:DropDownList ID="ddlSellingUnit11" runat="server" Width="100px" OnSelectedIndexChanged="ddlSellingUnit11_SelectedIndexChanged" AutoPostBack="true"></asp:DropDownList>
                                        <asp:TextBox ID="txtPackSize11" runat="server" Width="100px" Visible="false"></asp:TextBox>
                                    </td>
                                    <td class="style1">
                                        <asp:TextBox ID="txtQty11" runat="server" Width="100px" AutoPostBack="True" OnTextChanged="txtQty11_TextChanged" Style="text-align: right;"></asp:TextBox>
                                        <asp:HiddenField ID="qty11" runat="server" Value="" />
                                    </td>
                                    <td class="style1">
                                        <asp:TextBox ID="txtUnitPrice11" runat="server" Width="100px" Style="text-align: right;" OnTextChanged="txtUnitPrice11_TextChanged" AutoPostBack="true"></asp:TextBox>
                                    </td>
                                    <td class="style1">
                                        <asp:TextBox ID="txtdiscAmt11" runat="server" Width="100px" Style="text-align: right;" OnTextChanged="txtdiscAmt11_TextChanged" AutoPostBack="true"></asp:TextBox>
                                    </td>
                                    <td class="style1">
                                        <asp:TextBox ID="txtTaxableAmt11" runat="server" Width="100px" Style="text-align: right;"></asp:TextBox>
                                    </td>
                                    <td class="style1">
                                        <asp:TextBox ID="txtHsnCode11" runat="server" Width="100px" ReadOnly="true"></asp:TextBox>
                                    </td>
                                    <td class="style1">
                                        <asp:TextBox ID="txtCgstRt11" runat="server" Width="100px" Style="text-align: right;" ReadOnly="true"></asp:TextBox>
                                    </td>
                                    <td class="style1">
                                        <asp:TextBox ID="txtCgstAmt11" runat="server" Width="100px" Style="text-align: right;" ReadOnly="true"></asp:TextBox>
                                    </td>
                                    <td class="style1">
                                        <asp:TextBox ID="txtSgstRt11" runat="server" Width="100px" Style="text-align: right;" ReadOnly="true"></asp:TextBox>
                                    </td>
                                    <td class="style1">
                                        <asp:TextBox ID="txtSgstAmt11" runat="server" Width="100px" Style="text-align: right;" ReadOnly="true"></asp:TextBox>
                                    </td>
                                    <td class="style1">
                                        <asp:TextBox ID="txtTotalPrice11" runat="server" Width="100px" Style="text-align: right;" ReadOnly="true"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="style1">
                                        <asp:DropDownList ID="ddlMedi12" runat="server" Width="150px" AutoPostBack="True" OnSelectedIndexChanged="ddlMedi12_SelectedIndexChanged">
                                        </asp:DropDownList>
                                        <asp:Label ID="lbliType12" runat="server" Visible="false"></asp:Label>
                                    </td>
                                    <td class="style1" style="display: none;">
                                        <asp:DropDownList ID="ddlMfg12" runat="server" Width="150px">
                                        </asp:DropDownList>
                                    </td>
                                    <td class="style1" style="display: none;">
                                        <asp:DropDownList ID="ddlMediGrp12" runat="server" Width="100px"
                                            AutoPostBack="true">
                                        </asp:DropDownList>
                                    </td>

                                    <td class="style1" style="display: none;">
                                        <asp:DropDownList ID="ddlMediSubGrp12" runat="server" Width="100px"
                                            AutoPostBack="True">
                                        </asp:DropDownList>
                                    </td>



                                    <td class="style1">
                                        <asp:DropDownList ID="txtBatch12" runat="server" Width="100px" OnSelectedIndexChanged="txtBatch12_SelectedIndexChanged" AutoPostBack="true"></asp:DropDownList>
                                    </td>
                                    <td class="style1">
                                        <asp:TextBox ID="expdt12" runat="server" Width="100px" ReadOnly="true"> </asp:TextBox>
                                        <asp:TextBox ID="Calendar13" runat="server" Width="100px" TextMode="Date" Visible="false"> </asp:TextBox>
                                    </td>
                                    <td class="style1">
                                        <asp:TextBox ID="txtStock12" runat="server" Width="100px" ReadOnly="true" Style="text-align: right;"></asp:TextBox>
                                    </td>
                                    <td class="style1">
                                        <asp:TextBox ID="txtUnit12" runat="server" Width="100px" ReadOnly="true"></asp:TextBox>
                                    </td>
                                    <td class="style1">
                                        <asp:DropDownList ID="ddlSellingUnit12" runat="server" Width="100px" OnSelectedIndexChanged="ddlSellingUnit12_SelectedIndexChanged" AutoPostBack="true"></asp:DropDownList>
                                        <asp:TextBox ID="txtPackSize12" runat="server" Width="100px" Visible="false"></asp:TextBox>
                                    </td>
                                    <td class="style1">
                                        <asp:TextBox ID="txtQty12" runat="server" Width="100px" AutoPostBack="True" OnTextChanged="txtQty12_TextChanged" Style="text-align: right;"></asp:TextBox>
                                        <asp:HiddenField ID="qty12" runat="server" Value="" />
                                    </td>
                                    <td class="style1">
                                        <asp:TextBox ID="txtUnitPrice12" runat="server" Width="100px" Style="text-align: right;" OnTextChanged="txtUnitPrice12_TextChanged" AutoPostBack="true"></asp:TextBox>
                                    </td>
                                    <td class="style1">
                                        <asp:TextBox ID="txtdiscAmt12" runat="server" Width="100px" Style="text-align: right;" OnDataBinding="txtdiscAmt12_DataBinding" AutoPostBack="true"></asp:TextBox>
                                    </td>
                                    <td class="style1">
                                        <asp:TextBox ID="txtTaxableAmt12" runat="server" Width="100px" Style="text-align: right;"></asp:TextBox>
                                    </td>
                                    <td class="style1">
                                        <asp:TextBox ID="txtHsnCode12" runat="server" Width="100px" ReadOnly="true"></asp:TextBox>
                                    </td>
                                    <td class="style1">
                                        <asp:TextBox ID="txtCgstRt12" runat="server" Width="100px" Style="text-align: right;" ReadOnly="true"></asp:TextBox>
                                    </td>
                                    <td class="style1">
                                        <asp:TextBox ID="txtCgstAmt12" runat="server" Width="100px" Style="text-align: right;" ReadOnly="true"></asp:TextBox>
                                    </td>
                                    <td class="style1">
                                        <asp:TextBox ID="txtSgstRt12" runat="server" Width="100px" Style="text-align: right;" ReadOnly="true"></asp:TextBox>
                                    </td>
                                    <td class="style1">
                                        <asp:TextBox ID="txtSgstAmt12" runat="server" Width="100px" Style="text-align: right;" ReadOnly="true"></asp:TextBox>
                                    </td>
                                    <td class="style1">
                                        <asp:TextBox ID="txtTotalPrice12" runat="server" Width="100px" Style="text-align: right;" ReadOnly="true"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="18"></td>
                                </tr>
                            </table>
                        </div>


                    </div>
                    <div  class="form-sec-row">
                        <table style="text-align: left;">
                            <tr>
                                <td style="width: 100px;">Gross Total</td>
                                <td style="width: 150px;"><asp:TextBox ID="txtgross" runat="server" Width="35%" Style="text-align: right;" Enabled="false"></asp:TextBox></td>
                                <td colspan="2"></td>
                            </tr>
                            <tr>
                                <td style="width: 150px;">Pushing Charges</td>
                                <td style="width: 150px;"><asp:TextBox ID="txtPushChrg" runat="server" Width="35%" Style="text-align: right;" OnTextChanged="txtPushChrg_TextChanged" AutoPostBack="true"></asp:TextBox></td>
                                <td colspan="2"></td>
                            </tr>
                            <tr>
                                <td style="width: 100%;" colspan="4">Extra Charges</td>
                            </tr>
                            
                            <tr>

                                <td style="width: 100px;">1. Description</td>
                                <td style="width: 500px;"><asp:TextBox ID="txtDesc1" runat="server" Width="99%" Style="text-align: left;" MaxLength="200"></asp:TextBox></td>
                                <td style="width: 100px;">Amount</td>
                                <td style="width: 150px;"><asp:TextBox ID="txtChrgAmt1" runat="server" Width="99%" Style="text-align: right;" OnTextChanged="txtChrgAmt1_TextChanged" AutoPostBack="true"></asp:TextBox></td>
                            </tr>
                            <tr>
                                <td style="width: 100px;">2. Description</td>
                                <td style="width: 500px;"><asp:TextBox ID="txtDesc2" runat="server" Width="99%" Style="text-align: left;" MaxLength="200"></asp:TextBox></td>
                                <td style="width: 100px;">Amount</td>
                                <td style="width: 150px;"><asp:TextBox ID="txtChrgAmt2" runat="server" Width="99%" Style="text-align: right;" OnTextChanged="txtChrgAmt2_TextChanged" AutoPostBack="true"></asp:TextBox></td>
                            </tr>
                            <tr>
                                <td style="width: 100px;">Round Off</td>
                                <td style="width: 150px;" ><asp:TextBox ID="txtRoundOff" runat="server" Width="35%" Style="text-align: right;" Enabled="false"></asp:TextBox></td>
                                <td style="width: 100px;">Net Amount</td>
                                <td style="width: 150px;"><asp:TextBox ID="txtNetAmt" runat="server" Width="99%" Style="text-align: right;" Enabled="false"></asp:TextBox></td>
                            </tr>
                        </table>
                    </div>
                    <div class="form-sec-row">
                        <table style="text-align: left;">
                            <tr>
                                <td style="width: 150px;">Cash</td>
                                <td style="width: 200px;">
                                    <asp:TextBox ID="txtCash" runat="server" Width="75%" Style="text-align: right;"></asp:TextBox></td>
                                <td style="width: 100px;">Card</td>
                                <td style="width: 195px;">
                                    <asp:TextBox ID="txtCard" runat="server" Width="75%" Style="text-align: right;"></asp:TextBox></td>
                                <td style="width: 100px;">e-Wallet</td>
                                <td style="width: 200px;">
                                    <asp:TextBox ID="txtewallet" runat="server" Width="75%" Style="text-align: right;"></asp:TextBox></td>
                                <td style="width: 100px;">Net Banking</td>
                                <td style="width: 200px;">
                                    <asp:TextBox ID="txtNetBank" runat="server" Width="75%" Style="text-align: right;"></asp:TextBox></td>
                            </tr>
                        </table>
                    </div>
                    <div class="form-sec-row" style="display: none;">
                        <label>
                            <strong>Discount :</strong></label>
                        <asp:TextBox ID="TextBox7" runat="server" CssClass="textbox-medium1"></asp:TextBox>
                        <div class="clear">
                        </div>
                    </div>

                    <div class="form-sec-row" style="display: none;">
                        <label>
                            <strong>Vat (%) :</strong></label>
                        <asp:TextBox ID="TextBox8" runat="server" CssClass="textbox-medium1"></asp:TextBox>
                        <div class="clear">
                        </div>
                    </div>

                    <%--     <div class="form-sec-row">
                    <label><strong>
                      Net Amount :</strong></label>
                    <asp:TextBox ID="TextBox3" runat="server" CssClass="textbox-medium1"></asp:TextBox>
                    <div class="clear">
                    </div>
                </div>--%>
                    <div class="form-sec-row">
                        &nbsp;&nbsp;&nbsp;<label><strong>&nbsp;</strong></label>
                        <asp:Button ID="Button1" runat="server" CssClass="submit-button" Text="Submit" Height="28px" OnClick="Button1_Click" />
                        <asp:HiddenField ID="hdnInsUpd" runat="server" Value="I" />
                        <asp:Button ID="Button2" runat="server" CssClass="submit-button" Text="Cancel" Height="28px"
                            OnClick="Button2_Click" />
                        <asp:Button ID="Button7" runat="server" CssClass="submit-button" Text="Print" Height="28px" OnClick="Button7_Click" />
                        <div class="clear">
                        </div>
                        <asp:HiddenField ID="hdnRowId1" runat="server" Value="0" />
                        <asp:HiddenField ID="hdnRowId2" runat="server" Value="0" />
                        <asp:HiddenField ID="hdnRowId3" runat="server" Value="0" />
                        <asp:HiddenField ID="hdnRowId4" runat="server" Value="0" />
                        <asp:HiddenField ID="hdnRowId5" runat="server" Value="0" />
                        <asp:HiddenField ID="hdnRowId6" runat="server" Value="0" />
                        <asp:HiddenField ID="hdnRowId7" runat="server" Value="0" />
                        <asp:HiddenField ID="hdnRowId8" runat="server" Value="0" />
                        <asp:HiddenField ID="hdnRowId9" runat="server" Value="0" />
                        <asp:HiddenField ID="hdnRowId10" runat="server" Value="0" />
                        <asp:HiddenField ID="hdnRowId11" runat="server" Value="0" />
                        <asp:HiddenField ID="hdnRowId12" runat="server" Value="0" />

                    </div>
                    <div class="clear">
                    </div>
                    <center>
                        <table width="100%">
                            <tr>
                                <td>
                                    <div id='mydiv'>
                                        <asp:Literal ID="ltrReport" runat="server"></asp:Literal>
                                        <div id="divcancel"   align="center"  style="position:absolute;top:150px;width:100%;visibility:hidden" ><img src="../Images/cancel.png" alt=""/></div>
                                    </div>
                                </td>
                            </tr>
                            <tr>
                                <td align="center" align="center" style="margin-left: 30%;">
                                    <input type="button" value="Back" style="width: 70px; font-size: x-small; margin-left: 100px;" onclick="window.history.back()" />
                                    <input type="button" id="cmdPrint" value="Print" style="width: 70px; font-size: x-small" onclick="javascript: printDiv('mydiv')" /></td>
                            </tr>
                        </table>
                    </center>
                </div>
            </div>
            </div>
            </strong></strong>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>


