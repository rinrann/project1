<%@ Page Title="" Language="C#" MasterPageFile="~/MasterAll/MasterPageAll.master" AutoEventWireup="true" CodeFile="AllDoctorVisit.aspx.cs" Inherits="Assignment_AllDoctorVisit" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">


<script language="javascript" type="text/javascript">



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
        document.getElementById("ctl00_ContentPlaceHolder1_txtTimeofVisit").value = time;
        document.getElementById("ctl00_ContentPlaceHolder1_txtDateOfVisit").value = datetime;
    }


    function Calling() {

        //            var date = new Date();
        //            $("input[id$='txtvalidityDate']").datepicker({ dateFormat: 'dd/mm/yy', minDate: date });

        var date = new Date();
        $("input[id$='txtDateOfVisit']").datepicker({ dateFormat: 'dd/mm/yy' });


        $("input[id$='txtTimeofVisit']").timepicker({
            showPeriod: true,
            showLeadingZero: true
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


 <asp:UpdatePanel ID="UpdatePanel1" runat="server">
  <ContentTemplate>

  <div class="pageheader">
         <asp:Label ID="Label1" runat="server">All Doctor Visit</asp:Label>
     </div>
 
        <div class="form-sec">
            <div class="error">
                <strong><asp:Label ID="lblError" runat="server" Font-Bold="True"></asp:Label></strong>
                <div class="clear"></div>
            </div></div>
             
                 <div class="formbox">
            <table width="100%">

      
       <tr>

            <td>  <label><strong>Doctor Type:</strong></label> </td>
              <td>  
                  <asp:DropDownList ID="ddlDoctorType" CssClass="textbox-medium1"  Width="150px"  AutoPostBack="true"
                      runat="server" onselectedindexchanged="ddlDoctorType_SelectedIndexChanged">
                  </asp:DropDownList>
              </td>

       
               <td>  <label><strong>Dioctor:</strong></label> </td>
              <td>  
                 
                  <asp:DropDownList ID="ddlDoctor" CssClass="textbox-medium1"  Width="150px"  runat="server">
                  </asp:DropDownList>
              </td>

            
               <td> <asp:Button ID="btnSearch" runat="server"  CssClass="submit-generate" 
                       Text="Search" onclick="btnSearch_Click" /></td>


           </tr>
           </table>
          
           <div style='width:100%; height:200px; overflow:scroll;'>


              <asp:GridView id="GridView1"  CssClass="grid"
                 PagerStyle-CssClass="pgr"  runat="server"  ShowHeader="true"
                 AutoGenerateColumns="False" AllowPaging="True" PageSize ="100"  Width="100%" 
                 onrowdatabound="GridView1_RowDataBound">
                <RowStyle HorizontalAlign="Center" />
                <Columns>
                    

                    <asp:TemplateField HeaderText="Select" ItemStyle-Width="150px">
                        <ItemTemplate>
                            <asp:CheckBox ID="chkSelect" runat="server" />
                        </ItemTemplate>
                    </asp:TemplateField>

                       <asp:TemplateField HeaderText="RegNo" ItemStyle-Width="150px">
                        <ItemTemplate>
                            <asp:Label ID="lblPatientReg" runat="server" Text='<%# Bind("PatientReg") %>'></asp:Label>
                                <asp:Label ID="Label3" runat="server" ForeColor="Green"></asp:Label>   
                        </ItemTemplate>
                    </asp:TemplateField>

                     <asp:TemplateField HeaderText="PatientName" ItemStyle-Width="170px">
                        <ItemTemplate>
                            <asp:Label ID="lblPatientName" runat="server" Text='<%# Bind("patient_name") %>'></asp:Label>
                           
                        </ItemTemplate>
                    </asp:TemplateField>

                     <asp:TemplateField HeaderText="Address" ItemStyle-Width="150px">
                        <ItemTemplate>
                            <asp:Label ID="lblAddress" runat="server" Text='<%# Bind("vill_city") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>

                      <asp:TemplateField HeaderText="BedNo" ItemStyle-Width="150px">
                        <ItemTemplate>
                            <asp:Label ID="lblBedNo" runat="server" Text='<%# Bind("BedNoText") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>

                      <asp:TemplateField HeaderText="AdmissionDate" ItemStyle-Width="150px">
                        <ItemTemplate>
                            <asp:Label ID="lblAdmissionDate" runat="server" Text='<%# Bind("AdmissionDate") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>

                     <asp:TemplateField HeaderText="DiagnosisName" ItemStyle-Width="150px">
                        <ItemTemplate>
                            <asp:Label ID="lblDiagnosisName" runat="server" Text='<%# Bind("DiagnosisName") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>

                      <asp:TemplateField HeaderText="DocId" ItemStyle-Width="150px" Visible="false">
                        <ItemTemplate>
                            <asp:Label ID="lbldoc_id" runat="server" Text='<%# Bind("doc_id") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>

                        <asp:TemplateField HeaderText="DocTypeId" ItemStyle-Width="150px" Visible="false">
                        <ItemTemplate>
                            <asp:Label ID="lblDocTypeId" runat="server" Text='<%# Bind("DocTypeId") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>



               
                   <%-- <asp:CommandField SelectText="Edit" ShowSelectButton="True"    ItemStyle-Width="70px"  ControlStyle-CssClass="temp" HeaderText="Edit" SelectImageUrl="~/Images/edit.png" ButtonType="Image">
                        <ControlStyle CssClass="temp"></ControlStyle>
                    </asp:CommandField>
                    <asp:CommandField ShowDeleteButton="True" ControlStyle-CssClass="temp"    ItemStyle-Width="70px"  HeaderText="Delete" DeleteImageUrl="~/Images/delete.png" ButtonType="Image">
                        <ControlStyle CssClass="temp"></ControlStyle>
                    </asp:CommandField>--%>
                </Columns>
                <PagerStyle CssClass="pgr" />
                <EditRowStyle BackColor="#CCFF33" />
                <AlternatingRowStyle BackColor="#FFDB91" />
        </asp:GridView> 

           </div>
           
             <div class="form-sec-row">
         <label><strong>Date Of Visit:</strong></label> 
               <asp:TextBox ID="txtDateOfVisit" CssClass="textbox-medium1" runat="server"></asp:TextBox>
                             <cc1:MaskedEditExtender ID="MaskedEditExtender1" runat="server" TargetControlID="txtDateOfVisit"
                                            Mask="99/99/9999" MessageValidatorTip="true" MaskType="Date" DisplayMoney="Left"
                                            AcceptNegative="Left" ErrorTooltipEnabled="True" CultureName="en-GB" />
            <div class="clear">
                        </div>
                    </div>


                        <div class="form-sec-row">
            <label><strong>Time Of Visit:</strong></label> 
               <asp:TextBox ID="txtTimeofVisit" CssClass="textbox-medium1" runat="server" ></asp:TextBox>
           <div class="clear">
                        </div>
                    </div> 


            <div class="form-sec-row">
        <label><strong>Remarks:</strong></label>  
               <asp:TextBox ID="txtRemarks" CssClass="textbox-medium1" runat="server" textmode="MultiLine"  Height="47px"></asp:TextBox>
             <div class="clear">
                        </div>
                    </div> 

              <div class="form-sec-row">
          <label><strong>Bill Effect:</strong></label>
               <asp:CheckBox ID="CheckBox1" runat="server" />
              <div class="clear">
                        </div>
                    </div> 

               <div class="form-sec-row">
                   <label><strong> </strong></label>
                    <asp:Button ID="Button1" runat="server" CssClass="submit-button" Text="Submit" onclick="Button1_Click" />
                                    <asp:Button ID="btnCancel"  CssClass="submit-button" runat="server" Text="Cancel" />
           <div class="clear">
                        </div>
                    </div> 
     </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>

