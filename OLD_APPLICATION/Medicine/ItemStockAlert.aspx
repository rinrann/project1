<%@ Page Title="" Language="C#" MasterPageFile="~/MasterAll/MasterPageAll.master" AutoEventWireup="true" CodeFile="ItemStockAlert.aspx.cs" Inherits="Medicine_ItemStockAlert" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">



 
  <asp:UpdateProgress ID="UpdateProgress1" runat="server">
    <ProgressTemplate>
    
       <div id="aaa" class="progressBackgroundFilter">          </div>
        <div id="bbb" class="processMessage"><img alt="Loading" src="../images/pwait.gif"/>
      
              </div>
    </ProgressTemplate>
    </asp:UpdateProgress>



<script type="text/javascript" language="javascript">


    function printDiv(divID) {
        var divElements = document.getElementById(divID).innerHTML;
        var oldPage = document.body.innerHTML;
        document.body.innerHTML = "<html><head><title></title></head><body>" + divElements + "</body>";
        window.print();
        document.body.innerHTML = oldPage;
    }

    function SetContextKey() {
        $find('AutoCompleteExtender1').set_contextKey("GFC");
    }

    function autoCompleteEx_ItemSelected(sender, args) {

        var regname = args.get_value().split('~'); //alert(regname[0]);
        document.getElementById("ctl00_ContentPlaceHolder1_TextBoxicode").value = regname[1];
        document.getElementById("ctl00_ContentPlaceHolder1_TextBox8").value = regname[0];
        //document.getElementById("TextBox8").value = regname[0];
        
        $("#TextBox8").focus();
        //$("#DropDownList4").val(regname[1]);
    }
    </script>
    <div class="pageheader">
            <asp:Label ID="Label1" runat="server">Stock End Report</asp:Label>
        </div>
            <asp:UpdatePanel ID="UpdatePanel1" runat="server">
    <ContentTemplate>
        <div class="formbox">
    <center>
    <table width="100%" style='background-color:#42C25A; color:White;'>
    <tr>
    <td>   <label><strong>
                     Mfg. Company :</strong></label></td>
                     <td>
                         <asp:DropDownList ID="DropDownList1" runat="server" Width="150px" 
                             AutoPostBack="True" onselectedindexchanged="DropDownList1_SelectedIndexChanged">
                         </asp:DropDownList>
                     </td>

                      <td>   <label><strong>
                     Group :</strong></label></td>
                     <td>
                         <asp:DropDownList ID="DropDownList2" runat="server" Width="150px" 
                             AutoPostBack="True" onselectedindexchanged="DropDownList2_SelectedIndexChanged">
                         </asp:DropDownList>
                     </td>

                      <td>   <label><strong>
                     Sub Group :</strong></label></td>
                     <td>
                         <asp:DropDownList ID="DropDownList3" runat="server" Width="150px" 
                             AutoPostBack="True" onselectedindexchanged="DropDownList3_SelectedIndexChanged">
                         </asp:DropDownList>
                     </td>

                      <td>   <label><strong>
                     Medincine :</strong></label></td>
                     <td style="display:none">
                         <asp:DropDownList ID="DropDownList4" runat="server" Width="150px" 
                             AutoPostBack="True" onselectedindexchanged="DropDownList4_SelectedIndexChanged">
                         </asp:DropDownList>
                         <asp:TextBox ID="TextBoxicode" runat="server" Width="150px"  CssClass="textbox-medium1"></asp:TextBox> 
                     </td>
                    <td>  
                         
                        <asp:TextBox ID="TextBox8" runat="server" OnTextChanged="medicinechange" AutoPostBack="true" Width="150px"  CssClass="textbox-medium1" ></asp:TextBox>
                                   <cc1:AutoCompleteExtender ServiceMethod="SearchMedicineName" OnClientItemSelected="autoCompleteEx_ItemSelected" MinimumPrefixLength="1"
                                    CompletionInterval="100" EnableCaching="false" CompletionSetCount="10" TargetControlID="TextBox8" ID="AutoCompleteExtender1" runat="server" 
                                       FirstRowSelected = "false" ></cc1:AutoCompleteExtender>
                     </td>
 
    </tr>
    </table>
    </center>
    </div>
        <div class="formbox">
        <table width="100%">
          <tr>
        <td  align="center"> 
        
            <asp:RadioButtonList ID="RadioButtonList1" runat="server" Width="350px" 
                AutoPostBack="True" 
                onselectedindexchanged="RadioButtonList1_SelectedIndexChanged" 
                RepeatDirection="Horizontal">
                <asp:ListItem>With Header</asp:ListItem>
                <asp:ListItem>Without Header</asp:ListItem>
            </asp:RadioButtonList>  </td></tr>
            </table>
            </div>
               <div class="formbox">
               <div class="form-sec">
        <table width="100%">
                          <tr>        
                        <td>    <div id='mydiv'>              
                            <asp:Literal ID="ltrReport" runat="server"></asp:Literal></div>                
                        </td>
                    </tr>
                    <tr>
                        <td align="center">
                     
                         <input type="button" value="Back" style="width:70px; font-size:x-small" onclick="window.history.back()" />
                         <input type="button" id="cmdPrint" value="Print" style="width:70px; font-size:x-small" onclick="javascript:printDiv('mydiv')"/></td>
                    </tr>
            </table>
 </div>
                </div>
                </ContentTemplate>
                </asp:UpdatePanel>
</asp:Content>

