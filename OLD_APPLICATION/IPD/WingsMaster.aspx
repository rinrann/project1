<%@ Page Language="C#" MasterPageFile="~/MasterAll/MasterPageAll.master" AutoEventWireup="true" CodeFile="WingsMaster.aspx.cs" Inherits="Master_WingsMaster" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<script runat="server">

   
</script>
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
//        $('#page_effect').fadeIn(2000);
        $("input[id$='txtWingsName']").focus(function () {
            $("input[id$='txtWingsName']").addClass('textboxborder');
        });
        $("input[id$='txtWingsName']").blur(function () {
            $("input[id$='txtWingsName']").removeClass('textboxborder');
        });


        $("input[id$='Button1']").click(function () {


            if ($("input[id$='txtWingsName']").val() == '') {
                alert('Wings Name Can not be Blank!');
                $("input[id$='txtWingsName']").focus();
                $("input[id$='txtWingsName']").addClass('textboxerr');
                return false;
            }
            else {
                $("input[id$='txtWingsName']").removeClass('textboxerr');
            }

            var e = document.getElementById("ctl00_ContentPlaceHolder1_DropDownList1");
            var strUser = e.options[e.selectedIndex].text;

            if (strUser == '--Select--') {
                alert('Select Floor');
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
        document.getElementById("ctl00_ContentPlaceHolder1_txtWingsName").value = regname[0];

        $("#txtWingsName").focus();
        //$("#DropDownList4").val(regname[1]);
    }
</script>

</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">

<%--For Busy Loader .............................--%> 
  <asp:UpdateProgress ID="UpdateProgress1" runat="server">
    <ProgressTemplate>
    
       <div id="aaa" class="progressBackgroundFilter">          </div>
        <div id="bbb" class="processMessage"><img alt="Loading" src="../images/pwait.gif"/>
      
              </div>
    </ProgressTemplate>
    </asp:UpdateProgress>
    <%--For Busy Loader End.............................--%>
    <!-- Form Section html start -->
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
 <div id="page_effect">
            <div class="pageheader">
                <asp:Label ID="Label1" runat="server">Wings Master</asp:Label>
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
                    <asp:HiddenField ID="HiddenField1" runat="server"  />
  
                    <div class="form-sec-row">
                        <label>
                        <strong>
                        Wings Name :</strong></label>
                        <asp:TextBox ID="txtWingsName" runat="server" CssClass="textbox-medium1" ></asp:TextBox>
                        <cc1:AutoCompleteExtender ServiceMethod="SearchWing" OnClientItemSelected="autoCompleteEx_ItemSelected" MinimumPrefixLength="1"
                                    CompletionInterval="100" EnableCaching="false" CompletionSetCount="10" TargetControlID="txtWingsName" ID="AutoCompleteExtender2" runat="server" 
                                       FirstRowSelected = "false" ></cc1:AutoCompleteExtender>
                        <div class="clear">
                        </div>
                    </div>
                    <div class="form-sec-row">
                        <label>
                        <strong>
                        Work Station Name :</strong></label>
                        <asp:TextBox ID="txtWorkStinnm" runat="server" CssClass="textbox-medium1" ></asp:TextBox>
                        <div class="clear">
                        </div>
                    </div>
                     <div class="form-sec-row">
                        <label>
                        <strong>
                        Floor ID :</strong></label>
                         <asp:DropDownList ID="DropDownList1" runat="server" CssClass="textbox-medium1" >
                         </asp:DropDownList>
                        <div class="clear">
                        </div>
                    </div>
                    <div class="form-sec-row">
                    <label>
                    <strong>Pattern Text</strong>
                    </label>
                    <asp:TextBox ID="txtPattText" runat="server" CssClass="textbox-medium1"></asp:TextBox>
                    <div class="clear">
                    </div>
                    </div>
                 
                    <div class="form-sec-row">
                        <div class="clear">
                        </div>
                    </div>
                    <div class="form-sec-row">
                        &nbsp;&nbsp;&nbsp;<label><strong>&nbsp;</strong></label>
                        <asp:Button ID="Button1" runat="server" CssClass="submit-button"  Height="28px"
                            Text="Submit" onclick="Button1_Click"  />
                        <asp:Button ID="Button2" runat="server" CssClass="submit-button"    Height="28px"
                            Text="Cancel" onclick="Button2_Click" />
                        <div class="clear">
                        </div>
                    </div>
                    <div class="clear">
                    </div>
                </div>
          </asp:View>
              <asp:View ID="View2" runat="server">

                                         <div style='width:100%;'>
  <table width="100%" style='background-color:#FB7B13; color:#FFF;'>
  <tr style='border:1px solid black;'> 
        <td style='width:100px;' align="center">Wings Name</td> 
             <td style='width:100px;' align="center">Floor Name</td>  
      <td style='width:100px;' align="center">Work Station</td>
           <td style='width:100px;' align="center">Pattern Text</td> 
        <td style='width:70px;' align="center">Edit</td>
          <td style='width:70px;' align="center" id="coldel" runat="server">Delete</td>
          </tr>
  </table> 
  </div> 

         <div class="listing"  style='width:100%; height:500px; overflow:auto;'>

                <asp:GridView id="GridView1"  Width="978px" CssClass="grid" PagerStyle-CssClass="pgr"
                 DataKeyNames ="WingsID" runat="server" 
                 AutoGenerateColumns="False" AllowPaging="True"  ShowHeader="false"
                 OnRowCommand="GridView1_RowCommand"   SelectedRowStyle-BackColor="GreenYellow"    OnRowDataBound="GridView1_RowDataBound"
                 OnPageIndexChanging="GridView1_PageIndexChanging" 
                    onrowdeleting="GridView1_RowDeleting" > 
                               <RowStyle HorizontalAlign="Center" />
                    <Columns>
                        <asp:TemplateField HeaderText="Wings ID" Visible="false">
                            <ItemTemplate>
                                <asp:Label ID="WingsID" runat="server" Text='<%# Bind("WingsID") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Wings Name"  ItemStyle-Width="100px">
                            <ItemTemplate>
                                <asp:Label ID="WingsName" runat="server" Text='<%# Bind("WingsName") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Floor ID" Visible="false">
                            <ItemTemplate>
                                <asp:Label ID="FloorID" runat="server" Text='<%# Bind("FloorID") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                         <asp:TemplateField HeaderText="Floor Name"  ItemStyle-Width="100px">
                            <ItemTemplate>
                                <asp:Label ID="FloorName" runat="server" Text='<%# Bind("FloorName") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>  
                        <asp:TemplateField HeaderText="Work Station Name"  ItemStyle-Width="100px">
                            <ItemTemplate>
                                <asp:Label ID="WorkStation" runat="server" Text='<%# Bind("WorkStation") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>  
                        <asp:TemplateField HeaderText="Pattern Text"  ItemStyle-Width="100px">
                            <ItemTemplate>
                                <asp:Label ID="PatternText" runat="server" Text='<%# Bind("PatternText") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField> 
                                    
                        <asp:CommandField SelectText="Edit" ShowSelectButton="True"  ItemStyle-Width="70px" ControlStyle-CssClass="temp" HeaderText="Edit" SelectImageUrl="~/Images/edit.png" ButtonType="Image">
                            <ControlStyle CssClass="temp"></ControlStyle>
                        </asp:CommandField>
                        <asp:CommandField ShowDeleteButton="True" ControlStyle-CssClass="temp"  ItemStyle-Width="70px" HeaderText="Delete" DeleteImageUrl="~/Images/delete.png" ButtonType="Image">
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
           </div>
    </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
