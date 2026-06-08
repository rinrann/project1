<%@ Page Language="C#" MasterPageFile="~/MasterAll/MasterPageAll.master" AutoEventWireup="true" CodeFile="ServiceCategory.aspx.cs" Inherits="Master_ServiceCategory" %>


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


        $("input[id$='txtServiceCharge']").keydown(function (event) {
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

        $("input[id$='Button1']").click(function () {
            if ($("input[id$='txtServiceCatName']").val() == '') {
                $("input[id$='txtServiceCatName']").addClass('textboxerr');
            }
            if ($("input[id$='txtServiceCatName']").val() == '') {
                alert('Please Enter ServiceCategory name Properly!');
                $("input[id$='txtServiceCatName']").focus();
                $("input[id$='ttxtServiceCatName']").addClass('textboxerr');
                return false;
            }
            else {
                $("input[id$='txtServiceCatName']").removeClass('textboxerr');
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


<%-- 
  <asp:UpdateProgress ID="UpdateProgress1" runat="server">
    <ProgressTemplate>
    
       <div id="aaa" class="progressBackgroundFilter">          </div>
        <div id="bbb" class="processMessage"><img alt="Loading" src="../images/pwait.gif"/>
      
              </div>
    </ProgressTemplate>
    </asp:UpdateProgress>--%>


    <%--For Busy Loader End.............................--%>

    <!-- Form Section html start -->
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <div class="pageheader">
                <asp:Label ID="Label1" runat="server">Service Template Name</asp:Label>
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
                    <div class="form-sec-row" style='display:none;'>
                        <label><strong>
                        ServiceCategory ID :</strong></label>
                        <asp:TextBox ID="txtServiceCatId" runat="server" CssClass="textbox-medium1" 
                    Enabled="False" ></asp:TextBox>
                        <div class="clear">
                        </div>
                    </div>
                    <div class="form-sec-row">
                        <label><strong>
                        ServiceCategory Name :</strong></label>
                        <asp:TextBox ID="txtServiceCatName" runat="server" CssClass="textbox-medium1" ></asp:TextBox>
                        <div class="clear">
                        </div>
                    </div>

                          <div class="form-sec-row">
                        <label><strong>
                        Service Charges :</strong></label>
                        <asp:TextBox ID="txtServiceCharge" runat="server" CssClass="textbox-medium1" ></asp:TextBox>
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
                 <div style='width:100%;'>
  <table width="100%" style='background-color:#FB7B13; color:#FFF;'>
  <tr style='border:1px solid black;'> 
   <td style='width:150px;' align="center">Service Category Name</td> 
    <td style='width:150px;' align="center">Service Charge</td> 
        <td style='width:70px;' align="center">Edit</td>
          <td style='width:70px;' align="center">Delete</td>
          </tr>
  </table> 
  </div> 

            <div class="listing"  style='width:100%; height:500px; overflow:auto;'>
                <asp:GridView id="GridView1"  Width="100%" CssClass="grid" PagerStyle-CssClass="pgr"
                 DataKeyNames ="ServiceCategoryID" runat="server"  ShowHeader="false"
                 AutoGenerateColumns="False" AllowPaging="True"  PageSize="100"
                 OnRowCommand="GridView1_RowCommand"  OnRowDeleting="GridView1_RowDeleting" 
                 OnPageIndexChanging="GridView1_PageIndexChanging" SelectedRowStyle-BackColor="GreenYellow">



                    <Columns>
                        <asp:TemplateField HeaderText="ServiceCategory ID" Visible="false">
                            <ItemTemplate>
                                <asp:Label ID="ServiceCategoryID" runat="server" Text='<%# Bind("ServiceCategoryID") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Service Category Name" ItemStyle-Width="150px"  ItemStyle-HorizontalAlign="Center"  >
                            <ItemTemplate>
                                <asp:Label ID="ServiceCategoryName" runat="server" Text='<%# Bind("ServiceCategoryName") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>

                             <asp:TemplateField HeaderText="Service Charge" ItemStyle-Width="150px"  ItemStyle-HorizontalAlign="Center"  >
                            <ItemTemplate>
                                <asp:Label ID="ServiceCharge" runat="server" Text='<%# Bind("ServiceCharge") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                         

                        <asp:CommandField SelectText="Edit" ShowSelectButton="True"  ItemStyle-Width="70px"   ItemStyle-HorizontalAlign="Center"  ControlStyle-CssClass="temp" HeaderText="Edit" SelectImageUrl="~/Images/edit.png" ButtonType="Image">
                            <ControlStyle CssClass="temp"></ControlStyle>
                        </asp:CommandField>
                        <asp:CommandField ShowDeleteButton="True" ControlStyle-CssClass="temp"   ItemStyle-Width="70px"  ItemStyle-HorizontalAlign="Center"   HeaderText="Delete" DeleteImageUrl="~/Images/delete.png" ButtonType="Image">
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

