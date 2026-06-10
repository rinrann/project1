<%@ Page Title="" Language="C#" MasterPageFile="~/MasterAll/MasterPageAll.master" AutoEventWireup="true" CodeFile="AddConsumable.aspx.cs" Inherits="IPD_AddConsumable" %>

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

            if ($("select[id$='ddlconsumablegr1']").val() == '0') {
                alert('Please Select Consumable Group !');
                $("select[id$='ddlconsumablegr1']").addClass('textboxerr');
                $("select[id$='ddlconsumablegr1']").focus();
                return false;
            }
            else {
                $("select[id$='ddlconsumablegr1']").removeClass('textboxerr');
            }

            if ($("select[id$='ddlConsumableItem1']").val() == '0') {
                alert('Please Select Consumable Item !');
                $("select[id$='ddlConsumableItem1']").addClass('textboxerr');
                $("select[id$='ddlConsumableItem1']").focus();
                return false;
            }
            else {
                $("select[id$='ddlConsumableItem1']").removeClass('textboxerr');
            }

            if ($("input[id$='txtActualQty1']").val() == '') {
                alert('Please Enter Actual Quantity !');
                $("input[id$='txtActualQty1']").focus();
                $("input[id$='txtActualQty1']").addClass('textboxerr');
                return false;
            }
            else {
                $("input[id$='txtActualQty1']").removeClass('textboxerr');
            }

            if ($("input[id$='txtBillQty1']").val() == '') {
                alert('Please Enter Bill Quantity !');
                $("input[id$='txtBillQty1']").focus();
                $("input[id$='txtBillQty1']").addClass('textboxerr');
                return false;
            }
            else {
                $("input[id$='txtBillQty1']").removeClass('textboxerr');
            }
        });

        $("input[id$='Button1']").click(function () {

            if ($("select[id$='DropDownList4']").val() == '0') {
                alert('Please Select Visiting  Doctor Type !');
                $("select[id$='DropDownList4']").addClass('textboxerr');
                $("select[id$='DropDownList4']").focus();
                return false;
            }
            else {
                $("select[id$='DropDownList4']").removeClass('textboxerr');
            }

            if ($("select[id$='DropDownList3']").val() == '0') {
                alert('Please Select Visiting  Doctor!');
                $("select[id$='DropDownList3']").addClass('textboxerr');
                $("select[id$='DropDownList3']").focus();
                return false;
            }
            else {
                $("select[id$='DropDownList3']").removeClass('textboxerr');
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
        });


        $('.nonumber').keydown(function (event) {
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


  <asp:UpdateProgress ID="UpdateProgress2" runat="server">
    <ProgressTemplate>
    
       <div id="aaa" class="progressBackgroundFilter">          </div>
        <div id="bbb" class="processMessage"><img alt="Loading" src="../images/pwait.gif"/>
      
              </div>
    </ProgressTemplate>
    </asp:UpdateProgress>


    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
    <ContentTemplate>
    <div class="pageheader">
                <asp:Label ID="Label1" runat="server">Add Consumable</asp:Label>
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
                 
                      <div class="form-sec-row">
                        <label>
                        <strong>
                       Registration No :</strong></label>
                          <asp:TextBox ID="TextBox2" runat="server" CssClass="textbox-medium1" 
                              Enabled="true"></asp:TextBox>
                                <asp:Button ID="Button3" runat="server" Text="Quick Search" height="27px" CssClass="submit-buttonCheck" OnClientClick="ShowDialog()"/>
                              <asp:Button ID="Button4" runat="server" Text="Fetch"  height="27px"
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
                        <div class="clear">
                        </div>
                    </div>
<%--
                      <div class="form-sec-row">
                        <label>
                        <strong>
                        Consumable Group :</strong></label>
                        <asp:DropDownList ID="DropDownList1" runat="server" CssClass="textbox-medium1" 
                              AutoPostBack="True" onselectedindexchanged="DropDownList1_SelectedIndexChanged" 
                             >                               
                         </asp:DropDownList>
                        <div class="clear">
                        </div>
                    </div>

                        <div class="form-sec-row">
                        <label>
                        <strong>
                        Consumable Item :</strong></label>
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
                        <div class="clear">
                        </div>
                    </div>

                            <div class="form-sec-row">
                        <label>
                        <strong> 
                        Actual Quantity :</strong></label>
                        <asp:TextBox ID="TextBox1" runat="server" CssClass="textbox-medium1" ></asp:TextBox>
                        <div class="clear">
                        </div>
                    </div>
          
          
                            <div class="form-sec-row">
                        <label>
                        <strong> 
                        Bill Quantity :</strong></label>
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
                        Additional Doctor Type :</strong></label>
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

                      <div class="form-sec-row">
                        <label>
                        <strong> 
                        Date :</strong></label>
                         <asp:TextBox ID="txtdate" CssClass="textbox-medium1" runat="server"></asp:TextBox>
                        <div class="clear">
                        </div>
                    </div>


                    
<%--This portion not Required          ... Because consumable will comes from service ..........--%>

        <table border="1" cellpadding="0" style='background-color:#C5B9B9; display:none;' cellspacing="0" width="100%"  >
                        <tr>
                        <td colspan="3" align="center">
                            <div class="pageheader">
         <asp:Label ID="Label3" runat="server"> Patient Consumable</asp:Label>
     </div>
                        </td>
                        </tr>
          <tr>

           <td><strong>Consumable Template Group:</strong> 
                  <asp:DropDownList ID="DropDownList65" runat="server" AutoPostBack="True" 
                   Width="180px" 
                   onselectedindexchanged="DropDownList65_SelectedIndexChanged" >
                  </asp:DropDownList> </td>
              <td><strong>Consumable Template :</strong> 
                  <asp:DropDownList ID="DropDownList66" runat="server" AutoPostBack="True"  
                      Width="180px" onselectedindexchanged="DropDownList66_SelectedIndexChanged"
                     >
                  </asp:DropDownList> </td>

                  <%--  <td><strong>Date :</strong> <asp:TextBox ID="txtdate" CssClass="DatepickerReCall"  Width="125px"  runat="server"></asp:TextBox> </td>  --%>
</tr>
</table>
 
      
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
             <label class="lname"><strong> Actual Quantity </strong></label>
            </div>                  
</td>
 <td align="center">
                    <div class="form-sec-row"> 
             <label class="lname"><strong>Bill Quantity</strong></label> 
            </div>
            
</td>

 <td align="center">
                    

</td>
                
            </tr>
                <tr>
                <td align="center">
               
             <asp:DropDownList ID="ddlconsumablegr1" runat="server" CssClass="textbox-medium1"   AutoPostBack="true"
                            Width="150px" 
                        onselectedindexchanged="ddlconsumablegr1_SelectedIndexChanged">
                            </asp:DropDownList>
        
            
</td> 

                <td align="center"> 
             <asp:DropDownList ID="ddlConsumableItem1" runat="server" CssClass="textbox-medium1" 
                        Width="150px"  >
                            </asp:DropDownList>
             
</td>
                <td align="center">
                      
              <asp:TextBox ID="txtActualQty1" runat="server" CssClass="nonumber"   Width="150px"></asp:TextBox>
                      
</td>
 <td align="center">
                 
                       <asp:TextBox ID="txtBillQty1" runat="server"  CssClass="nonumber"  Width="150px"></asp:TextBox>
             
            
</td>
 

<td align="center">
    <asp:Button ID="Button5" runat="server" Text="add" CssClass="submit-button" 
        onclick="Button5_Click" />
</td>
 
 </tr>

 </table> 

 <table border="1" cellpadding="0" cellspacing="0" width="100%">
    <tr>
    <td>
    
                  <div class="pageheader">
         <asp:Label ID="Label6" runat="server"> Preview </asp:Label>
     </div>


                                    <div class="listing" style='width:100%; height:150px; overflow:auto;'>
         <asp:GridView id="GridView3"  CssClass="grid" PagerStyle-CssClass="pgr" DataKeyNames ="RowID" 
                                            runat="server" AutoGenerateColumns="False"
          AllowPaging="True" PageSize ="50"  Width="100%"
                 
              SelectedRowStyle-BackColor="GreenYellow" onpageindexchanging="GridView3_PageIndexChanging" 
                                            onrowcancelingedit="GridView3_RowCancelingEdit" 
                                            onrowdatabound="GridView3_RowDataBound" onrowdeleting="GridView3_RowDeleting" 
                                            onrowediting="GridView3_RowEditing" onrowupdating="GridView3_RowUpdating" >
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
                    <asp:DropDownList ID="ddlConGroupName"   runat="server"  Width="110px"  AutoPostBack="true"   onselectedindexchanged="ddlConGroupName_SelectedIndexChanged" >
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

                      <asp:TemplateField HeaderText="Actual Quantity"><ItemTemplate><asp:Label ID="lblActualQty" runat="server" Text='<%# Bind("ActualQty") %>'>
                      </asp:Label></ItemTemplate><EditItemTemplate><asp:TextBox ID="txtActualQty" CssClass="nonumber"  Width="30px"  runat="server" Text='<%# Bind("ActualQty") %>'>
                      </asp:TextBox>
                      </EditItemTemplate>
                      </asp:TemplateField> 

                    <asp:TemplateField HeaderText="Bill Quantity"><ItemTemplate><asp:Label ID="lblBillQty" runat="server" Text='<%# Bind("BillQty") %>'>
                    </asp:Label>
                    </ItemTemplate><EditItemTemplate><asp:TextBox ID="txtBillQty"  Width="30px" CssClass="nonumber" runat="server" Text='<%# Bind("BillQty") %>'></asp:TextBox></EditItemTemplate>
                    </asp:TemplateField>                    
                                  <asp:TemplateField HeaderText="Price">
                                  <ItemTemplate><asp:Label ID="lblPrice" runat="server" Text='<%# Bind("Price") %>'></asp:Label>
                    </ItemTemplate>
                    <EditItemTemplate>
                    <asp:TextBox ID="txtPrice"  Width="30px" CssClass="nonumber" runat="server" Text='<%# Bind("Price") %>'></asp:TextBox>
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
          

                    <div class="form-sec-row">
                        <div class="clear">
                        </div>
                    </div>
                    <div class="form-sec-row">
                        &nbsp;&nbsp;&nbsp;<label><strong>&nbsp;</strong></label>
                        <asp:Button ID="Button1" runat="server" CssClass="submit-button" height="30px"
                            Text="Submit" onclick="Button1_Click" />
                        <asp:Button ID="Button2" runat="server" CssClass="submit-button" height="30px"
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
                <asp:GridView id="GridView1"  Width="100%" CssClass="grid" PagerStyle-CssClass="pgr"
                 DataKeyNames ="RowID" runat="server" 
                 AutoGenerateColumns="False" AllowPaging="True" 
                 SelectedRowStyle-BackColor="GreenYellow" 
                    onpageindexchanging="GridView1_PageIndexChanging" 
                    onrowcommand="GridView1_RowCommand" OnRowDataBound="GridView1_RowDataBound" onrowdeleting="GridView1_RowDeleting" >
                    <RowStyle HorizontalAlign="Center" />
                    <Columns>
                        <asp:TemplateField HeaderText="ID" Visible="false">
                            <ItemTemplate>
                                <asp:Label ID="lblid" runat="server" Text='<%# Bind("RowID") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
             
                        <asp:TemplateField HeaderText="Registration No" Visible="false">
                            <ItemTemplate>
                                <asp:Label ID="lblRegno" runat="server" Text='<%# Bind("PatientReg") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>


                         <asp:TemplateField HeaderText="Patient's Name" Visible="false">
                            <ItemTemplate>
                                <asp:Label ID="lbllname" runat="server" Text='<%# Bind("patient_name") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>

                         <asp:TemplateField HeaderText="Admission Date" Visible="false">
                            <ItemTemplate>
                                <asp:Label ID="lbladate" runat="server" Text='<%# Bind("adate") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="Bed No" Visible="false">
                            <ItemTemplate>
                                <asp:Label ID="lblbedno" runat="server" Text='<%# Bind("BedNoText") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>

                         <asp:TemplateField HeaderText="Consumable Group Id"  Visible="false">
                            <ItemTemplate>
                                <asp:Label ID="lblConsumableGrpId" runat="server" Text='<%# Bind("ConsumableGrpId") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>

                             <asp:TemplateField HeaderText="Consumable Group" >
                            <ItemTemplate>
                                <asp:Label ID="lblConGroupName" runat="server" Text='<%# Bind("ConGroupName") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>


                             <asp:TemplateField HeaderText="Consumable Item" >
                            <ItemTemplate>
                                <asp:Label ID="lblConItemName" runat="server" Text='<%# Bind("ConItemName") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>   

                             <asp:TemplateField HeaderText="Issue Date" >
                            <ItemTemplate>
                                <asp:Label ID="lblisdate" runat="server" Text='<%# Bind("isdate") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>

                         <asp:TemplateField HeaderText="Quantity" >
                            <ItemTemplate>
                                <asp:Label ID="lblActualQty" runat="server" Text='<%# Bind("ActualQty") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                                        
                                         <asp:TemplateField HeaderText="Bill Quantity" >
                            <ItemTemplate>
                                <asp:Label ID="lblBillQty" runat="server" Text='<%# Bind("BillQty") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                                        
                                      

                               <asp:TemplateField HeaderText="Adviced By" Visible="false" >
                            <ItemTemplate>
                                <asp:Label ID="lbldoctype" runat="server" Text='<%# Bind("DoctypeID") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>

                           <asp:TemplateField HeaderText="Adviced By" Visible="false" >
                            <ItemTemplate>
                                <asp:Label ID="lbladvicedby" runat="server" Text='<%# Bind("AdviceBy") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>


                               <asp:TemplateField HeaderText="Adviced By" Visible="false" >
                            <ItemTemplate>
                                <asp:Label ID="lbladditiondoctype" runat="server" Text='<%# Bind("AddDoctypeID") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>

                               <asp:TemplateField HeaderText="Adviced By" Visible="false" >
                            <ItemTemplate>
                                <asp:Label ID="lbladddoc" runat="server" Text='<%# Bind("AddAdviceby") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                                        
                                        
                        <asp:CommandField SelectText="Edit" ShowSelectButton="True" ControlStyle-CssClass="temp" HeaderText="Edit" SelectImageUrl="~/Images/edit.png" ButtonType="Image">
                            <ControlStyle CssClass="temp"></ControlStyle>
                        </asp:CommandField>

                         <asp:CommandField ShowDeleteButton="True" ControlStyle-CssClass="temp" HeaderText="Delete" DeleteImageUrl="~/Images/delete.png" ButtonType="Image">
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

