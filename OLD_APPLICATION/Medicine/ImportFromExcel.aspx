<%@ Page Title="" Language="C#" MasterPageFile="~/MasterAll/MasterPageAll.master" AutoEventWireup="true" CodeFile="ImportFromExcel.aspx.cs" Inherits="Medicine_ImportFromExcel" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">

<script type="text/javascript" language="javascript">
  
    function Calling() {      
        var date = new Date();
        $("input[id$='TextBox2']").datepicker({ dateFormat: 'dd/mm/yy' });

        $("input[id$='Button1']").click(function () {

            /*if ($("select[id$='DropDownList1']").val() == '0') {
                alert('Please Select Manufacturing Company!');
                $("select[id$='DropDownList1']").addClass('textboxerr');
                $("select[id$='DropDownList1']").focus();
                return false;
            }
            else {
                $("select[id$='DropDownList1']").removeClass('textboxerr');
            }


            if ($("select[id$='DropDownList2']").val() == '0') {
                alert('Please Select Medicine Group!');
                $("select[id$='DropDownList2']").addClass('textboxerr');
                $("select[id$='DropDownList2']").focus();
                return false;
            }
            else {
                $("select[id$='DropDownList2']").removeClass('textboxerr');
            }

            if ($("select[id$='DropDownList3']").val() == '0') {
                alert('Please Select Medicine Sub Group!');
                $("select[id$='DropDownList3']").addClass('textboxerr');
                $("select[id$='DropDownList3']").focus();
                return false;
            }
            else {
                $("select[id$='DropDownList3']").removeClass('textboxerr');
            }*/

            if ($("select[id$='DropDownList4']").val() == '0') {
                alert('Please Select Medicine !');
                $("select[id$='DropDownList4']").addClass('textboxerr');
                $("select[id$='DropDownList4']").focus();
                return false;
            }
            else {
                $("select[id$='DropDownList4']").removeClass('textboxerr');
            }

            if ($("input[id$='TextBox3']").val() == '') {
                alert('Please Enter Opening Stock !');
                $("input[id$='TextBox3']").focus();
                $("input[id$='TextBox3']").addClass('textboxerr');
                return false;
            }
            else {
                $("input[id$='TextBox3']").removeClass('textboxerr');
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
     
<%--
     <asp:UpdatePanel ID="UpdatePanel1" runat="server">
    <ContentTemplate>--%>

        <div class="pageheader">
            <asp:Label ID="Label1" runat="server">Opening Stock Update</asp:Label>
        </div>

       <table width="290px" cellpadding="0" cellspacing="0">
         <tr>
            <td>
                <asp:Button Text="Manual" BorderStyle="None" ID="Tab1" Width="145px"  Height="40px" 
                    CssClass="Initial" runat="server" onclick="Tab1_Click"
                     /></td>
                     <td>
                <asp:Button Text="Import" BorderStyle="None" ID="Tab2" CssClass="Initial"  Width="145px"  Height="40px" 
                    runat="server" onclick="Tab2_Click" Visible="false"/></td>
            <td>
                <asp:Button Text="List" BorderStyle="None" ID="Tab3" Width="145px"  Height="40px" 
                    CssClass="Initial" runat="server" OnClick="Tab3_Click"  /></td>
           
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
                    <asp:HiddenField ID="HiddenField1" runat="server"/>

                     <div class="form-sec-row" style="display:none;">
                    <label><strong>
                     Manufacuring Company :</strong></label>
                         <asp:DropDownList ID="DropDownList1" runat="server" CssClass="textbox-medium1" 
                             AutoPostBack="True" onselectedindexchanged="DropDownList1_SelectedIndexChanged">
                         </asp:DropDownList>
       
                   <div class="clear">
                    </div>
                </div>

                <div class="form-sec-row" style="display:none;">
                    <label><strong>
                        Item Group :</strong></label>
  <asp:DropDownList ID="DropDownList2" runat="server" CssClass="textbox-medium1" 
                        AutoPostBack="True" onselectedindexchanged="DropDownList2_SelectedIndexChanged">
                         </asp:DropDownList>
                     <div class="clear">
                    </div>
                </div>         

                    <div class="form-sec-row" style="display:none;">
                    <label><strong> 
                        Item Sub Group :</strong></label>
                   <asp:DropDownList ID="DropDownList3" runat="server" CssClass="textbox-medium1" 
                            onselectedindexchanged="DropDownList3_SelectedIndexChanged" 
                            AutoPostBack="True">
                         </asp:DropDownList>
                    <div class="clear">
                    </div>
                </div>


                    <div class="form-sec-row">
                    <label><strong> 
                        Item Name :</strong></label>
                   <asp:DropDownList ID="DropDownList4" runat="server" CssClass="textbox-medium1"  >
                         </asp:DropDownList>
                    <div class="clear">
                    </div>
                </div>

                  <div class="form-sec-row">
                    <label><strong> 
                        Batch No :</strong></label>
                        <asp:TextBox ID="TextBox1" runat="server" CssClass="textbox-medium1"></asp:TextBox>
                    <div class="clear">
                    </div>
                </div>
                  <div class="form-sec-row">
                    <label><strong> 
                        Expire On :</strong></label>
                        <asp:TextBox ID="TextBox2" runat="server" CssClass="textbox-medium1" Visible="false"></asp:TextBox>
                      <asp:DropDownList ID="ddlmonth" runat="server"  CssClass="textbox-medium1" Width="150px"></asp:DropDownList>
                      <asp:DropDownList ID="ddlyear" runat="server"  CssClass="textbox-medium1"  Width="150px"></asp:DropDownList>
                    <div class="clear">
                    </div>
                </div>
                
                    <div class="form-sec-row">
                    <label><strong> 
                        Opening Stock :</strong></label>
                        <asp:TextBox ID="TextBox3" runat="server" CssClass="textbox-medium1"></asp:TextBox>
                    <div class="clear">
                    </div>
                </div>

                <div class="form-sec-row">
                    <label><strong> 
                        MRP :</strong></label>
                        <asp:TextBox ID="TextBox4" runat="server" CssClass="textbox-medium1"></asp:TextBox>
                    <div class="clear">
                    </div>
                </div>
                            
                <div class="form-sec-row">
                    <label><strong> </strong></label>
                    <asp:Button ID="Button1" runat="server" Text="Submit" Height="28px" 
                        CssClass="submit-button" onclick="Button1_Click" />
                     <div class="clear">
                    </div>
                </div>   

                </div>
                  </asp:View>
                     <asp:View ID="View2" runat="server">

                   
                    <label><strong>
                     Select A File :</strong></label>
                            <asp:FileUpload ID="FileUpload1" runat="server" />
       
                 

                        <div class="form-sec-row">
                    <label><strong> </strong></label>
                    <asp:Button ID="Button2" runat="server" Text="Import From Excel" Height="28px" 
                        CssClass="submit-buttonCheck" onclick="Button2_Click"  />
                     <div class="clear">
                    </div>
                </div> 

                    <div class="form-sec-row">
                   <asp:GridView ID="GridView1"  Width="100%"  runat="server" HeaderStyle-CssClass="producthead1">
        </asp:GridView> 
                     <div class="clear">
                    </div>
                </div> 
                           <div class="form-sec-row">
                    <label><strong> </strong></label>
                    <asp:Button ID="Button3" runat="server" Text="Submit" Height="28px" 
                        CssClass="submit-button" onclick="Button3_Click"  />
                     <div class="clear">
                    </div>
                </div>   
                     </asp:View>
                       <asp:View ID="View3" runat="server">
                           <div class="listing" style='height:400px; overflow:auto;'>
                           <asp:GridView ID="GridView2" runat="server" AutoGenerateColumns="false" DataKeyNames="ICODE"  CssClass="grid" PagerStyle-CssClass="pgr" 
                               OnRowEditing="GridView2_RowEditing" AllowPaging="True" PageSize="50"  Width="100%" OnRowCommand="GridView2_RowCommand" OnPageIndexChanging="GridView2_PageIndexChanging">
                   
                               <Columns>
                                   <asp:TemplateField HeaderText="Medicine">
                                       <ItemTemplate>
                                       <asp:Label runat="server" ID="lblMedCode" Text='<%# Bind("ICODE") %>' Visible="false"></asp:Label>
                                       <asp:Label runat="server" ID="lblMedId" Text='<%# Bind("MedicineID") %>' Visible="false"></asp:Label>
                                       <asp:Label runat="server" ID="lblMedName" Text='<%# Bind("MedicineName")  %>'></asp:Label>
                                       </ItemTemplate>
                                   </asp:TemplateField>
                                   <asp:TemplateField HeaderText="BatchNo">
                                       <ItemTemplate>
                                       <asp:Label runat="server" ID="lblMBatchno" Text='<%# Bind("BATCHNO")  %>'></asp:Label>
                                       </ItemTemplate>
                                   </asp:TemplateField>
                                   <asp:TemplateField HeaderText="Expiry Date">
                                       <ItemTemplate>
                                       <asp:Label runat="server" ID="lblExpdt" Text='<%# Bind("EXPDATE")  %>'></asp:Label>
                                       </ItemTemplate>
                                   </asp:TemplateField>
                                   <asp:TemplateField HeaderText="Opening Stock">
                                       <ItemTemplate>
                                       <asp:Label runat="server" ID="lblOpstock" Text='<%# Bind("OPSTOCK")  %>' Style="text-align:right;"></asp:Label>
                                       </ItemTemplate>
                                   </asp:TemplateField>
                                   <asp:TemplateField HeaderText="MRP">
                                       <ItemTemplate>
                                       <asp:Label runat="server" ID="lblMrp" Text='<%# Bind("MRP")  %>' Style="text-align:right;"></asp:Label>
                                       </ItemTemplate>
                                   </asp:TemplateField>
                            <asp:CommandField SelectText="Edit" ShowSelectButton="True" ItemStyle-Width="70px"  ItemStyle-HorizontalAlign="Center"   ControlStyle-CssClass="temp" HeaderText="Edit" SelectImageUrl="~/Images/edit.png" ButtonType="Image">
                                <ControlStyle CssClass="temp"></ControlStyle>
                            </asp:CommandField>
                               </Columns>

                           </asp:GridView>
                            </div>
                       </asp:View>
                     </asp:MultiView>
                </div>
<%--    </ContentTemplate>
    </asp:UpdatePanel>--%>
</asp:Content>

