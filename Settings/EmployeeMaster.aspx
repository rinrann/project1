<%@ Page Language="C#" MasterPageFile="~/MasterAll/MasterPageAll.master" AutoEventWireup="true" CodeFile="EmployeeMaster.aspx.cs" Inherits="Master_EmployeeMaster" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">

 
<script language="javascript" type="text/javascript">
      function Calling() {
        var date = new Date();
        $("input[id$='Calendar1']").datepicker({ dateFormat: 'dd/mm/yy'});

        var date = new Date();
        $("input[id$='Calendar2']").datepicker({ dateFormat: 'dd/mm/yy'});

        $("input[id$='Button1']").click(function () {

            if ($("input[id$='txtEmployeeName']").val() == '') {
                alert('Employee Name Can not be Blank!');
                $("input[id$='txtEmployeeName']").focus();
                $("input[id$='txtEmployeeName']").addClass('textboxerr');
                return false;
            }
            else {
                $("input[id$='txtEmployeeName']").removeClass('textboxerr');
            }



            if ($("input[id$='txtAddress']").val() == '') {
                alert('Address Can not be Blank!');
                $("input[id$='txtAddress']").focus();
                $("input[id$='txtAddress']").addClass('textboxerr');
                return false;
            }
            else {
                $("input[id$='txtAddress']").removeClass('textboxerr');
            }

            if ($("input[id$='txtphn13']").val() == '') {
                alert('Please Enter Phone_1 Properly!');
                $("input[id$='txtphn13']").focus();
                $("input[id$='txtphn13']").addClass('textboxerr');
                return false;
            }
            else {
                $("input[id$='txtphn13']").removeClass('textboxerr');
            }



            if ($("input[id$='Calendar1']").val() == '') {
                alert('Please Enter Joining Date!');
                $("input[id$='Calendar1']").focus();
                $("input[id$='Calendar1']").addClass('textboxerr');
                return false;
            }
            else {
                $("input[id$='Calendar1']").removeClass('textboxerr');
            }



            if ($("select[id$='DropDownList1']").val() == '0') {
                alert('Please Select Designaion !');
                $("select[id$='DropDownList1']").addClass('textboxerr');
                $("select[id$='DropDownList1']").focus();
                return false;
            }
            else {
                $("select[id$='DropDownList1']").removeClass('textboxerr');
            }





            if ($("select[id$='DropDownList2']").val() == '0') {
                alert('Please Select Sex !');
                $("select[id$='DropDownList2']").addClass('textboxerr');
                $("select[id$='DropDownList2']").focus();
                return false;
            }
            else {
                $("select[id$='DropDownList2']").removeClass('textboxerr');
            }



            if ($("input[id$='Calendar2']").val() == '') {
                alert('Please Enter Leaving Date!');
                $("input[id$='Calendar2']").focus();
                $("input[id$='Calendar2']").addClass('textboxerr');
                return false;
            }
            else {
                $("input[id$='Calendar2']").removeClass('textboxerr');
            }

        });



        $("input[id$='txtAge']").keydown(function (event) {
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



        $("input[id$='txtphn13']").keydown(function (event) {
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



        $("input[id$='txtphn23']").keydown(function (event) {
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
                <asp:Label ID="Label1" runat="server">Employee Master</asp:Label>
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
                    <asp:HiddenField ID="HiddenField1" runat="server" Value="0" />
         
                           <div class="form-sec-row">
                        <label>
                        <strong>
                        Employee Name : </strong></label>
                        <asp:TextBox ID="txtEmployeeName" runat="server" CssClass="textbox-medium1" ></asp:TextBox>
                               <span style="color:red">*</span>
                        <div class="clear">
                        </div>
                    </div>
                     <div class="form-sec-row">
                        <label>
                        <strong>
                        Designation : </strong></label> 
                         <asp:DropDownList ID="DropDownList1" runat="server" CssClass="textbox-medium1" >
                         </asp:DropDownList>
                         <span style="color:red">*</span>
                        <div class="clear">
                        </div>
                    </div>

                    <div class="form-sec-row">
                    <label>
                    <strong>Sex :</strong>
                    </label>
                    <asp:DropDownList ID="DropDownList2" runat="server" CssClass="textbox-medium1">
                    </asp:DropDownList>
                        <span style="color:red">*</span>
                    <div class="clear">                        
                    </div>
                    </div>

                    <div class="form-sec-row">
                        <label>
                        <strong>
                        Address :</strong></label>
                        <asp:TextBox ID="txtAddress" runat="server" CssClass="textbox-medium1" ></asp:TextBox>
                        <div class="clear">
                        </div>
                    </div>

                    <div class="form-sec-row">
                        <label>
                        <strong>
                        City :</strong></label>
                        <asp:TextBox ID="txtCity" runat="server" CssClass="textbox-medium1" ></asp:TextBox>
                        <div class="clear">
                        </div>
                    </div>

                    <div class="form-sec-row">
                        <label>
                        <strong>
                        State :</strong></label>
                      <asp:DropDownList ID="DropDownList5" runat="server" CssClass="textbox-medium1">
                    </asp:DropDownList>
                        <div class="clear">
                        </div>
                    </div>

                     <div class="form-sec-row">
                <label>
                <strong>
                Primary Phone No :</strong>
                </label>
                <asp:TextBox ID="txtphn11" runat="server" CssClass="textbox-medium1" Width="50" Enabled="False">+91</asp:TextBox>              
                <asp:TextBox ID="txtphn13" runat="server" CssClass="textbox-medium1" Width="244px" MaxLength="10"></asp:TextBox>
                         <span style="color:red">*</span>
                <div class="clear">
                </div>
                 </div>

                 <div class="form-sec-row">
                <label>
                <strong>
                Alternate Phone No :</strong></label>
                <asp:TextBox ID="txtphn21" runat="server" CssClass="textbox-medium1" Width="50" Enabled="False">+91</asp:TextBox>
                 <asp:TextBox ID="txtphn23" runat="server" CssClass="textbox-medium1" Width="244px" MaxLength="10"></asp:TextBox>
                <div class="clear">
                </div>
                 </div>

                 <div class="form-sec-row">
                <label>
                <strong>
                Joining Date :</strong></label>
                 <asp:TextBox TextMode="Date" ID="Calendar1" runat="server" CssClass="textbox-medium1">
                   </asp:TextBox>
                          <span style="color:red">*</span>
                <div class="clear">
                </div>
            </div>
            
                       <div class="form-sec-row">
                <label>
                <strong>
               Birth Date :</strong></label>
                 <asp:TextBox TextMode="Date" ID="Calendar3" runat="server" CssClass="textbox-medium1">
                   </asp:TextBox>
                          <span style="color:red">*</span>
                <div class="clear">
                </div>
            </div>

            <div class="form-sec-row" style="display:none">
                        <label>
                        <strong>
                        Age :</strong></label>
                        <asp:TextBox ID="txtAge" runat="server" MaxLength="2"  CssClass="textbox-medium1" >0</asp:TextBox>
                        <div class="clear">
                        </div>
                    </div>

                    <div class="form-sec-row">
                    <label>
                    <strong>Nationality</strong>
                    </label>
                    <asp:DropDownList ID="DropDownList3" runat="server" CssClass="textbox-medium1">
                       <asp:ListItem>Indian</asp:ListItem>
                    </asp:DropDownList>
                    <div class="clear">
                    </div>
                    </div>

                     <div class="form-sec-row">
                    <label>
                    <strong>Religion</strong>
                    </label>
                    <asp:DropDownList ID="DropDownList4" runat="server" CssClass="textbox-medium1">
                       </asp:DropDownList>
                    <div class="clear">
                    </div>
                    </div>

                 <div class="form-sec-row">
                <label>
                <strong>
                Retire Date :</strong></label>
                 <asp:TextBox ID="Calendar2" TextMode="Date" runat="server" CssClass="textbox-medium1"> 
                 </asp:TextBox>
                       
                <div class="clear">
                </div>
               </div>
                 
                    <div class="form-sec-row">
                        <div class="clear">
                        </div>
                    </div>
                    <div class="form-sec-row">
                        &nbsp;&nbsp;&nbsp;<label><strong>&nbsp;</strong></label>
                        <asp:Button ID="Button1" runat="server" CssClass="submit-button"  Height="28px"
                            Text="Submit" onclick="Button1_Click"    />
                        <asp:Button ID="Button2" runat="server" CssClass="submit-button"  Height="28px"
                            Text="Cancel" onclick="Button2_Click"    />
                        <div class="clear">
                        </div>
                    </div>
                    <div class="clear">
                    </div>
                </div>
     </asp:View>
                    
                    <asp:View ID="View2" runat="server">
            <div class="listing"  style='width:100%; height:600px; overflow:auto;'>
                <asp:GridView id="GridView1"  Width="100%" CssClass="grid" PagerStyle-CssClass="pgr"
                 DataKeyNames ="EmployeeID" runat="server" 
                 AutoGenerateColumns="False" AllowPaging="True"  PageSize="100"
                 OnRowCommand="GridView1_RowCommand" OnRowDataBound="GridView1_RowDataBound" SelectedRowStyle-BackColor="GreenYellow"
                 OnPageIndexChanging="GridView1_PageIndexChanging" 
                    onrowdeleting="GridView1_RowDeleting" > 
                    <RowStyle HorizontalAlign="Center" />
                    <Columns>
                        <asp:TemplateField HeaderText="Employee ID" Visible="false">
                            <ItemTemplate>
                                <asp:Label ID="EmployeeID" runat="server" Text='<%# Bind("EmployeeID") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Employee Name" ItemStyle-HorizontalAlign="Left">
                            <ItemTemplate>
                                <asp:Label ID="EmployeeName" runat="server" Text='<%# Bind("EmployeeName") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Designation ID" Visible="false">
                            <ItemTemplate>
                                <asp:Label ID="DesignationID" runat="server" Text='<%# Bind("DesignationID") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                         <asp:TemplateField HeaderText="Designation">
                            <ItemTemplate>
                                <asp:Label ID="DesignationName" runat="server" Text='<%# Bind("DesignationName") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>    
                        <asp:TemplateField HeaderText="Sex">
                            <ItemTemplate>
                                <asp:Label ID="Sex" runat="server" Text='<%# Bind("SexName") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField> 
                        <asp:TemplateField HeaderText="Address">
                            <ItemTemplate>
                                <asp:Label ID="Address" runat="server" Text='<%# Bind("Address") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField> 

                         <asp:TemplateField HeaderText="City">
                            <ItemTemplate>
                                <asp:Label ID="City" runat="server" Text='<%# Bind("City") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>

                         <asp:TemplateField HeaderText="State"  Visible="false">
                            <ItemTemplate>
                                <asp:Label ID="State" runat="server" Text='<%# Bind("State") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="PhoneNo_1">
                            <ItemTemplate>
                                <asp:Label ID="PhoneNo_1" runat="server" Text='<%# Bind("PhoneNo_1") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="PhoneNo_2" Visible="false">
                            <ItemTemplate>
                                <asp:Label ID="PhoneNo_2" runat="server" Text='<%# Bind("PhoneNo_2") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="Joining Date">
                            <ItemTemplate>
                                <asp:Label ID="JoiningDate" runat="server" Text='<%# Bind("jdate") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="DOB">
                            <ItemTemplate>
                                <asp:Label ID="dob" runat="server" Text='<%# Bind("dobdt") %>'></asp:Label>
                                <asp:Label ID="Age" runat="server" Text='<%# Bind("Age") %>' style="display:none"></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="Nationality"  Visible="false">
                            <ItemTemplate>
                                <asp:Label ID="Nationality" runat="server" Text='<%# Bind("Nationality") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="Religion"  Visible="false">
                            <ItemTemplate>
                                <asp:Label ID="Religion" runat="server" Text='<%# Bind("Name") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="Leaving Date">
                            <ItemTemplate>
                                <asp:Label ID="LeavingDate" runat="server" Text='<%# Bind("ldate") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                                    
                        <asp:CommandField SelectText="Edit" ShowSelectButton="True" ControlStyle-CssClass="temp" HeaderText="Edit" SelectImageUrl="~/Images/edit.png" ButtonType="Image">
                            <ControlStyle CssClass="temp"></ControlStyle>
                        </asp:CommandField>
                        <asp:CommandField ShowDeleteButton="true" ControlStyle-CssClass="temp" HeaderText="Delete" DeleteImageUrl="~/Images/delete.png" ButtonType="Image" Visible="false">
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