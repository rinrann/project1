<%@ Page Language="C#" MasterPageFile="~/MasterAll/MasterPageAll.master" AutoEventWireup="true" CodeFile="WingWiseMedicineAllot.aspx.cs" Inherits="Medicine_WingWiseMedicineAllot" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <script type="text/javascript" language="javascript">


        function printDiv(divID) {
            var divElements = document.getElementById(divID).innerHTML;
            var oldPage = document.body.innerHTML;
            document.body.innerHTML = "<html><head><title></title></head><body>" + divElements + "</body>";
            window.print();
            document.body.innerHTML = oldPage;
        }
    </script>

    <script language="javascript" type="text/javascript">
        $(function () {
            /* date picker event*/
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
            });
        });

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

        function Calling() {
            var date = new Date();
            $("input[id$='frmdt']").datepicker({ dateFormat: 'dd/mm/yy' });

            $("input[id$='todt']").datepicker({ dateFormat: 'dd/mm/yy' });
        }

     </script>

    <div class="pageheader">
            <asp:Label ID="Label1" runat="server">Wing Wise Medicine Allocation</asp:Label>
    </div>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <div class="formbox">
                <center>
                    <table width="100%">
                        <tr>
                            <td>
                                <label><strong>&nbsp;&nbsp;Floor:</strong></label>
                            </td>
                            <td>
                                <asp:DropDownList ID="ddlFloor" runat="server" 
                        CssClass="textbox-medium1" OnSelectedIndexChanged="ddlfloor_selectedIndexChange" AutoPostBack="true" Width="150"></asp:DropDownList>
                            </td>
                            <td>
                                <label><strong>&nbsp;&nbsp;WorkStation:</strong></label>
                            </td>
                            <td>
                                <asp:DropDownList ID="ddlWings" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlwing_SelectedIndexChange" Width="100px"></asp:DropDownList>
                            </td>
                            <td>
                                <label><strong>&nbsp;&nbsp;Medicine:</strong></label>
                            </td>
                            <td>
                                <asp:DropDownList ID="ddlmed" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlmed_SelectedIndexChange" Width="100px"></asp:DropDownList>
                            </td>
                            <td>  <label><strong>&nbsp;&nbsp;From Date:</strong></label> </td>
                           <td>  
                               <asp:TextBox ID="frmdt" runat="server" CssClass="textbox-medium2" Width="100px" OnTextChanged="frmdt_textChange" AutoPostBack="true"></asp:TextBox>
                                <div class="clear">
                                </div>
                           </td>
                            <td>  <label id="Label2"><strong>&nbsp;&nbsp;To Date :</strong></label> </td>
                           <td>  
                               <asp:TextBox ID="todt" runat="server" CssClass="textbox-medium2" Width="100px" OnTextChanged="todt_textChange" AutoPostBack="true"></asp:TextBox>
                                <div class="clear">
                                </div>
                           </td>
                        </tr>
                    </table>
                </center>
            </div>

            <div class="formbox">
                <table width="100%">
                    <tr>
                        <td  align="center"> 
        
                            <asp:RadioButtonList ID="RadioButtonList1" runat="server" Width="350px" 
                                AutoPostBack="True" 
                                onselectedindexchanged="RadioButtonList1_SelectedIndexChanged" 
                                RepeatDirection="Horizontal">
                                <asp:ListItem>With Header</asp:ListItem>
                                <asp:ListItem>Without Header</asp:ListItem>
                            </asp:RadioButtonList>
                         </td>
                    </tr>
                 </table>
            </div>
            <div class="formbox">
               <div class="form-sec">
                    <table width="100%">
                        <tr>        
                            <td>
                                <div id='mydiv'>              
                                    <asp:Literal ID="ltrReport" runat="server"></asp:Literal>
                                </div>                
                            </td>
                        </tr>
                        <tr>
                            <td align="center">
                                <input type="button" value="Back" style="width:70px; font-size:x-small" onclick="window.history.back()" />
                                <input type="button" id="cmdPrint" value="Print" style="width:70px; font-size:x-small" onclick="javascript: printDiv('mydiv')"/>
                            </td>
                        </tr>
                    </table>
                </div>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>