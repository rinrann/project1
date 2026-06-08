<%@ Control Language="C#" AutoEventWireup="true" CodeFile="UCService.ascx.cs" Inherits="UC_UCService" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<table id="Table2" cellspacing="0" cellpadding="0" border="0">
    <tr>
        <td style='background-color:#FB7B13; color:#FFF;'>
            <table id="Table1" cellspacing="0" cellpadding="1" border="0">
                <tr>
                    <td style="WIDTH: 80px; text-align:center"  ><strong>Service Id</strong></td>
                    <td style="WIDTH: 292px; text-align:center" ><strong>Service Name</strong></td>
                    <td style="WIDTH: 40px; text-align:center"  ><strong>Select</strong></td>
                </tr>
            </table>
        </td>
    </tr>
    <tr>
        <td colspan="3">
            <div id="div1" style="OVERFLOW: auto; height: 250px" runat="server">
                <asp:DataGrid ID="DataGrid1" ShowHeader="False" runat="server" DataKeyField="TestId"  CssClass="grid"
                 PagerStyle-CssClass="pgr" AutoGenerateColumns="False" Width="420px" OnPageIndexChanged="DataGrid1_PageIndexChanged">
                    <AlternatingItemStyle CssClass="odd1"></AlternatingItemStyle>
                    <PagerStyle CssClass="pgr" />
                <EditItemStyle BackColor="#CCFF33" />
                <AlternatingItemStyle BackColor="#FFDB91" />
                    <Columns>
                        <asp:TemplateColumn HeaderText="Code">
                            <HeaderStyle Width="80px"></HeaderStyle>
                            <ItemStyle Width="80px"></ItemStyle>
                            <ItemTemplate>
                                <asp:Label ID="lblcol1" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.TestId") %>'>
                                </asp:Label>
                            </ItemTemplate>
                        </asp:TemplateColumn>
                        <asp:TemplateColumn HeaderText="Book Name">
                            <HeaderStyle Width="300px"></HeaderStyle>
                            <ItemStyle Width="300px"></ItemStyle>
                            <ItemTemplate>
                                <asp:Label ID="lblcol2" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.TestName") %>'>
                                </asp:Label>
                            </ItemTemplate>
                        </asp:TemplateColumn>
                        <asp:TemplateColumn HeaderText="Select">
                            <HeaderStyle Width="40px"></HeaderStyle>
                            <ItemStyle HorizontalAlign="Center" Width="40px"></ItemStyle>
                            <ItemTemplate>
                                <asp:CheckBox ID="chkselect" runat="server" Checked="true" AutoPostBack="false" ></asp:CheckBox>
                            </ItemTemplate>
                        </asp:TemplateColumn>
                    </Columns>
                </asp:DataGrid>
            </div>
            <table>
                <tr>
                    <td colspan="3" style="padding-bottom: 5px; padding-top: 5px">
                        <asp:Button ID="cmdSelect"  Width="105px" runat="server" Text="Select All"   OnClick="cmdSelect_Click"></asp:Button>

                             </td>
                <td>

                        <asp:Button ID="cmdDeselect"  Width="90px" runat="server" Text="Deselect All"
                             OnClick="cmdDeselect_Click"></asp:Button>
                    </td>
                </tr>
            </table>
        </td>
    </tr>
</table>