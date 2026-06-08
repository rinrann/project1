<%@ Page Title="" Language="C#" MasterPageFile="~/MasterAll/MasterPageAll.master" AutoEventWireup="true" CodeFile="AnyTimePayment.aspx.cs" Inherits="Account_AnyTimePayment" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <style>
.BigWidth
{
    width:450px; 
}
</style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <script type="text/javascript" language="javascript">

       function printDiv(divID) {
           var divElements = document.getElementById(divID).innerHTML; 
           var oldPage = document.body.innerHTML;
           document.body.innerHTML = "<html><head><title></title></head><body>" + divElements + "</body>";
           window.print();
           document.body.innerHTML = oldPage;
       }


       function autoCompleteEx_ItemSelected(sender, args) {
           var regname = args.get_value().split('-');
           document.getElementById("ctl00_ContentPlaceHolder1_txtreg").value = regname[0];
           document.getElementById("ctl00_ContentPlaceHolder1_txtname").value = regname[1];
       }

    function ShowDialog() {

        var rtvalue = window.open("RegistrationPopup.aspx", "sss", "Width:800px; Height:550px; dialogLeft:250px;");
        //document.getElementById("ctl00_ContentPlaceHolder1_txtreg").value = rtvalue.NameValue;

    }

    function Calling() {
    
        var date = new Date();
        $("input[id$='txtdate']").datepicker({ dateFormat: 'dd/mm/yy' });

            $("input[id$='txtamt']").keyup(function () {
            var sum = Number($("input[id$='txtdue']").val()) - (Number($("input[id$='txtamt']").val()) + Number($("input[id$='txtdiscount']").val()));
            $("input[id$='txtdueamt']").val(sum);
            if (parseFloat(sum) == 0) { $("input[id$='CheckBox1']").prop('checked', 'checked'); /*$(".book").show();*/}
            else { $("input[id$='CheckBox1']").prop('checked', ''); /*$(".book").hide();*/ }
        });

        $("input[id$='txtdiscount']").keyup(function () {
            var sum = Number($("input[id$='txtdue']").val()) - (Number($("input[id$='txtamt']").val()) + Number($("input[id$='txtdiscount']").val()));
            $("input[id$='txtdueamt']").val(sum);
            if (parseFloat(sum) == 0) { $("input[id$='CheckBox1']").prop('checked', 'checked'); /*$(".book").show();*/ }
            else { $("input[id$='CheckBox1']").prop('checked', ''); /*$(".book").hide();*/ }
        });


        $("input[id$='Button1']").click(function () {

            if ($("input[id$='txtdate']").val() == '') {
                alert('Please Enter Transaction Date !');
                $("input[id$='txtdate']").focus();
                $("input[id$='txtdate']").addClass('textboxerr');
                return false;
            }
            else {
                $("input[id$='txtdate']").removeClass('textboxerr');
            }

            if ($("#Label2").val() == 'Amount to Refund :') {
                if ($("input[id$='txtamt']").val() == '') {
                    alert('Please Enter Payment Amount !');
                    $("input[id$='txtamt']").focus();
                    $("input[id$='txtamt']").addClass('textboxerr');
                    return false;
                }
                else {
                    $("input[id$='txtamt']").removeClass('textboxerr');
                }
            }


            if ($("input[id$='txtreason']").val() == '0') {
                alert('Please Enter Reason of Payment !');
                $("input[id$='txtreason']").focus();
                $("input[id$='txtreason']").addClass('textboxerr');
                return false;
            }
            else {
                $("input[id$='txtreason']").removeClass('textboxerr');
            }

            /*if ($("input[id$='HiddenField1']").val() == '1' && $("input[id$='ddlbook']").val() == '0') {
                alert('Please Select Book !');
                $("input[id$='ddlbook']").focus();
                $("input[id$='ddlbook']").addClass('textboxerr');
                return false;
            }
            else {
                $("input[id$='ddlbook']").removeClass('textboxerr');
            }*/
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
    </asp:UpdateProgress>
--%>

<asp:UpdatePanel ID="UpdatePanel1" runat="server">
    <ContentTemplate>
    <div class="pageheader">
         <asp:Label ID="Label1" runat="server">Any Time Payment</asp:Label>
     </div>
  

        
            <div class="error">
                <strong><asp:Label ID="lblError" runat="server" Font-Bold="True"></asp:Label>
                </strong>
                <div class="clear"></div>
            </div>
            <asp:HiddenField ID="TextBox4" runat="server" />

            <div class="formbox">

                       <div class="form-sec-row">
                <label style='color:Blue'><strong>Patient Details :</strong></label>
                <div class="clear"></div>
            </div>

			<div class="form-sec-row">
                <label><strong>Registration No :<span style="color:red;">*</span></strong></label>
               <asp:TextBox ID="txtreg" runat="server" CssClass="textbox-medium1" 
                    Enabled="False" EnableTheming="True" 
                   ></asp:TextBox> <asp:Button ID="Button4" runat="server"  Height="28px"
                    CssClass="submit-search" Text="Quick Search" OnClientClick="ShowDialog()" 
                    />
                   <asp:Button ID="Button5" runat="server" CssClass="submit-button"    Height="28px"
                    Text="Fetch" onclick="Button5_Click" />
                <div class="clear">  </div>
            </div>
           
            <div class="form-sec-row">
                <label><strong>Patient Name :</strong></label>
                <asp:TextBox ID="txtname" runat="server" CssClass="textbox-medium1" 
                   ></asp:TextBox>

                     <cc1:AutoCompleteExtender ServiceMethod="SearchCustomers"  CompletionListItemCssClass="BigWidth"  OnClientItemSelected="autoCompleteEx_ItemSelected"     MinimumPrefixLength="1"    CompletionInterval="100" EnableCaching="false" 
                   CompletionSetCount="10" TargetControlID="txtname"  ID="AutoCompleteExtender1" runat="server" FirstRowSelected = "false" ></cc1:AutoCompleteExtender>
             
                <div class="clear"></div>
            </div>

                <div class="form-sec-row">
                <label><strong>C/O :</strong></label>  
                 <asp:TextBox ID="TextBox1" runat="server" CssClass="textbox-medium1" Enabled="False"></asp:TextBox>
                <div class="clear"></div>
            </div>

                <div class="form-sec-row">
                <label><strong>Address :</strong></label>  
                 <asp:TextBox ID="TextBox3" runat="server" CssClass="textbox-medium1" Enabled="False" 
                   ></asp:TextBox>
                <div class="clear"></div>
            </div>
            </div>

               <div class="formbox">

                          <div class="form-sec-row">
                <label style='color:Blue'><strong>Bill Amount Details :</strong></label>
                <div class="clear"></div>
            </div>

            <div class="form-sec-row">
                <label><strong>Total Bill Amount :</strong></label>  
                 <asp:TextBox ID="TextBox2" runat="server" CssClass="textbox-medium1" Enabled="False" 
                   ></asp:TextBox>
                <div class="clear"></div>
            </div>
            <div class="form-sec-row">
                <label><strong>Total Amount Paid :</strong></label>  
                 <asp:TextBox ID="txtpaid" runat="server" CssClass="textbox-medium1" Enabled="False" 
                   ></asp:TextBox>
                <div class="clear"></div>
            </div>
            <div class="form-sec-row">
                <label><strong><asp:Label ID="Label2" runat="server" Text="Total Due :"></asp:Label></strong></label>
                  <asp:TextBox ID="txtdue" runat="server" CssClass="textbox-medium1" 
                    MaxLength="14" Enabled="False"
                   ></asp:TextBox>
                <div class="clear"></div>
            </div>
            </div>

               <div class="formbox">
            
                       <div class="form-sec-row">
                <label style='color:Blue'><strong>Payment Sattlement :</strong></label>
                <div class="clear"></div>
            </div>

                        <div class="form-sec-row">
                <label><strong>Payment Date :<span style="color:red;">*</span></strong></label>
                 <asp:TextBox ID="txtdate" runat="server" CssClass="textbox-medium1"></asp:TextBox>
                <div class="clear"></div>
            </div>

            <div class="form-sec-row">
                <label><strong>Current Payment :<span style="color:red;">*</span></strong></label>
                 <asp:TextBox ID="txtamt" runat="server" CssClass="textbox-medium1"></asp:TextBox>
                <div class="clear"></div>
            </div>

              <div class="form-sec-row">
                <label><strong>Discount Amount :</strong></label>
                 <asp:TextBox ID="txtdiscount" runat="server" CssClass="textbox-medium1"></asp:TextBox>
                <div class="clear"></div>
            </div>

              <div class="form-sec-row">
                <label><strong>Payment Mode :</strong></label>
               <asp:DropDownList ID="DropDownList1" runat="server" CssClass="textbox-medium1"  >
                     </asp:DropDownList>
                <div class="clear"></div>
            </div>
           

               <div class="form-sec-row">
                <label><strong>Reason :<span style="color:red;">*</span></strong></label>
                <%-- <asp:TextBox ID="txtreason" runat="server" TextMode="MultiLine" Height="40px" CssClass="textbox-medium1"></asp:TextBox>--%>
                   <asp:DropDownList ID="txtreason" runat="server" CssClass="textbox-medium1"  OnSelectedIndexChanged="textreason_change" AutoPostBack="true">
                     </asp:DropDownList>
                   <asp:TextBox ID="TextBox5" runat="server" TextMode="MultiLine" Height="40px" CssClass="textbox-medium1" Visible="false"></asp:TextBox>
                <div class="clear"></div>
            </div>

              <div class="form-sec-row">
                <label><strong><asp:Label ID="Label3" runat="server" Text="Due Amount :"></asp:Label></strong></label>
                 <asp:TextBox ID="txtdueamt" runat="server"  Enabled="false"  CssClass="textbox-medium1"></asp:TextBox>
                  <asp:HiddenField ID="txtRefundamt" runat="server" />
                <div class="clear"></div>
            </div>
     
           <div class="form-sec-row">
                <label><strong><asp:Label ID="Label7" runat="server" Text="Final Sattlement :"></asp:Label></strong></label>
                   <asp:CheckBox ID="CheckBox1" runat="server" Width="150px" TextAlign="Left" />
                    <span class="book" style="display:;"><asp:DropDownList ID="ddlBook" runat="server" CssClass="textbox-medium2"  >
                     </asp:DropDownList></span>
               <asp:HiddenField ID="HiddenField1" runat="server" Value="0"/>
               <asp:HiddenField ID="HdnPatientType" runat="server" Value="0"/>
                <div class="clear"></div>
            </div>

            <div class="form-sec-row">
                <label><strong>&nbsp;</strong></label>
                <asp:Button ID="Button1" runat="server" OnClick="Button1_Click"   Height="28px" Text="Submit" CssClass="submit-button" />
                <asp:Button ID="Button2" runat="server" Text="Cancel"   Height="28px" OnClick="Button2_Click" CssClass="submit-button" />
                <div class="clear"></div>
            </div>  
            </div>
         <asp:Panel ID="Panel1" runat="server">
         <table width="100%">
                    <tr>        
                        <td align="center">    <div id='mydiv'>              
                            <asp:Literal ID="ltrReport" runat="server"></asp:Literal><br />  </div>                  
                        </td>
                    </tr>
                    <tr>
                        <td align="center">
                     
                         <input type="button" value="Back" style="width:70px; font-size:x-small" onclick="window.history.back()" />
                         <input type="button" id="cmdPrint" value="Print" style="width:70px; font-size:x-small" onclick="javascript:printDiv('mydiv')"/>
                            <asp:Button ID="passJV" runat="server" Text="Pass Journal" OnClick="passJV_Click" Visible="true"  style="width:90px; font-size:x-small"/>
                            <asp:Button ID="passPayment" runat="server" Text="Accounts Entry" Visible="true" OnClick="passPayment_Click"  style="width:90px; font-size:x-small"/>
                        </td>
                    </tr>
            </table>
         </asp:Panel>
   

 
     
    </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>

