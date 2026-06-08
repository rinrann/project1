<%@ Page Title="" Language="C#" MasterPageFile="~/MasterAll/MasterPageAll.master" AutoEventWireup="true" CodeFile="TestResultReport.aspx.cs" Inherits="Pathology_TestResultReport" %>

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
    function ShowDialog() {

        var rtvalue = window.open("TestResultRegPopUp.aspx?TestID=R", "sss", "Width:800px; Height:550px; dialogLeft:250px;");
        //document.getElementById("ctl00_ContentPlaceHolder1_txtreg").value = rtvalue.NameValue;
        //document.getElementById("ctl00_ContentPlaceHolder1_HiddenField1").value = rtvalue.ProfessionValue;
    }      
</script>

<%--For Busy Loader .............................--%>
 
<%--  <asp:UpdateProgress ID="UpdateProgress1" runat="server">
    <ProgressTemplate>
    
       <div id="aaa" class="progressBackgroundFilter">          </div>
        <div id="bbb" class="processMessage"><img alt="Loading" src="../images/pwait.gif"/>
      
              </div>
    </ProgressTemplate>
    </asp:UpdateProgress>--%>

    <%--For Busy Loader End.............................--%>

    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
    <ContentTemplate>
    <div class="pageheader">
         <asp:Label ID="Label1" runat="server">Test Result Report</asp:Label>
     </div>
     <div class="formbox">
        <div class="form-sec">
            <div class="error">
                <strong><asp:Label ID="lblError" runat="server" Font-Bold="True"></asp:Label></strong>
                <div class="clear"></div>
            </div></div>
           <table width="100%">
           <tr>
             <div class="form-sec-row">
           <td>  <label><strong>Registration No :</strong></label> </td>
           <td>  <asp:TextBox ID="txtreg" CssClass="textbox-medium1"  runat="server" 
                    Enabled="False"></asp:TextBox></td>
                    <td> <asp:Button ID="Button3" runat="server" Text="Search"  CssClass="submit-button"  OnClientClick="ShowDialog()"/>
              </td>
              <td> 
                  <asp:Button ID="Button1" runat="server" Text="Fetch" CssClass="submit-button" 
                      onclick="Button1_Click" /> </td>
              <td><asp:DropDownList ID="DropDownList1" runat="server" CssClass="textbox-dropdown">
                 </asp:DropDownList></td>
                 <td>  <asp:Button ID="Button4" runat="server" Text="Generate Report" 
                     CssClass="submit-generate" onclick="Button4_Click" /></td>
           </div>
           </tr>
           </table>
  
           
         <asp:HiddenField ID="HiddenField1" runat="server" />
                      </div>
                         <table width="100%">
                    <tr>        
                        <td align="center">    <div id='mydiv'>              
                            <asp:Literal ID="ltrReport" runat="server"></asp:Literal><br />  </div>                  
                        </td>
                    </tr>
                    <tr>
                        <td align="center">
                     
                         <input type="button" id="btnBack" runat="server" value="Back" style="width:70px; font-size:x-small" onclick="window.history.back()" />
                         <input type="button" id="cmdPrint" runat="server" value="Print" style="width:70px; font-size:x-small" onclick="javascript:printDiv('mydiv')"/></td>
                    </tr>
            </table>
                            
    

     
    </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>

