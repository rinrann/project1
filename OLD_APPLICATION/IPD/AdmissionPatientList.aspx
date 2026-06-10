<%@ Page Title="" Language="C#" MasterPageFile="~/MasterAll/MasterPageAll.master" AutoEventWireup="true" CodeFile="AdmissionPatientList.aspx.cs" Inherits="IPD_AdmissionPatientList" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">

<script type="text/javascript" language="javascript">
    function showStuff() {
        var el = document.getElementById('disoff');
        if (el.style.display != 'none') {
            el.style.display = 'none';
        }
        else {
            el.style.display = '';
        }
    }

    function SetContextKey() {
        $find('AutoCompleteExtender1').set_contextKey("GFC");
    }

    function autoCompleteEx_ItemSelected(sender, args) {
        var regname = args.get_value().split('-');
        document.getElementById("ctl00_ContentPlaceHolder1_txtname").value = regname[0];
        
    }
</script> 

  <asp:UpdateProgress ID="UpdateProgress1" runat="server">
    <ProgressTemplate>
    
       <div id="aaa" class="progressBackgroundFilter">          </div>
        <div id="bbb" class="processMessage"><img alt="Loading" src="../images/pwait.gif"/>
      
              </div>
    </ProgressTemplate>
    </asp:UpdateProgress>


