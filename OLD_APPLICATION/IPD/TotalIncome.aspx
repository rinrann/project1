<%@ Page Title="" Language="C#" MasterPageFile="~/MasterAll/MasterPageAll.master" AutoEventWireup="true" CodeFile="TotalIncome.aspx.cs" Inherits="IPD_TotalIncome" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">

   <script type="text/javascript" language="javascript">

       function ShowDialog() {
           var rtvalue = window.open("TotalTransactionPopup.aspx", "sss", "Width:800px; Height:550px; dialogLeft:250px;");
       }    

       function printDiv() {
           var divElements = document.getElementById("ctl00_ContentPlaceHolder1_mydiv").innerHTML;
           var oldPage = document.body.innerHTML;
           document.body.innerHTML = "<html><head><title></title></head><body>" + divElements + "</body>";
           window.print();
           document.body.innerHTML = oldPage;
           window.history.back();

       }

       function printDivDaily() {
           var divElements = document.getElementById("ctl00_ContentPlaceHolder1_DivDaily").innerHTML;
           var oldPage = document.body.innerHTML;
           document.body.innerHTML = "<html><head><title></title></head><body>" + divElements + "</body>";
           window.print();
           document.body.innerHTML = oldPage;
           window.history.back();
       }

       function printDivincome() {
           var divElements = document.getElementById("ctl00_ContentPlaceHolder1_divincome").innerHTML;
           var oldPage = document.body.innerHTML;
           document.body.innerHTML = "<html><head><title></title></head><body>" + divElements + "</body>";
           window.print();
           document.body.innerHTML = oldPage;
           window.history.back();
       }

       function printDivdue() {
           var divElements = document.getElementById("ctl00_ContentPlaceHolder1_divdue").innerHTML;
           var oldPage = document.body.innerHTML;
           document.body.innerHTML = "<html><head><title></title></head><body>" + divElements + "</body>";
           window.print();
           document.body.innerHTML = oldPage;
           window.history.back();
       }

       function printDivdiscount() {
           var divElements = document.getElementById("ctl00_ContentPlaceHolder1_divdiscount").innerHTML;
           var oldPage = document.body.innerHTML;
           document.body.innerHTML = "<html><head><title></title></head><body>" + divElements + "</body>";
           window.print();
           document.body.innerHTML = oldPage;
           window.history.back();
       }

       function printDivrefund() {
           var divElements = document.getElementById("ctl00_ContentPlaceHolder1_divrefund").innerHTML;
           var oldPage = document.body.innerHTML;
           document.body.innerHTML = "<html><head><title></title></head><body>" + divElements + "</body>";
           window.print();
           document.body.innerHTML = oldPage;
           window.history.back();
       }


        function Calling() {

            $(function () {
                $("#tabs").tabs();
            });

            var date = new Date();
            $("input[id$='TextBox1']").datepicker({ dateFormat: 'dd/mm/yy'});


            var date = new Date();
            $("input[id$='TextBox2']").datepicker({ dateFormat: 'dd/mm/yy' });

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
         <asp:Label ID="Label1" runat="server"> Total Income & Expenduture</asp:Label>
     </div> 
      <div class="formbox">
          <table width="100%">
                        
                         <tr>

                           <td style="display:none;"><strong>Reg. No :</strong></td>
                                 <td style="display:none;">
                                     <asp:TextBox ID="TextBox3" runat="server"></asp:TextBox></td>

                         <td><strong>From Date :</strong></td>
                                 <td>
                                     <asp:TextBox ID="TextBox1" runat="server"></asp:TextBox></td>

                                         <td><strong>To Date :</strong></td>
                                                 <td>
                                     <asp:TextBox ID="TextBox2" runat="server"></asp:TextBox></td>

                                            <td>
                                                <asp:Button ID="Button1" runat="server" Width="80px" Height="30px"  Text="Search" onclick="Button1_Click" />
                                                  </td>
                                                <td>
                                                    <a href="../Account/DuePaymernt.aspx">Take Due Payment</a></td>
                         </tr>
                         </table>
      <div id="tabs">
  <ul>
  <li><a href="#tabs-1">Total Trunsaction</a></li>
  <li><a href="#tabs-7">Daily Cash Details</a></li>
	<li><a href="#tabs-2">Income Details</a></li>
	<li><a href="#tabs-3">Due Details</a></li>
    	<li><a href="#tabs-4">Discount Details</a></li>
        	<li><a href="#tabs-5">Refund Details</a></li>
            <li><a href="#tabs-6">Get Details</a></li>
  </ul>
  <div id="tabs-1">
    <table width="100%">   
                            
                    <tr>        
                        <td align="center"  colspan="5">    <div id='mydiv' runat="server">              
                            <asp:Literal ID="ltrReport" runat="server"></asp:Literal><br />  </div>                  
                        </td>
                    </tr>
        <tr><td align="center" colspan="5">            
         <input type="button" id="cmdPrint"  runat="server" value="Print" style="width:70px; font-size:x-small" onclick="javascript: printDiv()"/>
            </td></tr>
                     
            </table>
  </div>
  <div id="tabs-7">
    <table width="100%">   
                            
                    <tr>        
                        <td align="center"  colspan="5">    <div id='DivDaily' runat="server">              
                            <asp:Literal ID="ltrDaily" runat="server"></asp:Literal><br />  </div>                  
                        </td>
                    </tr>
        <tr><td align="center" colspan="5">            
         <input type="button" id="cmdPrintDaily"  runat="server" value="Print" style="width:70px; font-size:x-small" onclick="javascript: printDivDaily()"/>
            </td></tr>
                     
            </table>
  </div>
  <div id="tabs-2">
    <table width="100%"> 
                    
                            
                    <tr>        
                        <td align="center"  colspan="5">    <div id='divincome' runat="server">              
                            <asp:Literal ID="ltrIncome" runat="server"></asp:Literal><br />  </div>                  
                        </td>
                    </tr>
               <tr><td align="center" colspan="5">            
         <input type="button" id="cmdincomrprint"  runat="server" value="Print" style="width:70px; font-size:x-small" onclick="javascript: printDivincome()"/>
            </td></tr>                   
            </table>
  </div>
  <div id="tabs-3">
    <table width="100%"> 
                    
                            
                    <tr>        
                        <td align="center"  colspan="5">    <div id='divdue' runat="server">              
                            <asp:Literal ID="ltrDue" runat="server"></asp:Literal><br />  </div>                  
                        </td>
                    </tr>
                <tr><td align="center" colspan="5">            
         <input type="button" id="cmdDueprint"  runat="server" value="Print" style="width:70px; font-size:x-small" onclick="javascript: printDivdue()"/>
            </td></tr>   
            </table>
  </div>
  <div id="tabs-4">
	    <table width="100%"> 
                    
                            
                    <tr>        
                        <td align="center"  colspan="5">    <div id='divdiscount' runat="server">              
                            <asp:Literal ID="ltrDiscount" runat="server"></asp:Literal><br />  </div>                  
                        </td>
                    </tr>

                 <tr><td align="center" colspan="5">            
         <input type="button" id="cmdDiscountPrint"  runat="server" value="Print" style="width:70px; font-size:x-small" onclick="javascript: printDivdiscount()"/>
            </td></tr>   
        
            </table>
  </div>

   <div id="tabs-5">
	    <table width="100%"> 
                    
                            
                    <tr>        
                        <td align="center"  colspan="5">    <div id='divrefund' runat="server">              
                            <asp:Literal ID="ltrRefund" runat="server"></asp:Literal><br />  </div>                  
                        </td>
                    </tr> 
                   
                 <tr><td align="center" colspan="5">            
         <input type="button" id="cmdRefundPrint"  runat="server" value="Print" style="width:70px; font-size:x-small" onclick="javascript: printDivrefund()"/>
            </td></tr> 
            </table>
  </div>


    <div id="tabs-6">
	    <table width="100%"> 
               <tr>        
                        <td>  
                              <div class="listing" style='width:100%; height:250px; overflow:auto;'>
         <asp:GridView id="GridView1"  Width="100%" CssClass="grid" PagerStyle-CssClass="pgr"
                 DataKeyNames ="LedgerId" runat="server" AutoGenerateColumns="False" AllowPaging="True"  
                                      PageSize="10000"  SelectedRowStyle-BackColor="GreenYellow" 
                                      onrowcommand="GridView1_RowCommand">
                   <RowStyle  HorizontalAlign="Center" />
                     <Columns>
                        <asp:TemplateField HeaderText="Ledger Id" Visible="false">
                            <ItemTemplate>
                                <asp:Label ID="lblLedgerId" runat="server" Text='<%# Bind("LedgerId") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Date"  >
                            <ItemTemplate>
                                <asp:Label ID="lblDate" runat="server" Text='<%# Bind("Date") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>

                          <asp:TemplateField HeaderText="Reg. No."  >
                         


                             <ItemTemplate>
                            <asp:LinkButton ID="LinkButton1" CommandName="PatientReg"  CommandArgument='<%# Eval("LedgerId") %>' runat="server">
                              <asp:Label ID="lblPatientReg" runat="server" Text='<%# Bind("PatientReg") %>'></asp:Label>                              
                              </asp:LinkButton>
                        </ItemTemplate>
                        </asp:TemplateField>

                          <asp:TemplateField HeaderText="Name"  >
                            <ItemTemplate>
                                <asp:Label ID="lblpatient_name" runat="server" Text='<%# Bind("patient_name") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>

                          <asp:TemplateField HeaderText="Address"  >
                            <ItemTemplate>
                                <asp:Label ID="lblvill_City" runat="server" Text='<%# Bind("vill_City") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>

                          <asp:TemplateField HeaderText="Phone No"  >
                            <ItemTemplate>
                                <asp:Label ID="lblPhNo1" runat="server" Text='<%# Bind("PhNo1") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>

                             <asp:TemplateField HeaderText="Income"  >
                            <ItemTemplate>
                                <asp:Label ID="lblCredit" runat="server" Text='<%# Bind("Credit") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                    <PagerStyle CssClass="pgr" />
                    <EditRowStyle BackColor="#CCFF33" />
                    <AlternatingRowStyle BackColor="#FFDB91" />
                </asp:GridView> 
        </div>         
                        </td>
                    </tr>
              </table>
  </div>
</div>


                     
          
            
           </div>

           
    </ContentTemplate>
    </asp:UpdatePanel>

</asp:Content>

