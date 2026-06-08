<%@ Page Language="C#" MasterPageFile="~/MasterAll/MasterPageAll.master" AutoEventWireup="true" CodeFile="PatientQueue.aspx.cs" Inherits="OPD_PatientQueue" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <script type="text/javascript" language="javascript">

        function Calling() {

            var date = new Date();
            $("input[id$='TextBox3']").datepicker({ dateFormat: 'dd/mm/yy' });
            $("input[id$='TextBox1']").timepicker({
                showPeriod: true,
                showLeadingZero: true
            });

            $("input[id$='TextBox2']").timepicker({
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


        function printDiv(divID) {
            var divElements = document.getElementById(divID).innerHTML;
            // document.getElementById("ctl00_ContentPlaceHolder1_mydiv").style.width = '900px';
            var oldPage = document.body.innerHTML;
            document.body.innerHTML = "<html><head><title></title></head><body>" + divElements + "</body>";
            window.print();
            document.body.innerHTML = oldPage;
        }


        function autoCompleteEx_ItemSelected(sender, args) {
            debugger;
            var regname = args.get_value().split('~');
            document.getElementById("ctl00_ContentPlaceHolder1_txtDocId").value = regname[0];
            document.getElementById("ctl00_ContentPlaceHolder1_txtDocName").value = regname[1];

        }

</script>
    <div class="pageheader">
         <asp:Label ID="Label1" runat="server">Patient Queue</asp:Label>
    </div>
    <div class="formbox">
        <div class="form-sec">
            <%--<div class="error">
                <strong><asp:Label ID="lblError" runat="server" Font-Bold="True"></asp:Label></strong>
                <div class="clear"></div>
            </div></div></div>--%>
    <div>
        <table cellpadding="0" cellspacing="0"  title="Search" width="100%"> 
            <tr>
                <td style="width:15%">
                 <label class="ipdList" style='width:75px;'><strong> Doctor : </strong></label>
                </td>
                <td style="width:20%">
                    <div style="display:none;">
                        <asp:TextBox ID="txtDocId" CssClass="textbox-medium1"  runat="server"  style="width:100%;" ></asp:TextBox>
                    </div>
                    
                    <asp:TextBox ID="txtDocName" CssClass="textbox-medium1"  runat="server"  style="width:100%;"></asp:TextBox>
                   <cc1:AutoCompleteExtender ServiceMethod="SearchDoctorName"    OnClientItemSelected="autoCompleteEx_ItemSelected"    MinimumPrefixLength="1"
                        CompletionInterval="100" EnableCaching="false" CompletionSetCount="10" TargetControlID="txtDocName"  ID="AutoCompleteExtender1" runat="server" 
                       FirstRowSelected = "false" >
                    </cc1:AutoCompleteExtender>
                </td>
                <td  style="width:10%;">
                   <label class="ipdList" style='width:75px;'><strong>Date :</strong></label>
                </td>
                <td style="width:10%;">
                    <asp:TextBox ID="TextBox3" runat="server" CssClass="textbox-medium1" Width="138px" TextMode="Date"></asp:TextBox>
                </td>
                <td style="width:10%;">
                       
                </td>
                <td style="width:10%;">
                    <label class="ipdList" style='width:75px;'><strong>Printing Option :</strong></label>
                </td>
                <td style="width:40%;">
                   <asp:RadioButtonList ID="RadioButtonList1" runat="server"  Width="250px"
                        RepeatDirection="Horizontal">
                        <asp:ListItem Value="N" Text="Without Header" Selected="True"></asp:ListItem>
                        <asp:ListItem Value="Y" Text="With Header"></asp:ListItem>
                    </asp:RadioButtonList> 
                </td>
                
                
                
                
                <td style="width:10%;">
                    &nbsp;
                </td>
                <td style="width:10%;">
                        <div class="form-sec-row"> 
                           <asp:Button ID="Button1" runat="server" Text="Generate" CssClass="submit-button1"  Height="28px" onclick="Button1_Click"/>
                        </div>                  
                </td>             
                      
            </tr>
        </table>
        <table cellpadding="0" cellspacing="0"  title="Search" width="100%">
            <tr>
                <td align="center" style="width:100%;">
                    <div id='mydiv'  style="width:100%;">              
                            <asp:Literal ID="ltrReport" runat="server"></asp:Literal><br />  

                    </div>    
                </td>
            </tr>
            <tr>
                <td align="center">
                     
                    <%--<input type="button" value="Back" style="width:70px; font-size:x-small" onclick="window.history.back()" />--%>
                    <input type="button" id="cmdPrint" value="Print" style="width:70px; font-size:x-small" onclick="javascript: printDiv('mydiv')"/>

                </td>
            </tr>
        </table>

    </div>
</asp:Content>