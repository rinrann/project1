<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="AccessDenied.aspx.cs" Inherits="AccessDenied" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
     <link rel="stylesheet" type="text/css" href="../css/style.css" />   
    <link rel="stylesheet" type="text/css" href="../css/jquery-ui.css" /> 
    <link type="text/css" href="css/calendar-blue.css" rel="stylesheet" />
     

    <%--<link href="css/Master.css" rel="stylesheet" type="text/css" />--%>
<script src="../Script/jquery.webcam.js" type="text/javascript"></script>


    <script src="Script/ScrollableGridViewPlugin_ASP.NetAJAXmin.js" type="text/javascript"></script>
    <script src="Script/jquery-1.4.1.min.js" type="text/javascript"></script>
    <script src="Script/jquery.dynDateTime.min.js" type="text/javascript"></script>
    <script src="Script/calendar-en.min.js" type="text/javascript"></script>
    <script src="/js/jquery-1.9.1.js" type="text/javascript"></script>
    <link href="/js/jquery-ui.css" rel="stylesheet" type="text/css" />
    <script src="/js/jquery-ui.js" type="text/javascript"></script>
    <link type="text/css" href="css/jquery-ui-1.8.19.custom.css" rel="stylesheet" />
    <script type="text/javascript" src="Script/jquery-ui-1.8.19.custom.min.js"></script>  
    <script src="Script/jquery-1.3.1.js" type="text/javascript"></script>
    <script type="text/javascript" src="Script/menu.js"></script>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <div style="text-align:center;width:100%;height:200px">
                  <br /><br />
                <asp:Label ID="Label1" runat="server" Text=" *** You are not authorized to view this page ***" 
            style="font-weight: 700; text-align: center;color:red" Font-Size="XX-Large"></asp:Label>
                     
            </div>
            <%--<div class="form-sec-row">
                <asp:Button ID="Button2" runat="server" Text="Cancel" OnClick="Button2_Click" Height="28px" CssClass="submit-button" />
                <div class="clear"></div>
            </div>--%>  
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
