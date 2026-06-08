<%@ Page Title="" Language="C#" MasterPageFile="~/MasterAll/MasterPageAll.master" AutoEventWireup="true" CodeFile="ChargeSetails.aspx.cs" Inherits="DayCare_ChargeSetails" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
  
<script language="javascript" type="text/javascript">
    function ShowDialog() {

        var rtvalue = window.open("RegistrationPopup.aspx", "sss", "Width:800px; Height:550px; dialogLeft:250px;");
        document.getElementById("ctl00_ContentPlaceHolder1_txtreg").value = rtvalue.NameValue;
    }
    function Calling() {
        var date = new Date();
        $('.DatePicker').datepicker({ dateFormat: 'dd/mm/yy' });

        
        $("input[id$='Button1']").click(function () {
            if ($("input[id$='TextBox1']").val() == '') {
                alert('Please Enter Date  !');
                $("input[id$='TextBox1']").focus();
                $("input[id$='TextBox1']").addClass('textboxerr');
                return false;
            }
            else {
                $("input[id$='TextBox1']").removeClass('textboxerr');
            }


            if ($("input[id$='TextBox2']").val() == '') {
                alert('Please Enter Dialysis Charge  !');
                $("input[id$='TextBox2']").focus();
                $("input[id$='TextBox2']").addClass('textboxerr');
                return false;
            }
            else {
                $("input[id$='TextBox2']").removeClass('textboxerr');
            }



        });


        $("input[id$='TextBox2']").keydown(function (event) {
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


        $("input[id$='TextBox3']").keydown(function (event) {
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


 <%--
  <asp:UpdateProgress ID="UpdateProgress1" runat="server">
    <ProgressTemplate>
    
       <div id="aaa" class="progressBackgroundFilter">          </div>
        <div id="bbb" class="processMessage"><img alt="Loading" src="../images/pwait.gif"/>
      
              </div>
    </ProgressTemplate>
    </asp:UpdateProgress>--%>

<asp:UpdatePanel ID="UpdatePanel1" runat="server">
    <ContentTemplate>
    <div class="pageheader">
         <asp:Label ID="Label1" runat="server">Charge Details</asp:Label>
     </div>

      

     <div class="formbox">

   

        <div class="form-sec">
            <div class="error">
                <strong><asp:Label ID="lblError" runat="server" Font-Bold="True"></asp:Label></strong>
                <div class="clear"></div>
            </div>
     <asp:HiddenField ID="HiddenField1" runat="server" /><asp:HiddenField ID="HiddenField2" runat="server" />

       <div class="form-sec-row">
                <label><strong>Registration No :</strong></label>
                <asp:TextBox ID="txtreg" CssClass="textbox-medium1"  runat="server" 
                    Enabled="False"></asp:TextBox>  
    <asp:Button ID="Button3"  runat="server" Text="Search" Height="28px"  CssClass="submit-button"  OnClientClick="ShowDialog()"/> 
                <asp:Button ID="Button4" runat="server"   Text="fetch"  Height="28px"  
                    CssClass="submit-button" onclick="Button4_Click" />
                <div class="clear"></div>
            </div>

               <div class="form-sec-row">
                <label><strong>Patient's Name :</strong></label>
                 <asp:TextBox ID="txtname" runat="server" CssClass="textbox-medium1" 
                       Enabled="False" ></asp:TextBox>
                   <div class="clear">  </div>
            </div>


              <div class="form-sec-row">
                <label><strong>Bed No :</strong></label>
                 <asp:TextBox ID="txtbedno" runat="server" CssClass="textbox-medium1" 
                       Enabled="False" ></asp:TextBox>
                   <div class="clear">  </div>
            </div>

            
               <div class="form-sec-row">
               <div style='float:left;'>
                    <label><strong>Previous Dialysis Date :</strong></label></div>
                   <div style='float:left; padding-left:5px;'>
                 <asp:TextBox ID="TextBox10" runat="server" CssClass="textbox-medium1" Width="190px" Enabled="false"  ></asp:TextBox></div>
                 <div style='float:left;padding-left:5px; width:45px;'>
       <label> <strong>Days :
          </strong></label></div>
           <div style='float:left;padding-left:1px;'>
         <asp:TextBox ID="TextBox11" runat="server" CssClass="textbox-medium1" Width="52px" Enabled="false"  ></asp:TextBox></div>
                <div class="clear"></div>
                </div>

             
             <table border="1" cellpadding="0" cellspacing="0" width="100%">
          
       <tr style='background-color:#FF9300;'>
          
                <td align="center">
                             
             <label class="lname"><strong> Date</strong></label>
                        
</td>

                <td align="center">
                             
             <label class="lname"><strong> Dialysis Charge</strong></label>
                        
</td>
 <td align="center">
                   
             <label class="lname"><strong>Service Charge</strong></label> 
             
            
</td>
 <td align="center">
                  
             <label class="lname"><strong>Med Charge</strong></label> 
           
            
</td>

 <td align="center">
                 
             <label class="lname"><strong>Lab Charge</strong></label> 
            
            
</td>
 <td align="center">                 
             <label class="lname"><strong>Doc. Charge</strong></label>           
</td>
 <td align="center">                 
             <label class="lname"><strong>Dispo. Charge</strong></label>           
</td>
 <td align="center">                 
             <label class="lname"><strong>Other Charge</strong></label>           
</td>
 <td align="center">                 
             <label class="lname"><strong>Prev. Due</strong></label>           
</td>
   </tr>
  <tr>
  
  <td align="center">
       <asp:TextBox ID="TextBox1" runat="server" CssClass="DatePicker" Width="100px"></asp:TextBox>
   </td>
  <td align="center">
       <asp:TextBox ID="TextBox2" runat="server" Width="100px"></asp:TextBox>
   </td>
 <td align="center">
   <asp:TextBox ID="TextBox3" runat="server"  Width="100px"></asp:TextBox>
   </td>
  <td align="center">
         <asp:TextBox ID="TextBox4" runat="server"  Enabled="false"  Width="100px"></asp:TextBox>
   </td>
 <td align="center">
          <asp:TextBox ID="TextBox5" runat="server"  Enabled="false"  Width="100px"></asp:TextBox>
   </td>

    <td align="center">
          <asp:TextBox ID="TextBox6" runat="server"   Width="100px"></asp:TextBox>
   </td>

    <td align="center">
          <asp:TextBox ID="TextBox7" runat="server"   Width="100px"></asp:TextBox>
   </td>

    <td align="center">
          <asp:TextBox ID="TextBox8" runat="server"   Width="100px"></asp:TextBox>
   </td>

   <td align="center">
          <asp:TextBox ID="TextBox9" runat="server"   Enabled="false"  Width="100px"></asp:TextBox>
   </td>
 </tr>


     </table>

            <div class="form-sec-row">
                <label><strong>&nbsp;</strong></label>
                <asp:Button ID="Button1" runat="server"  Text="Submit" Height="28px" 
                    CssClass="submit-button" onclick="Button1_Click" />
                <asp:Button ID="Button2" runat="server" Text="Cancel"   Height="28px" 
                    CssClass="submit-button" onclick="Button2_Click"  />
                <div class="clear"></div>
            </div>  
     </div>
    
       <div class="listing"   style='overflow:auto;'>
         <asp:GridView id="GridView1"  CssClass="grid"
                 PagerStyle-CssClass="pgr" DataKeyNames ="RowId" runat="server" 
                 AutoGenerateColumns="False" AllowPaging="True" PageSize ="100"  Width="100%" 
                    OnPageIndexChanging="GridView1_PageIndexChanging"   SelectedRowStyle-BackColor="GreenYellow" 
                 OnRowCommand="GridView1_RowCommand" OnRowDeleting="GridView1_RowDeleting">
                <RowStyle HorizontalAlign="Center" />
                <Columns>
                    <asp:TemplateField HeaderText="Row Id" Visible="false">
                        <ItemTemplate>
                            <asp:Label ID="lblID" runat="server" Text='<%# Bind("RowId") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>


                            <asp:TemplateField HeaderText="Registration No">
                       
                        <ItemTemplate>
                            <asp:Label ID="PatientReg" runat="server" Text='<%# Bind("PatientReg") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>

                    
                            <asp:TemplateField HeaderText="Date">
                       
                        <ItemTemplate>
                            <asp:Label ID="Date" runat="server" Text='<%# Bind("Date1") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>


                    <asp:TemplateField HeaderText=" Dialysis Charge " >
                       
                        <ItemTemplate>
                            <asp:Label ID="DialysisCharge" runat="server" Text='<%# Bind("DialysisCharge") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>

                      <asp:TemplateField HeaderText="Service Chargea">
                       
                        <ItemTemplate>
                            <asp:Label ID="ServiceCharge" runat="server" Text='<%# Bind("ServiceCharge") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>

                     <asp:TemplateField HeaderText="Medicine Charge ">
                       
                        <ItemTemplate>
                            <asp:Label ID="Medicine" runat="server" Text='<%# Bind("Medicine") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>

                         <asp:TemplateField HeaderText="Pathology Charge">
                       
                        <ItemTemplate>
                            <asp:Label ID="RequisitionCharge" runat="server" Text='<%# Bind("RequisitionCharge") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                       <asp:TemplateField HeaderText="Doctor Fees">
                       
                        <ItemTemplate>
                            <asp:Label ID="DoctorFees" runat="server" Text='<%# Bind("DoctorFees") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                       <asp:TemplateField HeaderText="Dispsable Charge">
                       
                        <ItemTemplate>
                            <asp:Label ID="DispsableCharge" runat="server" Text='<%# Bind("DispsableCharge") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>

                       <asp:TemplateField HeaderText="Prev. Due">
                       
                        <ItemTemplate>
                            <asp:Label ID="lblPreviousDue" runat="server" Text='<%# Bind("PreviousDue") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>

                       <asp:TemplateField HeaderText="Other Charge">
                       
                        <ItemTemplate>
                            <asp:Label ID="OtherCharge" runat="server" Text='<%# Bind("OtherCharge") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
               
                    <asp:CommandField SelectText="Edit" ShowSelectButton="True" ControlStyle-CssClass="temp" HeaderText="Edit" SelectImageUrl="~/Images/edit.png" ButtonType="Image">
                        <ControlStyle CssClass="temp"></ControlStyle>
                    </asp:CommandField>
                    <asp:CommandField ShowDeleteButton="True" ControlStyle-CssClass="temp" HeaderText="Delete" DeleteImageUrl="~/Images/delete.png" ButtonType="Image" Visible="false">
                        <ControlStyle CssClass="temp"></ControlStyle>
                    </asp:CommandField>
                </Columns>
                <PagerStyle CssClass="pgr" />
                <EditRowStyle BackColor="#CCFF33" />
                <AlternatingRowStyle BackColor="#FFDB91" />
        </asp:GridView> 
        </div>
  
        </div>
    </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>

