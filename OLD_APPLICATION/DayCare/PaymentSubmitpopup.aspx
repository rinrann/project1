<%@ Page Language="C#" AutoEventWireup="true" CodeFile="PaymentSubmitpopup.aspx.cs" Inherits="DayCare_PaymentSubmitpopup" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
  
    <title></title> 
    <link href="../css/style.css" rel="stylesheet" type="text/css" />
    <link href="../css/MyCss.css" rel="stylesheet" type="text/css" />
    <link href="../css/jquery.ui.timepicker.css" rel="stylesheet" type="text/css" />
    <script src="../css/jquery.timepicker.min.js" type="text/javascript"></script>
    <script src="../css/jquery.timepicker.js" type="text/javascript"></script>
    <link href="../css/jquery.timepicker.css" rel="stylesheet" type="text/css" />
    <link href="../css/jquery-ui.css" rel="stylesheet" type="text/css" />
    <link href="../css/jquery-ui-1.8.19.custom.css" rel="stylesheet" type="text/css" />
    <link href="../css/calendar-blue.css" rel="stylesheet" type="text/css" />
   
   
    <script src="../Script/jquery-1.4.1.min.js" type="text/javascript"></script>
      
   
    <script src="../Script/jquery.dynDateTime.min.js" type="text/javascript"></script>
    <script src="../Script/calendar-en.min.js" type="text/javascript"></script> 
    <script type="text/javascript" src="../Script/jquery-ui-1.8.19.custom.min.js"></script>
    <script type="text/javascript" src="../Script/menu.js"></script>
    <script type="text/javascript" src="../Script/jquery.ui.timepicker.js"></script>

    <style type="text/css">
      body{
  width: 90%; 
  left: 5%; 
  
  margin-left:auto;
  margin-right:auto; 
}
    </style>
    <script type="text/javascript" >
        $(document).ready(function () {
            Sys.WebForms.PageRequestManager.getInstance().add_endRequest(EndRequestHandler);
            Sys.WebForms.PageRequestManager.getInstance().beginAsyncPostBack();
            function EndRequestHandler(sender, args) {
                var date = new Date();
                $("input[id$='txtdate']").datepicker({ dateFormat: 'dd/mm/yy' });
            }
        });

        function CloseDialog() { 
            window.close();

        }
 

    </script>
</head>
<body>
    <form id="form1" runat="server">
    <div>
       


        <asp:ScriptManager ID="ScriptManager1" runat="server">
        </asp:ScriptManager>
       
        <asp:UpdateProgress ID="UpdateProgress1" runat="server">
    <ProgressTemplate>
    
       <div id="aaa" class="progressBackgroundFilter">          </div>
        <div id="bbb" class="processMessage"><img alt="Loading" src="../images/pwait.gif"/>
      
              </div>
    </ProgressTemplate>

    </asp:UpdateProgress>

        <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Always">
        <ContentTemplate>
        <div class="formbox">
             <div class="form-sec">
   	<div class="form-sec-row">
                <label><strong>Registration No :</strong></label>
               <asp:TextBox ID="txtreg" runat="server" CssClass="textbox-medium1"  Width="150px" Enabled="false"
                   ></asp:TextBox>   
                <div class="clear">  </div>
            </div>


                  <div class="form-sec-row">
                <label><strong>Patient Name :</strong></label>
                <asp:TextBox ID="txtpname" runat="server" CssClass="textbox-medium1"   Width="150px" Enabled="False" 
                   ></asp:TextBox>
                <div class="clear"></div>
            </div>
            </div>
            </div>

               <div class="formbox">
     <div class="form-sec">

               <div class="form-sec-row">
                <label style='color:Blue'><strong>Bill Details :</strong></label>
                <div class="clear"></div>
            </div>
            <table width="60%">
            <tr>
            <td  style='width:120px;'><strong>Total Bill Amount :</strong></td>
            <td style='width:120px;'><asp:TextBox ID="TextBox2" runat="server"  Width="120px" CssClass="textbox-medium1" Enabled="False" 
                   ></asp:TextBox> </td>
            <td  style='width:120px;'><strong>Paid Amount :</strong></td>
            <td  style='width:120px;'> <asp:TextBox ID="txtpaid" runat="server"  Width="120px" CssClass="textbox-medium1" Enabled="False" 
                   ></asp:TextBox></td>
            </tr>

                <tr>
            <td> </td>
            <td style='width:120px;'> </td>
            <td  style='width:120px;'><strong>Discount :</strong></td>
            <td  style='width:120px;'> <asp:TextBox ID="TextBox8" runat="server"  Width="120px" CssClass="textbox-medium1" Enabled="False" 
                   ></asp:TextBox></td>
            </tr>
               <tr>
            <td> </td>
            <td > </td>
            <td  style='width:120px;'><strong>
                <asp:Label ID="Label1" runat="server" Text="Due :"></asp:Label></strong></td>
            <td  style='width:120px;'> <asp:TextBox ID="TextBox6" runat="server"  Width="120px" CssClass="textbox-medium1" Enabled="False" 
                   ></asp:TextBox></td>
            </tr>


                    <tr>
            <td  style='width:120px;'><strong>Total Debit :</strong></td>
            <td style='width:120px;'><asp:TextBox ID="TextBox1" runat="server"  Width="120px" CssClass="textbox-medium1" Enabled="False" 
                   ></asp:TextBox> </td>
            <td  style='width:120px;'><strong>Total Credit :</strong></td>
            <td  style='width:120px;'> <asp:TextBox ID="TextBox3" runat="server"  Width="120px" CssClass="textbox-medium1" Enabled="False" 
                   ></asp:TextBox></td>
            </tr>
            <tr>
            
            <td>  
               <input id="btnClose" type="button" class="submit-button" value="Ok" style='width:75px; height:30px;'  onclick="CloseDialog();" /> </td></tr>

            </table>
               
                
                <div class="clear"></div>
            </div>
 </div></div>

        </ContentTemplate>
        

        </asp:UpdatePanel>

    
    </div>
    </form>
</body>
</html>
