<%@ Page Title="" Language="C#" MasterPageFile="~/MasterAll/MasterPageAll.master" AutoEventWireup="true" CodeFile="PatientDashBoard.aspx.cs" Inherits="DayCare_PatientDashBoard" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <script type="text/javascript">
    function Calling() {

        var date = new Date();
        $("input[id$='txtdate']").datepicker({ dateFormat: 'dd/mm/yy'});
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
        var data = confirm("Do you want to Delete this record ? If yes Click Ok or Cancel");
        if (data) {
            return true;
        } else {
            return false;
        } 
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
         <asp:Label ID="Label1" runat="server">Patient Dashboard</asp:Label>
     </div>
     <div class="formbox">
        <div class="form-sec">
            <div class="error">
                <strong><asp:Label ID="lblError" runat="server" Font-Bold="True"></asp:Label></strong>
                <div class="clear"></div>
            </div></div>
           
            <table width="100%">
              <tr>
                <td>
                    <div class="form-sec-row"> 
             <label class="AppoList"><strong>Shift :</strong></label> 
                        <asp:DropDownList ID="DropDownList1" runat="server" CssClass="textbox-dropdown">
                        </asp:DropDownList>
            </div>
            
</td>
                <td>
                    <div class="form-sec-row"> 
             <label class="AppoList"><strong>Date :</strong></label> <asp:TextBox ID="txtdate" runat="server" CssClass="textbox-medium1" Width="138px" ></asp:TextBox>
            </div>
</td>
                <td>
                             <div class="form-sec-row"> 
             <label class="pname"><strong>Patient Name :</strong></label> <asp:TextBox ID="txtname" runat="server" CssClass="textbox-medium1" Width="138px" ></asp:TextBox>
            </div>                  
</td>
<td>
                             <div class="form-sec-row"> 
             <label class="reg"><strong>Reg No :</strong></label> <asp:TextBox ID="txtreg" runat="server" CssClass="textbox-medium1" Width="138px" ></asp:TextBox>
            </div>                  
</td>
                <td class="style1">
                             <div class="form-sec-row"> 
           <asp:Button ID="Button1" runat="server" Text="Search"  CssClass="submit-button"  Height="28px"
                                     onclick="Button1_Click1" />
            </div>                  
</td>             
                      
            </tr>
            </table>
            
            </div>
  <div>
<table>
           <tr>
                <td align="center">
                    <div class="form-sec-row" style=' height:auto; width:979px; overflow:auto;'> 
            <asp:GridView id="GridView1"  CssClass="grid"
                 PagerStyle-CssClass="pgr" DataKeyNames ="AppNo,PName" runat="server"  
                 AutoGenerateColumns="False" AllowPaging="True"  
                            onpageindexchanging="GridView1_PageIndexChanging" 
                            onrowcommand="GridView1_RowCommand" Width="979px" 
                            onrowdeleting="GridView1_RowDeleting" onrowdatabound="GridView1_RowDataBound" PageSize="100">
                <RowStyle HorizontalAlign="Center" />
                <Columns>

                    <asp:TemplateField HeaderText="Sl.No"   ItemStyle-Width="30px"  ItemStyle-HorizontalAlign="Center"> 
                        <ItemTemplate>
                            <asp:Label ID="lblSlno" runat="server" Width="30px"></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="ApppoId" Visible="false">
                        <ItemTemplate>
                            <asp:Label ID="lblID" runat="server" Text='<%# Bind("AppNo") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Regn. No" >
                     
                        <ItemTemplate>
                            <asp:Label ID="lblregno" runat="server" Text='<%# Bind("PatientReg") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Patient Name">
                         <HeaderStyle Width="250px" />
                        <ItemTemplate>
                            <asp:Label ID="lblname" runat="server" Text='<%# Bind("PName") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    
                    <asp:TemplateField HeaderText="Address">
                        <ItemTemplate>                        
                            <asp:Label ID="lbladdress" runat="server" Text='<%# Bind("PAddress") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField> 
                       
                    <asp:TemplateField HeaderText="Total Dialysis">
                        <ItemTemplate>                        
                            <asp:Label ID="lblDiaNo" runat="server" Text='<%# Bind("DiaNo") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField> 

                    <asp:TemplateField HeaderText="Current Dialysis Status">
                        <ItemTemplate>                        
                            <asp:Label ID="lblCurrentDiaNo" runat="server" Text='<%# Bind("CurrentDia") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                      <asp:TemplateField HeaderText="Paid Amt.">
                        <ItemTemplate>                        
                            <asp:Label ID="lblAdv" runat="server" Text='<%# Bind("Advance") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>

             <%--       for check Discharge Link..  Don't Delete this ;--%>

                      <asp:TemplateField HeaderText="Due Amt." Visible="false">
                        <ItemTemplate>                        
                            <asp:Label ID="lblDue" runat="server" Text='<%# Bind("Due") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>

                         <asp:TemplateField HeaderText="Due Amt."  >
                        <ItemTemplate>                        
                            <asp:Label ID="lblDueCF" runat="server" Text='<%# Bind("DueCF") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
     
            
                    <asp:TemplateField  HeaderText="Reschedule">
                        <ItemTemplate>
                        <asp:LinkButton ID="LinkButton3"  CommandName="reschedule"  CommandArgument='<%# Eval("AppNo") %>'  runat="server">Reschedule</asp:LinkButton>
                         </ItemTemplate>
                    </asp:TemplateField>

                   <asp:TemplateField HeaderText="Regn./Confirm">
                        <ItemTemplate>
                            <asp:LinkButton ID="LinkButton1"  CommandName="Confirm"  CommandArgument='<%# Eval("AppNo") %>'  runat="server">Register</asp:LinkButton>
                         </ItemTemplate>
                    </asp:TemplateField>
      
      
                       <asp:TemplateField  HeaderText="Monitoring">
                        <ItemTemplate>
                        <asp:LinkButton ID="LinkButton7"  CommandName="Monitoring"  CommandArgument='<%# Eval("AppoDate") %>'  runat="server">Monitoring</asp:LinkButton>
                         </ItemTemplate>
                    </asp:TemplateField>

                    
                    
                       <asp:CommandField  HeaderText="Medicine"  ShowSelectButton="True" 
                        SelectText="Add" />

                     

                       <asp:TemplateField  HeaderText="Lab Req.">
                        <ItemTemplate>
                        <asp:LinkButton ID="LinkButton5"  CommandName="LabReq"  CommandArgument='<%# Eval("PatientReg") %>'  runat="server">Send</asp:LinkButton>
                         </ItemTemplate>
                    </asp:TemplateField>


                    
                       <asp:TemplateField  HeaderText="Charge Dtls">
                        <ItemTemplate>
                        <asp:LinkButton ID="LinkButton4"  CommandName="ChargesDtls"  CommandArgument='<%# Eval("PatientReg") %>'  runat="server">Put</asp:LinkButton>
                         </ItemTemplate>
                    </asp:TemplateField>


                       <asp:TemplateField  HeaderText="Payment">
                        <ItemTemplate>
                        <asp:LinkButton ID="LinkButton8"  CommandName="Payment"  CommandArgument='<%# Eval("PatientReg") %>'  runat="server">Due</asp:LinkButton>
                         </ItemTemplate>
                    </asp:TemplateField>
                     
                   <asp:TemplateField  HeaderText="Cancel">
                        <ItemTemplate>
                          <asp:LinkButton ID="LinkButton2"  CommandName="cancel"  CommandArgument='<%# Eval("AppNo") %>'  OnClientClick="return ConfirmationMessage();"   runat="server">Cancel</asp:LinkButton>
                          </ItemTemplate>
                    </asp:TemplateField>  

                       <asp:TemplateField HeaderText="Discharge">
                        <ItemTemplate>
                        <asp:LinkButton ID="LinkButton6"  CommandName="Discharge"  CommandArgument='<%# Eval("PatientReg") %>'  runat="server">Discharge</asp:LinkButton>
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

