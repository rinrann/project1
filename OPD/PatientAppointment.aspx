<%@ Page Title="" Language="C#" MasterPageFile="~/MasterAll/MasterPageAll.master" AutoEventWireup="true" CodeFile="PatientAppointment.aspx.cs" Inherits="OPD_PatientAppointment" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
 
    <script type="text/javascript" language="javascript">
    function ShowDialog() {

        var rtvalue = window.open("RegistrationPopup.aspx?type=2", "sss", "Width:800px; Height:550px; dialogLeft:250px;");
        //document.getElementById("ctl00_ContentPlaceHolder1_TextBox2").value = rtvalue.NameValue;

    }

    function Calling() {
        var date = new Date();
        $("input[id$='TextBox12']").datepicker({ dateFormat: 'dd/mm/yy' });
        $("input[id$='TextBox13']").timepicker({
            showPeriod: true,
            showLeadingZero: true
        });


        $("input[id$='Button1']").click(function () {

            if ($("select[id$='DropDownList1']").val() == '0') {
                alert('Please Select Sex !');
                $("select[id$='DropDownList1']").addClass('textboxerr');
                $("select[id$='DropDownList1']").focus();
                return false;
            }
            else {
                $("select[id$='DropDownList1']").removeClass('textboxerr');
            }


            if ($("select[id$='DropDownList2']").val() == '0') {
                alert('Please Select Doctor Type !');
                $("select[id$='DropDownList2']").addClass('textboxerr');
                $("select[id$='DropDownList2']").focus();
                return false;
            }
            else {
                $("select[id$='DropDownList2']").removeClass('textboxerr');
            }

            if ($("select[id$='DropDownList4']").val() == '0') {
                alert('Please Select Patient Type !');
                $("select[id$='DropDownList4']").addClass('textboxerr');
                $("select[id$='DropDownList4']").focus();
                return false;
            }
            else {
                $("select[id$='DropDownList4']").removeClass('textboxerr');
            }


            if ($("select[id$='DropDownList3']").val() == '0') {
                alert('Please Select Doctor Name !');
                $("select[id$='DropDownList3']").addClass('textboxerr');
                $("select[id$='DropDownList3']").focus();
                return false;
            }
            else {
                $("select[id$='DropDownList3']").removeClass('textboxerr');
            }

            if ($("input[id$='TextBox1']").val() == '') {
                alert('Please Enter Appointment No!');
                $("input[id$='TextBox1']").focus();
                $("input[id$='TextBox1']").addClass('textboxerr');
                return false;
            }
            else {
                $("input[id$='TextBox1']").removeClass('textboxerr');
            }

            if ($("input[id$='TextBox3']").val() == '') {
                alert('Please Enter Name !');
                $("input[id$='TextBox3']").focus();
                $("input[id$='TextBox3']").addClass('textboxerr');
                return false;
            }
            else {
                $("input[id$='TextBox3']").removeClass('textboxerr');
            }


            if ($("input[id$='TextBox4']").val() == '') {
                alert('Please Enter C/O !');
                $("input[id$='TextBox4']").focus();
                $("input[id$='TextBox4']").addClass('textboxerr');
                return false;
            }
            else {
                $("input[id$='TextBox4']").removeClass('textboxerr');
            }


            if ($("input[id$='TextBox5']").val() == '') {
                alert('Please Enter Age !');
                $("input[id$='TextBox5']").focus();
                $("input[id$='TextBox5']").addClass('textboxerr');
                return false;
            }
            else {
                if ($("input[id$='TextBox5']").val().length > 3) {
                    alert('Invalid Age !');
                    alert('Age should be >1 and <150');
                    $("input[id$='TextBox5']").focus();
                    $("input[id$='TextBox5']").addClass('textboxerr');
                    return false;
                }
                else {

                    $("input[id$='TextBox5']").removeClass('textboxerr');
                }

            }
            if ($("input[id$='TextBox7']").val() == '') {
                alert('Please Enter Phone - 1 !');
                $("input[id$='TextBox7']").focus();
                $("input[id$='TextBox7']").addClass('textboxerr');
                return false;
            }
            else {
                $("input[id$='TextBox7']").removeClass('textboxerr');
            }


            if ($("input[id$='TextBox10']").val() == '') {
                alert('Please Enter Address !');
                $("input[id$='TextBox10']").focus();
                $("input[id$='TextBox10']").addClass('textboxerr');
                return false;
            }
            else {
                $("input[id$='TextBox10']").removeClass('textboxerr');
            }

            if ($("input[id$='TextBox12']").val() == '') {
                alert('Please Enter Appointment Date !');
                $("input[id$='TextBox12']").focus();
                $("input[id$='TextBox12']").addClass('textboxerr');
                return false;
            }
            else {
                $("input[id$='TextBox12']").removeClass('textboxerr');
            }


            if ($("input[id$='TextBox13']").val() == '') {
                alert('Please Enter Appointment Time !');
                $("input[id$='TextBox13']").focus();
                $("input[id$='TextBox13']").addClass('textboxerr');
                return false;
            }
            else {
                $("input[id$='TextBox13']").removeClass('textboxerr');
            }

        });

        $("input[id$='TextBox5']").keydown(function (event) {
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

        $("input[id$='TextBox9']").keydown(function (event) {
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
            <asp:HiddenField ID="HiddenField1" runat="server" />
                  <div class="form-sec-row">
                <label><strong>Appointment No :</strong></label>
                <asp:TextBox ID="TextBox1" runat="server" CssClass="textbox-medium1" Enabled="False" 
                   ></asp:TextBox>
                <div class="clear"></div>
            </div>
			<div class="form-sec-row">
                <label><strong>Registration No :</strong></label>
                 <asp:TextBox ID="TextBox2" runat="server" CssClass="textbox-medium1" Enabled="False" 
                   ></asp:TextBox> 
                <asp:Button ID="Button3" runat="server" Text="Search" Height="28px" CssClass="submit-button" OnClientClick="ShowDialog()" />
                <asp:Button ID="Button4" runat="server" Text="fetch" CssClass ="submit-button"  Height="28px"
                    onclick="Button4_Click" />
                <div class="clear">  </div>
            </div>
           
            <div class="form-sec-row">
                <label><strong>Patient Name :</strong></label>
                 <asp:TextBox ID="TextBox3" runat="server" CssClass="textbox-medium1" 
                   ></asp:TextBox>
                <div class="clear"></div>
            </div>

                   <div class="form-sec-row">
                <label><strong>C/O :</strong></label>
                 <asp:TextBox ID="TextBox4" runat="server" CssClass="textbox-medium1" 
                   ></asp:TextBox>
                <div class="clear"></div>
            </div> 

                        <div class="form-sec-row">
                <label><strong>Age :</strong></label>
                    <asp:TextBox ID="TextBox5" runat="server" CssClass="textbox-medium1" 
                   ></asp:TextBox>
                <div class="clear"></div>
            </div>

                        <div class="form-sec-row">
                <label><strong>Sex :</strong></label>
                            <asp:DropDownList ID="DropDownList1" runat="server" CssClass="textbox-medium1">
                            </asp:DropDownList>
                <div class="clear"></div>
            </div>
                 <div class="form-sec-row">
                <label><strong>Contact No :</strong></label>
                <asp:TextBox ID="TextBox6" runat="server" CssClass="textbox-medium1" Width="50" Enabled="False">+91</asp:TextBox>
            
                 <asp:TextBox ID="TextBox7" runat="server" CssClass="textbox-medium1" 
                    MaxLength="10" Width="246px" ></asp:TextBox>
                <div class="clear"></div>
            </div>
            
            <div class="form-sec-row">
                <label><strong>Alternative Contact No. :</strong></label>
                <asp:TextBox ID="TextBox8" runat="server" CssClass="textbox-medium1" Width="50" Enabled="False">+91</asp:TextBox>
                <asp:TextBox ID="TextBox9" runat="server" CssClass="textbox-medium1" 
                    MaxLength="10" Width="246px" ></asp:TextBox>
                <div class="clear"></div>
            </div>
            <div class="form-sec-row">
                <label><strong>Address:</strong></label>  
                 <asp:TextBox ID="TextBox10" runat="server" Height="55px"   CssClass="textbox-medium1" TextMode="MultiLine" 
                   ></asp:TextBox>
                <div class="clear"></div>
            </div>
                            <div class="form-sec-row">
                <label><strong>District :</strong></label>
          <asp:DropDownList ID="DropDownList9" CssClass="textbox-medium1" runat="server">
                    </asp:DropDownList>
                <div class="clear"></div>
            </div>    
            
            
                  <div class="form-sec-row">
                <label><strong>Doctor Type :</strong></label>
               <asp:DropDownList ID="DropDownList2" runat="server" CssClass="textbox-medium1" 
                          AutoPostBack="True" onselectedindexchanged="DropDownList2_SelectedIndexChanged">
                            </asp:DropDownList>
                <div class="clear"></div>
            </div> 
            
                  <div class="form-sec-row">
                <label><strong>Appointed Doctor :</strong></label>
            <asp:DropDownList ID="DropDownList3" runat="server" CssClass="textbox-medium1">
                            </asp:DropDownList>
                <div class="clear"></div>
            </div>         
       
            <div class="form-sec-row">
                <label><strong>Appointment Date :</strong></label>
         <asp:TextBox ID="TextBox12" runat="server" CssClass="textbox-medium1" TextMode="Date"></asp:TextBox>
                 <%--<cc1:MaskedEditExtender ID="MaskedEditExtender1" runat="server" TargetControlID="TextBox12"
                                            Mask="99/99/9999" MessageValidatorTip="true" MaskType="Date" DisplayMoney="Left"
                                            AcceptNegative="Left" ErrorTooltipEnabled="True" CultureName="en-GB" />
                         <asp:Label ID="Label3" runat="server" ForeColor="Green" Text="(DD/MM/YYYY)"></asp:Label>--%>
                <div class="clear"></div>
            </div>
            <div class="form-sec-row"> 
                <label><strong>Appointment Time :</strong></label>  
                  <asp:TextBox ID="TextBox13" runat="server" CssClass="textbox-medium1" TextMode="Time" ></asp:TextBox>
                <div class="clear"></div>
            </div>

                <div class="form-sec-row" style="display:none;">
                <label><strong>Appointed Type :</strong></label>
            <asp:DropDownList ID="DropDownList5" runat="server" CssClass="textbox-medium1" 
                        Enabled="False">
                <asp:ListItem Value="2">Appointment</asp:ListItem>
                            </asp:DropDownList>
                <div class="clear"></div>
            </div>

                 <div class="form-sec-row"  style="display:none;"> 
                <label><strong>Type of OPD Patient:</strong></label>
   <asp:DropDownList ID="DropDownList4" runat="server" CssClass="textbox-medium1">
                            </asp:DropDownList>
                <div class="clear"></div>
            </div>
            <div class="form-sec-row"  style="display:none;">
                <label><strong>Advance Amount :</strong></label>
                <asp:TextBox ID="TextBox14" runat="server" CssClass="textbox-medium1" ></asp:TextBox>
                <div class="clear"></div>
            </div>

                 <div class="form-sec-row">
                <label><strong>Doctor's Remarks:</strong></label>  
                 <asp:TextBox ID="TextBox15" runat="server" Height="45px"   CssClass="textbox-medium1" TextMode="MultiLine" 
                   ></asp:TextBox>
                <div class="clear"></div>
            </div>
            <div class="form-sec-row">
                <label><strong>&nbsp;</strong></label>
                <asp:Button ID="Button1" runat="server" OnClick="Button1_Click" Height="28px" Text="Submit" CssClass="submit-button" />
                <asp:Button ID="Button2" runat="server" Text="Cancel"  Height="28px" OnClick="Button2_Click" CssClass="submit-button" />
                <div class="clear"></div>
            </div>  
   
     </div>
     </asp:View>
     <asp:View ID="View2" runat="server">
      <div style="width:100%; height:600px; overflow:auto;">  
      
      <div class="listing">
         <asp:GridView id="GridView1"  CssClass="grid" PagerStyle-CssClass="pgr" DataKeyNames ="AppoNo" runat="server" AutoGenerateColumns="False" AllowPaging="True" 
         PageSize ="107" 
                 OnPageIndexChanging="GridView1_PageIndexChanging" 
                 OnRowCommand="GridView1_RowCommand"  SelectedRowStyle-BackColor="GreenYellow"   OnRowDataBound="GridView1_RowDataBound"
             OnRowDeleting="GridView1_RowDeleting" Width="100%">
                <RowStyle HorizontalAlign="Center" />
                <Columns>
                    <asp:TemplateField HeaderText="ApppoNo" Visible="true">
                        <ItemTemplate>
                            <asp:Label ID="lblID" runat="server" Text='<%# Bind("AppoNo") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Registration No">
                       
                        <ItemTemplate>
                            <asp:Label ID="lblregno" runat="server" Text='<%# Bind("PatientRegNo") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Patient Name">
                       
                        <ItemTemplate>
                            <asp:Label ID="lblname" runat="server" Text='<%# Bind("PName") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    
                    <asp:TemplateField HeaderText="Age">
                        <ItemTemplate>                        
                            <asp:Label ID="lblage" runat="server" Text='<%# Bind("Age") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField> 
                       
                    <asp:TemplateField HeaderText="Sex">
                        <ItemTemplate>                        
                            <asp:Label ID="lblsex" runat="server" Text='<%# Bind("SexName") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                      <asp:TemplateField HeaderText="Phone - 1" >
                        <ItemTemplate>                        
                            <asp:Label ID="lblphone1" runat="server" Text='<%# Bind("PhNo1") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Phone - 2">
                        <ItemTemplate>                        
                            <asp:Label ID="lblphone2" runat="server" Text='<%# Bind("PhNo2") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Address">
                        <ItemTemplate>                        
                            <asp:Label ID="lbladdress" runat="server" Text='<%# Bind("Address") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>  
                    <asp:TemplateField HeaderText="Guadian Name">
                        <ItemTemplate>                        
                            <asp:Label ID="lblguadian" runat="server" Text='<%# Bind("GuadianName") %>'></asp:Label>    
                        </ItemTemplate>
                    </asp:TemplateField>    

                      <asp:TemplateField HeaderText="Doctor Type ">
                        <ItemTemplate>                        
                            <asp:Label ID="lbldoctype" runat="server" Text='<%# Bind("dttype") %>'></asp:Label>    
                        </ItemTemplate>
                    </asp:TemplateField> 


                        <asp:TemplateField HeaderText="Doctor Name">
                        <ItemTemplate>                        
                            <asp:Label ID="lbldocname" runat="server" Text='<%# Bind("doc_name") %>'></asp:Label>    
                        </ItemTemplate>
                    </asp:TemplateField> 


                        <asp:TemplateField HeaderText="AppointMent Date">
                        <ItemTemplate>                        
                            <asp:Label ID="lblappodate" runat="server" Text='<%# Bind("apppdate") %>'></asp:Label>  
                            <asp:Label ID="lblappodate1" runat="server" Text='<%# Bind("strapppdate") %>' Visible="false"></asp:Label>  
                        </ItemTemplate>
                    </asp:TemplateField> 


                        <asp:TemplateField HeaderText="Appointment Time">
                        <ItemTemplate>                        
                            <asp:Label ID="lblappotime" runat="server" Text='<%# Bind("AppointmentTime") %>'></asp:Label>    
                        </ItemTemplate>
                    </asp:TemplateField> 

                    
                    <asp:TemplateField HeaderText="Patient Type">
                        <ItemTemplate>                        
                            <asp:Label ID="lblptype" runat="server" Text='<%# Bind("ptype") %>'></asp:Label>    
                        </ItemTemplate>
                    </asp:TemplateField>


                    
                        <asp:TemplateField HeaderText="District">
                        <ItemTemplate>                        
                            <asp:Label ID="lbldist" runat="server" Text='<%# Bind("District") %>'></asp:Label>    
                        </ItemTemplate>
                    </asp:TemplateField>
                    
                        <asp:TemplateField HeaderText="Advanced Amount">
                        <ItemTemplate>                        
                            <asp:Label ID="lbladvamt" runat="server" Text='<%# Bind("AdvancedAmount") %>'></asp:Label>    
                        </ItemTemplate>
                    </asp:TemplateField>

                    
                        <asp:TemplateField HeaderText="Remarks" Visible ="false">
                        <ItemTemplate>                        
                            <asp:Label ID="lblremarks" runat="server" Text='<%# Bind("Remarks") %>'></asp:Label>    
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
        </div>
        </asp:View>
        </asp:MultiView>
        </div>
    </ContentTemplate>
    </asp:UpdatePanel>

     
</asp:Content>

