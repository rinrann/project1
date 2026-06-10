<%@ Page Title="" Language="C#" MasterPageFile="~/MasterAll/MasterPageAll.master" AutoEventWireup="true" CodeFile="ProfileMaster.aspx.cs" Inherits="Pathology_ProfileMaster" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <script type="text/javascript">
    function ShowDialog() {

        var rtvalue = window.open("ProfilePopup.aspx", "sss", "Width:800px; Height:550px; dialogLeft:250px;");
        //document.getElementById("ctl00_ContentPlaceHolder1_txtcode").value = rtvalue;

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
        $("input[id$='txtprice']").focus(function () {
            $("input[id$='txtprice']").addClass('textboxborder');
        });
        $("input[id$='txtprice']").blur(function () {
            $("input[id$='txtprice']").removeClass('textboxborder');
        });

        $("input[id$='Button3']").click(function () {

            if ($("select[id$='DropDownList1']").val() == '0') {
                alert('Select Department !');
                $("select[id$='DropDownList1']").focus();
                $("select[id$='DropDownList1']").addClass('textboxerr');
                return false;
            }
            else {
                $("select[id$='DropDownList1']").removeClass('textboxerr');
            }

            if ($("input[id$='txtname']").val() == '') {
                $("input[id$='txtname']").addClass('textboxerr');
            }

            if ($("input[id$='txtname']").val() == '') {
                alert('Please Enter Test Group Name !');
                $("input[id$='txtname']").focus();
                $("input[id$='txtname']").addClass('textboxerr');
                return false;
            }
            else {
                $("input[id$='txtname']").removeClass('textboxerr');
            }


            if ($("input[id$='txtprice']").val() == '') {
                $("input[id$='txtprice']").addClass('textboxerr');
            }

            if ($("input[id$='txtprice']").val() == '') {
                alert('Please Enter Price !');
                $("input[id$='txtprice']").focus();
                $("input[id$='txtprice']").addClass('textboxerr');
                return false;
            }
            else {
                $("input[id$='txtprice']").removeClass('textboxerr');
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
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
  <ContentTemplate>
    <div class="pageheader">
         <asp:Label ID="Label1" runat="server">Test Group Master</asp:Label>
    </div>
     <div class="formbox">
      <div class="error">
                <strong><asp:Label ID="lblError" runat="server" Font-Bold="True"></asp:Label></strong>
                <div class="clear"></div>
            </div>
                  <div class="form-sec-row">
                <label><strong>Department :</strong></label>
                <asp:DropDownList ID="DropDownList1" runat="server" CssClass="textbox-medium1">
                </asp:DropDownList>
                <div class="clear"></div>
            </div>
        <div class="form-sec-row">
                <label><strong>Group Code :</strong></label>
            <asp:TextBox ID="txtcode" runat="server" CssClass="textbox-medium1"></asp:TextBox>
                <asp:Button ID="Button6" runat="server" Height="28px"  Text="quick Search" CssClass="submit-buttonCheck" OnClientClick="ShowDialog();" /><asp:Button ID="Button5" Height="28px"  runat="server" CssClass="submit-button" Text="fetch" onclick="Button5_Click" />
                <div class="clear"></div>
            </div>
            
            <div class="form-sec-row">
                <label><strong>Group Name :</strong></label>
                <asp:TextBox ID="txtname" runat="server" CssClass="textbox-medium1"></asp:TextBox>
                <asp:Label ID="lbltxt" runat="server" Text=" " 
                    style="font-size: small; font-family: 'Times New Roman', Times, serif;"></asp:Label>
                <cc1:AutoCompleteExtender ServiceMethod="SearchTestGroup" OnClientItemSelected="autoCompleteEx_ItemSelected" MinimumPrefixLength="1"
                                    CompletionInterval="100" EnableCaching="false" CompletionSetCount="10" TargetControlID="txtname" ID="AutoCompleteExtender2" runat="server" 
                                       FirstRowSelected = "false" ></cc1:AutoCompleteExtender>
                <div class="clear"></div>
            </div>

                   <div class="form-sec-row">
                <label><strong>Price :</strong></label>
                <asp:TextBox ID="txtprice" runat="server" CssClass="textbox-medium1"></asp:TextBox>
                <asp:Label ID="Label2" runat="server" Text=" " 
                    style="font-size: small; font-family: 'Times New Roman', Times, serif;"></asp:Label>
                <div class="clear"></div>
            </div>

      
            <div class="form-sec-row">
                <label><strong>&nbsp;</strong></label>
                <asp:Button ID="Button3" runat="server"  Text="Submit" CssClass="submit-button"  Height="28px" 
                    onclick="Button3_Click" />
                <asp:Button ID="Button4" runat="server" Text="Cancel"  CssClass="submit-button"  Height="28px"
                    onclick="Button4_Click" />
                <asp:HiddenField ID="HiddenField1" runat="server" />
                <div class="clear"></div>
            </div>
           
     </div>
     <table cellpadding="0" cellspacing="0"  title="Search">
     <tr>
                <td>
                    <div class="form-sec-row"> 
             <label><strong>Department :</strong></label> 
                        <asp:DropDownList ID="DropDownList2" runat="server" CssClass="textbox-medium1">
                        </asp:DropDownList>
            </div>
            
</td>
               <td>
                             <div class="form-sec-row"> 
             <label class="pname"><strong>Test Name :</strong></label> <asp:TextBox ID="TextBox1" runat="server" CssClass="textbox-medium1" Width="138px" ></asp:TextBox>
            </div>                  
</td>

                <td class="style1">
                             <div class="form-sec-row"> 
           <asp:Button ID="Button7" runat="server" Text="Search"  CssClass="submit-button1"  Height="28px"
                                     onclick="Button7_Click" />
            </div>                  
</td>             
                      
            </tr>
     </table>
     <div class="formbox">
               <table cellpadding="0" cellspacing="0" class="ui-accordion" title="Search">
                 <tr>
                <td rowspan="2"  class="style1">
                 <div style='height:200px; overflow:auto;'>
        
         <asp:GridView id="GridView1" style="width:400px;" CssClass="grid" PagerStyle-CssClass="pgr" AlternatingRowStyle-CssClass="alt"
            DataKeyNames ="TestId,TestName" 
            runat="server" AutoGenerateColumns="False" AllowPaging="True" 
             PageSize ="100" onpageindexchanging="GridView1_PageIndexChanging" >
                <Columns>

                    <asp:TemplateField HeaderText="Select" >
                        <ItemTemplate>
                            <asp:CheckBox ID="CheckBox1" runat="server"/>
                        </ItemTemplate>
                    </asp:TemplateField>         

                    <asp:TemplateField HeaderText="Test Code">
                        <ItemTemplate>
                            <asp:Label ID="lblid" runat="server" Text='<%# Bind("TestId") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="Test  Name">
                        <ItemTemplate>
                            <asp:Label ID="lblname" runat="server" Text='<%# Bind("TestName") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                      <asp:TemplateField HeaderText="Cost">
                        <ItemTemplate>
                            <asp:Label ID="lblcost" runat="server" Text='<%# Bind("Cost") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                   

                </Columns>
                <PagerStyle CssClass="pgr"></PagerStyle>
                <EditRowStyle BackColor="#CCFF33" />
                <AlternatingRowStyle BackColor="#FFDB91" />
        </asp:GridView>
           </div>

                  </td>
                        <td>
                     
                            <asp:Button ID="Button1" Height="28px" runat="server" Text=">>" CssClass="submit-button" onclick="Button1_Click" />
                     
                      </td>
                        <td rowspan="2">
                        <div class="listing">
                 <asp:GridView id="GridView2" style="width:400px;" CssClass="grid" 
                                PagerStyle-CssClass="pgr" AlternatingRowStyle-CssClass="alt"
            DataKeyNames ="TestId,TestName" 
            runat="server" AutoGenerateColumns="False" AllowPaging="True" 
             PageSize ="100" onpageindexchanging="GridView2_PageIndexChanging">
                <Columns>

                    <asp:TemplateField HeaderText="Select">
                        <ItemTemplate>
                            <asp:CheckBox ID="CheckBox2" runat="server" />
                        </ItemTemplate>
                    </asp:TemplateField>

                   <asp:TemplateField HeaderText="Test Code">
                        <ItemTemplate>
                            <asp:Label ID="lblId" runat="server" Text='<%# Bind("TestId") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="Test Name">
                        <ItemTemplate>
                            <asp:Label ID="lblName" runat="server" Text='<%# Bind("TestName") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>

                       <asp:TemplateField HeaderText="Cost">
                        <ItemTemplate>
                            <asp:Label ID="lblcost" runat="server" Text='<%# Bind("Cost") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>

                </Columns>
                <PagerStyle CssClass="pgr"></PagerStyle>
                <EditRowStyle BackColor="#CCFF33" />
                <AlternatingRowStyle BackColor="#FFDB91" />
        </asp:GridView>
           </div>

                   </td>
                      
            </tr>
                 <tr>
                     <td>
                         <asp:Button ID="Button2" runat="server" Text="<<" CssClass="submit-button" onclick="Button2_Click" />
                     </td>
                 </tr>

                   <tr>
                 <td colspan="3" align="right">  
                     <%--<asp:Button ID="Button7" runat="server" Text="Save" 
                         CssClass="submit-button" onclick="Button7_Click"  />--%></td>
                 </tr>
        </table>
                <div class="clear"> </div>
            </div>
    </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>

