<%@ Page Title="" Language="C#" MasterPageFile="~/MasterAll/MasterPageAll.master" AutoEventWireup="true" CodeFile="DCDischarge.aspx.cs" Inherits="DayCare_DCDischarge" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
     
<script language="javascript" type="text/javascript">
    function ShowDialog() {
        var rtvalue = window.open("RegistrationPopup.aspx", "sss", "Width:800px; Height:550px; dialogLeft:250px;");
        //document.getElementById("ctl00_ContentPlaceHolder1_txtreg").value = rtvalue.NameValue;
    }

    function Calling() {
        var date = new Date();
        $("input[id$='TextBox1']").datepicker({ dateFormat: 'dd/mm/yy' });

        $("input[id$='TextBox8']").datepicker({ dateFormat: 'dd/mm/yy' });

        $("input[id$='TextBox2']").timepicker({
            showPeriod: true,
            showLeadingZero: true
        });

        $("input[id$='TextBox9']").timepicker({
            showPeriod: true,
            showLeadingZero: true
        }); 

        $("input[id$='Tab2']").click(function () {
            if ($("input[id$='txtreg']").val() == '') {
                alert('Please Enter Registration No  !');
                $("input[id$='txtreg']").focus();
                $("input[id$='txtreg']").addClass('textboxerr');
                return false;
            }
            else {
                $("input[id$='txtreg']").removeClass('textboxerr');
            }
        });

        $("input[id$='Button1']").click(function () {
            if ($("input[id$='TextBox1']").val() == '') {
                alert('Please Enter Date  !');
                $("input[id$='TextBox1']").focus();
                $("input[id$='TextBox1']").addClass('textboxerr');
                return false;
            }
            else {
                $("input[id$='TextBox1']").removeClass('textboxerr');
            }


            if ($("input[id$='TextBox2']").val() == '') {
                alert('Please Enter Reason of Day Off  !');
                $("input[id$='TextBox2']").focus();
                $("input[id$='TextBox2']").addClass('textboxerr');
                return false;
            }
            else {
                $("input[id$='TextBox2']").removeClass('textboxerr');
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

  <asp:UpdateProgress ID="UpdateProgress1" runat="server">
    <ProgressTemplate>    
       <div id="aaa" class="progressBackgroundFilter"></div>
        <div id="bbb" class="processMessage"><img alt="Loading" src="../images/pwait.gif"/>      
              </div>
    </ProgressTemplate>
    </asp:UpdateProgress>

    <div class="pageheader">
         <asp:Label ID="Label1" runat="server">Check Out</asp:Label>
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
           <asp:UpdatePanel ID="UpdatePanel1" runat="server">
    <ContentTemplate>
            <div class="error">
                <strong><asp:Label ID="lblError" runat="server" Font-Bold="True"></asp:Label></strong>
                <div class="clear"></div>
            </div>
     <asp:HiddenField ID="HiddenField1" runat="server" /><asp:HiddenField ID="HiddenField2" runat="server" />

       <div class="form-sec-row">
                <label><strong>Registration No :</strong></label>
                <asp:TextBox ID="txtreg" CssClass="textbox-medium1"  runat="server" 
                    Enabled="False"></asp:TextBox>
                    <asp:Button ID="Button3" runat="server" Height="28px" Text="Search"  CssClass="submit-button"  OnClientClick="ShowDialog()"/>
                <asp:Button ID="Button4" runat="server"  Text="fetch"  Height="28px"
                     CssClass="submit-button" onclick="Button4_Click" />
                <div class="clear"></div>
            </div>

               <div class="form-sec-row">
                <label><strong>Patient's Name :</strong></label>
                 <asp:TextBox ID="TextBox4" runat="server" CssClass="textbox-medium1" 
                       Enabled="False" ></asp:TextBox>
                   <div class="clear">  </div>
            </div>


              <div class="form-sec-row">
                <label><strong>Bed No :</strong></label>
                 <asp:TextBox ID="TextBox5" runat="server" CssClass="textbox-medium1" 
                       Enabled="False" ></asp:TextBox>
                   <div class="clear">  </div>
            </div>

			<div class="form-sec-row">
                <label><strong>Discharge Date :</strong></label>
                 <asp:TextBox ID="TextBox1" runat="server" CssClass="textbox-medium1" 
                   ></asp:TextBox>
                <div class="clear">  </div>
            </div>


            <div class="form-sec-row">
                <label><strong>Discharge Time :</strong></label>
                 <asp:TextBox ID="TextBox2" runat="server" CssClass="textbox-medium1" 
                   ></asp:TextBox>
                <div class="clear">  </div>
            </div>

              <div class="form-sec-row">
                <label><strong>Is Death :</strong></label>
                       <asp:CheckBox ID="CheckBox1" runat="server" AutoPostBack="True" 
                           oncheckedchanged="CheckBox1_CheckedChanged" />
                <div class="clear"></div>
            </div>
        
           <div class="form-sec-row">
                <label><strong>Date of Death :</strong></label>
                <asp:TextBox ID="TextBox8" runat="server" CssClass="textbox-medium1" 
                   ></asp:TextBox>
                <div class="clear"></div>
            </div>

               <div class="form-sec-row">
                <label><strong>Time of Death :</strong></label>
                <asp:TextBox ID="TextBox9" runat="server" CssClass="textbox-medium1" 
                   ></asp:TextBox>
                <div class="clear"></div>
            </div>

            <div class="form-sec-row">
                <label><strong>Remarks :</strong></label>
                 <asp:TextBox ID="TextBox3" runat="server" CssClass="textbox-medium1" >              
                   </asp:TextBox>
                   <div class="clear">  </div>
            </div>



            <div class="form-sec-row">
                <label><strong>&nbsp;</strong></label>
                <asp:Button ID="Button1" runat="server"  Text="Submit"  Width="60px"  Height="28px" 
                    CssClass="submit-button" onclick="Button1_Click" />
                <asp:Button ID="Button2" runat="server" Text="Cancel"  Width="60px"  Height="28px" 
                    CssClass="submit-button" onclick="Button2_Click1" />
                <asp:Button ID="Button5" runat="server" Text="Certificate" Width="100px"  
                    Height="28px" CssClass="submit-generate" onclick="Button5_Click" />
                <asp:Button ID="Button6" runat="server" Text="Next Appointment"   Height="28px" 
                    CssClass="submit-generate" onclick="Button6_Click" />
                <div class="clear"></div>
            </div> 
            
        <asp:Panel ID="Panel1" runat="server">
        <center>
                <table width="100%">
                   <tr>
                   <td style='width:5%;'>
                        <asp:RadioButtonList ID="RadioButtonList1" runat="server"  Width="250px"
                   RepeatDirection="Horizontal" AutoPostBack="True" 
                            onselectedindexchanged="RadioButtonList1_SelectedIndexChanged">
                   <asp:ListItem>With Header</asp:ListItem>
                   <asp:ListItem>Without Header</asp:ListItem>
               </asp:RadioButtonList>
               </td>
               <td  style='width:5%;'>
                    <asp:DropDownList ID="DropDownList1" runat="server" CssClass="textbox-medium1" 
                         Width="150px"  >
                         <asp:ListItem Value="0">-- Select --</asp:ListItem>
                         <asp:ListItem Value="1">Bengali</asp:ListItem>
                         <asp:ListItem Value="2">English</asp:ListItem>
                     </asp:DropDownList>
                   </td>
                   <td style='width:5%;'>
                   <asp:Button ID="Button8" CssClass="submit-button" runat="server" Text="Generate" 
                           onclick="Button8_Click" />
               </td>
               </tr>
                    <tr>        
                        <td colspan="3" align="center">     <div id="mydiv" runat="server">          
                            <asp:Literal ID="ltrReport" runat="server"></asp:Literal>
                              </div>                  
                        </td>
                    </tr>
          
            </table>
            </center>
        </asp:Panel>
               </ContentTemplate>
    </asp:UpdatePanel>
            <asp:Panel ID="Panel2" runat="server">
          
               <table width="100%">
                                     <tr>
                        <td align="center">
                     
                         <input type="button" value="Back" style="width:70px; font-size:x-small" onclick="window.history.back()" />
                         <input type="button" id="cmdPrint" value="Print" style="width:70px; font-size:x-small" onclick="javascript:printDiv('mydiv')"/>
                       
                         <asp:Button ID="Button7" runat="server" Text="Generate PDF" 
                                style="width:90px; font-size:x-small" onclick="Button7_Click"  />
                         </td>
                    </tr>
                           </table> 
                             </asp:Panel>
     </div>
     </asp:View>
        <asp:View ID="View2" runat="server">
           <asp:UpdatePanel ID="UpdatePanel2" runat="server">
    <ContentTemplate>
        <table width="100%" border="1px" style='background-color:#D5FDC1;'>
        <tr>
        <td>   
                <label><strong>Registration No :</strong></label>    </td>
            <td>     <asp:TextBox ID="TextBox6" runat="server" Enabled="false"  
                   ></asp:TextBox> </td>
               
            <td>   
                <label><strong>Patient's Name :</strong></label>    </td>
            <td>     <asp:TextBox ID="TextBox7" runat="server" Enabled="false" 
                   ></asp:TextBox> </td>

        </tr>
        </table>

     <div class="listing"   style='width:100%; height:300px; overflow:auto;'>
         <asp:GridView id="GridView1"  CssClass="grid"
                 PagerStyle-CssClass="pgr" DataKeyNames ="RowId" runat="server" 
                 AutoGenerateColumns="False" AllowPaging="True" PageSize ="100"  Width="100%"
                    OnPageIndexChanging="GridView1_PageIndexChanging"   SelectedRowStyle-BackColor="GreenYellow"
                 OnRowCommand="GridView1_RowCommand" OnRowDeleting="GridView1_RowDeleting">
                <RowStyle HorizontalAlign="Center" />
                <Columns>
                    <asp:TemplateField HeaderText="Row Id" Visible="false">
                        <ItemTemplate>
                            <asp:Label ID="lblID" runat="server" Text='<%# Bind("RowId") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>


                            <asp:TemplateField HeaderText="Registration No " Visible="false">
                       
                        <ItemTemplate>
                            <asp:Label ID="PatientReg" runat="server" Text='<%# Bind("PatientReg") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="Patient Name " Visible="false">
                       
                        <ItemTemplate>
                            <asp:Label ID="PatientName" runat="server" Text='<%# Bind("patient_name") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>

                      <asp:TemplateField HeaderText="Discharge Date ">
                       
                        <ItemTemplate>
                            <asp:Label ID="DDate" runat="server" Text='<%# Bind("DDate") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>

                     <asp:TemplateField HeaderText="Discharge Time ">
                       
                        <ItemTemplate>
                            <asp:Label ID="DischargeTime" runat="server" Text='<%# Bind("DischargeTime") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>

                         <asp:TemplateField HeaderText="Comment">
                       
                        <ItemTemplate>
                            <asp:Label ID="Comment" runat="server" Text='<%# Bind("Remarks") %>'></asp:Label>
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
        </ContentTemplate>
        </asp:UpdatePanel>
        </asp:View>
        </asp:MultiView>
        </div>
 
</asp:Content>

