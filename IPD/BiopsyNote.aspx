<%@ Page Title="" Language="C#" MasterPageFile="~/MasterAll/MasterPageAll.master" AutoEventWireup="true" CodeFile="BiopsyNote.aspx.cs" Inherits="IPD_BiopsyNote" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
 
     <script type="text/javascript" language="javascript">
        function ShowDialog() {
            var rtvalue = window.open("RegistrationPopup.aspx", "sss", "Width:800px; Height:550px; dialogLeft:250px;");
            document.getElementById("ctl00_ContentPlaceHolder1_TextBox1").value = rtvalue.NameValue;

        }

        function Calling() {
            var date = new Date();
            $('.DatepickerCall1').datepicker({ dateFormat: 'dd/mm/yy' });

            $("input[id$='Tab2']").click(function () {


                if ($("input[id$='TextBox1']").val() == '') {
                    alert('Please Enter Registration No !');
                    $("input[id$='TextBox1']").focus();
                    $("input[id$='TextBox1']").addClass('textboxerr');
                    return false;
                }
                else {
                    $("input[id$='TextBox1']").removeClass('textboxerr');
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


 
  <asp:UpdateProgress ID="UpdateProgress1" runat="server">
    <ProgressTemplate>
    
       <div id="aaa" class="progressBackgroundFilter">          </div>
        <div id="bbb" class="processMessage"><img alt="Loading" src="../images/pwait.gif"/>
      
              </div>
    </ProgressTemplate>
    </asp:UpdateProgress>


    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
    <ContentTemplate>
    <div class="pageheader">
                <asp:Label ID="Label1" runat="server"> Biopsy Note </asp:Label>
            </div>

            
    <table width="290px" cellpadding="0" cellspacing="0">
         <tr>
            <td>
                <asp:Button Text="Entry" BorderStyle="None" ID="Tab1" Width="145px"  Height="40px" 
                    CssClass="Initial" runat="server" onclick="Tab1_Click"
                     /></td>
                     <td>
                <asp:Button Text="List" BorderStyle="None" ID="Tab2" CssClass="Initial"  Width="145px"  Height="40px" 
                    runat="server" onclick="Tab2_Click"
                    /></td>
           
                     </tr>
                     </table>


            <div class="formbox">

                  <asp:MultiView ID="MainView" runat="server">
                    <asp:View ID="View1" runat="server">
                <div class="form-sec">
                    <div class="error">
                        <strong>
                        <asp:Label ID="lblError" runat="server" Font-Bold="True" CssClass="fadeout"></asp:Label>
                        </strong>
                        <div class="clear">
                        </div>
                    </div>
                    <asp:HiddenField ID="HiddenField1" runat="server" Value="0" /><asp:HiddenField ID="HiddenField2" runat="server" Value="0" />
     
                      <div class="form-sec-row">
                        <label>
                        <strong>
                       Registration No :</strong></label>
                          <asp:TextBox ID="TextBox1" runat="server" CssClass="textbox-medium1" 
                              Enabled="true"></asp:TextBox>
                                <asp:Button ID="Button3" runat="server" Text="Quick Search"  Height="28px" CssClass="submit-buttonCheck" OnClientClick="ShowDialog()"/>
                              <asp:Button ID="Button4" runat="server" Text="Fetch"   Height="28px"
                               CssClass="submit-button" onclick="Button4_Click"/>
                        <div class="clear">
                        </div>
                    </div>

                     <div class="form-sec-row">
                        <label>
                        <strong>
                       Requisition No :</strong></label>
                          <asp:TextBox ID="TextBox2" runat="server" CssClass="textbox-medium1" 
                                Enabled="False"></asp:TextBox>
                        <div class="clear">
                        </div>
                    </div>
                        <div class="form-sec-row">
                        <label>
                        <strong>
                       Patient's Name :</strong></label>
                          <asp:TextBox ID="TextBox3" runat="server" CssClass="textbox-medium1" 
                                Enabled="False"></asp:TextBox>
                        <div class="clear">
                        </div>
                    </div>

                        <div class="form-sec-row">
                        <label>
                        <strong>
                       Current Bed No :</strong></label>
                          <asp:TextBox ID="TextBox4" runat="server" CssClass="textbox-medium1" 
                                Enabled="False"></asp:TextBox>
                        <div class="clear">
                        </div>
                    </div>

                      <div class="form-sec-row">
                        <label>
                        <strong>
                       Operation Name :</strong></label>
                          <asp:TextBox ID="TextBox5" runat="server" CssClass="textbox-medium1" 
                                Enabled="False"></asp:TextBox>
                        <div class="clear">
                        </div>
                    </div>

                        <div class="form-sec-row">
                        <label>
                        <strong>
                       Admission Date :</strong></label>
                          <asp:TextBox ID="TextBox6" runat="server" CssClass="textbox-medium1" 
                                Enabled="False"></asp:TextBox>
                        <div class="clear">
                        </div>
                    </div>
  
                   <div class="form-sec-row">
                        <div class="clear">
                        </div>
                    </div>
 
 <center>
 
   <table border="1" cellpadding="0" cellspacing="0" width="80%">
          
       <tr style='background-color:#FF9300;'>
          

                <th>                           
            <strong> Date of Collection</strong>
                        
</th>
 <th>  <strong>Type of Issue</strong> 
            
</th>
 <th>
                 <strong>Exam Required</strong> 
</th>

 <th>
                  <strong>Remarks / Status</strong> 
</th>
   </tr>
  <tr>
  
  <td align="center">
       <asp:TextBox ID="TextBox11" runat="server" CssClass="DatepickerCall1" Width="90px"></asp:TextBox>
   </td>
 <td align="center">
   <asp:TextBox ID="TextBox12" runat="server"  Width="150px"></asp:TextBox>
           <cc1:AutoCompleteExtender ServiceMethod="SearchCustomers"   MinimumPrefixLength="1"    CompletionInterval="100" EnableCaching="false" 
                   CompletionSetCount="10" TargetControlID="TextBox12"  ID="AutoCompleteExtender1" runat="server" FirstRowSelected = "false" ></cc1:AutoCompleteExtender>
   </td>
  <td align="center">
         <asp:TextBox ID="TextBox13" runat="server"   Width="150px"></asp:TextBox>
                 <cc1:AutoCompleteExtender ServiceMethod="SearchCustomers1"   MinimumPrefixLength="1"    CompletionInterval="100" EnableCaching="false" 
                   CompletionSetCount="10" TargetControlID="TextBox13"  ID="AutoCompleteExtender2" runat="server" FirstRowSelected = "false" ></cc1:AutoCompleteExtender>
   </td>
 <td align="center">
          <asp:TextBox ID="TextBox14" runat="server"   Width="230px"></asp:TextBox>
   </td>
 </tr>
 <tr>
  
  <td align="center">
       <asp:TextBox ID="TextBox15" runat="server" CssClass="DatepickerCall1" Width="90px"></asp:TextBox>
   </td>
 <td align="center">
   <asp:TextBox ID="TextBox16" runat="server"   Width="150px"></asp:TextBox>
   </td>
  <td align="center">
         <asp:TextBox ID="TextBox17" runat="server"  Width="150px"></asp:TextBox>
   </td>
 <td align="center">
          <asp:TextBox ID="TextBox18" runat="server"   Width="230px"></asp:TextBox>
   </td>
 </tr>

 <tr>
  
  <td align="center">
       <asp:TextBox ID="TextBox19" runat="server" CssClass="DatepickerCall1" Width="90px"></asp:TextBox>
   </td>
 <td align="center">
   <asp:TextBox ID="TextBox20" runat="server"  Width="150px"></asp:TextBox>
   </td>
  <td align="center">
         <asp:TextBox ID="TextBox21" runat="server"  Width="150px"></asp:TextBox>
   </td>
 <td align="center">
          <asp:TextBox ID="TextBox22" runat="server"  Width="230px"></asp:TextBox>
   </td>
 </tr>


 <tr>
  
  <td align="center">
       <asp:TextBox ID="TextBox23" runat="server" CssClass="DatepickerCall1" Width="90px"></asp:TextBox>
   </td>
 <td align="center">
   <asp:TextBox ID="TextBox24" runat="server"   Width="150px"></asp:TextBox>
   </td>
  <td align="center">
         <asp:TextBox ID="TextBox25" runat="server" Width="150px"></asp:TextBox>
   </td>
 <td align="center">
          <asp:TextBox ID="TextBox26" runat="server"  Width="230px"></asp:TextBox>
   </td>
 </tr>


 <tr>
  
  <td align="center">
       <asp:TextBox ID="TextBox27" runat="server" CssClass="DatepickerCall1" Width="90px"></asp:TextBox>
   </td>
 <td align="center">
   <asp:TextBox ID="TextBox28" runat="server" Width="150px"></asp:TextBox>
   </td>
  <td align="center">
         <asp:TextBox ID="TextBox29" runat="server" Width="150px"></asp:TextBox>
   </td>
 <td align="center">
          <asp:TextBox ID="TextBox30" runat="server"   Width="230px" ></asp:TextBox>
   </td>
 </tr>


 <tr>
  
  <td align="center">
       <asp:TextBox ID="TextBox31" runat="server" CssClass="DatepickerCall1" Width="90px"></asp:TextBox>
   </td>
 <td align="center">
   <asp:TextBox ID="TextBox32" runat="server"  Width="150px"></asp:TextBox>
   </td>
  <td align="center">
         <asp:TextBox ID="TextBox33" runat="server"   Width="150px"></asp:TextBox>
   </td>
 <td align="center">
          <asp:TextBox ID="TextBox34" runat="server" Width="230px"></asp:TextBox>
   </td>
 </tr>


 <tr>
  
  <td align="center">
       <asp:TextBox ID="TextBox35" runat="server" CssClass="DatepickerCall1" Width="90px"></asp:TextBox>
   </td>
 <td align="center">
   <asp:TextBox ID="TextBox36" runat="server"  Width="150px"></asp:TextBox>
   </td>
  <td align="center">
         <asp:TextBox ID="TextBox37" runat="server"   Width="150px"></asp:TextBox>
   </td>
 <td align="center">
          <asp:TextBox ID="TextBox38" runat="server"  Width="230px" ></asp:TextBox>
   </td>
 </tr>
     </table>
     </center>
                    <div class="form-sec-row">
                        &nbsp;&nbsp;&nbsp;<label><strong>&nbsp;</strong></label>
                        <asp:Button ID="Button1" runat="server" CssClass="submit-button"   Height="30px"
                            Text="Submit" onclick="Button1_Click" />
                        <asp:Button ID="Button2" runat="server" CssClass="submit-button"  Height="30px"
                            Text="Cancel" onclick="Button2_Click"  />
                        <div class="clear">
                        </div>
                    </div>
                    <div class="clear">
                    </div>
                    
                         </div>

</asp:View>
 <asp:View ID="View2" runat="server">

 <div class="listing">
                <asp:GridView id="GridView2"  Width="100%" CssClass="grid" PagerStyle-CssClass="pgr"
                 DataKeyNames ="OperationReqID" runat="server" 
                 AutoGenerateColumns="False" AllowPaging="True" 
                 SelectedRowStyle-BackColor="GreenYellow" 
                            onpageindexchanging="GridView2_PageIndexChanging" 
                            onrowcommand="GridView2_RowCommand"  >
                    <Columns>
                      <asp:CommandField SelectText="Select" ShowSelectButton="True" />
                  
                        <asp:TemplateField HeaderText="Requisition No" >
                            <ItemTemplate>
                                <asp:Label ID="lblid" runat="server" Text='<%# Bind("OperationReqID") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
             
                        <asp:TemplateField HeaderText="Registration No" Visible="false">
                            <ItemTemplate>
                                <asp:Label ID="lblRegno" runat="server" Text='<%# Bind("PatientRegId") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>


                         <asp:TemplateField HeaderText="Operation Type">
                            <ItemTemplate>
                                <asp:Label ID="lbllname" runat="server" Text='<%# Bind("OperationTypeName") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>

                         <asp:TemplateField HeaderText="Operation Name">
                            <ItemTemplate>
                                <asp:Label ID="lbladate" runat="server" Text='<%# Bind("OperationName") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>

                     </Columns>
                  <PagerStyle CssClass="pgr" />
                    <EditRowStyle BackColor="#CCFF33" />
                    <AlternatingRowStyle BackColor="#FFDB91" />
                </asp:GridView>
            </div>

            <br /><br />
                    <div class="listing">
                <asp:GridView id="GridView1"  Width="100%" CssClass="grid" PagerStyle-CssClass="pgr"
                 DataKeyNames ="RowId" runat="server" 
                 AutoGenerateColumns="False" AllowPaging="True" 
                 SelectedRowStyle-BackColor="GreenYellow" 
                    onpageindexchanging="GridView1_PageIndexChanging" 
                    onrowcommand="GridView1_RowCommand" >
                    <Columns>
                            <%--  <asp:CommandField SelectText="Select" ShowSelectButton="True" />--%>
                        <asp:TemplateField HeaderText="ID" Visible="false">
                            <ItemTemplate>
                                <asp:Label ID="lblid" runat="server" Text='<%# Bind("RowId") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
             
                        <asp:TemplateField HeaderText="Registration No">
                            <ItemTemplate>
                                <asp:Label ID="lblRegno" runat="server" Text='<%# Bind("PatientReg") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>


                         <asp:TemplateField HeaderText="Patient's Name">
                            <ItemTemplate>
                                <asp:Label ID="lbllname" runat="server" Text='<%# Bind("patient_name") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>

                         <asp:TemplateField HeaderText="Admission Date">
                            <ItemTemplate>
                                <asp:Label ID="lbladate" runat="server" Text='<%# Bind("adate") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="Bed No">
                            <ItemTemplate>
                                <asp:Label ID="lblbedno" runat="server" Text='<%# Bind("BedNoText") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>

                         <asp:TemplateField HeaderText="Date of Collection" >
                            <ItemTemplate>
                                <asp:Label ID="lblserivcecat" runat="server" Text='<%# Bind("coldate") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                             <asp:TemplateField HeaderText="Type Of Tissue" >
                            <ItemTemplate>
                                <asp:Label ID="lblservice" runat="server" Text='<%# Bind("TypeOfTissue") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>   

                             <asp:TemplateField HeaderText="Exam Required" >
                            <ItemTemplate>
                                <asp:Label ID="lbllblissuedate" runat="server" Text='<%# Bind("ExamRequired") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>

                         <asp:TemplateField HeaderText="Remarks" >
                            <ItemTemplate>
                                <asp:Label ID="lblquantity" runat="server" Text='<%# Bind("Remarks") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                            
       <%--       <asp:CommandField SelectText="Edit" ShowSelectButton="True" ControlStyle-CssClass="temp" HeaderText="Edit" SelectImageUrl="~/Images/edit.png" ButtonType="Image">
                            <ControlStyle CssClass="temp"></ControlStyle>
                        </asp:CommandField>--%>
       <%--      <asp:CommandField ShowDeleteButton="True" ControlStyle-CssClass="temp" HeaderText="Delete" DeleteImageUrl="~/Images/delete.png" ButtonType="Image">
                            <ControlStyle CssClass="temp"></ControlStyle>
                        </asp:CommandField>--%>
                    </Columns>
                  <PagerStyle CssClass="pgr" />
                    <EditRowStyle BackColor="#CCFF33" />
                    <AlternatingRowStyle BackColor="#FFDB91" />
                </asp:GridView>
            </div>
            </asp:View>
            </asp:MultiView>
         
                </div>
    </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>

