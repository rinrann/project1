<%@ Page Title="" Language="C#" MasterPageFile="~/MasterAll/MasterPageAll.master" AutoEventWireup="true" CodeFile="DialysisPayment.aspx.cs" Inherits="DayCare_DialysisPayment" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
 
<script type="text/javascript" language="javascript">

    function GetDatetime() {
        var now = new Date();
        var day, mnt, yr;
        day = now.getDate();
        mnt = now.getMonth() + 1;
        yr = now.getFullYear();
        if (day < 10)
            day = "0" + day;
        if (mnt < 10)
            mnt = "0" + mnt;

        var datetime = mnt + '/' + day + '/' + yr;
        var hour = now.getHours();
        var minute = now.getMinutes();
        var a;
        if (hour >= 12) {
            hour = hour - 12;
            a = "PM";
        }
        else {
            a = "AM";
            hour = hour;
        }

        if (minute < 10)
            minute = "0" + minute;
        var time = hour + ':' + minute + ' ' + a;
         
        document.getElementById("ctl00_ContentPlaceHolder1_HiddenField1").value = datetime;


    }

    function ShowDialog() {

        var rtvalue = window.open("RegistrationPopup.aspx", "sss", "Width:800px; Height:550px; dialogLeft:250px;");
        //document.getElementById("ctl00_ContentPlaceHolder1_txtreg").value = rtvalue.NameValue;

    }

    function ShowDialog1() {

        var rtvalue = window.open("PaymentSubmitpopup.aspx", "sss", "Width:800px; Height:550px; dialogLeft:250px;");
    }
     

    function Calling() {

 

        $("input[id$='txtamt']").keydown(function (event) {
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
         <asp:Label ID="Label1" runat="server">Dialysis Payment</asp:Label>
     </div>

 


     <div class="formbox">
     <div class="form-sec">
        
            <div class="error">
                <asp:HiddenField ID="HiddenField1" runat="server" />
                <strong><asp:Label ID="lblError" runat="server" Font-Bold="True"></asp:Label>
                </strong>
                <div class="clear"></div>
            </div>
            <asp:HiddenField ID="TextBox4" runat="server" />
         
            <div class="form-sec-row">
                <label style='color:Blue'><strong>Dialysis Details :</strong></label>
                <div class="clear"></div>
            </div>

			<div class="form-sec-row">
                <label><strong>Registration No :</strong></label>
               <asp:TextBox ID="txtreg" runat="server" CssClass="textbox-medium1"  Enabled="false"
                   ></asp:TextBox> <asp:Button ID="Button4" runat="server"  Height="28px"
                    CssClass="submit-search" Text="Quick Search" OnClientClick="ShowDialog()" 
                    />
                   <asp:Button ID="Button5" runat="server" CssClass="submit-button"  Height="28px"
                    Text="Fetch" onclick="Button5_Click" />
                <div class="clear">  </div>
            </div>


                  <div class="form-sec-row">
                <label><strong>Patient Name :</strong></label>
                <asp:TextBox ID="txtpname" runat="server" CssClass="textbox-medium1" Enabled="False" 
                   ></asp:TextBox>
                <div class="clear"></div>
            </div>


           
            <div class="form-sec-row">
                <label><strong>Dialysis Name :</strong></label>
                <asp:TextBox ID="txtname" runat="server" CssClass="textbox-medium1" Enabled="False" 
                   ></asp:TextBox>
                <div class="clear"></div>
            </div>

               
              <div class="form-sec-row"> 
                <label><strong>Appointment Shift :</strong></label>
                <div style="float:left;">
                    <asp:DropDownList ID="DropDownList1" runat="server" CssClass="textbox-medium1" 
                        Enabled="False">
                    </asp:DropDownList>
               </div> 
                <div class="clear"></div>
            </div>

            </div>
            </div>

              <div class="formbox">
     <div class="form-sec">

               <div class="form-sec-row">
                <label style='color:Blue'><strong>Bill Details :</strong></label>
                <div class="clear"></div>
            </div>
            <div class="form-sec-row">
                <label><strong>Total Bill Amount :</strong></label>  
                 <asp:TextBox ID="TextBox2" runat="server" CssClass="textbox-medium1" Enabled="False" 
                   ></asp:TextBox>
                <div class="clear"></div>
            </div>
            <div class="form-sec-row">
                <label><strong>Paid Amount :</strong></label>  
                 <asp:TextBox ID="txtpaid" runat="server" CssClass="textbox-medium1" Enabled="False" 
                   ></asp:TextBox>
                <div class="clear"></div>
            </div>

              <div class="form-sec-row">
                <label><strong> 
                    <asp:Label ID="Label5" runat="server" Text="Due Amount :"></asp:Label> </strong></label>
                 <asp:TextBox ID="TextBox3" runat="server" MaxLength="7"  CssClass="textbox-medium1"  Enabled="false" ></asp:TextBox>
                <div class="clear"></div>
            </div>
 </div></div>

      <div class="formbox">
     <div class="form-sec">
       
      <div class="form-sec-row">
                <label style='color:Blue'><strong>Payment Sattlement :</strong></label>
                <div class="clear"></div>
            </div>

         
        <div class="form-sec-row">
                <label><strong> 
                    <asp:Label ID="Label2" runat="server" Text="Current Payment :"></asp:Label> </strong></label>
                 <asp:TextBox ID="txtamt" runat="server" MaxLength="7" AutoPostBack="true" 
                    CssClass="textbox-medium1" ontextchanged="txtamt_TextChanged"></asp:TextBox>
                <div class="clear"></div>
            </div>

              

                <div class="form-sec-row">
                <label><strong> 
                    <asp:Label ID="Label6" runat="server" Text="Discount :"></asp:Label> </strong></label>
                 <asp:TextBox ID="TextBox5" runat="server" MaxLength="7"  
                        CssClass="textbox-medium1" AutoPostBack="True" 
                        ontextchanged="TextBox5_TextChanged"  ></asp:TextBox>
                <div class="clear"></div>
            </div>
           <div class="form-sec-row">
                <label><strong>Payment Mode :</strong></label>
               <asp:DropDownList ID="DropDownList3" runat="server" CssClass="textbox-medium1"  >
                     </asp:DropDownList>
                <div class="clear"></div>
            </div>
              <div class="form-sec-row">
                <label><strong>Reason :<span style="color:red;">*</span></strong></label>
                <%-- <asp:TextBox ID="txtreason" runat="server" TextMode="MultiLine" Height="40px" CssClass="textbox-medium1"></asp:TextBox>--%>
                   <asp:DropDownList ID="txtreason" runat="server" CssClass="textbox-medium1"  OnSelectedIndexChanged="txtreason_SelectedIndexChanged" AutoPostBack="true">
                     </asp:DropDownList>
                   <asp:TextBox ID="TextBox8" runat="server" TextMode="MultiLine" Height="40px" CssClass="textbox-medium1" Visible="false"></asp:TextBox>
                <div class="clear"></div>
            </div>

            <%--   <div class="form-sec-row">
                <label><strong><asp:Label ID="Label9" runat="server" Text="Reason / Remarks :"></asp:Label></strong></label>
                  <asp:TextBox ID="TextBox6" runat="server" CssClass="textbox-medium1" TextMode="MultiLine" Height="50px"></asp:TextBox>
                <div class="clear"></div>
            </div>--%>
 
                <div class="form-sec-row">
                <label><strong><asp:Label ID="Label3" runat="server" Text="Due Amount :"></asp:Label></strong></label>
                  <asp:TextBox ID="txtdue" runat="server" CssClass="textbox-medium1"  Enabled="false"
                    MaxLength="14"></asp:TextBox>
                <div class="clear"></div>
            </div>

               <div class="form-sec-row">
                <label><strong><asp:Label ID="Label7" runat="server" Text="Final Sattlement :"></asp:Label></strong></label>
                   <asp:CheckBox ID="CheckBox1" runat="server" />
                <div class="clear"></div>
            </div>

                <div class="form-sec-row"  style='display:none;'>
                <label><strong> 
                    <asp:Label ID="Label8" runat="server" Text="Advance :"></asp:Label> </strong></label>
                 <asp:TextBox ID="TextBox7" runat="server" MaxLength="7"  CssClass="textbox-medium1"  ></asp:TextBox>
                <div class="clear"></div>
            </div>
                 

     <div class="form-sec-row" style='display:none;'>
                <label><strong>Payment Sattlement :</strong></label>
         <asp:DropDownList ID="DropDownList2" runat="server" CssClass="textbox-medium1" >
             <asp:ListItem Value="1">Payment</asp:ListItem>
                    <asp:ListItem Value="2">Due</asp:ListItem>
             <asp:ListItem Value="3">Discount</asp:ListItem>
             <asp:ListItem Value="4">Refund</asp:ListItem>
             <asp:ListItem Value="5">Advance</asp:ListItem>
         </asp:DropDownList>
                   <div class="clear">  </div>
            </div>

               <div class="form-sec-row" style='display:none;'>
                <label><strong> 
                    <asp:Label ID="Label4" runat="server" Text="Adjustment Amount :"></asp:Label> </strong></label>
                 <asp:TextBox ID="TextBox1" runat="server"  Enabled="false"
                    CssClass="textbox-medium1" ></asp:TextBox>
                <div class="clear"></div>
            </div>
         
            <div class="form-sec-row">
                <label><strong>&nbsp;</strong></label>
                <asp:Button ID="Button1" runat="server" Height="28px" Text="Submit" 
                    CssClass="submit-button" onclick="Button1_Click"/>
                <asp:Button ID="Button2" runat="server" Height="28px" Text="Cancel" OnClick="Button2_Click" CssClass="submit-button" />
                <div class="clear"></div>
            </div>  
   
     </div>
     </div>
      
    </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>

