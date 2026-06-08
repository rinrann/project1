<%@ Page Title="" Language="C#" MasterPageFile="~/MasterAll/MasterPageAll.master" AutoEventWireup="true" CodeFile="OthersCharge.aspx.cs" Inherits="IPD_OthersCharge" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">

<script type="text/javascript" language="javascript">
    function ShowDialog() {
        var rtvalue = window.open("IpdpatientPopup.aspx", "sss", "Width:800px; Height:550px; dialogLeft:250px;");
        document.getElementById("ctl00_ContentPlaceHolder1_TextBox31").value = rtvalue.NameValue;

    }

    function Calling() {
        var date = new Date();
        $("input[id$='TextBox1']").datepicker({ dateFormat: 'dd/mm/yy' });

        var date = new Date();
        $("input[id$='TextBox4']").datepicker({ dateFormat: 'dd/mm/yy' });



        var date = new Date();
        $("input[id$='TextBox7']").datepicker({ dateFormat: 'dd/mm/yy' });


        var date = new Date();
        $("input[id$='TextBox10']").datepicker({ dateFormat: 'dd/mm/yy' });

        var date = new Date();
        $("input[id$='TextBox13']").datepicker({ dateFormat: 'dd/mm/yy' });

        var date = new Date();
        $("input[id$='TextBox16']").datepicker({ dateFormat: 'dd/mm/yy' });

        var date = new Date();
        $("input[id$='TextBox19']").datepicker({ dateFormat: 'dd/mm/yy' });

        var date = new Date();
        $("input[id$='TextBox22']").datepicker({ dateFormat: 'dd/mm/yy' });

        var date = new Date();
        $("input[id$='TextBox25']").datepicker({ dateFormat: 'dd/mm/yy' });

        var date = new Date();
        $("input[id$='TextBox28']").datepicker({ dateFormat: 'dd/mm/yy' });


        $("input[id$='Tab2']").click(function () {


            if ($("input[id$='TextBox31']").val() == '') {
                alert('Please Enter Registration No !');
                $("input[id$='TextBox31']").focus();
                $("input[id$='TextBox31']").addClass('textboxerr');
                return false;
            }
            else {
                $("input[id$='TextBox31']").removeClass('textboxerr');
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
         <asp:Label ID="Label1" runat="server">Others Charges</asp:Label>
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
                          <asp:TextBox ID="TextBox31" runat="server" CssClass="textbox-medium1" 
                              Enabled="true"></asp:TextBox>
                                <asp:Button ID="Button3" runat="server" Height="28px" Text="Quick Search" CssClass="submit-buttonCheck" OnClientClick="ShowDialog()"/>
                              <asp:Button ID="Button2" runat="server" Text="Fetch"  Height="28px" 
                            CssClass="submit-button" onclick="Button2_Click"  />
                        <div class="clear">
                        </div>
                    </div>
                        <div class="form-sec-row">
                        <label>
                        <strong>
                       Patient's Name :</strong></label>
                          <asp:TextBox ID="TextBox32" runat="server" CssClass="textbox-medium1" 
                                Enabled="False"></asp:TextBox>
                        <div class="clear">
                        </div>
                    </div>

                        <div class="form-sec-row">
                        <label>
                        <strong>
                       Bed No :</strong></label>
                          <asp:TextBox ID="TextBox33" runat="server" CssClass="textbox-medium1" 
                                Enabled="False"></asp:TextBox>
                        <div class="clear">
                        </div>
                    </div>

                        <div class="form-sec-row">
                        <label>
                        <strong>
                       Admission Date :</strong></label>
                          <asp:TextBox ID="TextBox34" runat="server" CssClass="textbox-medium1" 
                                Enabled="False"></asp:TextBox>
                                      <asp:Label ID="Label3" runat="server" ForeColor="Green" Text="(DD/MM/YYYY)"></asp:Label>
                        <div class="clear">
                        </div>
                    </div>
   
     </div>
 
 
    <table border="1" cellpadding="0" cellspacing="0" width="980px">
          
       <tr style='background-color:#FF9300;'>
     
 <td align="center">
                    <div class="form-sec-row"> 
             <label class="lname"><strong>Issue Date</strong></label> 
            </div>
            
</td>

 <td align="center">
                    <div class="form-sec-row"> 
             <label class="lname"><strong>Others Charge Details</strong></label> 
            </div>
            
</td>
 <td align="center">
                    <div class="form-sec-row"> 
             <label class="lname"><strong>Amount</strong></label> 
            </div>
            
</td>
        
                      
            </tr>
                <tr>
 

     
 <td align="center">
                  
                       <asp:TextBox ID="TextBox1" runat="server" Width="150px"></asp:TextBox>
          
            
</td>
 <td align="center">
                  
             <asp:TextBox ID="TextBox2" runat="server"  Width="350px"></asp:TextBox>
            
            
</td>

 <td align="center">
                
             <asp:TextBox ID="TextBox3" runat="server"  Width="150px"></asp:TextBox>
       
            
</td>
 </tr>


 <tr>
 

     
 <td align="center">
                  
                       <asp:TextBox ID="TextBox4" runat="server" Width="150px"></asp:TextBox>
          
            
</td>
 <td align="center">
                  
             <asp:TextBox ID="TextBox5" runat="server"  Width="350px"></asp:TextBox>
            
            
</td>

 <td align="center">
                
             <asp:TextBox ID="TextBox6" runat="server"  Width="150px"></asp:TextBox>
       
            
</td>
 </tr>

 <tr>
 

     
 <td align="center">
                  
                       <asp:TextBox ID="TextBox7" runat="server" Width="150px"></asp:TextBox>
          
            
</td>
 <td align="center">
                  
             <asp:TextBox ID="TextBox8" runat="server"  Width="350px"></asp:TextBox>
            
            
</td>

 <td align="center">
                
             <asp:TextBox ID="TextBox9" runat="server"  Width="150px"></asp:TextBox>
       
            
</td>
 </tr>

 <tr>
 

     
 <td align="center">
                  
                       <asp:TextBox ID="TextBox10" runat="server" Width="150px"></asp:TextBox>
          
            
</td>
 <td align="center">
                  
             <asp:TextBox ID="TextBox11" runat="server"  Width="350px"></asp:TextBox>
            
            
</td>

 <td align="center">
                
             <asp:TextBox ID="TextBox12" runat="server"  Width="150px"></asp:TextBox>
       
            
</td>
 </tr>

 <tr>
 

     
 <td align="center">
                  
                       <asp:TextBox ID="TextBox13" runat="server" Width="150px"></asp:TextBox>
          
            
</td>
 <td align="center">
                  
             <asp:TextBox ID="TextBox14" runat="server"  Width="350px"></asp:TextBox>
            
            
</td>

 <td align="center">
                
             <asp:TextBox ID="TextBox15" runat="server"  Width="150px"></asp:TextBox>
       
            
</td>
 </tr>

 <tr>
 

     
 <td align="center">
                  
                       <asp:TextBox ID="TextBox16" runat="server" Width="150px"></asp:TextBox>
          
            
</td>
 <td align="center">
                  
             <asp:TextBox ID="TextBox17" runat="server"  Width="350px"></asp:TextBox>
            
            
</td>

 <td align="center">
                
             <asp:TextBox ID="TextBox18" runat="server"  Width="150px"></asp:TextBox>
       
            
</td>
 </tr>

 <tr>
 

     
 <td align="center">
                  
                       <asp:TextBox ID="TextBox19" runat="server" Width="150px"></asp:TextBox>
          
            
</td>
 <td align="center">
                  
             <asp:TextBox ID="TextBox20" runat="server"  Width="350px"></asp:TextBox>
            
            
</td>

 <td align="center">
                
             <asp:TextBox ID="TextBox21" runat="server"  Width="150px"></asp:TextBox>
       
            
</td>
 </tr>

 <tr>
 

     
 <td align="center">
                  
                       <asp:TextBox ID="TextBox22" runat="server" Width="150px"></asp:TextBox>
          
            
</td>
 <td align="center">
                  
             <asp:TextBox ID="TextBox23" runat="server"  Width="350px"></asp:TextBox>
            
            
</td>

 <td align="center">
                
             <asp:TextBox ID="TextBox24" runat="server"  Width="150px"></asp:TextBox>
       
            
</td>
 </tr>

 <tr>
 

     
 <td align="center">
                  
                       <asp:TextBox ID="TextBox25" runat="server" Width="150px"></asp:TextBox>
          
            
</td>
 <td align="center">
                  
             <asp:TextBox ID="TextBox26" runat="server"  Width="350px"></asp:TextBox>
            
            
</td>

 <td align="center">
                
             <asp:TextBox ID="TextBox27" runat="server"  Width="150px"></asp:TextBox>
       
            
</td>
 </tr>

 <tr>
 

     
 <td align="center">
                  
                       <asp:TextBox ID="TextBox28" runat="server" Width="150px"></asp:TextBox>
          
            
</td>
 <td align="center">
                  
             <asp:TextBox ID="TextBox29" runat="server"  Width="350px"></asp:TextBox>
            
            
</td>

 <td align="center">
                
             <asp:TextBox ID="TextBox30" runat="server"  Width="150px"></asp:TextBox>
       
            
</td>
 </tr>
  
       </table>
  

      <div class="form-sec-row">
     <label>  <strong> </strong></label>
        <asp:Button ID="Button1" runat="server" Text="Submit"  Height="28px" 
        CssClass="submit-buttondtls" onclick="Button1_Click" />
      <asp:Button ID="Button4" runat="server" Text="Cancel"  Height="28px" 
        CssClass="submit-buttondtls" onclick="Button4_Click"  />
 
        </div>

          </asp:View>
                    
                    <asp:View ID="View2" runat="server">

    <div class="listing"  style='width:100%; height:300px; overflow:auto;'>
                <asp:GridView id="GridView1"  Width="100%" CssClass="grid" PagerStyle-CssClass="pgr"
                 DataKeyNames ="RowID" runat="server" 
                 AutoGenerateColumns="False" AllowPaging="True" 
                 SelectedRowStyle-BackColor="GreenYellow" 
                    onpageindexchanging="GridView1_PageIndexChanging" 
                    onrowcommand="GridView1_RowCommand" >
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

                         <asp:TemplateField HeaderText="Date" >
                            <ItemTemplate>
                                <asp:Label ID="lbldatee" runat="server" Text='<%# Bind("IssueDate") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                             <asp:TemplateField HeaderText="Others Details" >
                            <ItemTemplate>
                                <asp:Label ID="lblothers" runat="server" Text='<%# Bind("OthersDetails") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>   

                             <asp:TemplateField HeaderText="Amount" >
                            <ItemTemplate>
                                <asp:Label ID="lblamount" runat="server" Text='<%# Bind("Amount") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>

           
              <asp:CommandField SelectText="Edit" ShowSelectButton="True" ControlStyle-CssClass="temp" HeaderText="Edit" SelectImageUrl="~/Images/edit.png" ButtonType="Image">
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

