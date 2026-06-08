<%@ Page Title="" Language="C#" MasterPageFile="~/MasterAll/MasterPageAll.master" AutoEventWireup="true" CodeFile="OperationTemplate.aspx.cs" Inherits="IPD_OperationTemplate" %>

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

    function ShowDialog1() {
        var rtvalue = window.open("OTPopup.aspx", "sss", "Width:800px; Height:550px; dialogLeft:250px;");
        var b = rtvalue.NameValue.split("#");
        document.getElementById("ctl00_ContentPlaceHolder1_HiddenField1").value = b[0];
        document.getElementById("ctl00_ContentPlaceHolder1_txtOperationType").value = b[1];
        var a = rtvalue.ProfessionValue.split("#");
        document.getElementById("ctl00_ContentPlaceHolder1_HiddenField2").value = a[0];
        document.getElementById("ctl00_ContentPlaceHolder1_txtOperationName").value = a[1];

    }
    function Calling() {

        $("input[id$='txtserviceCharge']").keydown(function (event) {
            if (event.keyCode == 46 || event.keyCode == 8 || event.keyCode == 9 || event.keyCode == 27 || event.keyCode == 13 ||
                        (event.keyCode == 65 && event.ctrlKey === true) || (event.keyCode >= 35 && event.keyCode <= 39)) {
                return;
            }
            else {
                if (event.shiftKey || (event.keyCode < 48 || event.keyCode > 57) && (event.keyCode < 96 || event.keyCode > 105)) {
                    event.preventDefault();
                }
            }
        });

        $("input[id$='Button1']").click(function () {

          

            if ($("input[id$='txtTemplateName']").val() == '') {
                alert('Please Enter Template Name  !');
                $("input[id$='txtTemplateName']").focus();
                $("input[id$='txtTemplateName']").addClass('textboxerr');
                return false;
            }
            else {
                $("input[id$='txtTemplateName']").removeClass('textboxerr');
            }

            if ($("input[id$='txtserviceCharge']").val() == '') {
                alert('Please Enter Service Charge  !');
                $("input[id$='txtserviceCharge']").focus();
                $("input[id$='txtserviceCharge']").addClass('textboxerr');
                return false;
            }
            else {
                $("input[id$='txtserviceCharge']").removeClass('textboxerr');
            }

            if ($("select[id$='DropDownList1']").val() == '0') {
                alert('Please Select Template Category !');
                $("select[id$='DropDownList1']").addClass('textboxerr');
                $("select[id$='DropDownList1']").focus();
                return false;
            }
            else {
                $("select[id$='DropDownList1']").removeClass('textboxerr');
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
         <asp:Label ID="Label1" runat="server">Operation & Cons Template</asp:Label>
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
                              <asp:HiddenField ID="HiddenField1" runat="server" /><asp:HiddenField ID="HiddenField2" runat="server" />
<asp:MultiView ID="MainView" runat="server">
                    <asp:View ID="View1" runat="server">
                    <div class="form-sec">
            <div class="error">
                <strong><asp:Label ID="lblError" runat="server" Font-Bold="True"></asp:Label></strong>
                <div class="clear"></div>
            </div>
            <asp:HiddenField ID="TextBox4" runat="server" />
            		<div class="form-sec-row">
                 <label><strong>Select Operation :</strong></label>
                                             <asp:DropDownList ID="ddlSelOprn" runat="server" CssClass="textbox-medium1"  AutoPostBack="true"
                        Width="250px">
                            </asp:DropDownList>
             
               
                   
                
                <div class="clear"></div></div>
            </div>

			<div class="form-sec-row">
                <label style="visibility: hidden"><strong>Template Name :</strong></label>
                <asp:TextBox ID="txtTemplateName" runat="server" CssClass="textbox-medium1" Visible="False" Wrap="False" ></asp:TextBox>
                <div class="clear"></div>
            </div>

            	<div class="form-sec-row">
                <label style="visibility: hidden"><strong>Service Charge :</strong></label>
                <asp:TextBox ID="txtserviceCharge" runat="server" MaxLength="5"  CssClass="textbox-medium1" Visible="False" Wrap="False" >0</asp:TextBox>
                <div class="clear"></div>
            </div>
           
          
          <%--<table border="1" cellpadding="0" cellspacing="0" width="100%">
          
       <tr style='background-color:#FF9300;'>
                <td align="center">
                  <div class="form-sec-row"> 
             <label class="lname"><strong>Service Category</strong></label> 
            </div>
        </td> 

                <td align="center">
                    <div class="form-sec-row"> 
             <label class="lname"><strong>Service Name</strong></label>
            </div>
</td>
                <td align="center">
                             <div class="form-sec-row"> 
             <label class="lname"><strong> Time / Day </strong></label>
            </div>                  
</td>
 <td align="center">
                    <div class="form-sec-row"> 
             <label class="lname"><strong>Duration</strong></label> 
            </div>
            
</td>
 
        
                      
            </tr>
                <tr>
                <td align="center">
                    <div class="form-sec-row"> 
             <asp:DropDownList ID="ddlconsumablegr1" runat="server" CssClass="textbox-medium1"   AutoPostBack="true"
                            Width="150px" 
                            onselectedindexchanged="ddlconsumablegr1_SelectedIndexChanged">
                            </asp:DropDownList>
            </div>
            
</td> 

                <td align="center">
                    <div class="form-sec-row"> 
             <asp:DropDownList ID="ddlConsumableItem1" runat="server" CssClass="textbox-medium1" Width="150px">
                            </asp:DropDownList>
            </div>
</td>
                <td align="center">
                             <div class="form-sec-row"> 
              <asp:TextBox ID="txtActualQty1" runat="server"   Width="150px"></asp:TextBox>
            </div>                  
</td>
 <td align="center">
                    <div class="form-sec-row"> 
                       <asp:TextBox ID="txtBillQty1" runat="server"  Width="150px"></asp:TextBox>
            </div>
            
</td>
 
 </tr>

<tr>
                <td align="center">
                    <div class="form-sec-row"> 
            <asp:DropDownList ID="ddlconsumablegr2" runat="server" CssClass="textbox-medium1"    AutoPostBack="true"
                            Width="150px" 
                            onselectedindexchanged="ddlconsumablegr2_SelectedIndexChanged">
                            </asp:DropDownList>
            </div>
            
</td> 

                <td align="center">
                    <div class="form-sec-row"> 
            <asp:DropDownList ID="ddlConsumableItem2" runat="server" CssClass="textbox-medium1" Width="150px">
                            </asp:DropDownList>
            </div>
</td>
                <td align="center">
                             <div class="form-sec-row"> 
           <asp:TextBox ID="txtActualQty2" runat="server"   Width="150px"></asp:TextBox>
            </div>                  
</td>
 <td align="center">
                    <div class="form-sec-row"> 
                         <asp:TextBox ID="txtBillQty2" runat="server"  Width="150px"></asp:TextBox>
            </div>
            
</td>
 
   </tr>

<tr>
                <td align="center">
                    <div class="form-sec-row"> 
            <asp:DropDownList ID="ddlconsumablegr3" runat="server" CssClass="textbox-medium1"    AutoPostBack="true"
                            Width="150px" 
                            onselectedindexchanged="ddlconsumablegr3_SelectedIndexChanged">
                            </asp:DropDownList>
            </div>
            
</td> 

                <td align="center">
                    <div class="form-sec-row"> 
            <asp:DropDownList ID="ddlConsumableItem3" runat="server" CssClass="textbox-medium1" Width="150px">
                            </asp:DropDownList>
            </div>
</td>
                <td align="center">
                             <div class="form-sec-row"> 
             <asp:TextBox ID="txtActualQty3" runat="server"   Width="150px"></asp:TextBox>
            </div>                  
</td>
 <td align="center">
                    <div class="form-sec-row"> 
                         <asp:TextBox ID="txtBillQty3" runat="server"   Width="150px"></asp:TextBox>
            </div>
            
</td>
 
  </tr>

<tr>
                <td align="center">
                    <div class="form-sec-row"> 
           <asp:DropDownList ID="ddlconsumablegr4" runat="server" CssClass="textbox-medium1"    AutoPostBack="true"
                            Width="150px" 
                            onselectedindexchanged="ddlconsumablegr4_SelectedIndexChanged" >
                            </asp:DropDownList>
            </div>
            
</td> 

                <td align="center">
                    <div class="form-sec-row"> 
            <asp:DropDownList ID="ddlConsumableItem4" runat="server" CssClass="textbox-medium1" Width="150px">
                            </asp:DropDownList>
            </div>
</td>
                <td align="center">
                             <div class="form-sec-row"> 
           <asp:TextBox ID="txtActualQty4" runat="server"   Width="150px"></asp:TextBox>
            </div>                  
</td>
 <td align="center">
                    <div class="form-sec-row"> 
                         <asp:TextBox ID="txtBillQty4" runat="server"  Width="150px"></asp:TextBox>
            </div>
            
</td>
 
</tr>

<tr>
                <td align="center">
                    <div class="form-sec-row"> 
            <asp:DropDownList ID="ddlconsumablegr5" runat="server" CssClass="textbox-medium1"    AutoPostBack="true"
                            Width="150px" 
                            onselectedindexchanged="ddlconsumablegr5_SelectedIndexChanged" >
                            </asp:DropDownList>
            </div>
            
</td> 

                <td align="center">
                    <div class="form-sec-row"> 
             <asp:DropDownList ID="ddlConsumableItem5" runat="server" CssClass="textbox-medium1" Width="150px" >
                            </asp:DropDownList>
            </div>
</td>
                <td align="center">
                             <div class="form-sec-row"> 
         <asp:TextBox ID="txtActualQty5" runat="server"  Width="150px"></asp:TextBox>
            </div>                  
</td>
 <td align="center">
                    <div class="form-sec-row"> 
                         <asp:TextBox ID="txtBillQty5" runat="server"   Width="150px"></asp:TextBox>
            </div>
            
</td>
 
  </tr>

<tr>
                <td align="center">
                    <div class="form-sec-row"> 
            <asp:DropDownList ID="ddlconsumablegr6" runat="server" CssClass="textbox-medium1"    AutoPostBack="true"
                            Width="150px" 
                            onselectedindexchanged="ddlconsumablegr6_SelectedIndexChanged" >
                            </asp:DropDownList>
            </div>
            
</td> 

                <td align="center">
                    <div class="form-sec-row"> 
            <asp:DropDownList ID="ddlConsumableItem6" runat="server" CssClass="textbox-medium1" Width="150px">
                            </asp:DropDownList>
            </div>
</td>
                <td align="center">
                             <div class="form-sec-row"> 
             <asp:TextBox ID="txtActualQty6" runat="server"   Width="150px"></asp:TextBox>
            </div>                  
</td>
 <td align="center">
                    <div class="form-sec-row"> 
                         <asp:TextBox ID="txtBillQty6" runat="server" Width="150px"></asp:TextBox>
            </div>
            
</td>
 
  </tr>

<tr>
                <td align="center">
                    <div class="form-sec-row"> 
            <asp:DropDownList ID="ddlconsumablegr7" runat="server" CssClass="textbox-medium1"    AutoPostBack="true"
                            Width="150px" 
                            onselectedindexchanged="ddlconsumablegr7_SelectedIndexChanged" >
                            </asp:DropDownList>
            </div>
            
</td> 

                <td align="center">
                    <div class="form-sec-row"> 
             <asp:DropDownList ID="ddlConsumableItem7" runat="server" CssClass="textbox-medium1" Width="150px">
                            </asp:DropDownList>
            </div>
</td>
                <td align="center">
                             <div class="form-sec-row"> 
         <asp:TextBox ID="txtActualQty7" runat="server"   Width="150px"></asp:TextBox>
            </div>                  
</td>
 <td align="center">
                    <div class="form-sec-row"> 
                         <asp:TextBox ID="txtBillQty7" runat="server"  Width="150px"></asp:TextBox>
            </div>
            
</td>

 </tr>

<tr>
                <td align="center">
                    <div class="form-sec-row"> 
            <asp:DropDownList ID="ddlconsumablegr8" runat="server" CssClass="textbox-medium1"    AutoPostBack="true"
                            Width="150px" 
                            onselectedindexchanged="ddlconsumablegr8_SelectedIndexChanged" >
                            </asp:DropDownList>
            </div>
            
</td> 
       <td align="center">
                    <div class="form-sec-row"> 
            <asp:DropDownList ID="ddlConsumableItem8" runat="server" CssClass="textbox-medium1" Width="150px">
                            </asp:DropDownList>
            </div>
</td>
                <td align="center">
                             <div class="form-sec-row"> 
        <asp:TextBox ID="txtActualQty8" runat="server"   Width="150px"></asp:TextBox>
            </div>                  
</td>
 <td align="center">
                    <div class="form-sec-row"> 
                         <asp:TextBox ID="txtBillQty8" runat="server"  Width="150px"></asp:TextBox>
            </div>
            
</td>
 
  </tr>

<tr>
                <td align="center">
                    <div class="form-sec-row"> 
           <asp:DropDownList ID="ddlconsumablegr9" runat="server" CssClass="textbox-medium1"    AutoPostBack="true"
                            Width="150px" 
                            onselectedindexchanged="ddlconsumablegr9_SelectedIndexChanged" >
                            </asp:DropDownList>
            </div>
            
</td> 

                <td align="center">
                    <div class="form-sec-row"> 
             <asp:DropDownList ID="ddlConsumableItem9" runat="server" CssClass="textbox-medium1" Width="150px">
                            </asp:DropDownList>
            </div>
</td>
                <td align="center">
                             <div class="form-sec-row"> 
          <asp:TextBox ID="txtActualQty9" runat="server"   Width="150px"></asp:TextBox>
            </div>                  
</td>
 <td align="center">
                    <div class="form-sec-row"> 
                         <asp:TextBox ID="txtBillQty9" runat="server"  Width="150px"></asp:TextBox>
            </div>
            
</td>
 
   </tr>

<tr>
                <td align="center">
                    <div class="form-sec-row"> 
            <asp:DropDownList ID="ddlconsumablegr10" runat="server" CssClass="textbox-medium1"    AutoPostBack="true"
                            Width="150px" 
                            onselectedindexchanged="ddlconsumablegr10_SelectedIndexChanged" >
                            </asp:DropDownList>
            </div>
            
</td> 

                <td align="center">
                    <div class="form-sec-row"> 
             <asp:DropDownList ID="ddlConsumableItem10" runat="server" CssClass="textbox-medium1" Width="150px">
                            </asp:DropDownList>
            </div>
</td>
                <td align="center">
                             <div class="form-sec-row"> 
            <asp:TextBox ID="txtActualQty10" runat="server"   Width="150px"></asp:TextBox>
            </div>                  
</td>
 <td align="center">
                    <div class="form-sec-row"> 
                         <asp:TextBox ID="txtBillQty10" runat="server"  Width="150px"></asp:TextBox>
            </div>
            
</td>
 
 </tr>

<tr>
                <td align="center">
                    <div class="form-sec-row"> 
             <asp:DropDownList ID="ddlconsumablegr11" runat="server" CssClass="textbox-medium1"    AutoPostBack="true"
                            Width="150px" 
                            onselectedindexchanged="ddlconsumablegr11_SelectedIndexChanged" >
                            </asp:DropDownList>
            </div>
            
</td> 

                <td align="center">
                    <div class="form-sec-row"> 
             <asp:DropDownList ID="ddlConsumableItem11" runat="server" CssClass="textbox-medium1" Width="150px">
                            </asp:DropDownList>
            </div>
</td>
                <td align="center">
                             <div class="form-sec-row"> 
           <asp:TextBox ID="txtActualQty11" runat="server"  Width="150px"></asp:TextBox>
            </div>                  
</td>
 <td align="center">
                    <div class="form-sec-row"> 
                         <asp:TextBox ID="txtBillQty11" runat="server"   Width="150px"></asp:TextBox>
            </div>
            
</td>
 
  </tr>
  
 
       </table>--%>

       <table border="1" cellpadding="0" cellspacing="0" width="100%">
          
       <tr style='background-color:#FF9300;'>
                <td align="center">
                  <div class="form-sec-row"> 
             <label class="lname"><strong>Consumable Group</strong></label> 
            </div>
        </td> 

                <td align="center">
                    <div class="form-sec-row"> 
             <label class="lname"><strong>Consumable Item</strong></label>
            </div>
</td>
                <td align="center">
                             <div class="form-sec-row"> 
             <label class="lname"><strong>Actual Quantity </strong></label>
            </div>                  
</td>
      <td align="center">
                             <div class="form-sec-row"> 
             <label class="lname"><strong>Bill Quantity </strong></label>
            </div>                  
</td>
 <td align="center">
                    <div class="form-sec-row"> 
             <label class="lname"><strong>Price/Unit</strong></label> 
            </div>
            
</td>
 <td></td>
        
                      
            </tr>
                <tr>
                <td align="center">
               
             <asp:DropDownList ID="ddlconsumablegr1" runat="server" CssClass="textbox-medium1"   AutoPostBack="true"
                            Width="150px" 
                            onselectedindexchanged="ddlconsumablegr1_SelectedIndexChanged">
                            </asp:DropDownList>
        
            
</td> 

                <td align="center"> 
             <asp:DropDownList ID="ddlConsumableItem1" runat="server" CssClass="textbox-medium1"  AutoPostBack="true"
                        Width="150px" onselectedindexchanged="ddlConsumableItem1_SelectedIndexChanged">
                            </asp:DropDownList>
             
</td>
                <td align="center">
                      
              <asp:TextBox ID="txtActualQty1" runat="server" CssClass="nonumber"   Width="150px"></asp:TextBox>
                      
</td>
 <td align="center">
                 
                       <asp:TextBox ID="txtBillQty1" runat="server"  CssClass="nonumber"  Width="150px"></asp:TextBox>
             
            
</td>
 <td align="center">
                 
                       <asp:TextBox ID="txtPrice1" runat="server"  CssClass="nonumber"  Width="150px"></asp:TextBox>
             
            
</td>

<td align="center">
    <asp:Button ID="Button7" runat="server" Text="Add" CssClass="submit-button"  Width="80px"
        onclick="Button7_Click"  />
</td>
 
 </tr>

 </table>
 <table border="1" cellpadding="0" cellspacing="0" width="100%">
    <tr>
    <td>
    
                  <div class="pageheader">
         <asp:Label ID="Label15" runat="server"> Preview </asp:Label>
     </div>


                                    <div class="listing" style='width:100%; height:150px; overflow:auto;'>
         <asp:GridView id="GridView8"  CssClass="grid" PagerStyle-CssClass="pgr" DataKeyNames ="RowID" 
                                            runat="server" AutoGenerateColumns="False"
          AllowPaging="True" PageSize ="50"  Width="100%"   SelectedRowStyle-BackColor="GreenYellow" 
                                            onpageindexchanging="GridView8_PageIndexChanging" 
                                            onrowcancelingedit="GridView8_RowCancelingEdit" 
                                            onrowdatabound="GridView8_RowDataBound" onrowdeleting="GridView8_RowDeleting" 
                                            onrowediting="GridView8_RowEditing" onrowupdating="GridView8_RowUpdating"  >
                <RowStyle HorizontalAlign="Center" />
                <Columns>
                 

                  <asp:TemplateField HeaderText="Sl.No.">
                  
                  <ItemTemplate><asp:Label ID="lblid" runat="server" Text='<%# Bind("RowID") %>'>
                  </asp:Label>
                  </ItemTemplate>
                  </asp:TemplateField>

                     <asp:TemplateField HeaderText="Consumable Group Id" Visible ="false">
                     
                     <ItemTemplate><asp:Label ID="lblConGrId" runat="server" Text='<%# Bind("ConsumableGrId") %>'></asp:Label>
                     </ItemTemplate>
                     </asp:TemplateField>

                    <asp:TemplateField HeaderText="Consumable Group">
                    <ItemTemplate><asp:Label ID="lblConGroupName" runat="server" Text='<%# Bind("ConGroupName") %>'>
                    </asp:Label>
                    </ItemTemplate><EditItemTemplate>
                    <asp:DropDownList ID="ddlConGroupName"   runat="server"  Width="110px"  AutoPostBack="true"   onselectedindexchanged="ddlConGroupName_SelectedIndexChanged1" >
                    </asp:DropDownList>
                    </EditItemTemplate>
                    </asp:TemplateField>
                  
                  
                 
                   <asp:TemplateField HeaderText="Consumable Item Id" Visible ="false">
                   <ItemTemplate><asp:Label ID="lblConItemID" runat="server" Text='<%# Bind("ConsumableItemId") %>'></asp:Label></ItemTemplate></asp:TemplateField>

                    
                      <asp:TemplateField HeaderText="Consumable Item"><ItemTemplate><asp:Label ID="lblConItemName" runat="server" Text='<%# Bind("ConItemName") %>'>
                      </asp:Label></ItemTemplate>
                      <EditItemTemplate><asp:DropDownList ID="ddlConItemName"   Width="110px"  runat="server"></asp:DropDownList>
                      </EditItemTemplate>
                      </asp:TemplateField> 

                      <asp:TemplateField HeaderText="Actual Qty"><ItemTemplate><asp:Label ID="lblActualQty" runat="server" Text='<%# Bind("ActualQty") %>'>
                      </asp:Label></ItemTemplate><EditItemTemplate><asp:TextBox ID="txtActualQty" CssClass="nonumber"  Width="30px"  runat="server" Text='<%# Bind("ActualQty") %>'>
                      </asp:TextBox>
                      </EditItemTemplate>
                      </asp:TemplateField> 
                        <asp:TemplateField HeaderText="Bill Qty"><ItemTemplate><asp:Label ID="lblBillQty" runat="server" Text='<%# Bind("BillQty") %>'>
                      </asp:Label></ItemTemplate><EditItemTemplate><asp:TextBox ID="txtBillQty" CssClass="nonumber"  Width="30px"  runat="server" Text='<%# Bind("BillQty") %>'>
                      </asp:TextBox>
                      </EditItemTemplate>
                      </asp:TemplateField> 

                    <asp:TemplateField HeaderText="Price/Unit"><ItemTemplate><asp:Label ID="lblPrice" runat="server" Text='<%# Bind("Price") %>'>
                    </asp:Label>
                    </ItemTemplate><EditItemTemplate><asp:TextBox ID="txtPrice"  Width="30px" CssClass="nonumber" runat="server" Text='<%# Bind("Price") %>'></asp:TextBox></EditItemTemplate>
                    </asp:TemplateField>   
                    
                                      
                    <asp:CommandField HeaderText="Edit" ShowEditButton="True" />
                    <asp:CommandField HeaderText="Delete" ShowDeleteButton="True" />
                </Columns>
                <PagerStyle CssClass="pgr" />
                <EditRowStyle BackColor="#CCFF33" />
                <AlternatingRowStyle BackColor="#FFDB91" />
                <SelectedRowStyle BackColor="GreenYellow" />
        </asp:GridView> 
        </div>
          </td>
    </tr>
    </table>
            <div class="form-sec-row">
                <label><strong>&nbsp;</strong></label>
                <asp:Button ID="Button1" runat="server" Height="28px" OnClick="Button1_Click" Text="Submit" CssClass="submit-button" />
                <asp:Button ID="Button2" runat="server" Height="28px" Text="Cancel" OnClick="Button2_Click" CssClass="submit-button" />
                <div class="clear"></div>
            </div>  
     </div>
  </asp:View>


                    
                    <asp:View ID="View2" runat="server">
                         <div style='width:100%;'>
  <table width="100%" style='background-color:#FB7B13; color:#FFF;'>
  <tr style='border:1px solid black;'> 
   <td style='width:120px;' align="center">Template Category</td>
   <%--<td style='width:120px; visibility:hidden'  align="center">Template Name</td> --%>
      <%--<td style='width: 120px;' align="center">Charges</td>--%> 
        <td style='width:70px;' align="center">Edit</td>
          <td style='width:70px;' align="center" id="coldel" runat="server">Delete</td>
          </tr>
  </table> 
  </div> 
<div class="listing"  style='width:100%; height:500px; overflow:auto;'>
         <asp:GridView id="GridView1"  CssClass="grid"
                 PagerStyle-CssClass="pgr" DataKeyNames ="NameID" runat="server" 
                 AutoGenerateColumns="False" AllowPaging="True" PageSize ="1000"  ShowHeader="false"
                 OnPageIndexChanging="GridView1_PageIndexChanging"  Width="100%"   SelectedRowStyle-BackColor="GreenYellow"
                 OnRowCommand="GridView1_RowCommand" OnRowDataBound="GridView1_RowDataBound" OnRowDeleting="GridView1_RowDeleting">
                <RowStyle HorizontalAlign="Center" />
                <Columns>
                    <asp:TemplateField HeaderText="Id" Visible="false">
                        <ItemTemplate>
                            <asp:Label ID="NameID" runat="server" Text='<%# Bind("NameID") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="Category Name" Visible="false">                       
                        <ItemTemplate>
                            <asp:Label ID="TemplateCategoryId" runat="server" Text='<%# Bind("TemplateCategoryId") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="Category Name"  ItemStyle-HorizontalAlign="Center" ItemStyle-Width="120px"  >                       
                        <ItemTemplate>
                            <asp:Label ID="CategoryName" runat="server" Text='<%# Bind("CategoryName") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>

                   <asp:TemplateField HeaderText="Template Name"  ItemStyle-HorizontalAlign="Center" ItemStyle-Width="120px"  Visible="false" >                       
                        <ItemTemplate>
                            <asp:Label ID="ServiceTemplateName" runat="server" Text='<%# Bind("ServiceTemplateName") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                      <asp:TemplateField HeaderText="Charges"  ItemStyle-HorizontalAlign="Center" ItemStyle-Width="120px" Visible="false">                       
                        <ItemTemplate>
                            <asp:Label ID="ServiceCharge" runat="server" Text='<%# Bind("ServiceCharge") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>             
                                
                    <asp:CommandField SelectText="Edit" ShowSelectButton="True"  ItemStyle-HorizontalAlign="Center" ItemStyle-Width="70px"   ControlStyle-CssClass="temp" HeaderText="Edit" SelectImageUrl="~/Images/edit.png" ButtonType="Image">
                        <ControlStyle CssClass="temp"></ControlStyle>
                    </asp:CommandField>

                    <asp:CommandField ShowDeleteButton="True" ControlStyle-CssClass="temp"  ItemStyle-HorizontalAlign="Center"  ItemStyle-Width="70px"  HeaderText="Delete" DeleteImageUrl="~/Images/delete.png" ButtonType="Image">
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

