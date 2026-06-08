<%@ Page Title="" Language="C#" MasterPageFile="~/MasterAll/MasterPageAll.master" AutoEventWireup="true" CodeFile="MedicineSales.aspx.cs" Inherits="Medicine_MedicineSales" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <script type="text/javascript" language="javascript">
    function ShowDialog() {
        var rtvalue = window.open("MedicineSalePopup.aspx", "sss", "Width:800px; Height:550px; dialogLeft:250px;");
        //document.getElementById("ctl00_ContentPlaceHolder1_TextBox5").value = rtvalue.NameValue;

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
            <asp:Label ID="Label1" runat="server">Medicine Sales/Issue</asp:Label>
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
                     Sales/Issue Bill No :</strong></label>
                         <asp:TextBox ID="TextBox5" runat="server" Enabled="false"  CssClass="textbox-medium1"></asp:TextBox>
                         <asp:Button ID="Button3" runat="server" CssClass="submit-search" Text="SEARCH"  Width="74px" OnClientClick="ShowDialog()"/>
                    <asp:Button ID="Button4" runat="server" CssClass="submit-button" Text="FETCH" 
                        Width="74px" onclick="Button4_Click" />
                   <div class="clear">
                    </div>
                </div>
                <div class="form-sec-row">
                    <label>
                    <strong><strong>Floor:
                        <asp:Label ID="lblfloor" runat="server" Text=""></asp:Label>
                        
                    </strong></label>
                    <asp:DropDownList ID="ddlFloor" runat="server" 
                        CssClass="textbox-medium1" OnSelectedIndexChanged="ddlfloor_selectedIndexChange" AutoPostBack="true"></asp:DropDownList>
                  
                    <div class="clear">
                    </div>
                </div>
                <div class="form-sec-row">
                    <label>
                    <strong><strong>Work Station / Wings :
                        <asp:Label ID="lblid1" runat="server" Text=""></asp:Label>
                        
                    </strong></label>
                    <asp:DropDownList ID="ddlwing" runat="server" 
                        CssClass="textbox-medium1"></asp:DropDownList>
                  <%--  <asp:Button ID="Button3" runat="server" CssClass="submit-search" Text="SEARCH"  Width="74px" OnClientClick="ShowDialog()"/>--%>
                    <div class="clear">
                    </div>
                </div>
    
                <div class="form-sec-row">
                    <label>
                    <strong><strong>Patient Name :
                        <asp:Label ID="lblid2" runat="server" Text=""></asp:Label>
                    </strong></label>
                    <asp:DropDownList ID="ddlPatient" runat="server" 
                        CssClass="textbox-medium1"></asp:DropDownList>
                  <%--  <asp:Button ID="Button3" runat="server" CssClass="submit-search" Text="SEARCH"  Width="74px" OnClientClick="ShowDialog()"/>--%>
                    <div class="clear">
                    </div>
                </div>
          

                    <div class="form-sec-row" style="display:none;">
                    <label><strong> 
                        Patient's Address :</strong></label>
                    <asp:TextBox ID="TextBox2" runat="server" CssClass="textbox-medium1">
                     </asp:TextBox>
                    <div class="clear">
                    </div>
                </div>

                <div class="form-sec-row" style="display:none;">
                    <label><strong>
                      Doctor Name :</strong></label>
                             <asp:TextBox ID="TextBox3" runat="server" CssClass="textbox-medium1"></asp:TextBox>
                     
                   <div class="clear">
                    </div>
                </div>

                     <div class="form-sec-row" style="display:none;">
                    <label><strong>
                     Card No :</strong></label>
                         <asp:TextBox ID="TextBox4" runat="server" CssClass="textbox-medium1"></asp:TextBox>
                   <div class="clear">
                    </div>
                </div>
                <div class="form-sec-row">
                    <label><strong>
                     Issued By:</strong></label>
                         <asp:TextBox ID="issueby" runat="server" CssClass="textbox-medium1"></asp:TextBox>
                   <div class="clear">
                    </div>
                </div>
                

                    <div class="form-sec-row">
                    <label><strong>
                     Sales/Issue Bill Date :<span style="color:red;">*</span></strong></label>
                         <asp:TextBox ID="TextBox6" runat="server" CssClass="DatepickerCall"></asp:TextBox>
                               <asp:Label ID="Label3" runat="server" ForeColor="Green" Text="(DD/MM/YYYY)"></asp:Label>
                   <div class="clear">
                    </div>
                </div>
                <div class="form-sec-row">
                    <label><strong>
                     Received By :</strong></label>
                         <asp:TextBox ID="receiveby" runat="server" CssClass="textbox-medium1"></asp:TextBox>
                   <div class="clear">
                    </div>
                </div><div class="form-sec-row">
                    <label><strong>
                     Receive Date :</strong></label>
                         <asp:TextBox ID="receivedt" runat="server" CssClass="DatepickerCall"></asp:TextBox>
                               <asp:Label ID="Label2" runat="server" ForeColor="Green" Text="(DD/MM/YYYY)"></asp:Label>
                   <div class="clear">
                    </div>
                    </div>
                <div class="form-sec-row" style="width:100%;overflow:auto;"><table style="text-align: left; width:100%;" width="100%">
                   
                    <tr  style='background-color:#FF9300;'>
                        <td  align="center" style="width:15%;">
                            <asp:Label ID="lblMedi" runat="server" Font-Bold="True" Text="Medicine" Width=""></asp:Label>
                        </td>
                        <td align="center" style="width:15%;">
                            <asp:Label ID="lblMfg" runat="server" Font-Bold="True" Text="Manufacture "
                                Width="160px"></asp:Label>
                        </td>

                        <td  align="center" style="width:15%;">
                            <asp:Label ID="lblMediGrp" runat="server" Font-Bold="True" Text="Medicine Group" Width=""></asp:Label>
                        </td>

                             <td  align="center" style="width:15%;">
                            <asp:Label ID="lblMediSubGrp" runat="server" Font-Bold="True" Text="Med. Sub Group" Width=""></asp:Label>
                        </td>
                        
                        <td  align="center" style="width:15%;">
                            <asp:Label ID="lblBatch" runat="server" Font-Bold="True" Text="Batch" Width=""></asp:Label>
                        </td>
                        <td  align="center" style="width:15%;">
                            <asp:Label ID="lblExpiry" runat="server" Font-Bold="True" Text="Expiry" Width=""></asp:Label>
                        </td>
                        <td  align="center" style="width:15%;">
                            <asp:Label ID="lblStock" runat="server" Font-Bold="True" Text="Cur Stock" Width=""></asp:Label>
                        </td>
                        <td  align="center" style="width:10%;">
                            <asp:Label ID="lblQty" runat="server" Font-Bold="True" Text="Quantity" Width=""></asp:Label>
                        </td>
                        <td  align="center" style="display:none;">
                            <asp:Label ID="lblUnitPrice" runat="server" Font-Bold="True" Text="Unit Price" Width=""></asp:Label>
                        </td>
                        <td  align="center" style="display:none;">
                            <asp:Label ID="lblTotalPrice" runat="server" Font-Bold="True" 
                                Text="Total Price" Width="153px"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td class="style1">
                            <asp:DropDownList ID="ddlMedi1" runat="server" Width="100%" 
                                AutoPostBack="True" onselectedindexchanged="ddlMedi1_SelectedIndexChanged">
                            </asp:DropDownList>
                        </td>
                        <td class="style1">
                            <asp:DropDownList ID="ddlMfg1" runat="server" Width="100%">
                            </asp:DropDownList>
                        </td>
                        <td class="style1">
                            <asp:DropDownList ID="ddlMediGrp1" runat="server" Width="100%" 
                               AutoPostBack="true"  
                               Height="16px">
                            </asp:DropDownList>
                        </td>
                           <td class="style1">
                            <asp:DropDownList ID="ddlMediSubGrp1" runat="server" Width="100%" 
                                AutoPostBack="True">
                            </asp:DropDownList>
                        </td>

                        
                        <td class="style1">
                            <asp:DropDownList ID="txtBatch1" runat="server" Width="100%" OnSelectedIndexChanged="txtBatch1_SelectedIndexChanged" AutoPostBack="true" ></asp:DropDownList>
                        </td>
                        <td class="style1">
                            <asp:TextBox ID="Calendar2" runat="server" Width="100%" >
                            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                            </asp:TextBox>
                        </td>
                        <td class="style1"> 
                            <asp:TextBox ID="txtStock1" runat="server" Width="100%" AutoPostBack="True" ></asp:TextBox>
                        </td>
                        <td class="style1"> 
                            <asp:TextBox ID="txtQty1" runat="server" Width="100%" AutoPostBack="True" 
                                ontextchanged="txtQty1_TextChanged" ></asp:TextBox>
                            <asp:HiddenField ID="qty1" runat="server" Value="" />
                        </td>
                        <td class="style1" style="display:none;">
                            <asp:TextBox ID="txtUnitPrice1" runat="server" Width="100%" ></asp:TextBox>
                        </td>
                        <td class="style1" style="display:none;">
                            <asp:TextBox ID="txtTotalPrice1" runat="server" Width="100%" ></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td class="style1">
                            <asp:DropDownList ID="ddlMedi2" runat="server" Width="100%" 
                                AutoPostBack="True" onselectedindexchanged="ddlMedi2_SelectedIndexChanged">
                            </asp:DropDownList>
                        </td>
                        <td class="style1">
                            <asp:DropDownList ID="ddlMfg2" runat="server" Width="100%" >
                            </asp:DropDownList>
                        </td>
                        <td class="style1">
                            <asp:DropDownList ID="ddlMediGrp2" runat="server" Width="100%" 
                               AutoPostBack="true">
                            </asp:DropDownList>
                        </td>

                            <td class="style1">
                            <asp:DropDownList ID="ddlMediSubGrp2" runat="server" Width="100%" 
                                AutoPostBack="True">
                            </asp:DropDownList>
                        </td>
                        
                        <td class="style1">
                            <asp:DropDownList ID="txtBatch2" runat="server" Width="100%" OnSelectedIndexChanged="txtBatch2_SelectedIndexChanged" AutoPostBack="true"></asp:DropDownList>
                        </td>
                        <td class="style1">
                            <asp:TextBox ID="Calendar3" runat="server" Width="100%" >
                            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                            </asp:TextBox>
                        </td>
                        <td class="style1"> 
                            <asp:TextBox ID="txtStock2" runat="server" Width="100%" AutoPostBack="True" ></asp:TextBox>
                        </td>
                        <td class="style1"> 
                            <asp:TextBox ID="txtQty2" runat="server" Width="100%" AutoPostBack="True" 
                                ontextchanged="txtQty2_TextChanged" ></asp:TextBox>
                            <asp:HiddenField ID="qty2" runat="server" Value="" />
                        </td>
                        <td class="style1" style="display:none;">
                            <asp:TextBox ID="txtUnitPrice2" runat="server" Width="100%" ></asp:TextBox>
                        </td>
                        <td class="style1" style="display:none;">
                            <asp:TextBox ID="txtTotalPrice2" runat="server" Width="100%" ></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td class="style1">
                            <asp:DropDownList ID="ddlMedi3" runat="server" Width="100%" 
                                AutoPostBack="True" onselectedindexchanged="ddlMedi3_SelectedIndexChanged">
                            </asp:DropDownList>
                        </td>
                         <td class="style1">
                            <asp:DropDownList ID="ddlMfg3" runat="server" Width="100%" >
                            </asp:DropDownList>
                        </td>
                        <td class="style1">
                            <asp:DropDownList ID="ddlMediGrp3" runat="server" Width="100%" 
                               AutoPostBack="true">
                            </asp:DropDownList>
                        </td>
                            <td class="style1">
                            <asp:DropDownList ID="ddlMediSubGrp3" runat="server" Width="100%" 
                                AutoPostBack="True">
                            </asp:DropDownList>
                        </td>

                        
                        <td class="style1">
                            <asp:DropDownList ID="txtBatch3" runat="server" Width="100%" OnSelectedIndexChanged="txtBatch3_SelectedIndexChanged" AutoPostBack="true"></asp:DropDownList>
                        </td>
                        <td class="style1">
                            <asp:TextBox ID="Calendar4" runat="server" Width="100%" >
                            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                            </asp:TextBox>
                        </td>
                        <td class="style1"> 
                            <asp:TextBox ID="txtStock3" runat="server" Width="100%" AutoPostBack="True" ></asp:TextBox>
                        </td>
                        <td class="style1"> 
                            <asp:TextBox ID="txtQty3" runat="server" Width="100%" AutoPostBack="True" 
                                ontextchanged="txtQty3_TextChanged" ></asp:TextBox>
                            <asp:HiddenField ID="qty3" runat="server" Value="" />
                        </td>
                        <td class="style1" style="display:none;">
                            <asp:TextBox ID="txtUnitPrice3" runat="server" Width="100%" ></asp:TextBox>
                        </td>
                        <td class="style1" style="display:none;">
                            <asp:TextBox ID="txtTotalPrice3" runat="server" Width="100%" ></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td class="style1">
                            <asp:DropDownList ID="ddlMedi4" runat="server" Width="100%" 
                                AutoPostBack="True" onselectedindexchanged="ddlMedi4_SelectedIndexChanged">
                            </asp:DropDownList>
                        </td>
                       <td class="style1">
                            <asp:DropDownList ID="ddlMfg4" runat="server" Width="100%" >
                            </asp:DropDownList>
                        </td>
                        <td class="style1">
                            <asp:DropDownList ID="ddlMediGrp4" runat="server" Width="100%" 
                               AutoPostBack="true">
                            </asp:DropDownList>
                        </td>

                            <td class="style1">
                            <asp:DropDownList ID="ddlMediSubGrp4" runat="server" Width="100%" 
                                AutoPostBack="True">
                            </asp:DropDownList>
                        </td>
                        
                        
                        <td class="style1">
                            <asp:DropDownList ID="txtBatch4" runat="server" Width="100%" OnSelectedIndexChanged="txtBatch4_SelectedIndexChanged" AutoPostBack="true"></asp:DropDownList>
                        </td>
                        <td class="style1">
                            <asp:TextBox ID="Calendar5" runat="server" Width="100%" >
                            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                            </asp:TextBox>
                        </td>
                        <td class="style1"> 
                            <asp:TextBox ID="txtStock4" runat="server" Width="100%" AutoPostBack="True" ></asp:TextBox>
                        </td>
                        <td class="style1"> 
                            <asp:TextBox ID="txtQty4" runat="server" Width="100%" AutoPostBack="True" 
                                ontextchanged="txtQty4_TextChanged" ></asp:TextBox>
                            <asp:HiddenField ID="qty4" runat="server" Value="" />
                        </td>
                        <td class="style1" style="display:none;">
                            <asp:TextBox ID="txtUnitPrice4" runat="server" Width="100%" ></asp:TextBox>
                        </td>
                        <td class="style1" style="display:none;">
                            <asp:TextBox ID="txtTotalPrice4" runat="server" Width="100%" ></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td class="style1">
                            <asp:DropDownList ID="ddlMedi5" runat="server" Width="100%" 
                                AutoPostBack="True" onselectedindexchanged="ddlMedi5_SelectedIndexChanged">
                            </asp:DropDownList>
                        </td>
                         <td class="style1">
                            <asp:DropDownList ID="ddlMfg5" runat="server" Width="100%" >
                            </asp:DropDownList>
                        </td>
                        <td class="style1">
                            <asp:DropDownList ID="ddlMediGrp5" runat="server" Width="100%" 
                               AutoPostBack="true">
                            </asp:DropDownList>
                        </td>

                            <td class="style1">
                            <asp:DropDownList ID="ddlMediSubGrp5" runat="server" Width="100%" 
                                AutoPostBack="True" >
                            </asp:DropDownList>
                        </td>

                        
                        <td class="style1">
                            <asp:DropDownList ID="txtBatch5" runat="server" Width="100%" OnSelectedIndexChanged="txtBatch5_SelectedIndexChanged" AutoPostBack="true"></asp:DropDownList>
                        </td>
                        <td class="style1">
                            <asp:TextBox ID="Calendar6" runat="server" Width="100%" >
                            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                            </asp:TextBox>
                        </td>
                        <td class="style1"> 
                            <asp:TextBox ID="txtStock5" runat="server" Width="100%" AutoPostBack="True" ></asp:TextBox>
                        </td>
                        <td class="style1"> 
                            <asp:TextBox ID="txtQty5" runat="server" Width="100%" AutoPostBack="True" 
                                ontextchanged="txtQty5_TextChanged" ></asp:TextBox>
                            <asp:HiddenField ID="qty5" runat="server" Value="" />
                        </td>
                        <td class="style1" style="display:none;">
                            <asp:TextBox ID="txtUnitPrice5" runat="server" Width="100%" ></asp:TextBox>
                        </td>
                        <td class="style1" style="display:none;">
                            <asp:TextBox ID="txtTotalPrice5" runat="server" Width="100%" ></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td class="style1">
                            <asp:DropDownList ID="ddlMedi6" runat="server" Width="100%" 
                                AutoPostBack="True" onselectedindexchanged="ddlMedi6_SelectedIndexChanged">
                            </asp:DropDownList>
                        </td>
                          <td class="style1">
                            <asp:DropDownList ID="ddlMfg6" runat="server" Width="100%" >
                            </asp:DropDownList>
                        </td>
                        <td class="style1">
                            <asp:DropDownList ID="ddlMediGrp6" runat="server" Width="100%" 
                               AutoPostBack="true" >
                            </asp:DropDownList>
                        </td>

                            <td class="style1">
                            <asp:DropDownList ID="ddlMediSubGrp6" runat="server" Width="100%" 
                                AutoPostBack="True" >
                            </asp:DropDownList>
                        </td>
                        
                        
                        <td class="style1">
                            <asp:DropDownList ID="txtBatch6" runat="server" Width="100%" OnSelectedIndexChanged="txtBatch6_SelectedIndexChanged" AutoPostBack="true"></asp:DropDownList>
                        </td>
                        <td class="style1">
                            <asp:TextBox ID="Calendar7" runat="server" Width="100%" >
                            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                            </asp:TextBox>
                        </td>
                        <td class="style1"> 
                            <asp:TextBox ID="txtStock6" runat="server" Width="100%" AutoPostBack="True" ></asp:TextBox>
                        </td>
                        <td class="style1"> 
                            <asp:TextBox ID="txtQty6" runat="server" Width="100%" AutoPostBack="True" 
                                ontextchanged="txtQty6_TextChanged" ></asp:TextBox>
                            <asp:HiddenField ID="qty6" runat="server" Value="" />
                        </td>
                        <td class="style1" style="display:none;">
                            <asp:TextBox ID="txtUnitPrice6" runat="server" Width="100%" ></asp:TextBox>
                        </td>
                        <td class="style1" style="display:none;">
                            <asp:TextBox ID="txtTotalPrice6" runat="server" Width="100%" ></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td class="style1">
                            <asp:DropDownList ID="ddlMedi7" runat="server" Width="100%" 
                                AutoPostBack="True" onselectedindexchanged="ddlMedi7_SelectedIndexChanged">
                            </asp:DropDownList>
                        </td>
                         <td class="style1">
                            <asp:DropDownList ID="ddlMfg7" runat="server" Width="100%" >
                            </asp:DropDownList>
                        </td>
                        <td class="style1">
                            <asp:DropDownList ID="ddlMediGrp7" runat="server" Width="100%" 
                               AutoPostBack="true">
                            </asp:DropDownList>
                        </td>

                            <td class="style1">
                            <asp:DropDownList ID="ddlMediSubGrp7" runat="server" Width="100%" 
                                AutoPostBack="True" >
                            </asp:DropDownList>
                        </td>
                        
                        
                        <td class="style1">
                            <asp:DropDownList ID="txtBatch7" runat="server" Width="100%" OnSelectedIndexChanged="txtBatch7_SelectedIndexChanged" AutoPostBack="true"></asp:DropDownList>
                        </td>
                        <td class="style1">
                            <asp:TextBox ID="Calendar8" runat="server" Width="100%" >
                            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                            </asp:TextBox>
                        </td>
                        <td class="style1"> 
                            <asp:TextBox ID="txtStock7" runat="server" Width="100%" AutoPostBack="True" ></asp:TextBox>
                        </td>
                        <td class="style1"> 
                            <asp:TextBox ID="txtQty7" runat="server" Width="100%" AutoPostBack="True" 
                                ontextchanged="txtQty7_TextChanged" ></asp:TextBox>
                            <asp:HiddenField ID="qty7" runat="server" Value="" />
                        </td>
                        <td class="style1" style="display:none;">
                            <asp:TextBox ID="txtUnitPrice7" runat="server" Width="100%" ></asp:TextBox>
                        </td>
                        <td class="style1" style="display:none;">
                            <asp:TextBox ID="txtTotalPrice7" runat="server" Width="100%" ></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td class="style1">
                            <asp:DropDownList ID="ddlMedi8" runat="server" Width="100%" 
                                AutoPostBack="True" onselectedindexchanged="ddlMedi8_SelectedIndexChanged">
                            </asp:DropDownList>
                        </td>
                         <td class="style1">
                            <asp:DropDownList ID="ddlMfg8" runat="server" Width="100%" >
                            </asp:DropDownList>
                        </td>
                        <td class="style1">
                            <asp:DropDownList ID="ddlMediGrp8" runat="server" Width="100%" 
                               AutoPostBack="true" >
                            </asp:DropDownList>
                        </td>

                            <td class="style1">
                            <asp:DropDownList ID="ddlMediSubGrp8" runat="server" Width="100%" 
                                AutoPostBack="True">
                            </asp:DropDownList>
                        </td>
                        
                        
                        <td class="style1">
                            <asp:DropDownList ID="txtBatch8" runat="server" Width="100%" OnSelectedIndexChanged="txtBatch8_SelectedIndexChanged" AutoPostBack="true"></asp:DropDownList>
                        </td>
                        <td class="style1">
                            <asp:TextBox ID="Calendar9" runat="server" Width="100%" >
                            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                            </asp:TextBox>
                        </td>
                        <td class="style1"> 
                            <asp:TextBox ID="txtStock8" runat="server" Width="100%" AutoPostBack="True" ></asp:TextBox>
                        </td>
                        <td class="style1"> 
                            <asp:TextBox ID="txtQty8" runat="server" Width="100%" AutoPostBack="True" 
                                ontextchanged="txtQty8_TextChanged" ></asp:TextBox>
                            <asp:HiddenField ID="qty8" runat="server" Value="" />
                        </td>
                        <td class="style1" style="display:none;">
                            <asp:TextBox ID="txtUnitPrice8" runat="server" Width="100%" ></asp:TextBox>
                        </td>
                        <td class="style1" style="display:none;">
                            <asp:TextBox ID="txtTotalPrice8" runat="server" Width="100%" ></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td class="style1">
                            <asp:DropDownList ID="ddlMedi9" runat="server" Width="100%" 
                                AutoPostBack="True" onselectedindexchanged="ddlMedi9_SelectedIndexChanged">
                            </asp:DropDownList>
                        </td>
                         <td class="style1">
                            <asp:DropDownList ID="ddlMfg9" runat="server" Width="100%" >
                            </asp:DropDownList>
                        </td>
                        <td class="style1">
                            <asp:DropDownList ID="ddlMediGrp9" runat="server" Width="100%" 
                               AutoPostBack="true">
                            </asp:DropDownList>
                        </td>

                            <td class="style1">
                            <asp:DropDownList ID="ddlMediSubGrp9" runat="server" Width="100%" 
                                AutoPostBack="True">
                            </asp:DropDownList>
                        </td>

                        
                        <td class="style1">
                            <asp:DropDownList ID="txtBatch9" runat="server" Width="100%" OnSelectedIndexChanged="txtBatch9_SelectedIndexChanged" AutoPostBack="true"></asp:DropDownList>
                        </td>
                        <td class="style1">
                            <asp:TextBox ID="Calendar10" runat="server" Width="100%" >
                            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                            </asp:TextBox>
                        </td>
                        <td class="style1"> 
                            <asp:TextBox ID="txtStock9" runat="server" Width="100%" AutoPostBack="True" ></asp:TextBox>
                        </td>
                        <td class="style1"> 
                            <asp:TextBox ID="txtQty9" runat="server" Width="100%" AutoPostBack="True" 
                                ontextchanged="txtQty9_TextChanged" ></asp:TextBox>
                            <asp:HiddenField ID="qty9" runat="server" Value="" />
                        </td>
                        <td class="style1" style="display:none;">
                            <asp:TextBox ID="txtUnitPrice9" runat="server" Width="100%" ></asp:TextBox>
                        </td>
                        <td class="style1" style="display:none;">
                            <asp:TextBox ID="txtTotalPrice9" runat="server" Width="100%" ></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td class="style1">
                            <asp:DropDownList ID="ddlMedi10" runat="server" Width="100%" 
                                AutoPostBack="True" onselectedindexchanged="ddlMedi10_SelectedIndexChanged">
                            </asp:DropDownList>
                        </td>
                        <td class="style1">
                            <asp:DropDownList ID="ddlMfg10" runat="server" Width="100%" >
                            </asp:DropDownList>
                        </td>
                        <td class="style1">
                            <asp:DropDownList ID="ddlMediGrp10" runat="server" Width="100%" 
                               AutoPostBack="true">
                            </asp:DropDownList>
                        </td>

                            <td class="style1">
                            <asp:DropDownList ID="ddlMediSubGrp10" runat="server" Width="100%" 
                                AutoPostBack="True">
                            </asp:DropDownList>
                        </td>

                        
                        <td class="style1">
                            <asp:DropDownList ID="txtBatch10" runat="server" Width="100%" OnSelectedIndexChanged="txtBatch10_SelectedIndexChanged" AutoPostBack="true"></asp:DropDownList>
                        </td>
                        <td class="style1">
                            <asp:TextBox ID="Calendar11" runat="server" Width="100%" >
                            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                            </asp:TextBox>
                        </td>
                        <td class="style1"> 
                            <asp:TextBox ID="txtStock10" runat="server" Width="100%" AutoPostBack="True" ></asp:TextBox>
                        </td>
                        <td class="style1"> 
                            <asp:TextBox ID="txtQty10" runat="server" Width="100%" AutoPostBack="True" 
                                ontextchanged="txtQty10_TextChanged" ></asp:TextBox>
                            <asp:HiddenField ID="qty10" runat="server" Value="" />
                        </td>
                        <td class="style1" style="display:none;">
                            <asp:TextBox ID="txtUnitPrice10" runat="server" Width="100%" ></asp:TextBox>
                        </td>
                        <td class="style1" style="display:none;">
                            <asp:TextBox ID="txtTotalPrice10" runat="server" Width="100%" ></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td class="style1">
                            <asp:DropDownList ID="ddlMedi11" runat="server" Width="100%" 
                                AutoPostBack="True" onselectedindexchanged="ddlMedi11_SelectedIndexChanged">
                            </asp:DropDownList>
                        </td>
                         <td class="style1">
                            <asp:DropDownList ID="ddlMfg11" runat="server" Width="100%" >
                            </asp:DropDownList>
                        </td>
                        <td class="style1">
                            <asp:DropDownList ID="ddlMediGrp11" runat="server" Width="100%" 
                               AutoPostBack="true">
                            </asp:DropDownList>
                        </td>

                            <td class="style1">
                            <asp:DropDownList ID="ddlMediSubGrp11" runat="server" Width="100%" 
                                AutoPostBack="True" >
                            </asp:DropDownList>
                        </td>

                        
                        <td class="style1">
                            <asp:DropDownList ID="txtBatch11" runat="server" Width="100%" OnSelectedIndexChanged="txtBatch11_SelectedIndexChanged" AutoPostBack="true"></asp:DropDownList>
                        </td>
                        <td class="style1">
                            <asp:TextBox ID="Calendar12" runat="server" Width="100%" >
                            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                            </asp:TextBox>
                        </td>
                        <td class="style1"> 
                            <asp:TextBox ID="txtStock11" runat="server" Width="100%" AutoPostBack="True" ></asp:TextBox>
                        </td>
                        <td class="style1"> 
                            <asp:TextBox ID="txtQty11" runat="server" Width="100%" AutoPostBack="True" 
                                ontextchanged="txtQty11_TextChanged" ></asp:TextBox>
                            <asp:HiddenField ID="qty11" runat="server" Value="" />
                        </td>
                        <td class="style1" style="display:none;">
                            <asp:TextBox ID="txtUnitPrice11" runat="server" Width="100%" ></asp:TextBox>
                        </td>
                        <td class="style1" style="display:none;">
                            <asp:TextBox ID="txtTotalPrice11" runat="server" Width="100%" ></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td class="style1">
                            <asp:DropDownList ID="ddlMedi12" runat="server" Width="100%" 
                                AutoPostBack="True" onselectedindexchanged="ddlMedi12_SelectedIndexChanged">
                            </asp:DropDownList>
                        </td>
                         <td class="style1">
                            <asp:DropDownList ID="ddlMfg12" runat="server" Width="100%">
                            </asp:DropDownList>
                        </td>
                        <td class="style1">
                            <asp:DropDownList ID="ddlMediGrp12" runat="server" Width="100%" 
                               AutoPostBack="true">
                            </asp:DropDownList>
                        </td>

                            <td class="style1">
                            <asp:DropDownList ID="ddlMediSubGrp12" runat="server" Width="100%" 
                                AutoPostBack="True">
                            </asp:DropDownList>
                        </td>


                        
                        <td class="style1">
                            <asp:DropDownList ID="txtBatch12" runat="server" Width="100%"  OnSelectedIndexChanged="txtBatch12_SelectedIndexChanged" AutoPostBack="true"></asp:DropDownList>
                        </td>
                        <td class="style1">
                            <asp:TextBox ID="Calendar13" runat="server" Width="100%" >
                            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                            </asp:TextBox>
                        </td>
                        <td class="style1"> 
                            <asp:TextBox ID="txtStock12" runat="server" Width="100%" AutoPostBack="True" ></asp:TextBox>
                        </td>
                        <td class="style1"> 
                            <asp:TextBox ID="txtQty12" runat="server" Width="100%" AutoPostBack="True" 
                                ontextchanged="txtQty12_TextChanged" ></asp:TextBox>
                            <asp:HiddenField ID="qty12" runat="server" Value="" />
                        </td>
                        <td class="style1" style="display:none;">
                            <asp:TextBox ID="txtUnitPrice12" runat="server" Width="100%" ></asp:TextBox>
                        </td>
                        <td class="style1" style="display:none;">
                            <asp:TextBox ID="txtTotalPrice12" runat="server" Width="100%" ></asp:TextBox>
                        </td>
                    </tr>
                </table>
                </div>
                  
                            <div class="form-sec-row" style="display:none;">
                    <label><strong>
                      Discount :</strong></label>
                    <asp:TextBox ID="TextBox7" runat="server" CssClass="textbox-medium1"></asp:TextBox>
                    <div class="clear">
                    </div>
                </div>

                          <div class="form-sec-row" style="display:none;">
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
                          onclick="Button1_Click"  /><asp:HiddenField ID="hdnInsUpd" runat="server" Value="I" />
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
                        <td align="center" align="center" style="margin-left:30%;">
                         <input type="button" value="Back" style="width:70px; font-size:x-small;margin-left:100px;" onclick="window.history.back()" />
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

