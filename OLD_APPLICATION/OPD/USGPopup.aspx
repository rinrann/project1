<%@ Page Language="C#" AutoEventWireup="true" CodeFile="USGPopup.aspx.cs" Inherits="OPD_USGPopup" %>

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
            var date = new Date();
            $("input[id$='txtdate']").datepicker({ dateFormat: 'dd/mm/yy' });
        });

        function CloseDialog() {
            var arg = new Object();
            arg.NameValue = document.getElementById('HiddenField1').value;
            arg.ProfessionValue = document.getElementById('HiddenField2').value;
            window.returnValue = arg;
            window.close();

        }

        function getregno() {
            document.getElementById("ctl00_ContentPlaceHolder1_txtdate").value = Regno;
        }

        function Button1_onclick() {
            window.close();
        }

    </script>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <asp:HiddenField ID="HiddenField1" runat="server" />
        		<div class="form-sec-row">
                <label><strong>Date :</strong></label>
                                <asp:TextBox ID="txtdate" CssClass="textbox-medium1" runat="server"></asp:TextBox>
                <div class="clear">  </div>
            </div>


         		<div class="form-sec-row">
                <label><strong>Group Name :</strong></label>
                <asp:DropDownList ID="DropDownList53" runat="server" CssClass="combo-big1" 
                    AutoPostBack="True" onselectedindexchanged="DropDownList53_SelectedIndexChanged" 
                        >
                </asp:DropDownList>
                <div class="clear">  </div>
            </div>
               <div class="form-sec-row">
                <label><strong>Sub Group Name :</strong></label>
                <asp:DropDownList ID="DropDownList54" runat="server" CssClass="combo-big1" 
                       AutoPostBack="True" onselectedindexchanged="DropDownList54_SelectedIndexChanged" 
                     >
                </asp:DropDownList>
                <div class="clear"></div>
            </div>
            <div class="form-sec-row">
                <label><strong>USG Name :</strong></label>
                 <asp:DropDownList ID="DropDownList55" runat="server" CssClass="combo-big1" 
                    AutoPostBack="True" onselectedindexchanged="DropDownList55_SelectedIndexChanged" 
                    >
                </asp:DropDownList>
                <div class="clear"></div>
            </div>
                       
                             <asp:gridview ID="Gridview11" runat="server" CssClass="grid" 
                    PagerStyle-CssClass="pgr" AutoGenerateColumns="false"  Width="100%" 
                    onselectedindexchanged="Gridview11_SelectedIndexChanged">
                    <Columns>
                        <asp:TemplateField HeaderText="ID" Visible="false">
                            <ItemTemplate>
                                <asp:Label ID="lblID" runat="server" Text='<%# Eval("ID")%>' ></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        
                        <asp:TemplateField HeaderText="Header Name">
                            <ItemTemplate>
                                <asp:Label ID="lblTestName" runat="server" Text='<%# Eval("HeaderName")%>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                            <asp:TemplateField HeaderText="Header Content" >
                            <ItemTemplate>
                                <asp:TextBox ID="txtheaderContent" runat="server" TextMode="MultiLine" Text='<%# Eval("hc")%>' Height="100px" Width="300px"></asp:TextBox>
                            </ItemTemplate>
                        </asp:TemplateField>
                               </Columns>
            
                </asp:gridview>


                          <asp:gridview ID="Gridview12" runat="server" CssClass="grid" 
                    PagerStyle-CssClass="pgr" AutoGenerateColumns="false"  Width="100%" >
                    <Columns>
                        <asp:TemplateField HeaderText="ID" Visible="false">
                            <ItemTemplate>
                                <asp:Label ID="lblID" runat="server" Text='<%# Eval("Id")%>' ></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        
                        <asp:TemplateField HeaderText="Parameter Name">
                            <ItemTemplate>
                                <asp:Label ID="lblTestName" runat="server" Text='<%# Eval("Parameter")%>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                            <asp:TemplateField HeaderText="Value"  >
                            <ItemTemplate>
                                <asp:TextBox ID="txtval" runat="server"  Text='<%# Eval("Value")%>' ></asp:TextBox>
                            </ItemTemplate>
                        </asp:TemplateField>
                               </Columns>
            
                </asp:gridview>
    </div>
    </form>
</body>
</html>
