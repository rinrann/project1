<%@ Page Title="" Language="C#" MasterPageFile="~/MasterAll/MasterPageAll.master" AutoEventWireup="true" CodeFile="PatientListSearch.aspx.cs" Inherits="DayCare_PatientListSearch" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <script type="text/javascript" language="javascript">


        function ShowDialog(PatientReg)
        {

            //var myWindow = window.open("", "", "width=200,height=100");
            //var myWindow = window.open("", "+(dt1.Rows[i]["PatientReg"])+" width=200,height=100");
            //myWindow.document.write("<p>This window's name is: " + myWindow.name + "

            //alert('Hi');
            window.open("DialysisDetailsPopup.aspx?PatientReg=" +PatientReg , "", "Width:800px; Height:550px; dialogLeft:250px;");
            //window.open("DialysisDetailsPopup.aspx?regno=" + regno, "sss", "Width:800px; Height:550px; dialogLeft:250px;");
           // document.getElementById("ctl00_ContentPlaceHolder1_TextBox18").value = a[1];
            

        }


        function printDiv(divID) {
            var divElements = document.getElementById(divID).innerHTML;
            var oldPage = document.body.innerHTML;
            document.body.innerHTML = "<html><head><title></title></head><body>" + divElements + "</body>";
            window.print();
            document.body.innerHTML = oldPage;
        }

        function Calling() {
            var date = new Date();
            $("input[id$='TextBox2']").datepicker({ dateFormat: 'dd/mm/yy' });

            var date = new Date();
            $("input[id$='TextBox1']").datepicker({ dateFormat: 'dd/mm/yy' });


            $("input[id$='Button6']").click(function () {
                if ($("select[id$='DropDownList1']").val() == '0') {
                    alert('Select Type of Search !');
                    $("select[id$='DropDownList1']").focus();
                    $("select[id$='DropDownList1']").addClass('textboxerr');
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
         <asp:Label ID="Label11" runat="server">Report Analysis</asp:Label>
     </div>
     <div class="formbox">
        <div class="form-sec">
            <div class="error">
                <strong>
                    <asp:Label ID="lblError" runat="server" Text="Label" Visible="false"></asp:Label>
                </strong>
                <div class="clear"></div>
            </div>
            </div>
           <table width="100%" cellpadding="0" cellspacing="0">
           <tr>
                <td style='width:110px; '>
                   <label><strong>
          Type of Search :</strong></label>          
</td>
<td style='width:100px'>                
                        <asp:DropDownList ID="DropDownList1" runat="server" CssClass="textbox-dropdown">
                            <asp:ListItem Value="Select">--Select--</asp:ListItem>
                            <asp:ListItem Value="PatientSearch">Patient Search</asp:ListItem>
                            <asp:ListItem Value="TotalRegisteredPatient">Total Registered Patient</asp:ListItem>
                            <asp:ListItem Value="TotalNoofDialysisDonebyAllPatient">Total No of Dialysis</asp:ListItem>
                            <asp:ListItem Value="PerPatientDialysisFrequency">Per Patient  Dialysis Frequency</asp:ListItem>
                        </asp:DropDownList>
           
</td>
                <td style='width:70px; '>
                            <label><strong>
                         From Date:</strong></label>
                        
</td>
<td style='width:70px;'>
                           
   <asp:TextBox ID="TextBox2" runat="server" ToolTip="Select From Date"></asp:TextBox>
                            
</td>
<td style='width:70px;'>
          <label><strong>
                         To Date:</strong></label>                   
</td>
<td style='width:100px'>
                            
      <asp:TextBox ID="TextBox1" runat="server" ToolTip="Select To Date"></asp:TextBox>
                      
</td>
<td style='width:80px'>
                            
    <asp:Button ID="Button6" runat="server" Text="Search" CssClass="submit-button" 
        onclick="Button6_Click"/>
                      
</td>
    </tr>
            </table>
            </div>
            
             <div class="formbox">
           
                 <asp:Panel ID="Panel1" runat="server">
                 <table width="100%">
          
       <tr style='height:20px;'>
                <td style='width:10%; font-family: Verdana;font-size: small;font-weight: bold;text-align:left;'>
                    <div class="form-sec-row"> 
           Total Registered Patient:
            </div>
</td>
                <td style='  width:7%;text-align:center; font-family: Verdana;font-size: small;color:#3B3A35; font-weight: bold;'>
                             <div class="form-sec-row"> 
                                 <asp:Label ID="Label1" runat="server" Text="Label"></asp:Label>
            </div>                  
</td>
<td style='width:7%;text-align:right;'>
                            
                                 <asp:Button ID="Button1" CssClass="submit-buttondtls"  runat="server" 
                                     Text="Details" onclick="Button1_Click" />
                            
</td>
                             
                      
            </tr>
            </table>
                 </asp:Panel> 
                 
                                  
                 <asp:Panel ID="Panel2" runat="server">
                 <table width="100%">
            <tr>
             
                <td style='width:10%; font-family: Verdana;font-size: small;font-weight: bold;text-align:left;'>
                    <div class="form-sec-row"> 
            Total No of Dialysis Done by All Patient :
            </div>
</td>
                <td style='width:7%;text-align:center; font-family: Verdana;font-size: small;font-weight: bold;'>
                             <div class="form-sec-row"> 
                                 <asp:Label ID="Label2" runat="server" Text="Label"></asp:Label>
            </div>                  
</td>
<td style='width:7%;text-align:right;'>
                            
                                 <asp:Button ID="Button3" CssClass="submit-buttondtls" runat="server" 
                                     Text="Details" onclick="Button3_Click" />
                     
</td>
                             
                      
            </tr>
            </table>
                 </asp:Panel> 
                 
                 <asp:Panel ID="Panel3" runat="server">
                 <table width="100%">
            <tr>
             
                <td  style='width:10%; font-family:Verdana;font-size: small;font-weight: bold;text-align:left;'>
                    <div class="form-sec-row"> 
             Per patient  Dialysis frequency :
            </div>
</td>
                <td style='width:7%;text-align:center; font-family: Verdana;font-size: small;font-weight: bold;'>
                             <div class="form-sec-row"> 
                                 <asp:Label ID="Label3" runat="server" Text="Label"></asp:Label>
            </div>                  
</td>
<td style='width:7%;text-align:right;'>
                             
                                 <asp:Button ID="Button4" CssClass="submit-buttondtls" runat="server" 
                                     Text="Details" onclick="Button4_Click" />
            
</td>
                             
                      
            </tr>
            </table>
                 </asp:Panel> 
          
                 <asp:Panel ID="Panel5" runat="server">
                 <table width="100%">
            <tr>
            <td style='width:5%; font-family: Verdana;font-size: small;font-weight: bold;text-align:left;'>
                <div class="form-sec-row"> Reg. No : </div>
</td>

<td style='width:5%;text-align:center; font-family: Verdana;font-size: small;font-weight: bold;'>
                <div class="form-sec-row"><asp:TextBox ID="TextBox6" runat="server" Width="100px"></asp:TextBox></div>                  
</td>
<td style='width:5%; font-family: Verdana;font-size: small;font-weight: bold;text-align:left;'>
                <div class="form-sec-row"> Name : </div>
</td>

<td style='width:5%;text-align:center; font-family: Verdana;font-size: small;font-weight: bold;'>
                <div class="form-sec-row"><asp:TextBox ID="TextBox3" runat="server" Width="105px"></asp:TextBox></div>                  
</td>

<td style='width:5%; font-family: Verdana;font-size: small;font-weight: bold;text-align:left;'>
                <div class="form-sec-row"> Address : </div>
</td>

<td style='width:5%;text-align:center; font-family: Verdana;font-size: small;font-weight: bold;'>
                <div class="form-sec-row"><asp:TextBox ID="TextBox4" runat="server" Width="105px"></asp:TextBox></div>                  
</td>

<td style='width:5%; font-family: Verdana;font-size: small;font-weight: bold;text-align:left;'>
                <div class="form-sec-row"> Ph. No : </div>
</td>

<td style='width:5%;text-align:center; font-family: Verdana;font-size: small;font-weight: bold;'>
                <div class="form-sec-row"><asp:TextBox ID="TextBox5" runat="server" Width="105px"></asp:TextBox></div>                  
</td>
<td style='width:7%;text-align:center;'><asp:Button ID="Button2" 
        CssClass="submit-buttondtls" runat="server" Text="Details" 
        onclick="Button2_Click" />                         
</td>
                             
                      
            </tr>
      </table>
                 </asp:Panel>
             </div>
   
                         <table width="100%">
                    <tr>        
                        <td style="width: 100%" align="center">  <div id='mydiv'>                
                            <asp:Literal ID="ltrReport" runat="server"></asp:Literal><br />      </div>              
                        </td>
                    </tr>
                    <tr align="center">
                        <td align="center">
                     
                         <input type="button" value="Back" style="width:70px; font-size:x-small" onclick="window.history.back()" />
                         <input type="button" id="cmdPrint" value="Print" style="width:70px; font-size:x-small"  onclick="javascript:printDiv('mydiv')"/>
                            <%--<asp:Button ID="Button5" runat="server" Text="Show " Height="19px" OnClick="Button5_Click" />--%>
                        </td>
                    </tr>
            </table>
    </ContentTemplate>
    </asp:UpdatePanel>
  
</asp:Content>

