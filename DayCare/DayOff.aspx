<%@ Page Title="" Language="C#" MasterPageFile="~/MasterAll/MasterPageAll.master" AutoEventWireup="true" CodeFile="DayOff.aspx.cs" Inherits="DayCare_DayOff" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
 
<script language="javascript" type="text/javascript">

    function Calling() {
        var date = new Date();
        $("input[id$='TextBox1']").datepicker({ dateFormat: 'dd/mm/yy' });

        $("input[id$='Button1']").click(function () {
            if ($("input[id$='TextBox1']").val() == '') {
                alert('Please Enter Date  !');
                $("input[id$='TextBox1']").focus();
                $("input[id$='TextBox1']").addClass('textboxerr');
                return false;
            }
            else {
                $("input[id$='TextBox1']").removeClass('textboxerr');
            }


            if ($("input[id$='TextBox2']").val() == '') {
                alert('Please Enter Reason of Day Off  !');
                $("input[id$='TextBox2']").focus();
                $("input[id$='TextBox2']").addClass('textboxerr');
                return false;
            }
            else {
                $("input[id$='TextBox2']").removeClass('textboxerr');
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
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
    <ContentTemplate>
    <div class="pageheader">
         <asp:Label ID="Label1" runat="server">Day Off</asp:Label>
     </div>

         <table width="290px" cellpadding="0" cellspacing="0">
         <tr>
            <td>
                <asp:Button Text="Entry" BorderStyle="None" ID="Tab1" Width="145px"  Height="40px" 
                    CssClass="Initial" runat="server" onclick="Tab1_Click"
                     /></td>
                     <td>
                <asp:Button Text="List" BorderStyle="None" ID="Tab2" CssClass="Initial"  Width="145px"  Height="40px" 
                    runat="server" onclick="Tab2_Click"
                    /></td>
           
                     </tr>
                     </table>

     <div class="formbox">

      <asp:MultiView ID="MainView" runat="server">
                    <asp:View ID="View1" runat="server">

        <div class="form-sec">
            <div class="error">
                <strong><asp:Label ID="lblError" runat="server" Font-Bold="True"></asp:Label></strong>
                <div class="clear"></div>
            </div>
     <asp:HiddenField ID="HiddenField1" runat="server" />
			<div class="form-sec-row">
                <label><strong>Off Day :</strong></label>
                 <asp:TextBox ID="TextBox1" runat="server" CssClass="textbox-medium1" 
                   ></asp:TextBox>
                <div class="clear">  </div>
            </div>

            <div class="form-sec-row">
                <label><strong>Reason of Day Off :</strong></label>
                 <asp:TextBox ID="TextBox2" runat="server" CssClass="textbox-medium1" > 
              
                   </asp:TextBox>

                  

                <div class="clear">  </div>
            </div>
   

            <div class="form-sec-row">
                <label><strong>&nbsp;</strong></label>
                <asp:Button ID="Button1" runat="server" OnClick="Button1_Click" Text="Submit" Height="28px" CssClass="submit-button" />
                <asp:Button ID="Button2" runat="server" Text="Cancel" OnClick="Button2_Click" Height="28px" CssClass="submit-button" />
                <div class="clear"></div>
            </div>  
     </div>
     </asp:View>
        <asp:View ID="View2" runat="server">

     <div class="listing"   style='width:100%; height:300px; overflow:auto;'>
         <asp:GridView id="GridView1"  CssClass="grid"
                 PagerStyle-CssClass="pgr" DataKeyNames ="RowId" runat="server" 
                 AutoGenerateColumns="False" AllowPaging="True" PageSize ="100"  Width="100%"
                 OnPageIndexChanging="GridView1_PageIndexChanging"  SelectedRowStyle-BackColor="GreenYellow"
                 OnRowCommand="GridView1_RowCommand" OnRowDeleting="GridView1_RowDeleting">
                <RowStyle HorizontalAlign="Center" />
                <Columns>
                    <asp:TemplateField HeaderText="Id" Visible="false">
                        <ItemTemplate>
                            <asp:Label ID="lblID" runat="server" Text='<%# Bind("RowId") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Off Date ">
                       
                        <ItemTemplate>
                            <asp:Label ID="lblDayoffDay" runat="server" Text='<%# Bind("DayoffDay1") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>

                      <asp:TemplateField HeaderText="Reason of Day Off ">
                       
                        <ItemTemplate>
                            <asp:Label ID="lblDayoffReason" runat="server" Text='<%# Bind("DayoffReason") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
               
                    <asp:CommandField SelectText="Edit" ShowSelectButton="True" ControlStyle-CssClass="temp" HeaderText="Edit" SelectImageUrl="~/Images/edit.png" ButtonType="Image">
                        <ControlStyle CssClass="temp"></ControlStyle>
                    </asp:CommandField>
                    <asp:CommandField ShowDeleteButton="True" ControlStyle-CssClass="temp" HeaderText="Delete" DeleteImageUrl="~/Images/delete.png" ButtonType="Image">
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

