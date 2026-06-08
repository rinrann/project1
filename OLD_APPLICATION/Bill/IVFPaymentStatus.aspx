<%@ Page Language="C#"  MasterPageFile="~/MasterAll/MasterPageAll.master" AutoEventWireup="true" CodeFile="IVFPaymentStatus.aspx.cs" Inherits="Bill_IVFPaymentStatus" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>


<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
    <ContentTemplate>
        </ContentTemplate>
    </asp:UpdatePanel>
    <div class="pageheader">
         <asp:Label ID="Label1" runat="server">IVF PAYMENT STATUS</asp:Label>
    </div>
    <div>
        <div class="form-sec">
            <div class="error">
                <strong><asp:Label ID="lblError" runat="server" Font-Bold="True"></asp:Label></strong>
                <div class="clear"></div>
            </div>
        </div>
        <table style="width:100%">
            <tr>
                <td style="width:10%">
                    <label><strong>Search Option :</strong></label> 
                </td>
                <td style="width:15%">
                    <asp:DropDownList ID="ddlopt" runat="server" OnSelectedIndexChanged="ddlopt_SelectedIndexChanged" AutoPostBack="true"  style="width:90%;">
                        <asp:ListItem Value="PN" Text="Patient Name"></asp:ListItem>
                        <asp:ListItem Value="RN" Text="Registration No"></asp:ListItem>
                        <asp:ListItem Value="PH" Text="Phone No"></asp:ListItem>
                        <asp:ListItem Value="PD" Text="Payment Date"></asp:ListItem>
                        <asp:ListItem Value="RD" Text="Test Date"></asp:ListItem>

                    </asp:DropDownList>
                </td>
                <%--<td style="width:10%">
                    <label><strong>Search Option :</strong></label> 
                </td>--%>
                <td style="width:12%">
                    <asp:DropDownList ID="ddlOperator" runat="server" OnSelectedIndexChanged="ddlOperator_SelectedIndexChanged" AutoPostBack="true"  style="width:80%;">
                        <asp:ListItem Value="E" Text="Equal"></asp:ListItem>
                    </asp:DropDownList>
                </td>
                <td style="width:5%">
                    <label><strong>Value :</strong></label> 
                </td>
                <td style="width:45%">
                    <asp:TextBox ID="txtSearchText" runat="server" style="width:150px"></asp:TextBox>
                    <asp:TextBox ID="txtfromdt" runat="server" TextMode="Date" Visible="false"></asp:TextBox>
                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                    <label id="lblmid" runat="server" visible="false"><strong>And</strong></label> 
                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                    <asp:TextBox ID="txttodt" runat="server" TextMode="Date"  Visible="false"></asp:TextBox>            
                </td>
                
                
                <td style="width:5%">
                    
                </td>
                <td style="width:8%;">  
                   <asp:Button ID="btnGenRpt" runat="server" Text="Generate Report" CssClass="submit-generate" OnClick="btnGenRpt_Click"/>

               </td>
            </tr>
        </table>
        <table width="100%">
        <tr>        
            <td align="center">  
               <h3 id="hd" runat="server" visible="false"> MONTHLY MIS REPORT</h3>
                  <div id='mydiv' style="overflow:auto;width:100%">              
                <asp:Literal ID="ltrReport" runat="server"></asp:Literal><br />

                                   </div>                  
            </td>
        </tr>
        <tr>
            <td align="center">
                   <asp:Button ID="BtnBack" runat="server" style="width:70px; font-size:x-small" Text="Back" OnClick="BtnBack_Click"  />
                <asp:Button ID="btn_excel" runat="server" style="width:100px; font-size:x-small" Text="Export to Excel" OnClick="btn_excel_Click" />
                <%--<input type="button" id="cmdPrint" value="Print" style="width:70px; font-size:x-small" onclick="javascript: printDiv('mydiv')"/>--%>

            </td>
        </tr>
        </table>
    </div>
</asp:Content>
