<%@ Page Title="" Language="C#" MasterPageFile="~/MasterAll/MasterPageAll.master" AutoEventWireup="true" CodeFile="RadiologyGroup.aspx.cs" Inherits="Pathology_RadiologyGroup" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">

    <script type="text/javascript">
        function ShowDialog() {

            var rtvalue = window.open("ProfilePopup.aspx", "sss", "Width:800px; Height:550px; dialogLeft:250px;");
            document.getElementById("ctl00_ContentPlaceHolder1_txtcode").value = rtvalue;

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
         <asp:Label ID="Label1" runat="server">Radiology Group Master</asp:Label>
    </div>
     <div class="formbox">
      <div class="error">
                <strong><asp:Label ID="lblError" runat="server" Font-Bold="True"></asp:Label></strong>
                <div class="clear"></div>
            </div>
  
        <div class="form-sec-row">
                <label><strong>Group Code :</strong></label>
            <asp:TextBox ID="txtcode" runat="server" CssClass="textbox-medium1"></asp:TextBox>
                <asp:Button ID="Button6" runat="server" Text="quick Search" Height="28px"  CssClass="submit-buttonCheck" OnClientClick="ShowDialog();" /><asp:Button ID="Button5" Height="28px"  runat="server" CssClass="submit-button" Text="fetch" onclick="Button5_Click" />
                <div class="clear"></div>
            </div>
            
            <div class="form-sec-row">
                <label><strong>Group Name :</strong></label>
                <asp:TextBox ID="txtname" runat="server" CssClass="textbox-medium1"></asp:TextBox>
                <asp:Label ID="lbltxt" runat="server" Text=" " 
                    style="font-size: small; font-family: 'Times New Roman', Times, serif;"></asp:Label>
                <div class="clear"></div>
            </div>

                   <div class="form-sec-row">
                <label><strong>Price :</strong></label>
                <asp:TextBox ID="txtprice" runat="server" CssClass="textbox-medium1"></asp:TextBox>
                <asp:Label ID="Label2" runat="server" Text=" " 
                    style="font-size: small; font-family: 'Times New Roman', Times, serif;"></asp:Label>
                <div class="clear"></div>
            </div>

      
            <div class="form-sec-row">
                <label><strong>&nbsp;</strong></label>
                <asp:Button ID="Button3" runat="server"  Height="28px" Text="Submit" CssClass="submit-button" 
                    onclick="Button3_Click" />
                <asp:Button ID="Button4" runat="server" Text="Cancel"  CssClass="submit-button"  Height="28px" 
                    onclick="Button4_Click" />
                <asp:HiddenField ID="HiddenField1" runat="server" />
                <div class="clear"></div>
            </div>
           
     </div>

     <div class="formbox">
               <table cellpadding="0" cellspacing="0" class="ui-accordion" title="Search">
                 <tr>
                <td rowspan="2"  class="style1">
                 <div >
        
         <asp:GridView id="GridView1" style="width:400px;" CssClass="grid" PagerStyle-CssClass="pgr" AlternatingRowStyle-CssClass="alt"
            DataKeyNames ="ID,Name" 
            runat="server" AutoGenerateColumns="False" AllowPaging="True" 
             PageSize ="6" onpageindexchanging="GridView1_PageIndexChanging" >
                <Columns>

                    <asp:TemplateField HeaderText="Select" >
                        <ItemTemplate>
                            <asp:CheckBox ID="CheckBox1" runat="server"/>
                        </ItemTemplate>
                    </asp:TemplateField>         

                    <asp:TemplateField HeaderText="USG-Code">
                        <ItemTemplate>
                            <asp:Label ID="lblid" runat="server" Text='<%# Bind("ID") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="USG-Name">
                        <ItemTemplate>
                            <asp:Label ID="lblname" runat="server" Text='<%# Bind("Name") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <%--  <asp:TemplateField HeaderText="Cost">
                        <ItemTemplate>
                            <asp:Label ID="lblcost" runat="server" Text='<%# Bind("Price") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>--%>
                   

                </Columns>
                <PagerStyle CssClass="pgr"></PagerStyle>
                <EditRowStyle BackColor="#CCFF33" />
                <AlternatingRowStyle BackColor="#FFDB91" />
        </asp:GridView>
           </div>

                  </td>
                        <td>
                     
                            <asp:Button ID="Button1" runat="server" Text=">>" CssClass="submit-button" onclick="Button1_Click" />
                     
                      </td>
                        <td rowspan="2">
                        <div class="listing">
                 <asp:GridView id="GridView2" style="width:400px;" CssClass="grid" 
                                PagerStyle-CssClass="pgr" AlternatingRowStyle-CssClass="alt"
            DataKeyNames ="ID,Name" 
            runat="server" AutoGenerateColumns="False" AllowPaging="True" 
             PageSize ="6" onpageindexchanging="GridView2_PageIndexChanging">
                <Columns>

                    <asp:TemplateField HeaderText="Select">
                        <ItemTemplate>
                            <asp:CheckBox ID="CheckBox2" runat="server" />
                        </ItemTemplate>
                    </asp:TemplateField>

                   <asp:TemplateField HeaderText="USG-Code">
                        <ItemTemplate>
                            <asp:Label ID="lblId" runat="server" Text='<%# Bind("ID") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="USG-Name">
                        <ItemTemplate>
                            <asp:Label ID="lblName" runat="server" Text='<%# Bind("Name") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>

                      <%-- <asp:TemplateField HeaderText="Cost">
                        <ItemTemplate>
                            <asp:Label ID="lblcost" runat="server" Text='<%# Bind("Price") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>--%>

                </Columns>
                <PagerStyle CssClass="pgr"></PagerStyle>
                <EditRowStyle BackColor="#CCFF33" />
                <AlternatingRowStyle BackColor="#FFDB91" />
        </asp:GridView>
           </div>

                   </td>
                      
            </tr>
                 <tr>
                     <td>
                         <asp:Button ID="Button2" runat="server" Text="<<" CssClass="submit-button" onclick="Button2_Click" />
                     </td>
                 </tr>

                   <tr>
                 <td colspan="3" align="right">  
                     <%--<asp:Button ID="Button7" runat="server" Text="Save" 
                         CssClass="submit-button" onclick="Button7_Click"  />--%></td>
                 </tr>
        </table>
                <div class="clear"> </div>
            </div>
    </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>

