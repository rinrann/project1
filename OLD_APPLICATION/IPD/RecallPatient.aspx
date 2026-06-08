<%@ Page Title="" Language="C#" MasterPageFile="~/MasterAll/MasterPageAll.master" AutoEventWireup="true" CodeFile="RecallPatient.aspx.cs" Inherits="IPD_RecallPatient" %>

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

        var rtvalue = window.open("RecallPatientPopup.aspx", "sss", "Width:800px; Height:550px; dialogLeft:250px;");
        //document.getElementById("ctl00_ContentPlaceHolder1_txtreg").value = rtvalue.NameValue;

    }

    function Message() {
        //alert("Recalled Successfully ..");
        alert("Wait a moment Recalled Successfully ..");
        window.location = "../IPD/AdmissionPatientList.aspx";
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


 <asp:UpdatePanel ID="UpdatePanel1" runat="server">
    <ContentTemplate>
     <div class="pageheader">
         <asp:Label ID="Label1" runat="server">Recall Patient</asp:Label>
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
           <td>  
               <asp:TextBox ID="txtreg" CssClass="textbox-medium1"  runat="server" 
                   Width="150px" Enabled="False"></asp:TextBox></td>
           <td> <asp:Button ID="Button3" runat="server" Text="Search"  CssClass="submit-button"  OnClientClick="ShowDialog()"/></td>
        
              <td>  </td>
                 <td>  
                     </td>
                        <td>  <asp:Button ID="Button4" runat="server" Text="Get Details" CssClass="submit-generate" onclick="Button4_Click" /></td>
  
           </tr>
              <tr>
           <td colspan="6" align="center">
                <div class="listing"  style='width:100%; height:150px; overflow:auto;'>
         <asp:GridView id="GridView1"  CssClass="grid"
                 PagerStyle-CssClass="pgr"  runat="server" 
                 AutoGenerateColumns="False" AllowPaging="True" PageSize ="100" 
           Width="100%"   SelectedRowStyle-BackColor="GreenYellow" OnRowDataBound="GridView1_RowDataBound" onrowcommand="GridView1_RowCommand"
               >
                <RowStyle HorizontalAlign="Center" />
                <Columns>                   
                     <asp:TemplateField HeaderText="Ledger Id" Visible ="false">
                        <ItemTemplate>
                            <asp:Label ID="lblLedgerId" runat="server" Text='<%# Bind("LedgerId") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                   
   <asp:TemplateField HeaderText="Reg. No.">                       
                        <ItemTemplate>
                            <asp:Label ID="lblPatientReg" runat="server" Text='<%# Bind("PatientReg") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>


                    <asp:TemplateField HeaderText="Name">
                       
                        <ItemTemplate>
                            <asp:Label ID="lblpatient_name" runat="server" Text='<%# Bind("patient_name") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>

                  
                    
                    <asp:TemplateField HeaderText="Address">
                        <ItemTemplate>                        
                            <asp:Label ID="lblvill_city" runat="server" Text='<%# Bind("vill_city") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>


                      <asp:TemplateField HeaderText="Adm. Date">
                       
                        <ItemTemplate>
                            <asp:Label ID="lbladate" runat="server" Text='<%# Bind("adate") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>

                  
                    
                    <asp:TemplateField HeaderText="Under Doctor">
                        <ItemTemplate>                        
                            <asp:Label ID="lbldoc_name" runat="server" Text='<%# Bind("doc_name") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    
                  
                    <asp:CommandField SelectText="Recall" ShowSelectButton="True" ControlStyle-CssClass="temp" HeaderText="Recall">
                        <ControlStyle CssClass="temp"></ControlStyle>
                    </asp:CommandField>
                 
                </Columns>
                <PagerStyle CssClass="pgr" />
                <EditRowStyle BackColor="#CCFF33" />
                <AlternatingRowStyle BackColor="#FFDB91" />
        </asp:GridView> 
        </div>
               </td>
           </tr>
           </table> 
                      </div>
          
    </ContentTemplate>
    
    </asp:UpdatePanel>

</asp:Content>

