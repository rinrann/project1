<%@ Page Title="" Language="C#" MasterPageFile="~/MasterAll/MasterPageAll.master" AutoEventWireup="true" CodeFile="ShiftDetails.aspx.cs" Inherits="Master_ShiftDetails" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
 

<script language="javascript" type="text/javascript">
    function DisableBackButton() {
        window.history.forward()
    }
    DisableBackButton();
    window.onload = DisableBackButton;
    window.onpageshow = function (evt) { if (evt.persisted) DisableBackButton() }
    window.onunload = function () { void (0) }


    function Calling() {

        $("input[id$='TextBox2']").timepicker({
            showPeriod: true,
            showLeadingZero: true
        });

        $("input[id$='TextBox3']").timepicker({
            showPeriod: true,
            showLeadingZero: true
        });


        $("input[id$='Button1']").click(function () {

            if ($("input[id$='TextBox1']").val() == '') {
                alert('Please Enter Shift Name!');
                $("input[id$='TextBox1']").focus();
                $("input[id$='TextBox1']").addClass('textboxerr');
                return false;
            }
            else {
                $("input[id$='TextBox1']").removeClass('textboxerr');
            }

            if ($("input[id$='TextBox2']").val() == '') {
                alert('Please Enter In Time!');
                $("input[id$='TextBox2']").focus();
                $("input[id$='TextBox2']").addClass('textboxerr');
                return false;
            }
            else {
                $("input[id$='TextBox2']").removeClass('textboxerr');
            }



            if ($("input[id$='TextBox3']").val() == '') {
                alert('Please Enter Out Time!');
                $("input[id$='TextBox3']").focus();
                $("input[id$='TextBox3']").addClass('textboxerr');
                return false;
            }
            else {
                $("input[id$='TextBox3']").removeClass('textboxerr');
            }

            if ($("input[id$='TextBox5']").val() == '') {
                alert('Please Enter Maximum Limit!');
                $("input[id$='TextBox5']").focus();
                $("input[id$='TextBox5']").addClass('textboxerr');
                return false;
            }
            else {
                $("input[id$='TextBox5']").removeClass('textboxerr');
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
        document.getElementById("ctl00_ContentPlaceHolder1_TextBox1").value = regname[0];

        $("#TextBox1").focus();
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
         <asp:Label ID="Label1" runat="server">Shift Details</asp:Label>
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
            <asp:HiddenField ID="TextBox4" runat="server" />
			<div class="form-sec-row">
                <label><strong>Shift Name :</strong></label>
                <asp:TextBox ID="TextBox1" runat="server" CssClass="textbox-medium1" ></asp:TextBox>
                 <cc1:AutoCompleteExtender ServiceMethod="SearchShift" OnClientItemSelected="autoCompleteEx_ItemSelected" MinimumPrefixLength="1"
                                    CompletionInterval="100" EnableCaching="false" CompletionSetCount="10" TargetControlID="TextBox1" ID="AutoCompleteExtender2" runat="server" 
                                       FirstRowSelected = "false" ></cc1:AutoCompleteExtender>
                <div class="clear"></div>
            </div>
           
            <div class="form-sec-row">
                <label><strong>Time From :</strong></label>
                <asp:TextBox ID="TextBox2" runat="server" CssClass="textbox-medium1" ></asp:TextBox>
                <div class="clear"></div>
            </div>

            <div class="form-sec-row">
                <label><strong>To Time :</strong></label>
                 <asp:TextBox ID="TextBox3" runat="server" CssClass="textbox-medium1" 
                    MaxLength="10"></asp:TextBox>
                <div class="clear"></div>
            </div> 
            
            <div class="form-sec-row">
                <label><strong>Maximum Patient Limit :</strong></label>
                <asp:TextBox ID="TextBox5" runat="server" CssClass="textbox-medium1"></asp:TextBox>
                <div class="clear"></div>
            </div>

            <div class="form-sec-row">
                <label><strong>&nbsp;</strong></label>
                <asp:Button ID="Button1" runat="server" Height="28px" OnClick="Button1_Click" Text="Submit" CssClass="submit-button" />
                <asp:Button ID="Button2" runat="server" Height="28px" Text="Cancel" OnClick="Button2_Click" CssClass="submit-button" />
                <div class="clear"></div>
            </div>  
     </div>
  </asp:View>


                    
                    <asp:View ID="View2" runat="server">
<div class="listing"  style='width:100%; height:150px; overflow:auto;'>
         <asp:GridView id="GridView1"  CssClass="grid"
                 PagerStyle-CssClass="pgr" DataKeyNames ="ShiftID,ShiftName" runat="server" 
                 AutoGenerateColumns="False" AllowPaging="True" PageSize ="100" 
                 OnPageIndexChanging="GridView1_PageIndexChanging"  Width="100%"   SelectedRowStyle-BackColor="GreenYellow"
                 OnRowCommand="GridView1_RowCommand" OnRowDataBound="GridView1_RowDataBound" OnRowDeleting="GridView1_RowDeleting">
                <RowStyle HorizontalAlign="Center" />
                <Columns>
                    <asp:TemplateField HeaderText="Shift Id" Visible="false">
                        <ItemTemplate>
                            <asp:Label ID="lblShiftID" runat="server" Text='<%# Bind("ShiftID") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Shift Name">
                       
                        <ItemTemplate>
                            <asp:Label ID="lblShiftName" runat="server" Text='<%# Bind("ShiftName") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Time From">
                       
                        <ItemTemplate>
                            <asp:Label ID="lbltimefrom" runat="server" Text='<%# Bind("Time_From") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    
                    <asp:TemplateField HeaderText="To Time">
                        <ItemTemplate>                        
                            <asp:Label ID="lbltotime" runat="server" Text='<%# Bind("Time_TO") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    
                    <asp:TemplateField HeaderText="Max Patient Limit">
                        <ItemTemplate>
                            <asp:Label ID="lblmaxpatient" runat="server" Text='<%# Bind("MaxPatientLmt") %>'></asp:Label>
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

