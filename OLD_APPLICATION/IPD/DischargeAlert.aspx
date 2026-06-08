<%@ Page Title="" Language="C#" MasterPageFile="~/MasterAll/MasterPageAll.master" AutoEventWireup="true" CodeFile="DischargeAlert.aspx.cs" Inherits="IPD_DischargeAlert" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">


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
         <asp:Label ID="Label1" runat="server">Discharge Dashboard</asp:Label>
     </div>
                 <div class="formbox1">
                         <div class="error">
                        <strong>
                        <asp:Label ID="lblError" runat="server" Font-Bold="True" CssClass="fadeout"></asp:Label>
                        </strong>
                        <div class="clear">
                        </div>
                    </div>
<%--            
        <table width="100%">
      <tr>
                <td align="center">--%>
                 <div style=' height:auto; overflow:auto;'>
            <asp:GridView id="GridView2"  CssClass="grid"
                 PagerStyle-CssClass="pgr" DataKeyNames ="patient_name" runat="server"   SelectedRowStyle-BackColor="GreenYellow"
                 AutoGenerateColumns="False" AllowPaging="True" 
                           PageSize="100" onpageindexchanging="GridView2_PageIndexChanging" 
                         onrowcommand="GridView2_RowCommand" 
                         onrowdatabound="GridView2_RowDataBound" OnSelectedIndexChanged="GridView2_SelectedIndexChanged" >
                <RowStyle HorizontalAlign="Center" />
                <Columns>
                 

                      <asp:TemplateField HeaderText="Regn. No">
                       
                        <ItemTemplate>
                            <asp:Label ID="lblReg" runat="server" Text='<%# Bind("PatientReg") %>'></asp:Label>
                        </ItemTemplate> 
                    </asp:TemplateField>


                    <asp:TemplateField HeaderText="Patient Name">
                       
                        <ItemTemplate>
                            <asp:Label ID="lblname" runat="server" Text='<%# Bind("patient_name") %>'></asp:Label>
                        </ItemTemplate> 
                    </asp:TemplateField>
                    
                    <asp:TemplateField HeaderText="Bed No">
                        <ItemTemplate>                        
                            <asp:Label ID="lbladdress" runat="server" Text='<%# Bind("BedNoText") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField> 
                       
                    <asp:TemplateField HeaderText="Admission Date">
                        <ItemTemplate>                        
                            <asp:Label ID="lblADate" runat="server" Text='<%# Bind("ADate") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>    
                    
                        <asp:TemplateField HeaderText="Prob Discharge Date">
                        <ItemTemplate>                        
                            <asp:Label ID="DischargeDate" runat="server" Text='<%# Bind("DischargeDate") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>

                       <asp:TemplateField HeaderText="Diagnosis">
                        <ItemTemplate>                        
                            <asp:Label ID="Diagnosis" runat="server" Text='<%# Bind("DiagnosisName") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>

                       <asp:TemplateField HeaderText="Operation Name">
                        <ItemTemplate>                        
                            <asp:Label ID="OTName" runat="server" Text='<%# Bind("OTName") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>

                       <asp:TemplateField HeaderText="Operation Date">
                        <ItemTemplate>                        
                            <asp:Label ID="OTDate" runat="server" Text='<%# Bind("OTDate") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>


                       <asp:TemplateField HeaderText="Adm. Day No" >

                        <ItemTemplate>                        
                            <asp:Label ID="DateDifference" runat="server" Text='<%# Bind("DateDifference") %>'></asp:Label>
                        </ItemTemplate>

                    </asp:TemplateField>

                       <asp:TemplateField HeaderText="Opn Day No" >
                        <ItemTemplate>                        
                            <asp:Label ID="TotalOT" runat="server" Text='<%# Bind("TotalOT") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>

               

                       <asp:TemplateField HeaderText="Bill Amount">
                        <ItemTemplate>                        
                            <asp:Label ID="lblTotalBill" runat="server" Text='<%# Bind("TotalBill") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>

                       <asp:TemplateField HeaderText="Due Amount">
                        <ItemTemplate>                        
                            <asp:Label ID="Due" runat="server" Text='<%# Bind("Due") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:ButtonField ButtonType="Link" Text="Make Payment" CommandName="payment" HeaderText="Payment" />
                    <asp:CommandField HeaderText="Discharge" SelectText="Discharge" ShowSelectButton="True" />
                    
                    <asp:TemplateField HeaderText="Paid Amount" Visible="false">
                        <ItemTemplate>                        
                            <asp:Label ID="lblpaid" runat="server" Text='<%# Bind("paid") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="ledgerid" Visible="false">
                        <ItemTemplate>                        
                            <asp:Label ID="lblledgerid" runat="server" Text='<%# Bind("ledgerid") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                </Columns>
                <PagerStyle CssClass="pgr" />
                <EditRowStyle BackColor="#CCFF33" /> 
                <SelectedRowStyle BackColor="GreenYellow" />
        </asp:GridView>
          </div>
            
<%--</td>
            </tr>
        </table>--%>
      
   </div>
    </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>

