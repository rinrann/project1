<%@ Page Title="" Language="C#" MasterPageFile="~/MasterAll/MasterPageAll.master" AutoEventWireup="true" CodeFile="ReagentEntry.aspx.cs" Inherits="Pathology_ReagentEntry" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
<script type="text/javascript" language="javascript">
    function ShowDialog() {

        var rtvalue = window.open("ReagentPopup.aspx", "sss", "Width:800px; Height:550px; dialogLeft:250px;");
        document.getElementById("ctl00_ContentPlaceHolder1_txtdate").value = rtvalue.ProfessionValue;
        document.getElementById("ctl00_ContentPlaceHolder1_TxtDocno").value = rtvalue.NameValue;
    }

    function calculate() {
        var a = document.getElementById("ctl00_ContentPlaceHolder1_TextBox2").value;
        var b = document.getElementById("ctl00_ContentPlaceHolder1_TextBox4").value;
        var c = a * b;
        document.getElementById("ctl00_ContentPlaceHolder1_TextBox5").value = c;



        var a = document.getElementById("ctl00_ContentPlaceHolder1_TextBox8").value;
        var b = document.getElementById("ctl00_ContentPlaceHolder1_TextBox10").value;
        var c = a * b;
        document.getElementById("ctl00_ContentPlaceHolder1_TextBox11").value = c;



        var a = document.getElementById("ctl00_ContentPlaceHolder1_TextBox14").value;
        var b = document.getElementById("ctl00_ContentPlaceHolder1_TextBox16").value;
        var c = a * b;
        document.getElementById("ctl00_ContentPlaceHolder1_TextBox17").value = c;



        var a = document.getElementById("ctl00_ContentPlaceHolder1_TextBox20").value;
        var b = document.getElementById("ctl00_ContentPlaceHolder1_TextBox22").value;
        var c = a * b;
        document.getElementById("ctl00_ContentPlaceHolder1_TextBox23").value = c;



        var a = document.getElementById("ctl00_ContentPlaceHolder1_TextBox26").value;
        var b = document.getElementById("ctl00_ContentPlaceHolder1_TextBox28").value;
        var c = a * b;
        document.getElementById("ctl00_ContentPlaceHolder1_TextBox29").value = c;



        var a = document.getElementById("ctl00_ContentPlaceHolder1_TextBox32").value;
        var b = document.getElementById("ctl00_ContentPlaceHolder1_TextBox34").value;
        var c = a * b;
        document.getElementById("ctl00_ContentPlaceHolder1_TextBox35").value = c;



        var a = document.getElementById("ctl00_ContentPlaceHolder1_TextBox38").value;
        var b = document.getElementById("ctl00_ContentPlaceHolder1_TextBox40").value;
        var c = a * b;
        document.getElementById("ctl00_ContentPlaceHolder1_TextBox41").value = c;




        var a = document.getElementById("ctl00_ContentPlaceHolder1_TextBox44").value;
        var b = document.getElementById("ctl00_ContentPlaceHolder1_TextBox46").value;
        var c = a * b;
        document.getElementById("ctl00_ContentPlaceHolder1_TextBox47").value = c;



        var a = document.getElementById("ctl00_ContentPlaceHolder1_TextBox50").value;
        var b = document.getElementById("ctl00_ContentPlaceHolder1_TextBox52").value;
        var c = a * b;
        document.getElementById("ctl00_ContentPlaceHolder1_TextBox53").value = c;



        var a = document.getElementById("ctl00_ContentPlaceHolder1_TextBox56").value;
        var b = document.getElementById("ctl00_ContentPlaceHolder1_TextBox58").value;
        var c = a * b;
        document.getElementById("ctl00_ContentPlaceHolder1_TextBox59").value = c;


        var a = document.getElementById("ctl00_ContentPlaceHolder1_TextBox62").value;
        var b = document.getElementById("ctl00_ContentPlaceHolder1_TextBox64").value;
        var c = a * b;
        document.getElementById("ctl00_ContentPlaceHolder1_TextBox65").value = c;

    }
    function Calling() {

        var date = new Date();
        $('.DatepickerCall').datepicker({ dateFormat: 'dd/mm/yy' });
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
         <asp:Label ID="Label1" runat="server">Reagent / Kit Entry</asp:Label>
     </div>
     <div class="formbox">
     <div class="form-sec">
        
            <div class="error">
                <strong><asp:Label ID="lblError" runat="server" Font-Bold="True"></asp:Label>
                </strong>
                <div class="clear"></div>
            </div>
            <asp:HiddenField ID="HiddenField1" runat="server" />
			           
             <div class="form-sec-row">
                <br />
                <label><strong>Doc No :</strong></label>
                  <asp:TextBox ID="TxtDocno" runat="server" CssClass="textbox-medium1" Enabled="false"></asp:TextBox>
                <asp:Button ID="Button2" runat="server" Text="Search" Height="28px" CssClass="submit-button" OnClientClick="return ShowDialog();"/>
                  <asp:Button ID="Button3" runat="server"   Height="28px"
                          Text="Fetch" CssClass="submit-button" onclick="Button3_Click" />
                <div class="clear"></div>
            </div>           
            <div class="form-sec-row">
                <br />
                <label><strong>Date :</strong></label>
                  <asp:TextBox ID="txtdate" runat="server" CssClass="DatepickerCall"></asp:TextBox>
                <div class="clear"></div>
            </div>           
            <div class="form-sec-row">
                <br />
                <label><strong>Supplier :</strong></label>
                  <asp:DropDownList ID="ddlSupplier" runat="server" CssClass="textbox-medium1" AppendDataBoundItems="true">
                            </asp:DropDownList>
                <div class="clear"></div>
            </div> 
   
     </div>
     </div>
       <center>
    <table border="1" cellpadding="0" cellspacing="0" width="100%">
          
       <tr style='background-color:#FF9300;'>
                <td align="center">
                  <div class="form-sec-row"> 
             <label class="lname"><strong>Reagent    Name</strong></label> 
            </div>
        </td> 
<td align="center">
                    <div class="form-sec-row"> 
             <label class="lname"><strong>Batch No</strong></label> 
            </div>
            
</td>
                <!--<td align="center">
                    <div class="form-sec-row"> 
             <label class="lname"><strong>Company</strong></label>
            </div>
</td>
                <td align="center">
                             <div class="form-sec-row"> 
             <label class="lname"><strong>Supplier</strong></label>
            </div>                  
</td>
 <td align="center">
                    <div class="form-sec-row"> 
             <label class="lname"><strong>Manufacturer</strong></label> 
            </div>
            
</td>-->
 <td align="center">
                    <div class="form-sec-row"> 
             <label class="lname"><strong>Quantity</strong></label> 
            </div>
            
</td>
 <td align="center" style="display:none;">
                    <div class="form-sec-row"> 
             <label class="lname"><strong>Bonus Qty</strong></label> 
            </div>
            
</td>
 <td align="center">
                    <div class="form-sec-row"> 
             <label class="lname"><strong>Price</strong></label> 
            </div>
            
</td>
 <td align="center">
                    <div class="form-sec-row"> 
             <label class="lname"><strong>Total Price</strong></label> 
            </div>
            
</td>
 <td align="center">
                    <div class="form-sec-row"> 
             <label class="lname"><strong>Expiry Date</strong></label> 
            </div>
            
</td>
                <td>
                                
</td>             
                      
            </tr>
                <tr>
                <td align="center">
                    <div class="form-sec-row"> 
             <asp:DropDownList ID="DropDownList1" runat="server" CssClass="textbox-dropdown" AppendDataBoundItems="true">
                            </asp:DropDownList>
            </div>
            
</td> 
<td align="center">
                    <div class="form-sec-row"> 
             <asp:TextBox ID="TextBox1" runat="server" CssClass="textbox-dropdown"></asp:TextBox>
            </div>
            
</td>
                <!--<td align="center">
                    <div class="form-sec-row"> 
             <asp:DropDownList ID="DropDownList2" runat="server" CssClass="textbox-dropdown" AppendDataBoundItems="true">
                            </asp:DropDownList>
            </div>
</td>
                <td align="center">
                             <div class="form-sec-row"> 
             <asp:DropDownList ID="DropDownList3" runat="server" CssClass="textbox-dropdown" AppendDataBoundItems="true">
                            </asp:DropDownList>
            </div>                  
</td>
 <td align="center">
                    <div class="form-sec-row"> 
            <asp:DropDownList ID="DropDownList4" runat="server" CssClass="textbox-dropdown" AppendDataBoundItems="true">
                              </asp:DropDownList>
            </div>
            
</td>-->
 <td align="center">
                    <div class="form-sec-row"> 
             <asp:TextBox ID="TextBox2" runat="server" CssClass="textbox-entry"></asp:TextBox>
            </div>
            
</td>
 <td align="center" style="display:none;">
                    <div class="form-sec-row"> 
             <asp:TextBox ID="TextBox3" runat="server" CssClass="textbox-entry"></asp:TextBox>
            </div>
            
</td>
 <td align="center">
                    <div class="form-sec-row"> 
            <asp:TextBox ID="TextBox4" runat="server" CssClass="textbox-entry"  onblur="return calculate();"></asp:TextBox>
            </div>
            
</td>
 <td align="center">
                    <div class="form-sec-row"> 
              <asp:TextBox ID="TextBox5" runat="server" CssClass="textbox-entry" Enabled="False"></asp:TextBox>
            </div>
            
</td>
 <td align="center">
                    <div class="form-sec-row"> 
              <asp:TextBox ID="TextBox6" runat="server" Width="140px" CssClass="DatepickerCall"></asp:TextBox>
            </div>
            
</td>   </tr>

<tr>
                <td align="center">
                    <div class="form-sec-row"> 
             <asp:DropDownList ID="DropDownList5" runat="server" CssClass="textbox-dropdown" AppendDataBoundItems="true">
                            </asp:DropDownList>
            </div>
            
</td> 
<td align="center">
                    <div class="form-sec-row"> 
             <asp:TextBox ID="TextBox7" runat="server" CssClass="textbox-dropdown"></asp:TextBox>
            </div>
            
</td>
             <!--<td align="center">
                    <div class="form-sec-row"> 
             <asp:DropDownList ID="DropDownList6" runat="server" CssClass="textbox-dropdown" AppendDataBoundItems="true">
                            </asp:DropDownList>
            </div>
</td>
                <td align="center">
                             <div class="form-sec-row"> 
             <asp:DropDownList ID="DropDownList7" runat="server" CssClass="textbox-dropdown" AppendDataBoundItems="true">
                            </asp:DropDownList>
            </div>                  
</td>
 <td align="center">
                    <div class="form-sec-row"> 
            <asp:DropDownList ID="DropDownList8" runat="server" CssClass="textbox-dropdown" AppendDataBoundItems="true">
                              </asp:DropDownList>
            </div>
            
</td>-->
 <td align="center">
                    <div class="form-sec-row"> 
             <asp:TextBox ID="TextBox8" runat="server" CssClass="textbox-entry"></asp:TextBox>
            </div>
            
</td>
 <td align="center" style="display:none;">
                    <div class="form-sec-row"> 
             <asp:TextBox ID="TextBox9" runat="server" CssClass="textbox-entry"></asp:TextBox>
            </div>
            
</td>
 <td align="center">
                    <div class="form-sec-row"> 
            <asp:TextBox ID="TextBox10" runat="server" CssClass="textbox-entry" onblur="return calculate();"></asp:TextBox>
            </div>
            
</td>
 <td align="center">
                    <div class="form-sec-row"> 
              <asp:TextBox ID="TextBox11" runat="server" CssClass="textbox-entry" Enabled="False"></asp:TextBox>
            </div>
            
</td>
 <td align="center">
                    <div class="form-sec-row"> 
              <asp:TextBox ID="TextBox12" runat="server" Width="140px" CssClass="DatepickerCall"></asp:TextBox>
            </div>
            
</td>   </tr>

<tr>
                <td align="center">
                    <div class="form-sec-row"> 
             <asp:DropDownList ID="DropDownList9" runat="server" CssClass="textbox-dropdown" AppendDataBoundItems="true">
                            </asp:DropDownList>
            </div>
            
</td> 
<td align="center">
                    <div class="form-sec-row"> 
             <asp:TextBox ID="TextBox13" runat="server" CssClass="textbox-dropdown"></asp:TextBox>
            </div>
            
</td>
                <!--<td align="center">
                    <div class="form-sec-row"> 
             <asp:DropDownList ID="DropDownList10" runat="server" CssClass="textbox-dropdown" AppendDataBoundItems="true">
                            </asp:DropDownList>
            </div>
</td>
                <td align="center">
                             <div class="form-sec-row"> 
             <asp:DropDownList ID="DropDownList11" runat="server" CssClass="textbox-dropdown" AppendDataBoundItems="true">
                            </asp:DropDownList>
            </div>                  
</td>
 <td align="center">
                    <div class="form-sec-row"> 
            <asp:DropDownList ID="DropDownList12" runat="server" CssClass="textbox-dropdown" AppendDataBoundItems="true">
                              </asp:DropDownList>
            </div>
            
</td>-->
 <td align="center">
                    <div class="form-sec-row"> 
             <asp:TextBox ID="TextBox14" runat="server" CssClass="textbox-entry"></asp:TextBox>
            </div>
            
</td>
 <td align="center" style="display:none;">
                    <div class="form-sec-row"> 
             <asp:TextBox ID="TextBox15" runat="server" CssClass="textbox-entry"></asp:TextBox>
            </div>
            
</td>
 <td align="center">
                    <div class="form-sec-row"> 
            <asp:TextBox ID="TextBox16" runat="server" CssClass="textbox-entry" onblur="return calculate();"></asp:TextBox>
            </div>
            
</td>
 <td align="center">
                    <div class="form-sec-row"> 
              <asp:TextBox ID="TextBox17" runat="server" CssClass="textbox-entry" Enabled="False"></asp:TextBox>
            </div>
            
</td>
 <td align="center">
                    <div class="form-sec-row"> 
              <asp:TextBox ID="TextBox18" runat="server" Width="140px" CssClass="DatepickerCall"></asp:TextBox>
            </div>
            
</td>   </tr>

<tr>
                <td align="center">
                    <div class="form-sec-row"> 
             <asp:DropDownList ID="DropDownList13" runat="server" CssClass="textbox-dropdown" AppendDataBoundItems="true">
                            </asp:DropDownList>
            </div>
            
</td> 
<td align="center">
                    <div class="form-sec-row"> 
             <asp:TextBox ID="TextBox19" runat="server" CssClass="textbox-dropdown"></asp:TextBox>
            </div>
            
</td>
             <!--<td align="center">
                    <div class="form-sec-row"> 
             <asp:DropDownList ID="DropDownList14" runat="server" CssClass="textbox-dropdown" AppendDataBoundItems="true">
                            </asp:DropDownList>
            </div>
</td>
                <td align="center">
                             <div class="form-sec-row"> 
             <asp:DropDownList ID="DropDownList15" runat="server" CssClass="textbox-dropdown" AppendDataBoundItems="true">
                            </asp:DropDownList>
            </div>                  
</td>
 <td align="center">
                    <div class="form-sec-row"> 
            <asp:DropDownList ID="DropDownList16" runat="server" CssClass="textbox-dropdown" AppendDataBoundItems="true">
                              </asp:DropDownList>
            </div>
            
</td>-->
 <td align="center">
                    <div class="form-sec-row"> 
             <asp:TextBox ID="TextBox20" runat="server" CssClass="textbox-entry"></asp:TextBox>
            </div>
            
</td>
 <td align="center" style="display:none;">
                    <div class="form-sec-row"> 
             <asp:TextBox ID="TextBox21" runat="server" CssClass="textbox-entry"></asp:TextBox>
            </div>
            
</td>
 <td align="center">
                    <div class="form-sec-row"> 
            <asp:TextBox ID="TextBox22" runat="server" CssClass="textbox-entry" onblur="return calculate();"></asp:TextBox>
            </div>
            
</td>
 <td align="center">
                    <div class="form-sec-row"> 
              <asp:TextBox ID="TextBox23" runat="server" CssClass="textbox-entry" Enabled="False"></asp:TextBox>
            </div>
            
</td>
 <td align="center">
                    <div class="form-sec-row"> 
              <asp:TextBox ID="TextBox24" runat="server" Width="140px" CssClass="DatepickerCall"></asp:TextBox>
            </div>
            
</td>   </tr>

<tr>
                <td align="center">
                    <div class="form-sec-row"> 
             <asp:DropDownList ID="DropDownList17" runat="server" CssClass="textbox-dropdown" AppendDataBoundItems="true">
                            </asp:DropDownList>
            </div>
            
</td> 
<td align="center">
                    <div class="form-sec-row"> 
             <asp:TextBox ID="TextBox25" runat="server" CssClass="textbox-dropdown"></asp:TextBox>
            </div>
            
</td>
             <!--<td align="center">
                    <div class="form-sec-row"> 
             <asp:DropDownList ID="DropDownList18" runat="server" CssClass="textbox-dropdown" AppendDataBoundItems="true">
                            </asp:DropDownList>
            </div>
</td>
                <td align="center">
                             <div class="form-sec-row"> 
             <asp:DropDownList ID="DropDownList19" runat="server" CssClass="textbox-dropdown" AppendDataBoundItems="true">
                            </asp:DropDownList>
            </div>                  
</td>
 <td align="center">
                    <div class="form-sec-row"> 
            <asp:DropDownList ID="DropDownList20" runat="server" CssClass="textbox-dropdown" AppendDataBoundItems="true">
                              </asp:DropDownList>
            </div>
            
</td>-->
 <td align="center">
                    <div class="form-sec-row"> 
             <asp:TextBox ID="TextBox26" runat="server" CssClass="textbox-entry"></asp:TextBox>
            </div>
            
</td>
 <td align="center" style="display:none;">
                    <div class="form-sec-row"> 
             <asp:TextBox ID="TextBox27" runat="server" CssClass="textbox-entry"></asp:TextBox>
            </div>
            
</td>
 <td align="center">
                    <div class="form-sec-row"> 
            <asp:TextBox ID="TextBox28" runat="server" CssClass="textbox-entry" onblur="return calculate();"></asp:TextBox>
            </div>
            
</td>
 <td align="center">
                    <div class="form-sec-row"> 
              <asp:TextBox ID="TextBox29" runat="server" CssClass="textbox-entry" Enabled="False"></asp:TextBox>
            </div>
            
</td>
 <td align="center">
                    <div class="form-sec-row"> 
              <asp:TextBox ID="TextBox30" runat="server" Width="140px" CssClass="DatepickerCall"></asp:TextBox>
            </div>
            
</td>   </tr>

<tr>
                <td align="center">
                    <div class="form-sec-row"> 
             <asp:DropDownList ID="DropDownList21" runat="server" CssClass="textbox-dropdown" AppendDataBoundItems="true">
                            </asp:DropDownList>
            </div>
            
</td> 
<td align="center">
                    <div class="form-sec-row"> 
             <asp:TextBox ID="TextBox31" runat="server" CssClass="textbox-dropdown"></asp:TextBox>
            </div>
            
</td>
             <!--<td align="center">
                    <div class="form-sec-row"> 
             <asp:DropDownList ID="DropDownList22" runat="server" CssClass="textbox-dropdown" AppendDataBoundItems="true">
                            </asp:DropDownList>
            </div>
</td>
                <td align="center">
                             <div class="form-sec-row"> 
             <asp:DropDownList ID="DropDownList23" runat="server" CssClass="textbox-dropdown" AppendDataBoundItems="true">
                            </asp:DropDownList>
            </div>                  
</td>
 <td align="center">
                    <div class="form-sec-row"> 
            <asp:DropDownList ID="DropDownList24" runat="server" CssClass="textbox-dropdown" AppendDataBoundItems="true">
                              </asp:DropDownList>
            </div>
            
</td>-->
 <td align="center">
                    <div class="form-sec-row"> 
             <asp:TextBox ID="TextBox32" runat="server" CssClass="textbox-entry"></asp:TextBox>
            </div>
            
</td>
 <td align="center" style="display:none;">
                    <div class="form-sec-row"> 
             <asp:TextBox ID="TextBox33" runat="server" CssClass="textbox-entry"></asp:TextBox>
            </div>
            
</td>
 <td align="center">
                    <div class="form-sec-row"> 
            <asp:TextBox ID="TextBox34" runat="server" CssClass="textbox-entry" onblur="return calculate();"></asp:TextBox>
            </div>
            
</td>
 <td align="center">
                    <div class="form-sec-row"> 
              <asp:TextBox ID="TextBox35" runat="server" CssClass="textbox-entry" Enabled="False"></asp:TextBox>
            </div>
            
</td>
 <td align="center">
                    <div class="form-sec-row"> 
              <asp:TextBox ID="TextBox36" runat="server" Width="140px" CssClass="DatepickerCall"></asp:TextBox>
            </div>
            
</td>   </tr>

<tr>
                <td align="center">
                    <div class="form-sec-row"> 
             <asp:DropDownList ID="DropDownList25" runat="server" CssClass="textbox-dropdown" AppendDataBoundItems="true">
                            </asp:DropDownList>
            </div>
            
</td> 
<td align="center">
                    <div class="form-sec-row"> 
             <asp:TextBox ID="TextBox37" runat="server" CssClass="textbox-dropdown"></asp:TextBox>
            </div>
            
</td>
             <!--<td align="center">
                    <div class="form-sec-row"> 
             <asp:DropDownList ID="DropDownList26" runat="server" CssClass="textbox-dropdown" AppendDataBoundItems="true">
                            </asp:DropDownList>
            </div>
</td>
                <td align="center">
                             <div class="form-sec-row"> 
             <asp:DropDownList ID="DropDownList27" runat="server" CssClass="textbox-dropdown" AppendDataBoundItems="true">
                            </asp:DropDownList>
            </div>                  
</td>
 <td align="center">
                    <div class="form-sec-row"> 
            <asp:DropDownList ID="DropDownList28" runat="server" CssClass="textbox-dropdown" AppendDataBoundItems="true">
                              </asp:DropDownList>
            </div>
            
</td>-->
 <td align="center">
                    <div class="form-sec-row"> 
             <asp:TextBox ID="TextBox38" runat="server" CssClass="textbox-entry"></asp:TextBox>
            </div>
            
</td>
 <td align="center" style="display:none;">
                    <div class="form-sec-row"> 
             <asp:TextBox ID="TextBox39" runat="server" CssClass="textbox-entry"></asp:TextBox>
            </div>
            
</td>
 <td align="center">
                    <div class="form-sec-row"> 
            <asp:TextBox ID="TextBox40" runat="server" CssClass="textbox-entry" onblur="return calculate();"></asp:TextBox>
            </div>
            
</td>
 <td align="center">
                    <div class="form-sec-row"> 
              <asp:TextBox ID="TextBox41" runat="server" CssClass="textbox-entry" Enabled="False"></asp:TextBox>
            </div>
            
</td>
 <td align="center">
                    <div class="form-sec-row"> 
              <asp:TextBox ID="TextBox42" runat="server"  Width="140px" CssClass="DatepickerCall"></asp:TextBox>
            </div>
            
</td>   </tr>

<tr>
                <td align="center">
                    <div class="form-sec-row"> 
             <asp:DropDownList ID="DropDownList29" runat="server" CssClass="textbox-dropdown" AppendDataBoundItems="true">
                            </asp:DropDownList>
            </div>
            
</td> 
<td align="center">
                    <div class="form-sec-row"> 
             <asp:TextBox ID="TextBox43" runat="server" CssClass="textbox-dropdown"></asp:TextBox>
            </div>
            
</td>
             <!--<td align="center">
                    <div class="form-sec-row"> 
             <asp:DropDownList ID="DropDownList30" runat="server" CssClass="textbox-dropdown" AppendDataBoundItems="true">
                            </asp:DropDownList>
            </div>
</td>
                <td align="center">
                             <div class="form-sec-row"> 
             <asp:DropDownList ID="DropDownList31" runat="server" CssClass="textbox-dropdown" AppendDataBoundItems="true">
                            </asp:DropDownList>
            </div>                  
</td>
 <td align="center">
                    <div class="form-sec-row"> 
            <asp:DropDownList ID="DropDownList32" runat="server" CssClass="textbox-dropdown" AppendDataBoundItems="true">
                              </asp:DropDownList>
            </div>
            
</td>-->
 <td align="center">
                    <div class="form-sec-row"> 
             <asp:TextBox ID="TextBox44" runat="server" CssClass="textbox-entry"></asp:TextBox>
            </div>
            
</td>
 <td align="center" style="display:none;">
                    <div class="form-sec-row"> 
             <asp:TextBox ID="TextBox45" runat="server" CssClass="textbox-entry"></asp:TextBox>
            </div>
            
</td>
 <td align="center">
                    <div class="form-sec-row"> 
            <asp:TextBox ID="TextBox46" runat="server" CssClass="textbox-entry" onblur="return calculate();"></asp:TextBox>
            </div>
            
</td>
 <td align="center">
                    <div class="form-sec-row"> 
              <asp:TextBox ID="TextBox47" runat="server" CssClass="textbox-entry" Enabled="False"></asp:TextBox>
            </div>
            
</td>
 <td align="center">
                    <div class="form-sec-row"> 
              <asp:TextBox ID="TextBox48" runat="server" Width="140px" CssClass="DatepickerCall"></asp:TextBox>
            </div>
            
</td>   </tr>

<tr>
                <td align="center">
                    <div class="form-sec-row"> 
             <asp:DropDownList ID="DropDownList33" runat="server" CssClass="textbox-dropdown" AppendDataBoundItems="true">
                            </asp:DropDownList>
            </div>
            
</td> 
<td align="center">
                    <div class="form-sec-row"> 
             <asp:TextBox ID="TextBox49" runat="server" CssClass="textbox-dropdown"></asp:TextBox>
            </div>
            
</td>
            <!--<td align="center">
                    <div class="form-sec-row"> 
             <asp:DropDownList ID="DropDownList34" runat="server" CssClass="textbox-dropdown" AppendDataBoundItems="true">
                            </asp:DropDownList>
            </div>
</td>
                <td align="center">
                             <div class="form-sec-row"> 
             <asp:DropDownList ID="DropDownList35" runat="server" CssClass="textbox-dropdown" AppendDataBoundItems="true">
                            </asp:DropDownList>
            </div>                  
</td>
 <td align="center">
                    <div class="form-sec-row"> 
            <asp:DropDownList ID="DropDownList36" runat="server" CssClass="textbox-dropdown" AppendDataBoundItems="true">
                              </asp:DropDownList>
            </div>
            
</td>-->
 <td align="center">
                    <div class="form-sec-row"> 
             <asp:TextBox ID="TextBox50" runat="server" CssClass="textbox-entry"></asp:TextBox>
            </div>
            
</td>
 <td align="center" style="display:none;">
                    <div class="form-sec-row"> 
             <asp:TextBox ID="TextBox51" runat="server" CssClass="textbox-entry"></asp:TextBox>
            </div>
            
</td>
 <td align="center">
                    <div class="form-sec-row"> 
            <asp:TextBox ID="TextBox52" runat="server" CssClass="textbox-entry" onblur="return calculate();"></asp:TextBox>
            </div>
            
</td>
 <td align="center">
                    <div class="form-sec-row"> 
              <asp:TextBox ID="TextBox53" runat="server" CssClass="textbox-entry" Enabled="False"></asp:TextBox>
            </div>
            
</td>
 <td align="center">
                    <div class="form-sec-row"> 
              <asp:TextBox ID="TextBox54" runat="server" Width="140px" CssClass="DatepickerCall"></asp:TextBox>
            </div>
            
</td>   </tr>

<tr>
                <td align="center">
                    <div class="form-sec-row"> 
             <asp:DropDownList ID="DropDownList37" runat="server" CssClass="textbox-dropdown" AppendDataBoundItems="true">
                            </asp:DropDownList>
            </div>
            
</td> 
<td align="center">
                    <div class="form-sec-row"> 
             <asp:TextBox ID="TextBox55" runat="server" CssClass="textbox-dropdown"></asp:TextBox>
            </div>
            
</td>
             <!--<td align="center">
                    <div class="form-sec-row"> 
             <asp:DropDownList ID="DropDownList38" runat="server" CssClass="textbox-dropdown" AppendDataBoundItems="true">
                            </asp:DropDownList>
            </div>
</td>
                <td align="center">
                             <div class="form-sec-row"> 
             <asp:DropDownList ID="DropDownList39" runat="server" CssClass="textbox-dropdown" AppendDataBoundItems="true">
                            </asp:DropDownList>
            </div>                  
</td>
 <td align="center">
                    <div class="form-sec-row"> 
            <asp:DropDownList ID="DropDownList40" runat="server" CssClass="textbox-dropdown" AppendDataBoundItems="true">
                              </asp:DropDownList>
            </div>
            
</td>-->
 <td align="center">
                    <div class="form-sec-row"> 
             <asp:TextBox ID="TextBox56" runat="server" CssClass="textbox-entry"></asp:TextBox>
            </div>
            
</td>
 <td align="center" style="display:none;">
                    <div class="form-sec-row"> 
             <asp:TextBox ID="TextBox57" runat="server" CssClass="textbox-entry"></asp:TextBox>
            </div>
            
</td>
 <td align="center">
                    <div class="form-sec-row"> 
            <asp:TextBox ID="TextBox58" runat="server" CssClass="textbox-entry" onblur="return calculate();"></asp:TextBox>
            </div>
            
</td>
 <td align="center">
                    <div class="form-sec-row"> 
              <asp:TextBox ID="TextBox59" runat="server" CssClass="textbox-entry" Enabled="False"></asp:TextBox>
            </div>
            
</td>
 <td align="center">
                    <div class="form-sec-row"> 
              <asp:TextBox ID="TextBox60" runat="server" Width="140px" CssClass="DatepickerCall"></asp:TextBox>
            </div>
            
</td>   </tr>

<tr>
                <td align="center">
                    <div class="form-sec-row"> 
             <asp:DropDownList ID="DropDownList41" runat="server" CssClass="textbox-dropdown" AppendDataBoundItems="true">
                            </asp:DropDownList>
            </div>
            
</td> 
<td align="center">
                    <div class="form-sec-row"> 
             <asp:TextBox ID="TextBox61" runat="server" CssClass="textbox-dropdown"></asp:TextBox>
            </div>
            
</td>
             <!--<td align="center">
                    <div class="form-sec-row"> 
             <asp:DropDownList ID="DropDownList42" runat="server" CssClass="textbox-dropdown" AppendDataBoundItems="true">
                            </asp:DropDownList>
            </div>
</td>
                <td align="center">
                             <div class="form-sec-row"> 
             <asp:DropDownList ID="DropDownList43" runat="server" CssClass="textbox-dropdown" AppendDataBoundItems="true">
                            </asp:DropDownList>
            </div>                  
</td>
 <td align="center">
                    <div class="form-sec-row"> 
            <asp:DropDownList ID="DropDownList44" runat="server" CssClass="textbox-dropdown" AppendDataBoundItems="true">
                              </asp:DropDownList>
            </div>
            
</td>-->
 <td align="center">
                    <div class="form-sec-row"> 
             <asp:TextBox ID="TextBox62" runat="server" CssClass="textbox-entry"></asp:TextBox>
            </div>
            
</td>
 <td align="center" style="display:none;">
                    <div class="form-sec-row"> 
             <asp:TextBox ID="TextBox63" runat="server" CssClass="textbox-entry"></asp:TextBox>
            </div>
            
</td>
 <td align="center">
                    <div class="form-sec-row"> 
            <asp:TextBox ID="TextBox64" runat="server" CssClass="textbox-entry" onblur="return calculate();"></asp:TextBox>
            </div>
            
</td>
 <td align="center">
                    <div class="form-sec-row"> 
              <asp:TextBox ID="TextBox65" runat="server" CssClass="textbox-entry" Enabled="False"></asp:TextBox>
            </div>
            
</td>
 <td align="center">
                    <div class="form-sec-row"> 
              <asp:TextBox ID="TextBox66" runat="server"  Width="140px" CssClass="DatepickerCall"></asp:TextBox>
            </div>
            
</td>   </tr>
<tr>
<td colspan="10" align="right">  
    <asp:Button ID="Button4" runat="server" Text="Cancel" 
        CssClass="submit-buttondtls" onclick="Button4_Click" />
    <asp:Button ID="Button1" runat="server" Text="Submit" 
        CssClass="submit-buttondtls" onclick="Button1_Click"/>   </td>
</tr>
 
       </table>
    </center>
        <asp:HiddenField ID="HiddenField2" runat="server" /><asp:HiddenField ID="HiddenField3" runat="server" /><asp:HiddenField ID="HiddenField4" runat="server" />
        <asp:HiddenField ID="HiddenField5" runat="server" /><asp:HiddenField ID="HiddenField6" runat="server" /><asp:HiddenField ID="HiddenField7" runat="server" />
        <asp:HiddenField ID="HiddenField8" runat="server" /><asp:HiddenField ID="HiddenField9" runat="server" /><asp:HiddenField ID="HiddenField10" runat="server" />
    </ContentTemplate>
    </asp:UpdatePanel>

                   
      
</asp:Content>

