<%@ Page Title="" Language="C#"  MasterPageFile="~/MasterAll/MasterPageAll.master" AutoEventWireup="true" CodeFile="ReferPatientList.aspx.cs" Inherits="IPD_ReferPatientList" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

   


<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
     <script src="../Script/jquery.webcam.js" type="text/javascript"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <script type="text/javascript" language="javascript">


        $(function () {
            /* date picker event
            $('.datepicker').datepicker({
                dateFormat: "dd/mm/yy",
                changeMonth: true,
                changeYear: true,
                //yearRange: '1900:' + new Date().getFullYear(),
                yearRange: '1900:2900',
                showOn: "button",
                buttonImage: "../images/calender.png",
                //buttonImage: "../images/green-button.gif",
                buttonImageOnly: true,
                showAnim: "fold"
            });*/
            var date = new Date();
            $("input[id$='TextBox1']").datepicker({ dateFormat: 'dd/mm/yy'});
            var date = new Date();
            $("input[id$='TextBox2']").datepicker({ dateFormat: 'dd/mm/yy'});

            
        });

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
            document.getElementById("ctl00_ContentPlaceHolder1_time").value = time;
            document.getElementById("ctl00_ContentPlaceHolder1_Calendar1").value = datetime;
        }


    function printDiv(divID) {
        var divElements = document.getElementById(divID).innerHTML;
        // document.getElementById("ctl00_ContentPlaceHolder1_mydiv").style.width = '900px';
        var oldPage = document.body.innerHTML;
        document.body.innerHTML = "<html><head><title></title></head><body>" + divElements + "</body>";
        window.print();
        document.body.innerHTML = oldPage;
    }
    function ShowDialog() {

        var rtvalue = window.open("IpdpatientPopup.aspx", "sss", "Width:800px; Height:550px; dialogLeft:250px;");
        document.getElementById("ctl00_ContentPlaceHolder1_txtreg").value = rtvalue.NameValue;

    }

    $(document).ready(function () {
        $("input[id$='Button4']").click(function () {
            if ($("input[id$='txtreg']").val() == '') {
                alert('Please Select A Patient !');
                $("input[id$='txtreg']").focus();
                $("input[id$='txtreg']").addClass('textboxerr');
                return false;
            }
            else {
                $("input[id$='txtreg']").removeClass('textboxerr');
            }

            if ($("select[id$='DropDownList1']").val() == '0') {
                alert('Please Select Language!');
                $("select[id$='DropDownList1']").addClass('textboxerr');
                $("select[id$='DropDownList1']").focus();
                return false;
            }
            else {
                $("select[id$='DropDownList1']").removeClass('textboxerr');
            }
        });
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
         <asp:Label ID="Label1" runat="server">Patient List</asp:Label>
        </div>
        <div class="formbox">
            <div class="form-sec">
                <div class="error">
                    <strong><asp:Label ID="lblError" runat="server" Font-Bold="True"></asp:Label></strong>
                    <div class="clear">

                    </div>
                </div>
            </div>
            <table width="100%">
                <tr>
                       <td>  <label><strong>Refer By :</strong></label> </td>
                       <td>  
                            <asp:DropDownList ID="DropDownList1" Width="140px" runat="server" AutoPostBack="True" 
                            onselectedindexchanged="DropDownList1_SelectedIndexChanged">
                            </asp:DropDownList>
                            <div class="clear">
                            </div>
                       </td>
                        <td>  <label id="refdoc"><strong>Doctor :</strong></label> </td>
                       <td>  
                            <asp:DropDownList ID="DropDownList2" Width="140px" runat="server">
                            </asp:DropDownList>
                            <div class="clear">
                            </div>
                       </td>
                </tr>
                <tr>
                       <td>  <label><strong>From Date:</strong></label> </td>
                       <td>  
                           <asp:TextBox ID="TextBox1" Width="140px" runat="server" CssClass="textbox-medium1"></asp:TextBox>
                            <div class="clear">
                            </div>
                       </td>
                        <td>  <label id="Label2"><strong>To Date :</strong></label> </td>
                       <td>  
                           <asp:TextBox ID="TextBox2" Width="140px" runat="server" CssClass="textbox-medium1"></asp:TextBox>
                            <div class="clear">
                            </div>
                       </td>
                </tr>
                <tr>
           <td colspan="2" align="center">
               <asp:RadioButtonList ID="RadioButtonList1" runat="server"  Width="250px"
                   RepeatDirection="Horizontal">
                   <asp:ListItem>With Header</asp:ListItem>
                   <asp:ListItem>Without Header</asp:ListItem>
               </asp:RadioButtonList>
               </td>
                <td colspan="2" align="center">
                    <asp:Button ID="Button1" runat="server" Text="Generate Report" CssClass="submit-generate" OnClick="Button1_Click" />
                </td>
           </tr>
            </table>
             <asp:HiddenField ID="HiddenField1" runat="server" />
        </div>
        <table width="100%">
                    <tr>        
                        <td align="center">    <div id='mydiv' style="width:100%;">              
                            <asp:Literal ID="ltrReport" runat="server"></asp:Literal><br />  </div>                  
                        </td>
                    </tr>
                    <tr>
                        <td align="center">
                     
                         <input type="button" id="cmdBack" value="Back" style="width:70px; font-size:x-small" onclick="window.history.back()" />
                         <input type="button" id="cmdPrint" value="Print" style="width:70px; font-size:x-small" onclick="javascript: printDiv('mydiv')"/></td>
                    </tr>
            </table>
    </ContentTemplate>
    </asp:UpdatePanel>
    </asp:Content>