<%@ Page Language="C#" AutoEventWireup="true" CodeFile="QuickRegPopup.aspx.cs" Inherits="Medicine_QuickRegPopup" %>
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


        function CloseDialog() {
            debugger
            var arg = new Object();
            arg.NameValue = document.getElementById('HiddenField1').value;
            arg.ProfessionValue = document.getElementById('HiddenField2').value;
            var ledgerId = document.getElementById('HiddenField2').value;
            //alert(arg.ProfessionValue);
            window.returnValue = arg;
            window.opener.document.getElementById("ctl00_ContentPlaceHolder1_txtreg").value = arg.ProfessionValue;
            window.opener.document.getElementById("ctl00_ContentPlaceHolder1_hdnregno").value = arg.ProfessionValue;
            window.opener.document.getElementById("ctl00_ContentPlaceHolder1_txtPatientName").value = arg.NameValue;
            window.opener.document.getElementById("ctl00_ContentPlaceHolder1_hdnLedgerId").value = ledgerId;
            //window.opener.document.getElementById("txtreg").value = arg.NameValue;
            //window.opener.document.getElementById("hdnregno").value = arg.NameValue;
            //window.opener.document.getElementById("txtPatientName").value = arg.ProfessionValue;
            //window.opener.document.getElementById("hdnLedgerId").value = ledgerId;
            window.close();

        }

        function Button2_onclick() {
            window.close();
        }

    </script>
</head>
    <body>
    <form id="form1" runat="server">
    <div>
        <asp:ScriptManager ID="ScriptManager1" runat="server">
        </asp:ScriptManager>
            
<%--For Busy Loader .............................--%>


 
  <asp:UpdateProgress ID="UpdateProgress1" runat="server">
    <ProgressTemplate>
    
       <div id="aaa" class="progressBackgroundFilter">          </div>
        <div id="bbb" class="processMessage"><img alt="Loading" src="../images/pwait.gif"/>
      
              </div>
    </ProgressTemplate>
    </asp:UpdateProgress>


    <%--For Busy Loader End.............................--%>
        <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Always">
        <ContentTemplate>
            <div class="pageheader">
        <asp:Label ID="Label1" runat="server">Patient Registration</asp:Label>
    </div>
    <table width="290px" cellpadding="0" cellspacing="0">

    </table>
    <div class="form-sec">
        <div class="error">
            <strong><asp:Label ID="lblError" runat="server" Font-Bold="True"></asp:Label>
            </strong>
            <div class="clear"></div>
        </div>
        <div class="form-sec-row">
            <label><strong>Registration No :<span style="color:red;">*</span></strong></label>
                <asp:TextBox ID="TextBox2" runat="server" CssClass="textbox-medium1" Enabled="False" 
                ></asp:TextBox> 
            <div class="clear">  </div>
        </div>
        <div class="form-sec-row">
            <label><strong>Patient Name :<span style="color:red;">*</span></strong></label>
                <asp:TextBox ID="TextBox3" runat="server" CssClass="textbox-medium1" 
                ></asp:TextBox>
            <div class="clear"></div>
        </div>
        <div class="form-sec-row">
            <label><strong></strong></label>
                
            <div class="clear"></div>
        </div>
        <div class="form-sec-row">
            <asp:HiddenField ID="HiddenField1" runat="server" />
                <asp:HiddenField ID="HiddenField2" runat="server" />
            <asp:HiddenField ID="HiddenField3" runat="server" />
            <label><strong>&nbsp;</strong></label>
            <asp:Button ID="Button1" runat="server" Height="28px" OnClick="Button1_Click" Text="Submit" CssClass="submit-button" />
            <input id="btnClose" type="button" class="submit-button" value="Ok" style='width:75px; height:30px;'  onclick="CloseDialog();" /> 
            <%--<asp:Button ID="Button3" runat="server" Height="28px" Text="Ok" onclick="CloseDialog()" CssClass="submit-button" />--%>
            <input id="Button4" type="button" value="Cancel" class="submit-button" style='width:75px; height:30px;' onclick="return Button2_onclick()"" /> 
           <%-- <asp:Button ID="Button2" runat="server" Height="28px" Text="Cancel" onclick="return Button2_onclick()" CssClass="submit-button" />--%>
            <div class="clear"></div>
        </div>
    </div>
        </ContentTemplate>
        </asp:UpdatePanel>
    </div>
    </form>
    </body>
</html>


