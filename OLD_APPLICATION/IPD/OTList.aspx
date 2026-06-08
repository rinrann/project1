<%@ Page Title="" Language="C#" MasterPageFile="~/MasterAll/MasterPageAll.master" AutoEventWireup="true" CodeFile="OTList.aspx.cs" Inherits="IPD_OTList" %>

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
         <asp:Label ID="Label1" runat="server">OT Patient List</asp:Label>
    </div>
<div class="formbox">
        <div class="form-sec">
            <div class="error">
                <strong><asp:Label ID="lblError" runat="server" Font-Bold="True"></asp:Label></strong>
                <div class="clear"></div>
            </div></div></div>
    <div>

<table cellpadding="0" cellspacing="0"  title="Search" width="100%"> 
            <tr>
                <td>
                    
             <label class="ipdList" style='width:85px;'><strong>OT Type:</strong></label> </td>
             <td>
             
                        <asp:DropDownList ID="DropDownList1" runat="server" CssClass="textbox-medium1" Width="85px"
                            onselectedindexchanged="DropDownList1_SelectedIndexChanged" 
                            AutoPostBack="True">
                        </asp:DropDownList>
          
            
</td>
                <td>
             <label class="ipdList" style='width:75px;'><strong>OT Name :</strong></label>
             </td>
             <td>
             <asp:DropDownList 
                            ID="DropDownList2" runat="server" CssClass="textbox-medium1" 
                            Width="85px"
                         >
                        </asp:DropDownList>
          
</td>
  <td>
                       <div class="form-sec-row"> 
             <label class="pname"><strong>Doctor :</strong></label>
            </div>  
</td>
<td>
 <asp:TextBox ID="TextBox1" runat="server" CssClass="textbox-medium1" Width="138px" ></asp:TextBox>
</td>
                <td>
                             <div class="form-sec-row"> 
             <label class="pname"><strong>Patient Name :</strong></label> 
            </div>                  
</td>
<td><asp:TextBox ID="TextBox2" runat="server" CssClass="textbox-medium1" Width="138px" ></asp:TextBox></td>

                <td class="style1">
                             <div class="form-sec-row"> 
           <asp:Button ID="Button1" runat="server" Text="Search"  CssClass="submit-button1"  Height="28px"
                                     onclick="Button1_Click" />
            </div>                  
