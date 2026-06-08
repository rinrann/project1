<%@ Page Title="" Language="C#" MasterPageFile="~/MasterAll/MasterPageAll.master" AutoEventWireup="true" CodeFile="OTRequisition.aspx.cs" Inherits="IPD_OTRequisition" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">


    <script type="text/javascript" language="javascript">
    
    function ShowDialog() {
        var rtvalue = window.open("PoppReqPatient.aspx", "sss", "Width:800px; Height:550px; dialogLeft:250px;");
        document.getElementById("ctl00_ContentPlaceHolder1_txtreg").value = rtvalue.NameValue;
    }

    function ConfirmationMessage() {
        var data = confirm("Save ? If yes Click Ok or Cancel");
        if (data) {
            return true;
        } else {
            return false;
        } 
    }


    function showStuff(id) {
        var el = document.getElementById(id);
        if (el.style.display != 'none') {
            el.style.display = 'none';
        }
        else {
            el.style.display = '';

        }
    }


    function ShowDialog1() {
        var rtvalue = window.open("OTPopup.aspx", "sss", "Width:800px; Height:550px; dialogLeft:250px;");
        var b = rtvalue.NameValue.split("#");
        //document.getElementById("ctl00_ContentPlaceHolder1_HiddenField1").value = b[0];
        //document.getElementById("ctl00_ContentPlaceHolder1_txtOperationType").value = b[1];
        var a = rtvalue.ProfessionValue.split("#");
        document.getElementById("ctl00_ContentPlaceHolder1_HiddenField2").value = a[0];
        document.getElementById("ctl00_ContentPlaceHolder1_txtOperationName").value = a[1];
        document.getElementById("ctl00_ContentPlaceHolder1_ddlOperationType").value = b[0];

    }
    function printDiv(divID) {
        var divElements = document.getElementById(divID).innerHTML;
        var oldPage = document.body.innerHTML;
        document.body.innerHTML = "<html><head><title></title></head><body>" + divElements + "</body>";
        window.print();
        document.body.innerHTML = oldPage;
    }


    function DisableBackButton() {
        window.history.forward()
    }
    DisableBackButton();
    window.onload = DisableBackButton;
    window.onpageshow = function (evt) { if (evt.persisted) DisableBackButton() }
    window.onunload = function () { void (0) }

    function Calling() {
        var date = new Date();
        $("input[id$='txtOTDate']").datepicker({ dateFormat: 'dd/mm/yy'});

        $("input[id$='txtStartTime']").timepicker({
            showPeriod: true,
            showLeadingZero: true
        });

        $("input[id$='txtendtime']").timepicker({
            showPeriod: true,
            showLeadingZero: true
        });

        $("input[id$='Button1']").click(function () {

            if ($("input[id$='txtreg']").val() == '') {
                alert('Please Enter Registration No !');
                $("input[id$='txtreg']").focus();
                $("input[id$='txtreg']").addClass('textboxerr');
                return false;
            }
            else {
                $("input[id$='txtreg']").removeClass('textboxerr');
            }

            if ($("input[id$='txtName']").val() == '') {
                alert('Please Enter Patient Name!');
                $("input[id$='txtName']").focus();
                $("input[id$='txtName']").addClass('textboxerr');
                return false;
            }
            else {
                $("input[id$='txtName']").removeClass('textboxerr');
            }

            if ($("select[id$='ddlSurgeonName']").val() == '0') {
                alert('Please Select Surgeon!');
                $("select[id$='ddlSurgeonName']").addClass('textboxerr');
                $("select[id$='ddlSurgeonName']").focus();
                return false;
            }
            else {
                $("select[id$='ddlSurgeonName']").removeClass('textboxerr');
            }

            if ($("select[id$='ddlAnesthesia1']").val() == '0') {
                alert('Please Select Anethesist!');
                $("select[id$='ddlAnesthesia1']").addClass('textboxerr');
                $("select[id$='ddlAnesthesia1']").focus();
                return false;
            }
            else {
                $("select[id$='ddlAnesthesia1']").removeClass('textboxerr');
            }


            if ($("input[id$='txtOperationName']").val() == '') {
                alert('Please Select Operation !');
                $("input[id$='txtOperationName']").focus();
                $("input[id$='txtOperationName']").addClass('textboxerr');
                return false;
            }
            else {
                $("input[id$='txtOperationName']").removeClass('textboxerr');
            }

            if ($("input[id$='txtreqno']").val() == '') {
                alert('Please Enter Requisition No !');
                $("input[id$='txtreqno']").focus();
                $("input[id$='txtreqno']").addClass('textboxerr');
                return false;
            }
            else {
                $("input[id$='txtreqno']").removeClass('textboxerr');
            }

            if ($("input[id$='txtStartTime']").val() == '') {
                alert('Please Enter Start Time !');
                $("input[id$='txtStartTime']").focus();
                $("input[id$='txtStartTime']").addClass('textboxerr');
                return false;
            }
            else {
                $("input[id$='txtStartTime']").removeClass('textboxerr');
            }

            if ($("input[id$='txtOTDate']").val() == '') {
                alert('Please Enter Operation Date !');
                $("input[id$='txtOTDate']").focus();
                $("input[id$='txtOTDate']").addClass('textboxerr');
                return false;
            }
            else {
                $("input[id$='txtOTDate']").removeClass('textboxerr');
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
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">


<%--For Busy Loader .............................--%>

<%-- <asp:UpdateProgress ID="UpdateProgress1" runat="server">
    <ProgressTemplate>
       <div id="aaa" class="progressBackgroundFilter"></div>
        <div id="bbb" class="processMessage"><img alt="Loading" src="../images/pwait.gif"/>
              </div>
    </ProgressTemplate>
    </asp:UpdateProgress>
--%>
    <%--For Busy Loader End.............................--%>

    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
    <ContentTemplate>
    <div id="h1">
     <div class="pageheader">
         <asp:Label ID="Label1" runat="server">OT Requisition</asp:Label>
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
           <asp:HiddenField ID="HiddenField1" runat="server" /><asp:HiddenField ID="HiddenField2" runat="server" />
			<div class="form-sec-row">
                <label><strong>Registration No :</strong></label> 
               <asp:TextBox ID="txtreg" runat="server" CssClass="textbox-medium1" Enabled="False" 
                   ></asp:TextBox> 
                <asp:Button ID="Button4" runat="server" 
                    CssClass="submit-search" Text="Quick Search" OnClientClick="ShowDialog()"  Height="28px" />
                   <asp:Button ID="Button5" runat="server" CssClass="submit-button"   Height="28px" 
                    Text="Fetch" onclick="Button5_Click" />
                <div class="clear">  </div>
            </div>
             <div class="form-sec-row">
                <label><strong>Patient's Name :</strong></label>
                <asp:TextBox ID="txtName" runat="server" CssClass="textbox-medium1" Enabled="False" 
                   ></asp:TextBox>
                <div class="clear"></div>
            </div>

             <div class="form-sec-row">
                <label><strong>Current Bed No:</strong></label>
                <asp:TextBox ID="txtCurrentBed" runat="server" CssClass="textbox-medium1" Enabled="False" 
                   ></asp:TextBox>
                <div class="clear"></div>
            </div>
              <div class="form-sec-row">
                <label><strong>Admission Date :</strong></label>
                <asp:TextBox ID="txtAdmissionDate" runat="server" CssClass="textbox-medium1" Enabled="False" 
                   ></asp:TextBox>
                         <asp:Label ID="Label3" runat="server" ForeColor="Green" Text="(DD/MM/YYYY)"></asp:Label>
                <div class="clear"></div>
            </div>

            <div class="form-sec-row">
                <label><strong>OT Requisition No :</strong></label>
                <asp:TextBox ID="txtreqno" runat="server" CssClass="textbox-medium1" Enabled="False" 
                   ></asp:TextBox>
                <div class="clear"></div>
            </div>
           
 
             <div class="form-sec-row">
                <label><strong>Select Operation :</strong></label>
                                <asp:TextBox ID="txtOperationName" runat="server" 
                     CssClass="textbox-medium1" Enabled="False" 
                   ></asp:TextBox>                <asp:Button ID="Button8" runat="server" 
                    CssClass="submit-search" Text="Quick Search" OnClientClick="ShowDialog1()"  Height="27px" OnClick="Button8_Click"  />
                   
                
                <div class="clear"></div>
            </div>

               <div class="form-sec-row">
                <label><strong>Operation Type :</strong></label>
                                <asp:TextBox ID="txtOperationType" runat="server" 
                       CssClass="textbox-medium1" Enabled="False" Visible="false"></asp:TextBox>
                   <asp:DropDownList ID="ddlOperationType" runat="server" CssClass="textbox-medium1"></asp:DropDownList> 
                
                   <div class="clear"></div>
            </div>

                    <div class="form-sec-row" >
                <label><strong>Surgeon Name :</strong></label>
                <div style='float:left;'>
                  <asp:DropDownList ID="ddlSurgeonName" runat="server" CssClass="textbox-medium1">
                </asp:DropDownList></div><div style='padding-left:10px;'>
                                 <asp:Button ID="Button6" runat="server" CssClass="submit-button" Text="ADD DR."  Height="28px" Width="80px" OnClientClick="showStuff('Div1'); return false;"/>
                                 </div>
                          <div class="clear"></div>
 </div>
            <div id="Div1" style="display:none;">
              <div class="form-sec-row">
                <label><strong>Additional Doctor 1 :</strong></label>
                  <asp:DropDownList ID="ddlAddDoc1" runat="server" CssClass="textbox-medium1">
                </asp:DropDownList>
                <div class="clear"></div>
            </div>
              <div class="form-sec-row">
                <label><strong>Additional Doctor 2 :</strong></label>
                   <asp:DropDownList ID="ddlAddDoc2" runat="server" CssClass="textbox-medium1">
                </asp:DropDownList>
                <div class="clear"></div>
            </div>
              <div class="form-sec-row">
                <label><strong>Additional Doctor 3 :</strong></label>
                 <asp:DropDownList ID="ddlAddDoc3" runat="server" CssClass="textbox-medium1">
                </asp:DropDownList>
                <div class="clear"></div>
            </div>
            </div>
              <div class="form-sec-row">
                <label><strong>Anesthetist’s Name :<span style="color:red">*</span></strong></label>
                 <div style='float:left;'>
                <asp:DropDownList ID="ddlAnesthesia1" runat="server" CssClass="textbox-medium1">
                </asp:DropDownList></div> <div style='padding-left:10px;'>
                  <asp:Button ID="Button3" runat="server" CssClass="submit-button" Text="ADD Anth."  Height="28px" Width="80px" OnClientClick="showStuff('Div2'); return false;"/>
                  </div>
                <div class="clear"></div>
            </div>
       <div id="Div2" style="display:none;">
         <div class="form-sec-row">
                <label><strong>Additional Anesthetist :</strong></label>
                <asp:DropDownList ID="ddlAnesthesia2" runat="server" CssClass="textbox-medium1">
                </asp:DropDownList>
                <div class="clear"></div>
            </div>
            </div>
                     <div class="form-sec-row">
                <label><strong>OT Room :</strong></label>
                <asp:DropDownList ID="ddlOTRoom" runat="server" CssClass="textbox-medium1">
                </asp:DropDownList>
                <div class="clear"></div>
            </div>

       <div class="form-sec-row">
                <label><strong>Implant :</strong></label>
                 <%--<asp:TextBox ID="txtImplant" runat="server" CssClass="textbox-medium1"></asp:TextBox>--%>
           <asp:DropDownList ID="ddlImplant" runat="server" CssClass="textbox-medium1"></asp:DropDownList> 
                <div class="clear"></div>
            </div>
               <div class="form-sec-row">
                <label><strong>Operation Date :<span style="color:red">*</span></strong></label>
                 <asp:TextBox ID="txtOTDate" runat="server" CssClass="textbox-medium1"></asp:TextBox>
                       <asp:Label ID="Label2" runat="server" ForeColor="Green" Text="(DD/MM/YYYY)"></asp:Label>
                <div class="clear"></div>
            </div>
             <div class="form-sec-row">
                <label><strong>Start Time :<span style="color:red">*</span></strong></label>
                 <asp:TextBox ID="txtStartTime" runat="server" CssClass="textbox-medium1"></asp:TextBox>
                <div class="clear"></div>
            </div>
            <div class="form-sec-row" style='display:none;'>
                <label><strong>End Time :</strong></label>
                 <asp:TextBox ID="txtendtime" runat="server" CssClass="textbox-medium1"></asp:TextBox>
                <div class="clear"></div>
            </div>
            <div class="form-sec-row">
                <label><strong>&nbsp;</strong></label>
                <asp:Button ID="Button1" runat="server" OnClick="Button1_Click" Text="Submit" OnClientClick="return ConfirmationMessage();" CssClass="submit-button"   Height="28px" />
                <asp:Button ID="Button2" runat="server" Text="Cancel" OnClick="Button2_Click" CssClass="submit-button"  Height="28px" />
                
                   
                <div class="clear"></div>
            </div>  
   
     </div>
   </asp:View>
             <asp:View ID="View2" runat="server">
     <div class="listing"  style='width:100%; height:300px; overflow:auto;'>
         <asp:GridView id="GridView1"  CssClass="grid" PagerStyle-CssClass="pgr" 
             DataKeyNames ="OperationReqID" runat="server" AutoGenerateColumns="False" 
             AllowPaging="True" PageSize ="7" 
                 OnPageIndexChanging="GridView1_PageIndexChanging"   
             SelectedRowStyle-BackColor="GreenYellow" Width="100%" 
             onrowcommand="GridView1_RowCommand" OnRowDataBound="GridView1_RowDataBound" onrowdeleting="GridView1_RowDeleting">
                <RowStyle HorizontalAlign="Center" />
                <Columns>
                    <asp:TemplateField HeaderText="Registration No">
                        <ItemTemplate>
                            <asp:Label ID="lblregno" runat="server" Text='<%# Bind("PatientRegId") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>

                      <asp:TemplateField HeaderText="OT Requisition No"  Visible="false" >
                        <ItemTemplate>
                            <asp:Label ID="lblreqno" runat="server" Text='<%# Bind("OperationReqID") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="Patient Name">
                       
                        <ItemTemplate>
                            <asp:Label ID="lblname" runat="server" Text='<%# Bind("patient_name") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                                
                    <asp:TemplateField HeaderText="Phone">
                        <ItemTemplate>                        
                            <asp:Label ID="lblphone1" runat="server" Text='<%# Bind("PhNo1") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>

                      <asp:TemplateField HeaderText="Operation Type Id"  Visible="false" >
                        <ItemTemplate>
                            <asp:Label ID="lblOperationTypeID" runat="server" Text='<%# Bind("OperationTypeID") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>

                      <asp:TemplateField HeaderText="Operation Id"  Visible="false" >
                        <ItemTemplate>
                            <asp:Label ID="lblOperationID" runat="server" Text='<%# Bind("OperationID") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>

                    
                        <asp:TemplateField HeaderText="Operation Name">
                        <ItemTemplate>                        
                            <asp:Label ID="lblotname" runat="server" Text='<%# Bind("OperationName") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    
              
                        <asp:TemplateField HeaderText="Doctor Id" Visible="false">
                        <ItemTemplate>                        
                            <asp:Label ID="lblSurgeonID" runat="server" Text='<%# Bind("SurgeonID") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>

                         <asp:TemplateField HeaderText="Doctor Name">
                        <ItemTemplate>                        
                            <asp:Label ID="lbldoc_name" runat="server" Text='<%# Bind("doc_name") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>



                        <asp:TemplateField HeaderText="AddDoc-1" Visible="false">
                        <ItemTemplate>                        
                            <asp:Label ID="lblAddDocID1" runat="server" Text='<%# Bind("AddDocID1") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>


                        <asp:TemplateField HeaderText="AddDoc-2" Visible="false">
                        <ItemTemplate>                        
                            <asp:Label ID="lblAddDocID2" runat="server" Text='<%# Bind("AddDocID2") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>


                        <asp:TemplateField HeaderText="AddDoc-3" Visible="false">
                        <ItemTemplate>                        
                            <asp:Label ID="lblAddDocID3" runat="server" Text='<%# Bind("AddDocID3") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>


                        <asp:TemplateField HeaderText="Anesthetist 1" Visible="false">
                        <ItemTemplate>                        
                            <asp:Label ID="lblAnesthetist1" runat="server" Text='<%# Bind("Anesthetist1") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>

      <asp:TemplateField HeaderText="Anesthetist 1" Visible="false">
                        <ItemTemplate>                        
                            <asp:Label ID="lblAnesthetist2" runat="server" Text='<%# Bind("Anesthetist2") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>

            <asp:TemplateField HeaderText="Implant" Visible="true">
                        <ItemTemplate>                        
                            <asp:Label ID="lblImplant" runat="server" Text='<%# Bind("Implant1") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                     
                  <asp:TemplateField HeaderText="Operation Date">
                        <ItemTemplate>                        
                            <asp:Label ID="lblotdate" runat="server" Text='<%# Bind("otdate") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>

                              <asp:TemplateField HeaderText="OT Room" Visible="false">
                        <ItemTemplate>                        
                            <asp:Label ID="lblOTRoomNo" runat="server" Text='<%# Bind("OTRoomNo") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>

                       
                    
                     <asp:TemplateField HeaderText="Schedule Time">
                        <ItemTemplate>                        
                            <asp:Label ID="lblstarttime" runat="server" Text='<%# Bind("StartTime") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>  
                    
                     <asp:TemplateField HeaderText="End Time"  Visible="false">
                        <ItemTemplate>                        
                            <asp:Label ID="lblendtime" runat="server" Text='<%# Bind("EndTime") %>'></asp:Label>
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
        </div>
    </ContentTemplate>
    </asp:UpdatePanel>
 
</asp:Content>

