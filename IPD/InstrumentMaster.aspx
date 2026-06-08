<%@ Page Title="" Language="C#" MasterPageFile="~/MasterAll/MasterPageAll.master" AutoEventWireup="true" CodeFile="InstrumentMaster.aspx.cs" Inherits="IPD_InstrumentMaster" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
 
     <script type="text/javascript" language="javascript">
        function ShowDialog() {
            var rtvalue = window.open("IpdpatientPopup.aspx", "sss", "Width:800px; Height:550px; dialogLeft:250px;");
            document.getElementById("ctl00_ContentPlaceHolder1_TextBox2").value = rtvalue.NameValue;

        }

        function Calling() {

            $("input[id$='Button1']").click(function () {

                if ($("select[id$='DropDownList1']").val() == '0') {
                    alert('Please Select Instrument Type !');
                    $("select[id$='DropDownList1']").addClass('textboxerr');
                    $("select[id$='DropDownList1']").focus();
                    return false;
                }
                else {
                    $("select[id$='DropDownList1']").removeClass('textboxerr');
                }

                if ($("input[id$='TextBox1']").val() == '') {
                    alert('Please Enter Instrument Name !');
                    $("input[id$='TextBox1']").focus();
                    $("input[id$='TextBox1']").addClass('textboxerr');
                    return false;
                }
                else {
                    $("input[id$='TextBox1']").removeClass('textboxerr');
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
            document.getElementById("ctl00_ContentPlaceHolder1_TextBox1").value = regname[0];

            $("#TextBox1").focus();
            //$("#DropDownList4").val(regname[1]);
        }
</script>


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
                <asp:Label ID="Label1" runat="server">Instrument Master</asp:Label>
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
                       Instrument Id :</strong></label>
                     <asp:TextBox ID="TextBox2" runat="server" CssClass="textbox-medium1" 
                              Enabled="False"></asp:TextBox>
                        <div class="clear">
                        </div>
                    </div>
                <div class="form-sec-row">
                        <label>
                        <strong>
                       Type :</strong></label>
                     <asp:DropDownList ID="DropDownList5" runat="server" 
                              CssClass="textbox-medium1" >                           
                         </asp:DropDownList>
                        <div class="clear">
                        </div>
                    </div>
                      <div class="form-sec-row">
                        <label>
                        <strong>
                       Instrument Category :</strong></label>
                             <asp:DropDownList ID="DropDownList1" runat="server" 
                              CssClass="textbox-medium1" AutoPostBack="True" 
                              onselectedindexchanged="DropDownList1_SelectedIndexChanged">                               
                         </asp:DropDownList>
                         
                      
                        <div class="clear">
                        </div>
                    </div>

                         <div class="form-sec-row">
                        <label>
                        <strong>
                       Instrument Sub Category :</strong></label>
                             <asp:DropDownList ID="DropDownList2" runat="server" CssClass="textbox-medium1">                               
                         </asp:DropDownList>
                         
                      
                        <div class="clear">
                        </div>
                    </div>
                        <div class="form-sec-row">
                        <label>
                        <strong>
                       Instrument Name :</strong></label>
                     <asp:TextBox ID="TextBox1" runat="server" CssClass="textbox-medium1" 
                              Enabled="true"></asp:TextBox>
                            <cc1:AutoCompleteExtender ServiceMethod="SearchIns" OnClientItemSelected="autoCompleteEx_ItemSelected" MinimumPrefixLength="1"
                                    CompletionInterval="100" EnableCaching="false" CompletionSetCount="10" TargetControlID="TextBox1" ID="AutoCompleteExtender2" runat="server" 
                                       FirstRowSelected = "false" ></cc1:AutoCompleteExtender>
                        <div class="clear">
                        </div>
                    </div>

                    <div class="form-sec-row">
                        <label>
                        <strong>
                       Cost Per Unit :</strong></label>
                     <asp:TextBox ID="TextBox3" runat="server" CssClass="textbox-medium1" 
                              Enabled="true"></asp:TextBox>
                        <div class="clear">
                        </div>
                    </div>
            

                    <div class="form-sec-row">
                        <div class="clear">
                        </div>
                    </div>
                    <div class="form-sec-row">
                        &nbsp;&nbsp;&nbsp;<label><strong>&nbsp;</strong></label>
                        <asp:Button ID="Button1" runat="server" CssClass="submit-button" height="30px"
                            Text="Submit" onclick="Button1_Click" />
                        <asp:Button ID="Button2" runat="server" CssClass="submit-button" height="30px"
                            Text="Cancel" onclick="Button2_Click"  />
                        <div class="clear">
                        </div>
                    </div>
                    <div class="clear">
                    </div>
                </div>
            
            </asp:View>
                    
                    <asp:View ID="View2" runat="server">
                    <table width="100%" style='background-color:#7EEB76;'>
                    <tr>
                        <td><label><strong>Type :</strong></label></td>
                        <td> 
                        <asp:DropDownList ID="DropDownList6" runat="server" Width="150px"  
                            
                              >
                        </asp:DropDownList> </td>
                    <td><label><strong>Category :</strong></label></td>
                    <td> 
                        <asp:DropDownList ID="DropDownList3" runat="server" Width="150px"  
                            AutoPostBack="True" 
                            onselectedindexchanged="DropDownList3_SelectedIndexChanged"  >
                        </asp:DropDownList> </td>
                    <td><label><strong>Sub Category :</strong></label></td>
                    <td> <asp:DropDownList ID="DropDownList4" runat="server" Width="150px">
                        </asp:DropDownList>  </td>
                        <td> 
                            <asp:Button ID="Button3" runat="server" Text="Search"  CssClass="submit-button" 
                                onclick="Button3_Click"/> </td>
                    </tr>
                    </table>
                   
                     <div style='width:100%;'>
  <table width="960px"   style='background-color:#FB7B13;border:1px solid black; color:#FFF;'>
  <tr>
      
  <td style='width:150px;' align="center" >Instrument ID</td> 
      <td style='width:150px;' align="center" >Instrument Type</td>
   <td style='width:150px'  align="center">Instrument Category</td> 
       
     <td style='width:190px;' align="center">Sub Category</td>
      <td style='width:170px;' align="center">Instrument Name</td>
      <td style='width:170px;' align="center">Cost Per Unit</td>
       <td style='width:150px;' align="center">Edit</td>
        <td style='width:150px;' align="center" id="coldel" runat="server">Delete</td>
          </tr>
  </table> 
  </div> 

            <div class="listing" style='height:700px; overflow:auto;'>
                <asp:GridView id="GridView1"  Width="100%" CssClass="grid" PagerStyle-CssClass="pgr"
                 DataKeyNames ="RowID" runat="server" 
                 AutoGenerateColumns="False" AllowPaging="True" 
                 SelectedRowStyle-BackColor="GreenYellow"  PageSize="100" ShowHeader="false"
                    onpageindexchanging="GridView1_PageIndexChanging" 
                    onrowcommand="GridView1_RowCommand" onrowdeleting="GridView1_RowDeleting"  onrowdatabound="GridView1_RowDataBound">
                    <Columns>
                        <asp:TemplateField HeaderText="ID" ItemStyle-Width="150px"  ItemStyle-HorizontalAlign="Center" >
                            <ItemTemplate>
                                <asp:Label ID="lblid" runat="server" Text='<%# Bind("RowID") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
             
                        <asp:TemplateField HeaderText="Instrument Type Name"  ItemStyle-Width="150px"  ItemStyle-HorizontalAlign="Center">
                            <ItemTemplate>
                                
                                <asp:Label ID="lblTypeNm" runat="server" Text=""></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                      <asp:TemplateField HeaderText="Instrument Type Id" Visible="false" ItemStyle-Width="150px"  ItemStyle-HorizontalAlign="Center">
                            <ItemTemplate>
                                <asp:Label ID="lblTypeid" runat="server" Text='<%# Bind("InstrumentType") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>


                        <asp:TemplateField HeaderText="Instrument Category" ItemStyle-Width="150px"  ItemStyle-HorizontalAlign="Center">
                            <ItemTemplate>
                                <asp:Label ID="lblTypeName" runat="server" Text='<%# Bind("TypeName") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>

                                <asp:TemplateField HeaderText="Sub Category id" Visible="false" ItemStyle-Width="150px"  ItemStyle-HorizontalAlign="Center">
                            <ItemTemplate>
                                <asp:Label ID="SubCategoryId" runat="server" Text='<%# Bind("SubCategoryId") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>

                         <asp:TemplateField HeaderText="Sub Category" ItemStyle-Width="190px"  ItemStyle-HorizontalAlign="Center">
                            <ItemTemplate>
                                <asp:Label ID="SubCategoryName" runat="server" Text='<%# Bind("SubCategoryName") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>


                         <asp:TemplateField HeaderText="Instrument Name" ItemStyle-Width="170px" ItemStyle-HorizontalAlign="Center">
                            <ItemTemplate>
                                <asp:Label ID="lblInstrumentName" runat="server" Text='<%# Bind("InstrumentName") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="Cost" ItemStyle-Width="170px" ItemStyle-HorizontalAlign="Center">
                            <ItemTemplate>
                                <asp:Label ID="lblCost" runat="server" Text='<%# Bind("UnitCost") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        
                         <asp:TemplateField HeaderText="Type" ItemStyle-Width="170px" ItemStyle-HorizontalAlign="Center" Visible="false">
                            <ItemTemplate>
                                <asp:Label ID="lblType" runat="server" Text='<%# Bind("ins_type") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                                        
                        <asp:CommandField SelectText="Edit" ShowSelectButton="True"  ItemStyle-HorizontalAlign="Center" ControlStyle-CssClass="temp" ItemStyle-Width="150px" HeaderText="Edit" SelectImageUrl="~/Images/edit.png" ButtonType="Image">
                            <ControlStyle CssClass="temp"></ControlStyle>
                        </asp:CommandField>
                        <asp:CommandField ShowDeleteButton="True"  ItemStyle-HorizontalAlign="Center" ControlStyle-CssClass="temp" HeaderText="Delete" ItemStyle-Width="150px"  DeleteImageUrl="~/Images/delete.png" ButtonType="Image">
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

