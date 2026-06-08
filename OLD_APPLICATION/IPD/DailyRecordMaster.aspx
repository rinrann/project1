<%@ Page Language="C#" MasterPageFile="~/MasterAll/MasterPageAll.master" AutoEventWireup="true" CodeFile="DailyRecordMaster.aspx.cs" Inherits="Master_DailyRecordMaster" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
 
<script language="javascript" type="text/javascript">

    function DisableBackButton() {
        window.history.forward()
    }
    DisableBackButton();
    window.onload = DisableBackButton;
    window.onpageshow = function (evt) { if (evt.persisted) DisableBackButton() }
    window.onunload = function () { void (0) }

    function Calling() {
        $("input[id$='txtRecordName']").focus(function () {
            $("input[id$='txtRecordName']").addClass('textboxborder');
        });
        $("input[id$='txtRecordName']").blur(function () {
            $("input[id$='txtRecordName']").removeClass('textboxborder');
        });
        $("input[id$='Button1']").click(function () {

            if ($("input[id$='txtRecordName']").val() == '') {
                alert('Record Name Can not be Blank!');
                $("input[id$='txtRecordName']").focus();
                $("input[id$='txtRecordName']").addClass('textboxerr');
                return false;
            }
            else {
                $("input[id$='txtRecordName']").removeClass('textboxerr');
            }

            var e = document.getElementById("ctl00_ContentPlaceHolder1_DropDownList1");
            var strUser = e.options[e.selectedIndex].text;

            if (strUser == '--Select--') {
                alert('Select Unit');
                $(e).focus();
                $(e).addClass('textboxerr');
                return false;
            }
            else {
                $(e).removeClass('textboxerr');
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

    function SetContextKey() {
        $find('AutoCompleteExtender1').set_contextKey("GFC");
    }

    function autoCompleteEx_ItemSelected(sender, args) {

        var regname = args.get_value().split('~');// alert(regname[0]);
        //document.getElementById("ctl00_ContentPlaceHolder1_TextBoxicode").value = regname[1];
        document.getElementById("ctl00_ContentPlaceHolder1_txtRecordName").value = regname[0];

        $("#txtRecordName").focus();
        //$("#DropDownList4").val(regname[1]);
    }
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


    <!-- Form Section html start -->


    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <div class="pageheader">
                <asp:Label ID="Label1" runat="server">Daily Record Master</asp:Label>
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
                        <strong>
                        <asp:Label ID="lblError" runat="server" Font-Bold="True" CssClass="fadeout"></asp:Label>
                        </strong>
                        <div class="clear">
                        </div>
                    </div>
                    <asp:HiddenField ID="HiddenField1" runat="server" Value="0" />
                    <div class="form-sec-row">
                        <label>
                        <strong>Record ID :</strong></label>
                        <asp:TextBox ID="txtRecordId" runat="server" CssClass="textbox-medium1" 
                            Enabled="False" ></asp:TextBox>
                        <div class="clear">
                        </div>
                    </div>
                    <div class="form-sec-row">
                        <label>
                        <strong>
                        Record Name :</strong></label>
                        <asp:TextBox ID="txtRecordName" runat="server" CssClass="textbox-medium1" ></asp:TextBox>
                        <cc1:AutoCompleteExtender ServiceMethod="Searchrecord" OnClientItemSelected="autoCompleteEx_ItemSelected" MinimumPrefixLength="1"
                                    CompletionInterval="100" EnableCaching="false" CompletionSetCount="10" TargetControlID="txtRecordName" ID="AutoCompleteExtender2" runat="server" 
                                       FirstRowSelected = "false" ></cc1:AutoCompleteExtender>
                        <div class="clear">
                        </div>
                    </div>
                     <div class="form-sec-row">
                        <label>
                        <strong>
                        Unit ID :</strong></label>
                         <asp:DropDownList ID="DropDownList1" runat="server" CssClass="textbox-medium1" >
                         </asp:DropDownList>
                        <div class="clear">
                        </div>
                    </div>
                    <div class="form-sec-row">
                    <label>
                    <strong>Active</strong>
                    </label>
                    <asp:DropDownList ID="DropDownList2" runat="server" CssClass="textbox-medium1">
                    <asp:ListItem Value="0">-- Select --</asp:ListItem>
                    <asp:ListItem>Yes</asp:ListItem>
                    <asp:ListItem>No</asp:ListItem>
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
                        <asp:Button ID="Button1" runat="server" CssClass="submit-button"   Height="30px"
                            Text="Submit" onclick="Button1_Click"   />
                        <asp:Button ID="Button2" runat="server" CssClass="submit-button"   Height="30px"
                            Text="Cancel" onclick="Button2_Click"   />
                        <div class="clear">
                        </div>
                    </div>
                    <div class="clear">
                    </div>
                </div>
                </asp:View>
                 <asp:View ID="View2" runat="server">

       
            <div class="listing">
                <asp:GridView id="GridView1"  Width="100%" CssClass="grid" PagerStyle-CssClass="pgr"
                 DataKeyNames ="RecordID" runat="server" 
                 AutoGenerateColumns="False" AllowPaging="True" 
                 OnRowCommand="GridView1_RowCommand" OnRowDataBound="GridView1_RowDataBound" SelectedRowStyle-BackColor="GreenYellow"
                 OnPageIndexChanging="GridView1_PageIndexChanging" 
                    onrowdeleting="GridView1_RowDeleting" > 
                    <Columns>
                        <asp:TemplateField HeaderText="Record ID">
                            <ItemTemplate>
                                <asp:Label ID="RecordID" runat="server" Text='<%# Bind("RecordID") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Record Name">
                            <ItemTemplate>
                                <asp:Label ID="RecordName" runat="server" Text='<%# Bind("RecordName") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Unit ID" Visible="false">
                            <ItemTemplate>
                                <asp:Label ID="UnitID" runat="server" Text='<%# Bind("UnitID") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                         <asp:TemplateField HeaderText="Unit Name">
                            <ItemTemplate>
                                <asp:Label ID="UnitName" runat="server" Text='<%# Bind("UnitName") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>    
                        <asp:TemplateField HeaderText="Active">
                            <ItemTemplate>
                                <asp:Label ID="Active" runat="server" Text='<%# Bind("Active") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField> 
                                    
                        <asp:CommandField SelectText="Edit" ShowSelectButton="True" ControlStyle-CssClass="temp" ItemStyle-HorizontalAlign="Center" HeaderText="Edit" SelectImageUrl="~/Images/edit.png" ButtonType="Image">
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

