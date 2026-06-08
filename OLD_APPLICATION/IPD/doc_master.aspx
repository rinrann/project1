<%@ Page Language="C#" MasterPageFile="~/MasterAll/MasterPageAll.master" AutoEventWireup="true" CodeFile="doc_master.aspx.cs" Inherits="Master_doc_master" Title="Untitled Page" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content2" ContentPlaceHolderID="head" Runat="Server">     
</asp:Content>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
            <script language="javascript" type="text/javascript">
                function isNumberKey(evt) {
                  var charCode = (evt.which) ? evt.which : evt.keyCode
                    if (charCode > 31 && (charCode < 48 || charCode > 57))
                        return false;
                    return true;
                }
         function DisableBackButton() {
             window.history.forward()
         }
         DisableBackButton();
         window.onload = DisableBackButton;
         window.onpageshow = function (evt) { if (evt.persisted) DisableBackButton() }
         window.onunload = function () { void (0) }

         function Calling() {

             $("input[id$='txtInTime']").timepicker({
                 showPeriod: true,
                 showLeadingZero: true
             });

             $("input[id$='txtOutTime']").timepicker({
                 showPeriod: true,
                 showLeadingZero: true
             });

             $("input[id$='Button1']").click(function () {

                 if ($("textarea[id$='TextBox13']").val() > '100') {
                     alert('Commission must be between 1 to 100 !');
                     $("input[id$='TextBox13']").addClass('textboxerr');
                     $("textarea[id$='TextBox13']").focus();
                     return false;
                 }
                 else {
                     $("textarea[id$='TextBox13']").removeClass('textboxerr');
                 }

                 if ($("input[id$='TextBox1']").val() == '') {
                     alert('Please Enter Doctor Name!');
                     $("input[id$='TextBox1']").focus();
                     $("input[id$='TextBox1']").addClass('textboxerr');
                     return false;
                 }
                 else {
                     $("input[id$='TextBox1']").removeClass('textboxerr');
                 }


                 if ($("select[id$='DropDownList1']").val() == '0') {
                     alert('Please Select Doctor Type!');
                     $("select[id$='DropDownList1']").focus();
                     $("select[id$='DropDownList1']").addClass('textboxerr');
                     return false;
                 }
                 else {
                     $("select[id$='DropDownList1']").removeClass('textboxerr');
                 }
             

                 if ($("textarea[id$='TextBox2']").val() == '') {
                     alert('Please Enter Address Properly!');
                     $("input[id$='TextBox2']").addClass('textboxerr');
                     $("textarea[id$='TextBox2']").focus();
                     return false;
                 }
                 else {
                     $("textarea[id$='TextBox2']").removeClass('textboxerr');
                 }

                 if ($("select[id$='DropDownList2']").val() == '0') {
                     alert('Please select Country Properly!');
                     $("select[id$='DropDownList2']").focus();
                     $("select[id$='DropDownList2']").addClass('textboxerr');
                     return false;
                 }
                 else {
                     $("select[id$='DropDownList2']").removeClass('textboxerr');
                 }

                 if ($("select[id$='DropDownList3']").val() == '0') {
                     alert('Please select State!');
                     $("select[id$='DropDownList3']").focus();
                     $("select[id$='DropDownList3']").addClass('textboxerr');
                     return false;
                 }
                 else {
                     $("select[id$='DropDownList3']").removeClass('textboxerr');
                 }


            

                 if ($("input[id$='TextBox5']").val() == '') {
                     alert('Please Enter Qualification!');
                     $("input[id$='TextBox5']").focus();
                     $("input[id$='TextBox5']").addClass('textboxerr');
                     return false;
                 }

                 else {
                     $("input[id$='TextBox5']").removeClass('textboxerr');
                 }

                 if ($("input[id$='TextBox7']").val() == '') {
                     alert('Please Enter Phone Properly!');
                     $("input[id$='TextBox7']").focus();
                     $("input[id$='TextBox7']").addClass('textboxerr');
                     return false;
                 }

                 else if ($("input[id$='TextBox7']").val().length < '10') {
                     alert('Invalid Phone!');
                     $("input[id$='TextBox7']").focus();
                     $("input[id$='TextBox7']").addClass('textboxerr');
                     return false;
                 }
                 else {
                     $("input[id$='TextBox7']").removeClass('textboxerr');
                 }

          
             });
             $("input[id$='TextBox4']").keydown(function (event) {
                 if (event.keyCode == 46 || event.keyCode == 8 || event.keyCode == 9 || event.keyCode == 27 || event.keyCode == 13 ||
                    (event.keyCode == 65 && event.ctrlKey === true) || (event.keyCode >= 35 && event.keyCode <= 39)) {
                     return;
                 }
                 else {
                     if (event.shiftKey || (event.keyCode < 48 || event.keyCode > 57) && (event.keyCode < 96 || event.keyCode > 105)) {
                         event.preventDefault();
                     }
                 }
             });

             $("input[id$='TextBox16']").keydown(function (event) {
                 if (event.keyCode == 46 || event.keyCode == 8 || event.keyCode == 9 || event.keyCode == 27 || event.keyCode == 13 ||
                    (event.keyCode == 65 && event.ctrlKey === true) || (event.keyCode >= 35 && event.keyCode <= 39)) {
                     return;
                 }
                 else {
                     if (event.shiftKey || (event.keyCode < 48 || event.keyCode > 57) && (event.keyCode < 96 || event.keyCode > 105)) {
                         event.preventDefault();
                     }
                 }
             });

             



             $("input[id$='TextBox13']").keydown(function (event) {
                 if (event.keyCode == 46 || event.keyCode == 8 || event.keyCode == 9 || event.keyCode == 27 || event.keyCode == 13 ||
                    (event.keyCode == 65 && event.ctrlKey === true) || (event.keyCode >= 35 && event.keyCode <= 39)) {
                     return;
                 }
                 else {
                     if (event.shiftKey || (event.keyCode < 48 || event.keyCode > 57) && (event.keyCode < 96 || event.keyCode > 105)) {
                         event.preventDefault();
                     }
                 }
             });



             $("input[id$='TextBox14']").keydown(function (event) {
                 if (event.keyCode == 46 || event.keyCode == 8 || event.keyCode == 9 || event.keyCode == 27 || event.keyCode == 13 ||
                    (event.keyCode == 65 && event.ctrlKey === true) || (event.keyCode >= 35 && event.keyCode <= 39)) {
                     return;
                 }
                 else {
                     if (event.shiftKey || (event.keyCode < 48 || event.keyCode > 57) && (event.keyCode < 96 || event.keyCode > 105)) {
                         event.preventDefault();
                     }
                 }
             });



             $("input[id$='TextBox7']").keydown(function (event) {
                 if (event.keyCode == 46 || event.keyCode == 8 || event.keyCode == 9 || event.keyCode == 27 || event.keyCode == 13 ||
                    (event.keyCode == 65 && event.ctrlKey === true) || (event.keyCode >= 35 && event.keyCode <= 39)) {
                     return;
                 }
                 else {
                     if (event.shiftKey || (event.keyCode < 48 || event.keyCode > 57) && (event.keyCode < 96 || event.keyCode > 105)) {
                         event.preventDefault();
                     }
                 }
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

         function SetContextKey() {
             $find('AutoCompleteExtender1').set_contextKey("GFC");
         }

         function autoCompleteEx_ItemSelected(sender, args) {

             var regname = args.get_value().split('~');// alert(regname[0]);
             //document.getElementById("ctl00_ContentPlaceHolder1_TextBoxicode").value = regname[1];
             document.getElementById("ctl00_ContentPlaceHolder1_TextBox1").value = regname[0];

             $("#TextBox1").focus();
             //$("#DropDownList4").val(regname[1]);
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
         <asp:Label ID="Label1" runat="server">Doctor Master</asp:Label>
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
                <strong><asp:Label ID="lblError" runat="server" Font-Bold="True"></asp:Label></strong>
                <div class="clear"></div>
            </div>
                <div class="form-sec-row">
                <label><strong>Doctor Type :<span style="color:red;">*</span></strong></label>
                    <asp:DropDownList ID="DropDownList1" runat="server" CssClass="textbox-medium1">
                    </asp:DropDownList>
                <div class="clear">
                </div>
                
            </div>
            <div class="form-sec-row">
                <label><strong>Doctor Name :<span style="color:red;">*</span></strong></label>
                <asp:TextBox ID="TextBox20" runat="server" CssClass="textbox-medium1" 
                    Text="Dr." Enabled="False" Width="24px"></asp:TextBox>
                <asp:TextBox ID="TextBox1" runat="server" CssClass="textbox-medium1" 
                    Width="272px"></asp:TextBox>
                <cc1:AutoCompleteExtender ServiceMethod="Searchdoc" OnClientItemSelected="autoCompleteEx_ItemSelected" MinimumPrefixLength="1"
                                    CompletionInterval="100" EnableCaching="false" CompletionSetCount="10" TargetControlID="TextBox1" ID="AutoCompleteExtender2" runat="server" 
                                       FirstRowSelected = "false" ></cc1:AutoCompleteExtender>
                <div class="clear"></div>
            </div>


             <div class="form-sec-row">
                <label><strong>Dr. Registratin No :</strong></label>
                <asp:TextBox ID="TextBox15" runat="server" CssClass="textbox-medium1" ></asp:TextBox>
                <div class="clear"></div>
            </div>
            <div class="form-sec-row">
                <label><strong>Consultant :</strong></label>
                <asp:CheckBox ID="chkCon" runat="server" />
                <div class="clear"></div>
            </div>
		    <div class="form-sec-row">
                <label><strong>Address :<span style="color:red;">*</span></strong></label>
               <asp:TextBox ID="TextBox2" runat="server" CssClass="textarea1" 
                    TextMode="MultiLine" Columns="4" Rows="3" Width="295px" ></asp:TextBox>
                <div class="clear">
                </div>
                
            </div>
            
            <div class="form-sec-row">
                <label><strong>Country :<span style="color:red;">*</span></strong></label>
                <asp:DropDownList ID="DropDownList2" runat="server" CssClass="combo-big1" AutoPostBack="true" OnSelectedIndexChanged="DropDownList2_SelectedIndexChanged">
                    <asp:ListItem>INDIA</asp:ListItem>
                    <asp:ListItem>USA</asp:ListItem>
                    <asp:ListItem>UK</asp:ListItem>
                    <asp:ListItem>AUSTRALIA</asp:ListItem>
                </asp:DropDownList>
                <div class="clear"></div>
            </div>
              <div class="form-sec-row">
                <label><strong>State :<span style="color:red;">*</span></strong></label>
                  <asp:DropDownList ID="DropDownList3" runat="server" CssClass="textbox-medium1">
                   
                  </asp:DropDownList>
                <div class="clear"></div>
            </div>
              <div class="form-sec-row">
                <label><strong>City :</strong></label>
                <asp:TextBox ID="TextBox3" runat="server" CssClass="textbox-medium1" ></asp:TextBox>
                <div class="clear"></div>
            </div>
            <div class="form-sec-row">
                <label><strong>Pin :</strong></label>
                <asp:TextBox ID="TextBox4" runat="server" MaxLength="6"  CssClass="textbox-medium1" ></asp:TextBox>
                <div class="clear"></div>
            </div>
            
  
            
            <div class="form-sec-row">
                <label><strong>Qualification :<span style="color:red;">*</span></strong></label>
                <asp:TextBox ID="TextBox5" runat="server" CssClass="textbox-medium1" ></asp:TextBox>
                <div class="clear"></div>
            </div>
                   <div class="form-sec-row">
                <label><strong>Mobile No :<span style="color:red;">*</span></strong></label>
                  <asp:TextBox ID="TextBox6" runat="server" CssClass="textbox-medium1" Width="50" Enabled="False">+91</asp:TextBox>
                <asp:TextBox ID="TextBox7" runat="server" CssClass="textbox-medium1" 
                    MaxLength="10" Width="246px" ></asp:TextBox>
                <div class="clear"></div>
            </div>
            
            <div class="form-sec-row">
                <label><strong>Residential Phone :</strong></label>
                 <asp:TextBox ID="TextBox8" runat="server" CssClass="textbox-medium1" Width="50" Enabled="false">+91</asp:TextBox>
                    <asp:TextBox ID="TextBox9" runat="server" CssClass="textbox-medium1" MaxLength="4" Width="50"></asp:TextBox>
                    <asp:TextBox ID="TextBox10" runat="server" CssClass="textbox-medium1" MaxLength="8" Width="193"></asp:TextBox>
                    <div class="clear"></div></div>
                    
                <div class="form-sec-row">
                    <label><strong>Email ID :</strong></label>
                    <asp:TextBox ID="TextBox11" runat="server" CssClass="textbox-medium1" Enabled="true"></asp:TextBox>
                    <div class="clear"></div>
                </div>
            <div class="form-sec-row" style="display:none;">
                <label><strong>Department :</strong></label>
                <asp:DropDownList ID="ddlDept" runat="server" CssClass="textbox-medium1">
                </asp:DropDownList>
                <div class="clear"></div>
            </div>
                         
            <div class="form-sec-row"  style="display:none;">
                <label><strong>Fax :</strong></label>
                <asp:TextBox ID="TextBox12" runat="server" CssClass="textbox-medium1" ></asp:TextBox>
                <div class="clear"></div>
            </div>
              <div class="form-sec-row"  style="display:none;">
                <label><strong>Speciality 1:</strong></label>
                  <asp:DropDownList ID="DropDownList4" runat="server" CssClass="textbox-medium1">
                  </asp:DropDownList>
                <div class="clear"></div>
            </div>

               <div class="form-sec-row"  style="display:none;">
                <label><strong>Speciality 2:</strong></label>
                  <asp:DropDownList ID="DropDownList5" runat="server" CssClass="textbox-medium1">
                  </asp:DropDownList>
                <div class="clear"></div>
            </div>


               <div class="form-sec-row"  style="display:none;">
                <label><strong>Speciality 3:</strong></label>
                  <asp:DropDownList ID="DropDownList6" runat="server" CssClass="textbox-medium1">
                  </asp:DropDownList>
                <div class="clear"></div>
            </div>
            
   
            
     
                
                <div class="form-sec-row" style="display:none;">
                    <label><strong>Commission(%) :</strong></label>
                    <asp:TextBox ID="TextBox13" runat="server" MaxLength="3"     CssClass="textbox-medium1" ></asp:TextBox>
                    <div class="clear"></div>
                </div>
                
                 <div class="form-sec-row" style="display:none;">
                    <label><strong>Commission(Fixed) :</strong></label>
                    <asp:TextBox ID="TextBox14" runat="server" CssClass="textbox-medium1" ></asp:TextBox>
                    <div class="clear"></div>
                </div>

                   <div class="form-sec-row">
                    <label><strong>Doctor Fees :</strong></label>
                    <asp:TextBox ID="TextBox16" runat="server" onkeypress="return isNumberKey(event)"  CssClass="textbox-medium1" ></asp:TextBox>
                      <%--<span   id="error" style="color: Red; display: none">* Input digits (0 - 9)</span>--%>
                    <div class="clear"></div>
                </div>

            <div class="form-sec-row" style="display:none;">
                    <label><strong>Doctor Fees(Night) :</strong></label>
                    <asp:TextBox ID="TextBox17" runat="server" onkeypress="return isNumberKey(event)"  CssClass="textbox-medium1" ></asp:TextBox>
                      <%--<span   id="error" style="color: Red; display: none">* Input digits (0 - 9)</span>--%>
                    <div class="clear"></div>
                </div>
                   <div class="form-sec-row" style="display:none;">
                    <label><strong>Day Visit Charge :</strong></label>
                    <asp:TextBox ID="txtDoctorVisitCharge" runat="server" onkeypress="return isNumberKey(event)"  CssClass="textbox-medium1" ></asp:TextBox>
                 <%--   <span  id="Span1" style="color: Red; display: none">* Input digits (0 - 9)</span>--%>
                    <div class="clear"></div>
                </div>
            <div class="form-sec-row" style="display:none;">
                    <label><strong>Night Visit Charge :</strong></label>
                    <asp:TextBox ID="txtNightVisitCharge" runat="server" onkeypress="return isNumberKey(event)"  CssClass="textbox-medium1" ></asp:TextBox>
                 <%--   <span  id="Span1" style="color: Red; display: none">* Input digits (0 - 9)</span>--%>
                    <div class="clear"></div>
                </div>
      <div class="form-sec-row"  style="display:none;">
                <asp:gridview ID="Gridview3" runat="server" CssClass="grid" 
                    PagerStyle-CssClass="pgr" ShowFooter="true" AutoGenerateColumns="false"  Width="100%">
                    <Columns>
                        <asp:TemplateField HeaderText="Visiting Days">
                            <ItemTemplate>
                                <asp:DropDownList ID="ddlVisitingDays" runat="server">
                                    <asp:ListItem Value="0">--Select--</asp:ListItem>    
                                    <asp:ListItem Value="1">Sunday</asp:ListItem>
                                    <asp:ListItem Value="2">Monday</asp:ListItem>
                                    <asp:ListItem Value="3">Tuesday</asp:ListItem>
                                    <asp:ListItem Value="4">Wednesday</asp:ListItem>
                                    <asp:ListItem Value="5">Thrusday</asp:ListItem>
                                    <asp:ListItem Value="6">Friday</asp:ListItem>
                                    <asp:ListItem Value="7">Saturday</asp:ListItem>
                                </asp:DropDownList>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="In Time" ItemStyle-HorizontalAlign="Center">
                            <ItemTemplate>
                                <asp:TextBox ID="txtInTime" runat="server"></asp:TextBox>                                
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Out Time">
                            <ItemTemplate>
                                 <asp:TextBox ID="txtOutTime"  runat="server"></asp:TextBox>                                 
                            </ItemTemplate>
                            <FooterStyle HorizontalAlign="Right" />
                            <FooterTemplate>
                                 <asp:Button ID="ButtonAdd" runat="server" Text="Add New Row" onclick="ButtonAdd_Click" />
                            </FooterTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField>
                            <ItemTemplate>
                                <asp:LinkButton ID="LinkButton1" runat="server" onclick="LinkButton1_Click">Remove</asp:LinkButton>
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                    <PagerStyle CssClass="pgr"></PagerStyle>
                </asp:gridview>
            </div>
           
            <div class="form-sec-row">
                <label><strong>&nbsp;</strong></label>
                <asp:Button ID="Button1" runat="server" OnClick="Button1_Click" Text="Submit" 
                    CssClass="submit-button" Height="28px" />
                <asp:Button ID="Button2" runat="server" Text="Cancel" OnClick="Button2_Click1" 
                    CssClass="submit-button" Height="28px" />
                <div class="clear"></div>
            </div>   <asp:HiddenField ID="HiddenField1" runat="server" />
            <asp:HiddenField ID="HiddenField2" runat="server" />
                         
            <div class="clear"></div>
        </div>
       </asp:View>
                  <asp:View ID="View2" runat="server">

                       <div style='width:100%;'>
  <table width="100%" style='background-color:#FB7B13; color:#FFF;'>
  <tr style='border:1px solid black;'> 
   <td style='width:90px;' align="center">Doctor Id</td>
     <td style='width:90px;' align="center">Regn No</td>
   <td style='width:110px;'  align="center">Doctor Name</td>
    <td style='width:90px; ' align="center">City</td>
     <td style='width:65px; ' align="center">Phone</td>  
        <td style='width:65px;display:none;' align="center">Comm(%)</td>
               <td style='width:65px;display:none;' align="center">Comm(Rs)</td>
      <td style='width:65px; ' align="center">Fees</td>  
        <td style='width:70px;' align="center">Edit</td>
          <td style='width:70px;' align="center" id="coldel" runat="server">Delete</td>
          </tr>
  </table> 
  </div> 

<div class="listing"  style='width:100%; height:400px; overflow:auto;'>
        <asp:GridView id="GridView1" CssClass="grid" PagerStyle-CssClass="pgr" DataKeyNames ="doc_id,doc_name" runat="server" 
        AutoGenerateColumns="False" AllowPaging="True" PageSize ="100" OnRowCommand="GridView1_RowCommand" OnRowDataBound="GridView1_RowDataBound" Width="100%"
         OnRowDeleting="GridView1_RowDeleting"   ShowHeader="false"
        OnPageIndexChanging="GridView1_PageIndexChanging" AllowSorting="True" OnSorting="GridView1_Sorting">
            <Columns>
                <asp:TemplateField HeaderText="Doctor Id" ItemStyle-Width="90px"  ItemStyle-HorizontalAlign="left"  >
                    <ItemTemplate>
                        &nbsp;<asp:Label ID="lblDoctorId" runat="server" Text='<%# Bind("doc_id") %>'></asp:Label>
                        <asp:Label ID="lblDeptCode" runat="server" Text='<%# Bind("DeptCode") %>' Visible="false"></asp:Label>
                        <asp:Label ID="lblConsultant" runat="server" Text='<%# Bind("consultant") %>' Visible="false"></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>


                    <asp:TemplateField HeaderText="Registration No" ItemStyle-Width="90px"  ItemStyle-HorizontalAlign="left"  >
                    <ItemTemplate>
                        &nbsp;<asp:Label ID="lbldrregno" runat="server" Text='<%# Bind("DrRegNo") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>


                <asp:TemplateField HeaderText="Type" Visible="false">
                    <ItemTemplate>
                        &nbsp;<asp:Label ID="lbltype" runat="server" Text='<%# Bind("DocTypeId") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Doctor Name"  ItemStyle-Width="110px"  ItemStyle-HorizontalAlign="left"  >
                   <ItemTemplate>
                        <asp:Label ID="lblDoctorName" runat="server" Text='<%# Bind("dname") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Address" Visible="false">
                   <ItemTemplate>
                        <asp:Label ID="lblAddress" runat="server" Text='<%# Bind("Address") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Country" Visible="false">
                    <ItemTemplate>
                        <asp:Label ID="lblCountry" runat="server" Text='<%# Bind("Country") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                   <asp:TemplateField HeaderText="State" Visible="false">
                    <ItemTemplate>
                        <asp:Label ID="lblstate" runat="server" Text='<%# Bind("Stateid") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="City" ItemStyle-Width="90px"  ItemStyle-HorizontalAlign="left"  >
                    <ItemTemplate>
                        <asp:Label ID="lblCity" runat="server" Text='<%# Bind("City") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Pin" Visible="false">
                    <ItemTemplate>
                        <asp:Label ID="lblPin" runat="server" Text='<%# Bind("Pin") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Phone" ItemStyle-Width="65px"  ItemStyle-HorizontalAlign="left"  >
                    <ItemTemplate>
                        &nbsp;<asp:Label ID="lblPhone" runat="server" Text='<%# Bind("Phone") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>

                        <asp:TemplateField HeaderText="Fax" Visible="false">
                    <ItemTemplate>
                        &nbsp;<asp:Label ID="lblFax" runat="server" Text='<%# Bind("Fax") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Qualification" Visible="false">
                    <ItemTemplate>
                        <asp:Label ID="lblqualification" runat="server" Text='<%# Bind("Qualification") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                  <asp:TemplateField HeaderText="Speciality" Visible="false" >
                    <ItemTemplate>
                        <asp:Label ID="lblSpeciality1" runat="server" Text='<%# Bind("SpecialistIn1") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                        <asp:TemplateField HeaderText="Speciality" Visible="false">
                    <ItemTemplate>
                        <asp:Label ID="lblSpeciality2" runat="server" Text='<%# Bind("SpecialistIn2") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                        <asp:TemplateField HeaderText="Speciality" Visible="false">
                    <ItemTemplate>
                        <asp:Label ID="lblSpeciality3" runat="server" Text='<%# Bind("SpecialistIn3") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
        
                <asp:TemplateField HeaderText="Residential Phone" Visible="false">
                    <ItemTemplate>
                        &nbsp;<asp:Label ID="lbldoc_ph_res" runat="server" Text='<%# Bind("doc_ph_res") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Email Id" Visible="false" >
                    <ItemTemplate>
                        &nbsp;<asp:Label ID="lblEmailId" runat="server" Text='<%# Bind("EmailId") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>

                   <asp:TemplateField HeaderText="Visit Charge" Visible="false" >
                    <ItemTemplate>
                        &nbsp;<asp:Label ID="lblvisit_charges" runat="server" Text='<%# Bind("visit_charges") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
              <asp:TemplateField HeaderText="Commission Per" ItemStyle-Width="65px"  ItemStyle-HorizontalAlign="right"  Visible="false" >
                    <ItemTemplate>
                        &nbsp;<asp:Label ID="lblCommission_Per" runat="server" Text='<%# Bind("Commission_Per") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                 <asp:TemplateField HeaderText="Commission Rs" ItemStyle-Width="65px"  ItemStyle-HorizontalAlign="right"  Visible="false"  >
                    <ItemTemplate>
                        &nbsp;<asp:Label ID="lblCommission_Rs" runat="server" Text='<%# Bind("Commission_Rs") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                 <asp:TemplateField HeaderText="Doctor Fees" ItemStyle-Width="65px"  ItemStyle-HorizontalAlign="right">
                    <ItemTemplate>
                        &nbsp;<asp:Label ID="lblDocFees" runat="server" Text='<%# Bind("DoctorFees") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Doctor Fees Night" ItemStyle-Width="65px"  ItemStyle-HorizontalAlign="right" Visible="false"  >
                    <ItemTemplate>
                        &nbsp;<asp:Label ID="lblDocFeesNight" runat="server" Text='<%# Bind("DoctorFeesNight") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Doctor Night Visit" ItemStyle-Width="65px"  ItemStyle-HorizontalAlign="right" Visible="false"  >
                    <ItemTemplate>
                        &nbsp;<asp:Label ID="lblNightVisit" runat="server" Text='<%# Bind("visit_night") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:CommandField SelectText="Edit" ShowSelectButton="True" ItemStyle-Width="70px"  ItemStyle-HorizontalAlign="Center"   ControlStyle-CssClass="temp" HeaderText="Edit" SelectImageUrl="~/Images/edit.png" ButtonType="Image">
                    <ControlStyle CssClass="temp"></ControlStyle>
                </asp:CommandField>
                <asp:CommandField ShowDeleteButton="True" ControlStyle-CssClass="temp" ItemStyle-Width="70px"  ItemStyle-HorizontalAlign="Center"   HeaderText="Delete" DeleteImageUrl="~/Images/delete.png" ButtonType="Image">
                    <ControlStyle CssClass="temp"></ControlStyle>
                </asp:CommandField>
            </Columns>
            <EditRowStyle BackColor="#FFDB91" />
            <AlternatingRowStyle BackColor="#FFDB91" />
         <HeaderStyle BackColor="#FFC0C0" />
    </asp:GridView>
    
</div>
    </asp:View>
            </asp:MultiView>
</div>
    </ContentTemplate>
    </asp:UpdatePanel>

 
     </asp:Content>

