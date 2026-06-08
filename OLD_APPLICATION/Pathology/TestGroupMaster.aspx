<%@ Page Language="C#" MasterPageFile="~/MasterAll/MasterPageAll.master" AutoEventWireup="true" CodeFile="TestGroupMaster.aspx.cs" Inherits="Pathology_TestGroupMaster" %>
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

    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
    <ContentTemplate>
          <div class="pageheader">
            <asp:Label ID="Label1" runat="server">Investigation Group Master</asp:Label>
          </div>
        <table width="290px" cellpadding="0" cellspacing="0">
            <tr>
                <td>
                    <asp:Button Text="Entry" BorderStyle="None" ID="Tab1" Width="145px"  Height="40px" CssClass="Initial" runat="server" onclick="Tab1_Click"/>
                </td>
                <td>
                    <asp:Button Text="List" BorderStyle="None" ID="Tab2" CssClass="Initial"  Width="145px"  Height="40px" runat="server" onclick="Tab2_Click"/>
                </td>
            </tr>
        </table>
        <div class="formbox">
            <asp:MultiView ID="MainView" runat="server">
                <asp:View ID="View1" runat="server">
                    <div class="form-sec">
                        <div class="error">
                            <strong><asp:Label ID="lblError" runat="server" Font-Bold="True"></asp:Label>
                            </strong>
                            <div class="clear"></div>
                        </div>
                    </div>

                    

                    <div class="form-sec-row">
                        <label><strong>Group Code :</strong></label>
                       <asp:TextBox ID="txtcode" runat="server" CssClass="textbox-medium1" MaxLength="6"></asp:TextBox>
                        <div class="clear">  </div>
                    </div>

                    <div class="form-sec-row">
                        <label><strong>Group Name :</strong></label>
                        <asp:TextBox ID="txtname" runat="server" CssClass="textbox-medium1"></asp:TextBox>
                        <div class="clear"></div>
                    </div>

                    <div class="form-sec-row">
                        <label><strong>Department :</strong></label>
                        <asp:DropDownList ID="DropDownList1" runat="server" CssClass="textbox-medium1">
                        </asp:DropDownList>
                        <div class="clear"></div>
                    </div>
                    <div class="form-sec-row">
                        <label><strong>Test Type :</strong></label>
                        <asp:CheckBox runat="server" ID="chkcon" Visible="false" />
                        <asp:DropDownList ID="ddltesttype" runat="server" CssClass="textbox-medium1">
                             <asp:ListItem Value="">Select One</asp:ListItem>
                            <asp:ListItem Value="C">Consultant</asp:ListItem>
                            <asp:ListItem Value="T">Test</asp:ListItem>
                            <asp:ListItem Value="DIG">Digonistic</asp:ListItem>
                            <asp:ListItem Value="PRC">Procedure</asp:ListItem>
                        </asp:DropDownList>
                        <div class="clear"></div>
                    </div>
                    <div class="form-sec-row">
                        <label><strong>&nbsp;</strong></label>
                        <asp:Button ID="Button3" runat="server"  Text="Submit" CssClass="submit-button"  Height="28px" 
                            onclick="Button3_Click" />
                        <asp:Button ID="Button4" runat="server" Text="Cancel"  CssClass="submit-button"  Height="28px"
                            onclick="Button4_Click" />
                        <asp:HiddenField ID="HiddenField1" runat="server" />
                        <div class="clear"></div>
                    </div>
                </asp:View>
                <asp:View ID="View2" runat="server">
                    <div style='width:100%;'>
                        <div class="formbox" style="width:800px;" id="45">
                            <div class="form-sec">
                                   <table width="100%">
                                       <tr>
                                           <td><asp:Label Width="100px" ID="lblComp" runat="server" >Group Name</asp:Label></td>
                                           <td>
                                               <asp:TextBox ID="txtgrpname" runat="server"   Width=""  Height="" CssClass="textbox-medium1"></asp:TextBox>
                                                <asp:Label ID="lbltxt" runat="server" Text=" " 
                                                    style="font-size: small; font-family: 'Times New Roman', Times, serif;"></asp:Label>
                                                <cc1:AutoCompleteExtender ServiceMethod="SearchTestGroup" OnClientItemSelected="autoCompleteEx_ItemSelected" MinimumPrefixLength="1"
                                                                    CompletionInterval="100" EnableCaching="false" CompletionSetCount="10" TargetControlID="txtgrpname" ID="AutoCompleteExtender2" runat="server" 
                                                                       FirstRowSelected = "false" ></cc1:AutoCompleteExtender>
                                                <div class="clear"></div>
                                           </td>
                                           <td>
                                               <asp:Button ID="Button1" runat="server" Text="Search"   Height="28px" CssClass="submit-button" onclick="Button1_Click" />
                                           </td>
                                        </tr>
                                    </table>
                            </div>
                        </div>
                    </div>
                    <div class="listing"  style='width:100%; height:250px; overflow:auto;'>
                        <asp:GridView id="GridView1"  CssClass="grid" PagerStyle-CssClass="pgr" 
                         DataKeyNames ="ProfileCode" runat="server" AutoGenerateColumns="False"  SelectedRowStyle-BackColor="GreenYellow"
                         AllowPaging="True" PageSize ="100" Width="100%" 
                         onpageindexchanging="GridView1_PageIndexChanging"
                         onrowcommand="GridView1_RowCommand" onrowdeleting="GridView1_RowDeleting">
                            <RowStyle HorizontalAlign="Center" />
                            <Columns>
                                <asp:TemplateField HeaderText="Group Code" ItemStyle-HorizontalAlign="Left">
                                    <ItemTemplate>
                                        <asp:Label ID="lblcode" runat="server" Text='<%# Bind("ProfileCode") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Group Name" ItemStyle-HorizontalAlign="Left">
                                    <ItemTemplate>
                                        <asp:Label ID="lblname" runat="server" Text='<%# Bind("ProfileName") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Department Name" ItemStyle-HorizontalAlign="Left">
                                    <ItemTemplate>
                                        <asp:Label ID="lblDeptname" runat="server" Text='<%# Bind("DeptName") %>'></asp:Label>
                                        <asp:Label ID="lbldeptCode" runat="server" Text='<%# Bind("DepartmentID") %>' Visible="false"></asp:Label>
                                        <asp:Label ID="lblcons" runat="server" Text='<%# Bind("consultancy") %>' Visible="false"></asp:Label>
                                         <asp:Label ID="lblTestType" runat="server" Text='<%# Bind("TestType") %>' Visible="false"></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>                                          
                                <asp:CommandField SelectText="Edit" ShowSelectButton="True" ControlStyle-CssClass="temp" HeaderText="Edit" SelectImageUrl="~/Images/edit.png" ButtonType="Image">
                                    <ControlStyle CssClass="temp"></ControlStyle>
                                </asp:CommandField>
                                <asp:CommandField ShowDeleteButton="True" ControlStyle-CssClass="temp" HeaderText="Delete" DeleteImageUrl="~/Images/delete.png" ButtonType="Image" Visible="false">
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
        </div>
    </ContentTemplate>
    </asp:UpdatePanel>

</asp:Content>