<%@ Page Title="" Language="C#" MasterPageFile="~/MasterAll/MasterPageAll.master" AutoEventWireup="true" CodeFile="PatientReg.aspx.cs" Inherits="DayCare_PatientReg" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">    
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">

<script type="text/javascript" language="javascript">
    function ShowDialog() {
        var rtvalue = window.open("RegistrationPopup.aspx", "sss", "Width:800px; Height:550px; dialogLeft:250px;");
        //document.getElementById("ctl00_ContentPlaceHolder1_txtregno").value = rtvalue.NameValue;
    }

    function ShowDialog1() {
        var rtvalue = window.open("BedAllocationPopup.aspx", "sss", "Width:800px; Height:550px; dialogLeft:250px;");
        //document.getElementById("ctl00_ContentPlaceHolder1_TextBox19").value = rtvalue.NameValue;
        //document.getElementById("ctl00_ContentPlaceHolder1_HiddenField2").value = rtvalue.ProfessionValue;
    }


    function Calling() {
        $("input[id$='Button1']").click(function () {

            if ($("input[id$='txtreg']").val() == '') {
                alert('Please Enter Registration No !');
                $("input[id$='txtreg']").focus();
                $("input[id$='txtreg']").addClass('textboxerr');
                return false;
            }
            else {
                $("input[id$='txtreg']").removeClass('textboxerr');
            }




            if ($("input[id$='txtname']").val() == '') {
                alert('Please Enter First Name !');
                $("input[id$='txtname']").focus();
                $("input[id$='txtname']").addClass('textboxerr');
                return false;
            }
            else {
                $("input[id$='txtname']").removeClass('textboxerr');
            }



            if ($("input[id$='txtlname']").val() == '') {
                alert('Please Enter Last Name !');
                $("input[id$='txtlname']").focus();
                $("input[id$='txtlname']").addClass('textboxerr');
                return false;
            }
            else {
                $("input[id$='txtlname']").removeClass('textboxerr');
            }



            if ($("input[id$='TextBox19']").val() == '') {
                alert('Please Select a Bed !');
                $("input[id$='TextBox19']").focus();
                $("input[id$='TextBox19']").addClass('textboxerr');
                return false;
            }
            else {
                $("input[id$='TextBox19']").removeClass('textboxerr');
            }

            if ($("input[id$='txtage']").val() == '') {
                alert('Please Enter Age !');
                $("input[id$='txtage']").focus();
                $("input[id$='txtage']").addClass('textboxerr');
                return false;
            }
            else {
                $("input[id$='txtage']").removeClass('textboxerr');
            }


            if ($("select[id$='rblsex']").val() == '0') {
                alert('Select Gender !');
                $("select[id$='rblsex']").focus();
                $("select[id$='rblsex']").addClass('textboxerr');
                return false;
            }
            else {
                $("select[id$='rblsex']").removeClass('textboxerr');
            }


            if ($("input[id$='txtvillage']").val() == '') {
                alert('Please Enter Address !');
                $("input[id$='txtvillage']").focus();
                $("input[id$='txtvillage']").addClass('textboxerr');
                return false;
            }
            else {
                $("input[id$='txtvillage']").removeClass('textboxerr');
            }


            if ($("input[id$='txtcontact1']").val() == '') {
                alert('Please Enter Contact No - 1 !');
                $("input[id$='txtcontact1']").focus();
                $("input[id$='txtcontact1']").addClass('textboxerr');
                return false;
            }
            else {
                $("input[id$='txtcontact1']").removeClass('textboxerr');
            }



            if ($("select[id$='ddldialysertype']").val() == '0') {
                alert('Select Dialyser Type!');
                $("select[id$='ddldialysertype']").focus();
                $("select[id$='ddldialysertype']").addClass('textboxerr');
                return false;
            }
            else {
                $("select[id$='ddldialysertype']").removeClass('textboxerr');
            }







            if ($("select[id$='ddldialysername']").val() == '0') {
                alert('Select Dialyser Name!');
                $("select[id$='ddldialysername']").focus();
                $("select[id$='ddldialysername']").addClass('textboxerr');
                return false;
            }
            else {
                $("select[id$='ddldialysername']").removeClass('textboxerr');
            }

            if ($("select[id$='ddlstate']").val() == '0') {
                alert('Please Select State !');
                $("select[id$='ddlstate']").focus();
                $("select[id$='ddlstate']").addClass('textboxerr');
                return false;
            }
            else {
                $("select[id$='ddlstate']").removeClass('textboxerr');
            }

            if ($("select[id$='DropDownList1']").val() == '0') {
                alert('Select District !');
                $("select[id$='DropDownList1']").focus();
                $("select[id$='DropDownList1']").addClass('textboxerr');
                return false;
            }
            else {
                $("select[id$='DropDownList1']").removeClass('textboxerr');
            }


            if ($("input[id$='ddlamount']").val() == '') {
                alert('Please Enter Amount !');
                $("input[id$='ddlamount']").focus();
                $("input[id$='ddlamount']").addClass('textboxerr');
                return false;
            }
            else {
                $("input[id$='ddlamount']").removeClass('textboxerr');
            }
        });


        $("input[id$='txtage']").keydown(function (event) {
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


        $("input[id$='txtpin']").keydown(function (event) {
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

        $("input[id$='txtcontact1']").keydown(function (event) {
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


        $("input[id$='txtcontact2']").keydown(function (event) {
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
         <asp:Label ID="Label1" runat="server">Patient Registration</asp:Label>
    </div>
    <div class="formbox">
        <div class="form-sec">
            <div class="error">
                <strong><asp:Label ID="lblError" runat="server" Font-Bold="True"></asp:Label></strong>
                <div class="clear"></div>
            </div>
            <asp:HiddenField ID="txtPId" runat="server" /><asp:HiddenField ID="HiddenField1" runat="server" /><asp:HiddenField ID="HiddenField2" runat="server" />
            <asp:HiddenField ID="HiddenField3" runat="server" /><asp:HiddenField ID="HiddenField4" runat="server" />
            
            <div class="form-sec-row">
                <label><strong>Registration No :</strong></label>
                <asp:TextBox ID="txtreg" runat="server" CssClass="textbox-medium1" 
                    Enabled="False"  ></asp:TextBox>
                <asp:Button ID="Button3" runat="server" 
                    OnClientClick="ShowDialog();" Text="Quick Search" CssClass="submit-search"  Height="28px"
                   />
                <asp:Button ID="Button4" runat="server" Text="Fetch" CssClass="submit-cancel"    Height="28px"
                    onclick="Button4_Click" />
                <div class="clear"></div>
            </div>
           
            <div class="form-sec-row">
                <label><strong>First Name :</strong></label>
                <asp:TextBox ID="txtname" runat="server" CssClass="textbox-medium1" ></asp:TextBox>
                <div class="clear"></div>
            </div>
                     <div class="form-sec-row">
                <label><strong>Last Name :</strong></label>
                <asp:TextBox ID="txtlname" runat="server" CssClass="textbox-medium1" ></asp:TextBox>
                <div class="clear"></div>
            </div>


                    <div class="form-sec-row">
                <label><strong>C/O :</strong></label>
                <asp:TextBox ID="TextBox2" runat="server" CssClass="textbox-medium1" ></asp:TextBox>
                <div class="clear"></div>
            </div>
 
            <div class="form-sec-row">
                <label><strong>Age :</strong></label>
                <asp:TextBox ID="txtage" runat="server"  MaxLength="3" CssClass="textbox-medium1" ></asp:TextBox>
                <div class="clear"></div>
            </div>
            
            <div class="form-sec-row" style="padding-bottom:4px;">
                <label><strong>Sex :</strong></label>
        
                <asp:DropDownList ID="rblsex" runat="server"  CssClass="textbox-medium1" >
                   
                </asp:DropDownList>
                <div class="clear"></div>
            </div>
            
            <div class="form-sec-row">
                <label><strong>Village/City :</strong></label>
                <asp:TextBox ID="txtvillage" runat="server" CssClass="textbox-medium1" ></asp:TextBox>
                <div class="clear"></div>
            </div>

            <div class="form-sec-row">
                <label><strong>Post Office :</strong></label>
                <asp:TextBox ID="txtpo" runat="server" CssClass="textbox-medium1" ></asp:TextBox>
                <div class="clear"></div>
            </div>

            <div class="form-sec-row">
                <label><strong>Police Station :</strong></label>
                <asp:TextBox ID="txtps" runat="server" CssClass="textbox-medium1" ></asp:TextBox>
                <div class="clear"></div>
            </div>

            <div class="form-sec-row">
                <label><strong>District :</strong></label>
                <asp:DropDownList ID="DropDownList1" runat="server" CssClass="textbox-medium1">
                </asp:DropDownList>
                <div class="clear"></div>
            </div>

            <div class="form-sec-row">
                <label><strong>Pin Code :</strong></label>
                <asp:TextBox ID="txtpin" runat="server" MaxLength="6" CssClass="textbox-medium1" ></asp:TextBox>
                <div class="clear"></div>
            </div>
            
            <div class="form-sec-row">
                <label><strong>State :</strong></label>
                <asp:DropDownList ID="ddlstate" runat="server" CssClass="textbox-medium1" >
                </asp:DropDownList>
                <div class="clear"></div>
            </div>

            
                      
            <div class="form-sec-row">
                <label><strong>Contact No :</strong></label>
                <asp:TextBox ID="TextBox9" runat="server" CssClass="textbox-medium1" Width="50" Enabled="False">+91</asp:TextBox>
            
                 <asp:TextBox ID="txtcontact1" runat="server" CssClass="textbox-medium1" 
                    MaxLength="10" Width="246px" ></asp:TextBox>
                <div class="clear"></div>
            </div>

                <div class="form-sec-row">
                <label><strong>Alternative Contact No. :</strong></label>
                <asp:TextBox ID="TextBox17" runat="server" CssClass="textbox-medium1" Width="50" Enabled="False">+91</asp:TextBox>
                <asp:TextBox ID="txtcontact2" runat="server" CssClass="textbox-medium1" 
                    MaxLength="10" Width="246px" ></asp:TextBox>
                <div class="clear"></div>
            </div>

            
            <div class="form-sec-row">
                <label><strong>Email Id :</strong></label>
                <asp:TextBox ID="TextBox1" runat="server" CssClass="textbox-medium1" ></asp:TextBox>
                <div class="clear"></div>
            </div>

                   <div class="form-sec-row">
                <label><strong>Bed Allocation :</strong></label>
                 <asp:TextBox ID="TextBox19" runat="server" CssClass="textbox-medium1" 
                        Enabled="False" ></asp:TextBox>
        <asp:Button ID="Button9" runat="server" Text="Search"  Height="27px"  OnClientClick="ShowDialog1()" CssClass="submit-button" />

                <div class="clear"></div>
            </div>

               <div class="form-sec-row">
               <div style='float:left;'>
                    <label><strong>Previous Dialysis Date :</strong></label></div>
                   <div style='float:left; padding-left:5px;'>
                 <asp:TextBox ID="TextBox3" runat="server" CssClass="textbox-medium1" Width="190px" Enabled="false"  ></asp:TextBox></div>
                 <div style='float:left;padding-left:5px; width:45px;'>
       <label> <strong>Days :
          </strong></label></div>
           <div style='float:left;padding-left:1px;'>
         <asp:TextBox ID="TextBox4" runat="server" CssClass="textbox-medium1" Width="52px" Enabled="false"  ></asp:TextBox></div>
                <div class="clear"></div>
                </div>
        
            

            <div class="form-sec-row">
                <label><strong>Dialysis Type :</strong></label>
                <asp:DropDownList ID="ddldialysertype" runat="server" CssClass="combo-big22" Width="255px"
                    AutoPostBack="True" onselectedindexchanged="ddldialysertype_SelectedIndexChanged" 
                   >
                </asp:DropDownList>
                <asp:Button ID="Button5" runat="server" CssClass="submit-button" Text="Add" Height="27px"  onclick="Button5_Click" 
                     />
                <div class="clear"></div>
            </div>
            
            <div class="form-sec-row">
                <label><strong>Dialyser Model Name :</strong></label>
                <asp:DropDownList ID="ddldialysername" runat="server" CssClass="combo-big1" 
                    AutoPostBack="True" onselectedindexchanged="ddldialysername_SelectedIndexChanged" 
                   >
                </asp:DropDownList>
                <asp:Label ID="lbltxt" runat="server" Text=" " 
                    style="font-size: small; font-family: 'Times New Roman', Times, serif;"></asp:Label>
                <div class="clear"></div>
            </div>
            <div class="form-sec-row">
                <label><strong>Dialyser No :</strong></label>
                <asp:TextBox ID="txtdialyserno" runat="server" CssClass="textbox-medium1" 
                    Enabled="False" ></asp:TextBox><asp:Label ID="lbldia" runat="server" Text=""></asp:Label>
             <asp:Button ID="Button6" runat="server" CssClass="submit-buttonCheck" Text="Change Dialysis" Height="27px" Width="120px" onclick="Button6_Click" 
                     />
                    </div>
                <div class="clear"></div>
           
          <%--  <div class="form-sec-row">
                <label><strong>Amount :</strong></label>
                <asp:TextBox ID="ddlamount" CssClass="textbox-medium1"  runat="server"></asp:TextBox>
                <div class="clear"></div>
            </div>--%>
                <%-- 
                        <div class="form-sec-row">
                <label><strong>Due Amount :</strong></label>
                <asp:TextBox ID="TextBox1" CssClass="textbox-medium1"  runat="server" 
                    Enabled="False"></asp:TextBox>
                <div class="clear"></div>
            </div>--%>
            <div class="form-sec-row">
                <label><strong>&nbsp;</strong></label>
                <asp:Button ID="Button1" runat="server"  Height="28px" OnClick="Button1_Click" Text="Submit" CssClass="submit-button" />
                <asp:Button ID="Button2" runat="server"  Height="28px" OnClick="Button2_Click" Text="Cancel" CssClass="submit-button" />
                <div class="clear"></div>
            </div>                     

            <div class="clear"></div>
        </div>
    </div>
    <center>  
    <div class="listing">
        
         <asp:GridView id="GridView1" CssClass="grid" Width="50%" PagerStyle-CssClass="pgr" AlternatingRowStyle-CssClass="alt"
            DataKeyNames ="PatientReg" 
            runat="server" AutoGenerateColumns="False" AllowPaging="True" 
             PageSize ="10">
                <Columns>

                <%--   <asp:TemplateField HeaderText="Patient Registration No.">
                        <ItemTemplate>
                            <asp:Label ID="lblPatientId" runat="server" Text='<%# Bind("PatientReg") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="Patient Name">
                        <ItemTemplate>
                            <asp:Label ID="lblPatientName"  runat="server" Text='<%# Bind("patient_name") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>--%>
        
            
                    
                        <asp:TemplateField HeaderText="Dialyser No" ItemStyle-HorizontalAlign="Center">
                        <ItemTemplate>
                            <asp:Label ID="lbladdress"  runat="server" Text='<%# Bind("DialysisNo") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="No of Dialyser" ItemStyle-HorizontalAlign="Center">
                        <ItemTemplate>
                            <asp:Label ID="lblstate" runat="server" Text='<%# Bind("Total") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
       
                </Columns>
                <PagerStyle CssClass="pgr"></PagerStyle>
                <EditRowStyle BackColor="#CCFF33" />
                <AlternatingRowStyle BackColor="#FFDB91" />
        </asp:GridView>
           </div>
      </center>
    
    </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>

