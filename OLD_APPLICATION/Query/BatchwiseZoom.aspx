<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/MasterAll/MasterPageAll.master" CodeFile="BatchwiseZoom.aspx.cs" Inherits="Query_BatchwiseZoom" %>

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

    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
    <ContentTemplate>
      <div class="pageheader">
         <asp:Label ID="Label1" runat="server">BatchNo Wise Medicine Zoom</asp:Label>
     </div>
        <div class="form-sec-row"> 
             <label class="pname" style="width:auto;"><strong>Medicine Name :</strong></label> 
            <asp:TextBox ID="txtname" runat="server" CssClass="textbox-medium1" style="width:auto;" Enabled="false"></asp:TextBox>
            </div> 
        <div class="listing"  style='width:100%; height:450px; overflow:auto;'>
                <asp:GridView id="GridView2"  Width="100%" CssClass="grid" PagerStyle-CssClass="pgr"
                 DataKeyNames ="BATCHNO" runat="server" 
                 AutoGenerateColumns="False" AllowPaging="True" 
                 SelectedRowStyle-BackColor="GreenYellow"  PageSize="100" OnRowCommand="GridView2_RowCommand" OnRowDataBound="GridView2_RowDataBound" >
                    <Columns>
                      <asp:CommandField SelectText="Select" ShowSelectButton="True" />
                  
                        <asp:TemplateField HeaderText="Purchase Date" >
                            <ItemTemplate>
                                <asp:Label ID="lblpdate" runat="server" Text='<%# Bind("Purdate") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Supplier" >
                            <ItemTemplate>
                                <asp:Label ID="lblsup" runat="server" Text=""></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Bill No" >
                            <ItemTemplate>
                                <asp:Label ID="lblBillno" runat="server" Text=""></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                         <asp:TemplateField HeaderText="Batch No">
                            <ItemTemplate>
                                <asp:Label ID="lblbatchno" runat="server" Text='<%# Bind("BATCHNO") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>

                         <asp:TemplateField HeaderText="Expiry Date">
                            <ItemTemplate>
                                <asp:Label ID="lblexpdt" runat="server" Text='<%# Bind("EXPDATE") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                         <asp:TemplateField HeaderText="Opn.Stock Qty">
                            <ItemTemplate>
                                <asp:Label ID="lblqty" runat="server" Text='<%# Bind("OPSTOCK") %>' Style="text-align:right"></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Recd Stock Qty" >
                            <ItemTemplate>
                                <asp:Label ID="lblrecqty" runat="server" Text='<%# Bind("recqty") %>' Style="text-align:right"></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                         <asp:TemplateField HeaderText="Issue Stock Qty" >
                            <ItemTemplate>
                                <asp:Label ID="lblissqty" runat="server" Text='<%# Bind("issqty") %>' Style="text-align:right"></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                         <asp:TemplateField HeaderText="Current Stock Qty">
                            <ItemTemplate>
                                <asp:Label ID="lblcurstock" runat="server" Text='<%# Bind("CURSTOCK") %>' Style="text-align:right"></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>

                     </Columns>
                  <PagerStyle CssClass="pgr" />
                    <EditRowStyle BackColor="#CCFF33" />
                    <AlternatingRowStyle BackColor="#FFDB91" />
                </asp:GridView>
            </div>
        <div align="center" style="text-align:center; width:60%;margin-left:40%;">
            <asp:Button ID="butBack" runat="server" Text="Back" CssClass="submit-button" OnClick="butBack_Click" /></div>
</ContentTemplate>
    </asp:UpdatePanel>
    </asp:Content>
