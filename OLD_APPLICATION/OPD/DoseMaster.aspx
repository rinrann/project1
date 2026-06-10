<%@ Page Title="" Language="C#" MasterPageFile="~/MasterAll/MasterPageAll.master" AutoEventWireup="true" CodeFile="DoseMaster.aspx.cs" Inherits="Medicine_DoseMaster" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
     <link rel="stylesheet" type="text/css" href="../css/style.css" />   
    <link rel="stylesheet" type="text/css" href="../css/jquery-ui.css" /> 
    <link type="text/css" href="css/calendar-blue.css" rel="stylesheet" />
     

    <%--<link href="css/Master.css" rel="stylesheet" type="text/css" />--%>
<script src="../Script/jquery.webcam.js" type="text/javascript"></script>


    <script src="Script/ScrollableGridViewPlugin_ASP.NetAJAXmin.js" type="text/javascript"></script>
    <script src="Script/jquery-1.4.1.min.js" type="text/javascript"></script>
    <script src="Script/jquery.dynDateTime.min.js" type="text/javascript"></script>
    <script src="Script/calendar-en.min.js" type="text/javascript"></script>
    <script src="/js/jquery-1.9.1.js" type="text/javascript"></script>
    <link href="/js/jquery-ui.css" rel="stylesheet" type="text/css" />
    <script src="/js/jquery-ui.js" type="text/javascript"></script>
    <link type="text/css" href="css/jquery-ui-1.8.19.custom.css" rel="stylesheet" />
    <script type="text/javascript" src="Script/jquery-ui-1.8.19.custom.min.js"></script>  
    <script src="Script/jquery-1.3.1.js" type="text/javascript"></script>
    <script type="text/javascript" src="Script/menu.js"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
 
    <script language="javascript" type="text/javascript">

        function Calling() {
            $("input[id$='Button1']").click(function () {
                if ($("input[id$='txtchemicalname']").val() == '') {
                    alert('Please Enter Dose Name !');
                    $("input[id$='txtchemicalname']").focus();
                    $("input[id$='txtchemicalname']").addClass('textboxerr');
                    return false;
                }
                else {
                    $("input[id$='txtchemicalname']").removeClass('textboxerr');
                }

            });


            $("input[id$='txtchemicalname']").keydown(function (event) {
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


    function autoCompleteEx_ItemSelected(sender, args) {

        var regname = args.get_value().split('~');// alert(regname[0]);
        //document.getElementById("ctl00_ContentPlaceHolder1_TextBoxicode").value = regname[1];
        document.getElementById("ctl00_ContentPlaceHolder1_txtchemicalname").value = regname[0];

        $("#txtchemicalname").focus();
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
         <asp:Label ID="Label1" runat="server">Dose Master</asp:Label>
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
                <label><strong>Dose Name :</strong></label>
                 <asp:TextBox ID="txtchemicalname" runat="server" CssClass="textbox-medium1" 
                   ></asp:TextBox>
                <cc1:AutoCompleteExtender ServiceMethod="SearchDose" OnClientItemSelected="autoCompleteEx_ItemSelected" MinimumPrefixLength="1"
                                    CompletionInterval="100" EnableCaching="false" CompletionSetCount="10" TargetControlID="txtchemicalname" ID="AutoCompleteExtender2" runat="server" 
                                       FirstRowSelected = "false" ></cc1:AutoCompleteExtender>
                <div class="clear">  </div>
            </div>
   

            <div class="form-sec-row">
                <label><strong>&nbsp;</strong></label>
                <asp:Button ID="btnSave" runat="server" OnClick="Button1_Click" Text="Submit" Height="28px" CssClass="submit-button" />
                <asp:Button ID="Button2" runat="server" Text="Cancel" OnClick="Button2_Click" Height="28px" CssClass="submit-button" />
                <div class="clear"></div>
            </div>  
     </div>
     </asp:View>
   <asp:View ID="View2" runat="server">

           <div style='width:100%;'>
  <table width="100%" style='background-color:#FB7B13; color:#FFF;'>
  <tr style='border:1px solid black;'> 
   <td style='width:150px;' align="center">Dose Name</td> 
        <td style='width:70px;' align="center">Edit</td>
          <td style='width:70px;' align="center" id="coldel" runat="server">Delete</td>
          </tr>
  </table> 
  </div> 

     <div class="listing"   style='width:100%; height:500px; overflow:auto;'>
         <asp:GridView id="GridView1"  CssClass="grid"
                 PagerStyle-CssClass="pgr" DataKeyNames ="ID" runat="server"  ShowHeader="false"
                 AutoGenerateColumns="False" AllowPaging="True" PageSize ="100"  Width="100%"
                 OnPageIndexChanging="GridView1_PageIndexChanging"  SelectedRowStyle-BackColor="GreenYellow" OnRowDataBound="GridView1_RowDataBound"
                 OnRowCommand="GridView1_RowCommand" OnRowDeleting="GridView1_RowDeleting">
                <RowStyle HorizontalAlign="Center" />
                <Columns>
                    <asp:TemplateField HeaderText="Id" Visible="false">
                        <ItemTemplate>
                            <asp:Label ID="lblID" runat="server" Text='<%# Bind("ID") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="DOSE NAME "     ItemStyle-Width="150px">
                       
                        <ItemTemplate>
                            <asp:Label ID="lblname" runat="server" Text='<%# Bind("DoseName") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
               
                    <asp:CommandField SelectText="Edit" ShowSelectButton="True"    ItemStyle-Width="70px"  ControlStyle-CssClass="temp" HeaderText="Edit" SelectImageUrl="~/Images/edit.png" ButtonType="Image">
                        <ControlStyle CssClass="temp"></ControlStyle>
                    </asp:CommandField>
                    <asp:CommandField ShowDeleteButton="True" ControlStyle-CssClass="temp"    ItemStyle-Width="70px"  HeaderText="Delete" DeleteImageUrl="~/Images/delete.png" ButtonType="Image">
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

