<%@ Page Title="" Language="C#" MasterPageFile="~/MasterAll/MasterPageAll.master" AutoEventWireup="true" CodeFile="CarMaster.aspx.cs" Inherits="IPD_CarMaster" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<script runat="server">

   
</script>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">


    <script language="javascript" type="text/javascript">
        function DisableBackButton() {
            window.history.forward()
        }
        DisableBackButton();
        window.onload = DisableBackButton;
        window.onpageshow = function (evt) { if (evt.persisted) DisableBackButton() }
        window.onunload = function () { void (0) }




        function Calling() {
            //        $('#page_effect').fadeIn(2000);
            $("input[id$='txtName']").focus(function () {
                $("input[id$='txtName']").addClass('textboxborder');
            });
            $("input[id$='txtName']").blur(function () {
                $("input[id$='txtName']").removeClass('textboxborder');
            });


            $("input[id$='Button1']").click(function () {
                var e = document.getElementById("ctl00_ContentPlaceHolder1_DropDownList2");
                var strUser = e.options[e.selectedIndex].text;

                if (strUser == '--Select--') {
                    alert('Select Car Type');
                    $(e).focus();
                    $(e).addClass('textboxerr');
                    return false;
                }
                else {
                    $(e).removeClass('textboxerr');
                }
                if ($("input[id$='txtName']").val() == '') {
                    alert('Name Can not be Blank!');
                    $("input[id$='txtName']").focus();
                    $("input[id$='txtName']").addClass('textboxerr');
                    return false;
                }
                else {
                    $("input[id$='txtName']").removeClass('textboxerr');
                }
                if ($("input[id$='txtAddress']").val() == '') {
                    alert('Address Can not be Blank!');
                    $("input[id$='txtAddress']").focus();
                    $("input[id$='txtAddress']").addClass('textboxerr');
                    return false;
                }
                else {
                    $("input[id$='txtAddress']").removeClass('textboxerr');
                }
                var e = document.getElementById("ctl00_ContentPlaceHolder1_DropDownList1");
                var strUser = e.options[e.selectedIndex].text;

                if (strUser == '--Select--') {
                    alert('Select District');
                    $(e).focus();
                    $(e).addClass('textboxerr');
                    return false;
                }
                else {
                    $(e).removeClass('textboxerr');
                }
                if ($("input[id$='txtPinNo']").val() == '') {
                    alert('Please Enter Pin No!');
                    $("input[id$='txtPinNo']").focus();
                    $("input[id$='txtPinNo']").addClass('textboxerr');
                    return false;
                }
                else {
                    $("input[id$='txtPinNo']").removeClass('textboxerr');
                }
                if ($("input[id$='txtPhNo']").val() == '') {
                    alert('Please Enter Phone No !');
                    $("input[id$='txtPhNo']").focus();
                    $("input[id$='txtPhNo']").addClass('textboxerr');
                    return false;
                }
                else {
                    $("input[id$='txtPhNo']").removeClass('textboxerr');
                }

            });

            $("input[id$='txtPinNo']").keydown(function (event) {
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


            $("input[id$='txtPhNo']").keydown(function (event) {
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
            //document.getElementById("ctl00_ContentPlaceHolder1_TextBoxicode").value = regname[1];
            document.getElementById("ctl00_ContentPlaceHolder1_txtName").value = regname[0];
            //document.getElementById("ctl00_ContentPlaceHolder1_txtPinNo").value = regname[3];
            $("#txtName").focus();
            //$("#txtPinNo").focus();
            //$("#DropDownList4").val(regname[1]);
        }

    </script>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <asp:UpdateProgress ID="UpdateProgress1" runat="server">
        <ProgressTemplate>

            <div id="aaa" class="progressBackgroundFilter"></div>
            <div id="bbb" class="processMessage">
                <img alt="Loading" src="../images/pwait.gif" />

            </div>
        </ProgressTemplate>
    </asp:UpdateProgress>

    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <div id="page_effect">
                <div class="pageheader">
                    <asp:Label ID="Label1" runat="server">Car Master</asp:Label>
                </div>
                <table width="290px" cellpadding="0" cellspacing="0">
                    <tr>
                        <td>
                            <asp:Button Text="Entry" BorderStyle="None" ID="Tab1" Width="145px" Height="40px" CssClass="Initial" runat="server" OnClick="Tab1_Click" /></td>
                        <td>
                            <asp:Button Text="List" BorderStyle="None" ID="Tab2" CssClass="Initial" Width="145px" Height="40px" runat="server" OnClick="Tab2_Click" /></td>

                    </tr>
                </table>
                <div class="formbox">
                    <asp:MultiView ID="MainView" runat="server">
                        <asp:View ID="View1" runat="server">
                            <div class="form-sec">
                                <div class="error">
                                    <strong>
                                        <asp:Label ID="lblError" runat="server" Font-Bold="True" CssClass="fadeout"></asp:Label>
                                    </strong>
                                    <div class="clear">
                                    </div>
                                </div>
                                <asp:HiddenField ID="HiddenField1" runat="server" />
                                <div class="form-sec-row">
                                    <label>
                                        <strong>Cartype :</strong></label>
                                    <asp:DropDownList ID="DropDownList2" runat="server" CssClass="textbox-medium1">
                                        <%--<asp:ListItem Value="Car Rented">Car Rented</asp:ListItem>
                             <asp:ListItem Value="Car Private">Car Private</asp:ListItem>
                             <asp:ListItem Value="Ambulance">Ambulance</asp:ListItem>
                             <asp:ListItem Selected="True">Select</asp:ListItem>--%>
                                    </asp:DropDownList>
                                    <div class="clear">
                                    </div>
                                </div>
                                <div class="form-sec-row">
                                    <label>
                                        <strong>Name :</strong></label>
                                    <asp:TextBox ID="txtName" runat="server" CssClass="textbox-medium1"></asp:TextBox>
                                    <cc1:AutoCompleteExtender ServiceMethod="SearchWing" OnClientItemSelected="autoCompleteEx_ItemSelected" MinimumPrefixLength="1"
                                        CompletionInterval="100" EnableCaching="false" CompletionSetCount="10" TargetControlID="txtName" ID="AutoCompleteExtender2" runat="server"
                                        FirstRowSelected="false">
                                    </cc1:AutoCompleteExtender>
                                    <div class="clear">
                                    </div>
                                </div>
                                <div class="form-sec-row">
                                    <label>
                                        <strong>Address:</strong></label>
                                    <asp:TextBox ID="txtAddress" runat="server" CssClass="textbox-medium1"></asp:TextBox>
                                    <div class="clear">
                                    </div>
                                </div>
                                <div class="form-sec-row">
                                    <label>
                                        <strong>District :</strong></label>
                                    <asp:DropDownList ID="DropDownList1" runat="server" CssClass="textbox-medium1">
                                    </asp:DropDownList>
                                    <div class="clear">
                                    </div>
                                </div>
                                <div class="form-sec-row">
                                    <label>
                                        <strong>Pin No</strong>
                                    </label>
                                    <asp:TextBox ID="txtPinNo" runat="server" MaxLength="6" CssClass="textbox-medium1"></asp:TextBox>
                                    <asp:RegularExpressionValidator ID="RegularExpressionValidator2" runat="server"
                                        ControlToValidate="txtPinNo" ErrorMessage="Please enter 6 digit Pin No correctly"
                                       ValidationExpression="\d{6}"></asp:RegularExpressionValidator>
                                    <div class="clear">
                                    </div>
                                </div>
                                <div class="form-sec-row">
                                    <label>
                                        <strong>Ph No</strong>
                                    </label>
                                    <asp:TextBox ID="txtPhNo" runat="server" MaxLength="10" CssClass="textbox-medium1"></asp:TextBox>
                                    <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server"
                                        ControlToValidate="txtPhNo" ErrorMessage="Please enter Phone No correctly"
                                        ValidationExpression="[0-9]{10}"></asp:RegularExpressionValidator>


                                    <div class="clear">
                                    </div>
                                </div>
                                <div class="form-sec-row">
                                    <div class="clear">
                                    </div>
                                </div>
                                <div class="form-sec-row">
                                    &nbsp;&nbsp;&nbsp;<label><strong>&nbsp;</strong></label>
                                    <asp:Button ID="Button1" runat="server" CssClass="submit-button" Height="28px"
                                        Text="Submit" OnClick="Button1_Click" />
                                    <asp:Button ID="Button2" runat="server" CssClass="submit-button" Height="28px"
                                        Text="Cancel" OnClick="Button2_Click" />
                                    <div class="clear">
                                    </div>
                                </div>
                                <div class="clear">
                                </div>
                            </div>
                        </asp:View>
                        <asp:View ID="View2" runat="server">
                            <div style='width: 100%;'>
                                <table width="100%" style='background-color: #FB7B13; color: #FFF;'>
                                    <tr style='border: 1px solid black;'>
                                        <td style='width: 100px;' align="center">Name</td>
                                        <td style='width: 100px;' align="center">Address</td>
                                        <td style='width: 100px;' align="center">District</td>
                                        <td style='width: 100px;' align="center">Phone No</td>
                                        <td style='width: 70px;' align="center">Edit</td>
                                        <td style='width: 70px;' align="center" id="coldel" runat="server">Delete</td>
                                    </tr>
                                </table>
                            </div>
                            <div class="listing" style='width: 100%; height: 500px; overflow: auto;'>

                                <asp:GridView ID="GridView1" Width="978px" CssClass="grid" PagerStyle-CssClass="pgr"
                                    DataKeyNames="Name" runat="server"
                                    AutoGenerateColumns="False" AllowPaging="True" ShowHeader="False"
                                    OnRowCommand="GridView1_RowCommand" SelectedRowStyle-BackColor="GreenYellow"
                                    OnPageIndexChanging="GridView1_PageIndexChanging"  OnRowDataBound="GridView1_RowDataBound"
                                    OnRowDeleting="GridView1_RowDeleting">
                                    <RowStyle HorizontalAlign="Center" />
                                    <Columns>
                                        <asp:TemplateField HeaderText="CarType" Visible="false">
                                            <ItemTemplate>
                                                <asp:Label ID="CarType" runat="server" Text='<%# Bind("CarType") %>'></asp:Label>
                                                <asp:Label ID="lblId" runat="server" Visible="false" Text='<%# Bind("RowId") %>'></asp:Label>

                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Name" ItemStyle-Width="100px">
                                            <ItemTemplate>
                                                <asp:Label ID="Name" runat="server" Text='<%# Bind("Name") %>'></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle Width="100px" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Address" Visible="true">
                                            <ItemTemplate>
                                                <asp:Label ID="Address" runat="server" Text='<%# Bind("Address1") %>'></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle Width="100px" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="District" ItemStyle-Width="100px">
                                            <ItemTemplate>
                                                <asp:Label ID="District" runat="server" Visible="false" Text='<%# Bind("District") %>'></asp:Label>
                                                <asp:Label ID="DistrictName" runat="server" Text='<%# Bind("DistrictName") %>'></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle Width="100px" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Pin No" ItemStyle-Width="100px" Visible="false">
                                            <ItemTemplate>
                                                <asp:Label ID="PinNo" runat="server" Text='<%# Bind("Pin") %>'></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle Width="100px" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Phone No" ItemStyle-Width="100px">
                                            <ItemTemplate>
                                                <asp:Label ID="PhoneNo" runat="server" Text='<%# Bind("PhNo1") %>'></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle Width="100px" />
                                        </asp:TemplateField>
                                        <asp:CommandField SelectText="Edit" ShowSelectButton="True" ItemStyle-Width="70px" ControlStyle-CssClass="temp" HeaderText="Edit" SelectImageUrl="~/Images/edit.png" ButtonType="Image">
                                            <ControlStyle CssClass="temp"></ControlStyle>
                                            <ItemStyle Width="70px" />
                                        </asp:CommandField>
                                        <asp:CommandField ShowDeleteButton="True" ControlStyle-CssClass="temp" ItemStyle-Width="70px" HeaderText="Delete" DeleteImageUrl="~/Images/delete.png" ButtonType="Image">
                                            <ControlStyle CssClass="temp"></ControlStyle>
                                            <ItemStyle Width="70px" />
                                        </asp:CommandField>
                                    </Columns>
                                    <PagerStyle CssClass="pgr" />
                                    <EditRowStyle BackColor="#CCFF33" />
                                    <AlternatingRowStyle BackColor="#FFDB91" />
                                    <SelectedRowStyle BackColor="GreenYellow" />
                                </asp:GridView>
                            </div>
                        </asp:View>
                    </asp:MultiView>
                </div>
            </div>

        </ContentTemplate>
    </asp:UpdatePanel>

</asp:Content>

