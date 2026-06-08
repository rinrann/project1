<%@ Page Title="" Language="C#" MasterPageFile="~/MasterAll/MasterPageAll.master" AutoEventWireup="true" CodeFile="AddServices.aspx.cs" Inherits="IPD_AddServices" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">

 <script type="text/javascript" language="javascript">
    function ShowDialog() {
        var rtvalue = window.open("IpdpatientPopup.aspx", "sss", "Width:800px; Height:550px; dialogLeft:250px;");
        document.getElementById("ctl00_ContentPlaceHolder1_TextBox2").value = rtvalue.NameValue;

    }

    function GetDatetime() {
        var now = new Date();
        var day, mnt, yr;
        day = now.getDate();
        mnt = now.getMonth() + 1;
        yr = now.getFullYear();
        if (day < 10)
            day = "0" + day;
        if (mnt < 10)
            mnt = "0" + mnt;
        var datetime = day + '/' + mnt + '/' + yr;
        var hour = now.getHours();
        var minute = now.getMinutes();
        var a;
        if (hour > 12) {
            hour = hour - 12;
            a = "PM";
        }
        else {
            a = "AM";
            hour = hour;
        }
        if (minute < 10)
            minute = "0" + minute;
        var time = hour + ':' + minute + ' ' + a;
        document.getElementById("ctl00_ContentPlaceHolder1_HiddenField2").value = time;
    }

    function Calling() {

        var date = new Date();
        $("input[id$='txtdate']").datepicker({ dateFormat: 'dd/mm/yy' });

        $('.DatepickerReCall').datepicker({ dateFormat: 'dd/mm/yy' });

          $("input[id$='Tab2']").click(function () {


            if ($("input[id$='TextBox2']").val() == '') {
                alert('Please Enter Registration No !');
                $("input[id$='TextBox2']").focus();
                $("input[id$='TextBox2']").addClass('textboxerr');
                return false;
            }
            else {
                $("input[id$='TextBox2']").removeClass('textboxerr');
            }
        });


        $("input[id$='Button5']").click(function () {

            if ($("select[id$='ddlserviceCat1']").val() == '0') {
                alert('Please Select Service Type !');
                $("select[id$='ddlserviceCat1']").addClass('textboxerr');
                $("select[id$='ddlserviceCat1']").focus();
                return false;
            }
            else {
                $("select[id$='ddlserviceCat1']").removeClass('textboxerr');
            }

            if ($("select[id$='ddlService1']").val() == '0') {
                alert('Please Select Service  !');
                $("select[id$='ddlService1']").addClass('textboxerr');
                $("select[id$='ddlService1']").focus();
                return false;
            }
            else {
                $("select[id$='ddlService1']").removeClass('textboxerr');
            }


            if ($("input[id$='txtTimeperday1']").val() == '') {
                alert('Please Enter Time / Day !');
                $("input[id$='txtTimeperday1']").focus();
                $("input[id$='txtTimeperday1']").addClass('textboxerr');
                return false;
            }
            else {
                $("input[id$='txtTimeperday1']").removeClass('textboxerr');
            }



            if ($("input[id$='txtTotal1']").val() == '') {
                alert('Please Enter Total  !');
                $("input[id$='txtTotal1']").focus();
                $("input[id$='txtTotal1']").addClass('textboxerr');
                return false;
            }
            else {
                $("input[id$='txtTotal1']").removeClass('textboxerr');
            }





            if ($("input[id$='txtDuration1']").val() == '') {
                alert('Please Enter Duration !');
                $("input[id$='txtDuration1']").focus();
                $("input[id$='txtDuration1']").addClass('textboxerr');
                return false;
            }
            else {
                $("input[id$='txtDuration1']").removeClass('textboxerr');
            }
        });


        $("input[id$='Button1']").click(function () {

            if ($("select[id$='DropDownList1']").val() == '0') {
                alert('Please Select Visiting  Doctor!');
                $("select[id$='DropDownList1']").addClass('textboxerr');
                $("select[id$='DropDownList1']").focus();
                return false;
            }
            else {
                $("select[id$='DropDownList1']").removeClass('textboxerr');
            }

            if ($("input[id$='txtdate']").val() == '') {
                alert('Please Enter Visiting Date Properly!');
                $("input[id$='txtdate']").focus();
                $("input[id$='txtdate']").addClass('textboxerr');
                return false;
            }
            else {
                $("input[id$='txtdate']").removeClass('textboxerr');
            }

            if ($("input[id$='TextBox1']").val() == '') {
                alert('Please Enter No of Visit Properly!');
                $("input[id$='TextBox1']").focus();
                $("input[id$='TextBox1']").addClass('textboxerr');
                return false;
            }
            else {
                $("input[id$='TextBox1']").removeClass('textboxerr');
            }
        });

        $("input[id$='TextBox1']").keydown(function (event) {
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


<%--
  <asp:UpdateProgress ID="UpdateProgress1" runat="server">
    <ProgressTemplate>
    
       <div id="aaa" class="progressBackgroundFilter" >          </div>
        <div id="bbb" class="processMessage"  ><img alt="Loading" src="../images/pwait.gif"/>
      
              </div>
    </ProgressTemplate>
    </asp:UpdateProgress>--%>


    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
    <ContentTemplate>
    <div class="pageheader">
                <asp:Label ID="Label1" runat="server">Add Services</asp:Label>
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
                    <asp:HiddenField ID="HiddenField1" runat="server" Value="0" /><asp:HiddenField ID="HiddenField2" runat="server" Value="0" />
                 <%--   <div class="form-sec-row">
                        <label>
                        <strong>Bed No :</strong></label>
                        <asp:TextBox ID="txtBedNo" runat="server" CssClass="textbox-medium1" 
                            Enabled="False" ></asp:TextBox>
                        <div class="clear">
                        </div>
                    </div>--%>
                      <div class="form-sec-row">
                        <label>
                        <strong>
                       Registration No :</strong></label>
                          <asp:TextBox ID="TextBox2" runat="server" CssClass="textbox-medium1" 
                              Enabled="true"></asp:TextBox>
                                <asp:Button ID="Button3" runat="server" Text="Quick Search"  height="28px" CssClass="submit-buttonCheck" OnClientClick="ShowDialog()"/>
                              <asp:Button ID="Button4" runat="server" Text="Fetch"   height="28px"
                               CssClass="submit-button" onclick="Button4_Click"/>
                        <div class="clear">
                        </div>
                    </div>
                        <div class="form-sec-row">
                        <label>
                        <strong>
                       Patient's Name :</strong></label>
                          <asp:TextBox ID="TextBox3" runat="server" CssClass="textbox-medium1" 
                                Enabled="False"></asp:TextBox>
                        <div class="clear">
                        </div>
                    </div>

                        <div class="form-sec-row">
                        <label>
                        <strong>
                       Bed No :</strong></label>
                          <asp:TextBox ID="TextBox4" runat="server" CssClass="textbox-medium1" 
                                Enabled="False"></asp:TextBox>
                        <div class="clear">
                        </div>
                    </div>

                        <div class="form-sec-row">
                        <label>
                        <strong>
                       Admission Date :</strong></label>
                          <asp:TextBox ID="TextBox5" runat="server" CssClass="textbox-medium1" 
                                Enabled="False"></asp:TextBox>
                                      <asp:Label ID="Label3" runat="server" ForeColor="Green" Text="(DD/MM/YYYY)"></asp:Label>
                        <div class="clear">
                        </div>
                    </div>

                    <%--  <div class="form-sec-row">
                        <label>
                        <strong>
                        Service Category :</strong></label>
                        <asp:DropDownList ID="DropDownList1" runat="server" CssClass="textbox-medium1" 
                              onselectedindexchanged="DropDownList1_SelectedIndexChanged" 
                              AutoPostBack="True" >                               
                         </asp:DropDownList>
                        <div class="clear">
                        </div>
                    </div>

                        <div class="form-sec-row">
                        <label>
                        <strong>
                        Service :</strong></label>
                        <asp:DropDownList ID="DropDownList2" runat="server" CssClass="textbox-medium1" >                               
                         </asp:DropDownList>
                        <div class="clear">
                        </div>
                    </div>
                     
                     <div class="form-sec-row">
                        <label>
                        <strong> 
                       Date of Issue:</strong></label>
                        <asp:TextBox ID="txtdate" runat="server" CssClass="textbox-medium1"></asp:TextBox>
                              <asp:Label ID="Label2" runat="server" ForeColor="Green" Text="(DD/MM/YYYY)"></asp:Label>
                        <div class="clear">
                        </div>
                    </div>

                            <div class="form-sec-row">
                        <label>
                        <strong> 
                        Quantity :</strong></label>
                        <asp:TextBox ID="TextBox1" runat="server" CssClass="textbox-medium1" ></asp:TextBox>
                        <div class="clear">
                        </div>
                    </div>
          
          
                            <div class="form-sec-row">
                        <label>
                        <strong> 
                        Duration in Days :</strong></label>
                        <asp:TextBox ID="TextBox6" runat="server" CssClass="textbox-medium1" ></asp:TextBox>
                        <div class="clear">
                        </div>
                    </div>--%>
          
              <div class="form-sec-row">
                        <label>
                        <strong> 
                        Doctor Type :</strong></label>
                        <asp:DropDownList ID="DropDownList4" runat="server" CssClass="textbox-medium1" 
                            onselectedindexchanged="DropDownList4_SelectedIndexChanged" 
                            AutoPostBack="True" >                               
                         </asp:DropDownList>
                        <div class="clear">
                        </div>
                    </div>
                            <div class="form-sec-row">
                        <label>
                        <strong> 
                        Advice By :</strong></label>
                        <asp:DropDownList ID="DropDownList3" runat="server" CssClass="textbox-medium1" >                               
                         </asp:DropDownList>
                        <div class="clear">
                        </div>
                    </div>

                    <div class="form-sec-row">
                        <label>
                        <strong> 
                        Additional Doctor Type</strong></label>
                        <asp:DropDownList ID="DropDownList5" runat="server" CssClass="textbox-medium1" 
                            AutoPostBack="True" 
                            onselectedindexchanged="DropDownList5_SelectedIndexChanged" >                               
                         </asp:DropDownList>
                      
                        <div class="clear">
                        </div>
                    </div>
                        <div class="form-sec-row">
                        <label>
                        <strong> 
                        Additional Doctor:</strong></label>
                          <asp:DropDownList ID="DropDownList6" runat="server" CssClass="textbox-medium1" >                               
                         </asp:DropDownList>
                        <div class="clear">
                        </div>
                    </div>
          
          
                   
   <table border="1" cellpadding="0" style='background-color:#C5B9B9;' cellspacing="0" width="100%">
                        <tr>
                        <td colspan="3" align="center">
                            <div class="pageheader">
         <asp:Label ID="Label4" runat="server"> Patient Service </asp:Label>
     </div>
                        </td>
                        </tr>
          <tr>

           <td><strong>Template Category:</strong> 
                  <asp:DropDownList ID="DropDownList36" runat="server" 
                   Width="180px" 
                   onselectedindexchanged="DropDownList36_SelectedIndexChanged" AutoPostBack="True"  >
                  </asp:DropDownList> </td>
              <td><strong>Service Template :</strong> 
                  <asp:DropDownList ID="DropDownList37" runat="server" AutoPostBack="True" 
                      Width="180px" onselectedindexchanged="DropDownList37_SelectedIndexChanged" 
                     >
                  </asp:DropDownList> </td>

                     <td><strong>Date :</strong> <asp:TextBox ID="txtdate" CssClass="DatepickerReCall"  Width="125px"  runat="server"></asp:TextBox> </td>
</tr>
</table>

       <table border="1" cellpadding="0" cellspacing="0" width="100%">
    <tr>
    <td>
    
                  <div class="pageheader">
         <asp:Label ID="Label5" runat="server"> Preview </asp:Label>
     </div>


                                    <div class="listing" style='width:100%; height:150px; overflow:auto;'>
         <%--<asp:GridView id="GridView2"  CssClass="grid" PagerStyle-CssClass="pgr" DataKeyNames ="RowId" 
                                            runat="server" AutoGenerateColumns="False"
          AllowPaging="True" PageSize ="50"  Width="100%"
                 
              SelectedRowStyle-BackColor="GreenYellow" onpageindexchanging="GridView2_PageIndexChanging" 
                                            onrowcancelingedit="GridView2_RowCancelingEdit" 
                                            onrowdatabound="GridView2_RowDataBound" onrowdeleting="GridView2_RowDeleting" 
                                            onrowediting="GridView2_RowEditing" onrowupdating="GridView2_RowUpdating" >
                <RowStyle HorizontalAlign="Center" />
                <Columns>
                 

                  <asp:TemplateField HeaderText="Sl.No." >
                  <ItemTemplate><asp:Label ID="lblid" runat="server" Text='<%# Bind("RowID") %>'></asp:Label></ItemTemplate></asp:TemplateField>

                    <asp:TemplateField HeaderText="Service Cat Id"   Visible="false">
                  <ItemTemplate><asp:Label ID="lblServiceCategoryId" runat="server" Text='<%# Bind("ServiceCategoryId") %>'></asp:Label></ItemTemplate></asp:TemplateField>

                    <asp:TemplateField HeaderText="Service Category"><ItemTemplate><asp:Label ID="lblServiceCategoryName" runat="server" Text='<%# Bind("ServiceCategoryName") %>'>
                    </asp:Label></ItemTemplate><EditItemTemplate>
        <asp:DropDownList ID="ddlServiceCategory"   Width="110px" runat="server"  AutoPostBack="True" onselectedindexchanged="ddlServiceCategory_SelectedIndexChanged">
        </asp:DropDownList></EditItemTemplate></asp:TemplateField>
                  
                  
                                   <asp:TemplateField HeaderText="Service Name id"  Visible="false">
                  <ItemTemplate><asp:Label ID="lblServiceId" runat="server" Text='<%# Bind("ServiceId") %>'></asp:Label></ItemTemplate></asp:TemplateField>

                      <asp:TemplateField HeaderText="Service"><ItemTemplate><asp:Label ID="lblServiceName" runat="server" Text='<%# Bind("ServiceName") %>'></asp:Label>
                      </ItemTemplate><EditItemTemplate><asp:DropDownList ID="ddlService"   runat="server"></asp:DropDownList></EditItemTemplate></asp:TemplateField> 

                      <asp:TemplateField HeaderText="Times / Day"><ItemTemplate><asp:Label ID="lblTimeperDay" runat="server" Text='<%# Bind("TimeperDay") %>'></asp:Label>
                      </ItemTemplate><EditItemTemplate><asp:TextBox ID="txtTimeperDay"  Width="30px"  runat="server" Text='<%# Bind("TimeperDay") %>'></asp:TextBox>
                      </EditItemTemplate></asp:TemplateField> 

                    <asp:TemplateField HeaderText="Duration"><ItemTemplate><asp:Label ID="lblDuration" runat="server" Text='<%# Bind("Duration") %>'></asp:Label>
                    </ItemTemplate><EditItemTemplate><asp:TextBox ID="txtDuration"  Width="50px"  runat="server" Text='<%# Bind("Duration") %>'></asp:TextBox>
                    </EditItemTemplate></asp:TemplateField>   
                    
                    <asp:TemplateField HeaderText="Total Quantity"><ItemTemplate><asp:Label ID="lblTotalQty" runat="server" Text='<%# Bind("TotalQty") %>'></asp:Label>
                    </ItemTemplate><EditItemTemplate><asp:TextBox ID="txtTotalQuantity"  Width="40px"  runat="server" Text='<%# Bind("TotalQty") %>'></asp:TextBox></EditItemTemplate></asp:TemplateField> 
                       
                    <asp:CommandField HeaderText="Edit" ShowEditButton="True" />
                    <asp:CommandField HeaderText="Delete" ShowDeleteButton="True" />
                </Columns>
                <PagerStyle CssClass="pgr" />
                <EditRowStyle BackColor="#CCFF33" />
                <AlternatingRowStyle BackColor="#FFDB91" />
                <SelectedRowStyle BackColor="GreenYellow" />
        </asp:GridView>--%> 

        <asp:GridView id="GridView7"  CssClass="grid" PagerStyle-CssClass="pgr" DataKeyNames ="RowId" 
                                            runat="server" AutoGenerateColumns="False"
          AllowPaging="True" PageSize ="50"  Width="100%"
                 
              SelectedRowStyle-BackColor="GreenYellow" onpageindexchanging="GridView7_PageIndexChanging" 
                                            onrowcancelingedit="GridView7_RowCancelingEdit" 
                                            onrowdatabound="GridView7_RowDataBound" onrowdeleting="GridView7_RowDeleting" 
                                            onrowediting="GridView7_RowEditing" onrowupdating="GridView7_RowUpdating"  >
                <RowStyle HorizontalAlign="Center" />
                <Columns>
                 

                  <asp:TemplateField HeaderText="Sl.No." >
                  <ItemTemplate><asp:Label ID="lblid" runat="server" Text='<%# Bind("RowID") %>'></asp:Label></ItemTemplate></asp:TemplateField>

                    <asp:TemplateField HeaderText="Service Id"   Visible="false">
                  <ItemTemplate><asp:Label ID="lblServiceId" runat="server" Text='<%# Bind("ServiceId") %>'></asp:Label>
                  </ItemTemplate>
                  </asp:TemplateField>

                    <asp:TemplateField HeaderText="Service"><ItemTemplate><asp:Label ID="lblServiceName" runat="server" Text='<%# Bind("ServiceCategoryName") %>'>
                    </asp:Label></ItemTemplate> 
        </asp:TemplateField> 

                      <asp:TemplateField HeaderText="Quantity">
                      <ItemTemplate>
                      <asp:Label ID="lblQuantity" runat="server" Text='<%# Bind("Quantity") %>'></asp:Label>
                      </ItemTemplate>
                      <EditItemTemplate>
                      <asp:TextBox ID="txtQuantity"  Width="30px"  CssClass="NumberInput"  runat="server" Text='<%# Bind("Quantity") %>'></asp:TextBox>
                      </EditItemTemplate>
                      </asp:TemplateField> 

                    <asp:TemplateField HeaderText="Unit Price">
                    <ItemTemplate><asp:Label ID="lblPrice" runat="server" Text='<%# Bind("Price") %>'></asp:Label>
                    </ItemTemplate>
                    
                    <EditItemTemplate>
                    <asp:TextBox ID="txtPrice"  Width="50px"   CssClass="NumberInput"  runat="server" Text='<%# Bind("Price") %>'></asp:TextBox>
                    </EditItemTemplate>
                    </asp:TemplateField>   
                    
               <%--     <asp:TemplateField HeaderText="Total Price"><ItemTemplate><asp:Label ID="lblTotalQty" runat="server" Text='<%# Bind("TotalPrice") %>'></asp:Label>
                    </ItemTemplate><EditItemTemplate><asp:TextBox ID="txtTotalQuantity"  Width="40px"  CssClass="NumberInput"  runat="server" Text='<%# Bind("TotalPrice") %>'>
                    </asp:TextBox></EditItemTemplate></asp:TemplateField> --%>
                
                    <asp:TemplateField HeaderText="Continue">
                    <ItemTemplate><%--<asp:Label ID="lblcontinue" runat="server" Text='<%# Bind("ServCont") %>'></asp:Label>--%>
                        <asp:CheckBox ID="servcontinue" Width="50px" runat="server" />
                    </ItemTemplate>
                    <EditItemTemplate>
                    <%--<asp:CheckBox ID="servcontinueedit" Width="50px" runat="server" />--%>
                    </EditItemTemplate>
                    </asp:TemplateField> 

                    <asp:TemplateField HeaderText="WithConsumables">
                    <ItemTemplate><%--<asp:Label ID="lblcontinue" runat="server" Text='<%# Bind("ServCont") %>'></asp:Label>--%>
                        <asp:CheckBox ID="chkCons" Width="50px" runat="server" AutoPostBack="true" OnCheckedChanged="add_remove_cons"/>
                    </ItemTemplate>
                    <EditItemTemplate>
                    <%--<asp:CheckBox ID="servcontinueedit" Width="50px" runat="server" />--%>
                    </EditItemTemplate>
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
                    <table border="1" cellpadding="0" cellspacing="0" width="100%">
    <tr>
    <td>
    
                  <div class="pageheader">
         <asp:Label ID="Label6" runat="server"> Consumables Preview </asp:Label>
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
                    
                                      
                    <%--<asp:CommandField HeaderText="Edit" ShowEditButton="True" />--%>
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
                        <div class="clear">
                        </div>
                    </div>
                    <div class="form-sec-row">
                        &nbsp;&nbsp;&nbsp;<label><strong>&nbsp;</strong></label>
                        <asp:Button ID="Button1" runat="server" CssClass="submit-button"   height="30px"
                            Text="Submit" onclick="Button1_Click" />
                        <asp:Button ID="Button2" runat="server" CssClass="submit-button"  height="30px"
                            Text="Cancel" onclick="Button2_Click"  />
                        <div class="clear">
                        </div>
                    </div>
                    <div class="clear">
                    </div>
                </div>
                </asp:View>
                 <asp:View ID="View2" runat="server">
            
            <div class="listing">
                <%--<asp:GridView id="GridView1"  Width="100%" CssClass="grid" PagerStyle-CssClass="pgr"
                 DataKeyNames ="RowID" runat="server" 
                 AutoGenerateColumns="False" AllowPaging="True" 
                 SelectedRowStyle-BackColor="GreenYellow" 
                    onpageindexchanging="GridView1_PageIndexChanging" 
                    onrowcommand="GridView1_RowCommand" >
                    <RowStyle HorizontalAlign="Center" />
                    <Columns>
                        <asp:TemplateField HeaderText="ID" Visible="false"><ItemTemplate>
                        <asp:Label ID="lblid" runat="server"  Text='<%# Bind("RowID") %>'>
                        </asp:Label>
                        </ItemTemplate>
                        </asp:TemplateField>
             
                        <asp:TemplateField HeaderText="Regn. No" Visible="false">
                        <ItemTemplate><asp:Label ID="lblRegno" runat="server" 
                        Text='<%# Bind("PatientReg") %>'></asp:Label></ItemTemplate></asp:TemplateField>


                         <asp:TemplateField HeaderText="Patient's Name" Visible="false">
                         <ItemTemplate><asp:Label ID="lbllname" runat="server" Text='<%# Bind("patient_name") %>'></asp:Label></ItemTemplate></asp:TemplateField>

                         <asp:TemplateField HeaderText="Admission Date" Visible="false">
                         <ItemTemplate><asp:Label ID="lbladate" runat="server" Text='<%# Bind("adate") %>'></asp:Label></ItemTemplate></asp:TemplateField>

                        <asp:TemplateField HeaderText="Bed No" Visible="false">
                        <ItemTemplate><asp:Label ID="lblbedno" runat="server" Text='<%# Bind("BedNoText") %>'></asp:Label></ItemTemplate></asp:TemplateField>
 
                             <asp:TemplateField HeaderText="Service Name" >
                             <ItemTemplate><asp:Label ID="lblservice" runat="server" Text='<%# Bind("ServiceTemplateName") %>'></asp:Label></ItemTemplate></asp:TemplateField>   

                             <asp:TemplateField HeaderText="Issue Date" ><ItemTemplate><asp:Label ID="lbllblissuedate" runat="server"
                              Text='<%# Bind("isdate") %>'></asp:Label></ItemTemplate></asp:TemplateField>

                         <asp:TemplateField HeaderText="Quantity" ><ItemTemplate><asp:Label ID="lblquantity" runat="server" Text='<%# Bind("Quantity") %>'></asp:Label>
                         </ItemTemplate></asp:TemplateField>
  

        <asp:TemplateField HeaderText="Adviced By" Visible="false" ><ItemTemplate><asp:Label ID="lbldoctype" runat="server" Text='<%# Bind("DoctorType") %>'>
        </asp:Label></ItemTemplate></asp:TemplateField>

  <asp:TemplateField HeaderText="Adviced By" Visible="false" ><ItemTemplate><asp:Label ID="lbladditiondoctype" runat="server" Text='<%# Bind("DoctorId") %>'>
  </asp:Label></ItemTemplate></asp:TemplateField>
     <asp:TemplateField HeaderText="Adviced By" Visible="false" ><ItemTemplate><asp:Label ID="lblAddDoctorType" runat="server" Text='<%# Bind("AddDoctorType") %>'>
     </asp:Label></ItemTemplate></asp:TemplateField>
         <asp:TemplateField HeaderText="Adviced By" Visible="false" ><ItemTemplate><asp:Label ID="lblAddDoctorId" runat="server" Text='<%# Bind("AddDoctorId") %>'>
     </asp:Label></ItemTemplate></asp:TemplateField>
                                        
  <asp:CommandField SelectText="Edit" ShowSelectButton="True" ControlStyle-CssClass="temp" HeaderText="Edit" SelectImageUrl="~/Images/edit.png" ButtonType="Image">
  <ControlStyle CssClass="temp"></ControlStyle></asp:CommandField>
                        <asp:CommandField ShowDeleteButton="True" ControlStyle-CssClass="temp" HeaderText="Delete" DeleteImageUrl="~/Images/delete.png" ButtonType="Image">
                            <ControlStyle CssClass="temp"></ControlStyle>
                        </asp:CommandField>
                    </Columns>
                    <PagerStyle CssClass="pgr" />
                    <EditRowStyle BackColor="#CCFF33" />
                    <AlternatingRowStyle BackColor="#FFDB91" />
                </asp:GridView>--%>

                <asp:GridView id="GridView1"  CssClass="grid" PagerStyle-CssClass="pgr" DataKeyNames ="RowID" 
                                            runat="server" AutoGenerateColumns="False"
          AllowPaging="True" PageSize ="50"  Width="100%"
                 
              SelectedRowStyle-BackColor="GreenYellow" 
                    onpageindexchanging="GridView1_PageIndexChanging" 
                    onrowcancelingedit="GridView1_RowCancelingEdit" 
                    onrowdatabound="GridView1_RowDataBound" onrowdeleting="GridView1_RowDeleting" 
                    onrowediting="GridView1_RowEditing" onrowupdating="GridView1_RowUpdating">
                <RowStyle HorizontalAlign="Center" />
                <Columns>
                 
                     <asp:TemplateField HeaderText="ID" Visible="false"><ItemTemplate>
                        <asp:Label ID="lblid" runat="server"  Text='<%# Bind("RowID") %>'>
                        </asp:Label>
                        </ItemTemplate>
                        </asp:TemplateField>

                        
                <asp:TemplateField HeaderText="Category Id"   Visible="false">
                  <ItemTemplate>
                  <asp:Label ID="lblTemplateCategoryId" runat="server" Text='<%# Bind("TemplateCategoryId") %>'></asp:Label>
                  </ItemTemplate>
                  </asp:TemplateField>

                  

              <asp:TemplateField HeaderText="Category Name" >
              <ItemTemplate>
              <asp:Label ID="lblserviceServiceCategory" runat="server" Text='<%# Bind("CategoryName") %>'></asp:Label>
              </ItemTemplate>

                  <EditItemTemplate>
                      <asp:DropDownList ID="ddlServiceCategory" runat="server"   onselectedindexchanged="ddlServiceCategory_SelectedIndexChanged" AutoPostBack="True" >
                      </asp:DropDownList>
              </EditItemTemplate>
              </asp:TemplateField>  
              

                    <asp:TemplateField HeaderText="Service Id"   Visible="false">
                  <ItemTemplate><asp:Label ID="lblServiceId" runat="server" Text='<%# Bind("ServiceId") %>'></asp:Label>
                  </ItemTemplate>
                  </asp:TemplateField>

                  

              <asp:TemplateField HeaderText="Service Name" >
              <ItemTemplate>
              <asp:Label ID="lblservice" runat="server" Text='<%# Bind("ServiceTemplateName") %>'></asp:Label>
              </ItemTemplate>
                  <EditItemTemplate>
                      <asp:DropDownList ID="ddlService" runat="server">
                      </asp:DropDownList>
              </EditItemTemplate>
              </asp:TemplateField>  
               
                        
                        
                            <asp:TemplateField HeaderText="Issue Date" >
                            <ItemTemplate>
                            <asp:Label ID="lbllblissuedate" runat="server"      Text='<%# Bind("isdate") %>'></asp:Label>
                            </ItemTemplate>

                             <EditItemTemplate>
                      <asp:TextBox ID="txtgDate"  Width="80px"  CssClass="NumberInput"   runat="server" Text='<%# Bind("isdate") %>'></asp:TextBox>
                      
              </EditItemTemplate>

                            
                            </asp:TemplateField>

                        <asp:TemplateField HeaderText="Quantity">
                      <ItemTemplate>
                      <asp:Label ID="lblQuantity" runat="server" Text='<%# Bind("Quantity") %>'></asp:Label>
                      </ItemTemplate>
                      <EditItemTemplate>
                      <asp:TextBox ID="txtQuantity"  Width="30px"  CssClass="NumberInput"  runat="server" Text='<%# Bind("Quantity") %>'></asp:TextBox>
                      </EditItemTemplate>
                      </asp:TemplateField> 


                      

                    <asp:TemplateField HeaderText="Unit Price">
                    <ItemTemplate><asp:Label ID="lblPrice" runat="server" Text='<%# Bind("Price") %>'></asp:Label>
                    </ItemTemplate>
                    
                    <EditItemTemplate>
                    <asp:TextBox ID="txtPrice"  Width="50px"   CssClass="NumberInput"  runat="server" Text='<%# Bind("Price") %>'></asp:TextBox>
                    </EditItemTemplate>
                    </asp:TemplateField> 
                    <asp:TemplateField HeaderText="Continue" Visible="false">
                    <ItemTemplate>
                        <asp:Label ID="lblcont" Width="50px" runat="server" Text='<%# Bind("ServCont") %>' Visible="false"></asp:Label>
                    </ItemTemplate>
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="Continue">
                    <ItemTemplate>
                        
                        <asp:CheckBox ID="chkcont" Width="50px" runat="server" Enabled="false"/>
                    </ItemTemplate>
                    
                    <EditItemTemplate>
                    <asp:CheckBox ID="cont" Width="50px" runat="server" />
                    </EditItemTemplate>
                    </asp:TemplateField>
                  
                    <asp:CommandField HeaderText="Edit" ShowEditButton="True" />
                    <asp:CommandField HeaderText="Delete" ShowDeleteButton="True" />
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

