<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/MasterAll/MasterPageAll.master" CodeFile="MedicineZoom.aspx.cs" Inherits="Query_MedicineZoom" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

    
<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <script type="text/javascript" language="javascript">
        function SetContextKey() {
            $find('AutoCompleteExtender1').set_contextKey("GFC");
        }

        function autoCompleteEx_ItemSelected(sender, args) {

            var regname = args.get_value().split('~');// alert(regname[0]);
            document.getElementById("ctl00_ContentPlaceHolder1_TextBoxicode").value = regname[1];
            document.getElementById("ctl00_ContentPlaceHolder1_TextBox8").value = regname[0];
            // document.getElementById("TextBox8").value = regname[0];
            //document.getElementById("TextBox1").value = regname[0];
            $("#TextBox8").focus();
            //$("#DropDownList4").val(regname[1]);
        }

     </script>
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
         <asp:Label ID="Label1" runat="server">Medicine Zoom</asp:Label>
     </div>
        <div class="formbox">
                <center>
                    <table width="900px;">
                        <tr>
                            
                            <td>   <label><strong>
                     Medincine :</strong></label></td>
                     <td style="display:none">
                         
                         <asp:TextBox ID="TextBoxicode" runat="server" Width="150px"  CssClass="textbox-medium1"></asp:TextBox> 
                     </td>
                    <td>  
                         
                        <asp:TextBox ID="TextBox8" runat="server" OnTextChanged="medicinechange" AutoPostBack="true" Width="150px"  CssClass="textbox-medium1" ></asp:TextBox>
                                   <cc1:AutoCompleteExtender ServiceMethod="SearchMedicineName" OnClientItemSelected="autoCompleteEx_ItemSelected" MinimumPrefixLength="1"
                                    CompletionInterval="100" EnableCaching="false" CompletionSetCount="10" TargetControlID="TextBox8" ID="AutoCompleteExtender1" runat="server" 
                                       FirstRowSelected = "false" ></cc1:AutoCompleteExtender>
                     </td>
                            <td>  </td>
                           <td>  
                               
                           </td>
                            <td>  </td>
                           <td>  
                               
                           </td>
                        </tr>
                    </table>
                </center>
            </div>
        <div class="listing"  style='width:100%; height:450px; overflow:auto;'>
                <asp:GridView id="GridView2"  Width="100%" CssClass="grid" PagerStyle-CssClass="pgr"
                 DataKeyNames ="icode" runat="server" 
                 AutoGenerateColumns="False" AllowPaging="True" 
                 SelectedRowStyle-BackColor="GreenYellow"  PageSize="100" OnRowCommand="GridView2_RowCommand" OnPageIndexChanging="GridView2_PageIndexChanging">
                    <Columns>
                      <asp:CommandField SelectText="Select" ShowSelectButton="True" />
                  
                        <asp:TemplateField HeaderText="Code" >
                            <ItemTemplate>
                                <asp:Label ID="lblcd" runat="server" Text='<%# Bind("icode") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
             
                         <asp:TemplateField HeaderText="Name">
                            <ItemTemplate>
                                <asp:Label ID="lblname" runat="server" Text='<%# Bind("iname") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>

                         <asp:TemplateField HeaderText="Unit">
                            <ItemTemplate>
                                <asp:Label ID="lblunit" runat="server" Text='<%# Bind("iunit") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                         <asp:TemplateField HeaderText="Type">
                            <ItemTemplate>
                                <asp:Label ID="lbltype" runat="server" Text='<%# Bind("itype") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                         <asp:TemplateField HeaderText="Opn.Stock Qty">
                            <ItemTemplate>
                                <asp:Label ID="lblqty" runat="server" Text='<%# Bind("istock") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                         <asp:TemplateField HeaderText="Opening Value" Visible="false">
                            <ItemTemplate>
                                <asp:Label ID="lblopval" runat="server" Text='<%# Bind("IEPVAL") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Recd Stock Qty" >
                            <ItemTemplate>
                                <asp:Label ID="lblrecqty" runat="server" Text='<%# Bind("recqty") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                         <asp:TemplateField HeaderText="Recd Value" Visible="false">
                            <ItemTemplate>
                                <asp:Label ID="lblrecval" runat="server" Text='<%# Bind("recval") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                         <asp:TemplateField HeaderText="Issue Stock Qty" >
                            <ItemTemplate>
                                <asp:Label ID="lblissqty" runat="server" Text='<%# Bind("issqty") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                         <asp:TemplateField HeaderText="Isuue Value" Visible="false">
                            <ItemTemplate>
                                <asp:Label ID="lblissval" runat="server" Text='<%# Bind("issval") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                         <asp:TemplateField HeaderText="Current Stock Qty">
                            <ItemTemplate>
                                <asp:Label ID="lblcurstock" runat="server" Text='<%# Bind("curstock") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                         <asp:TemplateField HeaderText="Current Value" Visible="false">
                            <ItemTemplate>
                                <asp:Label ID="lblcurvalue" runat="server" Text='<%# Bind("curvalue") %>'></asp:Label>
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
