<%@ Control Language="C#" AutoEventWireup="true" CodeFile="UCServiceAutoSearch.ascx.cs" Inherits="UC_UCServiceAutoSearch" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<table id="Table2" cellspacing="0" cellpadding="0" border="0">
    <tr>
        <td id="tdtxt" style="height: 24px">
            <asp:TextBox ID="txtSearch" runat="server" Width="100"></asp:TextBox>
        </td>
        <td style="height: 24px">
            <asp:ImageButton ID="ImgBut" runat="server" ImageUrl="../Images/downArrow.jpg" OnClick="ImgBut_Click" 
                Width="20px" Height="20px"></asp:ImageButton>
        </td>
    </tr>
    <tr>
        <td colspan="2" height="0px">
            <div style="border-top-width: 1px; border-left-width: 1px;  border-left-color: silver;
                border-bottom-width: 1px; border-bottom-color: silver; overflow: auto; border-top-color: silver;
                position: absolute; height: 112px; background-color: transparent; border-right-width: 1px;
                border-right-color: silver">
                <asp:DataGrid ID="DataGrid_ddl" Font-Size="XX-Small" Font-Names="Verdana" BackColor="#F7F7F7"
                    runat="server" Width="410px" BorderColor="Silver" AutoGenerateColumns="False"
                    DataKeyField="TestId">

                    <HeaderStyle Font-Bold="True" BackColor="Silver"></HeaderStyle>
                    <AlternatingItemStyle BackColor="#D8E4F8"></AlternatingItemStyle>
                    <ItemStyle BorderColor="black"></ItemStyle>
                    <Columns>
                        <asp:TemplateColumn HeaderText="Code">
                            <HeaderStyle Width="80px"></HeaderStyle>
                            <ItemTemplate>
                                <asp:LinkButton runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.TestId") %>'
                                    CommandName="SelectITEM1" CausesValidation="false" ID="lnkIcode">
                                </asp:LinkButton>
                            </ItemTemplate>
                        </asp:TemplateColumn>
                        <asp:TemplateColumn HeaderText="Service Name">
                            <HeaderStyle Width="350px"></HeaderStyle>
                            <ItemTemplate>
                                <asp:LinkButton runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.TestName") %>'
                                    CommandName="SelectITEM2" CausesValidation="false" ID="lnkIname">
                                </asp:LinkButton>
                            </ItemTemplate>
                        </asp:TemplateColumn>
                    </Columns>
                </asp:DataGrid>
            </div>
        </td>
    </tr>
</table>