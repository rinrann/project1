<%@ Page Title="" Language="C#" MasterPageFile="~/MasterAll/MasterPageAll.master" AutoEventWireup="true" CodeFile="PatientList.aspx.cs" Inherits="OPD_PatientList" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <script type="text/javascript" language="javascript">

        function Calling() {

            var date = new Date();
            $("input[id$='TextBox3']").datepicker({ dateFormat: 'dd/mm/yy' });
            $("input[id$='TextBox1']").timepicker({
                showPeriod: true,
                showLeadingZero: true
            });

            $("input[id$='TextBox2']").timepicker({
                showPeriod: true,
                showLeadingZero: true
            });
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

<script type="text/javascript">
    function autoCompleteEx_ItemSelected(sender, args) {
        debugger;
        var regname = args.get_value().split('~');
        document.getElementById("ctl00_ContentPlaceHolder1_txtRegNo").value = regname[0];
        document.getElementById("ctl00_ContentPlaceHolder1_txtPtName").value = regname[1];

    }

    function autoCompleteEx_PhoneSelected(sender, args) {
        debugger;
        var regname = args.get_value().split('~');
        document.getElementById("ctl00_ContentPlaceHolder1_txtRegNo").value = regname[0];
        document.getElementById("ctl00_ContentPlaceHolder1_txtPtName").value = regname[1];
        document.getElementById("ctl00_ContentPlaceHolder1_txtPhoneNo").value = regname[2];
    }
    function autoCompleteEx_RegSelected(sender, args) {
        debugger;
        var regname = args.get_value().split('~');
        document.getElementById("ctl00_ContentPlaceHolder1_txtRegNo").value = regname[0];
        document.getElementById("ctl00_ContentPlaceHolder1_txtPtName").value = regname[1];
        
    }
    </script>

<%--For Busy Loader .............................--%>


 
<%--  <asp:UpdateProgress ID="UpdateProgress1" runat="server">
    <ProgressTemplate>
    
       <div id="aaa" class="progressBackgroundFilter">          </div>
        <div id="bbb" class="processMessage"><img alt="Loading" src="../images/pwait.gif"/>
      
              </div>
    </ProgressTemplate>
    </asp:UpdateProgress>--%>


    <%--For Busy Loader End.............................--%>
    <div class="pageheader">
         <asp:Label ID="Label1" runat="server">Patient List</asp:Label>
    </div>
<div class="formbox">
        <div class="form-sec">
            <div class="error">
                <strong><asp:Label ID="lblError" runat="server" Font-Bold="True"></asp:Label></strong>
                <div class="clear"></div>
            </div></div></div>
    <div>

<table cellpadding="0" cellspacing="0"  title="Search" width="100%"> 
            <%--<tr>
                <td colspan="8" align="center">
                    <label class="ipdList" style='width:85px;'><strong>Search By :</strong></label> 
                    <asp:RadioButtonList ID="rbloption" runat="server" RepeatDirection="Horizontal" >                      
                        <asp:ListItem Selected="True" Value="1">Date</asp:ListItem>
                        <asp:ListItem Value="2">Reg No</asp:ListItem>
                        <asp:ListItem Value="3">Patient Name</asp:ListItem>
                        <asp:ListItem Value="4">Phone No</asp:ListItem>
                    </asp:RadioButtonList>
                </td>
            </tr>--%>
            <tr>
                <td  style="width:10%;">
                   <%--<label class="ipdList" style='width:85px;'><strong>Doctor :</strong></label> --%>
                    <label class="ipdList" style='width:75px;'><strong>Date :</strong></label>
                </td>
                <td style="width:10%;">
                    <%--<asp:DropDownList ID="DropDownList1" runat="server" CssClass="textbox-medium1" Width="95px" Visible="false"></asp:DropDownList>--%>
                    <asp:TextBox ID="TextBox3" runat="server" CssClass="textbox-medium1" Width="138px" TextMode="Date"></asp:TextBox>
                </td>
                <td style="width:10%;">
                    <label class="ipdList" style='width:75px;'><strong>Reg No :</strong></label>
                </td>
                <td style="width:30%;">
                    <asp:TextBox ID="txtRegNo" CssClass="textbox-medium1"  runat="server"  style="width:100%;" ></asp:TextBox>
                    
                    <cc1:AutoCompleteExtender ServiceMethod="SearchByRegNo"    OnClientItemSelected="autoCompleteEx_RegSelected"    MinimumPrefixLength="1"
                        CompletionInterval="100" EnableCaching="false" CompletionSetCount="10" TargetControlID="txtRegNo"  ID="AutoCompleteExtender3" runat="server" 
                       FirstRowSelected = "false" >
                    </cc1:AutoCompleteExtender>
          
                </td>
                <td style="width:10%;">
                    <label class="ipdList" style='width:75px;'><strong>Patient Name :</strong></label>
                </td>
                <td style="width:30%;">
                    
                    <asp:TextBox ID="txtPtName" CssClass="textbox-medium1"  runat="server"  style="width:100%;"></asp:TextBox>
                   <cc1:AutoCompleteExtender ServiceMethod="SearchByPatientName"    OnClientItemSelected="autoCompleteEx_ItemSelected"    MinimumPrefixLength="1"
                        CompletionInterval="100" EnableCaching="false" CompletionSetCount="10" TargetControlID="txtPtName"  ID="AutoCompleteExtender1" runat="server" 
                       FirstRowSelected = "false" >
                    </cc1:AutoCompleteExtender>
          
                </td>
                <td style="width:10%;">
                       <label class="ipdList" style='width:75px;'><strong>Phone No :</strong></label>
                </td>
                <td style="width:10%;">
                    <asp:TextBox ID="txtPhoneNo" runat="server" CssClass="textbox-medium1" Width="138px" ></asp:TextBox>
                    <cc1:AutoCompleteExtender ServiceMethod="SearchByPhoneNo"    OnClientItemSelected="autoCompleteEx_PhoneSelected"    MinimumPrefixLength="1"
                        CompletionInterval="100" EnableCaching="false" CompletionSetCount="10" TargetControlID="txtPhoneNo"  ID="AutoCompleteExtender2" runat="server" 
                       FirstRowSelected = "false" >
                    </cc1:AutoCompleteExtender>
                </td>
                
                <td style="width:10%;">
                    &nbsp;
                </td>
                <td style="width:10%;">
                        <div class="form-sec-row"> 
                           <%--<asp:Button ID="Button1" runat="server" Text="Search"  CssClass="submit-button1"  Height="28px"onclick="Button1_Click" />--%>
                            <asp:Button ID="Button1" runat="server" Text="Search" CssClass="submit-button1"  Height="28px" onclick="Button1_Click"/>
                        </div>                  
                </td>             
                      
            </tr>

      
            <%--<tr style='height:55px;'>
                <td>
                    
            <asp:Label ID="Label6" runat="server" Font-Size="Medium" Font-Bold="True" Text="Total  :" Width ="75px" 
                        ForeColor="Red"></asp:Label>  </td>
             <td>
             
                 <asp:Label ID="Label2" runat="server" Font-Size="Medium" Text="Label" Font-Bold="True"  Width ="95px" 
                     ForeColor="Red"></asp:Label>     
</td>
                <td>
             <asp:Label ID="Label7" runat="server" Font-Size="Medium" Font-Bold="True" Text="Checked  :" Width ="95px" 
                        ForeColor="Red"></asp:Label>
             </td>
             <td>
            <asp:Label ID="Label3" runat="server" Font-Size="Medium" Text="Label" Font-Bold="True"  Width ="95px" ForeColor="Red"></asp:Label>     
          
</td>
  <td align="center">
                       <div class="form-sec-row"> 
             <asp:Label ID="Label8" runat="server" Font-Bold="True" Font-Size="Medium" Text="Waiting  :" Width ="95px" 
                               ForeColor="Red"></asp:Label>
            </div>  
</td>
<td>  <asp:Label ID="Label4" runat="server" Font-Bold="True" Font-Size="Medium" Text="Label" Width ="95px" 
        ForeColor="Red"></asp:Label>     
</td>
                <td>
                             <div class="form-sec-row"> 
             <label class="pname"><strong></strong></label> 
            </div>                  
</td>
<td>         <asp:UpdatePanel id="updPnl" 
                runat="server" UpdateMode="Conditional">
                <ContentTemplate>
                <asp:Label ID="Label5" runat="server" Font-Size="Medium" Text="" Width ="95px"  Font-Bold="True" 
        ForeColor="Red"></asp:Label> 
                </ContentTemplate>
                <Triggers>
                <asp:AsyncPostBackTrigger ControlID="timer1" EventName ="tick" />
                </Triggers>
                </asp:UpdatePanel>   
              </td>

                <td>
                             <div class="form-sec-row"> 
                                         <asp:Timer ID="timer1" runat="server" Interval="1000" OnTick="timer1_tick" 
                                             Enabled="False"></asp:Timer>
            </div>                  
</td>             
                      
            </tr>--%>
            
           <tr>
                <td colspan="10" align="center">
                    <div class="listing"    style='width:100%; height:300px; overflow:auto;'>
            <asp:GridView id="GridView1"  CssClass="grid" Width="100%"
                 PagerStyle-CssClass="pgr" DataKeyNames ="AppoNo" runat="server"   SelectedRowStyle-BackColor="GreenYellow"
                 AutoGenerateColumns="False" AllowPaging="True" 
                           PageSize="100" onrowcommand="GridView1_RowCommand" 
                            onpageindexchanging="GridView1_PageIndexChanging" 
                            onrowdatabound="GridView1_RowDataBound" onselectedindexchanged="GridView1_SelectedIndexChanged" 
                              
                            >
                <RowStyle HorizontalAlign="Center" />
                <Columns>
                
                    <asp:TemplateField HeaderText="Sl.No"   ItemStyle-Width="30px"  ItemStyle-HorizontalAlign="Center"> 
                        <ItemTemplate>
                            <asp:Label ID="lblSlno" runat="server" Width="30px"></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="APPOINTMENT NO" Visible="false">
                       
                        <ItemTemplate>
                            <asp:Label ID="lblappono" runat="server" Text='<%# Bind("AppoNo") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                  
                   <asp:TemplateField HeaderText="REG NO" ItemStyle-HorizontalAlign="Left">
                       
                        <ItemTemplate>
                            <asp:Label ID="lblregno" runat="server" Text='<%# Bind("PatientRegNo") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="PATIENT NAME" ItemStyle-HorizontalAlign="Left">
                       
                        <ItemTemplate>
                            <asp:Label ID="lblname" runat="server" Text='<%# Bind("PName") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                        <asp:TemplateField HeaderText="ADDRESS">
                       
                        <ItemTemplate>
                            <asp:Label ID="lbladdress" runat="server" Text='<%# Bind("Address") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="DISTRICT">
                        <ItemTemplate>                        
                            <asp:Label ID="lbldistrict" runat="server" Text='<%# Bind("DistrictName") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField> 
                       
                    <%--<asp:TemplateField HeaderText="TYPE">
                        <ItemTemplate>                        
                            <asp:Label ID="lbltype" runat="server" Text='<%# Bind("TypeName") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>--%>

                      <asp:TemplateField HeaderText="APPO TYPE" Visible ="false">
                        <ItemTemplate>                        
                            <asp:Label ID="lblappotype" runat="server" Text='<%# Bind("AppoType") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                            
                    <asp:TemplateField HeaderText="ENTRY DATE">
                       
                        <ItemTemplate>
                            <asp:Label ID="lbldate" runat="server" Text='<%# Bind("appdate") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="ENTRY TIME" Visible="false">
                        <ItemTemplate>                        
                            <asp:Label ID="lbltime" runat="server" Text='<%# Bind("AppointmentTime") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>   
                    <asp:TemplateField HeaderText="">
                        <ItemTemplate>
                             <asp:LinkButton ID="LinkButton4"  CommandName="OPD" CommandArgument='<%# Eval("PatientRegNo") %>' runat="server">OPD</asp:LinkButton>
                        </ItemTemplate>
                    </asp:TemplateField>    
                    <asp:TemplateField HeaderText="">
                        <ItemTemplate>
                             <asp:LinkButton ID="LinkButton3"  CommandName="Diagnostic" CommandArgument='<%# Eval("PatientRegNo") %>' runat="server">Diagnostic</asp:LinkButton>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="">
                        <ItemTemplate>
                             <asp:LinkButton ID="LinkButton14"  CommandName="Procedure" CommandArgument='<%# Eval("PatientRegNo") %>' runat="server">Procedure</asp:LinkButton>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="">
                        <ItemTemplate>
                             <asp:LinkButton ID="LinkButton15"  CommandName="Infertility" CommandArgument='<%# Eval("PatientRegNo") %>' runat="server">Infertility</asp:LinkButton>
                        </ItemTemplate>
                    </asp:TemplateField> 
                    <asp:TemplateField HeaderText="" Visible="false">
                        <ItemTemplate>
                             <asp:LinkButton ID="LinkButton2"  CommandName="select" CommandArgument='<%# Eval("PatientRegNo") %>' runat="server">Select</asp:LinkButton>
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
</asp:Content>

