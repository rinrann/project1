<%@ Page Title="" Language="C#" MasterPageFile="~/MasterAll/MasterPageAll.master" AutoEventWireup="true" CodeFile="MedicineAdd.aspx.cs" Inherits="DayCare_MedicineAdd" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
   <script type="text/javascript" language="javascript">
       function ShowDialog() {
           var rtvalue = window.open("RegistrationPopupMedicine.aspx", "sss", "Width:800px; Height:550px; dialogLeft:250px;");
           //document.getElementById("ctl00_ContentPlaceHolder1_TextBox23").value = rtvalue.NameValue;
           //var a = rtvalue.ProfessionValue.split('#');
           //document.getElementById("ctl00_ContentPlaceHolder1_TextBox24").value = a[0];
       }

       function Calling() {
           var date = new Date();
           $('.DatepickerCall').datepicker({ dateFormat: 'dd/mm/yy' });

           $("input[id$='Tab2']").click(function () {


               if ($("input[id$='TextBox23']").val() == '') {
                   alert('Please Enter Registration No !');
                   $("input[id$='TextBox23']").focus();
                   $("input[id$='TextBox23']").addClass('textboxerr');
                   return false;
               }
               else {
                   $("input[id$='TextBox23']").removeClass('textboxerr');
               }
               
           });

           $("input[id$='Button1']").click(function () {
               if (confirm("Do you want to save data?")) {
                   //confirm_value.value = "Yes";
                   return true;
               } else {
                   // confirm_value.value = "No";
                   return false;
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

    <asp:UpdatePanel ID="UpdatePanel1" runat="server" style="width:110%">
    <ContentTemplate>
      <div class="pageheader">
         <asp:Label ID="Label1" runat="server">Add Medicine</asp:Label>
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
     <div class="formbox" style="width:100%;">
      <asp:MultiView ID="MainView" runat="server">
                    <asp:View ID="View1" runat="server">
     <div class="form-sec">
        
            <div class="error">
                <strong><asp:Label ID="lblError" runat="server" Font-Bold="True"></asp:Label>
                </strong>
                <div class="clear"></div>
            </div>
            <asp:HiddenField ID="HiddenField1" runat="server" />
			           
                       
             <div class="form-sec-row">
                        <label>
                        <strong>
                       Registration No :</strong></label>
                          <asp:TextBox ID="TextBox23" runat="server" CssClass="textbox-medium1" 
                              Enabled="False"></asp:TextBox>
                                <asp:Button ID="Button3" runat="server" Text="Quick Search" height="28px" CssClass="submit-buttonCheck" OnClientClick="ShowDialog()"/>
                              <asp:Button ID="Button2" runat="server" Text="Fetch" height="28px"
                            CssClass="submit-button" onclick="Button2_Click"/>
                        <div class="clear">
                        </div>
                    </div>
                        <div class="form-sec-row">
                        <label>
                        <strong>
                       Patient's Name :</strong></label>
                          <asp:TextBox ID="TextBox24" runat="server" CssClass="textbox-medium1" 
                                Enabled="False"></asp:TextBox>
                        <div class="clear">
                        </div>
                    </div>
                                         
     </div>
    
     
       <center>
       <div style='width:100%; height:auto; overflow:;'>
    <table border="1" cellpadding="0" cellspacing="0" width="100%">
          
       <tr style='background-color:#FF9300;'>
       
 <td align="center">
                  
             <label class="lname"><strong>Issue Date</strong></label> 
           
            
</td>

                <td align="center">
                
             <label class="lname"><strong>Medicine Group</strong></label> 
            
        </td> 

                <td align="center">
                  
             <label class="lname"><strong>Sub Group</strong></label>
           
</td>
                <td align="center">
                  
             <label class="lname"><strong>Medicine</strong></label>
           
</td>
                <td align="center">
                           
             <label class="lname"><strong> Advice By </strong></label>
                             
</td>


 <td align="center">
                  
             <label class="lname"><strong>Batch No</strong></label> 
           
            
</td>

 <td align="center">                  
             <label class="lname"><strong>Expiry Date</strong></label> 
            
</td> 

 <td align="center">
                  
             <label class="lname"><strong>Bill Qty</strong></label> 
           
            
</td>
        
                      
            </tr>
                <tr>

                 <td align="center">
                  
                       <asp:TextBox ID="TextBox1" runat="server" CssClass="DatepickerCall" Width="120px"></asp:TextBox>
           
            
</td>

                <td align="center">
                  
             <asp:DropDownList ID="DropDownList1" runat="server" CssClass="textbox-medium1" 
                            Width="150px" AppendDataBoundItems="true" AutoPostBack="True" 
                            onselectedindexchanged="DropDownList1_SelectedIndexChanged">
                            </asp:DropDownList>
           
            
</td> 

       <td align="center">
                  
             <asp:DropDownList ID="ddlSubGroup1" runat="server" CssClass="textbox-medium1" 
                 Width="150px"  AutoPostBack="true" 
                 onselectedindexchanged="ddlSubGroup1_SelectedIndexChanged">
                            </asp:DropDownList>
           
</td>
                <td align="center">
                  
             <asp:DropDownList ID="DropDownList2" runat="server" CssClass="textbox-medium1" 
                        Width="150px"  AutoPostBack="true" 
                        onselectedindexchanged="DropDownList2_SelectedIndexChanged">
                            </asp:DropDownList>

    
                    
                  
           
</td>
                <td align="center">
                           
             <asp:DropDownList ID="DropDownList3" runat="server" CssClass="textbox-medium1" Width="150px" AutoPostBack="true">
                            </asp:DropDownList>
                             
</td>

 <td align="center">
                  
           <asp:DropDownList ID="ddlBatchNo1" runat="server" CssClass="textbox-medium1" 
               Width="150px" AutoPostBack="true" 
               onselectedindexchanged="ddlBatchNo1_SelectedIndexChanged">
                            </asp:DropDownList>
           
            
</td>
 <td align="center">
                  
             <asp:TextBox ID="txtExpiryDate1" runat="server" CssClass="DatepickerCall" Width="120px"></asp:TextBox>
           
            
</td> 
 <td align="center">
                 
             <asp:TextBox ID="txtBillQty1" runat="server" CssClass="textbox-medium1"  Width="60px"></asp:TextBox>
            
            
</td>
 </tr>  
<tr>
<td colspan="13" align="left" style='padding-left:250px;'>
 <asp:Button ID="Button1" runat="server" Text="Submit" 
        CssClass="submit-button" onclick="Button1_Click"/>   
    <asp:Button ID="Button4" runat="server" Text="Cancel" 
        CssClass="submit-button" onclick="Button4_Click" />
     </td>
</tr>
 
       </table>
       </div>
    </center>
     </asp:View>
         <asp:View ID="View2" runat="server">
    <div class="listing" style='width:100%; height:auto;'>
                <asp:GridView id="GridView1"  Width="100%" CssClass="grid" PagerStyle-CssClass="pgr"
                 DataKeyNames ="RowID" runat="server"  PageSize="100"
                 AutoGenerateColumns="False" AllowPaging="True" 
                 SelectedRowStyle-BackColor="GreenYellow" 
                    onpageindexchanging="GridView1_PageIndexChanging" 
                    onrowcommand="GridView1_RowCommand" OnRowDataBound="GridView1_RowDataBound" onrowdeleting="GridView1_RowDeleting" >
                    <Columns>
                        <asp:TemplateField HeaderText="ID" Visible="false">
                            <ItemTemplate>
                                <asp:Label ID="lblid" runat="server" Text='<%# Bind("RowID") %>'></asp:Label>
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
                         

                         <asp:TemplateField HeaderText="Medicine Group" >
                            <ItemTemplate>
                                <asp:Label ID="lblMedicineGroupName" runat="server" Text='<%# Bind("MedicineGroupName") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>


                            <asp:TemplateField HeaderText="MedicineSubGrId" Visible="false">
                            <ItemTemplate>
                                <asp:Label ID="lblMedicineSubGrId" runat="server" Text='<%# Bind("MedicineSubGrId") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>


                        
                             <asp:TemplateField HeaderText="Medicine Name" >
                            <ItemTemplate>
                                <asp:Label ID="lblMedicineName" runat="server" Text='<%# Bind("MedicineName") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>   

                             <asp:TemplateField HeaderText="Issue Date" >
                            <ItemTemplate>
                                <asp:Label ID="lblisdate" runat="server" Text='<%# Bind("isdate") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
 
                    <asp:TemplateField HeaderText="Adviced By" Visible="false" >
                            <ItemTemplate>
                                <asp:Label ID="lblAdviceBy" runat="server" Text='<%# Bind("AdviceBy") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>


                                <asp:TemplateField HeaderText="BillQty" >
                            <ItemTemplate>
                                <asp:Label ID="lblBillQty" runat="server" Text='<%# Bind("BillQty") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>


                                <asp:TemplateField HeaderText="Batch No">
                            <ItemTemplate>
                                <asp:Label ID="lblBatchNo" runat="server" Text='<%# Bind("BatchNo") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>

                                <asp:TemplateField HeaderText="Expiry Date" >
                            <ItemTemplate>
                                <asp:Label ID="lblExpirDate" runat="server" Text='<%# Bind("ExDate") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
              <asp:CommandField SelectText="Edit" ShowSelectButton="True" ControlStyle-CssClass="temp" HeaderText="Edit" SelectImageUrl="~/Images/edit.png" ButtonType="Image">
                            <ControlStyle CssClass="temp"></ControlStyle>
                        </asp:CommandField>

             <asp:CommandField ShowDeleteButton="True" ControlStyle-CssClass="temp" HeaderText="Delete" DeleteImageUrl="~/Images/delete.png" ButtonType="Image">
                            <ControlStyle CssClass="temp"></ControlStyle>
                        </asp:CommandField>
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

