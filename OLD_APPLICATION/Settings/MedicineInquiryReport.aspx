<%@ Page Title="" Language="C#" MasterPageFile="~/MasterAll/MasterPageAll.master" AutoEventWireup="true" CodeFile="MedicineInquiryReport.aspx.cs" Inherits="Assignment_MedicineInquiryReport" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">


<script language="javascript" type="text/javascript">


    function printDiv(divID) {
        var divElements = document.getElementById(divID).innerHTML;
        // document.getElementById("ctl00_ContentPlaceHolder1_mydiv").style.width = '900px';
        var oldPage = document.body.innerHTML;
        document.body.innerHTML = "<html><head><title></title></head><body>" + divElements + "</body>";
        window.print();
        document.body.innerHTML = oldPage;
    }





    function Calling() {

        //            var date = new Date();
        //            $("input[id$='txtvalidityDate']").datepicker({ dateFormat: 'dd/mm/yy', minDate: date });

        var date = new Date();
        $("input[id$='txtFromDate']").datepicker({ dateFormat: 'dd/mm/yy' });
        $("input[id$='txtToDate']").datepicker({ dateFormat: 'dd/mm/yy' });
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

 

 
<%--<asp:UpdateProgress ID="UpdateProgress1" runat="server">
    <ProgressTemplate>
    
       <div id="aaa" class="progressBackgroundFilter">          </div>
        <div id="bbb" class="processMessage"><img alt="Loading" src="../images/pwait.gif"/>
      
              </div>
    </ProgressTemplate>
    </asp:UpdateProgress>--%>




 <asp:UpdatePanel ID="UpdatePanel1" runat="server">
    <ContentTemplate>
     <div class="pageheader">
         <asp:Label ID="Label1" runat="server">Medicine Inquiry Report</asp:Label>
     </div>
     <div class="formbox">
        <div class="form-sec">
            <div class="error">
                <strong><asp:Label ID="lblError" runat="server" Font-Bold="True"></asp:Label></strong>
                <div class="clear"></div>
            </div></div>

           <table width="100%">
             <tr>
           <td>
           
            <label>From Date :</label>
             </td>
                 <td>
               <asp:TextBox ID="txtFromDate" runat="server" CssClass="textbox-medium1"  Width="150px"></asp:TextBox>
                 </td>
                   <td>
                 
                <label>To Date :</label>
                 </td>
                 <td>
               <asp:TextBox ID="txtToDate" runat="server" CssClass="textbox-medium1"  Width="150px">
               </asp:TextBox>
               </td>
                  <td>
                 
                <label>Search Type :</label>
                 </td>
               <td>
                   <asp:DropDownList ID="ddlReport" runat="server" CssClass="textbox-medium1"  
                       Width="150px" AutoPostBack="True" 
                       onselectedindexchanged="ddlReport_SelectedIndexChanged">
                       <asp:ListItem>-Select-</asp:ListItem>
                       <asp:ListItem>Medicine</asp:ListItem>
                       <asp:ListItem>Surgical</asp:ListItem>
                   </asp:DropDownList>
               </td>
           </tr>
           </table>

         <asp:Panel ID="Panel1" runat="server">
         <table width="100%">

           <tr>

           <td>
           
           <label>Gr :</label>
          
           </td>
             <td>
                 <asp:DropDownList ID="ddlMedicineGroup" runat="server" 
                     CssClass="textbox-medium1"  Width="140px" 
                     onselectedindexchanged="ddlMedicineGroup_SelectedIndexChanged" 
                     AutoPostBack="True">
                 </asp:DropDownList>
           </td>



            <td>
           
               <label>Sub Gr :</label> 
         
           </td>
             <td>
                 <asp:DropDownList ID="ddlMedicineSubGroup" runat="server" 
                     CssClass="textbox-medium1"  Width="140px" AutoPostBack="True" 
                     onselectedindexchanged="ddlMedicineSubGroup_SelectedIndexChanged">
                 </asp:DropDownList>
           </td>


            <td>
            
             <label>Medicine :</label> 
          
           </td>
             <td>
                 <asp:DropDownList ID="ddlMedicineName" runat="server" 
                     CssClass="textbox-medium1"  Width="150px" AutoPostBack="True">
                 </asp:DropDownList>
           </td>


           
            <td>
           <label>Search Dtls :</label>
          
           </td>
             <td>
                 <asp:DropDownList ID="ddlSearchDetails" runat="server" CssClass="textbox-medium1"  Width="150px">
                     <asp:ListItem>-Select-</asp:ListItem>
                     <asp:ListItem>Price Details</asp:ListItem>
                     <asp:ListItem>Purchase Details</asp:ListItem>
                     <asp:ListItem>Supplier Details</asp:ListItem>
                     <asp:ListItem>Stock Details</asp:ListItem>
                 </asp:DropDownList>
           </td>

           <td>
               <asp:Button ID="Button3" runat="server" Text="Report"  
                   CssClass="submit-button" onclick="Button3_Click" />
           </td> 
          
           </tr>
           </table>
         </asp:Panel>

         







           <table width="100%">

                  <tr>
           <td colspan="6" align="center">
               <asp:RadioButtonList ID="RadioButtonList1" runat="server"  Width="250px"
                   RepeatDirection="Horizontal">
                   <asp:ListItem>With Header</asp:ListItem>
                   <asp:ListItem>Without Header</asp:ListItem>
               </asp:RadioButtonList>
               </td>
           </tr>
           </table>
             
                      </div>
                         <table width="100%">
                    <tr>        
                        <td align="center">    <div id='mydiv' runat="server">              
                            <asp:Literal ID="ltrReport" runat="server"></asp:Literal><br />  </div>                  
                        </td>
                    </tr>
                    </table>

                     <table width="100%">
                    <tr>
                        <td align="center">
                     
                         <input type="button" id="btnBack" value="Back" runat="server" style="width:70px; font-size:x-small" onclick="window.history.back()" />
                         <input type="button" id="cmdPrint"  runat="server" value="Print" style="width:70px; font-size:x-small" onclick="javascript:printDiv('mydiv')"/>
                            <asp:Button ID="btnPDF" runat="server"  style="width:70px; font-size:x-small"  
                                Text="PDF" />
                         </td>

                    </tr>
            </table>
    </ContentTemplate>

      <Triggers>
  <asp:PostBackTrigger ControlID="btnPDF" />
  </Triggers>

    </asp:UpdatePanel>


</asp:Content>

