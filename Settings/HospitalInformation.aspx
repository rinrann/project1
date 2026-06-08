<%@ Page Title="" Language="C#" MasterPageFile="~/MasterAll/MasterPageAll.master" AutoEventWireup="true" CodeFile="HospitalInformation.aspx.cs" Inherits="Assignment_HospitalInformation" %>


<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">

<script language="javascript" type="text/javascript">

    function Calling() {
        var date = new Date();
        $("input[id$='txtvalidityDate']").datepicker({ dateFormat: 'dd/mm/yy' });
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



    

          <div class="pageheader">
                <asp:Label ID="Label1" runat="server">Hospital Details</asp:Label>
            </div>



              <table width="290px" cellpadding="0" cellspacing="0" style="display:none;">
         <tr>
            <td>
                <asp:Button Text="Entry" BorderStyle="None" ID="Tab1" Width="145px"  Height="40px"  CssClass="Initial" runat="server" onclick="Tab1_Click"/>
                    </td>
                     <td>
                <asp:Button Text="List" BorderStyle="None" ID="Tab2" CssClass="Initial"  Width="145px"  Height="40px" runat="server" onclick="Tab2_Click"/></td>
                     </tr>
                     </table>


                         <div class="formbox">
                           
                             
                                 
                                <div class="form-sec">

                             
                               <asp:HiddenField ID="HiddenField1" runat="server" />
                            

                                     <div class="form-sec-row">
                        <label><strong> Name Of the Institution :</strong></label>
                        <asp:TextBox ID="txtInstituteName" runat="server" CssClass="textbox-medium1"  ></asp:TextBox>
                        <div class="clear">
                        </div>
                          </div>



                        <div class="form-sec-row">
                        <label><strong>
                        Catagory :</strong></label>
                        <asp:TextBox ID="txtCatagory" runat="server" CssClass="textbox-medium1"  ></asp:TextBox>
                        <div class="clear">
                        </div>
                          </div>

                 <div class="form-sec-row">
                <label style='color:Blue'><strong> Address Details:</strong></label>
                <div class="clear"></div>
            </div>


            <div class="form-sec-row">
                        <label><strong> Address :</strong></label>
                        <asp:TextBox ID="txtAddress" runat="server" CssClass="textbox-medium1"  ></asp:TextBox>
                        <div class="clear">
                        </div>
                          </div>

                           <div class="form-sec-row">
                        <label><strong> Email :</strong></label>
                        <asp:TextBox ID="txtEmail" runat="server" CssClass="textbox-medium1"  ></asp:TextBox>
                        <div class="clear">
                        </div>
                          </div>

                          <div class="form-sec-row">
                        <label><strong> Phone 1:</strong></label>
                        <asp:TextBox ID="txtPhone1" runat="server" CssClass="textbox-medium1"  ></asp:TextBox>
                        <div class="clear">
                        </div>
                          </div>


                          <div class="form-sec-row">
                        <label><strong> Phone 2:</strong></label>
                        <asp:TextBox ID="txtPhone2" runat="server" CssClass="textbox-medium1"  ></asp:TextBox>
                        <div class="clear">
                        </div>
                          </div>

                          <div class="form-sec-row">
                        <label><strong> Fax:</strong></label>
                        <asp:TextBox ID="txtFaxNo" runat="server" CssClass="textbox-medium1"  ></asp:TextBox>
                        <div class="clear">
                        </div>
                          </div>



                          <div class="form-sec-row">
                        <label><strong> Website:</strong></label>
                        <asp:TextBox ID="txtWebsite" runat="server" CssClass="textbox-medium1"  ></asp:TextBox>
                        <div class="clear">
                        </div>
                          </div>


                            <div class="form-sec-row">
                <label style='color:Blue'><strong> Bed Details:</strong></label>
                <div class="clear"></div>
            </div>

             <div class="form-sec-row">
                        <label><strong> ICU:</strong></label>
                        <asp:TextBox ID="txticu" runat="server" CssClass="textbox-medium1"  ></asp:TextBox>
                        <div class="clear">
                        </div>
                          </div>

                           <div class="form-sec-row">
                        <label><strong> Dialysis:</strong></label>
                        <asp:TextBox ID="txtdialysis" runat="server" CssClass="textbox-medium1"  ></asp:TextBox>
                        <div class="clear">
                        </div>
                          </div>

                           <div class="form-sec-row">
                        <label><strong> Nicu:</strong></label>
                        <asp:TextBox ID="txtNicu" runat="server" CssClass="textbox-medium1"  ></asp:TextBox>
                        <div class="clear">
                        </div>
                          </div>

                          <div class="form-sec-row">
                        <label><strong> General Ward:</strong></label>
                        <asp:TextBox ID="txtGward" runat="server" CssClass="textbox-medium1"  ></asp:TextBox>
                        <div class="clear">
                        </div>
                          </div>


                           <div class="form-sec-row">
                        <label><strong> Cabin:</strong></label>
                        <asp:TextBox ID="txtCabin" runat="server" CssClass="textbox-medium1"  ></asp:TextBox>
                        <div class="clear">
                        </div>
                          </div>

                           <div class="form-sec-row">
                        <label><strong> Delux:</strong></label>
                        <asp:TextBox ID="txtDelux" runat="server" CssClass="textbox-medium1"  ></asp:TextBox>
                        <div class="clear">
                        </div>
                          </div>

                           <div class="form-sec-row">
                        <label><strong> HDU:</strong></label>
                        <asp:TextBox ID="txtHdu" runat="server" CssClass="textbox-medium1"  ></asp:TextBox>
                        <div class="clear">
                        </div>
                          </div>

                           <div class="form-sec-row">
                        <label><strong> Duplex:</strong></label>
                        <asp:TextBox ID="txtDuplex" runat="server" CssClass="textbox-medium1"  ></asp:TextBox>
                        <div class="clear">
                        </div>
                          </div>                     


                                <div class="form-sec-row">
                <label style='color:Blue'><strong> Others:</strong></label>
                <div class="clear"></div>
            </div>

            <div class="form-sec-row">
                        <label><strong> Hospital Logo1:</strong></label>
                <asp:FileUpload ID="FileUpload1" runat="server" />
                <strong><asp:Label ID="lblLogo" runat="server" Text=""></asp:Label></strong>
                <asp:HiddenField ID="hdnLogo" runat="server" />
                        <div class="clear">
                        </div>
                          </div>
            <div class="form-sec-row">
                        <label><strong> Hospital Logo2:</strong></label>
                <asp:FileUpload ID="FileUpload2" runat="server" />
                <strong><asp:Label ID="lblLogo2" runat="server" Text=""></asp:Label></strong>
                <asp:HiddenField ID="hdnLogo2" runat="server" />
                        <div class="clear">
                        </div>
                          </div>
            <div class="form-sec-row">
                        <label><strong> Lisence No:</strong></label>
                        <asp:TextBox ID="txtLisenseNo" runat="server" CssClass="textbox-medium1"  ></asp:TextBox>
                        <div class="clear">
                        </div>
                          </div>
                                    
            <div class="form-sec-row">
                        <label><strong>Hospital Regn No:</strong></label>
                        <asp:TextBox ID="txtHosrgno" runat="server" CssClass="textbox-medium1"  ></asp:TextBox>
                        <div class="clear">
                        </div>
                          </div>

                           <div class="form-sec-row">
                        <label><strong> Validity Date:</strong></label>
                        <asp:TextBox ID="txtvalidityDate" runat="server" CssClass="textbox-medium1"></asp:TextBox>
                        <div class="clear">
                        </div>
                          </div>


                           <div class="form-sec-row">
                        <label><strong> RMO:</strong></label>
                        <asp:TextBox ID="txtrmo" runat="server" CssClass="textbox-medium1"  ></asp:TextBox>
                        <div class="clear">
                        </div>
                          </div>
                        <div class="form-sec-row">
                        <label><strong> Patient Reg No Prefix:</strong></label>
                        <asp:TextBox ID="txtregnoPrfx" runat="server" CssClass="textbox-medium1"  ></asp:TextBox>
                        <div class="clear">
                        </div>
                          </div>

                        <div class="form-sec-row">
                <label style='color:Blue'><strong> Settings:</strong></label>
                <div class="clear"></div>
                </div>    
                        <div class="form-sec-row">
                        <label><strong> Commision Calculation Rule:</strong></label>
                            <asp:DropDownList ID="ddlCommCalRule" runat="server" CssClass="textbox-medium1" >
                                <asp:ListItem Value="0">Select</asp:ListItem>
                                <asp:ListItem Value="A">Including All Doctor Charges</asp:ListItem>
                                <asp:ListItem Value="B">Deducting All Doctor Charges</asp:ListItem>
                            </asp:DropDownList>
                        <div class="clear">
                        </div>
                          </div> 
                        <div class="form-sec-row">
                        <label><strong> Requisition for a single Patient:</strong></label>
                            <asp:DropDownList ID="ddlReqNo" runat="server" CssClass="textbox-medium1" >
                                <asp:ListItem Value="0">Select</asp:ListItem>
                                <asp:ListItem Value="S">Single</asp:ListItem>
                                <asp:ListItem Value="M">Multiple</asp:ListItem>
                            </asp:DropDownList>
                        <div class="clear">
                        </div>
                          </div>
                        
                        <div class="form-sec-row">
                        <label><strong> Medicine Issue:</strong></label>
                            <asp:DropDownList ID="ddlMedcineIssue" runat="server" CssClass="textbox-medium1" >
                                <asp:ListItem Value="0">Select</asp:ListItem>
                                <asp:ListItem Value="W">Work Station/ Wing wise</asp:ListItem>
                                <asp:ListItem Value="P">Patient Wise</asp:ListItem>
                            </asp:DropDownList>
                        <div class="clear">
                        </div>
                          </div> 

                        

                           <div class="form-sec-row">
                        <div class="clear">
                        </div>
                    </div>
                    <div class="form-sec-row">
                        &nbsp;&nbsp;&nbsp;<label><strong>&nbsp;</strong></label>
                        <asp:Button ID="btnSubmit" runat="server" CssClass="submit-button"  
                            Height="28px" Text="Save" onclick="btnSubmit_Click" />
                        <asp:Button ID="txtCancel" runat="server" CssClass="submit-button"   Height="28px" Text="Cancel" />
                        <div class="clear">
                        </div>
                    </div>
                    <div class="clear">
                    </div>


                                   </div>
                              


                               


        <div id="scroll" class="listing"  style="overflow: auto; width:100%; height: 200px;display:none;" >
                <asp:GridView id="GridView1"  Width="100%" CssClass="grid" PagerStyle-CssClass="pgr"
                  runat="server"  DataKeyNames ="Hid"
                 AutoGenerateColumns="False" AllowPaging="True"  PageSize="100"
                 SelectedRowStyle-BackColor="GreenYellow" onrowcommand="GridView1_RowCommand" 
                    onrowdeleting="GridView1_RowDeleting" >
                    <RowStyle HorizontalAlign="Center" />
                    <Columns>

                           <asp:TemplateField HeaderText="HID" Visible="false">
                            <ItemTemplate>
                                <asp:Label ID="lblHid" runat="server" Text='<%# Bind("Hid") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="Name" >
                            <ItemTemplate>
                                <asp:Label ID="lblInstitutionName" runat="server" Text='<%# Bind("InstitutionName") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>

                         <asp:TemplateField HeaderText="Catagory" >
                            <ItemTemplate>
                                <asp:Label ID="lblCatagory" runat="server" Text='<%# Bind("Catagory") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>

                          <asp:TemplateField HeaderText="Address">
                            <ItemTemplate>
                                <asp:Label ID="lblAddress" runat="server" Text='<%# Bind("Address") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>

                          <asp:TemplateField HeaderText="Email">
                            <ItemTemplate>
                                <asp:Label ID="lblEmail" runat="server" Text='<%# Bind("Email") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>

                         <asp:TemplateField HeaderText="Ph1">
                            <ItemTemplate>
                                <asp:Label ID="lblPh1" runat="server" Text='<%# Bind("Ph1") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>

                          <asp:TemplateField HeaderText="Ph2" Visible="false">
                            <ItemTemplate>
                                <asp:Label ID="lblPh2" runat="server" Text='<%# Bind("Ph2") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>

                          <asp:TemplateField HeaderText="Fax" Visible="false">
                            <ItemTemplate>
                                <asp:Label ID="lblFax" runat="server" Text='<%# Bind("Fax") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>

                         <asp:TemplateField HeaderText="website" Visible="false">
                            <ItemTemplate>
                                <asp:Label ID="lblwebsite" runat="server" Text='<%# Bind("website") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>

                          <asp:TemplateField HeaderText="ICU">
                            <ItemTemplate>
                                <asp:Label ID="lblicu" runat="server" Text='<%# Bind("icu") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>

                         <asp:TemplateField HeaderText="Dialysis">
                            <ItemTemplate>
                                <asp:Label ID="lbldialysis" runat="server" Text='<%# Bind("dialysis") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>

                         

                          <asp:TemplateField HeaderText="Nicu">
                            <ItemTemplate>
                                <asp:Label ID="lblnicu" runat="server" Text='<%# Bind("nicu") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>

                             <asp:TemplateField HeaderText="Gn. Ward">
                            <ItemTemplate>
                                <asp:Label ID="lblgeneralward" runat="server" Text='<%# Bind("generalward") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>

                          <asp:TemplateField HeaderText="Cabin">
                            <ItemTemplate>
                                <asp:Label ID="lblcabin" runat="server" Text='<%# Bind("cabin") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>

                        
                          <asp:TemplateField HeaderText="Delux">
                            <ItemTemplate>
                                <asp:Label ID="lbldelux" runat="server" Text='<%# Bind("delux") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>

                       

                         <asp:TemplateField HeaderText="HDU">
                            <ItemTemplate>
                                <asp:Label ID="lblhdu" runat="server" Text='<%# Bind("hdu") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="Duplex">
                            <ItemTemplate>
                                <asp:Label ID="lblDuplex" runat="server" Text='<%# Bind("Duplex") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>

  
                         <asp:TemplateField HeaderText="Total Bed" >
                            <ItemTemplate>
                                <asp:Label ID="lblTotalBed" runat="server" Text='<%# Bind("TotalBed") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>

                         <asp:TemplateField HeaderText="lisenceno" Visible="false">
                            <ItemTemplate>
                                <asp:Label ID="lbllisenceno" runat="server" Text='<%# Bind("lisenceno") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>

                            <asp:TemplateField HeaderText="validity" Visible="false">
                            <ItemTemplate>
                                <asp:Label ID="lblvalidity" runat="server" Text='<%# Bind("validity") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="Rmo" Visible="false">
                            <ItemTemplate>
                                <asp:Label ID="lblRmo" runat="server" Text='<%# Bind("Rmo") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Prefix" Visible="false">
                            <ItemTemplate>
                                <asp:Label ID="lblPre" runat="server" Text='<%# Bind("RegNoPrifix") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="MedicineIssue" Visible="false">
                            <ItemTemplate>
                                <asp:Label ID="lblMedicineIssue" runat="server" Text='<%# Bind("MedicineIssue") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="CommCalcRule" Visible="false">
                            <ItemTemplate>
                                <asp:Label ID="lblCommCalcRule" runat="server" Text='<%# Bind("CommCalcRule") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="ReqNoRule" Visible="false">
                            <ItemTemplate>
                                <asp:Label ID="lblReqNoRule" runat="server" Text='<%# Bind("ReqNoRule") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>

   
                        <asp:CommandField SelectText="Edit" ShowSelectButton="True"  ItemStyle-Width="30px"   ControlStyle-CssClass="temp" HeaderText="Edit" SelectImageUrl="~/Images/edit.png" ButtonType="Image">
                            <ControlStyle CssClass="temp"></ControlStyle>
                        </asp:CommandField>


                        <asp:CommandField ShowDeleteButton="True" ControlStyle-CssClass="temp"   ItemStyle-Width="30px"  HeaderText="Delete" DeleteImageUrl="~/Images/delete.png" ButtonType="Image">
                            <ControlStyle CssClass="temp"></ControlStyle>
                        </asp:CommandField>


                   


                    </Columns>
                    <PagerStyle CssClass="pgr" />
                    <EditRowStyle BackColor="#CCFF33" />
                    <AlternatingRowStyle BackColor="#FFDB91" />
                </asp:GridView>
            </div>



                                 
                            
                         </div> 




        

</asp:Content>

