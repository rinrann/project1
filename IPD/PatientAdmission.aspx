<%@ Page Title="" Language="C#" MasterPageFile="~/MasterAll/MasterPageAll.master" AutoEventWireup="true" CodeFile="PatientAdmission.aspx.cs" Inherits="IPD_PatientAdmission" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <script src="../Script/jquery.webcam.js" type="text/javascript"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
   
    <script type="text/javascript" language="javascript">  
       
    $(function () {
        /* date picker event*/
        $('.datepicker').datepicker({
            dateFormat: "dd/mm/yy",
            changeMonth: true,
            changeYear: true,
            //yearRange: '1900:' + new Date().getFullYear(),
            yearRange: '1900:2900',
            showOn: "button",
            buttonImage: "../images/calender.png",
            //buttonImage: "../images/green-button.gif",
            buttonImageOnly: true,
            showAnim: "fold"
        });
    });
  
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
            document.getElementById("ctl00_ContentPlaceHolder1_time").value = time;
            document.getElementById("ctl00_ContentPlaceHolder1_Calendar1").value = datetime;
        }

        function SetContextKey() {
            $find('AutoCompleteExtender1').set_contextKey("GFC");
        }


        function autoCompleteEx_ItemSelected(sender, args) {
            var regname = args.get_value().split('-');
            document.getElementById("ctl00_ContentPlaceHolder1_TextBox1").value = regname[1];
            document.getElementById("ctl00_ContentPlaceHolder1_TextBox2").value = regname[0];
        }





        function autoCompleteExAddress_ItemSelected(sender, args) {
            var regname = args.get_value().split('-');
            document.getElementById("ctl00_ContentPlaceHolder1_TextBox24").value = regname[1];
            document.getElementById("ctl00_ContentPlaceHolder1_TextBox9").value = regname[0];
            document.getElementById("ctl00_ContentPlaceHolder1_TextBox25").value = regname[2];
            document.getElementById("ctl00_ContentPlaceHolder1_TextBox11").value = regname[3];
        }


        function GetBirthDate() {
            var yr = document.getElementById("ctl00_ContentPlaceHolder1_TextBox4").value;
            var mn = document.getElementById("ctl00_ContentPlaceHolder1_TextBox30").value;
            var dy = document.getElementById("ctl00_ContentPlaceHolder1_TextBox31").value;
            if (dy < 10) { dy = '0' + dy } if (mn < 10) { mn = '0' + mn }
            var ddd = new Date(new Date().getFullYear() - yr, new Date().getMonth() - mn, (new Date().getDay() + 11) - dy);
            
            var dob = (ddd.getDate()) + "/" + (ddd.getMonth()+1) + "/" + ddd.getFullYear();
            //alert(dob)
            document.getElementById("ctl00_ContentPlaceHolder1_TextBox27").value = dob;
        }


        function displayDate() {
            var a = document.getElementById("ctl00_ContentPlaceHolder1_TextBox27").value; 
            var b = a.split("/");
            var bday = b[1] + "/" + b[0] + "/" + b[2];          
            var d1 = new Date(bday);   //from date yyyy-MM-dd
            var d2 = new Date(); //to date yyyy-MM-dd (taken currentdate)
            var Months = d2.getMonth() - d1.getMonth();         
            var Years = d2.getFullYear() - d1.getFullYear();     
            var Days = d2.getDate() - d1.getDate();       
            Months = (d2.getMonth() + 12 * d2.getFullYear()) - 	(d1.getMonth() + 12 * d1.getFullYear());
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
       
            document.getElementById("ctl00_ContentPlaceHolder1_TextBox4").value = Years;
            document.getElementById("ctl00_ContentPlaceHolder1_TextBox30").value = Months;
            document.getElementById("ctl00_ContentPlaceHolder1_TextBox31").value = Days;
        }
    function showStuff(id) {
        var el = document.getElementById(id);
        if (el.style.display != 'none') {
            el.style.display = 'none';
        }
        else {
            el.style.display = '';
        }
    }

    function ShowDialog() {
        //alert();
        var rtvalue = window.open("AdmissionRegPopup.aspx", '_blank', "sss", "Width:1050px; Height:550px; dialogLeft:250px;");
        //alert(rtvalue.NameValue);
        //document.getElementById("ctl00_ContentPlaceHolder1_TextBox1").value = rtvalue.NameValue;
    }
    function ShowDialogCopy() {
        var rtvalue = window.open("IpdpatientPopup.aspx", "sss", "Width:1050px; Height:550px; dialogLeft:250px;");
        //document.getElementById("ctl00_ContentPlaceHolder1_TextBox1").value = rtvalue.NameValue;
    }

    function ConfirmationMessage() {
        var data = confirm("Want to Take Photo ? If yes Click Ok or Cancel");
        if (data) {
            SetSession();
        }

        window.location = "../IPD/Allreport.aspx";
    }

        function SetSession() {
            var value = document.getElementById("ctl00_ContentPlaceHolder1_TextBox1").value;
            $.ajax({
                type: "POST",
                url: "PatientAdmission.aspx/SetSession",
                data: '{value: "' + value + '" }',
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: OnSuccess,
                failure: function (response) {
                    alert(response.d);
                }
            });
        }
        function OnSuccess(response) {
            window.open("Captureimage.aspx", "sss", "Width:950px; Height:550px; dialogLeft:250px;");
        }
 
        

    function ShowDialog1() {
        var rtvalue = window.open("BedAllocationPopup.aspx", "sss", "Width:800px; Height:550px; dialogLeft:250px;");
        //document.getElementById("ctl00_ContentPlaceHolder1_TextBox19").value = rtvalue.NameValue;
        //document.getElementById("ctl00_ContentPlaceHolder1_HiddenField2").value = rtvalue.ProfessionValue;
    }

    function ShowDialog2() {
        var rtvalue = window.open("UnderDoctorPopup.aspx", "sss", "Width:800px; Height:550px; dialogLeft:250px;");
        var a = rtvalue.split("#");
        //document.getElementById("ctl00_ContentPlaceHolder1_HiddenField4").value = a[0];
        //document.getElementById("ctl00_ContentPlaceHolder1_TextBox18").value = a[1];

    }

    function ShowDialog3() {
        var rtvalue = window.open("referbypopup.aspx", "sss", "Width:800px; Height:550px; dialogLeft:250px;");
        //document.getElementById("ctl00_ContentPlaceHolder1_HiddenField3").value = rtvalue.NameValue;
        //var a = rtvalue.ProfessionValue.split("#");
        //document.getElementById("ctl00_ContentPlaceHolder1_TextBox21").value = a[0];
        //document.getElementById("ctl00_ContentPlaceHolder1_HiddenField5").value = a[1];

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
        $("input[id$='Calendar1']").datepicker({ dateFormat: 'dd/mm/yy' });
        $("input[id$='TextBox27']").datepicker({ dateFormat: 'dd/mm/yy' });

        $("input[id$='time']").timepicker({
            showPeriod: true,
            showLeadingZero: true
        });

        $("input[id$='Button1']").click(function () {

            if ($("input[id$='TextBox1']").val() == '') {
                alert('Please Enter Registration No Properly !');
                $("input[id$='TextBox1']").focus();
                $("input[id$='TextBox1']").addClass('textboxerr');
                return false;
            }
            else {
                $("input[id$='TextBox1']").removeClass('textboxerr');
            }


            if ($("input[id$='Calendar1']").val() == '') {
                alert('Please Enter Date  !');
                $("input[id$='Calendar1']").focus();
                $("input[id$='Calendar1']").addClass('textboxerr');
                return false;
            }
            else {
                $("input[id$='Calendar1']").removeClass('textboxerr');
            }




            if ($("input[id$='time']").val() == '') {
                alert('Please Enter Admission Time  !');
                $("input[id$='time']").focus();
                $("input[id$='time']").addClass('textboxerr');
                return false;
            }
            else {
                $("input[id$='time']").removeClass('textboxerr');
            }




            if ($("input[id$='TextBox21']").val() == '') {
                alert('Please Select  Refer By  !');
                $("input[id$='TextBox21']").focus();
                $("input[id$='TextBox21']").addClass('textboxerr');
                return false;
            }
            else {
                $("input[id$='TextBox21']").removeClass('textboxerr');
            }




            if ($("input[id$='TextBox32']").val() == '') {
                alert('Please Enter Diagnosis  !');
                $("input[id$='TextBox32']").focus();
                $("input[id$='TextBox32']").addClass('textboxerr');
                return false;
            }
            else {
                $("input[id$='TextBox32']").removeClass('textboxerr');
            }
            //code start
           

           

            //code end




            if ($("input[id$='TextBox2']").val() == '') {
                alert('Please Enter Name Properly!');
                $("input[id$='TextBox2']").focus();
                $("input[id$='TextBox2']").addClass('textboxerr');
                return false;
            }
            else {
                $("input[id$='TextBox2']").removeClass('textboxerr');
            }




            if ($("input[id$='TextBox3']").val() == '') {
                alert('Please Enter Husband Name !');
                $("input[id$='TextBox3']").focus();
                $("input[id$='TextBox3']").addClass('textboxerr');
                return false;
            }
            else {
                $("input[id$='TextBox3']").removeClass('textboxerr');
            }

           
            if ($("textarea[id$='TextBox9']").val() == '') {
                alert('Please Enter Present Address Properly!');
                $("textarea[id$='TextBox9']").focus();
                $("textarea[id$='TextBox9']").addClass('textboxerr');
                return false;
            }
            else {
                $("textarea[id$='TextBox9']").removeClass('textboxerr');
            }

            if ($("textarea[id$='TextBox12']").val() == '') {
                alert('Please Enter Guadian Name!');
                $("textarea[id$='TextBox12']").focus();
                $("textarea[id$='TextBox12']").addClass('textboxerr');
                return false;
            }
            else {
                $("textarea[id$='TextBox12']").removeClass('textboxerr');
            }

            if ($("input[id$='TextBox13']").val() == '') {
                alert('Please Enter Relation With Guadian!');
                $("input[id$='TextBox13']").focus();
                $("input[id$='TextBox13']").addClass('textboxerr');
                return false;
            }
            else {
                $("input[id$='TextBox13']").removeClass('textboxerr');
            }



            if ($("input[id$='TextBox18']").val() == '') {
                alert('Please Enter Under Doctor!');
                $("input[id$='TextBox18']").focus();
                $("input[id$='TextBox18']").addClass('textboxerr');
                return false;
            }

            else {
                $("input[id$='TextBox18']").removeClass('textboxerr');
            }


            if ($("input[id$='TextBox19']").val() == '') {
                alert('Please Enter Bed No!');
                $("input[id$='TextBox19']").focus();
                $("input[id$='TextBox19']").addClass('textboxerr');
                return false;
            }
            else {
                $("input[id$='TextBox19']").removeClass('textboxerr');
            }

            if ($("input[id$='TextBox27']").val() == '') {
                alert('Please Date of Birth!');
                $("input[id$='TextBox27']").focus();
                $("input[id$='TextBox27']").addClass('textboxerr');
                return false;
            }
            else {
                $("input[id$='TextBox27']").removeClass('textboxerr');
            }

            /*if ($("select[id$='DropDownList1']").val() == '0') {
                alert('Please Select Refer By!');
                $("select[id$='DropDownList1']").addClass('textboxerr');
                $("select[id$='DropDownList1']").focus();
                return false;
            }
            else {
                $("select[id$='DropDownList1']").removeClass('textboxerr');
            }*/

            if ($("select[id$='DropDownList7']").val() == '0') {
                alert('Please Select State!');
                $("select[id$='DropDownList7']").addClass('textboxerr');
                $("select[id$='DropDownList7']").focus();
                return false;
            }
            else {
                $("select[id$='DropDownList7']").removeClass('textboxerr');
            }



            /*if ($("select[id$='DropDownList1']").val() == '0') {
                alert('Please Select Dicipline !');
                $("select[id$='DropDownList1']").addClass('textboxerr');
                $("select[id$='DropDownList1']").focus();
                return false;
            }
            else {
                $("select[id$='DropDownList1']").removeClass('textboxerr');
            }*/



            if ($("select[id$='DropDownList6']").val() == '0') {
                alert('Please Select Diagnosis !');
                $("select[id$='DropDownList6']").addClass('textboxerr');
                $("select[id$='DropDownList6']").focus();
                return false;
            }
            else {
                $("select[id$='DropDownList6']").removeClass('textboxerr');
            }



            if ($("select[id$='DropDownList2']").val() == '0') {
                alert('Please Select Sex!');
                $("select[id$='DropDownList2']").addClass('textboxerr');
                $("select[id$='DropDownList2']").focus();
                return false;
            }
            else {
                $("select[id$='DropDownList2']").removeClass('textboxerr');
            }

            if ($("select[id$='DropDownList3']").val() == '0') {
                alert('Please Select Religion!');
                $("select[id$='DropDownList3']").addClass('textboxerr');
                $("select[id$='DropDownList3']").focus();
                return false;
            }
            else {
                $("select[id$='DropDownList3']").removeClass('textboxerr');
            }

            if ($("select[id$='DropDownList4']").val() == '0') {
                alert('Please Select Maritial Status!');
                $("select[id$='DropDownList4']").addClass('textboxerr');
                $("select[id$='DropDownList4']").focus();
                return false;
            }
            else {
                $("select[id$='DropDownList4']").removeClass('textboxerr');
            }


        });

        $("input[id$='TextBox4']").keydown(function (event) {
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

        $("input[id$='TextBox6']").keydown(function (event) {
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




        $("input[id$='TextBox23']").keydown(function (event) {
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



        $("input[id$='TextBox8']").keydown(function (event) {
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


        $("input[id$='TextBox11']").keydown(function (event) {
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



        $("input[id$='TextBox4']").keydown(function (event) {
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



        $("input[id$='TextBox30']").keydown(function (event) {
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



        $("input[id$='TextBox31']").keydown(function (event) {
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
<%--  <asp:UpdateProgress ID="UpdateProgress1" runat="server">
    <ProgressTemplate>
    
       <div id="aaa" class="progressBackgroundFilter">          </div>
        <div id="bbb" class="processMessage"><img alt="Loading" src="../images/pwait.gif"/>
      
              </div>
    </ProgressTemplate>
    </asp:UpdateProgress>--%>
    <%--For Busy Loader End.............................--%>

    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
    <ContentTemplate>
     
    <div class="pageheader">
         <asp:Label ID="Label1" runat="server">Patient Admission</asp:Label>
    </div>
    <div class="formbox">
    <div id="h1">
        <div class="form-sec">
        
            <asp:HiddenField ID="HiddenField1" runat="server" />  
              <asp:HiddenField ID="HiddenField2" runat="server" /><asp:HiddenField ID="HiddenField3" runat="server" />
              <asp:HiddenField ID="HiddenField4" runat="server" />
              <asp:HiddenField ID="HiddenField5" runat="server" />
            
            <div class="form-sec-row">
                <label  style='color:Blue'><strong>Registration No :<span style="color:red">*</span></strong></label>
                <asp:TextBox ID="TextBox1" runat="server" CssClass="textbox-medium1"    Enabled="False"></asp:TextBox>
                  <asp:Button ID="Button4" runat="server" Text="Quick Search" Height="28px"  CssClass="submit-buttonCheck" OnClientClick="ShowDialog()"/>
           
               <asp:Button ID="Button5" runat="server" Text="Fetch" 
                    CssClass="submit-button" Height="28px" onclick="Button5_Click" />
              
                <div class="clear"></div>
            </div> 
		    <div class="form-sec-row">         
               <label><strong>Patient's Name :<span style="color:red">*</span></strong></label>
                <asp:TextBox ID="TextBox2" runat="server" CssClass="textbox-medium1" ></asp:TextBox>
         <cc1:AutoCompleteExtender ServiceMethod="SearchCustomersName"    OnClientItemSelected="autoCompleteEx_ItemSelected"    MinimumPrefixLength="1"
           CompletionInterval="100" EnableCaching="false" 
           CompletionSetCount="10" TargetControlID="TextBox2"  ID="AutoCompleteExtender1" runat="server" FirstRowSelected = "false" ></cc1:AutoCompleteExtender>
                <asp:Button ID="Button_copy" runat="server" Text="Copy" Height="28px"  CssClass="submit-button" OnClientClick="ShowDialogCopy()"/>
                <asp:Button ID="Button_fetchcopy" runat="server" Text="Fetch" CssClass="submit-button" Height="28px" OnClick="Button_fetchcopy_Click" />
              
                
           <div class="clear"></div>              
            </div> 

 
            

     <div class="form-sec-row">
                <label><strong>Husband Name / C/O :<span style="color:red">*</span></strong></label>
                 <asp:TextBox ID="TextBox3" runat="server" CssClass="textbox-medium1" 
                      ></asp:TextBox> 
                <div class="clear"></div>
            </div>  
            
  

   
        <div class="form-sec-row">
                <label><strong>Refer By :  (Mr./Dr./Mrs.)<span style="color:red">*</span></strong></label>
                                    
               <asp:TextBox ID="TextBox21" runat="server"    Enabled="False"  CssClass="textbox-medium1"></asp:TextBox>  
                 
                  <asp:Button ID="Button7" runat="server" Text="Search"  Height="28px" CssClass="submit-button" OnClientClick="ShowDialog3()"/>   
             
                <div class="clear"></div>
            </div>

   
      <div class="form-sec-row">
                <label><strong>Under Doctor :<span style="color:red">*</span></strong></label>
                 <asp:TextBox ID="TextBox18" runat="server" CssClass="textbox-medium1" 
                       Enabled="False" ></asp:TextBox>
        <asp:Button ID="Button8" runat="server" Text="Search" CssClass="submit-button"   Height="28px"  OnClientClick="ShowDialog2()" />                 
                <div class="clear"></div>
            </div>

            <div class="form-sec-row">
                <label><strong>Diagnosis :<span style="color:red">*</span></strong></label>
                <asp:TextBox ID="TextBox32" runat="server" CssClass="textbox-medium1"></asp:TextBox>
                
   <cc1:AutoCompleteExtender ServiceMethod="SearchCustomers2" MinimumPrefixLength="1"    CompletionInterval="100" EnableCaching="false" 
        CompletionSetCount="10" TargetControlID="TextBox32"  ID="AutoCompleteExtender2" runat="server" FirstRowSelected = "false" ></cc1:AutoCompleteExtender>
                
                <asp:Label ID="Label4" runat="server" ForeColor="Red" Text="(Never manually insert type first word & choose from the list if not present insert into diagnosis Master)"></asp:Label>
                <div class="clear"></div>
            </div>
   
               <div class="form-sec-row">
                <label><strong>Date of Birth :<span style="color:red">*</span></strong></label>
                <asp:TextBox ID="TextBox27" runat="server" CssClass="textbox-medium1 datepicker"  
                     onchange="displayDate();"></asp:TextBox>
                     <cc1:MaskedEditExtender ID="MaskedEditExtender1" runat="server" TargetControlID="TextBox27"
                                            Mask="99/99/9999" MessageValidatorTip="true" MaskType="Date" DisplayMoney="Left"
                                            AcceptNegative="Left" ErrorTooltipEnabled="True" CultureName="en-GB" />
                           <asp:Label ID="Label2" runat="server" ForeColor="Green" Text="(DD/MM/YYYY)"></asp:Label>
                   
                <div class="clear"></div>
            </div>
   
   <div class="form-sec-row">
                <label><strong>Age :</strong></label>
           <div style='float:left;padding-left:0px;'> <strong>  Year</strong></div><div style='float:left;padding-left:6px;'> 
                <asp:TextBox ID="TextBox4"  MaxLength="3" runat="server" Width="55px"  onchange="GetBirthDate();" Enabled="false"></asp:TextBox></div> 
             <div style=' float:left; padding-left:6px;'> <strong>  Month</strong></div> <div style=' float:left; padding-left:6px;'> 
                <asp:TextBox ID="TextBox30" runat="server"  Width="52px"  MaxLength="2"   onchange="GetBirthDate();" Enabled="false"></asp:TextBox></div>
             <div style=' float:left; padding-left:6px;'>   <strong>  Day</strong></div> <div style=' float:left; padding-left:6px;'> 
                <asp:TextBox ID="TextBox31" runat="server"  MaxLength="2"   Width="52px" onchange="GetBirthDate();" Enabled="false"></asp:TextBox></div>
                <div class="clear"></div>
            </div>
   
   
   <div class="form-sec-row" style="padding-bottom:4px;">
                <label><strong>Sex :<span style="color:red">*</span></strong></label>
                   <asp:DropDownList ID="DropDownList2" runat="server" CssClass="combo-big1">
                            </asp:DropDownList>
                <div class="clear"></div>
            </div>
       
            <div class="form-sec-row">
                <label><strong>Religion :<span style="color:red">*</span></strong></label>
                   <asp:DropDownList ID="DropDownList3" runat="server" CssClass="combo-big1">
                      
                   </asp:DropDownList>   <div class="clear"></div>
            </div>
  
  
  <div class="form-sec-row">
                <label><strong>Marital Status :<span style="color:red">*</span></strong></label>
                  <asp:DropDownList ID="DropDownList4" runat="server" CssClass="combo-big1">
                   
                   </asp:DropDownList> 
                <div class="clear"></div>
            </div>
   
     <div class="form-sec-row">
                <label style='color:Blue'><strong>Patient's Address :</strong></label>
                <div class="clear"></div>
            </div>
   
   
     <div class="form-sec-row">
                <label><strong>Address - 1 :<span style="color:red">*</span></strong></label>
                <asp:TextBox ID="TextBox9" runat="server" CssClass="textbox-medium1"></asp:TextBox>
                         <cc1:AutoCompleteExtender ServiceMethod="SearchCustomersAddress"    OnClientItemSelected="autoCompleteExAddress_ItemSelected"   MinimumPrefixLength="1"
           CompletionInterval="100" EnableCaching="false" 
           CompletionSetCount="10" TargetControlID="TextBox9"  ID="AutoCompleteExtender3" runat="server" FirstRowSelected = "false" ></cc1:AutoCompleteExtender>
                <div class="clear"></div>
            </div>

  
   <div class="form-sec-row">
                <label><strong>Address - 2 :</strong></label>
                <asp:TextBox ID="TextBox24" runat="server" CssClass="textbox-medium1" ></asp:TextBox>
                <div class="clear"></div>
            </div>
  
    <div class="form-sec-row">
                <label><strong>Police Station :</strong></label>
                <asp:TextBox ID="TextBox25" runat="server" CssClass="textbox-medium1" ></asp:TextBox>
                <div class="clear"></div>
            </div>

   
     <div class="form-sec-row">
                <label><strong>District :</strong></label>
                    <asp:DropDownList ID="DropDownList9" CssClass="textbox-medium1" runat="server">
                    </asp:DropDownList>
                <div class="clear"></div>
            </div>
  
  
    <div class="form-sec-row">
                <label><strong>Postal Code :</strong></label>
                <asp:TextBox ID="TextBox11" runat="server" MaxLength="6"  CssClass="textbox-medium1" ></asp:TextBox>
                <div class="clear"></div>
            </div>

            
             <div class="form-sec-row">
                <label><strong>State :<span style="color:red">*</span></strong></label>
                  <asp:DropDownList ID="DropDownList7" runat="server" CssClass="combo-big1">                   
                   </asp:DropDownList> 
                <div class="clear"></div>
            </div>

  
  
  <div class="form-sec-row">
                <label><strong>Contact No. 1 :</strong></label>
                <asp:TextBox ID="TextBox5" runat="server" CssClass="textbox-medium1" Width="50" Enabled="False">+91</asp:TextBox>
                <asp:TextBox ID="TextBox6" runat="server" CssClass="textbox-medium1" 
                    MaxLength="10" Width="246px" ></asp:TextBox>
                <div class="clear"></div>
            </div>

            
                    <div class="form-sec-row">
                <label><strong>Contact No. 2 :</strong></label>
                <asp:TextBox ID="TextBox7" runat="server" CssClass="textbox-medium1" Width="50" Enabled="False">+91</asp:TextBox>
                <asp:TextBox ID="TextBox8" runat="server" CssClass="textbox-medium1" 
                    MaxLength="10" Width="246px" ></asp:TextBox>
                <div class="clear"></div>
            </div>
    
    
     
              <div class="form-sec-row">
                <label><strong>Email ID :</strong></label>
                <asp:TextBox ID="TextBox28" runat="server" CssClass="textbox-medium1"></asp:TextBox>
              
                    
                <div class="clear"></div>
            </div>
   
   
       <div class="form-sec-row">
                <label  style='color:Blue'><strong>Guadian's Details :</strong></label>
                <asp:CheckBox ID="CheckBox1"  AutoPostBack="true" runat="server" 
                    oncheckedchanged="CheckBox1_CheckedChanged"/>  
                <div class="clear"></div>
            </div>
 
 
    <div class="form-sec-row">
                <label><strong>Guardian's Name :<span style="color:red">*</span></strong></label>
                <asp:TextBox ID="TextBox12" runat="server" CssClass="textbox-medium1" ></asp:TextBox>
                <asp:Button ID="Button6" runat="server" CssClass="submit-button" Text="ADD"  Height="28px" Width="80px" OnClientClick="showStuff('Div1'); return false;"/>
                 <div class="clear"></div>
            </div>
   
   
        <div class="form-sec-row">
                <label><strong>Relation :<span style="color:red">*</span></strong></label> 
              <asp:TextBox ID="TextBox13" runat="server" CssClass="textbox-medium1" ></asp:TextBox>
                <div class="clear"></div>
            </div>
   


             <div class="form-sec-row">
                <label><strong>Address - 1 :</strong></label>
                <asp:TextBox ID="TextBox10" runat="server" CssClass="textbox-medium1" ></asp:TextBox>
                <div class="clear"></div>
            </div>

                 <div class="form-sec-row">
                <label><strong>Address - 2 :</strong></label>
                <asp:TextBox ID="TextBox26" runat="server" CssClass="textbox-medium1" ></asp:TextBox>
                <div class="clear"></div>
            </div>

            

                 <div class="form-sec-row">
                <label><strong>District :</strong></label>
                    <asp:DropDownList ID="DropDownList10" CssClass="textbox-medium1" runat="server">
                    </asp:DropDownList>
                <div class="clear"></div>
            </div>

                 <div class="form-sec-row">
                <label><strong>Contact No :</strong></label>
                 <asp:TextBox ID="TextBox29" runat="server" Width="50px" CssClass="textbox-medium1" >+91</asp:TextBox>
                <asp:TextBox ID="TextBox23" runat="server" MaxLength="10"  Width="248px" CssClass="textbox-medium1" ></asp:TextBox>
                <div class="clear"></div>
            </div>


            <div id="Div1" style="display:none;">
                 <div class="form-sec-row">
                <label><strong> Another Guadian - 1 </strong></label>
                <asp:TextBox ID="TextBox14" runat="server" CssClass="textbox-medium1" ></asp:TextBox>
                <div class="clear"></div>
            </div>
            
            <div class="form-sec-row">
                <label><strong> Relation With Guadian </strong></label>
              <asp:TextBox ID="TextBox15" runat="server" CssClass="textbox-medium1" ></asp:TextBox>
                <div class="clear"></div>
            </div>
                 <div class="form-sec-row">
                <label><strong> Another Guadian - 2 </strong></label>
                <asp:TextBox ID="TextBox16" runat="server" CssClass="textbox-medium1" ></asp:TextBox>
                <div class="clear"></div>
            </div>
            
            <div class="form-sec-row">
                <label><strong> Relation With Guadian </strong></label>
              <asp:TextBox ID="TextBox17" runat="server" CssClass="textbox-medium1" ></asp:TextBox>
                <div class="clear"></div>
            </div>
            </div>

            

             <%--  <div class="form-sec-row">
                <label><strong>Postal Code :</strong></label>
                <asp:TextBox ID="TextBox27" runat="server" CssClass="textbox-medium1" ></asp:TextBox>
                <div class="clear"></div>
            </div>--%>

         <%--   
                <div class="form-sec-row">
                <label><strong>State :</strong></label>
                  <asp:DropDownList ID="DropDownList11" runat="server" CssClass="combo-big1">                   
                   </asp:DropDownList> 
                <div class="clear"></div>
            </div>--%>

         
  
             <div class="form-sec-row">
                <label style='color:Blue'><strong>Bed Allocation :</strong></label>
                <div class="clear"></div>
            </div>

                <div class="form-sec-row">
                <label><strong>Bed Allocation :<span style="color:red">*</span></strong></label>
                 <asp:TextBox ID="TextBox19" runat="server" CssClass="textbox-medium1" 
                        Enabled="False" ></asp:TextBox>
        <asp:Button ID="Button9" runat="server" Text="Search"  Height="27px"  OnClientClick="ShowDialog1()" CssClass="submit-button" />

     <%-- <input type ="button" class="submit-button" onclick="ShowDialog1()" value="Search" />--%>
                 
                <div class="clear"></div>
            </div>
            
               <div class="form-sec-row">
                <label><strong>Admission Date :<span style="color:red">*</span></strong></label>
                <asp:TextBox ID="Calendar1" runat="server" CssClass="textbox-medium1" ></asp:TextBox>
                      <asp:Label ID="Label3" runat="server" ForeColor="Green" Text="(DD/MM/YYYY)"></asp:Label>
                <div class="clear"></div>
            </div>
            
            <div class="form-sec-row">
                <label><strong>Admission Time :<span style="color:red">*</span></strong></label>
                <asp:TextBox ID="time" runat="server" CssClass="textbox-medium1" ></asp:TextBox>
                <div class="clear"></div>
            </div>

                    <div class="form-sec-row">
                <label><strong>Type of Payment :</strong></label>
                   <asp:DropDownList ID="DropDownList5" runat="server" CssClass="combo-big1" 
                            AutoPostBack="True" onselectedindexchanged="DropDownList5_SelectedIndexChanged">
                      </asp:DropDownList>
                <div class="clear"></div>
            </div>
            <asp:Panel ID="Panel3" runat="server">
                <div class="form-sec-row">
                <label><strong>Insurance / RSBY  No :</strong></label>
                <asp:TextBox ID="TextBox22" runat="server" CssClass="textbox-medium1" ></asp:TextBox>
                <div class="clear"></div>
            </div>
            </asp:Panel>

             <div class="form-sec-row">
                <label><strong>Avanced Amount :</strong></label>
                <asp:TextBox ID="TextBox20" runat="server" CssClass="textbox-medium1" Style="text-align:right;" onFocus="this.select()"></asp:TextBox>
                <div class="clear"></div>
            </div>
            
        

            <div class="form-sec-row">
                <label><strong>&nbsp;</strong></label>
                <asp:Button ID="Button1" runat="server"  Height="30px"  Width="80px" OnClick="Button1_Click" Text="Submit" CssClass="submit-button" />
                <asp:Button ID="Button2" runat="server"   Width="80px"  Height="30px" Text="Cancel" CssClass="submit-button" 
                    onclick="Button2_Click" />
                <asp:Button ID="Button3" runat="server"  Height="30px" Text="Admission Slip" 
                    CssClass="submit-buttonCheck" onclick="Button3_Click" />
                <div class="clear"></div>
            </div>                     

            <div class="clear"></div>

                    <div class="error1" style='padding-left:300px;'>
                <strong><asp:Label ID="lblError" runat="server" Font-Bold="True"></asp:Label></strong>
                <div class="clear"></div>
            </div>
        </div>

         </div>


         <table width="100%">    <tr>        
                        <td style="width: 100%" >   <div id='mydiv'>            
                            <asp:Literal ID="ltrReport" runat="server"></asp:Literal><br />   </div>                 
                        </td>
                    </tr>

                     <tr>
                        <td align="right" colspan="3">
                            <asp:Button ID="Button10" runat="server" Text="Back" Font-Size="X-Small"  
                                Width="70px" onclick="Button10_Click"  />
                            <asp:Button ID="Button11" runat="server" Font-Size="X-Small"  Width="70px" Text="Print" OnClientClick="javascript:printDiv('mydiv')" />
                                 <asp:Button ID="Button12" runat="server" Font-Size="X-Small"  Width="70px" 
                                Text="Save" onclick="Button12_Click" />
                            </td>
                        </tr>
                    </table>    
                    
          </div> 


    </ContentTemplate>
    </asp:UpdatePanel>

</asp:Content>

