<%@ Page Language="C#" AutoEventWireup="true" CodeFile="DoctorHolidayMaster.aspx.cs" Inherits="OPD_DoctorHolidayMaster" MasterPageFile="~/MasterAll/MasterPageAll.master" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
<script type="text/javascript" language="javascript">
    function Calling() {
        var date = new Date();
        $("input[id$='txtfrmdate']").datepicker({ dateFormat: 'dd/mm/yy' });

        $("input[id$='txttodt']").datepicker({ dateFormat: 'dd/mm/yy' });

        $("input[id$='Button1']").click(function () {

            if ($("select[id$='DropDownList1']").val() == '0') {
                alert('Please Select Visiting  Doctor!');
                $("select[id$='DropDownList1']").addClass('textboxerr');
                $("select[id$='DropDownList1']").focus();
                return false;
            }
            else {
                $("select[id$='DropDownList1']").removeClass('textboxerr');
            }

            if ($("input[id$='txtfrmdate']").val() == '') {
                alert('Please Enter From Date Properly!');
                $("input[id$='txtfrmdate']").focus();
                $("input[id$='txtfrmdate']").addClass('textboxerr');
                return false;
            }
            else {
                $("input[id$='txtfrmdate']").removeClass('textboxerr');
            }

            if ($("input[id$='txttodt']").val() == '') {
                alert('Please Enter To Date Properly!');
                $("input[id$='txttodt']").focus();
                $("input[id$='txttodt']").addClass('textboxerr');
                return false;
            }
            else {
                $("input[id$='txttodt']").removeClass('textboxerr');
            }
            if ($("input[id$='txtReason']").val() == '') {
                alert('Please Enter Reason of Holiday Properly!');
                $("input[id$='txtReason']").focus();
                $("input[id$='txtReason']").addClass('textboxerr');
                return false;
            }
            else {
                $("input[id$='txttodt']").removeClass('textboxerr');
            }

        });

        $('.NumberInput').keydown(function (event) {
            if (event.keyCode == 46 || event.keyCode == 8 || event.keyCode == 9 || event.keyCode == 27 || event.keyCode == 13 ||
                  (event.keyCode == 65 && event.ctrlKey === true) || (event.keyCode >= 35 && event.keyCode <= 39)) {
                return;
            }
            else {
                if (event.shiftKey || (event.keyCode < 48 || event.keyCode > 57) && (event.keyCode < 96 || event.keyCode > 105)) {
                    event.preventDefault();
                }
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
    <div class="pageheader">
         <asp:Label ID="Label2" runat="server">Doctor's on Leave</asp:Label>
    </div>
    <div class="formbox">
    <asp:HiddenField ID="HiddenField1" runat="server" Value="0" />
                       <div class="form-sec-row">
                        <label>
                        <strong>
                         Doctor Type :</strong></label>
                        <asp:DropDownList ID="DropDownList2" runat="server"   Width="205px"
                               AutoPostBack="True" 
                               onselectedindexchanged="DropDownList2_SelectedIndexChanged" >                               
                         </asp:DropDownList>
                        <div class="clear">
                        </div>
                    </div>

                      <div class="form-sec-row">
                        <label>
                        <strong>
                        Select Doctor :</strong></label>
                        <asp:DropDownList ID="DropDownList1" runat="server"  Width="205px"
                              AutoPostBack="True" 
                              onselectedindexchanged="DropDownList1_SelectedIndexChanged" >                               
                         </asp:DropDownList>
                        <div class="clear">
                        </div>
                    </div>

                     
                     <div class="form-sec-row">
                        <label>
                        <strong> 
                       From Date </strong></label>
                        <asp:TextBox ID="txtfrmdate" runat="server"  Width="200px" ></asp:TextBox>
                              <asp:Label ID="Label12" runat="server" ForeColor="Green" Text="(DD/MM/YYYY)"></asp:Label>
                        <div class="clear">
                        </div>
                    </div>

                        <div class="form-sec-row">
                        <label>
                        <strong> 
                       To Date </strong></label>
                        <asp:TextBox ID="txttodt" runat="server"  Width="200px"></asp:TextBox>
                              <asp:Label ID="Label1" runat="server" ForeColor="Green" Text="(DD/MM/YYYY)"></asp:Label>
                        <div class="clear">
                        </div>
                    </div>

                         <div class="form-sec-row">
                        <label>
                        <strong> 
                     Reason of Holiday :</strong></label>
                        <asp:TextBox ID="txtReason" runat="server"    Width="200px"  Height="60px"
                            TextMode="MultiLine" ></asp:TextBox>
                        <div class="clear">
                        </div>
                    </div>

                       

                    <div class="form-sec-row">
                        &nbsp;&nbsp;&nbsp;<label><strong>&nbsp;</strong></label>
                        <asp:Button ID="Button1" runat="server" CssClass="submit-button"   Height="28px" 
                            Text="Submit" onclick="Button1_Click" />
                        <asp:Button ID="Button2" runat="server" CssClass="submit-button"   Height="28px" 
                            Text="Cancel" onclick="Button2_Click"  />
                        <div class="clear">
                        </div>
                    </div>
                
                    <div class="form-sec-row">
                        <div class="clear">
                        </div>
                    </div>

                    <div class="listing"  style='width:100%; height:auto; overflow:auto;'>
                <asp:GridView id="GridView1"  Width="100%" CssClass="grid" PagerStyle-CssClass="pgr"
                 DataKeyNames ="Rowid" runat="server" 
                 AutoGenerateColumns="False" AllowPaging="True"  PageSize="10"
                 SelectedRowStyle-BackColor="GreenYellow" 
                    onpageindexchanging="GridView1_PageIndexChanging" 
                    onrowcommand="GridView1_RowCommand" >
                    <RowStyle HorizontalAlign="Center" />
                    <Columns>
                        <asp:TemplateField HeaderText="ID" Visible="false"><ItemTemplate><asp:Label ID="lblid" runat="server" Text='<%# Bind("Rowid") %>'></asp:Label></ItemTemplate></asp:TemplateField>
                        <asp:TemplateField HeaderText="Doc ID" Visible="false"><ItemTemplate><asp:Label ID="lbldocid" runat="server" Text='<%# Bind("doctorId") %>'></asp:Label></ItemTemplate></asp:TemplateField>   
                        <asp:TemplateField HeaderText="Doc Name"><ItemTemplate><asp:Label ID="lbldocnm" runat="server" Text='<%# Bind("doc_name") %>'></asp:Label></ItemTemplate></asp:TemplateField>   
                        <asp:TemplateField HeaderText="Doc Type" Visible="false"><ItemTemplate><asp:Label ID="lbldoctypeid" runat="server" Text='<%# Bind("DocTypeID") %>'></asp:Label></ItemTemplate></asp:TemplateField> 
                        <asp:TemplateField HeaderText="From Date" ><ItemTemplate><asp:Label ID="lblFrmdate" runat="server" Text='<%# Bind("DayoffDay1") %>'></asp:Label></ItemTemplate></asp:TemplateField>
                        <asp:TemplateField HeaderText="To Date" ><ItemTemplate><asp:Label ID="lblTodate" runat="server" Text='<%# Bind("DayoffDay2") %>'></asp:Label></ItemTemplate></asp:TemplateField>
                        <asp:TemplateField HeaderText="Reason" Visible="True"><ItemTemplate><asp:Label ID="lblremarks" runat="server" Text='<%# Bind("Remarks") %>'></asp:Label></ItemTemplate></asp:TemplateField>      
                        <asp:CommandField SelectText="Edit" ShowSelectButton="True" ControlStyle-CssClass="temp" HeaderText="Edit" SelectImageUrl="~/Images/edit.png" ButtonType="Image"><ControlStyle CssClass="temp"></ControlStyle></asp:CommandField>
               
                    </Columns>
                    <PagerStyle CssClass="pgr" />
                    <EditRowStyle BackColor="#CCFF33" />
                    <AlternatingRowStyle BackColor="#FFDB91" />
                </asp:GridView>
            </div>
    </div>
    </asp:Content>