<%@ Page Title="" Language="C#" MasterPageFile="~/MasterAll/MasterPageAll.master" AutoEventWireup="true" CodeFile="AttendenceFees.aspx.cs" Inherits="IPD_AttendenceFees" %>

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
        $("input[id$='TextBox7']").datepicker({ dateFormat: 'dd/mm/yy' });

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



        $("input[id$='Button1']").click(function () {


            if ($("input[id$='TextBox7']").val() == '') {
                alert('Please Enter Date !');
                $("input[id$='TextBox7']").focus();
                $("input[id$='TextBox7']").addClass('textboxerr');
                return false;
            }
            else {
                $("input[id$='TextBox7']").removeClass('textboxerr');
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

    
<%-- 
  <asp:UpdateProgress ID="UpdateProgress1" runat="server">
    <ProgressTemplate>
    
       <div id="aaa" class="progressBackgroundFilter">          </div>
        <div id="bbb" class="processMessage"><img alt="Loading" src="../images/pwait.gif"/>
      
              </div>
    </ProgressTemplate>
    </asp:UpdateProgress>--%>

    <asp:UpdatePanel ID="UpdatePanel1" runat="server">

    <ContentTemplate>
     <div class="pageheader">
                <asp:Label ID="Label1" runat="server">Attendant Fees</asp:Label>
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
                                <asp:Button ID="Button3" runat="server" Text="Quick Search"  height="28px" CssClass="submit-buttonCheck" OnClientClick="ShowDialog()"/>
                              <asp:Button ID="Button4" runat="server" Text="Fetch"   height="28px"
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
                       Operation Name :</strong></label>
                          <asp:TextBox ID="TextBox4" runat="server" CssClass="textbox-medium1" 
                                Enabled="False"></asp:TextBox>
                        <div class="clear">
                        </div>
                    </div>
                        <div class="form-sec-row">
                        <label>
                        <strong>
                       Bed No :</strong></label>
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
                        <label>
                        <strong> 
                       Date :</strong></label>
                        <asp:TextBox ID="TextBox7" runat="server" CssClass="textbox-medium1"></asp:TextBox>
                        <div class="clear">
                        </div>
                    </div>
             
         <table border="1" cellpadding="0" cellspacing="0" width="950px">
         <tr>
         <td style='width:120px;'>
           <table width="100%">
           <tr>
                      <td style='width:120px;background-color:#FF9300;'>
                   <strong>
                       <asp:Label ID="Label2" runat="server" Height="22px" Text=""></asp:Label></strong> 
            
</td> 
</tr>

        <tr>
                      <td  style='width:120px;background-color:#FF9300;'> 
                   <strong>
                       <asp:Label ID="Label3" runat="server" Height="25px" Text="Name :"></asp:Label></strong> 
            
</td> 
</tr>
<tr>
     <td   style='width:120px;background-color:#FF9300;'> 
            <strong>
                       <asp:Label ID="Label4" runat="server" Height="25px" Text="Actual Paid :"></asp:Label></strong> 
            
</td> 
</tr>
<tr>
          <td style='width:120px;background-color:#FF9300;'> 
                   <strong>
                       <asp:Label ID="Label5" runat="server" Height="25px" Text="Bill Reflection :"></asp:Label></strong> 
            
</td> 
</tr>
</table>
         </td>
         

         <td  style='width:800px;'>
                   <div style="width:800px;overflow:auto;">
         <table border="1" cellpadding="0" cellspacing="0" width="800px">
          
       <tr style='background-color:#FF9300;'>
              <td align="center"> 
             <label><strong>Surgeon</strong></label> 
     
        </td> 

                <td > 
             <label><strong>Doc-1</strong></label>
      
</td>
                <td>
             <label class="lname"><strong>Doc-2</strong></label>
                  
</td>
 <td >
             <label class="lname"><strong>Doc-3</strong></label> 
        
            
</td>
 <td>
             <label class="lname"><strong>Anesthesis-1</strong></label> 
    
            
</td>
 <td>
             <label class="lname"><strong>Anesthesis-2</strong></label> 
        
            
</td>
 <td>
             <label class="lname"><strong>Assistant-1</strong></label> 
    
            
</td>
        <td align="center"> 
             <label class="lname"><strong>Assistant-2</strong></label> 
        
            
</td>
<td>
             <label class="lname"><strong>Assistant-3</strong></label> 
           
</td>
<td>
             <label class="lname"><strong>Comment</strong></label> 
   
            
</td>
                      
            </tr>
                <tr>

         
                <td align="center">  
             <asp:DropDownList ID="DropDownList1" runat="server"  Width="120px" >
                            </asp:DropDownList>
       
            
</td> 

                <td align="center"> 
             <asp:DropDownList ID="DropDownList2" runat="server"  Width="120px" >
                            </asp:DropDownList>
            
</td>
                <td align="center"> 
             <asp:DropDownList ID="DropDownList3" runat="server"  Width="120px" >
                            </asp:DropDownList>
                           
</td>
 <td align="center"> 
                       <asp:DropDownList ID="DropDownList4" runat="server"    Width="120px"  >
                            </asp:DropDownList>
      
            
</td>
 <td align="center"> 
             <asp:DropDownList ID="DropDownList5" runat="server"   CssClass="textbox-medium1"  Width="120px">
                            </asp:DropDownList>
        
            
</td>
 <td align="center"> 
             <asp:DropDownList ID="DropDownList6" runat="server"  Width="120px">
                            </asp:DropDownList>
         
            
</td>
 <td align="center"> 
             <asp:DropDownList ID="DropDownList7" runat="server"     Width="120px" >
                            </asp:DropDownList>
          
            
</td>
 <td align="center"> 
             <asp:DropDownList ID="DropDownList8" runat="server"      Width="120px">
                            </asp:DropDownList>
        
            
</td>
 <td align="center"> 
             <asp:DropDownList ID="DropDownList9" runat="server"  Width="120px"   >
                            </asp:DropDownList>
          
            
</td>
 <td align="center"> 
                        <asp:TextBox ID="TextBox8" runat="server"    
                            Width="120px" ></asp:TextBox>
          
            
</td>
 </tr>

 <tr>

                 
                <td align="center"> 
                <asp:TextBox ID="TextBox9" runat="server"      Width="120px" ></asp:TextBox>
         
            
</td> 

                <td align="center"> 
             <asp:TextBox ID="TextBox10" runat="server"       Width="120px" ></asp:TextBox>
           
</td>
                <td align="center"> 
           <asp:TextBox ID="TextBox11" runat="server"       Width="120px" ></asp:TextBox>
                     
</td>
 <td align="center"> 
                      <asp:TextBox ID="TextBox12" runat="server"      Width="120px" ></asp:TextBox>
         
            
</td>
 <td align="center"> 
            <asp:TextBox ID="TextBox13" runat="server"       Width="120px" ></asp:TextBox>
         
            
</td>
 <td align="center"> 
            <asp:TextBox ID="TextBox14" runat="server"     Width="120px" ></asp:TextBox>
 
            
</td>
 <td align="center"> 
           <asp:TextBox ID="TextBox15" runat="server"     Width="120px" ></asp:TextBox>
         
            
</td>
 <td align="center"> 
           <asp:TextBox ID="TextBox16" runat="server"      Width="120px" ></asp:TextBox>
       
            
</td>
 <td align="center"> 
           <asp:TextBox ID="TextBox17" runat="server"    Width="120px" ></asp:TextBox>
        
            
</td>
 <td align="center"> 
                       <asp:TextBox ID="TextBox18" runat="server"    
                            Width="120px"></asp:TextBox>
 
            
</td>
 </tr>

 <tr>

            
                <td align="center"> 
                <asp:TextBox ID="TextBox19" runat="server"     Width="120px" ></asp:TextBox>
   
            
</td> 

                <td align="center"> 
             <asp:TextBox ID="TextBox20" runat="server"     Width="120px" ></asp:TextBox>
           
</td>
                <td align="center"> 
           <asp:TextBox ID="TextBox21" runat="server"      Width="120px" ></asp:TextBox>
                 
</td>
 <td align="center"> 
                      <asp:TextBox ID="TextBox22" runat="server"     Width="120px" ></asp:TextBox>
         
            
</td>
 <td align="center"> 
            <asp:TextBox ID="TextBox23" runat="server"     Width="120px" ></asp:TextBox>
     
            
</td>
 <td align="center"> 
            <asp:TextBox ID="TextBox24" runat="server"     Width="120px" ></asp:TextBox>
           
            
</td>
 <td align="center"> 
           <asp:TextBox ID="TextBox25" runat="server"    Width="120px" ></asp:TextBox>
         
            
</td>
 <td align="center"> 
           <asp:TextBox ID="TextBox26" runat="server"    Width="120px" ></asp:TextBox>
        
            
</td>
 <td align="center"> 
           <asp:TextBox ID="TextBox27" runat="server"     Width="120px" ></asp:TextBox>
       
            
</td>
 <td align="center"> 
                       <asp:TextBox ID="TextBox28" runat="server"   
                            Width="120px" ></asp:TextBox>
     
            
</td>
 </tr>

   </table> 
   </div>
         </td>
   
         </tr>
    </table>
  
                    <div class="form-sec-row">
                        <div class="clear">
                        </div>
                    </div>
                    <div class="form-sec-row">
                        &nbsp;&nbsp;&nbsp;<label><strong>&nbsp;</strong></label>
                        <asp:Button ID="Button1" runat="server" CssClass="submit-button"   height="28px"
                            Text="Submit" onclick="Button1_Click" />
                        <asp:Button ID="Button2" runat="server" CssClass="submit-button"  height="28px"
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
        
                       </asp:View>
                       </asp:MultiView>                
              
                </div>
    </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>

