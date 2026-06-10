<%@ Page Language="C#" MasterPageFile="~/MasterAll/MasterPageAll.master" AutoEventWireup="true" CodeFile="BillUpdate.aspx.cs" Inherits="Pathology_BillUpdate" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
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
            <asp:Label ID="Label1" runat="server"> Bill Update </asp:Label>
        </div>
    </div>
    <table cellpadding="0" cellspacing="0"  title="Search" width="100%">
        <tr>
            <td style="width:6%;">
                <label class="ipdList" style='width:75px;'><strong>Reg No :</strong></label>
            </td>
            <td style="width:15%;">
                <asp:TextBox ID="txtRegNo" CssClass="textbox-medium1"  runat="server"  style="width:100%;" ></asp:TextBox>
            </td>
            <td style="width:8%;">
                <label class="ipdList" style='width:75px;'><strong>&nbsp;&nbsp;Patient Name :</strong></label>
            </td>
            <td style="width:15%;">
                    
                <asp:TextBox ID="txtPtName" CssClass="textbox-medium1"  runat="server"  style="width:100%;"></asp:TextBox>
                       
            </td>
            <td style="width:8%;">
                <label class="ipdList" style='width:75px;'><strong>Invoice Date:</strong></label>
            </td>
            <td style="width:15%;">
                <asp:TextBox ID="txtInvDate" runat="server" CssClass="textbox-medium1" Width="138px" TextMode="Date"></asp:TextBox>
                        
            </td>
            <td style="width:10%;">
                <div class="form-sec-row"> 
                    <asp:Button ID="Button1" runat="server" Text="Generate" CssClass="submit-button1"  Height="28px" onclick="Button1_Click"/>
                </div>                  
        </td>       
        </tr>
    </table>
    <div class="formbox">
                <div class="listing"   style='width:100%; height:800px; overflow:auto;'>
                    <asp:GridView id="GridView1"  CssClass="grid" PagerStyle-CssClass="pgr" DataKeyNames ="VchNo" runat="server" AutoGenerateColumns="False"
                        AllowPaging="True" PageSize ="100" 
                        OnPageIndexChanging="GridView1_PageIndexChanging"   SelectedRowStyle-BackColor="GreenYellow"
                        OnRowCommand="GridView1_RowCommand" OnRowDataBound="GridView1_RowDataBound" Width="100%">
                        <RowStyle HorizontalAlign="Center" />
                            <Columns>
                                <asp:TemplateField HeaderText="Bill No">
                                    <ItemTemplate>
                                        <asp:Label ID="lblRecptNo" runat="server" Text='<%# Bind("ReceiptNo") %>'></asp:Label>
                                        <asp:Label ID="lblVchNo" runat="server" Text='<%# Bind("VchNo") %>' Visible="false"></asp:Label>
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
                                
                                
                                
                                <asp:TemplateField HeaderText="Payment Mode">
                                    <ItemTemplate>
                                        <asp:DropDownList ID="ddl_paymode" runat="server">
                                            <asp:ListItem Value="C">Cash</asp:ListItem>
                                            <asp:ListItem Value="R">Card</asp:ListItem>
                                            <asp:ListItem Value="N">Net Banking</asp:ListItem>
                                            <asp:ListItem Value="E">e-Wallet</asp:ListItem>
                                        </asp:DropDownList>
                                        <asp:Label ID="lblpaymode" runat="server" Text='<%# Bind("PaymentMode") %>' Visible="false"></asp:Label>
                                        
                                    </ItemTemplate>
                                    
                                    <ItemStyle HorizontalAlign="Left" />
                                </asp:TemplateField> 

                                   <asp:TemplateField HeaderText="">
                                    <ItemTemplate>
                                         <asp:LinkButton ID="LinkButton4"  CommandName="Save" CommandArgument='<%# Eval("VchNo") %>' runat="server">Save</asp:LinkButton>
                                    </ItemTemplate>
                                </asp:TemplateField> 
                                 
                            </Columns>
                        <PagerStyle CssClass="pgr" />
                            <EditRowStyle BackColor="#CCFF33" />
                            <AlternatingRowStyle BackColor="#FFDB91" />
                    </asp:GridView>
                </div>
            </div>
        
</asp:Content>