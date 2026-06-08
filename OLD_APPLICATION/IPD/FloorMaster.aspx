<%@ Page Language="C#" MasterPageFile="~/MasterAll/MasterPageAll.master" AutoEventWireup="true" CodeFile="FloorMaster.aspx.cs" Inherits="Master_FloorMaster" %>

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
        $("input[id$='txtFloorName']").focus(function () {
            $("input[id$='txtFloorName']").addClass('textboxborder');
        });
        $("input[id$='txtFloorName']").blur(function () {
            $("input[id$='txtFloorName']").removeClass('textboxborder');
        });
        $("input[id$='Button1']").click(function () {
            if ($("input[id$='txtFloorName']").val() == '') {
                $("input[id$='txtFloorName']").addClass('textboxerr');
            }
            if ($("input[id$='txtFloorName']").val() == '') {
                alert('Please Enter Floor name Properly!');
                $("input[id$='txtFloorName']").focus();
                $("input[id$='txtFloorName']").addClass('textboxerr');
                return false;
            }
            else {
                $("input[id$='txtFloorName']").removeClass('textboxerr');
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
                <asp:Label ID="Label1" runat="server">Floor Master</asp:Label>
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
                        <label><strong>
                        Floor Name :</strong></label>
                        <asp:TextBox ID="txtFloorName" runat="server" CssClass="textbox-medium1" ></asp:TextBox>
                        <div class="clear">
                        </div>
                    </div>

                    <div class="form-sec-row">
                        <label><strong>
                        Pattern Text  :</strong></label>
                        <asp:TextBox ID="txtPattText" runat="server" CssClass="textbox-medium1" ></asp:TextBox>
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
                            Text="Submit" onclick="Button1_Click1" />
                        <asp:Button ID="Button2" runat="server" CssClass="submit-button"   Height="28px"
                            Text="Cancel" onclick="Button2_Click" />
                        <div class="clear">
                        </div>
                    </div>
                    <div class="clear">
                    </div>
                </div>
             </asp:View>
                    
                    <asp:View ID="View2" runat="server">
            <div class="listing"  style='width:100%; height:400px; overflow:auto;'>
                <asp:GridView id="GridView1"  Width="100%" CssClass="grid" PagerStyle-CssClass="pgr"
                 DataKeyNames ="FloorID" runat="server" 
                 AutoGenerateColumns="False" AllowPaging="True" PageSize="100"
                 OnRowCommand="GridView1_RowCommand"  OnRowDeleting="GridView1_RowDeleting"  OnRowDataBound="GridView1_RowDataBound"
                 OnPageIndexChanging="GridView1_PageIndexChanging" SelectedRowStyle-BackColor="GreenYellow">

                            <RowStyle HorizontalAlign="Center" />

                    <Columns>
                        <asp:TemplateField HeaderText="Floor ID" Visible="false">
                            <ItemTemplate>
                                <asp:Label ID="FloorID" runat="server" Text='<%# Bind("FloorID") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Floor Name">
                            <ItemTemplate>
                                <asp:Label ID="FloorName" runat="server" Text='<%# Bind("FloorName") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                         <asp:TemplateField HeaderText="Pattern Text">
                            <ItemTemplate>
                                <asp:Label ID="PatternText" runat="server" Text='<%# Bind("PatternText") %>'></asp:Label>
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

