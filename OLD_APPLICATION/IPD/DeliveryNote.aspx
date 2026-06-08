<%@ Page Title="" Language="C#" MasterPageFile="~/MasterAll/MasterPageAll.master" AutoEventWireup="true" CodeFile="DeliveryNote.aspx.cs" Inherits="IPD_DeliveryNote" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
 
<script type="text/javascript" language="javascript">
    function ShowDialog() {
        var rtvalue = window.open("Deliverpopup.aspx", "sss", "Width:800px; Height:550px; dialogLeft:250px;");
        document.getElementById("ctl00_ContentPlaceHolder1_TextBox46").value = rtvalue.NameValue;
    }


    function isNumberKey(evt) {
        var charCode = (evt.which) ? evt.which : event.keyCode;
        if (charCode != 46 && charCode > 31
            && (charCode < 48 || charCode > 57))
            return false;

        return true;
    }


    function Calling() {

        var date = new Date();
        $('.DateCall').datepicker({ dateFormat: 'dd/mm/yy' });

        $('.Time1').timepicker({
            showPeriod: true,
            showLeadingZero: true
        });

        $("input[id$='Tab2']").click(function () {


            if ($("input[id$='TextBox46']").val() == '') {
                alert('Please Enter Registration No !');
                $("input[id$='TextBox46']").focus();
                $("input[id$='TextBox46']").addClass('textboxerr');
                return false;
            }
            else {
                $("input[id$='TextBox46']").removeClass('textboxerr');
            }
        });

        $("input[id$='Button1']").click(function () {

            if ($("select[id$='DropDownList1']").val() == '0') {
                alert('Please Select Sex Property!');
                $("select[id$='DropDownList1']").addClass('textboxerr');
                $("select[id$='DropDownList1']").focus();
                return false;
            }
            else {
                $("select[id$='DropDownList1']").removeClass('textboxerr');
            }

            if ($("input[id$='TextBox1']").val() == '') {
                alert('Please Enter Sex Properly!');
                $("input[id$='TextBox1']").focus();
                $("input[id$='TextBox1']").addClass('textboxerr');
                return false;
            }
            else {
                $("input[id$='TextBox1']").removeClass('textboxerr');
            }

            if ($("input[id$='TextBox21']").val() == '') {
                alert('Please Enter Date Properly!');
                $("input[id$='TextBox21']").focus();
                $("input[id$='TextBox21']").addClass('textboxerr');
                return false;
            }
            else {
                $("input[id$='TextBox21']").removeClass('textboxerr');
            }



            if ($("input[id$='TextBox22']").val() == '') {
                alert('Please Enter Time Properly!');
                $("input[id$='TextBox22']").focus();
                $("input[id$='TextBox22']").addClass('textboxerr');
                return false;
            }
            else {
                $("input[id$='TextBox22']").removeClass('textboxerr');
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
         <asp:Label ID="Label1" runat="server">Delivery Note</asp:Label>
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
                <strong><asp:Label ID="lblError" runat="server" Font-Bold="True"></asp:Label>
                </strong>
                <div class="clear"></div>
            </div>
            <asp:HiddenField ID="HiddenField1" runat="server" />
			           
                       
             <div class="form-sec-row">
                        <label>
                        <strong>
                       Registration No :</strong></label>
                          <asp:TextBox ID="TextBox46" runat="server" CssClass="textbox-medium1" 
                              Enabled="true"></asp:TextBox>
                                <asp:Button ID="Button3" runat="server"  Height="28px" Text="Quick Search" CssClass="submit-buttonCheck" OnClientClick="ShowDialog()"/>
                              <asp:Button ID="Button2" runat="server" Text="Fetch"   Height="28px"
                            CssClass="submit-button" onclick="Button2_Click"/>
                        <div class="clear">
                        </div>
                    </div>

                         <div class="form-sec-row">
                        <label>
                        <strong>
                     Requisition No :</strong></label>
                          <asp:TextBox ID="TextBox47" runat="server" CssClass="textbox-medium1" 
                                Enabled="False"></asp:TextBox>
                        <div class="clear">
                        </div>
                    </div>

                        <div class="form-sec-row">
                        <label>
                        <strong>
                       Patient's Name :</strong></label>
                          <asp:TextBox ID="TextBox48" runat="server" CssClass="textbox-medium1" 
                                Enabled="False"></asp:TextBox>
                        <div class="clear">
                        </div>
                    </div>

                        <div class="form-sec-row">
                        <label>
                        <strong>
                     Current Bed No :</strong></label>
                          <asp:TextBox ID="TextBox49" runat="server" CssClass="textbox-medium1" 
                                Enabled="False"></asp:TextBox>
                        <div class="clear">
                        </div>
                    </div>

                         <div class="form-sec-row">
                        <label>
                        <strong>
                       Operation Name :</strong></label>
                          <asp:TextBox ID="TextBox50" runat="server" CssClass="textbox-medium1" 
                                Enabled="False"></asp:TextBox>
                        <div class="clear">
                        </div>
                    </div>

                        <div class="form-sec-row">
                        <label>
                        <strong>
                       Admission Date :</strong></label>
                          <asp:TextBox ID="TextBox51" runat="server" CssClass="textbox-medium1" 
                                Enabled="False"></asp:TextBox>
                                      <asp:Label ID="Label3" runat="server" ForeColor="Green" Text="(DD/MM/YYYY)"></asp:Label>
                        <div class="clear">
                        </div>
                    </div>

                

                          <div class="form-sec-row">
                        <label>
                        <strong>
                       No of Baby :</strong></label>
              
                              <asp:DropDownList ID="DropDownList25" runat="server" CssClass="textbox-medium1" 
                                  AutoPostBack="True" 
                                  onselectedindexchanged="DropDownList25_SelectedIndexChanged">
                                  <asp:ListItem>Single</asp:ListItem>
                                  <asp:ListItem>Twin</asp:ListItem>
                                  <asp:ListItem>Triplet</asp:ListItem>
                                  <asp:ListItem>Quadruplet</asp:ListItem>
                                  <asp:ListItem>Quintuplet</asp:ListItem>
                              </asp:DropDownList>
                        <div class="clear">
                        </div>
                    </div>

                       <div class="form-sec-row">
                        <label>
                        <strong>
                       Mode of Delivery :</strong></label>
                           <asp:DropDownList ID="DropDownList24" runat="server" CssClass="textbox-medium1">
                           </asp:DropDownList>
                               <div class="clear">
                        </div>
                    </div>

                 </div>
   

  
     <div  style='width:100%; overflow:auto;'>
      
    <table border=".5" cellpadding="0" cellspacing="0" width="100%">
          
       <tr style='background-color:#FF9300;'>

           <td align="center" style='width:160px;'>
                  
             <label><strong>Date</strong></label> 
            
        </td> 

            <td align="center" style='width:160px;'>
                 
             <label><strong>Time</strong></label> 
          
        </td> 
                <td align="center" style='width:160px;'>
                  
             <label><strong>Weight</strong></label> 
          
        </td> 

      
                <td align="center" style='width:160px;'>
                          
             <label ><strong> Sex </strong></label>
                           
</td>
 <td align="center" style='width:160px;'>
                    
             <label><strong>Cry</strong></label> 
        
            
</td>
 <td align="center" style='width:160px;'>
                    
             <label><strong>Liquor</strong></label> 
           
            
</td>
 <td align="center" style='width:160px;'>
                 
             <label><strong>AGScoreAtBrth</strong></label> 
          
        </td> 

      
                <td align="center" style='width:160px;'>
                         
             <label ><strong> AGScoreAf.10Min </strong></label>
                       
</td>
 <td align="center" style='width:160px;'>
                    
             <label><strong>Maturity</strong></label> 
            
            
</td>
 <td align="center" style='width:160px;'>
                    
             <label><strong>Remarks</strong></label> 
           
            
</td>
        
                      
            </tr>


                <tr>

                           <td align="center" >
                
                       <asp:TextBox ID="TextBox21" runat="server" CssClass="DateCall" 
                            Width="80px"></asp:TextBox>
       
            
</td> 
           <td align="center" >
                
                       <asp:TextBox ID="TextBox22" runat="server" CssClass="Time1"  Width="80px"></asp:TextBox>
            
            
</td> 
                <td align="center" >
                
                       <asp:TextBox ID="TextBox1" runat="server"  Width="70px"  MaxLength="5" onkeypress="return isNumberKey(event)" ></asp:TextBox>
            
            
</td> 

       
      
 <td  align="center">
                   
                        <asp:DropDownList ID="DropDownList1" runat="server"   Width="80px">
                        </asp:DropDownList>
            
            
</td>
 <td align="center">
                  
  <asp:DropDownList ID="DropDownList2" runat="server"  Width="90px">
                        </asp:DropDownList>
           
            
</td>
<td align="center">
                  
                   <asp:DropDownList ID="DropDownList3" runat="server" Width="100px">
                        </asp:DropDownList>
          
            
</td>

       <td align="center">
                   
      <asp:TextBox ID="TextBox2" runat="server"       Width="60px"></asp:TextBox>
         
            
</td> 

       
      
 <td  align="center">
                    
                          <asp:TextBox ID="TextBox3" runat="server"     Width="60px"></asp:TextBox>
           
            
</td>
 <td align="center">
                  
                                 <asp:DropDownList ID="DropDownList4" runat="server"  Width="80px">
                        </asp:DropDownList>
      
            
</td>
<td align="center">
                 
             <asp:TextBox ID="TextBox4" runat="server"  Width="120px" ></asp:TextBox>
         
            
</td>
 </tr>

 <tr>

            <td align="center" >
                  
                       <asp:TextBox ID="TextBox23" runat="server"  CssClass="DateCall"    Width="80px"></asp:TextBox>
           
            
</td> 

           <td align="center" >
                    
                       <asp:TextBox ID="TextBox24" runat="server"  CssClass="Time1"     Width="80px"></asp:TextBox>
            
            
</td> 
                <td align="center" >
                    
                       <asp:TextBox ID="TextBox5" runat="server"  MaxLength="5" onkeypress="return isNumberKey(event)"  Width="70px"></asp:TextBox>
            
            
</td> 

       
      
 <td  align="center">
                   
                        <asp:DropDownList ID="DropDownList5" runat="server"  Width="80px">
                        </asp:DropDownList>
          
            
</td>
 <td align="center">
                  
  <asp:DropDownList ID="DropDownList6" runat="server" Width="90px">
                        </asp:DropDownList>
         
            
</td>
<td align="center">
                    
                   <asp:DropDownList ID="DropDownList7" runat="server"  Width="100px">
                        </asp:DropDownList>
         
            
</td>

       <td align="center"  >
                    
      <asp:TextBox ID="TextBox6" runat="server"   Width="60px"></asp:TextBox>
            
            
</td> 

       
      
 <td  align="center">
                   
                          <asp:TextBox ID="TextBox7" runat="server"     Width="60px"></asp:TextBox>
           
            
</td>
 <td align="center">
               
                                 <asp:DropDownList ID="DropDownList8" runat="server" Width="80px">
                        </asp:DropDownList>
        
            
</td>
<td align="center">
                   
             <asp:TextBox ID="TextBox8" runat="server" Width="120px" ></asp:TextBox>
          
            
</td>
 </tr>

 <tr >

            <td align="center" >
                    
                       <asp:TextBox ID="TextBox25" runat="server"  CssClass="DateCall"  Width="80px"></asp:TextBox>
           
            
</td> 
           <td align="center" >
                  
                       <asp:TextBox ID="TextBox26" runat="server"  CssClass="Time1"   Width="80px"></asp:TextBox>
         
            
</td> 

                <td align="center" >
                   
                       <asp:TextBox ID="TextBox9" runat="server"   MaxLength="5" onkeypress="return isNumberKey(event)"  Width="70px" 
                       ></asp:TextBox>
          
            
</td> 

       
      
 <td  align="center">
                
                        <asp:DropDownList ID="DropDownList9" runat="server"  Width="80px">
                        </asp:DropDownList>
       
            
</td>
 <td align="center">
                  
  <asp:DropDownList ID="DropDownList10" runat="server"  Width="90px">
                        </asp:DropDownList>
       
            
</td>
<td align="center">
                   
                   <asp:DropDownList ID="DropDownList11" runat="server"  Width="100px">
                        </asp:DropDownList>
            
            
</td>

       <td align="center"  >
                   
      <asp:TextBox ID="TextBox10" runat="server" 
                            Width="60px"></asp:TextBox>
           
            
</td> 

       
      
 <td  align="center">
                    
                          <asp:TextBox ID="TextBox11" runat="server"      Width="60px"></asp:TextBox>
           
            
</td>
 <td align="center">
                   
                                 <asp:DropDownList ID="DropDownList12" runat="server"  Width="80px">
                        </asp:DropDownList>
           
</td>
<td align="center">
                  
             <asp:TextBox ID="TextBox12" runat="server"   Width="120px" ></asp:TextBox>
           
            
</td>
 </tr>

 <tr>

            <td align="center" >
                   
                       <asp:TextBox ID="TextBox27" runat="server"   CssClass="DateCall"     Width="80px"></asp:TextBox>
          
            
</td> 

           <td align="center" >
                    
                       <asp:TextBox ID="TextBox28" runat="server"   CssClass="Time1"  Width="80px"></asp:TextBox>
        
            
</td> 
                <td align="center" >
                   
                       <asp:TextBox ID="TextBox13" runat="server"    MaxLength="5" onkeypress="return isNumberKey(event)"     Width="70px"></asp:TextBox>
            
            
</td> 

       
      
 <td  align="center">
                
                        <asp:DropDownList ID="DropDownList13" runat="server"  Width="80px">
                        </asp:DropDownList>
           
            
</td>
 <td align="center">
                   
  <asp:DropDownList ID="DropDownList14" runat="server"  Width="90px">
                        </asp:DropDownList>
   
            
</td>
<td align="center">
                    
                   <asp:DropDownList ID="DropDownList15" runat="server"  Width="100px">
                        </asp:DropDownList>
     
            
</td>

       <td align="center" >
              
      <asp:TextBox ID="TextBox14" runat="server"       Width="60px"></asp:TextBox>
            
            
</td> 

       
      
 <td  align="center">
                   
                          <asp:TextBox ID="TextBox15" runat="server"       Width="60px"></asp:TextBox>
         
            
</td>
 <td align="center">
                   
                                 <asp:DropDownList ID="DropDownList16" runat="server" Width="80px">
                        </asp:DropDownList>
         
            
</td>
<td align="center">
                   
             <asp:TextBox ID="TextBox16" runat="server"  Width="120px" ></asp:TextBox>
           
            
</td>
 </tr>

 <tr>

            <td align="center" >
                    
                       <asp:TextBox ID="TextBox29" runat="server"  CssClass="DateCall"        Width="80px"></asp:TextBox>
        
            
</td> 

           <td align="center" >
                    
                       <asp:TextBox ID="TextBox30" runat="server"     CssClass="Time1"     Width="80px"></asp:TextBox>
          
            
</td> 

                <td align="center" >
                   
                       <asp:TextBox ID="TextBox17" runat="server"   MaxLength="5" onkeypress="return isNumberKey(event)"   Width="70px"></asp:TextBox>
          
            
</td> 

       
      
 <td  align="center">
                
                        <asp:DropDownList ID="DropDownList17" runat="server" Width="80px">
                        </asp:DropDownList>
         
            
</td>
 <td align="center">
                 
  <asp:DropDownList ID="DropDownList18" runat="server"  Width="90px">
                        </asp:DropDownList>
         
            
</td>
<td align="center">
                  
                   <asp:DropDownList ID="DropDownList19" runat="server"  Width="100px">
                        </asp:DropDownList>
           
</td>

       <td align="center" >
                 
      <asp:TextBox ID="TextBox18" runat="server"  Width="60px"></asp:TextBox>
        
            
</td> 

       
      
 <td  align="center">
                
                          <asp:TextBox ID="TextBox19" runat="server"         Width="60px"></asp:TextBox>
         
            
</td>
 <td align="center">
                    
                                 <asp:DropDownList ID="DropDownList20" runat="server"  Width="80px">
                        </asp:DropDownList>
          
            
</td>
<td align="center">
               
             <asp:TextBox ID="TextBox20" runat="server"  Width="120px" ></asp:TextBox>
        
            
</td>
 </tr>

 </table>
 
    </div>
 
     <div class="form-sec-row"> 
     <label><strong ></strong></label>
     <asp:Button ID="Button1" runat="server" Text="Submit"  Height="30px" 
        CssClass="submit-button" onclick="Button1_Click"/> 
       <asp:Button ID="Button4" runat="server" Text="Cancel"   Height="30px"
        CssClass="submit-button" onclick="Button4_Click" />
    
     </div>
   
    <div class="listing"  style='width:100%; height:100px; overflow:auto;'>
                <asp:GridView id="GridView2"  Width="100%" CssClass="grid" PagerStyle-CssClass="pgr"
                 DataKeyNames ="OperationReqID" runat="server" 
                 AutoGenerateColumns="False" AllowPaging="True"  PageSize="100"
                 SelectedRowStyle-BackColor="GreenYellow" 
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
       <asp:View ID="View2" runat="server">

    <div class="listing"  style='width:100%; height:250px; overflow:auto;'>
                <asp:GridView id="GridView1"  Width="100%" CssClass="grid" PagerStyle-CssClass="pgr"
                 DataKeyNames ="RowID" runat="server" 
                 AutoGenerateColumns="False" AllowPaging="True" 
                 SelectedRowStyle-BackColor="GreenYellow"  PageSize="100"
                    onpageindexchanging="GridView1_PageIndexChanging" >
                    <Columns>

                        <asp:TemplateField HeaderText="ID" Visible="false">
                            <ItemTemplate>
                                <asp:Label ID="lblid" runat="server" Text='<%# Bind("RowID") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
         
                            <asp:TemplateField HeaderText="Patient's Name">
                            <ItemTemplate>
                                <asp:Label ID="lbllname" runat="server" Text='<%# Bind("patient_name") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>

                            <asp:TemplateField HeaderText="Operaton Name">
                            <ItemTemplate>
                                <asp:Label ID="lblotname" runat="server" Text='<%# Bind("OperationName") %>'></asp:Label>
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

                               <asp:TemplateField HeaderText="Weight" >
                            <ItemTemplate>
                                <asp:Label ID="lblusagecost" runat="server" Text='<%# Bind("Weight") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>

                         <asp:TemplateField HeaderText="AGScore">
                            <ItemTemplate>
                                <asp:Label ID="lblremarks" runat="server" Text='<%# Bind("AGScore") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                         <asp:TemplateField HeaderText="AGScore After 10 Min">
                            <ItemTemplate>
                                <asp:Label ID="lblremarks" runat="server" Text='<%# Bind("AGScoreAfter") %>'></asp:Label>
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

