<%@ Page Title="" Language="C#" MasterPageFile="~/MasterAll/MasterPageAll.master" AutoEventWireup="true" CodeFile="AdmissionRelatedReport.aspx.cs" Inherits="Settings_AdmissionRelatedReport" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">

    <script type="text/javascript">

    function Calling() {
        $("#ddl").hide();
        $("#textbox").hide();
        $("#po").hide();
        $("#underdoctor").hide();
        $("#referdoctor").hide();
        $("#quackdoctor").hide();

        var date = new Date();
        $("input[id$='txtFromDate']").datepicker({ dateFormat: 'dd/mm/yy' });
        $("input[id$='txtTodate']").datepicker({ dateFormat: 'dd/mm/yy' });
        
         
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


    function showStuff() {
        var el = document.getElementById('ddl');
        var e2 = document.getElementById('textbox');
        var e3 = document.getElementById('po');


        if ($("select[id$='ddlAddressType']").val() == '3') {
            el.style.display = 'block';
            e2.style.display = 'none';
            e3.style.display = 'none';
        }
        else {
            if ($("select[id$='ddlAddressType']").val() == '2') {
                el.style.display = 'none';
                e2.style.display = 'none';
                e3.style.display = 'block';
            }
            else {

                if ($("select[id$='ddlAddressType']").val() == '1') {
                    el.style.display = 'none';
                    e2.style.display = 'block';
                    e3.style.display = 'none';
                }
                else {
                    el.style.display = 'none';
                    e2.style.display = 'none';
                    e3.style.display = 'none';
                }
            }
        }
    }




    function doctorwise() {
        var el = document.getElementById('underdoctor');
        var e2 = document.getElementById('referdoctor');
        var e3 = document.getElementById('quackdoctor');


        if ($("select[id$='ddlAdmissionStatus']").val() == '4') {
            el.style.display = 'none';
            e2.style.display = 'none';
            e3.style.display = 'block';
        }
        else {
            if ($("select[id$='ddlAdmissionStatus']").val() == '3') {
                el.style.display = 'none';
                e2.style.display = 'block';
                e3.style.display = 'none';
            }
            else {

                if ($("select[id$='ddlAdmissionStatus']").val() == '2') {
                    el.style.display = 'block';
                    e2.style.display = 'none';
                    e3.style.display = 'none';
                }
                else {
                    el.style.display = 'none';
                    e2.style.display = 'none';
                    e3.style.display = 'none';
                }
            }
        }
    }
</script>


<%--For Busy Loader .............................--%>


<%-- 
  <asp:UpdateProgress ID="UpdateProgress1" runat="server">
    <ProgressTemplate>
    
       <div id="aaa" class="progressBackgroundFilter">          </div>
        <div id="bbb" class="processMessage"><img alt="Loading" src="../images/pwait.gif"/>
      
              </div>
    </ProgressTemplate>
    </asp:UpdateProgress>--%>


    <%--For Busy Loader End.............................--%>

         <asp:UpdatePanel ID="UpdatePanel1" runat="server">
    <ContentTemplate>
    <div class="pageheader">
         <asp:Label ID="Label1" runat="server">Admission Related Report</asp:Label>
     </div>
 
     <div class="formbox">
     
     <div class="form-sec">
        
            <div class="error">
                <strong><asp:Label ID="lblError" runat="server" Font-Bold="True"></asp:Label>
                </strong>
                <div class="clear"></div>
            </div>
            <table width="100%">
            <tr>
            <td> <label> From Date : </label></td>
                  <td> 
                      <asp:TextBox ID="txtFromDate" runat="server"></asp:TextBox></td>
                   <td> <label> To Date : </label></td>
                          <td> 
                      <asp:TextBox ID="txtTodate" runat="server"></asp:TextBox></td>
                          <td> <label> Religion : </label></td>
                                 <td> 
                                     <asp:DropDownList ID="ddlReligion" runat="server" Width="120px">
                                     </asp:DropDownList>
                                 </td>
            </tr></table>


                  <table width="100%">
            <tr>
            <td> <label> Select Address Type : </label></td>
                  <td>
                      <asp:DropDownList ID="ddlAddressType" Width="145px" runat="server" 
                          onchange="showStuff()" >
                          <asp:ListItem Value="0">Select</asp:ListItem>
                          <asp:ListItem Value="1">First Address</asp:ListItem>
                          <asp:ListItem Value="2">Post Office</asp:ListItem>
                          <asp:ListItem Value="3">District</asp:ListItem>
                      </asp:DropDownList>
                   </td>
                   <td>
                   <div id="textbox"> <label> Enter Address-1 : </label>
                       <asp:TextBox ID="txtAdress" runat="server"></asp:TextBox>
                       </div>

                            <div id="po"> <label> Enter Post Office : </label>
                       <asp:TextBox ID="txtPostOf" runat="server"></asp:TextBox>
                       </div>

                       <div id="ddl">
                       <label> Select District : </label>
                            <asp:DropDownList ID="ddlDistrictType" Width="160px"  runat="server">
                      </asp:DropDownList>
                      </div>

                      </td>



                      <td>
                          <asp:Label ID="Label2" runat="server" Text="Discipline"></asp:Label>
                          <asp:DropDownList ID="ddlDiscipline" runat="server" AutoPostBack="True" 
                              onselectedindexchanged="ddlDiscipline_SelectedIndexChanged">
                          </asp:DropDownList>

                      </td>

                       
                        <td>
                          <asp:Label ID="Label3" runat="server" Text="Diagonosis"></asp:Label>
                          <asp:DropDownList ID="ddlDiagonosis" runat="server">
                          </asp:DropDownList>

                      </td>


                 
                      
            </tr>
            </table>

             <table width="100%">
            <tr>
            <td><label>Admission Status :</label></td>
            <td>
                <asp:DropDownList ID="ddlAdmissionStatus"  Width="175px"  runat="server" onchange="doctorwise();">
                  <asp:ListItem Value="0">Select</asp:ListItem>
                          <asp:ListItem Value="1">Total Admission</asp:ListItem>
                          <asp:ListItem Value="2">Under Doctor</asp:ListItem>
                          <asp:ListItem Value="3">Refer Doctor</asp:ListItem>
                              <asp:ListItem Value="4">Refer Quack Doctor</asp:ListItem>
                </asp:DropDownList>
            </td>
            <td>
            <div id="underdoctor">
            <label>Select Under Doctor :</label>
             <asp:DropDownList ID="ddlUnderDoctor"  Width="175px"  runat="server">
                </asp:DropDownList>
            </div>

             <div id="referdoctor">
             <label>Select Refer Doctor :</label>
             <asp:DropDownList ID="ddlReferDoctor"  Width="175px"  runat="server">
                </asp:DropDownList>
            </div>

             <div id="quackdoctor">
             <label>Select Quack Doctor :</label>
             <asp:DropDownList ID="DropDownList6"  Width="175px"  runat="server">
                </asp:DropDownList>
            </div>
            </td>
            <td> 
                <asp:Button ID="Button1" runat="server" Height="30px" Width="90px" 
                    Text="Search" onclick="Button1_Click" /> </td>
            </tr></table>
		 
     </div>
      </div>
 <br /> 
     <div class="listing"    style='width:100%; height:450px; overflow:auto;'>
         <asp:Literal ID="ltrReport" runat="server"></asp:Literal> 
        </div>
 
   
    </ContentTemplate>
    </asp:UpdatePanel>

</asp:Content>

