<%@ Page Title="" Language="C#" MasterPageFile="~/MasterAll/MasterPageAll.master" AutoEventWireup="true" CodeFile="AmbulanceCharge.aspx.cs" Inherits="IPD_AmbulanceCharge" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">

     <script type="text/javascript" language="javascript">
    function ShowDialog() {
        var rtvalue = window.open("IpdpatientPopup.aspx", "sss", "Width:800px; Height:550px; dialogLeft:250px;");
        document.getElementById("ctl00_ContentPlaceHolder1_TextBox23").value = rtvalue.NameValue;

    }

    function Calling() {


        var date = new Date();
        $("input[id$='TextBox5']").datepicker({ dateFormat: 'dd/mm/yy' });

        $("input[id$='Tab2']").click(function () {


            if ($("input[id$='TextBox23']").val() == '') {
                alert('Please Enter Registration No !');
                $("input[id$='TextBox23']").focus();
                $("input[id$='TextBox23']").addClass('textboxerr');
                return false;
            }
            else {
                $("input[id$='TextBox23']").removeClass('textboxerr');
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
         <asp:Label ID="Label1" runat="server">Ambulance Charge</asp:Label>
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
            <asp:HiddenField ID="HiddenField1" runat="server" />
			           
                       
             <div class="form-sec-row">
                        <label>
                        <strong>
                       Registration No :</strong></label>
                          <asp:TextBox ID="TextBox23" runat="server" CssClass="textbox-medium1" 
                              Enabled="true"></asp:TextBox>
                                <asp:Button ID="Button3" runat="server" Text="Quick Search"  height="28px" CssClass="submit-buttonCheck" OnClientClick="ShowDialog()"/>
                              <asp:Button ID="Button2" runat="server" Text="Fetch"   height="28px"
                            CssClass="submit-button" onclick="Button2_Click"/>
                        <div class="clear">
                        </div>
                    </div>
                        <div class="form-sec-row">
                        <label>
                        <strong>
                       Patient's Name :</strong></label>
                          <asp:TextBox ID="TextBox24" runat="server" CssClass="textbox-medium1" 
                                Enabled="False"></asp:TextBox>
                        <div class="clear">
                        </div>
                    </div>

                        <div class="form-sec-row">
                        <label>
                        <strong>
                       Bed No :</strong></label>
                          <asp:TextBox ID="TextBox25" runat="server" CssClass="textbox-medium1" 
                                Enabled="False"></asp:TextBox>
                        <div class="clear">
                        </div>
                    </div>

                        <div class="form-sec-row">
                        <label>
                        <strong>
                       Admission Date :</strong></label>
                          <asp:TextBox ID="TextBox26" runat="server" CssClass="textbox-medium1" 
                                Enabled="False"></asp:TextBox>
                        <div class="clear">
                        </div>
                    </div>

                       <div class="form-sec-row">
                        <label>
                        <strong>
                       From Address :</strong></label>
                          <asp:TextBox ID="TextBox1" runat="server" CssClass="textbox-medium1" 
                              ></asp:TextBox>
                        <div class="clear">
                        </div>
                    </div>
                       <div class="form-sec-row">
                        <label>
                        <strong>
                       To Address :</strong></label>
                          <asp:TextBox ID="TextBox2" runat="server" CssClass="textbox-medium1" 
                               ></asp:TextBox>
                        <div class="clear">
                        </div>
                    </div>
                       <div class="form-sec-row">
                        <label>
                        <strong>
                      Total KM :</strong></label>
                          <asp:TextBox ID="TextBox3" runat="server" CssClass="textbox-medium1" 
                              ></asp:TextBox>
                        <div class="clear">
                        </div>
                    </div>
                       <div class="form-sec-row">
                        <label>
                        <strong>
                     Charge :</strong></label>
                          <asp:TextBox ID="TextBox4" runat="server" CssClass="textbox-medium1" 
                         ></asp:TextBox>
                        <div class="clear">
                        </div>
                    </div>
                           <div class="form-sec-row">
                        <label>
                        <strong>
                       Delivery Date :</strong></label>
                          <asp:TextBox ID="TextBox5" runat="server" CssClass="textbox-medium1" 
                            ></asp:TextBox>
                        <div class="clear">
                        </div>
                    </div>

                         <div class="form-sec-row">
                        <label> <strong>   </strong></label>
                           <asp:Button ID="Button1" runat="server" Text="Submit" CssClass="submit-button"   height="30px"
                                 onclick="Button1_Click"/>
                              <asp:Button ID="Button4" runat="server" Text="Cancel"  height="30px"
                            CssClass="submit-button" onclick="Button4_Click"/>
                        <div class="clear">
                        </div>
                    </div>
   
     </div>
     </asp:View>
           <asp:View ID="View2" runat="server">
 
       
    <div class="listing">
                <asp:GridView id="GridView1"  Width="100%" CssClass="grid" PagerStyle-CssClass="pgr"
                 DataKeyNames ="RowId" runat="server" 
                 AutoGenerateColumns="False" AllowPaging="True" 
                 SelectedRowStyle-BackColor="GreenYellow" 
                    onpageindexchanging="GridView1_PageIndexChanging" 
                    onrowcommand="GridView1_RowCommand" >
                    <Columns>
                        <asp:TemplateField HeaderText="ID" Visible="false">
                            <ItemTemplate>
                                <asp:Label ID="lblid" runat="server" Text='<%# Bind("RowId") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
             
                        <asp:TemplateField HeaderText="Registration No">
                            <ItemTemplate>
                                <asp:Label ID="lblRegno" runat="server" Text='<%# Bind("PatientReg") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>


                         <asp:TemplateField HeaderText="Patient's Name">
                            <ItemTemplate>
                                <asp:Label ID="lbllname" runat="server" Text='<%# Bind("patient_name") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>

                         <asp:TemplateField HeaderText="Admission Date">
                            <ItemTemplate>
                                <asp:Label ID="lbladate" runat="server" Text='<%# Bind("adate") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="Bed No">
                            <ItemTemplate>
                                <asp:Label ID="lblbedno" runat="server" Text='<%# Bind("BedNoText") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>

                         <asp:TemplateField HeaderText="From Address" >
                            <ItemTemplate>
                                <asp:Label ID="lblserivcecat" runat="server" Text='<%# Bind("FromAddress") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                             <asp:TemplateField HeaderText="To Address" >
                            <ItemTemplate>
                                <asp:Label ID="lblservice" runat="server" Text='<%# Bind("ToAddress") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>   

                             <asp:TemplateField HeaderText="Total KM" >
                            <ItemTemplate>
                                <asp:Label ID="lbllblissuedate" runat="server" Text='<%# Bind("Kelometer") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>

                         <asp:TemplateField HeaderText="Charges" >
                            <ItemTemplate>
                                <asp:Label ID="lblquantity" runat="server" Text='<%# Bind("Charges") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                                        
                    <asp:TemplateField HeaderText="Delivery Date" >
                            <ItemTemplate>
                                <asp:Label ID="lbladvicedby" runat="server" Text='<%# Bind("DelDate") %>'></asp:Label>
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

