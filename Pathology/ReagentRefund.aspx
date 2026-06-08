<%@ Page Title="" Language="C#" MasterPageFile="~/MasterAll/MasterPageAll.master" AutoEventWireup="true" CodeFile="ReagentRefund.aspx.cs" Inherits="Pathology_ReagentRefund" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">

  

<script type="text/javascript">

    function ShowDialog() {

        var rtvalue = window.open("ReagentPopup.aspx", "sss", "Width:800px; Height:550px; dialogLeft:250px;");
        document.getElementById("ctl00_ContentPlaceHolder1_txtid").value = rtvalue.NameValue 
    }

    function Calling() {

        var date = new Date();
        $("input[id$='txtdate']").datepicker({ dateFormat: 'dd/mm/yy' });
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
         <asp:Label ID="Label1" runat="server">Reagent Refund</asp:Label>
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
            <asp:HiddenField ID="TextBox4" runat="server" />
			           
            <div class="form-sec-row">
                <label><strong>Purchase ID :</strong></label>
                <asp:TextBox ID="txtid" runat="server" CssClass="textbox-medium1"
                   ></asp:TextBox><asp:Button ID="Button3" runat="server" Height="28px" Text="Search" CssClass="submit-button" OnClientClick="return ShowDialog();"/>
                <div class="clear"></div>
            </div>

            
            <div class="form-sec-row">
                <label><strong>Date :</strong></label>
                  <asp:TextBox ID="txtdate" runat="server" CssClass="textbox-medium1" MaxLength="10" onkeypress="return CheckNumber(event)"
                   ></asp:TextBox>
                <div class="clear"></div>
            </div>
            <div class="form-sec-row">
                <label><strong>Refund Quantity :</strong></label>
                 <asp:TextBox ID="txtpurchaseqty" runat="server" CssClass="textbox-medium1"  MaxLength="10" onkeypress="return CheckNumber(event)"
                   ></asp:TextBox>
                <div class="clear"></div>
            </div>
            <div class="form-sec-row">
                <label><strong>Refund Price :</strong></label>
                 <asp:TextBox ID="txtprice" runat="server" CssClass="textbox-medium1"  MaxLength="10" onkeypress="return CheckNumber(event)"
                   ></asp:TextBox>
                <div class="clear"></div>
            </div>
           
            <div class="form-sec-row">
                <label><strong>&nbsp;</strong></label>
                <asp:Button ID="Button1" runat="server"  Height="28px" OnClick="Button1_Click" Text="Submit" CssClass="submit-button" />
                <asp:Button ID="Button2" runat="server" Text="Cancel" OnClick="Button2_Click"  Height="28px"  CssClass="submit-button" />
                <div class="clear"></div>
            </div>  
   
     </div>
     </asp:View>
   <asp:View ID="View2" runat="server">
     <div class="listing"     style='width:100%; height:650px; overflow:auto;'>
         <asp:GridView id="GridView1"  CssClass="grid" PagerStyle-CssClass="pgr"  DataKeyNames="Id"
        runat="server" AutoGenerateColumns="False" 
             AllowPaging="True" PageSize ="100" Width="100%" 
             onpageindexchanging="GridView1_PageIndexChanging" 
             onrowcommand="GridView1_RowCommand" onrowdeleting="GridView1_RowDeleting">
                <RowStyle HorizontalAlign="Center" />
                <Columns>

                    <asp:TemplateField HeaderText=" ID" Visible="false" >
                        <ItemTemplate>
                            <asp:Label ID="lblcode" runat="server" Text='<%# Bind("Id") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>

                      <asp:TemplateField HeaderText="Purchase ID">
                        <ItemTemplate>
                            <asp:Label ID="lblpurid" runat="server" Text='<%# Bind("PurchaseId") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Refund Date">
                       
                        <ItemTemplate>
                            <asp:Label ID="lbldate" runat="server" Text='<%# Bind("Date1") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                                       
                    <asp:TemplateField HeaderText="Refund Qty">
                        <ItemTemplate>                        
                            <asp:Label ID="lblqty" runat="server" Text='<%# Bind("RefundQty") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField> 
                       
                    <asp:TemplateField HeaderText="Refund Price">
                        <ItemTemplate>                        
                            <asp:Label ID="lbltestper" runat="server" Text='<%# Bind("RefundPrice") %>'></asp:Label>
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

