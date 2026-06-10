<%@ Page Title="" Language="C#" MasterPageFile="~/MasterAll/MasterPageAll.master" AutoEventWireup="true" CodeFile="AddMedicine.aspx.cs" Inherits="IPD_AddMedicine" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <style type="text/css">
        .auto-style1
        {
            visibility: hidden;
        }
        .auto-style2
        {
            visibility: hidden;
            width: 4px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">

    <script type="text/javascript" language="javascript">
    function ShowDialog() {
        var rtvalue = window.open("IpdpatientPopup.aspx", "sss", "Width:800px; Height:550px; dialogLeft:250px;");
        document.getElementById("ctl00_ContentPlaceHolder1_TextBox23").value = rtvalue.NameValue;

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
        document.getElementById("ctl00_ContentPlaceHolder1_HiddenField2").value = time; 
    }

    function Calling() {
        var date = new Date();
        $('.DatepickerCall').datepicker({ dateFormat: 'dd/mm/yy' });
        $('.DatepickerCall1').datepicker({ dateFormat: 'dd/mm/yy' });

        $("input[id$='Tab2']").click(function () {


            if ($("input[id$='TextBox23']").val() == '') {
                alert('Please Enter Registration No !');
                $("input[id$='TextBox23']").focus();
                $("input[id$='TextBox23']").addClass('textboxerr');
                return false;
            }
            else {
                $("input[id$='TextBox23']").removeClass('textboxerr');
            }
        });


        $("input[id$='Button1']").click(function () {


            if ($("input[id$='TextBox23']").val() == '') {
                alert('Please Enter Registration No !');
                $("input[id$='TextBox23']").focus();
                $("input[id$='TextBox23']").addClass('textboxerr');
                return false;
            }
            else {
                $("input[id$='TextBox23']").removeClass('textboxerr');
            }


            if ($("input[id$='TextBox1']").val() == '') {
                alert('Please Enter Issue Date !');
                $("input[id$='TextBox1']").focus();
                $("input[id$='TextBox1']").addClass('textboxerr');
                return false;
            }
            else {
                $("input[id$='TextBox1']").removeClass('textboxerr');
            }

            if ($("select[id$='DropDownList3']").val() == '0') {
                alert('Please Select Visiting  Doctor!');
                $("select[id$='DropDownList3']").addClass('textboxerr');
                $("select[id$='DropDownList3']").focus();
                return false;
            }
            else {
                $("select[id$='DropDownList3']").removeClass('textboxerr');
            }


            if ($("select[id$='DropDownList1']").val() == '0') {
                alert('Please Select Medicine Group !');
                $("select[id$='DropDownList1']").addClass('textboxerr');
                $("select[id$='DropDownList1']").focus();
                return false;
            }
            else {
                $("select[id$='DropDownList1']").removeClass('textboxerr');
            }

            if ($("select[id$='DropDownList2']").val() == '0') {
                alert('Please Select Medicine !');
                $("select[id$='DropDownList2']").addClass('textboxerr');
                $("select[id$='DropDownList2']").focus();
                return false;
            }
            else {
                $("select[id$='DropDownList2']").removeClass('textboxerr');
            }


            if ($("select[id$='ddlBatchNo1']").val() == '0') {
                alert('Please Select Batch No !');
                $("select[id$='ddlBatchNo1']").addClass('textboxerr');
                $("select[id$='ddlBatchNo1']").focus();
                return false;
            }
            else {
                $("select[id$='ddlBatchNo1']").removeClass('textboxerr');
            }

            if ($("select[id$='ddlSubGroup1']").val() == '0') {
                alert('Please Select Sub Group !');
                $("select[id$='ddlSubGroup1']").addClass('textboxerr');
                $("select[id$='ddlSubGroup1']").focus();
                return false;
            }
            else {
                $("select[id$='ddlSubGroup1']").removeClass('textboxerr');
            }



            if ($("input[id$='TextBox23']").val() == '') {
                alert('Please Enter Registration No !');
                $("input[id$='TextBox23']").focus();
                $("input[id$='TextBox23']").addClass('textboxerr');
                return false;
            }
            else {
                $("input[id$='TextBox23']").removeClass('textboxerr');
            }



            if ($("input[id$='TextBox2']").val() == '') {
                alert('Please Enter Dose !');
                $("input[id$='TextBox2']").focus();
                $("input[id$='TextBox2']").addClass('textboxerr');
                return false;
            }
            else {
                $("input[id$='TextBox2']").removeClass('textboxerr');
            }

            if ($("input[id$='TextBox3']").val() == '') {
                alert('Please Enter Duration !');
                $("input[id$='TextBox3']").focus();
                $("input[id$='TextBox3']").addClass('textboxerr');
                return false;
            }
            else {
                $("input[id$='TextBox3']").removeClass('textboxerr');
            }


            if ($("input[id$='txtBillQty1']").val() == '') {
                alert('Please Enter Bill Quantity !');
                $("input[id$='txtBillQty1']").focus();
                $("input[id$='txtBillQty1']").addClass('textboxerr');
                return false;
            }
            else {
                $("input[id$='txtBillQty1']").removeClass('textboxerr');
            }



            if ($("input[id$='txtExpiryDate1']").val() == '') {
                alert('Please Enter Expiry Date !');
                $("input[id$='txtExpiryDate1']").focus();
                $("input[id$='txtExpiryDate1']").addClass('textboxerr');
                return false;
            }
            else {
                $("input[id$='txtExpiryDate1']").removeClass('textboxerr');
            }
        });


        $("input[id$='txtBillQty1']").keydown(function (event) {
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

    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
    <ContentTemplate>
      <div class="pageheader">
         <asp:Label ID="Label1" runat="server">Add Medicine</asp:Label>
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
            <asp:HiddenField ID="HiddenField1" runat="server" />   <asp:HiddenField ID="HiddenField2" runat="server" />
			           
                       
             <div class="form-sec-row">
                        <label>
                        <strong>
                       Registration No :</strong></label>
                          <asp:TextBox ID="TextBox23" runat="server" CssClass="textbox-medium1" 
                              Enabled="False"></asp:TextBox>
                                <asp:Button ID="Button3" runat="server" Text="Quick Search" height="28px" CssClass="submit-buttonCheck" OnClientClick="ShowDialog()"/>
                              <asp:Button ID="Button2" runat="server" Text="Fetch" height="28px"
                            CssClass="submit-button" onclick="Button2_Click"/>
                        <div class="clear">
                        </div>
                    </div>
                        <div class="form-sec-row">
                        <label>
                        <strong>
                       Patient's Name :</strong></label>
                          <asp:TextBox ID="TextBox24" runat="server" CssClass="textbox-medium1" 
                                Enabled="False"></asp:TextBox>
                        <div class="clear">
                        </div>
                    </div>

                        <div class="form-sec-row">
                        <label>
                        <strong>
                       Bed No :</strong></label>
                          <asp:TextBox ID="TextBox25" runat="server" CssClass="textbox-medium1" 
                                Enabled="False"></asp:TextBox>
                        <div class="clear">
                        </div>
                    </div>

                        <div class="form-sec-row">
                        <label>
                        <strong>
                       Admission Date :</strong></label>
                          <asp:TextBox ID="TextBox26" runat="server" CssClass="textbox-medium1" 
                                Enabled="False"></asp:TextBox>
                                      <asp:Label ID="Label3" runat="server" ForeColor="Green" Text="(DD/MM/YYYY)"></asp:Label>
                        <div class="clear">
                        </div>
                    </div>

                    
                        <div class="form-sec-row">
                        <label>
                        <strong>
                       Advice By :</strong></label>
                               <asp:DropDownList ID="DropDownList3" runat="server" CssClass="textbox-medium1" >
                            </asp:DropDownList>
                        <div class="clear">
                        </div>
                    </div>

                    <div class="form-sec-row">
                        <label>
                        <strong>
                       Issue Date :</strong></label>
                             <asp:TextBox ID="TextBox1" runat="server" CssClass="DatepickerCall"></asp:TextBox>
                        <div class="clear">
                        </div>
                    </div>

             
                             
   
     </div>
    
     
       <center>
       <div style='width:100%; height:auto; overflow:auto;'>
    <table border="1" cellpadding="0" cellspacing="0" width="100%">
          
       <tr style='background-color:#FF9300;'>
                <td align="center">
                
             <label class="lname"><strong>Medicine Group</strong></label> 
            
        </td> 

                <td align="center">
                  
             <label class="lname"><strong>Sub Group</strong></label>
           
</td>
                <td align="center">
                  
             <label class="lname"><strong>Medicine</strong></label>
           
</td>
  


 <td align="center">
                  
             <label class="lname"><strong>Batch No</strong></label> 
           
            
</td>

 <td align="center">
                  
             <label class="lname"><strong>Expiry Date</strong></label> 
           
            
</td>
 <td align="center">
                  
             <label class="lname"><strong>Dose</strong></label> 
           
            
</td>

                <td align="center" style="display:none;"><strong>Duration</strong></td>

                <td align="center"><strong>Actual Qty</strong></td>

 <td align="center" class="auto-style2">
                  
             <label class="lname"><strong></strong></label> 
           
            
</td>

 <td align="center">
                  
             <label class="lname"><strong>Bill Qty</strong></label> 
           
            
</td>
 </tr>

                <tr>
                <td align="center">
                  
             <asp:DropDownList ID="ddlMedGroup1" runat="server" Width="150px" AppendDataBoundItems="true" AutoPostBack="True" 
                            onselectedindexchanged="ddlMedGroup1_SelectedIndexChanged">
                            </asp:DropDownList>
           
            
</td> 

       <td align="center">
                  
             <asp:DropDownList ID="ddlSubGroup1" runat="server"     Width="150px"  AutoPostBack="true" 
                 onselectedindexchanged="ddlSubGroup1_SelectedIndexChanged">
                            </asp:DropDownList>
           
</td>
                <td align="center">
                  
             <asp:DropDownList ID="ddlMedicine1" runat="server"     Width="150px"  AutoPostBack="true" 
                        onselectedindexchanged="ddlMedicine1_SelectedIndexChanged">
                            </asp:DropDownList>
           
</td>
            
 
 <td align="center">
                  
           <asp:DropDownList ID="ddlBatchNo1" runat="server" Width="150px" AutoPostBack="true" 
               onselectedindexchanged="ddlBatchNo1_SelectedIndexChanged">
                            </asp:DropDownList>
           
            
</td>
 <td align="center">
                  
             <asp:TextBox ID="txtExpiryDate1" runat="server" CssClass="DatepickerCall1" 
                 Width="70px" Enabled="False"></asp:TextBox>
           
            
</td>
 <td align="center" >
                    
           <asp:DropDownList ID="ddlDose1" runat="server" Width="75px" AutoPostBack="True" 
               onselectedindexchanged="ddlDose1_SelectedIndexChanged" >
                            </asp:DropDownList>
           
            
</td>
                    <td align="center" style="display:none;">
                        <asp:DropDownList ID="ddlDuration21" runat="server" AutoPostBack="True" onselectedindexchanged="ddlDuration21_SelectedIndexChanged" Width="75px">
                        </asp:DropDownList>
                    </td>
                    <td align="center" >
                        <asp:TextBox ID="txtActualQty21" runat="server"  Width="60px"></asp:TextBox>
                    </td>
 <td align="center" class="auto-style2" >
                    
                <asp:DropDownList ID="dlDuration1" runat="server" Width="1px" >
                            </asp:DropDownList>
           
            
</td>
 <td align="center">
                 
             <asp:TextBox ID="txtBillQty1" runat="server"   Width="60px"></asp:TextBox>
            
            
</td>
 </tr>

 <tr>
                <td align="center">
                  
             <asp:DropDownList ID="ddlMedGroup2" runat="server"  
                            Width="150px" AppendDataBoundItems="true" AutoPostBack="True" 
                        onselectedindexchanged="ddlMedGroup2_SelectedIndexChanged"  >
                            </asp:DropDownList>
           
            
</td> 

       <td align="center">
                  
             <asp:DropDownList ID="ddlSubGroup2" runat="server"  
                 Width="150px"  AutoPostBack="true" 
                 onselectedindexchanged="ddlSubGroup2_SelectedIndexChanged"  >
                            </asp:DropDownList>
           
</td>
                <td align="center">
                  
             <asp:DropDownList ID="ddlMedicine2" runat="server"  
                        Width="150px"  AutoPostBack="true" 
                        onselectedindexchanged="ddlMedicine2_SelectedIndexChanged" >
                            </asp:DropDownList>
           
</td>
            
 
 <td align="center">
                  
           <asp:DropDownList ID="ddlBatchNo2" runat="server"  
               Width="150px" AutoPostBack="true" 
               onselectedindexchanged="ddlBatchNo2_SelectedIndexChanged" >
                            </asp:DropDownList>
           
            
</td>
 <td align="center">
                  
             <asp:TextBox ID="txtExpiryDate2" runat="server" CssClass="DatepickerCall1" 
                 Width="70px" Enabled="False"></asp:TextBox>
           
            
</td>
 <td align="center" >
                    
                   <asp:DropDownList ID="ddlDose2" runat="server"   Width="75px" 
                       AutoPostBack="True" onselectedindexchanged="ddlDose2_SelectedIndexChanged" >
                            </asp:DropDownList>
            
</td>
                <td align="center" style="display:none;">
                    <asp:DropDownList ID="ddlDuration22" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlDuration22_SelectedIndexChanged" Width="75px">
                    </asp:DropDownList>
                </td>
                <td align="center">
                    <asp:TextBox ID="txtActualQty22" runat="server"  Width="60px"></asp:TextBox>
                </td>
 <td align="center" class="auto-style2" >
                    
              <asp:DropDownList ID="dlDuration2" runat="server"   Width="1px" >
                            </asp:DropDownList>
</td>
 <td align="center">
                 
             <asp:TextBox ID="txtBillQty2" runat="server"   Width="60px"></asp:TextBox>
            
            
</td>
 </tr>

 <tr>
                <td align="center">
                  
             <asp:DropDownList ID="ddlMedGroup3" runat="server"  
                            Width="150px" AppendDataBoundItems="true" AutoPostBack="True" 
                        onselectedindexchanged="ddlMedGroup3_SelectedIndexChanged" >
                            </asp:DropDownList>
           
            
</td> 

       <td align="center">
                  
             <asp:DropDownList ID="ddlSubGroup3" runat="server"  
                 Width="150px"  AutoPostBack="true" 
                 onselectedindexchanged="ddlSubGroup3_SelectedIndexChanged"  >
                            </asp:DropDownList>
           
</td>
                <td align="center">
                  
             <asp:DropDownList ID="ddlMedicine3" runat="server"  
                        Width="150px"  AutoPostBack="true" 
                        onselectedindexchanged="ddlMedicine3_SelectedIndexChanged" >
                            </asp:DropDownList>
           
</td>
            
 
 <td align="center">
                  
           <asp:DropDownList ID="ddlBatchNo3" runat="server"  
               Width="150px" AutoPostBack="true" 
               onselectedindexchanged="ddlBatchNo3_SelectedIndexChanged" >
                            </asp:DropDownList>
           
            
</td>
 <td align="center">
                  
             <asp:TextBox ID="txtExpiryDate3" runat="server" CssClass="DatepickerCall1" 
                 Width="70px" Enabled="False"></asp:TextBox>
           
            
</td>
 <td align="center" >
                    
              <asp:DropDownList ID="ddlDose3" runat="server"   Width="75px" 
                  AutoPostBack="True" onselectedindexchanged="ddlDose3_SelectedIndexChanged" >
                            </asp:DropDownList>
           
            
</td>
                <td align="center" style="display:none;">
                    <asp:DropDownList ID="ddlDuration23" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlDuration23_SelectedIndexChanged" Width="75px">
                    </asp:DropDownList>
                </td>
                <td align="center">
                    <asp:TextBox ID="txtActualQty23" runat="server"  Width="60px"></asp:TextBox>
                </td>
 <td align="center" class="auto-style2" >
                    
             <asp:DropDownList ID="dlDuration3" runat="server"   Width="1px" >
                            </asp:DropDownList> 
            
</td>
 <td align="center">
                 
             <asp:TextBox ID="txtBillQty3" runat="server"   Width="60px"></asp:TextBox>
            
            
</td>
 </tr>

 <tr>
                <td align="center">
                  
             <asp:DropDownList ID="ddlMedGroup4" runat="server"  
                            Width="150px" AppendDataBoundItems="true" AutoPostBack="True" 
                        onselectedindexchanged="ddlMedGroup4_SelectedIndexChanged" >
                            </asp:DropDownList>
           
            
</td> 

       <td align="center">
                  
             <asp:DropDownList ID="ddlSubGroup4" runat="server"  
                 Width="150px"  AutoPostBack="true" 
                 onselectedindexchanged="ddlSubGroup4_SelectedIndexChanged"  >
                            </asp:DropDownList>
           
</td>
                <td align="center">
                  
             <asp:DropDownList ID="ddlMedicine4" runat="server"  
                        Width="150px"  AutoPostBack="true" 
                        onselectedindexchanged="ddlMedicine4_SelectedIndexChanged" >
                            </asp:DropDownList>
           
</td>
            
 
 <td align="center">
                  
           <asp:DropDownList ID="ddlBatchNo4" runat="server"  
               Width="150px" AutoPostBack="true" 
               onselectedindexchanged="ddlBatchNo4_SelectedIndexChanged" >
                            </asp:DropDownList>
           
            
</td>
 <td align="center">
                  
             <asp:TextBox ID="txtExpiryDate4" runat="server" CssClass="DatepickerCall1" 
                 Width="70px" Enabled="False"></asp:TextBox>
           
            
</td>
 <td align="center" >
                    
              <asp:DropDownList ID="ddlDose4" runat="server"   Width="75px" 
                  AutoPostBack="True" onselectedindexchanged="ddlDose4_SelectedIndexChanged" >
                            </asp:DropDownList>
           
            
</td>
                <td align="center" style="display:none;">
                    <asp:DropDownList ID="ddlDuration24" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlDuration24_SelectedIndexChanged" Width="75px">
                    </asp:DropDownList>
                </td>
                <td align="center">
                    <asp:TextBox ID="txtActualQty24" runat="server"  Width="60px"></asp:TextBox>
                </td>
 <td align="center" class="auto-style2" >
                    
              <asp:DropDownList ID="dlDuration4" runat="server"   Width="1px" >
                            </asp:DropDownList> 
</td>
 <td align="center">
                 
             <asp:TextBox ID="txtBillQty4" runat="server"   Width="60px"></asp:TextBox>
            
            
</td>
 </tr>

 <tr>
                <td align="center">
                  
             <asp:DropDownList ID="ddlMedGroup5" runat="server"  
                            Width="150px" AppendDataBoundItems="true" AutoPostBack="True" 
                        onselectedindexchanged="ddlMedGroup5_SelectedIndexChanged"  >
                            </asp:DropDownList>
           
            
</td> 

       <td align="center">
                  
             <asp:DropDownList ID="ddlSubGroup5" runat="server" Width="150px"  
                 AutoPostBack="true" 
                 onselectedindexchanged="ddlSubGroup5_SelectedIndexChanged"  >
                            </asp:DropDownList>
           
</td>
                <td align="center">
                  
             <asp:DropDownList ID="ddlMedicine5" runat="server"  
                        Width="150px"  AutoPostBack="true" 
                        onselectedindexchanged="ddlMedicine5_SelectedIndexChanged" >
                            </asp:DropDownList>
           
</td>
            
 
 <td align="center">
                  
           <asp:DropDownList ID="ddlBatchNo5" runat="server"  
               Width="150px" AutoPostBack="true" 
               onselectedindexchanged="ddlBatchNo5_SelectedIndexChanged" >
                            </asp:DropDownList>
           
            
</td>
 <td align="center">
                  
             <asp:TextBox ID="txtExpiryDate5" runat="server" CssClass="DatepickerCall1" 
                 Width="70px" Enabled="False"></asp:TextBox>
           
            
</td>
 <td align="center" >
                    
           <asp:DropDownList ID="ddlDose5" runat="server"   Width="75px" 
               AutoPostBack="True" onselectedindexchanged="ddlDose5_SelectedIndexChanged" >
                            </asp:DropDownList>
           
            
</td>
                <td align="center" style="display:none;">
                    <asp:DropDownList ID="ddlDuration25" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlDuration25_SelectedIndexChanged" Width="75px">
                    </asp:DropDownList>
                </td>
                <td align="center">
                    <asp:TextBox ID="txtActualQty25" runat="server"  Width="60px"></asp:TextBox>
                </td>
 <td align="center" class="auto-style2" >
            <asp:DropDownList ID="dlDuration5" runat="server"   Width="1px" >
                            </asp:DropDownList>  
</td>
 <td align="center">
                 
             <asp:TextBox ID="txtBillQty5" runat="server"   Width="60px"></asp:TextBox>
            
            
</td>
 </tr>


  
<tr>
<td colspan="10" align="left" style='padding-left:250px;'>
 <asp:Button ID="Button1" runat="server" Text="Submit" 
        CssClass="submit-button" onclick="Button1_Click"/>   
    <asp:Button ID="Button4" runat="server" Text="Cancel" 
        CssClass="submit-button" onclick="Button4_Click" />
     </td>
</tr>
 
       </table>
       </div>
    </center>
     </asp:View>
         <asp:View ID="View2" runat="server">
    <div class="listing">
                <asp:GridView id="GridView1"  Width="100%" CssClass="grid" PagerStyle-CssClass="pgr"
                 DataKeyNames ="RowID" runat="server" 
                 AutoGenerateColumns="False" AllowPaging="True" 
                 SelectedRowStyle-BackColor="GreenYellow" 
                    onpageindexchanging="GridView1_PageIndexChanging" 
                    onrowcommand="GridView1_RowCommand" onrowdeleting="GridView1_RowDeleting" >
                    <RowStyle HorizontalAlign="Center" />
                    <Columns>
                        <asp:TemplateField HeaderText="ID" Visible="false">
                            <ItemTemplate>
                                <asp:Label ID="lblid" runat="server" Text='<%# Bind("RowID") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
             
                        <asp:TemplateField HeaderText="Registration No" Visible="false">
                            <ItemTemplate>
                                <asp:Label ID="lblRegno" runat="server" Text='<%# Bind("PatientReg") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>


                         <asp:TemplateField HeaderText="Patient's Name" Visible="false">
                            <ItemTemplate>
                                <asp:Label ID="lbllname" runat="server" Text='<%# Bind("patient_name") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>

                         <asp:TemplateField HeaderText="Admission Date" Visible="false">
                            <ItemTemplate>
                                <asp:Label ID="lbladate" runat="server" Text='<%# Bind("adate") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="Bed No" Visible="false">
                            <ItemTemplate>
                                <asp:Label ID="lblbedno" runat="server" Text='<%# Bind("BedNoText") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>

                         <asp:TemplateField HeaderText="Medicine Group" >
                            <ItemTemplate>
                                <asp:Label ID="lblMedicineGroupName" runat="server" Text='<%# Bind("MedicineGroupName") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                             <asp:TemplateField HeaderText="Medicine Name" >
                            <ItemTemplate>
                                <asp:Label ID="lblMedicineName" runat="server" Text='<%# Bind("MedicineName") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>   

                        <asp:TemplateField HeaderText="Medicine Sub group" Visible="false" >
                            <ItemTemplate>
                                <asp:Label ID="lblMedicineSubGrId" runat="server" Text='<%# Bind("MedicineSubGrId") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>   


                             <asp:TemplateField HeaderText="Issue Date" >
                            <ItemTemplate>
                                <asp:Label ID="lblisdate" runat="server" Text='<%# Bind("isdate") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                                         
                    <asp:TemplateField HeaderText="Adviced By" Visible="false" >
                            <ItemTemplate>
                                <asp:Label ID="lbladvicedby" runat="server" Text='<%# Bind("AdviceBy") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>

                             <asp:TemplateField HeaderText="Quantity">
                            <ItemTemplate>
                                <asp:Label ID="lblBillQty" runat="server" Text='<%# Bind("BillQty") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Actual Quantity" Visible="false">
                            <ItemTemplate>
                                <asp:Label ID="lblActualQty" runat="server" Text='<%# Bind("ActualQty") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>

                             <asp:TemplateField HeaderText="Batch No">
                            <ItemTemplate>
                                <asp:Label ID="lblBatchNo" runat="server" Text='<%# Bind("BatchNo") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>


                           <asp:TemplateField HeaderText="Expiry Date">
                            <ItemTemplate>
                                <asp:Label ID="lblExpirDate" runat="server" Text='<%# Bind("ExDate") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>

                          <asp:TemplateField HeaderText="Dose" Visible="false">
                            <ItemTemplate>
                                <asp:Label ID="lblDose" runat="server" Text='<%# Bind("Dose") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>

                          <asp:TemplateField HeaderText="Duration"  Visible="false">
                            <ItemTemplate>
                                <asp:Label ID="lblDuration" runat="server" Text='<%# Bind("Duration") %>'></asp:Label>
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

