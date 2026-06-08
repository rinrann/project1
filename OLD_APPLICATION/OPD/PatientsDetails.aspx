<%@ Page Title="" Language="C#" MasterPageFile="~/MasterAll/MasterPageAll.master" AutoEventWireup="true" CodeFile="PatientsDetails.aspx.cs" Inherits="OPD_PatientsDetails" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <script type="text/javascript" language="javascript">
        function printDiv(divID) {
            var divElements = document.getElementById(divID).innerHTML;
            var oldPage = document.body.innerHTML;
            document.body.innerHTML = "<html><head><title></title></head><body>" + divElements + "</body>";
            window.print();
            document.body.innerHTML = oldPage;
        }

        function ConfirmationMessage() {
            var data = confirm("Are you sure you want to submit the form.");
            if (data) {
                return true;
            } else {
                return false;
            }

        }
    </script>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <script type="text/javascript" language="javascript">
        function ShowDialog() {

            var rtvalue = window.open("PtientDetailspopup.aspx", "sss", "Width:800px; Height:550px; dialogLeft:250px;");
            //var rtvalue = window.open("PtientAppointmentpopup.aspx", "sss", "Width:800px; Height:550px; dialogLeft:250px;");
            //document.getElementById("ctl00_ContentPlaceHolder1_TextBox2").value = rtvalue.NameValue;

        }

        function ShowDialog1() {
            var str = document.getElementById("ctl00_ContentPlaceHolder1_TextBox2").value;
            var rtvalue = window.open("USGPopup.aspx?Regno=" + str, "sss", "Width:800px; Height:550px; dialogLeft:250px;");

        }


        function Calling() {

            var date = new Date();
            $("input[id$='txtdate']").datepicker({ dateFormat: 'dd/mm/yy' });



            var date = new Date();
            $('.DatepickerReCall').datepicker({ dateFormat: 'dd/mm/yy' });



            $("input[id$='TextBox18']").timepicker({
                showPeriod: true,
                showLeadingZero: true
            });




            $("input[id$='Button4']").click(function () {
                if ($("input[id$='txtreg']").val() == '') {
                    alert('Please Select A Patient Registration No !');
                    $("input[id$='txtreg']").focus();
                    $("input[id$='txtreg']").addClass('textboxerr');
                    return false;
                }
                else {
                    $("input[id$='txtreg']").removeClass('textboxerr');
                }
            });


            $("input[id$='Tab2']").click(function () {
                if ($("input[id$='txtreg']").val() == '') {
                    alert('Please Select A Patient Registration No !');
                    $("input[id$='txtreg']").focus();
                    $("input[id$='txtreg']").addClass('textboxerr');
                    return false;
                }
                else {
                    $("input[id$='txtreg']").removeClass('textboxerr');
                }
            });


            $("input[id$='Tab7']").click(function () {
                if ($("input[id$='txtreg']").val() == '') {
                    alert('Please Select A Patient Registration No !');
                    $("input[id$='txtreg']").focus();
                    $("input[id$='txtreg']").addClass('textboxerr');
                    return false;
                }
                else {
                    $("input[id$='txtreg']").removeClass('textboxerr');
                }
            });


            $("input[id$='Tab8']").click(function () {
                if ($("input[id$='txtreg']").val() == '') {
                    alert('Please Select A Patient Registration No !');
                    $("input[id$='txtreg']").focus();
                    $("input[id$='txtreg']").addClass('textboxerr');
                    return false;
                }
                else {
                    $("input[id$='txtreg']").removeClass('textboxerr');
                }
            });


            $("input[id$='Tab3']").click(function () {
                if ($("input[id$='txtreg']").val() == '') {
                    alert('Please Select A Patient Registration No !');
                    $("input[id$='txtreg']").focus();
                    $("input[id$='txtreg']").addClass('textboxerr');
                    return false;
                }
                else {
                    $("input[id$='txtreg']").removeClass('textboxerr');
                }
            });


            $("input[id$='Tab4']").click(function () {
                if ($("input[id$='txtreg']").val() == '') {
                    alert('Please Select A Patient Registration No !');
                    $("input[id$='txtreg']").focus();
                    $("input[id$='txtreg']").addClass('textboxerr');
                    return false;
                }
                else {
                    $("input[id$='txtreg']").removeClass('textboxerr');
                }
            });


            $("input[id$='Tab5']").click(function () {
                if ($("input[id$='txtreg']").val() == '') {
                    alert('Please Select A Patient Registration No !');
                    $("input[id$='txtreg']").focus();
                    $("input[id$='txtreg']").addClass('textboxerr');
                    return false;
                }
                else {
                    $("input[id$='txtreg']").removeClass('textboxerr');
                }
            });


            $("input[id$='Tab6']").click(function () {
                if ($("input[id$='txtreg']").val() == '') {
                    alert('Please Select A Patient Registration No !');
                    $("input[id$='txtreg']").focus();
                    $("input[id$='txtreg']").addClass('textboxerr');
                    return false;
                }
                else {
                    $("input[id$='txtreg']").removeClass('textboxerr');
                }
            });


            $("input[id$='TextBox8']").keydown(function (event) {
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

            $("input[id$='TextBox5']").keydown(function (event) {
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

            $("input[id$='TextBox6']").keydown(function (event) {
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

            $("input[id$='TextBox7']").keydown(function (event) {
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
    </script>


    <%--For Busy Loader .............................--%>



    <asp:UpdateProgress ID="UpdateProgress1" runat="server">
        <ProgressTemplate>

            <div id="aaa" class="progressBackgroundFilter"></div>
            <div id="bbb" class="processMessage">
                <img alt="Loading" src="../images/pwait.gif" />

            </div>
        </ProgressTemplate>
    </asp:UpdateProgress>


    <%--For Busy Loader End.............................--%>

    <asp:UpdatePanel ID="UpdatePanel1" runat="server">

        <ContentTemplate>

            <div class="pageheader">
                <asp:Label ID="Label1" runat="server">Patient Details </asp:Label>
            </div>

            <div class="formbox">
                <div class="form-sec">

                    <div class="error">
                        <strong>
                            <asp:Label ID="lblError" runat="server" Font-Bold="True"></asp:Label>
                        </strong>
                        <div class="clear"></div>
                    </div>
                    <asp:HiddenField ID="HiddenField1" runat="server" />
                    <div class="form-sec-row">
                        <label><strong>Registration No :</strong></label>
                        <asp:TextBox ID="txtreg" runat="server" CssClass="textbox-medium1" ReadOnly="true" ClientIDMode="Static"></asp:TextBox>
                        <asp:Button ID="Button3" runat="server" Text="Search" CssClass="submit-button" Height="28px" OnClientClick="ShowDialog()" />
                        <asp:Button ID="Button4" runat="server" Text="fetch" CssClass="submit-button" Height="28px" OnClick="Button4_Click" />
                        <div class="clear"></div>
                    </div>

                    <div class="form-sec-row">
                        <label><strong>Appointment No :</strong></label>
                        <asp:TextBox ID="TextBox1" runat="server" CssClass="textbox-medium1"></asp:TextBox>
                        <div class="clear"></div>
                    </div>
                    <div class="form-sec-row">
                        <label><strong>Patient Checked :</strong></label>
                        <asp:CheckBox ID="Chk" runat="server" />&nbsp;&nbsp;&nbsp;&nbsp;
                      <div class="clear"></div>
                    </div>
                    <div class="form-sec-row">
                        <label><strong>Checked Date :</strong></label>
                        <asp:TextBox ID="txtCheckedDate" runat="server" CssClass="textbox-medium1"></asp:TextBox>
                        <cc1:MaskedEditExtender ID="MaskedEditExtender3" runat="server" TargetControlID="txtCheckedDate"
                        Mask="99/99/9999" MessageValidatorTip="true" MaskType="Date" DisplayMoney="Left"
                        AcceptNegative="Left" ErrorTooltipEnabled="True" CultureName="en-GB" />
                            <asp:Label ID="Label17" runat="server" ForeColor="Green" Text="(DD/MM/YYYY)"></asp:Label>
                        <div class="clear"></div>
                    </div>
                    <div class="form-sec-row">
                        <asp:Button ID="ButChk" runat="server" Text="Save" CssClass="submit-button" Height="28px" OnClick="ButChk_Click" Style="margin-left: 2px" />
                        <div class="clear"></div>
                    </div>
                </div>
            </div>
            <div class="formbox">
                <table cellpadding="0" width="100%" cellspacing="2">
                    <tr>
                        <td colspan="12">
                            <br />
                            <br />
                        </td>
                    </tr>
                    <tr>
                        <td colspan="2" style='background-color: #93B4EB;'><strong>Prescription Id</strong></td>
                        <td colspan="10">
                            <asp:TextBox ID="txtprescripId" style="border:none;" Enabled="False" runat="server"></asp:TextBox>
                            
                        </td>
                    </tr>
                    <tr>
                        <td style='background-color: #93B4EB;'><strong>NAME</strong></td>
                        <td>
                            <asp:TextBox ID="TextBox3" Width="200px" runat="server" Enabled="False"></asp:TextBox>
                        </td>
                        <td style='background-color: #93B4EB;'><strong>AGE</strong> </td>
                        <td>
                            <asp:TextBox ID="TextBox4" Width="55px" runat="server" Enabled="False"></asp:TextBox>
                        </td>

                        <td style='background-color: #93B4EB;'><strong>G</strong></td>
                        <td>
                            <asp:TextBox ID="TextBox5" Width="55px" runat="server" MaxLength="2"></asp:TextBox>
                        </td>
                        <td align="center" style='background-color: #93B4EB;'><strong>P</strong></td>
                        <td>
                            <asp:TextBox ID="TextBox6" Width="55px" runat="server" MaxLength="2"></asp:TextBox>
                        </td>
                        <td align="center" style='background-color: #93B4EB; width: 50px;'><strong>A</strong></td>
                        <td>
                            <asp:TextBox ID="TextBox7" Width="55px" runat="server" MaxLength="2"></asp:TextBox>
                        </td>
                        <td style='background-color: #93B4EB;'><strong>LIVE</strong></td>
                        <td>
                            <asp:TextBox ID="TextBox8" Width="55px" runat="server" MaxLength="2"></asp:TextBox>
                        </td>

                    </tr>
                    <tr>
                        <td style='background-color: #93B4EB;'><strong>LMP</strong></td>
                        <td>
                            <asp:TextBox ID="TextBox9" runat="server" CssClass="DatepickerReCall" Width="120px"></asp:TextBox>
                        </td>
                        <td style='background-color: #93B4EB;'><strong>EDD</strong></td>
                        <td>
                            <asp:TextBox ID="TextBox10" Width="95px" CssClass="DatepickerReCall" runat="server"></asp:TextBox>
                        </td>
                        <td style='background-color: #93B4EB;'><strong>LCB</strong></td>
                        <td>
                            <asp:TextBox ID="TextBox11" Width="100px" runat="server"></asp:TextBox>
                        </td>
                        <td style='background-color: #93B4EB;'><strong>COMMENT</strong></td>
                        <td colspan="5">
                            <asp:TextBox ID="TextBox12" Width="320px" runat="server"></asp:TextBox>
                        </td>
                    </tr>
                </table>
                <div class="form-sec-row">
                    <label><strong>&nbsp;</strong></label>
                    <asp:Button ID="Button5" runat="server" Text="Submit" Height="28px" CssClass="submit-button"
                        OnClick="Button5_Click" />
                    <asp:Button ID="Button6" runat="server" Height="28px" Text="Cancel" CssClass="submit-button"
                        OnClick="Button6_Click" />
                    <div class="clear"></div>
                </div>
            </div>


            <div class="formbox">
                <table width="100%" cellpadding="0" cellspacing="0">

                    <tr>
                        <td>
                            <asp:Button Text="Personal Info" BorderStyle="None" ID="Tab1" Width="145px" Height="40px" CssClass="Initial" runat="server" OnClick="Tab1_Click" />

                        </td>
                        <td>
                            <asp:Button Text="History" BorderStyle="None" ID="Tab2" CssClass="Initial" Width="110px" Height="40px" runat="server" OnClick="Tab2_Click" />

                        </td>
                        <td>
                            <asp:Button Text="Clinical Finding" BorderStyle="None" ID="Tab5" CssClass="Initial" Width="120px" Height="40px" runat="server" OnClick="Tab5_Click" />

                        </td>
                        <td>
                            <asp:Button Text="Diagnosis" BorderStyle="None" ID="Tab9" CssClass="Initial" Width="120px" Height="40px" runat="server" OnClick="Tab9_Click" />
                        </td>
                        <td>
                            <asp:Button Text="Investigation" BorderStyle="None" ID="Tab4" Width="120px" Height="40px" CssClass="Initial" runat="server" OnClick="Tab4_Click" />

                        </td>
                        <td>
                            <asp:Button Text="Medicine" BorderStyle="None" ID="Tab6" Width="120px" Height="40px" CssClass="Initial" runat="server" OnClick="Tab6_Click" />

                        </td>
                        
                        <td>
                            <asp:Button Text="Vaccination" BorderStyle="None" ID="Tab3" CssClass="Initial" runat="server" Width="125px" Height="40px" OnClick="Tab3_Click" />

                        </td>
                        
                        
                        
                        <td>
                            <asp:Button Text="USG" BorderStyle="None" ID="Tab7" Width="100px" Height="40px" CssClass="Initial" runat="server" OnClick="Tab7_Click" />

                        </td>
                        <td>
                            <asp:Button Text="Hospital Note" BorderStyle="None" ID="Tab8" Width="120px" Height="40px" CssClass="Initial" runat="server" OnClick="Tab8_Click" />

                        </td>
                    </tr>
                </table>

                <asp:MultiView ID="MainView" runat="server">
                    <asp:View ID="View1" runat="server">


                        <table style="width: 100%; border-width: 1px; border-color: #666; border-style: solid">
                            <tr>
                                <td colspan="4">
                                    <div class="pageheader">
                                        <asp:Label ID="Label4" runat="server"> Patient Info Details </asp:Label>
                                    </div>
                                    <br />
                                </td>
                            </tr>
                            <tr>
                                <td><strong>C/O :</strong></td>
                                <td>
                                    <asp:TextBox ID="TextBox13" runat="server" Width="150px"></asp:TextBox></td>
                                <td><strong>Contact No :</strong> </td>
                                <td>
                                    <asp:TextBox ID="TextBox19" runat="server" Text="+91" Width="30px"></asp:TextBox>
                                    <asp:TextBox ID="TextBox14" runat="server" Width="120px"></asp:TextBox></td>
                            </tr>

                            <tr>
                                <td rowspan="3"><strong>Address :</strong> </td>
                                <td rowspan="3">
                                    <asp:TextBox ID="TextBox15" runat="server" TextMode="MultiLine" Height="48px" Width="220px"></asp:TextBox>
                                </td>
                                <td><strong>District :</strong> </td>
                                <td>
                                    <asp:TextBox ID="TextBox16" runat="server" Width="150px"></asp:TextBox></td>
                            </tr>

                            <tr>
                                <td><strong>Appointment Date :</strong> </td>
                                <td>
                                    <asp:TextBox ID="TextBox17" runat="server" CssClass="DatepickerReCall" Width="150px"></asp:TextBox>
                                    <cc1:MaskedEditExtender ID="MaskedEditExtender1" runat="server" TargetControlID="TextBox17"
                                        Mask="99/99/9999" MessageValidatorTip="true" MaskType="Date" DisplayMoney="Left"
                                        AcceptNegative="Left" ErrorTooltipEnabled="True" CultureName="en-GB" />
                                </td>
                            </tr>

                            <tr>

                                <td><strong>Appointment Time :</strong> </td>
                                <td>
                                    <asp:TextBox ID="TextBox18" runat="server" Width="150px"></asp:TextBox></td>
                            </tr>
                            <tr>
                                <td colspan="4">
                                    <br />
                                    <div class="form-sec-row">
                                        <label><strong>&nbsp;</strong></label>
                                        <asp:Button ID="Button7" runat="server" Text="Submit" CssClass="submit-button"
                                            Height="28px" OnClick="Button7_Click" />
                                        <asp:Button ID="Button8" runat="server" Text="Cancel" CssClass="submit-button"
                                            Height="28px" OnClick="Button8_Click" />
                                        <div class="clear"></div>
                                    </div>
                                </td>
                            </tr>
                        </table>


                    </asp:View>
                    <asp:View ID="View2" runat="server">
                        <table border="1" cellpadding="0" cellspacing="0" width="100%">
                            <tr>
                                <td colspan="5">
                                    <%--  Patient History   ....   --%>

                                    <div class="pageheader">
                                        <asp:Label ID="Label2" runat="server"> Patient Previous History </asp:Label>
                                    </div>
                                    <div class="listing" style='width: 100%; height: 200px; overflow: auto;'>
                                        <asp:GridView ID="GridView1" CssClass="grid" PagerStyle-CssClass="pgr" DataKeyNames="RowID"
                                            runat="server" AutoGenerateColumns="False"
                                            AllowPaging="True" PageSize="70" Width="100%"
                                            OnPageIndexChanging="GridView1_PageIndexChanging"
                                            SelectedRowStyle-BackColor="GreenYellow" OnRowCancelingEdit="GridView1_RowCancelingEdit"
                                            OnRowDeleting="GridView1_RowDeleting" OnRowEditing="GridView1_RowEditing"
                                            OnRowUpdating="GridView1_RowUpdating">
                                            <RowStyle HorizontalAlign="Center" />
                                            <Columns>

                                                <asp:TemplateField HeaderText="ID" Visible="false">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblId" runat="server" Text='<%# Bind("RowID") %>'></asp:Label>
                                                    </ItemTemplate>

                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="Date">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lbldate" runat="server" Text='<%# Bind("Date1") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <EditItemTemplate>
                                                        <asp:TextBox ID="txtdate" runat="server" Text='<%# Bind("Date1") %>'></asp:TextBox>
                                                    </EditItemTemplate>
                                                </asp:TemplateField>


                                                <asp:TemplateField HeaderText="Mens">

                                                    <ItemTemplate>
                                                        <asp:Label ID="lblname" runat="server" Text='<%# Bind("Mens") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <EditItemTemplate>
                                                        <asp:TextBox ID="txtmens" runat="server" Text='<%# Bind("Mens") %>'></asp:TextBox>
                                                    </EditItemTemplate>
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="Operation Details">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblage" runat="server" Text='<%# Bind("OperationDtls") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <EditItemTemplate>
                                                        <asp:TextBox ID="txtopdtl" runat="server" Text='<%# Bind("OperationDtls") %>'></asp:TextBox>
                                                    </EditItemTemplate>
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="Special">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblsex" runat="server" Text='<%# Bind("Special") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <EditItemTemplate>
                                                        <asp:TextBox ID="txtspecial" runat="server" Text='<%# Bind("Special") %>'></asp:TextBox>
                                                    </EditItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Others">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblphone1" runat="server" Text='<%# Bind("Others") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <EditItemTemplate>
                                                        <asp:TextBox ID="txtothers" runat="server" Text='<%# Bind("Others") %>'></asp:TextBox>
                                                    </EditItemTemplate>
                                                </asp:TemplateField>

                                                <asp:CommandField ShowEditButton="True" ControlStyle-CssClass="temp" HeaderText="Edit" />
                                                <asp:CommandField ShowDeleteButton="True" HeaderText="Delete" />
                                            </Columns>
                                            <PagerStyle CssClass="pgr" />
                                            <EditRowStyle BackColor="#CCFF33" />
                                            <AlternatingRowStyle BackColor="#FFDB91" />
                                            <SelectedRowStyle BackColor="GreenYellow" />
                                        </asp:GridView>
                                    </div>


                                    <br />
                                    <br />
                                </td>
                            </tr>
                            <tr>
                                <td colspan="5">

                                    <div class="pageheader">
                                        <asp:Label ID="Label3" runat="server"> Patient New History </asp:Label>
                                    </div>

                                </td>
                            </tr>

                            <tr style='background-color: #FF9300;'>
                                <td align="center">
                                    <div class="form-sec-row">
                                        <label class="lname"><strong>Date</strong></label>
                                    </div>
                                </td>

                                <td align="center">
                                    <div class="form-sec-row">
                                        <label class="lname"><strong>Mens</strong></label>
                                    </div>
                                </td>
                                <td align="center">
                                    <div class="form-sec-row">
                                        <label class="lname"><strong>Operation Details </strong></label>
                                    </div>
                                </td>
                                <td align="center">
                                    <div class="form-sec-row">
                                        <label class="lname"><strong>Special</strong></label>
                                    </div>

                                </td>
                                <td align="center">
                                    <div class="form-sec-row">
                                        <label class="lname"><strong>Others</strong></label>
                                    </div>

                                </td>


                            </tr>
                            <tr>
                                <td align="center">

                                    <asp:TextBox ID="TextBox21" runat="server" CssClass="DatepickerReCall" Width="150px"></asp:TextBox>


                                </td>

                                <td align="center">

                                    <asp:TextBox ID="TextBox22" runat="server" Width="150px"></asp:TextBox>

                                </td>
                                <td align="center">

                                    <asp:TextBox ID="TextBox23" runat="server" Width="150px"></asp:TextBox>

                                </td>
                                <td align="center">

                                    <asp:TextBox ID="TextBox24" runat="server" Width="150px"></asp:TextBox>


                                </td>
                                <td align="center">

                                    <asp:TextBox ID="TextBox25" runat="server" Width="150px"></asp:TextBox>


                                </td>
                            </tr>

                            <tr>
                                <td align="center">

                                    <asp:TextBox ID="TextBox26" runat="server" CssClass="DatepickerReCall" Width="150px"></asp:TextBox>


                                </td>

                                <td align="center">

                                    <asp:TextBox ID="TextBox27" runat="server" Width="150px"></asp:TextBox>

                                </td>
                                <td align="center">

                                    <asp:TextBox ID="TextBox28" runat="server" Width="150px"></asp:TextBox>

                                </td>
                                <td align="center">

                                    <asp:TextBox ID="TextBox29" runat="server" Width="150px"></asp:TextBox>


                                </td>
                                <td align="center">

                                    <asp:TextBox ID="TextBox30" runat="server" Width="150px"></asp:TextBox>


                                </td>
                            </tr>

                            <tr>
                                <td align="center">

                                    <asp:TextBox ID="TextBox31" runat="server" CssClass="DatepickerReCall" Width="150px"></asp:TextBox>


                                </td>

                                <td align="center">

                                    <asp:TextBox ID="TextBox32" runat="server" Width="150px"></asp:TextBox>

                                </td>
                                <td align="center">

                                    <asp:TextBox ID="TextBox33" runat="server" Width="150px"></asp:TextBox>

                                </td>
                                <td align="center">

                                    <asp:TextBox ID="TextBox34" runat="server" Width="150px"></asp:TextBox>


                                </td>
                                <td align="center">

                                    <asp:TextBox ID="TextBox35" runat="server" Width="150px"></asp:TextBox>


                                </td>
                            </tr>

                            <tr>
                                <td align="center">

                                    <asp:TextBox ID="TextBox36" CssClass="DatepickerReCall" runat="server" Width="150px"></asp:TextBox>


                                </td>

                                <td align="center">

                                    <asp:TextBox ID="TextBox37" runat="server" Width="150px"></asp:TextBox>

                                </td>
                                <td align="center">

                                    <asp:TextBox ID="TextBox38" runat="server" Width="150px"></asp:TextBox>

                                </td>
                                <td align="center">

                                    <asp:TextBox ID="TextBox39" runat="server" Width="150px"></asp:TextBox>


                                </td>
                                <td align="center">

                                    <asp:TextBox ID="TextBox40" runat="server" Width="150px"></asp:TextBox>


                                </td>
                            </tr>

                            <tr>
                                <td align="center">

                                    <asp:TextBox ID="TextBox41" CssClass="DatepickerReCall" runat="server" Width="150px"></asp:TextBox>


                                </td>

                                <td align="center">

                                    <asp:TextBox ID="TextBox42" runat="server" Width="150px"></asp:TextBox>

                                </td>
                                <td align="center">

                                    <asp:TextBox ID="TextBox43" runat="server" Width="150px"></asp:TextBox>

                                </td>
                                <td align="center">

                                    <asp:TextBox ID="TextBox44" runat="server" Width="150px"></asp:TextBox>


                                </td>
                                <td align="center">

                                    <asp:TextBox ID="TextBox45" runat="server" Width="150px"></asp:TextBox>


                                </td>
                            </tr>

                            <tr>
                                <td colspan="5">
                                    <div class="form-sec-row">
                                        <label><strong>&nbsp;</strong></label>
                                        <asp:Button ID="Button1" runat="server" Text="Submit" CssClass="submit-button"
                                            Height="28px" OnClick="Button1_Click" />
                                        <asp:Button ID="Button2" runat="server" Text="Cancel" CssClass="submit-button" Height="28px"
                                            OnClick="Button2_Click" />
                                        <div class="clear"></div>
                                    </div>
                                </td>
                            </tr>


                        </table>
                    </asp:View>
                    <asp:View ID="View3" runat="server">
                        <asp:TextBox ID="TextBox20" runat="server" Visible="false" Width="150px"></asp:TextBox>
                        <table border="1" cellpadding="0" cellspacing="0" width="100%">
                            <tr>
                                <td colspan="3">

                                    <div class="pageheader">
                                        <asp:Label ID="Label7" runat="server"> Patient Previous Vaccine Report </asp:Label>
                                    </div>


                                    <div class="listing" style='width: 100%; height: 200px; overflow: auto;'>
                                        <asp:GridView ID="GridView3" CssClass="grid" PagerStyle-CssClass="pgr" DataKeyNames="RowId"
                                            runat="server" AutoGenerateColumns="False"
                                            AllowPaging="True" PageSize="70" Width="100%"
                                            OnPageIndexChanging="GridView3_PageIndexChanging"
                                            SelectedRowStyle-BackColor="GreenYellow" OnRowCancelingEdit="GridView3_RowCancelingEdit"
                                            OnRowDeleting="GridView3_RowDeleting" OnRowEditing="GridView3_RowEditing"
                                            OnRowUpdating="GridView3_RowUpdating" OnRowDataBound="GridView3_RowDataBound">
                                            <RowStyle HorizontalAlign="Center" />
                                            <Columns>


                                                <asp:TemplateField HeaderText="ID" Visible="false">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblid" runat="server" Text='<%# Bind("RowId") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Vaccine Name">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lbldate" runat="server" Text='<%# Bind("Name") %>'></asp:Label>
                                                        <asp:Label ID="lblvcid" runat="server" Text='<%# Bind("VacineID") %>' Visible="true"></asp:Label>
                                                    </ItemTemplate>

                                                    <EditItemTemplate>
                                                        <asp:DropDownList ID="ddlvaccinationName" runat="server">
                                                        </asp:DropDownList>
                                                    </EditItemTemplate>
                                                </asp:TemplateField>


                                                <asp:TemplateField HeaderText="Date">

                                                    <ItemTemplate>
                                                        <asp:Label ID="lblname" runat="server" Text='<%# Bind("Date1") %>'></asp:Label>
                                                    </ItemTemplate>

                                                    <EditItemTemplate>
                                                        <asp:TextBox ID="txtdate" runat="server" Text='<%# Bind("Date1") %>'></asp:TextBox>
                                                    </EditItemTemplate>
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="Comment">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblage" runat="server" Text='<%# Bind("Comment") %>'></asp:Label>
                                                    </ItemTemplate>

                                                    <EditItemTemplate>
                                                        <asp:TextBox ID="txtcommenet" runat="server" Text='<%# Bind("Comment") %>'></asp:TextBox>
                                                    </EditItemTemplate>
                                                </asp:TemplateField>
                                                <asp:CommandField HeaderText="Edit" ControlStyle-CssClass="temp" ShowEditButton="True" />
                                                <asp:CommandField HeaderText="Delete" ShowDeleteButton="True" />
                                            </Columns>
                                            <PagerStyle CssClass="pgr" />
                                            <EditRowStyle BackColor="#CCFF33" />
                                            <AlternatingRowStyle BackColor="#FFDB91" />
                                            <SelectedRowStyle BackColor="GreenYellow" />
                                        </asp:GridView>
                                    </div>


                                    <br />
                                    <br />
                                </td>
                            </tr>
                            <tr>
                                <td colspan="3">
                                    <div class="pageheader">
                                        <asp:Label ID="Label8" runat="server"> Patient New Vaccine</asp:Label>
                                    </div>
                                </td>
                            </tr>
                            <tr style='background-color: #FF9300;'>
                                <td align="center">
                                    <div class="form-sec-row">
                                        <label class="lname"><strong>Vaccine Name</strong></label>
                                    </div>
                                </td>

                                <td align="center">
                                    <div class="form-sec-row">
                                        <label class="lname"><strong>Date</strong></label>
                                    </div>
                                </td>
                                <td align="center">
                                    <div class="form-sec-row">
                                        <label class="lname"><strong>Comment </strong></label>
                                    </div>
                                </td>

                            </tr>
                            <tr>
                                <td align="center">
                                    <asp:DropDownList ID="DropDownList33" runat="server">
                                    </asp:DropDownList>



                                </td>

                                <td align="center">


                                    <asp:TextBox ID="TextBox92" runat="server" CssClass="DatepickerReCall" Width="150px"></asp:TextBox>

                                </td>
                                <td align="center">

                                    <asp:TextBox ID="TextBox93" runat="server" Width="150px"></asp:TextBox>

                                </td>

                            </tr>

                            <tr>
                                <td align="center">
                                    <asp:DropDownList ID="DropDownList34" runat="server">
                                    </asp:DropDownList>


                                </td>

                                <td align="center">


                                    <asp:TextBox ID="TextBox95" runat="server" CssClass="DatepickerReCall" Width="150px"></asp:TextBox>

                                </td>
                                <td align="center">

                                    <asp:TextBox ID="TextBox96" runat="server" Width="150px"></asp:TextBox>

                                </td>

                            </tr>
                            <tr>
                                <td align="center">
                                    <asp:DropDownList ID="DropDownList35" runat="server">
                                    </asp:DropDownList>


                                </td>

                                <td align="center">


                                    <asp:TextBox ID="TextBox98" runat="server" CssClass="DatepickerReCall" Width="150px"></asp:TextBox>

                                </td>
                                <td align="center">

                                    <asp:TextBox ID="TextBox99" runat="server" Width="150px"></asp:TextBox>

                                </td>

                            </tr>
                            <tr>
                                <td align="center">
                                    <asp:DropDownList ID="DropDownList36" runat="server">
                                    </asp:DropDownList>


                                </td>

                                <td align="center">


                                    <asp:TextBox ID="TextBox101" runat="server" CssClass="DatepickerReCall" Width="150px"></asp:TextBox>

                                </td>
                                <td align="center">

                                    <asp:TextBox ID="TextBox102" runat="server" Width="150px"></asp:TextBox>

                                </td>

                            </tr>
                            <tr>
                                <td align="center">
                                    <asp:DropDownList ID="DropDownList37" runat="server">
                                    </asp:DropDownList>


                                </td>

                                <td align="center">


                                    <asp:TextBox ID="TextBox104" runat="server" CssClass="DatepickerReCall" Width="150px"></asp:TextBox>

                                </td>
                                <td align="center">

                                    <asp:TextBox ID="TextBox105" runat="server" Width="150px"></asp:TextBox>

                                </td>

                            </tr>
                            <tr>
                                <td align="center">
                                    <asp:DropDownList ID="DropDownList38" runat="server">
                                    </asp:DropDownList>


                                </td>

                                <td align="center">


                                    <asp:TextBox ID="TextBox107" runat="server" CssClass="DatepickerReCall" Width="150px"></asp:TextBox>

                                </td>
                                <td align="center">

                                    <asp:TextBox ID="TextBox108" runat="server" Width="150px"></asp:TextBox>

                                </td>

                            </tr>
                            <tr>
                                <td align="center">
                                    <asp:DropDownList ID="DropDownList39" runat="server">
                                    </asp:DropDownList>


                                </td>

                                <td align="center">


                                    <asp:TextBox ID="TextBox110" runat="server" CssClass="DatepickerReCall" Width="150px"></asp:TextBox>

                                </td>
                                <td align="center">

                                    <asp:TextBox ID="TextBox111" runat="server" Width="150px"></asp:TextBox>

                                </td>

                            </tr>
                            <tr>
                                <td align="center">
                                    <asp:DropDownList ID="DropDownList40" runat="server">
                                    </asp:DropDownList>


                                </td>

                                <td align="center">


                                    <asp:TextBox ID="TextBox113" runat="server" CssClass="DatepickerReCall" Width="150px"></asp:TextBox>

                                </td>
                                <td align="center">

                                    <asp:TextBox ID="TextBox114" runat="server" Width="150px"></asp:TextBox>

                                </td>

                            </tr>
                            <tr>
                                <td align="center">
                                    <asp:DropDownList ID="DropDownList41" runat="server">
                                    </asp:DropDownList>


                                </td>

                                <td align="center">


                                    <asp:TextBox ID="TextBox116" runat="server" CssClass="DatepickerReCall" Width="150px"></asp:TextBox>

                                </td>
                                <td align="center">

                                    <asp:TextBox ID="TextBox117" runat="server" Width="150px"></asp:TextBox>

                                </td>

                            </tr>
                            <tr>
                                <td align="center">
                                    <asp:DropDownList ID="DropDownList42" runat="server">
                                    </asp:DropDownList>


                                </td>

                                <td align="center">


                                    <asp:TextBox ID="TextBox119" runat="server" CssClass="DatepickerReCall" Width="150px"></asp:TextBox>

                                </td>
                                <td align="center">

                                    <asp:TextBox ID="TextBox120" runat="server" Width="150px"></asp:TextBox>

                                </td>

                            </tr>
                            <tr>
                                <td align="center">
                                    <asp:DropDownList ID="DropDownList43" runat="server">
                                    </asp:DropDownList>


                                </td>

                                <td align="center">


                                    <asp:TextBox ID="TextBox122" runat="server" CssClass="DatepickerReCall" Width="150px"></asp:TextBox>

                                </td>
                                <td align="center">

                                    <asp:TextBox ID="TextBox123" runat="server" Width="150px"></asp:TextBox>

                                </td>

                            </tr>
                            <tr>
                                <td align="center">
                                    <asp:DropDownList ID="DropDownList44" runat="server">
                                    </asp:DropDownList>


                                </td>

                                <td align="center">


                                    <asp:TextBox ID="TextBox125" runat="server" CssClass="DatepickerReCall" Width="150px"></asp:TextBox>

                                </td>
                                <td align="center">

                                    <asp:TextBox ID="TextBox126" runat="server" Width="150px"></asp:TextBox>

                                </td>

                            </tr>
                            <tr>
                                <td align="center">
                                    <asp:DropDownList ID="DropDownList45" runat="server">
                                    </asp:DropDownList>


                                </td>

                                <td align="center">


                                    <asp:TextBox ID="TextBox128" runat="server" CssClass="DatepickerReCall" Width="150px"></asp:TextBox>

                                </td>
                                <td align="center">

                                    <asp:TextBox ID="TextBox129" runat="server" Width="150px"></asp:TextBox>

                                </td>

                            </tr>
                            <tr>
                                <td align="center">
                                    <asp:DropDownList ID="DropDownList46" runat="server">
                                    </asp:DropDownList>


                                </td>

                                <td align="center">


                                    <asp:TextBox ID="TextBox131" runat="server" CssClass="DatepickerReCall" Width="150px"></asp:TextBox>

                                </td>
                                <td align="center">

                                    <asp:TextBox ID="TextBox132" runat="server" Width="150px"></asp:TextBox>

                                </td>

                            </tr>
                            <tr>
                                <td align="center">

                                    <asp:DropDownList ID="DropDownList47" runat="server">
                                    </asp:DropDownList>

                                </td>

                                <td align="center">


                                    <asp:TextBox ID="TextBox134" runat="server" CssClass="DatepickerReCall" Width="150px"></asp:TextBox>

                                </td>
                                <td align="center">

                                    <asp:TextBox ID="TextBox135" runat="server" Width="150px"></asp:TextBox>

                                </td>

                            </tr>

                            <tr>
                                <td colspan="3">
                                    <div class="form-sec-row">
                                        <label><strong>&nbsp;</strong></label>
                                        <asp:Button ID="Button11" runat="server" Text="Submit" CssClass="submit-button"
                                            Height="28px" OnClick="Button11_Click" />
                                        <asp:Button ID="Button12" runat="server" Text="Cancel"
                                            CssClass="submit-button" Height="28px" OnClick="Button12_Click" />
                                        <div class="clear"></div>
                                    </div>
                                </td>
                            </tr>
                        </table>
                    </asp:View>

                    <asp:View ID="View4" runat="server">

                        <table border="1" cellpadding="0" cellspacing="0" width="100%">
                            <tr>
                                <td colspan="3">

                                    <div class="pageheader">
                                        <asp:Label ID="Label6" runat="server"> Patient Previous Investigation Report </asp:Label>
                                    </div>


                                    <div class="listing" style='width: 100%; height: 200px; overflow: auto;'>
                                        <asp:GridView ID="GridView2" CssClass="grid" PagerStyle-CssClass="pgr" DataKeyNames="RowId"
                                            runat="server" AutoGenerateColumns="False"
                                            AllowPaging="True" PageSize="70" Width="100%"
                                            SelectedRowStyle-BackColor="GreenYellow" OnPageIndexChanging="GridView2_PageIndexChanging"
                                            OnRowCancelingEdit="GridView2_RowCancelingEdit"
                                            OnRowDeleting="GridView2_RowDeleting" OnRowEditing="GridView2_RowEditing"
                                            OnRowUpdating="GridView2_RowUpdating">
                                            <RowStyle HorizontalAlign="Center" />
                                            <Columns>

                                                <asp:TemplateField HeaderText="ID" Visible="false">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblid" runat="server" Text='<%# Bind("RowId") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="Date">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lbldate" runat="server" Text='<%# Bind("Date1") %>'></asp:Label>
                                                    </ItemTemplate>

                                                    <EditItemTemplate>
                                                        <asp:TextBox ID="txtdate" runat="server" Text='<%# Bind("Date1") %>'></asp:TextBox>
                                                    </EditItemTemplate>
                                                </asp:TemplateField>


                                                <asp:TemplateField HeaderText="Investigation">

                                                    <ItemTemplate>
                                                        <asp:Label ID="lblname" runat="server" Text='<%# Bind("Investigation") %>'></asp:Label>
                                                    </ItemTemplate>

                                                    <EditItemTemplate>
                                                        <asp:TextBox ID="txtinvs" runat="server" Text='<%# Bind("Investigation") %>'></asp:TextBox>
                                                    </EditItemTemplate>
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="Details">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblage" runat="server" Text='<%# Bind("Details") %>'></asp:Label>
                                                    </ItemTemplate>

                                                    <EditItemTemplate>
                                                        <asp:TextBox ID="txtdtls" runat="server" Text='<%# Bind("Details") %>'></asp:TextBox>
                                                    </EditItemTemplate>
                                                </asp:TemplateField>



                                                <%-- <asp:CommandField SelectText="Edit" ShowSelectButton="True" ControlStyle-CssClass="temp" HeaderText="Edit" SelectImageUrl="~/Images/edit.png" ButtonType="Image">
                        <ControlStyle CssClass="temp"></ControlStyle>
                    </asp:CommandField>
                    <asp:CommandField ShowDeleteButton="True" ControlStyle-CssClass="temp" HeaderText="Delete" DeleteImageUrl="~/Images/delete.png" ButtonType="Image">
                        <ControlStyle CssClass="temp"></ControlStyle>
                    </asp:CommandField>--%>
                                                <asp:CommandField HeaderText="Edit" ControlStyle-CssClass="temp" ShowEditButton="True" />
                                                <asp:CommandField HeaderText="Delete" ShowDeleteButton="True" />
                                            </Columns>
                                            <PagerStyle CssClass="pgr" />
                                            <EditRowStyle BackColor="#CCFF33" />
                                            <AlternatingRowStyle BackColor="#FFDB91" />
                                            <SelectedRowStyle BackColor="GreenYellow" />
                                        </asp:GridView>
                                    </div>


                                    <br />
                                    <br />
                                </td>
                            </tr>
                            <tr>
                                <td colspan="3">
                                    <div class="pageheader">
                                        <asp:Label ID="Label5" runat="server"> Patient New Investigation</asp:Label>
                                    </div>
                                </td>
                            </tr>
                            <tr style='background-color: #FF9300;'>
                                <td align="center" style="width:15%;">
                                    <div class="form-sec-row">
                                        <label class="lname"><strong>Date</strong></label>
                                    </div>
                                </td>

                                <td align="center" style="width:25%;">
                                    <div class="form-sec-row">
                                        <label class="lname"><strong>Investigation</strong></label>
                                    </div>
                                </td>
                                <td align="center" style="width:60%;">
                                    <div class="form-sec-row">
                                        <label class="lname"><strong>Details </strong></label>
                                    </div>
                                </td>

                            </tr>
                            <tr>
                                <td align="left">
                                    <asp:TextBox ID="TextBox46" runat="server" CssClass="DatepickerReCall" Width="150px"></asp:TextBox>
                                    <cc1:MaskedEditExtender ID="MaskedEditExtender4" runat="server" TargetControlID="TextBox46"
                                    Mask="99/99/9999" MessageValidatorTip="true" MaskType="Date" DisplayMoney="Left"
                                    AcceptNegative="Left" ErrorTooltipEnabled="True" CultureName="en-GB" />
                                </td>

                                <td align="left">
                                    <asp:TextBox ID="TextBox47" runat="server" Width="90%"></asp:TextBox>
                                </td>
                                <td align="left">

                                    <asp:TextBox ID="TextBox48" runat="server" Width="90%"></asp:TextBox>
                                </td>

                            </tr>

                            <tr>
                                <td align="left">
                                    <asp:TextBox ID="TextBox49" runat="server" CssClass="DatepickerReCall" Width="150px"></asp:TextBox>
                                    <cc1:MaskedEditExtender ID="MaskedEditExtender5" runat="server" TargetControlID="TextBox49"
                                    Mask="99/99/9999" MessageValidatorTip="true" MaskType="Date" DisplayMoney="Left"
                                    AcceptNegative="Left" ErrorTooltipEnabled="True" CultureName="en-GB" />
                                </td>

                                <td align="left">
                                    <asp:TextBox ID="TextBox50" runat="server" Width="90%"></asp:TextBox>
                                </td>
                                <td align="left">
                                    <asp:TextBox ID="TextBox51" runat="server" Width="90%"></asp:TextBox>

                                </td>

                            </tr>
                            <tr>
                                <td align="left">
                                    <asp:TextBox ID="TextBox52" runat="server" CssClass="DatepickerReCall" Width="150px"></asp:TextBox>
                                    <cc1:MaskedEditExtender ID="MaskedEditExtender6" runat="server" TargetControlID="TextBox52"
                        Mask="99/99/9999" MessageValidatorTip="true" MaskType="Date" DisplayMoney="Left"
                        AcceptNegative="Left" ErrorTooltipEnabled="True" CultureName="en-GB" />

                                </td>
                                <td align="left">
                                    <asp:TextBox ID="TextBox53" runat="server" Width="90%"></asp:TextBox>
                                </td>
                                <td align="left">
                                    <asp:TextBox ID="TextBox54" runat="server" Width="90%"></asp:TextBox>
                                </td>

                            </tr>
                            <tr>
                                <td align="left">
                                    <asp:TextBox ID="TextBox55" runat="server" CssClass="DatepickerReCall" Width="150px"></asp:TextBox>
                                    <cc1:MaskedEditExtender ID="MaskedEditExtender7" runat="server" TargetControlID="TextBox55"
                                    Mask="99/99/9999" MessageValidatorTip="true" MaskType="Date" DisplayMoney="Left"
                                    AcceptNegative="Left" ErrorTooltipEnabled="True" CultureName="en-GB" />


                                </td>

                                <td align="left">
                                    <asp:TextBox ID="TextBox56" runat="server" Width="90%"></asp:TextBox>
                                </td>
                                <td align="left">
                                    <asp:TextBox ID="TextBox57" runat="server" Width="90%"></asp:TextBox>
                                </td>

                            </tr>
                            <tr>
                                <td align="left">
                                    <asp:TextBox ID="TextBox58" runat="server" CssClass="DatepickerReCall" Width="150px"></asp:TextBox>
                                    <cc1:MaskedEditExtender ID="MaskedEditExtender8" runat="server" TargetControlID="TextBox58"
                        Mask="99/99/9999" MessageValidatorTip="true" MaskType="Date" DisplayMoney="Left"
                        AcceptNegative="Left" ErrorTooltipEnabled="True" CultureName="en-GB" />
                                </td>
                                <td align="left">
                                    <asp:TextBox ID="TextBox59" runat="server" Width="90%"></asp:TextBox>
                                </td>
                                <td align="left">
                                    <asp:TextBox ID="TextBox60" runat="server" Width="90%"></asp:TextBox>
                                </td>

                            </tr>
                            <tr>
                                <td align="left">
                                    <asp:TextBox ID="TextBox61" runat="server" CssClass="DatepickerReCall" Width="150px"></asp:TextBox>
                                    <cc1:MaskedEditExtender ID="MaskedEditExtender9" runat="server" TargetControlID="TextBox61"
                        Mask="99/99/9999" MessageValidatorTip="true" MaskType="Date" DisplayMoney="Left"
                        AcceptNegative="Left" ErrorTooltipEnabled="True" CultureName="en-GB" />
                                </td>
                                <td align="left">
                                    <asp:TextBox ID="TextBox62" runat="server" Width="90%"></asp:TextBox>
                                </td>
                                <td align="left">
                                    <asp:TextBox ID="TextBox63" runat="server" Width="90%"></asp:TextBox>
                                 </td>

                            </tr>
                            <tr>
                                <td align="left">
                                    <asp:TextBox ID="TextBox64" runat="server" CssClass="DatepickerReCall" Width="150px"></asp:TextBox>
                                    <cc1:MaskedEditExtender ID="MaskedEditExtender10" runat="server" TargetControlID="TextBox64"
                        Mask="99/99/9999" MessageValidatorTip="true" MaskType="Date" DisplayMoney="Left"
                        AcceptNegative="Left" ErrorTooltipEnabled="True" CultureName="en-GB" />
                                </td>
                                <td align="left">
                                    <asp:TextBox ID="TextBox65" runat="server" Width="90%"></asp:TextBox>
                                </td>
                                <td align="left">
                                    <asp:TextBox ID="TextBox66" runat="server" Width="90%"></asp:TextBox>
                                </td>

                            </tr>
                            <tr>
                                <td align="left">
                                    <asp:TextBox ID="TextBox67" runat="server" CssClass="DatepickerReCall" Width="150px"></asp:TextBox>
                                    <cc1:MaskedEditExtender ID="MaskedEditExtender11" runat="server" TargetControlID="TextBox67"
                        Mask="99/99/9999" MessageValidatorTip="true" MaskType="Date" DisplayMoney="Left"
                        AcceptNegative="Left" ErrorTooltipEnabled="True" CultureName="en-GB" />

                               </td>
                                <td align="left">
                                   <asp:TextBox ID="TextBox68" runat="server" Width="90%"></asp:TextBox>
                                </td>
                                <td align="left">
                                    <asp:TextBox ID="TextBox69" runat="server" Width="90%"></asp:TextBox>
                                </td>

                            </tr>
                            <tr>
                                <td align="left">
                                    <asp:TextBox ID="TextBox70" runat="server" CssClass="DatepickerReCall" Width="150px"></asp:TextBox>
                                    <cc1:MaskedEditExtender ID="MaskedEditExtender12" runat="server" TargetControlID="TextBox70"
                        Mask="99/99/9999" MessageValidatorTip="true" MaskType="Date" DisplayMoney="Left"
                        AcceptNegative="Left" ErrorTooltipEnabled="True" CultureName="en-GB" />
                                </td>
                                <td align="left">
                                    <asp:TextBox ID="TextBox71" runat="server" Width="90%"></asp:TextBox>
                                </td>
                                <td align="left">
                                    <asp:TextBox ID="TextBox72" runat="server" Width="90%"></asp:TextBox>
                                </td>

                            </tr>
                            <tr>
                                <td align="left">
                                    <asp:TextBox ID="TextBox73" runat="server" CssClass="DatepickerReCall" Width="150px"></asp:TextBox>
                                    <cc1:MaskedEditExtender ID="MaskedEditExtender13" runat="server" TargetControlID="TextBox73"
                        Mask="99/99/9999" MessageValidatorTip="true" MaskType="Date" DisplayMoney="Left"
                        AcceptNegative="Left" ErrorTooltipEnabled="True" CultureName="en-GB" />


                                </td>

                                <td align="left">
                                    <asp:TextBox ID="TextBox74" runat="server" Width="90%"></asp:TextBox>
                                </td>
                                <td align="left">

                                    <asp:TextBox ID="TextBox75" runat="server" Width="90%"></asp:TextBox>

                                </td>

                            </tr>
                            <tr>
                                <td align="left">
                                    <asp:TextBox ID="TextBox76" runat="server" CssClass="DatepickerReCall" Width="150px"></asp:TextBox>
                                    <cc1:MaskedEditExtender ID="MaskedEditExtender14" runat="server" TargetControlID="TextBox76"
                        Mask="99/99/9999" MessageValidatorTip="true" MaskType="Date" DisplayMoney="Left"
                        AcceptNegative="Left" ErrorTooltipEnabled="True" CultureName="en-GB" />
                                </td>

                                <td align="left">
                                    <asp:TextBox ID="TextBox77" runat="server" Width="90%"></asp:TextBox>
                                </td>
                                <td align="left">
                                    <asp:TextBox ID="TextBox78" runat="server" Width="90%"></asp:TextBox>
                                </td>

                            </tr>
                            <tr>
                                <td align="left">
                                    <asp:TextBox ID="TextBox79" runat="server" CssClass="DatepickerReCall" Width="150px"></asp:TextBox>
                                    <cc1:MaskedEditExtender ID="MaskedEditExtender15" runat="server" TargetControlID="TextBox79"
                        Mask="99/99/9999" MessageValidatorTip="true" MaskType="Date" DisplayMoney="Left"
                        AcceptNegative="Left" ErrorTooltipEnabled="True" CultureName="en-GB" />
                                </td>

                                <td align="left">
                                    <asp:TextBox ID="TextBox80" runat="server" Width="90%"></asp:TextBox>
                                </td>
                                <td align="left">
                                    <asp:TextBox ID="TextBox81" runat="server" Width="90%"></asp:TextBox>
                                </td>

                            </tr>
                            <tr>
                                <td align="left">
                                    <asp:TextBox ID="TextBox82" runat="server" CssClass="DatepickerReCall" Width="150px"></asp:TextBox>
                                    <cc1:MaskedEditExtender ID="MaskedEditExtender16" runat="server" TargetControlID="TextBox82"
                        Mask="99/99/9999" MessageValidatorTip="true" MaskType="Date" DisplayMoney="Left"
                        AcceptNegative="Left" ErrorTooltipEnabled="True" CultureName="en-GB" />
                                </td>
                                <td align="left">
                                    <asp:TextBox ID="TextBox83" runat="server" Width="90%"></asp:TextBox>
                                </td>
                                <td align="left">
                                    <asp:TextBox ID="TextBox84" runat="server" Width="90%"></asp:TextBox>
                                </td>

                            </tr>
                            <tr>
                                <td align="left">
                                    <asp:TextBox ID="TextBox85" runat="server" CssClass="DatepickerReCall" Width="150px"></asp:TextBox>
                                    <cc1:MaskedEditExtender ID="MaskedEditExtender17" runat="server" TargetControlID="TextBox86"
                        Mask="99/99/9999" MessageValidatorTip="true" MaskType="Date" DisplayMoney="Left"
                        AcceptNegative="Left" ErrorTooltipEnabled="True" CultureName="en-GB" />
                                </td>
                                <td align="left">
                                    <asp:TextBox ID="TextBox86" runat="server" Width="90%"></asp:TextBox>
                                </td>
                                <td align="left">
                                    <asp:TextBox ID="TextBox87" runat="server" Width="90%"></asp:TextBox>
                                </td>

                            </tr>
                            <tr>
                                <td align="left">
                                    <asp:TextBox ID="TextBox88" runat="server" CssClass="DatepickerReCall" Width="150px"></asp:TextBox>
                                    <cc1:MaskedEditExtender ID="MaskedEditExtender18" runat="server" TargetControlID="TextBox88"
                        Mask="99/99/9999" MessageValidatorTip="true" MaskType="Date" DisplayMoney="Left"
                        AcceptNegative="Left" ErrorTooltipEnabled="True" CultureName="en-GB" />
                                </td>
                                <td align="left">
                                    <asp:TextBox ID="TextBox89" runat="server" Width="90%"></asp:TextBox>
                                </td>
                                <td align="left">
                                    <asp:TextBox ID="TextBox90" runat="server" Width="90%"></asp:TextBox>
                                </td>

                            </tr>

                            <tr>
                                <td colspan="3">
                                    <div class="form-sec-row">
                                        <label><strong>&nbsp;</strong></label>
                                        <asp:Button ID="Button9" runat="server" Text="Submit" CssClass="submit-button"
                                            Height="28px" OnClick="Button9_Click" />
                                        <asp:Button ID="Button10" runat="server" Text="Cancel"
                                            CssClass="submit-button" Height="28px" OnClick="Button10_Click" />
                                        <div class="clear"></div>
                                    </div>
                                </td>
                            </tr>
                        </table>

                    </asp:View>
                    <asp:View ID="View5" runat="server">
                        <div style="width: 100%; overflow: auto;">

                            <table border="1" cellpadding="0" cellspacing="0" width="100%">
                                <tr>
                                    <td colspan="5">


                                        <div class="pageheader">
                                            <asp:Label ID="Label9" runat="server"> Patient Previous Clinical Finding</asp:Label>
                                        </div>
                                        <div class="listing" style='width: 100%; height: 200px; overflow: auto;'>
                                            <asp:GridView ID="GridView4" CssClass="grid" PagerStyle-CssClass="pgr" DataKeyNames="RowID"
                                                runat="server" AutoGenerateColumns="False" AllowPaging="True" PageSize="70"
                                                SelectedRowStyle-BackColor="GreenYellow" OnPageIndexChanging="GridView4_PageIndexChanging"
                                                OnRowCancelingEdit="GridView4_RowCancelingEdit" Width="100%"
                                                OnRowDeleting="GridView4_RowDeleting" OnRowEditing="GridView4_RowEditing"
                                                OnRowUpdating="GridView4_RowUpdating"
                                                OnRowDataBound="GridView4_RowDataBound">
                                                <RowStyle HorizontalAlign="Center" />
                                                <Columns>


                                                    <asp:TemplateField HeaderText="ID" Visible="false">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblid" runat="server" Text='<%# Bind("RowId") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Date">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lbldate" runat="server" Text='<%# Bind("Date1") %>'></asp:Label>
                                                        </ItemTemplate>
                                                        <EditItemTemplate>
                                                            <asp:TextBox ID="txtdate" runat="server" Text='<%# Bind("Date1") %>'></asp:TextBox>
                                                        </EditItemTemplate>
                                                    </asp:TemplateField>


                                                    <asp:TemplateField HeaderText="Complain" ItemStyle-HorizontalAlign="Left">

                                                        <ItemTemplate>
                                                            <asp:Label ID="lblname" runat="server" Text='<%# Bind("ComplainName") %>'></asp:Label>
                                                        </ItemTemplate>

                                                        <EditItemTemplate>
                                                            <asp:DropDownList ID="ddlcomplain" runat="server">
                                                            </asp:DropDownList>
                                                        </EditItemTemplate>
                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="Weight">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblage" runat="server" Text='<%# Bind("Weight") %>'></asp:Label>
                                                        </ItemTemplate>

                                                        <EditItemTemplate>
                                                            <asp:TextBox ID="txtweight" runat="server" Text='<%# Bind("Weight") %>'></asp:TextBox>
                                                        </EditItemTemplate>
                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="BP">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblsex" runat="server" Text='<%# Bind("BP") %>'></asp:Label>
                                                        </ItemTemplate>

                                                        <EditItemTemplate>
                                                            <asp:TextBox ID="txtbp" runat="server" Text='<%# Bind("BP") %>'></asp:TextBox>
                                                        </EditItemTemplate>

                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="P">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblphone1" runat="server" Text='<%# Bind("P") %>'></asp:Label>
                                                        </ItemTemplate>

                                                        <EditItemTemplate>
                                                            <asp:TextBox ID="txtp" runat="server" Text='<%# Bind("P") %>'></asp:TextBox>
                                                        </EditItemTemplate>
                                                    </asp:TemplateField>



                                                    <asp:TemplateField HeaderText="E">

                                                        <ItemTemplate>
                                                            <asp:Label ID="lble" runat="server" Text='<%# Bind("E") %>'></asp:Label>
                                                        </ItemTemplate>

                                                        <EditItemTemplate>
                                                            <asp:TextBox ID="txte" runat="server" Text='<%# Bind("E") %>'></asp:TextBox>
                                                        </EditItemTemplate>
                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="Chest">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblwt" runat="server" Text='<%# Bind("Chest") %>'></asp:Label>
                                                        </ItemTemplate>

                                                        <EditItemTemplate>
                                                            <asp:TextBox ID="txtchest" runat="server" Text='<%# Bind("Chest") %>'></asp:TextBox>
                                                        </EditItemTemplate>
                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="PA">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblpa" runat="server" Text='<%# Bind("PA") %>'></asp:Label>
                                                        </ItemTemplate>

                                                        <EditItemTemplate>
                                                            <asp:TextBox ID="txtpa" runat="server" Text='<%# Bind("PA") %>'></asp:TextBox>
                                                        </EditItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="PV">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblpv" runat="server" Text='<%# Bind("PV") %>'></asp:Label>
                                                        </ItemTemplate>

                                                        <EditItemTemplate>
                                                            <asp:TextBox ID="txtpv" runat="server" Text='<%# Bind("PV") %>'></asp:TextBox>
                                                        </EditItemTemplate>
                                                    </asp:TemplateField>



                                                    <asp:TemplateField HeaderText="FH8">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblfh8" runat="server" Text='<%# Bind("FH8") %>'></asp:Label>
                                                        </ItemTemplate>

                                                        <EditItemTemplate>
                                                            <asp:TextBox ID="txtfh8" runat="server" Text='<%# Bind("FH8") %>'></asp:TextBox>
                                                        </EditItemTemplate>

                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Others">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblothers" runat="server" Text='<%# Bind("Others") %>'></asp:Label>
                                                        </ItemTemplate>

                                                        <EditItemTemplate>
                                                            <asp:TextBox ID="txtothers" runat="server" Text='<%# Bind("Others") %>'></asp:TextBox>
                                                        </EditItemTemplate>
                                                    </asp:TemplateField>



                                                    <asp:CommandField HeaderText="Edit" ControlStyle-CssClass="temp" ShowEditButton="True" />
                                                    <asp:CommandField HeaderText="Delete" ShowDeleteButton="True" />



                                                </Columns>
                                                <PagerStyle CssClass="pgr" />
                                                <EditRowStyle BackColor="#CCFF33" />
                                                <AlternatingRowStyle BackColor="#FFDB91" />
                                                <SelectedRowStyle BackColor="GreenYellow" />
                                            </asp:GridView>
                                        </div>


                                        <br />
                                        <br />
                                    </td>
                                </tr>

                            </table>
                        </div>
                        <div style="width: 100%; overflow: auto;">
                            <table border="1" cellpadding="0" cellspacing="0">
                                <tr>
                                    <td colspan="11">

                                        <div class="pageheader">
                                            <asp:Label ID="Label10" runat="server"> Patient Clinical Finding </asp:Label>
                                        </div>

                                    </td>
                                </tr>

                                <tr style='background-color: #FF9300;'>
                                    <td align="center" style='width: 100px;'>

                                        <label><strong>Date</strong></label>

                                    </td>

                                    <td align="center">

                                        <label><strong>Complain</strong></label>

                                    </td>
                                    <td align="center" style='width: 100px;'>

                                        <label><strong>Weight </strong></label>

                                    </td>
                                    <td align="center" style='width: 80px;'>

                                        <label><strong>BP</strong></label>


                                    </td>
                                    <td align="center" style='width: 40px;'>

                                        <label><strong>P</strong></label>


                                    </td>



                                    <td align="center">

                                        <label><strong>E</strong></label>

                                    </td>
                                    <td align="center">

                                        <label><strong>Chest </strong></label>

                                    </td>
                                    <td align="center">

                                        <label><strong>P/A</strong></label>


                                    </td>
                                    <td align="center">

                                        <label><strong>P/V</strong></label>


                                    </td>

                                    <td align="center">

                                        <label><strong>FH8</strong></label>


                                    </td>
                                    <td align="center">

                                        <label><strong>Others</strong></label>


                                    </td>


                                </tr>
                                <tr>
                                    <td align="left" style='width: 100px;'>

                                        <asp:TextBox ID="TextBox136" runat="server" CssClass="DatepickerReCall" Width="100px"></asp:TextBox>
                                        <cc1:MaskedEditExtender ID="MaskedEditExtender19" runat="server" TargetControlID="TextBox136"
                                        Mask="99/99/9999" MessageValidatorTip="true" MaskType="Date" DisplayMoney="Left"
                                        AcceptNegative="Left" ErrorTooltipEnabled="True" CultureName="en-GB" />

                                    </td>

                                    <td align="center">

                                        <asp:DropDownList ID="DropDownList48" runat="server">
                                        </asp:DropDownList>

                                    </td>
                                    <td align="center" style='width: 100px;'>

                                        <asp:TextBox ID="TextBox138" runat="server" Width="100px"></asp:TextBox>

                                    </td>
                                    <td align="center" style='width: 80px;'>

                                        <asp:TextBox ID="TextBox139" runat="server" Width="80px"></asp:TextBox>


                                    </td>
                                    <td align="center">

                                        <asp:TextBox ID="TextBox140" runat="server" Width="40px"></asp:TextBox>


                                    </td>

                                    <td align="center">

                                        <asp:TextBox ID="TextBox141" runat="server" Width="150px"></asp:TextBox>


                                    </td>

                                    <td align="center">

                                        <asp:TextBox ID="TextBox142" runat="server" Width="150px"></asp:TextBox>


                                    </td>

                                    <td align="center">

                                        <asp:TextBox ID="TextBox143" runat="server" Width="150px"></asp:TextBox>


                                    </td>
                                    <td align="center">

                                        <asp:TextBox ID="TextBox144" runat="server" Width="150px"></asp:TextBox>


                                    </td>

                                    <td align="center">

                                        <asp:TextBox ID="TextBox145" runat="server" Width="150px"></asp:TextBox>


                                    </td>

                                    <td align="center">

                                        <asp:TextBox ID="TextBox146" runat="server" Width="150px"></asp:TextBox>


                                    </td>
                                </tr>
                                <tr>
                                    <td align="center" style='width: 100px;'>

                                        <asp:TextBox ID="TextBox147" CssClass="DatepickerReCall" runat="server" Width="100px"></asp:TextBox>
                                        <cc1:MaskedEditExtender ID="MaskedEditExtender20" runat="server" TargetControlID="TextBox147"
                                        Mask="99/99/9999" MessageValidatorTip="true" MaskType="Date" DisplayMoney="Left"
                                        AcceptNegative="Left" ErrorTooltipEnabled="True" CultureName="en-GB" />

                                    </td>

                                    <td align="center">


                                        <asp:DropDownList ID="DropDownList49" runat="server">
                                        </asp:DropDownList>

                                    </td>
                                    <td align="center" style='width: 100px;'>

                                        <asp:TextBox ID="TextBox149" runat="server" Width="100px"></asp:TextBox>

                                    </td>
                                    <td align="center">

                                        <asp:TextBox ID="TextBox150" runat="server" Width="80px"></asp:TextBox>


                                    </td>
                                    <td align="center">

                                        <asp:TextBox ID="TextBox151" runat="server" Width="40px"></asp:TextBox>


                                    </td>

                                    <td align="center">

                                        <asp:TextBox ID="TextBox152" runat="server" Width="150px"></asp:TextBox>


                                    </td>

                                    <td align="center">

                                        <asp:TextBox ID="TextBox153" runat="server" Width="150px"></asp:TextBox>


                                    </td>

                                    <td align="center">

                                        <asp:TextBox ID="TextBox154" runat="server" Width="150px"></asp:TextBox>


                                    </td>
                                    <td align="center">

                                        <asp:TextBox ID="TextBox155" runat="server" Width="150px"></asp:TextBox>


                                    </td>

                                    <td align="center">

                                        <asp:TextBox ID="TextBox156" runat="server" Width="150px"></asp:TextBox>


                                    </td>

                                    <td align="center">

                                        <asp:TextBox ID="TextBox157" runat="server" Width="150px"></asp:TextBox>


                                    </td>
                                </tr>

                                <tr>
                                    <td align="center" style='width: 100px;'>

                                        <asp:TextBox ID="TextBox158" runat="server" CssClass="DatepickerReCall" Width="100px"></asp:TextBox>

                                        <cc1:MaskedEditExtender ID="MaskedEditExtender21" runat="server" TargetControlID="TextBox158"
                                        Mask="99/99/9999" MessageValidatorTip="true" MaskType="Date" DisplayMoney="Left"
                                        AcceptNegative="Left" ErrorTooltipEnabled="True" CultureName="en-GB" />
                                    </td>

                                    <td align="center">


                                        <asp:DropDownList ID="DropDownList50" runat="server">
                                        </asp:DropDownList>

                                    </td>
                                    <td align="center" style='width: 100px;'>

                                        <asp:TextBox ID="TextBox160" runat="server" Width="100px"></asp:TextBox>

                                    </td>
                                    <td align="center">

                                        <asp:TextBox ID="TextBox161" runat="server" Width="80px"></asp:TextBox>


                                    </td>
                                    <td align="center">

                                        <asp:TextBox ID="TextBox162" runat="server" Width="40px"></asp:TextBox>


                                    </td>

                                    <td align="center">

                                        <asp:TextBox ID="TextBox163" runat="server" Width="150px"></asp:TextBox>


                                    </td>

                                    <td align="center">

                                        <asp:TextBox ID="TextBox164" runat="server" Width="150px"></asp:TextBox>


                                    </td>

                                    <td align="center">

                                        <asp:TextBox ID="TextBox165" runat="server" Width="150px"></asp:TextBox>


                                    </td>
                                    <td align="center">

                                        <asp:TextBox ID="TextBox166" runat="server" Width="150px"></asp:TextBox>


                                    </td>

                                    <td align="center">

                                        <asp:TextBox ID="TextBox167" runat="server" Width="150px"></asp:TextBox>


                                    </td>

                                    <td align="center">

                                        <asp:TextBox ID="TextBox168" runat="server" Width="150px"></asp:TextBox>


                                    </td>
                                </tr>

                                <tr>
                                    <td align="center" style='width: 100px;'>

                                        <asp:TextBox ID="TextBox169" runat="server" CssClass="DatepickerReCall" Width="100px"></asp:TextBox>
                                        <cc1:MaskedEditExtender ID="MaskedEditExtender22" runat="server" TargetControlID="TextBox169"
                                        Mask="99/99/9999" MessageValidatorTip="true" MaskType="Date" DisplayMoney="Left"
                                        AcceptNegative="Left" ErrorTooltipEnabled="True" CultureName="en-GB" />

                                    </td>

                                    <td align="center">


                                        <asp:DropDownList ID="DropDownList51" runat="server">
                                        </asp:DropDownList>

                                    </td>
                                    <td align="center" style='width: 100px;'>

                                        <asp:TextBox ID="TextBox171" runat="server" Width="100px"></asp:TextBox>

                                    </td>
                                    <td align="center">

                                        <asp:TextBox ID="TextBox172" runat="server" Width="80px"></asp:TextBox>


                                    </td>
                                    <td align="center">

                                        <asp:TextBox ID="TextBox173" runat="server" Width="40px"></asp:TextBox>


                                    </td>

                                    <td align="center">

                                        <asp:TextBox ID="TextBox174" runat="server" Width="150px"></asp:TextBox>


                                    </td>

                                    <td align="center">

                                        <asp:TextBox ID="TextBox175" runat="server" Width="150px"></asp:TextBox>


                                    </td>

                                    <td align="center">

                                        <asp:TextBox ID="TextBox176" runat="server" Width="150px"></asp:TextBox>


                                    </td>
                                    <td align="center">

                                        <asp:TextBox ID="TextBox177" runat="server" Width="150px"></asp:TextBox>


                                    </td>

                                    <td align="center">

                                        <asp:TextBox ID="TextBox178" runat="server" Width="150px"></asp:TextBox>


                                    </td>

                                    <td align="center">

                                        <asp:TextBox ID="TextBox179" runat="server" Width="150px"></asp:TextBox>


                                    </td>
                                </tr>

                                <tr>
                                    <td align="center" style='width: 100px;'>

                                        <asp:TextBox ID="TextBox180" runat="server" CssClass="DatepickerReCall" Width="100px"></asp:TextBox>
                                        <cc1:MaskedEditExtender ID="MaskedEditExtender23" runat="server" TargetControlID="TextBox180"
                                        Mask="99/99/9999" MessageValidatorTip="true" MaskType="Date" DisplayMoney="Left"
                                        AcceptNegative="Left" ErrorTooltipEnabled="True" CultureName="en-GB" />

                                    </td>

                                    <td align="center">


                                        <asp:DropDownList ID="DropDownList52" runat="server">
                                        </asp:DropDownList>

                                    </td>
                                    <td align="center" style='width: 100px;'>

                                        <asp:TextBox ID="TextBox182" runat="server" Width="100px"></asp:TextBox>

                                    </td>
                                    <td align="center">

                                        <asp:TextBox ID="TextBox183" runat="server" Width="80px"></asp:TextBox>


                                    </td>
                                    <td align="center">

                                        <asp:TextBox ID="TextBox184" runat="server" Width="40px"></asp:TextBox>


                                    </td>

                                    <td align="center">

                                        <asp:TextBox ID="TextBox185" runat="server" Width="150px"></asp:TextBox>


                                    </td>

                                    <td align="center">

                                        <asp:TextBox ID="TextBox186" runat="server" Width="150px"></asp:TextBox>


                                    </td>

                                    <td align="center">

                                        <asp:TextBox ID="TextBox187" runat="server" Width="150px"></asp:TextBox>


                                    </td>
                                    <td align="center">

                                        <asp:TextBox ID="TextBox188" runat="server" Width="150px"></asp:TextBox>


                                    </td>

                                    <td align="center">

                                        <asp:TextBox ID="TextBox189" runat="server" Width="150px"></asp:TextBox>


                                    </td>

                                    <td align="center">

                                        <asp:TextBox ID="TextBox190" runat="server" Width="150px"></asp:TextBox>


                                    </td>
                                </tr>

                                <tr>
                                    <td colspan="5">
                                        <div class="form-sec-row">
                                            <label><strong>&nbsp;</strong></label>
                                            <asp:Button ID="Button13" runat="server" Text="Submit" CssClass="submit-button"
                                                Height="28px" OnClick="Button13_Click" />
                                            <asp:Button ID="Button14" runat="server" Text="Cancel"
                                                CssClass="submit-button" Height="28px" OnClick="Button14_Click" />
                                            <div class="clear"></div>
                                        </div>
                                    </td>
                                </tr>


                            </table>
                        </div>



                    </asp:View>

                    <asp:View ID="View6" runat="server">
                        <table border="1" cellpadding="0" cellspacing="0" width="100%">
                            <tr>
                                <td>

                                    <div class="pageheader">
                                        <asp:Label ID="Label11" runat="server"> Patient Previous Prescription Report </asp:Label>
                                    </div>


                                    <div class="listing" style='width: 100%; height: 200px; overflow: auto;'>
                                        <asp:GridView ID="GridView5" CssClass="grid" PagerStyle-CssClass="pgr" DataKeyNames="RowId"
                                            runat="server" AutoGenerateColumns="False"
                                            AllowPaging="True" PageSize="50" Width="100%"
                                            SelectedRowStyle-BackColor="GreenYellow" OnPageIndexChanging="GridView5_PageIndexChanging"
                                            OnRowDataBound="GridView5_RowDataBound"
                                            OnRowCancelingEdit="GridView5_RowCancelingEdit"
                                            OnRowDeleting="GridView5_RowDeleting" OnRowEditing="GridView5_RowEditing"
                                            OnRowUpdating="GridView5_RowUpdating">
                                            <RowStyle HorizontalAlign="Center" />
                                            <Columns>


                                                <asp:TemplateField HeaderText="Id" Visible="false">

                                                    <ItemTemplate>
                                                        <asp:Label ID="lblid" runat="server" Text='<%# Bind("RowID") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="Prescription Id">

                                                    <ItemTemplate>
                                                        <asp:Label ID="lblpid" runat="server" Text='<%# Bind("PrescriptionId") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="Date">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lbldate" runat="server" Text='<%# Bind("Date1") %>'></asp:Label>
                                                    </ItemTemplate>


                                                    <EditItemTemplate>
                                                        <asp:TextBox ID="txtdate" runat="server" Text='<%# Bind("Date1") %>'></asp:TextBox>
                                                    </EditItemTemplate>
                                                </asp:TemplateField>




                                                <asp:TemplateField HeaderText="Group Name">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblgroup" runat="server" Text='<%# Bind("MedicineGroupName") %>'></asp:Label>
                                                    </ItemTemplate>


                                                    <EditItemTemplate>
                                                        <asp:DropDownList ID="ddlgroup" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlgroup_SelectedIndexChanged">
                                                        </asp:DropDownList>
                                                    </EditItemTemplate>
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="Sub Group Name">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblsubgroup" runat="server" Text='<%# Bind("SubGrName") %>'></asp:Label>
                                                    </ItemTemplate>

                                                    <EditItemTemplate>
                                                        <asp:DropDownList ID="ddlsub" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlsub_SelectedIndexChanged">
                                                        </asp:DropDownList>
                                                    </EditItemTemplate>

                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="Medicine">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblmedicine" runat="server" Text='<%# Bind("MedicineName") %>'></asp:Label>
                                                    </ItemTemplate>

                                                    <EditItemTemplate>
                                                        <asp:DropDownList ID="ddlmed" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlmed_SelectedIndexChanged">
                                                        </asp:DropDownList>
                                                    </EditItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Generic Name">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblGenNameItem" runat="server" Text='<%# Bind("GenericName") %>'></asp:Label>
                                                    </ItemTemplate>


                                                    <EditItemTemplate>
                                                        <asp:Label ID="lblGenNameEdit" runat="server" Text='<%# Bind("GenericName") %>'></asp:Label>
                                                    </EditItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Dose">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lbldose" runat="server" Text='<%# Bind("Dose") %>'></asp:Label>
                                                    </ItemTemplate>


                                                    <EditItemTemplate>
                                                        <asp:TextBox ID="txtdose" runat="server" Text='<%# Bind("Dose") %>'></asp:TextBox>
                                                    </EditItemTemplate>
                                                </asp:TemplateField>

                                                <asp:CommandField HeaderText="Edit" ShowEditButton="True" />
                                                <asp:CommandField HeaderText="Delete" ShowDeleteButton="True" />
                                            </Columns>
                                            <PagerStyle CssClass="pgr" />
                                            <EditRowStyle BackColor="#CCFF33" />
                                            <AlternatingRowStyle BackColor="#FFDB91" />
                                            <SelectedRowStyle BackColor="GreenYellow" />
                                        </asp:GridView>
                                    </div>

                                </td>
                            </tr>
                        </table>
                        <table border="1" cellpadding="0" cellspacing="0" width="100%">
                            <tr>
                                <td colspan="4">
                                    <div class="pageheader">
                                        <asp:Label ID="Label12" runat="server"> Patient New Prescription</asp:Label>
                                    </div>
                                </td>
                            </tr>
                        </table>
                        <asp:Panel ID="Panel1" runat="server">

                            <table border="1" cellpadding="0" style='background-color: #C5B9B9;' cellspacing="0" width="100%">
                                <tr>

                                    <td><strong>Template Group:</strong>
                                        <asp:DropDownList ID="DropDownList32" runat="server" AutoPostBack="True"
                                            Width="180px" OnSelectedIndexChanged="DropDownList32_SelectedIndexChanged">
                                        </asp:DropDownList>
                                    </td>
                                    <td><strong>Template :</strong>
                                        <asp:DropDownList ID="DropDownList31" runat="server" AutoPostBack="True" Width="180px"
                                            OnSelectedIndexChanged="DropDownList31_SelectedIndexChanged">
                                        </asp:DropDownList>
                                    </td>
                                </tr>
                            </table>

                        </asp:Panel>



                        <asp:Panel ID="Panel2" runat="server">


                            <table border="1" cellpadding="0" style='background-color: #C5B9B9;' cellspacing="0" width="100%">
                                <tr>
                                    <td><strong>Prescription No :</strong>
                                        <asp:TextBox ID="TextBox201" runat="server" Enabled="False"></asp:TextBox>
                                    </td>
                                    <td><strong>Date :</strong>
                                        <asp:TextBox ID="TextBox202" CssClass="DatepickerReCall" Width="125px" runat="server"></asp:TextBox>
                                    </td>
                                    <td><strong>Name :</strong>
                                        <asp:TextBox ID="TextBox203" runat="server" Enabled="False"></asp:TextBox>
                                    </td>

                                </tr>
                            </table>


                            <table border="1" cellpadding="0" cellspacing="0" width="100%">
                                <tr style='background-color: #FF9300;'>
                                    <td align="center"  width="20%">

                                        <label><strong>Group</strong></label>

                                    </td>

                                    <td align="center" width="20%">

                                        <label><strong>Sub Group</strong></label>

                                    </td>
                                    <td align="center" width="50%">

                                        <label><strong>Medicine </strong></label>

                                    </td>
                                    <td align="center" width="20%">

                                        <label><strong>Generic Name </strong></label>

                                    </td>
                                    <td align="center" width="15%">

                                        <label><strong>Dose </strong></label>

                                    </td>

                                </tr>

                                <tr>
                                    <td align="left">
                                        <asp:DropDownList ID="DropDownList1" runat="server" AutoPostBack="True" Width="95%"
                                            OnSelectedIndexChanged="DropDownList1_SelectedIndexChanged">
                                        </asp:DropDownList>



                                    </td>

                                    <td align="left">


                                        <asp:DropDownList ID="DropDownList2" runat="server" AutoPostBack="True"  Width="95%"
                                            OnSelectedIndexChanged="DropDownList2_SelectedIndexChanged">
                                        </asp:DropDownList>

                                    </td>
                                    <td align="left">

                                        <asp:DropDownList ID="DropDownList3" runat="server" AutoPostBack="True"  Width="95%"
                                            OnSelectedIndexChanged="DropDownList3_SelectedIndexChanged">
                                        </asp:DropDownList>

                                    </td>
                                    <td align="left">

                                        <asp:TextBox ID="txtGenname1" runat="server"></asp:TextBox>

                                    </td>
                                    <td align="left">

                                        <asp:TextBox ID="TextBox191" runat="server"></asp:TextBox>

                                    </td>

                                </tr>

                                <tr>
                                    <td align="left">
                                        <asp:DropDownList ID="DropDownList4" runat="server" AutoPostBack="True"  Width="95%"
                                            OnSelectedIndexChanged="DropDownList4_SelectedIndexChanged">
                                        </asp:DropDownList>



                                    </td>

                                    <td align="left">


                                        <asp:DropDownList ID="DropDownList5" runat="server" AutoPostBack="True"  Width="95%"
                                            OnSelectedIndexChanged="DropDownList5_SelectedIndexChanged">
                                        </asp:DropDownList>

                                    </td>
                                    <td align="left">

                                        <asp:DropDownList ID="DropDownList6" runat="server" AutoPostBack="True"  Width="95%"
                                            OnSelectedIndexChanged="DropDownList6_SelectedIndexChanged">
                                        </asp:DropDownList>

                                    </td>
                                    <td align="left">

                                        <asp:TextBox ID="txtGenname2" runat="server"></asp:TextBox>

                                    </td>
                                    <td align="left">

                                        <asp:TextBox ID="TextBox192" runat="server"></asp:TextBox>

                                    </td>

                                </tr>

                                <tr>
                                    <td align="left">
                                        <asp:DropDownList ID="DropDownList7" runat="server" AutoPostBack="True"  Width="95%"
                                            OnSelectedIndexChanged="DropDownList7_SelectedIndexChanged">
                                        </asp:DropDownList>



                                    </td>

                                    <td align="left">


                                        <asp:DropDownList ID="DropDownList8" runat="server" AutoPostBack="True"  Width="95%"
                                            OnSelectedIndexChanged="DropDownList8_SelectedIndexChanged">
                                        </asp:DropDownList>

                                    </td>
                                    <td align="left">

                                        <asp:DropDownList ID="DropDownList9" runat="server" AutoPostBack="True"  Width="95%"
                                            OnSelectedIndexChanged="DropDownList9_SelectedIndexChanged">
                                        </asp:DropDownList>

                                    </td>
                                    <td align="left">

                                        <asp:TextBox ID="txtGenname3" runat="server"></asp:TextBox>

                                    </td>
                                    <td align="left">

                                        <asp:TextBox ID="TextBox193" runat="server"></asp:TextBox>

                                    </td>

                                </tr>

                                <tr>
                                    <td align="left">
                                        <asp:DropDownList ID="DropDownList10" runat="server" AutoPostBack="True"  Width="95%"
                                            OnSelectedIndexChanged="DropDownList10_SelectedIndexChanged">
                                        </asp:DropDownList>



                                    </td>

                                    <td align="left">


                                        <asp:DropDownList ID="DropDownList11" runat="server" AutoPostBack="True"  Width="95%"
                                            OnSelectedIndexChanged="DropDownList11_SelectedIndexChanged">
                                        </asp:DropDownList>

                                    </td>
                                    <td align="left">

                                        <asp:DropDownList ID="DropDownList12" runat="server" AutoPostBack="True"  Width="95%"
                                            OnSelectedIndexChanged="DropDownList12_SelectedIndexChanged">
                                        </asp:DropDownList>

                                    </td>
                                    <td align="left">

                                        <asp:TextBox ID="txtGenname4" runat="server"></asp:TextBox>

                                    </td>
                                    <td align="left">

                                        <asp:TextBox ID="TextBox194" runat="server"></asp:TextBox>

                                    </td>

                                </tr>

                                <tr>
                                    <td align="left">
                                        <asp:DropDownList ID="DropDownList13" runat="server" AutoPostBack="True"  Width="95%"
                                            OnSelectedIndexChanged="DropDownList13_SelectedIndexChanged">
                                        </asp:DropDownList>



                                    </td>

                                    <td align="left">


                                        <asp:DropDownList ID="DropDownList14" runat="server"  Width="95%"
                                            OnSelectedIndexChanged="DropDownList14_SelectedIndexChanged">
                                        </asp:DropDownList>

                                    </td>
                                    <td align="left">

                                        <asp:DropDownList ID="DropDownList15" runat="server" AutoPostBack="True"  Width="95%"
                                            OnSelectedIndexChanged="DropDownList15_SelectedIndexChanged">
                                        </asp:DropDownList>

                                    </td>
                                    <td align="left">

                                        <asp:TextBox ID="txtGenname5" runat="server"></asp:TextBox>

                                    </td>
                                    <td align="left">

                                        <asp:TextBox ID="TextBox195" runat="server"></asp:TextBox>

                                    </td>

                                </tr>

                                <tr>
                                    <td align="left">
                                        <asp:DropDownList ID="DropDownList16" runat="server" AutoPostBack="True"  Width="95%"
                                            OnSelectedIndexChanged="DropDownList16_SelectedIndexChanged">
                                        </asp:DropDownList>



                                    </td>

                                    <td align="left">


                                        <asp:DropDownList ID="DropDownList17" runat="server" AutoPostBack="True"  Width="95%"
                                            OnSelectedIndexChanged="DropDownList17_SelectedIndexChanged">
                                        </asp:DropDownList>

                                    </td>
                                    <td align="left">

                                        <asp:DropDownList ID="DropDownList18" runat="server" AutoPostBack="True"  Width="95%"
                                            OnSelectedIndexChanged="DropDownList18_SelectedIndexChanged">
                                        </asp:DropDownList>

                                    </td>
                                    <td align="left">

                                        <asp:TextBox ID="txtGenname6" runat="server"></asp:TextBox>

                                    </td>
                                    <td align="left">

                                        <asp:TextBox ID="TextBox196" runat="server"></asp:TextBox>

                                    </td>

                                </tr>

                                <tr>
                                    <td align="left">
                                        <asp:DropDownList ID="DropDownList19" runat="server" AutoPostBack="True"  Width="95%"
                                            OnSelectedIndexChanged="DropDownList19_SelectedIndexChanged">
                                        </asp:DropDownList>



                                    </td>

                                    <td align="left">


                                        <asp:DropDownList ID="DropDownList20" runat="server" AutoPostBack="True"  Width="95%"
                                            OnSelectedIndexChanged="DropDownList20_SelectedIndexChanged">
                                        </asp:DropDownList>

                                    </td>
                                    <td align="left">

                                        <asp:DropDownList ID="DropDownList21" runat="server" AutoPostBack="True"  Width="95%"
                                            OnSelectedIndexChanged="DropDownList21_SelectedIndexChanged">
                                        </asp:DropDownList>

                                    </td>
                                    <td align="left">

                                        <asp:TextBox ID="txtGenname7" runat="server"></asp:TextBox>

                                    </td>
                                    <td align="left">

                                        <asp:TextBox ID="TextBox197" runat="server"></asp:TextBox>

                                    </td>

                                </tr>

                                <tr>
                                    <td align="left">
                                        <asp:DropDownList ID="DropDownList22" runat="server" AutoPostBack="True"  Width="95%"
                                            OnSelectedIndexChanged="DropDownList22_SelectedIndexChanged">
                                        </asp:DropDownList>



                                    </td>

                                    <td align="left">


                                        <asp:DropDownList ID="DropDownList23" runat="server" AutoPostBack="True"  Width="95%"
                                            OnSelectedIndexChanged="DropDownList23_SelectedIndexChanged">
                                        </asp:DropDownList>

                                    </td>
                                    <td align="left">

                                        <asp:DropDownList ID="DropDownList24" runat="server" AutoPostBack="True"  Width="95%"
                                            OnSelectedIndexChanged="DropDownList24_SelectedIndexChanged">
                                        </asp:DropDownList>

                                    </td>
                                    <td align="left">

                                        <asp:TextBox ID="txtGenname8" runat="server"></asp:TextBox>

                                    </td>
                                    <td align="left">

                                        <asp:TextBox ID="TextBox198" runat="server"></asp:TextBox>

                                    </td>

                                </tr>

                                <tr>
                                    <td align="left">
                                        <asp:DropDownList ID="DropDownList25" runat="server" AutoPostBack="True"  Width="95%"
                                            OnSelectedIndexChanged="DropDownList25_SelectedIndexChanged">
                                        </asp:DropDownList>



                                    </td>

                                    <td align="left">


                                        <asp:DropDownList ID="DropDownList26" runat="server" AutoPostBack="True"  Width="95%"
                                            OnSelectedIndexChanged="DropDownList26_SelectedIndexChanged">
                                        </asp:DropDownList>

                                    </td>
                                    <td align="left">

                                        <asp:DropDownList ID="DropDownList27" runat="server" AutoPostBack="True"  Width="95%"
                                            OnSelectedIndexChanged="DropDownList27_SelectedIndexChanged">
                                        </asp:DropDownList>

                                    </td>
                                    <td align="left">

                                        <asp:TextBox ID="txtGenname9" runat="server"></asp:TextBox>

                                    </td>
                                    <td align="left">

                                        <asp:TextBox ID="TextBox199" runat="server"></asp:TextBox>

                                    </td>

                                </tr>

                                <tr>
                                    <td align="left">
                                        <asp:DropDownList ID="DropDownList28" runat="server" AutoPostBack="True"  Width="95%"
                                            OnSelectedIndexChanged="DropDownList28_SelectedIndexChanged">
                                        </asp:DropDownList>



                                    </td>

                                    <td align="left">


                                        <asp:DropDownList ID="DropDownList29" runat="server" AutoPostBack="True"  Width="95%"
                                            OnSelectedIndexChanged="DropDownList29_SelectedIndexChanged">
                                        </asp:DropDownList>

                                    </td>
                                    <td align="left">

                                        <asp:DropDownList ID="DropDownList30" runat="server" AutoPostBack="True"  Width="95%"
                                            OnSelectedIndexChanged="DropDownList30_SelectedIndexChanged">
                                        </asp:DropDownList>

                                    </td>
                                    <td align="left">

                                        <asp:TextBox ID="txtGenname10" runat="server"></asp:TextBox>

                                    </td>
                                    <td align="left">

                                        <asp:TextBox ID="TextBox200" runat="server"></asp:TextBox>

                                    </td>

                                </tr>
                                <tr>
                                    <td align="left"><strong>Advice :</strong></td>
                                    <td colspan="3">
                                        <asp:TextBox ID="TextBox204" TextMode="MultiLine" Width="100%" Height="45px" runat="server"></asp:TextBox></td>
                                </tr>
                                <tr>
                                    <td colspan="4">
                                        <div class="form-sec-row">
                                            <label><strong>&nbsp;</strong></label>
                                            <asp:Button ID="Button15" runat="server" Text="Submit" CssClass="submit-button"
                                                Height="28px" OnClick="Button15_Click" />
                                            <asp:Button ID="Button16" runat="server" Text="Cancel"
                                                CssClass="submit-button" Height="28px" OnClick="Button16_Click" />
                                            <asp:Button ID="Button17" runat="server" Text="Get Prescription"
                                                CssClass="submit-buttonCheck" Height="28px" OnClick="Button17_Click" />
                                            <div class="clear"></div>
                                        </div>
                                    </td>
                                </tr>
                            </table>


                            <table width="50%">
                                <tr>
                                    <td>
                                        <div id='mydiv'>
                                            <asp:Literal ID="ltrReport" runat="server"></asp:Literal>
                                        </div>
                                    </td>
                                </tr>
                                <tr>
                                    <td>

                                        <input type="button" value="Back" style="width: 70px; font-size: x-small" onclick="window.history.back()" />
                                        <input type="button" id="cmdPrint" value="Print" style="width: 70px; font-size: x-small" onclick="javascript: printDiv('mydiv')" /></td>
                                </tr>
                            </table>
                        </asp:Panel>
                    </asp:View>

                    <asp:View ID="View7" runat="server">

                        <div class="form-sec-row">
                            <label><strong>Date :</strong></label>
                            <asp:TextBox ID="txtdate" CssClass="textbox-medium1" runat="server"></asp:TextBox>
                            <div class="clear"></div>
                        </div>


                        <div class="form-sec-row">
                            <label><strong>Group Name :</strong></label>
                            <asp:DropDownList ID="DropDownList53" runat="server" CssClass="combo-big1"
                                AutoPostBack="True"
                                OnSelectedIndexChanged="DropDownList53_SelectedIndexChanged">
                            </asp:DropDownList>
                            <div class="clear"></div>
                        </div>
                        <div class="form-sec-row">
                            <label><strong>Sub Group Name :</strong></label>
                            <asp:DropDownList ID="DropDownList54" runat="server" CssClass="combo-big1"
                                AutoPostBack="True"
                                OnSelectedIndexChanged="DropDownList54_SelectedIndexChanged">
                            </asp:DropDownList>
                            <div class="clear"></div>
                        </div>
                        <div class="form-sec-row">
                            <label><strong>USG Name :</strong></label>
                            <asp:DropDownList ID="DropDownList55" runat="server" CssClass="combo-big1"
                                AutoPostBack="True"
                                OnSelectedIndexChanged="DropDownList55_SelectedIndexChanged">
                            </asp:DropDownList>
                            <div class="clear"></div>
                        </div>

                        <asp:GridView ID="Gridview11" runat="server" CssClass="grid"
                            PagerStyle-CssClass="pgr" AutoGenerateColumns="false" Width="100%">
                            <Columns>
                                <asp:TemplateField HeaderText="ID" Visible="false">
                                    <ItemTemplate>
                                        <asp:Label ID="lblID" runat="server" Text='<%# Eval("ID")%>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Header Name">
                                    <ItemTemplate>
                                        <asp:Label ID="lblTestName" runat="server" Text='<%# Eval("HeaderName")%>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Header Content">
                                    <ItemTemplate>
                                        <asp:TextBox ID="txtheaderContent" runat="server" TextMode="MultiLine" Text='<%# Eval("HeaderContent")%>' Height="100px" Width="300px"></asp:TextBox>
                                    </ItemTemplate>
                                </asp:TemplateField>
                            </Columns>

                        </asp:GridView>


                        <asp:GridView ID="Gridview12" runat="server" CssClass="grid"
                            PagerStyle-CssClass="pgr" AutoGenerateColumns="false" Width="100%">
                            <Columns>
                                <asp:TemplateField HeaderText="ID" Visible="false">
                                    <ItemTemplate>
                                        <asp:Label ID="lblID" runat="server" Text='<%# Eval("Id")%>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Parameter Name">
                                    <ItemTemplate>
                                        <asp:Label ID="lblTestName" runat="server" Text='<%# Eval("Parameter")%>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Value">
                                    <ItemTemplate>
                                        <asp:TextBox ID="txtval" runat="server"></asp:TextBox>
                                    </ItemTemplate>
                                </asp:TemplateField>
                            </Columns>

                        </asp:GridView>


                        <div class="form-sec-row">
                            <label><strong>&nbsp;</strong></label>
                            <asp:Button ID="Button20" runat="server" Text="Submit" CssClass="submit-button"
                                Height="28px" OnClick="Button20_Click" />
                            <asp:Button ID="Button21" runat="server" Text="Cancel"
                                CssClass="submit-button" Height="28px" OnClick="Button21_Click" />
                            <asp:Button ID="Button22" runat="server" Text="Show Details"
                                CssClass="submit-buttonCheck" Height="28px" OnClientClick="ShowDialog1()" />

                            <div class="clear"></div>
                        </div>
                    </asp:View>

                    <asp:View ID="View8" runat="server">
                        <table border="1" cellpadding="0" cellspacing="0" width="100%">
                            <tr>
                                <td>

                                    <div class="pageheader">
                                        <asp:Label ID="Label15" runat="server"> Operation Notes </asp:Label>
                                    </div>


                                </td>
                            </tr>
                            <tr>
                                <td>

                                    <div class="pageheader">
                                        <asp:TextBox ID="TextBox205" runat="server" CssClass="textbox-medium1" TextMode="MultiLine" Height="125px" Width="100%"></asp:TextBox>
                                    </div>


                                </td>
                            </tr>

                            <tr>
                                <td>
                                    <div class="form-sec-row">
                                        <label><strong>&nbsp;</strong></label>
                                        <asp:Button ID="Button18" runat="server" Text="Submit" CssClass="submit-button"
                                            Height="28px" OnClick="Button18_Click" />
                                        <asp:Button ID="Button19" runat="server" Text="Cancel"
                                            CssClass="submit-button" Height="28px" OnClick="Button19_Click" />

                                        <div class="clear"></div>
                                    </div>
                                </td>
                            </tr>
                        </table>
                    </asp:View>

                    <asp:View ID="View9" runat="server">
                        <table border="1" cellpadding="0" cellspacing="0" width="100%">
                            <tr>
                                <td style="width:100%;">
                                    <div class="pageheader">
                                        <asp:Label ID="Label14" runat="server"> Patient Previous Diagnosis </asp:Label>
                                    </div>
                                    <div class="listing" style='width: 100%; height: 150px; overflow: auto;'>
                                        <asp:GridView ID="GridView6" CssClass="grid" PagerStyle-CssClass="pgr" DataKeyNames="AppointmentNo"
                                            runat="server" AutoGenerateColumns="False"
                                            AllowPaging="True" PageSize="50" Width="100%"
                                            SelectedRowStyle-BackColor="GreenYellow" OnPageIndexChanging="GridView6_PageIndexChanging"
                                             OnRowCommand="GridView6_RowCommand">
                                            <RowStyle HorizontalAlign="Center" />
                                            <Columns>
                                                <asp:TemplateField HeaderText="Date" ItemStyle-Width="10%" ItemStyle-HorizontalAlign="Left">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lbldate" runat="server" Text='<%# Bind("Date") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="Appointment No" ItemStyle-Width="15%" ItemStyle-HorizontalAlign="Left">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblAppNo" runat="server" Text='<%# Bind("AppointmentNo") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    
                                                </asp:TemplateField>




                                                <asp:TemplateField HeaderText="Diagnosis" ItemStyle-Width="70%" ItemStyle-HorizontalAlign="Left">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblDiagnosis" runat="server" Text='<%# Bind("Diagnosis") %>'></asp:Label>
                                                    </ItemTemplate>


                                                </asp:TemplateField>

                                                <asp:CommandField SelectText="Edit" ShowSelectButton="True" ControlStyle-CssClass="temp"  HeaderText="Edit" SelectImageUrl="~/Images/edit.png" ButtonType="Image">
                        <ControlStyle CssClass="temp"></ControlStyle>
                                                    </asp:CommandField>

                                            </Columns>
                                            <PagerStyle CssClass="pgr" />
                                            <EditRowStyle BackColor="#CCFF33" />
                                            <AlternatingRowStyle BackColor="#FFDB91" />
                                            <SelectedRowStyle BackColor="GreenYellow" />

                                        </asp:GridView>
                                    </div>
                                </td>
                            </tr>
                        </table>
                        
                        <table border="1" cellpadding="0" cellspacing="0" width="100%">
                            <tr>
                                <td>
                                    <div class="pageheader">
                                        <asp:Label ID="Label13" runat="server"> Diagnosis </asp:Label>
                                        <asp:Label ID="lblAppNoOld" Visible="false" runat="server"></asp:Label>
                                    </div>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <div class="form-sec-row">
                                        <label><strong>Date :</strong></label>
                                        <asp:TextBox ID="txtDiagDate" runat="server" CssClass="textbox-medium1" ReadOnly="true"></asp:TextBox>
                                            <cc1:MaskedEditExtender ID="MaskedEditExtender2" runat="server" TargetControlID="txtDiagDate"
                                            Mask="99/99/9999" MessageValidatorTip="true" MaskType="Date" DisplayMoney="Left"
                                            AcceptNegative="Left" ErrorTooltipEnabled="True" CultureName="en-GB" />
                                             <asp:Label ID="Label16" runat="server" ForeColor="Green" Text="(DD/MM/YYYY)"></asp:Label>
                                    </div>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <div class="pageheader">
                                        <asp:TextBox ID="txtDiagnosis" runat="server" CssClass="textbox-medium1" TextMode="MultiLine" Height="125px" Width="100%"></asp:TextBox>
                                    </div>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <div class="form-sec-row">
                                        <label><strong>&nbsp;</strong></label>
                                        <asp:Button ID="btnSaveDiagnosis" runat="server" Text="Submit" CssClass="submit-button" Height="28px" OnClick="btnSaveDiagnosis_Click" />
                                        <asp:Button ID="btnCancelDiagnosis" runat="server" Text="Cancel" CssClass="submit-button" Height="28px" OnClick="btnCancelDiagnosis_Click" />
                                        <div class="clear"></div>
                                    </div>
                                </td>
                            </tr>
                        </table>
                    </asp:View>
                </asp:MultiView>

            </div>
        </ContentTemplate>
    </asp:UpdatePanel>

</asp:Content>

