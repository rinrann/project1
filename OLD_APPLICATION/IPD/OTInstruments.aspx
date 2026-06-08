<%@ Page Language="C#" MasterPageFile="~/MasterAll/MasterPageAll.master" AutoEventWireup="true" CodeFile="OTInstruments.aspx.cs" Inherits="Master_OTInstruments" %>
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
        var date = new Date();
        $("input[id$='Calendar2']").datepicker({ dateFormat: 'dd/mm/yy', minDate: date });
        var date = new Date();
        $("input[id$='Calendar1']").datepicker({ dateFormat: 'dd/mm/yy', minDate: date });

        var date = new Date();
        $("input[id$='Calendar3']").datepicker({ dateFormat: 'dd/mm/yy', minDate: date });


        $("input[id$='Button1']").click(function () {


            if ($("input[id$='txtMfgCom']").val() == '') {
                alert('Please Enter Company name Properly!');
                $("input[id$='txtMfgCom']").focus();
                $("input[id$='txtMfgCom']").addClass('textboxerr');
                return false;
            }
            else {
                $("input[id$='txtMfgCom']").removeClass('textboxerr');
            }


            if ($("select[id$='DropDownList1']").val() == '0') {
                alert('Please Select Companu Name !');
                $("select[id$='DropDownList1']").addClass('textboxerr');
                $("select[id$='DropDownList1']").focus();
                return false;
            }
            else {
                $("select[id$='DropDownList1']").removeClass('textboxerr');
            }

            if ($("input[id$='txtPurPrice']").val() == '') {
                alert('Please Enter Purchase Price!');
                $("input[id$='txtPurPrice']").focus();
                $("input[id$='txtPurPrice']").addClass('textboxerr');
                return false;
            }
            else {
                $("input[id$='txtPurPrice']").removeClass('textboxerr');
            }



            if ($("input[id$='TextBox2']").val() == '') {
                alert('Please Enter Bill No !');
                $("input[id$='TextBox2']").focus();
                $("input[id$='TextBox2']").addClass('textboxerr');
                return false;
            }
            else {
                $("input[id$='TextBox2']").removeClass('textboxerr');
            }





            if ($("input[id$='Calendar1']").val() == '') {
                alert('Please Enter Purchase Date !');
                $("input[id$='Calendar1']").focus();
                $("input[id$='Calendar1']").addClass('textboxerr');
                return false;
            }
            else {
                $("input[id$='Calendar1']").removeClass('textboxerr');
            }



 

  


            if ($("input[id$='TextBox1']").val() == '') {
                alert('Please Enter Quantity !');
                $("input[id$='TextBox1']").focus();
                $("input[id$='TextBox1']").addClass('textboxerr');
                return false;
            }
            else {
                $("input[id$='TextBox1']").removeClass('textboxerr');
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


        $("input[id$='txtAmcPrice']").keydown(function (event) {
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
</script>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">

<%--For Busy Loader .............................--%> 
<%--  <asp:UpdateProgress ID="UpdateProgress1" runat="server">
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
                <asp:Label ID="Label1" runat="server">OT Instruments Purchase</asp:Label>
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
                        Instrument Type :</strong></label>
                        <asp:DropDownList ID="DropDownList2" runat="server" CssClass="textbox-medium1" 
                            AutoPostBack="True" onselectedindexchanged="DropDownList2_SelectedIndexChanged">
                        </asp:DropDownList>
                        <div class="clear">
                        </div>
                    </div>

                    
                    <div class="form-sec-row">
                        <label>
                        <strong>
                        Instrument Name :</strong></label>
                        <asp:DropDownList ID="DropDownList3" runat="server" CssClass="textbox-medium1">
                        </asp:DropDownList>
                        <div class="clear">
                        </div>
                    </div>
                     

                     
                             <div class="form-sec-row">
                        <label>
                        <strong>
                        Model No :</strong></label>
                        <asp:TextBox ID="TextBox3" runat="server" CssClass="textbox-medium1" ></asp:TextBox>
                        <div class="clear">
                        </div>
                    </div>

                     
                             <div class="form-sec-row">
                        <label>
                        <strong>
                        Bill No :</strong></label>
                        <asp:TextBox ID="TextBox2" runat="server" CssClass="textbox-medium1" ></asp:TextBox>
                        <div class="clear">
                        </div>
                    </div>

                    <div class="form-sec-row">
                        <label>
                        <strong>
                        Manufacturing Company :</strong></label>
                        <asp:DropDownList ID="DropDownList1" runat="server" CssClass="textbox-medium1">
                        </asp:DropDownList>
                        <div class="clear">
                        </div>
                    </div>

                    <div class="form-sec-row">
                        <label>
                        <strong>
                        Purchase Price / Unit :</strong></label>
                        <asp:TextBox ID="txtPurPrice" runat="server" CssClass="textbox-medium1" ></asp:TextBox>
                        <div class="clear">
                        </div>
                    </div>


                             <div class="form-sec-row">
                        <label>
                        <strong>
                        Quantity :</strong></label>
                        <asp:TextBox ID="TextBox1" runat="server" CssClass="textbox-medium1" ></asp:TextBox>
                        <div class="clear">
                        </div>
                    </div>

                    <div class="form-sec-row">
                        <label>
                        <strong>
                        AMC Price :</strong></label>
                        <asp:TextBox ID="txtAmcPrice" runat="server" CssClass="textbox-medium1" ></asp:TextBox>
                        <div class="clear">
                        </div>
                    </div>

        

                

                 <div class="form-sec-row">
                <label>
                <strong>
                Purchase Date :</strong></label>
                 <asp:TextBox ID="Calendar1" runat="server" CssClass="textbox-medium1">
               &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                 </asp:TextBox>
                       <cc1:MaskedEditExtender ID="MaskedEditExtender1" runat="server" TargetControlID="Calendar1"
                                            Mask="99/99/9999" MessageValidatorTip="true" MaskType="Date" DisplayMoney="Left"
                                            AcceptNegative="Left" ErrorTooltipEnabled="True" CultureName="en-GB" />
                <div class="clear">
                </div>
            </div>
                    

                 <div class="form-sec-row">
                <label>
                <strong>
                AMC Commencement Date</strong></label>
                 <asp:TextBox ID="Calendar2" runat="server" CssClass="textbox-medium1">
               &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                 </asp:TextBox>
                <div class="clear">
                </div>
               </div>

               <div class="form-sec-row">
                <label>
                <strong>
                AMC Completion Date :</strong></label>
                 <asp:TextBox ID="Calendar3" runat="server" CssClass="textbox-medium1">
               &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                 </asp:TextBox>
                <div class="clear">
                </div>
               </div>
                 
                    <div class="form-sec-row">
                        <label>
                        <strong>
                        Chargable To Patient :</strong></label>
                        <asp:DropDownList ID="DropDownList4" runat="server" CssClass="textbox-medium1">
                            <asp:ListItem Value="0">--Select--</asp:ListItem>
                            <asp:ListItem Value="1">Yes</asp:ListItem>
                            <asp:ListItem Value="2">No</asp:ListItem>
                        </asp:DropDownList>
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
                            Text="Submit" onclick="Button1_Click"    />
                        <asp:Button ID="Button2" runat="server" CssClass="submit-button"    Height="28px"
                            Text="Cancel" onclick="Button2_Click"    />
                        <div class="clear">
                        </div>
                    </div>
                    <div class="clear">
                    </div>
                </div>
                </asp:View>
                   <asp:View ID="View2" runat="server">
            <div class="listing"  style='width:98%; height:300px; overflow:auto;'>
                <asp:GridView id="GridView1"  Width="100%" CssClass="grid" PagerStyle-CssClass="pgr"
                 DataKeyNames ="RowId" runat="server" 
                 AutoGenerateColumns="False" AllowPaging="True"  PageSize="100"
                 OnRowCommand="GridView1_RowCommand" OnRowDataBound="GridView1_RowDataBound" SelectedRowStyle-BackColor="GreenYellow"
                 OnPageIndexChanging="GridView1_PageIndexChanging" 
                    onrowdeleting="GridView1_RowDeleting" > 
                    <Columns>
                        <asp:TemplateField HeaderText="Instrument ID" Visible="false">
                            <ItemTemplate>
                                <asp:Label ID="InstrumentID" runat="server" Text='<%# Bind("RowId") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>

                        
                         <asp:TemplateField HeaderText="Instrument Type Id" Visible="false">
                            <ItemTemplate>
                                <asp:Label ID="InstrumentTypeId" runat="server" Text='<%# Bind("InstrumentType") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>

                         <asp:TemplateField HeaderText="Instrument Type">
                            <ItemTemplate>
                                <asp:Label ID="InstrumentType" runat="server" Text='<%# Bind("TypeName") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>

                        
                             <asp:TemplateField HeaderText="Instrument Name Id" Visible="false">
                            <ItemTemplate>
                                <asp:Label ID="InstrumentNameid" runat="server" Text='<%# Bind("InsNameId") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>

                         <asp:TemplateField HeaderText="Model No">
                            <ItemTemplate>
                                <asp:Label ID="ModelNo" runat="server" Text='<%# Bind("ModelNo") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText=" Instrument Name">
                            <ItemTemplate>
                                <asp:Label ID="InstrumentName" runat="server" Text='<%# Bind("InstrumentName") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                           
                              <asp:TemplateField HeaderText="id" Visible="false">
                            <ItemTemplate>
                                <asp:Label ID="ManufacturingCompanyid" runat="server" Text='<%# Bind("ManufacturingCompany") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField> 


                         <asp:TemplateField HeaderText="Manufacturing Company">
                            <ItemTemplate>
                                <asp:Label ID="ManufacturingCompanyname" runat="server" Text='<%# Bind("MName") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField> 

                         <asp:TemplateField HeaderText="Purchase Price">
                            <ItemTemplate>
                                <asp:Label ID="PurchasePrice" runat="server" Text='<%# Bind("PurchasePrice") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>


                           <asp:TemplateField HeaderText="Bill NO">
                            <ItemTemplate>
                                <asp:Label ID="BillNo" runat="server" Text='<%# Bind("BillNo") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>


                           <asp:TemplateField HeaderText="Quantity">
                            <ItemTemplate>
                                <asp:Label ID="Quantity" runat="server" Text='<%# Bind("Quantity") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>


                         <asp:TemplateField HeaderText="AMC Price">
                            <ItemTemplate>
                                <asp:Label ID="AMCPrice" runat="server" Text='<%# Bind("AMCPrice") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="Current Status">
                            <ItemTemplate>
                                <asp:Label ID="CurrentStatus" runat="server" Text='<%# Bind("CurrentStatus") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>


                        <asp:TemplateField HeaderText="Purchase Date">
                            <ItemTemplate>
                                <asp:Label ID="PurchaseDate" runat="server" Text='<%# Bind("pdate") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>

                        

                        <asp:TemplateField HeaderText="AMC Commencement Date">
                            <ItemTemplate>
                                <asp:Label ID="AMCCommencementDate" runat="server" Text='<%# Bind("comdate") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="AMC Completion Date">
                            <ItemTemplate>
                                <asp:Label ID="AMCCompletionDate" runat="server" Text='<%# Bind("pldate") %>'></asp:Label>
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