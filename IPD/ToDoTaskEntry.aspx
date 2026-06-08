<%@ Page Title="" Language="C#" MasterPageFile="~/MasterAll/MasterPageAll.master" AutoEventWireup="true" CodeFile="ToDoTaskEntry.aspx.cs" Inherits="ToDoTaskEntry" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
<script type="text/javascript" language="javascript">

    function ShowDialog() {

        var rtvalue = window.open("IpdpatientPopup.aspx", "sss", "Width:800px; Height:550px; dialogLeft:250px;");
        document.getElementById("ctl00_ContentPlaceHolder1_TextBox23").value = rtvalue.NameValue;

    }

    function Calling() {

        var date = new Date();
        $('.DatepickerCall').datepicker({ dateFormat: 'dd/mm/yy' });

        $('.time').timepicker({
            showPeriod: true,
            showLeadingZero: true
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

   <asp:UpdatePanel ID="UpdatePanel1" runat="server">
    <ContentTemplate>
     <div class="pageheader">
         <asp:Label ID="Label1" runat="server">To Do Task Entry</asp:Label>
     </div>


                <div class="listing">
                <asp:GridView id="GridView1"  Width="100%" CssClass="grid" 
                        PagerStyle-CssClass="pgr" runat="server" 
                 AutoGenerateColumns="False" AllowPaging="True" 
                 SelectedRowStyle-BackColor="GreenYellow" 
                        onpageindexchanging="GridView1_PageIndexChanging" 
                        onrowcommand="GridView1_RowCommand" >
                       
                    <Columns>
                    
                        <asp:TemplateField HeaderText="Patient Registration">
                            <ItemTemplate>
                                <asp:Label ID="lblPatientReg" runat="server" Text='<%# Bind("PatientReg") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
             
             
                        <asp:TemplateField HeaderText="Patient Name">
                            <ItemTemplate>
                                <asp:Label ID="lblpatient_name" runat="server" Text='<%# Bind("patient_name") %>'></asp:Label>
                            </ItemTemplate> 
                        </asp:TemplateField>

                     <asp:TemplateField HeaderText="Address">
                            <ItemTemplate>
                                <asp:Label ID="lblvill_city" runat="server" Text='<%# Bind("Address") %>'></asp:Label>
                            </ItemTemplate>
                              </asp:TemplateField>

                              <asp:CommandField ShowSelectButton="True" />
                              </Columns>
                    <PagerStyle CssClass="pgr" />
                    <EditRowStyle BackColor="#CCFF33" />
                    <AlternatingRowStyle BackColor="#FFDB91" />

<SelectedRowStyle BackColor="GreenYellow"></SelectedRowStyle>
                </asp:GridView>
            </div>
            <br /><br />
              <div class="listing">

               <asp:GridView id="GridTodoTask"  Width="100%" CssClass="grid" 
                        PagerStyle-CssClass="pgr" runat="server" 
                 AutoGenerateColumns="False" AllowPaging="True" 
                 SelectedRowStyle-BackColor="GreenYellow" 
                        onpageindexchanging="GridView1_PageIndexChanging" 
                      onrowupdating="GridTodoTask_RowUpdating" 
                      onrowcancelingedit="GridTodoTask_RowCancelingEdit" 
                      onrowediting="GridTodoTask_RowEditing" >
                       
                    <Columns>

                       <asp:TemplateField HeaderText="RowId" Visible="false">
                            <ItemTemplate>
                                <asp:Label ID="lblRowId" runat="server" Text='<%# Bind("RowId") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>

                    
                        <asp:TemplateField HeaderText="TaskName">
                            <ItemTemplate>
                                <asp:Label ID="lblTaskName" runat="server" Text='<%# Bind("TaskName") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
             
             
                        <asp:TemplateField HeaderText="ToDate">
                            <ItemTemplate>
                                <asp:Label ID="lblToDate" runat="server" Text='<%# Bind("ToDate") %>'></asp:Label>
                            </ItemTemplate> 
                        </asp:TemplateField>

                     <asp:TemplateField HeaderText="TaskBy">
                            <ItemTemplate>
                                <asp:Label ID="lblTaskBy" runat="server" Text='<%# Bind("TaskBy") %>'></asp:Label>
                            </ItemTemplate>
                             <EditItemTemplate>
                     <asp:TextBox ID="txtETaskBy" runat="server"  Text='<%# Bind("TaskBy") %>'></asp:TextBox>
                    </EditItemTemplate>
                              </asp:TemplateField>


                               <asp:TemplateField HeaderText="TaskTime">
                            <ItemTemplate>
                                <asp:Label ID="lblTaskTime" runat="server" Text='<%# Bind("TaskTime") %>'></asp:Label>
                            </ItemTemplate>
                             <EditItemTemplate>
                     <asp:TextBox ID="txtTaskTime" runat="server" CssClass="time" Text='<%# Bind("TaskTime") %>'></asp:TextBox>
                    </EditItemTemplate>
                              </asp:TemplateField>

                          <asp:TemplateField HeaderText="TaskDate">
                            <ItemTemplate>
                                <asp:Label ID="lblTaskDate" runat="server" Text='<%# Bind("TaskDate") %>'></asp:Label>
                            </ItemTemplate>
                             <EditItemTemplate>
                     <asp:TextBox ID="txtTaskDate" runat="server" CssClass="DatepickerCall" Text='<%# Bind("TaskDate") %>'></asp:TextBox>
                                   <cc1:MaskedEditExtender ID="MaskedEditExtender1" runat="server" TargetControlID="txtTaskDate"
                                            Mask="99/99/9999" MessageValidatorTip="true" MaskType="Date" DisplayMoney="Left"
                                            AcceptNegative="Left" ErrorTooltipEnabled="True" CultureName="en-GB" />
                    </EditItemTemplate>
                              </asp:TemplateField>

                 
                                        
                              <asp:CommandField ShowEditButton="True" />

                 
                                        
                              </Columns>
                    <PagerStyle CssClass="pgr" />
                    <EditRowStyle BackColor="#CCFF33" />
                    <AlternatingRowStyle BackColor="#FFDB91" />

<SelectedRowStyle BackColor="GreenYellow"></SelectedRowStyle>
                </asp:GridView>




              </div>

             </ContentTemplate>
    </asp:UpdatePanel>

    </asp:Content>


