<%@ Page Title="" Language="C#" MasterPageFile="~/MasterAll/MasterPageAll.master" AutoEventWireup="true" CodeFile="PatientAppointment.aspx.cs" Inherits="DayCare_PatientAppointment" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
 
<script type="text/javascript" language="javascript">


    function isNumberKey(evt) {
        var charCode = (evt.which) ? evt.which : event.keyCode;
        if (charCode != 46 && charCode > 31
            && (charCode < 48 || charCode > 57))
            return false;

        return true;
    }


    function autoCompleteEx_ItemSelected(sender, args) {
        var regname = args.get_value().split('-');
        document.getElementById("ctl00_ContentPlaceHolder1_txtreg").value = regname[1];
        document.getElementById("ctl00_ContentPlaceHolder1_txtname").value = regname[0];
    }
     

    function CheckExistappointment() {
        var dat = document.getElementById("ctl00_ContentPlaceHolder1_HiddenField2").value;
        alert("Patient Already Appointed in  " + dat + " Date  !!");
    }

    function ShowDialog() {

        var rtvalue = window.open("AppointmentPopup.aspx", "sss", "Width:800px; Height:550px; dialogLeft:250px;");
        document.getElementById("ctl00_ContentPlaceHolder1_txtreg").value = rtvalue.NameValue;

    }

    function Calling() {


        $("input[id$='Calendar1']").datepicker({ dateFormat: 'dd/mm/yy' });



        $("input[id$='Button1']").click(function () {
            if ($("input[id$='txtname']").val() == '') {
                alert('Please Enter Patient Name !');
                $("input[id$='txtname']").focus();
                $("input[id$='txtname']").addClass('textboxerr');
                return false;
            }
            else
                if ($("input[id$='txtname']").val().split(' ').length <= '1') {
                    alert('Please Enter Patient valid Name !');
                    alert('Name Format Will be First Name & Last Name !');
                    $("input[id$='txtname']").focus();
                    $("input[id$='txtname']").addClass('textboxerr');
                    return false;
                }
                else {
                    $("input[id$='txtname']").removeClass('textboxerr');
                }




            if ($("input[id$='txtaddress']").val() == '') {
                alert('Please Enter Address !');
                $("input[id$='txtaddress']").focus();
                $("input[id$='txtaddress']").addClass('textboxerr');
                return false;
            }
            else {
                $("input[id$='txtaddress']").removeClass('textboxerr');
            }






            if ($("input[id$='txtph1']").val() == '') {
                alert('Please Enter Phone no - 1 !');
                $("input[id$='txtph1']").focus();
                $("input[id$='txtph1']").addClass('textboxerr');
                return false;
            }
            else {
                $("input[id$='txtph1']").removeClass('textboxerr');
            }



            if ($("input[id$='Calendar1']").val() == '') {
                alert('Please Enter Date !');
                $("input[id$='Calendar1']").focus();
                $("input[id$='Calendar1']").addClass('textboxerr');
                return false;
            }
            else {
                $("input[id$='Calendar1']").removeClass('textboxerr');
            }


            if ($("select[id$='DropDownList1']").val() == '0') {
                alert('Select A Shift!');
                $("select[id$='DropDownList1']").focus();
                $("select[id$='DropDownList1']").addClass('textboxerr');
                return false;
            }
            else {
                $("select[id$='DropDownList1']").removeClass('textboxerr');
            }



        });

        $("input[id$='txtph1']").keydown(function (event) {
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


        $("input[id$='txtph2']").keydown(function (event) {
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
                 
</script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <%--For Busy Loader .............................--%>
  <asp:UpdateProgress ID="UpdateProgress2" runat="server">
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
         <asp:Label ID="Label1" runat="server">Patient Appointment</asp:Label>
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
                <strong><asp:Label ID="lblError" runat="server" Font-Bold="True"></asp:Label>
                </strong>
                <div class="clear"></div>
            </div>
            <asp:HiddenField ID="TextBox4" runat="server" /><asp:HiddenField ID="HiddenField1" runat="server" /><asp:HiddenField ID="HiddenField2" runat="server" />

                  <div class="form-sec-row">
                <label><strong>Appointment No :</strong></label>
                <asp:TextBox ID="TextBox3" runat="server" CssClass="textbox-medium1" Enabled="False" 
                   ></asp:TextBox>
                <div class="clear"></div>
            </div>

			<div class="form-sec-row">
                <label><strong>Registration No :</strong></label>
               <asp:TextBox ID="txtreg" runat="server" CssClass="textbox-medium1" Enabled="False" 
                   ></asp:TextBox> <asp:Button ID="Button4" runat="server" 
                    CssClass="submit-search" Height="28px" Text="Quick Search" OnClientClick="ShowDialog()" 
                   />
                   <asp:Button ID="Button5" runat="server"  Height="28px"  CssClass="submit-button" 
                    Text="Fetch" onclick="Button5_Click" />
                <div class="clear">  </div>
            </div>
           
            <div class="form-sec-row">
                <label><strong>Patient Name :</strong></label>
                <asp:TextBox ID="txtname" runat="server" CssClass="textbox-medium1"  autocomplete="on" 
                   ></asp:TextBox>
                   <cc1:AutoCompleteExtender ServiceMethod="SearchCustomers"  OnClientItemSelected="autoCompleteEx_ItemSelected"     MinimumPrefixLength="1"    CompletionInterval="100" EnableCaching="false" 
                   CompletionSetCount="10" TargetControlID="txtname"  ID="AutoCompleteExtender1" runat="server" FirstRowSelected = "false" ></cc1:AutoCompleteExtender>
                <div class="clear"></div>
            </div>

            <div class="form-sec-row">
                <label><strong>Address:</strong></label>  
                 <asp:TextBox ID="txtaddress" runat="server" CssClass="textbox-medium1" 
                   ></asp:TextBox>
         <cc1:AutoCompleteExtender ServiceMethod="SearchCustomersAddress"    MinimumPrefixLength="1"  CompletionInterval="100" EnableCaching="false" 
           CompletionSetCount="10" TargetControlID="txtaddress"  ID="AutoCompleteExtender2" runat="server" FirstRowSelected = "false" ></cc1:AutoCompleteExtender>
                <div class="clear"></div>
            </div>
            <div class="form-sec-row">
                <label><strong>Phone-1 :</strong></label>
                  <asp:TextBox ID="TextBox1" runat="server" CssClass="textbox-medium1" Width="50" Enabled="False">+91</asp:TextBox>
                  <asp:TextBox ID="txtph1" runat="server" CssClass="textbox-medium1" Width="246px"  MaxLength="10"
                   ></asp:TextBox>
 
                <div class="clear"></div>
            </div>
            <div class="form-sec-row">
                <label><strong>Phone-2 :</strong></label>
                  <asp:TextBox ID="TextBox2" runat="server" CssClass="textbox-medium1" Width="50" Enabled="False">+91</asp:TextBox>
                 <asp:TextBox ID="txtph2" runat="server" CssClass="textbox-medium1"  Width="246px"  MaxLength="10"
                   ></asp:TextBox>
                <div class="clear"></div>
            </div>
            <div class="form-sec-row">
                <label><strong>Appointment Date :</strong></label>
                 <asp:TextBox ID="Calendar1" runat="server" CssClass="textbox-medium1"></asp:TextBox>
                <asp:Label ID="Label3" runat="server" ForeColor="Green" Text="(DD/MM/YYYY)"></asp:Label>
                <div class="clear"></div>
            </div>
            <div class="form-sec-row"> 
                <label><strong>Appointment Shift :</strong></label>
                <div style="float:left;">
                <asp:DropDownList ID="DropDownList1" runat="server" CssClass="textbox-mediumpatcover">
              </asp:DropDownList></div> <div style="padding-left:10px;"><asp:Button ID="Button3" runat="server" style="width:150px" Height="28px" CssClass="submit-buttonCheck" Text="Check Availability" 
                     onclick="Button3_Click"/></div>
                <asp:Label ID="Label2" runat="server" Text="" style="font-size: small; font-family: 'Times New Roman', Times, serif;"></asp:Label>
                <div class="clear"></div>
            </div>
            <div class="form-sec-row">
                <label><strong>Advance Amount :</strong></label>
                 <asp:TextBox ID="txtamt" runat="server" CssClass="textbox-medium1" onkeypress="return isNumberKey(event)" ></asp:TextBox>
                <div class="clear"></div>
            </div>
            <div class="form-sec-row">
                <label><strong>&nbsp;</strong></label>
                <asp:Button ID="Button1" runat="server" Height="28px"  OnClick="Button1_Click" Text="Submit" CssClass="submit-button" />
                <asp:Button ID="Button2" runat="server" Height="28px"  Text="Cancel" OnClick="Button2_Click" CssClass="submit-button" />
                <div class="clear"></div>
            </div>  
   
     </div>
     </asp:View>
     <asp:View ID="View2" runat="server">

     <div class="listing"  style='height:auto; width:100%; overflow:auto;'>
         <asp:GridView id="GridView1"  CssClass="grid" PagerStyle-CssClass="pgr" DataKeyNames ="AppNo,PName" runat="server" AutoGenerateColumns="False" AllowPaging="True" 
         PageSize ="100" 
                 OnPageIndexChanging="GridView1_PageIndexChanging" 
                 OnRowCommand="GridView1_RowCommand" OnRowDataBound="GridView1_RowDataBound"  SelectedRowStyle-BackColor="GreenYellow"
             OnRowDeleting="GridView1_RowDeleting" Width="100%">
                <RowStyle HorizontalAlign="Center" />
                <Columns>
                    <asp:TemplateField HeaderText="ApppoId" Visible="false">
                        <ItemTemplate>
                            <asp:Label ID="lblID" runat="server" Text='<%# Bind("AppNo") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Registration No">
                       
                        <ItemTemplate>
                            <asp:Label ID="lblregno" runat="server" Text='<%# Bind("PatientReg") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Patient Name">
                       
                        <ItemTemplate>
                            <asp:Label ID="lblname" runat="server" Text='<%# Bind("PName") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    
                    <asp:TemplateField HeaderText="Address">
                        <ItemTemplate>                        
                            <asp:Label ID="lbladdress" runat="server" Text='<%# Bind("PAddress") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField> 
                       
                    <asp:TemplateField HeaderText="Phone - 1">
                        <ItemTemplate>                        
                            <asp:Label ID="lblphone1" runat="server" Text='<%# Bind("PhNo1") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                      <asp:TemplateField HeaderText="Phone - 2" Visible="false">
                        <ItemTemplate>                        
                            <asp:Label ID="lblphone2" runat="server" Text='<%# Bind("PhNo2") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Appointment Date">
                        <ItemTemplate>                        
                            <asp:Label ID="lblAppDate" runat="server" Text='<%# Bind("Date1") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Shift">
                        <ItemTemplate>                        
                            <asp:Label ID="lblshift" runat="server" Text='<%# Bind("ShiftName") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Advance">
                        <ItemTemplate>                        
                            <asp:Label ID="lblamt" runat="server" Text='<%# Bind("AdvAmount") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>    
                                
                    <asp:CommandField SelectText="Edit" ShowSelectButton="True" ControlStyle-CssClass="temp" HeaderText="Edit" SelectImageUrl="~/Images/edit.png" ButtonType="Image">
                        <ControlStyle CssClass="temp"></ControlStyle>
                    </asp:CommandField>
                    <asp:CommandField ShowDeleteButton="True" ControlStyle-CssClass="temp" HeaderText="Delete" DeleteImageUrl="~/Images/delete.png" ButtonType="Image">
                        <ControlStyle CssClass="temp"></ControlStyle>
                    </asp:CommandField>
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

