<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ExistTestPopup.aspx.cs" Inherits="Pathology_ExistTestPopup" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">


<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
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
        function Button1_onclick() {
            window.close();
        }
    </script>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <asp:ScriptManager ID="ScriptManager1" runat="server">
            </asp:ScriptManager>
        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
            <ContentTemplate>
                <table style="width:100%">
                    <tr>
                        <td  style="width:100%" align="center">
                            <strong>Patient Name : <asp:Label ID="lblptname" runat="server"></asp:Label></strong>
                        </td>
                    </tr>
                    <tr align="center">
                            <td style="width:100%">
                                <div style='width: 100%; height: 250px; overflow: auto;'>

                                    <asp:GridView ID="GridView1" CssClass="grid" PagerStyle-CssClass="pgr" DataKeyNames="TestId"
                                        runat="server" AutoGenerateColumns="False" AllowPaging="True" PageSize="100"
                                        Width="100%" BackColor="LightGoldenrodYellow" BorderColor="Tan" BorderWidth="1px"
                                        CellPadding="2" ForeColor="Black" GridLines="None"
                                        SelectedRowStyle-BackColor="GreenYellow"
                                        OnRowDataBound="GridView1_RowDataBound">

                                        <Columns>
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

                                            <asp:TemplateField HeaderText="Test Req Id" Visible="false">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblTestReqNo" runat="server" Text='<%# Bind("TestReqNo") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>


                                            <asp:TemplateField HeaderText="Service Name">

                                                <ItemTemplate>
                                                    <asp:Label ID="lblName" runat="server" Text='<%# Bind("TestName") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Cost" Visible="false">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblcost" runat="server" Text='<%# Bind("Cost") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Date" Visible="false">

                                                <ItemTemplate>
                                                    <asp:Label ID="lblDate" runat="server" Text='<%# Bind("Date") %>'></asp:Label>
                                                </ItemTemplate>
                                                <EditItemTemplate>
                                                    <asp:TextBox ID="txtdate" runat="server" Width="85px" Text='<%# Bind("Date") %>'></asp:TextBox>
                                                </EditItemTemplate>

                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Time" Visible="false">

                                                <ItemTemplate>
                                                    <asp:Label ID="lblTime" runat="server" Text='<%# Bind("Time") %>'></asp:Label>
                                                </ItemTemplate>

                                                <EditItemTemplate>
                                                    <asp:TextBox ID="txtTime" runat="server" Width="85px" Text='<%# Bind("Time") %>'></asp:TextBox>
                                                </EditItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Delivery Date" Visible="false">

                                                <ItemTemplate>
                                                    <asp:Label ID="lbldvdate" runat="server" Text='<%# Bind("DeliveryDate") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Remarks">

                                                <ItemTemplate>
                                                    <asp:TextBox ID="txtRemarks" runat="server" TextMode="MultiLine" Text='<%# Bind("Remarks") %>'></asp:TextBox>
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Performing Doc">

                                                <ItemTemplate>
                                                    <asp:Label ID="lbconsultant" Width="120px" runat="server" Text='<%# Bind("consultant") %>' Visible="false"></asp:Label>
                                                    <asp:DropDownList runat="server" ID="ddlExistconsult" Width="98%"></asp:DropDownList>
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
                        <td  style="width:100%" align="center">
                            <table width="20%">
                                <tr>
                                    <td>
                                        <asp:Button ID="btn_update" Text="Submit" runat="server" Class="submit-schedule" OnClick="btn_update_Click"/>
                            <input id="Button1" type="button" value="Close" class="submit-button" onclick="return Button1_onclick()" />
                                    </td>
                                </tr>
                            </table>
                            
                        </td>
                    </tr>
                </table>
            </ContentTemplate>
        </asp:UpdatePanel>
    </div>
    </form>
</body>
</html>
