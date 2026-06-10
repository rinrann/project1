<%@ Page Language="C#" MasterPageFile="~/MasterAll/MasterPageAll.master" AutoEventWireup="true" CodeFile="PerformingDocWiseMISReport.aspx.cs" Inherits="OPD_PerformingDocWiseMISReport" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <script type="text/javascript">
        function autoCompleteEx_ItemSelected(sender, args) {
            debugger;
            var regname = args.get_value().split('~');
            document.getElementById("ctl00_ContentPlaceHolder1_txtDocId").value = regname[0];
            document.getElementById("ctl00_ContentPlaceHolder1_txtDocName").value = regname[1];
            
        }
 
          function printDiv(divID) {
              var divElements = document.getElementById(divID).innerHTML;
              var oldPage = document.body.innerHTML;
              document.body.innerHTML = divElements;
              window.print();
              document.body.innerHTML = oldPage;
          }
    </script>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
    <ContentTemplate>
        </ContentTemplate>
    </asp:UpdatePanel>
    <div class="pageheader">
         <asp:Label ID="Label1" runat="server">MONTHLY MIS REPORT</asp:Label>
    </div>
    <div class="formbox">
        <div class="form-sec">
            <div class="error">
                <strong><asp:Label ID="lblError" runat="server" Font-Bold="True"></asp:Label></strong>
                <div class="clear"></div>
            </div>
        </div>
        <table style="width:100%">
            <tr>
                <td colspan="8" align="center">
                    <asp:RadioButtonList ID="rbloption" runat="server" RepeatDirection="Horizontal" AutoPostBack="true" OnSelectedIndexChanged="rbloption_SelectedIndexChanged" Visible="false">                      
                        <asp:ListItem Selected="True" Value="1">MONTHLY MIS REPORT</asp:ListItem>
                      <%--  <asp:ListItem Value="2">Referal Doctor Wise</asp:ListItem>--%>
                    </asp:RadioButtonList>
                </td>
            </tr>
            <tr>
                <td style="width:10%">
                    <label><strong>From Date :</strong></label> 
                </td>
                <td style="width:15%">
                    <asp:TextBox ID="txtfromdt" runat="server" TextMode="Date"></asp:TextBox>
                                   
                </td>
                <td style="width:10%">
                    <label><strong>To Date :</strong></label> 
                </td>
                <td  style="width:15%">
                        <asp:TextBox ID="txttodt" runat="server" TextMode="Date"></asp:TextBox>
                               
                </td>
                <td id="td_1" runat="server"  style="width:15%">
                 <strong>   Performing Doctor : </strong>
                </td>
                   <td id="td_2" runat="server" visible="false" style="">
                 <strong>   Referal Doctor :</strong>
                </td>
                <td id="td_3" runat="server" style="width:20%">
                    <div style="display:none;">
                        <asp:TextBox ID="txtDocId" CssClass="textbox-medium1"  runat="server"  style="width:100%;" ></asp:TextBox>
                    </div>
                    
                    <asp:TextBox ID="txtDocName" CssClass="textbox-medium1"  runat="server"  style="width:100%;"></asp:TextBox>
                   <cc1:AutoCompleteExtender ServiceMethod="SearchDoctorName"    OnClientItemSelected="autoCompleteEx_ItemSelected"    MinimumPrefixLength="1"
                        CompletionInterval="100" EnableCaching="false" CompletionSetCount="10" TargetControlID="txtDocName"  ID="AutoCompleteExtender1" runat="server" 
                       FirstRowSelected = "false" >
                    </cc1:AutoCompleteExtender>
                </td>
                <td style="width:5%">
                    
                </td>
                <td style="width:10%;">  
                   <asp:Button ID="btnGenRpt" runat="server" Text="Generate Report" CssClass="submit-generate" OnClick="btnGenRpt_Click"/>

               </td>
            </tr>
            
        </table>
   
    <table width="100%">
        <tr>        
            <td align="center">  
               <h3 id="hd" runat="server" visible="false"> MONTHLY MIS REPORT</h3>
                  <div id='mydiv' style="overflow:auto;width:100%">              
                <asp:Literal ID="ltrReport" runat="server"></asp:Literal><br />

                                   </div>                  
            </td>
        </tr>
        <tr>
            <td align="center">
                   <asp:Button ID="BtnBack" runat="server" style="width:70px; font-size:x-small" Text="Back" OnClick="BtnBack_Click"  />
                <asp:Button ID="btn_excel" runat="server" style="width:100px; font-size:x-small" Text="Export to Excel" OnClick="btn_excel_Click" />
                <%--<input type="button" id="cmdPrint" value="Print" style="width:70px; font-size:x-small" onclick="javascript: printDiv('mydiv')"/>--%>

            </td>
        </tr>
        </table>
        </div>
        
</asp:Content>
