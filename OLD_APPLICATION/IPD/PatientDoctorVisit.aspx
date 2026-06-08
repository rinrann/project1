<%@ Page Title="" Language="C#" MasterPageFile="~/MasterAll/MasterPageAll.master" AutoEventWireup="true" CodeFile="PatientDoctorVisit.aspx.cs" Inherits="IPD_PatientDoctorVisit" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
      <script type="text/javascript" language="javascript">
          function isNumberKey(evt) {
              var charCode = (evt.which) ? evt.which : event.keyCode;
              if (charCode != 46 && charCode > 31
            && (charCode < 48 || charCode > 57))
                  return false;
              return true;
          }

          function ConfirmationMessagePres() {
              var data = confirm("Do you want to Stop this Prescription ? If yes Click Ok or Cancel");
              if (data) {
                  return true;
              } else {
                  return false;
              }
          }

          function ConfirmationMessageMed() {
              var data = confirm("Do you want to Stop this Medicine ? If yes Click Ok or Cancel");
              if (data) {
                  return true;
              } else {
                  return false;
              }
          }

          function expandcollapse(obj, row) {
              var div = document.getElementById(obj);
              var img = document.getElementById('img' + obj);

              if (div.style.display == "none") {
                  div.style.display = "block";
                  if (row == 'alt') {
                      img.src = "../Images/minus.gif";
                  }
                  else {
                      img.src = "../Images/minus.gif";
                  }
                  img.alt = "Close to view other Customers";
              }
              else {
                  div.style.display = "none";
                  if (row == 'alt') {
                      img.src = "../Images/plus.gif";
                  }
                  else {
                      img.src = "../Images/plus.gif";
                  }
                  img.alt = "Expand to show Orders";
              }
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
              document.getElementById("ctl00_ContentPlaceHolder1_TextBox1").value = time;
          }

          function ShowDialog() {
              var rtvalue = window.open("IpdpatientPopup.aspx", "sss", "Width:800px; Height:550px; dialogLeft:250px;");
              document.getElementById("ctl00_ContentPlaceHolder1_TextBox2").value = rtvalue.NameValue;

          }

          function ShowDialog1() {
              var rtvalue = window.open("ComplainPopup.aspx", "sss", "Width:800px; Height:550px; dialogLeft:250px;");
              document.getElementById("ctl00_ContentPlaceHolder1_HiddenField3").value = rtvalue.NameValue;
              document.getElementById("ctl00_ContentPlaceHolder1_TextBox21").value = rtvalue.ProfessionValue;

          }

          function ShowDialog2() {
              var rtvalue = window.open("ComplainPopup.aspx", "sss", "Width:800px; Height:550px; dialogLeft:250px;");
              document.getElementById("ctl00_ContentPlaceHolder1_HiddenField3").value = rtvalue.NameValue;
              document.getElementById("ctl00_ContentPlaceHolder1_TextBox22").value = rtvalue.ProfessionValue;

          }


          function Calling() {
              var date = new Date();
              $("input[id$='txtdate']").datepicker({ dateFormat: 'dd/mm/yy' });

              $("input[id$='TextBox8']").datepicker({ dateFormat: 'dd/mm/yy' });

              $('.DatepickerReCall').datepicker({ dateFormat: 'dd/mm/yy' });

              $('.time').timepicker({
                  showPeriod: true,
                  showLeadingZero: true
              });

              $("input[id$='TextBox1']").timepicker({
                  showPeriod: true,
                  showLeadingZero: true
              });


              $("input[id$='Button13']").click(function () {

                  if ($("select[id$='DropDownList1']").val() == '0') {
                      alert('Please Select Visiting  Doctor!');
                      $("select[id$='DropDownList1']").addClass('textboxerr');
                      $("select[id$='DropDownList1']").focus();
                      return false;
                  }
                  else {
                      $("select[id$='DropDownList1']").removeClass('textboxerr');
                  }


                  if ($("input[id$='TextBox136']").val() == '') {
                      alert('Please Enter Date !');
                      $("input[id$='TextBox136']").focus();
                      $("input[id$='TextBox136']").addClass('textboxerr');
                      return false;
                  }
                  else {
                      $("input[id$='TextBox136']").removeClass('textboxerr');
                  }


                  if ($("input[id$='TextBox23']").val() == '') {
                      alert('Please Enter Time !');
                      $("input[id$='TextBox23']").focus();
                      $("input[id$='TextBox23']").addClass('textboxerr');
                      return false;
                  }
                  else {
                      $("input[id$='TextBox23']").removeClass('textboxerr');
                  }
              });

              $("input[id$='Button8']").click(function () {
                  if ($("input[id$='HiddenField4']").val() == '') {
                      alert('Please select a Doctor from doctor visit  !');
                      return false;
                  }
              });

              $("input[id$='Button15']").click(function () {
                  if ($("input[id$='HiddenField4']").val() == '') {
                      alert('Please select a Doctor from doctor visit  !');
                      return false;
                  }


                  if ($("select[id$='ddlMedicineGroup1']").val() != '0') {
                      if ($("select[id$='ddlSubGroup1']").val() != '0') {
                          if ($("select[id$='ddlMedicine1']").val() != '0') {
                              if ($("select[id$='ddlDuration21']").val() == '0') {
                                  alert('Please Select Duration Please !');
                                  $("select[id$='ddlDuration21']").addClass('textboxerr');
                                  $("select[id$='ddlDuration21']").focus();
                                  return false;
                              }
                              else {
                                  $("select[id$='ddlDuration21']").removeClass('textboxerr');
                              }
                          }
                      }
                  }


                  if ($("select[id$='ddlMedicineGroup2']").val() != '0') {
                      if ($("select[id$='ddlSubGroup2']").val() != '0') {
                          if ($("select[id$='ddlMedicine2']").val() != '0') {
                              if ($("select[id$='ddlDuration22']").val() == '0') {
                                  alert('Please Select Duration Please !');
                                  $("select[id$='ddlDuration22']").addClass('textboxerr');
                                  $("select[id$='ddlDuration22']").focus();
                                  return false;
                              }
                              else {
                                  $("select[id$='ddlDuration22']").removeClass('textboxerr');
                              }
                          }
                      }
                  }


                  if ($("select[id$='ddlMedicineGroup3']").val() != '0') {
                      if ($("select[id$='ddlSubGroup3']").val() != '0') {
                          if ($("select[id$='ddlMedicine3']").val() != '0') {
                              if ($("select[id$='ddlDuration23']").val() == '0') {
                                  alert('Please Select Duration Please !');
                                  $("select[id$='ddlDuration23']").addClass('textboxerr');
                                  $("select[id$='ddlDuration23']").focus();
                                  return false;
                              }
                              else {
                                  $("select[id$='ddlDuration23']").removeClass('textboxerr');
                              }
                          }
                      }
                  }


                  if ($("select[id$='ddlMedicineGroup4']").val() != '0') {
                      if ($("select[id$='ddlSubGroup4']").val() != '0') {
                          if ($("select[id$='ddlMedicine4']").val() != '0') {
                              if ($("select[id$='ddlDuration24']").val() == '0') {
                                  alert('Please Select Duration Please !');
                                  $("select[id$='ddlDuration24']").addClass('textboxerr');
                                  $("select[id$='ddlDuration24']").focus();
                                  return false;
                              }
                              else {
                                  $("select[id$='ddlDuration24']").removeClass('textboxerr');
                              }
                          }
                      }
                  }


                  if ($("select[id$='ddlMedicineGroup5']").val() != '0') {
                      if ($("select[id$='ddlSubGroup5']").val() != '0') {
                          if ($("select[id$='ddlMedicine5']").val() != '0') {
                              if ($("select[id$='ddlDuration25']").val() == '0') {
                                  alert('Please Select Duration Please !');
                                  $("select[id$='ddlDuration25']").addClass('textboxerr');
                                  $("select[id$='ddlDuration25']").focus();
                                  return false;
                              }
                              else {
                                  $("select[id$='ddlDuration25']").removeClass('textboxerr');
                              }
                          }
                      }
                  }


                  if ($("select[id$='ddlMedicineGroup6']").val() != '0') {
                      if ($("select[id$='ddlSubGroup6']").val() != '0') {
                          if ($("select[id$='ddlMedicine6']").val() != '0') {
                              if ($("select[id$='ddlDuration26']").val() == '0') {
                                  alert('Please Select Duration Please !');
                                  $("select[id$='ddlDuration26']").addClass('textboxerr');
                                  $("select[id$='ddlDuration26']").focus();
                                  return false;
                              }
                              else {
                                  $("select[id$='ddlDuration26']").removeClass('textboxerr');
                              }
                          }
                      }
                  }


                  if ($("select[id$='ddlMedicineGroup7']").val() != '0') {
                      if ($("select[id$='ddlSubGroup7']").val() != '0') {
                          if ($("select[id$='ddlMedicine7']").val() != '0') {
                              if ($("select[id$='ddlDuration27']").val() == '0') {
                                  alert('Please Select Duration Please !');
                                  $("select[id$='ddlDuration27']").addClass('textboxerr');
                                  $("select[id$='ddlDuration27']").focus();
                                  return false;
                              }
                              else {
                                  $("select[id$='ddlDuration27']").removeClass('textboxerr');
                              }
                          }
                      }
                  }


                  if ($("select[id$='ddlMedicineGroup8']").val() != '0') {
                      if ($("select[id$='ddlSubGroup8']").val() != '0') {
                          if ($("select[id$='ddlMedicine8']").val() != '0') {
                              if ($("select[id$='ddlDuration28']").val() == '0') {
                                  alert('Please Select Duration Please !');
                                  $("select[id$='ddlDuration28']").addClass('textboxerr');
                                  $("select[id$='ddlDuration28']").focus();
                                  return false;
                              }
                              else {
                                  $("select[id$='ddlDuration28']").removeClass('textboxerr');
                              }
                          }
                      }
                  }

                  if ($("select[id$='ddlMedicineGroup9']").val() != '0') {
                      if ($("select[id$='ddlSubGroup9']").val() != '0') {
                          if ($("select[id$='ddlMedicine9']").val() != '0') {
                              if ($("select[id$='ddlDuration29']").val() == '0') {
                                  alert('Please Select Duration Please !');
                                  $("select[id$='ddlDuration29']").addClass('textboxerr');
                                  $("select[id$='ddlDuration29']").focus();
                                  return false;
                              }
                              else {
                                  $("select[id$='ddlDuration29']").removeClass('textboxerr');
                              }
                          }
                      }
                  }


                  if ($("select[id$='ddlMedicineGroup10']").val() != '0') {
                      if ($("select[id$='ddlSubGroup10']").val() != '0') {
                          if ($("select[id$='ddlMedicine10']").val() != '0') {
                              if ($("select[id$='ddlDuration30']").val() == '0') {
                                  alert('Please Select Duration Please !');
                                  $("select[id$='ddlDuration30']").addClass('textboxerr');
                                  $("select[id$='ddlDuration30']").focus();
                                  return false;
                              }
                              else {
                                  $("select[id$='ddlDuration30']").removeClass('textboxerr');
                              }
                          }
                      }
                  }
              });


              $("input[id$='Button5']").click(function () {
                  if ($("input[id$='HiddenField4']").val() == '') {
                      alert('Please select a Doctor from doctor visit  !');
                      return false;
                  }


              });

              $("input[id$='Button9']").click(function () {
                  if ($("input[id$='TextBox9']").val() == '') {
                      alert('Please Enter Date !');
                      $("input[id$='TextBox9']").focus();
                      $("input[id$='TextBox9']").addClass('textboxerr');
                      return false;
                  }
                  else {
                      $("input[id$='TextBox9']").removeClass('textboxerr');
                  }


                  if ($("input[id$='TextBox24']").val() == '') {
                      alert('Please Enter Time !');
                      $("input[id$='TextBox24']").focus();
                      $("input[id$='TextBox24']").addClass('textboxerr');
                      return false;
                  }
                  else {
                      $("input[id$='TextBox24']").removeClass('textboxerr');

                  }

                  if (parseFloat($("input[id$='TextBox13']").val()) > 100) {
                      alert('SPO2 Will be not more than 100 !');
                      
                      $("input[id$='TextBox13']").focus();
                      $("input[id$='TextBox13']").addClass('textboxerr');
                      return false;
                  }
                  else {
                      $("input[id$='TextBox13']").removeClass('textboxerr');
                  }

                  var dplrid = GetClientID("TextBox16");
                  var dplr = document.getElementById(dplrid).value;
                  if (parseFloat(dplr) > 300) {
                      alert('Doppler will be not more than 300 !');
                      $("input[id$='TextBox16']").focus();
                      $("input[id$='TextBox16']").addClass('textboxerr');
                      return false;
                  }

                 /* if ($("input[id$='TextBox16']").val() > '300') {
                      alert('Doppler will be not more than 300 !');
                      $("input[id$='TextBox16']").focus();
                      $("input[id$='TextBox16']").addClass('textboxerr');
                      return false;
                  }*/
                  else {
                      $("input[id$='TextBox16']").removeClass('textboxerr');
                  }

              });



              $("input[id$='Tab1']").click(function () {
                  if ($("input[id$='TextBox2']").val() == '') {
                      alert('Please Enter Registration No !');
                      $("input[id$='TextBox2']").focus();
                      $("input[id$='TextBox2']").addClass('textboxerr');
                      return false;
                  }
                  else {
                      $("input[id$='TextBox2']").removeClass('textboxerr');
                  }
              });


              $("input[id$='Tab5']").click(function () {


                  if ($("input[id$='TextBox2']").val() == '') {
                      alert('Please Enter Registration No !');
                      $("input[id$='TextBox2']").focus();
                      $("input[id$='TextBox2']").addClass('textboxerr');
                      return false;
                  }
                  else {
                      $("input[id$='TextBox2']").removeClass('textboxerr');
                  }
              });


              $("input[id$='Tab2']").click(function () {


                  if ($("input[id$='TextBox2']").val() == '') {
                      alert('Please Enter Registration No !');
                      $("input[id$='TextBox2']").focus();
                      $("input[id$='TextBox2']").addClass('textboxerr');
                      return false;
                  }
                  else {
                      $("input[id$='TextBox2']").removeClass('textboxerr');
                  }
              });


              $("input[id$='Tab3']").click(function () {


                  if ($("input[id$='TextBox2']").val() == '') {
                      alert('Please Enter Registration No !');
                      $("input[id$='TextBox2']").focus();
                      $("input[id$='TextBox2']").addClass('textboxerr');
                      return false;
                  }
                  else {
                      $("input[id$='TextBox2']").removeClass('textboxerr');
                  }
              });


              $("input[id$='Tab7']").click(function () {


                  if ($("input[id$='TextBox2']").val() == '') {
                      alert('Please Enter Registration No !');
                      $("input[id$='TextBox2']").focus();
                      $("input[id$='TextBox2']").addClass('textboxerr');
                      return false;
                  }
                  else {
                      $("input[id$='TextBox2']").removeClass('textboxerr');
                  }
              });

              $("input[id$='Tab4']").click(function () {


                  if ($("input[id$='TextBox2']").val() == '') {
                      alert('Please Enter Registration No !');
                      $("input[id$='TextBox2']").focus();
                      $("input[id$='TextBox2']").addClass('textboxerr');
                      return false;
                  }
                  else {
                      $("input[id$='TextBox2']").removeClass('textboxerr');
                  }
              }); 
               


              $("input[id$='Button1']").click(function () {

                  if ($("select[id$='DropDownList1']").val() == '0') {
                      alert('Please Select Visiting  Doctor!');
                      $("select[id$='DropDownList1']").addClass('textboxerr');
                      $("select[id$='DropDownList1']").focus();
                      return false;
                  }
                  else {
                      $("select[id$='DropDownList1']").removeClass('textboxerr');
                  }

                  if ($("input[id$='txtdate']").val() == '') {
                      alert('Please Enter Visiting Date Properly!');
                      $("input[id$='txtdate']").focus();
                      $("input[id$='txtdate']").addClass('textboxerr');
                      return false;
                  }
                  else {
                      $("input[id$='txtdate']").removeClass('textboxerr');
                  }

                  if ($("input[id$='TextBox1']").val() == '') {
                      alert('Please Enter Time of Visit Properly!');
                      $("input[id$='TextBox1']").focus();
                      $("input[id$='TextBox1']").addClass('textboxerr');
                      return false;
                  }
                  else {
                      $("input[id$='TextBox1']").removeClass('textboxerr');
                  }
              });

              $('.NumberInput').keydown(function (event) {
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

          function GetClientID(asp_net_id) {
              return $("[id$=" + asp_net_id + "]").attr("id");
          };
</script>
 
  <asp:UpdateProgress ID="UpdateProgress1" runat="server">
    <ProgressTemplate>
    
       <div id="aaa" class="progressBackgroundFilter">          </div>
        <div id="bbb" class="processMessage"><img alt="Loading" src="../images/pwait.gif"/>
      
              </div>
    </ProgressTemplate>
    </asp:UpdateProgress>

    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
    <ContentTemplate>
    <div class="pageheader">
                <asp:Label ID="Label1" runat="server">Doctor Visit</asp:Label>
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
                    <asp:HiddenField ID="HiddenField1" runat="server" Value="0" />
                      <asp:HiddenField ID="HiddenField4" runat="server" Value="0" />
                      <asp:HiddenField ID="HiddenField2" runat="server" Value="0" /><asp:HiddenField ID="HiddenField3" runat="server" Value="0" />

           
                      <div class="form-sec-row">
                        <label>
                        <strong>
                       Registration No :</strong></label>
                          <asp:TextBox ID="TextBox2" runat="server" CssClass="textbox-medium1" 
                              Enabled="False"></asp:TextBox>
                                <asp:Button ID="Button3" runat="server" Text="Quick Search" Height="28px" CssClass="submit-buttonCheck" OnClientClick="ShowDialog()"/>
                              <asp:Button ID="Button4" runat="server" Text="Fetch"   Height="28px" 
                               CssClass="submit-button" onclick="Button4_Click"/>
                        <div class="clear">
                        </div>
                    </div>
                        <div class="form-sec-row">
                        <label>
                        <strong>
                       Patient's Name :</strong></label>
                          <asp:TextBox ID="TextBox3" runat="server" CssClass="textbox-medium1" 
                                Enabled="False"></asp:TextBox>
                        <div class="clear">
                        </div>
                    </div>

                        <div class="form-sec-row">
                        <label>
                        <strong>
                       Bed No :</strong></label>
                          <asp:TextBox ID="TextBox4" runat="server" CssClass="textbox-medium1" 
                                Enabled="False"></asp:TextBox>
                        <div class="clear">
                        </div>
                    </div>

                        <div class="form-sec-row">
                        <label>
                        <strong>
                       Admission Date :</strong></label>
                          <asp:TextBox ID="TextBox5" runat="server" CssClass="textbox-medium1" 
                                Enabled="False" EnableTheming="True"></asp:TextBox>
                        <div class="clear">
                        </div>
                    </div>

    <table width="290px" cellpadding="0" cellspacing="0">
         <tr>
         <td>
                <asp:Button Text="Visit Details" BorderStyle="None" ID="Tab6" CssClass="Initial"  
                              Width="130px"  Height="40px" 
                    runat="server" onclick="Tab6_Click" /></td>

                    <td>
                <asp:Button Text="TreateMent Note" BorderStyle="None" ID="Tab7" CssClass="Initial"  
                              Width="140px"  Height="40px" 
                    runat="server" onclick="Tab7_Click"  /></td>

            <td>
                <asp:Button Text="Clinical Finding" BorderStyle="None" ID="Tab1" Width="145px"  Height="40px" 
                    CssClass="Initial" runat="server" onclick="Tab1_Click"
                     /></td>

                         <td>
                <asp:Button Text="BHT Record" BorderStyle="None" ID="Tab5" CssClass="Initial"  
                                 Width="130px"  Height="40px" 
                    runat="server" onclick="Tab5_Click" /></td>

                     <td>
                <asp:Button Text="Prescription" BorderStyle="None" ID="Tab2" CssClass="Initial"  Width="120px"  Height="40px" 
                    runat="server" onclick="Tab2_Click"
                    /></td>

                      <td>
                <asp:Button Text="Services" BorderStyle="None" ID="Tab3" CssClass="Initial"  
                              Width="130px"  Height="40px" 
                    runat="server" onclick="Tab3_Click" /></td>

                      <td>
                <asp:Button Text="Consumable" BorderStyle="None" ID="Tab4" CssClass="Initial"  
                              Width="130px"  Height="40px" 
                    runat="server" onclick="Tab4_Click"  /></td>

                                
                     </tr>
                     </table>

<div class="formbox" style='width:99%;'>
 <asp:MultiView ID="MainView" runat="server">
                    <asp:View ID="View1" runat="server">
                         

                        <div style="width:100%;overflow:auto;">
                                <table border="1" width="100%" cellpadding="0" cellspacing="0">
                            <tr>
                            <td colspan="11">
                                                  
                                                  <div class="pageheader">
         <asp:Label ID="Label10" runat="server"> Patient Clinical Finding </asp:Label>
     </div>
           
                            </td>
                            </tr>
           
       <tr style='background-color:#FF9300;'>
                <td align="center" style='width:100px;'>
                 
             <label ><strong>Date</strong></label> 
            
        </td> 

          <td align="center" style='width:100px;'>
                 
             <label ><strong>Time</strong></label> 
            
        </td> 

                <td align="center">
                   
             <label ><strong>Complain</strong></label>
           
</td>
<td></td>
 <td align="center" style='width:90px;'>
               
             <label ><strong>BP</strong></label> 
          
            
</td>

                <td align="center" style='width:100px;'>
    
             <label ><strong> Pulse </strong></label>
                       
</td>

      <td align="center">
                   
             <label ><strong> Chest </strong></label>
                      
</td>
 <td align="center">
                   
             <label ><strong>P/A</strong></label> 
          
            
</td>
 <td align="center">
                  
             <label ><strong>P/V</strong></label> 
        
            
</td>

 <td align="center">
                  
             <label ><strong>Others</strong></label> 
          
            
</td>
        <td></td>
                      
            </tr>
                <tr>
                <td align="center"  style='width:100px;'>
                 
             <asp:TextBox ID="TextBox136" runat="server"  CssClass="DatepickerReCall"  Width="100px"></asp:TextBox>
           
            
</td> 


<td align="center" style='width:50px;'>
                  
                     <asp:TextBox ID="TextBox23" CssClass="time" runat="server" Width="100px"></asp:TextBox>
           
</td>


                <td align="center" style='width:280px;'>
                       <asp:TextBox ID="TextBox21" runat="server" TextMode="MultiLine" Height="30px" Enabled="false"  Width="280px"></asp:TextBox>
                           
</td>
<td style='width:60px;'  align="center">  <asp:LinkButton ID="LinkButton2" runat="server" OnClientClick="ShowDialog1()">Add Complain</asp:LinkButton></td>
 <td align="center" style='width:90px;'> 
                       <asp:TextBox ID="TextBox139" runat="server" MaxLength="3"  Width="30px"></asp:TextBox> 

                           <asp:TextBox ID="TextBox25" MaxLength="3"  runat="server" Width="30px"></asp:TextBox> 
</td>
                <td align="center" style='width:100px;'>
                   
          <asp:TextBox ID="TextBox138" runat="server"  MaxLength="3" Width="60px"></asp:TextBox>
                          
</td>



 <td align="center">
                    
             <asp:TextBox ID="TextBox142" runat="server" MaxLength="4"  Width="50px"></asp:TextBox>
            
            
</td>

 <td align="center">
                    
             <asp:TextBox ID="TextBox143" runat="server"  Width="50px"></asp:TextBox>
            
            
</td>
 <td align="center">
                    
             <asp:TextBox ID="TextBox144" runat="server"  Width="50px"></asp:TextBox>
            
            
</td>

 <td align="center">
                    
             <asp:TextBox ID="TextBox146" runat="server"  Width="80px"></asp:TextBox>
            
            
</td>
<td align="center" style='width:120px;'>
  <asp:Button ID="Button13" runat="server"  Text="Submit" CssClass="submit-button" 
                    Height="28px" onclick="Button13_Click"  />
</td>
 </tr>                      
                        </table>
                        </div>

                        <table border="1" cellpadding="0" cellspacing="0" width="100%">
                            <tr>
                                <td colspan="5">
                      
                        
                         <div class="pageheader">
         <asp:Label ID="Label9" runat="server"> Patient Previous Clinical Finding</asp:Label>
     </div>
                                         <div class="listing"  style='width:100%; height:150px; overflow:auto;'>
         <asp:GridView id="GridView4"  CssClass="grid" PagerStyle-CssClass="pgr" DataKeyNames ="RowID" 
                                            runat="server" AutoGenerateColumns="False"   AllowPaging="True" PageSize ="70"              
              SelectedRowStyle-BackColor="GreenYellow" onpageindexchanging="GridView4_PageIndexChanging"  Width="100%"
                                                 onrowdeleting="GridView4_RowDeleting" 
                                                 onrowcommand="GridView4_RowCommand"   >
                <RowStyle HorizontalAlign="Center" />
                <Columns>
                 

                     <asp:TemplateField HeaderText="ID" Visible ="false"><ItemTemplate><asp:Label ID="lblid" runat="server" Text='<%# Bind("RowId") %>'></asp:Label></ItemTemplate></asp:TemplateField>
                    <asp:TemplateField HeaderText="Date" ItemStyle-Width="70px"><ItemTemplate><asp:Label ID="lbldate" runat="server" Text='<%# Bind("Date") %>'></asp:Label></ItemTemplate></asp:TemplateField>

                       <asp:TemplateField HeaderText="Time" ItemStyle-Width="70px"><ItemTemplate><asp:Label ID="lblTime" runat="server" Text='<%# Bind("Time") %>'></asp:Label></ItemTemplate></asp:TemplateField>


                    <asp:TemplateField HeaderText="Complain"   ItemStyle-Width="120px"><ItemTemplate><asp:Label ID="lblComplainName" runat="server" Text='<%# Bind("ComplainName") %>'></asp:Label></ItemTemplate></asp:TemplateField>
                    
                 <asp:TemplateField HeaderText="BP"  ItemStyle-Width="50px"><ItemTemplate><asp:Label ID="lblBP" runat="server" Text='<%# Bind("BP") %>'></asp:Label></ItemTemplate></asp:TemplateField>
                          

                    <asp:TemplateField HeaderText="Pulse"  ItemStyle-Width="50px"><ItemTemplate><asp:Label ID="lblPulse" runat="server" Text='<%# Bind("Pulse") %>'></asp:Label></ItemTemplate></asp:TemplateField> 

                    <asp:TemplateField HeaderText="Chest"  ItemStyle-Width="50px"><ItemTemplate><asp:Label ID="lblChest" runat="server" Text='<%# Bind("Chest") %>'></asp:Label></ItemTemplate></asp:TemplateField> 
                       
                    <asp:TemplateField HeaderText="PA"  ItemStyle-Width="50px"><ItemTemplate><asp:Label ID="lblpa" runat="server" Text='<%# Bind("PA") %>'></asp:Label></ItemTemplate></asp:TemplateField>

                      <asp:TemplateField HeaderText="PV"  ItemStyle-Width="50px" ><ItemTemplate><asp:Label ID="lblpv" runat="server" Text='<%# Bind("PV") %>'></asp:Label></ItemTemplate></asp:TemplateField>

 

                      <asp:TemplateField HeaderText="Others"  ItemStyle-Width="50px" ><ItemTemplate><asp:Label ID="lblothers" runat="server" Text='<%# Bind("Others") %>'></asp:Label></ItemTemplate></asp:TemplateField>
              
                <asp:CommandField SelectText="Edit" ShowSelectButton="True"  ItemStyle-Width="50px"  ControlStyle-CssClass="temp" HeaderText="Edit" SelectImageUrl="~/Images/edit.png" ButtonType="Image"><ControlStyle CssClass="temp"></ControlStyle></asp:CommandField>

                      <asp:CommandField ShowDeleteButton="True" ControlStyle-CssClass="temp"  ItemStyle-Width="50px"  HeaderText="Delete" DeleteImageUrl="~/Images/delete.png" ButtonType="Image"><ControlStyle CssClass="temp"></ControlStyle></asp:CommandField>

                </Columns>
                <PagerStyle CssClass="pgr" />
                <EditRowStyle BackColor="#CCFF33" />
                <AlternatingRowStyle BackColor="#FFDB91" />
                <SelectedRowStyle BackColor="GreenYellow" />
        </asp:GridView> 
        </div>
         
                                       </td>
                            </tr>

                            </table>
</asp:View>

<asp:View ID="View5" runat="server">
                         

                        <div style="width:100%;overflow:auto;">
                                <table border="1" cellpadding="0" cellspacing="0">
                            <tr>
                            <td colspan="11">
                                                  
                                                  <div class="pageheader">
         <asp:Label ID="Label8" runat="server"> Patient BHT Record </asp:Label>
     </div>
           
                            </td>
                            </tr>
           
       <tr style='background-color:#FF9300;'>
                <td align="center" style='width:100px;'>
                 
             <label ><strong>Date</strong></label> 
            
        </td> 

                <td align="center">
                   
             <label ><strong>Time</strong></label>
           
</td>
     <td align="center">
                   
             <label ><strong>Complain</strong></label>
           
</td>
<td></td>
 <td align="center" style='width:80px;'>
               
             <label ><strong>BP</strong></label> 
          
            
</td>

                <td align="center" style='width:100px;'>
    
             <label ><strong> Pulse </strong></label>
                       
</td>

 <td align="center" style='width:40px;'>
                   
             <label><strong>Temp</strong></label> 
             
            
</td>



                <td align="center">
                   
             <label ><strong>SPO2</strong></label>
           
</td>
                <td align="center">
                   
             <label ><strong> Urine </strong></label>
                      
</td>
 <td align="center">
                   
             <label ><strong>Suction</strong></label> 
          
            
</td>
 <td align="center">
                  
             <label ><strong>Doppler</strong></label> 
        
            
</td>

 <td align="center">
                 
             <label ><strong>Bleeding</strong></label> 
           
            
</td>
 <td align="center">
                  
             <label ><strong>Others</strong></label> 
          
            
</td>
        
<td></td>
                      
            </tr>
                <tr>
                <td align="center"  style='width:100px;'>
                 
             <asp:TextBox ID="TextBox9" runat="server"  CssClass="DatepickerReCall"  Width="100px"></asp:TextBox>
           
            
</td> 

       <td align="center">
                  
                     <asp:TextBox ID="TextBox24" runat="server" CssClass="time"  Width="80px"></asp:TextBox>
           
</td>
                <td align="center">
                  
                     <asp:TextBox ID="TextBox22" runat="server" TextMode="MultiLine" Height="30px" Enabled="false"  Width="280px"></asp:TextBox>
           
</td>

        

<td style='width:60px;'  align="center">  <asp:LinkButton ID="LinkButton3" runat="server" OnClientClick="ShowDialog2()">Add Complain</asp:LinkButton></td>

                <td align="center" style='width:100px;'>
                   
          <asp:TextBox ID="TextBox10" runat="server" MaxLength="3"  Width="40px"></asp:TextBox>
             <asp:TextBox ID="TextBox27" runat="server" MaxLength="3"  Width="40px"></asp:TextBox>
                          
</td>
 <td align="center" style='width:80px;'>
                   
                       <asp:TextBox ID="TextBox11" MaxLength="3" runat="server" Width="80px"></asp:TextBox>
           
            
</td>
 <td align="center">
                    
             <asp:TextBox ID="TextBox12" runat="server" MaxLength="5"  Width="60px"></asp:TextBox>
            
            
</td>

 <td align="center">
                    
             <asp:TextBox ID="TextBox13" runat="server" MaxLength="3"   Width="80px"></asp:TextBox>
            
            
</td>

 <td align="center">
                    
             <asp:TextBox ID="TextBox14" runat="server"  MaxLength="4" Width="40px"></asp:TextBox>
            
            
</td>

 <td align="center">
                    
             <asp:TextBox ID="TextBox15" runat="server"   MaxLength="4" Width="70px"></asp:TextBox>
            
            
</td>
 <td align="center">
                    
             <asp:TextBox ID="TextBox16" runat="server" MaxLength="4"  Width="70px"></asp:TextBox>
            
            
</td>

 <td align="center">
                    
             <asp:TextBox ID="TextBox17" runat="server"  Width="70px"></asp:TextBox>
            
            
</td>

 <td align="center">
                    
             <asp:TextBox ID="TextBox18" runat="server"  Width="90px"></asp:TextBox>
            
            
</td>
<td align="center" style='width:120px;'>
  <asp:Button ID="Button9" runat="server"  Text="Submit" CssClass="submit-button" 
        Height="26px" onclick="Button9_Click"   />
    <br />
</td>
 </tr>
 </table>
                        </div>

                        <table border="1" cellpadding="0" cellspacing="0" width="100%">
                            <tr>
                                <td colspan="5">
                      
                        
                         <div class="pageheader">
         <asp:Label ID="Label7" runat="server"> BHT Record Details</asp:Label>
     </div>
                                         <div class="listing"  style='width:100%; height:150px; overflow:auto;'>
         <asp:GridView id="GridView6"  CssClass="grid" PagerStyle-CssClass="pgr" DataKeyNames ="RowID" 
                                            runat="server" AutoGenerateColumns="False"   AllowPaging="True" PageSize ="70"              
              SelectedRowStyle-BackColor="GreenYellow"  Width="100%"
                                                 onrowdeleting="GridView6_RowDeleting" 
                                                 onrowcommand="GridView6_RowCommand"   >
                <RowStyle HorizontalAlign="Center" />
                <Columns> 

                     <asp:TemplateField HeaderText="ID" Visible ="false"><ItemTemplate><asp:Label ID="lblid" runat="server" Text='<%# Bind("RowId") %>'></asp:Label></ItemTemplate></asp:TemplateField>


                    <asp:TemplateField HeaderText="Date"><ItemTemplate><asp:Label ID="lbldate" runat="server" Text='<%# Bind("Date") %>'></asp:Label></ItemTemplate></asp:TemplateField>

                         <asp:TemplateField HeaderText="Time"><ItemTemplate><asp:Label ID="lblTime" runat="server" Text='<%# Bind("Time") %>'></asp:Label></ItemTemplate></asp:TemplateField>

                        <asp:TemplateField HeaderText="Complain"><ItemTemplate><asp:Label ID="lblComplainName" runat="server" Text='<%# Bind("ComplainName") %>'></asp:Label></ItemTemplate></asp:TemplateField>

                      <asp:TemplateField HeaderText="BP"><ItemTemplate><asp:Label ID="lblBP" runat="server" Text='<%# Bind("BP") %>'></asp:Label></ItemTemplate></asp:TemplateField>

                      <asp:TemplateField HeaderText="Pulse"><ItemTemplate><asp:Label ID="lblPulse" runat="server" Text='<%# Bind("Pulse") %>'></asp:Label></ItemTemplate></asp:TemplateField>

                      <asp:TemplateField HeaderText="Temp"><ItemTemplate><asp:Label ID="lblTemp" runat="server" Text='<%# Bind("Temp") %>'></asp:Label></ItemTemplate></asp:TemplateField>

                      <asp:TemplateField HeaderText="SPO2"><ItemTemplate><asp:Label ID="lblSPO2" runat="server" Text='<%# Bind("SPO2") %>'></asp:Label></ItemTemplate></asp:TemplateField>

                      <asp:TemplateField HeaderText="Urin"><ItemTemplate><asp:Label ID="lblUrin" runat="server" Text='<%# Bind("Urin") %>'></asp:Label></ItemTemplate></asp:TemplateField>

                    
                      <asp:TemplateField HeaderText="Sunction"><ItemTemplate><asp:Label ID="lblSunction" runat="server" Text='<%# Bind("Sunction") %>'></asp:Label></ItemTemplate></asp:TemplateField>

                   
                   
                      <asp:TemplateField HeaderText="Doppler"><ItemTemplate><asp:Label ID="lblDoppler" runat="server" Text='<%# Bind("Doppler") %>'></asp:Label></ItemTemplate></asp:TemplateField>
                    
                      <asp:TemplateField HeaderText="Bleeding"><ItemTemplate><asp:Label ID="lblBleeding" runat="server" Text='<%# Bind("Bleeding") %>'></asp:Label></ItemTemplate></asp:TemplateField>

                          <asp:TemplateField HeaderText="Others"><ItemTemplate><asp:Label ID="lblOthers" runat="server" Text='<%# Bind("Others") %>'></asp:Label></ItemTemplate></asp:TemplateField>
                     
     
                <asp:CommandField SelectText="Edit" ShowSelectButton="True"  ItemStyle-Width="50px"  ControlStyle-CssClass="temp" HeaderText="Edit" SelectImageUrl="~/Images/edit.png" ButtonType="Image"><ControlStyle CssClass="temp"></ControlStyle></asp:CommandField>

                      <asp:CommandField ShowDeleteButton="True" ControlStyle-CssClass="temp"  ItemStyle-Width="50px"  HeaderText="Delete" DeleteImageUrl="~/Images/delete.png" ButtonType="Image"><ControlStyle CssClass="temp"></ControlStyle></asp:CommandField>

                </Columns>
                <PagerStyle CssClass="pgr" />
                <EditRowStyle BackColor="#CCFF33" />
                <AlternatingRowStyle BackColor="#FFDB91" />
                <SelectedRowStyle BackColor="GreenYellow" />
        </asp:GridView> 
        </div> 
                                       </td>
                            </tr>

                            </table>
</asp:View> 
   <asp:View ID="View2" runat="server">
      <asp:Panel ID="Panel1" runat="server">
      <table border="1" cellpadding="0" cellspacing="0" width="100%">
    <tr>
    <td>
           <div class="pageheader">
         <asp:Label ID="Label11" runat="server"> Patient Previous Prescription Report </asp:Label>
     </div>
  <div class="listing" style='width:100%; height:150px; overflow:auto;'>
         <asp:GridView id="GridView5"  CssClass="grid" PagerStyle-CssClass="pgr" DataKeyNames ="presRow,MapRowid" 
                                            runat="server" AutoGenerateColumns="False"
          AllowPaging="True" PageSize ="50"  Width="100%"
                 
              SelectedRowStyle-BackColor="GreenYellow" onpageindexchanging="GridView5_PageIndexChanging" 
                                            onrowdatabound="GridView5_RowDataBound" 
                                            onrowcancelingedit="GridView5_RowCancelingEdit" 
                                            onrowdeleting="GridView5_RowDeleting" onrowediting="GridView5_RowEditing" 
                                            onrowupdating="GridView5_RowUpdating" 
                                            >
                <RowStyle HorizontalAlign="Center" />
                <Columns>
                 

                      <asp:TemplateField HeaderText="Id" Visible ="false"><ItemTemplate><asp:Label ID="lblid" runat="server"
                   Text='<%# Bind("MapRowid") %>'></asp:Label></ItemTemplate></asp:TemplateField>

                       <asp:TemplateField HeaderText="Prescription Id"><ItemTemplate><asp:Label ID="lblpid" runat="server" 
                       Text='<%# Bind("PrescriptionID") %>'></asp:Label></ItemTemplate></asp:TemplateField>

                    <asp:TemplateField HeaderText="Date"><ItemTemplate><asp:Label ID="lbldate" runat="server"
                     Text='<%# Bind("Date1") %>'></asp:Label></ItemTemplate><EditItemTemplate><asp:TextBox ID="txtdate"
                      runat="server" Width="110px" Text='<%# Bind("Date1") %>'>
                    </asp:TextBox></EditItemTemplate></asp:TemplateField>
                  

                      <asp:TemplateField HeaderText="Doctor Id" Visible="false"><ItemTemplate><asp:Label ID="lbldoc_id" runat="server" Text='<%# Bind("DoctorId") %>'></asp:Label></ItemTemplate></asp:TemplateField>


                    <asp:TemplateField HeaderText="Doctor"><ItemTemplate><asp:Label ID="lbldoc_name" runat="server" Text='<%# Bind("doc_name") %>'></asp:Label></ItemTemplate><EditItemTemplate><asp:DropDownList ID="ddlDoctor"  Width="110px"    runat="server"></asp:DropDownList></EditItemTemplate></asp:TemplateField>

                        <asp:TemplateField HeaderText="Medicine Group Id" Visible="false"><ItemTemplate><asp:Label ID="lblMedicineGroupID" 
                        runat="server" Text='<%# Bind("GroupID") %>'>
                        </asp:Label></ItemTemplate></asp:TemplateField>
 
                      <asp:TemplateField HeaderText="Medicine Group"><ItemTemplate><asp:Label ID="lblgroup" runat="server" 
                      Text='<%# Bind("MedicineGroupName") %>'></asp:Label></ItemTemplate><EditItemTemplate><asp:DropDownList ID="ddlgroup"   Width="110px"  
                       runat="server" AutoPostBack="True" onselectedindexchanged="ddlgroup_SelectedIndexChanged"></asp:DropDownList></EditItemTemplate></asp:TemplateField> 



                        <asp:TemplateField HeaderText="Medicine Sub Group Id" Visible="false"><ItemTemplate><asp:Label ID="lblMedicineSubGroupID"
                         runat="server" Text='<%# Bind("SubGroupId") %>'></asp:Label></ItemTemplate></asp:TemplateField>


                      <asp:TemplateField HeaderText="Sub Group"><ItemTemplate><asp:Label ID="lblsubgroup" runat="server" 
                      Text='<%# Bind("SubGrName") %>'></asp:Label></ItemTemplate><EditItemTemplate><asp:DropDownList ID="ddlsub" runat="server" 
                       Width="110px"  AutoPostBack="True" onselectedindexchanged="ddlsub_SelectedIndexChanged"></asp:DropDownList></EditItemTemplate></asp:TemplateField> 


                       <asp:TemplateField HeaderText="Medicine Id" Visible="false"><ItemTemplate><asp:Label ID="lblMedicineID" runat="server" 
                       Text='<%# Bind("MedicineId") %>'></asp:Label></ItemTemplate></asp:TemplateField> 

                    <asp:TemplateField HeaderText="Medicine"><ItemTemplate><asp:Label ID="lblmedicine" runat="server" Text='<%# Bind("MedicineName") %>'>
                    </asp:Label></ItemTemplate><EditItemTemplate><asp:DropDownList ID="ddlmed" runat="server"   AutoPostBack="True"
                     onselectedindexchanged="ddlmed_SelectedIndexChanged"></asp:DropDownList></EditItemTemplate></asp:TemplateField>   
                                        
                    <asp:TemplateField HeaderText="Actual Qty"><ItemTemplate><asp:Label ID="lblActQty" runat="server" Text='<%# Bind("ActualQty") %>'>
                    </asp:Label></ItemTemplate><EditItemTemplate><asp:TextBox ID="txtActQty" runat="server"  Width="90px"  Text='<%# Bind("ActualQty") %>'> 
                    </asp:TextBox></EditItemTemplate></asp:TemplateField>

                    <asp:TemplateField HeaderText="Bill Qty"><ItemTemplate><asp:Label ID="lblBillQty" runat="server" Text='<%# Bind("BillQty") %>'>
                    </asp:Label></ItemTemplate><EditItemTemplate><asp:TextBox ID="txtBillQty" runat="server"  Width="90px"  Text='<%# Bind("BillQty") %>'> 
                    </asp:TextBox></EditItemTemplate></asp:TemplateField>
                    
                    <asp:TemplateField HeaderText="Duration in Days"><ItemTemplate><asp:Label ID="lblDuration" runat="server" Text='<%# Bind("Duration") %>'>
                    </asp:Label></ItemTemplate><EditItemTemplate><asp:TextBox ID="txtDuration" runat="server"  Width="90px"  Text='<%# Bind("Duration") %>'> 
                    </asp:TextBox></EditItemTemplate></asp:TemplateField> 
                       
                    <asp:CommandField HeaderText="Edit" ShowEditButton="True" />
                    <asp:CommandField HeaderText="Delete" ShowDeleteButton="True" />
                </Columns>
                <PagerStyle CssClass="pgr" />
                <EditRowStyle BackColor="#CCFF33" />
                <AlternatingRowStyle BackColor="#FFDB91" />
                <SelectedRowStyle BackColor="GreenYellow" />
        </asp:GridView> 
        </div>
          </td>
    </tr>
    </table>
                                   
                        <table border="1" cellpadding="0" style='background-color:#C5B9B9;' cellspacing="0" width="100%">
                        <tr>
                        <td colspan="2" align="center">
                            <div class="pageheader">
         <asp:Label ID="Label2" runat="server"> Patient Prescription </asp:Label>
     </div>
                        </td>
                        </tr>
          <tr>

           <td><strong>Template Group:</strong> 
                  <asp:DropDownList ID="DropDownList34" runat="server" AutoPostBack="True" 
                   Width="180px" 
                   onselectedindexchanged="DropDownList34_SelectedIndexChanged" >
                  </asp:DropDownList> </td>
              <td><strong>Template :</strong> 
                  <asp:DropDownList ID="DropDownList35" runat="server" AutoPostBack="True" 
                      Width="180px" onselectedindexchanged="DropDownList35_SelectedIndexChanged" 
                     >
                  </asp:DropDownList> </td>
</tr>
</table>

                        </asp:Panel> 

                        <asp:Panel ID="Panel2" runat="server">
                        

             <table border="1" cellpadding="0" style='background-color:#C5B9B9;' cellspacing="0" width="100%">
          <tr>
          <td><strong>Prescription No :</strong> 
              <asp:TextBox ID="TextBox201" runat="server" Enabled="False"></asp:TextBox> </td>
              <td><strong>Date :</strong> <asp:TextBox ID="TextBox202" CssClass="DatepickerReCall"  Width="125px"  runat="server"></asp:TextBox> </td>
            
          
          </tr>
          </table>
               
               <div style='width:100%; overflow:auto;'>
                        <table border="1" cellpadding="0" cellspacing="0" width="100%">
       <tr style='background-color:#FF9300;'>
                <td align="center">
           
             <label><strong>Group</strong></label> 
      
        </td> 

                <td align="center">
       
             <label ><strong>Sub Group</strong></label>
        
</td>
                <td align="center">
                       
             <label ><strong> Medicine </strong></label>
                   
</td>
     <td align="center">
                          
             <label ><strong> Dose </strong></label>
                       
</td>
<td align="center" style='display:none;'>
                          
             <label ><strong> Batch No </strong></label>
                       
</td>
   <td align="center" style='display:none;'>
                          
             <label ><strong> Expiry Date </strong></label>
                       
</td>


   <td align="center">
                          
             <label ><strong> Duration </strong></label>
                       
</td>
   
   <td align="center" >
                          
             <label ><strong> Actual  Qty </strong></label>
                       
</td>
   <td align="center">
                          
             <label ><strong>Bill Qty </strong></label>
                       
</td>
 
            </tr>
  
 <tr>
                <td align="center">

                    <asp:DropDownList ID="ddlMedicineGroup1" runat="server" AutoPostBack="True"   CssClass="group"   Width="150px" 
                        onselectedindexchanged="ddlMedicineGroup1_SelectedIndexChanged"   >
                    </asp:DropDownList>
</td> 

 <td align="center">                  
<asp:DropDownList ID="ddlSubGroup1" runat="server" AutoPostBack="True"   CssClass="subgroup"
         Width="150px" onselectedindexchanged="ddlSubGroup1_SelectedIndexChanged"></asp:DropDownList>           
</td>
                <td align="center">
                   
            <asp:DropDownList ID="ddlMedicine1"  CssClass="medicine"   Width="150px" runat="server" AutoPostBack="True" onselectedindexchanged="ddlMedicine1_SelectedIndexChanged" 
                   >
                    </asp:DropDownList>
                          
</td>

       <td align="center">                   
            <asp:DropDownList ID="ddlDose1" AutoPostBack="true"   Width="100px" 
                runat="server" onselectedindexchanged="ddlDose1_SelectedIndexChanged">
                    </asp:DropDownList>                          
</td>
   <td align="center" style='display:none;'>                   
            <asp:DropDownList ID="ddlBatchNo1"    Width="150px" runat="server" 
                AutoPostBack="True" onselectedindexchanged="ddlBatchNo1_SelectedIndexChanged">
                    </asp:DropDownList>                         
</td>
 <td align="center" style='display:none;'>                   
           <asp:TextBox ID="txtExpiryDate1"  CssClass="DatepickerReCall"  runat="server"></asp:TextBox>                          
</td>

 <td align="center">                   
             <asp:DropDownList ID="ddlDuration21" Width="90px" runat="server" onselectedindexchanged="ddlDuration21_SelectedIndexChanged" AutoPostBack="True">
                    </asp:DropDownList>                   
</td>

     
 <td align="center" >                   
           <asp:TextBox ID="txtActualQty21" runat="server"></asp:TextBox>                          
</td>
 <td align="center">                   
           <asp:TextBox ID="txtBillQty21"  CssClass="NumberInput" runat="server"></asp:TextBox>                          
</td>
 </tr>

 <tr>
                <td align="center">
                    <asp:DropDownList ID="ddlMedicineGroup2" runat="server" AutoPostBack="True"  CssClass="group" 
                        Width="150px" 
                        onselectedindexchanged="ddlMedicineGroup2_SelectedIndexChanged"   >
                    </asp:DropDownList>
           
           
            
</td> 

                <td align="center">
                  
                   
          <asp:DropDownList ID="ddlSubGroup2" runat="server" AutoPostBack="True"  CssClass="subgroup"
           Width="150px" onselectedindexchanged="ddlSubGroup2_SelectedIndexChanged" 
                      >
                    </asp:DropDownList>
           
</td>
                <td align="center">
                   
            <asp:DropDownList ID="ddlMedicine2"  runat="server" AutoPostBack="True"  CssClass="medicine"   Width="150px"   onselectedindexchanged="ddlMedicine2_SelectedIndexChanged" 
                       >
                    </asp:DropDownList>
                          
</td>

       <td align="center">
                   
         <asp:DropDownList ID="ddlDose2"   AutoPostBack="true" onselectedindexchanged="ddlDose2_SelectedIndexChanged"  Width="100px" runat="server">
                    </asp:DropDownList> 
                          
</td>
<td align="center" style='display:none;'>                   
            <asp:DropDownList ID="ddlBatchNo2"    Width="150px" runat="server" 
                AutoPostBack="True" onselectedindexchanged="ddlBatchNo2_SelectedIndexChanged">
                    </asp:DropDownList>                         
</td>
 <td align="center" style='display:none;'>                   
           <asp:TextBox ID="txtExpiryDate2"  CssClass="DatepickerReCall"  runat="server"></asp:TextBox>                          
</td>
 
 <td align="center">                   
            <asp:DropDownList ID="ddlDuration22" Width="90px" runat="server" OnSelectedIndexChanged="ddlDuration22_SelectedIndexChanged" AutoPostBack="True">
                    </asp:DropDownList>                        
</td>

     
 <td align="center" >                   
           <asp:TextBox ID="txtActualQty22" runat="server"></asp:TextBox>                          
</td>
 <td align="center">                   
           <asp:TextBox ID="txtBillQty22"  CssClass="NumberInput"  runat="server"></asp:TextBox>                          
</td>
 </tr>

 <tr>
                <td align="center">
                    <asp:DropDownList ID="ddlMedicineGroup3" runat="server"   Width="150px" CssClass="group" 
                        AutoPostBack="True" 
                        onselectedindexchanged="ddlMedicineGroup3_SelectedIndexChanged"    >
                    </asp:DropDownList>
           
           
            
</td> 

                <td align="center">
                  
                   
          <asp:DropDownList ID="ddlSubGroup3" runat="server" AutoPostBack="True"  CssClass="subgroup"
            Width="150px"    onselectedindexchanged="ddlSubGroup3_SelectedIndexChanged" >
                    </asp:DropDownList>
           
</td>
                <td align="center">
                   
            <asp:DropDownList ID="ddlMedicine3"  runat="server" AutoPostBack="True"    Width="150px"  CssClass="medicine" 
              onselectedindexchanged="ddlMedicine3_SelectedIndexChanged" >
                    </asp:DropDownList>
                          
</td>

       <td align="center">
                   
             <asp:DropDownList ID="ddlDose3"  AutoPostBack="true"  onselectedindexchanged="ddlDose3_SelectedIndexChanged"  Width="100px" runat="server">
                    </asp:DropDownList> 
                          
</td>
<td align="center" style='display:none;'>                   
            <asp:DropDownList ID="ddlBatchNo3"    Width="150px" runat="server" 
                AutoPostBack="True" onselectedindexchanged="ddlBatchNo3_SelectedIndexChanged" >
                    </asp:DropDownList>                         
</td>
 <td align="center" style='display:none;'>                   
           <asp:TextBox ID="txtExpiryDate3"  CssClass="DatepickerReCall"  runat="server"></asp:TextBox>                          
</td>
 
 <td align="center">                   
             <asp:DropDownList ID="ddlDuration23" Width="90px" runat="server" OnSelectedIndexChanged="ddlDuration23_SelectedIndexChanged" AutoPostBack="True">
                    </asp:DropDownList>                         
</td>

     
 <td align="center" >                   
           <asp:TextBox ID="txtActualQty23" runat="server"></asp:TextBox>                          
</td>
 <td align="center">                   
           <asp:TextBox ID="txtBillQty23"  CssClass="NumberInput"  runat="server"></asp:TextBox>                          
</td>
 </tr>

 <tr>
                <td align="center">
                    <asp:DropDownList ID="ddlMedicineGroup4" runat="server" AutoPostBack="True"  CssClass="group"    Width="150px" 
                           onselectedindexchanged="ddlMedicineGroup4_SelectedIndexChanged" >
                    </asp:DropDownList>
           
           
            
</td> 

                <td align="center">
                  
                   
          <asp:DropDownList ID="ddlSubGroup4" runat="server" AutoPostBack="True"  CssClass="subgroup"   Width="150px"
                        onselectedindexchanged="ddlSubGroup4_SelectedIndexChanged" >
                    </asp:DropDownList>
           
</td>
                <td align="center">
                   
            <asp:DropDownList ID="ddlMedicine4"  runat="server" AutoPostBack="True"    Width="150px"  CssClass="medicine" 
                        onselectedindexchanged="ddlMedicine4_SelectedIndexChanged" >
                    </asp:DropDownList>
                          
</td>

       <td align="center">
                   
            <asp:DropDownList ID="ddlDose4"   AutoPostBack="true"   Width="100px" 
                runat="server" onselectedindexchanged="ddlDose4_SelectedIndexChanged">
                    </asp:DropDownList> 
                          
</td>
<td align="center" style='display:none;'>                   
            <asp:DropDownList ID="ddlBatchNo4"    Width="150px" runat="server" 
                AutoPostBack="True" onselectedindexchanged="ddlBatchNo4_SelectedIndexChanged" >
                    </asp:DropDownList>                         
</td>
 <td align="center" style='display:none;'>                   
           <asp:TextBox ID="txtExpiryDate4"  CssClass="DatepickerReCall"  runat="server"></asp:TextBox>                          
</td>
 
 <td align="center">                   
             <asp:DropDownList ID="ddlDuration24"    Width="90px" runat="server" OnSelectedIndexChanged="ddlDuration24_SelectedIndexChanged" AutoPostBack="True">
                    </asp:DropDownList>                        
</td>

     
 <td align="center" >                   
           <asp:TextBox ID="txtActualQty24" runat="server"></asp:TextBox>                          
</td>
 <td align="center">                   
           <asp:TextBox ID="txtBillQty24"  CssClass="NumberInput"  runat="server"></asp:TextBox>                          
</td>
 </tr>

 <tr>
                <td align="center">
                    <asp:DropDownList ID="ddlMedicineGroup5" runat="server" AutoPostBack="True"  CssClass="group"   Width="150px"
                           onselectedindexchanged="ddlMedicineGroup5_SelectedIndexChanged" >
                    </asp:DropDownList>
           
           
            
</td> 

                <td align="center">
                  
                   
          <asp:DropDownList ID="ddlSubGroup5" runat="server" AutoPostBack="True"   Width="150px" CssClass="subgroup"
                        onselectedindexchanged="ddlSubGroup5_SelectedIndexChanged"  >
                    </asp:DropDownList>
           
</td>
                <td align="center">
                   
            <asp:DropDownList ID="ddlMedicine5"  runat="server" AutoPostBack="True"   Width="150px"  CssClass="medicine" 
                        onselectedindexchanged="ddlMedicine5_SelectedIndexChanged" >
                    </asp:DropDownList>
                          
</td>

       <td align="center">
                   
       <asp:DropDownList ID="ddlDose5"   AutoPostBack="true"   Width="100px" 
               runat="server" onselectedindexchanged="ddlDose5_SelectedIndexChanged">
                    </asp:DropDownList> 
                          
</td>
<td align="center" style='display:none;'>                   
            <asp:DropDownList ID="ddlBatchNo5"    Width="150px" runat="server" 
                AutoPostBack="True" onselectedindexchanged="ddlBatchNo5_SelectedIndexChanged" >
                    </asp:DropDownList>                         
</td>
 <td align="center" style='display:none;'>                   
           <asp:TextBox ID="txtExpiryDate5"  CssClass="DatepickerReCall"  runat="server"></asp:TextBox>                          
</td>
 
 <td align="center">                   
           <asp:DropDownList ID="ddlDuration25"    Width="90px" runat="server" OnSelectedIndexChanged="ddlDuration25_SelectedIndexChanged" AutoPostBack="True">
                    </asp:DropDownList>                          
</td>

     
 <td align="center" >                   
           <asp:TextBox ID="txtActualQty25" runat="server"></asp:TextBox>                          
</td>
 <td align="center">                   
           <asp:TextBox ID="txtBillQty25"  CssClass="NumberInput"  runat="server"></asp:TextBox>                          
</td>
 </tr>

 <tr>
                <td align="center">
                    <asp:DropDownList ID="ddlMedicineGroup6" runat="server" AutoPostBack="True"  CssClass="group"   Width="150px"
                           onselectedindexchanged="ddlMedicineGroup6_SelectedIndexChanged"  >
                    </asp:DropDownList>
           
           
            
</td> 

                <td align="center">
                  
                   
          <asp:DropDownList ID="ddlSubGroup6" runat="server" AutoPostBack="True"   Width="150px" CssClass="subgroup"
                        onselectedindexchanged="ddlSubGroup6_SelectedIndexChanged" >
                    </asp:DropDownList>
           
</td>
                <td align="center">
                   
            <asp:DropDownList ID="ddlMedicine6"  runat="server" AutoPostBack="True"   Width="150px"  CssClass="medicine" 
                        onselectedindexchanged="ddlMedicine6_SelectedIndexChanged" >
                    </asp:DropDownList>
                          
</td>

       <td align="center">
                   
           <asp:DropDownList ID="ddlDose6"    AutoPostBack="true"   Width="100px" 
               runat="server" onselectedindexchanged="ddlDose6_SelectedIndexChanged">
                    </asp:DropDownList> 
                          
</td>
<td align="center" style='display:none;'>                   
            <asp:DropDownList ID="ddlBatchNo6"    Width="150px" runat="server" 
                AutoPostBack="True" onselectedindexchanged="ddlBatchNo6_SelectedIndexChanged">
                    </asp:DropDownList>                         
</td>
 <td align="center" style='display:none;'>                   
           <asp:TextBox ID="txtExpiryDate6"  CssClass="DatepickerReCall"  runat="server"></asp:TextBox>                          
</td>
 
 <td align="center">                   
              <asp:DropDownList ID="ddlDuration26"    Width="90px" runat="server" OnSelectedIndexChanged="ddlDuration26_SelectedIndexChanged" AutoPostBack="True">
                    </asp:DropDownList>                          
</td>

     
 <td align="center" >                   
           <asp:TextBox ID="txtActualQty26"  CssClass="NumberInput"  runat="server"></asp:TextBox>                          
</td>
 <td align="center">                   
           <asp:TextBox ID="txtBillQty26"  CssClass="NumberInput" runat="server"></asp:TextBox>                          
</td>
 </tr>

 <tr>
                <td align="center">
                    <asp:DropDownList ID="ddlMedicineGroup7" runat="server" AutoPostBack="True" CssClass="group"   Width="150px" 
                           onselectedindexchanged="ddlMedicineGroup7_SelectedIndexChanged" >
                    </asp:DropDownList>
           
           
            
</td> 

                <td align="center">
                  
                   
          <asp:DropDownList ID="ddlSubGroup7" runat="server" AutoPostBack="True"   Width="150px" CssClass="subgroup"
                        onselectedindexchanged="ddlSubGroup7_SelectedIndexChanged" >
                    </asp:DropDownList>
           
</td>
                <td align="center">
                   
            <asp:DropDownList ID="ddlMedicine7"  runat="server" AutoPostBack="True"   Width="150px"  CssClass="medicine" 
                        onselectedindexchanged="ddlMedicine7_SelectedIndexChanged" >
                    </asp:DropDownList>
                          
</td>

       <td align="center">
                   
       <asp:DropDownList ID="ddlDose7"   AutoPostBack="true"   Width="100px" 
               runat="server" onselectedindexchanged="ddlDose7_SelectedIndexChanged">
                    </asp:DropDownList> 
                          
</td>
<td align="center" style='display:none;'>                   
            <asp:DropDownList ID="ddlBatchNo7"    Width="150px" runat="server" AutoPostBack="True" onselectedindexchanged="ddlBatchNo7_SelectedIndexChanged">
                    </asp:DropDownList>                         
</td>
 <td align="center" style='display:none;'>                   
           <asp:TextBox ID="txtExpiryDate7"  CssClass="DatepickerReCall"   runat="server"></asp:TextBox>                          
</td>
 
 <td align="center">                   
             <asp:DropDownList ID="ddlDuration27"    Width="90px" runat="server" OnSelectedIndexChanged="ddlDuration27_SelectedIndexChanged" AutoPostBack="True">
                    </asp:DropDownList>                          
</td>

     
 <td align="center" '>                   
           <asp:TextBox ID="txtActualQty27"   CssClass="NumberInput" runat="server"></asp:TextBox>                          
</td>
 <td align="center">                   
           <asp:TextBox ID="txtBillQty27"  CssClass="NumberInput"  runat="server"></asp:TextBox>                          
</td>
 </tr>

 <tr>
                <td align="center">
                    <asp:DropDownList ID="ddlMedicineGroup8" runat="server" AutoPostBack="True"  CssClass="group"  Width="150px" 
                           onselectedindexchanged="ddlMedicineGroup8_SelectedIndexChanged" >
                    </asp:DropDownList>
           
           
            
</td> 

                <td align="center">
                  
                   
          <asp:DropDownList ID="ddlSubGroup8" runat="server" AutoPostBack="True"   Width="150px" CssClass="subgroup"
                        onselectedindexchanged="ddlSubGroup8_SelectedIndexChanged" >
                    </asp:DropDownList>
           
</td>
                <td align="center">
                   
            <asp:DropDownList ID="ddlMedicine8"  runat="server" AutoPostBack="True"   Width="150px"  CssClass="medicine" 
                        onselectedindexchanged="ddlMedicine8_SelectedIndexChanged" >
                    </asp:DropDownList>
                          
</td>

       <td align="center">
             <asp:DropDownList ID="ddlDose8"    AutoPostBack="true"   Width="100px" 
                 runat="server" onselectedindexchanged="ddlDose8_SelectedIndexChanged">
                    </asp:DropDownList> 
                          
</td>
<td align="center" style='display:none;'>                   
            <asp:DropDownList ID="ddlBatchNo8"    Width="150px" runat="server" AutoPostBack="True" onselectedindexchanged="ddlBatchNo8_SelectedIndexChanged">
                    </asp:DropDownList>                         
</td>
 <td align="center" style='display:none;'>                   
           <asp:TextBox ID="txtExpiryDate8"   CssClass="DatepickerReCall"  runat="server"></asp:TextBox>                          
</td>
 
 <td align="center">                   
             <asp:DropDownList ID="ddlDuration28"    Width="90px" runat="server" OnSelectedIndexChanged="ddlDuration28_SelectedIndexChanged" AutoPostBack="True">
                    </asp:DropDownList>                            
</td>

     
 <td align="center" >                   
           <asp:TextBox ID="txtActualQty28"  CssClass="NumberInput"  runat="server"></asp:TextBox>                          
</td>
 <td align="center">                   
           <asp:TextBox ID="txtBillQty28"  CssClass="NumberInput"  runat="server"></asp:TextBox>                          
</td>
 </tr>

 <tr>
                <td align="center">
                    <asp:DropDownList ID="ddlMedicineGroup9" runat="server" AutoPostBack="True"  CssClass="group"   Width="150px" 
                           onselectedindexchanged="ddlMedicineGroup9_SelectedIndexChanged" >
                    </asp:DropDownList>
           
           
            
</td> 

                <td align="center">
                  
                   
          <asp:DropDownList ID="ddlSubGroup9" runat="server" AutoPostBack="True"   Width="150px" CssClass="subgroup"
                        onselectedindexchanged="ddlSubGroup9_SelectedIndexChanged" >
                    </asp:DropDownList>
           
</td>
                <td align="center">
                   
            <asp:DropDownList ID="ddlMedicine9"  runat="server" AutoPostBack="True"   Width="150px"  CssClass="medicine" 
                        onselectedindexchanged="ddlMedicine9_SelectedIndexChanged" >
                    </asp:DropDownList>
                          
</td>

       <td align="center">
                   
    <asp:DropDownList ID="ddlDose9"    AutoPostBack="true"   Width="100px" runat="server" 
               onselectedindexchanged="ddlDose9_SelectedIndexChanged">
                    </asp:DropDownList> 
                          
</td>
<td align="center" style='display:none;'>                   
            <asp:DropDownList ID="ddlBatchNo9"    Width="150px" runat="server" AutoPostBack="True" onselectedindexchanged="ddlBatchNo6_SelectedIndexChanged">
                    </asp:DropDownList>                         
</td>
 <td align="center" style='display:none;'>                   
           <asp:TextBox ID="txtExpiryDate9"  CssClass="DatepickerReCall"  runat="server"></asp:TextBox>                          
</td>
 
 <td align="center">                   
             <asp:DropDownList ID="ddlDuration29"    Width="90px" runat="server" OnSelectedIndexChanged="ddlDuration29_SelectedIndexChanged" AutoPostBack="True">
                    </asp:DropDownList>                        
</td>

     
 <td align="center" >                   
           <asp:TextBox ID="txtActualQty29"  CssClass="NumberInput"  runat="server"></asp:TextBox>                          
</td>
 <td align="center">                   
           <asp:TextBox ID="txtBillQty29"  CssClass="NumberInput"  runat="server"></asp:TextBox>                          
</td>
 </tr>

 <tr>
                <td align="center">
                    <asp:DropDownList ID="ddlMedicineGroup10" runat="server" AutoPostBack="True"  CssClass="group"   Width="150px" 
                           onselectedindexchanged="ddlMedicineGroup10_SelectedIndexChanged" >
                    </asp:DropDownList>
           
           
            
</td> 

                <td align="center">
                  
                   
          <asp:DropDownList ID="ddlSubGroup10" runat="server" AutoPostBack="True"   Width="150px" CssClass="subgroup"
                        onselectedindexchanged="ddlSubGroup10_SelectedIndexChanged" >
                    </asp:DropDownList>
           
</td>
                <td align="center">
                   
            <asp:DropDownList ID="ddlMedicine10"  runat="server" AutoPostBack="True"   Width="150px"  CssClass="medicine" 
                        onselectedindexchanged="ddlMedicine10_SelectedIndexChanged" >
                    </asp:DropDownList>
                          
</td>

       <td align="center">
                   
        <asp:DropDownList ID="ddlDose10"    AutoPostBack="true"   Width="100px" 
               runat="server" onselectedindexchanged="ddlDose10_SelectedIndexChanged">
                    </asp:DropDownList> 
                          
</td>
<td align="center" style='display:none;'>                   
            <asp:DropDownList ID="ddlBatchNo10"    Width="150px" runat="server" AutoPostBack="True" onselectedindexchanged="ddlBatchNo10_SelectedIndexChanged">
                    </asp:DropDownList>                         
</td>
 <td align="center" style='display:none;'>                   
           <asp:TextBox ID="txtExpiryDate10"  CssClass="DatepickerReCall"   runat="server"></asp:TextBox>                          
</td>
 
 <td align="center">                   
          <asp:DropDownList ID="ddlDuration30"    Width="90px" runat="server" OnSelectedIndexChanged="ddlDuration30_SelectedIndexChanged" AutoPostBack="True">
                    </asp:DropDownList>                           
</td>

     
 <td align="center" >                   
           <asp:TextBox ID="txtActualQty30"  CssClass="NumberInput"  runat="server"></asp:TextBox>                          
</td>
 <td align="center">                   
           <asp:TextBox ID="txtBillQty30"  CssClass="NumberInput"  runat="server"></asp:TextBox>                          
</td>
 </tr>
 <tr>
 <td align="center"><strong>Comment :</strong></td>
 <td colspan="10">  <asp:TextBox ID="TextBox204" TextMode="MultiLine" Width="298px"  Height="45px" runat="server"></asp:TextBox></td>
 </tr>
    <tr>
   <td colspan="4">
   <div class="form-sec-row">
                <label><strong>&nbsp;</strong></label>
                <asp:Button ID="Button15" runat="server"  Text="Submit" CssClass="submit-button" 
                    Height="28px" onclick="Button15_Click"  />
                <asp:Button ID="Button16" runat="server"  Text="Cancel" 
                    CssClass="submit-button"  Height="28px" onclick="Button16_Click" 
                      />
                        
                <div class="clear"></div>
            </div>
   </td>
   </tr>
</table>

 </div>
                        </asp:Panel>

                           <td>
           <div class="pageheader">
         <asp:Label ID="Label16" runat="server"> Running Prescription </asp:Label>
     </div>
      <div  style=' height:150px; overflow:auto;'>
        <asp:GridView ID="GridView9" DataKeyNames ="RowId" 
               Font-Size="Small"        runat="server" AutoGenerateColumns="False"
          AllowPaging="True" PageSize ="50"  Width="100%" 
              SelectedRowStyle-BackColor="GreenYellow" 
              onrowdatabound="GridView9_RowDataBound" 
              onrowcommand="GridView9_RowCommand">  
               <RowStyle HorizontalAlign="Center" />
                <RowStyle BackColor="Gainsboro" />
            <AlternatingRowStyle BackColor="White" />
            <HeaderStyle BackColor="#0083C1" ForeColor="White"/>
            <Columns>
                <asp:TemplateField><ItemTemplate><a href="javascript:expandcollapse('div<%# Eval("PrescriptionID") %>', 'one');"><img id="imgdiv<%# Eval("PrescriptionID") %>" alt="Click to show/hide Orders for Customer <%# Eval("PrescriptionID") %>"  width="9px" border="0" src="../Images/plus.gif"/></a></ItemTemplate></asp:TemplateField>
                <asp:TemplateField HeaderText="Row Id"  Visible="false"><ItemTemplate><asp:Label ID="lblRowId" Text='<%# Eval("RowId") %>' runat="server"></asp:Label></ItemTemplate></asp:TemplateField>
                <asp:TemplateField HeaderText="Prescription Id" SortExpression="Prescription Id"><ItemTemplate><asp:Label ID="lblPrescriptionID" Text='<%# Eval("PrescriptionID") %>' runat="server"></asp:Label></ItemTemplate></asp:TemplateField>

                <asp:TemplateField HeaderText="Date" SortExpression="Date"><ItemTemplate><%# Eval("Date1") %></ItemTemplate></asp:TemplateField>
                <asp:TemplateField HeaderText="Doctor" SortExpression="Doctor"><ItemTemplate><%# Eval("doc_name")%></ItemTemplate></asp:TemplateField> 
			  
                <asp:TemplateField HeaderText="Stop" SortExpression="Medicine" ><ItemTemplate><asp:LinkButton ID="linkStopPrescription" CommandArgument='<%# Eval("PrescriptionID") %>'  CommandName="StopPrescription" OnClientClick="return ConfirmationMessagePres();" runat="server">Stop</asp:LinkButton></ItemTemplate></asp:TemplateField>

                  <asp:TemplateField><ItemTemplate><tr><td colspan="100%"><div id="div<%# Eval("PrescriptionID") %>" style="display:none;" >
                  <asp:GridView ID="GridView10"  Font-Size="X-Small"  DataKeyNames ="PrescriptionId" runat="server" AutoGenerateColumns="False"
                     onrowcommand="GridView10_RowCommand" AllowPaging="True" PageSize ="50"  Width="100%" 
                      SelectedRowStyle-BackColor="GreenYellow"><RowStyle HorizontalAlign="Center" />
                      <RowStyle BackColor="Gainsboro" /><AlternatingRowStyle BackColor="White" />
                      <HeaderStyle BackColor="#0083C1" ForeColor="White"/>
                      <Columns><asp:TemplateField HeaderText="Prescription Id" SortExpression="ID" Visible="false">
                      <ItemTemplate><asp:Label ID="lblPrescriptionId" Text='<%# Eval("PrescriptionId") %>' runat="server"></asp:Label>
                      </ItemTemplate></asp:TemplateField><asp:TemplateField HeaderText="Group" SortExpression="Group">
                      <ItemTemplate><%# Eval("MedicineGroupName")%></ItemTemplate></asp:TemplateField>
                      <asp:TemplateField HeaderText="Sub Group" SortExpression="Sub Group"><ItemTemplate><%# Eval("SubGrName")%>
                      </ItemTemplate></asp:TemplateField><asp:TemplateField HeaderText="MedicineId" SortExpression="MedicineId" Visible="false">
                      <ItemTemplate><asp:Label ID="lblMedicineId" Text='<%# Eval("MedicineID") %>' runat="server"></asp:Label></ItemTemplate></asp:TemplateField>
                      <asp:TemplateField HeaderText="Medicine" SortExpression="Medicine" ><ItemTemplate><%# Eval("MedicineName")%></ItemTemplate></asp:TemplateField>
                      <asp:TemplateField HeaderText="Stop" SortExpression="Medicine" >
                      <ItemTemplate>
  <asp:LinkButton ID="linkStopMedicine" CommandArgument='<%# Eval("MedicineID") %>'  CommandName="StopMedicine" OnClientClick="return ConfirmationMessageMed();" runat="server">Stop</asp:LinkButton>
                      </ItemTemplate></asp:TemplateField></Columns></asp:GridView>
                      </div>
                      </td>
                      </tr>
                      </ItemTemplate>
                      </asp:TemplateField>			    
			</Columns> 
        </asp:GridView>
         
    </div>
          </td>
   </asp:View>

   <asp:View ID="View3" runat="server">
                     
                          <table border="1" cellpadding="0" cellspacing="0" width="100%">
    <tr>
    <td>
    
                  <div class="pageheader">
         <asp:Label ID="Label5" runat="server"> Patient Previous Service Details Report </asp:Label>
     </div>


                                    <div class="listing" style='width:100%; height:150px; overflow:auto;'>
         <asp:GridView id="GridView2"  CssClass="grid" PagerStyle-CssClass="pgr" DataKeyNames ="RowId" 
                                            runat="server" AutoGenerateColumns="False"
          AllowPaging="True" PageSize ="50"  Width="100%"
                 
              SelectedRowStyle-BackColor="GreenYellow" onpageindexchanging="GridView2_PageIndexChanging" 
                                            onrowcancelingedit="GridView2_RowCancelingEdit" 
                                            onrowdatabound="GridView2_RowDataBound" onrowdeleting="GridView2_RowDeleting" 
                                            onrowediting="GridView2_RowEditing" onrowupdating="GridView2_RowUpdating" >
                <RowStyle HorizontalAlign="Center" />
                <Columns>
                 

                  <asp:TemplateField HeaderText="Id" Visible ="false"><ItemTemplate><asp:Label ID="lblid" runat="server" Text='<%# Bind("RowID") %>'></asp:Label></ItemTemplate></asp:TemplateField>

                       <asp:TemplateField HeaderText="Prescription Id"><ItemTemplate><asp:Label ID="lblpid" runat="server" Text='<%# Bind("PrescriptionId") %>'></asp:Label></ItemTemplate></asp:TemplateField>


                     <asp:TemplateField HeaderText="Date"><ItemTemplate><asp:Label ID="lbldate" runat="server" Text='<%# Bind("Date1") %>'></asp:Label></ItemTemplate><EditItemTemplate><asp:TextBox ID="txtdate"  Width="90px"  runat="server" Text='<%# Bind("Date1") %>'></asp:TextBox></EditItemTemplate></asp:TemplateField>
                  

                      <asp:TemplateField HeaderText="Doctor Id" Visible="false"><ItemTemplate><asp:Label ID="lbldoc_id" runat="server" Text='<%# Bind("DoctorId") %>'></asp:Label></ItemTemplate></asp:TemplateField>

<%--
                    <asp:TemplateField HeaderText="Doctor"><ItemTemplate>
                    <asp:Label ID="lbldoc_name" runat="server" Text='<%# Bind("doc_name") %>'></asp:Label></ItemTemplate>
                    <EditItemTemplate><asp:DropDownList ID="ddlDoctor"  Width="110px"   runat="server"></asp:DropDownList></EditItemTemplate></asp:TemplateField>--%>

                
                  <asp:TemplateField HeaderText="Service Id" Visible="false"><ItemTemplate><asp:Label ID="lblServiceID" runat="server" Text='<%# Bind("ServiceId") %>'></asp:Label></ItemTemplate></asp:TemplateField> 


                    
                      <asp:TemplateField HeaderText="Service"><ItemTemplate><asp:Label ID="lblgroup" runat="server" Text='<%# Bind("ServiceTemplateName") %>'></asp:Label></ItemTemplate><EditItemTemplate><asp:DropDownList ID="ddlService"   runat="server"></asp:DropDownList></EditItemTemplate></asp:TemplateField> 

                      <asp:TemplateField HeaderText="Quantity"><ItemTemplate><asp:Label ID="lblQuantity" runat="server" Text='<%# Bind("Quantity") %>'></asp:Label></ItemTemplate><EditItemTemplate><asp:TextBox ID="txtQuantity"  Width="30px"  runat="server" Text='<%# Bind("Quantity") %>'></asp:TextBox></EditItemTemplate></asp:TemplateField> 

                    <asp:TemplateField HeaderText="Price"><ItemTemplate><asp:Label ID="lblPrice" runat="server" Text='<%# Bind("Price") %>'></asp:Label></ItemTemplate><EditItemTemplate><asp:TextBox ID="txtPrice"  Width="50px"  runat="server" Text='<%# Bind("Price") %>'></asp:TextBox></EditItemTemplate></asp:TemplateField>   
                    
                    <asp:TemplateField HeaderText="Total Price"><ItemTemplate><asp:Label ID="lblTotalPrice" runat="server" Text='<%# Bind("TotalPrice") %>'></asp:Label></ItemTemplate><EditItemTemplate><asp:TextBox ID="txtTotalPrice"  Width="40px"  runat="server" Text='<%# Bind("TotalPrice") %>'>
                    </asp:TextBox></EditItemTemplate></asp:TemplateField> 

                    <asp:TemplateField HeaderText="Continue" Visible="false">
                    <ItemTemplate>
                        <asp:Label ID="lblcont" Width="50px" runat="server" Text='<%# Bind("ServCont") %>' Visible="false"></asp:Label>
                    </ItemTemplate>
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="Continue">
                    <ItemTemplate>
                        
                        <asp:CheckBox ID="chkcont" Width="50px" runat="server" Enabled="false"/>
                    </ItemTemplate>
                    
                    <EditItemTemplate>
                    <asp:CheckBox ID="cont" Width="50px" runat="server" />
                    </EditItemTemplate>
                    </asp:TemplateField>
                          
                    <asp:CommandField HeaderText="Edit" ShowEditButton="True" />
                    <asp:CommandField HeaderText="Delete" ShowDeleteButton="True" />
                </Columns>
                <PagerStyle CssClass="pgr" />
                <EditRowStyle BackColor="#CCFF33" />
                <AlternatingRowStyle BackColor="#FFDB91" />
                <SelectedRowStyle BackColor="GreenYellow" />
        </asp:GridView> 
        </div>

          </td>
    </tr>
    </table>
         
   <table border="1" cellpadding="0" style='background-color:#C5B9B9;' cellspacing="0" width="100%">
                        <tr>
                        <td colspan="2" align="center">
                            <div class="pageheader">
         <asp:Label ID="Label4" runat="server"> Patient Service </asp:Label>
     </div>
                        </td>
                        </tr>
          <tr>

           <td><strong>Template Group:</strong> 
                  <asp:DropDownList ID="DropDownList36" runat="server" AutoPostBack="True" 
                   Width="180px" 
                   onselectedindexchanged="DropDownList36_SelectedIndexChanged"  >
                  </asp:DropDownList> </td>
              <td><strong>Template :</strong> 
                  <asp:DropDownList ID="DropDownList37" runat="server" AutoPostBack="True" 
                      Width="180px" onselectedindexchanged="DropDownList37_SelectedIndexChanged" 
                     >
                  </asp:DropDownList> </td>
</tr>
</table>


           
             <table border="1" cellpadding="0" style='background-color:#C5B9B9;' cellspacing="0" width="100%">
          <tr>
          <td><strong>Prescription No :</strong> 
              <asp:TextBox ID="TextBox6" runat="server" Enabled="False"></asp:TextBox> </td>
              <td><strong>Date :</strong> <asp:TextBox ID="TextBox7" CssClass="DatepickerReCall"  Width="125px"  runat="server"></asp:TextBox> </td>
            
          </tr>
          </table>
  
    <table border="1" cellpadding="0" cellspacing="0" width="100%">
    <tr>
    <td>
    
                  <div class="pageheader">
         <asp:Label ID="Label14" runat="server"> Preview </asp:Label>
     </div>


                                    <div class="listing" style='width:100%; height:150px; overflow:auto;'>
         <asp:GridView id="GridView7"  CssClass="grid" PagerStyle-CssClass="pgr" DataKeyNames ="RowId" 
                                            runat="server" AutoGenerateColumns="False"
          AllowPaging="True" PageSize ="50"  Width="100%"
                 
              SelectedRowStyle-BackColor="GreenYellow" onpageindexchanging="GridView7_PageIndexChanging" 
                                            onrowcancelingedit="GridView7_RowCancelingEdit" 
                                            onrowdatabound="GridView7_RowDataBound" onrowdeleting="GridView7_RowDeleting" 
                                            onrowediting="GridView7_RowEditing" onrowupdating="GridView7_RowUpdating"  >
                <RowStyle HorizontalAlign="Center" />
                <Columns>
                 

                  <asp:TemplateField HeaderText="Sl.No." ><ItemTemplate><asp:Label ID="lblid" runat="server" Text='<%# Bind("RowID") %>'></asp:Label></ItemTemplate></asp:TemplateField>

                    <asp:TemplateField HeaderText="Service Id"   Visible="false"><ItemTemplate><asp:Label ID="lblServiceId" runat="server" Text='<%# Bind("ServiceId") %>'></asp:Label></ItemTemplate></asp:TemplateField>

                    <asp:TemplateField HeaderText="Service"><ItemTemplate><asp:Label ID="lblServiceName" runat="server" Text='<%# Bind("ServiceCategoryName") %>'>
                    </asp:Label></ItemTemplate></asp:TemplateField>
        
          <%--
                                   <asp:TemplateField HeaderText="Service Id"  Visible="false">
                  <ItemTemplate><asp:Label ID="lblServiceId" runat="server" Text='<%# Bind("ServiceId") %>'></asp:Label></ItemTemplate></asp:TemplateField>

                      <asp:TemplateField HeaderText="Service"><ItemTemplate><asp:Label ID="lblServiceName" runat="server" Text='<%# Bind("ServiceName") %>'></asp:Label>
                      </ItemTemplate><EditItemTemplate><asp:DropDownList ID="ddlService"   runat="server"></asp:DropDownList></EditItemTemplate></asp:TemplateField> --%>

                      <asp:TemplateField HeaderText="Quantity"><ItemTemplate><asp:Label ID="lblQuantity" runat="server" Text='<%# Bind("Quantity") %>'></asp:Label></ItemTemplate><EditItemTemplate><asp:TextBox ID="txtQuantity"  Width="30px"  CssClass="NumberInput"  runat="server" Text='<%# Bind("Quantity") %>'></asp:TextBox></EditItemTemplate></asp:TemplateField> 

                    <asp:TemplateField HeaderText="Unit Price"><ItemTemplate><asp:Label ID="lblPrice" runat="server" Text='<%# Bind("Price") %>'></asp:Label></ItemTemplate><EditItemTemplate><asp:TextBox ID="txtPrice"  Width="50px"   CssClass="NumberInput"  runat="server" Text='<%# Bind("Price") %>'></asp:TextBox></EditItemTemplate></asp:TemplateField>   
                    
               <%--     <asp:TemplateField HeaderText="Total Price"><ItemTemplate><asp:Label ID="lblTotalQty" runat="server" Text='<%# Bind("TotalPrice") %>'></asp:Label>
                    </ItemTemplate><EditItemTemplate><asp:TextBox ID="txtTotalQuantity"  Width="40px"  CssClass="NumberInput"  runat="server" Text='<%# Bind("TotalPrice") %>'>
                    </asp:TextBox></EditItemTemplate></asp:TemplateField> --%>
                
                    <asp:TemplateField HeaderText="Continue">
                    <ItemTemplate><%--<asp:Label ID="lblcontinue" runat="server" Text='<%# Bind("ServCont") %>'></asp:Label>--%>
                        <asp:CheckBox ID="servcontinue" Width="50px" runat="server" />
                    </ItemTemplate>
                    <EditItemTemplate>
                    <%--<asp:CheckBox ID="servcontinueedit" Width="50px" runat="server" />--%>
                    </EditItemTemplate>
                    </asp:TemplateField> 

                    <asp:TemplateField HeaderText="WithConsumables">
                    <ItemTemplate><%--<asp:Label ID="lblcontinue" runat="server" Text='<%# Bind("ServCont") %>'></asp:Label>--%>
                        <asp:CheckBox ID="chkCons" Width="50px" runat="server" AutoPostBack="true" OnCheckedChanged="add_remove_cons"/>
                    </ItemTemplate>
                    <EditItemTemplate>
                    <%--<asp:CheckBox ID="servcontinueedit" Width="50px" runat="server" />--%>
                    </EditItemTemplate>
                    </asp:TemplateField> 
                    <asp:CommandField HeaderText="Edit" ShowEditButton="True" />
                    <asp:CommandField HeaderText="Delete" ShowDeleteButton="True" />
                </Columns>
                <PagerStyle CssClass="pgr" />
                <EditRowStyle BackColor="#CCFF33" />
                <AlternatingRowStyle BackColor="#FFDB91" />
                <SelectedRowStyle BackColor="GreenYellow" />
        </asp:GridView> 
        </div>

          </td>

    </tr>
    </table>
    <table border="1" cellpadding="0" cellspacing="0" width="100%">
        <tr>
            <td>
                    <div class="pageheader">
                        <asp:Label ID="Label17" runat="server"> Consumables Preview </asp:Label>
                    </div>

                    
                    <div class="listing" style='width:100%; height:150px; overflow:auto;'>
         <asp:GridView id="GridView11"  CssClass="grid" PagerStyle-CssClass="pgr" DataKeyNames ="RowID" 
                                            runat="server" AutoGenerateColumns="False"
          AllowPaging="True" PageSize ="50"  Width="100%"   SelectedRowStyle-BackColor="GreenYellow" 
                                            onpageindexchanging="GridView11_PageIndexChanging" 
                                            onrowcancelingedit="GridView11_RowCancelingEdit" 
                                            onrowdatabound="GridView11_RowDataBound" onrowdeleting="GridView11_RowDeleting" 
                                            onrowediting="GridView11_RowEditing" onrowupdating="GridView11_RowUpdating">
                <RowStyle HorizontalAlign="Center" />
                <Columns>
                 

                  <asp:TemplateField HeaderText="Sl.No.">
                  
                  <ItemTemplate><asp:Label ID="lblid" runat="server" Text='<%# Bind("RowID") %>'>
                  </asp:Label>
                  </ItemTemplate>
                  </asp:TemplateField>

                     <asp:TemplateField HeaderText="Consumable Group Id" Visible ="false">
                     
                     <ItemTemplate><asp:Label ID="lblConGrId" runat="server" Text='<%# Bind("ConsumableGrId") %>'></asp:Label>
                     </ItemTemplate>
                     </asp:TemplateField>

                    <asp:TemplateField HeaderText="Consumable Group">
                    <ItemTemplate><asp:Label ID="lblConGroupName" runat="server" Text='<%# Bind("ConGroupName") %>'>
                    </asp:Label>
                    </ItemTemplate><EditItemTemplate>
                    <asp:DropDownList ID="ddlConGroupName"   runat="server"  Width="110px"  AutoPostBack="true"   onselectedindexchanged="ddlConGroupName_SelectedIndexChanged1" >
                    </asp:DropDownList>
                    </EditItemTemplate>
                    </asp:TemplateField>
                  
                  
                 
                   <asp:TemplateField HeaderText="Consumable Item Id" Visible ="false">
                   <ItemTemplate><asp:Label ID="lblConItemID" runat="server" Text='<%# Bind("ConsumableItemId") %>'></asp:Label></ItemTemplate></asp:TemplateField>

                    
                      <asp:TemplateField HeaderText="Consumable Item"><ItemTemplate><asp:Label ID="lblConItemName" runat="server" Text='<%# Bind("ConItemName") %>'>
                      </asp:Label></ItemTemplate>
                      <EditItemTemplate><asp:DropDownList ID="ddlConItemName"   Width="110px"  runat="server"></asp:DropDownList>
                      </EditItemTemplate>
                      </asp:TemplateField> 

                      <asp:TemplateField HeaderText="Actual Qty"><ItemTemplate><asp:Label ID="lblActualQty" runat="server" Text='<%# Bind("ActualQty") %>'>
                      </asp:Label></ItemTemplate><EditItemTemplate><asp:TextBox ID="txtActualQty" CssClass="nonumber"  Width="30px"  runat="server" Text='<%# Bind("ActualQty") %>'>
                      </asp:TextBox>
                      </EditItemTemplate>
                      </asp:TemplateField> 
                        <asp:TemplateField HeaderText="Bill Qty"><ItemTemplate><asp:Label ID="lblBillQty" runat="server" Text='<%# Bind("BillQty") %>'>
                      </asp:Label></ItemTemplate><EditItemTemplate><asp:TextBox ID="txtBillQty" CssClass="nonumber"  Width="30px"  runat="server" Text='<%# Bind("BillQty") %>'>
                      </asp:TextBox>
                      </EditItemTemplate>
                      </asp:TemplateField> 

                    <asp:TemplateField HeaderText="Price/Unit"><ItemTemplate><asp:Label ID="lblPrice" runat="server" Text='<%# Bind("Price") %>'>
                    </asp:Label>
                    </ItemTemplate><EditItemTemplate><asp:TextBox ID="txtPrice"  Width="30px" CssClass="nonumber" runat="server" Text='<%# Bind("Price") %>'></asp:TextBox></EditItemTemplate>
                    </asp:TemplateField>   
                    
                                      
                    <%--<asp:CommandField HeaderText="Edit" ShowEditButton="True" />--%>
                    <asp:CommandField HeaderText="Delete" ShowDeleteButton="True" />
                </Columns>
                <PagerStyle CssClass="pgr" />
                <EditRowStyle BackColor="#CCFF33" />
                <AlternatingRowStyle BackColor="#FFDB91" />
                <SelectedRowStyle BackColor="GreenYellow" />
        </asp:GridView> 
        </div>


            </td>
        </tr>
    </table>
  <table border="1" cellpadding="0" cellspacing="0" width="100%">
    <tr>
   <td colspan="4">
   <div class="form-sec-row">
                <label><strong>&nbsp;</strong></label>
                <asp:Button ID="Button5" runat="server"  Text="Submit" CssClass="submit-button" 
                    Height="28px" onclick="Button5_Click"/>
                <asp:Button ID="Button6" runat="server"  Text="Cancel" 
                    CssClass="submit-button"  Height="28px" onclick="Button6_Click"  />
                        
                <div class="clear"></div>
            </div>
   </td>
   </tr> 
       </table>
   </asp:View>


   <asp:View ID="View4" runat="server">
     
          <table border="1" cellpadding="0" cellspacing="0" width="100%">
    <tr>
    <td>
    
                  <div class="pageheader">
         <asp:Label ID="Label6" runat="server"> Patient Previous Consumable Report </asp:Label>
     </div>


                                    <div class="listing" style='width:100%; height:150px; overflow:auto;'>
         <asp:GridView id="GridView3"  CssClass="grid" PagerStyle-CssClass="pgr" DataKeyNames ="RowID" 
                                            runat="server" AutoGenerateColumns="False"
          AllowPaging="True" PageSize ="50"  Width="100%"
                 
              SelectedRowStyle-BackColor="GreenYellow" onpageindexchanging="GridView3_PageIndexChanging" 
                                            onrowcancelingedit="GridView3_RowCancelingEdit" 
                                            onrowdatabound="GridView3_RowDataBound" onrowdeleting="GridView3_RowDeleting" 
                                            onrowediting="GridView3_RowEditing" onrowupdating="GridView3_RowUpdating" >
                <RowStyle HorizontalAlign="Center" />
                <Columns>
                 

                  <asp:TemplateField HeaderText="Id" Visible ="false"><ItemTemplate><asp:Label ID="lblid" runat="server" Text='<%# Bind("RowID") %>'></asp:Label></ItemTemplate></asp:TemplateField>


      
                       <asp:TemplateField HeaderText="Prescription Id"><ItemTemplate><asp:Label ID="lblpid" runat="server" Text='<%# Bind("PrescriptionID") %>'></asp:Label></ItemTemplate></asp:TemplateField>


                           <asp:TemplateField HeaderText="Date"><ItemTemplate><asp:Label ID="lbldate" runat="server" Text='<%# Bind("Date1") %>'></asp:Label></ItemTemplate><EditItemTemplate><asp:TextBox ID="txtdate" runat="server"  Width="90px"  Text='<%# Bind("Date1") %>'></asp:TextBox></EditItemTemplate></asp:TemplateField>
                  

                      <asp:TemplateField HeaderText="Doctor Id" Visible="false"><ItemTemplate><asp:Label ID="lbldoc_id" runat="server" Text='<%# Bind("DoctorId") %>'>
                      </asp:Label></ItemTemplate></asp:TemplateField>


                    <asp:TemplateField HeaderText="Doctor"><ItemTemplate><asp:Label ID="lbldoc_name" runat="server" Text='<%# Bind("doc_name") %>'></asp:Label></ItemTemplate><EditItemTemplate><asp:DropDownList ID="ddlDoctor"  Width="110px"   runat="server"></asp:DropDownList></EditItemTemplate></asp:TemplateField>


                     <asp:TemplateField HeaderText="Consumable Group Id" Visible ="false"><ItemTemplate><asp:Label ID="lblConGrId" runat="server" Text='<%# Bind("ConsumableGr") %>'></asp:Label></ItemTemplate></asp:TemplateField>

                    <asp:TemplateField HeaderText="Consumable Group"><ItemTemplate><asp:Label ID="lblConGroupName" runat="server" Text='<%# Bind("ConGroupName") %>'></asp:Label></ItemTemplate><EditItemTemplate><asp:DropDownList ID="ddlConGroupName"   runat="server"  Width="110px"  AutoPostBack="true"   onselectedindexchanged="ddlConGroupName_SelectedIndexChanged" ></asp:DropDownList></EditItemTemplate></asp:TemplateField>
                  
                  
                 
                   <asp:TemplateField HeaderText="Consumable Item Id" Visible ="false"><ItemTemplate><asp:Label ID="lblConItemID" runat="server" Text='<%# Bind("ConsumableItemId") %>'></asp:Label></ItemTemplate></asp:TemplateField>

                    
                      <asp:TemplateField HeaderText="Consumable Item"><ItemTemplate><asp:Label ID="lblConItemName" runat="server" Text='<%# Bind("ConItemName") %>'>
                      </asp:Label></ItemTemplate><EditItemTemplate><asp:DropDownList ID="ddlConItemName"   Width="110px"  runat="server"></asp:DropDownList></EditItemTemplate></asp:TemplateField> 

                      <asp:TemplateField HeaderText="Actual Quantity"><ItemTemplate><asp:Label ID="lblActualQty" runat="server" Text='<%# Bind("ActualQty") %>'>
                      </asp:Label></ItemTemplate><EditItemTemplate><asp:TextBox ID="txtActualQty"  Width="30px"  runat="server" Text='<%# Bind("ActualQty") %>'></asp:TextBox></EditItemTemplate></asp:TemplateField> 

                    <asp:TemplateField HeaderText="Bill Quantity"><ItemTemplate><asp:Label ID="lblBillQty" runat="server" Text='<%# Bind("BillQty") %>'></asp:Label></ItemTemplate><EditItemTemplate><asp:TextBox ID="txtBillQty"  Width="30px"  runat="server" Text='<%# Bind("BillQty") %>'></asp:TextBox></EditItemTemplate></asp:TemplateField>   
                    
                                      
                    <asp:CommandField HeaderText="Edit" ShowEditButton="True" />
                    <asp:CommandField HeaderText="Delete" ShowDeleteButton="True" />
                </Columns>
                <PagerStyle CssClass="pgr" />
                <EditRowStyle BackColor="#CCFF33" />
                <AlternatingRowStyle BackColor="#FFDB91" />
                <SelectedRowStyle BackColor="GreenYellow" />
        </asp:GridView> 
        </div>

          </td>
    </tr>
    </table>

        <table border="1" cellpadding="0" style='background-color:#C5B9B9;' cellspacing="0" width="100%">
                        <tr>
                        <td colspan="2" align="center">
                            <div class="pageheader">
         <asp:Label ID="Label3" runat="server"> Patient Consumable</asp:Label>
     </div>
                        </td>
                        </tr>
          <tr>

           <td><strong>Template Group:</strong> 
                  <asp:DropDownList ID="DropDownList65" runat="server" AutoPostBack="True" 
                   Width="180px" 
                   onselectedindexchanged="DropDownList65_SelectedIndexChanged" >
                  </asp:DropDownList> </td>
              <td><strong>Template :</strong> 
                  <asp:DropDownList ID="DropDownList66" runat="server" AutoPostBack="True"  
                      Width="180px" onselectedindexchanged="DropDownList66_SelectedIndexChanged"
                     >
                  </asp:DropDownList> </td>
</tr>
</table>


           
             <table border="1" cellpadding="0" style='background-color:#C5B9B9;' cellspacing="0" width="100%">
          <tr>
          <td><strong>Prescription No :</strong> 
              <asp:TextBox ID="TextBox19" runat="server" Enabled="False"></asp:TextBox> </td>
              <td><strong>Date :</strong> <asp:TextBox ID="TextBox20" CssClass="DatepickerReCall"  Width="125px"  runat="server"></asp:TextBox> </td>          
          </tr>
          </table>
      
            
      <table border="1" cellpadding="0" cellspacing="0" width="100%">
          
       <tr style='background-color:#FF9300;'>
                <td align="center">
                  <div class="form-sec-row"> 
             <label class="lname"><strong>Consumable Group</strong></label> 
            </div>
        </td> 

                <td align="center">
                    <div class="form-sec-row"> 
             <label class="lname"><strong>Consumable Item</strong></label>
            </div>
</td>
                <td align="center">
                             <div class="form-sec-row"> 
             <label class="lname"><strong> Actual Quantity </strong></label>
            </div>                  
</td>
 <td align="center">
                    <div class="form-sec-row"> 
             <label class="lname"><strong>Bill Quantity</strong></label> 
            </div>          
            
</td>
 <td align="center">
                    <div class="form-sec-row"> 
             <label class="lname"><strong>Price/Unit</strong></label> 
            </div>          
            
</td>
 <td></td>
        
                      
            </tr>
                <tr>
                <td align="center">
               
             <asp:DropDownList ID="ddlconsumablegr1" runat="server" CssClass="textbox-medium1"   AutoPostBack="true"
                            Width="150px" 
                            onselectedindexchanged="ddlconsumablegr1_SelectedIndexChanged">
                            </asp:DropDownList>
        
            
</td> 

                <td align="center"> 
             <asp:DropDownList ID="ddlConsumableItem1" runat="server" CssClass="textbox-medium1" 
                        Width="150px" AutoPostBack="True" 
                        onselectedindexchanged="ddlConsumableItem1_SelectedIndexChanged">
                            </asp:DropDownList>
             
</td>
                <td align="center">
                      
              <asp:TextBox ID="txtActualQty1" runat="server" CssClass="nonumber"   Width="150px"></asp:TextBox>
                      
</td>
 <td align="center">
                 
                       <asp:TextBox ID="txtBillQty1" runat="server"  CssClass="nonumber"  Width="150px"></asp:TextBox>
             
            
</td> <td align="center">
                 
                       <asp:TextBox ID="txtPrice1" runat="server"  CssClass="nonumber"  Width="150px"></asp:TextBox>
             
            
</td>

<td align="center">
    <asp:Button ID="Button7" runat="server" Text="add" CssClass="submit-button" 
        onclick="Button7_Click1"  />
</td>
 
 </tr>

 </table> 

 <table border="1" cellpadding="0" cellspacing="0" width="100%">
    <tr>
    <td>
    
                  <div class="pageheader">
         <asp:Label ID="Label15" runat="server"> Preview </asp:Label>
     </div>


                                    <div class="listing" style='width:100%; height:150px; overflow:auto;'>
         <asp:GridView id="GridView8"  CssClass="grid" PagerStyle-CssClass="pgr" DataKeyNames ="RowID" 
                                            runat="server" AutoGenerateColumns="False"
          AllowPaging="True" PageSize ="50"  Width="100%"   SelectedRowStyle-BackColor="GreenYellow" 
                                            onpageindexchanging="GridView8_PageIndexChanging" 
                                            onrowcancelingedit="GridView8_RowCancelingEdit" 
                                            onrowdatabound="GridView8_RowDataBound" onrowdeleting="GridView8_RowDeleting" 
                                            onrowediting="GridView8_RowEditing" onrowupdating="GridView8_RowUpdating"  >
                <RowStyle HorizontalAlign="Center" />
                <Columns>
                 

                  <asp:TemplateField HeaderText="Sl.No."><ItemTemplate><asp:Label ID="lblid" runat="server" Text='<%# Bind("RowID") %>'>
                  </asp:Label></ItemTemplate></asp:TemplateField>

                     <asp:TemplateField HeaderText="Consumable Group Id" Visible ="false"><ItemTemplate><asp:Label ID="lblConGrId" runat="server" Text='<%# Bind("ConsumableGrId") %>'></asp:Label></ItemTemplate></asp:TemplateField>

                    <asp:TemplateField HeaderText="Consumable Group"><ItemTemplate><asp:Label ID="lblConGroupName" runat="server" Text='<%# Bind("ConGroupName") %>'>
                    </asp:Label></ItemTemplate><EditItemTemplate><asp:DropDownList ID="ddlConGroupName"   runat="server"  Width="110px"  AutoPostBack="true"   onselectedindexchanged="ddlConGroupName_SelectedIndexChanged1" ></asp:DropDownList></EditItemTemplate></asp:TemplateField>
                  
                  
                 
                   <asp:TemplateField HeaderText="Consumable Item Id" Visible ="false"><ItemTemplate><asp:Label ID="lblConItemID" runat="server" Text='<%# Bind("ConsumableItemId") %>'></asp:Label></ItemTemplate></asp:TemplateField>

                    
                      <asp:TemplateField HeaderText="Consumable Item"><ItemTemplate><asp:Label ID="lblConItemName" runat="server" Text='<%# Bind("ConItemName") %>'>
                      </asp:Label></ItemTemplate><EditItemTemplate><asp:DropDownList ID="ddlConItemName"   Width="110px" AutoPostBack="true" onselectedindexchanged="ddlConItemName_SelectedIndexChanged"  runat="server"></asp:DropDownList></EditItemTemplate></asp:TemplateField> 

                      <asp:TemplateField HeaderText="Actual Quantity"><ItemTemplate><asp:Label ID="lblActualQty" runat="server" Text='<%# Bind("ActualQty") %>'>
                      </asp:Label></ItemTemplate><EditItemTemplate><asp:TextBox ID="txtActualQty" CssClass="nonumber"  Width="30px"  runat="server" Text='<%# Bind("ActualQty") %>'>
                      </asp:TextBox></EditItemTemplate></asp:TemplateField> 

                    <asp:TemplateField HeaderText="Bill Quantity"><ItemTemplate><asp:Label ID="lblBillQty" runat="server" Text='<%# Bind("BillQty") %>'>
                    </asp:Label></ItemTemplate><EditItemTemplate><asp:TextBox ID="txtBillQty"  Width="30px" CssClass="nonumber" runat="server" Text='<%# Bind("BillQty") %>'></asp:TextBox></EditItemTemplate></asp:TemplateField>  
                     <asp:TemplateField HeaderText="Price"><ItemTemplate><asp:Label ID="lblPrice" runat="server" Text='<%# Bind("Price") %>'>
                    </asp:Label></ItemTemplate><EditItemTemplate><asp:TextBox ID="txtPrice"  Width="30px" CssClass="nonumber" runat="server" Text='<%# Bind("Price") %>'></asp:TextBox></EditItemTemplate></asp:TemplateField>   
                    
                                      
                    <asp:CommandField HeaderText="Edit" ShowEditButton="True" />
                    <asp:CommandField HeaderText="Delete" ShowDeleteButton="True" />
                </Columns>
                <PagerStyle CssClass="pgr" />
                <EditRowStyle BackColor="#CCFF33" />
                <AlternatingRowStyle BackColor="#FFDB91" />
                <SelectedRowStyle BackColor="GreenYellow" />
        </asp:GridView> 
        </div>

         <div class="form-sec-row">
                <label><strong>&nbsp;</strong></label>
                <asp:Button ID="Button8" runat="server"  Text="Submit" CssClass="submit-button" 
                    Height="28px" onclick="Button8_Click"  />
                <asp:Button ID="Button11" runat="server"  Text="Cancel" 
                    CssClass="submit-button"  Height="28px" onclick="Button11_Click" 
                      />
                        
                <div class="clear"></div>
            </div>
          </td>
    </tr>
    </table> 
   </asp:View>

    <asp:View ID="View6" runat="server">
    
                       <div class="form-sec-row">
                        <label>
                        <strong>
                         Doctor Type :</strong></label>
                        <asp:DropDownList ID="DropDownList2" runat="server"   Width="205px"
                               AutoPostBack="True" 
                               onselectedindexchanged="DropDownList2_SelectedIndexChanged" >                               
                         </asp:DropDownList>
                        <div class="clear">
                        </div>
                    </div>

                      <div class="form-sec-row">
                        <label>
                        <strong>
                        Visiting Doctor :</strong></label>
                        <asp:DropDownList ID="DropDownList1" runat="server"  Width="205px"
                              AutoPostBack="True" 
                              onselectedindexchanged="DropDownList1_SelectedIndexChanged" >                               
                         </asp:DropDownList>
                        <div class="clear">
                        </div>
                    </div>

                     
                     <div class="form-sec-row">
                        <label>
                        <strong> 
                       Date of Visit:</strong></label>
                        <asp:TextBox ID="txtdate" runat="server"  Width="200px" ></asp:TextBox>
                              <asp:Label ID="Label12" runat="server" ForeColor="Green" Text="(DD/MM/YYYY)"></asp:Label>
                        <div class="clear">
                        </div>
                    </div>

                            <div class="form-sec-row">
                        <label>
                        <strong> 
                        Time of Visit :</strong></label>
                        <asp:TextBox ID="TextBox1" runat="server"  Width="200px"  ></asp:TextBox>
                        <div class="clear">
                        </div>
                    </div>
        <div class="form-sec-row">
                        <label>
                        <strong> 
                        Type of Visit :</strong></label>
                        <asp:DropDownList ID="DropDownList3" runat="server"  Width="205px" >                               
                         </asp:DropDownList>
                        <div class="clear">
                        </div>
                    </div>

             <div class="form-sec-row">
                        <label>
                        <strong>
                       Probable Discharge Date :</strong></label>
                          <asp:TextBox ID="TextBox8" runat="server"  Width="200px"  ></asp:TextBox>
                                <asp:Label ID="Label13" runat="server" ForeColor="Green" Text="(DD/MM/YYYY)"></asp:Label>
                        <div class="clear">
                        </div>
                    </div>

                         <div class="form-sec-row">
                        <label>
                        <strong> 
                     Additional Remarks :</strong></label>
                        <asp:TextBox ID="TextBox26" runat="server"    Width="200px"  Height="60px"
                            TextMode="MultiLine" ></asp:TextBox>
                        <div class="clear">
                        </div>
                    </div>

                          <div class="form-sec-row">
                        <label>
                        <strong>
                       Bill Effect :</strong></label>
                              <asp:CheckBox ID="CheckBox1" runat="server" />
                        <div class="clear">
                        </div>
                    </div>

                    <div class="form-sec-row">
                        &nbsp;&nbsp;&nbsp;<label><strong>&nbsp;</strong></label>
                        <asp:Button ID="Button1" runat="server" CssClass="submit-button"   Height="28px" 
                            Text="Submit" onclick="Button1_Click" />
                        <asp:Button ID="Button2" runat="server" CssClass="submit-button"   Height="28px" 
                            Text="Cancel" onclick="Button2_Click"  />
                        <div class="clear">
                        </div>
                    </div>
                
                    <div class="form-sec-row">
                        <div class="clear">
                        </div>
                    </div>

                    <div class="listing"  style='width:100%; height:auto; overflow:auto;'>
                <asp:GridView id="GridView1"  Width="100%" CssClass="grid" PagerStyle-CssClass="pgr"
                 DataKeyNames ="Rowid" runat="server" 
                 AutoGenerateColumns="False" AllowPaging="True"  PageSize="10"
                 SelectedRowStyle-BackColor="GreenYellow" 
                    onpageindexchanging="GridView1_PageIndexChanging" 
                    onrowcommand="GridView1_RowCommand" >
                    <RowStyle HorizontalAlign="Center" />
                    <Columns>
                        <asp:TemplateField HeaderText="ID" Visible="false"><ItemTemplate><asp:Label ID="lblid" runat="server" Text='<%# Bind("Rowid") %>'></asp:Label></ItemTemplate></asp:TemplateField>
             
                        <asp:TemplateField HeaderText="Registration No"><ItemTemplate><asp:Label ID="lblRegno" runat="server" Text='<%# Bind("PatientReg") %>'></asp:Label></ItemTemplate></asp:TemplateField>


                         <asp:TemplateField HeaderText="Patient's Name"><ItemTemplate><asp:Label ID="lbllname" runat="server" Text='<%# Bind("patient_name") %>'></asp:Label></ItemTemplate></asp:TemplateField>

                         <asp:TemplateField HeaderText="Admission Date"><ItemTemplate><asp:Label ID="lbladate" runat="server" Text='<%# Bind("adate") %>'></asp:Label></ItemTemplate></asp:TemplateField>

                        <asp:TemplateField HeaderText="Bed No"><ItemTemplate><asp:Label ID="lblbedno" runat="server" Text='<%# Bind("BedNoText") %>'></asp:Label></ItemTemplate></asp:TemplateField>

                         <asp:TemplateField HeaderText="Time" ><ItemTemplate><asp:Label ID="lblTime" runat="server" Text='<%# Bind("Time") %>'></asp:Label></ItemTemplate></asp:TemplateField>
                             <asp:TemplateField HeaderText="Doc Name"><ItemTemplate><asp:Label ID="lbldocid" runat="server" Text='<%# Bind("doc_name") %>'></asp:Label></ItemTemplate></asp:TemplateField>   
                             <asp:TemplateField HeaderText="Doc Type" Visible="false"><ItemTemplate><asp:Label ID="lbldoctypeid" runat="server" Text='<%# Bind("DocTypeID") %>'></asp:Label></ItemTemplate></asp:TemplateField> 
                             <asp:TemplateField HeaderText="Visiting Date" ><ItemTemplate><asp:Label ID="lblvisitdate" runat="server" Text='<%# Bind("visitdate") %>'></asp:Label></ItemTemplate></asp:TemplateField>
                                  
                                        <asp:TemplateField HeaderText="Remarks" Visible="false"><ItemTemplate><asp:Label ID="lblremarks" runat="server" Text='<%# Bind("Remarks") %>'></asp:Label></ItemTemplate></asp:TemplateField>      
                        <asp:TemplateField HeaderText="Visit Type" Visible="false"><ItemTemplate><asp:Label ID="lblvisittype" runat="server" Text='<%# Bind("VisitType") %>'></asp:Label></ItemTemplate></asp:TemplateField>   
                        <asp:CommandField SelectText="Edit" ShowSelectButton="True" ControlStyle-CssClass="temp" HeaderText="Edit" SelectImageUrl="~/Images/edit.png" ButtonType="Image"><ControlStyle CssClass="temp"></ControlStyle></asp:CommandField>
               
                    </Columns>
                    <PagerStyle CssClass="pgr" />
                    <EditRowStyle BackColor="#CCFF33" />
                    <AlternatingRowStyle BackColor="#FFDB91" />
                </asp:GridView>
            </div>
    </asp:View>

        <asp:View ID="View7" runat="server">
            <div class="form-sec-row">
                        <label>
                        <strong> 
                     Visited Doctors :</strong></label>
                        <asp:DropDownList ID="ddlvisiteddoc" runat="server"  Width="205px"
                              AutoPostBack="True" 
                              onselectedindexchanged="ddlvisiteddoc_SelectedIndexChanged" >                               
                         </asp:DropDownList>
                        <div class="clear">
                        </div>
                    </div>
      <div class="form-sec-row">
                        <label>
                        <strong> 
                     Treate Note :</strong></label>
                        <asp:TextBox ID="TextBox28" runat="server"    Width="200px"  Height="60px"
                            TextMode="MultiLine" ></asp:TextBox>
                        <div class="clear">
                        </div>
                    </div>

                        

                    <div class="form-sec-row">
                        &nbsp;&nbsp;&nbsp;<label><strong>&nbsp;</strong></label>
                        <asp:Button ID="Button10" runat="server" CssClass="submit-button"   Height="28px" 
                            Text="Submit" onclick="Button10_Click" />
                        <asp:Button ID="Button12" runat="server" CssClass="submit-button"   Height="28px" 
                            Text="Cancel" onclick="Button12_Click"   />
                        <div class="clear">
                        </div>
                    </div>
                    </asp:View>
   </asp:MultiView>
   </div>
   
    
                </div>
            
            
            </div>
    </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>

