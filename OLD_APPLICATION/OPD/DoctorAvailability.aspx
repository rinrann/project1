<%@ Page Language="C#" MasterPageFile="~/MasterAll/MasterPageAll.master" AutoEventWireup="true" CodeFile="DoctorAvailability.aspx.cs" Inherits="OPD_DoctorAvailability" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>


<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <style>
.BigWidth
{
    width:550px; 
}
</style>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <script language="javascript" type="text/javascript">

        function Calling() {

            //            var date = new Date();
            //            $("input[id$='txtvalidityDate']").datepicker({ dateFormat: 'dd/mm/yy', minDate: date });

            var date = new Date();
            $("input[id$='txtFromDate']").datepicker({ dateFormat: 'dd/mm/yy' });
            $("input[id$='txtTodate']").datepicker({ dateFormat: 'dd/mm/yy' });
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
                <asp:Label ID="Label1" runat="server">Doctor's Availability</asp:Label>
            </div>
             <div class="error">
                <strong><asp:Label ID="lblError" runat="server" Font-Bold="True"></asp:Label>
                </strong>
                <div class="clear"></div>
            </div>

            <div class="formbox" style='width:1100px;'>
                <div class="form-sec-row">
                    <label style='color:Blue'><strong>Search Criteria :</strong></label>
                    <div class="clear"></div>
                </div>
                <div class="form-sec-row">
                    <table width="100%">
                            <tr>
                                   <td style="width:10%">  
                                       <asp:Label id="Label2" Width="70px" runat="server" Text="Discipline"/>
                                   </td>
                                   <td style="width:15%">  
                                        <asp:DropDownList ID="ddldiscpline" Width="150px" runat="server" CssClass="textbox-medium1" onselectedindexchanged="ddldiscpline_SelectedIndexChanged" AutoPostBack="true">
                                        
                                        </asp:DropDownList>
                                        
                                        <div class="clear">
                                        </div>
                                    </td>
                                    <td style="width:10%">
                                        <asp:Label id="lblDco" Width="70px" runat="server" Text="Doctor"/>
                                          
                                    </td>
                                     <td style="width:20%">  
                                        <asp:DropDownList ID="ddlDoc" Width="200px" runat="server" CssClass="textbox-medium1" >
                                        
                                        </asp:DropDownList>
                                        <div class="clear">
                                        </div>
                                    </td>
                                    <td style="width:10%">  <label style="width:70px"><strong>From Date:</strong></label> </td>
                                    <td style="width:10%">   <asp:TextBox ID="txtFromDate" CssClass="textbox-medium1" runat="server" Width="100px"></asp:TextBox> </td>
                                <td>  <label style="width:60px"><strong>To Date:</strong></label> </td>
                        <td style="width:10%">   <asp:TextBox ID="txtTodate" CssClass="textbox-medium1" runat="server" Width="100px"></asp:TextBox> </td>
                                    <td style="width:10%">
                                    <asp:Button ID="Button1" runat="server" Text="Search" CssClass="submit-generate" Width="70px" OnClick="Button1_Click" />
                                    </td>
                            </tr>
                    </table>
                </div>
            </div>
            <div class="formbox"  style='width:1100px;max-height:250px;overflow:auto;'>
                    <div style='width:100%;'>
                        <center>
                            <table width="100%" style='background-color:#FB7B13; color:#FFF;'>
                                  <tr style='border:1px solid black;'> 
                                       <td style='width:10%;' align="center">Doctor Type</td>
                                        <td style='width:20%;' align="center">Doctor</td>
                                        <td style='width:9%; padding-left:15px;' align="center">Sunday</td>
                                        <td style='width:9%; padding-left:15px;' align="center">Monday</td>
                                        <td style='width:9%; padding-left:15px;' align="center">Tuesday</td>
                                        <td style='width:9%; padding-left:15px;' align="center">Wednesday</td>
                                        <td style='width:9%; padding-left:15px;' align="center">Thursday</td>
                                        <td style='width:9%; padding-left:15px;' align="center">Friday</td>
                                        <td style='width:9%; padding-left:15px;' align="center">Saturday</td>
                                        <td style='width:16%; padding-left:15px;' align="center">Remarks</td>
                                  </tr>
                            </table>
                       </center>
                    </div>
                    <div class="listing" style='height:200px; overflow:auto;'>
                            <asp:GridView id="GridView1"  Width="100%"  CssClass="grid" PagerStyle-CssClass="pgr" 
                                   DataKeyNames ="DoctorId" runat="server"   SelectedRowStyle-BackColor="GreenYellow"
                                     AutoGenerateColumns="False" AllowPaging="True" PageSize="100" 
                                     ShowHeader="false" onpageindexchanging="GridView1_PageIndexChanging" >
                                    <RowStyle HorizontalAlign="Center" />  
                                    <Columns>
                                            <asp:TemplateField HeaderText="Doctor Id"   ItemStyle-Width="0%"  ItemStyle-HorizontalAlign="Center" Visible="false"> 
                                                <ItemTemplate>
                                                    <asp:Label ID="lblDocId" runat="server" Text='<%# Bind("DoctorId") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Doctor Type"   ItemStyle-Width="10%"  ItemStyle-HorizontalAlign="Center"> 
                                                <ItemTemplate>
                                                    <asp:Label ID="lblType" runat="server" Text='<%# Bind("DoctorType") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                           <asp:TemplateField HeaderText="Doctor"   ItemStyle-Width="20%"  ItemStyle-HorizontalAlign="left"> 
                                                <ItemTemplate>
                                                    <asp:Label ID="lblDoc" runat="server" Text='<%# Bind("Doctor") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Sunday"   ItemStyle-Width="9%"  ItemStyle-HorizontalAlign="center">                      
                                                <ItemTemplate>
                                                    <asp:Label ID="lblSunday" runat="server"  Text='<%# Bind("Sunday") %>'></asp:Label>                   
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                           <asp:TemplateField HeaderText="Monday"   ItemStyle-Width="9%"  ItemStyle-HorizontalAlign="center"> 
                                                <ItemTemplate>
                                                    <asp:Label ID="lblMonday"  runat="server" Text='<%# Bind("Monday") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                           <asp:TemplateField HeaderText="Tuesday"   ItemStyle-Width="9%"  ItemStyle-HorizontalAlign="center"> 
                                                <ItemTemplate>
                                                    <asp:Label ID="lblTuesday" runat="server" Text='<%# Bind("Tuesday") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Wednesday"   ItemStyle-Width="9%"  ItemStyle-HorizontalAlign="center"> 
                                                <ItemTemplate>
                                                    <asp:Label ID="lblWednesday"  runat="server" Text='<%# Bind("Wednesday") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Thursday"   ItemStyle-Width="9%"  ItemStyle-HorizontalAlign="center"> 
                                                <ItemTemplate>
                                                    <asp:Label ID="lblThursday"  runat="server" Text='<%# Bind("Thursday") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Friday"   ItemStyle-Width="9%"  ItemStyle-HorizontalAlign="center"> 
                                                <ItemTemplate>
                                                    <asp:Label ID="lblFriday" runat="server" Text='<%# Bind("Friday") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Saturday"   ItemStyle-Width="9%"  ItemStyle-HorizontalAlign="center"> 
                                                <ItemTemplate>
                                                    <asp:Label ID="lblSaturday"  runat="server" Text='<%# Bind("Saturday") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Remarks"   ItemStyle-Width="16%"  ItemStyle-HorizontalAlign="center"> 
                                                <ItemTemplate>
                                                    <asp:Label ID="lblRemarks"  runat="server" Text='<%# Bind("Remarks") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                    </Columns>
                                    <PagerStyle CssClass="pgr" />
                                    <EditRowStyle BackColor="#CCFF33" />
                                    <%--<AlternatingRowStyle BackColor="#FFDB91" /> --%>
                                    <SelectedRowStyle BackColor="GreenYellow" />
                            </asp:GridView>
                    </div>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
    </asp:Content>