</td>             
                      
            </tr>
            
           <tr>
                <td colspan="9" align="center">
                 <div style='width:100%; height:550px; overflow:auto;'>
            <asp:GridView id="GridView1"  CssClass="grid"
                 PagerStyle-CssClass="pgr" DataKeyNames ="PatientReg" runat="server"   SelectedRowStyle-BackColor="GreenYellow"
                 AutoGenerateColumns="False" AllowPaging="True" 
                           PageSize="1000" onrowcommand="GridView1_RowCommand" 
                         onpageindexchanging="GridView1_PageIndexChanging" onrowdatabound="GridView1_RowDataBound" 
                              
                            >
                <RowStyle HorizontalAlign="Center" />
                <Columns>
                
                    <asp:TemplateField HeaderText="REQUISITION NO" Visible="false">
                       
                        <ItemTemplate>
                            <asp:Label ID="lblreqno" runat="server" Text='<%# Bind("OperationReqID") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                      <asp:TemplateField HeaderText="Reg No">
                       
                        <ItemTemplate>
                            <asp:Label ID="lblregno" runat="server" Text='<%# Bind("PatientReg") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Name">                       
                        <ItemTemplate>
                            <asp:Label ID="lblname" runat="server" Text='<%# Bind("patient_name") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>

                       <asp:TemplateField HeaderText="Bed No">
                        <ItemTemplate>                        
                            <asp:Label ID="lblbed" runat="server" Text='<%# Bind("BedNoText") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField> 
                       
                    <asp:TemplateField HeaderText="Admn Date">
                        <ItemTemplate>                        
                            <asp:Label ID="lbladate" runat="server" Text='<%# Bind("ADate") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>

                          <asp:TemplateField HeaderText="Opn Date & Time">
                        <ItemTemplate>                        
                            <asp:Label ID="lblStartTime" runat="server" Text='<%# Bind("StartTime") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>


                        <asp:TemplateField HeaderText="Opn Name">
                       
                        <ItemTemplate>
                            <asp:Label ID="lbloname" runat="server" Text='<%# Bind("OperationName") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                 
                             
                  <asp:TemplateField HeaderText="Opn Note">
                        <ItemTemplate>
                           <asp:LinkButton ID="LinkButton3" CommandName="OPERATION" CommandArgument='<%# Eval("OperationReqID") %>' runat="server">Operation</asp:LinkButton>
                                     <asp:Label ID="Label20" runat="server" Font-Bold="true"  ForeColor="Red"></asp:Label> 
                        </ItemTemplate>
                    </asp:TemplateField>


                         
                    
                     <asp:TemplateField HeaderText="DELIVERY NOTE">
                        <ItemTemplate>
                           <asp:LinkButton ID="LinkButton8" CommandName="DELIVERY" CommandArgument='<%# Eval("OperationReqID") %>'  runat="server">Delivery</asp:LinkButton>
                                     <asp:Label ID="Label21" runat="server" Font-Bold="true" ForeColor="Red"></asp:Label> 
                        </ItemTemplate>
                    </asp:TemplateField>  

                          <asp:TemplateField HeaderText="BIOPSY NOTE">
                        <ItemTemplate>
                           <asp:LinkButton ID="LinkButton4"  CommandName="BIOPSY" CommandArgument='<%# Eval("OperationReqID") %>' runat="server">Biopsy</asp:LinkButton>
                                     <asp:Label ID="Label22" runat="server" Font-Bold="true" ForeColor="Red"></asp:Label> 
                        </ItemTemplate>
                    </asp:TemplateField> 

                    <asp:TemplateField HeaderText="ANESTHESIA NOTE">
                        <ItemTemplate>
                             <asp:LinkButton ID="LinkButton2"  CommandName="ANESTHESIA" CommandArgument='<%# Eval("OperationReqID") %>' runat="server">Anesthesia</asp:LinkButton>
                                       <asp:Label ID="Label23" runat="server" Font-Bold="true" ForeColor="Red"></asp:Label> 
                        </ItemTemplate>
                    </asp:TemplateField>
                   
                
                      <asp:TemplateField HeaderText="OT CONSUMABLE">
                        <ItemTemplate>
                           <asp:LinkButton ID="LinkButton5" CommandName="CONSUMABLE" CommandArgument='<%# Eval("OperationReqID") %>' runat="server">OT Cons</asp:LinkButton>
                                     <asp:Label ID="Label24" runat="server" Font-Bold="true"  ForeColor="Red"></asp:Label> 
                        </ItemTemplate>
                    </asp:TemplateField> 
                      <asp:TemplateField HeaderText="ATTENDENCE FEES">
                        <ItemTemplate>
                           <asp:LinkButton ID="LinkButton6" CommandName="ATTENDENCE" CommandArgument='<%# Eval("OperationReqID") %>'  runat="server" >Attn. Fees</asp:LinkButton> 
                                     <asp:Label ID="Label25" runat="server" Font-Bold="true" ForeColor="Red"></asp:Label> 
                        </ItemTemplate>
                    </asp:TemplateField>    
                    
                        <asp:TemplateField HeaderText="INSTRUMENT COST">
                        <ItemTemplate>
                           <asp:LinkButton ID="LinkButton7" CommandName="INSTRUMENT" CommandArgument='<%# Eval("OperationReqID") %>'  runat="server">Ins. Cost</asp:LinkButton>
                                     <asp:Label ID="Label26" runat="server" Font-Bold="true" ForeColor="Red"></asp:Label> 
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
    </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>

