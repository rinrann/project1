<%@ Page Title="" Language="C#" MasterPageFile="~/MasterAll/MasterPageAll.master" AutoEventWireup="true" CodeFile="ReportMaster.aspx.cs" Inherits="IPD_ReportMaster" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <script type="text/javascript" src="../js/BengaliConverter.js"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <script language="javascript" type="text/javascript">
             google.load("elements", "1", {
                packages: "transliteration"
            });

            function onLoad() {
                var options = {
                    sourceLanguage: google.elements.transliteration.LanguageCode.ENGLISH,
                    destinationLanguage: google.elements.transliteration.LanguageCode.BENGALI, // available option English, Bengali, Marathi, Malayalam etc.
                    shortcutKey: 'ctrl+g',
                    transliterationEnabled: true
                };

                var control = new google.elements.transliteration.TransliterationControl(options);
                control.makeTransliteratable(['ctl00_ContentPlaceHolder1_txtBengali']);
                control.makeTransliteratable(['ctl00_ContentPlaceHolder1_txtReportName']);
            }
            google.setOnLoadCallback(onLoad);
</script>

<script language="javascript" type="text/javascript">

    function DisableBackButton() {
        window.history.forward()
    }
    DisableBackButton();
    window.onload = DisableBackButton;
    window.onpageshow = function (evt) { if (evt.persisted) DisableBackButton() }
    window.onunload = function () { void (0) }


    function Calling() {

        $("input[id$='btnSubmit']").click(function () {

            if ($("select[id$='DropDownList2']").val() == '0') {
                alert('Please Select REport Type Poperty !');
                $("select[id$='DropDownList2']").focus();
                $("select[id$='DropDownList2']").addClass('textboxerr');
                return false;
            }
            else {
                $("select[id$='DropDownList2']").removeClass('textboxerr');
            }

            if ($("select[id$='DropDownList1']").val() == '1') {


                if ($("input[id$='txtReportName']").val() == '') {
                    alert('Please Enter Report Name!');
                    $("input[id$='txtReportName']").focus();
                    $("input[id$='txtReportName']").addClass('textboxerr');
                    return false;
                }
                else {
                    $("input[id$='txtReportName']").removeClass('textboxerr');
                }



                if ($("input[id$='txtPatText']").val() == '') {
                    alert('Please Enter Report Context !');
                    $("input[id$='txtPatText']").focus();
                    $("input[id$='txtPatText']").addClass('textboxerr');
                    return false;
                }
                else {
                    $("input[id$='txtPatText']").removeClass('textboxerr');
                }

            }




            if ($("select[id$='DropDownList1']").val() == '2') {


                if ($("input[id$='txtReportNameEnglish']").val() == '') {
                    alert('Please Enter Report Name!');
                    $("input[id$='txtReportNameEnglish']").focus();
                    $("input[id$='txtReportNameEnglish']").addClass('textboxerr');
                    return false;
                }
                else {
                    $("input[id$='txtReportNameEnglish']").removeClass('textboxerr');
                }



                if ($("input[id$='txtEnglish']").val() == '') {
                    alert('Please Enter Report Context !');
                    $("input[id$='txtEnglish']").focus();
                    $("input[id$='txtEnglish']").addClass('textboxerr');
                    return false;
                }
                else {
                    $("input[id$='txtEnglish']").removeClass('textboxerr');
                }

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
        document.getElementById("ctl00_ContentPlaceHolder1_txtRoomCatName").value = regname[0];

        $("#txtRoomCatName").focus();
        //$("#DropDownList4").val(regname[1]);
    }
</script>


<%--    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
    <ContentTemplate>--%>
        <div class="pageheader">
         <asp:Label ID="Label1" runat="server">Report Creation Master</asp:Label>
    </div>


       <div class="formbox">
      <div class="error">
                <strong><asp:Label ID="lblError" runat="server" Font-Bold="True"></asp:Label></strong>
          <asp:HiddenField ID="HiddenField1" runat="server" />
                <div class="clear"></div>
            </div>

                  <div class="form-sec-row">
                <label><strong>Select Language :</strong></label>
                <asp:DropDownList ID="DropDownList1" runat="server" CssClass="textbox-medium1" 
                          AutoPostBack="True" onselectedindexchanged="DropDownList1_SelectedIndexChanged">
                    <asp:ListItem Value="1">Bengali</asp:ListItem>
                    <asp:ListItem Value="2">English</asp:ListItem>
                </asp:DropDownList>
                <div class="clear"></div>
            </div>

                  <div class="form-sec-row">
                <label><strong>Report Type :</strong></label>
                <asp:DropDownList ID="DropDownList2" runat="server"  CssClass="textbox-medium1">
                    <asp:ListItem Value="0">--Select--</asp:ListItem>
                    <asp:ListItem Value="1">Admission Form</asp:ListItem>
                    <asp:ListItem Value="2">Highrisk Form</asp:ListItem>
                </asp:DropDownList>
                <div class="clear"></div>
            </div>


              
         
                   <asp:Panel ID="Panel1" runat="server">
                    <div class="form-sec-row">
                <label><strong>Report Name (Bengali):</strong></label>
                       <asp:TextBox ID="txtReportName" CssClass="textbox-medium1" runat="server"></asp:TextBox>
                <div class="clear"></div>
            </div>

                 <div class="form-sec-row">
                <label><strong>Report Context (Bengali) :</strong><br /><br /><br /><br /><br /><br /><br /><br /><br /><br />
                <div style='font-weight:bold; color:Red;'>For a Para, Type " # " after ending of Previous Sentence</div></label>
                   <asp:TextBox ID="txtBengali" TextMode="MultiLine" Height="800px"  Width="700px"  
                    onKeypress="if (event.keyCode==8) event.returnValue = false;"
                    runat="server"></asp:TextBox>
                <div class="clear"></div>
            </div>
           </asp:Panel>


  <asp:Panel ID="Panel2" runat="server">
     <div class="form-sec-row">
                <label><strong>Report Name (English):</strong></label>
                       <asp:TextBox ID="txtReportNameEnglish" CssClass="textbox-medium1" runat="server"></asp:TextBox>
                <div class="clear"></div>
            </div>

                <div class="form-sec-row">
                <label><strong>Report Context (English) :</strong><br /><br /><br /><br /><br /><br /><br /><br /><br /><br />
                <div style='font-weight:bold; color:Red;'>For a Para, Type " # " after ending of Previous Sentence</div></label>
                   <asp:TextBox ID="txtEnglish" TextMode="MultiLine" Height="800px"  Width="700px" runat="server"></asp:TextBox>
                <div class="clear"></div>
            </div>
     </asp:Panel>


       <div class="form-sec-row">
                <label></label>
           <asp:Button ID="btnSubmit" runat="server" CssClass="submit-button" Width="80px" 
                    Text="Submit" onclick="btnSubmit_Click" />
           <asp:Button ID="btnClear" runat="server" CssClass="submit-button"  Width="80px"  
                    Text="Clear" onclick="btnClear_Click" />
                <div class="clear"></div>
            </div>
             

             <div class="listing"   style='width:100%; height:500px; overflow:auto;'>
         <asp:GridView id="GridView1"  CssClass="grid"
                 PagerStyle-CssClass="pgr" DataKeyNames ="ID" runat="server"  
                 AutoGenerateColumns="False" AllowPaging="True" PageSize ="100"  Width="100%"
               SelectedRowStyle-BackColor="GreenYellow" onrowcommand="GridView1_RowCommand" OnRowDataBound="GridView1_RowDataBound" 
                     onrowdeleting="GridView1_RowDeleting" >
                <RowStyle HorizontalAlign="Center" />
                <Columns>
                    <asp:TemplateField HeaderText="ID" Visible="false">
                        <ItemTemplate>
                            <asp:Label ID="lblID" runat="server" Text='<%# Bind("ID") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="Language"  Visible="false" >                       
                        <ItemTemplate>
                            <asp:Label ID="lblLanguage" runat="server" Text='<%# Bind("Language") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>


                       <asp:TemplateField HeaderText="FormType"  Visible="false" >                                
                        <ItemTemplate>
                            <asp:Label ID="lblFormType" runat="server" Text='<%# Bind("FormType") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>

                   <asp:TemplateField HeaderText="Form Name "  >                       
                        <ItemTemplate>
                            <asp:Label ID="lblFormName" runat="server" Text='<%# Bind("FormName") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField> 


                      <asp:TemplateField HeaderText="Form Context"  >                       
                        <ItemTemplate>
                            <asp:Label ID="lblFormContext" runat="server" Text='<%# Bind("FormContext") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>

               
                    <asp:CommandField SelectText="Edit" ShowSelectButton="True"    ItemStyle-Width="70px"  ControlStyle-CssClass="temp" HeaderText="Edit" SelectImageUrl="~/Images/edit.png" ButtonType="Image">
                        <ControlStyle CssClass="temp"></ControlStyle>
                    </asp:CommandField>
                    <asp:CommandField ShowDeleteButton="True" ControlStyle-CssClass="temp"    ItemStyle-Width="70px"  HeaderText="Delete" DeleteImageUrl="~/Images/delete.png" ButtonType="Image">
                        <ControlStyle CssClass="temp"></ControlStyle>
                    </asp:CommandField>
                </Columns>
                <PagerStyle CssClass="pgr" />
                <EditRowStyle BackColor="#CCFF33" />
                <AlternatingRowStyle BackColor="#FFDB91" />
        </asp:GridView> 
        </div>

            </div>
<%--    </ContentTemplate>
    </asp:UpdatePanel>--%>

</asp:Content>

