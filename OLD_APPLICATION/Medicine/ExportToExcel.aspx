<%@ Page Title="" Language="C#" MasterPageFile="~/MasterAll/MasterPageAll.master" AutoEventWireup="true" CodeFile="ExportToExcel.aspx.cs" Inherits="Medicine_ExportToExcel" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
 <%--   <asp:UpdatePanel ID="UpdatePanel1" runat="server">
    <ContentTemplate>--%>
     <div class="pageheader">
            <asp:Label ID="Label1" runat="server">Export To Excel</asp:Label>
        </div>
    <div class="formbox">
            <div class="form-sec">
                <div class="error">
                    <strong>
                        <asp:Label ID="lblError" runat="server" Font-Bold="True" CssClass="fadeout"></asp:Label>
                    </strong>
                    <div class="clear">
                    </div>
                </div>
                    <asp:HiddenField ID="HiddenField1" runat="server"/>

                     <div class="form-sec-row">
                    <label><strong>
                     Manufacuring Company :</strong></label>
                         <asp:DropDownList ID="DropDownList1" runat="server" CssClass="textbox-medium1">
                         </asp:DropDownList>
       
                   <div class="clear">
                    </div>
                </div>

                <div class="form-sec-row">
                    <label><strong>
                        Item Group :</strong></label>
  <asp:DropDownList ID="DropDownList2" runat="server" CssClass="textbox-medium1" 
                        AutoPostBack="True" onselectedindexchanged="DropDownList2_SelectedIndexChanged">
                         </asp:DropDownList>
                     <div class="clear">
                    </div>
                </div>         

                    <div class="form-sec-row">
                    <label><strong> 
                        Item Sub Group :</strong></label>
                   <asp:DropDownList ID="DropDownList3" runat="server" CssClass="textbox-medium1" 
                            onselectedindexchanged="DropDownList3_SelectedIndexChanged" 
                            AutoPostBack="True">
                         </asp:DropDownList>
                    <div class="clear">
                    </div>
                </div>

                <div class="listing"  style='width:100%; height:auto; overflow:auto;'>
                <asp:GridView id="GridView1"  Width="100%" CssClass="grid" PagerStyle-CssClass="pgr"  runat="server" 
                 AutoGenerateColumns="False" AllowPaging="True"  PageSize="100" >
                    <Columns>

                            <asp:TemplateField HeaderText="ManufacturingCompany">
                            <ItemTemplate>
                                <asp:Label ID="lblMName" runat="server" Text='<%# Bind("MName") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>

                                <asp:TemplateField HeaderText="Group">
                            <ItemTemplate>
                                <asp:Label ID="lblMedicineGroupName" runat="server" Text='<%# Bind("MedicineGroupName") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>

                                <asp:TemplateField HeaderText="SubGroup">
                            <ItemTemplate>
                                <asp:Label ID="lblSubGrName" runat="server" Text='<%# Bind("SubGrName") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>

                                <asp:TemplateField HeaderText="Medicine">
                            <ItemTemplate>
                                <asp:Label ID="lblMedicineName" runat="server" Text='<%# Bind("MedicineName") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>

                                <asp:TemplateField HeaderText="BatchNo" Visible="false">
                            <ItemTemplate>
                                <asp:Label ID="lblBatchNo" runat="server" Text="NULL"></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>

                                <asp:TemplateField HeaderText="ExpiryDate" Visible="false">
                            <ItemTemplate>
                                <asp:Label ID="lblExpiryDate" runat="server" Text="NULL"></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>

                          <asp:TemplateField HeaderText="OpeningStock" Visible="false">
                            <ItemTemplate>
                                <asp:Label ID="lblOpeningstock"   runat="server" Text="NULL" ></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>

                                        
                    </Columns>
                    <PagerStyle CssClass="pgr" />
                    <EditRowStyle BackColor="#CCFF33" />
                    <AlternatingRowStyle BackColor="#FFDB91" />
                </asp:GridView>
            </div>
                
                <div class="form-sec-row">
                    <label><strong> </strong></label>
                    <asp:Button ID="Button1" runat="server" Text="Export To Excel" Height="28px" 
                        CssClass="submit-buttonCheck" onclick="Button1_Click" />
                     <div class="clear">
                    </div>
                </div>   

                </div>
                </div>

 <%--   </ContentTemplate>
    </asp:UpdatePanel>--%>
    
</asp:Content>

