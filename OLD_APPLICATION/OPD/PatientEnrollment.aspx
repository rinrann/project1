<%@ Page Title="" Language="C#" MasterPageFile="~/MasterAll/MasterPageAll.master" AutoEventWireup="true" CodeFile="PatientEnrollment.aspx.cs" Inherits="OPD_PatientEnrollment" MaintainScrollPositionOnPostback="true" %>
 <%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
 
    
    <script type="text/javascript">
        function getCurrentState() {

            var cntryCode = $("#ctl00_ContentPlaceHolder1_ddlCountryPresent").val();
            var ddlState = $("#ctl00_ContentPlaceHolder1_ddlStatePresent");
            //alert(cntryCode);

            $.ajax({
                type: "POST",
                url: "PatientEnrollment.aspx/GetStateList",
                //url: "Rep_Investwisecollection.aspx/SearchServiceName",
                data: "{ cntryCode: '" + cntryCode + "'}",
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                async: "true",
                cache: "false",
                success: function (res) {
                    //alert(res);
                    var datas = res.d.split('~');
                    //alert(datas);
                    ddlState.empty().append('<option selected="selected" value="">--Select--</option>');
                    for (var x = 0; x < datas.length; x++) {
                        //alert(datas[x]);
                        var testname = datas[x].split('/');
                        //markup = markup + "<tr><td width='100%'><a href='#' onclick='setvalue(" + testname[0] + ")'>" + testname[1] + "</a></td></tr>";
                        ddlState.append($("<option></option>").val(testname[0]).html(testname[1]));
                    }
                    
                }
            });
            
        }
    </script>

    <script type="text/javascript">
        function getCurrentDistrict() {

            var stateCode = $("#ctl00_ContentPlaceHolder1_ddlStatePresent").val();
            var ddlDistrictPresent = $("#ctl00_ContentPlaceHolder1_ddlDistrictPresent");
            //alert(cntryCode);

            $.ajax({
                type: "POST",
                url: "PatientEnrollment.aspx/GetDistrictList",
                data: "{ stateCode: '" + stateCode + "'}",
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                async: "true",
                cache: "false",
                success: function (res) {
                    //alert(res);
                    var datas = res.d.split('~');
                    //alert(datas);
                    ddlDistrictPresent.empty().append('<option selected="selected" value="">--Select--</option>');
                    for (var x = 0; x < datas.length; x++) {
                        //alert(datas[x]);
                        var testname = datas[x].split('/');
                        //markup = markup + "<tr><td width='100%'><a href='#' onclick='setvalue(" + testname[0] + ")'>" + testname[1] + "</a></td></tr>";
                        ddlDistrictPresent.append($("<option></option>").val(testname[0]).html(testname[1]));
                    }

                }
            });

        }
    </script>

    <script type="text/javascript">
        function getParmanentState() {

            var cntryCode = $("#ctl00_ContentPlaceHolder1_ddlCountryPermanent").val();
            var ddlState = $("#ctl00_ContentPlaceHolder1_ddlStatePermanent");
            //alert(cntryCode);

            $.ajax({
                type: "POST",
                url: "PatientEnrollment.aspx/GetStateList",
                //url: "Rep_Investwisecollection.aspx/SearchServiceName",
                data: "{ cntryCode: '" + cntryCode + "'}",
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                async: "true",
                cache: "false",
                success: function (res) {
                    //alert(res);
                    var datas = res.d.split('~');
                    //alert(datas);
                    ddlState.empty().append('<option selected="selected" value="">--Select--</option>');
                    for (var x = 0; x < datas.length; x++) {
                        //alert(datas[x]);
                        var testname = datas[x].split('/');
                        //markup = markup + "<tr><td width='100%'><a href='#' onclick='setvalue(" + testname[0] + ")'>" + testname[1] + "</a></td></tr>";
                        ddlState.append($("<option></option>").val(testname[0]).html(testname[1]));
                    }

                }
            });

        }
    </script>


    <script type="text/javascript">
        function getParmanentDistrict() {

            var stateCode = $("#ctl00_ContentPlaceHolder1_ddlStatePermanent").val();
            var ddlDistrict = $("#ctl00_ContentPlaceHolder1_ddlDistrictPermanent");
            //alert(cntryCode);

            $.ajax({
                type: "POST",
                url: "PatientEnrollment.aspx/GetDistrictList",
                data: "{ stateCode: '" + stateCode + "'}",
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                async: "true",
                cache: "false",
                success: function (res) {
                    //alert(res);
                    var datas = res.d.split('~');
                    //alert(datas);
                    ddlDistrict.empty().append('<option selected="selected" value="">--Select--</option>');
                    for (var x = 0; x < datas.length; x++) {
                        //alert(datas[x]);
                        var testname = datas[x].split('/');
                        //markup = markup + "<tr><td width='100%'><a href='#' onclick='setvalue(" + testname[0] + ")'>" + testname[1] + "</a></td></tr>";
                        ddlDistrict.append($("<option></option>").val(testname[0]).html(testname[1]));
                    }

                }
            });

        }
    </script>
    <script type="text/javascript" language="javascript">
    function ShowDialog() {

        var rtvalue = window.open("RegistrationPopup.aspx?type=1", "sss", "Width:800px; Height:550px; dialogLeft:250px;");
        //if (rtvalue.NameValue != "") { document.getElementById("ctl00_ContentPlaceHolder1_TextBox2").value = rtvalue.NameValue; }
         //document.getElementById("ctl00_ContentPlaceHolder1_TextBox1").value = rtvalue.ProfessionValue; 

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

        var datetime = day + '/' + mnt + '/' + yr;
        var hour = now.getHours();
        var minute = now.getMinutes();
        var a;
        if (hour >= 12) {
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

        document.getElementById("ctl00_ContentPlaceHolder1_TextBox13").value = time;
        document.getElementById("ctl00_ContentPlaceHolder1_TextBox12").value = datetime;


    }
    function GetBirthDate() {
        debugger;
        var yr = document.getElementById("ctl00_ContentPlaceHolder1_TextBox5").value;
        var mn = document.getElementById("ctl00_ContentPlaceHolder1_TextBox30").value;
        var dy = document.getElementById("ctl00_ContentPlaceHolder1_TextBox31").value;
        if (dy < 10) { dy = '0' + dy } if (mn < 10) { mn = '0' + mn }
        var ddd = new Date(new Date().getFullYear() - yr, new Date().getMonth() - mn, (new Date().getDay() + 11) - dy);
        //var dob = (ddd.getDate()) + "/" + (ddd.getMonth() + 1) + "/" + ddd.getFullYear();
        var yr = ddd.getFullYear();
        var mn = ddd.getMonth() + 1;
        var dd = ddd.getDate();
        var dob = ddd.getFullYear() + "-" + zeropadding((ddd.getMonth() + 1)) + "-" + zeropadding((ddd.getDate()));
        document.getElementById("ctl00_ContentPlaceHolder1_TextBox27").value = dob;
    }
    function zeropadding(chr) {
        debugger;
        var retval;
        if (String(chr).length < 2) {
            retval = "0" + chr;
        }
        else {
            retval = chr;
        }
        return retval;
    }

    function displayDate() {
        debugger;
        var a = document.getElementById("ctl00_ContentPlaceHolder1_TextBox27").value;
        var b = a.split("-");
        //var bday = b[1] + "/" + b[0] + "/" + b[2];
        var bday = b[1] + "/" + b[2] + "/" + b[0];
        var d1 = new Date(bday);   //from date yyyy-MM-dd
        //var d1 = a;
        var d2 = new Date(); //to date yyyy-MM-dd (taken currentdate)
        var Months = d2.getMonth() - d1.getMonth();
        var Years = d2.getFullYear() - d1.getFullYear();
        var Days = d2.getDate() - d1.getDate();
        Months = (d2.getMonth() + 12 * d2.getFullYear()) - (d1.getMonth() + 12 * d1.getFullYear());
        var MonthOverflow = 0;
        if (Months - (Years * 12) < 0)
            MonthOverFlow = -1;
        else
            MonthOverFlow = 1;
        if (MonthOverFlow < 0)
            Years = Years - 1; Months = Months - (Years * 12);
        var LastDayOfMonth = new Date(d2.getFullYear(),
                d2.getMonth() + 1, 0, 23, 59, 59);
        LastDayOfMonth = LastDayOfMonth.getDate();
        if (MonthOverFlow < 0 && (d1.getDate() > d2.getDate())) {
            Days = LastDayOfMonth + (d2.getDate() - d1.getDate()) - 1;
        }
        else
            Days = d2.getDate() - d1.getDate();
        if (Days < 0)
            Months = Months - 1;
        var l = new Date(d2.getFullYear(), d2.getMonth(), 0);
        var l1 = new Date(d1.getFullYear(), d1.getMonth() + 1, 0);
        if (Days < 0) {
            if (l1 > l)
                Days = l1.getDate() + Days;
            else
                Days = l.getDate() + Days;
        }

        document.getElementById("ctl00_ContentPlaceHolder1_TextBox5").value = Years;
        document.getElementById("ctl00_ContentPlaceHolder1_TextBox30").value = Months;
        document.getElementById("ctl00_ContentPlaceHolder1_TextBox31").value = Days;
    }



    function Calling() {

        var date = new Date();
        //$("input[id$='TextBox12']").datepicker({ dateFormat: 'dd/mm/yy' });
        //$("input[id$='TextBox27']").datepicker({ dateFormat: 'dd/mm/yy' });
        $("input[id$='TextBox13']").timepicker({
            showPeriod: true,
            showLeadingZero: true
        });


        $("input[id$='Button1']").click(function () {

            if ($("select[id$='DropDownList1']").val() == '0') {
                alert('Please Select Sex !');
                $("select[id$='DropDownList1']").addClass('textboxerr');
                $("select[id$='DropDownList1']").focus();
                return false;
            }
            else {
                $("select[id$='DropDownList1']").removeClass('textboxerr');
            }


            if ($("select[id$='DropDownList2']").val() == '0') {
                alert('Please Select Doctor Type !');
                $("select[id$='DropDownList2']").addClass('textboxerr');
                $("select[id$='DropDownList2']").focus();
                return false;
            }
            else {
                $("select[id$='DropDownList2']").removeClass('textboxerr');
            }

            if ($("select[id$='DropDownList4']").val() == '0') {
                alert('Please Select Patient Type !');
                $("select[id$='DropDownList4']").addClass('textboxerr');
                $("select[id$='DropDownList4']").focus();
                return false;
            }
            else {
                $("select[id$='DropDownList4']").removeClass('textboxerr');
            }


            if ($("select[id$='DropDownList3']").val() == '0') {
                alert('Please Select Doctor Name !');
                $("select[id$='DropDownList3']").addClass('textboxerr');
                $("select[id$='DropDownList3']").focus();
                return false;
            }
            else {
                $("select[id$='DropDownList3']").removeClass('textboxerr');
            }

            if ($("input[id$='TextBox1']").val() == '') {
                alert('Please Enter Appointment No!');
                $("input[id$='TextBox1']").focus();
                $("input[id$='TextBox1']").addClass('textboxerr');
                return false;
            }
            else {
                $("input[id$='TextBox1']").removeClass('textboxerr');
            }

            if ($("input[id$='TextBox3']").val() == '') {
                alert('Please Enter Name !');
                $("input[id$='TextBox3']").focus();
                $("input[id$='TextBox3']").addClass('textboxerr');
                return false;
            }
            else {
                $("input[id$='TextBox3']").removeClass('textboxerr');
            }


            if ($("input[id$='TextBox4']").val() == '') {
                alert('Please Enter C/O !');
                $("input[id$='TextBox4']").focus();
                $("input[id$='TextBox4']").addClass('textboxerr');
                return false;
            }
            else {
                $("input[id$='TextBox4']").removeClass('textboxerr');
            }


            if ($("input[id$='TextBox5']").val() == '') {
                alert('Please Enter Age !');
                $("input[id$='TextBox5']").focus();
                $("input[id$='TextBox5']").addClass('textboxerr');
                return false;
            }
            else {
                if ($("input[id$='TextBox5']").val().length > 3) {
                    alert('Invalid Age !');
                    alert('Age should be >1 and <150');
                    $("input[id$='TextBox5']").focus();
                    $("input[id$='TextBox5']").addClass('textboxerr');
                    return false;
                }
                else {

                    $("input[id$='TextBox5']").removeClass('textboxerr');
                }

            }
            if ($("input[id$='TextBox7']").val() == '') {
                alert('Please Enter Phone - 1 !');
                $("input[id$='TextBox7']").focus();
                $("input[id$='TextBox7']").addClass('textboxerr');
                return false;
            }
            else {
                $("input[id$='TextBox7']").removeClass('textboxerr');
            }


            if ($("input[id$='TextBox10']").val() == '') {
                alert('Please Enter Address !');
                $("input[id$='TextBox10']").focus();
                $("input[id$='TextBox10']").addClass('textboxerr');
                return false;
            }
            else {
                $("input[id$='TextBox10']").removeClass('textboxerr');
            }

            if ($("input[id$='TextBox12']").val() == '') {
                alert('Please Enter Appointment Date !');
                $("input[id$='TextBox12']").focus();
                $("input[id$='TextBox12']").addClass('textboxerr');
                return false;
            }
            else {
                $("input[id$='TextBox12']").removeClass('textboxerr');
            }


            if ($("input[id$='TextBox13']").val() == '') {
                alert('Please Enter Appointment Time !');
                $("input[id$='TextBox13']").focus();
                $("input[id$='TextBox13']").addClass('textboxerr');
                return false;
            }
            else {
                $("input[id$='TextBox13']").removeClass('textboxerr');
            }

        });

        $("input[id$='TextBox5']").keydown(function (event) {
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

        $("input[id$='TextBox7']").keydown(function (event) {
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

        $("input[id$='TextBox9']").keydown(function (event) {
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

    


<%--For Busy Loader .............................--%>



  <asp:UpdateProgress ID="UpdateProgress1" runat="server">
    <ProgressTemplate>
    
       <div id="aaa" class="progressBackgroundFilter">          </div>
        <div id="bbb" class="processMessage"><img alt="Loading" src="../images/pwait.gif"/>
      
              </div>
    </ProgressTemplate>
    </asp:UpdateProgress>


    <%--For Busy Loader End.............................--%>

    <%--<asp:UpdatePanel ID="UpdatePanel1" runat="server">
    <ContentTemplate>--%>
    <div class="pageheader">
         <asp:Label ID="Label1" runat="server">Patient Registration</asp:Label>
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
            <asp:HiddenField ID="HiddenField1" runat="server" />
                  <div class="form-sec-row" style="display:none;">
                <label><strong>Appointment No :</strong></label>
                <asp:TextBox ID="TextBox1" runat="server" CssClass="textbox-medium1" ReadOnly="true"
                   ></asp:TextBox>
                <div class="clear"></div>
            </div>
			<div class="form-sec-row">
                <label><strong>Registration No :<span style="color:red;">*</span></strong></label>
                 <asp:TextBox ID="TextBox2" runat="server" CssClass="textbox-medium1" Enabled="False" 
                   ></asp:TextBox> 
                <asp:Button ID="Button3" runat="server" Text="Search" Height="28px" CssClass="submit-button" OnClientClick="ShowDialog()"  style="display:none;"/>
                <asp:Button ID="Button4" runat="server" Text="fetch" CssClass ="submit-button"  Height="28px" onclick="Button4_Click"  style="display:none;"/>
                <div class="clear">  </div>
            </div>
           
            <div class="form-sec-row">
                <label><strong>Patient Name :<span style="color:red;">*</span></strong></label>
                 <asp:TextBox ID="TextBox3" runat="server" CssClass="textbox-medium1" 
                   ></asp:TextBox>
                <div class="clear"></div>
            </div>

            <div class="form-sec-row">
                <label><strong>Guardian Name :</strong></label>
                 <asp:TextBox ID="TextBox4" runat="server" CssClass="textbox-medium1" 
                   ></asp:TextBox>
                <div class="clear"></div>
            </div> 
         <div class="form-sec-row">
                <label><strong>Spouse Name :</strong></label>
                 <asp:TextBox ID="txtSpouseName" runat="server" CssClass="textbox-medium1" 
                   ></asp:TextBox>
                <div class="clear"></div>
            </div> 
         <div class="form-sec-row">
                <label><strong>Date of Birth : </strong></label>
                <asp:TextBox ID="TextBox27" runat="server" CssClass="textbox-medium1 datepicker"   TextMode="Date"
                     onchange="displayDate();"></asp:TextBox>

              <%--<cc1:MaskedEditExtender ID="MaskedEditExtender1" runat="server" TargetControlID="TextBox27"
                                            Mask="99/99/9999" MessageValidatorTip="true" MaskType="Date" DisplayMoney="Left"
                                            AcceptNegative="Left" ErrorTooltipEnabled="True" CultureName="en-GB" />--%>

                           <%--<asp:Label ID="Label2" runat="server" ForeColor="Green" Text="(DD/MM/YYYY)"></asp:Label>--%>

                <div class="clear"></div>
            </div>
         <div class="form-sec-row">
                <label><strong>Age :</strong></label>
           <div style='float:left;padding-left:0px;'> <strong>  Year</strong></div><div style='float:left;padding-left:6px;'> 
                <asp:TextBox ID="TextBox5"  MaxLength="3" runat="server" Width="55px"  onchange="GetBirthDate();" ></asp:TextBox></div> 
             <div style=' float:left; padding-left:6px;'> <strong>  Month</strong></div> <div style=' float:left; padding-left:6px;'> 
                <asp:TextBox ID="TextBox30" runat="server"  Width="52px"  MaxLength="2"   onchange="GetBirthDate();" ></asp:TextBox></div>
             <div style=' float:left; padding-left:6px;'>   <strong>  Day</strong></div> <div style=' float:left; padding-left:6px;'> 
                <asp:TextBox ID="TextBox31" runat="server"  MaxLength="2"   Width="52px" onchange="GetBirthDate();" ></asp:TextBox></div>
                <div class="clear"></div>
            </div>
         

            <div class="form-sec-row">
                <label><strong>Sex :<span style="color:red;">*</span></strong></label>
                            <asp:DropDownList ID="DropDownList1" runat="server" CssClass="textbox-medium1">
                            </asp:DropDownList>
                <div class="clear"></div>
            </div>
                 <div class="form-sec-row">
                <label><strong>Contact No Primary:<span style="color:red;">*</span></strong></label>
                <asp:TextBox ID="TextBox6" runat="server" CssClass="textbox-medium1" Width="50" Enabled="False">+91</asp:TextBox>
            
                 <asp:TextBox ID="TextBox7" runat="server" CssClass="textbox-medium1" MaxLength="10" Width="246px" OnTextChanged="TextBox7_TextChanged" AutoPostBack="true"></asp:TextBox>
                <div class="clear"></div>
            </div>
            
            <div class="form-sec-row">
                <label><strong>Alternative Contact No. :</strong></label>
                <asp:TextBox ID="TextBox8" runat="server" CssClass="textbox-medium1" Width="50" Enabled="False">+91</asp:TextBox>
                <asp:TextBox ID="TextBox9" runat="server" CssClass="textbox-medium1" 
                    MaxLength="10" Width="246px" ></asp:TextBox>
                <div class="clear"></div>
            </div>
            <div class="form-sec-row">
                <label><strong>Present Address:</strong></label>  
                 <asp:TextBox ID="TextBox10" runat="server" Height="55px"   CssClass="textbox-medium1" TextMode="MultiLine" 
                   ></asp:TextBox>
                <div class="clear"></div>
            </div>
            <div class="form-sec-row">
                <label><strong>Country :</strong></label>  
                 <%--onChange="getCurrentState()"--%>
                <%--OnSelectedIndexChanged="ddlCountry_SelectedIndexChanged"--%>
                <asp:UpdatePanel ID="UpdCC" runat="server">
                    <ContentTemplate>
                        <asp:DropDownList ID="ddlCountryPresent" runat="server"  CssClass="textbox-medium1" AutoPostBack="true" OnSelectedIndexChanged="ddlCountry_SelectedIndexChanged"></asp:DropDownList>
                    </ContentTemplate>
                </asp:UpdatePanel>
                
                <div class="clear"></div>
            </div>
            <div class="form-sec-row">
                <label><strong>State :</strong></label>  
                 <%--onChange="getCurrentDistrict()"--%>
                <%--OnSelectedIndexChanged="ddlState_SelectedIndexChanged"--%>
                <asp:UpdatePanel ID="UpdCS" runat="server">
                    <ContentTemplate>
                        <asp:DropDownList ID="ddlStatePresent" runat="server" CssClass="textbox-medium1" AutoPostBack="true"  OnSelectedIndexChanged="ddlState_SelectedIndexChanged"></asp:DropDownList>
                    </ContentTemplate>
                </asp:UpdatePanel>
                
                <div class="clear"></div>
            </div>
            <div class="form-sec-row">
                <label><strong>District :</strong></label>  
                 <asp:UpdatePanel ID="UpdCD" runat="server">
                     <ContentTemplate>
                         <asp:DropDownList ID="ddlDistrictPresent" runat="server" CssClass="textbox-medium1"></asp:DropDownList>
                     </ContentTemplate>
                 </asp:UpdatePanel>
                
                <div class="clear"></div>
            </div>
            <div class="form-sec-row">
                <label><strong>Pin:</strong></label>  
                 <asp:TextBox ID="txtPresentPin" runat="server" CssClass="textbox-medium1"  
                   ></asp:TextBox>
                <div class="clear"></div>
            </div>
            

         

            

            
            <div class="form-sec-row">
                <label  style='color:Blue'><strong>Permanent Address :</strong></label>
                <asp:CheckBox ID="chkParmAdd"  AutoPostBack="true" runat="server"  Text="Same as Present"
                    OnCheckedChanged="chkParmAdd_CheckedChanged"/>  
                <div class="clear"></div>
            </div>
            <div class="form-sec-row">
                <label><strong>Permanent Address:</strong></label>  
                 <asp:TextBox ID="txtParmAddr" runat="server" Height="55px"   CssClass="textbox-medium1" TextMode="MultiLine" 
                   ></asp:TextBox>
                <div class="clear"></div>
            </div>
           <div class="form-sec-row">
                <label><strong>Country :</strong></label>  
                 <%--onChange="getParmanentState()"--%>
               <%--OnSelectedIndexChanged="ddlCountryParmanent_SelectedIndexChanged"--%>
               <asp:UpdatePanel ID="UpdPC" runat="server">
                   <ContentTemplate>
                       <asp:DropDownList ID="ddlCountryPermanent" runat="server" CssClass="textbox-medium1" AutoPostBack="true"  OnSelectedIndexChanged="ddlCountryParmanent_SelectedIndexChanged"></asp:DropDownList>
                   </ContentTemplate>
               </asp:UpdatePanel>
              <div class="clear"></div>
            </div>
            <div class="form-sec-row">
                <label><strong>State :</strong></label>  
                 <%--onChange="getParmanentDistrict()" --%>
                 <%--OnSelectedIndexChanged="ddlStatePermanent_SelectedIndexChanged"--%>
                <asp:UpdatePanel ID="UpdPS" runat="server">
                    <ContentTemplate>
                        <asp:DropDownList ID="ddlStatePermanent" runat="server" CssClass="textbox-medium1" AutoPostBack="true" OnSelectedIndexChanged="ddlStatePermanent_SelectedIndexChanged"></asp:DropDownList>
                    </ContentTemplate>
                </asp:UpdatePanel>
                
                <div class="clear"></div>
            </div>
            <div class="form-sec-row">
                <label><strong>District :</strong></label>
                <asp:UpdatePanel ID="UpdPD" runat="server">
                    <ContentTemplate>
                        <asp:DropDownList ID="ddlDistrictPermanent" runat="server" CssClass="textbox-medium1"></asp:DropDownList>
                    </ContentTemplate>
                </asp:UpdatePanel>
                 
                <div class="clear"></div>
            </div>
            <div class="form-sec-row">
                <label><strong>Pin:</strong></label>  
                 <asp:TextBox ID="txtParmanentPin" runat="server" CssClass="textbox-medium1"  
                   ></asp:TextBox>
                <div class="clear"></div>
            </div>
             
          <div class="form-sec-row">
                <label><strong>Email Id: </strong></label>  
                 <asp:TextBox ID="txtEmailId" runat="server" CssClass="textbox-medium1"></asp:TextBox>
                <div class="clear"></div>
            </div> 
            

            
            <div class="form-sec-row">
                <label><strong>Aadhaar No: </strong></label>  
                 <asp:TextBox ID="txtAadhaarNo" runat="server" CssClass="textbox-medium1"></asp:TextBox>
                <div class="clear"></div>
            </div>  
         
            <div class="form-sec-row">
                <label><strong>Passport Id:</strong></label>  
                 <asp:TextBox ID="txtPanNo" runat="server" CssClass="textbox-medium1"></asp:TextBox>
                <div class="clear"></div>
            </div>  

            <div class="form-sec-row">
                <label><strong>Attach Aadhaar Card:</strong></label>  
                 <asp:FileUpload ID="FileUpload1" runat="server" />
                <div class="clear"></div>
            </div>
            
            
           <div class="form-sec-row" style="display:none;">
                <label><strong>Doctor Type :</strong></label>
               <asp:DropDownList ID="DropDownList2" runat="server" CssClass="textbox-medium1" 
                          AutoPostBack="True" onselectedindexchanged="DropDownList2_SelectedIndexChanged">
                            </asp:DropDownList>
                <div class="clear"></div>
            </div> 
            
                  <div class="form-sec-row" style="display:none;">
                <label><strong>Appointed Doctor :<span style="color:red;">*</span></strong></label>
            <asp:DropDownList ID="DropDownList3" runat="server" CssClass="textbox-medium1">
                            </asp:DropDownList>
                <div class="clear"></div>
            </div>         
       
            <div class="form-sec-row" style="display:none;">
                <label><strong>Appointment Date :<span style="color:red;">*</span></strong></label>
         <asp:TextBox ID="TextBox12" runat="server" CssClass="textbox-medium1"></asp:TextBox>
                <cc1:MaskedEditExtender ID="MaskedEditExtender2" runat="server" TargetControlID="TextBox12"
                                            Mask="99/99/9999" MessageValidatorTip="true" MaskType="Date" DisplayMoney="Left"
                                            AcceptNegative="Left" ErrorTooltipEnabled="True" CultureName="en-GB" />
              
                         <asp:Label ID="Label3" runat="server" ForeColor="Green" Text="(DD/MM/YYYY)"></asp:Label>
                <div class="clear"></div>
            </div>
            <div class="form-sec-row" style="display:none;"> 
                <label><strong>Appointment Time :<span style="color:red;">*</span></strong></label>  
                  <asp:TextBox ID="TextBox13" runat="server" CssClass="textbox-medium1"  
                   ></asp:TextBox>
                <div class="clear"></div>
            </div>
           <div class="form-sec-row" style="display:none;">
                <label><strong>Refered By :</strong></label>
                <%--<asp:TextBox ID="txt_refred" runat="server" CssClass="textbox-medium1" ></asp:TextBox>--%>
               <asp:DropDownList ID="ddlRefDoc" runat="server" CssClass="textbox-medium1">
                            </asp:DropDownList>
                <div class="clear"></div>
            </div>
                <div class="form-sec-row" style="display:none">
                <label><strong>Appointed Type :</strong></label>
            <asp:DropDownList ID="DropDownList5" runat="server" CssClass="textbox-medium1" 
                        Enabled="False">
                <asp:ListItem Value="1">Enrollment</asp:ListItem>
                            </asp:DropDownList>
                <div class="clear"></div>
            </div>

            <div class="form-sec-row"  style="display:none"> 
                <label><strong>Type of OPD Patient:</strong></label>
                        <asp:DropDownList ID="DropDownList4" runat="server" CssClass="textbox-medium1"></asp:DropDownList>
                <div class="clear"></div>
            </div>
            <div class="form-sec-row"  style="display:none">
                <label><strong>Advance Amount :</strong></label>
                <asp:TextBox ID="TextBox14" runat="server" CssClass="textbox-medium1" ></asp:TextBox>
                <div class="clear"></div>
            </div>

            <div class="form-sec-row"  style="display:none"> 
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

            <div class="form-sec-row">
                <label><strong>Remarks:</strong></label>  
                 <asp:TextBox ID="TextBox15" runat="server" Height="45px"   CssClass="textbox-medium1" TextMode="MultiLine" 
                   ></asp:TextBox>
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
      <table width="100%">
          <tr>
                <td>
                    <div class="form-sec-row"> 
                        <asp:HiddenField ID="hdntype" runat="server" />
                         <label class="pname"><strong>Reg No :</strong></label> 
                        <asp:TextBox ID="txtregSrch" runat="server" CssClass="textbox-medium1" Width="138px" ></asp:TextBox>  <div class="clear"></div>
                    </div>
            
                </td>
                <td>
                    <div class="form-sec-row"> 
                    <label class="pname"><strong>Name :</strong></label> 
                    <asp:TextBox ID="txtnameSrch" runat="server" CssClass="textbox-medium1" Width="138px" ></asp:TextBox>  <div class="clear"></div>
                    </div>
                </td>

                <td>
                    <div class="form-sec-row"> 
                    <label class="pname"><strong>Ph No :</strong></label> <asp:TextBox ID="txtphSrch" runat="server" CssClass="textbox-medium1" Width="138px" ></asp:TextBox>
                    </div>
                </td>
                <td>
                    <div class="form-sec-row"> 
                    <label class="pname"><strong>Reg Date :</strong></label> <asp:TextBox ID="txtRegDate" runat="server" CssClass="textbox-medium1" Width="138px" TextMode="Date"></asp:TextBox>
                    </div>
                </td>
                <td>
                    <div class="form-sec-row"> 
                    <asp:Button ID="Button5" runat="server" Text="Search" Height="28px"  CssClass="submit-button1" onclick="Button5_Click" />
                    </div>                  
                </td>             
                      
            </tr>
      </table>
      <div class="listing"   style='width:100%; height:600px; overflow:auto;'>
         <asp:GridView id="GridView1"  CssClass="grid" PagerStyle-CssClass="pgr" 
              DataKeyNames ="AppoNo" runat="server" AutoGenerateColumns="False" 
              AllowPaging="True" PageSize ="100" 
                 OnPageIndexChanging="GridView1_PageIndexChanging" 
                 OnRowCommand="GridView1_RowCommand"  SelectedRowStyle-BackColor="GreenYellow"  OnRowDataBound="GridView1_RowDataBound"
             OnRowDeleting="GridView1_RowDeleting" Width="100%" 
              onselectedindexchanged="GridView1_SelectedIndexChanged">
                <RowStyle HorizontalAlign="Center" />
                <Columns>
                    <asp:TemplateField HeaderText="ApppoId" Visible="false">
                        <ItemTemplate>
                            <asp:Label ID="lblID" runat="server" Text='<%# Bind("AppoNo") %>'></asp:Label>
                            <asp:Label ID="lblAadhaar" runat="server" Text='<%# Bind("AADHAARNO") %>' Visible="false"></asp:Label>
                            <asp:Label ID="lblPan" runat="server" Text='<%# Bind("PanNo") %>' Visible="false"></asp:Label>
                            <asp:Label ID="lblFilePath" runat="server" Text='<%# Bind("FilePath") %>' Visible="false"></asp:Label>
                            <asp:Label ID="lblvchno" runat="server" Text='<%# Bind("VchNo") %>' Visible="false"></asp:Label>
                            <asp:Label ID="lblPaymode" runat="server" Text='<%# Bind("PaymentMode") %>' Visible="false"></asp:Label>
                            <asp:Label ID="lblChqNo" runat="server" Text='<%# Bind("Chq_CardNo") %>' Visible="false"></asp:Label>
                            <asp:Label ID="lblChqDt" runat="server" Text='<%# Bind("ChqDt_CardExpDt") %>' Visible="false"></asp:Label>
                            <asp:Label ID="lblBankNm" runat="server" Text='<%# Bind("Bank_CardHolderName") %>' Visible="false"></asp:Label>
                            <asp:Label ID="lblRefDoc" runat="server" Text='<%# Bind("ReferedBy") %>' Visible="false"></asp:Label>

                            <asp:Label ID="lblPresentPin" runat="server" Text='<%# Bind("PresentPin") %>' Visible="false"></asp:Label>
                            <asp:Label ID="lblPresentState" runat="server" Text='<%# Bind("PresentState") %>' Visible="false"></asp:Label>
                            <asp:Label ID="lblPresentCountry" runat="server" Text='<%# Bind("PresentCountry") %>' Visible="false"></asp:Label>
                            <asp:Label ID="lblParmAddr" runat="server" Text='<%# Bind("ParmAddr") %>' Visible="false"></asp:Label>
                            <asp:Label ID="lblParmanentPin" runat="server" Text='<%# Bind("ParmanentPin") %>' Visible="false"></asp:Label>
                            <asp:Label ID="lblParmDist" runat="server" Text='<%# Bind("ParmDist") %>' Visible="false"></asp:Label>
                            <asp:Label ID="lblParmState" runat="server" Text='<%# Bind("ParmState") %>' Visible="false"></asp:Label>
                            <asp:Label ID="lblparmCountry" runat="server" Text='<%# Bind("parmCountry") %>' Visible="false"></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Registration No">
                       
                        <ItemTemplate>
                            <asp:Label ID="lblregno" runat="server" Text='<%# Bind("PatientRegNo") %>'></asp:Label>
                        </ItemTemplate>
                        <ItemStyle HorizontalAlign="Left" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Patient Name">
                       
                        <ItemTemplate>
                            <asp:Label ID="lblname" runat="server" Text='<%# Bind("PName") %>'></asp:Label>
                        </ItemTemplate>
                        <ItemStyle HorizontalAlign="Left" />
                    </asp:TemplateField>
                    
                    <asp:TemplateField HeaderText="Age">
                        <ItemTemplate>                        
                            <asp:Label ID="lblage" runat="server" Text='<%# Bind("Age") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField> 
                       
                    <asp:TemplateField HeaderText="Sex">
                        <ItemTemplate>                        
                            <asp:Label ID="lblsex" runat="server" Text='<%# Bind("SexName") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                      <asp:TemplateField HeaderText="Phone - 1" >
                        <ItemTemplate>                        
                            <asp:Label ID="lblphone1" runat="server" Text='<%# Bind("PhNo1") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Phone - 2">
                        <ItemTemplate>                        
                            <asp:Label ID="lblphone2" runat="server" Text='<%# Bind("PhNo2") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Address">
                        <ItemTemplate>                        
                            <asp:Label ID="lbladdress" runat="server" Text='<%# Bind("Address") %>'></asp:Label>
                        </ItemTemplate>
                        <ItemStyle HorizontalAlign="Left" />
                    </asp:TemplateField>  
                    <asp:TemplateField HeaderText="Guardian Name" Visible="false">
                        <ItemTemplate>                        
                            <asp:Label ID="lblguadian" runat="server" Text='<%# Bind("GuadianName") %>'></asp:Label>    
                        </ItemTemplate>
                        <ItemStyle HorizontalAlign="Left" />
                    </asp:TemplateField>    
                    <asp:TemplateField HeaderText="Email Id">
                        <ItemTemplate>                        
                            <asp:Label ID="lblEmailId" runat="server" Text='<%# Bind("EmailId") %>'></asp:Label>    
                        </ItemTemplate>
                        <ItemStyle HorizontalAlign="Left" />
                    </asp:TemplateField> 
                      <%--<asp:TemplateField HeaderText="Doctor Type " Visible="false">
                        <ItemTemplate>                        
                            <asp:Label ID="lbldoctype" runat="server" Text='<%# Bind("dttype") %>'></asp:Label>    
                        </ItemTemplate>
                    </asp:TemplateField>--%> 


                        <%--<asp:TemplateField HeaderText="Doctor Name" Visible="false">
                        <ItemTemplate>                        
                            <asp:Label ID="lbldocname" runat="server" Text='<%# Bind("doc_name") %>'></asp:Label>    
                        </ItemTemplate>
                    </asp:TemplateField> --%>


                        <asp:TemplateField HeaderText="Registration Date">
                        <ItemTemplate>                        
                            <asp:Label ID="lblappodate" runat="server" Text='<%# Bind("apppdate") %>'></asp:Label>    
                        </ItemTemplate>
                    </asp:TemplateField> 

                        <asp:TemplateField HeaderText="DOB" Visible="false">
                        <ItemTemplate>                        
                            <asp:Label ID="lbldob" runat="server" Text='<%# Bind("dob1") %>'></asp:Label>    
                        </ItemTemplate>
                    </asp:TemplateField> 


                        <%--<asp:TemplateField HeaderText="Appointment Time" Visible="false">
                        <ItemTemplate>                        
                            <asp:Label ID="lblappotime" runat="server" Text='<%# Bind("AppointmentTime") %>'></asp:Label>    
                        </ItemTemplate>
                    </asp:TemplateField> --%>

                    
                        <%--<asp:TemplateField HeaderText="Patient Type" Visible="false">
                        <ItemTemplate>                        
                            <asp:Label ID="lblptype" runat="server" Text='<%# Bind("ptype") %>'></asp:Label>    
                        </ItemTemplate>
                    </asp:TemplateField>--%>


                    
                        <asp:TemplateField HeaderText="District">
                        <ItemTemplate>                        
                            <asp:Label ID="lbldist" runat="server" Text='<%# Bind("District") %>'></asp:Label>    
                        </ItemTemplate>
                    </asp:TemplateField>
                    
                        <%--<asp:TemplateField HeaderText="Advanced Amount" Visible="false">
                        <ItemTemplate>                        
                            <asp:Label ID="lbladvamt" runat="server" Text='<%# Bind("AdvancedAmount") %>'></asp:Label>    
                        </ItemTemplate>
                    </asp:TemplateField>--%>

                    
                   <asp:TemplateField HeaderText="Remarks" Visible="false">
                        <ItemTemplate>                        
                            <asp:Label ID="lblremarks" runat="server" Text='<%# Bind("Remarks") %>'></asp:Label>    
                        </ItemTemplate>
                    </asp:TemplateField>


                    <asp:TemplateField HeaderText="">
                        <ItemTemplate>
                             <asp:LinkButton ID="LinkButton4"  CommandName="OPD" CommandArgument='<%# Eval("PatientRegNo") %>' runat="server">OPD</asp:LinkButton>
                        </ItemTemplate>
                    </asp:TemplateField>    
                    <asp:TemplateField HeaderText="">
                        <ItemTemplate>
                             <asp:LinkButton ID="LinkButton3"  CommandName="Diagnostic" CommandArgument='<%# Eval("PatientRegNo") %>' runat="server">Diagnostic</asp:LinkButton>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="">
                        <ItemTemplate>
                             <asp:LinkButton ID="LinkButton5"  CommandName="Procedure" CommandArgument='<%# Eval("PatientRegNo") %>' runat="server">Procedure</asp:LinkButton>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="">
                        <ItemTemplate>
                             <asp:LinkButton ID="LinkButton6"  CommandName="Infertility" CommandArgument='<%# Eval("PatientRegNo") %>' runat="server">Infertility</asp:LinkButton>
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
    <%--</ContentTemplate>
    </asp:UpdatePanel>--%>

</asp:Content>

