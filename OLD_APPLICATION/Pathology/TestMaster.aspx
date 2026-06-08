<%@ Page Title="" Language="C#" MasterPageFile="~/MasterAll/MasterPageAll.master" AutoEventWireup="true" CodeFile="TestMaster.aspx.cs" Inherits="Pathology_TestMaster" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
<script type="text/javascript" language="javascript">
    function ShowDialog() {

        var rtvalue = window.open("TestPopup.aspx", "sss", "Width:800px; Height:550px; dialogLeft:250px;");
        //document.getElementById("ctl00_ContentPlaceHolder1_txtcode").value = rtvalue.NameValue;

    }


    function showStuff(id) {
        var el = document.getElementById(id);
        if (el.style.display != 'none') {
            el.style.display = 'none';
        }
        else {
            el.style.display = '';

        }
    }

    function DisableBackButton() {
        window.history.forward()
    }
    DisableBackButton();
    window.onload = DisableBackButton;
    window.onpageshow = function (evt) { if (evt.persisted) DisableBackButton() }
    window.onunload = function () { void (0) }

    function Calling() {
        $("input[id$='txtname']").focus(function () {
            $("input[id$='txtname']").addClass('textboxborder');
        });
        $("input[id$='txtname']").blur(function () {
            $("input[id$='txtname']").removeClass('textboxborder');
        });
        $("input[id$='txtcost']").focus(function () {
            $("input[id$='txtcost']").addClass('textboxborder');
        });
        $("input[id$='txtcost']").blur(function () {
            $("input[id$='txtcost']").removeClass('textboxborder');
        });

        $("input[id$='Button1']").click(function () {
            if ($("input[id$='txtname']").val() == '') {
                $("input[id$='txtname']").addClass('textboxerr');
            }

            if ($("input[id$='txtname']").val() == '') {
                alert('Please Enter Test Name !');
                $("input[id$='txtname']").focus();
                $("input[id$='txtname']").addClass('textboxerr');
                return false;
            }
            else {
                $("input[id$='txtname']").removeClass('textboxerr');
            }

            if ($("input[id$='txtcost']").val() == '') {
                $("input[id$='txtcost']").addClass('textboxerr');
            }

            if ($("input[id$='txtcost']").val() == '') {
                alert('Please Enter Test Cost!');
                $("input[id$='txtcost']").focus();
                $("input[id$='txtcost']").addClass('textboxerr');
                return false;
            }
            else {
                $("input[id$='txtcost']").removeClass('textboxerr');
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

        var regname = args.get_value().split('~');// alert(regname[0]);
        //document.getElementById("ctl00_ContentPlaceHolder1_TextBoxicode").value = regname[1];
        document.getElementById("ctl00_ContentPlaceHolder1_txtname").value = regname[0];

        $("#txtname").focus();
        //$("#DropDownList4").val(regname[1]);
    }
 </script> 
  <asp:UpdateProgress ID="UpdateProgress1" runat="server">
    <ProgressTemplate>
    
       <div id="aaa" class="progressBackgroundFilter" >          </div>
        <div id="bbb" class="processMessage"  ><img alt="Loading" src="../images/pwait.gif"/>
      
              </div>
    </ProgressTemplate>
    </asp:UpdateProgress>


    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
    <ContentTemplate>
    <div class="pageheader">
         <asp:Label ID="Label1" runat="server">InvestigationTest Master</asp:Label>
    </div>
     <div class="formbox">
      <div class="error">
                <strong><asp:Label ID="lblError" runat="server" Font-Bold="True"></asp:Label></strong>
                <div class="clear"></div>
            </div>
              <div class="form-sec-row">
                <label><strong>Department :</strong></label>
                <asp:DropDownList ID="DropDownList1" runat="server" CssClass="textbox-medium1" 
                      AutoPostBack="True" onselectedindexchanged="DropDownList1_SelectedIndexChanged">
                </asp:DropDownList>
                <div class="clear"></div>
            </div>
         <div class="form-sec-row">
                <label><strong>Test Code :</strong></label>
                <asp:TextBox ID="txtcode" runat="server" CssClass="textbox-medium1"></asp:TextBox><asp:Button
                    ID="Button2" runat="server" Text="Quick Search"  Height="28px"  CssClass="submit-buttonCheck" OnClientClick="ShowDialog();" />
                <asp:Button ID="Button3" runat="server"  Height="28px" 
                        Text="fetch" CssClass="submit-button" onclick="Button3_Click" />
             
                <div class="clear"></div>
            </div>

               <div class="form-sec-row">
                <label><strong>Test Name :</strong></label>
                <asp:TextBox ID="txtname" runat="server" CssClass="textbox-medium1"></asp:TextBox>
              <cc1:AutoCompleteExtender ServiceMethod="SearchTest" OnClientItemSelected="autoCompleteEx_ItemSelected" MinimumPrefixLength="1"
                                    CompletionInterval="100" EnableCaching="false" CompletionSetCount="10" TargetControlID="txtname" ID="AutoCompleteExtender2" runat="server" 
                                       FirstRowSelected = "false" ></cc1:AutoCompleteExtender>
                <div class="clear"></div>
            </div>

            
               <div class="form-sec-row">
                <label><strong>Cost :</strong></label>
                <asp:TextBox ID="txtcost" runat="server" CssClass="textbox-medium1"></asp:TextBox>
               
                <div class="clear"></div>
            </div>

                   <asp:Panel ID="Panel5" runat="server">
                <div class="form-sec-row">
                <label><strong>Template Name :</strong></label>
                <asp:DropDownList ID="DropDownList3" runat="server" CssClass="textbox-medium1" 
                        AutoPostBack="True" 
                        onselectedindexchanged="DropDownList3_SelectedIndexChanged" >
                </asp:DropDownList>
                <div class="clear"></div>
            </div>
              <div class="form-sec-row">
                <label><strong>Template Preview :</strong></label>
                <asp:TextBox ID="TextBox2" runat="server" CssClass="textbox-mediummul" 
                      TextMode="MultiLine" Enabled="False" 
                    ></asp:TextBox>    <div class="clear"></div></div>    
                        <div class="form-sec-row">
                <label><strong></strong></label>
                <asp:Button ID="Button10" runat="server" Text="Submit" CssClass="submit-button"  Height="28px" 
                                onclick="Button10_Click"/>  <div class="clear"></div></div>             
            </asp:Panel>
        

         <asp:Panel ID="Panel6" runat="server"> 
             <div class="form-sec-row">
                <label><strong>Type of Test :</strong></label>
                <asp:RadioButtonList ID="RadioButtonList1" runat="server"  
                    RepeatDirection="Horizontal" AutoPostBack="True" 
                    onselectedindexchanged="RadioButtonList1_SelectedIndexChanged">
                    <asp:ListItem>Single Parameter</asp:ListItem>
                    <asp:ListItem>Dual Parameter</asp:ListItem>
                    <asp:ListItem>Multiple Parameter</asp:ListItem>
                     <asp:ListItem>Complex Parameter</asp:ListItem>
                </asp:RadioButtonList>
            </div>  
            </asp:Panel>
             <asp:Panel ID="Panel1" runat="server"> 
            <table id="single" cellpadding="0" cellspacing="0" width="100%" border="1">
        <tr>
          <div class="form-sec-row">
            <td>  <label><strong>Normal Range</strong></label></td> 
           <td> <asp:TextBox ID="TextBox3" runat="server" CssClass="textbox-medium1"></asp:TextBox> </td>
             <td >  
                <asp:Button ID="Button1" runat="server" Text="Submit" CssClass="submit-button" 
                     onclick="Button1_Click"/></td>
            </div>
           
        </tr>
           </table>  
         </asp:Panel>     

         <asp:Panel ID="Panel2" runat="server">
         
         <table id="multiple" cellpadding="0" cellspacing="0" width="100%" border="1">
              <div class="form-sec-row">
              <tr>
              <td align="center">  <label><strong>Sl No</strong></label> </td>
                <td align="center">  <label><strong>Heading</strong></label> </td>
                  <td align="center">  <label><strong>Normal Range</strong></label> </td>
                     <td>&nbsp;</td>
              </tr>
                  <tr>
              <td align="center"><label><strong>1.</strong></label> </td>
               <td align="center" runat="server"> <asp:TextBox ID="TextBox19" CssClass="textbox-medium1" runat="server"></asp:TextBox> </td>
                <td align="center" runat="server"> <asp:TextBox ID="TextBox20" CssClass="textbox-medium1" runat="server"></asp:TextBox> </td>
                 
              </tr>

                   <tr>
              <td align="center"><label><strong>2.</strong></label> </td>
               <td align="center"> <asp:TextBox ID="TextBox21" CssClass="textbox-medium1" runat="server"></asp:TextBox> </td>
                <td align="center"> <asp:TextBox ID="TextBox22" CssClass="textbox-medium1" runat="server"></asp:TextBox> </td>
               
              </tr>

                   <tr>
              <td align="center"><label><strong>3.</strong></label> </td>
               <td align="center"> <asp:TextBox ID="TextBox23" CssClass="textbox-medium1" runat="server"></asp:TextBox> </td>
                <td align="center"> <asp:TextBox ID="TextBox24" CssClass="textbox-medium1" runat="server"></asp:TextBox> </td>
                 </tr>

                   <tr>
              <td align="center"><label><strong>4.</strong></label> </td>
               <td align="center"> <asp:TextBox ID="TextBox25" CssClass="textbox-medium1" runat="server"></asp:TextBox> </td>
                <td align="center"> <asp:TextBox ID="TextBox26" CssClass="textbox-medium1" runat="server"></asp:TextBox> </td>
                 </tr>

                   <tr>
              <td align="center"><label><strong>5.</strong></label> </td>
               <td align="center"> <asp:TextBox ID="TextBox27" CssClass="textbox-medium1" runat="server"></asp:TextBox> </td>
                <td align="center"> <asp:TextBox ID="TextBox28" CssClass="textbox-medium1" runat="server"></asp:TextBox> </td>
                </tr>

                    <tr>
              <td align="center"><label><strong>6.</strong></label> </td>
               <td align="center"> <asp:TextBox ID="TextBox29" CssClass="textbox-medium1" runat="server"></asp:TextBox> </td>
                <td align="center"> <asp:TextBox ID="TextBox30" CssClass="textbox-medium1" runat="server"></asp:TextBox> </td>
                </tr>

                    <tr>
              <td align="center"><label><strong>7.</strong></label> </td>
               <td align="center"> <asp:TextBox ID="TextBox31" CssClass="textbox-medium1" runat="server"></asp:TextBox> </td>
                <td align="center"> <asp:TextBox ID="TextBox32" CssClass="textbox-medium1" runat="server"></asp:TextBox> </td>
                </tr>

                    <tr>
              <td align="center"><label><strong>8.</strong></label> </td>
               <td align="center"> <asp:TextBox ID="TextBox33" CssClass="textbox-medium1" runat="server"></asp:TextBox> </td>
                <td align="center"> <asp:TextBox ID="TextBox34" CssClass="textbox-medium1" runat="server"></asp:TextBox> </td>
                </tr>

                    <tr>
              <td align="center"><label><strong>9.</strong></label> </td>
               <td align="center"> <asp:TextBox ID="TextBox35" CssClass="textbox-medium1" runat="server"></asp:TextBox> </td>
                <td align="center"> <asp:TextBox ID="TextBox36" CssClass="textbox-medium1" runat="server"></asp:TextBox> </td>
                </tr>

                    <tr>
              <td align="center"><label><strong>10.</strong></label> </td>
               <td align="center"> <asp:TextBox ID="TextBox37" CssClass="textbox-medium1" runat="server"></asp:TextBox> </td>
                <td align="center"> <asp:TextBox ID="TextBox38" CssClass="textbox-medium1" runat="server"></asp:TextBox> </td>
                </tr>
                <tr>
                <td colspan="3"><asp:Button ID="Button6" runat="server" Text="Submit" 
                           CssClass="submit-buttondtls" onclick="Button6_Click" 
               /></td>
                </tr>
             
              </div>
            </table>  
            
         </asp:Panel>
         
         <asp:Panel ID="Panel3" runat="server">
          <table id="complex1" cellpadding="0" cellspacing="0" width="950px" border="1">
        <tr style='width:100%;'>
          <div class="form-sec-row">
          <td style='width:5%; background-color:#9EE0F3'>  <label><strong>Heading</strong></label></td>
            <td style='width:5%;'> <asp:TextBox ID="TextBox501" runat="server" CssClass="textbox-medium1"></asp:TextBox></td>
              <td style='width:5%;background-color:#9EE0F3'>  <label><strong>Normal Range</strong></label></td>
            <td style='width:5%;'> <asp:TextBox ID="TextBox502" runat="server" CssClass="textbox-medium1"></asp:TextBox></td>
             <td style='width:2%;'>  <asp:Button ID="Button4" runat="server" CssClass="submit-button" Width="100px" Text="Show sub" OnClientClick="showStuff('row1'); return false;"/></td>
             </div>
           
        </tr>
        </table >
        <span id="row1" style="display: none;">
        <table cellpadding="0" cellspacing="0" width="950px" border="1">
       
                <tr style='width:100%'> <div class="form-sec-row"> 
             
                 <td style='width:5%;background-color:#FBF9C1;' align="center">  <label><strong>Sub Heading</strong></label></td>
            <td style='width:5%;'> <asp:TextBox ID="TextBox503" runat="server" CssClass="textbox-medium1"></asp:TextBox></td>
           <td style='width:5%;background-color:#FBF9C1;' align="center">   <label><strong>Normal Range</strong></label> </td>
             <td style='width:5%;'> <asp:TextBox ID="TextBox504" CssClass="textbox-medium1" runat="server"></asp:TextBox></td>
             <td style='width:5%;'></td></tr>

            <tr style='width:100%'> <div class="form-sec-row">       
                 <td style='width:5%;background-color:#FBF9C1;' align="center">  <label><strong>Sub Heading</strong></label></td>
            <td style='width:5%;'> <asp:TextBox ID="TextBox505" runat="server" CssClass="textbox-medium1"></asp:TextBox></td>
           <td style='width:5%;background-color:#FBF9C1;' align="center">   <label><strong>Normal Range</strong></label> </td>
             <td style='width:5%;'> <asp:TextBox ID="TextBox506" CssClass="textbox-medium1" runat="server"></asp:TextBox></td>
             <td style='width:5%;'></td>
             </tr>
          
          <tr style='width:100%'> <div class="form-sec-row"> 
         <td style='width:5%;background-color:#FBF9C1;' align="center">  <label><strong>Sub Heading</strong></label></td>
            <td style='width:5%;'> <asp:TextBox ID="TextBox507" runat="server" CssClass="textbox-medium1"></asp:TextBox></td>
           <td style='width:5%;background-color:#FBF9C1;' align="center">   <label><strong>Normal Range</strong></label> </td>
             <td style='width:5%;'> <asp:TextBox ID="TextBox508" CssClass="textbox-medium1" runat="server"></asp:TextBox></td>
             <td style='width:5%;'>
             </td></tr>
           

                 <tr style='width:100%'> <div class="form-sec-row"> 
             
                 <td style='width:5%;background-color:#FBF9C1;' align="center">  <label><strong>Sub Heading</strong></label></td>
            <td style='width:5%;'> <asp:TextBox ID="TextBox509" runat="server" CssClass="textbox-medium1"></asp:TextBox></td>
           <td style='width:5%;background-color:#FBF9C1;' align="center">   <label><strong>Normal Range</strong></label> </td>
             <td style='width:5%;'> <asp:TextBox ID="TextBox510" CssClass="textbox-medium1" runat="server"></asp:TextBox></td>
             <td style='width:5%;'></td></tr>
            

                 <tr style='width:100%'> <div class="form-sec-row"> 
             
                 <td style='width:5%;background-color:#FBF9C1;' align="center">  <label><strong>Sub Heading</strong></label></td>
            <td style='width:5%;'> <asp:TextBox ID="TextBox511" runat="server" CssClass="textbox-medium1"></asp:TextBox></td>
           <td style='width:5%;background-color:#FBF9C1;' align="center">   <label><strong>Normal Range</strong></label> </td>
             <td style='width:5%;'> <asp:TextBox ID="TextBox512" CssClass="textbox-medium1" runat="server"></asp:TextBox></td></tr>

              <tr style='width:100%'> <div class="form-sec-row"> 
             
                 <td style='width:5%;background-color:#FBF9C1;' align="center">  <label><strong>Sub Heading</strong></label></td>
            <td style='width:5%;'> <asp:TextBox ID="TextBox513" runat="server" CssClass="textbox-medium1"></asp:TextBox></td>
           <td style='width:5%;background-color:#FBF9C1;' align="center">   <label><strong>Normal Range</strong></label> </td>
             <td style='width:5%;'> <asp:TextBox ID="TextBox514" CssClass="textbox-medium1" runat="server"></asp:TextBox></td></tr>

              <tr style='width:100%'> <div class="form-sec-row"> 
             
                 <td style='width:5%;background-color:#FBF9C1;' align="center">  <label><strong>Sub Heading</strong></label></td>
            <td style='width:5%;'> <asp:TextBox ID="TextBox515" runat="server" CssClass="textbox-medium1"></asp:TextBox></td>
           <td style='width:5%;background-color:#FBF9C1;' align="center">   <label><strong>Normal Range</strong></label> </td>
             <td style='width:5%;'> <asp:TextBox ID="TextBox516" CssClass="textbox-medium1" runat="server"></asp:TextBox></td></tr>

              <tr style='width:100%'> <div class="form-sec-row"> 
             
                 <td style='width:5%;background-color:#FBF9C1;' align="center">  <label><strong>Sub Heading</strong></label></td>
            <td style='width:5%;'> <asp:TextBox ID="TextBox517" runat="server" CssClass="textbox-medium1"></asp:TextBox></td>
           <td style='width:5%;background-color:#FBF9C1;' align="center">   <label><strong>Normal Range</strong></label> </td>
             <td style='width:5%;'> <asp:TextBox ID="TextBox518" CssClass="textbox-medium1" runat="server"></asp:TextBox></td></tr>

              <tr style='width:100%'> <div class="form-sec-row"> 
             
                 <td style='width:5%;background-color:#FBF9C1;' align="center">  <label><strong>Sub Heading</strong></label></td>
            <td style='width:5%;'> <asp:TextBox ID="TextBox519" runat="server" CssClass="textbox-medium1"></asp:TextBox></td>
           <td style='width:5%;background-color:#FBF9C1;' align="center">   <label><strong>Normal Range</strong></label> </td>
             <td style='width:5%;'> <asp:TextBox ID="TextBox520" CssClass="textbox-medium1" runat="server"></asp:TextBox></td></tr>

              <tr style='width:100%'> <div class="form-sec-row"> 
             
                 <td style='width:5%;background-color:#FBF9C1;' align="center">  <label><strong>Sub Heading</strong></label></td>
            <td style='width:5%;'> <asp:TextBox ID="TextBox521" runat="server" CssClass="textbox-medium1"></asp:TextBox></td>
           <td style='width:5%;background-color:#FBF9C1;' align="center">   <label><strong>Normal Range</strong></label> </td>
             <td style='width:5%;'> <asp:TextBox ID="TextBox522" CssClass="textbox-medium1" runat="server"></asp:TextBox></td></tr>
           </table> 
          </span>

                <table id="Table1" cellpadding="0" cellspacing="0" width="950px" border="1">
        <tr style='width:100%;'>
          <div class="form-sec-row">
          <td style='width:5%;background-color:#9EE0F3'>  <label><strong>Heading</strong></label></td>
            <td style='width:5%;'> <asp:TextBox ID="TextBox523" runat="server" CssClass="textbox-medium1"></asp:TextBox></td>
              <td style='width:5%;background-color:#9EE0F3'>  <label><strong>Normal Range</strong></label></td>
            <td style='width:5%;'> <asp:TextBox ID="TextBox524" runat="server" CssClass="textbox-medium1"></asp:TextBox></td>
             <td style='width:2%;'>  <asp:Button ID="Button5" runat="server" CssClass="submit-button" Text="Show sub" OnClientClick="showStuff('Span1'); return false;"/></td>
             
           
           
            </div>
           
        </tr>

          </table >
        <span id="Span1"  style="display: none;" >
        <table cellpadding="0" cellspacing="0" width="950px" border="1">
      
                <tr style='width:100%'> <div class="form-sec-row"> 
             
                 <td style='width:5%;background-color:#FBF9C1;' align="center">  <label><strong>Sub Heading</strong></label></td>
            <td style='width:5%;'> <asp:TextBox ID="TextBox525" runat="server" CssClass="textbox-medium1"></asp:TextBox></td>
           <td style='width:5%;background-color:#FBF9C1;' align="center">   <label><strong>Normal Range</strong></label> </td>
             <td style='width:5%;'> <asp:TextBox ID="TextBox526" CssClass="textbox-medium1" runat="server"></asp:TextBox></td>
             <td style='width:5%;'></td></tr>

            <tr style='width:100%'> <div class="form-sec-row">       
                 <td style='width:5%;background-color:#FBF9C1;' align="center">  <label><strong>Sub Heading</strong></label></td>
            <td style='width:5%;'> <asp:TextBox ID="TextBox527" runat="server" CssClass="textbox-medium1"></asp:TextBox></td>
           <td style='width:5%;background-color:#FBF9C1;' align="center">   <label><strong>Normal Range</strong></label> </td>
             <td style='width:5%;'> <asp:TextBox ID="TextBox528" CssClass="textbox-medium1" runat="server"></asp:TextBox></td>
             <td style='width:5%;'></td>
             </tr>
          
          <tr style='width:100%'> <div class="form-sec-row"> 
         <td style='width:5%;background-color:#FBF9C1;' align="center">  <label><strong>Sub Heading</strong></label></td>
            <td style='width:5%;'> <asp:TextBox ID="TextBox529" runat="server" CssClass="textbox-medium1"></asp:TextBox></td>
           <td style='width:5%;background-color:#FBF9C1;' align="center">   <label><strong>Normal Range</strong></label> </td>
             <td style='width:5%;'> <asp:TextBox ID="TextBox530" CssClass="textbox-medium1" runat="server"></asp:TextBox></td>
             <td style='width:5%;'>
             </td></tr>
           

                 <tr style='width:100%'> <div class="form-sec-row"> 
             
                 <td style='width:5%;background-color:#FBF9C1;' align="center">  <label><strong>Sub Heading</strong></label></td>
            <td style='width:5%;'> <asp:TextBox ID="TextBox531" runat="server" CssClass="textbox-medium1"></asp:TextBox></td>
           <td style='width:5%;background-color:#FBF9C1;' align="center">   <label><strong>Normal Range</strong></label> </td>
             <td style='width:5%;'> <asp:TextBox ID="TextBox532" CssClass="textbox-medium1" runat="server"></asp:TextBox></td>
             <td style='width:5%;'></td></tr>
            

                 <tr style='width:100%'> <div class="form-sec-row"> 
             
                 <td style='width:5%;background-color:#FBF9C1;' align="center">  <label><strong>Sub Heading</strong></label></td>
            <td style='width:5%;'> <asp:TextBox ID="TextBox533" runat="server" CssClass="textbox-medium1"></asp:TextBox></td>
           <td style='width:5%;background-color:#FBF9C1;' align="center">   <label><strong>Normal Range</strong></label> </td>
             <td style='width:5%;'> <asp:TextBox ID="TextBox534" CssClass="textbox-medium1" runat="server"></asp:TextBox></td></tr>

              <tr style='width:100%'> <div class="form-sec-row"> 
             
                 <td style='width:5%;background-color:#FBF9C1;' align="center">  <label><strong>Sub Heading</strong></label></td>
            <td style='width:5%;'> <asp:TextBox ID="TextBox535" runat="server" CssClass="textbox-medium1"></asp:TextBox></td>
           <td style='width:5%;background-color:#FBF9C1;' align="center">   <label><strong>Normal Range</strong></label> </td>
             <td style='width:5%;'> <asp:TextBox ID="TextBox536" CssClass="textbox-medium1" runat="server"></asp:TextBox></td></tr>

              <tr style='width:100%'> <div class="form-sec-row"> 
             
                 <td style='width:5%;background-color:#FBF9C1;' align="center">  <label><strong>Sub Heading</strong></label></td>
            <td style='width:5%;'> <asp:TextBox ID="TextBox537" runat="server" CssClass="textbox-medium1"></asp:TextBox></td>
           <td style='width:5%;background-color:#FBF9C1;' align="center">   <label><strong>Normal Range</strong></label> </td>
             <td style='width:5%;'> <asp:TextBox ID="TextBox538" CssClass="textbox-medium1" runat="server"></asp:TextBox></td></tr>

              <tr style='width:100%'> <div class="form-sec-row"> 
             
                 <td style='width:5%;background-color:#FBF9C1;' align="center">  <label><strong>Sub Heading</strong></label></td>
            <td style='width:5%;'> <asp:TextBox ID="TextBox539" runat="server" CssClass="textbox-medium1"></asp:TextBox></td>
           <td style='width:5%;background-color:#FBF9C1;' align="center">   <label><strong>Normal Range</strong></label> </td>
             <td style='width:5%;'> <asp:TextBox ID="TextBox540" CssClass="textbox-medium1" runat="server"></asp:TextBox></td></tr>

              <tr style='width:100%'> <div class="form-sec-row"> 
             
                 <td style='width:5%;background-color:#FBF9C1;' align="center">  <label><strong>Sub Heading</strong></label></td>
            <td style='width:5%;'> <asp:TextBox ID="TextBox541" runat="server" CssClass="textbox-medium1"></asp:TextBox></td>
           <td style='width:5%;background-color:#FBF9C1;' align="center">   <label><strong>Normal Range</strong></label> </td>
             <td style='width:5%;'> <asp:TextBox ID="TextBox542" CssClass="textbox-medium1" runat="server"></asp:TextBox></td></tr>

              <tr style='width:100%'> <div class="form-sec-row"> 
             
                 <td style='width:5%;background-color:#FBF9C1;' align="center">  <label><strong>Sub Heading</strong></label></td>
            <td style='width:5%;'> <asp:TextBox ID="TextBox543" runat="server" CssClass="textbox-medium1"></asp:TextBox></td>
           <td style='width:5%;background-color:#FBF9C1;' align="center">   <label><strong>Normal Range</strong></label> </td>
             <td style='width:5%;'> <asp:TextBox ID="TextBox544" CssClass="textbox-medium1" runat="server"></asp:TextBox></td></tr>
           </table>
           </span>
                <table id="Table2" cellpadding="0" cellspacing="0" width="950px" border="1">
        <tr style='width:100%;'>
          <div class="form-sec-row">
          <td style='width:5%;background-color:#9EE0F3'>  <label><strong>Heading</strong></label></td>
            <td style='width:5%;'> <asp:TextBox ID="TextBox545" runat="server" CssClass="textbox-medium1"></asp:TextBox></td>
              <td style='width:5%;background-color:#9EE0F3'>  <label><strong>Normal Range</strong></label></td>
            <td style='width:5%;'> <asp:TextBox ID="TextBox546" runat="server" CssClass="textbox-medium1"></asp:TextBox></td>
             <td style='width:2%;'>  <asp:Button ID="Button8" runat="server" CssClass="submit-button" Text="Show sub" OnClientClick="showStuff('Span2'); return false;"/></td>
             
           
           
            </div>
           
        </tr>
          </table >
        <span id="Span2"  style="display: none;">
        <table cellpadding="0" cellspacing="0" width="950px" border="1">
      
                <tr style='width:100%'> <div class="form-sec-row"> 
             
                 <td style='width:5%;background-color:#FBF9C1;' align="center">  <label><strong>Sub Heading</strong></label></td>
            <td style='width:5%;'> <asp:TextBox ID="TextBox547" runat="server" CssClass="textbox-medium1"></asp:TextBox></td>
           <td style='width:5%;background-color:#FBF9C1;' align="center">   <label><strong>Normal Range</strong></label> </td>
             <td style='width:5%;'> <asp:TextBox ID="TextBox548" CssClass="textbox-medium1" runat="server"></asp:TextBox></td>
             <td style='width:5%;'></td></tr>

            <tr style='width:100%'> <div class="form-sec-row">       
                 <td style='width:5%;background-color:#FBF9C1;' align="center">  <label><strong>Sub Heading</strong></label></td>
            <td style='width:5%;'> <asp:TextBox ID="TextBox549" runat="server" CssClass="textbox-medium1"></asp:TextBox></td>
           <td style='width:5%;background-color:#FBF9C1;' align="center">   <label><strong>Normal Range</strong></label> </td>
             <td style='width:5%;'> <asp:TextBox ID="TextBox550" CssClass="textbox-medium1" runat="server"></asp:TextBox></td>
             <td style='width:5%;'></td>
             </tr>
          
          <tr style='width:100%'> <div class="form-sec-row"> 
         <td style='width:5%;background-color:#FBF9C1;' align="center">  <label><strong>Sub Heading</strong></label></td>
            <td style='width:5%;'> <asp:TextBox ID="TextBox551" runat="server" CssClass="textbox-medium1"></asp:TextBox></td>
           <td style='width:5%;background-color:#FBF9C1;' align="center">   <label><strong>Normal Range</strong></label> </td>
             <td style='width:5%;'> <asp:TextBox ID="TextBox552" CssClass="textbox-medium1" runat="server"></asp:TextBox></td>
             <td style='width:5%;'>
             </td></tr>
           

                 <tr style='width:100%'> <div class="form-sec-row"> 
             
                 <td style='width:5%;background-color:#FBF9C1;' align="center">  <label><strong>Sub Heading</strong></label></td>
            <td style='width:5%;'> <asp:TextBox ID="TextBox553" runat="server" CssClass="textbox-medium1"></asp:TextBox></td>
           <td style='width:5%;background-color:#FBF9C1;' align="center">   <label><strong>Normal Range</strong></label> </td>
             <td style='width:5%;'> <asp:TextBox ID="TextBox554" CssClass="textbox-medium1" runat="server"></asp:TextBox></td>
             <td style='width:5%;'></td></tr>
            

                 <tr style='width:100%'> <div class="form-sec-row"> 
             
                 <td style='width:5%;background-color:#FBF9C1;' align="center">  <label><strong>Sub Heading</strong></label></td>
            <td style='width:5%;'> <asp:TextBox ID="TextBox555" runat="server" CssClass="textbox-medium1"></asp:TextBox></td>
           <td style='width:5%;background-color:#FBF9C1;' align="center">   <label><strong>Normal Range</strong></label> </td>
             <td style='width:5%;'> <asp:TextBox ID="TextBox556" CssClass="textbox-medium1" runat="server"></asp:TextBox></td></tr>

              <tr style='width:100%'> <div class="form-sec-row"> 
             
                 <td style='width:5%;background-color:#FBF9C1;' align="center">  <label><strong>Sub Heading</strong></label></td>
            <td style='width:5%;'> <asp:TextBox ID="TextBox557" runat="server" CssClass="textbox-medium1"></asp:TextBox></td>
           <td style='width:5%;background-color:#FBF9C1;' align="center">   <label><strong>Normal Range</strong></label> </td>
             <td style='width:5%;'> <asp:TextBox ID="TextBox558" CssClass="textbox-medium1" runat="server"></asp:TextBox></td></tr>

              <tr style='width:100%'> <div class="form-sec-row"> 
             
                 <td style='width:5%;background-color:#FBF9C1;' align="center">  <label><strong>Sub Heading</strong></label></td>
            <td style='width:5%;'> <asp:TextBox ID="TextBox559" runat="server" CssClass="textbox-medium1"></asp:TextBox></td>
           <td style='width:5%;background-color:#FBF9C1;' align="center">   <label><strong>Normal Range</strong></label> </td>
             <td style='width:5%;'> <asp:TextBox ID="TextBox560" CssClass="textbox-medium1" runat="server"></asp:TextBox></td></tr>

              <tr style='width:100%'> <div class="form-sec-row"> 
             
                 <td style='width:5%;background-color:#FBF9C1;' align="center">  <label><strong>Sub Heading</strong></label></td>
            <td style='width:5%;'> <asp:TextBox ID="TextBox561" runat="server" CssClass="textbox-medium1"></asp:TextBox></td>
           <td style='width:5%;background-color:#FBF9C1;' align="center">   <label><strong>Normal Range</strong></label> </td>
             <td style='width:5%;'> <asp:TextBox ID="TextBox562" CssClass="textbox-medium1" runat="server"></asp:TextBox></td></tr>

              <tr style='width:100%'> <div class="form-sec-row"> 
             
                 <td style='width:5%;background-color:#FBF9C1;' align="center">  <label><strong>Sub Heading</strong></label></td>
            <td style='width:5%;'> <asp:TextBox ID="TextBox563" runat="server" CssClass="textbox-medium1"></asp:TextBox></td>
           <td style='width:5%;background-color:#FBF9C1;' align="center">   <label><strong>Normal Range</strong></label> </td>
             <td style='width:5%;'> <asp:TextBox ID="TextBox564" CssClass="textbox-medium1" runat="server"></asp:TextBox></td></tr>

              <tr style='width:100%'> <div class="form-sec-row"> 
             
                 <td style='width:5%;background-color:#FBF9C1;' align="center">  <label><strong>Sub Heading</strong></label></td>
            <td style='width:5%;'> <asp:TextBox ID="TextBox565" runat="server" CssClass="textbox-medium1"></asp:TextBox></td>
           <td style='width:5%;background-color:#FBF9C1;' align="center">   <label><strong>Normal Range</strong></label> </td>
             <td style='width:5%;'> <asp:TextBox ID="TextBox566" CssClass="textbox-medium1" runat="server"></asp:TextBox></td></tr>
      </table>
      </span>
                <table id="Table3" cellpadding="0" cellspacing="0" width="950px" border="1">
        <tr style='width:100%;'>
          <div class="form-sec-row">
          <td style='width:5%;background-color:#9EE0F3'>  <label><strong>Heading</strong></label></td>
            <td style='width:5%;'> <asp:TextBox ID="TextBox567" runat="server" CssClass="textbox-medium1"></asp:TextBox></td>
              <td style='width:5%;background-color:#9EE0F3'>  <label><strong>Normal Range</strong></label></td>
            <td style='width:5%;'> <asp:TextBox ID="TextBox568" runat="server" CssClass="textbox-medium1"></asp:TextBox></td>
             <td style='width:2%;'>  <asp:Button ID="Button569" runat="server" CssClass="submit-button" Text="Show sub" OnClientClick="showStuff('Span3'); return false;" /></td>
             
           
           
            </div>
           
        </tr>
          </table >
        <span id="Span3" style="display: none;">
        <table cellpadding="0" cellspacing="0" width="950px" border="1">
      
                <tr style='width:100%'> <div class="form-sec-row"> 
             
                 <td style='width:5%;background-color:#FBF9C1;' align="center">  <label><strong>Sub Heading</strong></label></td>
            <td style='width:5%;'> <asp:TextBox ID="TextBox569" runat="server" CssClass="textbox-medium1"></asp:TextBox></td>
           <td style='width:5%;background-color:#FBF9C1;' align="center">   <label><strong>Normal Range</strong></label> </td>
             <td style='width:5%;'> <asp:TextBox ID="TextBox570" CssClass="textbox-medium1" runat="server"></asp:TextBox></td>
             <td style='width:5%;'></td></tr>

            <tr style='width:100%'> <div class="form-sec-row">       
                 <td style='width:5%;background-color:#FBF9C1;' align="center">  <label><strong>Sub Heading</strong></label></td>
            <td style='width:5%;'> <asp:TextBox ID="TextBox571" runat="server" CssClass="textbox-medium1"></asp:TextBox></td>
           <td style='width:5%;background-color:#FBF9C1;' align="center">   <label><strong>Normal Range</strong></label> </td>
             <td style='width:5%;'> <asp:TextBox ID="TextBox572" CssClass="textbox-medium1" runat="server"></asp:TextBox></td>
             <td style='width:5%;'></td>
             </tr>
          
          <tr style='width:100%'> <div class="form-sec-row"> 
         <td style='width:5%;background-color:#FBF9C1;' align="center">  <label><strong>Sub Heading</strong></label></td>
            <td style='width:5%;'> <asp:TextBox ID="TextBox573" runat="server" CssClass="textbox-medium1"></asp:TextBox></td>
           <td style='width:5%;background-color:#FBF9C1;' align="center">   <label><strong>Normal Range</strong></label> </td>
             <td style='width:5%;'> <asp:TextBox ID="TextBox574" CssClass="textbox-medium1" runat="server"></asp:TextBox></td>
             <td style='width:5%;'>
             </td></tr>
           

                 <tr style='width:100%'> <div class="form-sec-row"> 
             
                 <td style='width:5%;background-color:#FBF9C1;' align="center">  <label><strong>Sub Heading</strong></label></td>
            <td style='width:5%;'> <asp:TextBox ID="TextBox575" runat="server" CssClass="textbox-medium1"></asp:TextBox></td>
           <td style='width:5%;background-color:#FBF9C1;' align="center">   <label><strong>Normal Range</strong></label> </td>
             <td style='width:5%;'> <asp:TextBox ID="TextBox576" CssClass="textbox-medium1" runat="server"></asp:TextBox></td>
             <td style='width:5%;'></td></tr>
            

                 <tr style='width:100%'> <div class="form-sec-row"> 
             
                 <td style='width:5%;background-color:#FBF9C1;' align="center">  <label><strong>Sub Heading</strong></label></td>
            <td style='width:5%;'> <asp:TextBox ID="TextBox577" runat="server" CssClass="textbox-medium1"></asp:TextBox></td>
           <td style='width:5%;background-color:#FBF9C1;' align="center">   <label><strong>Normal Range</strong></label> </td>
             <td style='width:5%;'> <asp:TextBox ID="TextBox578" CssClass="textbox-medium1" runat="server"></asp:TextBox></td></tr>

              <tr style='width:100%'> <div class="form-sec-row"> 
             
                 <td style='width:5%;background-color:#FBF9C1;' align="center">  <label><strong>Sub Heading</strong></label></td>
            <td style='width:5%;'> <asp:TextBox ID="TextBox579" runat="server" CssClass="textbox-medium1"></asp:TextBox></td>
           <td style='width:5%;background-color:#FBF9C1;' align="center">   <label><strong>Normal Range</strong></label> </td>
             <td style='width:5%;'> <asp:TextBox ID="TextBox580" CssClass="textbox-medium1" runat="server"></asp:TextBox></td></tr>

              <tr style='width:100%'> <div class="form-sec-row"> 
             
                 <td style='width:5%;background-color:#FBF9C1;' align="center">  <label><strong>Sub Heading</strong></label></td>
            <td style='width:5%;'> <asp:TextBox ID="TextBox581" runat="server" CssClass="textbox-medium1"></asp:TextBox></td>
           <td style='width:5%;background-color:#FBF9C1;' align="center">   <label><strong>Normal Range</strong></label> </td>
             <td style='width:5%;'> <asp:TextBox ID="TextBox582" CssClass="textbox-medium1" runat="server"></asp:TextBox></td></tr>

              <tr style='width:100%'> <div class="form-sec-row"> 
             
                 <td style='width:5%;background-color:#FBF9C1;' align="center">  <label><strong>Sub Heading</strong></label></td>
            <td style='width:5%;'> <asp:TextBox ID="TextBox583" runat="server" CssClass="textbox-medium1"></asp:TextBox></td>
           <td style='width:5%;background-color:#FBF9C1;' align="center">   <label><strong>Normal Range</strong></label> </td>
             <td style='width:5%;'> <asp:TextBox ID="TextBox584" CssClass="textbox-medium1" runat="server"></asp:TextBox></td></tr>

              <tr style='width:100%'> <div class="form-sec-row"> 
             
                 <td style='width:5%;background-color:#FBF9C1;' align="center">  <label><strong>Sub Heading</strong></label></td>
            <td style='width:5%;'> <asp:TextBox ID="TextBox585" runat="server" CssClass="textbox-medium1"></asp:TextBox></td>
           <td style='width:5%;background-color:#FBF9C1;' align="center">   <label><strong>Normal Range</strong></label> </td>
             <td style='width:5%;'> <asp:TextBox ID="TextBox586" CssClass="textbox-medium1" runat="server"></asp:TextBox></td></tr>

              <tr style='width:100%'> <div class="form-sec-row"> 
             
                 <td style='width:5%;background-color:#FBF9C1;' align="center">  <label><strong>Sub Heading</strong></label></td>
            <td style='width:5%;'> <asp:TextBox ID="TextBox587" runat="server" CssClass="textbox-medium1"></asp:TextBox></td>
           <td style='width:5%;background-color:#FBF9C1;' align="center">   <label><strong>Normal Range</strong></label> </td>
             <td style='width:5%;'> <asp:TextBox ID="TextBox588" CssClass="textbox-medium1" runat="server"></asp:TextBox></td></tr>
          
                     </table></span>
                      <table cellpadding="0" cellspacing="0" width="950px" border="1">
                     <tr><td colspan="4">
                         <asp:Button ID="Button12" runat="server" Text="Submit" 
                     CssClass="submit-buttondtls" onclick="Button12_Click"
                /></td></tr> </table>
         </asp:Panel>

         <asp:Panel ID="Panel4" runat="server">
         <table id="Table4" cellpadding="0" cellspacing="0" width="950px" border="1">
        <tr style='width:100%;'>
          <div class="form-sec-row">
          <td style='width:5%; background-color:#9EE0F3'>  <label><strong>Heading</strong></label></td>
            <td style='width:5%;'> <asp:TextBox ID="TextBox110" runat="server" CssClass="textbox-medium1"></asp:TextBox></td>
              <td style='width:5%;background-color:#9EE0F3'>  <label><strong>Normal Range</strong></label></td>
            <td style='width:5%;'> <asp:TextBox ID="TextBox111" runat="server" CssClass="textbox-medium1"></asp:TextBox></td>
             <td style='width:2%;'>  <asp:Button ID="Button7" runat="server" CssClass="submit-button" Text="Show sub" OnClientClick="showStuff('Span4'),showStuff('Span6'),showStuff('Span8'),showStuff('Span10'); return false;"/></td>
             </div>
           
        </tr>
        </table >
        <span id="Span4" style="display: none;">
        <table cellpadding="0" cellspacing="0" width="950px" border="1">
       
                <tr style='width:100%'> <div class="form-sec-row"> 
             
                 <td style='width:5%;background-color:#F5D9D0;' align="center">  <label><strong>Sub Heading</strong></label></td>
            <td style='width:5%;'> <asp:TextBox ID="TextBox112" runat="server" CssClass="textbox-medium1"></asp:TextBox></td>
           <td style='width:5%;background-color:#F5D9D0;' align="center">   <label><strong>Normal Range</strong></label> </td>
             <td style='width:5%;'> <asp:TextBox ID="TextBox113" CssClass="textbox-medium1" runat="server"></asp:TextBox></td>
             <td style='width:5%;'><asp:Button ID="Button9" runat="server" CssClass="submit-button" Text="Show sub" OnClientClick="showStuff('Span5'); return false;"/></td></tr>
             </table>
             <span id="Span5" style="display: none;">
              <table cellpadding="0" cellspacing="0" width="950px" border="1">
            <tr style='width:100%'> <div class="form-sec-row">       
                 <td style='width:5%;background-color:#FBF9C1;' align="center">  <label><strong>Sub Heading</strong></label></td>
            <td style='width:5%;'> <asp:TextBox ID="TextBox114" runat="server" CssClass="textbox-medium1"></asp:TextBox></td>
           <td style='width:5%;background-color:#FBF9C1;' align="center">   <label><strong>Normal Range</strong></label> </td>
             <td style='width:5%;'> <asp:TextBox ID="TextBox115" CssClass="textbox-medium1" runat="server"></asp:TextBox></td>
             <td style='width:5%;'></td>
             </tr>
          
          <tr style='width:100%'> <div class="form-sec-row"> 
         <td style='width:5%;background-color:#FBF9C1;' align="center">  <label><strong>Sub Heading</strong></label></td>
            <td style='width:5%;'> <asp:TextBox ID="TextBox116" runat="server" CssClass="textbox-medium1"></asp:TextBox></td>
           <td style='width:5%;background-color:#FBF9C1;' align="center">   <label><strong>Normal Range</strong></label> </td>
             <td style='width:5%;'> <asp:TextBox ID="TextBox117" CssClass="textbox-medium1" runat="server"></asp:TextBox></td>
             <td style='width:5%;'>
             </td></tr>
           

                 <tr style='width:100%'> <div class="form-sec-row"> 
             
                 <td style='width:5%;background-color:#FBF9C1;' align="center">  <label><strong>Sub Heading</strong></label></td>
            <td style='width:5%;'> <asp:TextBox ID="TextBox118" runat="server" CssClass="textbox-medium1"></asp:TextBox></td>
           <td style='width:5%;background-color:#FBF9C1;' align="center">   <label><strong>Normal Range</strong></label> </td>
             <td style='width:5%;'> <asp:TextBox ID="TextBox119" CssClass="textbox-medium1" runat="server"></asp:TextBox></td>
             <td style='width:5%;'></td></tr>
            

                 <tr style='width:100%'> <div class="form-sec-row"> 
             
                 <td style='width:5%;background-color:#FBF9C1;' align="center">  <label><strong>Sub Heading</strong></label></td>
            <td style='width:5%;'> <asp:TextBox ID="TextBox120" runat="server" CssClass="textbox-medium1"></asp:TextBox></td>
           <td style='width:5%;background-color:#FBF9C1;' align="center">   <label><strong>Normal Range</strong></label> </td>
             <td style='width:5%;'> <asp:TextBox ID="TextBox121" CssClass="textbox-medium1" runat="server"></asp:TextBox></td></tr>

              <tr style='width:100%'> <div class="form-sec-row"> 
             
                 <td style='width:5%;background-color:#FBF9C1;' align="center">  <label><strong>Sub Heading</strong></label></td>
            <td style='width:5%;'> <asp:TextBox ID="TextBox122" runat="server" CssClass="textbox-medium1"></asp:TextBox></td>
           <td style='width:5%;background-color:#FBF9C1;' align="center">   <label><strong>Normal Range</strong></label> </td>
             <td style='width:5%;'> <asp:TextBox ID="TextBox123" CssClass="textbox-medium1" runat="server"></asp:TextBox></td></tr>
           </table> 
          </span></span>

          <span id="Span6" style="display: none;">
        <table cellpadding="0" cellspacing="0" width="950px" border="1">
       
                <tr style='width:100%'> <div class="form-sec-row"> 
             
                 <td style='width:5%;background-color:#F5D9D0;' align="center">  <label><strong>Sub Heading</strong></label></td>
            <td style='width:5%;'> <asp:TextBox ID="TextBox124" runat="server" CssClass="textbox-medium1"></asp:TextBox></td>
           <td style='width:5%;background-color:#F5D9D0;' align="center">   <label><strong>Normal Range</strong></label> </td>
             <td style='width:5%;'> <asp:TextBox ID="TextBox125" CssClass="textbox-medium1" runat="server"></asp:TextBox></td>
             <td style='width:5%;'><asp:Button ID="Button11" runat="server" CssClass="submit-button" Text="Show sub" OnClientClick="showStuff('Span7'); return false;"/></td></tr>
             </table>
             <span id="Span7" style="display: none;">
              <table cellpadding="0" cellspacing="0" width="950px" border="1">
            <tr style='width:100%'> <div class="form-sec-row">       
                 <td style='width:5%;background-color:#FBF9C1;' align="center">  <label><strong>Sub Heading</strong></label></td>
            <td style='width:5%;'> <asp:TextBox ID="TextBox126" runat="server" CssClass="textbox-medium1"></asp:TextBox></td>
           <td style='width:5%;background-color:#FBF9C1;' align="center">   <label><strong>Normal Range</strong></label> </td>
             <td style='width:5%;'> <asp:TextBox ID="TextBox127" CssClass="textbox-medium1" runat="server"></asp:TextBox></td>
             <td style='width:5%;'></td>
             </tr>
          
          <tr style='width:100%'> <div class="form-sec-row"> 
         <td style='width:5%;background-color:#FBF9C1;' align="center">  <label><strong>Sub Heading</strong></label></td>
            <td style='width:5%;'> <asp:TextBox ID="TextBox128" runat="server" CssClass="textbox-medium1"></asp:TextBox></td>
           <td style='width:5%;background-color:#FBF9C1;' align="center">   <label><strong>Normal Range</strong></label> </td>
             <td style='width:5%;'> <asp:TextBox ID="TextBox129" CssClass="textbox-medium1" runat="server"></asp:TextBox></td>
             <td style='width:5%;'>
             </td></tr>
           

                 <tr style='width:100%'> <div class="form-sec-row"> 
             
                 <td style='width:5%;background-color:#FBF9C1;' align="center">  <label><strong>Sub Heading</strong></label></td>
            <td style='width:5%;'> <asp:TextBox ID="TextBox130" runat="server" CssClass="textbox-medium1"></asp:TextBox></td>
           <td style='width:5%;background-color:#FBF9C1;' align="center">   <label><strong>Normal Range</strong></label> </td>
             <td style='width:5%;'> <asp:TextBox ID="TextBox131" CssClass="textbox-medium1" runat="server"></asp:TextBox></td>
             <td style='width:5%;'></td></tr>
            

                 <tr style='width:100%'> <div class="form-sec-row"> 
             
                 <td style='width:5%;background-color:#FBF9C1;' align="center">  <label><strong>Sub Heading</strong></label></td>
            <td style='width:5%;'> <asp:TextBox ID="TextBox132" runat="server" CssClass="textbox-medium1"></asp:TextBox></td>
           <td style='width:5%;background-color:#FBF9C1;' align="center">   <label><strong>Normal Range</strong></label> </td>
             <td style='width:5%;'> <asp:TextBox ID="TextBox133" CssClass="textbox-medium1" runat="server"></asp:TextBox></td></tr>

              <tr style='width:100%'> <div class="form-sec-row"> 
             
                 <td style='width:5%;background-color:#FBF9C1;' align="center">  <label><strong>Sub Heading</strong></label></td>
            <td style='width:5%;'> <asp:TextBox ID="TextBox134" runat="server" CssClass="textbox-medium1"></asp:TextBox></td>
           <td style='width:5%;background-color:#FBF9C1;' align="center">   <label><strong>Normal Range</strong></label> </td>
             <td style='width:5%;'> <asp:TextBox ID="TextBox135" CssClass="textbox-medium1" runat="server"></asp:TextBox></td></tr>
           </table> 
          </span></span>

         <span id="Span8" style="display: none;">
        <table cellpadding="0" cellspacing="0" width="950px" border="1">
       
                <tr style='width:100%'> <div class="form-sec-row"> 
             
                 <td style='width:5%;background-color:#F5D9D0;' align="center">  <label><strong>Sub Heading</strong></label></td>
            <td style='width:5%;'> <asp:TextBox ID="TextBox136" runat="server" CssClass="textbox-medium1"></asp:TextBox></td>
           <td style='width:5%;background-color:#F5D9D0;' align="center">   <label><strong>Normal Range</strong></label> </td>
             <td style='width:5%;'> <asp:TextBox ID="TextBox137" CssClass="textbox-medium1" runat="server"></asp:TextBox></td>
             <td style='width:5%;'><asp:Button ID="Button13" runat="server" CssClass="submit-button" Text="Show sub" OnClientClick="showStuff('Span9'); return false;"/></td></tr>
             </table>
             <span id="Span9" style="display: none;">
              <table cellpadding="0" cellspacing="0" width="950px" border="1">
            <tr style='width:100%'> <div class="form-sec-row">       
                 <td style='width:5%;background-color:#FBF9C1;' align="center">  <label><strong>Sub Heading</strong></label></td>
            <td style='width:5%;'> <asp:TextBox ID="TextBox138" runat="server" CssClass="textbox-medium1"></asp:TextBox></td>
           <td style='width:5%;background-color:#FBF9C1;' align="center">   <label><strong>Normal Range</strong></label> </td>
             <td style='width:5%;'> <asp:TextBox ID="TextBox139" CssClass="textbox-medium1" runat="server"></asp:TextBox></td>
             <td style='width:5%;'></td>
             </tr>
          
          <tr style='width:100%'> <div class="form-sec-row"> 
         <td style='width:5%;background-color:#FBF9C1;' align="center">  <label><strong>Sub Heading</strong></label></td>
            <td style='width:5%;'> <asp:TextBox ID="TextBox140" runat="server" CssClass="textbox-medium1"></asp:TextBox></td>
           <td style='width:5%;background-color:#FBF9C1;' align="center">   <label><strong>Normal Range</strong></label> </td>
             <td style='width:5%;'> <asp:TextBox ID="TextBox141" CssClass="textbox-medium1" runat="server"></asp:TextBox></td>
             <td style='width:5%;'>
             </td></tr>
           

                 <tr style='width:100%'> <div class="form-sec-row"> 
             
                 <td style='width:5%;background-color:#FBF9C1;' align="center">  <label><strong>Sub Heading</strong></label></td>
            <td style='width:5%;'> <asp:TextBox ID="TextBox142" runat="server" CssClass="textbox-medium1"></asp:TextBox></td>
           <td style='width:5%;background-color:#FBF9C1;' align="center">   <label><strong>Normal Range</strong></label> </td>
             <td style='width:5%;'> <asp:TextBox ID="TextBox143" CssClass="textbox-medium1" runat="server"></asp:TextBox></td>
             <td style='width:5%;'></td></tr>
            

                 <tr style='width:100%'> <div class="form-sec-row"> 
             
                 <td style='width:5%;background-color:#FBF9C1;' align="center">  <label><strong>Sub Heading</strong></label></td>
            <td style='width:5%;'> <asp:TextBox ID="TextBox144" runat="server" CssClass="textbox-medium1"></asp:TextBox></td>
           <td style='width:5%;background-color:#FBF9C1;' align="center">   <label><strong>Normal Range</strong></label> </td>
             <td style='width:5%;'> <asp:TextBox ID="TextBox145" CssClass="textbox-medium1" runat="server"></asp:TextBox></td></tr>

              <tr style='width:100%'> <div class="form-sec-row"> 
             
                 <td style='width:5%;background-color:#FBF9C1;' align="center">  <label><strong>Sub Heading</strong></label></td>
            <td style='width:5%;'> <asp:TextBox ID="TextBox146" runat="server" CssClass="textbox-medium1"></asp:TextBox></td>
           <td style='width:5%;background-color:#FBF9C1;' align="center">   <label><strong>Normal Range</strong></label> </td>
             <td style='width:5%;'> <asp:TextBox ID="TextBox147" CssClass="textbox-medium1" runat="server"></asp:TextBox></td></tr>
           </table> 
          </span></span>

          <span id="Span10" style="display: none;">
        <table cellpadding="0" cellspacing="0" width="950px" border="1">
       
                <tr style='width:100%'> <div class="form-sec-row"> 
             
                 <td style='width:5%;background-color:#F5D9D0;' align="center">  <label><strong>Sub Heading</strong></label></td>
            <td style='width:5%;'> <asp:TextBox ID="TextBox148" runat="server" CssClass="textbox-medium1"></asp:TextBox></td>
           <td style='width:5%;background-color:#F5D9D0;' align="center">   <label><strong>Normal Range</strong></label> </td>
             <td style='width:5%;'> <asp:TextBox ID="TextBox149" CssClass="textbox-medium1" runat="server"></asp:TextBox></td>
             <td style='width:5%;'><asp:Button ID="Button14" runat="server" CssClass="submit-button" Text="Show sub" OnClientClick="showStuff('Span11'); return false;"/></td></tr>
             </table>
             <span id="Span11" style="display: none;">
              <table cellpadding="0" cellspacing="0" width="950px" border="1">
            <tr style='width:100%'> <div class="form-sec-row">       
                 <td style='width:5%;background-color:#FBF9C1;' align="center">  <label><strong>Sub Heading</strong></label></td>
            <td style='width:5%;'> <asp:TextBox ID="TextBox150" runat="server" CssClass="textbox-medium1"></asp:TextBox></td>
           <td style='width:5%;background-color:#FBF9C1;' align="center">   <label><strong>Normal Range</strong></label> </td>
             <td style='width:5%;'> <asp:TextBox ID="TextBox151" CssClass="textbox-medium1" runat="server"></asp:TextBox></td>
             <td style='width:5%;'></td>
             </tr>
          
          <tr style='width:100%'> <div class="form-sec-row"> 
         <td style='width:5%;background-color:#FBF9C1;' align="center">  <label><strong>Sub Heading</strong></label></td>
            <td style='width:5%;'> <asp:TextBox ID="TextBox152" runat="server" CssClass="textbox-medium1"></asp:TextBox></td>
           <td style='width:5%;background-color:#FBF9C1;' align="center">   <label><strong>Normal Range</strong></label> </td>
             <td style='width:5%;'> <asp:TextBox ID="TextBox153" CssClass="textbox-medium1" runat="server"></asp:TextBox></td>
             <td style='width:5%;'>
             </td></tr>
           

                 <tr style='width:100%'> <div class="form-sec-row"> 
             
                 <td style='width:5%;background-color:#FBF9C1;' align="center">  <label><strong>Sub Heading</strong></label></td>
            <td style='width:5%;'> <asp:TextBox ID="TextBox154" runat="server" CssClass="textbox-medium1"></asp:TextBox></td>
           <td style='width:5%;background-color:#FBF9C1;' align="center">   <label><strong>Normal Range</strong></label> </td>
             <td style='width:5%;'> <asp:TextBox ID="TextBox155" CssClass="textbox-medium1" runat="server"></asp:TextBox></td>
             <td style='width:5%;'></td></tr>
            

                 <tr style='width:100%'> <div class="form-sec-row"> 
             
                 <td style='width:5%;background-color:#FBF9C1;' align="center">  <label><strong>Sub Heading</strong></label></td>
            <td style='width:5%;'> <asp:TextBox ID="TextBox156" runat="server" CssClass="textbox-medium1"></asp:TextBox></td>
           <td style='width:5%;background-color:#FBF9C1;' align="center">   <label><strong>Normal Range</strong></label> </td>
             <td style='width:5%;'> <asp:TextBox ID="TextBox157" CssClass="textbox-medium1" runat="server"></asp:TextBox></td></tr>

              <tr style='width:100%'> <div class="form-sec-row"> 
             
                 <td style='width:5%;background-color:#FBF9C1;' align="center">  <label><strong>Sub Heading</strong></label></td>
            <td style='width:5%;'> <asp:TextBox ID="TextBox158" runat="server" CssClass="textbox-medium1"></asp:TextBox></td>
           <td style='width:5%;background-color:#FBF9C1;' align="center">   <label><strong>Normal Range</strong></label> </td>
             <td style='width:5%;'> <asp:TextBox ID="TextBox159" CssClass="textbox-medium1" runat="server"></asp:TextBox></td></tr>
           </table> 
          </span></span>






           <table id="Table5" cellpadding="0" cellspacing="0" width="950px" border="1">
        <tr style='width:100%;'>
          <div class="form-sec-row">
          <td style='width:5%; background-color:#9EE0F3'>  <label><strong>Heading</strong></label></td>
            <td style='width:5%;'> <asp:TextBox ID="TextBox160" runat="server" CssClass="textbox-medium1"></asp:TextBox></td>
              <td style='width:5%;background-color:#9EE0F3'>  <label><strong>Normal Range</strong></label></td>
            <td style='width:5%;'> <asp:TextBox ID="TextBox161" runat="server" CssClass="textbox-medium1"></asp:TextBox></td>
             <td style='width:2%;'>  <asp:Button ID="Button15" runat="server" CssClass="submit-button" Text="Show sub" OnClientClick="showStuff('Span12'),showStuff('Span14'),showStuff('Span16'),showStuff('Span18'); return false;"/></td>
             </div>
           
        </tr>
        </table >
        <span id="Span12" style="display: none;">
        <table cellpadding="0" cellspacing="0" width="950px" border="1">
       
                <tr style='width:100%'> <div class="form-sec-row"> 
             
                 <td style='width:5%;background-color:#F5D9D0;' align="center">  <label><strong>Sub Heading</strong></label></td>
            <td style='width:5%;'> <asp:TextBox ID="TextBox162" runat="server" CssClass="textbox-medium1"></asp:TextBox></td>
           <td style='width:5%;background-color:#F5D9D0;' align="center">   <label><strong>Normal Range</strong></label> </td>
             <td style='width:5%;'> <asp:TextBox ID="TextBox163" CssClass="textbox-medium1" runat="server"></asp:TextBox></td>
             <td style='width:5%;'><asp:Button ID="Button16" runat="server" CssClass="submit-button" Text="Show sub" OnClientClick="showStuff('Span13'); return false;"/></td></tr>
             </table>
             <span id="Span13" style="display: none;">
              <table cellpadding="0" cellspacing="0" width="950px" border="1">
            <tr style='width:100%'> <div class="form-sec-row">       
                 <td style='width:5%;background-color:#FBF9C1;' align="center">  <label><strong>Sub Heading</strong></label></td>
            <td style='width:5%;'> <asp:TextBox ID="TextBox164" runat="server" CssClass="textbox-medium1"></asp:TextBox></td>
           <td style='width:5%;background-color:#FBF9C1;' align="center">   <label><strong>Normal Range</strong></label> </td>
             <td style='width:5%;'> <asp:TextBox ID="TextBox165" CssClass="textbox-medium1" runat="server"></asp:TextBox></td>
             <td style='width:5%;'></td>
             </tr>
          
          <tr style='width:100%'> <div class="form-sec-row"> 
         <td style='width:5%;background-color:#FBF9C1;' align="center">  <label><strong>Sub Heading</strong></label></td>
            <td style='width:5%;'> <asp:TextBox ID="TextBox166" runat="server" CssClass="textbox-medium1"></asp:TextBox></td>
           <td style='width:5%;background-color:#FBF9C1;' align="center">   <label><strong>Normal Range</strong></label> </td>
             <td style='width:5%;'> <asp:TextBox ID="TextBox167" CssClass="textbox-medium1" runat="server"></asp:TextBox></td>
             <td style='width:5%;'>
             </td></tr>
           

                 <tr style='width:100%'> <div class="form-sec-row"> 
             
                 <td style='width:5%;background-color:#FBF9C1;' align="center">  <label><strong>Sub Heading</strong></label></td>
            <td style='width:5%;'> <asp:TextBox ID="TextBox168" runat="server" CssClass="textbox-medium1"></asp:TextBox></td>
           <td style='width:5%;background-color:#FBF9C1;' align="center">   <label><strong>Normal Range</strong></label> </td>
             <td style='width:5%;'> <asp:TextBox ID="TextBox169" CssClass="textbox-medium1" runat="server"></asp:TextBox></td>
             <td style='width:5%;'></td></tr>
            

                 <tr style='width:100%'> <div class="form-sec-row"> 
             
                 <td style='width:5%;background-color:#FBF9C1;' align="center">  <label><strong>Sub Heading</strong></label></td>
            <td style='width:5%;'> <asp:TextBox ID="TextBox170" runat="server" CssClass="textbox-medium1"></asp:TextBox></td>
           <td style='width:5%;background-color:#FBF9C1;' align="center">   <label><strong>Normal Range</strong></label> </td>
             <td style='width:5%;'> <asp:TextBox ID="TextBox171" CssClass="textbox-medium1" runat="server"></asp:TextBox></td></tr>

              <tr style='width:100%'> <div class="form-sec-row"> 
             
                 <td style='width:5%;background-color:#FBF9C1;' align="center">  <label><strong>Sub Heading</strong></label></td>
            <td style='width:5%;'> <asp:TextBox ID="TextBox172" runat="server" CssClass="textbox-medium1"></asp:TextBox></td>
           <td style='width:5%;background-color:#FBF9C1;' align="center">   <label><strong>Normal Range</strong></label> </td>
             <td style='width:5%;'> <asp:TextBox ID="TextBox173" CssClass="textbox-medium1" runat="server"></asp:TextBox></td></tr>
           </table> 
          </span></span>

          <span id="Span14" style="display: none;">
        <table cellpadding="0" cellspacing="0" width="950px" border="1">
       
                <tr style='width:100%'> <div class="form-sec-row"> 
             
                 <td style='width:5%;background-color:#F5D9D0;' align="center">  <label><strong>Sub Heading</strong></label></td>
            <td style='width:5%;'> <asp:TextBox ID="TextBox174" runat="server" CssClass="textbox-medium1"></asp:TextBox></td>
           <td style='width:5%;background-color:#F5D9D0;' align="center">   <label><strong>Normal Range</strong></label> </td>
             <td style='width:5%;'> <asp:TextBox ID="TextBox175" CssClass="textbox-medium1" runat="server"></asp:TextBox></td>
             <td style='width:5%;'><asp:Button ID="Button17" runat="server" CssClass="submit-button" Text="Show sub" OnClientClick="showStuff('Span15'); return false;"/></td></tr>
             </table>
             <span id="Span15" style="display: none;">
              <table cellpadding="0" cellspacing="0" width="950px" border="1">
            <tr style='width:100%'> <div class="form-sec-row">       
                 <td style='width:5%;background-color:#FBF9C1;' align="center">  <label><strong>Sub Heading</strong></label></td>
            <td style='width:5%;'> <asp:TextBox ID="TextBox176" runat="server" CssClass="textbox-medium1"></asp:TextBox></td>
           <td style='width:5%;background-color:#FBF9C1;' align="center">   <label><strong>Normal Range</strong></label> </td>
             <td style='width:5%;'> <asp:TextBox ID="TextBox177" CssClass="textbox-medium1" runat="server"></asp:TextBox></td>
             <td style='width:5%;'></td>
             </tr>
          
          <tr style='width:100%'> <div class="form-sec-row"> 
         <td style='width:5%;background-color:#FBF9C1;' align="center">  <label><strong>Sub Heading</strong></label></td>
            <td style='width:5%;'> <asp:TextBox ID="TextBox178" runat="server" CssClass="textbox-medium1"></asp:TextBox></td>
           <td style='width:5%;background-color:#FBF9C1;' align="center">   <label><strong>Normal Range</strong></label> </td>
             <td style='width:5%;'> <asp:TextBox ID="TextBox179" CssClass="textbox-medium1" runat="server"></asp:TextBox></td>
             <td style='width:5%;'>
             </td></tr>
           

                 <tr style='width:100%'> <div class="form-sec-row"> 
             
                 <td style='width:5%;background-color:#FBF9C1;' align="center">  <label><strong>Sub Heading</strong></label></td>
            <td style='width:5%;'> <asp:TextBox ID="TextBox180" runat="server" CssClass="textbox-medium1"></asp:TextBox></td>
           <td style='width:5%;background-color:#FBF9C1;' align="center">   <label><strong>Normal Range</strong></label> </td>
             <td style='width:5%;'> <asp:TextBox ID="TextBox181" CssClass="textbox-medium1" runat="server"></asp:TextBox></td>
             <td style='width:5%;'></td></tr>
            

                 <tr style='width:100%'> <div class="form-sec-row"> 
             
                 <td style='width:5%;background-color:#FBF9C1;' align="center">  <label><strong>Sub Heading</strong></label></td>
            <td style='width:5%;'> <asp:TextBox ID="TextBox182" runat="server" CssClass="textbox-medium1"></asp:TextBox></td>
           <td style='width:5%;background-color:#FBF9C1;' align="center">   <label><strong>Normal Range</strong></label> </td>
             <td style='width:5%;'> <asp:TextBox ID="TextBox183" CssClass="textbox-medium1" runat="server"></asp:TextBox></td></tr>

              <tr style='width:100%'> <div class="form-sec-row"> 
             
                 <td style='width:5%;background-color:#FBF9C1;' align="center">  <label><strong>Sub Heading</strong></label></td>
            <td style='width:5%;'> <asp:TextBox ID="TextBox184" runat="server" CssClass="textbox-medium1"></asp:TextBox></td>
           <td style='width:5%;background-color:#FBF9C1;' align="center">   <label><strong>Normal Range</strong></label> </td>
             <td style='width:5%;'> <asp:TextBox ID="TextBox185" CssClass="textbox-medium1" runat="server"></asp:TextBox></td></tr>
           </table> 
          </span></span>

         <span id="Span16" style="display: none;">
        <table cellpadding="0" cellspacing="0" width="950px" border="1">
       
                <tr style='width:100%'> <div class="form-sec-row"> 
             
                 <td style='width:5%;background-color:#F5D9D0;' align="center">  <label><strong>Sub Heading</strong></label></td>
            <td style='width:5%;'> <asp:TextBox ID="TextBox186" runat="server" CssClass="textbox-medium1"></asp:TextBox></td>
           <td style='width:5%;background-color:#F5D9D0;' align="center">   <label><strong>Normal Range</strong></label> </td>
             <td style='width:5%;'> <asp:TextBox ID="TextBox187" CssClass="textbox-medium1" runat="server"></asp:TextBox></td>
             <td style='width:5%;'><asp:Button ID="Button18" runat="server" CssClass="submit-button" Text="Show sub" OnClientClick="showStuff('Span17'); return false;"/></td></tr>
             </table>
             <span id="Span17" style="display: none;">
              <table cellpadding="0" cellspacing="0" width="950px" border="1">
            <tr style='width:100%'> <div class="form-sec-row">       
                 <td style='width:5%;background-color:#FBF9C1;' align="center">  <label><strong>Sub Heading</strong></label></td>
            <td style='width:5%;'> <asp:TextBox ID="TextBox188" runat="server" CssClass="textbox-medium1"></asp:TextBox></td>
           <td style='width:5%;background-color:#FBF9C1;' align="center">   <label><strong>Normal Range</strong></label> </td>
             <td style='width:5%;'> <asp:TextBox ID="TextBox189" CssClass="textbox-medium1" runat="server"></asp:TextBox></td>
             <td style='width:5%;'></td>
             </tr>
          
          <tr style='width:100%'> <div class="form-sec-row"> 
         <td style='width:5%;background-color:#FBF9C1;' align="center">  <label><strong>Sub Heading</strong></label></td>
            <td style='width:5%;'> <asp:TextBox ID="TextBox190" runat="server" CssClass="textbox-medium1"></asp:TextBox></td>
           <td style='width:5%;background-color:#FBF9C1;' align="center">   <label><strong>Normal Range</strong></label> </td>
             <td style='width:5%;'> <asp:TextBox ID="TextBox191" CssClass="textbox-medium1" runat="server"></asp:TextBox></td>
             <td style='width:5%;'>
             </td></tr>
           

                 <tr style='width:100%'> <div class="form-sec-row"> 
             
                 <td style='width:5%;background-color:#FBF9C1;' align="center">  <label><strong>Sub Heading</strong></label></td>
            <td style='width:5%;'> <asp:TextBox ID="TextBox192" runat="server" CssClass="textbox-medium1"></asp:TextBox></td>
           <td style='width:5%;background-color:#FBF9C1;' align="center">   <label><strong>Normal Range</strong></label> </td>
             <td style='width:5%;'> <asp:TextBox ID="TextBox193" CssClass="textbox-medium1" runat="server"></asp:TextBox></td>
             <td style='width:5%;'></td></tr>
            

                 <tr style='width:100%'> <div class="form-sec-row"> 
             
                 <td style='width:5%;background-color:#FBF9C1;' align="center">  <label><strong>Sub Heading</strong></label></td>
            <td style='width:5%;'> <asp:TextBox ID="TextBox194" runat="server" CssClass="textbox-medium1"></asp:TextBox></td>
           <td style='width:5%;background-color:#FBF9C1;' align="center">   <label><strong>Normal Range</strong></label> </td>
             <td style='width:5%;'> <asp:TextBox ID="TextBox195" CssClass="textbox-medium1" runat="server"></asp:TextBox></td></tr>

              <tr style='width:100%'> <div class="form-sec-row"> 
             
                 <td style='width:5%;background-color:#FBF9C1;' align="center">  <label><strong>Sub Heading</strong></label></td>
            <td style='width:5%;'> <asp:TextBox ID="TextBox196" runat="server" CssClass="textbox-medium1"></asp:TextBox></td>
           <td style='width:5%;background-color:#FBF9C1;' align="center">   <label><strong>Normal Range</strong></label> </td>
             <td style='width:5%;'> <asp:TextBox ID="TextBox197" CssClass="textbox-medium1" runat="server"></asp:TextBox></td></tr>
           </table> 
          </span></span>

          <span id="Span18" style="display: none;">
        <table cellpadding="0" cellspacing="0" width="950px" border="1">
       
                <tr style='width:100%'> <div class="form-sec-row"> 
             
                 <td style='width:5%;background-color:#F5D9D0;' align="center">  <label><strong>Sub Heading</strong></label></td>
            <td style='width:5%;'> <asp:TextBox ID="TextBox198" runat="server" CssClass="textbox-medium1"></asp:TextBox></td>
           <td style='width:5%;background-color:#F5D9D0;' align="center">   <label><strong>Normal Range</strong></label> </td>
             <td style='width:5%;'> <asp:TextBox ID="TextBox199" CssClass="textbox-medium1" runat="server"></asp:TextBox></td>
             <td style='width:5%;'><asp:Button ID="Button19" runat="server" CssClass="submit-button" Text="Show sub" OnClientClick="showStuff('Span19'); return false;"/></td></tr>
             </table>
             <span id="Span19" style="display: none;">
              <table cellpadding="0" cellspacing="0" width="950px" border="1">
            <tr style='width:100%'> <div class="form-sec-row">       
                 <td style='width:5%;background-color:#FBF9C1;' align="center">  <label><strong>Sub Heading</strong></label></td>
            <td style='width:5%;'> <asp:TextBox ID="TextBox200" runat="server" CssClass="textbox-medium1"></asp:TextBox></td>
           <td style='width:5%;background-color:#FBF9C1;' align="center">   <label><strong>Normal Range</strong></label> </td>
             <td style='width:5%;'> <asp:TextBox ID="TextBox201" CssClass="textbox-medium1" runat="server"></asp:TextBox></td>
             <td style='width:5%;'></td>
             </tr>
          
          <tr style='width:100%'> <div class="form-sec-row"> 
         <td style='width:5%;background-color:#FBF9C1;' align="center">  <label><strong>Sub Heading</strong></label></td>
            <td style='width:5%;'> <asp:TextBox ID="TextBox202" runat="server" CssClass="textbox-medium1"></asp:TextBox></td>
           <td style='width:5%;background-color:#FBF9C1;' align="center">   <label><strong>Normal Range</strong></label> </td>
             <td style='width:5%;'> <asp:TextBox ID="TextBox203" CssClass="textbox-medium1" runat="server"></asp:TextBox></td>
             <td style='width:5%;'>
             </td></tr>
           

                 <tr style='width:100%'> <div class="form-sec-row"> 
             
                 <td style='width:5%;background-color:#FBF9C1;' align="center">  <label><strong>Sub Heading</strong></label></td>
            <td style='width:5%;'> <asp:TextBox ID="TextBox204" runat="server" CssClass="textbox-medium1"></asp:TextBox></td>
           <td style='width:5%;background-color:#FBF9C1;' align="center">   <label><strong>Normal Range</strong></label> </td>
             <td style='width:5%;'> <asp:TextBox ID="TextBox205" CssClass="textbox-medium1" runat="server"></asp:TextBox></td>
             <td style='width:5%;'></td></tr>
            

                 <tr style='width:100%'> <div class="form-sec-row"> 
             
                 <td style='width:5%;background-color:#FBF9C1;' align="center">  <label><strong>Sub Heading</strong></label></td>
            <td style='width:5%;'> <asp:TextBox ID="TextBox206" runat="server" CssClass="textbox-medium1"></asp:TextBox></td>
           <td style='width:5%;background-color:#FBF9C1;' align="center">   <label><strong>Normal Range</strong></label> </td>
             <td style='width:5%;'> <asp:TextBox ID="TextBox207" CssClass="textbox-medium1" runat="server"></asp:TextBox></td></tr>

              <tr style='width:100%'> <div class="form-sec-row"> 
             
                 <td style='width:5%;background-color:#FBF9C1;' align="center">  <label><strong>Sub Heading</strong></label></td>
            <td style='width:5%;'> <asp:TextBox ID="TextBox208" runat="server" CssClass="textbox-medium1"></asp:TextBox></td>
           <td style='width:5%;background-color:#FBF9C1;' align="center">   <label><strong>Normal Range</strong></label> </td>
             <td style='width:5%;'> <asp:TextBox ID="TextBox209" CssClass="textbox-medium1" runat="server"></asp:TextBox></td></tr>
           </table> 
          </span></span>

















          


           <table id="Table6" cellpadding="0" cellspacing="0" width="950px" border="1">
        <tr style='width:100%;'>
          <div class="form-sec-row">
          <td style='width:5%; background-color:#9EE0F3'>  <label><strong>Heading</strong></label></td>
            <td style='width:5%;'> <asp:TextBox ID="TextBox210" runat="server" CssClass="textbox-medium1"></asp:TextBox></td>
              <td style='width:5%;background-color:#9EE0F3'>  <label><strong>Normal Range</strong></label></td>
            <td style='width:5%;'> <asp:TextBox ID="TextBox211" runat="server" CssClass="textbox-medium1"></asp:TextBox></td>
             <td style='width:2%;'>  <asp:Button ID="Button20" runat="server" CssClass="submit-button" Text="Show sub" OnClientClick="showStuff('Span20'),showStuff('Span22'),showStuff('Span24'),showStuff('Span26'); return false;"/></td>
             </div>
           
        </tr>
        </table >
        <span id="Span20" style="display: none;">
        <table cellpadding="0" cellspacing="0" width="950px" border="1">
       
                <tr style='width:100%'> <div class="form-sec-row"> 
             
                 <td style='width:5%;background-color:#F5D9D0;' align="center">  <label><strong>Sub Heading</strong></label></td>
            <td style='width:5%;'> <asp:TextBox ID="TextBox212" runat="server" CssClass="textbox-medium1"></asp:TextBox></td>
           <td style='width:5%;background-color:#F5D9D0;' align="center">   <label><strong>Normal Range</strong></label> </td>
             <td style='width:5%;'> <asp:TextBox ID="TextBox213" CssClass="textbox-medium1" runat="server"></asp:TextBox></td>
             <td style='width:5%;'><asp:Button ID="Button21" runat="server" CssClass="submit-button" Text="Show sub" OnClientClick="showStuff('Span21'); return false;"/></td></tr>
             </table>
             <span id="Span21" style="display: none;">
              <table cellpadding="0" cellspacing="0" width="950px" border="1">
            <tr style='width:100%'> <div class="form-sec-row">       
                 <td style='width:5%;background-color:#FBF9C1;' align="center">  <label><strong>Sub Heading</strong></label></td>
            <td style='width:5%;'> <asp:TextBox ID="TextBox214" runat="server" CssClass="textbox-medium1"></asp:TextBox></td>
           <td style='width:5%;background-color:#FBF9C1;' align="center">   <label><strong>Normal Range</strong></label> </td>
             <td style='width:5%;'> <asp:TextBox ID="TextBox215" CssClass="textbox-medium1" runat="server"></asp:TextBox></td>
             <td style='width:5%;'></td>
             </tr>
          
          <tr style='width:100%'> <div class="form-sec-row"> 
         <td style='width:5%;background-color:#FBF9C1;' align="center">  <label><strong>Sub Heading</strong></label></td>
            <td style='width:5%;'> <asp:TextBox ID="TextBox216" runat="server" CssClass="textbox-medium1"></asp:TextBox></td>
           <td style='width:5%;background-color:#FBF9C1;' align="center">   <label><strong>Normal Range</strong></label> </td>
             <td style='width:5%;'> <asp:TextBox ID="TextBox217" CssClass="textbox-medium1" runat="server"></asp:TextBox></td>
             <td style='width:5%;'>
             </td></tr>
           

                 <tr style='width:100%'> <div class="form-sec-row"> 
             
                 <td style='width:5%;background-color:#FBF9C1;' align="center">  <label><strong>Sub Heading</strong></label></td>
            <td style='width:5%;'> <asp:TextBox ID="TextBox218" runat="server" CssClass="textbox-medium1"></asp:TextBox></td>
           <td style='width:5%;background-color:#FBF9C1;' align="center">   <label><strong>Normal Range</strong></label> </td>
             <td style='width:5%;'> <asp:TextBox ID="TextBox219" CssClass="textbox-medium1" runat="server"></asp:TextBox></td>
             <td style='width:5%;'></td></tr>
            

                 <tr style='width:100%'> <div class="form-sec-row"> 
             
                 <td style='width:5%;background-color:#FBF9C1;' align="center">  <label><strong>Sub Heading</strong></label></td>
            <td style='width:5%;'> <asp:TextBox ID="TextBox220" runat="server" CssClass="textbox-medium1"></asp:TextBox></td>
           <td style='width:5%;background-color:#FBF9C1;' align="center">   <label><strong>Normal Range</strong></label> </td>
             <td style='width:5%;'> <asp:TextBox ID="TextBox221" CssClass="textbox-medium1" runat="server"></asp:TextBox></td></tr>

              <tr style='width:100%'> <div class="form-sec-row"> 
             
                 <td style='width:5%;background-color:#FBF9C1;' align="center">  <label><strong>Sub Heading</strong></label></td>
            <td style='width:5%;'> <asp:TextBox ID="TextBox222" runat="server" CssClass="textbox-medium1"></asp:TextBox></td>
           <td style='width:5%;background-color:#FBF9C1;' align="center">   <label><strong>Normal Range</strong></label> </td>
             <td style='width:5%;'> <asp:TextBox ID="TextBox223" CssClass="textbox-medium1" runat="server"></asp:TextBox></td></tr>
           </table> 
          </span></span>

          <span id="Span22" style="display: none;">
        <table cellpadding="0" cellspacing="0" width="950px" border="1">
       
                <tr style='width:100%'> <div class="form-sec-row"> 
             
                 <td style='width:5%;background-color:#F5D9D0;' align="center">  <label><strong>Sub Heading</strong></label></td>
            <td style='width:5%;'> <asp:TextBox ID="TextBox224" runat="server" CssClass="textbox-medium1"></asp:TextBox></td>
           <td style='width:5%;background-color:#F5D9D0;' align="center">   <label><strong>Normal Range</strong></label> </td>
             <td style='width:5%;'> <asp:TextBox ID="TextBox225" CssClass="textbox-medium1" runat="server"></asp:TextBox></td>
             <td style='width:5%;'><asp:Button ID="Button22" runat="server" CssClass="submit-button" Text="Show sub" OnClientClick="showStuff('Span23'); return false;"/></td></tr>
             </table>
             <span id="Span23" style="display: none;">
              <table cellpadding="0" cellspacing="0" width="950px" border="1">
            <tr style='width:100%'> <div class="form-sec-row">       
                 <td style='width:5%;background-color:#FBF9C1;' align="center">  <label><strong>Sub Heading</strong></label></td>
            <td style='width:5%;'> <asp:TextBox ID="TextBox226" runat="server" CssClass="textbox-medium1"></asp:TextBox></td>
           <td style='width:5%;background-color:#FBF9C1;' align="center">   <label><strong>Normal Range</strong></label> </td>
             <td style='width:5%;'> <asp:TextBox ID="TextBox227" CssClass="textbox-medium1" runat="server"></asp:TextBox></td>
             <td style='width:5%;'></td>
             </tr>
          
          <tr style='width:100%'> <div class="form-sec-row"> 
         <td style='width:5%;background-color:#FBF9C1;' align="center">  <label><strong>Sub Heading</strong></label></td>
            <td style='width:5%;'> <asp:TextBox ID="TextBox228" runat="server" CssClass="textbox-medium1"></asp:TextBox></td>
           <td style='width:5%;background-color:#FBF9C1;' align="center">   <label><strong>Normal Range</strong></label> </td>
             <td style='width:5%;'> <asp:TextBox ID="TextBox229" CssClass="textbox-medium1" runat="server"></asp:TextBox></td>
             <td style='width:5%;'>
             </td></tr>
           

                 <tr style='width:100%'> <div class="form-sec-row"> 
             
                 <td style='width:5%;background-color:#FBF9C1;' align="center">  <label><strong>Sub Heading</strong></label></td>
            <td style='width:5%;'> <asp:TextBox ID="TextBox230" runat="server" CssClass="textbox-medium1"></asp:TextBox></td>
           <td style='width:5%;background-color:#FBF9C1;' align="center">   <label><strong>Normal Range</strong></label> </td>
             <td style='width:5%;'> <asp:TextBox ID="TextBox231" CssClass="textbox-medium1" runat="server"></asp:TextBox></td>
             <td style='width:5%;'></td></tr>
            

                 <tr style='width:100%'> <div class="form-sec-row"> 
             
                 <td style='width:5%;background-color:#FBF9C1;' align="center">  <label><strong>Sub Heading</strong></label></td>
            <td style='width:5%;'> <asp:TextBox ID="TextBox232" runat="server" CssClass="textbox-medium1"></asp:TextBox></td>
           <td style='width:5%;background-color:#FBF9C1;' align="center">   <label><strong>Normal Range</strong></label> </td>
             <td style='width:5%;'> <asp:TextBox ID="TextBox233" CssClass="textbox-medium1" runat="server"></asp:TextBox></td></tr>

              <tr style='width:100%'> <div class="form-sec-row"> 
             
                 <td style='width:5%;background-color:#FBF9C1;' align="center">  <label><strong>Sub Heading</strong></label></td>
            <td style='width:5%;'> <asp:TextBox ID="TextBox234" runat="server" CssClass="textbox-medium1"></asp:TextBox></td>
           <td style='width:5%;background-color:#FBF9C1;' align="center">   <label><strong>Normal Range</strong></label> </td>
             <td style='width:5%;'> <asp:TextBox ID="TextBox235" CssClass="textbox-medium1" runat="server"></asp:TextBox></td></tr>
           </table> 
          </span></span>

         <span id="Span24" style="display: none;">
        <table cellpadding="0" cellspacing="0" width="950px" border="1">
       
                <tr style='width:100%'> <div class="form-sec-row"> 
             
                 <td style='width:5%;background-color:#F5D9D0;' align="center">  <label><strong>Sub Heading</strong></label></td>
            <td style='width:5%;'> <asp:TextBox ID="TextBox236" runat="server" CssClass="textbox-medium1"></asp:TextBox></td>
           <td style='width:5%;background-color:#F5D9D0;' align="center">   <label><strong>Normal Range</strong></label> </td>
             <td style='width:5%;'> <asp:TextBox ID="TextBox237" CssClass="textbox-medium1" runat="server"></asp:TextBox></td>
             <td style='width:5%;'><asp:Button ID="Button23" runat="server" CssClass="submit-button" Text="Show sub" OnClientClick="showStuff('Span25'); return false;"/></td></tr>
             </table>
             <span id="Span25" style="display: none;">
              <table cellpadding="0" cellspacing="0" width="950px" border="1">
            <tr style='width:100%'> <div class="form-sec-row">       
                 <td style='width:5%;background-color:#FBF9C1;' align="center">  <label><strong>Sub Heading</strong></label></td>
            <td style='width:5%;'> <asp:TextBox ID="TextBox238" runat="server" CssClass="textbox-medium1"></asp:TextBox></td>
           <td style='width:5%;background-color:#FBF9C1;' align="center">   <label><strong>Normal Range</strong></label> </td>
             <td style='width:5%;'> <asp:TextBox ID="TextBox239" CssClass="textbox-medium1" runat="server"></asp:TextBox></td>
             <td style='width:5%;'></td>
             </tr>
          
          <tr style='width:100%'> <div class="form-sec-row"> 
         <td style='width:5%;background-color:#FBF9C1;' align="center">  <label><strong>Sub Heading</strong></label></td>
            <td style='width:5%;'> <asp:TextBox ID="TextBox240" runat="server" CssClass="textbox-medium1"></asp:TextBox></td>
           <td style='width:5%;background-color:#FBF9C1;' align="center">   <label><strong>Normal Range</strong></label> </td>
             <td style='width:5%;'> <asp:TextBox ID="TextBox241" CssClass="textbox-medium1" runat="server"></asp:TextBox></td>
             <td style='width:5%;'>
             </td></tr>
           

                 <tr style='width:100%'> <div class="form-sec-row"> 
             
                 <td style='width:5%;background-color:#FBF9C1;' align="center">  <label><strong>Sub Heading</strong></label></td>
            <td style='width:5%;'> <asp:TextBox ID="TextBox242" runat="server" CssClass="textbox-medium1"></asp:TextBox></td>
           <td style='width:5%;background-color:#FBF9C1;' align="center">   <label><strong>Normal Range</strong></label> </td>
             <td style='width:5%;'> <asp:TextBox ID="TextBox243" CssClass="textbox-medium1" runat="server"></asp:TextBox></td>
             <td style='width:5%;'></td></tr>
            

                 <tr style='width:100%'> <div class="form-sec-row"> 
             
                 <td style='width:5%;background-color:#FBF9C1;' align="center">  <label><strong>Sub Heading</strong></label></td>
            <td style='width:5%;'> <asp:TextBox ID="TextBox244" runat="server" CssClass="textbox-medium1"></asp:TextBox></td>
           <td style='width:5%;background-color:#FBF9C1;' align="center">   <label><strong>Normal Range</strong></label> </td>
             <td style='width:5%;'> <asp:TextBox ID="TextBox245" CssClass="textbox-medium1" runat="server"></asp:TextBox></td></tr>

              <tr style='width:100%'> <div class="form-sec-row"> 
             
                 <td style='width:5%;background-color:#FBF9C1;' align="center">  <label><strong>Sub Heading</strong></label></td>
            <td style='width:5%;'> <asp:TextBox ID="TextBox246" runat="server" CssClass="textbox-medium1"></asp:TextBox></td>
           <td style='width:5%;background-color:#FBF9C1;' align="center">   <label><strong>Normal Range</strong></label> </td>
             <td style='width:5%;'> <asp:TextBox ID="TextBox247" CssClass="textbox-medium1" runat="server"></asp:TextBox></td></tr>
           </table> 
          </span></span>

          <span id="Span26" style="display: none;">
        <table cellpadding="0" cellspacing="0" width="950px" border="1">
       
                <tr style='width:100%'> <div class="form-sec-row"> 
             
                 <td style='width:5%;background-color:#F5D9D0;' align="center">  <label><strong>Sub Heading</strong></label></td>
            <td style='width:5%;'> <asp:TextBox ID="TextBox248" runat="server" CssClass="textbox-medium1"></asp:TextBox></td>
           <td style='width:5%;background-color:#F5D9D0;' align="center">   <label><strong>Normal Range</strong></label> </td>
             <td style='width:5%;'> <asp:TextBox ID="TextBox249" CssClass="textbox-medium1" runat="server"></asp:TextBox></td>
             <td style='width:5%;'><asp:Button ID="Button24" runat="server" CssClass="submit-button" Text="Show sub" OnClientClick="showStuff('Span27'); return false;"/></td></tr>
             </table>
             <span id="Span27" style="display: none;">
              <table cellpadding="0" cellspacing="0" width="950px" border="1">
            <tr style='width:100%'> <div class="form-sec-row">       
                 <td style='width:5%;background-color:#FBF9C1;' align="center">  <label><strong>Sub Heading</strong></label></td>
            <td style='width:5%;'> <asp:TextBox ID="TextBox250" runat="server" CssClass="textbox-medium1"></asp:TextBox></td>
           <td style='width:5%;background-color:#FBF9C1;' align="center">   <label><strong>Normal Range</strong></label> </td>
             <td style='width:5%;'> <asp:TextBox ID="TextBox251" CssClass="textbox-medium1" runat="server"></asp:TextBox></td>
             <td style='width:5%;'></td>
             </tr>
          
          <tr style='width:100%'> <div class="form-sec-row"> 
         <td style='width:5%;background-color:#FBF9C1;' align="center">  <label><strong>Sub Heading</strong></label></td>
            <td style='width:5%;'> <asp:TextBox ID="TextBox252" runat="server" CssClass="textbox-medium1"></asp:TextBox></td>
           <td style='width:5%;background-color:#FBF9C1;' align="center">   <label><strong>Normal Range</strong></label> </td>
             <td style='width:5%;'> <asp:TextBox ID="TextBox253" CssClass="textbox-medium1" runat="server"></asp:TextBox></td>
             <td style='width:5%;'>
             </td></tr>
           

                 <tr style='width:100%'> <div class="form-sec-row"> 
             
                 <td style='width:5%;background-color:#FBF9C1;' align="center">  <label><strong>Sub Heading</strong></label></td>
            <td style='width:5%;'> <asp:TextBox ID="TextBox254" runat="server" CssClass="textbox-medium1"></asp:TextBox></td>
           <td style='width:5%;background-color:#FBF9C1;' align="center">   <label><strong>Normal Range</strong></label> </td>
             <td style='width:5%;'> <asp:TextBox ID="TextBox255" CssClass="textbox-medium1" runat="server"></asp:TextBox></td>
             <td style='width:5%;'></td></tr>
            

                 <tr style='width:100%'> <div class="form-sec-row"> 
             
                 <td style='width:5%;background-color:#FBF9C1;' align="center">  <label><strong>Sub Heading</strong></label></td>
            <td style='width:5%;'> <asp:TextBox ID="TextBox256" runat="server" CssClass="textbox-medium1"></asp:TextBox></td>
           <td style='width:5%;background-color:#FBF9C1;' align="center">   <label><strong>Normal Range</strong></label> </td>
             <td style='width:5%;'> <asp:TextBox ID="TextBox257" CssClass="textbox-medium1" runat="server"></asp:TextBox></td></tr>

              <tr style='width:100%'> <div class="form-sec-row"> 
             
                 <td style='width:5%;background-color:#FBF9C1;' align="center">  <label><strong>Sub Heading</strong></label></td>
            <td style='width:5%;'> <asp:TextBox ID="TextBox258" runat="server" CssClass="textbox-medium1"></asp:TextBox></td>
           <td style='width:5%;background-color:#FBF9C1;' align="center">   <label><strong>Normal Range</strong></label> </td>
             <td style='width:5%;'> <asp:TextBox ID="TextBox259" CssClass="textbox-medium1" runat="server"></asp:TextBox></td></tr>
           </table> 
          </span></span>









            <table id="Table7" cellpadding="0" cellspacing="0" width="950px" border="1">
        <tr style='width:100%;'>
          <div class="form-sec-row">
          <td style='width:5%; background-color:#9EE0F3'>  <label><strong>Heading</strong></label></td>
            <td style='width:5%;'> <asp:TextBox ID="TextBox260" runat="server" CssClass="textbox-medium1"></asp:TextBox></td>
              <td style='width:5%;background-color:#9EE0F3'>  <label><strong>Normal Range</strong></label></td>
            <td style='width:5%;'> <asp:TextBox ID="TextBox261" runat="server" CssClass="textbox-medium1"></asp:TextBox></td>
             <td style='width:2%;'>  <asp:Button ID="Button25" runat="server" CssClass="submit-button" Text="Show sub" OnClientClick="showStuff('Span28'),showStuff('Span30'),showStuff('Span32'),showStuff('Span34'); return false;"/></td>
             </div>
           
        </tr>
        </table >
      
        <span id="Span28" style="display: none;">
        <table cellpadding="0" cellspacing="0" width="950px" border="1">
       
                <tr style='width:100%'> <div class="form-sec-row"> 
             
                 <td style='width:5%;background-color:#F5D9D0;' align="center">  <label><strong>Sub Heading</strong></label></td>
            <td style='width:5%;'> <asp:TextBox ID="TextBox262" runat="server" CssClass="textbox-medium1"></asp:TextBox></td>
           <td style='width:5%;background-color:#F5D9D0;' align="center">   <label><strong>Normal Range</strong></label> </td>
             <td style='width:5%;'> <asp:TextBox ID="TextBox263" CssClass="textbox-medium1" runat="server"></asp:TextBox></td>
             <td style='width:5%;'><asp:Button ID="Button26" runat="server" CssClass="submit-button" Text="Show sub" OnClientClick="showStuff('Span29'); return false;"/></td></tr>
             </table>
             <span id="Span29" style="display: none;">
              <table cellpadding="0" cellspacing="0" width="950px" border="1">
            <tr style='width:100%'> <div class="form-sec-row">       
                 <td style='width:5%;background-color:#FBF9C1;' align="center">  <label><strong>Sub Heading</strong></label></td>
            <td style='width:5%;'> <asp:TextBox ID="TextBox264" runat="server" CssClass="textbox-medium1"></asp:TextBox></td>
           <td style='width:5%;background-color:#FBF9C1;' align="center">   <label><strong>Normal Range</strong></label> </td>
             <td style='width:5%;'> <asp:TextBox ID="TextBox265" CssClass="textbox-medium1" runat="server"></asp:TextBox></td>
             <td style='width:5%;'></td>
             </tr>
          
          <tr style='width:100%'> <div class="form-sec-row"> 
         <td style='width:5%;background-color:#FBF9C1;' align="center">  <label><strong>Sub Heading</strong></label></td>
            <td style='width:5%;'> <asp:TextBox ID="TextBox266" runat="server" CssClass="textbox-medium1"></asp:TextBox></td>
           <td style='width:5%;background-color:#FBF9C1;' align="center">   <label><strong>Normal Range</strong></label> </td>
             <td style='width:5%;'> <asp:TextBox ID="TextBox267" CssClass="textbox-medium1" runat="server"></asp:TextBox></td>
             <td style='width:5%;'>
             </td></tr>
           

                 <tr style='width:100%'> <div class="form-sec-row"> 
             
                 <td style='width:5%;background-color:#FBF9C1;' align="center">  <label><strong>Sub Heading</strong></label></td>
            <td style='width:5%;'> <asp:TextBox ID="TextBox268" runat="server" CssClass="textbox-medium1"></asp:TextBox></td>
           <td style='width:5%;background-color:#FBF9C1;' align="center">   <label><strong>Normal Range</strong></label> </td>
             <td style='width:5%;'> <asp:TextBox ID="TextBox269" CssClass="textbox-medium1" runat="server"></asp:TextBox></td>
             <td style='width:5%;'></td></tr>
            

                 <tr style='width:100%'> <div class="form-sec-row"> 
             
                 <td style='width:5%;background-color:#FBF9C1;' align="center">  <label><strong>Sub Heading</strong></label></td>
            <td style='width:5%;'> <asp:TextBox ID="TextBox270" runat="server" CssClass="textbox-medium1"></asp:TextBox></td>
           <td style='width:5%;background-color:#FBF9C1;' align="center">   <label><strong>Normal Range</strong></label> </td>
             <td style='width:5%;'> <asp:TextBox ID="TextBox271" CssClass="textbox-medium1" runat="server"></asp:TextBox></td></tr>

              <tr style='width:100%'> <div class="form-sec-row"> 
             
                 <td style='width:5%;background-color:#FBF9C1;' align="center">  <label><strong>Sub Heading</strong></label></td>
            <td style='width:5%;'> <asp:TextBox ID="TextBox272" runat="server" CssClass="textbox-medium1"></asp:TextBox></td>
           <td style='width:5%;background-color:#FBF9C1;' align="center">   <label><strong>Normal Range</strong></label> </td>
             <td style='width:5%;'> <asp:TextBox ID="TextBox273" CssClass="textbox-medium1" runat="server"></asp:TextBox></td></tr>
           </table> 
          </span></span>

          <span id="Span30" style="display: none;">
        <table cellpadding="0" cellspacing="0" width="950px" border="1">
       
                <tr style='width:100%'> <div class="form-sec-row"> 
             
                 <td style='width:5%;background-color:#F5D9D0;' align="center">  <label><strong>Sub Heading</strong></label></td>
            <td style='width:5%;'> <asp:TextBox ID="TextBox274" runat="server" CssClass="textbox-medium1"></asp:TextBox></td>
           <td style='width:5%;background-color:#F5D9D0;' align="center">   <label><strong>Normal Range</strong></label> </td>
             <td style='width:5%;'> <asp:TextBox ID="TextBox275" CssClass="textbox-medium1" runat="server"></asp:TextBox></td>
             <td style='width:5%;'><asp:Button ID="Button27" runat="server" CssClass="submit-button" Text="Show sub" OnClientClick="showStuff('Span31'); return false;"/></td></tr>
             </table>
             <span id="Span31" style="display: none;">
              <table cellpadding="0" cellspacing="0" width="950px" border="1">
            <tr style='width:100%'> <div class="form-sec-row">       
                 <td style='width:5%;background-color:#FBF9C1;' align="center">  <label><strong>Sub Heading</strong></label></td>
            <td style='width:5%;'> <asp:TextBox ID="TextBox276" runat="server" CssClass="textbox-medium1"></asp:TextBox></td>
           <td style='width:5%;background-color:#FBF9C1;' align="center">   <label><strong>Normal Range</strong></label> </td>
             <td style='width:5%;'> <asp:TextBox ID="TextBox277" CssClass="textbox-medium1" runat="server"></asp:TextBox></td>
             <td style='width:5%;'></td>
             </tr>
          
          <tr style='width:100%'> <div class="form-sec-row"> 
         <td style='width:5%;background-color:#FBF9C1;' align="center">  <label><strong>Sub Heading</strong></label></td>
            <td style='width:5%;'> <asp:TextBox ID="TextBox278" runat="server" CssClass="textbox-medium1"></asp:TextBox></td>
           <td style='width:5%;background-color:#FBF9C1;' align="center">   <label><strong>Normal Range</strong></label> </td>
             <td style='width:5%;'> <asp:TextBox ID="TextBox279" CssClass="textbox-medium1" runat="server"></asp:TextBox></td>
             <td style='width:5%;'>
             </td></tr>
           

                 <tr style='width:100%'> <div class="form-sec-row"> 
             
                 <td style='width:5%;background-color:#FBF9C1;' align="center">  <label><strong>Sub Heading</strong></label></td>
            <td style='width:5%;'> <asp:TextBox ID="TextBox280" runat="server" CssClass="textbox-medium1"></asp:TextBox></td>
           <td style='width:5%;background-color:#FBF9C1;' align="center">   <label><strong>Normal Range</strong></label> </td>
             <td style='width:5%;'> <asp:TextBox ID="TextBox281" CssClass="textbox-medium1" runat="server"></asp:TextBox></td>
             <td style='width:5%;'></td></tr>
            

                 <tr style='width:100%'> <div class="form-sec-row"> 
             
                 <td style='width:5%;background-color:#FBF9C1;' align="center">  <label><strong>Sub Heading</strong></label></td>
            <td style='width:5%;'> <asp:TextBox ID="TextBox282" runat="server" CssClass="textbox-medium1"></asp:TextBox></td>
           <td style='width:5%;background-color:#FBF9C1;' align="center">   <label><strong>Normal Range</strong></label> </td>
             <td style='width:5%;'> <asp:TextBox ID="TextBox283" CssClass="textbox-medium1" runat="server"></asp:TextBox></td></tr>

              <tr style='width:100%'> <div class="form-sec-row"> 
             
                 <td style='width:5%;background-color:#FBF9C1;' align="center">  <label><strong>Sub Heading</strong></label></td>
            <td style='width:5%;'> <asp:TextBox ID="TextBox284" runat="server" CssClass="textbox-medium1"></asp:TextBox></td>
           <td style='width:5%;background-color:#FBF9C1;' align="center">   <label><strong>Normal Range</strong></label> </td>
             <td style='width:5%;'> <asp:TextBox ID="TextBox285" CssClass="textbox-medium1" runat="server"></asp:TextBox></td></tr>
           </table> 
          </span></span>

         <span id="Span32" style="display: none;">
        <table cellpadding="0" cellspacing="0" width="950px" border="1">
       
                <tr style='width:100%'> <div class="form-sec-row"> 
             
                 <td style='width:5%;background-color:#F5D9D0;' align="center">  <label><strong>Sub Heading</strong></label></td>
            <td style='width:5%;'> <asp:TextBox ID="TextBox286" runat="server" CssClass="textbox-medium1"></asp:TextBox></td>
           <td style='width:5%;background-color:#F5D9D0;' align="center">   <label><strong>Normal Range</strong></label> </td>
             <td style='width:5%;'> <asp:TextBox ID="TextBox287" CssClass="textbox-medium1" runat="server"></asp:TextBox></td>
             <td style='width:5%;'><asp:Button ID="Button28" runat="server" CssClass="submit-button" Text="Show sub" OnClientClick="showStuff('Span33'); return false;"/></td></tr>
             </table>
             <span id="Span33" style="display: none;">
              <table cellpadding="0" cellspacing="0" width="950px" border="1">
            <tr style='width:100%'> <div class="form-sec-row">       
                 <td style='width:5%;background-color:#FBF9C1;' align="center">  <label><strong>Sub Heading</strong></label></td>
            <td style='width:5%;'> <asp:TextBox ID="TextBox288" runat="server" CssClass="textbox-medium1"></asp:TextBox></td>
           <td style='width:5%;background-color:#FBF9C1;' align="center">   <label><strong>Normal Range</strong></label> </td>
             <td style='width:5%;'> <asp:TextBox ID="TextBox289" CssClass="textbox-medium1" runat="server"></asp:TextBox></td>
             <td style='width:5%;'></td>
             </tr>
          
          <tr style='width:100%'> <div class="form-sec-row"> 
         <td style='width:5%;background-color:#FBF9C1;' align="center">  <label><strong>Sub Heading</strong></label></td>
            <td style='width:5%;'> <asp:TextBox ID="TextBox290" runat="server" CssClass="textbox-medium1"></asp:TextBox></td>
           <td style='width:5%;background-color:#FBF9C1;' align="center">   <label><strong>Normal Range</strong></label> </td>
             <td style='width:5%;'> <asp:TextBox ID="TextBox291" CssClass="textbox-medium1" runat="server"></asp:TextBox></td>
             <td style='width:5%;'>
             </td></tr>
           

                 <tr style='width:100%'> <div class="form-sec-row"> 
             
                 <td style='width:5%;background-color:#FBF9C1;' align="center">  <label><strong>Sub Heading</strong></label></td>
            <td style='width:5%;'> <asp:TextBox ID="TextBox292" runat="server" CssClass="textbox-medium1"></asp:TextBox></td>
           <td style='width:5%;background-color:#FBF9C1;' align="center">   <label><strong>Normal Range</strong></label> </td>
             <td style='width:5%;'> <asp:TextBox ID="TextBox293" CssClass="textbox-medium1" runat="server"></asp:TextBox></td>
             <td style='width:5%;'></td></tr>
            

                 <tr style='width:100%'> <div class="form-sec-row"> 
             
                 <td style='width:5%;background-color:#FBF9C1;' align="center">  <label><strong>Sub Heading</strong></label></td>
            <td style='width:5%;'> <asp:TextBox ID="TextBox294" runat="server" CssClass="textbox-medium1"></asp:TextBox></td>
           <td style='width:5%;background-color:#FBF9C1;' align="center">   <label><strong>Normal Range</strong></label> </td>
             <td style='width:5%;'> <asp:TextBox ID="TextBox295" CssClass="textbox-medium1" runat="server"></asp:TextBox></td></tr>

              <tr style='width:100%'> <div class="form-sec-row"> 
             
                 <td style='width:5%;background-color:#FBF9C1;' align="center">  <label><strong>Sub Heading</strong></label></td>
            <td style='width:5%;'> <asp:TextBox ID="TextBox296" runat="server" CssClass="textbox-medium1"></asp:TextBox></td>
           <td style='width:5%;background-color:#FBF9C1;' align="center">   <label><strong>Normal Range</strong></label> </td>
             <td style='width:5%;'> <asp:TextBox ID="TextBox297" CssClass="textbox-medium1" runat="server"></asp:TextBox></td></tr>
           </table> 
          </span></span>

          <span id="Span34" style="display: none;">
        <table cellpadding="0" cellspacing="0" width="950px" border="1">
       
                <tr style='width:100%'> <div class="form-sec-row"> 
             
                 <td style='width:5%;background-color:#F5D9D0;' align="center">  <label><strong>Sub Heading</strong></label></td>
            <td style='width:5%;'> <asp:TextBox ID="TextBox298" runat="server" CssClass="textbox-medium1"></asp:TextBox></td>
           <td style='width:5%;background-color:#F5D9D0;' align="center">   <label><strong>Normal Range</strong></label> </td>
             <td style='width:5%;'> <asp:TextBox ID="TextBox299" CssClass="textbox-medium1" runat="server"></asp:TextBox></td>
             <td style='width:5%;'><asp:Button ID="Button29" runat="server" CssClass="submit-button" Text="Show sub" OnClientClick="showStuff('Span35'); return false;"/></td></tr>
             </table>
             <span id="Span35" style="display: none;">
              <table cellpadding="0" cellspacing="0" width="950px" border="1">
            <tr style='width:100%'> <div class="form-sec-row">       
                 <td style='width:5%;background-color:#FBF9C1;' align="center">  <label><strong>Sub Heading</strong></label></td>
            <td style='width:5%;'> <asp:TextBox ID="TextBox300" runat="server" CssClass="textbox-medium1"></asp:TextBox></td>
           <td style='width:5%;background-color:#FBF9C1;' align="center">   <label><strong>Normal Range</strong></label> </td>
             <td style='width:5%;'> <asp:TextBox ID="TextBox301" CssClass="textbox-medium1" runat="server"></asp:TextBox></td>
             <td style='width:5%;'></td>
             </tr>
          
          <tr style='width:100%'> <div class="form-sec-row"> 
         <td style='width:5%;background-color:#FBF9C1;' align="center">  <label><strong>Sub Heading</strong></label></td>
            <td style='width:5%;'> <asp:TextBox ID="TextBox302" runat="server" CssClass="textbox-medium1"></asp:TextBox></td>
           <td style='width:5%;background-color:#FBF9C1;' align="center">   <label><strong>Normal Range</strong></label> </td>
             <td style='width:5%;'> <asp:TextBox ID="TextBox303" CssClass="textbox-medium1" runat="server"></asp:TextBox></td>
             <td style='width:5%;'>
             </td></tr>
           

                 <tr style='width:100%'> <div class="form-sec-row"> 
             
                 <td style='width:5%;background-color:#FBF9C1;' align="center">  <label><strong>Sub Heading</strong></label></td>
            <td style='width:5%;'> <asp:TextBox ID="TextBox304" runat="server" CssClass="textbox-medium1"></asp:TextBox></td>
           <td style='width:5%;background-color:#FBF9C1;' align="center">   <label><strong>Normal Range</strong></label> </td>
             <td style='width:5%;'> <asp:TextBox ID="TextBox305" CssClass="textbox-medium1" runat="server"></asp:TextBox></td>
             <td style='width:5%;'></td></tr>
            

                 <tr style='width:100%'> <div class="form-sec-row"> 
             
                 <td style='width:5%;background-color:#FBF9C1;' align="center">  <label><strong>Sub Heading</strong></label></td>
            <td style='width:5%;'> <asp:TextBox ID="TextBox306" runat="server" CssClass="textbox-medium1"></asp:TextBox></td>
           <td style='width:5%;background-color:#FBF9C1;' align="center">   <label><strong>Normal Range</strong></label> </td>
             <td style='width:5%;'> <asp:TextBox ID="TextBox307" CssClass="textbox-medium1" runat="server"></asp:TextBox></td></tr>

              <tr style='width:100%'> <div class="form-sec-row"> 
             
                 <td style='width:5%;background-color:#FBF9C1;' align="center">  <label><strong>Sub Heading</strong></label></td>
            <td style='width:5%;'> <asp:TextBox ID="TextBox308" runat="server" CssClass="textbox-medium1"></asp:TextBox></td>
           <td style='width:5%;background-color:#FBF9C1;' align="center">   <label><strong>Normal Range</strong></label> </td>
             <td style='width:5%;'> <asp:TextBox ID="TextBox309" CssClass="textbox-medium1" runat="server"></asp:TextBox></td></tr>
           </table> 
          </span></span>


                <table cellpadding="0" cellspacing="0" width="950px" border="1">
                     <tr><td colspan="4">
                         <asp:Button ID="Button30" runat="server" Text="Submit" 
                             CssClass="submit-buttondtls" onclick="Button30_Click"/></td></tr> </table>




        </asp:Panel>
         
         <asp:HiddenField ID="HiddenField1" runat="server" />
         <asp:HiddenField ID="HiddenField2" runat="server" />
         <asp:HiddenField ID="HiddenField3" runat="server" />
         <asp:HiddenField ID="HiddenField4" runat="server" />
         <asp:HiddenField ID="HiddenField5" runat="server" />    
         <asp:HiddenField ID="HiddenField6" runat="server" />
         <asp:HiddenField ID="HiddenField7" runat="server" />
         <asp:HiddenField ID="HiddenField8" runat="server" />
         <asp:HiddenField ID="HiddenField9" runat="server" />
         <asp:HiddenField ID="HiddenField10" runat="server" />
         <asp:HiddenField ID="HiddenField11" runat="server" />
         <asp:HiddenField ID="HiddenField12" runat="server" />
         <asp:HiddenField ID="HiddenField13" runat="server" />
         <asp:HiddenField ID="HiddenField14" runat="server" />
         <asp:HiddenField ID="HiddenField15" runat="server" />
         <asp:HiddenField ID="HiddenField16" runat="server" />
         <asp:HiddenField ID="HiddenField17" runat="server" />
         <asp:HiddenField ID="HiddenField18" runat="server" />
         <asp:HiddenField ID="HiddenField19" runat="server" />
         <asp:HiddenField ID="HiddenField20" runat="server" />
         <asp:HiddenField ID="HiddenField21" runat="server" />
         <asp:HiddenField ID="HiddenField22" runat="server" />
         <asp:HiddenField ID="HiddenField23" runat="server" />
         <asp:HiddenField ID="HiddenField24" runat="server" />
         <asp:HiddenField ID="HiddenField25" runat="server" />
         <asp:HiddenField ID="HiddenField26" runat="server" />
         <asp:HiddenField ID="HiddenField27" runat="server" />
         <asp:HiddenField ID="HiddenField28" runat="server" />
         <asp:HiddenField ID="HiddenField29" runat="server" />
         <asp:HiddenField ID="HiddenField30" runat="server" />
         <asp:HiddenField ID="HiddenField31" runat="server" />
         <asp:HiddenField ID="HiddenField32" runat="server" />
         <asp:HiddenField ID="HiddenField33" runat="server" />
         <asp:HiddenField ID="HiddenField34" runat="server" />
         <asp:HiddenField ID="HiddenField35" runat="server" />
         <asp:HiddenField ID="HiddenField36" runat="server" />
         <asp:HiddenField ID="HiddenField37" runat="server" />
         <asp:HiddenField ID="HiddenField38" runat="server" />
         <asp:HiddenField ID="HiddenField39" runat="server" />
         <asp:HiddenField ID="HiddenField40" runat="server" />
         <asp:HiddenField ID="HiddenField41" runat="server" />
         <asp:HiddenField ID="HiddenField42" runat="server" />
         <asp:HiddenField ID="HiddenField43" runat="server" />
         <asp:HiddenField ID="HiddenField44" runat="server" />
         <asp:HiddenField ID="HiddenField45" runat="server" />
         <asp:HiddenField ID="HiddenField46" runat="server" />
         <asp:HiddenField ID="HiddenField47" runat="server" />
         <asp:HiddenField ID="HiddenField48" runat="server" />
         <asp:HiddenField ID="HiddenField49" runat="server" />
         <asp:HiddenField ID="HiddenField50" runat="server" />
         <asp:HiddenField ID="HiddenField51" runat="server" />
         <asp:HiddenField ID="HiddenField52" runat="server" />
         <asp:HiddenField ID="HiddenField53" runat="server" />
         <asp:HiddenField ID="HiddenField54" runat="server" />
         <asp:HiddenField ID="HiddenField55" runat="server" />    
         <asp:HiddenField ID="HiddenField56" runat="server" />
         <asp:HiddenField ID="HiddenField57" runat="server" />
         <asp:HiddenField ID="HiddenField58" runat="server" />
         <asp:HiddenField ID="HiddenField59" runat="server" />
         <asp:HiddenField ID="HiddenField60" runat="server" />
         <asp:HiddenField ID="HiddenField61" runat="server" />
         <asp:HiddenField ID="HiddenField62" runat="server" />
         <asp:HiddenField ID="HiddenField63" runat="server" />
         <asp:HiddenField ID="HiddenField64" runat="server" />
         <asp:HiddenField ID="HiddenField65" runat="server" />
         <asp:HiddenField ID="HiddenField66" runat="server" />
         <asp:HiddenField ID="HiddenField67" runat="server" />
         <asp:HiddenField ID="HiddenField68" runat="server" />
         <asp:HiddenField ID="HiddenField69" runat="server" />
         <asp:HiddenField ID="HiddenField70" runat="server" />
         <asp:HiddenField ID="HiddenField71" runat="server" />
         <asp:HiddenField ID="HiddenField72" runat="server" />
         <asp:HiddenField ID="HiddenField73" runat="server" />
         <asp:HiddenField ID="HiddenField74" runat="server" />
         <asp:HiddenField ID="HiddenField75" runat="server" />
         <asp:HiddenField ID="HiddenField76" runat="server" />
         <asp:HiddenField ID="HiddenField77" runat="server" />
         <asp:HiddenField ID="HiddenField78" runat="server" />
         <asp:HiddenField ID="HiddenField79" runat="server" />
         <asp:HiddenField ID="HiddenField80" runat="server" />
         <asp:HiddenField ID="HiddenField81" runat="server" />
         <asp:HiddenField ID="HiddenField82" runat="server" />
         <asp:HiddenField ID="HiddenField83" runat="server" />
         <asp:HiddenField ID="HiddenField84" runat="server" />
         <asp:HiddenField ID="HiddenField85" runat="server" />
         <asp:HiddenField ID="HiddenField86" runat="server" />
         <asp:HiddenField ID="HiddenField87" runat="server" />
         <asp:HiddenField ID="HiddenField88" runat="server" />
         <asp:HiddenField ID="HiddenField89" runat="server" />
         <asp:HiddenField ID="HiddenField90" runat="server" />
         <asp:HiddenField ID="HiddenField91" runat="server" />
         <asp:HiddenField ID="HiddenField92" runat="server" />
         <asp:HiddenField ID="HiddenField93" runat="server" />
         <asp:HiddenField ID="HiddenField94" runat="server" />
         <asp:HiddenField ID="HiddenField95" runat="server" />
         <asp:HiddenField ID="HiddenField96" runat="server" />
         <asp:HiddenField ID="HiddenField97" runat="server" />
         <asp:HiddenField ID="HiddenField98" runat="server" />
         <asp:HiddenField ID="HiddenField99" runat="server" />
         <asp:HiddenField ID="HiddenField100" runat="server" />
     </div>
    </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>

