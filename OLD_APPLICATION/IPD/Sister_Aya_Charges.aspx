<%@ Page Language="C#" MasterPageFile="~/MasterAll/MasterPageAll.master" AutoEventWireup="true" CodeFile="Sister_Aya_Charges.aspx.cs" Inherits="Documents_Sister_Aya_Charges" Title="Untitled Page" %>
<asp:Content ID="Content2" ContentPlaceHolderID="head" Runat="Server">
 
    <script language="javascript" type="text/javascript">
     function ShowDialog() {
         var rtvalue = window.open("IpdpatientPopup.aspx", "sss", "Width:800px; Height:550px; dialogLeft:250px;");
        document.getElementById("ctl00_ContentPlaceHolder1_TextBox1").value = rtvalue.NameValue;

    }

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
        var datetime = day + '/' + mnt + '/' + yr;
        var hour = now.getHours();
        var minute = now.getMinutes();
        var a;
        if (hour > 12) {
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
        document.getElementById("ctl00_ContentPlaceHolder1_HiddenField2").value = time;
    }
        function DisableBackButton() {
            window.history.forward()
            }
            DisableBackButton();
            window.onload = DisableBackButton;
            window.onpageshow = function(evt) { if (evt.persisted) DisableBackButton() }
            window.onunload = function() { void (0) }

            function Calling() {
                var date = new Date();
                $("input[id$='TextBox6']").datepicker({ dateFormat: 'dd/mm/yy' });


                var date = new Date();
                $("input[id$='TextBox9']").datepicker({ dateFormat: 'dd/mm/yy' });

                var date = new Date();
                $("input[id$='TextBox12']").datepicker({ dateFormat: 'dd/mm/yy' });


                var date = new Date();
                $("input[id$='TextBox15']").datepicker({ dateFormat: 'dd/mm/yy' });

                var date = new Date();
                $("input[id$='TextBox18']").datepicker({ dateFormat: 'dd/mm/yy' });

                var date = new Date();
                $("input[id$='TextBox21']").datepicker({ dateFormat: 'dd/mm/yy' });

                var date = new Date();
                $("input[id$='TextBox24']").datepicker({ dateFormat: 'dd/mm/yy' });

                var date = new Date();
                $("input[id$='TextBox27']").datepicker({ dateFormat: 'dd/mm/yy' });

                var date = new Date();
                $("input[id$='TextBox30']").datepicker({ dateFormat: 'dd/mm/yy' });

                var date = new Date();
                $("input[id$='TextBox33']").datepicker({ dateFormat: 'dd/mm/yy' });

                $("input[id$='Tab2']").click(function () {


                    if ($("input[id$='TextBox1']").val() == '') {
                        alert('Please Enter Registration No !');
                        $("input[id$='TextBox1']").focus();
                        $("input[id$='TextBox1']").addClass('textboxerr');
                        return false;
                    }
                    else {
                        $("input[id$='TextBox1']").removeClass('textboxerr');
                    }
                });

                $("input[id$='Button1']").click(function () {
                    if ($("select[id$='CenterNameList']").val() == '0' && $("input[id$='TextBox1']").val() == '' && $("input[id$='TextBox2']").val() == '' && $("input[id$='TextBox3']").val() == '' && $("input[id$='TextBox4']").val() == '') {
                        $("select[id$='CenterNameList']").addClass('textboxerr');
                        $("input[id$='TextBox1']").addClass('textboxerr');
                        $("input[id$='TextBox2']").addClass('textboxerr');
                        $("input[id$='TextBox3']").addClass('textboxerr');
                        $("input[id$='TextBox4']").addClass('textboxerr');
                    }

                    if ($("select[id$='CenterNameList']").val() == '0') {
                        alert('Please Enter Center Name Properly!');
                        $("select[id$='CenterNameList']").focus();
                        $("select[id$='CenterNameList']").addClass('textboxerr');
                        return false;
                    }
                    else {
                        $("select[id$='CenterNameList']").removeClass('textboxerr');
                    }

                    if ($("input[id$='TextBox2']").val() == '') {
                        alert('Please Enter Day Properly!');
                        $("input[id$='TextBox2']").focus();
                        $("input[id$='TextBox2']").addClass('textboxerr');
                        return false;
                    }
                    else {
                        $("input[id$='TextBox2']").removeClass('textboxerr');
                    }

                    if ($("input[id$='TextBox3']").val() == '') {
                        alert('Please Enter Night Properly!');
                        $("input[id$='TextBox3']").focus();
                        $("input[id$='TextBox3']").addClass('textboxerr');
                        return false;
                    }
                    else {
                        $("input[id$='TextBox3']").removeClass('textboxerr');
                    }

                    if ($("input[id$='TextBox4']").val() == '') {
                        alert('Please Enter Day+Night Properly!');
                        $("input[id$='TextBox4']").focus();
                        $("input[id$='TextBox4']").addClass('textboxerr');
                        return false;
                    }
                    else {
                        $("input[id$='TextBox4']").removeClass('textboxerr');
                    }
                });
                $("input[id$='TextBox2']").keydown(function (event) {
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
                $("input[id$='TextBox3']").keydown(function (event) {
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
</asp:Content>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">


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
         <asp:Label ID="Label1" runat="server">Sister Aya Charges</asp:Label>
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
    <asp:MultiView ID="MainView" runat="server" 
            onactiveviewchanged="MainView_ActiveViewChanged">
                    <asp:View ID="View1" runat="server">
        <div class="form-sec">
            <div class="error">
                <strong><asp:Label ID="lblError" runat="server" Font-Bold="True"></asp:Label></strong>
                <div class="clear"></div>
            </div>            
            <asp:HiddenField ID="HiddenField1" runat="server" />  <asp:HiddenField ID="HiddenField2" runat="server" />
    <div class="form-sec-row">
                        <label>
                        <strong>
                       Registration No :</strong></label>
                          <asp:TextBox ID="TextBox1" runat="server" CssClass="textbox-medium1" 
                              Enabled="true"></asp:TextBox>
                                <asp:Button ID="Button3" runat="server" Text="Quick Search"  Height="28px" CssClass="submit-buttonCheck" OnClientClick="ShowDialog()"/>
                              <asp:Button ID="Button4" runat="server" Text="Fetch"   Height="28px"
                               CssClass="submit-button" onclick="Button4_Click"/>
                        <div class="clear">
                        </div>
                    </div>
                        <div class="form-sec-row">
                        <label>
                        <strong>
                       Patient's Name :</strong></label>
                          <asp:TextBox ID="TextBox2" runat="server" CssClass="textbox-medium1" 
                                Enabled="False"></asp:TextBox>
                        <div class="clear">
                        </div>
                    </div>

                        <div class="form-sec-row">
                        <label>
                        <strong>
                       Bed No :</strong></label>
                          <asp:TextBox ID="TextBox3" runat="server" CssClass="textbox-medium1" 
                                Enabled="False"></asp:TextBox>
                        <div class="clear">
                        </div>
                    </div>

                        <div class="form-sec-row">
                        <label>
                        <strong>
                       Admission Date :</strong></label>
                          <asp:TextBox ID="TextBox4" runat="server" CssClass="textbox-medium1" 
                                Enabled="False"></asp:TextBox>
                        <div class="clear">
                        </div>
                    </div>
            
            <center>
    <table border="1" cellpadding="0" cellspacing="0" width="80%">
          
       <tr style='background-color:#FF9300;'>
                <td align="center">
                 <strong>Type</strong> 
        </td> 

                <td align="center">
                    <strong>Shift</strong> 
</td>
                <td align="center">
                         <strong> Charges </strong> 
</td>
 <td align="center">
                    
              <strong>Date</strong> 
</td>
           <td align="center">
                    
              <strong>Continue</strong> 
</td>
 <td align="center">
                   
  <strong>Remarks</strong> 
           
</td>
        
                      
            </tr>
                <tr>
                <td align="center">
                  
             <asp:DropDownList ID="DropDownList1" runat="server" CssClass="textbox-medium1" 
                            Width="150px"  >
                            </asp:DropDownList>
          
</td> 

                <td align="center">
                   
             <asp:DropDownList ID="DropDownList2" runat="server"   Width="150px" AutoPostBack="True" 
                            onselectedindexchanged="DropDownList2_SelectedIndexChanged">
                            </asp:DropDownList>
           
</td>
                <td align="center">
                           
                                <asp:TextBox ID="TextBox5" runat="server"  Width="150px"></asp:TextBox>
            
                      
</td>
 <td align="center">
                  
                       <asp:TextBox ID="TextBox6" runat="server"   Width="150px"></asp:TextBox>
           
</td>
                    <td align="center">
                        <asp:CheckBox ID="chksys1" runat="server" />
                    </td>
 <td align="center">
                    <asp:TextBox ID="TextBox7" runat="server"  Width="150px" 
                            ></asp:TextBox>
           
</td>
 </tr>

 <tr>
                <td align="center">
                  <asp:DropDownList ID="DropDownList3" runat="server" CssClass="textbox-medium1" 
                            Width="150px" >
                            </asp:DropDownList>
         
</td> 

                <td align="center">
                   <asp:DropDownList ID="DropDownList4" runat="server"  
                            Width="150px" AutoPostBack="True" 
                            onselectedindexchanged="DropDownList4_SelectedIndexChanged">
                            </asp:DropDownList>
         
</td>
                <td align="center">
                               <asp:TextBox ID="TextBox8" runat="server"  Width="150px"></asp:TextBox>
                          
</td>
 <td align="center">
                 
                       <asp:TextBox ID="TextBox9" runat="server"  Width="150px"></asp:TextBox>
          
</td>
     <td align="center">
                        <asp:CheckBox ID="chksys2" runat="server" />
                    </td>
 <td align="center">
                 
             <asp:TextBox ID="TextBox10" runat="server"  Width="150px" 
                            ></asp:TextBox>
          
</td>
 </tr>

 <tr>
                <td align="center">
                    
             <asp:DropDownList ID="DropDownList5" runat="server"  
                            Width="150px"  >
                            </asp:DropDownList>
          
            
</td> 

                <td align="center">
                    
             <asp:DropDownList ID="DropDownList6" runat="server"  
                            Width="150px" AutoPostBack="True" 
                            onselectedindexchanged="DropDownList6_SelectedIndexChanged">
                            </asp:DropDownList>
          
</td>
                <td align="center">
                             
                                <asp:TextBox ID="TextBox11" runat="server"  Width="150px"></asp:TextBox>
            
                            
</td>
 <td align="center">
                    
                       <asp:TextBox ID="TextBox12" runat="server"  Width="150px"></asp:TextBox>
          
            
</td>
     <td align="center">
                        <asp:CheckBox ID="chksys3" runat="server" />
                    </td>
 <td align="center">
                    
             <asp:TextBox ID="TextBox13" runat="server"  Width="150px" 
                            ></asp:TextBox>
          
            
</td>
 </tr>

 <tr>
                <td align="center">
                    
             <asp:DropDownList ID="DropDownList7" runat="server"  
                            Width="150px"  >
                            </asp:DropDownList>
          
            
</td> 

                <td align="center">
                    
             <asp:DropDownList ID="DropDownList8" runat="server"  
                            Width="150px" AutoPostBack="True" 
                            onselectedindexchanged="DropDownList8_SelectedIndexChanged">
                            </asp:DropDownList>
          
</td>
                <td align="center">
                             
                                <asp:TextBox ID="TextBox14" runat="server"  Width="150px"></asp:TextBox>
            
                            
</td>
 <td align="center">
                    
                       <asp:TextBox ID="TextBox15" runat="server"  Width="150px"></asp:TextBox>
          
            
</td>
     <td align="center">
                        <asp:CheckBox ID="chksys4" runat="server" />
                    </td>
 <td align="center">
                    
             <asp:TextBox ID="TextBox16" runat="server"  Width="150px" 
                            ></asp:TextBox>
          
            
</td>
 </tr>

 <tr>
                <td align="center">
                    
             <asp:DropDownList ID="DropDownList9" runat="server"  
                            Width="150px" >
                            </asp:DropDownList>
          
            
</td> 

                <td align="center">
                    
             <asp:DropDownList ID="DropDownList10" runat="server"  
                            Width="150px" AutoPostBack="True" 
                            onselectedindexchanged="DropDownList10_SelectedIndexChanged">
                            </asp:DropDownList>
          
</td>
                <td align="center">
                             
                                <asp:TextBox ID="TextBox17" runat="server"  Width="150px"></asp:TextBox>
            
                            
</td>
 <td align="center">
                    
                       <asp:TextBox ID="TextBox18" runat="server"  Width="150px"></asp:TextBox>
          
            
</td>
     <td align="center">
                        <asp:CheckBox ID="chksys5" runat="server" />
                    </td>
 <td align="center">
                    
             <asp:TextBox ID="TextBox19" runat="server"  Width="150px" 
                            ></asp:TextBox>
          
            
</td>
 </tr>

 <tr>
                <td align="center">
                    
             <asp:DropDownList ID="DropDownList11" runat="server"  
                            Width="150px"  >
                            </asp:DropDownList>
          
            
</td> 

                <td align="center">
                    
             <asp:DropDownList ID="DropDownList12" runat="server"  
                            Width="150px" AutoPostBack="True" 
                            onselectedindexchanged="DropDownList12_SelectedIndexChanged">
                            </asp:DropDownList>
          
</td>
                <td align="center">
                             
                                <asp:TextBox ID="TextBox20" runat="server"  Width="150px"></asp:TextBox>
            
                            
</td>
 <td align="center">
                    
                       <asp:TextBox ID="TextBox21" runat="server"  Width="150px"></asp:TextBox>
          
            
</td>
     <td align="center">
                        <asp:CheckBox ID="chksys6" runat="server" />
                    </td>
 <td align="center">
                    
             <asp:TextBox ID="TextBox22" runat="server"  Width="150px" 
                            ></asp:TextBox>
          
            
</td>
 </tr>

 <tr>
                <td align="center">
                    
             <asp:DropDownList ID="DropDownList13" runat="server"  
                            Width="150px"  >
                            </asp:DropDownList>
          
            
</td> 

                <td align="center">
                    
             <asp:DropDownList ID="DropDownList14" runat="server"  
                            Width="150px" AutoPostBack="True" 
                            onselectedindexchanged="DropDownList14_SelectedIndexChanged">
                            </asp:DropDownList>
          
</td>
                <td align="center">
                             
                                <asp:TextBox ID="TextBox23" runat="server"  Width="150px"></asp:TextBox>
            
                            
</td>
 <td align="center">
                    
                       <asp:TextBox ID="TextBox24" runat="server"  Width="150px"></asp:TextBox>
          
            
</td>
     <td align="center">
                        <asp:CheckBox ID="chksys7" runat="server" />
                    </td>
 <td align="center">
                    
             <asp:TextBox ID="TextBox25" runat="server"  Width="150px" 
                            ></asp:TextBox>
          
            
</td>
 </tr>

 <tr>
                <td align="center">
                    
             <asp:DropDownList ID="DropDownList15" runat="server"  
                            Width="150px" >
                            </asp:DropDownList>
          
            
</td> 

                <td align="center">
                    
             <asp:DropDownList ID="DropDownList16" runat="server"  
                            Width="150px" AutoPostBack="True" 
                            onselectedindexchanged="DropDownList16_SelectedIndexChanged">
                            </asp:DropDownList>
          
</td>
                <td align="center">
                             
                                <asp:TextBox ID="TextBox26" runat="server"  Width="150px"></asp:TextBox>
            
                            
</td>
 <td align="center">
                    
                       <asp:TextBox ID="TextBox27" runat="server"  Width="150px"></asp:TextBox>
          
            
</td>
     <td align="center">
                        <asp:CheckBox ID="chksys8" runat="server" />
                    </td>
 <td align="center">
                    
             <asp:TextBox ID="TextBox28" runat="server"  Width="150px" 
                            ></asp:TextBox>
          
            
</td>
 </tr>

 <tr>
                <td align="center">
                    
             <asp:DropDownList ID="DropDownList17" runat="server"  
                            Width="150px"  >
                            </asp:DropDownList>
          
            
</td> 

                <td align="center">
                    
             <asp:DropDownList ID="DropDownList18" runat="server"  
                            Width="150px" AutoPostBack="True" 
                            onselectedindexchanged="DropDownList18_SelectedIndexChanged">
                            </asp:DropDownList>
          
</td>
                <td align="center">
                             
                                <asp:TextBox ID="TextBox29" runat="server"   Width="150px"></asp:TextBox>
            
                            
</td>
 <td align="center">
                    
                       <asp:TextBox ID="TextBox30" runat="server" Width="150px"></asp:TextBox>
          
            
</td>
     <td align="center">
                        <asp:CheckBox ID="chksys9" runat="server" />
                    </td>
 <td align="center">
                    
             <asp:TextBox ID="TextBox31" runat="server"  Width="150px" 
                            ></asp:TextBox>
          
            
</td>
 </tr>

 <tr>
                <td align="center">
                    
             <asp:DropDownList ID="DropDownList19" runat="server"  
                            Width="150px" >
                            </asp:DropDownList>
          
            
</td> 

                <td align="center">
                    
             <asp:DropDownList ID="DropDownList20" runat="server"  
                            Width="150px" AutoPostBack="True" 
                            onselectedindexchanged="DropDownList20_SelectedIndexChanged">
                            </asp:DropDownList>
          
</td>
                <td align="center">
                             
                                <asp:TextBox ID="TextBox32" runat="server"  Width="150px"></asp:TextBox>
            
                            
</td>
 <td align="center">
                    
                       <asp:TextBox ID="TextBox33" runat="server" Width="150px"></asp:TextBox>
          
            
</td>
     <td align="center">
                        <asp:CheckBox ID="chksys10" runat="server" />
                    </td>
 <td align="center">
                    
             <asp:TextBox ID="TextBox34" runat="server" Width="150px" 
                            ></asp:TextBox>
          
            
</td>
 </tr>

    </table>
    </center>


            <div class="form-sec-row">
                <label><strong>&nbsp;</strong></label>
                <asp:Button ID="Button1" runat="server" OnClick="Button1_Click" Text="Submit"  Height="28px" CssClass="submit-button" />
                <asp:Button ID="Button2" runat="server" Text="Cancel" OnClick="Button2_Click"  Height="28px" CssClass="submit-button" />
                <div class="clear"></div>
          
            
            <div class="clear"></div>
      
  </asp:View>
       <asp:View ID="View2" runat="server">
     
     <div class="listing"  style="width:100%; height:200px; overflow:auto;"> 
    <asp:GridView ID="GridView1" runat="server" AllowPaging="True" CssClass="grid" PagerStyle-CssClass="pgr"
        AutoGenerateColumns="False" 
        DataKeyNames="iddd"  Width="100%"
        OnPageIndexChanging="GridView1_PageIndexChanging"  SelectedRowStyle-BackColor="GreenYellow" 
        OnRowCommand="GridView1_RowCommand" OnRowDeleting="GridView1_RowDeleting" 
        PageSize="10" onrowdatabound="GridView1_RowDataBound">
        <Columns>
            <asp:TemplateField HeaderText="RowId" Visible="false">
                <ItemTemplate>
                    &nbsp;<asp:Label ID="lblId" runat="server" Text='<%# Bind("iddd") %>'></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
            
              <asp:TemplateField HeaderText="Registration No" >
                <ItemTemplate>
                    &nbsp;<asp:Label ID="lblreg" runat="server" Text='<%# Bind("PatientReg") %>'></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
                 <asp:TemplateField HeaderText="Patient's Name" >
                <ItemTemplate>
                    &nbsp;<asp:Label ID="lblname" runat="server" Text='<%# Bind("patient_name") %>'></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
                 <asp:TemplateField HeaderText="Bed No" >
                <ItemTemplate>
                    &nbsp;<asp:Label ID="lblbedno" runat="server" Text='<%# Bind("BedNoText") %>'></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
                 <asp:TemplateField HeaderText="Admission Date" >
                <ItemTemplate>
                    &nbsp;<asp:Label ID="lbladate" runat="server" Text='<%# Bind("adate") %>'></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>

            <asp:TemplateField HeaderText="Type" >
                <ItemTemplate>
                    &nbsp;<asp:Label ID="lbltype" runat="server" Text='<%# Bind("TypeName") %>'></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
            
              
            <asp:TemplateField HeaderText="Shift">
                <ItemTemplate>
                    <asp:Label ID="lblDayShift" runat="server" Text='<%# Bind("ShiftName") %>'></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Charge">
                <ItemTemplate>
                    <asp:Label ID="lblCharges" runat="server" Text='<%# Bind("Charges1") %>'></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Date">
                <ItemTemplate>
                    <asp:Label ID="lblDate" runat="server" Text='<%# Bind("dt") %>'></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Date">
                <ItemTemplate>
                    <asp:Label ID="lblcont" runat="server" Text='<%# Bind("cont") %>' Visible="false"></asp:Label>
                    <asp:CheckBox ID="chksysaya" runat="server" Enabled="false"/>
                </ItemTemplate>
            </asp:TemplateField>
              <asp:TemplateField HeaderText="Remarks">
                <ItemTemplate>
                    <asp:Label ID="lblRemarks" runat="server" Text='<%# Bind("Remarks") %>'></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
            
            <asp:CommandField SelectText="Edit" ShowSelectButton="True" ControlStyle-CssClass="temp" HeaderText="Edit" SelectImageUrl="~/Images/edit.png" ButtonType="Image">
                <ControlStyle CssClass="temp"></ControlStyle>
            </asp:CommandField>
            <asp:CommandField ShowDeleteButton="True" ControlStyle-CssClass="temp" HeaderText="Delete" DeleteImageUrl="~/Images/delete.png" ButtonType="Image">
                <ControlStyle CssClass="temp"></ControlStyle>
            </asp:CommandField>
        </Columns>
        <EditRowStyle BackColor="#CCFF33" />
        <AlternatingRowStyle BackColor="#FFDB91" />
    </asp:GridView>
</div>
<</asp:View>
</asp:MultiView>
</div>
    
    </ContentTemplate>
    </asp:UpdatePanel>
  
</asp:Content>
