<%@ Page Title="" Language="C#" MasterPageFile="~/MasterAll/MasterPageAll.master" AutoEventWireup="true" CodeFile="PatientList.aspx.cs" Inherits="IPD_PatientList" %>

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
         <asp:Label ID="Label1" runat="server">Patient Dashboard</asp:Label>
     </div>

    <div class="formbox" style="overflow:scroll">
        <div class="form-sec">
            <div class="error">
                <strong><asp:Label ID="lblError" runat="server" Font-Bold="True"></asp:Label></strong>
                <div class="clear"></div>
            </div></div>
            <%--</div>--%>
 

<table cellpadding="0" cellspacing="0"  title="Search">
            <tr>
                <td>
                    <div class="form-sec-row"> 
             <label class="ipdList" style='width:45px;'><strong>Floor :</strong></label> 
                        <asp:DropDownList ID="DropDownList1" runat="server" CssClass="textbox-dropdown" 
                            onselectedindexchanged="DropDownList1_SelectedIndexChanged" 
                            AutoPostBack="True">
                        </asp:DropDownList>
            </div>
            
</td>
                <td>
                    <div class="form-sec-row"> 
             <label class="ipdList" style='width:80px;'><strong>Room Type :</strong></label><asp:DropDownList 
                            ID="DropDownList2" runat="server" CssClass="textbox-dropdown" 
                            onselectedindexchanged="DropDownList2_SelectedIndexChanged" AutoPostBack="True" 
                         >
                        </asp:DropDownList>
            
            </div>
</td>
  <td>
                    <div class="form-sec-row"> 
             <label class="ipdList" style='width:70px;'><strong>Room No :</strong></label><asp:DropDownList 
                            ID="DropDownList3" runat="server" CssClass="textbox-dropdown" 
                            onselectedindexchanged="DropDownList3_SelectedIndexChanged" 
                            AutoPostBack="True">
                        </asp:DropDownList>
            
            </div>
</td>

<td>
                             <div class="form-sec-row"> 
             <label class="ipdList" style='width:65px;'><strong>Bed No :</strong></label><asp:DropDownList 
                                     ID="DropDownList4" runat="server" CssClass="textbox-dropdown" 
                                     AutoPostBack="True">
                        </asp:DropDownList>
            
            </div>                  
</td>
                <td>
                             <div class="form-sec-row"> 
             <label class="pname"><strong>Patient Name :</strong></label> <asp:TextBox ID="txtname" runat="server" CssClass="textbox-medium1" Width="138px" ></asp:TextBox>
            </div>                  
</td>

                <td class="style1">
                             <div class="form-sec-row"> 
           <asp:Button ID="Button1" runat="server" Text="Search"  CssClass="submit-button"  Height="28px"
                                     onclick="Button1_Click" />
            </div>                  
</td>             
                      
            </tr>
            
           <tr>
                <td colspan="6" align="center">
                    <div   style='width:100%; height:300px; overflow:auto;'>
            <asp:GridView id="GridView1"  CssClass="grid"
                 PagerStyle-CssClass="pgr" DataKeyNames ="PatientReg" runat="server"   SelectedRowStyle-BackColor="GreenYellow"
                 AutoGenerateColumns="False" AllowPaging="True" 
                           PageSize="15" onrowcommand="GridView1_RowCommand" onpageindexchanging="GridView1_PageIndexChanging" 
                              
                            >
                <RowStyle HorizontalAlign="Center" />
                <Columns>
                
                    <asp:TemplateField HeaderText="Registration No" >
                       
                        <ItemTemplate>
                            <asp:Label ID="lblregno" runat="server" Text='<%# Bind("PatientReg") %>'></asp:Label>
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
                            <asp:Label ID="lblphone1" runat="server" Text='<%# Bind("ADate") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                            
                    <asp:TemplateField HeaderText="Sister/Aya">
                        <ItemTemplate>
                            <asp:LinkButton ID="LinkButton10" CommandName="sistecharge" CommandArgument='<%# Eval("PatientReg") %>' runat="server">Sister/Aya Charges</asp:LinkButton>
                        </ItemTemplate>
                    </asp:TemplateField> 
                    <asp:TemplateField HeaderText="BED TRANSFER">
                        <ItemTemplate>
                           <asp:LinkButton ID="LinkButton8" CommandName="BedTransfer" CommandArgument='<%# Eval("PatientReg") %>'  runat="server">Bed Transfer</asp:LinkButton>
                        </ItemTemplate>
                    </asp:TemplateField> 
                    <asp:TemplateField HeaderText="Doctor Visit">
                        <ItemTemplate>
                            <asp:LinkButton ID="LinkButton1" CommandName="docvisit" CommandArgument='<%# Eval("PatientReg") %>' runat="server">Doctor Visit</asp:LinkButton>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Add Medicine">
                        <ItemTemplate>
                             <asp:LinkButton ID="LinkButton2"  CommandName="addmedicine" CommandArgument='<%# Eval("PatientReg") %>' runat="server">Add Medicine</asp:LinkButton>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Add Services">
                        <ItemTemplate>
                           <asp:LinkButton ID="LinkButton3" CommandName="addservices" CommandArgument='<%# Eval("PatientReg") %>' runat="server">Add Services</asp:LinkButton>
                        </ItemTemplate>
                    </asp:TemplateField>
                      <asp:TemplateField HeaderText="Add Consumables">
                        <ItemTemplate>
                           <asp:LinkButton ID="LinkButton4"  CommandName="addConsumable" CommandArgument='<%# Eval("PatientReg") %>' runat="server">Add Consumables</asp:LinkButton>
                        </ItemTemplate>
                    </asp:TemplateField> 
                      <asp:TemplateField HeaderText="Daily Checkup">
                        <ItemTemplate>
                           <asp:LinkButton ID="LinkButton5" CommandName="dailycheckup" CommandArgument='<%# Eval("PatientReg") %>' runat="server">Daily Checkup</asp:LinkButton>
                        </ItemTemplate>
                    </asp:TemplateField> 
                      <asp:TemplateField HeaderText="OT Requisition">
                        <ItemTemplate>
                           <asp:LinkButton ID="LinkButton6" CommandName="otreq" CommandArgument='<%# Eval("PatientReg") %>'  runat="server" >OT Requisition</asp:LinkButton> 
                        </ItemTemplate>
                    </asp:TemplateField>   
                     
                    
                        <asp:TemplateField HeaderText="LAB Requisition">
                        <ItemTemplate>
                           <asp:LinkButton ID="LinkButton7" CommandName="Labrequisition" CommandArgument='<%# Eval("PatientReg") %>'  runat="server">LAB Requisition</asp:LinkButton>
                        </ItemTemplate>
                    </asp:TemplateField>    
                    
                               <asp:TemplateField HeaderText="To Do Task">
                        <ItemTemplate>
                           <asp:LinkButton ID="LinkButton8" CommandName="ToDoTask" CommandArgument='<%# Eval("PatientReg") %>'  runat="server" >To Do Task</asp:LinkButton> 
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

