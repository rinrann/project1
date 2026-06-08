<%@ Page Language="C#" MasterPageFile="~/MasterAll/MasterPageAll.master" AutoEventWireup="true" CodeFile="RequisitionBill.aspx.cs" Inherits="Pathology_RequisitionBill" %>
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
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
  
        <ContentTemplate>
        
        <div id="h1">
            <div class="pageheader">
                 <asp:Label ID="Label1" runat="server">Requisition Bill Entry</asp:Label>
             </div>
        </div>
        
        <div class="formbox">
            <asp:MultiView ID="MainView" runat="server">
                <asp:View ID="View1" runat="server">
                    <table  style="display:none">
                        <tr>
                            <td>Bill Type</td>
                            <td>
                                <asp:DropDownList ID="ddltype" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddltype_SelectedIndexChanged">
                                    <asp:ListItem Selected="True" Value="1">Investigation Bill</asp:ListItem>
                                    <asp:ListItem Value="2">Sample Bill</asp:ListItem>
                                </asp:DropDownList>
                            </td>
                        </tr>
                    </table>
                    <table width="100%">
                        <tr>
                            <td>
                                <div class="form-sec-row"> 
                                <label class="pname"><strong>Name :</strong></label> 
                                <asp:TextBox ID="txtnameSrch" runat="server" CssClass="textbox-medium1" Width="200px" ></asp:TextBox>  <div class="clear"></div>
                                </div>
                            </td>
                            <td>
                                <div class="form-sec-row"> 
                                <asp:Button ID="ButtonSrch" runat="server" Text="Search" Height="28px"  CssClass="submit-button1" OnClick="ButtonSrch_Click" />
                                </div>                  
                            </td>  
                        </tr>
                    </table>

                    <br />
                    <div class="listing"   style='width:100%; height:800px; overflow:auto;'>
                        <asp:GridView id="GridView1"  CssClass="grid" PagerStyle-CssClass="pgr" DataKeyNames ="RequisitionNo" runat="server" AutoGenerateColumns="False"
                        AllowPaging="True" PageSize ="100" 
                        OnPageIndexChanging="GridView1_PageIndexChanging"   SelectedRowStyle-BackColor="GreenYellow"
                        OnRowCommand="GridView1_RowCommand" OnRowDataBound="GridView1_RowDataBound" Width="100%">
                            <RowStyle HorizontalAlign="Center" />
                            <Columns>
                                <asp:TemplateField HeaderText="Registration No">
                                    <ItemTemplate><asp:Label ID="lblregno" runat="server" Text='<%# Bind("RegistrationNo") %>'></asp:Label></ItemTemplate>
                                    <ItemStyle HorizontalAlign="Left" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Requisition No" >
                                  <ItemTemplate>
                                      <asp:Label ID="lblreqno" runat="server" Text='<%# Bind("RequisitionNo") %>'></asp:Label>
                                      <asp:Label ID="lblvchno" runat="server" Text='<%# Bind("VchNo") %>'  Visible="false"></asp:Label>
                                      <asp:Label ID="lblReqType" runat="server" Text='<%# Bind("ReqType") %>'  Visible="false"></asp:Label>
                                      <asp:Label ID="lblbilldate" runat="server" Text='<%# Bind("billdate") %>'  Visible="false"></asp:Label>
                                  </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Left" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Patient Name">
                                    <ItemTemplate><asp:Label ID="lblname" runat="server" Text='<%# Bind("PatientName") %>'></asp:Label></ItemTemplate>
                                    <ItemStyle HorizontalAlign="Left" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Referal Name">
                                    <ItemTemplate><asp:Label ID="lblrefname" runat="server" Text='<%# Bind("RefDocName") %>'></asp:Label></ItemTemplate>
                                    <ItemStyle HorizontalAlign="Left" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Total Bill">
                                    <ItemTemplate><asp:Label ID="lblBillAmt" runat="server" Text='<%# Bind("PayableAmt") %>'></asp:Label></ItemTemplate>
                                    <ItemStyle HorizontalAlign="Right" />
                                </asp:TemplateField>
                                  <asp:TemplateField HeaderText="Disc Amount">
                                    <ItemTemplate><asp:Label ID="lblDiscAmt" runat="server" Text='<%# Bind("DiscountAmt") %>'></asp:Label></ItemTemplate>
                                    <ItemStyle HorizontalAlign="Right" />
                                </asp:TemplateField>      
                                <asp:TemplateField HeaderText="Paid Amount">
                                    <ItemTemplate><asp:Label ID="lblPaidAmt" runat="server" Text='<%# Bind("AdAmt") %>'></asp:Label></ItemTemplate>
                                    <ItemStyle HorizontalAlign="Right" />
                                </asp:TemplateField>    
                                <asp:TemplateField HeaderText="Due Amount">
                                    <ItemTemplate><asp:Label ID="lblDueAmt" runat="server" Text='<%# Bind("DueAmt") %>'></asp:Label></ItemTemplate>
                                    <ItemStyle HorizontalAlign="Right" />
                                </asp:TemplateField>    
                                <asp:TemplateField HeaderText="Pay Bill"  ItemStyle-Width="90px"  ItemStyle-HorizontalAlign="Center">
                                    <ItemTemplate>
                                       <asp:LinkButton ID="LinkButton9" CommandName="PayNow" CommandArgument='<%# Eval("RequisitionNo") %>'  runat="server" >Pay Now</asp:LinkButton> 
                                           <asp:Label ID="Label11" runat="server" ForeColor="Green"></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
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
                                <table style="width:100%;">
                                    <tr>
                                        <td style="width:20%;">Registration No:</td>
                                        <td style="width:30%;">
                                            <asp:Label ID="lblPRegno" Width="100%" runat="server"></asp:Label>
                                            <asp:Label ID="lblBillType" Width="100%" runat="server"></asp:Label>
                                            <asp:Label ID="lblReqdate" Width="100%" runat="server" Visible="false"></asp:Label>
                                        </td>
                                        <td colspan="2" style="width:50%;"> </td>
                                    </tr>
                                    <tr>
                                        <td>Patient Name:</td>
                                        <td>
                                            <asp:Label ID="lblPname" Width="100%" runat="server"></asp:Label>
                                        </td>
                                        <td colspan="2"> </td>
                                    </tr>
                                    <tr>
                                        <td>Requisition No:</td>
                                        <td>
                                            <asp:Label ID="lblPatientreqno" Width="100%" runat="server"></asp:Label>
                                        </td>
                                        <td colspan="2"> </td>
                                    </tr>
                                    <tr>
                                        <td colspan="2" valign="top">
                                            <asp:GridView ID="GridView2" CssClass="grid" PagerStyle-CssClass="pgr" DataKeyNames="TestId"
                                            runat="server" AutoGenerateColumns="False" AllowPaging="True" PageSize="100"
                                            Width="100%" BorderColor="Tan" BorderWidth="1px"
                                            CellPadding="2" ForeColor="Black" GridLines="None"
                                            OnRowDataBound= "GridView2_RowDataBound">
                                                    <Columns>
                                                        <asp:TemplateField HeaderText="Serial No">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblSerial" runat="server"></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>

                                                        <asp:TemplateField HeaderText="Test Code" Visible="false">

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
                                                            <ItemStyle HorizontalAlign="Right" />
                                                        </asp:TemplateField>
                                            
                                                    </Columns>
                                                    <%--<PagerStyle CssClass="pgr" />
                                                    <EditRowStyle BackColor="#CCFF33" />
                                                    <AlternatingRowStyle BackColor="#FFDB91" />
                                                    <SelectedRowStyle BackColor="GreenYellow" />--%>
                                            </asp:GridView>
                                        </td>
                                        <td style="width:10%;"></td>
                                        <td style="width:40%;" rowspan="5" valign="top"> 
                                            <div id="advbillDiv" runat="server" style="max-height:150px;overflow:auto;width:80%;" visible="false">
                                                <asp:GridView ID="GridView3" CssClass="grid" PagerStyle-CssClass="pgr" DataKeyNames="ReceiptNo"
                                                runat="server" AutoGenerateColumns="False" AllowPaging="True" PageSize="100"
                                                Width="100%" BorderColor="Tan" BorderWidth="1px"
                                                CellPadding="2" ForeColor="Black" GridLines="None">
                                                    <Columns>
                                                        <asp:TemplateField HeaderText="Serial No" ItemStyle-Width="10%">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblSerial" runat="server" Text='<%# Bind("srl") %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>

                                                        <asp:TemplateField HeaderText="Receipt No"  ItemStyle-Width="15%">

                                                            <ItemTemplate>
                                                                <asp:Label ID="lblrcptNo" runat="server" Text='<%# Bind("ReceiptNo") %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>

                                                        <asp:TemplateField HeaderText="Date"  ItemStyle-Width="10%">

                                                            <ItemTemplate>
                                                                <asp:Label ID="lblDate" runat="server" Text='<%# Bind("AdvDate") %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>

                                                        <asp:TemplateField HeaderText="Advance Amount"  ItemStyle-Width="20%">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblAmt" runat="server" Text='<%# Bind("BalAmount") %>'></asp:Label>
                                                            </ItemTemplate>
                                                            <ItemStyle HorizontalAlign="Right" />
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Balance Amount"  ItemStyle-Width="20%">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblbalAmt" runat="server" Text='<%# Bind("BalAmount") %>'></asp:Label>
                                                            </ItemTemplate>
                                                            <ItemStyle HorizontalAlign="Right" />
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Adjust" ItemStyle-Width="10%">
                                                            <ItemTemplate>
                                                                <asp:CheckBox ID="chkselect" runat="server" Checked="false" AutoPostBack="true" OnCheckedChanged="chkselect_CheckedChanged"></asp:CheckBox>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                    </Columns>
                                                </asp:GridView>
                                            </div>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>Total Bill Amount:</td>
                                        <td style="text-align:right">
                                            <asp:Label ID="lblTotBillAmt" Width="100%" runat="server"></asp:Label>
                                        </td>
                                        
                                    </tr>
                                        <tr>
                                        <td>Discount Amount:</td>
                                        <td style="text-align:right">
                                            <asp:Label ID="lbldiscountamt" Width="100%" runat="server" Visible="false"></asp:Label>
                                            <asp:TextBox runat="server" ID="txtdiscountamt" style="text-align:right" OnTextChanged="txtdiscountamt_TextChanged" AutoPostBack="true"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>Paid Amount:</td>
                                        <td style="text-align:right">
                                            <asp:Label ID="lblPaidBillAmt" Width="100%" runat="server"></asp:Label>
                                        </td>
                                        
                                    </tr>
                                    <tr>
                                        <td>Adjusted Advance</td>
                                        <td style="text-align:right">
                                            <asp:Label ID="lblAdvAdjustedAmt" Width="100%" runat="server"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>Due Amount:</td>
                                        <td style="text-align:right">
                                            <asp:Label ID="lblDueBillAmtOrg" Width="100%" runat="server" Visible="false"></asp:Label>
                                            <asp:Label ID="lblDueBillAmt" Width="100%" runat="server"></asp:Label>
                                        </td>
                                        
                                    </tr>
                                    <tr>
                                        <td>Current Amount:</td>
                                        <td style="text-align:right">
                                            <asp:TextBox runat="server" ID="txtPayableAmt" style="text-align:right"></asp:TextBox>
                                        </td>
                                        <td colspan="2"> 
                                            <asp:LinkButton ID="LinkButton10" CommandName="BillAdjust"  runat="server" OnClick="LinkButton10_Click">Adjust Advance Bill</asp:LinkButton> 
                                        </td>
                                    </tr>
                                    <tr style="display:none;">
                                        <td>Payment Type:</td>
                                        <td style="text-align:right">
                                            <asp:DropDownList ID="ddlPayType" runat="server" CssClass="textbox-medium1">
                                                <asp:ListItem Value="P" Text="Payment"></asp:ListItem>
                                                <asp:ListItem Value="A" Text="Advance"></asp:ListItem>
                                            </asp:DropDownList>
                                        </td>
                                        <td colspan="2"> </td>
                                    </tr>
                                    <tr>
                                        <td>Payment Mode</td>
                                        <td align="right">
                                            <asp:DropDownList ID="ddlPaymentMode" runat="server" CssClass="textbox-medium1" OnSelectedIndexChanged="ddlPaymentMode_SelectedIndexChanged" AutoPostBack="true">
                                                <asp:ListItem Value="C">Cash</asp:ListItem>
                                                <asp:ListItem Value="R">Card</asp:ListItem>
                                                <asp:ListItem Value="E">e-Wallet</asp:ListItem>
                                                <asp:ListItem Value="N">Net Banking</asp:ListItem>
                                            </asp:DropDownList>
                                        </td>
                                        <td colspan="2"> </td>
                                    </tr>
                                    <tr runat="server" id="divBank" visible="false">
                                        <td>
                                            <label id="lblbankNm" runat="server"><strong>Bank Name :</strong></label>
                                        </td>
                                        <td align="right">
                                            <asp:TextBox ID="txtBankName" runat="server" CssClass="textbox-medium1" ></asp:TextBox>
                                        </td>
                                        <td colspan="2"> </td>
                                    </tr>
                                    <tr runat="server" id="divchqno" visible="false">
                                        <td>
                                            <label id="lblchqno" runat="server"><strong>Cheque No :</strong></label>
                                        </td>
                                        <td align="right">
                                            <asp:TextBox ID="txtChequeNo" runat="server" CssClass="textbox-medium1" ></asp:TextBox>
                                        </td>
                                        <td colspan="2"> </td>
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
                                        <td colspan="2"> </td>
                                    </tr>
                                    <tr style="display:none;">
                                        <td>
                                            <asp:TextBox ID="txtAdvBillNo" runat="server"></asp:TextBox>
                                            <asp:TextBox ID="txtAdvBillAmt" runat="server"></asp:TextBox>
                                            
                                            
                                        </td>
                                        <td></td>
                                        <td colspan="2"> </td>
                                    </tr>
                                    <tr>
                                        <td></td>
                                        <td>
                                            <asp:Button ID="btnSubmit" runat="server" OnClick="btnSubmit_Click"  Height="28px" Text="Submit" CssClass="submit-button" />
                                            <asp:Button ID="btnCancel" runat="server" OnClick="btnCancel_Click" Text="Back"   Height="28px" CssClass="submit-button" />
                                            <%--<asp:Button ID="btnrcpt" runat="server" Text="Receipt"   Height="28px" CssClass="submit-button" OnClick="btnrcpt_Click" Visible="false"/>--%>
                                            <asp:Button ID="btnInvoice" runat="server" Text="Invoice"   Height="28px" CssClass="submit-button" OnClick="btnInvoice_Click" Visible="false"/>
                                            <div id="divOpt" runat="server" visible="false">
                                                <asp:RadioButtonList ID="RadioButtonList1" runat="server"  Width="250px"
                                                   RepeatDirection="Horizontal">
                                                    <asp:ListItem Value="N" Text="Without Header" Selected="True"></asp:ListItem>
                                                   <asp:ListItem Value="Y" Text="With Header"></asp:ListItem>
                                               </asp:RadioButtonList>
                                            </div>
                                        </td>
                                        <td colspan="2"> </td>
                                    </tr>
                                    <tr>
                                        <td colspan="2">
                                            <div class="error">
                                                <strong><asp:Label ID="lblError" runat="server" Font-Bold="True" Visible="false"></asp:Label>
                                                </strong>
                                                <asp:Label ID="lblreceitpNo" runat="server" Font-Bold="True" Visible="false"></asp:Label>
                                                <asp:Label ID="lblinvoiceNo" runat="server" Font-Bold="True" Visible="false"></asp:Label>
                                                <div class="clear"></div>
                                            </div>
                                        </td>
                                        <td colspan="2"> </td>
                                    </tr>
                                </table>
                                     <div width="100%" align="center">
          <table width="100%" >    <tr>        
                        <td style="width: 100%">   <div id='mydiv' style="width:100%;">            
                            <asp:Literal ID="ltrReport" runat="server"></asp:Literal><br />   </div>                 
                        </td>
                    </tr>

                     <tr>
                        <td  colspan="3" style="text-align:center">
                            <asp:Button ID="Button6" runat="server" Text="Back" Font-Size="X-Small" Visible="false" 
                                Width="70px" onclick="Button6_Click"/>
                            <asp:Button ID="Button7" runat="server" Font-Size="X-Small" Visible="false" Width="70px" Text="Print" OnClientClick="javascript:printDiv('mydiv')" />
      </td>
                    </tr>
                    </table>
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