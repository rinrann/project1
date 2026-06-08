<%@ Page Title="" Language="C#" MasterPageFile="~/MasterAll/MasterPageAll.master" AutoEventWireup="true" CodeFile="AppoinmentList.aspx.cs" Inherits="DayCare_AppoinmentList" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <script type="text/javascript">

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
            if (hour >= 12) {
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

             document.getElementById("ctl00_ContentPlaceHolder1_txtdate").value = datetime;


        }

        function Calling() {

            var date = new Date();
            $("input[id$='txtdate']").datepicker({ dateFormat: 'dd/mm/yy' });

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


    function ConfirmationMessage()
     {
        var data = confirm("Are you sure you want to Delete this record ? ");
        if (data) {
            return true;
        } else {
            return false;
        }

    } 
</script>
  
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">

 
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
         <asp:Label ID="Label1" runat="server">Appointment List</asp:Label>
     </div>
     <div class="formbox">
        <div class="form-sec">
            <div class="error">
                <strong><asp:Label ID="lblError" runat="server" Font-Bold="True"></asp:Label></strong>
                <div class="clear"></div>
            </div></div>
           
            <table width="100%">
              <tr>
                <td>
                    <div class="form-sec-row"> 
             <label class="AppoList"><strong>Shift :</strong></label> 
                        <asp:DropDownList ID="DropDownList1" runat="server" CssClass="textbox-dropdown">
                        </asp:DropDownList>
            </div>
            
</td>
                <td>
                    <div class="form-sec-row"> 
             <label class="AppoList"><strong>Date :</strong></label> <asp:TextBox ID="txtdate" runat="server" CssClass="textbox-medium1" Width="138px" ></asp:TextBox>
            </div>
</td>
                <td>
                             <div class="form-sec-row"> 
             <label class="pname"><strong>Patient Name :</strong></label> <asp:TextBox ID="txtname" runat="server" CssClass="textbox-medium1" Width="138px" ></asp:TextBox>
            </div>                  
</td>
<td>
                             <div class="form-sec-row"> 
             <label class="reg"><strong>Reg No :</strong></label> <asp:TextBox ID="txtreg" runat="server" CssClass="textbox-medium1" Width="138px" ></asp:TextBox>
            </div>                  
</td>
                <td class="style1">
                             <div class="form-sec-row"> 
           <asp:Button ID="Button1" runat="server" Text="Search"  CssClass="submit-button"  Height="28px"
                                     onclick="Button1_Click1" />
            </div>                  
</td>             
                      
            </tr>
            </table>
            
            </div>
    <div>

<table cellpadding="0" cellspacing="0"  title="Search">
                
            
           <tr>
                <td colspan="5" align="center">
                    <div class="form-sec-row" style=' height:700px; overflow:auto;'> 
            <asp:GridView id="GridView1"  CssClass="grid"
                 PagerStyle-CssClass="pgr" DataKeyNames ="AppNo,PName" runat="server"  
                 AutoGenerateColumns="False" AllowPaging="True" 
                            onpageindexchanging="GridView1_PageIndexChanging" 
                            onrowcommand="GridView1_RowCommand" Width="979px" 
                            onrowdeleting="GridView1_RowDeleting" onrowdatabound="GridView1_RowDataBound" PageSize="100" 
                            >
                <RowStyle HorizontalAlign="Center" />
                <Columns>

                    <asp:TemplateField HeaderText="Sl.No"   ItemStyle-Width="30px"  ItemStyle-HorizontalAlign="Center"> 
                        <ItemTemplate>
                            <asp:Label ID="lblSlno" runat="server" Width="30px"></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="ApppoId" Visible="false">
                        <ItemTemplate>
                            <asp:Label ID="lblID" runat="server" Text='<%# Bind("AppNo") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Registration No">
                       
                        <ItemTemplate>
                            <asp:Label ID="lblregno" runat="server" Text='<%# Bind("PatientReg") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Patient Name">
                       
                        <ItemTemplate>
                            <asp:Label ID="lblname" runat="server" Text='<%# Bind("PName") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    
                    <asp:TemplateField HeaderText="Address">
                        <ItemTemplate>                        
                            <asp:Label ID="lbladdress" runat="server" Text='<%# Bind("PAddress") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField> 
                       
                    <asp:TemplateField HeaderText="Phone - 1">
                        <ItemTemplate>                        
                            <asp:Label ID="lblphone1" runat="server" Text='<%# Bind("PhNo1") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                      <asp:TemplateField HeaderText="Phone - 2"  Visible="false">
                        <ItemTemplate>                        
                            <asp:Label ID="lblphone2" runat="server" Text='<%# Bind("PhNo2") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Appointment Date">
                        <ItemTemplate>                        
                            <asp:Label ID="lblAppDate" runat="server" Text='<%# Bind("AppoDate") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Shift">
                        <ItemTemplate>                        
                            <asp:Label ID="lblshift" runat="server" Text='<%# Bind("ShiftName") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
       
       
                    <asp:TemplateField HeaderText="Reschedule">
                        <ItemTemplate>
                        <asp:LinkButton ID="LinkButton3"  CommandName="reschedule"  CommandArgument='<%# Eval("AppNo") %>'  runat="server">Reschedule</asp:LinkButton>
                         </ItemTemplate>
                    </asp:TemplateField>

                   <asp:TemplateField HeaderText="Register">
                        <ItemTemplate>
                            <asp:LinkButton ID="LinkButton1"  CommandName="select"  CommandArgument='<%# Eval("AppNo") %>'  runat="server">Register</asp:LinkButton>
                         </ItemTemplate>
                    </asp:TemplateField>
           


                       <asp:TemplateField HeaderText="Lab Req">
                        <ItemTemplate>
                        <asp:LinkButton ID="LinkButton5"  CommandName="LabReq"  CommandArgument='<%# Eval("PatientReg") %>'  runat="server">Send</asp:LinkButton>
                         </ItemTemplate>
                    </asp:TemplateField>

                    
                       <asp:TemplateField HeaderText="Charges Dtls">
                        <ItemTemplate>
                        <asp:LinkButton ID="LinkButton4"  CommandName="ChargesDtls"  CommandArgument='<%# Eval("PatientReg") %>'  runat="server">Put Charges</asp:LinkButton>
                         </ItemTemplate>
                    </asp:TemplateField>


                             <asp:TemplateField HeaderText="Cancel">
                        <ItemTemplate>
                          <asp:LinkButton ID="LinkButton2"  CommandName="cancel"  CommandArgument='<%# Eval("AppNo") %>'  OnClientClick="return ConfirmationMessage();"   runat="server">Cancel</asp:LinkButton>
                          </ItemTemplate>
                    </asp:TemplateField>
                                
                                
<%--
                       <asp:TemplateField>
                        <ItemTemplate>
                        <asp:LinkButton ID="LinkButton6"  CommandName="Discharge"  CommandArgument='<%# Eval("PatientReg") %>'  runat="server">Discharge</asp:LinkButton>
                         </ItemTemplate>
                    </asp:TemplateField>--%>
                    
                    
                </Columns>
                <PagerStyle CssClass="pgr" />
                <EditRowStyle BackColor="#CCFF33" />
                <AlternatingRowStyle BackColor="#FFDB91" />
        </asp:GridView>
            </div>
            
</td>
            </tr>
        </table>    
    </div>
    </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>

