<%@ Page Title="" Language="C#" MasterPageFile="~/MasterAll/MasterPageAll.master" AutoEventWireup="true" CodeFile="MedicineSaleReturn.aspx.cs" Inherits="Medicine_MedicineSaleReturn" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
 

    <script type="text/javascript" language="javascript">
    function ShowDialog() {
        var rtvalue = window.open("MedicineReturnPopup.aspx", "sss", "Width:800px; Height:550px; dialogLeft:250px;");
        document.getElementById("ctl00_ContentPlaceHolder1_txtPurchaseMedicineId").value = rtvalue;

    }

    function printDiv(divID) {
        var divElements = document.getElementById(divID).innerHTML;
        var oldPage = document.body.innerHTML;
        document.body.innerHTML = "<html><head><title></title></head><body>" + divElements + "</body>";
        window.print();
        document.body.innerHTML = oldPage;
    }


    function Calling() {
        var date = new Date();
        $('.DatepickerCall1').datepicker({ dateFormat: 'dd/mm/yy' });

        var date = new Date();
        $('.DatepickerCall').datepicker({ dateFormat: 'dd/mm/yy' });

        $("input[id$='Button1']").click(function () {

            if ($("select[id$='DropDownList1']").val() == '0') {
                alert('Please Select Supplier Name!');
                $("select[id$='DropDownList1']").addClass('textboxerr');
                $("select[id$='DropDownList1']").focus();
                return false;
            }
            else {
                $("select[id$='DropDownList1']").removeClass('textboxerr');
            }

            if ($("input[id$='txtPurchaseMedicineId']").val() == '') {
                alert('Please Enter Medicine ID!');
                $("input[id$='txtPurchaseMedicineId']").focus();
                $("input[id$='txtPurchaseMedicineId']").addClass('textboxerr');
                return false;
            }
            else {
                $("input[id$='txtPurchaseMedicineId']").removeClass('textboxerr');
            }

            if ($("input[id$='txtPurchaseMedicineName']").val() == '') {
                alert('Please Enter Medicine Name!');
                $("input[id$='txtPurchaseMedicineName']").focus();
                $("input[id$='txtPurchaseMedicineName']").addClass('textboxerr');
                return false;
            }
            else {
                $("input[id$='txtPurchaseMedicineName']").removeClass('textboxerr');
            }

            if ($("input[id$='Calendar1']").val() == '') {
                alert('Please Enter purchase Date!');
                $("input[id$='Calendar1']").focus();
                $("input[id$='Calendar1']").addClass('textboxerr');
                return false;
            }
            else {
                $("input[id$='Calendar1']").removeClass('textboxerr');
            }

            if ($("input[id$='txtBillNo']").val() == '') {
                alert('Please Enter Bill No!');
                $("input[id$='txtBillNo']").focus();
                $("input[id$='txtBillNo']").addClass('textboxerr');
                return false;
            }
            else {
                $("input[id$='txtBillNo']").removeClass('textboxerr');
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
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
    <ContentTemplate>
      <div id="divContent" runat="server">
        <div class="pageheader">
            <asp:Label ID="Label1" runat="server">Medicine Return</asp:Label>
        </div>
        <div class="formbox">
            <div class="form-sec">
                <div class="error">
                    <strong>
                        <asp:Label ID="lblError" runat="server" Font-Bold="True" CssClass="fadeout"></asp:Label>
                    </strong>
                    <div class="clear">
                    </div>
                </div>
                <asp:HiddenField ID="hdnMode" runat="server" Value="0" />   <asp:HiddenField ID="HiddenField1" runat="server"/>

                     <div class="form-sec-row">
                    <label><strong>
                     Return Bill No :</strong></label>
                         <asp:TextBox ID="TextBox5" runat="server" Enabled="false"  CssClass="textbox-medium1"></asp:TextBox>
                   <div class="clear">
                    </div>
                </div>

                <div class="form-sec-row">
                    <label><strong>
                        Patient's Name :</strong></label>
                    <asp:TextBox ID="TextBox1" runat="server" 
                        CssClass="textbox-medium1"></asp:TextBox>
                   <%-- <asp:Button ID="Button3" runat="server" CssClass="submit-search" Text="SEARCH"  Width="74px" OnClientClick="ShowDialog()"/>--%>
                    <div class="clear">
                    </div>
                </div>
    

          

                    <div class="form-sec-row">
                    <label><strong> 
                        Patient's Address :</strong></label>
                    <asp:TextBox ID="TextBox2" runat="server" CssClass="textbox-medium1">
                     </asp:TextBox>
                    <div class="clear">
                    </div>
                </div>

                <div class="form-sec-row">
                    <label><strong>
                      Doctor Name :</strong></label>
                             <asp:TextBox ID="TextBox3" runat="server" CssClass="textbox-medium1"></asp:TextBox>
                     
                   <div class="clear">
                    </div>
                </div>

                     <div class="form-sec-row">
                    <label><strong>
                     Card No :</strong></label>
                         <asp:TextBox ID="TextBox4" runat="server" CssClass="textbox-medium1"></asp:TextBox>
                   <div class="clear">
                    </div>
                </div>

                

                    <div class="form-sec-row">
                    <label><strong>
                     Return Bill Date :</strong></label>
                         <asp:TextBox ID="TextBox6" runat="server" CssClass="DatepickerCall"></asp:TextBox>
                               <asp:Label ID="Label3" runat="server" ForeColor="Green" Text="(DD/MM/YYYY)"></asp:Label>
                   <div class="clear">
                    </div>
                </div>
                <div style="width:950px;overflow:auto;">
                <table style="text-align: left;">                   
                    <tr  style='background-color:#FF9300;'>
                        <td align="center">
                            <asp:Label ID="lblMfg" runat="server" Font-Bold="True" Text="Manufacture "
                                Width="160px"></asp:Label>
                        </td>

                        <td  align="center">
                            <asp:Label ID="lblMediGrp" runat="server" Font-Bold="True" Text="Medicine Group" Width="124px"></asp:Label>
                        </td>

                             <td  align="center">
                            <asp:Label ID="lblMediSubGrp" runat="server" Font-Bold="True" Text="Med. Sub Group" Width="124px"></asp:Label>
                        </td>
                        <td  align="center">
                            <asp:Label ID="lblMedi" runat="server" Font-Bold="True" Text="Medicine" Width="124px"></asp:Label>
                        </td>
                        <td  align="center">
                            <asp:Label ID="lblBatch" runat="server" Font-Bold="True" Text="Batch" Width="124px"></asp:Label>
                        </td>
                        <td  align="center">
                            <asp:Label ID="lblExpiry" runat="server" Font-Bold="True" Text="Expiry" Width="124px"></asp:Label>
                        </td>
                        <td  align="center">
                            <asp:Label ID="lblQty" runat="server" Font-Bold="True" Text="Quantity" Width="124px"></asp:Label>
                        </td>
                        <td  align="center">
                            <asp:Label ID="lblUnitPrice" runat="server" Font-Bold="True" Text="Unit Price" Width="124px"></asp:Label>
                        </td>
                        <td  align="center">
                            <asp:Label ID="lblTotalPrice" runat="server" Font-Bold="True" 
                                Text="Total Price" Width="153px"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td class="style1">
                            <asp:DropDownList ID="ddlMfg1" runat="server" Width="154px">
                            </asp:DropDownList>
                        </td>
                        <td class="style1">
                            <asp:DropDownList ID="ddlMediGrp1" runat="server" Width="154px" 
                               AutoPostBack="true"  
                                onselectedindexchanged="ddlMediGrp1_SelectedIndexChanged" Height="16px">
                            </asp:DropDownList>
                        </td>
                           <td class="style1">
                            <asp:DropDownList ID="ddlMediSubGrp1" runat="server" Width="154px" 
                                AutoPostBack="True" 
                                   onselectedindexchanged="ddlMediSubGrp1_SelectedIndexChanged"  >
                            </asp:DropDownList>
                        </td>

                        <td class="style1">
                            <asp:DropDownList ID="ddlMedi1" runat="server" Width="154px" 
                                AutoPostBack="True" onselectedindexchanged="ddlMedi1_SelectedIndexChanged">
                            </asp:DropDownList>
                        </td>
                        <td class="style1">
                            <asp:TextBox ID="txtBatch1" runat="server" Width="154px" ></asp:TextBox>
                        </td>
                        <td class="style1">
                            <asp:TextBox ID="Calendar2" runat="server" Width="154px" >
                            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                            </asp:TextBox>
                        </td>
                        <td class="style1"> 
                            <asp:TextBox ID="txtQty1" runat="server" Width="154px" AutoPostBack="True" 
                                ontextchanged="txtQty1_TextChanged" ></asp:TextBox>
                        </td>
                        <td class="style1">
                            <asp:TextBox ID="txtUnitPrice1" runat="server" Width="154px" ></asp:TextBox>
                        </td>
                        <td class="style1">
                            <asp:TextBox ID="txtTotalPrice1" runat="server" Width="154px" ></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td class="style1">
                            <asp:DropDownList ID="ddlMfg2" runat="server" Width="154px">
                            </asp:DropDownList>
                        </td>
                        <td class="style1">
                            <asp:DropDownList ID="ddlMediGrp2" runat="server" Width="154px" 
                               AutoPostBack="true"  onselectedindexchanged="ddlMediGrp2_SelectedIndexChanged">
                            </asp:DropDownList>
                        </td>

                            <td class="style1">
                            <asp:DropDownList ID="ddlMediSubGrp2" runat="server" Width="154px" 
                                AutoPostBack="True" 
                                    onselectedindexchanged="ddlMediSubGrp2_SelectedIndexChanged"  >
                            </asp:DropDownList>
                        </td>
                        <td class="style1">
                            <asp:DropDownList ID="ddlMedi2" runat="server" Width="154px" 
                                AutoPostBack="True" onselectedindexchanged="ddlMedi2_SelectedIndexChanged">
                            </asp:DropDownList>
                        </td>
                        <td class="style1">
                            <asp:TextBox ID="txtBatch2" runat="server" Width="154px" ></asp:TextBox>
                        </td>
                        <td class="style1">
                            <asp:TextBox ID="Calendar3" runat="server" Width="154px" >
                            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                            </asp:TextBox>
                        </td>
                        <td class="style1"> 
                            <asp:TextBox ID="txtQty2" runat="server" Width="154px" AutoPostBack="True" 
                                ontextchanged="txtQty2_TextChanged" ></asp:TextBox>
                        </td>
                        <td class="style1">
                            <asp:TextBox ID="txtUnitPrice2" runat="server" Width="154px" ></asp:TextBox>
                        </td>
                        <td class="style1">
                            <asp:TextBox ID="txtTotalPrice2" runat="server" Width="154px" ></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                         <td class="style1">
                            <asp:DropDownList ID="ddlMfg3" runat="server" Width="154px">
                            </asp:DropDownList>
                        </td>
                        <td class="style1">
                            <asp:DropDownList ID="ddlMediGrp3" runat="server" Width="154px" 
                               AutoPostBack="true"  onselectedindexchanged="ddlMediGrp3_SelectedIndexChanged">
                            </asp:DropDownList>
                        </td>
                            <td class="style1">
                            <asp:DropDownList ID="ddlMediSubGrp3" runat="server" Width="154px" 
                                AutoPostBack="True" 
                                    onselectedindexchanged="ddlMediSubGrp3_SelectedIndexChanged"  >
                            </asp:DropDownList>
                        </td>

                        <td class="style1">
                            <asp:DropDownList ID="ddlMedi3" runat="server" Width="154px" 
                                AutoPostBack="True" onselectedindexchanged="ddlMedi3_SelectedIndexChanged">
                            </asp:DropDownList>
                        </td>
                        <td class="style1">
                            <asp:TextBox ID="txtBatch3" runat="server" Width="154px" ></asp:TextBox>
                        </td>
                        <td class="style1">
                            <asp:TextBox ID="Calendar4" runat="server" Width="154px" >
                            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                            </asp:TextBox>
                        </td>
                        <td class="style1"> 
                            <asp:TextBox ID="txtQty3" runat="server" Width="154px" AutoPostBack="True" 
                                ontextchanged="txtQty3_TextChanged" ></asp:TextBox>
                        </td>
                        <td class="style1">
                            <asp:TextBox ID="txtUnitPrice3" runat="server" Width="154px" ></asp:TextBox>
                        </td>
                        <td class="style1">
                            <asp:TextBox ID="txtTotalPrice3" runat="server" Width="154px" ></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                       <td class="style1">
                            <asp:DropDownList ID="ddlMfg4" runat="server" Width="154px">
                            </asp:DropDownList>
                        </td>
                        <td class="style1">
                            <asp:DropDownList ID="ddlMediGrp4" runat="server" Width="154px" 
                               AutoPostBack="true"  onselectedindexchanged="ddlMediGrp4_SelectedIndexChanged">
                            </asp:DropDownList>
                        </td>

                            <td class="style1">
                            <asp:DropDownList ID="ddlMediSubGrp4" runat="server" Width="154px" 
                                AutoPostBack="True" 
                                    onselectedindexchanged="ddlMediSubGrp4_SelectedIndexChanged"  >
                            </asp:DropDownList>
                        </td>

                        <td class="style1">
                            <asp:DropDownList ID="ddlMedi4" runat="server" Width="154px" 
                                AutoPostBack="True" onselectedindexchanged="ddlMedi4_SelectedIndexChanged">
                            </asp:DropDownList>
                        </td>
                        <td class="style1">
                            <asp:TextBox ID="txtBatch4" runat="server" Width="154px" ></asp:TextBox>
                        </td>
                        <td class="style1">
                            <asp:TextBox ID="Calendar5" runat="server" Width="154px" >
                            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                            </asp:TextBox>
                        </td>
                        <td class="style1"> 
                            <asp:TextBox ID="txtQty4" runat="server" Width="154px" AutoPostBack="True" 
                                ontextchanged="txtQty4_TextChanged" ></asp:TextBox>
                        </td>
                        <td class="style1">
                            <asp:TextBox ID="txtUnitPrice4" runat="server" Width="154px" ></asp:TextBox>
                        </td>
                        <td class="style1">
                            <asp:TextBox ID="txtTotalPrice4" runat="server" Width="154px" ></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                         <td class="style1">
                            <asp:DropDownList ID="ddlMfg5" runat="server" Width="154px">
                            </asp:DropDownList>
                        </td>
                        <td class="style1">
                            <asp:DropDownList ID="ddlMediGrp5" runat="server" Width="154px" 
                               AutoPostBack="true"  onselectedindexchanged="ddlMediGrp5_SelectedIndexChanged">
                            </asp:DropDownList>
                        </td>

                            <td class="style1">
                            <asp:DropDownList ID="ddlMediSubGrp5" runat="server" Width="154px" 
                                AutoPostBack="True" 
                                    onselectedindexchanged="ddlMediSubGrp5_SelectedIndexChanged"  >
                            </asp:DropDownList>
                        </td>

                        <td class="style1">
                            <asp:DropDownList ID="ddlMedi5" runat="server" Width="154px" 
                                AutoPostBack="True" onselectedindexchanged="ddlMedi5_SelectedIndexChanged">
                            </asp:DropDownList>
                        </td>
                        <td class="style1">
                            <asp:TextBox ID="txtBatch5" runat="server" Width="154px" ></asp:TextBox>
                        </td>
                        <td class="style1">
                            <asp:TextBox ID="Calendar6" runat="server" Width="154px" >
                            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                            </asp:TextBox>
                        </td>
                        <td class="style1"> 
                            <asp:TextBox ID="txtQty5" runat="server" Width="154px" AutoPostBack="True" 
                                ontextchanged="txtQty5_TextChanged" ></asp:TextBox>
                        </td>
                        <td class="style1">
                            <asp:TextBox ID="txtUnitPrice5" runat="server" Width="154px" ></asp:TextBox>
                        </td>
                        <td class="style1">
                            <asp:TextBox ID="txtTotalPrice5" runat="server" Width="154px" ></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                          <td class="style1">
                            <asp:DropDownList ID="ddlMfg6" runat="server" Width="154px">
                            </asp:DropDownList>
                        </td>
                        <td class="style1">
                            <asp:DropDownList ID="ddlMediGrp6" runat="server" Width="154px" 
                               AutoPostBack="true"  onselectedindexchanged="ddlMediGrp6_SelectedIndexChanged">
                            </asp:DropDownList>
                        </td>

                            <td class="style1">
                            <asp:DropDownList ID="ddlMediSubGrp6" runat="server" Width="154px" 
                                AutoPostBack="True" 
                                    onselectedindexchanged="ddlMediSubGrp6_SelectedIndexChanged"  >
                            </asp:DropDownList>
                        </td>

                        <td class="style1">
                            <asp:DropDownList ID="ddlMedi6" runat="server" Width="154px" 
                                AutoPostBack="True" onselectedindexchanged="ddlMedi6_SelectedIndexChanged">
                            </asp:DropDownList>
                        </td>
                        <td class="style1">
                            <asp:TextBox ID="txtBatch6" runat="server" Width="154px" ></asp:TextBox>
                        </td>
                        <td class="style1">
                            <asp:TextBox ID="Calendar7" runat="server" Width="154px" >
                            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                            </asp:TextBox>
                        </td>
                        <td class="style1"> 
                            <asp:TextBox ID="txtQty6" runat="server" Width="154px" AutoPostBack="True" 
                                ontextchanged="txtQty6_TextChanged" ></asp:TextBox>
                        </td>
                        <td class="style1">
                            <asp:TextBox ID="txtUnitPrice6" runat="server" Width="154px" ></asp:TextBox>
                        </td>
                        <td class="style1">
                            <asp:TextBox ID="txtTotalPrice6" runat="server" Width="154px" ></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                         <td class="style1">
                            <asp:DropDownList ID="ddlMfg7" runat="server" Width="154px">
                            </asp:DropDownList>
                        </td>
                        <td class="style1">
                            <asp:DropDownList ID="ddlMediGrp7" runat="server" Width="154px" 
                               AutoPostBack="true"  onselectedindexchanged="ddlMediGrp7_SelectedIndexChanged">
                            </asp:DropDownList>
                        </td>

                            <td class="style1">
                            <asp:DropDownList ID="ddlMediSubGrp7" runat="server" Width="154px" 
                                AutoPostBack="True" 
                                    onselectedindexchanged="ddlMediSubGrp7_SelectedIndexChanged"  >
                            </asp:DropDownList>
                        </td>

                        <td class="style1">
                            <asp:DropDownList ID="ddlMedi7" runat="server" Width="154px" 
                                AutoPostBack="True" onselectedindexchanged="ddlMedi7_SelectedIndexChanged">
                            </asp:DropDownList>
                        </td>
                        <td class="style1">
                            <asp:TextBox ID="txtBatch7" runat="server" Width="154px" ></asp:TextBox>
                        </td>
                        <td class="style1">
                            <asp:TextBox ID="Calendar8" runat="server" Width="154px" >
                            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                            </asp:TextBox>
                        </td>
                        <td class="style1"> 
                            <asp:TextBox ID="txtQty7" runat="server" Width="154px" AutoPostBack="True" 
                                ontextchanged="txtQty7_TextChanged" ></asp:TextBox>
                        </td>
                        <td class="style1">
                            <asp:TextBox ID="txtUnitPrice7" runat="server" Width="154px" ></asp:TextBox>
                        </td>
                        <td class="style1">
                            <asp:TextBox ID="txtTotalPrice7" runat="server" Width="154px" ></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                         <td class="style1">
                            <asp:DropDownList ID="ddlMfg8" runat="server" Width="154px">
                            </asp:DropDownList>
                        </td>
                        <td class="style1">
                            <asp:DropDownList ID="ddlMediGrp8" runat="server" Width="154px" 
                               AutoPostBack="true"  onselectedindexchanged="ddlMediGrp8_SelectedIndexChanged">
                            </asp:DropDownList>
                        </td>

                            <td class="style1">
                            <asp:DropDownList ID="ddlMediSubGrp8" runat="server" Width="154px" 
                                AutoPostBack="True" 
                                    onselectedindexchanged="ddlMediSubGrp8_SelectedIndexChanged"  >
                            </asp:DropDownList>
                        </td>

                        <td class="style1">
                            <asp:DropDownList ID="ddlMedi8" runat="server" Width="154px" 
                                AutoPostBack="True" onselectedindexchanged="ddlMedi8_SelectedIndexChanged">
                            </asp:DropDownList>
                        </td>
                        <td class="style1">
                            <asp:TextBox ID="txtBatch8" runat="server" Width="154px" ></asp:TextBox>
                        </td>
                        <td class="style1">
                            <asp:TextBox ID="Calendar9" runat="server" Width="154px" >
                            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                            </asp:TextBox>
                        </td>
                        <td class="style1"> 
                            <asp:TextBox ID="txtQty8" runat="server" Width="154px" AutoPostBack="True" 
                                ontextchanged="txtQty8_TextChanged" ></asp:TextBox>
                        </td>
                        <td class="style1">
                            <asp:TextBox ID="txtUnitPrice8" runat="server" Width="154px" ></asp:TextBox>
                        </td>
                        <td class="style1">
                            <asp:TextBox ID="txtTotalPrice8" runat="server" Width="154px" ></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                         <td class="style1">
                            <asp:DropDownList ID="ddlMfg9" runat="server" Width="154px">
                            </asp:DropDownList>
                        </td>
                        <td class="style1">
                            <asp:DropDownList ID="ddlMediGrp9" runat="server" Width="154px" 
                               AutoPostBack="true"  onselectedindexchanged="ddlMediGrp9_SelectedIndexChanged">
                            </asp:DropDownList>
                        </td>

                            <td class="style1">
                            <asp:DropDownList ID="ddlMediSubGrp9" runat="server" Width="154px" 
                                AutoPostBack="True" 
                                    onselectedindexchanged="ddlMediSubGrp9_SelectedIndexChanged"  >
                            </asp:DropDownList>
                        </td>

                        <td class="style1">
                            <asp:DropDownList ID="ddlMedi9" runat="server" Width="154px" 
                                AutoPostBack="True" onselectedindexchanged="ddlMedi9_SelectedIndexChanged">
                            </asp:DropDownList>
                        </td>
                        <td class="style1">
                            <asp:TextBox ID="txtBatch9" runat="server" Width="154px" ></asp:TextBox>
                        </td>
                        <td class="style1">
                            <asp:TextBox ID="Calendar10" runat="server" Width="154px" >
                            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                            </asp:TextBox>
                        </td>
                        <td class="style1"> 
                            <asp:TextBox ID="txtQty9" runat="server" Width="154px" AutoPostBack="True" 
                                ontextchanged="txtQty9_TextChanged" ></asp:TextBox>
                        </td>
                        <td class="style1">
                            <asp:TextBox ID="txtUnitPrice9" runat="server" Width="154px" ></asp:TextBox>
                        </td>
                        <td class="style1">
                            <asp:TextBox ID="txtTotalPrice9" runat="server" Width="154px" ></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td class="style1">
                            <asp:DropDownList ID="ddlMfg10" runat="server" Width="154px">
                            </asp:DropDownList>
                        </td>
                        <td class="style1">
                            <asp:DropDownList ID="ddlMediGrp10" runat="server" Width="154px" 
                               AutoPostBack="true"  onselectedindexchanged="ddlMediGrp10_SelectedIndexChanged">
                            </asp:DropDownList>
                        </td>

                            <td class="style1">
                            <asp:DropDownList ID="ddlMediSubGrp10" runat="server" Width="154px" 
                                AutoPostBack="True" 
                                    onselectedindexchanged="ddlMediSubGrp10_SelectedIndexChanged"  >
                            </asp:DropDownList>
                        </td>

                        <td class="style1">
                            <asp:DropDownList ID="ddlMedi10" runat="server" Width="154px" 
                                AutoPostBack="True" onselectedindexchanged="ddlMedi10_SelectedIndexChanged">
                            </asp:DropDownList>
                        </td>
                        <td class="style1">
                            <asp:TextBox ID="txtBatch10" runat="server" Width="154px" ></asp:TextBox>
                        </td>
                        <td class="style1">
                            <asp:TextBox ID="Calendar11" runat="server" Width="154px" >
                            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                            </asp:TextBox>
                        </td>
                        <td class="style1"> 
                            <asp:TextBox ID="txtQty10" runat="server" Width="154px" AutoPostBack="True" 
                                ontextchanged="txtQty10_TextChanged" ></asp:TextBox>
                        </td>
                        <td class="style1">
                            <asp:TextBox ID="txtUnitPrice10" runat="server" Width="154px" ></asp:TextBox>
                        </td>
                        <td class="style1">
                            <asp:TextBox ID="txtTotalPrice10" runat="server" Width="154px"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                         <td class="style1">
                            <asp:DropDownList ID="ddlMfg11" runat="server" Width="154px">
                            </asp:DropDownList>
                        </td>
                        <td class="style1">
                            <asp:DropDownList ID="ddlMediGrp11" runat="server" Width="154px" 
                               AutoPostBack="true"  onselectedindexchanged="ddlMediGrp11_SelectedIndexChanged">
                            </asp:DropDownList>
                        </td>

                            <td class="style1">
                            <asp:DropDownList ID="ddlMediSubGrp11" runat="server" Width="154px" 
                                AutoPostBack="True" 
                                    onselectedindexchanged="ddlMediSubGrp11_SelectedIndexChanged"  >
                            </asp:DropDownList>
                        </td>

                        <td class="style1">
                            <asp:DropDownList ID="ddlMedi11" runat="server" Width="154px" 
                                AutoPostBack="True" onselectedindexchanged="ddlMedi11_SelectedIndexChanged">
                            </asp:DropDownList>
                        </td>
                        <td class="style1">
                            <asp:TextBox ID="txtBatch11" runat="server" Width="154px" ></asp:TextBox>
                        </td>
                        <td class="style1">
                            <asp:TextBox ID="Calendar12" runat="server" Width="154px" >
                            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                            </asp:TextBox>
                        </td>
                        <td class="style1"> 
                            <asp:TextBox ID="txtQty11" runat="server" Width="154px" AutoPostBack="True" 
                                ontextchanged="txtQty11_TextChanged" ></asp:TextBox>
                        </td>
                        <td class="style1">
                            <asp:TextBox ID="txtUnitPrice11" runat="server" Width="154px" ></asp:TextBox>
                        </td>
                        <td class="style1">
                            <asp:TextBox ID="txtTotalPrice11" runat="server" Width="154px" ></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                         <td class="style1">
                            <asp:DropDownList ID="ddlMfg12" runat="server" Width="154px">
                            </asp:DropDownList>
                        </td>
                        <td class="style1">
                            <asp:DropDownList ID="ddlMediGrp12" runat="server" Width="154px" 
                               AutoPostBack="true"  onselectedindexchanged="ddlMediGrp12_SelectedIndexChanged">
                            </asp:DropDownList>
                        </td>

                            <td class="style1">
                            <asp:DropDownList ID="ddlMediSubGrp12" runat="server" Width="154px" 
                                AutoPostBack="True" 
                                    onselectedindexchanged="ddlMediSubGrp12_SelectedIndexChanged"  >
                            </asp:DropDownList>
                        </td>


                        <td class="style1">
                            <asp:DropDownList ID="ddlMedi12" runat="server" Width="154px" 
                                AutoPostBack="True" onselectedindexchanged="ddlMedi12_SelectedIndexChanged">
                            </asp:DropDownList>
                        </td>
                        <td class="style1">
                            <asp:TextBox ID="txtBatch12" runat="server" Width="154px" ></asp:TextBox>
                        </td>
                        <td class="style1">
                            <asp:TextBox ID="Calendar13" runat="server" Width="154px" >
                            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                            </asp:TextBox>
                        </td>
                        <td class="style1"> 
                            <asp:TextBox ID="txtQty12" runat="server" Width="154px" AutoPostBack="True" 
                                ontextchanged="txtQty12_TextChanged" ></asp:TextBox>
                        </td>
                        <td class="style1">
                            <asp:TextBox ID="txtUnitPrice12" runat="server" Width="154px" ></asp:TextBox>
                        </td>
                        <td class="style1">
                            <asp:TextBox ID="txtTotalPrice12" runat="server" Width="154px" ></asp:TextBox>
                        </td>
                    </tr>
                </table>
                </div>
                  
                            <div class="form-sec-row">
                    <label><strong>
                      Discount :</strong></label>
                    <asp:TextBox ID="TextBox7" runat="server" CssClass="textbox-medium1"></asp:TextBox>
                    <div class="clear">
                    </div>
                </div>

                          <div class="form-sec-row">
                    <label><strong>
                      Vat (%) :</strong></label>
                    <asp:TextBox ID="TextBox8" runat="server" CssClass="textbox-medium1"></asp:TextBox>
                    <div class="clear">
                    </div>
                </div>

                     <%--     <div class="form-sec-row">
                    <label><strong>
                      Net Amount :</strong></label>
                    <asp:TextBox ID="TextBox3" runat="server" CssClass="textbox-medium1"></asp:TextBox>
                    <div class="clear">
                    </div>
                </div>--%>
                  <div class="form-sec-row">
                    &nbsp;&nbsp;&nbsp;<label><strong>&nbsp;</strong></label>
                    <asp:Button ID="Button1" runat="server" CssClass="submit-button" Text="Submit"  Height="28px"
                          onclick="Button1_Click"  />
                    <asp:Button ID="Button2" runat="server" CssClass="submit-button" Text="Cancel"  Height="28px"
                          onclick="Button2_Click"  />
                    <div class="clear">
                    </div>
                               <asp:HiddenField ID="hdnRowId1" runat="server" Value="0" />     
                               <asp:HiddenField ID="hdnRowId2" runat="server" Value="0" />  
                               <asp:HiddenField ID="hdnRowId3" runat="server" Value="0" />  
                               <asp:HiddenField ID="hdnRowId4" runat="server" Value="0" />  
                               <asp:HiddenField ID="hdnRowId5" runat="server" Value="0" />  
                               <asp:HiddenField ID="hdnRowId6" runat="server" Value="0" />  
                               <asp:HiddenField ID="hdnRowId7" runat="server" Value="0" />  
                               <asp:HiddenField ID="hdnRowId8" runat="server" Value="0" />  
                               <asp:HiddenField ID="hdnRowId9" runat="server" Value="0" />  
                               <asp:HiddenField ID="hdnRowId10" runat="server" Value="0" />  
                               <asp:HiddenField ID="hdnRowId11" runat="server" Value="0" />  
                               <asp:HiddenField ID="hdnRowId12" runat="server" Value="0" />  

                </div>
                <div class="clear">
                </div>
                <center>
                          <table width="100%">
                    <tr>        
                        <td>    <div id='mydiv'>              
                            <asp:Literal ID="ltrReport" runat="server"></asp:Literal></div>                
                        </td>
                    </tr>
                    <tr>
                        <td align="center">
                     
                         <input type="button" value="Back" style="width:70px; font-size:x-small" onclick="window.history.back()" />
                         <input type="button" id="cmdPrint" value="Print" style="width:70px; font-size:x-small" onclick="javascript:printDiv('mydiv')"/></td>
                    </tr>
            </table>
            </center>
            </div>
        </div>
        </div>
     </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>

