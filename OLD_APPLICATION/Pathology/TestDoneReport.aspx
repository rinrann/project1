<%@ Page Title="" Language="C#" MasterPageFile="~/MasterAll/MasterPageAll.master" AutoEventWireup="true" CodeFile="TestDoneReport.aspx.cs" Inherits="Pathology_TestDoneReport" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">&nbsp;
<script type="text/javascript" language="javascript">
    function printDiv(divID) {
        var divElements = document.getElementById(divID).innerHTML;
        var oldPage = document.body.innerHTML;
        document.body.innerHTML = "<html><head><title></title></head><body>" + divElements + "</body>";
        window.print();
        document.body.innerHTML = oldPage;
    }


    function Calling() {

        var date = new Date();
        $("input[id$='TextBox2']").datepicker({ dateFormat: 'dd/mm/yy' });

        var date = new Date();
        $("input[id$='TextBox3']").datepicker({ dateFormat: 'dd/mm/yy' });

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
         <asp:Label ID="Label1" runat="server">Test Done </asp:Label>
     </div>
         <div class="form-sec">
            <div class="error">
                <strong><asp:Label ID="lblError" runat="server" Font-Bold="True"></asp:Label></strong>
                <div class="clear"></div>
            </div></div>
              <div class="formbox">
   
                          <table width="100%">
                           <tr>        
                        <td style="width: 100%" align="center" colspan="5">               
                            <asp:DropDownList ID="DropDownList1" runat="server" CssClass="textbox-medium1" 
                                AutoPostBack="True" onselectedindexchanged="DropDownList1_SelectedIndexChanged">
                                <asp:ListItem>Select</asp:ListItem>
                                <asp:ListItem>Date Range</asp:ListItem>
                                <asp:ListItem>Monthly</asp:ListItem>
                            </asp:DropDownList>   
                        </td>
                    </tr>
                    </table>
                     <asp:Panel ID="Panel1" runat="server">
                              
                           <table width="100%">
                                <tr style='width:900px;'>        
                        <td><label><strong>From Date :</strong> </label> </td>
                          <td> <asp:TextBox ID="TextBox2" runat="server" CssClass="textbox-medium1"></asp:TextBox>
                
                        </td>

                            <td ><label><strong>To Date :</strong> </label></td>
                        
                            <td><asp:TextBox ID="TextBox3" runat="server" CssClass="textbox-medium1"></asp:TextBox></td>
                        <td><asp:Button ID="Button1" runat="server" Text="Generate Report" CssClass="submit-buttonCheck" 
                                onclick="Button1_Click" /></td>
                    </tr></table>
                               </asp:Panel>



                               <asp:Panel ID="Panel2" runat="server">
                               <table width="100%">
                                <tr style='width:900px;'>        
                        <td><label><strong>Month :</strong> </label> </td>
                          <td>   <asp:DropDownList ID="DropDownList2" runat="server" CssClass="textbox-medium1">
                                <asp:ListItem Value="0">Select</asp:ListItem>
                                <asp:ListItem Value="1">January</asp:ListItem>
                                <asp:ListItem Value="2">February</asp:ListItem>
                                <asp:ListItem Value="3">March</asp:ListItem>
                                <asp:ListItem Value="4">April</asp:ListItem>
                                <asp:ListItem Value="5">May</asp:ListItem>
                                <asp:ListItem Value="6">June</asp:ListItem>
                                <asp:ListItem Value="7">July</asp:ListItem>
                                <asp:ListItem Value="8">August</asp:ListItem>
                                <asp:ListItem Value="9">September</asp:ListItem>
                                <asp:ListItem Value="10">October</asp:ListItem>
                                <asp:ListItem Value="11">November</asp:ListItem>
                                <asp:ListItem Value="12">December</asp:ListItem>
                                </asp:DropDownList>
                
                        </td>

                            <td ><label><strong>Year :</strong> </label></td>
                        
                            <td>        <asp:DropDownList ID="DropDownList3" runat="server" CssClass="textbox-medium1">
                                </asp:DropDownList>         
                              
                        </td>
                        <td><asp:Button ID="Button2" runat="server" Text="Generate Report" CssClass="submit-buttonCheck" 
                              onclick="Button2_Click"     /></td>
                    </tr>
                           </table>    </asp:Panel>
                           </div>
                       
                     <table width="100%">
                             
                    <tr>        
                        <td style="width: 100%" >   <div id='mydiv'>            
                            <asp:Literal ID="ltrReport" runat="server"></asp:Literal><br />   </div>                 
                        </td>
                    </tr>
                    <tr>
                     <%--   <td align="right">
                     
                         <input type="button" value="Back" style="width:70px; font-size:x-small" onclick="window.history.back()" />
                         <input type="button" id="cmdPrint" value="Print" style="width:70px; font-size:x-small" onclick="javascript:printDiv('mydiv')"/></td>--%>
                    </tr>
            </table>
    </ContentTemplate>
    </asp:UpdatePanel>
   
</asp:Content>

