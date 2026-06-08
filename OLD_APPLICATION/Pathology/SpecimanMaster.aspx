<%@ Page Title="" Language="C#" MasterPageFile="~/MasterAll/MasterPageAll.master" AutoEventWireup="true" CodeFile="SpecimanMaster.aspx.cs" Inherits="Pathology_SpecimanMaster" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
 
    <script language="javascript" type="text/javascript">
 

    function Calling() {
        $("input[id$='txtname']").focus(function () {
            $("input[id$='txtname']").addClass('textboxborder');
        });
        $("input[id$='txtname']").blur(function () {
            $("input[id$='txtname']").removeClass('textboxborder');
        });

        $("input[id$='Button1']").click(function () {
            if ($("input[id$='txtname']").val() == '') {
                $("input[id$='txtname']").addClass('textboxerr');
            }

            if ($("input[id$='txtname']").val() == '') {
                alert('Please Enter Specimen Name !');
                $("input[id$='txtname']").focus();
                $("input[id$='txtname']").addClass('textboxerr');
                return false;
            }
            else {
                $("input[id$='txtname']").removeClass('textboxerr');
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
        document.getElementById("ctl00_ContentPlaceHolder1_txtname").value = regname[0];

        $("#txtname").focus();
        //$("#DropDownList4").val(regname[1]);
    }

    function autoCompleteEx_ItemSelected1(sender, args) {

        var regname = args.get_value().split('~');// alert(regname[0]);
        //document.getElementById("ctl00_ContentPlaceHolder1_TextBoxicode").value = regname[1];
        document.getElementById("ctl00_ContentPlaceHolder1_txtspnm").value = regname[0];

        $("#txtspnm").focus();
        //$("#DropDownList4").val(regname[1]);
    }
</script> 
<%--For Busy Loader .............................--%>
<%--   <asp:UpdateProgress ID="UpdateProgress1" runat="server">
    <ProgressTemplate>
    
       <div id="aaa" class="progressBackgroundFilter">          </div>
        <div id="bbb" class="processMessage"><img alt="Loading" src="../images/pwait.gif"/>
      
              </div>
    </ProgressTemplate>
    </asp:UpdateProgress>--%>


    <%--For Busy Loader End.............................--%>

    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
    <ContentTemplate>
    <div class="pageheader">
         <asp:Label ID="Label1" runat="server">Specimen Master</asp:Label>
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
			<div class="form-sec-row">
                <label><strong>Specimen Code :</strong></label>
               <asp:TextBox ID="txtcode" runat="server" CssClass="textbox-medium1" Enabled="False" 
                   ></asp:TextBox>
                <div class="clear">  </div>
            </div>
           
            <div class="form-sec-row">
                <label><strong>Specimen Name :</strong></label>
                <asp:TextBox ID="txtname" runat="server" CssClass="textbox-medium1" 
                   ></asp:TextBox>

                <cc1:AutoCompleteExtender ServiceMethod="SearchSpecimen" OnClientItemSelected="autoCompleteEx_ItemSelected" MinimumPrefixLength="1"
                                    CompletionInterval="100" EnableCaching="false" CompletionSetCount="10" TargetControlID="txtname" ID="AutoCompleteExtender2" runat="server" 
                                       FirstRowSelected = "false" ></cc1:AutoCompleteExtender>
                <div class="clear"></div>
            </div>
              <div class="form-sec-row">
                <label><strong>Department :</strong></label>
                <asp:DropDownList ID="DropDownList1" runat="server" CssClass="textbox-medium1">
                </asp:DropDownList>
                <div class="clear"></div>
            </div>
            <div class="form-sec-row">
                <label><strong>&nbsp;</strong></label>
                <asp:Button ID="Button1" runat="server"  Height="28px"  OnClick="Button1_Click" Text="Submit" CssClass="submit-button" />
                <asp:Button ID="Button2" runat="server" Height="28px"  Text="Cancel" OnClick="Button2_Click" CssClass="submit-button" />
                <div class="clear"></div>
            </div>  
   
     </div>
 
 </asp:View>
  <asp:View ID="View2" runat="server">
      
  <div style='width:100%;'>
  <table width="100%" style='background-color:#FB7B13; color:#FFF;'>
  <tr style='border:1px solid black;'> 
   <td style='width:90px;' align="center">Specimen Code</td>
   <td style='width:110px;'  align="center">Specimen Name</td>
    <td style='width:110px;' align="center">Department Name</td>
        <td style='width:70px;' align="center">Edit</td>
          <td style='width:70px;' align="center" id="code1" runat="server">Delete</td>
          </tr>
  </table> 
  </div> 

     <div class="listing"    style='width:100%; height:450px; overflow:auto;'>
         <asp:GridView id="GridView1"  CssClass="grid" PagerStyle-CssClass="pgr" DataKeyNames ="SCode,SName" runat="server" AutoGenerateColumns="False"  OnRowDataBound="GridView1_RowDataBound"
  SelectedRowStyle-BackColor="GreenYellow" AllowPaging="True" PageSize ="100" Width="100%"  ShowHeader="false"  onpageindexchanging="GridView1_PageIndexChanging" 
             onrowcommand="GridView1_RowCommand" onrowdeleting="GridView1_RowDeleting">
                <RowStyle HorizontalAlign="Center" />
                <Columns>
                    <asp:TemplateField HeaderText="Specimen Code" ItemStyle-Width="90px"  >
                        <ItemTemplate>
                            <asp:Label ID="lblcode" runat="server" Text='<%# Bind("SCode") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Specimen Name" ItemStyle-Width="110px"  >
                       
                        <ItemTemplate>
                            <asp:Label ID="lblname" runat="server" Text='<%# Bind("SName") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>     
                    
                          <asp:TemplateField HeaderText="Department Name" ItemStyle-Width="110px"  >
                       
                        <ItemTemplate>
                            <asp:Label ID="lbldeptname" runat="server" Text='<%# Bind("DeptName") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>    
                                                         
                    <asp:CommandField SelectText="Edit" ShowSelectButton="True"  ItemStyle-Width="70px"  ControlStyle-CssClass="temp" HeaderText="Edit" SelectImageUrl="~/Images/edit.png" ButtonType="Image">
                        <ControlStyle CssClass="temp"></ControlStyle>
                    </asp:CommandField>
                    <asp:CommandField ShowDeleteButton="True" ControlStyle-CssClass="temp" ItemStyle-Width="70px"   HeaderText="Delete" DeleteImageUrl="~/Images/delete.png" ButtonType="Image">
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

