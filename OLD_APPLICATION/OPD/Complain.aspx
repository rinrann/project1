<%@ Page Title="" Language="C#" MasterPageFile="~/MasterAll/MasterPageAll.master" AutoEventWireup="true" CodeFile="Complain.aspx.cs" Inherits="OPD_Complain" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
 
<script language="javascript" type="text/javascript">

    function Calling() {
        $("input[id$='Button1']").click(function () {
            if ($("input[id$='txtchemicalname']").val() == '') {
                alert('Please Enter Complain Name !');
                $("input[id$='txtchemicalname']").focus();
                $("input[id$='txtchemicalname']").addClass('textboxerr');
                return false;
            }
            else {
                $("input[id$='txtchemicalname']").removeClass('textboxerr');
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
        document.getElementById("ctl00_ContentPlaceHolder1_txtchemicalname").value = regname[0];

        $("#txtchemicalname").focus();
        //$("#DropDownList4").val(regname[1]);
    }
</script>


<%--For Busy Loader .............................--%>


<%-- 
  <asp:UpdateProgress ID="UpdateProgress1" runat="server">
    <ProgressTemplate>
    
       <div id="aaa" class="progressBackgroundFilter">          </div>
        <div id="bbb" class="processMessage"><img alt="Loading" src="../images/pwait.gif"/>
      
              </div>
    </ProgressTemplate>
    </asp:UpdateProgress>
--%>

    <%--For Busy Loader End.............................--%>


    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
    <ContentTemplate>
    <div class="pageheader">
         <asp:Label ID="Label1" runat="server">Complain Master</asp:Label>
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
                <label><strong>Complain Name :</strong></label>
                 <asp:TextBox ID="txtchemicalname" runat="server" CssClass="textbox-medium1" 
                   ></asp:TextBox>
                <cc1:AutoCompleteExtender ServiceMethod="SearchComplain" OnClientItemSelected="autoCompleteEx_ItemSelected" MinimumPrefixLength="1"
                                    CompletionInterval="100" EnableCaching="false" CompletionSetCount="10" TargetControlID="txtchemicalname" ID="AutoCompleteExtender2" runat="server" 
                                       FirstRowSelected = "false" ></cc1:AutoCompleteExtender>
                <div class="clear">  </div>
            </div>

                 
            <div class="form-sec-row"  style="display:none;">
                <label style='color:Blue'><strong> Suggest Medicine :</strong></label>
                <div class="clear"></div>
            </div>

            	<div class="form-sec-row" style="display:none;">
                <label><strong>Medicine Group :</strong></label>
               <asp:DropDownList ID="DropDownList1" runat="server" CssClass="textbox-medium1" 
                        AutoPostBack="True" onselectedindexchanged="DropDownList1_SelectedIndexChanged">
                    </asp:DropDownList>
                <div class="clear">  </div>
            </div>

            	<div class="form-sec-row" style="display:none;">
                <label><strong>Medicine Sub Group :</strong></label>
                    <asp:DropDownList ID="DropDownList2" runat="server" CssClass="textbox-medium1">
                    </asp:DropDownList>
                <div class="clear">  </div>
            </div>
   

            <div class="form-sec-row">
                <label><strong>&nbsp;</strong></label>
                <asp:Button ID="Button1" runat="server" OnClick="Button1_Click" Height="28px" Text="Submit" CssClass="submit-button" />
                <asp:Button ID="Button2" runat="server" Text="Cancel" OnClick="Button2_Click" Height="28px" CssClass="submit-button" />
                <div class="clear"></div>
            </div>  
     </div>
     </asp:View>
    <asp:View ID="View2" runat="server">
     <div class="listing"    style='width:100%; height:600px; overflow:auto;'>
         <asp:GridView id="GridView1"  CssClass="grid"
                 PagerStyle-CssClass="pgr" DataKeyNames ="RowId" runat="server" 
                 AutoGenerateColumns="False" AllowPaging="True" PageSize ="100"  Width="100%"
                 OnPageIndexChanging="GridView1_PageIndexChanging"  SelectedRowStyle-BackColor="GreenYellow"    OnRowDataBound="GridView1_RowDataBound1"
                 OnRowCommand="GridView1_RowCommand" OnRowDeleting="GridView1_RowDeleting">
                <RowStyle HorizontalAlign="Center" />
                <Columns>
                    <asp:TemplateField HeaderText="Id" Visible="false">
                        <ItemTemplate>
                            <asp:Label ID="lblID" runat="server" Text='<%# Bind("RowId") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Complaint Name">
                       
                        <ItemTemplate>
                            <asp:Label ID="lblname" runat="server" Text='<%# Bind("ComplainName") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>

                    

                         <asp:TemplateField HeaderText="Medicine Group Id" Visible="false">                       
                        <ItemTemplate>
                            <asp:Label ID="lblMedicineGroupId" runat="server" Text='<%# Bind("MedicineGroupId") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>


                      <asp:TemplateField HeaderText="Medicine Group">                       
                        <ItemTemplate>
                            <asp:Label ID="lblMedicineGroupName" runat="server" Text='<%# Bind("MedicineGroupName") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>

                      <asp:TemplateField HeaderText="Medicine Sub Group">
                       
                        <ItemTemplate>
                            <asp:Label ID="lblSubGrName" runat="server" Text='<%# Bind("SubGrName") %>'></asp:Label>
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

