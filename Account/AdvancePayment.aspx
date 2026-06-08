<%@ Page Language="C#"  MasterPageFile="~/MasterAll/MasterPageAll.master"  AutoEventWireup="true" CodeFile="AdvancePayment.aspx.cs" Inherits="Account_AdvancePayment" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <script type="text/javascript" language="javascript">
        function autoCompleteEx_PatientSelected(sender, args) {
            debugger;
            var regname = args.get_value().split('~');
            document.getElementById("ctl00_ContentPlaceHolder1_txtreg").value = regname[0];
            document.getElementById("ctl00_ContentPlaceHolder1_hdnregno").value = regname[0];
            document.getElementById("ctl00_ContentPlaceHolder1_txtPatientName").value = regname[1];
            document.getElementById("ctl00_ContentPlaceHolder1_hdnLedgerId").value = regname[2];
        }


        function autoCompleteEx_ItemSelected(sender, args) {
            debugger;
            var regname = args.get_value().split('~');// alert(regname[0]);
            document.getElementById("ctl00_ContentPlaceHolder1_txtDocId").value = regname[0];
            document.getElementById("ctl00_ContentPlaceHolder1_txtdocname").value = regname[1];

            $("#txtdocname").focus();
            //$("#DropDownList4").val(regname[1]);
        }
    </script>

    <script type="text/javascript" language="javascript">
        function printDiv(divID) {
            var divElements = document.getElementById(divID).innerHTML;
            var oldPage = document.body.innerHTML;
            document.body.innerHTML = "<html><head><title></title></head><body>" + divElements + "</body>";
            window.print();
            document.body.innerHTML = oldPage;
        }



        function getItemData() {
            var srvname = document.getElementById("ctl00_ContentPlaceHolder1_txtPatientName").value;
            $.ajax({
                type: "POST",
                url: "AdvancePayment.aspx/SearchByPatientName",
                data: "{ prefixText: '" + srvname + "'}",
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                async: "true",
                cache: "false",
                success: function (res) {
                    var datas = res.d;
                    //alert(datas);
                    var markup = "<table width='100%'>";
                    for (var x = 0; x < datas.length; x++) {
                        //alert(datas[x]);

                        var testname = datas[x].split('~');
                        markup = markup + "<tr><td width='40%' style='border:1px;border-right:1px solid;'><a href='#' onclick='setvalue(" + x + ")'>" + testname[0] + "</a></td><td width='60%' style='border:1px;'><input type='hidden' id='id" + x + "' value='" + testname[0] + "'><input type='hidden' id='name" + x + "' value='" + testname[1] + "'><input type='hidden' id='ledgerid" + x + "' value='" + testname[2] + "'><a href='#' onclick='setvalue(" + x + ")'>" + testname[1] + "</a></td></tr>";
                    }
                    markup = markup + "</table>";
                    $("#srvdiv").html(markup);
                    $("#srvdiv").show();
                }
            });
        }


        function setvalue(indx) {
            //alert(indx);
            var regno = $("#id" + indx).val();
            var name = $("#name" + indx).val();
            var ledgerid = $("#ledgerid" + indx).val();
            document.getElementById("ctl00_ContentPlaceHolder1_txtreg").value = regno;
            document.getElementById("ctl00_ContentPlaceHolder1_hdnregno").value = regno;
            document.getElementById("ctl00_ContentPlaceHolder1_txtPatientName").value = name;
            document.getElementById("ctl00_ContentPlaceHolder1_hdnLedgerId").value = ledgerid;
            $("#srvdiv").hide()
        }
    </script>



