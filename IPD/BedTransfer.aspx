<%@ Page Title="" Language="C#" MasterPageFile="~/MasterAll/MasterPageAll.master" AutoEventWireup="true" CodeFile="BedTransfer.aspx.cs" Inherits="IPD_BedTransfer" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
 
<script type="text/javascript" language="javascript">
    function ShowDialog() {
        var rtvalue = window.open("IpdpatientPopup.aspx", "sss", "Width:800px; Height:550px; dialogLeft:250px;");
        document.getElementById("ctl00_ContentPlaceHolder1_TextBox3").value = rtvalue.NameValue;

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
        document.getElementById("ctl00_ContentPlaceHolder1_TextBox2").value = time;
        document.getElementById("ctl00_ContentPlaceHolder1_TextBox1").value = datetime;
    }

    function ShowDialog1() {
        var rtvalue = window.open("BedAllocationPopup.aspx", "sss", "Width:800px; Height:550px; dialogLeft:250px;");
        document.getElementById("ctl00_ContentPlaceHolder1_TextBox5").value = rtvalue.NameValue;
        document.getElementById("ctl00_ContentPlaceHolder1_HiddenField2").value = rtvalue.ProfessionValue;
    }

    function Calling() {
        var date = new Date();
        $("input[id$='TextBox1']").datepicker({ dateFormat: 'dd/mm/yy' });
        $("input[id$='TextBox2']").timepicker({
            showPeriod: true,
            showLeadingZero: true
        });



        $("input[id$='Tab2']").click(function () {

            if ($("input[id$='TextBox3']").val() == '') {
                alert('Please Enter Registration No!');
                $("input[id$='TextBox3']").focus();
                $("input[id$='TextBox3']").addClass('textboxerr');
                return false;
            }
            else {
                $("input[id$='TextBox3']").removeClass('textboxerr');
            }
        });


        $("input[id$='Button1']").click(function () {

            if ($("input[id$='TextBox5']").val() == '') {
                alert('Please Select A Transfer to Bed First !');
                $("input[id$='TextBox5']").focus();
                $("input[id$='TextBox5']").addClass('textboxerr');
                return false;
            }
            else {
                $("input[id$='TextBox5']").removeClass('textboxerr');
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
    
       <div id="aaa" class="progressBackgroundFilter">          </div>
        <div id="bbb" class="processMessage"><img alt="Loading" src="../images/pwait.gif"/>
      
              </div>
    </ProgressTemplate>
    </asp:UpdateProgress>


    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
    <ContentTemplate>
       <div class="pageheader">
                <asp:Label ID="Label1" runat="server">Bed Transfer</asp:Label>
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
                    <asp:HiddenField ID="HiddenField1" runat="server" Value="0" /><asp:HiddenField ID="HiddenField2" runat="server" Value="0" />
                    <asp:HiddenField ID="HiddenField3" runat="server" />
                       <div class="form-sec-row">
                        <label>
                        <strong>Registration No :</strong></label>
                        <asp:TextBox ID="TextBox3" runat="server" CssClass="textbox-medium1" ></asp:TextBox>
                            <asp:Button ID="Button3" runat="server" Text="Quick Search"  Height="28px" CssClass="submit-buttonCheck" OnClientClick="ShowDialog()" OnClick="Button3_Click"/>
                              <asp:Button ID="Button4" runat="server" Text="Fetch"  Height="28px"
                               CssClass="submit-button" onclick="Button4_Click"/>
                        <div class="clear">
                        </div>
                    </div>

                       <div class="form-sec-row">
                        <label>
                        <strong>Patient's Name :</strong></label>
                        <asp:TextBox ID="TextBox4" runat="server" CssClass="textbox-medium1" 
                            Enabled="False"></asp:TextBox>
                        <div class="clear">
                        </div>
                    </div>
                    <div class="form-sec-row">
                        <label>
                        <strong>Current Bed No :</strong></label>
                        <asp:TextBox ID="txtBedNo" runat="server" CssClass="textbox-medium1" 
                            Enabled="False" ></asp:TextBox>
                        <div class="clear">
                        </div>
                    </div>
           
                     
                     <div class="form-sec-row">
                        <label>
                        <strong> 
                       Transfer To Bed :</strong></label>
                     
                         <asp:TextBox ID="TextBox5" runat="server" CssClass ="textbox-medium1"  Enabled="false" ></asp:TextBox>
                         <asp:Button ID="Button9" runat="server" Text="Search"  Height="28px" 
                             OnClientClick="ShowDialog1()" CssClass="submit-button"   />
                              <div class="clear">
                        </div>
                    </div>
                     <div class="form-sec-row">
                        <label>
                        <strong> 
                       Date of Transfer:</strong></label>
                        
                         <asp:TextBox ID="TextBox1" runat="server"  CssClass="textbox-medium1" ></asp:TextBox>
                                          <cc1:MaskedEditExtender ID="MaskedEditExtender1" runat="server" TargetControlID="TextBox1"
                                            Mask="99/99/9999" MessageValidatorTip="true" MaskType="Date" DisplayMoney="Left"
                                            AcceptNegative="Left" ErrorTooltipEnabled="True" CultureName="en-GB" />
                               <div class="clear">
                        </div>
                    </div>

                        <div class="form-sec-row">
                        <label>
                        <strong> 
                       Time Of Transfer:</strong></label>
                        
                            <asp:TextBox ID="TextBox2" runat="server"  CssClass="textbox-medium1" ></asp:TextBox>
                               <div class="clear">
                        </div>
                    </div>

                    

                    <div class="form-sec-row">
                        <div class="clear">
                        </div>
                    </div>
                    <div class="form-sec-row">
                        &nbsp;&nbsp;&nbsp;<label><strong>&nbsp;</strong></label>
                        <asp:Button ID="Button1" runat="server" CssClass="submit-button"   Height="30px"
                            Text="Submit" onclick="Button1_Click" />
                        <asp:Button ID="Button2" runat="server" CssClass="submit-button"  Height="30px"
                            Text="Cancel" onclick="Button2_Click"  />
                        <div class="clear">
                        </div>
                    </div>
                    <div class="clear">
                    </div>
                      </div>
                      </asp:View>
                                  <asp:View ID="View2" runat="server">
                    <div class="listing">
                <asp:GridView id="GridView1"  Width="100%" CssClass="grid" PagerStyle-CssClass="pgr"
                runat="server" AutoGenerateColumns="False" AllowPaging="True" 
                SelectedRowStyle-BackColor="GreenYellow"
                 OnPageIndexChanging="GridView1_PageIndexChanging" OnRowDataBound="GridView1_RowDataBound" onrowcommand="GridView1_RowCommand" 
                   > 
                    <Columns>

                    
                        <asp:TemplateField HeaderText="RowId" Visible="false" >
                            <ItemTemplate>
                                <asp:Label ID="lblRowId" runat="server" Text='<%# Bind("RowId") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>


                            <asp:TemplateField HeaderText="Patient Name">
                            <ItemTemplate>
                                <asp:Label ID="lblname" runat="server" Text='<%# Bind("patient_name") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>


                        <asp:TemplateField HeaderText="Patient Age">
                            <ItemTemplate>
                                <asp:Label ID="lblage" runat="server" Text='<%# Bind("age") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>


                              <asp:TemplateField HeaderText="Bed No Id" Visible="false">
                            <ItemTemplate>
                                <asp:Label ID="lblbednoId" runat="server" Text='<%# Bind("BNo") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>


                              <asp:TemplateField HeaderText="Bed No">
                            <ItemTemplate>
                                <asp:Label ID="lblbedno" runat="server" Text='<%# Bind("BedNo") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>

                          <asp:TemplateField HeaderText="From Date">
                            <ItemTemplate>
                                <asp:Label ID="lblfromdate" runat="server" Text='<%# Bind("fromdate") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="From Time">
                            <ItemTemplate>
                                <asp:Label ID="lblFromTime" runat="server" Text='<%# Bind("FromTime") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>

                          <asp:TemplateField HeaderText="Upto Date">
                            <ItemTemplate>
                                <asp:Label ID="lbluptodate" runat="server" Text='<%# Bind("todate") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Upto Time">
                            <ItemTemplate>
                                <asp:Label ID="lbluptotime" runat="server" Text='<%# Bind("ToTime") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                     
                                    
                <%--        <asp:CommandField SelectText="Edit" ShowSelectButton="True" ControlStyle-CssClass="temp" HeaderText="Edit" SelectImageUrl="~/Images/edit.png" ButtonType="Image">
                            <ControlStyle CssClass="temp"></ControlStyle>
                        </asp:CommandField>--%>
             <%--           <asp:CommandField ShowDeleteButton="True" ControlStyle-CssClass="temp" HeaderText="Delete" DeleteImageUrl="~/Images/delete.png" ButtonType="Image">
                            <ControlStyle CssClass="temp"></ControlStyle>
                        </asp:CommandField>--%>
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

