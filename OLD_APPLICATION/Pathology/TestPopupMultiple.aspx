<%@ Page Language="C#" AutoEventWireup="true" CodeFile="TestPopupMultiple.aspx.cs" Inherits="Pathology_TestPopupMultiple" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>
    <link href="../css/style.css" rel="stylesheet" type="text/css" />
    <script src="../Script/timeout-dialog.js" type="text/javascript"></script>
    <script src="../Script/snow.js" type="text/javascript"></script>
    <script src="../Script/menu.js" type="text/javascript"></script>
    <script src="../Script/jquery.ui.timepicker.js" type="text/javascript"></script>
    <script src="../Script/jquery.min.js" type="text/javascript"></script>
    <script src="../Script/jquery.js" type="text/javascript"></script>
    <script src="../Script/jquery.idle-timer.js" type="text/javascript"></script>
    <script src="../Script/jquery.dynDateTime.min.js" type="text/javascript"></script>
    <script src="../Script/Jquery.corner.js" type="text/javascript"></script>
    <script src="../Script/jquery-ui-1.8.19.custom.min.js" type="text/javascript"></script>
    <script src="../Script/jquery-1.8.2.min.js" type="text/javascript"></script>
    <script src="../Script/jquery-1.7.2.min.js" type="text/javascript"></script>
    <script src="../Script/jquery-1.4.1.min.js" type="text/javascript"></script>
    <script src="../Script/jquery-1.4.1.js" type="text/javascript"></script>
    <script src="../Script/jquery-1.4.1-vsdoc.js" type="text/javascript"></script>
    <script src="../Script/jquery-1.3.2.min.js" type="text/javascript"></script>
    <script src="../Script/datetimepicker.js" type="text/javascript"></script>
    <script src="../Script/calendar-en.min.js" type="text/javascript"></script>
    <link href="../css/calendar-blue.css" rel="stylesheet" type="text/css" />
    <link href="../css/jquery-ui-1.8.19.custom.css" rel="stylesheet" type="text/css" />
    <link href="../css/jquery-ui.css" rel="stylesheet" type="text/css" />
    <link href="../css/jquery.timepicker.css" rel="stylesheet" type="text/css" />
    <script src="../css/jquery.timepicker.js" type="text/javascript"></script>
    <script src="../css/jquery.timepicker.min.js" type="text/javascript"></script>
    <link href="../css/jquery.ui.timepicker.css" rel="stylesheet" type="text/css" />

    <link href="../css/MyCss.css" rel="stylesheet" type="text/css" />
    <link type="text/css" href="css/jquery.ui.timepicker.css" rel="stylesheet" />
    <link type="text/css" href="css/calendar-blue.css" rel="stylesheet" />

    <script src="Script/jquery-1.4.1.min.js" type="text/javascript"></script>
    <script src="Script/jquery.dynDateTime.min.js" type="text/javascript"></script>
    <script src="Script/calendar-en.min.js" type="text/javascript"></script>




    <script src="/js/jquery-1.9.1.js" type="text/javascript"></script>
    <link href="/js/jquery-ui.css" rel="stylesheet" type="text/css" />
    <script src="/js/jquery-ui.js" type="text/javascript"></script>

    <script type="text/javascript" src="Script/jquery-ui-1.8.19.custom.min.js"></script>
    <script type="text/javascript" src="../Script/menu.js"></script>
    <script type="text/javascript" src="../Script/jquery.ui.timepicker.js"></script>
    <script src="Script/jquery-1.3.1.js" type="text/javascript"></script>

    <script type="text/javascript">



        function Calling() {
            var date = new Date();
            $("input[id$='txttestdate']").datepicker({ dateFormat: 'dd/mm/yy' });

            $("input[id$='txtdeldate']").datepicker({ dateFormat: 'dd/mm/yy' });
        }

        function CloseDialog() {
            debugger;
            var arg = new Object();
            arg.NameValue = document.getElementById('HiddenField1').value;
            arg.ProfessionValue = document.getElementById('HiddenField2').value;
            window.returnValue = arg;
            var x = document.getElementById('HiddenField3').value;
            var c = document.getElementById('HiddenField4').value;
            var nm = document.getElementById('HiddenField5').value;
            var a = arg.ProfessionValue.split("#");
            //window.opener.ParentPageFunctionName(x);
            //window.opener.document.getElementById("ctl00_ContentPlaceHolder1_txttestcode").value = arg.NameValue
            //window.opener.document.getElementById("ctl00_ContentPlaceHolder1_txttestname").value = a[0];
            //window.opener.document.getElementById("ctl00_ContentPlaceHolder1_txtcost").value = a[1];
            //window.opener.document.getElementById("ctl00_ContentPlaceHolder1_txtdueamount").value = a[2];

            window.opener.document.getElementById("txttestcode").value = arg.NameValue
            window.opener.document.getElementById("txttestname").value = a[0];
            window.opener.document.getElementById("txtcost").value = a[1];
            window.opener.document.getElementById("ctl00_ContentPlaceHolder1_txtPayableAmt").value = a[1];
            window.opener.document.getElementById("txtdueamount").value = a[2];
            window.opener.document.getElementById("txtconsultant").value = c;
            window.opener.document.getElementById("txtconsultantname").value = nm;
            window.close();
        }

        function Button1_onclick() {
            window.close();
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
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <asp:ScriptManager ID="ScriptManager1" runat="server">
            </asp:ScriptManager>


            <%--For Busy Loader .............................--%>

            <%-- <asp:UpdateProgress ID="UpdateProgress1" runat="server">
    <ProgressTemplate>
    
       <div id="aaa" class="progressBackgroundFilter">          </div>
        <div id="bbb" class="processMessage"><img alt="Loading" src="../images/pwait.gif"/>
      
              </div>
    </ProgressTemplate>
    </asp:UpdateProgress>--%>


            <%--For Busy Loader End.............................--%>


            <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                <ContentTemplate>
                    <table>

                        <tr>
                            <td>
                                <div class="form-sec-row">
                                    <label class="pname"><strong>Investigation Group :</strong></label>
                                    <asp:DropDownList ID="ddltestGroup" runat="server" CssClass="textbox-medium1">
                                    </asp:DropDownList>
                                </div>
                            </td>

                            <td>
                                <div class="form-sec-row">
                                    <label class="pname"><strong>Investigation Code :</strong></label>
                                    <asp:TextBox ID="txtcode" runat="server" CssClass="textbox-medium1" Width="138px"></asp:TextBox>
                                </div>
                            </td>
                            <td>
                                <div class="form-sec-row">
                                    <label class="pname"><strong>Investigation Name :</strong></label>
                                    <asp:TextBox ID="txtname" runat="server" CssClass="textbox-medium1" Width="138px"></asp:TextBox>
                                </div>
                            </td>
                            <td style="display:none;">
                                <div class="form-sec-row">
                                    <label class="pname"><strong>Department :</strong></label>
                                    <asp:DropDownList ID="DropDownList1" runat="server" CssClass="textbox-mediumpatcover">
                                    </asp:DropDownList>
                                </div>
                            </td>

                            <td>
                                <div class="form-sec-row">
                                    <asp:Button ID="Button2" runat="server" Text="Search" CssClass="submit-button1" Height="28px"
                                        OnClick="Button2_Click" />
                                    <br />
                                </div>
                            </td>

                        </tr>

                        <tr align="center">
                            <td colspan="4">
                                <div style='width: 100%; height: 400px; overflow: auto;'>
                                    <asp:GridView ID="GridView_popup" CssClass="grid" PagerStyle-CssClass="pgr" DataKeyNames="TestId"
                                        runat="server" AutoGenerateColumns="False" AllowPaging="True" PageSize="100"
                                        Width="100%" BackColor="LightGoldenrodYellow" BorderColor="Tan" BorderWidth="1px"
                                        CellPadding="2" ForeColor="Black" GridLines="None"
                                        SelectedRowStyle-BackColor="GreenYellow"
                                        OnRowDataBound="GridView_popup_RowDataBound">

                                        <Columns>
                                            <asp:TemplateField HeaderText="Select">
                                                <ItemTemplate>
                                                    <asp:CheckBox ID="CheckBox1" runat="server" />
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Test Code">

                                                <ItemTemplate>
                                                    <asp:Label ID="lblid" runat="server" Text='<%# Bind("TestId") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Test Name">

                                                <ItemTemplate>
                                                    <asp:Label ID="lblName" runat="server" Text='<%# Bind("TestName") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Cost">

                                                <ItemTemplate>
                                                    <asp:Label ID="lblcost" runat="server" Text='<%# Bind("Cost") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Consultant">

                                                <ItemTemplate>
                                                    <asp:DropDownList ID="ddlconsult" runat="server" Width="98%"></asp:DropDownList>
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                        </Columns>
                                        <PagerStyle CssClass="pgr" />
                                        <EditRowStyle BackColor="#CCFF33" />
                                        <AlternatingRowStyle BackColor="#FFDB91" />
                                        <SelectedRowStyle BackColor="GreenYellow" />
                                    </asp:GridView>
                                </div>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="4">
                                <b>Test Date:</b><asp:TextBox ID="txttestdate" runat="server" CssClass="textbox-medium2"></asp:TextBox>
                                <b>Delivery Date:</b><asp:TextBox ID="txtdeldate" runat="server" CssClass="textbox-medium2"></asp:TextBox>

                                <asp:Button ID="Button4" runat="server" Text="Add" CssClass="submit-button"
                                    OnClick="Button4_Click" />
                            </td>
                        </tr>
                        <tr align="center">
                            <td colspan="4">
                                <div style='width: 100%; height: 250px; overflow: auto;'>

                                    <asp:GridView ID="GridView1" CssClass="grid" PagerStyle-CssClass="pgr" DataKeyNames="TestId"
                                        runat="server" AutoGenerateColumns="False" AllowPaging="True" PageSize="100"
                                        Width="100%" BackColor="LightGoldenrodYellow" BorderColor="Tan" BorderWidth="1px"
                                        CellPadding="2" ForeColor="Black" GridLines="None"
                                        SelectedRowStyle-BackColor="GreenYellow"
                                        OnRowDeleting="GridView1_RowDeleting"
                                        OnRowCancelingEdit="GridView1_RowCancelingEdit"
                                        OnRowEditing="GridView1_RowEditing" OnRowUpdating="GridView1_RowUpdating"
                                        OnRowDataBound="GridView1_RowDataBound">

                                        <Columns>
                                            <asp:TemplateField HeaderText="Select" Visible="false">
                                                <ItemTemplate>
                                                    <asp:CheckBox ID="CheckBox1" runat="server" />
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Serial No">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblSerial" runat="server"></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Test Code">

                                                <ItemTemplate>
                                                    <asp:Label ID="lblid" runat="server" Text='<%# Bind("TestId") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Test Req Id">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblTestReqNo" runat="server" Text='<%# Bind("TestReqNo") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>


                                            <asp:TemplateField HeaderText="Test Name">

                                                <ItemTemplate>
                                                    <asp:Label ID="lblName" runat="server" Text='<%# Bind("TestName") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Cost">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblcost" runat="server" Text='<%# Bind("Cost") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Date">

                                                <ItemTemplate>
                                                    <asp:Label ID="lblDate" runat="server" Text='<%# Bind("Date") %>'></asp:Label>
                                                </ItemTemplate>
                                                <EditItemTemplate>
                                                    <asp:TextBox ID="txtdate" runat="server" Width="85px" Text='<%# Bind("Date") %>'></asp:TextBox>
                                                </EditItemTemplate>

                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Time">

                                                <ItemTemplate>
                                                    <asp:Label ID="lblTime" runat="server" Text='<%# Bind("Time") %>'></asp:Label>
                                                </ItemTemplate>

                                                <EditItemTemplate>
                                                    <asp:TextBox ID="txtTime" runat="server" Width="85px" Text='<%# Bind("Time") %>'></asp:TextBox>
                                                </EditItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Delivery Date">

                                                <ItemTemplate>
                                                    <asp:Label ID="lbldvdate" runat="server" Text='<%# Bind("DeliveryDate") %>'></asp:Label>
                                                </ItemTemplate>

                                                <EditItemTemplate>
                                                    <asp:TextBox ID="txtdvdate" runat="server" Width="85px" Text='<%# Bind("DeliveryDate") %>'></asp:TextBox>
                                                </EditItemTemplate>
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Remarks">

                                                <ItemTemplate>
                                                    <asp:Label ID="lblRemarks" Width="120px" runat="server" Text='<%# Bind("Remarks") %>'></asp:Label>
                                                </ItemTemplate>

                                                <EditItemTemplate>
                                                    <asp:TextBox ID="txtRemarks" runat="server" TextMode="MultiLine" Text='<%# Bind("Remarks") %>'></asp:TextBox>
                                                </EditItemTemplate>
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Performing Doc">

                                                <ItemTemplate>
                                                    <asp:Label ID="lbconsultant" Width="120px" runat="server" Text='<%# Bind("consultant") %>' Visible="false"></asp:Label>
                                                    <asp:Label ID="lbconsultantname" Width="120px" runat="server" Text='<%# Bind("consultantname") %>'></asp:Label>
                                                </ItemTemplate>
                                                <EditItemTemplate>
                                                        <asp:Label ID="lblExistconsultant" Width="120px" runat="server" Text='<%# Bind("consultant") %>' Visible="false"></asp:Label>
                                                       <asp:DropDownList runat="server" ID="ddlExistconsult" Width="98%"></asp:DropDownList>
                                                </EditItemTemplate>
                                            </asp:TemplateField>

                                            <asp:CommandField HeaderText="Edit" ControlStyle-CssClass="temp" ShowEditButton="True" />
                                            <%--<asp:CommandField HeaderText="Delete" ShowDeleteButton="True" />--%>
                                            <asp:CommandField ShowDeleteButton="True" ControlStyle-CssClass="temp" ItemStyle-Width="70px" ItemStyle-HorizontalAlign="Center" HeaderText="Delete" DeleteImageUrl="~/Images/delete.png" ButtonType="Image">
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
                    <table width="100%">
                        <tr>
                            <td align="center">

                                <asp:HiddenField ID="HiddenField1" runat="server" />
                                <asp:HiddenField ID="HiddenField2" runat="server" />
                                <asp:HiddenField ID="HiddenField3" runat="server" />
                                <asp:HiddenField ID="HiddenField4" runat="server" />
                                <asp:HiddenField ID="HiddenField5" runat="server" />
                            </td>
                            <td>
                                <asp:Button ID="Button3" runat="server" Text="OK" CssClass="submit-button"
                                    OnClick="Button3_Click" />

                                <input id="Button1" type="button" value="Cancel" class="submit-button" onclick="return Button1_onclick()" />

                            </td>
                        </tr>
                    </table>
                </ContentTemplate>
            </asp:UpdatePanel>
        </div>
    </form>
</body>
</html>
