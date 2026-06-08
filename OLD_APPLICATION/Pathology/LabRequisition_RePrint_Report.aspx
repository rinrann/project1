<%@ Page Title="" Language="C#" MasterPageFile="~/MasterAll/MasterPageAll.master" AutoEventWireup="true" CodeFile="LabRequisition_RePrint_Report.aspx.cs" Inherits="Pathology_LabRequisition_RePrint_Report" %>

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


    function DisableBackButton() {
        window.history.forward()
    }
</script>   <div class="pageheader">
         <asp:Label ID="Label1" runat="server">Patient Requisition Report</asp:Label>
     </div>

     <div class="formbox">
       <table width="100%">
            <tr>
            <td> <label> Name : </label></td>
                  <td> 
                      <asp:TextBox ID="txtName" runat="server"></asp:TextBox></td>
                   <td> <label> C/O : </label></td>
                          <td> 
                      <asp:TextBox ID="txtCo" runat="server"></asp:TextBox></td>

                         <td> <label> Address : </label></td>
                          <td> 
                      <asp:TextBox ID="txtAddress" runat="server"></asp:TextBox></td>

                          <td> <label> Ph No : </label></td>
                                 <td> 
                                     <asp:TextBox ID="txtPhNo" runat="server"></asp:TextBox>
                                 </td>
                                 <td> 
                                     <asp:Button ID="btnSearch" runat="server" CssClass="submit-button"  
                                         Text="Search" onclick="btnSearch_Click" /></td>
            </tr></table>
            </div>
<div class="formbox">
<div  style='width:100%; height:150px; overflow:auto;'>
         <asp:GridView id="GridView1"  CssClass="grid" PagerStyle-CssClass="pgr" 
             DataKeyNames ="PatientReg" runat="server" AutoGenerateColumns="False"
          AllowPaging="True" PageSize ="100"   SelectedRowStyle-BackColor="GreenYellow"
                 OnRowCommand="GridView1_RowCommand" Width="100%">
                <RowStyle HorizontalAlign="Center" />
                <Columns>

                     <asp:TemplateField HeaderText="Requisition No" Visible="True">
                    <ItemTemplate><asp:Label ID="lblRequisitionNo" runat="server" Text='<%# Bind("RequisitionNo") %>'></asp:Label></ItemTemplate>
                    </asp:TemplateField> 

                    <asp:TemplateField HeaderText="Regn. No">
                    <ItemTemplate><asp:Label ID="lblPatientReg" runat="server" Text='<%# Bind("PatientReg") %>'></asp:Label>
                    </ItemTemplate></asp:TemplateField>


                    
                    <asp:TemplateField HeaderText="Patient Name">
                    <ItemTemplate><asp:Label ID="lblpatient_name" runat="server" Text='<%# Bind("patient_name") %>'></asp:Label></ItemTemplate>
                    </asp:TemplateField>


                      <asp:TemplateField HeaderText="C/O" >
                      <ItemTemplate><asp:Label ID="lblguardian_name" runat="server" Text='<%# Bind("guardian_name") %>'></asp:Label>
                      </ItemTemplate></asp:TemplateField>


                        
                    <asp:TemplateField HeaderText="Address">
                    <ItemTemplate><asp:Label ID="lblvill_city" runat="server" Text='<%# Bind("vill_city") %>'></asp:Label></ItemTemplate>
                    </asp:TemplateField> 
                       
                    <asp:TemplateField HeaderText="Phone - 1">
                    <ItemTemplate><asp:Label ID="lblPhNo1" runat="server" Text='<%# Bind("PhNo1") %>'></asp:Label></ItemTemplate>
                    </asp:TemplateField>
                   
                    <asp:TemplateField HeaderText="Test Date">
                    <ItemTemplate><asp:Label ID="lblTestDate" runat="server" Text='<%# Bind("TestDate") %>'></asp:Label></ItemTemplate>
                    </asp:TemplateField>
 
                    <asp:TemplateField HeaderText="Advance" ItemStyle-HorizontalAlign="Right">
                    <ItemTemplate><asp:Label ID="lblAdAmt" runat="server" Text='<%# Bind("AdAmt") %>'></asp:Label></ItemTemplate>
                    </asp:TemplateField>    
                                
                    <asp:CommandField SelectText="Show Report" ShowSelectButton="True" ControlStyle-CssClass="temp" HeaderText="Show Report">
                        <ControlStyle CssClass="temp"></ControlStyle>
                    </asp:CommandField>
                  
                </Columns>
                <PagerStyle CssClass="pgr" />
                <EditRowStyle BackColor="#CCFF33" />
                <AlternatingRowStyle BackColor="#FFDB91" />
        </asp:GridView> 
        </div>
        </div>

   <div class="formbox">
   
          <table width="100%" >    <tr>        
                        <td  align="center">   <div id='mydiv'>            
                            <asp:Literal ID="ltrReport" runat="server"></asp:Literal><br />   </div>                 
                        </td>
                    </tr>

                     <tr>
                        <td  align="center">
                            <asp:Button ID="Button6" runat="server" Text="Back" Font-Size="X-Small"  
                                Width="70px"  />
                            <asp:Button ID="Button7" runat="server" Font-Size="X-Small"  Width="70px" Text="Print" OnClientClick="javascript:printDiv('mydiv')" />
      </td>
                    </tr>
                    </table>
                     </div>
    
</asp:Content>

