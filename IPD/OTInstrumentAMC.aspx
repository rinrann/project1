<%@ Page Title="" Language="C#" MasterPageFile="~/MasterAll/MasterPageAll.master" AutoEventWireup="true" CodeFile="OTInstrumentAMC.aspx.cs" Inherits="IPD_OTInstrumentAMC" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <script language="javascript" type="text/javascript">
    function DisableBackButton() {
        window.history.forward()
    }
    DisableBackButton();
    window.onload = DisableBackButton;
    window.onpageshow = function (evt) { if (evt.persisted) DisableBackButton() }
    window.onunload = function () { void (0) }


    function Calling() {
        var date = new Date();
        $("input[id$='Calendar2']").datepicker({ dateFormat: 'dd/mm/yy', minDate: date });
        var date = new Date();
        $("input[id$='Calendar1']").datepicker({ dateFormat: 'dd/mm/yy', minDate: date });

        var date = new Date();
        $("input[id$='Calendar3']").datepicker({ dateFormat: 'dd/mm/yy', minDate: date });


        $("input[id$='Button1']").click(function () {

           
            

            if ($("select[id$='DropDownList1']").val() == '0') {
                alert('Please Select Instrument Name !');
                $("select[id$='DropDownList1']").addClass('textboxerr');
                $("select[id$='DropDownList1']").focus();
                return false;
            }
            else {
                $("select[id$='DropDownList1']").removeClass('textboxerr');
            }   


           if ($("input[id$='Calendar2']").val() == '') {
                alert('Please Enter Paid Date !');
                $("input[id$='Calendar2']").focus();
                $("input[id$='Calendar2']").addClass('textboxerr');
                return false;
            }
            else {
                $("input[id$='Calendar2']").removeClass('textboxerr');
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

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">

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
                <asp:Label ID="Label1" runat="server">OT Instrument AMC</asp:Label>
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
   
            <div class="formbox">
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
                         Instrument Name :</strong></label>
                        <asp:DropDownList ID="DropDownList1" runat="server" CssClass="textbox-medium1" 
                            AutoPostBack="True" onselectedindexchanged="DropDownList1_SelectedIndexChanged">
                        </asp:DropDownList>
                         <div class="clear">
                        </div>
                    </div>
                     

                          <div class="form-sec-row">
                        <label>
                        <strong>
                         Model No :</strong></label>
                        <asp:DropDownList ID="DropDownList3" runat="server" CssClass="textbox-medium1" 
                            AutoPostBack="True" 
                                  onselectedindexchanged="DropDownList3_SelectedIndexChanged"  >
                        </asp:DropDownList>
                         <div class="clear">
                        </div>
                    </div>

                     
                           
                           
                    <div class="form-sec-row">
                        <label>
                        <strong>
                        Manufacturing Company :</strong></label>
                       <asp:TextBox ID="TextBox1" runat="server" Enabled="false" CssClass="textbox-medium1" ></asp:TextBox>
                        <div class="clear">
                        </div>
                    </div>

                                  <div class="form-sec-row">
                <label>
                <strong>
                Purchase Date :</strong></label>
                 <asp:TextBox ID="Calendar1" runat="server"  Enabled="false" CssClass="textbox-medium1">
                  </asp:TextBox>
                        <asp:Label ID="Label3" runat="server" ForeColor="Green" Text="(DD/MM/YYYY)"></asp:Label>
                <div class="clear">
                </div>
            </div>



                     

                    <div class="form-sec-row">
                        <label>
                        <strong>
                        AMC Price :</strong></label>
                        <asp:TextBox ID="TextBox2" runat="server" CssClass="textbox-medium1" ></asp:TextBox>
                        <div class="clear">
                        </div>
                    </div>

                    
                    <div class="form-sec-row">
                        <label>
                        <strong>
                        AMC Paid Date :</strong></label>
                        <asp:TextBox ID="Calendar2" runat="server" CssClass="textbox-medium1" ></asp:TextBox>
                         <cc1:MaskedEditExtender ID="MaskedEditExtender1" runat="server" TargetControlID="Calendar2"
                                            Mask="99/99/9999" MessageValidatorTip="true" MaskType="Date" DisplayMoney="Left"
                                            AcceptNegative="Left" ErrorTooltipEnabled="True" CultureName="en-GB" />
                              <asp:Label ID="Label2" runat="server" ForeColor="Green" Text="(DD/MM/YYYY)"></asp:Label>
                        <div class="clear">
                        </div>
                    </div>


                        <div class="form-sec-row">
                        <label>
                        <strong>
                        Comment :</strong></label>
                        <asp:TextBox ID="TextBox4" runat="server" CssClass="textbox-medium1" ></asp:TextBox>
                        <div class="clear">
                        </div>
                    </div>

              

                

               
                 
                    <div class="form-sec-row">
                        <div class="clear">
                        </div>
                    </div>
                    <div class="form-sec-row">
                        &nbsp;&nbsp;&nbsp;<label><strong>&nbsp;</strong></label>
                        <asp:Button ID="Button1" runat="server" CssClass="submit-button"  Height="28px"
                            Text="Submit" onclick="Button1_Click"    />
                        <asp:Button ID="Button2" runat="server" CssClass="submit-button"    Height="28px"
                            Text="Cancel" onclick="Button2_Click"    />
                        <div class="clear">
                        </div>
                    </div>
                    <div class="clear">
                    </div>
                </div>
            </div>
        
      
               
                    </asp:View>


                    
                    <asp:View ID="View2" runat="server">
                    <table width="100%">
                    <tr>
                    <td><label><strong>Instrument Name :</strong></label></td>
                      <td> 
                          <asp:DropDownList ID="DropDownList2" Width="150px" runat="server">
                          </asp:DropDownList> </td>
                        <td><label><strong>Model No :</strong></label></td>
                          <td> 
                              <asp:TextBox ID="TextBox3" Width="150px" runat="server"></asp:TextBox> </td>
                              <td><asp:Button ID="Button3" runat="server" CssClass="submit-button"  Height="28px"
                            Text="Search" onclick="Button3_Click" /></td>
                    </tr>
                    </table>
        <div class="listing"  style='width:100%; height:300px; overflow:auto;'>
                <asp:GridView id="GridView1"  Width="100%" CssClass="grid" PagerStyle-CssClass="pgr"
                 DataKeyNames ="RowId" runat="server" 
                 AutoGenerateColumns="False" AllowPaging="True"  PageSize="100"
                 OnRowCommand="GridView1_RowCommand"   SelectedRowStyle-BackColor="GreenYellow"
                 OnPageIndexChanging="GridView1_PageIndexChanging" OnRowDataBound="GridView1_RowDataBound" 
                    onrowdeleting="GridView1_RowDeleting" > 
                    <Columns>
                        <asp:TemplateField HeaderText="ID" Visible="false">
                            <ItemTemplate>
                                <asp:Label ID="RowId" runat="server" Text='<%# Bind("RowId") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField> 
 
  
                            <asp:TemplateField HeaderText="Instrument Id" Visible="false">
                            <ItemTemplate>
                                <asp:Label ID="InstrumentId" runat="server" Text='<%# Bind("InstrumentId") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        
                        <asp:TemplateField HeaderText="Instrument Name">
                            <ItemTemplate>
                                <asp:Label ID="InstrumentName" runat="server" Text='<%# Bind("InstrumentName") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>

                          
                         <asp:TemplateField HeaderText="Model No">
                            <ItemTemplate>
                                <asp:Label ID="ModelNo" runat="server" Text='<%# Bind("ModelNo") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>

                             <asp:TemplateField HeaderText="AMC Price">
                            <ItemTemplate>
                                <asp:Label ID="AMCPrice" runat="server" Text='<%# Bind("AMCPrice") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="AMC Paid Date">
                            <ItemTemplate>
                                <asp:Label ID="AMCpaidDate" runat="server" Text='<%# Bind("pdate") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>

                         <asp:TemplateField HeaderText="Comment">
                            <ItemTemplate>
                                <asp:Label ID="Comment" runat="server" Text='<%# Bind("Comment") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                                    
                        <asp:CommandField SelectText="Edit" ShowSelectButton="True" ControlStyle-CssClass="temp" HeaderText="Edit" SelectImageUrl="~/Images/edit.png" ButtonType="Image">
                            <ControlStyle CssClass="temp"></ControlStyle>
                        </asp:CommandField>
                        <asp:CommandField ShowDeleteButton="True" ControlStyle-CssClass="temp" HeaderText="Delete" DeleteImageUrl="~/Images/delete.png" ButtonType="Image">
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

