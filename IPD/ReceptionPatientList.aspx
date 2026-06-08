<%@ Page Title="" Language="C#" MasterPageFile="~/MasterAll/MasterPageAll.master" AutoEventWireup="true" CodeFile="ReceptionPatientList.aspx.cs" Inherits="IPD_ReceptionPatientList" %>

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
         <asp:Label ID="Label1" runat="server">Patient Not Yet Discharged </asp:Label>
     </div>
                 <div class="formbox1"> 
                     <div style="float:none;padding-left:440px;">
                        <strong>
                        <asp:Label ID="lbltotalpatient" runat="server" Font-Size="Large"  Font-Names="Arial Rounded MT" Font-Bold="True" ></asp:Label>
                        </strong>
                        </div>


           
    <%--       <div style='width:100%;'>
  <table width="100%" style='background-color:#FB7B13; color:#FFF;'>
  <tr style='border:1px solid black;'> 
   <td style='width:100px;' align="center">Regn. No</td>
   <td style='width:100px;'  align="center">Name</td>
    <td style='width:100px;' align="center">Bed No</td>
     <td style='width:70px;' align="center">Adm. Date</td>
      <td style='width:100px;' align="center">Diagnosis</td>
       <td style='width:90px;' align="center">OT  Name</td>
        <td style='width:70px;' align="center">OT  Date</td>
               <td style='width:50px;' align="center">Adm. Day No</td>
        <td style='width:50px;' align="center">OT Day No</td>
          <td style='width:60px;' align="center">Prob Dis Date</td>
              <td style='width:70px;' align="center">Bill Amt</td>
                      <td style='width:70px;' align="center">Due Amt</td>
          <td style='width:90px;' align="center">Discharge</td>
          </tr>
  </table> 
  </div> --%>

         
      <%--           <div style='width:100%; height:500px; overflow:auto;'>--%>

            <asp:GridView id="GridView2"  CssClass="grid" Width="100%"
                 PagerStyle-CssClass="pgr" DataKeyNames ="patient_name" runat="server"   SelectedRowStyle-BackColor="GreenYellow"
                 AutoGenerateColumns="False" AllowPaging="True" 
                           PageSize="200" onpageindexchanging="GridView2_PageIndexChanging" 
                         onrowcommand="GridView2_RowCommand" 
                         onrowdatabound="GridView2_RowDataBound"  >
                <RowStyle HorizontalAlign="Center" />
                <Columns>
                 

                      <asp:TemplateField HeaderText="Regn. No" ItemStyle-Width="100px" >
                       
                        <ItemTemplate>
                            <asp:Label ID="lblReg" runat="server" Text='<%# Bind("PatientReg") %>'></asp:Label>
                        </ItemTemplate>
                        <HeaderStyle Width="100px" />
                    </asp:TemplateField>


                    <asp:TemplateField HeaderText="Patient Name" ItemStyle-Width="100px" >
                       
                        <ItemTemplate>
                            <asp:Label ID="lblname" runat="server" Text='<%# Bind("patient_name") %>'></asp:Label>
                        </ItemTemplate>
                        <HeaderStyle Width="100px" />
                    </asp:TemplateField>
                    
                    <asp:TemplateField HeaderText="Bed No" ItemStyle-Width="100px">
                        <ItemTemplate>                        
                            <asp:Label ID="lbladdress" runat="server" Text='<%# Bind("BedNoText") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField> 
                       
                    <asp:TemplateField HeaderText="Admission Date" ItemStyle-Width="70px">
                        <ItemTemplate>                        
                            <asp:Label ID="lblphone1" runat="server" Text='<%# Bind("ADate") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>

                       <asp:TemplateField HeaderText="Diagnosis" ItemStyle-Width="100px">
                        <ItemTemplate>                        
                            <asp:Label ID="Diagnosis" runat="server" Text='<%# Bind("DiagnosisName") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>

                       <asp:TemplateField HeaderText="Operation Name" ItemStyle-Width="90px">
                        <ItemTemplate>                        
                            <asp:Label ID="OTName" runat="server" Text='<%# Bind("OTName") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>

                       <asp:TemplateField HeaderText="Operation Date" ItemStyle-Width="70px">
                        <ItemTemplate>                        
                            <asp:Label ID="OTDate" runat="server" Text='<%# Bind("OTDate") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>


                       <asp:TemplateField HeaderText="Adm. Day No" ItemStyle-Width="50px" >

                        <ItemTemplate>                        
                            <asp:Label ID="DateDifference" runat="server" Text='<%# Bind("DateDifference") %>'></asp:Label>
                        </ItemTemplate> 
                    </asp:TemplateField>

                       <asp:TemplateField HeaderText="Opn Day No" ItemStyle-Width="50px">
                        <ItemTemplate>                        
                            <asp:Label ID="TotalOT" runat="server" Text='<%# Bind("TotalOT") %>'></asp:Label>
                        </ItemTemplate> 
                    </asp:TemplateField>

                       <asp:TemplateField HeaderText="Prob Discharge Date" ItemStyle-Width="60px">
                        <ItemTemplate>                        
                            <asp:Label ID="DischargeDate" runat="server" Text='<%# Bind("DischargeDate") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>

                       <asp:TemplateField HeaderText="Bill Amount" ItemStyle-Width="70px">
                        <ItemTemplate>                        
                            <asp:Label ID="lblTotalBill" runat="server" Text='<%# Bind("TotalBill") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>

                       <asp:TemplateField HeaderText="Due Amount" ItemStyle-Width="70px">
                        <ItemTemplate>                        
                            <asp:Label ID="Due" runat="server" Text='<%# Bind("Due") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>


                                  
                    <asp:CommandField HeaderText="Discharge" SelectText="Discharge"  ItemStyle-Width="90px"  
                        ShowSelectButton="True" />


                                  
                </Columns>
                <PagerStyle CssClass="pgr" />
                <EditRowStyle BackColor="#CCFF33" /> 
                <SelectedRowStyle BackColor="GreenYellow" />
        </asp:GridView>
       <%--   </div>--%>
 
   
   </div>
    </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>

