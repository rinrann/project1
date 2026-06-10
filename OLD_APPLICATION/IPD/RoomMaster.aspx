<%@ Page Language="C#" MasterPageFile="~/MasterAll/MasterPageAll.master" AutoEventWireup="true" CodeFile="RoomMaster.aspx.cs" Inherits="Master_RoomMaster" %>
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
        $("input[id$='txtRoomName']").focus(function () {
            $("input[id$='txtRoomName']").addClass('textboxborder');
        });
        $("input[id$='txtRoomName']").blur(function () {
            $("input[id$='txtRoomName']").removeClass('textboxborder');
        });
        $("input[id$='Button1']").click(function () {
            if ($("input[id$='txtRoomName']").val() == '') {
                $("input[id$='txtRoomName']").addClass('textboxerr');
            }
            if ($("input[id$='txtRoomName']").val() == '') {
                alert('Room Name Can not be Blank!');
                $("input[id$='txtRoomName']").focus();
                $("input[id$='txtRoomName']").addClass('textboxerr');
                return false;
            }
            else {
                $("input[id$='txtRoomName']").removeClass('textboxerr');
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
            var f = document.getElementById("ctl00_ContentPlaceHolder1_DropDownList2");
            var strUser = f.options[f.selectedIndex].text;

            if (strUser == '--Select--') {
                alert('Select Wings');
                $(f).focus();
                $(f).addClass('textboxerr');
                return false;
            }
            else {
                $(f).removeClass('textboxerr');
            }
            var f = document.getElementById("ctl00_ContentPlaceHolder1_DropDownList3");
            var strUser = f.options[f.selectedIndex].text;

            if (strUser == '--Select--') {
                alert('Select RoomCategory');
                $(f).focus();
                $(f).addClass('textboxerr');
                return false;
            }
            else {
                $(f).removeClass('textboxerr');
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
        document.getElementById("ctl00_ContentPlaceHolder1_txtRoomName").value = regname[0];

        $("#txtRoomName").focus();
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
            <div class="pageheader">
                <asp:Label ID="Label1" runat="server">Room Master</asp:Label>
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
                        <strong>
                        Room Name :</strong></label>
                        <asp:TextBox ID="txtRoomName" runat="server" CssClass="textbox-medium1" ></asp:TextBox>
                        <cc1:AutoCompleteExtender ServiceMethod="Searchroomtype" OnClientItemSelected="autoCompleteEx_ItemSelected" MinimumPrefixLength="1"
                                    CompletionInterval="100" EnableCaching="false" CompletionSetCount="10" TargetControlID="txtRoomName" ID="AutoCompleteExtender2" runat="server" 
                                       FirstRowSelected = "false" ></cc1:AutoCompleteExtender>
                        <div class="clear">
                        </div>
                    </div>
                     <div class="form-sec-row">
                        <label>
                        <strong>
                        Floor ID :</strong></label>
                         <asp:DropDownList ID="DropDownList1" runat="server" CssClass="textbox-medium1" 
                             AutoPostBack="true" onselectedindexchanged="DropDownList1_SelectedIndexChanged"
                             >
                         </asp:DropDownList>
                        <div class="clear">
                        </div>
                    </div>

                    <div class="form-sec-row">
                    <label><strong>
                    Wings ID :</strong></label>
                    <asp:DropDownList ID="DropDownList2" runat="server" CssClass="textbox-medium1">
                    </asp:DropDownList>
                    <div class="clear">
                    </div>
                    </div>

                    <div class="form-sec-row">
                    <label><strong>
                    RoomCategory ID :</strong></label>
                    <asp:DropDownList ID="DropDownList3" runat="server" CssClass="textbox-medium1">
                    </asp:DropDownList>
                    <div class="clear">
                    </div>
                    </div>

                     <div class="form-sec-row">
                        <label>
                        <strong> 
                        Pattern Text:</strong></label>
                        <asp:TextBox ID="txtPatText" runat="server" CssClass="textbox-medium1" ></asp:TextBox>
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
                            Text="Cancel" onclick="Button2_Click"  />
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
   <td style='width:90px;' align="center">Room Name</td> 
     <td style='width:90px;' align="center">Floor Name</td> 
       <td style='width:90px;' align="center">Wings Name</td> 
         <td style='width:90px;' align="center">Room Category Name</td> 
           <td style='width:90px;' align="center">Pattern Text</td> 
        <td style='width:70px;' align="center">Edit</td>
          <td style='width:70px;' align="center" id="coldel" runat="server">Delete</td>
          </tr>
  </table> 
  </div> 

            <div class="listing"  style='width:100%; height:500px; overflow:auto;'>
                <asp:GridView id="GridView1"  Width="100%" CssClass="grid" PagerStyle-CssClass="pgr"
                 DataKeyNames ="RoomID" runat="server"  ShowHeader="false"
                 AutoGenerateColumns="False" AllowPaging="True"  PageSize="100"
                 OnRowCommand="GridView1_RowCommand"  OnRowDataBound="GridView1_RowDataBound" OnRowDeleting="GridView1_RowDeleting" SelectedRowStyle-BackColor="GreenYellow" 
                 OnPageIndexChanging="GridView1_PageIndexChanging" >
                            <RowStyle HorizontalAlign="Center" />
                    <Columns>
                        <asp:TemplateField HeaderText="Room ID" Visible ="false">
                            <ItemTemplate>
                                <asp:Label ID="RoomID" runat="server" Text='<%# Bind("RoomID") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Room Name"   ItemStyle-Width="90px">
                            <ItemTemplate>
                                <asp:Label ID="RoomName" runat="server" Text='<%# Bind("RoomName") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                         <asp:TemplateField HeaderText="Floor Name"  ItemStyle-Width="90px">
                        <ItemTemplate>
                        <asp:Label ID="FloorName" runat="server" Text='<%#Bind("FloorName") %>'></asp:Label>
                        </ItemTemplate>
                        </asp:TemplateField>   

                        <asp:TemplateField HeaderText="Wings Name"  ItemStyle-Width="90px">
                        <ItemTemplate>
                        <asp:Label ID="WingsName" runat="server" Text='<%#Bind("WingsName") %>'></asp:Label>
                        </ItemTemplate>
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="RoomCategory Name"  ItemStyle-Width="90px">
                        <ItemTemplate>
                        <asp:Label ID="RoomCategoryName" runat="server" Text='<%#Bind("RoomCategoryName") %>'></asp:Label>
                        </ItemTemplate>
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="Pattern Text"  ItemStyle-Width="90px">
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
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>