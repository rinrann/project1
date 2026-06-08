<%@ Page Title="" Language="C#"  MasterPageFile="~/MasterAll/MasterPageAll.master" AutoEventWireup="true" CodeFile="DischargePatientList.aspx.cs" Inherits="IPD_DischargePatientList" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">

    <link href="../css/style.css" rel="stylesheet" type="text/css" />
    <script src="../Script/timeout-dialog.js" type="text/javascript"></script>
    <script src="../Script/snow.js" type="text/javascript"></script>
    <script src="../Script/menu.js" type="text/javascript"></script>
    <script src="../Script/jquery.ui.timepicker.js" type="text/javascript"></script>
    <script src="../Script/jquery.min.js" type="text/javascript"></script>
    <script src="../Script/jquery.js" type="text/javascript"></script>
    <script src="../Script/jquery.idle-timer.js" type="text/javascript"></script>
    <script src="../Script/jquery.dynDateTime.min.js" type="text/javascript"></script>
    <script src="../Script/Jquery.corner.js" type="text/javascript"></script>
    <script src="../Script/jquery-ui-1.8.19.custom.min.js" type="text/javascript"></script>
    <script src="../Script/jquery-1.8.2.min.js" type="text/javascript"></script>
    <script src="../Script/jquery-1.7.2.min.js" type="text/javascript"></script>
    <script src="../Script/jquery-1.4.1.min.js" type="text/javascript"></script>
    <script src="../Script/jquery-1.4.1.js" type="text/javascript"></script>
    <script src="../Script/jquery-1.4.1-vsdoc.js" type="text/javascript"></script>
    <script src="../Script/jquery-1.3.2.min.js" type="text/javascript"></script>
    <script src="../Script/datetimepicker.js" type="text/javascript"></script>
    <script src="../Script/calendar-en.min.js" type="text/javascript"></script>
    <link href="../css/calendar-blue.css" rel="stylesheet" type="text/css" />
    <link href="../css/jquery-ui-1.8.19.custom.css" rel="stylesheet" type="text/css" />
    <link href="../css/jquery-ui.css" rel="stylesheet" type="text/css" />
    <link href="../css/jquery.timepicker.css" rel="stylesheet" type="text/css" />
    <script src="../css/jquery.timepicker.js" type="text/javascript"></script>
    <script src="../css/jquery.timepicker.min.js" type="text/javascript"></script>
    <link href="../css/jquery.ui.timepicker.css" rel="stylesheet" type="text/css" />

    <link href="../css/MyCss.css" rel="stylesheet" type="text/css" />
