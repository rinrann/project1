<%@ Page Title="" Language="C#" MasterPageFile="~/MasterAll/MasterPageAll.master" AutoEventWireup="true" CodeFile="ToDoList.aspx.cs" Inherits="IPD_ToDoList" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
<script type="text/javascript" language="javascript">
 
    function Calling() {
      
              var date = new Date();
              $('.DatepickerCall11').datepicker({ dateFormat: 'dd/mm/yy' });

        $("input[id$='Button1']").click(function () {

            if ($("select[id$='DropDownList1']").val() == '0') {
                alert('Please Select To Do Task !');
                $("select[id$='DropDownList1']").addClass('textboxerr');
                $("select[id$='DropDownList1']").focus();
                return false;
            }
            else {
                $("select[id$='DropDownList1']").removeClass('textboxerr');
            }
  

            if ($("input[id$='TextBox1']").val() == '') {
                alert('Please Enter Date !');
                $("input[id$='TextBox1']").focus();
                $("input[id$='TextBox1']").addClass('textboxerr');
                return false;
            }
            else {
                $("input[id$='TextBox1']").removeClass('textboxerr');
            }
        });
    }
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
    </script>

    
 
<%--  <asp:UpdateProgress ID="UpdateProgress1" runat="server">
    <ProgressTemplate>
    
       <div id="aaa" class="progressBackgroundFilter">          </div>
        <div id="bbb" class="processMessage"><img alt="Loading" src="../images/pwait.gif"/>
      
              </div>
    </ProgressTemplate>
    </asp:UpdateProgress>--%>

    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
    <ContentTemplate>
      <div class="pageheader">
         <asp:Label ID="Label1" runat="server">To Do List</asp:Label>
     </div>
