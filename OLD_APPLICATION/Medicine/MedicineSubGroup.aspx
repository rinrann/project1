<%@ Page Title="" Language="C#" MasterPageFile="~/MasterAll/MasterPageAll.master" AutoEventWireup="true" CodeFile="MedicineSubGroup.aspx.cs" Inherits="Medicine_MedicineSubGroup" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
 

    <script language="javascript" type="text/javascript">

    function DisableBackButton() {
        window.history.forward()
    }
    DisableBackButton();
    window.onload = DisableBackButton;
    window.onpageshow = function (evt) { if (evt.persisted) DisableBackButton() }
    window.onunload = function () { void (0) }
    $(document).ready(function () {

        $("input[id$='Button1']").click(function () {

            if ($("input[id$='txtMedicineGroupName']").val() == '') {
                alert('Please Enter MedicineGroup name Properly!');
                $("input[id$='txtMedicineGroupName']").focus();
                $("input[id$='txtMedicineGroupName']").addClass('textboxerr');
                return false;
            }
            else {
                $("input[id$='txtMedicineGroupName']").removeClass('textboxerr');
            }

            if ($("select[id$='DropDownList1']").val() == '0') {
                alert('Please Select Medicine Group!');
                $("select[id$='DropDownList1']").addClass('textboxerr');
                $("select[id$='DropDownList1']").focus();
                return false;
            }
            else {
                $("select[id$='DropDownList1']").removeClass('textboxerr');
            }
        });

    });
                
             

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
                <asp:Label ID="Label1" runat="server">Medicine Sub Group</asp:Label>
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
                        <strong>
                        <asp:Label ID="lblError" runat="server" Font-Bold="True" CssClass="fadeout"></asp:Label>
                        </strong>
                        <div class="clear">
                        </div>
                    </div>
                    <asp:HiddenField ID="HiddenField1" runat="server" Value="0" />
                    <div class="form-sec-row">
                        <label><strong>
                       Group Name</strong></label>
                        <asp:DropDownList ID="DropDownList1" runat="server" CssClass="textbox-medium1">
                        </asp:DropDownList>
                        <div class="clear">
                        </div>
                    </div>
                    <div class="form-sec-row">
                        <label><strong>
                         Sub Group Name</strong></label>
                        <asp:TextBox ID="txtMedicineGroupName" runat="server" CssClass="textbox-medium1" ></asp:TextBox>
                            <cc1:AutoCompleteExtender ServiceMethod="SearchCustomers"  MinimumPrefixLength="1"    CompletionInterval="100" EnableCaching="false" 
                   CompletionSetCount="10" TargetControlID="txtMedicineGroupName"  ID="AutoCompleteExtender1" runat="server" FirstRowSelected = "false" >
                   </cc1:AutoCompleteExtender>
                        <div class="clear">
                        </div>
                    </div>

                    

                    <div class="form-sec-row">
                        <div class="clear">
                        </div>
                    </div>
                    <div class="form-sec-row">
                        &nbsp;&nbsp;&nbsp;<label><strong>&nbsp;</strong></label>
                        <asp:Button ID="Button1" runat="server" CssClass="submit-button"  Height="28px"
                            Text="Submit" onclick="Button1_Click"     />
                        <asp:Button ID="Button2" runat="server" CssClass="submit-button"  Height="28px"
                            Text="Cancel" onclick="Button2_Click"    />
                        <div class="clear">
                        </div>
                    </div>
                    <div class="clear">
                    </div>
                </div>
                </asp:View>
      <asp:View ID="View2" runat="server">
      <div style='width:100%;'>
         <table width="100%" style='background-color:#3AA8FC; color:#FFF;'>
         <tr>
         <td><strong>Group :</strong></td>
         <td> <asp:DropDownList ID="DropDownList8" Width="180px" AutoPostBack="true"  
                 runat="server" onselectedindexchanged="DropDownList8_SelectedIndexChanged">
             </asp:DropDownList></td>
    <td><strong>Sub Group :</strong></td>
         <td> <asp:DropDownList ID="DropDownList9" Width="180px" runat="server">
             </asp:DropDownList></td>
       
         <td>
             <asp:Button ID="Button3" runat="server" Text="Search" CssClass="submit-button" onclick="Button3_Click" />
         </td>
         </tr>
         </table>
       </div>
                <div style='width:100%;'>
  <table width="100%" style='background-color:#FB7B13; color:#FFF;'>
  <tr style='border:1px solid black;'> 
   <td style='width:150px;' align="center">Sub Group Name</td> 
     <td style='width:150px;' align="center">Group Name</td>  
        <td style='width:70px;' align="center">Edit</td>
          <td style='width:70px;' align="center" id="coldel" runat="server">Delete</td>
          </tr>
  </table> 
  </div> 


            <div class="listing"   style='width:100%; height:500px; overflow:auto;'>
                <asp:GridView id="GridView1"  Width="100%" CssClass="grid" PagerStyle-CssClass="pgr"
                 DataKeyNames ="ID" runat="server"  ShowHeader="false"
                 AutoGenerateColumns="False" AllowPaging="True"  PageSize="1000"
                 OnRowCommand="GridView1_RowCommand"  OnRowDataBound="GridView1_RowDataBound" OnRowDeleting="GridView1_RowDeleting" 
                 OnPageIndexChanging="GridView1_PageIndexChanging" SelectedRowStyle-BackColor="GreenYellow">
                 
                                                 <RowStyle HorizontalAlign="Center" />
                    <Columns>
                        <asp:TemplateField HeaderText="Medicine Sub Group ID" Visible ="false">
                            <ItemTemplate>
                                <asp:Label ID="lblid" runat="server" Text='<%# Bind("ID") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Sub Group Name" ItemStyle-Width="150px">
                            <ItemTemplate>
                                <asp:Label ID="lblsubgr" runat="server" Text='<%# Bind("SubGrName") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                         
                               <asp:TemplateField HeaderText="Group Name" ItemStyle-Width="150px">
                            <ItemTemplate>
                                <asp:Label ID="lblgroup" runat="server" Text='<%# Bind("MedicineGroupName") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>

                        <asp:CommandField SelectText="Edit" ShowSelectButton="True" ItemStyle-Width="70px" ControlStyle-CssClass="temp" HeaderText="Edit" SelectImageUrl="~/Images/edit.png" ButtonType="Image">
                            <ControlStyle CssClass="temp"></ControlStyle>
                        </asp:CommandField>
                        <asp:CommandField ShowDeleteButton="True" ControlStyle-CssClass="temp" ItemStyle-Width="70px" HeaderText="Delete" DeleteImageUrl="~/Images/delete.png" ButtonType="Image">
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

