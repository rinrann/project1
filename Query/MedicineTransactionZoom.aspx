<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/MasterAll/MasterPageAll.master" CodeFile="MedicineTransactionZoom.aspx.cs" Inherits="Query_MedicineTransactionZoom" %>

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
         <asp:Label ID="Label1" runat="server">BatchNo Wise Transaction</asp:Label>
     </div><div class="form-sec-row"> 
             <label class="pname" style="width:auto;"><strong>Medicine Name :</strong></label> 
            <asp:TextBox ID="txtname" runat="server" CssClass="textbox-medium1" style="width:auto;" Enabled="false"></asp:TextBox>
            </div> 
        <div class="form-sec-row"> 
             <label class="pname" style="width:auto;"><strong>Batch No:</strong></label> 
            <asp:TextBox ID="txtbatch" runat="server" CssClass="textbox-medium1" style="width:auto;" Enabled="false"></asp:TextBox>
            </div> 
        <div class="listing"  style='width:100%; height:450px; overflow:auto;'>
                <asp:GridView id="GridView2"  Width="100%" CssClass="grid" PagerStyle-CssClass="pgr"
                 DataKeyNames ="docno" runat="server" 
                 AutoGenerateColumns="False" AllowPaging="True" 
                 SelectedRowStyle-BackColor="GreenYellow"  PageSize="100" OnRowCommand="GridView2_RowCommand" >
                    <Columns>
                      <asp:CommandField SelectText="Select" ShowSelectButton="True" />
                  
                        <asp:TemplateField HeaderText="Type" >
                            <ItemTemplate>
                                <asp:Label ID="lbltype" runat="server" Text='<%# Bind("type") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="DocType" Visible="false">
                            <ItemTemplate>
                                <asp:Label ID="lbldoctype" runat="server" Text='<%# Bind("types") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Date" >
                            <ItemTemplate>
                                <asp:Label ID="lblpdate" runat="server" Text='<%# Bind("Purdate") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
             
                         <asp:TemplateField HeaderText="Doc No">
                            <ItemTemplate>
                                <asp:Label ID="lbldocno" runat="server" Text='<%# Bind("docno") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>

                         <asp:TemplateField HeaderText="Expiry Date">
                            <ItemTemplate>
                                <asp:Label ID="lblexpdt" runat="server" Text='<%# Bind("EXPDATE") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                         <asp:TemplateField HeaderText="Qty">
                            <ItemTemplate>
                                <asp:Label ID="lblqty" runat="server" Text='<%# Bind("iqty") %>' Style="text-align:right"></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                         <asp:TemplateField HeaderText="Rate">
                            <ItemTemplate>
                                <asp:Label ID="lblrate" runat="server" Text='<%# Bind("irate") %>' Style="text-align:right"></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                         <asp:TemplateField HeaderText="Amount">
                            <ItemTemplate>
                                <asp:Label ID="lblamount" runat="server" Text='<%# Bind("iamount") %>' Style="text-align:right"></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                         <asp:TemplateField HeaderText="Issue To">
                            <ItemTemplate>
                                <asp:Label ID="lblissue" runat="server" Text='<%# Bind("issueto") %>' Style="text-align:right"></asp:Label>
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
