<%@ Page Title="" Language="C#" MasterPageFile="~/MasterAll/MasterPageAll.master" AutoEventWireup="true" CodeFile="AnesthesiaNote.aspx.cs" Inherits="IPD_AnesthesiaNote" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
 

<script type="text/javascript" language="javascript">
    function ShowDialog() {
        var rtvalue = window.open("RegistrationPopup.aspx", "sss", "Width:800px; Height:550px; dialogLeft:250px;");
        document.getElementById("ctl00_ContentPlaceHolder1_TextBox1").value = rtvalue.NameValue;

    }


    function Calling() {

        $(function () {
            $("#tabs").tabs();
        });

        var date = new Date();
        $("input[id$='TextBox6']").datepicker({ dateFormat: 'dd/mm/yy' });
        $("input[id$='TextBox7']").timepicker({
            showPeriod: true,
            showLeadingZero: true
        });
        $("input[id$='txtPostTime']").timepicker({
            showPeriod: true,
            showLeadingZero: true
        });
        $("input[id$='Tab2']").click(function () {


            if ($("input[id$='TextBox1']").val() == '') {
                alert('Please Enter Registration No !');
                $("input[id$='TextBox1']").focus();
                $("input[id$='TextBox1']").addClass('textboxerr');
                return false;
            }
            else {
                $("input[id$='TextBox1']").removeClass('textboxerr');
            }
        });

        $("input[id$='Button1']").click(function () {

            if ($("select[id$='DropDownList4']").val() == '0') {
                alert('Please Select Mode!');
                $("select[id$='DropDownList4']").addClass('textboxerr');
                $("select[id$='DropDownList4']").focus();
                return false;
            }
            else {
                $("select[id$='DropDownList4']").removeClass('textboxerr');
            }

            if ($("input[id$='TextBox7']").val() == '') {
                alert('Please Enter Time Properly!');
                $("input[id$='TextBox7']").focus();
                $("input[id$='TextBox7']").addClass('textboxerr');
                return false;
            }
            else {
                $("input[id$='TextBox7']").removeClass('textboxerr');
            }

            if ($("input[id$='TextBox6']").val() == '') {
                alert('Please Enter Date Properly!');
                $("input[id$='TextBox6']").focus();
                $("input[id$='TextBox6']").addClass('textboxerr');
                return false;
            }
            else {
                $("input[id$='TextBox6']").removeClass('textboxerr');
            }
        });


        $("input[id$='TextBox8']").keydown(function (event) {
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


        $("input[id$='TextBox16']").keydown(function (event) {
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
                <asp:Label ID="Label1" runat="server">Anesthesia Note</asp:Label>
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
                    <asp:HiddenField ID="HiddenField1" runat="server" /><asp:HiddenField ID="HiddenField2" runat="server" />

                    <asp:HiddenField ID="HiddenField3" runat="server" /><asp:HiddenField ID="HiddenField4" runat="server" />

    
                      <div class="form-sec-row">
                        <label>
                        <strong>
                       Registration No :</strong></label>
                          <asp:TextBox ID="TextBox1" runat="server" CssClass="textbox-medium1" 
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
                       Requisition No :</strong></label>
                          <asp:TextBox ID="TextBox2" runat="server" CssClass="textbox-medium1" 
                                Enabled="False"></asp:TextBox>
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
                       Operation Name :</strong></label>
                          <asp:TextBox ID="TextBox15" runat="server" CssClass="textbox-medium1" 
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

                     
                     <div class="form-sec-row">
                        <label>
                        <strong> 
                       Date :</strong></label>
                        <asp:TextBox ID="TextBox6" runat="server" CssClass="textbox-medium1"></asp:TextBox>
                          <cc1:MaskedEditExtender ID="MaskedEditExtender2" runat="server" TargetControlID="TextBox6"
                                            Mask="99/99/9999" MessageValidatorTip="true" MaskType="Date" DisplayMoney="Left"
                                            AcceptNegative="Left" ErrorTooltipEnabled="True" CultureName="en-GB" />
                        <div class="clear">
                        </div>
                    </div>

                    
                     
                     

                    
              <div class="form-sec-row">
                        <label style="display:none;">
                        <strong> 
                        Mode :</strong></label>
                        <asp:DropDownList ID="DropDownList4" runat="server" CssClass="textbox-medium1"  Height="30px" Visible="false">                               
                         </asp:DropDownList>
                        <div class="clear">
                        </div>
                    </div>




                    <div id="tabs">
  <ul>
  <li><a href="#tabs-1">Pre Mode</a></li>
	<li><a href="#tabs-2">Post Mode</a></li>
  </ul>
  <div id="tabs-1">
     <div class="form-sec-row">
                        <label>
                        <strong> 
                      Pre BP :</strong></label>
                       <asp:TextBox ID="txtPreBp"  CssClass="textbox-medium1" runat="server"></asp:TextBox>
                                   <cc1:MaskedEditExtender ID="MEdExt1" runat="server" TargetControlID="txtPreBp" Mask="999/999"
                                MessageValidatorTip="true"  DisplayMoney="Left" AcceptNegative="Left" ClearMaskOnLostFocus="false"
                                ErrorTooltipEnabled="True" />
                        <div class="clear">
                        </div>
                    </div>

       <div class="form-sec-row">
                        <label>
                        <strong> 
                      Pre Pulse :</strong></label>
                       <asp:TextBox ID="txtPrePulse"  CssClass="textbox-medium1" runat="server" MaxLength="3"  ></asp:TextBox>
                        <div class="clear">
                        </div>
                    </div>

       <div class="form-sec-row">
                        <label>
                        <strong> 
                       Pre Chest :</strong></label>
                        <asp:TextBox ID="txtPreChest"  CssClass="textbox-medium1"  runat="server" ></asp:TextBox>
                        <div class="clear">
                        </div>
                    </div>

       <div class="form-sec-row">
                        <label>
                        <strong> 
                      Pre O2 :</strong></label>
                       <asp:TextBox ID="txtPreO2" CssClass="textbox-medium1"  runat="server"></asp:TextBox>
                        <div class="clear">
                        </div>
                    </div>

       <div class="form-sec-row">
                        <label>
                        <strong> 
                      Pre Risk Factor :</strong></label>
                        <asp:TextBox ID="txtPreRiskfactor"  CssClass="textbox-medium1"   runat="server"></asp:TextBox>
                        <div class="clear">
                        </div>
                    </div>

       <div class="form-sec-row">
                        <label>
                        <strong> 
                       Pre Others :</strong></label>
                     <asp:TextBox ID="txtPreOthers" CssClass="textbox-medium1" runat="server"></asp:TextBox>
                        <div class="clear">
                        </div>
                    </div>
      <div class="form-sec-row">
                        <label>
                        <strong> 
                       Pre Time :</strong></label>
                        <asp:TextBox ID="TextBox7" runat="server" CssClass="textbox-medium1"></asp:TextBox>
                        <div class="clear">
                        </div>
                    </div>

       <div class="form-sec-row">
                        <label>
                        <strong> 
                       Pre Remarks :</strong></label>
                        <asp:TextBox ID="txtPreRemarks" runat="server"  CssClass="textbox-medium1"  TextMode="MultiLine"  Height="60px"></asp:TextBox>
                        <div class="clear">
                        </div>
                    </div>
      
  </div>


  <div id="tabs-2">
 
       <div class="form-sec-row">
                        <label>
                        <strong> 
                        Post BP :</strong></label>
                        <asp:TextBox ID="txtPostBP" CssClass="textbox-medium1"  runat="server"></asp:TextBox>
                                  <cc1:MaskedEditExtender ID="MaskedEditExtender1" runat="server" TargetControlID="txtPostBP" Mask="999/999"
                                MessageValidatorTip="true" ClearMaskOnLostFocus="false" ClearTextOnInvalid="false" 
InputDirection="LeftToRight" AcceptNegative="Left"   />
                        <div class="clear">
                        </div>
                    </div>

       <div class="form-sec-row">
                        <label>
                        <strong> 
                       Post Pulse :</strong></label>
                       <asp:TextBox ID="txtPostPulse" CssClass="textbox-medium1"  runat="server"></asp:TextBox>
                        <div class="clear">
                        </div>
                    </div>

       <div class="form-sec-row">
                        <label>
                        <strong> 
                       Post Chest :</strong></label>
                       <asp:TextBox ID="txtPostChest"  CssClass="textbox-medium1"  runat="server"></asp:TextBox>
                        <div class="clear">
                        </div>
                    </div>

       <div class="form-sec-row">
                        <label>
                        <strong> 
                      Post O2 :</strong></label>
                        <asp:TextBox ID="txtPostO2"  CssClass="textbox-medium1"  runat="server"></asp:TextBox>
                        <div class="clear">
                        </div>
                    </div>

       <div class="form-sec-row">
                        <label>
                        <strong> 
                       Post Risk Factor :</strong></label>
                      <asp:TextBox ID="txtPostRiskFactor"   CssClass="textbox-medium1"  runat="server"></asp:TextBox>
                        <div class="clear">
                        </div>
                    </div>

       <div class="form-sec-row">
                        <label>
                        <strong> 
                      Post Others :</strong></label>
                         <asp:TextBox ID="txtPostOthers" CssClass="textbox-medium1"  runat="server"></asp:TextBox>
                        <div class="clear">
                        </div>
                    </div>
      <div class="form-sec-row">
                        <label>
                        <strong> 
                       Post Time :</strong></label>
                        <asp:TextBox ID="txtPostTime" runat="server" CssClass="textbox-medium1"></asp:TextBox>
                        <div class="clear">
                        </div>
                    </div>

       <div class="form-sec-row">
                        <label>
                        <strong> 
                      Post Remarks :</strong></label>
                        <asp:TextBox ID="txtPostRemarks" runat="server"   CssClass="textbox-medium1"  TextMode="MultiLine"  Height="60px"></asp:TextBox>
                        <div class="clear">
                        </div>
                    </div>

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
                <asp:GridView id="GridView2"  Width="100%" CssClass="grid" PagerStyle-CssClass="pgr"
                 DataKeyNames ="OperationReqID" runat="server" 
                 AutoGenerateColumns="False" AllowPaging="True" 
                 SelectedRowStyle-BackColor="GreenYellow" 
                            onpageindexchanging="GridView2_PageIndexChanging" 
                            onrowcommand="GridView2_RowCommand"  >
                    <Columns>
                      <asp:CommandField SelectText="Select" ShowSelectButton="True" />
                  
                        <asp:TemplateField HeaderText="Requisition No" >
                            <ItemTemplate>
                                <asp:Label ID="lblid" runat="server" Text='<%# Bind("OperationReqID") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
             
                        <asp:TemplateField HeaderText="Registration No" Visible="false">
                            <ItemTemplate>
                                <asp:Label ID="lblRegno" runat="server" Text='<%# Bind("PatientRegId") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>


                         <asp:TemplateField HeaderText="Operation Type">
                            <ItemTemplate>
                                <asp:Label ID="lbllname" runat="server" Text='<%# Bind("OperationTypeName") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>

                         <asp:TemplateField HeaderText="Operation Name">
                            <ItemTemplate>
                                <asp:Label ID="lbladate" runat="server" Text='<%# Bind("OperationName") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>

                     </Columns>
                  <PagerStyle CssClass="pgr" />
                    <EditRowStyle BackColor="#CCFF33" />
                    <AlternatingRowStyle BackColor="#FFDB91" />
                </asp:GridView>
            </div>
            <br /><br />

                    <div class="listing">
                <asp:GridView id="GridView1"  Width="100%" CssClass="grid" PagerStyle-CssClass="pgr"
                 DataKeyNames ="AnesthesiaNoteId" runat="server" 
                 AutoGenerateColumns="False" AllowPaging="True" 
                 SelectedRowStyle-BackColor="GreenYellow" 
                    onpageindexchanging="GridView1_PageIndexChanging" 
                    onrowcommand="GridView1_RowCommand" >
                    <Columns>
                        <asp:TemplateField HeaderText="ID" Visible="false">
                            <ItemTemplate>
                                <asp:Label ID="lblid" runat="server" Text='<%# Bind("AnesthesiaNoteId") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
             
                        <asp:TemplateField HeaderText="Registration No" Visible="false">
                            <ItemTemplate>
                                <asp:Label ID="lblRegno" runat="server" Text='<%# Bind("PatientReg") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>

                          <asp:TemplateField HeaderText="Operation Name" Visible="false" >
                            <ItemTemplate>
                                <asp:Label ID="lblOTName" runat="server" Text='<%# Bind("OperationName") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                            <asp:TemplateField HeaderText="ReqID" Visible="false">
                            <ItemTemplate>
                                <asp:Label ID="lblReq" runat="server" Text='<%# Bind("OperationReqID") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>

                                <asp:TemplateField HeaderText="Date" >
                            <ItemTemplate>
                                <asp:Label ID="lbldate" runat="server" Text='<%# Bind("dt") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                             <asp:TemplateField HeaderText="Time" >
                            <ItemTemplate>
                                <asp:Label ID="lbltime" runat="server" Text='<%# Bind("Time") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>  

                         <asp:TemplateField HeaderText="Patient's Name">
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

                  

                             <asp:TemplateField HeaderText="BP" >
                            <ItemTemplate>
                                <asp:Label ID="lblbp" runat="server" Text='<%# Bind("PreBP") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>

                         <asp:TemplateField HeaderText="Pulse" >
                            <ItemTemplate>
                                <asp:Label ID="lblpulse" runat="server" Text='<%# Bind("PrePulse") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                                        
                    <asp:TemplateField HeaderText="Chest" >
                            <ItemTemplate>
                                <asp:Label ID="lblchest" runat="server" Text='<%# Bind("PreChest") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>


                                    <asp:TemplateField HeaderText="O2" >
                            <ItemTemplate>
                                <asp:Label ID="lblo2" runat="server" Text='<%# Bind("PreO2") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>   

                             <asp:TemplateField HeaderText="Risk Factor" >
                            <ItemTemplate>
                                <asp:Label ID="lblriskfactor" runat="server" Text='<%# Bind("PreRiskFactor") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>

<%--                         <asp:TemplateField HeaderText="Mode" Visible="false" >
                            <ItemTemplate>
                                <asp:Label ID="lblmode" runat="server" Text='<%# Bind("Mode") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>--%>

                         <asp:TemplateField HeaderText="Others" >
                            <ItemTemplate>
                                <asp:Label ID="lblothers" runat="server" Text='<%# Bind("PreOthers") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                                        
                    <asp:TemplateField HeaderText="Remarks" Visible="false" >
                            <ItemTemplate>
                                <asp:Label ID="lblremarks" runat="server" Text='<%# Bind("PreRemarks") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="postBp" Visible="false" >
                            <ItemTemplate>
                                <asp:Label ID="lblpostBp" runat="server" Text='<%# Bind("PostBP") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                       <asp:TemplateField HeaderText="postpulse" Visible="false" >
                            <ItemTemplate>
                                <asp:Label ID="lblpostpulse" runat="server" Text='<%# Bind("PostPulse") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="postchest" Visible="false" >
                            <ItemTemplate>
                                <asp:Label ID="lblpostchest" runat="server" Text='<%# Bind("PostChest") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="postO2" Visible="false" >
                            <ItemTemplate>
                                <asp:Label ID="lblposto2" runat="server" Text='<%# Bind("PostO2") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="postristfactor" Visible="false" >
                            <ItemTemplate>
                                <asp:Label ID="lblpostriskfactor" runat="server" Text='<%# Bind("PostRiskFactor") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="postother" Visible="false" >
                            <ItemTemplate>
                                <asp:Label ID="lblpostother" runat="server" Text='<%# Bind("PostOthers") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="postremarks" Visible="false" >
                            <ItemTemplate>
                                <asp:Label ID="lblpostremarks" runat="server" Text='<%# Bind("PostRemarks") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>

              <asp:CommandField SelectText="Edit" ShowSelectButton="True" ControlStyle-CssClass="temp" HeaderText="Edit" SelectImageUrl="~/Images/edit.png" ButtonType="Image">
                            <ControlStyle CssClass="temp"></ControlStyle>
                        </asp:CommandField>
       <%--      <asp:CommandField ShowDeleteButton="True" ControlStyle-CssClass="temp" HeaderText="Delete" DeleteImageUrl="~/Images/delete.png" ButtonType="Image">
                            <ControlStyle CssClass="temp"></ControlStyle>
                        </asp:CommandField>--%>
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

