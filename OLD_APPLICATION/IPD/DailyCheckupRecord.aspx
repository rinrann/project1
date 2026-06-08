<%@ Page Title="" Language="C#" MasterPageFile="~/MasterAll/MasterPageAll.master" AutoEventWireup="true" CodeFile="DailyCheckupRecord.aspx.cs" Inherits="IPD_DailyCheckupRecord" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">

 <script type="text/javascript" language="javascript">
     function ShowDialog() {
         var rtvalue = window.open("IpdpatientPopup.aspx", "sss", "Width:800px; Height:550px; dialogLeft:250px;");
         document.getElementById("ctl00_ContentPlaceHolder1_TextBox2").value = rtvalue.NameValue;

     }
      

     function isNumberKey(evt) {
         var charCode = (evt.which) ? evt.which : event.keyCode;
         if (charCode != 46 && charCode > 31
            && (charCode < 48 || charCode > 57))
             return false;

         return true;
     }

     function ShowDialog2() {
         var rtvalue = window.open("ComplainPopup.aspx", "sss", "Width:800px; Height:550px; dialogLeft:250px;");
         document.getElementById("ctl00_ContentPlaceHolder1_HiddenField3").value = rtvalue.NameValue;
         document.getElementById("ctl00_ContentPlaceHolder1_TextBox22").value = rtvalue.ProfessionValue;

     }

     function CheckSpo2() {
         var valu = document.getElementById("ctl00_ContentPlaceHolder1_TextBox13").value;
         if (valu > 100) {
             alert('SPO2 can not more than 100');
             $("input[id$='TextBox13']").focus();
         }
     }


     function CheckDoppler() {
         var valu = document.getElementById("ctl00_ContentPlaceHolder1_TextBox16").value;
         if (valu > 300) {
             alert('Doppler can not more than 300');
             $("input[id$='TextBox16']").focus();
         }
     }

     function Calling() {

         var date = new Date();
         $("input[id$='txtdate']").datepicker({ dateFormat: 'dd/mm/yy' });
         $('.DatepickerReCall').datepicker({ dateFormat: 'dd/mm/yy' });

         $('.time').timepicker({
             showPeriod: true,
             showLeadingZero: true
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

         $("input[id$='Button9']").click(function () {

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
                 alert('Please Enter No of Visit Properly!');
                 $("input[id$='TextBox1']").focus();
                 $("input[id$='TextBox1']").addClass('textboxerr');
                 return false;
             }
             else {
                 $("input[id$='TextBox1']").removeClass('textboxerr');
             }
         });

         $('.NumberOnly').keydown(function (event) {
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
                <asp:Label ID="Label1" runat="server">BHT Record</asp:Label>
            </div>
        <asp:HiddenField ID="HiddenField3" runat="server" />   
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
                    <asp:HiddenField ID="HiddenField1" runat="server" Value="0" /><asp:HiddenField ID="HiddenField2" runat="server" Value="0" />
                 <%--   <div class="form-sec-row">
                        <label>
                        <strong>Bed No :</strong></label>
                        <asp:TextBox ID="txtBedNo" runat="server" CssClass="textbox-medium1" 
                            Enabled="False" ></asp:TextBox>
                        <div class="clear">
                        </div>
                    </div>--%>
                      <div class="form-sec-row">
                        <label>
                        <strong>
                       Registration No :</strong></label>
                          <asp:TextBox ID="TextBox2" runat="server" CssClass="textbox-medium1" 
                              Enabled="true"></asp:TextBox>
                                <asp:Button ID="Button3" runat="server" Text="Quick Search"  Height="28px"  CssClass="submit-buttonCheck" OnClientClick="ShowDialog()"/>
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
                                Enabled="False"></asp:TextBox>
                        <div class="clear">
                        </div>
                    </div>
 
                   
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

       <td align="center" style='width:100px;'>
    
             <label ><strong> P/A </strong></label>
                       
</td>

       <td align="center" style='width:100px;'>
    
             <label ><strong> P/V </strong></label>
                       
</td>

       <td align="center" style='width:100px;'>
    
             <label ><strong> Chest </strong></label>
                       
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
                   
          <asp:TextBox ID="TextBox10" runat="server"   Width="100px"></asp:TextBox>
                          
</td>
 <td align="center" style="margin-left: 40px">
                   
                       <asp:TextBox ID="TextBox11" runat="server"  Width="80px" MaxLength="3"></asp:TextBox>
           
            
</td>

 <td align="center" >
                   
                       <asp:TextBox ID="TextBox1" runat="server" Width="40px"></asp:TextBox>
           
            
</td>
 <td align="center" >
                   
                       <asp:TextBox ID="TextBox6" runat="server" Width="40px"></asp:TextBox>
           
            
</td>

 <td align="center" >
                   
                       <asp:TextBox ID="TextBox7" runat="server" MaxLength="4"  Width="40px" ></asp:TextBox>
           
            
</td>
 <td align="center">
                    
            <%--<asp:TextBox ID="TextBox12" runat="server"  MaxLength="5"  Width="60px" onkeypress="return isNumberKey(event)" ></asp:TextBox>--%>
        <asp:TextBox ID="TextBox12" runat="server"  MaxLength="5"  Width="60px"  ></asp:TextBox>
            
            
</td>

 <td align="center">
                    
             <asp:TextBox ID="TextBox13" runat="server" MaxLength="3"  onblur="CheckSpo2();" Width="80px"></asp:TextBox>
            
            
</td>

 <td align="center">
                    
             <asp:TextBox ID="TextBox14" runat="server"    MaxLength="4"  Width="40px"></asp:TextBox>
            
            
</td>

 <td align="center">
                    
             <asp:TextBox ID="TextBox15" runat="server"      MaxLength="4" Width="70px"></asp:TextBox>
            
            
</td>
 <td align="center">
                    
             <asp:TextBox ID="TextBox16" runat="server" MaxLength="4" onblur="CheckDoppler();"    Width="70px"></asp:TextBox>
            
            
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
</td>
 </tr>
 </table>
                        </div>

                                     </div>
                </asp:View>
                 <asp:View ID="View2" runat="server">
            
               <table border="1" cellpadding="0" cellspacing="0" width="100%">
                            <tr>
                                <td colspan="5">
               
                                         <div class="listing"  style='width:100%; height:auto; overflow:auto;'>
         <asp:GridView id="GridView6"  CssClass="grid" PagerStyle-CssClass="pgr" DataKeyNames ="RowID" 
                                            runat="server" AutoGenerateColumns="False"   AllowPaging="True" PageSize ="70"              
              SelectedRowStyle-BackColor="GreenYellow"  Width="100%"
                                                onrowdeleting="GridView6_RowDeleting" OnRowDataBound="GridView6_RowDataBound" 
                                                 onrowcommand="GridView6_RowCommand"   >
                <RowStyle HorizontalAlign="Center" />
                <Columns> 

                     <asp:TemplateField HeaderText="ID" Visible ="false">
                     <ItemTemplate>
                     <asp:Label ID="lblid" runat="server" Text='<%# Bind("RowId") %>'></asp:Label>
                     </ItemTemplate>
                     </asp:TemplateField>


                    <asp:TemplateField HeaderText="Date">
                    <ItemTemplate>
                    <asp:Label ID="lbldate" runat="server" Text='<%# Bind("Date") %>'></asp:Label>
                    </ItemTemplate> 
                    </asp:TemplateField>

                         <asp:TemplateField HeaderText="Time">
                    <ItemTemplate>
                    <asp:Label ID="lblTime" runat="server" Text='<%# Bind("Time") %>'></asp:Label>
                    </ItemTemplate> 
                    </asp:TemplateField>

                        <asp:TemplateField HeaderText="Complaint">
                    <ItemTemplate>
                    <asp:Label ID="lblComplainName" runat="server" Text='<%# Bind("ComplainName") %>'></asp:Label>
                    </ItemTemplate> 
                    </asp:TemplateField>

                      <asp:TemplateField HeaderText="BP">
                    <ItemTemplate>
                    <asp:Label ID="lblBP" runat="server" Text='<%# Bind("BP") %>'></asp:Label>
                    </ItemTemplate> 
                    </asp:TemplateField>

                      <asp:TemplateField HeaderText="Pulse">
                    <ItemTemplate>
                    <asp:Label ID="lblPulse" runat="server" Text='<%# Bind("Pulse") %>'></asp:Label>
                    </ItemTemplate> 
                    </asp:TemplateField>

                      <asp:TemplateField HeaderText="P/A">
                    <ItemTemplate>
                    <asp:Label ID="lblPA" runat="server" Text='<%# Bind("PA") %>'></asp:Label>
                    </ItemTemplate> 
                    </asp:TemplateField>

                        <asp:TemplateField HeaderText="P/V">
                    <ItemTemplate>
                    <asp:Label ID="lblPV" runat="server" Text='<%# Bind("PV") %>'></asp:Label>
                    </ItemTemplate> 
                    </asp:TemplateField>

                        <asp:TemplateField HeaderText="Chest">
                    <ItemTemplate>
                    <asp:Label ID="lblChest" runat="server" Text='<%# Bind("Chest") %>'></asp:Label>
                    </ItemTemplate> 
                    </asp:TemplateField>

                        <asp:TemplateField HeaderText="Temp">
                    <ItemTemplate>
                    <asp:Label ID="lblTemp" runat="server" Text='<%# Bind("Temp") %>'></asp:Label>
                    </ItemTemplate> 
                    </asp:TemplateField>

                      <asp:TemplateField HeaderText="SPO2">
                    <ItemTemplate>
                    <asp:Label ID="lblSPO2" runat="server" Text='<%# Bind("SPO2") %>'></asp:Label>
                    </ItemTemplate> 
                    </asp:TemplateField>

                      <asp:TemplateField HeaderText="Urine">
                    <ItemTemplate>
                    <asp:Label ID="lblUrin" runat="server" Text='<%# Bind("Urin") %>'></asp:Label>
                    </ItemTemplate> 
                    </asp:TemplateField>

                    
                      <asp:TemplateField HeaderText="Suction">
                    <ItemTemplate>
                    <asp:Label ID="lblSunction" runat="server" Text='<%# Bind("Sunction") %>'></asp:Label>
                    </ItemTemplate> 
                    </asp:TemplateField>

                   
                   
                      <asp:TemplateField HeaderText="Doppler">
                    <ItemTemplate>
                    <asp:Label ID="lblDoppler" runat="server" Text='<%# Bind("Doppler") %>'></asp:Label>
                    </ItemTemplate> 
                    </asp:TemplateField>
                    
                      <asp:TemplateField HeaderText="Bleeding">
                    <ItemTemplate>
                    <asp:Label ID="lblBleeding" runat="server" Text='<%# Bind("Bleeding") %>'></asp:Label>
                    </ItemTemplate> 
                    </asp:TemplateField>

                          <asp:TemplateField HeaderText="Others">
                    <ItemTemplate>
                    <asp:Label ID="lblOthers" runat="server" Text='<%# Bind("Others") %>'></asp:Label>
                    </ItemTemplate> 
                    </asp:TemplateField>
                     
     
                <asp:CommandField SelectText="Edit" ShowSelectButton="True"  ItemStyle-Width="50px"  ControlStyle-CssClass="temp" HeaderText="Edit" SelectImageUrl="~/Images/edit.png" ButtonType="Image">
                            <ControlStyle CssClass="temp"></ControlStyle>
                        </asp:CommandField>

                      <asp:CommandField ShowDeleteButton="True" ControlStyle-CssClass="temp"  ItemStyle-Width="50px"  HeaderText="Delete" DeleteImageUrl="~/Images/delete.png" ButtonType="Image">
                        <ControlStyle CssClass="temp"></ControlStyle>
                    </asp:CommandField>

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
            </asp:MultiView>
            </div>
    </ContentTemplate>
    </asp:UpdatePanel>

</asp:Content>

