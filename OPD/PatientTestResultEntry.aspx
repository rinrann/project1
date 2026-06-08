<%@ Page Title="" Language="C#" MasterPageFile="~/MasterAll/MasterPageAll.master" AutoEventWireup="true" CodeFile="PatientTestResultEntry.aspx.cs" Inherits="OPD_PatientTestResultEntry" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">

    <script type="text/javascript" language="javascript">



        function DisableBackButton() {
            window.history.forward()
        }
        DisableBackButton();
        window.onload = DisableBackButton;
        window.onpageshow = function (evt) { if (evt.persisted) DisableBackButton() }
        window.onunload = function () { void (0) }

        function printDiv(divID) {
            var divElements = document.getElementById(divID).innerHTML;
            // document.getElementById("ctl00_ContentPlaceHolder1_mydiv").style.width = '900px';
            var oldPage = document.body.innerHTML;
            document.body.innerHTML = "<html><head><title></title></head><body>" + divElements + "</body>";
            window.print();
            document.body.innerHTML = oldPage;
        }

    </script>


</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">


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
            <div id="h1" align="center">
                <div class="pageheader">
                    <asp:Label ID="Label1" runat="server">Patient's Test Result Entry</asp:Label>
                </div>

                <table width="100%">
                    <tr>
                        <td width="7%">
                            <label class="pname"><strong>Test Date :</strong></label>
                        </td>
                        <td width="20%">
                            <asp:TextBox ID="txtdateSrch" runat="server" CssClass="textbox-medium1" Width="100%" TextMode="Date"></asp:TextBox>
                            <div class="clear"></div>
                        </td>
                        <td width="7%">
                            <label class="pname"><strong>Name :</strong></label>

                        </td>
                        <td width="20%">
                            <asp:TextBox ID="txtnameSrch" runat="server" CssClass="textbox-medium1" Width="100%"></asp:TextBox>
                            <div class="clear"></div>
                        </td>
                        <td width="7%">
                            <label class="pname"><strong>Ph No :</strong></label>

                        </td>
                        <td width="10%">
                            <asp:TextBox ID="txtphSrch" runat="server" CssClass="textbox-medium1" Width="100%"></asp:TextBox>
                            <div class="clear"></div>
                        </td>

                        <td width="10%">
                            <div class="form-sec-row">
                                <asp:Button ID="Button11" runat="server" Text="Search" Height="28px" CssClass="submit-button1" OnClick="Button11_Click" />
                            </div>
                        </td>

                    </tr>
                </table>
                <div class="listing" style='width: 100%; height: 300px; overflow: auto;'>
                    <asp:GridView ID="GridView1" CssClass="grid" PagerStyle-CssClass="pgr" DataKeyNames="RequisitionNo" runat="server" AutoGenerateColumns="False"
                        AllowPaging="True" PageSize="100"
                        OnPageIndexChanging="GridView1_PageIndexChanging" SelectedRowStyle-BackColor="GreenYellow"
                        OnRowCommand="GridView1_RowCommand" Width="100%">
                        <RowStyle HorizontalAlign="Center" />
                        <Columns>
                            <asp:TemplateField HeaderText="Registration No">
                                <ItemTemplate>
                                    <asp:Label ID="lblregno" runat="server" Text='<%# Bind("RegistrationNo") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="Requisition No">
                                <ItemTemplate>
                                    <asp:Label ID="lblreqno" runat="server" Text='<%# Bind("RequisitionNo") %>'></asp:Label>
                                    <asp:Label ID="lblvchno" runat="server" Text='<%# Bind("VchNo") %>' Visible="false"></asp:Label>
                                </ItemTemplate>

                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="Patient Name">
                                <ItemTemplate>
                                    <asp:Label ID="lblname" runat="server" Text='<%# Bind("PatientName") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Referal Name">
                                <ItemTemplate>
                                    <asp:Label ID="lblrefname" runat="server" Text='<%# Bind("ReferalName") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="Address">
                                <ItemTemplate>
                                    <asp:Label ID="lbladdress" runat="server" Text='<%# Bind("Address") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="Phone - 1">
                                <ItemTemplate>
                                    <asp:Label ID="lblphone1" runat="server" Text='<%# Bind("Ph1") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Phone - 2">
                                <ItemTemplate>
                                    <asp:Label ID="lblphone2" runat="server" Text='<%# Bind("Ph2") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <%--   <asp:TemplateField HeaderText="Test Name">
                        <ItemTemplate>                        
                            <asp:Label ID="lbltestname" runat="server" Text='<%# Bind("TestName") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>--%>
                            <asp:TemplateField HeaderText="Test Date">
                                <ItemTemplate>
                                    <asp:Label ID="lbltestdate" runat="server" Text='<%# Bind("tdate") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="Delivery Date">
                                <ItemTemplate>
                                    <asp:Label ID="lbldeldate" runat="server" Text='<%# Bind("ddate") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Advance">
                                <ItemTemplate>
                                    <asp:Label ID="lblamt" runat="server" Text='<%# Bind("adv_amt") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>

                            <asp:CommandField SelectText="Edit" ShowSelectButton="True" ControlStyle-CssClass="temp" HeaderText="Edit" SelectImageUrl="~/Images/edit.png" ButtonType="Image">
                                <ControlStyle CssClass="temp"></ControlStyle>
                            </asp:CommandField>


                        </Columns>
                        <PagerStyle CssClass="pgr" />
                        <EditRowStyle BackColor="#CCFF33" />
                        <AlternatingRowStyle BackColor="#FFDB91" />
                    </asp:GridView>
                </div>

                <div class="listing" style='width: 60%; height: 200px; overflow: auto;'>
                    <asp:GridView ID="GridView2" CssClass="grid" PagerStyle-CssClass="pgr"
                        DataKeyNames="RowId" runat="server" AutoGenerateColumns="False" SelectedRowStyle-BackColor="GreenYellow"
                        AllowPaging="True" PageSize="100" Width="100%">
                        <RowStyle HorizontalAlign="Center" />
                        <Columns>
                            <asp:TemplateField HeaderText="Test Date" ItemStyle-HorizontalAlign="Left" ItemStyle-Width="10%" HeaderStyle-Width="20%">
                                <ItemTemplate>
                                    <asp:Label ID="lblRequisitionID" runat="server" Text='<%# Bind("RequisitionID") %>' Visible="false"></asp:Label>
                                    <asp:Label ID="lblRowId" runat="server" Text='<%# Bind("RowId") %>' Visible="false"></asp:Label>
                                    <asp:Label ID="lblTReqDate" runat="server" Text='<%# Bind("TReqDate") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Test Name" ItemStyle-HorizontalAlign="Left" ItemStyle-Width="40%" HeaderStyle-Width="60%">
                                <ItemTemplate>
                                    <asp:Label ID="lbltestcode" runat="server" Text='<%# Bind("TestCode") %>' Visible="false"></asp:Label>
                                    <asp:Label ID="lblname" runat="server" Text='<%# Bind("TestName") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Result" ItemStyle-HorizontalAlign="Left" ItemStyle-Width="50%" HeaderStyle-Width="20%">
                                <ItemTemplate>
                                    <asp:TextBox ID="txtResult" runat="server" Text='<%# Bind("Result") %>' TextMode="MultiLine" Height="80px" Width="400px"></asp:TextBox>
                                </ItemTemplate>
                            </asp:TemplateField>
                        </Columns>
                        <AlternatingRowStyle BackColor="#FFDB91" />
                    </asp:GridView>
                </div>
                <asp:Button ID="CmdSave" runat="server" Text="Save" Width="100px" OnClick="CmdSave_Click" />
                <asp:Button ID="cmdShow" runat="server" Text="View" Width="100px" OnClick="CmdShow_Click" />
                <br />
                <br />
                <asp:Label ID="lblmsg" runat="server" Font-Bold="true" Style="color: forestgreen; font-size: large"></asp:Label>
                 <br />
                <div class="listing" style='width: 100%; height: 300px; overflow: auto;'>
                        <table cellpadding="0" cellspacing="0"  title="Search" width="100%">
            <tr>
                <td align="center" style="width:100%;">
                    <div id='mydiv'  style="width:100%;">              
                            <asp:Literal ID="ltrReport" runat="server"></asp:Literal><br />  

                    </div>    
                </td>
            </tr>
            <tr>
                <td align="center"> 
                    <input type="button" id="cmdPrint" value="Print" style="width:70px; font-size:x-small" onclick="javascript: printDiv('mydiv')"/>
                </td>
            </tr>
        </table>
                </div>
            </div>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>


</asp:Content>

