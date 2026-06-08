<%@ Page Title="" Language="C#" MasterPageFile="~/MasterAll/MasterPageAll.master" AutoEventWireup="true" CodeFile="OperationNote.aspx.cs" Inherits="IPD_OperationNote" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">


<script type="text/javascript" language="javascript">
    function ShowDialog() {
        var rtvalue = window.open("RegistrationPopup.aspx", "sss", "Width:800px; Height:550px; dialogLeft:250px;");
        document.getElementById("ctl00_ContentPlaceHolder1_TextBox1").value = rtvalue.NameValue;

    }


    function showStuff(id) {
        var el = document.getElementById(id);
        if (el.style.display != 'none') {
            el.style.display = 'none';
        }
        else {
            el.style.display = '';

        }
    }


    function Calling() {
        var date = new Date();
        $("input[id$='TextBox7']").datepicker({ dateFormat: 'dd/mm/yy', minDate: date });
        $("input[id$='TextBox11']").datepicker({ dateFormat: 'dd/mm/yy', minDate: date });
        $("input[id$='txtProbableDischargeDate']").datepicker({ dateFormat: 'dd/mm/yy', minDate: date });

        $("input[id$='TextBox8']").timepicker({
            showPeriod: true,
            showLeadingZero: true
        });
        $("input[id$='TextBox9']").timepicker({
            showPeriod: true,
            showLeadingZero: true
        });

        $("input[id$='Tab2']").click(function () {


            if ($("input[id$='TextBox1']").val() == '') {
                alert('Please Enter Registration No !');
                $("input[id$='TextBox1']").focus();
                $("input[id$='TextBox1']").addClass('textboxerr');
                return false;
            }
            else {
                $("input[id$='TextBox1']").removeClass('textboxerr');
            }
        });


        $("input[id$='Button1']").click(function () {
            if ($("input[id$='TextBox7']").val() == '') {
                alert('Please Enter Date  !');
                $("input[id$='TextBox7']").focus();
                $("input[id$='TextBox7']").addClass('textboxerr');
                return false;
            }
            else {
                $("input[id$='TextBox7']").removeClass('textboxerr');
            }

            /*if ($("select[id$='DropDownList10']").val() == '0') {
                alert('Please Select Anesthesia Type !');
                $("select[id$='DropDownList10']").focus();
                $("select[id$='DropDownList10']").addClass('textboxerr');
                return false;
            }
            else {
                $("select[id$='DropDownList10']").removeClass('textboxerr');
            }*/
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
                <asp:Label ID="Label1" runat="server">Operation Note</asp:Label>
            </div>

            
    <table width="290px" cellpadding="0" cellspacing="0">
         <tr>
            <td>
                <asp:Button Text="Entry" BorderStyle="None" ID="Tab1" Width="145px"  Height="40px" 
                    CssClass="Initial" runat="server" onclick="Tab1_Click"
                     /></td>
                     <td>
                <asp:Button Text="List" BorderStyle="None" ID="Tab2" CssClass="Initial"  Width="145px"  Height="40px" 
                    runat="server" onclick="Tab2_Click"
                    /></td>
           
                     </tr>
                     </table>


            <div class="formbox">
             <asp:MultiView ID="MainView" runat="server">
                    <asp:View ID="View1" runat="server">
                <div class="form-sec">
                    <div class="error">
                        <strong>
                        <asp:Label ID="lblError" runat="server" Font-Bold="True" CssClass="fadeout"></asp:Label>
                        </strong>
                        <div class="clear">
                        </div>
                    </div>
                    <asp:HiddenField ID="HiddenField1" runat="server" Value="0" /><asp:HiddenField ID="HiddenField2" runat="server" Value="0" />
            
                      <div class="form-sec-row">
                        <label>
                        <strong>
                       Registration No :</strong></label>
                          <asp:TextBox ID="TextBox1" runat="server" CssClass="textbox-medium1" 
                              Enabled="False"></asp:TextBox>
                                <asp:Button ID="Button3"  Height="28px" runat="server" Text="Quick Search" CssClass="submit-buttonCheck" OnClientClick="ShowDialog()"/>
                              <asp:Button ID="Button4" runat="server" Text="Fetch"  Height="28px"
                               CssClass="submit-button" onclick="Button4_Click"/>
                        <div class="clear">
                        </div>
                    </div>

                     <div class="form-sec-row">
                        <label>
                        <strong>
                       Requisition No :</strong></label>
                          <asp:TextBox ID="TextBox2" runat="server" CssClass="textbox-medium1" 
                                Enabled="False"></asp:TextBox>
                        <div class="clear">
                        </div>
                    </div>
                        <div class="form-sec-row">
                        <label>
                        <strong>
                       Patient's Name :</strong></label>
                          <asp:TextBox ID="TextBox3" runat="server" CssClass="textbox-medium1" 
                                Enabled="False"></asp:TextBox>
                        <div class="clear">
                        </div>
                    </div>


                

                        <div class="form-sec-row">
                        <label>
                        <strong>
                       Current Bed No :</strong></label>
                          <asp:TextBox ID="TextBox4" runat="server" CssClass="textbox-medium1" 
                                Enabled="False"></asp:TextBox>
                        <div class="clear">
                        </div>
                    </div>

                      <div class="form-sec-row">
                        <label>
                        <strong>
                       Operation Name :</strong></label>
                          <asp:TextBox ID="TextBox5" runat="server" CssClass="textbox-medium1" 
                                Enabled="False"></asp:TextBox>
                        <div class="clear">
                        </div>
                    </div>


                        <div class="form-sec-row">
                        <label>
                        <strong>
                       Admission Date :</strong></label>
                          <asp:TextBox ID="TextBox6" runat="server" CssClass="textbox-medium1" 
                                Enabled="False"></asp:TextBox>
                                      <asp:Label ID="Label2" runat="server" ForeColor="Green" Text="(DD/MM/YYYY)"></asp:Label>
                        <div class="clear">
                        </div>
                    </div>

                       <div class="form-sec-row">
                        <label>
                        <strong> 
                         Surgeon Name :</strong></label>
                         <div style='float:left;'>
                        <asp:DropDownList ID="DropDownList1" runat="server" CssClass="textbox-medium1"  Height="30px">                               
                         </asp:DropDownList></div>  <div style='padding-left:10px;'>
                  <asp:Button ID="Button5" runat="server" CssClass="submit-button" Text="ADD Dr."  Height="26px" Width="80px" OnClientClick="showStuff('Div1'); return false;"/>
                  </div>
                        <div class="clear">
                        </div>
                    </div>  
                    
                     <div id="Div1" style="display:none;">
                     
                        <div class="form-sec-row">
                        <label>
                        <strong> 
                   Additional Doctor 1  :</strong></label>
                        <asp:DropDownList ID="DropDownList2" runat="server" CssClass="textbox-medium1"  Height="30px">                               
                         </asp:DropDownList>
                        <div class="clear">
                        </div>
                    </div>    
                    
                     <div class="form-sec-row">
                        <label>
                        <strong> 
                        Additional Doctor 2 :</strong></label>
                        <asp:DropDownList ID="DropDownList3" runat="server" CssClass="textbox-medium1"  Height="30px">                               
                         </asp:DropDownList>
                        <div class="clear">
                        </div>
                    </div>     
                    
                    <div class="form-sec-row">
                        <label>
                        <strong> 
                        Additional Doctor 3 :</strong></label>
                        <asp:DropDownList ID="DropDownList4" runat="server" CssClass="textbox-medium1"  Height="30px">                               
                         </asp:DropDownList>
                        <div class="clear">
                        </div>
                    </div>   
                    </div>
                    


                      <div class="form-sec-row">
                        <label>
                        <strong> 
                        Anesthesis Name :</strong></label>
                        <div style='float:left;'>
                        <asp:DropDownList ID="DropDownList5" runat="server" CssClass="textbox-medium1"  Height="30px">                               
                         </asp:DropDownList></div>
                          <div style='padding-left:10px;'>
                  <asp:Button ID="Button6" runat="server" CssClass="submit-button" Text="ADD Ant."  Height="26px" Width="80px" OnClientClick="showStuff('Div2'); return false;"/>
                  </div>
                        <div class="clear">
                        </div>
                    </div> 


                     <div id="Div2" style="display:none;">
                   
                    
                        <div class="form-sec-row">
                        <label>
                        <strong> 
                        Additional Anesthesis :</strong></label>
                        <asp:DropDownList ID="DropDownList6" runat="server" CssClass="textbox-medium1"  Height="30px">                               
                         </asp:DropDownList>
                        <div class="clear">
                        </div>
                    </div>  
                    </div>
                    
                       <div class="form-sec-row">
                        <label>
                        <strong> 
                        Assistant 1  :</strong></label>
                         <div style='float:left;'>
                        <asp:DropDownList ID="DropDownList7" runat="server" CssClass="textbox-medium1"  Height="30px">                               
                         </asp:DropDownList></div>

                         <div style='padding-left:10px;'>
                  <asp:Button ID="Button7" runat="server" CssClass="submit-button" Text="ADD Astn."  Height="26px" Width="80px" OnClientClick="showStuff('Div3'); return false;"/>
                  </div>

                        <div class="clear">
                        </div>
                    </div>  
                    
                     <div id="Div3" style="display:none;">
                     
                        <div class="form-sec-row">
                        <label>
                        <strong> 
                        Assistant 2 :</strong></label>
                        <asp:DropDownList ID="DropDownList8" runat="server" CssClass="textbox-medium1"  Height="30px">                               
                         </asp:DropDownList>
                        <div class="clear">
                        </div>
                    </div>     <div class="form-sec-row">
                        <label>
                        <strong> 
                        Assistant 3 :</strong></label>
                        <asp:DropDownList ID="DropDownList9" runat="server" CssClass="textbox-medium1"  Height="30px">                               
                         </asp:DropDownList>
                        <div class="clear">
                        </div>
                    </div> 
                    </div>  
                     <div class="form-sec-row">
                        <label>
                        <strong> 
                       Operation Date :</strong></label>
                        <asp:TextBox ID="TextBox7" runat="server" CssClass="textbox-medium1"></asp:TextBox>
                         
            <cc1:MaskedEditExtender ID="MaskedEditExtender1" runat="server" TargetControlID="TextBox7"
                                            Mask="99/99/9999" MessageValidatorTip="true" MaskType="Date" DisplayMoney="Left"
                                            AcceptNegative="Left" ErrorTooltipEnabled="True" CultureName="en-GB" />
                              <asp:Label ID="Label3" runat="server" ForeColor="Green" Text="(DD/MM/YYYY)"></asp:Label>
                        <div class="clear">
                        </div>
                    </div>

                    
                     
                     <div class="form-sec-row">
                        <label>
                        <strong> 
                     Start Time :</strong></label>
                        <asp:TextBox ID="TextBox8" runat="server" CssClass="textbox-medium1"></asp:TextBox>
                        <div class="clear">
                        </div>
                    </div>

                          <div class="form-sec-row">
                        <label>
                        <strong> 
                     End Time :</strong></label>
                        <asp:TextBox ID="TextBox9" runat="server" CssClass="textbox-medium1"></asp:TextBox>
                        <div class="clear">
                        </div>
                    </div>
                        
                        
                         <div class="form-sec-row">
                        <label>
                        <strong>
                       Anes Type (Consumable) :</strong></label>
                   <asp:DropDownList ID="DropDownList10" runat="server" CssClass="textbox-medium1"  
                                 Height="30px" AutoPostBack="True" 
                                 onselectedindexchanged="DropDownList10_SelectedIndexChanged">                               
                         </asp:DropDownList>
                        <div class="clear">
                        </div>
                    </div>

                             <div class="form-sec-row">
                        <label>
                        <strong>
                       Anes. Type (Medicine) :</strong></label>
                   <asp:DropDownList ID="DropDownList11" runat="server" CssClass="textbox-medium1"  
                                     Height="30px" Enabled="False">                               
                         </asp:DropDownList>
                        <div class="clear">
                        </div>
                    </div>

                                  <div class="form-sec-row">
                        <label>
                        <strong>
                       Probable Discharge Date :</strong></label>
                          <asp:TextBox ID="txtProbableDischargeDate" runat="server" CssClass="textbox-medium1" ></asp:TextBox>
                        <div class="clear">
                        </div>
                    </div>

                        <div class="form-sec-row">
                        <label>
                        <strong> 
                        Operation Note :</strong></label>
                        <asp:TextBox ID="TextBox10" runat="server" CssClass="textbox-medium1" TextMode="MultiLine" Width="298px" Height="100px"></asp:TextBox>
             <cc1:AutoCompleteExtender ServiceMethod="SearchCustomers"   MinimumPrefixLength="1"    CompletionInterval="100" EnableCaching="false" 
                   CompletionSetCount="10" TargetControlID="TextBox10"  ID="AutoCompleteExtender1" runat="server" FirstRowSelected = "false" ></cc1:AutoCompleteExtender>

                        <div class="clear">
                        </div>
                    </div>
                    <div class="form-sec-row">
                        <div class="clear">
                        </div>
                    </div>
                    <div class="form-sec-row">
                        &nbsp;&nbsp;&nbsp;<label><strong>&nbsp;</strong></label>
                        <asp:Button ID="Button1" runat="server" CssClass="submit-button"  Height="28px" Width="80px"
                            Text="Submit" onclick="Button1_Click" />
                        <asp:Button ID="Button2" runat="server" CssClass="submit-button"  Height="28px" Width="80px"
                            Text="Cancel" onclick="Button2_Click"  />

                                       <asp:Button ID="Button8" runat="server" 
                            CssClass="submit-button"  Height="28px" Width="80px"
                            Text="Lap Note" onclick="Button8_Click"  />
                        <div class="clear">
                        </div>
                    </div>
                
                    <br />
                    
         </div>
         </asp:View>
                    
                    <asp:View ID="View2" runat="server">

                    <div class="listing"  style='width:100%; height:120px; overflow:auto;'>
                <asp:GridView id="GridView2"  Width="100%" CssClass="grid" PagerStyle-CssClass="pgr"
                 DataKeyNames ="OperationReqID" runat="server" 
                 AutoGenerateColumns="False" AllowPaging="True"  PageSize="100"
                 SelectedRowStyle-BackColor="GreenYellow" 
                            onpageindexchanging="GridView2_PageIndexChanging" 
                            onrowcommand="GridView2_RowCommand"  >
                            <RowStyle  HorizontalAlign="Center" />
                    <Columns>
                      <asp:CommandField SelectText="Select" ShowSelectButton="True" />
                  
                        <asp:TemplateField HeaderText="Requisition No" >
                            <ItemTemplate>
                                <asp:Label ID="lblid" runat="server" Text='<%# Bind("OperationReqID") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
             
                        <asp:TemplateField HeaderText="Registration No" Visible="false">
                            <ItemTemplate>
                                <asp:Label ID="lblRegno" runat="server" Text='<%# Bind("PatientRegId") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>


                         <asp:TemplateField HeaderText="Operation Type">
                            <ItemTemplate>
                                <asp:Label ID="lbllname" runat="server" Text='<%# Bind("OperationTypeName") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>

                         <asp:TemplateField HeaderText="Operation Name">
                            <ItemTemplate>
                                <asp:Label ID="lbladate" runat="server" Text='<%# Bind("OperationName") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>

                     </Columns>
                  <PagerStyle CssClass="pgr" />
                    <EditRowStyle BackColor="#CCFF33" />
                    <AlternatingRowStyle BackColor="#FFDB91" />
                </asp:GridView>
            </div>

            <br />
                    <div class="listing"  style='width:100%; height:200px; overflow:auto;'>
                <asp:GridView id="GridView1"  Width="100%" CssClass="grid" PagerStyle-CssClass="pgr"
                 DataKeyNames ="OperationNoteId" runat="server" 
                 AutoGenerateColumns="False" AllowPaging="True" 
                 SelectedRowStyle-BackColor="GreenYellow" 
                    onpageindexchanging="GridView1_PageIndexChanging" 
                    onrowcommand="GridView1_RowCommand" >
                    <Columns>
                        <asp:TemplateField HeaderText="ID" Visible="false">
                            <ItemTemplate>
                                <asp:Label ID="lblid" runat="server" Text='<%# Bind("OperationNoteId") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
             
                        <asp:TemplateField HeaderText="Registration No">
                            <ItemTemplate>
                                <asp:Label ID="lblRegno" runat="server" Text='<%# Bind("PatientRegId") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>


                         <asp:TemplateField HeaderText="Patient's Name">
                            <ItemTemplate>
                                <asp:Label ID="lbllname" runat="server" Text='<%# Bind("patient_name") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>

                          <asp:TemplateField HeaderText="Operation Name">
                            <ItemTemplate>
                                <asp:Label ID="lblopname" runat="server" Text='<%# Bind("OperationName") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>

                         <asp:TemplateField HeaderText="Admission Date">
                            <ItemTemplate>
                                <asp:Label ID="lbladate" runat="server" Text='<%# Bind("adate") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="Bed No">
                            <ItemTemplate>
                                <asp:Label ID="lblbedno" runat="server" Text='<%# Bind("BedNoText") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>

                         <asp:TemplateField HeaderText="Operation Date" >
                            <ItemTemplate>
                                <asp:Label ID="lbldate" runat="server" Text='<%# Bind("opdate") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                             <asp:TemplateField HeaderText="Start Time" >
                            <ItemTemplate>
                                <asp:Label ID="lblstarttime" runat="server" Text='<%# Bind("StartTime") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>   

                             <asp:TemplateField HeaderText="End Time" >
                            <ItemTemplate>
                                <asp:Label ID="lblendtime" runat="server" Text='<%# Bind("EndTime") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>

                         
                     <asp:TemplateField HeaderText="Surgeon ID" Visible="false" >
                            <ItemTemplate>
                                <asp:Label ID="lblsurgeon" runat="server" Text='<%# Bind("SurgeonID") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>   

                             <asp:TemplateField HeaderText="Additional Doctor 1" Visible="false" >
                            <ItemTemplate>
                                <asp:Label ID="lbldoc1" runat="server" Text='<%# Bind("AdditionalDoctor1") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>

                         <asp:TemplateField HeaderText="Additional Doctor2" Visible="false"  >
                            <ItemTemplate>
                                <asp:Label ID="lbldoc2" runat="server" Text='<%# Bind("AdditionalDoctor2") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>

                         <asp:TemplateField HeaderText="Additional Doctor 3" Visible="false" >
                            <ItemTemplate>
                                <asp:Label ID="lbldoc3" runat="server" Text='<%# Bind("AdditionalDoctor3") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                                        

                          <asp:TemplateField HeaderText="Anesthetist Name 1" Visible="false" >
                            <ItemTemplate>
                                <asp:Label ID="lblanesthesis1" runat="server" Text='<%# Bind("AnesthetistName1") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>


                                    <asp:TemplateField HeaderText="Anesthetist Name 2" Visible="false" >
                            <ItemTemplate>
                                <asp:Label ID="lblanesthesis2" runat="server" Text='<%# Bind("AnesthetistName2") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>   

                             <asp:TemplateField HeaderText="Assistant 1" Visible="false" >
                            <ItemTemplate>
                                <asp:Label ID="lblassistant1" runat="server" Text='<%# Bind("Assistant1") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>

                         <asp:TemplateField HeaderText="Assistant 2" Visible="false" >
                            <ItemTemplate>
                                <asp:Label ID="lblassistant2" runat="server" Text='<%# Bind("Assistant2") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>

                         <asp:TemplateField HeaderText="Assistant 3" Visible="false" >
                            <ItemTemplate>
                                <asp:Label ID="lblassistant3" runat="server" Text='<%# Bind("Assistant3") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                    <asp:TemplateField HeaderText="Remarks" Visible="false" >
                            <ItemTemplate>
                                <asp:Label ID="lblremarks" runat="server" Text='<%# Bind("Remarks") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>

 
                    </Columns>
                  <PagerStyle CssClass="pgr" />
                    <EditRowStyle BackColor="#CCFF33" />
                    <AlternatingRowStyle BackColor="#FFDB91" />
                </asp:GridView>
            </div>
               
  </asp:View>
  
                </asp:MultiView>
                </div>
    </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>

