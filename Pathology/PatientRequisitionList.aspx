<%@ Page Title="" Language="C#" MasterPageFile="~/MasterAll/MasterPageAll.master" AutoEventWireup="true" CodeFile="PatientRequisitionList.aspx.cs" Inherits="Pathology_PatientRequisitionList" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
<script type="text/javascript">
    function Calling() {
        var date = new Date();
        $("input[id$='txtdate']").datepicker({ dateFormat: 'dd/mm/yy' });
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

    function ConfirmationMessage() {
        var data = confirm("Are you sure you want to submit the form.");
        if (data) {
            return true;
        } else {
            return false;
        }

    } 
</script>
  
<script type="text/javascript" language="javascript">
    function ShowDialog1() {
        //var rtvalue = window.open("ExistTestPopup.aspx", "sss", "Width=1050px; Height=400px;top=150px;");
        var w = 1000;
        var h = 400;
        var left = (screen.width / 2) - (w / 2);
        var top = (screen.height / 2) - (h / 2);
        return window.open("ExistTestPopup.aspx", "ServicePopup", 'titlebar=no, toolbar=no, location=no, directories=no, status=no, menubar=no, scrollbars=no, resizable=no, copyhistory=no, width=' + w + ', height=' + h + ', top=' + top + ', left=' + left);
     }
</script>

<script type="text/javascript">
    function autoCompleteEx_ItemSelected(sender, args) {
        debugger;
        var regname = args.get_value().split('~');
        document.getElementById("ctl00_ContentPlaceHolder1_txtDocId").value = regname[0];
        document.getElementById("ctl00_ContentPlaceHolder1_txtDocName").value = regname[1];

    }
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
         <asp:Label ID="Label1" runat="server">Requisition List</asp:Label>
     </div>
    <div class="formbox" style="width:auto;">
        <div class="form-sec">
            <div class="error">
                <strong><asp:Label ID="lblError" runat="server" Font-Bold="True"></asp:Label></strong>
                <div class="clear"></div>
            </div></div>
    

<table cellpadding="0" cellspacing="0"  title="Search">
              <tr>
               <td style="width:10%;">
                   <label class="AppoList"><strong>Date :</strong></label> 
               </td>
                <td  style="width:10%;">
                    <div class="form-sec-row"> 
                        
                        <asp:TextBox ID="txtdate" runat="server" CssClass="textbox-medium1" Width="138px" TextMode="Date"></asp:TextBox>
                    </div>
                </td>
                <td style="width:10%;">
                   <label class="AppoList"><strong>Consultant :</strong></label> 
               </td>
                <td style="width:20%;">
                    <div class="form-sec-row"> 
                        
                        <div style="display:none;">
                            <asp:TextBox ID="txtDocId" CssClass="textbox-medium1"  runat="server"  style="width:100%;" ></asp:TextBox>
                        </div>
                    
                        <asp:TextBox ID="txtDocName" CssClass="textbox-medium1"  runat="server"  style="width:100%;"></asp:TextBox>
                        <cc1:AutoCompleteExtender ServiceMethod="SearchDoctorName"    OnClientItemSelected="autoCompleteEx_ItemSelected"    MinimumPrefixLength="1"
                            CompletionInterval="100" EnableCaching="false" CompletionSetCount="10" TargetControlID="txtDocName"  ID="AutoCompleteExtender1" runat="server" 
                           FirstRowSelected = "false" >
                        </cc1:AutoCompleteExtender>
                    </div>
                </td>
                <td style="width:10%;">
                   <label class="AppoList"><strong>Patient Name :</strong></label> 
               </td>
                <td style="width:20%;">
                    <asp:TextBox ID="txtpname" CssClass="textbox-medium1"  runat="server"  style="width:100%;" ></asp:TextBox>
                </td>
                  <td style="width:10%;">
                      &nbsp;
                  </td>
                <td style="width:10%;">
                    <div class="form-sec-row"> 
                        <asp:Button ID="Button1" runat="server" Text="Search" Height="28px"   CssClass="submit-button1" onclick="Button1_Click"/>
                    </div>                  
                </td>             
                      
            </tr>
            
           <tr>
                <td colspan="8" align="center">
                    <div  style='width:100%; height:400px; overflow:auto;'>
            <asp:GridView id="GridView1"  CssClass="grid"
                 PagerStyle-CssClass="pgr" DataKeyNames ="RequisitionNo" runat="server"   SelectedRowStyle-BackColor="GreenYellow"
                 AutoGenerateColumns="False" AllowPaging="True" 
                           onrowdatabound="GridView1_RowDataBound" PageSize="100"  Width="100%"
                            onpageindexchanging="GridView1_PageIndexChanging" onrowcommand="GridView1_RowCommand" 
                            >
                <RowStyle HorizontalAlign="Center" />
                <Columns>
                    <asp:TemplateField HeaderText="Requisition Type">
                        <ItemTemplate>                        
                            <asp:Label ID="lblReqType" runat="server" Text='<%# Bind("RequisitionType") %>'></asp:Label>
                            <asp:Label ID="lblType" runat="server" Text='<%# Bind("ReqType") %>' Visible="false"></asp:Label>
                        </ItemTemplate>
                        <ItemStyle HorizontalAlign="Left" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Consultant / Referal">
                        <ItemTemplate>                        
                            <asp:Label ID="lblconsultant" runat="server" Text='<%# Bind("ConsultantName") %>'></asp:Label>
                        </ItemTemplate>
                        <ItemStyle HorizontalAlign="Left" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Requisition No">
                        <ItemTemplate>
                            <asp:Label ID="lblID" runat="server" Text='<%# Bind("RequisitionNo") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Registration No">
                       
                        <ItemTemplate>
                            <asp:Label ID="lblregno" runat="server" Text='<%# Bind("RegistrationNo") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Patient Name">
                       
                        <ItemTemplate>
                            <asp:Label ID="lblname" runat="server" Text='<%# Bind("PatientName") %>'></asp:Label>
                        </ItemTemplate>
                        <ItemStyle HorizontalAlign="Left" />
                    </asp:TemplateField>
                    
                    <asp:TemplateField HeaderText="Address">
                        <ItemTemplate>                        
                            <asp:Label ID="lbladdress" runat="server" Text='<%# Bind("Address") %>'></asp:Label>
                        </ItemTemplate>
                        <ItemStyle HorizontalAlign="Left" />
                    </asp:TemplateField> 
                       
                    <asp:TemplateField HeaderText="Phone No">
                        <ItemTemplate>                        
                            <asp:Label ID="lblphone1" runat="server" Text='<%# Bind("Ph1") %>'></asp:Label>
                        </ItemTemplate>
                        <ItemStyle HorizontalAlign="Left" />
                    </asp:TemplateField>
                      <asp:TemplateField HeaderText="Phone - 2" Visible="false" >
                        <ItemTemplate>                        
                            <asp:Label ID="lblphone2" runat="server" Text='<%# Bind("Ph2") %>'></asp:Label>
                        </ItemTemplate>
                          <ItemStyle HorizontalAlign="Left" />
                    </asp:TemplateField>
         
                    <asp:TemplateField HeaderText="Test Date"  Visible="false" >
                        <ItemTemplate>                        
                            <asp:Label ID="lbltestdate" runat="server" Text='<%# Bind("tdate") %>'></asp:Label>
                        </ItemTemplate>
                        <ItemStyle HorizontalAlign="center" />
                    </asp:TemplateField>
                     <asp:TemplateField HeaderText="Delivery Date"  Visible="false" >
                        <ItemTemplate>                        
                            <asp:Label ID="lbldeldate" runat="server" Text='<%# Bind("ddate") %>'></asp:Label>
                        </ItemTemplate>
                         <ItemStyle HorizontalAlign="center" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Paid Amount">
                        <ItemTemplate>                        
                            <asp:Label ID="lblBillAmt" runat="server" Text='<%# Bind("BillAmt") %>'></asp:Label>
                        </ItemTemplate>
                        <ItemStyle HorizontalAlign="Right" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Payment Date">
                        <ItemTemplate>                        
                            <asp:Label ID="lblBillDate" runat="server" Text='<%# Bind("PayDate") %>'></asp:Label>
                        </ItemTemplate>
                        <ItemStyle HorizontalAlign="center" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Payment Time">
                        <ItemTemplate>                        
                            <asp:Label ID="lblBillTime" runat="server" Text='<%# Bind("PayTime") %>'></asp:Label>
                        </ItemTemplate>
                        <ItemStyle HorizontalAlign="center" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Test Done" Visible="false">
                        <ItemTemplate>                        
                           <asp:CheckBox ID="chkdone" runat="server" />
                        </ItemTemplate>
                    </asp:TemplateField>
                    <%--<asp:TemplateField HeaderText="Created By">
                        <ItemTemplate>                        
                            <asp:Label ID="lblcreatedby" runat="server" Text='<%# Bind("createdBy") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>--%> 
                    <asp:TemplateField>
                        <ItemTemplate>
                           <asp:LinkButton ID="LinkButton4"  CommandName="PerfDoc" CommandArgument="<%# Container.DataItemIndex %>"  runat="server">Performing Doc</asp:LinkButton>
                        </ItemTemplate>
                    </asp:TemplateField>                  
                    <asp:TemplateField Visible="false">
                        <ItemTemplate>
                           <%-- <asp:Button ID="Button2" runat="server" Height="28px"  CommandName="select"  CommandArgument='<%# Eval("RequisitionNo") %>'  CssClass="submit-button" Text="Register"/>--%>
                            <asp:Button ID="Button2" runat="server" Height="28px" CommandName="select" CommandArgument='<%# Container.DataItemIndex %>' CssClass="submit-button" Text="Register" />
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField Visible="false">
                        <ItemTemplate>
                            <asp:Button ID="Button3" runat="server" Height="28px" CommandName="cancel" CommandArgument='<%# Eval("RequisitionNo") %>'  OnClientClick="return ConfirmationMessage();" CssClass="submit-cancel" Text="Cancel" />
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField>
                        <ItemTemplate>
                            <asp:Button ID="Button4" runat="server" Height="28px" CommandName="reschedule" CommandArgument='<%# Container.DataItemIndex %>' CssClass="submit-schedule" Text="Edit" />
                        </ItemTemplate>
                    </asp:TemplateField>                   
                </Columns>
                <PagerStyle CssClass="pgr" />
                <EditRowStyle BackColor="#CCFF33" />
                <AlternatingRowStyle BackColor="#FFDB91" />
        </asp:GridView>
            </div>
            <asp:Button ID="btn_save" Text="Submit" runat="server" Class="submit-schedule" OnClick="btn_save_Click" Visible="false"/>
                     
</td>
            </tr>
        </table>    
    </div>
    </ContentTemplate>
    </asp:UpdatePanel>

</asp:Content>

