<%@ Page Language="C#" MasterPageFile="~/MasterAll/MasterPageAll.master" AutoEventWireup="true" CodeFile="MedicineDashboard.aspx.cs" Inherits="Medicine_MedicineDashboard" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <style>
.BigWidth
{
    width:1000px; 
}
</style>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    
    <script language="javascript" type="text/javascript">
        $(function () {
            /* date picker event*/
            $('.datepicker').datepicker({
                dateFormat: "dd/mm/yy",
                changeMonth: true,
                changeYear: true,
                //yearRange: '1900:' + new Date().getFullYear(),
                yearRange: '1900:2900',
                showOn: "button",
                buttonImage: "../images/calender.png",
                //buttonImage: "../images/green-button.gif",
                buttonImageOnly: true,
                showAnim: "fold"
            });
        });

        

        $(document).ready(function () {

            Calling();

            var prm = Sys.WebForms.PageRequestManager.getInstance();
            if (prm != null) {
                prm.add_endRequest(function (sender, e) {
                    if (sender._postBackSettings.panelsToUpdate != null) {

                        Calling();

                    }
                });
            };
        });

        function Calling() {
            var date = new Date();
            $("input[id$='frmdt']").datepicker({ dateFormat: 'dd/mm/yy' });

            $("input[id$='todt']").datepicker({ dateFormat: 'dd/mm/yy' });
        }

     </script>

     <asp:UpdateProgress ID="UpdateProgress1" runat="server">
    <ProgressTemplate>
    
       <div id="aaa" class="progressBackgroundFilter">          </div>
        <div id="bbb" class="processMessage"><img alt="Loading" src="../images/pwait.gif"/>
      
              </div>
    </ProgressTemplate>
    </asp:UpdateProgress>

     <asp:UpdatePanel ID="UpdatePanel1" runat="server">
         <ContentTemplate>
             <div class="pageheader" style="width:1150px;">
                 <asp:Label ID="Label1" runat="server">Medicine DashBoard</asp:Label>
             </div>

             <div class="formbox"  style='width:1150px;'>
                    <div class="form-sec">
                        <div class="error">
                            <strong><asp:Label ID="lblError" runat="server" Font-Bold="True"></asp:Label></strong>
                            <div class="clear"></div>
                        </div>
                    </div>

                    <%--<table cellpadding="0" cellspacing="0"  title="Search" width="100%">
                        <tr>
                            <td>  <label><strong>Manufacturer:</strong></label> </td>
                           <td>  
                                <asp:DropDownList ID="ddlMfg" runat="server" Width="150px" CssClass="textbox-medium1"  AutoPostBack="true" onselectedindexchanged="ddlMfg_SelectedIndexChanged"></asp:DropDownList>
                                <div class="clear">
                                </div>
                           </td>
                            <td>  <label><strong>Group:</strong></label> </td>
                            <td>
                                <asp:DropDownList ID="ddlMediGrp" CssClass="textbox-medium1" AutoPostBack="true" runat="server" Width="150px"  onselectedindexchanged="ddlMediGrp_SelectedIndexChanged"></asp:DropDownList>
                                <div class="clear">
                                </div>
                            </td>
                            <td>  <label><strong>Sub Group:</strong></label> </td>
                            <td>
                                <asp:DropDownList ID="ddlMediSubGrp" CssClass="textbox-medium1" AutoPostBack="true" runat="server" Width="150px"  onselectedindexchanged="ddlMediSubGrp_SelectedIndexChanged"></asp:DropDownList>
                                <div class="clear">
                                </div>
                            </td>
                           <td>  <label><strong>Medicine Name:</strong></label> </td>
                           <td>  
                                <asp:DropDownList ID="ddlMedi" CssClass="textbox-medium1" runat="server" Width="150px" AutoPostBack="true"  onselectedindexchanged="ddlMed_SelectedIndexChanged">
                            </asp:DropDownList>
                                <div class="clear">
                                </div>
                           </td>
               
                           <td>  <label><strong>&nbsp;&nbsp;From Date:</strong></label> </td>
                           <td>  
                               <asp:TextBox ID="frmdt" runat="server" CssClass="textbox-medium2" Width="100px" OnTextChanged="frmdt_textChange" AutoPostBack="true"></asp:TextBox>
                                <div class="clear">
                                </div>
                           </td>
                            <td>  <label id="Label2"><strong>&nbsp;&nbsp;To Date :</strong></label> </td>
                           <td>  
                               <asp:TextBox ID="todt" runat="server" CssClass="textbox-medium2" Width="100px" OnTextChanged="todt_textChange" AutoPostBack="true"></asp:TextBox>
                                <div class="clear">
                                </div>
                           </td>
                            <td>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</td>
                            <td align="right">&nbsp;&nbsp;
                                <asp:Button ID="Button1" runat="server" Text="Search" CssClass="submit-generate" OnClick="Button1_Click" />
                            </td>
                        </tr>


                </table>--%>
             </div>
              <div class="formbox"  style='width:1150px;'>
                <div   style='width:100%;'>
                    <center>
                            <table width="100%" style='background-color:#FB7B13; color:#FFF;'>
                               <tr>
                                    <td style='width:30px;' align="center">Sl.No</td>
                                    <%--<td style='width:90px;'  align="center">Group</td> 
                                    <td style='width:90px; ' align="center">Sub Group</td>--%>
                                    <td style='width:90px;' align="center">Medicine</td>
                                    <%--<td style='width:90px;' align="center">Manufacturer</td>
                                    <td style='width:90px;' align="center">Supplier</td>--%>
                                    <td style='width:90px;' align="center">Purchase Date</td>
                                    <td style='width:90px;' align="center">Batch No</td>
                                    <td style='width:90px;' align="center">Expire Date</td>
                                    <td style='width:90px;' align="center">Current Stock</td>
                                    <td style='width:90px;' align="center"></td>
                                </tr>
                            </table>
                            
                        </center>
                    </div>
                    <div class="listing" style='height:400px; overflow:auto;'>
                            <asp:GridView id="GridView1"  CssClass="grid" PagerStyle-CssClass="pgr" 
               DataKeyNames ="MedicineID" runat="server" SelectedRowStyle-BackColor="GreenYellow"
                 AutoGenerateColumns="False" AllowPaging="True" PageSize="100"
                                onrowcommand="GridView1_RowCommand" 
                ShowHeader="false" onpageindexchanging="GridView1_PageIndexChanging" 
               onrowdatabound="GridView1_RowDataBound" Width="100%">          
				 <RowStyle HorizontalAlign="Center" />  
                                <Columns>
                                        <asp:TemplateField HeaderText="Sl.No"   ItemStyle-Width="30px"  ItemStyle-HorizontalAlign="Center"> 
                                        <ItemTemplate>
                                            <asp:Label ID="lblSlno" runat="server" Width="30px"></asp:Label>
                                        </ItemTemplate>
                                        </asp:TemplateField>
                                        
                                    <%--<asp:TemplateField HeaderText="Group Name"   ItemStyle-Width="90px"  ItemStyle-HorizontalAlign="Center"> 
                                        <ItemTemplate>
                                            <asp:Label ID="lblgrname" runat="server" Width="90px" Text='<%# Bind("MedicineGroupName") %>'></asp:Label>
                                        </ItemTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="sub Group Name"   ItemStyle-Width="90px"  ItemStyle-HorizontalAlign="Center"> 
                                        <ItemTemplate>
                                            <asp:Label ID="lblsgrname" runat="server" Width="90px" Text='<%# Bind("SubGrName") %>'></asp:Label>
                                        </ItemTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Regn No"   ItemStyle-Width="90px"  ItemStyle-HorizontalAlign="Center" Visible="false">                      
                                            <ItemTemplate>
                             
							                    <asp:Label ID="lblmedid" runat="server" Width="90px" Text='<%# Bind("MedicineID") %>'></asp:Label>
                                                      
                                            </ItemTemplate>
                                        </asp:TemplateField>--%>
                                        <asp:TemplateField HeaderText="Medicine Name"   ItemStyle-Width="90px"  ItemStyle-HorizontalAlign="Center"> 
                                        <ItemTemplate>
                                            <asp:Label ID="lblname" runat="server" Width="90px" Text='<%# Bind("MedicineName") %>'></asp:Label>
                                        </ItemTemplate>
                                        </asp:TemplateField>
                                        <%--<asp:TemplateField HeaderText="Manufacturer"   ItemStyle-Width="90px"  ItemStyle-HorizontalAlign="Center"> 
                                        <ItemTemplate>
                                            <asp:Label ID="lblmanu" runat="server" Width="90px" Text='<%# Bind("MName") %>'></asp:Label>
                                        </ItemTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Suplrno"   ItemStyle-Width="90px"  ItemStyle-HorizontalAlign="Center" Visible="false">                      
                                            <ItemTemplate>
                             
							                    <asp:Label ID="lblsuplrno" runat="server" Width="90px" Text='<%# Bind("SCode") %>'></asp:Label>
                                                      
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        
                                        <asp:TemplateField HeaderText="Supplier"   ItemStyle-Width="90px"  ItemStyle-HorizontalAlign="Center"> 
                                        <ItemTemplate>
                                            <asp:Label ID="lblsup" runat="server" Width="90px" Text=''></asp:Label>
                                        </ItemTemplate>
                                        </asp:TemplateField>--%>

                                        <asp:TemplateField HeaderText="Purchase Date"   ItemStyle-Width="90px"  ItemStyle-HorizontalAlign="Center"> 
                                        <ItemTemplate>
                                            <asp:Label ID="lblpurdt" runat="server" Width="90px" Text='<%# Bind("PurchaseDate") %>'></asp:Label>
                                        </ItemTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Batch No"  ItemStyle-Width="90px"  ItemStyle-HorizontalAlign="Center"> 
                                        <ItemTemplate>
                                            <asp:Label ID="lblbatch" runat="server" Width="90px" Text='<%# Bind("BatchNo") %>'></asp:Label>
                                        </ItemTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Expire Date"  ItemStyle-Width="90px"  ItemStyle-HorizontalAlign="Center"> 
                                        <ItemTemplate>
                                            <asp:Label ID="lblexpire" runat="server" Width="90px" Text='<%# Bind("ExpiryDate") %>'></asp:Label>
                                        </ItemTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Current Stock" ItemStyle-Width="90px"  ItemStyle-HorizontalAlign="Center"> 
                                        <ItemTemplate>
                                            <asp:Label ID="lblstock" runat="server" Width="90px" style="text-align: right;"  Text='<%# Bind("curstock") %>'></asp:Label>
                                        </ItemTemplate>
                                        </asp:TemplateField>
                                        
                                        <asp:TemplateField HeaderText="Purchase Medicine" ItemStyle-Width="90px"  ItemStyle-HorizontalAlign="Center"> 
                                        <ItemTemplate>
                                                <asp:LinkButton ID="linkButton" CommandName="Select" CommandArgument='<%#Eval("MedicineID") %>' runat="server">
                                                    <asp:Label ID="purchase" runat="server" Text='Purchase'></asp:Label>
                                                </asp:LinkButton>
                                        </ItemTemplate>
                                        </asp:TemplateField>
                                                
                                </Columns>
                            <PagerStyle CssClass="pgr" />
                            <EditRowStyle BackColor="#CCFF33" />
                            <AlternatingRowStyle BackColor="#FFDB91" />
                            <SelectedRowStyle BackColor="GreenYellow" />
                            </asp:GridView>
                    </div>
              </div>
         </ContentTemplate>
     </asp:UpdatePanel>
</asp:Content>
