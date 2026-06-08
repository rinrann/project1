<%@ Page Title="" Language="C#" MasterPageFile="~/MasterAll/MasterPageAll.master" AutoEventWireup="true" CodeFile="BirthCertificate.aspx.cs" Inherits="IPD_BirthCertificate" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
<script type="text/javascript" language="javascript">
    function printDiv(divID) {
        var divElements = document.getElementById("ctl00_ContentPlaceHolder1_mydiv").innerHTML;
        var oldPage = document.body.innerHTML;
        document.body.innerHTML = "<html><head><title></title></head><body>" + divElements + "</body>";
        window.print();
        document.body.innerHTML = oldPage;
    }

    function ShowDialog() {

        var rtvalue = window.open("Deliverpopup.aspx", "sss", "Width:800px; Height:550px; dialogLeft:250px;");
        document.getElementById("ctl00_ContentPlaceHolder1_txtreg").value = rtvalue.NameValue;

    }

    function ShowDialogDuplicate() {
        var rtvalue = window.open("IpdpatientPopup.aspx", "sss", "Width:800px; Height:550px; dialogLeft:250px;");
        document.getElementById("ctl00_ContentPlaceHolder1_TextBox1").value = rtvalue.NameValue;
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

            if ($("select[id$='DropDownList1']").val() == '0') {
                alert('Please Select Language!');
                $("select[id$='DropDownList1']").addClass('textboxerr');
                $("select[id$='DropDownList1']").focus();
                return false;
            }
            else {
                $("select[id$='DropDownList1']").removeClass('textboxerr');
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
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    


 
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
         <asp:Label ID="Label1" runat="server">Birth Certificate</asp:Label>
     </div>
     <div class="formbox">
        <div class="form-sec">
            <div class="error">
                <strong><asp:Label ID="lblError" runat="server" Font-Bold="True"></asp:Label></strong>
                <div class="clear"></div>
            </div></div>





           

                 <table width="100%">
             <tr>
           <td colspan="6" align="center">
               <asp:RadioButtonList ID="RadioButtonList2" runat="server"  Width="250px"
                   RepeatDirection="Horizontal" AutoPostBack="True" 
                   onselectedindexchanged="RadioButtonList2_SelectedIndexChanged">
                   <asp:ListItem>Current Report</asp:ListItem>
                   <asp:ListItem>Duplicate Report</asp:ListItem>
               </asp:RadioButtonList>
               </td>
           </tr>
           </table>

         <asp:Panel ID="Panel1" runat="server">
         <table width="100%">

           <tr>
   
           <td>  <label><strong>Registration No :</strong></label> </td>
           <td>  
               <asp:TextBox ID="txtreg" CssClass="textbox-medium1"  runat="server" 
                   Width="150px" ></asp:TextBox></td>
           <td> <asp:Button ID="Button3" runat="server" Text="Search"  CssClass="submit-button"  OnClientClick="ShowDialog()"/></td>
        
              <td>  <label><strong>Select Language :</strong></label> </td>
                 <td>  
                     <asp:DropDownList ID="DropDownList1" runat="server" CssClass="textbox-medium1" 
                         Width="150px" >
                         <asp:ListItem Value="0">-- Select --</asp:ListItem>
                         <asp:ListItem Value="1">Bengali</asp:ListItem>
                         <asp:ListItem Value="2">English</asp:ListItem>
                         <asp:ListItem Value="3">Hindi</asp:ListItem>
                     </asp:DropDownList> </td>
                        <td>  <asp:Button ID="Button4" runat="server" Text="Generate Report" 
                                CssClass="submit-generate" onclick="Button4_Click1" /></td>
  
           </tr>
           </table>
         </asp:Panel>
         <asp:Panel ID="Panel2" runat="server">
         <table width="100%">

           <tr>
   
           <td>  <label><strong>Registration No :</strong></label> </td>
           <td>  
               <asp:TextBox ID="TextBox1" CssClass="textbox-medium1"  runat="server" 
                   Width="150px" Enabled="False"></asp:TextBox></td>
           <td> <asp:Button ID="Button1" runat="server" Text="Search"  CssClass="submit-button"  OnClientClick="ShowDialogDuplicate()"/></td>
        
              <td>  <label><strong>Select Language :</strong></label> </td>
                 <td>  
                     <asp:DropDownList ID="DropDownList2" runat="server" CssClass="textbox-medium1" 
                         Width="150px" >
                         <asp:ListItem Value="0">-- Select --</asp:ListItem>
                         <asp:ListItem Value="1">Bengali</asp:ListItem>
                         <asp:ListItem Value="2">English</asp:ListItem>
                         <asp:ListItem Value="3">Hindi</asp:ListItem>
                     </asp:DropDownList> </td>
                        <td>  
                            <asp:Button ID="Button2" runat="server" Text="Generate Report" 
                                CssClass="submit-generate" onclick="Button2_Click"/></td>
  
           </tr>
             <tr>
                 <td colspan="4">
                     <asp:Panel ID="pnl2" runat="server" ScrollBars="Both" Height="200px">
                     
                <asp:DataGrid ID="GridView1" ShowHeader="true" runat="server" BackColor="White" DataKeyField="Doc_id"
                    AutoGenerateColumns="False" BorderColor="Silver" Width="420">
                    <AlternatingItemStyle CssClass="odd1"></AlternatingItemStyle>
                    <ItemStyle CssClass="even1"></ItemStyle>
                    <HeaderStyle  CssClass="titlebar"></HeaderStyle>
                    <Columns>
                        <asp:TemplateColumn HeaderText="Code" Visible="false">
                            <HeaderStyle Width="80px"></HeaderStyle>
                            <ItemStyle Width="80px"></ItemStyle>
                            <ItemTemplate>
                                <asp:Label ID="lblReg" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.Doc_id") %>'>
                                </asp:Label>
                            </ItemTemplate>
                        </asp:TemplateColumn>
                        <asp:TemplateColumn HeaderText="Doctor Name">
                            <HeaderStyle Width="300px"></HeaderStyle>
                            <ItemStyle Width="300px"></ItemStyle>
                            <ItemTemplate>
                                <asp:Label ID="lblName" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.doc_Name") %>'>
                                </asp:Label>
                            </ItemTemplate>
                        </asp:TemplateColumn>
                         <asp:TemplateColumn HeaderText="Qualification">
                            <HeaderStyle Width="300px"></HeaderStyle>
                            <ItemStyle Width="300px"></ItemStyle>
                            <ItemTemplate>
                                <asp:Label ID="lblQuali" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.Qualification") %>'>
                                </asp:Label>
                            </ItemTemplate>
                        </asp:TemplateColumn>
                        <asp:TemplateColumn HeaderText="Select">
                            <HeaderStyle Width="40px"></HeaderStyle>
                            <ItemStyle HorizontalAlign="Center" Width="40px"></ItemStyle>
                            <ItemTemplate>
                                <asp:CheckBox ID="chkselect" runat="server" Checked="false" AutoPostBack="false"></asp:CheckBox>
                                <%--Text='<%# DataBinder.Eval(Container, "DataItem.DIVN_Id") %>'--%>
                            </ItemTemplate>
                        </asp:TemplateColumn>
                    </Columns>
                </asp:DataGrid>
                         </asp:Panel>
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
                    <tr>
                        <td align="center">
                     
                      <input type="button" id="btnBack" value="Back" runat="server" style="width:70px; font-size:x-small" onclick="window.history.back()" />
                       <%--  <input type="button" id="cmdPrint"  runat="server" value="Print" style="width:70px; font-size:x-small" onclick="javascript:printDiv('mydiv')"/>--%>

                             <asp:Button ID="cmdPrint" runat="server"  style="width:70px; font-size:x-small"  Text="Print"  OnClientClick="javascript:printDiv('mydiv')"/>

                            <asp:Button ID="btnPDF" runat="server"  style="width:70px; font-size:x-small"  
                                Text="PDF" onclick="btnPDF_Click"/>
                         </td>
                        
                    </tr>
            </table>
</ContentTemplate>
 <Triggers>
  <asp:PostBackTrigger ControlID="btnPDF" />
  </Triggers>
<%--
  <Triggers >
  <asp:AsyncPostBackTrigger ControlID="cmdPrint" />
  </Triggers>--%>
    </asp:UpdatePanel>

</asp:Content>

