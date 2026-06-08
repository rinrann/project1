<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/MasterAll/MasterPageAll.master"  CodeFile="SampleCollection.aspx.cs" Inherits="Pathology_SampleCollection" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>


<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">

    <script type="text/javascript" language="javascript">
        function ShowDialog() {
            //var rtvalue = window.open("RegistrationPopup.aspx", "sss", "Width:800px; Height:550px; dialogLeft:250px;");
            var rtvalue = window.open("../OPD/PtientDetailspopup.aspx", "sss", "Width:800px; Height:550px; dialogLeft:250px;");
            //document.getElementById("ctl00_ContentPlaceHolder1_txtreg").value = rtvalue.NameValue;
        }



        function GetDatetime() {
            var now = new Date();
            var day, mnt, yr;
            day = now.getDate();
            mnt = now.getMonth() + 1;
            yr = now.getFullYear();
            if (day < 10)
                day = "0" + day;
            if (mnt < 10)
                mnt = "0" + mnt;
            var datetime = day + '/' + mnt + '/' + yr;/////
            var hour = now.getHours();
            var minute = now.getMinutes();
            var a;
            if (hour > 12) {
                hour = hour - 12;
                a = "PM";
            }
            else {
                a = "AM";
                hour = hour;
            }
            if (minute < 10)
                minute = "0" + minute;
            var time = hour + ':' + minute + ' ' + a;
            document.getElementById("ctl00_ContentPlaceHolder1_HiddenField1").value = time;
        }


        function printDiv(divID) {
            var divElements = document.getElementById(divID).innerHTML;
            var oldPage = document.body.innerHTML;
            document.body.innerHTML = "<html><head><title></title></head><body>" + divElements + "</body>";
            window.print();
            document.body.innerHTML = oldPage;
        }
        function ParentPageFunctionName(x) {
            document.getElementById("ctl00_ContentPlaceHolder1_txtNewCost").value = x;
        }

        function DisableBackButton() {
            window.history.forward()
        }
        DisableBackButton();
        window.onload = DisableBackButton;
        window.onpageshow = function (evt) { if (evt.persisted) DisableBackButton() }
        window.onunload = function () { void (0) }


        function Calling() {
            var date = new Date();

            $("input[id$='txtdate']").datepicker({ dateFormat: 'dd/mm/yy' });

            $("input[id$='txtdeldate']").datepicker({ dateFormat: 'dd/mm/yy' });


            $("input[id$='Button1']").click(function () {


                var text = document.getElementById("ctl00_ContentPlaceHolder1_txtdate").value;
                var date = Date.parse(text);
                if (isNaN(date)) {
                    alert("Invalid Date !");
                    $("input[id$='txtdate']").focus();
                    $("input[id$='txtdate']").addClass('textboxerr');
                    return false;
                }
                else {
                    $("input[id$='txtdate']").removeClass('textboxerr');
                }


                if ($("input[id$='txtreqno']").val() == '') {
                    alert('Please Enter Requisition No !');
                    $("input[id$='txtreqno']").focus();
                    $("input[id$='txtreqno']").addClass('textboxerr');
                    return false;
                }
                else {
                    $("input[id$='txtreqno']").removeClass('textboxerr');
                }

                if ($("input[id$='txtage']").val() == '') {
                    alert('Please Enter Age !');
                    $("input[id$='txtage']").focus();
                    $("input[id$='txtage']").addClass('textboxerr');
                    return false;
                }
                else {
                    $("input[id$='txtage']").removeClass('textboxerr');
                }



                if ($("input[id$='txtname']").val() == '') {
                    alert('Please Enter Name !');
                    $("input[id$='txtname']").focus();
                    $("input[id$='txtname']").addClass('textboxerr');
                    return false;
                }
                else {
                    $("input[id$='txtname']").removeClass('textboxerr');
                }


                if ($("input[id$='txtreferal']").val() == '') {
                    alert('Please Enter Referal Name !');
                    $("input[id$='txtreferal']").focus();
                    $("input[id$='txtreferal']").addClass('textboxerr');
                    return false;
                }
                else {
                    $("input[id$='txtreferal']").removeClass('textboxerr');
                }


                if ($("input[id$='txtaddress']").val() == '') {
                    alert('Please Enter Address !');
                    $("input[id$='txtaddress']").focus();
                    $("input[id$='txtaddress']").addClass('textboxerr');
                    return false;
                }
                else {
                    $("input[id$='txtaddress']").removeClass('textboxerr');
                }



                if ($("input[id$='txtph1']").val() == '') {
                    alert('Please Enter Phone -1  !');
                    $("input[id$='txtph1']").focus();
                    $("input[id$='txtph1']").addClass('textboxerr');
                    return false;
                }
                else {
                    $("input[id$='txtph1']").removeClass('textboxerr');
                }


                if ($("input[id$='txttestcode']").val() == '') {
                    alert('Please Enter Test Code!');
                    $("input[id$='txttestcode']").focus();
                    $("input[id$='txttestcode']").addClass('textboxerr');
                    return false;
                }
                else {
                    $("input[id$='txttestcode']").removeClass('textboxerr');
                }



                if ($("input[id$='txttestname']").val() == '') {
                    alert('Please Enter Test Name !');
                    $("input[id$='txttestname']").focus();
                    $("input[id$='txttestname']").addClass('textboxerr');
                    return false;
                }
                else {
                    $("input[id$='txttestname']").removeClass('textboxerr');
                }



                if ($("input[id$='txtcost']").val() == '') {
                    alert('Please Enter Test Cost !');
                    $("input[id$='txtcost']").focus();
                    $("input[id$='txtcost']").addClass('textboxerr');
                    return false;
                }
                else {
                    $("input[id$='txtcost']").removeClass('textboxerr');
                }


                if ($("input[id$='txtdate']").val() == '') {
                    alert('Please Enter Test Date!');
                    $("input[id$='txtdate']").focus();
                    $("input[id$='txtdate']").addClass('textboxerr');
                    return false;
                }
                else {
                    $("input[id$='txtdate']").removeClass('textboxerr');
                }


                if ($("input[id$='txtdeldate']").val() == '') {
                    alert('Please Enter Delivery Date !');
                    $("input[id$='txtdeldate']").focus();
                    $("input[id$='txtdeldate']").addClass('textboxerr');
                    return false;
                }
                else {
                    $("input[id$='txtdeldate']").removeClass('textboxerr');
                }
            });

            $("input[id$='txtph1']").keydown(function (event) {
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


            $("input[id$='txtage']").keydown(function (event) {
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


            $("input[id$='txtph2']").keydown(function (event) {
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

            $("input[id$='txtadvamount']").keydown(function (event) {
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



        function autoCompleteEx_ItemSelected(sender, args) {

            var regname = args.get_value().split('~');// alert(regname[0]);
            //document.getElementById("ctl00_ContentPlaceHolder1_TextBoxicode").value = regname[1];
            document.getElementById("ctl00_ContentPlaceHolder1_txtreferal").value = regname[0];

            $("#txtreferal").focus();
            //$("#DropDownList4").val(regname[1]);
        }
    </script>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <%--For Busy Loader .............................--%>
      <asp:UpdateProgress ID="UpdateProgress1" runat="server">
        <ProgressTemplate>
           <div id="aaa" class="progressBackgroundFilter">          </div>
           <div id="bbb" class="processMessage"><img alt="Loading" src="../images/pwait.gif"/></div>
        </ProgressTemplate>
      </asp:UpdateProgress>
    <%--For Busy Loader End.............................--%>

    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <div id="h1">
                <div class="pageheader">
                    <asp:Label ID="Label1" runat="server">Sample Collection</asp:Label>
                </div>

                <table width="290px" cellpadding="0" cellspacing="0">
                    <tr>
                        <td>
                            <asp:Button Text="Entry" BorderStyle="None" ID="Tab1" Width="145px"  Height="40px" CssClass="Initial" runat="server" onclick="Tab1_Click"/>
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
                                <asp:HiddenField ID="TextBox4" runat="server" /> <asp:HiddenField ID="HiddenField1" runat="server" />
                                <asp:HiddenField ID="hdlSrlNo" runat="server" />
                                <div class="form-sec-row">
                                    <label><strong>Registration No :</strong></label> 
                                   <asp:TextBox ID="txtreg" runat="server" CssClass="textbox-medium1" ReadOnly="true" ClientIDMode="Static"></asp:TextBox> 
                                    <asp:Button ID="Button4" runat="server" 
                                        CssClass="submit-search" Height="28px"  Text="Quick Search" OnClientClick="ShowDialog()" />
                                       <asp:Button ID="btnFetch" runat="server" CssClass="submit-button"  Height="28px" Text="Fetch" onclick="btnFetch_Click" />
                                    <div class="clear">  </div>
                                </div>

                                <div class="form-sec-row">
                                    <label><strong>Patient Name :</strong></label>
                                    <asp:TextBox ID="txtname" runat="server" CssClass="textbox-medium1" 
                                       ></asp:TextBox>
                                    <div class="clear"></div>
                                </div>
                                <div class="form-sec-row">
                                    <label><strong>Under Doctor :</strong></label>
                                    <asp:TextBox ID="txtunderdoc" runat="server"  CssClass="textbox-medium1" Enabled="false"></asp:TextBox>
                
                                    <div class="clear"></div>
                                </div>
                                <div class="form-sec-row">
                                    <label><strong>Referal Name :</strong></label>
                                    <asp:TextBox ID="txtreferal" runat="server"  CssClass="textbox-medium1" 
                                       ></asp:TextBox>
                                    <cc1:AutoCompleteExtender ServiceMethod="Searchdoc" OnClientItemSelected="autoCompleteEx_ItemSelected" MinimumPrefixLength="1"
                                                        CompletionInterval="100" EnableCaching="false" CompletionSetCount="10" TargetControlID="txtreferal" ID="AutoCompleteExtender2" runat="server" 
                                                           FirstRowSelected = "false" ></cc1:AutoCompleteExtender>
                                    <div class="clear"></div>
                                </div>

                                <div class="form-sec-row">
                                    <label><strong>Age :</strong></label>  
                                     <asp:TextBox ID="txtage" runat="server" MaxLength="3" CssClass="textbox-medium1"></asp:TextBox>
                                    <div class="clear"></div>
                                </div>

                                <div class="form-sec-row">
                                    <label><strong>Address - 1:</strong></label>  
                                     <asp:TextBox ID="txtaddress" runat="server" CssClass="textbox-medium1"></asp:TextBox>
                                    <div class="clear"></div>
                                </div>

                                <div class="form-sec-row">
                                    <label><strong>Address - 2:</strong></label>  
                                     <asp:TextBox ID="txtaddress2" runat="server" CssClass="textbox-medium1" ></asp:TextBox>
                                    <div class="clear"></div>
                                </div>
                                <div class="form-sec-row">
                                    <label><strong>Phone-1 :</strong></label>
                                    <asp:TextBox ID="TextBox5" runat="server" CssClass="textbox-medium1" Width="50" Enabled="False">+91</asp:TextBox>
                                    <asp:TextBox ID="txtph1" runat="server" CssClass="textbox-medium1" MaxLength="10" Width="246px" ></asp:TextBox>
                                    <div class="clear"></div>
                                </div>
                                <div class="form-sec-row">
                                    <label><strong>Phone-2 :</strong></label>
                                        <asp:TextBox ID="TextBox1" runat="server" CssClass="textbox-medium1" Width="50" Enabled="False">+91</asp:TextBox>
                                        <asp:TextBox ID="txtph2" runat="server" CssClass="textbox-medium1" MaxLength="10" Width="246px" ></asp:TextBox>
                                        <div class="clear"></div>
                                </div>
                                <div class="form-sec-row">
                                    <label><strong>Email Id:</strong></label>  
                                     <asp:TextBox ID="TextBox2" runat="server" CssClass="textbox-medium1" ></asp:TextBox>
                                    <div class="clear"></div>
                                </div>
                                <div class="form-sec-row">
                                    <label><strong>Requisition No :</strong></label>  
                                     <asp:DropDownList ID="ddlReqNo" runat="server" CssClass="textbox-medium1" ></asp:DropDownList>
                                    <div class="clear"></div>
                                </div>
                                <div class="form-sec-row">
                                    <label><strong>Sample Name :</strong></label>  
                                     <asp:DropDownList ID="ddlSample" runat="server" CssClass="textbox-medium1" ></asp:DropDownList>
                                    <div class="clear"></div>
                                </div>
                                <div class="form-sec-row">
                                    <label><strong>Unit Collected:</strong></label>  
                                     <asp:TextBox ID="txtUnit" runat="server" CssClass="textbox-medium1" ></asp:TextBox>
                                    <div class="clear"></div>
                                </div>
                                <div class="form-sec-row">
                                    <label><strong>Agency Name :</strong></label>  
                                     <asp:DropDownList ID="ddlAgency" runat="server" CssClass="textbox-medium1" ></asp:DropDownList>
                                    <div class="clear"></div>
                                </div>
                                <div class="form-sec-row">
                                    <label><strong>Collector Name</strong></label>  
                                     <asp:TextBox ID="txtCollector" runat="server" CssClass="textbox-medium1" ></asp:TextBox>
                                    <div class="clear"></div>
                                </div>
                                <div class="form-sec-row">
                                    <label><strong>Collect Date :</strong></label>
                                     <asp:TextBox ID="txtdate" runat="server" CssClass="textbox-medium1"></asp:TextBox>
                                    <cc1:MaskedEditExtender ID="MaskedEditExtender2" runat="server" TargetControlID="txtdate"
                                            Mask="99/99/9999" MessageValidatorTip="true" MaskType="Date" DisplayMoney="Left"
                                            AcceptNegative="Left" ErrorTooltipEnabled="True" CultureName="en-GB" />
                                           <asp:Label ID="Label3" runat="server" ForeColor="Green" Text="(DD/MM/YYYY)"></asp:Label>
                                    <div class="clear"></div>
                                </div>
                                 <div class="form-sec-row">
                                    <label><strong>Delivery Date :</strong></label>
                                     <asp:TextBox ID="txtdeldate" runat="server" CssClass="textbox-medium1"></asp:TextBox>
                                     <cc1:MaskedEditExtender ID="MaskedEditExtender1" runat="server" TargetControlID="txtdeldate"
                                            Mask="99/99/9999" MessageValidatorTip="true" MaskType="Date" DisplayMoney="Left"
                                            AcceptNegative="Left" ErrorTooltipEnabled="True" CultureName="en-GB" />
                                           <asp:Label ID="Label2" runat="server" ForeColor="Green" Text="(DD/MM/YYYY)"></asp:Label>
                                    <div class="clear"></div>
                                </div>
                                <div class="form-sec-row">
                                    <label><strong>Agency Bill Amount:</strong></label>  
                                     <asp:TextBox ID="txtAgencyAmt" runat="server" CssClass="textbox-medium1" Text="0.00" style="text-align:right;" ></asp:TextBox>
                                    <div class="clear"></div>
                                </div>
                                <div class="form-sec-row">
                                    <label><strong>Party Bill Amount:</strong></label>  
                                     <asp:TextBox ID="txtPatientAmt" runat="server" CssClass="textbox-medium1" style="text-align:right;" Text="0.00"></asp:TextBox>
                                    <div class="clear"></div>
                                </div>

                                <div class="form-sec-row" style="display:none"> 
                                    <label><strong>Type of Payment:</strong></label>
                                            <asp:DropDownList ID="ddlPaymentMode" runat="server" CssClass="textbox-medium1" OnSelectedIndexChanged="ddlPaymentMode_SelectedIndexChanged" AutoPostBack="true">
                                                <asp:ListItem Value="C">Cash</asp:ListItem>
                                                <asp:ListItem Value="B">Bank</asp:ListItem>
                                                <asp:ListItem Value="R">Card</asp:ListItem>
                                                <asp:ListItem Value="O">Other</asp:ListItem>
                                            </asp:DropDownList>
                                    <div class="clear"></div>
                                </div>

                                <div class="form-sec-row" runat="server" id="divBank">
                                    <label id="lblbankNm" runat="server"><strong>Bank Name :</strong></label>
                                    <asp:TextBox ID="txtBankName" runat="server" CssClass="textbox-medium1" ></asp:TextBox>
                                    <div class="clear"></div>
                                </div>

                                <div class="form-sec-row" runat="server" id="divchqno">
                                    <label id="lblchqno" runat="server"><strong>Cheque No :</strong></label>
                                    <asp:TextBox ID="txtChequeNo" runat="server" CssClass="textbox-medium1" ></asp:TextBox>
                                    <div class="clear"></div>
                                </div>
            
                                <div class="form-sec-row" runat="server" id="divchqdt">
                                    <label id="lblchqdt" runat="server"><strong>Cheque Date :</strong></label>
                                    <asp:TextBox ID="txtchqdt" runat="server" CssClass="textbox-medium1" ></asp:TextBox>
                                    <cc1:MaskedEditExtender ID="MaskedEditExtender3" runat="server" TargetControlID="txtchqdt"
                                                                Mask="99/99/9999" MessageValidatorTip="true" MaskType="Date" DisplayMoney="Left"
                                                                AcceptNegative="Left" ErrorTooltipEnabled="True" CultureName="en-GB" />
              
                                             <asp:Label ID="Label4" runat="server" ForeColor="Green" Text="(DD/MM/YYYY)"></asp:Label>
                                    <asp:HiddenField ID="hdnvchno" runat="server" />
                                    <div class="clear"></div>
                                </div>

                                <div class="form-sec-row" runat="server" id="cncldiv1">
                                    <label id="Label5" runat="server"><strong>Cancel Requisition :</strong></label>
                                    <asp:CheckBox runat="server" ID="chkCancel" />
                                    <div class="clear"></div>
                                </div>

                                <div class="form-sec-row" runat="server" id="cncldiv2">
                                    <label><strong>Reason for cancel :</strong></label>
                                    <asp:TextBox ID="txtcancelReason" runat="server" CssClass="textbox-medium1"  TextMode="MultiLine" Height="30px" ClientIDMode="Static"></asp:TextBox>
                                    <div class="clear"></div>
                                </div>
                                <div class="form-sec-row">
                                    <label><strong>&nbsp;</strong></label>
                                    <asp:Button ID="btnSave" runat="server" Height="28px" OnClick="btnSave_Click" Text="Submit" CssClass="submit-button" />
                                    <asp:Button ID="btnUpdate" runat="server" Height="28px" Text="Update" CssClass="submit-button" OnClick="btnUpdate_Click" />
                                    <asp:Button ID="btnCancel" runat="server" Text="Cancel"  Height="28px" OnClick="btnCancel_Click" CssClass="submit-button" />
                                    <asp:Button ID="Button3" runat="server"  Text="Requisition Slip"  Height="28px" CssClass="submit-buttonCheck" onclick="Button3_Click" />
                                    <div class="clear"></div>
                                </div>
                                <div class="error">
                                    <strong><asp:Label ID="lblError" runat="server" Font-Bold="True"></asp:Label>
                                    </strong>
                                    <div class="clear"></div>
                                </div>
                            </div>
                                <div width="100%" align="center">
          <table width="" >    <tr>        
                        <td style="width: 100%">   <div id='mydiv'>            
                            <asp:Literal ID="ltrReport" runat="server"></asp:Literal><br />   </div>                 
                        </td>
                    </tr>

                     <tr>
                        <td  colspan="3" style="text-align:center">
                            <asp:Button ID="Button6" runat="server" Text="Back" Font-Size="X-Small"  
                                Width="70px" onclick="Button6_Click"/>
                            <asp:Button ID="Button7" runat="server" Font-Size="X-Small"  Width="70px" Text="Print" OnClientClick="javascript:printDiv('mydiv')" />
      </td>
                    </tr>
                    </table>
</div>
                            
                        </asp:View>
                        <asp:View ID="View2" runat="server">
                            <div class="listing"   style='width:100%; height:800px; overflow:auto;'>
                                <asp:GridView id="GridView1"  CssClass="grid" PagerStyle-CssClass="pgr" DataKeyNames ="SrlNo" runat="server" AutoGenerateColumns="False"
                                AllowPaging="True" PageSize ="100" OnPageIndexChanging="GridView1_PageIndexChanging"   SelectedRowStyle-BackColor="GreenYellow"
                                OnRowCommand="GridView1_RowCommand" Width="100%" OnRowDeleting="GridView1_RowDeleting">
                                <RowStyle HorizontalAlign="Center" />
                                    <Columns>
                                        <asp:TemplateField Visible="false">
                                            <ItemTemplate>
                                                <asp:Label ID="lblSrlno" runat="server" Text='<%# Bind("SrlNo") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Registration No" ItemStyle-HorizontalAlign="Left">
                                            <ItemTemplate><asp:Label ID="lblvchno" runat="server" Text='<%# Bind("VchNo") %>' Visible="false"></asp:Label>
                                                <asp:Label ID="lblregno" runat="server" Text='<%# Bind("RegistrationNo") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Requisition No"  ItemStyle-HorizontalAlign="Left">
                                            <ItemTemplate>
                                                <asp:Label ID="lblreqno" runat="server" Text='<%# Bind("RequisitionNo") %>'></asp:Label>
                                                
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Patient Name"  ItemStyle-HorizontalAlign="Left">
                                            <ItemTemplate>
                                                <asp:Label ID="lblname" runat="server" Text='<%# Bind("PName") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Doctor Name"  ItemStyle-HorizontalAlign="Left">
                                            <ItemTemplate>
                                                <asp:Label ID="lblrefname" runat="server" Text='<%# Bind("doc_name") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Sample Collected"  ItemStyle-HorizontalAlign="Left">
                                            <ItemTemplate>
                                                <asp:Label ID="lblsample" runat="server" Text='<%# Bind("SName") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Agency Name"  ItemStyle-HorizontalAlign="Left">
                                            <ItemTemplate>
                                                <asp:Label ID="lblAgency" runat="server" Text='<%# Bind("AgencyName") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Unit" ItemStyle-HorizontalAlign="Left">
                                            <ItemTemplate>
                                                <asp:Label ID="lblunit" runat="server" Text='<%# Bind("Unit") %>'></asp:Label></ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Collected Date"  ItemStyle-HorizontalAlign="Left">
                                            <ItemTemplate>
                                                <asp:Label ID="lblcollectdt" runat="server" Text='<%# Bind("ColDate") %>'></asp:Label>
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
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
