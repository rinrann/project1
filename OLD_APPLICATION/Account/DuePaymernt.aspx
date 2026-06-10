<%@ Page Title="" Language="C#" MasterPageFile="~/MasterAll/MasterPageAll.master" AutoEventWireup="true" CodeFile="DuePaymernt.aspx.cs" Inherits="Account_DuePaymernt" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
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

            
        function Calling() {

            $("input[id$='txtamt']").keyup(function () {
                var sum = Number($("input[id$='HiddenField1']").val()) - (Number($("input[id$='txtamt']").val()) + Number($("input[id$='txtdiscount']").val()));
                $("input[id$='txtdueamt']").val(sum);
            });

            $("input[id$='txtdiscount']").keyup(function () {
                var sum = Number($("input[id$='HiddenField1']").val()) - (Number($("input[id$='txtamt']").val()) + Number($("input[id$='txtdiscount']").val()));
                $("input[id$='txtdueamt']").val(sum);
            });


            $("input[id$='txtamt']").keydown(function (event) {
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


            $("input[id$='txtdiscount']").keydown(function (event) {
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


            $("input[id$='Button1']").click(function () {

                   if ($("input[id$='txtamt']").val() == '') {
                        alert('Please Enter Payment Amount !');
                        $("input[id$='txtamt']").focus();
                        $("input[id$='txtamt']").addClass('textboxerr');
                        return false;
                    }
                    else {
                        $("input[id$='txtamt']").removeClass('textboxerr');
                    }
            
                if ($("input[id$='txtreason']").val() == '') {
                    alert('Please Enter Reason of Payment !');
                    $("input[id$='txtreason']").focus();
                    $("input[id$='txtreason']").addClass('textboxerr');
                    return false;
                }
                else {
                    $("input[id$='txtreason']").removeClass('textboxerr');
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
    </asp:UpdateProgress>
--%>

<asp:UpdatePanel ID="UpdatePanel1" runat="server">
    <ContentTemplate>
    <div class="pageheader">
         <asp:Label ID="Label1" runat="server">Due Payment</asp:Label>
     </div>
  

        
            <div class="error">
                <strong><asp:Label ID="lblError" runat="server" Font-Bold="True"></asp:Label>
                </strong>
                <div class="clear"></div>
            </div>
            <asp:HiddenField ID="HiddenField1" runat="server" />          <asp:HiddenField ID="HiddenField2" runat="server" />

            <div class="formbox">

            <table width="100%">      
            <tr>
            <td>   <label><strong>Registration No :</strong></label></td>
            <td> 
                <asp:TextBox ID="txtreg" runat="server" CssClass="textbox-medium1" EnableTheming="True" 
                   ></asp:TextBox> 
                   </td>
            <td>      <label><strong>Patient Name :</strong></label></td>
            <td>
                  <asp:TextBox ID="txtname" runat="server" CssClass="textbox-medium1" 
                   ></asp:TextBox>
                   </td>
                   <td></td>
            </tr>
            <tr>
            <td><label><strong>Address :</strong></label>  </td>
            <td>    
                <asp:TextBox ID="txtAddress" runat="server" CssClass="textbox-medium1" 
                   ></asp:TextBox>
                   </td>
            <td>     <label><strong>Ph No :</strong></label>  </td>
            <td>   
                <asp:TextBox ID="txtPhNo" runat="server" CssClass="textbox-medium1" 
                   ></asp:TextBox>
                   </td>
                   <td>
                       <asp:Button ID="Button3" runat="server" Text="Search" onclick="Button3_Click"   Height="28px"   Width="80px" CssClass="submit-button"/></td>
            </tr>
            
            </table>


             <div class="listing" style=' height:250px; overflow:auto;'>
         <asp:GridView id="GridView1"  CssClass="grid"  
                 PagerStyle-CssClass="pgr"  runat="server" 
                 AutoGenerateColumns="False" AllowPaging="True" PageSize ="1000"  Width="100%"  
                     SelectedRowStyle-BackColor="GreenYellow" onrowcommand="GridView1_RowCommand"> 
                 <RowStyle HorizontalAlign="Center" />
                <Columns>
                    <asp:TemplateField HeaderText="LedgerID" Visible="false">
                        <ItemTemplate>
                            <asp:Label ID="lblLedgerID" runat="server" Text='<%# Bind("LedgerID") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>


                     <asp:TemplateField HeaderText="Transaction id" Visible ="false">                       
                        <ItemTemplate>
                            <asp:Label ID="lblTransactionId" runat="server" Text='<%# Bind("TransactionId") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="Date">                       
                        <ItemTemplate>
                            <asp:Label ID="lblDate" runat="server" Text='<%# Bind("Date") %>'></asp:Label>
                        </ItemTemplate>
                        </asp:TemplateField>

                    <asp:TemplateField HeaderText="Reg.No.">
                       
                        <ItemTemplate>
                            <asp:Label ID="lblPatientReg" runat="server" Text='<%# Bind("PatientReg") %>'></asp:Label>
                        </ItemTemplate>
                        </asp:TemplateField>
                    
                    <asp:TemplateField HeaderText="Name">
                        <ItemTemplate>                        
                            <asp:Label ID="lblpatient_name" runat="server" Text='<%# Bind("patient_name") %>'></asp:Label>
                        </ItemTemplate>
                       </asp:TemplateField>   
                     
                    <asp:TemplateField HeaderText="Address" >
                        <ItemTemplate>                        
                            <asp:Label ID="lblvill_City" runat="server" Text='<%# Bind("vill_City") %>'></asp:Label>
                        </ItemTemplate>
                         </asp:TemplateField>

                      <asp:TemplateField HeaderText="Ph.No." >
                        <ItemTemplate>                        
                            <asp:Label ID="lblPhNo1" runat="server" Text='<%# Bind("PhNo1") %>'></asp:Label>
                        </ItemTemplate>
                     </asp:TemplateField>

                      <asp:TemplateField HeaderText="Due Amt." >
                        <ItemTemplate>                        
                            <asp:Label ID="lblDebit" runat="server" Text='<%# Bind("Debit") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
     
                    <asp:CommandField SelectText="Adjust" ShowSelectButton="True" ItemStyle-HorizontalAlign="Center"  ControlStyle-CssClass="temp" 
                        HeaderText="Adjust" EditText="Adjust">
                        <ControlStyle CssClass="temp"></ControlStyle>
                    </asp:CommandField>
                </Columns>
                <PagerStyle CssClass="pgr" />
                <EditRowStyle BackColor="#CCFF33" />
                <AlternatingRowStyle BackColor="#FFDB91" />
                <SelectedRowStyle BackColor="GreenYellow" />
        </asp:GridView> 
        </div>

            </div>
 
               <div class="formbox">
            

            <div class="form-sec-row">
                <label><strong>Current Payment :</strong></label>
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
                     <asp:ListItem>Cash</asp:ListItem>
                     <asp:ListItem>Card</asp:ListItem>
                     </asp:DropDownList>
                <div class="clear"></div>
            </div>
           

               <div class="form-sec-row">
                <label><strong>Reason :</strong></label>
                 <asp:TextBox ID="txtreason" runat="server" TextMode="MultiLine" Height="40px" CssClass="textbox-medium1"></asp:TextBox>
                <div class="clear"></div>
            </div>

              <div class="form-sec-row">
                <label><strong><asp:Label ID="Label3" runat="server" Text="Due Amount :"></asp:Label></strong></label>
                 <asp:TextBox ID="txtdueamt" runat="server"  Enabled="false"  CssClass="textbox-medium1"></asp:TextBox>
                <div class="clear"></div>
            </div>
              <div class="form-sec-row">
                <label><strong><asp:Label ID="Label2" runat="server" Text="Select Book:"></asp:Label></strong></label>
                 <asp:DropDownList ID="ddlBook" runat="server" CssClass="textbox-medium2"  >
                     </asp:DropDownList>
                <div class="clear"></div>
            </div>
     
              <div class="form-sec-row">
                <label><strong>&nbsp;</strong></label>
                <asp:Button ID="Button1" runat="server"   Height="28px" Text="Submit"  
                      Width="80px" CssClass="submit-button" onclick="Button1_Click"  />
                <asp:Button ID="Button2" runat="server" Text="Cancel"   Height="28px"  
                      Width="80px" CssClass="submit-button" onclick="Button2_Click"   />
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
                         <input type="button" id="cmdPrint" value="Print" style="width:70px; font-size:x-small" onclick="javascript:printDiv('mydiv')"/></td>
                    </tr>
            </table>
         </asp:Panel>
   

 
     
    </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>

