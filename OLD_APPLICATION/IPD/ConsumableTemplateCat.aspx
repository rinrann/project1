<%@ Page Title="" Language="C#" MasterPageFile="~/MasterAll/MasterPageAll.master" AutoEventWireup="true" CodeFile="ConsumableTemplateCat.aspx.cs" Inherits="IPD_ConsumableTemplateCat" %>

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


        $("input[id$='Button1']").click(function () {

            if ($("input[id$='TextBox1']").val() == '') {
                alert('Please Enter Category Name!');
                $("input[id$='TextBox1']").focus();
                $("input[id$='TextBox1']").addClass('textboxerr');
                return false;
            }
            else {
                $("input[id$='TextBox1']").removeClass('textboxerr');
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

    
 
  <asp:UpdateProgress ID="UpdateProgress1" runat="server">
    <ProgressTemplate>
    
       <div id="aaa" class="progressBackgroundFilter">          </div>
        <div id="bbb" class="processMessage"><img alt="Loading" src="../images/pwait.gif"/>
      
              </div>
    </ProgressTemplate>
    </asp:UpdateProgress>


           <asp:UpdatePanel ID="UpdatePanel1" runat="server">
    <ContentTemplate>

         <div class="pageheader">
         <asp:Label ID="Label1" runat="server">Consumable Template Group</asp:Label>
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
                <label><strong>Cons Template Group :</strong></label>
                <asp:TextBox ID="TextBox1" runat="server" CssClass="textbox-medium1" ></asp:TextBox>
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

                         <div style='width:100%;'>
  <table width="100%" style='background-color:#FB7B13; color:#FFF;'>
  <tr style='border:1px solid black;'> 
   <td style='width:160px;' align="center">Category Name</td> 
        <td style='width:70px;' align="center">Edit</td>
          <td style='width:70px;' align="center">Delete</td>
          </tr>
  </table> 
  </div> 

<div class="listing"  style='width:100%; height:500px; overflow:auto;'>
         <asp:GridView id="GridView1"  CssClass="grid" ShowHeader="false"
                 PagerStyle-CssClass="pgr" DataKeyNames ="TemplateCategoryId" runat="server" 
                 AutoGenerateColumns="False" AllowPaging="True" PageSize ="1000" 
                 OnPageIndexChanging="GridView1_PageIndexChanging"  Width="100%"   SelectedRowStyle-BackColor="GreenYellow"
                 OnRowCommand="GridView1_RowCommand" OnRowDeleting="GridView1_RowDeleting">
                <RowStyle HorizontalAlign="Center" />
                <Columns>
                    <asp:TemplateField HeaderText="Id" Visible="false">
                        <ItemTemplate>
                            <asp:Label ID="TemplateCategoryId" runat="server" Text='<%# Bind("TemplateCategoryId") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Category Name" ItemStyle-Width="160px"   ItemStyle-HorizontalAlign="Center"  >
                       
                        <ItemTemplate>
                            <asp:Label ID="CategoryName" runat="server" Text='<%# Bind("CategoryName") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
             
                                
                    <asp:CommandField SelectText="Edit" ShowSelectButton="True"  ItemStyle-Width="70px"   ItemStyle-HorizontalAlign="Center"  ControlStyle-CssClass="temp" HeaderText="Edit" SelectImageUrl="~/Images/edit.png" ButtonType="Image">
                        <ControlStyle CssClass="temp"></ControlStyle>
                    </asp:CommandField>
                    <asp:CommandField ShowDeleteButton="True" ControlStyle-CssClass="temp"  ItemStyle-Width="70px"   ItemStyle-HorizontalAlign="Center"  HeaderText="Delete" DeleteImageUrl="~/Images/delete.png" ButtonType="Image">
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

