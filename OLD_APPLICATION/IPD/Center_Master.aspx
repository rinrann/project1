<%@ Page Language="C#" MasterPageFile="~/MasterAll/MasterPageAll.master" AutoEventWireup="true" CodeFile="Center_Master.aspx.cs" Inherits="Master_Center_Master" Title="Untitled Page" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content2" ContentPlaceHolderID="head" Runat="Server">
     <script language="javascript" type="text/javascript">
        function DisableBackButton() {
        window.history.forward()
        }
        DisableBackButton();
        window.onload = DisableBackButton;
        window.onpageshow = function(evt) { if (evt.persisted) DisableBackButton() }
        window.onunload = function() { void (0) }

        function Calling() {
            $("input[id$='Button1']").click(function () {

                if ($("input[id$='TextBox1']").val() == '') {
                    alert('Please Enter Center Name Properly!');
                    $("input[id$='TextBox1']").focus();
                    $("input[id$='TextBox1']").addClass('textboxerr');
                    return false;
                }
                else {
                    $("input[id$='TextBox1']").removeClass('textboxerr');
                }


                if ($("input[id$='TextBox2']").val() == '') {
                    alert('Please Enter Address Properly!');
                    $("input[id$='TextBox2']").focus();
                    $("input[id$='TextBox2']").addClass('textboxerr');
                    return false;
                }
                else {
                    $("input[id$='TextBox2']").removeClass('textboxerr');
                }

                if ($("textarea[id$='TextBox3']").val() == '') {
                    alert('Please Enter Contact person!');
                    $("textarea[id$='TextBox3']").focus();
                    $("textarea[id$='TextBox3']").addClass('textboxerr');
                    return false;
                }
                else {
                    $("textarea[id$='TextBox3']").removeClass('textboxerr');
                }



                if ($("input[id$='TextBox5']").val() == '') {
                    alert('Please Enter Phone No. Properly!');
                    $("input[id$='TextBox5']").focus();
                    $("input[id$='TextBox5']").addClass('textboxerr');
                    return false;
                }
                else {
                    $("input[id$='TextBox5']").removeClass('textboxerr');
                }


                if ($("input[id$='TextBox8']").val() != '') {
                    var emailReg = /^([\w-\.]+@([\w-]+\.)+[\w-]{2,4})?$/;
                    if (!emailReg.test($("input[id$='TextBox8']").val())) {
                        alert('Invalid Email Address!');
                        $("input[id$='TextBox8']").focus();
                        $("input[id$='TextBox8']").addClass('textboxerr');
                        return false;
                    }
                }
                else {
                    $("input[id$='TextBox8']").removeClass('textboxerr');
                }
            });
            $("input[id$='TextBox5']").keydown(function (event) {
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


            $("input[id$='TextBox7']").keydown(function (event) {
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
        $(document).ready(function() {
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
            document.getElementById("ctl00_ContentPlaceHolder1_TextBox1").value = regname[0];

            $("#TextBox1").focus();
            //$("#DropDownList4").val(regname[1]);
        }
    </script>
</asp:Content>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">

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
         <asp:Label ID="Label1" runat="server">Sister/Aya Center</asp:Label>
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
                <label><strong>Center Name :</strong></label>
                <asp:TextBox ID="TextBox1" runat="server" CssClass="textbox-medium1" ></asp:TextBox>

                <cc1:AutoCompleteExtender ServiceMethod="Searchcenter" OnClientItemSelected="autoCompleteEx_ItemSelected" MinimumPrefixLength="1"
                                    CompletionInterval="100" EnableCaching="false" CompletionSetCount="10" TargetControlID="TextBox1" ID="AutoCompleteExtender2" runat="server" 
                                       FirstRowSelected = "false" ></cc1:AutoCompleteExtender>
                <div class="clear"></div>
            </div>
            
            <div class="form-sec-row">
                <label><strong>Center Address :</strong></label>
                <asp:TextBox ID="TextBox2" runat="server" CssClass="textbox-medium1" Height="60px" TextMode="MultiLine"></asp:TextBox>
                <div class="clear"></div>
            </div>
            
            <div class="form-sec-row">
                <label><strong>Contact Person :</strong></label>
                <asp:TextBox ID="TextBox3" runat="server" CssClass="textbox-medium1" ></asp:TextBox>
                <div class="clear"></div>
            </div>

          
            
            <div class="form-sec-row">
                <label><strong>Phone Number :</strong></label>
                <asp:TextBox ID="TextBox4" runat="server" CssClass="textbox-medium1" Text="+91" Enabled="false" Width="50px"></asp:TextBox>
                <asp:TextBox ID="TextBox5" runat="server" CssClass="textbox-medium1" Width="246px"
                    MaxLength="10" ></asp:TextBox>
                <div class="clear"></div>
            </div>
            
               <div class="form-sec-row">
                <label><strong>Alternative No :</strong></label>
               <asp:TextBox ID="TextBox6" runat="server" CssClass="textbox-medium1" Text="+91" Enabled="false" Width="50px"></asp:TextBox>
                <asp:TextBox ID="TextBox7" runat="server" CssClass="textbox-medium1" Width="246px"
                    MaxLength="10" ></asp:TextBox>
                <div class="clear"></div>
            </div>

            <div class="form-sec-row">
                <label><strong>Email Id :</strong></label>
                <asp:TextBox ID="TextBox8" runat="server" CssClass="textbox-medium1" ></asp:TextBox>
                <div class="clear"></div>
            </div>
            
            <div class="form-sec-row">
                <label><strong>&nbsp;</strong></label>
                <asp:Button ID="Button1" runat="server" OnClick="Button1_Click"  Height="30px"  Text="Submit" CssClass="submit-button" />
                <asp:Button ID="Button2" runat="server" Text="Cancel"   Height="30px"  OnClick="Button2_Click" CssClass="submit-button" />
                <div class="clear"></div>
            </div>                     

            <div class="clear"></div>
        </div>
               </asp:View>
              <asp:View ID="View2" runat="server">
  
        
<div class="listing">
     <asp:GridView id="GridView1" DataKeyNames ="CenterCode" CssClass="grid" PagerStyle-CssClass="pgr"  Width="100%" SelectedRowStyle-BackColor="GreenYellow"
     runat="server" AutoGenerateColumns="False" AllowPaging="True" PageSize ="10" OnRowDataBound="GridView1_RowDataBound" OnRowCommand="GridView1_RowCommand" 
      OnRowDeleting="GridView1_RowDeleting" OnPageIndexChanging="GridView1_PageIndexChanging" >
            <Columns>
                <asp:TemplateField HeaderText="Center Id" Visible="false">
                    <ItemTemplate>
                        &nbsp;<asp:Label ID="lblCenter_Id" runat="server" Text='<%# Bind("CenterCode") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                
                <asp:TemplateField HeaderText="Center Name">
                    <ItemTemplate>
                        &nbsp;<asp:Label ID="lblCenter_Name" runat="server" Text='<%# Bind("CenterName") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                
                <asp:TemplateField HeaderText="Center Address">
                    <ItemTemplate>
                        <asp:Label ID="lblCenter_Address" runat="server" Text='<%# Bind("Address") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                
                <asp:TemplateField HeaderText="Contact Person">
                    <ItemTemplate>
                        <asp:Label ID="lblContact_Person" runat="server" Text='<%# Bind("ContactPerson") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                
                <asp:TemplateField HeaderText="Phone Number">
                    <ItemTemplate>
                        <asp:Label ID="lblPhone_No" runat="server" Text='<%# Bind("PrimaryPhNo") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>

                
                <asp:TemplateField HeaderText="Alternative No">
                    <ItemTemplate>
                        <asp:Label ID="lblalPhone_No" runat="server" Text='<%# Bind("AlternativePhNo") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                
                <asp:TemplateField HeaderText="Email Id">
                    <ItemTemplate>
                        <asp:Label ID="lblEmailId" runat="server" Text='<%# Bind("EmailId") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                
                <asp:CommandField SelectText="Edit" ShowSelectButton="True" ControlStyle-CssClass="temp" HeaderText="Edit" SelectImageUrl="~/Images/edit.png" ButtonType="Image">
                    <ControlStyle CssClass="temp"></ControlStyle>
                </asp:CommandField>
                <asp:CommandField ShowDeleteButton="True" ControlStyle-CssClass="temp" HeaderText="Delete" DeleteImageUrl="~/Images/delete.png" ButtonType="Image">
                    <ControlStyle CssClass="temp"></ControlStyle>
                </asp:CommandField>
            </Columns>
            <EditRowStyle BackColor="#FFDB91" />
            <AlternatingRowStyle BackColor="#FFDB91" />
         <HeaderStyle BackColor="#FFC0C0" />
    </asp:GridView>
</div> 
  </asp:View>
            </asp:MultiView>
  </div>
    </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>