<div class="form-sec">
        
            <div class="error">
                <strong><asp:Label ID="lblError" runat="server" Font-Bold="True"></asp:Label>
                </strong>
                <div class="clear"></div>
            </div>
            <asp:HiddenField ID="HiddenField1" runat="server" />
             <div class="form-sec-row">
                        <label>
                        <strong>
                       Registration No :</strong></label>
                          <asp:TextBox ID="TextBox23" runat="server" CssClass="textbox-medium1" 
                              Enabled="False"></asp:TextBox> 
                       <div class="clear">  </div>
                    </div>
                        <div class="form-sec-row">        <label>
                        <strong>
                       Patient's Name :</strong></label>
                          <asp:TextBox ID="TextBox24" runat="server" CssClass="textbox-medium1"  Enabled="False"></asp:TextBox>
                       <div class="clear">
                        </div>
                    </div>

                        <div class="form-sec-row">
                        <label>
                        <strong>
                       Bed No :</strong></label>
                          <asp:TextBox ID="TextBox25" runat="server" CssClass="textbox-medium1" 
                                Enabled="False"></asp:TextBox>
                        <div class="clear">
                        </div>
                    </div>

                        <div class="form-sec-row">
                        <label>
                        <strong>
                       Admission Date :</strong></label>
                          <asp:TextBox ID="TextBox26" runat="server" CssClass="textbox-medium1" 
                                Enabled="False"></asp:TextBox>
                        <div class="clear">
                        </div>
                    </div>
                    <div class="formbox">
                    <table width="100%"  border="3" rules="none" frame="box" style='background-color:#F9B2B1; color:#000;'>
                    <tr>
                    <td style='width:100px;'> <strong>
                    Task List : </strong></td>
                    <td  style='width:220px;'>   
                        <asp:DropDownList ID="DropDownList1" runat="server"  Width="150px">
                        </asp:DropDownList>     </td>

                       <td  style='width:110px;'>
                    <strong>  To Do Date : </strong></td>
                    <td>       <asp:TextBox ID="TextBox1" runat="server" CssClass="DatepickerCall11"  Width="150px"></asp:TextBox> 
                        
            <cc1:MaskedEditExtender ID="MaskedEditExtender1" runat="server" TargetControlID="TextBox1"
                                            Mask="99/99/9999" MessageValidatorTip="true" MaskType="Date" DisplayMoney="Left"
                                            AcceptNegative="Left" ErrorTooltipEnabled="True" CultureName="en-GB" />

                    </td>

                       <td>
                           <asp:Button ID="Button1" runat="server" Text="Submit" CssClass="submit-button"  Width="80px"
                               onclick="Button1_Click" /></td>

                               <td> <asp:LinkButton ID="LinkButton1" runat="server" onclick="LinkButton1_Click">Go To Dashboard</asp:LinkButton></td>
                     
                    </tr>
                    </table>

                    <div class="listing">
                <asp:GridView id="GridView1"  Width="100%" CssClass="grid" PagerStyle-CssClass="pgr"
                 DataKeyNames ="RowId" runat="server" 
                 AutoGenerateColumns="False" AllowPaging="True" 
                 SelectedRowStyle-BackColor="GreenYellow" 
                            onpageindexchanging="GridView1_PageIndexChanging" 
                            onrowcancelingedit="GridView1_RowCancelingEdit" 
                            onrowdeleting="GridView1_RowDeleting" onrowediting="GridView1_RowEditing" 
                            onrowupdating="GridView1_RowUpdating" 
                            onrowdatabound="GridView1_RowDataBound" OnRowCommand="GridView1_RowCommand"   >
                              <RowStyle HorizontalAlign="Center" />
                    <Columns>
                        <asp:TemplateField HeaderText="ID" Visible="false">
                           
                            <ItemTemplate>
                                <asp:Label ID="lblid" runat="server" Text='<%# Bind("RowId") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
             
             
                        <asp:TemplateField HeaderText="TO Do Task id" Visible="false">
                            <ItemTemplate>
                                <asp:Label ID="lblToDoTaskId" runat="server" Text='<%# Bind("ToDoTaskId") %>'></asp:Label>
                            </ItemTemplate> 
                        </asp:TemplateField>

                     <asp:TemplateField HeaderText="TO Do Task">
                            <ItemTemplate>
                                <asp:Label ID="lblTaskName" runat="server" Text='<%# Bind("TaskName") %>'></asp:Label>
                            </ItemTemplate>

                            
                         <EditItemTemplate>
                             <asp:DropDownList ID="ddltask"  Width="150px" runat="server">
                             </asp:DropDownList>
                    </EditItemTemplate>

                        </asp:TemplateField>

                         <asp:TemplateField HeaderText="Date">
                     <ItemTemplate>
                                <asp:Label ID="lbladate" runat="server" Text='<%# Bind("Date1") %>'></asp:Label>
                            </ItemTemplate>

                                    <EditItemTemplate>
                        <asp:TextBox ID="txtdate" Width="70px" CssClass="DatepickerCall11"   runat="server" Text='<%# Bind("Date1") %>'></asp:TextBox>
                                                <cc1:MaskedEditExtender ID="MaskedEditExtender1" runat="server" TargetControlID="txtdate"
                                            Mask="99/99/9999" MessageValidatorTip="true" MaskType="Date" DisplayMoney="Left"
                                            AcceptNegative="Left" ErrorTooltipEnabled="True" CultureName="en-GB" />
                    </EditItemTemplate>
                        </asp:TemplateField>
                    <asp:TemplateField HeaderText="Completed (Y/N)" ItemStyle-Width="100px" HeaderStyle-Width="100px">
                            <ItemTemplate>
                                <asp:Label ID="lblStatus" runat="server" Text='<%# Bind("CompletedFlag") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                       <asp:TemplateField HeaderText="Change Status" ItemStyle-Width="100px" HeaderStyle-Width="100px">
                            <ItemTemplate>
                               <asp:Button ID="btnstatus" runat="server" Text="Status(Y/N)" CommandName="UpdateStatus" CommandArgument="<%# ((GridViewRow) Container).RowIndex %>"/>
                            </ItemTemplate>
                        </asp:TemplateField>
                            <asp:CommandField HeaderText="Edit"   ControlStyle-CssClass="temp"  SelectImageUrl="~/Images/edit.png"   ShowEditButton="True" />
                     <asp:CommandField HeaderText="Delete"  DeleteImageUrl="~/Images/delete.png" ShowDeleteButton="True" />              
                                        
                              </Columns>
                    <PagerStyle CssClass="pgr" />
                    <EditRowStyle BackColor="#CCFF33" />
                    <AlternatingRowStyle BackColor="#FFDB91" />

<SelectedRowStyle BackColor="GreenYellow"></SelectedRowStyle>
                </asp:GridView>
            </div>

        
            </div>
      
     </div>
    </ContentTemplate>
    </asp:UpdatePanel>
   

</asp:Content>

