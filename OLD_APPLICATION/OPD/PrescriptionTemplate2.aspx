<%@ Page Language="C#" MasterPageFile="~/MasterAll/MasterPageAll.master" AutoEventWireup="true"
    CodeFile="PrescriptionTemplate2.aspx.cs" Inherits="Master_PrescriptionTemplate2" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
     <script type="text/javascript" language="javascript">
         function ShowDialog() {

             var rtvalue = window.open("PrescriptionTempPopup.aspx", "sss", "Width:800px; Height:550px; dialogLeft:250px;");
             //document.getElementById("ctl00_ContentPlaceHolder1_HiddenField1").value = rtvalue.NameValue;
             //document.getElementById("ctl00_ContentPlaceHolder1_txtPrescrpTemName").value = rtvalue.ProfessionValue;
         }


         function SetContextKey() {
             $find('AutoCompleteExtender1').set_contextKey("GFC");
         }

         function autoCompleteEx_ItemSelected(sender, args) {

             var regname = args.get_value().split('~');// alert(regname[0]);
             //document.getElementById("ctl00_ContentPlaceHolder1_TextBoxicode").value = regname[1];
             document.getElementById("ctl00_ContentPlaceHolder1_txtPrescrpTemName").value = regname[0];

             $("#txtPrescrpTemName").focus();
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

             <div id="divContent" runat="server">
                 <!-- Form Section html start -->
                 <div class="pageheader">
                   <asp:Label ID="Label1" runat="server">Prescription Template</asp:Label>
                </div>
                <div class="formbox">
                     <div class="form-sec">
                         <div class ="error">
                             <strong>
                                 <asp:label ID="lblError" runat="server" Font-Bold="True" CssClass="fadeout"></asp:label>
                             </strong>
                             <div class="clear"></div>
                         </div>
                         <asp:HiddenField ID="HiddenField1" runat="server" Value="0"/>
                         <div class="form-sec-row">
                             <label><strong>Template Group :</strong></label>
                             <asp:DropDownList ID="DropDownList51" runat="server" CssClass="textbox-medium1">
                             </asp:DropDownList>
                             <div class="clear"></div>
                         </div>
                        <div class="form-sec-row">
                            <label><strong>Template Name :</strong></label>
                          <asp:TextBox ID="txtPrescrpTemName" runat="server" CssClass="textbox-medium1" Width="328px" Height="20px" Enabled="true"></asp:TextBox>
                            <cc1:AutoCompleteExtender ServiceMethod="SearchTemplate" OnClientItemSelected="autoCompleteEx_ItemSelected" MinimumPrefixLength="1"
                                    CompletionInterval="100" EnableCaching="false" CompletionSetCount="10" TargetControlID="txtPrescrpTemName" ID="AutoCompleteExtender3" runat="server" 
                                       FirstRowSelected = "false" ></cc1:AutoCompleteExtender>
                          <asp:Button ID="Button3" runat="server" CssClass="submit-button" Text="SEARCH" Height="28px" OnClientClick="return ShowDialog();"/>
                          <asp:Button ID="Button4" runat="server" CssClass="submit-button" Text="FETCH"  Height="28px"  />
                           <div class="clear"></div>
                        </div>
                        <div class="form-sec-row">
                            <label><strong>Checked By Medicine :</strong></label>
                            <asp:TextBox ID="txtCheckMedicine" runat="server" Width="328px" CssClass="textbox-medium1" Height="20px"></asp:TextBox>
                            <cc1:AutoCompleteExtender ServiceMethod="SearchMedicine"   MinimumPrefixLength="2"    CompletionInterval="100" EnableCaching="false" 
                            CompletionSetCount="10" TargetControlID="txtCheckMedicine"  ID="AutoCompleteExtender1" runat="server" FirstRowSelected = "false" ></cc1:AutoCompleteExtender>
                            <div class="clear"></div>
                        </div>
                        <div class="form-sec-row">
                            <label><strong>Checked By Sub Group :</strong></label>
                            <asp:TextBox ID="txtSubgroup" runat="server" Width="328px" CssClass="textbox-medium1" Height="16px"></asp:TextBox>
                            <cc1:AutoCompleteExtender ServiceMethod="SearchSubGroup"   MinimumPrefixLength="1"    CompletionInterval="100" EnableCaching="false" 
                                CompletionSetCount="10" TargetControlID="txtSubgroup"  ID="AutoCompleteExtender2" runat="server" FirstRowSelected = "false" ></cc1:AutoCompleteExtender>
                                <div class="clear"></div>
                        </div>
                         <table>
                              <tr>
                                  <td style="width: 100%;"align="center">
                                    <div style="padding: 10px; width: 110%; height: 221px; overflow: auto;">
                                                            <asp:DataGrid ID="dg" runat="server" Width="104%"
                                                                AutoGenerateColumns="false" ShowFooter="true" ShowHeader="true"
                                                                PageSize="10">
                                                                <AlternatingItemStyle CssClass="odd1" />
                                                                <ItemStyle CssClass="even1" />
                                                                <HeaderStyle CssClass="titlebar" />
                                                                <Columns>

                                                                    <asp:TemplateColumn HeaderStyle-Width="10%" HeaderText="MEDICINE GROUP" HeaderStyle-BackColor="#ff9300">
                                                                        <ItemTemplate>
                     
                                                                            <asp:DropDownList ID="ddlMEDICINEGROUP" runat="server" CssClass="textbox-medium1"  Height="29px" Width="155px" AutoPostBack="True" >

                                                                            </asp:DropDownList>
                                                                        </ItemTemplate>
                                                                        <EditItemTemplate>
                                                                            <asp:DropDownList ID="DropDownList1" runat="server"></asp:DropDownList>
                                                                        </EditItemTemplate>
                                                                    
                                                                        <HeaderStyle Width="10%" />
                                                                        <ItemStyle VerticalAlign="Middle" HorizontalAlign="Left" />
                                                                    </asp:TemplateColumn>

                                                                    <asp:TemplateColumn HeaderStyle-Width="10%" HeaderText="MEDICINE NAME" HeaderStyle-BackColor="#ff9300">
                                                                        <ItemTemplate>
                                                                           
                                                                            <asp:DropDownList ID="ddlMEDICINENAME" runat="server" CssClass="textbox-medium1"  Height="29px" Width="155px" AutoPostBack="True">

                                                                            </asp:DropDownList>
                                                                        </ItemTemplate>
                                                                        <EditItemTemplate>
                                                                            <asp:DropDownList ID="DropDownList2" runat="server"></asp:DropDownList>
                                                                        </EditItemTemplate>
                                                                       
                                                                        <HeaderStyle Width="10%" />
                                                                        <ItemStyle VerticalAlign="Middle" HorizontalAlign="Left" />
                                                                    </asp:TemplateColumn>
                                                                    <asp:TemplateColumn HeaderStyle-Width="10%" HeaderText="ROUTE NAME" HeaderStyle-BackColor="#ff9300">
                                                                        <ItemTemplate>
                                                                           
                                                                            <asp:DropDownList ID="ddlROUTENAME" runat="server" CssClass="textbox-medium1"  Height="29px" Width="155px" AutoPostBack="True">

                                                                            </asp:DropDownList>
                                                                        </ItemTemplate>
                                                                        <EditItemTemplate>
                                                                            <asp:DropDownList ID="DropDownList3" runat="server"></asp:DropDownList>
                                                                        </EditItemTemplate>
                                                                       
                                                                        <HeaderStyle Width="10%" />
                                                                        <ItemStyle VerticalAlign="Middle" HorizontalAlign="Left" />
                                                                    </asp:TemplateColumn>
                                                                    <asp:TemplateColumn HeaderStyle-Width="10%" HeaderText="DAILY DOSE" HeaderStyle-BackColor="#ff9300">
                                                                        <ItemTemplate>
                                                                           
                                                                            <asp:DropDownList ID="ddlDAILYDOSE" runat="server" CssClass="textbox-medium1"  Height="29px" Width="155px" AutoPostBack="True">

                                                                            </asp:DropDownList>
                                                                        </ItemTemplate>
                                                                        <EditItemTemplate>
                                                                            <asp:DropDownList ID="DropDownList4" runat="server"></asp:DropDownList>
                                                                        </EditItemTemplate>
                                                                       
                                                                        <HeaderStyle Width="10%" />
                                                                        <ItemStyle VerticalAlign="Middle" HorizontalAlign="Left" />
                                                                    </asp:TemplateColumn>
                                                                     <asp:TemplateColumn HeaderStyle-Width="10%" HeaderText="DURATION" HeaderStyle-BackColor="#ff9300">
                                                                        <ItemTemplate>
                                                                           
                                                                            <asp:DropDownList ID="ddlDURATION" runat="server" CssClass="textbox-medium1"  Height="29px" Width="155px" AutoPostBack="True">

                                                                            </asp:DropDownList>
                                                                        </ItemTemplate>
                                                                        <EditItemTemplate>
                                                                            <asp:DropDownList ID="DropDownList5" runat="server"></asp:DropDownList>
                                                                        </EditItemTemplate>
                                                                       
                                                                        <HeaderStyle Width="10%" />
                                                                        <ItemStyle VerticalAlign="Middle" HorizontalAlign="Left" />
                                                                    </asp:TemplateColumn>

                          

                                                                </Columns>
                                                            </asp:DataGrid>
                                                        </div>

                                                    </td>
                                                </tr>
                                  <tr>
                                                    <td align="center" style="width: 50%">
                                                        <asp:Button ID="ButtonAdd" runat="server" Width="107px" CssClass="submitbutton" Text="Add Row" OnClick="ButtonAdd_Click" />
                                                    </td>

                                                </tr>
                             </table>
                         </div>

                         
                     </div>
                 </div>
             </div>

         </ContentTemplate>

          </asp:UpdatePanel>
    </asp:Content>