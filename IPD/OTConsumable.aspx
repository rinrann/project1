<%@ Page Title="" Language="C#" MasterPageFile="~/MasterAll/MasterPageAll.master" AutoEventWireup="true" CodeFile="OTConsumable.aspx.cs" Inherits="IPD_OTConsumable" %>

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
                alert('Please Enter Issue Date !');
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
                <asp:Label ID="Label1" runat="server"> OT Consumable  </asp:Label>
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
                                <asp:Button ID="Button3" runat="server"   Height="28px" Text="Quick Search" CssClass="submit-buttonCheck" OnClientClick="ShowDialog()"/>
                              <asp:Button ID="Button4" runat="server" Text="Fetch"  Height="28px"
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
                                      <asp:Label ID="Label3" runat="server" ForeColor="Green" Text="(DD/MM/YYYY)"></asp:Label>
                        <div class="clear">
                        </div>
                    </div>

                       <div class="form-sec-row">
                        <label>
                        <strong>
                       Issue Date :</strong></label>
                          <asp:TextBox ID="TextBox7" runat="server" CssClass="textbox-medium1" ></asp:TextBox>
                                <asp:Label ID="Label2" runat="server" ForeColor="Green" Text="(DD/MM/YYYY)"></asp:Label>
                        <div class="clear">
                        </div>
                    </div>
  
                   <div class="form-sec-row">
                        <div class="clear">
                        </div>
                    </div>
 <center>

      <table border="1" cellpadding="0" style='background-color:#C5B9B9;' cellspacing="0" width="100%">
                        <tr>
                        <td colspan="2" align="center">
                            <div class="pageheader">
         <asp:Label ID="Label4" runat="server"> Patient Consumable </asp:Label>
     </div>
                        </td>
                        </tr>
          <tr>
           <td><strong>Template Group:</strong> 
                  <asp:DropDownList ID="ddlTempGroup" runat="server" AutoPostBack="True" 
                   Width="180px" OnSelectedIndexChanged="ddlTempGroup_SelectedIndexChanged"  >
                  </asp:DropDownList> </td>
              <td style="visibility: hidden"><strong>Template :</strong> 
                  <asp:DropDownList ID="ddlTemplate" runat="server" AutoPostBack="True" 
                      Width="180px" OnSelectedIndexChanged="ddlTemplate_SelectedIndexChanged" >
                  </asp:DropDownList> </td>
</tr>
</table>

    <table border="1" cellpadding="0" cellspacing="0" width="80%">
          
       <tr style='background-color:#FF9300;'>
                <th>
        Consumable Group
        </th> 

                <th>
    Consumable Item
</th>
                <th>
   Advice By
</th>
 <th>
                 Act Qty.
            
</th>
 <th>
     Bill Qty.
</th>
 <th>
    Remarks
</th>          
            </tr>
                <tr>
                <td>
                   
             <asp:DropDownList ID="DropDownList1" runat="server"  
                            Width="150px" AutoPostBack="True" 
                            onselectedindexchanged="DropDownList1_SelectedIndexChanged"  >
                            </asp:DropDownList>
          
            
</td> 

                <td>
                 
             <asp:DropDownList ID="DropDownList2" runat="server"  Width="150px" AppendDataBoundItems="true">
                            </asp:DropDownList>
          
</td>
                <td>
             <asp:DropDownList ID="DropDownList3" runat="server"  Width="200px" AppendDataBoundItems="true">
                            </asp:DropDownList>
                             
</td>
 <td>
                        <asp:TextBox ID="TextBox8" runat="server"  Width="50px"></asp:TextBox>
          
</td>
 <td>
                   
             <asp:TextBox ID="TextBox9" runat="server"  Width="50px"></asp:TextBox>
          
            
</td>
 <td>
                 
             <asp:TextBox ID="TextBox10" runat="server"  Width="120px"></asp:TextBox>
           
            
</td>
 </tr>

<tr>
                <td>
            <asp:DropDownList ID="DropDownList4" runat="server"  
                            Width="150px" AutoPostBack="True" 
                            onselectedindexchanged="DropDownList4_SelectedIndexChanged"   >
                            </asp:DropDownList>
</td> 

                <td>
                    <asp:DropDownList ID="DropDownList5" runat="server"  Width="150px" AppendDataBoundItems="true">
                            </asp:DropDownList>
          </td>
                <td>
                           
            <asp:DropDownList ID="DropDownList6" runat="server"  Width="200px" AppendDataBoundItems="true">
                            </asp:DropDownList>
                         
