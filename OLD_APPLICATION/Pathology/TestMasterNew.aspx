<%@ Page Language="C#" MasterPageFile="~/MasterAll/MasterPageAll.master" AutoEventWireup="true" CodeFile="TestMasterNew.aspx.cs" Inherits="Pathology_TestMasterNew" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
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
    <asp:UpdatePanel ID="UpdatePanel1" runat="server" style="">
        <ContentTemplate>
            <div class="pageheader">
                <asp:Label ID="Label1" runat="server">Investigation Master</asp:Label>
            </div>
            <div style='width: 100%;'>
                <div class="formbox" style="width: 800px;" id="45">
                    <div class="form-sec">
                        <table width="100%">
                            <tr>
                                <td>
                                    <asp:Label Width="100px" ID="lblComp" runat="server">Group Name</asp:Label></td>
                                <td>
                                    <asp:DropDownList ID="ddlgroup" runat="server" CssClass="textbox-medium1" AutoPostBack="true" OnSelectedIndexChanged="ddlgroup_SelectedIndexChanged">
                                    </asp:DropDownList>
                                    <asp:TextBox ID="txtDeptCode" Visible="false" runat="server"></asp:TextBox>
                                    <div class="clear"></div>
                                </td>
                                <td>
                                    <asp:Button ID="btnlist" runat="server" Text="View List" OnClick="btnlist_Click" Width="150px" />
                                </td>
                            </tr>
                        </table>
                    </div>
                </div>
            </div>
            <div class="listing" style='width: 100%; height: 450px; overflow: auto;'>
                <asp:GridView ID="GridView1" CssClass="grid" PagerStyle-CssClass="pgr" ShowFooter="true"
                    DataKeyNames="TestId" runat="server" AutoGenerateColumns="False" SelectedRowStyle-BackColor="GreenYellow"
                    AllowPaging="True" PageSize="100" Width="100%"
                    OnPageIndexChanging="GridView1_PageIndexChanging"
                    OnRowCommand="GridView1_RowCommand" OnRowCancelingEdit="GridView1_RowCancelingEdit"
                    OnRowEditing="GridView1_RowEditing" OnRowDataBound="GridView1_RowDataBound" OnRowUpdating="GridView1_RowUpdating">
                    <RowStyle HorizontalAlign="Center" />
                    <Columns>
                        <asp:TemplateField HeaderText="Name" ItemStyle-HorizontalAlign="Left" ItemStyle-Width="20%" HeaderStyle-Width="20%">
                            <ItemTemplate>
                                <asp:Label ID="lblname" runat="server" Text='<%# Bind("TestName") %>'></asp:Label>
                                <asp:Label ID="lblCode" runat="server" Text='<%# Bind("TestId") %>' Visible="false"></asp:Label>
                            </ItemTemplate>
                            <EditItemTemplate>
                                <asp:Label ID="lblCodeOld" runat="server" Text='<%# Bind("TestId") %>' Visible="false"></asp:Label>
                                <asp:TextBox ID="txtNameOld" runat="server" Text='<%# Bind("TestName") %>' CssClass="textbox-medium1" Style="width: 98%;"></asp:TextBox>
                            </EditItemTemplate>
                            <FooterTemplate>
                                <asp:TextBox ID="txtNameNew" runat="server" Text="" CssClass="textbox-medium1" Style="width: 98%;"></asp:TextBox>
                            </FooterTemplate>
                        </asp:TemplateField>

                           <asp:TemplateField HeaderText="Details" ItemStyle-HorizontalAlign="Left" ItemStyle-Width="20%" HeaderStyle-Width="20%">
                            <ItemTemplate>
                                <asp:Label ID="lblDetails" runat="server" Text='<%# Bind("Details") %>'></asp:Label> 
                            </ItemTemplate>
                            <EditItemTemplate> 
                                <asp:TextBox ID="txtDetailsOld" runat="server" Text='<%# Bind("Details") %>' CssClass="textbox-medium1" Style="width: 300PX;height:70PX" TextMode="MultiLine"></asp:TextBox>
                            </EditItemTemplate>
                            <FooterTemplate>
                                <asp:TextBox ID="txtDetailsNew" runat="server" Text="" CssClass="textbox-medium1" Style="width: 300PX;height:70PX;" TextMode="MultiLine"></asp:TextBox>
                            </FooterTemplate>
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="INR" ItemStyle-HorizontalAlign="Right" ItemStyle-Width="7%" HeaderStyle-Width="7%">
                            <ItemTemplate>
                                <asp:Label ID="lblInr" runat="server" Text='<%# Bind("Cost") %>'></asp:Label>

                            </ItemTemplate>
                            <EditItemTemplate>
                                <asp:TextBox ID="txtInrOld" runat="server" Text='<%# Bind("Cost") %>' Style="text-align: right; width: 98%;" CssClass="textbox-medium1"></asp:TextBox>
                            </EditItemTemplate>
                            <FooterTemplate>
                                <asp:TextBox ID="txtInrNew" runat="server" Text="0.00" Style="text-align: right; width: 98%;" CssClass="textbox-medium1"></asp:TextBox>
                            </FooterTemplate>
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="Bio. Ref. Interval" ItemStyle-HorizontalAlign="Left" ItemStyle-Width="7%" HeaderStyle-Width="7%">
                            <ItemTemplate>
                                <asp:Label ID="lblNormalRange" runat="server" Text='<%# Bind("NormalRange") %>'></asp:Label>
                            </ItemTemplate>
                            <EditItemTemplate>
                                <asp:TextBox ID="txtNormalRangeOld" runat="server" Text='<%# Bind("NormalRange") %>' Style="width: 98%;" CssClass="textbox-medium1"></asp:TextBox>
                            </EditItemTemplate>
                            <FooterTemplate>
                                <asp:TextBox ID="txtNormalRangeNew" runat="server" Text="" Style="width: 98%;" CssClass="textbox-medium1"></asp:TextBox>
                            </FooterTemplate>
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="Unit" ItemStyle-HorizontalAlign="Left" ItemStyle-Width="7%" HeaderStyle-Width="7%">
                            <ItemTemplate>
                                <asp:Label ID="lblUnit" runat="server" Text='<%# Bind("Unit") %>'></asp:Label>
                            </ItemTemplate>
                            <EditItemTemplate>
                                <asp:TextBox ID="txtUnitOld" runat="server" Text='<%# Bind("Unit") %>' Style="width: 98%;" CssClass="textbox-medium1"></asp:TextBox>
                            </EditItemTemplate>
                            <FooterTemplate>
                                <asp:TextBox ID="txtUnitNew" runat="server" Text="" Style="width: 98%;" CssClass="textbox-medium1"></asp:TextBox>
                            </FooterTemplate>
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="Method" ItemStyle-HorizontalAlign="Left" ItemStyle-Width="7%" HeaderStyle-Width="7%">
                            <ItemTemplate>
                                <asp:Label ID="lblMethod" runat="server" Text='<%# Bind("Method") %>'></asp:Label>
                            </ItemTemplate>
                            <EditItemTemplate>
                                <asp:TextBox ID="txtMethodOld" runat="server" Text='<%# Bind("Method") %>' Style="width: 98%;" CssClass="textbox-medium1"></asp:TextBox>
                            </EditItemTemplate>
                            <FooterTemplate>
                                <asp:TextBox ID="txtMethodNew" runat="server" Style="width: 98%;" CssClass="textbox-medium1"></asp:TextBox>
                            </FooterTemplate>
                        </asp:TemplateField>


                        <asp:TemplateField HeaderText="Consultant Name 1" ItemStyle-HorizontalAlign="left" ItemStyle-Width="10%" HeaderStyle-Width="7%">
                            <ItemTemplate>
                                <asp:Label ID="lblconsullt_name1" runat="server" Text='<%# Bind("docname1") %>'></asp:Label>
                            </ItemTemplate>
                            <EditItemTemplate>
                                <asp:Label ID="lblconsullt1_Old" runat="server" Text='<%# Bind("consullt_name1") %>' Visible="false"></asp:Label>
                                <asp:DropDownList ID="ddlConsult1_Old" runat="server" Style="width: 98px;" CssClass="textbox-medium1"></asp:DropDownList>
                            </EditItemTemplate>
                            <FooterTemplate>
                                <%--<asp:TextBox ID="txtconsullt_name1New" runat="server" Text="" style="width:98%;" CssClass="textbox-medium1"></asp:TextBox>--%>
                                <asp:DropDownList runat="server" ID="ddlConsult1_New" Style="width: 98px"></asp:DropDownList>
                            </FooterTemplate>
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="Consultant Name 2" ItemStyle-HorizontalAlign="left" ItemStyle-Width="10%" HeaderStyle-Width="7%">
                            <ItemTemplate>
                                <asp:Label ID="lblconsullt_name2" runat="server" Text='<%# Bind("docname2") %>'></asp:Label>
                            </ItemTemplate>
                            <EditItemTemplate>
                                <asp:Label ID="lblconsullt2_Old" runat="server" Text='<%# Bind("consullt_name2") %>' Visible="false"></asp:Label>
                                <asp:DropDownList ID="ddlConsult2_Old" runat="server" Style="width: 98px;" CssClass="textbox-medium1"></asp:DropDownList>
                            </EditItemTemplate>
                            <FooterTemplate>
                                <%--<asp:TextBox ID="txtconsullt_name2New" runat="server" Text="" style="width:98%;" CssClass="textbox-medium1"></asp:TextBox>--%>
                                <asp:DropDownList runat="server" ID="ddlConsult2_New" Style="width: 98px;"></asp:DropDownList>
                            </FooterTemplate>
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="Consultant Name 3" ItemStyle-HorizontalAlign="left" ItemStyle-Width="10%" HeaderStyle-Width="7%">
                            <ItemTemplate>
                                <asp:Label ID="lblconsullt_name3" runat="server" Text='<%# Bind("docname3") %>'></asp:Label>
                            </ItemTemplate>
                            <EditItemTemplate>
                                <asp:Label ID="lblconsullt3_Old" runat="server" Text='<%# Bind("consullt_name3") %>' Visible="false"></asp:Label>
                                <asp:DropDownList ID="ddlConsult3_Old" runat="server" Style="width: 98px;" CssClass="textbox-medium1"></asp:DropDownList>
                            </EditItemTemplate>
                            <FooterTemplate>
                                <%--<asp:TextBox ID="txtconsullt_name3New" runat="server" Text="" style="width:98%;" CssClass="textbox-medium1"></asp:TextBox>--%>
                                <asp:DropDownList runat="server" ID="ddlConsult3_New" Style="width: 98px;"></asp:DropDownList>
                            </FooterTemplate>
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="Consultant" ItemStyle-HorizontalAlign="Right" ItemStyle-Width="7%" HeaderStyle-Width="7%">
                            <ItemTemplate>
                                <asp:Label ID="lblCons" runat="server" Text='<%# Bind("ConsultantChrg") %>'></asp:Label>

                            </ItemTemplate>
                            <EditItemTemplate>
                                <asp:TextBox ID="txtConsOld" runat="server" Text='<%# Bind("ConsultantChrg") %>' Style="text-align: right; width: 98%;" CssClass="textbox-medium1"></asp:TextBox>
                            </EditItemTemplate>
                            <FooterTemplate>
                                <asp:TextBox ID="txtConsNew" runat="server" Text="0.00" Style="text-align: right; width: 98%;" CssClass="textbox-medium1"></asp:TextBox>
                            </FooterTemplate>
                        </asp:TemplateField>


                        <asp:TemplateField HeaderText="Company" ItemStyle-HorizontalAlign="Right" ItemStyle-Width="7%" HeaderStyle-Width="7%">
                            <ItemTemplate>
                                <asp:Label ID="lblComp" runat="server" Text='<%# Bind("CompanyChrg") %>'></asp:Label>

                            </ItemTemplate>
                            <EditItemTemplate>
                                <asp:TextBox ID="txtCompOld" runat="server" Text='<%# Bind("CompanyChrg") %>' Style="text-align: right; width: 98%;" CssClass="textbox-medium1"></asp:TextBox>
                            </EditItemTemplate>
                            <FooterTemplate>
                                <asp:TextBox ID="txtCompNew" runat="server" Text="0.00" Style="text-align: right; width: 98%;" CssClass="textbox-medium1"></asp:TextBox>
                            </FooterTemplate>
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="Single" ItemStyle-HorizontalAlign="Right" ItemStyle-Width="7%" HeaderStyle-Width="7%">
                            <ItemTemplate>
                                <asp:Label ID="lblSingle" runat="server" Text='<%# Bind("SingleChrg") %>'></asp:Label>

                            </ItemTemplate>
                            <EditItemTemplate>
                                <asp:TextBox ID="txtSingleOld" runat="server" Text='<%# Bind("SingleChrg") %>' Style="text-align: right; width: 98%;" CssClass="textbox-medium1"></asp:TextBox>
                            </EditItemTemplate>
                            <FooterTemplate>
                                <asp:TextBox ID="txtSingleNew" runat="server" Text="0.00" Style="text-align: right; width: 98%;" CssClass="textbox-medium1"></asp:TextBox>
                            </FooterTemplate>
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="Twins" ItemStyle-HorizontalAlign="Right" ItemStyle-Width="7%" HeaderStyle-Width="7%">
                            <ItemTemplate>
                                <asp:Label ID="lblTwins" runat="server" Text='<%# Bind("TwinsChrg") %>'></asp:Label>

                            </ItemTemplate>
                            <EditItemTemplate>
                                <asp:TextBox ID="txtTwinsOld" runat="server" Text='<%# Bind("TwinsChrg") %>' Style="text-align: right; width: 98%;" CssClass="textbox-medium1"></asp:TextBox>
                            </EditItemTemplate>
                            <FooterTemplate>
                                <asp:TextBox ID="txtTwinsNew" runat="server" Text="0.00" Style="text-align: right; width: 98%;" CssClass="textbox-medium1"></asp:TextBox>
                            </FooterTemplate>
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="Lab Cost" ItemStyle-HorizontalAlign="Right" ItemStyle-Width="7%" HeaderStyle-Width="7%">
                            <ItemTemplate>
                                <asp:Label ID="lblLab" runat="server" Text='<%# Bind("LabChrg") %>'></asp:Label>

                            </ItemTemplate>
                            <EditItemTemplate>
                                <asp:TextBox ID="txtLabOld" runat="server" Text='<%# Bind("LabChrg") %>' Style="text-align: right; width: 98%;" CssClass="textbox-medium1"></asp:TextBox>
                            </EditItemTemplate>
                            <FooterTemplate>
                                <asp:TextBox ID="txtLabNew" runat="server" Text="0.00" Style="text-align: right; width: 98%;" CssClass="textbox-medium1"></asp:TextBox>
                            </FooterTemplate>
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="OT Charge" ItemStyle-HorizontalAlign="Right" ItemStyle-Width="7%" HeaderStyle-Width="7%">
                            <ItemTemplate>
                                <asp:Label ID="lblOt" runat="server" Text='<%# Bind("OtCharge") %>'></asp:Label>

                            </ItemTemplate>
                            <EditItemTemplate>
                                <asp:TextBox ID="txtOtOld" runat="server" Text='<%# Bind("OtCharge") %>' Style="text-align: right; width: 98%;" CssClass="textbox-medium1"></asp:TextBox>
                            </EditItemTemplate>
                            <FooterTemplate>
                                <asp:TextBox ID="txtOtNew" runat="server" Text="0.00" Style="text-align: right; width: 98%;" CssClass="textbox-medium1"></asp:TextBox>
                            </FooterTemplate>
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="Medicines" ItemStyle-HorizontalAlign="Right" ItemStyle-Width="7%" HeaderStyle-Width="7%">
                            <ItemTemplate>
                                <asp:Label ID="lblMed" runat="server" Text='<%# Bind("MedicinesChrg") %>'></asp:Label>

                            </ItemTemplate>
                            <EditItemTemplate>
                                <asp:TextBox ID="txtMedOld" runat="server" Text='<%# Bind("MedicinesChrg") %>' Style="text-align: right; width: 98%;" CssClass="textbox-medium1"></asp:TextBox>
                            </EditItemTemplate>
                            <FooterTemplate>
                                <asp:TextBox ID="txtMedNew" runat="server" Text="0.00" Style="text-align: right; width: 98%;" CssClass="textbox-medium1"></asp:TextBox>
                            </FooterTemplate>
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="Biopsy" ItemStyle-HorizontalAlign="Right" ItemStyle-Width="7%" HeaderStyle-Width="7%">
                            <ItemTemplate>
                                <asp:Label ID="lblBio" runat="server" Text='<%# Bind("BiopsyChrg") %>'></asp:Label>

                            </ItemTemplate>
                            <EditItemTemplate>
                                <asp:TextBox ID="txtBioOld" runat="server" Text='<%# Bind("BiopsyChrg") %>' Style="text-align: right; width: 98%;" CssClass="textbox-medium1"></asp:TextBox>
                            </EditItemTemplate>
                            <FooterTemplate>
                                <asp:TextBox ID="txtBioNew" runat="server" Text="0.00" Style="text-align: right; width: 98%;" CssClass="textbox-medium1"></asp:TextBox>
                            </FooterTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="IVF/ Andro Lab" ItemStyle-HorizontalAlign="Right" ItemStyle-Width="7%" HeaderStyle-Width="7%">
                            <ItemTemplate>
                                <asp:Label ID="lblIvf" runat="server" Text='<%# Bind("IVFLAbChrg") %>'></asp:Label>

                            </ItemTemplate>
                            <EditItemTemplate>
                                <asp:TextBox ID="txtIvfOld" runat="server" Text='<%# Bind("IVFLAbChrg") %>' Style="text-align: right; width: 98%;" CssClass="textbox-medium1"></asp:TextBox>
                            </EditItemTemplate>
                            <FooterTemplate>
                                <asp:TextBox ID="txtIvfNew" runat="server" Text="0.00" Style="text-align: right; width: 98%;" CssClass="textbox-medium1"></asp:TextBox>
                            </FooterTemplate>
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="Offer Rate 1" ItemStyle-HorizontalAlign="Right" ItemStyle-Width="7%" HeaderStyle-Width="7%" Visible="false">
                            <ItemTemplate>
                                <asp:Label ID="lblOfrRt1" runat="server" Text='<%# Bind("OfferRate1") %>'></asp:Label>

                            </ItemTemplate>
                            <EditItemTemplate>
                                <asp:TextBox ID="txtOfrRt1Old" runat="server" Text='<%# Bind("OfferRate1") %>' Style="text-align: right; width: 98%;" CssClass="textbox-medium1"></asp:TextBox>
                            </EditItemTemplate>
                            <FooterTemplate>
                                <asp:TextBox ID="txtOfrRt1New" runat="server" Text="0.00" Style="text-align: right; width: 98%;" CssClass="textbox-medium1"></asp:TextBox>
                            </FooterTemplate>
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="Offer Rate 2" ItemStyle-HorizontalAlign="Right" ItemStyle-Width="7%" HeaderStyle-Width="7%" Visible="false">
                            <ItemTemplate>
                                <asp:Label ID="lblOfrRt2" runat="server" Text='<%# Bind("OfferRate2") %>'></asp:Label>

                            </ItemTemplate>
                            <EditItemTemplate>
                                <asp:TextBox ID="txtOfrRt2Old" runat="server" Text='<%# Bind("OfferRate2") %>' Style="text-align: right; width: 98%;" CssClass="textbox-medium1"></asp:TextBox>
                            </EditItemTemplate>
                            <FooterTemplate>
                                <asp:TextBox ID="txtOfrRt2New" runat="server" Text="0.00" Style="text-align: right; width: 98%;" CssClass="textbox-medium1"></asp:TextBox>
                            </FooterTemplate>
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="Offer Rate 3" ItemStyle-HorizontalAlign="Right" ItemStyle-Width="7%" HeaderStyle-Width="7%" Visible="false">
                            <ItemTemplate>
                                <asp:Label ID="lblOfrRt3" runat="server" Text='<%# Bind("OfferRate3") %>'></asp:Label>

                            </ItemTemplate>
                            <EditItemTemplate>
                                <asp:TextBox ID="txtOfrRt3Old" runat="server" Text='<%# Bind("OfferRate3") %>' Style="text-align: right; width: 98%;" CssClass="textbox-medium1"></asp:TextBox>
                            </EditItemTemplate>
                            <FooterTemplate>
                                <asp:TextBox ID="txtOfrRt3New" runat="server" Text="0.00" Style="text-align: right; width: 98%;" CssClass="textbox-medium1"></asp:TextBox>
                            </FooterTemplate>
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="Command" ItemStyle-Width="10%" HeaderStyle-Width="10%">
                            <EditItemTemplate>
                                <asp:Button ID="btnUpdate" runat="server" CommandName="Update" Text="Upd" class="submit-button" Width="40%" />
                                <asp:Button ID="btnCancel" runat="server" CommandName="Cancel" Text="Cnl" class="submit-button" Width="40%" />
                            </EditItemTemplate>
                            <FooterTemplate>
                                <asp:Button ID="btnInsert" runat="server" CommandName="Insert" Text="Add" class="submit-button" Width="95%" />
                            </FooterTemplate>
                            <ItemTemplate>
                                <asp:Button ID="btnEdit" runat="server" CommandName="EDIT" Text="Edit" class="submit-button" Width="95%" />
                            </ItemTemplate>
                            <FooterStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                            <ItemStyle Width="20%" />
                        </asp:TemplateField>
                    </Columns>
                    <PagerStyle CssClass="pgr" />
                    <EditRowStyle BackColor="#CCFF33" />
                    <AlternatingRowStyle BackColor="#FFDB91" />
                </asp:GridView>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>

</asp:Content>
