<%@ Page Language="C#" MasterPageFile="~/MasterAll/MasterPageAll.master" AutoEventWireup="true" CodeFile="PatientRequisitionOPD.aspx.cs" Inherits="Pathology_PatientRequisitionOPD" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
 
    <script type="text/javascript" language="javascript">

        function ShowDialog() {
            //var rtvalue = window.open("RegistrationPopup.aspx", "sss", "Width:800px; Height:550px; dialogLeft:250px;");
            var rtvalue = window.open("../OPD/PtientDetailspopup.aspx", "sss", "Width:800px; Height:550px; dialogLeft:250px;");
            //document.getElementById("ctl00_ContentPlaceHolder1_txtreg").value = rtvalue.NameValue;
        }

        function AddNewDoc() {
            window.open("../IPD/doc_master.aspx", "sss", "Width:800px; Height:550px; dialogLeft:250px;");
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

        function ShowDialog1() {

            var rtvalue = window.open("TestPopupMultiple.aspx", "sss", "Width:1050px; Height:550px; dialogLeft:250px;");
            //document.getElementById("ctl00_ContentPlaceHolder1_txttestcode").value = rtvalue.NameValue;
            var a = rtvalue.ProfessionValue.split("#");
            //document.getElementById("ctl00_ContentPlaceHolder1_txttestname").value = a[0];
            //document.getElementById("ctl00_ContentPlaceHolder1_txtcost").value = a[1];
            //document.getElementById("ctl00_ContentPlaceHolder1_txtdueamount").value = a[2];

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

            //$("input[id$='txtdate']").datepicker({ dateFormat: 'dd/mm/yy' });

            //$("input[id$='txtdeldate']").datepicker({ dateFormat: 'dd/mm/yy' });




            $("input[id$='Button1']").click(function () {


                //var text = document.getElementById("ctl00_ContentPlaceHolder1_txtdate").value;
                //var date = Date.parse(text);
                //if (isNaN(date)) {
                //    alert("Invalid Date !");
                //    $("input[id$='txtdate']").focus();
                //    $("input[id$='txtdate']").addClass('textboxerr');
                //    return false;
                //}
                //else {
                //    $("input[id$='txtdate']").removeClass('textboxerr');
                //}


                //if ($("input[id$='txtreqno']").val() == '') {
                //    alert('Please Enter Requisition No !');
                //    $("input[id$='txtreqno']").focus();
                //    $("input[id$='txtreqno']").addClass('textboxerr');
                //    return false;
                //}
                //else {
                //    $("input[id$='txtreqno']").removeClass('textboxerr');
                //}

                //if ($("input[id$='txtage']").val() == '') {
                //    alert('Please Enter Age !');
                //    $("input[id$='txtage']").focus();
                //    $("input[id$='txtage']").addClass('textboxerr');
                //    return false;
                //}
                //else {
                //    $("input[id$='txtage']").removeClass('textboxerr');
                //}



                //if ($("input[id$='txtname']").val() == '') {
                //    alert('Please Enter Name !');
                //    $("input[id$='txtname']").focus();
                //    $("input[id$='txtname']").addClass('textboxerr');
                //    return false;
                //}
                //else {
                //    $("input[id$='txtname']").removeClass('textboxerr');
                //}


                //if ($("input[id$='txtreferal']").val() == '') {
                //    alert('Please Enter Referal Name !');
                //    $("input[id$='txtreferal']").focus();
                //    $("input[id$='txtreferal']").addClass('textboxerr');
                //    return false;
                //}
                //else {
                //    $("input[id$='txtreferal']").removeClass('textboxerr');
                //}


                //if ($("input[id$='txtaddress']").val() == '') {
                //    alert('Please Enter Address !');
                //    $("input[id$='txtaddress']").focus();
                //    $("input[id$='txtaddress']").addClass('textboxerr');
                //    return false;
                //}
                //else {
                //    $("input[id$='txtaddress']").removeClass('textboxerr');
                //}



                //if ($("input[id$='txtph1']").val() == '') {
                //    alert('Please Enter Phone -1  !');
                //    $("input[id$='txtph1']").focus();
                //    $("input[id$='txtph1']").addClass('textboxerr');
                //    return false;
                //}
                //else {
                //    $("input[id$='txtph1']").removeClass('textboxerr');
                //}


                //if ($("input[id$='txttestcode']").val() == '') {
                //    alert('Please Enter Test Code!');
                //    $("input[id$='txttestcode']").focus();
                //    $("input[id$='txttestcode']").addClass('textboxerr');
                //    return false;
                //}
                //else {
                //    $("input[id$='txttestcode']").removeClass('textboxerr');
                //}



                //if ($("input[id$='txttestname']").val() == '') {
                //    alert('Please Enter Test Name !');
                //    $("input[id$='txttestname']").focus();
                //    $("input[id$='txttestname']").addClass('textboxerr');
                //    return false;
                //}
                //else {
                //    $("input[id$='txttestname']").removeClass('textboxerr');
                //}



                //if ($("input[id$='txtcost']").val() == '') {
                //    alert('Please Enter Test Cost !');
                //    $("input[id$='txtcost']").focus();
                //    $("input[id$='txtcost']").addClass('textboxerr');
                //    return false;
                //}
                //else {
                //    $("input[id$='txtcost']").removeClass('textboxerr');
                //}


                //if ($("input[id$='txtdate']").val() == '') {
                //    alert('Please Enter Test Date!');
                //    $("input[id$='txtdate']").focus();
                //    $("input[id$='txtdate']").addClass('textboxerr');
                //    return false;
                //}
                //else {
                //    $("input[id$='txtdate']").removeClass('textboxerr');
                //}


                //if ($("input[id$='txtdeldate']").val() == '') {
                //    alert('Please Enter Delivery Date !');
                //    $("input[id$='txtdeldate']").focus();
                //    $("input[id$='txtdeldate']").addClass('textboxerr');
                //    return false;
                //}
                //else {
                //    $("input[id$='txtdeldate']").removeClass('textboxerr');
                //}
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
            document.getElementById("ctl00_ContentPlaceHolder1_txtreferalname").value = regname[1];
            document.getElementById("ctl00_ContentPlaceHolder1_txtreferal").value = regname[0];

            $("#txtreferal").focus();
            //$("#DropDownList4").val(regname[1]);
        }
</script>

    <script type="text/javascript">
        function calcpayableAmt() {
            //debugger;
            //alert("Hi");
            var testcost = document.getElementById("ctl00_ContentPlaceHolder1_txtcost").value
            var reptcost = document.getElementById("ctl00_ContentPlaceHolder1_txtReptAmt").value
            //alert(testcost);
            var discamt = document.getElementById("ctl00_ContentPlaceHolder1_txtDiscAmt").value
            
            if (testcost == "") {
                testcost = "0";
            }
            if (reptcost == "") {
                reptcost = "0";
            }
            if (discamt == "") {
                discamt = "0";
            }
            var payableamt = parseFloat(testcost) + parseFloat(reptcost) - parseFloat(discamt)
            document.getElementById("ctl00_ContentPlaceHolder1_txtPayableAmt").value = payableamt.toFixed("2");
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">


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
    <div id="h1">
     <div class="pageheader">
         <asp:Label ID="Label1" runat="server">Patient Requisition</asp:Label>
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
        
     
            <asp:HiddenField ID="TextBox4" runat="server" /> <asp:HiddenField ID="HiddenField1" runat="server" />
			<div class="form-sec-row">
                <label><strong>Registration No :<span style="color:red;">*</span></strong></label> 
               <asp:TextBox ID="txtreg" runat="server" CssClass="textbox-medium1" ReadOnly="true" ClientIDMode="Static"></asp:TextBox> 
                <asp:Button ID="ButtonSrch" runat="server" 
                    CssClass="submit-search" Height="28px"  Text="Quick Search" OnClientClick="ShowDialog()" Visible="false" />
                   <asp:Button ID="Button5" runat="server" CssClass="submit-button"  Height="28px" 
                    Text="Fetch" onclick="Button5_Click" Visible="false"/>
                <div class="clear">  </div>
            </div>
            <div class="form-sec-row">
                <label><strong>Requisition No :<span style="color:red;">*</span></strong></label>
                <asp:TextBox ID="txtreqno" runat="server" CssClass="textbox-medium1" Enabled="False" 
                   ></asp:TextBox>
                <div class="clear"></div>
            </div>
           
            <div class="form-sec-row">
                <label><strong>Patient Name :</strong></label>
                <asp:TextBox ID="txtname" runat="server" CssClass="textbox-medium1" 
                   ></asp:TextBox>
                <div class="clear"></div>
            </div>
            <div class="form-sec-row">
                <label><strong>Spouse Name :</strong></label>
                <asp:TextBox ID="txtSpouse" runat="server" CssClass="textbox-medium1" 
                   ></asp:TextBox>
                <div class="clear"></div>
            </div>
            <div class="form-sec-row">
                <label><strong>Guardian Name :</strong></label>
                <asp:TextBox ID="txtGuardian" runat="server" CssClass="textbox-medium1" 
                   ></asp:TextBox>
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

                   <div class="form-sec-row" style="display:none;">
                <label><strong>Address - 2:</strong></label>  
                 <asp:TextBox ID="txtaddress2" runat="server" CssClass="textbox-medium1" ></asp:TextBox>
                <div class="clear"></div>
            </div>
            <div class="form-sec-row">
                <label><strong>Contact No(Primary) :</strong></label>
               <%--   <asp:TextBox ID="txtph1" runat="server" CssClass="textbox-medium1" MaxLength="14"
                   ></asp:TextBox>--%>
                    <asp:TextBox ID="TextBox5" runat="server" CssClass="textbox-medium1" Width="50" Enabled="False">+91</asp:TextBox>
                <asp:TextBox ID="txtph1" runat="server" CssClass="textbox-medium1" 
                    MaxLength="10" Width="246px" ></asp:TextBox>
                <div class="clear"></div>
            </div>
            <div class="form-sec-row"  style="display:none;">
                <label><strong>Phone-2 :</strong></label>
              <%--   <asp:TextBox ID="txtph2" runat="server" CssClass="textbox-medium1"  MaxLength="14"
                   ></asp:TextBox>--%>
                      <asp:TextBox ID="TextBox1" runat="server" CssClass="textbox-medium1" Width="50" Enabled="False">+91</asp:TextBox>
                <asp:TextBox ID="txtph2" runat="server" CssClass="textbox-medium1" 
                    MaxLength="10" Width="246px" ></asp:TextBox>
                <div class="clear"></div>
            </div>

             <div class="form-sec-row"  style="display:none;">
                <label><strong>Email Id:</strong></label>  
                 <asp:TextBox ID="TextBox2" runat="server" CssClass="textbox-medium1" ></asp:TextBox>
                <div class="clear"></div>
            </div>
         <div class="form-sec-row" style="display:none;">
                <label><strong>Date of Appointment :</strong></label>
                 <asp:TextBox ID="txtdate" runat="server" CssClass="textbox-medium1" TextMode="Date"></asp:TextBox>
                       
                <div class="clear"></div>
            </div>
         <div class="form-sec-row" style="display:none;">
                <label><strong>Time of Appointment :</strong></label>
                 <asp:TextBox ID="txttime" runat="server" CssClass="textbox-medium1" TextMode="Time"></asp:TextBox>
                       
                <div class="clear"></div>
         </div>
         <div class="form-sec-row" style="display:none;">
                <label><strong>Department :</strong></label>
                <asp:DropDownList ID="ddlDept" runat="server" CssClass="textbox-medium1" OnSelectedIndexChanged="ddlDept_SelectedIndexChanged" AutoPostBack="true">
                </asp:DropDownList>
                <div class="clear"></div>
         </div>
         
         
         <div class="form-sec-row">
                <label><strong>Referred By Doctor :</strong></label>
             <div style="display:none;">
                 <asp:TextBox ID="txtreferal" runat="server"  CssClass="textbox-medium1"></asp:TextBox>
             </div>
                <asp:TextBox ID="txtreferalname" runat="server"  CssClass="textbox-medium1"></asp:TextBox>
                <cc1:AutoCompleteExtender ServiceMethod="Searchdoc" OnClientItemSelected="autoCompleteEx_ItemSelected" MinimumPrefixLength="1"
                                    CompletionInterval="100" EnableCaching="false" CompletionSetCount="10" TargetControlID="txtreferalname" ID="AutoCompleteExtender2" runat="server" 
                                       FirstRowSelected = "false" ></cc1:AutoCompleteExtender>
             <div style='float:left; padding-left:1px;'><asp:Button ID="btnaddDoc" runat="server"  Height="30px" OnClientClick="AddNewDoc()"
                    CssClass="submit-buttonCheck" Text="Add New Doctor" /></div>
                <div class="clear"></div>
            </div>
         <div class="form-sec-row"> 
                <label><strong>Service Group:</strong></label>
                        <asp:DropDownList ID="ddltestGroup" runat="server" CssClass="textbox-medium1">
                        </asp:DropDownList>
                <div class="clear"></div>
            </div>
         <asp:UpdatePanel runat="server" ID="upd1">
             <ContentTemplate>

             
            <div class="form-sec-row" style="display:none;">
    
                <label><strong>Service Code :</strong></label> 
                  <div style='float:left;'>
                <asp:TextBox ID="txttestcode" runat="server" CssClass="textbox-medium1"  TextMode="MultiLine" Height="30px" ReadOnly="true" Visible="false"></asp:TextBox>
                      
                      <asp:HiddenField ID="txtcurtest" runat="server" />
                  </div>
                      <div style='float:left; padding-left:1px;display:none;'><asp:Button ID="Button8" runat="server"  Height="30px"
                    CssClass="submit-search" Text="Add Service" OnClick="Button8_Click" /></div>
                <div class="clear"></div>
            </div>

            <div class="form-sec-row">
                <label><strong>Service Name :</strong></label>
                <asp:TextBox ID="txttestname" runat="server" CssClass="textbox-medium1"  TextMode="MultiLine" Height="30px" ReadOnly="true" Visible="false"></asp:TextBox>
                <asp:DropDownList ID="ddltestCode" runat="server" CssClass="textbox-medium1" AutoPostBack="true" OnSelectedIndexChanged="ddltestCode_SelectedIndexChanged">
                        </asp:DropDownList>
                <div class="clear"></div>
            </div>
      
            

           <div class="form-sec-row" style="display:none;">
                <label><strong>Performing Doctor :</strong></label>
                 <asp:TextBox ID="txtconsultant" runat="server" CssClass="textbox-medium1" ReadOnly="true" ClientIDMode="Static" style="display:none;"></asp:TextBox>
                 <asp:TextBox ID="txtconsultantname" runat="server" CssClass="textbox-medium1" ReadOnly="true" ClientIDMode="Static" ></asp:TextBox>
                <div class="clear"></div>
           </div>
          <div class="form-sec-row">
                <label><strong>Consultant Type :</strong></label>
                <%--<asp:TextBox ID="txtunderdoc" runat="server"  CssClass="textbox-medium1" Enabled="false"></asp:TextBox>--%>
                <asp:DropDownList ID="ddldocType" runat="server" CssClass="textbox-medium1" OnSelectedIndexChanged="ddldocType_SelectedIndexChanged" AutoPostBack="true">
                        </asp:DropDownList>
                <div class="clear"></div>
            </div>
         <div class="form-sec-row">
                <label><strong>Consultant :</strong></label>
                <%--<asp:TextBox ID="txtunderdoc" runat="server"  CssClass="textbox-medium1" Enabled="false"></asp:TextBox>--%>
                <asp:DropDownList ID="ddlDoctor" runat="server" CssClass="textbox-medium1" OnSelectedIndexChanged="ddlDoctor_SelectedIndexChanged" AutoPostBack="true">
                        </asp:DropDownList>
                <div class="clear"></div>
            </div>
           <div class="form-sec-row">
                <label><strong>Service Cost :</strong></label>
                 <asp:TextBox ID="txtcost" runat="server" CssClass="textbox-medium1" ReadOnly="true"></asp:TextBox>
                <div class="clear"></div>
            </div>
            <div class="form-sec-row">
                <label><strong>Report Review Charge :</strong></label>
                 <asp:TextBox ID="txtReptAmt" runat="server" CssClass="textbox-medium1" OnChange="calcpayableAmt();" Text="0.00"></asp:TextBox>
                <div class="clear"></div>
            </div>
            <div class="form-sec-row">
                <label><strong>Discount Amount :</strong></label>
                 <asp:TextBox ID="txtDiscAmt" runat="server" CssClass="textbox-medium1" OnChange="calcpayableAmt();" Text="0.00"></asp:TextBox>
                <div class="clear"></div>
            </div>
            <div class="form-sec-row">
                <label><strong>Payable Amount :</strong></label>
                 <asp:TextBox ID="txtPayableAmt" runat="server" CssClass="textbox-medium1" ReadOnly="true" ></asp:TextBox>
                <div class="clear"></div>
            </div>
         
         <div class="form-sec-row"  style="display:none;">
                <label><strong>New Test Cost :</strong></label>
                 <asp:TextBox ID="txtNewCost" runat="server" CssClass="textbox-medium1" ReadOnly="true" ClientIDMode="Static"></asp:TextBox>
                <div class="clear"></div>
            </div>

            
             <div class="form-sec-row" style="display:none;">
                <label><strong>Delivery Date :</strong></label>
                 <asp:TextBox ID="txtdeldate" runat="server" CssClass="textbox-medium1"></asp:TextBox>
                       
                <div class="clear"></div>
            </div>

            <%--  <div class="form-sec-row">
                <label><strong>Prev. Advanced Amount :</strong></label>
                 <asp:TextBox ID="TextBox3" runat="server" Enabled="false"  CssClass="textbox-medium1"></asp:TextBox>
                <div class="clear"></div>
            </div>--%>
            
            <div class="form-sec-row" style="display:none" >
                <label><strong>Previous Advanced Amount :</strong></label>
                 <asp:TextBox ID="txtpreadvamount" runat="server" CssClass="textbox-medium1" ReadOnly="true" ClientIDMode="Static" ></asp:TextBox>
                <div class="clear"></div>
            </div>
            <div class="form-sec-row" style="display:none;">
                <label><strong>Due Amount :</strong></label>
                 <asp:TextBox ID="txtdueamount" runat="server" CssClass="textbox-medium1" ReadOnly="true" ClientIDMode="Static"></asp:TextBox>
                <div class="clear"></div>
            </div>
            <div class="form-sec-row" style="display:none">
                <label><strong>Amount Paid:</strong></label>
                 <asp:TextBox ID="txtadvamount" runat="server" CssClass="textbox-medium1"></asp:TextBox>
                <div class="clear"></div>
            </div>

            <div class="form-sec-row" style="display:none;"> 
                <label><strong>Type of Payment:</strong></label>
                        <asp:DropDownList ID="ddlPaymentMode" runat="server" CssClass="textbox-medium1" OnSelectedIndexChanged="ddlPaymentMode_SelectedIndexChanged" AutoPostBack="true">
                            <asp:ListItem Value="C">Cash</asp:ListItem>
                            <asp:ListItem Value="B">Bank</asp:ListItem>
                            <asp:ListItem Value="R">Card</asp:ListItem>
                            <asp:ListItem Value="O">Other</asp:ListItem>
                        </asp:DropDownList>
                <div class="clear"></div>
            </div>

            <div class="form-sec-row" runat="server" id="divBank" style="display:none;">
                <label id="lblbankNm" runat="server"><strong>Bank Name :</strong></label>
                <asp:TextBox ID="txtBankName" runat="server" CssClass="textbox-medium1" ></asp:TextBox>
                <div class="clear"></div>
            </div>

            <div class="form-sec-row" runat="server" id="divchqno" style="display:none;">
                <label id="lblchqno" runat="server"><strong>Cheque No :</strong></label>
                <asp:TextBox ID="txtChequeNo" runat="server" CssClass="textbox-medium1" ></asp:TextBox>
                <div class="clear"></div>
            </div>
            
            <div class="form-sec-row" runat="server" id="divchqdt" style="display:none;">
                <label id="lblchqdt" runat="server"><strong>Cheque Date :</strong></label>
                <asp:HiddenField ID="hdnvchno" runat="server" />
                <asp:TextBox ID="txtchqdt" runat="server" CssClass="textbox-medium1"></asp:TextBox>
                <cc1:MaskedEditExtender ID="MaskedEditExtender3" runat="server" TargetControlID="txtchqdt"
                                            Mask="99/99/9999" MessageValidatorTip="true" MaskType="Date" DisplayMoney="Left"
                                            AcceptNegative="Left" ErrorTooltipEnabled="True" CultureName="en-GB" />
              
                         <asp:Label ID="Label4" runat="server" ForeColor="Green" Text="(DD/MM/YYYY)"></asp:Label>
                <div class="clear"></div>
            </div>

            <div class="form-sec-row" runat="server" id="cncldiv1" visible="false">
                <label id="Label5" runat="server"><strong>Cancel Requisition :</strong></label>
                <asp:CheckBox runat="server" ID="chkCancel" />
                <div class="clear"></div>
            </div>

            <div class="form-sec-row" runat="server" id="cncldiv2" visible="false">
                <label><strong>Reason for cancel :</strong></label>
                <asp:TextBox ID="txtcancelReason" runat="server" CssClass="textbox-medium1"  TextMode="MultiLine" Height="30px" ClientIDMode="Static"></asp:TextBox>
                <div class="clear"></div>
            </div>
            </ContentTemplate>
         </asp:UpdatePanel>
            <div class="form-sec-row">
                <label><strong>&nbsp;</strong></label>
                <asp:Button ID="Button1" runat="server" Height="28px" OnClick="Button1_Click" Text="Submit" CssClass="submit-button" />
                <asp:Button ID="Button2" runat="server" Text="Cancel"  Height="28px"  OnClick="Button2_Click" CssClass="submit-button" />
                <asp:Button ID="Button3" runat="server"  Text="Requisition Slip"  Height="28px" CssClass="submit-buttonCheck" onclick="Button3_Click" />
                <asp:Button ID="Button4" runat="server" Text="Prescription" Height="28px" CssClass="submit-buttonCheck"  OnClick="Button4_Click" /> 
                <asp:Button ID="Button9" runat="server" Text="Pay Bill" Height="28px" CssClass="submit-buttonCheck" OnClick="Button9_Click"/>
                <div class="clear"></div>
            </div>  
         <div class="form-sec-row">
             <label><strong>&nbsp;</strong></label>
             <div id="divOpt" runat="server" visible="false">
                <asp:RadioButtonList ID="RadioButtonList1" runat="server"  Width="250px"
                    RepeatDirection="Horizontal">
                    <asp:ListItem Value="N" Text="Without Header" Selected="True"></asp:ListItem>
                    <asp:ListItem Value="Y" Text="With Header"></asp:ListItem>
                </asp:RadioButtonList>
            </div>
             <div class="clear"></div>
        </div>
         
            
          <div class="error">
                <strong><asp:Label ID="lblError" runat="server" Font-Bold="True"></asp:Label>
                </strong>
                <div class="clear"></div>
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
     </div>
     </asp:View>
  <asp:View ID="View2" runat="server">
      <table width="100%">
          <tr>
                <td width="7%">
                   <label class="pname"><strong>Reg No :</strong></label> 
                </td>
                  <td  width="20%">
                      <asp:TextBox ID="txtregSrch" runat="server" CssClass="textbox-medium1" Width="100%" ></asp:TextBox>  <div class="clear"></div>
                  </td>
                <td width="7%">
                    <label class="pname"><strong>Name :</strong></label> 
                 
                </td>
              <td width="20%">
                  <asp:TextBox ID="txtnameSrch" runat="server" CssClass="textbox-medium1" Width="100%" ></asp:TextBox>  <div class="clear"></div>
              </td>
                <td width="7%">
                    <label class="pname"><strong>Ph No :</strong></label> 
                    
                </td>
              <td width="10%">
                  <asp:TextBox ID="txtphSrch" runat="server" CssClass="textbox-medium1" Width="100%" ></asp:TextBox>  <div class="clear"></div>
              </td>
                <td width="7%">
                    <div class="form-sec-row"> 
                    <label class="pname"><strong>Reg Date :</strong></label> 
                    </div>
                </td>
                <td width="10%">
                    <asp:TextBox ID="txtRegDate" runat="server" CssClass="textbox-medium1" Width="138px" TextMode="Date"></asp:TextBox>
                </td>
                <td width="10%">
                    <div class="form-sec-row"> 
                    <asp:Button ID="Button10" runat="server" Text="Search" Height="28px"  CssClass="submit-button1" onclick="Button10_Click" />
                    </div>                  
                </td>             
                      
            </tr>
      </table>
     <div class="listing"   style='width:100%; height:800px; overflow:auto;'>
         <asp:GridView id="GridView1"  CssClass="grid" PagerStyle-CssClass="pgr" DataKeyNames ="RequisitionNo" runat="server" AutoGenerateColumns="False"
          AllowPaging="True" PageSize ="100" 
                 OnPageIndexChanging="GridView1_PageIndexChanging"   SelectedRowStyle-BackColor="GreenYellow"
                 OnRowCommand="GridView1_RowCommand" 
             OnRowDeleting="GridView1_RowDeleting" Width="100%">
                <RowStyle HorizontalAlign="Center" />
                <Columns>
                    <asp:TemplateField HeaderText="Registration No"><ItemTemplate><asp:Label ID="lblregno" runat="server" Text='<%# Bind("RegistrationNo") %>'></asp:Label></ItemTemplate></asp:TemplateField>

                      <asp:TemplateField HeaderText="Requisition No" >
                          <ItemTemplate>
                              <asp:Label ID="lblreqno" runat="server" Text='<%# Bind("RequisitionNo") %>'></asp:Label>
                              <asp:Label ID="lblvchno" runat="server" Text='<%# Bind("VchNo") %>'  Visible="false"></asp:Label>
                          </ItemTemplate>

                      </asp:TemplateField>

                    <asp:TemplateField HeaderText="Patient Name"><ItemTemplate><asp:Label ID="lblname" runat="server" Text='<%# Bind("PatientName") %>'></asp:Label></ItemTemplate></asp:TemplateField>
                    <asp:TemplateField HeaderText="Referal Name"><ItemTemplate><asp:Label ID="lblrefname" runat="server" Text='<%# Bind("ReferalName") %>'></asp:Label></ItemTemplate></asp:TemplateField>
                    
                    <asp:TemplateField HeaderText="Address"><ItemTemplate><asp:Label ID="lbladdress" runat="server" Text='<%# Bind("Address") %>'></asp:Label></ItemTemplate></asp:TemplateField> 
                       
                    <asp:TemplateField HeaderText="Phone - 1"><ItemTemplate><asp:Label ID="lblphone1" runat="server" Text='<%# Bind("Ph1") %>'></asp:Label></ItemTemplate></asp:TemplateField>
                      <asp:TemplateField HeaderText="Phone - 2" ><ItemTemplate><asp:Label ID="lblphone2" runat="server" Text='<%# Bind("Ph2") %>'></asp:Label></ItemTemplate></asp:TemplateField>
                 <%--   <asp:TemplateField HeaderText="Test Name">
                        <ItemTemplate>                        
                            <asp:Label ID="lbltestname" runat="server" Text='<%# Bind("TestName") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>--%>
                    <asp:TemplateField HeaderText="Test Date"><ItemTemplate><asp:Label ID="lbltestdate" runat="server" Text='<%# Bind("tdate") %>'></asp:Label></ItemTemplate></asp:TemplateField>

                     <asp:TemplateField HeaderText="Delivery Date"><ItemTemplate><asp:Label ID="lbldeldate" runat="server" Text='<%# Bind("ddate") %>'></asp:Label></ItemTemplate></asp:TemplateField>
                    <asp:TemplateField HeaderText="Advance"><ItemTemplate><asp:Label ID="lblamt" runat="server" Text='<%# Bind("adv_amt") %>'></asp:Label></ItemTemplate></asp:TemplateField>    
                                
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