<asp:UpdatePanel ID="UpdatePanel1" runat="server">

    <ContentTemplate>
        <div class="pageheader">
         <asp:Label ID="Label1" runat="server">Patient Dashboard</asp:Label>
     </div>
     <div class="formbox"  style='width:1080px;'>

        <div class="form-sec">
            <div class="error">
                <strong><asp:Label ID="lblError" runat="server" Font-Bold="True"></asp:Label></strong>
                <div class="clear"></div>
            </div>
            </div>

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
                <label class="pname"><strong>Patient Name :</strong></label>
                     <asp:TextBox ID="txtname" runat="server" CssClass="textbox-medium1" Width="138px" ></asp:TextBox>
                    <cc1:AutoCompleteExtender ServiceMethod="SearchPatientName"    OnClientItemSelected="autoCompleteEx_ItemSelected"    MinimumPrefixLength="1"
           CompletionInterval="100" EnableCaching="false" 
           CompletionSetCount="10" TargetControlID="txtname"  ID="AutoCompleteExtender1" runat="server" FirstRowSelected = "false" ></cc1:AutoCompleteExtender>
                </div>
            </td>

            <td class="style1">
            <div class="form-sec-row"> 
            <asp:Button ID="Button1" runat="server" Text="Search"  CssClass="submit-button"  Height="28px"  onclick="Button1_Click" />
            </div>
            </td>             
                      
            </tr>
            </table>
            </div>
                <div class="formbox"  style='width:1080px;'>
            <div   style='width:100%;'>
            <center>
  <table width="1080px" style='background-color:#FB7B13; color:#FFF;'>
  <tr>
      <td style='width:30px;' align="center">Sl.No</td>
  <td style='width:90px;' align="center">Regtn. No</td>
   <td style='width:150px; padding-left:25px;' align="center">Patient Name</td> 
     <td style='width:70px; padding-left:20px;' align="center">Bed</td>
      <td style='width:90px;padding-left:10px;' align="center">Dr Visit</td>
       <td style='width:90px;padding-left:20px;' align="center">Medicine</td>
        <td style='width:90px;' align="center">Service</td>
      <td style='width:90px;' align="center">Instrument</td>
          <td style='width:90px;'  align="center">Consumable</td>
    <td style='width:90px;' align="center">Checkup</td>
     <td style='width:90px;' align="center">OT Reqn</td>
      <td style='width:90px;' align="center">Lab Reqn</td>
       <td style='width:90px;padding-right:5px;' align="center">Sister/Aya</td>
        <td style='width:90px;padding-right:10px;' align="center">ToDo Task</td>
          </tr>
  </table> 
  </center>
  </div> 
       <div class="listing" style='height:500px; overflow:auto;'>
                              
          <asp:GridView id="GridView1"  CssClass="grid" PagerStyle-CssClass="pgr" 
               DataKeyNames ="PatientReg" runat="server"   SelectedRowStyle-BackColor="GreenYellow"
                 AutoGenerateColumns="False" AllowPaging="True" PageSize="100" 
               onrowcommand="GridView1_RowCommand"  ShowHeader="false"
				 onpageindexchanging="GridView1_PageIndexChanging" 
               onrowdatabound="GridView1_RowDataBound">          
				 <RowStyle HorizontalAlign="Center" />  
                <Columns>
                    <asp:TemplateField HeaderText="Sl.No"   ItemStyle-Width="30px"  ItemStyle-HorizontalAlign="Center"> 
                        <ItemTemplate>
                            <asp:Label ID="lblSlno" runat="server" Width="30px"></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>

                                     <asp:TemplateField HeaderText="Regn No"   ItemStyle-Width="90px"  ItemStyle-HorizontalAlign="Center">                      
                            <ItemTemplate>
                            <asp:LinkButton ID="LinkButton11" CommandName="Select" CommandArgument='<%# Eval("Mix") %>'  runat="server"> 
							<asp:Label ID="lblregno" runat="server" Text='<%# Bind("PatientReg") %>'></asp:Label>
                             </asp:LinkButton>                          
                        </ItemTemplate>
                    </asp:TemplateField>
                     


                    <asp:TemplateField HeaderText="Patient Name"   ItemStyle-Width="150px"  ItemStyle-HorizontalAlign="Center"> 
                        <ItemTemplate>
                            <asp:Label ID="lblname" runat="server" Text='<%# Bind("patient_name") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    
                    <asp:TemplateField HeaderText="Bed No"  ItemStyle-Width="70px" Visible="false"  ItemStyle-HorizontalAlign="Center">
                        <ItemTemplate>                        
                            <asp:Label ID="lbladdress" runat="server" Text='<%# Bind("BedNoText") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField> 
                       
                    <asp:TemplateField HeaderText="Adm. Date"  ItemStyle-Width="70px"  Visible="false" ItemStyle-HorizontalAlign="Center">
                        <ItemTemplate>                        
                            <asp:Label ID="lblphone1" runat="server" Text='<%# Bind("ADate") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                            
                   
                    <asp:TemplateField HeaderText="Transfer"  ItemStyle-Width="70px"  ItemStyle-HorizontalAlign="Center">
                        <ItemTemplate>
                           <asp:LinkButton ID="LinkButton8" CommandName="BedTransfer" CommandArgument='<%# Eval("PatientReg") %>'  runat="server">Transfer</asp:LinkButton>
                            <asp:Label ID="Label2" runat="server" ForeColor="Green"></asp:Label> 
                        </ItemTemplate>
                    </asp:TemplateField> 
                    <asp:TemplateField HeaderText="Dr. Visit" ItemStyle-Width="90px"  ItemStyle-HorizontalAlign="Center">
                        <ItemTemplate>
                            <asp:LinkButton ID="LinkButton1" CommandName="docvisit" CommandArgument='<%# Eval("PatientReg") %>' runat="server">Add Visit</asp:LinkButton>
                                  <asp:Label ID="Label3" runat="server" ForeColor="Green"></asp:Label>                                                    
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Medicine"  ItemStyle-Width="90px"  ItemStyle-HorizontalAlign="Center">
                        <ItemTemplate>
                             <asp:LinkButton ID="LinkButton2"  CommandName="addmedicine" CommandArgument='<%# Eval("PatientReg") %>' runat="server">Medicine </asp:LinkButton>
                                 <asp:Label ID="Label4" runat="server" ForeColor="Green" ></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Services"  ItemStyle-Width="90px"  ItemStyle-HorizontalAlign="Center">
                        <ItemTemplate>
                           <asp:LinkButton ID="LinkButton3" CommandName="addservices" CommandArgument='<%# Eval("PatientReg") %>' runat="server">Services</asp:LinkButton>
                                 <asp:Label ID="Label5" runat="server" ForeColor="Green" ></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Instrument"  ItemStyle-Width="90px"  ItemStyle-HorizontalAlign="Center">
                        <ItemTemplate>
                           <asp:LinkButton ID="LinkButton12" CommandName="addinstrument" CommandArgument='<%# Eval("PatientReg") %>' runat="server">Instrument</asp:LinkButton>
                                 <asp:Label ID="Label12" runat="server" ForeColor="Green" ></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>

                      <asp:TemplateField HeaderText="Consumable"  ItemStyle-Width="90px"  ItemStyle-HorizontalAlign="Center">
                        <ItemTemplate>
                           <asp:LinkButton ID="LinkButton4"  CommandName="addConsumable" CommandArgument='<%# Eval("PatientReg") %>' runat="server">Consumable</asp:LinkButton>
                                <asp:Label ID="Label6" runat="server" ForeColor="Green"></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField> 

                      <asp:TemplateField HeaderText="Checkup"  ItemStyle-Width="90px"  ItemStyle-HorizontalAlign="Center">
                        <ItemTemplate>
                           <asp:LinkButton ID="LinkButton5" CommandName="dailycheckup" CommandArgument='<%# Eval("PatientReg") %>' runat="server">Checkup</asp:LinkButton>
                               <asp:Label ID="Label7" runat="server" ForeColor="Green" ></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField> 

                      <asp:TemplateField HeaderText="OT Reqn"  ItemStyle-Width="90px"  ItemStyle-HorizontalAlign="Center">
                        <ItemTemplate>
                           <asp:LinkButton ID="LinkButton6" CommandName="otreq" CommandArgument='<%# Eval("PatientReg") %>'  runat="server" >OT Reqn</asp:LinkButton> 
                               <asp:Label ID="Label8" runat="server" ForeColor="Green"></asp:Label>
                        </ItemTemplate>
                       </asp:TemplateField>    
                    
                        <asp:TemplateField HeaderText="LAB Reqn" ItemStyle-Width="90px"  ItemStyle-HorizontalAlign="Center">
                        <ItemTemplate>
                           <asp:LinkButton ID="LinkButton7" CommandName="Labrequisition" CommandArgument='<%# Eval("PatientReg") %>'  runat="server">LAB Reqn</asp:LinkButton>
                                 <asp:Label ID="Label9" runat="server" ForeColor="Green"></asp:Label>
                        </ItemTemplate>

                    </asp:TemplateField>  
                    
                     <asp:TemplateField HeaderText="Sister/Aya"  ItemStyle-Width="90px"  ItemStyle-HorizontalAlign="Center">
                        <ItemTemplate>
                            <asp:LinkButton ID="LinkButton10" CommandName="sistecharge" CommandArgument='<%# Eval("PatientReg") %>' runat="server">Sister/Aya</asp:LinkButton>
                                  <asp:Label ID="Label10" runat="server" ForeColor="Green" ></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>   
                    
                        <asp:TemplateField HeaderText="ToDo Task"  ItemStyle-Width="90px"  ItemStyle-HorizontalAlign="Center">
                        <ItemTemplate>
                           <asp:LinkButton ID="LinkButton9" CommandName="ToDoTask" CommandArgument='<%# Eval("PatientReg") %>'  runat="server" >ToDo Task</asp:LinkButton> 
                               <asp:Label ID="Label11" runat="server" ForeColor="Green"></asp:Label>
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
            <br />
               <div id="disoff" style='display:none;width:1080px;height:auto; overflow:auto;' class="formbox">
                         
            <asp:GridView id="GridView2"  CssClass="grid"
                 PagerStyle-CssClass="pgr" DataKeyNames ="patient_name" runat="server"   Width="1080px" SelectedRowStyle-BackColor="GreenYellow"
                 AutoGenerateColumns="False" AllowPaging="True" 
                           PageSize="15" onrowcommand="GridView1_RowCommand" onpageindexchanging="GridView1_PageIndexChanging" 
                              
                            >
                <RowStyle HorizontalAlign="Center" />
                <Columns>
                 
                    <asp:TemplateField HeaderText="Patient Name"  ControlStyle-Width="150px">
                       
                        <ItemTemplate>
                            <asp:Label ID="lblname" runat="server" Text='<%# Bind("patient_name") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    
                    <asp:TemplateField HeaderText="Bed No"  ControlStyle-Width="80px">
                        <ItemTemplate>                        
                            <asp:Label ID="lbladdress" runat="server" Text='<%# Bind("BedNoText") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField> 
                       
                    <asp:TemplateField HeaderText="Adm. Date">
                        <ItemTemplate>                        
                            <asp:Label ID="lblphone1" runat="server" Text='<%# Bind("ADate") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>

                       <asp:TemplateField HeaderText="Diagnosis">
                        <ItemTemplate>                        
                            <asp:Label ID="Diagnosis" runat="server" Text='<%# Bind("DiagnosisName") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>

                          <asp:TemplateField HeaderText="Dr. Name">
                        <ItemTemplate>                        
                            <asp:Label ID="Diagnosis" runat="server" Text='<%# Bind("doc_name") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>

                       <asp:TemplateField HeaderText="Opn. Name">
                        <ItemTemplate>                        
                            <asp:Label ID="OTName" runat="server" Text='<%# Bind("OTName") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>

                       <asp:TemplateField HeaderText="Opn. Date"   ControlStyle-Width="50px">
                        <ItemTemplate>                        
                            <asp:Label ID="OTDate" runat="server" Text='<%# Bind("OTDate") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>


                       <asp:TemplateField HeaderText="Adm.(Days)"   ControlStyle-Width="50px">

                        <ItemTemplate>                        
                            <asp:Label ID="DateDifference" runat="server" Text='<%# Bind("DateDifference") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>

                       <asp:TemplateField HeaderText="Opn.(Days)">
                        <ItemTemplate>                        
                            <asp:Label ID="TotalOT" runat="server" Text='<%# Bind("TotalOT") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>

                       <asp:TemplateField HeaderText="Prob. Dis. Date">
                        <ItemTemplate>                        
                            <asp:Label ID="DischargeDate" runat="server" Text='<%# Bind("DischargeDate") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>

                       <asp:TemplateField HeaderText="Bill Amount">
                        <ItemTemplate>                        
                            <asp:Label ID="lblphone1" runat="server" Text='<%# Bind("TotalBill") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>

                         <asp:TemplateField HeaderText="Due Amount">
                        <ItemTemplate>                        
                            <asp:Label ID="Due" runat="server" Text='<%# Bind("Due") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                                  
                </Columns>
                <PagerStyle CssClass="pgr" />
                <EditRowStyle BackColor="#CCFF33" />
                <AlternatingRowStyle BackColor="#FFDB91" />
        </asp:GridView>
    
   
   </div>
    </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>

