<%@ Page Title="" Language="C#" MasterPageFile="~/MasterAll/MasterPageAll.master" AutoEventWireup="true" CodeFile="CompanyMaster.aspx.cs" Inherits="Pathology_CompanyMaster" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">

 

 <script language="javascript" type="text/javascript">
 
     function Calling() {
         $("input[id$='txtname']").focus(function () {
             $("input[id$='txtname']").addClass('textboxborder');
         });
         $("input[id$='txtname']").blur(function () {
             $("input[id$='txtname']").removeClass('textboxborder');
         });
         $("input[id$='txtaddress']").focus(function () {
             $("input[id$='txtaddress']").addClass('textboxborder');
         });
         $("input[id$='txtaddress']").blur(function () {
             $("input[id$='txtaddress']").removeClass('textboxborder');
         });
         $("input[id$='txtph1']").focus(function () {
             $("input[id$='txtph1']").addClass('textboxborder');
         });
         $("input[id$='txtph1']").blur(function () {
             $("input[id$='txtph1']").removeClass('textboxborder');
         });

         $("input[id$='Button1']").click(function () {
             if ($("input[id$='txtname']").val() == '') {
                 $("input[id$='txtname']").addClass('textboxerr');
             }

             if ($("input[id$='txtname']").val() == '') {
                 alert('Please Enter Company Name !');
                 $("input[id$='txtname']").focus();
                 $("input[id$='txtname']").addClass('textboxerr');
                 return false;
             }
             else {
                 $("input[id$='txtname']").removeClass('textboxerr');
             }


             if ($("input[id$='txtaddress']").val() == '') {
                 $("input[id$='txtaddress']").addClass('textboxerr');
             }

             if ($("input[id$='txtaddress']").val() == '') {
                 alert('Please Enter Address !');
                 $("input[id$='txtaddress']").focus();
                 $("input[id$='txtaddress']").addClass('textboxerr');
                 return false;
             }
             else {
                 $("input[id$='txtaddress']").removeClass('textboxerr');
             }



             if ($("input[id$='txtph1']").val() == '') {
                 $("input[id$='txtph1']").addClass('textboxerr');
             }

             if ($("input[id$='txtph1']").val() == '') {
                 alert('Please Enter Phone 1 !');
                 $("input[id$='txtph1']").focus();
                 $("input[id$='txtph1']").addClass('textboxerr');
                 return false;
             }
             else {
                 $("input[id$='txtph1']").removeClass('textboxerr');
             }
         });
     }
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
         <asp:Label ID="Label1" runat="server">Manufacturing Company</asp:Label>
     </div>
     <table width="290px" cellpadding="0" cellspacing="0">
         <tr>
            <td>
                <asp:Button Text="Entry" BorderStyle="None" ID="Tab1" Width="145px"  Height="40px" 
                    CssClass="Initial" runat="server" onclick="Tab1_Click"
                     /></td>
                     <td>
                <asp:Button Text="List" BorderStyle="None" ID="Tab2" CssClass="Initial"  Width="145px"  Height="40px" 
                    runat="server" onclick="Tab2_Click"
                    /></td>
           
                     </tr>
                     </table>
     <div class="formbox">
         
                      <asp:MultiView ID="MainView" runat="server">
                    <asp:View ID="View1" runat="server">
     <div class="form-sec">
        
            <div class="error">
                <strong><asp:Label ID="lblError" runat="server" Font-Bold="True"></asp:Label>
                </strong>
                <div class="clear"></div>
            </div>
            <asp:HiddenField ID="TextBox4" runat="server" />
			<div class="form-sec-row">
                <label><strong>Company Code :</strong></label>
               <asp:TextBox ID="txtcode" runat="server" CssClass="textbox-medium1" Enabled="False" 
                   ></asp:TextBox>
                <div class="clear">  </div>
            </div>
           
            <div class="form-sec-row">
                <label><strong>Company Name :</strong></label>
                <asp:TextBox ID="txtname" runat="server" CssClass="textbox-medium1" 
                   ></asp:TextBox>
                <div class="clear"></div>
            </div>

            <div class="form-sec-row">
                <label><strong>Address:</strong></label>  
                 <asp:TextBox ID="txtaddress" runat="server" CssClass="textbox-medium1" 
                   ></asp:TextBox>
                <div class="clear"></div>
            </div>
            <div class="form-sec-row">
                <label><strong>Phone-1 :</strong></label>
                  <asp:TextBox ID="txtph1" runat="server" CssClass="textbox-medium1" MaxLength="10" onkeypress="return CheckNumber(event)"
                   ></asp:TextBox>
                <div class="clear"></div>
            </div>
            <div class="form-sec-row">
                <label><strong>Phone-2 :</strong></label>
                 <asp:TextBox ID="txtph2" runat="server" CssClass="textbox-medium1"  MaxLength="10" onkeypress="return CheckNumber(event)"
                   ></asp:TextBox>
                <div class="clear"></div>
            </div>
               <div class="form-sec-row">
                <label><strong>Email ID :</strong></label>
                 <asp:TextBox ID="txtemail" runat="server" CssClass="textbox-medium1" ></asp:TextBox>
                <div class="clear"></div>
            </div>
            <div class="form-sec-row">
                <label><strong>&nbsp;</strong></label>
                <asp:Button ID="Button1" runat="server" Height="28px"  OnClick="Button1_Click" Text="Submit" CssClass="submit-button" />
                <asp:Button ID="Button2" runat="server"  Height="28px"  Text="Cancel" OnClick="Button2_Click" CssClass="submit-button" />
                <div class="clear"></div>
            </div>  
   
     </div>
 </asp:View>
    <asp:View ID="View2" runat="server">
         <div style='width:100%;'>
  <table width="100%" style='background-color:#FB7B13; color:#FFF;'>
  <tr style='border:1px solid black;'> 
   <td style='width:80px;' align="center">Code</td>
   <td style='width:110px;'  align="center">Company Name</td>
    <td style='width:110px;' align="center">Company Address</td>
     <td style='width:90px;' align="center">Phone-1</td>
      <td style='width:90px;' align="center"> Phone-2</td>
       <td style='width:90px;' align="center">Email-Id</td>
        <td style='width:70px;' align="center">Edit</td>
          <td style='width:70px;' align="center" id="code1" runat="server">Delete</td>
          </tr>
  </table> 
  </div> 

     <div class="listing"    style='width:100%; height:500px; overflow:auto;'>
         <asp:GridView id="GridView1"  CssClass="grid" PagerStyle-CssClass="pgr" DataKeyNames ="CCode,CName"  runat="server" AutoGenerateColumns="False" AllowPaging="True" 
         PageSize ="1000" ShowHeader="false" OnPageIndexChanging="GridView1_PageIndexChanging"   SelectedRowStyle-BackColor="GreenYellow" OnRowDataBound="GridView1_RowDataBound"
                 OnRowCommand="GridView1_RowCommand" OnRowDeleting="GridView1_RowDeleting" Width="100%">
                <RowStyle HorizontalAlign="Center" />
                <Columns>
                    <asp:TemplateField HeaderText="Code" ItemStyle-Width="80px"   ItemStyle-HorizontalAlign="Center"  >
                        <ItemTemplate>
                            <asp:Label ID="lblcode" runat="server" Text='<%# Bind("CCode") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Company Name" ItemStyle-Width="110px"   ItemStyle-HorizontalAlign="Center"  >
                       
                        <ItemTemplate>
                            <asp:Label ID="lblname" runat="server" Text='<%# Bind("CName") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                                       
                    <asp:TemplateField HeaderText="Company Address" ItemStyle-Width="110px"   ItemStyle-HorizontalAlign="Center"  >
                        <ItemTemplate>                        
                            <asp:Label ID="lbladdress" runat="server" Text='<%# Bind("Address") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField> 
                       
                    <asp:TemplateField HeaderText="Phone-1" ItemStyle-Width="90px"   ItemStyle-HorizontalAlign="Center"  >
                        <ItemTemplate>                        
                            <asp:Label ID="lblphone1" runat="server" Text='<%# Bind("Ph1") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                      <asp:TemplateField HeaderText="Phone-2" ItemStyle-Width="90px"   ItemStyle-HorizontalAlign="Center"   >
                        <ItemTemplate>                        
                            <asp:Label ID="lblphone2" runat="server" Text='<%# Bind("Ph2") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>

                         <asp:TemplateField HeaderText="Email ID"  ItemStyle-Width="90px"   ItemStyle-HorizontalAlign="Center"   >
                        <ItemTemplate>                        
                            <asp:Label ID="lblmail" runat="server" Text='<%# Bind("Email") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                                          
                    <asp:CommandField SelectText="Edit" ShowSelectButton="True"  ItemStyle-Width="70px"   ItemStyle-HorizontalAlign="Center"  ControlStyle-CssClass="temp" HeaderText="Edit" SelectImageUrl="~/Images/edit.png" ButtonType="Image">
                        <ControlStyle CssClass="temp"></ControlStyle>
                    </asp:CommandField>
                    <asp:CommandField ShowDeleteButton="True" ControlStyle-CssClass="temp" ItemStyle-Width="70px"   ItemStyle-HorizontalAlign="Center"   HeaderText="Delete" DeleteImageUrl="~/Images/delete.png" ButtonType="Image">
                        <ControlStyle CssClass="temp"></ControlStyle>
                    </asp:CommandField>
                </Columns>
                <PagerStyle CssClass="pgr" />
                <EditRowStyle BackColor="#CCFF33" />
                <AlternatingRowStyle BackColor="#FFDB91" />
        </asp:GridView> 
        </div>
        </asp:View>
        </asp:MultiView>
        </div>
    </ContentTemplate>

    </asp:UpdatePanel>
</asp:Content>

