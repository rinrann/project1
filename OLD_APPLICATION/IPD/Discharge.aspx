<%@ Page Title="" Language="C#" MasterPageFile="~/MasterAll/MasterPageAll.master" AutoEventWireup="true" CodeFile="Discharge.aspx.cs" Inherits="IPD_Discharge" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
  
   <script type="text/javascript" language="javascript">

    function ShowDialog() {
        var rtvalue = window.open("IpdpatientPopupForDischarge.aspx", "sss", "Width:800px; Height:550px; dialogLeft:250px;");
        //document.getElementById("ctl00_ContentPlaceHolder1_txtreg").value = rtvalue.NameValue;

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
        $("input[id$='TextBox2']").datepicker({ dateFormat: 'dd/mm/yy' });

        $("input[id$='TextBox3']").timepicker({
            showPeriod: true,
            showLeadingZero: true
        });


        $("input[id$='TextBox8']").datepicker({ dateFormat: 'dd/mm/yy', minDate: date });

        $("input[id$='TextBox9']").timepicker({
            showPeriod: true,
            showLeadingZero: true
        });

        $("input[id$='Tab2']").click(function () {


            if ($("input[id$='txtreg']").val() == '') {
                alert('Please Enter Registration No !');
                $("input[id$='txtreg']").focus();
                $("input[id$='txtreg']").addClass('textboxerr');
                return false;
            }
            else {
                $("input[id$='txtreg']").removeClass('textboxerr');
            }
        });

        $("input[id$='Button1']").click(function () {

            if ($("input[id$='TextBox2']").val() == '') {
                alert('Please Enter Discharge Date !');
                $("input[id$='TextBox2']").focus();
                $("input[id$='TextBox2']").addClass('textboxerr');
                return false;
            }
            else {
                $("input[id$='TextBox2']").removeClass('textboxerr');
            }



            if ($("input[id$='TextBox3']").val() == '') {
                alert('Please Enter Discharge Time !');
                $("input[id$='TextBox3']").focus();
                $("input[id$='TextBox3']").addClass('textboxerr');
                return false;
            }
            else {
                $("input[id$='TextBox3']").removeClass('textboxerr');
            }




            if ($("input[id$='TextBox6']").val() == '') {
                alert('Please Enter Admission Date !');
                $("input[id$='TextBox6']").focus();
                $("input[id$='TextBox6']").addClass('textboxerr');
                return false;
            }
            else {
                $("input[id$='TextBox6']").removeClass('textboxerr');
            }



            if ($("input[id$='TextBox7']").val() == '') {
                alert('Please Enter Admission Time !');
                $("input[id$='TextBox7']").focus();
                $("input[id$='TextBox7']").addClass('textboxerr');
                return false;
            }
            else {
                $("input[id$='TextBox7']").removeClass('textboxerr');
            }



            if ($("input[id$='TextBox4']").val() == '') {
                alert('Please Enter Discharge Condition !');
                $("input[id$='TextBox4']").focus();
                $("input[id$='TextBox4']").addClass('textboxerr');
                return false;
            }
            else {
                $("input[id$='TextBox4']").removeClass('textboxerr');
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
         <asp:Label ID="Label1" runat="server">Discharge Details</asp:Label>
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
                    CssClass="submit-search" Text="Quick Search" OnClientClick="ShowDialog()"  Height="27px" />
                   <asp:Button ID="Button5" runat="server" CssClass="submit-button"   Height="27px" 
                    Text="Fetch" onclick="Button5_Click" />
                <div class="clear">  </div>
            </div>
             <div class="form-sec-row">
                <label><strong>Patient's Name :</strong></label>
                <asp:TextBox ID="TextBox1" runat="server" CssClass="textbox-medium1" Enabled="False" 
                   ></asp:TextBox>
                <div class="clear"></div>
            </div>            
             <div class="form-sec-row">
                <label><strong>Refer By :</strong></label>
                <asp:TextBox ID="txtRefDocid" runat="server" CssClass="textbox-medium1" Visible="false" 
                   ></asp:TextBox>
                <asp:TextBox ID="txtRefDocnm" runat="server" CssClass="textbox-medium1" Enabled="False" 
                   ></asp:TextBox>
                <div class="clear"></div>
            </div>
             <div class="form-sec-row">
                <label><strong>Doctor Commision :</strong></label>
                <asp:TextBox ID="txtDocComm" runat="server" CssClass="textbox-medium1" Enabled="True" 
                   ></asp:TextBox>
                <div class="clear"></div>
            </div>

            
                 <div class="form-sec-row">
                <label><strong>Bed No :</strong></label>
                                <asp:TextBox ID="TextBox5" runat="server" CssClass="textbox-medium1" Enabled="False" 
                   ></asp:TextBox>           
                
                <div class="clear"></div>
            </div>


                <div class="form-sec-row">
                <label><strong>Admission Date :</strong></label>
                                <asp:TextBox ID="TextBox6" runat="server" CssClass="textbox-medium1" Enabled="False" 
                   ></asp:TextBox>           
                
                <div class="clear"></div>
            </div>


                <div class="form-sec-row">
                <label><strong>Admission Time :</strong></label>
                                <asp:TextBox ID="TextBox7" runat="server" CssClass="textbox-medium1" Enabled="False" 
                   ></asp:TextBox>           
                
                <div class="clear"></div>
            </div>


   
              <div class="form-sec-row">
                <label><strong>Discharge Date :</strong></label>
                <asp:TextBox ID="TextBox2" runat="server" CssClass="textbox-medium1"  
                   ></asp:TextBox>
                <div class="clear"></div>
            </div>

            <div class="form-sec-row">
                <label><strong>Discharge Time :</strong></label>
                <asp:TextBox ID="TextBox3" runat="server" CssClass="textbox-medium1" 
                   ></asp:TextBox>
                <div class="clear"></div>
            </div>

                   <div class="form-sec-row">
                <label><strong>Is Death :</strong></label>
                       <asp:CheckBox ID="CheckBox1" runat="server" AutoPostBack="True" 
                           oncheckedchanged="CheckBox1_CheckedChanged" />
                <div class="clear"></div>
            </div>
        
           <div class="form-sec-row">
                <label><strong>Date of Death :</strong></label>
                <asp:TextBox ID="TextBox8" runat="server" CssClass="textbox-medium1" 
                   ></asp:TextBox>
                <div class="clear"></div>
            </div>

               <div class="form-sec-row">
                <label><strong>Time of Death :</strong></label>
                <asp:TextBox ID="TextBox9" runat="server" CssClass="textbox-medium1" 
                   ></asp:TextBox>
                <div class="clear"></div>
            </div>

                 <div class="form-sec-row">
                <label><strong>Remarks :</strong></label>
                                <asp:TextBox ID="TextBox4" runat="server" CssClass="textbox-medium1" 
                   ></asp:TextBox>  
                   
                                      <cc1:AutoCompleteExtender ServiceMethod="SearchCondition"     MinimumPrefixLength="1"
           CompletionInterval="100" EnableCaching="false" 
           CompletionSetCount="10" TargetControlID="TextBox4"  ID="AutoCompleteExtender1" runat="server" FirstRowSelected = "false" ></cc1:AutoCompleteExtender>         
                
                <div class="clear"></div>
            </div>
  
     
            <div class="form-sec-row">
                <label><strong>&nbsp;</strong></label>
                <asp:Button ID="Button1" runat="server" OnClick="Button1_Click" Text="Submit" CssClass="submit-button"   Height="27px" />
                <asp:Button ID="Button2" runat="server" Text="Cancel" OnClick="Button2_Click" CssClass="submit-button"  Height="27px" />
                
                   
                <div class="clear"></div>
            </div>  
              </div>
              </asp:View>
              
                    <asp:View ID="View2" runat="server">


            <div class="listing" style='width:100%; height:200px; overflow:auto;'>
         <asp:GridView id="GridView1"  CssClass="grid" PagerStyle-CssClass="pgr" 
             DataKeyNames ="RowId" runat="server" AutoGenerateColumns="False" 
             AllowPaging="True" PageSize ="100"   
             SelectedRowStyle-BackColor="GreenYellow" Width="100%" 
                    onrowcommand="GridView1_RowCommand">
                <RowStyle HorizontalAlign="Center" />
                <Columns>
                    <asp:TemplateField HeaderText="Id" Visible="false">
                        <ItemTemplate>
                            <asp:Label ID="lblid" runat="server" Text='<%# Bind("RowId") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>

                      <asp:TemplateField HeaderText="Registration No" >
                        <ItemTemplate>
                            <asp:Label ID="lblregno" runat="server" Text='<%# Bind("PatientReg") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="Patient Name">
                       
                        <ItemTemplate>
                            <asp:Label ID="lblname" runat="server" Text='<%# Bind("patient_name") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                                
                    <asp:TemplateField HeaderText="Discharge Date">
                        <ItemTemplate>                        
                            <asp:Label ID="lbldate" runat="server" Text='<%# Bind("ddate") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                     
                  <asp:TemplateField HeaderText="Discharge Time">
                        <ItemTemplate>                        
                            <asp:Label ID="lbltime" runat="server" Text='<%# Bind("DischargeTime") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>

                     <asp:TemplateField HeaderText="Discharge Condition">
                        <ItemTemplate>                        
                            <asp:Label ID="lblcon" runat="server" Text='<%# Bind("Remarks") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
             
           
                                
                <asp:CommandField SelectText="Edit" ShowSelectButton="True" ControlStyle-CssClass="temp" HeaderText="Edit" SelectImageUrl="~/Images/edit.png" ButtonType="Image">
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

