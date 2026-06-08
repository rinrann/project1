<%@ Page Title="" Language="C#" MasterPageFile="~/MasterAll/MasterPageAll.master" AutoEventWireup="true" CodeFile="PatientRegistration.aspx.cs" Inherits="Pathology_PatientRegistration" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">

    <script type="text/javascript" language="javascript">
    function ShowDialog() {
        var rtvalue = window.open("RegistrationPopup.aspx", "sss", "Width:800px; Height:550px; dialogLeft:250px;");
        //document.getElementById("ctl00_ContentPlaceHolder1_txtregno").value = rtvalue.NameValue;
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
            if ($("input[id$='txtreg']").val() == '') {
                $("input[id$='txtreg']").addClass('textboxerr');
            }

            if ($("input[id$='txtreg']").val() == '') {
                alert('Please Enter Registration  No !');
                $("input[id$='txtreg']").focus();
                $("input[id$='txtreg']").addClass('textboxerr');
                return false;
            }
            else {
                $("input[id$='txtreg']").removeClass('textboxerr');
            }



            if ($("input[id$='txtname']").val() == '') {
                $("input[id$='txtname']").addClass('textboxerr');
            }

            if ($("input[id$='txtname']").val() == '') {
                alert('Please Enter Name !');
                $("input[id$='txtname']").focus();
                $("input[id$='txtname']").addClass('textboxerr');
                return false;
            }
            else {
                $("input[id$='txtname']").removeClass('textboxerr');
            }



            if ($("input[id$='txtage']").val() == '') {
                $("input[id$='txtage']").addClass('textboxerr');
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
                alert('Select Sex!');
                $("select[id$='rblsex']").focus();
                $("select[id$='rblsex']").addClass('textboxerr');
                return false;
            }
            else {
                $("select[id$='rblsex']").removeClass('textboxerr');
            }



            if ($("input[id$='txtvillage']").val() == '') {
                $("input[id$='txtvillage']").addClass('textboxerr');
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




            if ($("input[id$='txtpo']").val() == '') {
                $("input[id$='txtpo']").addClass('textboxerr');
            }

            if ($("input[id$='txtpo']").val() == '') {
                alert('Please Enter Post Office  !');
                $("input[id$='txtpo']").focus();
                $("input[id$='txtpo']").addClass('textboxerr');
                return false;
            }
            else {
                $("input[id$='txtpo']").removeClass('textboxerr');
            }



            if ($("input[id$='txtps']").val() == '') {
                $("input[id$='txtps']").addClass('textboxerr');
            }

            if ($("input[id$='txtps']").val() == '') {
                alert('Please Enter Police Station!');
                $("input[id$='txtps']").focus();
                $("input[id$='txtps']").addClass('textboxerr');
                return false;
            }
            else {
                $("input[id$='txtps']").removeClass('textboxerr');
            }




            if ($("input[id$='txtdist']").val() == '') {
                $("input[id$='txtdist']").addClass('textboxerr');
            }

            if ($("input[id$='txtdist']").val() == '') {
                alert('Please Enter District !');
                $("input[id$='txtdist']").focus();
                $("input[id$='txtdist']").addClass('textboxerr');
                return false;
            }
            else {
                $("input[id$='txtdist']").removeClass('textboxerr');
            }




            if ($("input[id$='txtpin']").val() == '') {
                $("input[id$='txtpin']").addClass('textboxerr');
            }

            if ($("input[id$='txtpin']").val() == '') {
                alert('Please Enter Pin Code !');
                $("input[id$='txtpin']").focus();
                $("input[id$='txtpin']").addClass('textboxerr');
                return false;
            }
            else {
                $("input[id$='txtpin']").removeClass('textboxerr');
            }


            if ($("select[id$='ddlstate']").val() == '0') {
                alert('Select State!');
                $("select[id$='ddlstate']").focus();
                $("select[id$='ddlstate']").addClass('textboxerr');
                return false;
            }
            else {
                $("select[id$='ddlstate']").removeClass('textboxerr');
            }


            if ($("input[id$='txtcontact1']").val() == '') {
                $("input[id$='txtcontact1']").addClass('textboxerr');
            }

            if ($("input[id$='txtcontact1']").val() == '') {
                alert('Please Enter Contact 1!');
                $("input[id$='txtcontact1']").focus();
                $("input[id$='txtcontact1']").addClass('textboxerr');
                return false;
            }
            else {
                $("input[id$='txtcontact1']").removeClass('textboxerr');
            }



            if ($("input[id$='txtcontact2']").val() == '') {
                $("input[id$='txtcontact2']").addClass('textboxerr');
            }

            if ($("input[id$='txtcontact2']").val() == '') {
                alert('Please Enter Contact 2 !');
                $("input[id$='txtcontact2']").focus();
                $("input[id$='txtcontact2']").addClass('textboxerr');
                return false;
            }
            else {
                $("input[id$='txtcontact2']").removeClass('textboxerr');
            }



            if ($("input[id$='txtamt']").val() == '') {
                $("input[id$='txtamt']").addClass('textboxerr');
            }

            if ($("input[id$='txtamt']").val() == '') {
                alert('Please Enter Adv. Amount !');
                $("input[id$='txtamt']").focus();
                $("input[id$='txtamt']").addClass('textboxerr');
                return false;
            }
            else {
                $("input[id$='txtamt']").removeClass('textboxerr');
            }




            if ($("input[id$='TextBox1']").val() == '') {
                $("input[id$='TextBox1']").addClass('textboxerr');
            }

            if ($("input[id$='TextBox1']").val() == '') {
                alert('Please Enter Due Amount !');
                $("input[id$='TextBox1']").focus();
                $("input[id$='TextBox1']").addClass('textboxerr');
                return false;
            }
            else {
                $("input[id$='TextBox1']").removeClass('textboxerr');
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
            <asp:HiddenField ID="txtPId" runat="server" />
            
            <div class="form-sec-row">
                <label><strong>Registration No :</strong></label>
                <asp:TextBox ID="txtreg" runat="server" CssClass="textbox-medium1" 
                    Enabled="False"  ></asp:TextBox>
                <asp:Button ID="Button3" runat="server" 
                    OnClientClick="ShowDialog();" Height="28px"  Text="Quick Search" CssClass="submit-search" 
                   />
                <asp:Button ID="Button4" runat="server" Text="Fetch" CssClass="submit-cancel"  Height="28px"
                    onclick="Button4_Click" />
                <div class="clear"></div>
            </div>
           
            <div class="form-sec-row">
                <label><strong>Patient's Name :</strong></label>
                <asp:TextBox ID="txtname" runat="server" CssClass="textbox-medium1" ></asp:TextBox>
                <div class="clear"></div>
            </div>

            <div class="form-sec-row">
                <label><strong>Age :</strong></label>
                <asp:TextBox ID="txtage" runat="server" CssClass="textbox-medium1" ></asp:TextBox>
                <div class="clear"></div>
            </div>
            
            <div class="form-sec-row" style="padding-bottom:4px;">
                <label><strong>Sex :</strong></label>
             
                <asp:DropDownList ID="rblsex" runat="server"  CssClass="textbox-medium1" >
                     </asp:DropDownList>
                <div class="clear"></div>
            </div>
            
            <div class="form-sec-row">
                <label><strong>Address - 1 :</strong></label>
                <asp:TextBox ID="txtvillage" runat="server" CssClass="textbox-medium1" ></asp:TextBox>
                <div class="clear"></div>
            </div>

            <div class="form-sec-row">
                <label><strong>Address - 2 :</strong></label>
                <asp:TextBox ID="txtvillage2" runat="server" CssClass="textbox-medium1" ></asp:TextBox>
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
                <asp:TextBox ID="txtpin" runat="server" CssClass="textbox-medium1" ></asp:TextBox>
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

                <div class="clear"></div>
           
            <div class="form-sec-row">
                <label><strong>Advanced Amount :</strong></label>
                <asp:TextBox ID="txtamt" CssClass="textbox-medium1"  runat="server" 
                    Enabled="False"></asp:TextBox>
                <div class="clear"></div>
            </div>

               <div class="form-sec-row">
                <label><strong>Due Amount :</strong></label>
                <asp:TextBox ID="TextBox1" CssClass="textbox-medium1"  runat="server" 
                    Enabled="False"></asp:TextBox>
                <div class="clear"></div>
            </div>
                 
            <div class="form-sec-row">
                <label><strong>&nbsp;</strong></label>
                <asp:Button ID="Button1" runat="server" Text="Submit" CssClass="submit-button"  Height="28px"
                    onclick="Button1_Click" />
                <asp:Button ID="Button2" runat="server" Text="Cancel" CssClass="submit-button"  Height="28px"
                    onclick="Button2_Click" />
                <div class="clear"></div>
            </div>                     

            <div class="clear"></div>
        </div>
    </div>
    </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>

