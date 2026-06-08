<%@ Page Language="C#"  MasterPageFile="~/MasterAll/MasterPageAll.master" AutoEventWireup="true" CodeFile="PatientRegistrationList.aspx.cs" Inherits="OPD_PatientRegistrationList" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">

    <div class="pageheader">
         <asp:Label ID="Label1" runat="server">Patient List</asp:Label>
    </div>
    <div class="formbox">
        <div class="form-sec">
            <div>
                <table cellpadding="0" cellspacing="0"  title="Search" width="100%">
                    <tr>
                        
                        <td style="width:10%;">
                            <label class="ipdList" style='width:75px;'><strong>Regintration Date From:</strong></label>
                        </td>
                        <td style="width:15%;">
                            <asp:TextBox ID="txtFrmDate" runat="server" CssClass="textbox-medium1" Width="138px" TextMode="Date"></asp:TextBox>
                            
                        </td>
                        <td style="width:10%;">
                            <label class="ipdList" style='width:75px;'><strong>Regintration Date To:</strong></label>
                        </td>
                        <td style="width:15%;">
                            <asp:TextBox ID="txtToDate" runat="server" CssClass="textbox-medium1" Width="138px" TextMode="Date"></asp:TextBox>
                            
                        </td>
                        <%--<td style="width:10%;">
                           
                        </td>--%>
                
                    
                
                
                        
                        <td style="width:10%;">
                                <asp:Button ID="btnGenRpt" runat="server" Text="Generate Report" CssClass="submit-generate" OnClick="btnGenRpt_Click"/>          
                        </td>         
                    </tr>
                </table>

                <table width="100%">
                <tr>        
                    <td align="center">  
                       <div id="mydiv" style="text-align: left;width:100%;">
                            <asp:Literal ID="ltrReport" runat="server"></asp:Literal>
                        </div>                 
                    </td>
                </tr>
                <tr>
                    <td align="center">
                        <asp:Button ID="BtnBack" runat="server" style="width:70px;height:21px; font-size:x-small" Text="Back" OnClick="BtnBack_Click"  />
                        <%--<asp:Button ID="Button2" runat="server" Text="Back"   CssClass="submit-button" OnClick="Button2_Click" />--%>
                        <asp:Button ID="btn_excel" runat="server" style="width:100px; height:21px; font-size:x-small" Text="Export to Excel" OnClick="btn_excel_Click" />
                    </td>
                </tr>
            </table>
        
            </div>
        </div>
    </div>
</asp:Content>
