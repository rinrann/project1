<%@ Page Language="C#" MasterPageFile="~/MasterAll/MasterPageAll.master" AutoEventWireup="true" CodeFile="DoctorPaymentDashBoard.aspx.cs" Inherits="Account_DoctorPaymentDashBoard" %>

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
    <script language="javascript" type="text/javascript">

        function Calling() {

            //            var date = new Date();
            //            $("input[id$='txtvalidityDate']").datepicker({ dateFormat: 'dd/mm/yy', minDate: date });

            var date = new Date();
            $("input[id$='txtFromDate']").datepicker({ dateFormat: 'dd/mm/yy' });
            $("input[id$='txtTodate']").datepicker({ dateFormat: 'dd/mm/yy' });
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

    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <div class="pageheader">
                <asp:Label ID="Label1" runat="server">Doctor Payment DashBoard</asp:Label>
            </div>
             <div class="error">
                <strong><asp:Label ID="lblError" runat="server" Font-Bold="True"></asp:Label>
                </strong>
                <div class="clear"></div>
            </div>

            <div class="formbox" style='width:1000px;'>
                <div class="form-sec-row">
                    <label style='color:Blue'><strong>Search Criteria :</strong></label>
                    <div class="clear"></div>
                </div>
                <div class="form-sec-row">
                    <table width="100%">
                           <tr>
                                   <td style="width:10%">  
                                       <asp:Label id="Label2" Width="100px" runat="server" Text="Type of Payee"/>
                                   </td>
                                   <td style="width:20%">  
                                        <asp:DropDownList ID="DropDownList1" Width="150px" runat="server" AutoPostBack="True" CssClass="textbox-medium1" 
                                            onselectedindexchanged="DropDownList1_SelectedIndexChanged">
                                        </asp:DropDownList>
                                        <div class="clear">
                                        </div>
                                   </td>
                                    <td style="width:20%">
                                        <asp:Label id="lbltype" runat="server" Text="Type"/>
                                          <%-- <label id="reftype" runat="server"><strong>Doctor/Quack Type :</strong></label> --%>
                                    </td>
                                   <td style="width:20%">  
                                        <asp:DropDownList ID="ddlType" Width="150px" runat="server" CssClass="textbox-medium1" >
                                        
                                        </asp:DropDownList>
                                        <div class="clear">
                                        </div>
                                   </td>
                                    <td style="width:20%">
                                    <asp:Button ID="Button1" runat="server" Text="Search" CssClass="submit-generate" OnClick="Button1_Click" />
                                    </td>
                            </tr>
                        <tr>

                        <td>  <label><strong>From Date:</strong></label> </td>
                        <td>   <asp:TextBox ID="txtFromDate" CssClass="textbox-medium1" runat="server" Width="150px"></asp:TextBox> </td>

                        <td>  <label><strong>To Date:</strong></label> </td>
                        <td colspan="2">   <asp:TextBox ID="txtTodate" CssClass="textbox-medium1" runat="server" Width="150px"></asp:TextBox> </td>
                        

           </tr>
                    </table>
                </div>
            </div>
             <div class="formbox"  style='width:100%;max-height:150px;overflow:auto;'>
                 <asp:HiddenField ID="docIdhid" runat="server" />
            <div style='width:100%;'>
                <center>
                <table width="100%" style='background-color:#FB7B13; color:#FFF;'>
                      <tr style='border:1px solid black;'> 
                           <td style='width:10%;' align="center">Sl.No</td>
                            <td style='width:50%;' align="center">Name Of Payee</td>
                            <td style='width:20%; padding-left:25px;' align="center">Type of Payee</td>
                            <td style='width:20%; padding-left:25px;' align="center">Payable Amount</td>
                      </tr>
                </table>
               </center>
            </div>

            <div class="listing" style='height:200px; overflow:auto;'>
                   <asp:GridView id="GridView1"  Width="100%"  CssClass="grid" PagerStyle-CssClass="pgr" 
               DataKeyNames ="Id" runat="server"   SelectedRowStyle-BackColor="GreenYellow"
                 AutoGenerateColumns="False" AllowPaging="True" PageSize="100" 
               onrowcommand="GridView1_RowCommand"  ShowHeader="false"
				 onpageindexchanging="GridView1_PageIndexChanging" 
               onrowdatabound="GridView1_RowDataBound">          
				 <RowStyle HorizontalAlign="Center" />  
                       <Columns>
                           <asp:TemplateField HeaderText="Sl.No"   ItemStyle-Width="0%"  ItemStyle-HorizontalAlign="Center"> 
                                <ItemTemplate>
                                    <asp:Label ID="lblSlno" runat="server" Width="0%"></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                           <asp:TemplateField HeaderText="Sl.No"   ItemStyle-Width="10%"  ItemStyle-HorizontalAlign="Center" Visible="false"> 
                                <ItemTemplate>
                                    <asp:Label ID="lblId" runat="server" Width="10%" Text='<%# Bind("Id") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                           <asp:TemplateField HeaderText="Name of Payee"   ItemStyle-Width="50%"  ItemStyle-HorizontalAlign="Left">                      
                                <ItemTemplate>
                                    <asp:LinkButton ID="LinkButton1" CommandName="Select" CommandArgument='<%# Eval("Id") %>'  runat="server"> 
							        <asp:Label ID="lblregno" runat="server" Text='<%# Bind("Name") %>'></asp:Label>
                                    </asp:LinkButton>                          
                                </ItemTemplate>
                            </asp:TemplateField>
                           <asp:TemplateField HeaderText="Type Of Payee"   ItemStyle-Width="20%"  ItemStyle-HorizontalAlign="Center"> 
                        <ItemTemplate>
                            <asp:Label ID="lblname" runat="server" Text='<%# Bind("Type") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                           <asp:TemplateField HeaderText="Amount"   ItemStyle-Width="24%"  ItemStyle-HorizontalAlign="Right"> 
                                <ItemTemplate>
                                    <asp:Label ID="lblAmt" runat="server" Width="24%"></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                        </Columns>
                <PagerStyle CssClass="pgr" />
                <EditRowStyle BackColor="#CCFF33" />
                <%--<AlternatingRowStyle BackColor="#FFDB91" /> --%>
                <SelectedRowStyle BackColor="GreenYellow" />
        </asp:GridView>
            </div>

            </div>


        <%--Listing for quack --%>
            <div id="quackdiv" runat="server" style="width:1000px;">
            <div class="formbox"style='width:100%; '>
                <div style='width:100%;'>
                <center>
                <table width="100%" style='background-color:#FB7B13; color:#FFF;'>
                      <tr style='border:1px solid black;'> 
                          <td style='width:30px;' align="center"></td>
                           <td style='width:30px;' align="center">Sl.No</td>
                           <td style='width:90px;' align="center">Name Of Patient</td>
                           <td style='width:50px; padding-left:25px;' align="center">admission Date </td>
                           <td style='width:50px; padding-left:25px;' align="center">Discharge Date</td>
                           <td style='width:50px; padding-left:25px;' align="center">Commission</td>
                           <td style='width:50px; padding-left:25px;' align="center">Total Bill Paid(by patient)</td>
                      </tr>
                </table>
                </center>
                </  div>
                <div class="listing" style='max-height:200px; overflow:auto;'>
                   <asp:GridView id="GridView2"  Width="1000px"  CssClass="grid" PagerStyle-CssClass="pgr" 
               DataKeyNames ="patientledger" runat="server"   SelectedRowStyle-BackColor="GreenYellow"
                 AutoGenerateColumns="False" AllowPaging="False" PageSize="100" 
               onrowcommand="GridView2_RowCommand"  ShowHeader="false"
				 onpageindexchanging="GridView2_PageIndexChanging" 
               onrowdatabound="GridView2_RowDataBound">          
				 <RowStyle HorizontalAlign="Center" />  
                       <Columns>
                           <asp:TemplateField HeaderText="Check"   ItemStyle-Width="5px"  ItemStyle-HorizontalAlign="Center"> 
                                        <ItemTemplate>
                                            <asp:CheckBox ID="ChkPatientqck" runat="server"/>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                           <asp:TemplateField HeaderText="Sl.No"   ItemStyle-Width="30px"  ItemStyle-HorizontalAlign="Center"> 
                                <ItemTemplate>
                                    <asp:Label ID="lblQckSlno" runat="server" Width="30px"></asp:Label>
                                            <asp:Label ID="lblPatientLedgerQck" runat="server" Text='<%# Eval("patientledger") %>' Visible="false" Width="25px"></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                           <asp:TemplateField HeaderText="Name of Payee"   ItemStyle-Width="90px"  ItemStyle-HorizontalAlign="Left">                      
                                <ItemTemplate>
                                    
							        <asp:Label ID="lblQckname" runat="server" Text='<%# Bind("patientName") %>'></asp:Label>
                                                           
                                </ItemTemplate>
                            </asp:TemplateField>

                           <asp:TemplateField HeaderText="Type Of Payee"   ItemStyle-Width="50px"  ItemStyle-HorizontalAlign="Center"> 
                                <ItemTemplate>
                                    <asp:Label ID="lblQckAdd" runat="server" Text='<%# Bind("admissionDate") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>

                           <asp:TemplateField HeaderText="Type Of Payee"   ItemStyle-Width="50px"  ItemStyle-HorizontalAlign="Center"> 
                                <ItemTemplate>
                                    <asp:Label ID="lblQckType" runat="server" Text='<%# Bind("DischargeDt") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                           <asp:TemplateField HeaderText="Type Of Payee"   ItemStyle-Width="50px"  ItemStyle-HorizontalAlign="Right"> 
                                <ItemTemplate>
                                    <asp:Label ID="lblQckPh" runat="server" Text='<%# Bind("ReferCharge") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                           <asp:TemplateField HeaderText=""   ItemStyle-Width="50px"  ItemStyle-HorizontalAlign="Center" Visible="false"> 
                                <ItemTemplate>
                                    <asp:Label ID="lblBold" runat="server" Text='<%# Bind("bold") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="TotBill"   ItemStyle-Width="50px"  ItemStyle-HorizontalAlign="Right"> 
                                <ItemTemplate>
                                 <asp:Label ID="lblTotBillqck" runat="server" Text='<%# Eval("TotalBill") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                        </Columns>
                       <PagerStyle CssClass="pgr" />
                <EditRowStyle BackColor="#CCFF33" />
                <AlternatingRowStyle BackColor="#FFDB91" />
                <SelectedRowStyle BackColor="GreenYellow" />
                  </asp:GridView>
            </div>
                </div>
                </div>
        <%--Listing for quack end --%>

            <%--Listing for Doctor --%>
            <div id="docdiv" runat="server" style="width:100%;">
                <div class="formbox"style='width:100%; '>
                    <div style='width:100%;'>
                    <center>
                    <table width="100%" style='background-color:#FB7B13; color:#FFF;'>
                          <tr style='border:1px solid black;'> 
                               <td style='width:5px;' align="center" rowspan="2"></td>
                               <td style='width:35px;' align="center" rowspan="2">Sl.No</td>
                               <td style='width:90px;' align="center" rowspan="2">Patient Name</td>
                               <td style='width:50px; padding-left:25px;' align="center" rowspan="2">Admission Date</td>
                               <td style='width:50px; padding-left:25px;' align="center" rowspan="2">Discharge Date</td>
                               <td style='width:50px; padding-left:25px;' align="center" rowspan="2">Diagnosis</td>
                               <td style='width:50px; padding-left:25px;' align="center" rowspan="2">OT Name</td>
                               <td style='width:150px; padding-left:25px;' align="center" colspan="3">Pay Amount</td>
                               <td style='width:50px; padding-left:25px;' align="center" rowspan="2">Total Bill Paid(by patient)</td>
                          </tr>
                          <tr>
                              <td style='width:50px; padding-left:25px;' align="center" >Fees</td>
                              <td style='width:50px; padding-left:25px;' align="center" >Surgary/<br/>Anesthetics</td>
                              <td style='width:50px; padding-left:25px;' align="center" >Commissions</td>
                          </tr>
                    </table>
                    </center>
                    </div>
                    <div class="listing" style='max-height:200px; overflow:auto;'>
                        <asp:GridView id="GridView3"  Width="100%"  CssClass="grid" PagerStyle-CssClass="pgr" 
                           DataKeyNames ="patientledger" runat="server"   SelectedRowStyle-BackColor="GreenYellow"
                             AutoGenerateColumns="False" AllowPaging="False" PageSize="100" 
                           onrowcommand="GridView3_RowCommand"  ShowHeader="false"
				             onpageindexchanging="GridView3_PageIndexChanging" 
                           onrowdatabound="GridView3_RowDataBound">          
				             <RowStyle HorizontalAlign="Center" />  
                                <Columns>
                                    <asp:TemplateField HeaderText="Check"   ItemStyle-Width="5px"  ItemStyle-HorizontalAlign="Center"> 
                                        <ItemTemplate>
                                            <asp:CheckBox ID="ChkPatient" runat="server"/>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Sl.No"   ItemStyle-Width="10px"  ItemStyle-HorizontalAlign="Center"> 
                                        <ItemTemplate>
                                            <asp:Label ID="lblDocSlno" runat="server" Width="25px"></asp:Label>
                                            <asp:Label ID="lblPatientLedgerDoc" runat="server"  Text='<%# Eval("patientledger") %>' Visible="false" Width="25px"></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="PatientName"   ItemStyle-Width="90px"  ItemStyle-HorizontalAlign="Left">                      
                                        <ItemTemplate>
                                    
							        <asp:Label ID="lblpatientName" runat="server" Text='<%# Eval("patientName") %>'></asp:Label>
                                                           
                                        </ItemTemplate>
                                    </asp:TemplateField>

                                   <asp:TemplateField HeaderText="AdDate"   ItemStyle-Width="50px"  ItemStyle-HorizontalAlign="Center"> 
                                        <ItemTemplate>
                                            <asp:Label ID="lblAdDate" runat="server" Text='<%# Eval("admissionDate") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>

                                   <asp:TemplateField HeaderText="DiscDate"   ItemStyle-Width="50px"  ItemStyle-HorizontalAlign="Center"> 
                                        <ItemTemplate>
                                            <asp:Label ID="lblDiscDate" runat="server" Text='<%# Eval("DischargeDt") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                   <asp:TemplateField HeaderText="Diagnosis"   ItemStyle-Width="50px"  ItemStyle-HorizontalAlign="left"> 
                                        <ItemTemplate>
                                            <asp:Label ID="lblDiag" runat="server" Text='<%# Eval("Diagnosis") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                   <asp:TemplateField HeaderText="OtName"   ItemStyle-Width="50px"  ItemStyle-HorizontalAlign="left"> 
                                        <ItemTemplate>
                                            <asp:Label ID="lblot" runat="server" Text='<%# Eval("OtName") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Fees"   ItemStyle-Width="60px"  ItemStyle-HorizontalAlign="Right"> 
                                        <ItemTemplate>
                                            <asp:Label ID="lblFees" runat="server" Text='<%# Eval("VisitCharge") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Surgary"   ItemStyle-Width="60px"  ItemStyle-HorizontalAlign="Right"> 
                                        <ItemTemplate>
                                            <asp:Label ID="lblSurgary" runat="server" Text='<%# Eval("surgoncharge") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Commissions"   ItemStyle-Width="60px"  ItemStyle-HorizontalAlign="Right"> 
                                        <ItemTemplate>
                                            <asp:Label ID="lblCommissions" runat="server" Text='<%# Eval("ReferCharge") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    
                                    <asp:TemplateField HeaderText="TotBill"   ItemStyle-Width="50px"  ItemStyle-HorizontalAlign="Right"> 
                                        <ItemTemplate>
                                            <asp:Label ID="lblTotBill" runat="server" Text='<%# Eval("TotalBill") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                           <asp:TemplateField HeaderText=""   ItemStyle-Width="50px"  ItemStyle-HorizontalAlign="Center" Visible="false"> 
                                <ItemTemplate>
                                    <asp:Label ID="lblBoldDoc" runat="server" Text='<%# Bind("bold") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                                </Columns>
                            <PagerStyle CssClass="pgr" />
                            <EditRowStyle BackColor="#CCFF33" />
                            <AlternatingRowStyle BackColor="#FFDB91" />
                            <SelectedRowStyle BackColor="GreenYellow" />
                         </asp:GridView>
                        </div>
                </div>
            </div>
            <%--Listing for Doctor end --%>
            <div align="center" style="text-align:center;">
                <asp:Label ID="HiddenField1" runat="server" Value="" Visible="false"/>
            <asp:Button ID="butPay" runat="server" Text="Pay" CssClass="submit-generate" Width="50px" OnClick="butPay_Click" cssStyle="align:center" />
            <asp:Button ID="butSelect" runat="server" Text="Select All" CssClass="submit-generate" Width="100px" cssStyle="align:center" OnClick="butSelect_Click" />
            <asp:Button ID="butDeselect" runat="server" Text="Deselect All" CssClass="submit-generate" Width="100px" cssStyle="align:center" OnClick="butDeselect_Click" /></div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>