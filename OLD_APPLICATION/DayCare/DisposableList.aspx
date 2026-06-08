<%@ Page Title="" Language="C#" MasterPageFile="~/MasterAll/MasterPageAll.master" AutoEventWireup="true" CodeFile="DisposableList.aspx.cs" Inherits="DayCare_DisposableList" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
 <script language="javascript" type="text/javascript">

    function Calling() {
        $("input[id$='Button1']").click(function () {
            if ($("input[id$='txtchemicalname']").val() == '') {
                alert('Please Enter Chemical Name !');
                $("input[id$='txtchemicalname']").focus();
                $("input[id$='txtchemicalname']").addClass('textboxerr');
                return false;
            }
            else {
                $("input[id$='txtchemicalname']").removeClass('textboxerr');
            }




            if ($("input[id$='txtuserfor']").val() == '') {
                alert('Please Enter Used For !');
                $("input[id$='txtuserfor']").focus();
                $("input[id$='txtuserfor']").addClass('textboxerr');
                return false;
            }
            else {
                $("input[id$='txtuserfor']").removeClass('textboxerr');
            }





            if ($("select[id$='ddlunit']").val() == '0') {
                alert('Select Unit!');
                $("select[id$='ddlunit']").focus();
                $("select[id$='ddlunit']").addClass('textboxerr');
                return false;
            }
            else {
                $("select[id$='ddlunit']").removeClass('textboxerr');
            }



            if ($("input[id$='txtpatientcover']").val() == '') {
                alert('Please Enter Chemical used !');
                $("input[id$='txtpatientcover']").focus();
                $("input[id$='txtpatientcover']").addClass('textboxerr');
                return false;
            }
            else {
                $("input[id$='txtpatientcover']").removeClass('textboxerr');
            }






            if ($("select[id$='ddlpatientcover']").val() == '0') {
                alert('Select Patient Cover!');
                $("select[id$='ddlpatientcover']").focus();
                $("select[id$='ddlpatientcover']").addClass('textboxerr');
                return false;
            }
            else {
                $("select[id$='ddlpatientcover']").removeClass('textboxerr');
            }


            if ($("input[id$='txtopeningstock']").val() == '') {
                alert('Please Enter Opening Stock !');
                $("input[id$='txtopeningstock']").focus();
                $("input[id$='txtopeningstock']").addClass('textboxerr');
                return false;
            }
            else {
                $("input[id$='txtopeningstock']").removeClass('textboxerr');
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


 <%--
  <asp:UpdateProgress ID="UpdateProgress1" runat="server">
    <ProgressTemplate>
    
       <div id="aaa" class="progressBackgroundFilter">          </div>
        <div id="bbb" class="processMessage"><img alt="Loading" src="../images/pwait.gif"/>
      
              </div>
    </ProgressTemplate>
    </asp:UpdateProgress>--%>


    <%--For Busy Loader End.............................--%>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
    <ContentTemplate>
    <div class="pageheader">
         <asp:Label ID="Label1" runat="server">Disposable list</asp:Label>
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
                <label><strong>Disposable Item Name :</strong></label>
            <asp:DropDownList ID="DropDownList1" runat="server" CssClass="textbox-medium1" >
                </asp:DropDownList>
                <div class="clear">  </div>
            </div>
           
            <div class="form-sec-row">
                <label><strong>User For :</strong></label>
                <asp:TextBox ID="txtuserfor" runat="server" CssClass="textbox-medium1" 
                   ></asp:TextBox>
                <div class="clear"></div>
            </div>

            <div class="form-sec-row">
                <label><strong>Unit Name:</strong></label>  
                <asp:DropDownList ID="ddlunit" runat="server" CssClass="textbox-medium1" 
                    >
                </asp:DropDownList>
                <div class="clear"></div>
            </div>
            <div class="form-sec-row">
                <label><strong>Disposable Item / Dialysis :</strong></label>
                 <asp:TextBox ID="txtpatientcover" runat="server" CssClass="textbox-medium1" 
                    ></asp:TextBox>        
                <div class="clear"></div>
            </div>
       
            <div class="form-sec-row">
                <label><strong>&nbsp;</strong></label>
                <asp:Button ID="Button1" runat="server" OnClick="Button1_Click" Height="28px"  Text="Submit" CssClass="submit-button" />
                <asp:Button ID="Button2" runat="server" Text="Cancel" Height="28px" OnClick="Button2_Click" CssClass="submit-button" />
                <div class="clear"></div>
            </div>  
     </div>
     </asp:View>
       <asp:View ID="View2" runat="server">
            <div style='width:100%;'>
  <table width="100%" style='background-color:#FB7B13; color:#FFF;'>
  <tr style='border:1px solid black;'> 
   <td style='width:110px;' align="center">Item Name</td>
   <td style='width:110px;'  align="center">Used For</td>
    <td style='width:110px;' align="center">Unit Name</td>
     <td style='width:110px;' align="center">Patient Cover</td> 
        <td style='width:70px;' align="center">Edit</td>
          <td style='width:70px;' align="center" id="coldel" runat="server">Delete</td>
          </tr>
  </table> 
  </div> 

     <div class="listing"  style='width:100%; height:450px; overflow:auto;'>
         <asp:GridView id="GridView1"  CssClass="grid" Width="100%"  ShowHeader="false"
                 PagerStyle-CssClass="pgr" DataKeyNames ="ID,ItemName" runat="server" 
                 AutoGenerateColumns="False" AllowPaging="True" PageSize ="100" 
                 OnPageIndexChanging="GridView1_PageIndexChanging"  SelectedRowStyle-BackColor="GreenYellow"
                 OnRowCommand="GridView1_RowCommand" OnRowDataBound="GridView1_RowDataBound" OnRowDeleting="GridView1_RowDeleting">
                <RowStyle HorizontalAlign="Center" />
                <Columns>
                    <asp:TemplateField HeaderText="Id" Visible="false">
                        <ItemTemplate>
                            <asp:Label ID="lblID" runat="server" Text='<%# Bind("ID") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Item id" Visible ="false">
                       
                        <ItemTemplate>
                            <asp:Label ID="lblname" runat="server" Text='<%# Bind("ItemName") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>

                       <asp:TemplateField HeaderText="Item Name" ItemStyle-Width="110px"   ItemStyle-HorizontalAlign="Center" >
                       
                        <ItemTemplate>
                            <asp:Label ID="lblMedicineName" runat="server" Text='<%# Bind("MedicineName") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>


                    <asp:TemplateField HeaderText="User For" ItemStyle-Width="110px"   ItemStyle-HorizontalAlign="Center" >
                       
                        <ItemTemplate>
                            <asp:Label ID="lbluserfor" runat="server" Text='<%# Bind("UsedFor") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    
                    <asp:TemplateField HeaderText="Unit Name" ItemStyle-Width="110px"   ItemStyle-HorizontalAlign="Center" >
                        <ItemTemplate>                        
                            <asp:Label ID="lblunitmas" runat="server" Text='<%# Bind("UnitName") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField> 
                       
                    <asp:TemplateField HeaderText="Patient Cover" ItemStyle-Width="110px"   ItemStyle-HorizontalAlign="Center" >
                        <ItemTemplate>                        
                            <asp:Label ID="lblpatientcover" runat="server" Text='<%# Bind("PatientCover") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
 
                
                    <asp:CommandField SelectText="Edit" ShowSelectButton="True" ItemStyle-Width="70px"   ItemStyle-HorizontalAlign="Center"  ControlStyle-CssClass="temp" HeaderText="Edit" SelectImageUrl="~/Images/edit.png" ButtonType="Image">
                        <ControlStyle CssClass="temp"></ControlStyle>
                    </asp:CommandField>
                    <asp:CommandField ShowDeleteButton="True" ControlStyle-CssClass="temp" ItemStyle-Width="70px"   ItemStyle-HorizontalAlign="Center"  HeaderText="Delete" DeleteImageUrl="~/Images/delete.png" ButtonType="Image">
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

