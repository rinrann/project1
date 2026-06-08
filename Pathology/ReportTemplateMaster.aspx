<%@ Page Title="" Language="C#" MasterPageFile="~/MasterAll/MasterPageAll.master" AutoEventWireup="true" CodeFile="ReportTemplateMaster.aspx.cs" Inherits="Pathology_ReportTemplateMaster" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
 
<script type="text/javascript" language="javascript">
    function DisableBackButton() {
        window.history.forward()
    }
    DisableBackButton();
    window.onload = DisableBackButton;
    window.onpageshow = function (evt) { if (evt.persisted) DisableBackButton() }
    window.onunload = function () { void (0) }
    function Calling() {

        $("input[id$='Button1']").click(function () {

            if ($("select[id$='DropDownList1']").val() == '0') {
                alert('Select Any Department!');
                $("select[id$='DropDownList1']").focus();
                $("select[id$='DropDownList1']").addClass('textboxerr');
                return false;
            }
            else {
                $("select[id$='DropDownList1']").removeClass('textboxerr');
            }
            if ($("input[id$='txttemname']").val() == '') {
                $("input[id$='txttemname']").addClass('textboxerr');
            }

            if ($("input[id$='txttemname']").val() == '') {
                alert('Please Enter Template Name !');
                $("input[id$='txttemname']").focus();
                $("input[id$='txttemname']").addClass('textboxerr');
                return false;
            }
            else {
                $("input[id$='txttemname']").removeClass('textboxerr');
            }




            if ($("input[id$='txttemplate']").val() == '') {
                $("input[id$='txttemplate']").addClass('textboxerr');
            }

            if ($("input[id$='txttemplate']").val() == '') {
                alert('Please Enter Template Content !');
                $("input[id$='txttemplate']").focus();
                $("input[id$='txttemplate']").addClass('textboxerr');
                return false;
            }
            else {
                $("input[id$='txttemplate']").removeClass('textboxerr');
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
        document.getElementById("ctl00_ContentPlaceHolder1_txttemname").value = regname[0];

        $("#txttemname").focus();
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
         <asp:Label ID="Label1" runat="server">Report Template Master</asp:Label>
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
                <strong><asp:Label ID="lblError" runat="server" Font-Bold="True"></asp:Label>
                </strong>
                <div class="clear"></div>
            </div>
            <asp:HiddenField ID="TextBox4" runat="server" />
			<div class="form-sec-row">
                <label><strong>Department :</strong></label>
                <asp:DropDownList ID="DropDownList1" runat="server" CssClass="textbox-medium1">
                </asp:DropDownList>
                <div class="clear">  </div>
            </div>
           
            <div class="form-sec-row">
                <label><strong>Template Name :</strong></label>
                <asp:TextBox ID="txttemname" runat="server" CssClass="textbox-medium1" 
                   ></asp:TextBox>
                <cc1:AutoCompleteExtender ServiceMethod="SearchRptTemplate" OnClientItemSelected="autoCompleteEx_ItemSelected" MinimumPrefixLength="1"
                                    CompletionInterval="100" EnableCaching="false" CompletionSetCount="10" TargetControlID="txttemname" ID="AutoCompleteExtender2" runat="server" 
                                       FirstRowSelected = "false" ></cc1:AutoCompleteExtender>
                <div class="clear"></div>
            </div>

            <div class="form-sec-row">
                <label><strong>Template:</strong></label>  
                 <asp:TextBox ID="txttemplate" runat="server" CssClass="textbox-mediummul"
                    Rows="6" TextMode="MultiLine" Columns="6" 
                   ></asp:TextBox>
                <div class="clear"></div>
            </div>
            
            <div class="form-sec-row">
                <label><strong>&nbsp;</strong></label>
                <asp:Button ID="Button1" runat="server"   Text="Submit"  Height="28px"
                    CssClass="submit-button" onclick="Button1_Click" />
                <asp:Button ID="Button2" runat="server" Text="Cancel"   Height="28px" 
                    CssClass="submit-button" onclick="Button2_Click" />
                <div class="clear"></div>
            </div>  
   
     </div>
  
  </asp:View>
   <asp:View ID="View2" runat="server">
     <div class="listing"     style='width:100%; height:250px; overflow:auto;'>
         <asp:GridView id="GridView1"  CssClass="grid" PagerStyle-CssClass="pgr" DataKeyNames ="TemplateID,TemplateName" runat="server" 
         AutoGenerateColumns="False" AllowPaging="True" PageSize ="100" 
                 OnPageIndexChanging="GridView1_PageIndexChanging"   SelectedRowStyle-BackColor="GreenYellow"
                 OnRowCommand="GridView1_RowCommand" 
             OnRowDeleting="GridView1_RowDeleting" Width="979px">
                <RowStyle HorizontalAlign="Center" />
                <Columns>
                    <asp:TemplateField HeaderText="Template ID" Visible="false">
                        <ItemTemplate>
                            <asp:Label ID="lblcode" runat="server" Text='<%# Bind("TemplateID") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Template Name">
                       
                        <ItemTemplate>
                            <asp:Label ID="lblname" runat="server" Text='<%# Bind("TemplateName") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                                       
                    <asp:TemplateField HeaderText="Template">
                        <ItemTemplate>                        
                            <asp:Label ID="lbltemplate" runat="server" Text='<%# Bind("Template") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField> 
                       
                    <asp:TemplateField HeaderText="Department">
                        <ItemTemplate>                        
                            <asp:Label ID="lbldept" runat="server" Text='<%# Bind("DeptName") %>'></asp:Label>
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

