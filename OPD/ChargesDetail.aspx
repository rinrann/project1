<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ChargesDetail.aspx.cs" Inherits="OPD_ChargesDetail" MasterPageFile="~/MasterAll/MasterPageAll.master" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
  
<script language="javascript" type="text/javascript">
    function ShowDialog() {

        var rtvalue = window.open("PtientDetailspopup.aspx", "sss", "Width:800px; Height:550px; dialogLeft:250px;");
        //document.getElementById("ctl00_ContentPlaceHolder1_txtreg").value = rtvalue.NameValue;
        //document.getElementById("ctl00_ContentPlaceHolder1_txtAppo").value = rtvalue.ProfessionValue;
    }
    function Calling() {
        var date = new Date();
        $('.DatePicker').datepicker({ dateFormat: 'dd/mm/yy' });

        $("input[id$='txtdate']").datepicker({ dateFormat: 'dd/mm/yy' });

        $("input[id$='Button1']").click(function () {
            if ($("input[id$='txtRgnFee']").val() == '') {
                alert('Please Regn Fee !');
                $("input[id$='txtRgnFee']").focus();
                $("input[id$='txtRgnFee']").addClass('textboxerr');
                return false;
            }
            else {
                $("input[id$='txtRgnFee']").removeClass('textboxerr');
            }


            if ($("input[id$='txtDocFees']").val() == '') {
                alert('Please Enter Doctor Fees !');
                $("input[id$='txtDocFees']").focus();
                $("input[id$='txtDocFees']").addClass('textboxerr');
                return false;
            }
            else {
                $("input[id$='txtDocFees']").removeClass('textboxerr');
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
                <asp:TextBox ID="txtreg" CssClass="textbox-medium1"  runat="server" Enabled="False" ClientIDMode="Static"></asp:TextBox>  
    <asp:Button ID="Button3"  runat="server" Text="Search" Height="28px"  CssClass="submit-button"  OnClientClick="ShowDialog()"/> 
                <asp:Button ID="Button4" runat="server"   Text="fetch"  Height="28px"  
                    CssClass="submit-button" onclick="Button4_Click" />
                <div class="clear"></div>
            </div>
            <div class="form-sec-row">
                <label><strong>Appointment No :</strong></label>
                <asp:TextBox ID="txtappo" CssClass="textbox-medium1"  runat="server" 
                    Enabled="False"></asp:TextBox>  
                <div class="clear"></div>
            </div>

               <div class="form-sec-row">
                <label><strong>Patient's Name :</strong></label>
                 <asp:TextBox ID="txtname" runat="server" CssClass="textbox-medium1" 
                       Enabled="False" ></asp:TextBox>
                   <div class="clear">  </div>
            </div>



            <div class="formbox"> 
             <table border="0" cellpadding="2" cellspacing="2" width="100%">
        <tr><td colspan="4"><div class="pageheader">
         <asp:Label ID="Label2" runat="server">Charges</asp:Label>
     </div></td></tr>  
       <tr>
       <td style='' align="left" width="20%">                             
             <label class="lname"><strong> Regn Fee:<span style="color:red;">*</span></strong></label> </td>
        <td align="left" width="30%">
       <asp:TextBox ID="txtRgnFee" runat="server" Width="90%" style='text-align:right;' OnTextChanged="txtRgnFee_TextChanged"></asp:TextBox>
   </td>
       <td style='' align="left" width="20%">                             
           <asp:DropDownList ID="ddl1" runat="server" Width="100%"></asp:DropDownList> </td>
        <td align="left" width="30%">
       <asp:TextBox ID="TextBox1" runat="server" Width="90%" style='text-align:right;' OnTextChanged="TextBox1_TextChanged"></asp:TextBox>
   </td></tr><tr>
 <td align="left"><label class="lname"><strong>Doctor Fees:<span style="color:red;">*</span></strong></label> </td><td align="left">
       <asp:TextBox ID="txtDocFees" runat="server" Width="90%" style='text-align:right;' OnTextChanged="txtDocFees_TextChanged"></asp:TextBox>
   </td><td style='' align="left">                             
           <asp:DropDownList ID="ddl2" runat="server" Width="100%"></asp:DropDownList> </td>
        <td align="left">
       <asp:TextBox ID="TextBox2" runat="server" Width="90%" style='text-align:right;' OnTextChanged="TextBox2_TextChanged"></asp:TextBox>
   </td</tr>
    <tr><td align="left">                  
             <label class="lname"><strong>USG Charge:</strong></label></td><td align="left">
       <asp:TextBox ID="txtUsgChrg" runat="server" Width="90%" style='text-align:right;' OnTextChanged="txtUsgChrg_TextChanged"></asp:TextBox>
   </td><td style='' align="left">                             
           <asp:DropDownList ID="ddl3" runat="server" Width="100%"></asp:DropDownList> </td>
        <td align="left">
       <asp:TextBox ID="TextBox3" runat="server" Width="90%" style='text-align:right;' OnTextChanged="TextBox3_TextChanged"></asp:TextBox>
   </td</tr><tr>
       
                     <td align="left">
                         <label class="lname">
                         <strong>IUI Charge:</strong></label></td>
                     <td align="left">
                         <asp:TextBox ID="txtIuiChrg" runat="server" Width="90%" style='text-align:right;' OnTextChanged="txtIuiChrg_TextChanged"></asp:TextBox>
                     </td><td style='' align="left">                             
           <asp:DropDownList ID="ddl4" runat="server" Width="100%"></asp:DropDownList> </td>
        <td align="left">
       <asp:TextBox ID="TextBox4" runat="server" Width="90%" style='text-align:right;' OnTextChanged="TextBox4_TextChanged"></asp:TextBox>
   </td</tr>
                 <tr>
                     <td align="left">
                         <label class="lname">
                         <strong>Investigation Charge:</strong></label></td>
                     <td align="left">
                         <asp:TextBox ID="txtInvChrg" runat="server" Width="90%" style='text-align:right;' OnTextChanged="txtInvChrg_TextChanged"></asp:TextBox>
                     </td><td style='' align="left">                             
           <asp:DropDownList ID="ddl5" runat="server" Width="100%"></asp:DropDownList> </td>
        <td align="left">
       <asp:TextBox ID="TextBox5" runat="server" Width="90%" style='text-align:right;' OnTextChanged="TextBox5_TextChanged"></asp:TextBox>
   </td
                 </tr>
                 <tr>
                     <td align="left">
                         <label class="lname">
                         <strong>Operation Charge:</strong></label></td>
                     <td align="left">
                         <asp:TextBox ID="txtOptChrg" runat="server" Width="90%" style='text-align:right;' OnTextChanged="txtOptChrg_TextChanged"></asp:TextBox>
                     </td><td style='' align="left">                             
           <asp:DropDownList ID="ddl6" runat="server" Width="100%"></asp:DropDownList> </td>
        <td align="left">
       <asp:TextBox ID="TextBox6" runat="server" Width="90%" style='text-align:right;' OnTextChanged="TextBox6_TextChanged"></asp:TextBox>
   </td
                 </tr>
                 <tr>
                     <td align="left">
                         <label class="lname">
                         <strong>Medicine Charge:</strong></label></td>
                     <td align="left">
                         <asp:TextBox ID="txtMedChrg" runat="server" Width="90%" style='text-align:right;' OnTextChanged="txtMedChrg_TextChanged"></asp:TextBox>
                     </td><td style='' align="center">                             
           <label class="lname"><strong>Total Charge:</strong></label> </td>
        <td align="left">
       <asp:TextBox ID="txtTotal" runat="server" Width="90%" style='text-align:right;'></asp:TextBox>
   </td></tr>
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

              

            <div class="formbox"> 
             <table border="0" cellpadding="1" cellspacing="1" width="100%">
        <tr><td><div class="pageheader">
         <asp:Label ID="Label3" runat="server">Payment</asp:Label>
            <div class="error">
                <strong><asp:Label ID="lblpayError" runat="server" Font-Bold="True"></asp:Label></strong>
                <div class="clear"></div>
            </div>
     </div></td></tr>
                 <tr><td>
                     <div class="form-sec-row">
                <label><strong>Total Bill Amount :</strong></label>  
                 <asp:TextBox ID="TextBox7" runat="server" CssClass="textbox-medium1" Enabled="False" 
                   ></asp:TextBox><asp:HiddenField ID="HdnReceipNo" runat="server" />
                <div class="clear"></div>
            </div>
            <div class="form-sec-row">
                <label><strong>Total Amount Paid :</strong></label>  
                 <asp:TextBox ID="txtpaid" runat="server" CssClass="textbox-medium1" Enabled="False" 
                   ></asp:TextBox>
                <div class="clear"></div>
            </div>
            <div class="form-sec-row">
                <label><strong><asp:Label ID="Label4" runat="server" Text="Total Due :"></asp:Label></strong></label>
                  <asp:TextBox ID="txtdue" runat="server" CssClass="textbox-medium1" 
                    MaxLength="14" Enabled="False"
                   ></asp:TextBox>
                <div class="clear"></div>
            </div>
                     <div class="form-sec-row">
                <label><strong>Payment Date :<span style="color:red;">*</span></strong></label>
                 <asp:TextBox ID="txtdate" runat="server" CssClass="textbox-medium1"></asp:TextBox>
                           <cc1:MaskedEditExtender ID="MaskedEditExtender1" runat="server" TargetControlID="txtdate"
                                            Mask="99/99/9999" MessageValidatorTip="true" MaskType="Date" DisplayMoney="Left"
                                            AcceptNegative="Left" ErrorTooltipEnabled="True" CultureName="en-GB" />
                <div class="clear"></div>
            </div>

            <div class="form-sec-row">
                <label><strong>Current Payment :<span style="color:red;">*</span></strong></label>
                 <asp:TextBox ID="txtamt" runat="server" CssClass="textbox-medium1"></asp:TextBox>
                <div class="clear"></div>
            </div>
                     
              <div class="form-sec-row">
                <label><strong>Payment Mode :</strong></label>
               <asp:DropDownList ID="DropDownList1" runat="server" CssClass="textbox-medium1"  >
                     </asp:DropDownList>
                <div class="clear"></div>
            </div>  
              <div class="form-sec-row">
                <label><strong>
                    <asp:Label ID="lblbk" runat="server" Text="Book :"></asp:Label><span style="color:red;">*</span></strong></label>
               <asp:DropDownList ID="ddlBook" runat="server" CssClass="textbox-medium1"  >
                     </asp:DropDownList>
                <div class="clear"></div>
            </div>
                     <div class="form-sec-row">
                <label><strong>&nbsp;</strong></label>
                <asp:Button ID="Button5" runat="server" OnClick="Button5_Click"   Height="28px" Text="Submit" CssClass="submit-button" />
                <asp:Button ID="Button6" runat="server" Text="Cancel"   Height="28px" OnClick="Button2_Click" CssClass="submit-button" />
                <asp:Button ID="passJV" runat="server" Text="Pass Journal" OnClick="passJV_Click" Visible="false"  CssClass="submit-button" />
                <asp:Button ID="passPayment" runat="server" Text="Pass Accounts Entry" Visible="true" OnClick="passPayment_Click" CssClass="submit-button"/>
                        
                <div class="clear"></div>
            </div>
                     </td></tr>  
                </table>
                </div>
     </div>
    
       
  
        </div>
    </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>


