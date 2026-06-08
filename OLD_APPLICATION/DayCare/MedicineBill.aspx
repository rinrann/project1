<%@ Page Title="" Language="C#" MasterPageFile="~/MasterAll/MasterPageAll.master" AutoEventWireup="true" CodeFile="MedicineBill.aspx.cs" Inherits="DayCare_MedicineBill" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
<script type="text/javascript" language="javascript">

    function printDiv(divID) {
        var divElements = document.getElementById(divID).innerHTML;
        // document.getElementById("ctl00_ContentPlaceHolder1_mydiv").style.width = '900px';
        var oldPage = document.body.innerHTML;
        document.body.innerHTML = "<html><head><title></title></head><body>" + divElements + "</body>";
        window.print();
        document.body.innerHTML = oldPage;
    }

    function ShowDialog() {

        var rtvalue = window.open("RegistrationPopup.aspx", "sss", "Width:800px; Height:550px; dialogLeft:250px;");
        //document.getElementById("ctl00_ContentPlaceHolder1_txtreg").value = rtvalue.NameValue;

    }

    function Calling() {
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
         <asp:Label ID="Label1" runat="server">Medicine Bill</asp:Label>
     </div>
     <div class="formbox">
        <div class="form-sec">
            <div class="error">
                <strong><asp:Label ID="lblError" runat="server" Font-Bold="True"></asp:Label></strong>
                <div class="clear"></div>
            </div></div>
           <table width="100%">
           <tr>
   
           <td>  <label><strong>Registration No :</strong></label> </td>
           <td>  <asp:TextBox ID="txtreg" CssClass="textbox-medium1"  Enabled="false" runat="server" 
                   Width="150px"  ></asp:TextBox></td>
           <td> <asp:Button ID="Button3" runat="server" Text="Search"  CssClass="submit-button" Height="27px" Width="65px" OnClientClick="ShowDialog()"/></td>
        
              <td>  <label><strong>Generate Option:</strong></label> </td>
                 <td>  
                     <asp:DropDownList ID="DropDownList1" runat="server" CssClass="textbox-medium1" 
                         Width="150px"  >
                         <asp:ListItem Value="0">-- Select --</asp:ListItem>
                         <asp:ListItem Value="1">Bengali</asp:ListItem>
                         <asp:ListItem Value="2">English</asp:ListItem>
                     </asp:DropDownList> </td>
         
                        <td>  <asp:Button ID="Button4" runat="server" Height="27px" Text="Generate Report" 
                                CssClass="submit-generate" onclick="Button4_Click" /></td>
  
           </tr>
           </table>
            
         <asp:HiddenField ID="HiddenField1" runat="server" />
                      </div>
                         <table width="100%">
                    <tr>        
                        <td align="center" colspan="6">    <div id='mydiv'>              
                            <asp:Literal ID="ltrReport" runat="server"></asp:Literal><br />  </div>                  
                        </td>
                    </tr>
                    <tr>
                        <td align="right" colspan="3" >
                     
                         <input type="button" value="Back" style="width:70px; font-size:x-small" onclick="window.history.back()" />
                       </td>
                       <td align="left" colspan="3">
                         <input type="button" id="cmdPrint" value="Print" style="width:70px; font-size:x-small" onclick="javascript:printDiv('mydiv')"/>
                       </td>
                    </tr>
            </table>
    </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>

