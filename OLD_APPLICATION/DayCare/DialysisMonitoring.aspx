<%@ Page Title="" Language="C#" MasterPageFile="~/MasterAll/MasterPageAll.master" AutoEventWireup="true" CodeFile="DialysisMonitoring.aspx.cs" Inherits="DayCare_DialysisMonitoring" %>


<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
<script type="text/javascript">

    function ConfirmationMessage() {

        var a = document.getElementById("ctl00_ContentPlaceHolder1_HiddenField5").value;
        //        if (a == "2") {
        var data = confirm("Please Submit Data Before Leaving !");
        if (data) {
            document.getElementById("ctl00_ContentPlaceHolder1_HiddenField2").value = 1;
        } else {
            document.getElementById("ctl00_ContentPlaceHolder1_HiddenField2").value = 0;
        }
        //        }

    }
    function Calling() {
        var date = new Date();
        $("input[id$='TextBox1']").datepicker({ dateFormat: 'dd/mm/yy' });
        $('.TimeClass').timepicker({
            showPeriod: true,
            showLeadingZero: true
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

 
  <asp:UpdateProgress ID="UpdateProgress1" runat="server">
    <ProgressTemplate>
    
       <div id="aaa" class="progressBackgroundFilter">          </div>
        <div id="bbb" class="processMessage"><img alt="Loading" src="../images/pwait.gif"/>
      
              </div>
    </ProgressTemplate>
    </asp:UpdateProgress>

    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
    <ContentTemplate>
    <div class="formbox">
     <div class="pageheader">
            <asp:HiddenField ID="HiddenField4" runat="server" />
            <asp:Label ID="Label1" runat="server">Dialysis Monitoring</asp:Label>
        </div>
      <div class="error">
     <strong><asp:Label ID="lblError" runat="server" Font-Bold="True" ></asp:Label></strong>
                <div class="clear"></div>
            </div>
             <div class="formbox" style="padding-left:0px;">
            <div class="form-sec-row">
                    <table cellpadding="0" cellspacing="0" class="ui-accordion" style="width:100%">
                        <tr>
                       
                            <td>
                                <label class="pname">
                    <strong>Date :</strong></label>  <asp:TextBox ID="TextBox1" runat="server" CssClass="textbox-dropdown2"></asp:TextBox> </td>
                            <td>
                                <label class="pname">
                     <strong>Shift :</strong></label>
                     <asp:DropDownList ID="DropDownList2" runat="server" CssClass="textbox-dropdown" 
                                  >
                     </asp:DropDownList></td>
                            <td>
                                <asp:Button ID="btnSearch"  CssClass="submit-button" runat="server"  Height="28px"
                                    Text="Search" onclick="btnSearch_Click" 
                                 /></td>
                                    <td><asp:Button ID="Button1" runat="server"  Height="28px" Text="Submit" CssClass="submit-button" onclick="Button7_Click" /></td>
                          
                            <asp:HiddenField ID="HiddenField3" runat="server" /> <asp:HiddenField ID="HiddenField5" runat="server" /><asp:HiddenField ID="HiddenField2" runat="server" /><asp:HiddenField ID="HiddenField1" runat="server" />
                        </tr>
                    </table> </div></div>
                     

         <div class="listing">
         <asp:GridView id="GridView1"  CssClass="grid"
                 PagerStyle-CssClass="pgr" DataKeyNames ="AppNo,PName" runat="server" 
                 AutoGenerateColumns="False" AllowPaging="True" PageSize ="6" Width="955px" SelectedRowStyle-BackColor="GreenYellow"
                 onrowcommand="GridView1_RowCommand" 
                 onpageindexchanging="GridView1_PageIndexChanging" 
                 onrowdatabound="GridView1_RowDataBound"> 
                <RowStyle HorizontalAlign="Center" />
                <Columns> 
                <asp:TemplateField HeaderText="Sl. No.">
    <ItemTemplate>
        <asp:Label ID="lblSerial" runat="server" Text='<%# Bind("RowNum") %>'></asp:Label>
    </ItemTemplate>
</asp:TemplateField>

                   <asp:TemplateField HeaderText="Row id" Visible="false">
                        <ItemTemplate>
                            <asp:Label ID="ID" runat="server" Text='<%# Bind("RowID") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="Registration No">
                        <ItemTemplate>
                            <asp:Label ID="lblID" runat="server" Text='<%# Bind("PatientReg") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Patient Name">
                       
                        <ItemTemplate>
                            <asp:Label ID="lblname" runat="server" Text='<%# Bind("PName") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>

                
                    <asp:TemplateField HeaderText="Shift">
                       
                        <ItemTemplate>
                            <asp:Label ID="lblshift" runat="server" Text='<%# Bind("ShiftName") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="ShiftId" Visible="false">
                       
                        <ItemTemplate>
                            <asp:Label ID="lblshiftId" runat="server" Text='<%# Bind("ShiftID") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                      <asp:TemplateField HeaderText="Address">
                       
                        <ItemTemplate>
                            <asp:Label ID="lblAddress" runat="server" Text='<%# Bind("PAddress") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                      <asp:TemplateField HeaderText="Ph No">
                       
                        <ItemTemplate>
                            <asp:Label ID="lblPhNo" runat="server" Text='<%# Bind("PhNo1") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                         <asp:TemplateField HeaderText="Age">
                       
                        <ItemTemplate>
                            <asp:Label ID="lblage" runat="server" Text='<%# Bind("Age") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                      <asp:TemplateField HeaderText="Appointment Date">
                       
                        <ItemTemplate>
                            <asp:Label ID="lblappodate" runat="server" Text='<%# Bind("appo") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                                        
                   <asp:TemplateField >
                     <ItemTemplate>
     <asp:ImageButton ID="imgbtnDelete" runat="server" alt="Edit"  CommandArgument='<%# Bind("RowNum") %>' CommandName="Select" ImageUrl="~/Images/edit.png"   OnClientClick="javascript:return ConfirmationMessage();"/>
                     </ItemTemplate>
                </asp:TemplateField>
                 
                </Columns>
                <PagerStyle CssClass="pgr" />
                <EditRowStyle BackColor="#CCFF33" />
                <AlternatingRowStyle BackColor="#FFDB91" />
        </asp:GridView> 
         
        </div>
        <div class="formbox" style="padding-left:0px;">
            <div class="form-sec-row">
                <asp:Panel ID="Panel2" runat="server">
                   <table width="100%">
                        <tr>
                            <td style='width:7%; font-family: Verdana;font-size: small;font-weight: bold;text-align:left;'>
                    <strong>Reg. No :</strong></td>
                    <td style='width:5%; font-family: Verdana;font-size: small;font-weight: bold;text-align:left;'>
                     <asp:TextBox ID="TextBox32" runat="server" CssClass="textbox-dropdown" Enabled="False"></asp:TextBox> </td>
                            <td style='width:5%; font-family: Verdana;font-size: small;font-weight: bold;text-align:left;'>
                              <strong>Name :</strong></td>
                              
                              <td style='width:5%; font-family: Verdana;font-size: small;font-weight: bold;text-align:left;'><asp:TextBox ID="TextBox33" runat="server" 
                                    CssClass="textbox-dropdown" Enabled="False"></asp:TextBox>
                    </td>
                            <td style='width:5%; font-family: Verdana;font-size: small;font-weight: bold;text-align:left;'><strong>Age :</strong>
                         </td>
                         
                         <td style='width:5%; font-family: Verdana;font-size: small;font-weight: bold;text-align:left;'>   <asp:TextBox ID="TextBox34" runat="server" CssClass="textbox-dropdown" Width="65px"
                                Enabled="False"></asp:TextBox>
                                </td>

                                    <td style='width:5%; font-family: Verdana;font-size: small;font-weight: bold;text-align:left;'> 
                     <strong>Address :</strong></td>
                     
                     <td style='width:5%; font-family: Verdana;font-size: small;font-weight: bold;text-align:left;'><asp:TextBox ID="TextBox35" runat="server" 
                                    CssClass="textbox-dropdown" Enabled="False"></asp:TextBox></td>
                          
                        </tr></table>
                        <table width="100%">
                           <tr>
                            <td style='width:160px; font-family: Verdana;font-size: small;font-weight: bold;text-align:center;' >
                         
                    <strong>Start Time :</strong></td><td style='width:160px; font-family: Verdana;font-size: small;font-weight: bold;text-align:left;'><asp:TextBox ID="TextBox42" runat="server" CssClass="TimeClass"></asp:TextBox> </td>
                            <td style='width:160px; font-family: Verdana;font-size: small;font-weight: bold;text-align:center;'>
                      
                     <strong>End Time :</strong></td><td style='width:160px; font-family: Verdana;font-size: small;font-weight: bold;text-align:left;'><asp:TextBox ID="TextBox43" runat="server" CssClass="TimeClass"></asp:TextBox>
                    </td>
                            <td style='text-align:center;width:160px;padding-right:15px;font:normal 13px/22px Arial, Helvetica, sans-serif;color:#373a3e;margin-bottom:10px;' >
                     <strong>Current Dialysis No:</strong></td><td style='width:160px; font-family: Verdana;font-size: small;font-weight: bold;text-align:left;'><asp:TextBox ID="TextBox44" runat="server" 
                                       CssClass="textbox-monitor" Enabled="False"></asp:TextBox></td>
                                   
                        </tr>
                    </table>
                </asp:Panel>
                   
                </div>
               
            </div>
                  
     </div>

        <asp:Panel ID="Panel1" runat="server">
         <div class="formbox">
<table width="100%" cellpadding="0" cellspacing="0">
<tr>
<td align="right" style='width:6%;background-color:#E4F7EC;padding-right:15px;font:normal 13px/22px Arial, Helvetica, sans-serif;color:#373a3e;margin-bottom:10px;'><div><strong>Dialysis</strong></div></td>
<td align="center" style='width:6%;background-color:#E4F7EC;padding-right:15px;font:normal 13px/22px Arial, Helvetica, sans-serif;color:#373a3e;margin-bottom:10px;'><div><strong>BP</strong></div></td>
<td align="center" style='width:6%;background-color:#E4F7EC;padding-right:15px;font:normal 13px/22px Arial, Helvetica, sans-serif;color:#373a3e;margin-bottom:10px;'><div><strong>Weight</strong></div></td>
<td align="center" style='width:6%;background-color:#E4F7EC;padding-right:15px;font:normal 13px/22px Arial, Helvetica, sans-serif;color:#373a3e;margin-bottom:10px;'><div><strong>HB</strong></div></td>
<td align="center" style='width:6%;background-color:#E4F7EC;padding-right:15px;font:normal 13px/22px Arial, Helvetica, sans-serif;color:#373a3e;margin-bottom:10px;'><div><strong>Urea</strong></div></td>
<td align="center" style='width:6%;background-color:#E4F7EC;padding-right:15px;font:normal 13px/22px Arial, Helvetica, sans-serif;color:#373a3e;margin-bottom:10px;'><div><strong>Creatinine</strong></div></td>
<td align="center" style='width:6%;background-color:#E4F7EC;padding-right:15px;font:normal 13px/22px Arial, Helvetica, sans-serif;color:#373a3e;margin-bottom:10px;'><div><strong>Na+</strong></div></td>
<td align="center" style='width:6%;background-color:#E4F7EC;padding-right:15px;font:normal 13px/22px Arial, Helvetica, sans-serif;color:#373a3e;margin-bottom:10px;'><div><strong>K+</strong></div></td>
</tr>
	<tr>
        <td align="center"><div><label><strong>Pre</strong></label></div></td>
        <td align="center" style='width:6%;padding-right:15px;font:normal 13px/22px Arial, Helvetica, sans-serif;color:#373a3e;margin-bottom:10px;'><div><asp:TextBox ID="TextBox45" runat="server" CssClass="textbox-monitor"></asp:TextBox> </div></td>
        <td align="center" style='width:6%;padding-right:15px;font:normal 13px/22px Arial, Helvetica, sans-serif;color:#373a3e;margin-bottom:10px;'><div><asp:TextBox ID="TextBox46" runat="server" CssClass="textbox-monitor"></asp:TextBox> </div></td>
        <td align="center" style='width:6%;padding-right:15px;font:normal 13px/22px Arial, Helvetica, sans-serif;color:#373a3e;margin-bottom:10px;'><div><asp:TextBox ID="TextBox47" runat="server" CssClass="textbox-monitor"></asp:TextBox> </div></td>
        <td align="center" style='width:6%;padding-right:15px;font:normal 13px/22px Arial, Helvetica, sans-serif;color:#373a3e;margin-bottom:10px;'><div><asp:TextBox ID="TextBox48" runat="server" CssClass="textbox-monitor"></asp:TextBox> </div></td>
        <td align="center" style='width:6%;padding-right:15px;font:normal 13px/22px Arial, Helvetica, sans-serif;color:#373a3e;margin-bottom:10px;'><div><asp:TextBox ID="TextBox49" runat="server" CssClass="textbox-monitor"></asp:TextBox> </div></td>
        <td align="center" style='width:6%;padding-right:15px;font:normal 13px/22px Arial, Helvetica, sans-serif;color:#373a3e;margin-bottom:10px;'><div><asp:TextBox ID="TextBox50" runat="server" CssClass="textbox-monitor"></asp:TextBox> </div></td>
        <td align="center" style='width:6%;padding-right:15px;font:normal 13px/22px Arial, Helvetica, sans-serif;color:#373a3e;margin-bottom:10px;'><div><asp:TextBox ID="TextBox51" runat="server" CssClass="textbox-monitor"></asp:TextBox> </div></td>
       
        </tr>
    <tr>
        <td align="center"><div><label><strong>Post</strong></label></div></td>
        <td align="center" style='width:6%;padding-right:15px;font:normal 13px/22px Arial, Helvetica, sans-serif;color:#373a3e;margin-bottom:10px;'><div><asp:TextBox ID="TextBox52" runat="server" CssClass="textbox-monitor"></asp:TextBox> </div></td>
        <td align="center" style='width:6%;padding-right:15px;font:normal 13px/22px Arial, Helvetica, sans-serif;color:#373a3e;margin-bottom:10px;'><div><asp:TextBox ID="TextBox53" runat="server" CssClass="textbox-monitor"></asp:TextBox> </div></td>
        <td align="center" style='width:6%;padding-right:15px;font:normal 13px/22px Arial, Helvetica, sans-serif;color:#373a3e;margin-bottom:10px;'><div><asp:TextBox ID="TextBox54" runat="server" CssClass="textbox-monitor"></asp:TextBox> </div></td>
        <td align="center" style='width:6%;padding-right:15px;font:normal 13px/22px Arial, Helvetica, sans-serif;color:#373a3e;margin-bottom:10px;'><div><asp:TextBox ID="TextBox55" runat="server" CssClass="textbox-monitor"></asp:TextBox> </div></td>
        <td align="center" style='width:6%;padding-right:15px;font:normal 13px/22px Arial, Helvetica, sans-serif;color:#373a3e;margin-bottom:10px;'><div><asp:TextBox ID="TextBox56" runat="server" CssClass="textbox-monitor"></asp:TextBox> </div></td>
        <td align="center" style='width:6%;padding-right:15px;font:normal 13px/22px Arial, Helvetica, sans-serif;color:#373a3e;margin-bottom:10px;'><div><asp:TextBox ID="TextBox57" runat="server" CssClass="textbox-monitor"></asp:TextBox> </div></td>
        <td align="center" style='width:6%;padding-right:15px;font:normal 13px/22px Arial, Helvetica, sans-serif;color:#373a3e;margin-bottom:10px;'><div><asp:TextBox ID="TextBox58" runat="server" CssClass="textbox-monitor"></asp:TextBox> </div></td>
       
        </tr>
</table></div>
<div class="formbox">
<table width="100%" cellpadding="0" cellspacing="0">
<tr>
<td align="center" style='width:6%;padding-right:15px;background-color:#E4F7EC;font:normal 13px/22px Arial, Helvetica, sans-serif;color:#373a3e;margin-bottom:10px;'><div><strong>SL. NO.</strong></div></td>
<td align="center" style='width:6%;background-color:#E4F7EC;padding-right:15px;font:normal 13px/22px Arial, Helvetica, sans-serif;color:#373a3e;margin-bottom:10px;'><div><strong>Time</strong></div></td>
<td align="center" style='width:6%;background-color:#E4F7EC;padding-right:15px;font:normal 13px/22px Arial, Helvetica, sans-serif;color:#373a3e;margin-bottom:10px;'><div><strong>BP</strong></div></td>
<td align="center" style='width:6%;background-color:#E4F7EC;padding-right:15px;font:normal 13px/22px Arial, Helvetica, sans-serif;color:#373a3e;margin-bottom:10px;'><div><strong>Pulse</strong></div></td>
<td align="center" style='width:6%;background-color:#E4F7EC;padding-right:15px;font:normal 13px/22px Arial, Helvetica, sans-serif;color:#373a3e;margin-bottom:10px;'><div><strong>Blood Flow</strong></div></td>
<td align="center" style='width:6%;background-color:#E4F7EC;padding-right:15px;font:normal 13px/22px Arial, Helvetica, sans-serif;color:#373a3e;margin-bottom:10px;'><div><strong>UF Goal</strong></div></td>
<td align="center" style='width:6%;background-color:#E4F7EC;padding-right:15px;font:normal 13px/22px Arial, Helvetica, sans-serif;color:#373a3e;margin-bottom:10px;'><div><strong>Comment</strong></div></td>
</tr>
	<tr>
        <td align="center" style='width:6%;padding-right:15px;font:normal 13px/22px Arial, Helvetica, sans-serif;color:#373a3e;margin-bottom:10px;'><div><strong>1ST</strong></div></td>
        <td align="center" style='width:6%;padding-right:15px;font:normal 13px/22px Arial, Helvetica, sans-serif;color:#373a3e;margin-bottom:10px;'><div><asp:TextBox ID="TextBox2" runat="server" CssClass="TimeClass"></asp:TextBox> </div></td>
        <td align="center" style='width:6%;padding-right:15px;font:normal 13px/22px Arial, Helvetica, sans-serif;color:#373a3e;margin-bottom:10px;'><div><asp:TextBox ID="TextBox3" runat="server" CssClass="textbox-monitor"></asp:TextBox> </div></td>
        <td align="center" style='width:6%;padding-right:15px;font:normal 13px/22px Arial, Helvetica, sans-serif;color:#373a3e;margin-bottom:10px;'><div><asp:TextBox ID="TextBox4" runat="server" CssClass="textbox-monitor"></asp:TextBox> </div></td>
        <td align="center" style='width:6%;padding-right:15px;font:normal 13px/22px Arial, Helvetica, sans-serif;color:#373a3e;margin-bottom:10px;'><div><asp:TextBox ID="TextBox5" runat="server" CssClass="textbox-monitor"></asp:TextBox> </div></td>
        <td align="center" style='width:6%;padding-right:15px;font:normal 13px/22px Arial, Helvetica, sans-serif;color:#373a3e;margin-bottom:10px;'><div><asp:TextBox ID="TextBox6" runat="server" CssClass="textbox-monitor"></asp:TextBox> </div></td>
        <td align="center" style='width:6%;padding-right:15px;font:normal 13px/22px Arial, Helvetica, sans-serif;color:#373a3e;margin-bottom:10px;'><div><asp:TextBox ID="TextBox7" runat="server" CssClass="textbox-monitor"></asp:TextBox> </div></td>
       
        </tr>
    <tr>
    <td align="center" style='width:6%;padding-right:15px;font:normal 13px/22px Arial, Helvetica, sans-serif;color:#373a3e;margin-bottom:10px;'><div><strong>2ND</strong></div></td>
        <td align="center" style='width:6%;padding-right:15px;font:normal 13px/22px Arial, Helvetica, sans-serif;color:#373a3e;margin-bottom:10px;'><div><asp:TextBox ID="TextBox9" runat="server" CssClass="TimeClass"></asp:TextBox> </div></td>
        <td align="center" style='width:6%;padding-right:15px;font:normal 13px/22px Arial, Helvetica, sans-serif;color:#373a3e;margin-bottom:10px;'><div><asp:TextBox ID="TextBox10" runat="server" CssClass="textbox-monitor"></asp:TextBox> </div></td>
        <td align="center" style='width:6%;padding-right:15px;font:normal 13px/22px Arial, Helvetica, sans-serif;color:#373a3e;margin-bottom:10px;'><div><asp:TextBox ID="TextBox11" runat="server" CssClass="textbox-monitor"></asp:TextBox> </div></td>
        <td align="center" style='width:6%;padding-right:15px;font:normal 13px/22px Arial, Helvetica, sans-serif;color:#373a3e;margin-bottom:10px;'><div><asp:TextBox ID="TextBox12" runat="server" CssClass="textbox-monitor"></asp:TextBox> </div></td>
        <td align="center" style='width:6%;padding-right:15px;font:normal 13px/22px Arial, Helvetica, sans-serif;color:#373a3e;margin-bottom:10px;'><div><asp:TextBox ID="TextBox13" runat="server" CssClass="textbox-monitor"></asp:TextBox> </div></td>
        <td align="center" style='width:6%;padding-right:15px;font:normal 13px/22px Arial, Helvetica, sans-serif;color:#373a3e;margin-bottom:10px;'><div><asp:TextBox ID="TextBox14" runat="server" CssClass="textbox-monitor"></asp:TextBox> </div></td>
     
       
        </tr>
        <tr>
        <td align="center" style='width:6%;padding-right:15px;font:normal 13px/22px Arial, Helvetica, sans-serif;color:#373a3e;margin-bottom:10px;'><div><strong>3RD</strong></div></td>
        <td align="center" style='width:6%;padding-right:15px;font:normal 13px/22px Arial, Helvetica, sans-serif;color:#373a3e;margin-bottom:10px;'><div><asp:TextBox ID="TextBox8" runat="server"  CssClass="TimeClass"></asp:TextBox> </div></td>
        <td align="center" style='width:6%;padding-right:15px;font:normal 13px/22px Arial, Helvetica, sans-serif;color:#373a3e;margin-bottom:10px;'><div><asp:TextBox ID="TextBox15" runat="server" CssClass="textbox-monitor"></asp:TextBox> </div></td>
        <td align="center" style='width:6%;padding-right:15px;font:normal 13px/22px Arial, Helvetica, sans-serif;color:#373a3e;margin-bottom:10px;'><div><asp:TextBox ID="TextBox16" runat="server" CssClass="textbox-monitor"></asp:TextBox> </div></td>
        <td align="center" style='width:6%;padding-right:15px;font:normal 13px/22px Arial, Helvetica, sans-serif;color:#373a3e;margin-bottom:10px;'><div><asp:TextBox ID="TextBox17" runat="server" CssClass="textbox-monitor"></asp:TextBox> </div></td>
        <td align="center" style='width:6%;padding-right:15px;font:normal 13px/22px Arial, Helvetica, sans-serif;color:#373a3e;margin-bottom:10px;'><div><asp:TextBox ID="TextBox18" runat="server" CssClass="textbox-monitor"></asp:TextBox> </div></td>
        <td align="center" style='width:6%;padding-right:15px;font:normal 13px/22px Arial, Helvetica, sans-serif;color:#373a3e;margin-bottom:10px;'><div><asp:TextBox ID="TextBox19" runat="server" CssClass="textbox-monitor"></asp:TextBox> </div></td>
       
        </tr>
    <tr>
    <td align="center" style='width:6%;padding-right:15px;font:normal 13px/22px Arial, Helvetica, sans-serif;color:#373a3e;margin-bottom:10px;'><div><strong>4TH</strong></div></td>
        <td align="center" style='width:6%;padding-right:15px;font:normal 13px/22px Arial, Helvetica, sans-serif;color:#373a3e;margin-bottom:10px;'><div><asp:TextBox ID="TextBox20" runat="server" CssClass="TimeClass"></asp:TextBox> </div></td>
        <td align="center" style='width:6%;padding-right:15px;font:normal 13px/22px Arial, Helvetica, sans-serif;color:#373a3e;margin-bottom:10px;'><div><asp:TextBox ID="TextBox21" runat="server" CssClass="textbox-monitor"></asp:TextBox> </div></td>
        <td align="center" style='width:6%;padding-right:15px;font:normal 13px/22px Arial, Helvetica, sans-serif;color:#373a3e;margin-bottom:10px;'><div><asp:TextBox ID="TextBox22" runat="server" CssClass="textbox-monitor"></asp:TextBox> </div></td>
        <td align="center" style='width:6%;padding-right:15px;font:normal 13px/22px Arial, Helvetica, sans-serif;color:#373a3e;margin-bottom:10px;'><div><asp:TextBox ID="TextBox23" runat="server" CssClass="textbox-monitor"></asp:TextBox> </div></td>
        <td align="center" style='width:6%;padding-right:15px;font:normal 13px/22px Arial, Helvetica, sans-serif;color:#373a3e;margin-bottom:10px;'><div><asp:TextBox ID="TextBox24" runat="server" CssClass="textbox-monitor"></asp:TextBox> </div></td>
        <td align="center" style='width:6%;padding-right:15px;font:normal 13px/22px Arial, Helvetica, sans-serif;color:#373a3e;margin-bottom:10px;'><div><asp:TextBox ID="TextBox25" runat="server" CssClass="textbox-monitor"></asp:TextBox> </div></td>
     
       
        </tr>
        <tr>
        <td align="center" style='width:6%;padding-right:15px;font:normal 13px/22px Arial, Helvetica, sans-serif;color:#373a3e;margin-bottom:10px;'><div><strong>5TH</strong></div></td>
        <td align="center" style='width:6%;padding-right:15px;font:normal 13px/22px Arial, Helvetica, sans-serif;color:#373a3e;margin-bottom:10px;'><div><asp:TextBox ID="TextBox26" runat="server" CssClass="TimeClass"></asp:TextBox> </div></td>
        <td align="center" style='width:6%;padding-right:15px;font:normal 13px/22px Arial, Helvetica, sans-serif;color:#373a3e;margin-bottom:10px;'><div><asp:TextBox ID="TextBox27" runat="server" CssClass="textbox-monitor"></asp:TextBox> </div></td>
        <td align="center" style='width:6%;padding-right:15px;font:normal 13px/22px Arial, Helvetica, sans-serif;color:#373a3e;margin-bottom:10px;'><div><asp:TextBox ID="TextBox28" runat="server" CssClass="textbox-monitor"></asp:TextBox> </div></td>
        <td align="center" style='width:6%;padding-right:15px;font:normal 13px/22px Arial, Helvetica, sans-serif;color:#373a3e;margin-bottom:10px;'><div><asp:TextBox ID="TextBox29" runat="server" CssClass="textbox-monitor"></asp:TextBox> </div></td>
        <td align="center" style='width:6%;padding-right:15px;font:normal 13px/22px Arial, Helvetica, sans-serif;color:#373a3e;margin-bottom:10px;'><div><asp:TextBox ID="TextBox30" runat="server" CssClass="textbox-monitor"></asp:TextBox> </div></td>
        <td align="center" style='width:6%;padding-right:15px;font:normal 13px/22px Arial, Helvetica, sans-serif;color:#373a3e;margin-bottom:10px;'><div><asp:TextBox ID="TextBox31" runat="server" CssClass="textbox-monitor"></asp:TextBox> </div></td>
       
        </tr>
    <tr>
    <td align="center" style='width:6%;padding-right:15px;font:normal 13px/22px Arial, Helvetica, sans-serif;color:#373a3e;margin-bottom:10px;'><div><strong>6TH</strong></div></td>
        <td align="center" style='width:6%;padding-right:15px;font:normal 13px/22px Arial, Helvetica, sans-serif;color:#373a3e;margin-bottom:10px;'><div><asp:TextBox ID="TextBox36" runat="server" CssClass="TimeClass"></asp:TextBox> </div></td>
        <td align="center" style='width:6%;padding-right:15px;font:normal 13px/22px Arial, Helvetica, sans-serif;color:#373a3e;margin-bottom:10px;'><div><asp:TextBox ID="TextBox37" runat="server" CssClass="textbox-monitor"></asp:TextBox> </div></td>
        <td align="center" style='width:6%;padding-right:15px;font:normal 13px/22px Arial, Helvetica, sans-serif;color:#373a3e;margin-bottom:10px;'><div><asp:TextBox ID="TextBox38" runat="server" CssClass="textbox-monitor"></asp:TextBox> </div></td>
        <td align="center" style='width:6%;padding-right:15px;font:normal 13px/22px Arial, Helvetica, sans-serif;color:#373a3e;margin-bottom:10px;'><div><asp:TextBox ID="TextBox39" runat="server" CssClass="textbox-monitor"></asp:TextBox> </div></td>
        <td align="center" style='width:6%;padding-right:15px;font:normal 13px/22px Arial, Helvetica, sans-serif;color:#373a3e;margin-bottom:10px;'><div><asp:TextBox ID="TextBox40" runat="server" CssClass="textbox-monitor"></asp:TextBox> </div></td>
        <td align="center" style='width:6%;padding-right:15px;font:normal 13px/22px Arial, Helvetica, sans-serif;color:#373a3e;margin-bottom:10px;'><div><asp:TextBox ID="TextBox41" runat="server" CssClass="textbox-monitor"></asp:TextBox> </div></td>
     
       
        </tr>
</table>
            
           </div>

  
        </asp:Panel>
    </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>

