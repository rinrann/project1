<%@ Page Title="" Language="C#" MasterPageFile="~/MasterAll/MasterPageAll.master" AutoEventWireup="true" CodeFile="ChemicalMapping.aspx.cs" Inherits="DayCare_ChemicalMapping" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
 
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <script type="text/javascript" language="javascript">
    $(document).ready(function () {
        function Calling() {
            $("input[id$='Button3']").click(function () {

                if ($("select[id$='ddldialysertype']").val() == '0') {
                    alert('Select Dialyser Type!');
                    $("select[id$='ddldialysertype']").focus();
                    $("select[id$='ddldialysertype']").addClass('textboxerr');
                    return false;
                }
                else {
                    $("select[id$='ddldialysertype']").removeClass('textboxerr');
                }




                if ($("select[id$='ddldialysername']").val() == '0') {
                    alert('Select Dialyser Name!');
                    $("select[id$='ddldialysername']").focus();
                    $("select[id$='ddldialysername']").addClass('textboxerr');
                    return false;
                }
                else {
                    $("select[id$='ddldialysername']").removeClass('textboxerr');
                }
            });
        }
    });

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


 
<%--  <asp:UpdateProgress ID="UpdateProgress1" runat="server">
    <ProgressTemplate>
    
       <div id="aaa" class="progressBackgroundFilter">          </div>
        <div id="bbb" class="processMessage"><img alt="Loading" src="../images/pwait.gif"/>
      
              </div>
    </ProgressTemplate>
    </asp:UpdateProgress>--%>


    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
    <ContentTemplate>
     <div class="pageheader">
         <asp:Label ID="Label1" runat="server">Chemical Mapping</asp:Label>
    </div>
     <div class="formbox">
      <div class="error">
                <strong><asp:Label ID="lblError" runat="server" Font-Bold="True"></asp:Label></strong>
                <div class="clear"></div>
            </div>
        <div class="form-sec-row">
                <label><strong>Dialysis Type :</strong></label>
                <asp:DropDownList ID="ddldialysertype" runat="server" CssClass="combo-big1" 
                    AutoPostBack="True" onselectedindexchanged="ddldialysertype_SelectedIndexChanged" 
                   >
                </asp:DropDownList>
                <div class="clear"></div>
            </div>
            
            <div class="form-sec-row">
                <label><strong>Dialyser Model Name :</strong></label>
                <asp:DropDownList ID="ddldialysername" runat="server" CssClass="combo-big1" 
                    AutoPostBack="True" onselectedindexchanged="ddldialysername_SelectedIndexChanged" 
                   >
                </asp:DropDownList>
                <asp:Label ID="lbltxt" runat="server" Text=" " 
                    style="font-size: small; font-family: 'Times New Roman', Times, serif;"></asp:Label>
                <div class="clear"></div>
            </div>
       
           
     </div>
      <div class="formbox" >
                
                     <div style='width:100%;'>
  <table width="100%" style='background-color:#FB7B13; color:#FFF;'>
  <tr style='border:1px solid black;'> 
   <td style='width:20px;' align="center"></td>
   <td style='width:90px;'  align="center">Group</td>
    <td style='width:100px;' align="center">Sub Group</td>
     <td style='width:110px;' align="center">Chemical Name</td>
      <td style='width:90px;' align="center">Used For</td>
       <td style='width:90px;' align="center">Unit</td>
        <td style='width:70px;' align="center">Patient Cover</td> 
          </tr>
  </table> 
  </div> 

               <div  style='width:100% ; height:300px; overflow:auto;'>
         <asp:GridView id="GridView1" style="width:100%;" CssClass="grid" 
                       PagerStyle-CssClass="pgr" AlternatingRowStyle-CssClass="alt"
            DataKeyNames ="MedicineID,MedicineName"  ShowHeader="false"
            runat="server" AutoGenerateColumns="False" AllowPaging="True" SelectedRowStyle-BackColor="Green" 
             PageSize ="1000" onrowcommand="GridView1_RowCommand" 
                         onpageindexchanging="GridView1_PageIndexChanging" 
                       onrowdatabound="GridView1_RowDataBound">
                <Columns>

                    <asp:TemplateField HeaderText="Select" ItemStyle-Width="20px"   ItemStyle-HorizontalAlign="Center"  >
                        <ItemTemplate>
                            <asp:CheckBox ID="CheckBox1" runat="server"   />
                        </ItemTemplate>
                    </asp:TemplateField>         

                    <asp:TemplateField HeaderText="ID" Visible="false">
                        <ItemTemplate>
                            <asp:Label ID="lblid" runat="server" Text='<%# Bind("MedicineID") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="Group" ItemStyle-Width="90px"   ItemStyle-HorizontalAlign="Center"  >
                        <ItemTemplate>
                            <asp:Label ID="lblDIALYSIS" runat="server" Text="DIALYSIS"></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="Sub Group" ItemStyle-Width="100px"   ItemStyle-HorizontalAlign="Center"  >
                        <ItemTemplate>
                            <asp:Label ID="lblCHEMICAL" runat="server" Text="CHEMICAL"></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="Stock Chemical Name"   ItemStyle-Width="110px"   ItemStyle-HorizontalAlign="Center"  >
                        <ItemTemplate>
                            <asp:Label ID="lblname" runat="server" Text='<%# Bind("MedicineName") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>

                         <asp:TemplateField HeaderText="Used For"  ItemStyle-Width="90px"   ItemStyle-HorizontalAlign="Center"  >
                        <ItemTemplate>
                            <asp:Label ID="lblusedfor" runat="server" Text='<%# Bind("UsedFor") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                         <asp:TemplateField HeaderText="Unit Name"  ItemStyle-Width="90px"   ItemStyle-HorizontalAlign="Center"  >
                        <ItemTemplate>
                            <asp:Label ID="lblunitname" runat="server" Text='<%# Bind("UnitName") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                         <asp:TemplateField HeaderText="Patient Cover"   ItemStyle-Width="70px"   ItemStyle-HorizontalAlign="Center"  >
                        <ItemTemplate>
                            <asp:Label ID="lblpatientCover" runat="server" Text='<%# Bind("PatientCover") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>

                   

                </Columns>
                <PagerStyle CssClass="pgr"></PagerStyle>
                <EditRowStyle BackColor="#CCFF33" />
                <AlternatingRowStyle BackColor="#FFDB91" />
        </asp:GridView>
         </div>
          
                <div class="clear"> </div>

                     <div class="form-sec-row">
                <label><strong>&nbsp;</strong></label>
                <asp:Button ID="Button3" runat="server" Height="28px" Text="Submit" CssClass="submit-button" onclick="Button3_Click"/>
                <asp:Button ID="Button4" runat="server" Text="Cancel" Height="28px"  CssClass="submit-button" />
                <div class="clear"></div>
            </div>
            </div>     
    </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>

