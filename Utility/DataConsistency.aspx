<%@ Page Title="" Language="C#" MasterPageFile="~/MasterAll/MasterPageAll.master" AutoEventWireup="true" CodeFile="DataConsistency.aspx.cs" Inherits="Master_DataConsistency" %>


<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <script language="javascript" type="text/javascript">
    </script>

 <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>

          <div class="pageheader">
                <asp:Label ID="Label1" runat="server">Data Consistency</asp:Label>
            </div>
            <div class="formbox">
<div class="form-sec-row" style="margin-left:300px;">
                        &nbsp;&nbsp;&nbsp;<label><strong>&nbsp;</strong></label>
                        <asp:Button ID="btnSubmit" runat="server" CssClass="submit-button"  
                            Height="28px" Text="Proceed" onclick="btnSubmit_Click" />
                        
                        
                    </div>
                    <div class="clear">
                    </div>
                </div>
</ContentTemplate>
    </asp:UpdatePanel>

</asp:Content>

