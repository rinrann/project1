<%@ Page Title="" Language="C#" MasterPageFile="~/MasterAll/MasterPageAll.master" AutoEventWireup="true" CodeFile="OperationReportDetails.aspx.cs" Inherits="Assignment_OperationReportDetails" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">

<script language="javascript" type="text/javascript">

    function Calling() {

        //            var date = new Date();
        //            $("input[id$='txtvalidityDate']").datepicker({ dateFormat: 'dd/mm/yy', minDate: date });

        var date = new Date();
        $("input[id$='txtFromDate']").datepicker({ dateFormat: 'dd/mm/yy' });
        $("input[id$='txtTodate']").datepicker({ dateFormat: 'dd/mm/yy' });
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

 <asp:UpdatePanel ID="UpdatePanel1" runat="server">
    <ContentTemplate>

  <div class="pageheader">
         <asp:Label ID="Label1" runat="server">Operation Report</asp:Label>
     </div>
     <div class="formbox">
        <div class="form-sec">
            <div class="error">
                <strong><asp:Label ID="lblError" runat="server" Font-Bold="True"></asp:Label></strong>
                <div class="clear"></div>
            </div></div>
            </div>

               <asp:Panel ID="Panel1" runat="server">
            <table width="100%">

             <tr>

            <td>  <label><strong>Operation Type:</strong></label> </td>
              <td>  
                  <asp:DropDownList ID="ddlOTType" CssClass="textbox-medium1"  Width="150px"  AutoPostBack="true"
                      runat="server" onselectedindexchanged="ddlOTType_SelectedIndexChanged">
                  </asp:DropDownList>
              </td>

           
               <td>  <label><strong>Operation Name:</strong></label> </td>
              <td>  
                 
                  <asp:DropDownList ID="ddlOTName" CssClass="textbox-medium1"  Width="150px"  runat="server">
                  </asp:DropDownList>
              </td>

              <td>  <label><strong>Patient Name:</strong></label> </td>
              <td>   <asp:TextBox ID="txtPatientName" CssClass="textbox-medium1" runat="server" Width="150px"></asp:TextBox> </td>

           </tr>

           <tr>

            <td>  <label><strong>From Date:</strong></label> </td>
              <td>   <asp:TextBox ID="txtFromDate" CssClass="textbox-medium1" runat="server" Width="150px"></asp:TextBox> </td>

               <td>  <label><strong>To Date:</strong></label> </td>
              <td colspan="2">   <asp:TextBox ID="txtTodate" CssClass="textbox-medium1" runat="server" Width="150px"></asp:TextBox> </td>

             
              <td> <asp:Button ID="btnSearch" runat="server"  CssClass="submit-generate" 
                      Text="Search" onclick="btnSearch_Click" /></td>


           </tr>

            </table>
             </asp:Panel>

              <table width="100%">
                    <tr>        
                        <td style="width: 100%">    <div id='mydiv' runat="server">                 
                            <asp:Literal ID="ltrReport" runat="server"></asp:Literal><br />    </div>                
                        </td>
                    </tr>
                    <tr>
                        <td align="center">
                     
                               <input type="button" id="btnBack" value="Back" runat="server" style="width:70px; font-size:x-small" onclick="window.history.back()" />
                                <input type="button" id="cmdPrint"  runat="server" value="Print" style="width:70px; font-size:x-small" onclick="javascript:printDiv('mydiv')"/>
                            <asp:Button ID="btnPDF" runat="server"  style="width:70px; font-size:x-small" 
                                   Text="PDF"/>  
                         
                         
                         </td>
                    </tr>
            </table>

             </ContentTemplate>
    </asp:UpdatePanel>


</asp:Content>


