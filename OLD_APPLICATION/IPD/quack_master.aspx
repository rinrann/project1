<%@ Page Title="" Language="C#" MasterPageFile="~/MasterAll/MasterPageAll.master" AutoEventWireup="true" CodeFile="quack_master.aspx.cs" Inherits="IPD_quack_master" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">

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





    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
    <ContentTemplate>
         <div class="pageheader">
         <asp:Label ID="Label1" runat="server">Quack / Technician Master</asp:Label>
     </div>
         <table width="290px" cellpadding="0" cellspacing="0">
         <tr>
            <td>
                <asp:Button Text="Entry" BorderStyle="None" ID="Tab1" Width="145px"  Height="40px" 
                    CssClass="Initial" runat="server" onclick="Tab1_Click"
                     /></td>
                     <td>
                <asp:Button Text="List" BorderStyle="None" ID="Tab2" CssClass="Initial"  Width="145px"  Height="40px" 
                    runat="server" OnClick="Tab2_Click"
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
                <label><strong>Quack Type :<span style="color:red;">*</span></strong></label>
                    <asp:DropDownList ID="DropDownList1" runat="server" CssClass="textbox-medium1">
                        
                <%-- <asp:ListItem value="A">Agent</asp:ListItem>
                <asp:ListItem value="R" >Rural Doctor</asp:ListItem>--%>
                    </asp:DropDownList>
                <div class="clear">
                </div>
                
            </div>
            <div class="form-sec-row">
                <label><strong> Name :<span style="color:red;">*</span></strong></label>
                
                <asp:TextBox ID="TextBox1" runat="server" CssClass="textbox-medium1" 
                    Width="272px"></asp:TextBox>
                <cc1:AutoCompleteExtender ServiceMethod="Searchquacktype" OnClientItemSelected="autoCompleteEx_ItemSelected" MinimumPrefixLength="1"
                                    CompletionInterval="100" EnableCaching="false" CompletionSetCount="10" TargetControlID="TextBox1" ID="AutoCompleteExtender2" runat="server" 
                                       FirstRowSelected = "false" ></cc1:AutoCompleteExtender>
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
                <asp:DropDownList ID="DropDownList2" runat="server" CssClass="combo-big1" AutoPostBack="false">
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
                   <asp:ListItem Text="West Bengal" Value="1"></asp:ListItem>
                    <asp:ListItem Text="Andhra Pradesh" Value="2"></asp:ListItem>
                    <asp:ListItem Text="Assam" Value="3"></asp:ListItem>
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

                         
            <div class="form-sec-row">
                <label><strong>Fax :</strong></label>
                <asp:TextBox ID="TextBox12" runat="server" CssClass="textbox-medium1" ></asp:TextBox>
                <div class="clear"></div>
            </div>
            
                
                <div class="form-sec-row">
                    <label><strong>Commission(%) :</strong></label>
                    <asp:TextBox ID="TextBox13" runat="server" MaxLength="3"     CssClass="textbox-medium1" ></asp:TextBox>
                    <div class="clear"></div>
                </div>
                
                 <div class="form-sec-row">
                    <label><strong>Commission(Fixed) :</strong></label>
                    <asp:TextBox ID="TextBox14" runat="server" CssClass="textbox-medium1" ></asp:TextBox>
                    <div class="clear"></div>
                </div>

            <div class="form-sec-row">
                <label><strong>&nbsp;</strong></label>
                <asp:Button ID="Button1" runat="server" OnClick="Button1_Click" Text="Submit" 
                    CssClass="submit-button" Height="28px" />
                <asp:Button ID="Button2" runat="server" Text="Cancel" OnClick="Button2_Click" 
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
   <td style='width:90px;' align="center">Id</td>
   <td style='width:110px;'  align="center">Name</td>
    <td style='width:90px; ' align="center">City</td>
     <td style='width:65px; ' align="center">Phone</td>  
        <td style='width:65px;' align="center">Comm(%)</td>
               <td style='width:65px;' align="center">Comm(Rs)</td>
        <td style='width:70px;' align="center">Edit</td>
          <td style='width:70px;' align="center" id="coldel" runat="server">Delete</td>
          </tr>
  </table> 
  </div> 

<div class="listing"  style='width:100%; height:500px; overflow:auto;'>
        <asp:GridView id="GridView1" CssClass="grid" PagerStyle-CssClass="pgr" DataKeyNames ="QuackId,QuackName" runat="server" 
        AutoGenerateColumns="False" AllowPaging="True" PageSize ="100" OnRowCommand="GridView1_RowCommand" OnRowDataBound="GridView1_RowDataBound" Width="100%"
         OnRowDeleting="GridView1_RowDeleting"   ShowHeader="false"
        OnPageIndexChanging="GridView1_PageIndexChanging">
            <Columns>
                <asp:TemplateField HeaderText="Doctor Id" ItemStyle-Width="90px"  ItemStyle-HorizontalAlign="Center"  >
                    <ItemTemplate>
                        &nbsp;<asp:Label ID="lblDoctorId" runat="server" Text='<%# Bind("QuackId") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>


                <asp:TemplateField HeaderText="Type" Visible="false">
                    <ItemTemplate>
                        &nbsp;<asp:Label ID="lbltype" runat="server" Text='<%# Bind("Type") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Doctor Name"  ItemStyle-Width="110px"  ItemStyle-HorizontalAlign="Center"  >
                   <ItemTemplate>
                        <asp:Label ID="lblDoctorName" runat="server" Text='<%# Bind("QuackName") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Address" Visible="false">
                   <ItemTemplate>
                        <asp:Label ID="lblAddress" runat="server" Text='<%# Bind("Address1") %>'></asp:Label>
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
                <asp:TemplateField HeaderText="City" ItemStyle-Width="90px"  ItemStyle-HorizontalAlign="Center"  >
                    <ItemTemplate>
                        <asp:Label ID="lblCity" runat="server" Text='<%# Bind("Address2") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Pin" Visible="false">
                    <ItemTemplate>
                        <asp:Label ID="lblPin" runat="server" Text='<%# Bind("Pin") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Phone" ItemStyle-Width="65px"  ItemStyle-HorizontalAlign="Center"  >
                    <ItemTemplate>
                        &nbsp;<asp:Label ID="lblPhone" runat="server" Text='<%# Bind("PhNo1") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="ph_res" Visible="false">
                    <ItemTemplate>
                        <asp:Label ID="lbldoc_ph_res" runat="server" Text='<%# Bind("Phno2") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>

                        <asp:TemplateField HeaderText="Fax" Visible="false">
                    <ItemTemplate>
                        &nbsp;<asp:Label ID="lblFax" runat="server" Text='<%# Bind("Fax") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Email Id" Visible="false" >
                    <ItemTemplate>
                        &nbsp;<asp:Label ID="lblEmailId" runat="server" Text='<%# Bind("EmailId") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>

                

              <asp:TemplateField HeaderText="Commission Per" ItemStyle-Width="65px"  ItemStyle-HorizontalAlign="Center"  >
                    <ItemTemplate>
                        &nbsp;<asp:Label ID="lblCommission_Per" runat="server" Text='<%# Bind("Commission_Per") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                 <asp:TemplateField HeaderText="Commission Rs" ItemStyle-Width="65px"  ItemStyle-HorizontalAlign="Center"  >
                    <ItemTemplate>
                        &nbsp;<asp:Label ID="lblCommission_Rs" runat="server" Text='<%# Bind("Commission_Rs") %>'></asp:Label>
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