<%@ Page Title="" Language="C#" MasterPageFile="~/MasterAll/MasterPageAll.master" AutoEventWireup="true" CodeFile="DiagnosisMaster.aspx.cs" Inherits="IPD_DiagnosisMaster" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
  



    <script language="javascript" type="text/javascript">
    function DisableBackButton() {
        window.history.forward()
    }
    DisableBackButton();
    window.onload = DisableBackButton;
    window.onpageshow = function (evt) { if (evt.persisted) DisableBackButton() }
    window.onunload = function () { void (0) }

    function Calling() {
    
        $("input[id$='Button1']").click(function () {

            if ($("input[id$='txtdiagnosis']").val() == '') {
                alert('Diagnosis Name Can not be Blank!');
                $("input[id$='txtdiagnosis']").focus();
                $("input[id$='txtdiagnosis']").addClass('textboxerr');
                return false;
            }
            else {
                $("input[id$='txtdiagnosis']").removeClass('textboxerr');
            }



            if ($("select[id$='DropDownList1']").val() == '0') {
                alert('Please Select Discipline !');
                $("select[id$='DropDownList1']").addClass('textboxerr');
                $("select[id$='DropDownList1']").focus();
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
          
    function SetContextKey() {
        $find('AutoCompleteExtender1').set_contextKey("GFC");
    }

    function autoCompleteEx_ItemSelected(sender, args) {

        var regname = args.get_value().split('~');// alert(regname[0]);
        //document.getElementById("ctl00_ContentPlaceHolder1_TextBoxicode").value = regname[1];
        document.getElementById("ctl00_ContentPlaceHolder1_txtdiagnosis").value = regname[0];

        $("#txtdiagnosis").focus();
        //$("#DropDownList4").val(regname[1]);
    }
</script>


<%--For Busy Loader .............................--%>


 <%--
  <asp:UpdateProgress ID="UpdateProgress1" runat="server">
    <ProgressTemplate>
    
       <div id="aaa" class="progressBackgroundFilter">          </div>
        <div id="bbb" class="processMessage"><img alt="Loading" src="../images/pwait.gif"/>
      
              </div>
    </ProgressTemplate>
    </asp:UpdateProgress>--%>


    <%--For Busy Loader End.............................--%>


<asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <div class="pageheader">
                <asp:Label ID="Label1" runat="server">Diagnosis Master</asp:Label>
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
                        <strong>
                        <asp:Label ID="lblError" runat="server" Font-Bold="True" CssClass="fadeout"></asp:Label>
                        </strong>
                        <div class="clear">
                        </div>
                    </div>
                    <asp:HiddenField ID="HiddenField1" runat="server" Value="0" />
                       <div class="form-sec-row">
                        <label>
                        <strong>
                        Discipline :</strong></label>
                         <asp:DropDownList ID="DropDownList1" runat="server" CssClass="textbox-medium1" >
                         </asp:DropDownList>
                        <div class="clear">
                        </div>
                    </div>


                    <div class="form-sec-row">
                        <label>
                        <strong>
                        Diagnosis Name :</strong></label>
                        <asp:TextBox ID="txtdiagnosis" runat="server" CssClass="textbox-medium1" ></asp:TextBox>
                        <cc1:AutoCompleteExtender ServiceMethod="Searchdiagno" OnClientItemSelected="autoCompleteEx_ItemSelected" MinimumPrefixLength="1"
                                    CompletionInterval="100" EnableCaching="false" CompletionSetCount="10" TargetControlID="txtdiagnosis" ID="AutoCompleteExtender2" runat="server" 
                                       FirstRowSelected = "false" ></cc1:AutoCompleteExtender>
                        <div class="clear">
                        </div>
                    </div>
                
 
                    <div class="form-sec-row">
                        &nbsp;&nbsp;&nbsp;<label><strong>&nbsp;</strong></label>
                        <asp:Button ID="Button1" runat="server" CssClass="submit-button"   Height="30px"
                            Text="Submit" onclick="Button1_Click"  />
                        <asp:Button ID="Button2" runat="server" CssClass="submit-button"  Height="30px"
                            Text="Cancel" onclick="Button2_Click"  />
                        <div class="clear">
                        </div>
                    </div>
                    <div class="clear">
                    </div>
                </div>
           
           </asp:View>
            <asp:View ID="View2" runat="server">

               <div style='width:100%;'>
  <table width="100%" style='background-color:#FB7B13; color:#FFF;'>
  <tr style='border:1px solid black;'> 
   <td style='width:160px;' align="center">Dicipline Name</td>
   <td style='width:160px;'  align="center">Diagnosis Name</td> 
        <td style='width:70px;' align="center">Edit</td>
          <td style='width:70px;' align="center" id="coldel" runat="server">Delete</td>
          </tr>
  </table> 
  </div> 

         <div class="listing"  style='width:100%; height:500px; overflow:auto;'>
                <asp:GridView id="GridView1"  Width="100%" CssClass="grid" PagerStyle-CssClass="pgr"
                 DataKeyNames ="DiagnosisId" runat="server"  ShowHeader="false"
                 AutoGenerateColumns="False" AllowPaging="True"  PageSize="1000"
                 OnRowCommand="GridView1_RowCommand" OnRowDataBound="GridView1_RowDataBound" OnRowDeleting="GridView1_RowDeleting" SelectedRowStyle-BackColor="GreenYellow"
                 OnPageIndexChanging="GridView1_PageIndexChanging" >
                    <Columns>
                        <asp:TemplateField HeaderText="Service ID" Visible="false">
                            <ItemTemplate>
                                <asp:Label ID="lblID" runat="server" Text='<%# Bind("DiagnosisId") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>


                           <asp:TemplateField HeaderText="Discipline id"  Visible ="false">
                            <ItemTemplate>
                                <asp:Label ID="Disciplineid" runat="server" Text='<%# Bind("DisciplineId") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>


                        <asp:TemplateField HeaderText="Discipline Name" ItemStyle-Width="160px" ItemStyle-HorizontalAlign="Center" >
                            <ItemTemplate>
                                <asp:Label ID="ServiceName" runat="server" Text='<%# Bind("DisciplineName") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="DiagnosisName"  ItemStyle-Width="160px"  ItemStyle-HorizontalAlign="Center"  >
                            <ItemTemplate>
                                <asp:Label ID="DiagnosisName" runat="server" Text='<%# Bind("DiagnosisName") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                 
                      
 
                               
                        <asp:CommandField SelectText="Edit" ShowSelectButton="True"  ItemStyle-Width="70px"  ItemStyle-HorizontalAlign="Center"  ControlStyle-CssClass="temp" HeaderText="Edit" SelectImageUrl="~/Images/edit.png" ButtonType="Image">
                            <ControlStyle CssClass="temp"></ControlStyle>
                        </asp:CommandField>
                        <asp:CommandField ShowDeleteButton="True" ControlStyle-CssClass="temp"  ItemStyle-Width="70px" ItemStyle-HorizontalAlign="Center"  HeaderText="Delete" DeleteImageUrl="~/Images/delete.png" ButtonType="Image">
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

