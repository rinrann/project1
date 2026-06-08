<%@ Page Title="" Language="C#" MasterPageFile="~/MasterAll/MasterPageAll.master" AutoEventWireup="true" CodeFile="Bedavaibility.aspx.cs" Inherits="IPD_Bedavaibility" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <script src="../Script/ScrollableGridViewPlugin_ASP.NetAJAXmin.js" type="text/javascript"></script>
    <script src="../Script/jquery-1.4.1.min.js" type="text/javascript"></script> 
 
 <script type="text/javascript" language="javascript">

     function isNumberKey(evt) {
         var charCode = (evt.which) ? evt.which : event.keyCode;
         if (charCode != 46 && charCode > 31
            && (charCode < 48 || charCode > 57))
             return false;

         return true;
     }

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
         <asp:Label ID="Label1" runat="server">Bed Availability</asp:Label>
     </div>
     <div class="formbox">
    <table width="100%">
          
       <tr>
                <td>
     
             <label class="pname"><strong>Bed Cost:</strong></label> 
                      
</td>
            <td>
     
            <asp:TextBox ID="TextBox1" runat="server"  MaxLength="5" onkeypress="return isNumberKey(event)"  Width="68px" ></asp:TextBox>  
                             
            
</td>
   <td>
     
             <label class="pname"><strong>To:</strong></label> 
                           
            
</td>
   <td>
     
                <asp:TextBox ID="TextBox2"  MaxLength="5" onkeypress="return isNumberKey(event)"  runat="server" Width="68px" ></asp:TextBox>  
                             
            
</td>
       <td>
     
             <label class="pname"><strong>Floor:</strong></label> 
                     </td>
                     <td>
                         <asp:DropDownList ID="DropDownList1" runat="server" Width="100px" Height="27px" 
                             AutoPostBack="True" onselectedindexchanged="DropDownList1_SelectedIndexChanged">
                         </asp:DropDownList> </td>
          <td>
     
             <label class="pname"><strong>Wings:</strong></label> 
                   </td>
                   <td>
                     <asp:DropDownList ID="DropDownList2" runat="server" Width="100px"  Height="27px">
                         </asp:DropDownList> </td>
        <td>
     
             <label class="pname"><strong>Room Type:</strong></label> 
                    </td>
                    <td>
                     <asp:DropDownList ID="DropDownList3" runat="server" Width="100px" Height="27px" 
                            AutoPostBack="True" onselectedindexchanged="DropDownList3_SelectedIndexChanged">
                         </asp:DropDownList> </td>
  <td>
     
             <label class="pname"><strong>Room No:</strong></label> 
                     
</td>
<td>
 <asp:DropDownList ID="DropDownList4" runat="server" Width="100px"  Height="27px">
                         </asp:DropDownList> </td>
      
          <td>
                             <div class="form-sec-row"> 
           <asp:Button ID="Button2" runat="server" Text="Search"  CssClass="submit-button"  Height="30px"
                                     onclick="Button2_Click"     />
            </div>                  
  </td>          
  </tr> 
  </table>
  </div>
     <div class="formbox"> 
  <table width="100%" style='background-color:#FB7B13; color:#FFF;'>
  <tr style='border:1px solid black;'>
  <%--<td style='width:120px; display:none;' align="center" ></td>--%>
   <td style='width:30px;' align="center">Sl.No</td>
      <td style='width:120px;' align="center">Floor Name</td>
   <td style='width:140px;'  align="center">Wings Name</td>
    <td style='width:120px;' align="center">Room Category</td>
     <td style='width:120px;' align="center">Room</td>
      <td style='width:120px;' align="center">Bed No</td>
       <td style='width:120px;' align="center">Charges</td>
        <td style='width:120px;' align="center">Admission</td>
          </tr>
  </table>  
        <div style='height:500px; overflow:scroll;'>

         <asp:GridView id="GridView1"  CssClass="grid" PagerStyle-CssClass="pgr"    
             DataKeyNames ="BedNo" runat="server" AutoGenerateColumns="False"  Width="100%" ShowHeader="false"
                AllowPaging="True" PageSize ="100"  OnPageIndexChanging="GridView1_PageIndexChanging"  
              SelectedRowStyle-BackColor="GreenYellow"   
                onrowcommand="GridView1_RowCommand" onrowdatabound="GridView1_RowDataBound">
              
              <RowStyle HorizontalAlign="Center" />
                <Columns>

                    <asp:TemplateField HeaderText="Sl.No"   ItemStyle-Width="30px"  ItemStyle-HorizontalAlign="Center"> 
                        <ItemTemplate>
                            <asp:Label ID="lblSlno" runat="server" Width="30px"></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>

                    <asp:TemplateField Visible ="false" ItemStyle-Width="120px" >
                        <ItemTemplate>
                            <asp:Label ID="lblregno" runat="server" Text='<%# Bind("BedNo") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>

                      <asp:TemplateField ItemStyle-Width="120px" >
                        <ItemTemplate>
                            <asp:Label ID="lblreqno" runat="server" Text='<%# Bind("FloorName") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>

                    <asp:TemplateField ItemStyle-Width="140px">
                       
                        <ItemTemplate>
                            <asp:Label ID="lblname" runat="server" Text='<%# Bind("WingsName") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                                
                    <asp:TemplateField  ItemStyle-Width="120px">
                        <ItemTemplate>                        
                            <asp:Label ID="lblphone1" runat="server" Text='<%# Bind("RoomCategoryName") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                     
                  <asp:TemplateField  ItemStyle-Width="120px">
                        <ItemTemplate>                        
                            <asp:Label ID="lblotdate" runat="server" Text='<%# Bind("RoomName") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>

                     <asp:TemplateField  ItemStyle-Width="120px">
                        <ItemTemplate>                        
                            <asp:Label ID="lblottype" runat="server" Text='<%# Bind("BedNoText") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField   ItemStyle-Width="120px" ItemStyle-HorizontalAlign="Right">
                        <ItemTemplate>                        
                            <asp:Label ID="lblotname" runat="server" Text='<%# Bind("Charges") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>   
                            
                    <asp:TemplateField  ItemStyle-Width="120px">
                        <ItemTemplate>
                            <asp:LinkButton ID="LinkButton1" CommandName="bedavail" CommandArgument='<%# Eval("BedNo") %>' runat="server">Get Admission</asp:LinkButton>
                        </ItemTemplate>
                    </asp:TemplateField>
                </Columns>
                <PagerStyle CssClass="pgr" />
                <EditRowStyle BackColor="#CCFF33" />
                <AlternatingRowStyle BackColor="#FFDB91" />
        </asp:GridView> 
        </div>
        </div>
    </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>

