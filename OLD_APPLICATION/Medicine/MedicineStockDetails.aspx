<%@ Page Language="C#"  MasterPageFile="~/MasterAll/MasterPageAll.master" AutoEventWireup="true" CodeFile="MedicineStockDetails.aspx.cs" Inherits="Medicine_MedicineStockDetails" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
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

        function SetContextKey() {
            $find('AutoCompleteExtender1').set_contextKey("GFC");
        }   

        function autoCompleteEx_ItemSelected(sender, args) {

            var regname = args.get_value().split('~');// alert(regname[0]);
            document.getElementById("ctl00_ContentPlaceHolder1_TextBoxicode").value = regname[0];
            document.getElementById("ctl00_ContentPlaceHolder1_TextBox8").value = regname[1];
            // document.getElementById("TextBox8").value = regname[0];
            //document.getElementById("TextBox1").value = regname[0];
            $("#TextBox8").focus();
            //$("#DropDownList4").val(regname[1]);
        }

     </script>
    <div class="pageheader">
            <asp:Label ID="Label1" runat="server">Medicine/Reagent Stock Details</asp:Label>
    </div>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <div class="formbox">
                <center>
                    <table width="900px;">
                        <tr>
                            <td>   
                                <label><strong>Medicine/Reagent :</strong></label>
                            </td>
                            <td  colspan="2"> 
                                <asp:DropDownList ID="DropDownList5" runat="server" CssClass="textbox-medium1" OnSelectedIndexChanged="ddl5_selecttedIndexChanged" AutoPostBack="true"></asp:DropDownList>
                            </td>
                            
                        </tr>
                        <tr>
                            <%--<td>  <label><strong>&nbsp;&nbsp;From Date:</strong></label> </td>
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
                           </td>--%>
                            <td>   <label><strong>
                     Medincine :</strong></label></td>
                    
                    <td>  
                         
                        <asp:TextBox ID="TextBox8" runat="server" OnTextChanged="medicinechange" AutoPostBack="true" Width="250px"  CssClass="textbox-medium1" ></asp:TextBox>
                                   <cc1:AutoCompleteExtender ServiceMethod="SearchMedicineName" OnClientItemSelected="autoCompleteEx_ItemSelected" MinimumPrefixLength="1"
                                    CompletionInterval="100" EnableCaching="false" CompletionSetCount="10" TargetControlID="TextBox8" ID="AutoCompleteExtender1" runat="server" 
                                       FirstRowSelected = "false" ></cc1:AutoCompleteExtender>
                        <asp:TextBox ID="TextBoxicode" runat="server" Width="150px"  CssClass="textbox-medium1" style="display:none;"></asp:TextBox> 
                     </td>
                            <td style="width:10%;">
                                <div class="form-sec-row"> 
                                   <asp:Button ID="Button1" runat="server" Text="Generate" CssClass="submit-button1"  Height="28px" onclick="Button1_Click"/>
                                </div>                  
                        </td>  
                        </tr>
                    </table>
                </center>
            </div>

            <div class="formbox">
                <table width="100%;">
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
                    <table width="100%;">
                        <tr>        
                            <td>
                                <div id='mydiv' style="width:100%;">              
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
