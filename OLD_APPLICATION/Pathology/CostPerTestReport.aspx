<%@ Page Title="" Language="C#" MasterPageFile="~/MasterAll/MasterPageAll.master" AutoEventWireup="true" CodeFile="CostPerTestReport.aspx.cs" Inherits="Pathology_CostPerTestReport" %>

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
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
    <ContentTemplate>
        <div class="pageheader">
         <asp:Label ID="Label1" runat="server">Cost Per Test</asp:Label>
     </div>
     <div class="formbox">
        <div class="form-sec">
            <div class="error">
                <strong><asp:Label ID="lblError" runat="server" Font-Bold="True"></asp:Label></strong>
                <div class="clear"></div>
            </div></div>
            <table>
            <tr><div class="form-sec">
              <td>
                <strong><label>Department</label></strong>
          </td>
            <td> <div class="error">
                <asp:DropDownList ID="DropDownList1" runat="server" CssClass="textbox-medium1"  AutoPostBack="True" onselectedindexchanged="DropDownList1_SelectedIndexChanged">
                </asp:DropDownList></div>
          </td>
            <td> 
                <strong><label>Test Name</label></strong>
          </td>
            <td> <div class="error">
                <asp:TextBox ID="TextBox1" runat="server" CssClass="textbox-medium1"></asp:TextBox></div>
          </td>
          <td><div class="error">
          <asp:Button ID="Button1" runat="server" Text="Search" CssClass="submit-button" 
                  onclick="Button1_Click"/></div></td>
          </div>
          
            </tr>
            </table>
                </div>
                         <table width="100%">
                    <tr>        
                        <td style="width: 100%">    <div id='mydiv'>              
                            <asp:Literal ID="ltrReport" runat="server"></asp:Literal></div>                
                        </td>
                    </tr>
                    <tr>
                        <td align="right" colspan="3">
                     
                         <input type="button" value="Back" style="width:70px; font-size:x-small" onclick="window.history.back()" />
                         <input type="button" id="cmdPrint" value="Print" style="width:70px; font-size:x-small" onclick="javascript:printDiv('mydiv')"/></td>
                    </tr>
            </table>
    </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>