<link type="text/css" href="css/jquery.ui.timepicker.css" rel="stylesheet" />
    <link type="text/css" href="css/calendar-blue.css" rel="stylesheet" />
    
    <script src="Script/jquery-1.4.1.min.js" type="text/javascript"></script>
    <script src="Script/jquery.dynDateTime.min.js" type="text/javascript"></script>
    <script src="Script/calendar-en.min.js" type="text/javascript"></script>




    <script src="/js/jquery-1.9.1.js" type="text/javascript"></script>
    <link href="/js/jquery-ui.css" rel="stylesheet" type="text/css" />
    <script src="/js/jquery-ui.js" type="text/javascript"></script>

    <script type="text/javascript" src="Script/jquery-ui-1.8.19.custom.min.js"></script>
    <script type="text/javascript" src="../Script/menu.js"></script>
    <script type="text/javascript" src="../Script/jquery.ui.timepicker.js"></script>
    <script src="Script/jquery-1.3.1.js" type="text/javascript"></script>

    <script type="text/javascript" language="javascript">  
       
        $(function () {
            /* date picker event*/
            $('.datepicker').datepicker({
                dateFormat: "dd/mm/yy",
                changeMonth: true,
                changeYear: true,
                //yearRange: '1900:' + new Date().getFullYear(),
                yearRange: '1900:2900',
                showOn: "button",
                buttonImage: "../images/calender.png",
                //buttonImage: "../images/green-button.gif",
                buttonImageOnly: true,
                showAnim: "fold"
            });
        });

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
            document.getElementById("ctl00_ContentPlaceHolder1_time").value = time;
            document.getElementById("ctl00_ContentPlaceHolder1_Calendar1").value = datetime;
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

        function Calling() {
            var date = new Date();
            $("input[id$='TextBox1']").datepicker({ dateFormat: 'dd/mm/yy' });

            $("input[id$='TextBox2']").datepicker({ dateFormat: 'dd/mm/yy' });
        }
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
         <asp:Label ID="Label1" runat="server">Discharge Patients </asp:Label>
     </div>

            <div class="formbox"  style='width:1150px;'>

                <div class="form-sec">
                    <div class="error">
                        <strong><asp:Label ID="lblError" runat="server" Font-Bold="True"></asp:Label></strong>
                        <div class="clear"></div>
                    </div>
                    </div>

                <table cellpadding="0" cellspacing="0"  title="Search" width="100%">
                    <tr>
                       <td>  <label><strong>Patient Name:</strong></label> </td>
                       <td>  
                            <asp:TextBox ID="txtname" runat="server" CssClass="textbox-medium1" Width="138px" ></asp:TextBox>
                            <div class="clear">
                            </div>
                       </td>
               
                       <td>  <label><strong>&nbsp;&nbsp;From Date:</strong></label> </td>
                       <td>  
                           <asp:TextBox ID="TextBox1" runat="server" CssClass="textbox-medium2" Width="100px"></asp:TextBox>
                            <div class="clear">
                            </div>
                       </td>
                        <td>  <label id="Label2"><strong>&nbsp;&nbsp;To Date :</strong></label> </td>
                       <td>  
                           <asp:TextBox ID="TextBox2" runat="server" CssClass="textbox-medium2" Width="100px"></asp:TextBox>
                            <div class="clear">
                            </div>
                       </td>
                        <td>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</td>
                        <td align="right">&nbsp;&nbsp;
                            <asp:Button ID="Button1" runat="server" Text="Search" CssClass="submit-generate" OnClick="Button1_Click" />
                        </td>
                </tr>


                </table>
    </div>

            <div class="formbox"  style='width:1150px;'>
                <div   style='width:100%;'>
                    <center>
                            <table width="100%" style='background-color:#FB7B13; color:#FFF;'>
                               <tr>
                                    <td style='width:30px;' align="center">Sl.No</td>
                                    <%--<td style='width:90px;' align="center">Regtn. No</td>--%>
                                    <td style='width:150px; padding-left:25px;' align="center">Patient Name</td> 
                                    <td style='width:70px; padding-left:20px;' align="center">Admission Date</td>
                                    <td style='width:70px;padding-left:10px;' align="center">Discharge Date</td>
                                    <td style='width:90px;padding-left:20px;' align="center">Address</td>
                                     <td style='width:90px;padding-left:20px;' align="center">Gusrdian Name</td>
                                    <td style='width:90px;padding-left:20px;' align="center">Phone No</td>
                                    <td style='width:90px;padding-left:20px;' align="center">Under Doctor</td>
                                    <td style='width:90px;padding-left:20px;' align="center">Refer By</td>
                                    <td style='width:90px;padding-left:20px;' align="center">Diagonosis</td>
                                    <td style='width:90px;padding-left:20px;' align="center">Ot Diagonosis</td>
                                </tr>
                            </table>
                            
                        </center>
                    </div>
                    <div class="listing" style='height:500px; overflow:auto;'>
                        <asp:GridView id="GridView1"  CssClass="grid" PagerStyle-CssClass="pgr" 
               DataKeyNames ="PatientReg" runat="server" SelectedRowStyle-BackColor="GreenYellow"
                 AutoGenerateColumns="False" AllowPaging="True" PageSize="100" 
                ShowHeader="false" onpageindexchanging="GridView1_PageIndexChanging" 
               onrowdatabound="GridView1_RowDataBound" Width="100%">          
				 <RowStyle HorizontalAlign="Center" />  
                    <Columns>
                        <asp:TemplateField HeaderText="Sl.No"   ItemStyle-Width="30px"  ItemStyle-HorizontalAlign="Center"> 
                        <ItemTemplate>
                            <asp:Label ID="lblSlno" runat="server" Width="30px"></asp:Label>
                        </ItemTemplate>

                        </asp:TemplateField>


                        <asp:TemplateField HeaderText="Regn No"   ItemStyle-Width="90px"  ItemStyle-HorizontalAlign="Center" Visible="false">                      
                            <ItemTemplate>
                             
							<asp:Label ID="lblregno" runat="server" Text='<%# Bind("PatientReg") %>'></asp:Label>
                                                      
                        </ItemTemplate>
                    </asp:TemplateField>

                        <asp:TemplateField HeaderText="Patient Name"   ItemStyle-Width="150px"  ItemStyle-HorizontalAlign="Center"> 
                        <ItemTemplate>
                            <asp:Label ID="lblname" runat="server" Text='<%# Bind("patient_name") %>'></asp:Label>
                        </ItemTemplate>
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="Adimssion Date"   ItemStyle-Width="70px"  ItemStyle-HorizontalAlign="Center"> 
                        <ItemTemplate>
                            <asp:Label ID="lbladmdt" runat="server" Text='<%# Bind("ADate") %>'></asp:Label>
                        </ItemTemplate>
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="Discharge Date"   ItemStyle-Width="70px"  ItemStyle-HorizontalAlign="Center"> 
                        <ItemTemplate>
                            <asp:Label ID="lbldschrgdt" runat="server" Text='<%# Bind("DiscDate") %>'></asp:Label>
                        </ItemTemplate>
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="Address"   ItemStyle-Width="70px"  ItemStyle-HorizontalAlign="Center"> 
                        <ItemTemplate>
                            <asp:Label ID="lbladdr" runat="server" Text='<%# Bind("address") %>'></asp:Label>
                        </ItemTemplate>
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="Guardian"   ItemStyle-Width="70px"  ItemStyle-HorizontalAlign="Center"> 
                        <ItemTemplate>
                            <asp:Label ID="lblgrdn" runat="server" Text='<%# Bind("guardian_name") %>'></asp:Label>
                        </ItemTemplate>
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="Phone"  ItemStyle-Width="70px"  ItemStyle-HorizontalAlign="Center"> 
                        <ItemTemplate>
                            <asp:Label ID="lblphno" runat="server" Text='<%# Bind("PhNo1") %>'></asp:Label>
                        </ItemTemplate>
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="Doctor"  ItemStyle-Width="100px"  ItemStyle-HorizontalAlign="Center"> 
                        <ItemTemplate>
                            <asp:Label ID="lbldoc" runat="server" Text='<%# Bind("DocName") %>'></asp:Label>
                        </ItemTemplate>
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="Referee" ItemStyle-Width="90px"  ItemStyle-HorizontalAlign="Center"> 
                        <ItemTemplate>
                            <asp:Label ID="lblref" runat="server" Text='<%# Bind("ReferName") %>'></asp:Label>
                        </ItemTemplate>
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="Diagonosis"   ItemStyle-Width="90px"  ItemStyle-HorizontalAlign="Center"> 
                        <ItemTemplate>
                            <asp:Label ID="lbldiagn" runat="server" Text='<%# Bind("DiagnosisName") %>'></asp:Label>
                        </ItemTemplate>
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="OT Diagonosis"   ItemStyle-Width="70px"  ItemStyle-HorizontalAlign="Center"> 
                        <ItemTemplate>
                            <asp:Label ID="lbldiagot" runat="server"></asp:Label>
                        </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                            <PagerStyle CssClass="pgr" />
                <EditRowStyle BackColor="#CCFF33" />
                <AlternatingRowStyle BackColor="#FFDB91" />
                <SelectedRowStyle BackColor="GreenYellow" />
                </asp:GridView>
                        </div>
                </div>

           </ContentTemplate>
        </asp:UpdatePanel>

    </asp:Content>