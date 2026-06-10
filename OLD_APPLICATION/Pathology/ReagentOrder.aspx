<%@ Page Title="" Language="C#" MasterPageFile="~/MasterAll/MasterPageAll.master" AutoEventWireup="true" CodeFile="ReagentOrder.aspx.cs" Inherits="Pathology_ReagentOrder" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
 
<script type="text/javascript" language="javascript">
 

    function calculate() {
        var a = document.getElementById("ctl00_ContentPlaceHolder1_txtpurchaseqty").value;
        var b = document.getElementById("ctl00_ContentPlaceHolder1_txtprice").value;
        var c = a * b;
        document.getElementById("ctl00_ContentPlaceHolder1_txttotal").value = c;
      
       
    }

     function ShowDialog1() {
            var rtvalue = window.open("AgentPopup.aspx", "sss", "Width:800px; Height:550px; dialogLeft:250px;");
            document.getElementById("ctl00_ContentPlaceHolder1_TextBox1").value = rtvalue;
        }
    function ShowDialog() {

        var ddlvalue = document.getElementById("ctl00_ContentPlaceHolder1_DropDownList1").value;
        //alert(ddlvalue);
        if (ddlvalue == "Supplier") {
            var rtvalue = window.open("SupplierPopup.aspx", "sss", "Width:800px; Height:550px; dialogLeft:250px;");
            document.getElementById("ctl00_ContentPlaceHolder1_txtid").value = rtvalue;
        }
        else {
            if (ddlvalue == "Manufacturer") {
                var rtvalue = window.open("ManufacturerPopup.aspx", "sss", "Width:800px; Height:550px; dialogLeft:250px;");
                document.getElementById("ctl00_ContentPlaceHolder1_txtid").value = rtvalue;
            }
            else {
                if (ddlvalue == "Company") {
                    var rtvalue = window.open("Companypopup.aspx", "sss", "Width:800px; Height:550px; dialogLeft:250px;");
                    document.getElementById("ctl00_ContentPlaceHolder1_txtid").value = rtvalue;
                }
                else {
                    alert("Select Type..");
                }
            }
        }

    }


    function Calling() {

        var date = new Date();
        $("input[id$='txtdate']").datepicker({ dateFormat: 'dd/mm/yy', minDate: date });


        $("input[id$='Button1']").click(function () {
            if ($("input[id$='TextBox1']").val() == '') {
                $("input[id$='TextBox1']").addClass('textboxerr');
            }

            if ($("input[id$='TextBox1']").val() == '') {
                alert('Please Enter Reagent Name !');
                $("input[id$='TextBox1']").focus();
                $("input[id$='TextBox1']").addClass('textboxerr');
                return false;
            }
            else {
                $("input[id$='TextBox1']").removeClass('textboxerr');
            }


            if ($("select[id$='DropDownList1']").val() == '0') {
                alert('Select Type!');
                $("select[id$='DropDownList1']").focus();
                $("select[id$='DropDownList1']").addClass('textboxerr');
                return false;
            }
            else {
                $("select[id$='DropDownList1']").removeClass('textboxerr');
            }



            if ($("input[id$='txtid']").val() == '') {
                $("input[id$='txtid']").addClass('textboxerr');
            }

            if ($("input[id$='txtid']").val() == '') {
                alert('Please Enter ID !');
                $("input[id$='txtid']").focus();
                $("input[id$='txtid']").addClass('textboxerr');
                return false;
            }
            else {
                $("input[id$='txtid']").removeClass('textboxerr');
            }



            if ($("input[id$='txtdate']").val() == '') {
                $("input[id$='txtdate']").addClass('textboxerr');
            }

            if ($("input[id$='txtdate']").val() == '') {
                alert('Please Enter Date !');
                $("input[id$='txtdate']").focus();
                $("input[id$='txtdate']").addClass('textboxerr');
                return false;
            }
            else {
                $("input[id$='txtdate']").removeClass('textboxerr');
            }





            if ($("input[id$='txtpurchaseqty']").val() == '') {
                $("input[id$='txtpurchaseqty']").addClass('textboxerr');
            }

            if ($("input[id$='txtpurchaseqty']").val() == '') {
                alert('Please Enter Quantity !');
                $("input[id$='txtpurchaseqty']").focus();
                $("input[id$='txtpurchaseqty']").addClass('textboxerr');
                return false;
            }
            else {
                $("input[id$='txtpurchaseqty']").removeClass('textboxerr');
            }


            if ($("input[id$='txtbonusqty']").val() == '') {
                $("input[id$='txtbonusqty']").addClass('textboxerr');
            }

            if ($("input[id$='txtbonusqty']").val() == '') {
                alert('Please Enter Bonus Quantity !');
                $("input[id$='txtbonusqty']").focus();
                $("input[id$='txtbonusqty']").addClass('textboxerr');
                return false;
            }
            else {
                $("input[id$='txtbonusqty']").removeClass('textboxerr');
            }



            if ($("input[id$='txtprice']").val() == '') {
                $("input[id$='txtprice']").addClass('textboxerr');
            }

            if ($("input[id$='txtprice']").val() == '') {
                alert('Please Enter Price !');
                $("input[id$='txtprice']").focus();
                $("input[id$='txtprice']").addClass('textboxerr');
                return false;
            }
            else {
                $("input[id$='txtprice']").removeClass('textboxerr');
            }






            if ($("input[id$='txttotal']").val() == '') {
                $("input[id$='txttotal']").addClass('textboxerr');
            }

            if ($("input[id$='txttotal']").val() == '') {
                alert('Please Enter Total Price !');
                $("input[id$='txttotal']").focus();
                $("input[id$='txttotal']").addClass('textboxerr');
                return false;
            }
            else {
                $("input[id$='txttotal']").removeClass('textboxerr');
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
         <asp:Label ID="Label1" runat="server">Reagent Order</asp:Label>
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
                <label><strong>Reagent Name :</strong></label>
          
                <asp:TextBox ID="TextBox1" runat="server" CssClass="textbox-medium1" 
                    Enabled="False"></asp:TextBox><asp:Button ID="Button5" runat="server" Height="28px" Text="Search Reagent"  CssClass="submit-buttonCheck" OnClientClick="return ShowDialog1();" />
                 <div class="clear"></div>
            </div>
			<div class="form-sec-row">
                <label><strong>Type :</strong></label>
                <asp:DropDownList ID="DropDownList1" runat="server" CssClass="textbox-medium1">
                    <asp:ListItem>Select</asp:ListItem>
                    <asp:ListItem>Supplier</asp:ListItem>
                    <asp:ListItem>Manufacturer</asp:ListItem>
                    <asp:ListItem>Company</asp:ListItem>
                </asp:DropDownList>
                <div class="clear">  </div>
            </div>
           
            <div class="form-sec-row">
                <label><strong>ID :</strong></label>
                <asp:TextBox ID="txtid" runat="server" CssClass="textbox-medium1" 
                   ></asp:TextBox><asp:Button ID="Button3" runat="server"  Height="28px" Text="Get ID" CssClass="submit-button" OnClientClick="return ShowDialog();"/><asp:Button
                       ID="Button4" runat="server" Text="New Entry" CssClass="submit-button"   Height="28px"
                    onclick="Button4_Click" />
                <div class="clear"></div>
            </div>

            
            <div class="form-sec-row">
                <label><strong>Date :</strong></label>
                  <asp:TextBox ID="txtdate" runat="server" CssClass="textbox-medium1"
                   ></asp:TextBox>
                         <asp:Label ID="Label3" runat="server" ForeColor="Green" Text="(DD/MM/YYYY)"></asp:Label>
                <div class="clear"></div>
            </div>
            <div class="form-sec-row">
                <label><strong>Purchase Quantity :</strong></label>
                 <asp:TextBox ID="txtpurchaseqty" runat="server" CssClass="textbox-medium1" onkeypress="return CheckNumber(event);"
                   ></asp:TextBox>
                <div class="clear"></div>
            </div>
            <div class="form-sec-row">
                <label><strong>Bonus Quantity :</strong></label>
                 <asp:TextBox ID="txtbonusqty" runat="server" CssClass="textbox-medium1" 
                   ></asp:TextBox>
                <div class="clear"></div>
            </div>
            <div class="form-sec-row">
                <label><strong>Price :</strong></label>
                 <asp:TextBox ID="txtprice" runat="server" CssClass="textbox-medium1" onkeypress="return checkdecimal(event);" onblur="return calculate();"
                   ></asp:TextBox>
                <div class="clear"></div>
            </div>

             <div class="form-sec-row">
                <label><strong>Total Price :</strong></label>
                 <asp:TextBox ID="txttotal" runat="server" CssClass="textbox-medium1" 
                     Enabled="False"></asp:TextBox>
                <div class="clear"></div>
            </div>
            <div class="form-sec-row">
                <label><strong>&nbsp;</strong></label>
                <asp:Button ID="Button1" runat="server" Height="28px" OnClick="Button1_Click" Text="Submit" CssClass="submit-button" />
                <asp:Button ID="Button2" runat="server"  Height="28px"  Text="Cancel" OnClick="Button2_Click" CssClass="submit-button" />
                 
                <div class="clear"></div>
            </div>  
   
     </div>
 </asp:View>
 <asp:View ID="View2" runat="server">
     <div class="listing"    style='width:100%; height:650px; overflow:auto;'>
         <asp:GridView id="GridView1"  CssClass="grid" PagerStyle-CssClass="pgr" 
             DataKeyNames ="PurchaseId" runat="server" AutoGenerateColumns="False"   SelectedRowStyle-BackColor="GreenYellow"
             AllowPaging="True" PageSize ="100" Width="100%" 
             onpageindexchanging="GridView1_PageIndexChanging"
             onrowcommand="GridView1_RowCommand" onrowdeleting="GridView1_RowDeleting">
                <RowStyle HorizontalAlign="Center" />
                <Columns>
                    <asp:TemplateField HeaderText="Purchase ID" Visible="false">
                        <ItemTemplate>
                            <asp:Label ID="lblcode" runat="server" Text='<%# Bind("PurchaseId") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Reagent Name">
                       
                        <ItemTemplate>
                            <asp:Label ID="lblname" runat="server" Text='<%# Bind("ReagentName") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                                       
                    <asp:TemplateField HeaderText="Date">
                        <ItemTemplate>                        
                            <asp:Label ID="lbldate" runat="server" Text='<%# Bind("date1") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField> 
                       
                    <asp:TemplateField HeaderText="Purchase Quantity">
                        <ItemTemplate>                        
                            <asp:Label ID="lblpqty" runat="server" Text='<%# Bind("PurchaseQty") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>

                     <asp:TemplateField HeaderText="S/M/C Id">
                        <ItemTemplate>                        
                            <asp:Label ID="lblid" runat="server" Text='<%# Bind("ID") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                      <asp:TemplateField HeaderText="Bonus Quantity" >
                        <ItemTemplate>                        
                            <asp:Label ID="lblbqty" runat="server" Text='<%# Bind("BonusQty") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>

                      <asp:TemplateField HeaderText="Price Per Unit" >
                        <ItemTemplate>                        
                            <asp:Label ID="lblprice" runat="server" Text='<%# Bind("PerPrice") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>

                      <asp:TemplateField HeaderText="Total Price" >
                        <ItemTemplate>                        
                            <asp:Label ID="lbltotal" runat="server" Text='<%# Bind("TotalPrice") %>'></asp:Label>
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

