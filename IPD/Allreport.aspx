<%@ Page Title="" Language="C#" MasterPageFile="~/MasterAll/MasterPageAll.master" AutoEventWireup="true" CodeFile="Allreport.aspx.cs" Inherits="IPD_Allreport" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <style type="text/css">
    .scroll_checkboxes
    {
        height: 60px;
        width: 200px;
        padding: 0px;
        overflow: auto;
        border: 0px solid #ccc;
    }
    
     .FormText
    {
        FONT-SIZE: 12px;
        FONT-FAMILY:Arial;
    }
</style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">


<script type="text/javascript" language="javascript">
    function printDiv(divID) {
        var divElements = document.getElementById(divID).innerHTML;
        var oldPage = document.body.innerHTML;
        document.body.innerHTML = "<html><head><title></title></head><body>" + divElements + "</body>";
        window.print();
        document.body.innerHTML = oldPage;
    }

    function Calling() {
        var date = new Date();
        $("input[id$='txtDate']").datepicker({ dateFormat: 'dd/mm/yy' });

        $("input[id$='Button1']").click(function () {
            if ($("select[id$='DropDownList5']").val() == '0') {
                alert('Please Select Language  !');
                $("select[id$='DropDownList5']").focus();
                $("select[id$='DropDownList5']").addClass('textboxerr');
                return false;
            }
            else {
                $("select[id$='DropDownList5']").removeClass('textboxerr');
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

    function SetContextKey() {
        $find('AutoCompleteExtender1').set_contextKey("GFC");
    }

    function autoCompleteEx_ItemSelected(sender, args) {
        var regname = args.get_value().split('-');
        //document.getElementById("ctl00_ContentPlaceHolder1_txtname").value = regname[0];
        document.getElementById("ctl00_ContentPlaceHolder1_txtname").value = regname[0];
        document.getElementById("ctl00_ContentPlaceHolder1_txtRegno").value = regname[1];
        document.getElementById("ctl00_ContentPlaceHolder1_txtCO").value = regname[2];
        document.getElementById("ctl00_ContentPlaceHolder1_txtAddress").value = regname[3];
        
    }
        </script> 
<%--
  <asp:UpdateProgress ID="UpdateProgress1" runat="server">
    <ProgressTemplate>
    
       <div id="aaa" class="progressBackgroundFilter">          </div>
        <div id="bbb" class="processMessage"><img alt="Loading" src="../images/pwait.gif"/>
      
              </div>
    </ProgressTemplate>
    </asp:UpdateProgress>
--%>


    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
    <ContentTemplate>
        <div class="pageheader">
         <asp:Label ID="Label1" runat="server">Admission All Forms</asp:Label>
     </div>
              <div class="error">
                        <strong>
                        <asp:Label ID="lblError" runat="server" Font-Bold="True" CssClass="fadeout"></asp:Label>
                        </strong>
                        <div class="clear">
                        </div>
                    </div>
                    <div class="formbox">
                    <table style='width:100%;'>
           
        <tr>
         <td>  <label style='width:200px;'><strong>Select Language :</strong></label> </td>
          <td align="left">
              <asp:DropDownList ID="DropDownList5" runat="server" CssClass="textbox-medium1" 
                         Width="150px" AutoPostBack="True" 
                  onselectedindexchanged="DropDownList5_SelectedIndexChanged"  >
                         <asp:ListItem Value="0">-- Select --</asp:ListItem>
                         <asp:ListItem Value="1">Bengali</asp:ListItem>
                         <asp:ListItem Value="2">English</asp:ListItem>
                     </asp:DropDownList>
               </td>

           <td  align="center">
               <asp:RadioButtonList ID="RadioButtonList1" runat="server"  Width="250px"
                   RepeatDirection="Horizontal">
                   <asp:ListItem Value="0">With Header</asp:ListItem>
                   <asp:ListItem Value="1">Without Header</asp:ListItem>
               </asp:RadioButtonList>
               </td>
           </tr>
           </table>
                    </div>
                               
     <div class="formbox">
<table cellpadding="0" cellspacing="0"  title="Search">
            <tr>
        
           <td>
                             <div class="form-sec-row"> 
             <label class="pname"><strong>Name :</strong></label>
                   <asp:TextBox ID="txtname" runat="server" CssClass="textbox-medium1" Width="138px" ></asp:TextBox>
                    <asp:TextBox ID="txtRegno" runat="server" Visible="false"></asp:TextBox>
                    <cc1:AutoCompleteExtender ServiceMethod="SearchPatientName"    OnClientItemSelected="autoCompleteEx_ItemSelected"    MinimumPrefixLength="1"
           CompletionInterval="100" EnableCaching="false" 
           CompletionSetCount="10" TargetControlID="txtname"  ID="AutoCompleteExtender1" runat="server" FirstRowSelected = "false"></cc1:AutoCompleteExtender>
            </div>                  
</td>

       <td>
                             <div class="form-sec-row"> 
             <label class="pname"><strong>C/O :</strong></label> <asp:TextBox ID="txtCO" runat="server" CssClass="textbox-medium1" Width="138px" ></asp:TextBox>
            </div>                  
</td>


       <td>
                             <div class="form-sec-row"> 
             <label class="pname"><strong>Address :</strong></label> <asp:TextBox ID="txtAddress" runat="server" CssClass="textbox-medium1" Width="138px" ></asp:TextBox>
            </div>                  
</td>

       <td>
                             <div class="form-sec-row"> 
             <label class="pname"><strong>Date :</strong></label> <asp:TextBox ID="txtDate" runat="server" CssClass="textbox-medium1" Width="90px" ></asp:TextBox>
            </div>                  
</td>


<td>
                             <div class="form-sec-row"> 
             <label class="ipdList" style='width:65px;'><strong>Bed No :</strong></label><asp:DropDownList 
                                     ID="DropDownList4" runat="server" CssClass="textbox-dropdown" 
                                     AutoPostBack="True">
                        </asp:DropDownList>
            
            </div>                  
</td>
         

                <td class="style1">
                             <div class="form-sec-row"> 
           <asp:Button ID="Button1" runat="server" Text="Search"  CssClass="submit-button"  Height="28px"
                                     onclick="Button1_Click" />
            </div>                  
</td>             
                      
            </tr>

            <tr>
            <td> <br /></td></tr>
           </table>
           </div>
             <div   style='width:100%; max-height:300px; overflow:auto;'>            
            <asp:GridView id="GridView1" DataKeyNames ="PatientReg" runat="server"   SelectedRowStyle-BackColor="GreenYellow"
                 AutoGenerateColumns="False" AllowPaging="True"  Width="100%"
                           PageSize="1000" onrowcommand="GridView1_RowCommand" 
                      onpageindexchanging="GridView1_PageIndexChanging" onrowdatabound="GridView1_RowDataBound"  
                            >
                <RowStyle HorizontalAlign="Center" />
                <HeaderStyle BackColor="#29F4B8" />
                <Columns>
                
                      <asp:CommandField HeaderText="Form" SelectText="Form" 
                        ShowSelectButton="True" />
                    <asp:TemplateField HeaderText="Registration No" >
                       
                        <ItemTemplate>
                            <asp:Label ID="lblregno" runat="server" Text='<%# Bind("PatientReg") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Patient Name">
                       
                        <ItemTemplate>
                            <asp:Label ID="lblname" runat="server" Text='<%# Bind("patient_name") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    
                    <asp:TemplateField HeaderText="Bed No">
                        <ItemTemplate>                        
                            <asp:Label ID="lbladdress" runat="server" Text='<%# Bind("BedNoText") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField> 
                       
                    <asp:TemplateField HeaderText="Admission Date">
                        <ItemTemplate>                        
                            <asp:Label ID="lblphone1" runat="server" Text='<%# Bind("ADate") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                          
                
                    <asp:TemplateField HeaderText="General Report">
                        <ItemTemplate>
                
                       <div class="scroll_checkboxes">
                            <asp:CheckBoxList ID="CheckBoxList1" CssClass="FormText" RepeatColumns="1" BorderWidth="0"  RepeatDirection="Vertical"  runat="server">
                            </asp:CheckBoxList>
                            </div>
                        </ItemTemplate>
                    </asp:TemplateField>
                
   
                             <asp:TemplateField HeaderText="High Risk Report">
                        <ItemTemplate>
                                   <asp:DropDownList ID="DropDownList6" runat="server">
                            </asp:DropDownList>
                        </ItemTemplate>
                    </asp:TemplateField> 
                </Columns>
                <RowStyle BackColor="#F3F5C5" />
                <AlternatingRowStyle BackColor="#FBD7F5" />
        </asp:GridView>
         </div>
       
       
       
        <asp:Panel ID="Panel1" runat="server">
               <table  style='width:100%;'>     
        <tr>        
                        <td align="center">    <div id='mydiv' style='width:100%;'>              
                            <asp:Literal ID="ltrReport" runat="server"></asp:Literal><br />  </div>                  
                        </td>
                    </tr>
                    <tr>
                        <td align="center">
                     
                         <input type="button" value="Back" style="width:70px; font-size:x-small" onclick="window.history.back()" />
                         <input type="button" id="cmdPrint" value="Print" style="width:70px; font-size:x-small" onclick="javascript:printDiv('mydiv')"/></td>
                    </tr>
        </table>
        </asp:Panel>
        
        </ContentTemplate>
        </asp:UpdatePanel>
 

</asp:Content>

