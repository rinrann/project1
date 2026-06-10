<%@ Page Language="C#" MasterPageFile="~/MasterAll/MasterPageAll.master" AutoEventWireup="true" CodeFile="MedicineMaster.aspx.cs" Inherits="Master_MedicineMaster" %>
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
    $(document).ready(function () {

        $("input[id$='Button1']").click(function () {

            //if ($("input[id$='txtMedicineName']").val() == '') {
            //    alert('Medicine Name Can not be Blank!');
            //    $("input[id$='txtMedicineName']").focus();
            //    $("input[id$='txtMedicineName']").addClass('textboxerr');
            //    return false;
            //}
            //else {
            //    $("input[id$='txtMedicineName']").removeClass('textboxerr');
            //}



            //if ($("input[id$='TextBox1']").val() == '') {
            //    alert('Enter Alert Quantity!');
            //    $("input[id$='TextBox1']").focus();
            //    $("input[id$='TextBox1']").addClass('textboxerr');
            //    return false;
            //}
            //else {
            //    $("input[id$='TextBox1']").removeClass('textboxerr');
            //}

            if ($("select[id$='DropDownList1']").val() == '0') {
                alert('Please Select Manufacturing Company !');
                $("select[id$='DropDownList1']").addClass('textboxerr');
                $("select[id$='DropDownList1']").focus();
                return false;
            }
            else {
                $("select[id$='DropDownList1']").removeClass('textboxerr');
            }


            //if ($("select[id$='DropDownList2']").val() == '0') {
            //    alert('Please Select Group !');
            //    $("select[id$='DropDownList2']").addClass('textboxerr');
            //    $("select[id$='DropDownList2']").focus();
            //    return false;
            //}
            //else {
            //    $("select[id$='DropDownList2']").removeClass('textboxerr');
            //}


            //if ($("select[id$='DropDownList4']").val() == '0') {
            //    alert('Please Select Medicine Sub Group !');
            //    $("select[id$='DropDownList4']").addClass('textboxerr');
            //    $("select[id$='DropDownList4']").focus();
            //    return false;
            //}
            //else {
            //    $("select[id$='DropDownList4']").removeClass('textboxerr');
            //}

            if ($("select[id$='ddlitemType']").val() == '0') {
                alert('Please Select Medicine/Reagent Type !');
                $("select[id$='ddlitemType']").addClass('textboxerr');
                $("select[id$='ddlitemType']").focus();
                return false;
            }
            else {
                $("select[id$='ddlitemType']").removeClass('textboxerr');
            }
        });

        $("input[id$='TextBox1']").keydown(function (event) {
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
    });
                
</script>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">



<%-- 
  <asp:UpdateProgress ID="UpdateProgress1" runat="server">
    <ProgressTemplate>
    
       <div id="aaa" class="progressBackgroundFilter">          </div>
        <div id="bbb" class="processMessage"><img alt="Loading" src="../images/pwait.gif"/>
      
              </div>
    </ProgressTemplate>
    </asp:UpdateProgress>--%>


    <!-- Form Section html start -->

    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <div class="pageheader">
                <asp:Label ID="Label1" runat="server">Medicine / Reagent Master</asp:Label>
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
                    <asp:HiddenField ID="HdnItemcode" runat="server" Value="0" />

                    <div class="form-sec-row">
                    <label><strong>
                        Medicine/Reagent :</strong></label>
                    
                     <asp:DropDownList ID="ddlitemType" runat="server" CssClass="textbox-medium1"></asp:DropDownList>
                    <div class="clear">
                    </div>
                </div>
       
                    <div class="form-sec-row">
                        <label>
                        <strong>
                        Medicine/Reagent Name :</strong></label>
                        <asp:TextBox ID="txtMedicineName" runat="server" Width="180px" 
                            CssClass="textbox-medium1" TabIndex="1" ></asp:TextBox>
                          <cc1:AutoCompleteExtender ServiceMethod="SearchCustomers"  MinimumPrefixLength="1"    CompletionInterval="100" EnableCaching="false" 
                   CompletionSetCount="10" TargetControlID="txtMedicineName"  ID="AutoCompleteExtender1" runat="server" FirstRowSelected = "false" ></cc1:AutoCompleteExtender>
                            <asp:DropDownList ID="DropDownList6" runat="server" 
                            CssClass="textbox-medium1" Height="27px" Width="118px" TabIndex="2" Visible="false">
                         </asp:DropDownList>
                        <div class="clear">
                        </div>
                    </div>
                    <div class="form-sec-row">
                        <label><strong>Generic Name :</strong></label>
                           <asp:TextBox ID="txtGenericName" runat="server" CssClass="textbox-medium1" TabIndex="9"></asp:TextBox>
                        <div class="clear"></div>
                    </div>
                     <div class="form-sec-row">
                        <label>
                        <strong>
                        Manufacturing Company :</strong></label>
                         <asp:DropDownList ID="DropDownList1" runat="server" CssClass="textbox-medium1" 
                             TabIndex="3" >
                         </asp:DropDownList>
                        <div class="clear">
                        </div>
                    </div>

                     <div class="form-sec-row">
                        <label>
                        <strong>
                        Medicine/Reagent Group :</strong></label>
                         <asp:DropDownList ID="DropDownList2" runat="server" CssClass="textbox-medium1" 
                             AutoPostBack="True" 
                             onselectedindexchanged="DropDownList2_SelectedIndexChanged"  TabIndex="4">
                         </asp:DropDownList>
                        <div class="clear">
                        </div>
                    </div>

                    
                     <div class="form-sec-row" style="display:none;">
                        <label>
                        <strong>
                        Medicine/Reagent Sub Group :</strong></label>
                         <asp:DropDownList ID="DropDownList4" runat="server" CssClass="textbox-medium1" 
                             TabIndex="5" >
                         </asp:DropDownList>
                        <div class="clear">
                        </div>
                    </div>

                    <div class="form-sec-row"  style="display:none;">
                        <label>
                        <strong>
                        Injectable Or Not :</strong></label>
                   <asp:DropDownList ID="DropDownList7" runat="server" CssClass="textbox-medium1" 
                            TabIndex="6" >
                         </asp:DropDownList>
                        <div class="clear">
                        </div>
                    </div>

                    <div class="form-sec-row">
                        <label>
                        <strong>
                        Purchase Unit :</strong></label>
                         <asp:DropDownList ID="DropDownList3" runat="server" CssClass="textbox-medium1" 
                            TabIndex="7" >
                         </asp:DropDownList>
                        <div class="clear">
                        </div>
                    </div>

                   <div class="form-sec-row">
                        <label>
                        <strong>
                        Selling/Billing Unit :</strong></label>
                         <asp:DropDownList ID="DropDownList5" runat="server" CssClass="textbox-medium1" 
                                      TabIndex="8" >
                         </asp:DropDownList>
                        <div class="clear">
                        </div>
                    </div>


                       <div class="form-sec-row">
                        <label>
                        <strong>
                        Alert Quantity :</strong></label>
                           <asp:TextBox ID="TextBox1" runat="server" CssClass="textbox-medium1" 
                               TabIndex="9"></asp:TextBox>
                        <div class="clear">
                        </div>
                    </div>

                    <div class="form-sec-row">
                        <label>
                        <strong>
                        Conversion Factor :</strong></label>
                           <asp:TextBox ID="TextBox2" runat="server" CssClass="textbox-medium1" TabIndex="10"></asp:TextBox>
                        <div class="clear">
                        </div>
                    </div>
                    
                    <div class="form-sec-row">
                        <label>
                        <strong>
                        HSN Code :</strong></label>
                           <asp:TextBox ID="txtHsnNo" runat="server" CssClass="textbox-medium1" TabIndex="11"></asp:TextBox>
                        <div class="clear">
                        </div>
                    </div>
                    <div class="form-sec-row">
                        <label>
                        <strong>
                        IGST Rate :</strong></label>
                           <asp:TextBox ID="txtIGstRate" runat="server" CssClass="textbox-medium1" TabIndex="12" style="text-align:right;" Text="0.00"></asp:TextBox>
                        <div class="clear">
                        </div>
                    </div>

                    <div class="form-sec-row">
                        <label>
                        <strong>
                        CGST Rate :</strong></label>
                           <asp:TextBox ID="txtCGstRate" runat="server" CssClass="textbox-medium1" TabIndex="12" style="text-align:right;" Text="0.00"></asp:TextBox>
                        <div class="clear">
                        </div>
                    </div>

                    <div class="form-sec-row">
                        <label>
                        <strong>
                        SGST Rate :</strong></label>
                           <asp:TextBox ID="txtSGstRate" runat="server" CssClass="textbox-medium1" TabIndex="12" style="text-align:right;" Text="0.00"></asp:TextBox>
                        <div class="clear">
                        </div>
                    </div>
                     <div class="form-sec-row">
                        <label>
                        <strong>
                        Purchase Without GST :</strong></label>
                           <asp:CheckBox ID="chkPurGst" runat="server" />
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
                            Text="Submit" onclick="Button1_Click" TabIndex="11"   />
                        <asp:Button ID="Button2" runat="server" CssClass="submit-button"  Height="28px"
                            Text="Cancel" onclick="Button2_Click" TabIndex="12"   />
                    <asp:Button ID="Button5" runat="server" CssClass="submit-generate" Text="Go To Purchase"  Height="28px" OnClick="Button5_Click" Visible="false" />
                        <div class="clear">
                        </div>
                    </div>
                    <div class="clear">
                    </div>
                </div>
     
     </asp:View>
      <asp:View ID="View2" runat="server">
       <div style='width:100%;'>
         <table width="100%" style='background-color:#3AA8FC; color:#FFF;'>
         <tr>
         <%--<td><strong>Group :</strong></td>
         <td> <asp:DropDownList ID="DropDownList8" Width="180px" AutoPostBack="true"  
                 runat="server" onselectedindexchanged="DropDownList8_SelectedIndexChanged">
             </asp:DropDownList></td>
    <td><strong>Sub Group :</strong></td>
         <td> <asp:DropDownList ID="DropDownList9" Width="180px" runat="server">
             </asp:DropDownList></td>--%>
         <td><strong>Medicine :</strong></td>
         <td>  
             <asp:TextBox ID="TextBox3" Width="180px"  runat="server"></asp:TextBox>  </td>
         <td>
             <asp:Button ID="Button3" runat="server" Text="Search" CssClass="submit-button" onclick="Button3_Click" />
         </td>
         </tr>
         </table>
       </div>
        <div style='width:100%;'>
  <%--<table width="100%" style='background-color:#FB7B13; color:#FFF;'>
    <tr style='border:1px solid black;'> 
        <td style='width:15%;' align="center">Medicine Name</td>
        <td style='width:15%;' align="center">Generic Name</td>
        <td style='width:15%;'  align="center">Manufacturing Company</td>
        <td style='width:10%;' align="center">Medicine Group Name</td>
        <td style='width:5%;' align="center">Purchase Unit</td>
        <td style='width:10%;' align="center">Sub Group Name</td>
        <td style='width:5%;' align="center">Conversion Factor</td> 
        <td style='width:5%;' align="center">Alert Quantity</td>
        <td style='width:10%;' align="center">Edit</td>
        <td style='width:10%;' align="center" id="coldel" runat="server">Delete</td>
     </tr>
  </table> --%>
  </div> 

            <div class="listing"  style='width:100%; height:500px; overflow:auto;'>
                <asp:GridView id="GridView1"  Width="100%" CssClass="grid" PagerStyle-CssClass="pgr"
                 DataKeyNames ="MedicineID" runat="server" 
                 AutoGenerateColumns="False" AllowPaging="True"  PageSize="1000" ShowHeader="true"
                 OnRowCommand="GridView1_RowCommand" OnRowDataBound="GridView1_RowDataBound"  OnRowDeleting="GridView1_RowDeleting" SelectedRowStyle-BackColor="GreenYellow"
                 OnPageIndexChanging="GridView1_PageIndexChanging" 
                    onselectedindexchanged="GridView1_SelectedIndexChanged" >

                       <RowStyle HorizontalAlign="Center" />

                    <Columns>
                        <asp:TemplateField HeaderText="Medicine ID" Visible="false" ItemStyle-HorizontalAlign="Left">
                            <ItemTemplate>
                                <asp:Label ID="MedicineID" runat="server" Text='<%# Bind("MedicineID") %>'></asp:Label>
                                <asp:Label ID="Icode" runat="server" Text='<%# Bind("ICODE") %>' Visible="false"></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Medicine Name"  ItemStyle-Width="15%" ItemStyle-HorizontalAlign="Left">
                            <ItemTemplate>
                                <asp:Label ID="MedicineName" runat="server" Text='<%# Bind("MedicineName") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Generic Name"  ItemStyle-Width="15%" ItemStyle-HorizontalAlign="Left">
                            <ItemTemplate>
                                <asp:Label ID="lblGenericName" runat="server" Text='<%# Bind("GenericName") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Mfg Code" Visible="false" ItemStyle-HorizontalAlign="Left">
                            <ItemTemplate>
                                <asp:Label ID="MCode" runat="server" Text='<%# Bind("MCode") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                         <asp:TemplateField HeaderText="Manufacturing Company"   ItemStyle-Width="15%" ItemStyle-HorizontalAlign="Left">
                            <ItemTemplate>
                                <asp:Label ID="MName" runat="server" Text='<%# Bind("MName") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField> 
                                
                         <asp:TemplateField HeaderText="MedicineGroup ID" Visible="false" ItemStyle-HorizontalAlign="Left">
                            <ItemTemplate>
                                <asp:Label ID="MedicineGroupID" runat="server" Text='<%# Bind("MedicineGroupID") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>

                         <asp:TemplateField HeaderText="MedicineGroup Name"   ItemStyle-Width="10%" ItemStyle-HorizontalAlign="Left">
                            <ItemTemplate>
                                <asp:Label ID="MedicineGroupName" runat="server" Text='<%# Bind("MedicineGroupName") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>    

                        <asp:TemplateField HeaderText="Unit ID" Visible="false" ItemStyle-HorizontalAlign="Left">
                            <ItemTemplate>
                                <asp:Label ID="UnitID" runat="server" Text='<%# Bind("UnitID") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>

                         <asp:TemplateField HeaderText="Purchase Unit"   ItemStyle-Width="5%" ItemStyle-HorizontalAlign="Left">
                            <ItemTemplate>
                                <asp:Label ID="UnitName" runat="server" Text='<%# Bind("UnitName") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField> 

                        <asp:TemplateField HeaderText="Selling Unit Id" Visible="false">
                            <ItemTemplate>
                                <asp:Label ID="lblSellingUnit" runat="server" Text='<%# Bind("SellingUnit") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField> 

                        <asp:TemplateField HeaderText="Sub" Visible ="false">
                            <ItemTemplate>
                                <asp:Label ID="lblsub" runat="server" Text='<%# Bind("SubGroupid") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField> 
                        
                        <%--<asp:TemplateField HeaderText="Sub Group Name"    ItemStyle-Width="10%" ItemStyle-HorizontalAlign="Left">
                            <ItemTemplate>
                                <asp:Label ID="lblsubname" runat="server" Text='<%# Bind("SubGrName") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField> --%>

                        
                        <asp:TemplateField HeaderText="Conversion Factor"   ItemStyle-Width="5%" ItemStyle-HorizontalAlign="Right">
                            <ItemTemplate>
                                <asp:Label ID="lblConversionFactor" runat="server" Text='<%# Bind("ConversionFactor") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField> 
                        <asp:TemplateField HeaderText="HSN Code"   ItemStyle-Width="5%" ItemStyle-HorizontalAlign="Right">
                            <ItemTemplate>
                                <asp:Label ID="lblHsnCode" runat="server" Text='<%# Bind("HSNCode") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="IGST Rate"   ItemStyle-Width="5%" ItemStyle-HorizontalAlign="Right">
                            <ItemTemplate>
                                <asp:Label ID="lblIgstrate" runat="server" Text='<%# Bind("IGSTRate") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="CGST Rate"   ItemStyle-Width="5%" ItemStyle-HorizontalAlign="Right">
                            <ItemTemplate>
                                <asp:Label ID="lblCgstrate" runat="server" Text='<%# Bind("CGSTRate") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="SGST Rate"   ItemStyle-Width="5%" ItemStyle-HorizontalAlign="Right">
                            <ItemTemplate>
                                <asp:Label ID="lblSgstrate" runat="server" Text='<%# Bind("SGSTRate") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>

                      <%--     <asp:TemplateField HeaderText="Dose"   Visible="false"  ItemStyle-Width="65px">
                            <ItemTemplate>
                                <asp:Label ID="lblDose" runat="server" Text='<%# Bind("Dose") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField> --%>

                        <asp:TemplateField HeaderText="Alert Quantity"    ItemStyle-Width="5%" ItemStyle-HorizontalAlign="Right">
                            <ItemTemplate>
                                <asp:Label ID="lblPurGst" runat="server" Text='<%# Bind("ApplPurWithoutGst") %>' Visible="false"></asp:Label>
                                <asp:Label ID="lblitype" runat="server" Text='<%# Bind("itype") %>' Visible="false"></asp:Label>
                                <asp:Label ID="lblStockAlert" runat="server" Text='<%# Bind("StockAlert") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField> 
                                           
                        <asp:CommandField SelectText="Edit" ShowSelectButton="True"     ItemStyle-Width="10%" ControlStyle-CssClass="temp" HeaderText="Edit" SelectImageUrl="~/Images/edit.png" ButtonType="Image">
                            <ControlStyle CssClass="temp"></ControlStyle>
                        </asp:CommandField>
                        <asp:CommandField ShowDeleteButton="True" ControlStyle-CssClass="temp"   ItemStyle-Width="10%" HeaderText="Delete" DeleteImageUrl="~/Images/delete.png" ButtonType="Image">
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