</td>
 <td>
               
                         <asp:TextBox ID="TextBox11" runat="server"  Width="50px"></asp:TextBox>
          
</td>
 <td>
                  
                         <asp:TextBox ID="TextBox12" runat="server"  Width="50px"></asp:TextBox>
          
</td>
 <td >
                   
             <asp:TextBox ID="TextBox13" runat="server"  Width="120px" 
                           ></asp:TextBox>
           
            
</td>
   </tr>

<tr>
                <td>
                   
            <asp:DropDownList ID="DropDownList7" runat="server"  
                            Width="150px" AutoPostBack="True" 
                            onselectedindexchanged="DropDownList7_SelectedIndexChanged" >
                            </asp:DropDownList>
           
            
</td> 

                <td>                
            <asp:DropDownList ID="DropDownList8" runat="server"  Width="150px" AppendDataBoundItems="true">
                            </asp:DropDownList>
         </td>
                <td>
                   <asp:DropDownList ID="DropDownList9" runat="server"  Width="200px" AppendDataBoundItems="true">
                            </asp:DropDownList>
         </td>
 <td>
                
                         <asp:TextBox ID="TextBox14" runat="server"  Width="50px"></asp:TextBox>
        </td>
 <td>
                     
                        <asp:TextBox ID="TextBox15" runat="server"  Width="50px"></asp:TextBox>
         
</td>
 <td>
                 
             <asp:TextBox ID="TextBox16" runat="server"  Width="120px" ></asp:TextBox>
        
</td>
  </tr>

<tr>
                <td >
                   
           <asp:DropDownList ID="DropDownList10" runat="server"  
                            Width="150px" AutoPostBack="True" 
                            onselectedindexchanged="DropDownList10_SelectedIndexChanged" >
                            </asp:DropDownList>
          
</td> 

                <td>
                  
            <asp:DropDownList ID="DropDownList11" runat="server"  Width="150px" AppendDataBoundItems="true">
                            </asp:DropDownList>
         </td>
                <td>
                           
            <asp:DropDownList ID="DropDownList12" runat="server"  Width="200px" AppendDataBoundItems="true">
                            </asp:DropDownList>
                       
</td>
 <td>
                  
                         <asp:TextBox ID="TextBox17" runat="server"  Width="50px"></asp:TextBox>
         
</td>
 <td>
                  
                          <asp:TextBox ID="TextBox18" runat="server"  Width="50px"></asp:TextBox>
     
</td>
 <td>
                         <asp:TextBox ID="TextBox19" runat="server"  Width="120px" ></asp:TextBox>
          
</td>
</tr>

<tr>
                <td>
                        <asp:DropDownList ID="DropDownList13" runat="server"  
                            Width="150px" AutoPostBack="True" 
                            onselectedindexchanged="DropDownList13_SelectedIndexChanged" >
                            </asp:DropDownList>
                      
</td> 

                <td>
                  
             <asp:DropDownList ID="DropDownList14" runat="server"  Width="150px" AppendDataBoundItems="true">
                            </asp:DropDownList>
           </td>
                <td >
                          
           <asp:DropDownList ID="DropDownList15" runat="server"  Width="200px" AppendDataBoundItems="true">
                            </asp:DropDownList>
                           
</td>
 <td> <asp:TextBox ID="TextBox20" runat="server"  Width="50px"></asp:TextBox>
         
</td>
 <td>
                   
                         <asp:TextBox ID="TextBox21" runat="server"  Width="50px"></asp:TextBox>
         
</td>
 <td>
    <asp:TextBox ID="TextBox22" runat="server"  Width="120px" ></asp:TextBox>
   </td>
  </tr>

<tr>
                <td>
                   <asp:DropDownList ID="DropDownList16" runat="server"  
                            Width="150px" AutoPostBack="True" 
                            onselectedindexchanged="DropDownList16_SelectedIndexChanged" >
                            </asp:DropDownList>
         </td> 

                <td>
                  <asp:DropDownList ID="DropDownList17" runat="server"  Width="150px" AppendDataBoundItems="true">
                            </asp:DropDownList>
          </td>
                <td>
    <asp:DropDownList ID="DropDownList18" runat="server"  Width="200px" AppendDataBoundItems="true">
                            </asp:DropDownList>
             
