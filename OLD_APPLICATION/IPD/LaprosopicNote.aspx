<%@ Page Title="" Language="C#" MasterPageFile="~/MasterAll/MasterPageAll.master" AutoEventWireup="true" CodeFile="LaprosopicNote.aspx.cs" Inherits="Assignment_LaprosopicNote" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">




 <asp:UpdatePanel ID="UpdatePanel1" runat="server">
    <ContentTemplate>
 <div class="pageheader">
         <asp:Label ID="Label1" runat="server">Laparosopic Note</asp:Label>
    </div>

     <div class="formbox">

      <div class="form-sec-row">
                <label><strong> OT Type :</strong></label>
                <asp:DropDownList ID="ddlOTType" runat="server" CssClass="textbox-medium1" 
                    AutoPostBack="True" onselectedindexchanged="ddlOTType_SelectedIndexChanged">
                </asp:DropDownList>
                <div class="clear"></div>
            </div>

             <div class="form-sec-row">
                <label><strong> OT Name :</strong></label>
                <asp:DropDownList ID="ddlOTName" runat="server" CssClass="textbox-medium1" 
                     AutoPostBack="True" 
                     onselectedindexchanged="ddlOTName_SelectedIndexChanged">
                </asp:DropDownList>
                <div class="clear"></div>
            </div>


             <asp:Panel ID="Panel1" runat="server">

                 <div class="form-sec-row">
                <label><strong> Laprosopic Note :</strong><br /><br /><br /><br /><br /><br /><br /><br /><br /><br />
                </label>
                   <asp:TextBox ID="txtLapNote" TextMode="MultiLine" Height="650px"  Width="700px"  
                    onKeypress="if (event.keyCode==8) event.returnValue = false;"
                    runat="server"></asp:TextBox>
                <div class="clear"></div>
            </div>
           </asp:Panel>


             <div class="form-sec-row">
                <label></label>
           <asp:Button ID="btnSubmit" runat="server" CssClass="submit-button" Width="80px" 
                    Text="Submit" onclick="btnSubmit_Click"/>
           <asp:Button ID="btnClear" runat="server" CssClass="submit-button"  Width="80px"  
                    Text="Clear" />
                <div class="clear"></div>
            </div>


        </div>
        </ContentTemplate> 
        </asp:UpdatePanel> 

</asp:Content>

