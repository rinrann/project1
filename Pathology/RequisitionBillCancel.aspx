<%@ Page Language="C#" MasterPageFile="~/MasterAll/MasterPageAll.master" AutoEventWireup="true" CodeFile="RequisitionBillCancel.aspx.cs" Inherits="Pathology_RequisitionBillCancel" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <script type="text/javascript" language="javascript">
        function printDiv(divID) {
            var divElements = document.getElementById(divID).innerHTML;
            var oldPage = document.body.innerHTML;
            document.body.innerHTML = "<html><head><title></title></head><body>" + divElements + "</body>";
            window.print();
            document.body.innerHTML = oldPage;
        }
    </script>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <%--For Busy Loader .............................--%>
     <asp:UpdateProgress ID="UpdateProgress1" runat="server">
    <ProgressTemplate>
    
       <div id="aaa" class="progressBackgroundFilter">          </div>
        <div id="bbb" class="processMessage"><img alt="Loading" src="../images/pwait.gif"/>
      
              </div>
    </ProgressTemplate>
    </asp:UpdateProgress>
    <%--For Busy Loader End.............................--%>
    
            <div id="h1">
                <div class="pageheader">
                    <asp:Label ID="Label1" runat="server">Bill Cancellation Entry</asp:Label>
                </div>
            </div>
            <table width="100%">
          <tr>
                <td width="7%">
                   <label class="pname"><strong>Reg No :</strong></label> 
                </td>
                  <td  width="20%">
                      <asp:TextBox ID="txtregSrch" runat="server" CssClass="textbox-medium1" Width="100%" ></asp:TextBox>  <div class="clear"></div>
                  </td>
                <td width="7%">
                    <label class="pname"><strong>Name :</strong></label> 
                 
                </td>
              <td width="20%">
                  <asp:TextBox ID="txtnameSrch" runat="server" CssClass="textbox-medium1" Width="100%" ></asp:TextBox>  <div class="clear"></div>
              </td>
                <td width="7%">
                    <label class="pname"><strong>Ph No :</strong></label> 
                    
                </td>
              <td width="10%">
                  <asp:TextBox ID="txtphSrch" runat="server" CssClass="textbox-medium1" Width="100%" ></asp:TextBox>  <div class="clear"></div>
              </td>
                <td width="7%">
                    <div class="form-sec-row"> 
                    <label class="pname"><strong>Reg Date :</strong></label> 
                    </div>
                </td>
                <td width="10%">
                    <asp:TextBox ID="txtRegDate" runat="server" CssClass="textbox-medium1" Width="138px" TextMode="Date"></asp:TextBox>
                </td>
                <td>
                    <div class="form-sec-row"> 
                    <asp:Button ID="Button5" runat="server" Text="Search" Height="28px"  CssClass="submit-button1" onclick="Button5_Click" />
                    </div>                  
                </td>             
                      
            </tr>
      </table>
            <div class="formbox">
                <asp:MultiView ID="MainView" runat="server">
                    <asp:View ID="View1" runat="server">
                        <div class="listing"   style='width:100%; height:800px; overflow:auto;'>
                            <asp:GridView id="GridView1"  CssClass="grid" PagerStyle-CssClass="pgr" DataKeyNames ="VchNo" runat="server" AutoGenerateColumns="False"
                                AllowPaging="True" PageSize ="100" 
                                OnPageIndexChanging="GridView1_PageIndexChanging"   SelectedRowStyle-BackColor="GreenYellow"
                                OnRowCommand="GridView1_RowCommand" OnRowDataBound="GridView1_RowDataBound" Width="100%">
                                <RowStyle HorizontalAlign="Center" />
                                    <Columns>
                                        <asp:TemplateField HeaderText="Invoice No">
                                            <ItemTemplate>
                                                <asp:Label ID="lblVchNo" runat="server" Text='<%# Bind("VchNo") %>' Visible="false"></asp:Label >
                                                <asp:Label ID="lblInvoiceNo" runat="server" Text='<%# Bind("ReceiptNo") %>'></asp:Label >
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Left" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Registration No">
                                            <ItemTemplate><asp:Label ID="lblregno" runat="server" Text='<%# Bind("RegNo") %>'></asp:Label></ItemTemplate>
                                            <ItemStyle HorizontalAlign="Left" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Requisition No" >
                                          <ItemTemplate>
                                              <asp:Label ID="lblreqno" runat="server" Text='<%# Bind("ReqNo") %>'></asp:Label>
                                              </ItemTemplate>
                                          <ItemStyle HorizontalAlign="Left" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Patient Name">
                                            <ItemTemplate><asp:Label ID="lblname" runat="server" Text='<%# Bind("PatientName") %>'></asp:Label></ItemTemplate>
                                            <ItemStyle HorizontalAlign="Left" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Bill Type">
                                            <ItemTemplate>
                                                <asp:Label ID="lbltypeName" runat="server" Text='<%# Bind("BillTypeName") %>'></asp:Label>
                                                <asp:Label ID="lbltype" runat="server" Text='<%# Bind("BillType") %>' Visible="false"></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Left" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Bill Date">
                                            <ItemTemplate><asp:Label ID="lbldate" runat="server" Text='<%# Bind("BillDate") %>'></asp:Label></ItemTemplate>
                                            <ItemStyle HorizontalAlign="Left" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Bill Amt">
                                            <ItemTemplate><asp:Label ID="lblamt" runat="server" Text='<%# Bind("BillAmt") %>'></asp:Label></ItemTemplate>
                                            <ItemStyle HorizontalAlign="Right" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Cancel Bill"   ItemStyle-Width="5px"  ItemStyle-HorizontalAlign="Center"> 
                                            <ItemTemplate>
                                                <asp:Label ID="lblcancel" runat="server" Text='<%# Bind("Cancel") %>' Visible="false"></asp:Label>
                                                <asp:CheckBox ID="ChkCancel" runat="server" OnCheckedChanged="ChkCancel_CheckedChanged" AutoPostBack="true"/>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Reason"   ItemStyle-Width="5px"  ItemStyle-HorizontalAlign="Center"> 
                                            <ItemTemplate>
                                                <asp:TextBox ID="txtReason" runat="server" TextMode="MultiLine" Text='<%# Bind("CancelRemarks") %>'></asp:TextBox>
                                             </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Cancel Request">
                                            <ItemTemplate>
                                                 <asp:LinkButton ID="LinkButton4"  CommandName="CancelRequest" CommandArgument='<%# Eval("VchNo") %>' runat="server">Send Request</asp:LinkButton>
                                            </ItemTemplate>
                                        </asp:TemplateField> 
                                        <asp:TemplateField HeaderText="Request Status">
                                            <ItemTemplate>
                                                <asp:Label ID="lblReqStatus" runat="server" Text='<%# Bind("ReqStatus") %>'></asp:Label>
                                                <asp:Label ID="lblReqSts" runat="server" Text='<%# Bind("ReqSts") %>' Visible="false"></asp:Label>
                                            </ItemTemplate>
                                    
                                            <ItemStyle HorizontalAlign="Left" />
                                        </asp:TemplateField> 
                                        <asp:TemplateField HeaderText="Refund Status">
                                            <ItemTemplate>
                                                <asp:Label ID="lblRefundStatus" runat="server" Text='<%# Bind("RefundStatus") %>'></asp:Label>
                                                <asp:Label ID="lblRefsts" runat="server" Text='<%# Bind("RefStatus") %>' Visible="false"></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Left" />
                                        </asp:TemplateField> 
                                        <asp:TemplateField HeaderText="Refund Bill">
                                            <ItemTemplate>
                                                 <asp:LinkButton ID="LinkButton5"  CommandName="BillRefund" CommandArgument='<%# Eval("VchNo") %>' runat="server"> Refund</asp:LinkButton>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <%--<asp:CommandField SelectText="Edit" ShowSelectButton="True" ControlStyle-CssClass="temp" HeaderText="Save" SelectImageUrl="~/Images/ok.png" ButtonType="Image">
                                            <ControlStyle CssClass="temp"></ControlStyle>
                                        </asp:CommandField>--%>
                                    </Columns>
                                <PagerStyle CssClass="pgr" />
                                    <EditRowStyle BackColor="#CCFF33" />
                                    <AlternatingRowStyle BackColor="#FFDB91" />
                            </asp:GridView>
                        </div>
                    </asp:View>
                    <asp:View ID="View2" runat="server">
                        <table style="width:100%;">
                            <tr>
                                <td style="width:100%;">
                                    <table style="width:50%;">
                                        <tr>
                                            <td style="width:40%;">Registration No:</td>
                                            <td style="width:60%;">
                                                <asp:Label ID="lblPRegno" Width="100%" runat="server"></asp:Label>
                                                <asp:Label ID="lblRefVchNo" Width="100%" runat="server" Visible="false"></asp:Label>
                                                <asp:Label ID="lblRefReqNo" Width="100%" runat="server" Visible="false"></asp:Label>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>Patient Name:</td>
                                            <td>
                                                <asp:Label ID="lblPname" Width="100%" runat="server"></asp:Label>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>Requisition No:</td>
                                            <td>
                                                <asp:Label ID="lblPatientreqno" Width="100%" runat="server"></asp:Label>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>Bill Amount:</td>
                                            <td style="text-align:right">
                                                <asp:Label ID="lblTotBillAmt" Width="100%" runat="server"  Enabled="false"></asp:Label>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>Refund Amount:</td>
                                            <td style="text-align:right">
                                                <asp:Label ID="lblRefAmt" Width="100%" runat="server"></asp:Label>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>Refund Mode</td>
                                            <td align="right">
                                                <asp:DropDownList ID="ddlPaymentMode" runat="server" CssClass="textbox-medium1" OnSelectedIndexChanged="ddlPaymentMode_SelectedIndexChanged" AutoPostBack="true">
                                                    <asp:ListItem Value="C">Cash</asp:ListItem>
                                                    <asp:ListItem Value="R">Card</asp:ListItem>
                                                    <asp:ListItem Value="E">e-Wallet</asp:ListItem>
                                                    <asp:ListItem Value="N">Net Banking</asp:ListItem>
                                                </asp:DropDownList>
                                            </td>
                                        </tr>
                                        <tr runat="server" id="divBank" visible="false">
                                            <td>
                                                <label id="lblbankNm" runat="server"><strong>Bank Name :</strong></label>
                                            </td>
                                            <td align="right">
                                                <asp:TextBox ID="txtBankName" runat="server" CssClass="textbox-medium1" ></asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr runat="server" id="divchqno" visible="false">
                                            <td>
                                                <label id="lblchqno" runat="server"><strong>Cheque No :</strong></label>
                                            </td>
                                            <td align="right">
                                                <asp:TextBox ID="txtChequeNo" runat="server" CssClass="textbox-medium1" ></asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr runat="server" id="divchqdt" visible="false">
                                            <td>
                                                <label id="lblchqdt" runat="server"><strong>Cheque Date :</strong></label>
                                            </td>
                                            <td align="right">
                                                <asp:TextBox ID="txtchqdt" runat="server" CssClass="textbox-medium1" ></asp:TextBox>
                                                <%--<cc1:MaskedEditExtender ID="MaskedEditExtender3" runat="server" TargetControlID="txtchqdt"
                                                Mask="99/9999" MessageValidatorTip="true" MaskType="Date" DisplayMoney="Left"
                                                AcceptNegative="Left" ErrorTooltipEnabled="True" CultureName="en-GB" />
                                                <br /><asp:Label ID="Label4" runat="server" ForeColor="Green" Text="(MM/YYYY)"></asp:Label>--%>
                                            </td>
                                        </tr>
                                         <tr>
                                            <td></td>
                                            <td>
                                                <asp:Button ID="btnSubmit" runat="server" OnClick="btnSubmit_Click"  Height="28px" Text="Submit" CssClass="submit-button" />
                                                <asp:Button ID="btnCancel" runat="server" OnClick="btnCancel_Click" Text="Back"   Height="28px" CssClass="submit-button" />
                                                
                                            </td>
                                        </tr>
                                        <tr>
                                            <td colspan="2">
                                                <div class="error">
                                                    <strong><asp:Label ID="lblError" runat="server" Font-Bold="True" Visible="false"></asp:Label>
                                                    </strong>
                                                    
                                                    <div class="clear"></div>
                                                </div>
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                        </table>
                    </asp:View>
                </asp:MultiView>
            </div>
        
</asp:Content>