</td>
 <td>
                
                         <asp:TextBox ID="TextBox23" runat="server"  Width="50px"></asp:TextBox>
           
</td>
 <td>
               
                         <asp:TextBox ID="TextBox24" runat="server"  Width="50px"></asp:TextBox>
          
</td>
 <td>
                
             <asp:TextBox ID="TextBox25" runat="server"  Width="120px"  ></asp:TextBox>
         
</td>
  </tr>

<tr>
                <td>
                 
            <asp:DropDownList ID="DropDownList19" runat="server"  
                            Width="150px" AutoPostBack="True" 
                            onselectedindexchanged="DropDownList19_SelectedIndexChanged"  >
                            </asp:DropDownList>
            
</td> 

                <td >
                   
             <asp:DropDownList ID="DropDownList20" runat="server"  Width="150px" AppendDataBoundItems="true">
                            </asp:DropDownList>
           
</td>
                <td>
                           
            <asp:DropDownList ID="DropDownList21" runat="server"  Width="200px" AppendDataBoundItems="true">
                            </asp:DropDownList>
                          
</td>
 <td >
            
 <asp:TextBox ID="TextBox26" runat="server"  Width="50px"></asp:TextBox>           
</td>
 <td>    <asp:TextBox ID="TextBox27" runat="server"  Width="50px"></asp:TextBox>
        
</td>
 <td>
                             <asp:TextBox ID="TextBox28" runat="server"  Width="120px"  ></asp:TextBox>
       </td>
 </tr>

<tr>
                <td>
               <asp:DropDownList ID="DropDownList22" runat="server"  
                            Width="150px" AutoPostBack="True" 
                            onselectedindexchanged="DropDownList22_SelectedIndexChanged" >
                            </asp:DropDownList>
       </td> 
       <td>
            
            <asp:DropDownList ID="DropDownList23" runat="server"  Width="150px" AppendDataBoundItems="true">
                            </asp:DropDownList>
       </td>
                <td >
         <asp:DropDownList ID="DropDownList24" runat="server"  Width="200px" AppendDataBoundItems="true">
                            </asp:DropDownList>
                        
</td>
 <td>
                 <asp:TextBox ID="TextBox29" runat="server"  Width="50px"></asp:TextBox>
         
</td>
 <td>
  <asp:TextBox ID="TextBox30" runat="server"  Width="50px"></asp:TextBox>
       </td>
 <td>
                       <asp:TextBox ID="TextBox31" runat="server"  Width="120px"  ></asp:TextBox>
         
</td>
  </tr>

<tr>
                <td>
                     
           <asp:DropDownList ID="DropDownList25" runat="server"  
                            Width="150px" AutoPostBack="True" 
                            onselectedindexchanged="DropDownList25_SelectedIndexChanged" >
                            </asp:DropDownList>
            
            
</td> 

                <td >
                     
             <asp:DropDownList ID="DropDownList26" runat="server"  Width="150px" AppendDataBoundItems="true">
                            </asp:DropDownList>
            
</td>
                <td >
                              
             <asp:DropDownList ID="DropDownList27" runat="server"  Width="200px" AppendDataBoundItems="true">
                            </asp:DropDownList>
                              
</td>
 <td >
                     
                         <asp:TextBox ID="TextBox32" runat="server"  Width="50px"></asp:TextBox>
            
            
</td>
 <td >
                     
                         <asp:TextBox ID="TextBox33" runat="server"  Width="50px"></asp:TextBox>
            
            
</td>
 <td >
                     
             <asp:TextBox ID="TextBox34" runat="server"  Width="120px"  ></asp:TextBox>
            
            
</td>
   </tr>

<tr>
                <td >
                     
            <asp:DropDownList ID="DropDownList28" runat="server"  
                            Width="150px" AutoPostBack="True" 
                            onselectedindexchanged="DropDownList28_SelectedIndexChanged" >
                            </asp:DropDownList>
            
            
</td> 

                <td >
                     
             <asp:DropDownList ID="DropDownList29" runat="server"  Width="150px" AppendDataBoundItems="true">
                            </asp:DropDownList>
            
</td>
                <td >
                              
            <asp:DropDownList ID="DropDownList30" runat="server"  Width="200px" AppendDataBoundItems="true">
                            </asp:DropDownList>
                              
</td>
 <td >
                     
                         <asp:TextBox ID="TextBox35" runat="server"  Width="50px"></asp:TextBox>
            
            
