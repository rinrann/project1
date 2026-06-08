<%@ Page Title="" Language="C#" MasterPageFile="~/MasterAll/MasterPageAll.master" AutoEventWireup="true" CodeFile="DoctorPaymentReport.aspx.cs" Inherits="Assignment_DoctorPaymentReport" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
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
    function printDiv(divID) {
        var divElements = document.getElementById(divID).innerHTML;
        // document.getElementById("ctl00_ContentPlaceHolder1_mydiv").style.width = '900px';
        var oldPage = document.body.innerHTML;
        document.body.innerHTML = "<html><head><title></title></head><body>" + divElements + "</body>";
        window.print();
        document.body.innerHTML = oldPage;
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
         <asp:Label ID="Label1" runat="server">Doctor Payment Report</asp:Label>
     </div>
     <div class="formbox" style="width:1100px;">
         <asp:Panel ID="Panel1" runat="server">
            <table width="100%">
                <tr>
                    <td>
                       <strong><asp:Label id="Label2" Width="100px" runat="server" Text="Type of Payee" /></strong>
                    </td>
                    <td>  
                        <asp:DropDownList ID="DropDownList1" Width="150px" runat="server" AutoPostBack="True" CssClass="textbox-medium1" 
                                            onselectedindexchanged="DropDownList1_SelectedIndexChanged">
                        </asp:DropDownList>
                      <div class="clear">
                      </div>
                    </td>


                    <td>
                        <strong><asp:Label id="lbltype" runat="server" Text="Type"/></strong>
                    </td>
                    <td>  
                 <%-- <asp:DropDownList ID="ddlDoctorType" CssClass="textbox-medium1"  Width="150px"  AutoPostBack="true"
                      runat="server" 
                      onselectedindexchanged="ddlDoctorType_SelectedIndexChanged" >
                  </asp:DropDownList>--%>
                        <asp:DropDownList ID="ddlType" Width="150px" runat="server" CssClass="textbox-medium1" AutoPostBack="True" onselectedindexchanged="ddlType_SelectedIndexChanged">
                        </asp:DropDownList>
                        <div class="clear">
                        </div>
                    </td>

           
                    <td> 
                         <label><strong>Doctor Name:</strong></label>
                    </td>
                    <td>  
                        <asp:DropDownList ID="ddlDoctorName" CssClass="textbox-medium1"  Width="150px"  runat="server">
                        </asp:DropDownList>
                    </td>
                 
                    <td>  
                     <asp:Button ID="btnFetch" runat="server"  CssClass="submit-generate" 
                      Text="Search" onclick="btnFetch_Click"  />
                    </td>
           

                </tr>

                <tr>

                    <td>  <label><strong>From Date:</strong></label> </td>
                    <td>   <asp:TextBox ID="txtFromDate" CssClass="textbox-medium1" runat="server" Width="150px"></asp:TextBox> </td>

                    <td>  <label><strong>To Date:</strong></label> </td>
                    <td >   <asp:TextBox ID="txtTodate" CssClass="textbox-medium1" runat="server" Width="150px"></asp:TextBox> </td>
                    <td style="display:none">  <label><strong>Receipt No:</strong></label> </td>
                    <td style="display:none">  
                 
                        <asp:DropDownList ID="ddlrecptno" CssClass="textbox-medium1"  Width="150px"  runat="server">
                        </asp:DropDownList>
                        <asp:HiddenField ID="receiptno" runat="server" />
                        <asp:HiddenField ID="receiptdt" runat="server" />
                        <asp:HiddenField ID="docid" runat="server" />
                        <asp:HiddenField ID="docname" runat="server" />
                    </td>
                    <td style="display:none"> 
                  <asp:Button ID="btnGenerate" runat="server"  CssClass="submit-generate" 
                      Text="Generate Report" OnClick="btnGenerate_Click"  /></td>


              </tr>
        </table>
     </asp:Panel>
  </div>

        <div class="formbox"  style='width:100%;max-height:150px;overflow:auto;'>
            <div style='width:100%;'>
                <center>
                    <table width="100%" style='background-color:#FB7B13; color:#FFF;'>
                        <tr style='border:1px solid black;'> 
                            <td style='width:10%;' align="center">Sl.No</td>
                            <td style='width:10%;' align="center">Receipt No</td>
                            <td style='width:30%; padding-left:25px;' align="center">Doctor Name</td>
                            <td style='width:10%; padding-left:25px;' align="center">Payment Date</td>
                            <td style='width:15%; padding-left:25px;' align="center">Paid Amount</td>
                            <td style='width:15%; padding-left:25px;' align="center">Discount Amount</td>
                            <td style='width:10%; padding-left:25px;' align="center"></td>
                        </tr>
                    </table>
                </center>
            </div>
            <div class="listing" style='height:200px; overflow:auto;'>
                <asp:GridView id="GridView1"  Width="100%"  CssClass="grid" PagerStyle-CssClass="pgr" 
               DataKeyNames ="ReceiptNo" runat="server"  
                 AutoGenerateColumns="False" AllowPaging="True" PageSize="100" 
               onrowcommand="GridView1_RowCommand"  ShowHeader="false"
				 onpageindexchanging="GridView1_PageIndexChanging" 
               onrowdatabound="GridView1_RowDataBound">          
				 <RowStyle HorizontalAlign="Center" /> 
                    <Columns>
                        <asp:TemplateField HeaderText="Sl.No"   ItemStyle-Width="10%"  ItemStyle-HorizontalAlign="Center"> 
                                <ItemTemplate>
                                    <asp:Label ID="lblSlno" runat="server" Width="0%"></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                           <asp:TemplateField HeaderText="Receipt No"   ItemStyle-Width="10%"  ItemStyle-HorizontalAlign="Center" > 
                                <ItemTemplate>
                                    <%--<asp:LinkButton ID="LinkButton1" CommandName="Select" CommandArgument='<%# Eval("ReceiptNo") %>'  runat="server">  --%>
                                        <asp:Label ID="lblrecptno" runat="server" Text='<%# Bind("ReceiptNo") %>'></asp:Label>
                                    <asp:Label ID="lbldocid" runat="server" Text='<%# Bind("LedgerFK") %>' Visible="false"></asp:Label>
                                    </asp:LinkButton>  
                                </ItemTemplate>
                            </asp:TemplateField>
                           <asp:TemplateField HeaderText="Doctor Name"   ItemStyle-Width="30%"  ItemStyle-HorizontalAlign="Left">                      
                                <ItemTemplate>
                                    <asp:Label ID="lblname" runat="server" Text='<%# Bind("ledgerName") %>'></asp:Label>
							                                
                                </ItemTemplate>
                            </asp:TemplateField>
                           <asp:TemplateField HeaderText="Transaction date"   ItemStyle-Width="10%"  ItemStyle-HorizontalAlign="Center"> 
                        <ItemTemplate>
                            <asp:Label ID="lblDate" runat="server" Text='<%# Bind("Date") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                           <asp:TemplateField HeaderText="PayAmount"   ItemStyle-Width="15%"  ItemStyle-HorizontalAlign="Right"> 
                                <ItemTemplate>
                                     <asp:Label ID="lblpay" runat="server" Text='<%# Bind("Credit") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                        <asp:TemplateField HeaderText="DiscAmount"   ItemStyle-Width="15%"  ItemStyle-HorizontalAlign="Right"> 
                                <ItemTemplate>
                                     <asp:Label ID="lblDisc" runat="server" ></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                        <asp:TemplateField HeaderText="Receipt No"   ItemStyle-Width="10%"  ItemStyle-HorizontalAlign="Center" > 
                                <ItemTemplate>
                                    <asp:LinkButton ID="LinkButton2" CommandName="report" CommandArgument='<%# Container.DataItemIndex %>'  runat="server"> 
                                        <asp:Label ID="lblgenreport" runat="server" Text='GeneRate Report'></asp:Label>
                                    </asp:LinkButton>  
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
          <div id="mydiv">
              <table width="100%">
                    <tr>        
                        <td align="center" style='width:100%;'>   <div id='Div1' runat="server">                 
                            <asp:Literal ID="ltrReport" runat="server"></asp:Literal><br />    </div>                
                        </td>
                    </tr>
                    </table>
                 </div>   
                     <table width="100%">
                    <tr>
                        <td align="center">
                     
                               <input type="button" id="btnBack" value="Back" runat="server" style="width:70px; font-size:x-small" onclick="window.history.back()" />
                                <input type="button" id="cmdPrint"  runat="server" value="Print" style="width:70px; font-size:x-small" onclick="javascript: printDiv('mydiv')"/>
                            <asp:Button ID="btnPDF" runat="server"  style="width:70px; font-size:x-small" 
                                   Text="PDF"/>  
                         
                         
                         </td>
                    </tr>
            </table>

            

             </ContentTemplate>
    </asp:UpdatePanel>


</asp:Content>

