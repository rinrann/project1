<%@ Page Language="C#" MasterPageFile="~/MasterAll/MasterPageAll.master" AutoEventWireup="true" CodeFile="AccountSetup.aspx.cs" Inherits="Master_AccountSetup" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <style>
.BigWidth
{
    width:1200px; 
}
        /*.wrapper {
        width:1200px; 
        }*/
</style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">

    <script type="text/javascript">

    </script>
    <asp:UpdateProgress ID="UpdateProgress1" runat="server">
    <ProgressTemplate>
    
       <div id="aaa" class="progressBackgroundFilter">          </div>
        <div id="bbb" class="processMessage"><img alt="Loading" src="../images/pwait.gif"/>
      
              </div>
    </ProgressTemplate>
    </asp:UpdateProgress>
   <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <div class="pageheader">
                <asp:Label ID="Label1" runat="server">Account Setup</asp:Label>
            </div>
        <div class="formbox" style="width:1000px; padding:0px;margin-left:0px;" >
            
                        <div class="form-sec">
                            <div class="error">
                                <strong>
                                <asp:Label ID="lblError" runat="server" Font-Bold="True" CssClass="fadeout"></asp:Label>
                                </strong>
                                <div class="clear">
                                </div>
                            </div>
                        </div>

                        <div class="form-sec-row">
                            <table width="100%" cellpadding="0" cellspacing="0">
                                <tr>
                                    <td width="25%" >
                                        <label style="width:100%;"><strong>Link With Account :</strong></label>
                                    </td>
                                    <td width="15%">
                                        <asp:CheckBox ID="chkLink" runat="server" />
                                    </td>
                                    <td width="20%">
                                        &nbsp;
                                    </td>
                                    <td width="25%" >
                                        <label style="width:100%;"><strong>Journal Book Code :</strong></label>
                                    </td>
                                    <td width="15%">
                                        <asp:DropDownList ID="ddlJournal" runat="server" CssClass="textbox-medium1" >
                                            </asp:DropDownList>
                                    </td>
                                </tr>
                                <tr>
                                    <td width="25%">
                                        <label style="width:100%;"><strong>Patient GL:</strong></label>
                                    </td>
                                    <td width="15%">
                                        <asp:DropDownList ID="ddlpatient" runat="server" CssClass="textbox-medium1">
                                            </asp:DropDownList>
                                    </td>
                                     <td width="20%">
                                        &nbsp;
                                    </td>
                                    <td width="25%">
                                        <label style="width:100%;"><strong>Doctor GL:</strong></label>
                                    </td>
                                    <td width="15%">
                                        <asp:DropDownList ID="ddldoctor" runat="server" CssClass="textbox-medium1">
                                            </asp:DropDownList>
                                    </td>
                                </tr>
                            </table>
                            <table width="100%" cellpadding="0" cellspacing="0" style="background-color:#FB7B13; color:#FFF;">
                                <tr>
                                    <td width="100%" >
                                        <label style="width:100%;"><strong>   Patient</strong></label>
                                    </td>
                                </tr>
                            </table>
                            <table width="100%" cellpadding="0" cellspacing="0">
                                 <tr>
                                    <td width="25%" valign="top">
                                        <label style="width:100%;"><strong>Bed Charges GL</strong></label>
                                    </td>
                                     <td width="15%" valign="top">
                                         <asp:DropDownList ID="ddlbedchrg" runat="server" CssClass="textbox-medium1">
                                            </asp:DropDownList>
                                    </td>
                                     <td width="20%">
                                        &nbsp;
                                    </td>
                                     <td width="25%">
                                        <label style="width:100%;"><strong>Consumable Charges GL</strong></label>
                                    </td>
                                     <td width="15%" valign="top">
                                         <asp:DropDownList ID="ddlconschrg" runat="server" CssClass="textbox-medium1">
                                            </asp:DropDownList>
                                    </td>
                                </tr>
                                <tr>
                                     <td width="25%" valign="top">
                                        <label style="width:100%;"><strong>Doctor Visit Charges GL</strong></label>
                                    </td>
                                     <td width="15%" valign="top">
                                         <asp:DropDownList ID="ddldocvisit" runat="server" CssClass="textbox-medium1">
                                            </asp:DropDownList>
                                    </td>
                                
                                    <td width="20%">
                                        &nbsp;
                                    </td>
                                     <td width="25%" valign="top">
                                        <label style="width:100%;"><strong>Medicin Charges GL</strong></label>
                                    </td>
                                     <td width="15%" valign="top">
                                         <asp:DropDownList ID="ddlmedicin" runat="server" CssClass="textbox-medium1">
                                            </asp:DropDownList>
                                    </td>
                                </tr>
                                   <td width="25%" valign="top">
                                        <label style="width:100%;"><strong>Pathology Charges GL</strong></label>
                                    </td>
                                     <td width="13%" valign="top">
                                         <asp:DropDownList ID="ddlpatho" runat="server" CssClass="textbox-medium1">
                                            </asp:DropDownList>
                                    </td>
                                    <td width="20%">
                                        &nbsp;
                                    </td>
                                     <td width="25%" valign="top">
                                        <label style="width:100%;"><strong>Services Charges GL</strong></label>
                                    </td>
                                     <td width="15%" valign="top">
                                         <asp:DropDownList ID="ddlservice" runat="server" CssClass="textbox-medium1">
                                            </asp:DropDownList>
                                    </td>
                                </tr>
                                <tr>
                                    <td width="25%" valign="top">
                                        <label style="width:100%;"><strong>Sister/Aya Charges GL</strong></label>
                                    </td>
                                     <td width="15%">
                                         <asp:DropDownList ID="ddlsister" runat="server" CssClass="textbox-medium1">
                                            </asp:DropDownList>
                                    </td>
                                    <td width="20%">
                                        &nbsp;
                                    </td>
                                     <td width="25%" valign="top">
                                        <label style="width:100%;"><strong>Usg Charges GL</strong></label>
                                    </td>
                                     <td width="15%" valign="top">
                                         <asp:DropDownList ID="ddlusg" runat="server" CssClass="textbox-medium1">
                                            </asp:DropDownList>
                                    </td>
                                </tr>
                                <tr>
                                    <td width="25%" valign="top">
                                        <label style="width:100%;"><strong>X-Ray Charges GL</strong></label>
                                    </td>
                                     <td width="15%" valign="top">
                                         <asp:DropDownList ID="ddlxray" runat="server" CssClass="textbox-medium1">
                                            </asp:DropDownList>
                                    </td>
                                    <td width="20%">
                                        &nbsp;
                                    </td>
                                
                                    <td width="25%" valign="top">
                                        <label style="width:100%;"><strong>OT Charges GL</strong></label>
                                    </td>
                                     <td width="15%" valign="top">
                                         <asp:DropDownList ID="ddlotchrg" runat="server" CssClass="textbox-medium1">
                                            </asp:DropDownList>
                                    </td>
                                </tr>
                                <tr>
                                    <td width="25%" valign="top">
                                      <label style="width:100%;"><strong>OT Consumable Charges GL</strong></label>
                                    </td>
                                    <td width="15%" valign="top">
                                         <asp:DropDownList ID="ddlotconschrg" runat="server" CssClass="textbox-medium1">
                                            </asp:DropDownList>
                                    </td>
                                    <td width="20%">
                                        &nbsp;
                                    </td>
                                    <td width="25%" valign="top">
                                      <label style="width:100%;"><strong>OT Instrument Charges GL</strong></label>
                                    </td>
                                    <td width="15%" valign="top">
                                         <asp:DropDownList ID="ddlotinstchrg" runat="server" CssClass="textbox-medium1">
                                            </asp:DropDownList>
                                    </td>
                                </tr>
                                <tr>
                                    <td width="25%" valign="top">
                                      <label style="width:100%;"><strong>OT Attendance Charges GL</strong></label>
                                    </td>
                                    <td width="15%" valign="top">
                                         <asp:DropDownList ID="ddlotattenchrg" runat="server" CssClass="textbox-medium1">
                                            </asp:DropDownList>
                                    </td>
                                       <td width="20%">
                                        &nbsp;
                                    </td>                          
                                     <td width="25%" valign="top">
                                        <label style="width:100%;"><strong>Anesthesia Medicin Charges GL</strong></label>
                                    </td>
                                     <td width="15%" valign="top">
                                         <asp:DropDownList ID="ddlanesthesia" runat="server" CssClass="textbox-medium1">
                                            </asp:DropDownList>
                                    </td>
                                </tr>
                                <tr>
                                    <td width="25%" valign="top">
                                        <label style="width:100%;"><strong>Anesthesia Consumable Charges GL</strong></label>
                                    </td>
                                     <td width="15%" valign="top">
                                         <asp:DropDownList ID="ddlanesconschrg" runat="server" CssClass="textbox-medium1">
                                            </asp:DropDownList>
                                    </td>
                                    <td width="20%">
                                        &nbsp;
                                    </td>
                                
                                    <td width="25%" valign="top">
                                        <label style="width:100%;"><strong>Ambulance Charges GL</strong></label>
                                    </td>
                                     <td width="15%" valign="top">
                                         <asp:DropDownList ID="ddlambulance" runat="server" CssClass="textbox-medium1">
                                            </asp:DropDownList>
                                    </td>
                                </tr>
                                <tr>
                                    <td width="25%" valign="top">
                                        <label style="width:100%;"><strong>Dialysis Fees GL</strong></label>
                                    </td>
                                     <td width="15%" valign="top">
                                         <asp:DropDownList ID="ddldialysis" runat="server" CssClass="textbox-medium1">
                                            </asp:DropDownList>
                                    </td>
                                    <td width="20%">
                                        &nbsp;
                                    </td>
                                    <td width="25%" valign="top">
                                        <label style="width:100%;"><strong>OPD Fees GL</strong></label>
                                    </td>
                                     <td width="15%" valign="top">
                                         <asp:DropDownList ID="ddlopdfees" runat="server" CssClass="textbox-medium1">
                                            </asp:DropDownList>
                                    </td>
                                </tr>
                                <tr>
                                    <td width="25%" valign="top">
                                        <label style="width:100%;"><strong>Disposable Fees GL</strong></label>
                                    </td>
                                     <td width="15%" valign="top">
                                         <asp:DropDownList ID="ddldisposable" runat="server" CssClass="textbox-medium1">
                                            </asp:DropDownList>
                                    </td>
                                    <td width="20%">
                                        &nbsp;
                                    </td>
                                    <td width="25%" valign="top">
                                        <label style="width:100%;"><strong>Other Charges GL</strong></label>
                                    </td>
                                     <td width="15%" valign="top">
                                         <asp:DropDownList ID="ddlothchrg" runat="server" CssClass="textbox-medium1">
                                            </asp:DropDownList>
                                    </td>
                                    
                                    
                                    
                                </tr>
                            </table>
                            <table width="100%" cellpadding="0" cellspacing="0" style="background-color:#FB7B13; color:#FFF;">
                                <tr>
                                    <td width="100%" >
                                        <label><strong>  Doctor</strong></label>
                                    </td>
                                </tr>
                            </table>
                            <table width="100%" cellpadding="0" cellspacing="0">
                                <tr>
                                    <td width="25%" valign="top">
                                        <label style="width:100%;"><strong>Surgeon Charges GL</strong></label>
                                    </td>
                                     <td width="15%" valign="top">
                                         <asp:DropDownList ID="ddlsurgeonchrg" runat="server" CssClass="textbox-medium1">
                                            </asp:DropDownList>
                                    </td>
                                    <td width="20%">
                                        &nbsp;
                                    </td>
                                    
                                    <td width="25%" valign="top">
                                        <label style="width:100%;"><strong>Visit Chages GL</strong></label>
                                    </td>
                                     <td width="15%" valign="top">
                                         <asp:DropDownList ID="ddlvisitchrg" runat="server" CssClass="textbox-medium1">
                                            </asp:DropDownList>
                                    </td>
                                </tr>
                                <tr>
                                    <td width="25%" valign="top">
                                        <label style="width:100%;"><strong>Anesthesis Chages GL</strong></label>
                                    </td>
                                     <td width="15%" valign="top">
                                         <asp:DropDownList ID="ddldocaneschrg" runat="server" CssClass="textbox-medium1">
                                            </asp:DropDownList>
                                    </td>
                                    <td width="20%">
                                        &nbsp;
                                    </td>
                                    
                                    <td width="25%" valign="top">
                                        <label style="width:100%;"><strong>Ref. Consul Chages GL</strong></label>
                                    </td>
                                     <td width="15%" valign="top">
                                         <asp:DropDownList ID="ddlrefconsulchrg" runat="server" CssClass="textbox-medium1">
                                            </asp:DropDownList>
                                    </td>
                                </tr>
                            </table>
                        </div>
                        
           
            
        </div>

             
 <div class="form-sec-row" style="margin-left:300px;">
                        &nbsp;&nbsp;&nbsp;<label><strong>&nbsp;</strong></label>
                        <asp:Button ID="btnSubmit" runat="server" CssClass="submit-button"  
                            Height="28px" Text="Save" onclick="btnSubmit_Click" />
                        
                        
                    </div>
   
    </ContentTemplate>
    </asp:UpdatePanel>

</asp:Content>

