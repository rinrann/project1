<%@ Page Language="C#" MasterPageFile="~/MasterAll/MasterPageAll.master"  AutoEventWireup="true" CodeFile="PatientInvoiceList.aspx.cs" Inherits="OPD_PatientInvoiceList" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
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
         <asp:Label ID="Label1" runat="server">Patient Invoice</asp:Label>
    </div>
    <div class="formbox">
        <div class="form-sec">
            <div>
                <table cellpadding="0" cellspacing="0"  title="Search" width="100%">
                    <tr>
                        <td style="width:6%;">
                            <label class="ipdList" style='width:75px;'><strong>Reg No :</strong></label>
                        </td>
                        <td style="width:15%;">
                            <asp:TextBox ID="txtRegNo" CssClass="textbox-medium1"  runat="server"  style="width:100%;" ></asp:TextBox>
                    
                            <cc1:AutoCompleteExtender ServiceMethod="SearchByRegNo"    OnClientItemSelected="autoCompleteEx_RegSelected"    MinimumPrefixLength="1"
                                CompletionInterval="100" EnableCaching="false" CompletionSetCount="10" TargetControlID="txtRegNo"  ID="AutoCompleteExtender3" runat="server" 
                               FirstRowSelected = "false" >
                            </cc1:AutoCompleteExtender>
          
                        </td>
                        <td style="width:8%;">
                            <label class="ipdList" style='width:75px;'><strong>&nbsp;&nbsp;Patient Name :</strong></label>
                        </td>
                        <td style="width:15%;">
                    
                            <asp:TextBox ID="txtPtName" CssClass="textbox-medium1"  runat="server"  style="width:100%;"></asp:TextBox>
                           <cc1:AutoCompleteExtender ServiceMethod="SearchByPatientName"    OnClientItemSelected="autoCompleteEx_ItemSelected"    MinimumPrefixLength="1"
                                CompletionInterval="100" EnableCaching="false" CompletionSetCount="10" TargetControlID="txtPtName"  ID="AutoCompleteExtender1" runat="server" 
                               FirstRowSelected = "false" >
                            </cc1:AutoCompleteExtender>
          
                        </td>
                        <td style="width:8%;">
                            <label class="ipdList" style='width:75px;'><strong>&nbsp;&nbsp;Phone No :</strong></label>
                        </td>
                        <td style="width:15%;">
                            <asp:TextBox ID="txtphSrch" runat="server" CssClass="textbox-medium1" Width="138px" ></asp:TextBox>
                        </td>
                        <td style="width:8%;">
                            <label class="ipdList" style='width:75px;'><strong>Invoice Date:</strong></label>
                        </td>
                        <td style="width:15%;">
                            <asp:TextBox ID="txtInvDate" runat="server" CssClass="textbox-medium1" Width="138px" TextMode="Date"></asp:TextBox>
                            <asp:RadioButtonList ID="RadioButtonList1" runat="server"  Width="250px" RepeatDirection="Horizontal" Visible="false">
                                <asp:ListItem Value="N" Text="Without Header" Selected="True"></asp:ListItem>
                                <asp:ListItem Value="Y" Text="With Header"></asp:ListItem>
                            </asp:RadioButtonList> 
                        </td>
                        <%--<td style="width:10%;">
                           
                        </td>--%>
                
                    
                
                
                        
                        <td style="width:10%;">
                                <div class="form-sec-row"> 
                                   <asp:Button ID="Button1" runat="server" Text="Generate" CssClass="submit-button1"  Height="28px" onclick="Button1_Click"/>
                                </div>                  
                        </td>         
                    </tr>
                </table>
                <table cellpadding="0" cellspacing="0"  title="Search" width="100%">
                    <tr>
                        <td align="center" style="width:100%;">
                            <div id='mydiv'  style="width:100%;">              
                                    <%-- --%> 
                                <asp:GridView id="GridView1"  CssClass="grid" Width="100%"
                                PagerStyle-CssClass="pgr" DataKeyNames ="VchNo" runat="server"   SelectedRowStyle-BackColor="GreenYellow"
                                AutoGenerateColumns="False" AllowPaging="True" PageSize="100" onrowcommand="GridView1_RowCommand" OnRowDataBound="GridView1_RowDataBound" >
                <RowStyle HorizontalAlign="Center" />
                <Columns>
                
                    <asp:TemplateField HeaderText="Sl.No" ItemStyle-Width="5%"  ItemStyle-HorizontalAlign="left"> 
                        <ItemTemplate>
                            <asp:Label ID="lblSlno" runat="server" Text='<%# Bind("SrNo") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="REG NO" ItemStyle-Width="15%"  ItemStyle-HorizontalAlign="left">
                       <ItemTemplate>
                            <asp:Label ID="lblregno" runat="server" Text='<%# Bind("RegNo") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="PATIENT NAME" ItemStyle-Width="15%"  ItemStyle-HorizontalAlign="left">
                       <ItemTemplate>
                            <asp:Label ID="lblname" runat="server" Text='<%# Bind("PatientName") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="INVOICE NO" ItemStyle-Width="15%" ItemStyle-HorizontalAlign="left">
                       <ItemTemplate>
                            <asp:Label ID="lblrecptno" runat="server" Text='<%# Bind("ReceiptNo") %>'></asp:Label>
                           <asp:Label ID="lblmoneyrcptno" runat="server" Text='<%# Bind("MoneyReceiptNo") %>' Visible="false"></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="BILL DATE" ItemStyle-Width="10%"  ItemStyle-HorizontalAlign="left">
                        <ItemTemplate>                        
                            <asp:Label ID="lblbilldt" runat="server" Text='<%# Bind("BillDate") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField> 
                    <asp:TemplateField HeaderText="PAYMENT DATE" ItemStyle-Width="10%"  ItemStyle-HorizontalAlign="left">
                        <ItemTemplate>                        
                            <asp:Label ID="lblpaymentdt" runat="server" Text='<%# Bind("PaymentDate") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField> 
                    <asp:TemplateField HeaderText="REQUISITION NO" ItemStyle-Width="15%"  ItemStyle-HorizontalAlign="left">
                        <ItemTemplate>                        
                            <asp:Label ID="lblreqno" runat="server" Text='<%# Bind("ReqNo") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                            
                    <asp:TemplateField HeaderText="BILL TYPE" ItemStyle-Width="10%"  ItemStyle-HorizontalAlign="left">
                       <ItemTemplate>
                            <asp:Label ID="lblbilltype" runat="server" Text='<%# Bind("BillTypeName") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="BILL AMOUNT" ItemStyle-Width="10%"  ItemStyle-HorizontalAlign="right">
                        <ItemTemplate>                        
                            <asp:Label ID="lblbillamt" runat="server" Text='<%# Bind("BillAmt") %>'></asp:Label>
                            <asp:Label ID="lbldueAmt" runat="server" Text='<%# Bind("DueAmt") %>' Visible="false"></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>  
                    <asp:TemplateField HeaderText="PAYMENT MODE" ItemStyle-Width="10%"  ItemStyle-HorizontalAlign="left">
                        <ItemTemplate>                        
                            <asp:Label ID="lblpmode" runat="server" Text='<%# Bind("PayMode") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>  
                    <asp:TemplateField HeaderText="STATUS" ItemStyle-Width="10%"  ItemStyle-HorizontalAlign="left">
                        <ItemTemplate>                        
                            <asp:Label ID="lblbillstatus" runat="server" Text='<%# Bind("BillStatus") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>  
                    <asp:TemplateField HeaderText="">
                        <ItemTemplate>
                             <asp:LinkButton ID="LinkButton4"  CommandName="Print" CommandArgument="<%# Container.DataItemIndex %>"  runat="server">Print Invoice</asp:LinkButton>
                        </ItemTemplate>
                    </asp:TemplateField>    
                    <asp:TemplateField HeaderText="">
                        <ItemTemplate>
                             <asp:LinkButton ID="LinkButton5"  CommandName="Receipt" CommandArgument="<%# Container.DataItemIndex %>"  runat="server">Final Invoice</asp:LinkButton>
                        </ItemTemplate>
                    </asp:TemplateField>  
                       
                </Columns>
                <%--<PagerStyle CssClass="pgr" />
                <EditRowStyle BackColor="#CCFF33" />
                <AlternatingRowStyle BackColor="#FFDB91" />--%>
        </asp:GridView>
                           </div>    
                        </td>
                    </tr>
                    <tr>
                        <td align="center" style="width:100%;">
                     
                            <%--<input type="button" value="Back" style="width:70px; font-size:x-small" onclick="window.history.back()" />--%>
                            <input type="button" id="cmdPrint" value="Print" style="width:70px; font-size:x-small" onclick="javascript: printDiv('mydiv')"/>

                        </td>
                    </tr>
                </table>
                <table cellpadding="0" cellspacing="0"  title="Search" width="100%">
                    <tr>
                        <td align="center" style="width:100%;">
                            <div id='billdiv'  style="width:100%;" runat="server" visible="false"> 
                                <table cellpadding="0" cellspacing="0"  title="Search" width="100%">
                                    <tr>
                                        <td align="center" style="width:100%;">
                                            <div id='printdiv'  style="width:100%;"> 
                                                <asp:Literal ID="ltrReport" runat="server"></asp:Literal><br />
                                            </div>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="center" style="width:100%;">
                                            <input type="button" id="Button2" value="Print" style="width:70px; font-size:x-small" onclick="javascript: printDiv('printdiv')"/>
                                            <asp:Button ID="Button3" runat="server" Text="Close"  style="width:70px; font-size:x-small" OnClick="Button3_Click"
                                                />
                                        </td>
                                    </tr>
                                </table>
                                
                            </div>
                        </td>
                    </tr>
                </table>
            </div>
        </div>
    </div>
</asp:Content>