</td>
 <td >
                     
                         <asp:TextBox ID="TextBox36" runat="server"  Width="50px"></asp:TextBox>
            
            
</td>
 <td >
                     
             <asp:TextBox ID="TextBox37" runat="server"  Width="120px"  ></asp:TextBox>
            
            
</td>
 </tr>

<tr>
                <td >
                     
             <asp:DropDownList ID="DropDownList31" runat="server"  
                            Width="150px" AutoPostBack="True" 
                            onselectedindexchanged="DropDownList31_SelectedIndexChanged" >
                            </asp:DropDownList>
            
            
</td> 

                <td >
                     
             <asp:DropDownList ID="DropDownList32" runat="server"  Width="150px" AppendDataBoundItems="true">
                            </asp:DropDownList>
            
</td>
                <td >
                              
             <asp:DropDownList ID="DropDownList33" runat="server"  Width="200px" AppendDataBoundItems="true">
                            </asp:DropDownList>
                              
</td>
 <td >
                     
                         <asp:TextBox ID="TextBox38" runat="server"  Width="50px"></asp:TextBox>
            
            
</td>
 <td >
                           <asp:TextBox ID="TextBox39" runat="server"  Width="50px"></asp:TextBox>
            
            
</td>
 <td >
                                <asp:TextBox ID="TextBox40" runat="server"  Width="120px"  ></asp:TextBox>
            
            
</td>
  </tr>
       </table>
    </center>
                    <div class="form-sec-row">
                        &nbsp;&nbsp;&nbsp;<label><strong>&nbsp;</strong></label>
                        <asp:Button ID="Button1" runat="server" CssClass="submit-button"   Height="28px"
                            Text="Submit" onclick="Button1_Click" />
                        <asp:Button ID="Button2" runat="server" CssClass="submit-button"   Height="28px"
                            Text="Cancel" onclick="Button2_Click"  />
                        <div class="clear">
                        </div>
                    </div>
                    <div class="clear">
                    </div>
                    <div class="listing"  style='width:100%; height:300px; overflow:auto;'>
                <asp:GridView id="GridView2"  Width="100%" CssClass="grid" PagerStyle-CssClass="pgr"
                 DataKeyNames ="OperationReqID" runat="server"  PageSize="100"
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
                       </div>
                         </asp:View>
                    
                    <asp:View ID="View2" runat="server"> 
                    <div class="listing">
                <asp:GridView id="GridView1"  Width="100%" CssClass="grid" PagerStyle-CssClass="pgr"
                 DataKeyNames ="RowID" runat="server" 
                 AutoGenerateColumns="False" AllowPaging="True" 
                 SelectedRowStyle-BackColor="GreenYellow" 
                    onpageindexchanging="GridView1_PageIndexChanging" 
                    onrowcommand="GridView1_RowCommand" >
                    <Columns>
                            <%--  <asp:CommandField SelectText="Select" ShowSelectButton="True" />--%>
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

                         <asp:TemplateField HeaderText="Issue Date" >
                            <ItemTemplate>
                                <asp:Label ID="lblissudate" runat="server" Text='<%# Bind("issudate") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                             <asp:TemplateField HeaderText="ConGrID" Visible="false">
                            <ItemTemplate>
                                <asp:Label ID="lblcongrid" runat="server" Text='<%# Bind("ConsumableGrID") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>   

                             <asp:TemplateField HeaderText="Conid" Visible="false">
                            <ItemTemplate>
                                <asp:Label ID="lblconid" runat="server" Text='<%# Bind("ConsumableID") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>

                         <asp:TemplateField HeaderText="Actual Quantity" >
                            <ItemTemplate>
                                <asp:Label ID="lblquantity" runat="server" Text='<%# Bind("ActualQty") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>

                          <asp:TemplateField HeaderText="Bill Quantity" >
                            <ItemTemplate>
                                <asp:Label ID="lblbillquantity" runat="server" Text='<%# Bind("BillQty") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>

                          <asp:TemplateField HeaderText="Advice By" Visible="false" >
                            <ItemTemplate>
                                <asp:Label ID="lbladviceby" runat="server" Text='<%# Bind("AdviceBy") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>

                          <asp:TemplateField HeaderText="Comment" Visible="false" >
                            <ItemTemplate>
                                <asp:Label ID="lblcomment" runat="server" Text='<%# Bind("Comment") %>'></asp:Label>
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