</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <div class="pageheader">
                <asp:Label ID="Label1" runat="server">Advance Payment</asp:Label>
            </div>
            <table width="290px" cellpadding="0" cellspacing="0">
                <tr>
                    <td>
                        <asp:Button Text="Entry" BorderStyle="None" ID="Tab1" Width="145px"  Height="40px"  CssClass="Initial" runat="server" onclick="Tab1_Click"/>

                    </td>
                    <td>
                        <asp:Button Text="List" BorderStyle="None" ID="Tab2" CssClass="Initial"  Width="145px"  Height="40px" runat="server" onclick="Tab2_Click"/>
                    </td>
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
                            <table style="width:100%;">
                                <tr>
                                    <td style="width:50%">
                                         <table style="width:100%;">
                                <tr>
                                    <td style="width:20%;">
                                        <label><strong>Receipt No :</strong></label>
                                    </td>
                                    <td style="width:30%;">
                                        <asp:TextBox ID="txtReceiptNo" runat="server" Width="180px" CssClass="textbox-medium1" TabIndex="1" Enabled="false"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <label><strong><strong>Patient Name :</strong></label>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtPatientName" runat="server" CssClass="textbox-medium1" TabIndex="2" onkeyup="getItemData()"></asp:TextBox>
                                <div id="srvdiv" style="width:30%;position:absolute;z-index:1;max-height:150px;overflow:auto;display:none;border-left:1px solid;border-right:1px solid;border-top:1px solid;border-bottom:1px solid;background-color:white;"></div>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                         <label><strong>Registration No :</strong></label> 
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtreg" runat="server" CssClass="textbox-medium1" ReadOnly="true" TabIndex="3" ></asp:TextBox> 
                                        <asp:HiddenField ID="hdnLedgerId" runat="server" />
                                        <asp:HiddenField ID="hdnregno" runat="server"/>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <label><strong>Doctor Name :</strong></label>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtDocId" runat="server" CssClass="textbox-medium1" style="display:none;"></asp:TextBox>
                                   <asp:TextBox ID="txtdocname" runat="server" CssClass="textbox-medium1" ></asp:TextBox>
                                    <cc1:AutoCompleteExtender ServiceMethod="Searchdoc" OnClientItemSelected="autoCompleteEx_ItemSelected" MinimumPrefixLength="1"
                                                CompletionInterval="100" EnableCaching="false" CompletionSetCount="10" TargetControlID="txtdocname" ID="AutoCompleteExtender2" runat="server" 
                                                   FirstRowSelected = "false" ></cc1:AutoCompleteExtender>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <label><strong>Service Name :</strong></label>
                                    </td>
                                    <td>
                                        <asp:DropDownList ID="ddlService" runat="server" CssClass="textbox-medium1" OnSelectedIndexChanged="ddlService_SelectedIndexChanged" AutoPostBack="true"></asp:DropDownList>
                                        <asp:Label ID="lblSrvName" runat="server" Visible="false"></asp:Label>
                                        
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <label><strong>Service Cost :</strong></label>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="lblSrvCost" runat="server" CssClass="textbox-medium1" ReadOnly="true" ></asp:TextBox>
                                        
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <label><strong>Amount Paid :</strong></label> 
                                    </td>
                                    <td>
                                        <asp:TextBox runat="server" ID="txtPayableAmt" CssClass="textbox-medium1" style="text-align:right" TabIndex="4" Text="0.00"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <label><strong>Payment Mode :</strong></label> 
                                    </td>
                                    <td>
                                        <asp:DropDownList ID="ddlPaymentMode" runat="server" CssClass="textbox-medium1" TabIndex="5" OnSelectedIndexChanged="ddlPaymentMode_SelectedIndexChanged" AutoPostBack="true">
                                            <asp:ListItem Value="C">Cash</asp:ListItem>
                                            <asp:ListItem Value="R">Card</asp:ListItem>
                                            <asp:ListItem Value="E">e-Wallet</asp:ListItem>
                                            <asp:ListItem Value="N">Net Banking</asp:ListItem>
                                        </asp:DropDownList>
                                    </td>
                                </tr>
                                <tr runat="server" id="divBank" visible="false">
                                    <td>
                                        <label  id="lblbankNm" runat="server"><strong>Bank Name :</strong></label> 
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtBankName" runat="server" CssClass="textbox-medium1" ></asp:TextBox>
                                    </td>
                                </tr>
                                <tr runat="server" id="divchqno" visible="false">
                                    <td>
                                        <label id="lblchqno" runat="server"><strong>Cheque No :</strong></label> 
                                
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtChequeNo" runat="server" CssClass="textbox-medium1" ></asp:TextBox>
                                    </td>
                                </tr>
                                <tr runat="server" id="divchqdt" visible="false">
                                    <td>
                                        <label id="lblchqdt" runat="server"><strong>Cheque Date :</strong></label> 
                               
                                    </td>
                                    <td>
                                         <asp:TextBox ID="txtchqdt" runat="server" CssClass="textbox-medium1"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td>

                                    </td>
                                    <td>

                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="2" align="center">
                                        <asp:Button ID="Button1" runat="server" CssClass="submit-button"  Height="28px" Text="Submit" onclick="Button1_Click" TabIndex="6"   />
                                <asp:Button ID="Button2" runat="server" CssClass="submit-button"  Height="28px" Text="Cancel" onclick="Button2_Click" TabIndex="7"   />
                                <asp:Button ID="btnrcpt" runat="server" Text="Receipt"   Height="28px" CssClass="submit-button" OnClick="btnrcpt_Click"/>
                                    </td>
                                </tr>
                            </table>
                                    </td>
                                    <td style="width:50%"></td>
                                </tr>
                            </table>
                           
                            
                            
                            
                        </div>
                         <table width="100%" >    
                             <tr>        
                                <td style="width: 100%">   <div id='mydiv' style="width:100%;">            
                                    <asp:Literal ID="ltrReport" runat="server"></asp:Literal><br />   </div>                 
                                </td>
                            </tr>

                             <tr>
                                <td  colspan="3" style="text-align:center">
                                    <asp:Button ID="Button6" runat="server" Text="Back" Font-Size="X-Small" Visible="false" Width="70px" onclick="Button6_Click"/>
                                    <asp:Button ID="Button7" runat="server" Font-Size="X-Small" Visible="false" Width="70px" Text="Print" OnClientClick="javascript:printDiv('mydiv')" />
                                </td>
                            </tr>
                        </table>
                    </asp:View>
                    <asp:View ID="View2" runat="server">
                        <div style='width:100%;'>
                            <table width="100%" style='background-color:#3AA8FC; color:#FFF;'>
                                <tr>
                                    <td><strong>Patient Name :</strong></td>
                                     <td>  
                                         <asp:TextBox ID="txtPnameSrch" Width="180px"  runat="server"></asp:TextBox>  
                                     </td>
                                    <td><strong>Registration No :</strong></td>
                                     <td>  
                                         <asp:TextBox ID="txtRegNoSrch" Width="180px"  runat="server"></asp:TextBox>  
                                     </td>
                                    <td><strong>Phone No :</strong></td>
                                     <td>  
                                         <asp:TextBox ID="txtPhnoSrch" Width="180px"  runat="server"></asp:TextBox>  
                                     </td>
                                     <td>
                                         <asp:Button ID="Button3" runat="server" Text="Search" CssClass="submit-button" onclick="Button3_Click" />
                                     </td>
                                </tr>
                            </table>
                        </div>
                        <div class="listing"  style='width:100%; height:500px; overflow:auto;'>
                            <asp:GridView id="GridView1"  Width="100%" CssClass="grid" PagerStyle-CssClass="pgr"
                             DataKeyNames ="ReceiptNo" runat="server" 
                             AutoGenerateColumns="False" AllowPaging="True"  PageSize="1000" ShowHeader="true"
                             OnRowCommand="GridView1_RowCommand" OnRowDataBound="GridView1_RowDataBound"  OnRowDeleting="GridView1_RowDeleting" SelectedRowStyle-BackColor="GreenYellow"
                             OnPageIndexChanging="GridView1_PageIndexChanging" 
                                onselectedindexchanged="GridView1_SelectedIndexChanged" >

                                   <RowStyle HorizontalAlign="Center" />

                                <Columns>
                                    <asp:TemplateField HeaderText="Receipt No" ItemStyle-Width="15%" ItemStyle-HorizontalAlign="Left">
                                        <ItemTemplate>
                                            <asp:Label ID="receiptNo" runat="server" Text='<%# Bind("ReceiptNo") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Registration No"  ItemStyle-Width="15%" ItemStyle-HorizontalAlign="Left">
                                        <ItemTemplate>
                                            <asp:Label ID="lblRegNo" runat="server" Text='<%# Bind("RegNo") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Patient Name"  ItemStyle-Width="20%" ItemStyle-HorizontalAlign="Left">
                                        <ItemTemplate>
                                            <asp:Label ID="lblPatientName" runat="server" Text='<%# Bind("PName") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Doctor Name"  ItemStyle-Width="20%" ItemStyle-HorizontalAlign="Left">
                                        <ItemTemplate>
                                            <asp:Label ID="lbldocId" runat="server" Text='<%# Bind("Referdoc") %>' Visible="false"></asp:Label>
                                            <asp:Label ID="lblDocName" runat="server" Text='<%# Bind("doc_name") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Advance Amount" ItemStyle-Width="10%"  ItemStyle-HorizontalAlign="right">
                                        <ItemTemplate>
                                            <asp:Label ID="lblAdvAmt" runat="server" Text='<%# Bind("AdvAmount") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                     <asp:TemplateField HeaderText="Advance Date"   ItemStyle-Width="10%" ItemStyle-HorizontalAlign="Left">
                                        <ItemTemplate>
                                            <asp:Label ID="lblAdvDate" runat="server" Text='<%# Bind("AdvDate") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField> 
                                
                                     <asp:TemplateField HeaderText="PaymentMode" ItemStyle-Width="10%"  ItemStyle-HorizontalAlign="Left">
                                        <ItemTemplate>
                                            <asp:Label ID="lblPaymode" runat="server" Text='<%# Bind("paymode") %>'></asp:Label>
                                            <asp:Label ID="lblPaymentMode" runat="server" Text='<%# Bind("PaymentMode") %>' Visible="false"></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Status"   ItemStyle-Width="10%" ItemStyle-HorizontalAlign="Left">
                                        <ItemTemplate>
                                            <asp:Label ID="lblStatusText" runat="server" Text='<%# Bind("StatusShow") %>'></asp:Label>
                                            <asp:Label ID="lblstatus" runat="server" Text='<%# Bind("Status") %>' Visible="false"></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField> 
                                    <asp:TemplateField HeaderText="">
                                        <ItemTemplate>
                                             <asp:LinkButton ID="LinkButton4"  CommandName="Print" CommandArgument="<%# Container.DataItemIndex %>"  runat="server">Print Receipt</asp:LinkButton>
                                        </ItemTemplate>
                                    </asp:TemplateField>  
                                           
                                    <%--<asp:CommandField SelectText="Edit" ShowSelectButton="false"     ItemStyle-Width="10%" ControlStyle-CssClass="temp" HeaderText="Edit" SelectImageUrl="~/Images/edit.png" ButtonType="Image">
                                        <ControlStyle CssClass="temp"></ControlStyle>
                                    </asp:CommandField>--%>
                                    <asp:CommandField ShowDeleteButton="true" ControlStyle-CssClass="temp"   ItemStyle-Width="10%" HeaderText="Delete" DeleteImageUrl="~/Images/delete.png" ButtonType="Image"  Visible="false">
                                        <ControlStyle CssClass="temp"></ControlStyle>
                                    </asp:CommandField>
                                </Columns>
                                <PagerStyle CssClass="pgr" />
                                <EditRowStyle BackColor="#CCFF33" />
                                <AlternatingRowStyle BackColor="#FFDB91" />
                            </asp:GridView>
                        </div>
                        <table width="100%" >    
                             <tr>        
                                <td style="width: 100%">   <div id='rcptDiv' style="width:100%;">            
                                    <asp:Literal ID="ltrReceipt" runat="server" Visible="false"></asp:Literal><br />   </div>                 
                                </td>
                            </tr>

                             <tr>
                                <td  colspan="3" style="text-align:center">
                                    <asp:Button ID="Button4" runat="server" Text="Close" Font-Size="X-Small" Visible="false" Width="70px" onclick="Button4_Click"/>
                                    <asp:Button ID="Button5" runat="server" Font-Size="X-Small" Visible="false" Width="70px" Text="Print" OnClientClick="javascript:printDiv('rcptDiv')" />
                                </td>
                            </tr>
                        </table>
                    </asp:View>
                </asp:MultiView>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>

