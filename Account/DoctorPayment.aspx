<%@ Page Language="C#" MasterPageFile="~/MasterAll/MasterPageAll.master" AutoEventWireup="true" CodeFile="DoctorPayment.aspx.cs" Inherits="Account_DoctorPayment" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <style>
.BigWidth
{
    width:450px; 
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

        function ShowDialog() {

            var rtvalue = window.open("DocRegistrationPopup.aspx", "sss", "Width:800px; Height:550px; dialogLeft:250px;");
            //document.getElementById("ctl00_ContentPlaceHolder1_txtdocid").value = rtvalue.NameValue;
            //var val = rtvalue.ProfessionValue.split("#");
            //document.getElementById("ctl00_ContentPlaceHolder1_HiddenField1").value = val[2];

        }
    </script>

    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <div class="pageheader">
                <asp:Label ID="Label1" runat="server">Doctor Payment</asp:Label>
            </div>


            <div class="error">
                <strong><asp:Label ID="lblError" runat="server" Font-Bold="True"></asp:Label>
                </strong>
                <div class="clear"></div>
            </div>

            <asp:HiddenField ID="TextBox4" runat="server" />
            <asp:HiddenField ID="HiddenField1" runat="server" />

            <div class="formbox">
                <div class="form-sec-row">
                    <label style='color:Blue'><strong>Doctor's Details :</strong></label>
                    <div class="clear"></div>
                </div>

                <div class="form-sec-row">
                    <label><strong><!--Doctor -->Id :</strong></label>
                    <asp:TextBox ID="txtdocid" runat="server" CssClass="textbox-medium1" Enabled="False" EnableTheming="True"></asp:TextBox>
                    <asp:Button ID="btnQuickSearch" runat="server"  Height="28px" CssClass="submit-search" Text="Quick Search" OnClientClick="ShowDialog()"/>
                    <asp:Button ID="btnFetch" runat="server" CssClass="submit-button"    Height="28px" Text="Fetch" onclick="btnFetch_Click" />
                    <div class="clear">  </div>
                </div>

                <div class="form-sec-row">
                    <label><strong><!--Doctor -->Name :</strong></label>
                    <asp:TextBox ID="txtdocname" runat="server" CssClass="textbox-medium1" ></asp:TextBox>

                     <cc1:AutoCompleteExtender ServiceMethod="SearchCustomers"  CompletionListItemCssClass="BigWidth"  OnClientItemSelected="autoCompleteEx_ItemSelected"     MinimumPrefixLength="1"    CompletionInterval="100" EnableCaching="false" 
                   CompletionSetCount="10" TargetControlID="txtdocname"  ID="AutoCompleteExtender1" runat="server" FirstRowSelected = "false" ></cc1:AutoCompleteExtender>
             
                    <div class="clear"></div>
                </div>

                <div class="form-sec-row">
                    <label><strong>Address :</strong></label>  
                    <asp:TextBox ID="txtaddr" runat="server" CssClass="textbox-medium1" Enabled="False" ></asp:TextBox>
                    <div class="clear"></div>
                </div>

                <div class="form-sec-row">
                    <label><strong><!--Doctor -->Type :</strong></label>  
                    <asp:DropDownList ID="ddldoctype" runat="server" CssClass="textbox-medium1"></asp:DropDownList>
                    <div class="clear"></div>
                </div>

                <div class="form-sec-row">
                    <label><strong>Specialization :</strong></label>  
                    <asp:DropDownList ID="ddlsplz" runat="server" CssClass="textbox-medium1"></asp:DropDownList>
                    <div class="clear"></div>
                </div>


            </div>

            <div class="formbox">
                <div class="form-sec-row">
                    <label style='color:Blue'><strong>Bill Amount Details :</strong></label>
                    <div class="clear"></div>
                </div>

                <div class="form-sec-row" style="display:none;">
                    <label><strong>Total Bill Amount :</strong></label>  
                    <asp:TextBox ID="TextBox2" runat="server" CssClass="textbox-medium1" Enabled="False"></asp:TextBox>
                    <div class="clear"></div>
                </div>
                <div class="form-sec-row">
                    <label><strong>Surgeon Charges :</strong></label>  
                    <asp:TextBox ID="TextOT" runat="server" CssClass="textbox-medium1" Enabled="False"></asp:TextBox>
                    <asp:HiddenField ID="OTanalcode" runat="server" /><asp:HiddenField ID="OTcostcode" runat="server" />
                    <div class="clear"></div>
                </div>
                <div class="form-sec-row">
                    <label><strong>Anesthesis Charges :</strong></label>  
                    <asp:TextBox ID="TextAnes" runat="server" CssClass="textbox-medium1" Enabled="False"></asp:TextBox>
                    <asp:HiddenField ID="HiddenField3" runat="server" /><asp:HiddenField ID="HiddenField4" runat="server" />
                    <div class="clear"></div>
                </div>
                <div class="form-sec-row">
                    <label><strong>Visit Charges :</strong></label>  
                    <asp:TextBox ID="TextVisit" runat="server" CssClass="textbox-medium1" Enabled="False"></asp:TextBox>
                    <asp:HiddenField ID="VisitAnalcode" runat="server" /><asp:HiddenField ID="IpdCostcode" runat="server" />
                    <div class="clear"></div>
                </div>
                <div class="form-sec-row">
                    <label><strong>Refer Charges :</strong></label>  
                    <asp:TextBox ID="TextRefer" runat="server" CssClass="textbox-medium1" Enabled="False"></asp:TextBox>
                    <asp:HiddenField ID="ReferAnalcode" runat="server" /><asp:HiddenField ID="OpdCostcode" runat="server" />
                    <div class="clear"></div>
                </div>
                <div class="form-sec-row">
                    <label><strong>Total Bill Amount :</strong></label>  
                    <asp:TextBox ID="TextBox1" runat="server" CssClass="textbox-medium1" Enabled="False"></asp:TextBox>
                    <div class="clear"></div>
                </div>
            </div>

            <div class="formbox">
                <div class="form-sec-row">
                    <label style='color:Blue'><strong>Payment Sattlement :</strong></label>
                    <div class="clear"></div>
                </div>
                <div class="form-sec-row">
                    <label><strong>Payment Date :</strong></label>
                    <asp:TextBox ID="txtdate" runat="server" CssClass="textbox-medium1"></asp:TextBox>
                    <div class="clear"></div>
                </div>
                <div class="form-sec-row" style="display:;">
                    <label><strong>Current Payment :</strong></label>
                     <asp:TextBox ID="txtamt" runat="server" CssClass="textbox-medium1"></asp:TextBox>
                    <div class="clear"></div>
                 </div>

              <div class="form-sec-row" style="display:;">
                <label><strong>Discount Amount :</strong></label>
                 <asp:TextBox ID="txtdiscount" runat="server" CssClass="textbox-medium1" OnTextChanged="txtdiscount_TextChanged" AutoPostBack="true"></asp:TextBox>
                <div class="clear"></div>
            </div>
                
                <div class="form-sec-row" style="display:none;">
                    <label><strong>Payment for Surgeon Charges:</strong></label>
                     <asp:TextBox ID="txtamtOT" runat="server" CssClass="textbox-medium1"></asp:TextBox>
                    <div class="clear"></div>
                 </div>

              <div class="form-sec-row" style="display:none;">
                <label><strong>Discount for Surgeon Charges:</strong></label>
                 <asp:TextBox ID="txtdiscountOT" runat="server" CssClass="textbox-medium1"></asp:TextBox>
                <div class="clear"></div>
            </div>
                <div class="form-sec-row" style="display:none;">
                    <label><strong>Payment for Anesthesis Charges:</strong></label>
                     <asp:TextBox ID="txtamtAn" runat="server" CssClass="textbox-medium1"></asp:TextBox>
                    <div class="clear"></div>
                 </div>

              <div class="form-sec-row" style="display:none;">
                <label><strong>Discount for Anesthesis Charges:</strong></label>
                 <asp:TextBox ID="txtdiscountAn" runat="server" CssClass="textbox-medium1"></asp:TextBox>
                <div class="clear"></div>
            </div>
                <div class="form-sec-row" style="display:none;">
                    <label><strong>Payment for Visit Charges:</strong></label>
                     <asp:TextBox ID="txtamtVisit" runat="server" CssClass="textbox-medium1"></asp:TextBox>
                    <div class="clear"></div>
                 </div>

              <div class="form-sec-row" style="display:none;">
                <label><strong>Discount for Visit Charges:</strong></label>
                 <asp:TextBox ID="txtdiscountVisit" runat="server" CssClass="textbox-medium1"></asp:TextBox>
                <div class="clear"></div>
            </div>
                <div class="form-sec-row" style="display:none;">
                    <label><strong>Payment for Refer Charges:</strong></label>
                     <asp:TextBox ID="txtamtRefer" runat="server" CssClass="textbox-medium1"></asp:TextBox>
                    <div class="clear"></div>
                 </div>

              <div class="form-sec-row" style="display:none;">
                <label><strong>Discount for Refer Charges:</strong></label>
                 <asp:TextBox ID="txtdiscountRefer" runat="server" CssClass="textbox-medium1"></asp:TextBox>
                <div class="clear"></div>
            </div>

                <div class="form-sec-row">
                    <label><strong>Payment Mode :</strong></label>
                    <asp:DropDownList ID="ddlpaymode" runat="server" CssClass="textbox-medium1" AutoPostBack="True" OnSelectedIndexChanged="ddlpaymodeselectedIndexchng"></asp:DropDownList>
                    <div class="clear"></div>
                </div>
           <div class="form-sec-row" style="vertical-align: text-top">
                <asp:Label ID="lblchq" runat="server" Width="165px"><strong>Cheque Details:</strong></asp:Label>
                 <asp:TextBox ID="txtchqdtl" runat="server" CssClass="textbox-medium1" TextMode="MultiLine" Rows="5" Columns="50" Height="50px"> </asp:TextBox>
                <div class="clear"></div>
            </div>
           <div class="form-sec-row">
                <label><strong><asp:Label ID="Label7" runat="server" Text="Select Book:"></asp:Label></strong></label>
                    <span class="book" style="display:;"><asp:DropDownList ID="ddlBook" runat="server" CssClass="textbox-medium2"  >
                     </asp:DropDownList></span>
               <asp:HiddenField ID="HiddenField2" runat="server" Value="0"/>
                <div class="clear"></div>
            </div>

                

              <div class="form-sec-row">
                    <label><strong>&nbsp;</strong></label>
                    <asp:Button ID="btnSave" runat="server" OnClick="btnSave_Click"   Height="28px" Text="Submit" CssClass="submit-button" />
                    <asp:Button ID="btnCancel" runat="server" Text="Cancel"   Height="28px" OnClick="btnCancel_Click" CssClass="submit-button" />
                    <asp:Button ID="btnBack" runat="server" Text="Back"   Height="28px" OnClick="btnBack_Click" CssClass="submit-button" />
                    <div class="clear"></div>
            </div>
            </div>
            <asp:Panel ID="Panel1" runat="server">
                <table width="100%">
                    <tr>        
                        <td align="center">    <div id='mydiv'>              
                            <asp:Literal ID="ltrReport" runat="server"></asp:Literal><br />  </div>                  
                        </td>
                    </tr>
                    <tr>
                        <td align="center">
                     
                         <input type="button" value="Back" style="width:70px; font-size:x-small" onclick="window.history.back()" />
                         <input type="button" id="cmdPrint" value="Print" style="width:70px; font-size:x-small" onclick="javascript: printDiv('mydiv')"/></td>
                    </tr>
                </table>
            </asp:Panel>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
