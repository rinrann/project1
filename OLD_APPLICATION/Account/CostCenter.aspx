<%@ Page Language="C#"  MasterPageFile="~/MasterAll/MasterPageAll.master" AutoEventWireup="true" CodeFile="CostCenter.aspx.cs" Inherits="Account_CostCenter" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>


<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <script language="javascript" type="text/javascript">
    </script>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <asp:UpdateProgress ID="UpdateProgress1" runat="server">
    <ProgressTemplate>
    
       <div id="aaa" class="progressBackgroundFilter">          </div>
        <div id="bbb" class="processMessage"><img alt="Loading" src="../images/pwait.gif"/>
      
              </div>
    </ProgressTemplate>
    </asp:UpdateProgress>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <div class="pageheader">
                <asp:Label ID="Label1" runat="server">Cost Centre Master</asp:Label>
            </div>
            <table width="290px" cellpadding="0" cellspacing="0">
                <tr>
                    <td>
                        <asp:Button Text="Entry" BorderStyle="None" ID="Tab1" Width="145px"  Height="40px" 
                            CssClass="Initial" runat="server" onclick="Tab1_Click"/>

                    </td>
                     <td>
                        <asp:Button Text="List" BorderStyle="None" ID="Tab2" CssClass="Initial"  Width="145px"  Height="40px" 
                    runat="server" onclick="Tab2_Click"/>

                     </td>
           
                </tr>
            </table>
            <div class="formbox">
                <asp:MultiView ID="MainView" runat="server">
                    <asp:View ID="View1" runat="server">
                        <div class="form-sec">
                            <div class="error">
                                <strong>
                                    <asp:Label ID="lblError" runat="server" Font-Bold="True" CssClass="fadeout"></asp:Label>
                                </strong>
                                <div class="clear">
                                </div>
                            </div>
                            <asp:HiddenField ID="HiddenField1" runat="server" Value="0" /><asp:HiddenField ID="HiddenField2" runat="server" Value="0" />



                            <div class="form-sec-row">
                                <label>
                                <strong>
                                Cost Code :</strong></label>
                                 <asp:TextBox ID="TextBox1" runat="server" CssClass="textbox-medium1" MaxLength="6"  ></asp:TextBox>
                                <div class="clear">
                                </div>
                            </div>

                            <div class="form-sec-row">
                                <label>
                                <strong>
                                Cost Center Name :</strong></label>
                                 <asp:TextBox ID="TextBox2" runat="server" CssClass="textbox-medium1" ></asp:TextBox>
                                <div class="clear">
                                </div>
                            </div>

                            <div class="form-sec-row">
                                <label>
                                <strong>
                                Cost Center Type :</strong></label>
                                 <asp:DropDownList ID="DropDownList1" runat="server" CssClass="textbox-medium1" AutoPostBack="true">
                         </asp:DropDownList>
                                <div class="clear">
                                </div>
                            </div>

                            <div class="form-sec-row">
                        <div class="clear">
                        </div>
                    </div>

                    <div class="form-sec-row">
                        &nbsp;&nbsp;&nbsp;<label><strong>&nbsp;</strong></label>
                        <asp:Button ID="Button1" runat="server" CssClass="submit-button"  Height="30px"
                            Text="Submit" onclick="Button1_Click" />
                        <asp:Button ID="Button2" runat="server" CssClass="submit-button"  Height="30px"
                            Text="Cancel" onclick="Button2_Click"  />
                        <div class="clear">
                        </div>
                    </div>
                    <div class="clear">
                    </div>
                </div>
               </asp:View>

                <asp:View ID="View2" runat="server">
                    <div style='width:100%;'>
                         <table width="960px" style='background-color:#FB7B13; color:#FFF;'>
                             <tr style='border:1px solid black;'>
                             <td style='width:50px;' align="center">Cost Centre Code</td>
                             <td style='width:80px;'  align="center">Cost Centre Name</td>
                             <td style='width:50px;' align="center">Cost Centre Type</td>
                             <td style='width:65px;' align="center">Edit</td>
                             <td style='width:90px;' align="center" id="code1" runat="server">Delete</td>
                             </tr>
                        </table>
                   </div>
                   <div class="listing" style='width:100%; height:500px; overflow:auto;'>
                       <asp:GridView id="GridView1"  Width="100%" CssClass="grid" PagerStyle-CssClass="pgr" OnRowDataBound="GridView1_RowDataBound"
                         DataKeyNames ="COSTCODE" runat="server" 
                         AutoGenerateColumns="False" AllowPaging="True"  PageSize="100" ShowHeader="false"
                         OnRowCommand="GridView1_RowCommand"  OnRowDeleting="GridView1_RowDeleting" SelectedRowStyle-BackColor="GreenYellow"
                         OnPageIndexChanging="GridView1_PageIndexChanging" >

                        <Columns>
                            <asp:TemplateField HeaderText="Cost Code" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="50px">
                            <ItemTemplate>
                                <asp:Label ID="CostCode" runat="server" Text='<%# Bind("COSTCODE") %>'></asp:Label>
                            </ItemTemplate>
                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="Name"  ItemStyle-HorizontalAlign="Center" ItemStyle-Width="80px">
                            <ItemTemplate>
                                <asp:Label ID="CostName" runat="server" Text='<%# Bind("CCNAME") %>'></asp:Label>
                            </ItemTemplate>
                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="Type"  ItemStyle-HorizontalAlign="Center"  ItemStyle-Width="50px" Visible="false">
                            <ItemTemplate>
                                <asp:Label ID="CostType" runat="server" Text='<%# Bind("CFILLER02") %>'></asp:Label>
                            </ItemTemplate>
                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="TypeText"  ItemStyle-HorizontalAlign="Center"  ItemStyle-Width="50px">
                            <ItemTemplate>
                                <asp:Label ID="CostTypeText" runat="server" Text='<%# Bind("TYPE") %>'></asp:Label>
                            </ItemTemplate>
                            </asp:TemplateField>

                            <asp:CommandField SelectText="Edit" ShowSelectButton="True"  ItemStyle-HorizontalAlign="Center" ItemStyle-Width="65px" ControlStyle-CssClass="temp" HeaderText="Edit" SelectImageUrl="~/Images/edit.png" ButtonType="Image">
                            <ControlStyle CssClass="temp"></ControlStyle>
                        </asp:CommandField>
                        <asp:CommandField ShowDeleteButton="True" ControlStyle-CssClass="temp"  ItemStyle-HorizontalAlign="Center"  ItemStyle-Width="90px" HeaderText="Delete" DeleteImageUrl="~/Images/delete.png" ButtonType="Image">
                            <ControlStyle CssClass="temp"></ControlStyle>
                        </asp:CommandField>
                    </Columns>
                    <PagerStyle CssClass="pgr" />
                    <EditRowStyle BackColor="#CCFF33" />
                    <AlternatingRowStyle BackColor="#FFDB91" />
                </asp:GridView>
                   </div>
                </asp:View>
            </asp:MultiView>

        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
